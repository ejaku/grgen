/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */
 
/*
 * GrGen model and rule specification language grammar for ANTLR 3
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski, Veit Batz, Edgar Jakumeit, Sebastian Buchwald, Moritz Kroll, Peter Gr�ner
*/

grammar GrGen;

options {
	k = 2;
}

import EmbeddedExec;

tokens {
	NUM_BYTE;
	NUM_SHORT;
	NUM_INTEGER;
	NUM_LONG;
	NUM_FLOAT;
	NUM_DOUBLE;
}

// todo: use scopes for the variables passed through numerous parsing rules as e.g. context
// should simplify grammar a good deal / eliminate a lot of explicit parameter passing
//scope Context {
//	int context;
//	PatternGraphNode directlyNestingLHSGraph;
//}
// todo: maybe user other features of antlr 3?

@lexer::header {
	package de.unika.ipd.grgen.parser.antlr;

	import java.io.File;
	import java.io.IOException;
}

@lexer::members {
	GRParserEnvironment env;

	void setEnv(GRParserEnvironment env) {
		this.env = env;
	}
  
	// overriden for handling EOF of included file
	public Token nextToken() {
		Token token = super.nextToken();

		if(token.getType()==Token.EOF) {
			if(env.popFile(this)) {
				token = this.nextToken();
			}
		}

		// Skip first token after switching to another input.
		int startIndex = ((CommonToken)token).getStartIndex();
		if(startIndex < 0) {
			token = this.nextToken();
		}
			
		return token;
	}
}

@header {
	package de.unika.ipd.grgen.parser.antlr;
	
	import java.util.Iterator;
	import java.util.LinkedList;
	import java.util.Collection;
	
	import java.io.File;

	import de.unika.ipd.grgen.parser.*;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.ast.exprevals.*;
	import de.unika.ipd.grgen.ast.containers.*;
	import de.unika.ipd.grgen.util.*;
}

@members {
	boolean hadError = false;

	private static Map<Integer, Integer> opIds = new HashMap<Integer, Integer>();

	private static void putOpId(int tokenId, int opId) {
		opIds.put(new Integer(tokenId), new Integer(opId));
	}

	static {
		putOpId(QUESTION, OperatorSignature.COND);
		putOpId(EQUAL, OperatorSignature.EQ);
		putOpId(NOT_EQUAL, OperatorSignature.NE);
		putOpId(STRUCTURAL_EQUAL, OperatorSignature.SE);
		putOpId(NOT, OperatorSignature.LOG_NOT);
		putOpId(TILDE, OperatorSignature.BIT_NOT);
		putOpId(SL, OperatorSignature.SHL);
		putOpId(SR, OperatorSignature.SHR);
		putOpId(BSR, OperatorSignature.BIT_SHR);
		putOpId(DIV, OperatorSignature.DIV);
		putOpId(STAR, OperatorSignature.MUL);
		putOpId(MOD, OperatorSignature.MOD);
		putOpId(PLUS, OperatorSignature.ADD);
		putOpId(MINUS, OperatorSignature.SUB);
		putOpId(GE, OperatorSignature.GE);
		putOpId(GT, OperatorSignature.GT);
		putOpId(LE, OperatorSignature.LE);
		putOpId(LT, OperatorSignature.LT);
		putOpId(BAND, OperatorSignature.BIT_AND);
		putOpId(BOR, OperatorSignature.BIT_OR);
		putOpId(BXOR, OperatorSignature.BIT_XOR);
		putOpId(BXOR, OperatorSignature.BIT_XOR);
		putOpId(LAND, OperatorSignature.LOG_AND);
		putOpId(LOR, OperatorSignature.LOG_OR);
		putOpId(IN, OperatorSignature.IN);
		putOpId(BACKSLASH, OperatorSignature.EXCEPT);
	};

	public OpNode makeOp(org.antlr.runtime.Token t) {
		Integer opId = opIds.get(new Integer(t.getType()));
		assert opId != null : "Invalid operator ID";
		return new ArithmeticOpNode(getCoords(t), opId.intValue());
	}

	public OpNode makeBinOp(org.antlr.runtime.Token t, ExprNode op0, ExprNode op1) {
		OpNode res = makeOp(t);
		res.addChild(op0);
		res.addChild(op1);
		return res;
	}

	public OpNode makeUnOp(org.antlr.runtime.Token t, ExprNode op) {
		OpNode res = makeOp(t);
		res.addChild(op);
		return res;
	}

	protected ParserEnvironment env;

	public void setEnv(ParserEnvironment env) {
		this.env = env;
		gEmbeddedExec.env = env;
	}

	protected Coords getCoords(org.antlr.runtime.Token tok) {
		return new Coords(tok);
	}

	protected final void reportError(de.unika.ipd.grgen.parser.Coords c, String s) {
		hadError = true;
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

	public boolean hadError() {
		return hadError;
	}

	public String getFilename() {
		return env.getFilename();
	}
}



////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Actions and Patterns
////////////////////////////////////////////////////////////////////////////////////////////////////////////////



/**
 * Build a main node.
 * It has a collect node with the decls as child
 */
textActions returns [ UnitNode main = null ]
	@init{
		CollectNode<ModelNode> modelChilds = new CollectNode<ModelNode>();
		CollectNode<IdentNode> packages = new CollectNode<IdentNode>();
		CollectNode<IdentNode> patternChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> actionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> filterChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> functionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> procedureChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> sequenceChilds = new CollectNode<IdentNode>();
		String actionsName = Util.getActionsNameFromFilename(getFilename());
		if(!Util.isFilenameValidActionName(getFilename())) {
			reportError(new de.unika.ipd.grgen.parser.Coords(), "the filename "+getFilename()+" can't be used as action name, must be similar to an identifier");
		}
	}

	: ( usingDecl[modelChilds] )*
	
       ( globalVarDecl
       | ( pack=packageActionDecl { packages.addChild(pack); } )
       | (declsPatternMatchingOrAttributeEvaluationUnitWithModifier[
                       patternChilds, actionChilds, filterChilds, functionChilds, procedureChilds, sequenceChilds])
       )*
       EOF
		{
			if(modelChilds.getChildren().size() == 0)
				modelChilds.addChild(env.getStdModel());
			else if(modelChilds.getChildren().size() > 1) {
				//
				// If more than one model is specified, generate a new graph model
				// using the name of the grg-file containing all given models.
				//
				IdentNode id = new IdentNode(env.define(ParserEnvironment.ENTITIES, actionsName,
					modelChilds.getCoords()));
				boolean isEmitClassDefined = false;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isEmitClassDefined |= modelChild.IsEmitClassDefined();
				}
				boolean isEmitGraphClassDefined = false;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isEmitGraphClassDefined |= modelChild.IsEmitGraphClassDefined();
				}
				boolean isCopyClassDefined = false;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isCopyClassDefined |= modelChild.IsCopyClassDefined();
				}
				boolean isEqualClassDefined = false;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isEqualClassDefined |= modelChild.IsEqualClassDefined();
				}				
				boolean isLowerClassDefined = false;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isLowerClassDefined |= modelChild.IsLowerClassDefined();
				}
				boolean isUniqueDefined = false;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isUniqueDefined |= modelChild.IsUniqueDefined();
				}				
				boolean isUniqueIndexDefined = false;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isUniqueIndexDefined |= modelChild.IsUniqueIndexDefined();
				}				
				int isoParallel = 0;
				for(ModelNode modelChild : modelChilds.getChildren()) {
					isoParallel = Math.max(isoParallel, modelChild.IsoParallel());
				}
				ModelNode model = new ModelNode(id, new CollectNode<IdentNode>(),
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(), 
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(), modelChilds, 
						isEmitClassDefined, isEmitGraphClassDefined, isCopyClassDefined, 
						isEqualClassDefined, isLowerClassDefined,
						isUniqueDefined, isUniqueIndexDefined,
						isoParallel);
				modelChilds = new CollectNode<ModelNode>();
				modelChilds.addChild(model);
			}
			main = new UnitNode(actionsName, getFilename(), env.getStdModel(), 
								modelChilds, patternChilds, actionChilds, filterChilds,
								functionChilds, procedureChilds,
								sequenceChilds, packages);
		}
	;

usingDecl [ CollectNode<ModelNode> modelChilds ]
	options { k = 1; }
	@init{ Collection<String> modelNames = new LinkedList<String>(); }

	: u=USING identList[modelNames]
		{
			modelChilds.setCoords(getCoords(u));
			for(Iterator<String> it = modelNames.iterator(); it.hasNext();)
			{
				String modelName = it.next();
				File modelFile = env.findModel(modelName);
				if ( modelFile == null ) {
					reportError(getCoords(u), "model \"" + modelName + "\" could not be found");
				} else {
					ModelNode model;
					model = env.parseModel(modelFile);
					modelChilds.addChild(model);
				}
			}
		}
		SEMI // don't move before the semantic action, this would cause a following include to be processed before the using of the model
	| h=HASHUSING s=STRING_LITERAL
		{
			modelChilds.setCoords(getCoords(h));
			String modelName = s.getText();
			modelName = modelName.substring(1,modelName.length()-1);
			File modelFile = env.findModel(modelName);
			if ( modelFile == null ) {
				reportError(getCoords(h), "model \"" + modelName + "\" could not be found");
			} else {
				ModelNode model;
				model = env.parseModel(modelFile);
				modelChilds.addChild(model);
			}
		}
	;

globalVarDecl 
	: DOUBLECOLON id=entIdentDecl COLON type=typeIdentUse SEMI
		{
			id.setDecl(new NodeDeclNode(id, type, false, 0, TypeExprNode.getEmpty(), null));
		}
	| MINUS DOUBLECOLON id=entIdentDecl COLON type=typeIdentUse (RARROW | MINUS) SEMI
		{
			id.setDecl(new EdgeDeclNode(id, type, false, 0, TypeExprNode.getEmpty(), null));
		}
	| modifier=IDENT DOUBLECOLON id=entIdentDecl COLON 
		(
			type=typeIdentUse
			{
				id.setDecl(new VarDeclNode(id, type, null, 0));
				if(!modifier.getText().equals("var")) 
					{ reportError(getCoords(modifier), "var keyword needed before non graph element and non container global variable"); }
			}
		|
			MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				id.setDecl(new VarDeclNode(id, MapTypeNode.getMapType(keyType, valueType), null, 0));
				if(!modifier.getText().equals("ref"))
					{ reportError(getCoords(modifier), "ref keyword needed before map global variable"); }
			}
		|
			SET LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				id.setDecl(new VarDeclNode(id, SetTypeNode.getSetType(keyType), null, 0));
				if(!modifier.getText().equals("ref"))
					{ reportError(getCoords(modifier), "ref keyword needed before set global variable"); }
			}
		|
			ARRAY LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				id.setDecl(new VarDeclNode(id, ArrayTypeNode.getArrayType(keyType), null, 0));
				if(!modifier.getText().equals("ref"))
					{ reportError(getCoords(modifier), "ref keyword needed before array global variable"); }
			}
		|
			DEQUE LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				id.setDecl(new VarDeclNode(id, DequeTypeNode.getDequeType(keyType), null, 0));
				if(!modifier.getText().equals("ref"))
					{ reportError(getCoords(modifier), "ref keyword needed before deque global variable"); }
			}
		)
		SEMI
	;

packageActionDecl returns [ IdentNode res = env.getDummyIdent() ]
	@init{
		CollectNode<IdentNode> patternChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> actionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> sequenceChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> functionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> procedureChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> filterChilds = new CollectNode<IdentNode>();
	}
	: PACKAGE id=packageIdentDecl LBRACE { env.pushScope(id); }
               ( declsPatternMatchingOrAttributeEvaluationUnitWithModifier[
			patternChilds, actionChilds, filterChilds, functionChilds, procedureChilds, sequenceChilds]
               )*
	  RBRACE
	  {
			PackageActionTypeNode pt = new PackageActionTypeNode(patternChilds, actionChilds, filterChilds,
				functionChilds, procedureChilds, sequenceChilds);
			id.setDecl(new TypeDeclNode(id, pt));
			res = id;
	  }
	  { env.popScope(); }
	;

declsPatternMatchingOrAttributeEvaluationUnitWithModifier [ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds,
													CollectNode<IdentNode> filterChilds, CollectNode<IdentNode> functionChilds, 
													CollectNode<IdentNode> procedureChilds, CollectNode<IdentNode> sequenceChilds ]
	@init{ mod = 0; }
	: ( mod=patternModifiers declPatternMatchingOrAttributeEvaluationUnit[
			patternChilds, actionChilds, filterChilds, functionChilds, procedureChilds, sequenceChilds, mod]
         )
	;

patternModifiers returns [ int res = 0 ]
	: ( m=patternModifier[ res ]  { res = m; } )*
	;

patternModifier [ int mod ] returns [ int res = 0 ]
	: modifier=INDUCED
		{
			if((mod & PatternGraphNode.MOD_INDUCED)!=0) {
				reportError(getCoords(modifier), "\"induced\" modifier already declared");
			}
			res = mod | PatternGraphNode.MOD_INDUCED;
		}
	| modifier=EXACT
		{		
			if((mod & PatternGraphNode.MOD_EXACT)!=0) {
				reportError(getCoords(modifier), "\"exact\" modifier already declared");
			}
			res = mod | PatternGraphNode.MOD_EXACT;
		}
	| modifier=IDENT 
		{
			if(modifier.getText().equals("dpo")) {
				if((mod & PatternGraphNode.MOD_DANGLING)!=0 || (mod & PatternGraphNode.MOD_IDENTIFICATION)!=0) {
					reportError(getCoords(modifier), "\"dpo\" or \"dangling\" or \"identification\" modifier dangling already declared");
				}
				res = mod | PatternGraphNode.MOD_DANGLING | PatternGraphNode.MOD_IDENTIFICATION;
			} else if(modifier.getText().equals("dangling")) {
				if((mod & PatternGraphNode.MOD_DANGLING)!=0) {
					reportError(getCoords(modifier), "\"dangling\" modifier already declared");
				}
				res = mod | PatternGraphNode.MOD_DANGLING;
			} else if(modifier.getText().equals("identification")) {
				if((mod & PatternGraphNode.MOD_IDENTIFICATION)!=0) {
					reportError(getCoords(modifier), "\"identification\" modifier already declared");
				}
				res = mod | PatternGraphNode.MOD_IDENTIFICATION;
			} else {
				reportError(getCoords(modifier), "unknown modifier "+modifier.getText());
			}
		}
	;

declPatternMatchingOrAttributeEvaluationUnit [ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds, 
											 CollectNode<IdentNode> filterChilds, CollectNode<IdentNode> functionChilds,
											 CollectNode<IdentNode> procedureChilds, CollectNode<IdentNode> sequenceChilds,
											 int mod ]
	@init{
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<RhsDeclNode> rightHandSides = new CollectNode<RhsDeclNode>();
		CollectNode<BaseNode> modifyParams = new CollectNode<BaseNode>();
		CollectNode<BaseNode> retTypes = new CollectNode<BaseNode>();
		CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>();
		ExecNode exec = null;
		AnonymousScopeNamer namer = new AnonymousScopeNamer(env);
		TestDeclNode actionDecl = null;
	}

	: t=TEST id=actionIdentDecl { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS|BaseNode.CONTEXT_PARAMETER, null] 
		ret=returnTypes LBRACE
		left=patternBody[getCoords(t), params, namer, mod, BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.toString()]
			{
				actionDecl = new TestDeclNode(id, left, ret);
				id.setDecl(actionDecl);
				actionChilds.addChild(id);
			}
		RBRACE { env.popScope(); }
		{
			if((mod & PatternGraphNode.MOD_DANGLING)!=0 || (mod & PatternGraphNode.MOD_IDENTIFICATION)!=0) {
				reportError(getCoords(t), "no \"dpo\" or \"dangling\" or \"identification\" modifier allowed for test");
			}
		}
		filterDecls[id, actionDecl]
	| r=RULE id=actionIdentDecl { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS|BaseNode.CONTEXT_PARAMETER, null]
		ret=returnTypes LBRACE
		left=patternBody[getCoords(r), params, namer, mod, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.toString()]
		( rightReplace=replacePart[new CollectNode<BaseNode>(), namer, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				actionDecl = new RuleDeclNode(id, left, rightReplace, ret);
				id.setDecl(actionDecl);
				actionChilds.addChild(id);
			}
		| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				actionDecl = new RuleDeclNode(id, left, rightModify, ret);
				id.setDecl(actionDecl);
				actionChilds.addChild(id);
			}
		| emptyRightModify=emptyModifyPart[getCoords(r), dels, new CollectNode<BaseNode>(), BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				actionDecl = new RuleDeclNode(id, left, emptyRightModify, ret);
				id.setDecl(actionDecl);
				actionChilds.addChild(id);
			}
		)
		RBRACE { env.popScope(); }
		filterDecls[id, actionDecl]
	| p=PATTERN id=patIdentDecl { env.pushScope(id); } params=patternParameters[BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS|BaseNode.CONTEXT_PARAMETER, null] 
		((MODIFY|REPLACE) mp=patternParameters[BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS|BaseNode.CONTEXT_PARAMETER, null] { modifyParams = mp; })?
		LBRACE
		left=patternBody[getCoords(p), params, namer, mod, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS, id.toString()]
		( rightReplace=replacePart[modifyParams, namer, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id, left]
			{
				rightHandSides.addChild(rightReplace);
			}
		| rightModify=modifyPart[dels, modifyParams, namer, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id, left]
			{
				rightHandSides.addChild(rightModify);
			}
		)*
			{
				id.setDecl(new SubpatternDeclNode(id, left, rightHandSides));
				patternChilds.addChild(id);
			}
		RBRACE { env.popScope(); }
	| s=SEQUENCE id=actionIdentDecl { env.pushScope(id); } { exec = new ExecNode(getCoords(s)); }
		inParams=execInParameters[exec] outParams=execOutParameters[exec]
		LBRACE 
			xgrs[exec]
		RBRACE { env.popScope(); }
		{
			id.setDecl(new SequenceDeclNode(id, exec, inParams, outParams));
			sequenceChilds.addChild(id);
		}
	| EXTERNAL s=SEQUENCE id=actionIdentDecl { env.pushScope(id); } { exec = new ExecNode(getCoords(s)); }
		inParams=execInParameters[exec] outParams=execOutParameters[exec]
		SEMI { env.popScope(); }
		{
			id.setDecl(new SequenceDeclNode(id, exec, inParams, outParams));
			sequenceChilds.addChild(id);
		}
	| f=FUNCTION id=funcOrExtFuncIdentDecl { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphNode.getInvalid()]
		COLON retType=returnType
		LBRACE
			( c=computation[false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphNode.getInvalid()] { evals.addChild(c); } )*
		RBRACE { env.popScope(); }
		{
			id.setDecl(new FunctionDeclNode(id, evals, params, retType, false));
			functionChilds.addChild(id);
		}
	| pr=PROCEDURE id=funcOrExtFuncIdentDecl { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, PatternGraphNode.getInvalid()]
		(COLON LPAREN (returnTypeList[retTypes])? RPAREN)?
		LBRACE
			( c=computation[false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, PatternGraphNode.getInvalid()] { evals.addChild(c); } )*
		RBRACE { env.popScope(); }
		{
			id.setDecl(new ProcedureDeclNode(id, evals, params, retTypes, false));
			procedureChilds.addChild(id);
		}
	| f=FILTER id=actionIdentDecl LT actionId=actionIdentUse GT { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphNode.getInvalid()]
		LBRACE
			{
				evals.addChild(new DefDeclStatementNode(getCoords(f), new VarDeclNode(
						new IdentNode(env.define(ParserEnvironment.ENTITIES, "this", getCoords(f))), ArrayTypeNode.getArrayType(new IdentNode(env.occurs(ParserEnvironment.ACTIONS, actionId.toString(), actionId.getCoords()))), PatternGraphNode.getInvalid(), BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, true),
					BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION));
			}
			( c=computation[false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphNode.getInvalid()] { evals.addChild(c); } )*
		RBRACE { env.popScope(); }
		{
			FilterFunctionDeclNode ff = new FilterFunctionDeclNode(id, evals, params, actionId);
			id.setDecl(ff);
			filterChilds.addChild(id);
		}
	| EXTERNAL f=FILTER id=actionIdentDecl LT actionId=actionIdentUse GT { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphNode.getInvalid()]
		SEMI { env.popScope(); }
		{
			FilterFunctionDeclNode ff = new FilterFunctionDeclNode(id, null, params, actionId);
			id.setDecl(ff);
			filterChilds.addChild(id);
		} 
	;

