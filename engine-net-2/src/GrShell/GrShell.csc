options {
	STATIC=false;
}

PARSER_BEGIN(GrShell)
    namespace de.unika.ipd.grGen.grShell;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using de.unika.ipd.grGen.libGr;
    using de.unika.ipd.grGen.libGr.sequenceParser;
    using grIO;

    public class GrShell {
        GrShellImpl impl = null;
        bool valid;
        public bool Quit = false;
        public bool Eof = false;
        public bool ShowPrompt = true;
        bool readFromConsole = false;
        public IWorkaround workaround; 
        bool noError;
        bool exitOnError = false;

		public void SetImpl(GrShellImpl impl)
		{
			this.impl = impl;
		}
		
        static int Main(string[] args)
        {
            String command = null;
            ArrayList scriptFilename = new ArrayList();
            bool showUsage = false;
            bool nonDebugNonGuiExitOnError = false;
			int errorCode = 0; // 0==success, the return value
            
            GrShellImpl.PrintVersion();

            for(int i = 0; i < args.Length; i++)
            {
                if(args[i][0] == '-')
                {
                    if(args[i] == "-C")
                    {
                        if(command != null)
                        {
                            Console.WriteLine("Another command has already been specified with -C!");
                            errorCode = -1;
                            showUsage = true;
                            break;
                        }
                        if(i + 1 >= args.Length)
                        {
                            Console.WriteLine("Missing parameter for -C option!");
                            errorCode = -1;
                            showUsage = true;
                            break;
                        }
                        command = args[i + 1];
                        i++;
                    }
                    else if(args[i] == "-N")
                    {
                        nonDebugNonGuiExitOnError = true;
                    }
                    else if(args[i] == "--help")
                    {
	                    Console.WriteLine("Displays help");                       
                        showUsage = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Illegal option: " + args[i]);
                        showUsage = true;
                        errorCode = -1;
                        break;
                    }
                }
                else
                {
                    String filename = args[i];
                    if(!File.Exists(filename))
                    {
                        filename = filename + ".grs";
                        if(!File.Exists(filename))
                        {
                            Console.WriteLine("The script file \"" + args[i] + "\" or \"" + filename + "\" does not exist!");
                            showUsage = true;
                            errorCode = -1;
                            break;
                        }
                    }
                    scriptFilename.Add(filename);
                }
            }

            // if(args[args.Length - 1] == "--noquitateof") readFromConsole = false;	// TODO: Readd this?

            if(showUsage)
            {
                Console.WriteLine("Usage: GrShell [-C <command>] [<grs-file>]...");
                Console.WriteLine("If called without options, GrShell is started awaiting user input. (Type help for help.)");
                Console.WriteLine("Options:");
                Console.WriteLine("  -C <command> Specifies a command to be executed >first<. Using");
                Console.WriteLine("               ';;' as a delimiter it can actually contain multiple shell commands");
                Console.WriteLine("  -N           non-interactive non-gui shell which exits on error instead of waiting for user input");
                Console.WriteLine("  <grs-file>   Includes the grs-file(s) in the given order");
                return errorCode;
            }
            
            IWorkaround workaround = WorkaroundManager.Workaround;
            TextReader reader;
            bool showPrompt;
            bool readFromConsole;

            if(command != null)
            {
                reader = new StringReader(command);
                showPrompt = false;
                readFromConsole = false;
            }
            else if(scriptFilename.Count != 0)
            {
                try
                {
                    reader = new StreamReader((String) scriptFilename[0]);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Unable to read file \"" + scriptFilename[0] + "\": " + e.Message);
                    return -1;
                }
                scriptFilename.RemoveAt(0);
                showPrompt = false;
                readFromConsole = false;
            }
            else
            {
                reader = workaround.In;
                showPrompt = true;
                readFromConsole = true;
            }

            GrShell shell = new GrShell(reader);
            shell.ShowPrompt = showPrompt;
            shell.readFromConsole = readFromConsole;
            shell.workaround = workaround;
            shell.impl = new GrShellImpl();
            shell.impl.TokenSourceStack.AddFirst(shell.token_source);
            shell.impl.nonDebugNonGuiExitOnError = nonDebugNonGuiExitOnError;
            try
            {
                while(!shell.Quit && !shell.Eof)
                {
                    bool noError = shell.ParseShellCommand();
                    if(!shell.readFromConsole && (shell.Eof || !noError))
                    {
	                    if(nonDebugNonGuiExitOnError && !noError) {
		                    return -1;
	                    } 
	                    
                        if(scriptFilename.Count != 0)
                        {
                            TextReader newReader;
                            try
                            {
                                newReader = new StreamReader((String) scriptFilename[0]);
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("Unable to read file \"" + scriptFilename[0] + "\": " + e.Message);
                                return -1;
                            }
                            scriptFilename.RemoveAt(0);
                            shell.ReInit(newReader);
                            shell.Eof = false;
                            reader.Close();
                            reader = newReader;
                        }
                        else
                        {
                            shell.ReInit(workaround.In);
                            shell.impl.TokenSourceStack.RemoveFirst();
                            shell.impl.TokenSourceStack.AddFirst(shell.token_source);
                            shell.ShowPrompt = true;
                            shell.readFromConsole = true;
                            shell.Eof = false;
                            reader.Close();
                        }
                    }
                }
            }
			catch(Exception e)
			{
				Console.WriteLine("exit due to " + e.Message);
				errorCode = -2;
			}
            finally
            {
                shell.impl.Cleanup();
            }
            return errorCode;
        }
    }
PARSER_END(GrShell)

// characters to be skipped
SKIP: {
	" " |
	"\t" |
	"\r" |
	"\\\r\n" |
	"\\\n" |
	"\\\r"
}

TOKEN: {
    < NL: "\n" >
|   < QUOTE: "\"" >
|   < SINGLEQUOTE: "\'" >
|   < COLON: ":" >
|   < DOUBLESEMICOLON: ";;" >
|   < SEMICOLON: ";" >
|   < EQUAL: "=" >
|   < DOT: "." >
|   < COMMA: "," >
|   < DOLLAR: "$" >
|   < DOUBLEAMPERSAND: "&&" >
|   < AMPERSAND: "&" >
|   < DOUBLEPIPE: "||" >
|   < PIPE: "|" >
|   < CIRCUMFLEX: "^" >
|   < ARROW: "->" >
|   < MINUS: "-" >
|   < STAR: "*" >
|   < PLUS: "+" >
|   < EXCLAMATIONMARK: "!" >
|   < QUESTIONMARK: "?" >
|   < PERCENT: "%" >
|   < LPARENTHESIS: "(" >
|   < RPARENTHESIS: ")" >
|   < LBRACE: "{" >
|   < RBRACE: "}" >
|   < LBOXBRACKET: "[" >
|   < RBOXBRACKET: "]" >
|   < LANGLE: "<" >
|   < RANGLE: ">" >
|   < AT : "@" >
}

