/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import java.util.Iterator;


/**
 * A type.
 */
public interface InheritanceType extends Type {

	/**
	 * Check if this type is a root type.
	 * @return true if this type is a root type.
	 */
	boolean isRoot();
	
	/**
	 * Get all direct super types of this one.
	 * @return An iterator.
	 */
	Iterator<Object> getSuperTypes();
	
	/**
	 * Get all types that inherit from this one.
	 * @return An iterator.
	 */
	Iterator<Object> getSubTypes();
	
	/**
	 * Checks, if this type is also of type <code>t</type>
	 * @param t The type to check for.
	 * @return true, if this type is also of type <code>t</code>
	 */
	boolean isA(InheritanceType t);
	
	
}