parameters [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN (paramList[res, context, directlyNestingLHSGraph])? RPAREN
	|
	;

patternParameters [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN (patternParamList[res, context, directlyNestingLHSGraph])? RPAREN
	|
	;

paramList [ CollectNode<BaseNode> params, int context, PatternGraphNode directlyNestingLHSGraph ]
	: p=param[context, directlyNestingLHSGraph] { params.addChild(p); }
		( COMMA p=param[context, directlyNestingLHSGraph] { params.addChild(p); } )*
	;

patternParamList [ CollectNode<BaseNode> params, int context, PatternGraphNode directlyNestingLHSGraph ]
	: (p=param[context, directlyNestingLHSGraph] { params.addChild(p); } | dp=defEntityToBeYieldedTo[null, null, context, directlyNestingLHSGraph] { params.addChild(dp); })
		( COMMA (p=param[context, directlyNestingLHSGraph] { params.addChild(p); } | dp=defEntityToBeYieldedTo[null, null, context, directlyNestingLHSGraph] { params.addChild(dp); }) )*
	;

param [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	: MINUS edge=edgeDeclParam[context, directlyNestingLHSGraph] direction = forwardOrUndirectedEdgeParam
		{
			BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
			res = new ConnectionNode(dummy, edge, dummy, direction, ConnectionNode.NO_REDIRECTION);
		}
	| LARROW edge=edgeDeclParam[context, directlyNestingLHSGraph] RARROW
		{
			BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
			res = new ConnectionNode(dummy, edge, dummy, ConnectionNode.ARBITRARY_DIRECTED, ConnectionNode.NO_REDIRECTION);
		}
	| QUESTIONMINUS edge=edgeDeclParam[context, directlyNestingLHSGraph] MINUSQUESTION
		{
			BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
			res = new ConnectionNode(dummy, edge, dummy, ConnectionNode.ARBITRARY, ConnectionNode.NO_REDIRECTION);
		}
	| v=varDecl[context, directlyNestingLHSGraph] 
		{
			res = v;
		}
	| node=nodeDeclParam[context, directlyNestingLHSGraph]
		{
			res = new SingleNodeConnNode(node);
		}
	;

forwardOrUndirectedEdgeParam returns [ int res = ConnectionNode.ARBITRARY ]
	: RARROW { res = ConnectionNode.DIRECTED; }
	| MINUS  { res = ConnectionNode.UNDIRECTED; }
	;

returnTypes returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: COLON LPAREN (returnTypeList[res])? RPAREN
	|
	;

returnTypeList [ CollectNode<BaseNode> returnTypes ]
	: t=returnType { returnTypes.addChild(t); } ( COMMA t=returnType { returnTypes.addChild(t); } )*
	;

returnType returns [ BaseNode res = env.initNode() ]
	:	type=typeIdentUse
		{
			res = type;
		}
	|
		MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT
		{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
			res = MapTypeNode.getMapType(keyType, valueType);
		}
	|
		SET LT keyType=typeIdentUse GT
		{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
			res = SetTypeNode.getSetType(keyType);
		}
	|
		ARRAY LT keyType=typeIdentUse GT
		{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
			res = ArrayTypeNode.getArrayType(keyType);
		}
	|
		DEQUE LT keyType=typeIdentUse GT
		{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
			res = DequeTypeNode.getDequeType(keyType);
		}
	;

filterDecls [ IdentNode actionIdent, TestDeclNode actionDecl ]
	@init {
		ArrayList<FilterCharacter> filters = new ArrayList<FilterCharacter>();
		actionDecl.addFilters(filters);
	}
	: BACKSLASH filterDeclList[actionIdent, filters]
	|
	;

filterDeclList [ IdentNode actionIdent, ArrayList<FilterCharacter> filters ]
	: (EXTERNAL id=actionIdentDecl params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphNode.getInvalid()]
		{
			FilterFunctionDeclNode ff = new FilterFunctionDeclNode(id, null, params, actionIdent);
			filters.add(ff);
			id.setDecl(ff);
		} 
	| a=AUTO
		{
			filters.add(new FilterAutoGeneratedNode(getCoords(a), "auto", null, actionIdent));
		}
	| filterBase=IDENT LT fvl=filterVariableList GT 
		{
			filters.add(new FilterAutoGeneratedNode(getCoords(filterBase), filterBase.getText(), fvl, actionIdent));
		}
	)
	(
		filterDeclListContinuation [ actionIdent, filters ]
	)*
	;

filterDeclListContinuation [ IdentNode actionIdent, ArrayList<FilterCharacter> filters ]
options { k = 3; }
	: COMMA EXTERNAL id=actionIdentDecl params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphNode.getInvalid()]
		{ 
			FilterFunctionDeclNode ff = new FilterFunctionDeclNode(id, null, params, actionIdent);
			filters.add(ff);
			id.setDecl(ff);
		}
	| COMMA a=AUTO
		{
			filters.add(new FilterAutoGeneratedNode(getCoords(a), "auto", null, actionIdent));
		}
	| COMMA filterBase=IDENT LT fvl=filterVariableList GT
		{
			filters.add(new FilterAutoGeneratedNode(getCoords(filterBase), filterBase.getText(), fvl, actionIdent));
		}
	;

filterVariableList returns [ ArrayList<String> filterVariables = new ArrayList<String>() ]
	: filterVar=IDENT { filterVariables.add(filterVar.getText()); } ( COMMA filterVar=IDENT { filterVariables.add(filterVar.getText()); } )*
	;
	
replacePart [ CollectNode<BaseNode> params, AnonymousScopeNamer namer,
			int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ]
			returns [ ReplaceDeclNode res = null ]
	: r=REPLACE ( id=rhsIdentDecl { nameOfRHS = id; } )?
		LBRACE
		b=replaceBody[getCoords(r), params, namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		RBRACE
	| LBRACEMINUS 
		{ params = new CollectNode<BaseNode>(); }
		b=replaceBody[getCoords(r), params, namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
	  RBRACE
	;

modifyPart [ CollectNode<IdentNode> dels, CollectNode<BaseNode> params, AnonymousScopeNamer namer,
			int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ]
			returns [ ModifyDeclNode res = null ]
	: m=MODIFY ( id=rhsIdentDecl { nameOfRHS = id; } )?
		LBRACE
		b=modifyBody[getCoords(m), dels, params, namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		RBRACE
	| LBRACEPLUS 
		{ params = new CollectNode<BaseNode>(); }
		b=modifyBody[getCoords(m), dels, params, namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
	  RBRACE
	;

emptyModifyPart [ Coords coords, CollectNode<IdentNode> dels, CollectNode<BaseNode> params,
				int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ]
				returns [ ModifyDeclNode res = null ]
	@init{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
		CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		GraphNode graph = new GraphNode(nameOfRHS.toString(), coords, 
			connections, params, defVariablesToBeYieldedTo, subpatterns,
			orderedReplacements, evals, returnz, imperativeStmts,
			context, directlyNestingLHSGraph);
		res = new ModifyDeclNode(nameOfRHS, graph, dels);
	}
	: 
	;

patternBody [ Coords coords, CollectNode<BaseNode> params, AnonymousScopeNamer namer, int mod, int context, String nameOfGraph ] returns [ PatternGraphNode res = null ]
	@init{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
		CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
		CollectNode<AlternativeNode> alts = new CollectNode<AlternativeNode>();
		CollectNode<IteratedNode> iters = new CollectNode<IteratedNode>();
		CollectNode<PatternGraphNode> negs = new CollectNode<PatternGraphNode>();
		CollectNode<PatternGraphNode> idpts = new CollectNode<PatternGraphNode>();
		CollectNode<ExprNode> conds = new CollectNode<ExprNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<HomNode> homs = new CollectNode<HomNode>();
		CollectNode<TotallyHomNode> totallyhoms = new CollectNode<TotallyHomNode>();
		CollectNode<ExactNode> exact = new CollectNode<ExactNode>();
		CollectNode<InducedNode> induced = new CollectNode<InducedNode>();
		res = new PatternGraphNode(nameOfGraph, coords, 
				connections, params, defVariablesToBeYieldedTo, subpatterns, orderedReplacements, 
				alts, iters, negs, idpts, conds, evals,
				returnz, homs, totallyhoms, exact, induced, mod, context);
	}

	: ( patternStmt[connections, defVariablesToBeYieldedTo, subpatterns, orderedReplacements,
			alts, iters, negs, idpts, namer, conds, evals,
			returnz, homs, totallyhoms, exact, induced, context, res] )*
	;

patternStmt [ CollectNode<BaseNode> conn, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
			CollectNode<SubpatternUsageNode> subpatterns, CollectNode<OrderedReplacementsNode> orderedReplacements,
			CollectNode<AlternativeNode> alts, CollectNode<IteratedNode> iters, CollectNode<PatternGraphNode> negs,
			CollectNode<PatternGraphNode> idpts, AnonymousScopeNamer namer, CollectNode<ExprNode> conds, CollectNode<EvalStatementsNode> evals,
			CollectNode<ExprNode> returnz, CollectNode<HomNode> homs, CollectNode<TotallyHomNode> totallyhoms,
			CollectNode<ExactNode> exact, CollectNode<InducedNode> induced,
			int context, PatternGraphNode directlyNestingLHSGraph]
	: connectionsOrSubpattern[conn, defVariablesToBeYieldedTo, subpatterns, orderedReplacements, context, directlyNestingLHSGraph] SEMI
	| (iterated[AnonymousScopeNamer.getDummyNamer(), 0]) => iter=iterated[namer, context] { iters.addChild(iter); } // must scan ahead to end of () to see if *,+,?,[ is following in order to distinguish from one-case alternative ()
	| alt=alternative[namer, context] { alts.addChild(alt); }
	| neg=negative[namer, context] { negs.addChild(neg); }
	| idpt=independent[namer, context] { idpts.addChild(idpt); }
	| condition[conds]
	| yielding[evals, namer, context, directlyNestingLHSGraph]
	| rets[returnz, context] SEMI
	| hom=homStatement { homs.addChild(hom); } SEMI
	| totallyhom=totallyHomStatement { totallyhoms.addChild(totallyhom); } SEMI
	| exa=exactStatement { exact.addChild(exa); } SEMI
	| ind=inducedStatement { induced.addChild(ind); } SEMI
	;

connectionsOrSubpattern [ CollectNode<BaseNode> conn, CollectNode<VarDeclNode> defVariablesToBeYieldedTo, 
						CollectNode<SubpatternUsageNode> subpatterns, CollectNode<OrderedReplacementsNode> orderedReplacements,
						int context, PatternGraphNode directlyNestingLHSGraph ]
	: firstEdge[conn, context, directlyNestingLHSGraph] // connection starts with an edge which dangles on the left
	| firstNodeOrSubpattern[conn, subpatterns, orderedReplacements, context, directlyNestingLHSGraph] // there's a subpattern or a connection that starts with a node
	| defEntityToBeYieldedTo[conn, defVariablesToBeYieldedTo, context, directlyNestingLHSGraph] // single entity definitions to be filled by later yield assignments
	;

firstEdge [ CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
		MutableInteger redirection = new MutableInteger(ConnectionNode.NO_REDIRECTION);
	}

	:   ( e=forwardOrUndirectedEdgeOcc[context, direction, redirection, directlyNestingLHSGraph] { forward=true; } // get first edge
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction, redirection, directlyNestingLHSGraph] { forward=false; }
		| e=arbitraryEdgeOcc[context, directlyNestingLHSGraph] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
		nodeContinuation[e, env.getDummyNodeDecl(context, directlyNestingLHSGraph), forward, direction, redirection, conn, context, directlyNestingLHSGraph] // and continue looking for node
	;

firstNodeOrSubpattern [ CollectNode<BaseNode> conn, CollectNode<SubpatternUsageNode> subpatterns, 
			CollectNode<OrderedReplacementsNode> orderedReplacements, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		id = env.getDummyIdent();
		IdentNode type = env.getNodeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
		CollectNode<ExprNode> subpatternConn = new CollectNode<ExprNode>();
		CollectNode<ExprNode> subpatternReplConn = new CollectNode<ExprNode>();
		IdentNode curId = env.getDummyIdent();
		CollectNode<IdentNode> mergees = new CollectNode<IdentNode>();
		BaseNode n = null;
	}

	: id=entIdentUse firstEdgeContinuation[id, conn, context, directlyNestingLHSGraph] // use of already declared node, continue looking for first edge
	| id=entIdentUse l=LPAREN arguments[subpatternReplConn] RPAREN // use of already declared subpattern
		{ OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(id.getCoords(), id.toString());
		  orderedReplacements.addChild(curOrderedRepl);
		  curOrderedRepl.addChild(new SubpatternReplNode(id, subpatternReplConn));
		}
	| id=entIdentDecl cc=COLON // node or subpattern declaration
		firstNodeOrSubpatternDeclaration [ id, conn, subpatterns, context, directlyNestingLHSGraph ]
	| c=COLON // anonymous node or subpattern declaration
		anonymousFirstNodeOrSubpatternDeclaration [ c, conn, subpatterns, context, directlyNestingLHSGraph ]
	| d=DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		{ n = new NodeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph); }
		//( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	;

firstNodeOrSubpatternDeclaration [ IdentNode id, CollectNode<BaseNode> conn, CollectNode<SubpatternUsageNode> subpatterns, 
			int context, PatternGraphNode directlyNestingLHSGraph ]
	options { k = 4; }
	@init{
		type = env.getNodeRoot();
		constr = TypeExprNode.getEmpty();
		CollectNode<ExprNode> subpatternConn = new CollectNode<ExprNode>();
		curId = env.getDummyIdent();
		CollectNode<IdentNode> mergees = new CollectNode<IdentNode>();
		BaseNode n = null;
	}

	: // node declaration
		type=typeIdentUse
		( constr=typeConstraint )?
		( 
			{
				n = new NodeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph);
			}
		| LT oldid=entIdentUse (COMMA curId=entIdentUse { mergees.addChild(curId); })* GT
			{
				n = new NodeTypeChangeNode(id, type, context, oldid, mergees, directlyNestingLHSGraph);
			}
		| LBRACE nsic=nodeStorageIndexContinuation [ id, type, context, directlyNestingLHSGraph ] RBRACE
			{
				n = nsic;
			}
		)
		( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node typeof declaration
		TYPEOF LPAREN type=entIdentUse RPAREN
		( constr=typeConstraint )?
		( LT oldid=entIdentUse (COMMA curId=entIdentUse { mergees.addChild(curId); })* GT )?
		{
			if(oldid==null) {
				n = new NodeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph);
			} else {
				n = new NodeTypeChangeNode(id, type, context, oldid, mergees, directlyNestingLHSGraph);
			}
		}
		( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node copy declaration
		COPY LT type=entIdentUse GT 
		{
			n = new NodeDeclNode(id, type, true, context, constr, directlyNestingLHSGraph);
		}
		( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // subpattern declaration
		type=patIdentUse LPAREN arguments[subpatternConn] RPAREN
		{ subpatterns.addChild(new SubpatternUsageNode(id, type, context, subpatternConn)); }
	;

nodeStorageIndexContinuation [ IdentNode id, IdentNode type, int context, PatternGraphNode directlyNestingLHSGraph ]
					returns [ NodeDeclNode n = null ]
	: (DOUBLECOLON)? oldid=entIdentUse (d=DOT attr=entIdentUse)? (LBRACK (DOUBLECOLON)? mapAccess=entIdentUse RBRACK)?
		{
			if(mapAccess==null)
				n = new MatchNodeFromStorageNode(id, type, context, 
					attr==null ? new IdentExprNode(oldid) : new QualIdentNode(getCoords(d), oldid, attr), directlyNestingLHSGraph);
			else
				n = new MatchNodeByStorageAccessNode(id, type, context, 
					attr==null ? new IdentExprNode(oldid) : new QualIdentNode(getCoords(d), oldid, attr), new IdentExprNode(mapAccess), directlyNestingLHSGraph);
		}
	| idx=indexIdentUse EQUAL e=expr[false]
		{
			n = new MatchNodeByIndexAccessEqualityNode(id, type, context, 
						idx, e, directlyNestingLHSGraph);
		}
	| i=IDENT LPAREN idx=indexIdentUse (os=relOS e=expr[false] (COMMA idx2=indexIdentUse os2=relOS e2=expr[false])?)? RPAREN
		{
			if(i.getText().equals("ascending")) 
				n = new MatchNodeByIndexAccessOrderingNode(id, type, context, 
						true, idx, os, e, os2, e2, directlyNestingLHSGraph);
			else if(i.getText().equals("descending"))
				n = new MatchNodeByIndexAccessOrderingNode(id, type, context, 
						false, idx, os, e, os2, e2, directlyNestingLHSGraph);
			else
				reportError(getCoords(i), "ordered index access expression must start with ascending or descending");
			if(idx2!=null && !idx.toString().equals(idx2.toString()))
				reportError(idx2.getCoords(), "the same index must be used in an ordered index access expression with two constraints");
		}
	| AT LPAREN e=expr[false] RPAREN
		{
			n = new MatchNodeByNameLookupNode(id, type, context, 
						e, directlyNestingLHSGraph);
		}
	| {input.LT(1).getText().equals("unique")}? i=IDENT LBRACK e=expr[false] RBRACK
		{
			n = new MatchNodeByUniqueLookupNode(id, type, context,
						e, directlyNestingLHSGraph);
		}
	;

relOS returns [ int os = OperatorSignature.ERROR ]
	: lt=LT { os = OperatorSignature.LT; }
	| le=LE { os = OperatorSignature.LE; }
	| gt=GT { os = OperatorSignature.GT; }
	| ge=GE { os = OperatorSignature.GE; }
	;

anonymousFirstNodeOrSubpatternDeclaration [ Token c, CollectNode<BaseNode> conn, CollectNode<SubpatternUsageNode> subpatterns, 
			int context, PatternGraphNode directlyNestingLHSGraph ] returns [ IdentNode id = env.getDummyIdent() ]
	options { k = 4; }
	@init{
		type = env.getNodeRoot();
		constr = TypeExprNode.getEmpty();
		CollectNode<ExprNode> subpatternConn = new CollectNode<ExprNode>();
		curId = env.getDummyIdent();
		CollectNode<IdentNode> mergees = new CollectNode<IdentNode>();
		BaseNode n = null;
	}

	:  // node declaration
		{ id = env.defineAnonymousEntity("node", getCoords(c)); }
		type=typeIdentUse
		( constr=typeConstraint )?
		(
			{
				n = new NodeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph);
			}
		| LT oldid=entIdentUse (COMMA curId=entIdentUse { mergees.addChild(curId); })* GT
			{
				n = new NodeTypeChangeNode(id, type, context, oldid, mergees, directlyNestingLHSGraph);
			}
		| LBRACE nsic=nodeStorageIndexContinuation [ id, type, context, directlyNestingLHSGraph ] RBRACE
			{
				n = nsic;
			}
		)
		( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node typeof declaration
		{ id = env.defineAnonymousEntity("node", getCoords(c)); }
		TYPEOF LPAREN type=entIdentUse RPAREN
		( constr=typeConstraint )?
		( LT oldid=entIdentUse (COMMA curId=entIdentUse { mergees.addChild(curId); })* GT )?
		{
			if(oldid==null) {
				n = new NodeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph);
			} else {
				n = new NodeTypeChangeNode(id, type, context, oldid, mergees, directlyNestingLHSGraph);
			}
		}
		( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node copy declaration
		COPY LT type=entIdentUse GT 
		{
			n = new NodeDeclNode(id, type, true, context, constr, directlyNestingLHSGraph);
		}
		( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // subpattern declaration
		{ id = env.defineAnonymousEntity("sub", getCoords(c)); }
		type=patIdentUse LPAREN arguments[subpatternConn] RPAREN
		{ subpatterns.addChild(new SubpatternUsageNode(id, type, context, subpatternConn)); }
	;

defEntityToBeYieldedTo [ CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
						int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	: DEF (
		MINUS edge=defEdgeToBeYieldedTo[context, directlyNestingLHSGraph] direction=forwardOrUndirectedEdgeParam
			{
				BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
				res = new ConnectionNode(dummy, edge, dummy, direction, ConnectionNode.NO_REDIRECTION);
				if(connections!=null) connections.addChild(res);
			}
		  ( defGraphElementInitialization[context, edge] )? 
		| LARROW edge=defEdgeToBeYieldedTo[context, directlyNestingLHSGraph] RARROW
			{
				BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
				res = new ConnectionNode(dummy, edge, dummy, ConnectionNode.ARBITRARY_DIRECTED, ConnectionNode.NO_REDIRECTION);
				if(connections!=null) connections.addChild(res);
			}
		  ( defGraphElementInitialization[context, edge] )? 
		| QUESTIONMINUS edge=defEdgeToBeYieldedTo[context, directlyNestingLHSGraph] MINUSQUESTION
			{
				BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
				res = new ConnectionNode(dummy, edge, dummy, ConnectionNode.ARBITRARY, ConnectionNode.NO_REDIRECTION);
				if(connections!=null) connections.addChild(res);
			}
		  ( defGraphElementInitialization[context, edge] )? 
		| v=defVarDeclToBeYieldedTo[context, directlyNestingLHSGraph]
			{
				res = v;
				if(defVariablesToBeYieldedTo!=null) defVariablesToBeYieldedTo.addChild(v);
			}
		| node=defNodeToBeYieldedTo[context, directlyNestingLHSGraph]
			{
				res = new SingleNodeConnNode(node);
				if(connections!=null) connections.addChild(res);
			}
		  ( defGraphElementInitialization[context, node] )? 
		)
	;

defNodeToBeYieldedTo[ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ NodeDeclNode res = null ]
	: id=entIdentDecl COLON type=typeIdentUse
		{ res = new NodeDeclNode(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph, false, true); }
	;
	
defEdgeToBeYieldedTo [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	: id=entIdentDecl COLON type=typeIdentUse
		{ res = new EdgeDeclNode(id, type, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph, false, true); }
	;

defGraphElementInitialization [ int context, ConstraintDeclNode graphElement ]
	: a=ASSIGN e=expr[false] 
	    { if((context & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION) {
			reportError(getCoords(a), "initialization of a def node/edge only allowed in a function");
	      } else {
			if(graphElement!=null) graphElement.setInitialization(e);
		  }
		} 
	;

defVarDeclToBeYieldedTo [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ VarDeclNode res = env.initVarNode(directlyNestingLHSGraph, context) ]
	@init{ VarDeclNode var = null; }
	: modifier=IDENT id=entIdentDecl COLON
		(
			type=typeIdentUse
			{
				var = new VarDeclNode(id, type, directlyNestingLHSGraph, context, true);
				if(!modifier.getText().equals("var")) 
					{ reportError(getCoords(modifier), "var keyword needed before non graph element and non container def variable"); }
			}
		|
			MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				var = new VarDeclNode(id, MapTypeNode.getMapType(keyType, valueType), directlyNestingLHSGraph, context, true);
				if(!modifier.getText().equals("ref"))
					{ reportError(getCoords(modifier), "ref keyword needed before map typed def variable"); }
			}
		|
			SET LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				var = new VarDeclNode(id, SetTypeNode.getSetType(keyType), directlyNestingLHSGraph, context, true);
				if(!modifier.getText().equals("ref")) 
					{ reportError(getCoords(modifier), "ref keyword needed before set typed def variable"); }
			}
		|
			ARRAY LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				var = new VarDeclNode(id, ArrayTypeNode.getArrayType(keyType), directlyNestingLHSGraph, context, true);
				if(!modifier.getText().equals("ref")) 
					{ reportError(getCoords(modifier), "ref keyword needed before array typed def variable"); }
			}
		|
			DEQUE LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				var = new VarDeclNode(id, DequeTypeNode.getDequeType(keyType), directlyNestingLHSGraph, context, true);
				if(!modifier.getText().equals("ref")) 
					{ reportError(getCoords(modifier), "ref keyword needed before deque typed def variable"); }
			}
		|
			MATCH LT keyType=actionIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				var = new VarDeclNode(id, MatchTypeNode.getMatchType(keyType), directlyNestingLHSGraph, 0);
				if(!modifier.getText().equals("ref"))
					{ reportError(getCoords(modifier), "ref keyword needed before match typed def variable"); }
			}
		)
		{ if(var!=null) res = var; }
		(a=ASSIGN e=expr[false] { if(var!=null) var.setInitialization(e); } )? 
	;

nodeContinuation [ BaseNode e, BaseNode n1, boolean forward, MutableInteger direction, MutableInteger redirection, CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{ n2 = env.getDummyNodeDecl(context, directlyNestingLHSGraph); }

	: n2=nodeOcc[context, directlyNestingLHSGraph] // node following - get it and build connection with it, then continue with looking for follwing edge
		{
			if (direction.getValue() == ConnectionNode.DIRECTED && !forward) {
				conn.addChild(new ConnectionNode(n2, e, n1, direction.getValue(), redirection.getValue()));
			} else {
				conn.addChild(new ConnectionNode(n1, e, n2, direction.getValue(), redirection.getValue()));
			}
		}
		edgeContinuation[n2, conn, context, directlyNestingLHSGraph]
	|   // nothing following - build connection with edge dangeling on the right (see n2 initialization)
		{
			if (direction.getValue() == ConnectionNode.DIRECTED && !forward) {
				conn.addChild(new ConnectionNode(n2, e, n1, direction.getValue(), redirection.getValue()));
			} else {
				conn.addChild(new ConnectionNode(n1, e, n2, direction.getValue(), redirection.getValue()));
			}
		}
	;

firstEdgeContinuation [ BaseNode n, CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
		MutableInteger redirection = new MutableInteger(ConnectionNode.NO_REDIRECTION);
	}

	: // nothing following? -> one single node
	{
		if (n instanceof IdentNode) {
			conn.addChild(new SingleGraphEntityNode((IdentNode)n));
		}
		else {
			conn.addChild(new SingleNodeConnNode(n));
		}
	}
	|   ( e=forwardOrUndirectedEdgeOcc[context, direction, redirection, directlyNestingLHSGraph] { forward=true; }
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction, redirection, directlyNestingLHSGraph] { forward=false; }
		| e=arbitraryEdgeOcc[context, directlyNestingLHSGraph] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
			nodeContinuation[e, n, forward, direction, redirection, conn, context, directlyNestingLHSGraph] // continue looking for node
	;

edgeContinuation [ BaseNode left, CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
		MutableInteger redirection = new MutableInteger(ConnectionNode.NO_REDIRECTION);
	}

	:   // nothing following? -> connection end reached
	|   ( e=forwardOrUndirectedEdgeOcc[context, direction, redirection, directlyNestingLHSGraph] { forward=true; }
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction, redirection, directlyNestingLHSGraph] { forward=false; }
		| e=arbitraryEdgeOcc[context, directlyNestingLHSGraph] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
			nodeContinuation[e, left, forward, direction, redirection, conn, context, directlyNestingLHSGraph] // continue looking for node
	;

nodeOcc [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	@init{
		id = env.getDummyIdent();
	}

	: e=entIdentUse { res = e; } // use of already declared node
	| id=entIdentDecl COLON co=nodeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; } // node declaration
	| c=COLON // anonymous node declaration
			{ id = env.defineAnonymousEntity("node", getCoords(c)); }
			co=nodeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; }
	| d=DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		//( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
		{ res = new NodeDeclNode(id, env.getNodeRoot(), false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph); }
	;

nodeTypeContinuation [ IdentNode id, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	@init{
		type = env.getNodeRoot();
		constr = TypeExprNode.getEmpty();
		curId = env.getDummyIdent();
		CollectNode<IdentNode> mergees = new CollectNode<IdentNode>();
	}

	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		( 
			{
				res = new NodeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph);
			}
		| LT oldid=entIdentUse (COMMA curId=entIdentUse { mergees.addChild(curId); })* GT
			{
				res = new NodeTypeChangeNode(id, type, context, oldid, mergees, directlyNestingLHSGraph);
			}
		| LBRACE nsic=nodeStorageIndexContinuation [ id, type, context, directlyNestingLHSGraph ] RBRACE
			{
				res = nsic;
			}
		)
		( AT LPAREN nameAndAttributesInitializationList[res] RPAREN )?
	| COPY LT type=entIdentUse GT
		{
			res = new NodeDeclNode(id, type, true, context, constr, directlyNestingLHSGraph);
		}
		( AT LPAREN nameAndAttributesInitializationList[res] RPAREN )?
	;

nodeDeclParam [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	@init{
		constr = TypeExprNode.getEmpty();
	}

	: id=entIdentDecl COLON
		type=typeIdentUse
		( constr=typeConstraint )?
		( LT (interfaceType=typeIdentUse (PLUS maybe=NULL)?
				| maybe=NULL (PLUS interfaceType=typeIdentUse)?) GT )?
			{
				if(interfaceType==null) {
					res = new NodeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph, maybe!=null, false);
				} else {
					res = new NodeInterfaceTypeChangeNode(id, type, context, interfaceType, directlyNestingLHSGraph, maybe!=null);
				}
			}
	;

varDecl [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	: paramModifier=IDENT id=entIdentDecl COLON
		(
			type=typeIdentUse
			{
				res = new VarDeclNode(id, type, directlyNestingLHSGraph, context);
				if(!paramModifier.getText().equals("var")) 
					{ reportError(getCoords(paramModifier), "var keyword needed before non graph element and non container parameter"); }
			}
		|
			MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				res = new VarDeclNode(id, MapTypeNode.getMapType(keyType, valueType), directlyNestingLHSGraph, context);
				if(!paramModifier.getText().equals("ref"))
					{ reportWarning(getCoords(paramModifier), "ref keyword needed before map typed parameter"); } // TODO: next version -> error
			}
		|
			SET LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				res = new VarDeclNode(id, SetTypeNode.getSetType(keyType), directlyNestingLHSGraph, context);
				if(!paramModifier.getText().equals("ref")) 
					{ reportWarning(getCoords(paramModifier), "ref keyword needed before set typed parameter"); } // TODO: next version -> error
			}
		|
			ARRAY LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				res = new VarDeclNode(id, ArrayTypeNode.getArrayType(keyType), directlyNestingLHSGraph, context);
				if(!paramModifier.getText().equals("ref")) 
					{ reportWarning(getCoords(paramModifier), "ref keyword needed before array typed parameter"); } // TODO: next version -> error
			}
		|
			DEQUE LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				res = new VarDeclNode(id, DequeTypeNode.getDequeType(keyType), directlyNestingLHSGraph, context);
				if(!paramModifier.getText().equals("ref")) 
					{ reportWarning(getCoords(paramModifier), "ref keyword needed before deque typed parameter"); } // TODO: next version -> error
			}
		)
	;

forwardOrUndirectedEdgeOcc [int context, MutableInteger direction, MutableInteger redirection, PatternGraphNode directlyNestingLHSGraph] returns [ BaseNode res = env.initNode() ]
	: (NOT { redirection.setValue(ConnectionNode.REDIRECT_SOURCE); })? MINUS 
		( e1=edgeDecl[context, directlyNestingLHSGraph] { res = e1; } 
		| e2=entIdentUse { res = e2; } ) 
		forwardOrUndirectedEdgeOccContinuation[direction, redirection]
	| da=DOUBLE_RARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.DIRECTED);
		}
		//( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
	| mm=MINUSMINUS
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, env.getUndirectedEdgeRoot(), false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.UNDIRECTED);
		}
		//( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
	;

