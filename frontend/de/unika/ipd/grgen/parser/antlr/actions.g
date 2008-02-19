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
	import java.util.LinkedList;
	import java.util.Map;
	import java.util.HashMap;
	import java.util.Collection;
	import java.io.File;

	import de.unika.ipd.grgen.parser.*;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.util.*;
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
		CollectNode<BaseNode> modelChilds = new CollectNode<BaseNode>();
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

usingDecl [ CollectNode<BaseNode> modelChilds ]
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
		CollectNode<BaseNode> params;
		CollectNode<IdentNode> ret;
		CollectNode<AssignNode> eval = new CollectNode<AssignNode>();
		CollectNode<IdentNode> dels = new CollectNode<IdentNode>();
	}

	: t:TEST id=actionIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS] ret=returnTypes LBRACE
		left=patternPart[getCoords(t), mod, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.toString()]
			{
				id.setDecl(new TestDeclNode(id, left, params, ret));
				actionChilds.addChild(id);
			}
		RBRACE popScope
		{
			if((mod & PatternGraphNode.MOD_DPO)!=0) {
				reportError(getCoords(t), "no \"dpo\" modifier allowed");
			}
		}
	| r:RULE id=actionIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS] ret=returnTypes LBRACE
		left=patternPart[getCoords(r), mod, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_LHS, id.toString()]
		( right=replacePart[eval, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id.toString()]
			{
				id.setDecl(new RuleDeclNode(id, left, right, eval, params, ret));
				actionChilds.addChild(id);
			}
		| right=modifyPart[eval, dels, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, id.toString()]
			{
				id.setDecl(new ModifyRuleDeclNode(id, left, right, eval, params, ret, dels));
				actionChilds.addChild(id);
			}
		)
		RBRACE popScope
	| p:PATTERN id=typeIdentDecl pushScope[id] params=parameters[BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS] LBRACE
		left=patternPart[getCoords(p), mod, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_LHS, id.toString()]
		(
			{
				id.setDecl(new TestDeclNode(id, left, params, new CollectNode<IdentNode>()));
				patternChilds.addChild(id);
				if((mod & PatternGraphNode.MOD_DPO)!=0) {
					reportError(getCoords(t), "no \"dpo\" modifier allowed");
				}
			}
		| right=replacePart[eval, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id.toString()]
			{
				id.setDecl(new RuleDeclNode(id, left, right, eval, params, new CollectNode<IdentNode>()));
				patternChilds.addChild(id);
			}
		| right=modifyPart[eval, dels, BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, id.toString()]
			{
				id.setDecl(new ModifyRuleDeclNode(id, left, right, eval, params, new CollectNode<IdentNode>(), dels));
				patternChilds.addChild(id);
			}
		)
		RBRACE popScope
	;

parameters [ int context ] returns [ CollectNode<BaseNode> res = new CollectNode<BaseNode>() ]
	: LPAREN (paramList[res, context])? RPAREN
	|
	;

paramList [ CollectNode<BaseNode> params, int context ]
	{ BaseNode p; }

	: p=param[context] { params.addChild(p); } ( COMMA p=param[context] { params.addChild(p); } )*
	;

param [ int context ] returns [ BaseNode res = null ]
	{
		int direction;
		EdgeDeclNode edge = null;
		NodeDeclNode node = null;
	}

	: MINUS edge=edgeDecl[context] direction = forwardOrUndirectedEdgeParam
	{
		BaseNode dummy = env.getDummyNodeDecl(context);
		res = new ConnectionNode(dummy, edge, dummy, direction);
	}

	| node=nodeDecl[context]
	{
		res = new SingleNodeConnNode(node); 
	}
	;

forwardOrUndirectedEdgeParam returns [ int res = ConnectionNode.ARBITRARY ]
	: RARROW { res = ConnectionNode.DIRECTED; }
	| MINUS  { res = ConnectionNode.UNDIRECTED; }
	;

returnTypes returns [ CollectNode<IdentNode> res = new CollectNode<IdentNode>() ]
	{ IdentNode type; }

	: COLON LPAREN
		( type=typeIdentUse { res.addChild(type); } ( COMMA type=typeIdentUse { res.addChild(type); } )* )?
		RPAREN
	|
	;

