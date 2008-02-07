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

	package de.unika.ipd.grgen.parser.antlr;

	import java.util.Map;
	import java.util.HashMap;

	import de.unika.ipd.grgen.parser.*;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.util.*;
}

/**
 * GrGen types grammar
 * @author Sebastian Hack, Daniel Grund, Rubino Geiss, Adam Szalkowski
 * @version $Id$
 */
class GRTypeParser extends GRBaseParser;
options {
	k=2;
	codeGenMakeSwitchThreshold = 2;
	codeGenBitsetTestThreshold = 3;
	defaultErrorHandler = true;
	buildAST = false;
	importVocab = GRBase;

}

text returns [ BaseNode model = env.initNode() ]
	{
		CollectNode<IdentNode> types = new CollectNode<IdentNode>();
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
				model = new ModelNode(id, types);
			}
	;

typeDecls [ CollectNode<IdentNode> types ]
	{ IdentNode type; }

	: (type=typeDecl { types.addChild(type); } )*
	;

typeDecl returns [ IdentNode res = env.getDummyIdent() ]
	: res=classDecl
	| res=enumDecl
	;

classDecl returns [ IdentNode res = env.getDummyIdent() ]
	{ int mods = 0; }

	: (mods=typeModifiers)? (res=edgeClassDecl[mods] | res=nodeClassDecl[mods])
	;

typeModifiers returns [ int res = 0; ]
	{ int mod = 0; }

	: (mod=typeModifier { res |= mod; })+
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
		CollectNode<BaseNode> body = null;
		CollectNode<IdentNode> ext;
		CollectNode<ConnAssertNode> cas;
		String externalName = null;
	}

	:	EDGE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
	  	ext=edgeExtends[id] cas=connectAssertions pushScope[id]
		(
			LBRACE body=edgeClassBody RBRACE
		|	SEMI
			{ body = new CollectNode<BaseNode>(); }
		)
		{
			DirectedEdgeTypeNode et = new DirectedEdgeTypeNode(ext, cas, body, modifiers, externalName);
			id.setDecl(new TypeDeclNode(id, et));
			res = id;
		}
		popScope
  ;

nodeClassDecl[int modifiers] returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		CollectNode<BaseNode> body = null;
		CollectNode<IdentNode> ext;
		String externalName = null;
	}

	: 	NODE CLASS id=typeIdentDecl (LT externalName=fullQualIdent GT)?
	  	ext=nodeExtends[id] pushScope[id]
		(
			LBRACE body=nodeClassBody RBRACE
		|	SEMI
			{ body = new CollectNode<BaseNode>(); }
		)
		{
			NodeTypeNode nt = new NodeTypeNode(ext, body, modifiers, externalName);
			id.setDecl(new TypeDeclNode(id, nt));
			res = id;
		}
		popScope
	;

validIdent returns [ String id = "" ]
	:	i:~GT
		{
			if(i.getType() != IDENT && !env.isKeyword(i.getText()))
				throw new SemanticException(i.getText() + " is not a valid identifier",
					getFilename(), i.getLine(), i.getColumn());
			id = i.getText();
		}
	;

fullQualIdent returns [ String id = "", id2 = "" ]
	:	id=validIdent
	 	(DOT id2=validIdent { id += "." + id2; })*
	;

connectAssertions returns [ CollectNode<ConnAssertNode> c = new CollectNode<ConnAssertNode>() ]
	: CONNECT connectAssertion[c]
		( COMMA connectAssertion[c] )*
	|
	;

connectAssertion [ CollectNode<ConnAssertNode> c ]
	{
		IdentNode src, tgt;
		RangeSpecNode srcRange, tgtRange;
	}

	: src=typeIdentUse srcRange=rangeSpec RARROW
		tgt=typeIdentUse tgtRange=rangeSpec
			{ c.addChild(new ConnAssertNode(src, srcRange, tgt, tgtRange)); }
	;

edgeExtends [IdentNode clsId] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS edgeExtendsCont[clsId, c]
	|	{ c.addChild(env.getEdgeRoot()); }
	;

