/**
 * Created on Apr 1, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.C;

import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.be.sql.SQLFormatter;
import de.unika.ipd.grgen.be.sql.SQLMangler;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;


public class CSQLFormatter implements SQLFormatter {

	private SQLParameters parameters;
	
	private IDBase id;

	final String nodeTypeIsAFunc;
	
	final String edgeTypeIsAFunc;
	
	CSQLFormatter(SQLParameters parameters, IDBase id, String nodeTypeIsAFunc,
			String edgeTypeIsAFunc) {
		this.nodeTypeIsAFunc = nodeTypeIsAFunc;
		this.edgeTypeIsAFunc = edgeTypeIsAFunc;
		
		this.parameters = parameters;
		this.id = id;
	}
	
	public String makeNodeTypeIsA(Node n, SQLMangler mangler) {
		//return nodeTypeIsAFunc + "(" + mangler.getNodeCol(n, parameters.getColNodesId()) 
		// 	+ "," + id.getId(n.getNodeType()) + ")";
		return "xx";
	}
	
	public String makeEdgeTypeIsA(Edge e, SQLMangler mangler) {
		// return edgeTypeIsAFunc + "(" + mangler.getEdgeCol(e, parameters.getColEdgesId())
		//	+ "," + id.getId(e.getEdgeType()) + ")";
		return "TRUE";
	}
	
}
