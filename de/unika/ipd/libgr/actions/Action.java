/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.actions;

import de.unika.ipd.libgr.Named;
import de.unika.ipd.libgr.graph.Graph;


/**
 * A graph action.
 */
public interface Action extends Named {

	/**
	 * Apply this action to a graph.
	 * @param graph The graph to apply this action to.
	 * @return The found matches.
	 */
	Matches apply(Graph graph);
	
	/**
	 * Finish this action.
	 * This method needs to be called to free all resources associated with it.
	 * @param match A match contained in the matches obtained by {@link #apply(Graph)}.
	 */
	void finish(Match m);

}
