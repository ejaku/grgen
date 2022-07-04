/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */
 
/*
 * GrGen sequence in rule specification language grammar for ANTLR 3
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski, Veit Batz, Edgar Jakumeit, Sebastian Buchwald, Moritz Kroll
*/

parser grammar EmbeddedExec;

@members {
	private OperatorNode makeOp(org.antlr.runtime.Token t) {
		return gParent.makeOp(t);
	}

	private OperatorNode makeTernOp(org.antlr.runtime.Token t, ExprNode op0, ExprNode op1, ExprNode op2) {
		return gParent.makeTernOp(t, op0, op1, op2);
	}

	private OperatorNode makeBinOp(org.antlr.runtime.Token t, ExprNode op0, ExprNode op1) {
		return gParent.makeBinOp(t, op0, op1);
	}

	private OperatorNode makeUnOp(org.antlr.runtime.Token t, ExprNode op) {
		return gParent.makeUnOp(t, op);
	}

	protected ParserEnvironment env;

	public void setEnv(ParserEnvironment env) {
		this.env = env;
	}

	protected Coords getCoords(org.antlr.runtime.Token tok) {
		return new Coords(tok);
	}

	protected final void reportError(de.unika.ipd.grgen.parser.Coords c, String s) {
		gParent.hadError = true;
		env.getSystem().getErrorReporter().error(c, s);
	}

	public void displayRecognitionError(String[] tokenNames, RecognitionException e) {
		String hdr = getErrorHeader(e);
		String msg = getErrorMessage(e, tokenNames);
		reportError(new Coords(e), msg);
	}

	public void reportWarning(de.unika.ipd.grgen.parser.Coords c, String s) {
		env.getSystem().getErrorReporter().warning(c, s);
	}

	public String getFilename() {
		return env.getFilename();
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

sequenceParamList [ CollectNode<ExecVarDeclNode> params, ExecNode xg ]
	: { xg.disableXgrsStringBuilding(); } p=seqEntityDecl[xg] { params.addChild(p); xg.enableXgrsStringBuilding(); }
		( { xg.disableXgrsStringBuilding(); } COMMA p=seqEntityDecl[xg] { params.addChild(p); xg.enableXgrsStringBuilding(); } )*
	;

sequence [ ExecNode xg ]
	: seqLazyOr[xg]
		( (DOLLAR THENLEFT { xg.append(" $<; "); } | THENLEFT { xg.append(" <; "); } 
			| DOLLAR THENRIGHT { xg.append(" $;> "); } | THENRIGHT { xg.append(" ;> "); }) sequence[xg]
		)?
	;

seqLazyOr [ ExecNode xg ]
	: seqLazyAnd[xg]
		( (DOLLAR LOR { xg.append(" $|| "); } | LOR { xg.append(" || "); }) seqLazyOr[xg]
		)?
	;

seqLazyAnd [ ExecNode xg ]
	: seqStrictOr[xg]
		( (DOLLAR LAND { xg.append(" $&& "); } | LAND { xg.append(" && "); }) seqLazyAnd[xg]
		)?
	;

seqStrictOr [ ExecNode xg ]
	: seqStrictXor[xg]
		( (DOLLAR BOR { xg.append(" $| "); } | BOR { xg.append(" | "); }) seqStrictOr[xg]
		)?
	;

seqStrictXor [ ExecNode xg ]
	: seqStrictAnd[xg]
		( (DOLLAR BXOR { xg.append(" $^ "); } | BXOR { xg.append(" ^ "); }) seqStrictXor[xg]
		)?
	;

seqStrictAnd [ ExecNode xg ]
	: seqNegOrIteration[xg]
		( (DOLLAR BAND { xg.append(" $& "); } | BAND { xg.append(" & "); }) seqStrictAnd[xg]
		)?
	;

seqNegOrIteration [ ExecNode xg ]
	: NOT { xg.append("!"); } seqIterSequence[xg] 
		( (ASSIGN_TO { xg.append("=>"); } | BOR_TO { xg.append("|>"); } | BAND_TO { xg.append("&>"); }) seqEntity[xg]
		)?
	| seqIterSequence[xg]
		( (ASSIGN_TO { xg.append("=>"); } | BOR_TO { xg.append("|>"); } | BAND_TO { xg.append("&>"); }) seqEntity[xg]
		)?
	;

seqIterSequence [ ExecNode xg ]
	: seqSimpleSequence[xg]
		(
			rsn=seqRangeSpecLoop[xg]
		|
			STAR { xg.append("*"); }
		|
			PLUS { xg.append("+"); }
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
	: (seqEntity[null] (ASSIGN | GE )) => lhs=seqEntity[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); })
		(
			id=seqEntIdentUse LPAREN // deliver understandable error message for case of missing parenthesis at rule result assignment
				{ reportError(id.getCoords(), "The destination variable(s) of a rule result assignment must be enclosed in parenthesis."); }
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
				n=NUM_INTEGER RPAREN { xg.append("$("); xg.append(n.getText()); xg.append(")"); }
				| f=NUM_DOUBLE RPAREN { xg.append("$("); xg.append(f.getText()); xg.append(")"); }
			)
		|
			LPAREN { xg.append('('); } sequence[xg] RPAREN { xg.append(')'); }
		)
	| seqVarDecl=seqEntityDecl[xg]
	| YIELD { xg.append("yield "); } lhsent=seqEntIdentUse { xg.append(lhsent); xg.addUsage(lhsent); } ASSIGN { xg.append('='); } 
		( (seqConstant[null]) => seqConstant[xg]
		| seqVarUse[xg]
		)
	| TRUE { xg.append("true"); }
	| FALSE { xg.append("false"); }
	| seqRulePrefixedSequence[xg, returns]
	| seqMultiRulePrefixedSequence[xg, returns]
	| (seqParallelCallRule[null, null]) => seqParallelCallRule[xg, returns]
	| seqMultiRuleAllCall[xg, returns, true]
	| DOUBLECOLON id=seqEntIdentUse { xg.append("::" + id); xg.addUsage(id); }
	| (( DOLLAR ( MOD )? )? LBRACE LT) => ( DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? )?
		LBRACE LT { xg.append("{<"); } seqParallelCallRule[xg, returns]
			( COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); } seqParallelCallRule[xg, returns] )*
			GT RBRACE { xg.append(">}"); }
	| DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? 
		(LOR { xg.append("||"); } | LAND { xg.append("&&"); } | BOR { xg.append("|"); } | BAND { xg.append("&"); }) 
		LPAREN { xg.append("("); } sequence[xg] ( COMMA { xg.append(","); } sequence[xg] )* RPAREN { xg.append(")"); }
	| DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? DOT { xg.append("."); } 
		LPAREN { xg.append("("); } f=NUM_DOUBLE { xg.append(f.getText() + " "); } sequence[xg] 
			( COMMA { xg.append(","); } f=NUM_DOUBLE { xg.append(f.getText() + " "); } sequence[xg] )* RPAREN { xg.append(")"); }
	| LPAREN { xg.append("("); } sequence[xg] RPAREN { xg.append(")"); }
	| LT { xg.append(" <"); } sequence[xg] GT { xg.append("> "); }
	| l=SL { env.pushScope("backtrack/exec", getCoords(l)); } { xg.append(" <<"); } 
		seqParallelCallRule[xg, returns] (DOUBLE_SEMI|SEMI) { xg.append(";;"); }
		sequence[xg] { env.popScope(); } SR { xg.append(" >> "); }
	| l=SL { env.pushScope("backtrack/exec", getCoords(l)); } { xg.append(" <<"); } 
		seqMultiRuleAllCall[xg, returns, false] (DOUBLE_SEMI|SEMI) { xg.append(";;"); }
		sequence[xg] { env.popScope(); } SR { xg.append(" >> "); }
	| SL { xg.append(" <<"); } seqMultiRulePrefixedSequence[xg, returns] SR { xg.append(" >> "); }
	| DIV { xg.append(" /"); } sequence[xg] DIV { xg.append("/ "); }
	| IF l=LBRACE { env.pushScope("if/exec", getCoords(l)); } { xg.append("if{"); } sequence[xg] s=SEMI 
		{ env.pushScope("if/then-part", getCoords(s)); } { xg.append("; "); }
		sequence[xg] { env.popScope(); } (SEMI { xg.append("; "); } sequence[xg])? { env.popScope(); } RBRACE { xg.append("}"); }
	| FOR l=LBRACE { env.pushScope("for/exec", getCoords(l)); } { xg.append("for{"); } seqEntity[xg] seqForSeqRemainder[xg, returns]
	| i=IN { xg.append("in "); } { env.pushScope("in subgraph sequence", getCoords(i)); } seqExpression[xg]
		LBRACE { xg.append("{"); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); } 
	| LOCK l=LPAREN { xg.append("lock("); } { env.pushScope("lock sequence", getCoords(l)); } seqExpression[xg] RPAREN  { xg.append(")"); }
		LBRACE { xg.append("{"); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); } 
	| LBRACE { xg.append("{"); } { env.pushScope("sequence computation", getCoords(l)); }
		seqCompoundComputation[xg] (SEMI)? { env.popScope(); } RBRACE { xg.append("}"); } 
	;

