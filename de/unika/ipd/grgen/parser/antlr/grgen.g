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
		
	import de.unika.ipd.grgen.parser.Symbol;
	import de.unika.ipd.grgen.parser.SymbolTable;
	import de.unika.ipd.grgen.parser.Scope;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.util.report.*;
	import de.unika.ipd.grgen.util.*;
	import de.unika.ipd.grgen.Main;
}


/**
 * GRGen grammar
 * @version 0.1
 * @author Sebastian Hack
 */
class GRParser extends Parser;
options {
  k=3;
	//codeGenMakeSwitchThreshold = 2;
	//codeGenBitsetTestThreshold = 3;
	defaultErrorHandler = true;
	buildAST = false;
}


tokens {
    DECL_GROUP;
    DECL_TEST;
    DECL_TYPE;
    DECL_NODE;
    DECL_EDGE;
    DECL_BASIC;
    TYPE_NODE;
    TYPE_EDGE;
    SIMPLE_CONN;
    GROUP_CONN;
    TEST_BODY;
    PATTERN_BODY;
    SUBGRAPH_SPEC;
    CONN_DECL;
    CONN_CONT;
    MAIN;
}

{
		private ConstNode one = new IntConstNode(Coords.getBuiltin(), 1);
	
		private ConstNode zero = new IntConstNode(Coords.getBuiltin(), 0);

		/// did we encounter errors during parsing
		private boolean hadError;
	
		/// the current scope
    private Scope currScope;
    
    /// The root scope
    private Scope rootScope;
    
    /// The symbol table
    private SymbolTable symbolTable;

		/// the error reporter
    private ErrorReporter reporter;
   
    /// unique ids for anonymous identifiers
    private static int uniqueId = 1;
    
    /// a dummy identifier
    private IdentNode dummyIdent;

		private IdentNode edgeRoot, nodeRoot;

		private CollectNode mainChilds;

		private static Map opIds = new HashMap();

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
		};
    
  
    private OpNode makeOp(antlr.Token t) {
    	Coords c = new Coords(t, this);
			Integer opId = (Integer) opIds.get(new Integer(t.getType()));
			assert opId != null : "Invalid operator ID";
    	return new ArithmeticOpNode(getCoords(t), opId.intValue());
    }
    
    private OpNode makeBinOp(antlr.Token t, BaseNode op0, BaseNode op1) {
    	OpNode res = makeOp(t);
    	res.addChild(op0);
    	res.addChild(op1);
    	return res;
    }
    
    private OpNode makeUnOp(antlr.Token t, BaseNode op) {
    	OpNode res = makeOp(t);
    	res.addChild(op);
    	return res;
    }
    
    private int makeId() {
    	return uniqueId++;
    }
    
    private BaseNode initNode() {
    	return BaseNode.getErrorNode();
    }

		private IdentNode getDummyIdent() {
			return IdentNode.getInvalid();
		}

		private Coords getCoords(antlr.Token tok) {
			return new Coords(tok, this);
		}
		
    private Symbol.Definition define(String name, Coords coords) {
      Symbol sym = symbolTable.get(name);
      return currScope.define(sym, coords);
    }
    
    private IdentNode defineAnonymous(String part, Coords coords) {
    	return new IdentNode(currScope.defineAnonymous(part, coords));
    }
    
    private IdentNode predefine(String name) {
      Symbol sym = symbolTable.get(name);
      return new IdentNode(rootScope.define(sym, BaseNode.BUILTIN));
    }
    
    private IdentNode predefineType(String str, TypeNode type) {
    	IdentNode id = predefine(str);
    	id.setDecl(new TypeDeclNode(id, type));
    	return id;
    }
    
    private void makeBuiltin() {
    	nodeRoot = predefineType("Node", 
    	  new NodeTypeNode(new CollectNode(), new CollectNode(), 0));
			edgeRoot = predefineType("Edge", 
			  new EdgeTypeNode(new CollectNode(), new CollectNode(),  new CollectNode(), 0));
			
			predefineType("int", BasicTypeNode.intType);
			predefineType("string", BasicTypeNode.stringType);
			predefineType("boolean", BasicTypeNode.booleanType);
    }
    
    public void init(ErrorReporter reporter) {
    	this.reporter = reporter;
    	hadError = false;
      symbolTable = new SymbolTable();
      
      symbolTable.enterKeyword("int");
      symbolTable.enterKeyword("string");
      symbolTable.enterKeyword("boolean");
      
      rootScope = new Scope(reporter);
      currScope = rootScope;
      
      makeBuiltin();
      mainChilds = new CollectNode();
			mainChilds.addChild(edgeRoot.getDecl());
			mainChilds.addChild(nodeRoot.getDecl());
    }
    
	  public void reportError(RecognitionException arg0) {
  	  hadError = true;
	  	reporter.error(new Coords(arg0), arg0.getErrorMessage());
    }

	  public void reportError(String arg0) {
	  	hadError = true;
			reporter.error(arg0);
  	}
  	
  	public void reportWarning(String arg0) {
  		reporter.warning(arg0);
  	}
  	
  	public boolean hadError() {
  		return hadError;
  	}
}


