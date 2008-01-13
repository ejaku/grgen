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
 * @version 0.1
 * @author Sebastian Hack
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
		CollectNode patternChilds = new CollectNode();
		CollectNode actionChilds = new CollectNode();
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

	( patternAndActionDecls[patternChilds, actionChilds] EOF )?
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

patternAndActionDecls[ CollectNode patternChilds, CollectNode actionChilds ]
	{
		IdentNode d;
		int mod = 0;
	}

	:   (mod=patternModifiers
			( d=patternDecl[mod]
				{ patternChilds.addChild(d); } 
			| d=testDecl[mod]
				{ actionChilds.addChild(d); } 
			| d=ruleDecl[mod]
				{ actionChilds.addChild(d); } 
			)
		)+
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

testDecl [int mod] returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		PatternGraphNode pattern;
		CollectNode params, ret;
		CollectNode negs = new CollectNode();
	}

	: t:TEST { if((mod & PatternGraphNode.MOD_DPO)!=0) {
				reportError(getCoords(t), "no \"dpo\" modifier allowed");
			}
		}
	
		id=actionIdentDecl pushScope[id] params=parameters ret=returnTypes LBRACE
		pattern=patternPart[getCoords(t), negs, mod]
			{
				id.setDecl(new TestDeclNode(id, pattern, negs, params, ret));
				res = id;
			}
		RBRACE popScope
	;

ruleDecl [int mod] returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		PatternGraphNode left;
		GraphNode right;
		CollectNode params, ret;
		CollectNode negs = new CollectNode();
		CollectNode eval = new CollectNode();
		CollectNode dels = new CollectNode();
	}

	: r:RULE id=actionIdentDecl pushScope[id] params=parameters ret=returnTypes LBRACE
		left=patternPart[getCoords(r), negs, mod]
		( right=replacePart[eval]
			{
				id.setDecl(new RuleDeclNode(id, left, right, negs, eval, params, ret));
				res = id;
			}
		| right=modifyPart[eval,dels]
			{
				id.setDecl(new ModifyRuleDeclNode(id, left, right, negs, eval, params, ret, dels));
				res = id;
			}
		)
		RBRACE popScope
	;

patternDecl [int mod] returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		PatternGraphNode left;
		GraphNode right;
		CollectNode params;
		CollectNode negs = new CollectNode();
		CollectNode eval = new CollectNode();
		CollectNode dels = new CollectNode();
	}
	: p:PATTERN id=typeIdentDecl pushScope[id] params=parameters LBRACE
		left=patternPart[getCoords(p), negs, mod]
		( right=replacePart[eval]
			{
				id.setDecl(new RuleDeclNode(id, left, right, negs, eval, params, null));
				res = id;
			}
		| right=modifyPart[eval,dels]
			{
				id.setDecl(new ModifyRuleDeclNode(id, left, right, negs, eval, params, null, dels));
				res = id;
			}
		)
		RBRACE popScope
			{ reportError(getCoords(p), "pattern declarations not yet supported"); }
	;
	
parameters returns [ CollectNode res = new CollectNode() ]
	: LPAREN (paramList[res])? RPAREN
	|
	;

paramList [ CollectNode params ]
	{ BaseNode p; }

	: p=param { params.addChild(p); } ( COMMA p=param { params.addChild(p); } )*
	;

param returns [ BaseNode res = env.initNode() ]
	: MINUS res=patEdgeDecl RARROW
	| res=patNodeDecl
	;

returnTypes returns [ CollectNode res = new CollectNode() ]
	{ BaseNode type; }

	: COLON LPAREN
		( type=typeIdentUse { res.addChild(type); } ( COMMA type=typeIdentUse { res.addChild(type); } )* )?
		RPAREN
	|
	;

patternPart [ Coords pattern_coords, CollectNode negs, int mod ] returns [ PatternGraphNode res = null ]
	: p:PATTERN LBRACE
		res=patternBody[getCoords(p), negs, mod]
		RBRACE
			{ reportWarning(getCoords(p), "separate pattern part deprecated, just merge content directly into rule/test-body"); }
	| res=patternBody[pattern_coords, negs, mod]
	;

replacePart [ CollectNode eval ] returns [ GraphNode res = null ]
	: r:REPLACE LBRACE
		res=replaceBody[getCoords(r), eval]
		RBRACE
	;

