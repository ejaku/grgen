// by Edgar Jakumeit, Moritz Kroll

options {
    STATIC = false;
    IGNORE_CASE = false;
}

PARSER_BEGIN(SequenceParser)
    namespace de.unika.ipd.grGen.libGr.sequenceParser;
    using System;
    using System.IO;
    using System.Text;
    using System.Collections.Generic;
    using de.unika.ipd.grGen.libGr;

    /// <summary>
    /// A parser class for xgrs strings.
    /// </summary>
    public class SequenceParser
    {
        /// <summary>
        /// The environment setting the context for the sequence parser, containing the entitites and types that can be referenced
        /// </summary>
        SequenceParserEnvironment env;

        /// <summary>
        /// Symbol table of the sequence variables, maps from name to the prefixed(by block nesting) name and the type;
        /// a graph-global variable maps to type "", a sequence-local to its type
        /// </summary>
        SymbolTable varDecls;
        
        /// <summary>
        /// Stores the warnings which occur during parsing
        /// </summary>
        List<String> warnings;

        /// <summary>
        /// Parses a given string in xgrs syntax and builds a Sequence object. Used for the interpreted xgrs.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="env">The environment containing the entities and types that can be referenced.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
        public static Sequence ParseSequence(String sequenceStr, SequenceParserEnvironmentInterpreted env, List<String> warnings)
        {
            SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
            parser.env = env;
            parser.varDecls = new SymbolTable();
            parser.varDecls.PushFirstScope(null);
            parser.warnings = warnings;
            Sequence seq = parser.XGRS();
            SequenceCheckingEnvironment checkEnv = new SequenceCheckingEnvironmentInterpreted(env.Actions);
            seq.Check(checkEnv);
            return seq;
        }

        /// <summary>
        /// Parses a given string in xgrs exp syntax and builds a SequenceExpression object. 
        /// Used for the interpreted if clauses for conditional watchpoint debugging.
        /// </summary>
        /// <param name="sequenceExprStr">The string representing a xgrs expression (e.g. "func() &amp;&amp; (st[e]==0 || var + 1 < 42)")</param>
        /// <param name="predefinedVariables">A map from variables to types giving the predefined this variable for the sequence expression.</param>
        /// <param name="env">The environment containing the entities and types that can be referenced.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence expression object according to sequenceExprStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
        public static SequenceExpression ParseSequenceExpression(String sequenceExprStr, Dictionary<String, String> predefinedVariables, SequenceParserEnvironmentInterpreted env, List<String> warnings)
        {
            SequenceParser parser = new SequenceParser(new StringReader(sequenceExprStr));
            parser.env = env;
            parser.varDecls = new SymbolTable();
            parser.varDecls.PushFirstScope(predefinedVariables);
            parser.warnings = warnings;
            SequenceExpression seqExpr = parser.Expression();
            SequenceCheckingEnvironment checkEnv = new SequenceCheckingEnvironmentInterpreted(env.Actions);
            seqExpr.Check(checkEnv);
            return seqExpr;
        }

        /// <summary>
        /// Parses a given string in sequence definition syntax and builds a SequenceDefinition object. Used for the interpreted xgrs.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="env">The environment containing the entities and types that can be referenced.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
        public static ISequenceDefinition ParseSequenceDefinition(String sequenceStr, SequenceParserEnvironmentInterpreted env, List<String> warnings)
        {
            SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
            parser.env = env;
            parser.varDecls = new SymbolTable();
            parser.varDecls.PushFirstScope(null);
            parser.warnings = warnings;
            SequenceDefinition seq = parser.defXGRS();
            SequenceCheckingEnvironment checkEnv = new SequenceCheckingEnvironmentInterpreted(env.Actions);
            seq.Check(checkEnv);
            return seq;
        }

        /// <summary>
        /// Parses a given string in xgrs syntax and builds a Sequence object. Used for the compiled xgrs.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="env">The environment containing the entities and types that can be referenced.</param>
        /// <param name="predefinedVariables">A map from variables to types giving the parameters to the sequence, i.e. predefined variables.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
        public static Sequence ParseSequence(String sequenceStr, SequenceParserEnvironmentCompiled env, 
                Dictionary<String, String> predefinedVariables, List<String> warnings)
        {
            SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
            parser.env = env;
            parser.varDecls = new SymbolTable();
            parser.varDecls.PushFirstScope(predefinedVariables);
            parser.warnings = warnings;
            Sequence seq = parser.XGRS();
            // check will be done by LGSPSequenceChecker from lgsp code afterwards outside of this libGr code
            return seq;
        }
    }
PARSER_END(SequenceParser)

// characters to be skipped
SKIP: {
    " " |
    "\t" |
    "\n" |
    "\r"
}

TOKEN: {
    < EQUAL: "=" >
|   < ASSIGN_TO: "=>" >
|   < BOR_TO: "|>" >
|   < BAND_TO: "&>" >
|   < COMMA: "," >
|   < DOLLAR: "$" >
|   < DOUBLEAMPERSAND: "&&" >
|   < AMPERSAND: "&" >
|   < DOUBLEPIPE: "||" >
|   < PIPE: "|" >
|   < CIRCUMFLEX: "^" >
|   < EQUALITY: "==" >
|   < INEQUALITY: "!=" >
|   < LOWEREQUAL: "<=" >
|   < GREATEREQUAL: ">=" >
|   < STRUCTURALEQUAL: "~~" >
|   < STAR: "*" >
|   < PLUS: "+" >
|   < DIV: "/" >
|   < BACKSLASH: "\\" >
|   < EXCLAMATIONMARK: "!" >
|   < LPARENTHESIS: "(" >
|   < RPARENTHESIS: ")" >
|   < LBOXBRACKET: "[" >
|   < RBOXBRACKET: "]" >
|   < LANGLE: "<" >
|   < RANGLE: ">" >
|   < LLANGLE: "<<" >
|   < RRANGLE: ">>" >
|   < LBRACE: "{" >
|   < RBRACE: "}" >
|   < COLON: ":" >
|   < DOUBLECOLON: "::" >
|   < PERCENT: "%" >
|   < QUESTIONMARK: "?" >
|   < AT : "@" >
|   < ARROW: "->" >
|   < DOT: "." >
|   < THENLEFT: "<;" >
|   < THENRIGHT: ";>" >
|   < SEMI: ";" >
|   < DOUBLESEMI: ";;" >
}

TOKEN: {
    < DEF: "def" >
|   < TRUE: "true" >
|   < FALSE: "false" >
|   < NULL: "null" >
|   < SET: "set" >
|   < MAP: "map" >
|   < ARRAY: "array" >
|   < DEQUE: "deque" >
|   < MATCH: "match" >
|   < FOR: "for" >
|   < IF: "if" >
|   < IN: "in" >
|   < VISITED: "visited" >
|   < YIELD: "yield" >
|   < COUNT: "count" >
|   < THIS: "this" >
}

