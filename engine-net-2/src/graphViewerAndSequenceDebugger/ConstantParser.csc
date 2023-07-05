// by Moritz Kroll, Edgar Jakumeit

options {
    STATIC=false;
}

PARSER_BEGIN(ConstantParser)
    namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger;
    using System;
    using System.Collections;
    using de.unika.ipd.grGen.libGr;

    public class ConstantParser {
        ConstantParserHelper impl = null;
        bool noError;

        public void SetImpl(ConstantParserHelper impl)
        {
            this.impl = impl;
        }
    }
PARSER_END(ConstantParser)

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
|   < ATAT : "@@" >
}

TOKEN: {
    < ARRAY: "array" >
|   < DEQUE: "deque" >
|   < FALSE: "false" >
|   < MAP: "map" >
|   < NULL: "null" >
|   < SET: "set" >
|   < TRUE: "true" >
}

TOKEN: {
    < NUMBER: ("-")? (["0"-"9"])+ >
|   < NUMBER_BYTE: ("-")? (["0"-"9"])+ ("y"|"Y") >
|   < NUMBER_SHORT: ("-")? (["0"-"9"])+ ("s"|"S") >
|   < NUMBER_LONG: ("-")? (["0"-"9"])+ ("l"|"L") >
|
    < HEXNUMBER: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ >
|   < HEXNUMBER_BYTE: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("y"|"Y") >
|   < HEXNUMBER_SHORT: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("s"|"S") >
|   < HEXNUMBER_LONG: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("l"|"L") >
}

TOKEN: {
    < NUMFLOAT:
            ("-")? (["0"-"9"])+ ("." (["0"-"9"])+)? (<EXPONENT>)? ["f", "F"]
        |   ("-")? "." (["0"-"9"])+ (<EXPONENT>)? ["f", "F"]
    >
|
    < NUMDOUBLE:
            ("-")? (["0"-"9"])+ "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
        |   ("-")? "." (["0"-"9"])+ (<EXPONENT>)? (["d", "D"])?
        |   ("-")? (["0"-"9"])+ <EXPONENT> (["d", "D"])?
        |   ("-")? (["0"-"9"])+ ["d", "D"]
    >
|
    < #EXPONENT: ["e", "E"] (["+", "-"])? (["0"-"9"])+ >
}

TOKEN: {
    < DOUBLEQUOTEDTEXT : "\"" (~["\"", "\n", "\r"])* "\"" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|   < SINGLEQUOTEDTEXT : "\'" (~["\'", "\n", "\r"])* "\'" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|   < WORD : ~["\'", "\"", "0"-"9", "=", ":", ";", ".", ",", "+", "-", "&", "%", "?", "$", "|", "<", ">", "(", ")", "{", "}", "[", "]", "*", "!", "#", " ", "@", "\n", "\r"]
         (~["\'", "\"", "=", ":", ";", ".", ",", "+", "-", "&", "%", "?", "$", "|", "<", ">", "(", ")", "{", "}", "[", "]", "*", "!", "#", " ", "@", "\n", "\r"])*    >
}

SPECIAL_TOKEN: {
    < SINGLE_LINE_COMMENT: "#" (~["\n", "\r", "\u00A7"])* ("\u00A7")? > // \u00A7 == "§"
}

<WithinFilename> SKIP: {
    " " |
    "\t" |
    "\r"
}

<WithinFilename> TOKEN: {
    < DOUBLEQUOTEDFILENAME: "\"" (~["\"", "\n", "\r"])* "\"" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|   < SINGLEQUOTEDFILENAME: "\'" (~["\'", "\n", "\r"])* "\'" >
        { matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); } : DEFAULT
|   < FILENAME: ~["\'", "\"", "=", ";", "$", "|", "*", " ", "?", "\n", "\r"]
         (~["\'", "\"", "=", ";", "$", "|", "*", " ", "?", "\n", "\r"])* > : DEFAULT
|   < NLINFILENAME: "\n" > : DEFAULT
|   < DOUBLESEMICOLONINFILENAME: ";;" > : DEFAULT
|   < ERRORFILENAME: ~[] > : DEFAULT
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