forwardOrUndirectedEdgeOccContinuation [MutableInteger direction, MutableInteger redirection]
	: MINUS { direction.setValue(ConnectionNode.UNDIRECTED); } (NOT { redirection.setValue(ConnectionNode.REDIRECT_TARGET | redirection.getValue()); })? // redirection not allowd but semantic error is better
	| RARROW { direction.setValue(ConnectionNode.DIRECTED); } (NOT { redirection.setValue(ConnectionNode.REDIRECT_TARGET | redirection.getValue()); })?
	;

backwardOrArbitraryDirectedEdgeOcc [ int context, MutableInteger direction, MutableInteger redirection, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	: (NOT { redirection.setValue(ConnectionNode.REDIRECT_TARGET); })? LARROW 
		( e1=edgeDecl[context, directlyNestingLHSGraph] { res = e1; }
		| e2=entIdentUse { res = e2; } )
		backwardOrArbitraryDirectedEdgeOccContinuation[ direction, redirection ]
	| da=DOUBLE_LARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.DIRECTED);
		}
		//( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
	| lr=LRARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(lr));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.ARBITRARY_DIRECTED);
		}
		//( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
	;

backwardOrArbitraryDirectedEdgeOccContinuation [MutableInteger direction, MutableInteger redirection]
	: MINUS { direction.setValue(ConnectionNode.DIRECTED); } (NOT { redirection.setValue(ConnectionNode.REDIRECT_SOURCE | redirection.getValue()); })?
	| RARROW { direction.setValue(ConnectionNode.ARBITRARY_DIRECTED); } (NOT { redirection.setValue(ConnectionNode.REDIRECT_SOURCE | redirection.getValue()); })? // redirection not allowd but semantic error is better
	;

arbitraryEdgeOcc [int context, PatternGraphNode directlyNestingLHSGraph] returns [ BaseNode res = env.initNode() ]
	: QUESTIONMINUS
		( e1=edgeDecl[context, directlyNestingLHSGraph] { res = e1; }
		| e2=entIdentUse { res = e2; } )
		MINUSQUESTION
	| q=QMMQ
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(q));
			res = new EdgeDeclNode(id, env.getArbitraryEdgeRoot(), false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		}
		//( AT LPAREN nameAndAttributesInitializationList[n] RPAREN )?
	;

edgeDecl [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	@init{
		id = env.getDummyIdent();
	}

	:   ( id=entIdentDecl COLON
			co=edgeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; } 
		| c=COLON
			{ id = env.defineAnonymousEntity("edge", getCoords(c)); }
			co=edgeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; } 
		)
	;

edgeDeclParam [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	@init{
		id = env.getDummyIdent();
		type = env.getNodeRoot();
		constr = TypeExprNode.getEmpty();
	}

	: id=entIdentDecl COLON type=typeIdentUse
		( constr=typeConstraint )?
		( LT (interfaceType=typeIdentUse (PLUS maybe=NULL)?
				| maybe=NULL (PLUS interfaceType=typeIdentUse)?) GT )?
			{
				if( interfaceType == null ) {
					res = new EdgeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph, maybe!=null, false);
				} else {
					res = new EdgeInterfaceTypeChangeNode(id, type, context, interfaceType, directlyNestingLHSGraph, maybe!=null);
				}
			}
	;

