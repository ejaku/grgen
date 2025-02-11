/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
	 * @param bn The AST node to check
	 * @return true if the check succeeded, false if not.
	 */
	boolean check(BaseNode bn, ErrorReporter reporter);
}
