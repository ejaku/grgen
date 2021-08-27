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
        /// Used for expression evaluation, and the interpreted if clauses for conditional watchpoint debugging.
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
|   < BITWISECOMPLEMENT: "~" >
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
|   < NEW: "new" >
|   < NULL: "null" >
|   < FOR: "for" >
|   < IF: "if" >
|   < IN: "in" >
|   < VISITED: "visited" >
|   < YIELD: "yield" >
|   < COUNT: "count" >
|   < THIS: "this" >
|   < CLASS: "class" >
|   < SCAN: "scan" >
|   < TRYSCAN: "tryscan" >
}

TOKEN: {
    < NUMFLOAT:
            (["0"-"9"])+ ("." (["0"-"9"])+)? (<EXPONENT>)? ["f", "F"]
        |    "." (["0"-"9"])+ (<EXPONENT>)? ["f", "F"]
    >
|
    < NUMDOUBLE:
            (["0"-"9"])+ "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
        |    "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
        |    (["0"-"9"])+ <EXPONENT> (["d", "D"])?
        |    (["0"-"9"])+ ["d", "D"]
    >
|
    < #EXPONENT: ["e", "E"] (["+", "-"])? (["0"-"9"])+ >
|
    < NUMBER: (["0"-"9"])+ >
|   < NUMBER_BYTE: (["0"-"9"])+ ("y"|"Y") >
|   < NUMBER_SHORT: (["0"-"9"])+ ("s"|"S") >
|   < NUMBER_LONG: (["0"-"9"])+ ("l"|"L") >
|
    < HEXNUMBER: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ >
|   < HEXNUMBER_BYTE: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("y"|"Y") >
|   < HEXNUMBER_SHORT: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("s"|"S") >
|   < HEXNUMBER_LONG: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("l"|"L") >
|
    < DOUBLEQUOTEDTEXT: "\"" ("\\\"" | ~["\"", "\n", "\r"])* "\"" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|
    < SINGLEQUOTEDTEXT: "\'" ("\\\'" | ~["\'", "\n", "\r"])* "\'" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|
    < WORD: ["A"-"Z", "a"-"z", "_"] (["A"-"Z", "a"-"z", "_", "0"-"9"])* >
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
}

object NumberOrHexNumber():
{
    Token tok;
}
{
    tok=<NUMBER>
    {
        return Convert.ToInt32(tok.image);
    }
| 
    tok=<NUMBER_BYTE>
    {
        return Convert.ToSByte(RemoveTypeSuffix(tok.image));
    }
|
    tok=<NUMBER_SHORT>
    {
        return Convert.ToInt16(RemoveTypeSuffix(tok.image));
    }
|
    tok=<NUMBER_LONG>
    {
        return Convert.ToInt64(RemoveTypeSuffix(tok.image));
    }
|
    tok=<HEXNUMBER>
    {
        return Int32.Parse(tok.image.Substring("0x".Length), System.Globalization.NumberStyles.HexNumber);
    }
|
    tok=<HEXNUMBER_BYTE>
    {
        return SByte.Parse(RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber);
    }
|
    tok=<HEXNUMBER_SHORT>
    {
        return Int16.Parse(RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber);
    }
|
    tok=<HEXNUMBER_LONG>
    {
        return Int64.Parse(RemoveTypeSuffix(tok.image.Substring("0x".Length)), System.Globalization.NumberStyles.HexNumber);
    }
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
        constant=NumberOrHexNumber()
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
            attrType = TypesHelper.GetEnumAttributeType(package + "::" + type, env.Model);
            if(attrType != null)
                constant = Enum.Parse(attrType.EnumType, value);
            if(constant == null)
                throw new ParseException("Invalid constant \"" + package + "::" + type + "::" + value + "\"!");
        }
    |
        LOOKAHEAD(2)
        packageOrType=Word() "::" typeOrValue=Word()
        {
            package = packageOrType;
            type = typeOrValue;
            constant = TypesHelper.GetInheritanceType(package + "::" + type, env.Model);
            if(constant == null)
            {
                type = packageOrType;
                value = typeOrValue;
                attrType = TypesHelper.GetEnumAttributeType(type, env.Model);
                if(attrType != null)
                    constant = Enum.Parse(attrType.EnumType, value);
            }
            if(constant == null)
                throw new ParseException("Invalid constant \"" + packageOrType + "::" + typeOrValue + "\"!");
        }
    |
        LOOKAHEAD({ GetToken(1).kind == WORD && varDecls.Lookup(GetToken(1).image) == null
                && TypesHelper.GetInheritanceType(GetToken(1).image, env.Model) != null })
        type=Word()
        {
            constant = TypesHelper.GetInheritanceType(type, env.Model);
        }
    )
    {
        return constant;
    }
}

SequenceExpression InitMatchClassExpr():
{
    SequenceExpression res = null;
}
{
    ("new")? res=InitMatchClassExprCont()
    {
        return res;
    }
}

SequenceExpression InitMatchClassExprCont():
{
    string typeName;
    string matchClassPackage = null;
    string matchClassName = null;
}
{
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "match" })
    Word() "<" "class" (LOOKAHEAD(2) matchClassPackage=Word() "::")? matchClassName=Word() ">" "(" ")"
    {
        return new SequenceExpressionMatchClassConstructor(env.GetPackagePrefixedMatchClassName(matchClassName, matchClassPackage));
    }
}

SequenceExpression InitObjectExpr():
{
    String type;
    SequenceExpression expr = null;
}
{
    "new" type=TypeNonGeneric() expr=ExpressionInitObjectCont(type)
    {
        return expr;
    }
}

SequenceExpression InitContainerExpr():
{
    SequenceExpression res = null;
}
{
    ("new")? res=InitContainerExprCont()
    {
        return res;
    }
}

SequenceExpression InitContainerExprCont():
{
    string typeName, typeNameDst;
    List<SequenceExpression> srcItems = null;
    List<SequenceExpression> dstItems = null;
    SequenceExpression src = null, dst = null, res = null, value = null;
}
{
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "set" })
    (
        LOOKAHEAD(Word() "<" TypeNonGeneric() ">") Word() "<" typeName=TypeNonGeneric() ">"
    |
        LOOKAHEAD(Word() "<" MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(3).kind == WORD && GetToken(3).image == "match" })
        Word() "<" typeName=MatchTypeInContainerType() (">" ">" | ">>")
    )
        { srcItems = new List<SequenceExpression>(); }
    (
        "{"
            (src=Expression() { srcItems.Add(src); }
                ( "," src=Expression() { srcItems.Add(src); } )*)?
        "}"
        {
            return new SequenceExpressionSetConstructor(typeName, srcItems.ToArray());
        }
    |
        "("
            value=Expression()
        ")"
        {
            return new SequenceExpressionSetCopyConstructor(typeName, value);
        }
    )
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "map" })
    (
        LOOKAHEAD(Word() "<" TypeNonGeneric() "," TypeNonGeneric() ">") Word() "<" typeName=TypeNonGeneric() "," typeNameDst=TypeNonGeneric() ">"
    |
        LOOKAHEAD(Word() "<" Word() "," MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(5).kind == WORD && GetToken(5).image == "match" }) 
        Word() "<" typeName=Word() "," typeNameDst=MatchTypeInContainerType() (">" ">" | ">>")
    )
        { srcItems = new List<SequenceExpression>(); dstItems = new List<SequenceExpression>(); }
    (
        "{"
            (src=Expression() "->" dst=Expression() { srcItems.Add(src); dstItems.Add(dst); }
                ( "," src=Expression() "->" dst=Expression() { srcItems.Add(src); dstItems.Add(dst); } )*)?
        "}"
        {
            return new SequenceExpressionMapConstructor(typeName, typeNameDst, srcItems.ToArray(), dstItems.ToArray());
        }
    |
        "("
            value=Expression()
        ")"
        {
            return new SequenceExpressionMapCopyConstructor(typeName, typeNameDst, value);
        }
    )
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "array" })
    (
        LOOKAHEAD(Word() "<" TypeNonGeneric() ">") Word() "<" typeName=TypeNonGeneric() ">"
    |
        LOOKAHEAD(Word() "<" MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(3).kind == WORD && GetToken(3).image == "match" })
        Word() "<" typeName=MatchTypeInContainerType() (">" ">" | ">>")
    )
        { srcItems = new List<SequenceExpression>(); }
    (
        "["
            (src=Expression() { srcItems.Add(src); }
                ( "," src=Expression() { srcItems.Add(src); } )*)?
        "]"
        {
            return new SequenceExpressionArrayConstructor(typeName, srcItems.ToArray());
        }
    |
        "("
            value=Expression()
        ")"
        {
            return new SequenceExpressionArrayCopyConstructor(typeName, value);
        }
    )
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "deque" })
    (
        LOOKAHEAD(Word() "<" TypeNonGeneric() ">") Word() "<" typeName=TypeNonGeneric() ">"
    |
        LOOKAHEAD(Word() "<" MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(3).kind == WORD && GetToken(3).image == "match" })
        Word() "<" typeName=MatchTypeInContainerType() (">" ">" | ">>")
    )
        { srcItems = new List<SequenceExpression>(); }
    (
        "["
            (src=Expression() { srcItems.Add(src); }
                ( "," src=Expression() { srcItems.Add(src); } )*)?
        "]"
        {
            return new SequenceExpressionDequeConstructor(typeName, srcItems.ToArray());
        }
    |
        "("
            value=Expression()
        ")"
        {
            return new SequenceExpressionDequeCopyConstructor(typeName, value);
        }
    )
}

