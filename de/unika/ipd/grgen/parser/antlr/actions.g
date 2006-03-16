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
  	    String modelFile = (String) it.next();
  	  
  	    FileInputStream modelStream = env.openModel(modelFile);
  	    GRLexer lexer = new GRLexer(modelStream);
  	    GRTypeParser parser = new GRTypeParser(lexer);
  	    parser.setEnv(env);
  	    parser.setFilename(modelFile);

  	    BaseNode model = parser.text();
  	    modelChilds.addChild(model);
  	    if(parser.hadError)
  	      hadError = true;
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
  		CollectNode negs = new CollectNode();
  	}
  	: TEST id=actionIdentDecl pushScope[id] parameters LBRACE!
  	  pattern=patternPart[negs]
  	  {
      	id.setDecl(new TestDeclNode(id, pattern, negs));
      	res = id;
  	  } RBRACE! popScope!;

ruleDecl returns [ BaseNode res = env.initNode() ]
  {
  		IdentNode id;
  		BaseNode rb, left, right;
  		CollectNode params;

  		CollectNode negs = new CollectNode();
  		CollectNode eval = new CollectNode();
  }
  : RULE id=actionIdentDecl pushScope[id] params=parameters LBRACE!
  	left=patternPart[negs]
  	right=replacePart
  	( evalPart[eval] )?
  	{
  	   id.setDecl(new RuleDeclNode(id, left, right, negs, eval, params));
  	   res = id;
    }
    RBRACE! popScope!;

parameters returns [ CollectNode res = new CollectNode() ]
	: LPAREN paramList[res] RPAREN
	| LPAREN RPAREN
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
	: NODE id=entIdentDecl COLON type=typeIdentUse
	  { res = new NodeDeclNode(id, type, TypeExprNode.getEmpty()); }
	| EDGE id=entIdentDecl COLON type=typeIdentUse
	  { res = new EdgeDeclNode(id, type, TypeExprNode.getEmpty()); }
	;

patternPart [ BaseNode negsCollect ] returns [ BaseNode res = env.initNode() ]
  : p:PATTERN LBRACE! res=patternBody[getCoords(p), negsCollect] RBRACE!
  ;
  
replacePart returns [ BaseNode res = env.initNode() ]
	: r:REPLACE LBRACE! res=replaceBody[getCoords(r)] RBRACE!
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
		res = new PatternGraphNode(coords, connections, conditions);
		int negCounter = 0;
  }
  : (negCounter = patternStmt[connections, conditions, negsCollect, negCounter])*
  ;

patternStmt [ BaseNode connCollect, BaseNode condCollect,
  BaseNode negsCollect, int negCount ] returns [ int newNegCount ]
  
	{
  	IdentNode id = env.getDummyIdent();
		BaseNode n, o, e, neg;
		//nesting of negative Parts is not allowed.
		CollectNode negsInNegs = new CollectNode();
		
		newNegCount = negCount;
	}
	: patternConnections[connCollect] SEMI
	| NODE patternNodeDecl ( COMMA patternNodeDecl )* SEMI
	
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
	;

patternConnections [ BaseNode connColl ]
  { BaseNode n; }
  : n=patternNodeOcc (patternContinuation[n,connColl] | {
  	connColl.addChild(new SingleNodeConnNode(n));
  })
  ;

/**
 * A continuation is a list of edge node pairs or a list of these pair lists, comma
 * seperated and delimited by parantheses.
 * all produced connection nodes are appended to the collect node
 */
patternContinuation [ BaseNode left, BaseNode collect ]
  { BaseNode n; }
  : n=patternPair[left, collect] (patternContinuation[n, collect])?
  | LPAREN patternContinuation[left, collect]
    ( COMMA patternContinuation[left, collect] )* RPAREN
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
	{ BaseNode e; }
	:	e=patternEdge res=patternNodeOcc {
			coll.addChild(new ConnectionNode(left, e, res));
	  }
	| e=patternReversedEdge res=patternNodeOcc {
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
  

patternReversedEdge returns [ BaseNode res = null ]
  {
  	BaseNode type = env.getEdgeRoot();
  }
  : LARROW res=edgeDecl MINUS
  | LARROW res=entIdentUse MINUS
  | LARROW (COLON type=typeIdentUse)? m:MINUS {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, type);
    }
  ;

patternEdge returns [ BaseNode res = null ]
	{
		BaseNode type = env.getEdgeRoot();
	}
  : MINUS res=edgeDecl RARROW
  | MINUS res=entIdentUse RARROW
  | MINUS (COLON type=typeIdentUse)? m:RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, type);
  }
  ;

