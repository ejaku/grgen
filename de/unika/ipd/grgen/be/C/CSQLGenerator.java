/**
 * Created on Mar 10, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import java.util.prefs.Preferences;

import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.TypeID;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;


/**
 * SQL code generator atapted for the C backend.
 */
public class CSQLGenerator extends SQLGenerator {

	public final String nodeTypeIsAFunc;

	public final String edgeTypeIsAFunc;
	
	private final TypeID typeId;
	
	CSQLGenerator(TypeID typeId) {
		this.typeId = typeId;
		Preferences prefs = Preferences.userNodeForPackage(getClass());
		
		nodeTypeIsAFunc = prefs.get("nodeTypeIsAFunc", "node_type_is_a");
		edgeTypeIsAFunc = prefs.get("edgeTypeIsAFunc", "edge_type_is_a");

	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#getId(de.unika.ipd.grgen.ir.NodeType)
	 */
	public int getId(NodeType nt) {
		return typeId.getId(nt);
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#getId(de.unika.ipd.grgen.ir.EdgeType)
	 */
	public int getId(EdgeType et) {
		return typeId.getId(et);
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#makeNodeTypeIsA(de.unika.ipd.grgen.ir.Node)
	 */
	protected String makeNodeTypeIsA(Node n) {
		return nodeTypeIsAFunc + "(" + getNodeCol(n, colNodesId) + "," + getId(n.getNodeType())
			+ ")";
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#makeEdgeTypeIsA(de.unika.ipd.grgen.ir.Edge)
	 */
	protected String makeEdgeTypeIsA(Edge e) {
		return edgeTypeIsAFunc + "(" + getEdgeCol(e, colEdgesId) + "," + getId(e.getEdgeType())
		+ ")";
	}

}
