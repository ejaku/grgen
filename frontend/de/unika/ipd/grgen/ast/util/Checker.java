/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
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
