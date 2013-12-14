options {
	STATIC = false;
	IGNORE_CASE = false;
}

PARSER_BEGIN(SequenceParser)
	namespace de.unika.ipd.grGen.libGr.sequenceParser;
	using System;
	using System.IO;
	using System.Text;
	using System.Collections;
	using System.Collections.Generic;
	using de.unika.ipd.grGen.libGr;

	/// <summary>
	/// A parser class for xgrs strings.
	/// </summary>
	public class SequenceParser
	{
		/// <summary>
		/// The rules and sequences used in the specification, set if parsing an xgrs to be interpreted
		/// </summary>
		BaseActions actions;

		/// <summary>
		/// The names of the rules used in the specification, set if parsing an xgrs to be compiled
		/// </summary>
		String[] ruleNames;

		/// <summary>
		/// The names of the sequences used in the specification, set if parsing an xgrs to be compiled
		/// </summary>
		String[] sequenceNames;

		/// <summary>
		/// The names of the procedures used in the specification, set if parsing an xgrs to be compiled
		/// </summary>
		String[] procedureNames;

		/// <summary>
		/// The names of the functions used in the specification, set if parsing an xgrs to be compiled
		/// </summary>
		String[] functionNames;

		/// <summary>
		/// The output types of the functions used in the specification, set if parsing an xgrs to be compiled
		/// </summary>
		String[] functionOutputTypes;

		/// <summary>
		/// The model used in the specification
		/// </summary>
		IGraphModel model;

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
        /// <param name="actions">The BaseActions object containing the rules used in the string.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, BaseActions actions, List<String> warnings)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = actions;
			parser.ruleNames = null;
			parser.model = actions.Graph.Model;
			parser.varDecls = new SymbolTable();
			parser.varDecls.PushFirstScope(null);
			parser.warnings = warnings;
			Sequence seq = parser.XGRS();
			SequenceCheckingEnvironment env = new SequenceCheckingEnvironmentInterpreted(actions);
			seq.Check(env);
			return seq;
		}

        /// <summary>
        /// Parses a given string in sequence definition syntax and builds a SequenceDefinition object. Used for the interpreted xgrs.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="actions">The BaseActions object containing the rules used in the string.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static SequenceDefinition ParseSequenceDefinition(String sequenceStr, BaseActions actions, List<String> warnings)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = actions;
			parser.ruleNames = null;
			parser.model = actions.Graph.Model;
			parser.varDecls = new SymbolTable();
			parser.varDecls.PushFirstScope(null);
			parser.warnings = warnings;
			SequenceDefinition seq = parser.defXGRS();
			SequenceCheckingEnvironment env = new SequenceCheckingEnvironmentInterpreted(actions);
			seq.Check(env);
			return seq;
		}

        /// <summary>
        /// Parses a given string in xgrs syntax and builds a Sequence object. Used for the compiled xgrs.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="ruleNames">An array containing the names of the rules used in the specification.</param>
        /// <param name="sequenceNames">An array containing the names of the sequences used in the specification.</param>
        /// <param name="procedureNames">An array containing the names of the procedures used in the specification.</param>
        /// <param name="functionNames">An array containing the names of the functions used in the specification.</param>
        /// <param name="functionOutputTypes">An array containing the output types of the functions used in the specification.</param>
        /// <param name="predefinedVariables">A map from variables to types giving the parameters to the sequence, i.e. predefined variables.</param>
        /// <param name="model">The model used in the specification.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, String[] ruleNames, String[] sequenceNames,
				String[] procedureNames, String[] functionNames, String[] functionOutputTypes,
		        Dictionary<String, String> predefinedVariables, IGraphModel model, List<String> warnings)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = null;
			parser.ruleNames = ruleNames;
			parser.sequenceNames = sequenceNames;
			parser.procedureNames = procedureNames;
			parser.functionNames = functionNames;
			parser.functionOutputTypes = functionOutputTypes;
			parser.model = model;
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
|	< ASSIGN_TO: "=>" >
|	< BOR_TO: "|>" >
|	< BAND_TO: "&>" >
|	< COMMA: "," >
|	< DOLLAR: "$" >
|   < DOUBLEAMPERSAND: "&&" >
|	< AMPERSAND: "&" >
|   < DOUBLEPIPE: "||" >
|	< PIPE: "|" >
|   < CIRCUMFLEX: "^" >
|   < EQUALITY: "==" >
|   < INEQUALITY: "!=" >
|   < LOWEREQUAL: "<=" >
|   < GREATEREQUAL: ">=" >
|   < STRUCTURALEQUAL: "~~" >
|	< STAR: "*" >
|	< PLUS: "+" >
|	< DIV: "/" >
|	< BACKSLASH: "\\" >
|	< EXCLAMATIONMARK: "!" >
|	< LPARENTHESIS: "(" >
|	< RPARENTHESIS: ")" >
|	< LBOXBRACKET: "[" >
|	< RBOXBRACKET: "]" >
|   < LANGLE: "<" >
|   < RANGLE: ">" >
|   < LLANGLE: "<<" >
|   < RRANGLE: ">>" >
|   < LBRACE: "{" >
|   < RBRACE: "}" >
|	< COLON: ":" >
|	< DOUBLECOLON: "::" >
|   < PERCENT: "%" >
|   < QUESTIONMARK: "?" >
|	< AT : "@" >
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
|   < HIGHLIGHT: "highlight" >
|   < COUNT: "count" >
|   < THIS: "this" >
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
|
	< NUMBER: ("-")? (["0"-"9"])+ >
