header {
/**
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss
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
  	  
  	  for(Iterator it = modelList.iterator(); it.hasNext();) {
  	    String modelFile = (String) it.next();
  	  
  	    FileInputStream modelStream = env.openModel(modelFile);
  	    GRLexer lexer = new GRLexer(modelStream);
  	    GRTypeParser parser = new GRTypeParser(lexer);
  	    parser.setEnv(env);

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
  		CollectNode neg = new CollectNode();
  		CollectNode cond = new CollectNode();
  	}
  	: TEST id=actionIdentDecl pushScope[id] LBRACE!
  	  pattern=patternPart
      ( condPart[cond] )?
      ( negativePart[neg] )* {
      id.setDecl(new TestDeclNode(id, pattern, neg, cond));
      res = id;
  	} RBRACE! popScope!;

ruleDecl returns [ BaseNode res = env.initNode() ]
  {
  		IdentNode id;
  		BaseNode rb, left, right;

  		CollectNode neg = new CollectNode();
  		CollectNode eval = new CollectNode();
  		CollectNode cond = new CollectNode();
  }
  : RULE id=actionIdentDecl pushScope[id] LBRACE!
  	left=patternPart
	( condPart[cond] )?
	( negativePart[neg] )*
  	right=replacePart
  	( evalPart[eval] )?
  	{
  	   id.setDecl(new RuleDeclNode(id, left, right, neg, cond, eval));
  	   res = id;
    }
    RBRACE! popScope!;

parameters
	: LPAREN paramList RPAREN
	| LPAREN RPAREN
	|
	;

paramList
	: param (COMMA param)*
	;
	
param
	{
		IdentNode id;
	}
	: inOutSpec paramType id=entIdentDecl
	;
	
inOutSpec
	: IN
	| OUT
	;
	
paramType
	: typeIdentUse
	;

patternPart returns [ BaseNode res = env.initNode() ]
  : p:PATTERN LBRACE! res=patternBody[getCoords(p)] RBRACE!
  ;
  
negativePart [ CollectNode negs ]
	{ BaseNode neg;	}
  : p:NEGATIVE LBRACE! neg=patternBody[getCoords(p)] { negs.addChild(neg); } RBRACE!
  ;

replacePart returns [ BaseNode res = env.initNode() ]
	: r:REPLACE LBRACE! res=replaceBody[getCoords(r)] RBRACE!
	;

evalPart [ BaseNode n ]
	: e:EVAL LBRACE evalBody[n] RBRACE
	;
	
condPart [ BaseNode n ]
	: c:COND LBRACE condBody[n] RBRACE
	;
	
condBody [ BaseNode n ]
	{ BaseNode e; }
	: (e=expr { n.addChild(e); } SEMI)*
	;
	
evalBody [ BaseNode n  ]
  { BaseNode a; }
  : (a=assignment { n.addChild(a); } SEMI)*
  ;
	
term [ BaseNode connColl ]
	{
		IdentNode edgeType;
		IdentNode attr;
	}
	: TERM LPAREN edgeType=typeIdentUse COMMA attr=entIdentUse RPAREN
			LBRACE termBody[edgeType, connColl] RBRACE
	;

/**
 * A Term body.
 * A term body is basically a node declaration with other seperated
 * node declaration in parentheses. These other decls are connected
 * with edges of a certain type to the "root".
 */
termBody [ IdentNode edgeType, BaseNode connColl ] returns [ BaseNode res = env.initNode(); ]
	{
		CollectNode childs = new CollectNode();
		List coords = new LinkedList();
		BaseNode child;
	}
	: res=patternNodeDecl l:LPAREN child=termBody[edgeType, connColl] {
			childs.addChild(child);
			coords.add(getCoords(l));
		}
		
		(comma:COMMA child=termBody[edgeType, connColl] {
			childs.addChild(child);
			coords.add(getCoords(comma));
		})* RPAREN {
				
				int n = childs.children();
				for(int i = 0; i < n; i++)  {
					Coords coord = (Coords) coords.get(i);
					BaseNode c = childs.getChild(i);
					BaseNode edge = env.defineAnonymousEntity("edge", coord);
					ConnectionNode conn = new ConnectionNode(res, edge, c);
					connColl.addChild(conn);
				}
			}
	;

/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
patternBody [ Coords coords ] returns [ BaseNode res = env.initNode() ]
    {
  		BaseNode s;
  		CollectNode connections = new CollectNode();
			res = new PatternNode(coords, connections);
    }
    : ( patternStmt[connections] SEMI )*
  ;

patternStmt [ BaseNode connCollect ]
	{ BaseNode n, o; }
	: patternConnections[connCollect]
	| NODE patternNodeDecl ( COMMA patternNodeDecl )*
	| term[connCollect]
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
  	List ids = null;
  }
  : (multiNodeDecl) => res=multiNodeDecl
  | LPAREN { ids = new LinkedList(); } id=entIdentDecl { ids.add(id); }
    (TILDE id=entIdentDecl {	ids.add(id); })+ RPAREN COLON type=typeIdentUse {
    	
    	IdentNode[] idents = (IdentNode[]) ids.toArray(new IdentNode[0]);
    	BaseNode[] colls = new BaseNode[idents.length];
    	NodeDeclNode[] decls = new NodeDeclNode[idents.length];

			for(int i = 0; i < idents.length; i++) {
				colls[i] = new CollectNode();
				decls[i] = new NodeDeclNode(idents[i], type, colls[i]);
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
  	  	res = new PatternNode(coords, connections);
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
  | res=entIdentUse c:DOUBLECOLON id=typeIdentUse {
  	res = new NodeTypeChangeNode(getCoords(c), res, id);
  }
  ;

anonymousEdge returns [ BaseNode res = null ]
	: MINUS m:RARROW {
		IdentNode id = env.defineAnonymousEntity("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, env.getEdgeRoot());
	}
	;

replaceNodeDecl returns [ BaseNode res = env.initNode() ]
  {
    IdentNode id;
  	BaseNode type;
  }
  : res=multiNodeDecl
  ;

/**
 * The declaration of node(s)
 * It can look like
 *
 * 1) a:X
 * 2) (a, b, c, d, e):X
 *
 * In the second case, always the first node is returned.
 */
multiNodeDecl returns [ BaseNode res = env.initNode() ]
	{
		List ids = new LinkedList();
		IdentNode id;
		BaseNode type;
	}
	: id=entIdentDecl COLON type=typeIdentUse {
		res = new NodeDeclNode(id, type);
	}
  | LPAREN { ids = new LinkedList(); } id=entIdentDecl { ids.add(id); }
    (COMMA id=entIdentDecl { ids.add(id); })* RPAREN COLON type=typeIdentUse {

    	int i = 0;
    	for(Iterator it = ids.iterator(); it.hasNext(); i++) {
    		IdentNode ident = (IdentNode) it.next();
    		if(i == 0)
    			res = new NodeDeclNode(ident, type);
    		else
    			// This is ok, the object does not vanish, since it is
    			// held by its identifier which is held by the symbol's
    			// occurrence whcih is held by the scope.
					new NodeDeclNode(ident, type);
    	}
    	
    }
	;
	
edgeDecl returns [ EdgeDeclNode res = null ]
	{
		IdentNode id, type;
	}
	: id=entIdentDecl COLON type=typeIdentUse {
		res = new EdgeDeclNode(id, type);
	}
	;


/*identList [ BaseNode c ]
	{ BaseNode id; }
	: id=identUse { c.addChild(id); } (COMMA id=identUse { c.addChild(id); })*
	;*/


