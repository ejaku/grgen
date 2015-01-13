/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * Represents an accumulation yielding of a container variable in the IR.
 */
public class IntegerRangeIterationYield extends EvalStatement {

	private Variable iterationVar;
	private Expression leftExpr;
	private Expression rightExpr;
	private Collection<EvalStatement> accumulationStatements = new LinkedList<EvalStatement>();

	public IntegerRangeIterationYield(Variable iterationVar, Expression left, Expression right) {
		super("integer range iteration yield");
		this.iterationVar = iterationVar;
		this.leftExpr = left;
		this.rightExpr = right;
	}

	public void addAccumulationStatement(EvalStatement accumulationStatement) {
		accumulationStatements.add(accumulationStatement);
	}

	public Variable getIterationVar() {
		return iterationVar;
	}

	public Expression getLeftExpr() {
		return leftExpr;
	}

	public Expression getRightExpr() {
		return rightExpr;
	}

	public Collection<EvalStatement> getAccumulationStatements() {
		return accumulationStatements;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		leftExpr.collectNeededEntities(needs);
		rightExpr.collectNeededEntities(needs);
		for(EvalStatement accumulationStatement : accumulationStatements)
			accumulationStatement.collectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.remove(iterationVar);
	}
}