edgeTypeContinuation [ IdentNode id, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	@init{
		type = env.getNodeRoot();
		constr = TypeExprNode.getEmpty();
	}

	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		(
			{
				res = new EdgeDeclNode(id, type, false, context, constr, directlyNestingLHSGraph);
			}
		| LT oldid=entIdentUse GT
			{
				res = new EdgeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
			}
		| LBRACE esic=edgeStorageIndexContinuation [ id, type, context, directlyNestingLHSGraph ] RBRACE
			{
				res = esic;
			}
		)
		( AT LPAREN nameAndAttributesInitializationList[res] RPAREN )?
	| COPY LT type=entIdentUse GT
		{
			res = new EdgeDeclNode(id, type, true, context, constr, directlyNestingLHSGraph);
		}
		( AT LPAREN nameAndAttributesInitializationList[res] RPAREN )?
	;

edgeStorageIndexContinuation [ IdentNode id, IdentNode type, int context, PatternGraphNode directlyNestingLHSGraph ]
					returns [ EdgeDeclNode res = null ]
	: (DOUBLECOLON)? oldid=entIdentUse (d=DOT attr=entIdentUse)? (LBRACK (DOUBLECOLON)? mapAccess=entIdentUse RBRACK)?
		{
			if(mapAccess==null)
				res = new MatchEdgeFromStorageNode(id, type, context, 
					attr==null ? new IdentExprNode(oldid) : new QualIdentNode(getCoords(d), oldid, attr), directlyNestingLHSGraph);
			else
				res = new MatchEdgeByStorageAccessNode(id, type, context, 
					attr==null ? new IdentExprNode(oldid) : new QualIdentNode(getCoords(d), oldid, attr), new IdentExprNode(mapAccess), directlyNestingLHSGraph);
		}
	| idx=indexIdentUse EQUAL e=expr[false]
		{
			res = new MatchEdgeByIndexAccessEqualityNode(id, type, context, 
						idx, e, directlyNestingLHSGraph);
		}
	| i=IDENT LPAREN idx=indexIdentUse (os=relOS e=expr[false] (COMMA idx2=indexIdentUse os2=relOS e2=expr[false])?)? RPAREN
		{
			if(i.getText().equals("ascending")) 
				res = new MatchEdgeByIndexAccessOrderingNode(id, type, context, 
						true, idx, os, e, os2, e2, directlyNestingLHSGraph);
			else if(i.getText().equals("descending"))
				res = new MatchEdgeByIndexAccessOrderingNode(id, type, context, 
						false, idx, os, e, os2, e2, directlyNestingLHSGraph);
			else
				reportError(getCoords(i), "ordered index access expression must start with ascending or descending");
			if(idx2!=null && !idx.toString().equals(idx2.toString()))
				reportError(idx2.getCoords(), "the same index must be used in an ordered index access expression with two constraints");
		}
	| AT LPAREN e=expr[false] RPAREN
		{
			res = new MatchEdgeByNameLookupNode(id, type, context, 
						e, directlyNestingLHSGraph);
		}
	| {input.LT(1).getText().equals("unique")}? i=IDENT LBRACK e=expr[false] RBRACK
		{
			res = new MatchEdgeByUniqueLookupNode(id, type, context,
						e, directlyNestingLHSGraph);
		}
	;

nameAndAttributesInitializationList[BaseNode n]
	@init{ ConstraintDeclNode cdl = (ConstraintDeclNode)n; }
	: nameOrAttributeInitialization[cdl] (COMMA nameOrAttributeInitialization[cdl])*
	;

nameOrAttributeInitialization[ConstraintDeclNode n]
	: DOLLAR ASSIGN arg=expr[false]
		{ n.addNameOrAttributeInitialization(new NameOrAttributeInitializationNode(n, arg)); }
	| attr=memberIdentUse ASSIGN arg=expr[false]
		{ n.addNameOrAttributeInitialization(new NameOrAttributeInitializationNode(n, attr, arg)); }
	;
	
arguments[CollectNode<ExprNode> args]
	: ( arg=argument[args] ( COMMA argument[args] )* )?
	;
	
argument[CollectNode<ExprNode> args] // argument for a subpattern usage or subpattern dependent rewrite usage
	@init{ boolean yielded=false; }
	: (y=YIELD { yielded = true; })? arg=expr[false] { args.addChild(arg); if(yielded) { if(arg instanceof IdentExprNode) ((IdentExprNode)arg).setYieldedTo(); else reportError(getCoords(y), "Can only yield to an element/variable, defined to by yielded to (def)"); } }
 	;

homStatement returns [ HomNode res = null ]
	: h=HOM {res = new HomNode(getCoords(h)); }
		LPAREN id=entIdentUse { res.addChild(id); }
			(COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
	;

totallyHomStatement returns [ TotallyHomNode res = null ]
	: i=INDEPENDENT {res = new TotallyHomNode(getCoords(i)); }
		LPAREN id=entIdentUse { res.setTotallyHom(id); } 
			(BACKSLASH entityUnaryExpr[res])?
		RPAREN
	;

entityUnaryExpr[ TotallyHomNode thn ]
	: ent=entIdentUse { thn.addChild(ent); }
	| LPAREN te=entityAddExpr[thn] RPAREN 
	;

entityAddExpr[ TotallyHomNode thn ]
	: ent=entIdentUse { thn.addChild(ent); }
		(p=PLUS op=entIdentUse { thn.addChild(ent); })*
	;
	
exactStatement returns [ ExactNode res = null ]
	: e=EXACT {res = new ExactNode(getCoords(e)); }
		LPAREN id=entIdentUse { res.addChild(id); }
			(COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
	;

inducedStatement returns [ InducedNode res = null ]
	: i=INDUCED {res = new InducedNode(getCoords(i)); }
		LPAREN id=entIdentUse { res.addChild(id); }
			(COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
	;

replaceBody [ Coords coords, CollectNode<BaseNode> params, AnonymousScopeNamer namer, 
			int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ] 
			returns [ ReplaceDeclNode res = null ]
	@init{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
		CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		GraphNode graph = new GraphNode(nameOfRHS.toString(), coords, 
			connections, params, defVariablesToBeYieldedTo, subpatterns, 
			orderedReplacements, evals, returnz, imperativeStmts,
			context, directlyNestingLHSGraph);
		res = new ReplaceDeclNode(nameOfRHS, graph);
	}

	: ( replaceStmt[coords, connections, defVariablesToBeYieldedTo, subpatterns, orderedReplacements, evals, namer, context, directlyNestingLHSGraph] 
		| rets[returnz, context] SEMI
		| execStmt[imperativeStmts, context, directlyNestingLHSGraph] SEMI
		| emitStmt[imperativeStmts, orderedReplacements] SEMI
		)*
	;

replaceStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<SubpatternUsageNode> subpatterns, CollectNode<OrderedReplacementsNode> orderedReplacements,
		CollectNode<EvalStatementsNode> evals, AnonymousScopeNamer namer,
		int context, PatternGraphNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[connections, defVariablesToBeYieldedTo, subpatterns, orderedReplacements, context, directlyNestingLHSGraph] SEMI
	| evaluation[evals, orderedReplacements, namer, context, directlyNestingLHSGraph]
	| alternativeOrIteratedRewriteUsage[orderedReplacements]
	;

modifyBody [ Coords coords, CollectNode<IdentNode> dels, CollectNode<BaseNode> params, AnonymousScopeNamer namer, 
			int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ]
			returns [ ModifyDeclNode res = null ]
	@init{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
		CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		GraphNode graph = new GraphNode(nameOfRHS.toString(), coords, 
			connections, params, defVariablesToBeYieldedTo, subpatterns,
			orderedReplacements, evals, returnz, imperativeStmts,
			context, directlyNestingLHSGraph);
		res = new ModifyDeclNode(nameOfRHS, graph, dels);
	}

	: ( modifyStmt[coords, connections, defVariablesToBeYieldedTo, subpatterns, orderedReplacements, evals, dels, namer, context, directlyNestingLHSGraph] 
		| rets[returnz, context] SEMI
		| execStmt[imperativeStmts, context, directlyNestingLHSGraph] SEMI
		| emitStmt[imperativeStmts, orderedReplacements] SEMI
		)*
	;

modifyStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<SubpatternUsageNode> subpatterns, CollectNode<OrderedReplacementsNode> orderedReplacements,
		CollectNode<EvalStatementsNode> evals, CollectNode<IdentNode> dels, AnonymousScopeNamer namer,
		int context, PatternGraphNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[connections, defVariablesToBeYieldedTo, subpatterns, orderedReplacements, context, directlyNestingLHSGraph] SEMI
	| deleteStmt[dels] SEMI
	| evaluation[evals, orderedReplacements, namer, context, directlyNestingLHSGraph ]
	| alternativeOrIteratedRewriteUsage[orderedReplacements]
	;

alternative [ AnonymousScopeNamer namer, int context ] returns [ AlternativeNode alt = null ]
	: a=ALTERNATIVE (name=altIdentDecl)? { namer.defAlt(name, getCoords(a)); alt = new AlternativeNode(namer.alt()); } LBRACE
		( alternativeCase[alt, namer, context] ) +
		RBRACE
	| a=LPAREN { namer.defAlt(null, getCoords(a)); alt = new AlternativeNode(namer.alt()); }
		( alternativeCasePure[alt, a, namer, context] )
			( BOR alternativeCasePure[alt, a, namer, context] ) *
		RPAREN
	;	
	
alternativeCase [ AlternativeNode alt, AnonymousScopeNamer namer, int context ]
	@init{
		int mod = 0;
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<RhsDeclNode> rightHandSides = new CollectNode<RhsDeclNode>();
	}
	
	: (name=altIdentDecl)? l=LBRACE { namer.defAltCase(name, getCoords(l)); } { env.pushScope(namer.altCase()); }
		left=patternBody[getCoords(l), new CollectNode<BaseNode>(), namer, mod, context, namer.altCase().toString()]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.altCase(), left]
				{
					rightHandSides.addChild(rightReplace);
				}
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.altCase(), left]
				{
					rightHandSides.addChild(rightModify);
				}
		) ?
		RBRACE { env.popScope(); } { alt.addChild(new AlternativeCaseNode(namer.altCase(), left, rightHandSides)); }
	;

alternativeCasePure [ AlternativeNode alt, Token a, AnonymousScopeNamer namer, int context ]
	@init{
		int mod = 0;
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<RhsDeclNode> rightHandSides = new CollectNode<RhsDeclNode>();
		IdentNode altCaseName = IdentNode.getInvalid();
	}
	
	: { namer.defAltCase(null, getCoords(a)); } { env.pushScope(namer.altCase()); }
		left=patternBody[getCoords(a), new CollectNode<BaseNode>(), namer, mod, context, namer.altCase().toString()]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.altCase(), left]
				{
					rightHandSides.addChild(rightReplace);
				}
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.altCase(), left]
				{
					rightHandSides.addChild(rightModify);
				}
		) ?
		{ env.popScope(); } { alt.addChild(new AlternativeCaseNode(namer.altCase(), left, rightHandSides)); }
	;

iterated [ AnonymousScopeNamer namer, int context ] returns [ IteratedNode res = null ]
	@init{
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<RhsDeclNode> rightHandSides = new CollectNode<RhsDeclNode>();
		IdentNode iterName = IdentNode.getInvalid();
		int minMatches = -1;
		int maxMatches = -1;
	}

	: ( i=ITERATED { minMatches = 0; maxMatches = 0; } 
	  | i=OPTIONAL { minMatches = 0; maxMatches = 1; }
	  | i=MULTIPLE { minMatches = 1; maxMatches = 0; }
	  )
	    ( name=iterIdentDecl { namer.defIter(name, null); } 
		  | { namer.defIter(null, getCoords(i)); } )
		LBRACE { env.pushScope(namer.iter()); }
		left=patternBody[getCoords(i), new CollectNode<BaseNode>(), namer, 0, context, namer.iter().toString()]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.iter(), left]
				{
					rightHandSides.addChild(rightReplace);
				}
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.iter(), left]
				{
					rightHandSides.addChild(rightModify);
				}
		) ?				
		RBRACE { env.popScope(); } { res = new IteratedNode(namer.iter(), left, rightHandSides, minMatches, maxMatches); }
	| 
		l=LPAREN { namer.defIter(null, getCoords(l)); } { env.pushScope(namer.iter()); }
		left=patternBody[getCoords(i), new CollectNode<BaseNode>(), namer, 0, context, namer.iter().toString()]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.iter(), left]
				{
					rightHandSides.addChild(rightReplace);
				}
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.iter(), left]
				{
					rightHandSides.addChild(rightModify);
				}
		) ?	
		RPAREN { env.popScope(); } 
	  ( 
	    STAR { minMatches = 0; maxMatches = 0; } 
	  | QUESTION { minMatches = 0; maxMatches = 1; }
	  | PLUS { minMatches = 1; maxMatches = 0; }
	  | LBRACK i=NUM_INTEGER { minMatches = Integer.parseInt(i.getText()); }
	  	   ( COLON ( STAR { maxMatches=0; } | i=NUM_INTEGER { maxMatches = Integer.parseInt(i.getText()); } ) | { maxMatches = minMatches; } )
		  RBRACK
	  )
		{ res = new IteratedNode(namer.iter(), left, rightHandSides, minMatches, maxMatches); }
	;

negative [ AnonymousScopeNamer namer, int context ] returns [ PatternGraphNode res = null ]
	@init{
		int mod = 0;
		boolean brk = false;
	}
	
	: (BREAK { brk = true; })? n=NEGATIVE (name=negIdentDecl)? { namer.defNeg(name, getCoords(n)); } 
		LBRACE { env.pushScope(namer.neg()); }
			( ( PATTERNPATH { mod = PatternGraphNode.MOD_PATTERNPATH_LOCKED; }
			| PATTERN { mod = PatternGraphNode.MOD_PATTERN_LOCKED; } ) SEMI )*
			b=patternBody[getCoords(n), new CollectNode<BaseNode>(), namer, mod, 
				context|BaseNode.CONTEXT_NEGATIVE, namer.neg().toString()] { res = b; b.iterationBreaking = brk; } 
		RBRACE { env.popScope(); }
	| n=TILDE { namer.defNeg(null, getCoords(n)); }
		LPAREN { env.pushScope(namer.neg()); }
			( ( PATTERNPATH { mod = PatternGraphNode.MOD_PATTERNPATH_LOCKED; }
			| PATTERN { mod = PatternGraphNode.MOD_PATTERN_LOCKED; } ) SEMI )*
			b=patternBody[getCoords(n), new CollectNode<BaseNode>(), namer, mod, 
				context|BaseNode.CONTEXT_NEGATIVE, namer.neg().toString()] { res = b; } 
		RPAREN { env.popScope(); }
	;

independent [ AnonymousScopeNamer namer, int context ] returns [ PatternGraphNode res = null ]
	@init{
		int mod = 0;
		boolean brk = false;
	}
	
	: (BREAK { brk = true; })? i=INDEPENDENT (name=idptIdentDecl)? { namer.defIdpt(name, getCoords(i)); }
		LBRACE { env.pushScope(namer.idpt()); }
			( ( PATTERNPATH { mod = PatternGraphNode.MOD_PATTERNPATH_LOCKED; }
			| PATTERN { mod = PatternGraphNode.MOD_PATTERN_LOCKED; } ) SEMI )*
			b=patternBody[getCoords(i), new CollectNode<BaseNode>(), namer, mod,
				context|BaseNode.CONTEXT_INDEPENDENT, namer.idpt().toString()] { res = b; b.iterationBreaking = brk; } 
		RBRACE { env.popScope(); }
	| i=BAND { namer.defIdpt(null, getCoords(i)); }
		LPAREN { env.pushScope(namer.idpt()); }
			( ( PATTERNPATH { mod = PatternGraphNode.MOD_PATTERNPATH_LOCKED; }
			| PATTERN { mod = PatternGraphNode.MOD_PATTERN_LOCKED; } ) SEMI )*
			b=patternBody[getCoords(i), new CollectNode<BaseNode>(), namer, mod,
				context|BaseNode.CONTEXT_INDEPENDENT, namer.idpt().toString()] { res = b; } 
		RPAREN { env.popScope(); }
	;

condition [ CollectNode<ExprNode> conds ]
	: IF
		LBRACE
			( e=expr[false] { conds.addChild(e); } SEMI )* 
		RBRACE
	| IF LPAREN e=expr[false] { conds.addChild(e); } RPAREN SEMI
	;

evaluation [ CollectNode<EvalStatementsNode> evals, CollectNode<OrderedReplacementsNode> orderedReplacements,
				AnonymousScopeNamer namer, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		EvalStatementsNode curEval = null;
		OrderedReplacementsNode curOrderedRepl = null;
	}
	: e=EVAL
			{ namer.defEval(null, getCoords(e));
			  curEval = new EvalStatementsNode(getCoords(e), namer.eval().toString());
			  evals.addChild(curEval);
			}
		LBRACE { env.pushScope(namer.eval()); }
			( c=computation[false, context|BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, directlyNestingLHSGraph] { curEval.addChild(c); } )*
		RBRACE { env.popScope(); }
	| eh=EVALHERE
			{ namer.defEval(null, getCoords(eh));
			  curOrderedRepl = new OrderedReplacementsNode(getCoords(eh), namer.eval().toString());
			  orderedReplacements.addChild(curOrderedRepl);
			}
		LBRACE { env.pushScope(namer.eval()); }
			( c=computation[false, context|BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, directlyNestingLHSGraph] { curOrderedRepl.addChild(c); } )*
		RBRACE { env.popScope(); }
	;

yielding [ CollectNode<EvalStatementsNode> evals, AnonymousScopeNamer namer, int context, PatternGraphNode directlyNestingLHSGraph]
	@init{
		EvalStatementsNode curEval = null;
	}
	: y=YIELD
			{ namer.defYield(null, getCoords(y));
			  curEval = new EvalStatementsNode(getCoords(y), namer.yield().toString());
			  evals.addChild(curEval); }
		LBRACE { env.pushScope(namer.yield()); }
			( c=computation[true, context|BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, directlyNestingLHSGraph] { curEval.addChild(c); } )*
		RBRACE { env.popScope(); }
	;
	
rets[CollectNode<ExprNode> res, int context]
	@init{
		boolean multipleReturns = ! res.getChildren().isEmpty();
	}

	: r=RETURN
		{
			if ( multipleReturns ) {
				reportError(getCoords(r), "multiple occurrence of return statement in one rule");
			}
			if ( (context & BaseNode.CONTEXT_ACTION_OR_PATTERN) == BaseNode.CONTEXT_PATTERN) {
				reportError(getCoords(r), "return statement only allowed in actions, not in pattern type declarations");
			}
			res.setCoords(getCoords(r));
		}
		LPAREN exp=expr[false] { if ( !multipleReturns ) res.addChild(exp); }
		( COMMA exp=expr[false] { if ( !multipleReturns ) res.addChild(exp); } )*
		RPAREN
	;

deleteStmt[CollectNode<IdentNode> res]
	: DELETE LPAREN paramListOfEntIdentUse[res] RPAREN
	;

paramListOfEntIdentUse[CollectNode<IdentNode> res]
	: id=entIdentUse { res.addChild(id); }	( COMMA id=entIdentUse { res.addChild(id); } )*
	;

alternativeOrIteratedRewriteUsage[CollectNode<OrderedReplacementsNode> orderedReplacements]
	: a=ALTERNATIVE id=altIdentUse SEMI
		{ OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(id.getCoords(), id.toString());
		  orderedReplacements.addChild(curOrderedRepl);
		  curOrderedRepl.addChild(new AlternativeReplNode(id));
		}
	| i=ITERATED id=iterIdentUse SEMI
		{ OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(id.getCoords(), id.toString());
		  orderedReplacements.addChild(curOrderedRepl);
		  curOrderedRepl.addChild(new IteratedReplNode(id));
		}
	;