/**
 * Build a main node.
 * It has a collect node with the decls as child
 */
text returns [ BaseNode main = initNode() ]
    {
  		CollectNode n;
  		IdentNode id;
  	}
  	: "unit" id=identDecl SEMI n=decls EOF { mainChilds.addChildren(n); } {
  		main = new UnitNode(id, getFilename());
  		main.addChild(mainChilds);
  		
  		// leave the root scope and finish all occurrences
  		currScope.leaveScope();
  	}
  	;

/**
 * Decls make a collect node with all the decls as children
 */
decls returns [ CollectNode n = new CollectNode() ]
  { BaseNode d; }
  : (d=decl { n.addChild(d); } )* ;

/**
 * A decl is a
 * - group
 * - edge type
 * - node type
 */
decl returns [ BaseNode res = initNode() ]
  : res=actionDecl
  | res=classDecl
  | res=enumDecl
  ;

classDecl returns [ BaseNode res = initNode() ]
	{ int mods = 0; }
	: mods=typeModifiers (res=edgeClassDecl[mods] | res=nodeClassDecl[mods])
	;

typeModifiers returns [ int res = 0; ]
  { int mod = 0; }
	: (mod=typeModifier { res |= mod; })*
	;
	
typeModifier returns [ int res = 0; ]
	: "abstract" { res |= InheritanceTypeNode.MOD_ABSTRACT; }
	| "const" { res |= InheritanceTypeNode.MOD_CONST; }
	;
	

/**
 * An edge class decl makes a new type decl node with the declaring id and
 * a new edge type node as children
 */
edgeClassDecl[int modifiers] returns [ BaseNode res = initNode() ]
  {
	BaseNode body = new CollectNode(), ext, cas;
	IdentNode id;
  }
  
	: "edge" "class"! id=identDecl ext=edgeExtends cas=connectAssertions pushScope[id] 
		(LBRACE! body=edgeClassBody RBRACE! | SEMI) {

		EdgeTypeNode et = new EdgeTypeNode(ext, cas, body, modifiers);
		id.setDecl(new TypeDeclNode(id, et));
		res = id;
  } popScope!
  ;

nodeClassDecl![int modifiers] returns [ BaseNode res = initNode() ]
	{
  	BaseNode body = new CollectNode(), ext;
  	IdentNode id;
  }

	: "node" "class"! id=identDecl ext=nodeExtends pushScope[id] 
	  (LBRACE! body=nodeClassBody RBRACE! | SEMI) {

		NodeTypeNode nt = new NodeTypeNode(ext, body, modifiers);
		id.setDecl(new TypeDeclNode(id, nt));
		res = id;
	} popScope!
	;

connectAssertions returns [ CollectNode c = new CollectNode() ]
	: "connect" connectAssertion[c] ( COMMA connectAssertion[c] )*
	|
	;

connectAssertion [ CollectNode c ]
	{ BaseNode src, srcRange, edge, tgt, tgtRange; }
	: src=identUse srcRange=rangeSpec RARROW 
	  tgt=identUse tgtRange=rangeSpec
	{ c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange)); }
	;

