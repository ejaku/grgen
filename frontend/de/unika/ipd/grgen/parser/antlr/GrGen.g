/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/*
 * GrGen model and rule specification language grammar for ANTLR 3
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski, Veit Batz, Edgar Jakumeit, Sebastian Buchwald, Moritz Kroll, Peter Grüner
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
//	PatternGraphLhsNode directlyNestingLHSGraph;
//}
// todo: maybe user other features of antlr 3?

@lexer::header {
	//package de.unika.ipd.grgen.parser.antlr;
	using de.unika.ipd.grgen.parser.antlr;
	using System; // for String
	using System.IO; // for file handling
	
	//using java.io.File;
	//using java.io.IOException;
}

@lexer::members {
	GRParserEnvironment env;

	void setEnv(GRParserEnvironment env) {
		this.env = env;
	}
  
	// overriden for handling EOF of included file
	public IToken nextToken() {
		IToken token = base.NextToken();

		if(token.Type == EOF) {
			if(env.PopFile(this)) {
				token = this.nextToken();
			}
		}

		// Skip first token after switching to another input.
		int startIndex = ((CommonToken)token).StartIndex;
		if(startIndex < 0) {
			token = this.nextToken();
		}
			
		return token;
	}
}

@header {
	//package de.unika.ipd.grgen.parser.antlr;
	
	//using java.util.Collection;
	
	//using java.io.File;

	//using System.Diagnostics;
	using System.Text;
	using System;

	using de.unika.ipd.grgen.parser;
	using de.unika.ipd.grgen.ast.model;
	using de.unika.ipd.grgen.ast.model.decl;
	using de.unika.ipd.grgen.ast.model.type;
	using de.unika.ipd.grgen.ast;
	using de.unika.ipd.grgen.ast.decl;
	using de.unika.ipd.grgen.ast.decl.executable;
	using de.unika.ipd.grgen.ast.decl.pattern;
	using CopyKind = de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode.CopyKind;
	using de.unika.ipd.grgen.ast.pattern;
	using de.unika.ipd.grgen.ast.expr;
	using de.unika.ipd.grgen.ast.expr.numeric;
	using de.unika.ipd.grgen.ast.expr.@string;
	using de.unika.ipd.grgen.ast.expr.graph;
	using de.unika.ipd.grgen.ast.expr.array;
	using de.unika.ipd.grgen.ast.expr.deque;
	using de.unika.ipd.grgen.ast.expr.map;
	using de.unika.ipd.grgen.ast.expr.set;
	using de.unika.ipd.grgen.ast.expr.invocation;
	using de.unika.ipd.grgen.ast.stmt;
	using de.unika.ipd.grgen.ast.stmt.graph;
	using de.unika.ipd.grgen.ast.stmt.invocation;
	using de.unika.ipd.grgen.ast.type;
	using de.unika.ipd.grgen.ast.type.basic;
	using de.unika.ipd.grgen.ast.type.container;
	using de.unika.ipd.grgen.util;
	using de.unika.ipd.grgen.util.collection;
}

@members {
	enum InheritanceTypeKind {
		NODE,
		EDGE,
		CLASS,
		TRANSIENT_CLASS
	}
	
	internal bool hadError_ = false;

	private static Dictionary<int, Operator> opIds = new Dictionary<int, Operator>();

	private static void putOpId(int tokenId, Operator opId) {
		opIds.Add(tokenId, opId);
	}

	static GrGenParser() {
		putOpId(QUESTION, Operator.COND);
		putOpId(EQUAL, Operator.EQ);
		putOpId(NOT_EQUAL, Operator.NE);
		putOpId(STRUCTURAL_EQUAL, Operator.SE);
		putOpId(NOT, Operator.LOG_NOT);
		putOpId(TILDE, Operator.BIT_NOT);
		putOpId(SL, Operator.SHL);
		putOpId(SR, Operator.SHR);
		putOpId(BSR, Operator.BIT_SHR);
		putOpId(DIV, Operator.DIV);
		putOpId(STAR, Operator.MUL);
		putOpId(MOD, Operator.MOD);
		putOpId(PLUS, Operator.ADD);
		putOpId(MINUS, Operator.SUB);
		putOpId(GE, Operator.GE);
		putOpId(GT, Operator.GT);
		putOpId(LE, Operator.LE);
		putOpId(LT, Operator.LT);
		putOpId(BAND, Operator.BIT_AND);
		putOpId(BOR, Operator.BIT_OR);
		putOpId(BXOR, Operator.BIT_XOR);
		putOpId(BXOR, Operator.BIT_XOR);
		putOpId(LAND, Operator.LOG_AND);
		putOpId(LOR, Operator.LOG_OR);
		putOpId(IN, Operator.IN);
		putOpId(LBRACK, Operator.INDEX);
		putOpId(BACKSLASH, Operator.EXCEPT);
	}

	public OperatorNode makeOp(Antlr.Runtime.IToken t) {
		Operator opId = opIds[t.Type];
		//Debug.Assert(opId != null, "Invalid operator ID");
		return new ArithmeticOperatorNode(getCoords(t), opId);
	}

	public OperatorNode makeTernOp(Antlr.Runtime.IToken t, ExprNode op0, ExprNode op1, ExprNode op2) {
		OperatorNode res = makeOp(t);
		res.AddChild(op0);
		res.AddChild(op1);
		res.AddChild(op2);
		return res;
	}

	public OperatorNode makeBinOp(Antlr.Runtime.IToken t, ExprNode op0, ExprNode op1) {
		OperatorNode res = makeOp(t);
		res.AddChild(op0);
		res.AddChild(op1);
		return res;
	}

	public OperatorNode makeUnOp(Antlr.Runtime.IToken t, ExprNode op) {
		OperatorNode res = makeOp(t);
		res.AddChild(op);
		return res;
	}

	protected ParserEnvironment env;

	public void setEnv(ParserEnvironment env) {
		this.env = env;
		gEmbeddedExec.env = env;
	}

	protected Coords getCoords(Antlr.Runtime.IToken tok) {
		return new de.unika.ipd.grgen.parser.antlr.Coords(tok);
	}

	protected void reportError(de.unika.ipd.grgen.parser.Coords c, String s) {
		hadError_ = true;
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

	public bool hadError() {
		return hadError_;
	}

	public String getFilename() {
		return env.Filename;
	}
	
	public String join(String separator, IList<String> joinees) {
		StringBuilder sb = new StringBuilder();
		bool first = true;
		foreach(String joinee in joinees) {
			if(first)
				first = false;
			else
				sb.Append(separator);
			sb.Append(joinee);
		}
		return sb.ToString();
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
	@init {
		CollectNode<ModelNode> modelChilds = new CollectNode<ModelNode>();
		CollectNode<IdentNode> packages = new CollectNode<IdentNode>();
		CollectNode<IdentNode> patternChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> actionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchTypeChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> filterChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchClassChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchClassFilterChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchTypeIteratedChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> functionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> procedureChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> sequenceChilds = new CollectNode<IdentNode>();
		String actionsName = Util.GetActionsNameFromFilename(getFilename());
		if(!Util.IsFilenameValidActionName(getFilename())) {
			reportError(new de.unika.ipd.grgen.parser.Coords(), "The filename "+getFilename()+" cannot be used as the action name, it must be of the same format as an identifier.");
		}
	}
	: ( usingDecl[modelChilds] )*
	
		( globalVarDecl
		| ( pack=packageActionDecl { packages.AddChild(pack); } )
		| (declsPatternMatchingOrAttributeEvaluationUnitWithModifier[patternChilds, actionChilds,
				matchTypeChilds, filterChilds, matchClassChilds, matchClassFilterChilds, matchTypeIteratedChilds,
				functionChilds, procedureChilds, sequenceChilds])
		)*
		EOF
		{
			if(modelChilds.ChildrenExact.Count == 0)
				modelChilds.AddChild(env.StdModel);
			else if(modelChilds.ChildrenExact.Count > 1) {
				//
				// If more than one model is specified, generate a new graph model
				// using the name of the grg-file containing all given models.
				//
				IdentNode id = new IdentNode(env.Define(ParserEnvironment.ENTITIES, actionsName,
					modelChilds.Coords));
				bool isEmitClassDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isEmitClassDefined |= modelChild.IsEmitClassDefined();
				}
				bool isEmitGraphClassDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isEmitGraphClassDefined |= modelChild.IsEmitGraphClassDefined();
				}
				bool isCopyClassDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isCopyClassDefined |= modelChild.IsCopyClassDefined();
				}
				bool isEqualClassDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isEqualClassDefined |= modelChild.IsEqualClassDefined();
				}
				bool isLowerClassDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isLowerClassDefined |= modelChild.IsLowerClassDefined();
				}
				bool isGraphofDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isGraphofDefined |= modelChild.IsGraphofDefined();
				}
				bool isUniqueDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isUniqueDefined |= modelChild.IsUniqueDefined();
				}
				bool isUniqueClassDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isUniqueClassDefined |= modelChild.IsUniqueClassDefined();
				}
				bool isUniqueIndexDefined = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isUniqueIndexDefined |= modelChild.IsUniqueIndexDefined();
				}
				bool areFunctionsParallel = false;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					areFunctionsParallel |= modelChild.AreFunctionsParallel();
				}
				int isoParallel = 0;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					isoParallel = Math.Max(isoParallel, modelChild.IsoParallel());
				}
				int sequencesParallel = 0;
				foreach(ModelNode modelChild in modelChilds.ChildrenExact) {
					sequencesParallel = Math.Max(sequencesParallel, modelChild.SequencesParallel());
				}
				ModelNode model = new ModelNode(id, new CollectNode<IdentNode>(),
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(), 
						new CollectNode<IdentNode>(), new CollectNode<IdentNode>(), modelChilds, 
						isEmitClassDefined, isEmitGraphClassDefined, isCopyClassDefined, 
						isEqualClassDefined, isLowerClassDefined, isGraphofDefined,
						isUniqueDefined, isUniqueClassDefined, isUniqueIndexDefined,
						areFunctionsParallel, isoParallel, sequencesParallel);
				modelChilds = new CollectNode<ModelNode>();
				modelChilds.AddChild(model);
			}
			main = new UnitNode(actionsName, getFilename(),
					env.StdModel, modelChilds, patternChilds, actionChilds,
					matchTypeChilds, filterChilds, matchClassChilds, matchClassFilterChilds, matchTypeIteratedChilds,
					functionChilds, procedureChilds, sequenceChilds, packages);
		}
	;

usingDecl [ CollectNode<ModelNode> modelChilds ]
	options { k = 1; }
	@init {
		ICollection<String> modelNames = new ArrayList<String>();
	}
	: u=USING identList[modelNames]
		{
			modelChilds.Coords = getCoords(u);
			foreach(String modelName in modelNames)
			{
				File modelFile = env.FindModel(modelName);
				if(modelFile == null) {
					reportError(getCoords(u), "The model " + modelName + " could not be found.");
				} else {
					ModelNode model;
					model = env.ParseModel(modelFile);
					modelChilds.AddChild(model);
				}
			}
		}
		SEMI // don't move before the semantic action, this would cause a following include to be processed before the using of the model
	| h=HASHUSING s=STRING_LITERAL
		{
			modelChilds.Coords = getCoords(h);
			String modelName = s.Text;
			modelName = modelName.Substring(1,modelName.length()-2);
			File modelFile = env.FindModel(modelName);
			if(modelFile == null) {
				reportError(getCoords(h), "The model " + modelName + " could not be found.");
			} else {
				ModelNode model;
				model = env.ParseModel(modelFile);
				modelChilds.AddChild(model);
			}
		}
	;

globalVarDecl 
	: DOUBLECOLON id=entIdentDecl COLON type=typeIdentUse SEMI
		{
			id.Decl = new NodeDeclNode(id, type, CopyKind.None, 0, TypeExprNode.Empty, null);
		}
	| MINUS DOUBLECOLON id=entIdentDecl COLON type=typeIdentUse (RARROW | MINUS) SEMI
		{
			id.Decl = new EdgeDeclNode(id, type, CopyKind.None, 0, TypeExprNode.Empty, null);
		}
	| modifier=IDENT DOUBLECOLON id=entIdentDecl COLON 
		(
			type=typeIdentUse
			{
				id.Decl = new VarDeclNode(id, type, null, 0, false, false, modifier.Text);
			}
		|
			containerType=containerTypeUse
			{
				id.Decl = new VarDeclNode(id, containerType, null, 0, false, false, modifier.Text);
			}
		|
			matchTypeIdent=matchTypeIdentUse
			{
				id.Decl = new VarDeclNode(id, matchTypeIdent, null, 0, false, false, modifier.Text);
			}
		)
		SEMI
	;

packageActionDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	@init {
		CollectNode<IdentNode> patternChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> actionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchTypeChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> filterChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchClassChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchClassFilterChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> matchTypeIteratedChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> functionChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> procedureChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> sequenceChilds = new CollectNode<IdentNode>();
	}
	: PACKAGE id=packageIdentDecl LBRACE { env.PushScope(id); env.CurrentPackage = id; }
		{
			if(ParserEnvironment.IsKnownPackage(id.ToString()))
				reportError(id.Coords, "The package " + id.ToString() + " cannot be defined - a builtin package of the same name already exists.");
		}
			( declsPatternMatchingOrAttributeEvaluationUnitWithModifier[patternChilds, actionChilds, 
					matchTypeChilds, filterChilds, matchClassChilds, matchClassFilterChilds, matchTypeIteratedChilds,
					functionChilds, procedureChilds, sequenceChilds]
			)*
	  RBRACE
		{
			PackageActionTypeNode pt = new PackageActionTypeNode(patternChilds, actionChilds, 
				matchTypeChilds, filterChilds, matchClassChilds, matchClassFilterChilds, matchTypeIteratedChilds,
				functionChilds, procedureChilds, sequenceChilds);
			id.Decl = new TypeDeclNode(id, pt);
			res = id;
		}
		{ env.CurrentPackage = null; env.PopScope(); }
	;

declsPatternMatchingOrAttributeEvaluationUnitWithModifier [ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds,
		CollectNode<IdentNode> matchTypeChilds, CollectNode<IdentNode> filterChilds,
		CollectNode<IdentNode> matchClassChilds, CollectNode<IdentNode> matchClassFilterChilds,
		CollectNode<IdentNode> matchTypeIteratedChilds, 
		CollectNode<IdentNode> functionChilds, CollectNode<IdentNode> procedureChilds,
		CollectNode<IdentNode> sequenceChilds ]
	@init {
		mod = 0;
	}
	: ( mod=patternModifiers declPatternMatchingOrAttributeEvaluationUnit[patternChilds, actionChilds,
			matchTypeChilds, filterChilds, matchClassChilds, matchClassFilterChilds, matchTypeIteratedChilds,
			functionChilds, procedureChilds, sequenceChilds, mod]
	  )
	;

patternModifiers returns [ int res = 0 ]
	: ( m=patternModifier[ res ]  { res = m; } )*
	;

patternModifier [ int mod ] returns [ int res = 0 ]
	: modifier=INDUCED
		{
			if((mod & PatternGraphLhsNode.MOD_INDUCED) != 0) {
				reportError(getCoords(modifier), "The modifier induced has already been declared.");
			}
			res = mod | PatternGraphLhsNode.MOD_INDUCED;
		}
	| modifier=EXACT
		{		
			if((mod & PatternGraphLhsNode.MOD_EXACT) != 0) {
				reportError(getCoords(modifier), "The modifier exact has already been declared.");
			}
			res = mod | PatternGraphLhsNode.MOD_EXACT;
		}
	| modifier=IDENT 
		{
			if(modifier.Text.Equals("dpo")) {
				if((mod & PatternGraphLhsNode.MOD_DANGLING) != 0 || (mod & PatternGraphLhsNode.MOD_IDENTIFICATION) != 0) {
					reportError(getCoords(modifier), "The modifier dpo or dangling or identification has already been declared.");
				}
				res = mod | PatternGraphLhsNode.MOD_DANGLING | PatternGraphLhsNode.MOD_IDENTIFICATION;
			} else if(modifier.Text.Equals("dangling")) {
				if((mod & PatternGraphLhsNode.MOD_DANGLING) != 0) {
					reportError(getCoords(modifier), "The modifier dangling has already been declared.");
				}
				res = mod | PatternGraphLhsNode.MOD_DANGLING;
			} else if(modifier.Text.Equals("identification")) {
				if((mod & PatternGraphLhsNode.MOD_IDENTIFICATION) != 0) {
					reportError(getCoords(modifier), "The modifier identification has already been declared.");
				}
				res = mod | PatternGraphLhsNode.MOD_IDENTIFICATION;
			} else {
				reportError(getCoords(modifier), "The modifier "+modifier.Text+" is not known.");
			}
		}
	;

declPatternMatchingOrAttributeEvaluationUnit [ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds,
		CollectNode<IdentNode> matchTypeChilds, CollectNode<IdentNode> filterChilds,
		CollectNode<IdentNode> matchClassChilds, CollectNode<IdentNode> matchClassFilterChilds,
		CollectNode<IdentNode> matchTypeIteratedChilds,
		CollectNode<IdentNode> functionChilds, CollectNode<IdentNode> procedureChilds,
		CollectNode<IdentNode> sequenceChilds, int mod ]
	@init {
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		RhsDeclNode rightHandSide = null;
		CollectNode<BaseNode> modifyParams = new CollectNode<BaseNode>();
		CollectNode<BaseNode> retTypes = new CollectNode<BaseNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>();
		CollectNode<BaseNode> conn = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evalss = new CollectNode<EvalStatementsNode>();
		CollectNode<IdentNode> implementedMatchTypes = new CollectNode<IdentNode>();
		ExecNode exec = null;
		AnonymousScopeNamer namer = new AnonymousScopeNamer(env);
		ActionDeclNode actionDecl = null;
		DefinedMatchTypeNode mt = null;
		env.MatchTypeChilds = matchTypeIteratedChilds;
		FunctionAutoNode functionAutoImplementation = null;
	}
	: t=TEST id=actionIdentDecl { matchTypeChilds.AddChild(MatchTypeActionNode.DefineMatchType(env, id)); env.CurrentActionOrSubpattern = id; env.PushScope(id); }
		paramz=parameters[BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS|BaseNode.CONTEXT_PARAMETER, null]
		ret=returnTypes (IMPLEMENTS matchClasses[implementedMatchTypes])? LBRACE
		left=patternBody[getCoords(t), paramz, conn, returnz, namer, mod, BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.ToString()]
			{
				actionDecl = new TestDeclNode(id, left, implementedMatchTypes, ret);
				id.Decl = actionDecl;
				actionChilds.AddChild(id);
			}
		defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evalss, returnz, namer, BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, left]
		RBRACE
		{
			if((mod & PatternGraphLhsNode.MOD_DANGLING) != 0 || (mod & PatternGraphLhsNode.MOD_IDENTIFICATION) != 0) {
				reportError(getCoords(t), "None of the modifiers dpo or dangling or identification is allowed for a test.");
			}
		}
		filterDecls[id, actionDecl]
		{ env.PopScope(); }
		{ env.CurrentActionOrSubpattern = null; }
	| r=RULE id=actionIdentDecl { matchTypeChilds.AddChild(MatchTypeActionNode.DefineMatchType(env, id)); env.CurrentActionOrSubpattern = id; env.PushScope(id); }
		paramz=parameters[BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS|BaseNode.CONTEXT_PARAMETER, null]
		ret=returnTypes (IMPLEMENTS matchClasses[implementedMatchTypes])? LBRACE
		left=patternBody[getCoords(r), paramz, conn, new CollectNode<ExprNode>(), namer, mod, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.ToString()]
		defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evalss, new CollectNode<ExprNode>(), namer, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, left]
		( rightReplace=replacePart[new CollectNode<BaseNode>(), namer, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				actionDecl = new RuleDeclNode(id, left, implementedMatchTypes, rightReplace, ret);
				id.Decl = actionDecl;
				actionChilds.AddChild(id);
			}
		| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				actionDecl = new RuleDeclNode(id, left, implementedMatchTypes, rightModify, ret);
				id.Decl = actionDecl;
				actionChilds.AddChild(id);
			}
		| emptyRightModify=emptyModifyPart[getCoords(r), dels, new CollectNode<BaseNode>(), BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				actionDecl = new RuleDeclNode(id, left, implementedMatchTypes, emptyRightModify, ret);
				id.Decl = actionDecl;
				actionChilds.AddChild(id);
			}
		)
		RBRACE
		filterDecls[id, actionDecl]
		{ env.PopScope(); }
		{ env.CurrentActionOrSubpattern = null; }
	| p=PATTERN id=patIdentDecl { env.CurrentActionOrSubpattern = id; env.PushScope(id); }
		paramz=patternParameters[namer, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS|BaseNode.CONTEXT_PARAMETER, null] 
		((MODIFY|REPLACE) mp=patternParameters[namer, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS|BaseNode.CONTEXT_PARAMETER, null] { modifyParams = mp; })?
		LBRACE
		left=patternBody[getCoords(p), paramz, conn, new CollectNode<ExprNode>(), namer, mod, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS, id.ToString()]
		defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evalss, new CollectNode<ExprNode>(), namer, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS, left]
		( rightReplace=replacePart[modifyParams, namer, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id, left]
			{ rightHandSide = rightReplace; }
		| rightModify=modifyPart[dels, modifyParams, namer, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id, left]
			{ rightHandSide = rightModify; }
		)?
			{
				id.Decl = new SubpatternDeclNode(id, left, rightHandSide);
				patternChilds.AddChild(id);
			}
		RBRACE { env.PopScope(); }
		{ env.CurrentActionOrSubpattern = null; }
	| s=SEQUENCE id=actionIdentDecl { env.PushScope(id); } { exec = new ExecNode(getCoords(s)); }
		inParams=sequenceInParameters[exec] outParams=sequenceOutParameters[exec]
		LBRACE 
			sequence[exec]
		RBRACE { env.PopScope(); }
		{
			id.Decl = new SequenceDeclNode(id, exec, inParams, outParams);
			sequenceChilds.AddChild(id);
		}
	| EXTERNAL s=SEQUENCE id=actionIdentDecl { env.PushScope(id); } { exec = new ExecNode(getCoords(s)); }
		inParams=sequenceInParameters[exec] outParams=sequenceOutParameters[exec]
		SEMI { env.PopScope(); }
		{
			id.Decl = new SequenceDeclNode(id, exec, inParams, outParams);
			sequenceChilds.AddChild(id);
		}
	| f=FUNCTION id=funcOrExtFuncIdentDecl { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
		COLON retType=returnType
		{
			if(ParserEnvironment.IsGlobalFunction(id.ToString(), paramz.ChildrenExact.Count))
				reportError(id.Coords, "The function " + id.ToString() + " cannot be defined - a builtin function of the same name and with the same number of parameters already exists.");
		}
		LBRACE
			(
				AUTO LPAREN functionAuto=autoFunctionBody { functionAutoImplementation = functionAuto; } RPAREN
			|
				( c=computation[false, false, namer, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
					{ evals.AddChild(c); }
				)*
			)
		RBRACE { env.PopScope(); }
		{
			id.Decl = new FunctionDeclNode(id, evals, functionAutoImplementation, paramz, retType, false);
			functionChilds.AddChild(id);
		}
	| pr=PROCEDURE id=funcOrExtFuncIdentDecl { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, PatternGraphLhsNode.Invalid]
		(COLON LPAREN (returnTypeList[retTypes])? RPAREN)?
		{
			if(ParserEnvironment.IsGlobalProcedure(id.ToString(), paramz.ChildrenExact.Count))
				reportError(id.Coords, "The procedure " + id.ToString() + " cannot be defined - a builtin procedure of the same name and with the same number of parameters already exists.");
		}
		LBRACE
			( c=computation[false, false, namer, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, PatternGraphLhsNode.Invalid]
				{ evals.AddChild(c); }
			)*
		RBRACE { env.PopScope(); }
		{
			id.Decl = new ProcedureDeclNode(id, evals, paramz, retTypes, false);
			procedureChilds.AddChild(id);
		}
	| f=FILTER id=actionIdentDecl filterFunctionDecl[f, id, filterChilds, matchClassFilterChilds]
	| EXTERNAL f=FILTER id=actionIdentDecl externalFilterFunctionDecl[f, id, filterChilds, matchClassFilterChilds]
	| MATCH mc=CLASS id=typeIdentDecl { env.PushScope(id); } LBRACE
		(body=matchClassBody[getCoords(mc), namer, mod, BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.ToString()]
			{
				mt = new DefinedMatchTypeNode(body);
				id.Decl = new TypeDeclNode(id, mt);
				matchClassChilds.AddChild(id);
			}
		| autoBody=matchClassAutoBody[getCoords(mc), namer, mod, BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.ToString()]
			{
				mt = new DefinedMatchTypeNode(autoBody);
				id.Decl = new TypeDeclNode(id, mt);
				matchClassChilds.AddChild(id);
			}
		)
		RBRACE
		matchClassFilterDecls[id, mt]
		{ env.PopScope(); }
	;

defEntitiesOrYieldings [ CollectNode<BaseNode> conn, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<EvalStatementsNode> evals, CollectNode<ExprNode> returnz,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: reportErrorOnDefEntityOrYielding[namer, context]
	  ( TRIPLEMINUS
		( defEntityToBeYieldedTo[conn, defVariablesToBeYieldedTo, evals, namer, context, directlyNestingLHSGraph] SEMI // single entity definitions to be filled by later yield assignments
		| iteratedFiltering[evals, namer, context]
		| yielding[evals, namer, context, directlyNestingLHSGraph]
		| rets[returnz, namer, context] SEMI
		)*
	  )?
	  { directlyNestingLHSGraph.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo); }
	  { directlyNestingLHSGraph.AddYieldings(evals); }
	;

reportErrorOnDefEntityOrYielding [ AnonymousScopeNamer namer, int context ]
	: ( 
		( d=DEF { reportError(getCoords(d), "A def entity declaration is only allowed in the yield part. Likely a --- separating the pattern part from the yield part is missing."); }
		//the iterated filter declaration collides with the iterated declaration, would require additional lookahead, but was not available before, so no help in transforming old grgen specifications, thus way less of importance -> left out
		//| i=ITERATED ident=iterIdentUse ( filter=filterUse[ident, namer, context] )+ SEMI { reportError(getCoords(i), "An iterated filter declaration is only allowed in the yield part. Likely a --- separating the pattern part from the yield part is missing."); } 
		| y=YIELD { reportError(getCoords(y), "A yield block is only allowed in the yield part. Likely a --- separating the pattern part from the yield part is missing."); }
		)
	  )?
	;

filterFunctionDecl [ IToken f, IdentNode id, CollectNode<IdentNode> filterChilds, CollectNode<IdentNode> matchClassFilterChilds ]
	@init {
		CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>();
		AnonymousScopeNamer namer = new AnonymousScopeNamer(env);
	}
	: LT actionId=actionIdentUse GT { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
		LBRACE
			{
				evals.AddChild(new DefDeclStatementNode(getCoords(f), new VarDeclNode(
						new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(f))),
						new ArrayTypeNode(MatchTypeActionNode.GetMatchTypeIdentNode(env, actionId)),
						PatternGraphLhsNode.Invalid, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, true, false, "ref"),
					BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION));
			}
			( c=computation[false, false, namer, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
				{ evals.AddChild(c); }
			)*
		RBRACE { env.PopScope(); }
		{
			FilterFunctionDeclNode ff = new FilterFunctionDeclNode(id, evals, paramz, actionId);
			id.Decl = ff;
			filterChilds.AddChild(id);
		}
	| LT CLASS typeId=typeIdentUse GT { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
		LBRACE
			{
				evals.AddChild(new DefDeclStatementNode(getCoords(f), new VarDeclNode(
						new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(f))),
						new ArrayTypeNode(typeId),
						PatternGraphLhsNode.Invalid, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, true, false, "ref"),
					BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION));
			}
			( c=computation[false, false, namer, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
				{ evals.AddChild(c); }
			)*
		RBRACE { env.PopScope(); }
		{
			MatchClassFilterFunctionDeclNode mff = new MatchClassFilterFunctionDeclNode(id, evals, paramz, typeId);
			id.Decl = mff;
			matchClassFilterChilds.AddChild(id);
		}
	;

externalFilterFunctionDecl [ IToken f, IdentNode id, CollectNode<IdentNode> filterChilds, CollectNode<IdentNode> matchClassFilterChilds ]
	: LT actionId=actionIdentUse GT { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
		SEMI { env.PopScope(); }
		{
			FilterFunctionDeclNode ff = new FilterFunctionDeclNode(id, null, paramz, actionId);
			id.Decl = ff;
			filterChilds.AddChild(id);
		} 
	| LT CLASS typeId=typeIdentUse GT { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, PatternGraphLhsNode.Invalid]
		SEMI { env.PopScope(); }
		{
			MatchClassFilterFunctionDeclNode mff = new MatchClassFilterFunctionDeclNode(id, null, paramz, typeId);
			id.Decl = mff;
			matchClassFilterChilds.AddChild(id);
		} 
	;

matchClasses [ CollectNode<IdentNode> implementedMatchTypes ]
	: mtid=typeIdentUse { implementedMatchTypes.AddChild(mtid); }
		( COMMA mtid=typeIdentUse { implementedMatchTypes.AddChild(mtid); } )*
	;

parameters [ int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN (paramList[res, context, directlyNestingLHSGraph])? RPAREN
	|
	;

patternParameters [ AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN ( patternParamList [ res, context, directlyNestingLHSGraph ] )? ( TRIPLEMINUS patternDefParamList [ res, namer, context, directlyNestingLHSGraph ] )? RPAREN
	|
	;

paramList [ CollectNode<BaseNode> paramz, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: p=param[context, directlyNestingLHSGraph] { paramz.AddChild(p); }
		( COMMA p=param[context, directlyNestingLHSGraph] { paramz.AddChild(p); } )*
	;

patternParamList [ CollectNode<BaseNode> paramz, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: p=param[context, directlyNestingLHSGraph] { paramz.AddChild(p); }
		( COMMA p=param[context, directlyNestingLHSGraph] { paramz.AddChild(p); } )*
	;

patternDefParamList [ CollectNode<BaseNode> paramz, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: dp=defEntityToBeYieldedTo[null, null, null, namer, context, directlyNestingLHSGraph] { paramz.AddChild(dp); }
		( COMMA dp=defEntityToBeYieldedTo[null, null, null, namer, context, directlyNestingLHSGraph] { paramz.AddChild(dp); } )*
	;

param [ int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ BaseNode res = ParserEnvironment.InitNode() ]
	: MINUS edge=edgeDeclParam[context, directlyNestingLHSGraph] direction = forwardOrUndirectedEdgeParam
		{
			BaseNode dummy = env.GetDummyNodeDecl(context, directlyNestingLHSGraph);
			res = new ConnectionNode(dummy, edge, dummy, direction, ConnectionNode.NO_REDIRECTION);
		}
	| LARROW edge=edgeDeclParam[context, directlyNestingLHSGraph] RARROW
		{
			BaseNode dummy = env.GetDummyNodeDecl(context, directlyNestingLHSGraph);
			res = new ConnectionNode(dummy, edge, dummy, 
					ConnectionKind.ARBITRARY_DIRECTED, ConnectionNode.NO_REDIRECTION);
		}
	| QUESTIONMINUS edge=edgeDeclParam[context, directlyNestingLHSGraph] MINUSQUESTION
		{
			BaseNode dummy = env.GetDummyNodeDecl(context, directlyNestingLHSGraph);
			res = new ConnectionNode(dummy, edge, dummy,
					ConnectionKind.ARBITRARY, ConnectionNode.NO_REDIRECTION);
		}
	| v=varDecl[context, directlyNestingLHSGraph] 
		{ res = v; }
	| node=nodeDeclParam[context, directlyNestingLHSGraph]
		{ res = new SingleNodeConnNode(node); }
	;

forwardOrUndirectedEdgeParam returns [ ConnectionKind res = ConnectionKind.ARBITRARY ]
	: RARROW { res = ConnectionKind.DIRECTED; }
	| MINUS  { res = ConnectionKind.UNDIRECTED; }
	;

returnTypes returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: COLON LPAREN (returnTypeList[res])? RPAREN
	|
	;

returnTypeList [ CollectNode<BaseNode> returnTypes ]
	: t=returnType { returnTypes.AddChild(t); } ( COMMA t=returnType { returnTypes.AddChild(t); } )*
	;

returnType returns [ BaseNode res = ParserEnvironment.InitNode() ]
	: type=typeIdentUse { res = type; }
	| containerType=containerTypeUse { res = containerType; }
	;

filterDecls [ IdentNode actionIdent, ActionDeclNode actionDecl ]
	@init {
		List<FilterAutoDeclNode> filters = new List<FilterAutoDeclNode>();
	}
	: BACKSLASH filterDeclList[actionIdent, filters]
		{ actionDecl.AddFilters(filters); }
	|
	;

filterDeclsIterated [ IdentNode iteratedIdent, IteratedDeclNode iterated ]
	@init {
		List<FilterAutoDeclNode> filtersAutoGenerated = new List<FilterAutoDeclNode>();
		List<FilterAutoDeclNode> filtersAutoSupplied = env.GetFiltersAutoSupplied(iterated);
	}
	: BACKSLASH filterDeclList[iteratedIdent, filtersAutoGenerated]
		{
			if(iterated != null) // may happen due to syntactic predicate / backtracking peek ahead
				iterated.AddFilters(filtersAutoGenerated);
		}
	|
	;

filterDeclList [ IdentNode actionOrIteratedIdent, List<FilterAutoDeclNode> filters ]
	@init {
		String filterBaseText = null;
	}
	: (filterBase=IDENT | filterBase=AUTO) { filterBaseText = filterBase.Text; } (LT fvl=filterVariableList GT (filterBaseTextExt=filterExtension[filterBaseText, fvl] { filterBaseText = filterBaseTextExt; })?)? 
		{
			String fullName = filterBaseText + (fvl != null ? "<" + join("_", fvl) + ">" : "");
			IdentNode filterIdent = new IdentNode(env.Define(ParserEnvironment.ACTIONS, fullName, getCoords(filterBase)));

			FilterAutoGeneratedDeclNode filterAutoGenerated;
			if(fullName.Equals("auto"))
				filterAutoGenerated = new FilterAutoGeneratedDeclNode(filterIdent, "auto", null, actionOrIteratedIdent);
			else
				filterAutoGenerated = new FilterAutoGeneratedDeclNode(filterIdent, filterBaseText, fvl, actionOrIteratedIdent);

			filterIdent.Decl = filterAutoGenerated;
			filters.Add(filterAutoGenerated);
		}
	(
		filterDeclListContinuation [ actionOrIteratedIdent, filters ]
	)*
	;

filterDeclListContinuation [ IdentNode actionOrIteratedIdent, List<FilterAutoDeclNode> filters ]
	: COMMA (filterBase=IDENT | filterBase=AUTO) (LT fvl=filterVariableList GT)?
		{
			String fullName = filterBase.Text + (fvl != null ? "<" + join("_", fvl) + ">" : "");
			IdentNode filterIdent = new IdentNode(env.Define(ParserEnvironment.ACTIONS, fullName, getCoords(filterBase)));

			FilterAutoGeneratedDeclNode filterAutoGenerated;
			if(fullName.Equals("auto"))
				filterAutoGenerated = new FilterAutoGeneratedDeclNode(filterIdent, "auto", null, actionOrIteratedIdent);
			else
				filterAutoGenerated = new FilterAutoGeneratedDeclNode(filterIdent, filterBase.Text, fvl, actionOrIteratedIdent);

			filterIdent.Decl = filterAutoGenerated;
			filters.Add(filterAutoGenerated);
		}
	;

filterVariableList returns [ List<String> filterVariables = new List<String>() ]
	: filterVar=IDENT { filterVariables.Add(filterVar.Text); }
		( COMMA filterVar=IDENT { filterVariables.Add(filterVar.Text); } )*
	;

matchClassFilterDecls [ IdentNode matchClassIdent, DefinedMatchTypeNode matchClass ]
	@init {
		List<MatchClassFilterCharacter> matchClassFilters = new List<MatchClassFilterCharacter>();
		matchClass.AddFilters(matchClassFilters);
	}
	: BACKSLASH matchClassFilterDeclList[matchClassIdent, matchClassFilters]
	|
	;

matchClassFilterDeclList [ IdentNode matchClassIdent, List<MatchClassFilterCharacter> matchClassFilters ]
	@init {
		String filterBaseText = null;
	}
	: filterBase=IDENT { filterBaseText = filterBase.Text; } LT fvl=filterVariableList GT (filterBaseTextExt=filterExtension[filterBaseText, fvl] { filterBaseText = filterBaseTextExt; })?
		{
			String fullName = filterBaseText + (fvl != null ? "<" + join("_", fvl) + ">" : "");
			IdentNode filterIdent = new IdentNode(env.Define(ParserEnvironment.ACTIONS, fullName, getCoords(filterBase)));

			MatchClassFilterAutoGeneratedDeclNode filterAutoGenerated = 
				new MatchClassFilterAutoGeneratedDeclNode(filterIdent, filterBaseText, fvl, matchClassIdent);

			filterIdent.Decl = filterAutoGenerated;
			matchClassFilters.Add(filterAutoGenerated);
		}
	(
		matchClassFilterDeclListContinuation [ matchClassIdent, matchClassFilters ]
	)*
	;

filterExtension [ String idText, List<String> filterVariables ] returns [ String res = null ]
	: idExtension=IDENT LT fvl2=filterVariableList GT idExtension2=IDENT LT fvl3=filterVariableList GT
		{
			filterVariables.Add(fvl2.get(0));
			filterVariables.Add(fvl3.get(0));
			res = idText + idExtension.Text + idExtension2.Text;
		}
	;

matchClassFilterDeclListContinuation [ IdentNode matchClassIdent, List<MatchClassFilterCharacter> matchClassFilters ]
	: COMMA filterBase=IDENT LT fvl=filterVariableList GT
		{
			String fullName = filterBase.Text + (fvl != null ? "<" + join("_", fvl) + ">" : "");
			IdentNode filterIdent = new IdentNode(env.Define(ParserEnvironment.ACTIONS, fullName, getCoords(filterBase)));

			MatchClassFilterAutoGeneratedDeclNode filterAutoGenerated =
				new MatchClassFilterAutoGeneratedDeclNode(filterIdent, filterBase.Text, fvl, matchClassIdent);

			filterIdent.Decl = filterAutoGenerated;
			matchClassFilters.Add(filterAutoGenerated);
		}
	;

replacePart [ CollectNode<BaseNode> paramz, AnonymousScopeNamer namer,
		int context, IdentNode nameOfRHS, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ ReplaceDeclNode res = null ]
	@init {
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
	}
	: r=REPLACE ( id=rhsIdentDecl { nameOfRHS = id; } )?
		LBRACE
		b=replaceBody[getCoords(r), paramz, connections, defVariablesToBeYieldedTo,
				evals, orderedReplacements, imperativeStmts, returnz,
				namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		defEntitiesOrEvals[connections, defVariablesToBeYieldedTo,
				evals, orderedReplacements, imperativeStmts, returnz,
				namer, context, b.RhsGraph, directlyNestingLHSGraph]
		RBRACE
	| LBRACEMINUS 
		{ paramz = new CollectNode<BaseNode>(); }
		b=replaceBody[getCoords(r), paramz, connections, defVariablesToBeYieldedTo,
				evals, orderedReplacements, imperativeStmts, returnz,
				namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		defEntitiesOrEvals[connections, defVariablesToBeYieldedTo, evals,
				orderedReplacements, imperativeStmts, returnz, 
				namer, context, b.RhsGraph, directlyNestingLHSGraph]
		RBRACE
	;

modifyPart [ CollectNode<IdentNode> dels, CollectNode<BaseNode> paramz, AnonymousScopeNamer namer,
		int context, IdentNode nameOfRHS, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ ModifyDeclNode res = null ]
	@init {
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
	}
	: m=MODIFY ( id=rhsIdentDecl { nameOfRHS = id; } )?
		LBRACE
		b=modifyBody[getCoords(m), dels, paramz, connections, defVariablesToBeYieldedTo,
				evals, orderedReplacements, imperativeStmts, returnz, 
				namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		defEntitiesOrEvals[connections, defVariablesToBeYieldedTo,
				evals, orderedReplacements, imperativeStmts, returnz,
				namer, context, b.RhsGraph, directlyNestingLHSGraph]
		RBRACE
	| LBRACEPLUS 
		{ paramz = new CollectNode<BaseNode>(); }
		b=modifyBody[getCoords(m), dels, paramz, connections, defVariablesToBeYieldedTo,
				evals, orderedReplacements, imperativeStmts, returnz,
				namer, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		defEntitiesOrEvals[connections, defVariablesToBeYieldedTo, evals,
				orderedReplacements, imperativeStmts, returnz,
				namer, context, b.RhsGraph, directlyNestingLHSGraph]
	  RBRACE
	;

emptyModifyPart [ Coords coords, CollectNode<IdentNode> dels, CollectNode<BaseNode> paramz,
		int context, IdentNode nameOfRHS, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ ModifyDeclNode res = null ]
	@init {
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
		CollectNode<SubpatternReplNode> subpatternRepls = new CollectNode<SubpatternReplNode>();
		CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		PatternGraphRhsNode patternGraph = new PatternGraphRhsNode(nameOfRHS.ToString(), coords, 
			connections, paramz, subpatterns, subpatternRepls,
			orderedReplacements, returnz, imperativeStmts,
			context, directlyNestingLHSGraph);
		patternGraph.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
		patternGraph.AddEvals(evals);
		res = new ModifyDeclNode(nameOfRHS, patternGraph, dels);
	}
	: 
	;

patternBody [ Coords coords, CollectNode<BaseNode> paramz, CollectNode<BaseNode> connections,
		CollectNode<ExprNode> returnz, AnonymousScopeNamer namer, int mod, int context, String nameOfGraph ]
		returns [ PatternGraphLhsNode res = null ]
	@init {
		CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
		CollectNode<SubpatternReplNode> subpatternRepls = new CollectNode<SubpatternReplNode>();
		CollectNode<AlternativeDeclNode> alts = new CollectNode<AlternativeDeclNode>();
		CollectNode<IteratedDeclNode> iters = new CollectNode<IteratedDeclNode>();
		CollectNode<PatternGraphLhsNode> negs = new CollectNode<PatternGraphLhsNode>();
		CollectNode<PatternGraphLhsNode> idpts = new CollectNode<PatternGraphLhsNode>();
		CollectNode<ExprNode> conds = new CollectNode<ExprNode>();
		CollectNode<HomNode> homs = new CollectNode<HomNode>();
		CollectNode<TotallyHomNode> totallyhoms = new CollectNode<TotallyHomNode>();
		CollectNode<ExactNode> exact = new CollectNode<ExactNode>();
		CollectNode<InducedNode> induced = new CollectNode<InducedNode>();
		res = new PatternGraphLhsNode(nameOfGraph, coords, 
				connections, paramz, subpatterns, subpatternRepls,
				alts, iters, negs, idpts, conds,
				returnz, homs, totallyhoms, exact, induced, mod, context);
	}
	: ( patternStmt[connections, subpatterns, subpatternRepls,
			alts, iters, negs, idpts, namer, conds,
			returnz, homs, totallyhoms, exact, induced, context, res] )*
	;

patternStmt [ CollectNode<BaseNode> conn,
		CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
		CollectNode<AlternativeDeclNode> alts, CollectNode<IteratedDeclNode> iters,
		CollectNode<PatternGraphLhsNode> negs, CollectNode<PatternGraphLhsNode> idpts,
		AnonymousScopeNamer namer, CollectNode<ExprNode> conds,
		CollectNode<ExprNode> returnz, CollectNode<HomNode> homs, CollectNode<TotallyHomNode> totallyhoms,
		CollectNode<ExactNode> exact, CollectNode<InducedNode> induced,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[conn, subpatterns, subpatternRepls, namer, context, directlyNestingLHSGraph] SEMI
	| (iteratedEBNFNotation[AnonymousScopeNamer.DummyNamer, 0]) => iter=iteratedEBNFNotation[namer, context] { iters.AddChild(iter); } // must scan ahead to end of () to see if *,+,?,[ is following in order to distinguish from one-case alternative ()
	| iter=iterated[namer, context] { iters.AddChild(iter); }
	| alt=alternative[namer, context] { alts.AddChild(alt); }
	| neg=negative[namer, context] { negs.AddChild(neg); }
	| idpt=independent[namer, context] { idpts.AddChild(idpt); }
	| condition[conds, namer, context]
	| rets[returnz, namer, context] SEMI
	| hom=homStatement { homs.AddChild(hom); } SEMI
	| totallyhom=totallyHomStatement { totallyhoms.AddChild(totallyhom); } SEMI
	| exa=exactStatement { exact.AddChild(exa); } SEMI
	| ind=inducedStatement { induced.AddChild(ind); } SEMI
	;

matchClassBody [ Coords coords, AnonymousScopeNamer namer, int mod, int context, String nameOfGraph ]
		returns [ PatternGraphLhsNode res = null ]
	@init {
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<BaseNode> varDecls = new CollectNode<BaseNode>();
		CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
		CollectNode<SubpatternReplNode> subpatternRepls = new CollectNode<SubpatternReplNode>();
		CollectNode<AlternativeDeclNode> alts = new CollectNode<AlternativeDeclNode>();
		CollectNode<IteratedDeclNode> iters = new CollectNode<IteratedDeclNode>();
		CollectNode<PatternGraphLhsNode> negs = new CollectNode<PatternGraphLhsNode>();
		CollectNode<PatternGraphLhsNode> idpts = new CollectNode<PatternGraphLhsNode>();
		CollectNode<ExprNode> conds = new CollectNode<ExprNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<HomNode> homs = new CollectNode<HomNode>();
		CollectNode<TotallyHomNode> totallyhoms = new CollectNode<TotallyHomNode>();
		CollectNode<ExactNode> exact = new CollectNode<ExactNode>();
		CollectNode<InducedNode> induced = new CollectNode<InducedNode>();
		res = new PatternGraphLhsNode(nameOfGraph, coords, 
				connections, varDecls, subpatterns, subpatternRepls,
				alts, iters, negs, idpts, conds,
				returnz, homs, totallyhoms, exact, induced, mod, context);
		res.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
		res.AddYieldings(evals);
	}
	: ( matchClassStmt[connections, defVariablesToBeYieldedTo, varDecls, subpatterns, subpatternRepls, namer, context, res] )*
	;

matchClassAutoBody [ Coords coords, AnonymousScopeNamer namer, int mod, int context, String nameOfGraph ]
		returns [ MatchClassAutoNode res = null ]
	@init {
		CollectNode<IdentNode> matchTypes = new CollectNode<IdentNode>();
	}
	: AUTO LPAREN matchTypeIdent=matchTypeIdentUse { matchTypes.AddChild(matchTypeIdent); }
		(BOR matchTypeIdentFollowing=matchTypeIdentUse { matchTypes.AddChild(matchTypeIdentFollowing); } )+ RPAREN
		{ res = new MatchClassAutoNode(nameOfGraph, coords, mod, context, matchTypes); }
	;

matchClassStmt [ CollectNode<BaseNode> conn, CollectNode<VarDeclNode> defVariablesToBeYieldedTo, CollectNode<BaseNode> varDecls, 
		CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[conn, subpatterns, subpatternRepls, namer, context, directlyNestingLHSGraph] SEMI
	| v=varDecl[context, directlyNestingLHSGraph] { varDecls.AddChild(v); } SEMI
	| defEntityToBeYieldedTo[conn, defVariablesToBeYieldedTo, null, namer, context, directlyNestingLHSGraph] SEMI // single entity definitions to be filled by later yield assignments
	;

connectionsOrSubpattern [ CollectNode<BaseNode> conn,
		CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: firstEdge[conn, namer, context, directlyNestingLHSGraph] // connection starts with an edge which dangles on the left
	| firstNodeOrSubpattern[conn, subpatterns, subpatternRepls, namer, context, directlyNestingLHSGraph] // there's a subpattern or a connection that starts with a node
	;

firstEdge [ CollectNode<BaseNode> conn, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		bool forward = true;
		Mutable<ConnectionKind> direction =
				new Mutable<ConnectionKind>(ConnectionKind.ARBITRARY);
		Mutable<int> redirection = new Mutable<int>(ConnectionNode.NO_REDIRECTION);
	}
	:   ( e=forwardOrUndirectedEdgeOcc[namer, context, direction, redirection, directlyNestingLHSGraph] { forward=true; } // get first edge
		| e=backwardOrArbitraryDirectedEdgeOcc[namer, context, direction, redirection, directlyNestingLHSGraph] { forward=false; }
		| e=arbitraryEdgeOcc[namer, context, directlyNestingLHSGraph] { forward=false; direction.Value = ConnectionKind.ARBITRARY; }
		)
		nodeContinuation[e, env.GetDummyNodeDecl(context, directlyNestingLHSGraph), forward, direction, redirection, conn,
				namer, context, directlyNestingLHSGraph] // and continue looking for node
	;

firstNodeOrSubpattern [ CollectNode<BaseNode> conn,
		CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		id = ParserEnvironment.DummyIdent;
		IdentNode type = env.NodeRoot;
		TypeExprNode constr = TypeExprNode.Empty;
		CollectNode<ExprNode> subpatternReplConn = new CollectNode<ExprNode>();
		IdentNode curId = ParserEnvironment.DummyIdent;
		NodeDeclNode nodeDecl = null;
	}
	: id=entIdentUse firstEdgeContinuation[id, conn, namer, context, directlyNestingLHSGraph] // use of already declared node, continue looking for first edge
	| id=entIdentUse l=LPAREN arguments[subpatternReplConn, namer, context] RPAREN // use of already declared subpattern
		{ subpatternRepls.AddChild(new SubpatternReplNode(id, subpatternReplConn)); }
	| id=entIdentDecl cc=COLON // node or subpattern declaration
		firstNodeOrSubpatternDeclaration[id, conn, subpatterns, namer, context, directlyNestingLHSGraph]
	| c=COLON // anonymous node or subpattern declaration
		anonymousFirstNodeOrSubpatternDeclaration[c, conn, subpatterns, namer, context, directlyNestingLHSGraph]
	| d=DOT { id = env.DefineAnonymousEntity("node", getCoords(d)); } // anonymous node declaration of type node		
		{ nodeDecl = new NodeDeclNode(id, type, CopyKind.None, context, constr, directlyNestingLHSGraph); }
		//( AT LPAREN nameAndAttributesInitializationList[nodeDecl, context] RPAREN )?
		firstEdgeContinuation[nodeDecl, conn, namer, context, directlyNestingLHSGraph] // and continue looking for first edge
	;

firstNodeOrSubpatternDeclaration [ IdentNode id, CollectNode<BaseNode> conn, CollectNode<SubpatternUsageDeclNode> subpatterns, 
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	options { k = 4; }
	@init {
		type = env.NodeRoot;
		constr = TypeExprNode.Empty;
		CollectNode<ExprNode> subpatternConn = new CollectNode<ExprNode>();
		curId = ParserEnvironment.DummyIdent;
		CollectNode<IdentNode> mergees = new CollectNode<IdentNode>();
		NodeDeclNode nodeDecl = null;
		CopyKind copyKind = CopyKind.None;
	}
	: // node declaration
		type=typeIdentUse
		( constr=typeConstraint )?
		( 
			{ nodeDecl = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		| LT oldid=entIdentUse ( COMMA curId=entIdentUse { mergees.AddChild(curId); } )* GT
			{ nodeDecl = new NodeTypeChangeDeclNode(id, type, context, oldid, mergees, directlyNestingLHSGraph); }
		| LBRACE nsic=nodeStorageIndexContinuation [ id, type, namer, context, directlyNestingLHSGraph ] RBRACE
			{ nodeDecl = nsic; }
		)
		( AT LPAREN nameAndAttributesInitializationList[nodeDecl, namer, context] RPAREN )?
		firstEdgeContinuation[nodeDecl, conn, namer, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node typeof declaration
		TYPEOF LPAREN type=entIdentUse RPAREN
		( constr=typeConstraint )?
		( LT oldid=entIdentUse ( COMMA curId=entIdentUse { mergees.AddChild(curId); } )* GT )?
		{
			if(oldid == null) {
				nodeDecl = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph);
			} else {
				nodeDecl = new NodeTypeChangeDeclNode(id, type, context, oldid, mergees, directlyNestingLHSGraph);
			}
		}
		( AT LPAREN nameAndAttributesInitializationList[nodeDecl, namer, context] RPAREN )?
		firstEdgeContinuation[nodeDecl, conn, namer, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node copy/clone declaration
		( COPY { copyKind = CopyKind.Copy; } | CLONE { copyKind = CopyKind.Clone; } ) LT type=entIdentUse GT 
		{ nodeDecl = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		( AT LPAREN nameAndAttributesInitializationList[nodeDecl, namer, context] RPAREN )?
		firstEdgeContinuation[nodeDecl, conn, namer, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // subpattern declaration
		type=patIdentUse LPAREN arguments[subpatternConn, namer, context] RPAREN
		{ subpatterns.AddChild(new SubpatternUsageDeclNode(id, type, context, subpatternConn)); }
	;

nodeStorageIndexContinuation [ IdentNode id, IdentNode type, AnonymousScopeNamer namer, int context,
		PatternGraphLhsNode directlyNestingLHSGraph ] returns [ NodeDeclNode nodeDecl = null ]
	: (DOUBLECOLON)? oldid=entIdentUse (d=DOT attr=entIdentUse)? (LBRACK (DOUBLECOLON)? mapAccess=entIdentUse RBRACK)?
		{
			if(mapAccess == null) {
				nodeDecl = new MatchNodeFromStorageDeclNode(id, type, context, 
					attr == null ? (BaseNode)new IdentExprNode(oldid) : (BaseNode)new QualIdentNode(getCoords(d), oldid, attr), directlyNestingLHSGraph);
			} else {
				nodeDecl = new MatchNodeByStorageAccessDeclNode(id, type, context, 
					attr == null ? (BaseNode)new IdentExprNode(oldid) : (BaseNode)new QualIdentNode(getCoords(d), oldid, attr), new IdentExprNode(mapAccess), directlyNestingLHSGraph);
			}
		}
	| idx=indexIdentUse EQUAL e=expr[namer, context, false]
		{
			nodeDecl = new MatchNodeByIndexAccessEqualityDeclNode(id, type, context, 
						idx, e, directlyNestingLHSGraph);
		}
	| i=IDENT LPAREN idx=indexIdentUse (os=relOS e=expr[namer, context, false] (COMMA idx2=indexIdentUse os2=relOS e2=expr[namer, context, false])?)? RPAREN
		{
			if(i.Text.Equals("ascending")) {
				nodeDecl = new MatchNodeByIndexAccessOrderingDeclNode(id, type, context, 
						true, idx, os, e, os2, e2, directlyNestingLHSGraph);
			} else if(i.Text.Equals("descending")) {
				nodeDecl = new MatchNodeByIndexAccessOrderingDeclNode(id, type, context, 
						false, idx, os, e, os2, e2, directlyNestingLHSGraph);
			} else
				reportError(getCoords(i), "An ordered index access must start with ascending or descending (given is " + i.Text + ").");
			if(idx2 != null && !idx.ToString().Equals(idx2.ToString()))
				reportError(idx2.Coords, "The same index must be used in an ordered index access with two constraints (given are " + idx + " and " + idx2 + ").");
		}
	| i=MULTIPLE
			{ nodeDecl = new MatchNodeByIndexAccessMultipleDeclNode(id, type, context, directlyNestingLHSGraph); }
		LPAREN
			idx=indexIdentUse os=relOS e=expr[namer, context, false] COMMA idx2=indexIdentUse os2=relOS e2=expr[namer, context, false]
			{ 
				((MatchNodeByIndexAccessMultipleDeclNode)nodeDecl).AddIndexAccessPart(new MatchByIndexAccessOrderingPartNode(idx, os, e, os2, e2, (MatchNodeByIndexAccessMultipleDeclNode)nodeDecl));
				if(idx2 != null && !idx.ToString().Equals(idx2.ToString()))
					reportError(idx2.Coords, "The same index must be used with the two constraints per index of a multiple index access (given are " + idx + " and " + idx2 + ").");
			}
			nodeMultipleIndexContinuation[(MatchNodeByIndexAccessMultipleDeclNode)nodeDecl, namer, context, directlyNestingLHSGraph]
		RPAREN
	| AT LPAREN e=expr[namer, context, false] RPAREN
		{
			nodeDecl = new MatchNodeByNameLookupDeclNode(id, type, context, 
						e, directlyNestingLHSGraph);
		}
	| {input.LT(1).Text.Equals("unique")}? i=IDENT LBRACK e=expr[namer, context, false] RBRACK
		{
			nodeDecl = new MatchNodeByUniqueLookupDeclNode(id, type, context,
						e, directlyNestingLHSGraph);
		}
	;

nodeMultipleIndexContinuation [ MatchNodeByIndexAccessMultipleDeclNode nodeDecl, AnonymousScopeNamer namer, int context,
		PatternGraphLhsNode directlyNestingLHSGraph ]
	: COMMA idx=indexIdentUse os=relOS e=expr[namer, context, false] COMMA idx2=indexIdentUse os2=relOS e2=expr[namer, context, false]
		{ 
			nodeDecl.AddIndexAccessPart(new MatchByIndexAccessOrderingPartNode(idx, os, e, os2, e2, nodeDecl));
			if(idx2 != null && !idx.ToString().Equals(idx2.ToString()))
				reportError(idx2.Coords, "The same index must be used with the two constraints per index of a multiple index access (given are " + idx + " and " + idx2 + ").");
		}
		nodeMultipleIndexContinuation[nodeDecl, namer, context, directlyNestingLHSGraph]
	|
	;

relOS returns [ Operator os = Operator.ERROR ]
	: lt=LT { os = Operator.LT; }
	| le=LE { os = Operator.LE; }
	| gt=GT { os = Operator.GT; }
	| ge=GE { os = Operator.GE; }
	;

anonymousFirstNodeOrSubpatternDeclaration [ IToken c, CollectNode<BaseNode> conn, CollectNode<SubpatternUsageDeclNode> subpatterns, 
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ IdentNode id = ParserEnvironment.DummyIdent ]
	options { k = 4; }
	@init {
		type = env.NodeRoot;
		constr = TypeExprNode.Empty;
		CollectNode<ExprNode> subpatternConn = new CollectNode<ExprNode>();
		curId = ParserEnvironment.DummyIdent;
		CollectNode<IdentNode> mergees = new CollectNode<IdentNode>();
		NodeDeclNode nodeDecl = null;
		CopyKind copyKind = CopyKind.None;
	}
	:  // node declaration
		{ id = env.DefineAnonymousEntity("node", getCoords(c)); }
		type=typeIdentUse
		( constr=typeConstraint )?
		(
			{ nodeDecl = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		| LT oldid=entIdentUse ( COMMA curId=entIdentUse { mergees.AddChild(curId); } )* GT
			{ nodeDecl = new NodeTypeChangeDeclNode(id, type, context, oldid, mergees, directlyNestingLHSGraph); }
		| LBRACE nsic=nodeStorageIndexContinuation [ id, type, namer, context, directlyNestingLHSGraph ] RBRACE
			{ nodeDecl = nsic; }
		)
		( AT LPAREN nameAndAttributesInitializationList[nodeDecl, namer, context] RPAREN )?
		firstEdgeContinuation[nodeDecl, conn, namer, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node typeof declaration
		{ id = env.DefineAnonymousEntity("node", getCoords(c)); }
		TYPEOF LPAREN type=entIdentUse RPAREN
		( constr=typeConstraint )?
		( LT oldid=entIdentUse ( COMMA curId=entIdentUse { mergees.AddChild(curId); } )* GT )?
		{
			if(oldid == null) {
				nodeDecl = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph);
			} else {
				nodeDecl = new NodeTypeChangeDeclNode(id, type, context, oldid, mergees, directlyNestingLHSGraph);
			}
		}
		( AT LPAREN nameAndAttributesInitializationList[nodeDecl, namer, context] RPAREN )?
		firstEdgeContinuation[nodeDecl, conn, namer, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // node copy/clone declaration
		{ id = env.DefineAnonymousEntity("node", getCoords(c)); }
		( COPY { copyKind = CopyKind.Copy; } | CLONE { copyKind = CopyKind.Clone; } ) LT type=entIdentUse GT 
		{ nodeDecl = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		( AT LPAREN nameAndAttributesInitializationList[nodeDecl, namer, context] RPAREN )?
		firstEdgeContinuation[nodeDecl, conn, namer, context, directlyNestingLHSGraph] // and continue looking for first edge
	| // subpattern declaration
		{ id = env.DefineAnonymousEntity("sub", getCoords(c)); }
		type=patIdentUse LPAREN arguments[subpatternConn, namer, context] RPAREN
		{ subpatterns.AddChild(new SubpatternUsageDeclNode(id, type, context, subpatternConn)); }
	;

defEntityToBeYieldedTo [ CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<EvalStatementsNode> evals, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ BaseNode res = ParserEnvironment.InitNode() ]
	: DEF (
		MINUS edge=defEdgeToBeYieldedTo[context, directlyNestingLHSGraph] direction=forwardOrUndirectedEdgeParam
			{
				BaseNode dummy = env.GetDummyNodeDecl(context, directlyNestingLHSGraph);
				res = new ConnectionNode(dummy, edge, dummy, direction, ConnectionNode.NO_REDIRECTION);
				if(connections != null)
					connections.AddChild(res);
			}
		  ( defGraphElementInitialization[namer, context, edge] )? 
		| LARROW edge=defEdgeToBeYieldedTo[context, directlyNestingLHSGraph] RARROW
			{
				BaseNode dummy = env.GetDummyNodeDecl(context, directlyNestingLHSGraph);
				res = new ConnectionNode(dummy, edge, dummy,
						ConnectionKind.ARBITRARY_DIRECTED, ConnectionNode.NO_REDIRECTION);
				if(connections != null)
					connections.AddChild(res);
			}
		  ( defGraphElementInitialization[namer, context, edge] )? 
		| QUESTIONMINUS edge=defEdgeToBeYieldedTo[context, directlyNestingLHSGraph] MINUSQUESTION
			{
				BaseNode dummy = env.GetDummyNodeDecl(context, directlyNestingLHSGraph);
				res = new ConnectionNode(dummy, edge, dummy,
						ConnectionKind.ARBITRARY, ConnectionNode.NO_REDIRECTION);
				if(connections != null)
					connections.AddChild(res);
			}
		  ( defGraphElementInitialization[namer, context, edge] )? 
		| v=defVarDeclToBeYieldedTo[evals, namer, context, directlyNestingLHSGraph]
			{
				res = v;
				if(defVariablesToBeYieldedTo != null)
					defVariablesToBeYieldedTo.AddChild(v);
			}
		| node=defNodeToBeYieldedTo[context, directlyNestingLHSGraph]
			{
				res = new SingleNodeConnNode(node);
				if(connections != null)
					connections.AddChild(res);
			}
		  ( defGraphElementInitialization[namer, context, node] )? 
		)
	;

defNodeToBeYieldedTo [ int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ NodeDeclNode res = null ]
	: id=entIdentDecl COLON type=typeIdentUse
		{ res = new NodeDeclNode(id, type, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph, false, true); }
	;
	
defEdgeToBeYieldedTo [ int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	: id=entIdentDecl COLON type=typeIdentUse
		{ res = new EdgeDeclNode(id, type, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph, false, true); }
	;

defGraphElementInitialization [ AnonymousScopeNamer namer, int context, ConstraintDeclNode graphElement ]
	: a=ASSIGN e=expr[namer, context, false]
		{
			if((context & BaseNode.CONTEXT_COMPUTATION) != BaseNode.CONTEXT_COMPUTATION) {
				reportError(getCoords(a), "A def node/edge can only be initialized in a function (attempted on " + graphElement.Ident + ").");
			} else {
				if(graphElement != null)
					graphElement.Initialization = e;
			}
		}
	;

defVarDeclToBeYieldedTo [ CollectNode<EvalStatementsNode> evals,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ VarDeclNode res = ParserEnvironment.InitVarNode(directlyNestingLHSGraph, context) ]
	@init {
		EvalStatementsNode curEval = null;
		VarDeclNode var = null;
	}
	: modifier=IDENT id=entIdentDecl COLON
		(
			type=typeIdentUse
			{
				var = new VarDeclNode(id, type, directlyNestingLHSGraph, context, true, false, modifier.Text);
			}
		|
			containerType=containerTypeUse
			{
				var = new VarDeclNode(id, containerType, directlyNestingLHSGraph, context, true, false, modifier.Text);
			}
		|
			matchTypeIdent=matchTypeIdentUse
			{
				var = new VarDeclNode(id, matchTypeIdent, directlyNestingLHSGraph, context, true, false, modifier.Text);
			}
		)
		{
			res = var;
		}
		(ASSIGN e=expr[namer, context, false]
			{
				var.Initialization = e;
			}
		)?
		(a=ASSIGN y=YIELD LPAREN e=expr[namer, context, false]
			{
				if(evals != null) {
					curEval = new EvalStatementsNode(getCoords(y), "initialization_of_" + id.ToString());
					evals.AddChild(curEval);
					IdentNode varIdent = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, id.ToString(), id.Coords));
					curEval.AddChild(new AssignNode(getCoords(a), new IdentExprNode(varIdent, true), e, context, true));
				} else {
					reportError(getCoords(y), "A yield expression can only appear in the pattern after the initialization of a def variable (not in the rewrite part).");
				}
			}
			RPAREN
		)?
	;

iteratedFiltering [ CollectNode<EvalStatementsNode> evals, AnonymousScopeNamer namer, int context ]
	@init {
		CollectNode<FilterInvocationBaseNode> filters = new CollectNode<FilterInvocationBaseNode>();
	}
	: i=ITERATED ident=iterIdentUse ( filter=filterUse[ident, namer, context] { filters.AddChild(filter); } )+ SEMI
		{
			EvalStatementsNode curEval = new EvalStatementsNode(getCoords(i), "iterated_" + ident.ToString() + "_filter_call");
			evals.AddChild(curEval);
			curEval.AddChild(new IteratedFilteringNode(env.CurrentActionOrSubpattern, ident, filters));
		}
	;

filterUse [ IdentNode iterated, AnonymousScopeNamer namer, int context ] returns [ FilterInvocationBaseNode res = null ]
	@init {
		CollectNode<ExprNode> args = new CollectNode<ExprNode>();
		String idText = null;
	}
	: BACKSLASH id=IDENT { idText = id.Text; } 
			( LT fvl=filterVariableList GT
				(idTextExt=filterExtension[idText, fvl] { idText = idTextExt; })? )?
			(initExp=initExpression[namer, context, idText] { idText = $initExp.filterText; })?
			( LBRACE { namer.DefExprBlock(/*TODO:id, id.Coords*/null, getCoords(id)); } { env.PushScope(namer.ExprBlock()); }
				lambdaExprVar=lambdaExprVarDeclPrefix[namer, context] e=expr[namer, context, false]
				{ env.PopScope(); } { namer.UndefExprBlock(); } RBRACE )?
			(LPAREN arguments[args, namer, context] RPAREN)?
		{
			if(fvl != null)
			{
				String fullName = idText + "<" + join("_", fvl) + ">";
				if(args.Size() != 0)
					reportError(getCoords(id), "The filter " + fullName + " expects 0 arguments (given are " + args.Size() + ").");

				if(idText.Equals("assign")) {
					res = new FilterInvocationLambdaExpressionNode(iterated, getCoords(id), idText, fvl.get(0),
						$lambdaExprVar.va, $lambdaExprVar.vi, $lambdaExprVar.vd, e);
				} else if(idText.Equals("assignStartWithAccumulateBy")) {
					res = new FilterInvocationLambdaExpressionNode(iterated, getCoords(id), idText, fvl.get(0),
						$initExp.va, $initExp.expr,
						$lambdaExprVar.va, $lambdaExprVar.vp, $lambdaExprVar.vi, $lambdaExprVar.vd, e);
				} else {
					if(!ParserEnvironment.IsAutoGeneratedBaseFilterName(idText))
						reportError(getCoords(id), "Unknown def-variable-based filter " + idText + ". Available are: orderAscendingBy, orderDescendingBy, groupBy, keepSameAsFirst, keepSameAsLast, keepOneForEach, keepOneForEachAccumulateBy.");
					IdentNode filterAutoGen = new IdentNode(env.Occurs(ParserEnvironment.ACTIONS, fullName, getCoords(id)));
					res = new FilterInvocationNode(iterated, filterAutoGen, args);
				}
			}
			else if(idText.Equals("auto"))
			{
				if(args.Size() != 0)
					reportError(getCoords(id), "The filter " + idText + " expects 0 arguments (given are " + args.Size() + ").");
				reportError(getCoords(id), "The filter " + idText + " is not supported for iterateds.");
				/*IdentNode filterAutoGen = new IdentNode(env.Occurs(ParserEnvironment.ACTIONS, fullName, getCoords(id)));
				res = new FilterInvocationNode(iterated, filterAutoGen, args);*/
			}
			else if(idText.Equals("removeIf"))
			{
				res = new FilterInvocationLambdaExpressionNode(iterated, getCoords(id), idText, null,
					$lambdaExprVar.va, $lambdaExprVar.vi, $lambdaExprVar.vd, e);
			}
			else if(ParserEnvironment.IsAutoSuppliedFilterName(idText))
			{
				if(args.Size() != 1)
					reportError(getCoords(id), "The filter " + idText + " expects 1 arguments (given are " + args.Size() + ").");
				IdentNode filterAutoSup = new IdentNode(env.Occurs(ParserEnvironment.ACTIONS, idText, getCoords(id)));
				res = new FilterInvocationNode(iterated, filterAutoSup, args);
			}
			else
			{
				reportError(getCoords(id), "Unknown filter " + idText + ". Available are the auto-supplied and auto-generated ones.");
			}
		}
	;