void Arguments(List<SequenceExpression> argExprs):
{
}
{
    Argument(argExprs) ( "," Argument(argExprs) )*
}


SequenceVariable Variable(): // usage as well as definition
{
    String varName, typeName=null;
    SequenceVariable oldVariable, newVariable;
}
{
    varName=Word() (":" typeName=Type())?
    {
        oldVariable = varDecls.Lookup(varName);
        if(typeName != null)
        {
            if(oldVariable == null) {
                newVariable = varDecls.Define(varName, typeName);
            } else if(oldVariable.Type == "") {
                if(varDecls.WasImplicitelyDeclared(oldVariable)) 
                    throw new ParseException("The variable \"" + varName + "\" has already been used/implicitely declared as global variable!");
                else // it was explicitely used as global before, we are allowed to create a local variable with the same name, the global is (only) accessible with global prefix then
                    newVariable = varDecls.Define(varName, typeName);
            } else {
                throw new ParseException("The variable \"" + varName + "\" has already been declared as local variable with type \"" + oldVariable.Type + "\"!");
            }
        }
        else
        {
            if(oldVariable == null) {
                newVariable = varDecls.Define(varName, "");
                warnings.Add("WARNING: using global variables without \"::\" prefix is deprecated, missing for: " + varName);
            } else {
                if(oldVariable.Type == "")
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
        if(oldVariable == null) {
            newVariable = varDecls.Define(varName, typeName);
        } else if(oldVariable.Type == "") {
            if(varDecls.WasImplicitelyDeclared(oldVariable)) 
                throw new ParseException("The variable \"" + varName + "\" has already been used/implicitely declared as global variable!");
            else // it was explicitely used as global before, we are allowed to create a local variable with the same name, the global is (only) accessible with global prefix then
                newVariable = varDecls.Define(varName, typeName);
        } else {
            throw new ParseException("The variable \"" + varName + "\" has already been declared as local variable with type \"" + oldVariable.Type + "\"!");
        }
        return newVariable;
    }
}

SequenceVariable VariableDefinitionNonGeneric():
{
    String varName, typeName;
}
{
    varName=Word() ":" typeName=TypeNonGeneric()
    {
        SequenceVariable oldVariable = varDecls.Lookup(varName);
        SequenceVariable newVariable;
        if(oldVariable == null) {
            newVariable = varDecls.Define(varName, typeName);
        } else if(oldVariable.Type == "") {
            if(varDecls.WasImplicitelyDeclared(oldVariable)) 
                throw new ParseException("The variable \"" + varName + "\" has already been used/implicitely declared as global variable!");
            else // it was explicitely used as global before, we are allowed to create a local variable with the same name, the global is (only) accessible with global prefix then
                newVariable = varDecls.Define(varName, typeName);
        } else {
            throw new ParseException("The variable \"" + varName + "\" has already been declared as local variable with type \"" + oldVariable.Type + "\"!");
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
    varName=Word()
    {
        oldVariable = varDecls.Lookup(varName);
        if(oldVariable == null) {
            newVariable = varDecls.Define(varName, "");
            warnings.Add("WARNING: using global variables without \"::\" prefix is deprecated, missing for: " + varName);
        } else {
            if(oldVariable.Type == "")
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
}

void VariableList(List<SequenceVariable> variables):
{
    SequenceVariable var;
}
{
    var=Variable() { variables.Add(var); } ( "," var=Variable() { variables.Add(var); } )*
}

void VariableDefinitionList(List<SequenceVariable> variables):
{
    SequenceVariable var;
}
{
    ( var=VariableDefinition() { variables.Add(var); } ( "," var=VariableDefinition() { variables.Add(var); } )* )?
}

String Type():
{
    String type;
}
{
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "set" }) type=SetType()
    {
        return type;
    }
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "map" }) type=MapType()
    {
        return type;
    }
| 
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "array" }) type=ArrayType()
    {
        return type;
    }
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "deque" }) type=DequeType()
    {
        return type;
    }
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "match" }) type=MatchType()
    {
        return type;
    }
|
    type=TypeNonGeneric()
    {
        return type;
    }
}

String SetType():
{
    String type;
    String typeParam;
}
{
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">") Word() "<" typeParam=TypeNonGeneric() ">" { type = "set<" + typeParam + ">"; }
        (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(3).kind == WORD && GetToken(3).image == "match" }) 
    Word() "<" typeParam=MatchTypeInContainerType() (">" ">" | ">>") { type = "set<" + typeParam + ">"; }
    (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
    {
        return type;
    }
| // for below: keep >= which is from generic type closing plus a following assignment, it's tokenized into '>=' if written without whitespace, we'll eat the >= at the assignment
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">=") Word() "<" typeParam=TypeNonGeneric() { type = "set<" + typeParam + ">"; }
        (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" MatchTypeInContainerType() ">" ">=", { GetToken(3).kind == WORD && GetToken(3).image == "match" })
    Word() "<" typeParam=MatchTypeInContainerType() ">" { type = "set<" + typeParam + ">"; }
    (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
    {
        return type;
    }
}

String MapType():
{
    String type;
    String typeParam, typeParamDst;
}
{
    LOOKAHEAD(Word() "<" TypeNonGeneric() "," TypeNonGeneric() ">") Word() "<" typeParam=TypeNonGeneric() "," typeParamDst=TypeNonGeneric() ">" { type = "map<" + typeParam + "," + typeParamDst + ">"; }
    (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
    {
        return type;
    }
|
    // todo: TypeNonGeneric() for domain instead of Word(), i.e. package prefix admissible
    LOOKAHEAD(Word() "<" Word() "," MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(5).kind == WORD && GetToken(5).image == "match" }) 
    Word() "<" typeParam=Word() "," typeParamDst=MatchTypeInContainerType() (">" ">" | ">>") { type = "map<" + typeParam + "," + typeParamDst + ">"; }
    (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
    {
        return type;
    }
| // for below: keep >= which is from generic type closing plus a following assignment, it's tokenized into '>=' if written without whitespace, we'll eat the >= at the assignment
    LOOKAHEAD(Word() "<" TypeNonGeneric() "," TypeNonGeneric() ">=") Word() "<" typeParam=TypeNonGeneric() "," typeParamDst=TypeNonGeneric() { type = "map<" + typeParam + "," + typeParamDst + ">"; }
    (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
    {
        return type;
    }
|
    // todo: TypeNonGeneric() for domain instead of Word(), i.e. package prefix admissible
    LOOKAHEAD(Word() "<" Word() "," MatchTypeInContainerType() ">" ">=", { GetToken(5).kind == WORD && GetToken(5).image == "match" })
    Word() "<" typeParam=Word() "," typeParamDst=MatchTypeInContainerType() ">" { type = "map<" + typeParam + "," + typeParamDst + ">"; }
    (LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
    {
        return type;
    }
}

String ArrayType():
{
    String type;
    String typeParam;
}
{ 
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">")
    Word() "<" typeParam=TypeNonGeneric() ">" { type = "array<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(3).kind == WORD && GetToken(3).image == "match" }) 
    Word() "<" typeParam=MatchTypeInContainerType() (">" ">" | ">>") { type = "array<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
    {
        return type;
    }
| // for below: keep >= which is from generic type closing plus a following assignment, it's tokenized into '>=' if written without whitespace, we'll eat the >= at the assignment
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">=")
    Word() "<" typeParam=TypeNonGeneric() { type = "array<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" MatchTypeInContainerType() ">" ">=", { GetToken(3).kind == WORD && GetToken(3).image == "match" })
    Word() "<" typeParam=MatchTypeInContainerType() ">" { type = "array<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
    {
        return type;
    }
}

String DequeType():
{
    String type;
    String typeParam;
}
{ 
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">") Word() "<" typeParam=TypeNonGeneric() ">" { type = "deque<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at deque declaration, use d:deque<T> = deque<T>[] for initialization"); })?
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" MatchTypeInContainerType() (">" ">" | ">>"), { GetToken(3).kind == WORD && GetToken(3).image == "match" }) 
    Word() "<" typeParam=MatchTypeInContainerType() (">" ">" | ">>") { type = "deque<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at deque declaration, use d:deque<T> = deque<T>[] for initialization"); })?
    {
        return type;
    }
| // for below: keep >= which is from generic type closing plus a following assignment, it's tokenized into '>=' if written without whitespace, we'll eat the >= at the assignment
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">=") Word() "<" typeParam=TypeNonGeneric() { type = "deque<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at deque declaration, use d:deque<T> = deque<T>[] for initialization"); })?
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" MatchTypeInContainerType() ">" ">=", { GetToken(3).kind == WORD && GetToken(3).image == "match" })
    Word() "<" typeParam=MatchTypeInContainerType() ">" { type = "deque<" + typeParam + ">"; }
    (LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at deque declaration, use d:deque<T> = deque<T>[] for initialization"); })?
    {
        return type;
    }
}

