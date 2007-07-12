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
  	: ACTIONS id=entIdentDecl {
  	  		if ( ! (id.toString() + ".grg").equals(env.getFilenameWithoutPath()) ) {
  	  			id.reportError("filename \"" + env.getFilenameWithoutPath() +
  	  				"\" does not conform with name \"" + id + "\" of this action set");
  	  		}
  	  }
  	  (USING identList[modelList])? SEMI {
  	  
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
	: MINUS res=patEdgeDecl RARROW
	| res=patNodeDecl
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
  : (negCounter = patternStmt[connections, conditions, negsCollect, negCounter, returnz, homs])*
  ;

patternStmt [ BaseNode connCollect, BaseNode condCollect,
  BaseNode negsCollect, int negCount, CollectNode returnz, CollectNode homs ] returns [ int newNegCount ]
  
	{
  		IdentNode id = env.getDummyIdent();
    	BaseNode n, o, e, neg, hom;
    	//nesting of negative Parts is not allowed.
    	CollectNode negsInNegs = new CollectNode();

    	newNegCount = negCount;
	}
	: patConnections[connCollect] SEMI
	
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

patConnections [ BaseNode connColl ]
  {
    BaseNode n,e;
    boolean forward = true;
    NodeDeclNode dummyNode = env.getDummyNodeDecl();
  }
  : ( e=patForwardEdgeOcc { forward=true; } | e=patBackwardEdgeOcc { forward=false; } )
    (
        n=patNodeContinuation[connColl] {
        	/* the edge declared by <code>e</code> dangles on the left */
        	if (forward)
        		connColl.addChild(new ConnectionNode(dummyNode, e, n));
        	else
        		connColl.addChild(new ConnectionNode(n, e, dummyNode));
        }
      | /* both target and source of the edge <code>e</code> dangle */ {
    		connColl.addChild(new ConnectionNode(dummyNode, e, dummyNode));
        }
    )
  | n=patNodeOcc ( patEdgeContinuation[n, connColl] | {
    	connColl.addChild(new SingleNodeConnNode(n));
    })
  ;

patNodeContinuation [ BaseNode collect ] returns [ BaseNode res = env.initNode() ]
  { BaseNode n; }
  : res=patNodeOcc ( patEdgeContinuation[res, collect] )?
  ;

patEdgeContinuation [ BaseNode left, BaseNode collect ]
  {
    BaseNode n,e;
	boolean forward = true;
  }
  : ( e=patForwardEdgeOcc { forward=true; } | e=patBackwardEdgeOcc { forward=false; } )
    (
    	  n=patNodeContinuation[collect] {
    	  		if (forward)
    	  			collect.addChild(new ConnectionNode(left, e, n));
    	  		else
    	  			collect.addChild(new ConnectionNode(n, e, left));
    	  }
    	| /* the edge declared by <code>res</code> dangles on the right */ {
			    NodeDeclNode dummyNode = env.getDummyNodeDecl();
    	  		if (forward)
    	  			collect.addChild(new ConnectionNode(left, e, dummyNode));
    	  		else
    	  			collect.addChild(new ConnectionNode(dummyNode, e, left));
    	  }
    )
  ;

patNodeOcc returns [ BaseNode res = env.initNode() ]
  : res=patAnonNodeOcc
  | res=patKnownNodeOcc
  ;

patAnonNodeOcc returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id = env.getDummyIdent();
  	BaseNode type = env.getNodeRoot();
	BaseNode constr = TypeExprNode.getEmpty();
	Attributes attrs;
  }
  :
    (
        d:DOT {
	    	id = env.defineAnonymousEntity("node", getCoords(d));
	    	res = new NodeDeclNode(id, type, constr);
	    }
      | c:COLON (type=typeIdentUse (constr=typeConstraint)?| TYPEOF LPAREN type=entIdentUse RPAREN) {
			id = env.defineAnonymousEntity("node", getCoords(c));
			res = new NodeDeclNode(id, type, constr);
	    }
    )
    (attrs=attributes { id.setAttributes(attrs); })?
  ;

patKnownNodeOcc returns [ BaseNode res = env.initNode() ]
  : res = entIdentUse
  | res = patNodeDecl
  ;

patNodeDecl returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id, type;
    BaseNode constr = TypeExprNode.getEmpty();
  }
  : id=entIdentDecl COLON
    ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
    (constr=typeConstraint)? {
    	res = new NodeDeclNode(id, type, constr);
    }
  ;

patForwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  {
    BaseNode type = env.getEdgeRoot();
    BaseNode constr = TypeExprNode.getEmpty();
  }
  : MINUS res=patEdgeDecl RARROW
  | MINUS res=entIdentUse RARROW
  | MINUS m:COLON (type=typeIdentUse (constr=typeConstraint)? | TYPEOF LPAREN type=entIdentUse RPAREN) RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new EdgeDeclNode(id, type, constr);
    }
  | mm:DOUBLE_RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
		res = new EdgeDeclNode(id, type, constr);
    }
  ;

patBackwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  {
    BaseNode type = env.getEdgeRoot();
    BaseNode constr = TypeExprNode.getEmpty();
  }
  : LARROW res=patEdgeDecl MINUS
  | LARROW res=entIdentUse MINUS
  | LARROW m:COLON (type=typeIdentUse (constr=typeConstraint)? | TYPEOF LPAREN type=entIdentUse RPAREN) MINUS {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new EdgeDeclNode(id, type, constr);
    }
  | mm:DOUBLE_LARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
		res = new EdgeDeclNode(id, type, constr);
    }
  ;