initExpression [ AnonymousScopeNamer namer, int context, String filterBaseText ] returns [ String filterText = null, VarDeclNode va  = null, ExprNode expr = null ]
	: filterBaseExtension=IDENT
		l=LBRACE { env.PushScope("filterassign/initexpr", getCoords(l)); } 
		initExp=initExprVarDeclPrefix[namer, context]
		{ env.PopScope(); } RBRACE
		filterBaseExtension2=IDENT
		{
			$filterText = filterBaseText + filterBaseExtension.Text + filterBaseExtension2.Text;
			$expr = $initExp.expr;
			$va = $initExp.va;
		}
	;

initExprVarDeclPrefix [ AnonymousScopeNamer namer, int context ] returns [ VarDeclNode va = null, ExprNode expr = null ]
	options { k = *; }
	: arrayAccessVar=entIdentDecl COLON containerType=containerTypeUse SEMI e=expr[namer, context, false]
		{ $va = new VarDeclNode(arrayAccessVar, containerType, PatternGraphLhsNode.Invalid, context, true, true, "ref"); $expr = e; }
	| e=expr[namer, context, false]
		{ $va = null; $expr = e; }
	;

containerTypeUse returns [ ContainerTypeNode res = null ]
	: { input.LT(1).Text.Equals("map") }?
		i=IDENT LT keyType=typeIdentUse COMMA containerType=containerTypeContinuation[i, keyType] { res = containerType; }
	| { input.LT(1).Text.Equals("set") }?
		i=IDENT LT containerType=containerTypeContinuation[i, keyType] { res = containerType; }
	| { input.LT(1).Text.Equals("array") }?
		i=IDENT LT containerType=containerTypeContinuation[i, keyType] { res = containerType; }
	| { input.LT(1).Text.Equals("deque") }?
		i=IDENT LT containerType=containerTypeContinuation[i, keyType] { res = containerType; }
	;

