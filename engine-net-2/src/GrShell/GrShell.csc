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
|   < DOUBLECOLON: "::" >
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
|   < APPLY: "apply" >
|   < ARRAY: "array" >
|   < ASKFOR: "askfor" >
|   < ATTRIBUTES: "attributes" >
|   < BACKEND: "backend" >
|   < BORDERCOLOR: "bordercolor" >
|   < BY: "by" >
|   < CD: "cd" >
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
|   < EXEC: "exec" >
|   < EXIT: "exit" >
|   < EXITONFAILURE: "exitonfailure" >
|   < EXPORT: "export" >
|   < FALSE: "false" >
|   < FILE: "file" >
|   < FROM: "from" >
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
|   < KEEPDEBUG: "keepdebug" >
|   < LABELS: "labels" >
|   < LAYOUT: "layout" >
|   < LAZYNIC: "lazynic" >
|   < LS: "ls" >
|   < MAP: "map" >
|   < MODE: "mode" >
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
|   < PWD: "pwd" >
|   < QUIT: "quit" >
|   < RANDOMSEED: "randomseed" >
|   < RECORD: "record" >
|   < REDIRECT: "redirect" >
|   < REFERENCE: "reference" >
|   < REM: "rem" >
|   < REPLAY: "replay" >
|   < RESET: "reset" >
|   < RETYPE: "retype" >
|   < SAVE: "save" >
|   < SELECT: "select" >
|   < SET: "set" >
|   < SHAPE: "shape" >
|   < SHORTINFOTAG: "shortinfotag" >
|   < SHOW: "show" >
|   < SILENCE: "silence" >
|   < SPECIFIED: "specified" >
|   < START: "start" >
|   < STRICT: "strict" >
|   < STOP: "stop" >
|   < LINESTYLE: "linestyle" >
|   < SUB: "sub" >
|   < SUPER: "super" >
|   < SYNC: "sync" >
|   < TEXTCOLOR: "textcolor" >
|   < THICKNESS: "thickness" >
|   < TO: "to" >
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
|	< NUMBER_BYTE: ("-")? (["0"-"9"])+ ("y"|"Y") >
|	< NUMBER_SHORT: ("-")? (["0"-"9"])+ ("s"|"S") >
|	< NUMBER_LONG: ("-")? (["0"-"9"])+ ("l"|"L") >
|
	< HEXNUMBER: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ >
|	< HEXNUMBER_BYTE: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("y"|"Y") >
|	< HEXNUMBER_SHORT: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("s"|"S") >
|	< HEXNUMBER_LONG: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("l"|"L") >
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

// external shell command or graph rewrite sequence
<WithinCommand> TOKEN: {
    < COMMANDLINE: ("\\\r\n" | "\\\n" | "\\\r" | "\\#" | ~["\n","\r","#"])* ("\n" | "\r" | "\r\n")? > : DEFAULT
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
|   < ANYSTRING: (~[" ", "\t", "\n", "\r", "#", "\"", "\'", "=", "."])+ > : DEFAULT
|   < ERRORANYSTRING: ~[] > : DEFAULT
}

// this -- anystring, filename, ... -- this only works if triggered
// a) from another lexical state, another lexical rule (not possible for any but the simplest tasks) or
// b) from the parser BUT ONLY in the case no lookahead had to be applied
// if lookahead was needed and reached a token to be handled by a non-default-state,
// it was tokenized with the default rules, not with the rules of this state
// -> be very careful with this rules and the switches to them,
// ensure that every switch to such a state from the parser is not in reach of a lookahead decision to be made
// it would make a lot of sense to use this token at a lot of more places, to get type and attribute names not colliding with shell keywords,
// but unfortunately they are used to take parsing decisions via lookahead, so this lexer state won't be used
// sigh, maybe it's improper, but parser directed lexing is a must in the real world...
  // can't use this e.g. for type names cause in a lot of places lookahead touches them -> shell keyword typename -> use ""
  // quoted versions here only for consistency with other places, types, where quotes are allowed (needed to prevent keyword clashes), so user can blindly always use them