String MatchType():
{
    String type;
    String typeParam;
}
{
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">") Word() "<" typeParam=TypeNonGeneric() ">" { type = "match<" + typeParam + ">"; }
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" TypeNonGeneric() ">=") Word() "<" typeParam=TypeNonGeneric() { type = "match<" + typeParam + ">"; }
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" "class" TypeNonGeneric() ">") Word() "<" "class" typeParam=TypeNonGeneric() ">" { type = "match<class " + typeParam + ">"; }
    {
        return type;
    }
|
    LOOKAHEAD(Word() "<" "class" TypeNonGeneric() ">=") Word() "<" "class" typeParam=TypeNonGeneric() { type = "match<class " + typeParam + ">"; }
    {
        return type;
    }
}

String MatchTypeInContainerType():
{
    String type;
    String typeParam;
}
{
    LOOKAHEAD(3) Word() "<" typeParam=TypeNonGeneric() { type = "match<" + typeParam + ">"; }    
    {
        return type;
    }
|
    Word() "<" "class" typeParam=TypeNonGeneric() { type = "match<class " + typeParam + ">"; }    
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
        return package != null ? package + "::" + type : type;
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
    name=Word() ("(" VariableDefinitionList(inputVariables) ")")? (":" "(" VariableDefinitionList(outputVariables) ")")?
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
    Sequence seqOrLeft, right;
    bool random = false, choice = false;
}
{
    seqOrLeft=RewriteSequenceLazyOr()
    (
        LOOKAHEAD(3)
        (
            LOOKAHEAD(3)
            ("$" { random = true; } ("%" { choice = true; })?)? "<;" right=RewriteSequenceLazyOr()
            {
                seqOrLeft = new SequenceThenLeft(seqOrLeft, right, random, choice);
            }
        |
            ("$" { random = true; } ("%" { choice = true; })?)? ";>" right=RewriteSequenceLazyOr()
            {
                seqOrLeft = new SequenceThenRight(seqOrLeft, right, random, choice);
            }
        )
    )*
    {
        return seqOrLeft;
    }
}

Sequence RewriteSequenceLazyOr():
{
    Sequence seqOrLeft, right;
    bool random = false, choice = false;
}
{
    seqOrLeft=RewriteSequenceLazyAnd()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "||" right=RewriteSequenceLazyAnd()
        {
            seqOrLeft = new SequenceLazyOr(seqOrLeft, right, random, choice);
        }
    )*
    {
        return seqOrLeft;
    }
}

Sequence RewriteSequenceLazyAnd():
{
    Sequence seqOrLeft, right;
    bool random = false, choice = false;
}
{
    seqOrLeft=RewriteSequenceStrictOr()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "&&" right=RewriteSequenceStrictOr()
        {
            seqOrLeft = new SequenceLazyAnd(seqOrLeft, right, random, choice);
        }
    )*
    {
        return seqOrLeft;
    }
}

Sequence RewriteSequenceStrictOr():
{
    Sequence seqOrLeft, right;
    bool random = false, choice = false;
}
{
    seqOrLeft=RewriteSequenceStrictXor()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "|" right=RewriteSequenceStrictXor()
        {
            seqOrLeft = new SequenceStrictOr(seqOrLeft, right, random, choice);
        }
    )*
    {
        return seqOrLeft;
    }
}

Sequence RewriteSequenceStrictXor():
{
    Sequence seqOrLeft, right;
    bool random = false, choice = false;
}
{
    seqOrLeft=RewriteSequenceStrictAnd()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "^" right=RewriteSequenceStrictAnd()
        {
            seqOrLeft = new SequenceXor(seqOrLeft, right, random, choice);
        }
    )*
    {
        return seqOrLeft;
    }
}

Sequence RewriteSequenceStrictAnd():
{
    Sequence seqOrLeft, right;
    bool random = false, choice = false;
}
{
    seqOrLeft=RewriteSequenceNeg()
    (
        LOOKAHEAD(3)
        ("$" { random = true; } ("%" { choice = true; })?)? "&" right=RewriteSequenceNeg()
        {
            seqOrLeft = new SequenceStrictAnd(seqOrLeft, right, random, choice);
        }
    )*
    {
        return seqOrLeft;
    }
}