containerTypeContinuation [ IToken i, IdentNode keyType ] returns [ ContainerTypeNode res = null ]
	: valueType=typeIdentUse GT
		{
			if(i.Text.Equals("map"))
				res = new MapTypeNode(keyType, valueType);
			else if(i.Text.Equals("set"))
				res = new SetTypeNode(valueType);
			else if(i.Text.Equals("array"))
				res = new ArrayTypeNode(valueType);
			else if(i.Text.Equals("deque"))
				res = new DequeTypeNode(valueType);
		}
	| valueType=matchTypeIdentUseInContainerType (GT GT | SR)
		{
			if(i.Text.Equals("map"))
				res = new MapTypeNode(keyType, valueType);
			else if(i.Text.Equals("set"))
				res = new SetTypeNode(valueType);
			else if(i.Text.Equals("array"))
				res = new ArrayTypeNode(valueType);
			else if(i.Text.Equals("deque"))
				res = new DequeTypeNode(valueType);
		}
	;

matchTypeIdentUse returns [ IdentNode res = null ]
	options { k = 3; }
	: MATCH LT actionIdent=actionIdentUse (DOT iterIdent=iterIdentUse)? GT
		{
			if(iterIdent == null)
				res = MatchTypeActionNode.GetMatchTypeIdentNode(env, actionIdent);
			else
				res = MatchTypeIteratedNode.GetMatchTypeIdentNode(env, actionIdent, iterIdent);
		}
	| MATCH LT CLASS matchClassIdent=typeIdentUse GT
		{ res = matchClassIdent; }
	;

matchTypeIdentUseInContainerType returns [ IdentNode res = null ]
	options { k = 3; }
	: MATCH LT actionIdent=actionIdentUse (DOT iterIdent=iterIdentUse)?
		{
			if(iterIdent == null)
				res = MatchTypeActionNode.GetMatchTypeIdentNode(env, actionIdent);
			else
				res = MatchTypeIteratedNode.GetMatchTypeIdentNode(env, actionIdent, iterIdent);
		}
	| MATCH LT CLASS matchClassIdent=typeIdentUse
		{ res = matchClassIdent; }
	;

nodeContinuation [ BaseNode edge, BaseNode node1, bool forward, Mutable<ConnectionKind> direction,
					Mutable<int> redirection, CollectNode<BaseNode> conn, 
					AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		node2 = env.GetDummyNodeDecl(context, directlyNestingLHSGraph);
	}
	: node2=nodeOcc[namer, context, directlyNestingLHSGraph] // node following - get it and build connection with it, then continue with looking for follwing edge
		{
			if(direction.Value == ConnectionKind.DIRECTED && !forward) {
				conn.AddChild(new ConnectionNode(node2, edge, node1, direction.Value, redirection.Value));
			} else {
				conn.AddChild(new ConnectionNode(node1, edge, node2, direction.Value, redirection.Value));
			}
		}
		edgeContinuation[node2, conn, namer, context, directlyNestingLHSGraph]
	|   // nothing following - build connection with edge dangeling on the right (see node2 initialization)
		{
			if(direction.Value == ConnectionKind.DIRECTED && !forward) {
				conn.AddChild(new ConnectionNode(node2, edge, node1, direction.Value, redirection.Value));
			} else {
				conn.AddChild(new ConnectionNode(node1, edge, node2, direction.Value, redirection.Value));
			}
		}
	;

firstEdgeContinuation [ BaseNode node, CollectNode<BaseNode> conn, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		bool forward = true;
		Mutable<ConnectionKind> direction =
				new Mutable<ConnectionKind>(ConnectionKind.ARBITRARY);
		Mutable<int> redirection = new Mutable<int>(ConnectionNode.NO_REDIRECTION);
	}
	: // nothing following? -> one single node
		{
			if(node is IdentNode) {
				conn.AddChild(new SingleGraphEntityNode((IdentNode)node));
			} else {
				conn.AddChild(new SingleNodeConnNode(node));
			}
		}
	|   ( edge=forwardOrUndirectedEdgeOcc[namer, context, direction, redirection, directlyNestingLHSGraph] { forward=true; }
		| edge=backwardOrArbitraryDirectedEdgeOcc[namer, context, direction, redirection, directlyNestingLHSGraph] { forward=false; }
		| edge=arbitraryEdgeOcc[namer, context, directlyNestingLHSGraph] { forward=false; direction.Value = ConnectionKind.ARBITRARY;}
		)
			nodeContinuation[edge, node, forward, direction, redirection, conn, namer, context, directlyNestingLHSGraph] // continue looking for node
	;

edgeContinuation [ BaseNode node, CollectNode<BaseNode> conn, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		bool forward = true;
		Mutable<ConnectionKind> direction =
				new Mutable<ConnectionKind>(ConnectionKind.ARBITRARY);
		Mutable<int> redirection = new Mutable<int>(ConnectionNode.NO_REDIRECTION);
	}
	:   // nothing following? -> connection end reached
	|   ( edge=forwardOrUndirectedEdgeOcc[namer, context, direction, redirection, directlyNestingLHSGraph] { forward=true; }
		| edge=backwardOrArbitraryDirectedEdgeOcc[namer, context, direction, redirection, directlyNestingLHSGraph] { forward=false; }
		| edge=arbitraryEdgeOcc[namer, context, directlyNestingLHSGraph] { forward=false; direction.Value = ConnectionKind.ARBITRARY;}
		)
			nodeContinuation[edge, node, forward, direction, redirection, conn, namer, context, directlyNestingLHSGraph] // continue looking for node
	;

