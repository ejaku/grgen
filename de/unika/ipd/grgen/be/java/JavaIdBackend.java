/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;


import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

import de.unika.ipd.grgen.be.Backend;
import de.unika.ipd.grgen.be.IDBase;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.report.ErrorReporter;


/**
 * A Java backend with ID support.
 */
public abstract class JavaIdBackend extends IDBase implements Backend, IDTypeModel {

	/** The IR unit. */
	private Unit unit;

  /** node type to type id map. (Type -> Integer) */
	protected Map nodeTypeMap = new HashMap();
	
	/** node type to type id map. (Type -> Integer) */
	protected Map edgeTypeMap = new HashMap();
	
	/** node attribute map. (Entity -> Integer) */
	protected Map nodeAttrMap = new HashMap();

	/** node attribute map. (Entity -> Integer) */
	protected Map edgeAttrMap = new HashMap();

	/** enum value map. (Enum -> Integer) */
	protected Map enumMap = new HashMap();

	/** action map. (Action -> Integer) */
	protected Map actionMap = new HashMap();
	
	/** Transitive closure over edge type "is a" relation. */
	protected boolean[][] edgeTypeIsA;
	
	/** Transitive closure over node type "is a" relation. */
	protected boolean[][] nodeTypeIsA;
	
	/** Matrix A_ij = true iff i inherits b. */
	protected boolean[][] nodeInherits;

	/** Matrix A_ij = true iff i inherits b. */
	protected boolean[][] edgeInherits;

	/**
	 * Gives the amount of types a node type inherits.
	 * This is only used for optimization purposes. The methods
	 * {@link #getEdgeTypeSuperTypes(int)} and the like can use this value to save
	 * a little time.
	 */
	protected int[] nodeInheritsCount;
	
	/**
	 * See comment of {@link #nodeInheritsCount}.
	 */
	protected int[] edgeInheritsCount;
	
	/**
	 * Map an ID to a node type class.
	 */
	protected NodeType[] nodeTypes;
	
	/**
	 * Map an ID to an edge type class.
	 */
	protected EdgeType[] edgeTypes;
	
	/** Node root type id. */
	int nodeRootType;
	
	/** Edge root type id. */
	int edgeRootType;
	
	/** Count of node types in the type model. */
	int nodeTypeCount;
	
	/** Count of edge types in the type model. */
	int edgeTypeCount;
	
	/**
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter, java.lang.String)
	 */
	public void init(Unit unit, ErrorReporter reporter, String outputPath) {
		this.unit = unit;
		
		makeTypes(unit);
		
		nodeTypeIsA = computeIsA(nodeTypeMap);
		edgeTypeIsA = computeIsA(edgeTypeMap);
		
		nodeTypeCount = nodeTypeIsA.length;
		edgeTypeCount = edgeTypeIsA.length;


		nodeInherits = new boolean[nodeTypeCount][nodeTypeCount];
		edgeInherits = new boolean[edgeTypeCount][edgeTypeCount];
		
		nodeInheritsCount = new int[nodeTypeCount];
		edgeInheritsCount = new int[edgeTypeCount];

		Map[] typeMaps = new Map[] { nodeTypeMap, edgeTypeMap };
		int[] rootTypes = new int[2];
		boolean[][][] inherits = new boolean[][][] { nodeInherits, edgeInherits };
		int[][] inheritsCount = new int[][] { nodeInheritsCount, edgeInheritsCount };
		
		for(int i = 0; i < typeMaps.length; i++) {
			Map typeMap = typeMaps[i];
			
			for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
				InheritanceType inh = (InheritanceType) it.next();
				int id = ((Integer) typeMap.get(inh)).intValue();
				int count = 0;

				for(Iterator jt = inh.getSuperTypes(); jt.hasNext(); count++) {
					InheritanceType ty = (InheritanceType) jt.next();
					int tyId = ((Integer) typeMap.get(inh)).intValue();
					inherits[id][tyId][i] = true;
				}

				if(count == 0)
					rootTypes[i] = id;
				
				inheritsCount[id][i] = count;
			}
		}
		