Sequence RewriteSequenceNeg():
{
    Sequence seq;
    SequenceVariable toVar;
}
{
    "!" seq=RewriteSequenceIteration()
        ( "=>" toVar=Variable()
            {
                return new SequenceAssignSequenceResultToVar(toVar, new SequenceNot(seq));
            }
        | "|>" toVar=Variable()
            {
                return new SequenceOrAssignSequenceResultToVar(toVar, new SequenceNot(seq));
            }
        | "&>" toVar=Variable()
            {
                return new SequenceAndAssignSequenceResultToVar(toVar, new SequenceNot(seq));
            }
        |
            {
                return new SequenceNot(seq);
            }
        )
|
    seq=RewriteSequenceIteration()
        ( "=>" toVar=Variable()
            {
                return new SequenceAssignSequenceResultToVar(toVar, seq);
            }
        | "|>" toVar=Variable()
            {
                return new SequenceOrAssignSequenceResultToVar(toVar, seq);
            }
        | "&>" toVar=Variable()
            {
                return new SequenceAndAssignSequenceResultToVar(toVar, seq);
            }
        |
            {
                return seq;
            }
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
        LOOKAHEAD(3) expr=InitContainerExpr()
        {
            return new SequenceAssignContainerConstructorToVar(toVar, expr); // needed as sequence to allow variable declaration and initialization in sequence scope
        }
    |
        expr=InitObjectExpr()
        {
            return new SequenceAssignObjectConstructorToVar(toVar, expr); // needed as sequence to allow variable declaration and initialization in sequence scope
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
        LOOKAHEAD(4) "$" ("%" { choice = true; })? "(" num=Number() ")" 
        {
            return new SequenceAssignRandomIntToVar(toVar, num, choice);
        }
    |
        "$" ("%" { choice = true; })? "(" numDouble=DoubleNumber() ")" 
        {
            if(numDouble != 1.0)
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
            return new SequenceBooleanComputation(new SequenceComputationAssignment(
                new AssignmentTargetYieldingVar(toVar), new SequenceExpressionConstant(constant)), null, false);
        }
    |
        expr=InitContainerExpr()
        {
            return new SequenceBooleanComputation(new SequenceComputationAssignment(
                new AssignmentTargetYieldingVar(toVar), expr), null, false);
        }
    |
        fromVar=Variable()
        {
            return new SequenceBooleanComputation(new SequenceComputationAssignment(
                new AssignmentTargetYieldingVar(toVar), new SequenceExpressionVariable(fromVar)), null, false);
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
    "$" ("%" { choice = true; })? "||" "(" seq=RewriteSequence() { sequences.Add(seq); } 
        ( "," seq=RewriteSequence() { sequences.Add(seq); } )* ")"
    {
        return new SequenceLazyOrAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; })? "&&" "(" seq=RewriteSequence() { sequences.Add(seq); }
        ( "," seq=RewriteSequence() { sequences.Add(seq); } )* ")"
    {
        return new SequenceLazyAndAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; })? "|" "(" seq=RewriteSequence() { sequences.Add(seq); }
        ( "," seq=RewriteSequence() { sequences.Add(seq); } )* ")"
    {
        return new SequenceStrictOrAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; })? "&" "(" seq=RewriteSequence() { sequences.Add(seq); }
        ( "," seq=RewriteSequence() { sequences.Add(seq); } )* ")"
    {
        return new SequenceStrictAndAll(sequences, choice);
    }
|
    LOOKAHEAD(3)
    "$" ("%" { choice = true; })? "." "(" numDouble=DoubleNumber() seq=RewriteSequence() { numbers.Add(numDouble); sequences.Add(seq); }
        ( "," numDouble=DoubleNumber() seq=RewriteSequence() { numbers.Add(numDouble); sequences.Add(seq); } )* ")"
    {
        return new SequenceWeightedOne(sequences, numbers, choice);
    }
|
    LOOKAHEAD(3)
    ("$" { chooseRandSpecified=true; } ("%" { choice = true; })?)? "{" "<" seq=Rule() { sequences.Add(seq); }
        ( "," seq=Rule() { sequences.Add(seq); } )* ">" "}"
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
    "<<" { varDecls.PushScope(ScopeType.Backtrack); } seq=MultiRuleAllCall(false) (";;"|";")
        seq2=RewriteSequence() { varDecls.PopScope(variableList1); } ">>"
    {
        return new SequenceMultiBacktrack((SequenceMultiRuleAllCall)seq, seq2);
    }
|
    "<<" { varDecls.PushScope(ScopeType.Backtrack); } seq=Rule() (";;"|";")
        seq2=RewriteSequence() { varDecls.PopScope(variableList1); } (">>"|">")
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
        if(seq3 == null)
            return new SequenceIfThen(seq, seq2, variableList1, variableList2);
        else
            return new SequenceIfThenElse(seq, seq2, seq3, variableList1, variableList2);
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
                if(str == "ascending")
                    ascending = true;
                else if(str == "descending")
                    ascending = false;
                else
                    throw new SequenceParserException(str, SequenceParserError.UnknownIndexAccessDirection);
                if(indexName2 != null) {
                    if(indexName != indexName2)
                        throw new SequenceParserException(indexName, SequenceParserError.TwoDifferentIndexNames);
                }
                return new SequenceForIndexAccessOrdering(fromVar, ascending, indexName, expr, left, expr2, right, seq2, variableList1);
            }
        )
    |
        LOOKAHEAD(3) "in" str=Word() "(" (Arguments(argExprs))? ")" ";" seq=RewriteSequence()
            { varDecls.PopScope(variableList1); } "}"
        {
            if(str == "adjacent") {
                return new SequenceForFunction(SequenceType.ForAdjacentNodes, fromVar, argExprs, seq, variableList1);
            } else if(str == "adjacentIncoming") {
                return new SequenceForFunction(SequenceType.ForAdjacentNodesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str == "adjacentOutgoing") {
                return new SequenceForFunction(SequenceType.ForAdjacentNodesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str == "incident") {
                return new SequenceForFunction(SequenceType.ForIncidentEdges, fromVar, argExprs, seq, variableList1);
            } else if(str == "incoming") {
                return new SequenceForFunction(SequenceType.ForIncomingEdges, fromVar, argExprs, seq, variableList1);
            } else if(str == "outgoing") {
                return new SequenceForFunction(SequenceType.ForOutgoingEdges, fromVar, argExprs, seq, variableList1);
            } else if(str == "reachable") {
                return new SequenceForFunction(SequenceType.ForReachableNodes, fromVar, argExprs, seq, variableList1);
            } else if(str == "reachableIncoming") {
                return new SequenceForFunction(SequenceType.ForReachableNodesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str == "reachableOutgoing") {
                return new SequenceForFunction(SequenceType.ForReachableNodesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str == "reachableEdges") {
                return new SequenceForFunction(SequenceType.ForReachableEdges, fromVar, argExprs, seq, variableList1);
            } else if(str == "reachableEdgesIncoming") {
                return new SequenceForFunction(SequenceType.ForReachableEdgesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str == "reachableEdgesOutgoing") {
                return new SequenceForFunction(SequenceType.ForReachableEdgesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str == "boundedReachable") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableNodes, fromVar, argExprs, seq, variableList1);
            } else if(str == "boundedReachableIncoming") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableNodesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str == "boundedReachableOutgoing") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableNodesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str == "boundedReachableEdges") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableEdges, fromVar, argExprs, seq, variableList1);
            } else if(str == "boundedReachableEdgesIncoming") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableEdgesViaIncoming, fromVar, argExprs, seq, variableList1);
            } else if(str == "boundedReachableEdgesOutgoing") {
                return new SequenceForFunction(SequenceType.ForBoundedReachableEdgesViaOutgoing, fromVar, argExprs, seq, variableList1);
            } else if(str == "nodes") {
                return new SequenceForFunction(SequenceType.ForNodes, fromVar, argExprs, seq, variableList1);
            } else if(str == "edges") {
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
    "in" toVar=VariableUse() ("." attrName=Word())?
        "{" { varDecls.PushScope(ScopeType.InSubgraph); } seq=RewriteSequence() { varDecls.PopScope(variableList1); } "}"
    {
        return new SequenceExecuteInSubgraph(toVar, attrName, seq);
    }
|
    ("%" { special = true; })? "{" { varDecls.PushScope(ScopeType.Computation); }
        comp=CompoundComputation() { varDecls.PopScope(variableList1); } (";")? "}"
    {
        return new SequenceBooleanComputation(comp, variableList1, special);
    }
}

SequenceComputation CompoundComputation():
{
    SequenceComputation comp, compRight;
}
{
    comp=Computation()
        (";" compRight=CompoundComputation() 
            {
                return new SequenceComputationThen(comp, compRight);
            }
        |
            {
                return comp;
            }
        )
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
    SequenceExpression fromExpr = null;
    String attrName;
}
{
    LOOKAHEAD(2) "." attrName=Word()
    ("[" fromExpr=Expression() "]" 
        {
            return new AssignmentTargetAttributeIndexed(toVar, attrName, fromExpr);
        }
    )? // todo: this should be a composition of the two targets, not a fixed special one
    {
        return new AssignmentTargetAttribute(toVar, attrName);
    }
|
    "." "visited" ("[" fromExpr=Expression() "]")?
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
}

SequenceExpression Expression():
{
    SequenceExpression exprOrCond, trueCase, falseCase;
}
{
    exprOrCond=ExpressionExcept() ("?" trueCase=Expression() ":" falseCase=Expression()
        { exprOrCond = new SequenceExpressionConditional(exprOrCond, trueCase, falseCase); })?
        {
            return exprOrCond;
        }
}

SequenceExpression ExpressionExcept():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionLazyOr() 
        ( "\\" right=ExpressionLazyOr() { exprOrLeft = new SequenceExpressionExcept(exprOrLeft, right); } )*
        {
            return exprOrLeft;
        }
}

SequenceExpression ExpressionLazyOr():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionLazyAnd()
        ( "||" right=ExpressionLazyAnd() { exprOrLeft = new SequenceExpressionLazyOr(exprOrLeft, right); } )*
        {
            return exprOrLeft;
        }
}

SequenceExpression ExpressionLazyAnd():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionStrictOr()
        ( "&&" right=ExpressionStrictOr() { exprOrLeft = new SequenceExpressionLazyAnd(exprOrLeft, right); } )*
        {
            return exprOrLeft;
        }
}

SequenceExpression ExpressionStrictOr():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionStrictXor()
        ( "|" right=ExpressionStrictXor() { exprOrLeft = new SequenceExpressionStrictOr(exprOrLeft, right); } )*
        {
            return exprOrLeft;
        }
}

SequenceExpression ExpressionStrictXor():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionStrictAnd()
        ( "^" right=ExpressionStrictAnd() { exprOrLeft = new SequenceExpressionStrictXor(exprOrLeft, right); } )*
        {
            return exprOrLeft;
        }
}

SequenceExpression ExpressionStrictAnd():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionEquality()
        ( "&" right=ExpressionEquality() { exprOrLeft = new SequenceExpressionStrictAnd(exprOrLeft, right); } )*
        {
            return exprOrLeft;
        }
}

