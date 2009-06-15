/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */
 
/*
 * GrGen model and rule specification language grammar for ANTLR 3
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski, Veit Batz, Edgar Jakumeit, Sebastian Buchwald, Moritz Kroll
 * @version $Id: base.g 20237 2008-06-24 15:59:24Z eja $
*/

grammar GrGen;

options {
	k = 2;
}

tokens {
	NUM_INTEGER;
	NUM_FLOAT;
	NUM_DOUBLE;
}

@lexer::header {
	package de.unika.ipd.grgen.parser.antlr;

	import java.io.File;
}

@lexer::members {
	GRParserEnvironment env;

	void setEnv(GRParserEnvironment env) {
		this.env = env;
	}
  
	// overriden for handling EOF of included file
	public Token nextToken() {
		Token token = super.nextToken();

		if(token==Token.EOF_TOKEN) {
			if(env.popFile(this)) {
				token = super.nextToken();
			}
		}

		// Skip first token after switching to another input.
		if(((CommonToken)token).getStartIndex() < 0) {
			token = this.nextToken();
		}
			
		return token;
	}
}

@header {
	package de.unika.ipd.grgen.parser.antlr;
	
	import java.util.Iterator;
	import java.util.LinkedList;
	import java.util.Map;
	import java.util.HashMap;
	import java.util.Collection;
	
	import java.io.File;

	import de.unika.ipd.grgen.parser.*;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.util.*;
}

@members {
	class NestedPatternCounters {
		NestedPatternCounters() {
			alt = 0;
			iter = 0;
			neg = 0;
			idpt = 0;
		}
		
		int alt;
		int iter;
		int neg;
		int idpt;
	}
		
	boolean hadError = false;

	private static Map<Integer, Integer> opIds = new HashMap<Integer, Integer>();

	private static final void putOpId(int tokenId, int opId) {
		opIds.put(new Integer(tokenId), new Integer(opId));
	}

	static {
		putOpId(QUESTION, OperatorSignature.COND);
		putOpId(EQUAL, OperatorSignature.EQ);
		putOpId(NOT_EQUAL, OperatorSignature.NE);
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

	private OpNode makeOp(org.antlr.runtime.Token t) {
		Integer opId = opIds.get(new Integer(t.getType()));
		assert opId != null : "Invalid operator ID";
		return new ArithmeticOpNode(getCoords(t), opId.intValue());
	}

	private OpNode makeBinOp(org.antlr.runtime.Token t, ExprNode op0, ExprNode op1) {
		OpNode res = makeOp(t);
		res.addChild(op0);
		res.addChild(op1);
		return res;
	}

	private OpNode makeUnOp(org.antlr.runtime.Token t, ExprNode op) {
		OpNode res = makeOp(t);
		res.addChild(op);
		return res;
	}

	protected ParserEnvironment env;

	public void setEnv(ParserEnvironment env) {
		this.env = env;
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
		CollectNode<IdentNode> patternChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> actionChilds = new CollectNode<IdentNode>();
		String actionsName = Util.getActionsNameFromFilename(getFilename());
	}

	: (
		( a=ACTIONS i=ident
			{
				reportWarning(getCoords(a), "keyword \"actions\" is deprecated");
				reportWarning(getCoords(i),
					"the name of this actions component is not set by the identifier " +
					"after the \"actions\" keyword anymore but derived from the filename");
			}
			( usingDecl[modelChilds]
			| SEMI
			)
		)
	| usingDecl[modelChilds]
	)?

	( patternOrActionDecls[patternChilds, actionChilds] )? EOF
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
				ModelNode model = new ModelNode(id, new CollectNode<IdentNode>(), modelChilds);
				modelChilds = new CollectNode<ModelNode>();
				modelChilds.addChild(model);
			}
			main = new UnitNode(actionsName, getFilename(), env.getStdModel(), modelChilds, patternChilds, actionChilds);
		}
	;

usingDecl [ CollectNode<ModelNode> modelChilds ]
	@init{ Collection<String> modelNames = new LinkedList<String>(); }

	: u=USING identList[modelNames] SEMI
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
	;

patternOrActionDecls[ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds ]
	@init{ mod = 0; }

	: ( mod=patternModifiers patternOrActionDecl[patternChilds, actionChilds, mod] )+
	;

patternModifiers returns [ int res = 0 ]
	: ( m=patternModifier[ res ]  { res = m; } )*
	;

patternModifier [ int mod ] returns [ int res = 0 ]
	: i=INDUCED { if((mod & PatternGraphNode.MOD_INDUCED)!=0) {
	              reportError(getCoords(i), "\"induced\" modifier already declared");
	              }
	              res = mod | PatternGraphNode.MOD_INDUCED;
	            }
	| e=EXACT { if((mod & PatternGraphNode.MOD_EXACT)!=0) {
	              reportError(getCoords(e), "\"exact\" modifier already declared");
	              }
	              res = mod | PatternGraphNode.MOD_EXACT;
	          }
	| d=DPO { if((mod & PatternGraphNode.MOD_DPO)!=0) {
	              reportError(getCoords(d), "\"dpo\" modifier already declared");
	              }
	              res = mod | PatternGraphNode.MOD_DPO;
	        }
	;

patternOrActionDecl [ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds, int mod ]
	@init{
		CollectNode<EvalStatementNode> eval = new CollectNode<EvalStatementNode>();
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<RhsDeclNode> rightHandSides = new CollectNode<RhsDeclNode>();
	}

	: t=TEST id=actionIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, null] ret=returnTypes LBRACE
		left=patternPart[getCoords(t), params, mod, BaseNode.CONTEXT_TEST|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.toString()]
			{
				id.setDecl(new TestDeclNode(id, left, ret));
				actionChilds.addChild(id);
			}
		RBRACE popScope
		{
			if((mod & PatternGraphNode.MOD_DPO)!=0) {
				reportError(getCoords(t), "no \"dpo\" modifier allowed");
			}
		}
	| r=RULE id=actionIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, null] ret=returnTypes LBRACE
		left=patternPart[getCoords(r), params, mod, BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.toString()]
		( rightReplace=replacePart[eval, new CollectNode<BaseNode>(), BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				id.setDecl(new RuleDeclNode(id, left, rightReplace, ret));
				actionChilds.addChild(id);
			}
		| rightModify=modifyPart[eval, dels, new CollectNode<BaseNode>(), BaseNode.CONTEXT_RULE|BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id, left]
			{
				id.setDecl(new RuleDeclNode(id, left, rightModify, ret));
				actionChilds.addChild(id);
			}
		)
		RBRACE popScope
	| p=PATTERN id=patIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS, null] LBRACE
		left=patternPart[getCoords(p), params, mod, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS, id.toString()]
		( rightReplace=replacePart[eval, new CollectNode<BaseNode>(), BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id, left]
			{
				rightHandSides.addChild(rightReplace);
			}
		| rightModify=modifyPart[eval, dels, new CollectNode<BaseNode>(), BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id, left]
			{
				rightHandSides.addChild(rightModify);
			}
		)*
			{
				id.setDecl(new SubpatternDeclNode(id, left, rightHandSides));
				patternChilds.addChild(id);
			}
		RBRACE popScope
	;

