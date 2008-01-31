header {
/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

/**
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski
 * @version $Id$
 */
	package de.unika.ipd.grgen.parser.antlr;

	import java.util.Iterator;
	import java.util.List;
	import java.util.LinkedList;
	import java.util.Map;
	import java.util.HashMap;
	import java.util.Collection;
	import java.io.DataInputStream;
	import java.io.FileInputStream;
	import java.io.FileNotFoundException;
	import java.io.File;

	import de.unika.ipd.grgen.parser.*;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.util.report.*;
	import de.unika.ipd.grgen.util.*;
	import de.unika.ipd.grgen.Main;

	import antlr.*;
}


/**
 * GRGen grammar
 * @version 1.5
 * @author Sebastian Hack, Rubino Geiss, Veit Batz, Edgar Jakumeit, Sebastian Buchwald
 */
class GRActionsParser extends GRBaseParser;

options {
	k=2;
	codeGenMakeSwitchThreshold = 2;
	codeGenBitsetTestThreshold = 3;
	defaultErrorHandler = true;
	buildAST = false;
	importVocab = GRBase;
}

/**
 * Build a main node.
 * It has a collect node with the decls as child
 */
text returns [ BaseNode main = env.initNode() ]
	{
		CollectNode modelChilds = new CollectNode();
		CollectNode<IdentNode> patternChilds = new CollectNode<IdentNode>();
		CollectNode<IdentNode> actionChilds = new CollectNode<IdentNode>();
		IdentNode id;
		String actionsName = Util.getActionsNameFromFilename(getFilename());
		id = new IdentNode(
			env.define(ParserEnvironment.ENTITIES, actionsName,
				new de.unika.ipd.grgen.parser.Coords(0, 0, getFilename())));
		modelChilds.addChild(env.getStdModel());
	}

	: (
		( a:ACTIONS i:IDENT
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

	( patternOrActionDecls[patternChilds, actionChilds] EOF )?
		{
			main = new UnitNode(id, getFilename(), modelChilds, patternChilds, actionChilds);
			env.getCurrScope().leaveScope();
		}
	;

identList [ Collection<String> strings ]
	: fid:IDENT { strings.add(fid.getText()); }
		( COMMA sid:IDENT { strings.add(sid.getText()); } )*
	;

usingDecl [ CollectNode modelChilds ]
	{ Collection<String> modelNames = new LinkedList<String>(); }

	: u:USING identList[modelNames] SEMI
		{
			for(Iterator<String> it = modelNames.iterator(); it.hasNext();)
			{
				String modelName = it.next();
				File modelFile = env.findModel(modelName);
				if ( modelFile == null ) {
					reportError(getCoords(u), "model \"" + modelName + "\" could not be found");
				} else {
					BaseNode model;
					model = env.parseModel(modelFile);
					modelChilds.addChild(model);
				}
			}
		}
	;

patternOrActionDecls[ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds ]
	{ int mod = 0; }

	: ( mod=patternModifiers patternOrActionDecl[patternChilds, actionChilds, mod] )+
	;

patternModifiers returns [ int res = 0 ]
	: ( res = patternModifier[ res ] )*
	;

patternModifier [ int mod ] returns [ int res = 0 ]
	: i:INDUCED { if((mod & PatternGraphNode.MOD_INDUCED)!=0) {
	              reportError(getCoords(i), "\"induced\" modifier already declared");
	              }
	              res = mod | PatternGraphNode.MOD_INDUCED;
	            }
	| e:EXACT { if((mod & PatternGraphNode.MOD_EXACT)!=0) {
	              reportError(getCoords(e), "\"exact\" modifier already declared");
	              }
	              res = mod | PatternGraphNode.MOD_EXACT;
	          }
	| d:DPO { if((mod & PatternGraphNode.MOD_DPO)!=0) {
	              reportError(getCoords(d), "\"dpo\" modifier already declared");
	              }
	              res = mod | PatternGraphNode.MOD_DPO;
	        }
	;

patternOrActionDecl [ CollectNode<IdentNode> patternChilds, CollectNode<IdentNode> actionChilds, int mod ]
	{
		IdentNode id;
		PatternGraphNode left;
		GraphNode right;
		CollectNode params, ret;
		CollectNode negs = new CollectNode();
		CollectNode eval = new CollectNode();
		CollectNode dels = new CollectNode();
	}

	: t:TEST id=actionIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS] ret=returnTypes LBRACE
		left=patternPart[getCoords(t), negs, mod, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, "test "+id.toString()]
			{
				id.setDecl(new TestDeclNode(id, left, negs, params, ret));
				actionChilds.addChild(id);
			}
		RBRACE popScope
		{
			if((mod & PatternGraphNode.MOD_DPO)!=0) {
				reportError(getCoords(t), "no \"dpo\" modifier allowed");
			}
		}
	| r:RULE id=actionIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS] ret=returnTypes LBRACE
		left=patternPart[getCoords(r), negs, mod, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, "rule "+id.toString()]
		( right=replacePart[eval, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, "rule "+id.toString()]
			{
				id.setDecl(new RuleDeclNode(id, left, right, negs, eval, params, ret));
				actionChilds.addChild(id);
			}
		| right=modifyPart[eval, dels, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, "rule "+id.toString()]
			{
				id.setDecl(new ModifyRuleDeclNode(id, left, right, negs, eval, params, ret, dels));
				actionChilds.addChild(id);
			}
		)
		RBRACE popScope
	| p:PATTERN id=typeIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS] LBRACE
		left=patternPart[getCoords(p), negs, mod, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS, "pattern "+id.toString()]
		( 
			{
				id.setDecl(new TestDeclNode(id, left, negs, params, new CollectNode()));
				patternChilds.addChild(id);
				if((mod & PatternGraphNode.MOD_DPO)!=0) {
					reportError(getCoords(t), "no \"dpo\" modifier allowed");
				}
			}
		| right=replacePart[eval, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, "pattern "+id.toString()]
			{
				id.setDecl(new RuleDeclNode(id, left, right, negs, eval, params, new CollectNode()));
				patternChilds.addChild(id);
			}
		| right=modifyPart[eval, dels, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, "pattern "+id.toString()]
			{
				id.setDecl(new ModifyRuleDeclNode(id, left, right, negs, eval, params, new CollectNode(), dels));
				patternChilds.addChild(id);
			}
		)
		RBRACE popScope
	;