|	< NUMBER_BYTE: ("-")? (["0"-"9"])+ ("y"|"Y") >
|	< NUMBER_SHORT: ("-")? (["0"-"9"])+ ("s"|"S") >
|	< NUMBER_LONG: ("-")? (["0"-"9"])+ ("l"|"L") >
|
	< HEXNUMBER: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ >
|	< HEXNUMBER_BYTE: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("y"|"Y") >
|	< HEXNUMBER_SHORT: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("s"|"S") >
|	< HEXNUMBER_LONG: "0x" (["0"-"9", "a"-"f", "A"-"F"])+ ("l"|"L") >
|
	< DOUBLEQUOTEDTEXT : "\"" (~["\"", "\n", "\r"])* "\"" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|
	< SINGLEQUOTEDTEXT : "\'" (~["\'", "\n", "\r"])* "\'" >
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
	tok=<DOUBLEQUOTEDTEXT>
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
	string type, value;
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
		LOOKAHEAD(2)
		type=Word() "::" value=Word()
		{
			foreach(EnumAttributeType attrType in model.EnumAttributeTypes)
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
	|
		LOOKAHEAD({ GetToken(1).kind==WORD && varDecls.Lookup(GetToken(1).image)==null && TypesHelper.GetNodeOrEdgeType(GetToken(1).image, model)!=null})
		type=Word()
		{
			return TypesHelper.GetNodeOrEdgeType(type, model);
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
	SequenceExpression src = null, dst = null, res = null;
}
{
	(
		"set" "<" typeName=Word() ">" { srcItems = new List<SequenceExpression>(); }
		"{"
			( src=Expression() { srcItems.Add(src); } )?
				( "," src=Expression() { srcItems.Add(src); })*
		"}"
		{
			res = new SequenceExpressionSetConstructor(typeName, srcItems.ToArray());
		}
	|
		"map" "<" typeName=Word() "," typeNameDst=Word() ">" { srcItems = new List<SequenceExpression>(); dstItems = new List<SequenceExpression>(); }
		"{"
			( src=Expression() "->" dst=Expression() { srcItems.Add(src); dstItems.Add(dst); } )?
				( "," src=Expression() "->" dst=Expression() { srcItems.Add(src); dstItems.Add(dst); } )*
		"}"
		{
			res = new SequenceExpressionMapConstructor(typeName, typeNameDst, srcItems.ToArray(), dstItems.ToArray());
		}
	|
		"array" "<" typeName=Word() ">" { srcItems = new List<SequenceExpression>(); }
		"["
			( src=Expression() { srcItems.Add(src); } )?
				( "," src=Expression() { srcItems.Add(src); })*
		"]"
		{
			res = new SequenceExpressionArrayConstructor(typeName, srcItems.ToArray());
		}
	|
		"deque" "<" typeName=Word() ">" { srcItems = new List<SequenceExpression>(); }
		"]"
			( src=Expression() { srcItems.Add(src); } )?
				( "," src=Expression() { srcItems.Add(src); })*
		"["
		{
			res = new SequenceExpressionDequeConstructor(typeName, srcItems.ToArray());
		}
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
	(type=Word()
	| LOOKAHEAD("set" "<" Word() ">") "set" "<" typeParam=Word() ">" { type = "set<"+typeParam+">"; }
		(LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
	| LOOKAHEAD("map" "<" Word() "," Word() ">") "map" "<" typeParam=Word() "," typeParamDst=Word() ">" { type = "map<"+typeParam+","+typeParamDst+">"; }
		(LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
	| LOOKAHEAD("array" "<" Word() ">") "array" "<" typeParam=Word() ">" { type = "array<"+typeParam+">"; }
		(LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
	| LOOKAHEAD("deque" "<" Word() ">") "deque" "<" typeParam=Word() ">" { type = "deque<"+typeParam+">"; }
		(LOOKAHEAD(2) "]" { throw new ParseException("no ][ allowed at deque declaration, use d:deque<T> = deque<T>][ for initialization"); })?
	// for below: keep >= which is from generic type closing plus a following assignment, it's tokenized into '>=' if written without whitespace, we'll eat the >= at the assignment
	| LOOKAHEAD("set" "<" Word() ">=") "set" "<" typeParam=Word() { type = "set<"+typeParam+">"; }
		(LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
	| LOOKAHEAD("map" "<" Word() "," Word() ">=") "map" "<" typeParam=Word() "," typeParamDst=Word() { type = "map<"+typeParam+","+typeParamDst+">"; }
		(LOOKAHEAD(2) "{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
	| LOOKAHEAD("array" "<" Word() ">=") "array" "<" typeParam=Word() { type = "array<"+typeParam+">"; }
		(LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
	| LOOKAHEAD("deque" "<" Word() ">=") "deque" "<" typeParam=Word() { type = "deque<"+typeParam+">"; }
		(LOOKAHEAD(2) "]" { throw new ParseException("no ][ allowed at deque declaration, use d:deque<T> = deque<T>][ for initialization"); })?
	// the match type exists only for the loop variable of the for matches loop
	| LOOKAHEAD("match" "<" Word() ">") "match" "<" typeParam=Word() ">" { type = "match<"+typeParam+">"; }	
	)
	{
		return type;
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
	SequenceExpression expr, expr2 = null, expr3 = null;
	SequenceComputation comp;
	int num = 0;
	double numDouble = 0.0;
	String str, attrName = null;
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
    "<<" seq=Rule() (";;"|";") seq2=RewriteSequence() ">>"
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
		LOOKAHEAD(2) "in" "[" "?" seq=Rule() "]" ";" seq2=RewriteSequence()
			{ varDecls.PopScope(variableList1); } "}"
		{
			return new SequenceForMatch(fromVar, seq, seq2, variableList1);
		}
	|
		LOOKAHEAD(3) "in" str=Word() "(" (Arguments(argExprs))? ")" ";" seq=RewriteSequence()
			{ varDecls.PopScope(variableList1); } "}"
		{
			if(str=="adjacent") {
				return new SequenceForFunction(fromVar, SequenceType.ForAdjacentNodes, argExprs, seq, variableList1);
			} else if(str=="adjacentIncoming") {
				return new SequenceForFunction(fromVar, SequenceType.ForAdjacentNodesViaIncoming, argExprs, seq, variableList1);
			} else if(str=="adjacentOutgoing") {
				return new SequenceForFunction(fromVar, SequenceType.ForAdjacentNodesViaOutgoing, argExprs, seq, variableList1);
			} else if(str=="incident") {
				return new SequenceForFunction(fromVar, SequenceType.ForIncidentEdges, argExprs, seq, variableList1);
			} else if(str=="incoming") {
				return new SequenceForFunction(fromVar, SequenceType.ForIncomingEdges, argExprs, seq, variableList1);
			} else if(str=="outgoing") {
				return new SequenceForFunction(fromVar, SequenceType.ForOutgoingEdges, argExprs, seq, variableList1);
			} else if(str=="reachable") {
				return new SequenceForFunction(fromVar, SequenceType.ForReachableNodes, argExprs, seq, variableList1);
			} else if(str=="reachableIncoming") {
				return new SequenceForFunction(fromVar, SequenceType.ForReachableNodesViaIncoming, argExprs, seq, variableList1);
			} else if(str=="reachableOutgoing") {
				return new SequenceForFunction(fromVar, SequenceType.ForReachableNodesViaOutgoing, argExprs, seq, variableList1);
			} else if(str=="reachableEdges") {
				return new SequenceForFunction(fromVar, SequenceType.ForReachableEdges, argExprs, seq, variableList1);
			} else if(str=="reachableEdgesIncoming") {
				return new SequenceForFunction(fromVar, SequenceType.ForReachableEdgesViaIncoming, argExprs, seq, variableList1);
			} else if(str=="reachableEdgesOutgoing") {
				return new SequenceForFunction(fromVar, SequenceType.ForReachableEdgesViaOutgoing, argExprs, seq, variableList1);
			} else if(str=="nodes") {
				return new SequenceForFunction(fromVar, SequenceType.ForNodes, argExprs, seq, variableList1);
			} else if(str=="edges") {
				return new SequenceForFunction(fromVar, SequenceType.ForEdges, argExprs, seq, variableList1);
			}
		}
	|
		("->" fromVar2=Variable())? "in" fromVar3=VariableUse() ";" seq=RewriteSequence()
			{ varDecls.PopScope(variableList1); } "}"
		{
			return new SequenceForContainer(fromVar, fromVar2, fromVar3, seq, variableList1);
		}
	)
|
	"highlight" "(" str=Text() ")"
	{
		return new SequenceHighlight(str);
	}
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
	LOOKAHEAD(VariableDefinition())
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
	SequenceExpression fromExpr;
	String attrName;
}
{
	"yield" toVar=VariableUse()
	{
		return new AssignmentTargetYieldingVar(toVar);
	}
|
	LOOKAHEAD(VariableUse() "." "visited" "[" Expression() "]")
	toVar=VariableUse() "." "visited" "[" fromExpr=Expression() "]"
	{
		return new AssignmentTargetVisited(toVar, fromExpr);
	}
|
	LOOKAHEAD(VariableUse() "." Word())
	toVar=VariableUse() "." attrName=Word()
	( "[" fromExpr=Expression() "]" 
		{ return new AssignmentTargetAttributeIndexed(toVar, attrName, fromExpr); }
	)? // todo: this should be a composition of the two targets, not a fixed special one
    {
        return new AssignmentTargetAttribute(toVar, attrName);
    }
|
	LOOKAHEAD(VariableUse() "[" Expression() "]")
	toVar=VariableUse() "[" fromExpr=Expression() "]"
	{
		return new AssignmentTargetIndexedVar(toVar, fromExpr);
	}
|
	toVar=Variable()
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

SequenceExpression ExpressionAdd():
{
	SequenceExpression seq, seq2;
}
{
	seq=ExpressionUnary() ( "+" seq2=ExpressionUnary() { seq = new SequenceExpressionPlus(seq, seq2); } 
						  | "-" seq2=ExpressionUnary() { seq = new SequenceExpressionMinus(seq, seq2); }
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
	LOOKAHEAD(VariableUse() "." "visited")
	fromVar=VariableUse() "." "visited" "[" expr=Expression() "]"
	{
		return new SequenceExpressionIsVisited(fromVar, expr);
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
	expr=InitContainerExpr()
	{
		return expr;
	}
|
	"def" "(" Arguments(argExprs) ")"
	{
		return new SequenceExpressionDef(argExprs.ToArray());
	}
|
	fromVar=VariableUse() { expr = new SequenceExpressionVariable(fromVar); }
	(LOOKAHEAD({ GetToken(1).kind==LBOXBRACKET && // we're at a deque end or at an indexed access?
		( GetToken(2).kind!=THENLEFT && GetToken(2).kind!=THENRIGHT
			&& GetToken(2).kind!=DOUBLEPIPE && GetToken(2).kind!=DOUBLEAMPERSAND 
			&& GetToken(2).kind!=PIPE && GetToken(2).kind!=CIRCUMFLEX 
			&& GetToken(2).kind!=AMPERSAND && GetToken(2).kind!=PLUS
			&& GetToken(2).kind!=RPARENTHESIS && GetToken(2).kind!=RBOXBRACKET
			&& GetToken(2).kind!=EOF
		)
		|| GetToken(1).kind==DOT}) 
		expr=SelectorExpression(expr)
	)*
	{	
		return expr;
	}
|
	"@" "(" elemName=Text() ")"
	{
		return new SequenceExpressionElementFromGraph(elemName);
	}
|
	"this"
	{
		return new SequenceExpressionThis();
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
	SequenceExpression fromExpr2 = null;
	List<SequenceExpression> argExprs = new List<SequenceExpression>();
}
{
	"." methodOrAttrName=Word()
	(
		"(" (Arguments(argExprs))? ")"
		{
			if(methodOrAttrName=="size") {
				if(argExprs.Count!=0) throw new ParseException("\"" + methodOrAttrName + "\" expects no parameters)");
				return new SequenceExpressionContainerSize(fromExpr);
			} else if(methodOrAttrName=="empty") {
				if(argExprs.Count!=0) throw new ParseException("\"" + methodOrAttrName + "\" expects no parameters)");
				return new SequenceExpressionContainerEmpty(fromExpr);
			} else if(methodOrAttrName=="peek") {
				if(argExprs.Count!=0 && argExprs.Count!=1) throw new ParseException("\"" + methodOrAttrName + "\" expects none or one parameter)");
				return new SequenceExpressionContainerPeek(fromExpr, argExprs.Count!=0 ? argExprs[0] : null);
			} else {
				return new SequenceExpressionFunctionMethodCall(fromExpr, CreateFunctionMethodInvocationParameterBindings(methodOrAttrName, argExprs));
			}
		}
	|
		{
			if(fromExpr is SequenceExpressionVariable && ((SequenceExpressionVariable)fromExpr).Variable.Type.StartsWith("match<"))
				return new SequenceExpressionMatchAccess(((SequenceExpressionVariable)fromExpr).Variable, methodOrAttrName);
			else
				return new SequenceExpressionAttributeAccess(((SequenceExpressionVariable)fromExpr).Variable, methodOrAttrName);
		}
	)
|
	"[" fromExpr2=Expression() "]"
	{
		return new SequenceExpressionContainerAccess(fromExpr, fromExpr2);
	}
}
			
SequenceComputation ProcedureOrMethodCall():
{
	String procedure;
	SequenceVariable fromVar = null;
	String attrName = null;
	List<SequenceExpression> argExprs = new List<SequenceExpression>();
	List<SequenceVariable> returnVars = new List<SequenceVariable>();
}
{
	("(" VariableList(returnVars) ")" "=" )?
		(LOOKAHEAD(2) fromVar=VariableUse() "." (LOOKAHEAD(2) attrName=Word() ".")?)? procedure=Word() "(" (Arguments(argExprs))? ")"
	{
		if(fromVar==null) // procedure call
		{
			if(procedure=="valloc") {
				if(argExprs.Count!=0) throw new ParseException("\"" + procedure + "\" expects no parameters)");
				return new SequenceComputationBuiltinProcedureCall(new SequenceComputationVAlloc(), returnVars);
			} else if(procedure=="vfree") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
				return new SequenceComputationVFree(getArgument(argExprs, 0), true);
			} else if(procedure=="vfreenonreset") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
				return new SequenceComputationVFree(getArgument(argExprs, 0), false);
			} else if(procedure=="vreset") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
				return new SequenceComputationVReset(getArgument(argExprs, 0));
			} else if(procedure=="emit") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
				return new SequenceComputationEmit(getArgument(argExprs, 0));
			} else if(procedure=="record") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
				return new SequenceComputationRecord(getArgument(argExprs, 0));
			} else if(procedure=="add") {
				if(argExprs.Count!=1 && argExprs.Count!=3) throw new ParseException("\"" + procedure + "\" expects 1(for a node) or 3(for an edge) parameters)");
				return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGraphAdd(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2)), returnVars);
			} else if(procedure=="rem") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
				return new SequenceComputationGraphRem(getArgument(argExprs, 0));
			} else if(procedure=="clear") {
				if(argExprs.Count!=0) throw new ParseException("\"" + procedure + "\" expects no parameters)");
				return new SequenceComputationGraphClear();
			} else if(procedure=="retype") {
				if(argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 2 (graph entity, new type) parameters)");
				return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGraphRetype(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
			} else if(procedure=="addCopy") {
				if(argExprs.Count!=1 && argExprs.Count!=3) throw new ParseException("\"" + procedure + "\" expects 1(for a node) or 3(for an edge) parameters)");
				return new SequenceComputationBuiltinProcedureCall(new SequenceComputationGraphAddCopy(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2)), returnVars);
			} else if(procedure=="merge") {
				if(argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 2 (the nodes to merge) parameters)");
				return new SequenceComputationGraphMerge(getArgument(argExprs, 0), getArgument(argExprs, 1));
			} else if(procedure=="redirectSource") {
				if(argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 2 (edge to redirect, new source node) parameters)");
				return new SequenceComputationGraphRedirectSource(getArgument(argExprs, 0), getArgument(argExprs, 1));
			} else if(procedure=="redirectTarget") {
				if(argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 2 (edge to redirect, new target node) parameters)");
				return new SequenceComputationGraphRedirectTarget(getArgument(argExprs, 0), getArgument(argExprs, 1));
			} else if(procedure=="redirectSourceAndTarget") {
				if(argExprs.Count!=3) throw new ParseException("\"" + procedure + "\" expects 3 (edge to redirect, new source node, new target node) parameters)");
				return new SequenceComputationGraphRedirectSourceAndTarget(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2));
			} else if(procedure=="insert") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 (graph to destroyingly insert) parameter)");
				return new SequenceComputationInsert(getArgument(argExprs, 0));
			} else if(procedure=="insertCopy") {
				if(argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 2 (graph and one node to return the clone of) parameters)");
				return new SequenceComputationBuiltinProcedureCall(new SequenceComputationInsertCopy(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
			} else if(procedure=="insertInduced") {
				if(argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 2 parameters (the set of nodes to compute the induced subgraph from which will be cloned and inserted, and one node of the set of which the clone will be returned)");
				return new SequenceComputationBuiltinProcedureCall(new SequenceComputationInsertInduced(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
			} else if(procedure=="insertDefined") {
				if(argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 2 parameters (the set of edges which define the subgraph which will be cloned and inserted, and one edge of the set of which the clone will be returned)");
				return new SequenceComputationBuiltinProcedureCall(new SequenceComputationInsertDefined(getArgument(argExprs, 0), getArgument(argExprs, 1)), returnVars);
			} else if(procedure=="export") {
				if(argExprs.Count!=1 && argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 1 (name of file only) or 2 (graph to export, name of file) parameters)");
				return new SequenceComputationExport(getArgument(argExprs, 0), getArgument(argExprs, 1));
			} else if(procedure=="deleteFile") {
				if(argExprs.Count!=1) throw new ParseException("\"" + procedure + "\" expects 1 (the path of the file) parameter)");
				return new SequenceComputationDeleteFile(getArgument(argExprs, 0));
			} else {
				if(IsProcedureName(procedure)) {
					return new SequenceComputationProcedureCall(CreateProcedureInvocationParameterBindings(procedure, argExprs, returnVars));
				} else {
					throw new ParseException("Unknown procedure name: \"" + procedure + "\"! (available are valloc|vfree|vfreenonreset|vreset|emit|record|export|deleteFile|add|addCopy|rem|clear|retype|merge|redirectSource|redirectTarget|redirectSourceAndTarget|insert|insertCopy|insertInduced|insertDefined or one of the procedures defined in the .grg: " + GetProcedureNames() + ")");
				}
			}
		} else { // method call
			if(attrName==null)
			{
				if(procedure=="add") {
					if(argExprs.Count!=1 && argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 1(for set,deque,array end) or 2(for map,array with index) parameters)");
					return new SequenceComputationContainerAdd(fromVar, argExprs[0], argExprs.Count==2 ? argExprs[1] : null);
				} else if(procedure=="rem") {
					if(argExprs.Count>1) throw new ParseException("\"" + procedure + "\" expects 1(for set,map,array with index) or 0(for deque,array end) parameters )");
					return new SequenceComputationContainerRem(fromVar, argExprs.Count==1 ? argExprs[0] : null);
				} else if(procedure=="clear") {
					if(argExprs.Count>0) throw new ParseException("\"" + procedure + "\" expects no parameters)");
					return new SequenceComputationContainerClear(fromVar);
				} else {
					return new SequenceComputationProcedureMethodCall(fromVar, CreateProcedureMethodInvocationParameterBindings(procedure, argExprs, returnVars));
				}
			} else { // attribute method call
				SequenceExpressionAttributeAccess attrAcc = new SequenceExpressionAttributeAccess(fromVar, attrName);
				if(procedure=="add") {
					if(argExprs.Count!=1 && argExprs.Count!=2) throw new ParseException("\"" + procedure + "\" expects 1(for set,deque,array end) or 2(for map,array with index) parameters)");
					return new SequenceComputationContainerAdd(attrAcc, argExprs[0], argExprs.Count==2 ? argExprs[1] : null);
				} else if(procedure=="rem") {
					if(argExprs.Count>1) throw new ParseException("\"" + procedure + "\" expects 1(for set,map,array with index) or 0(for deque,array end) parameters )");
					return new SequenceComputationContainerRem(attrAcc, argExprs.Count==1 ? argExprs[0] : null);
				} else if(procedure=="clear") {
					if(argExprs.Count>0) throw new ParseException("\"" + procedure + "\" expects no parameters)");
					return new SequenceComputationContainerClear(attrAcc);
				} else {
					return new SequenceComputationProcedureMethodCall(attrAcc, CreateProcedureMethodInvocationParameterBindings(procedure, argExprs, returnVars));
				}
			}
		}
    }
}

SequenceExpression FunctionCall():
{
	String function;
	List<SequenceExpression> argExprs = new List<SequenceExpression>();
}
{
	function=Word() "(" (Arguments(argExprs))? ")"
	{
		if(function=="nodes") {
			if(argExprs.Count>1) throw new ParseException("\"" + function + "\" expects 1 parameter (node type) or none (to get all nodes)");
			return new SequenceExpressionNodes(getArgument(argExprs, 0));
		} else if(function=="edges") {
			if(argExprs.Count>1) throw new ParseException("\"" + function + "\" expects 1 parameter (edge type) or none (to get all edges)");
			return new SequenceExpressionEdges(getArgument(argExprs, 0));
		} else if(function=="empty") {
			if(argExprs.Count>0) throw new ParseException("\"" + function + "\" expects no parameters");
			return new SequenceExpressionEmpty();
		} else if(function=="size") {
			if(argExprs.Count>0) throw new ParseException("\"" + function + "\" expects no parameters");
			return new SequenceExpressionSize();
		} else if(function=="adjacent") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.AdjacentNodes);
		} else if(function=="adjacentIncoming") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.AdjacentNodesViaIncoming);
		} else if(function=="adjacentOutgoing") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.AdjacentNodesViaOutgoing);
		} else if(function=="incident") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.IncidentEdges);
		} else if(function=="incoming") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.IncomingEdges);
		} else if(function=="outgoing") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.OutgoingEdges);
		} else if(function=="reachable") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableNodes);
		} else if(function=="reachableIncoming") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableNodesViaIncoming);
		} else if(function=="reachableOutgoing") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableNodesViaOutgoing);
		} else if(function=="reachableEdges") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableEdges);
		} else if(function=="reachableEdgesIncoming") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableEdgesViaIncoming);
		} else if(function=="reachableEdgesOutgoing") {
			if(argExprs.Count<1 || argExprs.Count>3) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), SequenceExpressionType.ReachableEdgesViaOutgoing);
		} else if(function=="isAdjacent") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsAdjacentNodes);
		} else if(function=="isAdjacentIncoming") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsAdjacentNodesViaIncoming);
		} else if(function=="isAdjacentOutgoing") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsAdjacentNodesViaOutgoing);
		} else if(function=="isIncident") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsIncidentEdges);
		} else if(function=="isIncoming") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsIncomingEdges);
		} else if(function=="isOutgoing") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsAdjacentIncident(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsOutgoingEdges);
		} else if(function=="isReachable") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableNodes);
		} else if(function=="isReachableIncoming") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableNodesViaIncoming);
		} else if(function=="isReachableOutgoing") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableNodesViaOutgoing);
		} else if(function=="isReachableEdges") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableEdges);
		} else if(function=="isReachableEdgesIncoming") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableEdgesViaIncoming);
		} else if(function=="isReachableEdgesOutgoing") {
			if(argExprs.Count<2 || argExprs.Count>4) throw new ParseException("\"" + function + "\" expects 2 (start node, end node) or 3 (start node, end node, incident edge type) or 4 (start node, end node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionIsReachable(getArgument(argExprs, 0), getArgument(argExprs, 1), getArgument(argExprs, 2), getArgument(argExprs, 3), SequenceExpressionType.IsReachableEdgesViaOutgoing);
		} else if(function=="inducedSubgraph") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the set of nodes to construct the induced subgraph from)");
			return new SequenceExpressionInducedSubgraph(getArgument(argExprs, 0));
		} else if(function=="definedSubgraph") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the set of edges to construct the defined subgraph from)");
			return new SequenceExpressionDefinedSubgraph(getArgument(argExprs, 0));
		} else if(function=="source") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the edge to get the source node from)");
			return new SequenceExpressionSource(getArgument(argExprs, 0));
		} else if(function=="target") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the edge to get the target node from)");
			return new SequenceExpressionTarget(getArgument(argExprs, 0));
		} else if(function=="opposite") {
			if(argExprs.Count!=2) throw new ParseException("\"" + function + "\" expects 2 parameters (the edge and the node to get the opposite node from)");
			return new SequenceExpressionOpposite(getArgument(argExprs, 0), getArgument(argExprs, 1));
		} else if(function=="nameof") {
			if(argExprs.Count>1) throw new ParseException("\"" + function + "\" expects none (for the name of the current graph) or 1 parameter (for the name of the node/edge/subgraph given as parameter)");
			return new SequenceExpressionNameof(getArgument(argExprs, 0));
		} else if(function=="existsFile") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the path as string)");
			return new SequenceExpressionExistsFile(getArgument(argExprs, 0));
		} else if(function=="import") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the path as string to the grs file containing the subgraph to import)");
			return new SequenceExpressionImport(getArgument(argExprs, 0));
		} else if(function=="copy") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the subgraph to copy)");
			return new SequenceExpressionCopy(getArgument(argExprs, 0));
		} else if(function=="random") {
			if(argExprs.Count>1) throw new ParseException("\"" + function + "\" expects none (returns double in [0..1[) or 1 parameter (returns int in [0..parameter[)");
			return new SequenceExpressionRandom(getArgument(argExprs, 0));
		} else if(function=="canonize") {
			if(argExprs.Count!=1) throw new ParseException("\"" + function + "\" expects 1 parameter (the graph to generate the canonical string representation for)");
			return new SequenceExpressionCanonize(getArgument(argExprs, 0));
		} else {
			if(IsFunctionName(function)) {
				return new SequenceExpressionFunctionCall(CreateFunctionInvocationParameterBindings(function, argExprs));
			} else {
				if(function=="valloc" || function=="add" || function=="retype" || function=="insertInduced" || function=="insertDefined") {
					throw new ParseException("\"" + function + "\" is a procedure, call with (var)=" + function + "();");
				} else {
					throw new ParseException("Unknown function name: \"" + function + "\"! (available are nodes|edges|empty|size|adjacent|adjacentIncoming|adjacentOutgoing|incident|incoming|outgoing|reachable|reachableIncoming|reachableOutgoing|reachableEdges|reachableEdgesIncoming|reachableEdgesOutgoing|isAdjacent|isAdjacentIncoming|isAdjacentOutgoing|isIncident|isIncoming|isOutgoing|isReachable|isReachableIncoming|isReachableOutgoing|isReachableEdges|isReachableEdgeIncoming|isReachableEdgesOutgoing|inducedSubgraph|definedSubgraph|source|target|opposite|nameof|existsFile|import|copy|random|canonize or one of the functions defined in the .grg:" + GetFunctionNames() + ")");
				}
			}
		}
    }
}