TOKEN: {
    < NUMFLOAT:
            ("-")? (["0"-"9"])+ ("." (["0"-"9"])+)? (<EXPONENT>)? ["f", "F"]
        |    ("-")? "." (["0"-"9"])+ (<EXPONENT>)? ["f", "F"]
    >
|
    < NUMDOUBLE:
            ("-")? (["0"-"9"])+ "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
        |    ("-")? "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
        |    ("-")? (["0"-"9"])+ <EXPONENT> (["d", "D"])?
        |    ("-")? (["0"-"9"])+ ["d", "D"]
    >
|
    < #EXPONENT: ["e", "E"] (["+", "-"])? (["0"-"9"])+ >
|
    < NUMBER: ("-")? (["0"-"9"])+ >
|   < NUMBER_BYTE: ("-")? (["0"-"9"])+ ("y"|"Y") >
|   < NUMBER_SHORT: ("-")? (["0"-"9"])+ ("s"|"S") >
|   < NUMBER_LONG: ("-")? (["0"-"9"])+ ("l"|"L") >
|
    < HEXNUMBER: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ >
|   < HEXNUMBER_BYTE: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("y"|"Y") >
|   < HEXNUMBER_SHORT: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("s"|"S") >
|   < HEXNUMBER_LONG: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("l"|"L") >
|
    < DOUBLEQUOTEDTEXT : "\"" ("\\\"" | ~["\"", "\n", "\r"])* "\"" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|
    < SINGLEQUOTEDTEXT : "\'" ("\\\'" | ~["\'", "\n", "\r"])* "\'" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|
< WORD : ["A"-"Z", "a"-"z", "_"] (["A"-"Z", "a"-"z", "_", "0"-"9"])* >
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

String TextString():
{
    Token tok;
}
{
    (tok=<DOUBLEQUOTEDTEXT> | tok=<SINGLEQUOTEDTEXT>)
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

int Number():
{
    Token t;
    int val;
}
{
    (
        t=<NUMBER>
        {
            if(!Int32.TryParse(t.image, out val))
                throw new ParseException("Integer expected but found: \"" + t + "\" (" + t.kind + ")");
            return val;
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

void Argument(List<SequenceExpression> argExprs):
{
    SequenceExpression expr;
}
{
    expr=Expression()
    {
        argExprs.Add(expr);
    }
}

object Constant():
{
    object constant = null;
    Token tok;
    string type, value, package, packageOrType, typeOrValue;
    EnumAttributeType attrType;
}
{
    (
      (
        tok=<NUMBER> { constant = Convert.ToInt32(tok.image); }
        | tok=<NUMBER_BYTE> { constant = Convert.ToSByte(RemoveTypeSuffix(tok.image)); }
        | tok=<NUMBER_SHORT> { constant = Convert.ToInt16(RemoveTypeSuffix(tok.image)); }
        | tok=<NUMBER_LONG> { constant = Convert.ToInt64(RemoveTypeSuffix(tok.image)); }
        | tok=<HEXNUMBER> { constant = Int32.Parse(tok.image.Substring("0x".Length), System.Globalization.NumberStyles.HexNumber); }
        | tok=<HEXNUMBER_BYTE> { constant = SByte.Parse(RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber); }
        | tok=<HEXNUMBER_SHORT> { constant = Int16.Parse(RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber); }
        | tok=<HEXNUMBER_LONG> { constant = Int64.Parse(RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber); }
      )
    |
        constant=FloatNumber()
    |
        constant=DoubleNumber()
    |
        constant=TextString()
    |
        <TRUE> { constant = true; }
    |
        <FALSE> { constant = false; }
    |
        <NULL> { constant = null; }
    |
        LOOKAHEAD(4)
        package=Word() "::" type=Word() "::" value=Word()
        {
            attrType = TypesHelper.GetEnumAttributeType(package+"::"+type, env.Model);
            if(attrType!=null)
                constant = Enum.Parse(attrType.EnumType, value);
            if(constant==null)
                throw new ParseException("Invalid constant \""+package+"::"+type+"::"+value+"\"!");
        }
    |
        LOOKAHEAD(2)
        packageOrType=Word() "::" typeOrValue=Word()
        {
            package = packageOrType;
            type = typeOrValue;
            constant = TypesHelper.GetNodeOrEdgeType(package+"::"+type, env.Model);
            if(constant==null)
            {
                type = packageOrType;
                value = typeOrValue;
                attrType = TypesHelper.GetEnumAttributeType(type, env.Model);
                if(attrType!=null)
                    constant = Enum.Parse(attrType.EnumType, value);
            }
            if(constant==null)
                throw new ParseException("Invalid constant \""+packageOrType+"::"+typeOrValue+"\"!");
        }
    |
        LOOKAHEAD({ GetToken(1).kind==WORD && varDecls.Lookup(GetToken(1).image)==null && TypesHelper.GetNodeOrEdgeType(GetToken(1).image, env.Model)!=null })
        type=Word()
        {
            constant = TypesHelper.GetNodeOrEdgeType(type, env.Model);
        }
    )
    {
        return constant;
    }
}

SequenceExpression InitContainerExpr():
{
    string typeName, typeNameDst;
    List<SequenceExpression> srcItems = null;
    List<SequenceExpression> dstItems = null;
    SequenceExpression src = null, dst = null, res = null, value = null;
}
{
    (
        "set" "<" typeName=TypeNonGeneric() ">" { srcItems = new List<SequenceExpression>(); }
        (
            "{"
                ( src=Expression() { srcItems.Add(src); }
                    ( "," src=Expression() { srcItems.Add(src); })* )?
            "}"
            {
                res = new SequenceExpressionSetConstructor(typeName, srcItems.ToArray());
            }
        |
            "("
                value=Expression()
            ")"
            {
                res = new SequenceExpressionSetCopyConstructor(typeName, value);
            }
        )
    |
        "map" "<" typeName=TypeNonGeneric() "," typeNameDst=TypeNonGeneric() ">" { srcItems = new List<SequenceExpression>(); dstItems = new List<SequenceExpression>(); }
        (
            "{"
                ( src=Expression() "->" dst=Expression() { srcItems.Add(src); dstItems.Add(dst); }
                    ( "," src=Expression() "->" dst=Expression() { srcItems.Add(src); dstItems.Add(dst); } )* )?
            "}"
            {
                res = new SequenceExpressionMapConstructor(typeName, typeNameDst, srcItems.ToArray(), dstItems.ToArray());
            }
        )
    |
        "array" "<" typeName=TypeNonGeneric() ">" { srcItems = new List<SequenceExpression>(); }
        (
            "["
                ( src=Expression() { srcItems.Add(src); }
                    ( "," src=Expression() { srcItems.Add(src); })* )?
            "]"
            {
                res = new SequenceExpressionArrayConstructor(typeName, srcItems.ToArray());
            }
        )
    |
        "deque" "<" typeName=TypeNonGeneric() ">" { srcItems = new List<SequenceExpression>(); }
        (
            "["
                ( src=Expression() { srcItems.Add(src); }
                    ( "," src=Expression() { srcItems.Add(src); })* )?
            "]"
            {
                res = new SequenceExpressionDequeConstructor(typeName, srcItems.ToArray());
            }
        )
    )
    {
        return res;
    }
}

void Arguments(List<SequenceExpression> argExprs):
{ }
{
    Argument(argExprs) ("," Argument(argExprs))*
}


SequenceVariable Variable(): // usage as well as definition
{
    String varName, typeName=null;
    SequenceVariable oldVariable, newVariable;
}
{
    (
        varName=Word() (":" typeName=Type() )?
        {
            oldVariable = varDecls.Lookup(varName);
            if(typeName!=null)
            {
                if(oldVariable==null) {
                    newVariable = varDecls.Define(varName, typeName);
                } else if(oldVariable.Type=="") {
                    if(varDecls.WasImplicitelyDeclared(oldVariable)) 
                        throw new ParseException("The variable \""+varName+"\" has already been used/implicitely declared as global variable!");
                    else // it was explicitely used as global before, we are allowed to create a local variable with the same name, the global is (only) accessible with global prefix then
                        newVariable = varDecls.Define(varName, typeName);
                } else {
                    throw new ParseException("The variable \""+varName+"\" has already been declared as local variable with type \""+oldVariable.Type+"\"!");
                }
            }
            else
            {
                if(oldVariable==null) {
                    newVariable = varDecls.Define(varName, "");
                    warnings.Add("WARNING: using global variables without \"::\" prefix is deprecated, missing for: " + varName);
                } else {
                    if(oldVariable.Type=="")
                        warnings.Add("WARNING: using global variables without \"::\" prefix is deprecated, missing for: " + varName);
                    newVariable = oldVariable;
                }
            }
            return newVariable;
        }
    |
        "::" varName=Word()
        {
            return varDecls.LookupDefineGlobal(varName);
        }
    )
}

SequenceVariable VariableDefinition(): // only definition in contrast to Variable
{
    String varName, typeName;
}
{
    varName=Word() ":" typeName=Type()
    {
        SequenceVariable oldVariable = varDecls.Lookup(varName);
        SequenceVariable newVariable;
        if(oldVariable==null) {
            newVariable = varDecls.Define(varName, typeName);
        } else if(oldVariable.Type=="") {
            if(varDecls.WasImplicitelyDeclared(oldVariable)) 
                throw new ParseException("The variable \""+varName+"\" has already been used/implicitely declared as global variable!");
            else // it was explicitely used as global before, we are allowed to create a local variable with the same name, the global is (only) accessible with global prefix then
                newVariable = varDecls.Define(varName, typeName);
        } else {
            throw new ParseException("The variable \""+varName+"\" has already been declared as local variable with type \""+oldVariable.Type+"\"!");
        }
        return newVariable;
    }
}

SequenceVariable VariableUse(): // only usage in contrast to Variable()
{
    String varName;
    SequenceVariable oldVariable, newVariable;
}
{
    (
        varName=Word()
        {
            oldVariable = varDecls.Lookup(varName);
            if(oldVariable==null) {
                newVariable = varDecls.Define(varName, "");
                warnings.Add("WARNING: using global variables without \"::\" prefix is deprecated, missing for: " + varName);
            } else {
                if(oldVariable.Type=="")
                    warnings.Add("WARNING: using global variables without \"::\" prefix is deprecated, missing for: " + varName);
                newVariable = oldVariable;
            }
            return newVariable;
        }
    |
        "::" varName=Word()
        {
            return varDecls.LookupDefineGlobal(varName);
        }
    )
}

void VariableList(List<SequenceVariable> variables):
{
    SequenceVariable var;
}
{
    var=Variable() { variables.Add(var); } ("," var=Variable() { variables.Add(var); })*
}

void VariableDefinitionList(List<SequenceVariable> variables):
{
    SequenceVariable var;
}
{
    var=VariableDefinition() { variables.Add(var); } ("," var=VariableDefinition() { variables.Add(var); })*
}

String Type():
{
    String type;
    String typeParam, typeParamDst;
}
{ 
    (type=TypeNonGeneric()
    | LOOKAHEAD("set" "<" TypeNonGeneric() ">") "set" "<" typeParam=TypeNonGeneric() ">" { type = "set<"+typeParam+">"; }
        (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
    | LOOKAHEAD("map" "<" TypeNonGeneric() "," TypeNonGeneric() ">") "map" "<" typeParam=TypeNonGeneric() "," typeParamDst=TypeNonGeneric() ">" { type = "map<"+typeParam+","+typeParamDst+">"; }
        (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
    | LOOKAHEAD("array" "<" TypeNonGeneric() ">") "array" "<" typeParam=TypeNonGeneric() ">" { type = "array<"+typeParam+">"; }
        (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
    | LOOKAHEAD("deque" "<" TypeNonGeneric() ">") "deque" "<" typeParam=TypeNonGeneric() ">" { type = "deque<"+typeParam+">"; }
        (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at deque declaration, use d:deque<T> = deque<T>[] for initialization"); })?
    // for below: keep >= which is from generic type closing plus a following assignment, it's tokenized into '>=' if written without whitespace, we'll eat the >= at the assignment
    | LOOKAHEAD("set" "<" TypeNonGeneric() ">=") "set" "<" typeParam=TypeNonGeneric() { type = "set<"+typeParam+">"; }
        (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
    | LOOKAHEAD("map" "<" TypeNonGeneric() "," TypeNonGeneric() ">=") "map" "<" typeParam=TypeNonGeneric() "," typeParamDst=TypeNonGeneric() { type = "map<"+typeParam+","+typeParamDst+">"; }
        (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
    | LOOKAHEAD("array" "<" TypeNonGeneric() ">=") "array" "<" typeParam=TypeNonGeneric() { type = "array<"+typeParam+">"; }
        (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
    | LOOKAHEAD("deque" "<" TypeNonGeneric() ">=") "deque" "<" typeParam=TypeNonGeneric() { type = "deque<"+typeParam+">"; }
        (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at deque declaration, use d:deque<T> = deque<T>[] for initialization"); })?
    // the match type exists only for the loop variable of the for matches loop
    | LOOKAHEAD("match" "<" Word() ">") "match" "<" typeParam=Word() ">" { type = "match<"+typeParam+">"; }    
    )
    {
        return type;
    }
}

String TypeNonGeneric():
{
    String package=null, type;
}
{ 
    (LOOKAHEAD(2) package=Word() "::")? type=Word()
    {
        return package!=null ? package + "::" + type : type;
    }
}

Sequence XGRS():
{
    Sequence seq;
}
{
    seq=RewriteSequence() <EOF>
    {
        return seq;
    }
}

SequenceDefinition defXGRS():
{
    String name;
    List<SequenceVariable> inputVariables = new List<SequenceVariable>();
    List<SequenceVariable> outputVariables = new List<SequenceVariable>();
    Sequence seq;
}
{
    name=Word() ( "(" VariableDefinitionList(inputVariables) ")" )? ( ":" "(" VariableDefinitionList(outputVariables) ")" )?
        "{" seq=RewriteSequence() "}" <EOF>
    {
        return new SequenceDefinitionInterpreted(name, inputVariables.ToArray(), outputVariables.ToArray(), seq);
    }
}

/////////////////////////////////////////
// Extended rewrite sequence           //
// (lowest precedence operators first) //
/////////////////////////////////////////

Sequence RewriteSequence():
{
    Sequence seq, seq2;
    bool random = false, choice = false;
}
{
    seq=RewriteSequenceLazyOr()
    (
        LOOKAHEAD(3)
        (
            LOOKAHEAD(3)
            ("$" { random = true; } ("%" { choice = true; })?)? "<;" seq2=RewriteSequenceLazyOr()
            {
                seq = new SequenceThenLeft(seq, seq2, random, choice);
            }
        |
            ("$" { random = true; } ("%" { choice = true; })?)? ";>" seq2=RewriteSequenceLazyOr()
            {
                seq = new SequenceThenRight(seq, seq2, random, choice);
            }
        )
    )*
    {
        return seq;
    }
}

Sequence RewriteSequenceLazyOr():
{
    Sequence seq, seq2;
    bool random = false, choice = false;
}
{
    seq=RewriteSequenceLazyAnd()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "||" seq2=RewriteSequenceLazyAnd()
        {
            seq = new SequenceLazyOr(seq, seq2, random, choice);
        }
    )*
    {
        return seq;
    }
}

Sequence RewriteSequenceLazyAnd():
{
    Sequence seq, seq2;
    bool random = false, choice = false;
}
{
    seq=RewriteSequenceStrictOr()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "&&" seq2=RewriteSequenceStrictOr()
        {
            seq = new SequenceLazyAnd(seq, seq2, random, choice);
        }
    )*
    {
        return seq;
    }
}

Sequence RewriteSequenceStrictOr():
{
    Sequence seq, seq2;
    bool random = false, choice = false;
}
{
    seq=RewriteSequenceStrictXor()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "|" seq2=RewriteSequenceStrictXor()
        {
            seq = new SequenceStrictOr(seq, seq2, random, choice);
        }
    )*
    {
        return seq;
    }
}

Sequence RewriteSequenceStrictXor():
{
    Sequence seq, seq2;
    bool random = false, choice = false;
}
{
    seq=RewriteSequenceStrictAnd()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "^" seq2=RewriteSequenceStrictAnd()
        {
            seq = new SequenceXor(seq, seq2, random, choice);
        }
    )*
    {
        return seq;
    }
}

Sequence RewriteSequenceStrictAnd():
{
    Sequence seq, seq2;
    bool random = false, choice = false;
}
{
    seq=RewriteSequenceNeg()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "&" seq2=RewriteSequenceNeg()
        {
            seq = new SequenceStrictAnd(seq, seq2, random, choice);
        }
    )*
    {
        return seq;
    }
}

Sequence RewriteSequenceNeg():
{
    Sequence seq;
    SequenceVariable toVar;
}
{
    "!" seq=RewriteSequenceIteration()
        ( "=>" toVar=Variable() { return new SequenceAssignSequenceResultToVar(toVar, new SequenceNot(seq)); }
        | "|>" toVar=Variable() { return new SequenceOrAssignSequenceResultToVar(toVar, new SequenceNot(seq)); }
        | "&>" toVar=Variable() { return new SequenceAndAssignSequenceResultToVar(toVar, new SequenceNot(seq)); }
        | { return new SequenceNot(seq); }
        )
|
    seq=RewriteSequenceIteration()
        ( "=>" toVar=Variable() { return new SequenceAssignSequenceResultToVar(toVar, seq); }
        | "|>" toVar=Variable() { return new SequenceOrAssignSequenceResultToVar(toVar, seq); }
        | "&>" toVar=Variable() { return new SequenceAndAssignSequenceResultToVar(toVar, seq); }
        | { return seq; }
        )
}

Sequence RewriteSequenceIteration():
{
    Sequence seq;
    int minnum, maxnum = -1;
    bool maxspecified = false;
    bool maxstar = false;
}
{
    (
        seq=SimpleSequence()
        (
            "*"
            {
                seq = new SequenceIterationMin(seq, 0);
            }
        |
            "+"
            {
                seq = new SequenceIterationMin(seq, 1);
            }
        |
            "["
                (
                    minnum=Number()
                    (
                        ":"
                        (
                            maxnum=Number() { maxspecified = true; }
                        |
                            "*" { maxstar = true; }
                        )
                    )?
                |
                    "*" { minnum = 0; maxstar = true; }
                |
                    "+" { minnum = 1; maxstar = true; }
                )
            "]"
            {
                if(maxstar) {
                    seq = new SequenceIterationMin(seq, minnum);
                } else {
                    if(!maxspecified) maxnum = minnum;
                    seq = new SequenceIterationMinMax(seq, minnum, maxnum);
                }
            }
        )?
    )
    {
        return seq;
    }
}

Sequence SimpleSequence():
{
    bool special = false, choice = false, chooseRandSpecified = false;
    Sequence seq, seq2, seq3 = null;
    List<SequenceVariable> variableList1 = new List<SequenceVariable>();
    List<SequenceVariable> variableList2 = new List<SequenceVariable>();
    List<Sequence> sequences = new List<Sequence>();
    List<Double> numbers = new List<Double>();
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceVariable toVar, fromVar, fromVar2 = null, fromVar3 = null;
    SequenceExpression expr = null, expr2 = null, expr3 = null;
    SequenceComputation comp;
    int num = 0;
    RelOpDirection left = RelOpDirection.Undefined, right = RelOpDirection.Undefined;
    double numDouble = 0.0;
    String str, attrName = null, indexName = null, indexName2 = null;
    object constant;
}
{
    LOOKAHEAD(Variable() ("="|">="))
    toVar=Variable() ("="|">=")
    (
        LOOKAHEAD(Word() "(")
        Word() "(" // deliver understandable error message for case of missing parenthesis at rule result assignment
        {
            throw new ParseException("the destination variable(s) of a rule result assignment must be enclosed in parenthesis");
        }
    |
        LOOKAHEAD(Constant())
        constant=Constant()
        {
            return new SequenceAssignConstToVar(toVar, constant); // needed as sequence to allow variable declaration and initialization in sequence scope
        }
    |
        expr=InitContainerExpr()
        {
            return new SequenceAssignContainerConstructorToVar(toVar, expr); // needed as sequence to allow variable declaration and initialization in sequence scope
        }
    |
        fromVar=Variable()
        {
            return new SequenceAssignVarToVar(toVar, fromVar); // needed as sequence to allow variable declaration and initialization in sequence scope
        }
    |
        LOOKAHEAD(4) "$" "%" "(" str=Text() ")"
        {
            return new SequenceAssignUserInputToVar(toVar, str);
        }
    |
        LOOKAHEAD(4) "$" ("%" { choice = true; } )? "(" num=Number() ")" 
        {
            return new SequenceAssignRandomIntToVar(toVar, num, choice);
        }
    |
        "$" ("%" { choice = true; } )? "(" numDouble=DoubleNumber() ")" 
        {
            if(numDouble!=1.0)
                throw new ParseException("The random assignment of type double only supports 1.0 as upper bound");
            return new SequenceAssignRandomDoubleToVar(toVar, choice);
        }
    |
        "(" seq=RewriteSequence() ")"
        {
            return new SequenceAssignSequenceResultToVar(toVar, seq);
        }
    )
|
    LOOKAHEAD(VariableDefinition())
    toVar=VariableDefinition()
    {
        return new SequenceDeclareVariable(toVar);
    }
|
    "yield" toVar=VariableUse() "=" 
    (
        LOOKAHEAD(2)
        constant=Constant()
        {
            return new SequenceBooleanComputation(new SequenceComputationAssignment(new AssignmentTargetYieldingVar(toVar), new SequenceExpressionConstant(constant)), null, false);
        }
    |
        expr=InitContainerExpr()
        {
            return new SequenceBooleanComputation(new SequenceComputationAssignment(new AssignmentTargetYieldingVar(toVar), expr), null, false);
        }
    |
        fromVar=Variable()
        {
            return new SequenceBooleanComputation(new SequenceComputationAssignment(new AssignmentTargetYieldingVar(toVar), new SequenceExpressionVariable(fromVar)), null, false);
        }
    )
|
    LOOKAHEAD(2) ("%" { special = true; })? "true"
    {
        return new SequenceBooleanComputation(new SequenceExpressionConstant(true), null, special);
    }
|
    LOOKAHEAD(2) ("%" { special = true; })? "false"
    {
        return new SequenceBooleanComputation(new SequenceExpressionConstant(false), null, special);
    }
|
	LOOKAHEAD(3) seq=MultiRulePrefixedSequence()
	{
		return seq;
	}
|
    LOOKAHEAD(3) seq=MultiRuleAllCall(true)
    {
        return seq;
    }
|
	LOOKAHEAD(2) seq=RulePrefixedSequence()
	{
		return seq;
	}
|
    LOOKAHEAD(RuleLookahead())
    seq=Rule() // accepts variables, rules, all-bracketed rules, and counted all-bracketed rules
    {
        return seq;
    }
|
    "::" str=Word()
    {
        fromVar = varDecls.LookupDefineGlobal(str);
        return new SequenceBooleanComputation(new SequenceExpressionVariable(fromVar), null, false);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; } )?
        "||" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
    {
        return new SequenceLazyOrAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; } )?
        "&&" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
    {
        return new SequenceLazyAndAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; } )?
        "|" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
    {
        return new SequenceStrictOrAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; } )?
        "&" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
    {
        return new SequenceStrictAndAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; } )?
        "." "(" numDouble=DoubleNumber() seq=RewriteSequence() { numbers.Add(numDouble); sequences.Add(seq); } ("," numDouble=DoubleNumber() seq=RewriteSequence() { numbers.Add(numDouble); sequences.Add(seq); })* ")"
    {
        return new SequenceWeightedOne(sequences, numbers, choice);
    }
|
    LOOKAHEAD(3)
    ( "$" { chooseRandSpecified=true; } ("%" { choice = true; } )? )?
        "{" "<" seq=Rule() { sequences.Add(seq); } ("," seq=Rule() { sequences.Add(seq); })* ">" "}"
    {
        return new SequenceSomeFromSet(sequences, chooseRandSpecified, choice);
    }
|
    "(" seq=RewriteSequence() ")"
    {
        return seq;
    }
|
    "<" seq=RewriteSequence() ">"
    {
        return new SequenceTransaction(seq);
    }
|
    LOOKAHEAD(5)
    "<<" seq=MultiRulePrefixedSequence() ">>"
    {
        return new SequenceMultiSequenceBacktrack((SequenceMultiRulePrefixedSequence)seq);
    }
|
    LOOKAHEAD(3)
    "<<" { varDecls.PushScope(ScopeType.Backtrack); } seq=MultiRuleAllCall(false) (";;"|";") seq2=RewriteSequence() { varDecls.PopScope(variableList1); } ">>"
    {
        return new SequenceMultiBacktrack((SequenceMultiRuleAllCall)seq, seq2);
    }
|
    "<<" { varDecls.PushScope(ScopeType.Backtrack); } seq=Rule() (";;"|";") seq2=RewriteSequence() { varDecls.PopScope(variableList1); } ">>"
    {
        return new SequenceBacktrack(seq, seq2);
    }
|
    "/" seq=RewriteSequence() "/"
    {
        return new SequencePause(seq);
    }
|
    "if" "{" { varDecls.PushScope(ScopeType.If); } seq=RewriteSequence() ";"
        { varDecls.PushScope(ScopeType.IfThenPart); } seq2=RewriteSequence() { varDecls.PopScope(variableList2); }
        (";" seq3=RewriteSequence())? { varDecls.PopScope(variableList1); } "}"
    {
        if(seq3==null) return new SequenceIfThen(seq, seq2, variableList1, variableList2);
        else return new SequenceIfThenElse(seq, seq2, seq3, variableList1, variableList2);
    }
|
    "for" "{" { varDecls.PushScope(ScopeType.For); } fromVar=Variable()
    ( 
        LOOKAHEAD(3) "in" "[" "?" seq=Rule() "]" ";" seq2=RewriteSequence()
            { varDecls.PopScope(variableList1); } "}"
        {
            return new SequenceForMatch(fromVar, seq, seq2, variableList1);
        }
    |    
        LOOKAHEAD(3) "in" "{" 
            (
                LOOKAHEAD(2) indexName=Word() "==" expr=Expression() "}" ";" seq2=RewriteSequence()
                    { varDecls.PopScope(variableList1); } "}"
                {
                    return new SequenceForIndexAccessEquality(fromVar, indexName, expr, seq2, variableList1);
                }
            |
                str=Word() "(" indexName=Word() 
                    (left=RelationOp() expr=Expression() 
                        ("," indexName2=Word() right=RelationOp() expr2=Expression())? 
                    )? ")" "}" ";" seq2=RewriteSequence()
                    { varDecls.PopScope(variableList1); } "}"
                {
                    bool ascending;
                    if(str=="ascending")
                        ascending = true;
                    else if(str=="descending")
                        ascending = false;
                    else
                        throw new SequenceParserException(str, SequenceParserError.UnknownIndexAccessDirection);
                    if(indexName2!=null)
                        if(indexName!=indexName2)
                            throw new SequenceParserException(indexName, SequenceParserError.TwoDifferentIndexNames);
                    return new SequenceForIndexAccessOrdering(fromVar, ascending, indexName, expr, left, expr2, right, seq2, variableList1);
                }
            )
    |
        LOOKAHEAD(3) "in" str=Word() "(" (Arguments(argExprs))? ")" ";" seq=RewriteSequence()
            { varDecls.PopScope(variableList1); } "}"
        {
            if(str=="adjacent") {
                return new SequenceForFunction(SequenceType.ForAdjacentNodes, fromVar, argExprs, seq, variableList1);
            } else if(str=="adjacentIncoming") {
                return new SequenceForFunction(SequenceType.ForAdjacentNodesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str=="adjacentOutgoing") {
                return new SequenceForFunction(SequenceType.ForAdjacentNodesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str=="incident") {
                return new SequenceForFunction(SequenceType.ForIncidentEdges, fromVar, argExprs, seq, variableList1);
            } else if(str=="incoming") {
                return new SequenceForFunction(SequenceType.ForIncomingEdges, fromVar, argExprs, seq, variableList1);
            } else if(str=="outgoing") {
                return new SequenceForFunction(SequenceType.ForOutgoingEdges, fromVar, argExprs, seq, variableList1);
            } else if(str=="reachable") {
                return new SequenceForFunction(SequenceType.ForReachableNodes, fromVar, argExprs, seq, variableList1);
            } else if(str=="reachableIncoming") {
                return new SequenceForFunction(SequenceType.ForReachableNodesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str=="reachableOutgoing") {
                return new SequenceForFunction(SequenceType.ForReachableNodesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str=="reachableEdges") {
                return new SequenceForFunction(SequenceType.ForReachableEdges, fromVar, argExprs, seq, variableList1);
            } else if(str=="reachableEdgesIncoming") {
                return new SequenceForFunction(SequenceType.ForReachableEdgesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str=="reachableEdgesOutgoing") {
                return new SequenceForFunction(SequenceType.ForReachableEdgesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str=="boundedReachable") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableNodes, fromVar, argExprs, seq, variableList1);
            } else if(str=="boundedReachableIncoming") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableNodesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str=="boundedReachableOutgoing") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableNodesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str=="boundedReachableEdges") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableEdges, fromVar, argExprs, seq, variableList1);
            } else if(str=="boundedReachableEdgesIncoming") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableEdgesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str=="boundedReachableEdgesOutgoing") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableEdgesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str=="nodes") {
                return new SequenceForFunction(SequenceType.ForNodes, fromVar, argExprs, seq, variableList1);
            } else if(str=="edges") {
                return new SequenceForFunction(SequenceType.ForEdges, fromVar, argExprs, seq, variableList1);
            }
        }
    | 
        LOOKAHEAD(3)
        ("->" fromVar2=Variable())? "in" fromVar3=VariableUse() ";" seq=RewriteSequence()
            { varDecls.PopScope(variableList1); } "}"
        {
            return new SequenceForContainer(fromVar, fromVar2, fromVar3, seq, variableList1);
        }
    |
        "in" "[" expr=Expression() ":" expr2=Expression() "]" ";" seq=RewriteSequence()
            { varDecls.PopScope(variableList1); } "}"
        {
            return new SequenceForIntegerRange(fromVar, expr, expr2, seq, variableList1);
        }
    )
|
    "in" toVar=VariableUse() ("." attrName=Word())? "{" { varDecls.PushScope(ScopeType.InSubgraph); } seq=RewriteSequence() { varDecls.PopScope(variableList1); } "}"
    {
        return new SequenceExecuteInSubgraph(toVar, attrName, seq);
    }
|
    ("%" { special = true; })? "{" { varDecls.PushScope(ScopeType.Computation); } comp=CompoundComputation() { varDecls.PopScope(variableList1); } (";")? "}"
    {
        return new SequenceBooleanComputation(comp, variableList1, special);
    }
}

SequenceComputation CompoundComputation():
{
    SequenceComputation comp, compRight;
}
{
    comp=Computation() (";" compRight=CompoundComputation() { return new SequenceComputationThen(comp, compRight); } | { return comp; })
}

SequenceComputation Computation():
{
    SequenceVariable toVar;
    SequenceExpression expr;
    SequenceComputation comp, assignOrExpr;
    AssignmentTarget tgt;
    String procedure;
}
{
    // this is a special case of the special case solution to accept e.g. s:set<int>= as s:set<int> = and not s:set<int >= which is what the lexer gives
    // it is not correct, I just assume that one doesn't want to compare a just defined but not assigned variable to something, 
    // so it's pretty safe to assume it's a set/map/array/deque declaration with the ">=" != ">""=" issue
    LOOKAHEAD(VariableDefinition() ">=")
    toVar=VariableDefinition() ">=" assignOrExpr=ExpressionOrAssignment()
    {
        return new SequenceComputationAssignment(new AssignmentTargetVar(toVar), assignOrExpr);
    }
|
    LOOKAHEAD(AssignmentTarget() "=")
    tgt=AssignmentTarget() "=" assignOrExpr=ExpressionOrAssignment()
    {
        return new SequenceComputationAssignment(tgt, assignOrExpr);
    }
|
    LOOKAHEAD(2)
    toVar=VariableDefinition()
    {
        return new SequenceComputationVariableDeclaration(toVar);
    }
|
    comp=ProcedureOrMethodCall()
    {
        return comp;
    }
|
    "{" expr=Expression() "}"
    {
        return expr;
    }
}

AssignmentTarget AssignmentTarget():
{
    SequenceVariable toVar;
    AssignmentTarget target;
}
{
    "yield" toVar=VariableUse()
    {
        return new AssignmentTargetYieldingVar(toVar);
    }
|
    LOOKAHEAD(2) toVar=VariableDefinition()
    {
        return new AssignmentTargetVar(toVar);
    }
|
    toVar=VariableUse() target=AssignmentTargetSelector(toVar)
    {
        return target;
    }
}

AssignmentTarget AssignmentTargetSelector(SequenceVariable toVar):
{
    SequenceExpression fromExpr;
    String attrName;
}
{
    LOOKAHEAD(2) "." attrName=Word()
    ( "[" fromExpr=Expression() "]" 
        { return new AssignmentTargetAttributeIndexed(toVar, attrName, fromExpr); }
    )? // todo: this should be a composition of the two targets, not a fixed special one
    {
        return new AssignmentTargetAttribute(toVar, attrName);
    }
|
    "." "visited" "[" fromExpr=Expression() "]"
    {
        return new AssignmentTargetVisited(toVar, fromExpr);
    }
|
    "[" fromExpr=Expression() "]"
    {
        return new AssignmentTargetIndexedVar(toVar, fromExpr);
    }
|
    {
        return new AssignmentTargetVar(toVar);
    }
}

SequenceComputation ExpressionOrAssignment():
{
    SequenceExpression expr;
    SequenceComputation assignOrExpr;
    AssignmentTarget tgt;
    SequenceVariable toVar;
    String function;
}
{
    (
        // special case handling for ">=" != ">""="
        LOOKAHEAD(VariableDefinition() ">=")
        toVar=VariableDefinition() ">=" assignOrExpr=ExpressionOrAssignment()
        {
            return new SequenceComputationAssignment(new AssignmentTargetVar(toVar), assignOrExpr);
        }
    |
        LOOKAHEAD(AssignmentTarget() "=")
        tgt=AssignmentTarget() "=" assignOrExpr=ExpressionOrAssignment()
        {
            return new SequenceComputationAssignment(tgt, assignOrExpr);
        }    
    |
        expr=Expression()
        {
            return expr;
        }
    )
}

SequenceExpression Expression():
{
    SequenceExpression seq, seq2, seq3;
}
{
    seq=ExpressionLazyOr() ( "?" seq2=Expression() ":" seq3=Expression() { seq = new SequenceExpressionConditional(seq, seq2, seq3); } )? { return seq; }
}

SequenceExpression ExpressionLazyOr():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionLazyAnd() ( "||" seq2=ExpressionLazyAnd() { seq = new SequenceExpressionLazyOr(seq, seq2); } )* { return seq; }
}