execStmt[CollectNode<BaseNode> imperativeStmts, int context, PatternGraphNode directlyNestingLHSGraph] returns[ExecNode exec = null]
	: e=EXEC { env.pushScope("exec_", getCoords(e)); } { exec = new ExecNode(getCoords(e)); } LPAREN xgrs[exec] RPAREN
		{ if(imperativeStmts!=null) imperativeStmts.addChild(exec); } { env.popScope(); }
	;

emitStmt[CollectNode<BaseNode> imperativeStmts, CollectNode<OrderedReplacementsNode> orderedReplacements]
	@init{ EmitNode emit = null; boolean isHere = false; boolean isDebug = false; }
	
	: (e=EMIT | e=EMITDEBUG { isDebug = true; } | e=EMITHERE { isHere = true; } | e=EMITHEREDEBUG { isHere = true; isDebug = true; })
		{ emit = new EmitNode(getCoords(e), isDebug); }
		LPAREN
			exp=expr[false] { emit.addChild(exp); }
			( COMMA exp=expr[false] { emit.addChild(exp); } )*
		RPAREN
		{ 
			if(isHere) {
				OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(getCoords(e), e.toString());
				orderedReplacements.addChild(curOrderedRepl);
				curOrderedRepl.addChild(emit);
			} else {
				imperativeStmts.addChild(emit);
			}
		}
	;

typeConstraint returns [ TypeExprNode constr = null ]
	: BACKSLASH te=typeUnaryExpr { constr = te; } 
	;

typeAddExpr returns [ TypeExprNode res = null ]
	: typeUse=typeIdentUse { res = new TypeConstraintNode(typeUse); }
		(t=PLUS op=typeUnaryExpr
			{ res = new TypeBinaryExprNode(getCoords(t), TypeExprNode.UNION, res, op); }
		)*
	;

typeUnaryExpr returns [ TypeExprNode res = null ]
	: typeUse=typeIdentUse { res = new TypeConstraintNode(typeUse); }
	| LPAREN te=typeAddExpr RPAREN { res = te; } 
	;


	
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Types / Model
////////////////////////////////////////////////////////////////////////////////////////////////////////////////



textTypes returns [ ModelNode model = null ]
	@init{
		CollectNode<ModelNode> modelChilds = new CollectNode<ModelNode>();
		CollectNode<IdentNode> types = new CollectNode<IdentNode>();
		CollectNode<IdentNode> packages = new CollectNode<IdentNode>();
		CollectNode<IdentNode> externalFuncs = new CollectNode<IdentNode>();
		CollectNode<IdentNode> externalProcs = new CollectNode<IdentNode>();
		CollectNode<IdentNode> indices = new CollectNode<IdentNode>();
		IdentNode id = env.getDummyIdent();

		String modelName = Util.removeFileSuffix(Util.removePathPrefix(getFilename()), "gm");

		id = new IdentNode(
			env.define(ParserEnvironment.MODELS, modelName,
			new de.unika.ipd.grgen.parser.Coords(0, 0, getFilename())));
	}

	: ( usingDecl[modelChilds] )*
	
	specialClasses = typeDecls[types, packages, externalFuncs, externalProcs, indices] EOF
		{
			if(modelChilds.getChildren().size() == 0)
				modelChilds.addChild(env.getStdModel());
			model = new ModelNode(id, packages, types, externalFuncs, externalProcs, indices, modelChilds,
				$specialClasses.isEmitClassDefined, $specialClasses.isEmitGraphClassDefined, $specialClasses.isCopyClassDefined, 
				$specialClasses.isEqualClassDefined, $specialClasses.isLowerClassDefined,
				$specialClasses.isUniqueDefined, $specialClasses.isUniqueIndexDefined,
				$specialClasses.isoParallel);
		}
	;

typeDecls [ CollectNode<IdentNode> types, CollectNode<IdentNode> packages,
            CollectNode<IdentNode> externalFuncs, CollectNode<IdentNode> externalProcs, 
			CollectNode<IdentNode> indices ]
		returns [ boolean isEmitClassDefined = false, boolean isEmitGraphClassDefined = false, boolean isCopyClassDefined = false, 
				  boolean isEqualClassDefined = false, boolean isLowerClassDefined = false,
				  boolean isUniqueDefined = false, boolean isUniqueIndexDefined = false,
				  int isoParallel = 0;]
	@init{
		boolean graphFound = false;
	}
	: (
		type=typeDecl { types.addChild(type); }
	  |
  		pack=packageDecl { packages.addChild(pack); }
	  |
		externalFunctionOrProcedureDecl[externalFuncs, externalProcs]
	  |
	    NODE EDGE i=IDENT SEMI { if(!i.getText().equals("unique")) reportError(getCoords(i), "malformed \"node edge unique;\""); else $isUniqueDefined = true; }
	  |
	    EXTERNAL EMIT (i=IDENT { if(!i.getText().equals("graph")) reportError(getCoords(i), "malformed \"external emit graph class;\""); else graphFound = true;} )? c=CLASS SEMI { if(graphFound) $isEmitGraphClassDefined = true; else $isEmitClassDefined = true; }
	  |
	    EXTERNAL COPY c=CLASS SEMI { $isCopyClassDefined = true; }
	  |
	    EXTERNAL EQUAL c=CLASS SEMI { $isEqualClassDefined = true; }
	  |
	    EXTERNAL LT c=CLASS SEMI { $isLowerClassDefined = true; }
	  |
		res = indexDecl[indices] { $isUniqueIndexDefined = res; }
	  |
	    FOR i=IDENT LBRACK j=IDENT ASSIGN con=constant RBRACK
				{
					if(!i.getText().equals("equalsAny"))
						reportError(getCoords(i), "malformed \"for equalsAny[parallelize=k];\"");
					else if(!j.getText().equals("parallelize"))
						reportError(getCoords(j), "malformed \"for equalsAny[parallelize=k];\"");
					else {
						Object ip = ((ConstNode) con).getValue();
						if(!(ip instanceof Integer))
							reportError(getCoords(i), "\"for equalsAny[parallelize=k];\" requires an integer constant");
						else
							$isoParallel = (Integer)ip;
					}
				}
			SEMI
	  )*
	;

indexDecl [ CollectNode<IdentNode> indices ] returns [ boolean res = false ]
options { k = 3; }
	: INDEX id=indexIdentDecl LBRACE indexDeclBody[id] RBRACE
		{
			indices.addChild(id);
		}
	| INDEX i=IDENT SEMI
		{ 
			if(i.getText().equals("unique"))
				res = true;
			else
				reportError(getCoords(i), "only unique allowed for an index declaration without body, not \"" + i.getText() + "\"");
		}
	;

indexDeclBody [ IdentNode id ]
	: type=typeIdentUse DOT member=memberIdentUse
		{
			id.setDecl(new AttributeIndexDeclNode(id, type, member));
		}
	| i=IDENT LPAREN startNodeType=typeIdentUse (COMMA incidentEdgeType=typeIdentUse (COMMA adjacentNodeType=typeIdentUse)?)? RPAREN 
		{
			id.setDecl(new IncidenceCountIndexDeclNode(id, i.getText(), startNodeType, incidentEdgeType, adjacentNodeType, env));
		}
	;

externalFunctionOrProcedureDecl [ CollectNode<IdentNode> externalFuncs, CollectNode<IdentNode> externalProcs ]
	@init{
		CollectNode<BaseNode> returnTypes = new CollectNode<BaseNode>();
	}
	: EXTERNAL f=FUNCTION id=funcOrExtFuncIdentDecl params=paramTypes COLON ret=returnType SEMI
		{
			id.setDecl(new ExternalFunctionDeclNode(id, params, ret, false));
			externalFuncs.addChild(id);
		}
	| EXTERNAL p=PROCEDURE id=funcOrExtFuncIdentDecl params=paramTypes (COLON LPAREN (returnTypeList[returnTypes])? RPAREN)? SEMI
		{
			id.setDecl(new ExternalProcedureDeclNode(id, params, returnTypes, false));
			externalProcs.addChild(id);
		}
	;

paramTypes returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN (returnTypeList[res])? RPAREN // we reuse the return type list cause it's of format we need
	;

typeDecl returns [ IdentNode res = env.getDummyIdent() ]
	: d=classDecl { res = d; } 
	| d=enumDecl { res = d; } 
	| d=extClassDecl { res = d; }
	;

packageDecl returns [ IdentNode res = env.getDummyIdent() ]
	@init{
		CollectNode<IdentNode> types = new CollectNode<IdentNode>(); 
	}
	: PACKAGE id=packageIdentDecl LBRACE { env.pushScope(id); }
		( type=typeDecl { types.addChild(type); }
		)*
	  RBRACE
	  {
			PackageTypeNode pt = new PackageTypeNode(types);
			id.setDecl(new TypeDeclNode(id, pt));
			res = id;
	  }
	  { env.popScope(); }
	;
	
classDecl returns [ IdentNode res = env.getDummyIdent() ]
	@init{ mods = 0; }

	: (mods=typeModifiers)? (d=edgeClassDecl[mods] { res = d; } | d=nodeClassDecl[mods] { res = d; } )
	;

typeModifiers returns [ int res = 0; ]
	@init{ mod = 0; }

	: (mod=typeModifier { res |= mod; })+
	;

typeModifier returns [ int res = 0; ]
	: ABSTRACT { res |= InheritanceTypeNode.MOD_ABSTRACT; }
	| CONST { res |= InheritanceTypeNode.MOD_CONST; }
	;

/**
 * An edge class decl makes a new type decl node with the declaring id and
 * a new edge type node as children
 */
edgeClassDecl[int modifiers] returns [ IdentNode res = env.getDummyIdent() ]
	@init{
		boolean arbitrary = false;
		boolean undirected = false;
	}

	:	(
			ARBITRARY
			{
				arbitrary = true;
				modifiers |= InheritanceTypeNode.MOD_ABSTRACT;
			}
		|	DIRECTED // do nothing, that's default
		|	UNDIRECTED { undirected = true; }
		)?
		EDGE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
	  	ext=edgeExtends[id, arbitrary, undirected] cas=connectAssertions { env.pushScope(id); }
		(
			LBRACE body=classBody[id, false] RBRACE
		|	SEMI
			{ body = new CollectNode<BaseNode>(); }
		)
		{
			EdgeTypeNode et;
			if (arbitrary) {
				et = new ArbitraryEdgeTypeNode(ext, cas, body, modifiers, externalName);
			}
			else {
				if (undirected) {
					et = new UndirectedEdgeTypeNode(ext, cas, body, modifiers, externalName);
				} else {
					et = new DirectedEdgeTypeNode(ext, cas, body, modifiers, externalName);
				}
			}
			id.setDecl(new TypeDeclNode(id, et));
			res = id;
		}
		{ env.popScope(); }
  ;

nodeClassDecl[int modifiers] returns [ IdentNode res = env.getDummyIdent() ]
	: 	NODE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
	  	ext=nodeExtends[id] { env.pushScope(id); }
		(
			LBRACE body=classBody[id, true] RBRACE
		|	SEMI
			{ body = new CollectNode<BaseNode>(); }
		)
		{
			NodeTypeNode nt = new NodeTypeNode(ext, body, modifiers, externalName);
			id.setDecl(new TypeDeclNode(id, nt));
			res = id;
		}
		{ env.popScope(); }
	;

validIdent returns [ String id = "" ]
	:	i=~GT
		{
			if(i.getType() != IDENT && !env.isLexerKeyword(i.getText()))
				reportError(getCoords(i), "\"" + i.getText() + "\" is not a valid identifier");
			id = i.getText();
		}
	;

fullQualIdent returns [ String id = "" ]
	:	i=validIdent { id = i; } 
	 	(DOT id2=validIdent { id += "." + id2; })*
	;

connectAssertions returns [ CollectNode<ConnAssertNode> c = new CollectNode<ConnAssertNode>() ]
	: CONNECT connectAssertion[c]
		( COMMA connectAssertion[c] )*
	|
	;

connectAssertion [ CollectNode<ConnAssertNode> c ]
options { k = *; }
	: src=typeIdentUse srcRange=rangeSpec DOUBLE_RARROW tgt=typeIdentUse tgtRange=rangeSpec
		{ c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange, false)); }
	| src=typeIdentUse srcRange=rangeSpec DOUBLE_LARROW tgt=typeIdentUse tgtRange=rangeSpec
		{ c.addChild(new ConnAssertNode(tgt, tgtRange, src, srcRange, false)); }
	| src=typeIdentUse srcRange=rangeSpec QMMQ tgt=typeIdentUse tgtRange=rangeSpec
		{ c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange, true)); }
	| src=typeIdentUse srcRange=rangeSpec MINUSMINUS tgt=typeIdentUse tgtRange=rangeSpec
		{ c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange, true)); }
	| co=COPY EXTENDS
		{ c.addChild(new ConnAssertNode(getCoords(co))); }
	;

edgeExtends [IdentNode clsId, boolean arbitrary, boolean undirected] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS edgeExtendsCont[clsId, c, undirected]
	|	{
			if (arbitrary) {
				c.addChild(env.getArbitraryEdgeRoot());
			} else {
				if(undirected) {
					c.addChild(env.getUndirectedEdgeRoot());
				} else {
					c.addChild(env.getDirectedEdgeRoot());
				}
			}
		}
	;

edgeExtendsCont [ IdentNode clsId, CollectNode<IdentNode> c, boolean undirected ]
	: e=typeIdentUse
		{
			if ( ! ((IdentNode)e).toString().equals(clsId.toString()) )
				c.addChild(e);
			else
				reportError(e.getCoords(), "A class must not extend itself");
		}
	(COMMA e=typeIdentUse
		{
			if ( ! ((IdentNode)e).toString().equals(clsId.toString()) )
				c.addChild(e);
			else
				reportError(e.getCoords(), "A class must not extend itself");
		}
	)*
		{
			if (c.getChildren().size() == 0) {
				if (undirected) {
					c.addChild(env.getUndirectedEdgeRoot());
				} else {
					c.addChild(env.getDirectedEdgeRoot());
				}
			}
		}
	;

nodeExtends [ IdentNode clsId ] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS nodeExtendsCont[clsId, c]
	|	{ c.addChild(env.getNodeRoot()); }
	;

nodeExtendsCont [IdentNode clsId, CollectNode<IdentNode> c ]
	: n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	(COMMA n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	)*
		{ if ( c.getChildren().size() == 0 ) c.addChild(env.getNodeRoot()); }
	;

classBody [IdentNode clsId, boolean isNode] returns [ CollectNode<BaseNode> c = new CollectNode<BaseNode>() ]
	:	(
			(
				basicAndContainerDecl[c] SEMI
			|
				funcMethod=inClassFunctionDecl[clsId, isNode] { c.addChild(funcMethod); }
			|
				procMethod=inClassProcedureDecl[clsId, isNode] { c.addChild(procMethod); }
			|
				init=initExpr { c.addChild(init); } SEMI
			|
				constr=constrDecl[clsId] { c.addChild(constr); } SEMI
			)
		)*
	;

enumDecl returns [ IdentNode res = env.getDummyIdent() ]
	@init{
		CollectNode<EnumItemNode> c = new CollectNode<EnumItemNode>();
	}

	: ENUM id=typeIdentDecl { env.pushScope(id); }
		LBRACE enumList[id, c]
		{
			TypeNode enumType = new EnumTypeNode(c);
			id.setDecl(new TypeDeclNode(id, enumType));
			res = id;
		}
		RBRACE { env.popScope(); }
	;

enumList[ IdentNode enumType, CollectNode<EnumItemNode> collect ]
	@init{
		int pos = 0;
	}

	: init=enumItemDecl[enumType, collect, env.getZero(), pos++]
		( COMMA init=enumItemDecl[enumType, collect, init, pos++] )*
	;

enumItemDecl [ IdentNode type, CollectNode<EnumItemNode> coll, ExprNode defInit, int pos ]
				returns [ ExprNode res = env.initExprNode() ]
	@init{
		ExprNode value;
	}

	: id=entIdentDecl	( ASSIGN init=expr[true] )? //'true' means that expr initializes an enum item
		{
			if(init != null) {
				value = init;
			} else {
				value = defInit;
			}
			EnumItemNode memberDecl = new EnumItemNode(id, type, value, pos);
			id.setDecl(memberDecl);
			coll.addChild(memberDecl);
			OpNode add = new ArithmeticOpNode(id.getCoords(), OperatorSignature.ADD);
			add.addChild(value);
			add.addChild(env.getOne());
			res = add;
		}
	;

extClassDecl returns [ IdentNode res = env.getDummyIdent() ]
	: EXTERNAL c=CLASS id=typeIdentDecl 
	  ext=extExtends[id] { env.pushScope(id); }
		(
			LBRACE body=extClassBody[id] RBRACE
		|	SEMI
			{ body = new CollectNode<BaseNode>(); }
		)
		{
			ExternalTypeNode et = new ExternalTypeNode(ext, body);
			id.setDecl(new TypeDeclNode(id, et));
			res = id;
		}
		{ env.popScope(); }
	;

extExtends [ IdentNode clsId ] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: (EXTENDS extExtendsCont[clsId, c])?
	;

extExtendsCont [IdentNode clsId, CollectNode<IdentNode> c ]
	: n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	(COMMA n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	)*
	;

extClassBody [IdentNode clsId] returns [ CollectNode<BaseNode> c = new CollectNode<BaseNode>() ]
	:	(
			(
				funcMethod=inClassExtFunctionDecl[clsId] { c.addChild(funcMethod); }
			|
				procMethod=inClassExtProcedureDecl[clsId] { c.addChild(procMethod); }
			)
		)*
	;

inClassExtFunctionDecl [ IdentNode clsId ] returns [ ExternalFunctionDeclNode res = null ]
	: EXTERNAL f=FUNCTION id=methodOrExtMethodIdentDecl { env.pushScope(id); } params=paramTypes COLON retType=returnType SEMI { env.popScope(); }
		{
			res = new ExternalFunctionDeclNode(id, params, retType, true);
			id.setDecl(res);
		}
	;

inClassExtProcedureDecl [ IdentNode clsId ] returns [ ExternalProcedureDeclNode res = null ]
	@init{
		CollectNode<BaseNode> retTypes = new CollectNode<BaseNode>();
	}
	
	: EXTERNAL pr=PROCEDURE id=methodOrExtMethodIdentDecl { env.pushScope(id); } params=paramTypes
		(COLON LPAREN (returnTypeList[retTypes])? RPAREN)? SEMI { env.popScope(); }
		{
			res = new ExternalProcedureDeclNode(id, params, retTypes, true);
			id.setDecl(res);
		}
	;
	
