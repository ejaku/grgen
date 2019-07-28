/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

/**
 * Represents a relict from an assignment statement of the form x=x optimized away.
 */
public class AssignmentIdentical extends EvalStatement {
	public AssignmentIdentical() {
		super("assignment identical");
	}

	public String toString() {
		return ". = .";
	}

	public void collectNeededEntities(NeededEntities needs)
	{
	}
}