void RuleLookahead():
{
}
{
	("(" ( Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">" | "array" "<" Word() ">" | "deque" "<" Word() ">"))? | "::" Word() ) 
			("," ( Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">" | "array" "<" Word() ">" | "deque" "<" Word() ">"))? | "::" Word() ) )* ")" "=")?
	(
	    ( "$" ("%")? ( Variable() ("," (Variable() | "*"))? )? )? "["
	|
	    ( "%" | "?" )* (LOOKAHEAD(2) Word() |  Variable() ".")
	|
		"count" "["
	)
}

Sequence Rule():
{
	bool special = false, test = false;
	String str;
	FilterCall filter = null;
	bool chooseRandSpecified = false, chooseRandSpecified2 = false, choice = false;
	SequenceVariable varChooseRand = null, varChooseRand2 = null, subgraph = null, countResult = null;
	List<SequenceExpression> argExprs = new List<SequenceExpression>();
	List<SequenceVariable> returnVars = new List<SequenceVariable>();
	List<FilterCall> filters = new List<FilterCall>();
}
{
	("(" VariableList(returnVars) ")" "=" )?
	(
		(
			"$" ("%" { choice = true; })? ( varChooseRand=Variable() ("," (varChooseRand2=Variable() | "*") { chooseRandSpecified2 = true; })? )? { chooseRandSpecified = true; }
		)?
		"[" ("%" { special = true; } | "?" { test = true; })* 
		(LOOKAHEAD(2) subgraph=Variable() ".")? str=Word() ("(" (Arguments(argExprs))? ")")?
			("\\" filter=Filter() { filters.Add(filter); })*
		"]"
		{
			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			return new SequenceRuleAllCall(CreateRuleInvocationParameterBindings(str, argExprs, returnVars, subgraph),
					special, test, chooseRandSpecified, varChooseRand, chooseRandSpecified2, varChooseRand2, choice, filters);
		}
	|
		"count"
		"[" ("%" { special = true; } | "?" { test = true; })* 
		(LOOKAHEAD(2) subgraph=Variable() ".")? str=Word() ("(" (Arguments(argExprs))? ")")?
			("\\" filter=Filter() { filters.Add(filter); })*
		"]" "=>" countResult=Variable()
		{
			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			return new SequenceRuleCountAllCall(CreateRuleInvocationParameterBindings(str, argExprs, returnVars, subgraph),
					special, test, countResult, filters);
		}
	|
		("%" { special = true; } | "?" { test = true; })*
		(LOOKAHEAD(2) subgraph=Variable() ".")? str=Word() ("(" (Arguments(argExprs))? ")")? // if only str is given, this might be a variable predicate; but this is decided later on in resolve
			("\\" filter=Filter() { filters.Add(filter); })*
		{
			if(argExprs.Count==0 && returnVars.Count==0)
			{
				SequenceVariable var = varDecls.Lookup(str);
				if(var!=null)
				{
					if(var.Type!="" && var.Type!="boolean")
						throw new SequenceParserException(str, "untyped or bool", var.Type);
					if(filters.Count > 0)
						throw new SequenceParserException(str, filter.ToString(), SequenceParserError.FilterError);
					if(subgraph!=null)
						throw new SequenceParserException(str, "", SequenceParserError.SubgraphError);
					return new SequenceBooleanComputation(new SequenceExpressionVariable(var), null, special);
				}
			}

			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			if(IsSequenceName(str)) {
				if(filters.Count > 0)
					throw new SequenceParserException(str, FiltersToString(filters), SequenceParserError.FilterError);
				return new SequenceSequenceCall(
								CreateSequenceInvocationParameterBindings(str, argExprs, returnVars, subgraph),
								special);
			} else {
				return new SequenceRuleCall(
								CreateRuleInvocationParameterBindings(str, argExprs, returnVars, subgraph),
								special, test, filters);
			}
		}
	)
}

