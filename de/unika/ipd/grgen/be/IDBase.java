/**
 * Created on Mar 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.Action;
import de.unika.ipd.grgen.ir.CompoundType;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.Base;


/**
 * Basic equipment for backends that treat node and edge types as IDs.
 */
public abstract class IDBase extends Base implements IDTypeModel {
	
  /** node type to type id map. (Type -> Integer) */
	protected final Map nodeTypeMap = new HashMap();
	
	/** node type to type id map. (Type -> Integer) */
	protected final Map edgeTypeMap = new HashMap();
	
	/** node attribute map. (Entity -> Integer) */
	protected final Map nodeAttrMap = new HashMap();
	
	/** node attribute map. (Entity -> Integer) */
	protected final Map edgeAttrMap = new HashMap();
	
	/** enum value map. (Enum -> Integer) */
	protected final Map enumMap = new HashMap();
	
	/** action map. (Action -> Integer) */
	protected final Map actionMap = new HashMap();
	
	private short[][] nodeTypeIsAMatrix;
	
	private short[][] edgeTypeIsAMatrix;
	
	private int[][] nodeTypeSuperTypes;
	
	private int[][] edgeTypeSuperTypes;
	
	private int[][] nodeTypeSubTypes;
	
	private int[][] edgeTypeSubTypes;
	
	private String[] nodeTypeNames;
	
	private String[] edgeTypeNames;
	
	private int edgeRoot;
	
	private int nodeRoot;
	
	private void addMembers(CompoundType ct) {
		for(Iterator it = ct.getMembers(); it.hasNext();) {
			Entity ent = (Entity) it.next();
			
			if(ct instanceof NodeType)
				nodeAttrMap.put(ent, new Integer(nodeAttrMap.size()));
			else if(ct instanceof EdgeType)
				edgeAttrMap.put(ent, new Integer(edgeAttrMap.size()));
			else
				assert false : "Wrong type";
		}
	}
	
	private void makeTypeIds(Unit unit) {
		unit.canonicalize();
		
		for(Iterator mt = unit.getModels(); mt.hasNext();) {
			Model model = (Model) mt.next();
			
			for(Iterator it = model.getTypes(); it.hasNext();) {
				Type type = (Type) it.next();
				
				if(type instanceof NodeType) {
					nodeTypeMap.put(type, new Integer(nodeTypeMap.size()));
				} else if(type instanceof EdgeType) {
					edgeTypeMap.put(type, new Integer(edgeTypeMap.size()));
				} else if(type instanceof EnumType) {
					enumMap.put(type, new Integer(enumMap.size()));
				}
				
				if(type instanceof CompoundType) {
					CompoundType ct = (CompoundType) type;
					addMembers(ct);
				}
			}
		}
	}
	
	public static final short[][] computeIsA(Map typeMap) {
		int maxId = 0;
		
		for(Iterator it = typeMap.values().iterator(); it.hasNext();) {
			int id = ((Integer) it.next()).intValue();
			maxId = id > maxId ? id : maxId;
		}
		
		boolean[] helper = new boolean[maxId + 1];
		short[][] res = new short[maxId + 1][maxId + 1];
		
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			InheritanceType ty = (InheritanceType) it.next();
			int typeId = ((Integer) typeMap.get(ty)).intValue();
			res[typeId][typeId] = 1;
			
			for(Iterator tt = ty.getSuperTypes(); tt.hasNext();) {
				InheritanceType st = (InheritanceType) tt.next();
				int inhId = ((Integer) typeMap.get(st)).intValue();
				res[typeId][inhId] = 1;
			}
		}
		
		res = floydWarshall(res);
		for(int i = 0; i < res.length; i++)
			res[i][i] = 0;
		