parameters [ int context ] returns [ CollectNode res = new CollectNode() ]
	: LPAREN (paramList[res, context])? RPAREN
	|
	;

paramList [ CollectNode params, int context ]
	{ BaseNode p; }

	: p=param[context] { params.addChild(p); } ( COMMA p=param[context] { params.addChild(p); } )*
	;

param [ int context ] returns [ BaseNode res = env.initNode() ]
	: MINUS res=edgeDecl[context] RARROW
	| res=nodeDecl[context]
	;

returnTypes returns [ CollectNode res = new CollectNode() ]
	{ BaseNode type; }

	: COLON LPAREN
		( type=typeIdentUse { res.addChild(type); } ( COMMA type=typeIdentUse { res.addChild(type); } )* )?
		RPAREN
	|
	;

patternPart [ Coords pattern_coords, CollectNode negs, int mod, int context, String nameOfGraph ] returns [ PatternGraphNode res = null ]
	: p:PATTERN LBRACE
		res=patternBody[getCoords(p), negs, mod, context, nameOfGraph]
		RBRACE
			{ reportWarning(getCoords(p), "separate pattern part deprecated, just merge content directly into rule/test-body"); }
	| res=patternBody[pattern_coords, negs, mod, context, nameOfGraph]
	;

replacePart [ CollectNode eval, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	: r:REPLACE LBRACE
		res=replaceBody[getCoords(r), eval, context, nameOfGraph]
		RBRACE
	;

modifyPart [ CollectNode eval, CollectNode<IdentNode> dels, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	: r:MODIFY LBRACE
		res=modifyBody[getCoords(r), eval, dels, context, nameOfGraph]
		RBRACE
	;

evalPart [ CollectNode n ]
	: EVAL LBRACE
		evalBody[n]
		RBRACE
	;

evalBody [ CollectNode n  ]
	{ AssignNode a; }

	: ( a=assignment { n.addChild(a); } SEMI )*
	;

patternBody [ Coords coords, CollectNode negs, int mod, int context, String nameOfGraph ] returns [ PatternGraphNode res = null ]
	{
		CollectNode connections = new CollectNode();
		CollectNode conditions = new CollectNode();
		CollectNode returnz = new CollectNode();
		CollectNode homs = new CollectNode();
		CollectNode exact = new CollectNode();
		CollectNode induced = new CollectNode();
		CollectNode subpatterns = new CollectNode();
		res = new PatternGraphNode(nameOfGraph+".pattern", coords, connections, subpatterns, conditions, returnz, homs, exact, induced, mod, context);
		int negCounter = 0;
	}

	: ( negCounter = patternStmt[connections, subpatterns, conditions, negs, negCounter, returnz, homs, exact, induced, context, nameOfGraph+".pattern"] )*
	;

patternStmt [ CollectNode conn, CollectNode subpatterns, CollectNode cond,
	CollectNode negs, int negCount, CollectNode returnz, CollectNode homs, CollectNode exact, CollectNode induced, int context, String nameOfGraph ]
	returns [ int newNegCount ]
	{
		int mod = 0;
		ExprNode e;
		PatternGraphNode neg;
		HomNode hom;
		ExactNode exa;
		InducedNode ind;
		//nesting of negative Parts is not allowed.
		CollectNode negsInNegs = new CollectNode();
		newNegCount = negCount;
	}

	: connectionsOrSubpattern[conn, subpatterns, context] SEMI
		// TODO: insert mod=patternModifiers iff nesting of negative parts is allowed
	| p:NEGATIVE pushScopeStr[ "neg" + negCount, getCoords(p) ] LBRACE
		neg=patternBody[getCoords(p), negsInNegs, mod, context, nameOfGraph+".negative"]
			{
				newNegCount = negCount + 1;
				negs.addChild(neg);
			}
		RBRACE popScope
			{
				if(negsInNegs.getChildren().size() != 0) {
					reportError(getCoords(p), "Nesting of negative parts not allowed");
				}
			}
	| COND e=expr[false] { cond.addChild(e); } SEMI //'false' means that expr is not an enum item initializer
	| COND LBRACE
		( e=expr[false] { cond.addChild(e); } SEMI )*
		RBRACE
	| rets[returnz, context] SEMI
	| hom=homStatement { homs.addChild(hom); } SEMI
	| exa=exactStatement { exact.addChild(exa); } SEMI
	| ind=inducedStatement { induced.addChild(ind); } SEMI
	;

connectionsOrSubpattern [ CollectNode conn, CollectNode subpatterns, int context ]
	: firstEdge[conn, context] // connection starts with an edge which dangles on the left
	| firstNodeOrSubpattern[conn, subpatterns, context] // there's a subpattern or a connection that starts with a node
	;

firstEdge [ CollectNode conn, int context ]
	{
		BaseNode e;
		boolean forward = true;
	}

	:   ( e=forwardEdgeOcc[context] { forward=true; } // get first edge
		| e=backwardEdgeOcc[context] { forward=false; }
		)
			nodeContinuation[e, env.getDummyNodeDecl(context), forward, conn, context] // and continue looking for node
	;

firstNodeOrSubpattern [ CollectNode conn, CollectNode subpatterns, int context ]
	{
		IdentNode id = env.getDummyIdent();
		IdentNode type = env.getNodeRoot();
		IdentNode oldid = null;
		TypeExprNode constr = TypeExprNode.getEmpty();
		Annotations annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
		CollectNode<IdentNode> subpatternConn = new CollectNode<IdentNode>();
		CollectNode<IdentNode> subpatternReplConn = new CollectNode<IdentNode>();
		BaseNode n = null;
	}

	: id=entIdentUse firstEdgeContinuation[id, conn, context] // use of already declared node, continue looking for first edge
	| id=entIdentUse l:LPAREN subpatternConnections[subpatternReplConn] RPAREN // use of already declared subpattern
		{ subpatterns.addChild(new SubpatternReplNode(id, subpatternReplConn)); }
		{ reportError(getCoords(l), "subpatterns not yet supported"); }
	| id=entIdentDecl cc:COLON // node or subpattern declaration
		( // node declaration
			type=typeIdentUse
			( constr=typeConstraint )?
			( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					n = new NodeDeclNode(id, type, context, constr);
				} else {
					n = new NodeTypeChangeNode(id, type, context, oldid);
				}
			}
			firstEdgeContinuation[n, conn, context] // and continue looking for first edge
		| // node typeof declaration
			TYPEOF LPAREN type=entIdentUse RPAREN
			( constr=typeConstraint )?
			( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					n = new NodeDeclNode(id, type, context, constr);
				} else {
					n = new NodeTypeChangeNode(id, type, context, oldid);
				}
			}
			firstEdgeContinuation[n, conn, context] // and continue looking for first edge
		| // subpattern declaration
			type=typeIdentUse LPAREN subpatternConnections[subpatternConn] RPAREN
			{ subpatterns.addChild(new SubpatternUsageNode(id, type, subpatternConn)); }
		)
	| ( annots=annotations { hasAnnots = true; } )?
		c:COLON // anonymous node or subpattern declaration
			( // node declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				type=typeIdentUse
				( constr=typeConstraint )?
				( LT oldid=entIdentUse GT )?
				{
					if(oldid==null) {
						n = new NodeDeclNode(id, type, context, constr);
					} else {
						n = new NodeTypeChangeNode(id, type, context, oldid);
					}
				}
				firstEdgeContinuation[n, conn, context] // and continue looking for first edge
			| // node typeof declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				TYPEOF LPAREN type=entIdentUse RPAREN
				( constr=typeConstraint )?
				( LT oldid=entIdentUse GT )?
				{
					if(oldid==null) {
						n = new NodeDeclNode(id, type, context, constr);
					} else {
						n = new NodeTypeChangeNode(id, type, context, oldid);
					}
				}
				firstEdgeContinuation[n, conn, context] // and continue looking for first edge
			| // subpattern declaration
				{ id = env.defineAnonymousEntity("subpattern", getCoords(c)); }
				type=typeIdentUse LPAREN subpatternConnections[subpatternConn] RPAREN
				{ subpatterns.addChild(new SubpatternUsageNode(id, type, subpatternConn)); }
			)
			{ if (hasAnnots) { id.setAnnotations(annots); } }
	| d:DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ n = new NodeDeclNode(id, type, context, constr); }
		firstEdgeContinuation[n, conn, context] // and continue looking for first edge
	;