nodeOcc [ AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ BaseNode res = ParserEnvironment.InitNode() ]
	@init {
		id = ParserEnvironment.DummyIdent;
	}
	: e=entIdentUse { res = e; } // use of already declared node
	| id=entIdentDecl COLON co=nodeTypeContinuation[id, namer, context, directlyNestingLHSGraph] { res = co; } // node declaration
	| c=COLON { id = env.DefineAnonymousEntity("node", getCoords(c)); } // anonymous node declaration
		co=nodeTypeContinuation[id, namer, context, directlyNestingLHSGraph] { res = co; }
	| d=DOT { id = env.DefineAnonymousEntity("node", getCoords(d)); } // anonymous node declaration of type node		
		//( AT LPAREN nameAndAttributesInitializationList[n, namer, context] RPAREN )?
		{ res = new NodeDeclNode(id, env.NodeRoot, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph); }
	;

nodeTypeContinuation [ IdentNode id, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ NodeDeclNode res = null ]
	@init {
		type = env.NodeRoot;
		constr = TypeExprNode.Empty;
		curId = ParserEnvironment.DummyIdent;
		CollectNode<IdentNode> mergees = new CollectNode<IdentNode>();
		CopyKind copyKind = CopyKind.None;
	}
	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		( 
			{ res = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		| LT oldid=entIdentUse ( COMMA curId=entIdentUse { mergees.AddChild(curId); } )* GT
			{ res = new NodeTypeChangeDeclNode(id, type, context, oldid, mergees, directlyNestingLHSGraph); }
		| LBRACE nsic=nodeStorageIndexContinuation [ id, type, namer, context, directlyNestingLHSGraph ] RBRACE
			{ res = nsic; }
		)
		( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	| ( COPY { copyKind = CopyKind.Copy; } | CLONE { copyKind = CopyKind.Clone; } ) LT type=entIdentUse GT
		{ res = new NodeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	;

nodeDeclParam [ int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ BaseNode res = ParserEnvironment.InitNode() ]
	@init {
		constr = TypeExprNode.Empty;
	}
	: id=entIdentDecl COLON
		type=typeIdentUse
		( constr=typeConstraint )?
		( LT (interfaceType=typeIdentUse (PLUS maybe=NULL)?
				| maybe=NULL (PLUS interfaceType=typeIdentUse)?) GT )?
			{
				if(interfaceType == null) {
					res = new NodeDeclNode(id, type, CopyKind.None, context, constr, directlyNestingLHSGraph, maybe!=null, false);
				} else {
					res = new NodeInterfaceTypeChangeDeclNode(id, type, context, interfaceType, directlyNestingLHSGraph, maybe!=null);
				}
			}
	;

varDecl [ int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ BaseNode res = ParserEnvironment.InitNode() ]
	: paramModifier=IDENT id=entIdentDecl COLON
		(
			type=typeIdentUse
			{
				res = new VarDeclNode(id, type, directlyNestingLHSGraph, context, paramModifier.Text);
			}
		|
			containerType=containerTypeUse
			{
				res = new VarDeclNode(id, containerType, directlyNestingLHSGraph, context, paramModifier.Text);
			}
		)
	;

forwardOrUndirectedEdgeOcc [ AnonymousScopeNamer namer, int context, Mutable<ConnectionKind> direction,
		Mutable<int> redirection, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ BaseNode res = ParserEnvironment.InitNode() ]
	: (NOT { redirection.Value = ConnectionNode.REDIRECT_SOURCE; })? MINUS 
		( e1=edgeDecl[namer, context, directlyNestingLHSGraph] { res = e1; } 
		| e2=entIdentUse { res = e2; } ) 
		forwardOrUndirectedEdgeOccContinuation[direction, redirection]
	| da=DOUBLE_RARROW
		{
			IdentNode id = env.DefineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.DirectedEdgeRoot, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph);
			direction.Value = ConnectionKind.DIRECTED;
		}
		//( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	| mm=MINUSMINUS
		{
			IdentNode id = env.DefineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, env.UndirectedEdgeRoot, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph);
			direction.Value = ConnectionKind.UNDIRECTED;
		}
		//( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	;

forwardOrUndirectedEdgeOccContinuation [ Mutable<ConnectionKind> direction, Mutable<int> redirection ]
	: MINUS { direction.Value = ConnectionKind.UNDIRECTED; }
			(NOT { redirection.Value = ConnectionNode.REDIRECT_TARGET | redirection.Value; })? // redirection not allowd but semantic error is better
	| RARROW { direction.Value = ConnectionKind.DIRECTED; }
			(NOT { redirection.Value = ConnectionNode.REDIRECT_TARGET | redirection.Value; })?
	;

backwardOrArbitraryDirectedEdgeOcc [ AnonymousScopeNamer namer, int context, Mutable<ConnectionKind> direction,
		Mutable<int> redirection, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ BaseNode res = ParserEnvironment.InitNode() ]
	: (NOT { redirection.Value = ConnectionNode.REDIRECT_TARGET; })? LARROW 
		( e1=edgeDecl[namer, context, directlyNestingLHSGraph] { res = e1; }
		| e2=entIdentUse { res = e2; } )
		backwardOrArbitraryDirectedEdgeOccContinuation[ direction, redirection ]
	| da=DOUBLE_LARROW
		{
			IdentNode id = env.DefineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.DirectedEdgeRoot, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph);
			direction.Value = ConnectionKind.DIRECTED;
		}
		//( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	| lr=LRARROW
		{
			IdentNode id = env.DefineAnonymousEntity("edge", getCoords(lr));
			res = new EdgeDeclNode(id, env.DirectedEdgeRoot, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph);
			direction.Value = ConnectionKind.ARBITRARY_DIRECTED;
		}
		//( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	;

backwardOrArbitraryDirectedEdgeOccContinuation [ Mutable<ConnectionKind> direction, Mutable<int> redirection ]
	: MINUS { direction.Value = ConnectionKind.DIRECTED; }
			(NOT { redirection.Value = ConnectionNode.REDIRECT_SOURCE | redirection.Value; })?
	| RARROW { direction.Value = ConnectionKind.ARBITRARY_DIRECTED; }
			(NOT { redirection.Value = ConnectionNode.REDIRECT_SOURCE | redirection.Value; })? // redirection not allowd but semantic error is better
	;

arbitraryEdgeOcc [ AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ BaseNode res = ParserEnvironment.InitNode() ]
	: QUESTIONMINUS
		( e1=edgeDecl[namer, context, directlyNestingLHSGraph] { res = e1; }
		| e2=entIdentUse { res = e2; } )
		MINUSQUESTION
	| q=QMMQ
		{
			IdentNode id = env.DefineAnonymousEntity("edge", getCoords(q));
			res = new EdgeDeclNode(id, env.ArbitraryEdgeRoot, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph);
		}
		//( AT LPAREN nameAndAttributesInitializationList[res, context] RPAREN )?
	;

edgeDecl [ AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	@init {
		id = ParserEnvironment.DummyIdent;
	}
	:   ( id=entIdentDecl COLON
			co=edgeTypeContinuation[id, namer, context, directlyNestingLHSGraph] { res = co; } 
		| c=COLON
			{ id = env.DefineAnonymousEntity("edge", getCoords(c)); }
			co=edgeTypeContinuation[id, namer, context, directlyNestingLHSGraph] { res = co; } 
		)
	;

edgeDeclParam [ int context, PatternGraphLhsNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	@init {
		id = ParserEnvironment.DummyIdent;
		type = env.NodeRoot;
		constr = TypeExprNode.Empty;
	}
	: id=entIdentDecl COLON type=typeIdentUse
		( constr=typeConstraint )?
		( LT (interfaceType=typeIdentUse (PLUS maybe=NULL)?
				| maybe=NULL (PLUS interfaceType=typeIdentUse)?) GT )?
			{
				if( interfaceType == null ) {
					res = new EdgeDeclNode(id, type, CopyKind.None, context, constr, directlyNestingLHSGraph, maybe!=null, false);
				} else {
					res = new EdgeInterfaceTypeChangeDeclNode(id, type, context, interfaceType, directlyNestingLHSGraph, maybe!=null);
				}
			}
	;

edgeTypeContinuation [ IdentNode id, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ EdgeDeclNode res = null ]
	@init {
		type = env.NodeRoot;
		constr = TypeExprNode.Empty;
		CopyKind copyKind = CopyKind.None;
	}
	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		(
			{ res = new EdgeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		| LT oldid=entIdentUse GT
			{ res = new EdgeTypeChangeDeclNode(id, type, context, oldid, directlyNestingLHSGraph); }
		| LBRACE esic=edgeStorageIndexContinuation [ id, type, namer, context, directlyNestingLHSGraph ] RBRACE
			{ res = esic; }
		)
		( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	| ( COPY { copyKind = CopyKind.Copy; } | CLONE { copyKind = CopyKind.Clone; } ) LT type=entIdentUse GT
		{ res = new EdgeDeclNode(id, type, copyKind, context, constr, directlyNestingLHSGraph); }
		( AT LPAREN nameAndAttributesInitializationList[res, namer, context] RPAREN )?
	;

edgeStorageIndexContinuation [ IdentNode id, IdentNode type, AnonymousScopeNamer namer, int context,
		PatternGraphLhsNode directlyNestingLHSGraph ] returns [ EdgeDeclNode edgeDecl = null ]
	: (DOUBLECOLON)? oldid=entIdentUse (d=DOT attr=entIdentUse)? (LBRACK (DOUBLECOLON)? mapAccess=entIdentUse RBRACK)?
		{
			if(mapAccess == null) {
				edgeDecl = new MatchEdgeFromStorageDeclNode(id, type, context, 
					attr == null ? (BaseNode)new IdentExprNode(oldid) : (BaseNode)new QualIdentNode(getCoords(d), oldid, attr), directlyNestingLHSGraph);
			} else {
				edgeDecl = new MatchEdgeByStorageAccessDeclNode(id, type, context, 
					attr == null ? (BaseNode)new IdentExprNode(oldid) : (BaseNode)new QualIdentNode(getCoords(d), oldid, attr), new IdentExprNode(mapAccess), directlyNestingLHSGraph);
			}
		}
	| idx=indexIdentUse EQUAL e=expr[namer, context, false]
		{
			edgeDecl = new MatchEdgeByIndexAccessEqualityDeclNode(id, type, context, 
						idx, e, directlyNestingLHSGraph);
		}
	| i=IDENT LPAREN idx=indexIdentUse (os=relOS e=expr[namer, context, false] (COMMA idx2=indexIdentUse os2=relOS e2=expr[namer, context, false])?)? RPAREN
		{
			if(i.Text.Equals("ascending")) {
				edgeDecl = new MatchEdgeByIndexAccessOrderingDeclNode(id, type, context, 
						true, idx, os, e, os2, e2, directlyNestingLHSGraph);
			} else if(i.Text.Equals("descending")) {
				edgeDecl = new MatchEdgeByIndexAccessOrderingDeclNode(id, type, context, 
						false, idx, os, e, os2, e2, directlyNestingLHSGraph);
			} else
				reportError(getCoords(i), "An ordered index access must start with ascending or descending (given is " + i.Text + ").");
			if(idx2 != null && !idx.ToString().Equals(idx2.ToString()))
				reportError(idx2.Coords, "The same index must be used in an ordered index access with two constraints (given are " + idx + " and " + idx2 + ").");
		}
	| i=MULTIPLE
			{ edgeDecl = new MatchEdgeByIndexAccessMultipleDeclNode(id, type, context, directlyNestingLHSGraph); }
		LPAREN
			idx=indexIdentUse os=relOS e=expr[namer, context, false] COMMA idx2=indexIdentUse os2=relOS e2=expr[namer, context, false]
			{ 
				((MatchEdgeByIndexAccessMultipleDeclNode)edgeDecl).AddIndexAccessPart(new MatchByIndexAccessOrderingPartNode(idx, os, e, os2, e2, (MatchEdgeByIndexAccessMultipleDeclNode)edgeDecl));
				if(idx2 != null && !idx.ToString().Equals(idx2.ToString()))
					reportError(idx2.Coords, "The same index must be used with the two constraints per index of a multiple index access (given are " + idx + " and " + idx2 + ").");
			}
			edgeMultipleIndexContinuation[(MatchEdgeByIndexAccessMultipleDeclNode)edgeDecl, namer, context, directlyNestingLHSGraph]
		RPAREN
	| AT LPAREN e=expr[namer, context, false] RPAREN
		{
			edgeDecl = new MatchEdgeByNameLookupDeclNode(id, type, context, 
						e, directlyNestingLHSGraph);
		}
	| {input.LT(1).Text.Equals("unique")}? i=IDENT LBRACK e=expr[namer, context, false] RBRACK
		{
			edgeDecl = new MatchEdgeByUniqueLookupDeclNode(id, type, context,
						e, directlyNestingLHSGraph);
		}
	;

edgeMultipleIndexContinuation [ MatchEdgeByIndexAccessMultipleDeclNode edgeDecl, AnonymousScopeNamer namer, int context,
		PatternGraphLhsNode directlyNestingLHSGraph ]
	: COMMA idx=indexIdentUse os=relOS e=expr[namer, context, false] COMMA idx2=indexIdentUse os2=relOS e2=expr[namer, context, false]
		{ 
			edgeDecl.AddIndexAccessPart(new MatchByIndexAccessOrderingPartNode(idx, os, e, os2, e2, edgeDecl));
			if(idx2 != null && !idx.ToString().Equals(idx2.ToString()))
				reportError(idx2.Coords, "The same index must be used with the two constraints per index of a multiple index access (given are " + idx + " and " + idx2 + ").");
		}
		edgeMultipleIndexContinuation[edgeDecl, namer, context, directlyNestingLHSGraph]
	|
	;

nameAndAttributesInitializationList [ ConstraintDeclNode cdl, AnonymousScopeNamer namer, int context ]
	: nameOrAttributeInitialization[cdl, namer, context] ( COMMA nameOrAttributeInitialization[cdl, namer, context] )*
	;

nameOrAttributeInitialization [ ConstraintDeclNode n, AnonymousScopeNamer namer, int context ]
	: DOLLAR ASSIGN arg=expr[namer, context, false]
		{ n.AddNameOrAttributeInitialization(new NameOrAttributeInitializationNode(n, arg)); }
	| attr=memberIdentUse ASSIGN arg=expr[namer, context, false]
		{ n.AddNameOrAttributeInitialization(new NameOrAttributeInitializationNode(n, attr, arg)); }
	;

arguments [ CollectNode<ExprNode> args, AnonymousScopeNamer namer, int context ]
	: ( arg=argument[args, namer, context] ( COMMA argument[args, namer, context] )* )?
		( TRIPLEMINUS ( arg=yieldArgument[args, namer, context] ( COMMA yieldArgument[args, namer, context] )* )? )?
	;

argument [ CollectNode<ExprNode> args, AnonymousScopeNamer namer, int context ] // argument for a subpattern usage or subpattern dependent rewrite usage
	: arg=expr[namer, context, false] { args.AddChild(arg); }
 	;

yieldArgument [ CollectNode<ExprNode> args, AnonymousScopeNamer namer, int context ] // argument for a subpattern usage or subpattern dependent rewrite usage
	: y=YIELD arg=expr[namer, context, false]
		{ args.AddChild(arg); if(arg is IdentExprNode) ((IdentExprNode)arg).SetYieldedTo(); else reportError(getCoords(y), "Can only yield to an element/variable (def-ined to by yielded to)."); }
 	;

homStatement returns [ HomNode res = null ]
	: h=HOM {res = new HomNode(getCoords(h)); }
		LPAREN id=entIdentUse { res.AddChild(id); }
			( COMMA id=entIdentUse { res.AddChild(id); } )*
		RPAREN
	;

totallyHomStatement returns [ TotallyHomNode res = null ]
	: i=INDEPENDENT {res = new TotallyHomNode(getCoords(i)); }
		LPAREN id=entIdentUse { res.TotallyHom = id; } 
			(BACKSLASH entityUnaryExpr[res])?
		RPAREN
	;

entityUnaryExpr [ TotallyHomNode thn ]
	: ent=entIdentUse { thn.AddChild(ent); }
	| LPAREN te=entityAddExpr[thn] RPAREN 
	;

entityAddExpr [ TotallyHomNode thn ]
	: ent=entIdentUse { thn.AddChild(ent); }
		( p=PLUS op=entIdentUse { thn.AddChild(ent); } )*
	;
	
exactStatement returns [ ExactNode res = null ]
	: e=EXACT { res = new ExactNode(getCoords(e)); }
		LPAREN id=entIdentUse { res.AddChild(id); }
			( COMMA id=entIdentUse { res.AddChild(id); } )*
		RPAREN
	;

inducedStatement returns [ InducedNode res = null ]
	: i=INDUCED { res = new InducedNode(getCoords(i)); }
		LPAREN id=entIdentUse { res.AddChild(id); }
			( COMMA id=entIdentUse { res.AddChild(id); } )*
		RPAREN
	;

replaceBody [ Coords coords, CollectNode<BaseNode> paramz, 
		CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<EvalStatementsNode> evals, CollectNode<OrderedReplacementsNode> orderedReplacements, 
		CollectNode<BaseNode> imperativeStmts, CollectNode<ExprNode> returnz,
		AnonymousScopeNamer namer, int context, IdentNode nameOfRHS, PatternGraphLhsNode directlyNestingLHSGraph ] 
		returns [ ReplaceDeclNode res = null ]
	@init {
		CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
		CollectNode<SubpatternReplNode> subpatternRepls = new CollectNode<SubpatternReplNode>();
		PatternGraphRhsNode patternGraph = new PatternGraphRhsNode(nameOfRHS.ToString(), coords, 
			connections, paramz, subpatterns, subpatternRepls,
			orderedReplacements, returnz, imperativeStmts,
			context, directlyNestingLHSGraph);
		patternGraph.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
		patternGraph.AddEvals(evals);
		res = new ReplaceDeclNode(nameOfRHS, patternGraph);
	}
	: ( replaceStmt[coords, connections, defVariablesToBeYieldedTo, subpatterns, subpatternRepls,
				evals, namer, context, directlyNestingLHSGraph] 
		| rets[returnz, namer, context] SEMI
		)*
	;

replaceStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
		CollectNode<EvalStatementsNode> evals, AnonymousScopeNamer namer,
		int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[connections, subpatterns, subpatternRepls, namer, context, directlyNestingLHSGraph] SEMI
	| simpleEvaluation[evals, namer, context, directlyNestingLHSGraph]
	;

modifyBody [ Coords coords, CollectNode<IdentNode> dels, CollectNode<BaseNode> paramz, 
		CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<EvalStatementsNode> evals, CollectNode<OrderedReplacementsNode> orderedReplacements,
		CollectNode<BaseNode> imperativeStmts, CollectNode<ExprNode> returnz,
		AnonymousScopeNamer namer, int context, IdentNode nameOfRHS, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ ModifyDeclNode res = null ]
	@init {
		CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
		CollectNode<SubpatternReplNode> subpatternRepls = new CollectNode<SubpatternReplNode>();
		PatternGraphRhsNode patternGraph = new PatternGraphRhsNode(nameOfRHS.ToString(), coords, 
			connections, paramz, subpatterns, subpatternRepls,
			orderedReplacements, returnz, imperativeStmts,
			context, directlyNestingLHSGraph);
		patternGraph.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
		patternGraph.AddEvals(evals);
		res = new ModifyDeclNode(nameOfRHS, patternGraph, dels);
	}
	: ( modifyStmt[coords, connections, defVariablesToBeYieldedTo, subpatterns, subpatternRepls,
				evals, dels, namer, context, directlyNestingLHSGraph] 
		| rets[returnz, namer, context] SEMI
		)*
	;

modifyStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<SubpatternUsageDeclNode> subpatterns, CollectNode<SubpatternReplNode> subpatternRepls,
		CollectNode<EvalStatementsNode> evals, CollectNode<IdentNode> dels,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[connections, subpatterns, subpatternRepls, namer, context, directlyNestingLHSGraph] SEMI
	| deleteStmt[dels] SEMI
	| simpleEvaluation[evals, namer, context, directlyNestingLHSGraph]
	;

defEntitiesOrEvals [ CollectNode<BaseNode> conn, CollectNode<VarDeclNode> defVariablesToBeYieldedTo,
		CollectNode<EvalStatementsNode> evals, CollectNode<OrderedReplacementsNode> orderedReplacements,
		CollectNode<BaseNode> imperativeStmts, CollectNode<ExprNode> returnz,
		AnonymousScopeNamer namer, int context, PatternGraphRhsNode patternGraph, PatternGraphLhsNode directlyNestingLHSGraph ]
	: reportErrorOnDefEntityOrEval
	  ( TRIPLEMINUS
		( defEntityToBeYieldedTo[conn, defVariablesToBeYieldedTo, null, namer, context, directlyNestingLHSGraph] SEMI // single entity definitions to be filled by later yield assignments
		| evaluation[evals, orderedReplacements, namer, context, directlyNestingLHSGraph]
		| rets[returnz, namer, context] SEMI
		| alternativeOrIteratedOrSubpatternRewriteOrder[orderedReplacements]
		| execStmt[imperativeStmts, context, directlyNestingLHSGraph] SEMI
		| emitStmt[imperativeStmts, orderedReplacements, namer, context] SEMI
		)*
	  )?
	  { patternGraph.AddDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo); }
	  { patternGraph.AddEvals(evals); }
	;

reportErrorOnDefEntityOrEval
	: ( 
		( d=DEF
			{ reportError(getCoords(d), "A def entity declaration is only allowed in the yield part. Likely a --- separating the rewrite part from the yield part is missing."); }
		| (ro=ALTERNATIVE id=altIdentUse SEMI | ro=ITERATED id=iterIdentUse SEMI | ro=PATTERN id=entIdentUse SEMI) 
			{ reportError(getCoords(ro), "An alternative or iterated or subpattern rewrite order specification is only allowed in the yield part. Likely a --- separating the pattern part from the rewrite part is missing."); }
		| e=EXEC
			{ reportError(getCoords(e), "An exec statement is only allowed in the yield part. Likely a --- separating the rewrite part from the yield part is missing."); }
		| (e=EMIT | e=EMITDEBUG | e=EMITHERE | e=EMITHEREDEBUG)
			{ reportError(getCoords(e), "An emit statement is only allowed in the yield part. Likely a --- separating the rewrite part from the yield part is missing."); }
		)
	  )?
	;

alternative [ AnonymousScopeNamer namer, int context ] returns [ AlternativeDeclNode alt = null ]
	: a=ALTERNATIVE (name=altIdentDecl)? { namer.DefAlt(name, getCoords(a)); } { env.PushScope(namer.AltCase()); }
			{ alt = new AlternativeDeclNode(namer.Alt()); } LBRACE
		( alternativeCase[alt, namer, context] )+
		RBRACE { env.PopScope(); } { namer.UndefAlt(); }
	| a=LPAREN { namer.DefAlt(null, getCoords(a)); alt = new AlternativeDeclNode(namer.Alt()); }
		( alternativeCasePure[alt, a, namer, context] )
			( BOR alternativeCasePure[alt, a, namer, context] )*
		RPAREN { namer.UndefAlt(); }
	;

alternativeCase [ AlternativeDeclNode alt, AnonymousScopeNamer namer, int context ]
	@init {
		int mod = 0;
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<BaseNode> conn = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		RhsDeclNode rightHandSide = null;
	}
	: (name=altIdentDecl)? l=LBRACE { namer.DefAltCase(name, getCoords(l)); } { env.PushScope(namer.AltCase()); }
		left=patternBody[getCoords(l), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(),
				namer, mod, context, namer.AltCase().ToString()]
		defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evals, new CollectNode<ExprNode>(), namer, context, left]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.AltCase(), left]
				{ rightHandSide = rightReplace; }
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.AltCase(), left]
				{ rightHandSide = rightModify; }
		)?
		RBRACE { env.PopScope(); }
		{ alt.AddChild(new AlternativeCaseDeclNode(namer.AltCase(), left, rightHandSide)); namer.UndefAltCase(); }
	;

alternativeCasePure [ AlternativeDeclNode alt, IToken a, AnonymousScopeNamer namer, int context ]
	@init {
		int mod = 0;
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<BaseNode> conn = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		RhsDeclNode rightHandSide = null;
	}
	: { namer.DefAltCase(null, getCoords(a)); } { env.PushScope(namer.AltCase()); }
		left=patternBody[getCoords(a), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(),
				namer, mod, context, namer.AltCase().ToString()]
		defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evals, new CollectNode<ExprNode>(), namer, context, left]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.AltCase(), left]
				{ rightHandSide = rightReplace; }
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.AltCase(), left]
				{ rightHandSide = rightModify; }
		)?
		{ env.PopScope(); }
		{ alt.AddChild(new AlternativeCaseDeclNode(namer.AltCase(), left, rightHandSide)); namer.UndefAltCase(); }
	;

iterated [ AnonymousScopeNamer namer, int context ] returns [ IteratedDeclNode res = null ]
	@init {
		CollectNode<BaseNode> conn = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		RhsDeclNode rightHandSide = null;
		int minMatches = -1;
		int maxMatches = -1;
	}
	: ( i=ITERATED { minMatches = 0; maxMatches = 0; } 
	  | i=OPTIONAL { minMatches = 0; maxMatches = 1; }
	  | i=MULTIPLE { minMatches = 1; maxMatches = 0; }
	  )
		( name=iterIdentDecl { namer.DefIter(name, null); env.AddMatchTypeChild(MatchTypeIteratedNode.DefineMatchType(env, env.CurrentActionOrSubpattern, name)); }
		  | { namer.DefIter(null, getCoords(i)); } )
		LBRACE { env.PushScope(namer.Iter()); }
		left=patternBody[getCoords(i), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(),
				namer, 0, context, namer.Iter().ToString()]
		defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evals, new CollectNode<ExprNode>(), namer, context, left]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.Iter(), left]
				{ rightHandSide = rightReplace; }
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.Iter(), left]
				{ rightHandSide = rightModify; }
		)?
		RBRACE
		{
			if(minMatches == 0 && maxMatches == 1)
				res = new OptionalDeclNode(namer.Iter(), left, rightHandSide);
			else if(minMatches == 1 && maxMatches == 0)
				res = new MultipleDeclNode(namer.Iter(), left, rightHandSide);
			else
				res = new IteratedPureDeclNode(namer.Iter(), left, rightHandSide);
			namer.UndefIter();
		}
		filterDeclsIterated[name, res]
		{ env.PopScope(); }
	;