patEdgeDecl returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id, type;
    BaseNode constr = TypeExprNode.getEmpty();
  }
  : id=entIdentDecl COLON
    ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
    (constr=typeConstraint)?
    { res = new EdgeDeclNode(id, type, constr); }
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
	: replConnections[connections] SEMI
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
	: replConnections[connections] SEMI
	| replaceReturns[returnz] SEMI
    | deleteStmt[dels] SEMI
    | evalPart[eval]
	;

replConnections [ BaseNode connColl ]
  {
    BaseNode n,e;
    boolean forward = true;
    NodeDeclNode dummyNode = env.getDummyNodeDecl();
  }
  : (
        e=replForwardEdgeOcc { forward=true; }
      | e=replBackwardEdgeOcc { forward=false; }
    )
    {
		if ( ! e.isKept() ) reportError(e.getCoords(),
				"dangling edges in replace/modify part must already " +
				"occur in the pattern part");
    }
    (
        n=replNodeContinuation[connColl] {
        	/* the edge declared by <code>e</code> dangles on the left */
        	if (forward)
        		connColl.addChild(new ConnectionNode(dummyNode, e, n));
        	else
        		connColl.addChild(new ConnectionNode(n, e, dummyNode));
        }
      | /* the edge declared by <code>e</code> dangles on both sides */ {
    		connColl.addChild(new ConnectionNode(dummyNode, e, dummyNode));
        }
    )
  | n=replNodeOcc ( replEdgeContinuation[n, connColl] | {
    	connColl.addChild(new SingleNodeConnNode(n));
    })
  ;

replNodeContinuation [ BaseNode collect ] returns [ BaseNode res = env.initNode() ]
  { BaseNode n; }
  : res=replNodeOcc ( replEdgeContinuation[res, collect] )?
  ;

replEdgeContinuation [ BaseNode left, BaseNode collect ]
  {
    BaseNode n,e;
	boolean forward = true;
  }
  : (
        e=replForwardEdgeOcc { forward=true; }
      | e=replBackwardEdgeOcc { forward=false; }
    )
    (
    	  n=replNodeContinuation[collect] {
    	  		if (forward)
    	  			collect.addChild(new ConnectionNode(left, e, n));
    	  		else
    	  			collect.addChild(new ConnectionNode(n, e, left));
    	  }
    	| /* the edge declared by <code>res</code> dangles on the right */ {
			    NodeDeclNode dummyNode = env.getDummyNodeDecl();

			    if (! e.isKept() ) reportError(e.getCoords(),
			    	"dangling edges in replace/modify part must already " +
			    	"occur in the pattern part");
			    
    	  		if (forward)
    	  			collect.addChild(new ConnectionNode(left, e, dummyNode));
    	  		else
    	  			collect.addChild(new ConnectionNode(dummyNode, e, left));
    	  }
    )
  ;

replNodeOcc returns [ BaseNode res = env.initNode() ]
  : res=replAnonNodeOcc
  | res=replKnownNodeOcc
  ;

replAnonNodeOcc returns [ BaseNode res = env.initNode() ]
  {
    BaseNode type = env.getNodeRoot();
    IdentNode id = env.getDummyIdent();
    IdentNode oldid = null;
  }
  : d:DOT {
		id = env.defineAnonymousEntity("node", getCoords(d));
		res = new NodeDeclNode(id, type);
    }
  | c:COLON (type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN) {
		id = env.defineAnonymousEntity("node", getCoords(c));
	}
    (LT oldid=entIdentUse GT)? {
    	if(oldid==null) {
    		res = new NodeDeclNode(id, type);
    	} else {
    		res = new NodeTypeChangeNode(id, type, oldid);
    	}
	}
  ;

replKnownNodeOcc returns [ BaseNode res = env.initNode() ]
  : res = entIdentUse
  | res = replNodeDecl
  ;

replNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id, oldid=null;
		BaseNode type;
	}
  : id=entIdentDecl COLON
    ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
    (LT oldid=entIdentUse GT)? {
       if(oldid==null) {
           res = new NodeDeclNode(id, type);
       } else {
           res = new NodeTypeChangeNode(id, type, oldid);
       }
	}
	;

replForwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  { BaseNode type = env.getEdgeRoot(); }
  : MINUS res=entIdentUse RARROW { res.setKept(true); }
  | MINUS res=replEdgeDecl RARROW
  | mm:DOUBLE_RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
		res = new EdgeDeclNode(id, type);
    }
  ;

replBackwardEdgeOcc returns [ BaseNode res = env.initNode() ]
  { BaseNode type = env.getEdgeRoot(); }
  : LARROW res=entIdentUse MINUS { res.setKept(true); }
  | LARROW res=replEdgeDecl MINUS
  | mm:DOUBLE_LARROW {
    	IdentNode id = env.defineAnonymousEntity("edge", getCoords(mm));
    	res = new EdgeDeclNode(id, type);
    }
  ;

replEdgeDecl returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id = env.getDummyIdent(), type, oldid = null;
    boolean anonymous = false;
  }
  : ( id=entIdentDecl | { anonymous = true; } )
    d:COLON {
    	if (anonymous) id = env.defineAnonymousEntity("edge", getCoords(d));
    }
    ( type=typeIdentUse | TYPEOF LPAREN type=entIdentUse RPAREN )
    (LT oldid=entIdentUse GT {res.setKept(true);} )?
    {
       if( oldid == null )
           res = new EdgeDeclNode(id, type);
       else
           res = new EdgeTypeChangeNode(id, type, oldid);
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
  

