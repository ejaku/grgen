/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */
 
/*
 * GrGen sequence in rule specification language grammar for ANTLR 3
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski, Veit Batz, Edgar Jakumeit, Sebastian Buchwald, Moritz Kroll
 * @version $Id: base.g 20237 2008-06-24 15:59:24Z eja $
*/

parser grammar EmbeddedExec;

@members {
	private OpNode makeOp(org.antlr.runtime.Token t) {
		return gParent.makeOp(t);
	}

	private OpNode makeBinOp(org.antlr.runtime.Token t, ExprNode op0, ExprNode op1) {
		return gParent.makeBinOp(t, op0, op1);
	}

	private OpNode makeUnOp(org.antlr.runtime.Token t, ExprNode op) {
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
	: p=seqEntityDecl[xg, false] { params.addChild(p); } ( COMMA p=seqEntityDecl[xg, false] { params.addChild(p); } )*
	;

sequence [ExecNode xg]
	: seqLazyOr[xg] ( DOLLAR THENLEFT {xg.append(" $<; ");} sequence[xg] | THENLEFT {xg.append(" <; ");} sequence[xg]
						| DOLLAR THENRIGHT {xg.append(" $;> ");} sequence[xg] | THENRIGHT {xg.append(" ;> ");} sequence[xg] )?
	;

seqLazyOr [ExecNode xg]
	: seqLazyAnd[xg] ( DOLLAR LOR {xg.append(" $|| ");} seqLazyOr[xg] | LOR {xg.append(" || ");} seqLazyOr[xg] )?
	;

seqLazyAnd [ExecNode xg]
	: seqStrictOr[xg] ( DOLLAR LAND {xg.append(" $&& ");} seqLazyAnd[xg] | LAND {xg.append(" && ");} seqLazyAnd[xg] )?
	;

seqStrictOr [ExecNode xg]
	: seqStrictXor[xg] ( DOLLAR BOR {xg.append(" $| ");} seqStrictOr[xg] | BOR {xg.append(" | ");} seqStrictOr[xg] )?
	;

seqStrictXor [ExecNode xg]
	: seqStrictAnd[xg] ( DOLLAR BXOR {xg.append(" $^ ");} seqStrictXor[xg] | BXOR {xg.append(" ^ ");} seqStrictXor[xg] )?
	;

seqStrictAnd [ExecNode xg]
	: seqNegOrIteration[xg] ( DOLLAR BAND {xg.append(" $& ");} seqStrictAnd[xg] | BAND {xg.append(" & ");} seqStrictAnd[xg] )?
	;

seqNegOrIteration [ExecNode xg]
	: NOT {xg.append("!");} seqIterSequence[xg] 
		(ASSIGN_TO {xg.append("=>");} seqEntity[xg] | BOR_TO {xg.append("|>");} seqEntity[xg] | BAND_TO {xg.append("&>");} seqEntity[xg])?
	| seqIterSequence[xg]
		(ASSIGN_TO {xg.append("=>");} seqEntity[xg] | BOR_TO {xg.append("|>");} seqEntity[xg] | BAND_TO {xg.append("&>");} seqEntity[xg])?
	;

seqIterSequence [ExecNode xg]
	: seqSimpleSequence[xg]
		(
			rsn=seqRangeSpecLoop { xg.append(rsn); }
		|
			STAR { xg.append("*"); }
		|
			PLUS { xg.append("+"); }
		)
	;

seqSimpleSequence [ExecNode xg]
options { k = 4; }
	@init{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	
	// attention/todo: names are are only partly resolved!
	// -> using not existing types, not declared names outside of the return assignment of an action call 
	// will not be detected in the frontend; xgrs in the frontend are to a certain degree syntax only
	: (seqEntity[null] (ASSIGN | GE )) => lhs=seqEntity[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); })
		(
			id=seqEntIdentUse LPAREN // deliver understandable error message for case of missing parenthesis at rule result assignment
				{ reportError(id.getCoords(), "the destination variable(s) of a rule result assignment must be enclosed in parenthesis"); }
		|
			(seqConstant[null]) => seqConstant[xg]
		|
			seqVarUse[xg]
		|
			d=DOLLAR MOD LPAREN seqTypeIdentUse RPAREN
			{ reportError(getCoords(d), "user input is only requestable in the GrShell, not at lgsp(libgr search plan backend)-level"); }
		|
			d=DOLLAR LPAREN 
			(
				n=NUM_INTEGER RPAREN { xg.append("$("); xg.append(n.getText()); xg.append(")"); }
				| f=NUM_DOUBLE RPAREN { xg.append("$("); xg.append(f.getText()); xg.append(")"); }
			)
		|
			LPAREN { xg.append('('); } sequence[xg] RPAREN { xg.append(')'); }
		)
	| seqVarDecl=seqEntityDecl[xg, true]
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
			(COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); } seqParallelCallRule[xg, returns])* GT RBRACE { xg.append(">}"); }
	| DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? 
		(LOR { xg.append("||"); } | LAND { xg.append("&&"); } | BOR { xg.append("|"); } | BAND { xg.append("&"); }) 
		LPAREN { xg.append("("); } sequence[xg] (COMMA { xg.append(","); } sequence[xg])* RPAREN { xg.append(")"); }
	| DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? DOT { xg.append("."); } 
		LPAREN { xg.append("("); } f=NUM_DOUBLE { xg.append(f.getText() + " "); } sequence[xg] 
			(COMMA { xg.append(","); } f=NUM_DOUBLE { xg.append(f.getText() + " "); } sequence[xg])* RPAREN { xg.append(")"); }
	| LPAREN { xg.append("("); } sequence[xg] RPAREN { xg.append(")"); }
	| LT { xg.append(" <"); } sequence[xg] GT { xg.append("> "); }
	| l=SL { env.pushScope("backtrack/exec", getCoords(l)); } { xg.append(" <<"); } 
		seqParallelCallRule[xg, returns] (DOUBLE_SEMI|SEMI) { xg.append(";;"); } sequence[xg] { env.popScope(); } SR { xg.append(">> "); }
	| l=SL { env.pushScope("backtrack/exec", getCoords(l)); } { xg.append(" <<"); } 
		seqMultiRuleAllCall[xg, returns, false] (DOUBLE_SEMI|SEMI) { xg.append(";;"); } sequence[xg] { env.popScope(); } SR { xg.append(">> "); }
	| SL { xg.append(" <<"); } seqMultiRulePrefixedSequence[xg, returns] SR { xg.append(">> "); }
	| DIV { xg.append(" /"); } sequence[xg] DIV { xg.append("/ "); }
	| IF l=LBRACE { env.pushScope("if/exec", getCoords(l)); } { xg.append("if{"); } sequence[xg] s=SEMI 
		{ env.pushScope("if/then-part", getCoords(s)); } { xg.append("; "); } sequence[xg] { env.popScope(); }
		(SEMI { xg.append("; "); } sequence[xg])? { env.popScope(); } RBRACE { xg.append("}"); }
	| FOR l=LBRACE { env.pushScope("for/exec", getCoords(l)); } { xg.append("for{"); } seqEntity[xg] seqForSeqRemainder[xg, returns]
	| IN { xg.append("in "); } seqVarUse[xg] (d=DOT attr=IDENT { xg.append("."+attr.getText()); })? 
		LBRACE { xg.append("{"); } { env.pushScope("in subgraph sequence", getCoords(l)); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); } 
	| LBRACE { xg.append("{"); } { env.pushScope("sequence computation", getCoords(l)); }
		seqCompoundComputation[xg] (SEMI)? { env.popScope(); } RBRACE { xg.append("}"); } 
	;

