/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import de.unika.ipd.libgr.Named;


/**
 * An attribute.
 */
public interface Attribute extends Named {

	/**
	 * Get the type of the attribute.
	 * @return The type of the attribute.
	 */
	PrimitiveType getType();
	
	/**
	 * Get the value if the attribute.
	 * @return The value of the attribute.
	 */
	Object getValue();
	
}
