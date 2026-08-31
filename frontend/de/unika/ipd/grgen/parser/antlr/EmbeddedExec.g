/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/*
 * GrGen sequence in rule specification language grammar for ANTLR 3
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski, Veit Batz, Edgar Jakumeit, Sebastian Buchwald, Moritz Kroll
*/

parser grammar EmbeddedExec;

@members {
	private OperatorNode makeOp(Antlr.Runtime.IToken t) {
		return gParent.makeOp(t);
	}

	private OperatorNode makeTernOp(Antlr.Runtime.IToken t, ExprNode op0, ExprNode op1, ExprNode op2) {
		return gParent.makeTernOp(t, op0, op1, op2);
	}

	private OperatorNode makeBinOp(Antlr.Runtime.IToken t, ExprNode op0, ExprNode op1) {
		return gParent.makeBinOp(t, op0, op1);
	}

	private OperatorNode makeUnOp(Antlr.Runtime.IToken t, ExprNode op) {
		return gParent.makeUnOp(t, op);
	}

	internal ParserEnvironment env;

	public void setEnv(ParserEnvironment env) {
		this.env = env;
	}

	protected Coords getCoords(Antlr.Runtime.IToken tok) {
		return new de.unika.ipd.grgen.parser.antlr.Coords(tok);
	}

	protected void reportError(de.unika.ipd.grgen.parser.Coords c, String s) {
		gParent.hadError_ = true;
		env.Sys.ErrorReporter.Error(c, s);
	}

	public void displayRecognitionError(String[] tokenNames, RecognitionException e) {
		String hdr = GetErrorHeader(e);
		String msg = GetErrorMessage(e, tokenNames);
		reportError(new de.unika.ipd.grgen.parser.antlr.Coords(e), msg);
	}

	public void reportWarning(de.unika.ipd.grgen.parser.Coords c, String s) {
		env.Sys.ErrorReporter.Warning(c, s);
	}

	public String getFilename() {
		return env.Filename;
	}
}

//////////////////////////////////////////
// Embedded XGRS / extended graph rewrite sequences
//////////////////////////////////////////

// todo: add more user friendly explicit error messages for % used after $ instead of implicit syntax error
// (a user choice $% override for the random flag $ is only available in the shell/debugger)

// note: sequences and expressions are right associative here, that's wrong but doesn't matter cause this is only a syntax checking pre pass
// in the backend, the operators are parsed with correct associativity (and with correct left-to-right, def-before-use order of variables)

sequenceInParameters [ ExecNode xg ] returns [ CollectNode<ExecVarDeclNode> res = new CollectNode<ExecVarDeclNode>() ]
	: LPAREN (sequenceParamList[res, xg])? RPAREN
	|
	;

sequenceOutParameters [ ExecNode xg ] returns [ CollectNode<ExecVarDeclNode> res = new CollectNode<ExecVarDeclNode>() ]
	: COLON LPAREN (sequenceParamList[res, xg])? RPAREN
	|
	;

sequenceParamList [ CollectNode<ExecVarDeclNode> paramz, ExecNode xg ]
	: { xg.DisableXgrsStringBuilding(); } p=seqEntityDecl[xg] { paramz.AddChild(p); xg.EnableXgrsStringBuilding(); }
		( { xg.DisableXgrsStringBuilding(); } COMMA p=seqEntityDecl[xg] { paramz.AddChild(p); xg.EnableXgrsStringBuilding(); } )*
	;

sequence [ ExecNode xg ]
	: seqLazyOr[xg]
		( (DOLLAR THENLEFT { xg.Append(" $<; "); } | THENLEFT { xg.Append(" <; "); } 
			| DOLLAR THENRIGHT { xg.Append(" $;> "); } | THENRIGHT { xg.Append(" ;> "); }) sequence[xg]
		)?
	;

seqLazyOr [ ExecNode xg ]
	: seqLazyAnd[xg]
		( (DOLLAR LOR { xg.Append(" $|| "); } | LOR { xg.Append(" || "); }) seqLazyOr[xg]
		)?
	;

seqLazyAnd [ ExecNode xg ]
	: seqStrictOr[xg]
		( (DOLLAR LAND { xg.Append(" $&& "); } | LAND { xg.Append(" && "); }) seqLazyAnd[xg]
		)?
	;

seqStrictOr [ ExecNode xg ]
	: seqStrictXor[xg]
		( (DOLLAR BOR { xg.Append(" $| "); } | BOR { xg.Append(" | "); }) seqStrictOr[xg]
		)?
	;

seqStrictXor [ ExecNode xg ]
	: seqStrictAnd[xg]
		( (DOLLAR BXOR { xg.Append(" $^ "); } | BXOR { xg.Append(" ^ "); }) seqStrictXor[xg]
		)?
	;

seqStrictAnd [ ExecNode xg ]
	: seqNegOrIteration[xg]
		( (DOLLAR BAND { xg.Append(" $& "); } | BAND { xg.Append(" & "); }) seqStrictAnd[xg]
		)?
	;

seqNegOrIteration [ ExecNode xg ]
	: NOT { xg.Append("!"); } seqIterSequence[xg] 
		( (ASSIGN_TO { xg.Append("=>"); } | BOR_TO { xg.Append("|>"); } | BAND_TO { xg.Append("&>"); }) seqEntity[xg]
		)?
	| seqIterSequence[xg]
		( (ASSIGN_TO { xg.Append("=>"); } | BOR_TO { xg.Append("|>"); } | BAND_TO { xg.Append("&>"); }) seqEntity[xg]
		)?
	;

seqIterSequence [ ExecNode xg ]
	: seqSimpleSequence[xg]
		(
			rsn=seqRangeSpecLoop[xg]
		|
			STAR { xg.Append("*"); }
		|
			PLUS { xg.Append("+"); }
		)
	;

seqSimpleSequence [ ExecNode xg ]
	options { k = 4; }
	@init {
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	// attention/todo: names are are only partly resolved!
	// -> using not existing types, not declared names outside of the return assignment of an action call 
	// will not be detected in the frontend; xgrs in the frontend are to a certain degree syntax only
	: (seqEntity[null] (ASSIGN | GE )) => lhs=seqEntity[xg] (ASSIGN { xg.Append("="); } | GE { xg.Append(">="); })
		(
			id=seqEntIdentUse LPAREN // deliver understandable error message for case of missing parenthesis at rule result assignment
				{ reportError(id.Coords, "The destination variable(s) of a rule result assignment must be enclosed in parenthesis."); }
		|
			(seqConstant[null]) => seqConstant[xg]
		|
			seqInitObjectExpr[xg]
		|
			seqVarUse[xg]
		|
			d=DOLLAR MOD LPAREN seqTypeIdentUse RPAREN
			{ reportError(getCoords(d), "User input is only requestable in the GrShell, not at lgsp(libgr search plan backend)-level."); }
		|
			d=DOLLAR LPAREN 
			(
				n=NUM_INTEGER RPAREN { xg.Append("$("); xg.Append(n.Text); xg.Append(")"); }
				| f=NUM_DOUBLE RPAREN { xg.Append("$("); xg.Append(f.Text); xg.Append(")"); }
			)
		|
			LPAREN { xg.Append('('); } sequence[xg] RPAREN { xg.Append(')'); }
		)
	| seqVarDecl=seqEntityDecl[xg]
	| YIELD { xg.Append("yield "); } lhsent=seqEntIdentUse { xg.Append(lhsent); xg.AddUsage(lhsent); } ASSIGN { xg.Append('='); } 
		( (seqConstant[null]) => seqConstant[xg]
		| seqVarUse[xg]
		)
	| TRUE { xg.Append("true"); }
	| FALSE { xg.Append("false"); }
	| seqRulePrefixedSequence[xg, returns]
	| seqMultiRulePrefixedSequence[xg, returns]
	| (seqParallelCallRule[null, null]) => seqParallelCallRule[xg, returns]
	| seqMultiRuleAllCall[xg, returns, true]
	| DOUBLECOLON id=seqEntIdentUse { xg.Append("::" + id); xg.AddUsage(id); }
	| (( DOLLAR ( MOD )? )? LBRACE LT) => ( DOLLAR { xg.Append("$"); } ( MOD { xg.Append("\%"); } )? )?
		LBRACE LT { xg.Append("{<"); } seqParallelCallRule[xg, returns]
			( COMMA { xg.Append(","); returns = new CollectNode<BaseNode>(); } seqParallelCallRule[xg, returns] )*
			GT RBRACE { xg.Append(">}"); }
	| DOLLAR { xg.Append("$"); } ( MOD { xg.Append("\%"); } )? 
		(LOR { xg.Append("||"); } | LAND { xg.Append("&&"); } | BOR { xg.Append("|"); } | BAND { xg.Append("&"); }) 
		LPAREN { xg.Append("("); } sequence[xg] ( COMMA { xg.Append(","); } sequence[xg] )* RPAREN { xg.Append(")"); }
	| DOLLAR { xg.Append("$"); } ( MOD { xg.Append("\%"); } )? DOT { xg.Append("."); } 
		LPAREN { xg.Append("("); } f=NUM_DOUBLE { xg.Append(f.Text + " "); } sequence[xg] 
			( COMMA { xg.Append(","); } f=NUM_DOUBLE { xg.Append(f.Text + " "); } sequence[xg] )* RPAREN { xg.Append(")"); }
	| LPAREN { xg.Append("("); } sequence[xg] RPAREN { xg.Append(")"); }
	| LT { xg.Append(" <"); } sequence[xg] GT { xg.Append("> "); }
	| l=SL { env.PushScope("backtrack/exec", getCoords(l)); } { xg.Append(" <<"); } 
		seqParallelCallRule[xg, returns] (DOUBLE_SEMI|SEMI) { xg.Append(";;"); }
		sequence[xg] { env.PopScope(); } SR { xg.Append(" >> "); }
	| l=SL { env.PushScope("backtrack/exec", getCoords(l)); } { xg.Append(" <<"); } 
		seqMultiRuleAllCall[xg, returns, false] (DOUBLE_SEMI|SEMI) { xg.Append(";;"); }
		sequence[xg] { env.PopScope(); } SR { xg.Append(" >> "); }
	| SL { xg.Append(" <<"); } seqMultiRulePrefixedSequence[xg, returns] SR { xg.Append(" >> "); }
	| DIV { xg.Append(" /"); } sequence[xg] DIV { xg.Append("/ "); }
	| LTCOLON { xg.Append(" <:"); } sequence[xg] GTCOLON { xg.Append(":> "); }
	| GTLTCOLON { xg.Append(" >:< "); }
	| IF l=LBRACE { env.PushScope("if/exec", getCoords(l)); } { xg.Append("if{"); } sequence[xg] s=SEMI 
		{ env.PushScope("if/then-part", getCoords(s)); } { xg.Append("; "); }
		sequence[xg] { env.PopScope(); } (SEMI { xg.Append("; "); } sequence[xg])? { env.PopScope(); } RBRACE { xg.Append("}"); }
	| FOR l=LBRACE { env.PushScope("for/exec", getCoords(l)); } { xg.Append("for{"); } seqEntity[xg] seqForSeqRemainder[xg, returns]
	| i=IN { xg.Append("in "); } { env.PushScope("in subgraph sequence", getCoords(i)); } seqExpression[xg]
		LBRACE { xg.Append("{"); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); } 
	| LOCK l=LPAREN { xg.Append("lock("); } { env.PushScope("lock sequence", getCoords(l)); } seqExpression[xg] RPAREN  { xg.Append(")"); }
		LBRACE { xg.Append("{"); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); } 
	| LBRACE { xg.Append("{"); } { env.PushScope("sequence computation", getCoords(l)); }
		seqCompoundComputation[xg] (SEMI)? { env.PopScope(); } RBRACE { xg.Append("}"); } 
	;

