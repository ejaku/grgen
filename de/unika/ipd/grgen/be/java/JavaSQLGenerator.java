/**
 * Created on Mar 12, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import de.unika.ipd.grgen.be.sql.SQLGenerator;
import de.unika.ipd.grgen.be.sql.SQLParameters;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;


/**
 * 
 */
class JavaSQLGenerator extends SQLGenerator {

	private final SQLParameters parameters;
	
	private final IDTypeModel typeModel;
	
	JavaSQLGenerator(SQLParameters parameters, IDTypeModel typeModel) {
		super(parameters);
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
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#makeNodeTypeIsA(de.unika.ipd.grgen.ir.Node)
	 */
	protected String makeNodeTypeIsA(Node n) {
		int ntId = typeModel.getId(n.getNodeType());
		int[] ids = typeModel.getNodeTypeIsA(ntId);
		
		return makeIsA(ids, getNodeCol(n, parameters.getColNodesTypeId()));
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLGenerator#makeEdgeTypeIsA(de.unika.ipd.grgen.ir.Edge)
	 */
	protected String makeEdgeTypeIsA(Edge e) {
		int etId = typeModel.getId(e.getEdgeType());
		int[] ids = typeModel.getEdgeTypeIsA(etId);
		
		return makeIsA(ids, getEdgeCol(e, parameters.getColNodesTypeId()));
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.TypeID#getId(de.unika.ipd.grgen.ir.EdgeType)
	 */
	public int getId(EdgeType et) {
		return typeModel.getId(et);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.TypeID#getId(de.unika.ipd.grgen.ir.NodeType)
	 */
	public int getId(NodeType nt) {
		return typeModel.getId(nt);
	}
}
