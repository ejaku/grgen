/**
 * Created on Mar 9, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.actions;

import de.unika.ipd.libgr.graph.Edge;
import de.unika.ipd.libgr.graph.Node;



/**
 * A bunch of matches. 
 */
public interface Matches {

	/**
	 * Get the number of found matches.
	 * @return The number of found matches.
	 */
	int count();
	
	Node[] getNodes(int whichOne);
	Edge[] getEdges(int whichOne);
	
}