seqForSeqRemainder [ ExecNode xg, CollectNode<BaseNode> returns ]
	options { k = 4; }
	: (RARROW { xg.Append(" -> "); } seqEntity[xg])? IN { xg.Append(" in "); } seqVarUse[xg]
			SEMI { xg.Append("; "); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); }
	| IN { xg.Append(" in "); } seqForFunctionRemainder[xg]
			SEMI { xg.Append("; "); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); }
	| IN { xg.Append(" in "); } LBRACE { xg.Append("{"); } seqIndex[xg] EQUAL { xg.Append(" == "); } seqExpression[xg] 
		RBRACE { xg.Append("}"); } SEMI { xg.Append("; "); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); }
	| IN { xg.Append(" in "); } LBRACE { xg.Append("{"); } 
		i=IDENT { xg.Append(i.Text); } 
		LPAREN { xg.Append("("); } seqIndex[xg] ( seqRelOs[xg] seqExpression[xg]
				( COMMA { xg.Append(","); } seqIndex[xg] seqRelOs[xg] seqExpression[xg] )? )? 
		RPAREN { xg.Append(")"); } RBRACE { xg.Append("}"); } SEMI { xg.Append("; "); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); }
	| IN LBRACK QUESTION { xg.Append(" in [?"); } seqCallRule[xg, null, returns, true] RBRACK { xg.Append("]"); }
			SEMI { xg.Append("; "); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); }
	| IN LBRACK { xg.Append(" in ["); } left=seqExpression[xg] COLON { xg.Append(" : "); } right=seqExpression[xg] RBRACK { xg.Append("]"); }
			SEMI { xg.Append("; "); } sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); }
	;

seqForFunctionRemainder [ ExecNode xg ]
	: { ParserEnvironment.IsKnownForFunction(input.LT(1).Text) }?
		i=IDENT LPAREN { xg.Append(i.Text); xg.Append("("); }
		(expr1=seqExpression[xg] (COMMA { xg.Append(","); } expr2=seqExpression[xg] 
			(COMMA { xg.Append(","); } expr3=seqExpression[xg] (COMMA { xg.Append(","); } expr4=seqExpression[xg])? )? 
				)? )?
		RPAREN { xg.Append(")"); }
	| { ParserEnvironment.IsKnownForIndexFunction(input.LT(1).Text) }?
		i=IDENT LPAREN { xg.Append(i.Text); xg.Append("("); }
			seqForIndexFunctionCallParameters[xg]
		RPAREN { xg.Append(")"); }
	;

seqForIndexFunctionCallParameters [ ExecNode xg ]
	: fromExpr=seqExpression[xg] ( COMMA { xg.Append(","); } fromExpr2=seqExpression[xg] )*
	;

seqCompoundComputation [ ExecNode xg ]
	: seqComputation[xg] (SEMI { xg.Append(";"); } seqCompoundComputation[xg])?
	;

seqComputation [ ExecNode xg ]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.Append("="); } | GE { xg.Append(">="); })
		seqExpressionOrAssign[xg]
	| seqEntityDecl[xg]
	| seqProcedureOrMethodCall[xg]
	| LBRACE { xg.Append("{"); } seqExpression[xg] RBRACE { xg.Append("}"); }
	;

seqExpressionOrAssign [ ExecNode xg ]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.Append("="); } | GE { xg.Append(">="); })
		seqExpressionOrAssign[xg]
	| seqExpression[xg] 
	;

seqAssignTarget [ ExecNode xg ]
	: YIELD { xg.Append("yield "); } seqVarUse[xg] 
	| seqVarUse[xg] seqAssignTargetSelector[xg]
	| seqEntityDecl[xg]
	;

seqAssignTargetSelector [ ExecNode xg ]
	: DOT attr=IDENT { xg.Append("."+attr.Text); } 
		(LBRACK { xg.Append("["); } seqExpression[xg] RBRACK { xg.Append("]"); })?
	| DOT VISITED { xg.Append(".visited"); } (LBRACK { xg.Append("["); } seqExpression[xg] RBRACK { xg.Append("]"); })?
	| LBRACK { xg.Append("["); } seqExpression[xg] RBRACK { xg.Append("]"); }
	|
	;