FilterCall Filter() :
{
	String filterBase, filterVariable;
	List<SequenceExpression> argExprs = new List<SequenceExpression>();
}
{
	LOOKAHEAD(2) filterBase=Word() "<" filterVariable=Word() ">"
		{
			if(filterBase!="orderAscendingBy" && filterBase!="orderDescendingBy" && filterBase!="groupBy"
				&& filterBase!="keepSameAsFirst" && filterBase!="keepSameAsLast" && filterBase!="keepOneForEach")
				throw new ParseException("Unknown def-variable-based filter " + filterBase + "! Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach.");
			else
				return new FilterCall(filterBase, filterVariable, true);
		}
|
	filterBase=Word() ("(" (Arguments(argExprs))? ")")?
		{
			if(filterBase=="keepFirst" || filterBase=="keepLast"
				|| filterBase=="keepFirstFraction" || filterBase=="keepLastFraction")
			{
				if(argExprs.Count!=1)
					throw new ParseException("The auto-supplied filter " + filterBase + " expects exactly one parameter!");
				return new FilterCall(filterBase, argExprs[0]);
			}
			else
			{
				if(filterBase=="auto")
					return new FilterCall("auto", null, true);
				return new FilterCall(filterBase, argExprs);
			}
		}
}

CSHARPCODE
RuleInvocationParameterBindings CreateRuleInvocationParameterBindings(String ruleName,
				List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph)
{
	IAction action = null;
	if(actions != null) {
		action = actions.GetAction(ruleName);
		if(action == null)
			throw new Exception("Unknown rule: " + ruleName);
	}

	RuleInvocationParameterBindings paramBindings = new RuleInvocationParameterBindings(action,
			argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray(), subgraph);

	if(action == null)
		paramBindings.Name = ruleName;

	return paramBindings;
}

