/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.actions;

import java.util.Iterator;


/**
 * A bunch of actions. 
 */
public interface Actions {

	/**
	 * Get the action with a certain name.
	 * @param name The name of the action.
	 * @return The corresponding Action object or <code>null</code> if 
	 * nothing is known about an action named <code>name</code>.
	 */
	Action getAction(String name);

	/**
	 * Get an iterator iterating over all known actions.
	 * @return The iterator.
	 */
	Iterator getActions();
	
}
