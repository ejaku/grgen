/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;
import java.util.LinkedList;

import de.unika.ipd.grgen.ir.*;

/**
 * Represents an accumulation yielding of an iterated match def variable in the IR.
 */
public class IteratedAccumulationYield extends EvalStatement {

	private Variable iterationVar;
	private Rule iterated;
	private Collection<EvalStatement> accumulationStatements = new LinkedList<EvalStatement>();

	public IteratedAccumulationYield(Variable accumulationVar, Rule iterated) {
		super("iterated accumulation yield");
		this.iterationVar = accumulationVar;
		this.iterated = iterated;
	}

	public void addAccumulationStatement(EvalStatement accumulationStatement) {
		accumulationStatements.add(accumulationStatement);
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public Rule getIterated() {
		return iterated;
	}

	public Collection<EvalStatement> getAccumulationStatements() {
		return accumulationStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement accumulationStatement : accumulationStatements)
			accumulationStatement.collectNeededEntities(needs);
		needs.variables.remove(iterationVar);
	}
}
