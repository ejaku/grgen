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
  }
  : MODEL id=entIdentDecl SEMI typeDecls[n] EOF {
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
  
	: EDGE CLASS id=typeIdentDecl ext=edgeExtends[id] cas=connectAssertions pushScope[id]
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

	: NODE CLASS id=typeIdentDecl ext=nodeExtends[id] pushScope[id]
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

edgeExtends [IdentNode clsId] returns [ CollectNode c = new CollectNode() ]
  : EXTENDS edgeExtendsCont[clsId,c]
  | { c.addChild(env.getEdgeRoot()); }
  ;

edgeExtendsCont [ IdentNode clsId, CollectNode c ]
    {
      BaseNode e;
      int extCount = 0;
    }
    : e=typeIdentUse {
      	if ( ! ((IdentNode)e).toString().equals(clsId.toString()) )
      		c.addChild(e);
      	else
      		e.reportError("a class must not extend itself");
      }
      (COMMA! e=typeIdentUse {
      	if ( ! ((IdentNode)e).toString().equals(clsId.toString()) )
      		c.addChild(e);
      	else
      		e.reportError("a class must not extend itself");
      })*
      { if ( c.getChildren().size() == 0 ) c.addChild(env.getEdgeRoot()); }
	;

nodeExtends [ IdentNode clsId ] returns [ CollectNode c = new CollectNode() ]
  : EXTENDS nodeExtendsCont[clsId, c]
  | { c.addChild(env.getNodeRoot()); }
  ;

nodeExtendsCont [IdentNode clsId, CollectNode c ]
  { BaseNode n; }
  : n=typeIdentUse {
    	if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
    		c.addChild(n);
    	else
      		n.reportError("a class must not extend itself");
    }
    (COMMA! n=typeIdentUse {
    	if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
    		c.addChild(n);
    	else
      		n.reportError("a class must not extend itself");
    })*
    { if ( c.getChildren().size() == 0 ) c.addChild(env.getNodeRoot()); }
  ;

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
		// TODO fix range to allow only [*], [+], [c:*], [c], [c:d]
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
	: i:NUM_INTEGER {
		value = Integer.parseInt(i.getText());
	}
	;
	
enumDecl returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id;
		BaseNode c = new CollectNode();
	}
	: ENUM id=typeIdentDecl pushScope[id] LBRACE enumList[id, c] {
		TypeNode enumType = new EnumTypeNode(c);
  		enumType.addChild(enumType);

  		id.setDecl(new TypeDeclNode(id, enumType));
  		res = id;

  	} RBRACE popScope;

enumList[ BaseNode enumType, BaseNode collect ]
	{
		int pos = 0;
		BaseNode init;
	}
	:	init=enumItemDecl[enumType, collect, env.getZero(), pos++]
	    (COMMA init=enumItemDecl[enumType, collect, init, pos++])*
	;
	
enumItemDecl [ BaseNode type, BaseNode coll, BaseNode defInit, int pos ]
  returns [ BaseNode res = env.initNode() ]
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
			
		MemberDeclNode memberDecl = new EnumItemNode(id, type, value, pos);
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



