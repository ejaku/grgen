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
  k=3;
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
  		CollectNode actions;
  		CollectNode mainChilds = new CollectNode();
  		CollectNode modelChilds = new CollectNode();
  		IdentNode id;
  		List modelList = new LinkedList();
  	}
  	: ACTIONS id=entIdentDecl (USING identList[modelList])? SEMI {
  	  
  	  modelChilds.addChild(env.getStdModel());
  	  
  	  for(Iterator it = modelList.iterator(); it.hasNext();) {
  	    String modelName = (String) it.next();
  	    File modelFile = env.findModel(modelName);
  	  
  	    BaseNode model = env.parseModel(modelFile);
  	    modelChilds.addChild(model);
  	  }

  	} actions=actionDecls EOF {

      mainChilds.addChildren(actions);
  	  main = new UnitNode(id, getFilename());
  	  main.addChild(modelChilds);
  	  main.addChild(mainChilds);
    	
      env.getCurrScope().leaveScope();
   	}
  	;
  	
identList [ Collection strings ]
  : fid:IDENT { strings.add(fid.getText()); }
    (COMMA sid:IDENT { strings.add(sid.getText()); })*
  ;
  	
actionDecls returns [ CollectNode c = new CollectNode() ]
  { BaseNode d; }
  : ( d=actionDecl { c.addChild(d); } )+;

/**
 * graph declarations contain
 * - rules
 * - tests
 */
actionDecl returns [ BaseNode res = env.initNode() ]
  : res=testDecl
  | res=ruleDecl
  ;

testDecl returns [ BaseNode res = env.initNode() ]
    {
  		IdentNode id;
  		BaseNode tb, pattern;
  		CollectNode params, ret, negs = new CollectNode();
  	}
  	: TEST id=actionIdentDecl pushScope[id] params=parameters ret=returnTypes LBRACE!
  	  pattern=patternPart[negs]
  	  {
      	id.setDecl(new TestDeclNode(id, pattern, negs, params, ret));
      	res = id;
  	  } RBRACE! popScope!;

ruleDecl returns [ BaseNode res = env.initNode() ]
  {
  		IdentNode id;
  		BaseNode rb, left, right;
  		CollectNode params, ret;

  		CollectNode negs = new CollectNode();
  		CollectNode eval = new CollectNode();
  		CollectNode dels = new CollectNode();
  }
  : RULE id=actionIdentDecl pushScope[id] params=parameters ret=returnTypes LBRACE!
  	left=patternPart[negs]
  	(right=replacePart[eval]
  	{
		id.setDecl(new RuleDeclNode(id, left, right, negs, eval, params, ret));
		res = id;
    }|right=modifyPart[eval,dels]
  	{
  	   id.setDecl(new ModifyRuleDeclNode(id, left, right, negs, eval, params, ret, dels));
  	   res = id;
    })
    RBRACE! popScope!
  ;

parameters returns [ CollectNode res = new CollectNode() ]
	: LPAREN (paramList[res])? RPAREN
	|
	;

paramList [ CollectNode params ]
  {
  	BaseNode p;
  }
	: p=param { params.addChild(p); } ( COMMA p=param { params.addChild(p); } )*
	;
	
param returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id;
		BaseNode type;
	}
	: MINUS res=patternEdgeDecl RARROW
	| res=patternNodeDecl
//	: id=entIdentDecl COLON type=typeIdentUse
//	  { res = new ParamDeclNode(id, type); }
	;

returnTypes returns [ CollectNode res = new CollectNode() ]
    {
    	BaseNode type;
    }
	: COLON LPAREN type=typeIdentUse { res.addChild(type); }
      (COMMA type=typeIdentUse { res.addChild(type); })* RPAREN
	| COLON LPAREN RPAREN
	|
	;

patternPart [ BaseNode negsCollect ] returns [ BaseNode res = env.initNode() ]
  : p:PATTERN LBRACE! res=patternBody[getCoords(p), negsCollect] RBRACE!
  ;
  
replacePart [ CollectNode eval ] returns [ BaseNode res = env.initNode() ]
	: r:REPLACE LBRACE! res=replaceBody[getCoords(r),eval] RBRACE!
	;

modifyPart [ CollectNode eval, CollectNode dels ] returns [ BaseNode res = env.initNode() ]
	: r:MODIFY LBRACE! res=modifyBody[getCoords(r),eval,dels] RBRACE!
	;

evalPart [ BaseNode n ]
	: e:EVAL LBRACE evalBody[n] RBRACE
	;
	
