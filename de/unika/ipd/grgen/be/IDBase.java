/**
 * Created on Mar 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import de.unika.ipd.grgen.ir.*;
import java.util.*;

import de.unika.ipd.grgen.be.sql.TypeID;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.Util;
import de.unika.ipd.grgen.util.Visitor;
import de.unika.ipd.grgen.util.Walkable;
import java.security.MessageDigest;


/**
 * Basic equipment for backends that treat node and edge types as IDs.
 */
public abstract class IDBase extends Base implements TypeID {
	
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
	
	private boolean[][] nodeTypeIsAMatrix;
	
	private boolean[][] edgeTypeIsAMatrix;
	
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
	
	public final boolean[][] getNodeTypeIsAMatrix() {
		return nodeTypeIsAMatrix;
	}
	
	public final boolean[][] getEdgeTypeIsAMatrix() {
		return edgeTypeIsAMatrix;
	}
	
	public static final boolean[][] computeIsA(Map typeMap) {
		int maxId = 0;
		
		for(Iterator it = typeMap.values().iterator(); it.hasNext();) {
			int id = ((Integer) it.next()).intValue();
			maxId = id > maxId ? id : maxId;
		}
		
		boolean[] helper = new boolean[maxId + 1];
		boolean[][] res = new boolean[maxId + 1][maxId + 1];
		
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			InheritanceType ty = (InheritanceType) it.next();
			computeIsAHelper(ty, typeMap, res, helper);
		}
		
		return res;
	}
	
	private static final void computeIsAHelper(InheritanceType ty, Map typeMap,
																						 boolean[][] res,	boolean[] alreadyDone) {
		
		int id = ((Integer) typeMap.get(ty)).intValue();
		
		if(!alreadyDone[id]) {
			
			for(Iterator it = ty.getSuperTypes(); it.hasNext();) {
				InheritanceType inh = (InheritanceType) it.next();
				int inhId = ((Integer) typeMap.get(inh)).intValue();
				computeIsAHelper(inh, typeMap, res, alreadyDone);
				res[id][inhId] = true;
				
				for(int i = 0; i < res.length; i++) {
					if(res[inhId][i])
						res[id][i] = true;
				}
			}
			
			alreadyDone[id] = true;
		}
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
	}
	
}