parameters [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN (paramList[res, context, directlyNestingLHSGraph])? RPAREN
	|
	;

paramList [ CollectNode<BaseNode> params, int context, PatternGraphNode directlyNestingLHSGraph ]
	: p=param[context, directlyNestingLHSGraph] { params.addChild(p); } ( COMMA p=param[context, directlyNestingLHSGraph] { params.addChild(p); } )*
	;

param [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	: MINUS edge=edgeDeclParam[context, directlyNestingLHSGraph] direction = forwardOrUndirectedEdgeParam
	{
		BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
		res = new ConnectionNode(dummy, edge, dummy, direction);
	}

	| LARROW edge=edgeDeclParam[context, directlyNestingLHSGraph] RARROW
	{
		BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
		res = new ConnectionNode(dummy, edge, dummy, ConnectionNode.ARBITRARY_DIRECTED);
	}

	| QUESTIONMINUS edge=edgeDeclParam[context, directlyNestingLHSGraph] MINUSQUESTION
	{
		BaseNode dummy = env.getDummyNodeDecl(context, directlyNestingLHSGraph);
		res = new ConnectionNode(dummy, edge, dummy, ConnectionNode.ARBITRARY);
	}

	| v=varDecl[context] { res = v; }

	| node=nodeDeclParam[context, directlyNestingLHSGraph]
	{
		res = new SingleNodeConnNode(node, directlyNestingLHSGraph);
	}
	;

forwardOrUndirectedEdgeParam returns [ int res = ConnectionNode.ARBITRARY ]
	: RARROW { res = ConnectionNode.DIRECTED; }
	| MINUS  { res = ConnectionNode.UNDIRECTED; }
	;

returnTypes returns [ CollectNode<IdentNode> res = new CollectNode<IdentNode>() ]
	: COLON LPAREN
		( type=typeIdentUse { res.addChild(type); } ( COMMA type=typeIdentUse { res.addChild(type); } )* )?
		RPAREN
	|
	;

patternPart [ Coords pattern_coords, CollectNode<BaseNode> params, int mod, int context, String nameOfGraph ] returns [ PatternGraphNode res = null ]
	: p=PATTERN LBRACE
		n=patternBody[getCoords(p), params, mod, context, nameOfGraph] { res = n; }
		RBRACE
			{ reportWarning(getCoords(p), "separate pattern part deprecated, just merge content directly into rule/test-body"); }
	| n=patternBody[pattern_coords, params, mod, context, nameOfGraph] { res = n; }
	;

replacePart [ CollectNode<EvalStatementNode> eval, CollectNode<BaseNode> params,
              int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ]
            returns [ ReplaceDeclNode res = null ]
	: r=REPLACE ( id=rhsIdentDecl { nameOfRHS = id; } )?
		p=parameters[context, directlyNestingLHSGraph] { params = p; } 
		LBRACE
		b=replaceBody[getCoords(r), params, eval, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		RBRACE
	;

modifyPart [ CollectNode<EvalStatementNode> eval, CollectNode<IdentNode> dels,
             CollectNode<BaseNode> params, int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ]
           returns [ ModifyDeclNode res = null ]
	: m=MODIFY ( id=rhsIdentDecl { nameOfRHS = id; } )?
		p=parameters[context, directlyNestingLHSGraph] { params = p; } 
		LBRACE
		b=modifyBody[getCoords(m), eval, dels, params, context, nameOfRHS, directlyNestingLHSGraph] { res = b; }
		RBRACE
	;

evalPart [ CollectNode<EvalStatementNode> n ]
	: EVAL LBRACE
		evalBody[n]
		RBRACE
	;

evalBody [ CollectNode<EvalStatementNode> n  ]
	: ( a=assignment { n.addChild(a); } SEMI )*
	;

patternBody [ Coords coords, CollectNode<BaseNode> params, int mod, int context, String nameOfGraph ] returns [ PatternGraphNode res = null ]
	@init{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
		CollectNode<SubpatternReplNode> subpatternReplacements = new CollectNode<SubpatternReplNode>();
		CollectNode<AlternativeNode> alts = new CollectNode<AlternativeNode>();
		CollectNode<IteratedNode> iters = new CollectNode<IteratedNode>();
		CollectNode<PatternGraphNode> negs = new CollectNode<PatternGraphNode>();
		CollectNode<PatternGraphNode> idpts = new CollectNode<PatternGraphNode>();
		CollectNode<ExprNode> conds = new CollectNode<ExprNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<HomNode> homs = new CollectNode<HomNode>();
		CollectNode<ExactNode> exact = new CollectNode<ExactNode>();
		CollectNode<InducedNode> induced = new CollectNode<InducedNode>();
		NestedPatternCounters counters = new NestedPatternCounters();
		res = new PatternGraphNode(nameOfGraph, coords, connections, params, subpatterns, subpatternReplacements, 
				alts, iters, negs, idpts, conds,
				returnz, homs, exact, induced, mod, context);
	}

	: ( patternStmt[connections, subpatterns, subpatternReplacements,
			alts, iters, negs, idpts, counters, conds,
			returnz, homs, exact, induced, context, res] )*
	;

patternStmt [ CollectNode<BaseNode> conn, CollectNode<SubpatternUsageNode> subpatterns, CollectNode<SubpatternReplNode> subpatternReplacements,
			CollectNode<AlternativeNode> alts, CollectNode<IteratedNode> iters, CollectNode<PatternGraphNode> negs,
			CollectNode<PatternGraphNode> idpts, NestedPatternCounters counters, CollectNode<ExprNode> conds,
			CollectNode<ExprNode> returnz, CollectNode<HomNode> homs, CollectNode<ExactNode> exact, CollectNode<InducedNode> induced,
			int context, PatternGraphNode directlyNestingLHSGraph]
	: connectionsOrSubpattern[conn, subpatterns, subpatternReplacements, context, directlyNestingLHSGraph] SEMI
	| alt=alternative[counters.alt, context] { alts.addChild(alt); ++counters.alt; }
	| iter=iterated[counters.iter, context] { iters.addChild(iter); ++counters.iter; }
	| neg=negative[counters.neg, context] { negs.addChild(neg); ++counters.neg; }
	| idpt=independent[counters.idpt, context] { idpts.addChild(idpt); ++counters.idpt; }
	| condition[conds]
	| rets[returnz, context] SEMI
	| hom=homStatement { homs.addChild(hom); } SEMI
	| exa=exactStatement { exact.addChild(exa); } SEMI
	| ind=inducedStatement { induced.addChild(ind); } SEMI
	;

connectionsOrSubpattern [ CollectNode<BaseNode> conn, CollectNode<SubpatternUsageNode> subpatterns, CollectNode<SubpatternReplNode> subpatternReplacements, int context, PatternGraphNode directlyNestingLHSGraph ]
	: firstEdge[conn, context, directlyNestingLHSGraph] // connection starts with an edge which dangles on the left
	| firstNodeOrSubpattern[conn, subpatterns, subpatternReplacements, context, directlyNestingLHSGraph] // there's a subpattern or a connection that starts with a node
	;

firstEdge [ CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
	}

	:   ( e=forwardOrUndirectedEdgeOcc[context, direction, directlyNestingLHSGraph] { forward=true; } // get first edge
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction, directlyNestingLHSGraph] { forward=false; }
		| e=arbitraryEdgeOcc[context, directlyNestingLHSGraph] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
		nodeContinuation[e, env.getDummyNodeDecl(context, directlyNestingLHSGraph), forward, direction, conn, context, directlyNestingLHSGraph] // and continue looking for node
	;

firstNodeOrSubpattern [ CollectNode<BaseNode> conn, CollectNode<SubpatternUsageNode> subpatterns, CollectNode<SubpatternReplNode> subpatternReplacements, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		id = env.getDummyIdent();
		type = env.getNodeRoot();
		constr = TypeExprNode.getEmpty();
		annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
		CollectNode<IdentNode> subpatternConn = new CollectNode<IdentNode>();
		CollectNode<IdentNode> subpatternReplConn = new CollectNode<IdentNode>();
		BaseNode n = null;
	}

	: id=entIdentUse firstEdgeContinuation[id, conn, context, directlyNestingLHSGraph] // use of already declared node, continue looking for first edge
	| id=entIdentUse l=LPAREN subpatternConnections[subpatternReplConn] RPAREN // use of already declared subpattern
		{ subpatternReplacements.addChild(new SubpatternReplNode(id, subpatternReplConn)); }
	| id=entIdentDecl cc=COLON // node or subpattern declaration
		( // node declaration
			type=typeIdentUse
			( constr=typeConstraint )?
			( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					n = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
				} else {
					n = new NodeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
				}
			}
			firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
		| // node typeof declaration
			TYPEOF LPAREN type=entIdentUse RPAREN
			( constr=typeConstraint )?
			( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					n = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
				} else {
					n = new NodeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
				}
			}
			firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
		| // subpattern declaration
			type=patIdentUse LPAREN subpatternConnections[subpatternConn] RPAREN
			{ subpatterns.addChild(new SubpatternUsageNode(id, type, subpatternConn)); }
		)
	| ( annots=annotations { hasAnnots = true; } )?
		c=COLON // anonymous node or subpattern declaration
			( // node declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				type=typeIdentUse
				( constr=typeConstraint )?
				( LT oldid=entIdentUse GT )?
				{
					if(oldid==null) {
						n = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
					} else {
						n = new NodeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
					}
				}
				firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
			| // node typeof declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				TYPEOF LPAREN type=entIdentUse RPAREN
				( constr=typeConstraint )?
				( LT oldid=entIdentUse GT )?
				{
					if(oldid==null) {
						n = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
					} else {
						n = new NodeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
					}
				}
				firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
			| // subpattern declaration
				{ id = env.defineAnonymousEntity("subpattern", getCoords(c)); }
				type=patIdentUse LPAREN subpatternConnections[subpatternConn] RPAREN
				{ subpatterns.addChild(new SubpatternUsageNode(id, type, subpatternConn)); }
			)
			{ if (hasAnnots) { id.setAnnotations(annots); } }
	| d=DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ n = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph); }
		firstEdgeContinuation[n, conn, context, directlyNestingLHSGraph] // and continue looking for first edge
	;