seqForSeqRemainder [ ExecNode xg, CollectNode<BaseNode> returns ]
	options { k = 4; }
	: (RARROW { xg.append(" -> "); } seqEntity[xg])? IN { xg.append(" in "); } seqVarUse[xg]
			SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN { xg.append(" in "); } { env.isKnownForFunction(input.LT(1).getText()) }?
			i=IDENT LPAREN { xg.append(i.getText()); xg.append("("); }
			(expr1=seqExpression[xg] (COMMA { xg.append(","); } expr2=seqExpression[xg] 
				(COMMA { xg.append(","); } expr3=seqExpression[xg] (COMMA { xg.append(","); } expr4=seqExpression[xg])? )? 
					)? )?
			RPAREN { xg.append(")"); }
			SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN { xg.append(" in "); } LBRACE { xg.append("{"); } seqIndex[xg] EQUAL { xg.append(" == "); } seqExpression[xg] 
		RBRACE { xg.append("}"); } SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN { xg.append(" in "); } LBRACE { xg.append("{"); } 
		i=IDENT { xg.append(i.getText()); } 
		LPAREN { xg.append("("); } seqIndex[xg] ( seqRelOs[xg] seqExpression[xg]
				( COMMA { xg.append(","); } seqIndex[xg] seqRelOs[xg] seqExpression[xg] )? )? 
		RPAREN { xg.append(")"); } RBRACE { xg.append("}"); } SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN LBRACK QUESTION { xg.append(" in [?"); } seqCallRule[xg, null, returns, true] RBRACK { xg.append("]"); }
			SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN LBRACK { xg.append(" in ["); } left=seqExpression[xg] COLON { xg.append(" : "); } right=seqExpression[xg] RBRACK { xg.append("]"); }
			SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	;

seqCompoundComputation [ ExecNode xg ]
	: seqComputation[xg] (SEMI { xg.append(";"); } seqCompoundComputation[xg])?
	;

seqComputation [ ExecNode xg ]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); })
		seqExpressionOrAssign[xg]
	| seqEntityDecl[xg]
	| seqProcedureOrMethodCall[xg]
	| LBRACE { xg.append("{"); } seqExpression[xg] RBRACE { xg.append("}"); }
	;

seqExpressionOrAssign [ ExecNode xg ]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); })
		seqExpressionOrAssign[xg]
	| seqExpression[xg] 
	;

seqAssignTarget [ ExecNode xg ]
	: YIELD { xg.append("yield "); } seqVarUse[xg] 
	| seqVarUse[xg] seqAssignTargetSelector[xg]
	| seqEntityDecl[xg]
	;

seqAssignTargetSelector [ ExecNode xg ]
	: DOT attr=IDENT { xg.append("."+attr.getText()); } 
		(LBRACK { xg.append("["); } seqExpression[xg] RBRACK { xg.append("]"); })?
	| DOT VISITED { xg.append(".visited"); } (LBRACK { xg.append("["); } seqExpression[xg] RBRACK { xg.append("]"); })?
	| LBRACK { xg.append("["); } seqExpression[xg] RBRACK { xg.append("]"); }
	|
	;

