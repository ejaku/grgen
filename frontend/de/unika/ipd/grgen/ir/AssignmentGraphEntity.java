/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ast.BaseNode;


/**
 * Represents an assignment statement in the IR.
 */
public class AssignmentGraphEntity extends EvalStatement {

	/** The lhs of the assignment. */
	private GraphEntity target;

	/** The rhs of the assignment. */
	private Expression expr;

	public AssignmentGraphEntity(GraphEntity target, Expression expr) {
		super("assignment graph entity");
		this.target = target;
		this.expr = expr;
	}

	public GraphEntity getTarget() {
		return target;
	}

	public Expression getExpression() {
		return expr;
	}

	public String toString() {
		return getTarget() + " = " + getExpression();
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target) && (target.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(target);

		getExpression().collectNeededEntities(needs);
	}
}