SequenceExpression ExpressionEquality():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionRelation()
        ( "==" right=ExpressionRelation() { exprOrLeft = new SequenceExpressionEqual(exprOrLeft, right); } 
        | "!=" right=ExpressionRelation() { exprOrLeft = new SequenceExpressionNotEqual(exprOrLeft, right); }
        | "~~" right=ExpressionRelation() { exprOrLeft = new SequenceExpressionStructuralEqual(exprOrLeft, right); }
        )*
    {
        return exprOrLeft;
    }
}

SequenceExpression ExpressionRelation():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionShift()
        ( "<" right=ExpressionShift() { exprOrLeft = new SequenceExpressionLower(exprOrLeft, right); }
        | ">" right=ExpressionShift() { exprOrLeft = new SequenceExpressionGreater(exprOrLeft, right); }
        | "<=" right=ExpressionShift() { exprOrLeft = new SequenceExpressionLowerEqual(exprOrLeft, right); }
        | ">=" right=ExpressionShift() { exprOrLeft = new SequenceExpressionGreaterEqual(exprOrLeft, right); }
        | "in" right=ExpressionShift() { exprOrLeft = new SequenceExpressionInContainerOrString(exprOrLeft, right); }
        )* 
    {
        return exprOrLeft;
    }
}

RelOpDirection RelationOp():
{
}
{
    "<"
    {
        return RelOpDirection.Smaller;
    }
|
    ">"
    {
        return RelOpDirection.Greater;
    }
|
    "<="
    {
        return RelOpDirection.SmallerEqual;
    }
|
    ">="
    {
        return RelOpDirection.GreaterEqual;
    }
}

SequenceExpression ExpressionShift():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionAdd()
        ( "<<" right=ExpressionAdd() { exprOrLeft = new SequenceExpressionShiftLeft(exprOrLeft, right); }
        | ">>" right=ExpressionAdd() { exprOrLeft = new SequenceExpressionShiftRight(exprOrLeft, right); }
        | ">>>" right=ExpressionAdd() { exprOrLeft = new SequenceExpressionShiftRightUnsigned(exprOrLeft, right); }
        )* 
    {
        return exprOrLeft;
    }
}

SequenceExpression ExpressionAdd():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionMul()
        ( "+" right=ExpressionMul() { exprOrLeft = new SequenceExpressionPlus(exprOrLeft, right); } 
        | "-" right=ExpressionMul() { exprOrLeft = new SequenceExpressionMinus(exprOrLeft, right); }
        )*
    {
        return exprOrLeft;
    }
}

SequenceExpression ExpressionMul():
{
    SequenceExpression exprOrLeft, right;
}
{
    exprOrLeft=ExpressionUnary()
        ( "*" right=ExpressionUnary() { exprOrLeft = new SequenceExpressionMul(exprOrLeft, right); } 
        | "/" right=ExpressionUnary() { exprOrLeft = new SequenceExpressionDiv(exprOrLeft, right); }
        | "%" right=ExpressionUnary() { exprOrLeft = new SequenceExpressionMod(exprOrLeft, right); }
        )*
    {
        return exprOrLeft;
    }
}

SequenceExpression ExpressionUnary():
{
    SequenceExpression seq;
    object type;
}
{
    LOOKAHEAD("(" Constant() ")") "(" type=Constant() ")" seq=ExpressionBasic()
    {
        return new SequenceExpressionCast(seq, type);
    }
|
    "!" seq=ExpressionBasic()
    {
        return new SequenceExpressionNot(seq);
    }
|
    "-" seq=ExpressionBasic()
    {
        return new SequenceExpressionUnaryMinus(seq);
    }
|
    "+" seq=ExpressionBasic()
    {
        return new SequenceExpressionUnaryPlus(seq);
    }
|
    "~" seq=ExpressionBasic()
    {
        return new SequenceExpressionBitwiseComplement(seq);
    }
|
    seq=ExpressionBasic()
    {
        return seq;
    }
}

SequenceExpression ExpressionBasic():
{
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceVariable fromVar;
    String elemName;
    String type;
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
    LOOKAHEAD(3)
    expr=MultiRuleQuery() expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    expr=MappingClause() expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    LOOKAHEAD(FunctionCall())
    expr=FunctionCall() expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    expr=ScanFunctionCall() expr=SelectorExpression(expr)
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
    LOOKAHEAD(2)
    expr=InitContainerExpr() expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    LOOKAHEAD(2)
    expr=InitMatchClassExpr() expr=SelectorExpression(expr)
    {
        return expr;
    }
|
    "new" type=TypeNonGeneric() expr=ExpressionInitObjectCont(type)
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

SequenceExpression ExpressionInitObjectCont(String internalObjectType):
{
    List<KeyValuePair<String, SequenceExpression>> attributes = new List<KeyValuePair<String, SequenceExpression>>();
    KeyValuePair<String, SequenceExpression> attribute;
}
{
    "(" ")"
    {
        return new SequenceExpressionNew(internalObjectType);
    }
|
    "@" "(" (attribute=AttributeInitialization() { attributes.Add(attribute); } )? ("," attribute=AttributeInitialization() { attributes.Add(attribute); } )* ")"
    {
        return new SequenceExpressionNew(internalObjectType, attributes);
    }
}

KeyValuePair<String, SequenceExpression> AttributeInitialization():
{
    String attribute;
    SequenceExpression expr;
}
{
    attribute=Word() "=" expr = Expression()
    {
        return new KeyValuePair<String, SequenceExpression>(attribute, expr);
    }
}

SequenceExpression SelectorExpression(SequenceExpression fromExpr):
{
    String methodOrAttrName;
    String extendedMethodName = null, methodNameExtension = null, methodNameExtension2 = null;
    String memberOrAttribute = null;
    String typeName;
    SequenceVariable var, initArrayAccess = null;
    Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable> arrayAccessWithPreviousAccumulationAccessWithIndexWithValue = null;
    SequenceExpression expr = null, initExpr = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<SequenceVariable> variableList = new List<SequenceVariable>();
}
{
    LOOKAHEAD(2)
    "." methodOrAttrName=Word()
    ( LOOKAHEAD(4)
        "<" memberOrAttribute=Word() ">"
        "(" (Arguments(argExprs))? ")"
            { expr = env.CreateSequenceExpressionArrayAttributeAccessMethodCall(fromExpr, methodOrAttrName, memberOrAttribute, argExprs); }
    |
        "<" (LOOKAHEAD(2) typeName=TypeNonGeneric() | typeName=MatchType()) ">"
        ( { extendedMethodName = methodOrAttrName; } methodNameExtension=Word() { extendedMethodName += methodNameExtension; } "{" { varDecls.PushScope(ScopeType.Computation); } 
            (LOOKAHEAD(VariableDefinition() ";") initArrayAccess=VariableDefinition() ";")? initExpr=Expression() 
            { varDecls.PopScope(variableList); } "}" methodNameExtension2=Word() { extendedMethodName += methodNameExtension2; } )?
        "{" { varDecls.PushScope(ScopeType.Computation); } 
        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue=LambdaExprVarDeclPrefix() expr=Expression()
        { varDecls.PopScope(variableList); } "}"
            {
                if(extendedMethodName != null) {
                    expr = env.CreateSequenceExpressionPerElementMethodCall(fromExpr, extendedMethodName, typeName,
                        initArrayAccess, initExpr,
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item2, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, expr);
                } else {
                    expr = env.CreateSequenceExpressionPerElementMethodCall(fromExpr, methodOrAttrName, typeName,
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, expr);
                }
            }
    |
        "{" { varDecls.PushScope(ScopeType.Computation); }
        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue=LambdaExprVarDeclPrefix() expr=Expression()
        { varDecls.PopScope(variableList); } "}"
            {
                expr = env.CreateSequenceExpressionPerElementMethodCall(fromExpr, methodOrAttrName, null,
                    arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, expr);
            }
    |
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
    "." "visited" (LOOKAHEAD(2) "[" expr=Expression() "]")?
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

Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable> LambdaExprVarDeclPrefix():
{
    SequenceVariable arrayAccess;
    Tuple<SequenceVariable, SequenceVariable, SequenceVariable> previousAccumulationAccessWithIndexWithValue;
}
{
    LOOKAHEAD(VariableDefinition() ";") arrayAccess=VariableDefinition() ";" previousAccumulationAccessWithIndexWithValue=MaybePreviousAccumulationAccessLambdaExprVarDecl()
    {
        return new Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable>(arrayAccess, previousAccumulationAccessWithIndexWithValue.Item1, 
            previousAccumulationAccessWithIndexWithValue.Item2, previousAccumulationAccessWithIndexWithValue.Item3);
    }
|
    previousAccumulationAccessWithIndexWithValue=MaybePreviousAccumulationAccessLambdaExprVarDecl()
    {
        return new Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable>(null, previousAccumulationAccessWithIndexWithValue.Item1,
            previousAccumulationAccessWithIndexWithValue.Item2, previousAccumulationAccessWithIndexWithValue.Item3);
    }
}

Tuple<SequenceVariable, SequenceVariable, SequenceVariable> MaybePreviousAccumulationAccessLambdaExprVarDecl():
{
    SequenceVariable previousAccumulationAccess;
    Tuple<SequenceVariable, SequenceVariable> indexWithValue;
}
{
    LOOKAHEAD(VariableDefinition() ",") previousAccumulationAccess=VariableDefinition() "," indexWithValue=MaybeIndexedLambdaExprVarDecl()
    {
        return new Tuple<SequenceVariable, SequenceVariable, SequenceVariable>(previousAccumulationAccess, indexWithValue.Item1, indexWithValue.Item2);
    }
|
    indexWithValue=MaybeIndexedLambdaExprVarDecl()
    {
        return new Tuple<SequenceVariable, SequenceVariable, SequenceVariable>(null, indexWithValue.Item1, indexWithValue.Item2);
    }
}

Tuple<SequenceVariable, SequenceVariable> MaybeIndexedLambdaExprVarDecl():
{
    SequenceVariable index, var;
}
{
    LOOKAHEAD(VariableDefinitionNonGeneric() "->" VariableDefinition() "->") index=VariableDefinitionNonGeneric() "->" var=VariableDefinition() "->"
    {
        return new Tuple<SequenceVariable, SequenceVariable>(index, var);
    }
|
    var=VariableDefinition() "->"
    {
        return new Tuple<SequenceVariable, SequenceVariable>(null, var);
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
    ("(" VariableList(returnVars) ")" "=")?
        (LOOKAHEAD(2) package=Word() "::")? (LOOKAHEAD(2) fromVar=VariableUse() "." (LOOKAHEAD(2) attrName=Word() ".")?)? 
        procedure=Word() "(" (Arguments(argExprs))? ")"
    {
        if(fromVar == null) // procedure call
        {
            return env.CreateSequenceComputationProcedureCall(procedure, package, argExprs, returnVars);
        }
        else
        { // method call
            if(attrName == null)
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

SequenceExpression ScanFunctionCall():
{
    Token function;
    String type = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
}
{
    (function="scan" | function="tryscan") ("<" type=TypeOrContainerTypeContinuation())? "(" Argument(argExprs) ")"
    {
        if(function.image == "scan")
            return new SequenceExpressionScan(type, argExprs[0]);
        else //function.image == "tryscan"
            return new SequenceExpressionTryScan(type, argExprs[0]);
    }
}

String TypeOrContainerTypeContinuation():
{
    String type;
    String typeParam, typeParamDst;
}
{
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "set" })
    Word() "<" typeParam=TypeNonGeneric() (">" ">"| ">>") { type = "set<" + typeParam + ">"; }
    {
        return type;
    }
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "map" })
    Word() "<" typeParam=TypeNonGeneric() "," typeParamDst=TypeNonGeneric() (">" ">" | ">>") { type = "map<" + typeParam + "," + typeParamDst + ">"; }
    {
        return type;
    }
| 
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "array" })
    Word() "<" typeParam=TypeNonGeneric() (">" ">" | ">>") { type = "array<" + typeParam + ">"; }
    {
        return type;
    }
|
    LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "deque" })
    Word() "<" typeParam=TypeNonGeneric() (">" ">" | ">>") { type = "deque<" + typeParam + ">"; }
    {
        return type;
    }
