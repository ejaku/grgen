/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt;

/**
 * Represents a relict from an assignment statement of the form x=x optimized away.
 */
public class AssignmentIdentical extends EvalStatement
{
	public AssignmentIdentical()
	{
		super("assignment identical");
	}

	@Override
	public String toString()
	{
		return ". = .";
	}
}