<WithinAttributeName> TOKEN: {
    < DOUBLEQUOTEDATTRIBUTENAME: "\"" (~["\"", "\n", "\r"])* "\"" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|   < SINGLEQUOTEDATTRIBUTENAME: "\'" (~["\'", "\n", "\r"])* "\'" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|   < ATTRIBUTENAME: (["0"-"9", "a"-"z", "A"-"Z", "_"])+ > : DEFAULT
|   < ERRORATTRIBUTENAME: ~[] > : DEFAULT
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
|   < ANYSTRINGS: (~[" ", "\t", "\n", "\r", "#", "\"", "\'", "=", "."])+ >
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

String AttributeName():
{
    Token tok;
}
{
    {token_source.SwitchTo(WithinAttributeName);}
    (tok=<DOUBLEQUOTEDATTRIBUTENAME> | tok=<SINGLEQUOTEDATTRIBUTENAME> | tok=<ATTRIBUTENAME>)
    {
        return tok.image;
    }
}

String WordOrText():
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

String Variable():
{
	Token tok;
}
{
	(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD> | "::" tok=<WORD>)
	{
		return tok.image;
	}
}

String AttributeValue():
{
	Token tok;
	String enumName, enumValue, elemName;
}
{
	(
		LOOKAHEAD(2) enumName=WordOrText() "::" enumValue=AttributeName()
		{
			return enumName + "::" + enumValue;
		}
	|
		"@" "(" elemName=WordOrText() ")"
		{
			return "@(" + elemName + ")";
		}
	|
		(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD>
		| tok=<NUMBER> | tok=<NUMBER_BYTE> | tok=<NUMBER_SHORT> | tok=<NUMBER_LONG> 
		| tok=<HEXNUMBER> | tok=<HEXNUMBER_BYTE> | tok=<HEXNUMBER_SHORT> | tok=<HEXNUMBER_LONG>
		| tok=<NUMFLOAT> | tok=<NUMDOUBLE> | tok=<TRUE> | tok=<FALSE> | tok=<NULL>)
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
	str=Variable() { val = impl.GetVarValue(str); return val; }
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
	str=Variable() { val = impl.GetVarValue(str); return val; }
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
		"@" "(" str=WordOrText() ")" { elem = impl.GetElemByName(str); }
	|
		str=Variable() { elem = impl.GetElemByVar(str); }
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
		"@" "(" str=WordOrText() ")" { val = impl.GetElemByName(str); }
	|
		str=Variable() { val = impl.GetVarValue(str); }
	)
	{ return val; }
}

object GraphElementOrUnquotedVar():
{
	object val;
	String str;
	Token tok;
}
{
	(
		"@" "(" str=WordOrText() ")" { val = impl.GetElemByName(str); }
	|
		tok=<WORD> { val = impl.GetVarValue(tok.image); }
	|
		"::" tok=<WORD> { val = impl.GetVarValue(tok.image); }
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
		"@" "(" str=WordOrText() ")" { val = impl.GetElemByName(str); }
    |
        "null" { val = null; }
	|
		str=Variable() { val = impl.GetVarValue(str); }
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
		"@" "(" str=WordOrText() ")" { node = impl.GetNodeByName(str); }
	|
		str=Variable() { node = impl.GetNodeByVar(str); }
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
		"@" "(" str=WordOrText() ")" { edge = impl.GetEdgeByName(str); }
	|
		str=Variable() { edge = impl.GetEdgeByVar(str); }
	)
	{ return edge; }
}

NodeType NodeType():
{
	String str;
}
{
	str=WordOrText() { return impl.GetNodeType(str); }
}

EdgeType EdgeType():
{
	String str;
}
{
	str=WordOrText() { return impl.GetEdgeType(str); }
}

ShellGraphProcessingEnvironment Graph():
{
	String str;
	int index;
}
{
    (
        index=Number() { return impl.GetShellGraph(index); }
    |
	    str=WordOrText() { return impl.GetShellGraph(str); }
	)
}

object SimpleConstant():
{
	object constant = null;
	Token tok;
	string type, value;
}
{
	(
	  (
		tok=<NUMBER> { constant = Convert.ToInt32(tok.image); }
		| tok=<NUMBER_BYTE> { constant = Convert.ToSByte(impl.RemoveTypeSuffix(tok.image)); }
		| tok=<NUMBER_SHORT> { constant = Convert.ToInt16(impl.RemoveTypeSuffix(tok.image)); }
		| tok=<NUMBER_LONG> { constant = Convert.ToInt64(impl.RemoveTypeSuffix(tok.image)); }
		| tok=<HEXNUMBER> { constant = Int32.Parse(tok.image.Substring("0x".Length), System.Globalization.NumberStyles.HexNumber); }
		| tok=<HEXNUMBER_BYTE> { constant = SByte.Parse(impl.RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber); }
		| tok=<HEXNUMBER_SHORT> { constant = Int16.Parse(impl.RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber); }
		| tok=<HEXNUMBER_LONG> { constant = Int64.Parse(impl.RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber); }
	  )
	|
		constant=FloatNumber()
	|
		constant=DoubleNumber()
	|
		LOOKAHEAD(2) constant=QuotedText()
	|
		<TRUE> { constant = true; }
	|
		<FALSE> { constant = false; }
	|
		<NULL> { constant = null; }
	|
		type=WordOrText() "::" value=AttributeName()
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
		"set" "<" typeName=WordOrText() ">"
		{
			srcType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeName, impl.CurrentGraph.Model);
			dstType = typeof(de.unika.ipd.grGen.libGr.SetValueType);
			if(srcType!=null)
				constant = DictionaryListHelper.NewDictionary(srcType, dstType);
			if(constant==null)
				throw new ParseException("Invalid constant \"set<"+typeName+">\"!");
		}
		"{"
			( src=SimpleConstant() { ((IDictionary)constant).Add(src, null); } )?
				( "," src=SimpleConstant() { ((IDictionary)constant).Add(src, null); })*
		"}"
	|
		"map" "<" typeName=WordOrText() "," typeNameDst=WordOrText() ">"
		{
			srcType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeName, impl.CurrentGraph.Model);
			dstType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeNameDst, impl.CurrentGraph.Model);
			if(srcType!=null && dstType!=null)
				constant = DictionaryListHelper.NewDictionary(srcType, dstType);
			if(constant==null)
				throw new ParseException("Invalid constant \"map<"+typeName+","+typeNameDst+">\"!");
		}
		"{"
			( src=SimpleConstant() "->" dst=SimpleConstant() { ((IDictionary)constant).Add(src, dst); } )?
				( "," src=SimpleConstant() "->" dst=SimpleConstant() { ((IDictionary)constant).Add(src, dst); } )*
		"}"
	|
		"array" "<" typeName=WordOrText() ">"
		{
			srcType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeName, impl.CurrentGraph.Model);
			if(srcType!=null)
				constant = DictionaryListHelper.NewList(srcType);
			if(constant==null)
				throw new ParseException("Invalid constant \"array<"+typeName+">\"!");
		}
		"["
			( src=SimpleConstant() { ((IList)constant).Add(src); } )?
				( "," src=SimpleConstant() { ((IList)constant).Add(src); })*
		"]"
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
	String str1, str2 = null, str3 = null;
	IGraphElement elem;
	object obj, obj2;
	INode node1, node2;
	IEdge edge1, edge2;
	ShellGraphProcessingEnvironment shellGraph = null;
	Sequence seq;
	SequenceDefinition seqDef;
	bool shellGraphSpecified = false, boolVal, boolVal2;
	bool strict = false, exitOnFailure = false, validated = false, onlySpecified = false;
	int num;
	List<String> parameters;
	Param param;
	Token tok;
}
{
    "!" str1=CommandLine()
    {
        impl.ExecuteCommandLine(str1);
    }
|
	"cd" str1=Filename() LineEnd()
	{
		noError = impl.ChangeDirectory(str1);
	}
|
	"ls" LineEnd()
	{
		noError = impl.ListDirectory();
	}
|
	"pwd" LineEnd()
	{
		noError = impl.PrintWorkingDirectory();
	}
|
	"askfor"
	{
		impl.Askfor(null);
	}
|
	"clear" "graph" (shellGraph=Graph() { shellGraphSpecified = true; })? LineEnd()
	{
	    if(shellGraphSpecified && shellGraph == null) noError = false;
	    else impl.ClearGraph(shellGraph, shellGraphSpecified);
	}
|
	"custom" CustomCommand()
|
    "debug" DebugCommand()
|
	"delete" DeleteCommand()
|
	"dump" DumpCommand()
|
	"echo" str1=QuotedText() LineEnd()
	{
        Console.WriteLine(Regex.Unescape(str1));
	}
|
	"edge" "type" edge1=Edge() "is" edge2=Edge() LineEnd()
	{
		impl.EdgeTypeIsA(edge1, edge2);
	}
|
	"export" parameters=FilenameParameterList()
	{
		noError = impl.Export(parameters);
	}
|
	"grs" str1=CommandLine()
	{
        Console.WriteLine("The old grs are not supported any longer. Please use the extended graph rewrite sequences exec(/xgrs).");
        noError = false;
	}
|
	"help" parameters=SpacedParametersAndLineEnd()
	{
		impl.Help(parameters);
	}
|
	"import" parameters=FilenameParameterList()
	{
		noError = impl.Import(parameters);
	}
|
    "include" str1=Filename() LineEnd()
    {
        noError = impl.Include(this, str1, null, null);
    }
|
	"new" NewCommand()
|
	"node" "type" node1=Node() "is" node2=Node() LineEnd()
	{
		impl.NodeTypeIsA(node1, node2);
	}
|
	"parse"
	(
	    "file" str1=Filename() LineEnd()
	    {
		    noError = impl.ParseFile(str1);
	    }
    |
	    str1=WordOrText() LineEnd()
	    {
		    noError = impl.ParseString(str1);
	    }
	)
|
	("quit" | "exit") LineEnd()
	{
		impl.Quit();
		Quit = true;
	}
|
	"randomseed"
	(
		num=Number()
		{
			impl.SetRandomSeed(num);
		}
	|
		str1=WordOrText()
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
	"record" str1=Filename() { boolVal=false; boolVal2=false; } ("start" { boolVal=true; boolVal2=true; } | "stop" { boolVal=true; boolVal2=false; })? LineEnd()
	{
		noError = impl.Record(str1, boolVal, boolVal2);
	}
|
    "redirect" RedirectCommand()
|
	"replay" str1=Filename() ("from" str2=QuotedText())? ("to" str3=QuotedText())? LineEnd()
	{
		noError = impl.Replay(str1, this, str2, str3);
	}
|
    "retype" RetypeCommand()
|
	"save" "graph" str1=Filename() LineEnd()
	{
		impl.SaveGraph(str1);
	}
|
	"select" SelectCommand()
|
	"show" ShowCommand()
|
	"silence"
	(
		"on" { impl.Silence = true; }
	|
		"off" { impl.Silence = false; }
	)
|
	"sync" "io" LineEnd()
	{
		impl.SyncIO();
	}
|
	tok="validate" ("exitonfailure" {exitOnFailure = true;})?
	(
	    ("xgrs" | "exec") str1=CommandLine()
	    {
            try
            {
				List<String> warnings = new List<String>();
                seq = SequenceParser.ParseSequence(str1, impl.CurrentActions, warnings);
                foreach(string warning in warnings)
                {
					Console.WriteLine("The validate sequence at line " + tok.beginLine + " reported back: " + warning);
                }
    	        validated = impl.ValidateWithSequence(seq);
                noError = !impl.OperationCancelled;
            }
            catch(SequenceParserException ex)
            {
	            Console.WriteLine("Unable to parse validate sequence at line " + tok.beginLine);
                impl.HandleSequenceParserException(ex);
                noError = false;
            }
            catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
            {
				Console.WriteLine("Unable to execute validate sequence at line " + tok.beginLine + ": " + ex.Message);
                noError = false;
            }
            catch(Exception ex)
            {
				Console.WriteLine("Unable to execute validate sequence at line " + tok.beginLine + ": " + ex);
                noError = false;
            }
			if((!validated || !noError) && exitOnFailure)
			{
				throw new Exception("validate (at line " + tok.beginLine + ") failed");
			}
        }
    |
	    ( "strict" { strict = true; } ("only" "specified" { onlySpecified = true;})? )? LineEnd()
	    {
		    validated = impl.Validate(strict, onlySpecified);
			if(!validated && exitOnFailure)
			{
				throw new Exception("validate (at line " + tok.beginLine + ") failed");
			}
	    }
	)
|
    (tok="xgrs" | tok="exec") str1=CommandLine()
    {
        try
        {
			List<String> warnings = new List<String>();
            seq = SequenceParser.ParseSequence(str1, impl.CurrentActions, warnings);
			foreach(string warning in warnings)
			{
				Console.WriteLine("The sequence at line " + tok.beginLine + " reported back: " + warning);
			}
            impl.ApplyRewriteSequence(seq, false);
            noError = !impl.OperationCancelled;
        }
        catch(SequenceParserException ex)
        {
            Console.WriteLine("Unable to parse sequence at line " + tok.beginLine);
            impl.HandleSequenceParserException(ex);
            noError = false;
        }
        catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
        {
            Console.WriteLine("Unable to execute sequence at line " + tok.beginLine + ": " + ex.Message);
            noError = false;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Unable to execute sequence at line " + tok.beginLine + ": " + ex);
            noError = false;
        }
    }
|
    tok="def" str1=CommandLine()
    {
        try
        {
			List<String> warnings = new List<String>();
            seqDef = SequenceParser.ParseSequenceDefinition(str1, impl.CurrentActions, warnings);
			foreach(string warning in warnings)
			{
				Console.WriteLine("The sequence definition at line " + tok.beginLine + " reported back: " + warning);
			}
            impl.DefineRewriteSequence(seqDef);
        }
        catch(SequenceParserException ex)
        {
            Console.WriteLine("Unable to parse sequence definition at line " + tok.beginLine);
            impl.HandleSequenceParserException(ex);
            noError = false;
        }
        catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
        {
            Console.WriteLine("Unable to process sequence definition at line " + tok.beginLine + ": " + ex.Message);
            noError = false;
        }
        catch(Exception ex)
        {
            Console.WriteLine("Unable to process sequence definition at line " + tok.beginLine + ": " + ex);
            Console.WriteLine("(You tried to overwrite a compiled sequence?)");
            noError = false;
        }
    }
|
    // TODO: Introduce prefix for the following commands to allow useful error handling!

    try
    {
	    LOOKAHEAD(GraphElement() ".") elem=GraphElement() "." str1=AttributeName()
	    (
	        LineEnd()
	        {
	            impl.ShowElementAttribute(elem, str1);
	        }
	    |
	        "=" { param = new Param(str1); } AttributeParamValue(ref param) LineEnd()
	        {
		        impl.SetElementAttribute(elem, param);
	        }
	    |
	        "[" obj=SimpleConstant() "]" "=" str2=AttributeValue() LineEnd()
	        {
		        impl.SetElementAttributeIndexed(elem, str1, str2, obj);
	        }
		|
			LOOKAHEAD(2) "." "add" "(" obj=SimpleConstant()
			(
				"," obj2=SimpleConstant() ")" LineEnd()
				{
					impl.MapArrayAdd(elem, str1, obj, obj2);
				}
			|
				")" LineEnd()
				{
					impl.SetArrayAdd(elem, str1, obj);
				}
			)
		|
			"." "rem" "("
			(
				obj=SimpleConstant() ")" LineEnd()
				{
					impl.SetMapArrayRemove(elem, str1, obj);
				}
			|
				")" LineEnd()
				{
					impl.SetMapArrayRemove(elem, str1, null);
				}
			)
	    )
	|
        LOOKAHEAD(2) str1=Variable() "="
        (
			"askfor"
			(
				str2=WordOrText()
				{
					obj = impl.Askfor(str2);
					if(obj == null) noError = false;
				}
			|
				"set" "<" str2=WordOrText() ">"
				{
					obj = impl.Askfor("set<"+str2+">");
					if(obj == null) noError = false;
				}
			|
				"map" "<" str2=WordOrText() "," str3=WordOrText() ">"
				{
					obj = impl.Askfor("map<"+str2+","+str3+">");
					if(obj == null) noError = false;
				}
			|
				"array" "<" str2=WordOrText() ">"
				{
					obj = impl.Askfor("array<"+str2+">");
					if(obj == null) noError = false;
				}
			) LineEnd()
		|
		    LOOKAHEAD(2) obj=GraphElementOrUnquotedVar()
			( "." str2=AnyString() { obj = impl.GetElementAttribute((IGraphElement) obj, str2); } )? LineEnd()
		|
			obj=Constant() LineEnd()
		)
        {
			if(noError) impl.SetVariable(str1, obj);
        }
	|
		str1=Variable() "[" obj=SimpleConstant() "]" "=" obj2=SimpleConstant() LineEnd()
		{
			impl.SetVariableIndexed(str1, obj2, obj);
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
	String modelFilename, referencePath, graphName = "DefaultGraph";
	INode srcNode, tgtNode;
	ElementDef elemDef;
	bool directed, on;
}
{
	try
	{
		"graph" modelFilename=Filename() (graphName=WordOrText())? LineEnd()
		{
			noError = impl.NewGraph(modelFilename, graphName);
		}
	|
		"add" "reference" referencePath=Filename() LineEnd()
		{
			noError = impl.NewGraphAddReference(referencePath);
		}
	|
		LOOKAHEAD(2) "set" "keepdebug" ("on" { on = true; } | "off" { on = false; }) LineEnd()
		{
			noError = impl.NewGraphSetKeepDebug(on);
		}
	|
		"set" "lazynic" ("on" { on = true; } | "off" { on = false; }) LineEnd()
		{
			noError = impl.NewGraphSetLazyNIC(on);
		}
	|
		LOOKAHEAD(3)
		srcNode=Node() "-" elemDef=ElementDefinition() ( "->" { directed = true; } | "-" { directed = false; } ) tgtNode=Node() LineEnd()
		{
			noError = impl.NewEdge(elemDef, srcNode, tgtNode, directed) != null;
		}
	|
		LOOKAHEAD(2)
		tgtNode=Node() "<-" elemDef=ElementDefinition() "-" { directed = true; } srcNode=Node() LineEnd()
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
	(varName=Variable())?
	(
		":" typeName=WordOrText()
		(
			"("
			(
				"$" "=" elemName=WordOrText() ("," Attributes(attributes))?
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
	SingleAttribute(attributes) ("," SingleAttribute(attributes) )*
}

void SingleAttribute(ArrayList attributes):
{
	String attribName;
	Param param;
}
{
	attribName=WordOrText() "="
		{ param = new Param(attribName); }
		AttributeParamValue(ref param)
			{ attributes.Add(param); }
}

void AttributeParamValue(ref Param param):
{
	String value, valueTgt;
	String type, typeTgt;
}
{
	value=AttributeValue()
		{
			param.Value = value;
		}
	| "set" "<" type=WordOrText() ">"
		{
			param.Value = "set";
			param.Type = type;
			param.Values = new ArrayList();
		}
		"{" ( value=AttributeValue() { param.Values.Add(value); } )?
			(<COMMA> value=AttributeValue() { param.Values.Add(value); })* "}"
	| "map" "<" type=WordOrText() "," typeTgt=WordOrText() ">"
		{
			param.Value = "map";
			param.Type = type;
			param.TgtType = typeTgt;
			param.Values = new ArrayList();
			param.TgtValues = new ArrayList();
		}
		"{" ( value=AttributeValue() { param.Values.Add(value); } <ARROW> valueTgt=AttributeValue() { param.TgtValues.Add(valueTgt); } )?
			( <COMMA> value=AttributeValue() { param.Values.Add(value); } <ARROW> valueTgt=AttributeValue() { param.TgtValues.Add(valueTgt); } )* "}"
	| "array" "<" type=WordOrText() ">"
		{
			param.Value = "array";
			param.Type = type;
			param.Values = new ArrayList();
		}
		"[" ( value=AttributeValue() { param.Values.Add(value); } )?
			(<COMMA> value=AttributeValue() { param.Values.Add(value); })* "]"
}

//////////////////////
// "select" command //
//////////////////////

void SelectCommand():
{
	String str, mainname;
	ArrayList parameters = new ArrayList();
	ShellGraphProcessingEnvironment shellGraph;
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
		"parser" str=Filename() mainname=WordOrText() LineEnd()
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
	str=WordOrText() { parameters.Add(str); } ("," str=WordOrText() { parameters.Add(str); })*
}

//////////////////////////////////////////////////
// "delete" and "retype" and "redirect" command //
//////////////////////////////////////////////////

void DeleteCommand():
{
	INode node;
	IEdge edge;
	ShellGraphProcessingEnvironment shellGraph = null;
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

void RetypeCommand():
{
	INode node;
	IEdge edge;
	String typeName;
}
{
	try
	{
		"-" edge=Edge() "<" typeName=WordOrText() ">" ( "->" | "-" ) LineEnd()
		{
			noError = impl.Retype(edge, typeName) != null;
		}
	|
		node=Node() "<" typeName=WordOrText() ">" LineEnd()
		{
			noError = impl.Retype(node, typeName) != null;
		}
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpRetype(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

void RedirectCommand():
{
	INode node;
	IEdge edge;
	String str1;
}
{
	try
	{
		"emit" str1=Filename() LineEnd()
		{
			noError = impl.RedirectEmit(str1);
		}
	|
		edge=Edge() str1=WordOrText() node=Node() LineEnd()
		{
			noError = impl.Redirect(edge, str1, node);
		}
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpRedirect(new List<String>());
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
		"graph" str=Filename() (args=WordOrText())? LineEnd()
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
		elem=GraphElement() "." str=AttributeName() LineEnd()
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
	str=Variable() LineEnd()
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
	Token tok;
}
{
	try
	{
		"grs" str=CommandLine()
		{
			Console.WriteLine("The old grs are not supported any longer. Please use the extended graph rewrite sequences exec(/xgrs).");
			noError = false;
		}
	|
		(tok="xgrs" | tok="exec") str=CommandLine()
		{
			try
			{
				List<String> warnings = new List<String>();
				seq = SequenceParser.ParseSequence(str, impl.CurrentActions, warnings);
				foreach(string warning in warnings)
				{
					Console.WriteLine("The debug sequence at line " + tok.beginLine + " reported back: " + warning);
				}
				impl.DebugRewriteSequence(seq);
				noError = !impl.OperationCancelled;
			}
			catch(SequenceParserException ex)
			{
			    Console.WriteLine("Unable to parse debug sequence at line " + tok.beginLine);
				impl.HandleSequenceParserException(ex);
				noError = false;
			}
			catch(de.unika.ipd.grGen.libGr.sequenceParser.ParseException ex)
			{
				Console.WriteLine("Unable to execute debug sequence at line " + tok.beginLine + ": " + ex.Message);
				noError = false;
			}
			catch(Exception ex)
			{
	            Console.WriteLine("Unable to execute debug sequence at line " + tok.beginLine + ": " + ex);
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
		LOOKAHEAD(2)
		"set" "layout"
		(
			"option" str=WordOrText() str2=AnyString() LineEnd()
			{
				impl.SetDebugLayoutOption(str, str2);
			}
		|
			(str=WordOrText())? LineEnd()
			{
				impl.SetDebugLayout(str);
			}
		)
	|
		"get" "layout" "options" LineEnd()
		{
			impl.GetDebugLayoutOptions();
		}
	|
		LOOKAHEAD(2)
		"set" "node" DebugSetNode()
	|
		"set" "edge" DebugSetEdge()
	}
	catch(ParseException ex)
	{
		Console.WriteLine("Invalid command!");
		impl.HelpDebug(new List<String>());
		errorSkipSilent();
		noError = false;
	}
}

void DebugSetNode():
{
	String mode = null, colorName = null, shapeName = null;
}
{
	"mode" mode=WordOrText()
	(
		"color" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDebugNodeModeColor(mode, colorName);
		}
	|
		"bordercolor" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDebugNodeModeBorderColor(mode, colorName);
		}
	|
		"shape" (shapeName=WordOrText())? LineEnd()
		{
			noError = impl.SetDebugNodeModeShape(mode, shapeName);
		}
	|
		"textcolor" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDebugNodeModeTextColor(mode, colorName);
		}
	)
}

void DebugSetEdge():
{
	String mode = null, colorName = null, styleName = null;
	int thickness = 0;
}
{
	"mode" mode=WordOrText()
	(
		"color" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDebugEdgeModeColor(mode, colorName);
		}
	|
		"textcolor" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDebugEdgeModeTextColor(mode, colorName);
		}
	|
		"thickness" (thickness=Number())? LineEnd()
		{
			noError = impl.SetDebugEdgeModeThickness(mode, thickness);
		}
	|
		"linestyle" (styleName=WordOrText())? LineEnd()
		{
			noError = impl.SetDebugEdgeModeStyle(mode, styleName);
		}
	)
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
{ }
{
	"node" DumpSetNode()
|
	"edge" DumpSetEdge()
}

void DumpSetNode():
{
	NodeType nodeType;
	String colorName = null, shapeName = null, labelStr = null;
	bool only = false;
}
{
	("only" { only=true; })? nodeType=NodeType()
	(
		"color" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeColor(nodeType, colorName, only);
		}
	|
		"bordercolor" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeBorderColor(nodeType, colorName, only);
		}
	|
		"shape" (shapeName=WordOrText())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeShape(nodeType, shapeName, only);
		}
	|
		"textcolor" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDumpNodeTypeTextColor(nodeType, colorName, only);
		}
	|
		"labels" ("on" | "off" { labelStr = ""; } | labelStr=WordOrText()) LineEnd()
		{
			noError = impl.SetDumpLabel(nodeType, labelStr, only);
		}
	)
}

void DumpSetEdge():
{
	EdgeType edgeType;
	String colorName = null, styleName = null, labelStr = null;
	bool only = false;
	int thickness = 0;
}
{
	("only" { only=true; })? edgeType=EdgeType()
	(
		"color" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDumpEdgeTypeColor(edgeType, colorName, only);
		}
	|
		"textcolor" (colorName=WordOrText())? LineEnd()
		{
			noError = impl.SetDumpEdgeTypeTextColor(edgeType, colorName, only);
		}
	|
		"thickness" (thickness=Number())? LineEnd()
		{
			noError = impl.SetDumpEdgeTypeThickness(edgeType, thickness, only);
		}
	|
		"linestyle" (styleName=WordOrText())? LineEnd()
		{
			noError = impl.SetDumpEdgeTypeLineStyle(edgeType, styleName, only);
		}
	|
		"labels" ("on" | "off" { labelStr = ""; } | labelStr=WordOrText()) LineEnd()
		{
			noError = impl.SetDumpLabel(edgeType, labelStr, only);
		}
	)
}

void DumpAdd():
{ }
{
	"node" DumpAddNode()
|
	"edge" DumpAddEdge()
}

void DumpAddNode():
{
	NodeType nodeType, adjNodeType = impl.CurrentGraph.Model.NodeModel.RootType;
	EdgeType edgeType = impl.CurrentGraph.Model.EdgeModel.RootType;
	String attrName, groupModeStr = "incoming";
	bool only = false, onlyEdge = false, onlyAdjNode = false, hidden = false;
	GroupMode groupMode;
}
{
	("only" { only=true; })? nodeType=NodeType()
	(
		"exclude" LineEnd()
		{
			noError = impl.AddDumpExcludeNodeType(nodeType, only);
		}
	|
		"group"
		(
		    "by" ("hidden" { hidden = true; })? groupModeStr=WordOrText()
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
		"infotag" attrName=WordOrText() LineEnd()
		{
		    noError = impl.AddDumpInfoTag(nodeType, attrName, only, false);
	    }
	|
		"shortinfotag" attrName=WordOrText() LineEnd()
		{
		    noError = impl.AddDumpInfoTag(nodeType, attrName, only, true);
	    }
    )
}

void DumpAddEdge():
{
	EdgeType edgeType = impl.CurrentGraph.Model.EdgeModel.RootType;
	String attrName;
	bool only = false;
}
{
	("only" { only=true; })? edgeType=EdgeType()
	(
	    "exclude" LineEnd()
	    {
	        noError = impl.AddDumpExcludeEdgeType(edgeType, only);
	    }
	|
	    "infotag" attrName=WordOrText() LineEnd()
	    {
            noError = impl.AddDumpInfoTag(edgeType, attrName, only, false);
        }
	|
	    "shortinfotag" attrName=WordOrText() LineEnd()
	    {
            noError = impl.AddDumpInfoTag(edgeType, attrName, only, true);
        }
	)
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
