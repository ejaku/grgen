/**
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import java.util.prefs.Preferences;


/**
 * Parameters for the database.
 */
public class PreferencesSQLParameters implements SQLParameters {

	/** if 0, the query should not be limited. */
	protected final int limitQueryResults;
	
	/** The name of the nodes table. */
	protected final String tableNodes;
	
	/** The name of the edge table. */
	protected final String tableEdges;
	
	/** The name of the node attributes table. */
	protected final String tableNodeAttrs;
	
	/** The name of the edge attributes table. */
	protected final String tableEdgeAttrs;
	
	/** The name of the node ID column. */
	protected final String colNodesId;
	
	/** The name of the node type ID column. */	
	protected final String colNodesTypeId;
	
	/** The name of the edge ID column. */
	protected final String colEdgesId;
	
	/** The name of the edge type ID column. */
	protected final String colEdgesTypeId;
	
	/** The name of the source node column. */
	protected final String colEdgesSrcId;
	
	/** The name of the target node column. */	
	protected final String colEdgesTgtId;
	
	protected final String colNodeAttrNodeId;
	
	protected final String colEdgeAttrEdgeId;
	
	protected final String idType;

	public PreferencesSQLParameters() {
		Preferences prefs = Preferences.userNodeForPackage(getClass());
		
		tableNodes = prefs.get("tableNodes", "nodes");
		tableEdges = prefs.get("tableEdges", "edges");
		tableNodeAttrs = prefs.get("tableNodeAttrs", "node_attrs");
		tableEdgeAttrs = prefs.get("tableEdgeAttrs", "edge_attrs");
		colNodesId = prefs.get("colNodesId", "node_id");
		colNodesTypeId = prefs.get("colNodesTypeId", "type_id");
		colEdgesId = prefs.get("colEdgesId", "edge_id");
		colEdgesTypeId = prefs.get("colEdgesTypeId", "type_id");
		colEdgesSrcId = prefs.get("colEdgesSrcId", "src_id");
		colEdgesTgtId = prefs.get("colEdgesTgtId", "tgt_id");
		colNodeAttrNodeId = prefs.get("colNodeAttrNodeId", "node_id");
		colEdgeAttrEdgeId = prefs.get("colEdgeAttrEdgeId", "edge_id");
		
		idType = prefs.get("idType", "INT");
		
		limitQueryResults = prefs.getInt("limitQueryResults", 0);
	}

	/**
	 * @return Returns the colEdgeAttrEdgeId.
	 */
	public String getColEdgeAttrEdgeId() {
		return colEdgeAttrEdgeId;
	}
	/**
	 * @return Returns the colEdgesId.
	 */
	public String getColEdgesId() {
		return colEdgesId;
	}
	/**
	 * @return Returns the colEdgesSrcId.
	 */
	public String getColEdgesSrcId() {
		return colEdgesSrcId;
	}
	/**
	 * @return Returns the colEdgesTgtId.
	 */
	public String getColEdgesTgtId() {
		return colEdgesTgtId;
	}
	/**
	 * @return Returns the colEdgesTypeId.
	 */
	public String getColEdgesTypeId() {
		return colEdgesTypeId;
	}
	/**
	 * @return Returns the colNodeAttrNodeId.
	 */
	public String getColNodeAttrNodeId() {
		return colNodeAttrNodeId;
	}
	/**
	 * @return Returns the colNodesId.
	 */
	public String getColNodesId() {
		return colNodesId;
	}
	/**
	 * @return Returns the colNodesTypeId.
	 */
	public String getColNodesTypeId() {
		return colNodesTypeId;
	}
	/**
	 * @return Returns the limitQueryResults.
	 */
	public int getLimitQueryResults() {
		return limitQueryResults;
	}
	/**
	 * @return Returns the tableEdgeAttrs.
	 */
	public String getTableEdgeAttrs() {
		return tableEdgeAttrs;
	}
	/**
	 * @return Returns the tableEdges.
	 */
	public String getTableEdges() {
		return tableEdges;
	}
	/**
	 * @return Returns the tableNodeAttrs.
	 */
	public String getTableNodeAttrs() {
		return tableNodeAttrs;
	}
	/**
	 * @return Returns the tableNodes.
	 */
	public String getTableNodes() {
		return tableNodes;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLParameters#getIdType()
	 */
	public String getIdType() {
		return idType;
	}
}
