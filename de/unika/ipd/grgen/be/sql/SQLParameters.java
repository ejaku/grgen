/**
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;


/**
 * Parameters for the database.
 * Some functions return the names of the tables and columns used in the database
 * to model the graph.
 *
 * Other functions return types and other parameters to use while constructing SQL
 * queries.
 */
public interface SQLParameters {

	/** The name of the table with one column and row. */
	String getTableNeutral();
	
	/** The name of the nodes table. */
	String getTableNodes();
	
	/** The name of the edge table. */
	String getTableEdges();
	
	/** The name of the node attributes table. */
	String getTableNodeAttrs();
	
	/** The name of the edge attributes table. */
	String getTableEdgeAttrs();
	
	/** The name of the node ID column. */
	String getColNodesId();
	
	/** The name of the node type ID column. */
	String getColNodesTypeId();
	
	/** The name of the edge ID column. */
	String getColEdgesId();
	
	/** The name of the edge type ID column. */
	String getColEdgesTypeId();
	
	/** The name of the source node column. */
	String getColEdgesSrcId();
	
	/** The name of the target node column. */
	String getColEdgesTgtId();
	
	String getColNodeAttrNodeId();
	
	String getColEdgeAttrEdgeId();
	
	String getIdType();

}