nodeContinuation [ BaseNode e, BaseNode n1, boolean forward, MutableInteger direction, CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{ n2 = env.getDummyNodeDecl(context, directlyNestingLHSGraph); }

	: n2=nodeOcc[context, directlyNestingLHSGraph] // node following - get it and build connection with it, then continue with looking for follwing edge
		{
			if (direction.getValue() == ConnectionNode.DIRECTED && !forward) {
				conn.addChild(new ConnectionNode(n2, e, n1, direction.getValue()));
			} else {
				conn.addChild(new ConnectionNode(n1, e, n2, direction.getValue()));
			}
		}
		edgeContinuation[n2, conn, context, directlyNestingLHSGraph]
	|   // nothing following - build connection with edge dangeling on the right (see n2 initialization)
		{
			if (direction.getValue() == ConnectionNode.DIRECTED && !forward) {
				conn.addChild(new ConnectionNode(n2, e, n1, direction.getValue()));
			} else {
				conn.addChild(new ConnectionNode(n1, e, n2, direction.getValue()));
			}
		}
	;

firstEdgeContinuation [ BaseNode n, CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
	}

	: // nothing following? -> one single node
	{
		if (n instanceof IdentNode) {
			conn.addChild(new SingleGraphEntityNode((IdentNode)n, directlyNestingLHSGraph));
		}
		else {
			conn.addChild(new SingleNodeConnNode(n, directlyNestingLHSGraph));
		}
	}
	|   ( e=forwardOrUndirectedEdgeOcc[context, direction, directlyNestingLHSGraph] { forward=true; }
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction, directlyNestingLHSGraph] { forward=false; }
		| e=arbitraryEdgeOcc[context, directlyNestingLHSGraph] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
			nodeContinuation[e, n, forward, direction, conn, context, directlyNestingLHSGraph] // continue looking for node
	;

edgeContinuation [ BaseNode left, CollectNode<BaseNode> conn, int context, PatternGraphNode directlyNestingLHSGraph ]
	@init{
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
	}

	:   // nothing following? -> connection end reached
	|   ( e=forwardOrUndirectedEdgeOcc[context, direction, directlyNestingLHSGraph] { forward=true; }
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction, directlyNestingLHSGraph] { forward=false; }
		| e=arbitraryEdgeOcc[context, directlyNestingLHSGraph] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
			nodeContinuation[e, left, forward, direction, conn, context, directlyNestingLHSGraph] // continue looking for node
	;

nodeOcc [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	@init{
		id = env.getDummyIdent();
		annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
	}

	: e=entIdentUse { res = e; } // use of already declared node
	| id=entIdentDecl COLON co=nodeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; } // node declaration
	| ( annots=annotations { hasAnnots = true; } )?
		c=COLON // anonymous node declaration
			{ id = env.defineAnonymousEntity("node", getCoords(c)); }
			{ if (hasAnnots) { id.setAnnotations(annots); } }
			co=nodeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; }
	| d=DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ res = new NodeDeclNode(id, env.getNodeRoot(), context, TypeExprNode.getEmpty(), directlyNestingLHSGraph); }
	;

nodeTypeContinuation [ IdentNode id, int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	@init{
		type = env.getNodeRoot();
		constr = TypeExprNode.getEmpty();
	}

	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					res = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
				} else {
					res = new NodeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
				}
			}
	;

nodeDecl [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	@init{
		constr = TypeExprNode.getEmpty();
	}

	: id=entIdentDecl COLON
		( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					res = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
				} else {
					res = new NodeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
				}
			}
	;

