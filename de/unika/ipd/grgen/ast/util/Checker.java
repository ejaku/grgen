/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.util.report.ErrorReporter;

/**
 * Interface for something, that can check a node
 */
public interface Checker {
	
	/**
	 * Check a node
	 * @param node The ast node to check
	 * @return true if the check succeeded, false if not.
	 */
	public abstract boolean check(BaseNode node, ErrorReporter reporter);
}
