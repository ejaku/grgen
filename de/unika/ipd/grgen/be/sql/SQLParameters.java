/**
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;


/**
 * 
 */
public interface SQLParameters {

	/** if 0, the query should not be limited. */
	int getLimitQueryResults();
	
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

}