nodeDeclParam [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	@init{
		constr = TypeExprNode.getEmpty();
	}

	: id=entIdentDecl COLON
		type=typeIdentUse
		( constr=typeConstraint )?
		( LT interfaceType=typeIdentUse GT )?
			{
				if(interfaceType==null) {
					res = new NodeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
				} else {
					res = new NodeInterfaceTypeChangeNode(id, type, context, interfaceType, directlyNestingLHSGraph);
				}
			}
	;

varDecl [ int context ] returns [ BaseNode res = env.initNode() ]
	: VAR id=entIdentDecl COLON
		(
			type=typeIdentUse
			{
				res = new VarDeclNode(id, type);
			}
		|
			MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				res = new VarDeclNode(id, MapTypeNode.getMapType(keyType, valueType));
			}
		|
			SET LT keyType=typeIdentUse GT
			{ // MAP TODO: das sollte eigentlich kein Schluesselwort sein, sondern ein Typbezeichner
				res = new VarDeclNode(id, SetTypeNode.getSetType(keyType));
			}
		)
	;


forwardOrUndirectedEdgeOcc [int context, MutableInteger direction, PatternGraphNode directlyNestingLHSGraph] returns [ BaseNode res = env.initNode() ]
	: MINUS ( e1=edgeDecl[context, directlyNestingLHSGraph] { res = e1; } | e2=entIdentUse { res = e2; } ) forwardOrUndirectedEdgeOccContinuation[direction]
	| da=DOUBLE_RARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.DIRECTED);
		}
	| mm=MINUSMINUS
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, env.getUndirectedEdgeRoot(), context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.UNDIRECTED);
		}
	;

forwardOrUndirectedEdgeOccContinuation [MutableInteger direction]
	: MINUS { direction.setValue(ConnectionNode.UNDIRECTED); }
	| RARROW { direction.setValue(ConnectionNode.DIRECTED); }
	;

backwardOrArbitraryDirectedEdgeOcc [ int context, MutableInteger direction, PatternGraphNode directlyNestingLHSGraph ] returns [ BaseNode res = env.initNode() ]
	: LARROW ( e1=edgeDecl[context, directlyNestingLHSGraph] { res = e1; } | e2=entIdentUse { res = e2; } ) backwardOrArbitraryDirectedEdgeOccContinuation[ direction ]
	| da=DOUBLE_LARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.DIRECTED);
		}
	| lr=LRARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(lr));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
			direction.setValue(ConnectionNode.ARBITRARY_DIRECTED);
		}
	;

backwardOrArbitraryDirectedEdgeOccContinuation [MutableInteger direction]
	: MINUS { direction.setValue(ConnectionNode.DIRECTED); }
	| RARROW { direction.setValue(ConnectionNode.ARBITRARY_DIRECTED); }
	;

arbitraryEdgeOcc [int context, PatternGraphNode directlyNestingLHSGraph] returns [ BaseNode res = env.initNode() ]
	: QUESTIONMINUS ( e1=edgeDecl[context, directlyNestingLHSGraph] { res = e1; } | e2=entIdentUse { res = e2; } ) MINUSQUESTION
	| q=QMMQ
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(q));
			res = new EdgeDeclNode(id, env.getArbitraryEdgeRoot(), context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		}
	;

