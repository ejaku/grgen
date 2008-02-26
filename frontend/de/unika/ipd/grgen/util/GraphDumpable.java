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


/*
 * @author Sebastian Hack
 * @date Jul 6, 2003
 */
package de.unika.ipd.grgen.util;

import java.awt.Color;

public interface GraphDumpable {

	/**
	 * Get the unique id of a node.
	 * Only one node with this id can be in the graph.
	 * @return A unique id of this node.
	 */
	String getNodeId();

	/**
	 * Get the color of this node.
	 * @return The color.
	 */
	Color getNodeColor();

	/**
	 * Get the shape of this node.
	 * @see GraphDumper
	 * @return The node's shape
	 */
	int getNodeShape();

	/**
	 * Get the label for a node.
	 * This should be the string the user sees in the output.
	 * @return The label of the node.
	 */
	String getNodeLabel();

	/**
	 * Get node info.
	 * @return Some additional information of the node.
	 */
	String getNodeInfo();

	/**
	 * Gets the label for an edge outgoing from this node.
	 * @param edge The number of the edge.
	 * @return The label for edge <code>edge</code>
	 */
	String getEdgeLabel(int edge);

}