|
    type=TypeNonGeneric() ">"
    {
        return type;
    }
}

Sequence RulePrefixedSequence():
{
    SequenceRuleCall rule;
    Sequence seq;
    List<SequenceVariable> variableList = new List<SequenceVariable>();
}
{
    "[" "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMultiRuleAllCall(false) ";"
        seq=RewriteSequence() { varDecls.PopScope(variableList); } "}" "]"
    {
        return new SequenceRulePrefixedSequence(rule, seq, variableList);
    }
}

Sequence MultiRulePrefixedSequence():
{
    SequenceRuleCall rule;
    Sequence seq;
    List<SequenceVariable> variableList = new List<SequenceVariable>();
    List<SequenceRulePrefixedSequence> rulePrefixedSequences = new List<SequenceRulePrefixedSequence>();
    SequenceMultiRulePrefixedSequence seqMultiRulePrefixedSequence;
    SequenceFilterCallBase filter = null;
}
{
    "[" "[" "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMultiRuleAllCall(false) ";"
        seq=RewriteSequence() { varDecls.PopScope(variableList); } { rulePrefixedSequences.Add(new SequenceRulePrefixedSequence(rule, seq, variableList)); } "}"
        ( "," "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMultiRuleAllCall(false) ";"
            seq=RewriteSequence() { varDecls.PopScope(variableList); } { rulePrefixedSequences.Add(new SequenceRulePrefixedSequence(rule, seq, variableList)); } "}" 
        )*
        "]" { seqMultiRulePrefixedSequence = new SequenceMultiRulePrefixedSequence(rulePrefixedSequences); }
        ( "\\" filter=Filter(null, true) { seqMultiRulePrefixedSequence.AddFilterCall(filter); } )*
        "]"
    {
        return seqMultiRulePrefixedSequence;
    }
}

Sequence MultiRuleAllCall(bool returnsArrays):
{
    Sequence seq;
    List<Sequence> sequences = new List<Sequence>();
    SequenceMultiRuleAllCall seqMultiRuleAll;
    SequenceFilterCallBase filter = null;
}
{
    "[" "[" seq=RuleForMultiRuleAllCall(returnsArrays) { sequences.Add(seq); }
        ( "," seq=RuleForMultiRuleAllCall(returnsArrays) { sequences.Add(seq); } )*
        "]" { seqMultiRuleAll = new SequenceMultiRuleAllCall(sequences); }
        ( "\\" filter=Filter(null, true) { seqMultiRuleAll.AddFilterCall(filter); } )*
        "]" 
    {
        return seqMultiRuleAll;
    }
}

SequenceRuleCall RuleForMultiRuleAllCall(bool returnsArrays):
{
    bool special = false, test = false;
    String str, package = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<SequenceVariable> returnVars = new List<SequenceVariable>();
    SequenceRuleCall ruleCall;
    SequenceFilterCallBase filter = null;
}
{
    ("(" VariableList(returnVars) ")" "=")?
    (
        (LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } | "%" { special = true; })?
        (LOOKAHEAD(2) package=Word() "::")? 
        str=Word() ("(" (Arguments(argExprs))? ")")?
        {
            // No variable with this name may exist
            if(varDecls.Lookup(str) != null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            ruleCall = env.CreateSequenceRuleCall(str, package, argExprs, returnVars, null,
                special, test, returnsArrays);
        }
            ( "\\" filter=Filter(ruleCall, false) { ruleCall.AddFilterCall(filter); } )*
            {
                return ruleCall;
            }
    )
}

