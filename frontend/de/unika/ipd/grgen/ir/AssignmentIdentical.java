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