patternPart [ Coords pattern_coords, int mod, int context, String nameOfGraph ] returns [ PatternGraphNode res = null ]
	: p:PATTERN LBRACE
		res=patternBody[getCoords(p), mod, context, nameOfGraph]
		RBRACE
			{ reportWarning(getCoords(p), "separate pattern part deprecated, just merge content directly into rule/test-body"); }
	| res=patternBody[pattern_coords, mod, context, nameOfGraph]
	;

replacePart [ CollectNode<AssignNode> eval, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	: r:REPLACE LBRACE
		res=replaceBody[getCoords(r), eval, context, nameOfGraph]
		RBRACE
	;

modifyPart [ CollectNode<AssignNode> eval, CollectNode<IdentNode> dels, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	: r:MODIFY LBRACE
		res=modifyBody[getCoords(r), eval, dels, context, nameOfGraph]
		RBRACE
	;

evalPart [ CollectNode<AssignNode> n ]
	: EVAL LBRACE
		evalBody[n]
		RBRACE
	;

evalBody [ CollectNode<AssignNode> n  ]
	{ AssignNode a; }

	: ( a=assignment { n.addChild(a); } SEMI )*
	;

patternBody [ Coords coords, int mod, int context, String nameOfGraph ] returns [ PatternGraphNode res = null ]
	{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<BaseNode> subpatterns = new CollectNode<BaseNode>();
		CollectNode<AlternativeNode> alts = new CollectNode<AlternativeNode>();
		CollectNode<PatternGraphNode> negs = new CollectNode<PatternGraphNode>();
		CollectNode<ExprNode> conditions = new CollectNode<ExprNode>();
		CollectNode<IdentNode> returnz = new CollectNode<IdentNode>();
		CollectNode<HomNode> homs = new CollectNode<HomNode>();
		CollectNode<ExactNode> exact = new CollectNode<ExactNode>();
		CollectNode<InducedNode> induced = new CollectNode<InducedNode>();
		res = new PatternGraphNode(nameOfGraph, coords, connections, subpatterns, alts, negs, conditions,
				returnz, homs, exact, induced, mod, context);
	}

	: ( patternStmt[connections, subpatterns, alts, negs, conditions,
			 returnz, homs, exact, induced, context] )*
	;

patternStmt [ CollectNode<BaseNode> conn, CollectNode<BaseNode> subpatterns, CollectNode<AlternativeNode> alts,
			CollectNode<PatternGraphNode> negs, CollectNode<ExprNode> cond,
			CollectNode<IdentNode> returnz, CollectNode<HomNode> homs, CollectNode<ExactNode> exact, CollectNode<InducedNode> induced,
			int context]
	{
		AlternativeNode alt;
		int altCounter = 0;
		PatternGraphNode neg;
		int negCounter = 0;
		int mod = 0; // TODO: insert mod=patternModifiers iff nesting of negative parts is allowed
		ExprNode e;
		HomNode hom;
		ExactNode exa;
		InducedNode ind;
	}

	: connectionsOrSubpattern[conn, subpatterns, context] SEMI
	| a:ALTERNATIVE LBRACE alt=alternative[getCoords(a), altCounter, context] { alts.addChild(alt); ++altCounter; } RBRACE
	| neg=negative[negCounter, context] { negs.addChild(neg); ++negCounter; }
	| COND e=expr[false] { cond.addChild(e); } SEMI //'false' means that expr is not an enum item initializer
	| COND LBRACE ( e=expr[false] { cond.addChild(e); } SEMI )* RBRACE
	| rets[returnz, context] SEMI
	| hom=homStatement { homs.addChild(hom); } SEMI
	| exa=exactStatement { exact.addChild(exa); } SEMI
	| ind=inducedStatement { induced.addChild(ind); } SEMI
	;

connectionsOrSubpattern [ CollectNode<BaseNode> conn, CollectNode<BaseNode> subpatterns, int context ]
	: firstEdge[conn, context] // connection starts with an edge which dangles on the left
	| firstNodeOrSubpattern[conn, subpatterns, context] // there's a subpattern or a connection that starts with a node
	;

firstEdge [ CollectNode<BaseNode> conn, int context ]
	{
		BaseNode e;
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
	}

	:   ( e=forwardOrUndirectedEdgeOcc[context, direction] { forward=true; } // get first edge
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction] { forward=false; }
		| e=arbitraryEdgeOcc[context] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
		nodeContinuation[e, env.getDummyNodeDecl(context), forward, direction, conn, context] // and continue looking for node
	;

firstNodeOrSubpattern [ CollectNode<BaseNode> conn, CollectNode<BaseNode> subpatterns, int context ]
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

nodeContinuation [ BaseNode e, BaseNode n1, boolean forward, MutableInteger direction, CollectNode<BaseNode> conn, int context ]
	{ BaseNode n2 = env.getDummyNodeDecl(context); }

	: n2=nodeOcc[context] // node following - get it and build connection with it, then continue with looking for follwing edge
		{
			if (direction.getValue() == ConnectionNode.DIRECTED && !forward) {
				conn.addChild(new ConnectionNode(n2, e, n1, direction.getValue()));
			} else {
				conn.addChild(new ConnectionNode(n1, e, n2, direction.getValue()));
			}
		}
		edgeContinuation[n2, conn, context]
	|   // nothing following - build connection with edge dangeling on the right (see n2 initialization)
		{
			if (direction.getValue() == ConnectionNode.DIRECTED && !forward) {
				conn.addChild(new ConnectionNode(n2, e, n1, direction.getValue()));
			} else {
				conn.addChild(new ConnectionNode(n1, e, n2, direction.getValue()));
			}
		}
	;

firstEdgeContinuation [ BaseNode n, CollectNode<BaseNode> conn, int context ]
	{
		BaseNode e;
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
	}

	:   { conn.addChild(new SingleNodeConnNode(n)); } // nothing following? -> one single node
	|   ( e=forwardOrUndirectedEdgeOcc[context, direction] { forward=true; }
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction] { forward=false; }
		| e=arbitraryEdgeOcc[context] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
			nodeContinuation[e, n, forward, direction, conn, context] // continue looking for node
	;

edgeContinuation [ BaseNode left, CollectNode<BaseNode> conn, int context ]
	{
		BaseNode e;
		boolean forward = true;
		MutableInteger direction = new MutableInteger(ConnectionNode.ARBITRARY);
	}

	:   // nothing following? -> connection end reached
	|   ( e=forwardOrUndirectedEdgeOcc[context, direction] { forward=true; }
		| e=backwardOrArbitraryDirectedEdgeOcc[context, direction] { forward=false; }
		| e=arbitraryEdgeOcc[context] { forward=false; direction.setValue(ConnectionNode.ARBITRARY);}
		)
			nodeContinuation[e, left, forward, direction, conn, context] // continue looking for node
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

nodeDecl [ int context ] returns [ NodeDeclNode res = null ]
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

forwardOrUndirectedEdgeOcc [int context, MutableInteger direction] returns [ BaseNode res = env.initNode() ]
	: MINUS ( res=edgeDecl[context] | res=entIdentUse) forwardOrUndirectedEdgeOccContinuation[ direction ]
	| da:DOUBLE_RARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty());
			direction.setValue(ConnectionNode.DIRECTED);
		}
	| mm:MINUSMINUS
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, env.getUndirectedEdgeRoot(), context, TypeExprNode.getEmpty());
			direction.setValue(ConnectionNode.UNDIRECTED);
		}
	;

