/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
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
 * Represents a for lookup of a graph element (type) in the IR.
 */
public class ForLookup extends EvalStatement {

	private Variable iterationVar;
	private Collection<EvalStatement> loopedStatements = new LinkedList<EvalStatement>();

	public ForLookup(Variable iterationVar) {
		super("for lookup");
		this.iterationVar = iterationVar;
	}

	public void addLoopedStatement(EvalStatement accumulationStatement) {
		loopedStatements.add(accumulationStatement);
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public Collection<EvalStatement> getLoopedStatements() {
		return loopedStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		for(EvalStatement loopedStatement : loopedStatements)
			loopedStatement.collectNeededEntities(needs);
		needs.variables.remove(iterationVar);
	}
}
