/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.ast.BaseNode;

/**
 * something, that resolves a node to another node.
 */
public interface Resolver {

	/**
	 * Resolve a node.
	 * @param node The parent node of the node to resolve.
	 * @param child The index of the node to resolve in <code>node</code>'s
	 * children.
	 * @return true, if the resolving was successful, false, if not.
	 */
	public boolean resolve(BaseNode node, int child);

}
