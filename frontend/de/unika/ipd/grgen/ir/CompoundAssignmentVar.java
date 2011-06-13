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
 * Represents a compound assignment var statement in the IR.
 */
public class CompoundAssignmentVar extends EvalStatement {

	public static final int NONE = -1;
	public static final int UNION = 0;
	public static final int INTERSECTION = 2;
	public static final int WITHOUT = 3;
	public static final int ASSIGN = 4;

	/** The lhs of the assignment. */
	private Variable target;

	/** The operation of the compound assignment */
	private int operation;

	/** The rhs of the assignment. */
	private Expression expr;

	public CompoundAssignmentVar(Variable target, int compoundAssignmentType, Expression expr) {
		super("compound assignment var");
		this.target = target;
		this.operation = compoundAssignmentType;
		this.expr = expr;
	}

	public Variable getTarget() {
		return target;
	}

	public Expression getExpression() {
		return expr;
	}

	public int getOperation() {
		return operation;
	}

	public String toString() {
		return getTarget() + (operation==UNION?" |= ":operation==INTERSECTION?" &= ":" \\= ") + getExpression();
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(target);

		getExpression().collectNeededEntities(needs);
	}
}