edgeDecl [ int context, PatternGraphNode directlyNestingLHSGraph ] returns [ EdgeDeclNode res = null ]
	@init{
		id = env.getDummyIdent();
	}

	:   ( id=entIdentDecl COLON
			co=edgeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; } 
		| atCo=annotationsWithCoords
			( c=COLON
				{ id = env.defineAnonymousEntity("edge", getCoords(c)); }
				co=edgeTypeContinuation[id, context, directlyNestingLHSGraph] { res = co; } 
			|   { id = env.defineAnonymousEntity("edge", atCo.second); }
				{ res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty(), directlyNestingLHSGraph); }
			)
				{ id.setAnnotations(atCo.first); }
		| cc=COLON
			{ id = env.defineAnonymousEntity("edge", getCoords(cc)); }
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
		( LT interfaceType=typeIdentUse GT )?
			{
				if( interfaceType == null ) {
					res = new EdgeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
				} else {
					res = new EdgeInterfaceTypeChangeNode(id, type, context, interfaceType, directlyNestingLHSGraph);
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
		( LT oldid=entIdentUse GT )?
			{
				if( oldid == null ) {
					res = new EdgeDeclNode(id, type, context, constr, directlyNestingLHSGraph);
				} else {
					res = new EdgeTypeChangeNode(id, type, context, oldid, directlyNestingLHSGraph);
				}
			}
	;

subpatternConnections[CollectNode<IdentNode> subpatternConn]
	: ( id=entIdentUse { subpatternConn.addChild(id); } (COMMA id=entIdentUse { subpatternConn.addChild(id); } )* )?
	;

homStatement returns [ HomNode res = null ]
	: h=HOM {res = new HomNode(getCoords(h)); }
		LPAREN id=entIdentUse { res.addChild(id); }
			(COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
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

replaceBody [ Coords coords, CollectNode<BaseNode> params, CollectNode<EvalStatementNode> eval, int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ] returns [ ReplaceDeclNode res = null ]
	@init{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
		CollectNode<SubpatternReplNode> subpatternReplacements = new CollectNode<SubpatternReplNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		GraphNode graph = new GraphNode(nameOfRHS.toString(), coords, connections, params, subpatterns, subpatternReplacements, returnz, imperativeStmts, context);
		res = new ReplaceDeclNode(nameOfRHS, graph, eval);
	}

	: ( replaceStmt[coords, connections, subpatterns, subpatternReplacements, eval, context, directlyNestingLHSGraph] 
		| rets[returnz, context] SEMI
		| execStmt[imperativeStmts] SEMI
		| emitStmt[imperativeStmts] SEMI
		)*
	;

replaceStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<SubpatternUsageNode> subpatterns,
 		CollectNode<SubpatternReplNode> subpatternReplacements, CollectNode<EvalStatementNode> eval, int context, PatternGraphNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[connections, subpatterns, subpatternReplacements, context, directlyNestingLHSGraph] SEMI
	| evalPart[eval]
	;

modifyBody [ Coords coords, CollectNode<EvalStatementNode> eval, CollectNode<IdentNode> dels, CollectNode<BaseNode> params, int context, IdentNode nameOfRHS, PatternGraphNode directlyNestingLHSGraph ] returns [ ModifyDeclNode res = null ]
	@init{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
		CollectNode<SubpatternReplNode> subpatternReplacements = new CollectNode<SubpatternReplNode>();
		CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
		GraphNode graph = new GraphNode(nameOfRHS.toString(), coords, connections, params, subpatterns, subpatternReplacements, returnz, imperativeStmts, context);
		res = new ModifyDeclNode(nameOfRHS, graph, eval, dels);
	}

	: ( modifyStmt[coords, connections, subpatterns, subpatternReplacements, eval, dels, context, directlyNestingLHSGraph] 
		| rets[returnz, context] SEMI
		| execStmt[imperativeStmts] SEMI
		| emitStmt[imperativeStmts] SEMI
		)*
	;

modifyStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<SubpatternUsageNode> subpatterns,
 		CollectNode<SubpatternReplNode> subpatternReplacements,
		CollectNode<EvalStatementNode> eval, CollectNode<IdentNode> dels, int context, PatternGraphNode directlyNestingLHSGraph ]
	: connectionsOrSubpattern[connections, subpatterns, subpatternReplacements, context, directlyNestingLHSGraph] SEMI
	| deleteStmt[dels] SEMI
	| evalPart[eval]
	;

alternative [ int altCount, int context ] returns [ AlternativeNode alt = null ]
	: a=ALTERNATIVE { alt = new AlternativeNode(getCoords(a)); } LBRACE
		( alternativeCase[alt, altCount, context] ) +
		RBRACE
	;	
	
alternativeCase [ AlternativeNode alt, int altCount, int context ]
	@init{
		int mod = 0;
		CollectNode<EvalStatementNode> eval = new CollectNode<EvalStatementNode>();
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<RhsDeclNode> rightHandSides = new CollectNode<RhsDeclNode>();
	}
	
	: id=altIdentDecl l=LBRACE pushScopeStr["alt_"+altCount+id.toString(), getCoords(l)]
		left=patternBody[getCoords(l), new CollectNode<BaseNode>(), mod, context, id.toString()]
		(
			rightReplace=replacePart[eval, new CollectNode<BaseNode>(), context|BaseNode.CONTEXT_RHS, id, left]
				{
					rightHandSides.addChild(rightReplace);
				}
			| rightModify=modifyPart[eval, dels, new CollectNode<BaseNode>(), context|BaseNode.CONTEXT_RHS, id, left]
				{
					rightHandSides.addChild(rightModify);
				}
		) ?
		RBRACE popScope	{ alt.addChild(new AlternativeCaseNode(id, left, rightHandSides)); }
	;

iterated [ int iterCount, int context ] returns [ IteratedNode res = null ]
	@init{
		CollectNode<EvalStatementNode> eval = new CollectNode<EvalStatementNode>();
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
		CollectNode<RhsDeclNode> rightHandSides = new CollectNode<RhsDeclNode>();
		IdentNode iterName = IdentNode.getInvalid();
		int minMatches = -1;
		int maxMatches = -1;
	}

	: (i=ITERATED { minMatches = 0; maxMatches = 0; } 
	  | i=OPTIONAL { minMatches = 0; maxMatches = 1; }
	  | i=MULTIPLE { minMatches = 1; maxMatches = 0; }
	  )
	    ( in=iterIdentDecl { iterName = in; } | { iterName = new IdentNode(env.define(ParserEnvironment.ITERATEDS, "iter_"+iterCount, getCoords(i))); } )
		LBRACE pushScopeStr["iter_"+iterCount, getCoords(i)]
		left=patternBody[getCoords(i), new CollectNode<BaseNode>(), 0, context, "iter_"+iterCount]
		(
			rightReplace=replacePart[eval, new CollectNode<BaseNode>(), context|BaseNode.CONTEXT_RHS, iterName, left]
				{
					rightHandSides.addChild(rightReplace);
				}
			| rightModify=modifyPart[eval, dels, new CollectNode<BaseNode>(), context|BaseNode.CONTEXT_RHS, iterName, left]
				{
					rightHandSides.addChild(rightModify);
				}
		) ?				
		RBRACE popScope { res = new IteratedNode(iterName, left, rightHandSides, minMatches, maxMatches); }
	;

negative [ int negCount, int context ] returns [ PatternGraphNode res = null ]
	@init{
		int mod = 0;
	}
	
	: n=NEGATIVE LBRACE pushScopeStr["neg"+negCount, getCoords(n)]
			( ( PATTERNPATH { mod = PatternGraphNode.MOD_PATTERNPATH_LOCKED; }
			| PATTERN { mod = PatternGraphNode.MOD_PATTERN_LOCKED; } ) SEMI )*
			b=patternBody[getCoords(n), new CollectNode<BaseNode>(), mod, 
				context|BaseNode.CONTEXT_NEGATIVE, "negative"+negCount] { res = b; } 
		RBRACE popScope
	;

independent [ int idptCount, int context ] returns [ PatternGraphNode res = null ]
	@init{
		int mod = 0;
	}
	
	: i=INDEPENDENT LBRACE pushScopeStr["idpt"+idptCount, getCoords(i)]
			( ( PATTERNPATH { mod = PatternGraphNode.MOD_PATTERNPATH_LOCKED; }
			| PATTERN { mod = PatternGraphNode.MOD_PATTERN_LOCKED; } ) SEMI )*
			b=patternBody[getCoords(i), new CollectNode<BaseNode>(), mod,
				context|BaseNode.CONTEXT_INDEPENDENT, "independent"+idptCount] { res = b; } 
		RBRACE popScope
	;

condition [ CollectNode<ExprNode> conds ]
	: COND LBRACE
			( e=expr[false] { conds.addChild(e); } SEMI )* 
		RBRACE
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

paramListOfExecEntIdentUseOrEntIdentDecl[CollectNode<BaseNode> res]
	: child=execEntIdentUseOrEntIdentDecl { res.addChild(child); }
		( COMMA child=execEntIdentUseOrEntIdentDecl { res.addChild(child); } )*
	;

execEntIdentUseOrEntIdentDecl returns [BaseNode res = null]
	:
		id=entIdentUse { res = id; } // var of node, edge, or basic type
	|
		id=entIdentDecl COLON type=typeIdentUse // node decl
		{
			res = new ExecVarDeclNode(id, type);
		}
	|
		MINUS id=entIdentDecl COLON type=typeIdentUse  RARROW // edge decl
		{
			res = new ExecVarDeclNode(id, type);
		}
	;

execStmt[CollectNode<BaseNode> imperativeStmts]
    @init{ ExecNode exec = null; }
    
	: e=EXEC pushScopeStr["exec_", getCoords(e)] { exec = new ExecNode(getCoords(e)); } LPAREN xgrs[exec] RPAREN { imperativeStmts.addChild(exec); } popScope
	;

emitStmt[CollectNode<BaseNode> imperativeStmts]
	@init{ EmitNode emit = null; boolean isPre = false;}
	
	: (e=EMIT | e=EMITPRE { isPre = true; } | e=EMITPOST)
		{ emit = new EmitNode(getCoords(e), isPre); }
		LPAREN
			exp=expr[false] { emit.addChild(exp); }
			( COMMA exp=expr[false] { emit.addChild(exp); } )*
		RPAREN
		{ imperativeStmts.addChild(emit); }
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


//////////////////////////////////////////
// Embedded XGRS
//////////////////////////////////////////


// Due to a bug in ANTLR it is not possible to use the obvious "xgrs3 ( (DOLLAR)? LAND xgrs2 )?"
xgrs[ExecNode xg]
/*
	: xgrs2 ( DOLLAR LOR xgrs | LOR xgrs | )
*/
	: xgrs6[xg] (	DOLLAR (LOR {xg.append("||");} |LAND {xg.append("&&");} |BOR {xg.append("|");} |BXOR {xg.append("^");} |BAND {xg.append("&");} ) xgrs[xg]
	        		|      (LOR {xg.append("||");} |LAND {xg.append("&&");} |BOR {xg.append("|");} |BXOR {xg.append("^");} |BAND {xg.append("&");} ) xgrs[xg]
	        		|
	            )
	;

/*
xgrs2
	: xgrs3 ( DOLLAR LAND xgrs2 | LAND xgrs2 | )
	;

xgrs3
	: xgrs4 ( DOLLAR BOR  xgrs3 | BOR  xgrs3 | )
	;

xgrs4
	: xgrs5 ( DOLLAR BXOR  xgrs4 | BXOR  xgrs4 | )
	;

xgrs5
	: xgrs6 ( DOLLAR BAND  xgrs5 | BAND  xgrs5 | )
	;
*/

xgrs6[ExecNode xg]
	: NOT {xg.append("!");} xgrs6[xg]
	| iterSequence[xg]
	;

iterSequence[ExecNode xg]
	: simpleSequence[xg]
		(
			rsn=rangeSpec
			{
				if(rsn != null)
				{
					if(rsn.getLower() == rsn.getUpper())
					{
						if(rsn.getLower() != 1)
							xg.append("[" + rsn.getLower() + "]");
					}
					else xg.append("["+rsn.getLower()+":"+rsn.getUpper()+"]");
				}
			}
		|
			STAR { xg.append("*"); }
		|
			PLUS { xg.append("+"); }
		)
	;

simpleSequence[ExecNode xg]
	@init{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
	}
	
	: LPAREN {xg.append("(");}
		(
			(entIdentUse COMMA | entIdentUse RPAREN ASSIGN | entIdentUse COLON typeIdentUse (COMMA | RPAREN) | MINUS) =>
				paramListOfExecEntIdentUseOrEntIdentDecl[returns]
					{
						for(Iterator<BaseNode> i =returns.getChildren().iterator(); i.hasNext();) {
							BaseNode r = i.next();
							if(r instanceof ExecVarDeclNode) {
								ExecVarDeclNode decl = (ExecVarDeclNode)r;
								xg.append(decl.getIdentNode().getIdent());
								xg.append(':');
								xg.append(decl.typeUnresolved);
								xg.addVarDecls(decl);
							} else
								xg.append(r);
							if(i.hasNext()) xg.append(",");
						}
					}
				RPAREN ASSIGN {xg.append(")=");} parallelCallRule[xg, returns]
			| xgrs[xg] RPAREN {xg.append(")");}
		)
	| (entIdentUse ASSIGN | entIdentDecl COLON | MINUS) =>
	lhs=execEntIdentUseOrEntIdentDecl ASSIGN
		{
			if(lhs instanceof ExecVarDeclNode) {
				ExecVarDeclNode decl = (ExecVarDeclNode)lhs;
				xg.append(decl.getIdentNode().getIdent());
				xg.append(':');
				xg.append(decl.typeUnresolved);
				xg.addVarDecls(decl);
			}
			else {
				xg.append(lhs);
			}
			xg.append('=');
		}
		(
			id=entIdentUse {
				xg.append(id);
				xg.addGraphElementUsageOutsideOfCall(id);
			}
		|
			TRUE { xg.append("true"); }
		|
			FALSE { xg.append("false"); }
		|
			LPAREN { xg.append('('); } xgrs[xg] RPAREN { xg.append(')'); }
		)
	| parallelCallRule[xg, returns]
	| TRUE { xg.append("true"); }
	| FALSE { xg.append("false"); }
	| LT {xg.append("<");} xgrs[xg] GT {xg.append(">");}
	;

parallelCallRule[ExecNode xg, CollectNode<BaseNode> returns]
	@init{
		numRndChoose = 0;
	}
	
	: (LBRACK) => LBRACK {xg.append("[");} callRule[xg, returns] RBRACK {xg.append("]");}
	| (DOLLAR (NUM_INTEGER)? LBRACK) =>
	  DOLLAR {xg.append("$");}
	  (numRndChoose=integerConst {xg.append(numRndChoose);})?
	  LBRACK {xg.append("[");}
	  callRule[xg, returns] RBRACK {xg.append("]");}
	| callRule[xg, returns]
	;

callRule[ExecNode xg, CollectNode<BaseNode> returns]
	@init{
		CollectNode<BaseNode> params = new CollectNode<BaseNode>();
	}
	
	: ( | MOD { xg.append("\%"); } | MOD QUESTION { xg.append("\%?"); } | QUESTION { xg.append("?"); } | QUESTION MOD { xg.append("?\%"); } )
		id=actionIdentUse {xg.append(id);}
		(LPAREN ruleParams[params] RPAREN)?
		{
			xg.addCallAction(new CallActionNode(id.getCoords(), id, params, returns));
			if(params.getChildren().iterator().hasNext()) {
				xg.append("(");
				for(Iterator<BaseNode> i = params.getChildren().iterator(); i.hasNext();) {
					BaseNode p = i.next();
					xg.append(p);
					if(i.hasNext()) xg.append(", ");
				}
				xg.append(")");
			}
		}
	;

ruleParam[CollectNode<BaseNode> parameters]
	: exp=identExpr { parameters.addChild(exp); }
	| exp=constant { parameters.addChild(exp); }
	| MINUS
		(
			i=NUM_INTEGER
				{ parameters.addChild(new IntConstNode(getCoords(i), Integer.parseInt("-" + i.getText(), 10))); }
		|	f=NUM_FLOAT
				{ parameters.addChild(new FloatConstNode(getCoords(f), Float.parseFloat("-" + f.getText()))); }
		| 	d=NUM_DOUBLE
				{ parameters.addChild(new DoubleConstNode(getCoords(d), Double.parseDouble("-" + d.getText()))); }
		)
	;

ruleParams[CollectNode<BaseNode> parameters]
	: ruleParam[parameters]	( COMMA ruleParam[parameters] )*
	;



////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Types
////////////////////////////////////////////////////////////////////////////////////////////////////////////////



textTypes returns [ ModelNode model = null ]
	@init{
		CollectNode<ModelNode> modelChilds = new CollectNode<ModelNode>();
		CollectNode<IdentNode> types = new CollectNode<IdentNode>();
		IdentNode id = env.getDummyIdent();

		String modelName = Util.removeFileSuffix(Util.removePathPrefix(getFilename()), "gm");

		id = new IdentNode(
			env.define(ParserEnvironment.MODELS, modelName,
			new de.unika.ipd.grgen.parser.Coords(0, 0, getFilename())));
	}

	:   ( m=MODEL ignoredToken=ident SEMI
			{ reportWarning(getCoords(m), "keyword \"model\" is deprecated"); }
		)?
		( usingDecl[modelChilds] )?
		typeDecls[types] EOF
		{
			if(modelChilds.getChildren().size() == 0)
				modelChilds.addChild(env.getStdModel());
			model = new ModelNode(id, types, modelChilds);
		}
	;

typeDecls [ CollectNode<IdentNode> types ]
	: (type=typeDecl { types.addChild(type); } )*
	;

typeDecl returns [ IdentNode res = env.getDummyIdent() ]
	: d=classDecl { res = d; } 
	| d=enumDecl { res = d; } 
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
	  	ext=edgeExtends[id, arbitrary, undirected] cas=connectAssertions pushScope[id]
		(
			LBRACE body=classBody[id] RBRACE
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
		popScope
  ;

nodeClassDecl[int modifiers] returns [ IdentNode res = env.getDummyIdent() ]
	: 	NODE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
	  	ext=nodeExtends[id] pushScope[id]
		(
			LBRACE body=classBody[id] RBRACE
		|	SEMI
			{ body = new CollectNode<BaseNode>(); }
		)
		{
			NodeTypeNode nt = new NodeTypeNode(ext, body, modifiers, externalName);
			id.setDecl(new TypeDeclNode(id, nt));
			res = id;
		}
		popScope
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
	: src=typeIdentUse srcRange=rangeSpec RARROW
		tgt=typeIdentUse tgtRange=rangeSpec
			{ c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange)); }
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

classBody [IdentNode clsId] returns [ CollectNode<BaseNode> c = new CollectNode<BaseNode>() ]
	:	(
			(
				b1=basicAndContainerDecl[c]
			|
				b3=initExpr { c.addChild(b3); }
			|
				b4=constrDecl[clsId] { c.addChild(b4); }
			) SEMI
		)*
	;

enumDecl returns [ IdentNode res = env.getDummyIdent() ]
	@init{
		CollectNode<EnumItemNode> c = new CollectNode<EnumItemNode>();
	}

	: ENUM id=typeIdentDecl pushScope[id]
		LBRACE enumList[id, c]
		{
			TypeNode enumType = new EnumTypeNode(c);
			id.setDecl(new TypeDeclNode(id, enumType));
			res = id;
		}
		RBRACE popScope
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
			)
		)
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