modifyPart [ CollectNode eval, CollectNode dels ] returns [ GraphNode res = null ]
	: r:MODIFY LBRACE
		res=modifyBody[getCoords(r), eval, dels]
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

patternBody [ Coords coords, CollectNode negs, int mod ] returns [ PatternGraphNode res = null ]
	{
		CollectNode connections = new CollectNode();
		CollectNode conditions = new CollectNode();
		CollectNode returnz = new CollectNode();
		CollectNode homs = new CollectNode();
		CollectNode exact = new CollectNode();
		CollectNode induced = new CollectNode();
		CollectNode subpatterns = new CollectNode();
		res = new PatternGraphNode(coords, connections, subpatterns, conditions, returnz, homs, exact, induced, mod);
		int negCounter = 0;
	}

	: ( negCounter = patternStmt[connections, subpatterns, conditions, negs, negCounter, returnz, homs, exact, induced] )*
	;

patternStmt [ CollectNode conn, CollectNode subpatterns, CollectNode cond,
	CollectNode negs, int negCount, CollectNode returnz, CollectNode homs, CollectNode exact, CollectNode induced ]
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

	: patConnectionsOrSubpattern[conn, subpatterns] SEMI
		// TODO: insert mod=patternModifiers iff nesting of negative parts is allowed
	| p:NEGATIVE pushScopeStr[ "neg" + negCount, getCoords(p) ] LBRACE
		neg=patternBody[getCoords(p), negsInNegs, mod]
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
	| replaceReturns[returnz] SEMI
	| hom=homStatement { homs.addChild(hom); } SEMI
	| exa=exactStatement { exact.addChild(exa); } SEMI
	| ind=inducedStatement { induced.addChild(ind); } SEMI
	;

patConnectionsOrSubpattern [ CollectNode conn, CollectNode subpatterns ]
	: patFirstEdge[conn] // connection starts with an edge which dangles on the left
	| patFirstNodeOrSubpattern[conn, subpatterns] // there's a subpattern or a connection that starts with a node
	;

patFirstEdge [ CollectNode conn ]
	{
		BaseNode e;
		boolean forward = true;
	}
	
	:   ( e=patForwardEdgeOcc { forward=true; } // get first edge
		| e=patBackwardEdgeOcc { forward=false; }
		)
			patNodeContinuation[e, env.getDummyNodeDecl(NodeDeclNode.DECL_IN_PATTERN), forward, conn] // and continue looking for node
	;
	