SequenceExpression ExpressionLazyAnd():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionStrictOr() ( "&&" seq2=ExpressionStrictOr() { seq = new SequenceExpressionLazyAnd(seq, seq2); } )* { return seq; }
}

SequenceExpression ExpressionStrictOr():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionStrictXor() ( "|" seq2=ExpressionStrictXor() { seq = new SequenceExpressionStrictOr(seq, seq2); } )* { return seq; }
}

SequenceExpression ExpressionStrictXor():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionStrictAnd() ( "^" seq2=ExpressionStrictAnd() { seq = new SequenceExpressionStrictXor(seq, seq2); } )* { return seq; }
}

SequenceExpression ExpressionStrictAnd():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionEquality() ( "&" seq2=ExpressionEquality() { seq = new SequenceExpressionStrictAnd(seq, seq2); } )* { return seq; }
}

SequenceExpression ExpressionEquality():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionRelation() ( "==" seq2=ExpressionRelation() { seq = new SequenceExpressionEqual(seq, seq2); } 
                             | "!=" seq2=ExpressionRelation() { seq = new SequenceExpressionNotEqual(seq, seq2); }
                             | "~~" seq2=ExpressionRelation() { seq = new SequenceExpressionStructuralEqual(seq, seq2); }
                             )*
    { return seq; }
}

