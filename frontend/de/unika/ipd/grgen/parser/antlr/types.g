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
		CollectNode types = new CollectNode();
		IdentNode id = env.getDummyIdent();

		String modelName = Util.removePathPrefix(getFilename());
//		Util.removeFileSuffix(getFilename(), "gm") );

		id = new IdentNode(
		env.define(ParserEnvironment.ENTITIES, modelName,
		new de.unika.ipd.grgen.parser.Coords(0, 0, getFilename())));
	}

	:   ( m:MODEL i:IDENT SEMI
			{ reportWarning(getCoords(m), "keyword \"model\" is deprecated"); }
		)?
		typeDecls[types] EOF
			{
				model = new ModelNode(id);
				model.addChild(types);
			}
	;

typeDecls [ CollectNode types ]
	{ IdentNode type; }

	: (type=typeDecl { types.addChild(type); } )*
	;

typeDecl returns [ IdentNode res = env.getDummyIdent() ]
	: res=classDecl
	| res=enumDecl
	;

classDecl returns [ IdentNode res = env.getDummyIdent() ]
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
edgeClassDecl[int modifiers] returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		CollectNode body = null, ext, cas;
		String externalName = null;
	}

	:	EDGE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
	  	ext=edgeExtends[id] cas=connectAssertions pushScope[id]
		(
			LBRACE! body=edgeClassBody RBRACE!
		|	SEMI
			{ body = new CollectNode(); }
		)
		{
			EdgeTypeNode et = new EdgeTypeNode(ext, cas, body, modifiers, externalName);
			id.setDecl(new TypeDeclNode(id, et));
			res = id;
		}
		popScope!
  ;

nodeClassDecl![int modifiers] returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		CollectNode body = null, ext;
		String externalName = null;
	}

	: 	NODE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
	  	ext=nodeExtends[id] pushScope[id]
		(
			LBRACE! body=nodeClassBody RBRACE!
		|	SEMI
			{ body = new CollectNode(); }
		)
		{
			NodeTypeNode nt = new NodeTypeNode(ext, body, modifiers, externalName);
			id.setDecl(new TypeDeclNode(id, nt));
			res = id;
		}
		popScope!
	;

validIdent returns [ String id = "" ]
	:	i:~GT
		{
			if(i.getType() != IDENT && !env.isKeyword(i.getText()))
				throw new SemanticException(i.getText() + " is not a valid identifier",
					getFilename(), i.getLine(), i.getColumn());
		}
	;

fullQualIdent returns [ String id = "", id2 = "" ]
	:	id=validIdent
	 	(DOT id2=validIdent { id += "." + id2; })*
	;

connectAssertions returns [ CollectNode c = new CollectNode() ]
	: CONNECT connectAssertion[c]
		( COMMA connectAssertion[c] )*
	|
	;

connectAssertion [ CollectNode c ]
	{
		IdentNode src, tgt;
		BaseNode srcRange, tgtRange;
	}

	: src=typeIdentUse srcRange=rangeSpec RARROW
		tgt=typeIdentUse tgtRange=rangeSpec
			{ c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange)); }
	;

edgeExtends [IdentNode clsId] returns [ CollectNode c = new CollectNode() ]
	: EXTENDS edgeExtendsCont[clsId, c]
	|	{ c.addChild(env.getEdgeRoot()); }
	;

edgeExtendsCont [ IdentNode clsId, CollectNode c ]
	{
		IdentNode e;
		int extCount = 0;
	}

	: e=typeIdentUse
		{
			if ( ! ((IdentNode)e).toString().equals(clsId.toString()) )
				c.addChild(e);
			else
				reportError(e.getCoords(), "A class must not extend itself");
		}
	(COMMA! e=typeIdentUse
		{
			if ( ! ((IdentNode)e).toString().equals(clsId.toString()) )
				c.addChild(e);
			else
				reportError(e.getCoords(), "A class must not extend itself");
		}
	)*
		{ if ( c.getChildren().size() == 0 ) c.addChild(env.getEdgeRoot()); }
	;