patFirstNodeOrSubpattern [ CollectNode conn, CollectNode subpatterns ]
	{
		IdentNode id = env.getDummyIdent();
		IdentNode type = env.getNodeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
		Annotations annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
		CollectNode subpatternConnections = new CollectNode();
		BaseNode n = null;
	}

	: id=entIdentUse firstPatEdgeContinuation[id, conn] // use of already declared node, continue looking for first edge
	| id=entIdentDecl cc:COLON // node or subpattern declaration
		( // node declaration
			type=typeIdentUse
			( constr=typeConstraint )?
			{ n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
			firstPatEdgeContinuation[n, conn] // and continue looking for first edge
		| // node typeof declaration
			TYPEOF LPAREN type=entIdentUse RPAREN
			( constr=typeConstraint )?
			{ n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
			firstPatEdgeContinuation[n, conn] // and continue looking for first edge
		| // subpattern declaration
			type=typeIdentUse LPAREN (paramList[subpatternConnections])? RPAREN
			{ subpatterns.addChild(new SubpatternNode(id, type, subpatternConnections)); }
			{ reportError(getCoords(cc), "subpatterns not yet supported"); }
		)
	| ( annots=annotations { hasAnnots = true; } )?
		c:COLON // anonymous node or subpattern declaration
			( // node declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				type=typeIdentUse
				( constr=typeConstraint )?
				{ n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
				firstPatEdgeContinuation[n, conn] // and continue looking for first edge
			| // node typeof declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				TYPEOF LPAREN type=entIdentUse RPAREN
				( constr=typeConstraint )?
				{ n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
				firstPatEdgeContinuation[n, conn] // and continue looking for first edge
			| // subpattern declaration
				{ id = env.defineAnonymousEntity("subpattern", getCoords(c)); }
				type=typeIdentUse LPAREN (paramList[subpatternConnections])? RPAREN
				{ subpatterns.addChild(new SubpatternNode(id, type, subpatternConnections)); }
				{ reportError(getCoords(c), "subpatterns not yet supported"); }
			)
			{ if (hasAnnots) { id.setAnnotations(annots); } }
	| d:DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
		firstPatEdgeContinuation[n, conn] // and continue looking for first edge
	;

patNodeContinuation [ BaseNode e, BaseNode n1, boolean forward, CollectNode conn ]
	{ BaseNode n2 = env.getDummyNodeDecl(NodeDeclNode.DECL_IN_PATTERN); }
	
	: n2=patNodeOcc // node following - get it and build connection with it, then continue with looking for follwing edge 
		{
			if (forward) {
				conn.addChild(new ConnectionNode(n1, e, n2));
			} else {
				conn.addChild(new ConnectionNode(n2, e, n1));
			}
		}
		patEdgeContinuation[n2, conn]
	|   // nothing following - build connection with edge dangeling on the right (see n2 initialization)
		{
			if (forward) {
				conn.addChild(new ConnectionNode(n1, e, n2));
			} else {
				conn.addChild(new ConnectionNode(n2, e, n1));
			}
		}
	;

firstPatEdgeContinuation [ BaseNode n, CollectNode conn ]
	{
		BaseNode e;
		boolean forward = true;
	}
	
	:   { conn.addChild(new SingleNodeConnNode(n)); } // nothing following? -> one single node
	|   ( e=patForwardEdgeOcc { forward=true; }
		| e=patBackwardEdgeOcc { forward=false; }
		)
			patNodeContinuation[e, n, forward, conn] // continue looking for node
	;
	
patEdgeContinuation [ BaseNode left, CollectNode conn ]
	{
		BaseNode e;
		boolean forward = true;
	}
	
	:   // nothing following? -> connection end reached
	|   ( e=patForwardEdgeOcc { forward=true; }
		| e=patBackwardEdgeOcc { forward=false; }
		)
			patNodeContinuation[e, left, forward, conn] // continue looking for node
	;

patNodeOcc returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id = env.getDummyIdent();
		IdentNode type = env.getNodeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
		Annotations annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
	}

	: res=entIdentUse // use of already declared node
	| id=entIdentDecl COLON res=patNodeTypeContinuation[id] // node declaration
	| ( annots=annotations { hasAnnots = true; } )?
		c:COLON // anonymous node declaration
			{ id = env.defineAnonymousEntity("node", getCoords(c)); }
			{ if (hasAnnots) { id.setAnnotations(annots); } }
			res=patNodeTypeContinuation[id]
	| d:DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ res = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
	;

patNodeTypeContinuation [ IdentNode id ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode type = env.getNodeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
	}
	
	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		) ( constr=typeConstraint )?
			{ res = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
	;
	
patNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id, type;
		TypeExprNode constr = TypeExprNode.getEmpty();
	}

	: id=entIdentDecl COLON
		( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( constr=typeConstraint )?
			{ res = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_PATTERN, constr); }
	;

patForwardEdgeOcc returns [ BaseNode res = env.initNode() ]
	{
		BaseNode type = env.getEdgeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
	}

	: MINUS ( res=patEdgeDecl | res=entIdentUse) RARROW
	| mm:DOUBLE_RARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, type, EdgeDeclNode.DECL_IN_PATTERN, constr);
		}
	;

patBackwardEdgeOcc returns [ BaseNode res = env.initNode() ]
	{
		BaseNode type = env.getEdgeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
	}

	: LARROW ( res=patEdgeDecl | res=entIdentUse ) MINUS
	| mm:DOUBLE_LARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, type, EdgeDeclNode.DECL_IN_PATTERN, constr);
		}
	;

patEdgeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id = env.getDummyIdent();
		Annotations annots = env.getEmptyAnnotations();
		Pair<DefaultAnnotations, de.unika.ipd.grgen.parser.Coords> atCo;
	}

	:   ( id=entIdentDecl COLON
			res=patEdgeTypeContinuation[id]
		| atCo=annotationsWithCoords
			( c:COLON
				{ id = env.defineAnonymousEntity("edge", getCoords(c)); }
				res=patEdgeTypeContinuation[id]
			|   { id = env.defineAnonymousEntity("edge", atCo.second); }
				{ res = new EdgeDeclNode(id, env.getEdgeRoot(), EdgeDeclNode.DECL_IN_PATTERN, TypeExprNode.getEmpty()); }
			)
				{ id.setAnnotations(atCo.first); }
		| cc:COLON
			{ id = env.defineAnonymousEntity("edge", getCoords(cc)); }
			res=patEdgeTypeContinuation[id]
		)
	;

patEdgeTypeContinuation [ IdentNode id ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode type = env.getNodeRoot();
		TypeExprNode constr = TypeExprNode.getEmpty();
	}
	
	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		) ( constr=typeConstraint )?
			{ res = new EdgeDeclNode(id, type, EdgeDeclNode.DECL_IN_PATTERN, constr); }
	;
	
/**
 * A statement defining some nodes/edges to be matched potentially
 * homomorphically
 */
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

replaceBody [ Coords coords, CollectNode eval ] returns [ GraphNode res = null ]
	{
		CollectNode connections = new CollectNode();
		CollectNode subpatterns = new CollectNode();
		CollectNode returnz = new CollectNode();
		res = new GraphNode(coords, connections, subpatterns, returnz);
	}

	: ( replaceStmt[coords, connections, subpatterns, returnz, eval] )*
	;

replaceStmt [ Coords coords, CollectNode connections, CollectNode subpatterns, CollectNode returnz, CollectNode eval ]
	: replConnectionsOrSubpattern[connections, subpatterns] SEMI
	| replaceReturns[returnz] SEMI
	| evalPart[eval]
	;

modifyBody [ Coords coords, CollectNode eval, CollectNode dels ] returns [ GraphNode res = null ]
	{
		CollectNode connections = new CollectNode();
		CollectNode subpatterns = new CollectNode();
		CollectNode returnz = new CollectNode();
		res = new GraphNode(coords, connections, subpatterns, returnz);
	}

	: ( modifyStmt[coords, connections, subpatterns, returnz, eval, dels] )*
	;

modifyStmt [ Coords coords, CollectNode connections, CollectNode subpatterns, CollectNode returnz, CollectNode eval, CollectNode dels ]
	: replConnectionsOrSubpattern[connections, subpatterns] SEMI
	| replaceReturns[returnz] SEMI
	| deleteStmt[dels] SEMI
	| evalPart[eval]
	;

replConnectionsOrSubpattern [ CollectNode conn, CollectNode subpatterns ]
	: replFirstEdge[conn] // connection starts with an edge which dangles on the left
	| replFirstNodeOrSubpattern[conn, subpatterns] // connection starts with a node
	;
	
replFirstEdge [ CollectNode conn ]
	{
		BaseNode e;
		boolean forward = true;
	}
	
	:   ( e=replForwardEdgeOcc { forward=true; } // get first edge
		| e=replBackwardEdgeOcc { forward=false; }
		)
			replNodeContinuation[e, env.getDummyNodeDecl(NodeDeclNode.DECL_IN_REPLACEMENT), forward, conn] // and continue looking for node
	;
	