SequenceExpression ExpressionRelation():
{
    SequenceExpression seq, seq2;
    SequenceVariable fromVar;
    String attrName;
}
{
    seq=ExpressionAdd() ( "<" seq2=ExpressionAdd() { seq = new SequenceExpressionLower(seq, seq2); }
                        | ">" seq2=ExpressionAdd() { seq = new SequenceExpressionGreater(seq, seq2); }
                        | "<=" seq2=ExpressionAdd() { seq = new SequenceExpressionLowerEqual(seq, seq2); }
                        | ">=" seq2=ExpressionAdd() { seq = new SequenceExpressionGreaterEqual(seq, seq2); }
                        | "in" seq2=ExpressionAdd() { seq = new SequenceExpressionInContainer(seq, seq2); }
                        )* 
    { return seq; }
}

RelOpDirection RelationOp():
{
    RelOpDirection result = RelOpDirection.Undefined;
}
{
    ( "<" { result = RelOpDirection.Smaller; }
    | ">" { result = RelOpDirection.Greater; }
    | "<=" { result = RelOpDirection.SmallerEqual; }
    | ">=" { result = RelOpDirection.GreaterEqual; }
    )
    { return result; }
}

SequenceExpression ExpressionAdd():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionMul() ( "+" seq2=ExpressionMul() { seq = new SequenceExpressionPlus(seq, seq2); } 
                          | "-" seq2=ExpressionMul() { seq = new SequenceExpressionMinus(seq, seq2); }
                          )*
    { return seq; }
}