seqForSeqRemainder [ExecNode xg, CollectNode<BaseNode> returns]
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
		{ input.LT(1).getText().equals("ascending") || input.LT(1).getText().equals("descending") }? i=IDENT { xg.append(i.getText()); } 
		LPAREN { xg.append("("); } seqIndex[xg] ( seqRelOs[xg] seqExpression[xg]
				( COMMA { xg.append(","); } seqIndex[xg] seqRelOs[xg] seqExpression[xg] )? )? 
		RPAREN { xg.append(")"); } RBRACE { xg.append("}"); } SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN LBRACK QUESTION { xg.append(" in [?"); } seqCallRule[xg, null, returns, true] RBRACK { xg.append("]"); }
			SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN LBRACK { xg.append(" in ["); } left=seqExpression[xg] COLON { xg.append(" : "); } right=seqExpression[xg] RBRACK { xg.append("]"); }
			SEMI { xg.append("; "); } sequence[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	;

seqCompoundComputation [ExecNode xg]
	: seqComputation[xg] (SEMI { xg.append(";"); } seqCompoundComputation[xg])?
	;

seqComputation [ExecNode xg]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); }) seqExpressionOrAssign[xg]
	| seqEntityDecl[xg, true]
	| seqMethodCall[xg]
	| seqProcedureCall[xg]
	| LBRACE { xg.append("{"); } seqExpression[xg] RBRACE { xg.append("}"); }
	;

seqMethodCall [ExecNode xg]
	: seqVarUse[xg] d=DOT method=IDENT LPAREN { xg.append("."+method.getText()+"("); } 
			 ( seqExpression[xg] (COMMA { xg.append(","); } seqExpression[xg])* )? RPAREN { xg.append(")"); }
	;

seqExpressionOrAssign [ExecNode xg]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); }) seqExpressionOrAssign[xg]
	| seqExpression[xg] 
	;

seqAssignTarget [ExecNode xg]
	: YIELD { xg.append("yield "); } seqVarUse[xg] 
	| seqVarUse[xg] seqAssignTargetSelector[xg]
	| seqEntityDecl[xg, true]
	;

seqAssignTargetSelector [ExecNode xg]
	: DOT attr=IDENT { xg.append("."+attr.getText()); } 
		(LBRACK { xg.append("["); } seqExpression[xg] RBRACK { xg.append("]"); })?
	| DOT VISITED LBRACK { xg.append(".visited["); } seqExpression[xg] RBRACK { xg.append("]"); } 
	| LBRACK { xg.append("["); } seqExpression[xg] RBRACK { xg.append("]"); }
	|
	;

