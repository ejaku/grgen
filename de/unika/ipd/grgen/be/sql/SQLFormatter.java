/**
 * Created on Mar 31, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;


/**
 * Something, that produces SQL code stating that a node or edge is of a
 * certain type. 
 */
public interface SQLFormatter {
	/**
	 * Generate SQL statement that expresses that a given node is of its type.
	 * @param n The node.
	 * @return SQL code.
	 */
	String makeNodeTypeIsA(Node n, SQLMangler mangler);
	
	/**
	 * Generate SQL statement that expresses that a given edge is of its type.
	 * @param e The edge.
	 * @return SQL code.
	 */
	String makeEdgeTypeIsA(Edge e, SQLMangler mangler);

}
