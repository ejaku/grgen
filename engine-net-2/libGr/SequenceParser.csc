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
		NamedGraph namedGraph;
		
		/// <summary>
		/// Maps variable names to type names.
		/// Variable usages before declaration create a map entry with a null type.
		/// If varDecls is null, no variable declarations are allowed.
		/// </summary>
		Dictionary<String, String> varDecls;
		
        /// <summary>
        /// Parses a given string in xgrs syntax and builds a Sequence object.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="actions">The BaseActions object containing the rules used in the string.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserRuleException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, BaseActions actions)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = actions;
			return parser.XGRS();
		}		

        /// <summary>
        /// Parses a given string in xgrs syntax and builds a Sequence object.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="actions">The BaseActions object containing the rules used in the string.</param>
        /// <param name="varDecls">A map from variables to types which will be filled for the
        /// given sequence. It may already contain predefined variables.</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserRuleException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, BaseActions actions,
		        Dictionary<String, String> varDecls)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = actions;
			parser.varDecls = varDecls;
			Sequence seq = parser.XGRS();
			parser.ResolveVars(ref seq);
			return seq;
		}		

        /// <summary>
        /// Parses a given string in xgrs syntax and builds a Sequence object.
        /// </summary>
        /// <param name="sequenceStr">The string representing a xgrs (e.g. "test[7] &amp;&amp; (chicken+ || egg)*")</param>
        /// <param name="actions">The BaseActions object containing the rules used in the string.</param>
        /// <param name="namedGraph">A NamedGraph object to be used for named element access (@-operator).</param>
        /// <returns>The sequence object according to sequenceStr.</returns>
        /// <exception cref="ParseException">Thrown when a syntax error was found in the string.</exception>
        /// <exception cref="SequenceParserRuleException">Thrown when a rule is used with the wrong number of arguments
        /// or return parameters.</exception>
		public static Sequence ParseSequence(String sequenceStr, BaseActions actions, NamedGraph namedGraph)
		{
			SequenceParser parser = new SequenceParser(new StringReader(sequenceStr));
			parser.actions = actions;
			parser.namedGraph = namedGraph;
			return parser.XGRS();
		}
		
		private void ResolveVars(ref Sequence seq)
		{
			switch(seq.SequenceType)
			{
				case SequenceType.LazyOr:
				case SequenceType.LazyAnd:
				case SequenceType.StrictOr:
				case SequenceType.Xor:
				case SequenceType.StrictAnd:
				{
					SequenceBinary binSeq = (SequenceBinary) seq;
					ResolveVars(ref binSeq.Left);
					ResolveVars(ref binSeq.Right);
					break;
				}
				
				case SequenceType.Not:
				case SequenceType.Min:
				case SequenceType.MinMax:
				case SequenceType.Transaction:
				{
					SequenceUnary unSeq = (SequenceUnary) seq;
					ResolveVars(ref unSeq.Seq);
					break;
				}
				
				case SequenceType.Rule:
				{
					SequenceRule ruleSeq = (SequenceRule) seq;
					RuleObject ruleObj = ruleSeq.RuleObj;
					
					// This can only be a predicate, if this "rule" has neither parameters nor returns
					if(ruleObj.ParamVars.Length != 0 || ruleObj.ReturnVars.Length != 0) break;
					
					// Does a boolean variable exist with the "rule" name?
					String typeName;
					if(!varDecls.TryGetValue(ruleObj.RuleName, out typeName) || typeName != "boolean") break;
					
					// Yes, so transform this SequenceRule into a SequenceVarPredicate
					seq = new SequenceVarPredicate(ruleObj.RuleName, ruleSeq.Special);
					break;
				}

				case SequenceType.AssignSequenceResultToVar:
				{
					SequenceAssignSequenceResultToVar assignSeq = (SequenceAssignSequenceResultToVar) seq;
					ResolveVars(ref assignSeq.Seq);
					break;
				}
				
				case SequenceType.RuleAll:
				case SequenceType.Def:
				case SequenceType.True:
				case SequenceType.False:
				case SequenceType.AssignVarToVar:
				case SequenceType.AssignElemToVar:
					// Nothing to be done here
					break;

				default:
					throw new Exception("Unknown sequence type: " + seq.SequenceType);
			}			
		}
	}
