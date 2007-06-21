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
  	
  public boolean hadError() {
 	  return hadError;
  }
  
  public String getFilename() {
  	return env.getFilename();
  }

}

pushScope! [IdentNode name] options { defaultErrorHandler = false; } {
  env.pushScope(name);
} : ;

pushScopeStr! [String str] options { defaultErrorHandler = false; } {
  env.pushScope(str);
} : ;

popScope! options { defaultErrorHandler = false; }  {
  env.popScope();
} : ;

typeIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
  : res=identDecl[ParserEnvironment.TYPES];
  
entIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
  : res=identDecl[ParserEnvironment.ENTITIES];

actionIdentDecl returns [ IdentNode res = env.getDummyIdent() ]
  : res=identDecl[ParserEnvironment.ACTIONS];

typeIdentUse returns [ IdentNode res = env.getDummyIdent() ]
  : res=identUse[ParserEnvironment.TYPES];
  
entIdentUse returns [ IdentNode res = env.getDummyIdent() ]
  : res=identUse[ParserEnvironment.ENTITIES];

actionIdentUse returns [ IdentNode res = env.getDummyIdent() ]
  : res=identUse[ParserEnvironment.ACTIONS];

attributes returns [ DefaultAttributes attrs = new DefaultAttributes() ]
  : LBRACK keyValuePairs[attrs] RBRACK
  ;
  
keyValuePairs [ DefaultAttributes attrs ]
  : keyValuePair[attrs] (COMMA keyValuePair[attrs])*
  ;
  
keyValuePair [ DefaultAttributes attrs ]
  { BaseNode c; }
  : id:IDENT ASSIGN c=constant {
    attrs.put(id.getText(), ((ConstNode) c).getValue());
  }
  ;

/**
 * declaration of an identifier
 */
identDecl [ int symTab ] returns [ IdentNode res = env.getDummyIdent() ]
  { Attributes attrs; }
  : i:IDENT {
      res = new IdentNode(env.define(symTab, i.getText(), getCoords(i)));
    } ((attributes) => attrs=attributes {
      res.setAttributes(attrs);
    })?
  ;
	
/**
 * Represents the usage of an identifier.
 * It is checked, whether the identifier is declared. The IdentNode
 * created by the definition is returned.
 */
identUse [ int symTab ] returns [ IdentNode res = env.getDummyIdent() ]
  : i:IDENT {
    res = new IdentNode(env.occurs(symTab, i.getText(), getCoords(i)));
    }
  ;

// Expressions


assignment returns [ BaseNode res = env.initNode() ]
  { BaseNode q, e; }
  : q=qualIdent a:ASSIGN e=expr {
  	res = new AssignNode(getCoords(a), q, e);
  }
;

expr returns [ BaseNode res = env.initNode() ]
	: res=condExpr ;

condExpr returns [ BaseNode res = env.initNode() ]
	{ BaseNode op0, op1, op2; }
	: op0=logOrExpr { res=op0; } (t:QUESTION op1=expr COLON op2=condExpr {
		res=makeOp(t);
		res.addChild(op0);
		res.addChild(op1);
		res.addChild(op2);
	})?
;

logOrExpr returns [ BaseNode res = env.initNode() ]
	{ BaseNode op; }
	: res=logAndExpr (t:LOR op=logAndExpr {
		res=makeBinOp(t, res, op);
	})*
	;

logAndExpr returns [ BaseNode res = env.initNode() ]
	{ BaseNode op; }
	: res=bitOrExpr (t:LAND op=bitOrExpr {
		res = makeBinOp(t, res, op);
	})*
	;

bitOrExpr returns [ BaseNode res = env.initNode() ]
	{ BaseNode op; }
	: res=bitXOrExpr (t:BOR op=bitXOrExpr {
		res = makeBinOp(t, res, op);
	})*
	;

bitXOrExpr returns [ BaseNode res = env.initNode() ]
	{ BaseNode op; }
	: res=bitAndExpr (t:BXOR op=bitAndExpr {
		res = makeBinOp(t, res, op);
	})*
	;