void RuleLookahead():
{
}
{
    ("(" ( Word() (":" (LOOKAHEAD({ GetToken(1).kind == WORD && (GetToken(1).image == "set" || GetToken(1).image == "array" || GetToken(1).image == "deque") }) Word() "<" TypeNonGeneric() ">" |
                        LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "map" }) Word() "<" TypeNonGeneric() "," TypeNonGeneric() ">" |
                        TypeNonGeneric())
                    )? | "::" Word() ) 
            ( "," ( Word() (":" (LOOKAHEAD({ GetToken(1).kind == WORD && (GetToken(1).image == "set" || GetToken(1).image == "array" || GetToken(1).image == "deque") }) Word() "<" TypeNonGeneric() ">" |
                                LOOKAHEAD({ GetToken(1).kind == WORD && GetToken(1).image == "map" }) Word() "<" TypeNonGeneric() "," TypeNonGeneric() ">" |
                                TypeNonGeneric())
                            )? | "::" Word() ) )* ")" "=")?
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
    SequenceFilterCallBase filter = null;
}
{
    ("(" VariableList(returnVars) ")" "=")?
    (
        (
            "$" ("%" { choice = true; })? ( varChooseRand=Variable() ("," (varChooseRand2=Variable() | "*") { chooseRandSpecified2 = true; })? )? { chooseRandSpecified = true; }
        )?
        "[" (LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } | "%" { special = true; })? 
        (LOOKAHEAD(2) subgraph=Variable() ".")? (LOOKAHEAD(2) package=Word() "::")?
        str=Word() ("(" (Arguments(argExprs))? ")")?
        {
            // No variable with this name may exist
            if(varDecls.Lookup(str) != null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            ruleAllCall = env.CreateSequenceRuleAllCall(str, package, argExprs, returnVars, subgraph,
                    special, test, chooseRandSpecified, varChooseRand, chooseRandSpecified2, varChooseRand2, choice);
        }
            ( "\\" filter=Filter(ruleAllCall, false) { ruleAllCall.AddFilterCall(filter); } )*
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
            if(varDecls.Lookup(str) != null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            ruleCountAllCall = env.CreateSequenceRuleCountAllCall(str, package, argExprs, returnVars, subgraph,
                    special, test);
        }
            ( "\\" filter=Filter(ruleCountAllCall, false) { ruleCountAllCall.AddFilterCall(filter); } )*
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
            if(argExprs.Count == 0 && returnVars.Count == 0)
            {
                SequenceVariable var = varDecls.Lookup(str);
                if(var != null)
                {
                    if(var.Type != "" && var.Type != "boolean")
                        throw new SequenceParserException(str, "untyped or bool", var.Type);
                    if(subgraph != null)
                        throw new SequenceParserException(str, "", SequenceParserError.SubgraphError);
                    return new SequenceBooleanComputation(new SequenceExpressionVariable(var), null, special);
                }
            }

            // No variable with this name may exist
            if(varDecls.Lookup(str) != null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            if(env.IsSequenceName(str, package)) {
                sequenceCall = env.CreateSequenceSequenceCall(str, package, argExprs, returnVars, subgraph,
                                special);
            } else {
                ruleCall = env.CreateSequenceRuleCall(str, package, argExprs, returnVars, subgraph,
                                special, test, false);
            }
        }
            ( "\\" filter=Filter(ruleCall, false)
                {
                    if(varDecls.Lookup(str) != null)
                        throw new SequenceParserException(str, filter.ToString(), SequenceParserError.FilterError);
                    if(sequenceCall != null) {
                        List<SequenceFilterCallBase> filters = new List<SequenceFilterCallBase>();
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
    SequenceFilterCallBase filter = null;
}
{
    "[" ( LOOKAHEAD(2) "?" "%" { test = true; special = true; } | LOOKAHEAD(2) "%" "?" { special = true; test = true; } | "?" { test = true; } )
    (LOOKAHEAD(2) subgraph=Variable() ".")? (LOOKAHEAD(2) package=Word() "::")?
    str=Word() ("(" (Arguments(argExprs))? ")")?
    {
        ruleAllCall = env.CreateSequenceRuleAllCall(str, package, argExprs, new List<SequenceVariable>(), subgraph,
                special, test, false, null, false, null, false);
    }
        ( "\\" filter=Filter(ruleAllCall, false) { ruleAllCall.AddFilterCall(filter); } )*
    "]"
    {
        return new SequenceExpressionRuleQuery(ruleAllCall);
    }
}

SequenceExpression MultiRuleQuery():
{
    Sequence seq;
    List<Sequence> sequences = new List<Sequence>();
    SequenceMultiRuleAllCall seqMultiRuleAll;
    SequenceFilterCallBase filter = null;
    String matchClassPackage = null;
    String matchClassName = null;
}
{
    "[" "?" "[" seq=RuleForMultiRuleQuery() { sequences.Add(seq); }
        ( "," seq=RuleForMultiRuleQuery() { sequences.Add(seq); } )*
        "]" { seqMultiRuleAll = new SequenceMultiRuleAllCall(sequences); }
        ( LOOKAHEAD(2) "\\" filter=Filter(null, true) { seqMultiRuleAll.AddFilterCall(filter); } )*
        "\\" "<" "class" (LOOKAHEAD(2) matchClassPackage=Word() "::")? matchClassName=Word() ">"
        "]"
    {
        return new SequenceExpressionMultiRuleQuery(seqMultiRuleAll, env.GetPackagePrefixedMatchClassName(matchClassName, matchClassPackage));
    }
}

SequenceRuleCall RuleForMultiRuleQuery():
{
    bool special = false;
    String str, package = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<SequenceVariable> returnVars = new List<SequenceVariable>();
    SequenceRuleCall ruleCall;
    SequenceFilterCallBase filter = null;
}
{
    ("%" { special = true; })?
    (LOOKAHEAD(2) package=Word() "::")? 
    str=Word() ("(" (Arguments(argExprs))? ")")?
    {
        // No variable with this name may exist
        if(varDecls.Lookup(str) != null)
            throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

        ruleCall = env.CreateSequenceRuleCall(str, package, argExprs, returnVars, null,
            special, true, false);
    }
        ( "\\" filter=Filter(ruleCall, false) { ruleCall.AddFilterCall(filter); } )*
        {
            return ruleCall;
        }
}

SequenceExpression MappingClause():
{
    SequenceRuleCall rule;
    Sequence seq;
    List<SequenceVariable> variableList = new List<SequenceVariable>();
    List<SequenceRulePrefixedSequence> rulePrefixedSequences = new List<SequenceRulePrefixedSequence>();
    SequenceMultiRulePrefixedSequence seqMultiRulePrefixedSequence;
    SequenceFilterCallBase filter = null;
}
{
    "[" ":"
        "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMappingMultiRulePrefixedSequence() ";"
            seq=RewriteSequence() { varDecls.PopScope(variableList); } { rulePrefixedSequences.Add(new SequenceRulePrefixedSequence(rule, seq, variableList)); } "}"
            ( "," "for" "{" { varDecls.PushScope(ScopeType.ForRulePrefixedSequence); } rule=RuleForMappingMultiRulePrefixedSequence() ";"
                seq=RewriteSequence() { varDecls.PopScope(variableList); } { rulePrefixedSequences.Add(new SequenceRulePrefixedSequence(rule, seq, variableList)); } "}" 
            )*
            { seqMultiRulePrefixedSequence = new SequenceMultiRulePrefixedSequence(rulePrefixedSequences); }
            ( "\\" filter=Filter(null, true) { seqMultiRulePrefixedSequence.AddFilterCall(filter); } )*
    ":" "]"
    {
        return new SequenceExpressionMappingClause(seqMultiRulePrefixedSequence);
    }
}

SequenceRuleCall RuleForMappingMultiRulePrefixedSequence():
{
    bool special = false;
    String str, package = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    List<SequenceVariable> returnVars = new List<SequenceVariable>();
    SequenceRuleCall ruleCall;
    SequenceFilterCallBase filter = null;
}
{
    ("(" VariableList(returnVars) ")" "=")?
    (
        ("%" { special = true; })?
        (LOOKAHEAD(2) package=Word() "::")? 
        str=Word() ("(" (Arguments(argExprs))? ")")?
        {
            // No variable with this name may exist
            if(varDecls.Lookup(str) != null)
                throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

            ruleCall = env.CreateSequenceRuleCall(str, package, argExprs, returnVars, null,
                special, false, false);
        }
            ( "\\" filter=Filter(ruleCall, false) { ruleCall.AddFilterCall(filter); } )*
            {
                return ruleCall;
            }
    )
}

SequenceFilterCallBase Filter(SequenceRuleCall ruleCall, bool isMatchClassFilter):
{
    String id, package = null;
    SequenceFilterCallBase filter = null;
}
{
    (LOOKAHEAD(2) package=Word() "::")? id=Word()
    (
        "." filter=FilterMatchClassContinuation(ruleCall, isMatchClassFilter, package, id)
        {
            return filter;
        }
    |
        filter=FilterRuleContinuation(ruleCall, isMatchClassFilter, package, id)
        {
            return filter;
        }
    )
}

SequenceFilterCallBase FilterMatchClassContinuation(SequenceRuleCall ruleCall, bool isMatchClassFilter, String matchClassPackage, String matchClass):
{
    String filterBase, package = null;
    List<String> words = new List<String>();
    SequenceFilterCallBase filter = null;
}
{
    LOOKAHEAD(2) filterBase=Word() "<" WordList(words) (">" | ">>")
        filter=FilterMatchClassContinuationMember(ruleCall, isMatchClassFilter, matchClassPackage, matchClass, filterBase, words)
    {
        return filter;
    }
|
    (LOOKAHEAD(2) package=Word() "::")? filterBase=Word()
        filter=FilterMatchClassContinuationNonMember(ruleCall, isMatchClassFilter, matchClassPackage, matchClass, package, filterBase)
    {
        return filter;
    }
}

SequenceFilterCallBase FilterMatchClassContinuationMember(SequenceRuleCall ruleCall, bool isMatchClassFilter, String matchClassPackage, String matchClass, String filterBase, List<String> words):
{
    String package = null;
    String filterExtension = null, filterExtension2 = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceExpression initExpr = null, lambdaExpr = null;
    Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable> arrayAccessWithPreviousAccumulationAccessWithIndexWithValue = null;
    List<SequenceVariable> variableList = new List<SequenceVariable>();
    SequenceVariable initArrayAccess = null;
}
{
    (LOOKAHEAD(2) filterExtension=Word() { filterBase += filterExtension; } "<" WordList(words) ">" filterExtension2=Word() { filterBase += filterExtension2; } "<" WordList(words) (">" | ">>") )?
    ( filterExtension=Word() { filterBase += filterExtension; } "{" { varDecls.PushScope(ScopeType.Computation); } 
        (LOOKAHEAD(VariableDefinition() ";") initArrayAccess=VariableDefinition() ";")? initExpr=Expression() 
        { varDecls.PopScope(variableList); } "}" filterExtension2=Word() { filterBase += filterExtension2; } )?
    ( "{" { varDecls.PushScope(ScopeType.Computation); }
        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue=LambdaExprVarDeclPrefix() lambdaExpr=Expression()
        { varDecls.PopScope(variableList); } "}" )?
    ("(" ")")?
    {
        if(!isMatchClassFilter)
            throw new ParseException("A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

        if(!env.IsAutoGeneratedBaseFilterName(filterBase) && !env.IsPerElementBaseFilterName(filterBase))
            throw new ParseException("Unknown def-variable-based filter " + filterBase + "! Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy and assign, removeIf, assignStartWithAccumulateBy.");
        else
        {
            if(lambdaExpr != null)
            {
                if(initExpr != null) {
                    return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words,
                        initArrayAccess, initExpr,
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item2,
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, lambdaExpr);
                } else {
                    return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words,
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, lambdaExpr);
                }
            }
            else
                return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words, argExprs);
        }
    }
}

SequenceFilterCallBase FilterMatchClassContinuationNonMember(SequenceRuleCall ruleCall, bool isMatchClassFilter, String matchClassPackage, String matchClass, String package, String filterBase):
{
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceExpression lambdaExpr = null;
    Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable> arrayAccessWithPreviousAccumulationAccessWithIndexWithValue = null;
    List<String> words = new List<String>();
    List<SequenceVariable> variableList = new List<SequenceVariable>();
}
{
    ( "{" { varDecls.PushScope(ScopeType.Computation); }
        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue=LambdaExprVarDeclPrefix() lambdaExpr=Expression()
        { varDecls.PopScope(variableList); } "}" )?
    ("(" (Arguments(argExprs))? ")")?
    {
        if(!isMatchClassFilter)
            throw new ParseException("A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

        if(lambdaExpr != null)
        {
            return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words,
                arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, lambdaExpr);
        }
        else if(env.IsAutoSuppliedFilterName(filterBase))
        {
            if(argExprs.Count != 1)
                throw new ParseException("The auto-supplied filter " + filterBase + " expects exactly one parameter!");

            return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words, argExprs);
        }
        else
        {
            if(filterBase == "auto")
                throw new ParseException("The auto filter is not available for multi rule call or multi rule backtracking constructs.");

            return env.CreateSequenceMatchClassFilterCall(matchClass, matchClassPackage, package, filterBase, words, argExprs);
        }
    }
}

SequenceFilterCallBase FilterRuleContinuation(SequenceRuleCall ruleCall, bool isMatchClassFilter, String package, String filterBase):
{
    List<String> words = new List<String>();
    SequenceFilterCallBase filter = null;
}
{
    "<" WordList(words) (">" | ">>") filter=FilterRuleContinuationMember(ruleCall, isMatchClassFilter, package, filterBase, words)
    {
        return filter;
    }
|
    filter=FilterRuleContinuationNonMember(ruleCall, isMatchClassFilter, package, filterBase)
    {
        return filter;
    }
}

SequenceFilterCallBase FilterRuleContinuationMember(SequenceRuleCall ruleCall, bool isMatchClassFilter, String package, String filterBase, List<String> words):
{
    String filterExtension = null, filterExtension2 = null;
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceExpression initExpr = null, lambdaExpr = null;
    Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable> arrayAccessWithPreviousAccumulationAccessWithIndexWithValue = null;
    List<SequenceVariable> variableList = new List<SequenceVariable>();
    SequenceVariable initArrayAccess = null;
}
{
    (LOOKAHEAD(2) filterExtension=Word() { filterBase += filterExtension; } "<" WordList(words) ">" filterExtension2=Word() { filterBase += filterExtension2; } "<" WordList(words) (">" | ">>") )?
    ( filterExtension=Word() { filterBase += filterExtension; } "{" { varDecls.PushScope(ScopeType.Computation); }
        (LOOKAHEAD(VariableDefinition() ";") initArrayAccess=VariableDefinition() ";")? initExpr=Expression()
        { varDecls.PopScope(variableList); } "}" filterExtension2=Word() { filterBase += filterExtension2; } )?
    ( "{" { varDecls.PushScope(ScopeType.Computation); }
        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue=LambdaExprVarDeclPrefix() lambdaExpr=Expression()
        { varDecls.PopScope(variableList); } "}" )?
    ("(" ")")?
    {
        if(isMatchClassFilter)
            throw new ParseException("A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

        if(!env.IsAutoGeneratedBaseFilterName(filterBase) && !env.IsPerElementBaseFilterName(filterBase))
            throw new ParseException("Unknown def-variable-based filter " + filterBase + "! Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy and assign, removeIf, assignStartWithAccumulateBy.");
        else
        {
            if(lambdaExpr != null)
            {
                if(initExpr != null) {
                    return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words,
                        initArrayAccess, initExpr, 
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item2,
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, lambdaExpr);
                } else {
                    return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words,
                        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, lambdaExpr);
                }
            }
            else
                return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words, argExprs);
        }
    }
}