/**
 * In a pattern, a node decl is like multiNodeDecl (see below) or
 * a multi node with tildes instead of commas, like
 *
 * (a ~ b ~ c ~ d):X
 *
 * This allows a, b, c, d to be the same node.
 */
 patternNodeDecl returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id;
  	BaseNode type;
  	BaseNode constr = TypeExprNode.getEmpty();
  	List ids = null;
  }
  : (multiNodeDecl) => res=multiNodeDecl
  | LPAREN { ids = new LinkedList(); } id=entIdentDecl { ids.add(id); }
    (TILDE id=entIdentDecl {	ids.add(id); })+ RPAREN COLON
    type=typeIdentUse (constr=typeConstraint)? {
    	
    	IdentNode[] idents = (IdentNode[]) ids.toArray(new IdentNode[0]);
    	BaseNode[] colls = new BaseNode[idents.length];
    	NodeDeclNode[] decls = new NodeDeclNode[idents.length];

			for(int i = 0; i < idents.length; i++) {
				colls[i] = new CollectNode();
				decls[i] = new NodeDeclNode(idents[i], type, constr, colls[i]);
			}

			//Add homomorphic nodes
			for(int i = 0; i < idents.length; i++)
				for(int j = i + 1; j < idents.length; j++)
					colls[i].addChild(idents[j]);
    }
  ;

/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
replaceBody [ Coords coords ] returns [ BaseNode res = env.initNode() ]
    {
  	  	BaseNode s;
  		CollectNode connections = new CollectNode();
  	  	res = new GraphNode(coords, connections);
    }
    : ( replaceStmt[connections] SEMI )*
  ;

replaceStmt [ BaseNode connCollect ]
	{ BaseNode n; }
	: replaceConnections[connCollect]
	| NODE replaceNodeDecl ( COMMA replaceNodeDecl )*
	;

replaceConnections [ BaseNode connColl ]
	{ BaseNode n; }
	: n=replaceNodeOcc (replaceContinuation[n, connColl] | {
		connColl.addChild(new SingleNodeConnNode(n));
  	})
  ;

/**
 * Acontinuation is a list of edge node pairs or a list of these pair lists, comma
 * seperated and delimited by parantheses.
 * all produced connection nodes are appended to the collect node
 */
replaceContinuation [ BaseNode left, BaseNode collect ]
  { BaseNode n; }
  : n=replacePair[left, collect] (replaceContinuation[n, collect])?
  | LPAREN replaceContinuation[left, collect]
    ( COMMA replaceContinuation[left, collect] )* RPAREN
  ;

/**
 * An edge node pair.
 * This rule builds a connection node with the parameter left, the edge and the nodeOcc
 * and appends this connection node to the children of coll.
 * The rule returns the right node (the one from the nodeOcc rule)
 */
replacePair [ BaseNode left, BaseNode coll ] returns [ BaseNode res = env.initNode() ]
	{ BaseNode e; }
	: e=replaceEdge res=replaceNodeOcc {
		coll.addChild(new ConnectionNode(left, e, res));
  	}
	| e=replaceReversedEdge res=replaceNodeOcc {
  		coll.addChild(new ConnectionNode(res, e, left));
    };

replaceEdge returns [ BaseNode res = null ]
	{ BaseNode type = env.getEdgeRoot(); }
	: MINUS res=edgeDecl RARROW
	| MINUS res=entIdentUse RARROW
	| m:MINUS (COLON type=typeIdentUse)? RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, type);
	}
	;

replaceReversedEdge returns [ BaseNode res = null ]
	{ BaseNode type = env.getEdgeRoot(); }
	: LARROW res=edgeDecl MINUS
	| LARROW res=entIdentUse MINUS
	| m:LARROW(COLON type=typeIdentUse)? MINUS {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, type);
	}
	;

/**
 * The occurrence of a node.
 * A node occurrence is either the declaration of a new node, a usage of
 * a already declared node or a usage combined with a type change.
 */
replaceNodeOcc returns [ BaseNode res = env.initNode() ]
	{
		CollectNode coll = new CollectNode();
		IdentNode id;
	}
  : res=entIdentUse
  | res=replaceNodeDecl
  ;

anonymousEdge returns [ BaseNode res = null ]
	: MINUS m:RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, env.getEdgeRoot());
	}
	;
  