		return res;
	}
	
	private static final short[][] floydWarshall(short[][] matrix) {
		int n = matrix.length;
		short[][] curr = matrix;
		short[][] next = new short[n][n];
		
		for(int k = 0; k < n; k++) {
			short[][] tmp;
			
			for(int i = 0; i < n; i++)
				for(int j = 0; j < n; j++) {
					int v1 = curr[i][k];
					int v2 = curr[k][j];
					int res = v1 == 0 || v2 == 0 ? Short.MAX_VALUE : v1 + v2;
					int v = curr[i][j];
					
					v = v == 0 ? Short.MAX_VALUE : v;
					v = v < res ? v : res;
					
					next[i][j] = (short) (v == Short.MAX_VALUE ? 0 : v);
				}
			
			tmp = curr;
			curr = next;
			next = tmp;
		}
		
		return next;
	}
	
	private static final int[][] computeSuperTypes(Map typeMap) {
		int[][] res = new int[typeMap.size()][];
		List aux = new LinkedList();
		
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			aux.clear();
			InheritanceType ty = (InheritanceType) it.next();
			int id = ((Integer) typeMap.get(ty)).intValue();
			
			for(Iterator jt = ty.getSuperTypes(); jt.hasNext();)
				aux.add(typeMap.get(jt.next()));
			
			res[id] = new int[aux.size()];
			int i = 0;
			for(Iterator jt = aux.iterator(); jt.hasNext(); i++)
				res[id][i] = ((Integer) jt.next()).intValue();
		}
		
		return res;
	}
	
	private static final int[][] computeSubTypes(Map typeMap) {
		int[][] res = new int[typeMap.size()][];
		List aux = new LinkedList();
		
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			aux.clear();
			InheritanceType ty = (InheritanceType) it.next();
			int id = ((Integer) typeMap.get(ty)).intValue();
			
			for(Iterator jt = ty.getSubTypes(); jt.hasNext();)
				aux.add(typeMap.get(jt.next()));
			
			res[id] = new int[aux.size()];
			int i = 0;
			for(Iterator jt = aux.iterator(); jt.hasNext(); i++)
				res[id][i] = ((Integer) jt.next()).intValue();
		}
		
		return res;
	}
	
	private static final String[] makeNames(Map typeMap) {
		String[] res = new String[typeMap.size()];
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			InheritanceType ty = (InheritanceType) it.next();
			int id = ((Integer) typeMap.get(ty)).intValue();
			res[id] = ty.getIdent().toString();
		}
		
		return res;
	}
	
  /**
	 * Make action IDs.
	 * @param actionMap The map to put the IDs to.
	 */
	private void makeActionIds(Unit unit) {
		int id = 0;
		for(Iterator it = unit.getActions(); it.hasNext();) {
			Action act = (Action) it.next();
			actionMap.put(act, new Integer(id++));
		}
	}
	
  /**
	 * Get the ID of an IR type.
	 * @param map The map to look into.
	 * @param ty The inheritance type to get the id for.
	 * @return The type id for this type.
	 */
  protected final int getTypeId(Map map, IR obj) {
		Integer res = (Integer) map.get(obj);
		return res.intValue();
  }
	
	/**
	 * @see de.unika.ipd.grgen.be.sql.TypeID#getId(de.unika.ipd.grgen.ir.EdgeType)
	 */
	public final int getId(EdgeType et) {
		return getTypeId(edgeTypeMap, et);
	}
	/**
	 * @see de.unika.ipd.grgen.be.sql.TypeID#getId(de.unika.ipd.grgen.ir.NodeType)
	 */
	public final int getId(NodeType nt) {
		return getTypeId(nodeTypeMap, nt);
	}
	
	public final int getId(Type t, boolean forNode) {
		return forNode ? getTypeId(nodeTypeMap, t) : getTypeId(edgeTypeMap, t);
	}
	
	public final short[][] getIsAMatrix(boolean forNode) {
		return forNode ? nodeTypeIsAMatrix : edgeTypeIsAMatrix;
	}
	
	public final String getTypeName(boolean forNode, int obj) {
		return forNode ? nodeTypeNames[obj] : edgeTypeNames[obj];
	}
	
	public final int[] getSuperTypes(boolean forNode, int obj) {
		return forNode ? nodeTypeSuperTypes[obj] : edgeTypeSuperTypes[obj];
	}
	
	public final int[] getSubTypes(boolean forNode, int obj) {
		return forNode ? nodeTypeSubTypes[obj] : edgeTypeSubTypes[obj];
	}
	
	public final int getRootType(boolean forNode) {
		return forNode ? nodeRoot : edgeRoot;
	}
	
	public final int[] getIDs(boolean forNode) {
		Map map = forNode ? nodeTypeMap : edgeTypeMap;
		int[] res = new int[map.size()];
		
		int i = 0;
		for(Iterator it = map.values().iterator(); it.hasNext();)
			res[i++] = ((Integer) it.next()).intValue();
		
		return res;
	}
	
	
	/**
	 * Compute all IDs.
	 * @param unit The IR unit for ID computation.
	 * @return A digest for the type model.
	 */
	protected final void makeTypes(Unit unit) {
		makeTypeIds(unit);
		makeActionIds(unit);
		
		nodeTypeIsAMatrix = computeIsA(nodeTypeMap);
		edgeTypeIsAMatrix = computeIsA(edgeTypeMap);
		nodeTypeSuperTypes = computeSuperTypes(nodeTypeMap);
		edgeTypeSuperTypes = computeSuperTypes(edgeTypeMap);
		nodeTypeSubTypes = computeSubTypes(nodeTypeMap);
		edgeTypeSubTypes = computeSubTypes(edgeTypeMap);
		nodeTypeNames = makeNames(nodeTypeMap);
		edgeTypeNames = makeNames(edgeTypeMap);
	}
	
}
