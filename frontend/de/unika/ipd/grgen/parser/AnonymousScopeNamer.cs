/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// AnonymousPatternNamer.java
/// 
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.parser
{

using System.Collections.Generic;

using IdentNode = de.unika.ipd.grgen.ast.IdentNode;

public class AnonymousScopeNamer
{
	public AnonymousScopeNamer(de.unika.ipd.grgen.parser.ParserEnvironment env)
		: this()
	{
		this.env = env;
	}

	public AnonymousScopeNamer()
	{
		curAlt = new Stack<IdentNode>();
		curAltCase = new Stack<IdentNode>();
		curIter = new Stack<IdentNode>();
		curNeg = new Stack<IdentNode>();
		curIdpt = new Stack<IdentNode>();

		curYield = IdentNode.Invalid;
		curEval = IdentNode.Invalid;
		curExprBlock = IdentNode.Invalid;

		altCount = 0;
		altCaseCount = 0;
		iterCount = 0;
		negCount = 0;
		idptCount = 0;
		exprBlockCount = 0;

		yieldCount = 0;
		evalCount = 0;
	}

	private static AnonymousScopeNamer dummy = new AnonymousScopeNamer();

	public static AnonymousScopeNamer DummyNamer
	{
		get
		{
		return dummy;
		}
	}

	public virtual void DefAlt(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curAlt.Push(maybeIdent);
		else
			curAlt.Push(new IdentNode(env.Define(ParserEnvironment.ALTERNATIVES, "alt_" + altCount, coords).DeclareAnonymous()));
		++altCount;
		altCaseCount = 0;
	}

	public virtual void UndefAlt()
	{
		curAlt.Pop();
	}

	public virtual IdentNode Alt()
	{
		return curAlt.Count == 0 ? IdentNode.Invalid : curAlt.Peek();
	}

	public virtual void DefAltCase(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curAltCase.Push(maybeIdent);
		else
			curAltCase.Push(new IdentNode(env.Define(ParserEnvironment.ALTERNATIVES, "_" + altCaseCount, coords).DeclareAnonymous()));
		++altCaseCount;
	}

	public virtual void UndefAltCase()
	{
		curAltCase.Pop();
	}

	public virtual IdentNode AltCase()
	{
		return curAltCase.Count == 0 ? IdentNode.Invalid : curAltCase.Peek();
	}

	public virtual void DefIter(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curIter.Push(maybeIdent);
		else
			curIter.Push(new IdentNode(env.Define(ParserEnvironment.ITERATEDS, "iter_" + iterCount, coords).DeclareAnonymous()));
		++iterCount;
	}

	public virtual void UndefIter()
	{
		curIter.Pop();
	}

	public virtual IdentNode Iter()
	{
		return curIter.Count == 0 ? IdentNode.Invalid : curIter.Peek();
	}

	public virtual void DefNeg(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curNeg.Push(maybeIdent);
		else
			curNeg.Push(new IdentNode(env.Define(ParserEnvironment.NEGATIVES, "neg_" + negCount, coords).DeclareAnonymous()));
		++negCount;
	}

	public virtual void UndefNeg()
	{
		curNeg.Pop();
	}

	public virtual IdentNode Neg()
	{
		return curNeg.Count == 0 ? IdentNode.Invalid : curNeg.Peek();
	}

	public virtual void DefIdpt(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curIdpt.Push(maybeIdent);
		else
			curIdpt.Push(new IdentNode(env.Define(ParserEnvironment.INDEPENDENTS, "idpt_" + idptCount, coords).DeclareAnonymous()));
		++idptCount;
	}

	public virtual void UndefIdpt()
	{
		curIdpt.Pop();
	}

	public virtual IdentNode Idpt()
	{
		return curIdpt.Count == 0 ? IdentNode.Invalid : curIdpt.Peek();
	}

	public virtual void DefYield(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curYield = maybeIdent;
		else
			curYield = new IdentNode(env.Define(ParserEnvironment.COMPUTATION_BLOCKS, "yield_" + yieldCount, coords).DeclareAnonymous());
		++yieldCount;
	}

	public virtual void UndefYield()
	{
		curYield = IdentNode.Invalid;
	}

	public virtual IdentNode Yield()
	{
		return curYield;
	}

	public virtual void DefEval(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curEval = maybeIdent;
		else
			curEval = new IdentNode(env.Define(ParserEnvironment.COMPUTATION_BLOCKS, "eval_" + evalCount, coords).DeclareAnonymous());
		++evalCount;
	}

	public virtual void UndefEval()
	{
		curEval = IdentNode.Invalid;
	}

	public virtual IdentNode Eval()
	{
		return curEval;
	}

	public virtual void DefExprBlock(IdentNode maybeIdent, Coords coords)
	{
		if(maybeIdent != null)
			curExprBlock = maybeIdent;
		else
			curExprBlock = new IdentNode(env.Define(ParserEnvironment.COMPUTATION_BLOCKS, "expr_block_" + exprBlockCount, coords).DeclareAnonymous());
		++exprBlockCount;
	}

	public virtual void UndefExprBlock()
	{
		curExprBlock = IdentNode.Invalid;
	}

	public virtual IdentNode ExprBlock()
	{
		return curExprBlock;
	}

	private int altCount;
	private int altCaseCount;
	private int iterCount;
	private int negCount;
	private int idptCount;

	private int yieldCount;
	private int evalCount;
	private int exprBlockCount;

	private Stack<IdentNode> curAlt;
	private Stack<IdentNode> curAltCase;
	private Stack<IdentNode> curIter;
	private Stack<IdentNode> curNeg;
	private Stack<IdentNode> curIdpt;

	private IdentNode curYield;
	private IdentNode curEval;
	private IdentNode curExprBlock;

	private ParserEnvironment env;
}

}