		nodeRootType = rootTypes[0];
		edgeRootType = rootTypes[1];
		
		nodeTypes = new NodeType[nodeTypeCount];
		edgeTypes = new EdgeType[edgeTypeCount];
		
		for(Iterator it = nodeTypeMap.keySet().iterator(); it.hasNext();) {
			NodeType nt = (NodeType) it.next();
			int id = ((Integer) nodeTypeMap.get(nt)).intValue();
			nodeTypes[id] = nt;
		}
		
		for(Iterator it = edgeTypeMap.keySet().iterator(); it.hasNext();) {
			EdgeType et = (EdgeType) it.next();
			int id = ((Integer) edgeTypeMap.get(et)).intValue();
			edgeTypes[id] = et;
		}
		
	}
	
	
	
	/**
	 * @see de.unika.ipd.grgen.be.IDBase#getUnit()
	 */
	protected Unit getUnit() {
		return unit;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#edgeTypeIsA(int, int)
	 */
	public boolean edgeTypeIsA(int e1, int e2) {
		return edgeTypeIsA[e1][e2];
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getEdgeRootType()
	 */
	public int getEdgeRootType() {
		return nodeRootType;
	}

	private int[] getFollowTypes(int[] count, boolean[][] matrix, int id, boolean superTypes) {
		int[] res = new int[count[id]];
		int j = 0;
		
		for(int i = 0; i < matrix.length; i++) {
			
			assert j <= res.length;
			boolean entry = superTypes ? matrix[id][i] : matrix[i][id];
			
			if(entry)
				res[j++] = i;
		}
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getEdgeTypeSuperTypes(int)
	 */
	public int[] getEdgeTypeSuperTypes(int edge) {
		return getFollowTypes(edgeInheritsCount, edgeInherits, edge, true);
	}
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getNodeRootType()
	 */
	public int getNodeRootType() {
		return edgeRootType;
	}
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getNodeTypeSuperTypes(int)
	 */
	public int[] getNodeTypeSuperTypes(int node) {
		return getFollowTypes(nodeInheritsCount, nodeInherits, node, true);
	}

	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getEdgeTypeSubTypes(int)
	 */
	public int[] getEdgeTypeSubTypes(int edge) {
		return getFollowTypes(edgeInheritsCount, edgeInherits, edge, false);
	}

	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getNodeTypeSubTypes(int)
	 */
	public int[] getNodeTypeSubTypes(int node) {
		return getFollowTypes(nodeInheritsCount, nodeInherits, node, false);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#nodeTypeIsA(int, int)
	 */
	public boolean nodeTypeIsA(int n1, int n2) {
		return nodeTypeIsA[n1][n2];
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getEdgeTypeName(int)
	 */
	public String getEdgeTypeName(int edge) {
		return edgeTypes[edge].getIdent().toString();
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getNodeTypeName(int)
	 */
	public String getNodeTypeName(int node) {
		return nodeTypes[node].getIdent().toString();
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getEdgeTypes()
	 */
	public int[] getEdgeTypes() {
		int[] res = new int[edgeTypeCount];
		for(int i = 0; i < res.length; i++)
			res[i] = i;
		
		return res;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getNodeTypes()
	 */
	public int[] getNodeTypes() {
		int[] res = new int[nodeTypeCount];
		for(int i = 0; i < res.length; i++)
			res[i] = i;
		
		return res;
	}
	
	private static final int[] getIsA(boolean[] matrixRow) {
		int[] result;
		int entries = 0;
		
		// count the entries first.
		for(int i = 0; i < matrixRow.length; i++)
			entries = matrixRow[i] ? entries + 1 : entries;

		result = new int[entries];
		for(int i = 0, j = 0; i < matrixRow.length; i++) {
			if(matrixRow[i])
				result[j++] = i;
		}
		
		return result;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getEdgeTypeIsA(int)
	 */
	public int[] getEdgeTypeIsA(int et) {
		return getIsA(edgeTypeIsA[et]);
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getNodeTypeIsA(int)
	 */
	public int[] getNodeTypeIsA(int nt) {
		return getIsA(nodeTypeIsA[nt]);
	}
}