SequenceExpression ExpressionMul():
{
    SequenceExpression seq, seq2;
}
{
    seq=ExpressionUnary() ( "*" seq2=ExpressionUnary() { seq = new SequenceExpressionMul(seq, seq2); } 
                          | "/" seq2=ExpressionUnary() { seq = new SequenceExpressionDiv(seq, seq2); }
                          | "%" seq2=ExpressionUnary() { seq = new SequenceExpressionMod(seq, seq2); }
                          )*
    { return seq; }
}

SequenceExpression ExpressionUnary():
{
    SequenceExpression seq;
    object type;
}
{
    LOOKAHEAD("(" Constant() ")") "(" type=Constant() ")" seq=ExpressionBasic() { return new SequenceExpressionCast(seq, type); }
    | "!" seq=ExpressionBasic() { return new SequenceExpressionNot(seq); }
    | "-" seq=ExpressionBasic() { return new SequenceExpressionUnaryMinus(seq); }
    | seq=ExpressionBasic() { return seq; }
}

SequenceExpression ExpressionBasic():
{
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceVariable fromVar;
    String elemName;
    SequenceExpression expr;
    object constant;
}
{
    LOOKAHEAD(3)
    expr=RuleQuery() expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    LOOKAHEAD(FunctionCall())
    expr=FunctionCall()
    {
        return expr;
    }
|
    LOOKAHEAD(2)
    constant=Constant()
    {
        return new SequenceExpressionConstant(constant);
    }
|
    expr=InitContainerExpr() expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    "def" "(" Arguments(argExprs) ")"
    {
        return new SequenceExpressionDef(argExprs.ToArray());
    }
|
    fromVar=VariableUse() { expr = new SequenceExpressionVariable(fromVar); } expr=SelectorExpression(expr)
    {    
        return expr;
    }
|
    "@" "(" elemName=Text() ")"
    {
        return new SequenceExpressionElementFromGraph(elemName);
    }
|
    "this" { expr = new SequenceExpressionThis(env.RuleOfMatchThis, env.TypeOfGraphElementThis); } expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    "(" expr=Expression() ")"
    {
        return expr;
    }
}

