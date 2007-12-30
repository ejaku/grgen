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

  import java.util.*;

	import de.unika.ipd.grgen.parser.*;
	import de.unika.ipd.grgen.ast.*;
	import de.unika.ipd.grgen.util.report.*;
	import de.unika.ipd.grgen.util.*;
	import de.unika.ipd.grgen.Main;

	import antlr.*;
}


/**
 * GrGen base grammar (just for inheritance)
 * @version 0.1
 * @author Sebastian Hack
 */
class GRBaseParser extends Parser;
options {
	k=3;
	codeGenMakeSwitchThreshold = 2;
	codeGenBitsetTestThreshold = 3;
	defaultErrorHandler = true;
	buildAST = false;
	importVocab = GRBase;
}

{
	boolean hadError = false;

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

	private OpNode makeBinOp(antlr.Token t, ExprNode op0, ExprNode op1) {
		OpNode res = makeOp(t);
		res.addChild(op0);
		res.addChild(op1);
		return res;
	}

	private OpNode makeUnOp(antlr.Token t, ExprNode op) {
		OpNode res = makeOp(t);
		res.addChild(op);
		return res;
	}

	protected ParserEnvironment env;

	public void setEnv(ParserEnvironment env) {
		this.env = env;
	}

	protected Coords getCoords(antlr.Token tok) {
		return new Coords(tok, this);
	}

	protected final void reportError(de.unika.ipd.grgen.parser.Coords c, String s) {
		hadError = true;
		env.getSystem().getErrorReporter().error(c, s);
	}

	public void reportError(String arg0) {
		reportError(Coords.getInvalid(), arg0);
	}

	public void reportError(RecognitionException e) {
		reportError(new Coords(e), e.getErrorMessage());
	}

	public void reportError(RecognitionException e, String s) {
		reportError(new Coords(e), s);
	}

	public void reportWarning(String arg0) {
		env.getSystem().getErrorReporter().warning(arg0);
	}

	public void reportWarning(de.unika.ipd.grgen.parser.Coords c, String s) {
		env.getSystem().getErrorReporter().warning(c, s);
	}

	public boolean hadError() {
		return hadError;
	}

	public String getFilename() {
		return env.getFilename();
	}
}

pushScope! [IdentNode name] options { defaultErrorHandler = false; }
	{ env.pushScope(name); }

	:
	;

pushScopeStr! [String str, Coords coords] options { defaultErrorHandler = false; }
	{ env.pushScope(new IdentNode(new Symbol.Definition(env.getCurrScope(), coords, new Symbol(str, SymbolTable.getInvalid())))); }

	:
	;

popScope! options { defaultErrorHandler = false; }
	{ env.popScope(); }

	:
	;

typeIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: res=identDecl[ParserEnvironment.TYPES]
	;

entIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: res=identDecl[ParserEnvironment.ENTITIES]
	;

actionIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
	: res=identDecl[ParserEnvironment.ACTIONS]
	;

typeIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: res=identUse[ParserEnvironment.TYPES]
	;

entIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: res=identUse[ParserEnvironment.ENTITIES]
	;

actionIdentUse returns [ IdentNode res = env.getDummyIdent() ]
	: res=identUse[ParserEnvironment.ACTIONS]
	;

attributes returns [ DefaultAttributes attrs = new DefaultAttributes() ]
	: LBRACK keyValuePairs[attrs] RBRACK
	;

attributesWithCoords
	returns [
		Pair<DefaultAttributes, de.unika.ipd.grgen.parser.Coords> res =
			new Pair<DefaultAttributes, de.unika.ipd.grgen.parser.Coords>(
				new DefaultAttributes(), Coords.getInvalid()
			)
	]
	: l:LBRACK keyValuePairs[res.first] RBRACK
		{ res.second = getCoords(l); }
	;

keyValuePairs [ DefaultAttributes attrs ]
	: keyValuePair[attrs] (COMMA keyValuePair[attrs])*
	;

keyValuePair [ DefaultAttributes attrs ]
	{ BaseNode c; }

	: id:IDENT ASSIGN c=constant
		{ attrs.put(id.getText(), ((ConstNode) c).getValue()); }
	;

