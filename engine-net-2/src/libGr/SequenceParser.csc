options {
	STATIC = false;
	IGNORE_CASE = false;
}

PARSER_BEGIN(SequenceParser)
	namespace de.unika.ipd.grGen.libGr.sequenceParser;
	using System;
	using System.IO;
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
        /// <param name="predefinedVariables">A map from variables to types giving the parameters to the sequence, i.e. predefined variables.</param>
        /// <param name="model">The model used in the specification.</param>
        /// <param name="warnings">A list which receives the warnings generated during parsing.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, String[] ruleNames, String[] sequenceNames,
		        Dictionary<String, String> predefinedVariables, IGraphModel model, List<String> warnings)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = null;
			parser.ruleNames = ruleNames;
			parser.sequenceNames = sequenceNames;
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
|   < DEF: "def" >
|   < TRUE: "true" >
|   < FALSE: "false" >
|   < NULL: "null" >
|   < SET: "set" >
|   < MAP: "map" >
|   < ARRAY: "array" >
|   < ARROW: "->" >
|   < FOR: "for" >
|   < IF: "if" >
|   < IN: "in" >
|   < DOT: "." >
|   < THENLEFT: "<;" >
|   < THENRIGHT: ";>" >
|   < SEMI: ";" >
|   < DOUBLESEMI: ";;" >
|   < VISITED: "visited" >
|   < YIELD: "yield" >
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
			srcType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeName, model);
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
		"map" "<" typeName=Word() "," typeNameDst=Word() ">"
		{
			srcType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeName, model);
			dstType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeNameDst, model);
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
		"array" "<" typeName=Word() ">"
		{
			srcType = DictionaryListHelper.GetTypeFromNameForDictionaryOrList(typeName, model);
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
		("{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
	| LOOKAHEAD("map" "<" Word() "," Word() ">") "map" "<" typeParam=Word() "," typeParamDst=Word() ">" { type = "map<"+typeParam+","+typeParamDst+">"; }
		("{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
	| LOOKAHEAD("array" "<" Word() ">") "array" "<" typeParam=Word() ">" { type = "array<"+typeParam+">"; }
		(LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
	// for below: keep >= which is from generic type closing plus a following assignment, it's tokenized into '>=' if written without whitespace, we'll eat the >= at the assignment
	| LOOKAHEAD("set" "<" Word() ">=") "set" "<" typeParam=Word() { type = "set<"+typeParam+">"; }
		("{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
	| LOOKAHEAD("map" "<" Word() "," Word() ">=") "map" "<" typeParam=Word() "," typeParamDst=Word() { type = "map<"+typeParam+","+typeParamDst+">"; }
		("{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
	| LOOKAHEAD("array" "<" Word() ">=") "array" "<" typeParam=Word() { type = "array<"+typeParam+">"; }
		(LOOKAHEAD(2) "[" { throw new ParseException("no [] allowed at array declaration, use a:array<T> = array<T>[] for initialization"); })?
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
	SequenceVariable toVar, fromVar, fromVar2 = null, fromVar3 = null;
	SequenceExpression expr;
	SequenceComputation comp;
	int num = 0;
	String str;
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
        "$" ("%" { choice = true; } )? "(" num=Number() ")"
        {
            return new SequenceAssignRandomToVar(toVar, num, choice);
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
	seq=Rule() // accepts variables, rules, and all-bracketed rules
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
	( "$" { chooseRandSpecified=true; } ("%" { choice = true; } )? )?
		"{" "(" seq=Rule() { sequences.Add(seq); } ("," seq=Rule() { sequences.Add(seq); })* ")" "}"
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
	"for" "{" { varDecls.PushScope(ScopeType.For); } fromVar=Variable() ( ("->" fromVar2=Variable())? "in" fromVar3=VariableUse() )? ";"
		seq=RewriteSequence() { varDecls.PopScope(variableList1); } "}"
	{
        return new SequenceFor(fromVar, fromVar2, fromVar3, seq, variableList1);
    }
|
	("%" { special = true; })? "{" { varDecls.PushScope(ScopeType.Computation); } comp=CompoundComputation() { varDecls.PopScope(variableList1); } "}"
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
	// so it's pretty safe to assume it's a set/map/array declaration with the ">=" != ">""=" issue
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
	LOOKAHEAD(MethodCall())
	comp=MethodCallRepeated()
	{
		return comp;
	}
|
	LOOKAHEAD({ GetToken(1).kind==WORD && GetToken(2).kind==LPARENTHESIS 
				&& (GetToken(1).image=="vfree" || GetToken(1).image=="vreset"
					|| GetToken(1).image=="emit" || GetToken(1).image=="record"
					|| GetToken(1).image=="rem" || GetToken(1).image=="clear")})
	comp=ProcedureCall()
	{
		return comp;
	}
|
	expr=Expression()
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
	seq=ExpressionNot() ( "+" seq2=ExpressionNot() { seq = new SequenceExpressionPlus(seq, seq2); } )* { return seq; }
}

SequenceExpression ExpressionNot():
{
	SequenceExpression seq;
}
{
    "!" seq=ExpressionBasic() { return new SequenceExpressionNot(seq); }
	| seq=ExpressionBasic() { return seq; }
}

SequenceExpression ExpressionBasic():
{
	List<SequenceVariable> variableList1 = new List<SequenceVariable>();
	List<SequenceExpression> argExprs = new List<SequenceExpression>();
	SequenceVariable fromVar;
	String attrName, method, elemName;
	SequenceExpression expr;
	SequenceComputation comp;
	object constant;
}
{
	LOOKAHEAD(MethodCall())
	comp=MethodCall()
	{
		if(comp is SequenceExpression)
			return (SequenceExpression)comp;
		else
			throw new ParseException("expression method call expected, not the compution method call with side effects: "+comp.Symbol);
	}
|
	LOOKAHEAD(VariableUse() "." "visited")
	fromVar=VariableUse() "." "visited" "[" expr=Expression() "]"
	{
		return new SequenceExpressionIsVisited(fromVar, expr);
	}
|
	LOOKAHEAD(VariableUse() ".")
	fromVar=VariableUse() "." attrName=Word()
	{
		return new SequenceExpressionAttribute(fromVar, attrName);
	}
|
	LOOKAHEAD(VariableUse() "[")
	fromVar=VariableUse() "[" expr=Expression() "]"
	{
		return new SequenceExpressionContainerAccess(fromVar, expr);
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
	"def" "(" Arguments(argExprs) ")"
	{
		return new SequenceExpressionDef(argExprs.ToArray());
	}
|
	fromVar=VariableUse()
	{
		return new SequenceExpressionVariable(fromVar);
	}
|
	"@" "(" elemName=Text() ")"
	{
		return new SequenceExpressionElementFromGraph(elemName);
	}
|
	"(" expr=Expression() ")"
	{
		return expr;
	}
}

SequenceComputation ProcedureCall():
{
	String procedure;
	SequenceExpression fromExpr = null;
}
{
	procedure=Word() "(" ( fromExpr=Expression() )? ")"
	{
		if(procedure=="vfree") {
			if(fromExpr==null) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
			return new SequenceComputationVFree(fromExpr);
		} else if(procedure=="vreset") {
			if(fromExpr==null) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
			return new SequenceComputationVReset(fromExpr);
		} else if(procedure=="emit") {
			if(fromExpr==null) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
			return new SequenceComputationEmit(fromExpr);
		} else if(procedure=="record") {
			if(fromExpr==null) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
			return new SequenceComputationRecord(fromExpr);
		} else if(procedure=="rem") {
			if(fromExpr==null) throw new ParseException("\"" + procedure + "\" expects 1 parameter)");
			return new SequenceComputationGraphRem(fromExpr);
		} else if(procedure=="clear") {
			if(fromExpr!=null) throw new ParseException("\"" + procedure + "\" expects no parameters)");
			return new SequenceComputationGraphClear();
		} else {
			throw new ParseException("Unknown procedure name: \"" + procedure + "\"! (available are vfree|vreset|emit|record|rem|clear)");
		}
    }
}

SequenceExpression FunctionCall():
{
	String function;
	SequenceExpression fromExpr = null, fromExpr2 = null, fromExpr3 = null;
}
{
	function=Word() "(" ( fromExpr=Expression() ("," fromExpr2=Expression() ("," fromExpr3=Expression())? )? )? ")"
	{
		if(function=="valloc") {
			if(fromExpr!=null || fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + function + "\" expects no parameters)");
			return new SequenceExpressionVAlloc();
		} else if(function=="add") {
			if(fromExpr==null || (fromExpr2!=null && fromExpr3==null)) throw new ParseException("\"" + function + "\" expects 1(for a node) or 3(for an edge) parameters)");
			return new SequenceExpressionGraphAdd(fromExpr, fromExpr2, fromExpr3);
		} else if(function=="insertInduced") {
			if(fromExpr==null || fromExpr2==null || fromExpr3!=null) throw new ParseException("\"" + function + "\" expects 2 parameters (the set of nodes to compute the induced subgraph from which will be cloned and inserted, and one node of the set of which the clone will be returned)");
			return new SequenceExpressionInsertInduced(fromExpr, fromExpr2);
		} else if(function=="insertDefined") {
			if(fromExpr==null || fromExpr2==null || fromExpr3!=null) throw new ParseException("\"" + function + "\" expects 2 parameters (the set of edges which define the subgraph which will be cloned and inserted, and one edge of the set of which the clone will be returned)");
			return new SequenceExpressionInsertDefined(fromExpr, fromExpr2);
		} else if(function=="adjacent") {
			if(fromExpr==null) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(fromExpr, fromExpr2, fromExpr3, SequenceExpressionType.AdjacentNodes);
		} else if(function=="adjacentIncoming") {
			if(fromExpr==null) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(fromExpr, fromExpr2, fromExpr3, SequenceExpressionType.AdjacentNodesViaIncoming);
		} else if(function=="adjacentOutgoing") {
			if(fromExpr==null) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(fromExpr, fromExpr2, fromExpr3, SequenceExpressionType.AdjacentNodesViaOutgoing);
		} else if(function=="incident") {
			if(fromExpr==null) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(fromExpr, fromExpr2, fromExpr3, SequenceExpressionType.IncidentEdges);
		} else if(function=="incoming") {
			if(fromExpr==null) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(fromExpr, fromExpr2, fromExpr3, SequenceExpressionType.IncomingEdges);
		} else if(function=="outgoing") {
			if(fromExpr==null) throw new ParseException("\"" + function + "\" expects 1 (start node only) or 2 (start node, incident edge type) or 3 (start node, incident edge type, adjacent node type) parameters)");
			return new SequenceExpressionAdjacentIncident(fromExpr, fromExpr2, fromExpr3, SequenceExpressionType.OutgoingEdges);
		} else if(function=="inducedSubgraph") {
			if(fromExpr==null || fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + function + "\" expects 1 parameter (the set of nodes to construct the induced subgraph from)");
			return new SequenceExpressionInducedSubgraph(fromExpr);
		} else if(function=="definedSubgraph") {
			if(fromExpr==null || fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + function + "\" expects 1 parameter (the set of edges to construct the defined subgraph from)");
			return new SequenceExpressionDefinedSubgraph(fromExpr);
		} else if(function=="source") {
			if(fromExpr==null || fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + function + "\" expects 1 parameter (the edge to get the source node from)");
			return new SequenceExpressionSource(fromExpr);
		} else if(function=="target") {
			if(fromExpr==null || fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + function + "\" expects 1 parameter (the edge to get the target node from)");
			return new SequenceExpressionTarget(fromExpr);
		} else {
			throw new ParseException("Unknown function name: \"" + function + "\"! (available are valloc|add|insertInduced|insertDefined|adjacent|adjacentIncoming|adjacentOutgoing|incident|incoming|outgoing|inducedSubgraph|definedSubgraph|source|target)");
		}
    }
}

SequenceComputation MethodCall():
{
	String method;
	SequenceVariable fromVar;
	SequenceExpression fromExpr2 = null, fromExpr3 = null;
}
{
	fromVar=VariableUse() "." method=Word() "(" ( fromExpr2=Expression() ("," fromExpr3=Expression())? )? ")"
	{
		if(method=="add") {
			if(fromExpr2==null) throw new ParseException("\"" + method + "\" expects 1(for set,array end) or 2(for map,array with index) parameters)");
			return new SequenceComputationContainerAdd(fromVar, fromExpr2, fromExpr3);
		} else if(method=="rem") {
			if(fromExpr3!=null) throw new ParseException("\"" + method + "\" expects 1(for set,map,array with index) or 0(for array end) parameters )");
			return new SequenceComputationContainerRem(fromVar, fromExpr2);
		} else if(method=="clear") {
			if(fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
			return new SequenceComputationContainerClear(fromVar);
		} else if(method=="size") {
			if(fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
			return new SequenceExpressionContainerSize(fromVar);
		} else if(method=="empty") {
			if(fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
			return new SequenceExpressionContainerEmpty(fromVar);
		} else {
			throw new ParseException("Unknown method name: \"" + method + "\"! (available are add|rem|clear as sequences and size|empty as expressions on set/map/array)");
		}
    }
}

SequenceComputation MethodCallRepeated():
{
	String method;
	SequenceComputation methodCall;
	SequenceExpression fromExpr2 = null, fromExpr3 = null;
}
{
	methodCall=MethodCall()
	( "." method=Word() "(" ( fromExpr2=Expression() ("," fromExpr3=Expression())? )? ")"
		{
			if(method=="add") {
				if(fromExpr2==null) throw new ParseException("\"" + method + "\" expects 1(for set,array end) or 2(for map,array with index) parameters)");
				methodCall = new SequenceComputationContainerAdd(methodCall, fromExpr2, fromExpr3);
				fromExpr2 = null; fromExpr3 = null;
			} else if(method=="rem") {
				if(fromExpr3!=null) throw new ParseException("\"" + method + "\" expects 1(for set,map,array with index) or 0(for array end) parameters )");
				methodCall = new SequenceComputationContainerRem(methodCall, fromExpr2);
				fromExpr2 = null;
			} else if(method=="clear") {
				if(fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
				methodCall = new SequenceComputationContainerClear(methodCall);
			} else if(method=="size") {
				if(fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
				methodCall = new SequenceExpressionContainerSize(methodCall);
			} else if(method=="empty") {
				if(fromExpr2!=null || fromExpr3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
				methodCall = new SequenceExpressionContainerEmpty(methodCall);
			} else {
				throw new ParseException("Unknown method name: \"" + method + "\"! (available are add|rem|clear as sequences and size|empty as expressions on set/map/array)");
			}
		}
	)*
	{ return methodCall; }
}

void RuleLookahead():
{
}
{
	("(" ( Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">" | "array" "<" Word() ">"))? | "::" Word() ) 
			("," ( Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">" | "array" "<" Word() ">"))? | "::" Word() ) )* ")" "=")?
	(
	    ( "$" ("%")? ( Variable() ("," (Variable() | "*"))? )? )? "["
	|
	    ( "%" | "?" )* Word()
	)
}

Sequence Rule():
{
	bool special = false, test = false;
	String str;
	bool chooseRandSpecified = false, chooseRandSpecified2 = false, choice = false;
	SequenceVariable varChooseRand = null, varChooseRand2 = null;
	List<SequenceExpression> argExprs = new List<SequenceExpression>();
	List<SequenceVariable> returnVars = new List<SequenceVariable>();
}
{
	("(" VariableList(returnVars) ")" "=" )?
	(
		(
			"$" ("%" { choice = true; })? ( varChooseRand=Variable() ("," (varChooseRand2=Variable() | "*") { chooseRandSpecified2 = true; })? )? { chooseRandSpecified = true; }
		)?
		"[" ("%" { special = true; } | "?" { test = true; })* str=Word()
		("(" Arguments(argExprs) ")")?
		"]"
		{
			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			return new SequenceRuleAllCall(CreateRuleInvocationParameterBindings(str, argExprs, returnVars),
					special, test, chooseRandSpecified, varChooseRand, chooseRandSpecified2, varChooseRand2, choice);
		}
	|
		("%" { special = true; } | "?" { test = true; })*
		str=Word() ("(" Arguments(argExprs) ")")? // if only str is given, this might be a variable predicate; but this is decided later on in resolve
		{
			if(argExprs.Count==0 && returnVars.Count==0)
			{
				SequenceVariable var = varDecls.Lookup(str);
				if(var!=null)
				{
					if(var.Type!="" && var.Type!="boolean")
						throw new SequenceParserException(str, "untyped or bool", var.Type);
					return new SequenceBooleanComputation(new SequenceExpressionVariable(var), null, special);
				}
			}

			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			if(IsSequenceName(str))
				return new SequenceSequenceCall(
								CreateSequenceInvocationParameterBindings(str, argExprs, returnVars),
								special);
			else
				return new SequenceRuleCall(
								CreateRuleInvocationParameterBindings(str, argExprs, returnVars),
								special, test);
		}
	)
}

CSHARPCODE
RuleInvocationParameterBindings CreateRuleInvocationParameterBindings(String ruleName,
				List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
{
	IAction action = null;
	if(actions != null)
		action = actions.GetAction(ruleName);

	RuleInvocationParameterBindings paramBindings = new RuleInvocationParameterBindings(action,
			argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());

	if(action == null)
		paramBindings.Name = ruleName;

	return paramBindings;
}

CSHARPCODE
SequenceInvocationParameterBindings CreateSequenceInvocationParameterBindings(String sequenceName,
				List<SequenceExpression> argExprs, List<SequenceVariable> returnVars)
{
	SequenceDefinition sequenceDef = null;
	if(actions != null) {
		sequenceDef = actions.RetrieveGraphRewriteSequenceDefinition(sequenceName);
	}

	SequenceInvocationParameterBindings paramBindings = new SequenceInvocationParameterBindings(sequenceDef,
			argExprs.ToArray(), new object[argExprs.Count], returnVars.ToArray());

	if(sequenceDef == null)
		paramBindings.Name = sequenceName;

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
String RemoveTypeSuffix(String value)
{
	if (value.EndsWith("y") || value.EndsWith("Y")
		|| value.EndsWith("s") || value.EndsWith("S")
		|| value.EndsWith("l") || value.EndsWith("L"))
		return value.Substring(0, value.Length - 1);
	else
		return value;
}

TOKEN: { < ERROR: ~[] > }
