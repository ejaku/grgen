/**
 * Created on Mar 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ir.Action;
import de.unika.ipd.grgen.ir.CompoundType;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.Base;
import de.unika.ipd.grgen.util.PostWalker;
import de.unika.ipd.grgen.util.Visitor;
import de.unika.ipd.grgen.util.Walkable;


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

	/** Get the IR root node. */
	protected abstract Unit getUnit(); 
	
	/**
	 * Assign an id to each type in the IR graph.
	 * This method puts all IR object in the IR graph that are instance of
	 * <code>cl</code> into the map <code>typeMap</code> and associates
	 * it with an id that is unique within the map. The id starts with 0.
	 * @param typeMap The type map to fill. 
	 * @param cl The class that an IR object must be instance of, to be put 
	 * into the type map. 
	 */
	private void makeTypeIds(Map typeMap, Class cl) {
		final Class typeClass = cl;
		final Map map = typeMap;
		 
		/*
		 * This visitor enters each type into a hashmap with 
		 * an id as value, and the type object as key.
		 */
		
		Visitor v = new Visitor() {
			private int id = 0;
						  		 
			public void visit(Walkable w) {
				if(typeClass.isInstance(w)) 
					map.put(w, new Integer(id++));
			}
		};
  	
		(new PostWalker(v)).walk(getUnit());
	}
	
	/**
	 * Make the attribute Ids.
	 * @param attrMap	A map that will be filled with all attributes and there Ids.
	 * @param cl			The attribute class.
	 */
	private void makeAttrIds(Map attrMap, Class cl) {
		final Class attrClass = cl;
		final Map map = attrMap;
		
		Visitor v = new Visitor() {
			private int id = 0;
			
			public void visit(Walkable w) {
				if(attrClass.isInstance(w)) {
					CompoundType ty = (CompoundType) w;
					for(Iterator it = ty.getMembers(); it.hasNext();) {
						Entity ent = (Entity) it.next();
						assert !map.containsKey(ent) : "entity must not be in map";
						map.put(ent, new Integer(id++));
					}
				}
			}
		};
		
		(new PostWalker(v)).walk(getUnit());
	}
	
	/**
	 * Make all enum type Ids.
	 * @param enumMap A map that will be filled with all Enum types and there Ids.
	 */
	private void makeEnumIds(Map enumMap) {
		final Map map = enumMap;
		
		Visitor v = new Visitor() {
			private int id = 0;
			
			public void visit(Walkable w) {
				if(w instanceof EnumType) {
					map.put(w, new Integer(id++));
				}
			}
		};
		
		(new PostWalker(v)).walk(getUnit());
	}

	/**
	 * Gets a set with all types the given type is compatible with.
	 * (It builds the transitive closure over the subtype relation.)
	 * @param ty The type to determine all compatible types for.
	 * @param isaMap A temporary map, where this method can record data.
	 * @return A set containing all compatible types to <code>ty</code>.
	 */
	protected final Set getIsA(InheritanceType ty, Map isaMap) {
		Set res; 
		
		if(!isaMap.containsKey(ty)) {
			res = new HashSet();
			isaMap.put(ty, res);
			for(Iterator it = ty.getInherits(); it.hasNext();) {
				InheritanceType t = (InheritanceType) it.next();
				res.add(t);
				res.addAll(getIsA(t, isaMap));				
			}
		} else
			res = (Set) isaMap.get(ty);
			
		return res;
	}
	
	protected final boolean[][] computeIsA(Map typeMap) {
		int maxId = 0;
		
		for(Iterator it = typeMap.values().iterator(); it.hasNext();) {
			int id = ((Integer) it.next()).intValue();
			maxId = id > maxId ? id : maxId;
		}
		
		boolean[] helper = new boolean[maxId];
		boolean[][] res = new boolean[maxId][maxId];
		
		for(Iterator it = typeMap.keySet().iterator(); it.hasNext();) {
			InheritanceType ty = (InheritanceType) it.next();
			computeIsAHelper(ty, typeMap, res, helper);
		}
		
		return res;
	}
	
	private final void computeIsAHelper(InheritanceType ty, Map typeMap, boolean[][] res,
			boolean[] alreadyDone) {
		
		int id = ((Integer) typeMap.get(ty)).intValue();
		
		if(!alreadyDone[id]) {

			for(Iterator it = ty.getInherits(); it.hasNext();) {
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
	private void makeActionIds(Map actionMap) {
		final Map map = actionMap;
		
		Visitor v = new Visitor() {
			private int id = 0;
			
			public void visit(Walkable w) {
				if(w instanceof Action) 
					map.put(w, new Integer(id++));					
			}
		};
		
		(new PostWalker(v)).walk(getUnit());
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
	 * Compute all type IDs.
	 */
	protected final void makeTypes() {
		makeTypeIds(nodeTypeMap,  NodeType.class);
		makeTypeIds(edgeTypeMap,  EdgeType.class);
		makeAttrIds(nodeAttrMap,  NodeType.class);
		makeAttrIds(edgeAttrMap,  EdgeType.class);
		makeEnumIds(enumMap);
		makeActionIds(actionMap);
	}

}
