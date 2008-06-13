options {
	STATIC=false;
}

PARSER_BEGIN(GrShell)
    namespace de.unika.ipd.grGen.grShell;
    using System;
    using System.Collections;
    using System.IO;
    using System.Text.RegularExpressions;
    using de.unika.ipd.grGen.libGr;
    using de.unika.ipd.grGen.libGr.sequenceParser;
    using grIO;

    class GrShell {
        GrShellImpl impl = null;
        bool valid;
        public bool Quit = false;
        public bool Eof = false;
        public bool ShowPrompt = true;
        bool readFromConsole = false;
        public IWorkaround workaround; 
        bool noError;

        static void Main(string[] args)
        {
            String command = null;
            ArrayList scriptFilename = new ArrayList();
            bool showUsage = false;
            
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
                            showUsage = true;
                            break;
                        }
                        if(i + 1 >= args.Length)
                        {
                            Console.WriteLine("Missing parameter for -C option!");
                            showUsage = true;
                            break;
                        }
                        command = args[i + 1];
                        i++;
                    }
                    else if(args[i] == "--help")
                    {
                        showUsage = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Illegal option: " + args[i]);
                        showUsage = true;
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
                Console.WriteLine("Options:");
                Console.WriteLine("  -C <command> Specifies a command to be executed >first<. Using");
                Console.WriteLine("               ';;' as a delimiter it can actually contain multiple shell commands");
                Console.WriteLine("  <grs-file>   Includes the grs-file(s) in the given order");
                return;
            }
            
            IWorkaround workaround = WorkaroundManager.GetWorkaround();
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
                    return;
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
            try
            {
                while(!shell.Quit && !shell.Eof)
                {
                    bool noError = shell.ParseShellCommand();
                    if(!shell.readFromConsole && (shell.Eof || !noError))
                    {
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
                                return;
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
            finally
            {
                shell.impl.Cleanup();
            }
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
|   < EXIT: "exit" >
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
|   < INCLUDE: "include" >
|   < INFOTAG: "infotag" >
|   < IO: "io" >
|   < IS: "is" >
|   < LABELS: "labels" >
|   < LAYOUT: "layout" >
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
|   < REDIRECT: "redirect" >
|   < RESET: "reset" >
|   < RESETVISITFLAG: "resetvisitflag" >
|   < SAVE: "save" >
|   < SELECT: "select" >
|   < SET: "set" >
|   < SHAPE: "shape" >
|   < SHOW: "show" >
|   < SILENT: "silent" >
|   < STRICT: "strict" >
|   < SUB: "sub" >
|   < SUPER: "super" >
|   < SYNC: "sync" >
|   < TEXTCOLOR: "textcolor" >
|   < TRUE: "true" >
|   < TYPE: "type" >
|   < TYPES: "types" >
|   < VALIDATE: "validate" >
|   < WITH: "with" >
|   < XGRS: "xgrs" >
}

TOKEN: {
	< NUMBER: (["0"-"9"])+ >
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
|   < ANYSTRING: (~[" ", "\t", "\n", "\r", "#", "\"", "\'"])+ > : DEFAULT
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
|   < ANYSTRINGS: (~[" ", "\t", "\n", "\r", "#", "\"", "\'"])+ >
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
	(tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT> | tok=<WORD> | tok=<NUMBER>)
	{
		return tok.image;		
	}
}

int Number():
{
	Token t;
}
{
	t=<NUMBER>
	{
		return Convert.ToInt32(t.image);
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
	String str1, str2;
	IGraphElement elem;
	object obj;
	INode node1, node2;
	IEdge edge1, edge2;
	ShellGraph shellGraph = null;
	Sequence seq;
	bool strict = false, shellGraphSpecified = false;
}
{
    "!" str1=CommandLine()
    {
        impl.ExecuteCommandLine(str1);
    }
|    
	"help" LineEnd()
	{
		impl.Help();
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
	"grs" { valid = true; } seq=OldRewriteSequence() LineEnd()
	{
		if(valid)
		{
		    impl.WarnDeprecatedGrs(seq);
		    impl.ApplyRewriteSequence(seq, false);
		    
		    noError = !impl.OperationCancelled;
		}
		else noError = false;
	}
|
    "xgrs" str1=CommandLine()
    {
        try
        {
            seq = SequenceParser.ParseSequence(str1, impl.CurrentActions, impl.CurrentGraph);
            impl.ApplyRewriteSequence(seq, false);
            noError = !impl.OperationCancelled;
        }
        catch(SequenceParserRuleException ex)
        {
            impl.HandleSequenceParserRuleException(ex);
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
	"validate"
	(
	    "xgrs" str1=CommandLine()
	    {
            try
            {
                seq = SequenceParser.ParseSequence(str1, impl.CurrentActions, impl.CurrentGraph);
    	        impl.ValidateWithSequence(seq);
                noError = !impl.OperationCancelled;
            }
            catch(SequenceParserRuleException ex)
            {
                impl.HandleSequenceParserRuleException(ex);
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
	    ("strict" { strict = true; })? LineEnd()
	    {
		    impl.Validate(strict);
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
    // TODO: Introduce prefix for the following commands to allow useful error handling!
    
    try
    {
	    LOOKAHEAD(2) elem=GraphElement() "." str1=Text()
	    (
	        LineEnd()
	        {
	            impl.ShowElementAttribute(elem, str1);
	        }
	    |
	        "=" str2=TextOrNumber() LineEnd()
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
            obj=GraphElementOrVarOrNull() LineEnd()
        |
            obj=QuotedText() LineEnd()
        |
            obj=Number() LineEnd()
        |
			obj=Bool() LineEnd()
        )
        {
			if(noError) impl.SetVariable(str1, obj);
        }
    }
    catch(ParseException ex)
    {
        throw new ParseException("Unknown command!");
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
	bool silent = false;
}
{
	"graph" modelFilename=Filename() (graphName=Text())? LineEnd()
	{
		noError = impl.NewGraph(modelFilename, graphName);
	}
|
	LOOKAHEAD(2)
	srcNode=Node() "-" elemDef=ElementDefinition() "->" tgtNode=Node() ("silent" { silent = true;} )? LineEnd()
	{
		noError = impl.NewEdge(elemDef, srcNode, tgtNode, silent) != null;
	}
	
|
	elemDef=ElementDefinition() ("silent" { silent = true;} )? LineEnd()
	{
		noError = impl.NewNode(elemDef, silent) != null;
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
	String a, b;
}
{
	a=Text() "=" b=TextOrNumber()
	{
		attributes.Add(new Param(a, b));
	}
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

////////////////////
// "show" command //
////////////////////

void ShowCommand():
{
	String str;
	String args = null;
	NodeType nodeType = null;
	EdgeType edgeType = null;
	bool typeProvided = false;
	bool only = false;
}
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

//////////////////////
// "debug" command" //
//////////////////////

void DebugCommand():
{
    Sequence seq;
    String str = null, str2;
}
{
	"grs" { valid = true; } seq=OldRewriteSequence() LineEnd()
	{
		if(valid)
		{
		    impl.WarnDeprecatedGrs(seq);
		    impl.DebugRewriteSequence(seq);
		    noError = !impl.OperationCancelled;
		}
		else noError = false;
	}
|
    "xgrs" str=CommandLine()
    {
        try
        {
            seq = SequenceParser.ParseSequence(str, impl.CurrentActions, impl.CurrentGraph);
            impl.DebugRewriteSequence(seq);
            noError = !impl.OperationCancelled;
        }
        catch(SequenceParserRuleException ex)
        {
            impl.HandleSequenceParserRuleException(ex);
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

/////////////////////////////////////////
// Old rewrite sequence                //
// (lowest precedence operators first) //
/////////////////////////////////////////

Sequence OldRewriteSequence():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=OldRewriteSequence2()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? ";" seq2=OldRewriteSequence()							
		{
			seq = new SequenceStrictOr(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence OldRewriteSequence2():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=OldRewriteSequence3()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "|" seq2=OldRewriteSequence2()
		{
			seq = new SequenceLazyOr(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence OldRewriteSequence3():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=OldSingleSequence()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "&" seq2=OldRewriteSequence3()
		{
			seq = new SequenceTransaction(new SequenceLazyAnd(seq, seq2, random));
		}
	)?
	{
		return seq;
	}
}

Sequence OldSingleSequence():
{
	Sequence seq;
	int maxnum;
}
{
	(
		seq=OldSimpleSequence()
		(
			"*"
			{
				seq = new SequenceMin(seq, 1);
			}
		|	"{" maxnum=Number() "}"
			{
				seq = new SequenceMinMax(seq, 1, maxnum);
			}
		)?
	)
	{
		return seq;
	}
}

Sequence OldSimpleSequence():
{
	bool dump = false;
	Sequence seq;
	RuleObject ruleObject;
	ArrayList defParamVars = new ArrayList();
	String toVarName, fromName;
	IGraphElement elem;
}
{
	LOOKAHEAD(2)
	("!" { dump = true; } )? "[" ruleObject=Rule() "]"
	{
		if(ruleObject == null)
		{
			return null;
		}
		return new SequenceRuleAll(ruleObject, dump, false, 0);
	}
|
	LOOKAHEAD(2)
    toVarName=Text() "="
    (
        fromName=Text()
        {
            return new SequenceNot(new SequenceAssignVarToVar(toVarName, fromName));
        }
    |
        "@" "(" fromName=Text() ")"
        {
            elem = impl.GetElemByName(fromName);
            if(elem == null)
            {
                valid = false;
                return null;
            }
            return new SequenceNot(new SequenceAssignElemToVar(toVarName, elem));
        }
    )
|
    // 4 tokens lookahead: "(" <TEXT> ")" ("=" => next is Rule() | (<NL> | operator) => parentheses around OldRewriteSequence)
	LOOKAHEAD(4)        
	("!" { dump = true; } )? ruleObject=Rule()
	{
		if(ruleObject == null)
		{
			return null;
		}
		return new SequenceRule(ruleObject, dump, false);
	}
|
	"def" "(" Parameters(defParamVars) ")"
	{
		return new SequenceDef((String[]) defParamVars.ToArray(typeof(String)));
	}
|
    "true"
    {
        return new SequenceTrue(false);
    }
|
    "false"
    {
        return new SequenceFalse(false);
    }
|
	"(" seq=OldRewriteSequence() ")"
	{
		return seq;
	}
}

RuleObject Rule():
{
	String str;
	IAction action;
	bool retSpecified = false;
	ArrayList paramVars = new ArrayList();
	ArrayList returnVars = new ArrayList();
}
{
	("(" Parameters(returnVars) ")" "=" { retSpecified = true; })? str=Text() ("(" Parameters(paramVars) ")")?
	{
		action = impl.GetAction(str, paramVars.Count, returnVars.Count, retSpecified);
		if(action == null)
		{
			valid = false;
			return null;
		}
		if(!retSpecified && action.RulePattern.Outputs.Length > 0)
		{
			returnVars = ArrayList.Repeat(null, action.RulePattern.Outputs.Length);
		}
		return new RuleObject(action, (String[]) paramVars.ToArray(typeof(String)), (String[]) returnVars.ToArray(typeof(String)));
	}
}

/////////////////////
// "dump" commands //
/////////////////////

void DumpCommand():
{
	String filename;
}
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

void DumpSet():
{
	NodeType nodeType;
	EdgeType edgeType;
	String colorName, shapeName;
	bool labels = false, only = false;
}
{
	"node" ("only" { only=true; })? nodeType=NodeType()
	(
		"color" colorName=Text() LineEnd()
		{
			impl.SetDumpNodeTypeColor(nodeType, colorName, only);
		}
    |
		"bordercolor" colorName=Text() LineEnd()
		{
			impl.SetDumpNodeTypeBorderColor(nodeType, colorName, only);
		}
    |
		"shape" shapeName=Text() LineEnd()
		{
			impl.SetDumpNodeTypeShape(nodeType, shapeName, only);
		}
    |
		"textcolor" colorName=Text() LineEnd()
		{
			impl.SetDumpNodeTypeTextColor(nodeType, colorName, only);
		}
	)
|
	"edge"
	(
	    ("only" { only=true; })? edgeType=EdgeType()
	    (
	        "color" colorName=Text() LineEnd()
	        {
		        impl.SetDumpEdgeTypeColor(edgeType, colorName, only);
	        }
        | 
	        "textcolor" colorName=Text() LineEnd()
	        {
		        impl.SetDumpEdgeTypeTextColor(edgeType, colorName, only);
	        }
	    )
    |
		"labels" ("on" { labels = true; } | "off") LineEnd()
		{
			impl.SetDumpEdgeLabels(labels);
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
			impl.AddDumpExcludeNodeType(nodeType, only);
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
	                return;
	        }
	        if(hidden)
	        {
	            if(groupMode == GroupMode.None)
	            {
	                Console.WriteLine("The 'hidden' modifier can not be used with the group mode 'no'!");
	                return;
	            }
	            groupMode |= GroupMode.Hidden;
	        }
            impl.AddDumpGroupNodesBy(nodeType, only, edgeType, onlyEdge, adjNodeType, onlyAdjNode, groupMode);
	    }
	|
		"infotag" attrName=Text() LineEnd()
		{
		    impl.AddDumpInfoTag(nodeType, attrName, only);
	    }
    )
|
	"edge" ("only" { only=true; })? edgeType=EdgeType()
	(
	    "exclude" LineEnd()
	    {
	        impl.AddDumpExcludeEdgeType(edgeType, only);
	    }
	|
	    "infotag" attrName=Text() LineEnd()
	    {
            impl.AddDumpInfoTag(edgeType, attrName, only);
        }
	)
}

///////////////////////
// "custom" commands //
///////////////////////

void CustomCommand():
{
	ArrayList parameters = new ArrayList();
}
{
	"graph" SpacedParametersAndLineEnd(parameters)
	{
		impl.CustomGraph(parameters);
	}
|
	"actions" SpacedParametersAndLineEnd(parameters)
	{
		impl.CustomActions(parameters);
	}
}

void SpacedParametersAndLineEnd(ArrayList parameters):
{
	Token tok;
}
{
    {token_source.SwitchTo(WithinAnyStrings);}
    (
        (tok=<ANYSTRINGS> | tok=<DOUBLEQUOTEDANYSTRINGS> | tok=<SINGLEQUOTEDANYSTRINGS>)
        { parameters.Add(tok.image); }
    )*
    (<NLINANYSTRINGS> | <EOF>)
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

TOKEN: {
	<ERROR: ~[]>
}