nodeContinuation [ BaseNode e, BaseNode n1, boolean forward, CollectNode conn, int context ]
	{ BaseNode n2 = env.getDummyNodeDecl(context); }

	: n2=nodeOcc[context] // node following - get it and build connection with it, then continue with looking for follwing edge
		{
			if (forward) {
				conn.addChild(new ConnectionNode(n1, e, n2));
			} else {
				conn.addChild(new ConnectionNode(n2, e, n1));
			}
		}
		edgeContinuation[n2, conn, context]
	|   // nothing following - build connection with edge dangeling on the right (see n2 initialization)
		{
			if (forward) {
				conn.addChild(new ConnectionNode(n1, e, n2));
			} else {
				conn.addChild(new ConnectionNode(n2, e, n1));
			}
		}
	;

firstEdgeContinuation [ BaseNode n, CollectNode conn, int context ]
	{
		BaseNode e;
		boolean forward = true;
	}

	:   { conn.addChild(new SingleNodeConnNode(n)); } // nothing following? -> one single node
	|   ( e=forwardEdgeOcc[context] { forward=true; }
		| e=backwardEdgeOcc[context] { forward=false; }
		)
			nodeContinuation[e, n, forward, conn, context] // continue looking for node
	;

edgeContinuation [ BaseNode left, CollectNode conn, int context ]
	{
		BaseNode e;
		boolean forward = true;
	}

	:   // nothing following? -> connection end reached
	|   ( e=forwardEdgeOcc[context] { forward=true; }
		| e=backwardEdgeOcc[context] { forward=false; }
		)
			nodeContinuation[e, left, forward, conn, context] // continue looking for node
	;

