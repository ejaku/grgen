/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;


/**
 * An edge.
 */
public interface Edge {

	/**
	 * Get the source of the edge.
	 * @return The edge's source node.
	 */
	Node getSource();
	
	/**
	 * Get the target of the edge.
	 * @return The edge's target node.
	 */
	Node getTarget();
	
	/**
	 * Get the edge's type.
	 * @return The edge type.
	 */
	EdgeType getType();
	
}
