/**
 * Created on Apr 1, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import java.util.prefs.Preferences;

import de.unika.ipd.grgen.be.sql.SQLFormatter;
import de.unika.ipd.grgen.be.sql.SQLMangler;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.be.sql.TypeID;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;


public class CSQLFormatter implements SQLFormatter {

	private SQLParameters parameters;
	
	private TypeID typeID;

	final String nodeTypeIsAFunc;
	
	final String edgeTypeIsAFunc;
	
	CSQLFormatter(SQLParameters parameters, TypeID typeID) {
		Preferences prefs = Preferences.userNodeForPackage(getClass());
		
		nodeTypeIsAFunc = prefs.get("nodeTypeIsAFunc", "node_type_is_a");
		edgeTypeIsAFunc = prefs.get("edgeTypeIsAFunc", "edge_type_is_a");
		
		
		this.parameters = parameters;
		this.typeID = typeID;
	}
	
	public String makeNodeTypeIsA(Node n, SQLMangler mangler) {
		return nodeTypeIsAFunc + "(" + mangler.getNodeCol(n, parameters.getColNodesId()) 
		+ "," + typeID.getId(n.getNodeType()) + ")";
	}
	
	public String makeEdgeTypeIsA(Edge e, SQLMangler mangler) {
		return edgeTypeIsAFunc + "(" + mangler.getEdgeCol(e, parameters.getColEdgesId())
		+ "," + typeID.getId(e.getEdgeType()) + ")";
	}
	
}