nodeOcc [ int context ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id = env.getDummyIdent();
		Annotations annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
	}

	: res=entIdentUse // use of already declared node
	| id=entIdentDecl COLON res=nodeTypeContinuation[id, context] // node declaration
	| ( annots=annotations { hasAnnots = true; } )?
		c:COLON // anonymous node declaration
			{ id = env.defineAnonymousEntity("node", getCoords(c)); }
			{ if (hasAnnots) { id.setAnnotations(annots); } }
			res=nodeTypeContinuation[id, context]
	| d:DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ res = new NodeDeclNode(id, env.getNodeRoot(), context, TypeExprNode.getEmpty()); }
	;

nodeTypeContinuation [ IdentNode id, int context ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode type = env.getNodeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
		IdentNode oldid = null;
	}

	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					res = new NodeDeclNode(id, type, context, constr);
				} else {
					res = new NodeTypeChangeNode(id, type, context, oldid);
				}
			}
	;

nodeDecl [ int context ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id, type;
		TypeExprNode constr = TypeExprNode.getEmpty();
		IdentNode oldid = null;
	}

	: id=entIdentDecl COLON
		( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					res = new NodeDeclNode(id, type, context, constr);
				} else {
					res = new NodeTypeChangeNode(id, type, context, oldid);
				}
			}
	;

