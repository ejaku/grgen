/**
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.java;

import java.util.Collection;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
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
	
	/** Node supertypes for each node type id. */
	protected int[][] nodeSuperTypes;
	
	/** Edge supertypes for each edge type id. */
	protected int[][] edgeSuperTypes;
	
	protected NodeType[] nodeTypes;
	
	protected EdgeType[] edgeTypes;
	
	/** Node root type id. */
	int nodeRootType;
	
	/** Edge root type id. */
	int edgeRootType;
	
	int nodeTypeCount;
	
	int edgeTypeCount;
	
	/**
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter, java.lang.String)
	 */
	public void init(Unit unit, ErrorReporter reporter, String outputPath) {
		this.unit = unit;
		
		makeTypes();
		
		nodeTypeIsA = computeIsA(nodeTypeMap);
		edgeTypeIsA = computeIsA(edgeTypeMap);
		
		nodeTypeCount = nodeTypeIsA.length;
		edgeTypeCount = edgeTypeIsA.length;
		
		/*
		 * At last build the super type arrays for node and edge types. 
		 */
		nodeSuperTypes = new int[nodeTypeIsA.length][];
		edgeSuperTypes = new int[edgeTypeIsA.length][];
		Collection aux = new LinkedList();

		int[][][] superTypes = new int[][][] { nodeSuperTypes, edgeSuperTypes };
		Map[] typeMaps = new Map[] { nodeTypeMap, edgeTypeMap };
		int[] rootTypes = new int[2];
		
		for(int i = 0; i < typeMaps.length; i++) {
			int[][] superType = superTypes[i];
			Map typeMap = typeMaps[i];
			
			for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
				InheritanceType inh = (InheritanceType) it.next();
				int id = ((Integer) typeMap.get(inh)).intValue();
				aux.clear();
				
				for(Iterator jt = inh.getInherits(); jt.hasNext();) {
					Object obj = jt.next();
					aux.add(typeMap.get(obj));
				}
				
				// Root type found, store it in the root types array.
				if(aux.size() == 0) 
					rootTypes[i] = id;
				
				superType[id] = new int[aux.size()];
				int j = 0;
				for(Iterator jt = aux.iterator(); jt.hasNext(); j++)
					nodeSuperTypes[id][j] = ((Integer) jt.next()).intValue();
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

	/**
	 * @see de.unika.ipd.grgen.be.java.IDTypeModel#getEdgeTypeSuperTypes(int)
	 */
	public int[] getEdgeTypeSuperTypes(int edge) {
		return edgeSuperTypes[edge];
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
		return nodeSuperTypes[node];
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
