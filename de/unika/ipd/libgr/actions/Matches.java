/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.actions;




/**
 * A bunch of matches. 
 */
public interface Matches {

	/**
	 * Get the number of found matches.
	 * @return The number of found matches.
	 */
	int count();
	
	Match get(int whichOne);
	
}