SequenceExpression SelectorExpression(SequenceExpression fromExpr):
{
    String methodOrAttrName;
    SequenceExpression expr = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
}
{
    LOOKAHEAD(2)
    "." methodOrAttrName=Word()
    (
        "(" (Arguments(argExprs))? ")"
            { expr = env.CreateSequenceExpressionFunctionMethodCall(fromExpr, methodOrAttrName, argExprs); }
    |
        { expr = new SequenceExpressionAttributeOrMatchAccess(fromExpr, methodOrAttrName); }
    )
    expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    "." "visited" "[" expr=Expression() "]"
        { expr = new SequenceExpressionIsVisited(fromExpr, expr); }
    expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    "[" expr=Expression() "]"
        { expr = new SequenceExpressionContainerAccess(fromExpr, expr); }
    expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    {
        return fromExpr;
    }
}

SequenceComputation ProcedureOrMethodCall():
{
    String procedure, attrName = null, package = null;
    SequenceVariable fromVar = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<SequenceVariable> returnVars = new List<SequenceVariable>();
}
{
    ("(" VariableList(returnVars) ")" "=" )?
        (LOOKAHEAD(2) package=Word() "::")? (LOOKAHEAD(2) fromVar=VariableUse() "." (LOOKAHEAD(2) attrName=Word() ".")?)? 
        procedure=Word() "(" (Arguments(argExprs))? ")"
    {
        if(fromVar==null) // procedure call
        {
            return env.CreateSequenceComputationProcedureCall(procedure, package, argExprs, returnVars);
        }
        else
        { // method call
            if(attrName==null)
            {
                return env.CreateSequenceComputationProcedureMethodCall(fromVar, procedure, argExprs, returnVars);
            } else { // attribute method call
                SequenceExpressionAttributeAccess attrAcc = new SequenceExpressionAttributeAccess(new SequenceExpressionVariable(fromVar), attrName);
                return env.CreateSequenceComputationProcedureMethodCall(attrAcc, procedure, argExprs, returnVars);
            }
        }
    }
}