evalBody [ BaseNode n  ]
  { BaseNode a; }
  : (a=assignment { n.addChild(a); } SEMI)*
  ;
	
/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
patternBody [ Coords coords, BaseNode negsCollect ] returns [ BaseNode res = env.initNode() ]
  {
		BaseNode s;
 		CollectNode connections = new CollectNode();
 		CollectNode conditions = new CollectNode();
 		CollectNode returnz = new CollectNode();
 		CollectNode homs = new CollectNode();
		res = new PatternGraphNode(coords, connections, conditions, returnz, homs);
		int negCounter = 0;
  }
  // TODO: where to get coordinates from for the statement???
  : (negCounter = patternStmt[coords, connections, conditions, negsCollect, negCounter, returnz, homs])*
  ;

patternStmt [ Coords coords, BaseNode connCollect, BaseNode condCollect,
  BaseNode negsCollect, int negCount, CollectNode returnz, CollectNode homs ] returns [ int newNegCount ]
  
	{
  	IdentNode id = env.getDummyIdent();
		BaseNode n, o, e, neg, hom;
		//nesting of negative Parts is not allowed.
		CollectNode negsInNegs = new CollectNode();
		
		newNegCount = negCount;
	}
	: patternConnections[coords,connCollect] SEMI
	
	| p:NEGATIVE pushScopeStr[ "neg" + negCount ] LBRACE!
	  neg=patternBody[getCoords(p), negsInNegs] {
	    newNegCount = negCount + 1;
	    negsCollect.addChild(neg);
	  } RBRACE! popScope! {
	    if(negsInNegs.children() != 0)
	      reportError(getCoords(p), "Nesting of negative parts not allowed");
	  }
	| COND e=expr { condCollect.addChild(e); } SEMI
	| COND LBRACE (e=expr SEMI { condCollect.addChild(e); })* RBRACE
	| replaceReturns[returnz] SEMI
	| hom=homStatement { homs.addChild(hom); } SEMI
	;

patternConnections [ Coords coords, BaseNode connColl ]
  { BaseNode n; }
  : n=patternAnonNodeOcc[coords] (patternContinuation[n,connColl] | {
  	connColl.addChild(new SingleNodeConnNode(n));
  })
  ;

/**
 * A statement defining some nodes/edges to be mactched potentially
 * homomorphically
 */
homStatement returns [ BaseNode res = env.initNode() ]
    {
    	BaseNode id;
    }
	: h:HOM {res = new HomNode(getCoords(h)); } LPAREN id=entIdentUse { res.addChild(id); }
      (COMMA id=entIdentUse { res.addChild(id); })* RPAREN
	;

/**
 * A continuation is a list of edge node pairs or a list of these pair lists, comma
 * seperated and delimited by parantheses.
 * all produced connection nodes are appended to the collect node
 */
patternContinuation [ BaseNode left, BaseNode collect ]
  { BaseNode n; }
  : n=patternPair[left, collect] (patternContinuation[n, collect])?
  ;

/**
 * An edge node pair.
 * This rule builds a connection node with the parameter left,
 * the edge and the patternNodeOcc
 * and appends this connection node to the children of coll.
 * The rule returns the right node (the one from the nodeOcc rule)
 * It also treats reversed edges (a <-- b).
 */
patternPair [ BaseNode left, BaseNode coll ] returns [ BaseNode res = env.initNode() ]
  {
  	BaseNode e;
  }
	:	e=patternEdgeOcc res=patternAnonNodeOcc[(de.unika.ipd.grgen.parser.antlr.Coords) e.getCoords()] {
			coll.addChild(new ConnectionNode(left, e, res));
	  }
	|	e=patternReversedEdge res=patternAnonNodeOcc[(de.unika.ipd.grgen.parser.antlr.Coords) e.getCoords()] {
		  coll.addChild(new ConnectionNode(res, e, left));
	  }
 	;

/**
 * The occurrence of a node in a pattern part is the usage of an
 * identifier (must be a declared node) or a pattern node declaration
 */
patternNodeOcc returns [ BaseNode res = env.initNode() ]
  : res=entIdentUse
  | res=patternNodeDecl
  ;

/**
 * A potentially anonymous node occurence
 */
patternAnonNodeOcc [ Coords coords ] returns [ BaseNode res = env.initNode() ]
  : res=patternNodeOcc
  | res=anonPatternNode[coords]
  ;
  
/**
  * An anonymous Node
  */
