/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * Interface for something, that can check an AST node
 */
public interface Checker
{
	/**
	 * Check some AST node
	 * @param node The AST node to check
	 * @return true if the check succeeded, false if not.
	 */
	boolean check(BaseNode node, ErrorReporter reporter);
}