TOKEN: {
    < ACTIONS: "actions" >
|   < ADD: "add" >
|   < ALLOCVISITFLAG: "allocvisitflag" >
|   < APPLY: "apply" >
|   < ASKFOR: "askfor" >
|   < ATTRIBUTES: "attributes" >
|   < BACKEND: "backend" >
|   < BORDERCOLOR: "bordercolor" >
|   < BY: "by" >
|   < CLEAR: "clear" >
|   < COLOR: "color" >
|   < CUSTOM: "custom" >
|   < DEBUG: "debug" >
|   < DEF: "def" >
|   < DELETE: "delete" >
|   < DISABLE: "disable" >
|   < DUMP: "dump" >
|   < ECHO: "echo">
|   < EDGE: "edge" >
|   < EDGES: "edges" >
|   < EMIT: "emit" >
|   < ENABLE: "enable" >
|   < EXCLUDE: "exclude" >
|   < EXPORT: "export" >
|   < EXIT: "exit" >
|   < EXITONFAILURE: "exitonfailure" >
|   < FALSE: "false" >
|   < FILE: "file" >
|   < FREEVISITFLAG: "freevisitflag" >
|   < GET: "get" >
|   < GRAPH: "graph" >
|   < GRAPHS: "graphs" >
|   < GROUP: "group" >
|   < GRS: "grs" >
|   < HELP: "help" >
|   < HIDDEN: "hidden" >
|   < IMPORT: "import" >
|   < INCLUDE: "include" >
|   < INFOTAG: "infotag" >
|   < IO: "io" >
|   < IS: "is" >
|   < ISVISITED: "isvisited" >
|   < LABELS: "labels" >
|   < LAYOUT: "layout" >
|   < MAP: "map" >
|   < NEW: "new" >
|   < NODE: "node" >
|   < NODES: "nodes" >
|   < NULL: "null" >
|   < NUM: "num" >
|   < OFF: "off" >
|   < ON: "on" >
|   < ONLY: "only" >
|   < OPEN: "open" >
|   < OPTION: "option" >
|   < OPTIONS: "options" >
|   < PARSE: "parse" >
|   < PARSER: "parser" >
|   < QUIT: "quit" >
|   < RANDOMSEED: "randomseed" >
|   < REDIRECT: "redirect" >
|   < REMOVE: "remove" >
|   < RESET: "reset" >
|   < RESETVISITFLAG: "resetvisitflag" >
|   < SAVE: "save" >
|   < SELECT: "select" >
|   < SET: "set" >
|   < SETVISITED: "setvisited" >
|   < SIZE: "size" >
|   < SHAPE: "shape" >
|   < SHORTINFOTAG: "shortinfotag" >
|   < SHOW: "show" >
|   < SILENCE: "silence" >
|   < STRICT: "strict" >
|   < SUB: "sub" >
|   < SUPER: "super" >
|   < SYNC: "sync" >
|   < TEXTCOLOR: "textcolor" >
|   < TRUE: "true" >
|   < TYPE: "type" >
|   < TYPES: "types" >
|   < VALIDATE: "validate" >
|   < VAR: "var" >
|   < WITH: "with" >
|   < XGRS: "xgrs" >
}

TOKEN: {
	< NUMBER: ("-")? (["0"-"9"])+ >
|
	< HEXNUMBER: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ >
}

TOKEN: {
	< NUMFLOAT:
			("-")? (["0"-"9"])+ ("." (["0"-"9"])+)? (<EXPONENT>)? ["f", "F"]
		|	("-")? "." (["0"-"9"])+ (<EXPONENT>)? ["f", "F"]
	>
|
	< NUMDOUBLE:
			("-")? (["0"-"9"])+ "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
		|	("-")? "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
		|	("-")? (["0"-"9"])+ <EXPONENT> (["d", "D"])?
		|	("-")? (["0"-"9"])+ ["d", "D"]
	>
|
	< #EXPONENT: ["e", "E"] (["+", "-"])? (["0"-"9"])+ >
}

TOKEN: {
	< DOUBLEQUOTEDTEXT : "\"" (~["\"", "\n", "\r"])* "\"" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|	< SINGLEQUOTEDTEXT : "\'" (~["\'", "\n", "\r"])* "\'" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|	< WORD : ~["\'", "\"", "0"-"9", "=", ":", ";", ".", ",", "+", "-", "&", "%", "?", "$", "|", "<", ">", "(", ")", "{", "}", "[", "]", "*", "!", "#", " ", "@", "\n", "\r"]
	     (~["\'", "\"", "=", ":", ";", ".", ",", "+", "-", "&", "%", "?", "$", "|", "<", ">", "(", ")", "{", "}", "[", "]", "*", "!", "#", " ", "@", "\n", "\r"])*	>
}

SPECIAL_TOKEN: {
	< SINGLE_LINE_COMMENT: "#" (~["\n"])* >
}

<WithinFilename> SKIP: {
	" " |
	"\t" |
	"\r"
}

<WithinFilename> TOKEN: {
	< DOUBLEQUOTEDFILENAME: "\"" (~["\"", "\n", "\r"])* "\"" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|	< SINGLEQUOTEDFILENAME: "\'" (~["\'", "\n", "\r"])* "\'" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|	< FILENAME: ~["\'", "\"", "=", ";", "$", "|", "*", " ", "?", "\n", "\r"]
	     (~["\'", "\"", "=", ";", "$", "|", "*", " ", "?", "\n", "\r"])* > : DEFAULT
|	< NLINFILENAME: "\n" > : DEFAULT
|	< ERRORFILENAME: ~[] > : DEFAULT
}

<WithinCommand> TOKEN: {
    < COMMANDLINE: ("\\\r\n" | "\\\n" | "\\\r" | ~["\n","\r","#"])* ("\n" | "\r" | "\r\n")? > : DEFAULT
}

<WithinAnyString> SKIP: {
	" " |
	"\t" |
	"\r"
}