SequenceFilterCallBase FilterRuleContinuationNonMember(SequenceRuleCall ruleCall, bool isMatchClassFilter, String package, String filterBase):
{
    List<SequenceExpression> argExprs = new List<SequenceExpression>();
    SequenceExpression lambdaExpr = null;
    Tuple<SequenceVariable, SequenceVariable, SequenceVariable, SequenceVariable> arrayAccessWithPreviousAccumulationAccessWithIndexWithValue = null;
    List<String> words = new List<String>();
    List<SequenceVariable> variableList = new List<SequenceVariable>();
}
{
    ( "{" { varDecls.PushScope(ScopeType.Computation); }
        arrayAccessWithPreviousAccumulationAccessWithIndexWithValue=LambdaExprVarDeclPrefix() lambdaExpr=Expression()
        { varDecls.PopScope(variableList); } "}" )?
    ("(" (Arguments(argExprs))? ")")?
    {
        if(isMatchClassFilter)
            throw new ParseException("A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

        if(lambdaExpr != null)
        {
            return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words,
                arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item1, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item3, arrayAccessWithPreviousAccumulationAccessWithIndexWithValue.Item4, lambdaExpr);
        }
        else if(env.IsAutoSuppliedFilterName(filterBase))
        {
            if(argExprs.Count != 1)
                throw new ParseException("The auto-supplied filter " + filterBase + " expects exactly one parameter!");

            return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words, argExprs);
        }
        else
        {
            if(filterBase == "auto")
            {
                if(isMatchClassFilter)
                    throw new ParseException("The auto filter is not available for multi rule call or multi rule backtracking constructs.");

                return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, "auto", words, argExprs);
            }

            return env.CreateSequenceFilterCall(ruleCall.Name, ruleCall.Package, package, filterBase, words, argExprs);
        }
    }
}

void WordList(List<String> words):
{
    String word;
}
{
    word=Word() { words.Add(word); } ( "," word=Word() { words.Add(word); } )*
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
String FiltersToString(List<SequenceFilterCallBase> filters)
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
