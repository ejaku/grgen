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
        /// Parses a given string in xgrs syntax and builds a Sequence object. Used for the interpreted xgrs.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="actions">The BaseActions object containing the rules used in the string.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, BaseActions actions)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = actions;
			parser.ruleNames = null;
			parser.model = actions.Graph.Model;
			parser.varDecls = new SymbolTable();
			parser.varDecls.PushFirstScope(null);
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
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static SequenceDefinition ParseSequenceDefinition(String sequenceStr, BaseActions actions)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = actions;
			parser.ruleNames = null;
			parser.model = actions.Graph.Model;
			parser.varDecls = new SymbolTable();
			parser.varDecls.PushFirstScope(null);
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
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, String[] ruleNames, String[] sequenceNames,
		        Dictionary<String, String> predefinedVariables, IGraphModel model)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = null;
			parser.ruleNames = ruleNames;
			parser.sequenceNames = sequenceNames;
			parser.model = model;
			parser.varDecls = new SymbolTable();
			parser.varDecls.PushFirstScope(predefinedVariables);
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
|	< STAR: "*" >
|	< PLUS: "+" >
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
|   < VALLOC: "valloc" >
|   < VFREE: "vfree" >
|   < VISITED: "visited" >
|   < VRESET: "vreset" >
|   < EMIT: "emit" >
|   < record: "record" >
|   < NULL: "null" >
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

void Parameters(List<SequenceVariable> parameters):
{
	SequenceVariable var;
}
{
	var=VariableUse() { parameters.Add(var); } ("," var=VariableUse() { parameters.Add(var); })*
}

