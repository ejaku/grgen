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
 * GrGen types grammar
 * @version 0.1
 * @author Sebastian Hack
 */
class GRTypeParser extends GRBaseParser;
options {
  k=3;
	codeGenMakeSwitchThreshold = 2;
	codeGenBitsetTestThreshold = 3;
	defaultErrorHandler = true;
	buildAST = false;
	importVocab = GRBase;
	
}

text returns [ BaseNode model = env.initNode() ]
  {
    CollectNode n = new CollectNode();
    IdentNode id;
    
    env.addBuiltinTypes(n);
  }
  : MODEL id=entIdentDecl SEMI typeDecls[n] {
    model = new ModelNode(id);
    model.addChild(n);
  }
  ;
  	
  	
typeDecls [ BaseNode n ]
  { BaseNode d; }
  : (d=typeDecl { n.addChild(d); } )*
  ;

typeDecl returns [ BaseNode res = env.initNode() ]
  : res=classDecl
  | res=enumDecl
  ;

classDecl returns [ BaseNode res = env.initNode() ]
	{ int mods = 0; }
	: mods=typeModifiers (res=edgeClassDecl[mods] | res=nodeClassDecl[mods])
	;

typeModifiers returns [ int res = 0; ]
  { int mod = 0; }
	: (mod=typeModifier { res |= mod; })*
	;
	
typeModifier returns [ int res = 0; ]
	: ABSTRACT { res |= InheritanceTypeNode.MOD_ABSTRACT; }
	| CONST { res |= InheritanceTypeNode.MOD_CONST; }
	;
	

/**
 * An edge class decl makes a new type decl node with the declaring id and
 * a new edge type node as children
 */
edgeClassDecl[int modifiers] returns [ BaseNode res = env.initNode() ]
  {
  	BaseNode body = new CollectNode(), ext, cas;
	  IdentNode id;
  }
  
	: EDGE CLASS id=typeIdentDecl ext=edgeExtends cas=connectAssertions pushScope[id]
		(LBRACE! body=edgeClassBody RBRACE! | SEMI) {

		EdgeTypeNode et = new EdgeTypeNode(ext, cas, body, modifiers);
		id.setDecl(new TypeDeclNode(id, et));
		res = id;
  } popScope!
  ;

nodeClassDecl![int modifiers] returns [ BaseNode res = env.initNode() ]
	{
  	BaseNode body = new CollectNode(), ext;
  	IdentNode id;
  }

	: NODE CLASS id=typeIdentDecl ext=nodeExtends pushScope[id]
	  (LBRACE! body=nodeClassBody RBRACE! | SEMI) {

		NodeTypeNode nt = new NodeTypeNode(ext, body, modifiers);
		id.setDecl(new TypeDeclNode(id, nt));
		res = id;
	} popScope!
	;

connectAssertions returns [ CollectNode c = new CollectNode() ]
	: CONNECT connectAssertion[c] ( COMMA connectAssertion[c] )*
	|
	;

connectAssertion [ CollectNode c ]
	{ BaseNode src, srcRange, edge, tgt, tgtRange; }
	: src=typeIdentUse srcRange=rangeSpec RARROW
	  tgt=typeIdentUse tgtRange=rangeSpec {
	  c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange));
	}
	;

edgeExtends returns [ CollectNode c = new CollectNode() ]
  : EXTENDS edgeExtendsCont[c]
  | { c.addChild(env.getEdgeRoot()); }
  ;

edgeExtendsCont[ CollectNode c ]
    { BaseNode e; }
    : e=typeIdentUse { c.addChild(e); }
      (COMMA! e=typeIdentUse { c.addChild(e); } )*
	;

nodeExtends returns [ CollectNode c = new CollectNode() ]
  : EXTENDS nodeExtendsCont[c]
  | { c.addChild(env.getNodeRoot()); }
  ;

nodeExtendsCont[ CollectNode c ]
  { BaseNode n; }
  : n=typeIdentUse { c.addChild(n); }
    (COMMA! n=typeIdentUse { c.addChild(n); } )* ;

nodeClassBody returns [ CollectNode c = new CollectNode() ]
  { BaseNode d; }
  : (d=basicDecl { c.addChild(d); } SEMI!)*
  ;

edgeClassBody returns [ CollectNode c = new CollectNode() ]
	{ BaseNode d; }
    : (d=basicDecl { c.addChild(d); } SEMI!)*
	;
	
rangeSpec returns [ BaseNode res = env.initNode() ]
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
	
enumDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id;
		BaseNode c;
	}
	: ENUM id=typeIdentDecl pushScope[id] LBRACE c=enumList {
		BaseNode enumType = new EnumTypeNode(c);
		enumType.addChild(enumType);
		id.setDecl(new TypeDeclNode(id, enumType));
		res = id;
	} RBRACE popScope;

enumList returns [ BaseNode res = env.initNode() ]
	{
		int pos = 0;
		res = new CollectNode();
		BaseNode init;
	}
	:	init=enumItemDecl[res, env.getZero(), pos++]
	    (COMMA init=enumItemDecl[res, init, pos++])*
	;
	
enumItemDecl [ BaseNode coll, BaseNode defInit, int pos ] returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id;
		BaseNode init = null;
		BaseNode value;
	}
	: id=entIdentDecl (ASSIGN init=expr)? {

		if(init != null)
			value = init;
		else
			value = defInit;
			
		MemberDeclNode memberDecl = new EnumItemNode(id, value, pos);
		id.setDecl(memberDecl);
		coll.addChild(memberDecl);
		
		res = new ArithmeticOpNode(id.getCoords(), OperatorSignature.ADD);
		res.addChild(value);
		res.addChild(env.getOne());
	}
	;

basicDecl returns [ BaseNode res = env.initNode() ]
    {
  	  IdentNode id;
  	  BaseNode type;
  	  DeclNode decl;
    }
  
  : id=entIdentDecl COLON! type=typeIdentUse {
	
	decl = new MemberDeclNode(id, type);
	id.setDecl(decl);
	res = decl;
};

/*
typeSetExpr
  : typeSetAddExpr

typeSetAddExpr
  : typeSetMulExpr (typeAddOp typeSetAddExpr)*
  ;
  
typeAddOp
  : PLUS
  | MINUS
  ;
  
typeSetMulExpr
  : typeSetUnaryExpr (typeMulOp typeSetMulExpr)*
  ;
  
typeMulOp
  : AMPERSAND
  ;
  
typeSetUnaryExpr
  : COLON typeIdentUse
  | LBRACE typeIdentList RBRACE
  | LPAREN typeSetExpr RPAREN
  ;

typeIdentList
  : typeIdentUse (COMMA typeIdentUse)*
  ;
 */
