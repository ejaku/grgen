/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

/**
 * An interface for something that has an id unique in 
 * space and life time of the program.
 */
public interface Id {
	
	/**
	 * Get the id.
	 * An implementation must ensure, that for all objects that are instance of Id
	 * the two strings (returned be the <code>getId()</code> methods
	 * respectively) differ.
	 * @return A new id.
	 */
	public abstract String getId();
}
