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
import de.unika.ipd.grgen.be.sql.IDBase;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.report.ErrorReporter;
import de.unika.ipd.libgr.graph.id.IDTypeModel;


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
	
	/** Node root type id. */
	int nodeRootType;
	
	/** Edge root type id. */
	int edgeRootType;
	
	public IDTypeModel getTypeModel() {
		return this;
	}
	
	/**
	 * @see de.unika.ipd.grgen.be.Backend#init(de.unika.ipd.grgen.ir.Unit, de.unika.ipd.grgen.util.report.ErrorReporter, java.lang.String)
	 */
	public void init(Unit unit, ErrorReporter reporter, String outputPath) {
		this.unit = unit;
		
		makeTypes();
		
		nodeTypeIsA = computeIsA(nodeTypeMap);
		edgeTypeIsA = computeIsA(edgeTypeMap);
		
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
	}
	
	
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.IDBase#getUnit()
	 */
	protected Unit getUnit() {
		return unit;
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#edgeTypeIsA(int, int)
	 */
	public boolean edgeTypeIsA(int e1, int e2) {
		return edgeTypeIsA[e1][e2];
	}
	
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getEdgeRootType()
	 */
	public int getEdgeRootType() {
		return nodeRootType;
	}

	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getEdgeTypeSuperTypes(int)
	 */
	public int[] getEdgeTypeSuperTypes(int edge) {
		return edgeSuperTypes[edge];
	}
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getNodeRootType()
	 */
	public int getNodeRootType() {
		return edgeRootType;
	}
	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#getNodeTypeSuperTypes(int)
	 */
	public int[] getNodeTypeSuperTypes(int node) {
		return nodeSuperTypes[node];
	}

	/**
	 * @see de.unika.ipd.libgr.graph.id.IDTypeModel#nodeTypeIsA(int, int)
	 */
	public boolean nodeTypeIsA(int n1, int n2) {
		return nodeTypeIsA[n1][n2];
	}
}
