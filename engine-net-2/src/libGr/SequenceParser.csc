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
		BaseActions actions;
		String[] ruleNames;
		
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
			parser.varDecls = new SymbolTable();
			parser.varDecls.PushFirstScope(null);
			Sequence seq = parser.XGRS();
			SequenceChecker seqChecker = new SequenceChecker(actions);
			seqChecker.Check(seq);
			return seq;
		}		

        /// <summary>
        /// Parses a given string in xgrs syntax and builds a Sequence object. Used for the compiled xgrs.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="ruleNames">An array containing the names of the rules used in the specification.</param>
        /// <param name="predefinedVariables">A map from variables to types giving the parameters to the sequence, i.e. predefined variables.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, String[] ruleNames,
		        Dictionary<String, String> predefinedVariables)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = null;
			parser.ruleNames = ruleNames;
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
|   < NULL: "null" >
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
	< NUM: (("-")? ["0"-"9"])+ >
|	< DOUBLEQUOTEDTEXT : "\"" (~["\"", "\n", "\r"])* "\"" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|	< SINGLEQUOTEDTEXT : "\'" (~["\'", "\n", "\r"])* "\'" >
		{ matchedToken.image = matchedToken.image.Substring(1, matchedToken.image.Length-2); }
|	< WORD : ["A"-"Z", "a"-"z", "_"] (["A"-"Z", "a"-"z", "_", "0"-"9"])* >
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

