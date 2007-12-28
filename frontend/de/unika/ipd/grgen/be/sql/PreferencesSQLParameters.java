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
 * Created on Mar 11, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import java.util.prefs.Preferences;
import java.util.prefs.BackingStoreException;


/**
 * Parameters for the database.
 */
public class PreferencesSQLParameters implements SQLParameters {

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
	
	protected final String tableNeutral;
	
	protected final String tableNodeTypeRel;
	
	protected final String tableEdgeTypeRel;

	protected final String colNodeTypeRelId;
	
	protected final String colNodeTypeRelIsAId;

	protected final String colEdgeTypeRelId;

	protected final String colEdgeTypeRelIsAId;

	private static String getOrSet(Preferences prefs, String key,
																			 String defaultValue) {
		String res = prefs.get(key, defaultValue);
		prefs.put(key, res);
		return res;
	}
	
	public PreferencesSQLParameters() {
		Preferences prefs = Preferences.userNodeForPackage(getClass());
		
		tableNeutral = getOrSet(prefs, "tableNeutral", "neutral");
		tableNodes = getOrSet(prefs, "tableNodes", "nodes");
		tableEdges = getOrSet(prefs, "tableEdges", "edges");
		tableNodeAttrs = getOrSet(prefs, "tableNodeAttrs", "node_attrs");
		tableEdgeAttrs = getOrSet(prefs, "tableEdgeAttrs", "edge_attrs");
		colNodesId = getOrSet(prefs, "colNodesId", "node_id");
		colNodesTypeId = getOrSet(prefs, "colNodesTypeId", "type_id");
		colEdgesId = getOrSet(prefs, "colEdgesId", "edge_id");
		colEdgesTypeId = getOrSet(prefs, "colEdgesTypeId", "type_id");
		colEdgesSrcId = getOrSet(prefs, "colEdgesSrcId", "src_id");
		colEdgesTgtId = getOrSet(prefs, "colEdgesTgtId", "tgt_id");
		colNodeAttrNodeId = getOrSet(prefs, "colNodeAttrNodeId", "node_id");
		colEdgeAttrEdgeId = getOrSet(prefs, "colEdgeAttrEdgeId", "edge_id");
		idType = getOrSet(prefs, "idType", "INT");
		tableNodeTypeRel = getOrSet(prefs, "tableNodeTypeRel", "node_type_rel");
		tableEdgeTypeRel = getOrSet(prefs, "tableEdgeTypeRel", "edge_type_rel");
		colNodeTypeRelId = getOrSet(prefs, "colNodeTypeRelId", "rel_type_id");
		colNodeTypeRelIsAId = getOrSet(prefs, "colNodeTypeIsAId", "rel_isa_id");
		colEdgeTypeRelId = getOrSet(prefs, "colEdgeTypeIsAId", "rel_type_id");
		colEdgeTypeRelIsAId = getOrSet(prefs, "colEdgeTypeIsAId", "rel_isa_id");
		
		try {
			prefs.sync();
		} catch (BackingStoreException e) {
			e.printStackTrace(System.err);
		}
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
	
	/** The name of the table with one column and row. */
	public String getTableNeutral() {
		return tableNeutral;
	}
	
		/**
	 * Get the SQL type to use for ids.
	 * @return The SQL type for ids.
	 */
	public String getIdType() {
		return idType;
	}
	
	public String getColTypeRelId(boolean forNode) {
		return forNode ? colNodeTypeRelId : colEdgeTypeRelId;
	}
	
	public String getColTypeRelIsAId(boolean forNode) {
		return forNode ? colNodeTypeRelIsAId : colEdgeTypeRelIsAId;
	}
	
	/** Get the name of the node type relation. */
	public String getTableTypeRel(boolean forNode) {
		return forNode ? tableNodeTypeRel : tableEdgeTypeRel;
	}
	
	


}
