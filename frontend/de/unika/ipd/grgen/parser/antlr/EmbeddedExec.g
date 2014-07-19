/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */
 
/*
 * GrGen model and rule specification language grammar for ANTLR 3
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
// Embedded XGRS
//////////////////////////////////////////

// todo: add more user friendly explicit error messages for % used after $ instead of implicit syntax error
// (a user choice $% override for the random flag $ is only available in the shell/debugger)

// note: sequences and expressions are right associative here, that's wrong but doesn't matter cause this is only a syntax checking pre pass
// in the backend, the operators are parsed with correct associativity (and with correct left-to-right, def-before-use order of variables)

execInParameters [ ExecNode xg ] returns [ CollectNode<ExecVarDeclNode> res = new CollectNode<ExecVarDeclNode>() ]
	: LPAREN (execParamList[res, xg])? RPAREN
	|
	;

execOutParameters [ ExecNode xg ] returns [ CollectNode<ExecVarDeclNode> res = new CollectNode<ExecVarDeclNode>() ]
	: COLON LPAREN (execParamList[res, xg])? RPAREN
	|
	;

execParamList [ CollectNode<ExecVarDeclNode> params, ExecNode xg ]
	: p=xgrsEntityDecl[xg, false] { params.addChild(p); } ( COMMA p=xgrsEntityDecl[xg, false] { params.addChild(p); } )*
	;

xgrs[ExecNode xg]
	: xgrsLazyOr[xg] ( DOLLAR THENLEFT {xg.append(" $<; ");} xgrs[xg] | THENLEFT {xg.append(" <; ");} xgrs[xg]
						| DOLLAR THENRIGHT {xg.append(" $;> ");} xgrs[xg] | THENRIGHT {xg.append(" ;> ");} xgrs[xg] )?
	;

xgrsLazyOr[ExecNode xg]
	: xgrsLazyAnd[xg] ( DOLLAR LOR {xg.append(" $|| ");} xgrsLazyOr[xg] | LOR {xg.append(" || ");} xgrsLazyOr[xg] )?
	;

xgrsLazyAnd[ExecNode xg]
	: xgrsStrictOr[xg] ( DOLLAR LAND {xg.append(" $&& ");} xgrsLazyAnd[xg] | LAND {xg.append(" && ");} xgrsLazyAnd[xg] )?
	;

xgrsStrictOr[ExecNode xg]
	: xgrsStrictXor[xg] ( DOLLAR BOR {xg.append(" $| ");} xgrsStrictOr[xg] | BOR {xg.append(" | ");} xgrsStrictOr[xg] )?
	;

xgrsStrictXor[ExecNode xg]
	: xgrsStrictAnd[xg] ( DOLLAR BXOR {xg.append(" $^ ");} xgrsStrictXor[xg] | BXOR {xg.append(" ^ ");} xgrsStrictXor[xg] )?
	;

xgrsStrictAnd[ExecNode xg]
	: xgrsNegOrIteration[xg] ( DOLLAR BAND {xg.append(" $& ");} xgrsStrictAnd[xg] | BAND {xg.append(" & ");} xgrsStrictAnd[xg] )?
	;

xgrsNegOrIteration[ExecNode xg]
	: NOT {xg.append("!");} iterSequence[xg] (ASSIGN_TO {xg.append("=>");} xgrsEntity[xg] | BOR_TO {xg.append("|>");} xgrsEntity[xg] | BAND_TO {xg.append("&>");} xgrsEntity[xg])?
	| iterSequence[xg] (ASSIGN_TO {xg.append("=>");} xgrsEntity[xg] | BOR_TO {xg.append("|>");} xgrsEntity[xg] | BAND_TO {xg.append("&>");} xgrsEntity[xg])?
	;

iterSequence[ExecNode xg]
	: simpleSequence[xg]
		(
			rsn=rangeSpecXgrsLoop { xg.append(rsn); }
		|
			STAR { xg.append("*"); }
		|
			PLUS { xg.append("+"); }
		)
	;

simpleSequence[ExecNode xg]
options { k = 3; }
	@init{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	
	// attention/todo: names are are only partly resolved!
	// -> using not existing types, not declared names outside of the return assignment of an action call 
	// will not be detected in the frontend; xgrs in the frontend are to a certain degree syntax only
	: (xgrsEntity[null] (ASSIGN | GE )) => lhs=xgrsEntity[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); })
		(
			id=entIdentUse LPAREN // deliver understandable error message for case of missing parenthesis at rule result assignment
				{ reportError(id.getCoords(), "the destination variable(s) of a rule result assignment must be enclosed in parenthesis"); }
		|
			xgrsConstant[xg]
		|
			xgrsVarUse[xg]
		|
			d=DOLLAR MOD LPAREN typeIdentUse RPAREN
			{ reportError(getCoords(d), "user input is only requestable in the GrShell, not at lgsp(libgr search plan backend)-level"); }
		|
			d=DOLLAR LPAREN 
			(
				n=NUM_INTEGER RPAREN { xg.append("$("); xg.append(n.getText()); xg.append(")"); }
				| f=NUM_DOUBLE RPAREN { xg.append("$("); xg.append(f.getText()); xg.append(")"); }
			)
		|
			LPAREN { xg.append('('); } xgrs[xg] RPAREN { xg.append(')'); }
		)
	| xgrsVarDecl=xgrsEntityDecl[xg, true]
	| YIELD { xg.append("yield "); } lhsent=entIdentUse { xg.append(lhsent); xg.addUsage(lhsent); } ASSIGN { xg.append('='); } 
	    ( xgrsConstant[xg]
		| xgrsVarUse[xg]
		)
	| TRUE { xg.append("true"); }
	| FALSE { xg.append("false"); }
	| (parallelCallRule[null, null]) => parallelCallRule[xg, returns]
	| DOUBLECOLON id=entIdentUse { xg.append("::" + id); xg.addUsage(id); }
	| (( DOLLAR ( MOD )? )? LBRACE LT) => ( DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? )?
		LBRACE LT { xg.append("{<"); } parallelCallRule[xg, returns] (COMMA { xg.append(","); returns = new CollectNode<BaseNode>(); } parallelCallRule[xg, returns])* GT RBRACE { xg.append(">}"); }
	| DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? 
		(LOR { xg.append("||"); } | LAND { xg.append("&&"); } | BOR { xg.append("|"); } | BAND { xg.append("&"); }) 
		LPAREN { xg.append("("); } xgrs[xg] (COMMA { xg.append(","); } xgrs[xg])* RPAREN { xg.append(")"); }
	| DOLLAR { xg.append("$"); } ( MOD { xg.append("\%"); } )? DOT { xg.append("."); } 
		LPAREN { xg.append("("); } f=NUM_DOUBLE { xg.append(f.getText() + " "); } xgrs[xg] (COMMA { xg.append(","); } f=NUM_DOUBLE { xg.append(f.getText() + " "); } xgrs[xg])* RPAREN { xg.append(")"); }
	| LPAREN { xg.append("("); } xgrs[xg] RPAREN { xg.append(")"); }
	| LT { xg.append(" <"); } xgrs[xg] GT { xg.append("> "); }
	| SL { xg.append(" <<"); } parallelCallRule[xg, returns] (DOUBLE_SEMI|SEMI) { xg.append(";;"); } xgrs[xg] SR { xg.append(">> "); }
	| DIV { xg.append(" /"); } xgrs[xg] DIV { xg.append("/ "); }
	| IF l=LBRACE { env.pushScope("if/exec", getCoords(l)); } { xg.append("if{"); } xgrs[xg] s=SEMI 
		{ env.pushScope("if/then-part", getCoords(s)); } { xg.append("; "); } xgrs[xg] { env.popScope(); }
		(SEMI { xg.append("; "); } xgrs[xg])? { env.popScope(); } RBRACE { xg.append("}"); }
	| FOR l=LBRACE { env.pushScope("for/exec", getCoords(l)); } { xg.append("for{"); } xgrsEntity[xg] forSeqRemainder[xg, returns]
	| IN { xg.append("in "); } xgrsVarUse[xg] (d=DOT attr=IDENT { xg.append("."+attr.getText()); })? LBRACE { xg.append("{"); } { env.pushScope("in subgraph sequence", getCoords(l)); } xgrs[xg] { env.popScope(); } RBRACE { xg.append("}"); } 
	| LBRACE { xg.append("{"); } { env.pushScope("sequence computation", getCoords(l)); } seqCompoundComputation[xg] (SEMI)? { env.popScope(); } RBRACE { xg.append("}"); } 
	;

forSeqRemainder[ExecNode xg, CollectNode<BaseNode> returns]
options { k = 4; }
	: (RARROW { xg.append(" -> "); } xgrsEntity[xg])? IN { xg.append(" in "); } xgrsEntity[xg]
			SEMI { xg.append("; "); } xgrs[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN { xg.append(" in "); } { input.LT(1).getText().equals("adjacent") || input.LT(1).getText().equals("adjacentIncoming") || input.LT(1).getText().equals("adjacentOutgoing")
			|| input.LT(1).getText().equals("incident") || input.LT(1).getText().equals("incoming") || input.LT(1).getText().equals("outgoing")
			|| input.LT(1).getText().equals("reachable") || input.LT(1).getText().equals("reachableIncoming") || input.LT(1).getText().equals("reachableOutgoing")
			|| input.LT(1).getText().equals("reachableEdges") || input.LT(1).getText().equals("reachableEdgesIncoming") || input.LT(1).getText().equals("reachableEdgesOutgoing") 
			|| input.LT(1).getText().equals("boundedReachable") || input.LT(1).getText().equals("boundedReachableIncoming") || input.LT(1).getText().equals("boundedReachableOutgoing")
			|| input.LT(1).getText().equals("boundedReachableEdges") || input.LT(1).getText().equals("boundedReachableEdgesIncoming") || input.LT(1).getText().equals("boundedReachableEdgesOutgoing") 
			|| input.LT(1).getText().equals("nodes") || input.LT(1).getText().equals("edges")
		 }?
			i=IDENT LPAREN { xg.append(i.getText()); xg.append("("); }
			expr1=seqExpression[xg] (COMMA { xg.append(","); } expr2=seqExpression[xg] (COMMA { xg.append(","); } expr3=seqExpression[xg] (COMMA { xg.append(","); } expr4=seqExpression[xg])? )? )?
			RPAREN { xg.append(")"); }
			SEMI { xg.append("; "); } xgrs[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN { xg.append(" in "); } LBRACE { xg.append("{"); } xgrsIndex[xg] EQUAL { xg.append(" == "); } seqExpression[xg] 
		RBRACE { xg.append("}"); } SEMI { xg.append("; "); } xgrs[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN { xg.append(" in "); } LBRACE { xg.append("{"); } 
		{ input.LT(1).getText().equals("ascending") || input.LT(1).getText().equals("descending") }? i=IDENT { xg.append(i.getText()); } 
		LPAREN { xg.append("("); } xgrsIndex[xg] ( seqRelOs[xg] seqExpression[xg]
				( COMMA { xg.append(","); } xgrsIndex[xg] seqRelOs[xg] seqExpression[xg] )? )? 
		RPAREN { xg.append(")"); } RBRACE { xg.append("}"); } SEMI { xg.append("; "); } xgrs[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN LBRACK QUESTION { xg.append(" in [?"); } callRule[xg, returns, true] RBRACK { xg.append("]"); }
			SEMI { xg.append("; "); } xgrs[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	| IN LBRACK { xg.append(" in ["); } left=seqExpression[xg] COLON { xg.append(" : "); } right=seqExpression[xg] RBRACK { xg.append("]"); }
			SEMI { xg.append("; "); } xgrs[xg] { env.popScope(); } RBRACE { xg.append("}"); }
	;

seqCompoundComputation[ExecNode xg]
	: seqComputation[xg] (SEMI { xg.append(";"); } seqCompoundComputation[xg])?
	;

seqComputation[ExecNode xg]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); }) seqExpressionOrAssign[xg]
	| (xgrsEntityDecl[null,true]) => xgrsVarDecl=xgrsEntityDecl[xg, true]
	| (methodCall[null]) => methodCall[xg]
	| (procedureCall[null]) => procedureCall[xg]
	| LBRACE { xg.append("{"); } seqExpression[xg] RBRACE { xg.append("}"); }
	;

seqExpressionOrAssign[ExecNode xg]
	: (seqAssignTarget[null] (ASSIGN|GE)) => seqAssignTarget[xg] (ASSIGN { xg.append("="); } | GE { xg.append(">="); }) seqExpressionOrAssign[xg]
	| seqExpression[xg] 
	;

seqAssignTarget[ExecNode xg]
	: YIELD { xg.append("yield "); } xgrsVarUse[xg] 
	| (xgrsVarUse[null] DOT VISITED) => xgrsVarUse[xg] DOT VISITED LBRACK { xg.append(".visited["); } seqExpression[xg] RBRACK { xg.append("]"); } 
	| (xgrsVarUse[null] DOT IDENT ) => xgrsVarUse[xg] d=DOT attr=IDENT { xg.append("."+attr.getText()); }
		(LBRACK { xg.append("["); } seqExpression[xg] RBRACK { xg.append("]"); })?
	| (xgrsVarUse[null] LBRACK) => xgrsVarUse[xg] LBRACK { xg.append("["); } seqExpression[xg] RBRACK { xg.append("]"); }
	| xgrsEntity[xg]
	;

// todo: add expression value returns to remaining sequence expressions,
// as of now only some sequence expressions return an expression
// the expressions are needed for the argument expressions of rule/sequence calls,
// in all other places of the sequences we only need a textual emit of the constructs just parsed
seqExpression[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprLazyOr[xg] { res = exp; }
		( 
			q=QUESTION { xg.append("?"); } op1=seqExpression[xg] COLON { xg.append(":"); } op2=seqExpression[xg]
			{
				OpNode cond=makeOp(q);
				cond.addChild(exp);
				cond.addChild(op1);
				cond.addChild(op2);
				res=cond;
			}
		)?
	;

seqExprLazyOr[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprLazyAnd[xg] { res=exp; } ( t=LOR {xg.append(" || ");} exp2=seqExprLazyOr[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprLazyAnd[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprStrictOr[xg] { res=exp; } ( t=LAND {xg.append(" && ");} exp2=seqExprLazyAnd[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprStrictOr[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprStrictXor[xg] { res=exp; } ( t=BOR {xg.append(" | ");} exp2=seqExprStrictOr[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprStrictXor[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprStrictAnd[xg] { res=exp; } ( t=BXOR {xg.append(" ^ ");} exp2=seqExprStrictXor[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprStrictAnd[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprEquality[xg] { res=exp; } ( t=BAND {xg.append(" & ");} exp2=seqExprStrictAnd[xg] { res = makeBinOp(t, exp, exp2); })?
	;
	
seqEqOp[ExecNode xg] returns [ Token t = null ]
	: e=EQUAL {xg.append(" == "); t = e; }
	| n=NOT_EQUAL {xg.append(" != "); t = n; }
	| s=STRUCTURAL_EQUAL {xg.append(" ~~ "); t = s; }
	;

seqExprEquality[ExecNode xg] returns [ExprNode res = env.initExprNode()]
	: exp=seqExprRelation[xg] { res=exp; } ( t=seqEqOp[xg] exp2=seqExprEquality[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqRelOp[ExecNode xg] returns [ Token t = null ]
	: lt=LT {xg.append(" < "); t = lt; }
	| le=LE {xg.append(" <= "); t = le; }
	| gt=GT {xg.append(" > "); t = gt; }
	| ge=GE {xg.append(" >= "); t = ge; }
	| in=IN {xg.append(" in "); t = in; }
	;

seqRelOs[ExecNode xg] returns [ Token t = null ]
	: lt=LT {xg.append(" < "); t = lt; }
	| le=LE {xg.append(" <= "); t = le; }
	| gt=GT {xg.append(" > "); t = gt; }
	| ge=GE {xg.append(" >= "); t = ge; }
	;

seqExprRelation[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprAdd[xg] { res=exp; } ( t=seqRelOp[xg] exp2=seqExprRelation[xg] { res = makeBinOp(t, exp, exp2); })?
	;

seqExprAdd[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprMul[xg] { res=exp; } ( (t=PLUS {xg.append(" + ");} | t=MINUS {xg.append(" - ");}) exp2=seqExprAdd[xg]  { res = makeBinOp(t, exp, exp2); })?
	;

seqExprMul[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	: exp=seqExprUnary[xg] { res=exp; } ( (t=STAR {xg.append(" * ");} | t=DIV {xg.append(" / ");} | t=MOD {xg.append(" \% ");}) exp2=seqExprMul[xg]  { res = makeBinOp(t, exp, exp2); })?
	;

seqExprUnary[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	@init{ Token t = null; }
	: (LPAREN typeIdentUse RPAREN) =>
		p=LPAREN {xg.append("(");} id=typeIdentUse {xg.append(id);} RPAREN {xg.append(")");} op=seqExprBasic[xg]
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

// todo: the xgrsVarUse[xg] casted to IdenNodes might be not simple variable identifiers, but global variables with :: prefix,
//  probably a distinction is needed
seqExprBasic[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	@init{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	
	: (methodCall[null]) => methodCall[xg]
	| (xgrsVarUse[null] DOT VISITED) => xgrsVarUse[xg] DOT VISITED LBRACK 
		{ xg.append(".visited["); } seqExpression[xg] RBRACK { xg.append("]"); }
	| (xgrsVarUse[null] DOT IDENT) => target=xgrsVarUse[xg] d=DOT attr=memberIdentUse { xg.append("."+attr.getSymbol().getText()); }
			{ res = new MemberAccessExprNode(getCoords(d), new IdentExprNode((IdentNode)target), attr); }
		sel=seqExprSelector[res, xg] { res = sel; }
	| (xgrsConstant[null]) => exp=xgrsConstant[xg] { res = (ExprNode)exp; }
	| {input.LT(1).getText().equals("this")}? i=IDENT { xg.append("this"); }
	| (functionCall[null]) => fc=functionCall[xg]
			 { res = fc; }
	| (xgrsVarUse[null]) => target=xgrsVarUse[xg]
			{ res = new IdentExprNode((IdentNode)target); }
		sel=seqExprSelector[res, xg] { res = sel; }
	| DEF LPAREN { xg.append("def("); } xgrsVariableList[xg, returns] RPAREN { xg.append(")"); } 
	| a=AT LPAREN { xg.append("@("); } (i=IDENT { xg.append(i.getText()); } | s=STRING_LITERAL { xg.append(s.getText()); }) RPAREN { xg.append(")"); }
	| LPAREN { xg.append("("); } seqExpression[xg] RPAREN { xg.append(")"); } 
	;

seqExprSelector[ExprNode prefix, ExecNode xg] returns[ExprNode res = prefix]
	: (LBRACK seqExprSelectorTerminator) => // terminate, deque end
	| (LBRACK) => l=LBRACK { xg.append("["); } key=seqExpression[xg] RBRACK { xg.append("]"); }
			{ res = new IndexedAccessExprNode(getCoords(l), prefix, key); } // array/deque/map access
	| // no selector
	;
	
seqExprSelectorTerminator
	: THENLEFT
	| THENRIGHT
	| LOR
	| LAND 
	| BOR
	| BXOR 
	| BAND
	| PLUS
	| RPAREN
	| RBRACE
	;

procedureCall[ExecNode xg]
	@init{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	// built-in procedure or user defined procedure, backend has to decide whether the call is valid
	: ( LPAREN {xg.append("(");} xgrsVariableList[xg, returns] RPAREN ASSIGN {xg.append(")=");} )?
		( p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); } )?
		( i=IDENT | i=EMIT | i=DELETE) LPAREN { xg.append(i.getText()); xg.append("("); } functionCallParameters[xg] RPAREN { xg.append(")"); }
	;

functionCall[ExecNode xg] returns[ExprNode res = env.initExprNode()]
	@init{
		boolean inPackage = false;
	}
	// built-in function or user defined function, backend has to decide whether the call is valid
	: ( p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); } )?
	  ( i=IDENT | i=COPY | i=NAMEOF | i=TYPEOF ) LPAREN { xg.append(i.getText()); xg.append("("); } params=functionCallParameters[xg] RPAREN { xg.append(")"); }
		{
			if( (i.getText().equals("now")) && params.getChildren().size()==0
				|| (i.getText().equals("nodes") || i.getText().equals("edges")) && params.getChildren().size()<=1
				|| (i.getText().equals("countNodes") || i.getText().equals("countEdges")) && params.getChildren().size()<=1
				|| (i.getText().equals("empty") || i.getText().equals("size")) && params.getChildren().size()==0
				|| (i.getText().equals("source") || i.getText().equals("target")) && params.getChildren().size()==1
				|| i.getText().equals("opposite") && params.getChildren().size()==2
				|| (i.getText().equals("nodeByName") || i.getText().equals("edgeByName")) && params.getChildren().size()==1
				|| (i.getText().equals("nodeByUnique") || i.getText().equals("edgeByUnique")) && params.getChildren().size()==1
				|| (i.getText().equals("incoming") || i.getText().equals("outgoing") || i.getText().equals("incident")) && params.getChildren().size()>=1 && params.getChildren().size()<=3
				|| (i.getText().equals("adjacentIncoming") || i.getText().equals("adjacentOutgoing") || i.getText().equals("adjacent")) && params.getChildren().size()>=1 && params.getChildren().size()<=3
				|| (i.getText().equals("reachableIncoming") || i.getText().equals("reachableOutgoing") || i.getText().equals("reachable")) && params.getChildren().size()>=1 && params.getChildren().size()<=3
				|| (i.getText().equals("reachableEdgesIncoming") || i.getText().equals("reachableEdgesOutgoing") || i.getText().equals("reachableEdges")) && params.getChildren().size()>=1 && params.getChildren().size()<=3 
				|| (i.getText().equals("boundedReachableIncoming") || i.getText().equals("boundedReachableOutgoing") || i.getText().equals("boundedReachable")) && params.getChildren().size()>=2 && params.getChildren().size()<=4
				|| (i.getText().equals("boundedReachableEdgesIncoming") || i.getText().equals("boundedReachableEdgesOutgoing") || i.getText().equals("boundedReachableEdges")) && params.getChildren().size()>=2 && params.getChildren().size()<=4 
				|| (i.getText().equals("boundedReachableWithRemainingDepthIncoming") || i.getText().equals("boundedReachableWithRemainingDepthOutgoing") || i.getText().equals("boundedReachableWithRemainingDepth")) && params.getChildren().size()>=2 && params.getChildren().size()<=4
				|| (i.getText().equals("countIncoming") || i.getText().equals("countOutgoing") || i.getText().equals("countIncident")) && params.getChildren().size()>=1 && params.getChildren().size()<=3
				|| (i.getText().equals("countAdjacentIncoming") || i.getText().equals("countAdjacentOutgoing") || i.getText().equals("countAdjacent")) && params.getChildren().size()>=1 && params.getChildren().size()<=3
				|| (i.getText().equals("countReachableIncoming") || i.getText().equals("countReachableOutgoing") || i.getText().equals("countReachable")) && params.getChildren().size()>=1 && params.getChildren().size()<=3
				|| (i.getText().equals("countReachableEdgesIncoming") || i.getText().equals("countReachableEdgesOutgoing") || i.getText().equals("countReachableEdges")) && params.getChildren().size()>=1 && params.getChildren().size()<=3 
				|| (i.getText().equals("countBoundedReachableIncoming") || i.getText().equals("countBoundedReachableOutgoing") || i.getText().equals("countBoundedReachable")) && params.getChildren().size()>=2 && params.getChildren().size()<=4
				|| (i.getText().equals("countBoundedReachableEdgesIncoming") || i.getText().equals("countBoundedReachableEdgesOutgoing") || i.getText().equals("countBoundedReachableEdges")) && params.getChildren().size()>=2 && params.getChildren().size()<=4 
				|| (i.getText().equals("isIncoming") || i.getText().equals("isOutgoing") || i.getText().equals("isIncident")) && params.getChildren().size()>=2 && params.getChildren().size()<=4
				|| (i.getText().equals("isAdjacentIncoming") || i.getText().equals("isAdjacentOutgoing") || i.getText().equals("isAdjacent")) && params.getChildren().size()>=2 && params.getChildren().size()<=4
				|| (i.getText().equals("isReachableIncoming") || i.getText().equals("isReachableOutgoing") || i.getText().equals("isReachable")) && params.getChildren().size()>=2 && params.getChildren().size()<=4
				|| (i.getText().equals("isReachableEdgesIncoming") || i.getText().equals("isReachableEdgesOutgoing") || i.getText().equals("isReachableEdges")) && params.getChildren().size()>=2 && params.getChildren().size()<=4 
				|| (i.getText().equals("isBoundedReachableIncoming") || i.getText().equals("isBoundedReachableOutgoing") || i.getText().equals("isBoundedReachable")) && params.getChildren().size()>=3 && params.getChildren().size()<=5
				|| (i.getText().equals("isBoundedReachableEdgesIncoming") || i.getText().equals("isBoundedReachableEdgesOutgoing") || i.getText().equals("isBoundedReachableEdges")) && params.getChildren().size()>=3 && params.getChildren().size()<=5 
				|| i.getText().equals("random") && params.getChildren().size()>=0 && params.getChildren().size()<=1
				|| i.getText().equals("canonize") && params.getChildren().size()==1
				|| (i.getText().equals("inducedSubgraph") || i.getText().equals("definedSubgraph")) && params.getChildren().size()==1
				|| (i.getText().equals("equalsAny") || i.getText().equals("equalsAnyStructurally")) && params.getChildren().size()==2
				|| (i.getText().equals("exists") || i.getText().equals("import")) && params.getChildren().size()==1
				|| i.getText().equals("copy") && params.getChildren().size()==1
				|| i.getText().equals("nameof") && (params.getChildren().size()==1 || params.getChildren().size()==0)
				|| i.getText().equals("uniqueof") && (params.getChildren().size()==1 || params.getChildren().size()==0)
			  )
			{
				IdentNode funcIdent = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				res = new FunctionInvocationExprNode(funcIdent, params, env);
			} else {
				IdentNode funcIdent = inPackage ? 
					new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
						env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)))
					: new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				res = new FunctionOrExternalFunctionInvocationExprNode(funcIdent, params);
			}
		}
	;

functionCallParameters[ExecNode xg] returns [ CollectNode<ExprNode> params = new CollectNode<ExprNode>(); ]
	: (fromExpr=seqExpression[xg] { params.addChild(fromExpr); } (COMMA { xg.append(","); } fromExpr2=seqExpression[xg] { params.addChild(fromExpr2); } )* )?
	;
	
methodCall[ExecNode xg]
	: xgrsVarUse[xg] d=DOT method=IDENT LPAREN { xg.append("."+method.getText()+"("); } 
			 ( seqExpression[xg] (COMMA { xg.append(","); } seqExpression[xg])? )? RPAREN { xg.append(")"); }
	;

xgrsConstant[ExecNode xg] returns[ExprNode res = env.initExprNode()]
options { k = 4; }
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
	| i=IDENT d=DOUBLECOLON id=entIdentUse { xg.append(i.getText() + "::" + id); res = new DeclExprNode(new EnumExprNode(getCoords(d), new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))), id)); }
	| p=IDENT DOUBLECOLON i=IDENT d=DOUBLECOLON id=entIdentUse { xg.append(p.getText() + "::" + i.getText() + "::" + id); res = new DeclExprNode(new EnumExprNode(getCoords(d), new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))), id)); }
	| MAP LT typeName=typeIdentUse COMMA toTypeName=typeIdentUse GT { xg.append("map<"+typeName+","+toTypeName+">"); } e1=seqInitMapExpr[xg, MapTypeNode.getMapType(typeName, toTypeName)] { res = e1; }
	| SET LT typeName=typeIdentUse GT { xg.append("set<"+typeName+">"); } e2=seqInitSetExpr[xg, SetTypeNode.getSetType(typeName)] { res = e2; }
	| ARRAY LT typeName=typeIdentUse GT { xg.append("array<"+typeName+">"); } e3=seqInitArrayExpr[xg, ArrayTypeNode.getArrayType(typeName)] { res = e3; }
	| DEQUE LT typeName=typeIdentUse GT { xg.append("deque<"+typeName+">"); } e4=seqInitDequeExpr[xg, DequeTypeNode.getDequeType(typeName)] { res = e4; }
	;

seqInitMapExpr [ExecNode xg, MapTypeNode mapType] returns [ MapInitNode res = null ]
	: l=LBRACE { xg.append("{"); } { res = new MapInitNode(getCoords(l), null, mapType); }
		( item1=seqMapItem[xg] { res.addMapItem(item1); }
			( COMMA { xg.append(","); } item2=seqMapItem[xg] { res.addMapItem(item2); } )*
		)?
	  RBRACE { xg.append("}"); }
	;

seqInitSetExpr [ExecNode xg, SetTypeNode setType] returns [ SetInitNode res = null ]
	: l=LBRACE { xg.append("{"); } { res = new SetInitNode(getCoords(l), null, setType); }	
		( item1=seqSetItem[xg] { res.addSetItem(item1); }
			( COMMA { xg.append(","); } item2=seqSetItem[xg] { res.addSetItem(item2); } )*
		)?
	  RBRACE { xg.append("}"); }
	;

seqInitArrayExpr [ExecNode xg, ArrayTypeNode arrayType] returns [ ArrayInitNode res = null ]
	: l=LBRACK { xg.append("["); } { res = new ArrayInitNode(getCoords(l), null, arrayType); }	
		( item1=seqArrayItem[xg] { res.addArrayItem(item1); }
			( COMMA { xg.append(","); } item2=seqArrayItem[xg] { res.addArrayItem(item2); } )*
		)?
	  RBRACK { xg.append("]"); }
	;

seqInitDequeExpr [ExecNode xg, DequeTypeNode dequeType] returns [ DequeInitNode res = null ]
	: l=RBRACK { xg.append("]"); } { res = new DequeInitNode(getCoords(l), null, dequeType); }	
		( item1=seqDequeItem[xg] { res.addDequeItem(item1); }
			( COMMA { xg.append(","); } item2=seqDequeItem[xg] { res.addDequeItem(item2); } )*
		)?
	  LBRACK { xg.append("["); }
	;

seqMapItem [ExecNode xg] returns [ MapItemNode res = null ]
	: key=seqExpression[xg] a=RARROW { xg.append("->"); } value=seqExpression[xg]
		{
			res = new MapItemNode(getCoords(a), key, value);
		}
	;

seqSetItem [ExecNode xg] returns [ SetItemNode res = null ]
	: value=seqExpression[xg]
		{
			res = new SetItemNode(value.getCoords(), value);
		}
	;

seqArrayItem [ExecNode xg] returns [ ArrayItemNode res = null ]
	: value=seqExpression[xg]
		{
			res = new ArrayItemNode(value.getCoords(), value);
		}
	;

seqDequeItem [ExecNode xg] returns [ DequeItemNode res = null ]
	: value=seqExpression[xg]
		{
			res = new DequeItemNode(value.getCoords(), value);
		}
	;
	
parallelCallRule[ExecNode xg, CollectNode<BaseNode> returns]
	: ( LPAREN {xg.append("(");} xgrsVariableList[xg, returns] RPAREN ASSIGN {xg.append(")=");} )?
		(	( DOLLAR {xg.append("$");} ( xgrsVarUse[xg] 
						(COMMA {xg.append(",");} (xgrsVarUse[xg] | STAR {xg.append("*");}))? )? )?
				LBRACK {xg.append("[");} 
				callRule[xg, returns, true]
				RBRACK {xg.append("]");}
		| 
			COUNT {xg.append("count");}
				LBRACK {xg.append("[");} 
				callRule[xg, returns, true]
				RBRACK {xg.append("]");}
		|
			callRule[xg, returns, false]
		)
	;
		
callRule[ExecNode xg, CollectNode<BaseNode> returns, boolean isAllBracketed]
	@init{
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
		CollectNode<IdentNode> filters = new CollectNode<IdentNode>();
	}
	
	: ( | MOD { xg.append("\%"); } | MOD QUESTION { xg.append("\%?"); } | QUESTION { xg.append("?"); } | QUESTION MOD { xg.append("?\%"); } )
		(xgrsVarUse[xg] DOT {xg.append(".");})?
		id=actionIdentUse {xg.append(id);}
		(LPAREN {xg.append("(");} (ruleParams[xg, params])? RPAREN {xg.append(")");})?
		(callRuleFilter[xg, filters])*
		{
			// TODO: there may be more than one user-defined filter be given (that should be checked in the call action node, just postponed because unlikely)
			xg.addCallAction(new CallActionNode(id.getCoords(), id, params, returns, filters, isAllBracketed));
		}
	;

callRuleFilter[ExecNode xg, CollectNode<IdentNode> filters]
options { k = 5; }
	@init{
		boolean inPackage = false;
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
	}
	: BACKSLASH { xg.append("\\"); } (p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); inPackage = true; })? 
		(filterId=IDENT | filterId=AUTO) { xg.append(filterId.getText()); } 
		(LPAREN {xg.append("(");} (ruleParams[xg, params])? RPAREN {xg.append(")");})?
			{
				if(filterId.getText().equals("keepFirst") || filterId.getText().equals("keepLast")
					|| filterId.getText().equals("removeFirst") || filterId.getText().equals("removeLast")
					|| filterId.getText().equals("keepFirstFraction") || filterId.getText().equals("keepLastFraction")
					|| filterId.getText().equals("removeFirstFraction") || filterId.getText().equals("removeLastFraction"))
				{
					if(params.size()!=1)
						reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 1 arguments.");
				}
				else if(filterId.getText().equals("auto"))
				{
					if(params.size()!=0)
						reportError(getCoords(filterId), "The filter " + filterId.getText() + " expects 0 arguments.");
				}
				else
				{
					filters.addChild(inPackage ? 
						new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
							env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId))) :
						new IdentNode(env.occurs(ParserEnvironment.ACTIONS, filterId.getText(), getCoords(filterId)))
						);
				}
			}
	| BACKSLASH { xg.append("\\"); } (p=IDENT DOUBLECOLON { xg.append(p.getText()); xg.append("::"); })?
		filterBase=IDENT LT filterVariable=IDENT GT 
		{
			if(!filterBase.getText().equals("orderAscendingBy") && !filterBase.getText().equals("orderDescendingBy") && !filterBase.getText().equals("groupBy")
				&& !filterBase.getText().equals("keepSameAsFirst") && !filterBase.getText().equals("keepSameAsLast") && !filterBase.getText().equals("keepOneForEach"))
					reportError(getCoords(filterBase), "Unknown def-variable-based filter " + filterBase.getText() + "! Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach.");
			else
					xg.append(filterBase.getText() + "<" + filterVariable.getText() + "> ");
		}
	;

ruleParam[ExecNode xg, CollectNode<BaseNode> parameters]
	: exp=seqExpression[xg] { parameters.addChild(exp); }
	;

ruleParams[ExecNode xg, CollectNode<BaseNode> parameters]
	: ruleParam[xg, parameters]	( COMMA {xg.append(",");} ruleParam[xg, parameters] )*
	;

xgrsVariableList[ExecNode xg, CollectNode<BaseNode> res]
	: child=xgrsEntity[xg] { res.addChild(child); }
		( COMMA { xg.append(","); } child=xgrsEntity[xg] { res.addChild(child); } )*
	;

xgrsVarUse[ExecNode xg] returns [BaseNode res = null]
	:
		id=entIdentUse // var of node, edge, or basic type
		{ res = id; xg.append(id); xg.addUsage(id); } 
	|
		DOUBLECOLON id=entIdentUse // global var of node, edge, or basic type
		{ res = id; xg.append("::" + id); xg.addUsage(id); } 
	;

xgrsEntity[ExecNode xg] returns [BaseNode res = null]
	:
		varUse=xgrsVarUse[xg] 
		{ res = varUse; }
	|
		(IDENT COLON | MINUS IDENT COLON) => xgrsVarDecl=xgrsEntityDecl[xg, true]
		{ res = xgrsVarDecl; }
	;

xgrsEntityDecl[ExecNode xg, boolean emit] returns [ExecVarDeclNode res = null]
options { k = *; }
	:
		id=entIdentDecl COLON type=typeIdentUse // node decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, type);
			if(emit) xg.append(id.toString()+":"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=entIdentDecl COLON MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT // map decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, MapTypeNode.getMapType(keyType, valueType));
			if(emit) xg.append(id.toString()+":map<"+keyType.toString()+","+valueType.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(entIdentDecl COLON MAP LT typeIdentUse COMMA typeIdentUse GE) =>
		id=entIdentDecl COLON MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse // map decl; special to save user from splitting map<S,T>=x to map<S,T> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, MapTypeNode.getMapType(keyType, valueType));
			if(emit) xg.append(id.toString()+":map<"+keyType.toString()+","+valueType.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=entIdentDecl COLON SET LT type=typeIdentUse GT // set decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, SetTypeNode.getSetType(type));
			if(emit) xg.append(id.toString()+":set<"+type.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(entIdentDecl COLON SET LT typeIdentUse GE) => 
		id=entIdentDecl COLON SET LT type=typeIdentUse // set decl; special to save user from splitting set<S>=x to set<S> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, SetTypeNode.getSetType(type));
			if(emit) xg.append(id.toString()+":set<"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=entIdentDecl COLON ARRAY LT type=typeIdentUse GT // array decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, ArrayTypeNode.getArrayType(type));
			if(emit) xg.append(id.toString()+":array<"+type.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(entIdentDecl COLON ARRAY LT typeIdentUse GE) => 
		id=entIdentDecl COLON ARRAY LT type=typeIdentUse // array decl; special to save user from splitting array<S>=x to array<S> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, ArrayTypeNode.getArrayType(type));
			if(emit) xg.append(id.toString()+":array<"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=entIdentDecl COLON DEQUE LT type=typeIdentUse GT // deque decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, DequeTypeNode.getDequeType(type));
			if(emit) xg.append(id.toString()+":deque<"+type.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		(entIdentDecl COLON DEQUE LT typeIdentUse GE) => 
		id=entIdentDecl COLON DEQUE LT type=typeIdentUse // deque decl; special to save user from splitting deque<S>=x to deque<S> =x as >= is GE not GT ASSIGN
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, DequeTypeNode.getDequeType(type));
			if(emit) xg.append(id.toString()+":deque<"+type.toString());
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		id=entIdentDecl COLON MATCH LT type=actionIdentUse GT // match decl
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, MatchTypeNode.getMatchType(type));
			if(emit) xg.append(id.toString()+":match<"+type.toString()+">");
			xg.addVarDecl(decl);
			res = decl;
		}
	|
		MINUS id=entIdentDecl COLON type=typeIdentUse RARROW // edge decl, interpreted grs don't use -:-> form
		{
			ExecVarDeclNode decl = new ExecVarDeclNode(id, type);
			if(emit) xg.append(decl.getIdentNode().getIdent() + ":" + decl.typeUnresolved);
			xg.addVarDecl(decl);
			res = decl;
		}
	;

xgrsIndex[ExecNode xg]
	: id=indexIdentUse { xg.append(id.toString()); }
	;

	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Base  --- copied from GrGen.g as needed here, I don't know what abominations will arise if they differ
////////////////////////////////////////////////////////////////////////////////////////////////////////////////


memberIdent returns [ Token t = null ]
	: i=IDENT { t = i; }
	| r=REPLACE { r.setType(IDENT); t = r; }             // HACK: For string replace function... better choose another name?
	; 

entIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
//		( annots=annotations { res.setAnnotations(annots); } )?
	;

/////////////////////////////////////////////////////////
// Identifier usages, it is checked, whether the identifier is declared.
// The IdentNode created by the definition is returned.
// Don't factor the common stuff into "identUse", that pollutes the follow sets

typeIdentUse returns [ IdentNode res = env.getDummyIdent() ]
options { k = 3; }
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i!=null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i))); }
	;

rhsIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.REPLACES, i.getText(), getCoords(i))); }
	;

entIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	;

actionIdentUse returns [ IdentNode res = env.getDummyIdent() ]
options { k = 3; }
	: i=IDENT
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i!=null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
	;

altIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ALTERNATIVES, i.getText(), getCoords(i))); }
	;

iterIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ITERATEDS, i.getText(), getCoords(i))); }
	;

negIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.NEGATIVES, i.getText(), getCoords(i))); }
	;

idptIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.INDEPENDENTS, i.getText(), getCoords(i))); }
	;

patIdentUse returns [ IdentNode res = env.getDummyIdent() ]
options { k = 3; }
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.PATTERNS, i.getText(), getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i!=null) res = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)), 
				env.occurs(ParserEnvironment.PATTERNS, i.getText(), getCoords(i))); }
	;

funcOrExtFuncIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i))); }
	;

indexIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
	{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.INDICES, i.getText(), getCoords(i))); }
	;

identList [ Collection<String> strings ]
	: fid=IDENT { strings.add(fid.getText()); }
		( COMMA sid=IDENT { strings.add(sid.getText()); } )*
	;

memberIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=memberIdent
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	;

	
	//////////////////////////////////////////
// Range Spec
//////////////////////////////////////////

rangeSpecXgrsLoop returns [ RangeSpecNode res = null ]
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
				lower=integerConstXgrs
				(
					COLON ( STAR { upper=RangeSpecNode.UNBOUND; } | upper=integerConstXgrs )
				|
					{ upper = lower; }
				)
			)
			RBRACK
		)?
		{ res = new RangeSpecNode(coords, lower, upper); }
	;

integerConstXgrs returns [ long value = 0 ]
	: i=NUM_INTEGER
		{ value = Long.parseLong(i.getText()); }
	;