/**
 * The declaration of replacement node(s)
 * It can look like
 *
 * 1) a:X <old_node>
  * 2) (a, b, c, d, e):X
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
  : id=entIdentDecl COLON type=typeIdentUse (LT oldid=entIdentUse GT)? {
       if(oldid==null) {
           res = new NodeDeclNode(id, type, constr);
       } else {
           res = new NodeTypeChangeNode(id, type, oldid);
       }
	}
  | LPAREN { ids = new LinkedList(); } id=entIdentDecl { ids.add(id); }
    (COMMA id=entIdentDecl { ids.add(id); })* RPAREN
     COLON type=typeIdentUse {

    	int i = 0;
    	for(Iterator it = ids.iterator(); it.hasNext(); i++) {
    		IdentNode ident = (IdentNode) it.next();
    		if(i == 0)
    			res = new NodeDeclNode(ident, type, constr);
    		else
    			// This is ok, the object does not vanish, since it is
    			// held by its identifier which is held by the symbol's
    			// occurrence which is held by the scope.
					new NodeDeclNode(ident, type, constr);
    	}
    	
    }
	;

/**
 * The declaration of node(s)
 * It can look like
 *
 * 1) a:X \ (Z+Y)
 * 2) (a, b, c, d, e):X \ (A+B+Z)
 *
 * In the second case, always the first node is returned.
 */
multiNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		List ids = new LinkedList();
		IdentNode id;
		BaseNode type;
		BaseNode constr = TypeExprNode.getEmpty();
	}
  : id=entIdentDecl COLON type=typeIdentUse (constr=typeConstraint)? {
        res = new NodeDeclNode(id, type, constr);
	}
  | LPAREN { ids = new LinkedList(); } id=entIdentDecl { ids.add(id); }
    (COMMA id=entIdentDecl { ids.add(id); })* RPAREN
     COLON type=typeIdentUse (constr=typeConstraint)? {

    	int i = 0;
    	for(Iterator it = ids.iterator(); it.hasNext(); i++) {
    		IdentNode ident = (IdentNode) it.next();
    		if(i == 0)
    			res = new NodeDeclNode(ident, type, constr);
    		else
    			// This is ok, the object does not vanish, since it is
    			// held by its identifier which is held by the symbol's
    			// occurrence which is held by the scope.
					new NodeDeclNode(ident, type, constr);
    	}
    	
    }
	;
	
edgeDecl returns [ EdgeDeclNode res = null ]
	{
		IdentNode id, type;
		BaseNode constr = TypeExprNode.getEmpty();
	}
	: id=entIdentDecl COLON type=typeIdentUse (constr=typeConstraint)? {
		res = new EdgeDeclNode(id, type, constr);
	}
	;

typeConstraint returns [ BaseNode constr = env.initNode() ]
//  : WITHOUT LPAREN constr=typeExpr RPAREN
  : BACKSLASH LPAREN constr=typeExpr RPAREN
  ;

typeExpr returns [ BaseNode constr = env.initNode() ]
  : constr=typeAddExpr
  ;
  
typeAddExpr returns [ BaseNode res = env.initNode() ]
  {
    int op = -1;
    BaseNode op1;
    Token t = null;
  }
  : res=typeMulExpr (t=typeAddOp op1=typeMulExpr {
      switch(t.getType()) {
      case PLUS:
        op = TypeExprNode.UNION;
        break;
      case BACKSLASH:
        op = TypeExprNode.DIFFERENCE;
        break;
      }
      res = new TypeExprNode(getCoords(t), op, res, op1);
    })*
  ;
  
typeAddOp returns [ Token t = null ]
  : pl:PLUS { t = pl; }
  | mi:BACKSLASH { t = mi; }
  ;

typeMulExpr returns [ BaseNode res = env.initNode() ]
  { BaseNode op1; }
  : res=typeUnaryExpr (a:AMPERSAND op1=typeUnaryExpr {
      res = new TypeExprNode(getCoords(a), TypeExprNode.INTERSECT, res, op1);
    })*
  ;
  
typeUnaryExpr returns [ BaseNode res = env.initNode() ]
  { BaseNode n = env.initNode(); }
  : c:COLON n=typeIdentUse {
      res = new TypeExprSubtypeNode(getCoords(c), n);
    }
  | b:LBRACE { n = new CollectNode(); } typeIdentList[n] RBRACE {
      res = new TypeConstNode(getCoords(b), n);
    }
  | LPAREN res=typeExpr RPAREN
  | n=typeIdentUse { res = new TypeConstNode(n); }
  ;
  
typeIdentList [ BaseNode addTo ]
  { BaseNode n; }
  : n=typeIdentUse { addTo.addChild(n); } (COMMA n=typeIdentUse {
      addTo.addChild(n);
    })*
  |
  ;


