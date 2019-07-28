/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * AnonymousPatternNamer.java
 *
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.ast.IdentNode;

public class AnonymousScopeNamer {
	public AnonymousScopeNamer(de.unika.ipd.grgen.parser.ParserEnvironment env) {
		this.env = env;
		altCount = 0;
		altCaseCount = 0;
		iterCount = 0;
		negCount = 0;
		idptCount = 0;
		yieldCount = 0;
		evalCount = 0;
	}

	public AnonymousScopeNamer() {
		curAlt = IdentNode.getInvalid();
		curAltCase = IdentNode.getInvalid();
		curIter = IdentNode.getInvalid();
		curNeg = IdentNode.getInvalid();
		curIdpt = IdentNode.getInvalid();
		curYield = IdentNode.getInvalid();
		curEval = IdentNode.getInvalid();
	}

	private static AnonymousScopeNamer dummy = new AnonymousScopeNamer();

	public static AnonymousScopeNamer getDummyNamer() // returns dummy object needed in syntactic predicate parsing
	{
		return dummy;
	}

	public void defAlt(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null) curAlt = maybeIdent;
		else curAlt = new IdentNode(env.define(ParserEnvironment.ALTERNATIVES, "alt_" + altCount, coords));
		++altCount;
		altCaseCount = 0;
	}
	public IdentNode alt() {
		return curAlt;
	}

	public void defAltCase(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null) curAltCase = maybeIdent;
		else curAltCase = new IdentNode(env.define(ParserEnvironment.ALTERNATIVES, "_" + altCaseCount, coords));
		++altCaseCount;
	}
	public IdentNode altCase() {
		return curAltCase;
	}

	public void defIter(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null) curIter = maybeIdent;
		else curIter = new IdentNode(env.define(ParserEnvironment.ITERATEDS, "iter_" + iterCount, coords));
		++iterCount;
	}
	public IdentNode iter() {
		return curIter;
	}

	public void defNeg(IdentNode maybeIdent, Coords coords) {
		if(maybeIdent!=null) curNeg = maybeIdent;
		else curNeg = new IdentNode(env.define(ParserEnvironment.NEGATIVES, "neg_" + negCount, coords));
		++negCount;
	}
	public IdentNode neg() {
		return curNeg;
	}

	public void defIdpt(IdentNode maybeIdent, Coords coords)	{
		if(maybeIdent!=null) curIdpt = maybeIdent;
		else curIdpt = new IdentNode(env.define(ParserEnvironment.INDEPENDENTS, "idpt_" + idptCount, coords));
		++idptCount;
	}
	public IdentNode idpt() {
		return curIdpt;
	}

	public void defYield(IdentNode maybeIdent, Coords coords)	{
		if(maybeIdent!=null) curYield = maybeIdent;
		else curYield = new IdentNode(env.define(ParserEnvironment.COMPUTATION_BLOCKS, "yield_" + yieldCount, coords));
		++yieldCount;
	}
	public IdentNode yield() {
		return curYield;
	}

	public void defEval(IdentNode maybeIdent, Coords coords)	{
		if(maybeIdent!=null) curEval = maybeIdent;
		else curEval = new IdentNode(env.define(ParserEnvironment.COMPUTATION_BLOCKS, "eval_" + evalCount, coords));
		++evalCount;
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

	private IdentNode curAlt;
	private IdentNode curAltCase;
	private IdentNode curIter;
	private IdentNode curNeg;
	private IdentNode curIdpt;
	private IdentNode curYield;
	private IdentNode curEval;

	private ParserEnvironment env;
}