<WithinAnyString> TOKEN: {
    < DOUBLEQUOTEDANYSTRING: "\"" (~["\"", "\n", "\r"])* "\"" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|   < SINGLEQUOTEDANYSTRING: "\'" (~["\'", "\n", "\r"])* "\'" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|   < ANYSTRING: (~[" ", "\t", "\n", "\r", "#", "\"", "\'", "="])+ > : DEFAULT
|   < ERRORANYSTRING: ~[] > : DEFAULT
}


<WithinAnyStrings> SKIP: {
	" " |
	"\t" |
	"\r" |
	"\\\r\n" |
	"\\\n" |
	"\\\r"
}

<WithinAnyStrings> TOKEN: {
    < DOUBLEQUOTEDANYSTRINGS: "\"" (~["\"", "\n", "\r"])* "\"" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|   < SINGLEQUOTEDANYSTRINGS: "\'" (~["\'", "\n", "\r"])* "\'" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|   < ANYSTRINGS: (~[" ", "\t", "\n", "\r", "#", "\"", "\'", "="])+ >
|   < NLINANYSTRINGS: "\n" > : DEFAULT
|   < ERRORANYSTRINGS: ~[] > : DEFAULT
}

String AnyString():
{
    Token tok;
}
{
    {token_source.SwitchTo(WithinAnyString);}
    (tok=<DOUBLEQUOTEDANYSTRING> | tok=<SINGLEQUOTEDANYSTRING> | tok=<ANYSTRING>)
    {
        return tok.image;
    }
}

String Word():
{
    Token tok;
}
{
    tok=<WORD>
    {
        return tok.image;
    }
}

String Text():
{
	Token tok;
}
{
	(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD>)
	{
		return tok.image;		
	}
}

String QuotedText():
{
	Token tok;
}
{
	(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT>)
	{
		return tok.image;		
	}
}

String TextOrNumber():
{
	Token tok;
}
{
	(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD> | tok=<NUMBER> | tok=<HEXNUMBER>)
	{
		return tok.image;		
	}
}

String AttributeValue():
{
	Token tok;
	String enumName, enumValue;
}
{
	(
		LOOKAHEAD(2) enumName=Word() "::" enumValue=Word()
		{
			return enumName + "::" + enumValue;
		}
	|
		(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD> | tok=<NUMBER> | tok=<HEXNUMBER> | tok=<NUMFLOAT> | tok=<NUMDOUBLE> | tok=<TRUE> | tok=<FALSE> | tok=<NULL>)
		{
			return tok.image;
		}
	)
}

int Number():
{
	Token t;
}
{
	( 
		t=<NUMBER> 
		{
			return Convert.ToInt32(t.image);
		}
	|
		t=<HEXNUMBER>
		{
            return Int32.Parse(t.image.Substring("0x".Length), System.Globalization.NumberStyles.HexNumber);
		}
	)
}

float FloatNumber():
{
	Token t;
	float val;
}
{
	t=<NUMFLOAT>
	{
		// Remove 'F' from the end of the image to parse it
		if(!float.TryParse(t.image.Substring(0, t.image.Length - 1), System.Globalization.NumberStyles.Float,
				System.Globalization.CultureInfo.InvariantCulture, out val))
			throw new ParseException("float expected but found: \"" + t + "\" (" + t.kind + ")");
		return val;
	}
}

double DoubleNumber():
{
	Token t;
	String img;
	double val;
}
{
	t=<NUMDOUBLE>
	{
		// Remove optional 'D' from the end of the image to parse it if necessary
		if(t.image[t.image.Length - 1] == 'd' || t.image[t.image.Length - 1] == 'D')
			img = t.image.Substring(0, t.image.Length - 1);
		else
			img = t.image;
		if(!double.TryParse(img, System.Globalization.NumberStyles.Float,
				System.Globalization.CultureInfo.InvariantCulture, out val))
			throw new ParseException("double expected but found: \"" + t + "\" (" + t.kind + ")");
		return val;
	}
}

object NumberOrVar():
{
	Token t;
	object val;
	String str;
}
{
	t=<NUMBER>
	{
		return Convert.ToInt32(t.image);
	}
|
	str=Word() { val = impl.GetVarValue(str); return val; }
}

bool Bool():
{ }
{
	"true" { return true; }
|
	"false" { return false; }
}

object BoolOrVar():
{
	object val;
	String str;
}
{
	"true" { return true; }
|
	"false" { return false; }
|
	str=Word() { val = impl.GetVarValue(str); return val; }
}

String Filename():
{
    Token tok;
}
{
    {token_source.SwitchTo(WithinFilename);}
    (tok=<DOUBLEQUOTEDFILENAME> | tok=<SINGLEQUOTEDFILENAME> | tok=<FILENAME>)
	{
		return tok.image.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
	}
}

String FilenameOptionalAtEndOfLine():
{
    Token tok;
}
{
    {token_source.SwitchTo(WithinFilename);}
    (
		(tok=<DOUBLEQUOTEDFILENAME> | tok=<SINGLEQUOTEDFILENAME> | tok=<FILENAME>) LineEnd()
		{
			return tok.image.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}
	|
		(tok=<NLINFILENAME> | tok=<EOF>)
		{
			return null;
		}
	)
}

String FilenameParameterOrEndOfLine():
{
    Token tok;
}
{
    {token_source.SwitchTo(WithinFilename);}
    (
		(tok=<DOUBLEQUOTEDFILENAME> | tok=<SINGLEQUOTEDFILENAME> | tok=<FILENAME>)
		{
			return tok.image.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
		}
	|
		(tok=<NLINFILENAME> | tok=<EOF>)
		{
			return null;
		}
	)
}

List<String> FilenameParameterList():
{
	List<String> list = new List<String>();
	String cur;
}
{
	{
		while((cur = FilenameParameterOrEndOfLine()) != null)
			list.Add(cur);
		return list;
	}	
}

String CommandLine():
{
    Token tok;
    String str;
}
{
    {token_source.SwitchTo(WithinCommand);}
    tok=<COMMANDLINE>
    {
        str = tok.image.Replace("\\\r\n", "").Replace("\\\n", "").Replace("\n", "");
        return str;
    }
}

IGraphElement GraphElement():
{
	IGraphElement elem;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { elem = impl.GetElemByName(str); }
	|
		str=Word() { elem = impl.GetElemByVar(str); }
	)
	{ return elem; }
}