forwardEdgeOcc [ int context ] returns [ BaseNode res = env.initNode() ]
	: MINUS ( res=edgeDecl[context] | res=entIdentUse) RARROW
	| mm:DOUBLE_RARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, env.getEdgeRoot(), context, TypeExprNode.getEmpty());
		}
	;

backwardEdgeOcc [ int context ] returns [ BaseNode res = env.initNode() ]
	: LARROW ( res=edgeDecl[context] | res=entIdentUse ) MINUS
	| mm:DOUBLE_LARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, env.getEdgeRoot(), context, TypeExprNode.getEmpty());
		}
	;

edgeDecl [ int context ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id = env.getDummyIdent();
		Annotations annots = env.getEmptyAnnotations();
		Pair<DefaultAnnotations, de.unika.ipd.grgen.parser.Coords> atCo;
	}

	:   ( id=entIdentDecl COLON
			res=edgeTypeContinuation[id, context]
		| atCo=annotationsWithCoords
			( c:COLON
				{ id = env.defineAnonymousEntity("edge", getCoords(c)); }
				res=edgeTypeContinuation[id, context]
			|   { id = env.defineAnonymousEntity("edge", atCo.second); }
				{ res = new EdgeDeclNode(id, env.getEdgeRoot(), context, TypeExprNode.getEmpty()); }
			)
				{ id.setAnnotations(atCo.first); }
		| cc:COLON
			{ id = env.defineAnonymousEntity("edge", getCoords(cc)); }
			res=edgeTypeContinuation[id, context]
		)
	;

