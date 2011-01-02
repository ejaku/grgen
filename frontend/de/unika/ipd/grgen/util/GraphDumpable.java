/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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