initMapExpr [IdentNode id, MapTypeNode mapType] returns [ MapInitNode res = null ]
	: l=LBRACE { res = new MapInitNode(getCoords(l), id, mapType); }
	          item1=mapItem { res.addMapItem(item1); }
	  ( COMMA item2=mapItem { res.addMapItem(item2); } )*
	  RBRACE
	;

initSetExpr [IdentNode id, SetTypeNode setType] returns [ SetInitNode res = null ]
	: l=LBRACE { res = new SetInitNode(getCoords(l), id, setType); }	
	          item1=setItem { res.addSetItem(item1); }
	  ( COMMA item2=setItem { res.addSetItem(item2); } )*
	  RBRACE
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
// Base
////////////////////////////////////////////////////////////////////////////////////////////////////////////////



nonKeywordLiterals returns [ Token t = null ]
	: v=VISITED { t = v; }
	;

ident returns [ Token t = null ]
	: i=IDENT { t = i; }
	| l=nonKeywordLiterals { l.setType(IDENT); t = l; }
	;

memberIdent returns [ Token t = null ]
	: i=IDENT { t = i; }
	| l=nonKeywordLiterals { l.setType(IDENT); t = l; }
	| r=REPLACE { r.setType(IDENT); t = r; }             // HACK: For string replace function... better choose another name?
	; 

