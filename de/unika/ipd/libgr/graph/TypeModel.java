/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import java.util.Iterator;


/**
 * A type model for graphs.
 */
public interface TypeModel {
	
	/**
	 * Get the root type of the nodes.
	 * @return The node type root.
	 */
	NodeType getNodeRootType();
	
	/**
	 * Get the root type of the edges.
	 * @return The edge type root.
	 */
	EdgeType getEdgeRootType();
	
	/**
	 * Get all node types.
	 * @return An iterator iterating over all node types.
	 */
	Iterator<NodeType> getNodeTypes();

	/**
	 * Get all edge types.
	 * @return An iterator iterating over all edge types.
	 */
	Iterator<EdgeType> getEdgeTypes();

}