void RuleParameter(List<SequenceVariable> paramVars, List<Object> paramConsts):
{
	SequenceVariable var;
	object constant;
}
{
	LOOKAHEAD(2) constant=Constant()
	{
		paramVars.Add(null);
		paramConsts.Add(constant);
	}
|
	var=VariableUse()
	{
		paramVars.Add(var);
		paramConsts.Add(null);
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

void RuleParameters(List<SequenceVariable> paramVars, List<Object> paramConsts):
{ }
{
	RuleParameter(paramVars, paramConsts) ("," RuleParameter(paramVars, paramConsts))*
}


SequenceVariable Variable(): // usage as well as definition
{
	String varName, typeName=null;
}
{
	varName=Word() (":" typeName=Type() )?
	{
		SequenceVariable oldVariable = varDecls.Lookup(varName);
		SequenceVariable newVariable;
		if(typeName!=null)
		{
			if(oldVariable==null) {
				newVariable = varDecls.Define(varName, typeName);
			} else if(oldVariable.Type=="") {
				throw new ParseException("The variable \""+varName+"\" has already been used/implicitely declared as global variable!");
			} else {
				throw new ParseException("The variable \""+varName+"\" has already been declared as local variable with type \""+oldVariable.Type+"\"!");
			}
		}
		else
		{
			if(oldVariable==null) {
				newVariable = varDecls.Define(varName, "");
			} else {
				newVariable = oldVariable;
			}
		}
		return newVariable;
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
		if(oldVariable==null) {
			newVariable = varDecls.Define(varName, typeName);
		} else if(oldVariable.Type=="") {
			throw new ParseException("The variable \""+varName+"\" has already been used/implicitely declared as global variable!");
		} else {
			throw new ParseException("The variable \""+varName+"\" has already been declared as local variable with type \""+oldVariable.Type+"\"!");
		}
		return newVariable;
	}
}

SequenceVariable VariableUse(): // only usage in contrast to Variable()
{
	String varName;
}
{
	varName=Word()
	{
		SequenceVariable oldVariable = varDecls.Lookup(varName);
		SequenceVariable newVariable;
		if(oldVariable==null) {
			newVariable = varDecls.Define(varName, "");
		} else {
			newVariable = oldVariable;
		}
		return newVariable;
	}
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
		| "set" "<" typeParam=Word() ">" { type = "set<"+typeParam+">"; }
			("{" { throw new ParseException("no {} allowed at set declaration, use s:set<T> = set<T>{} for initialization"); })?
		| "map" "<" typeParam=Word() "," typeParamDst=Word() ">" { type = "map<"+typeParam+","+typeParamDst+">"; }
			("{" { throw new ParseException("no {} allowed at map declaration, use m:map<S,T> = map<S,T>{} for initialization"); })?
		| "array" "<" typeParam=Word() ">" { type = "array<"+typeParam+">"; }
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
	String attrName, method, elemName;
	int num = 0;
	object constant;
	String str;
}
{
	LOOKAHEAD(Variable() "=") toVar=Variable() "="
    (
	    "valloc" "(" ")"
		{
			return new SequenceAssignExprToVar(toVar, new SequenceExpressionVAlloc());
		}
	|
		LOOKAHEAD(3) fromVar=VariableUse() "." "visited" "[" fromVar2=VariableUse() "]"
        {
			return new SequenceAssignExprToVar(toVar, new SequenceExpressionIsVisited(fromVar, fromVar2));
        }
	|
        LOOKAHEAD(4) fromVar=VariableUse() "." method=Word() "(" ")"
		{
			if(method=="size") return new SequenceAssignExprToVar(toVar, new SequenceExpressionContainerSize(fromVar));
			else if(method=="empty") return new SequenceAssignExprToVar(toVar, new SequenceExpressionContainerEmpty(fromVar));
			else throw new ParseException("Unknown method name: \"" + method + "\"! (available are size|empty on set/map)");
		}
	|
        LOOKAHEAD(2) fromVar=VariableUse() "." attrName=Word()
        {
            return new SequenceAssignExprToVar(toVar, new SequenceExpressionAttribute(fromVar, attrName));
        }
	|
		LOOKAHEAD(2) fromVar=VariableUse() "[" fromVar2=VariableUse() "]" // parsing v=a[ as v=a[x] has priority over (v=a)[*]
		{
			return new SequenceAssignExprToVar(toVar, new SequenceExpressionContainerAccess(fromVar, fromVar2));
		}
	|
		LOOKAHEAD(2) fromVar=VariableUse() "in" fromVar2=VariableUse()
		{
			return new SequenceAssignExprToVar(toVar, new SequenceExpressionInContainer(fromVar, fromVar2));
		}
	|
        LOOKAHEAD(2) fromVar=VariableUse() "(" // deliver understandable error message for case of missing parenthesis at rule result assignment
        {
            throw new ParseException("the destination variable(s) of a rule result assignment must be enclosed in parenthesis");
        }
	|
		LOOKAHEAD(2) constant=Constant()
		{
			return new SequenceAssignExprToVar(toVar, new SequenceExpressionConstant(constant));
		}
    |
		fromVar=VariableUse()
        {
            return new SequenceAssignExprToVar(toVar, new SequenceExpressionVariable(fromVar));
        }
    |
        "@" "(" elemName=Text() ")"
        {
            return new SequenceAssignExprToVar(toVar, new SequenceExpressionElementFromGraph(elemName));
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
		"def" "(" Parameters(variableList1) ")" // todo: eigentliches Ziel: Zuweisung simple sequence an Variable
		{
			return new SequenceAssignExprToVar(toVar, new SequenceExpressionDef(variableList1.ToArray()));
		}
	|
		"(" seq=RewriteSequence() ")"
		{
			return new SequenceAssignSequenceResultToVar(toVar, seq);
		}
    )
|
	LOOKAHEAD(7) toVar=VariableUse() "." "visited" "[" fromVar=VariableUse() "]" "="
		(
			fromVar2=VariableUse() { return new SequenceSetVisited(toVar, fromVar, fromVar2); }
		|
			"true" { return new SequenceSetVisited(toVar, fromVar, true); }
		|
			"false" { return new SequenceSetVisited(toVar, fromVar, false); }
		)
|
	LOOKAHEAD(3) fromVar=VariableUse() "." "visited" "[" fromVar2=VariableUse() "]"
	{
		return new SequenceBooleanExpression(new SequenceExpressionIsVisited(fromVar, fromVar2), false);
	}
|
	"vfree" "(" fromVar=VariableUse() ")"
	{
		return new SequenceVFree(fromVar);
	}
|
	"vreset" "(" fromVar=VariableUse() ")"
	{
		return new SequenceVReset(fromVar);
	}
|
	"emit" "("
		( str=TextString() { seq = new SequenceEmit(str); }
		| fromVar=VariableUse() { seq = new SequenceEmit(fromVar);} )
	")" { return seq; }
|
	"record" "("
		( str=TextString() { seq = new SequenceRecord(str); }
		| fromVar=VariableUse() { seq = new SequenceRecord(fromVar);} )
	")" { return seq; }
|
	LOOKAHEAD(4) toVar=VariableUse() "." attrName=Word() "=" fromVar=VariableUse()
    {
        return new SequenceAssignExprToAttribute(toVar, attrName, new SequenceExpressionVariable(fromVar));
    }
|
	LOOKAHEAD(2) fromVar=VariableUse() "." method=Word() "(" ( fromVar2=VariableUse() ("," fromVar3=VariableUse())? )? ")"
	{
		if(method=="add") {
			if(fromVar2==null) throw new ParseException("\"" + method + "\" expects 1(for set,array end) or 2(for map,array with index) parameters)");
			return new SequenceContainerAdd(fromVar, fromVar2, fromVar3);
		} else if(method=="rem") {
			if(fromVar3!=null) throw new ParseException("\"" + method + "\" expects 1(for set,map,array with index) or 0(for array end) parameters )");
			return new SequenceContainerRem(fromVar, fromVar2);
		} else if(method=="clear") {
			if(fromVar2!=null || fromVar3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
			return new SequenceContainerClear(fromVar);
		} else {
			throw new ParseException("Unknown method name: \"" + method + "\"! (available are add|rem|clear on set/map)");
		}
    }
|
	LOOKAHEAD(2) fromVar=VariableUse() "in" fromVar2=VariableUse()
	{
		return new SequenceBooleanExpression(new SequenceExpressionInContainer(fromVar, fromVar2), false);
	}
|
	LOOKAHEAD(RuleLookahead()) seq=Rule()
	{
		return seq;
	}
|
	toVar=VariableUse() "[" fromVar=VariableUse() "]" "=" fromVar2=VariableUse()
	{
		return new SequenceAssignExprToIndexedVar(toVar, fromVar, new SequenceExpressionVariable(fromVar2));
	}
|
	"def" "(" Parameters(variableList1) ")"
	{
		return new SequenceBooleanExpression(new SequenceExpressionDef(variableList1.ToArray()), false);
	}
|
    LOOKAHEAD(2) ("%" { special = true; })? "true"
    {
        return new SequenceBooleanExpression(new SequenceExpressionTrue(), special);
    }
|
    LOOKAHEAD(2) ("%" { special = true; })? "false"
    {
        return new SequenceBooleanExpression(new SequenceExpressionFalse(), special);
    }
|
	LOOKAHEAD(4) "$" ("%" { choice = true; } )?
		"||" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
	{
		return new SequenceLazyOrAll(sequences, choice);
	}
|
	LOOKAHEAD(4) "$" ("%" { choice = true; } )?
		"&&" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
	{
		return new SequenceLazyAndAll(sequences, choice);
	}
|
	LOOKAHEAD(4) "$" ("%" { choice = true; } )?
		"|" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
	{
		return new SequenceStrictOrAll(sequences, choice);
	}
|
	LOOKAHEAD(4) "$" ("%" { choice = true; } )?
		"&" "(" seq=RewriteSequence() { sequences.Add(seq); } ("," seq=RewriteSequence() { sequences.Add(seq); })* ")"
	{
		return new SequenceStrictAndAll(sequences, choice);
	}
|
	( "$" { chooseRandSpecified=true; } ("%" { choice = true; } )? )?
		"{" seq=Rule() { sequences.Add(seq); } ("," seq=Rule() { sequences.Add(seq); })* "}"
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
    "<<" seq=Rule() ";" seq2=RewriteSequence() ">>"
    {
        return new SequenceBacktrack(seq, seq2);
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
	"for" "{" { varDecls.PushScope(ScopeType.For); } fromVar=Variable() ("->" fromVar2=Variable())? "in" fromVar3=VariableUse() ";"
		seq=RewriteSequence() { varDecls.PopScope(variableList1); } "}"
	{
        return new SequenceFor(fromVar, fromVar2, fromVar3, seq, variableList1);
    }
|
	"yield" toVar=VariableUse() "=" fromVar=VariableUse()
	{
		return new SequenceYieldingAssignExprToVar(toVar, new SequenceExpressionVariable(fromVar));
	}
}

void RuleLookahead():
{
}
{
	("(" Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">" | "array" "<" Word() ">"))?
			("," Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">" | "array" "<" Word() ">"))?)* ")" "=")?
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
	List<SequenceVariable> paramVars = new List<SequenceVariable>();
	List<Object> paramConsts = new List<Object>();
	List<SequenceVariable> returnVars = new List<SequenceVariable>();
}
{
	("(" VariableList(returnVars) ")" "=" )?
	(
		(
			"$" ("%" { choice = true; })? ( varChooseRand=Variable() ("," (varChooseRand2=Variable() | "*") { chooseRandSpecified2 = true; })? )? { chooseRandSpecified = true; }
		)?
		"[" ("%" { special = true; } | "?" { test = true; })* str=Word()
		("(" RuleParameters(paramVars, paramConsts) ")")?
		"]"
		{
			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			return new SequenceRuleAllCall(CreateRuleInvocationParameterBindings(str, paramVars, paramConsts, returnVars),
					special, test, chooseRandSpecified, varChooseRand, chooseRandSpecified2, varChooseRand2, choice);
		}
	|
		("%" { special = true; } | "?" { test = true; })*
		str=Word() ("(" RuleParameters(paramVars, paramConsts) ")")? // if only str is given, this might be a variable predicate; but this is decided later on in resolve
		{
			if(paramVars.Count==0 && returnVars.Count==0)
			{
				SequenceVariable var = varDecls.Lookup(str);
				if(var!=null)
				{
					if(var.Type!="" && var.Type!="boolean")
						throw new SequenceParserException(str, "untyped or bool", var.Type);
					return new SequenceBooleanExpression(new SequenceExpressionVariable(var), special);
				}
			}

			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			if(IsSequenceName(str))
				return new SequenceSequenceCall(
								CreateSequenceInvocationParameterBindings(str, paramVars, paramConsts, returnVars),
								special);
			else
				return new SequenceRuleCall(
								CreateRuleInvocationParameterBindings(str, paramVars, paramConsts, returnVars),
								special, test);
		}
	)
}

CSHARPCODE
RuleInvocationParameterBindings CreateRuleInvocationParameterBindings(String ruleName,
				List<SequenceVariable> paramVars, List<Object> paramConsts, List<SequenceVariable> returnVars)
{
	IAction action = null;
	if(actions != null)
		action = actions.GetAction(ruleName);

	RuleInvocationParameterBindings paramBindings = new RuleInvocationParameterBindings(action,
			paramVars.ToArray(), paramConsts.ToArray(), returnVars.ToArray());

	if(action == null)
		paramBindings.Name = ruleName;

	return paramBindings;
}

CSHARPCODE
SequenceInvocationParameterBindings CreateSequenceInvocationParameterBindings(String sequenceName,
				List<SequenceVariable> paramVars, List<Object> paramConsts, List<SequenceVariable> returnVars)
{
	SequenceDefinition sequenceDef = null;
	if(actions != null) {
		sequenceDef = actions.RetrieveGraphRewriteSequenceDefinition(sequenceName);
	}

	SequenceInvocationParameterBindings paramBindings = new SequenceInvocationParameterBindings(sequenceDef,
			paramVars.ToArray(), paramConsts.ToArray(), returnVars.ToArray());

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