String TypeName():
{
    String package=null, type;
}
{
    (LOOKAHEAD(2) package=WordOrText() "::")? type=WordOrText() { return package!=null ? package+"::"+type : type; }
}

object SimpleConstant():
{
    object constant = null;
    Token tok;
    string package, type, value = null;
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
        package=WordOrText() "::" type=AttributeName() ("::" value=AttributeName())?
        {
            if(value!=null)
            {
                EnumAttributeType attrType = TypesHelper.GetEnumAttributeType(package+"::"+type, impl.CurrentGraph.Model);
                if(attrType!=null)
                    constant = Enum.Parse(attrType.EnumType, value);
                if(constant==null)
                    throw new ParseException("Invalid constant \""+package+"::"+type+"::"+value+"\"!");
            }
            else
            {
                value = type;
                type = package;
                EnumAttributeType attrType = TypesHelper.GetEnumAttributeType(type, impl.CurrentGraph.Model);
                if(attrType!=null)
                    constant = Enum.Parse(attrType.EnumType, value);
                if(constant==null)
                    throw new ParseException("Invalid constant \""+type+"::"+value+"\"!");
            }
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
        "set" "<" typeName=TypeName() ">"
        {
            srcType = TypesHelper.GetType(typeName, impl.CurrentGraph.Model);
            dstType = typeof(SetValueType);
            if(srcType!=null)
                constant = ContainerHelper.NewDictionary(srcType, dstType);
            if(constant==null)
                throw new ParseException("Invalid constant \"set<"+typeName+">\"!");
        }
        "{"
            ( src=SimpleConstant() { ((IDictionary)constant)[src] = null; } )?
                ( "," src=SimpleConstant() { ((IDictionary)constant)[src] = null; })*
        "}"
    |
        "map" "<" typeName=TypeName() "," typeNameDst=TypeName() ">"
        {
            srcType = TypesHelper.GetType(typeName, impl.CurrentGraph.Model);
            dstType = TypesHelper.GetType(typeNameDst, impl.CurrentGraph.Model);
            if(srcType!=null && dstType!=null)
                constant = ContainerHelper.NewDictionary(srcType, dstType);
            if(constant==null)
                throw new ParseException("Invalid constant \"map<"+typeName+","+typeNameDst+">\"!");
        }
        "{"
            ( src=SimpleConstant() "->" dst=SimpleConstant() { ((IDictionary)constant)[src] = dst; } )?
                ( "," src=SimpleConstant() "->" dst=SimpleConstant() { ((IDictionary)constant)[src] = dst; } )*
        "}"
    |
        "array" "<" typeName=TypeName() ">"
        {
            srcType = TypesHelper.GetType(typeName, impl.CurrentGraph.Model);
            if(srcType!=null)
                constant = ContainerHelper.NewList(srcType);
            if(constant==null)
                throw new ParseException("Invalid constant \"array<"+typeName+">\"!");
        }
        "["
            ( src=SimpleConstant() { ((IList)constant).Add(src); } )?
                ( "," src=SimpleConstant() { ((IList)constant).Add(src); })*
        "]"
    |
        "deque" "<" typeName=TypeName() ">"
        {
            srcType = TypesHelper.GetType(typeName, impl.CurrentGraph.Model);
            if(srcType!=null)
                constant = ContainerHelper.NewDeque(srcType);
            if(constant==null)
                throw new ParseException("Invalid constant \"deque<"+typeName+">\"!");
        }
        "["
            ( src=SimpleConstant() { ((IDeque)constant).Enqueue(src); } )?
                ( "," src=SimpleConstant() { ((IDeque)constant).Enqueue(src); })*
        "]"
    )
    {
        return constant;
    }
}

void LineEnd():
{}
{
    (<NL> | <DOUBLESEMICOLON> | <EOF>)
}

////////////////////
// Error recovery //
////////////////////

CSHARPCODE
void errorSkip(ParseException ex) {
    ConsoleUI.outWriter.WriteLine(ex.Message);
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
