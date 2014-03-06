/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

/**
 * Represents a continue statement in the IR.
 */
public class ContinueStatement extends EvalStatement {

	public ContinueStatement() {
		super("continue statement");
	}

	public void collectNeededEntities(NeededEntities needs)
	{
	}
}