iteratedEBNFNotation [ AnonymousScopeNamer namer, int context ] returns [ IteratedDeclNode res = null ]
	@init {
		CollectNode<BaseNode> conn = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		RhsDeclNode rightHandSide = null;
		int minMatches = -1;
		int maxMatches = -1;
	}
	: l=LPAREN { namer.DefIter(null, getCoords(l)); } { env.PushScope(namer.Iter()); }
		left=patternBody[getCoords(i), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(),
				namer, 0, context, namer.Iter().ToString()]
		defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evals, new CollectNode<ExprNode>(), namer, context, left]
		(
			rightReplace=replacePart[new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.Iter(), left]
				{ rightHandSide = rightReplace; }
			| rightModify=modifyPart[dels, new CollectNode<BaseNode>(), namer, context|BaseNode.CONTEXT_RHS, namer.Iter(), left]
				{ rightHandSide = rightModify; }
		)?
		RPAREN { env.PopScope(); }
	  ( 
	    STAR { minMatches = 0; maxMatches = 0; } 
	  | QUESTION { minMatches = 0; maxMatches = 1; }
	  | PLUS { minMatches = 1; maxMatches = 0; }
	  | LBRACK i=NUM_INTEGER { minMatches = Int32.Parse(i.Text); }
	  	   ( COLON ( STAR { maxMatches=0; } | i=NUM_INTEGER { maxMatches = Int32.Parse(i.Text); } ) | { maxMatches = minMatches; } )
		  RBRACK
	  )
		{
			if(minMatches == 0 && maxMatches == 1)
				res = new OptionalDeclNode(namer.Iter(), left, rightHandSide);
			else if(minMatches == 1 && maxMatches == 0)
				res = new MultipleDeclNode(namer.Iter(), left, rightHandSide);
			else if(minMatches == 0 && maxMatches == 0)
				res = new IteratedPureDeclNode(namer.Iter(), left, rightHandSide);
			else 
				res = new IteratedMinMaxDeclNode(namer.Iter(), left, rightHandSide, minMatches, maxMatches);
			namer.UndefIter();
		}
	;

negative [ AnonymousScopeNamer namer, int context ] returns [ PatternGraphLhsNode res = null ]
	@init {
		CollectNode<BaseNode> conn = new CollectNode<BaseNode>();
		int mod = 0;
		bool brk = false;
	}
	: (BREAK { brk = true; })? n=NEGATIVE (name=negIdentDecl)? { namer.DefNeg(name, getCoords(n)); } 
		LBRACE { env.PushScope(namer.Neg()); }
			( (PATTERNPATH { mod = PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED; }
				| PATTERN { mod = PatternGraphLhsNode.MOD_PATTERN_LOCKED; }) SEMI
			)*
			b=patternBody[getCoords(n), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(), namer, mod,
					context|BaseNode.CONTEXT_NEGATIVE, namer.Neg().ToString()]
				{
					res = b;
					b.iterationBreaking = brk;
					b.AddDefVariablesToBeYieldedTo(new CollectNode<VarDeclNode>());
					b.AddYieldings(new CollectNode<EvalStatementsNode>());
				}
		RBRACE { env.PopScope(); namer.UndefNeg(); }
	| n=TILDE { namer.DefNeg(null, getCoords(n)); }
		LPAREN { env.PushScope(namer.Neg()); }
			( (PATTERNPATH { mod = PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED; }
				| PATTERN { mod = PatternGraphLhsNode.MOD_PATTERN_LOCKED; }) SEMI
			)*
			b=patternBody[getCoords(n), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(), namer, mod,
					context|BaseNode.CONTEXT_NEGATIVE, namer.Neg().ToString()]
				{
					res = b;
					b.AddDefVariablesToBeYieldedTo(new CollectNode<VarDeclNode>());
					b.AddYieldings(new CollectNode<EvalStatementsNode>());
				}
		RPAREN { env.PopScope(); namer.UndefNeg(); }
	;

independent [ AnonymousScopeNamer namer, int context ] returns [ PatternGraphLhsNode res = null ]
	@init {
		CollectNode<BaseNode> conn = new CollectNode<BaseNode>();
		CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
		CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
		int mod = 0;
		bool brk = false;
	}
	: (BREAK { brk = true; })? i=INDEPENDENT (name=idptIdentDecl)? { namer.DefIdpt(name, getCoords(i)); }
		LBRACE { env.PushScope(namer.Idpt()); }
			( (PATTERNPATH { mod = PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED; }
				| PATTERN { mod = PatternGraphLhsNode.MOD_PATTERN_LOCKED; }) SEMI
			)*
			b=patternBody[getCoords(i), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(), namer, mod,
					context|BaseNode.CONTEXT_INDEPENDENT, namer.Idpt().ToString()] { res = b; b.iterationBreaking = brk; } 
			defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evals, new CollectNode<ExprNode>(),
					namer, context|BaseNode.CONTEXT_INDEPENDENT, b]
		RBRACE { env.PopScope(); namer.UndefIdpt(); }
	| i=BAND { namer.DefIdpt(null, getCoords(i)); }
		LPAREN { env.PushScope(namer.Idpt()); }
			( (PATTERNPATH { mod = PatternGraphLhsNode.MOD_PATTERNPATH_LOCKED; }
				| PATTERN { mod = PatternGraphLhsNode.MOD_PATTERN_LOCKED; }) SEMI
			)*
			b=patternBody[getCoords(i), new CollectNode<BaseNode>(), conn, new CollectNode<ExprNode>(), namer, mod,
					context|BaseNode.CONTEXT_INDEPENDENT, namer.Idpt().ToString()] { res = b; } 
			defEntitiesOrYieldings[conn, defVariablesToBeYieldedTo, evals, new CollectNode<ExprNode>(),
					namer, context|BaseNode.CONTEXT_INDEPENDENT, b]
		RPAREN { env.PopScope(); namer.UndefIdpt(); }
	;

condition [ CollectNode<ExprNode> conds, AnonymousScopeNamer namer, int context ]
	: IF
		LBRACE
			( e=expr[namer, context, false] { conds.AddChild(e); } SEMI )* 
		RBRACE
	| IF LPAREN e=expr[namer, context, false] { conds.AddChild(e); } RPAREN SEMI
	;

simpleEvaluation [ CollectNode<EvalStatementsNode> evals,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		EvalStatementsNode curEval = null;
	}
	: e=EVAL
			{
				namer.DefEval(null, getCoords(e));
				curEval = new EvalStatementsNode(getCoords(e), namer.Eval().ToString());
				evals.AddChild(curEval);
			}
		LBRACE { env.PushScope(namer.Eval()); }
			( c=computation[false, true, namer, context|BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, directlyNestingLHSGraph]
				{ curEval.AddChild(c); }
			)*
		RBRACE { env.PopScope(); namer.UndefEval(); }
	;

evaluation [ CollectNode<EvalStatementsNode> evals, CollectNode<OrderedReplacementsNode> orderedReplacements,
				AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		EvalStatementsNode curEval = null;
		OrderedReplacementsNode curOrderedRepl = null;
	}
	: e=EVAL
			{
				namer.DefEval(null, getCoords(e));
				curEval = new EvalStatementsNode(getCoords(e), namer.Eval().ToString());
				evals.AddChild(curEval);
			}
		LBRACE { env.PushScope(namer.Eval()); }
			( c=computation[false, false, namer, context|BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, directlyNestingLHSGraph]
				{ curEval.AddChild(c); }
			)*
		RBRACE { env.PopScope(); namer.UndefEval(); }
	| eh=EVALHERE
			{
				namer.DefEval(null, getCoords(eh));
				curOrderedRepl = new OrderedReplacementsNode(getCoords(eh), namer.Eval().ToString());
				orderedReplacements.AddChild(curOrderedRepl);
			}
		LBRACE { env.PushScope(namer.Eval()); }
			( c=computation[false, false, namer, context|BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE, directlyNestingLHSGraph] 
				{ curOrderedRepl.AddChild(c); }
			)*
		RBRACE { env.PopScope(); namer.UndefEval(); }
	;

yielding [ CollectNode<EvalStatementsNode> evals, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
	@init {
		EvalStatementsNode curEval = null;
	}
	: y=YIELD
			{
				namer.DefYield(null, getCoords(y));
				curEval = new EvalStatementsNode(getCoords(y), namer.Yield().ToString());
				evals.AddChild(curEval);
			}
		LBRACE { env.PushScope(namer.Yield()); }
			( c=computation[true, false, namer, context|BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION, directlyNestingLHSGraph]
				{ curEval.AddChild(c); }
			)*
		RBRACE { env.PopScope(); namer.UndefYield(); }
	;
	
rets [ CollectNode<ExprNode> res, AnonymousScopeNamer namer, int context ]
	@init {
		bool multipleReturns = !res.ChildrenExact.isEmpty();
	}
	: r=RETURN
		{
			if(multipleReturns) {
				reportError(getCoords(r), "A return statement may only appear once in a rule.");
			}
			if((context & BaseNode.CONTEXT_ACTION_OR_PATTERN) == BaseNode.CONTEXT_PATTERN) {
				reportError(getCoords(r), "A return statement is only allowed in actions, not in pattern type declarations.");
			}
			res.Coords = getCoords(r);
		}
		LPAREN exp=expr[namer, context, false]
			{
				if(!multipleReturns)
					res.AddChild(exp);
			}
		( COMMA exp=expr[namer, context, false]
			{
				if(!multipleReturns)
					res.AddChild(exp);
			}
		)*
		RPAREN
	;

deleteStmt [ CollectNode<IdentNode> res ]
	: DELETE LPAREN paramListOfEntIdentUse[res] RPAREN
	;

paramListOfEntIdentUse [ CollectNode<IdentNode> res ]
	: id=entIdentUse { res.AddChild(id); } ( COMMA id=entIdentUse { res.AddChild(id); } )*
	;

alternativeOrIteratedOrSubpatternRewriteOrder [ CollectNode<OrderedReplacementsNode> orderedReplacements ]
	: a=ALTERNATIVE id=altIdentUse SEMI
		{
			OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(id.Coords, id.ToString());
			orderedReplacements.AddChild(curOrderedRepl);
			curOrderedRepl.AddChild(new AlternativeReplNode(id));
		}
	| i=ITERATED id=iterIdentUse SEMI
		{
			OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(id.Coords, id.ToString());
			orderedReplacements.AddChild(curOrderedRepl);
			curOrderedRepl.AddChild(new IteratedReplNode(id));
		}
	| p=PATTERN id=entIdentUse SEMI
		{
			OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(id.Coords, id.ToString());
			orderedReplacements.AddChild(curOrderedRepl);
			curOrderedRepl.AddChild(new SubpatternReplNode(id, new CollectNode<ExprNode>()));
		}
	;

execStmt [ CollectNode<BaseNode> imperativeStmts, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ ExecNode exec = null ]
	: e=EXEC { env.PushScope("exec_", getCoords(e)); } { exec = new ExecNode(getCoords(e)); } LPAREN sequence[exec] RPAREN
		{
			if(imperativeStmts != null)
				imperativeStmts.AddChild(exec);
		}
		{ env.PopScope(); }
	;

emitStmt [ CollectNode<BaseNode> imperativeStmts, CollectNode<OrderedReplacementsNode> orderedReplacements,
		AnonymousScopeNamer namer, int context ]
	@init {
		EmitNode emit = null;
		bool isHere = false;
		bool isDebug = false;
	}
	: (e=EMIT | e=EMITDEBUG { isDebug = true; } | e=EMITHERE { isHere = true; } | e=EMITHEREDEBUG { isHere = true; isDebug = true; })
		{ emit = new EmitNode(getCoords(e), isDebug); }
		LPAREN
			exp=expr[namer, context, false] { emit.AddChild(exp); }
			( COMMA exp=expr[namer, context, false] { emit.AddChild(exp); } )*
		RPAREN
		{ 
			if(isHere) {
				OrderedReplacementsNode curOrderedRepl = new OrderedReplacementsNode(getCoords(e), e.ToString());
				orderedReplacements.AddChild(curOrderedRepl);
				curOrderedRepl.AddChild(emit);
			} else {
				imperativeStmts.AddChild(emit);
			}
		}
	;

typeConstraint returns [ TypeExprNode constr = null ]
	: BACKSLASH te=typeUnaryExpr { constr = te; } 
	;

typeAddExpr returns [ TypeExprNode res = null ]
	: typeUse=typeIdentUse { res = new TypeConstraintNode(typeUse); }
		( t=PLUS op=typeUnaryExpr
			{ res = new TypeBinaryExprNode(getCoords(t), TypeExprNode.TypeOperator.UNION, res, op); }
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
	@init {
		CollectNode<ModelNode> modelChilds = new CollectNode<ModelNode>();
		CollectNode<IdentNode> types = new CollectNode<IdentNode>();
		CollectNode<IdentNode> packages = new CollectNode<IdentNode>();
		CollectNode<IdentNode> externalFuncs = new CollectNode<IdentNode>();
		CollectNode<IdentNode> externalProcs = new CollectNode<IdentNode>();
		CollectNode<IdentNode> indices = new CollectNode<IdentNode>();
		AnonymousScopeNamer namer = new AnonymousScopeNamer(env);
		IdentNode id = ParserEnvironment.DummyIdent;

		String modelName = Util.RemoveFileSuffix(Util.RemovePathPrefix(getFilename()), "gm");

		id = new IdentNode(env.Define(ParserEnvironment.MODELS, modelName,
			new de.unika.ipd.grgen.parser.Coords(0, 0, getFilename())));
	}
	: ( usingDecl[modelChilds] )*
		specialClasses = typeDecls[namer, types, packages, externalFuncs, externalProcs, indices] EOF
		{
			if(modelChilds.ChildrenExact.Count == 0)
				modelChilds.AddChild(env.StdModel);
			model = new ModelNode(id, packages, types, externalFuncs, externalProcs, indices, modelChilds,
				$specialClasses.isEmitClassDefined, $specialClasses.isEmitGraphClassDefined, $specialClasses.isCopyClassDefined, 
				$specialClasses.isEqualClassDefined, $specialClasses.isLowerClassDefined, $specialClasses.isGraphofDefined,
				$specialClasses.isUniqueDefined, $specialClasses.isUniqueClassDefined, $specialClasses.isUniqueIndexDefined,
				$specialClasses.areFunctionsParallel, $specialClasses.isoParallel, $specialClasses.sequencesParallel);
		}
	;

typeDecls [ AnonymousScopeNamer namer, CollectNode<IdentNode> types, CollectNode<IdentNode> packages,
		CollectNode<IdentNode> externalFuncs, CollectNode<IdentNode> externalProcs, 
		CollectNode<IdentNode> indices ]
		returns [ bool isEmitClassDefined = false, bool isEmitGraphClassDefined = false, bool isCopyClassDefined = false, 
				bool isEqualClassDefined = false, bool isLowerClassDefined = false, bool isGraphofDefined = false,
				bool isUniqueDefined = false, bool isUniqueClassDefined, bool isUniqueIndexDefined = false,
				bool areFunctionsParallel = false, int isoParallel = 0, int sequencesParallel = 0 ]
	@init {
		bool graphFound = false;
	}
	: (
		type=typeDecl[namer] { types.AddChild(type); }
	  |
		pack=packageDecl[namer] { packages.AddChild(pack); }
	  |
		externalFunctionOrProcedureDecl[externalFuncs, externalProcs]
	  |
		NODE EDGE i=IDENT SEMI
			{
				if(!i.Text.Equals("unique") && !i.Text.Equals("graph"))
					reportError(getCoords(i), "Malformed \"node edge unique;\" or \"node edge graph;\".");
				else if(i.Text.Equals("unique"))
					$isUniqueDefined = true;
				else
					$isGraphofDefined = true;
			}
	  |
		o=IDENT CLASS i=IDENT SEMI
			{
				if(!o.Text.Equals("object") && !i.Text.Equals("unique"))
					reportError(getCoords(i), "Malformed \"object class unique;\".");
				else
					$isUniqueClassDefined = true;
			}
	  |
		EXTERNAL EMIT (i=IDENT
				{
					if(!i.Text.Equals("graph"))
						reportError(getCoords(i), "Malformed \"external emit graph class;\".");
					else
						graphFound = true;
				}
			)? c=CLASS SEMI
			{
				if(graphFound)
					$isEmitGraphClassDefined = true;
				else
					$isEmitClassDefined = true;
			}
	  |
		EXTERNAL COPY c=CLASS SEMI { $isCopyClassDefined = true; }
	  |
		EXTERNAL e=EQUAL c=CLASS SEMI { reportWarning(getCoords(e), "external == class; declaration is deprecated, use external ~~ class; instead. Beware: == and != compare objects by reference identity now, use ~~ (and !(a~~b)) for structural equality/value comparison."); }
	  |
		EXTERNAL STRUCTURAL_EQUAL c=CLASS SEMI { $isEqualClassDefined = true; }
	  |
		EXTERNAL LT c=CLASS SEMI { $isLowerClassDefined = true; }
	  |
		res = indexDecl[indices] { $isUniqueIndexDefined = res; }
	  |
		FOR i=IDENT LBRACK j=IDENT ASSIGN con=constant RBRACK
			{
				if(!i.Text.Equals("equalsAny"))
					reportError(getCoords(i), "Malformed \"for equalsAny[parallelize=k];\".");
				else if(!j.Text.Equals("parallelize"))
					reportError(getCoords(j), "Malformed \"for equalsAny[parallelize=k];\".");
				else {
					Object icon = ((ConstNode) con).Value;
					if(!(icon is Int32))
						reportError(getCoords(i), "\"for equalsAny[parallelize=k];\" requires an integer constant.");
					else
						$isoParallel = (Int32)icon;
				}
			}
			SEMI
	  |
		FOR i=FUNCTION LBRACK j=IDENT ASSIGN con=constant RBRACK
			{
				if(!j.Text.Equals("parallelize"))
					reportError(getCoords(j), "Malformed \"for function[parallelize=true];\".");
				else {
					Object bcon = ((ConstNode) con).Value;
					if(!(bcon is Boolean))
						reportError(getCoords(i), "\"for function[parallelize=true];\" requires a bool constant.");
					else
						$areFunctionsParallel = (Boolean)bcon;
				}
			}
			SEMI
	  |
		FOR s=SEQUENCE LBRACK j=IDENT ASSIGN con=constant RBRACK
			{
				if(!j.Text.Equals("parallelize"))
					reportError(getCoords(j), "Malformed \"for sequence[parallelize=k];\".");
				else {
					Object icon = ((ConstNode) con).Value;
					if(!(icon is Int32))
						reportError(getCoords(i), "\"for sequence[parallelize=k];\" requires an integer constant.");
					else
						$sequencesParallel = (Int32)icon;
				}
			}
			SEMI
	  )*
	;

indexDecl [ CollectNode<IdentNode> indices ] returns [ bool res = false ]
	options { k = 3; }
	: INDEX id=indexIdentDecl LBRACE indexDeclBody[id] RBRACE
		{ indices.AddChild(id); }
	| INDEX i=IDENT SEMI
		{ 
			if(i.Text.Equals("unique"))
				res = true;
			else
				reportError(getCoords(i), "Only unique allowed for an index declaration without body, not " + i.Text + ".");
		}
	;

indexDeclBody [ IdentNode id ]
	: type=typeIdentUse DOT member=memberIdentUse
		{ id.Decl = new AttributeIndexDeclNode(id, type, member); }
	| i=IDENT LPAREN startNodeType=typeIdentUse (COMMA incidentEdgeType=typeIdentUse (COMMA adjacentNodeType=typeIdentUse)?)? RPAREN 
		{ id.Decl = new IncidenceCountIndexDeclNode(id, i.Text, startNodeType, incidentEdgeType, adjacentNodeType, env); }
	;

externalFunctionOrProcedureDecl [ CollectNode<IdentNode> externalFuncs, CollectNode<IdentNode> externalProcs ]
	@init {
		CollectNode<BaseNode> returnTypes = new CollectNode<BaseNode>();
	}
	: EXTERNAL f=FUNCTION id=funcOrExtFuncIdentDecl paramz=paramTypes COLON ret=returnType SEMI
		{
			id.Decl = new ExternalFunctionDeclNode(id, paramz, ret, false);
			externalFuncs.AddChild(id);
		}
	| EXTERNAL p=PROCEDURE id=funcOrExtFuncIdentDecl paramz=paramTypes (COLON LPAREN (returnTypeList[returnTypes])? RPAREN)? SEMI
		{
			id.Decl = new ExternalProcedureDeclNode(id, paramz, returnTypes, false);
			externalProcs.AddChild(id);
		}
	;

paramTypes returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN (returnTypeList[res])? RPAREN // we reuse the return type list cause it's of format we need
	;

typeDecl [ AnonymousScopeNamer namer ] returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: d=classDecl[namer] { res = d; } 
	| d=enumDecl { res = d; } 
	| d=extClassDecl { res = d; }
	;

packageDecl [ AnonymousScopeNamer namer ] returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	@init {
		CollectNode<IdentNode> types = new CollectNode<IdentNode>(); 
	}
	: PACKAGE id=packageIdentDecl LBRACE { env.PushScope(id); }
		( type=typeDecl[namer] { types.AddChild(type); }
		)*
	  RBRACE
		{
			PackageTypeNode pt = new PackageTypeNode(types);
			id.Decl = new TypeDeclNode(id, pt);
			res = id;
		}
		{ env.PopScope(); }
	;
	
classDecl [ AnonymousScopeNamer namer ] returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	@init {
		mods = 0;
	}
	: (mods=typeModifiers)?
		( d=edgeClassDecl[namer, mods] { res = d; } 
		| d=nodeClassDecl[namer, mods] { res = d; }
		| d=objectClassDecl[namer, mods] { res = d; }
		| d=transientObjectClassDecl[namer, mods] { res = d; } )
	;

typeModifiers returns [ int res = 0; ]
	@init {
		mod = 0;
	}
	: ( mod=typeModifier { res |= mod; } )+
	;

typeModifier returns [ int res = 0; ]
	: ABSTRACT { res |= InheritanceTypeNode.MOD_ABSTRACT; }
	| CONST { res |= InheritanceTypeNode.MOD_CONST; }
	;

/**
 * An edge class decl makes a new type decl node with the declaring id and
 * a new edge type node as children
 */
edgeClassDecl [ AnonymousScopeNamer namer, int modifiers ] returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	@init {
		bool arbitrary = false;
		bool undirected = false;
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
		ext=edgeExtends[id, arbitrary, undirected] cas=connectAssertions { env.PushScope(id); }
		(
			LBRACE body=classBody[namer, id, InheritanceTypeKind.EDGE] RBRACE
		|	SEMI
			{ body = new CollectNode<BaseNode>(); }
		)
		{
			EdgeTypeNode et;
			if(arbitrary) {
				et = new ArbitraryEdgeTypeNode(ext, cas, body, modifiers, externalName);
			} else {
				if(undirected) {
					et = new UndirectedEdgeTypeNode(ext, cas, body, modifiers, externalName);
				} else {
					et = new DirectedEdgeTypeNode(ext, cas, body, modifiers, externalName);
				}
			}
			id.Decl = new TypeDeclNode(id, et);
			res = id;
		}
		{ env.PopScope(); }
  ;

nodeClassDecl [ AnonymousScopeNamer namer, int modifiers ] returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: NODE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
		ext=nodeExtends[id] { env.PushScope(id); }
		(
			LBRACE body=classBody[namer, id, InheritanceTypeKind.NODE] RBRACE
		|
			SEMI { body = new CollectNode<BaseNode>(); }
		)
		{
			NodeTypeNode nt = new NodeTypeNode(ext, body, modifiers, externalName);
			id.Decl = new TypeDeclNode(id, nt);
			res = id;
		}
		{ env.PopScope(); }
	;

objectClassDecl [ AnonymousScopeNamer namer, int modifiers ] returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: CLASS id=typeIdentDecl ext=objectExtends[id] { env.PushScope(id); }
		(
			LBRACE body=classBody[namer, id, InheritanceTypeKind.CLASS] RBRACE
		|
			SEMI { body = new CollectNode<BaseNode>(); }
		)
		{
			InternalObjectTypeNode iot = new InternalObjectTypeNode(ext, body, modifiers);
			id.Decl = new TypeDeclNode(id, iot);
			res = id;
		}
		{ env.PopScope(); }
	;

transientObjectClassDecl [ AnonymousScopeNamer namer, int modifiers ] returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: TRANSIENT CLASS id=typeIdentDecl ext=transientObjectExtends[id] { env.PushScope(id); }
		(
			LBRACE body=classBody[namer, id, InheritanceTypeKind.TRANSIENT_CLASS] RBRACE
		|
			SEMI { body = new CollectNode<BaseNode>(); }
		)
		{
			InternalTransientObjectTypeNode itot = new InternalTransientObjectTypeNode(ext, body, modifiers);
			id.Decl = new TypeDeclNode(id, itot);
			res = id;
		}
		{ env.PopScope(); }
	;

validIdent returns [ String id = "" ]
	:	i=~GT
		{
			if(i.Type != IDENT && !env.IsLexerKeyword(i.Text))
				reportError(getCoords(i), i.Text + " is is not a valid identifier.");
			id = i.Text;
		}
	;

fullQualIdent returns [ String id = "" ]
	:	i=validIdent { id = i; } 
	 	( DOT id2=validIdent { id += "." + id2; } )*
	;

connectAssertions returns [ CollectNode<ConnAssertNode> c = new CollectNode<ConnAssertNode>() ]
	: CONNECT connectAssertion[c]
		( COMMA connectAssertion[c] )*
	|
	;

connectAssertion [ CollectNode<ConnAssertNode> c ]
	options { k = *; }
	: src=typeIdentUse srcRange=rangeSpec DOUBLE_RARROW tgt=typeIdentUse tgtRange=rangeSpec
		{ c.AddChild(new ConnAssertNode(src, srcRange, tgt, tgtRange, false)); }
	| src=typeIdentUse srcRange=rangeSpec DOUBLE_LARROW tgt=typeIdentUse tgtRange=rangeSpec
		{ c.AddChild(new ConnAssertNode(tgt, tgtRange, src, srcRange, false)); }
	| src=typeIdentUse srcRange=rangeSpec QMMQ tgt=typeIdentUse tgtRange=rangeSpec
		{ c.AddChild(new ConnAssertNode(src, srcRange, tgt, tgtRange, true)); }
	| src=typeIdentUse srcRange=rangeSpec MINUSMINUS tgt=typeIdentUse tgtRange=rangeSpec
		{ c.AddChild(new ConnAssertNode(src, srcRange, tgt, tgtRange, true)); }
	| co=COPY EXTENDS
		{ c.AddChild(new ConnAssertNode(getCoords(co))); }
	;

edgeExtends [IdentNode clsId, bool arbitrary, bool undirected]
		returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS edgeExtendsCont[clsId, c, undirected]
	|	{
			if(arbitrary) {
				c.AddChild(env.ArbitraryEdgeRoot);
			} else {
				if(undirected) {
					c.AddChild(env.UndirectedEdgeRoot);
				} else {
					c.AddChild(env.DirectedEdgeRoot);
				}
			}
		}
	;

edgeExtendsCont [ IdentNode clsId, CollectNode<IdentNode> c, bool undirected ]
	: e=typeIdentUse
		{
			if(!e.ToString().Equals(clsId.ToString()))
				c.AddChild(e);
			else
				reportError(e.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	( COMMA e=typeIdentUse
		{
			if(!e.ToString().Equals(clsId.ToString()))
				c.AddChild(e);
			else
				reportError(e.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	)*
		{
			if(c.ChildrenExact.Count == 0) {
				if(undirected) {
					c.AddChild(env.UndirectedEdgeRoot);
				} else {
					c.AddChild(env.DirectedEdgeRoot);
				}
			}
		}
	;

nodeExtends [ IdentNode clsId ] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS nodeExtendsCont[clsId, c]
	|	{ c.AddChild(env.NodeRoot); }
	;

nodeExtendsCont [ IdentNode clsId, CollectNode<IdentNode> c ]
	: t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	( COMMA t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	)*
		{
			if(c.ChildrenExact.Count == 0)
				c.AddChild(env.NodeRoot);
		}
	;

objectExtends [ IdentNode clsId ] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS objectExtendsCont[clsId, c]
	|	{ c.AddChild(env.InternalObjectRoot); }
	;

objectExtendsCont [ IdentNode clsId, CollectNode<IdentNode> c ]
	: t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	( COMMA t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	)*
		{
			if(c.ChildrenExact.Count == 0)
				c.AddChild(env.InternalObjectRoot);
		}
	;

transientObjectExtends [ IdentNode clsId ] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS objectExtendsCont[clsId, c]
	|	{ c.AddChild(env.InternalTransientObjectRoot); }
	;

transientObjectExtendsCont [ IdentNode clsId, CollectNode<IdentNode> c ]
	: t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	( COMMA t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	)*
		{
			if(c.ChildrenExact.Count == 0)
				c.AddChild(env.InternalTransientObjectRoot);
		}
	;

classBody [ AnonymousScopeNamer namer, IdentNode clsId, InheritanceTypeKind kind ] returns [ CollectNode<BaseNode> c = new CollectNode<BaseNode>() ]
	:	(
			(
				basicAndContainerDecl[namer, c] SEMI
			|
				funcMethod=inClassFunctionDecl[clsId, kind] { c.AddChild(funcMethod); }
			|
				procMethod=inClassProcedureDecl[clsId, kind] { c.AddChild(procMethod); }
			|
				init=initExpr[namer] { c.AddChild(init); } SEMI
			|
				constr=constrDecl[namer, clsId] { c.AddChild(constr); } SEMI
			)
		)*
	;

enumDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	@init {
		CollectNode<EnumItemDeclNode> c = new CollectNode<EnumItemDeclNode>();
	}
	: ENUM id=typeIdentDecl { env.PushScope(id); }
		LBRACE enumList[id, c]
		{
			TypeNode enumType = new EnumTypeNode(c);
			id.Decl = new TypeDeclNode(id, enumType);
			res = id;
		}
		RBRACE { env.PopScope(); }
	;

enumList [ IdentNode enumType, CollectNode<EnumItemDeclNode> collect ]
	@init {
		int pos = 0;
	}
	: init=enumItemDecl[enumType, collect, env.Zero, pos++]
		( COMMA init=enumItemDecl[enumType, collect, init, pos++] )*
	;

enumItemDecl [ IdentNode type, CollectNode<EnumItemDeclNode> coll, ExprNode defInit, int pos ]
		returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		ExprNode value;
	}
	: id=entIdentDecl ( ASSIGN init=expr[null, 0, true] )? //'true' means that expr initializes an enum item
		{
			if(init != null) {
				value = init;
			} else {
				value = defInit;
			}
			EnumItemDeclNode memberDecl = new EnumItemDeclNode(id, type, value, pos);
			id.Decl = memberDecl;
			coll.AddChild(memberDecl);
			OperatorNode add = new ArithmeticOperatorNode(id.Coords, Operator.ADD);
			add.AddChild(value);
			add.AddChild(env.One);
			res = add;
		}
	;

extClassDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: EXTERNAL c=CLASS id=typeIdentDecl 
	  ext=extExtends[id] { env.PushScope(id); }
		(
			LBRACE body=extClassBody[id] RBRACE
		|
			SEMI { body = new CollectNode<BaseNode>(); }
		)
		{
			ExternalObjectTypeNode et = new ExternalObjectTypeNode(ext, body);
			id.Decl = new TypeDeclNode(id, et);
			res = id;
		}
		{ env.PopScope(); }
	;

extExtends [ IdentNode clsId ] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: (EXTENDS extExtendsCont[clsId, c])?
	;

extExtendsCont [ IdentNode clsId, CollectNode<IdentNode> c ]
	: t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	( COMMA t=typeIdentUse
		{
			if(!t.ToString().Equals(clsId.ToString()))
				c.AddChild(t);
			else
				reportError(t.Coords, "A class is not allowed to extend itself (" + clsId.ToString() + " does so).");
		}
	)*
	;

extClassBody [ IdentNode clsId ] returns [ CollectNode<BaseNode> c = new CollectNode<BaseNode>() ]
	:	(
			(
				funcMethod=inClassExtFunctionDecl[clsId] { c.AddChild(funcMethod); }
			|
				procMethod=inClassExtProcedureDecl[clsId] { c.AddChild(procMethod); }
			)
		)*
	;

inClassExtFunctionDecl [ IdentNode clsId ] returns [ ExternalFunctionDeclNode res = null ]
	: EXTERNAL f=FUNCTION id=methodOrExtMethodIdentDecl { env.PushScope(id); } 
			paramz=paramTypes COLON retType=returnType SEMI { env.PopScope(); }
		{
			res = new ExternalFunctionDeclNode(id, paramz, retType, true);
			id.Decl = res;
		}
	;

inClassExtProcedureDecl [ IdentNode clsId ] returns [ ExternalProcedureDeclNode res = null ]
	@init {
		CollectNode<BaseNode> retTypes = new CollectNode<BaseNode>();
	}
	: EXTERNAL pr=PROCEDURE id=methodOrExtMethodIdentDecl { env.PushScope(id); }
		paramz=paramTypes (COLON LPAREN (returnTypeList[retTypes])? RPAREN)? SEMI { env.PopScope(); }
		{
			res = new ExternalProcedureDeclNode(id, paramz, retTypes, true);
			id.Decl = res;
		}
	;
	
basicAndContainerDecl [ AnonymousScopeNamer namer, CollectNode<BaseNode> c ]
	@init {
		id = ParserEnvironment.DummyIdent;
		bool isConst = false;
	}
	: ABSTRACT ( CONST { isConst = true; } )? id=entIdentDecl
		{
			MemberDeclNode decl = new AbstractMemberDeclNode(id, isConst);
			c.AddChild(decl);
		}
	| ( CONST { isConst = true; } )? id=entIdentDecl COLON 
		(
			basicDecl[namer, id, isConst, c]
		|
			mapDecl[namer, id, isConst, c]
		|
			setDecl[namer, id, isConst, c]
		|
			arrayDecl[namer, id, isConst, c]
		|
			dequeDecl[namer, id, isConst, c]
		)
	;

basicDecl [ AnonymousScopeNamer namer, IdentNode id, bool isConst, CollectNode<BaseNode> c ]
	@init {
		MemberDeclNode decl = null;
	}
	: type=typeIdentUse
		{
			decl = new MemberDeclNode(id, type, isConst);
			id.Decl = decl;
			c.AddChild(decl);
		}
		(
			init=initExprDecl[namer, decl.Ident]
				{
					c.AddChild(init);
					if(isConst)
						decl.ConstInitializer = init;
				}
		)?
	;

mapDecl [ AnonymousScopeNamer namer, IdentNode id, bool isConst, CollectNode<BaseNode> c ]
	@init {
		MemberDeclNode decl = null;
	}
	: { input.LT(1).Text.Equals("map") }?
		IDENT LT keyType=typeIdentUse COMMA valueType=typeIdentUse
			{
				decl = new MemberDeclNode(id, new MapTypeNode(keyType, valueType), isConst);
				id.Decl = decl;
				c.AddChild(decl);
			}
		(
			GT
		|
			(GT ASSIGN | GE) init=initMapExpr[namer, 0, decl.Ident, null]
				{
					c.AddChild(init);
					if(isConst)
						decl.ConstInitializer = init;
				}
		)
	;

setDecl [ AnonymousScopeNamer namer, IdentNode id, bool isConst, CollectNode<BaseNode> c ]
	@init {
		MemberDeclNode decl = null;
	}
	: { input.LT(1).Text.Equals("set") }?
		IDENT LT valueType=typeIdentUse
			{
				decl = new MemberDeclNode(id, new SetTypeNode(valueType), isConst);
				id.Decl = decl;
				c.AddChild(decl);
			}
		(
			GT
		|
			(GT ASSIGN | GE) init=initSetExpr[namer, 0, decl.Ident, null]
				{
					c.AddChild(init);
					if(isConst)
						decl.ConstInitializer = init;
				}
		)
	;

arrayDecl [ AnonymousScopeNamer namer, IdentNode id, bool isConst, CollectNode<BaseNode> c ]
	@init {
		MemberDeclNode decl = null;
	}
	: { input.LT(1).Text.Equals("array") }?
		IDENT LT valueType=typeIdentUse
			{
				decl = new MemberDeclNode(id, new ArrayTypeNode(valueType), isConst);
				id.Decl = decl;
				c.AddChild(decl);
			}
		(
			GT
		|
			(GT ASSIGN | GE) init=initArrayExpr[namer, 0, decl.Ident, null]
				{
					c.AddChild(init);
					if(isConst)
						decl.ConstInitializer = init;
				}
		)
	;

dequeDecl [ AnonymousScopeNamer namer, IdentNode id, bool isConst, CollectNode<BaseNode> c ]
	@init {
		MemberDeclNode decl = null;
	}
	: { input.LT(1).Text.Equals("deque") }?
		IDENT LT valueType=typeIdentUse
			{
				decl = new MemberDeclNode(id, new DequeTypeNode(valueType), isConst);
				id.Decl = decl;
				c.AddChild(decl);
			}
		(
			GT
		|
			(GT ASSIGN | GE) init=initDequeExpr[namer, 0, decl.Ident, null]
				{
					c.AddChild(init);
					if(isConst)
						decl.ConstInitializer = init;
				}
		)
	;

inClassFunctionDecl [ IdentNode clsId, InheritanceTypeKind kind ] returns [ FunctionDeclNode res = null ]
	@init {
		CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>();
		AnonymousScopeNamer namer = new AnonymousScopeNamer(env);
	}
	: f=FUNCTION id=methodOrExtMethodIdentDecl { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, PatternGraphLhsNode.Invalid]
		COLON retType=returnType
		LBRACE
			{
				if(kind == InheritanceTypeKind.CLASS) {
					evals.AddChild(new DefDeclStatementNode(getCoords(f),
							new VarDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(f))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									PatternGraphLhsNode.Invalid, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, true, false, "ref"),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD));
				} else if(kind == InheritanceTypeKind.TRANSIENT_CLASS) {
					evals.AddChild(new DefDeclStatementNode(getCoords(f),
							new VarDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(f))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									PatternGraphLhsNode.Invalid, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, true, false, "ref"),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD));
				} else if(kind == InheritanceTypeKind.NODE) {
					evals.AddChild(new DefDeclStatementNode(getCoords(f), new SingleNodeConnNode(
							new NodeDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(f))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									CopyKind.None, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, TypeExprNode.Empty, PatternGraphLhsNode.Invalid, false, true)),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD));
				} else if(kind == InheritanceTypeKind.EDGE) {
					evals.AddChild(new DefDeclStatementNode(getCoords(f), new ConnectionNode(
							env.GetDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION, PatternGraphLhsNode.Invalid),
							new EdgeDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(f))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									CopyKind.None, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, TypeExprNode.Empty, PatternGraphLhsNode.Invalid, false, true),
							env.GetDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, PatternGraphLhsNode.Invalid), ConnectionKind.DIRECTED, ConnectionNode.NO_REDIRECTION),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD));
				}
			}
			( c=computation[false, false, namer, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_FUNCTION|BaseNode.CONTEXT_METHOD, PatternGraphLhsNode.Invalid]
				{ evals.AddChild(c); }
			)*
		RBRACE { env.PopScope(); }
		{
			res = new FunctionDeclNode(id, evals, null, paramz, retType, true);
			id.Decl = res;
		}
	;

