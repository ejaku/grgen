/**
 * Created on Apr 1, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import de.unika.ipd.grgen.be.sql.SQLFormatter;
import de.unika.ipd.grgen.be.sql.SQLMangler;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;


/**
 * SQL Formatting for the Java SQL backend.
 */
public class JavaSQLFormatter implements SQLFormatter {

	private final SQLParameters parameters;
	
	private final IDTypeModel typeModel;
	
	JavaSQLFormatter(SQLParameters parameters, IDTypeModel typeModel) {
		this.parameters = parameters;
		this.typeModel = typeModel;
	}
	
	
	private static String makeIsA(int[] ids, String col) {
		StringBuffer sb = new StringBuffer();
		
		if(ids.length > 0) {
			sb.append(col + " IN (");
			for(int i = 0; i < ids.length; i++) {
				sb.append((i > 0 ? "," : "") + ids[i]);
			}
			sb.append(")");
		}
		
		return sb.toString();
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLFormatter#makeEdgeTypeIsA(de.unika.ipd.grgen.ir.Edge)
	 */
	public String makeEdgeTypeIsA(Edge e, SQLMangler mangler) {
		int etId = typeModel.getId(e.getEdgeType());
		int[] ids = typeModel.getEdgeTypeIsA(etId);
		
		return makeIsA(ids, mangler.getEdgeCol(e, parameters.getColNodesTypeId()));
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLFormatter#makeNodeTypeIsA(de.unika.ipd.grgen.ir.Node)
	 */
	public String makeNodeTypeIsA(Node n, SQLMangler mangler) {
		int ntId = typeModel.getId(n.getNodeType());
		int[] ids = typeModel.getNodeTypeIsA(ntId);
		
		return makeIsA(ids, mangler.getNodeCol(n, parameters.getColNodesTypeId()));
	}
	
}
