/**
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.actions;

import java.util.Map;



/**
 * A match. 
 */
public interface Match {
	
	Map getNodes();
	
	Map getEdges();
	
	/**
	 * Is this match valid?
	 * @return true, if the match is valid, false if it does not mean a thing.
	 */
	boolean isValid();

}