inClassProcedureDecl [ IdentNode clsId, InheritanceTypeKind kind ] returns [ ProcedureDeclNode res = null ]
	@init {
		CollectNode<BaseNode> retTypes = new CollectNode<BaseNode>();
		CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>();
		AnonymousScopeNamer namer = new AnonymousScopeNamer(env);
	}
	: pr=PROCEDURE id=methodOrExtMethodIdentDecl { env.PushScope(id); } paramz=parameters[BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphLhsNode.Invalid]
		(COLON LPAREN (returnTypeList[retTypes])? RPAREN)?
		LBRACE
			{
				if(kind == InheritanceTypeKind.CLASS) {
					evals.AddChild(new DefDeclStatementNode(getCoords(pr),
							new VarDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(pr))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									PatternGraphLhsNode.Invalid, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, true, false, "ref"),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD));
				} else if(kind == InheritanceTypeKind.TRANSIENT_CLASS) {
					evals.AddChild(new DefDeclStatementNode(getCoords(pr),
							new VarDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(pr))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									PatternGraphLhsNode.Invalid, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, true, false, "ref"),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD));
				} else if(kind == InheritanceTypeKind.NODE) {
					evals.AddChild(new DefDeclStatementNode(getCoords(pr), new SingleNodeConnNode(
							new NodeDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(pr))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									CopyKind.None, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, TypeExprNode.Empty, PatternGraphLhsNode.Invalid, false, true)),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD));
				} else if(kind == InheritanceTypeKind.EDGE) {
					evals.AddChild(new DefDeclStatementNode(getCoords(pr), new ConnectionNode(
							env.GetDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphLhsNode.Invalid), 
							new EdgeDeclNode(new IdentNode(env.Define(ParserEnvironment.ENTITIES, "this", getCoords(pr))),
									new IdentNode(env.Occurs(ParserEnvironment.TYPES, clsId.ToString(), clsId.Coords)),
									CopyKind.None, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, TypeExprNode.Empty, PatternGraphLhsNode.Invalid, false, true),
							env.GetDummyNodeDecl(BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphLhsNode.Invalid), ConnectionKind.DIRECTED, ConnectionNode.NO_REDIRECTION),
							BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD));
				}
			}
			( c=computation[false, false, namer, BaseNode.CONTEXT_COMPUTATION|BaseNode.CONTEXT_PROCEDURE|BaseNode.CONTEXT_METHOD, PatternGraphLhsNode.Invalid]
				{ evals.AddChild(c); }
			)*
		RBRACE { env.PopScope(); }
		{
			res = new ProcedureDeclNode(id, evals, paramz, retTypes, true);
			id.Decl = res;
		}
	;

initExpr [ AnonymousScopeNamer namer ] returns [ MemberInitNode res = null ]
	: id=entIdentUse init=initExprDecl[namer, id] { res = init; }
	;

initExprDecl [ AnonymousScopeNamer namer, IdentNode id ] returns [ MemberInitNode res = null ]
	: a=ASSIGN e=expr[namer, 0, false]
		{
			res = new MemberInitNode(getCoords(a), id, e);
		}
	;

initMapExpr [ AnonymousScopeNamer namer, int context, IdentNode id, MapTypeNode mapType ] returns [ ExprNode res = null ]
	@init {
		MapInitNode mapInit = null;
	}
	: l=LBRACE { res = mapInit = new MapInitNode(getCoords(l), id, mapType); }
		( item1=keyToValue[namer, context] { mapInit.AddPairItem(item1); }
			( COMMA item2=keyToValue[namer, context] { mapInit.AddPairItem(item2); } )*
		)?
	  RBRACE
	| lp=LPAREN value=expr[namer, context, false]
		{ res = new MapCopyConstructorNode(getCoords(lp), id, mapType, value); }
	  RPAREN 
	;

initSetExpr [ AnonymousScopeNamer namer, int context, IdentNode id, SetTypeNode setType ] returns [ ExprNode res = null ]
	@init {
		SetInitNode setInit = null;
	}
	: l=LBRACE { res = setInit = new SetInitNode(getCoords(l), id, setType); }
		( initializerOfSingleElements[namer, context, setInit] )?
	  RBRACE
	| lp=LPAREN value=expr[namer, context, false]
		{ res = new SetCopyConstructorNode(getCoords(lp), id, setType, value); }
	  RPAREN 
	;

initArrayExpr [ AnonymousScopeNamer namer, int context, IdentNode id, ArrayTypeNode arrayType ] returns [ ExprNode res = null ]
	@init {
		ArrayInitNode arrayInit = null;
	}
	: l=LBRACK { res = arrayInit = new ArrayInitNode(getCoords(l), id, arrayType); }
		( initializerOfSingleElements[namer, context, arrayInit] )?
	  RBRACK
	| lp=LPAREN value=expr[namer, context, false]
		{ res = new ArrayCopyConstructorNode(getCoords(lp), id, arrayType, value); }
	  RPAREN 
	;

initDequeExpr [ AnonymousScopeNamer namer, int context, IdentNode id, DequeTypeNode dequeType ] returns [ ExprNode res = null ]
	@init {
		DequeInitNode dequeInit = null;
	}
	: l=LBRACK { res = dequeInit = new DequeInitNode(getCoords(l), id, dequeType); }
		( initializerOfSingleElements[namer, context, dequeInit] )?
	  RBRACK
	| lp=LPAREN value=expr[namer, context, false]
		{ res = new DequeCopyConstructorNode(getCoords(lp), id, dequeType, value); }
	  RPAREN 
	;

initializerOfSingleElements [ AnonymousScopeNamer namer, int context, ContainerSingleElementInitNode initNode ]
	: item1=expr[namer, context, false] { initNode.AddItem(item1); }
		( COMMA item2=expr[namer, context, false] { initNode.AddItem(item2); } )*
	;

keyToValue [ AnonymousScopeNamer namer, int context ] returns [ ExprPairNode res = null ]
	: key=expr[namer, context, false] a=RARROW value=expr[namer, context, false]
		{ res = new ExprPairNode(getCoords(a), key, value); }
	;

constrDecl [ AnonymousScopeNamer namer, IdentNode clsId ] returns [ ConstructorDeclNode res = null ]
	@init {
		CollectNode<ConstructorParamNode> paramz = new CollectNode<ConstructorParamNode>();
	}
	: id=typeIdentUse LPAREN constrParamList[namer, paramz] RPAREN
		{
			res = new ConstructorDeclNode(id, paramz);
			
			if(!id.ToString().Equals(clsId.ToString()))
				reportError(id.Coords, "A constructor must come with the name of the containing class (but " + id.ToString() + " is different from " + clsId.ToString() + ").");
		}
	;

constrParamList [ AnonymousScopeNamer namer, CollectNode<ConstructorParamNode> paramz ]
	: p=constrParam[namer] { paramz.AddChild(p); } ( COMMA p=constrParam[namer] { paramz.AddChild(p); } )*
	;

constrParam [ AnonymousScopeNamer namer ] returns [ ConstructorParamNode res = null ]
	: id=entIdentUse ( ASSIGN e=expr[namer, 0, false] )?
		{ res = new ConstructorParamNode(id, e); }
	;


////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Base  --- copied to GrGenEmbeddedExec.g as needed there, I don't know what abominations will arise if they differ
////////////////////////////////////////////////////////////////////////////////////////////////////////////////


memberIdent returns [ IToken t = null ]
	: i=IDENT { t = i; }
	| r=REPLACE { r.Type = IDENT; t = r; }             // HACK: For string replace function... better choose another name?
	; 

packageIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.PACKAGES, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

typeIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.TYPES, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

rhsIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.REPLACES, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

entIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

actionIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.ACTIONS, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
		//{ if (res.getAnnotations() is EmptyAnnotations) { res.setAnnotations(new DefaultAnnotations()); } } // uncomment to parallelize everything as far as possible, for testing
	;

altIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.ALTERNATIVES, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

iterIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.ITERATEDS, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;
	
negIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.ITERATEDS, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

idptIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.INDEPENDENTS, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

patIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.PATTERNS, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

funcOrExtFuncIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

methodOrExtMethodIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;
	
indexIdentDecl returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Define(ParserEnvironment.INDICES, i.Text, getCoords(i))); }
		( annots=annotations { res.Annotations = annots; } )?
	;

/////////////////////////////////////////////////////////
// Identifier usages, it is checked, whether the identifier is declared.
// The IdentNode created by the definition is returned.
// Don't factor the common stuff into "identUse", that pollutes the follow sets

typeIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	options { k = 3; }
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
				env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i))); }
	;

rhsIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.REPLACES, i.Text, getCoords(i))); }
	;

entIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
	;

actionIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	options { k = 3; }
	: i=IDENT
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.ACTIONS, i.Text, getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
				env.Occurs(ParserEnvironment.ACTIONS, i.Text, getCoords(i))); }
	;

altIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.ALTERNATIVES, i.Text, getCoords(i))); }
	;

iterIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.ITERATEDS, i.Text, getCoords(i))); }
	;

negIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.NEGATIVES, i.Text, getCoords(i))); }
	;

idptIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.INDEPENDENTS, i.Text, getCoords(i))); }
	;

patIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	options { k = 3; }
	: i=IDENT 
		{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.PATTERNS, i.Text, getCoords(i))); }
	| p=IDENT DOUBLECOLON i=IDENT 
		{ if(i != null) res = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)), 
				env.Occurs(ParserEnvironment.PATTERNS, i.Text, getCoords(i))); }
	;

funcOrExtFuncIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i))); }
	;

indexIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=IDENT 
	{ if(i != null) res = new IdentNode(env.Occurs(ParserEnvironment.INDICES, i.Text, getCoords(i))); }
	;

	
annotations returns [ Annotations annots = new DefaultAnnotations() ]
	: LBRACK keyValuePairs[annots] RBRACK
	;

keyValuePairs [ Annotations annots ]
	: keyValuePair[annots] ( COMMA keyValuePair[annots] )*
	;

keyValuePair [ Annotations annots ]
	: id=IDENT
		(
			ASSIGN c=constant
			{ annots.Put(id.Text, ((ConstNode) c).Value); }
		|
			{ annots.Put(id.Text, true); }
		)
	;

identList [ ICollection<String> strings ]
	: fid=IDENT { strings.Add(fid.Text); }
		( COMMA sid=IDENT { strings.Add(sid.Text); } )*
	;

memberIdentUse returns [ IdentNode res = ParserEnvironment.DummyIdent ]
	: i=memberIdent
		{ if(i!=null) res = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i))); }
	;


//////////////////////////////////////////
// Expressions
//////////////////////////////////////////


autoFunctionBody returns [ FunctionAutoNode res = null ]
	@init {
		CollectNode<IdentNode> paramz = new CollectNode<IdentNode>();
	}
	: join=IDENT LT joinFunction=IDENT GT LPAREN id=entIdentUse { paramz.AddChild(id); }
		( COMMA id=entIdentUse { paramz.AddChild(id); } )+ RPAREN
		{ res = new FunctionAutoJoinNode(getCoords(join), join.Text, joinFunction.Text, paramz); }
	| target=entIdentUse DOT keepOne=IDENT LT id=entIdentUse GT accumulate=IDENT LT accuId=entIdentUse GT by=IDENT LT accuFunction=IDENT GT 
		{ res = new FunctionAutoKeepOneForEachAccumulateByNode(getCoords(join), keepOne.Text + accumulate.Text + by.Text,
			id, accuId, accuFunction.Text, target); }
	;

computations [ bool onLHS, bool isSimple, int context, PatternGraphLhsNode directlyNestingLHSGraph ] 
		returns [ CollectNode<EvalStatementNode> evals = new CollectNode<EvalStatementNode>() ]
	@init {
		AnonymousScopeNamer namer = new AnonymousScopeNamer(env);
	}
	: ( 
		c=computation[onLHS, isSimple, namer, context, directlyNestingLHSGraph]
			{ evals.AddChild(c); }
	  )*
	;