forwardOrUndirectedEdgeOccContinuation [MutableInteger direction]
	: MINUS { direction.setValue(ConnectionNode.UNDIRECTED); }
	| RARROW { direction.setValue(ConnectionNode.DIRECTED); }
	;

backwardOrArbitraryDirectedEdgeOcc [ int context, MutableInteger direction ] returns [ BaseNode res = env.initNode() ]
	: LARROW ( res=edgeDecl[context] | res=entIdentUse ) backwardOrArbitraryDirectedEdgeOccContinuation[ direction ]
	| da:DOUBLE_LARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(da));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty());
			direction.setValue(ConnectionNode.DIRECTED);
		}
	| lr:LRARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(lr));
			res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty());
			direction.setValue(ConnectionNode.ARBITRARY_DIRECTED);
		}
	;

backwardOrArbitraryDirectedEdgeOccContinuation [MutableInteger direction]
	: MINUS { direction.setValue(ConnectionNode.DIRECTED); }
	| RARROW { direction.setValue(ConnectionNode.ARBITRARY_DIRECTED); }
	;

arbitraryEdgeOcc [int context] returns [ BaseNode res = env.initNode() ]
	: QUESTIONMINUS ( res=edgeDecl[context] | res=entIdentUse) MINUSQUESTION
	| q:QMMQ
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(q));
			res = new EdgeDeclNode(id, env.getArbitraryEdgeRoot(), context, TypeExprNode.getEmpty());
		}
	;