edgeExtendsCont [ IdentNode clsId, CollectNode<IdentNode> c ]
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
	(COMMA e=typeIdentUse
		{
			if ( ! ((IdentNode)e).toString().equals(clsId.toString()) )
				c.addChild(e);
			else
				reportError(e.getCoords(), "A class must not extend itself");
		}
	)*
		{ if ( c.getChildren().size() == 0 ) c.addChild(env.getEdgeRoot()); }
	;

nodeExtends [ IdentNode clsId ] returns [ CollectNode<IdentNode> c = new CollectNode<IdentNode>() ]
	: EXTENDS nodeExtendsCont[clsId, c]
	|	{ c.addChild(env.getNodeRoot()); }
	;

nodeExtendsCont [IdentNode clsId, CollectNode<IdentNode> c ]
	{ IdentNode n; }

	: n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	(COMMA n=typeIdentUse
		{
			if ( ! ((IdentNode)n).toString().equals(clsId.toString()) )
				c.addChild(n);
			else
				reportError(n.getCoords(), "A class must not extend itself");
		}
	)*
		{ if ( c.getChildren().size() == 0 ) c.addChild(env.getNodeRoot()); }
	;

nodeClassBody returns [ CollectNode<BaseNode> c = new CollectNode<BaseNode>() ]
	{
		BaseNode b;
	}

	:	(
			(
				b=basicDecl { c.addChild(b); }
				(
					b=initExprDecl[((DeclNode)b).getIdentNode()] { c.addChild(b); }
				)?
			|
				b=initExpr { c.addChild(b); }
			) SEMI
		)*
	;

edgeClassBody returns [ CollectNode<BaseNode> c = new CollectNode<BaseNode>() ]
	{
		BaseNode b;
	}

	:	(
			(
				b=basicDecl { c.addChild(b); }
				(
					b=initExprDecl[((DeclNode)b).getIdentNode()] { c.addChild(b); }
				)?
			|
				b=initExpr { c.addChild(b); }
			) SEMI
		)*
	;

enumDecl returns [ IdentNode res = env.getDummyIdent() ]
	{
		IdentNode id;
		CollectNode<EnumItemNode> c = new CollectNode<EnumItemNode>();
	}

	: ENUM id=typeIdentDecl pushScope[id]
		LBRACE enumList[id, c]
		{
			TypeNode enumType = new EnumTypeNode(c);
			id.setDecl(new TypeDeclNode(id, enumType));
			res = id;
		}
		RBRACE popScope
	;

enumList[ IdentNode enumType, CollectNode<EnumItemNode> collect ]
	{
		int pos = 0;
		ExprNode init;
	}

	: init=enumItemDecl[enumType, collect, env.getZero(), pos++]
		( COMMA init=enumItemDecl[enumType, collect, init, pos++] )*
	;

enumItemDecl [ IdentNode type, CollectNode<EnumItemNode> coll, ExprNode defInit, int pos ]
				returns [ ExprNode res = env.initExprNode() ]
	{
		IdentNode id;
		ExprNode init = null;
		ExprNode value;
	}

	: id=entIdentDecl	( ASSIGN init=expr[true] )? //'true' means that expr initializes an enum item
		{
			if(init != null) {
				value = init;
			} else {
				value = defInit;
			}
			EnumItemNode memberDecl = new EnumItemNode(id, type, value, pos);
			id.setDecl(memberDecl);
			coll.addChild(memberDecl);
			OpNode add = new ArithmeticOpNode(id.getCoords(), OperatorSignature.ADD);
			add.addChild(value);
			add.addChild(env.getOne());
			res = add;
		}
	;

basicDecl returns [ MemberDeclNode res = null ]
	{
		IdentNode id = env.getDummyIdent();
		IdentNode type;
		MemberDeclNode decl;
		boolean isConst = false;
	}

	:	(
			ABSTRACT ( CONST { isConst = true; } )? id=entIdentDecl
			{
				res = new AbstractMemberDeclNode(id, isConst);
			}
		|
			( CONST { isConst = true; } )? id=entIdentDecl COLON type=typeIdentUse
			{
				decl = new MemberDeclNode(id, type, isConst);
				id.setDecl(decl);
				res = decl;
			}
		)
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