edgeExtends returns [ CollectNode c = new CollectNode() ]
  : "extends" edgeExtendsCont[c]
  | { c.addChild(edgeRoot); }
  ;

edgeExtendsCont[ CollectNode c ]
    { BaseNode e; }
    : e=identUse { c.addChild(e); }
      (COMMA! e=identUse { c.addChild(e); } )*
	;

nodeExtends returns [ CollectNode c = new CollectNode() ]
  : "extends" nodeExtendsCont[c]
  | { c.addChild(nodeRoot); }
  ;

nodeExtendsCont[ CollectNode c ]
  { BaseNode n; }
  : n=identUse { c.addChild(n); }
    (COMMA! n=identUse { c.addChild(n); } )* ;

nodeClassBody returns [ CollectNode c = new CollectNode() ]
  { BaseNode d; }
  : (d=basicDecl { c.addChild(d); } SEMI!)*
  ;

edgeClassBody returns [ CollectNode c = new CollectNode() ]
	{ BaseNode d; }
    : (d=basicDecl { c.addChild(d); } SEMI!)*
	;
	
rangeSpec returns [ BaseNode res = initNode() ]
	{
		int lower = 0, upper = RangeSpecNode.UNBOUND;
		de.unika.ipd.grgen.parser.Coords coords = de.unika.ipd.grgen.parser.Coords.getInvalid();
	}
	: (	l:LBRACK { coords = getCoords(l); }
			( 	(STAR | PLUS { lower=1; }) | 
				lower=integerConst ( COLON ( STAR | upper=integerConst ) )? 
		) RBRACK
	  )?
	{
			res = new RangeSpecNode(coords, lower, upper);
	}
	;
	
integerConst returns [ int value = 0 ]
	: i:NUM_DEC {
		value = Integer.parseInt(i.getText());
	}
	;
	
/* constDecl returns [ BaseNode res = initNode() ]
	{
		IdentNode type, id;
		BaseNode init;
	}
	: "const" type=identUse id=identDecl ASSIGN init=expr {
		res = new ConstDeclNode(id, type, init);
	};
*/


	
enumDecl returns [ BaseNode res = initNode() ]
	{
		IdentNode id;
		BaseNode c;
	}
	: "enum" id=identDecl pushScope[id] LBRACE c=enumList {
		BaseNode enumType = new EnumTypeNode(c);
		enumType.addChild(enumType);
		id.setDecl(new TypeDeclNode(id, enumType));
		res = id;
	} RBRACE popScope;

enumList returns [ BaseNode res = initNode() ]
	{
		int pos = 0;
		res = new CollectNode();
		BaseNode init;
	}
	:	init=enumItemDecl[res, zero, pos++] (COMMA init=enumItemDecl[res, init, pos++])*
	;
	
enumItemDecl [ BaseNode coll, BaseNode defInit, int pos ] returns [ BaseNode res = initNode() ]
	{
		IdentNode id;
		BaseNode init = null;
		BaseNode value;
	}
	: id=identDecl (ASSIGN init=expr)? {

		if(init != null)
			value = init;
		else
			value = defInit;
			
		MemberDeclNode memberDecl = new EnumItemNode(id, value, pos);
		id.setDecl(memberDecl);
		coll.addChild(memberDecl);
		
		res = new ArithmeticOpNode(id.getCoords(), OperatorSignature.ADD);
		res.addChild(value);
		res.addChild(one);
	}
	;

basicDecl returns [ BaseNode res = initNode() ]
    {
  	  IdentNode id;
  	  BaseNode type;
  	  DeclNode decl;
    }
  
  : id=identDecl COLON! type=identUse {
	
	decl = new MemberDeclNode(id, type);
	id.setDecl(decl);
	res = decl;
};

/// groups have names and contain graph declarations
groupDecl returns [ BaseNode res = initNode() ]
	{
		IdentNode id;
  		CollectNode decls;
  	}
  	: "group" id=identDecl pushScope[id] LBRACE! decls=actionDecls {
	  	id.setDecl(new GroupDeclNode(id, decls));
	  	
	  	res = id;
  } RBRACE! popScope!;

