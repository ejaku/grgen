/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import de.unika.ipd.grgen.util.GraphDumper;
import de.unika.ipd.libgr.Named;
import java.util.Collection;

/**
 * A graph.
 */
public interface Graph extends Named {
	
	/**
	 * Add a node to the graph.
	 * @param t The desired type of the node.
	 * @return A new node.
	 */
	Node add(NodeType t);
	
	/**
	 * Add an edge to a graph.
	 * @param t The desired edge type.
	 * @param src The source node.
	 * @param tgt The target node.
	 * @return A new edge.
	 */
	Edge add(EdgeType t, Node src, Node tgt);
	
	/**
	 * Remove a node from the graph.
	 * Note that incident edges to the node are also deleted.
	 * @param node The node to remove.
	 * @return true, if the node was in the graph, false if not.
	 */
	boolean remove(Node node);

	/**
	 * Remove an edge from the graph.
	 * @param edge The edge to remove.
	 * @return true, if the edge was in the graph, false if not.
	 */
	boolean remove(Edge edge);
	
	Collection<Integer> putAllNodesInstaceOf(NodeType type, Collection<Integer> coll);
	
	/**
	 * Dump the graph.
	 * @param dumper A graph dumper.
	 */
	void dump(GraphDumper dumper);
	
	/**
	 * Get the type model of the graph.
	 * @return The type model.
	 */
	TypeModel getTypeModel();
	
}