object GraphElementOrVar():
{
	object val;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { val = impl.GetElemByName(str); }
	|
		str=Word() { val = impl.GetVarValue(str); }
	)
	{ return val; }
}


object GraphElementOrVarOrNull():
{
	object val;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { val = impl.GetElemByName(str); }
    |
        "null" { val = null; }
	|
		str=Word() { val = impl.GetVarValue(str); }
	)
	{ return val; }
}

INode Node():
{
	INode node;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { node = impl.GetNodeByName(str); }
	|
		str=Text() { node = impl.GetNodeByVar(str); }
	)
	{ return node; }
}

IEdge Edge():
{
	IEdge edge;
	String str;
}
{
	(
		"@" "(" str=Text() ")" { edge = impl.GetEdgeByName(str); }
	|
		str=Text() { edge = impl.GetEdgeByVar(str); }
	)
	{ return edge; }
}

NodeType NodeType():
{
	String str;
}
{
	str=Text() { return impl.GetNodeType(str); }
}

EdgeType EdgeType():
{
	String str;
}
{
	str=Text() { return impl.GetEdgeType(str); }
}

ShellGraph Graph():
{
	String str;
	int index;
}
{
    (
        index=Number() { return impl.GetShellGraph(index); }
    |
	    str=Text() { return impl.GetShellGraph(str); }
	)
}

// TODO: remove this in 2.7 and the set/map functions using it
object Expr():
{
	Object obj;
	String str;
}
{
    (
		"null" { obj = null; }
	|
        obj=GraphElementOrVar()
        ( "." str=AnyString() { obj = impl.GetElementAttribute((IGraphElement) obj, str); } )?
    |
        obj=QuotedText()
    |
        obj=Number()
    |
		obj=Bool()
    )
    { return obj; }
}

object SimpleConstant():
{
	object constant = null;
	int number;
	string type, value;
}
{
	(
		number=Number() { constant = (int) number; }
	|
		constant=FloatNumber()
	|
		constant=DoubleNumber()
	|
		constant=QuotedText()
	|
		<TRUE> { constant = true; }
	|
		<FALSE> { constant = false; }
	|
		<NULL> { constant = null; }
	| 
		type=Word() "::" value=Word() 
		{ 
			foreach(EnumAttributeType attrType in impl.CurrentGraph.Model.EnumAttributeTypes)
			{
				if(attrType.Name == type)
				{
					Type enumType = attrType.EnumType;
					constant = Enum.Parse(enumType, value);
					break;
				}
			}
			if(constant==null) 
				throw new ParseException("Invalid constant \""+type+"::"+value+"\"!");
		}
	)
	{
		return constant;
	}
}

object Constant():
{
	object constant = null;
	object src = null, dst = null;
	string typeName, typeNameDst;
	Type srcType, dstType;
}
{
	(
		constant=SimpleConstant()
    |
		"set" "<" typeName=Word() ">"
		{
			srcType = DictionaryHelper.GetTypeFromNameForDictionary(typeName, impl.CurrentGraph.Model);
			dstType = typeof(de.unika.ipd.grGen.libGr.SetValueType);
			if(srcType!=null)
				constant = DictionaryHelper.NewDictionary(srcType, dstType);
			if(constant==null) 
				throw new ParseException("Invalid constant \"set<"+typeName+">\"!");
		}
		"{" 
			( src=SimpleConstant() { ((IDictionary)constant).Add(src, null); } )? 
				( "," src=SimpleConstant() { ((IDictionary)constant).Add(src, null); })*
		"}"
	|
		"map" "<" typeName=Word() "," typeNameDst=Word() ">"
		{
			srcType = DictionaryHelper.GetTypeFromNameForDictionary(typeName, impl.CurrentGraph.Model);
			dstType = DictionaryHelper.GetTypeFromNameForDictionary(typeNameDst, impl.CurrentGraph.Model);
			if(srcType!=null && dstType!=null) 
				constant = DictionaryHelper.NewDictionary(srcType, dstType);
			if(constant==null) 
				throw new ParseException("Invalid constant \"map<"+typeName+","+typeNameDst+">\"!");
		}
		"{" 
			( src=SimpleConstant() "->" dst=SimpleConstant() { ((IDictionary)constant).Add(src, dst); } )?
				( "," src=SimpleConstant() "->" dst=SimpleConstant() { ((IDictionary)constant).Add(src, dst); } )*
		"}"
	)
	{
		return constant;
	}
}

void LineEnd():
{}
{
	{ if(Quit) return; }
	(<NL> | <DOUBLESEMICOLON> | <EOF> { Eof = true; })
}

bool ParseShellCommand():
{}
{
    { noError = true; }
	try
	{
		{ if(ShowPrompt) Console.Write("> "); }
		
		(
			<NL>
			| <DOUBLESEMICOLON>
			| <EOF> { Eof = true; }
			| ShellCommand()
		)
	}
	catch(ParseException ex)
	{
		errorSkip(ex);
		return false;
	}
	{ return noError; }
}

