/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

/**
 * A visitor that computes a result.
 */
public interface ResultVisitor<RT> extends Visitor
{
	/**
	 * Get the result, the visitor computed.
	 * @return The result
	 */
	RT getResult();
}
