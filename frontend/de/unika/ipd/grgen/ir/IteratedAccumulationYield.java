/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


/**
 * Represents an accumulation yielding of an iterated match def variable in the IR.
 */
public class IteratedAccumulationYield extends EvalStatement {

	private Variable iterationVar;
	private Rule iterated;
	private EvalStatement accumulationStatement;

	public IteratedAccumulationYield(Variable accumulationVar, Rule iterated, EvalStatement accumulationStatement) {
		super("iterated accumulation yield");
		this.iterationVar = accumulationVar;
		this.iterated = iterated;
		this.accumulationStatement = accumulationStatement;
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public Rule getIterated() {
		return iterated;
	}

	public EvalStatement getAccumulationStatement() {
		return accumulationStatement;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		accumulationStatement.collectNeededEntities(needs);
		needs.variables.remove(iterationVar);
	}
}