CSHARPCODE
SequenceInvocationParameterBindings CreateSequenceInvocationParameterBindings(String sequenceName,
				List<SequenceExpression> argExprs, List<SequenceVariable> returnVars, SequenceVariable subgraph)
{
	SequenceDefinition sequenceDef = null;
	if(actions != null) {
		sequenceDef = actions.RetrieveGraphRewriteSequenceDefinition(sequenceName);
	}

	SequenceInvocationParameterBindings paramBindings = new SequenceInvocationParameterBindings(sequenceDef,
			argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray(), subgraph);

	if(sequenceDef == null)
		paramBindings.Name = sequenceName;

	return paramBindings;
}

CSHARPCODE
ProcedureInvocationParameterBindings CreateProcedureInvocationParameterBindings(String procedureName,
				List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
{
	ProcedureInfo procedureDef = null;
	if(actions != null) {
		procedureDef = actions.RetrieveProcedureDefinition(procedureName);
	}

	ProcedureInvocationParameterBindings paramBindings = new ProcedureInvocationParameterBindings(procedureDef,
			argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());

	if(procedureDef == null)
		paramBindings.Name = procedureName;

	return paramBindings;
}

CSHARPCODE
ProcedureInvocationParameterBindings CreateProcedureMethodInvocationParameterBindings(String procedureName,
				List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
{
	ProcedureInvocationParameterBindings paramBindings = new ProcedureInvocationParameterBindings(null,
			argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());

	paramBindings.Name = procedureName;

	return paramBindings;
}

CSHARPCODE
FunctionInvocationParameterBindings CreateFunctionInvocationParameterBindings(String functionName,
				List<SequenceExpression> argExprs)
{
	FunctionInfo functionDef = null;
	if(actions != null) {
		functionDef = actions.RetrieveFunctionDefinition(functionName);
	}

	FunctionInvocationParameterBindings paramBindings = new FunctionInvocationParameterBindings(functionDef,
			argExprs.ToArray(), new object[argExprs.Count]);

	if(functionDef == null)
	{
		paramBindings.Name = functionName;
		for(int i=0; i<functionNames.Length; ++i)
			if(functionNames[i] == functionName)
				paramBindings.ReturnType = functionOutputTypes[i];
	}

	return paramBindings;
}

CSHARPCODE
FunctionInvocationParameterBindings CreateFunctionMethodInvocationParameterBindings(String functionMethodName,
				List<SequenceExpression> argExprs)
{
	FunctionInvocationParameterBindings paramBindings = new FunctionInvocationParameterBindings(null,
			argExprs.ToArray(), new object[argExprs.Count]);

	paramBindings.Name = functionMethodName;
	paramBindings.ReturnType = "";

	return paramBindings;
}

CSHARPCODE
bool IsSequenceName(String ruleOrSequenceName)
{
	if(actions != null) {
		return actions.RetrieveGraphRewriteSequenceDefinition(ruleOrSequenceName) != null;
	} else {
		foreach(String sequenceName in sequenceNames)
			if(ruleOrSequenceName == sequenceName)
				return true;
		return false;
	}
}

CSHARPCODE
bool IsFunctionName(String functionName)
{
	if(actions != null) {
		return actions.RetrieveFunctionDefinition(functionName) != null;
	} else {
		foreach(String funcName in functionNames)
			if(funcName == functionName)
				return true;
		return false;
	}
}

CSHARPCODE
string GetFunctionNames()
{
	if(actions != null) {
		return actions.FunctionNames;
	} else {
		StringBuilder sb = new StringBuilder();
		bool first = true;
		foreach(String funcName in functionNames)
		{
			if(first)
				first = false;
			else
				sb.Append(",");
			sb.Append(funcName);
		}
		return sb.ToString();
	}
}

CSHARPCODE
bool IsProcedureName(String procedureName)
{
	if(actions != null) {
		return actions.RetrieveProcedureDefinition(procedureName) != null;
	} else {
		foreach(String procName in procedureNames)
			if(procName == procedureName)
				return true;
		return false;
	}
}

CSHARPCODE
string GetProcedureNames()
{
	if(actions != null) {
		return actions.ProcedureNames;
	} else {
		StringBuilder sb = new StringBuilder();
		bool first = true;
		foreach(String procName in procedureNames)
		{
			if(first)
				first = false;
			else
				sb.Append(",");
			sb.Append(procName);
		}
		return sb.ToString();
	}
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
SequenceExpression getArgument(List<SequenceExpression> argExprs, int index)
{
	if(index < argExprs.Count)
		return argExprs[index];
	else // optional argument, is not parsed into list, function constructor requires null value
		return null;
}

CSHARPCODE
String FiltersToString(List<FilterCall> filters)
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
