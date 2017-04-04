/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.exprevals;

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;


/**
 * Represents a compound assignment statement in the IR.
 */
public class CompoundAssignment extends EvalStatement {

	public static final int NONE = -1;
	public static final int UNION = 0;
	public static final int INTERSECTION = 2;
	public static final int WITHOUT = 3;
	public static final int CONCATENATE = 4;
	public static final int ASSIGN = 5;

	/** The lhs of the assignment. */
	private Qualification target;

	/** The operation of the compound assignment */
	private int operation;

	/** The rhs of the assignment. */
	private Expression expr;

	public CompoundAssignment(Qualification target, int compoundAssignmentType, Expression expr) {
		super("compound assignment");
		this.target = target;
		this.operation = compoundAssignmentType;
		this.expr = expr;
	}

	public Qualification getTarget() {
		return target;
	}

	public Expression getExpression() {
		return expr;
	}

	public int getOperation() {
		return operation;
	}

	public String toString() {
		String res = getTarget().toString();
		if(operation==UNION) res += " |= ";
		else if(operation==INTERSECTION) res += " &= ";
		else if(operation==WITHOUT) res += " \\= ";
		else if(operation==CONCATENATE) res += " += ";
		else res += " = ";
		res += getExpression().toString();
		return res;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity))
			needs.add((GraphEntity) entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getExpression().collectNeededEntities(needs);
	}
}