edgeDecl [ int context ] returns [ EdgeDeclNode res = null ]
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
				{ res = new EdgeDeclNode(id, env.getDirectedEdgeRoot(), context, TypeExprNode.getEmpty()); }
			)
				{ id.setAnnotations(atCo.first); }
		| cc:COLON
			{ id = env.defineAnonymousEntity("edge", getCoords(cc)); }
			res=edgeTypeContinuation[id, context]
		)
	;

edgeTypeContinuation [ IdentNode id, int context ] returns [ EdgeDeclNode res = null ]
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

replaceBody [ Coords coords, CollectNode<AssignNode> eval, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<BaseNode> subpatterns = new CollectNode<BaseNode>();
		CollectNode<IdentNode> returnz = new CollectNode<IdentNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();

		res = new GraphNode(nameOfGraph, coords, connections, subpatterns, returnz, imperativeStmts, context);
	}

	: ( replaceStmt[coords, connections, subpatterns, returnz, eval, imperativeStmts, context] )*
	;

replaceStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<BaseNode> subpatterns, CollectNode<IdentNode> returnz,
		CollectNode<AssignNode> eval, CollectNode<BaseNode> imperativeStmts, int context ]
	: connectionsOrSubpattern[connections, subpatterns, context] SEMI
	| rets[returnz, context] SEMI
	| evalPart[eval]
	| execStmt[imperativeStmts] SEMI
	| emitStmt[imperativeStmts] SEMI
	;

modifyBody [ Coords coords, CollectNode<AssignNode> eval, CollectNode<IdentNode> dels, int context, String nameOfGraph ] returns [ GraphNode res = null ]
	{
		CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
		CollectNode<BaseNode> subpatterns = new CollectNode<BaseNode>();
		CollectNode<IdentNode> returnz = new CollectNode<IdentNode>();
		CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();

		EmitNode es = null;
		res = new GraphNode(nameOfGraph, coords, connections, subpatterns, returnz, imperativeStmts, context);
	}

	: ( modifyStmt[coords, connections, subpatterns, returnz, eval, dels, imperativeStmts, context] )*
	;

modifyStmt [ Coords coords, CollectNode<BaseNode> connections, CollectNode<BaseNode> subpatterns, CollectNode<IdentNode> returnz,
		CollectNode<AssignNode> eval, CollectNode<IdentNode> dels, CollectNode<BaseNode> imperativeStmts, int context ]
	: connectionsOrSubpattern[connections, subpatterns, context] SEMI
	| rets[returnz, context] SEMI
	| deleteStmt[dels] SEMI
	| evalPart[eval]
	| execStmt[imperativeStmts] SEMI
	| emitStmt[imperativeStmts] SEMI
	;

alternative [ Coords coords, int altCount, int context ] returns [ AlternativeNode alt = new AlternativeNode(coords) ]
	{
		IdentNode id;
		PatternGraphNode left;
		int mod = 0;
	}
	: ( id=entIdentDecl l:LBRACE pushScopeStr["alt"+altCount, getCoords(l)]
		left=patternBody[getCoords(l), mod, context, id.toString()]
		RBRACE popScope	{ alt.addChild(left); }
	  ) *
	;