PARSER_END(SequenceParser)

// characters to be skipped
SKIP: {
	" " |
	"\t" |
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
|	< COLON: ":" >
|   < PERCENT: "%" >
|   < QUESTIONMARK: "?" >
|	< AT : "@" >
|   < DEF: "def" >
|   < TRUE: "true" >
|   < FALSE: "false" >
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

void Parameters(List<String> parameters):
{
	String str;
}
{
	str=Word() { parameters.Add(str); } ("," str=Word() { parameters.Add(str); })*
}

void RuleParameter(List<String> paramVars, List<Object> paramConsts):
{
	String str;
	object constant;
	long number;
}
{
	str=Word()
	{
		paramVars.Add(str);
		paramConsts.Add(null);
	}
|
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
	)
	{
		paramVars.Add(null);
		paramConsts.Add(constant);
	}
}

void RuleParameters(List<String> paramVars, List<Object> paramConsts):
{ }
{
	RuleParameter(paramVars, paramConsts) ("," RuleParameter(paramVars, paramConsts))*
}


String Variable():
{
	String toVarName, typeName = null;
}
{
	toVarName=Word() (":" typeName=Word())?
	{
		if(varDecls != null)
		{
			String oldTypeName;
			if(varDecls.TryGetValue(toVarName, out oldTypeName))
			{
				if(typeName != null)
				{
					if(oldTypeName != null)
						throw new ParseException("The variable \"" + toVarName + "\" has already been declared!");
					varDecls[toVarName] = typeName;
				}
			}
			else varDecls[toVarName] = typeName;
		}
		else if(typeName != null)
			throw new ParseException("Variable types are not supported here!");
		return toVarName;
	}
}