/**
 * declaration of an identifier
 */
identDecl [ int symTab ] returns [ IdentNode res = env.getDummyIdent() ]
	{ Attributes attrs; }

	: i:IDENT
		{ res = new IdentNode(env.define(symTab, i.getText(), getCoords(i))); }
	( (attributes) => attrs=attributes
		{ res.setAttributes(attrs); }
	)?
	;

/**
 * Represents the usage of an identifier.
 * It is checked, whether the identifier is declared. The IdentNode
 * created by the definition is returned.
 */
identUse [ int symTab ] returns [ IdentNode res = env.getDummyIdent() ]
	: i:IDENT
		{ res = new IdentNode(env.occurs(symTab, i.getText(), getCoords(i))); }
	;

///////////////////////////////////////////////////////////////////////////
// Expressions
///////////////////////////////////////////////////////////////////////////

assignment returns [ AssignNode res = null ]
	{
		QualIdentNode q;
		ExprNode e;
	}

	: q=qualIdent a:ASSIGN e=expr[false] //'false' because this rule is not used for the assignments in enum item decls
		{ res = new AssignNode(getCoords(a), q, e); }
	;

expr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: res=condExpr[inEnumInit]
	;

condExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{ ExprNode op0, op1, op2; }

	: op0=logOrExpr[inEnumInit] { res=op0; }
		( t:QUESTION op1=expr[inEnumInit] COLON op2=condExpr[inEnumInit]
			{
				OpNode cond=makeOp(t);
				cond.addChild(op0);
				cond.addChild(op1);
				cond.addChild(op2);
				res=cond;
			}
		)?
	;

logOrExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{ ExprNode op; }

	: res=logAndExpr[inEnumInit]
		( t:LOR op=logAndExpr[inEnumInit]
			{ res=makeBinOp(t, res, op); }
		)*
	;

logAndExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{ ExprNode op; }

	: res=bitOrExpr[inEnumInit]
		( t:LAND op=bitOrExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

bitOrExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{ ExprNode op; }

	: res=bitXOrExpr[inEnumInit]
		( t:BOR op=bitXOrExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

bitXOrExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{ ExprNode op; }

	: res=bitAndExpr[inEnumInit]
		( t:BXOR op=bitAndExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

bitAndExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{ ExprNode op; }

	: res=eqExpr[inEnumInit]
		( t:BAND op=eqExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

eqOp returns [ Token t = null ]
	: e:EQUAL { t=e; }
	| n:NOT_EQUAL { t=n; }
	;

eqExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{
		ExprNode op;
		Token t;
	}

	: res=relExpr[inEnumInit]
		( t=eqOp op=relExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

relOp returns [ Token t = null ]
	: lt:LT { t=lt; }
	| le:LE { t=le; }
	| gt:GT { t=gt; }
	| ge:GE { t=ge; }
	;

relExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{
		ExprNode op;
		Token t;
	}

	: res=shiftExpr[inEnumInit]
		( t=relOp op=shiftExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

shiftOp returns [ Token res = null ]
	: l:SL { res=l; }
	| r:SR { res=r; }
	| b:BSR { res=b; }
	;

shiftExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{
		ExprNode op;
		Token t;
	}

	: res=addExpr[inEnumInit]
		( t=shiftOp op=addExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

addOp returns [ Token t = null ]
	: p:PLUS { t=p; }
	| m:MINUS { t=m; }
	;

addExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{
		ExprNode op;
		Token t;
	}

	: res=mulExpr[inEnumInit]
		( t=addOp op=mulExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

mulOp returns [ Token t = null ]
	: s:STAR { t=s; }
	| m:MOD { t=m; }
	| d:DIV { t=d; }
	;


mulExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{
		ExprNode op;
		Token t;
	}

	: res=unaryExpr[inEnumInit]
		(t=mulOp op=unaryExpr[inEnumInit]
			{ res = makeBinOp(t, res, op); }
		)*
	;

unaryExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	{
		ExprNode op;
		BaseNode id;
	}

	: t:TILDE op=unaryExpr[inEnumInit]
		{ res = makeUnOp(t, op); }
	| n:NOT op=unaryExpr[inEnumInit]
		 { res = makeUnOp(n, op); }
	| m:MINUS op=unaryExpr[inEnumInit]
		{
			OpNode neg = new ArithmeticOpNode(getCoords(m), OperatorSignature.NEG);
			neg.addChild(op);
			res = neg;
		}
	| PLUS res=unaryExpr[inEnumInit]
	|   ( options { generateAmbigWarnings = false; } :
			(LPAREN typeIdentUse RPAREN unaryExpr[inEnumInit])
			=> p:LPAREN id=typeIdentUse RPAREN op=unaryExpr[inEnumInit]
				{
					res = new CastNode(getCoords(p), id, op);
				}
		| res=primaryExpr[inEnumInit]
		)
	;

primaryExpr [ boolean inEnumInit ] returns [ ExprNode res = env.initExprNode() ]
	: res=qualIdentExpr
	| res=identExpr
	| res=constant
	| res=enumItemExpr
	| res=typeOf
	| p:PLUSPLUS
		{ reportError(getCoords(p), "increment operator \"++\" not supported"); }
	| q:MINUSMINUS
		{ reportError(getCoords(q), "decrement operator \"--\" not supported"); }
	| LPAREN res=expr[inEnumInit] RPAREN
	;

typeOf returns [ ExprNode res = env.initExprNode() ]
	{ BaseNode id; }

	: t:TYPEOF LPAREN id=entIdentUse RPAREN { res = new TypeofNode(getCoords(t), id); }
	;

constant returns [ ExprNode res = env.initExprNode() ]
	: i:NUM_INTEGER
		{ res = new IntConstNode(getCoords(i), Integer.parseInt(i.getText(), 10)); }
	| h:NUM_HEX
		{ res = new IntConstNode(getCoords(h), Integer.parseInt(h.getText().substring(2), 16)); }
	| f:NUM_FLOAT
		{ res = new FloatConstNode(getCoords(f), Float.parseFloat(f.getText())); }
	| d:NUM_DOUBLE
		{ res = new DoubleConstNode(getCoords(d), Double.parseDouble(d.getText())); }
	| s:STRING_LITERAL
		{
			String buff = s.getText();
			// Strip the " from the string
			buff = buff.substring(1, buff.length() - 1);
			res = new StringConstNode(getCoords(s), buff);
		}
	| tt:TRUE
		{ res = new BoolConstNode(getCoords(tt), true); }
	| ff:FALSE
		{ res = new BoolConstNode(getCoords(ff), false); }
	| n:NULL
		{ res = new NullConstNode(getCoords(n)); }
	;

identExpr returns [ ExprNode res = env.initExprNode() ]
	{ IdentNode id; }

	: i:IDENT
		{
			if(env.test(ParserEnvironment.TYPES, i.getText())) {
				id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
				res = new TypeConstNode(id);
			} else {
				id = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
				res = new DeclExprNode(id);
			}
		}
	;

qualIdent returns [ QualIdentNode res = null ]
	{
		IdentNode id;
		BaseNode currentLeft;
	}

	: currentLeft=entIdentUse
		(d:DOT id=entIdentUse
			{
				res = new QualIdentNode(getCoords(d), currentLeft, id);
				currentLeft = res;
			}
		)+
	;

enumItemAcc returns [ EnumExprNode res = null ]
	{
		IdentNode id;
		IdentNode tid;
	}

	: tid = typeIdentUse d:DOUBLECOLON id = entIdentUse
	{ res = new EnumExprNode(getCoords(d), tid, id); }
	;

enumItemExpr returns [ ExprNode res = env.initExprNode() ]
	{ EnumExprNode n; }

	: n = enumItemAcc { res = new DeclExprNode(n); }
	;

qualIdentExpr returns [ ExprNode res = env.initExprNode() ]
	{ QualIdentNode n; }

	: n = qualIdent { res = new DeclExprNode(n); }
	;