long Number():
{
	Token t;
	long val;
}
{
	t=<NUM>
	{
		if(!long.TryParse(t.image, out val))
			throw new ParseException("64-bit integer expected but found: \"" + t + "\" (" + t.kind + ")");
		return val;
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

object Constant():
{
	object constant;
	long number;
	string tid, id;
	string typeName, typeNameDst;
}
{
	(
		number=Number() { constant = (int) number; }
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
		tid=Word() "::" id=Word() { constant = tid+"::"+id; }
    |
		"set" "<" typeName=Word() ">" "{" "}" { constant = "set<"+typeName+">"; }
	|
		"map" "<" typeName=Word() "," typeNameDst=Word() ">" "{" "}" { constant = "map<"+typeName+","+typeNameDst+">"; }
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
	String varName, typeName = null, typeNameDst;
}
{
	varName=Word() (":" (typeName=Word()
							| "set" "<" typeName=Word() ">" { typeName = "set<"+typeName+">"; }
							| "map" "<" typeName=Word() "," typeNameDst=Word() ">" { typeName = "map<"+typeName+","+typeNameDst+">"; }
						  )
					 )?
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

/////////////////////////////////////////
// Extended rewrite sequence           //
// (lowest precedence operators first) //
/////////////////////////////////////////

Sequence RewriteSequence():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequenceLazyOr()
	(
		LOOKAHEAD(2)
		(
			LOOKAHEAD(2)
			("$" { random = true; })? "<;" seq2=RewriteSequence()							
			{
				seq = new SequenceThenLeft(seq, seq2, random);
			}
		|
			("$" { random = true; })? ";>" seq2=RewriteSequence()							
			{
				seq = new SequenceThenRight(seq, seq2, random);
			}
		)
	)?
	{
		return seq;
	}
}

Sequence RewriteSequenceLazyOr():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequenceLazyAnd()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "||" seq2=RewriteSequence()							
		{
			seq = new SequenceLazyOr(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequenceLazyAnd():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequenceStrictOr()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "&&" seq2=RewriteSequenceLazyAnd()
		{
			seq = new SequenceLazyAnd(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequenceStrictOr():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequenceStrictXor()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "|" seq2=RewriteSequenceStrictOr()
		{
			seq = new SequenceStrictOr(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequenceStrictXor():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequenceStrictAnd()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "^" seq2=RewriteSequenceStrictXor()
		{
			seq = new SequenceXor(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequenceStrictAnd():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequenceNeg()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "&" seq2=RewriteSequenceStrictAnd()
		{
			seq = new SequenceStrictAnd(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequenceNeg():
{
	Sequence seq;
}
{
    "!" seq=RewriteSequenceNeg()
	{
		return new SequenceNot(seq);
	}
|	
	seq=RewriteSequenceIteration()
	{
		return seq;
	}
}

Sequence RewriteSequenceIteration():
{
	Sequence seq;
	long minnum, maxnum = -1;
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
		    "[" minnum=Number()
		    (
				":"
				(
					maxnum=Number() { maxspecified = true; }
				|
					"*" { maxstar = true; }
				)
			)?
			"]"
			{
			    if(maxstar)
			    {
					seq = new SequenceIterationMin(seq, minnum);
			    }
			    else
			    {
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
	bool special = false;
	Sequence seq, seq2, seq3 = null;
	List<SequenceVariable> defParamVars = new List<SequenceVariable>();
	SequenceVariable toVar, fromVar, fromVar2 = null, fromVar3 = null;
	String attrName, method, elemName;
	object constant;
	String str;
}
{
	LOOKAHEAD(Variable() "=") toVar=Variable() "="
    (
	    "valloc" "(" ")"
		{
			return new SequenceAssignVAllocToVar(toVar);
		}
	|
		LOOKAHEAD(3) fromVar=VariableUse() "." "visited" "[" fromVar2=VariableUse() "]"
        {
			return new SequenceAssignSequenceResultToVar(toVar, new SequenceIsVisited(fromVar, fromVar2));
        }
	|
        LOOKAHEAD(4) fromVar=VariableUse() "." method=Word() "(" ")"
		{
			if(method=="size") return new SequenceAssignSetmapSizeToVar(toVar, fromVar);
			else if(method=="empty") return new SequenceAssignSetmapEmptyToVar(toVar, fromVar);
			else throw new ParseException("Unknown method name: \"" + method + "\"! (available are size|empty on set/map)");
		}
	|
        LOOKAHEAD(2) fromVar=VariableUse() "." attrName=Word()
        {
            return new SequenceAssignAttributeToVar(toVar, fromVar, attrName);
        }
	|
		LOOKAHEAD(2) fromVar=VariableUse() "[" fromVar2=VariableUse() "]" // parsing v=a[ as v=a[x] has priority over (v=a)[*]
		{
			return new SequenceAssignMapAccessToVar(toVar, fromVar, fromVar2);
		}
	|
		LOOKAHEAD(2) fromVar=VariableUse() "in" fromVar2=VariableUse()
		{
			return new SequenceAssignSequenceResultToVar(toVar, new SequenceIn(fromVar, fromVar2));
		}
	|
        LOOKAHEAD(2) fromVar=VariableUse() "(" // deliver understandable error message for case of missing parenthesis at rule result assignment
        { 
            throw new ParseException("the destination variable(s) of a rule result assignment must be enclosed in parenthesis");
        }
	|
		LOOKAHEAD(2) constant=Constant()
		{
			return new SequenceAssignConstToVar(toVar, constant);
		}
    |
		fromVar=VariableUse()
        {
            return new SequenceAssignVarToVar(toVar, fromVar);
        }
    |
        "@" "(" elemName=Text() ")"
        {
            return new SequenceAssignElemToVar(toVar, elemName);
        }
	|
		"def" "(" Parameters(defParamVars) ")" // todo: eigentliches Ziel: Zuweisung simple sequence an Variable
		{
			return new SequenceAssignSequenceResultToVar(toVar, new SequenceDef(defParamVars.ToArray()));
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
		return new SequenceIsVisited(fromVar, fromVar2);
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
	LOOKAHEAD(4) toVar=VariableUse() "." attrName=Word() "=" fromVar=VariableUse()
    {
        return new SequenceAssignVarToAttribute(toVar, attrName, fromVar);
    }
|
	LOOKAHEAD(2) fromVar=VariableUse() "." method=Word() "(" ( fromVar2=VariableUse() ("," fromVar3=VariableUse())? )? ")"
	{
		if(method=="add") {
			if(fromVar2==null) throw new ParseException("\"" + method + "\" expects 1(for set) or 2(for map) parameters)");
			return new SequenceSetmapAdd(fromVar, fromVar2, fromVar3);
		} else if(method=="rem") {
			if(fromVar2==null || fromVar3!=null) throw new ParseException("\"" + method + "\" expects 1 parameter)");
			return new SequenceSetmapRem(fromVar, fromVar2); 
		} else if(method=="clear") {
			if(fromVar2!=null || fromVar3!=null) throw new ParseException("\"" + method + "\" expects no parameters)");
			return new SequenceSetmapClear(fromVar);
		} else {
			throw new ParseException("Unknown method name: \"" + method + "\"! (available are add|rem|clear on set/map)");
		}
    }
|
	LOOKAHEAD(2) fromVar=VariableUse() "in" fromVar2=VariableUse()
	{
		return new SequenceIn(fromVar, fromVar2);
	}
|
	LOOKAHEAD(RuleLookahead()) seq=Rule()
	{
		return seq;
	}
|
	"def" "(" Parameters(defParamVars) ")"
	{
		return new SequenceDef(defParamVars.ToArray());
	}
|
    LOOKAHEAD(2) ("%" { special = true; })? "true"
    {
        return new SequenceTrue(special);
    }
|
    LOOKAHEAD(2) ("%" { special = true; })? "false"
    {
        return new SequenceFalse(special);
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
    "if" "{" seq=RewriteSequence() ";" seq2=RewriteSequence() (";" seq3=RewriteSequence())? "}"
    { // TODO block nesting
		if(seq3==null) return new SequenceIfThen(seq, seq2);
        else return new SequenceIfThenElse(seq, seq2, seq3);
    }
|
	"for" "{" fromVar=Variable() ("->" fromVar2=Variable())? "in" fromVar3=VariableUse() ";" seq=RewriteSequence() "}"
	{ // TODO block nesting
        return new SequenceFor(fromVar, fromVar2, fromVar3, seq);
    }
}

void RuleLookahead():
{
}
{
	("(" Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">"))?
			("," Word() (":" (Word() | "set" "<" Word() ">" | "map" "<" Word() "," Word() ">"))?)* ")" "=")?
	(
	    ("$" (Number())?)? "["
	|
	    ("%" | "?")* Word()
	)
}

Sequence Rule():
{
	bool special = false, test = false;
	String str;
	IAction action = null;
	bool retSpecified = false, numChooseRandSpecified = false;
	long numChooseRand = 1;
	List<SequenceVariable> paramVars = new List<SequenceVariable>();
	List<Object> paramConsts = new List<Object>();
	List<SequenceVariable> returnVars = new List<SequenceVariable>();
}
{
	("(" VariableList(returnVars) ")" "=" { retSpecified = true; })? 
	(
		(
			"$" (numChooseRand=Number())?
			{
				numChooseRandSpecified = true;
				if(numChooseRand <= 0)
					throw new ParseException("The number of randomly chosen elements must be greater than zero!");
				if(numChooseRand > Int32.MaxValue)
					throw new ParseException("The number of randomly chosen elements must be less than 2147483648!");
			}
		)?
		"[" ("%" { special = true; } | "?" { test = true; })* str=Word()
		("(" RuleParameters(paramVars, paramConsts) ")")?
		"]"
		{
			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);

			return new SequenceRuleAll(CreateRuleInvocationParameterBindings(str, paramVars, paramConsts, returnVars),
					special, test, numChooseRandSpecified ? (int) numChooseRand : 0);
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
					return new SequenceVarPredicate(var, special);
				}
			}

			// No variable with this name may exist
			if(varDecls.Lookup(str)!=null)
				throw new SequenceParserException(str, SequenceParserError.RuleNameUsedByVariable);
				
			return new SequenceRule(CreateRuleInvocationParameterBindings(str, paramVars, paramConsts, returnVars),
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
		paramBindings.RuleName = ruleName;

	return paramBindings;
}

TOKEN: { < ERROR: ~[] > }