actionDecls returns [ CollectNode c = new CollectNode() ]
  { BaseNode d; }
  : ( d=actionDecl { c.addChild(d); } )+;

/**
 * graph declarations contain
 * - rules
 * - tests
 */
actionDecl returns [ BaseNode res = initNode() ]
  : res=testDecl
  | res=ruleDecl
  ;

testDecl returns [ BaseNode res = initNode() ]
    {
  		IdentNode id;
  		BaseNode tb, pattern;
  		BaseNode neg = new PatternNode(Coords.getInvalid(), new CollectNode());
  		CollectNode cond = new CollectNode();
  	}
  	: "test" id=identDecl pushScope[id] LBRACE!
  	  pattern=patternPart
      ( condPart[cond] )? 
      ( neg=negativePart )? {
      id.setDecl(new TestDeclNode(id, pattern, neg, cond));
      res = id;
  	} RBRACE! popScope!;

ruleDecl returns [ BaseNode res = initNode() ]
  {
  		IdentNode id;
  		BaseNode rb, left, right;
  		BaseNode neg = new PatternNode(Coords.getInvalid(), new CollectNode());

  		CollectNode redir = new CollectNode();
  		CollectNode eval = new CollectNode();
  		CollectNode cond = new CollectNode();
  }
  : "rule" id=identDecl pushScope[id] LBRACE!
  	left=patternPart
	( condPart[cond] )?
	( neg=negativePart )?
  	right=replacePart
  	( redirectPart[redir] )?
  	( evalPart[eval] )?
  	{
  	   id.setDecl(new RuleDeclNode(id, left, right, neg, redir, cond, eval));
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
	: inOutSpec paramType id=identDecl
	;
	
inOutSpec
	: "in"
	| "out"
	;
	
paramType
	: identUse
	;

patternPart returns [ BaseNode res = initNode() ]
  : p:"pattern" LBRACE! res=patternBody[getCoords(p)] RBRACE!
  ;
  
negativePart returns [ BaseNode res = initNode() ]
  : p:"negative" LBRACE! res=patternBody[getCoords(p)] RBRACE!
  ;

replacePart returns [ BaseNode res = initNode() ]
	: r:"replace" LBRACE! res=replaceBody[getCoords(r)] RBRACE!
	;

redirectPart [ CollectNode collect ]
	: r:"redirect" LBRACE! redirectBody[collect, getCoords(r)] RBRACE!
	;

evalPart [ BaseNode n ]
	: e:"eval" LBRACE evalBody[n] RBRACE
	;
	
condPart [ BaseNode n ]
	: c:"cond" LBRACE condBody[n] RBRACE
	;
	
condBody [ BaseNode n ]
	{ BaseNode e; }
	: (e=expr { n.addChild(e); } SEMI)*
	;
	
evalBody [ BaseNode n  ]
  { BaseNode a; }
  : (a=assignment { n.addChild(a); } SEMI)*
  ;
	
redirectBody [ CollectNode c, Coords coords ]
	:	( redirectStmt[c] SEMI )*
	;
	
redirEdgeOcc returns [ Object[] res ]
	{
			BaseNode id;
			res = new Object[2];
			res[1] = new Boolean(false);
	}
	
  : MINUS id=identUse RARROW { res[0] = id; }
	| RARROW { res[0] = edgeRoot; 	}
	| LARROW id=identUse MINUS {
		res[0] = id;
		res[1] = new Boolean(true);
	}
	| LARROW {
		res[0] = edgeRoot;
		res[1] = new Boolean(true);
	}
	;
	
redirectStmt [ BaseNode c ]
	{
		BaseNode src, tgt, to;
		Object[] redirEdge;
	}

	: to=identUse COLON src=identUse redirEdge=redirEdgeOcc tgt=identUse {
		BaseNode edgeTypeId = (BaseNode) redirEdge[0];
		Boolean incoming = (Boolean) redirEdge[1];
		c.addChild(new RedirectionNode(src, edgeTypeId, tgt, to, incoming.booleanValue()));
	}
	;
	
term [ BaseNode connColl ]
	{ 
		IdentNode edgeType;
		IdentNode attr;
	}
	: "term" LPAREN edgeType=identUse COMMA attr=identUse RPAREN 
			LBRACE termBody[edgeType, connColl] RBRACE 
	;	

/**
 * A Term body.
 * A term body is basically a node declaration with other seperated
 * node declaration in parentheses. These other decls are connected 
 * with edges of a certain type to the "root".
 */
termBody [ IdentNode edgeType, BaseNode connColl ] returns [ BaseNode res = initNode(); ]
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
					BaseNode edge = defineAnonymous("edge", coord);
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
patternBody [ Coords coords ] returns [ BaseNode res = initNode() ]
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
	| "node" patternNodeDecl ( COMMA patternNodeDecl )*
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
patternPair [ BaseNode left, BaseNode coll ] returns [ BaseNode res = initNode() ]
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
patternNodeOcc returns [ BaseNode res = initNode() ]
  : res=identUse
  | res=patternNodeDecl
  ;
  

patternReversedEdge returns [ BaseNode res = null ]
  {
  	BaseNode type = edgeRoot;
  }
  : LARROW res=edgeDecl MINUS
  | LARROW (COLON type=identUse)? m:MINUS {
		IdentNode id = defineAnonymous("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, type);
    }
  ;

patternEdge returns [ BaseNode res = null ]
	{
		BaseNode type = edgeRoot;
	}
  : MINUS res=edgeDecl RARROW
  | MINUS (COLON type=identUse)? m:RARROW {
		IdentNode id = defineAnonymous("edge", getCoords(m));
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
 patternNodeDecl returns [ BaseNode res = initNode() ]
  {
    IdentNode id;
  	BaseNode type;
  	List ids = null;
  }
  : res=multiNodeDecl
  | LPAREN { ids = new LinkedList(); } id=identDecl { ids.add(id); }
    (TILDE id=identDecl {	ids.add(id); })+ RPAREN COLON type=identUse {
    	
    	IdentNode[] idents = (IdentNode[]) ids.toArray(new IdentNode[0]);
    	BaseNode[] colls = new BaseNode[idents.length];
    	NodeDeclNode[] decls = new NodeDeclNode[idents.length];

			for(int i = 0; i < idents.length; i++) {
				colls[i] = new CollectNode();
				decls[i] = new NodeDeclNode(idents[i], type, colls[i]);
			}

			//Add homomorphic nodes 
			for(int i = 0; i < idents.length; i++)
				for(int j = 0 /*i + 1*/; j < idents.length; j++)
					if (i != j) colls[i].addChild(idents[j]);
    }
  ;

/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
replaceBody [ Coords coords ] returns [ BaseNode res = initNode() ]
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
	| "node" replaceNodeDecl ( COMMA replaceNodeDecl )*
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
replacePair [ BaseNode left, BaseNode coll ] returns [ BaseNode res = initNode() ]
	{ BaseNode e; }
	: e=replaceEdge res=replaceNodeOcc {
		coll.addChild(new ConnectionNode(left, e, res));
  	}
	| e=replaceReversedEdge res=replaceNodeOcc {
  		coll.addChild(new ConnectionNode(res, e, left));
    };

replaceEdge returns [ BaseNode res = null ]
	{ BaseNode type = edgeRoot; }
	: MINUS res=edgeDecl RARROW
	| MINUS res=identUse RARROW
	| m:MINUS (COLON type=identUse)? RARROW {
		IdentNode id = defineAnonymous("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, type);
	}
	;

replaceReversedEdge returns [ BaseNode res = null ]
	{ BaseNode type = edgeRoot; }
	: LARROW res=edgeDecl MINUS
	| LARROW res=identUse MINUS
	| m:LARROW(COLON type=identUse)? MINUS {
		IdentNode id = defineAnonymous("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, type);
	}
	;

/**
 * The occurrence of a node.
 * A node occurrence is either the declaration of a new node, a usage of
 * a already declared node or a usage combined with a type change.
 */
replaceNodeOcc returns [ BaseNode res = initNode() ]
	{
		CollectNode coll = new CollectNode();
		IdentNode id;
	}
  : res=identUse
  | res=replaceNodeDecl
  | res=identUse c:DOUBLECOLON id=identUse {
  	res = new NodeTypeChangeNode(getCoords(c), res, id);
  }
  ;

anonymousEdge returns [ BaseNode res = null ]
	: MINUS m:RARROW {
		IdentNode id = defineAnonymous("edge", getCoords(m));
		res = new AnonymousEdgeDeclNode(id, edgeRoot);
	}
	;

replaceNodeDecl returns [ BaseNode res = initNode() ]
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
multiNodeDecl returns [ BaseNode res = initNode() ]
	{
		List ids = new LinkedList();
		IdentNode id;
		BaseNode type;
	}
	: id=identDecl COLON type=identUse {
		res = new NodeDeclNode(id, type);
	}
  | LPAREN { ids = new LinkedList(); } id=identDecl { ids.add(id); }
    (COMMA id=identDecl { ids.add(id); })* RPAREN COLON type=identUse {

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
	: id=identDecl COLON type=identUse {
		res = new EdgeDeclNode(id, type);
	}
	;


identList [ BaseNode c ]
	{ BaseNode id; }
	: id=identUse { c.addChild(id); } (COMMA id=identUse { c.addChild(id); })*
	;

/**
 * declaration of an identifier
 */
identDecl returns [ IdentNode res = getDummyIdent() ]
  : i:IDENT {
  	
  		Symbol sym = symbolTable.get(i.getText());
  		Symbol.Definition def = currScope.define(sym, getCoords(i));
      res = new IdentNode(def);
    }
  ;
  
/**
 * Attributed declaration of an identifier
 */
attrIdentDecl returns [ IdentNode res = getDummyIdent() ]
	: res=identDecl (LBRACK keyValuePairs[res] RBRACK)?
	;
	
keyValuePairs [ Attributed attr ]
  : keyValuePair[attr] (COMMA keyValuePair[attr])
  ;	  

keyValuePair[ Attributed attr ]
  { BaseNode c; }
	: i:IDENT ASSIGN c=constant {
		if(c instanceof ConstNode) 
			attr.getAttributes().put(i.getText(), ((ConstNode) c).getValue());
	};
	
/**
 * Represents the usage of an identifier.
 * It is checked, whether the identifier is declared. The IdentNode
 * created by the definition is returned.
 */
identUse returns [ IdentNode res = getDummyIdent() ]
  : i:IDENT {
  	Symbol sym = symbolTable.get(i.getText());
  	Symbol.Occurrence occ = currScope.occurs(sym, getCoords(i));

	  res = new IdentNode(occ);
  }
  ;

pushScope! [IdentNode name] options { defaultErrorHandler = false; } {
  currScope = currScope.newScope(name.toString());
  BaseNode.setCurrScope(currScope);
} : ;

popScope! options { defaultErrorHandler = false; }  {
  if(currScope != rootScope)
      currScope = currScope.leaveScope();
  BaseNode.setCurrScope(currScope);
    
} : ;



// Expressions


assignment returns [ BaseNode res = initNode() ]
  { BaseNode q, e; }
  : q=qualIdent a:ASSIGN e=expr {
  	return new AssignNode(getCoords(a), q, e);
  }
;

expr returns [ BaseNode res = initNode() ]
	: res=condExpr ;

condExpr returns [ BaseNode res = initNode() ]
	{ BaseNode op0, op1, op2; }
	: op0=logOrExpr { res=op0; } (t:QUESTION op1=expr COLON op2=condExpr {
		res=makeOp(t);
		res.addChild(op0);
		res.addChild(op1);
		res.addChild(op2);
	})?
;

logOrExpr returns [ BaseNode res = initNode() ]
	{ BaseNode op; }
	: res=logAndExpr (t:LOR op=logAndExpr {
		res=makeBinOp(t, res, op);
	})*
	;

logAndExpr returns [ BaseNode res = initNode() ]
	{ BaseNode op; }
	: res=bitOrExpr (t:LAND op=bitOrExpr {
		res = makeBinOp(t, res, op);
	})*
	;

bitOrExpr returns [ BaseNode res = initNode() ]
	{ BaseNode op; }
	: res=bitXOrExpr (t:BOR op=bitXOrExpr {
		res = makeBinOp(t, res, op);
	})*
	;

bitXOrExpr returns [ BaseNode res = initNode() ]
	{ BaseNode op; }
	: res=bitAndExpr (t:BXOR op=bitAndExpr {
		res = makeBinOp(t, res, op);
	})*
	;

bitAndExpr returns [ BaseNode res = initNode() ]
	{ BaseNode op; }
	: res=eqExpr (t:BAND op=eqExpr {
		res = makeBinOp(t, res, op);
	})*
	;

eqOp returns [ Token t = null ]
	: e:EQUAL { t=e; }
	| n:NOT_EQUAL { t=n; }
	;

eqExpr returns [ BaseNode res = initNode() ]
	{
		BaseNode op;
		Token t;
	}
	: res=relExpr (t=eqOp op=relExpr {
		res = makeBinOp(t, res, op);
	})*
	;

relOp returns [ Token t = null ]
	: lt:LT { t=lt; }
	| le:LE { t=le; }
	| gt:GT { t=gt; }
	| ge:GE { t=ge; }
	;
	
relExpr returns [ BaseNode res  = initNode() ]
	{
		BaseNode op;
		Token t;
	}
	: res=shiftExpr (t=relOp op=shiftExpr {
		res = makeBinOp(t, res, op);
	})*
	;

shiftOp returns [ Token res = null ]
	: l:SL { res=l; }
	| r:SR { res=r; }
	| b:BSR { res=b; }
	;

shiftExpr returns [ BaseNode res = initNode() ]
	{
		BaseNode op;
		Token t;
	}
	: res=addExpr (t=shiftOp op=addExpr {
		res = makeBinOp(t, res, op);
	})*
	;
	
addOp returns [ Token t = null ]
	: p:PLUS { t=p; }
	| m:MINUS { t=m; }
	;
	
addExpr returns [ BaseNode res = initNode() ]
	{
		BaseNode op;
		Token t;
	}
	: res=mulExpr (t=addOp op=mulExpr {
		res = makeBinOp(t, res, op);
	})*
	;
	
mulOp returns [ Token t = null ]
	: s:STAR { t=s; }
	| m:MOD { t=m; }
	| d:DIV { t=d; }
	;

	
mulExpr returns [ BaseNode res = initNode() ]
	{
		BaseNode op;
		Token t;
	}
	: res=unaryExpr (t=mulOp op=unaryExpr {
		res = makeBinOp(t, res, op);
	})*
	;
	
unaryExpr returns [ BaseNode res = initNode() ]
	{ BaseNode op, id; }
	: t:TILDE op=unaryExpr {
		res = makeUnOp(t, op);
	}
	| n:NOT op=unaryExpr {
		res = makeUnOp(n, op);
	}
	| m:MINUS op=unaryExpr {
		res = new ArithmeticOpNode(getCoords(m), OperatorSignature.NEG);
		res.addChild(op);
	}
	| PLUS res=unaryExpr
	| ( options { generateAmbigWarnings = false; } :
			(LPAREN identUse RPAREN unaryExpr) => p:LPAREN id=identUse RPAREN op=unaryExpr {
				res = new CastNode(getCoords(p));
				res.addChild(id);
				res.addChild(op);
			}
			| res=primaryExpr)

	;
	
primaryExpr returns [ BaseNode res = initNode() ]
	: res=qualIdent
	| res=identExpr
	| res=constant
	| LPAREN res=expr RPAREN
	;
	
constant returns [ BaseNode res = initNode() ]
	: i:NUM_DEC {
		res = new IntConstNode(getCoords(i), Integer.parseInt(i.getText(), 10));
	}
	| h:NUM_HEX {
		res = new IntConstNode(getCoords(h), Integer.parseInt(h.getText(), 16));
	}
	| s:STRING_LITERAL {
		String buff = s.getText();
		// Strip the " from the string
		buff = buff.substring(1, buff.length() - 1);
		res = new StringConstNode(getCoords(s), buff);
	}
	| t:"true" {
		res = new BoolConstNode(getCoords(t), true);
	}
	| f:"false" {
		res = new BoolConstNode(getCoords(f), false);
	}
	;
	

identExpr returns [ BaseNode res = initNode() ]
	{ IdentNode id; }
	: id=identUse {
		res = new DeclExprNode(id);
	};

qualIdent returns [ BaseNode res = initNode() ]
	{ BaseNode id; }
	: res=identUse (d:DOT id=identUse{
		res = new QualIdentNode(getCoords(d), res, id);
	})+ {
		res = new DeclExprNode(res);
	}
	;






// Lexer stuff

class GRLexer extends Lexer;

options {
	testLiterals=false;    // don't automatically test for literals
	k=4;                   // four characters of lookahead
	codeGenBitsetTestThreshold=20;
}

QUESTION		:	'?'		;
LPAREN			:	'('		;
RPAREN			:	')'		;
LBRACK			:	'['		;
RBRACK			:	']'		;
LBRACE			:	'{'		;
RBRACE			:	'}'		;
COLON			:	':'		;
COMMA			:	','		;
DOT 			:	'.'		;
ASSIGN			:	'='		;
EQUAL			:	"=="	;
NOT         	:	'!'		;
TILDE			:	'~'		;
NOT_EQUAL		:	"!="	;
SL			  : "<<" ;
SR 			  : ">>" ;
BSR       : ">>>" ;
DIV				:	'/'		;
PLUS			:	'+'		;
MINUS			:	'-'		;
STAR			:	'*'		;
MOD				:	'%'		;
GE				:	">="	;
GT				:	">"		;
LE				:	"<="	;
LT				:	'<'		;
RARROW    : "->"  ;
LARROW    : "<-"  ;
DOUBLECOLON : "::" ;
BXOR			:	'^'		;
BOR				:	'|'		;
LOR				:	"||"	;
BAND			:	'&'		;
LAND			:	"&&"	;
SEMI			:	';'		;

// Whitespace -- ignored
WS	:	(	' '
		|	'\t'
		|	'\f'
			// handle newlines
		|	(	options {generateAmbigWarnings=false;}
			:	"\r\n"  // Evil DOS
			|	'\r'    // Macintosh
			|	'\n'    // Unix (the right way)
			)
			{ newline(); }
		)+
		{ $setType(Token.SKIP); }
	;

SL_COMMENT
  :	"//" (~('\n'|'\r'))* ('\n'|'\r'('\n')?)
        {
			$setType(Token.SKIP);
			newline();
		}
	;

// multiple-line comments
ML_COMMENT
  :	"/*"
		(	/*	'\r' '\n' can be matched in one alternative or by matching
				'\r' in one iteration and '\n' in another.  I am trying to
				handle any flavor of newline that comes in, but the language
				that allows both "\r\n" and "\r" and "\n" to all be valid
				newline is ambiguous.  Consequently, the resulting grammar
				must be ambiguous.  I'm shutting this warning off.
			 */
			options {
				generateAmbigWarnings=false;
			}
		:
			{ LA(2)!='/' }? '*'
		|	'\r' '\n'		{newline();}
		|	'\r'			{newline();}
		|	'\n'			{newline();}
		|	~('*'|'\n'|'\r')
		)*
		"*/"
		{ $setType(Token.SKIP); }
  ;
  
NUM_DEC
	: ('0'..'9')+
	;
	
NUM_HEX
	: '0' 'x' ('0'..'9' | 'a' .. 'f' | 'A' .. 'F')+
	;
	
protected
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

IDENT
	options {testLiterals=true;}
	:	('a'..'z'|'A'..'Z'|'_') ('a'..'z'|'A'..'Z'|'_'|'0'..'9')*
	;