basicAndContainerDecl [ CollectNode<BaseNode> c ]
	@init{
		id = env.getDummyIdent();
		MemberDeclNode decl = null;
		boolean isConst = false;
	}

	:	(
			ABSTRACT ( CONST { isConst = true; } )? id=entIdentDecl
			{
				decl = new AbstractMemberDeclNode(id, isConst);
				c.addChild(decl);
			}
		|
			( CONST { isConst = true; } )? id=entIdentDecl COLON 
			(
				type=typeIdentUse
				{
					decl = new MemberDeclNode(id, type, isConst);
					id.setDecl(decl);
					c.addChild(decl);
				}
				(
					init=initExprDecl[decl.getIdentNode()]
					{
						c.addChild(init);
						if(isConst)
							decl.setConstInitializer(init);
					}
				)?
			|
				MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT
				{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
					decl = new MemberDeclNode(id, MapTypeNode.getMapType(keyType, valueType), isConst);
					id.setDecl(decl);
					c.addChild(decl);
				}
				(
					ASSIGN init2=initMapExpr[decl.getIdentNode(), null]
					{
						c.addChild(init2);
						if(isConst)
							decl.setConstInitializer(init2);
					}
				)?
			|
				SET LT valueType=typeIdentUse GT
				{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
					decl = new MemberDeclNode(id, SetTypeNode.getSetType(valueType), isConst);
					id.setDecl(decl);
					c.addChild(decl);
				}
				(
					ASSIGN init3=initSetExpr[decl.getIdentNode(), null]
					{
						c.addChild(init3);
						if(isConst)
							decl.setConstInitializer(init3);
					}
				)?
			|
				ARRAY LT valueType=typeIdentUse GT
				{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
					decl = new MemberDeclNode(id, ArrayTypeNode.getArrayType(valueType), isConst);
					id.setDecl(decl);
					c.addChild(decl);
				}
				(
					ASSIGN init4=initArrayExpr[decl.getIdentNode(), null]
					{
						c.addChild(init4);
						if(isConst)
							decl.setConstInitializer(init4);
					}
				)?
			|
				DEQUE LT valueType=typeIdentUse GT
				{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
					decl = new MemberDeclNode(id, DequeTypeNode.getDequeType(valueType), isConst);
					id.setDecl(decl);
					c.addChild(decl);
				}
				(
					ASSIGN init5=initDequeExpr[decl.getIdentNode(), null]
					{
						c.addChild(init5);
						if(isConst)
							decl.setConstInitializer(init5);
					}
				)?
			)
		)
	;

inClassFunctionDecl [ IdentNode clsId, boolean isNode ] returns [ FunctionDeclNode res = null ]
	@init{
		CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>();
	}
	
	: f=FUNCTION id=methodOrExtMethodIdentDecl { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, PatternGraphNode.getInvalid()]
		COLON retType=returnType
		LBRACE
			{
				if(isNode)
					evals.addChild(new DefDeclStatementNode(getCoords(f), new SingleNodeConnNode(
							new NodeDeclNode(new IdentNode(env.define(ParserEnvironment.ENTITIES, "this", getCoords(f))), new IdentNode(env.occurs(ParserEnvironment.TYPES, clsId.toString(), clsId.getCoords())), false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, TypeExprNode.getEmpty(), PatternGraphNode.getInvalid(), false, true)),
						BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD));
				else
					evals.addChild(new DefDeclStatementNode(getCoords(f), new ConnectionNode(
							env.getDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION, PatternGraphNode.getInvalid()),
							new EdgeDeclNode(new IdentNode(env.define(ParserEnvironment.ENTITIES, "this", getCoords(f))), new IdentNode(env.occurs(ParserEnvironment.TYPES, clsId.toString(), clsId.getCoords())), false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, TypeExprNode.getEmpty(), PatternGraphNode.getInvalid(), false, true),
							env.getDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, PatternGraphNode.getInvalid()), ConnectionNode.DIRECTED, ConnectionNode.NO_REDIRECTION),
						BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD));
			}
			( c=computation[false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, PatternGraphNode.getInvalid()] { evals.addChild(c); } )*
		RBRACE { env.popScope(); }
		{
			res = new FunctionDeclNode(id, evals, params, retType, true);
			id.setDecl(res);
		}
	;

inClassProcedureDecl [ IdentNode clsId, boolean isNode ] returns [ ProcedureDeclNode res = null ]
	@init{
		CollectNode<BaseNode> retTypes = new CollectNode<BaseNode>();
		CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>();
	}
	
	: pr=PROCEDURE id=methodOrExtMethodIdentDecl { env.pushScope(id); } params=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphNode.getInvalid()]
		(COLON LPAREN (returnTypeList[retTypes])? RPAREN)?
		LBRACE
			{
				if(isNode)
					evals.addChild(new DefDeclStatementNode(getCoords(pr), new SingleNodeConnNode(
							new NodeDeclNode(new IdentNode(env.define(ParserEnvironment.ENTITIES, "this", getCoords(pr))), new IdentNode(env.occurs(ParserEnvironment.TYPES, clsId.toString(), clsId.getCoords())), false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, TypeExprNode.getEmpty(), PatternGraphNode.getInvalid(), false, true)),
						BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD));
				else
					evals.addChild(new DefDeclStatementNode(getCoords(pr), new ConnectionNode(
							env.getDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphNode.getInvalid()), 
							new EdgeDeclNode(new IdentNode(env.define(ParserEnvironment.ENTITIES, "this", getCoords(pr))), new IdentNode(env.occurs(ParserEnvironment.TYPES, clsId.toString(), clsId.getCoords())), false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, TypeExprNode.getEmpty(), PatternGraphNode.getInvalid(), false, true),
							env.getDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphNode.getInvalid()), ConnectionNode.DIRECTED, ConnectionNode.NO_REDIRECTION), 
						BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD));
			}
			( c=computation[false, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphNode.getInvalid()] { evals.addChild(c); } )*
		RBRACE { env.popScope(); }
		{
			res = new ProcedureDeclNode(id, evals, params, retTypes, true);
			id.setDecl(res);
		}
	;

initExpr returns [ MemberInitNode res = null ]
	: id=entIdentUse init=initExprDecl[id] { res = init; }
	;

initExprDecl [IdentNode id] returns [ MemberInitNode res = null ]
	: a=ASSIGN e=expr[false]
		{
			res = new MemberInitNode(getCoords(a), id, e);
		}
	;

initMapExpr [IdentNode id, MapTypeNode mapType] returns [ ExprNode res = null ]
	@init{ MapInitNode mapInit = null; }
	: l=LBRACE { res = mapInit = new MapInitNode(getCoords(l), id, mapType); }
		( item1=mapItem { mapInit.addMapItem(item1); }
			( COMMA item2=mapItem { mapInit.addMapItem(item2); } )*
		)?
	  RBRACE
	| lp=LPAREN value=expr[false]
		{ res = new MapCopyConstructorNode(getCoords(lp), id, mapType, value); }
	  RPAREN 
	;

initSetExpr [IdentNode id, SetTypeNode setType] returns [ ExprNode res = null ]
	@init{ SetInitNode setInit = null; }
	: l=LBRACE { res = setInit = new SetInitNode(getCoords(l), id, setType); }	
		( item1=setItem { setInit.addSetItem(item1); }
			( COMMA item2=setItem { setInit.addSetItem(item2); } )*
		)?
	  RBRACE
	| lp=LPAREN value=expr[false]
		{ res = new SetCopyConstructorNode(getCoords(lp), id, setType, value); }
	  RPAREN 
	;

initArrayExpr [IdentNode id, ArrayTypeNode arrayType] returns [ ExprNode res = null ]
	@init{ ArrayInitNode arrayInit = null; }
	: l=LBRACK { res = arrayInit = new ArrayInitNode(getCoords(l), id, arrayType); }	
		( item1=arrayItem { arrayInit.addArrayItem(item1); }
			( COMMA item2=arrayItem { arrayInit.addArrayItem(item2); } )*
		)?
	  RBRACK
	| lp=LPAREN value=expr[false]
		{ res = new ArrayCopyConstructorNode(getCoords(lp), id, arrayType, value); }
	  RPAREN 
	;

initDequeExpr [IdentNode id, DequeTypeNode dequeType] returns [ ExprNode res = null ]
	@init{ DequeInitNode dequeInit = null; }
	: l=RBRACK { res = dequeInit = new DequeInitNode(getCoords(l), id, dequeType); }	
		( item1=dequeItem { dequeInit.addDequeItem(item1); }
			( COMMA item2=dequeItem { dequeInit.addDequeItem(item2); } )*
		)?
	  LBRACK
	| lp=LPAREN value=expr[false]
		{ res = new DequeCopyConstructorNode(getCoords(lp), id, dequeType, value); }
	  RPAREN 
	;

mapItem returns [ MapItemNode res = null ]
	: key=expr[false] a=RARROW value=expr[false]
		{
			res = new MapItemNode(getCoords(a), key, value);
		}
	;

setItem returns [ SetItemNode res = null ]
	: value=expr[false]
		{
			res = new SetItemNode(value.getCoords(), value);
		}
	;

arrayItem returns [ ArrayItemNode res = null ]
	: value=expr[false]
		{
			res = new ArrayItemNode(value.getCoords(), value);
		}
	;

dequeItem returns [ DequeItemNode res = null ]
	: value=expr[false]
		{
			res = new DequeItemNode(value.getCoords(), value);
		}
	;

constrDecl [IdentNode clsId] returns [ ConstructorDeclNode res = null ]
	@init {
		CollectNode<ConstructorParamNode> params = new CollectNode<ConstructorParamNode>();
	}
	
	: id=typeIdentUse LPAREN constrParamList[params] RPAREN
		{
			res = new ConstructorDeclNode(id, params);
			
			if(!id.toString().equals(clsId.toString()) )
				reportError(id.getCoords(), "A constructor must have the name of the containing class");
		}
	;

constrParamList [ CollectNode<ConstructorParamNode> params ]
	: p=constrParam { params.addChild(p); } ( COMMA p=constrParam { params.addChild(p); } )*
	;

constrParam returns [ ConstructorParamNode res = null ]
	: id=entIdentUse ( ASSIGN e=expr[false] )?
		{
			res = new ConstructorParamNode(id, e);
		}
	;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Base  --- copied to GrGenEmbeddedExec.g as needed there, I don't know what abominations will arise if they differ
////////////////////////////////////////////////////////////////////////////////////////////////////////////////


memberIdent returns [ Token t = null ]
	: i=IDENT { t = i; }
	| r=REPLACE { r.setType(IDENT); t = r; }             // HACK: For string replace function... better choose another name?
	; 

packageIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.PACKAGES, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

typeIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.TYPES, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

rhsIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.REPLACES, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

entIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

actionIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ACTIONS, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

altIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ALTERNATIVES, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

iterIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ITERATEDS, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;
	
negIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ITERATEDS, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

idptIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.INDEPENDENTS, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

patIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.PATTERNS, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

funcOrExtFuncIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

methodOrExtMethodIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;
	
indexIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=IDENT 
		{ if(i!=null) res = new IdentNode(env.define(ParserEnvironment.INDICES, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
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

	
annotations returns [ Annotations annots = new DefaultAnnotations() ]
	: LBRACK keyValuePairs[annots] RBRACK
	;

keyValuePairs [ Annotations annots ]
	: keyValuePair[annots] (COMMA keyValuePair[annots])*
	;

keyValuePair [ Annotations annots ]
	: id=IDENT
		(
			ASSIGN c=constant
			{ annots.put(id.getText(), ((ConstNode) c).getValue()); }
		|
			{ annots.put(id.getText(), true); }
		)
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
// Expressions
//////////////////////////////////////////


computations [ boolean onLHS, int context, PatternGraphNode directlyNestingLHSGraph ] 
	returns [ CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>() ]
	: ( 
		c=computation[onLHS, context, directlyNestingLHSGraph] { evals.addChild(c); }
	  )*
	;
	
computation [ boolean onLHS, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EvalStatementNode res = null ]
options { k = 5; }
	@init{
		int cat = -1; // compound assign type
		int ccat = CompoundAssignNode.NONE; // changed compound assign type
		BaseNode tgtChanged = null;
		CollectNode<ExprNode> subpatternConn = new CollectNode<ExprNode>();
		boolean yielded = false, methodCall = false, attributeMethodCall = false, packPrefix = false;
		CollectNode<ExprNode> returnValues = new CollectNode<ExprNode>();
		CollectNode<ProjectionExprNode> targetProjs = new CollectNode<ProjectionExprNode>();
		CollectNode<EvalStatementNode> targets = new CollectNode<EvalStatementNode>();
		MultiStatementNode ms = new MultiStatementNode();
	}

	: (DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse a=ASSIGN e=expr[false] SEMI//'false' because this rule is not used for the assignments in enum item decls
		{ res = new AssignNode(getCoords(a), new QualIdentNode(getCoords(d), owner, member), e, context); }
		{ if(onLHS) reportError(getCoords(d), "Assignment to an attribute is forbidden in LHS eval, only yield assignment to a def variable allowed."); }
	|
	  (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse a=ASSIGN e=expr[false] SEMI
		{ res = new AssignNode(getCoords(a), new IdentExprNode(variable, yielded), e, context, onLHS); }
	|
	  vis=visited a=ASSIGN e=expr[false] SEMI
		{ res = new AssignVisitedNode(getCoords(a), vis, e, context); }
		{ if(onLHS) reportError(getCoords(a), "Assignment to a visited flag is forbidden in LHS eval."); }
	|
	  n=NAMEOF LPAREN (id=expr[false])? RPAREN a=ASSIGN e=expr[false] SEMI
	    { res = new AssignNameofNode(getCoords(a), id, e, context); }
		{ if(onLHS) reportError(getCoords(d), "Name assignment is forbidden in LHS eval."); }
	|
	  (DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse LBRACK idx=expr[false] RBRACK a=ASSIGN e=expr[false] SEMI //'false' because this rule is not used for the assignments in enum item decls
		{ res = new AssignIndexedNode(getCoords(a), new QualIdentNode(getCoords(d), owner, member), e, idx, context); }
		{ if(onLHS) reportError(getCoords(d), "Indexed assignment to an attribute is forbidden in LHS eval, only yield indexed assignment to a def variable allowed."); }
	|
	  (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse LBRACK idx=expr[false] RBRACK a=ASSIGN e=expr[false] SEMI
		{ res = new AssignIndexedNode(getCoords(a), new IdentExprNode(variable, yielded), e, idx, context, onLHS); }
	| 
	  (DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse 
		(BOR_ASSIGN { cat = CompoundAssignNode.UNION; } | BAND_ASSIGN { cat = CompoundAssignNode.INTERSECTION; }
			| BACKSLASH_ASSIGN { cat = CompoundAssignNode.WITHOUT; } | PLUS_ASSIGN { cat = CompoundAssignNode.CONCATENATE; })
		e=expr[false] ( at=assignTo { ccat = $at.ccat; tgtChanged = $at.tgtChanged; } )? SEMI
			{ res = new CompoundAssignNode(getCoords(a), new QualIdentNode(getCoords(d), owner, member), cat, e, ccat, tgtChanged); }
			{ if(onLHS) reportError(getCoords(d), "Assignment to an attribute is forbidden in LHS eval, only yield assignment to a def variable allowed."); }
			{ if(cat==CompoundAssignNode.CONCATENATE && ccat!=CompoundAssignNode.NONE) reportError(getCoords(d), "No change assignment allowed for array|deque concatenation."); }
	|
	  (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse 
		(BOR_ASSIGN { cat = CompoundAssignNode.UNION; } | BAND_ASSIGN { cat = CompoundAssignNode.INTERSECTION; } 
			| BACKSLASH_ASSIGN { cat = CompoundAssignNode.WITHOUT; } | PLUS_ASSIGN { cat = CompoundAssignNode.CONCATENATE; })
		e=expr[false] ( at=assignTo { ccat = $at.ccat; tgtChanged = $at.tgtChanged; } )? SEMI
			{ res = new CompoundAssignNode(getCoords(a), new IdentExprNode(variable, yielded), cat, e, ccat, tgtChanged); }
			{ if(cat==CompoundAssignNode.CONCATENATE && ccat!=CompoundAssignNode.NONE) reportError(getCoords(d), "No change assignment allowed for array|deque concatenation."); }
	|
	  de=defEntityToBeYieldedTo[null, null, context, directlyNestingLHSGraph] SEMI
			{ res=new DefDeclStatementNode(de.getCoords(), de, context); }
	|
	  r=RETURN ( retValues=paramExprs[false] { returnValues = retValues; } )? SEMI
			{ res=new ReturnStatementNode(getCoords(r), returnValues); }
	|
	  f=FOR LPAREN { env.pushScope("for", getCoords(f)); } fc=forContent[getCoords(f), onLHS, context, directlyNestingLHSGraph]
			{ res=fc; }
	|
	  c=CONTINUE SEMI
			{ res=new ContinueStatementNode(getCoords(c)); }
	|
	  b=BREAK SEMI
			{ res=new BreakStatementNode(getCoords(b)); }
	|
	  ie=ifelse[onLHS, context, directlyNestingLHSGraph]
			{ res=ie; }
	|
	  sc=switchcase[onLHS, context, directlyNestingLHSGraph]
			{ res=sc; }
	|
	  w=WHILE LPAREN e=expr[false] RPAREN
		LBRACE { env.pushScope("while", getCoords(w)); }
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
			{ res=new WhileStatementNode(getCoords(w), e, cs); }
	|
	  d=DO 
		LBRACE { env.pushScope("do", getCoords(d)); }
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
	  WHILE LPAREN e=expr[false] RPAREN
			{ res=new DoWhileStatementNode(getCoords(d), cs, e); }
	|
	  (l=LPAREN tgts=targets[onLHS, getCoords(l), ms, context, directlyNestingLHSGraph] RPAREN a=ASSIGN { targetProjs = $tgts.tgtProjs; targets = $tgts.tgts; } )? 
		( (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse d=DOT { methodCall = true; } (member=entIdentUse DOT { attributeMethodCall = true; })? )?
		(pack=IDENT DOUBLECOLON {packPrefix=true;})? (i=IDENT | i=EMIT | i=EMITDEBUG | i=DELETE) params=paramExprs[false] SEMI
			{ 
				if(!methodCall)
				{
					if(	( pack!=null && pack.getText().equals("File") &&
							( i.getText().equals("export")
							|| i.getText().equals("delete")
							)
						|| pack!=null && pack.getText().equals("Transaction") &&
							( i.getText().equals("start") 
							|| i.getText().equals("pause") 
							|| i.getText().equals("resume")
							|| i.getText().equals("commit")
							|| i.getText().equals("rollback")
							)
						|| pack!=null && pack.getText().equals("Debug") &&
							( i.getText().equals("add") 
							|| i.getText().equals("rem") 
							|| i.getText().equals("emit") 
							|| i.getText().equals("halt") 
							|| i.getText().equals("highlight") 
							)
						)
						|| i.getText().equals("valloc") && params.getChildren().size()==0
						|| i.getText().equals("vfree") || i.getText().equals("vfreenonreset") || i.getText().equals("vreset") 
						|| i.getText().equals("record") || i.getText().equals("emit") || i.getText().equals("emitdebug")
						|| i.getText().equals("add") && (params.getChildren().size()==1 || params.getChildren().size()==3)
						|| i.getText().equals("rem") || i.getText().equals("clear")
						|| i.getText().equals("retype") && params.getChildren().size()==2
						|| i.getText().equals("addCopy") && (params.getChildren().size()==1 || params.getChildren().size()==3)
						|| i.getText().equals("merge")
						|| i.getText().equals("redirectSource") || i.getText().equals("redirectTarget")
						|| i.getText().equals("redirectSourceAndTarget")
						|| i.getText().equals("insert") && params.getChildren().size()==1
						|| i.getText().equals("insertCopy") && params.getChildren().size()==2
						|| (i.getText().equals("insertInduced") || i.getText().equals("insertDefined")) && params.getChildren().size()==2
					)
					{
						IdentNode procIdent = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, pack!=null ? i.getText() + pack.getText() : i.getText(), getCoords(i)));
						ProcedureInvocationNode proc = new ProcedureInvocationNode(procIdent, params, context, env);
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), proc, targets, context);
						for(ProjectionExprNode proj : targetProjs.getChildren()) {
							proj.setProcedure(proc);
						}
						for(EvalStatementNode eval : targets.getChildren()) {
							eval.setCoords(getCoords(a));
						}
						ms.addStatement(ra);
						res = ms;
					}
					else
					{
						IdentNode procIdent;
						if(packPrefix) {
							procIdent = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, pack.getText(), getCoords(pack)), 
								env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
						} else {
							procIdent = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
						}
						ProcedureOrExternalProcedureInvocationNode proc = new ProcedureOrExternalProcedureInvocationNode(procIdent, params, context);
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), proc, targets, context);
						for(ProjectionExprNode proj : targetProjs.getChildren()) {
							proj.setProcedure(proc);
						}
						for(EvalStatementNode eval : targets.getChildren()) {
							eval.setCoords(getCoords(a));
						}
						ms.addStatement(ra);
						res = ms;
					}
				}
				else
				{
					IdentNode method_ = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
					if(!attributeMethodCall) 
					{
						ProcedureMethodInvocationDecisionNode pmi = new ProcedureMethodInvocationDecisionNode(new IdentExprNode(variable, yielded), method_, params, context);
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), pmi, targets, context);
						for(ProjectionExprNode proj : targetProjs.getChildren()) {
							proj.setProcedure(pmi);
						}
						for(EvalStatementNode eval : targets.getChildren()) {
							eval.setCoords(getCoords(a));
						}
						ms.addStatement(ra);
						res = ms;
					}
					else
					{
						ProcedureMethodInvocationDecisionNode pmi = new ProcedureMethodInvocationDecisionNode(new QualIdentNode(getCoords(d), variable, member), method_, params, context);
						if(onLHS) reportError(getCoords(d), "Method call on an attribute is forbidden in LHS eval, only yield method call to a def variable allowed.");
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), pmi, targets, context);
						for(ProjectionExprNode proj : targetProjs.getChildren()) {
							proj.setProcedure(pmi);
						}
						for(EvalStatementNode eval : targets.getChildren()) {
							eval.setCoords(getCoords(a));
						}
						ms.addStatement(ra);
						res = ms;
					}
				}
			}
	|
	  exec=execStmt[null, context, directlyNestingLHSGraph] SEMI
		{ res = new ExecStatementNode(exec, context); }
	;