replFirstNodeOrSubpattern [ CollectNode conn, CollectNode subpatterns ]
	{
		IdentNode id = env.getDummyIdent();
		IdentNode type = env.getNodeRoot();
		IdentNode oldid = null;
		Annotations annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
		CollectNode subpatternConnections = new CollectNode();
		CollectNode subpatternReplConnections = new CollectNode();
		BaseNode n = null;
	}

	: n=entIdentUse firstReplEdgeContinuation[n, conn] // use of already declared node, continue looking for first edge
	| id=entIdentUse l:LPAREN (paramList[subpatternReplConnections])? RPAREN // use of already declared subpattern
		{ subpatterns.addChild(new SubpatternReplNode(id, subpatternReplConnections)); }
		{ reportError(getCoords(l), "subpatterns not yet supported"); }
	| id=entIdentDecl cc:COLON // node or subpattern declaration
		( // node declaration
			type=typeIdentUse
			( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT);
				} else {
					n = new NodeTypeChangeNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT, oldid);
				}
			}
			firstReplEdgeContinuation[n, conn] // and continue looking for first edge
		| // node typeof declaration
			TYPEOF LPAREN type=entIdentUse RPAREN
			( LT oldid=entIdentUse GT )?
			{
				if(oldid==null) {
					n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT);
				} else {
					n = new NodeTypeChangeNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT, oldid);
				}
			}
			firstReplEdgeContinuation[n, conn] // and continue looking for first edge
		| // subpattern declaration
			type=typeIdentUse LPAREN (paramList[subpatternConnections])? RPAREN
			{ subpatterns.addChild(new SubpatternNode(id, type, subpatternConnections)); }
			{ reportError(getCoords(cc), "subpatterns not yet supported"); }
		)
	| ( annots=annotations { hasAnnots = true; } )?
		c:COLON // anonymous node or subpattern declaration
			( // node declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				type=typeIdentUse
				( LT oldid=entIdentUse GT )?
				{
					if(oldid==null) {
						n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT);
					} else {
						n = new NodeTypeChangeNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT, oldid);
					}
				}
				firstReplEdgeContinuation[n, conn] // and continue looking for first edge
			| // node typeof declaration
				{ id = env.defineAnonymousEntity("node", getCoords(c)); }
				TYPEOF LPAREN type=entIdentUse RPAREN
				( LT oldid=entIdentUse GT )?
				{
					if(oldid==null) {
						n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT);
					} else {
						n = new NodeTypeChangeNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT, oldid);
					}
				}
				firstReplEdgeContinuation[n, conn] // and continue looking for first edge
			| // subpattern declaration
				{ id = env.defineAnonymousEntity("subpattern", getCoords(c)); }
				type=typeIdentUse LPAREN (paramList[subpatternConnections])? RPAREN
				{ subpatterns.addChild(new SubpatternNode(id, type, subpatternConnections)); }
				{ reportError(getCoords(c), "subpatterns not yet supported"); }
			)
			{ if (hasAnnots) { id.setAnnotations(annots); } }
	| d:DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ n = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT); }
		firstReplEdgeContinuation[n, conn] // and continue looking for first edge
	;
	
replNodeContinuation [ BaseNode e, BaseNode n1, boolean forward, CollectNode conn ]
	{ BaseNode n2 = env.getDummyNodeDecl(NodeDeclNode.DECL_IN_REPLACEMENT); }
	
	: n2=replNodeOcc // node following - get it and build connection with it, then continue with looking for follwing edge 
		{
			if (forward) {
				conn.addChild(new ConnectionNode(n1, e, n2));
			} else {
				conn.addChild(new ConnectionNode(n2, e, n1));
			}
		}
		replEdgeContinuation[n2, conn]
	|   // nothing following - build connection with edge dangeling on the right (see n2 initialization)
		{
			if (forward) {
				conn.addChild(new ConnectionNode(n1, e, n2));
			} else {
				conn.addChild(new ConnectionNode(n2, e, n1));
			}
		}
	;

firstReplEdgeContinuation [ BaseNode n, CollectNode conn ]
	{
		BaseNode e;
		boolean forward = true;
	}
	
	:   { conn.addChild(new SingleNodeConnNode(n)); } // nothing following? -> one single node
	|   ( e=replForwardEdgeOcc { forward=true; }
		| e=replBackwardEdgeOcc { forward=false; }
		)
			replNodeContinuation[e, n, forward, conn] // continue looking for node
	;
	
replEdgeContinuation [ BaseNode left, CollectNode conn ]
	{
		BaseNode e;
		boolean forward = true;
	}
	
	:   // nothing following? -> connection end reached
	|   ( e=replForwardEdgeOcc { forward=true; }
		| e=replBackwardEdgeOcc { forward=false; }
		)
			replNodeContinuation[e, left, forward, conn] // continue looking for node
	;
	
replNodeOcc returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id = env.getDummyIdent();
		IdentNode type = env.getNodeRoot();
		Annotations annots = env.getEmptyAnnotations();
		boolean hasAnnots = false;
	}

	: res=entIdentUse // use of already declared node
	| id=entIdentDecl COLON res=replNodeTypeContinuation[id] // node declaration
	| ( annots=annotations { hasAnnots = true; } )?
		c:COLON // anonymous node declaration
			{ id = env.defineAnonymousEntity("node", getCoords(c)); }
			{ if (hasAnnots) { id.setAnnotations(annots); } }
			res=replNodeTypeContinuation[id]
	| d:DOT // anonymous node declaration of type node
		{ id = env.defineAnonymousEntity("node", getCoords(d)); }
		( annots=annotations { id.setAnnotations(annots); } )?
		{ res = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT); }
	;

