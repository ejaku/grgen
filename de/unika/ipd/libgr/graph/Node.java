/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import java.util.Iterator;


/**
 * A node in a graph.
 */
public interface Node {

	/**
	 * Get the incoming edges.
	 * @return An iterator iterating over all incoming edges.
	 */
	Iterator getIncoming();	

	/**
	 * Get the outgoing edges.
	 * @return An iterator iterating over all outgoing edges.
	 */
	Iterator getOutgoing();	

	/**
	 * Get the node's type
	 * @return the node type of this node.
	 */
	NodeType getType();
	
	
	
}
