// $ANTLR : "grgen.g" -> "GRParser.java"$

    package de.unika.ipd.grgen.parser.antlr;

		import java.util.Iterator;
		import java.util.List;
		import java.util.LinkedList;
		import java.util.Map;
		import java.util.HashMap;
		
		import de.unika.ipd.grgen.parser.Symbol;
		import de.unika.ipd.grgen.parser.SymbolTable;
		import de.unika.ipd.grgen.parser.Scope;
    import de.unika.ipd.grgen.ast.*;
    import de.unika.ipd.grgen.util.report.*;
    import de.unika.ipd.grgen.Main;

import antlr.TokenBuffer;
import antlr.TokenStreamException;
import antlr.TokenStreamIOException;
import antlr.ANTLRException;
import antlr.LLkParser;
import antlr.Token;
import antlr.TokenStream;
import antlr.RecognitionException;
import antlr.NoViableAltException;
import antlr.MismatchedTokenException;
import antlr.SemanticException;
import antlr.ParserSharedInputState;
import antlr.collections.impl.BitSet;

/**
 * GRGen grammar
 * @version 0.1
 * @author Sebastian Hack
 */
public class GRParser extends antlr.LLkParser       implements GRParserTokenTypes
 {

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
			putOpId(DOT, OperatorSignature.QUAL);
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
    	return new OpNode(getCoords(t), opId.intValue());
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
    	nodeRoot = predefineType("Node", new NodeTypeNode(new CollectNode(), new CollectNode()));
			edgeRoot = predefineType("Edge", new EdgeTypeNode(new CollectNode(), new CollectNode()));
			
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



protected GRParser(TokenBuffer tokenBuf, int k) {
  super(tokenBuf,k);
  tokenNames = _tokenNames;
}

public GRParser(TokenBuffer tokenBuf) {
  this(tokenBuf,3);
}

protected GRParser(TokenStream lexer, int k) {
  super(lexer,k);
  tokenNames = _tokenNames;
}

public GRParser(TokenStream lexer) {
  this(lexer,3);
}

public GRParser(ParserSharedInputState state) {
  super(state,3);
  tokenNames = _tokenNames;
}

/**
 * Build a main node. 
 * It has a collect node with the decls as child
 */
	public final BaseNode  text() throws RecognitionException, TokenStreamException {
		 BaseNode main = initNode() ;
		
		
		try {      // for error handling
			
					CollectNode n;
					IdentNode id;
				
			match(LITERAL_unit);
			id=identDecl();
			match(SEMI);
			n=decls();
			match(Token.EOF_TYPE);
			mainChilds.addChildren(n);
			
					main = new UnitNode(id, getFilename());
					main.addChild(mainChilds);
					
					// leave the root scope and finish all occurrences
					currScope.leaveScope();
				
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
		return main;
	}
	
/**
 * declaration of an identifier
 */
	public final IdentNode  identDecl() throws RecognitionException, TokenStreamException {
		 IdentNode res = getDummyIdent() ;
		
		Token  i = null;
		
		try {      // for error handling
			i = LT(1);
			match(IDENT);
			
				
					Symbol sym = symbolTable.get(i.getText());
					Symbol.Definition def = currScope.define(sym, getCoords(i));
			res = new IdentNode(def);
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_1);
		}
		return res;
	}
	
/**
 * Decls make a collect node with all the decls as children
 */
	public final CollectNode  decls() throws RecognitionException, TokenStreamException {
		 CollectNode n = new CollectNode() ;
		
		
		try {      // for error handling
			BaseNode d;
			{
			_loop4:
			do {
				if ((_tokenSet_2.member(LA(1)))) {
					d=decl();
					n.addChild(d);
				}
				else {
					break _loop4;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
		return n;
	}
	
/**
 * A decl is a 
 * - group
 * - edge type
 * - node type
 */
	public final BaseNode  decl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			switch ( LA(1)) {
			case LITERAL_test:
			case LITERAL_rule:
			{
				res=actionDecl();
				break;
			}
			case LITERAL_edge:
			{
				res=edgeClassDecl();
				break;
			}
			case LITERAL_node:
			{
				res=nodeClassDecl();
				break;
			}
			case LITERAL_enum:
			{
				res=enumDecl();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_3);
		}
		return res;
	}
	
/**
 * graph declarations contain
 * - rules
 * - tests
 */
	public final BaseNode  actionDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			switch ( LA(1)) {
			case LITERAL_test:
			{
				res=testDecl();
				break;
			}
			case LITERAL_rule:
			{
				res=ruleDecl();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_4);
		}
		return res;
	}
	
/**
 * An edge class decl makes a new type decl node with the declaring id and 
 * a new edge type node as children
 */
	public final BaseNode  edgeClassDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			
				  BaseNode body, ext;
				  IdentNode id;
			
			match(LITERAL_edge);
			match(LITERAL_class);
			id=identDecl();
			ext=edgeExtends();
			pushScope(id);
			match(LBRACE);
			body=edgeClassBody();
			
			
					id.setDecl(new TypeDeclNode(id, new EdgeTypeNode(ext, body)));
					res = id;
			
			match(RBRACE);
			popScope();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_3);
		}
		return res;
	}
	
	public final BaseNode  nodeClassDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			
					BaseNode body, ext;
					IdentNode id;
				
			match(LITERAL_node);
			match(LITERAL_class);
			id=identDecl();
			ext=nodeExtends();
			pushScope(id);
			match(LBRACE);
			body=nodeClassBody();
			
			
			id.setDecl(new TypeDeclNode(id, new NodeTypeNode(ext, body)));
					  res = id;
			
			match(RBRACE);
			popScope();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_3);
		}
		return res;
	}
	
	public final BaseNode  enumDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				IdentNode id;
				BaseNode c;
			
		
		try {      // for error handling
			match(LITERAL_enum);
			id=identDecl();
			pushScope(id);
			match(LBRACE);
			c=enumList();
			
					BaseNode enumType = new EnumTypeNode();
					enumType.addChild(c);
					res = new TypeDeclNode(id, enumType);
					res.addChild(c);
				
			match(RBRACE);
			popScope();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_3);
		}
		return res;
	}
	
	public final CollectNode  edgeExtends() throws RecognitionException, TokenStreamException {
		 CollectNode c = new CollectNode() ;
		
		
		try {      // for error handling
			switch ( LA(1)) {
			case LITERAL_extends:
			{
				match(LITERAL_extends);
				edgeExtendsCont(c);
				break;
			}
			case LBRACE:
			{
				c.addChild(edgeRoot);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_5);
		}
		return c;
	}
	
	public final void pushScope(
		IdentNode name
	) throws RecognitionException, TokenStreamException {
		
		
		currScope = currScope.newScope(name.toString());
		BaseNode.setCurrScope(currScope);    
		
		
	}
	
	public final CollectNode  edgeClassBody() throws RecognitionException, TokenStreamException {
		 CollectNode c = new CollectNode() ;
		
		
		try {      // for error handling
			BaseNode d;
			{
			_loop22:
			do {
				if ((LA(1)==IDENT)) {
					d=basicDecl();
					c.addChild(d);
					match(SEMI);
				}
				else {
					break _loop22;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
		return c;
	}
	
	public final void popScope() throws RecognitionException, TokenStreamException {
		
		
		if(currScope != rootScope)
		currScope = currScope.leaveScope();
		BaseNode.setCurrScope(currScope);    
		
		
		
	}
	
	public final BaseNode  connectAssertion() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		BaseNode src, tgt, srcRange, tgtRange;
		
		try {      // for error handling
			src=identUse();
			srcRange=rangeSpec();
			match(RARROW);
			tgt=identUse();
			tgtRange=rangeSpec();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
		return res;
	}
	
/**
 * Represents the usage of an identifier. 
 * It is checked, whether the identifier is declared. The IdentNode
 * created by the definition is returned.
 */
	public final IdentNode  identUse() throws RecognitionException, TokenStreamException {
		 IdentNode res = getDummyIdent() ;
		
		Token  i = null;
		
		try {      // for error handling
			i = LT(1);
			match(IDENT);
			
				Symbol sym = symbolTable.get(i.getText());
				Symbol.Occurrence occ = currScope.occurs(sym, getCoords(i));
			
				  res = new IdentNode(occ);
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_7);
		}
		return res;
	}
	
	public final BaseNode  rangeSpec() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  l = null;
		
				int lower = 1, upper = 1;
			
		
		try {      // for error handling
			{
			switch ( LA(1)) {
			case LBRACK:
			{
				l = LT(1);
				match(LBRACK);
				lower=integerConst();
				{
				switch ( LA(1)) {
				case COLON:
				{
					match(COLON);
					upper = RangeSpecNode.UNBOUND;
					{
					switch ( LA(1)) {
					case INTEGER:
					{
						upper=integerConst();
						break;
					}
					case RBRACK:
					{
						break;
					}
					default:
					{
						throw new NoViableAltException(LT(1), getFilename());
					}
					}
					}
					break;
				}
				case RBRACK:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				match(RBRACK);
				break;
			}
			case EOF:
			case RARROW:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			
						res = new RangeSpecNode(getCoords(l), lower, upper);
				
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_8);
		}
		return res;
	}
	
	public final CollectNode  nodeExtends() throws RecognitionException, TokenStreamException {
		 CollectNode c = new CollectNode() ;
		
		
		try {      // for error handling
			switch ( LA(1)) {
			case LITERAL_extends:
			{
				match(LITERAL_extends);
				nodeExtendsCont(c);
				break;
			}
			case LBRACE:
			{
				c.addChild(nodeRoot);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_5);
		}
		return c;
	}
	
	public final CollectNode  nodeClassBody() throws RecognitionException, TokenStreamException {
		 CollectNode c = new CollectNode() ;
		
		
		try {      // for error handling
			BaseNode d;
			{
			_loop19:
			do {
				if ((LA(1)==IDENT)) {
					d=basicDecl();
					c.addChild(d);
					match(SEMI);
				}
				else {
					break _loop19;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
		return c;
	}
	
	public final void edgeExtendsCont(
		 CollectNode c 
	) throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			BaseNode e;
			e=identUse();
			c.addChild(e);
			{
			_loop12:
			do {
				if ((LA(1)==COMMA)) {
					match(COMMA);
					e=identUse();
					c.addChild(e);
				}
				else {
					break _loop12;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_5);
		}
	}
	
	public final void nodeExtendsCont(
		 CollectNode c 
	) throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			BaseNode n;
			n=identUse();
			c.addChild(n);
			{
			_loop16:
			do {
				if ((LA(1)==COMMA)) {
					match(COMMA);
					n=identUse();
					c.addChild(n);
				}
				else {
					break _loop16;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_5);
		}
	}
	
	public final BaseNode  basicDecl() throws RecognitionException, TokenStreamException {
		 BaseNode n = initNode() ;
		
		
		try {      // for error handling
			
				  IdentNode id;
				  BaseNode type;
			
			id=identDecl();
			match(COLON);
			type=identUse();
			
				
				id.setDecl(new MemberDeclNode(id, type));
				n = id;
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_9);
		}
		return n;
	}
	
	public final int  integerConst() throws RecognitionException, TokenStreamException {
		 int value = 0 ;
		
		Token  i = null;
		
		try {      // for error handling
			i = LT(1);
			match(INTEGER);
			
					value = Integer.parseInt(i.getText());
				
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_10);
		}
		return value;
	}
	
	public final BaseNode  enumList() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				IdentNode id;
				res = new CollectNode(); 
			
		
		try {      // for error handling
			id=identDecl();
			res.addChild(id);
			{
			_loop31:
			do {
				if ((LA(1)==COMMA)) {
					match(COMMA);
					id=identDecl();
					res.addChild(id);
				}
				else {
					break _loop31;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
		return res;
	}
	
	public final BaseNode  groupDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			
					IdentNode id;
					CollectNode decls;
				
			match(LITERAL_group);
			id=identDecl();
			pushScope(id);
			match(LBRACE);
			decls=actionDecls();
			
				  	id.setDecl(new GroupDeclNode(id, decls));
				  	
				  	res = id;
			
			match(RBRACE);
			popScope();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
		return res;
	}
	
	public final CollectNode  actionDecls() throws RecognitionException, TokenStreamException {
		 CollectNode c = new CollectNode() ;
		
		
		try {      // for error handling
			BaseNode d;
			{
			int _cnt36=0;
			_loop36:
			do {
				if ((LA(1)==LITERAL_test||LA(1)==LITERAL_rule)) {
					d=actionDecl();
					c.addChild(d);
				}
				else {
					if ( _cnt36>=1 ) { break _loop36; } else {throw new NoViableAltException(LT(1), getFilename());}
				}
				
				_cnt36++;
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
		return c;
	}
	
	public final BaseNode  testDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			
					IdentNode id;
					BaseNode tb, pattern;
				
			match(LITERAL_test);
			id=identDecl();
			pushScope(id);
			match(LBRACE);
			pattern=patternPart();
			
			id.setDecl(new TestDeclNode(id, pattern));
			res = id;
				
			match(RBRACE);
			popScope();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_4);
		}
		return res;
	}
	
	public final BaseNode  ruleDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			
					IdentNode id;
					BaseNode rb, left, right;
					CollectNode redir = new CollectNode();
					CollectNode eval = new CollectNode();
					CollectNode cond = new CollectNode();
			
			match(LITERAL_rule);
			id=identDecl();
			pushScope(id);
			match(LBRACE);
			left=patternPart();
			{
			switch ( LA(1)) {
			case LITERAL_cond:
			{
				condPart(cond);
				break;
			}
			case LITERAL_replace:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			right=replacePart();
			{
			switch ( LA(1)) {
			case LITERAL_redirect:
			{
				redirectPart(redir);
				break;
			}
			case RBRACE:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
			
				id.setDecl(new RuleDeclNode(id, left, right, redir, cond, eval));
				res = id;
			
			match(RBRACE);
			popScope();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_4);
		}
		return res;
	}
	
	public final BaseNode  patternPart() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  p = null;
		
		try {      // for error handling
			p = LT(1);
			match(LITERAL_pattern);
			match(LBRACE);
			res=patternBody(getCoords(p));
			match(RBRACE);
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_11);
		}
		return res;
	}
	
	public final void condPart(
		 BaseNode n 
	) throws RecognitionException, TokenStreamException {
		
		Token  c = null;
		
		try {      // for error handling
			c = LT(1);
			match(LITERAL_cond);
			match(LBRACE);
			condBody(n);
			match(RBRACE);
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_12);
		}
	}
	
	public final BaseNode  replacePart() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  r = null;
		
		try {      // for error handling
			r = LT(1);
			match(LITERAL_replace);
			match(LBRACE);
			res=replaceBody(getCoords(r));
			match(RBRACE);
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_13);
		}
		return res;
	}
	
	public final void redirectPart(
		 CollectNode collect 
	) throws RecognitionException, TokenStreamException {
		
		Token  r = null;
		
		try {      // for error handling
			r = LT(1);
			match(LITERAL_redirect);
			match(LBRACE);
			redirectBody(collect, getCoords(r));
			match(RBRACE);
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
	}
	
	public final void parameters() throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			if ((LA(1)==LPAREN) && (LA(2)==LITERAL_in||LA(2)==LITERAL_out)) {
				match(LPAREN);
				paramList();
				match(RPAREN);
			}
			else if ((LA(1)==LPAREN) && (LA(2)==RPAREN)) {
				match(LPAREN);
				match(RPAREN);
			}
			else if ((LA(1)==EOF)) {
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
	}
	
	public final void paramList() throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			param();
			{
			_loop45:
			do {
				if ((LA(1)==COMMA)) {
					match(COMMA);
					param();
				}
				else {
					break _loop45;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_14);
		}
	}
	
	public final void param() throws RecognitionException, TokenStreamException {
		
		
				IdentNode id;
			
		
		try {      // for error handling
			inOutSpec();
			paramType();
			id=identDecl();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_15);
		}
	}
	
	public final void inOutSpec() throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			switch ( LA(1)) {
			case LITERAL_in:
			{
				match(LITERAL_in);
				break;
			}
			case LITERAL_out:
			{
				match(LITERAL_out);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_16);
		}
	}
	
	public final void paramType() throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			identUse();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_16);
		}
	}
	
/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
	public final BaseNode  patternBody(
		 Coords coords 
	) throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			
					BaseNode s;
					CollectNode connections = new CollectNode();
				  res = new PatternNode(coords, connections);
			
			{
			_loop67:
			do {
				if ((LA(1)==LITERAL_node||LA(1)==LPAREN||LA(1)==IDENT)) {
					patternStmt(connections);
					match(SEMI);
				}
				else {
					break _loop67;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
		return res;
	}
	
/**
 * Pattern bodies consist of connection nodes
 * The connection nodes in the collect node from subgraphSpec are integrated
 * In the collect node of the pattern node.
 */
	public final BaseNode  replaceBody(
		 Coords coords 
	) throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			
					BaseNode s;
					CollectNode connections = new CollectNode();
				  res = new PatternNode(coords, connections);
			
			{
			_loop90:
			do {
				if ((LA(1)==LITERAL_node||LA(1)==LPAREN||LA(1)==IDENT)) {
					replaceStmt(connections);
					match(SEMI);
				}
				else {
					break _loop90;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
		return res;
	}
	
	public final void redirectBody(
		 CollectNode c, Coords coords 
	) throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			{
			_loop62:
			do {
				if ((LA(1)==IDENT)) {
					redirectStmt(c);
					match(SEMI);
				}
				else {
					break _loop62;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
	}
	
	public final void evalPart(
		 BaseNode n 
	) throws RecognitionException, TokenStreamException {
		
		Token  e = null;
		
		try {      // for error handling
			e = LT(1);
			match(LITERAL_eval);
			match(LBRACE);
			evalBody(n);
			match(RBRACE);
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
	}
	
	public final void evalBody(
		 BaseNode n  
	) throws RecognitionException, TokenStreamException {
		
		BaseNode a;
		
		try {      // for error handling
			{
			_loop59:
			do {
				if ((LA(1)==IDENT)) {
					a=assignment();
					n.addChild(a);
					match(SEMI);
				}
				else {
					break _loop59;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
	}
	
	public final void condBody(
		 BaseNode n 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode e;
		
		try {      // for error handling
			{
			_loop56:
			do {
				if ((_tokenSet_17.member(LA(1)))) {
					e=expr();
					n.addChild(e);
					match(SEMI);
				}
				else {
					break _loop56;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_6);
		}
	}
	
	public final BaseNode  expr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			res=condExpr();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_18);
		}
		return res;
	}
	
	public final BaseNode  assignment() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  a = null;
		BaseNode q, e;
		
		try {      // for error handling
			q=qualIdent();
			a = LT(1);
			match(ASSIGN);
			e=expr();
			
				return new AssignNode(getCoords(a), q, e);
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_9);
		}
		return res;
	}
	
	public final void redirectStmt(
		 BaseNode c 
	) throws RecognitionException, TokenStreamException {
		
		
		try {      // for error handling
			
					BaseNode src, tgt, to;
					Object[] redirEdge;
				
			to=identUse();
			match(COLON);
			src=identUse();
			redirEdge=redirEdgeOcc();
			tgt=identUse();
			
					BaseNode edgeTypeId = (BaseNode) redirEdge[0];
					Boolean incoming = (Boolean) redirEdge[1];
					c.addChild(new RedirectionNode(src, edgeTypeId, tgt, to, incoming.booleanValue()));
				
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_9);
		}
	}
	
	public final  Object[]  redirEdgeOcc() throws RecognitionException, TokenStreamException {
		 Object[] res ;
		
		
					BaseNode id;
					res = new Object[2];
					res[1] = new Boolean(false);
			
		
		try {      // for error handling
			switch ( LA(1)) {
			case MINUS:
			{
				match(MINUS);
				id=identUse();
				match(RARROW);
				res[0] = id;
				break;
			}
			case RARROW:
			{
				match(RARROW);
				res[0] = edgeRoot; 	
				break;
			}
			default:
				if ((LA(1)==LARROW) && (LA(2)==IDENT) && (LA(3)==MINUS)) {
					match(LARROW);
					id=identUse();
					match(MINUS);
					
							res[0] = id;
							res[1] = new Boolean(true);
						
				}
				else if ((LA(1)==LARROW) && (LA(2)==IDENT) && (LA(3)==SEMI)) {
					match(LARROW);
					
							res[0] = edgeRoot;
							res[1] = new Boolean(true); 
						
				}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_16);
		}
		return res ;
	}
	
	public final void patternStmt(
		 BaseNode connCollect 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode n, o;
		
		try {      // for error handling
			switch ( LA(1)) {
			case LPAREN:
			case IDENT:
			{
				patternConnections(connCollect);
				break;
			}
			case LITERAL_node:
			{
				match(LITERAL_node);
				patternNodeDecl();
				{
				_loop70:
				do {
					if ((LA(1)==COMMA)) {
						match(COMMA);
						patternNodeDecl();
					}
					else {
						break _loop70;
					}
					
				} while (true);
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_9);
		}
	}
	
	public final void patternConnections(
		 BaseNode connColl 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode n;
		
		try {      // for error handling
			n=patternNodeOcc();
			{
			switch ( LA(1)) {
			case LPAREN:
			case MINUS:
			case LARROW:
			case NOTLARROW:
			case NOTMINUS:
			{
				patternContinuation(n,connColl);
				break;
			}
			case SEMI:
			{
				
					connColl.addChild(new SingleNodeConnNode(n));
				
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_9);
		}
	}
	
/**
 * In a pattern, a node decl is like multiNodeDecl (see below) or
 * a multi node with tildes instead of commas, like
 * 
 * (a ~ b ~ c ~ d):X
 *
 * This allows b, c, d to be the same node.
 */
	public final BaseNode  patternNodeDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		IdentNode id;
			BaseNode type; 
			List ids;
		
		
		try {      // for error handling
			if ((LA(1)==LPAREN||LA(1)==IDENT) && (LA(2)==COLON||LA(2)==IDENT) && (LA(3)==COMMA||LA(3)==RPAREN||LA(3)==IDENT)) {
				res=multiNodeDecl();
			}
			else if ((LA(1)==LPAREN) && (LA(2)==IDENT) && (LA(3)==TILDE)) {
				match(LPAREN);
				ids = new LinkedList();
				id=identDecl();
				ids.add(id);
				{
				int _cnt87=0;
				_loop87:
				do {
					if ((LA(1)==TILDE)) {
						match(TILDE);
						id=identDecl();
							ids.add(id);
					}
					else {
						if ( _cnt87>=1 ) { break _loop87; } else {throw new NoViableAltException(LT(1), getFilename());}
					}
					
					_cnt87++;
				} while (true);
				}
				match(RPAREN);
				match(COLON);
				type=identUse();
				
					
					IdentNode[] idents = (IdentNode[]) ids.toArray(new IdentNode[0]);
					BaseNode[] colls = new BaseNode[idents.length];
					NodeDeclNode[] decls = new NodeDeclNode[idents.length];
				
							for(int i = 0; i < idents.length; i++) {
								colls[i] = new CollectNode();
								decls[i] = new NodeDeclNode(idents[i], type, colls[i]);
							}
				
							for(int i = 0; i < idents.length; i++) 
								for(int j = i + 1; j < idents.length; j++) 
									colls[i].addChild(idents[j]);
				
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_19);
		}
		return res;
	}
	
/**
 * The occurrence of a node in a pattern part is the usage of an
 * identifier (must be a declared node) or a pattern node declaration
 */
	public final BaseNode  patternNodeOcc() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			if ((LA(1)==IDENT) && (_tokenSet_19.member(LA(2)))) {
				res=identUse();
			}
			else if ((LA(1)==LPAREN||LA(1)==IDENT) && (LA(2)==COLON||LA(2)==IDENT)) {
				res=patternNodeDecl();
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_19);
		}
		return res;
	}
	
/**
 * Acontinuation is a list of edge node pairs or a list of these pair lists, comma
 * seperated and delimited by parantheses.
 * all produced connection nodes are appended to the collect node
 */
	public final void patternContinuation(
		 BaseNode left, BaseNode collect 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode n;
		
		try {      // for error handling
			switch ( LA(1)) {
			case MINUS:
			case LARROW:
			case NOTLARROW:
			case NOTMINUS:
			{
				n=patternPair(left, collect);
				{
				switch ( LA(1)) {
				case LPAREN:
				case MINUS:
				case LARROW:
				case NOTLARROW:
				case NOTMINUS:
				{
					patternContinuation(n, collect);
					break;
				}
				case SEMI:
				case COMMA:
				case RPAREN:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				break;
			}
			case LPAREN:
			{
				match(LPAREN);
				patternContinuation(left, collect);
				{
				_loop76:
				do {
					if ((LA(1)==COMMA)) {
						match(COMMA);
						patternContinuation(left, collect);
					}
					else {
						break _loop76;
					}
					
				} while (true);
				}
				match(RPAREN);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_20);
		}
	}
	
/**
 * An edge node pair.
 * This rule builds a connection node with the parameter left, 
 * the edge and the patternNodeOcc
 * and appends this connection node to the children of coll.
 * The rule returns the right node (the one from the nodeOcc rule)
 * It also treats reversed edges (a <-- b).
 */
	public final BaseNode  patternPair(
		 BaseNode left, BaseNode coll 
	) throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				BaseNode e;
		
		
		try {      // for error handling
			switch ( LA(1)) {
			case MINUS:
			case NOTMINUS:
			{
				e=patternEdge();
				res=patternNodeOcc();
				
							coll.addChild(new ConnectionNode(left, e, res));
					
				break;
			}
			case LARROW:
			case NOTLARROW:
			{
				e=patternReversedEdge();
				res=patternNodeOcc();
				
						  coll.addChild(new ConnectionNode(res, e, left));
					
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_19);
		}
		return res;
	}
	
	public final BaseNode  patternEdge() throws RecognitionException, TokenStreamException {
		 BaseNode res = null ;
		
		Token  m = null;
		
				boolean negated = false;
				BaseNode type = edgeRoot;
			
		
		try {      // for error handling
			if ((LA(1)==MINUS) && (LA(2)==IDENT)) {
				match(MINUS);
				res=edgeDecl();
				match(RARROW);
			}
			else if ((LA(1)==MINUS||LA(1)==NOTMINUS) && (LA(2)==RARROW||LA(2)==COLON)) {
				{
				switch ( LA(1)) {
				case NOTMINUS:
				{
					match(NOTMINUS);
					negated = true;
					break;
				}
				case MINUS:
				{
					match(MINUS);
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				{
				switch ( LA(1)) {
				case COLON:
				{
					match(COLON);
					type=identUse();
					break;
				}
				case RARROW:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				m = LT(1);
				match(RARROW);
				
						IdentNode id = defineAnonymous("edge", getCoords(m));
						res = new AnonymousEdgeDeclNode(id, type, negated);
				
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_21);
		}
		return res;
	}
	
	public final BaseNode  patternReversedEdge() throws RecognitionException, TokenStreamException {
		 BaseNode res = null ;
		
		Token  m = null;
		
			boolean negated = false; 
			BaseNode type = edgeRoot;
		
		
		try {      // for error handling
			if ((LA(1)==LARROW) && (LA(2)==IDENT)) {
				match(LARROW);
				res=edgeDecl();
				match(MINUS);
			}
			else if ((LA(1)==LARROW||LA(1)==NOTLARROW) && (LA(2)==COLON||LA(2)==MINUS)) {
				{
				switch ( LA(1)) {
				case NOTLARROW:
				{
					match(NOTLARROW);
					negated = true;
					break;
				}
				case LARROW:
				{
					match(LARROW);
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				{
				switch ( LA(1)) {
				case COLON:
				{
					match(COLON);
					type=identUse();
					break;
				}
				case MINUS:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				m = LT(1);
				match(MINUS);
				
						IdentNode id = defineAnonymous("edge", getCoords(m));
						res = new AnonymousEdgeDeclNode(id, type, negated);
				
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_21);
		}
		return res;
	}
	
	public final EdgeDeclNode  edgeDecl() throws RecognitionException, TokenStreamException {
		 EdgeDeclNode res = null ;
		
		
				IdentNode id, type;
			
		
		try {      // for error handling
			id=identDecl();
			match(COLON);
			type=identUse();
			
					res = new EdgeDeclNode(id, type, false);
				
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_22);
		}
		return res;
	}
	
/**
 * The declaration of node(s)
 * It can look like 
 *
 * 1) a:X  
 * 2) (a, b, c, d, e):X
 *
 * In the second case, always the first node is returned.
 */
	public final BaseNode  multiNodeDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				List ids = new LinkedList();
				IdentNode id;
				BaseNode type;
			
		
		try {      // for error handling
			switch ( LA(1)) {
			case IDENT:
			{
				id=identDecl();
				match(COLON);
				type=identUse();
				
						res = new NodeDeclNode(id, type);
					
				break;
			}
			case LPAREN:
			{
				match(LPAREN);
				ids = new LinkedList();
				id=identDecl();
				ids.add(id);
				{
				_loop110:
				do {
					if ((LA(1)==COMMA)) {
						match(COMMA);
						id=identDecl();
						ids.add(id);
					}
					else {
						break _loop110;
					}
					
				} while (true);
				}
				match(RPAREN);
				match(COLON);
				type=identUse();
				
				
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
					
				
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_19);
		}
		return res;
	}
	
	public final void replaceStmt(
		 BaseNode connCollect 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode n;
		
		try {      // for error handling
			switch ( LA(1)) {
			case LPAREN:
			case IDENT:
			{
				replaceConnections(connCollect);
				break;
			}
			case LITERAL_node:
			{
				match(LITERAL_node);
				replaceNodeDecl();
				{
				_loop93:
				do {
					if ((LA(1)==COMMA)) {
						match(COMMA);
						replaceNodeDecl();
					}
					else {
						break _loop93;
					}
					
				} while (true);
				}
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_9);
		}
	}
	
	public final void replaceConnections(
		 BaseNode connColl 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode n;
		
		try {      // for error handling
			n=replaceNodeOcc();
			{
			switch ( LA(1)) {
			case LPAREN:
			case MINUS:
			case LARROW:
			{
				replaceContinuation(n, connColl);
				break;
			}
			case SEMI:
			{
				
					connColl.addChild(new SingleNodeConnNode(n));
				
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_9);
		}
	}
	
	public final BaseNode  replaceNodeDecl() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		IdentNode id;
			BaseNode type; 
		
		
		try {      // for error handling
			res=multiNodeDecl();
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_23);
		}
		return res;
	}
	
/**
 * The occurrence of a node.
 * A node occurrence is either the declaration of a new node, a usage of
 * a already declared node or a usage combined with a type change.
 */
	public final BaseNode  replaceNodeOcc() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  c = null;
		
				CollectNode coll = new CollectNode(); 
			  IdentNode id;
			
		
		try {      // for error handling
			if ((LA(1)==IDENT) && (_tokenSet_23.member(LA(2)))) {
				res=identUse();
			}
			else if ((LA(1)==LPAREN||LA(1)==IDENT) && (LA(2)==COLON||LA(2)==IDENT)) {
				res=replaceNodeDecl();
			}
			else if ((LA(1)==IDENT) && (LA(2)==DOUBLECOLON)) {
				res=identUse();
				c = LT(1);
				match(DOUBLECOLON);
				id=identUse();
				
					res = new NodeTypeChangeNode(getCoords(c), res, id);	
				
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_23);
		}
		return res;
	}
	
/**
 * Acontinuation is a list of edge node pairs or a list of these pair lists, comma
 * seperated and delimited by parantheses.
 * all produced connection nodes are appended to the collect node
 */
	public final void replaceContinuation(
		 BaseNode left, BaseNode collect 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode n;
		
		try {      // for error handling
			switch ( LA(1)) {
			case MINUS:
			case LARROW:
			{
				n=replacePair(left, collect);
				{
				switch ( LA(1)) {
				case LPAREN:
				case MINUS:
				case LARROW:
				{
					replaceContinuation(n, collect);
					break;
				}
				case SEMI:
				case COMMA:
				case RPAREN:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				break;
			}
			case LPAREN:
			{
				match(LPAREN);
				replaceContinuation(left, collect);
				{
				_loop99:
				do {
					if ((LA(1)==COMMA)) {
						match(COMMA);
						replaceContinuation(left, collect);
					}
					else {
						break _loop99;
					}
					
				} while (true);
				}
				match(RPAREN);
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_20);
		}
	}
	
/**
 * An edge node pair.
 * This rule builds a connection node with the parameter left, the edge and the nodeOcc
 * and appends this connection node to the children of coll.
 * The rule returns the right node (the one from the nodeOcc rule)
 */
	public final BaseNode  replacePair(
		 BaseNode left, BaseNode coll 
	) throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		BaseNode e;
		
		try {      // for error handling
			switch ( LA(1)) {
			case MINUS:
			{
				e=replaceEdge();
				res=replaceNodeOcc();
				
						  coll.addChild(new ConnectionNode(left, e, res));
					
				break;
			}
			case LARROW:
			{
				e=replaceReversedEdge();
				res=replaceNodeOcc();
				
					  coll.addChild(new ConnectionNode(res, e, left));
				
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_23);
		}
		return res;
	}
	
	public final BaseNode  replaceEdge() throws RecognitionException, TokenStreamException {
		 BaseNode res = null ;
		
		Token  m = null;
		BaseNode type = edgeRoot;
		
		try {      // for error handling
			if ((LA(1)==MINUS) && (LA(2)==IDENT) && (LA(3)==COLON)) {
				match(MINUS);
				res=edgeDecl();
				match(RARROW);
			}
			else if ((LA(1)==MINUS) && (LA(2)==IDENT) && (LA(3)==RARROW)) {
				match(MINUS);
				res=identUse();
				match(RARROW);
			}
			else if ((LA(1)==MINUS) && (LA(2)==RARROW||LA(2)==COLON)) {
				m = LT(1);
				match(MINUS);
				{
				switch ( LA(1)) {
				case COLON:
				{
					match(COLON);
					type=identUse();
					break;
				}
				case RARROW:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				match(RARROW);
				
						IdentNode id = defineAnonymous("edge", getCoords(m));
						res = new AnonymousEdgeDeclNode(id, type);
					
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_21);
		}
		return res;
	}
	
	public final BaseNode  replaceReversedEdge() throws RecognitionException, TokenStreamException {
		 BaseNode res = null ;
		
		Token  m = null;
		BaseNode type = edgeRoot;
		
		try {      // for error handling
			if ((LA(1)==LARROW) && (LA(2)==IDENT) && (LA(3)==COLON)) {
				match(LARROW);
				res=edgeDecl();
				match(MINUS);
			}
			else if ((LA(1)==LARROW) && (LA(2)==IDENT) && (LA(3)==MINUS)) {
				match(LARROW);
				res=identUse();
				match(MINUS);
			}
			else if ((LA(1)==LARROW) && (LA(2)==COLON||LA(2)==MINUS)) {
				m = LT(1);
				match(LARROW);
				{
				switch ( LA(1)) {
				case COLON:
				{
					match(COLON);
					type=identUse();
					break;
				}
				case MINUS:
				{
					break;
				}
				default:
				{
					throw new NoViableAltException(LT(1), getFilename());
				}
				}
				}
				match(MINUS);
				
						IdentNode id = defineAnonymous("edge", getCoords(m));
						res = new AnonymousEdgeDeclNode(id, type);
					
			}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_21);
		}
		return res;
	}
	
	public final BaseNode  anonymousEdge(
		 boolean negated 
	) throws RecognitionException, TokenStreamException {
		 BaseNode res = null ;
		
		Token  m = null;
		
		try {      // for error handling
			match(MINUS);
			m = LT(1);
			match(RARROW);
			
					IdentNode id = defineAnonymous("edge", getCoords(m));
					res = new AnonymousEdgeDeclNode(id, edgeRoot, negated);
				
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
		return res;
	}
	
	public final void identList(
		 BaseNode c 
	) throws RecognitionException, TokenStreamException {
		
		BaseNode id;
		
		try {      // for error handling
			id=identUse();
			c.addChild(id);
			{
			_loop114:
			do {
				if ((LA(1)==COMMA)) {
					match(COMMA);
					id=identUse();
					c.addChild(id);
				}
				else {
					break _loop114;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_0);
		}
	}
	
	public final BaseNode  qualIdent() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  d = null;
		BaseNode id;
		
		try {      // for error handling
			res=identExpr();
			{
			int _cnt164=0;
			_loop164:
			do {
				if ((LA(1)==DOT)) {
					d = LT(1);
					match(DOT);
					id=identExpr();
					
							res = new QualIdentNode(getCoords(d), res, id);
						
				}
				else {
					if ( _cnt164>=1 ) { break _loop164; } else {throw new NoViableAltException(LT(1), getFilename());}
				}
				
				_cnt164++;
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_24);
		}
		return res;
	}
	
	public final BaseNode  condExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  t = null;
		BaseNode op0, op1, op2;
		
		try {      // for error handling
			op0=logOrExpr();
			res=op0;
			{
			switch ( LA(1)) {
			case QUESTION:
			{
				t = LT(1);
				match(QUESTION);
				op1=expr();
				match(COLON);
				op2=condExpr();
				
						res=makeOp(t);
						res.addChild(op0);
						res.addChild(op1);
						res.addChild(op2);
					
				break;
			}
			case SEMI:
			case COLON:
			case RPAREN:
			{
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_18);
		}
		return res;
	}
	
	public final BaseNode  logOrExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  t = null;
		BaseNode op;
		
		try {      // for error handling
			res=logAndExpr();
			{
			_loop125:
			do {
				if ((LA(1)==LOR)) {
					t = LT(1);
					match(LOR);
					op=logAndExpr();
					
							res=makeBinOp(t, res, op);
						
				}
				else {
					break _loop125;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_25);
		}
		return res;
	}
	
	public final BaseNode  logAndExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  t = null;
		BaseNode op;
		
		try {      // for error handling
			res=bitOrExpr();
			{
			_loop128:
			do {
				if ((LA(1)==LAND)) {
					t = LT(1);
					match(LAND);
					op=bitOrExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop128;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_26);
		}
		return res;
	}
	
	public final BaseNode  bitOrExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  t = null;
		BaseNode op;
		
		try {      // for error handling
			res=bitXOrExpr();
			{
			_loop131:
			do {
				if ((LA(1)==BOR)) {
					t = LT(1);
					match(BOR);
					op=bitXOrExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop131;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_27);
		}
		return res;
	}
	
	public final BaseNode  bitXOrExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  t = null;
		BaseNode op;
		
		try {      // for error handling
			res=bitAndExpr();
			{
			_loop134:
			do {
				if ((LA(1)==BXOR)) {
					t = LT(1);
					match(BXOR);
					op=bitAndExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop134;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_28);
		}
		return res;
	}
	
	public final BaseNode  bitAndExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  t = null;
		BaseNode op;
		
		try {      // for error handling
			res=eqExpr();
			{
			_loop137:
			do {
				if ((LA(1)==BAND)) {
					t = LT(1);
					match(BAND);
					op=eqExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop137;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_29);
		}
		return res;
	}
	
	public final BaseNode  eqExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				BaseNode op;
				Token t;
			
		
		try {      // for error handling
			res=relExpr();
			{
			_loop141:
			do {
				if ((LA(1)==EQUAL||LA(1)==NOT_EQUAL)) {
					t=eqOp();
					op=relExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop141;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_30);
		}
		return res;
	}
	
	public final Token  eqOp() throws RecognitionException, TokenStreamException {
		 Token t = null ;
		
		Token  e = null;
		Token  n = null;
		
		try {      // for error handling
			switch ( LA(1)) {
			case EQUAL:
			{
				e = LT(1);
				match(EQUAL);
				t=e;
				break;
			}
			case NOT_EQUAL:
			{
				n = LT(1);
				match(NOT_EQUAL);
				t=n;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_17);
		}
		return t;
	}
	
	public final BaseNode  relExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res  = initNode() ;
		
		
				BaseNode op; 
				Token t;
			
		
		try {      // for error handling
			res=shiftExpr();
			{
			_loop145:
			do {
				if (((LA(1) >= LT && LA(1) <= GE))) {
					t=relOp();
					op=shiftExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop145;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_31);
		}
		return res;
	}
	
	public final Token  relOp() throws RecognitionException, TokenStreamException {
		 Token t = null ;
		
		Token  lt = null;
		Token  le = null;
		Token  gt = null;
		Token  ge = null;
		
		try {      // for error handling
			switch ( LA(1)) {
			case LT:
			{
				lt = LT(1);
				match(LT);
				t=lt;
				break;
			}
			case LE:
			{
				le = LT(1);
				match(LE);
				t=le;
				break;
			}
			case GT:
			{
				gt = LT(1);
				match(GT);
				t=gt;
				break;
			}
			case GE:
			{
				ge = LT(1);
				match(GE);
				t=ge;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_17);
		}
		return t;
	}
	
	public final BaseNode  shiftExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				BaseNode op;
				Token t;
			
		
		try {      // for error handling
			res=addExpr();
			{
			_loop149:
			do {
				if (((LA(1) >= SL && LA(1) <= BSR))) {
					t=shiftOp();
					op=addExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop149;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_32);
		}
		return res;
	}
	
	public final Token  shiftOp() throws RecognitionException, TokenStreamException {
		 Token res = null ;
		
		Token  l = null;
		Token  r = null;
		Token  b = null;
		
		try {      // for error handling
			switch ( LA(1)) {
			case SL:
			{
				l = LT(1);
				match(SL);
				res=l;
				break;
			}
			case SR:
			{
				r = LT(1);
				match(SR);
				res=r;
				break;
			}
			case BSR:
			{
				b = LT(1);
				match(BSR);
				res=b;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_17);
		}
		return res;
	}
	
	public final BaseNode  addExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				BaseNode op;
				Token t;
			
		
		try {      // for error handling
			res=mulExpr();
			{
			_loop153:
			do {
				if ((LA(1)==MINUS||LA(1)==PLUS)) {
					t=addOp();
					op=mulExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop153;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_33);
		}
		return res;
	}
	
	public final Token  addOp() throws RecognitionException, TokenStreamException {
		 Token t = null ;
		
		Token  p = null;
		Token  m = null;
		
		try {      // for error handling
			switch ( LA(1)) {
			case PLUS:
			{
				p = LT(1);
				match(PLUS);
				t=p;
				break;
			}
			case MINUS:
			{
				m = LT(1);
				match(MINUS);
				t=m;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_17);
		}
		return t;
	}
	
	public final BaseNode  mulExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
				BaseNode op;
				Token t;
			
		
		try {      // for error handling
			res=unaryExpr();
			{
			_loop157:
			do {
				if (((LA(1) >= STAR && LA(1) <= DIV))) {
					t=mulOp();
					op=unaryExpr();
					
							res = makeBinOp(t, res, op);
						
				}
				else {
					break _loop157;
				}
				
			} while (true);
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_34);
		}
		return res;
	}
	
	public final Token  mulOp() throws RecognitionException, TokenStreamException {
		 Token t = null ;
		
		Token  s = null;
		Token  m = null;
		Token  d = null;
		
		try {      // for error handling
			switch ( LA(1)) {
			case STAR:
			{
				s = LT(1);
				match(STAR);
				t=s;
				break;
			}
			case MOD:
			{
				m = LT(1);
				match(MOD);
				t=m;
				break;
			}
			case DIV:
			{
				d = LT(1);
				match(DIV);
				t=d;
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_17);
		}
		return t;
	}
	
	public final BaseNode  unaryExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  t = null;
		Token  n = null;
		Token  m = null;
		Token  p = null;
		BaseNode op, id;
		
		try {      // for error handling
			switch ( LA(1)) {
			case TILDE:
			{
				t = LT(1);
				match(TILDE);
				op=unaryExpr();
				
						res = makeUnOp(t, op);
					
				break;
			}
			case NOT:
			{
				n = LT(1);
				match(NOT);
				op=unaryExpr();
				
						res = makeUnOp(n, op);
					
				break;
			}
			case MINUS:
			{
				m = LT(1);
				match(MINUS);
				op=unaryExpr();
				
						res = new OpNode(getCoords(m), OperatorSignature.NEG);
						res.addChild(op);
					
				break;
			}
			case PLUS:
			{
				match(PLUS);
				res=unaryExpr();
				break;
			}
			case LITERAL_cast:
			{
				p = LT(1);
				match(LITERAL_cast);
				match(LT);
				id=identUse();
				match(GT);
				match(LPAREN);
				op=expr();
				match(RPAREN);
				
						res = new CastNode(getCoords(p));
						res.addChild(id);
						res.addChild(op);
					
				break;
			}
			case LPAREN:
			case IDENT:
			case NUM_DEC:
			case NUM_HEX:
			case STRING_LITERAL:
			case LITERAL_true:
			case LITERAL_false:
			{
				res=primaryExpr();
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_35);
		}
		return res;
	}
	
	public final BaseNode  primaryExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		
		try {      // for error handling
			switch ( LA(1)) {
			case NUM_DEC:
			case NUM_HEX:
			case STRING_LITERAL:
			case LITERAL_true:
			case LITERAL_false:
			{
				res=constant();
				break;
			}
			case LPAREN:
			{
				match(LPAREN);
				res=expr();
				match(RPAREN);
				break;
			}
			default:
				if ((LA(1)==IDENT) && (LA(2)==DOT)) {
					res=qualIdent();
				}
				else if ((LA(1)==IDENT) && (_tokenSet_35.member(LA(2)))) {
					res=identExpr();
				}
			else {
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_35);
		}
		return res;
	}
	
	public final BaseNode  identExpr() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		IdentNode id;
		
		try {      // for error handling
			id=identUse();
			
					res = new IdentExprNode(id);
				
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_36);
		}
		return res;
	}
	
	public final BaseNode  constant() throws RecognitionException, TokenStreamException {
		 BaseNode res = initNode() ;
		
		Token  i = null;
		Token  h = null;
		Token  s = null;
		Token  t = null;
		Token  f = null;
		
		try {      // for error handling
			switch ( LA(1)) {
			case NUM_DEC:
			{
				i = LT(1);
				match(NUM_DEC);
				
						res = new IntConstNode(getCoords(i), Integer.parseInt(i.getText()));
					
				break;
			}
			case NUM_HEX:
			{
				h = LT(1);
				match(NUM_HEX);
				
						res = new IntConstNode(getCoords(h), Integer.parseInt(h.getText(), 16));
					
				break;
			}
			case STRING_LITERAL:
			{
				s = LT(1);
				match(STRING_LITERAL);
				
						res = new StringConstNode(getCoords(s), s.getText());
					
				break;
			}
			case LITERAL_true:
			{
				t = LT(1);
				match(LITERAL_true);
				
						res = new BoolConstNode(getCoords(t), true);
					
				break;
			}
			case LITERAL_false:
			{
				f = LT(1);
				match(LITERAL_false);
				
						res = new BoolConstNode(getCoords(f), false);
					
				break;
			}
			default:
			{
				throw new NoViableAltException(LT(1), getFilename());
			}
			}
		}
		catch (RecognitionException ex) {
			reportError(ex);
			consume();
			consumeUntil(_tokenSet_35);
		}
		return res;
	}
	
	
	public static final String[] _tokenNames = {
		"<0>",
		"EOF",
		"<2>",
		"NULL_TREE_LOOKAHEAD",
		"DECL_GROUP",
		"DECL_TEST",
		"DECL_TYPE",
		"DECL_NODE",
		"DECL_EDGE",
		"DECL_BASIC",
		"TYPE_NODE",
		"TYPE_EDGE",
		"SIMPLE_CONN",
		"GROUP_CONN",
		"TEST_BODY",
		"PATTERN_BODY",
		"SUBGRAPH_SPEC",
		"CONN_DECL",
		"CONN_CONT",
		"MAIN",
		"\"unit\"",
		"SEMI",
		"\"edge\"",
		"\"class\"",
		"LBRACE",
		"RBRACE",
		"RARROW",
		"\"node\"",
		"\"extends\"",
		"COMMA",
		"LBRACK",
		"COLON",
		"RBRACK",
		"INTEGER",
		"\"enum\"",
		"\"group\"",
		"\"test\"",
		"\"rule\"",
		"LPAREN",
		"RPAREN",
		"\"in\"",
		"\"out\"",
		"\"pattern\"",
		"\"replace\"",
		"\"redirect\"",
		"\"eval\"",
		"\"cond\"",
		"MINUS",
		"LARROW",
		"NOTLARROW",
		"NOTMINUS",
		"TILDE",
		"DOUBLECOLON",
		"IDENT",
		"ASSIGN",
		"QUESTION",
		"LOR",
		"LAND",
		"BOR",
		"BXOR",
		"BAND",
		"EQUAL",
		"NOT_EQUAL",
		"LT",
		"LE",
		"GT",
		"GE",
		"SL",
		"SR",
		"BSR",
		"PLUS",
		"STAR",
		"MOD",
		"DIV",
		"NOT",
		"\"cast\"",
		"NUM_DEC",
		"NUM_HEX",
		"STRING_LITERAL",
		"\"true\"",
		"\"false\"",
		"DOT",
		"WS",
		"SL_COMMENT",
		"ML_COMMENT",
		"ESC"
	};
	
	private static final long[] mk_tokenSet_0() {
		long[] data = { 2L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_0 = new BitSet(mk_tokenSet_0());
	private static final long[] mk_tokenSet_1() {
		long[] data = { 2252352574717952L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_1 = new BitSet(mk_tokenSet_1());
	private static final long[] mk_tokenSet_2() {
		long[] data = { 223476711424L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_2 = new BitSet(mk_tokenSet_2());
	private static final long[] mk_tokenSet_3() {
		long[] data = { 223476711426L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_3 = new BitSet(mk_tokenSet_3());
	private static final long[] mk_tokenSet_4() {
		long[] data = { 223510265858L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_4 = new BitSet(mk_tokenSet_4());
	private static final long[] mk_tokenSet_5() {
		long[] data = { 16777216L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_5 = new BitSet(mk_tokenSet_5());
	private static final long[] mk_tokenSet_6() {
		long[] data = { 33554432L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_6 = new BitSet(mk_tokenSet_6());
	private static final long[] mk_tokenSet_7() {
		long[] data = { -2391708824240126L, 132095L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_7 = new BitSet(mk_tokenSet_7());
	private static final long[] mk_tokenSet_8() {
		long[] data = { 67108866L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_8 = new BitSet(mk_tokenSet_8());
	private static final long[] mk_tokenSet_9() {
		long[] data = { 2097152L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_9 = new BitSet(mk_tokenSet_9());
	private static final long[] mk_tokenSet_10() {
		long[] data = { 6442450944L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_10 = new BitSet(mk_tokenSet_10());
	private static final long[] mk_tokenSet_11() {
		long[] data = { 79164870754304L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_11 = new BitSet(mk_tokenSet_11());
	private static final long[] mk_tokenSet_12() {
		long[] data = { 8796093022208L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_12 = new BitSet(mk_tokenSet_12());
	private static final long[] mk_tokenSet_13() {
		long[] data = { 17592219598848L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_13 = new BitSet(mk_tokenSet_13());
	private static final long[] mk_tokenSet_14() {
		long[] data = { 549755813888L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_14 = new BitSet(mk_tokenSet_14());
	private static final long[] mk_tokenSet_15() {
		long[] data = { 550292684800L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_15 = new BitSet(mk_tokenSet_15());
	private static final long[] mk_tokenSet_16() {
		long[] data = { 9007199254740992L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_16 = new BitSet(mk_tokenSet_16());
	private static final long[] mk_tokenSet_17() {
		long[] data = { 11400011434688512L, 130112L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_17 = new BitSet(mk_tokenSet_17());
	private static final long[] mk_tokenSet_18() {
		long[] data = { 551905394688L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_18 = new BitSet(mk_tokenSet_18());
	private static final long[] mk_tokenSet_19() {
		long[] data = { 2111887498018816L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_19 = new BitSet(mk_tokenSet_19());
	private static final long[] mk_tokenSet_20() {
		long[] data = { 550294781952L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_20 = new BitSet(mk_tokenSet_20());
	private static final long[] mk_tokenSet_21() {
		long[] data = { 9007474132647936L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_21 = new BitSet(mk_tokenSet_21());
	private static final long[] mk_tokenSet_22() {
		long[] data = { 140737555464192L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_22 = new BitSet(mk_tokenSet_22());
	private static final long[] mk_tokenSet_23() {
		long[] data = { 423037637754880L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_23 = new BitSet(mk_tokenSet_23());
	private static final long[] mk_tokenSet_24() {
		long[] data = { -17873109115731968L, 1023L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_24 = new BitSet(mk_tokenSet_24());
	private static final long[] mk_tokenSet_25() {
		long[] data = { 36029348924358656L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_25 = new BitSet(mk_tokenSet_25());
	private static final long[] mk_tokenSet_26() {
		long[] data = { 108086942962286592L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_26 = new BitSet(mk_tokenSet_26());
	private static final long[] mk_tokenSet_27() {
		long[] data = { 252202131038142464L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_27 = new BitSet(mk_tokenSet_27());
	private static final long[] mk_tokenSet_28() {
		long[] data = { 540432507189854208L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_28 = new BitSet(mk_tokenSet_28());
	private static final long[] mk_tokenSet_29() {
		long[] data = { 1116893259493277696L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_29 = new BitSet(mk_tokenSet_29());
	private static final long[] mk_tokenSet_30() {
		long[] data = { 2269814764100124672L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_30 = new BitSet(mk_tokenSet_30());
	private static final long[] mk_tokenSet_31() {
		long[] data = { 9187343791741206528L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_31 = new BitSet(mk_tokenSet_31());
	private static final long[] mk_tokenSet_32() {
		long[] data = { -36028245113569280L, 7L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_32 = new BitSet(mk_tokenSet_32());
	private static final long[] mk_tokenSet_33() {
		long[] data = { -36028245113569280L, 63L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_33 = new BitSet(mk_tokenSet_33());
	private static final long[] mk_tokenSet_34() {
		long[] data = { -35887507625213952L, 127L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_34 = new BitSet(mk_tokenSet_34());
	private static final long[] mk_tokenSet_35() {
		long[] data = { -35887507625213952L, 1023L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_35 = new BitSet(mk_tokenSet_35());
	private static final long[] mk_tokenSet_36() {
		long[] data = { -17873109115731968L, 132095L, 0L, 0L};
		return data;
	}
	public static final BitSet _tokenSet_36 = new BitSet(mk_tokenSet_36());
	
	}