anonPatternNode [ Coords coords ] returns  [ BaseNode res = env.initNode() ]
  {
  	BaseNode type = env.getNodeRoot();
	BaseNode constr = TypeExprNode.getEmpty();
  }
  : (COLON (type=typeIdentUse (constr=typeConstraint)?| TYPEOF LPAREN type=entIdentUse RPAREN))? {
		IdentNode id = env.defineAnonymousEntity("node", coords);
		res = new NodeDeclNode(id, type, constr);
	}
  ;
  	

patternReversedEdge returns [ BaseNode res = env.initNode() ]
  {
	BaseNode type = env.getEdgeRoot();
	BaseNode constr = TypeExprNode.getEmpty();
  }
  : LARROW res=patternEdgeDecl MINUS
  | LARROW res=entIdentUse MINUS
  | LARROW (COLON (type=typeIdentUse (constr=typeConstraint)? | TYPEOF LPAREN type=entIdentUse RPAREN))? m:MINUS {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new EdgeDeclNode(id, type, constr);
    }
  ;

patternEdgeOcc returns [ BaseNode res = env.initNode() ]
	{
		BaseNode type = env.getEdgeRoot();
		BaseNode constr = TypeExprNode.getEmpty();
	}
  : MINUS res=patternEdgeDecl RARROW
  | MINUS res=entIdentUse RARROW
  | MINUS (COLON (type=typeIdentUse (constr=typeConstraint)? | TYPEOF LPAREN type=entIdentUse RPAREN))? m:RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new EdgeDeclNode(id, type, constr);
  }
  ;

/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
replaceBody [ Coords coords, CollectNode eval ] returns [ BaseNode res = env.initNode() ]
    {
  		CollectNode connections = new CollectNode();
  		CollectNode returnz = new CollectNode();
  	  	res = new GraphNode(coords, connections, returnz);
    }
    // TODO: where to get coordinates from for the statement???
    : ( replaceStmt[coords, connections, returnz, eval] )*
  ;

replaceStmt [ Coords coords, CollectNode connections, CollectNode returnz, CollectNode eval ]
	{ BaseNode n; }
	: replaceConnections[coords,connections] SEMI
	| replaceReturns[returnz] SEMI
    | evalPart[eval]
	;

/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
modifyBody [ Coords coords, CollectNode eval, CollectNode dels ] returns [ BaseNode res = env.initNode() ]
    {
  		CollectNode connections = new CollectNode();
  		CollectNode returnz = new CollectNode();
  	  	res = new GraphNode(coords, connections, returnz);
    }
    // TODO: where to get coordinates from for the statement???
    : ( modifyStmt[coords, connections, returnz, eval, dels] )*
  ;

modifyStmt [ Coords coords, CollectNode connections, CollectNode returnz, CollectNode eval, CollectNode dels ]
	{ BaseNode n; }
	: replaceConnections[coords,connections] SEMI
	| replaceReturns[returnz] SEMI
    | deleteStmt[dels] SEMI
    | evalPart[eval]
	;

replaceConnections [ Coords coords, BaseNode connColl ]
	{ BaseNode n; }
	: n=replaceNodeOcc (replaceContinuation[n, connColl] | {
		connColl.addChild(new SingleNodeConnNode(n));
  	})
  	| n=anonReplaceNode[coords] replaceContinuation[n,connColl]
  ;
  
/**
 * Acontinuation is a list of edge node pairs or a list of these pair lists, comma
 * seperated and delimited by parantheses.
 * all produced connection nodes are appended to the collect node
 */
replaceContinuation [ BaseNode left, BaseNode collect ]
  { BaseNode n; }
  : n=replacePair[left, collect] (replaceContinuation[n, collect])?
  ;

/**
 * An edge node pair.
 * This rule builds a connection node with the parameter left, the edge and the nodeOcc
 * and appends this connection node to the children of coll.
 * The rule returns the right node (the one from the nodeOcc rule)
 */
replacePair [ BaseNode left, BaseNode coll ] returns [ BaseNode res = env.initNode() ]
	{ BaseNode e; }
	: e=replaceEdgeOcc res=replaceAnonNodeOcc[(de.unika.ipd.grgen.parser.antlr.Coords) e.getCoords()] {
		coll.addChild(new ConnectionNode(left, e, res));
  	}
	| e=replaceReversedEdge res=replaceAnonNodeOcc[(de.unika.ipd.grgen.parser.antlr.Coords) e.getCoords()] {
  		coll.addChild(new ConnectionNode(res, e, left));
    };

