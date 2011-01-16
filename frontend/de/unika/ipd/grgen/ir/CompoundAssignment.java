/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.HashSet;


/**
 * Represents a compound assignment statement in the IR.
 */
public class CompoundAssignment extends EvalStatement {

	public static final int NONE = -1;
	public static final int UNION = 0;
	public static final int INTERSECTION = 2;
	public static final int WITHOUT = 3;
	public static final int ASSIGN = 4;

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
		return getTarget() + (operation==UNION?" |= ":operation==INTERSECTION?" &= ":" =\\ ") + getExpression();
	}
	
	public void collectNeededEntities(NeededEntities needs)
	{
		Entity entity = target.getOwner();
		needs.add((GraphEntity) entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getExpression().collectNeededEntities(needs);
	}
}
