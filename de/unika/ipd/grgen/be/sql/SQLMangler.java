/**
 * Created on Apr 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;


/**
 * Someone who mangles node and edges and produces column names.
 */
public interface SQLMangler {

	/**
	 * Make an SQL table identifier out of a node.
	 * @param e The node to mangle.
	 * @return An identifier usable in SQL statements and unique for each node.
	 */
	public String mangleNode(Node n);

	/**
	 * Make an SQL table identifier out of an edge.
	 * @param e The edge to mangle.
	 * @return An identifier usable in SQL statements and unique for each edge.
	 */
	public String mangleEdge(Edge e);

	/**
	 * Make a SQL column expression for a node and a given column name.
	 * @param e The node.
	 * @param col The column.
	 * @return The column expression.
	 */
	public String getNodeCol(Node n, String col);

	/**
	 * Mangle an identifiable object to a valid SQL identifier.
	 * @param id The identifiable object. 
	 * @return A valid SQL identifier.
	 */
	public String getEdgeCol(Edge e, String col);
	
}