bitAndExpr returns [ BaseNode res = env.initNode() ]
	{ BaseNode op; }
	: res=eqExpr (t:BAND op=eqExpr {
		res = makeBinOp(t, res, op);
	})*
	;

eqOp returns [ Token t = null ]
	: e:EQUAL { t=e; }
	| n:NOT_EQUAL { t=n; }
	;

eqExpr returns [ BaseNode res = env.initNode() ]
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
	
relExpr returns [ BaseNode res  = env.initNode() ]
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

shiftExpr returns [ BaseNode res = env.initNode() ]
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
	
addExpr returns [ BaseNode res = env.initNode() ]
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

	
mulExpr returns [ BaseNode res = env.initNode() ]
	{
		BaseNode op;
		Token t;
	}
	: res=unaryExpr (t=mulOp op=unaryExpr {
		res = makeBinOp(t, res, op);
	})*
	;
	
unaryExpr returns [ BaseNode res = env.initNode() ]
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
			(LPAREN typeIdentUse RPAREN unaryExpr) => p:LPAREN id=typeIdentUse RPAREN op=unaryExpr {
				res = new CastNode(getCoords(p));
				res.addChild(id);
				res.addChild(op);
			}
			| res=primaryExpr)

	;
	
primaryExpr returns [ BaseNode res = env.initNode() ]
	: res=qualIdentExpr
	| res=identExpr
	| res=constant
	| res=enumItemExpr
	| res=typeOf
	| p:PLUSPLUS {
			reportError(getCoords(p), "increment operator \"++\" not supported");
	}
	| q:MINUSMINUS {
			reportError(getCoords(q), "decrement operator \"--\" not supported");
	}
	| LPAREN res=expr RPAREN
	;

typeOf returns [ BaseNode res = env.initNode() ]
	: t:TYPEOF LPAREN res=entIdentUse RPAREN { res = new TypeofNode(getCoords(t), res); }
	;

constant returns [ BaseNode res = env.initNode() ]
	: i:NUM_INTEGER {
		res = new IntConstNode(getCoords(i), Integer.parseInt(i.getText(), 10));
	}
	| h:NUM_HEX {
		res = new IntConstNode(getCoords(h), Integer.parseInt(h.getText(), 16));
	}
	| f:NUM_FLOAT {
		res = new FloatConstNode(getCoords(f), Float.parseFloat(f.getText()));
	}
	| d:NUM_DOUBLE {
		res = new DoubleConstNode(getCoords(d), Double.parseDouble(d.getText()));
	}
	| s:STRING_LITERAL {
		String buff = s.getText();
		// Strip the " from the string
		buff = buff.substring(1, buff.length() - 1);
		res = new StringConstNode(getCoords(s), buff);
	}
	| t:TRUE {
		res = new BoolConstNode(getCoords(t), true);
	}
	| n:FALSE {
		res = new BoolConstNode(getCoords(n), false);
	}
	;

identExpr returns [ BaseNode res = env.initNode() ]
	{ IdentNode id; }
	  : i:IDENT {
	  	if(env.test(ParserEnvironment.TYPES, i.getText())) {
		  	id = new IdentNode(env.occurs(ParserEnvironment.TYPES, i.getText(), getCoords(i)));
		  	res = new TypeConstNode(id);
	  	} else {
		  	id = new IdentNode(env.occurs(ParserEnvironment.ENTITIES, i.getText(), getCoords(i)));
		  	res = new DeclExprNode(id);
	  	}
    }
    ;

qualIdent returns [ BaseNode res = env.initNode() ]
	{ BaseNode id; }
	: res=entIdentUse (d:DOT id=entIdentUse {
		  res = new QualIdentNode(getCoords(d), res, id);
  	})+
	;
	
enumItemAcc returns [ BaseNode res = env.initNode() ]
  { BaseNode id; }
  : res = typeIdentUse d:DOUBLECOLON id = entIdentUse {
    res = new EnumExprNode(getCoords(d), res, id);
  };
  
enumItemExpr returns [ BaseNode res = env.initNode() ]
  : res = enumItemAcc { res = new DeclExprNode(res); };
	
qualIdentExpr returns [ BaseNode res = env.initNode() ]
  : res=qualIdent { res = new DeclExprNode(res); }
  ;



