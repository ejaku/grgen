/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * AnonymousPatternNamer.java
 *
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.parser;

import java.util.Stack;

import de.unika.ipd.grgen.ast.IdentNode;

public class AnonymousScopeNamer {
	public AnonymousScopeNamer(de.unika.ipd.grgen.parser.ParserEnvironment env) {
		this();
		this.env = env;
	}

	public AnonymousScopeNamer() {
		curAlt = new Stack<IdentNode>();
		curAltCase = new Stack<IdentNode>();
		curIter = new Stack<IdentNode>();
		curNeg = new Stack<IdentNode>();
		curIdpt = new Stack<IdentNode>();
		
		curYield = IdentNode.getInvalid();
		curEval = IdentNode.getInvalid();

		altCount = 0;
		altCaseCount = 0;
		iterCount = 0;
		negCount = 0;
		idptCount = 0;
		
		yieldCount = 0;
		evalCount = 0;
	}

	private static AnonymousScopeNamer dummy = new AnonymousScopeNamer();

	public static AnonymousScopeNamer getDummyNamer() // returns dummy object needed in syntactic predicate parsing
	{
		return dummy;
	}

	public void defAlt(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null)
			curAlt.push(maybeIdent);
		else
			curAlt.push(new IdentNode(env.define(ParserEnvironment.ALTERNATIVES, "alt_" + altCount, coords)));
		++altCount;
		altCaseCount = 0;
	}
	public void undefAlt() {
		curAlt.pop();
	}
	public IdentNode alt() {
		return curAlt.empty() ? IdentNode.getInvalid() : curAlt.peek();
	}

	public void defAltCase(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null)
			curAltCase.push(maybeIdent);
		else
			curAltCase.push(new IdentNode(env.define(ParserEnvironment.ALTERNATIVES, "_" + altCaseCount, coords)));
		++altCaseCount;
	}
	public void undefAltCase() {
		curAltCase.pop();
	}
	public IdentNode altCase() {
		return curAltCase.empty() ? IdentNode.getInvalid() : curAltCase.peek();
	}

	public void defIter(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null)
			curIter.push(maybeIdent);
		else
			curIter.push(new IdentNode(env.define(ParserEnvironment.ITERATEDS, "iter_" + iterCount, coords)));
		++iterCount;
	}
	public void undefIter() {
		curIter.pop();
	}
	public IdentNode iter() {
		return curIter.empty() ? IdentNode.getInvalid() : curIter.peek();
	}

	public void defNeg(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null)
			curNeg.push(maybeIdent);
		else
			curNeg.push(new IdentNode(env.define(ParserEnvironment.NEGATIVES, "neg_" + negCount, coords)));
		++negCount;
	}
	public void undefNeg() {
		curNeg.pop();
	}
	public IdentNode neg() {
		return curNeg.empty() ? IdentNode.getInvalid() : curNeg.peek();
	}

	public void defIdpt(IdentNode maybeIdent, Coords coords)	{
		if(maybeIdent!=null)
			curIdpt.push(maybeIdent);
		else
			curIdpt.push(new IdentNode(env.define(ParserEnvironment.INDEPENDENTS, "idpt_" + idptCount, coords)));
		++idptCount;
	}
	public void undefIdpt() {
		curIdpt.pop();
	}
	public IdentNode idpt() {
		return curIdpt.empty() ? IdentNode.getInvalid() : curIdpt.peek();
	}

	public void defYield(IdentNode maybeIdent, Coords coords)	{
		if(maybeIdent!=null)
			curYield = maybeIdent;
		else
			curYield = new IdentNode(env.define(ParserEnvironment.COMPUTATION_BLOCKS, "yield_" + yieldCount, coords));
		++yieldCount;
	}
	public void undefYield() {
		curYield = IdentNode.getInvalid();
	}
	public IdentNode yield() {
		return curYield;
	}

	public void defEval(IdentNode maybeIdent, Coords coords)	{
		if(maybeIdent!=null)
			curEval = maybeIdent;
		else
			curEval = new IdentNode(env.define(ParserEnvironment.COMPUTATION_BLOCKS, "eval_" + evalCount, coords));
		++evalCount;
	}
	public void undefEval() {
		curEval = IdentNode.getInvalid();
	}
	public IdentNode eval() {
		return curEval;
	}

	private int altCount;
	private int altCaseCount;
	private int iterCount;
	private int negCount;
	private int idptCount;

	private int yieldCount;
	private int evalCount;

	private Stack<IdentNode> curAlt;
	private Stack<IdentNode> curAltCase;
	private Stack<IdentNode> curIter;
	private Stack<IdentNode> curNeg;
	private Stack<IdentNode> curIdpt;
	
	private IdentNode curYield;
	private IdentNode curEval;

	private ParserEnvironment env;
}