nodeExtends [ IdentNode clsId ] returns [ CollectNode c = new CollectNode() ]
	: EXTENDS nodeExtendsCont[clsId, c]
	|	{ c.addChild(env.getNodeRoot()); }
	;

nodeExtendsCont [IdentNode clsId, CollectNode c ]
	{ IdentNode n; }

	: n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	(COMMA! n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	)*
		{ if ( c.getChildren().size() == 0 ) c.addChild(env.getNodeRoot()); }
	;

nodeClassBody returns [ CollectNode c = new CollectNode() ]
	{ BaseNode b;}

	:   (
			(
				b=basicDecl { c.addChild(b); }
				(
					b=initExprDecl[((DeclNode)b).getIdentNode()] { c.addChild(b); }
				)?
			|
				b=initExpr { c.addChild(b); }
			) SEMI!
		)*
	;

edgeClassBody returns [ CollectNode c = new CollectNode() ]
	{ BaseNode b; }

	:   (
			( b=basicDecl | b=initExpr ) { c.addChild(b); } SEMI!
		)*
	;

rangeSpec returns [ RangeSpecNode res = null ]
	{
		int lower = 0, upper = RangeSpecNode.UNBOUND;
		de.unika.ipd.grgen.parser.Coords coords = de.unika.ipd.grgen.parser.Coords.getInvalid();
		// TODO fix range to allow only [*], [+], [c:*], [c], [c:d]
	}

	:   ( l:LBRACK { coords = getCoords(l); }
			( ( STAR | PLUS { lower=1; } )
			| lower=integerConst ( COLON ( STAR | upper=integerConst ) )?
			) RBRACK
		)?
			{ res = new RangeSpecNode(coords, lower, upper); }
	;

integerConst returns [ int value = 0 ]
	: i:NUM_INTEGER
		{ value = Integer.parseInt(i.getText()); }
	;

enumDecl returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		CollectNode c = new CollectNode();
	}

	: ENUM id=typeIdentDecl pushScope[id]
		LBRACE enumList[id, c]
		{
			TypeNode enumType = new EnumTypeNode(c);
			enumType.addChild(enumType);
			id.setDecl(new TypeDeclNode(id, enumType));
			res = id;
		}
		RBRACE popScope
	;

enumList[ IdentNode enumType, CollectNode collect ]
	{
		int pos = 0;
		BaseNode init;
	}

	: init=enumItemDecl[enumType, collect, env.getZero(), pos++]
		( COMMA init=enumItemDecl[enumType, collect, init, pos++] )*
	;

enumItemDecl [ IdentNode type, CollectNode coll, BaseNode defInit, int pos ]
				returns [ BaseNode res = env.initNode() ]
	{
		IdentNode id;
		BaseNode init = null;
		BaseNode value;
	}

	: id=entIdentDecl
		( ASSIGN init=expr[true] )? //'true' means that expr initializes an enum item
			{
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

basicDecl returns [ MemberDeclNode res = null ]
	{
		IdentNode id;
		IdentNode type;
		MemberDeclNode decl;
	}

	: id=entIdentDecl COLON! type=typeIdentUse
		{
			decl = new MemberDeclNode(id, type);
			id.setDecl(decl);
			res = decl;
		}
	;

initExpr returns [ MemberInitNode res = null ]
	{
		IdentNode id;
		ExprNode e = env.initExprNode();
	}

	: id=entIdentUse a:ASSIGN e=expr[false]
		{
			res = new MemberInitNode(getCoords(a), id, e);
		}
	;

initExprDecl[IdentNode id] returns [ MemberInitNode res = null ]
	{
		ExprNode e;
	}

	: a:ASSIGN e=expr[false]
		{
			res = new MemberInitNode(getCoords(a), id, e);
		}
	;


