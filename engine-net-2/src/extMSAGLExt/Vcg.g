// by Claude Code with Edgar Jakumeit
//
// ANTLR 3.5 grammar for VCG files as produced by GrGen.NET VCGDumper.
// Builds a VcgAst.Graph. The NodeOrSubgraphByName map of the VcgParserEnvironment env
// is populated as a side effect during parsing (working as a kind of definition table),
// so edge source/target names can be resolved in the second pass.
//
// Regenerate with: ./genparser.sh  (or genparser.bat on Windows)
// Generated files: VcgLexer.cs, VcgParser.cs  (do not edit)

grammar Vcg;

/*options {
    language = CSharp3;
}*/

@lexer::header {
    // VcgLexer - generated from Vcg.g by ANTLR 3.5. Do not edit.
}

@parser::header {
    // VcgParser - generated from Vcg.g by ANTLR 3.5. Do not edit.
    using de.unika.ipd.grGen.extMSAGLExt;
    using de.unika.ipd.grGen.extMSAGLExt.VcgAst;
}

@lexer::members {
    public List<string> Errors = new List<string>();

    public override void EmitErrorMessage(string msg)
    {
        Errors.Add(msg);
    }
}

@parser::members {
    public List<string> Errors = new List<string>();

    public override void EmitErrorMessage(string msg)
    {
        Errors.Add(msg);
    }

    // the definition table and the helpers, to be set before parsing (referenced in the actions)
    public VcgParserEnvironment env;
}

// ---- parser rules ----

public vcgFile returns [Graph result]
    : g=topGraph EOF { result = $g.result; }
    ;

topGraph returns [Graph result]
    @init { result = new Graph(); }
    : 'graph' COLON LBRACE
        ( a=graphAttribute  { result.Attributes[$a.key] = $a.val; }
        | s=subgraph        { if($s.result != null) result.Children.Add($s.result); }
        | n=nodeRule        { if($n.result != null) result.Children.Add($n.result); }
        | e=edgeRule        { if($e.result != null) result.Children.Add($e.result); }
        )*
      RBRACE
    ;

subgraph returns [Subgraph result]
    @init { result = new Subgraph(); }
    : 'graph' COLON LBRACE
        ( a=graphAttribute  { result.Attributes[$a.key] = $a.val; }
        | s=subgraph        { if($s.result != null) result.Children.Add($s.result); }
        | n=nodeRule        { if($n.result != null) result.Children.Add($n.result); }
        | e=edgeRule        { if($e.result != null) result.Children.Add($e.result); }
        )*
      RBRACE
      {
          string title = env.GetNameOrNull(result, "title", "subgraph");
          if(title == null || !env.AddNodeOrSubgraph(title, result, "subgraph"))
              result = null;
      }
    ;

nodeRule returns [Node result]
    @init { result = new Node(); }
    : 'node' COLON LBRACE
        ( a=nodeAttribute { result.Attributes[$a.key] = $a.val; } )*
      RBRACE
      {
          string title = env.GetNameOrNull(result, "title", "node");
          if(title == null || !env.AddNodeOrSubgraph(title, result, "node"))
              result = null;
      }
    ;

edgeRule returns [Edge result]
    @init { result = new Edge(); }
    : 'edge' COLON LBRACE
        ( a=edgeAttribute { result.Attributes[$a.key] = $a.val; } )*
      RBRACE
    ;

graphAttribute returns [string key, string val]
    : k=IDENT (NUMBER)? COLON v=attributeValue { $key = $k.text; $val = $v.result; }
    ;

nodeAttribute returns [string key, string val]
    : k=IDENT (NUMBER)? COLON v=attributeValue { $key = $k.text; $val = $v.result; }
    ;

edgeAttribute returns [string key, string val]
    : k=IDENT (NUMBER)? COLON v=attributeValue { $key = $k.text; $val = $v.result; }
    ;

attributeValue returns [string result]
    : n=NUMBER          { result = $n.text; }
    | f=FLOAT           { result = $f.text; }
    | s=STRING_LITERAL  { result = $s.text; } // including the quotation marks, so the kind of the value remains known
    | i=IDENT           { result = $i.text; }
    ;

// ---- lexer rules ----

COLON   : ':' ;
LBRACE  : '{' ;
RBRACE  : '}' ;

FLOAT  : ('0'..'9')+ '.' ('0'..'9')* ;
NUMBER : ('0'..'9')+ ;

// the quotation marks and backslashes in the string are escaped by a backslash, newlines are contained verbatim
STRING_LITERAL : '"' ( '\\' . | ~('"' | '\\') )* '"' ;

IDENT : ('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'0'..'9'|'_'|'-'|'.')* ;

LINE_COMMENT  : '//' ( ~('\n') )* '\n'                              { $channel = Hidden; } ;
BLOCK_COMMENT : '/*' ( options { greedy=false; } : . )* '*/'       { $channel = Hidden; } ;

WS : (' '|'\t'|'\r'|'\n')+ { $channel = Hidden; } ;
