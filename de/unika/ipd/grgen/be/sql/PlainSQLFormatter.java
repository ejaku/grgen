/**
 * Created on Apr 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.util.Base;


/**
 * Makes type constraints using only SQL features. 
 */
public class PlainSQLFormatter extends Base implements SQLFormatter {

	private final SQLParameters parameters;
	
	private final TypeID typeID;
	
	private final boolean useBetween;
	
	public PlainSQLFormatter(SQLParameters parameters, TypeID typeID, boolean useBetween) {
		this.parameters = parameters;
		this.typeID = typeID;
		this.useBetween = useBetween;
	}
	
	private String makeCond(String col, int tid, boolean isRoot, boolean[][] matrix) { 
		debug.entering();
		
		StringBuffer sb = new StringBuffer();
		int compatTypesCount = 1;
		
		if(isRoot)
			return "TRUE";
		
		for(int i = 0; i < matrix.length; i++) 
			compatTypesCount += matrix[i][tid] ? 1 : 0;

		switch(compatTypesCount) {
		case 0:
			sb.append(col + " = " + tid);
			break;
		default:
			int[] compat = new int[compatTypesCount];
			compat[0] = tid; 
			for(int i = 0, index = 1; i < matrix.length; i++) {
				if(matrix[i][tid]) 
					compat[index++] = i;
			}

			int[] setMembers = new int[compatTypesCount];
			int setMembersCount = 0;
			boolean somethingAdded = false;
			
			StringBuffer deb = new StringBuffer();
			for(int i = 0; i < compat.length; i++) {
				deb.append((i > 0 ? "," : "") + compat[i]);
			}

			
			sb.append("(");
			for(int i = 0; i < compat.length;) {
				
				// Search as long as the numbers a incrementing by one. 
				int j;

				for(j = i + 1; j < compat.length && compat[j - 1] + 1 == compat[j] && useBetween; j++);

				// If there has been found a list use BETWEEN ... AND ... 
				if(i != j - 1) { 
					sb.append(somethingAdded ? " OR " : "");
					sb.append("(" + col + " BETWEEN " + compat[i] + " AND " + compat[j - 1] + ")");
					somethingAdded = true;
				} else
					setMembers[setMembersCount++] = compat[i];
					
				i = j;
			}

			switch(setMembersCount) {
			case 0:
				break;
			case 1:
				sb.append(somethingAdded ? " OR " : "");
				sb.append(col + " = " + setMembers[0]);
				break;
			default:
				sb.append(somethingAdded ? " OR " : "");
				sb.append(col + " IN (");
				for(int i = 0; i < setMembersCount; i++)
					sb.append((i > 0 ? "," : "") + setMembers[i]);
				sb.append(")");
			}
			sb.append(")");
			
			debug.report(NOTE, deb.toString());
			debug.report(NOTE, sb.toString());
		}
		
		debug.leaving();

		return sb.toString();
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLFormatter#makeNodeTypeIsA(de.unika.ipd.grgen.ir.Node, de.unika.ipd.grgen.be.sql.SQLMangler)
	 */
	public String makeNodeTypeIsA(Node n, SQLMangler mangler) {
		debug.entering();
		
		NodeType nt = n.getNodeType();
		int id = typeID.getId(nt);
		debug.report(NOTE, "" + nt + ", id: " + id);
		
		String res = makeCond(mangler.getNodeCol(n, parameters.getColNodesTypeId()), id, 
				nt.isRoot(), typeID.getNodeTypeIsAMatrix()); 
		
		debug.leaving();
		return res;
	}

	/**
	 * @see de.unika.ipd.grgen.be.sql.SQLFormatter#makeEdgeTypeIsA(de.unika.ipd.grgen.ir.Edge, de.unika.ipd.grgen.be.sql.SQLMangler)
	 */
	public String makeEdgeTypeIsA(Edge e, SQLMangler mangler) {
		EdgeType et = e.getEdgeType();
		int id = typeID.getId(et);
		return makeCond(mangler.getEdgeCol(e, parameters.getColNodesTypeId()), id, 
				et.isRoot(), typeID.getEdgeTypeIsAMatrix());
	}

}
