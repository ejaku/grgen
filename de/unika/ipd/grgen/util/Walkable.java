/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.util.Iterator;

/**
 * Something you can walk on. This means, that there are children to visit. 
 */
public interface Walkable {
	
	/**
	 * Get the children of this object
	 * All Objects in the iterator must also implement Walkable! 
	 * @return The children
	 */
	public Iterator getWalkableChildren();
}