SequenceExpression FunctionCall():
{
    String function, package = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
}
{
    (LOOKAHEAD(2) package=Word() "::")? 
    function=Word() "(" (Arguments(argExprs))? ")"
    {
        return env.CreateSequenceExpressionFunctionCall(function, package, argExprs);
    }
}

Sequence RulePrefixedSequence():
{
    SequenceRuleCall rule;
    Sequence seq;
    List<SequenceVariable> variableList = new List<SequenceVariable>();
}
{
    "[" "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMultiRuleAllCall(false) ";" seq=RewriteSequence() { varDecls.PopScope(variableList); } "}" "]"
    { return new SequenceRulePrefixedSequence(rule, seq, variableList); }
}

Sequence MultiRulePrefixedSequence():
{
    SequenceRuleCall rule;
    Sequence seq;
    List<SequenceVariable> variableList = new List<SequenceVariable>();
    List<SequenceRulePrefixedSequence> rulePrefixedSequences = new List<SequenceRulePrefixedSequence>();
    SequenceMultiRulePrefixedSequence seqMultiRulePrefixedSequence;
    SequenceFilterCall filter = null;
}
{
    "[" "[" "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMultiRuleAllCall(false) ";" seq=RewriteSequence() { varDecls.PopScope(variableList); } { rulePrefixedSequences.Add(new SequenceRulePrefixedSequence(rule, seq, variableList)); } "}"
        ("," "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMultiRuleAllCall(false) ";" seq=RewriteSequence() { varDecls.PopScope(variableList); } { rulePrefixedSequences.Add(new SequenceRulePrefixedSequence(rule, seq, variableList)); } "}")*  "]" "]"
    { seqMultiRulePrefixedSequence = new SequenceMultiRulePrefixedSequence(rulePrefixedSequences); }
        ("\\" filter=Filter(null, true) { seqMultiRulePrefixedSequence.AddFilterCall(filter); })*
    { return seqMultiRulePrefixedSequence; }
}

Sequence MultiRuleAllCall(bool returnsArrays):
{
    Sequence seq;
    List<Sequence> sequences = new List<Sequence>();
    SequenceMultiRuleAllCall seqMultiRuleAll;
    SequenceFilterCall filter = null;
}
{
    "[" "[" seq=RuleForMultiRuleAllCall(returnsArrays) { sequences.Add(seq); } ("," seq=RuleForMultiRuleAllCall(returnsArrays) { sequences.Add(seq); })* "]" "]"
    { seqMultiRuleAll = new SequenceMultiRuleAllCall(sequences); }
        ("\\" filter=Filter(null, true) { seqMultiRuleAll.AddFilterCall(filter); })*
    { return seqMultiRuleAll; }
}

SequenceRuleCall RuleForMultiRuleAllCall(bool returnsArrays):
{
    bool special = false, test = false;
    String str, package = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<SequenceVariable> returnVars = new List<SequenceVariable>();
    SequenceRuleCall ruleCall;
    SequenceFilterCall filter = null;
}
{
    ("(" VariableList(returnVars) ")" "=" )?
    (
        (LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } | "%" { special = true; })?
        (LOOKAHEAD(2) package=Word() "::")? 
        str=Word() ("(" (Arguments(argExprs))? ")")?
        {
            // No variable with this name may exist
            if(varDecls.Lookup(str)!=null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            ruleCall = env.CreateSequenceRuleCall(str, package, argExprs, returnVars, null,
                special, test, returnsArrays);
        }
            ("\\" filter=Filter(ruleCall, false) { ruleCall.AddFilterCall(filter); })*
            {
                return ruleCall;
            }
    )
}

void RuleLookahead():
{
}
{
    ("(" ( Word() (":" (TypeNonGeneric() | "set" "<" TypeNonGeneric() ">" | "map" "<" TypeNonGeneric() "," TypeNonGeneric() ">" | "array" "<" TypeNonGeneric() ">" | "deque" "<" TypeNonGeneric() ">"))? | "::" Word() ) 
            ("," ( Word() (":" (TypeNonGeneric() | "set" "<" TypeNonGeneric() ">" | "map" "<" TypeNonGeneric() "," TypeNonGeneric() ">" | "array" "<" TypeNonGeneric() ">" | "deque" "<" TypeNonGeneric() ">"))? | "::" Word() ) )* ")" "=")?
    (
        ( "$" ("%")? ( Variable() ("," (Variable() | "*"))? )? )? "["
    |
        (LOOKAHEAD(2) "?" "%" | LOOKAHEAD(2) "%" "?" | "?" | "%")? (LOOKAHEAD(2) Word() |  Variable() ".")
    |
        "count" "["
    )
}