void ShellCommand():
{
	String str1, str2 = null, str3;
	IGraphElement elem;
	object obj, obj2;
	INode node1, node2;
	IEdge edge1, edge2;
	ShellGraph shellGraph = null;
	Sequence seq;
	bool shellGraphSpecified = false, boolVal;
	bool strict = false, exitOnFailure = false, validated = false;
	int num;
	List<String> parameters;
}
{
    "!" str1=CommandLine()
    {
        impl.ExecuteCommandLine(str1);
    }
|    
	"help" parameters=SpacedParametersAndLineEnd()
	{
		impl.Help(parameters);
	}
|
	("quit" | "exit") LineEnd()
	{
		impl.Quit();
		Quit = true;
	}
|
    "include" str1=Filename() LineEnd()
    {
        noError = impl.Include(this, str1);
    }
|
	"new" NewCommand()
|
	"open" "graph" str1=Filename() str2=Text() LineEnd()
	{
		noError = impl.OpenGraph(str1, str2);
	}
|
	"select" SelectCommand()
|
	"silence"
	(
		"on" { impl.Silence = true; }
	|
		"off" { impl.Silence = false; }
	)
|
	"delete" DeleteCommand()
|
	"clear" "graph" (shellGraph=Graph() { shellGraphSpecified = true; })? LineEnd()
	{
	    if(shellGraphSpecified && shellGraph == null) noError = false;
	    else impl.ClearGraph(shellGraph, shellGraphSpecified);
	}
|
	"show" ShowCommand()
|
	"node" "type" node1=Node() "is" node2=Node() LineEnd()
	{
		impl.NodeTypeIsA(node1, node2);
	}
|
	"edge" "type" edge1=Edge() "is" edge2=Edge() LineEnd()
	{
		impl.EdgeTypeIsA(edge1, edge2);
	}
|
    "debug" DebugCommand()
|
	"grs" str1=CommandLine()
	{
        Console.WriteLine("The old grs are not supported any longer. Please use the extended graph rewrite sequences xgrs.");
        noError = false;
	}
|
    "xgrs" str1=CommandLine()
    {
        try
        {
            seq = SequenceParser.ParseSequence(str1, impl.CurrentActions);
            impl.ApplyRewriteSequence(seq, false);
            noError = !impl.OperationCancelled;
        }
        catch(SequenceParserException ex)
        {
            impl.HandleSequenceParserException(ex);
            noError = false;
        }
        catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
        {
            Console.WriteLine("Unable to execute xgrs: " + ex.Message);
            noError = false;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Unable to execute xgrs: " + ex);
            noError = false;
        }
    }
|
	"validate" ("exitonfailure" {exitOnFailure = true;})?
	(
	    "xgrs" str1=CommandLine()
	    {
            try
            {
                seq = SequenceParser.ParseSequence(str1, impl.CurrentActions);
    	        validated = impl.ValidateWithSequence(seq);
                noError = !impl.OperationCancelled;
            }
            catch(SequenceParserException ex)
            {
                impl.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
                Console.WriteLine("Unable to execute xgrs: " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to execute xgrs: " + ex);
                noError = false;
            }
			if((!validated || !noError) && exitOnFailure)
			{
				throw new Exception("validate failed");
			}
        }
    |
	    ( "strict" { strict = true; } )? LineEnd()
	    {
		    validated = impl.Validate(strict);
			if(!validated && exitOnFailure)
			{
				throw new Exception("validate failed");
			}
	    }
	)
|
	"dump" DumpCommand()
|
	"save" "graph" str1=Filename() LineEnd()
	{
		impl.SaveGraph(str1);
	}
|
	"export" parameters=FilenameParameterList()
	{
		noError = impl.Export(parameters);
	}
|
	"import" parameters=FilenameParameterList()
	{
		noError = impl.Import(parameters);
	}
|
	"echo" str1=Text() LineEnd()
	{
        Console.WriteLine(Regex.Unescape(str1));
	}
|
	"custom" CustomCommand()
|
    "redirect" "emit" str1=Filename() LineEnd()
    {
        noError = impl.RedirectEmit(str1);
    }
|
	"sync" "io" LineEnd()
	{
		impl.SyncIO();
	}
|
	"parse"
	(
	    "file" str1=Filename() LineEnd()
	    {
		    noError = impl.ParseFile(str1);
	    }
    |
	    str1=Text() LineEnd()
	    {
		    noError = impl.ParseString(str1);
	    }
	)
|
	"randomseed"
	(
		num=Number()
		{
			impl.SetRandomSeed(num);
		}
	|
		str1=Word()
		{
			if(str1 != "time")
			{
				Console.WriteLine("The new seed as integer or the word \"time\" for setting the current time as seed expected!");
				noError = false;
			}
			else impl.SetRandomSeed(Environment.TickCount);
		}		
	)	
|
	"isvisited" elem=GraphElement() obj=NumberOrVar() LineEnd()
	{
		noError = impl.IsVisited(elem, obj, true, out boolVal);
	}
|
	"setvisited" elem=GraphElement() obj=NumberOrVar() obj2=BoolOrVar() LineEnd()
	{
		noError = impl.SetVisited(elem, obj, obj2);
	}
|
	"freevisitflag" obj=NumberOrVar() LineEnd()
	{
		noError = impl.FreeVisitFlag(obj);
	}
|
	"resetvisitflag" obj=NumberOrVar() LineEnd()
	{
		noError = impl.ResetVisitFlag(obj);
	}
|
	"map" MapCommand()
|
	"set" SetCommand()
|
    // TODO: Introduce prefix for the following commands to allow useful error handling!
    
    try
    {
	    LOOKAHEAD(2) elem=GraphElement() "." str1=AnyString()
	    (
	        LineEnd()
	        {
	            impl.ShowElementAttribute(elem, str1);
	        }
	    |
	        "=" str2=AttributeValue() LineEnd()
	        {
		        impl.SetElementAttribute(elem, str1, str2);
	        }
	    )
    |
        str1=Word() "="
        (
			"allocvisitflag" LineEnd()
			{
				obj = impl.AllocVisitFlag();
				if((int) obj < 0) noError = false;
			}
		|
			"isvisited" elem=GraphElement() obj=NumberOrVar() LineEnd()
			{
				noError = impl.IsVisited(elem, obj, false, out boolVal);
				obj = boolVal;
			}
		|
			"new"
			(
				"map" str2=Word() str3=Word()
				{
					Console.WriteLine(str1+"="+"new map "+str2+" "+str3+" is deprecated, use xgrs "+str1+" = map<"+str2+","+str3+"> instead");
					obj = impl.MapNew(str2, str3);
					if(obj == null) noError = false;
				}
			|
				"set" str2=Word() 
				{
					Console.WriteLine(str1+"="+"new set "+str2+" is deprecated, use xgrs "+str1+" = set<"+str2+"> instead");
					obj = impl.SetNew(str2);
					if(obj == null) noError = false;
				}
			) LineEnd()
		|
			"askfor" 
			(
				str2=Word()
				{
					obj = impl.Askfor(str2);
					if(obj == null) noError = false;
				}
			|
				"set" "<" str2=Word() ">" 
				{
					obj = impl.Askfor("set<"+str2+">");
					if(obj == null) noError = false;
				}
			|
				"map" "<" str2=Word() "," str3=Word() ">"
				{
					obj = impl.Askfor("map<"+str2+","+str3+">");
					if(obj == null) noError = false;
				}
			) LineEnd()
		|
		    LOOKAHEAD(2) obj=GraphElementOrVar()
			( "." str2=AnyString() { obj = impl.GetElementAttribute((IGraphElement) obj, str2); } )? LineEnd()
		|
			obj=Constant() LineEnd()
		)
        {
			if(noError) impl.SetVariable(str1, obj);
        }
    }
    catch(ParseException ex)
    {
        throw new ParseException("Unknown command. Enter \"help\" to get a list of commands.");
    }
}

///////////////////
// "New" command //
///////////////////

void NewCommand():
{
	String modelFilename, graphName = "DefaultGraph";
	INode srcNode, tgtNode;
	ElementDef elemDef;
	bool directed;
}
{
	try
	{
		"graph" modelFilename=Filename() (graphName=Text())? LineEnd()
		{
			noError = impl.NewGraph(modelFilename, graphName);
		}
	|
		LOOKAHEAD(2)
		srcNode=Node() "-" elemDef=ElementDefinition() ( "->" { directed = true; } | "-" { directed = false; } ) tgtNode=Node() LineEnd()
		{
			noError = impl.NewEdge(elemDef, srcNode, tgtNode, directed) != null;
		}
	|
		elemDef=ElementDefinition() LineEnd()
		{
			noError = impl.NewNode(elemDef) != null;
		}
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpNew(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

ElementDef ElementDefinition():
{
	String varName = null, typeName = null, elemName = null;
	ArrayList attributes = new ArrayList();
}
{
	(varName=Text())?
	(
		":" typeName=Text()
		(
			"("
			(
				"$" "=" elemName=Text() ("," Attributes(attributes))?
			|
				Attributes(attributes)
			)?
			")"
		)?	
	)?
	{
		return new ElementDef(elemName, varName, typeName, attributes);
	}
}

void Attributes(ArrayList attributes):
{}
{
	SingleAttribute(attributes) (LOOKAHEAD(2) "," SingleAttribute(attributes) )*
}

void SingleAttribute(ArrayList attributes):
{
	String attribName, value, valueTgt;
	Token type, typeTgt;
	Param param;
}
{
	attribName=Text() "=" 
		(value=AttributeValue()
			{
				attributes.Add(new Param(attribName, value));
			}
		| <SET> <LANGLE> type=<WORD> <RANGLE> 
			{ param = new Param(attribName, "set", type.image); }
			<LBRACE> ( value=AttributeValue() { param.Values.Add(value); } )? 
			    (<COMMA> value=AttributeValue() { param.Values.Add(value); })* <RBRACE>
			{ attributes.Add(param); }
		| <MAP> <LANGLE> type=<WORD> <COMMA> typeTgt=<WORD> <RANGLE>
			{ param = new Param(attribName, "map", type.image, typeTgt.image); }
			<LBRACE> ( value=AttributeValue() { param.Values.Add(value); } <ARROW> valueTgt=AttributeValue() { param.TgtValues.Add(valueTgt); } )?
				( <COMMA> value=AttributeValue() { param.Values.Add(value); } <ARROW> valueTgt=AttributeValue() { param.TgtValues.Add(valueTgt); } )* <RBRACE>
			{ attributes.Add(param); }
		)
}

//////////////////////
// "select" command //
//////////////////////

void SelectCommand():
{
	String str, mainname;
	ArrayList parameters = new ArrayList();
	ShellGraph shellGraph;
}
{
	try
	{
		"backend" str=Filename() (":" Parameters(parameters))? LineEnd()
		{
			noError = impl.SelectBackend(str, parameters);
		}
	|
		"graph" shellGraph=Graph() LineEnd()
		{
			if(shellGraph == null) noError = false;
			else impl.SelectGraph(shellGraph);
		}	
	|
		"actions" str=Filename() LineEnd()
		{
			noError = impl.SelectActions(str);
		}
	|
		"parser" str=Filename() mainname=Text() LineEnd()
		{
			noError = impl.SelectParser(str, mainname);
		}
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpSelect(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

void Parameters(ArrayList parameters):
{
	String str;
}
{
	str=Text() { parameters.Add(str); } ("," str=Text() { parameters.Add(str); })*
}

//////////////////////
// "delete" command //
//////////////////////

void DeleteCommand():
{
	INode node;
	IEdge edge;
	ShellGraph shellGraph = null;
	bool shellGraphSpecified = false;
}
{
	try
	{
		"node" node=Node() LineEnd()
		{
			noError = impl.Remove(node);
		}
	|
		"edge" edge=Edge() LineEnd()
		{
			noError = impl.Remove(edge);
		}
	|
		"graph" (shellGraph=Graph() { shellGraphSpecified = true; })? LineEnd()
		{
			noError = impl.DestroyGraph(shellGraph, shellGraphSpecified);
		}
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpDelete(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

////////////////////
// "show" command //
////////////////////

void ShowCommand():
{
	String str;
	String args = null;
	NodeType nodeType = null;
	EdgeType edgeType = null;
	IGraphElement elem = null;
	bool typeProvided = false;
	bool only = false;
}
{
	try
	{
		"nodes" (("only" { only=true; })? nodeType=NodeType() { typeProvided=true; })? LineEnd()
		{
			if(!typeProvided || nodeType != null)
				impl.ShowNodes(nodeType, only);
		}
	|
		"edges" (("only" { only=true; })? edgeType=EdgeType() { typeProvided=true; })? LineEnd()
		{
			if(!typeProvided || edgeType != null)
				impl.ShowEdges(edgeType, only);
		}
	|
		LOOKAHEAD(2)
		"num" "nodes" (("only" { only=true; })? nodeType=NodeType() { typeProvided=true; })? LineEnd()
		{
			if(!typeProvided || nodeType != null)
				impl.ShowNumNodes(nodeType, only);
		}
	|
		"num" "edges" (("only" { only=true; })? edgeType=EdgeType() { typeProvided=true; })? LineEnd()
		{
			if(!typeProvided || edgeType != null)
				impl.ShowNumEdges(edgeType, only);
		}
	|
		"node" ShowNode()
	|
		"edge" ShowEdge()
	|
		"var" ShowVar()
	|
		"graph" str=Filename() (args=Text())? LineEnd()
		{
			impl.ShowGraphWith(str, args);
		}
	|
		"graphs" LineEnd()
		{
			impl.ShowGraphs();
		}
	|
		"actions" LineEnd()
		{
			impl.ShowActions();
		}
	|
		"backend" LineEnd()
		{
			impl.ShowBackend();
		}
	|
		elem=GraphElement() "." str=AnyString() LineEnd()
        {
            impl.ShowElementAttribute(elem, str);
        }
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpShow(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

void ShowNode():
{
	bool only = false;
	INode node;
    NodeType nodeType = null;
}
{
	"types" LineEnd()
	{
		impl.ShowNodeTypes();
	}
|
	"super" "types" nodeType=NodeType() LineEnd()
	{
		impl.ShowSuperTypes(nodeType, true);
	}
|
	"sub" "types" nodeType=NodeType() LineEnd()
	{
		impl.ShowSubTypes(nodeType, true);
	}
|
	"attributes" (("only" { only=true; })? nodeType=NodeType())? LineEnd()
	{
		impl.ShowAvailableNodeAttributes(only, nodeType);
	}
|
	node=Node() LineEnd()
	{
		impl.ShowElementAttributes(node);
	}
}

void ShowEdge():
{
	bool only = false;
	IEdge edge;
	EdgeType edgeType = null;
}
{
	"types" LineEnd()
	{
		impl.ShowEdgeTypes();
	}
|
	"super" "types" edgeType=EdgeType() LineEnd()
	{
		impl.ShowSuperTypes(edgeType, false);
	}
|
	"sub" "types" edgeType=EdgeType() LineEnd()
	{
		impl.ShowSubTypes(edgeType, false);
	}
|
	"attributes" (("only" { only = true; })? edgeType=EdgeType())? LineEnd()
	{
		impl.ShowAvailableEdgeAttributes(only, edgeType);
	}
|
	edge=Edge() LineEnd()
	{
		impl.ShowElementAttributes(edge);
	}
}

void ShowVar():
{
	String str;
}
{
	str=Word() LineEnd()
	{
		impl.ShowVar(str);
	}
}

//////////////////////
// "debug" command" //
//////////////////////

void DebugCommand():
{
    Sequence seq;
    String str = null, str2;
    RuleInvocationParameterBindings paramBindings;
}
{
	try
	{
		"apply" paramBindings=Rule() LineEnd()
		{
			Console.WriteLine("debug apply is deprecated, use <var> = askfor <type> instead");
			if(paramBindings != null)
			{
				noError = impl.DebugApply(paramBindings);
			}
			else noError = false;
		}
	|
		"grs" str=CommandLine()
		{
			Console.WriteLine("The old grs are not supported any longer. Please use the extended graph rewrite sequences xgrs.");
			noError = false;
		}
	|
		"xgrs" str=CommandLine()
		{
			try
			{
				seq = SequenceParser.ParseSequence(str, impl.CurrentActions);
				impl.DebugRewriteSequence(seq);
				noError = !impl.OperationCancelled;
			}
			catch(SequenceParserException ex)
			{
				impl.HandleSequenceParserException(ex);
				noError = false;
			}
			catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
			{
				Console.WriteLine("Unable to execute xgrs: " + ex.Message);
				noError = false;
			}
			catch(Exception ex)
			{
				Console.WriteLine("Unable to execute xgrs: " + ex);
				noError = false;
			}
		}
	|
		"enable" LineEnd()
		{
			impl.SetDebugMode(true);
		}
	|
		"disable" LineEnd()
		{
			impl.SetDebugMode(false);
		}
	|
		"layout" LineEnd()
		{
			impl.DebugLayout();
		}
	|
		"set" "layout"
		(
			"option" str=Text() str2=AnyString() LineEnd()
			{
				impl.SetDebugLayoutOption(str, str2);
			}
		|
			(str=Text())? LineEnd()
			{
				impl.SetDebugLayout(str);
			}
		)
	|
		"get" "layout" "options" LineEnd()
		{
			impl.GetDebugLayoutOptions();
		}
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpDebug(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}


///////////////////////
// debug apply stuff //
///////////////////////

RuleInvocationParameterBindings Rule():
{
	String str;
	IAction action;
	bool retSpecified = false;
	ArrayList paramVarNames = new ArrayList();
	ArrayList returnVarNames = new ArrayList();
}
{
	("(" Parameters(returnVarNames) ")" "=" { retSpecified = true; })? str=Text() ("(" RuleParameters(paramVarNames) ")")?
	{
		action = impl.GetAction(str, paramVarNames.Count, returnVarNames.Count, retSpecified);
		if(action == null)
		{
			valid = false;
			return null;
		}
		if(!retSpecified && action.RulePattern.Outputs.Length > 0)
		{
			returnVarNames = ArrayList.Repeat(null, action.RulePattern.Outputs.Length);
		}
		SequenceVariable[] paramVars = new SequenceVariable[paramVarNames.Count];
		for(int i=0; i<paramVarNames.Count; ++i) {
			String paramVarName = (String)paramVarNames[i];
			paramVars[i] = new SequenceVariable(paramVarName, "", "");
		}
		SequenceVariable[] returnVars = new SequenceVariable[returnVarNames.Count];
		for(int i=0; i<returnVarNames.Count; ++i) {
			String returnVarName = (String)returnVarNames[i];
			returnVars[i] = new SequenceVariable(returnVarName, "", "");
		}

		return new RuleInvocationParameterBindings(action, paramVars, new object[paramVars.Length], returnVars);
	}
}

void RuleParameters(ArrayList parameters):
{
	String str;
}
{
	(
		str=Text() { parameters.Add(str); }
	|
		"?" { parameters.Add("?"); }
	)
	(
		","
		(
			str=Text() { parameters.Add(str); }
		|
			"?" { parameters.Add("?"); }
		)
	)*
}


/////////////////////
// "dump" commands //
/////////////////////

void DumpCommand():
{
	String filename;
}
{
	try
	{
		"graph" filename=Filename() LineEnd()
		{
			impl.DumpGraph(filename);
		}
	|
		"set" DumpSet()
	|
		"add" DumpAdd()
	|
		"reset" LineEnd()
		{
			impl.DumpReset();
		}
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpDump(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

void DumpSet():
{
	NodeType nodeType;
	EdgeType edgeType;
	String colorName = null, shapeName = null, labelStr = null;
	bool labels = false, only = false;
}
{
	"node" ("only" { only=true; })? nodeType=NodeType()
	(
		"color" (colorName=Text())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeColor(nodeType, colorName, only);
		}
	|
		"bordercolor" (colorName=Text())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeBorderColor(nodeType, colorName, only);
		}
	|
		"shape" (shapeName=Text())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeShape(nodeType, shapeName, only);
		}
	|
		"textcolor" (colorName=Text())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeTextColor(nodeType, colorName, only);
		}
	|
		"labels" ("on" | "off" { labelStr = ""; } | labelStr=Text()) LineEnd()
		{
			noError = impl.SetDumpLabel(nodeType, labelStr, only);
		}
	)
|
	"edge" ("only" { only=true; })? edgeType=EdgeType()
	(
		"color" (colorName=Text())? LineEnd()
		{
			noError = impl.SetDumpEdgeTypeColor(edgeType, colorName, only);
		}
	| 
		"textcolor" (colorName=Text())? LineEnd()
		{
			noError = impl.SetDumpEdgeTypeTextColor(edgeType, colorName, only);
		}
	|
		"labels" ("on" | "off" { labelStr = ""; } | labelStr=Text()) LineEnd()
		{
			noError = impl.SetDumpLabel(edgeType, labelStr, only);
		}
	)
}

void DumpAdd():
{
	NodeType nodeType, adjNodeType = impl.CurrentGraph.Model.NodeModel.RootType;
	EdgeType edgeType = impl.CurrentGraph.Model.EdgeModel.RootType;
	String attrName, groupModeStr = "incoming";
	bool only = false, onlyEdge = false, onlyAdjNode = false, hidden = false;
	GroupMode groupMode;
}
{
	"node" ("only" { only=true; })? nodeType=NodeType()
	(
		"exclude" LineEnd()
		{
			noError = impl.AddDumpExcludeNodeType(nodeType, only);
		}
	|
		"group" 
		(
		    "by" ("hidden" { hidden = true; })? groupModeStr=Word()
		    (
	            ("only" { onlyEdge=true; })? edgeType=EdgeType()
	            (
	                "with" ("only" { onlyAdjNode=true; })? adjNodeType=NodeType()
	            )?
	        )?
		)? LineEnd()
		{
	        switch(groupModeStr)
	        {
	            case "no":       groupMode = GroupMode.None;               break;
	            case "incoming": groupMode = GroupMode.GroupIncomingNodes; break;
	            case "outgoing": groupMode = GroupMode.GroupOutgoingNodes; break;
	            case "any":      groupMode = GroupMode.GroupAllNodes;      break;
	            default:
	                Console.WriteLine("Group mode must be one of: no, incoming, outgoing, any");
	                noError = false;
	                return;
	        }
	        if(hidden)
	        {
	            if(groupMode == GroupMode.None)
	            {
	                Console.WriteLine("The 'hidden' modifier can not be used with the group mode 'no'!");
	                noError = false;
	                return;
	            }
	            groupMode |= GroupMode.Hidden;
	        }
            noError = impl.AddDumpGroupNodesBy(nodeType, only, edgeType, onlyEdge, adjNodeType, onlyAdjNode, groupMode);
	    }
	|
		"infotag" attrName=Text() LineEnd()
		{
		    noError = impl.AddDumpInfoTag(nodeType, attrName, only, false);
	    }
	|
		"shortinfotag" attrName=Text() LineEnd()
		{
		    noError = impl.AddDumpInfoTag(nodeType, attrName, only, true);
	    }
    )
|
	"edge" ("only" { only=true; })? edgeType=EdgeType()
	(
	    "exclude" LineEnd()
	    {
	        noError = impl.AddDumpExcludeEdgeType(edgeType, only);
	    }
	|
	    "infotag" attrName=Text() LineEnd()
	    {
            noError = impl.AddDumpInfoTag(edgeType, attrName, only, false);
        }
	|
	    "shortinfotag" attrName=Text() LineEnd()
	    {
            noError = impl.AddDumpInfoTag(edgeType, attrName, only, true);
        }
	)
}

////////////////////
// "map" commands //
////////////////////

void MapCommand():
{
	bool usedGraphElement = false;
	IGraphElement elem = null;
	String str;
	object keyExpr, valueExpr;
}
{
	try
	{
		(
			LOOKAHEAD(2) elem=GraphElement() "." str=AnyString() { usedGraphElement = true; }
		|
			str=Word()
		)
		(
			"add" keyExpr=Expr() valueExpr=Expr() LineEnd()
			{
				impl.MapAdd(usedGraphElement, elem, str, keyExpr, valueExpr);
			}
		|
			"remove" keyExpr=Expr() LineEnd()
			{
				impl.MapRemove(usedGraphElement, elem, str, keyExpr);
			}
		|
			"size" LineEnd()
			{
				impl.MapSize(usedGraphElement, elem, str);
			}
		)
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpMap(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

////////////////////
// "set" commands //
////////////////////

void SetCommand():
{
	bool usedGraphElement = false;
	IGraphElement elem = null;
	String str;
	object keyExpr;
}
{
	try
	{
		(
			LOOKAHEAD(2) elem=GraphElement() "." str=Text() { usedGraphElement = true; }
		|
			str=Word()
		)
		(
			"add" keyExpr=Expr() LineEnd()
			{
				impl.SetAdd(usedGraphElement, elem, str, keyExpr);
			}
		|
			"remove" keyExpr=Expr() LineEnd()
			{
				impl.SetRemove(usedGraphElement, elem, str, keyExpr);
			}
		|
			"size" LineEnd()
			{
				impl.SetSize(usedGraphElement, elem, str);
			}
		)
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpSet(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

///////////////////////
// "custom" commands //
///////////////////////

void CustomCommand():
{
    List<String> parameters;
}
{
	"graph" parameters=SpacedParametersAndLineEnd()
	{
		impl.CustomGraph(parameters);
	}
|
	"actions" parameters=SpacedParametersAndLineEnd()
	{
		impl.CustomActions(parameters);
	}
}

List<String> SpacedParametersAndLineEnd():
{
    Token tok;
    List<String> parameters = new List<String>();
}
{
    {token_source.SwitchTo(WithinAnyStrings);}
    (
        (tok=<ANYSTRINGS> | tok=<DOUBLEQUOTEDANYSTRINGS> | tok=<SINGLEQUOTEDANYSTRINGS>)
        { parameters.Add(tok.image); }
    )*
    (<NLINANYSTRINGS> | <EOF>)
    {
        return parameters;
    }
}

////////////////////
// Error recovery //
////////////////////

CSHARPCODE
void errorSkip(ParseException ex) {
	Console.WriteLine(ex.Message);
	Token t;
	do
	{
		t = GetNextToken();
	}
	while(t.kind != EOF && t.kind != NL && t.kind != NLINFILENAME);
}

CSHARPCODE
void errorSkipSilent() {
	Token t;
	do
	{
		t = GetNextToken();
	}
	while(t.kind != EOF && t.kind != NL && t.kind != NLINFILENAME);
}

TOKEN: {
	<ERROR: ~[]>
}