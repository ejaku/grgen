/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ast.BaseNode;
import java.util.Collection;

/**
 * Something you can walk on. This means, that there are children to visit.
 */
public interface Walkable {
	
	/**
	 * Get the children of this object
	 * All Objects in the iterator must also implement Walkable!
	 * @return The children
	 */
	public Collection<? extends BaseNode> getWalkableChildren();
}
