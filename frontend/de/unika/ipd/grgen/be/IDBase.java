/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */


/**
 * Created on Mar 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.be;

import java.util.*;
import de.unika.ipd.grgen.ir.*;
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
	public final Map<Action, Integer> actionMap = new LinkedHashMap<Action, Integer>();

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

		for(Model model : unit.getModels()) {
			for(Type type : model.getTypes()) {
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

	private static final int[][] computeSuperTypes(Map<? extends InheritanceType, Integer> typeMap) {
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

	private static final int[][] computeSubTypes(Map<? extends InheritanceType, Integer> typeMap) {
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

	private static final String[] makeNames(Map<? extends InheritanceType, Integer> typeMap) {
		String[] res = new String[typeMap.size()];
		for(InheritanceType ty : typeMap.keySet()) {
			int id = typeMap.get(ty).intValue();
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
		for(Iterator<Action> it = unit.getActions().iterator(); it.hasNext();) {
			Action act = it.next();
			actionMap.put(act, new Integer(id++));
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