replNodeTypeContinuation [ IdentNode id ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode type = env.getNodeRoot();
		IdentNode oldid = null;
	}
	
	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		) ( LT oldid=entIdentUse GT )?
			{ 
				if(oldid==null) {
					res = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT);
				} else {
					res = new NodeTypeChangeNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT, oldid);
				}
			}
	;
	
replNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id, type;
		IdentNode oldid = null;
	}

	: id=entIdentDecl COLON
		( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		)
		( LT oldid=entIdentUse GT )?
			{ 
				if(oldid==null) {
					res = new NodeDeclNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT);
				} else {
					res = new NodeTypeChangeNode(id, type, NodeDeclNode.DECL_IN_REPLACEMENT, oldid);
				}
			}
	;
	
replForwardEdgeOcc returns [ BaseNode res = env.initNode() ]
	{ IdentNode type = env.getEdgeRoot(); }

	: MINUS ( res=entIdentUse | res=replEdgeDecl ) RARROW 
	| mm:DOUBLE_RARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, type, EdgeDeclNode.DECL_IN_REPLACEMENT);
		}
  ;

replBackwardEdgeOcc returns [ BaseNode res = env.initNode() ]
	{ IdentNode type = env.getEdgeRoot(); }

	: LARROW ( res=entIdentUse | res=replEdgeDecl ) MINUS 
	| mm:DOUBLE_LARROW
		{
			IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
			res = new EdgeDeclNode(id, type, EdgeDeclNode.DECL_IN_REPLACEMENT);
		}
	;

replEdgeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id = env.getDummyIdent();
		Annotations annots = env.getEmptyAnnotations();
		Pair<DefaultAnnotations, de.unika.ipd.grgen.parser.Coords> atCo;
	}

	:   ( id=entIdentDecl COLON
			res=replEdgeTypeContinuation[id]
		| atCo=annotationsWithCoords
			( c:COLON
				{ id = env.defineAnonymousEntity("edge", getCoords(c)); }
				res=replEdgeTypeContinuation[id]
			|   { id = env.defineAnonymousEntity("edge", atCo.second); }
				{ res = new EdgeDeclNode(id, env.getEdgeRoot(), EdgeDeclNode.DECL_IN_REPLACEMENT); }
			)
				{ id.setAnnotations(atCo.first); }
		| cc:COLON
			{ id = env.defineAnonymousEntity("edge", getCoords(cc)); }
			res=replEdgeTypeContinuation[id]
		)
	;

replEdgeTypeContinuation [ IdentNode id ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode type = env.getNodeRoot();
		IdentNode oldid = null;
	}
	
	:	( type=typeIdentUse
		| TYPEOF LPAREN type=entIdentUse RPAREN
		) ( LT oldid=entIdentUse GT )?
			{
				if( oldid == null ) {
					res = new EdgeDeclNode(id, type, EdgeDeclNode.DECL_IN_REPLACEMENT);
				} else {
					res = new EdgeTypeChangeNode(id, type, EdgeDeclNode.DECL_IN_REPLACEMENT, oldid);
				}
			}
	;
	
replaceReturns[CollectNode res]
	{
		IdentNode id;
		boolean multipleReturns = ! res.getChildren().isEmpty();
	}

	: r:RETURN
		{
			if ( multipleReturns ) {
				reportError(getCoords(r), "multiple occurence of return statement in one rule");
			}
		}
		LPAREN id=entIdentUse { if ( !multipleReturns ) res.addChild(id); }
		( COMMA id=entIdentUse { if ( !multipleReturns ) res.addChild(id); } )*
		RPAREN
			{ res.setCoords(getCoords(r)); }
	;

deleteStmt[CollectNode res]
	{
		IdentNode id;
	}

	: DELETE
		LPAREN id=entIdentUse { res.addChild(id); }
		( COMMA id=entIdentUse { res.addChild(id); } )*
		RPAREN
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

