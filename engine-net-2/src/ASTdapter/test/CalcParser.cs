// $ANTLR 2.7.7 (20060930): "calc.g" -> "CalcParser.cs"$

	// Generate the header common to all output files.
	using System;

	using TokenBuffer              = antlr.TokenBuffer;
	using TokenStreamException     = antlr.TokenStreamException;
	using TokenStreamIOException   = antlr.TokenStreamIOException;
	using ANTLRException           = antlr.ANTLRException;
	using LLkParser = antlr.LLkParser;
	using Token                    = antlr.Token;
	using IToken                   = antlr.IToken;
	using TokenStream              = antlr.TokenStream;
	using RecognitionException     = antlr.RecognitionException;
	using NoViableAltException     = antlr.NoViableAltException;
	using MismatchedTokenException = antlr.MismatchedTokenException;
	using SemanticException        = antlr.SemanticException;
	using ParserSharedInputState   = antlr.ParserSharedInputState;
	using BitSet                   = antlr.collections.impl.BitSet;
	using AST                      = antlr.collections.AST;
	using ASTPair                  = antlr.ASTPair;
	using ASTFactory               = antlr.ASTFactory;
	using ASTArray                 = antlr.collections.impl.ASTArray;

	public 	class CalcParser : antlr.LLkParser
	{
		public const int EOF = 1;
		public const int NULL_TREE_LOOKAHEAD = 3;
		public const int PLUS = 4;
		public const int SEMI = 5;
		public const int STAR = 6;
		public const int INT = 7;
		public const int WS = 8;
		public const int LPAREN = 9;
		public const int RPAREN = 10;
		public const int DIGIT = 11;


		protected void initialize()
		{
			tokenNames = tokenNames_;
			initializeFactory();
		}


		protected CalcParser(TokenBuffer tokenBuf, int k) : base(tokenBuf, k)
		{
			initialize();
		}

		public CalcParser(TokenBuffer tokenBuf) : this(tokenBuf,1)
		{
		}

		protected CalcParser(TokenStream lexer, int k) : base(lexer,k)
		{
			initialize();
		}

		public CalcParser(TokenStream lexer) : this(lexer,1)
		{
		}

		public CalcParser(ParserSharedInputState state) : base(state,1)
		{
			initialize();
		}

	public void expr() //throws RecognitionException, TokenStreamException
{

		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST expr_AST = null;

		try {      // for error handling
			mexpr();
			astFactory.addASTChild(ref currentAST, returnAST);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==PLUS))
					{
						AST tmp3_AST = null;
						tmp3_AST = astFactory.create(LT(1));
						astFactory.makeASTRoot(ref currentAST, tmp3_AST);
						match(PLUS);
						mexpr();
						astFactory.addASTChild(ref currentAST, returnAST);
					}
					else
					{
						goto _loop3_breakloop;
					}

				}
_loop3_breakloop:				;
			}    // ( ... )*
			match(SEMI);
			expr_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_0_);
		}
		returnAST = expr_AST;
	}

	public void mexpr() //throws RecognitionException, TokenStreamException
{

		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST mexpr_AST = null;

		try {      // for error handling
			atom();
			astFactory.addASTChild(ref currentAST, returnAST);
			{    // ( ... )*
				for (;;)
				{
					if ((LA(1)==STAR))
					{
						AST tmp5_AST = null;
						tmp5_AST = astFactory.create(LT(1));
						astFactory.makeASTRoot(ref currentAST, tmp5_AST);
						match(STAR);
						atom();
						astFactory.addASTChild(ref currentAST, returnAST);
					}
					else
					{
						goto _loop6_breakloop;
					}

				}
_loop6_breakloop:				;
			}    // ( ... )*
			mexpr_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_1_);
		}
		returnAST = mexpr_AST;
	}

	public void atom() //throws RecognitionException, TokenStreamException
{

		returnAST = null;
		ASTPair currentAST = new ASTPair();
		AST atom_AST = null;

		try {      // for error handling
			AST tmp6_AST = null;
			tmp6_AST = astFactory.create(LT(1));
			astFactory.addASTChild(ref currentAST, tmp6_AST);
			match(INT);
			atom_AST = currentAST.root;
		}
		catch (RecognitionException ex)
		{
			reportError(ex);
			recover(ex,tokenSet_2_);
		}
		returnAST = atom_AST;
	}

	private void initializeFactory()
	{
		if (astFactory == null)
		{
			astFactory = new ASTFactory();
		}
		initializeASTFactory( astFactory );
	}
	static public void initializeASTFactory( ASTFactory factory )
	{
		factory.setMaxNodeType(11);
	}

	public static readonly string[] tokenNames_ = new string[] {
		@"""<0>""",
		@"""EOF""",
		@"""<2>""",
		@"""NULL_TREE_LOOKAHEAD""",
		@"""PLUS""",
		@"""SEMI""",
		@"""STAR""",
		@"""INT""",
		@"""WS""",
		@"""LPAREN""",
		@"""RPAREN""",
		@"""DIGIT"""
	};

	private static long[] mk_tokenSet_0_()
	{
		long[] data = { 2L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_0_ = new BitSet(mk_tokenSet_0_());
	private static long[] mk_tokenSet_1_()
	{
		long[] data = { 48L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_1_ = new BitSet(mk_tokenSet_1_());
	private static long[] mk_tokenSet_2_()
	{
		long[] data = { 112L, 0L};
		return data;
	}
	public static readonly BitSet tokenSet_2_ = new BitSet(mk_tokenSet_2_());

}