replaceEdgeOcc returns [ BaseNode res = env.initNode() ]
	{ BaseNode type = env.getEdgeRoot(); }
	: MINUS res=replaceEdgeDecl RARROW
	| MINUS res=entIdentUse RARROW
	| m:MINUS (COLON (type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN))? RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new EdgeDeclNode(id, type);
	}
	;

replaceReversedEdge returns [ BaseNode res = env.initNode() ]
	{ BaseNode type = env.getEdgeRoot(); }
	: LARROW res=replaceEdgeDecl MINUS
	| LARROW res=entIdentUse MINUS
	| m:LARROW(COLON (type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN))? MINUS {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new EdgeDeclNode(id, type);
	}
	;

replaceReturns[CollectNode res]
    {
    	BaseNode id;
    }
	: RETURN LPAREN id=entIdentUse { res.addChild(id); }
      (COMMA id=entIdentUse { res.addChild(id); })* RPAREN
	;

deleteStmt[CollectNode res]
    {
    	BaseNode id;
    }
	: DELETE LPAREN id=entIdentUse { res.addChild(id); }
      (COMMA id=entIdentUse { res.addChild(id); })* RPAREN
	;

/**
 * The occurrence of a node.
 * A node occurrence is either the declaration of a new node, a usage of
 * a already declared node or a usage combined with a type change.
 */
replaceNodeOcc returns [ BaseNode res = env.initNode() ]
  : res=entIdentUse
  | res=replaceNodeDecl
  ;

replaceAnonNodeOcc [ Coords coords ] returns [ BaseNode res = env.initNode() ]
  { BaseNode type = env.getNodeRoot(); }
  : res=replaceNodeOcc
  | res=anonReplaceNode[coords]
  ;

anonReplaceNode [ Coords coords ] returns [ BaseNode res = env.initNode() ]
  { BaseNode type = env.getNodeRoot(); }
  : (COLON  (type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN))? {
	IdentNode id = env.defineAnonymousEntity("node", coords);
	res = new NodeDeclNode(id, type);
  }
  ;


/**
 * The declaration of replacement node(s)
 * It can look like
 *
 * 1) a:X <old_node>
 * 2) a:X
 *
 * In the second case, always the first node is returned.
 */
replaceNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		List ids = new LinkedList();
		IdentNode id,oldid=null;
		BaseNode type;
		BaseNode constr = TypeExprNode.getEmpty();
	}
  : id=entIdentDecl COLON ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN ) (LT oldid=entIdentUse GT)? {
       if(oldid==null) {
           res = new NodeDeclNode(id, type, constr);
       } else {
           res = new NodeTypeChangeNode(id, type, oldid);
       }
	}
	;

/**
 * The declaration of node(s)
 * It can look like
 *
 * a:X \ (Z+Y) [prio=1]
 *
 * In the second case, always the first node is returned.
 */
patternNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id,type;
		BaseNode constr = TypeExprNode.getEmpty();
	}
    : id=entIdentDecl COLON ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN ) (constr=typeConstraint)? {
        res = new NodeDeclNode(id, type, constr);
	}
	;
	
replaceEdgeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id, type, oldid=null;
		BaseNode constr = TypeExprNode.getEmpty();
	}
    : id=entIdentDecl COLON ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN ) (LT oldid=entIdentUse GT)? {
       if(oldid==null) {
           res = new EdgeDeclNode(id, type, constr);
       } else {
           res = new EdgeTypeChangeNode(id, type, oldid);
       }
	}
	;

patternEdgeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id, type;
		BaseNode constr = TypeExprNode.getEmpty();
	}
	: id=entIdentDecl COLON ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN ) (constr=typeConstraint)? {
		res = new EdgeDeclNode(id, type, constr);
	}
	;

typeConstraint returns [ BaseNode constr = env.initNode() ]
  : BACKSLASH constr=typeUnaryExpr
  ;

typeAddExpr returns [ BaseNode res = env.initNode() ]
  {
    BaseNode op;
  }
  : res=typeIdentUse { res = new TypeConstraintNode(res); }
    (t:PLUS op=typeUnaryExpr {
      res = new TypeExprNode(getCoords(t), TypeExprNode.UNION, res, op);
    })*
  ;

typeUnaryExpr returns [ BaseNode res = env.initNode() ]
  : res=typeIdentUse { res = new TypeConstraintNode(res); }
  | LPAREN res=typeAddExpr RPAREN
  ;
  