// todo: add expression value returns to remaining sequence expressions,
// as of now only some sequence expressions return an expression
// the expressions are needed for the argument expressions of rule/sequence calls,
// in all other places of the sequences we only need a textual emit of the constructs just parsed
seqExpression [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprLazyOr[xg] { res = exp; }
		( 
			q=QUESTION { xg.append("?"); } op1=seqExpression[xg] COLON { xg.append(" : "); } op2=seqExpression[xg]
			{
				OpNode cond=makeOp(q);
				cond.addChild(exp);
				cond.addChild(op1);
				cond.addChild(op2);
				res=cond;
			}
		)?
	;

seqExprLazyOr [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprLazyAnd[xg] { res=exp; } ( t=LOR {xg.append(" || ");} exp2=seqExprLazyOr[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprLazyAnd [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprStrictOr[xg] { res=exp; } ( t=LAND {xg.append(" && ");} exp2=seqExprLazyAnd[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprStrictOr [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprStrictXor[xg] { res=exp; } ( t=BOR {xg.append(" | ");} exp2=seqExprStrictOr[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprStrictXor [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprStrictAnd[xg] { res=exp; } ( t=BXOR {xg.append(" ^ ");} exp2=seqExprStrictXor[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprStrictAnd [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprExcept[xg] { res=exp; } ( t=BAND {xg.append(" & ");} exp2=seqExprStrictAnd[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprExcept [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprEquality[xg] { res = exp; } ( t=BACKSLASH {xg.append(" \\ ");} exp2=seqExprExcept[xg] { res = makeBinOp(t, exp, exp2); })?
	;
	
seqEqOp [ExecNode xg] returns [ Token t = null ]
	: e=EQUAL {xg.append(" == "); t = e; }
	| n=NOT_EQUAL {xg.append(" != "); t = n; }
	| s=STRUCTURAL_EQUAL {xg.append(" ~~ "); t = s; }
	;

seqExprEquality [ExecNode xg] returns [ExprNode res = env.initExprNode()]
	: exp=seqExprRelation[xg] { res=exp; } ( t=seqEqOp[xg] exp2=seqExprEquality[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqRelOp [ExecNode xg] returns [ Token t = null ]
	: lt=LT {xg.append(" < "); t = lt; }
	| le=LE {xg.append(" <= "); t = le; }
	| gt=GT {xg.append(" > "); t = gt; }
	| ge=GE {xg.append(" >= "); t = ge; }
	| in=IN {xg.append(" in "); t = in; }
	;

seqRelOs [ExecNode xg] returns [ Token t = null ]
	: lt=LT {xg.append(" < "); t = lt; }
	| le=LE {xg.append(" <= "); t = le; }
	| gt=GT {xg.append(" > "); t = gt; }
	| ge=GE {xg.append(" >= "); t = ge; }
	;

seqExprRelation [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprAdd[xg] { res = exp; } 
		( t=seqRelOp[xg] exp2=seqExprRelation[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprAdd [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprMul[xg] { res = exp; } 
		( (t=PLUS {xg.append(" + ");} | t=MINUS {xg.append(" - ");}) exp2=seqExprAdd[xg]  { res = makeBinOp(t, exp, exp2); })?
	;

seqExprMul [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprUnary[xg] { res = exp; } 
		( (t=STAR {xg.append(" * ");} | t=DIV {xg.append(" / ");} | t=MOD {xg.append(" \% ");}) exp2=seqExprMul[xg]  { res = makeBinOp(t, exp, exp2); })?
	;

seqExprUnary [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	@init{ Token t = null; }
	: (LPAREN seqTypeIdentUse RPAREN) =>
		p=LPAREN {xg.append("(");} id=seqTypeIdentUse {xg.append(id);} RPAREN {xg.append(")");} op=seqExprBasic[xg]
		{
			res = new CastNode(getCoords(p), id, op);
		}
	| (n=NOT {t=n; xg.append("!");})? exp=seqExprBasic[xg] { if(t!=null) res = makeUnOp(t, exp); else res = exp; }
	| m=MINUS {xg.append("-");} exp=seqExprBasic[xg]
		{
			OpNode neg = new ArithmeticOpNode(getCoords(m), OperatorSignature.NEG);
			neg.addChild(exp);
			res = neg;
		}
	;

// todo: the seqVarUse[xg] casted to IdenNodes might be not simple variable identifiers, but global variables with :: prefix,
//  probably a distinction is needed
seqExprBasic [ExecNode xg] returns[ExprNode res = env.initExprNode()]
options { k = 4; }
	@init{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		IdentNode id;
	}
	: owner=seqVarUseInExpr[xg] sel=seqExprSelector[owner, xg] { res = sel; }
	| {input.LT(1).getText().equals("this")}? i=IDENT { xg.append("this"); } sel=seqExprSelector[owner, xg] { res = sel; }
	| fc=seqFunctionCall[xg] { res = fc; }
	| DEF LPAREN { xg.append("def("); } seqVariableList[xg, returns] RPAREN { xg.append(")"); } 
	| a=AT LPAREN { xg.append("@("); } 
		(i=IDENT { xg.append(i.getText()); } | s=STRING_LITERAL { xg.append(s.getText()); }) RPAREN { xg.append(")"); }
	| rq=seqRuleQuery[xg] sel=seqExprSelector[rq, xg] { res = sel; }
	| mrq=seqMultiRuleQuery[xg] sel=seqExprSelector[mrq, xg] { res = sel; }
	| LPAREN { xg.append("("); } seqExpression[xg] RPAREN { xg.append(")"); } 
	| exp=seqConstantWithoutType[xg] sel=seqExprSelector[(ExprNode)exp, xg] { res = sel; }
	| {env.test(ParserEnvironment.TYPES, input.LT(1).getText()) && !env.test(ParserEnvironment.ENTITIES, input.LT(1).getText())}? i=IDENT
		{
			id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
			res = new IdentExprNode(id);
			xg.append(i.getText());
		}
	;

seqVarUseInExpr [ExecNode xg] returns[IdentExprNode res]
	: var=seqVarUse[xg] { res = new IdentExprNode(var); }
	;

seqExprSelector [ExprNode prefix, ExecNode xg] returns[ExprNode res = prefix]
options { k = 3; }
	@init{
		CollectNode<ExprNode> arguments = new CollectNode<ExprNode>();
	}
	: d=DOT methodOrAttrName=seqMemberIdentUse { xg.append("."+methodOrAttrName.getSymbol().getText()); } 
			({ env.isArrayAttributeAccessMethodName(input.get(input.LT(1).getTokenIndex()-1).getText()) }? LT mi=memberIdentUse GT { xg.append("<" + mi.getSymbol().getText() + ">"); })?
		(
			LPAREN { xg.append("("); } 
			( arg=seqExpression[xg] { arguments.addChild(arg); }
				(COMMA { xg.append(","); } arg=seqExpression[xg] { arguments.addChild(arg); })*
				)? RPAREN { xg.append(")"); }
			{ res = new FunctionMethodInvocationDecisionNode(prefix, methodOrAttrName, arguments, mi); }
		|
			{ res = new MemberAccessExprNode(getCoords(d), prefix, methodOrAttrName); }
		)
		sel=seqExprSelector[res, xg] { res = sel; }
	| DOT v=VISITED LBRACK 
		{ xg.append(".visited["); } visId=seqExpression[xg] RBRACK { xg.append("]"); }
		{ res = new VisitedNode(getCoords(v), visId, prefix); }
		sel=seqExprSelector[res, xg] { res = sel; }
	| l=LBRACK { xg.append("["); } key=seqExpression[xg] RBRACK { xg.append("]"); }
		{ res = new IndexedAccessExprNode(getCoords(l), prefix, key); } // array/deque/map access
		sel=seqExprSelector[res, xg] { res = sel; }
	| // no selector
	;
	
seqProcedureCall [ExecNode xg]
	@init{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	// built-in procedure or user defined procedure, backend has to decide whether the call is valid
	: ( LPAREN {xg.append("(");} seqVariableList[xg, returns] RPAREN ASSIGN {xg.append(")=");} )?
		( p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); } )?
		( i=IDENT | i=EMIT | i=EMITDEBUG | i=DELETE) LPAREN { xg.append(i.getText()); xg.append("("); } 
			seqFunctionCallParameters[xg] RPAREN { xg.append(")"); }
	;

seqFunctionCall [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	@init{
		boolean inPackage = false;
	}
	// built-in function or user defined function, backend has to decide whether the call is valid
	: ( p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); } )?
	  ( i=IDENT | i=COPY | i=NAMEOF | i=TYPEOF ) LPAREN { xg.append(i.getText()); xg.append("("); } params=seqFunctionCallParameters[xg] RPAREN { xg.append(")"); }
		{
			if(i.getText().equals("now") && params.getChildren().size()==0
				|| env.isGlobalFunction(null, i, params)) {
				IdentNode funcIdent = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				res = new FunctionInvocationDecisionNode(funcIdent, params, env);
			} else {
				IdentNode funcIdent = inPackage ? 
					new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
						env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)))
					: new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				res = new FunctionOrExternalFunctionInvocationExprNode(funcIdent, params);
			}
		}
	;

seqFunctionCallParameters [ExecNode xg] returns [ CollectNode<ExprNode> params = new CollectNode<ExprNode>(); ]
	: (fromExpr=seqExpression[xg] { params.addChild(fromExpr); }
		(COMMA { xg.append(","); } fromExpr2=seqExpression[xg] { params.addChild(fromExpr2); } )* )?
	;

seqConstant [ExecNode xg] returns[ExprNode res = env.initExprNode()]
@init{ IdentNode id; }
	: seqConstantWithoutType[xg]
	| {env.test(ParserEnvironment.TYPES, input.LT(1).getText()) && !env.test(ParserEnvironment.ENTITIES, input.LT(1).getText())}? i=IDENT
		{
			id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
			res = new IdentExprNode(id);
			xg.append(i.getText());
		}
	;
	
seqConstantWithoutType [ExecNode xg] returns[ExprNode res = env.initExprNode()]
options { k = 4; }
@init{ IdentNode id; }
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
	| MAP LT typeName=seqTypeIdentUse COMMA toTypeName=seqTypeIdentUse GT { xg.append("map<"+typeName+","+toTypeName+">"); } 
		e1=seqInitMapExpr[xg, new MapTypeNode(typeName, toTypeName)] { res = e1; }
	| SET LT typeName=seqTypeIdentUse GT { xg.append("set<"+typeName+">"); } 
		e2=seqInitSetExpr[xg, new SetTypeNode(typeName)] { res = e2; }
	| ARRAY LT typeName=seqTypeIdentUse GT { xg.append("array<"+typeName+">"); } 
		e3=seqInitArrayExpr[xg, new ArrayTypeNode(typeName)] { res = e3; }
	| ARRAY LT { xg.append("array<"); } typeName=seqMatchTypeIdentUseInContainerType[xg, true] (GT GT { xg.append("> >"); } | SR { xg.append(">>"); })
		e3=seqInitArrayExpr[xg, new ArrayTypeNode(typeName)] { res = e3; }
	| DEQUE LT typeName=seqTypeIdentUse GT { xg.append("deque<"+typeName+">"); } 
		e4=seqInitDequeExpr[xg, new DequeTypeNode(typeName)] { res = e4; }
	| pen=IDENT d=DOUBLECOLON i=IDENT 
		{
			if(env.test(ParserEnvironment.PACKAGES, pen.getText()) || !env.test(ParserEnvironment.TYPES, pen.getText())) {
				id = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, pen.getText(), getCoords(pen)), 
					env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
				res = new IdentExprNode(id);
			} else {
				res = new DeclExprNode(new EnumExprNode(getCoords(d), 
					new IdentNode(env.occurs(ParserEnvironment.TYPES, pen.getText(), getCoords(pen))),
					new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)))));
			}
			xg.append(pen.getText() + "::" + i.getText());
		}
	| p=IDENT DOUBLECOLON en=IDENT d=DOUBLECOLON i=IDENT
		{
			res = new DeclExprNode(new EnumExprNode(getCoords(d), 
				new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)),
					env.occurs(ParserEnvironment.TYPES, en.getText(), getCoords(en))),
				new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)))));
			xg.append(p.getText() + "::" + en.getText() + "::" + i.getText());
		}
	;

seqInitMapExpr [ExecNode xg, MapTypeNode mapType] returns [ ExprNode res = null ]
	@init{ MapInitNode mapInit = null; }
	: l=LBRACE { xg.append("{"); } { res = mapInit = new MapInitNode(getCoords(l), null, mapType); }
		( item1=seqKeyToValue[xg] { mapInit.addMapItem(item1); }
			( COMMA { xg.append(","); } item2=seqKeyToValue[xg] { mapInit.addMapItem(item2); } )*
		)?
	  RBRACE { xg.append("}"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new MapCopyConstructorNode(getCoords(l), null, mapType, value); }
	  RPAREN { xg.append(")"); }
	;

seqInitSetExpr [ExecNode xg, SetTypeNode setType] returns [ ExprNode res = null ]
	@init{ SetInitNode setInit = null; }
	: l=LBRACE { xg.append("{"); } { res = setInit = new SetInitNode(getCoords(l), null, setType); }
		( item1=seqExpression[xg] { setInit.addSetItem(item1); }
			( COMMA { xg.append(","); } item2=seqExpression[xg] { setInit.addSetItem(item2); } )*
		)?
	  RBRACE { xg.append("}"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new SetCopyConstructorNode(getCoords(l), null, setType, value); }
	  RPAREN { xg.append(")"); }
	;

seqInitArrayExpr [ExecNode xg, ArrayTypeNode arrayType] returns [ ExprNode res = null ]
	@init{ ArrayInitNode arrayInit = null; }
	: l=LBRACK { xg.append("["); } { res = arrayInit = new ArrayInitNode(getCoords(l), null, arrayType); }	
		( item1=seqExpression[xg] { arrayInit.addArrayItem(item1); }
			( COMMA { xg.append(","); } item2=seqExpression[xg] { arrayInit.addArrayItem(item2); } )*
		)?
	  RBRACK { xg.append("]"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new ArrayCopyConstructorNode(getCoords(l), null, arrayType, value); }
	  RPAREN { xg.append(")"); }
	;

seqInitDequeExpr [ExecNode xg, DequeTypeNode dequeType] returns [ ExprNode res = null ]
	@init{ DequeInitNode dequeInit = null; }
	: l=LBRACK { xg.append("["); } { res = dequeInit = new DequeInitNode(getCoords(l), null, dequeType); }	
		( item1=seqExpression[xg] { dequeInit.addDequeItem(item1); }
			( COMMA { xg.append(","); } item2=seqExpression[xg] { dequeInit.addDequeItem(item2); } )*
		)?
	  RBRACK { xg.append("]"); }
	| l=LPAREN { xg.append("("); } value=seqExpression[xg]
		{ res = new DequeCopyConstructorNode(getCoords(l), null, dequeType, value); }
	  RPAREN { xg.append(")"); }
	;

seqKeyToValue [ExecNode xg] returns [ ExprPairNode res = null ]
	: key=seqExpression[xg] a=RARROW { xg.append("->"); } value=seqExpression[xg]
		{
			res = new ExprPairNode(getCoords(a), key, value);
		}
	;

seqMultiRulePrefixedSequence [ExecNode xg, CollectNode<BaseNode> returns]
	@init{
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}

	: l=LBRACK LBRACK {xg.append("[[");} 
		seqRulePrefixedSequenceAtom[xg, ruleCalls, returns]
		( COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); } seqRulePrefixedSequenceAtom[xg, ruleCalls, returns] )*
	  RBRACK {xg.append("]");} (seqCallRuleFilter[xg, filters, true])* RBRACK {xg.append("]");}
		{
			xg.addMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters));
		}
	;

seqRulePrefixedSequence [ExecNode xg, CollectNode<BaseNode> returns]
	: LBRACK {xg.append("[");} seqRulePrefixedSequenceAtom[xg, null, returns] RBRACK {xg.append("]");}
	;

seqRulePrefixedSequenceAtom [ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns]
	: FOR l=LBRACE { env.pushScope("ruleprefixedsequence/exec", getCoords(l)); } {xg.append("for{");} 
		seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, false] SEMI {xg.append(";");}
			sequence[xg] { env.popScope(); } RBRACE {xg.append("}");}
	;

seqMultiRuleAllCall [ExecNode xg, CollectNode<BaseNode> returns, boolean isAllBracketed]
	@init{
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}

	: l=LBRACK LBRACK {xg.append("[[");} 
		seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, isAllBracketed]
		( COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); } seqCallRuleWithOptionalReturns[xg, ruleCalls, returns, isAllBracketed] )*
	  RBRACK {xg.append("]");} (seqCallRuleFilter[xg, filters, true])* RBRACK {xg.append("]");}
		{
			xg.addMultiCallAction(new MultiCallActionNode(getCoords(l), ruleCalls, filters));
		}
	;
	
seqParallelCallRule [ExecNode xg, CollectNode<BaseNode> returns]
	: ( LPAREN {xg.append("(");} seqVariableList[xg, returns] RPAREN ASSIGN {xg.append(")=");} )?
		(	( DOLLAR {xg.append("$");} (MOD { xg.append("\%"); })? ( seqVarUse[xg] 
						(COMMA {xg.append(",");} (seqVarUse[xg] | STAR {xg.append("*");}))? )? )?
				LBRACK {xg.append("[");} 
				seqCallRule[xg, null, returns, true]
				RBRACK {xg.append("]");}
		| 
			COUNT {xg.append("count");}
				LBRACK {xg.append("[");} 
				seqCallRule[xg, null, returns, true]
				RBRACK {xg.append("]");}
		|
			seqCallRule[xg, null, returns, false]
		)
	;

seqRuleQuery [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: LBRACK {xg.append("[");} 
		cre=seqCallRuleExpression[xg] { res = cre; }
		RBRACK {xg.append("]");}
	;

seqMultiRuleQuery [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	@init{
		CollectNode<CallActionNode> ruleCalls = new CollectNode<CallActionNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
		CollectNode<ExprNode> ruleCallExprs = new CollectNode<ExprNode>();
	}

	: l=LBRACK QUESTION LBRACK { xg.append("[?["); } 
		cre=seqCallRuleExpressionForMulti[xg, ruleCalls] { ruleCallExprs.addChild(cre); }
		( COMMA { xg.append(","); } cre=seqCallRuleExpressionForMulti[xg, ruleCalls] { ruleCallExprs.addChild(cre); } )*
	  RBRACK {xg.append("]");} ( seqCallRuleFilter[xg, filters, true] )*
		BACKSLASH { xg.append("\\"); } LT CLASS matchClassIdent=seqTypeIdentUse GT { xg.append("<class " + matchClassIdent.toString() + ">"); }
	  RBRACK {xg.append("]");}
		{
			MultiCallActionNode multiRuleCall = new MultiCallActionNode(getCoords(l), ruleCalls, filters);
			xg.addMultiCallAction(multiRuleCall);
			res = new MultiRuleQueryExprNode(getCoords(l), ruleCallExprs, matchClassIdent, new ArrayTypeNode(matchClassIdent));
		}
	;

seqCallRuleExpressionForMulti [ExecNode xg, CollectNode<CallActionNode> ruleCalls] returns[ExprNode res = env.initExprNode()]
	@init{
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	
	: id=seqActionOrEntIdentUse { xg.append(id); }
		(LPAREN {xg.append("(");} ( seqRuleParams[xg, params] )? RPAREN {xg.append(")");})?
		(seqCallRuleFilter[xg, filters, false])*
		{
			CallActionNode ruleCall = new CallActionNode(id.getCoords(), id, params, returns, filters, false);
			xg.addCallAction(ruleCall);
			if(ruleCalls != null) { // must be added to MultiCallActionNode if used from multi rule all call or multi backtrack construct
				ruleCalls.addChild(ruleCall);
			}
			res = new RuleQueryExprNode(id.getCoords(), ruleCall, new ArrayTypeNode(MatchTypeNode.getMatchTypeIdentNode(env, id)));
		}
	;

seqCallRuleWithOptionalReturns [ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns, boolean isAllBracketed]
	: (LPAREN {xg.append("(");} seqVariableList[xg, returns] RPAREN ASSIGN {xg.append(")=");})? seqCallRule[xg, ruleCalls, returns, isAllBracketed]
	;

seqCallRule [ExecNode xg, CollectNode<CallActionNode> ruleCalls, CollectNode<BaseNode> returns, boolean isAllBracketed]
	@init{
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	
	: ( | MOD { xg.append("\%"); } | MOD QUESTION { xg.append("\%?"); } | QUESTION { xg.append("?"); } | QUESTION MOD { xg.append("?\%"); } )
		(seqVarUse[xg] DOT { xg.append("."); })?
		id=seqActionOrEntIdentUse { xg.append(id); }
		(LPAREN {xg.append("(");} (seqRuleParams[xg, params])? RPAREN { xg.append(")"); })?
		(seqCallRuleFilter[xg, filters, false])*
		{
			CallActionNode ruleCall = new CallActionNode(id.getCoords(), id, params, returns, filters, isAllBracketed);
			xg.addCallAction(ruleCall);
			if(ruleCalls != null) { // must be added to MultiCallActionNode if used from multi rule all call or multi backtrack construct
				ruleCalls.addChild(ruleCall);
			}
		}
	;

seqCallRuleExpression [ExecNode xg] returns[ExprNode res = env.initExprNode()]
	@init{
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		CollectNode<BaseNode> filters = new CollectNode<BaseNode>();
	}
	
	: ( QUESTION MOD { xg.append("?\%"); } | MOD QUESTION { xg.append("\%?"); } | QUESTION { xg.append("?"); } )
		(seqVarUse[xg] DOT { xg.append("."); })?
		id=seqActionOrEntIdentUse { xg.append(id); }
		(LPAREN {xg.append("(");} (seqRuleParams[xg, params])? RPAREN { xg.append(")"); })?
		(seqCallRuleFilter[xg, filters, false])*
		{
			CallActionNode ruleCall = new CallActionNode(id.getCoords(), id, params, returns, filters, true);
			xg.addCallAction(ruleCall);
			res = new RuleQueryExprNode(id.getCoords(), ruleCall, new ArrayTypeNode(MatchTypeNode.getMatchTypeIdentNode(env, id)));
		}
	;

seqCallRuleFilter [ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter]
	: BACKSLASH { xg.append("\\"); } (p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); })? id=IDENT { xg.append(id.getText()); } 
		seqCallRuleFilterContinuation[xg, filters, isMatchClassFilter, p, id]
	;

seqCallRuleFilterContinuation [ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pin, Token idin]
	@init{
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		String filterBaseText = null;
	}
	: DOT { xg.append("."); } seqCallMatchClassRuleFilterContinuation[xg, filters, isMatchClassFilter, pin, idin]
	| (LPAREN {xg.append("(");} (seqRuleParams[xg, params])? RPAREN {xg.append(")");})?
		{
			Token p = pin;
			Token filterId = idin;

			if(isMatchClassFilter)
				reportError(getCoords(filterId), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(env.isAutoSuppliedFilterName(filterId.getText()))
			{
				if(params.size()!=1)
					reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 1 arguments.");
			}
			else if(filterId.getText().equals("auto"))
			{
				if(isMatchClassFilter)
					reportError(getCoords(filterId), "The auto filter is not available for multi rule call or multi rule backtracking constructs.");
				if(params.size()!=0)
					reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 0 arguments.");
			}
			else
			{
				IdentNode filter = p != null ? new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
														env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId)))
												: new IdentNode(env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId)));
				filters.addChild(filter);
			}
		}
	| { filterBaseText = idin.getText(); } LT { xg.append("<"); } seqFilterCallVariableList[xg] GT { xg.append("> "); }
			(filterBaseTextExt=seqFilterExtension[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		{
			Token p = pin;
			Token filterBase = idin;

			if(p != null)
				reportError(getCoords(filterBase), "No package specifier allowed for auto-generated filters.");
			if(isMatchClassFilter)
				reportError(getCoords(filterBase), "A match class specifier is required for filters of multi rule call or multi rule backtracking constructs.");

			if(!env.isAutoGeneratedBaseFilterName(filterBaseText))
			{
				reportError(getCoords(filterBase), "Unknown def-variable-based filter " + filterBaseText + "! Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy.");
			}
		}
	;

seqCallMatchClassRuleFilterContinuation [ExecNode xg, CollectNode<BaseNode> filters, boolean isMatchClassFilter, Token pmc, Token mc]
	@init{
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		String filterBaseText = null;
	}
	: (p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); })? filterId=IDENT { xg.append(filterId.getText()); } 
		(LPAREN {xg.append("(");} (seqRuleParams[xg, params])? RPAREN {xg.append(")");})?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(env.isAutoSuppliedFilterName(filterId.getText()))
			{
				if(params.size()!=1)
					reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 1 arguments.");
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
	| filterBase=IDENT { xg.append(filterBase.getText()); filterBaseText = filterBase.getText(); } LT { xg.append("<"); } seqFilterCallVariableList[xg] GT { xg.append("> "); }
			(filterBaseTextExt=seqFilterExtension[xg, filterBaseText] { filterBaseText = filterBaseTextExt; })?
		{
			if(!isMatchClassFilter)
				reportError(getCoords(mc), "A match class specifier is only admissible for filters of multi rule call or multi rule backtracking constructs.");

			if(!env.isAutoGeneratedBaseFilterName(filterBaseText))
			{
				reportError(getCoords(filterBase), "Unknown def-variable-based filter " + filterBaseText + "! Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy.");
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

seqFilterCallVariableList [ExecNode xg]
	: filterVariable=IDENT { xg.append(filterVariable.getText()); }
		( COMMA {xg.append(",");} filterVariable=IDENT { xg.append(filterVariable.getText()); } )*
	;

seqRuleParam [ExecNode xg, CollectNode<BaseNode> parameters]
	: exp=seqExpression[xg] { parameters.addChild(exp); }
	;

seqRuleParams [ExecNode xg, CollectNode<BaseNode> parameters]
	: seqRuleParam[xg, parameters]	( COMMA {xg.append(",");} seqRuleParam[xg, parameters] )*
	;

seqVariableList [ExecNode xg, CollectNode<BaseNode> res]
	: child=seqEntity[xg] { res.addChild(child); }
		( COMMA { xg.append(","); } child=seqEntity[xg] { res.addChild(child); } )*
	;

// read context (assignment rhs)
seqVarUse [ExecNode xg] returns [IdentNode res = null]
	:
		id=seqEntIdentUse { res = id; xg.append(id); xg.addUsage(id); } // var of node, edge, or basic type
	|
		DOUBLECOLON id=seqEntIdentUse { res = id; xg.append("::" + id); xg.addUsage(id); } // global var of node, edge, or basic type
	;

// write context (assignment lhs)
seqEntity [ExecNode xg] returns [BaseNode res = null]
	:
		id=seqEntIdentUse { res = id; xg.append(id); xg.addWriteUsage(id); } // var of node, edge, or basic type
	|
		DOUBLECOLON id=seqEntIdentUse { res = id; xg.append("::" + id); xg.addWriteUsage(id); } // global var of node, edge, or basic type
	|
		seqVarDecl=seqEntityDecl[xg, true] { res = seqVarDecl; }
	;

seqEntityDecl [ExecNode xg, boolean emit] returns [ExecVarDeclNode res = null]
options { k = *; }
	:
		id=seqEntIdentDecl COLON type=seqTypeIdentUse // node decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, type);
			if(emit) xg.append(id.toString()+":"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=seqEntIdentDecl COLON MAP LT keyType=seqTypeIdentUse COMMA valueType=seqTypeIdentUse GT // map decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new MapTypeNode(keyType, valueType));
			if(emit) xg.append(id.toString()+":map<"+keyType.toString()+","+valueType.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(seqEntIdentDecl COLON MAP LT seqTypeIdentUse COMMA seqTypeIdentUse GE) =>
		id=seqEntIdentDecl COLON MAP LT keyType=seqTypeIdentUse COMMA valueType=seqTypeIdentUse // map decl; special to save user from splitting map<S,T>=x to map<S,T> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new MapTypeNode(keyType, valueType));
			if(emit) xg.append(id.toString()+":map<"+keyType.toString()+","+valueType.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=seqEntIdentDecl COLON SET LT type=seqTypeIdentUse GT // set decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new SetTypeNode(type));
			if(emit) xg.append(id.toString()+":set<"+type.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(seqEntIdentDecl COLON SET LT seqTypeIdentUse GE) => 
		id=seqEntIdentDecl COLON SET LT type=seqTypeIdentUse // set decl; special to save user from splitting set<S>=x to set<S> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new SetTypeNode(type));
			if(emit) xg.append(id.toString()+":set<"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=seqEntIdentDecl COLON ARRAY LT type=seqTypeIdentUse GT // array decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			if(emit) xg.append(id.toString()+":array<"+type.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=seqEntIdentDecl COLON ARRAY LT { if(emit) xg.append(id.toString()+":array<"); } type=seqMatchTypeIdentUseInContainerType[xg, emit] (GT GT { if(emit) xg.append("> >"); } | SR { if(emit) xg.append(">>"); }) // array of match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(seqEntIdentDecl COLON ARRAY LT seqTypeIdentUse GE) => 
		id=seqEntIdentDecl COLON ARRAY LT type=seqTypeIdentUse // array decl; special to save user from splitting array<S>=x to array<S> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			if(emit) xg.append(id.toString()+":array<"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(seqEntIdentDecl COLON ARRAY LT type=seqMatchTypeIdentUseInContainerType[null, false] GT GE) =>
		id=seqEntIdentDecl COLON ARRAY LT { if(emit) xg.append(id.toString()+":array<"); } type=seqMatchTypeIdentUseInContainerType[xg, emit] GT { if(emit) xg.append(">"); } // array of match decl; special to save user from splitting array<match<S> >=x to array<match<S> > =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new ArrayTypeNode(type));
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=seqEntIdentDecl COLON DEQUE LT type=seqTypeIdentUse GT // deque decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new DequeTypeNode(type));
			if(emit) xg.append(id.toString()+":deque<"+type.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(seqEntIdentDecl COLON DEQUE LT seqTypeIdentUse GE) => 
		id=seqEntIdentDecl COLON DEQUE LT type=seqTypeIdentUse // deque decl; special to save user from splitting deque<S>=x to deque<S> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, new DequeTypeNode(type));
			if(emit) xg.append(id.toString()+":deque<"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=seqEntIdentDecl COLON MATCH LT actionIdent=seqActionIdentUse GT // match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, MatchTypeNode.getMatchTypeIdentNode(env, actionIdent));
			if(emit) xg.append(id.toString()+":match<"+actionIdent.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=seqEntIdentDecl COLON MATCH LT CLASS matchClassIdent=seqTypeIdentUse GT // match class decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, matchClassIdent);
			if(emit) xg.append(id.toString()+":match<class "+matchClassIdent.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		MINUS id=seqEntIdentDecl COLON type=seqTypeIdentUse RARROW // edge decl, interpreted grs don't use -:-> form
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, type);
			if(emit) xg.append(decl.getIdentNode().getIdent() + ":" + decl.typeUnresolved);
			xg.addVarDecl(decl);
			res = decl;
		}
	;

seqMatchTypeIdentUseInContainerType [ExecNode xg, boolean emit] returns [IdentNode res = null]
options { k = 3; }
	:
		MATCH LT actionIdent=seqActionIdentUse // match decl
		{
			res = MatchTypeNode.getMatchTypeIdentNode(env, actionIdent);
			if(emit) xg.append("match<" + actionIdent.toString());
		}
	|
		MATCH LT CLASS matchClassIdent=seqTypeIdentUse // match class decl
		{
			res = matchClassIdent;
			if(emit) xg.append("match<class " + matchClassIdent.toString());
		}
	;

seqIndex [ExecNode xg]
	: id=seqIndexIdentUse { xg.append(id.toString()); }
	;

seqEntIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	;

seqTypeIdentUse returns [ IdentNode res = env.getDummyIdent() ]
options { k = 3; }
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i!=null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))); }
	;

seqEntIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	;

seqActionIdentUse returns [ IdentNode res = env.getDummyIdent() ]
options { k = 3; }
	: i=IDENT
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i!=null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	;

seqActionOrEntIdentUse returns [ IdentNode res = env.getDummyIdent() ]
options { k = 3; }
	: i=IDENT
		{ if(i!=null) res = new AmbiguousIdentNode(env.occurs(ParserEnvironment.ACTIONS,
			i.getText(), getCoords(i)), env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i!=null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	;

seqIndexIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.INDICES, i.getText(), getCoords(i))); }
	;

seqMemberIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	| r=REPLACE
		{ if(r!=null) r.setType(IDENT); res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES,
			r.getText(), getCoords(r))); } // HACK: For string replace function... better choose another name?
	;

seqRangeSpecLoop returns [ RangeSpecNode res = null ]
	@init{
		lower = 1; upper = 1;
		de.unika.ipd.grgen.parser.Coords coords = de.unika.ipd.grgen.parser.Coords.getInvalid();
		// range allows [*], [+], [c:*], [c], [c:d]; no range equals 1:1
	}
	:
		(
			l=LBRACK { coords = getCoords(l); }
			(
				STAR { lower=0; upper=RangeSpecNode.UNBOUND; }
			|
				PLUS { lower=1; upper=RangeSpecNode.UNBOUND; }
			|
				lower=seqIntegerConst
				(
					COLON ( STAR { upper=RangeSpecNode.UNBOUND; } | upper=seqIntegerConst )
				|
					{ upper = lower; }
				)
			)
			RBRACK
		)?
		{ res = new RangeSpecNode(coords, lower, upper); }
	;

seqIntegerConst returns [ long value = 0 ]
	: i=NUM_INTEGER
		{ value = Long.parseLong(i.getText()); }
	;
