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

    // The attribute values are stored as written (string literals including their quotation marks),
    // the checking of the values is up to the second pass.
    static readonly HashSet<string> graphAttributes = new HashSet<string> { "title", "label", "textcolor", "color", "info1" };
    static readonly HashSet<string> nodeAttributes = new HashSet<string> { "title", "label", "textcolor", "color", "bordercolor", "shape", "info1" };
    static readonly HashSet<string> edgeAttributes = new HashSet<string> { "sourcename", "targetname", "label", "textcolor", "color", "linestyle", "thickness" };
    // written by the VCGBaseDumper, but of no interest to us (the layout is chosen by extMSAGLExt)
    static readonly HashSet<string> silentlyIgnoredGraphAttributes = new HashSet<string> { "layoutalgorithm" };

    // returns true if the attribute is to be entered into the AST, otherwise a warning was emitted (or it is silently ignored)
    bool AcceptAttribute(HashSet<string> knownAttributes, string context, string key, string val)
    {
        if(knownAttributes.Contains(key))
            return true;
        if(context == "graph" && silentlyIgnoredGraphAttributes.Contains(key))
            return false;
        System.Console.Error.WriteLine("VCG warning: unknown " + context + " attribute \"" + key + "\" (value: " + val + ") -- ignored");
        return false;
    }

    // the title/sourcename/targetname attributes must be string literals, returns their content or null (after emitting an error)
    static string GetNameOrNull(Entity entity, string attributeName, string context)
    {
        string raw;
        if(!entity.Attributes.TryGetValue(attributeName, out raw))
        {
            System.Console.Error.WriteLine("VCG error: " + context + " missing mandatory " + attributeName + " attribute -- skipped");
            return null;
        }
        if(!Literal.IsStringLiteral(raw))
        {
            System.Console.Error.WriteLine("VCG error: " + context + " attribute " + attributeName + " is not a string literal (" + raw + ") -- skipped");
            return null;
        }
        return Literal.ToStringValue(raw);
    }
}

// ---- parser rules ----

public vcgFile returns [Graph result]
    : g=topGraph EOF { result = $g.result; }
    ;

topGraph returns [Graph result]
    @init { result = new Graph(); }
    : 'graph' COLON LBRACE
        ( a=graphAttribute  { if(AcceptAttribute(graphAttributes, "graph", $a.key, $a.val)) result.Attributes[$a.key] = $a.val; }
        | s=subgraph        { if($s.result != null) result.Children.Add($s.result); }
        | n=nodeRule        { if($n.result != null) result.Children.Add($n.result); }
        | e=edgeRule        { if($e.result != null) result.Children.Add($e.result); }
        )*
      RBRACE
    ;

subgraph returns [Subgraph result]
    @init { result = new Subgraph(); }
    : 'graph' COLON LBRACE
        ( a=graphAttribute  { if(AcceptAttribute(graphAttributes, "graph", $a.key, $a.val)) result.Attributes[$a.key] = $a.val; }
        | s=subgraph        { if($s.result != null) result.Children.Add($s.result); }
        | n=nodeRule        { if($n.result != null) result.Children.Add($n.result); }
        | e=edgeRule        { if($e.result != null) result.Children.Add($e.result); }
        )*
      RBRACE
      {
          string title = GetNameOrNull(result, "title", "subgraph");
          if(title == null)
              result = null;
          else if(NodeOrSubgraphByName.ContainsKey(title))
          {
              System.Console.Error.WriteLine("VCG error: duplicate subgraph title \"" + title + "\" -- skipped");
              result = null;
          }
          else
              NodeOrSubgraphByName[title] = result;
      }
    ;

nodeRule returns [Node result]
    @init { result = new Node(); }
    : 'node' COLON LBRACE
        ( a=nodeAttribute { if(AcceptAttribute(nodeAttributes, "node", $a.key, $a.val)) result.Attributes[$a.key] = $a.val; } )*
      RBRACE
      {
          string title = GetNameOrNull(result, "title", "node");
          if(title == null)
              result = null;
          else if(NodeOrSubgraphByName.ContainsKey(title))
          {
              System.Console.Error.WriteLine("VCG error: duplicate node title \"" + title + "\" -- skipped");
              result = null;
          }
          else
              NodeOrSubgraphByName[title] = result;
      }
    ;

edgeRule returns [Edge result]
    @init { result = new Edge(); }
    : 'edge' COLON LBRACE
        ( a=edgeAttribute { if(AcceptAttribute(edgeAttributes, "edge", $a.key, $a.val)) result.Attributes[$a.key] = $a.val; } )*
      RBRACE
      {
          if(GetNameOrNull(result, "sourcename", "edge") == null || GetNameOrNull(result, "targetname", "edge") == null)
              result = null;
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