targets	[boolean onLHS, Coords coords, MultiStatementNode ms, int context, PatternGraphNode directlyNestingLHSGraph] returns [ CollectNode<ProjectionExprNode> tgtProjs = new CollectNode<ProjectionExprNode>(), CollectNode<EvalStatementNode> tgts = new CollectNode<EvalStatementNode>() ]
	@init{
		int index = 0; // index of return target in sequence of returns
		ProjectionExprNode e = null;
	}
	: ( { e = new ProjectionExprNode(coords, index); $tgtProjs.addChild(e); } tgt=assignmentTarget[onLHS, coords, e, ms, context, directlyNestingLHSGraph] { $tgts.addChild(tgt); ++index; } 
		  ( c=COMMA { e = new ProjectionExprNode(getCoords(c), index); $tgtProjs.addChild(e); } tgt=assignmentTarget[onLHS, coords, e, ms, context, directlyNestingLHSGraph] { $tgts.addChild(tgt); ++index; } )*
	  )?
	;

assignmentTarget [ boolean onLHS, Coords coords, ProjectionExprNode e, MultiStatementNode ms, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EvalStatementNode res = null ]
options { k = 5; }
	@init{
		boolean yielded = false;
	}

	: (DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse
		{ res = new AssignNode(coords, new QualIdentNode(getCoords(d), owner, member), e, context); }
	|
	  (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse
		{ res = new AssignNode(coords, new IdentExprNode(variable, yielded), e, context, onLHS); }
	|
	  vis=visited
		{ res = new AssignVisitedNode(coords, vis, e, context); }
	| 
	  (DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse LBRACK idx=expr[false] RBRACK
		{ res = new AssignIndexedNode(coords, new QualIdentNode(getCoords(d), owner, member), e, idx, context); }
	|
	  (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse LBRACK idx=expr[false] RBRACK
		{ res = new AssignIndexedNode(coords, new IdentExprNode(variable, yielded), e, idx, context, onLHS); }
	|
	  de=defEntityToBeYieldedTo[null, null, context, directlyNestingLHSGraph]
		{
			DefDeclStatementNode tgt = new DefDeclStatementNode(coords, de, context);
			ms.addStatement(tgt);
			res = new AssignNode(coords, new IdentExprNode(tgt.getDecl().getIdentNode()), e, context, onLHS);
		}
	;

ifelse [ boolean onLHS, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EvalStatementNode res = null ]
	@init{
		CollectNode<EvalStatementNode> elseRemainder = new CollectNode<EvalStatementNode>();
	}

	: i=IF LPAREN e=expr[false] RPAREN 
		LBRACE { env.pushScope("if", getCoords(i)); } 
			cs=computations[onLHS, context, directlyNestingLHSGraph] 
		RBRACE { env.popScope(); }
	  (el=ELSE // allow else { statements } as well as else if{ expr; statements} else { statements}, and so on (nesting mapped to linear syntax)
	      (
			  ie = ifelse[onLHS, context, directlyNestingLHSGraph]
				  { elseRemainder.addChild(ie); }
		  | 
			  LBRACE { env.pushScope("else", getCoords(el)); }
				ecs=computations[onLHS, context, directlyNestingLHSGraph]
			  RBRACE { env.popScope(); }
			      { elseRemainder = ecs; }
		  )
	  )?
			{ res=new ConditionStatementNode(getCoords(i), e, cs, elseRemainder); }
	;

switchcase [ boolean onLHS, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EvalStatementNode res = null ]
	@init{
		CollectNode<CaseStatementNode> cases = new CollectNode<CaseStatementNode>();
		int caseCounter = 1;
		Token branch = null;
		ExprNode caseExpr = null;
	}

	: s=SWITCH LPAREN e=expr[false] RPAREN 
		LBRACE
			(
				( ca=CASE 
					(
					  c=constant 
						{
							branch = ca;
							caseExpr = c;
						} 
					| ec=enumConstant 
						{
							branch = ca;
							caseExpr = ec;
						}
					)
				|
					el=ELSE { branch = el; caseExpr = null; }
				)
				LBRACE { env.pushScope("case_"+caseCounter, getCoords(s)); } 
					cs=computations[onLHS, context, directlyNestingLHSGraph]
					{ cases.addChild(new CaseStatementNode(getCoords(branch), caseExpr, cs)); ++caseCounter; }
				RBRACE { env.popScope(); } 
			)+
		RBRACE
			{ res=new SwitchStatementNode(getCoords(s), e, cases); }
	;

forContent [ Coords f, boolean onLHS, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EvalStatementNode res = null ]
options { k = *; }
	@init{
		IdentNode iterIdentUse = null;
		VarDeclNode iterVar = null;
	}

	: variable=entIdentDecl IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			iterIdentUse = new IdentNode(env.occurs(ParserEnvironment.ITERATEDS, i.getText(), getCoords(i)));
			iterVar = new VarDeclNode(variable, IdentNode.getInvalid(), directlyNestingLHSGraph, context);
			res = new IteratedAccumulationYieldNode(f, iterVar, iterIdentUse, cs);
		}
	| variable=entIdentDecl COLON dres=forContentTypedIteration[f, variable, onLHS, context, directlyNestingLHSGraph]
		{ res = dres; }
	;

forContentTypedIteration [ Coords f, IdentNode leftVar, boolean onLHS, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EvalStatementNode res = null ]
options { k = *; }
	@init{
		IdentNode iterIdentUse = null;
		IdentNode containerIdentUse = null;
		IdentNode matchesIdentUse = null;
		IdentNode functionIdentUse = null;
		VarDeclNode iterVar = null;
		VarDeclNode iterIndex = null;
	}

	: type=typeIdentUse IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			containerIdentUse = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context);
			res = new ContainerAccumulationYieldNode(f, iterVar, null, containerIdentUse, cs);
		}
	| indexType=typeIdentUse RARROW variable=entIdentDecl COLON type=typeIdentUse IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			containerIdentUse = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
			iterVar = new VarDeclNode(variable, type, directlyNestingLHSGraph, context);
			iterIndex = new VarDeclNode(leftVar, indexType, directlyNestingLHSGraph, context);
			res = new ContainerAccumulationYieldNode(f, iterVar, iterIndex, containerIdentUse, cs);
		}
	| type=typeIdentUse IN
			{ input.LT(1).getText().equals("adjacent") || input.LT(1).getText().equals("adjacentIncoming") || input.LT(1).getText().equals("adjacentOutgoing")
			  || input.LT(1).getText().equals("incident") || input.LT(1).getText().equals("incoming") || input.LT(1).getText().equals("outgoing")
			  || input.LT(1).getText().equals("reachable") || input.LT(1).getText().equals("reachableIncoming") || input.LT(1).getText().equals("reachableOutgoing")
			  || input.LT(1).getText().equals("reachableEdges") || input.LT(1).getText().equals("reachableEdgesIncoming") || input.LT(1).getText().equals("reachableEdgesOutgoing")
			  || input.LT(1).getText().equals("boundedReachable") || input.LT(1).getText().equals("boundedReachableIncoming") || input.LT(1).getText().equals("boundedReachableOutgoing")
			  || input.LT(1).getText().equals("boundedReachableEdges") || input.LT(1).getText().equals("boundedReachableEdgesIncoming") || input.LT(1).getText().equals("boundedReachableEdgesOutgoing")
			  || input.LT(1).getText().equals("nodes") || input.LT(1).getText().equals("edges")
			  }?
			function=externalFunctionInvocationExpr[false] RPAREN
			{
				if(!(function instanceof FunctionInvocationExprNode))
					reportError(function.getCoords(), "unknown function or wrong parameters in for loop over graph access function");
			}
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context);
			res = new ForFunctionNode(f, iterVar, (FunctionInvocationExprNode)function, cs);
		}
	| MATCH LT type=actionIdentUse GT IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			matchesIdentUse = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
			iterVar = new VarDeclNode(leftVar, MatchTypeNode.getMatchType(type), directlyNestingLHSGraph, context);
			res = new MatchesAccumulationYieldNode(f, iterVar, matchesIdentUse, cs);
		}
	| type=typeIdentUse IN LBRACK left=expr[false] COLON right=expr[false] RBRACK RPAREN
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context);
			res = new IntegerRangeIterationYieldNode(f, iterVar, left, right, cs);
		}
	| type=typeIdentUse IN LBRACE idx=indexIdentUse EQUAL e=expr[false] RBRACE RPAREN
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context);
			res = new ForIndexAccessEqualityYieldNode(f, iterVar, context, idx, e, directlyNestingLHSGraph, cs);
		}
	| type=typeIdentUse IN LBRACE i=IDENT LPAREN idx=indexIdentUse (os=relOS e=expr[false] (COMMA idx2=indexIdentUse os2=relOS e2=expr[false])?)? RPAREN RBRACE RPAREN
		LBRACE
			cs=computations[onLHS, context, directlyNestingLHSGraph]
		RBRACE { env.popScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context);
			boolean ascending = true;
			if(i.getText().equals("ascending")) 
				ascending = true;
			else if(i.getText().equals("descending"))
				ascending = false;
			else
				reportError(getCoords(i), "ordered index access loop must start with ascending or descending");
			if(idx2!=null && !idx.toString().equals(idx2.toString()))
				reportError(idx2.getCoords(), "the same index must be used in an ordered index access loop with two constraints");
			res = new ForIndexAccessOrderingYieldNode(f, iterVar, context, ascending, idx, os, e, os2, e2, directlyNestingLHSGraph, cs);
		}
	;

assignTo returns [ int ccat = CompoundAssignNode.NONE, BaseNode tgtChanged = null ]
	: (ASSIGN_TO { $ccat = CompoundAssignNode.ASSIGN; }
		| BOR_TO { $ccat = CompoundAssignNode.UNION; }
		| BAND_TO { $ccat = CompoundAssignNode.INTERSECTION; })
	  tgtc=assignToTgt { $tgtChanged = tgtc; }
	;

assignToTgt returns [ BaseNode tgtChanged = null ]
options { k = 4; }
@init{ boolean yielded=false; }
	: tgtOwner=entIdentUse d=DOT tgtMember=entIdentUse { tgtChanged = new QualIdentNode(getCoords(d), tgtOwner, tgtMember); }
	    | (y=YIELD { yielded = true; })? tgtVariable=entIdentUse { tgtChanged = new IdentExprNode(tgtVariable, yielded); }
	    | vis=visited { tgtChanged = vis; }
	;

expr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=condExpr[inEnumInit] { res = e; }
	;

condExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: op0=logOrExpr[inEnumInit] { res=op0; }
		( t=QUESTION op1=expr[inEnumInit] COLON op2=condExpr[inEnumInit]
			{
				OpNode cond=makeOp(t);
				cond.addChild(op0);
				cond.addChild(op1);
				cond.addChild(op2);
				res=cond;
			}
		)?
	;

logOrExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=logAndExpr[inEnumInit] { res = e; }
		( t=LOR op=logAndExpr[inEnumInit]
			{ res=makeBinOp(t, res, op); }
		)*
	;

logAndExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=bitOrExpr[inEnumInit] { res = e; }
		( t=LAND op=bitOrExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

bitOrExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=bitXOrExpr[inEnumInit] { res = e; }
		( t=BOR op=bitXOrExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

bitXOrExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=bitAndExpr[inEnumInit] { res = e; }
		( t=BXOR op=bitAndExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

bitAndExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=exceptExpr[inEnumInit] { res = e; }
		( t=BAND op=exceptExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

exceptExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=eqExpr[inEnumInit] { res = e; }
		( t=BACKSLASH op=eqExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

eqOp returns [ Token t = null ]
	: e=EQUAL { t = e; }
	| n=NOT_EQUAL { t = n; }
	| s=STRUCTURAL_EQUAL { t = s; }
	;

eqExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=relExpr[inEnumInit] { res = e; }
		( t=eqOp op=relExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

relOp returns [ Token t = null ]
	: lt=LT { t = lt; }
	| le=LE { t = le; }
	| gt=GT { t = gt; }
	| ge=GE { t = ge; }
	| in=IN { t = in; }
	;

relExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=shiftExpr[inEnumInit] { res = e; }
		( t=relOp op=shiftExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

shiftOp returns [ Token res = null ]
	: l=SL { res = l; }
	| r=SR { res = r; }
	| b=BSR { res = b; }
	;

shiftExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=addExpr[inEnumInit] { res = e; }
		( t=shiftOp op=addExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

addOp returns [ Token t = null ]
	: p=PLUS { t = p; }
	| m=MINUS { t = m; }
	;

addExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=mulExpr[inEnumInit] { res = e; }
		( t=addOp op=mulExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

mulOp returns [ Token t = null ]
	: s=STAR { t = s; }
	| m=MOD { t = m; }
	| d=DIV { t = d; }
	;


mulExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=unaryExpr[inEnumInit] { res = e; }
		( t=mulOp op=unaryExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

unaryExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: t=TILDE op=unaryExpr[inEnumInit]
		{ res = makeUnOp(t, op); }
	| n=NOT op=unaryExpr[inEnumInit]
		 { res = makeUnOp(n, op); }
	| m=MINUS op=unaryExpr[inEnumInit]
		{
			OpNode neg = new ArithmeticOpNode(getCoords(m), OperatorSignature.NEG);
			neg.addChild(op);
			res = neg;
		}
	| PLUS e=unaryExpr[inEnumInit] { res = e; }
	| (LPAREN typeIdentUse RPAREN unaryExpr[false])
		=> p=LPAREN id=typeIdentUse RPAREN op=unaryExpr[inEnumInit]
		{
			res = new CastNode(getCoords(p), id, op);
		}
	| { env.test(ParserEnvironment.INDICES, input.LT(1).getText()) }?
		i=IDENT l=LBRACK key=expr[inEnumInit] RBRACK
			{ res = new IndexedIncidenceCountIndexAccessExprNode(getCoords(l), new IdentNode(env.occurs(ParserEnvironment.INDICES, i.getText(), getCoords(i))), key); }
	| e=primaryExpr[inEnumInit] ((LBRACK ~PLUS | DOT) => e=selectorExpr[e, inEnumInit])* { res = e; }
	; 

primaryExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
options { k = 4; }
@init{ IdentNode id; }
	: e=visited { res = e; }
	| e=nameOf { res = e; }
	| e=count { res = e; }
	| e=globalsAccessExpr { res = e; }
	| e=constant { res = e; }
	| e=typeOf { res = e; }
	| e=initContainerExpr { res = e; }
	| e=externalFunctionInvocationExpr[inEnumInit] { res = e; }
	| LPAREN e=expr[inEnumInit] { res = e; } RPAREN
	| p=PLUSPLUS { reportError(getCoords(p), "increment operator \"++\" not supported"); }
	| q=MINUSMINUS { reportError(getCoords(q), "decrement operator \"--\" not supported"); }
	| i=IDENT
		{
			if(i.getText().equals("this") && !env.test(ParserEnvironment.ENTITIES, "this"))
				res = new ThisExprNode(getCoords(i));
			else {
				// Entity names can overwrite type names
				if(env.test(ParserEnvironment.ENTITIES, i.getText()) || !env.test(ParserEnvironment.TYPES, i.getText()))
					id = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
				else
					id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
				res = new IdentExprNode(id);
			}
		}
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
		}
	| p=IDENT DOUBLECOLON en=IDENT d=DOUBLECOLON i=IDENT
		{
			res = new DeclExprNode(new EnumExprNode(getCoords(d), 
				new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)),
					env.occurs(ParserEnvironment.TYPES, en.getText(), getCoords(en))),
				new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)))));
		}
	;

visited returns [ VisitedNode res ]
	: v=VISITED LPAREN elem=expr[false] 
		( COMMA idExpr=expr[false] RPAREN
			{ res = new VisitedNode(getCoords(v), idExpr, elem); }
		| RPAREN
			{ res = new VisitedNode(getCoords(v), new IntConstNode(getCoords(v), 0), elem); }
		)
	|
		(elem=entIdentExpr | elem=globalsAccessExpr) DOT v=VISITED  
		( (LBRACK) => LBRACK idExpr=expr[false] RBRACK // [ starts a visited flag expression, not a following map access selector expression
			{ res = new VisitedNode(getCoords(v), idExpr, elem); }
		| 
			{ res = new VisitedNode(getCoords(v), new IntConstNode(getCoords(v), 0), elem); }
		)
	;

nameOf returns [ ExprNode res = env.initExprNode() ]
	: n=NAMEOF LPAREN (id=expr[false])? RPAREN { res = new NameofNode(getCoords(n), id); }
	;

count returns [ ExprNode res = env.initExprNode() ]
	: c=COUNT LPAREN i=IDENT RPAREN	{ res = new CountNode(getCoords(c), new IdentNode(env.occurs(ParserEnvironment.ITERATEDS, i.getText(), getCoords(i)))); }
	;

typeOf returns [ ExprNode res = env.initExprNode() ]
	: t=TYPEOF LPAREN id=entIdentUse RPAREN { res = new TypeofNode(getCoords(t), id); }
	;
	
initContainerExpr returns [ ExprNode res = env.initExprNode() ]
	: MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT e1=initMapExpr[null, MapTypeNode.getMapType(keyType, valueType)] { res = e1; }
	| SET LT valueType=typeIdentUse GT e2=initSetExpr[null, SetTypeNode.getSetType(valueType)] { res = e2; }
	| ARRAY LT valueType=typeIdentUse GT e3=initArrayExpr[null, ArrayTypeNode.getArrayType(valueType)] { res = e3; }
	| DEQUE LT valueType=typeIdentUse GT e4=initDequeExpr[null, DequeTypeNode.getDequeType(valueType)] { res = e4; }
	;
	
constant returns [ ExprNode res = env.initExprNode() ]
	: b=NUM_BYTE
		{ res = new ByteConstNode(getCoords(b), Byte.parseByte(ByteConstNode.removeSuffix(b.getText()), 10)); }
	| sh=NUM_SHORT
		{ res = new ShortConstNode(getCoords(sh), Short.parseShort(ShortConstNode.removeSuffix(sh.getText()), 10)); }
	| i=NUM_INTEGER
		{ res = new IntConstNode(getCoords(i), Integer.parseInt(i.getText(), 10)); }
	| l=NUM_LONG
		{ res = new LongConstNode(getCoords(l), Long.parseLong(LongConstNode.removeSuffix(l.getText()), 10)); }
	| hb=NUM_HEX_BYTE
		{ res = new ByteConstNode(getCoords(hb), Byte.parseByte(ByteConstNode.removeSuffix(hb.getText().substring(2)), 16)); }
	| hsh=NUM_HEX_SHORT
		{ res = new ShortConstNode(getCoords(hsh), Short.parseShort(ShortConstNode.removeSuffix(hsh.getText().substring(2)), 16)); }
	| hi=NUM_HEX
		{ res = new IntConstNode(getCoords(hi), Integer.parseInt(hi.getText().substring(2), 16)); }
	| hl=NUM_HEX_LONG
		{ res = new LongConstNode(getCoords(hl), Long.parseLong(LongConstNode.removeSuffix(hl.getText().substring(2)), 16)); }
	| f=NUM_FLOAT
		{ res = new FloatConstNode(getCoords(f), Float.parseFloat(f.getText())); }
	| d=NUM_DOUBLE
		{ res = new DoubleConstNode(getCoords(d), Double.parseDouble(d.getText())); }
	| s=STRING_LITERAL
		{
			String buff = s.getText();
			// Strip the " from the string
			buff = buff.substring(1, buff.length() - 1);
			res = new StringConstNode(getCoords(s), buff);
		}
	| tt=TRUE
		{ res = new BoolConstNode(getCoords(tt), true); }
	| ff=FALSE
		{ res = new BoolConstNode(getCoords(ff), false); }
	| n=NULL
		{ res = new NullConstNode(getCoords(n)); }
	;

enumConstant returns [ ExprNode res = env.initExprNode() ]
options { k = 4; }
	: pen=IDENT d=DOUBLECOLON i=IDENT 
		{
			res = new DeclExprNode(new EnumExprNode(getCoords(d), 
				new IdentNode(env.occurs(ParserEnvironment.TYPES, pen.getText(), getCoords(pen))),
				new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)))));
		}
	| p=IDENT DOUBLECOLON en=IDENT d=DOUBLECOLON i=IDENT
		{
			res = new DeclExprNode(new EnumExprNode(getCoords(d), 
				new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, p.getText(), getCoords(p)),
					env.occurs(ParserEnvironment.TYPES, en.getText(), getCoords(en))),
				new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)))));
		}
	;
	
entIdentExpr returns [ ExprNode res = env.initExprNode() ]
	: i=IDENT
		{
			if(i.getText().equals("this") && !env.test(ParserEnvironment.ENTITIES, "this"))
				res = new ThisExprNode(getCoords(i));
			else
				res = new IdentExprNode(new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))));
		}
	;

globalsAccessExpr returns [ ExprNode res = env.initExprNode() ]
	@init{ IdentNode id; }

	: DOUBLECOLON i=IDENT
		{
			id = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
			res = new IdentExprNode(id);
		}
	;
	
externalFunctionInvocationExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	@init{
		boolean packPrefix = false;
	}
	: (pack=IDENT DOUBLECOLON {packPrefix=true;})? (i=IDENT | i=COPY) params=paramExprs[inEnumInit]
		{
			if( ( pack!=null && pack.getText().equals("Math") && 
					( i.getText().equals("min") || i.getText().equals("max") 
					|| i.getText().equals("sin") || i.getText().equals("cos") || i.getText().equals("tan")
					|| i.getText().equals("arcsin") || i.getText().equals("arccos") || i.getText().equals("arctan") 
					|| i.getText().equals("pow") || i.getText().equals("log")
					|| i.getText().equals("ceil") || i.getText().equals("floor") || i.getText().equals("round") || i.getText().equals("truncate")
					|| i.getText().equals("abs") || i.getText().equals("sgn")
					|| i.getText().equals("pi") || i.getText().equals("e") 
					|| i.getText().equals("byteMin") || i.getText().equals("byteMax") 
					|| i.getText().equals("shortMin") || i.getText().equals("shortMax") 
					|| i.getText().equals("intMin") || i.getText().equals("intMax") 
					|| i.getText().equals("longMin") || i.getText().equals("longMax") 
					|| i.getText().equals("floatMin") || i.getText().equals("floatMax") 
					|| i.getText().equals("doubleMin") || i.getText().equals("doubleMax") 
					)
				)
				|| ( pack!=null && pack.getText().equals("File") &&
					( i.getText().equals("exists") 
					|| i.getText().equals("import")
					)
				)
				|| ( pack!=null && pack.getText().equals("Time") &&
					( i.getText().equals("now")
					)
				)
				|| (i.getText().equals("nodes") || i.getText().equals("edges")) && params.getChildren().size()<=1
				|| (i.getText().equals("countNodes") || i.getText().equals("countEdges")) && params.getChildren().size()<=1
				|| (i.getText().equals("empty") || i.getText().equals("size")) && params.getChildren().size()==0
				|| (i.getText().equals("source") || i.getText().equals("target")) && params.getChildren().size()==1
				|| i.getText().equals("opposite") && params.getChildren().size()==2
				|| (i.getText().equals("nodeByName") || i.getText().equals("edgeByName")) && params.getChildren().size()>=1 && params.getChildren().size()<=2
				|| (i.getText().equals("nodeByUnique") || i.getText().equals("edgeByUnique")) && params.getChildren().size()>=1 && params.getChildren().size()<=2
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
				|| i.getText().equals("copy") && params.getChildren().size()==1
				|| i.getText().equals("uniqueof") && (params.getChildren().size()==1 || params.getChildren().size()==0)
			)
			{
				IdentNode funcIdent = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, pack!=null ? i.getText() + pack.getText() : i.getText(), getCoords(i)));
				res = new FunctionInvocationExprNode(funcIdent, params, env);
			} else {
				IdentNode funcIdent;
				if(packPrefix) {
					funcIdent = new PackageIdentNode(env.occurs(ParserEnvironment.PACKAGES, pack.getText(), getCoords(pack)), 
						env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				} else {
					funcIdent = new IdentNode(env.occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.getText(), getCoords(i)));
				}
				res = new FunctionOrExternalFunctionInvocationExprNode(funcIdent, params);
			}
		}
	;
	
selectorExpr [ ExprNode target, boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	:	l=LBRACK key=expr[inEnumInit] RBRACK { res = new IndexedAccessExprNode(getCoords(l), target, key); }
	|	d=DOT id=memberIdentUse
			( { input.get(input.LT(1).getTokenIndex()-1).getText().equals("indexOfBy") || input.get(input.LT(1).getTokenIndex()-1).getText().equals("indexOfOrderedBy") 
				|| input.get(input.LT(1).getTokenIndex()-1).getText().equals("lastIndexOfBy") || input.get(input.LT(1).getTokenIndex()-1).getText().equals("orderAscendingBy") }? 
				LT mi=memberIdentUse GT )?
		(
			params=paramExprs[inEnumInit]
			{
				res = new MethodInvocationExprNode(target, id, params, mi);
			}
		| 
			{
				res = new MemberAccessExprNode(getCoords(d), target, id);
			}
		)
	;
	
paramExprs [boolean inEnumInit] returns [ CollectNode<ExprNode> params = new CollectNode<ExprNode>(); ]
	:	LPAREN
		(
			e=expr[inEnumInit] { params.addChild(e); }
			( COMMA e=expr[inEnumInit] { params.addChild(e); } ) *
		)?
		RPAREN
	;

//////////////////////////////////////////
// Range Spec
//////////////////////////////////////////


rangeSpec returns [ RangeSpecNode res = null ]
	@init{
		lower = 0; upper = RangeSpecNode.UNBOUND;
		de.unika.ipd.grgen.parser.Coords coords = de.unika.ipd.grgen.parser.Coords.getInvalid();
		// range allows [*], [+], [?], [c:*], [c], [c:d]; no range equals [*]
	}

	:
		(
			l=LBRACK { coords = getCoords(l); }
			(
				STAR { lower=0; upper=RangeSpecNode.UNBOUND; }
			|
				PLUS { lower=1; upper=RangeSpecNode.UNBOUND; }
			|
				QUESTION { lower=0; upper=1; }
			|
				lower=integerConst
				(
					COLON ( STAR { upper=RangeSpecNode.UNBOUND; } | upper=integerConst )
				|
					{ upper = lower; }
				)
			)
			RBRACK
		)?
		{ res = new RangeSpecNode(coords, lower, upper); }
	;

integerConst returns [ long value = 0 ]
	: i=NUM_INTEGER
		{ value = Long.parseLong(i.getText()); }
	;



////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Symbols
////////////////////////////////////////////////////////////////////////////////////////////////////////////////



QUESTION		:	'?'		;
QUESTIONMINUS	:	'?-'	;
MINUSQUESTION	:	'-?'	;
QMMQ			:	'?--?'	;
LPAREN			:	'('		;
RPAREN			:	')'		;
LBRACK			:	'['		;
RBRACK			:	']'		;
LBRACE			:	'{'		;
LBRACEMINUS		:	'{-'	;
LBRACEPLUS		:	'{+'	;
RBRACE			:	'}'		;
COLON			:	':'		;
DOUBLECOLON     :   '::'    ;
COMMA			:	','		;
DOT 			:	'.'		;
ASSIGN			:	'='		;
BOR_ASSIGN		:	'|='	;
BAND_ASSIGN		:	'&='	;
BACKSLASH_ASSIGN:	'\\='	;
ASSIGN_TO		:	'=>'	;
BOR_TO			:	'|>'	;
BAND_TO			:	'&>'	;
EQUAL			:	'=='	;
NOT         	:	'!'		;
TILDE			:	'~'		;
STRUCTURAL_EQUAL:	'~~'	;
NOT_EQUAL		:	'!='	;
SL				:	'<<'	;
SR				:	'>>'	;
BSR				:	'>>>'	;
DIV				:	'/'		;
PLUS			:	'+'		;
PLUS_ASSIGN		:	'+='	;
MINUS			:	'-'		;
STAR			:	'*'		;
MOD				:	'%'		;
GE				:	'>='	;
GT				:	'>'		;
LE				:	'<='	;
LT				:	'<'		;
RARROW			:	'->'	;
LARROW			:	'<-'	;
LRARROW			:	'<-->'	;
DOUBLE_LARROW	:	'<--'	;
DOUBLE_RARROW	:	'-->'	;
BXOR			:	'^'		;
BOR				:	'|'		;
LOR				:	'||'	;
BAND			:	'&'		;
LAND			:	'&&'	;
SEMI			:	';'		;
DOUBLE_SEMI		:	';;'	;
BACKSLASH		:	'\\'	;
PLUSPLUS		:	'++'	;
MINUSMINUS		:	'--'	;
DOLLAR          :   '$'     ;
THENLEFT		:	'<;'	;
THENRIGHT		:	';>'	;
AT				:   '@'		;

// Whitespace -- ignored
WS	:	(	' '
		|	'\t'
		|	'\f'
		|	'\r'
		|	'\n'
		)+
		{ $channel=HIDDEN; }
	;

// single-line comment
SL_COMMENT
	: '//' ~('\n'|'\r')* '\r'? '\n' {$channel=HIDDEN;}
    ;

// multiple-line comment
ML_COMMENT
	:   '/*' ( options {greedy=false;} : . )* '*/' {$channel=HIDDEN;}
	;

fragment NUM_BYTE: ;
fragment NUM_SHORT: ;
fragment NUM_INTEGER: ;
fragment NUM_LONG: ;
fragment NUM_FLOAT: ;
fragment NUM_DOUBLE: ;
NUMBER
   : ('0'..'9')+
   ( ('.' '0'..'9') => '.' ('0'..'9')+
     (   ('f'|'F')    { $type = NUM_FLOAT; }
       | ('d'|'D')?   { $type = NUM_DOUBLE; }
     )
   | ('y'|'Y') { $type = NUM_BYTE; }
   | ('s'|'S') { $type = NUM_SHORT; }
   | { $type = NUM_INTEGER; }
   | ('l'|'L') { $type = NUM_LONG; }
   )
   ;


fragment NUM_HEX_BYTE: ;
fragment NUM_HEX_SHORT: ;
fragment NUM_HEX_LONG: ;
NUM_HEX
	: '0' 'x' ('0'..'9' | 'a' .. 'f' | 'A' .. 'F')+
	( ('y'|'Y') { $type = NUM_HEX_BYTE; }
	| ('s'|'S') { $type = NUM_HEX_SHORT; }
	| { $type = NUM_HEX; }
	| ('l'|'L') { $type = NUM_HEX_LONG; }
	)
	;

fragment
ESC
	:	'\\'
		(	'n'
		|	'r'
		|	't'
		|	'b'
		|	'f'
		|	'"'
		|	'\''
		|	'\\')
		;

STRING_LITERAL
	:	'"' (ESC|~('"'|'\\'))* '"'
	;

INCLUDE
  : '#include' WS s=STRING_LITERAL {
	String filename = s.getText();
	filename = filename.substring(1,filename.length()-1);
	
	//Instead of making includes relative to the main grg file
	//we try to interpret the included path as relative to the including file
	File file = new File(filename);
	if(!file.isAbsolute())
	{
		try
		{
			//Get the parent folder of the including file
			File dir = new File(env.getFilename()).getCanonicalFile().getParentFile();
			file = new File(dir, filename);
		}
		catch(IOException e)
		{
			//getCanonicalFile can throw an IOException if that happens we just return to the old behaviour
		}
	}
	env.pushFile(this, file);
  }
  ;

HASHUSING : '#using';

ABSTRACT : 'abstract';
ALTERNATIVE : 'alternative';
ARBITRARY : 'arbitrary';
ARRAY : 'array';
AUTO : 'auto';
BREAK : 'break';
CASE : 'case';
CLASS : 'class';
COPY : 'copy';
CONNECT : 'connect';
CONST : 'const';
CONTINUE : 'continue';
COUNT : 'count';
DEF : 'def';
DELETE : 'delete';
DIRECTED : 'directed';
DO : 'do';
EDGE : 'edge';
ELSE : 'else';
EMIT : 'emit';
EMITDEBUG : 'emitdebug';
EMITHERE : 'emithere';
EMITHEREDEBUG : 'emitheredebug';
ENUM : 'enum';
EVAL : 'eval';
EVALHERE : 'evalhere';
EXACT : 'exact';
EXEC : 'exec';
EXTENDS : 'extends';
EXTERNAL : 'external';
FALSE : 'false';
FILTER : 'filter';
FOR : 'for';
FUNCTION : 'function';
HOM : 'hom';
IF : 'if';
IN : 'in';
INDEPENDENT : 'independent';
INDEX : 'index';
INDUCED : 'induced';
ITERATED : 'iterated';
MAP : 'map';
MATCH : 'match';
MODIFY : 'modify';
MULTIPLE : 'multiple';
NAMEOF : 'nameof';
NEGATIVE : 'negative';
NODE : 'node';
NULL : 'null';
OPTIONAL : 'optional';
PACKAGE : 'package';
PATTERN : 'pattern';
PATTERNPATH : 'patternpath';
PROCEDURE : 'procedure';
DEQUE : 'deque';
REPLACE : 'replace';
RETURN : 'return';
RULE : 'rule';
SEQUENCE : 'sequence';
SET : 'set';
SWITCH : 'switch';
TEST : 'test';
TRUE : 'true';
TYPEOF : 'typeof';
UNDIRECTED : 'undirected';
USING : 'using';
VISITED : 'visited';
WHILE : 'while';
YIELD : 'yield';

IDENT : ('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')* ;