void VariableList(List<String> variables):
{
	String str;
}
{
	str=Variable() { variables.Add(str); } ("," str=Variable() { variables.Add(str); })*
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
	seq=RewriteSequence2()
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

Sequence RewriteSequence2():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequence3()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "&&" seq2=RewriteSequence2()
		{
			seq = new SequenceLazyAnd(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequence3():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequence4()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "|" seq2=RewriteSequence3()
		{
			seq = new SequenceStrictOr(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequence4():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequence5()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "^" seq2=RewriteSequence4()
		{
			seq = new SequenceXor(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequence5():
{
	Sequence seq, seq2;
	bool random = false;
}
{
	seq=RewriteSequence6()
	(
		LOOKAHEAD(2)
		("$" { random = true; })? "&" seq2=RewriteSequence5()
		{
			seq = new SequenceStrictAnd(seq, seq2, random);
		}
	)?
	{
		return seq;
	}
}

Sequence RewriteSequence6():
{
	Sequence seq;
}
{
    "!" seq=RewriteSequence6()
	{
		return new SequenceNot(seq);
	}
|	
	seq=SingleSequence()
	{
		return seq;
	}
}

Sequence SingleSequence():
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
				seq = new SequenceMin(seq, 0);
			}
		|
			"+"
			{
				seq = new SequenceMin(seq, 1);
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
					seq = new SequenceMin(seq, minnum);
			    }
			    else
			    {
					if(!maxspecified) maxnum = minnum;
					seq = new SequenceMinMax(seq, minnum, maxnum);
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
	Sequence seq;
	List<String> defParamVars = new List<String>();
	String toVarName, typeName = null, fromName;
	IGraphElement elem;
}
{
	LOOKAHEAD(2) toVarName=Word() (":" typeName=Word())? "="
	{
		if(varDecls != null)
		{
			String oldTypeName;
			if(varDecls.TryGetValue(toVarName, out oldTypeName))
			{
				if(typeName != null)
				{
					if(oldTypeName != null)
					throw new ParseException("The variable \"" + toVarName + "\" has already been declared!");
					varDecls[toVarName] = typeName;
				}
			}
			else varDecls[toVarName] = typeName;
		}
	}
    (
        fromName=Word()
        {
            return new SequenceAssignVarToVar(toVarName, fromName);
        }
    |
        "@" "(" fromName=Text() ")"
        {
            if(actions == null)
                throw new ParseException("The @-operator is not allowed without an BaseActions instance!");
            if(namedGraph == null)
                throw new ParseException("The @-operator can only be used with NamedGraphs!");
                
            elem = namedGraph.GetGraphElement(fromName);
            if(elem == null)
                throw new ParseException("Graph element does not exist: \"" + fromName + "\"!");
            return new SequenceAssignElemToVar(toVarName, elem);
        }
    |
		"true"
		{
			return new SequenceAssignSequenceResultToVar(toVarName, new SequenceTrue(false));
		}
    |
		"false"
		{
			return new SequenceAssignSequenceResultToVar(toVarName, new SequenceFalse(false));
		}
    |
		"(" seq=RewriteSequence() ")"
		{
			return new SequenceAssignSequenceResultToVar(toVarName, seq);
		}
    )
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
}

void RuleLookahead():
{
}
{
	("(" Word() (":" Word())? ("," Word() (":" Word())?)* ")" "=")?
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
	List<String> paramVars = new List<String>();
	List<Object> paramConsts = new List<Object>();
	List<String> returnVars = new List<String>();
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
			return new SequenceRuleAll(CreateRuleObject(str, paramVars, paramConsts, returnVars, retSpecified),
					special, test, numChooseRandSpecified ? (int) numChooseRand : 0);
		}
	|
		("%" { special = true; } | "?" { test = true; })*
		str=Word() ("(" RuleParameters(paramVars, paramConsts) ")")?
		{
			return new SequenceRule(CreateRuleObject(str, paramVars, paramConsts, returnVars, retSpecified),
					special, test);
		}
	)
}

CSHARPCODE
RuleObject CreateRuleObject(String ruleName, List<String> paramVars, List<Object> paramConsts,
		List<String> returnVars, bool retSpecified)
{
	IAction action = null;
	if(actions != null)
	{
		action = actions.GetAction(ruleName);
		if(action == null || action.RulePattern.Inputs.Length != paramVars.Count
				|| retSpecified && action.RulePattern.Outputs.Length != returnVars.Count)
			throw new SequenceParserRuleException(ruleName, action, paramVars.Count, returnVars.Count, -1);
		for(int i = 0; i < paramVars.Count; i++)
		{
			// CSharpCC does not support as-expressions, yet...
			VarType inputType = (VarType) (action.RulePattern.Inputs[i] is VarType ? action.RulePattern.Inputs[i] : null);

			// If input type is not a VarType, a variable must be specified.
			// Otherwise, if a constant is specified, the VarType must match the type of the constant
			if(inputType == null && paramVars[i] == null
					|| inputType != null && paramConsts[i] != null && inputType.Type != paramConsts[i].GetType())
				throw new SequenceParserRuleException(ruleName, action, paramVars.Count, returnVars.Count, i);
		}
		if(!retSpecified && action.RulePattern.Outputs.Length > 0)
		{
			for(int i = action.RulePattern.Outputs.Length; i > 0; i--)
			returnVars.Add(null);
		}
	}
	RuleObject ruleObj = new RuleObject(action, paramVars.ToArray(), paramConsts.ToArray(), returnVars.ToArray());
	if(actions == null)
		ruleObj.RuleName = ruleName;

	return ruleObj;
}

TOKEN: { < ERROR: ~[] > }
