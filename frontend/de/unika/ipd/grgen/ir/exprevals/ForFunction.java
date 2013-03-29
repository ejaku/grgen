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
 * Represents a for lookup of a neighborhood function in the IR.
 */
public class ForFunction extends EvalStatement {

	private Variable iterationVar;
	private Expression function;
	private Collection<EvalStatement> loopedStatements = new LinkedList<EvalStatement>();

	public ForFunction(Variable iterationVar,
			Expression function) {
		super("for function");
		this.iterationVar = iterationVar;
		this.function = function;
	}

	public void addLoopedStatement(EvalStatement loopedStatement) {
		loopedStatements.add(loopedStatement);
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public Expression getFunction() {
		return function;
	}

	public Collection<EvalStatement> getLoopedStatements() {
		return loopedStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		function.collectNeededEntities(needs);
		for(EvalStatement loopedStatement : loopedStatements)
			loopedStatement.collectNeededEntities(needs);
		needs.variables.remove(iterationVar);
	}
}