computation [ bool onLHS, bool isSimple, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ EvalStatementNode res = null ]
	options { k = 5; }
	@init {
		CompoundAssignNode.CompoundAssignmentType cat = CompoundAssignNode.CompoundAssignmentType.NONE; // compound assign type
		CompoundAssignNode.CompoundAssignmentType ccat = CompoundAssignNode.CompoundAssignmentType.NONE; // changed compound assign type
		BaseNode tgtChanged = null;
		CollectNode<ExprNode> subpatternConn = new CollectNode<ExprNode>();
		bool yielded = false, methodCall = false, attributeMethodCall = false, packPrefix = false;
		CollectNode<ExprNode> returnValues = new CollectNode<ExprNode>();
		CollectNode<ProjectionExprNode> targetProjs = new CollectNode<ProjectionExprNode>();
		CollectNode<EvalStatementNode> targetEvals = new CollectNode<EvalStatementNode>();
		MultiStatementNode ms = new MultiStatementNode();
	}
	: (dc=DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse a=ASSIGN e=expr[namer, context, false] SEMI//'false' because this rule is not used for the assignments in enum item decls
		{
			res = new AssignNode(getCoords(a), new QualIdentNode(getCoords(d), owner, member), e, context); 
			if(onLHS)
				reportError(getCoords(d), "An assignment to an attribute is forbidden in a yield, only an yield assignment to a def variable is allowed.");
			if(isSimple && dc!=null)
				reportError(getCoords(dc), "An assignment to an attribute of a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  (y=YIELD { yielded = true; })? (dc=DOUBLECOLON)? variable=entIdentUse a=ASSIGN e=expr[namer, context, false] SEMI
		{
			res = new AssignNode(getCoords(a), new IdentExprNode(variable, yielded), e, context, onLHS);
			if(isSimple && dc!=null)
				reportError(getCoords(dc), "An assignment to a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
			if(isSimple && yielded)
				reportError(getCoords(y), "A yield assignment to a def entity is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  vis=visited[namer, context] a=ASSIGN e=expr[namer, context, false] SEMI
		{
			res = new AssignVisitedNode(getCoords(a), vis, e, context);
			if(onLHS)
				reportError(getCoords(a), "An assignment to a visited flag is forbidden in a yield.");
		}
	|
	  n=NAMEOF LPAREN (id=expr[namer, context, false])? RPAREN a=ASSIGN e=expr[namer, context, false] SEMI
		{
			res = new AssignNameofNode(getCoords(a), id, e, context);
			if(onLHS)
				reportError(getCoords(a), "A name assignment is forbidden in a yield.");
		}
	|
	  (dc=DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse LBRACK idx=expr[namer, context, false] RBRACK a=ASSIGN e=expr[namer, context, false] SEMI //'false' because this rule is not used for the assignments in enum item decls
		{
			res = new AssignIndexedNode(getCoords(a), new QualIdentNode(getCoords(d), owner, member), e, idx, context);
			if(onLHS)
				reportError(getCoords(d), "An indexed assignment to an attribute is forbidden in a yield, only a yield indexed assignment to a def variable is allowed.");
			if(isSimple && dc!=null)
				reportError(getCoords(dc), "An indexed assignment to an attribute of a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  (y=YIELD { yielded = true; })? (dc=DOUBLECOLON)? variable=entIdentUse LBRACK idx=expr[namer, context, false] RBRACK a=ASSIGN e=expr[namer, context, false] SEMI
		{
			res = new AssignIndexedNode(getCoords(a), new IdentExprNode(variable, yielded), e, idx, context, onLHS);
			if(isSimple && dc!=null)
				reportError(getCoords(dc), "An indexed assignment to a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
			if(isSimple && yielded)
				reportError(getCoords(y), "A yield indexed assignment to a def entity is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	| 
	  (dc=DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse 
		(a=BOR_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.UNION; } | a=BAND_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.INTERSECTION; }
			| a=BACKSLASH_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.WITHOUT; } | a=PLUS_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.CONCATENATE; })
		e=expr[namer, context, false] ( at=assignTo[namer, context] { ccat = $at.ccat; tgtChanged = $at.tgtChanged; } )? SEMI
		{
			res = new CompoundAssignNode(getCoords(a), new QualIdentNode(getCoords(d), owner, member), cat, e, ccat, tgtChanged);
			if(onLHS)
				reportError(getCoords(d), "A compound assignment to an attribute is forbidden in a yield, only a yield assignment to a def variable is allowed.");
			if(cat == CompoundAssignNode.CompoundAssignmentType.CONCATENATE && ccat!=CompoundAssignNode.CompoundAssignmentType.NONE)
				reportError(getCoords(d), "A change assignment is not allowed for array|deque concatenation.");
			if(isSimple && dc!=null)
				reportError(getCoords(dc), "A compound assignment to an attribute of a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  (y=YIELD { yielded = true; })? (dc=DOUBLECOLON)? variable=entIdentUse 
		(a=BOR_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.UNION; } | a=BAND_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.INTERSECTION; } 
			| a=BACKSLASH_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.WITHOUT; } | a=PLUS_ASSIGN { cat = CompoundAssignNode.CompoundAssignmentType.CONCATENATE; })
		e=expr[namer, context, false] ( at=assignTo[namer, context] { ccat = $at.ccat; tgtChanged = $at.tgtChanged; } )? SEMI
		{
			res = new CompoundAssignNode(getCoords(a), new IdentExprNode(variable, yielded), cat, e, ccat, tgtChanged);
			if(cat == CompoundAssignNode.CompoundAssignmentType.CONCATENATE && ccat!=CompoundAssignNode.CompoundAssignmentType.NONE)
				reportError(getCoords(d), "A change assignment is not allowed for array|deque concatenation.");
			if(isSimple && dc!=null)
				reportError(getCoords(dc), "A compound assignment to a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
			if(isSimple && yielded)
				reportError(getCoords(y), "A yield compound assignment to a def entity is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  de=defEntityToBeYieldedTo[null, null, null, namer, context, directlyNestingLHSGraph] SEMI
		{ res = new DefDeclStatementNode(de.Coords, de, context); }
	|
	  r=RETURN ( retValues=paramExprs[namer, context, false] { returnValues = retValues; } )? SEMI
		{
			res = new ReturnStatementNode(getCoords(r), returnValues);
			if(onLHS)
				reportError(getCoords(r), "A return statement is forbidden in a yield.");
			if(isSimple)
				reportError(getCoords(r), "A return statement is forbidden in a simple eval.");
		}
	|
	  f=FOR LPAREN { env.PushScope("for", getCoords(f)); } fc=forContent[getCoords(f), onLHS, isSimple, namer, context, directlyNestingLHSGraph]
		{
			res = fc;
			if(isSimple)
				reportError(getCoords(f), "A for loop is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  c=CONTINUE SEMI
		{
			res = new ContinueStatementNode(getCoords(c));
			if(isSimple)
				reportError(getCoords(c), "A continue statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  b=BREAK SEMI
		{
			res = new BreakStatementNode(getCoords(b));
			if(isSimple)
				reportError(getCoords(b), "A break statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  ie=ifelse[onLHS, isSimple, namer, context, directlyNestingLHSGraph]
		{
			res = ie;
			if(isSimple)
				reportError(ie.Coords, "An if statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  sc=switchcase[onLHS, isSimple, namer, context, directlyNestingLHSGraph]
		{
			res = sc;
			if(isSimple)
				reportError(sc.Coords, "A switch statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  w=WHILE LPAREN e=expr[namer, context, false] RPAREN
		LBRACE { env.PushScope("while", getCoords(w)); }
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			res = new WhileStatementNode(getCoords(w), e, cs);
			if(isSimple)
				reportError(getCoords(w), "A while statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  l=LOCK LPAREN e=expr[namer, context, false] RPAREN
		LBRACE { env.PushScope("lock", getCoords(l)); }
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			res = new LockStatementNode(getCoords(l), e, cs);
			if(isSimple)
				reportError(getCoords(l), "A lock statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  d=DO 
		LBRACE { env.PushScope("do", getCoords(d)); }
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
	  WHILE LPAREN e=expr[namer, context, false] RPAREN
		{
			res = new DoWhileStatementNode(getCoords(d), cs, e);
			if(isSimple)
				reportError(getCoords(d), "A do while statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	|
	  (l=LPAREN tgts=targets[onLHS, getCoords(l), ms, namer, context, directlyNestingLHSGraph] RPAREN a=ASSIGN { targetProjs = $tgts.tgtProjs; targetEvals = $tgts.tgts; } )? 
		( (y=YIELD { yielded = true; })? (dc=DOUBLECOLON)? variable=entIdentUse d=DOT { methodCall = true; } (member=entIdentUse DOT { attributeMethodCall = true; })? )?
		(pack=IDENT DOUBLECOLON {packPrefix=true;})? (i=IDENT | i=EMIT | i=EMITDEBUG | i=DELETE) paramz=paramExprs[namer, context, false] SEMI
			{ 
				if(!methodCall)
				{
					if(isSimple) {
						reportError(getCoords(i), "A procedure call is forbidden in a simple eval, move it to a full eval after the --- separator.");
					}
					if(ParserEnvironment.IsKnownProcedure(pack, i, paramz))
					{
						IdentNode procIdent = new IdentNode(env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
						ProcedureInvocationDecisionNode proc;
						if(packPrefix) {
							proc = new PackageProcedureInvocationDecisionNode(pack.Text, procIdent, paramz, context, env);
						} else {
							proc = new ProcedureInvocationDecisionNode(procIdent, paramz, context, env);
						}
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), proc, targetEvals, context);
						foreach(ProjectionExprNode proj in targetProjs.ChildrenExact) {
							proj.Procedure = proc;
						}
						foreach(EvalStatementNode eval in targetEvals.ChildrenExact) {
							eval.Coords = getCoords(a);
						}
						ms.AddStatement(ra);
						res = ms;
					}
					else
					{
						IdentNode procIdent;
						if(packPrefix) {
							procIdent = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, pack.Text, getCoords(pack)), 
								env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
						} else {
							procIdent = new IdentNode(env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
						}
						ProcedureOrExternalProcedureInvocationNode proc = new ProcedureOrExternalProcedureInvocationNode(procIdent, paramz, context);
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), proc, targetEvals, context);
						foreach(ProjectionExprNode proj in targetProjs.ChildrenExact) {
							proj.Procedure = proc;
						}
						foreach(EvalStatementNode eval in targetEvals.ChildrenExact) {
							eval.Coords = getCoords(a);
						}
						ms.AddStatement(ra);
						res = ms;
					}
				}
				else
				{
					IdentNode method_ = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)));
					if(!attributeMethodCall) 
					{
						if(isSimple && dc!=null) {
							reportError(getCoords(dc), "A method call on a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
						}
						if(isSimple && yielded) {
							reportError(getCoords(y), "A yield method call on a def entity is forbidden in a simple eval, move it to a full eval after the --- separator.");
						}
						ProcedureMethodInvocationDecisionNode pmi = new ProcedureMethodInvocationDecisionNode(new IdentExprNode(variable, yielded), method_, paramz, context);
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), pmi, targetEvals, context);
						foreach(ProjectionExprNode proj in targetProjs.ChildrenExact) {
							proj.Procedure = pmi;
						}
						foreach(EvalStatementNode eval in targetEvals.ChildrenExact) {
							eval.Coords = getCoords(a);
						}
						ms.AddStatement(ra);
						res = ms;
					}
					else
					{
						if(isSimple && dc!=null) {
							reportError(getCoords(dc), "A method call on an attribute of a global variable is forbidden in a simple eval, move it to a full eval after the --- separator.");
						}
						if(isSimple && yielded) {
							reportError(getCoords(y), "A yield method call on an attribute of a def entity is forbidden in a simple eval, move it to a full eval after the --- separator.");
						}
						ProcedureMethodInvocationDecisionNode pmi = new ProcedureMethodInvocationDecisionNode(new QualIdentNode(getCoords(d), variable, member), method_, paramz, context);
						if(onLHS) {
							reportError(getCoords(d), "A method call on an attribute is forbidden in a yield, only a yield method call to a def variable is allowed.");
						}
						ReturnAssignmentNode ra = new ReturnAssignmentNode(getCoords(i), pmi, targetEvals, context);
						foreach(ProjectionExprNode proj in targetProjs.ChildrenExact) {
							proj.Procedure = pmi;
						}
						foreach(EvalStatementNode eval in targetEvals.ChildrenExact) {
							eval.Coords = getCoords(a);
						}
						ms.AddStatement(ra);
						res = ms;
					}
				}
			}
	|
	  exec=execStmt[null, context, directlyNestingLHSGraph] SEMI
		{
			res = new ExecStatementNode(exec, context);
			if(onLHS)
				reportError(exec.Coords, "An exec statement is forbidden in a yield.");
			if(isSimple)
				reportError(exec.Coords, "An exec statement is forbidden in a simple eval, move it to a full eval after the --- separator.");
		}
	;

targets	[ bool onLHS, Coords coords, MultiStatementNode ms,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ CollectNode<ProjectionExprNode> tgtProjs = new CollectNode<ProjectionExprNode>(),
				CollectNode<EvalStatementNode> tgts = new CollectNode<EvalStatementNode>() ]
	@init {
		int index = 0; // index of return target in sequence of returns
		ProjectionExprNode e = null;
	}
	: ( { e = new ProjectionExprNode(coords, index); $tgtProjs.AddChild(e); } 
			tgt=assignmentTarget[onLHS, coords, e, ms, namer, context, directlyNestingLHSGraph] { $tgts.AddChild(tgt); ++index; } 
		  ( c=COMMA { e = new ProjectionExprNode(getCoords(c), index); $tgtProjs.AddChild(e); }
				tgt=assignmentTarget[onLHS, coords, e, ms, namer, context, directlyNestingLHSGraph] { $tgts.AddChild(tgt); ++index; }
		  )*
	  )?
	;

assignmentTarget [ bool onLHS, Coords coords, ProjectionExprNode e, MultiStatementNode ms,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ EvalStatementNode res = null ]
	options { k = 5; }
	@init {
		bool yielded = false;
	}
	: (DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse
		{ res = new AssignNode(coords, new QualIdentNode(getCoords(d), owner, member), e, context); }
	|
	  (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse
		{ res = new AssignNode(coords, new IdentExprNode(variable, yielded), e, context, onLHS); }
	|
	  vis=visited[namer, context]
		{ res = new AssignVisitedNode(coords, vis, e, context); }
	| 
	  (DOUBLECOLON)? owner=entIdentUse d=DOT member=entIdentUse LBRACK idx=expr[namer, context, false] RBRACK
		{ res = new AssignIndexedNode(coords, new QualIdentNode(getCoords(d), owner, member), e, idx, context); }
	|
	  (y=YIELD { yielded = true; })? (DOUBLECOLON)? variable=entIdentUse LBRACK idx=expr[namer, context, false] RBRACK
		{ res = new AssignIndexedNode(coords, new IdentExprNode(variable, yielded), e, idx, context, onLHS); }
	|
	  de=defEntityToBeYieldedTo[null, null, null, namer, context, directlyNestingLHSGraph]
		{
			DefDeclStatementNode tgt = new DefDeclStatementNode(coords, de, context);
			ms.AddStatement(tgt);
			res = new AssignNode(coords, new IdentExprNode(tgt.Decl.Ident), e, context, onLHS);
		}
	;

ifelse [ bool onLHS, bool isSimple, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ EvalStatementNode res = null ]
	@init {
		CollectNode<EvalStatementNode> elseRemainder = new CollectNode<EvalStatementNode>();
	}
	: i=IF LPAREN e=expr[namer, context, false] RPAREN
		LBRACE { env.PushScope("if", getCoords(i)); }
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
	  (el=ELSE // allow else { statements } as well as else if{ expr; statements} else { statements}, and so on (nesting mapped to linear syntax)
		(
			ie = ifelse[onLHS, isSimple, namer, context, directlyNestingLHSGraph]
				{ elseRemainder.AddChild(ie); }
		| 
			LBRACE { env.PushScope("else", getCoords(el)); }
				ecs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
			RBRACE { env.PopScope(); }
				{ elseRemainder = ecs; }
		)
	  )?
		{ res=new ConditionStatementNode(getCoords(i), e, cs, elseRemainder); }
	;

switchcase [ bool onLHS, bool isSimple, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ EvalStatementNode res = null ]
	@init {
		CollectNode<CaseStatementNode> cases = new CollectNode<CaseStatementNode>();
		int caseCounter = 1;
		IToken branch = null;
		ExprNode caseExpr = null;
	}
	: s=SWITCH LPAREN e=expr[namer, context, false] RPAREN 
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
				LBRACE { env.PushScope("case_"+caseCounter, getCoords(s)); } 
					cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
					{ cases.AddChild(new CaseStatementNode(getCoords(branch), caseExpr, cs)); ++caseCounter; }
				RBRACE { env.PopScope(); } 
			)+
		RBRACE
			{ res=new SwitchStatementNode(getCoords(s), e, cases); }
	;

forContent [ Coords f, bool onLHS, bool isSimple, AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ EvalStatementNode res = null ]
	options { k = *; }
	@init {
		IdentNode iterIdentUse = null;
		VarDeclNode iterVar = null;
	}
	: variable=entIdentDecl IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			iterIdentUse = new IdentNode(env.Occurs(ParserEnvironment.ITERATEDS, i.Text, getCoords(i)));
			iterVar = new VarDeclNode(variable, IdentNode.Invalid, directlyNestingLHSGraph, context, null);
			res = new IteratedAccumulationYieldNode(f, iterVar, iterIdentUse, cs);
		}
	| variable=entIdentDecl COLON dres=forContentTypedIteration[f, variable, onLHS, isSimple, namer, context, directlyNestingLHSGraph]
		{ res = dres; }
	;

forContentTypedIteration [ Coords f, IdentNode leftVar, bool onLHS, bool isSimple,
		AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ EvalStatementNode res = null ]
	options { k = *; }
	@init {
		IdentNode iterIdentUse = null;
		IdentNode containerIdentUse = null;
		IdentNode matchesIdentUse = null;
		IdentNode functionIdentUse = null;
		VarDeclNode iterVar = null;
		VarDeclNode iterIndex = null;
	}
	: type=typeIdentUse IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			containerIdentUse = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)));
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context, null);
			res = new ContainerAccumulationYieldNode(f, iterVar, null, containerIdentUse, cs);
		}
	| indexType=typeIdentUse RARROW variable=entIdentDecl COLON type=typeIdentUse IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			containerIdentUse = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)));
			iterVar = new VarDeclNode(variable, type, directlyNestingLHSGraph, context, null);
			iterIndex = new VarDeclNode(leftVar, indexType, directlyNestingLHSGraph, context, null);
			res = new ContainerAccumulationYieldNode(f, iterVar, iterIndex, containerIdentUse, cs);
		}
	| type=typeIdentUse IN
		(
			{ ParserEnvironment.IsKnownForFunction(input.LT(1).Text) }?
			function=externalFunctionInvocationExpr[namer, context, false] RPAREN
			{
				if(!(function is FunctionInvocationDecisionNode)) // TODO: print function name
					reportError(function.Coords, "Unknown function (or wrong number of arguments) in for loop iterating over a graph access function.");
			}
		|
			{ ParserEnvironment.IsKnownForIndexFunction(input.LT(1).Text) }?
			funcIdent=funcOrExtFuncIdentUse LPAREN idx=indexIdentUse function=indexFunctionInvocationExprContinuation[funcIdent, null, idx, namer, context, false] RPAREN
		)
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context, null);
			res = new ForFunctionNode(f, iterVar, (FunctionOrBuiltinFunctionInvocationBaseNode)function, cs);
		}
	| MATCH LT actionIdent=actionIdentUse GT IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			matchesIdentUse = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)));
			iterVar = new VarDeclNode(leftVar, MatchTypeActionNode.GetMatchTypeIdentNode(env, actionIdent), directlyNestingLHSGraph, context, null);
			res = new MatchesAccumulationYieldNode(f, iterVar, matchesIdentUse, cs);
		}
	| MATCH LT CLASS matchClassIdent=typeIdentUse GT IN i=IDENT RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			matchesIdentUse = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)));
			iterVar = new VarDeclNode(leftVar, matchClassIdent, directlyNestingLHSGraph, context, null);
			res = new MatchesAccumulationYieldNode(f, iterVar, matchesIdentUse, cs);
		}
	| type=typeIdentUse IN LBRACK left=expr[namer, context, false] COLON right=expr[namer, context, false] RBRACK RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context, null);
			res = new IntegerRangeIterationYieldNode(f, iterVar, left, right, cs);
		}
	| type=typeIdentUse IN LBRACE idx=indexIdentUse EQUAL e=expr[namer, context, false] RBRACE RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context, null);
			res = new ForIndexAccessEqualityYieldNode(f, iterVar, context, idx, e, directlyNestingLHSGraph, cs);
			reportWarning(f, "the for(. in { idx == .}) loop is deprecated, use for(. in nodesFromIndexSame(idx, .)) or for(. in edgesFromIndexSame(idx, .)) instead.");
		}
	| type=typeIdentUse IN LBRACE i=IDENT LPAREN idx=indexIdentUse (os=relOS e=expr[namer, context, false]
			(COMMA idx2=indexIdentUse os2=relOS e2=expr[namer, context, false])?)? RPAREN RBRACE RPAREN
		LBRACE
			cs=computations[onLHS, isSimple, context, directlyNestingLHSGraph]
		RBRACE { env.PopScope(); }
		{
			iterVar = new VarDeclNode(leftVar, type, directlyNestingLHSGraph, context, null);
			bool ascending = true;
			if(i.Text.Equals("ascending")) 
				ascending = true;
			else if(i.Text.Equals("descending"))
				ascending = false;
			else
				reportError(getCoords(i), "An ordered index access loop must start with ascending or descending (given is " + i.Text + ").");
			if(idx2!=null && !idx.ToString().Equals(idx2.ToString()))
				reportError(idx2.Coords, "The same index must be used in an ordered index access loop with two constraints (given are " + idx + " and " + idx2 + ").");
			res = new ForIndexAccessOrderingYieldNode(f, iterVar, context, ascending, idx, os, e, os2, e2, directlyNestingLHSGraph, cs);
			reportWarning(f, "the for(. in { ascending(idx >= ., idx <=. )}) loop is deprecated, use for(. in nodesFromIndexFromToAscending(idx, ., .)) or for(. in edgesFromIndexFromToAscending(idx, ., .)) instead (or their descending versions).");
		}
	;

assignTo [ AnonymousScopeNamer namer, int context ]
		returns [ CompoundAssignNode.CompoundAssignmentType ccat = CompoundAssignNode.CompoundAssignmentType.NONE,
				BaseNode tgtChanged = null ]
	: (ASSIGN_TO { $ccat = CompoundAssignNode.CompoundAssignmentType.ASSIGN; }
		| BOR_TO { $ccat = CompoundAssignNode.CompoundAssignmentType.UNION; }
		| BAND_TO { $ccat = CompoundAssignNode.CompoundAssignmentType.INTERSECTION; })
	  tgtc=assignToTgt[namer, context] { $tgtChanged = tgtc; }
	;

assignToTgt [ AnonymousScopeNamer namer, int context ] returns [ BaseNode tgtChanged = null ]
	options { k = 4; }
	@init {
		bool yielded = false;
	}
	: tgtOwner=entIdentUse d=DOT tgtMember=entIdentUse { tgtChanged = new QualIdentNode(getCoords(d), tgtOwner, tgtMember); }
		| (y=YIELD { yielded = true; })? tgtVariable=entIdentUse { tgtChanged = new IdentExprNode(tgtVariable, yielded); }
		| vis=visited[namer, context] { tgtChanged = vis; }
	;

expr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: e=condExpr[namer, context, inEnumInit] { res = e; }
	;

condExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrCond=logOrExpr[namer, context, inEnumInit] { res = exprOrCond; }
		( op=QUESTION trueCase=expr[namer, context, inEnumInit] COLON falseCase=condExpr[namer, context, inEnumInit]
			{ res = makeTernOp(op, exprOrCond, trueCase, falseCase); }
		)?
	;

logOrExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=logAndExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=LOR right=logAndExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

logAndExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=bitOrExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=LAND right=bitOrExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

bitOrExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=bitXOrExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=BOR right=bitXOrExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

bitXOrExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=bitAndExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=BXOR right=bitAndExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

bitAndExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=exceptExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=BAND right=exceptExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

exceptExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=eqExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=BACKSLASH right=eqExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

eqOp returns [ IToken t = null ]
	: e=EQUAL { t = e; }
	| ne=NOT_EQUAL { t = ne; }
	| se=STRUCTURAL_EQUAL { t = se; }
	;

eqExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=relExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=eqOp right=relExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

relOp returns [ IToken t = null ]
	: lt=LT { t = lt; }
	| le=LE { t = le; }
	| gt=GT { t = gt; }
	| ge=GE { t = ge; }
	| in_=IN { t = in_; }
	;

relExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=shiftExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=relOp right=shiftExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

shiftOp returns [ IToken t = null ]
	: sl=SL { t = sl; }
	| sr=SR { t = sr; }
	| bsr=BSR { t = bsr; }
	;

shiftExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=addExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=shiftOp right=addExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

addOp returns [ IToken t = null ]
	: p=PLUS { t = p; }
	| m=MINUS { t = m; }
	;

addExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=mulExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=addOp right=mulExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

mulOp returns [ IToken t = null ]
	: s=STAR { t = s; }
	| m=MOD { t = m; }
	| d=DIV { t = d; }
	;

mulExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: exprOrLeft=unaryExpr[namer, context, inEnumInit] { res = exprOrLeft; }
		( op=mulOp right=unaryExpr[namer, context, inEnumInit]
			{ res = makeBinOp(op, res, right); }
		)*
	;

unaryExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: t=TILDE e=unaryExpr[namer, context, inEnumInit]
		{ res = makeUnOp(t, e); }
	| n=NOT e=unaryExpr[namer, context, inEnumInit]
		{ res = makeUnOp(n, e); }
	| m=MINUS e=unaryExpr[namer, context, inEnumInit]
		{
			OperatorNode neg = new ArithmeticOperatorNode(getCoords(m), Operator.NEG);
			neg.AddChild(e);
			res = neg;
		}
	| PLUS e=unaryExpr[namer, context, inEnumInit] { res = e; }
	| (LPAREN typeIdentUse RPAREN unaryExpr[new AnonymousScopeNamer(null), 0, false]) =>
		p=LPAREN id=typeIdentUse RPAREN e=unaryExpr[namer, context, inEnumInit]
		{ res = new CastNode(getCoords(p), id, e); }
	| e=primaryExpr[namer, context, inEnumInit] ( (LBRACK ~PLUS | DOT) => e=selectorExpr[namer, context, e, inEnumInit] )* { res = e; }
	; 

primaryExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	options { k = 4; }
	@init {
		IdentNode id;
	}
	: e=visitedFunction[namer, context] { res = e; }
	| e=nameOf[namer, context] { res = e; }
	| e=count { res = e; }
	| e=globalsAccessExpr { res = e; }
	| e=constant { res = e; }
	| e=typeOf { res = e; }
	| e=newInitExpr[namer, context] { res = e; }
	| { ParserEnvironment.IsIsInIndexFunction(input.LT(1).Text) }? f=funcOrExtFuncIdentUse LPAREN cand=expr[namer, context, inEnumInit] COMMA idx=indexIdentUse e=indexFunctionInvocationExprContinuation[f, cand, idx, namer, context, inEnumInit] { res = e; }
	| { ParserEnvironment.IsNonIsInIndexFunction(input.LT(1).Text) }? f=funcOrExtFuncIdentUse LPAREN idx=indexIdentUse e=indexFunctionInvocationExprContinuation[f, null, idx, namer, context, inEnumInit] { res = e; }
	| e=externalFunctionInvocationExpr[namer, context, inEnumInit] { res = e; }
	| e=scanFunctionInvocationExpr[namer, context, inEnumInit] { res = e; }
	| LPAREN e=expr[namer, context, inEnumInit] { res = e; } RPAREN
	| p=PLUSPLUS { reportError(getCoords(p), "An increment operator \"++\" is not supported."); }
	| q=MINUSMINUS { reportError(getCoords(q), "A decrement operator \"--\" is not supported."); }
	| i=IDENT
		{
			if(i.Text.Equals("this") && !env.Test(ParserEnvironment.ENTITIES, "this"))
				res = new ThisExprNode(getCoords(i));
			else {
				// entity names overwrite type names
				if(env.Test(ParserEnvironment.ENTITIES, i.Text) || !env.Test(ParserEnvironment.TYPES, i.Text))
					id = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)));
				else
					id = new IdentNode(env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i)));
				res = new IdentExprNode(id);
			}
		}
	| pen=IDENT d=DOUBLECOLON i=IDENT 
		{
			if(env.Test(ParserEnvironment.PACKAGES, pen.Text) || !env.Test(ParserEnvironment.TYPES, pen.Text)) {
				id = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, pen.Text, getCoords(pen)), 
					env.Occurs(ParserEnvironment.TYPES, i.Text, getCoords(i)));
				res = new IdentExprNode(id);
			} else {
				res = new DeclExprNode(new EnumExprNode(getCoords(d), 
					new IdentNode(env.Occurs(ParserEnvironment.TYPES, pen.Text, getCoords(pen))),
					new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)))));
			}
		}
	| p=IDENT DOUBLECOLON en=IDENT d=DOUBLECOLON i=IDENT
		{
			res = new DeclExprNode(new EnumExprNode(getCoords(d), 
				new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)),
					env.Occurs(ParserEnvironment.TYPES, en.Text, getCoords(en))),
				new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)))));
		}
	| LBRACK QUESTION iterIdent=iterIdentUse { res = new IteratedQueryExprNode(iterIdent.Coords, iterIdent,
			new ArrayTypeNode(MatchTypeIteratedNode.GetMatchTypeIdentNode(env, env.CurrentActionOrSubpattern, iterIdent))); } RBRACK
	;

visitedFunction [ AnonymousScopeNamer namer, int context ] returns [ VisitedNode res ]
	: v=VISITED LPAREN elem=expr[namer, context, false] 
		( COMMA idExpr=expr[namer, context, false] RPAREN
			{ res = new VisitedNode(getCoords(v), idExpr, elem); }
		| RPAREN
			{ res = new VisitedNode(getCoords(v), new IntConstNode(getCoords(v), 0), elem); }
		)
	;

visited [ AnonymousScopeNamer namer, int context ] returns [ VisitedNode res ]
	: vf=visitedFunction[namer, context] { res = vf; }
	| (elem=entIdentExpr | elem=globalsAccessExpr) DOT v=VISITED  
		( (LBRACK) => LBRACK idExpr=expr[namer, context, false] RBRACK // [ starts a visited flag expression, not a following map access selector expression
			{ res = new VisitedNode(getCoords(v), idExpr, elem); }
		| 
			{ res = new VisitedNode(getCoords(v), new IntConstNode(getCoords(v), 0), elem); }
		)
	;

nameOf [ AnonymousScopeNamer namer, int context ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: n=NAMEOF LPAREN (id=expr[namer, context, false])? RPAREN { res = new NameofNode(getCoords(n), id); }
	;

count returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: c=COUNT LPAREN i=IDENT RPAREN	{ res = new CountNode(getCoords(c),
			new IdentNode(env.Occurs(ParserEnvironment.ITERATEDS, i.Text, getCoords(i)))); }
	;

typeOf returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: t=TYPEOF LPAREN id=entIdentUse RPAREN { res = new TypeofNode(getCoords(t), id); }
	;

newInitExpr [ AnonymousScopeNamer namer, int context ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	options { k = 3; }
	: (NEW)? e=initContainerExpr[namer, context] { res = e; }
	| (NEW)? e=initMatchExpr[context] { res = e; }
	| e=initObjectExpr[namer, context] { res = e; }
	;

initContainerExpr [ AnonymousScopeNamer namer, int context ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: { input.LT(1).Text.Equals("map") }?
		i=IDENT LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT
		e1=initMapExpr[namer, context, null, new MapTypeNode(keyType, valueType)] { res = e1; }
	| { input.LT(1).Text.Equals("set") }?
		i=IDENT LT valueType=typeIdentUse GT
		e2=initSetExpr[namer, context, null, new SetTypeNode(valueType)] { res = e2; }
	| { input.LT(1).Text.Equals("array") }?
		i=IDENT LT arrayType=containerTypeContinuation[i, null]
		e3=initArrayExpr[namer, context, null, (ArrayTypeNode)arrayType] { res = e3; }
	| { input.LT(1).Text.Equals("deque") }?
		i=IDENT LT valueType=typeIdentUse GT
		e4=initDequeExpr[namer, context, null, new DequeTypeNode(valueType)] { res = e4; }
	;

initMatchExpr [ int context ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: MATCH LT CLASS matchClassIdent=typeIdentUse GT l=LPAREN RPAREN
		{ res = new MatchInitNode(getCoords(l), matchClassIdent); }
	;

initObjectExpr [ AnonymousScopeNamer namer, int context ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	options { k = 5; }
	@init {
		ObjectInitNode oin = null;
	}
	: NEW classIdent=typeIdentUse l=LPAREN RPAREN
		{ oin = new ObjectInitNode(getCoords(l), classIdent); res = oin; }
	| NEW classIdent=typeIdentUse AT l=LPAREN
		{ oin = new ObjectInitNode(getCoords(l), classIdent); }
		(attributesInitializationList[oin, classIdent, namer, context])?
		RPAREN { res = oin; }
	;

attributesInitializationList [ ObjectInitNode oi, IdentNode classIdent, AnonymousScopeNamer namer, int context ]
	: attributeInitialization[oi, classIdent, namer, context] ( COMMA attributeInitialization[oi, classIdent, namer, context] )*
	;

attributeInitialization [ ObjectInitNode oi, IdentNode classIdent, AnonymousScopeNamer namer, int context ]
	: attr=memberIdentUse ASSIGN arg=expr[namer, context, false]
		{ oi.AddAttributeInitialization(new AttributeInitializationNode(oi, classIdent, attr, arg)); }
	;

constant returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: b=NUM_BYTE
		{ res = new ByteConstNode(getCoords(b), SByte.Parse(ByteConstNode.RemoveSuffix(b.Text))); }
	| sh=NUM_SHORT
		{ res = new ShortConstNode(getCoords(sh), Int16.Parse(ShortConstNode.RemoveSuffix(sh.Text))); }
	| i=NUM_INTEGER
		{ res = new IntConstNode(getCoords(i), Int32.Parse(i.Text)); }
	| l=NUM_LONG
		{ res = new LongConstNode(getCoords(l), Int64.Parse(LongConstNode.RemoveSuffix(l.Text))); }
	| hb=NUM_HEX_BYTE
		{ res = new ByteConstNode(getCoords(hb), SByte.Parse(ByteConstNode.RemoveSuffix(hb.Text.Substring(2)), System.Globalization.NumberStyles.HexNumber)); }
	| hsh=NUM_HEX_SHORT
		{ res = new ShortConstNode(getCoords(hsh), Int16.Parse(ShortConstNode.RemoveSuffix(hsh.Text.Substring(2)), System.Globalization.NumberStyles.HexNumber)); }
	| hi=NUM_HEX
		{ res = new IntConstNode(getCoords(hi), Int32.Parse(hi.Text.Substring(2), System.Globalization.NumberStyles.HexNumber)); }
	| hl=NUM_HEX_LONG
		{ res = new LongConstNode(getCoords(hl), Int64.Parse(LongConstNode.RemoveSuffix(hl.Text.Substring(2)), System.Globalization.NumberStyles.HexNumber)); }
	| f=NUM_FLOAT
		{ res = new FloatConstNode(getCoords(f), Single.Parse(f.Text, System.Globalization.CultureInfo.InvariantCulture)); }
	| d=NUM_DOUBLE
		{ res = new DoubleConstNode(getCoords(d), Double.Parse(d.Text, System.Globalization.CultureInfo.InvariantCulture)); }
	| s=STRING_LITERAL
		{
			String buff = s.Text;
			// Strip the " from the string
			buff = buff.Substring(1, buff.length() - 2);
			res = new StringConstNode(getCoords(s), buff);
		}
	| tt=TRUE
		{ res = new BoolConstNode(getCoords(tt), true); }
	| ff=FALSE
		{ res = new BoolConstNode(getCoords(ff), false); }
	| n=NULL
		{ res = new NullConstNode(getCoords(n)); }
	;

enumConstant returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	options { k = 4; }
	: pen=IDENT d=DOUBLECOLON i=IDENT 
		{
			res = new DeclExprNode(new EnumExprNode(getCoords(d), 
					new IdentNode(env.Occurs(ParserEnvironment.TYPES, pen.Text, getCoords(pen))),
					new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)))));
		}
	| p=IDENT DOUBLECOLON en=IDENT d=DOUBLECOLON i=IDENT
		{
			res = new DeclExprNode(new EnumExprNode(getCoords(d), 
					new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, p.Text, getCoords(p)),
						env.Occurs(ParserEnvironment.TYPES, en.Text, getCoords(en))),
					new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)))));
		}
	;

entIdentExpr returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: i=IDENT
		{
			if(i.Text.Equals("this") && !env.Test(ParserEnvironment.ENTITIES, "this"))
				res = new ThisExprNode(getCoords(i));
			else
				res = new IdentExprNode(new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i))));
		}
	;

globalsAccessExpr returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		IdentNode id;
	}
	: DOUBLECOLON i=IDENT
		{
			id = new IdentNode(env.Occurs(ParserEnvironment.ENTITIES, i.Text, getCoords(i)));
			res = new IdentExprNode(id);
		}
	;

indexFunctionInvocationExprContinuation [ IdentNode funcIdent, ExprNode cand, IdentNode idx, AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		CollectNode<BaseNode> paramz = new CollectNode<BaseNode>();
		if(cand != null)
			paramz.AddChild(cand);
		paramz.AddChild(idx);
	}
	: RPAREN
		{ res = new IndexFunctionInvocationDecisionNode(funcIdent, paramz, env); }
	| COMMA e=expr[namer, context, inEnumInit] { paramz.AddChild(e); }
		(
			RPAREN { res = new IndexFunctionInvocationDecisionNode(funcIdent, paramz, env); } 
		|
			COMMA e=expr[namer, context, inEnumInit] { paramz.AddChild(e); } multipleIndexFunctionInvocationExprContinuation[funcIdent, paramz, namer, context, inEnumInit]
				RPAREN { res = new IndexFunctionInvocationDecisionNode(funcIdent, paramz, env); }
		)
	;

multipleIndexFunctionInvocationExprContinuation [ IdentNode funcIdent, CollectNode<BaseNode> paramz, AnonymousScopeNamer namer, int context, bool inEnumInit ]
	: ( COMMA idx=indexIdentUse { paramz.AddChild(idx); } ( COMMA e=expr[namer, context, inEnumInit] { paramz.AddChild(e); } ( COMMA e=expr[namer, context, inEnumInit] { paramz.AddChild(e); } multipleIndexFunctionInvocationExprContinuation[funcIdent, paramz, namer, context, inEnumInit] )? )? )?
	;

externalFunctionInvocationExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	@init {
		bool packPrefix = false;
	}
	: (pack=IDENT DOUBLECOLON {packPrefix=true;})? (i=IDENT | i=COPY | i=CLONE) paramz=paramExprs[namer, context, inEnumInit]
		{
			if(ParserEnvironment.IsKnownFunction(pack, i, paramz)) {
				IdentNode funcIdent = new IdentNode(env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
				if(packPrefix) {
					res = new PackageFunctionInvocationDecisionNode(pack.Text, funcIdent, paramz, env);
				} else {
					res = new FunctionInvocationDecisionNode(funcIdent, paramz, env);
				}
			} else {
				IdentNode funcIdent;
				if(packPrefix) {
					funcIdent = new PackageIdentNode(env.Occurs(ParserEnvironment.PACKAGES, pack.Text, getCoords(pack)), 
						env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
				} else {
					funcIdent = new IdentNode(env.Occurs(ParserEnvironment.FUNCTIONS_AND_EXTERNAL_FUNCTIONS, i.Text, getCoords(i)));
				}
				res = new FunctionOrExternalFunctionInvocationExprNode(funcIdent, paramz);
			}
		}
	;

scanFunctionInvocationExpr [ AnonymousScopeNamer namer, int context, bool inEnumInit ] returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: (i=SCAN | i=TRYSCAN) (LT type=typeOrContainerTypeContinuation[namer, context])? LPAREN e=expr[namer, context, inEnumInit] RPAREN
		{
			if(i.Text.Equals("scan")) {
				res = new ScanExprNode(getCoords(i), type, e);
			} else {
				res = new TryScanExprNode(getCoords(i), type, e);
			}
		}
	;

typeOrContainerTypeContinuation [ AnonymousScopeNamer namer, int context ] returns [ BaseNode res = null ]
	: { input.LT(1).Text.Equals("map") }?
		i=IDENT LT keyType=typeIdentUse COMMA valueType=typeIdentUse (GT GT | SR) 
		{ res = new MapTypeNode(keyType, valueType); }
	| { input.LT(1).Text.Equals("set") }?
		i=IDENT LT valueType=typeIdentUse (GT GT | SR)
		{ res = new SetTypeNode(valueType); }
	| { input.LT(1).Text.Equals("array") }?
		i=IDENT LT valueType=typeIdentUse (GT GT | SR)
		{ res = new ArrayTypeNode(valueType); }
	| { input.LT(1).Text.Equals("deque") }?
		i=IDENT LT valueType=typeIdentUse (GT GT | SR)
		{ res = new DequeTypeNode(valueType); }
	| typeIdent=typeIdentUse GT
		{ res = typeIdent; }
	;

selectorExpr [ AnonymousScopeNamer namer, int context, ExprNode target, bool inEnumInit ]
		returns [ ExprNode res = ParserEnvironment.InitExprNode() ]
	: l=LBRACK key=expr[namer, context, inEnumInit] RBRACK { res = makeBinOp(l, target, key); }
	| d=DOT id=memberIdentUse
		(
			{ ParserEnvironment.IsArrayAttributeAccessMethodName(input.Get(input.LT(1).TokenIndex-1).Text) }?
			LT mi=memberIdentUse GT
			paramz=paramExprs[namer, context, inEnumInit]
			{ res = new FunctionMethodInvocationDecisionNode(target, id, paramz, mi); }
		|
			{ input.Get(input.LT(1).TokenIndex-1).Text.Equals("map") }?
			LT ti=typeIdentUse GT
			(initExp=initExpression[namer, context, id.ToString()])?
			LBRACE { namer.DefExprBlock(id, id.Coords); } { env.PushScope(namer.ExprBlock()); }
			lambdaExprVar=lambdaExprVarDeclPrefix[namer, context] e=expr[namer, context, inEnumInit]
			{
				if(initExp != null) {
					if($initExp.filterText.Equals("mapStartWithAccumulateBy")) {
						res = new ArrayMapStartWithAccumulateByNode(getCoords(d), target, ti,
							$initExp.va, $initExp.expr,
							$lambdaExprVar.va, $lambdaExprVar.vp, $lambdaExprVar.vi, $lambdaExprVar.vd, e);
					} else
						reportError(id.Coords, "Unknown lambda expression method "+ $initExp.filterText + ". Available are: map, removeIf, mapStartWithAccumulateBy.");
				} else
					res = new ArrayMapNode(getCoords(d), target, ti, $lambdaExprVar.va, $lambdaExprVar.vi, $lambdaExprVar.vd, e);
			}
			{ env.PopScope(); } { namer.UndefExprBlock(); } RBRACE
		|
			{ input.Get(input.LT(1).TokenIndex-1).Text.Equals("removeIf") }?
			LBRACE { namer.DefExprBlock(id, id.Coords); } { env.PushScope(namer.ExprBlock()); }
			lambdaExprVar=lambdaExprVarDeclPrefix[namer, context] e=expr[namer, context, inEnumInit]
			{ res = new ArrayRemoveIfNode(getCoords(d), target, $lambdaExprVar.va, $lambdaExprVar.vi, $lambdaExprVar.vd, e); }
			{ env.PopScope(); } { namer.UndefExprBlock(); } RBRACE
		|
			paramz=paramExprs[namer, context, inEnumInit]
			{ res = new FunctionMethodInvocationDecisionNode(target, id, paramz, mi); }
		| 
			{ res = new MemberAccessExprNode(getCoords(d), target, id); }
		)
	| DOT v=VISITED  
		( (LBRACK) => LBRACK idExpr=expr[namer, context, false] RBRACK // [ starts a visited flag expression, not a following map access selector expression
			{ res = new VisitedNode(getCoords(v), idExpr, target); }
		| 
			{ res = new VisitedNode(getCoords(v), new IntConstNode(getCoords(v), 0), target); }
		)
	;

lambdaExprVarDeclPrefix [ AnonymousScopeNamer namer, int context ]
		returns [ VarDeclNode va, VarDeclNode vp, VarDeclNode vi, VarDeclNode vd ]
	options { k = *; }
	: arrayAccessVar=entIdentDecl COLON containerType=containerTypeUse SEMI
		{ $va = new VarDeclNode(arrayAccessVar, containerType, PatternGraphLhsNode.Invalid, context, true, true, "ref"); }
		lambdaExprVar=maybePreviousAccumulationAccessLambdaExprVarDecl[namer, context]
			{ $vp = $lambdaExprVar.vp; $vi = $lambdaExprVar.vi; $vd = $lambdaExprVar.vd; }
	| { $va = null; }
		lambdaExprVar=maybePreviousAccumulationAccessLambdaExprVarDecl[namer, context]
			{ $vp = $lambdaExprVar.vp; $vi = $lambdaExprVar.vi; $vd = $lambdaExprVar.vd; }
	;

maybePreviousAccumulationAccessLambdaExprVarDecl [ AnonymousScopeNamer namer, int context ]
		returns [ VarDeclNode vp, VarDeclNode vi, VarDeclNode vd ]
	options { k = *; }
	: previousAccumulationVar=entIdentDecl COLON type=typeIdentUse COMMA
		{ $vp = new VarDeclNode(previousAccumulationVar, type, PatternGraphLhsNode.Invalid, context, true, true, "var"); }
		lambdaExprVar=maybeIndexedLambdaExprVarDecl[namer, context]
			{ $vi = $lambdaExprVar.vi; $vd = $lambdaExprVar.vd; }
	| { $vp = null; }
		lambdaExprVar=maybeIndexedLambdaExprVarDecl[namer, context]
			{ $vi = $lambdaExprVar.vi; $vd = $lambdaExprVar.vd; }
	;

maybeIndexedLambdaExprVarDecl [ AnonymousScopeNamer namer, int context ]
		returns [ VarDeclNode vi, VarDeclNode vd ]
	options { k = *; }
	: indexLambdaExprVarDecl=lambdaExprVarDeclToBeYieldedTo[namer, context, PatternGraphLhsNode.Invalid] RARROW
		lambdaExprVarDecl=lambdaExprVarDeclToBeYieldedTo[namer, context, PatternGraphLhsNode.Invalid] RARROW 
		{ $vi = indexLambdaExprVarDecl; $vd = lambdaExprVarDecl; }
	|
		lambdaExprVarDecl=lambdaExprVarDeclToBeYieldedTo[namer, context, PatternGraphLhsNode.Invalid] RARROW
		{ $vd = lambdaExprVarDecl; }
	;

lambdaExprVarDeclToBeYieldedTo [ AnonymousScopeNamer namer, int context, PatternGraphLhsNode directlyNestingLHSGraph ]
		returns [ VarDeclNode res = ParserEnvironment.InitVarNode(directlyNestingLHSGraph, context) ]
	@init {
		VarDeclNode var = null;
	}
	: id=entIdentDecl COLON
		(
			type=typeIdentUse
			{
				var = new VarDeclNode(id, type, directlyNestingLHSGraph, context, true, true, null);
			}
		|
			matchTypeIdent=matchTypeIdentUse
			{
				var = new VarDeclNode(id, matchTypeIdent, directlyNestingLHSGraph, 0, null);
			}
		)
		{
			res = var;
		}
	;

paramExprs [ AnonymousScopeNamer namer, int context, bool inEnumInit ]
		returns [ CollectNode<ExprNode> paramz = new CollectNode<ExprNode>(); ]
	:	LPAREN
		(
			e=expr[namer, context, inEnumInit] { paramz.AddChild(e); }
			( COMMA e=expr[namer, context, inEnumInit] { paramz.AddChild(e); } )*
		)?
		RPAREN
	;


//////////////////////////////////////////
// Range Spec
//////////////////////////////////////////


rangeSpec returns [ RangeSpecNode res = null ]
	@init {
		lower = 0; upper = RangeSpecNode.UNBOUND;
		de.unika.ipd.grgen.parser.Coords coords = de.unika.ipd.grgen.parser.Coords.Invalid;
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
		{ value = Int64.Parse(i.Text); }
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
LTCOLON			:	'<:'	;
GTCOLON			:	':>'	;
GTLTCOLON		:	'>:<'	;
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
TRIPLEMINUS		:	'---'	;
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
		{ $channel=Hidden; }
	;

// single-line comment
SL_COMMENT
	: '//' ~('\n'|'\r')* '\r'? '\n' {$channel=Hidden;}
    ;

// multiple-line comment
ML_COMMENT
	:   '/*' ( options {greedy=false;} : . )* '*/' {$channel=Hidden;}
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
	String filename = s.Text;
	filename = filename.Substring(1,filename.Length-2);
	
	//Instead of making includes relative to the main grg file
	//we try to interpret the included path as relative to the including file
	//File file = new File(filename);
	//if(!file.isAbsolute())
	if(!Path.IsPathRooted(filename))
	{
		try
		{
			//Get the parent folder of the including file
			//File dir = new File(env.getFilename()).getCanonicalFile().getParentFile();
			//file = new File(dir, filename);
			string parentDir = Directory.GetParent(env.Filename).FullName;
			filename = parentDir + Path.DirectorySeparatorChar + filename;
		}
		catch(IOException e)
		{
			//getCanonicalFile can throw an IOException if that happens we just return to the old behaviour
		}
	}
	env.PushFile(this, file);
  }
  ;

HASHUSING : '#using';

ABSTRACT : 'abstract';
ALTERNATIVE : 'alternative';
ARBITRARY : 'arbitrary';
AUTO : 'auto';
BREAK : 'break';
CASE : 'case';
CLASS : 'class';
CLONE : 'clone';
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
IMPLEMENTS : 'implements';
IN : 'in';
INDEPENDENT : 'independent';
INDEX : 'index';
INDUCED : 'induced';
ITERATED : 'iterated';
LOCK : 'lock';
MATCH : 'match';
MODIFY : 'modify';
MULTIPLE : 'multiple';
NAMEOF : 'nameof';
NEGATIVE : 'negative';
NEW : 'new';
NODE : 'node';
NULL : 'null';
OPTIONAL : 'optional';
PACKAGE : 'package';
PATTERN : 'pattern';
PATTERNPATH : 'patternpath';
PROCEDURE : 'procedure';
REPLACE : 'replace';
RETURN : 'return';
RULE : 'rule';
SCAN : 'scan';
SEQUENCE : 'sequence';
SWITCH : 'switch';
TEST : 'test';
TRANSIENT : 'transient';
TRUE : 'true';
TRYSCAN : 'tryscan';
TYPEOF : 'typeof';
UNDIRECTED : 'undirected';
USING : 'using';
VISITED : 'visited';
WHILE : 'while';
YIELD : 'yield';

IDENT : ('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')* ;