pushScope [IdentNode name]
	@init{ env.pushScope(name); }

	:
	;

pushScopeStr [String str, Coords coords]
	@init{ env.pushScope(new IdentNode(new Symbol.Definition(env.getCurrScope(), coords, new Symbol(str, SymbolTable.getInvalid())))); }

	:
	;

popScope
	@init{ env.popScope(); }

	:
	;

typeIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=identDecl[ParserEnvironment.TYPES] { res = i; }
	;

rhsIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=identDecl[ParserEnvironment.REPLACES] { res = i; }
	;

entIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=identDecl[ParserEnvironment.ENTITIES] { res = i; }
	;

actionIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=identDecl[ParserEnvironment.ACTIONS] { res = i; }
	;

altIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=identDecl[ParserEnvironment.ALTERNATIVES] { res = i; }
	;

iterIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=identDecl[ParserEnvironment.ITERATEDS] { res = i; }
	;

patIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: i=identDecl[ParserEnvironment.PATTERNS] { res = i; }
	;

typeIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=identUse[ParserEnvironment.TYPES] { res = i; }
	;

rhsIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=identUse[ParserEnvironment.REPLACES] { res = i; }
	;

entIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=identUse[ParserEnvironment.ENTITIES] { res = i; }
	;

actionIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=identUse[ParserEnvironment.ACTIONS] { res = i; }
	;

iterIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=identUse[ParserEnvironment.ITERATEDS] { res = i; }
	;

altIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=identUse[ParserEnvironment.ALTERNATIVES] { res = i; }
	;

patIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=identUse[ParserEnvironment.PATTERNS] { res = i; }
	;

annotations returns [ Annotations annots = new DefaultAnnotations() ]
	: LBRACK keyValuePairs[annots] RBRACK
	;

annotationsWithCoords
	returns [
		Pair<DefaultAnnotations, de.unika.ipd.grgen.parser.Coords> res =
			new Pair<DefaultAnnotations, de.unika.ipd.grgen.parser.Coords>(
				new DefaultAnnotations(), Coords.getInvalid()
			)
	]
	: l=LBRACK keyValuePairs[res.first] RBRACK
		{ res.second = getCoords(l); }
	;

keyValuePairs [ Annotations annots ]
	: keyValuePair[annots] (COMMA keyValuePair[annots])*
	;

keyValuePair [ Annotations annots ]
	: id=ident
		(
			ASSIGN c=constant
			{ annots.put(id.getText(), ((ConstNode) c).getValue()); }
		|
			{ annots.put(id.getText(), true); }
		)
	;

identList [ Collection<String> strings ]
	: fid=ident { strings.add(fid.getText()); }
		( COMMA sid=ident { strings.add(sid.getText()); } )*
	;

/**
 * declaration of an identifier
 */
identDecl [ int symTab ] returns [ IdentNode res = env.getDummyIdent() ]
	: i=ident
		{ if(i!=null) res = new IdentNode(env.define(symTab, i.getText(), getCoords(i))); }
		( annots=annotations { res.setAnnotations(annots); } )?
	;

/**
 * Represents the usage of an identifier.
 * It is checked, whether the identifier is declared. The IdentNode
 * created by the definition is returned.
 */
identUse [ int symTab ] returns [ IdentNode res = env.getDummyIdent() ]
	: i=ident
		{ if(i!=null) res = new IdentNode(env.occurs(symTab, i.getText(), getCoords(i))); }
	;

memberIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: i=memberIdent
		{ if(i!=null) res = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i))); }
	;

//////////////////////////////////////////
// Expressions
//////////////////////////////////////////


assignment returns [ EvalStatementNode res = null ]
	: tgt=qualIdent a=ASSIGN e=expr[false] //'false' because this rule is not used for the assignments in enum item decls
		{ res = new AssignNode(getCoords(a), tgt, e); }
	|
	  tgt2=visitedExpr a=ASSIGN e=expr[false]
		{ res = new AssignNode(getCoords(a), tgt2, e); }
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
	| e=primaryExpr[inEnumInit] (e=selectorExpr[e, inEnumInit])* { res = e; }
	; 

primaryExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: e=visitedExpr { res = e; }
	| e=nameOf { res = e; }
	| e=identExpr { res = e; }
	| e=constant { res = e; }
	| e=enumItemExpr { res = e; }
	| e=typeOf { res = e; }
	| e=initMapOrSetExpr { res = e; }
	| LPAREN e=expr[inEnumInit] { res = e; } RPAREN
	| p=PLUSPLUS { reportError(getCoords(p), "increment operator \"++\" not supported"); }
	| q=MINUSMINUS { reportError(getCoords(q), "decrement operator \"--\" not supported"); }
	;

visitedExpr returns [ ExprNode res = env.initExprNode() ]
	: v=VISITED LPAREN elem=entIdentUse 
		( COMMA idExpr=expr[false] RPAREN
			{ res = new VisitedNode(getCoords(v), idExpr, elem); }
		| RPAREN
			{ res = new VisitedNode(getCoords(v), new IntConstNode(getCoords(v), 0), elem); }
		)
	;

nameOf returns [ ExprNode res = env.initExprNode() ]
	: n=NAMEOF LPAREN (id=entIdentUse)? RPAREN { res = new NameofNode(getCoords(n), id); }
	;

typeOf returns [ ExprNode res = env.initExprNode() ]
	: t=TYPEOF LPAREN id=entIdentUse RPAREN { res = new TypeofNode(getCoords(t), id); }
	;
	
initMapOrSetExpr returns [ ExprNode res = env.initExprNode() ]
	: MAP LT keyType=typeIdentUse COMMA valueType=typeIdentUse GT e1=initMapExpr[null, MapTypeNode.getMapType(keyType, valueType)] { res = e1; }
	| SET LT valueType=typeIdentUse GT e2=initSetExpr[null, SetTypeNode.getSetType(valueType)] { res = e2; }
	| (LBRACE expr[false] RARROW) => e1=initMapExpr[null, null] { res = e1; }
	| (LBRACE) => e2=initSetExpr[null, null] { res = e2; }
	;
	
constant returns [ ExprNode res = env.initExprNode() ]
	: i=NUM_INTEGER
		{ res = new IntConstNode(getCoords(i), Integer.parseInt(i.getText(), 10)); }
	| h=NUM_HEX
		{ res = new IntConstNode(getCoords(h), Integer.parseInt(h.getText().substring(2), 16)); }
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

identExpr returns [ ExprNode res = env.initExprNode() ]
	@init{ IdentNode id; }

	: i=ident
		{
			// Entity names can overwrite type names
			if(env.test(ParserEnvironment.ENTITIES, i.getText()) || !env.test(ParserEnvironment.TYPES, i.getText()))
				id = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
			else
				id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
			res = new IdentExprNode(id);
		}
	;

qualIdent returns [ QualIdentNode res = null ]
	: currentLeft=entIdentUse
		(d=DOT id=entIdentUse
			{
				res = new QualIdentNode(getCoords(d), currentLeft, id);
			}
		)
	;

enumItemAcc returns [ EnumExprNode res = null ]
	: tid=typeIdentUse d=DOUBLECOLON id=entIdentUse
	{ res = new EnumExprNode(getCoords(d), tid, id); }
	;

enumItemExpr returns [ ExprNode res = env.initExprNode() ]
	: n=enumItemAcc { res = new DeclExprNode(n); }
	;

selectorExpr [ ExprNode target, boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	:	l=LBRACK key=expr[inEnumInit] RBRACK { res = new MapAccessExprNode(getCoords(l), target, key); }
	|	d=DOT id=memberIdentUse
		(
			params=paramExprs[inEnumInit]
			{
				res = new MethodInvocationExprNode(target, id, params);
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
		lower = 1; upper = 1;
		de.unika.ipd.grgen.parser.Coords coords = de.unika.ipd.grgen.parser.Coords.getInvalid();
		// range allows [*], [+], [c:*], [c], [c:d]
	}

	:
		(
			l=LBRACK { coords = getCoords(l); }
			(
				STAR { lower=0; upper=RangeSpecNode.UNBOUND; }
			|
				PLUS { lower=1; upper=RangeSpecNode.UNBOUND; }
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
RBRACE			:	'}'		;
COLON			:	':'		;
DOUBLECOLON     :   '::'    ;
COMMA			:	','		;
DOT 			:	'.'		;
ASSIGN			:	'='		;
EQUAL			:	'=='	;
NOT         	:	'!'		;
TILDE			:	'~'		;
NOT_EQUAL		:	'!='	;
SL				:	'<<'	;
SR				:	'>>'	;
BSR				:	'>>>'	;
DIV				:	'/'		;
PLUS			:	'+'		;
PLUSASSIGN		:	'+='	;
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
BACKSLASH		:	'\\'	;
PLUSPLUS		:	'++'	;
MINUSMINUS		:	'--'	;
DOLLAR          :   '$'     ;

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

fragment NUM_INTEGER: ;
fragment NUM_FLOAT: ;
fragment NUM_DOUBLE: ;
NUMBER
   : ('0'..'9')+
   ( ('.' '0'..'9') => '.' ('0'..'9')+
     (   ('f'|'F')    { $type = NUM_FLOAT; }
       | ('d'|'D')?   { $type = NUM_DOUBLE; }
     )
   | { $type = NUM_INTEGER; }
   )
   ;

NUM_HEX
	: '0' 'x' ('0'..'9' | 'a' .. 'f' | 'A' .. 'F')+
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
  	$channel=HIDDEN;
	String file = s.getText();
	file = file.substring(1,file.length()-1);
	env.pushFile(this, new File(file));
  }
  ;

ABSTRACT : 'abstract';
ACTIONS : 'actions';
ALTERNATIVE : 'alternative';
ARBITRARY : 'arbitrary';
CLASS : 'class';
COND : 'if';
CONNECT : 'connect';
CONST : 'const';
DELETE : 'delete';
DIRECTED : 'directed';
DPO : 'dpo';
EDGE : 'edge';
EMIT : 'emit';
EMITPRE : 'emitpre';
EMITPOST : 'emitpost';
ENUM : 'enum';
EVAL : 'eval';
EXACT : 'exact';
EXEC : 'exec';
EXTENDS : 'extends';
FALSE : 'false';
HOM : 'hom';
IN : 'in';
INDEPENDENT : 'independent';
INDUCED : 'induced';
ITERATED : 'iterated';
MAP : 'map';
MODEL : 'model';
MODIFY : 'modify';
MULTIPLE : 'multiple';
NAMEOF : 'nameof';
NEGATIVE : 'negative';
NODE : 'node';
NULL : 'null';
OPTIONAL : 'optional';
PATTERN : 'pattern';
PATTERNPATH : 'patternpath';
REPLACE : 'replace';
RETURN : 'return';
RULE : 'rule';
SET : 'set';
TEST : 'test';
TRUE : 'true';
TYPEOF : 'typeof';
UNDIRECTED : 'undirected';
USING : 'using';
VAR : 'var';
VISITED : 'visited';

IDENT : ('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')* ;
