/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/*
 * @author Sebastian Hack
 */

namespace de.unika.ipd.grgen.util
{

public interface GraphDumpable
{
	/// <summary>
	/// Get the unique id of a node.
	/// Only one node with this id can be in the graph. </summary>
	/// <returns> A unique id of this node. </returns>
	string NodeId {get;}

	/// <summary>
	/// Get the color of this node. </summary>
	/// <returns> The color. </returns>
	Color NodeColor {get;}

	/// <summary>
	/// Get the shape of this node. </summary>
	/// <seealso cref="GraphDumper"/>
	/// <returns> The node's shape </returns>
	int NodeShape {get;}

	/// <summary>
	/// Get the label for a node.
	/// This should be the string the user sees in the output. </summary>
	/// <returns> The label of the node. </returns>
	string NodeLabel {get;}

	/// <summary>
	/// Get node info. </summary>
	/// <returns> Some additional information of the node. </returns>
	string NodeInfo {get;}

	/// <summary>
	/// Gets the label for an edge outgoing from this node. </summary>
	/// <param name="edge"> The number of the edge. </param>
	/// <returns> The label for edge <code>edge</code> </returns>
	string GetEdgeLabel(int edge);
}

}