// todo: add expression value returns to remaining sequence expressions,
// as of now only some sequence expressions return an expression
// the expressions are needed for the argument expressions of rule/sequence calls,
// in all other places of the sequences we only need a textual emit of the constructs just parsed
seqExpression [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrCond=seqExprLazyOr[xg] { res = expOrCond; }
		( q=QUESTION { xg.append("?"); } trueCase=seqExpression[xg] COLON { xg.append(" : "); } falseCase=seqExpression[xg]
			{ res = makeTernOp(q, expOrCond, trueCase, falseCase); }
		)?
	;

seqExprLazyOr [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprLazyAnd[xg] { res = expOrLeft; } 
		( op=LOR { xg.append(" || "); } right=seqExprLazyOr[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprLazyAnd [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprStrictOr[xg] { res = expOrLeft; }
		( op=LAND { xg.append(" && "); } right=seqExprLazyAnd[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprStrictOr [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprStrictXor[xg] { res = expOrLeft; }
		( op=BOR { xg.append(" | "); } right=seqExprStrictOr[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprStrictXor [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprStrictAnd[xg] { res = expOrLeft; }
		( op=BXOR { xg.append(" ^ "); } right=seqExprStrictXor[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprStrictAnd [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprExcept[xg] { res = expOrLeft; }
		( op=BAND { xg.append(" & "); } right=seqExprStrictAnd[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprExcept [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprEquality[xg] { res = expOrLeft; }
		( op=BACKSLASH { xg.append(" \\ "); } right=seqExprExcept[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;
	
seqEqOp [ ExecNode xg ] returns [ Token t = null ]
	: e=EQUAL { xg.append(" == "); t = e; }
	| n=NOT_EQUAL { xg.append(" != "); t = n; }
	| s=STRUCTURAL_EQUAL { xg.append(" ~~ "); t = s; }
	;

seqExprEquality [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprRelation[xg] { res = expOrLeft; }
		( op=seqEqOp[xg] right=seqExprEquality[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqRelOp [ ExecNode xg ] returns [ Token t = null ]
	: lt=LT { xg.append(" < "); t = lt; }
	| le=LE { xg.append(" <= "); t = le; }
	| gt=GT { xg.append(" > "); t = gt; }
	| ge=GE { xg.append(" >= "); t = ge; }
	| in=IN { xg.append(" in "); t = in; }
	;

seqRelOs [ ExecNode xg ] returns [ Token t = null ]
	: lt=LT { xg.append(" < "); t = lt; }
	| le=LE { xg.append(" <= "); t = le; }
	| gt=GT { xg.append(" > "); t = gt; }
	| ge=GE { xg.append(" >= "); t = ge; }
	;

seqExprRelation [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprShift[xg] { res = expOrLeft; } 
		( op=seqRelOp[xg] right=seqExprRelation[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqShiftOp [ ExecNode xg ] returns [ Token t = null ]
	: sl=SL { xg.append(" << "); t = sl; }
	| sr=SR { xg.append(" >> "); t = sr; }
	| bsr=BSR { xg.append(" >>> "); t = bsr; }
	;

seqExprShift [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprAdd[xg] { res = expOrLeft; } 
		( op=seqShiftOp[xg] right=seqExprShift[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqAddOp [ ExecNode xg ] returns [ Token t = null ]
	: p=PLUS { xg.append(" + "); t = p; }
	| m=MINUS { xg.append(" - "); t = m; }
	;

seqExprAdd [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprMul[xg] { res = expOrLeft; } 
		( op=seqAddOp[xg] right=seqExprAdd[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqMulOp [ ExecNode xg ] returns [ Token t = null ]
	: s=STAR { xg.append(" * "); t = s; }
	| m=MOD { xg.append(" \% "); t = m; }
	| d=DIV { xg.append(" / "); t = d; }
	;

seqExprMul [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: expOrLeft=seqExprUnary[xg] { res = expOrLeft; } 
		( op=seqMulOp[xg] right=seqExprMul[xg]
			{ res = makeBinOp(op, res, right); }
		)?
	;

seqExprUnary [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		Token t = null;
	}
	: (LPAREN seqTypeIdentUse RPAREN) =>
		p=LPAREN { xg.append("("); } id=seqTypeIdentUse {xg.append(id);} RPAREN { xg.append(")"); } op=seqExprBasic[xg]
		{ res = new CastNode(getCoords(p), id, op); }
	| (n=NOT { t = n; xg.append("!"); })? exp=seqExprBasic[xg]
		{
			if(t != null)
				res = makeUnOp(t, exp);
			else
				res = exp;
		}
	| m=MINUS { xg.append("-"); } exp=seqExprBasic[xg]
		{
			OperatorNode neg = new ArithmeticOperatorNode(getCoords(m), OperatorDeclNode.Operator.NEG);
			neg.addChild(exp);
			res = neg;
		}
	| p=PLUS { xg.append("+"); } exp=seqExprBasic[xg]
		{
			res = exp;
		}
	| ti=TILDE { xg.append("~"); } exp=seqExprBasic[xg]
		{
			res = exp;
		}
	;

// todo: the seqVarUse[xg] casted to IdenNodes might be not simple variable identifiers, but global variables with :: prefix,
//  probably a distinction is needed
seqExprBasic [ExecNode xg] returns [ ExprNode res = env.initExprNode() ]
	options { k = 4; }
	@init {
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		IdentNode id;
	}
	: owner=seqVarUseInExpr[xg] sel=seqExprSelector[owner, xg] { res = sel; }
	| {input.LT(1).getText().equals("this")}? i=IDENT { xg.append("this"); } sel=seqExprSelector[new ThisExprNode(getCoords(i)), xg] { res = sel; }
	| fc=seqFunctionCall[xg] { res = fc; } sel=seqExprSelector[fc, xg] { res = sel; }
	| fc=seqScanFunctionCall[xg] { res = fc; } sel=seqExprSelector[fc, xg] { res = sel; }
	| DEF LPAREN { xg.append("def("); } seqVariableList[xg, returns] RPAREN { xg.append(")"); } 
	| a=AT LPAREN { xg.append("@("); } 
		(i=IDENT { xg.append(i.getText()); } | s=STRING_LITERAL { xg.append(s.getText()); }) RPAREN { xg.append(")"); }
	| rq=seqRuleQuery[xg] sel=seqExprSelector[rq, xg] { res = sel; }
	| mrq=seqMultiRuleQuery[xg] sel=seqExprSelector[mrq, xg] { res = sel; }
	| mc=seqMappingClause[xg] sel=seqExprSelector[mc, xg] { res = sel; }
	| LPAREN { xg.append("("); } seqExpression[xg] RPAREN { xg.append(")"); } 
	| exp=seqConstantOfBasicOrEnumType[xg] sel=seqExprSelector[(ExprNode)exp, xg] { res = sel; }
	| (seqConstantOfContainerType[null]) => exp=seqConstantOfContainerType[xg] sel=seqExprSelector[(ExprNode)exp, xg] { res = sel; }
	| seqConstantOfMatchClassType[xg]
	| exp=seqInitObjectExpr[xg]
	| {env.test(ParserEnvironment.TYPES, input.LT(1).getText()) && !env.test(ParserEnvironment.ENTITIES, input.LT(1).getText())}? i=IDENT
		{
			id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
			res = new IdentExprNode(id);
			xg.append(i.getText());
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
	: d=DOT methodOrAttrName=seqMemberIdentUse { xg.append("."+methodOrAttrName.getSymbol().getText()); } 
		(
			{ env.isArrayAttributeAccessMethodName(input.get(input.LT(1).getTokenIndex()-1).getText()) }?
				LT mi=seqMemberIdentUse GT { xg.append("<" + mi.getSymbol().getText() + ">"); }
			LPAREN { xg.append("("); } 
			( arg=seqExpression[xg] { arguments.addChild(arg); }
				( COMMA { xg.append(","); } arg=seqExpression[xg] { arguments.addChild(arg); } )*
				)? RPAREN { xg.append(")"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, mi); }
		|
			{ input.get(input.LT(1).getTokenIndex()-1).getText().equals("map") }?
				LT ti=seqTypeIdentUse GT { xg.append("<" + ti.getSymbol().getText() + ">"); }
			( combinedName=seqInitExpression[xg, methodOrAttrName.toString()]
				{ if(!combinedName.equals("mapStartWithAccumulateBy"))
					reportError(getCoords(d), "The lambda-expression method " + combinedName + " is not known. Available are: assign, removeIf, assignStartWithAccumulateBy.");
				}
			)?
			LBRACE { xg.append("{"); } { env.pushScope("arraymap/exec", getCoords(d)); }
				seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
				{ env.popScope(); } RBRACE { xg.append("}"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, ti); } // note: "assign" als in case of "assignStartWithAccumulateBy"
		|
			{ input.get(input.LT(1).getTokenIndex()-1).getText().equals("removeIf") }?
			LBRACE { xg.append("{"); } { env.pushScope("arrayremoveIf/exec", getCoords(d)); }
				seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
				{ env.popScope(); } RBRACE { xg.append("}"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, ti); }
		|
			LPAREN { xg.append("("); } 
			( arg=seqExpression[xg] { arguments.addChild(arg); }
				( COMMA { xg.append(","); } arg=seqExpression[xg] { arguments.addChild(arg); } )*
				)? RPAREN { xg.append(")"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, mi); }
		|
			{ res = new MemberAccessExprNode(getCoords(d), prefix, methodOrAttrName); }
		)
		sel=seqExprSelector[res, xg] { res = sel; }
	| DOT v=VISITED { xg.append(".visited"); } ((LBRACK) => LBRACK 
		{ xg.append("["); } visId=seqExpression[xg] RBRACK { xg.append("]"); })?
		{ res = new VisitedNode(getCoords(v), visId, prefix); }
		sel=seqExprSelector[res, xg] { res = sel; }
	| l=LBRACK { xg.append("["); } key=seqExpression[xg] RBRACK { xg.append("]"); }
		{ res = makeBinOp(l, prefix, key); } // array/deque/map access
		sel=seqExprSelector[res, xg] { res = sel; }
	| // no selector
	;

seqLambdaExprVarDeclPrefix [ ExecNode xg ]
	options { k = *; }
	: 	seqEntityDecl[xg] SEMI { xg.append(";"); }
			seqMaybePreviousAccumulationAccessLambdaExprVarDecl[xg]
	|
		seqMaybePreviousAccumulationAccessLambdaExprVarDecl[xg]
	;

seqMaybePreviousAccumulationAccessLambdaExprVarDecl [ ExecNode xg ]
	options { k = *; }
	: 	seqEntityDecl[xg] COMMA { xg.append(","); }
			seqMaybeIndexedLambdaExprVarDecl[xg]
	|
		seqMaybeIndexedLambdaExprVarDecl[xg]
	;

seqMaybeIndexedLambdaExprVarDecl [ ExecNode xg ]
	options { k = *; }
	: 	seqEntityDecl[xg] RARROW { xg.append("->"); }
			seqEntityDecl[xg] RARROW { xg.append("->"); }
	|
		seqEntityDecl[xg] RARROW { xg.append("->"); }
	;

seqProcedureOrMethodCall [ ExecNode xg ]
	@init {
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	// built-in procedure or user defined procedure, backend has to decide whether the call is valid
	: ( LPAREN { xg.append("("); } seqVariableList[xg, returns] RPAREN ASSIGN { xg.append(")="); } )?
		( p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); } )?
		( seqVarUse[xg] d=DOT { xg.append("."); } (attrName=IDENT DOT { xg.append(attrName.getText()+ "."); })? )?
		( i=IDENT | i=EMIT | i=EMITDEBUG | i=DELETE) LPAREN { xg.append(i.getText()); xg.append("("); } 
			seqFunctionCallParameters[xg] RPAREN { xg.append(")"); }
	;

seqFunctionCall [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init{
		boolean inPackage = false;
		boolean packPrefix = false;
	}
	// built-in function or user defined function, backend has to decide whether the call is valid
	: ( p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); packPrefix=true; } )?
	  ( i=IDENT | i=COPY | i=CLONE | i=NAMEOF | i=TYPEOF ) LPAREN { xg.append(i.getText()); xg.append("("); }
			params=seqFunctionCallParameters[xg] RPAREN { xg.append(")"); }
		{
			if(i.getText().equals("now") && params.getChildren().size() == 0 || env.isGlobalFunction(null, i, params)) {
				IdentNode funcIdent = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				if(packPrefix) {
					res = new PackageFunctionInvocationDecisionNode(p.getText(), funcIdent, params, env);
				} else {
					res = new FunctionInvocationDecisionNode(funcIdent, params, env);
				}
			} else {
				IdentNode funcIdent = inPackage ? 
					new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
						env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)))
					: new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				res = new FunctionOrExternalFunctionInvocationExprNode(funcIdent, params);
			}
		}
	;

seqFunctionCallParameters [ ExecNode xg ] returns [ CollectNode<ExprNode> params = new CollectNode<ExprNode>(); ]
	: (fromExpr=seqExpression[xg] { params.addChild(fromExpr); }
		( COMMA { xg.append(","); } fromExpr2=seqExpression[xg] { params.addChild(fromExpr2); } )* )?
	;

seqScanFunctionCall [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: ( s=SCAN { xg.append(s.getText()); } | s=TRYSCAN { xg.append(s.getText()); } ) (LT { xg.append("<"); } type=seqTypeOrContainerTypeContinuation[xg])? LPAREN { xg.append("("); }
			params=seqFunctionCallParameters[xg] RPAREN { xg.append(")"); }
		{
			if(params.getChildren().size() == 1) {
				if(s.getText().equals("scan")) {
					res = new ScanExprNode(getCoords(s), type, params.get(0));
				} else {
					res = new TryScanExprNode(getCoords(s), type, params.get(0));
				}
			} else {
				reportError(getCoords(s), "The function " + s.getText() + " expects 1 parameter (and a type parameter) (given are " + params.getChildren().size() + ").");
			}
		}
	;

seqTypeOrContainerTypeContinuation [ ExecNode xg ] returns [ BaseNode res = null ]
	: { input.LT(1).getText().equals("map") }?
		i=IDENT LT { xg.append(i.getText() + "<"); } keyType=seqTypeIdentUse COMMA { xg.append(keyType + ","); } valueType=seqTypeIdentUse { xg.append(valueType); } (GT GT { xg.append("> >"); } | SR { xg.append(">>"); } )
		{ res = new MapTypeNode(keyType, valueType); }
	| { input.LT(1).getText().equals("set") }?
		i=IDENT LT { xg.append(i.getText() + "<"); } valueType=seqTypeIdentUse { xg.append(valueType); } (GT GT { xg.append("> >"); } | SR { xg.append(">>"); })
		{ res = new SetTypeNode(valueType); }
	| { input.LT(1).getText().equals("array") }?
		i=IDENT LT { xg.append(i.getText() + "<"); } valueType=seqTypeIdentUse { xg.append(valueType); } (GT GT { xg.append("> >"); } | SR { xg.append(">>"); })
		{ res = new ArrayTypeNode(valueType); }
	| { input.LT(1).getText().equals("deque") }?
		i=IDENT { xg.append(i.getText() + "<"); } LT valueType=seqTypeIdentUse { xg.append(valueType); } (GT GT { xg.append("> >"); } | SR { xg.append(">>"); })
		{ res = new DequeTypeNode(valueType); }
	| typeIdent=seqTypeIdentUse GT
		{ res = typeIdent; } { xg.append(typeIdent); } { xg.append(">"); }
	;

seqConstant [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		IdentNode id;
	}
	: seqConstantOfBasicOrEnumType[xg]
	| seqConstantOfContainerType[xg]
	| {env.test(ParserEnvironment.TYPES, input.LT(1).getText()) && !env.test(ParserEnvironment.ENTITIES, input.LT(1).getText())}? i=IDENT
		{
			id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
			res = new IdentExprNode(id);
			xg.append(i.getText());
		}
	;
	
seqConstantOfBasicOrEnumType [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		IdentNode id;
	}
	: b=NUM_BYTE { xg.append(b.getText()); res = new ByteConstNode(getCoords(b), Byte.parseByte(ByteConstNode.removeSuffix(b.getText()), 10)); }
	| sh=NUM_SHORT { xg.append(sh.getText()); res = new ShortConstNode(getCoords(sh), Short.parseShort(ShortConstNode.removeSuffix(sh.getText()), 10)); }
	| i=NUM_INTEGER { xg.append(i.getText()); res = new IntConstNode(getCoords(i), Integer.parseInt(i.getText(), 10)); }
	| l=NUM_LONG { xg.append(l.getText()); res = new LongConstNode(getCoords(l), Long.parseLong(LongConstNode.removeSuffix(l.getText()), 10)); }
	| f=NUM_FLOAT { xg.append(f.getText()); res = new FloatConstNode(getCoords(f), Float.parseFloat(f.getText())); }
	| d=NUM_DOUBLE { xg.append(d.getText()); res = new DoubleConstNode(getCoords(d), Double.parseDouble(d.getText())); }
	| s=STRING_LITERAL { xg.append(s.getText()); String buff = s.getText();
			// Strip the " from the string
			buff = buff.substring(1, buff.length() - 1);
			res = new StringConstNode(getCoords(s), buff); }
	| tt=TRUE { xg.append(tt.getText()); res = new BoolConstNode(getCoords(tt), true); }
	| ff=FALSE { xg.append(ff.getText()); res = new BoolConstNode(getCoords(ff), false); }
	| n=NULL { xg.append(n.getText()); res = new NullConstNode(getCoords(n)); }
	| i1=IDENT d=DOUBLECOLON i2=IDENT e=seqConstantOfBasicOrEnumTypeCont[xg, i1, d, i2] { res = e; }
	;

seqConstantOfMatchClassType [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: (NEW { xg.append("new "); })? MATCH LT CLASS { xg.append("match<class "); } type=seqTypeIdentUse GT LPAREN RPAREN { xg.append(type + ">()"); }
	;
	
seqConstantOfContainerType [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: (NEW { xg.append("new "); })? init=seqConstantOfContainerTypeCont[xg] { res = init; }
	;

seqConstantOfContainerTypeCont [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		IdentNode id;
	}
	: { input.LT(1).getText().equals("map") }?
		IDENT LT typeName=seqTypeIdentUse COMMA toTypeName=seqTypeIdentUse GT { xg.append("map<"+typeName+","+toTypeName+">"); } 
		e1=seqInitMapExpr[xg, new MapTypeNode(typeName, toTypeName)] { res = e1; }
	| { input.LT(1).getText().equals("set") }?
		IDENT LT typeName=seqTypeIdentUse GT { xg.append("set<"+typeName+">"); } 
		e2=seqInitSetExpr[xg, new SetTypeNode(typeName)] { res = e2; }
	| { input.LT(1).getText().equals("array") }?
		IDENT LT { xg.append("array<"); } e3=seqConstantOfContainerTypeArrayCont[xg] { res = e3; }
	| { input.LT(1).getText().equals("deque") }?
		IDENT LT typeName=seqTypeIdentUse GT { xg.append("deque<"+typeName+">"); } 
		e4=seqInitDequeExpr[xg, new DequeTypeNode(typeName)] { res = e4; }
	;

seqConstantOfBasicOrEnumTypeCont [ ExecNode xg, Token i1, Token d1, Token i2 ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		IdentNode id;
	}
	:
		{
			Token pen = i1;
			if(env.test(ParserEnvironment.PACKAGES, pen.getText()) || !env.test(ParserEnvironment.TYPES, pen.getText())) {
				id = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, pen.getText(), getCoords(pen)), 
						env.occurs(ParserEnvironment.TYPES, i2.getText(), getCoords(i2)));
				res = new IdentExprNode(id);
			} else {
				res = new DeclExprNode(new EnumExprNode(getCoords(d1), 
					new IdentNode(env.occurs(ParserEnvironment.TYPES, pen.getText(), getCoords(pen))),
					new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i2.getText(), getCoords(i2)))));
			}
			xg.append(pen.getText() + "::" + i2.getText());
		}
	|
		d2=DOUBLECOLON i=IDENT
		{
			Token p = i1;
			Token en = i2;
			res = new DeclExprNode(new EnumExprNode(getCoords(d2), 
					new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)),
							env.occurs(ParserEnvironment.TYPES, en.getText(), getCoords(en))),
					new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)))));
			xg.append(p.getText() + "::" + en.getText() + "::" + i.getText());
		}
	;

seqConstantOfContainerTypeArrayCont [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: typeName=seqTypeIdentUse GT { xg.append(typeName + ">"); } 
		e=seqInitArrayExpr[xg, new ArrayTypeNode(typeName)] { res = e; }
	| typeName=seqMatchTypeIdentUseInContainerType[xg] (GT GT { xg.append("> >"); } | SR { xg.append(">>"); })
		e=seqInitArrayExpr[xg, new ArrayTypeNode(typeName)] { res = e; }
	;

seqInitMapExpr [ ExecNode xg, MapTypeNode mapType ] returns [ ExprNode res = null ]
	@init {
		MapInitNode mapInit = null;
	}
	: l=LBRACE { xg.append("{"); } { res = mapInit = new MapInitNode(getCoords(l), null, mapType); }
		( item1=seqKeyToValue[xg] { mapInit.addPairItem(item1); }
			( COMMA { xg.append(","); } item2=seqKeyToValue[xg] { mapInit.addPairItem(item2); } )*
		)?
	  RBRACE { xg.append("}"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new MapCopyConstructorNode(getCoords(l), null, mapType, value); }
	  RPAREN { xg.append(")"); }
	;

seqInitSetExpr [ ExecNode xg, SetTypeNode setType ] returns [ ExprNode res = null ]
	@init {
		SetInitNode setInit = null;
	}
	: l=LBRACE { xg.append("{"); } { res = setInit = new SetInitNode(getCoords(l), null, setType); }
		( seqInitializerOfSingleElements[xg, setInit] )?
	  RBRACE { xg.append("}"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new SetCopyConstructorNode(getCoords(l), null, setType, value); }
	  RPAREN { xg.append(")"); }
	;

seqInitArrayExpr [ ExecNode xg, ArrayTypeNode arrayType ] returns [ ExprNode res = null ]
	@init {
		ArrayInitNode arrayInit = null;
	}
	: l=LBRACK { xg.append("["); } { res = arrayInit = new ArrayInitNode(getCoords(l), null, arrayType); }	
		( seqInitializerOfSingleElements[xg, arrayInit] )?
	  RBRACK { xg.append("]"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new ArrayCopyConstructorNode(getCoords(l), null, arrayType, value); }
	  RPAREN { xg.append(")"); }
	;

seqInitDequeExpr [ ExecNode xg, DequeTypeNode dequeType ] returns [ ExprNode res = null ]
	@init {
		DequeInitNode dequeInit = null;
	}
	: l=LBRACK { xg.append("["); } { res = dequeInit = new DequeInitNode(getCoords(l), null, dequeType); }	
		( seqInitializerOfSingleElements[xg, dequeInit] )?
	  RBRACK { xg.append("]"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new DequeCopyConstructorNode(getCoords(l), null, dequeType, value); }
	  RPAREN { xg.append(")"); }
	;

seqInitializerOfSingleElements [ ExecNode xg, ContainerSingleElementInitNode initNode ]
	: item1=seqExpression[xg] { initNode.addItem(item1); }
		( COMMA { xg.append(","); } item2=seqExpression[xg] { initNode.addItem(item2); } )*
	;

seqKeyToValue [ ExecNode xg ] returns [ ExprPairNode res = null ]
	: key=seqExpression[xg] a=RARROW { xg.append("->"); } value=seqExpression[xg]
		{
			res = new ExprPairNode(getCoords(a), key, value);
		}
	;

seqInitObjectExpr [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	options { k = 5; }
	: NEW { xg.append("new "); } type=seqTypeIdentUse LPAREN RPAREN { xg.append(type); xg.append("()"); }
	| NEW { xg.append("new "); } type=seqTypeIdentUse { xg.append(type); }
		AT l=LPAREN { xg.append("@("); } (seqAttributesInitializationList[xg])? RPAREN { xg.append(")"); }
	;

seqAttributesInitializationList [ ExecNode xg ]
	: seqAttributeInitialization[xg] ( COMMA { xg.append(","); } seqAttributeInitialization[xg] )*
	;

seqAttributeInitialization [ ExecNode xg ]
	: attr=seqMemberIdentUse { xg.append(attr.getSymbol().getText()); } ASSIGN { xg.append("="); } arg=seqExpression[xg]
	;

seqMultiRulePrefixedSequence [ ExecNode xg, CollectNode<BaseNode> returns ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: l=LBRACK LBRACK {xg.append("[[");} 
		seqRulePrefixedSequenceAtom[xg, ruleCalls, returns]
		( COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); } seqRulePrefixedSequenceAtom[xg, ruleCalls, returns] )*
	  RBRACK { xg.append("]"); } ( seqCallRuleOrMatchClassFilter[xg, filters, true] )* RBRACK { xg.append("]"); }
		{ xg.addMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters)); }
	;

seqRulePrefixedSequence [ ExecNode xg, CollectNode<BaseNode> returns ]
	: LBRACK { xg.append("["); } seqRulePrefixedSequenceAtom[xg, null, returns] RBRACK { xg.append("]"); }
	;

seqRulePrefixedSequenceAtom [ ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns ]
	: FOR l=LBRACE { env.pushScope("ruleprefixedsequence/exec", getCoords(l)); } { xg.append("for{"); } 
		seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, false] SEMI { xg.append(";"); }
			sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	;

seqMultiRuleAllCall [ ExecNode xg, CollectNode<BaseNode> returns, boolean isAllBracketed ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: l=LBRACK LBRACK { xg.append("[["); } 
		seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, isAllBracketed]
		( COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); }
				seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, isAllBracketed] )*
	  RBRACK { xg.append("]"); } ( seqCallRuleOrMatchClassFilter[xg, filters, true] )* RBRACK { xg.append("]"); }
		{ xg.addMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters)); }
	;
	
seqParallelCallRule [ ExecNode xg, CollectNode<BaseNode> returns ]
	: ( LPAREN { xg.append("("); } seqVariableList[xg, returns] RPAREN ASSIGN { xg.append(")="); } )?
		(	( DOLLAR {xg.append("$");} (MOD { xg.append("\%"); })? ( seqVarUse[xg] 
						(COMMA { xg.append(","); } (seqVarUse[xg] | STAR { xg.append("*"); }))? )? )?
				LBRACK { xg.append("["); } 
				seqCallRule[xg, null, returns, true]
				RBRACK { xg.append("]"); }
		| 
			COUNT { xg.append("count"); }
				LBRACK { xg.append("["); } 
				seqCallRule[xg, null, returns, true]
				RBRACK { xg.append("]"); }
		|
			seqCallRule[xg, null, returns, false]
		)
	;

seqRuleQuery [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	: LBRACK { xg.append("["); }
		cre=seqCallRuleExpression[xg] { res = cre; }
		RBRACK { xg.append("]"); }
	;

seqMultiRuleQuery [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
		CollectNode<ExprNode> ruleCallExprs = new CollectNode<ExprNode>();
	}
	: l=LBRACK QUESTION LBRACK { xg.append("[?["); } 
		cre=seqCallRuleExpressionForMulti[xg, ruleCalls] { ruleCallExprs.addChild(cre); }
		( COMMA { xg.append(","); } cre=seqCallRuleExpressionForMulti[xg, ruleCalls] { ruleCallExprs.addChild(cre); } )*
	  RBRACK { xg.append("]"); }
		( seqCallRuleOrMatchClassFilter[xg, filters, true] )*
		BACKSLASH { xg.append("\\"); } LT CLASS matchClassIdent=seqTypeIdentUse GT
			{ xg.append("<class " + matchClassIdent.toString() + ">"); }
	  RBRACK { xg.append("]"); }
		{
			MultiCallActionNode multiRuleCall = new MultiCallActionNode(getCoords(l), ruleCalls, filters);
			xg.addMultiCallAction(multiRuleCall);
			res = new MultiRuleQueryExprNode(getCoords(l), ruleCallExprs, matchClassIdent, new ArrayTypeNode(matchClassIdent));
		}
	;

seqMappingClause [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: l=LBRACK COLON {xg.append("[:");} 
		seqRulePrefixedSequenceAtom[xg, ruleCalls, returns]
		( COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); } seqRulePrefixedSequenceAtom[xg, ruleCalls, returns] )*
	  ( seqCallRuleOrMatchClassFilter[xg, filters, true] )* COLON RBRACK {xg.append(":]");} 
		{ xg.addMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters)); }
	;

seqCallRuleExpressionForMulti [ ExecNode xg, CollectNode<CallActionNode> ruleCalls ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: id=seqActionOrEntIdentUse { xg.append(id); }
		(LPAREN { xg.append("("); } ( seqRuleParams[xg, params] )? RPAREN { xg.append(")");})?
		( seqCallRuleOrMatchClassFilter[xg, filters, false] )*
		{
			CallActionNode ruleCall = new CallActionNode(id.getCoords(), id, params, returns, filters, false);
			xg.addCallAction(ruleCall);
			if(ruleCalls != null) { // must be added to MultiCallActionNode if used from multi rule all call or multi backtrack construct
				ruleCalls.addChild(ruleCall);
			}
			res = new RuleQueryExprNode(id.getCoords(), ruleCall, new ArrayTypeNode(MatchTypeActionNode.getMatchTypeIdentNode(env, id)));
		}
	;

seqCallRuleWithOptionalReturns [ ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns, boolean isAllBracketed ]
	: (LPAREN { xg.append("("); } seqVariableList[xg, returns] RPAREN ASSIGN { xg.append(")="); })? 
		seqCallRule[xg, ruleCalls, returns, isAllBracketed]
	;

seqCallRule [ ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns, boolean isAllBracketed ]
	@init {
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: ( | MOD { xg.append("\%"); } | MOD QUESTION { xg.append("\%?"); } | QUESTION { xg.append("?"); } | QUESTION MOD { xg.append("?\%"); } )
		(seqVarUse[xg] DOT { xg.append("."); })?
		id=seqActionOrEntIdentUse { xg.append(id); }
		(LPAREN {xg.append("(");} (seqRuleParams[xg, params])? RPAREN { xg.append(")"); })?
		( seqCallRuleOrMatchClassFilter[xg, filters, false] )*
		{
			CallActionNode ruleCall = new CallActionNode(id.getCoords(), id, params, returns, filters, isAllBracketed);
			xg.addCallAction(ruleCall);
			if(ruleCalls != null) { // must be added to MultiCallActionNode if used from multi rule all call or multi backtrack construct
				ruleCalls.addChild(ruleCall);
			}
		}
	;

seqCallRuleExpression [ ExecNode xg ] returns [ ExprNode res = env.initExprNode() ]
	@init {
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	: ( QUESTION MOD { xg.append("?\%"); } | MOD QUESTION { xg.append("\%?"); } | QUESTION { xg.append("?"); } )
		(seqVarUse[xg] DOT { xg.append("."); })?
		id=seqActionOrEntIdentUse { xg.append(id); }
		(LPAREN {xg.append("(");} (seqRuleParams[xg, params])? RPAREN { xg.append(")"); })?
		( seqCallRuleOrMatchClassFilter[xg, filters, false] )*
		{
			CallActionNode ruleCall = new CallActionNode(id.getCoords(), id, params, returns, filters, true);
			xg.addCallAction(ruleCall);
			res = new RuleQueryExprNode(id.getCoords(), ruleCall, new ArrayTypeNode(MatchTypeActionNode.getMatchTypeIdentNode(env, id)));
		}
	;

seqCallRuleOrMatchClassFilter [ ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter ]
	: BACKSLASH { xg.append("\\"); } (p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); })? (id=IDENT | id=AUTO) { xg.append(id.getText()); }
		(
			DOT { xg.append("."); } seqCallMatchClassFilterContinuation[xg, filters, isMatchClassFilter, p, id]
		|
			seqCallRuleFilterContinuation[xg, filters, isMatchClassFilter, p, id]
		)
	;

seqCallRuleFilterContinuation [ ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pin, Token idin ]
	@init {
		String filterBaseText = null;
	}
	: { filterBaseText = idin.getText(); } LT { xg.append("<"); } seqFilterCallVariableList[xg] GT { xg.append(">"); }
		seqCallRuleFilterContinuationMember[xg, filters, isMatchClassFilter, pin, idin, filterBaseText]
	| seqCallRuleFilterContinuationNonMember[xg, filters, isMatchClassFilter, pin, idin]
	;

seqCallRuleFilterContinuationMember [ ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pin, Token idin, String filterBaseText ]
	: (filterBaseTextExt=seqFilterExtension[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		(LPAREN { xg.append("("); } RPAREN { xg.append(")"); })?
		{
			Token p = pin;
			Token filterBase = idin;

			if(p != null)
				reportError(getCoords(filterBase), "A package specifier is not allowed for auto-generated filters.");
			if(isMatchClassFilter)
				reportError(getCoords(filterBase), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(!env.isAutoGeneratedBaseFilterName(filterBaseText))
			{
				reportError(getCoords(filterBase), "The def-variable-based filter " + filterBaseText + " is not known. Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy.");
			}
		}
	|
		(filterBaseTextExt=seqInitExpression[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		l=LBRACE { xg.append("{"); } { env.pushScope("filterassign/exec", getCoords(l)); } 
				seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
				{ env.popScope(); } RBRACE { xg.append("}"); }
			(LPAREN { xg.append("("); } RPAREN { xg.append(")"); })?
		{
			Token p = pin;
			Token filterId = idin;

			if(p != null)
				reportError(getCoords(filterId), "A package specifier is not allowed for per-element with lambda-expression filters.");
			if(isMatchClassFilter)
				reportError(getCoords(filterId), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(!filterBaseText.equals("assign") 
				&& !filterBaseText.equals("assignStartWithAccumulateBy"))
			{
				reportError(getCoords(filterId), "The per-element with lambda-expression filter " + filterBaseText + " is not known. Available are: assign and assignStartWithAccumulateBy.");
			}
		}
	;

seqCallRuleFilterContinuationNonMember [ ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pin, Token idin ]
	@init {
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
	}
	: (LPAREN { xg.append("("); } (seqRuleParams[xg, params])? RPAREN { xg.append(")"); })?
		{
			Token p = pin;
			Token filterId = idin;

			if(isMatchClassFilter)
				reportError(getCoords(filterId), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(env.isAutoSuppliedFilterName(filterId.getText()))
			{
				if(params.size() != 1)
					reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 1 argument (given are " + params.size() + " arguments).");
			}
			else if(filterId.getText().equals("auto"))
			{
				if(isMatchClassFilter)
					reportError(getCoords(filterId), "The auto filter is not available for multi rule call or multi rule backtracking constructs.");
				if(params.size() != 0)
					reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 0 arguments (given are " + params.size() + " arguments).");
			}
			else
			{
				IdentNode filter = p != null ? new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
														env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId)))
												: new IdentNode(env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId)));
				filters.addChild(filter);
			}
		}
	| l=LBRACE { xg.append("{"); } { env.pushScope("filterremoveIf/exec", getCoords(l)); }
		seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
		{ env.popScope(); } RBRACE { xg.append("}"); }
		(LPAREN { xg.append("("); } RPAREN { xg.append(")"); })?
		{
			Token p = pin;
			Token filterId = idin;

			if(p != null)
				reportError(getCoords(filterId), "A package specifier is not allowed for per-element with lambda-expression filters.");
			if(isMatchClassFilter)
				reportError(getCoords(filterId), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(!filterId.getText().equals("removeIf"))
			{
				reportError(getCoords(filterId), "The per-element with lambda-expression filter " + filterId.getText() + " is not known. Available is: removeIf.");
			}
		}
	;

seqCallMatchClassFilterContinuation [ ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pmc, Token mc ]
	@init {
		String filterBaseText = null;
	}
	: filterBase=IDENT { xg.append(filterBase.getText()); filterBaseText = filterBase.getText(); } LT { xg.append("<"); } seqFilterCallVariableList[xg] GT { xg.append(">"); }
		seqCallMatchClassFilterContinuationMember[xg, filters, isMatchClassFilter, pmc, mc, filterBase, filterBaseText]
	| (p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); })? filterId=IDENT { xg.append(filterId.getText()); }
		seqCallMatchClassFilterContinuationNonMember[xg, filters, isMatchClassFilter, pmc, mc, p, filterId]
	;

seqCallMatchClassFilterContinuationMember [ ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pmc, Token mc, Token filterBase, String filterBaseText ]
	: (filterBaseTextExt=seqFilterExtension[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		(LPAREN { xg.append("("); } RPAREN { xg.append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(!env.isAutoGeneratedBaseFilterName(filterBaseText))
			{
				reportError(getCoords(filterBase), "The def-variable-based filter " + filterBaseText + " is not known. Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy.");
			}
		}
	| (filterBaseTextExt=seqInitExpression[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		l=LBRACE { xg.append("{"); } { env.pushScope("filterassign/exec", getCoords(l)); }
		seqLambdaExprVarDeclPrefix[xg] seqExpression[xg]
		{ env.popScope(); } RBRACE { xg.append("}"); }
		(LPAREN { xg.append("("); } RPAREN { xg.append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(!filterBaseText.equals("assign")
				&& !filterBaseText.equals("assignStartWithAccumulateBy"))
			{
				reportError(getCoords(filterBase), "The per-element with lambda-expression filter " + filterBaseText + " is not known. Available are: assign and assignStartWithAccumulateBy.");
			}
		}
	;

seqCallMatchClassFilterContinuationNonMember [ ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pmc, Token mc, Token p, Token filterId ]
	@init {
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
	}
	: (LPAREN { xg.append("("); } (seqRuleParams[xg, params])? RPAREN { xg.append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(env.isAutoSuppliedFilterName(filterId.getText()))
			{
				if(params.size() != 1)
					reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 1 argument (given are " + params.size() + " arguments).");
			}
			else
			{
				IdentNode matchClass = pmc != null ? new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, pmc.getText(), getCoords(pmc)), 
														env.occurs(ParserEnvironment.TYPES, mc.getText(), getCoords(mc)))
													: new IdentNode(env.occurs(ParserEnvironment.TYPES, mc.getText(), getCoords(mc)));
				IdentNode matchClassFilter = p != null ? new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
														env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId)))
													: new IdentNode(env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId)));
				filters.addChild(new MatchTypeQualIdentNode(getCoords(filterId), matchClass, matchClassFilter));
			}
		}
	| l=LBRACE { xg.append("{"); } { env.pushScope("filterassign/exec", getCoords(l)); }
		seqLambdaExprVarDeclPrefix[xg] seqExpression[xg] 
		{ env.popScope(); } RBRACE { xg.append("}"); }
		(LPAREN { xg.append("("); } RPAREN { xg.append(")"); })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");
			if(p != null)
				reportError(getCoords(filterId), "A package specifier is not allowed for per-element with lambda-expression filters.");
		
			if(!filterId.getText().equals("removeIf"))
			{
				reportError(getCoords(filterId), "The per-element with lambda-expression filter " + filterId.getText() + " is not known. Available is: removeIf.");
			}
		}
	;

seqFilterExtension [ ExecNode xg, String filterBaseText ] returns [ String res = null ]
	: filterBaseExtension=IDENT { xg.append(filterBaseExtension.getText()); } LT { xg.append("<"); } seqFilterCallVariableList[xg] GT { xg.append(">"); } 
		filterBaseExtension2=IDENT { xg.append(filterBaseExtension2.getText()); } LT { xg.append("<"); } seqFilterCallVariableList[xg] GT { xg.append(">"); }
		{
			res = filterBaseText + filterBaseExtension.getText() + filterBaseExtension2.getText();
		}
	;

seqInitExpression [ ExecNode xg, String filterBaseText ] returns [ String res = null ]
	: filterBaseExtension=IDENT { xg.append(filterBaseExtension.getText()); } 
		l=LBRACE { xg.append("{"); } { env.pushScope("filterassign/initexpr", getCoords(l)); } 
		seqInitExprVarDeclPrefix[xg]
		{ env.popScope(); } RBRACE { xg.append("}"); }
		filterBaseExtension2=IDENT { xg.append(filterBaseExtension2.getText()); }
		{
			res = filterBaseText + filterBaseExtension.getText() + filterBaseExtension2.getText();
		}
	;

seqInitExprVarDeclPrefix [ ExecNode xg ]
	options { k = *; }
	: seqEntityDecl[xg] SEMI { xg.append(";"); } seqExpression[xg]
	| seqExpression[xg]
	;

seqFilterCallVariableList [ ExecNode xg ]
	: filterVariable=IDENT { xg.append(filterVariable.getText()); }
		( COMMA { xg.append(","); } filterVariable=IDENT { xg.append(filterVariable.getText()); } )*
	;

seqRuleParam [ ExecNode xg, CollectNode<BaseNode> parameters ]
	: exp=seqExpression[xg] { parameters.addChild(exp); if(exp == null) throw new RuntimeException(); }
	;

seqRuleParams [ ExecNode xg, CollectNode<BaseNode> parameters ]
	: seqRuleParam[xg, parameters] ( COMMA { xg.append(","); } seqRuleParam[xg, parameters] )*
	;

seqVariableList [ ExecNode xg, CollectNode<BaseNode> res ]
	: child=seqEntity[xg] { res.addChild(child); }
		( COMMA { xg.append(","); } child=seqEntity[xg] { res.addChild(child); } )*
	;

// read context (assignment rhs)
seqVarUse [ ExecNode xg ] returns [ IdentNode res = null ]
	:
		id=seqEntIdentUse { res = id; xg.append(id); xg.addUsage(id); } // var of node, edge, or basic type
	|
		DOUBLECOLON id=seqEntIdentUse { res = id; xg.append("::" + id); xg.addUsage(id); } // global var of node, edge, or basic type
	;

// write context (assignment lhs)
seqEntity [ ExecNode xg ] returns [ BaseNode res = null ]
	:
		id=seqEntIdentUse { res = id; xg.append(id); xg.addWriteUsage(id); } // var of node, edge, or basic type
	|
		DOUBLECOLON id=seqEntIdentUse { res = id; xg.append("::" + id); xg.addWriteUsage(id); } // global var of node, edge, or basic type
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
			xg.append(decl.getIdentNode().getIdent() + ":" + decl.typeUnresolved);
			xg.addVarDecl(decl);
			res = decl;
		}
	;

seqEntityDeclCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // node/var decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, type);
			xg.append(id.toString() + ":" + type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		cont=seqEntityDeclGenericTypeCont[xg, id] { res = cont; } // container/match type decl
	;

seqEntityDeclGenericTypeCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		{ input.LT(1).getText().equals("map") }?
		IDENT LT keyType=seqTypeIdentUse COMMA { xg.append(id.toString() + ":map<" + keyType.toString() + ","); }
		cont=seqEntityDeclGenericTypeMapCont[xg, id, keyType] { res = cont; }
	|
		{ input.LT(1).getText().equals("set") }?
		IDENT LT cont=seqEntityDeclGenericTypeSetCont[xg, id] { res = cont; }
	|
		{ input.LT(1).getText().equals("array") }?
		IDENT LT cont=seqEntityDeclGenericTypeArrayCont[xg, id] { res = cont; }
	|
		{ input.LT(1).getText().equals("deque") }?
		IDENT LT cont=seqEntityDeclGenericTypeDequeCont[xg, id] { res = cont; }
	|
		MATCH LT cont=seqEntityDeclGenericTypeMatchCont[xg, id] { res = cont; }
	;

seqEntityDeclGenericTypeMapCont [ ExecNode xg, IdentNode id, IdentNode keyType ] returns [ ExecVarDeclNode res = null ]
	:
		valueType=seqTypeIdentUse // map decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new MapTypeNode(keyType, valueType));
			xg.append(valueType.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		valueType=seqMatchTypeIdentUseInContainerType[xg] // map to match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new MapTypeNode(keyType, valueType));
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeSetCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // set decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new SetTypeNode(type));
			xg.append(id.toString() + ":set<" + type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		{ xg.append(id.toString() + ":set<"); } type=seqMatchTypeIdentUseInContainerType[xg] // set of match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new SetTypeNode(type));
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeArrayCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // array decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			xg.append(id.toString() + ":array<" + type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		{ xg.append(id.toString() + ":array<"); } type=seqMatchTypeIdentUseInContainerType[xg] // array of match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeDequeCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		type=seqTypeIdentUse // deque decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new DequeTypeNode(type));
			xg.append(id.toString() + ":deque<" + type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		{ xg.append(id.toString() + ":deque<"); } type=seqMatchTypeIdentUseInContainerType[xg] // deque of match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new DequeTypeNode(type));
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEndPastMatchType[xg]
	;

seqEntityDeclGenericTypeMatchCont [ ExecNode xg, IdentNode id ] returns [ ExecVarDeclNode res = null ]
	:
		actionIdent=seqActionIdentUse // match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, MatchTypeActionNode.getMatchTypeIdentNode(env, actionIdent));
			xg.append(id.toString() + ":match<" + actionIdent.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	|
		CLASS matchClassIdent=seqTypeIdentUse // match class decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, matchClassIdent);
			xg.append(id.toString() + ":match<class " + matchClassIdent.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
		genericTypeEnd[xg]
	;

seqMatchTypeIdentUseInContainerType [ ExecNode xg ] returns [ IdentNode res = null ]
	options { k = 3; }
	:
		MATCH LT actionIdent=seqActionIdentUse // match decl
		{
			res = MatchTypeActionNode.getMatchTypeIdentNode(env, actionIdent);
			xg.append("match<" + actionIdent.toString());
		}
	|
		MATCH LT CLASS matchClassIdent=seqTypeIdentUse // match class decl
		{
			res = matchClassIdent;
			xg.append("match<class " + matchClassIdent.toString());
		}
	;

// special to save user from splitting e.g. map<S,T>=x to map<S,T> =x as >= is GE not GT ASSIGN
genericTypeEnd [ ExecNode xg ]
	: GT { xg.append(">"); }
	| (GE) => { }
	;

// special to save user from splitting e.g. array< match<T> >=x to array< match<T> > =x as >= is GE not GT ASSIGN
// note that array<match<T>>=x is SR ASSIGN, not GT GT ASSIGN as in array< match<T> > =x
genericTypeEndPastMatchType [ ExecNode xg ]
	: GT GT { xg.append("> >"); } 
	| SR { xg.append(">>"); }
	| (GT GE) => GT { xg.append(">"); }
	;

seqIndex [ ExecNode xg ]
	: id=seqIndexIdentUse { xg.append(id.toString()); }
	;

seqEntIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.define(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	;

seqTypeIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	options { k = 3; }
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))); }
	;

seqEntIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	;

seqActionIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	options { k = 3; }
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	;

seqActionOrEntIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	options { k = 3; }
	: i=IDENT
		{ if(i != null) res = new AmbiguousIdentNode(env.occurs(ParserEnvironment.ACTIONS,
			i.getText(), getCoords(i)), env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	;

seqIndexIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.occurs(ParserEnvironment.INDICES, i.getText(), getCoords(i))); }
	;

seqMemberIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	| r=REPLACE // HACK: For string replace function... better choose another name?
		{ if(r != null) {
			r.setType(IDENT);
			res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, r.getText(), getCoords(r)));
		  }
		}
	;

seqRangeSpecLoop [ ExecNode xg ]
	: 	// range allows [*], [+], [c:*], [c], [c:d]; no range equals 1:1
		(
			LBRACK { xg.append("["); }
			(
				STAR { xg.append("*"); }
			|
				PLUS { xg.append("+"); }
			|
				seqExpression[xg]
				(
					COLON { xg.append(" : "); } ( STAR { xg.append("*"); } | seqExpression[xg] )
				)?
			)
			RBRACK { xg.append("]"); }
		)?
	;
