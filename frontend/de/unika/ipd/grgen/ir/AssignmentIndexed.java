/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir;


/**
 * Represents an indexed assignment statement in the IR.
 */
public class AssignmentIndexed extends Assignment {

	/** The index to the lhs. */
	private Expression index;

	public AssignmentIndexed(Qualification target, Expression expr, Expression index) {
		super("assignment indexed", target, expr);
		this.index = index;
	}

	public Expression getIndex() {
		return index;
	}

	public String toString() {
		return getTarget() + "[" + getIndex() + "] = " + getExpression();
	}
	
	public void collectNeededEntities(NeededEntities needs)
	{
		super.collectNeededEntities(needs);
		getIndex().collectNeededEntities(needs);
	}
}