// todo: add expression value returns to remaining sequence expressions,
// as of now only some sequence expressions return an expression
// the expressions are needed for the argument expressions of rule/sequence calls,
// in all other places of the sequences we only need a textual emit of the constructs just parsed
seqExpression [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrCond=seqExprLazyOr[xg] { res = expOrCond; }
		( q=QUESTION { xg.Append("?"); } trueCase=seqExpression[xg] COLON { xg.Append(" : "); } falseCase=seqExpression[xg]
			{ res = makeTernOp(q, expOrCond, trueCase, falseCase); }
		)?
	;

seqExprLazyOr [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprLazyAnd[xg] { res = expOrLeft; } 
		( op=LOR { xg.Append(" || "); } right=seqExprLazyOr[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprLazyAnd [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprStrictOr[xg] { res = expOrLeft; }
		( op=LAND { xg.Append(" && "); } right=seqExprLazyAnd[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprStrictOr [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprStrictXor[xg] { res = expOrLeft; }
		( op=BOR { xg.Append(" | "); } right=seqExprStrictOr[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprStrictXor [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprStrictAnd[xg] { res = expOrLeft; }
		( op=BXOR { xg.Append(" ^ "); } right=seqExprStrictXor[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprStrictAnd [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprExcept[xg] { res = expOrLeft; }
		( op=BAND { xg.Append(" & "); } right=seqExprStrictAnd[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprExcept [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprEquality[xg] { res = expOrLeft; }
		( op=BACKSLASH { xg.Append(" \\ "); } right=seqExprExcept[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;
	
seqEqOp [ ExecNode xg ] returns [ IToken t = null ]
	: e=EQUAL { xg.Append(" == "); t = e; }
	| n=NOT_EQUAL { xg.Append(" != "); t = n; }
	| s=STRUCTURAL_EQUAL { xg.Append(" ~~ "); t = s; }
	;

seqExprEquality [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprRelation[xg] { res = expOrLeft; }
		( op=seqEqOp[xg] right=seqExprEquality[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqRelOp [ ExecNode xg ] returns [ IToken t = null ]
	: lt=LT { xg.Append(" < "); t = lt; }
	| le=LE { xg.Append(" <= "); t = le; }
	| gt=GT { xg.Append(" > "); t = gt; }
	| ge=GE { xg.Append(" >= "); t = ge; }
	| in_=IN { xg.Append(" in "); t = in_; }
	;

seqRelOs [ ExecNode xg ] returns [ IToken t = null ]
	: lt=LT { xg.Append(" < "); t = lt; }
	| le=LE { xg.Append(" <= "); t = le; }
	| gt=GT { xg.Append(" > "); t = gt; }
	| ge=GE { xg.Append(" >= "); t = ge; }
	;

seqExprRelation [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprShift[xg] { res = expOrLeft; } 
		( op=seqRelOp[xg] right=seqExprRelation[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqShiftOp [ ExecNode xg ] returns [ IToken t = null ]
	: sl=SL { xg.Append(" << "); t = sl; }
	| sr=SR { xg.Append(" >> "); t = sr; }
	| bsr=BSR { xg.Append(" >>> "); t = bsr; }
	;

seqExprShift [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprAdd[xg] { res = expOrLeft; } 
		( op=seqShiftOp[xg] right=seqExprShift[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqAddOp [ ExecNode xg ] returns [ IToken t = null ]
	: p=PLUS { xg.Append(" + "); t = p; }
	| m=MINUS { xg.Append(" - "); t = m; }
	;

seqExprAdd [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprMul[xg] { res = expOrLeft; } 
		( op=seqAddOp[xg] right=seqExprAdd[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqMulOp [ ExecNode xg ] returns [ IToken t = null ]
	: s=STAR { xg.Append(" * "); t = s; }
	| m=MOD { xg.Append(" \% "); t = m; }
	| d=DIV { xg.Append(" / "); t = d; }
	;

seqExprMul [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: expOrLeft=seqExprUnary[xg] { res = expOrLeft; } 
		( op=seqMulOp[xg] right=seqExprMul[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprUnary [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		IToken t = null;
	}
	: (LPAREN seqTypeIdentUse RPAREN) =>
		p=LPAREN { xg.Append("("); } id=seqTypeIdentUse {xg.Append(id);} RPAREN { xg.Append(")"); } op=seqExprUnary[xg]
		{ res = new CastNode(getCoords(p), id, op); }
	| (LPAREN genericTypeForCast[null] RPAREN) =>
		p=LPAREN { xg.Append("("); } type=genericTypeForCast[xg] RPAREN { xg.Append(")"); } op=seqExprUnary[xg]
		{ res = new CastNode(getCoords(p), type, op); }
	| (n=NOT { t = n; xg.Append("!"); })? exp=seqExprBasic[xg]
		{
			if(t != null)
				res = makeUnOp(t, exp);
			else
				res = exp;
		}
	| m=MINUS { xg.Append("-"); } exp=seqExprBasic[xg]
		{
			OperatorNode neg = new ArithmeticOperatorNode(getCoords(m), Operator.NEG);
			neg.AddChild(exp);
			res = neg;
		}
	| p=PLUS { xg.Append("+"); } exp=seqExprBasic[xg]
		{
			res = exp;
		}
	| ti=TILDE { xg.Append("~"); } exp=seqExprBasic[xg]
		{
			res = exp;
		}
	;

genericTypeForCast [ ExecNode xg ] returns [ BaseNode res = null ]
	:
		{ input.LT(1).Text.Equals("map") }?
		IDENT LT keyType=seqTypeIdentUse COMMA valType=seqTypeIdentUse GT
		{ xg.Append("map<" + keyType.ToString() + "," + valType.ToString() + ">"); }
		{ res = new MapTypeNode(keyType, valType); }
	|
		{ input.LT(1).Text.Equals("set") }?
		IDENT LT valType=seqTypeIdentUse GT
		{ xg.Append("set<" + valType.ToString() + ">"); }
		{ res = new SetTypeNode(valType); }
	|
		{ input.LT(1).Text.Equals("array") }?
		IDENT LT valType=seqTypeIdentUse GT
		{ xg.Append("array<" + valType.ToString() + ">"); }
		{ res = new ArrayTypeNode(valType); }
	|
		{ input.LT(1).Text.Equals("deque") }?
		IDENT LT valType=seqTypeIdentUse GT
		{ xg.Append("deque<" + valType.ToString() + ">"); }
		{ res = new DequeTypeNode(valType); }
	;

// todo: the seqVarUse[xg] casted to IdenNodes might be not simple variable identifiers, but global variables with :: prefix,
//  probably a distinction is needed
seqExprBasic [ExecNode xg] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	options { k = 4; }
	@init {
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		IdentNode id;
	}
	: owner=seqVarUseInExpr[xg] sel=seqExprSelector[owner, xg] { res = sel; }
	| {input.LT(1).Text.Equals("this")}? i=IDENT { xg.Append("this"); } sel=seqExprSelector[new ThisExprNode(getCoords(i)), xg] { res = sel; }
	| fc=seqFunctionCall[xg] { res = fc; } sel=seqExprSelector[fc, xg] { res = sel; }
	| fc=seqScanFunctionCall[xg] { res = fc; } sel=seqExprSelector[fc, xg] { res = sel; }
	| DEF LPAREN { xg.Append("def("); } seqVariableList[xg, returns] RPAREN { xg.Append(")"); } 
	| a=AT LPAREN { xg.Append("@("); } 
		(i=IDENT { xg.Append(i.Text); } | s=STRING_LITERAL { xg.Append(s.Text); }) RPAREN { xg.Append(")"); }
	| rq=seqRuleQuery[xg] sel=seqExprSelector[rq, xg] { res = sel; }
	| mrq=seqMultiRuleQuery[xg] sel=seqExprSelector[mrq, xg] { res = sel; }
	| mc=seqMappingClause[xg] sel=seqExprSelector[mc, xg] { res = sel; }
	| LPAREN { xg.Append("("); } seqExpression[xg] RPAREN { xg.Append(")"); } 
	| exp=seqConstantOfBasicOrEnumType[xg] sel=seqExprSelector[(ExprNode)exp, xg] { res = sel; }
	| (seqConstantOfContainerType[null]) => exp=seqConstantOfContainerType[xg] sel=seqExprSelector[(ExprNode)exp, xg] { res = sel; }
	| seqConstantOfMatchClassType[xg]
	| exp=seqInitObjectExpr[xg]
	| {env.Test(ParserEnvironment.TYPES, input.LT(1).Text) && !env.Test(ParserEnvironment.ENTITIES, input.LT(1).Text)}? i=IDENT
		{
			id = new IdentNode(env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i)));
			res = new IdentExprNode(id);
			xg.Append(i.Text);
		}
	;

seqVarUseInExpr [ ExecNode xg ] returns [ IdentExprNode res ]
	: var=seqVarUse[xg] { res = new IdentExprNode(var); }
	;

seqExprSelector [ ExprNode prefix, ExecNode xg ] returns [ ExprNode res = prefix ]
	options { k = 3; }
	@init {
		CollectNode<ExprNode> arguments = new CollectNode<ExprNode>();
	}
	: d=DOT methodOrAttrName=seqMemberIdentUse { xg.Append("."+methodOrAttrName.Symbol.Text); } 
		(
			{ ParserEnvironment.IsArrayAttributeAccessMethodName(input.Get(input.LT(1).TokenIndex-1).Text) }?
				LT mi=seqMemberIdentUse GT { xg.Append("<" + mi.Symbol.Text + ">"); }
			LPAREN { xg.Append("("); } 
			( arg=seqExpression[xg] { arguments.AddChild(arg); }
				( COMMA { xg.Append(","); } arg=seqExpression[xg] { arguments.AddChild(arg); } )*
				)? RPAREN { xg.Append(")"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, mi); }
		|
			{ input.Get(input.LT(1).TokenIndex-1).Text.Equals("map") }?
				LT ti=seqTypeIdentUse GT { xg.Append("<" + ti.Symbol.Text + ">"); }
			( combinedName=seqInitExpression[xg, methodOrAttrName.ToString()]
				{ if(!combinedName.Equals("mapStartWithAccumulateBy"))
					reportError(getCoords(d), "The lambda-expression method " + combinedName + " is not known. Available are: assign, removeIf, assignStartWithAccumulateBy.");
				}
			)?
			LBRACE { xg.Append("{"); } { env.PushScope("arraymap/exec", getCoords(d)); }
				seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
				{ env.PopScope(); } RBRACE { xg.Append("}"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, ti); } // note: "assign" als in case of "assignStartWithAccumulateBy"
		|
			{ input.Get(input.LT(1).TokenIndex-1).Text.Equals("removeIf") }?
			LBRACE { xg.Append("{"); } { env.PushScope("arrayremoveIf/exec", getCoords(d)); }
				seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
				{ env.PopScope(); } RBRACE { xg.Append("}"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, ti); }
		|
			LPAREN { xg.Append("("); } 
			( arg=seqExpression[xg] { arguments.AddChild(arg); }
				( COMMA { xg.Append(","); } arg=seqExpression[xg] { arguments.AddChild(arg); } )*
				)? RPAREN { xg.Append(")"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, mi); }
		|
			{ res = new MemberAccessExprNode(getCoords(d), prefix, methodOrAttrName); }
		)
		sel=seqExprSelector[res, xg] { res = sel; }
	| DOT v=VISITED { xg.Append(".visited"); } ((LBRACK) => LBRACK 
		{ xg.Append("["); } visId=seqExpression[xg] RBRACK { xg.Append("]"); })?
		{ res = new VisitedNode(getCoords(v), visId, prefix); }
		sel=seqExprSelector[res, xg] { res = sel; }
	| l=LBRACK { xg.Append("["); } key=seqExpression[xg] RBRACK { xg.Append("]"); }
		{ res = makeBinOp(l, prefix, key); } // array/deque/map access
		sel=seqExprSelector[res, xg] { res = sel; }
	| // no selector
	;

seqLambdaExprVarDeclPrefix [ ExecNode xg ]
	options { k = *; }
	: 	seqEntityDecl[xg] SEMI { xg.Append(";"); }
			seqMaybePreviousAccumulationAccessLambdaExprVarDecl[xg]
	|
		seqMaybePreviousAccumulationAccessLambdaExprVarDecl[xg]
	;

seqMaybePreviousAccumulationAccessLambdaExprVarDecl [ ExecNode xg ]
	options { k = *; }
	: 	seqEntityDecl[xg] COMMA { xg.Append(","); }
			seqMaybeIndexedLambdaExprVarDecl[xg]
	|
		seqMaybeIndexedLambdaExprVarDecl[xg]
	;

seqMaybeIndexedLambdaExprVarDecl [ ExecNode xg ]
	options { k = *; }
	: 	seqEntityDecl[xg] RARROW { xg.Append("->"); }
			seqEntityDecl[xg] RARROW { xg.Append("->"); }
	|
		seqEntityDecl[xg] RARROW { xg.Append("->"); }
	;

seqProcedureOrMethodCall [ ExecNode xg ]
	@init {
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	// built-in procedure or user defined procedure, backend has to decide whether the call is valid
	: ( LPAREN { xg.Append("("); } seqVariableList[xg, returns] RPAREN ASSIGN { xg.Append(")="); } )?
		( p=IDENT DOUBLECOLON { xg.Append(p.Text); xg.Append("::"); } )?
		( seqVarUse[xg] d=DOT { xg.Append("."); } (attrName=IDENT DOT { xg.Append(attrName.Text+ "."); })? )?
		( i=IDENT | i=EMIT | i=EMITDEBUG | i=DELETE) LPAREN { xg.Append(i.Text); xg.Append("("); } 
			seqFunctionCallParameters[xg] RPAREN { xg.Append(")"); }
	;

seqFunctionCall [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init{
		bool inPackage = false;
		bool packPrefix = false;
	}
	// built-in function or user defined function, backend has to decide whether the call is valid
	: ( p=IDENT DOUBLECOLON { xg.Append(p.Text); xg.Append("::"); packPrefix=true; } )?
	  ( i=IDENT | i=COPY | i=CLONE | i=NAMEOF | i=TYPEOF ) LPAREN { xg.Append(i.Text); xg.Append("("); }
			paramz=seqFunctionCallParameters[xg] RPAREN { xg.Append(")"); }
		{
			if(i.Text.Equals("now") && paramz.ChildrenExact.Count == 0 || ParserEnvironment.IsGlobalFunction(null, i, paramz)) {
				IdentNode funcIdent = new IdentNode(env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
				if(packPrefix) {
					res = new PackageFunctionInvocationDecisionNode(p.Text, funcIdent, paramz, env);
				} else {
					res = new FunctionInvocationDecisionNode(funcIdent, paramz, env);
				}
			} else {
				IdentNode funcIdent = inPackage ? 
					new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
						env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)))
					: new IdentNode(env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
				res = new FunctionOrExternalFunctionInvocationExprNode(funcIdent, paramz);
			}
		}
	;

seqFunctionCallParameters [ ExecNode xg ] returns [ CollectNode<ExprNode> paramz = new CollectNode<ExprNode>(); ]
	: (fromExpr=seqExpression[xg] { paramz.AddChild(fromExpr); }
		( COMMA { xg.Append(","); } fromExpr2=seqExpression[xg] { paramz.AddChild(fromExpr2); } )* )?
	;

seqScanFunctionCall [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: ( s=SCAN { xg.Append(s.Text); } | s=TRYSCAN { xg.Append(s.Text); } ) (LT { xg.Append("<"); } type=seqTypeOrContainerTypeContinuation[xg])? LPAREN { xg.Append("("); }
			paramz=seqFunctionCallParameters[xg] RPAREN { xg.Append(")"); }
		{
			if(paramz.ChildrenExact.Count == 1) {
				if(s.Text.Equals("scan")) {
					res = new ScanExprNode(getCoords(s), type, paramz.Get(0));
				} else {
					res = new TryScanExprNode(getCoords(s), type, paramz.Get(0));
				}
			} else {
				reportError(getCoords(s), "The function " + s.Text + " expects 1 parameter (and a type parameter) (given are " + paramz.ChildrenExact.Count + ").");
			}
		}
	;

seqTypeOrContainerTypeContinuation [ ExecNode xg ] returns [ BaseNode res = null ]
	: { input.LT(1).Text.Equals("map") }?
		i=IDENT LT { xg.Append(i.Text + "<"); } keyType=seqTypeIdentUse COMMA { xg.Append(keyType + ","); } valueType=seqTypeIdentUse { xg.Append(valueType); } (GT GT { xg.Append("> >"); } | SR { xg.Append(">>"); } )
		{ res = new MapTypeNode(keyType, valueType); }
	| { input.LT(1).Text.Equals("set") }?
		i=IDENT LT { xg.Append(i.Text + "<"); } valueType=seqTypeIdentUse { xg.Append(valueType); } (GT GT { xg.Append("> >"); } | SR { xg.Append(">>"); })
		{ res = new SetTypeNode(valueType); }
	| { input.LT(1).Text.Equals("array") }?
		i=IDENT LT { xg.Append(i.Text + "<"); } valueType=seqTypeIdentUse { xg.Append(valueType); } (GT GT { xg.Append("> >"); } | SR { xg.Append(">>"); })
		{ res = new ArrayTypeNode(valueType); }
	| { input.LT(1).Text.Equals("deque") }?
		i=IDENT { xg.Append(i.Text + "<"); } LT valueType=seqTypeIdentUse { xg.Append(valueType); } (GT GT { xg.Append("> >"); } | SR { xg.Append(">>"); })
		{ res = new DequeTypeNode(valueType); }
	| typeIdent=seqTypeIdentUse GT
		{ res = typeIdent; } { xg.Append(typeIdent); } { xg.Append(">"); }
	;

seqConstant [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		IdentNode id;
	}
	: seqConstantOfBasicOrEnumType[xg]
	| seqConstantOfContainerType[xg]
	| {env.Test(ParserEnvironment.TYPES, input.LT(1).Text) && !env.Test(ParserEnvironment.ENTITIES, input.LT(1).Text)}? i=IDENT
		{
			id = new IdentNode(env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i)));
			res = new IdentExprNode(id);
			xg.Append(i.Text);
		}
	;
	
seqConstantOfBasicOrEnumType [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: b=NUM_BYTE { xg.Append(b.Text); res = new ByteConstNode(getCoords(b), SByte.Parse(ByteConstNode.RemoveSuffix(b.Text))); }
	| sh=NUM_SHORT { xg.Append(sh.Text); res = new ShortConstNode(getCoords(sh), Int16.Parse(ShortConstNode.RemoveSuffix(sh.Text))); }
	| i=NUM_INTEGER { xg.Append(i.Text); res = new IntConstNode(getCoords(i), Int32.Parse(i.Text)); }
	| l=NUM_LONG { xg.Append(l.Text); res = new LongConstNode(getCoords(l), Int64.Parse(LongConstNode.RemoveSuffix(l.Text))); }
	| f=NUM_FLOAT { xg.Append(f.Text); res = new FloatConstNode(getCoords(f), Single.Parse(FloatConstNode.RemoveSuffix(f.Text), System.Globalization.CultureInfo.InvariantCulture)); }
	| d=NUM_DOUBLE { xg.Append(d.Text); res = new DoubleConstNode(getCoords(d), Double.Parse(DoubleConstNode.RemoveSuffix(d.Text), System.Globalization.CultureInfo.InvariantCulture)); }
	| s=STRING_LITERAL { xg.Append(s.Text); String buff = s.Text;
			// Strip the " from the string
			buff = buff.Substring(1, buff.Length - 2);
			res = new StringConstNode(getCoords(s), buff); }
	| tt=TRUE { xg.Append(tt.Text); res = new BoolConstNode(getCoords(tt), true); }
	| ff=FALSE { xg.Append(ff.Text); res = new BoolConstNode(getCoords(ff), false); }
	| n=NULL { xg.Append(n.Text); res = new NullConstNode(getCoords(n)); }
	| i1=IDENT d=DOUBLECOLON i2=IDENT e=seqConstantOfBasicOrEnumTypeCont[xg, i1, d, i2] { res = e; }
	;

seqConstantOfMatchClassType [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: (NEW { xg.Append("new "); })? MATCH LT CLASS { xg.Append("match<class "); } type=seqTypeIdentUse GT LPAREN RPAREN { xg.Append(type + ">()"); }
	;
	
seqConstantOfContainerType [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: (NEW { xg.Append("new "); })? init=seqConstantOfContainerTypeCont[xg] { res = init; }
	;

seqConstantOfContainerTypeCont [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: { input.LT(1).Text.Equals("map") }?
		IDENT LT typeName=seqTypeIdentUse COMMA toTypeName=seqTypeIdentUse GT { xg.Append("map<"+typeName+","+toTypeName+">"); } 
		e1=seqInitMapExpr[xg, new MapTypeNode(typeName, toTypeName)] { res = e1; }
	| { input.LT(1).Text.Equals("set") }?
		IDENT LT typeName=seqTypeIdentUse GT { xg.Append("set<"+typeName+">"); } 
		e2=seqInitSetExpr[xg, new SetTypeNode(typeName)] { res = e2; }
	| { input.LT(1).Text.Equals("array") }?
		IDENT LT { xg.Append("array<"); } e3=seqConstantOfContainerTypeArrayCont[xg] { res = e3; }
	| { input.LT(1).Text.Equals("deque") }?
		IDENT LT typeName=seqTypeIdentUse GT { xg.Append("deque<"+typeName+">"); } 
		e4=seqInitDequeExpr[xg, new DequeTypeNode(typeName)] { res = e4; }
	;

seqConstantOfBasicOrEnumTypeCont [ ExecNode xg, IToken i1, IToken d1, IToken i2 ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		IdentNode id;
	}
	:
		{
			IToken pen = i1;
			if(env.Test(ParserEnvironment.PACKAGES, pen.Text) || !env.Test(ParserEnvironment.TYPES, pen.Text)) {
				id = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, pen.Text, getCoords(pen)), 
						env.Occurs(ParserEnvironment.TYPES, i2.Text, getCoords(i2)));
				res = new IdentExprNode(id);
			} else {
				res = new DeclExprNode(new EnumExprNode(getCoords(d1), 
					new IdentNode(env.Occurs(ParserEnvironment.TYPES, pen.Text, getCoords(pen))),
					new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i2.Text, getCoords(i2)))));
			}
			xg.Append(pen.Text + "::" + i2.Text);
		}
	|
		d2=DOUBLECOLON i=IDENT
		{
			IToken p = i1;
			IToken en = i2;
			res = new DeclExprNode(new EnumExprNode(getCoords(d2), 
					new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)),
							env.Occurs(ParserEnvironment.TYPES, en.Text, getCoords(en))),
					new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)))));
			xg.Append(p.Text + "::" + en.Text + "::" + i.Text);
		}
	;

seqConstantOfContainerTypeArrayCont [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: typeName=seqTypeIdentUse GT { xg.Append(typeName + ">"); } 
		e=seqInitArrayExpr[xg, new ArrayTypeNode(typeName)] { res = e; }
	| typeName=seqMatchTypeIdentUseInContainerType[xg] (GT GT { xg.Append("> >"); } | SR { xg.Append(">>"); })
		e=seqInitArrayExpr[xg, new ArrayTypeNode(typeName)] { res = e; }
	;

seqInitMapExpr [ ExecNode xg, MapTypeNode mapType ] returns [ ExprNode res = null ]
	@init {
		MapInitNode mapInit = null;
	}
	: l=LBRACE { xg.Append("{"); } { res = mapInit = new MapInitNode(getCoords(l), null, mapType); }
		( item1=seqKeyToValue[xg] { mapInit.AddPairItem(item1); }
			( COMMA { xg.Append(","); } item2=seqKeyToValue[xg] { mapInit.AddPairItem(item2); } )*
		)?
	  RBRACE { xg.Append("}"); }
	| l=LPAREN { xg.Append("("); } value=seqExpression[xg]
		{ res = new MapCopyConstructorNode(getCoords(l), null, mapType, value); }
	  RPAREN { xg.Append(")"); }
	;

seqInitSetExpr [ ExecNode xg, SetTypeNode setType ] returns [ ExprNode res = null ]
	@init {
		SetInitNode setInit = null;
	}
	: l=LBRACE { xg.Append("{"); } { res = setInit = new SetInitNode(getCoords(l), null, setType); }
		( seqInitializerOfSingleElements[xg, setInit] )?
	  RBRACE { xg.Append("}"); }
	| l=LPAREN { xg.Append("("); } value=seqExpression[xg]
		{ res = new SetCopyConstructorNode(getCoords(l), null, setType, value); }
	  RPAREN { xg.Append(")"); }
	;

seqInitArrayExpr [ ExecNode xg, ArrayTypeNode arrayType ] returns [ ExprNode res = null ]
	@init {
		ArrayInitNode arrayInit = null;
	}
	: l=LBRACK { xg.Append("["); } { res = arrayInit = new ArrayInitNode(getCoords(l), null, arrayType); }	
		( seqInitializerOfSingleElements[xg, arrayInit] )?
	  RBRACK { xg.Append("]"); }
	| l=LPAREN { xg.Append("("); } value=seqExpression[xg]
		{ res = new ArrayCopyConstructorNode(getCoords(l), null, arrayType, value); }
	  RPAREN { xg.Append(")"); }
	;

seqInitDequeExpr [ ExecNode xg, DequeTypeNode dequeType ] returns [ ExprNode res = null ]
	@init {
		DequeInitNode dequeInit = null;
	}
	: l=LBRACK { xg.Append("["); } { res = dequeInit = new DequeInitNode(getCoords(l), null, dequeType); }	
		( seqInitializerOfSingleElements[xg, dequeInit] )?
	  RBRACK { xg.Append("]"); }
	| l=LPAREN { xg.Append("("); } value=seqExpression[xg]
		{ res = new DequeCopyConstructorNode(getCoords(l), null, dequeType, value); }
	  RPAREN { xg.Append(")"); }
	;

seqInitializerOfSingleElements [ ExecNode xg, ContainerSingleElementInitNode initNode ]
	: item1=seqExpression[xg] { initNode.AddItem(item1); }
		( COMMA { xg.Append(","); } item2=seqExpression[xg] { initNode.AddItem(item2); } )*
	;

seqKeyToValue [ ExecNode xg ] returns [ ExprPairNode res = null ]
	: key=seqExpression[xg] a=RARROW { xg.Append("->"); } value=seqExpression[xg]
		{
			res = new ExprPairNode(getCoords(a), key, value);
		}
	;

seqInitObjectExpr [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	options { k = 5; }
	: NEW { xg.Append("new "); } type=seqTypeIdentUse LPAREN RPAREN { xg.Append(type); xg.Append("()"); }
	| NEW { xg.Append("new "); } type=seqTypeIdentUse { xg.Append(type); }
		AT l=LPAREN { xg.Append("@("); } (seqAttributesInitializationList[xg])? RPAREN { xg.Append(")"); }
	;

seqAttributesInitializationList [ ExecNode xg ]
	: seqAttributeInitialization[xg] ( COMMA { xg.Append(","); } seqAttributeInitialization[xg] )*
	;

seqAttributeInitialization [ ExecNode xg ]
	: attr=seqMemberIdentUse { xg.Append(attr.Symbol.Text); } ASSIGN { xg.Append("="); } arg=seqExpression[xg]
	;

seqMultiRulePrefixedSequence [ ExecNode xg, CollectNode<BaseNode> returns ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: l=LBRACK LBRACK {xg.Append("[[");} 
		seqRulePrefixedSequenceAtom[xg, ruleCalls, returns]
		( COMMA { xg.Append(","); returns = new CollectNode<BaseNode>(); } seqRulePrefixedSequenceAtom[xg, ruleCalls, returns] )*
	  RBRACK { xg.Append("]"); } ( seqCallRuleOrMatchClassFilter[xg, filters, true] )* RBRACK { xg.Append("]"); }
		{ xg.AddMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters)); }
	;

seqRulePrefixedSequence [ ExecNode xg, CollectNode<BaseNode> returns ]
	: LBRACK { xg.Append("["); } seqRulePrefixedSequenceAtom[xg, null, returns] RBRACK { xg.Append("]"); }
	;

seqRulePrefixedSequenceAtom [ ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns ]
	: FOR l=LBRACE { env.PushScope("ruleprefixedsequence/exec", getCoords(l)); } { xg.Append("for{"); } 
		seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, false] SEMI { xg.Append(";"); }
			sequence[xg] { env.PopScope(); } RBRACE { xg.Append("}"); }
	;

seqMultiRuleAllCall [ ExecNode xg, CollectNode<BaseNode> returns, bool isAllBracketed ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: l=LBRACK LBRACK { xg.Append("[["); } 
		seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, isAllBracketed]
		( COMMA { xg.Append(","); returns = new CollectNode<BaseNode>(); }
				seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, isAllBracketed] )*
	  RBRACK { xg.Append("]"); } ( seqCallRuleOrMatchClassFilter[xg, filters, true] )* RBRACK { xg.Append("]"); }
		{ xg.AddMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters)); }
	;
	
seqParallelCallRule [ ExecNode xg, CollectNode<BaseNode> returns ]
	: ( LPAREN { xg.Append("("); } seqVariableList[xg, returns] RPAREN ASSIGN { xg.Append(")="); } )?
		(	( DOLLAR {xg.Append("$");} (MOD { xg.Append("\%"); })? ( seqVarUse[xg] 
						(COMMA { xg.Append(","); } (seqVarUse[xg] | STAR { xg.Append("*"); }))? )? )?
				LBRACK { xg.Append("["); } 
				seqCallRule[xg, null, returns, true]
				RBRACK { xg.Append("]"); }
		| 
			COUNT { xg.Append("count"); }
				LBRACK { xg.Append("["); } 
				seqCallRule[xg, null, returns, true]
				RBRACK { xg.Append("]"); }
		|
			seqCallRule[xg, null, returns, false]
		)
	;

seqRuleQuery [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: LBRACK { xg.Append("["); }
		cre=seqCallRuleExpression[xg] { res = cre; }
		RBRACK { xg.Append("]"); }
	;

seqMultiRuleQuery [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
		CollectNode<ExprNode> ruleCallExprs = new CollectNode<ExprNode>();
	}
	: l=LBRACK QUESTION LBRACK { xg.Append("[?["); } 
		cre=seqCallRuleExpressionForMulti[xg, ruleCalls] { ruleCallExprs.AddChild(cre); }
		( COMMA { xg.Append(","); } cre=seqCallRuleExpressionForMulti[xg, ruleCalls] { ruleCallExprs.AddChild(cre); } )*
	  RBRACK { xg.Append("]"); }
		( seqCallRuleOrMatchClassFilter[xg, filters, true] )*
		BACKSLASH { xg.Append("\\"); } LT CLASS matchClassIdent=seqTypeIdentUse GT
			{ xg.Append("<class " + matchClassIdent.ToString() + ">"); }
	  RBRACK { xg.Append("]"); }
		{
			MultiCallActionNode multiRuleCall = new MultiCallActionNode(getCoords(l), ruleCalls, filters);
			xg.AddMultiCallAction(multiRuleCall);
			res = new MultiRuleQueryExprNode(getCoords(l), ruleCallExprs, matchClassIdent, new ArrayTypeNode(matchClassIdent));
		}
	;

seqMappingClause [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: l=LBRACK COLON {xg.Append("[:");} 
		seqRulePrefixedSequenceAtom[xg, ruleCalls, returns]
		( COMMA { xg.Append(","); returns = new CollectNode<BaseNode>(); } seqRulePrefixedSequenceAtom[xg, ruleCalls, returns] )*
	  ( seqCallRuleOrMatchClassFilter[xg, filters, true] )* COLON RBRACK {xg.Append(":]");} 
		{ xg.AddMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters)); }
	;

seqCallRuleExpressionForMulti [ ExecNode xg, CollectNode<CallActionNode> ruleCalls ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		CollectNode<BaseNode> paramz = new CollectNode<BaseNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: id=seqActionOrEntIdentUse { xg.Append(id); }
		(LPAREN { xg.Append("("); } ( seqRuleParams[xg, paramz] )? RPAREN { xg.Append(")");})?
		( seqCallRuleOrMatchClassFilter[xg, filters, false] )*
		{
			CallActionNode ruleCall = new CallActionNode(id.Coords, id, paramz, returns, filters, false);
			xg.AddCallAction(ruleCall);
			if(ruleCalls != null) { // must be added to MultiCallActionNode if used from multi rule all call or multi backtrack construct
				ruleCalls.AddChild(ruleCall);
			}
			res = new RuleQueryExprNode(id.Coords, ruleCall, new ArrayTypeNode(MatchTypeActionNode.GetMatchTypeIdentNode(env, id)));
		}
	;

seqCallRuleWithOptionalReturns [ ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns, bool isAllBracketed ]
	: (LPAREN { xg.Append("("); } seqVariableList[xg, returns] RPAREN ASSIGN { xg.Append(")="); })? 
		seqCallRule[xg, ruleCalls, returns, isAllBracketed]
	;

seqCallRule [ ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns, bool isAllBracketed ]
	@init {
		CollectNode<BaseNode> paramz = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: ( | MOD { xg.Append("\%"); } | MOD QUESTION { xg.Append("\%?"); } | QUESTION { xg.Append("?"); } | QUESTION MOD { xg.Append("?\%"); } )
		(seqVarUse[xg] DOT { xg.Append("."); })?
		id=seqActionOrEntIdentUse { xg.Append(id); }
		(LPAREN {xg.Append("(");} (seqRuleParams[xg, paramz])? RPAREN { xg.Append(")"); })?
		( seqCallRuleOrMatchClassFilter[xg, filters, false] )*
		{
			CallActionNode ruleCall = new CallActionNode(id.Coords, id, paramz, returns, filters, isAllBracketed);
			xg.AddCallAction(ruleCall);
			if(ruleCalls != null) { // must be added to MultiCallActionNode if used from multi rule all call or multi backtrack construct
				ruleCalls.AddChild(ruleCall);
			}
		}
	;

seqCallRuleExpression [ ExecNode xg ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		CollectNode<BaseNode> paramz = new CollectNode<BaseNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: ( QUESTION MOD { xg.Append("?\%"); } | MOD QUESTION { xg.Append("\%?"); } | QUESTION { xg.Append("?"); } )
		(seqVarUse[xg] DOT { xg.Append("."); })?
		id=seqActionOrEntIdentUse { xg.Append(id); }
		(LPAREN {xg.Append("(");} (seqRuleParams[xg, paramz])? RPAREN { xg.Append(")"); })?
		( seqCallRuleOrMatchClassFilter[xg, filters, false] )*
		{
			CallActionNode ruleCall = new CallActionNode(id.Coords, id, paramz, returns, filters, true);
			xg.AddCallAction(ruleCall);
			res = new RuleQueryExprNode(id.Coords, ruleCall, new ArrayTypeNode(MatchTypeActionNode.GetMatchTypeIdentNode(env, id)));
		}
	;

seqCallRuleOrMatchClassFilter [ ExecNode xg, CollectNode<BaseNode> filters, bool isMatchClassFilter ]
	: BACKSLASH { xg.Append("\\"); } (p=IDENT DOUBLECOLON { xg.Append(p.Text); xg.Append("::"); })? (id=IDENT | id=AUTO) { xg.Append(id.Text); }
		(
			DOT { xg.Append("."); } seqCallMatchClassFilterContinuation[xg, filters, isMatchClassFilter, p, id]
		|
			seqCallRuleFilterContinuation[xg, filters, isMatchClassFilter, p, id]
		)
	;

seqCallRuleFilterContinuation [ ExecNode xg, CollectNode<BaseNode> filters, bool isMatchClassFilter, IToken pin, IToken idin ]
	@init {
		String filterBaseText = null;
	}
	: { filterBaseText = idin.Text; } LT { xg.Append("<"); } seqFilterCallVariableList[xg] GT { xg.Append(">"); }
		seqCallRuleFilterContinuationMember[xg, filters, isMatchClassFilter, pin, idin, filterBaseText]
	| seqCallRuleFilterContinuationNonMember[xg, filters, isMatchClassFilter, pin, idin]
	;

seqCallRuleFilterContinuationMember [ ExecNode xg, CollectNode<BaseNode> filters, bool isMatchClassFilter, IToken pin, IToken idin, String filterBaseText ]
	: (filterBaseTextExt=seqFilterExtension[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		(LPAREN { xg.Append("("); } RPAREN { xg.Append(")"); })?
		{
			IToken p = pin;
			IToken filterBase = idin;

			if(p != null)
				reportError(getCoords(filterBase), "A package specifier is not allowed for auto-generated filters.");
			if(isMatchClassFilter)
				reportError(getCoords(filterBase), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(!ParserEnvironment.IsAutoGeneratedBaseFilterName(filterBaseText))
			{
				reportError(getCoords(filterBase), "The def-variable-based filter " + filterBaseText + " is not known. Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy.");
			}
		}
	|
		(filterBaseTextExt=seqInitExpression[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		l=LBRACE { xg.Append("{"); } { env.PushScope("filterassign/exec", getCoords(l)); } 
				seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
				{ env.PopScope(); } RBRACE { xg.Append("}"); }
			(LPAREN { xg.Append("("); } RPAREN { xg.Append(")"); })?
		{
			IToken p = pin;
			IToken filterId = idin;

			if(p != null)
				reportError(getCoords(filterId), "A package specifier is not allowed for per-element with lambda-expression filters.");
			if(isMatchClassFilter)
				reportError(getCoords(filterId), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(!filterBaseText.Equals("assign") 
				&& !filterBaseText.Equals("assignStartWithAccumulateBy"))
			{
				reportError(getCoords(filterId), "The per-element with lambda-expression filter " + filterBaseText + " is not known. Available are: assign and assignStartWithAccumulateBy.");
			}
		}
	;

seqCallRuleFilterContinuationNonMember [ ExecNode xg, CollectNode<BaseNode> filters, bool isMatchClassFilter, IToken pin, IToken idin ]
	@init {
		CollectNode<BaseNode> paramz = new CollectNode<BaseNode>();
	}
	: (LPAREN { xg.Append("("); } (seqRuleParams[xg, paramz])? RPAREN { xg.Append(")"); })?
		{
			IToken p = pin;
			IToken filterId = idin;

			if(isMatchClassFilter)
				reportError(getCoords(filterId), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(ParserEnvironment.IsAutoSuppliedFilterName(filterId.Text))
			{
				if(paramz.Size() != 1)
					reportError(getCoords(filterId), "The filter " + filterId.Text + " expects 1 argument (given are " + paramz.Size() + " arguments).");
			}
			else if(filterId.Text.Equals("auto"))
			{
				if(isMatchClassFilter)
					reportError(getCoords(filterId), "The auto filter is not available for multi rule call or multi rule backtracking constructs.");
				if(paramz.Size() != 0)
					reportError(getCoords(filterId), "The filter " + filterId.Text + " expects 0 arguments (given are " + paramz.Size() + " arguments).");
			}
			else
			{
				IdentNode filter = p != null ? new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
														env.Occurs(ParserEnvironment.ACTIONS, filterId.Text, getCoords(filterId)))
												: new IdentNode(env.Occurs(ParserEnvironment.ACTIONS, filterId.Text, getCoords(filterId)));
				filters.AddChild(filter);
			}
		}
	| l=LBRACE { xg.Append("{"); } { env.PushScope("filterremoveIf/exec", getCoords(l)); }
		seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
		{ env.PopScope(); } RBRACE { xg.Append("}"); }
		(LPAREN { xg.Append("("); } RPAREN { xg.Append(")"); })?
		{
			IToken p = pin;
			IToken filterId = idin;

			if(p != null)
				reportError(getCoords(filterId), "A package specifier is not allowed for per-element with lambda-expression filters.");
			if(isMatchClassFilter)
				reportError(getCoords(filterId), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(!filterId.Text.Equals("removeIf"))
			{
				reportError(getCoords(filterId), "The per-element with lambda-expression filter " + filterId.Text + " is not known. Available is: removeIf.");
			}
		}
	;

seqCallMatchClassFilterContinuation [ ExecNode xg, CollectNode<BaseNode> filters, bool isMatchClassFilter, IToken pmc, IToken mc ]
	@init {
		String filterBaseText = null;
	}
	: filterBase=IDENT { xg.Append(filterBase.Text); filterBaseText = filterBase.Text; } LT { xg.Append("<"); } seqFilterCallVariableList[xg] GT { xg.Append(">"); }
		seqCallMatchClassFilterContinuationMember[xg, filters, isMatchClassFilter, pmc, mc, filterBase, filterBaseText]
	| (p=IDENT DOUBLECOLON { xg.Append(p.Text); xg.Append("::"); })? filterId=IDENT { xg.Append(filterId.Text); }
		seqCallMatchClassFilterContinuationNonMember[xg, filters, isMatchClassFilter, pmc, mc, p, filterId]
	;

seqCallMatchClassFilterContinuationMember [ ExecNode xg, CollectNode<BaseNode> filters, bool isMatchClassFilter, IToken pmc, IToken mc, IToken filterBase, String filterBaseText ]
	: (filterBaseTextExt=seqFilterExtension[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		(LPAREN { xg.Append("("); } RPAREN { xg.Append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(!ParserEnvironment.IsAutoGeneratedBaseFilterName(filterBaseText))
			{
				reportError(getCoords(filterBase), "The def-variable-based filter " + filterBaseText + " is not known. Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy.");
			}
		}
	| (filterBaseTextExt=seqInitExpression[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		l=LBRACE { xg.Append("{"); } { env.PushScope("filterassign/exec", getCoords(l)); }
		seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
		{ env.PopScope(); } RBRACE { xg.Append("}"); }
		(LPAREN { xg.Append("("); } RPAREN { xg.Append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(!filterBaseText.Equals("assign")
				&& !filterBaseText.Equals("assignStartWithAccumulateBy"))
			{
				reportError(getCoords(filterBase), "The per-element with lambda-expression filter " + filterBaseText + " is not known. Available are: assign and assignStartWithAccumulateBy.");
			}
		}
	;

seqCallMatchClassFilterContinuationNonMember [ ExecNode xg, CollectNode<BaseNode> filters, bool isMatchClassFilter, IToken pmc, IToken mc, IToken p, IToken filterId ]
	@init {
		CollectNode<BaseNode> paramz = new CollectNode<BaseNode>();
	}
	: (LPAREN { xg.Append("("); } (seqRuleParams[xg, paramz])? RPAREN { xg.Append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(ParserEnvironment.IsAutoSuppliedFilterName(filterId.Text))
			{
				if(paramz.Size() != 1)
					reportError(getCoords(filterId), "The filter " + filterId.Text + " expects 1 argument (given are " + paramz.Size() + " arguments).");
			}
			else
			{
				IdentNode matchClass = pmc != null ? new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, pmc.Text, getCoords(pmc)), 
														env.Occurs(ParserEnvironment.TYPES, mc.Text, getCoords(mc)))
													: new IdentNode(env.Occurs(ParserEnvironment.TYPES, mc.Text, getCoords(mc)));
				IdentNode matchClassFilter = p != null ? new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
														env.Occurs(ParserEnvironment.ACTIONS, filterId.Text, getCoords(filterId)))
													: new IdentNode(env.Occurs(ParserEnvironment.ACTIONS, filterId.Text, getCoords(filterId)));
				filters.AddChild(new MatchTypeQualIdentNode(getCoords(filterId), matchClass, matchClassFilter));
			}
		}
	| l=LBRACE { xg.Append("{"); } { env.PushScope("filterassign/exec", getCoords(l)); }
		seqLambdaExprVarDeclPrefix[xg] seqExpression[xg] 
		{ env.PopScope(); } RBRACE { xg.Append("}"); }
		(LPAREN { xg.Append("("); } RPAREN { xg.Append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");
			if(p != null)
				reportError(getCoords(filterId), "A package specifier is not allowed for per-element with lambda-expression filters.");
		
			if(!filterId.Text.Equals("removeIf"))
			{
				reportError(getCoords(filterId), "The per-element with lambda-expression filter " + filterId.Text + " is not known. Available is: removeIf.");
			}
		}
	;

seqFilterExtension [ ExecNode xg, String filterBaseText ] returns [ String res = null ]
	: filterBaseExtension=IDENT { xg.Append(filterBaseExtension.Text); } LT { xg.Append("<"); } seqFilterCallVariableList[xg] GT { xg.Append(">"); } 
		filterBaseExtension2=IDENT { xg.Append(filterBaseExtension2.Text); } LT { xg.Append("<"); } seqFilterCallVariableList[xg] GT { xg.Append(">"); }
		{
			res = filterBaseText + filterBaseExtension.Text + filterBaseExtension2.Text;
		}
	;

seqInitExpression [ ExecNode xg, String filterBaseText ] returns [ String res = null ]
	: filterBaseExtension=IDENT { xg.Append(filterBaseExtension.Text); } 
		l=LBRACE { xg.Append("{"); } { env.PushScope("filterassign/initexpr", getCoords(l)); } 
		seqInitExprVarDeclPrefix[xg]
		{ env.PopScope(); } RBRACE { xg.Append("}"); }
		filterBaseExtension2=IDENT { xg.Append(filterBaseExtension2.Text); }
		{
			res = filterBaseText + filterBaseExtension.Text + filterBaseExtension2.Text;
		}
	;

seqInitExprVarDeclPrefix [ ExecNode xg ]
	options { k = *; }
	: seqEntityDecl[xg] SEMI { xg.Append(";"); } seqExpression[xg]
	| seqExpression[xg]
	;

seqFilterCallVariableList [ ExecNode xg ]
	: filterVariable=IDENT { xg.Append(filterVariable.Text); }
		( COMMA { xg.Append(","); } filterVariable=IDENT { xg.Append(filterVariable.Text); } )*
	;

seqRuleParam [ ExecNode xg, CollectNode<BaseNode> parameters ]
	: exp=seqExpression[xg] { parameters.AddChild(exp); if(exp == null) throw new Exception(); }
	;

seqRuleParams [ ExecNode xg, CollectNode<BaseNode> parameters ]
	: seqRuleParam[xg, parameters] ( COMMA { xg.Append(","); } seqRuleParam[xg, parameters] )*
	;

seqVariableList [ ExecNode xg, CollectNode<BaseNode> res ]
	: child=seqEntity[xg] { res.AddChild(child); }
		( COMMA { xg.Append(","); } child=seqEntity[xg] { res.AddChild(child); } )*
	;

// read context (assignment rhs)
seqVarUse [ ExecNode xg ] returns [ IdentNode res = null ]
	:
		id=seqEntIdentUse { res = id; xg.Append(id); xg.AddUsage(id); } // var of node, edge, or basic type
	|
		DOUBLECOLON id=seqEntIdentUse { res = id; xg.Append("::" + id); xg.AddUsage(id); } // global var of node, edge, or basic type
	;

// write context (assignment lhs)
seqEntity [ ExecNode xg ] returns [ BaseNode res = null ]
	:
		id=seqEntIdentUse { res = id; xg.Append(id); xg.AddWriteUsage(id); } // var of node, edge, or basic type
	|
		DOUBLECOLON id=seqEntIdentUse { res = id; xg.Append("::" + id); xg.AddWriteUsage(id); } // global var of node, edge, or basic type
	|
		seqVarDecl=seqEntityDecl[xg] { res = seqVarDecl; }
	;

seqEntityDecl [ ExecNode xg ] returns [ ExecVarDeclNode res = null ]
	:
		id=seqEntIdentDecl COLON cont=seqEntityDeclCont[xg, id] { res = cont; } // node/var decl or container/match type decl 
	|
		MINUS id=seqEntIdentDecl COLON type=seqTypeIdentUse RARROW // edge decl, interpreted grs don't use -:-> form
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, type);
			xg.Append(decl.Ident + ":" + decl.typeUnresolved);
			xg.AddVarDecl(decl);
			res = decl;
		}
	;

seqEntityDeclCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // node/var decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, type);
			xg.Append(id.ToString() + ":" + type.ToString());
			xg.AddVarDecl(decl);
			res = decl;
		}
	|
		cont=seqEntityDeclGenericTypeCont[xg, id] { res = cont; } // container/match type decl
	;

seqEntityDeclGenericTypeCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		{ input.LT(1).Text.Equals("map") }?
		IDENT LT keyType=seqTypeIdentUse COMMA { xg.Append(id.ToString() + ":map<" + keyType.ToString() + ","); }
		cont=seqEntityDeclGenericTypeMapCont[xg, id, keyType] { res = cont; }
	|
		{ input.LT(1).Text.Equals("set") }?
		IDENT LT cont=seqEntityDeclGenericTypeSetCont[xg, id] { res = cont; }
	|
		{ input.LT(1).Text.Equals("array") }?
		IDENT LT cont=seqEntityDeclGenericTypeArrayCont[xg, id] { res = cont; }
	|
		{ input.LT(1).Text.Equals("deque") }?
		IDENT LT cont=seqEntityDeclGenericTypeDequeCont[xg, id] { res = cont; }
	|
		MATCH LT cont=seqEntityDeclGenericTypeMatchCont[xg, id] { res = cont; }
	;

seqEntityDeclGenericTypeMapCont [ ExecNode xg, IdentNode id, IdentNode keyType ] returns [ ExecVarDeclNode res = null ]
	:
		valueType=seqTypeIdentUse // map decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new MapTypeNode(keyType, valueType));
			xg.Append(valueType.ToString());
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		valueType=seqMatchTypeIdentUseInContainerType[xg] // map to match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new MapTypeNode(keyType, valueType));
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeSetCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // set decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new SetTypeNode(type));
			xg.Append(id.ToString() + ":set<" + type.ToString());
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		{ xg.Append(id.ToString() + ":set<"); } type=seqMatchTypeIdentUseInContainerType[xg] // set of match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new SetTypeNode(type));
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeArrayCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // array decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			xg.Append(id.ToString() + ":array<" + type.ToString());
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		{ xg.Append(id.ToString() + ":array<"); } type=seqMatchTypeIdentUseInContainerType[xg] // array of match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeDequeCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // deque decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new DequeTypeNode(type));
			xg.Append(id.ToString() + ":deque<" + type.ToString());
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		{ xg.Append(id.ToString() + ":deque<"); } type=seqMatchTypeIdentUseInContainerType[xg] // deque of match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new DequeTypeNode(type));
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeMatchCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		actionIdent=seqActionIdentUse // match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, MatchTypeActionNode.GetMatchTypeIdentNode(env, actionIdent));
			xg.Append(id.ToString() + ":match<" + actionIdent.ToString());
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		CLASS matchClassIdent=seqTypeIdentUse // match class decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, matchClassIdent);
			xg.Append(id.ToString() + ":match<class " + matchClassIdent.ToString());
			xg.AddVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	;

seqMatchTypeIdentUseInContainerType [ ExecNode xg ] returns [ IdentNode res = null ]
	options { k = 3; }
	:
		MATCH LT actionIdent=seqActionIdentUse // match decl
		{
			res = MatchTypeActionNode.GetMatchTypeIdentNode(env, actionIdent);
			xg.Append("match<" + actionIdent.ToString());
		}
	|
		MATCH LT CLASS matchClassIdent=seqTypeIdentUse // match class decl
		{
			res = matchClassIdent;
			xg.Append("match<class " + matchClassIdent.ToString());
		}
	;

// special to save user from splitting e.g. map<S,T>=x to map<S,T> =x as >= is GE not GT ASSIGN
genericTypeEnd [ ExecNode xg ]
	: GT { xg.Append(">"); }
	| (GE) => { }
	;

// special to save user from splitting e.g. array< match<T> >=x to array< match<T> > =x as >= is GE not GT ASSIGN
// note that array<match<T>>=x is SR ASSIGN, not GT GT ASSIGN as in array< match<T> > =x
genericTypeEndPastMatchType [ ExecNode xg ]
	: GT GT { xg.Append("> >"); } 
	| SR { xg.Append(">>"); }
	| (GT GE) => GT { xg.Append(">"); }
	;

seqIndex [ ExecNode xg ]
	: id=seqIndexIdentUse { xg.Append(id.ToString()); }
	;

seqEntIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
	;

seqTypeIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	options { k = 3; }
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
				env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i))); }
	;

seqEntIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
	;

seqActionIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	options { k = 3; }
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.ACTIONS, i.Text, getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
				env.Occurs(ParserEnvironment.ACTIONS, i.Text, getCoords(i))); }
	;

seqActionOrEntIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	options { k = 3; }
	: i=IDENT
		{ if(i != null) res = new AmbiguousIdentNode(env.Occurs(ParserEnvironment.ACTIONS,
			i.Text, getCoords(i)), env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
				env.Occurs(ParserEnvironment.ACTIONS, i.Text, getCoords(i))); }
	;

seqIndexIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.INDICES, i.Text, getCoords(i))); }
	;

seqMemberIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
	| r=REPLACE // HACK: For string replace function... better choose another name?
		{ if(r != null) {
			r.Type = IDENT;
			res = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, r.Text, getCoords(r)));
		  }
		}
	;

seqRangeSpecLoop [ ExecNode xg ]
	: 	// range allows [*], [+], [c:*], [c], [c:d]; no range equals 1:1
		(
			LBRACK { xg.Append("["); }
			(
				STAR { xg.Append("*"); }
			|
				PLUS { xg.Append("+"); }
			|
				seqExpression[xg]
				(
					COLON { xg.Append(" : "); } ( STAR { xg.Append("*"); } | seqExpression[xg] )
				)?
			)
			RBRACK { xg.Append("]"); }
		)?
	;