edgeTypeContinuation [ IdentNode id, int context ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode type = env.getNodeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
		IdentNode oldid = null;
	}

	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
		( LT oldid=entIdentUse GT )?
			{
				if( oldid == null ) {
					res = new EdgeDeclNode(id, type, context, constr);
				} else {
					res = new EdgeTypeChangeNode(id, type, context, oldid);
				}
			}
	;

subpatternConnections[CollectNode<IdentNode> subpatternConn]
	{ IdentNode id; }

	: ( id=entIdentUse { subpatternConn.addChild(id); } (COMMA id=entIdentUse { subpatternConn.addChild(id); } )* )?
	;
	
homStatement returns [ HomNode res = null ]
	{
		IdentNode id;
	}

	: h:HOM {res = new HomNode(getCoords(h)); }
		LPAREN id=entIdentUse { res.addChild(id); }
			(COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
	;

exactStatement returns [ ExactNode res = null ]
	{
		IdentNode id;
	}

	: e:EXACT {res = new ExactNode(getCoords(e)); }
		LPAREN id=entIdentUse { res.addChild(id); }
			(COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
	;

inducedStatement returns [ InducedNode res = null ]
	{
		IdentNode id;
	}

	: i:INDUCED {res = new InducedNode(getCoords(i)); }
		LPAREN id=entIdentUse { res.addChild(id); }
			(COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
	;

replaceBody [ Coords coords, CollectNode eval, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	{
		CollectNode connections = new CollectNode();
		CollectNode subpatterns = new CollectNode();
		CollectNode returnz = new CollectNode();
		CollectNode imperativeStmts = new CollectNode();

		res = new GraphNode(nameOfGraph+".replace", coords, connections, subpatterns, returnz, imperativeStmts, context);
	}

	: ( replaceStmt[coords, connections, subpatterns, returnz, eval, imperativeStmts, context] )*
	;

replaceStmt [ Coords coords, CollectNode connections, CollectNode subpatterns, CollectNode returnz,
		CollectNode eval, CollectNode imperativeStmts, int context ]
	: connectionsOrSubpattern[connections, subpatterns, context] SEMI
	| rets[returnz, context] SEMI
	| evalPart[eval]
	| execStmt[imperativeStmts] SEMI
	| emitStmt[imperativeStmts] SEMI
	;

modifyBody [ Coords coords, CollectNode eval, CollectNode<IdentNode> dels, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	{
		CollectNode connections = new CollectNode();
		CollectNode subpatterns = new CollectNode();
		CollectNode returnz = new CollectNode();
		CollectNode imperativeStmts = new CollectNode();

		EmitNode es = null;
		res = new GraphNode(nameOfGraph+".modify", coords, connections, subpatterns, returnz, imperativeStmts, context);
	}

	: ( modifyStmt[coords, connections, subpatterns, returnz, eval, dels, imperativeStmts, context] )*
	;

modifyStmt [ Coords coords, CollectNode connections, CollectNode subpatterns, CollectNode returnz,
		CollectNode eval, CollectNode<IdentNode> dels, CollectNode imperativeStmts, int context ]
	: connectionsOrSubpattern[connections, subpatterns, context] SEMI
	| rets[returnz, context] SEMI
	| deleteStmt[dels] SEMI
	| evalPart[eval]
	| execStmt[imperativeStmts] SEMI
	| emitStmt[imperativeStmts] SEMI
	;

rets[CollectNode res, int context]
	{
		IdentNode id;
		boolean multipleReturns = ! res.getChildren().isEmpty();
	}

	: r:RETURN
		{
			if ( multipleReturns ) {
				reportError(getCoords(r), "multiple occurence of return statement in one rule");
			}
			if ( (context & BaseNode.CONTEXT_ACTION_OR_PATTERN) == BaseNode.CONTEXT_PATTERN) {
				reportError(getCoords(r), "return statement only allowed in actions, not in pattern type declarations");
			}
		}
		LPAREN id=entIdentUse { if ( !multipleReturns ) res.addChild(id); }
		( COMMA id=entIdentUse { if ( !multipleReturns ) res.addChild(id); } )*
		RPAREN
			{ res.setCoords(getCoords(r)); }
	;

deleteStmt[CollectNode<IdentNode> res]
	{ IdentNode id; }

	: DELETE LPAREN paramListOfEntIdentUse[res] RPAREN
	;

paramListOfEntIdentUse[CollectNode<IdentNode> res]
	{ IdentNode id; }
	: id=entIdentUse { res.addChild(id); }	( COMMA id=entIdentUse { res.addChild(id); } )*
	;

execStmt[CollectNode imperativeStmts]
    {
    	ExecNode exec = null;
    }
	: e:EXEC { exec = new ExecNode(getCoords(e)); } LPAREN xgrs[exec] RPAREN { imperativeStmts.addChild(exec); }
	;

emitStmt[CollectNode imperativeStmts]
	{
		EmitNode emit = null;
		ExprNode exp = null;
	}
	: e:EMIT { emit = new EmitNode(getCoords(e)); }
		LPAREN
			exp=expr[false] { emit.addChild(exp); }
			( c:COMMA exp=expr[false] { emit.addChild(exp); } )*
		RPAREN
		{ imperativeStmts.addChild(emit); }
	;

// Due to a bug in ANTLR it is not possible to use the obvious "xgrs3 ( (DOLLAR)? LAND xgrs2 )?"
xgrs[ExecNode xg]
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
	{
		RangeSpecNode rsn = null;
	}
	: simpleSequence[xg] rsn=rangeSpec { if(rsn != null) xg.append("["+rsn.getLower()+":"+rsn.getUpper()+"]"); }
	;

simpleSequence[ExecNode xg]
	{
		CollectNode<IdentNode> results = new CollectNode<IdentNode>();
	}
	: LPAREN {xg.append("(");}
		(
			(entIdentUse COMMA|entIdentUse RPAREN ASSIGN) =>
				paramListOfEntIdentUse[results]
					{
						for(Iterator i =results.getChildren().iterator(); i.hasNext();) {
								xg.append(i.next());
								if(i.hasNext()) xg.append(",");
							}
					}
				RPAREN ASSIGN {xg.append(")=");} parallelCallRule[xg]
			| xgrs[xg] RPAREN {xg.append(")");}
		)
	| parallelCallRule[xg]
	| LT {xg.append("<");} xgrs[xg] GT {xg.append(">");}
	;

parallelCallRule[ExecNode xg]
	: LBRACK {xg.append("[");} callRule[xg] RBRACK {xg.append("]");}
	| callRule[xg]
	;

callRule[ExecNode xg]
	{
		CollectNode<IdentNode> params = new CollectNode<IdentNode>();
		IdentNode id;
	}
	: id=entIdentUse {xg.append(id);}
		(LPAREN paramListOfEntIdentUse[params]
			{
				xg.append("(");
				for(Iterator<IdentNode> i =params.getChildren().iterator(); i.hasNext();) {
					BaseNode p = i.next();
					xg.addParameter(p);
					xg.append(p);
					if(i.hasNext()) xg.append(",");
				}
				xg.append(")");
			}
		RPAREN)?
	;

typeConstraint returns [ TypeExprNode constr = null ]
	: BACKSLASH constr=typeUnaryExpr
	;

typeAddExpr returns [ TypeExprNode res = null ]
	{ IdentNode typeUse; TypeExprNode op; }

	: typeUse=typeIdentUse { res = new TypeConstraintNode(typeUse); }
		(t:PLUS op=typeUnaryExpr
			{ res = new TypeBinaryExprNode(getCoords(t), TypeExprNode.UNION, res, op); }
		)*
	;

typeUnaryExpr returns [ TypeExprNode res = null ]
	{ IdentNode typeUse; }
	: typeUse=typeIdentUse { res = new TypeConstraintNode(typeUse); }
	| LPAREN res=typeAddExpr RPAREN
	;





