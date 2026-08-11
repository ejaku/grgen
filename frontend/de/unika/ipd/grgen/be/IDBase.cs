/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.be
{

	using System.Collections.Generic;
	using System.Diagnostics;

	using Entity = de.unika.ipd.grgen.ir.Entity;
	using Unit = de.unika.ipd.grgen.ir.Unit;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
	using CompoundType = de.unika.ipd.grgen.ir.type.CompoundType;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using Base = de.unika.ipd.grgen.util.Base;

	/// <summary>
	/// Basic equipment for backends that treat node and edge types as IDs.
	/// </summary>
	public abstract class IDBase : Base, IDTypeModel
	{
		/// <summary>
		/// node type to type id map. (Type -> Integer) </summary>
		public readonly IDictionary<NodeType, int> nodeTypeMap = new LinkedHashMap<NodeType, int>();

		/// <summary>
		/// edge type to type id map. (Type -> Integer) </summary>
		public readonly IDictionary<EdgeType, int> edgeTypeMap = new LinkedHashMap<EdgeType, int>();

		/// <summary>
		/// node attribute map. (Entity -> Integer) </summary>
		public readonly IDictionary<Entity, int> nodeAttrMap = new LinkedHashMap<Entity, int>();

		/// <summary>
		/// edge attribute map. (Entity -> Integer) </summary>
		public readonly IDictionary<Entity, int> edgeAttrMap = new LinkedHashMap<Entity, int>();

		/// <summary>
		/// enum value map. (Enum -> Integer) </summary>
		public readonly IDictionary<EnumType, int> enumMap = new LinkedHashMap<EnumType, int>();

		/// <summary>
		/// action map. (Action -> Integer) </summary>
		public readonly IDictionary<Rule, int> actionRuleMap = new LinkedHashMap<Rule, int>();

		/// <summary>
		/// pattern map. (Subpattern action -> Integer) </summary>
		public readonly IDictionary<Rule, int> subpatternRuleMap = new LinkedHashMap<Rule, int>();

		private short[][] nodeTypeIsAMatrix;

		private short[][] edgeTypeIsAMatrix;

		private int[][] nodeTypeSuperTypes;

		private int[][] edgeTypeSuperTypes;

		private int[][] nodeTypeSubTypes;

		private int[][] edgeTypeSubTypes;

		private string[] nodeTypeNames;

		private string[] edgeTypeNames;

		private int edgeRoot;

		private int nodeRoot;

		private void AddMembers(CompoundType ct)
		{
			foreach(Entity ent in ct.Members)
			{
				if(ct is NodeType)
					nodeAttrMap[ent] = new int?(nodeAttrMap.Count);
				else if(ct is EdgeType)
					edgeAttrMap[ent] = new int?(edgeAttrMap.Count);
				else
					Debug.Assert(false, "Wrong type");
			}
		}

		private void MakeTypeIds(Unit unit)
		{
			unit.Canonicalize();

			foreach(Type type in unit.ActionsGraphModel.Types)
			{
				if(type is NodeType)
					nodeTypeMap[(NodeType)type] = new int?(nodeTypeMap.Count);
				else if(type is EdgeType)
					edgeTypeMap[(EdgeType)type] = new int?(edgeTypeMap.Count);
				else if(type is EnumType)
					enumMap[(EnumType)type] = new int?(enumMap.Count);

				if(type is CompoundType)
				{
					CompoundType ct = (CompoundType)type;
					AddMembers(ct);
				}
			}
		}

		public static short[][] ComputeIsA(IDictionary<InheritanceType, int> typeMap)
		{
			int maxId = 0;

			foreach(int? id in typeMap.Values)
				maxId = id.Value > maxId ? id.Value : maxId;

			short[][] res = RectangularArrays.RectangularShortArray(maxId + 1, maxId + 1);

			foreach(InheritanceType ty in typeMap.Keys)
			{
				int typeId = typeMap[ty];
				res[typeId][typeId] = 1;

				foreach(InheritanceType st in ty.DirectSuperTypes)
				{
					int inhId = typeMap[st];
					res[typeId][inhId] = 1;
				}
			}

			res = FloydWarshall(res);
			for(int i = 0; i < res.Length; i++)
				res[i][i] = 0;

			return res;
		}

		private static short[][] FloydWarshall(short[][] matrix)
		{
			int n = matrix.Length;
			short[][] curr = matrix;
			short[][] next = RectangularArrays.RectangularShortArray(n, n);

			for(int k = 0; k < n; k++)
			{
				short[][] tmp;

				for(int i = 0; i < n; i++)
				{
					for(int j = 0; j < n; j++)
					{
						int v1 = curr[i][k];
						int v2 = curr[k][j];
						int res = v1 == 0 || v2 == 0 ? short.MaxValue : v1 + v2;
						int v = curr[i][j];

						v = v == 0 ? short.MaxValue : v;
						v = v < res ? v : res;

						next[i][j] = (short)(v == short.MaxValue ? 0 : v);
					}
				}

				tmp = curr;
				curr = next;
				next = tmp;
			}

			return next;
		}

		private static int[][] ComputeSuperTypes(IDictionary<InheritanceType, int> typeMap)
		{
			int[][] res = new int[typeMap.Count][];
			IList<int> aux = new List<int>();

			foreach(InheritanceType ty in typeMap.Keys)
			{
				aux.Clear();
				int id = typeMap[ty];

				foreach(InheritanceType t in ty.DirectSuperTypes)
					aux.Add(typeMap[t]);

				res[id] = new int[aux.Count];
				int i = 0;
				foreach(int? j in aux)
				{
					res[id][i] = j.Value;
					++i;
				}
			}

			return res;
		}

		private static int[][] ComputeSubTypes(IDictionary<InheritanceType, int> typeMap)
		{
			int[][] res = new int[typeMap.Count][];
			IList<int> aux = new List<int>();

			foreach(InheritanceType ty in typeMap.Keys)
			{
				aux.Clear();
				int id = typeMap[ty];

				foreach(InheritanceType t in ty.DirectSubTypes)
					aux.Add(typeMap[t]);

				res[id] = new int[aux.Count];
				int i = 0;
				foreach(int? j in aux)
				{
					res[id][i] = j.Value;
					++i;
				}
			}

			return res;
		}

		private static string[] MakeNames(IDictionary<InheritanceType, int> typeMap)
		{
			string[] res = new string[typeMap.Count];
			foreach(InheritanceType ty in typeMap.Keys)
			{
				int id = typeMap[ty];
				res[id] = ty.Ident.ToString();
			}

			return res;
		}

		/// <summary>
		/// Make subpattern IDs. </summary>
		/// <param name="subpatternRuleMap"> The map to put the IDs to. </param>
		private void MakeSubpatternIds(Unit unit)
		{
			int id = 0;
			foreach(Rule rule in unit.SubpatternRules)
			{
				subpatternRuleMap[rule] = new int?(id);
				++id;
			}
		}

		/// <summary>
		/// Make action IDs. </summary>
		/// <param name="actionRuleMap"> The map to put the IDs to. </param>
		private void MakeActionIds(Unit unit)
		{
			int id = 0;
			foreach(Rule rule in unit.ActionRules)
			{
				actionRuleMap[rule] = new int?(id);
				++id;
			}
		}

		/// <summary>
		/// Get the ID of an IR type. </summary>
		/// <param name="map"> The map to look into. </param>
		/// <param name="ty"> The inheritance type to get the id for. </param>
		/// <returns> The type id for this type. </returns>
		protected internal static int GetTypeId<T1>(IDictionary<T1> map, Type t) where T1 : de.unika.ipd.grgen.ir.type.Type
		{
			int? res = map[t];
			return res.Value;
		}

		public int GetId(EdgeType et)
		{
			return GetTypeId(edgeTypeMap, et);
		}

		public int GetId(NodeType nt)
		{
			return GetTypeId(nodeTypeMap, nt);
		}

		public int GetId(Type t, bool forNode)
		{
			return forNode ? GetTypeId(nodeTypeMap, t) : GetTypeId(edgeTypeMap, t);
		}

		public short[][] GetIsAMatrix(bool forNode)
		{
			return forNode ? nodeTypeIsAMatrix : edgeTypeIsAMatrix;
		}

		public string GetTypeName(bool forNode, int obj)
		{
			return forNode ? nodeTypeNames[obj] : edgeTypeNames[obj];
		}

		public int[] GetSuperTypes(bool forNode, int obj)
		{
			return forNode ? nodeTypeSuperTypes[obj] : edgeTypeSuperTypes[obj];
		}

		public int[] GetSubTypes(bool forNode, int obj)
		{
			return forNode ? nodeTypeSubTypes[obj] : edgeTypeSubTypes[obj];
		}

		public int GetRootType(bool forNode)
		{
			return forNode ? nodeRoot : edgeRoot;
		}

		public int[] GetIDs(bool forNode)
		{
			IDictionary<InheritanceType, int> map = forNode
					? GetTypeMap(nodeTypeMap)
					: GetTypeMap(edgeTypeMap);
			int[] res = new int[map.Count];

			int i = 0;
			foreach(int? typeId in map.Values)
				res[i++] = typeId.Value;

			return res;
		}

		public static IDictionary<InheritanceType, int> GetTypeMap<T1>(IDictionary<T1> typeMap) where T1 : de.unika.ipd.grgen.ir.model.type.InheritanceType
		{
			return new LinkedHashMap<InheritanceType, int>(typeMap); // TODO: performance optimization caching (and maybe another collection type fits better)
		}

		/// <summary>
		/// Compute all IDs. </summary>
		/// <param name="unit"> The IR unit for ID computation. </param>
		protected internal void MakeTypes(Unit unit)
		{
			MakeTypeIds(unit);
			MakeSubpatternIds(unit);
			MakeActionIds(unit);

			nodeTypeIsAMatrix = ComputeIsA(GetTypeMap(nodeTypeMap));
			edgeTypeIsAMatrix = ComputeIsA(GetTypeMap(edgeTypeMap));
			nodeTypeSuperTypes = ComputeSuperTypes(GetTypeMap(nodeTypeMap));
			edgeTypeSuperTypes = ComputeSuperTypes(GetTypeMap(edgeTypeMap));
			nodeTypeSubTypes = ComputeSubTypes(GetTypeMap(nodeTypeMap));
			edgeTypeSubTypes = ComputeSubTypes(GetTypeMap(edgeTypeMap));
			nodeTypeNames = MakeNames(GetTypeMap(nodeTypeMap));
			edgeTypeNames = MakeNames(GetTypeMap(edgeTypeMap));
		}
	}

}
