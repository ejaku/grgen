/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Created on Mar 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.be;

import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.CompoundType;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Unit;
import de.unika.ipd.grgen.util.Base;


/**
 * Basic equipment for backends that treat node and edge types as IDs.
 */
public abstract class IDBase extends Base implements IDTypeModel {
	/** node type to type id map. (Type -> Integer) */
	public final Map<NodeType, Integer> nodeTypeMap = new LinkedHashMap<NodeType, Integer>();

	/** node type to type id map. (Type -> Integer) */
	public final Map<EdgeType, Integer> edgeTypeMap = new LinkedHashMap<EdgeType, Integer>();

	/** node attribute map. (Entity -> Integer) */
	public final Map<Entity, Integer> nodeAttrMap = new LinkedHashMap<Entity, Integer>();

	/** node attribute map. (Entity -> Integer) */
	public final Map<Entity, Integer> edgeAttrMap = new LinkedHashMap<Entity, Integer>();

	/** enum value map. (Enum -> Integer) */
	public final Map<EnumType, Integer> enumMap = new LinkedHashMap<EnumType, Integer>();

	/** action map. (Action -> Integer) */
	public final Map<Rule, Integer> actionRuleMap = new LinkedHashMap<Rule, Integer>();

	/** pattern map. (Subpattern action -> Integer) */
	public final Map<Rule, Integer> subpatternRuleMap = new LinkedHashMap<Rule, Integer>();

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
		for(Entity ent : ct.getMembers()) {
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

		for(Type type : unit.getActionsGraphModel().getTypes()) {
			if(type instanceof NodeType) {
				nodeTypeMap.put((NodeType)type, new Integer(nodeTypeMap.size()));
			} else if(type instanceof EdgeType) {
				edgeTypeMap.put((EdgeType)type, new Integer(edgeTypeMap.size()));
			} else if(type instanceof EnumType) {
				enumMap.put((EnumType)type, new Integer(enumMap.size()));
			}

			if(type instanceof CompoundType) {
				CompoundType ct = (CompoundType) type;
				addMembers(ct);
			}
		}
	}

	public static final short[][] computeIsA(Map<? extends InheritanceType, Integer> typeMap) {
		int maxId = 0;

		for(Iterator<Integer> it = typeMap.values().iterator(); it.hasNext();) {
			int id = it.next().intValue();
			maxId = id > maxId ? id : maxId;
		}

		short[][] res = new short[maxId + 1][maxId + 1];

		for(InheritanceType ty : typeMap.keySet()) {
			int typeId = typeMap.get(ty).intValue();
			res[typeId][typeId] = 1;

			for(InheritanceType st : ty.getDirectSuperTypes()) {
				int inhId = typeMap.get(st).intValue();
				res[typeId][inhId] = 1;
			}
		}

		res = floydWarshall(res);
		for(int i = 0; i < res.length; i++)
			res[i][i] = 0;

		return res;
	}

	private static short[][] floydWarshall(short[][] matrix) {
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

	private static int[][] computeSuperTypes(Map<? extends InheritanceType, Integer> typeMap) {
		int[][] res = new int[typeMap.size()][];
		List<Integer> aux = new LinkedList<Integer>();

		for(InheritanceType ty :typeMap.keySet()) {
			aux.clear();
			int id = typeMap.get(ty).intValue();

			for(InheritanceType t: ty.getDirectSuperTypes())
				aux.add(typeMap.get(t));

			res[id] = new int[aux.size()];
			int i = 0;
			for(Iterator<Integer> jt = aux.iterator(); jt.hasNext(); i++)
				res[id][i] = jt.next().intValue();
		}

		return res;
	}

	private static int[][] computeSubTypes(Map<? extends InheritanceType, Integer> typeMap) {
		int[][] res = new int[typeMap.size()][];
		List<Integer> aux = new LinkedList<Integer>();

		for(InheritanceType ty :typeMap.keySet()) {
			aux.clear();
			int id = typeMap.get(ty).intValue();

			for(InheritanceType t : ty.getDirectSubTypes())
				aux.add(typeMap.get(t));

			res[id] = new int[aux.size()];
			int i = 0;
			for(Iterator<Integer> jt = aux.iterator(); jt.hasNext(); i++)
				res[id][i] = jt.next().intValue();
		}

		return res;
	}

	private static String[] makeNames(Map<? extends InheritanceType, Integer> typeMap) {
		String[] res = new String[typeMap.size()];
		for(InheritanceType ty : typeMap.keySet()) {
			int id = typeMap.get(ty).intValue();
			res[id] = ty.getIdent().toString();
		}

		return res;
	}

	/**
	 * Make subpattern IDs.
	 * @param subpatternRuleMap The map to put the IDs to.
	 */
	private void makeSubpatternIds(Unit unit) {
		int id = 0;
		for(Iterator<Rule> it = unit.getSubpatternRules().iterator(); it.hasNext();) {
			Rule rule = it.next();
			subpatternRuleMap.put(rule, new Integer(id));
			++id;
		}
	}

	/**
	 * Make action IDs.
	 * @param actionRuleMap The map to put the IDs to.
	 */
	private void makeActionIds(Unit unit) {
		int id = 0;
		for(Iterator<Rule> it = unit.getActionRules().iterator(); it.hasNext();) {
			Rule rule = it.next();
			actionRuleMap.put(rule, new Integer(id));
			++id;
		}
	}

	/**
	 * Get the ID of an IR type.
	 * @param map The map to look into.
	 * @param ty The inheritance type to get the id for.
	 * @return The type id for this type.
	 */
	protected final int getTypeId(Map<? extends Identifiable, Integer> map, IR obj) {
		Integer res = map.get(obj);
		return res.intValue();
	}

	public final int getId(EdgeType et) {
		return getTypeId(edgeTypeMap, et);
	}

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
		Map<? extends Identifiable, Integer> map = forNode ?
			(Map<? extends Identifiable, Integer>)nodeTypeMap :
			(Map<? extends Identifiable, Integer>)edgeTypeMap;
		int[] res = new int[map.size()];

		int i = 0;
		for(Iterator<Integer> it = map.values().iterator(); it.hasNext();)
			res[i++] = it.next().intValue();

		return res;
	}

	/**
	 * Compute all IDs.
	 * @param unit The IR unit for ID computation.
	 */
	protected final void makeTypes(Unit unit) {
		makeTypeIds(unit);
		makeSubpatternIds(unit);
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