Sequence Rule():
{
    bool special = false, test = false;
    String str, package = null;
    bool chooseRandSpecified = false, chooseRandSpecified2 = false, choice = false;
    SequenceVariable varChooseRand = null, varChooseRand2 = null, subgraph = null, countResult = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<SequenceVariable> returnVars = new List<SequenceVariable>();
    SequenceRuleAllCall ruleAllCall = null;
    SequenceRuleCountAllCall ruleCountAllCall = null;
    SequenceRuleCall ruleCall = null;
    SequenceSequenceCall sequenceCall = null;
    SequenceFilterCall filter = null;
}
{
    ("(" VariableList(returnVars) ")" "=" )?
    (
        (
            "$" ("%" { choice = true; })? ( varChooseRand=Variable() ("," (varChooseRand2=Variable() | "*") { chooseRandSpecified2 = true; })? )? { chooseRandSpecified = true; }
        )?
        "[" (LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } | "%" { special = true; })? 
        (LOOKAHEAD(2) subgraph=Variable() ".")? (LOOKAHEAD(2) package=Word() "::")?
        str=Word() ("(" (Arguments(argExprs))? ")")?
        {
            // No variable with this name may exist
            if(varDecls.Lookup(str)!=null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            ruleAllCall = env.CreateSequenceRuleAllCall(str, package, argExprs, returnVars, subgraph,
                    special, test, chooseRandSpecified, varChooseRand, chooseRandSpecified2, varChooseRand2, choice);
        }
            ("\\" filter=Filter(ruleAllCall, false) { ruleAllCall.AddFilterCall(filter); })*
        "]"
        {
            return ruleAllCall;
        }

    |
        "count"
        "[" (LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } | "%" { special = true; })? 
        (LOOKAHEAD(2) subgraph=Variable() ".")? (LOOKAHEAD(2) package=Word() "::")?
        str=Word() ("(" (Arguments(argExprs))? ")")?
        {
            // No variable with this name may exist
            if(varDecls.Lookup(str)!=null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            ruleCountAllCall = env.CreateSequenceRuleCountAllCall(str, package, argExprs, returnVars, subgraph,
                    special, test);
        }
            ("\\" filter=Filter(ruleCountAllCall, false) { ruleCountAllCall.AddFilterCall(filter); })*
        "]" "=>" countResult=Variable() 
        {
            ruleCountAllCall.AddCountResult(countResult);
            return ruleCountAllCall;
        }
    |
        (LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } | "%" { special = true; })?
        (LOOKAHEAD(2) subgraph=Variable() ".")? (LOOKAHEAD(2) package=Word() "::")?
        str=Word() ("(" (Arguments(argExprs))? ")")? // if only str is given, this might be a variable predicate; but this is decided later on in resolve
        {
            if(argExprs.Count==0 && returnVars.Count==0)
            {
                SequenceVariable var = varDecls.Lookup(str);
                if(var!=null)
                {
                    if(var.Type!="" && var.Type!="boolean")
                        throw new SequenceParserException(str, "untyped or bool", var.Type);
                    if(subgraph!=null)
                        throw new SequenceParserException(str, "", SequenceParserError.SubgraphError);
                    return new SequenceBooleanComputation(new SequenceExpressionVariable(var), null, special);
                }
            }

            // No variable with this name may exist
            if(varDecls.Lookup(str)!=null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            if(env.IsSequenceName(str, package)) {
                sequenceCall = env.CreateSequenceSequenceCall(str, package, argExprs, returnVars, subgraph,
                                special);
            } else {
                ruleCall = env.CreateSequenceRuleCall(str, package, argExprs, returnVars, subgraph,
                                special, test, false);
            }
        }
            ("\\" filter=Filter(ruleCall, false)
                {
                    if(varDecls.Lookup(str) != null)
                        throw new SequenceParserException(str, filter.ToString(), SequenceParserError.FilterError);
                    if(sequenceCall != null) {
                        List<SequenceFilterCall> filters = new List<SequenceFilterCall>();
                        filters.Add(filter);
                        throw new SequenceParserException(str, FiltersToString(filters), SequenceParserError.FilterError);
                    }
                    ruleCall.AddFilterCall(filter);
                }
            )*
            {
                if(sequenceCall != null)
                    return sequenceCall;
                else
                    return ruleCall;
            }
    )
}

SequenceExpression RuleQuery():
{
    bool special = false, test = false;
    String str, package = null;
    SequenceVariable subgraph = null; // maybe todo - remove
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceRuleAllCall ruleAllCall = null;
    SequenceRuleCall ruleCall = null;
    SequenceFilterCall filter = null;
}
{
    "[" ( LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } )
    (LOOKAHEAD(2) subgraph=Variable() ".")? (LOOKAHEAD(2) package=Word() "::")?
    str=Word() ("(" (Arguments(argExprs))? ")")?
    {
        ruleAllCall = env.CreateSequenceRuleAllCall(str, package, argExprs, new List<SequenceVariable>(), subgraph,
                special, test, false, null, false, null, false);
    }
        ("\\" filter=Filter(ruleAllCall, false) { ruleAllCall.AddFilterCall(filter); })*
    "]"
    {
        return new SequenceExpressionRuleQuery(ruleAllCall);
    }
}

SequenceFilterCall Filter(SequenceRuleCall ruleCall, bool isMatchClassFilter) :
{
    String filterBase, package = null, matchClass = null, matchClassPackage = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<String> words = new List<String>();
}
{
    LOOKAHEAD(6) (LOOKAHEAD(4) (LOOKAHEAD(2) matchClassPackage=Word() "::")? matchClass=Word() ".")? filterBase=Word() "<" WordList(words) ">"
        {
            if(isMatchClassFilter && matchClass==null)
                throw new ParseException("A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");
            if(!isMatchClassFilter && matchClass!=null)
                throw new ParseException("A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

            if(!env.IsAutoGeneratedBaseFilterName(filterBase))
                throw new ParseException("Unknown def-variable-based filter " + filterBase + "! Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach.");
            else
            {
                if(matchClass!=null)
                    return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words, argExprs);
                else
                    return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words, argExprs);
            }
        }
|
    (LOOKAHEAD(4) (LOOKAHEAD(2) matchClassPackage=Word() "::")? matchClass=Word() ".")? (LOOKAHEAD(2) package=Word() "::")? filterBase=Word() ("(" (Arguments(argExprs))? ")")?
        {
            if(isMatchClassFilter && matchClass==null)
                throw new ParseException("A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");
            if(!isMatchClassFilter && matchClass!=null)
                throw new ParseException("A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

            if(env.IsAutoSuppliedFilterName(filterBase))
            {
                if(argExprs.Count!=1)
                    throw new ParseException("The auto-supplied filter " + filterBase + " expects exactly one parameter!");

                if(matchClass!=null)
                    return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words, argExprs);
                else
                    return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words, argExprs);
            }
            else
            {
                if(filterBase=="auto")
                {
                    if(isMatchClassFilter || matchClass!=null)
                        throw new ParseException("The auto filter is not available for multi rule call or multi rule backtracking constructs.");

                    return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, "auto", words, argExprs);
                }

                if(matchClass!=null)
                    return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words, argExprs);
                else
                    return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words, argExprs);
            }
        }
}

void WordList(List<String> words) :
{
    String word;
}
{
    word=Word() { words.Add(word); } ("," word=Word() { words.Add(word); })*
}


CSHARPCODE
String RemoveTypeSuffix(String value)
{
    if (value.EndsWith("y") || value.EndsWith("Y")
        || value.EndsWith("s") || value.EndsWith("S")
        || value.EndsWith("l") || value.EndsWith("L"))
        return value.Substring(0, value.Length - 1);
    else
        return value;
}

CSHARPCODE
String FiltersToString(List<SequenceFilterCall> filters)
{
    StringBuilder sb = new StringBuilder();
    bool first = true;
    for(int i=0; i<filters.Count; ++i)
    {
        if(first)
            first = false;
        else
            sb.Append("\\");
        sb.Append(filters[i].ToString());
    }
    return sb.ToString();
}

TOKEN: { < ERROR: ~[] > }