negative [ int negCount, int context ] returns [ PatternGraphNode res = null ]
	{
		int mod = 0;
	}
	: n:NEGATIVE LBRACE pushScopeStr["neg"+negCount, getCoords(n)]
		res=patternBody[getCoords(n), mod, context, "negative"+negCount]
		RBRACE popScope
	;

rets[CollectNode<IdentNode> res, int context]
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

paramListOfEntIdentUseOrEntIdentDecl[CollectNode<BaseNode> res]
	{ BaseNode child; }
	: child=entIdentUseOrEntIdentDecl { res.addChild(child); }
		( COMMA child=entIdentUseOrEntIdentDecl { res.addChild(child); } )*
	;

entIdentUseOrEntIdentDecl returns [BaseNode res = null]
	{ IdentNode id, type; }
	:
	(
		id=entIdentUse { res = id; }
	|
		id=entIdentDecl COLON type=typeIdentUse
			{
				res = new NodeDeclNode(id, type, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, TypeExprNode.getEmpty());
			}
	|
		MINUS id=entIdentDecl COLON type=typeIdentUse  RARROW
		{
			res = new EdgeDeclNode(id, type, BaseNode.CONTEXT_ACTION|BaseNode.CONTEXT_RHS, TypeExprNode.getEmpty());
		}
	)
	;

execStmt[CollectNode<BaseNode> imperativeStmts]
    {
    	ExecNode exec = null;
    }
	: e:EXEC pushScopeStr["exec_", getCoords(e)] { exec = new ExecNode(getCoords(e)); } LPAREN xgrs[exec] RPAREN { imperativeStmts.addChild(exec); } popScope
	;

emitStmt[CollectNode<BaseNode> imperativeStmts]
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
	{
		RangeSpecNode rsn = null;
	}
	: simpleSequence[xg] rsn=rangeSpec
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
	;

simpleSequence[ExecNode xg]
	{
		CollectNode<BaseNode> returns = new CollectNode<BaseNode>();
		IdentNode id;
	}
	: LPAREN {xg.append("(");}
		(
			(entIdentUse COMMA|entIdentUse RPAREN ASSIGN|entIdentUse COLON) =>
				paramListOfEntIdentUseOrEntIdentDecl[returns]
					{
						for(Iterator<BaseNode> i =returns.getChildren().iterator(); i.hasNext();) {
								BaseNode r = i.next();
								xg.append(r);
								if(i.hasNext()) xg.append(",");
							}
					}
				RPAREN ASSIGN {xg.append(")=");} parallelCallRule[xg, returns]
			| xgrs[xg] RPAREN {xg.append(")");}
		)
	| (entIdentUse ASSIGN | entIdentDecl COLON | MINUS entIdentDecl) =>
	entIdentUseOrEntIdentDecl ASSIGN
		(
			id=entIdentUse { xg.append(id); }
		|
			TRUE { xg.append("true"); }
		|
			FALSE { xg.append("false"); }
		|
			LPAREN xgrs[xg] RPAREN
		)
	| parallelCallRule[xg, returns]
	| TRUE { xg.append("true"); }
	| FALSE { xg.append("false"); }
	| LT {xg.append("<");} xgrs[xg] GT {xg.append(">");}
	;

parallelCallRule[ExecNode xg, CollectNode<BaseNode> returns]
	: LBRACK {xg.append("[");} callRule[xg, returns] RBRACK {xg.append("]");}
	| callRule[xg, returns]
	;

callRule[ExecNode xg, CollectNode<BaseNode> returns]
	{
		CollectNode<IdentNode> params = new CollectNode<IdentNode>();
		IdentNode id;
	}
	: id=actionIdentUse {xg.append(id);}
		(LPAREN paramListOfEntIdentUse[params]
			{
				xg.addCallAction(new CallActionNode(id.getCoords(), id, params, returns));
				xg.append("(");
				for(Iterator<IdentNode> i =params.getChildren().iterator(); i.hasNext();) {
					IdentNode p = i.next();
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








