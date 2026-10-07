// by Claude Code with Edgar Jakumeit
//
// ANTLR 3.5 grammar for VCG files as produced by GrGen.NET VCGDumper.
// Builds a VcgAst.Graph. The nodeOrSubgraphNameToEntity map is populated
// as a side effect during parsing (working as a kind of definition table),
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

    // Populated during parsing; edges use this to resolve source/target names.
    public Dictionary<string, Node> NodeOrSubgraphByName = new Dictionary<string, Node>();

    void WarnUnknown(string context, string key, string val)
    {
        System.Console.Error.WriteLine("VCG warning: unknown " + context + " attribute \"" + key + "\" (value: \"" + val + "\") -- ignored");
    }
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
          string title;
          if(result.Attributes.TryGetValue("title", out title))
          {
              if(!NodeOrSubgraphByName.ContainsKey(title))
                  NodeOrSubgraphByName[title] = result;
              else
              {
                  System.Console.Error.WriteLine("VCG error: duplicate subgraph title \"" + title + "\" -- skipped");
                  result = null;
              }
          }
          else
          {
              System.Console.Error.WriteLine("VCG error: subgraph missing mandatory title attribute -- skipped");
              result = null;
          }
      }
    ;

nodeRule returns [Node result]
    @init { result = new Node(); }
    : 'node' COLON LBRACE
        ( a=nodeAttribute { result.Attributes[$a.key] = $a.val; } )*
      RBRACE
      {
          string title;
          if(result.Attributes.TryGetValue("title", out title))
          {
              if(!NodeOrSubgraphByName.ContainsKey(title))
                  NodeOrSubgraphByName[title] = result;
              else
              {
                  System.Console.Error.WriteLine("VCG error: duplicate node title \"" + title + "\" -- skipped");
                  result = null;
              }
          }
          else
          {
              System.Console.Error.WriteLine("VCG error: node missing mandatory title attribute -- skipped");
              result = null;
          }
      }
    ;

edgeRule returns [Edge result]
    @init { result = new Edge(); }
    : 'edge' COLON LBRACE
        ( a=edgeAttribute { result.Attributes[$a.key] = $a.val; } )*
      RBRACE
      {
          if(!result.Attributes.ContainsKey("sourcename") || !result.Attributes.ContainsKey("targetname"))
          {
              System.Console.Error.WriteLine("VCG error: edge missing mandatory sourcename or targetname -- skipped");
              result = null;
          }
      }
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
    | s=STRING_LITERAL  { string raw = $s.text; result = raw.Substring(1, raw.Length - 2); }
    | i=IDENT           { result = $i.text; }
    ;

// ---- lexer rules ----

COLON   : ':' ;
LBRACE  : '{' ;
RBRACE  : '}' ;

FLOAT  : ('0'..'9')+ '.' ('0'..'9')* ;
NUMBER : ('0'..'9')+ ;

STRING_LITERAL : '"' ( ~('"') )* '"' ;

IDENT : ('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'0'..'9'|'_'|'-'|'.')* ;

LINE_COMMENT  : '//' ( ~('\n') )* '\n'                              { $channel = Hidden; } ;
BLOCK_COMMENT : '/*' ( options { greedy=false; } : . )* '*/'       { $channel = Hidden; } ;

WS : (' '|'\t'|'\r'|'\n')+ { $channel = Hidden; } ;
