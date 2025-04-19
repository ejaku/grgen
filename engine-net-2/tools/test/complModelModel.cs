// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "test.grg" on Sat Apr 19 15:09:25 CEST 2025

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Diagnostics;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_complModel;

namespace de.unika.ipd.grGen.Model_complModel
{

	//
	// Enums
	//

	public class Enums
	{
	}

	//
	// Node types
	//

	public enum NodeTypes { @Node=0, @A1=1, @A2=2, @A3=3, @A4=4, @A5=5, @B21=6, @B22=7, @B23=8, @B41=9, @B42=10, @B43=11, @C221=12, @C222_411=13, @C412_421_431_51=14, @C432_422=15, @D11_2221=16, @D2211_2222_31=17, @D231_4121=18 };

	// *** Node Node ***


	public sealed partial class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@Node[] pool;

		static @Node() {
		}

		public @Node() : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
			// implicit initialization, container creation of Node
		}

		public static GRGEN_MODEL.NodeType_Node TypeInstance { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@Node(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@Node(this, graph, oldToNewObjectMap);
		}

		private @Node(GRGEN_MODEL.@Node oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @Node))
				return false;
			@Node that_ = (@Node)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@Node[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Node
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@Node[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of Node
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@Node[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Node type \"Node\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Node type \"Node\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Node
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Node does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Node does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Node typeVar = new GRGEN_MODEL.NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Node();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@Node();
		}

	}

	// *** Node A1 ***

	public interface IA1 : GRGEN_LIBGR.INode
	{
		int @a1 { get; set; }
	}

	public sealed partial class @A1 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA1
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@A1[] pool;

		// explicit initializations of A1 for target A1
		// implicit initializations of A1 for target A1
		static @A1() {
		}

		public @A1() : base(GRGEN_MODEL.NodeType_A1.typeVar)
		{
			// implicit initialization, container creation of A1
			// explicit initializations of A1 for target A1
		}

		public static GRGEN_MODEL.NodeType_A1 TypeInstance { get { return GRGEN_MODEL.NodeType_A1.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@A1(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@A1(this, graph, oldToNewObjectMap);
		}

		private @A1(GRGEN_MODEL.@A1 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_A1.typeVar)
		{
			a1_M0no_suXx_h4rD = oldElem.a1_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @A1))
				return false;
			@A1 that_ = (@A1)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a1_M0no_suXx_h4rD == that_.a1_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@A1 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A1 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A1();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A1[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A1
				node.@a1 = 0;
				// explicit initializations of A1 for target A1
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@A1 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@A1 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A1();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A1[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A1
				node.@a1 = 0;
				// explicit initializations of A1 for target A1
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@A1[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a1_M0no_suXx_h4rD;
		public int @a1
		{
			get { return a1_M0no_suXx_h4rD; }
			set { a1_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a1": return this.@a1;
			}
			throw new NullReferenceException(
				"The Node type \"A1\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a1": this.@a1 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"A1\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of A1
			this.@a1 = 0;
			// explicit initializations of A1 for target A1
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A1 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A1 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_A1 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_A1 typeVar = new GRGEN_MODEL.NodeType_A1();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_a1;
		public NodeType_A1() : base((int) NodeTypes.@A1)
		{
			AttributeType_a1 = new GRGEN_LIBGR.AttributeType("a1", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "A1"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "A1"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IA1"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@A1"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@A1();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a1;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a1" : return AttributeType_a1;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@A1 newNode = new GRGEN_MODEL.@A1();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A1:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
					// copy attributes for: A1
					{
						GRGEN_MODEL.IA1 old = (GRGEN_MODEL.IA1) oldNode;
						newNode.@a1 = old.@a1;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_A1_a1 : Comparer<GRGEN_MODEL.IA1>
	{
		public static Comparer_A1_a1 thisComparer = new Comparer_A1_a1();
		public override int Compare(GRGEN_MODEL.IA1 a, GRGEN_MODEL.IA1 b)
		{
			return a.@a1.CompareTo(b.@a1);
		}
	}

	public class ReverseComparer_A1_a1 : Comparer<GRGEN_MODEL.IA1>
	{
		public static ReverseComparer_A1_a1 thisComparer = new ReverseComparer_A1_a1();
		public override int Compare(GRGEN_MODEL.IA1 b, GRGEN_MODEL.IA1 a)
		{
			return a.@a1.CompareTo(b.@a1);
		}
	}

	public class ArrayHelper_A1_a1
	{
		private static GRGEN_MODEL.IA1 instanceBearingAttributeForSearch = new GRGEN_MODEL.@A1();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IA1> list, int entry)
		{
			instanceBearingAttributeForSearch.@a1 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_A1_a1.thisComparer);
		}
		public static List<GRGEN_MODEL.IA1> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA1> list)
		{
			List<GRGEN_MODEL.IA1> newList = new List<GRGEN_MODEL.IA1>(list);
			newList.Sort(Comparer_A1_a1.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA1> ArrayOrderDescendingBy(List<GRGEN_MODEL.IA1> list)
		{
			List<GRGEN_MODEL.IA1> newList = new List<GRGEN_MODEL.IA1>(list);
			newList.Sort(ReverseComparer_A1_a1.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA1> ArrayGroupBy(List<GRGEN_MODEL.IA1> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IA1>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IA1>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a1)) {
					seenValues[list[pos].@a1].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IA1> tempList = new List<GRGEN_MODEL.IA1>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a1, tempList);
				}
			}
			List<GRGEN_MODEL.IA1> newList = new List<GRGEN_MODEL.IA1>();
			foreach(List<GRGEN_MODEL.IA1> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IA1> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IA1> list)
		{
			List<GRGEN_MODEL.IA1> newList = new List<GRGEN_MODEL.IA1>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IA1 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a1)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a1, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IA1> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IA1 entry in list)
				resultList.Add(entry.@a1);
			return resultList;
		}
	}


	// *** Node A2 ***

	public interface IA2 : GRGEN_LIBGR.INode
	{
		int @a2 { get; set; }
	}

	public sealed partial class @A2 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA2
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@A2[] pool;

		// explicit initializations of A2 for target A2
		// implicit initializations of A2 for target A2
		static @A2() {
		}

		public @A2() : base(GRGEN_MODEL.NodeType_A2.typeVar)
		{
			// implicit initialization, container creation of A2
			// explicit initializations of A2 for target A2
		}

		public static GRGEN_MODEL.NodeType_A2 TypeInstance { get { return GRGEN_MODEL.NodeType_A2.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@A2(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@A2(this, graph, oldToNewObjectMap);
		}

		private @A2(GRGEN_MODEL.@A2 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_A2.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @A2))
				return false;
			@A2 that_ = (@A2)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@A2 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A2 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A2();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A2[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A2
				node.@a2 = 0;
				// explicit initializations of A2 for target A2
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@A2 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@A2 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A2();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A2[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A2
				node.@a2 = 0;
				// explicit initializations of A2 for target A2
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@A2[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
			}
			throw new NullReferenceException(
				"The Node type \"A2\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"A2\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of A2
			this.@a2 = 0;
			// explicit initializations of A2 for target A2
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A2 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A2 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_A2 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_A2 typeVar = new GRGEN_MODEL.NodeType_A2();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, true, true, true, false, false, false, true, true, false, false, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_a2;
		public NodeType_A2() : base((int) NodeTypes.@A2)
		{
			AttributeType_a2 = new GRGEN_LIBGR.AttributeType("a2", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "A2"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "A2"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IA2"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@A2"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@A2();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a2;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return AttributeType_a2;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@A2 newNode = new GRGEN_MODEL.@A2();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@B23:
				case (int) GRGEN_MODEL.NodeTypes.@C221:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_A2_a2 : Comparer<GRGEN_MODEL.IA2>
	{
		public static Comparer_A2_a2 thisComparer = new Comparer_A2_a2();
		public override int Compare(GRGEN_MODEL.IA2 a, GRGEN_MODEL.IA2 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_A2_a2 : Comparer<GRGEN_MODEL.IA2>
	{
		public static ReverseComparer_A2_a2 thisComparer = new ReverseComparer_A2_a2();
		public override int Compare(GRGEN_MODEL.IA2 b, GRGEN_MODEL.IA2 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_A2_a2
	{
		private static GRGEN_MODEL.IA2 instanceBearingAttributeForSearch = new GRGEN_MODEL.@A2();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IA2> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_A2_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.IA2> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA2> list)
		{
			List<GRGEN_MODEL.IA2> newList = new List<GRGEN_MODEL.IA2>(list);
			newList.Sort(Comparer_A2_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA2> ArrayOrderDescendingBy(List<GRGEN_MODEL.IA2> list)
		{
			List<GRGEN_MODEL.IA2> newList = new List<GRGEN_MODEL.IA2>(list);
			newList.Sort(ReverseComparer_A2_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA2> ArrayGroupBy(List<GRGEN_MODEL.IA2> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IA2>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IA2>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IA2> tempList = new List<GRGEN_MODEL.IA2>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.IA2> newList = new List<GRGEN_MODEL.IA2>();
			foreach(List<GRGEN_MODEL.IA2> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IA2> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IA2> list)
		{
			List<GRGEN_MODEL.IA2> newList = new List<GRGEN_MODEL.IA2>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IA2 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IA2> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IA2 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	// *** Node A3 ***

	public interface IA3 : GRGEN_LIBGR.INode
	{
		int @a3 { get; set; }
	}

	public sealed partial class @A3 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA3
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@A3[] pool;

		// explicit initializations of A3 for target A3
		// implicit initializations of A3 for target A3
		static @A3() {
		}

		public @A3() : base(GRGEN_MODEL.NodeType_A3.typeVar)
		{
			// implicit initialization, container creation of A3
			// explicit initializations of A3 for target A3
		}

		public static GRGEN_MODEL.NodeType_A3 TypeInstance { get { return GRGEN_MODEL.NodeType_A3.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@A3(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@A3(this, graph, oldToNewObjectMap);
		}

		private @A3(GRGEN_MODEL.@A3 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_A3.typeVar)
		{
			a3_M0no_suXx_h4rD = oldElem.a3_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @A3))
				return false;
			@A3 that_ = (@A3)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a3_M0no_suXx_h4rD == that_.a3_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@A3 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A3 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A3();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A3[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A3
				node.@a3 = 0;
				// explicit initializations of A3 for target A3
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@A3 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@A3 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A3();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A3[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A3
				node.@a3 = 0;
				// explicit initializations of A3 for target A3
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@A3[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a3_M0no_suXx_h4rD;
		public int @a3
		{
			get { return a3_M0no_suXx_h4rD; }
			set { a3_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a3": return this.@a3;
			}
			throw new NullReferenceException(
				"The Node type \"A3\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a3": this.@a3 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"A3\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of A3
			this.@a3 = 0;
			// explicit initializations of A3 for target A3
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A3 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A3 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_A3 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_A3 typeVar = new GRGEN_MODEL.NodeType_A3();
		public static bool[] isA = new bool[] { true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_a3;
		public NodeType_A3() : base((int) NodeTypes.@A3)
		{
			AttributeType_a3 = new GRGEN_LIBGR.AttributeType("a3", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "A3"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "A3"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IA3"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@A3"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@A3();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a3;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a3" : return AttributeType_a3;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@A3 newNode = new GRGEN_MODEL.@A3();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A3:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: A3
					{
						GRGEN_MODEL.IA3 old = (GRGEN_MODEL.IA3) oldNode;
						newNode.@a3 = old.@a3;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_A3_a3 : Comparer<GRGEN_MODEL.IA3>
	{
		public static Comparer_A3_a3 thisComparer = new Comparer_A3_a3();
		public override int Compare(GRGEN_MODEL.IA3 a, GRGEN_MODEL.IA3 b)
		{
			return a.@a3.CompareTo(b.@a3);
		}
	}

	public class ReverseComparer_A3_a3 : Comparer<GRGEN_MODEL.IA3>
	{
		public static ReverseComparer_A3_a3 thisComparer = new ReverseComparer_A3_a3();
		public override int Compare(GRGEN_MODEL.IA3 b, GRGEN_MODEL.IA3 a)
		{
			return a.@a3.CompareTo(b.@a3);
		}
	}

	public class ArrayHelper_A3_a3
	{
		private static GRGEN_MODEL.IA3 instanceBearingAttributeForSearch = new GRGEN_MODEL.@A3();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IA3> list, int entry)
		{
			instanceBearingAttributeForSearch.@a3 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_A3_a3.thisComparer);
		}
		public static List<GRGEN_MODEL.IA3> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA3> list)
		{
			List<GRGEN_MODEL.IA3> newList = new List<GRGEN_MODEL.IA3>(list);
			newList.Sort(Comparer_A3_a3.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA3> ArrayOrderDescendingBy(List<GRGEN_MODEL.IA3> list)
		{
			List<GRGEN_MODEL.IA3> newList = new List<GRGEN_MODEL.IA3>(list);
			newList.Sort(ReverseComparer_A3_a3.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA3> ArrayGroupBy(List<GRGEN_MODEL.IA3> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IA3>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IA3>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a3)) {
					seenValues[list[pos].@a3].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IA3> tempList = new List<GRGEN_MODEL.IA3>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a3, tempList);
				}
			}
			List<GRGEN_MODEL.IA3> newList = new List<GRGEN_MODEL.IA3>();
			foreach(List<GRGEN_MODEL.IA3> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IA3> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IA3> list)
		{
			List<GRGEN_MODEL.IA3> newList = new List<GRGEN_MODEL.IA3>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IA3 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a3)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a3, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IA3> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IA3 entry in list)
				resultList.Add(entry.@a3);
			return resultList;
		}
	}


	// *** Node A4 ***

	public interface IA4 : GRGEN_LIBGR.INode
	{
		int @a4 { get; set; }
	}

	public sealed partial class @A4 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA4
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@A4[] pool;

		// explicit initializations of A4 for target A4
		// implicit initializations of A4 for target A4
		static @A4() {
		}

		public @A4() : base(GRGEN_MODEL.NodeType_A4.typeVar)
		{
			// implicit initialization, container creation of A4
			// explicit initializations of A4 for target A4
		}

		public static GRGEN_MODEL.NodeType_A4 TypeInstance { get { return GRGEN_MODEL.NodeType_A4.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@A4(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@A4(this, graph, oldToNewObjectMap);
		}

		private @A4(GRGEN_MODEL.@A4 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_A4.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @A4))
				return false;
			@A4 that_ = (@A4)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@A4 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A4 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A4();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A4[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A4
				node.@a4 = 0;
				// explicit initializations of A4 for target A4
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@A4 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@A4 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A4();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A4[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A4
				node.@a4 = 0;
				// explicit initializations of A4 for target A4
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@A4[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return this.@a4;
			}
			throw new NullReferenceException(
				"The Node type \"A4\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"A4\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of A4
			this.@a4 = 0;
			// explicit initializations of A4 for target A4
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A4 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A4 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_A4 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_A4 typeVar = new GRGEN_MODEL.NodeType_A4();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, true, true, true, false, true, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_a4;
		public NodeType_A4() : base((int) NodeTypes.@A4)
		{
			AttributeType_a4 = new GRGEN_LIBGR.AttributeType("a4", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "A4"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "A4"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IA4"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@A4"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@A4();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a4;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a4" : return AttributeType_a4;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@A4 newNode = new GRGEN_MODEL.@A4();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@B42:
				case (int) GRGEN_MODEL.NodeTypes.@B43:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_A4_a4 : Comparer<GRGEN_MODEL.IA4>
	{
		public static Comparer_A4_a4 thisComparer = new Comparer_A4_a4();
		public override int Compare(GRGEN_MODEL.IA4 a, GRGEN_MODEL.IA4 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_A4_a4 : Comparer<GRGEN_MODEL.IA4>
	{
		public static ReverseComparer_A4_a4 thisComparer = new ReverseComparer_A4_a4();
		public override int Compare(GRGEN_MODEL.IA4 b, GRGEN_MODEL.IA4 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_A4_a4
	{
		private static GRGEN_MODEL.IA4 instanceBearingAttributeForSearch = new GRGEN_MODEL.@A4();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IA4> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_A4_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.IA4> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA4> list)
		{
			List<GRGEN_MODEL.IA4> newList = new List<GRGEN_MODEL.IA4>(list);
			newList.Sort(Comparer_A4_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA4> ArrayOrderDescendingBy(List<GRGEN_MODEL.IA4> list)
		{
			List<GRGEN_MODEL.IA4> newList = new List<GRGEN_MODEL.IA4>(list);
			newList.Sort(ReverseComparer_A4_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA4> ArrayGroupBy(List<GRGEN_MODEL.IA4> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IA4>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IA4>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IA4> tempList = new List<GRGEN_MODEL.IA4>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.IA4> newList = new List<GRGEN_MODEL.IA4>();
			foreach(List<GRGEN_MODEL.IA4> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IA4> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IA4> list)
		{
			List<GRGEN_MODEL.IA4> newList = new List<GRGEN_MODEL.IA4>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IA4 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IA4> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IA4 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	// *** Node A5 ***

	public interface IA5 : GRGEN_LIBGR.INode
	{
		int @a5 { get; set; }
	}

	public sealed partial class @A5 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA5
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@A5[] pool;

		// explicit initializations of A5 for target A5
		// implicit initializations of A5 for target A5
		static @A5() {
		}

		public @A5() : base(GRGEN_MODEL.NodeType_A5.typeVar)
		{
			// implicit initialization, container creation of A5
			// explicit initializations of A5 for target A5
		}

		public static GRGEN_MODEL.NodeType_A5 TypeInstance { get { return GRGEN_MODEL.NodeType_A5.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@A5(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@A5(this, graph, oldToNewObjectMap);
		}

		private @A5(GRGEN_MODEL.@A5 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_A5.typeVar)
		{
			a5_M0no_suXx_h4rD = oldElem.a5_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @A5))
				return false;
			@A5 that_ = (@A5)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a5_M0no_suXx_h4rD == that_.a5_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@A5 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A5 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A5();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A5[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A5
				node.@a5 = 0;
				// explicit initializations of A5 for target A5
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@A5 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@A5 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A5();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@A5[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of A5
				node.@a5 = 0;
				// explicit initializations of A5 for target A5
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@A5[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a5_M0no_suXx_h4rD;
		public int @a5
		{
			get { return a5_M0no_suXx_h4rD; }
			set { a5_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a5": return this.@a5;
			}
			throw new NullReferenceException(
				"The Node type \"A5\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a5": this.@a5 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"A5\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of A5
			this.@a5 = 0;
			// explicit initializations of A5 for target A5
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A5 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("A5 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_A5 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_A5 typeVar = new GRGEN_MODEL.NodeType_A5();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, false, false, true, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_a5;
		public NodeType_A5() : base((int) NodeTypes.@A5)
		{
			AttributeType_a5 = new GRGEN_LIBGR.AttributeType("a5", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "A5"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "A5"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IA5"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@A5"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@A5();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a5;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a5" : return AttributeType_a5;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@A5 newNode = new GRGEN_MODEL.@A5();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A5:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A5
					{
						GRGEN_MODEL.IA5 old = (GRGEN_MODEL.IA5) oldNode;
						newNode.@a5 = old.@a5;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_A5_a5 : Comparer<GRGEN_MODEL.IA5>
	{
		public static Comparer_A5_a5 thisComparer = new Comparer_A5_a5();
		public override int Compare(GRGEN_MODEL.IA5 a, GRGEN_MODEL.IA5 b)
		{
			return a.@a5.CompareTo(b.@a5);
		}
	}

	public class ReverseComparer_A5_a5 : Comparer<GRGEN_MODEL.IA5>
	{
		public static ReverseComparer_A5_a5 thisComparer = new ReverseComparer_A5_a5();
		public override int Compare(GRGEN_MODEL.IA5 b, GRGEN_MODEL.IA5 a)
		{
			return a.@a5.CompareTo(b.@a5);
		}
	}

	public class ArrayHelper_A5_a5
	{
		private static GRGEN_MODEL.IA5 instanceBearingAttributeForSearch = new GRGEN_MODEL.@A5();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IA5> list, int entry)
		{
			instanceBearingAttributeForSearch.@a5 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_A5_a5.thisComparer);
		}
		public static List<GRGEN_MODEL.IA5> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA5> list)
		{
			List<GRGEN_MODEL.IA5> newList = new List<GRGEN_MODEL.IA5>(list);
			newList.Sort(Comparer_A5_a5.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA5> ArrayOrderDescendingBy(List<GRGEN_MODEL.IA5> list)
		{
			List<GRGEN_MODEL.IA5> newList = new List<GRGEN_MODEL.IA5>(list);
			newList.Sort(ReverseComparer_A5_a5.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IA5> ArrayGroupBy(List<GRGEN_MODEL.IA5> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IA5>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IA5>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a5)) {
					seenValues[list[pos].@a5].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IA5> tempList = new List<GRGEN_MODEL.IA5>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a5, tempList);
				}
			}
			List<GRGEN_MODEL.IA5> newList = new List<GRGEN_MODEL.IA5>();
			foreach(List<GRGEN_MODEL.IA5> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IA5> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IA5> list)
		{
			List<GRGEN_MODEL.IA5> newList = new List<GRGEN_MODEL.IA5>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IA5 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a5)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a5, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IA5> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IA5 entry in list)
				resultList.Add(entry.@a5);
			return resultList;
		}
	}


	// *** Node B21 ***

	public interface IB21 : IA2
	{
		int @b21 { get; set; }
	}

	public sealed partial class @B21 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB21
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@B21[] pool;

		// explicit initializations of A2 for target B21
		// implicit initializations of A2 for target B21
		// explicit initializations of B21 for target B21
		// implicit initializations of B21 for target B21
		static @B21() {
		}

		public @B21() : base(GRGEN_MODEL.NodeType_B21.typeVar)
		{
			// implicit initialization, container creation of B21
			// explicit initializations of A2 for target B21
			// explicit initializations of B21 for target B21
		}

		public static GRGEN_MODEL.NodeType_B21 TypeInstance { get { return GRGEN_MODEL.NodeType_B21.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@B21(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@B21(this, graph, oldToNewObjectMap);
		}

		private @B21(GRGEN_MODEL.@B21 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_B21.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b21_M0no_suXx_h4rD = oldElem.b21_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @B21))
				return false;
			@B21 that_ = (@B21)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b21_M0no_suXx_h4rD == that_.b21_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@B21 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B21 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B21();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B21[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B21
				node.@a2 = 0;
				node.@b21 = 0;
				// explicit initializations of A2 for target B21
				// explicit initializations of B21 for target B21
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@B21 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@B21 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B21();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B21[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B21
				node.@a2 = 0;
				node.@b21 = 0;
				// explicit initializations of A2 for target B21
				// explicit initializations of B21 for target B21
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@B21[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b21_M0no_suXx_h4rD;
		public int @b21
		{
			get { return b21_M0no_suXx_h4rD; }
			set { b21_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
				case "b21": return this.@b21;
			}
			throw new NullReferenceException(
				"The Node type \"B21\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b21": this.@b21 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"B21\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of B21
			this.@a2 = 0;
			this.@b21 = 0;
			// explicit initializations of A2 for target B21
			// explicit initializations of B21 for target B21
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B21 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B21 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_B21 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_B21 typeVar = new GRGEN_MODEL.NodeType_B21();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_b21;
		public NodeType_B21() : base((int) NodeTypes.@B21)
		{
			AttributeType_b21 = new GRGEN_LIBGR.AttributeType("b21", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "B21"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "B21"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IB21"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@B21"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@B21();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return AttributeType_b21;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b21" : return AttributeType_b21;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@B21 newNode = new GRGEN_MODEL.@B21();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@B23:
				case (int) GRGEN_MODEL.NodeTypes.@C221:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B21:
					// copy attributes for: B21
					{
						GRGEN_MODEL.IB21 old = (GRGEN_MODEL.IB21) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b21 = old.@b21;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_B21_a2 : Comparer<GRGEN_MODEL.IB21>
	{
		public static Comparer_B21_a2 thisComparer = new Comparer_B21_a2();
		public override int Compare(GRGEN_MODEL.IB21 a, GRGEN_MODEL.IB21 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_B21_a2 : Comparer<GRGEN_MODEL.IB21>
	{
		public static ReverseComparer_B21_a2 thisComparer = new ReverseComparer_B21_a2();
		public override int Compare(GRGEN_MODEL.IB21 b, GRGEN_MODEL.IB21 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_B21_a2
	{
		private static GRGEN_MODEL.IB21 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B21();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB21> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B21_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.IB21> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>(list);
			newList.Sort(Comparer_B21_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB21> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>(list);
			newList.Sort(ReverseComparer_B21_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB21> ArrayGroupBy(List<GRGEN_MODEL.IB21> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB21>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB21>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB21> tempList = new List<GRGEN_MODEL.IB21>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>();
			foreach(List<GRGEN_MODEL.IB21> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB21> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB21 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB21> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB21 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	public class Comparer_B21_b21 : Comparer<GRGEN_MODEL.IB21>
	{
		public static Comparer_B21_b21 thisComparer = new Comparer_B21_b21();
		public override int Compare(GRGEN_MODEL.IB21 a, GRGEN_MODEL.IB21 b)
		{
			return a.@b21.CompareTo(b.@b21);
		}
	}

	public class ReverseComparer_B21_b21 : Comparer<GRGEN_MODEL.IB21>
	{
		public static ReverseComparer_B21_b21 thisComparer = new ReverseComparer_B21_b21();
		public override int Compare(GRGEN_MODEL.IB21 b, GRGEN_MODEL.IB21 a)
		{
			return a.@b21.CompareTo(b.@b21);
		}
	}

	public class ArrayHelper_B21_b21
	{
		private static GRGEN_MODEL.IB21 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B21();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB21> list, int entry)
		{
			instanceBearingAttributeForSearch.@b21 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B21_b21.thisComparer);
		}
		public static List<GRGEN_MODEL.IB21> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>(list);
			newList.Sort(Comparer_B21_b21.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB21> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>(list);
			newList.Sort(ReverseComparer_B21_b21.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB21> ArrayGroupBy(List<GRGEN_MODEL.IB21> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB21>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB21>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b21)) {
					seenValues[list[pos].@b21].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB21> tempList = new List<GRGEN_MODEL.IB21>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b21, tempList);
				}
			}
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>();
			foreach(List<GRGEN_MODEL.IB21> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB21> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB21 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b21)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b21, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB21> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB21 entry in list)
				resultList.Add(entry.@b21);
			return resultList;
		}
	}


	// *** Node B22 ***

	public interface IB22 : IA2
	{
		int @b22 { get; set; }
	}

	public sealed partial class @B22 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB22
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@B22[] pool;

		// explicit initializations of A2 for target B22
		// implicit initializations of A2 for target B22
		// explicit initializations of B22 for target B22
		// implicit initializations of B22 for target B22
		static @B22() {
		}

		public @B22() : base(GRGEN_MODEL.NodeType_B22.typeVar)
		{
			// implicit initialization, container creation of B22
			// explicit initializations of A2 for target B22
			// explicit initializations of B22 for target B22
		}

		public static GRGEN_MODEL.NodeType_B22 TypeInstance { get { return GRGEN_MODEL.NodeType_B22.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@B22(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@B22(this, graph, oldToNewObjectMap);
		}

		private @B22(GRGEN_MODEL.@B22 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_B22.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @B22))
				return false;
			@B22 that_ = (@B22)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@B22 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B22 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B22();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B22[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B22
				node.@a2 = 0;
				node.@b22 = 0;
				// explicit initializations of A2 for target B22
				// explicit initializations of B22 for target B22
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@B22 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@B22 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B22();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B22[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B22
				node.@a2 = 0;
				node.@b22 = 0;
				// explicit initializations of A2 for target B22
				// explicit initializations of B22 for target B22
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@B22[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b22_M0no_suXx_h4rD;
		public int @b22
		{
			get { return b22_M0no_suXx_h4rD; }
			set { b22_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
				case "b22": return this.@b22;
			}
			throw new NullReferenceException(
				"The Node type \"B22\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b22": this.@b22 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"B22\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of B22
			this.@a2 = 0;
			this.@b22 = 0;
			// explicit initializations of A2 for target B22
			// explicit initializations of B22 for target B22
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B22 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B22 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_B22 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_B22 typeVar = new GRGEN_MODEL.NodeType_B22();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, true, true, false, false, true, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_b22;
		public NodeType_B22() : base((int) NodeTypes.@B22)
		{
			AttributeType_b22 = new GRGEN_LIBGR.AttributeType("b22", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "B22"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "B22"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IB22"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@B22"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@B22();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return AttributeType_b22;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b22" : return AttributeType_b22;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@B22 newNode = new GRGEN_MODEL.@B22();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B23:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@C221:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: B22
					{
						GRGEN_MODEL.IB22 old = (GRGEN_MODEL.IB22) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_B22_a2 : Comparer<GRGEN_MODEL.IB22>
	{
		public static Comparer_B22_a2 thisComparer = new Comparer_B22_a2();
		public override int Compare(GRGEN_MODEL.IB22 a, GRGEN_MODEL.IB22 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_B22_a2 : Comparer<GRGEN_MODEL.IB22>
	{
		public static ReverseComparer_B22_a2 thisComparer = new ReverseComparer_B22_a2();
		public override int Compare(GRGEN_MODEL.IB22 b, GRGEN_MODEL.IB22 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_B22_a2
	{
		private static GRGEN_MODEL.IB22 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B22();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB22> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B22_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.IB22> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>(list);
			newList.Sort(Comparer_B22_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB22> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>(list);
			newList.Sort(ReverseComparer_B22_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB22> ArrayGroupBy(List<GRGEN_MODEL.IB22> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB22>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB22>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB22> tempList = new List<GRGEN_MODEL.IB22>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>();
			foreach(List<GRGEN_MODEL.IB22> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB22> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB22 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB22> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB22 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	public class Comparer_B22_b22 : Comparer<GRGEN_MODEL.IB22>
	{
		public static Comparer_B22_b22 thisComparer = new Comparer_B22_b22();
		public override int Compare(GRGEN_MODEL.IB22 a, GRGEN_MODEL.IB22 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ReverseComparer_B22_b22 : Comparer<GRGEN_MODEL.IB22>
	{
		public static ReverseComparer_B22_b22 thisComparer = new ReverseComparer_B22_b22();
		public override int Compare(GRGEN_MODEL.IB22 b, GRGEN_MODEL.IB22 a)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ArrayHelper_B22_b22
	{
		private static GRGEN_MODEL.IB22 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B22();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB22> list, int entry)
		{
			instanceBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B22_b22.thisComparer);
		}
		public static List<GRGEN_MODEL.IB22> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>(list);
			newList.Sort(Comparer_B22_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB22> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>(list);
			newList.Sort(ReverseComparer_B22_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB22> ArrayGroupBy(List<GRGEN_MODEL.IB22> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB22>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB22>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b22)) {
					seenValues[list[pos].@b22].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB22> tempList = new List<GRGEN_MODEL.IB22>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b22, tempList);
				}
			}
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>();
			foreach(List<GRGEN_MODEL.IB22> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB22> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB22 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b22)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b22, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB22> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB22 entry in list)
				resultList.Add(entry.@b22);
			return resultList;
		}
	}


	// *** Node B23 ***

	public interface IB23 : IA2
	{
		int @b23 { get; set; }
	}

	public sealed partial class @B23 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB23
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@B23[] pool;

		// explicit initializations of A2 for target B23
		// implicit initializations of A2 for target B23
		// explicit initializations of B23 for target B23
		// implicit initializations of B23 for target B23
		static @B23() {
		}

		public @B23() : base(GRGEN_MODEL.NodeType_B23.typeVar)
		{
			// implicit initialization, container creation of B23
			// explicit initializations of A2 for target B23
			// explicit initializations of B23 for target B23
		}

		public static GRGEN_MODEL.NodeType_B23 TypeInstance { get { return GRGEN_MODEL.NodeType_B23.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@B23(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@B23(this, graph, oldToNewObjectMap);
		}

		private @B23(GRGEN_MODEL.@B23 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_B23.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b23_M0no_suXx_h4rD = oldElem.b23_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @B23))
				return false;
			@B23 that_ = (@B23)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b23_M0no_suXx_h4rD == that_.b23_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@B23 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B23 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B23();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B23[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B23
				node.@a2 = 0;
				node.@b23 = 0;
				// explicit initializations of A2 for target B23
				// explicit initializations of B23 for target B23
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@B23 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@B23 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B23();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B23[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B23
				node.@a2 = 0;
				node.@b23 = 0;
				// explicit initializations of A2 for target B23
				// explicit initializations of B23 for target B23
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@B23[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b23_M0no_suXx_h4rD;
		public int @b23
		{
			get { return b23_M0no_suXx_h4rD; }
			set { b23_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
				case "b23": return this.@b23;
			}
			throw new NullReferenceException(
				"The Node type \"B23\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b23": this.@b23 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"B23\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of B23
			this.@a2 = 0;
			this.@b23 = 0;
			// explicit initializations of A2 for target B23
			// explicit initializations of B23 for target B23
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B23 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B23 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_B23 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_B23 typeVar = new GRGEN_MODEL.NodeType_B23();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_b23;
		public NodeType_B23() : base((int) NodeTypes.@B23)
		{
			AttributeType_b23 = new GRGEN_LIBGR.AttributeType("b23", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "B23"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "B23"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IB23"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@B23"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@B23();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return AttributeType_b23;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b23" : return AttributeType_b23;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@B23 newNode = new GRGEN_MODEL.@B23();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@C221:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B23:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: B23
					{
						GRGEN_MODEL.IB23 old = (GRGEN_MODEL.IB23) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b23 = old.@b23;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_B23_a2 : Comparer<GRGEN_MODEL.IB23>
	{
		public static Comparer_B23_a2 thisComparer = new Comparer_B23_a2();
		public override int Compare(GRGEN_MODEL.IB23 a, GRGEN_MODEL.IB23 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_B23_a2 : Comparer<GRGEN_MODEL.IB23>
	{
		public static ReverseComparer_B23_a2 thisComparer = new ReverseComparer_B23_a2();
		public override int Compare(GRGEN_MODEL.IB23 b, GRGEN_MODEL.IB23 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_B23_a2
	{
		private static GRGEN_MODEL.IB23 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B23();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB23> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B23_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.IB23> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>(list);
			newList.Sort(Comparer_B23_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB23> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>(list);
			newList.Sort(ReverseComparer_B23_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB23> ArrayGroupBy(List<GRGEN_MODEL.IB23> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB23>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB23>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB23> tempList = new List<GRGEN_MODEL.IB23>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>();
			foreach(List<GRGEN_MODEL.IB23> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB23> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB23 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB23> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB23 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	public class Comparer_B23_b23 : Comparer<GRGEN_MODEL.IB23>
	{
		public static Comparer_B23_b23 thisComparer = new Comparer_B23_b23();
		public override int Compare(GRGEN_MODEL.IB23 a, GRGEN_MODEL.IB23 b)
		{
			return a.@b23.CompareTo(b.@b23);
		}
	}

	public class ReverseComparer_B23_b23 : Comparer<GRGEN_MODEL.IB23>
	{
		public static ReverseComparer_B23_b23 thisComparer = new ReverseComparer_B23_b23();
		public override int Compare(GRGEN_MODEL.IB23 b, GRGEN_MODEL.IB23 a)
		{
			return a.@b23.CompareTo(b.@b23);
		}
	}

	public class ArrayHelper_B23_b23
	{
		private static GRGEN_MODEL.IB23 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B23();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB23> list, int entry)
		{
			instanceBearingAttributeForSearch.@b23 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B23_b23.thisComparer);
		}
		public static List<GRGEN_MODEL.IB23> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>(list);
			newList.Sort(Comparer_B23_b23.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB23> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>(list);
			newList.Sort(ReverseComparer_B23_b23.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB23> ArrayGroupBy(List<GRGEN_MODEL.IB23> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB23>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB23>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b23)) {
					seenValues[list[pos].@b23].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB23> tempList = new List<GRGEN_MODEL.IB23>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b23, tempList);
				}
			}
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>();
			foreach(List<GRGEN_MODEL.IB23> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB23> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB23 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b23)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b23, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB23> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB23 entry in list)
				resultList.Add(entry.@b23);
			return resultList;
		}
	}


	// *** Node B41 ***

	public interface IB41 : IA4
	{
		int @b41 { get; set; }
	}

	public sealed partial class @B41 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB41
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@B41[] pool;

		// explicit initializations of A4 for target B41
		// implicit initializations of A4 for target B41
		// explicit initializations of B41 for target B41
		// implicit initializations of B41 for target B41
		static @B41() {
		}

		public @B41() : base(GRGEN_MODEL.NodeType_B41.typeVar)
		{
			// implicit initialization, container creation of B41
			// explicit initializations of A4 for target B41
			// explicit initializations of B41 for target B41
		}

		public static GRGEN_MODEL.NodeType_B41 TypeInstance { get { return GRGEN_MODEL.NodeType_B41.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@B41(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@B41(this, graph, oldToNewObjectMap);
		}

		private @B41(GRGEN_MODEL.@B41 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_B41.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @B41))
				return false;
			@B41 that_ = (@B41)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@B41 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B41 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B41();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B41[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B41
				node.@a4 = 0;
				node.@b41 = 0;
				// explicit initializations of A4 for target B41
				// explicit initializations of B41 for target B41
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@B41 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@B41 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B41();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B41[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B41
				node.@a4 = 0;
				node.@b41 = 0;
				// explicit initializations of A4 for target B41
				// explicit initializations of B41 for target B41
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@B41[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b41_M0no_suXx_h4rD;
		public int @b41
		{
			get { return b41_M0no_suXx_h4rD; }
			set { b41_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return this.@a4;
				case "b41": return this.@b41;
			}
			throw new NullReferenceException(
				"The Node type \"B41\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
				case "b41": this.@b41 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"B41\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of B41
			this.@a4 = 0;
			this.@b41 = 0;
			// explicit initializations of A4 for target B41
			// explicit initializations of B41 for target B41
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B41 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B41 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_B41 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_B41 typeVar = new GRGEN_MODEL.NodeType_B41();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, false, true, true, false, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_b41;
		public NodeType_B41() : base((int) NodeTypes.@B41)
		{
			AttributeType_b41 = new GRGEN_LIBGR.AttributeType("b41", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "B41"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "B41"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IB41"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@B41"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@B41();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return AttributeType_b41;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b41" : return AttributeType_b41;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@B41 newNode = new GRGEN_MODEL.@B41();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B42:
				case (int) GRGEN_MODEL.NodeTypes.@B43:
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_B41_a4 : Comparer<GRGEN_MODEL.IB41>
	{
		public static Comparer_B41_a4 thisComparer = new Comparer_B41_a4();
		public override int Compare(GRGEN_MODEL.IB41 a, GRGEN_MODEL.IB41 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_B41_a4 : Comparer<GRGEN_MODEL.IB41>
	{
		public static ReverseComparer_B41_a4 thisComparer = new ReverseComparer_B41_a4();
		public override int Compare(GRGEN_MODEL.IB41 b, GRGEN_MODEL.IB41 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_B41_a4
	{
		private static GRGEN_MODEL.IB41 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B41();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB41> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B41_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.IB41> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>(list);
			newList.Sort(Comparer_B41_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB41> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>(list);
			newList.Sort(ReverseComparer_B41_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB41> ArrayGroupBy(List<GRGEN_MODEL.IB41> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB41>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB41>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB41> tempList = new List<GRGEN_MODEL.IB41>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>();
			foreach(List<GRGEN_MODEL.IB41> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB41> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB41 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB41> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB41 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	public class Comparer_B41_b41 : Comparer<GRGEN_MODEL.IB41>
	{
		public static Comparer_B41_b41 thisComparer = new Comparer_B41_b41();
		public override int Compare(GRGEN_MODEL.IB41 a, GRGEN_MODEL.IB41 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ReverseComparer_B41_b41 : Comparer<GRGEN_MODEL.IB41>
	{
		public static ReverseComparer_B41_b41 thisComparer = new ReverseComparer_B41_b41();
		public override int Compare(GRGEN_MODEL.IB41 b, GRGEN_MODEL.IB41 a)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ArrayHelper_B41_b41
	{
		private static GRGEN_MODEL.IB41 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B41();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB41> list, int entry)
		{
			instanceBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B41_b41.thisComparer);
		}
		public static List<GRGEN_MODEL.IB41> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>(list);
			newList.Sort(Comparer_B41_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB41> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>(list);
			newList.Sort(ReverseComparer_B41_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB41> ArrayGroupBy(List<GRGEN_MODEL.IB41> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB41>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB41>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b41)) {
					seenValues[list[pos].@b41].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB41> tempList = new List<GRGEN_MODEL.IB41>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b41, tempList);
				}
			}
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>();
			foreach(List<GRGEN_MODEL.IB41> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB41> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB41 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b41)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b41, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB41> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB41 entry in list)
				resultList.Add(entry.@b41);
			return resultList;
		}
	}


	// *** Node B42 ***

	public interface IB42 : IA4
	{
		int @b42 { get; set; }
	}

	public sealed partial class @B42 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB42
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@B42[] pool;

		// explicit initializations of A4 for target B42
		// implicit initializations of A4 for target B42
		// explicit initializations of B42 for target B42
		// implicit initializations of B42 for target B42
		static @B42() {
		}

		public @B42() : base(GRGEN_MODEL.NodeType_B42.typeVar)
		{
			// implicit initialization, container creation of B42
			// explicit initializations of A4 for target B42
			// explicit initializations of B42 for target B42
		}

		public static GRGEN_MODEL.NodeType_B42 TypeInstance { get { return GRGEN_MODEL.NodeType_B42.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@B42(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@B42(this, graph, oldToNewObjectMap);
		}

		private @B42(GRGEN_MODEL.@B42 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_B42.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @B42))
				return false;
			@B42 that_ = (@B42)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@B42 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B42 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B42();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B42[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B42
				node.@a4 = 0;
				node.@b42 = 0;
				// explicit initializations of A4 for target B42
				// explicit initializations of B42 for target B42
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@B42 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@B42 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B42();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B42[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B42
				node.@a4 = 0;
				node.@b42 = 0;
				// explicit initializations of A4 for target B42
				// explicit initializations of B42 for target B42
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@B42[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b42_M0no_suXx_h4rD;
		public int @b42
		{
			get { return b42_M0no_suXx_h4rD; }
			set { b42_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return this.@a4;
				case "b42": return this.@b42;
			}
			throw new NullReferenceException(
				"The Node type \"B42\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
				case "b42": this.@b42 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"B42\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of B42
			this.@a4 = 0;
			this.@b42 = 0;
			// explicit initializations of A4 for target B42
			// explicit initializations of B42 for target B42
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B42 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B42 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_B42 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_B42 typeVar = new GRGEN_MODEL.NodeType_B42();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, false, false, true, true, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_b42;
		public NodeType_B42() : base((int) NodeTypes.@B42)
		{
			AttributeType_b42 = new GRGEN_LIBGR.AttributeType("b42", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "B42"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "B42"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IB42"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@B42"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@B42();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return AttributeType_b42;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b42" : return AttributeType_b42;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@B42 newNode = new GRGEN_MODEL.@B42();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@B43:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B42:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: B42
					{
						GRGEN_MODEL.IB42 old = (GRGEN_MODEL.IB42) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_B42_a4 : Comparer<GRGEN_MODEL.IB42>
	{
		public static Comparer_B42_a4 thisComparer = new Comparer_B42_a4();
		public override int Compare(GRGEN_MODEL.IB42 a, GRGEN_MODEL.IB42 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_B42_a4 : Comparer<GRGEN_MODEL.IB42>
	{
		public static ReverseComparer_B42_a4 thisComparer = new ReverseComparer_B42_a4();
		public override int Compare(GRGEN_MODEL.IB42 b, GRGEN_MODEL.IB42 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_B42_a4
	{
		private static GRGEN_MODEL.IB42 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B42();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB42> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B42_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.IB42> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>(list);
			newList.Sort(Comparer_B42_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB42> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>(list);
			newList.Sort(ReverseComparer_B42_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB42> ArrayGroupBy(List<GRGEN_MODEL.IB42> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB42>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB42>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB42> tempList = new List<GRGEN_MODEL.IB42>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>();
			foreach(List<GRGEN_MODEL.IB42> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB42> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB42 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB42> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB42 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	public class Comparer_B42_b42 : Comparer<GRGEN_MODEL.IB42>
	{
		public static Comparer_B42_b42 thisComparer = new Comparer_B42_b42();
		public override int Compare(GRGEN_MODEL.IB42 a, GRGEN_MODEL.IB42 b)
		{
			return a.@b42.CompareTo(b.@b42);
		}
	}

	public class ReverseComparer_B42_b42 : Comparer<GRGEN_MODEL.IB42>
	{
		public static ReverseComparer_B42_b42 thisComparer = new ReverseComparer_B42_b42();
		public override int Compare(GRGEN_MODEL.IB42 b, GRGEN_MODEL.IB42 a)
		{
			return a.@b42.CompareTo(b.@b42);
		}
	}

	public class ArrayHelper_B42_b42
	{
		private static GRGEN_MODEL.IB42 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B42();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB42> list, int entry)
		{
			instanceBearingAttributeForSearch.@b42 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B42_b42.thisComparer);
		}
		public static List<GRGEN_MODEL.IB42> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>(list);
			newList.Sort(Comparer_B42_b42.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB42> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>(list);
			newList.Sort(ReverseComparer_B42_b42.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB42> ArrayGroupBy(List<GRGEN_MODEL.IB42> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB42>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB42>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b42)) {
					seenValues[list[pos].@b42].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB42> tempList = new List<GRGEN_MODEL.IB42>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b42, tempList);
				}
			}
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>();
			foreach(List<GRGEN_MODEL.IB42> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB42> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB42 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b42)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b42, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB42> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB42 entry in list)
				resultList.Add(entry.@b42);
			return resultList;
		}
	}


	// *** Node B43 ***

	public interface IB43 : IA4
	{
	}

	public sealed partial class @B43 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB43
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@B43[] pool;

		// explicit initializations of A4 for target B43
		// implicit initializations of A4 for target B43
		// explicit initializations of B43 for target B43
		// implicit initializations of B43 for target B43
		static @B43() {
		}

		public @B43() : base(GRGEN_MODEL.NodeType_B43.typeVar)
		{
			// implicit initialization, container creation of B43
			// explicit initializations of A4 for target B43
			// explicit initializations of B43 for target B43
		}

		public static GRGEN_MODEL.NodeType_B43 TypeInstance { get { return GRGEN_MODEL.NodeType_B43.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@B43(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@B43(this, graph, oldToNewObjectMap);
		}

		private @B43(GRGEN_MODEL.@B43 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_B43.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @B43))
				return false;
			@B43 that_ = (@B43)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@B43 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B43 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B43();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B43[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B43
				node.@a4 = 0;
				// explicit initializations of A4 for target B43
				// explicit initializations of B43 for target B43
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@B43 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@B43 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B43();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@B43[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of B43
				node.@a4 = 0;
				// explicit initializations of A4 for target B43
				// explicit initializations of B43 for target B43
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@B43[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return this.@a4;
			}
			throw new NullReferenceException(
				"The Node type \"B43\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"B43\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of B43
			this.@a4 = 0;
			// explicit initializations of A4 for target B43
			// explicit initializations of B43 for target B43
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B43 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("B43 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_B43 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_B43 typeVar = new GRGEN_MODEL.NodeType_B43();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, true, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_B43() : base((int) NodeTypes.@B43)
		{
		}
		public override string Name { get { return "B43"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "B43"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IB43"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@B43"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@B43();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@B43 newNode = new GRGEN_MODEL.@B43();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@B42:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B43:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: B43
					{
						GRGEN_MODEL.IB43 old = (GRGEN_MODEL.IB43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_B43_a4 : Comparer<GRGEN_MODEL.IB43>
	{
		public static Comparer_B43_a4 thisComparer = new Comparer_B43_a4();
		public override int Compare(GRGEN_MODEL.IB43 a, GRGEN_MODEL.IB43 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_B43_a4 : Comparer<GRGEN_MODEL.IB43>
	{
		public static ReverseComparer_B43_a4 thisComparer = new ReverseComparer_B43_a4();
		public override int Compare(GRGEN_MODEL.IB43 b, GRGEN_MODEL.IB43 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_B43_a4
	{
		private static GRGEN_MODEL.IB43 instanceBearingAttributeForSearch = new GRGEN_MODEL.@B43();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IB43> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_B43_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.IB43> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB43> list)
		{
			List<GRGEN_MODEL.IB43> newList = new List<GRGEN_MODEL.IB43>(list);
			newList.Sort(Comparer_B43_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB43> ArrayOrderDescendingBy(List<GRGEN_MODEL.IB43> list)
		{
			List<GRGEN_MODEL.IB43> newList = new List<GRGEN_MODEL.IB43>(list);
			newList.Sort(ReverseComparer_B43_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IB43> ArrayGroupBy(List<GRGEN_MODEL.IB43> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IB43>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IB43>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IB43> tempList = new List<GRGEN_MODEL.IB43>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.IB43> newList = new List<GRGEN_MODEL.IB43>();
			foreach(List<GRGEN_MODEL.IB43> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IB43> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IB43> list)
		{
			List<GRGEN_MODEL.IB43> newList = new List<GRGEN_MODEL.IB43>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IB43 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IB43> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IB43 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	// *** Node C221 ***

	public interface IC221 : IB22
	{
		int @c221 { get; set; }
	}

	public sealed partial class @C221 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC221
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@C221[] pool;

		// explicit initializations of A2 for target C221
		// implicit initializations of A2 for target C221
		// explicit initializations of B22 for target C221
		// implicit initializations of B22 for target C221
		// explicit initializations of C221 for target C221
		// implicit initializations of C221 for target C221
		static @C221() {
		}

		public @C221() : base(GRGEN_MODEL.NodeType_C221.typeVar)
		{
			// implicit initialization, container creation of C221
			// explicit initializations of A2 for target C221
			// explicit initializations of B22 for target C221
			// explicit initializations of C221 for target C221
		}

		public static GRGEN_MODEL.NodeType_C221 TypeInstance { get { return GRGEN_MODEL.NodeType_C221.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@C221(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@C221(this, graph, oldToNewObjectMap);
		}

		private @C221(GRGEN_MODEL.@C221 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_C221.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
			c221_M0no_suXx_h4rD = oldElem.c221_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @C221))
				return false;
			@C221 that_ = (@C221)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& c221_M0no_suXx_h4rD == that_.c221_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@C221 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C221 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C221();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C221[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C221
				node.@a2 = 0;
				node.@b22 = 0;
				node.@c221 = 0;
				// explicit initializations of A2 for target C221
				// explicit initializations of B22 for target C221
				// explicit initializations of C221 for target C221
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@C221 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@C221 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C221();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C221[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C221
				node.@a2 = 0;
				node.@b22 = 0;
				node.@c221 = 0;
				// explicit initializations of A2 for target C221
				// explicit initializations of B22 for target C221
				// explicit initializations of C221 for target C221
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@C221[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b22_M0no_suXx_h4rD;
		public int @b22
		{
			get { return b22_M0no_suXx_h4rD; }
			set { b22_M0no_suXx_h4rD = value; }
		}

		private int c221_M0no_suXx_h4rD;
		public int @c221
		{
			get { return c221_M0no_suXx_h4rD; }
			set { c221_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
				case "b22": return this.@b22;
				case "c221": return this.@c221;
			}
			throw new NullReferenceException(
				"The Node type \"C221\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b22": this.@b22 = (int) value; return;
				case "c221": this.@c221 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"C221\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of C221
			this.@a2 = 0;
			this.@b22 = 0;
			this.@c221 = 0;
			// explicit initializations of A2 for target C221
			// explicit initializations of B22 for target C221
			// explicit initializations of C221 for target C221
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C221 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C221 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_C221 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_C221 typeVar = new GRGEN_MODEL.NodeType_C221();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, true, false, false, false, false, true, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_c221;
		public NodeType_C221() : base((int) NodeTypes.@C221)
		{
			AttributeType_c221 = new GRGEN_LIBGR.AttributeType("c221", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "C221"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "C221"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IC221"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@C221"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@C221();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				yield return AttributeType_c221;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b22" : return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				case "c221" : return AttributeType_c221;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@C221 newNode = new GRGEN_MODEL.@C221();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B23:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
					// copy attributes for: B22
					{
						GRGEN_MODEL.IB22 old = (GRGEN_MODEL.IB22) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: C221
					{
						GRGEN_MODEL.IC221 old = (GRGEN_MODEL.IC221) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@c221 = old.@c221;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_C221_a2 : Comparer<GRGEN_MODEL.IC221>
	{
		public static Comparer_C221_a2 thisComparer = new Comparer_C221_a2();
		public override int Compare(GRGEN_MODEL.IC221 a, GRGEN_MODEL.IC221 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_C221_a2 : Comparer<GRGEN_MODEL.IC221>
	{
		public static ReverseComparer_C221_a2 thisComparer = new ReverseComparer_C221_a2();
		public override int Compare(GRGEN_MODEL.IC221 b, GRGEN_MODEL.IC221 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_C221_a2
	{
		private static GRGEN_MODEL.IC221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC221> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C221_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(Comparer_C221_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(ReverseComparer_C221_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayGroupBy(List<GRGEN_MODEL.IC221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC221> tempList = new List<GRGEN_MODEL.IC221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>();
			foreach(List<GRGEN_MODEL.IC221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC221 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	public class Comparer_C221_b22 : Comparer<GRGEN_MODEL.IC221>
	{
		public static Comparer_C221_b22 thisComparer = new Comparer_C221_b22();
		public override int Compare(GRGEN_MODEL.IC221 a, GRGEN_MODEL.IC221 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ReverseComparer_C221_b22 : Comparer<GRGEN_MODEL.IC221>
	{
		public static ReverseComparer_C221_b22 thisComparer = new ReverseComparer_C221_b22();
		public override int Compare(GRGEN_MODEL.IC221 b, GRGEN_MODEL.IC221 a)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ArrayHelper_C221_b22
	{
		private static GRGEN_MODEL.IC221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC221> list, int entry)
		{
			instanceBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C221_b22.thisComparer);
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(Comparer_C221_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(ReverseComparer_C221_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayGroupBy(List<GRGEN_MODEL.IC221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b22)) {
					seenValues[list[pos].@b22].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC221> tempList = new List<GRGEN_MODEL.IC221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b22, tempList);
				}
			}
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>();
			foreach(List<GRGEN_MODEL.IC221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b22)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b22, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC221 entry in list)
				resultList.Add(entry.@b22);
			return resultList;
		}
	}


	public class Comparer_C221_c221 : Comparer<GRGEN_MODEL.IC221>
	{
		public static Comparer_C221_c221 thisComparer = new Comparer_C221_c221();
		public override int Compare(GRGEN_MODEL.IC221 a, GRGEN_MODEL.IC221 b)
		{
			return a.@c221.CompareTo(b.@c221);
		}
	}

	public class ReverseComparer_C221_c221 : Comparer<GRGEN_MODEL.IC221>
	{
		public static ReverseComparer_C221_c221 thisComparer = new ReverseComparer_C221_c221();
		public override int Compare(GRGEN_MODEL.IC221 b, GRGEN_MODEL.IC221 a)
		{
			return a.@c221.CompareTo(b.@c221);
		}
	}

	public class ArrayHelper_C221_c221
	{
		private static GRGEN_MODEL.IC221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC221> list, int entry)
		{
			instanceBearingAttributeForSearch.@c221 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C221_c221.thisComparer);
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(Comparer_C221_c221.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(ReverseComparer_C221_c221.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayGroupBy(List<GRGEN_MODEL.IC221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@c221)) {
					seenValues[list[pos].@c221].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC221> tempList = new List<GRGEN_MODEL.IC221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@c221, tempList);
				}
			}
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>();
			foreach(List<GRGEN_MODEL.IC221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@c221)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@c221, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC221 entry in list)
				resultList.Add(entry.@c221);
			return resultList;
		}
	}


	// *** Node C222_411 ***

	public interface IC222_411 : IB22, IB41
	{
		int @c222_411 { get; set; }
	}

	public sealed partial class @C222_411 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC222_411
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@C222_411[] pool;

		// explicit initializations of A2 for target C222_411
		// implicit initializations of A2 for target C222_411
		// explicit initializations of B22 for target C222_411
		// implicit initializations of B22 for target C222_411
		// explicit initializations of A4 for target C222_411
		// implicit initializations of A4 for target C222_411
		// explicit initializations of B41 for target C222_411
		// implicit initializations of B41 for target C222_411
		// explicit initializations of C222_411 for target C222_411
		// implicit initializations of C222_411 for target C222_411
		static @C222_411() {
		}

		public @C222_411() : base(GRGEN_MODEL.NodeType_C222_411.typeVar)
		{
			// implicit initialization, container creation of C222_411
			// explicit initializations of A2 for target C222_411
			// explicit initializations of B22 for target C222_411
			// explicit initializations of A4 for target C222_411
			// explicit initializations of B41 for target C222_411
			// explicit initializations of C222_411 for target C222_411
		}

		public static GRGEN_MODEL.NodeType_C222_411 TypeInstance { get { return GRGEN_MODEL.NodeType_C222_411.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@C222_411(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@C222_411(this, graph, oldToNewObjectMap);
		}

		private @C222_411(GRGEN_MODEL.@C222_411 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_C222_411.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			c222_411_M0no_suXx_h4rD = oldElem.c222_411_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @C222_411))
				return false;
			@C222_411 that_ = (@C222_411)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& c222_411_M0no_suXx_h4rD == that_.c222_411_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@C222_411 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C222_411 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C222_411();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C222_411[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C222_411
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				// explicit initializations of A2 for target C222_411
				// explicit initializations of B22 for target C222_411
				// explicit initializations of A4 for target C222_411
				// explicit initializations of B41 for target C222_411
				// explicit initializations of C222_411 for target C222_411
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@C222_411 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@C222_411 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C222_411();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C222_411[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C222_411
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				// explicit initializations of A2 for target C222_411
				// explicit initializations of B22 for target C222_411
				// explicit initializations of A4 for target C222_411
				// explicit initializations of B41 for target C222_411
				// explicit initializations of C222_411 for target C222_411
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@C222_411[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b22_M0no_suXx_h4rD;
		public int @b22
		{
			get { return b22_M0no_suXx_h4rD; }
			set { b22_M0no_suXx_h4rD = value; }
		}

		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b41_M0no_suXx_h4rD;
		public int @b41
		{
			get { return b41_M0no_suXx_h4rD; }
			set { b41_M0no_suXx_h4rD = value; }
		}

		private int c222_411_M0no_suXx_h4rD;
		public int @c222_411
		{
			get { return c222_411_M0no_suXx_h4rD; }
			set { c222_411_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
				case "b22": return this.@b22;
				case "a4": return this.@a4;
				case "b41": return this.@b41;
				case "c222_411": return this.@c222_411;
			}
			throw new NullReferenceException(
				"The Node type \"C222_411\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b22": this.@b22 = (int) value; return;
				case "a4": this.@a4 = (int) value; return;
				case "b41": this.@b41 = (int) value; return;
				case "c222_411": this.@c222_411 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"C222_411\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of C222_411
			this.@a2 = 0;
			this.@b22 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
			this.@c222_411 = 0;
			// explicit initializations of A2 for target C222_411
			// explicit initializations of B22 for target C222_411
			// explicit initializations of A4 for target C222_411
			// explicit initializations of B41 for target C222_411
			// explicit initializations of C222_411 for target C222_411
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C222_411 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C222_411 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_C222_411 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_C222_411 typeVar = new GRGEN_MODEL.NodeType_C222_411();
		public static bool[] isA = new bool[] { true, false, true, false, true, false, false, true, false, true, false, false, false, true, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_c222_411;
		public NodeType_C222_411() : base((int) NodeTypes.@C222_411)
		{
			AttributeType_c222_411 = new GRGEN_LIBGR.AttributeType("c222_411", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "C222_411"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "C222_411"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IC222_411"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@C222_411"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@C222_411();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 5; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				yield return AttributeType_c222_411;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b22" : return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b41" : return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				case "c222_411" : return AttributeType_c222_411;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@C222_411 newNode = new GRGEN_MODEL.@C222_411();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B23:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B42:
				case (int) GRGEN_MODEL.NodeTypes.@B43:
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@C221:
					// copy attributes for: B22
					{
						GRGEN_MODEL.IB22 old = (GRGEN_MODEL.IB22) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: C222_411
					{
						GRGEN_MODEL.IC222_411 old = (GRGEN_MODEL.IC222_411) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@c222_411 = old.@c222_411;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_C222_411_a2 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static Comparer_C222_411_a2 thisComparer = new Comparer_C222_411_a2();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_C222_411_a2 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static ReverseComparer_C222_411_a2 thisComparer = new ReverseComparer_C222_411_a2();
		public override int Compare(GRGEN_MODEL.IC222_411 b, GRGEN_MODEL.IC222_411 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_C222_411_a2
	{
		private static GRGEN_MODEL.IC222_411 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C222_411_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(Comparer_C222_411_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(ReverseComparer_C222_411_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayGroupBy(List<GRGEN_MODEL.IC222_411> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC222_411>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC222_411>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC222_411> tempList = new List<GRGEN_MODEL.IC222_411>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			foreach(List<GRGEN_MODEL.IC222_411> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC222_411 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC222_411> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC222_411 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	public class Comparer_C222_411_b22 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static Comparer_C222_411_b22 thisComparer = new Comparer_C222_411_b22();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ReverseComparer_C222_411_b22 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static ReverseComparer_C222_411_b22 thisComparer = new ReverseComparer_C222_411_b22();
		public override int Compare(GRGEN_MODEL.IC222_411 b, GRGEN_MODEL.IC222_411 a)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ArrayHelper_C222_411_b22
	{
		private static GRGEN_MODEL.IC222_411 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			instanceBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C222_411_b22.thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(Comparer_C222_411_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(ReverseComparer_C222_411_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayGroupBy(List<GRGEN_MODEL.IC222_411> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC222_411>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC222_411>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b22)) {
					seenValues[list[pos].@b22].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC222_411> tempList = new List<GRGEN_MODEL.IC222_411>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b22, tempList);
				}
			}
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			foreach(List<GRGEN_MODEL.IC222_411> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC222_411 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b22)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b22, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC222_411> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC222_411 entry in list)
				resultList.Add(entry.@b22);
			return resultList;
		}
	}


	public class Comparer_C222_411_a4 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static Comparer_C222_411_a4 thisComparer = new Comparer_C222_411_a4();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_C222_411_a4 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static ReverseComparer_C222_411_a4 thisComparer = new ReverseComparer_C222_411_a4();
		public override int Compare(GRGEN_MODEL.IC222_411 b, GRGEN_MODEL.IC222_411 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_C222_411_a4
	{
		private static GRGEN_MODEL.IC222_411 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C222_411_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(Comparer_C222_411_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(ReverseComparer_C222_411_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayGroupBy(List<GRGEN_MODEL.IC222_411> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC222_411>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC222_411>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC222_411> tempList = new List<GRGEN_MODEL.IC222_411>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			foreach(List<GRGEN_MODEL.IC222_411> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC222_411 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC222_411> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC222_411 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	public class Comparer_C222_411_b41 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static Comparer_C222_411_b41 thisComparer = new Comparer_C222_411_b41();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ReverseComparer_C222_411_b41 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static ReverseComparer_C222_411_b41 thisComparer = new ReverseComparer_C222_411_b41();
		public override int Compare(GRGEN_MODEL.IC222_411 b, GRGEN_MODEL.IC222_411 a)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ArrayHelper_C222_411_b41
	{
		private static GRGEN_MODEL.IC222_411 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			instanceBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C222_411_b41.thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(Comparer_C222_411_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(ReverseComparer_C222_411_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayGroupBy(List<GRGEN_MODEL.IC222_411> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC222_411>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC222_411>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b41)) {
					seenValues[list[pos].@b41].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC222_411> tempList = new List<GRGEN_MODEL.IC222_411>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b41, tempList);
				}
			}
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			foreach(List<GRGEN_MODEL.IC222_411> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC222_411 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b41)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b41, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC222_411> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC222_411 entry in list)
				resultList.Add(entry.@b41);
			return resultList;
		}
	}


	public class Comparer_C222_411_c222_411 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static Comparer_C222_411_c222_411 thisComparer = new Comparer_C222_411_c222_411();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
	}

	public class ReverseComparer_C222_411_c222_411 : Comparer<GRGEN_MODEL.IC222_411>
	{
		public static ReverseComparer_C222_411_c222_411 thisComparer = new ReverseComparer_C222_411_c222_411();
		public override int Compare(GRGEN_MODEL.IC222_411 b, GRGEN_MODEL.IC222_411 a)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
	}

	public class ArrayHelper_C222_411_c222_411
	{
		private static GRGEN_MODEL.IC222_411 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			instanceBearingAttributeForSearch.@c222_411 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C222_411_c222_411.thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(Comparer_C222_411_c222_411.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(ReverseComparer_C222_411_c222_411.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayGroupBy(List<GRGEN_MODEL.IC222_411> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC222_411>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC222_411>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@c222_411)) {
					seenValues[list[pos].@c222_411].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC222_411> tempList = new List<GRGEN_MODEL.IC222_411>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@c222_411, tempList);
				}
			}
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			foreach(List<GRGEN_MODEL.IC222_411> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC222_411 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@c222_411)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@c222_411, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC222_411> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC222_411 entry in list)
				resultList.Add(entry.@c222_411);
			return resultList;
		}
	}


	// *** Node C412_421_431_51 ***

	public interface IC412_421_431_51 : IB41, IB42, IB43, IA5
	{
	}

	public sealed partial class @C412_421_431_51 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC412_421_431_51
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@C412_421_431_51[] pool;

		// explicit initializations of A4 for target C412_421_431_51
		// implicit initializations of A4 for target C412_421_431_51
		// explicit initializations of B41 for target C412_421_431_51
		// implicit initializations of B41 for target C412_421_431_51
		// explicit initializations of B42 for target C412_421_431_51
		// implicit initializations of B42 for target C412_421_431_51
		// explicit initializations of B43 for target C412_421_431_51
		// implicit initializations of B43 for target C412_421_431_51
		// explicit initializations of A5 for target C412_421_431_51
		// implicit initializations of A5 for target C412_421_431_51
		// explicit initializations of C412_421_431_51 for target C412_421_431_51
		// implicit initializations of C412_421_431_51 for target C412_421_431_51
		static @C412_421_431_51() {
		}

		public @C412_421_431_51() : base(GRGEN_MODEL.NodeType_C412_421_431_51.typeVar)
		{
			// implicit initialization, container creation of C412_421_431_51
			// explicit initializations of A4 for target C412_421_431_51
			// explicit initializations of B41 for target C412_421_431_51
			// explicit initializations of B42 for target C412_421_431_51
			// explicit initializations of B43 for target C412_421_431_51
			// explicit initializations of A5 for target C412_421_431_51
			// explicit initializations of C412_421_431_51 for target C412_421_431_51
		}

		public static GRGEN_MODEL.NodeType_C412_421_431_51 TypeInstance { get { return GRGEN_MODEL.NodeType_C412_421_431_51.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@C412_421_431_51(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@C412_421_431_51(this, graph, oldToNewObjectMap);
		}

		private @C412_421_431_51(GRGEN_MODEL.@C412_421_431_51 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_C412_421_431_51.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
			a5_M0no_suXx_h4rD = oldElem.a5_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @C412_421_431_51))
				return false;
			@C412_421_431_51 that_ = (@C412_421_431_51)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
				&& a5_M0no_suXx_h4rD == that_.a5_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@C412_421_431_51 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C412_421_431_51 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C412_421_431_51();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C412_421_431_51[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C412_421_431_51
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
				// explicit initializations of A4 for target C412_421_431_51
				// explicit initializations of B41 for target C412_421_431_51
				// explicit initializations of B42 for target C412_421_431_51
				// explicit initializations of B43 for target C412_421_431_51
				// explicit initializations of A5 for target C412_421_431_51
				// explicit initializations of C412_421_431_51 for target C412_421_431_51
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@C412_421_431_51 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@C412_421_431_51 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C412_421_431_51();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C412_421_431_51[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C412_421_431_51
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
				// explicit initializations of A4 for target C412_421_431_51
				// explicit initializations of B41 for target C412_421_431_51
				// explicit initializations of B42 for target C412_421_431_51
				// explicit initializations of B43 for target C412_421_431_51
				// explicit initializations of A5 for target C412_421_431_51
				// explicit initializations of C412_421_431_51 for target C412_421_431_51
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@C412_421_431_51[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b41_M0no_suXx_h4rD;
		public int @b41
		{
			get { return b41_M0no_suXx_h4rD; }
			set { b41_M0no_suXx_h4rD = value; }
		}

		private int b42_M0no_suXx_h4rD;
		public int @b42
		{
			get { return b42_M0no_suXx_h4rD; }
			set { b42_M0no_suXx_h4rD = value; }
		}

		private int a5_M0no_suXx_h4rD;
		public int @a5
		{
			get { return a5_M0no_suXx_h4rD; }
			set { a5_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return this.@a4;
				case "b41": return this.@b41;
				case "b42": return this.@b42;
				case "a5": return this.@a5;
			}
			throw new NullReferenceException(
				"The Node type \"C412_421_431_51\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
				case "b41": this.@b41 = (int) value; return;
				case "b42": this.@b42 = (int) value; return;
				case "a5": this.@a5 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"C412_421_431_51\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of C412_421_431_51
			this.@a4 = 0;
			this.@b41 = 0;
			this.@b42 = 0;
			this.@a5 = 0;
			// explicit initializations of A4 for target C412_421_431_51
			// explicit initializations of B41 for target C412_421_431_51
			// explicit initializations of B42 for target C412_421_431_51
			// explicit initializations of B43 for target C412_421_431_51
			// explicit initializations of A5 for target C412_421_431_51
			// explicit initializations of C412_421_431_51 for target C412_421_431_51
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C412_421_431_51 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C412_421_431_51 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_C412_421_431_51 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_C412_421_431_51 typeVar = new GRGEN_MODEL.NodeType_C412_421_431_51();
		public static bool[] isA = new bool[] { true, false, false, false, true, true, false, false, false, true, true, true, false, false, true, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_C412_421_431_51() : base((int) NodeTypes.@C412_421_431_51)
		{
		}
		public override string Name { get { return "C412_421_431_51"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "C412_421_431_51"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IC412_421_431_51"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@C412_421_431_51"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@C412_421_431_51();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 4; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				yield return GRGEN_MODEL.NodeType_B42.AttributeType_b42;
				yield return GRGEN_MODEL.NodeType_A5.AttributeType_a5;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b41" : return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				case "b42" : return GRGEN_MODEL.NodeType_B42.AttributeType_b42;
				case "a5" : return GRGEN_MODEL.NodeType_A5.AttributeType_a5;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@C412_421_431_51 newNode = new GRGEN_MODEL.@C412_421_431_51();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A4:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A5:
					// copy attributes for: A5
					{
						GRGEN_MODEL.IA5 old = (GRGEN_MODEL.IA5) oldNode;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B42:
					// copy attributes for: B42
					{
						GRGEN_MODEL.IB42 old = (GRGEN_MODEL.IB42) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B43:
					// copy attributes for: B43
					{
						GRGEN_MODEL.IB43 old = (GRGEN_MODEL.IB43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: C412_421_431_51
					{
						GRGEN_MODEL.IC412_421_431_51 old = (GRGEN_MODEL.IC412_421_431_51) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@b42 = old.@b42;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
					// copy attributes for: B42
					{
						GRGEN_MODEL.IB42 old = (GRGEN_MODEL.IB42) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					// copy attributes for: B43
						// already copied: a4
					break;
			}
			return newNode;
		}

	}

	public class Comparer_C412_421_431_51_a4 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static Comparer_C412_421_431_51_a4 thisComparer = new Comparer_C412_421_431_51_a4();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_C412_421_431_51_a4 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static ReverseComparer_C412_421_431_51_a4 thisComparer = new ReverseComparer_C412_421_431_51_a4();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 b, GRGEN_MODEL.IC412_421_431_51 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_C412_421_431_51_a4
	{
		private static GRGEN_MODEL.IC412_421_431_51 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C412_421_431_51_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(Comparer_C412_421_431_51_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(ReverseComparer_C412_421_431_51_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayGroupBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC412_421_431_51> tempList = new List<GRGEN_MODEL.IC412_421_431_51>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			foreach(List<GRGEN_MODEL.IC412_421_431_51> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC412_421_431_51 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC412_421_431_51 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	public class Comparer_C412_421_431_51_b41 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static Comparer_C412_421_431_51_b41 thisComparer = new Comparer_C412_421_431_51_b41();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ReverseComparer_C412_421_431_51_b41 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static ReverseComparer_C412_421_431_51_b41 thisComparer = new ReverseComparer_C412_421_431_51_b41();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 b, GRGEN_MODEL.IC412_421_431_51 a)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ArrayHelper_C412_421_431_51_b41
	{
		private static GRGEN_MODEL.IC412_421_431_51 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			instanceBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C412_421_431_51_b41.thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(Comparer_C412_421_431_51_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(ReverseComparer_C412_421_431_51_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayGroupBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b41)) {
					seenValues[list[pos].@b41].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC412_421_431_51> tempList = new List<GRGEN_MODEL.IC412_421_431_51>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b41, tempList);
				}
			}
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			foreach(List<GRGEN_MODEL.IC412_421_431_51> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC412_421_431_51 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b41)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b41, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC412_421_431_51 entry in list)
				resultList.Add(entry.@b41);
			return resultList;
		}
	}


	public class Comparer_C412_421_431_51_b42 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static Comparer_C412_421_431_51_b42 thisComparer = new Comparer_C412_421_431_51_b42();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@b42.CompareTo(b.@b42);
		}
	}

	public class ReverseComparer_C412_421_431_51_b42 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static ReverseComparer_C412_421_431_51_b42 thisComparer = new ReverseComparer_C412_421_431_51_b42();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 b, GRGEN_MODEL.IC412_421_431_51 a)
		{
			return a.@b42.CompareTo(b.@b42);
		}
	}

	public class ArrayHelper_C412_421_431_51_b42
	{
		private static GRGEN_MODEL.IC412_421_431_51 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			instanceBearingAttributeForSearch.@b42 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C412_421_431_51_b42.thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(Comparer_C412_421_431_51_b42.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(ReverseComparer_C412_421_431_51_b42.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayGroupBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b42)) {
					seenValues[list[pos].@b42].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC412_421_431_51> tempList = new List<GRGEN_MODEL.IC412_421_431_51>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b42, tempList);
				}
			}
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			foreach(List<GRGEN_MODEL.IC412_421_431_51> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC412_421_431_51 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b42)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b42, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC412_421_431_51 entry in list)
				resultList.Add(entry.@b42);
			return resultList;
		}
	}


	public class Comparer_C412_421_431_51_a5 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static Comparer_C412_421_431_51_a5 thisComparer = new Comparer_C412_421_431_51_a5();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@a5.CompareTo(b.@a5);
		}
	}

	public class ReverseComparer_C412_421_431_51_a5 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		public static ReverseComparer_C412_421_431_51_a5 thisComparer = new ReverseComparer_C412_421_431_51_a5();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 b, GRGEN_MODEL.IC412_421_431_51 a)
		{
			return a.@a5.CompareTo(b.@a5);
		}
	}

	public class ArrayHelper_C412_421_431_51_a5
	{
		private static GRGEN_MODEL.IC412_421_431_51 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			instanceBearingAttributeForSearch.@a5 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C412_421_431_51_a5.thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(Comparer_C412_421_431_51_a5.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(ReverseComparer_C412_421_431_51_a5.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayGroupBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC412_421_431_51>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a5)) {
					seenValues[list[pos].@a5].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC412_421_431_51> tempList = new List<GRGEN_MODEL.IC412_421_431_51>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a5, tempList);
				}
			}
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			foreach(List<GRGEN_MODEL.IC412_421_431_51> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC412_421_431_51 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a5)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a5, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC412_421_431_51 entry in list)
				resultList.Add(entry.@a5);
			return resultList;
		}
	}


	// *** Node C432_422 ***

	public interface IC432_422 : IB43, IB42
	{
		int @c432_422 { get; set; }
	}

	public sealed partial class @C432_422 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC432_422
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@C432_422[] pool;

		// explicit initializations of A4 for target C432_422
		// implicit initializations of A4 for target C432_422
		// explicit initializations of B43 for target C432_422
		// implicit initializations of B43 for target C432_422
		// explicit initializations of B42 for target C432_422
		// implicit initializations of B42 for target C432_422
		// explicit initializations of C432_422 for target C432_422
		// implicit initializations of C432_422 for target C432_422
		static @C432_422() {
		}

		public @C432_422() : base(GRGEN_MODEL.NodeType_C432_422.typeVar)
		{
			// implicit initialization, container creation of C432_422
			// explicit initializations of A4 for target C432_422
			// explicit initializations of B43 for target C432_422
			// explicit initializations of B42 for target C432_422
			// explicit initializations of C432_422 for target C432_422
		}

		public static GRGEN_MODEL.NodeType_C432_422 TypeInstance { get { return GRGEN_MODEL.NodeType_C432_422.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@C432_422(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@C432_422(this, graph, oldToNewObjectMap);
		}

		private @C432_422(GRGEN_MODEL.@C432_422 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_C432_422.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
			c432_422_M0no_suXx_h4rD = oldElem.c432_422_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @C432_422))
				return false;
			@C432_422 that_ = (@C432_422)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
				&& c432_422_M0no_suXx_h4rD == that_.c432_422_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@C432_422 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C432_422 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C432_422();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C432_422[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C432_422
				node.@a4 = 0;
				node.@b42 = 0;
				node.@c432_422 = 0;
				// explicit initializations of A4 for target C432_422
				// explicit initializations of B43 for target C432_422
				// explicit initializations of B42 for target C432_422
				// explicit initializations of C432_422 for target C432_422
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@C432_422 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@C432_422 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C432_422();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@C432_422[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of C432_422
				node.@a4 = 0;
				node.@b42 = 0;
				node.@c432_422 = 0;
				// explicit initializations of A4 for target C432_422
				// explicit initializations of B43 for target C432_422
				// explicit initializations of B42 for target C432_422
				// explicit initializations of C432_422 for target C432_422
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@C432_422[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b42_M0no_suXx_h4rD;
		public int @b42
		{
			get { return b42_M0no_suXx_h4rD; }
			set { b42_M0no_suXx_h4rD = value; }
		}

		private int c432_422_M0no_suXx_h4rD;
		public int @c432_422
		{
			get { return c432_422_M0no_suXx_h4rD; }
			set { c432_422_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return this.@a4;
				case "b42": return this.@b42;
				case "c432_422": return this.@c432_422;
			}
			throw new NullReferenceException(
				"The Node type \"C432_422\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
				case "b42": this.@b42 = (int) value; return;
				case "c432_422": this.@c432_422 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"C432_422\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of C432_422
			this.@a4 = 0;
			this.@b42 = 0;
			this.@c432_422 = 0;
			// explicit initializations of A4 for target C432_422
			// explicit initializations of B43 for target C432_422
			// explicit initializations of B42 for target C432_422
			// explicit initializations of C432_422 for target C432_422
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C432_422 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("C432_422 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_C432_422 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_C432_422 typeVar = new GRGEN_MODEL.NodeType_C432_422();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, true, true, false, false, false, true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_c432_422;
		public NodeType_C432_422() : base((int) NodeTypes.@C432_422)
		{
			AttributeType_c432_422 = new GRGEN_LIBGR.AttributeType("c432_422", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "C432_422"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "C432_422"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IC432_422"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@C432_422"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@C432_422();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return GRGEN_MODEL.NodeType_B42.AttributeType_b42;
				yield return AttributeType_c432_422;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b42" : return GRGEN_MODEL.NodeType_B42.AttributeType_b42;
				case "c432_422" : return AttributeType_c432_422;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@C432_422 newNode = new GRGEN_MODEL.@C432_422();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B42:
					// copy attributes for: B42
					{
						GRGEN_MODEL.IB42 old = (GRGEN_MODEL.IB42) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B43:
					// copy attributes for: B43
					{
						GRGEN_MODEL.IB43 old = (GRGEN_MODEL.IB43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: B42
					{
						GRGEN_MODEL.IB42 old = (GRGEN_MODEL.IB42) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					// copy attributes for: B43
						// already copied: a4
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
					// copy attributes for: C432_422
					{
						GRGEN_MODEL.IC432_422 old = (GRGEN_MODEL.IC432_422) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
						newNode.@c432_422 = old.@c432_422;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_C432_422_a4 : Comparer<GRGEN_MODEL.IC432_422>
	{
		public static Comparer_C432_422_a4 thisComparer = new Comparer_C432_422_a4();
		public override int Compare(GRGEN_MODEL.IC432_422 a, GRGEN_MODEL.IC432_422 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_C432_422_a4 : Comparer<GRGEN_MODEL.IC432_422>
	{
		public static ReverseComparer_C432_422_a4 thisComparer = new ReverseComparer_C432_422_a4();
		public override int Compare(GRGEN_MODEL.IC432_422 b, GRGEN_MODEL.IC432_422 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_C432_422_a4
	{
		private static GRGEN_MODEL.IC432_422 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C432_422();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC432_422> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C432_422_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(Comparer_C432_422_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(ReverseComparer_C432_422_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayGroupBy(List<GRGEN_MODEL.IC432_422> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC432_422>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC432_422>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC432_422> tempList = new List<GRGEN_MODEL.IC432_422>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>();
			foreach(List<GRGEN_MODEL.IC432_422> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC432_422 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC432_422> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC432_422 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	public class Comparer_C432_422_b42 : Comparer<GRGEN_MODEL.IC432_422>
	{
		public static Comparer_C432_422_b42 thisComparer = new Comparer_C432_422_b42();
		public override int Compare(GRGEN_MODEL.IC432_422 a, GRGEN_MODEL.IC432_422 b)
		{
			return a.@b42.CompareTo(b.@b42);
		}
	}

	public class ReverseComparer_C432_422_b42 : Comparer<GRGEN_MODEL.IC432_422>
	{
		public static ReverseComparer_C432_422_b42 thisComparer = new ReverseComparer_C432_422_b42();
		public override int Compare(GRGEN_MODEL.IC432_422 b, GRGEN_MODEL.IC432_422 a)
		{
			return a.@b42.CompareTo(b.@b42);
		}
	}

	public class ArrayHelper_C432_422_b42
	{
		private static GRGEN_MODEL.IC432_422 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C432_422();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC432_422> list, int entry)
		{
			instanceBearingAttributeForSearch.@b42 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C432_422_b42.thisComparer);
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(Comparer_C432_422_b42.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(ReverseComparer_C432_422_b42.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayGroupBy(List<GRGEN_MODEL.IC432_422> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC432_422>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC432_422>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b42)) {
					seenValues[list[pos].@b42].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC432_422> tempList = new List<GRGEN_MODEL.IC432_422>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b42, tempList);
				}
			}
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>();
			foreach(List<GRGEN_MODEL.IC432_422> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC432_422 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b42)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b42, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC432_422> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC432_422 entry in list)
				resultList.Add(entry.@b42);
			return resultList;
		}
	}


	public class Comparer_C432_422_c432_422 : Comparer<GRGEN_MODEL.IC432_422>
	{
		public static Comparer_C432_422_c432_422 thisComparer = new Comparer_C432_422_c432_422();
		public override int Compare(GRGEN_MODEL.IC432_422 a, GRGEN_MODEL.IC432_422 b)
		{
			return a.@c432_422.CompareTo(b.@c432_422);
		}
	}

	public class ReverseComparer_C432_422_c432_422 : Comparer<GRGEN_MODEL.IC432_422>
	{
		public static ReverseComparer_C432_422_c432_422 thisComparer = new ReverseComparer_C432_422_c432_422();
		public override int Compare(GRGEN_MODEL.IC432_422 b, GRGEN_MODEL.IC432_422 a)
		{
			return a.@c432_422.CompareTo(b.@c432_422);
		}
	}

	public class ArrayHelper_C432_422_c432_422
	{
		private static GRGEN_MODEL.IC432_422 instanceBearingAttributeForSearch = new GRGEN_MODEL.@C432_422();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IC432_422> list, int entry)
		{
			instanceBearingAttributeForSearch.@c432_422 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_C432_422_c432_422.thisComparer);
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(Comparer_C432_422_c432_422.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderDescendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(ReverseComparer_C432_422_c432_422.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayGroupBy(List<GRGEN_MODEL.IC432_422> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IC432_422>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IC432_422>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@c432_422)) {
					seenValues[list[pos].@c432_422].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IC432_422> tempList = new List<GRGEN_MODEL.IC432_422>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@c432_422, tempList);
				}
			}
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>();
			foreach(List<GRGEN_MODEL.IC432_422> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IC432_422 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@c432_422)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@c432_422, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IC432_422> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IC432_422 entry in list)
				resultList.Add(entry.@c432_422);
			return resultList;
		}
	}


	// *** Node D11_2221 ***

	public interface ID11_2221 : IA1, IC222_411
	{
		int @d11_2221 { get; set; }
	}

	public sealed partial class @D11_2221 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ID11_2221
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@D11_2221[] pool;

		// explicit initializations of A1 for target D11_2221
		// implicit initializations of A1 for target D11_2221
		// explicit initializations of A2 for target D11_2221
		// implicit initializations of A2 for target D11_2221
		// explicit initializations of B22 for target D11_2221
		// implicit initializations of B22 for target D11_2221
		// explicit initializations of A4 for target D11_2221
		// implicit initializations of A4 for target D11_2221
		// explicit initializations of B41 for target D11_2221
		// implicit initializations of B41 for target D11_2221
		// explicit initializations of C222_411 for target D11_2221
		// implicit initializations of C222_411 for target D11_2221
		// explicit initializations of D11_2221 for target D11_2221
		// implicit initializations of D11_2221 for target D11_2221
		static @D11_2221() {
		}

		public @D11_2221() : base(GRGEN_MODEL.NodeType_D11_2221.typeVar)
		{
			// implicit initialization, container creation of D11_2221
			// explicit initializations of A1 for target D11_2221
			// explicit initializations of A2 for target D11_2221
			// explicit initializations of B22 for target D11_2221
			// explicit initializations of A4 for target D11_2221
			// explicit initializations of B41 for target D11_2221
			// explicit initializations of C222_411 for target D11_2221
			// explicit initializations of D11_2221 for target D11_2221
		}

		public static GRGEN_MODEL.NodeType_D11_2221 TypeInstance { get { return GRGEN_MODEL.NodeType_D11_2221.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@D11_2221(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@D11_2221(this, graph, oldToNewObjectMap);
		}

		private @D11_2221(GRGEN_MODEL.@D11_2221 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_D11_2221.typeVar)
		{
			a1_M0no_suXx_h4rD = oldElem.a1_M0no_suXx_h4rD;
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			c222_411_M0no_suXx_h4rD = oldElem.c222_411_M0no_suXx_h4rD;
			d11_2221_M0no_suXx_h4rD = oldElem.d11_2221_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @D11_2221))
				return false;
			@D11_2221 that_ = (@D11_2221)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a1_M0no_suXx_h4rD == that_.a1_M0no_suXx_h4rD
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& c222_411_M0no_suXx_h4rD == that_.c222_411_M0no_suXx_h4rD
				&& d11_2221_M0no_suXx_h4rD == that_.d11_2221_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@D11_2221 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@D11_2221 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@D11_2221();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@D11_2221[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of D11_2221
				node.@a1 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				node.@d11_2221 = 0;
				// explicit initializations of A1 for target D11_2221
				// explicit initializations of A2 for target D11_2221
				// explicit initializations of B22 for target D11_2221
				// explicit initializations of A4 for target D11_2221
				// explicit initializations of B41 for target D11_2221
				// explicit initializations of C222_411 for target D11_2221
				// explicit initializations of D11_2221 for target D11_2221
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@D11_2221 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@D11_2221 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@D11_2221();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@D11_2221[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of D11_2221
				node.@a1 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				node.@d11_2221 = 0;
				// explicit initializations of A1 for target D11_2221
				// explicit initializations of A2 for target D11_2221
				// explicit initializations of B22 for target D11_2221
				// explicit initializations of A4 for target D11_2221
				// explicit initializations of B41 for target D11_2221
				// explicit initializations of C222_411 for target D11_2221
				// explicit initializations of D11_2221 for target D11_2221
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@D11_2221[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a1_M0no_suXx_h4rD;
		public int @a1
		{
			get { return a1_M0no_suXx_h4rD; }
			set { a1_M0no_suXx_h4rD = value; }
		}

		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b22_M0no_suXx_h4rD;
		public int @b22
		{
			get { return b22_M0no_suXx_h4rD; }
			set { b22_M0no_suXx_h4rD = value; }
		}

		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b41_M0no_suXx_h4rD;
		public int @b41
		{
			get { return b41_M0no_suXx_h4rD; }
			set { b41_M0no_suXx_h4rD = value; }
		}

		private int c222_411_M0no_suXx_h4rD;
		public int @c222_411
		{
			get { return c222_411_M0no_suXx_h4rD; }
			set { c222_411_M0no_suXx_h4rD = value; }
		}

		private int d11_2221_M0no_suXx_h4rD;
		public int @d11_2221
		{
			get { return d11_2221_M0no_suXx_h4rD; }
			set { d11_2221_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a1": return this.@a1;
				case "a2": return this.@a2;
				case "b22": return this.@b22;
				case "a4": return this.@a4;
				case "b41": return this.@b41;
				case "c222_411": return this.@c222_411;
				case "d11_2221": return this.@d11_2221;
			}
			throw new NullReferenceException(
				"The Node type \"D11_2221\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a1": this.@a1 = (int) value; return;
				case "a2": this.@a2 = (int) value; return;
				case "b22": this.@b22 = (int) value; return;
				case "a4": this.@a4 = (int) value; return;
				case "b41": this.@b41 = (int) value; return;
				case "c222_411": this.@c222_411 = (int) value; return;
				case "d11_2221": this.@d11_2221 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"D11_2221\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of D11_2221
			this.@a1 = 0;
			this.@a2 = 0;
			this.@b22 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
			this.@c222_411 = 0;
			this.@d11_2221 = 0;
			// explicit initializations of A1 for target D11_2221
			// explicit initializations of A2 for target D11_2221
			// explicit initializations of B22 for target D11_2221
			// explicit initializations of A4 for target D11_2221
			// explicit initializations of B41 for target D11_2221
			// explicit initializations of C222_411 for target D11_2221
			// explicit initializations of D11_2221 for target D11_2221
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("D11_2221 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("D11_2221 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_D11_2221 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_D11_2221 typeVar = new GRGEN_MODEL.NodeType_D11_2221();
		public static bool[] isA = new bool[] { true, true, true, false, true, false, false, true, false, true, false, false, false, true, false, false, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_d11_2221;
		public NodeType_D11_2221() : base((int) NodeTypes.@D11_2221)
		{
			AttributeType_d11_2221 = new GRGEN_LIBGR.AttributeType("d11_2221", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "D11_2221"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "D11_2221"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.ID11_2221"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@D11_2221"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@D11_2221();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 7; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A1.AttributeType_a1;
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				yield return GRGEN_MODEL.NodeType_C222_411.AttributeType_c222_411;
				yield return AttributeType_d11_2221;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a1" : return GRGEN_MODEL.NodeType_A1.AttributeType_a1;
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b22" : return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b41" : return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				case "c222_411" : return GRGEN_MODEL.NodeType_C222_411.AttributeType_c222_411;
				case "d11_2221" : return AttributeType_d11_2221;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@D11_2221 newNode = new GRGEN_MODEL.@D11_2221();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A1:
					// copy attributes for: A1
					{
						GRGEN_MODEL.IA1 old = (GRGEN_MODEL.IA1) oldNode;
						newNode.@a1 = old.@a1;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B23:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B42:
				case (int) GRGEN_MODEL.NodeTypes.@B43:
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@C221:
					// copy attributes for: B22
					{
						GRGEN_MODEL.IB22 old = (GRGEN_MODEL.IB22) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: C222_411
					{
						GRGEN_MODEL.IC222_411 old = (GRGEN_MODEL.IC222_411) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@c222_411 = old.@c222_411;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
					// copy attributes for: D11_2221
					{
						GRGEN_MODEL.ID11_2221 old = (GRGEN_MODEL.ID11_2221) oldNode;
						newNode.@a1 = old.@a1;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@c222_411 = old.@c222_411;
						newNode.@d11_2221 = old.@d11_2221;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_D11_2221_a1 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static Comparer_D11_2221_a1 thisComparer = new Comparer_D11_2221_a1();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@a1.CompareTo(b.@a1);
		}
	}

	public class ReverseComparer_D11_2221_a1 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static ReverseComparer_D11_2221_a1 thisComparer = new ReverseComparer_D11_2221_a1();
		public override int Compare(GRGEN_MODEL.ID11_2221 b, GRGEN_MODEL.ID11_2221 a)
		{
			return a.@a1.CompareTo(b.@a1);
		}
	}

	public class ArrayHelper_D11_2221_a1
	{
		private static GRGEN_MODEL.ID11_2221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			instanceBearingAttributeForSearch.@a1 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D11_2221_a1.thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(Comparer_D11_2221_a1.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(ReverseComparer_D11_2221_a1.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayGroupBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID11_2221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID11_2221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a1)) {
					seenValues[list[pos].@a1].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID11_2221> tempList = new List<GRGEN_MODEL.ID11_2221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a1, tempList);
				}
			}
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			foreach(List<GRGEN_MODEL.ID11_2221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID11_2221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a1)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a1, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID11_2221 entry in list)
				resultList.Add(entry.@a1);
			return resultList;
		}
	}


	public class Comparer_D11_2221_a2 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static Comparer_D11_2221_a2 thisComparer = new Comparer_D11_2221_a2();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_D11_2221_a2 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static ReverseComparer_D11_2221_a2 thisComparer = new ReverseComparer_D11_2221_a2();
		public override int Compare(GRGEN_MODEL.ID11_2221 b, GRGEN_MODEL.ID11_2221 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_D11_2221_a2
	{
		private static GRGEN_MODEL.ID11_2221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D11_2221_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(Comparer_D11_2221_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(ReverseComparer_D11_2221_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayGroupBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID11_2221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID11_2221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID11_2221> tempList = new List<GRGEN_MODEL.ID11_2221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			foreach(List<GRGEN_MODEL.ID11_2221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID11_2221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID11_2221 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	public class Comparer_D11_2221_b22 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static Comparer_D11_2221_b22 thisComparer = new Comparer_D11_2221_b22();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ReverseComparer_D11_2221_b22 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static ReverseComparer_D11_2221_b22 thisComparer = new ReverseComparer_D11_2221_b22();
		public override int Compare(GRGEN_MODEL.ID11_2221 b, GRGEN_MODEL.ID11_2221 a)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ArrayHelper_D11_2221_b22
	{
		private static GRGEN_MODEL.ID11_2221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			instanceBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D11_2221_b22.thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(Comparer_D11_2221_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(ReverseComparer_D11_2221_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayGroupBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID11_2221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID11_2221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b22)) {
					seenValues[list[pos].@b22].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID11_2221> tempList = new List<GRGEN_MODEL.ID11_2221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b22, tempList);
				}
			}
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			foreach(List<GRGEN_MODEL.ID11_2221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID11_2221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b22)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b22, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID11_2221 entry in list)
				resultList.Add(entry.@b22);
			return resultList;
		}
	}


	public class Comparer_D11_2221_a4 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static Comparer_D11_2221_a4 thisComparer = new Comparer_D11_2221_a4();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_D11_2221_a4 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static ReverseComparer_D11_2221_a4 thisComparer = new ReverseComparer_D11_2221_a4();
		public override int Compare(GRGEN_MODEL.ID11_2221 b, GRGEN_MODEL.ID11_2221 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_D11_2221_a4
	{
		private static GRGEN_MODEL.ID11_2221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D11_2221_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(Comparer_D11_2221_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(ReverseComparer_D11_2221_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayGroupBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID11_2221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID11_2221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID11_2221> tempList = new List<GRGEN_MODEL.ID11_2221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			foreach(List<GRGEN_MODEL.ID11_2221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID11_2221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID11_2221 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	public class Comparer_D11_2221_b41 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static Comparer_D11_2221_b41 thisComparer = new Comparer_D11_2221_b41();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ReverseComparer_D11_2221_b41 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static ReverseComparer_D11_2221_b41 thisComparer = new ReverseComparer_D11_2221_b41();
		public override int Compare(GRGEN_MODEL.ID11_2221 b, GRGEN_MODEL.ID11_2221 a)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ArrayHelper_D11_2221_b41
	{
		private static GRGEN_MODEL.ID11_2221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			instanceBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D11_2221_b41.thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(Comparer_D11_2221_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(ReverseComparer_D11_2221_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayGroupBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID11_2221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID11_2221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b41)) {
					seenValues[list[pos].@b41].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID11_2221> tempList = new List<GRGEN_MODEL.ID11_2221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b41, tempList);
				}
			}
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			foreach(List<GRGEN_MODEL.ID11_2221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID11_2221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b41)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b41, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID11_2221 entry in list)
				resultList.Add(entry.@b41);
			return resultList;
		}
	}


	public class Comparer_D11_2221_c222_411 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static Comparer_D11_2221_c222_411 thisComparer = new Comparer_D11_2221_c222_411();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
	}

	public class ReverseComparer_D11_2221_c222_411 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static ReverseComparer_D11_2221_c222_411 thisComparer = new ReverseComparer_D11_2221_c222_411();
		public override int Compare(GRGEN_MODEL.ID11_2221 b, GRGEN_MODEL.ID11_2221 a)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
	}

	public class ArrayHelper_D11_2221_c222_411
	{
		private static GRGEN_MODEL.ID11_2221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			instanceBearingAttributeForSearch.@c222_411 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D11_2221_c222_411.thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(Comparer_D11_2221_c222_411.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(ReverseComparer_D11_2221_c222_411.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayGroupBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID11_2221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID11_2221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@c222_411)) {
					seenValues[list[pos].@c222_411].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID11_2221> tempList = new List<GRGEN_MODEL.ID11_2221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@c222_411, tempList);
				}
			}
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			foreach(List<GRGEN_MODEL.ID11_2221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID11_2221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@c222_411)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@c222_411, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID11_2221 entry in list)
				resultList.Add(entry.@c222_411);
			return resultList;
		}
	}


	public class Comparer_D11_2221_d11_2221 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static Comparer_D11_2221_d11_2221 thisComparer = new Comparer_D11_2221_d11_2221();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@d11_2221.CompareTo(b.@d11_2221);
		}
	}

	public class ReverseComparer_D11_2221_d11_2221 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		public static ReverseComparer_D11_2221_d11_2221 thisComparer = new ReverseComparer_D11_2221_d11_2221();
		public override int Compare(GRGEN_MODEL.ID11_2221 b, GRGEN_MODEL.ID11_2221 a)
		{
			return a.@d11_2221.CompareTo(b.@d11_2221);
		}
	}

	public class ArrayHelper_D11_2221_d11_2221
	{
		private static GRGEN_MODEL.ID11_2221 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			instanceBearingAttributeForSearch.@d11_2221 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D11_2221_d11_2221.thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(Comparer_D11_2221_d11_2221.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(ReverseComparer_D11_2221_d11_2221.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayGroupBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID11_2221>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID11_2221>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@d11_2221)) {
					seenValues[list[pos].@d11_2221].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID11_2221> tempList = new List<GRGEN_MODEL.ID11_2221>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@d11_2221, tempList);
				}
			}
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			foreach(List<GRGEN_MODEL.ID11_2221> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID11_2221 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@d11_2221)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@d11_2221, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID11_2221 entry in list)
				resultList.Add(entry.@d11_2221);
			return resultList;
		}
	}


	// *** Node D2211_2222_31 ***

	public interface ID2211_2222_31 : IC221, IC222_411, IA3
	{
		int @d2211_2222_31 { get; set; }
	}

	public sealed partial class @D2211_2222_31 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ID2211_2222_31
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@D2211_2222_31[] pool;

		// explicit initializations of A2 for target D2211_2222_31
		// implicit initializations of A2 for target D2211_2222_31
		// explicit initializations of B22 for target D2211_2222_31
		// implicit initializations of B22 for target D2211_2222_31
		// explicit initializations of C221 for target D2211_2222_31
		// implicit initializations of C221 for target D2211_2222_31
		// explicit initializations of A4 for target D2211_2222_31
		// implicit initializations of A4 for target D2211_2222_31
		// explicit initializations of B41 for target D2211_2222_31
		// implicit initializations of B41 for target D2211_2222_31
		// explicit initializations of C222_411 for target D2211_2222_31
		// implicit initializations of C222_411 for target D2211_2222_31
		// explicit initializations of A3 for target D2211_2222_31
		// implicit initializations of A3 for target D2211_2222_31
		// explicit initializations of D2211_2222_31 for target D2211_2222_31
		// implicit initializations of D2211_2222_31 for target D2211_2222_31
		static @D2211_2222_31() {
		}

		public @D2211_2222_31() : base(GRGEN_MODEL.NodeType_D2211_2222_31.typeVar)
		{
			// implicit initialization, container creation of D2211_2222_31
			// explicit initializations of A2 for target D2211_2222_31
			// explicit initializations of B22 for target D2211_2222_31
			// explicit initializations of C221 for target D2211_2222_31
			// explicit initializations of A4 for target D2211_2222_31
			// explicit initializations of B41 for target D2211_2222_31
			// explicit initializations of C222_411 for target D2211_2222_31
			// explicit initializations of A3 for target D2211_2222_31
			// explicit initializations of D2211_2222_31 for target D2211_2222_31
		}

		public static GRGEN_MODEL.NodeType_D2211_2222_31 TypeInstance { get { return GRGEN_MODEL.NodeType_D2211_2222_31.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@D2211_2222_31(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@D2211_2222_31(this, graph, oldToNewObjectMap);
		}

		private @D2211_2222_31(GRGEN_MODEL.@D2211_2222_31 oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_D2211_2222_31.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
			c221_M0no_suXx_h4rD = oldElem.c221_M0no_suXx_h4rD;
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			c222_411_M0no_suXx_h4rD = oldElem.c222_411_M0no_suXx_h4rD;
			a3_M0no_suXx_h4rD = oldElem.a3_M0no_suXx_h4rD;
			d2211_2222_31_M0no_suXx_h4rD = oldElem.d2211_2222_31_M0no_suXx_h4rD;
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @D2211_2222_31))
				return false;
			@D2211_2222_31 that_ = (@D2211_2222_31)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& c221_M0no_suXx_h4rD == that_.c221_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& c222_411_M0no_suXx_h4rD == that_.c222_411_M0no_suXx_h4rD
				&& a3_M0no_suXx_h4rD == that_.a3_M0no_suXx_h4rD
				&& d2211_2222_31_M0no_suXx_h4rD == that_.d2211_2222_31_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@D2211_2222_31 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@D2211_2222_31 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@D2211_2222_31();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@D2211_2222_31[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of D2211_2222_31
				node.@a2 = 0;
				node.@b22 = 0;
				node.@c221 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				node.@a3 = 0;
				node.@d2211_2222_31 = 0;
				// explicit initializations of A2 for target D2211_2222_31
				// explicit initializations of B22 for target D2211_2222_31
				// explicit initializations of C221 for target D2211_2222_31
				// explicit initializations of A4 for target D2211_2222_31
				// explicit initializations of B41 for target D2211_2222_31
				// explicit initializations of C222_411 for target D2211_2222_31
				// explicit initializations of A3 for target D2211_2222_31
				// explicit initializations of D2211_2222_31 for target D2211_2222_31
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@D2211_2222_31 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@D2211_2222_31 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@D2211_2222_31();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@D2211_2222_31[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of D2211_2222_31
				node.@a2 = 0;
				node.@b22 = 0;
				node.@c221 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				node.@a3 = 0;
				node.@d2211_2222_31 = 0;
				// explicit initializations of A2 for target D2211_2222_31
				// explicit initializations of B22 for target D2211_2222_31
				// explicit initializations of C221 for target D2211_2222_31
				// explicit initializations of A4 for target D2211_2222_31
				// explicit initializations of B41 for target D2211_2222_31
				// explicit initializations of C222_411 for target D2211_2222_31
				// explicit initializations of A3 for target D2211_2222_31
				// explicit initializations of D2211_2222_31 for target D2211_2222_31
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@D2211_2222_31[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int a2_M0no_suXx_h4rD;
		public int @a2
		{
			get { return a2_M0no_suXx_h4rD; }
			set { a2_M0no_suXx_h4rD = value; }
		}

		private int b22_M0no_suXx_h4rD;
		public int @b22
		{
			get { return b22_M0no_suXx_h4rD; }
			set { b22_M0no_suXx_h4rD = value; }
		}

		private int c221_M0no_suXx_h4rD;
		public int @c221
		{
			get { return c221_M0no_suXx_h4rD; }
			set { c221_M0no_suXx_h4rD = value; }
		}

		private int a4_M0no_suXx_h4rD;
		public int @a4
		{
			get { return a4_M0no_suXx_h4rD; }
			set { a4_M0no_suXx_h4rD = value; }
		}

		private int b41_M0no_suXx_h4rD;
		public int @b41
		{
			get { return b41_M0no_suXx_h4rD; }
			set { b41_M0no_suXx_h4rD = value; }
		}

		private int c222_411_M0no_suXx_h4rD;
		public int @c222_411
		{
			get { return c222_411_M0no_suXx_h4rD; }
			set { c222_411_M0no_suXx_h4rD = value; }
		}

		private int a3_M0no_suXx_h4rD;
		public int @a3
		{
			get { return a3_M0no_suXx_h4rD; }
			set { a3_M0no_suXx_h4rD = value; }
		}

		private int d2211_2222_31_M0no_suXx_h4rD;
		public int @d2211_2222_31
		{
			get { return d2211_2222_31_M0no_suXx_h4rD; }
			set { d2211_2222_31_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
				case "b22": return this.@b22;
				case "c221": return this.@c221;
				case "a4": return this.@a4;
				case "b41": return this.@b41;
				case "c222_411": return this.@c222_411;
				case "a3": return this.@a3;
				case "d2211_2222_31": return this.@d2211_2222_31;
			}
			throw new NullReferenceException(
				"The Node type \"D2211_2222_31\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b22": this.@b22 = (int) value; return;
				case "c221": this.@c221 = (int) value; return;
				case "a4": this.@a4 = (int) value; return;
				case "b41": this.@b41 = (int) value; return;
				case "c222_411": this.@c222_411 = (int) value; return;
				case "a3": this.@a3 = (int) value; return;
				case "d2211_2222_31": this.@d2211_2222_31 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"D2211_2222_31\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of D2211_2222_31
			this.@a2 = 0;
			this.@b22 = 0;
			this.@c221 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
			this.@c222_411 = 0;
			this.@a3 = 0;
			this.@d2211_2222_31 = 0;
			// explicit initializations of A2 for target D2211_2222_31
			// explicit initializations of B22 for target D2211_2222_31
			// explicit initializations of C221 for target D2211_2222_31
			// explicit initializations of A4 for target D2211_2222_31
			// explicit initializations of B41 for target D2211_2222_31
			// explicit initializations of C222_411 for target D2211_2222_31
			// explicit initializations of A3 for target D2211_2222_31
			// explicit initializations of D2211_2222_31 for target D2211_2222_31
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("D2211_2222_31 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("D2211_2222_31 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_D2211_2222_31 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_D2211_2222_31 typeVar = new GRGEN_MODEL.NodeType_D2211_2222_31();
		public static bool[] isA = new bool[] { true, false, true, true, true, false, false, true, false, true, false, false, true, true, false, false, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_d2211_2222_31;
		public NodeType_D2211_2222_31() : base((int) NodeTypes.@D2211_2222_31)
		{
			AttributeType_d2211_2222_31 = new GRGEN_LIBGR.AttributeType("d2211_2222_31", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "D2211_2222_31"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "D2211_2222_31"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.ID2211_2222_31"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@D2211_2222_31"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@D2211_2222_31();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 8; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				yield return GRGEN_MODEL.NodeType_C221.AttributeType_c221;
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				yield return GRGEN_MODEL.NodeType_C222_411.AttributeType_c222_411;
				yield return GRGEN_MODEL.NodeType_A3.AttributeType_a3;
				yield return AttributeType_d2211_2222_31;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b22" : return GRGEN_MODEL.NodeType_B22.AttributeType_b22;
				case "c221" : return GRGEN_MODEL.NodeType_C221.AttributeType_c221;
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b41" : return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				case "c222_411" : return GRGEN_MODEL.NodeType_C222_411.AttributeType_c222_411;
				case "a3" : return GRGEN_MODEL.NodeType_A3.AttributeType_a3;
				case "d2211_2222_31" : return AttributeType_d2211_2222_31;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@D2211_2222_31 newNode = new GRGEN_MODEL.@D2211_2222_31();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B23:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A3:
					// copy attributes for: A3
					{
						GRGEN_MODEL.IA3 old = (GRGEN_MODEL.IA3) oldNode;
						newNode.@a3 = old.@a3;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A4:
				case (int) GRGEN_MODEL.NodeTypes.@B42:
				case (int) GRGEN_MODEL.NodeTypes.@B43:
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B22:
					// copy attributes for: B22
					{
						GRGEN_MODEL.IB22 old = (GRGEN_MODEL.IB22) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B41:
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C221:
					// copy attributes for: C221
					{
						GRGEN_MODEL.IC221 old = (GRGEN_MODEL.IC221) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@c221 = old.@c221;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
					// copy attributes for: C222_411
					{
						GRGEN_MODEL.IC222_411 old = (GRGEN_MODEL.IC222_411) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@c222_411 = old.@c222_411;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: D2211_2222_31
					{
						GRGEN_MODEL.ID2211_2222_31 old = (GRGEN_MODEL.ID2211_2222_31) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@c221 = old.@c221;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@c222_411 = old.@c222_411;
						newNode.@a3 = old.@a3;
						newNode.@d2211_2222_31 = old.@d2211_2222_31;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_D2211_2222_31_a2 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_a2 thisComparer = new Comparer_D2211_2222_31_a2();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ReverseComparer_D2211_2222_31_a2 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_a2 thisComparer = new ReverseComparer_D2211_2222_31_a2();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@a2.CompareTo(b.@a2);
		}
	}

	public class ArrayHelper_D2211_2222_31_a2
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_a2.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_a2.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a2)) {
					seenValues[list[pos].@a2].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a2, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a2)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a2, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@a2);
			return resultList;
		}
	}


	public class Comparer_D2211_2222_31_b22 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_b22 thisComparer = new Comparer_D2211_2222_31_b22();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ReverseComparer_D2211_2222_31_b22 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_b22 thisComparer = new ReverseComparer_D2211_2222_31_b22();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@b22.CompareTo(b.@b22);
		}
	}

	public class ArrayHelper_D2211_2222_31_b22
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_b22.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_b22.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b22)) {
					seenValues[list[pos].@b22].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b22, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b22)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b22, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@b22);
			return resultList;
		}
	}


	public class Comparer_D2211_2222_31_c221 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_c221 thisComparer = new Comparer_D2211_2222_31_c221();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@c221.CompareTo(b.@c221);
		}
	}

	public class ReverseComparer_D2211_2222_31_c221 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_c221 thisComparer = new ReverseComparer_D2211_2222_31_c221();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@c221.CompareTo(b.@c221);
		}
	}

	public class ArrayHelper_D2211_2222_31_c221
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@c221 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_c221.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_c221.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_c221.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@c221)) {
					seenValues[list[pos].@c221].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@c221, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@c221)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@c221, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@c221);
			return resultList;
		}
	}


	public class Comparer_D2211_2222_31_a4 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_a4 thisComparer = new Comparer_D2211_2222_31_a4();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ReverseComparer_D2211_2222_31_a4 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_a4 thisComparer = new ReverseComparer_D2211_2222_31_a4();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@a4.CompareTo(b.@a4);
		}
	}

	public class ArrayHelper_D2211_2222_31_a4
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_a4.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_a4.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a4)) {
					seenValues[list[pos].@a4].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a4, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a4)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a4, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@a4);
			return resultList;
		}
	}


	public class Comparer_D2211_2222_31_b41 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_b41 thisComparer = new Comparer_D2211_2222_31_b41();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ReverseComparer_D2211_2222_31_b41 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_b41 thisComparer = new ReverseComparer_D2211_2222_31_b41();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@b41.CompareTo(b.@b41);
		}
	}

	public class ArrayHelper_D2211_2222_31_b41
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_b41.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_b41.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@b41)) {
					seenValues[list[pos].@b41].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@b41, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@b41)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@b41, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@b41);
			return resultList;
		}
	}


	public class Comparer_D2211_2222_31_c222_411 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_c222_411 thisComparer = new Comparer_D2211_2222_31_c222_411();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
	}

	public class ReverseComparer_D2211_2222_31_c222_411 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_c222_411 thisComparer = new ReverseComparer_D2211_2222_31_c222_411();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
	}

	public class ArrayHelper_D2211_2222_31_c222_411
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@c222_411 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_c222_411.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_c222_411.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_c222_411.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@c222_411)) {
					seenValues[list[pos].@c222_411].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@c222_411, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@c222_411)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@c222_411, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@c222_411);
			return resultList;
		}
	}


	public class Comparer_D2211_2222_31_a3 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_a3 thisComparer = new Comparer_D2211_2222_31_a3();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@a3.CompareTo(b.@a3);
		}
	}

	public class ReverseComparer_D2211_2222_31_a3 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_a3 thisComparer = new ReverseComparer_D2211_2222_31_a3();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@a3.CompareTo(b.@a3);
		}
	}

	public class ArrayHelper_D2211_2222_31_a3
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@a3 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_a3.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_a3.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_a3.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@a3)) {
					seenValues[list[pos].@a3].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@a3, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@a3)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@a3, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@a3);
			return resultList;
		}
	}


	public class Comparer_D2211_2222_31_d2211_2222_31 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static Comparer_D2211_2222_31_d2211_2222_31 thisComparer = new Comparer_D2211_2222_31_d2211_2222_31();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@d2211_2222_31.CompareTo(b.@d2211_2222_31);
		}
	}

	public class ReverseComparer_D2211_2222_31_d2211_2222_31 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		public static ReverseComparer_D2211_2222_31_d2211_2222_31 thisComparer = new ReverseComparer_D2211_2222_31_d2211_2222_31();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 b, GRGEN_MODEL.ID2211_2222_31 a)
		{
			return a.@d2211_2222_31.CompareTo(b.@d2211_2222_31);
		}
	}

	public class ArrayHelper_D2211_2222_31_d2211_2222_31
	{
		private static GRGEN_MODEL.ID2211_2222_31 instanceBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			instanceBearingAttributeForSearch.@d2211_2222_31 = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_D2211_2222_31_d2211_2222_31.thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(Comparer_D2211_2222_31_d2211_2222_31.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderDescendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(ReverseComparer_D2211_2222_31_d2211_2222_31.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayGroupBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>> seenValues = new Dictionary<int, List<GRGEN_MODEL.ID2211_2222_31>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@d2211_2222_31)) {
					seenValues[list[pos].@d2211_2222_31].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.ID2211_2222_31> tempList = new List<GRGEN_MODEL.ID2211_2222_31>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@d2211_2222_31, tempList);
				}
			}
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			foreach(List<GRGEN_MODEL.ID2211_2222_31> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayKeepOneForEachBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.ID2211_2222_31 element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@d2211_2222_31)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@d2211_2222_31, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.ID2211_2222_31 entry in list)
				resultList.Add(entry.@d2211_2222_31);
			return resultList;
		}
	}


	// *** Node D231_4121 ***

	public interface ID231_4121 : IB23, IC412_421_431_51
	{
		int @d231_4121 { get; set; }
	}

	public abstract class @D231_4121 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ID231_4121
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@D231_4121[] pool;

		// explicit initializations of A2 for target D231_4121
		// implicit initializations of A2 for target D231_4121
		// explicit initializations of B23 for target D231_4121
		// implicit initializations of B23 for target D231_4121
		// explicit initializations of A4 for target D231_4121
		// implicit initializations of A4 for target D231_4121
		// explicit initializations of B41 for target D231_4121
		// implicit initializations of B41 for target D231_4121
		// explicit initializations of B42 for target D231_4121
		// implicit initializations of B42 for target D231_4121
		// explicit initializations of B43 for target D231_4121
		// implicit initializations of B43 for target D231_4121
		// explicit initializations of A5 for target D231_4121
		// implicit initializations of A5 for target D231_4121
		// explicit initializations of C412_421_431_51 for target D231_4121
		// implicit initializations of C412_421_431_51 for target D231_4121
		// explicit initializations of D231_4121 for target D231_4121
		// implicit initializations of D231_4121 for target D231_4121
		static @D231_4121() {
		}

		public @D231_4121() : base(GRGEN_MODEL.NodeType_D231_4121.typeVar)
		{
			// implicit initialization, container creation of D231_4121
			// explicit initializations of A2 for target D231_4121
			// explicit initializations of B23 for target D231_4121
			// explicit initializations of A4 for target D231_4121
			// explicit initializations of B41 for target D231_4121
			// explicit initializations of B42 for target D231_4121
			// explicit initializations of B43 for target D231_4121
			// explicit initializations of A5 for target D231_4121
			// explicit initializations of C412_421_431_51 for target D231_4121
			// explicit initializations of D231_4121 for target D231_4121
		}

		public static GRGEN_MODEL.NodeType_D231_4121 TypeInstance { get { return GRGEN_MODEL.NodeType_D231_4121.typeVar; } }

		public static GRGEN_MODEL.@D231_4121 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@D231_4121 node;
			if(poolLevel == 0)
				node = new global::test.D231_4121_Impl();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@D231_4121[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of D231_4121
				node.@a2 = 0;
				node.@b23 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
				node.@d231_4121 = 0;
				// explicit initializations of A2 for target D231_4121
				// explicit initializations of B23 for target D231_4121
				// explicit initializations of A4 for target D231_4121
				// explicit initializations of B41 for target D231_4121
				// explicit initializations of B42 for target D231_4121
				// explicit initializations of B43 for target D231_4121
				// explicit initializations of A5 for target D231_4121
				// explicit initializations of C412_421_431_51 for target D231_4121
				// explicit initializations of D231_4121 for target D231_4121
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@D231_4121 CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@D231_4121 node;
			if(poolLevel == 0)
				node = new global::test.D231_4121_Impl();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@D231_4121[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of D231_4121
				node.@a2 = 0;
				node.@b23 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
				node.@d231_4121 = 0;
				// explicit initializations of A2 for target D231_4121
				// explicit initializations of B23 for target D231_4121
				// explicit initializations of A4 for target D231_4121
				// explicit initializations of B41 for target D231_4121
				// explicit initializations of B42 for target D231_4121
				// explicit initializations of B43 for target D231_4121
				// explicit initializations of A5 for target D231_4121
				// explicit initializations of C412_421_431_51 for target D231_4121
				// explicit initializations of D231_4121 for target D231_4121
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@D231_4121[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public abstract int @a2 { get; set; }
		public abstract int @b23 { get; set; }
		public abstract int @a4 { get; set; }
		public abstract int @b41 { get; set; }
		public abstract int @b42 { get; set; }
		public abstract int @a5 { get; set; }
		public abstract int @d231_4121 { get; set; }
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return this.@a2;
				case "b23": return this.@b23;
				case "a4": return this.@a4;
				case "b41": return this.@b41;
				case "b42": return this.@b42;
				case "a5": return this.@a5;
				case "d231_4121": return this.@d231_4121;
			}
			throw new NullReferenceException(
				"The Node type \"D231_4121\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b23": this.@b23 = (int) value; return;
				case "a4": this.@a4 = (int) value; return;
				case "b41": this.@b41 = (int) value; return;
				case "b42": this.@b42 = (int) value; return;
				case "a5": this.@a5 = (int) value; return;
				case "d231_4121": this.@d231_4121 = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"D231_4121\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of D231_4121
			this.@a2 = 0;
			this.@b23 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
			this.@b42 = 0;
			this.@a5 = 0;
			this.@d231_4121 = 0;
			// explicit initializations of A2 for target D231_4121
			// explicit initializations of B23 for target D231_4121
			// explicit initializations of A4 for target D231_4121
			// explicit initializations of B41 for target D231_4121
			// explicit initializations of B42 for target D231_4121
			// explicit initializations of B43 for target D231_4121
			// explicit initializations of A5 for target D231_4121
			// explicit initializations of C412_421_431_51 for target D231_4121
			// explicit initializations of D231_4121 for target D231_4121
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("D231_4121 does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("D231_4121 does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class NodeType_D231_4121 : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_D231_4121 typeVar = new GRGEN_MODEL.NodeType_D231_4121();
		public static bool[] isA = new bool[] { true, false, true, false, true, true, false, false, true, true, true, true, false, false, true, false, false, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_d231_4121;
		public NodeType_D231_4121() : base((int) NodeTypes.@D231_4121)
		{
			AttributeType_d231_4121 = new GRGEN_LIBGR.AttributeType("d231_4121", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "D231_4121"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "D231_4121"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.ID231_4121"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@D231_4121"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new global::test.D231_4121_Impl();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 7; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				yield return GRGEN_MODEL.NodeType_B23.AttributeType_b23;
				yield return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				yield return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				yield return GRGEN_MODEL.NodeType_B42.AttributeType_b42;
				yield return GRGEN_MODEL.NodeType_A5.AttributeType_a5;
				yield return AttributeType_d231_4121;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "a2" : return GRGEN_MODEL.NodeType_A2.AttributeType_a2;
				case "b23" : return GRGEN_MODEL.NodeType_B23.AttributeType_b23;
				case "a4" : return GRGEN_MODEL.NodeType_A4.AttributeType_a4;
				case "b41" : return GRGEN_MODEL.NodeType_B41.AttributeType_b41;
				case "b42" : return GRGEN_MODEL.NodeType_B42.AttributeType_b42;
				case "a5" : return GRGEN_MODEL.NodeType_A5.AttributeType_a5;
				case "d231_4121" : return AttributeType_d231_4121;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@D231_4121 newNode = new global::test.D231_4121_Impl();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@A2:
				case (int) GRGEN_MODEL.NodeTypes.@B21:
				case (int) GRGEN_MODEL.NodeTypes.@B22:
				case (int) GRGEN_MODEL.NodeTypes.@C221:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A4:
					// copy attributes for: A4
					{
						GRGEN_MODEL.IA4 old = (GRGEN_MODEL.IA4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@A5:
					// copy attributes for: A5
					{
						GRGEN_MODEL.IA5 old = (GRGEN_MODEL.IA5) oldNode;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B23:
					// copy attributes for: B23
					{
						GRGEN_MODEL.IB23 old = (GRGEN_MODEL.IB23) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b23 = old.@b23;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B41:
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B42:
					// copy attributes for: B42
					{
						GRGEN_MODEL.IB42 old = (GRGEN_MODEL.IB42) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@B43:
					// copy attributes for: B43
					{
						GRGEN_MODEL.IB43 old = (GRGEN_MODEL.IB43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C222_411:
				case (int) GRGEN_MODEL.NodeTypes.@D11_2221:
				case (int) GRGEN_MODEL.NodeTypes.@D2211_2222_31:
					// copy attributes for: A2
					{
						GRGEN_MODEL.IA2 old = (GRGEN_MODEL.IA2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						GRGEN_MODEL.IB41 old = (GRGEN_MODEL.IB41) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C412_421_431_51:
					// copy attributes for: C412_421_431_51
					{
						GRGEN_MODEL.IC412_421_431_51 old = (GRGEN_MODEL.IC412_421_431_51) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@b42 = old.@b42;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@C432_422:
					// copy attributes for: B42
					{
						GRGEN_MODEL.IB42 old = (GRGEN_MODEL.IB42) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					// copy attributes for: B43
						// already copied: a4
					break;
				case (int) GRGEN_MODEL.NodeTypes.@D231_4121:
					// copy attributes for: D231_4121
					{
						GRGEN_MODEL.ID231_4121 old = (GRGEN_MODEL.ID231_4121) oldNode;
						newNode.@a2 = old.@a2;
						newNode.@b23 = old.@b23;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@b42 = old.@b42;
						newNode.@a5 = old.@a5;
						newNode.@d231_4121 = old.@d231_4121;
					}
					break;
			}
			return newNode;
		}

	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge=0, @Edge=1, @UEdge=2 };

	// *** Edge AEdge ***


	public sealed partial class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override string Name { get { return "AEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "AEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return null; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Arbitrary; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}


		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge does not support source and target setting!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			throw new Exception("Cannot retype to the abstract type AEdge!");
		}
	}

	// *** Edge Edge ***


	public sealed partial class @Edge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IDEdge
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@Edge[] pool;

		static @Edge() {
		}

		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, source, target)
		{
			// implicit initialization, container creation of Edge
		}

		public static GRGEN_MODEL.EdgeType_Edge TypeInstance { get { return GRGEN_MODEL.EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {
			return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);
		}

		public override GRGEN_LIBGR.IEdge Copy(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);
		}

		private @Edge(GRGEN_MODEL.@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @Edge))
				return false;
			@Edge that_ = (@Edge)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@Edge[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of Edge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@Edge[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of Edge
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@Edge[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Edge
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Edge does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Edge does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_Edge typeVar = new GRGEN_MODEL.EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IDEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@Edge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}


		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed partial class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IUEdge
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@UEdge[] pool;

		static @UEdge() {
		}

		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, source, target)
		{
			// implicit initialization, container creation of UEdge
		}

		public static GRGEN_MODEL.EdgeType_UEdge TypeInstance { get { return GRGEN_MODEL.EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {
			return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);
		}

		public override GRGEN_LIBGR.IEdge Copy(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);
		}

		private @UEdge(GRGEN_MODEL.@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @UEdge))
				return false;
			@UEdge that_ = (@UEdge)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@UEdge[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of UEdge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@UEdge[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of UEdge
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@UEdge[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of UEdge
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("UEdge does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("UEdge does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_UEdge typeVar = new GRGEN_MODEL.EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IUEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_complModel.@UEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Undirected; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}


		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Object types
	//

	public enum ObjectTypes { @Object=0 };

	// *** Object Object ***


	public sealed partial class @Object : GRGEN_LGSP.LGSPObject, GRGEN_LIBGR.IObject
	{

		static @Object() {
		}

		//create object by CreateObject of the type class, not this internal-use constructor
		public @Object(long uniqueId) : base(GRGEN_MODEL.ObjectType_Object.typeVar, uniqueId)
		{
			// implicit initialization, container creation of Object
		}

		public static GRGEN_MODEL.ObjectType_Object TypeInstance { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }

		public override GRGEN_LIBGR.IObject Clone(GRGEN_LIBGR.IGraph graph) {
			GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(this, graph, null);
			((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
			return newObject;
		}

		public override GRGEN_LIBGR.IObject Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(this, graph, oldToNewObjectMap);
			((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
			return newObject;
		}

		private @Object(GRGEN_MODEL.@Object oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.ObjectType_Object.typeVar, -1)
		{
			if(oldToNewObjectMap != null)
				oldToNewObjectMap.Add(oldElem, this);
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @Object))
				return false;
			@Object that_ = (@Object)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The Object type \"Object\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The Object type \"Object\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of Object
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Object does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("Object does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class ObjectType_Object : GRGEN_LIBGR.ObjectType
	{
		public static GRGEN_MODEL.ObjectType_Object typeVar = new GRGEN_MODEL.ObjectType_Object();
		public static bool[] isA = new bool[] { true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public ObjectType_Object() : base((int) ObjectTypes.@Object)
		{
		}
		public override string Name { get { return "Object"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Object"; } }
		public override string ObjectInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.IObject"; } }
		public override string ObjectClassName { get { return "de.unika.ipd.grGen.Model_complModel.@Object"; } }
		public override GRGEN_LIBGR.IObject CreateObject(GRGEN_LIBGR.IGraph graph, long uniqueId)
		{
			if(uniqueId != -1) {
				throw new Exception("The model of the object class type Object does not support uniqueIds!");
			} else {
				GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(-1);
				((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
				return newObject;
			}
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	//
	// Transient object types
	//

	public enum TransientObjectTypes { @TransientObject=0 };

	// *** TransientObject TransientObject ***


	public sealed partial class @TransientObject : GRGEN_LGSP.LGSPTransientObject, GRGEN_LIBGR.ITransientObject
	{

		static @TransientObject() {
		}

		//create object by CreateTransientObject of the type class, not this internal-use constructor
		public @TransientObject() : base(GRGEN_MODEL.TransientObjectType_TransientObject.typeVar)
		{
			// implicit initialization, container creation of TransientObject
		}

		public static GRGEN_MODEL.TransientObjectType_TransientObject TypeInstance { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }

		public override GRGEN_LIBGR.ITransientObject Clone() {
			return new GRGEN_MODEL.@TransientObject(this, null, null);
		}

		public override GRGEN_LIBGR.ITransientObject Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@TransientObject(this, graph, oldToNewObjectMap);
		}

		private @TransientObject(GRGEN_MODEL.@TransientObject oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.TransientObjectType_TransientObject.typeVar)
		{
			if(oldToNewObjectMap != null)
				oldToNewObjectMap.Add(oldElem, this);
		}
		
		private GRGEN_LIBGR.IBaseObject Copy(GRGEN_LIBGR.IBaseObject oldObj, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
		{
			if(oldObj == null)
				return null;
			if(oldToNewObjectMap.ContainsKey(oldObj))
				return (GRGEN_LIBGR.IBaseObject)oldToNewObjectMap[oldObj];
			else {
				if(oldObj is GRGEN_LIBGR.IObject) {
					GRGEN_LIBGR.IObject newObj = ((GRGEN_LIBGR.IObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				} else {
					GRGEN_LIBGR.ITransientObject newObj = ((GRGEN_LIBGR.ITransientObject)oldObj).Copy(graph, oldToNewObjectMap);
					return newObj;
				}
			}
		}

		public override bool IsDeeplyEqual(GRGEN_LIBGR.IDeepEqualityComparer that, IDictionary<object, object> visitedObjects) {
			if(visitedObjects.ContainsKey(this) || visitedObjects.ContainsKey(that))
				throw new Exception("Multiple appearances (and cycles) forbidden in deep equality comparison (only tree-like structures are supported)!");
			if(this == that)
				return true;
			if(!(that is @TransientObject))
				return false;
			@TransientObject that_ = (@TransientObject)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The TransientObject type \"TransientObject\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The TransientObject type \"TransientObject\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of TransientObject
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("TransientObject does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("TransientObject does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed partial class TransientObjectType_TransientObject : GRGEN_LIBGR.TransientObjectType
	{
		public static GRGEN_MODEL.TransientObjectType_TransientObject typeVar = new GRGEN_MODEL.TransientObjectType_TransientObject();
		public static bool[] isA = new bool[] { true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public TransientObjectType_TransientObject() : base((int) TransientObjectTypes.@TransientObject)
		{
		}
		public override string Name { get { return "TransientObject"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "TransientObject"; } }
		public override string TransientObjectInterfaceName { get { return "de.unika.ipd.grGen.Model_complModel.ITransientObject"; } }
		public override string TransientObjectClassName { get { return "de.unika.ipd.grGen.Model_complModel.@TransientObject"; } }
		public override GRGEN_LIBGR.ITransientObject CreateTransientObject()
		{
			return new GRGEN_MODEL.@TransientObject();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override GRGEN_LIBGR.Annotations Annotations { get { return annotations; } }
		public GRGEN_LIBGR.Annotations annotations = new GRGEN_LIBGR.Annotations();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	public sealed class ExternalObjectType_object : GRGEN_LIBGR.ExternalObjectType
	{
		public ExternalObjectType_object()
			: base("object", typeof(object))
		{
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }

		public static object ThrowCopyClassMissingException() { throw new Exception("Cannot copy/clone external object, copy class specification is missing in the model."); }
	}

	//
	// Indices
	//

	public class complModelIndexSet : GRGEN_LIBGR.IIndexSet
	{
		public complModelIndexSet(GRGEN_LGSP.LGSPGraph graph)
		{
		}


		public GRGEN_LIBGR.IIndex GetIndex(string indexName)
		{
			switch(indexName)
			{
				default: return null;
			}
		}

		public void FillAsClone(GRGEN_LGSP.LGSPGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap)
		{
		}
	}

	//
	// Node model
	//

	public sealed class complModelNodeModel : GRGEN_LIBGR.INodeModel
	{
		public complModelNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A1.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_A3.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_A5.typeVar,
				GRGEN_MODEL.NodeType_B21.typeVar,
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_B23.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_B43.typeVar,
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_C432_422.typeVar,
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A1.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_A3.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_A5.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_A1.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_A1.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A1.typeVar,
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
			};
			GRGEN_MODEL.NodeType_A1.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_A1.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
			};
			GRGEN_MODEL.NodeType_A1.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_A1.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A1.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A1.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_A1.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A2.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_A2.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_B21.typeVar,
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_B23.typeVar,
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_A2.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_A2.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B21.typeVar,
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_B23.typeVar,
			};
			GRGEN_MODEL.NodeType_A2.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_A2.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A2.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_A2.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A3.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_A3.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A3.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_A3.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_A3.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_A3.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_A3.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A3.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A3.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_A3.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A4.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_A4.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_B43.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_C432_422.typeVar,
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_A4.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_A4.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_B43.typeVar,
			};
			GRGEN_MODEL.NodeType_A4.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_A4.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A4.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_A4.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A5.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_A5.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A5.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_A5.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_A5.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
			};
			GRGEN_MODEL.NodeType_A5.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_A5.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A5.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_A5.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_A5.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_B21.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_B21.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B21.typeVar,
			};
			GRGEN_MODEL.NodeType_B21.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_B21.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_B21.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_B21.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B21.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
			};
			GRGEN_MODEL.NodeType_B21.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_B21.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A2.typeVar,
			};
			GRGEN_MODEL.NodeType_B22.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_B22.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_B22.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_B22.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
			};
			GRGEN_MODEL.NodeType_B22.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_B22.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
			};
			GRGEN_MODEL.NodeType_B22.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_B22.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A2.typeVar,
			};
			GRGEN_MODEL.NodeType_B23.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_B23.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B23.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_B23.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_B23.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_B23.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_B23.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B23.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
			};
			GRGEN_MODEL.NodeType_B23.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_B23.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A2.typeVar,
			};
			GRGEN_MODEL.NodeType_B41.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_B41.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_B41.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_B41.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
			};
			GRGEN_MODEL.NodeType_B41.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_B41.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
			};
			GRGEN_MODEL.NodeType_B41.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_B41.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A4.typeVar,
			};
			GRGEN_MODEL.NodeType_B42.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_B42.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_C432_422.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_B42.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_B42.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_C432_422.typeVar,
			};
			GRGEN_MODEL.NodeType_B42.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_B42.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
			};
			GRGEN_MODEL.NodeType_B42.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_B42.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A4.typeVar,
			};
			GRGEN_MODEL.NodeType_B43.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_B43.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B43.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_C432_422.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_B43.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_B43.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_C432_422.typeVar,
			};
			GRGEN_MODEL.NodeType_B43.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_B43.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B43.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
			};
			GRGEN_MODEL.NodeType_B43.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_B43.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A4.typeVar,
			};
			GRGEN_MODEL.NodeType_C221.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_C221.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_C221.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_C221.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_C221.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_C221.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_B22.typeVar,
			};
			GRGEN_MODEL.NodeType_C221.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_C221.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B22.typeVar,
			};
			GRGEN_MODEL.NodeType_C222_411.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_C222_411.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_C222_411.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_C222_411.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_C222_411.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_C222_411.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
			};
			GRGEN_MODEL.NodeType_C222_411.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_C222_411.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
			};
			GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_A5.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_B43.typeVar,
			};
			GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_C412_421_431_51.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_B43.typeVar,
				GRGEN_MODEL.NodeType_A5.typeVar,
			};
			GRGEN_MODEL.NodeType_C432_422.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_C432_422.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C432_422.typeVar,
			};
			GRGEN_MODEL.NodeType_C432_422.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_C432_422.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_C432_422.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_C432_422.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C432_422.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_B43.typeVar,
			};
			GRGEN_MODEL.NodeType_C432_422.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_C432_422.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B43.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
			};
			GRGEN_MODEL.NodeType_D11_2221.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_D11_2221.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
			};
			GRGEN_MODEL.NodeType_D11_2221.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_D11_2221.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_D11_2221.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_D11_2221.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D11_2221.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A1.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
			};
			GRGEN_MODEL.NodeType_D11_2221.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_D11_2221.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_A1.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
			};
			GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			};
			GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_A3.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_B22.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
			};
			GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_D2211_2222_31.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C221.typeVar,
				GRGEN_MODEL.NodeType_C222_411.typeVar,
				GRGEN_MODEL.NodeType_A3.typeVar,
			};
			GRGEN_MODEL.NodeType_D231_4121.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_D231_4121.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
			};
			GRGEN_MODEL.NodeType_D231_4121.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_D231_4121.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_D231_4121.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_D231_4121.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_D231_4121.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_A2.typeVar,
				GRGEN_MODEL.NodeType_A4.typeVar,
				GRGEN_MODEL.NodeType_A5.typeVar,
				GRGEN_MODEL.NodeType_B23.typeVar,
				GRGEN_MODEL.NodeType_B41.typeVar,
				GRGEN_MODEL.NodeType_B42.typeVar,
				GRGEN_MODEL.NodeType_B43.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
			};
			GRGEN_MODEL.NodeType_D231_4121.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_D231_4121.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_B23.typeVar,
				GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public GRGEN_LIBGR.NodeType RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		public GRGEN_LIBGR.NodeType GetType(string name)
		{
			switch(name)
			{
				case "Node" : return GRGEN_MODEL.NodeType_Node.typeVar;
				case "A1" : return GRGEN_MODEL.NodeType_A1.typeVar;
				case "A2" : return GRGEN_MODEL.NodeType_A2.typeVar;
				case "A3" : return GRGEN_MODEL.NodeType_A3.typeVar;
				case "A4" : return GRGEN_MODEL.NodeType_A4.typeVar;
				case "A5" : return GRGEN_MODEL.NodeType_A5.typeVar;
				case "B21" : return GRGEN_MODEL.NodeType_B21.typeVar;
				case "B22" : return GRGEN_MODEL.NodeType_B22.typeVar;
				case "B23" : return GRGEN_MODEL.NodeType_B23.typeVar;
				case "B41" : return GRGEN_MODEL.NodeType_B41.typeVar;
				case "B42" : return GRGEN_MODEL.NodeType_B42.typeVar;
				case "B43" : return GRGEN_MODEL.NodeType_B43.typeVar;
				case "C221" : return GRGEN_MODEL.NodeType_C221.typeVar;
				case "C222_411" : return GRGEN_MODEL.NodeType_C222_411.typeVar;
				case "C412_421_431_51" : return GRGEN_MODEL.NodeType_C412_421_431_51.typeVar;
				case "C432_422" : return GRGEN_MODEL.NodeType_C432_422.typeVar;
				case "D11_2221" : return GRGEN_MODEL.NodeType_D11_2221.typeVar;
				case "D2211_2222_31" : return GRGEN_MODEL.NodeType_D2211_2222_31.typeVar;
				case "D231_4121" : return GRGEN_MODEL.NodeType_D231_4121.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_A1.typeVar,
			GRGEN_MODEL.NodeType_A2.typeVar,
			GRGEN_MODEL.NodeType_A3.typeVar,
			GRGEN_MODEL.NodeType_A4.typeVar,
			GRGEN_MODEL.NodeType_A5.typeVar,
			GRGEN_MODEL.NodeType_B21.typeVar,
			GRGEN_MODEL.NodeType_B22.typeVar,
			GRGEN_MODEL.NodeType_B23.typeVar,
			GRGEN_MODEL.NodeType_B41.typeVar,
			GRGEN_MODEL.NodeType_B42.typeVar,
			GRGEN_MODEL.NodeType_B43.typeVar,
			GRGEN_MODEL.NodeType_C221.typeVar,
			GRGEN_MODEL.NodeType_C222_411.typeVar,
			GRGEN_MODEL.NodeType_C412_421_431_51.typeVar,
			GRGEN_MODEL.NodeType_C432_422.typeVar,
			GRGEN_MODEL.NodeType_D11_2221.typeVar,
			GRGEN_MODEL.NodeType_D2211_2222_31.typeVar,
			GRGEN_MODEL.NodeType_D231_4121.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GraphElementType[] GRGEN_LIBGR.IGraphElementTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_A1),
			typeof(GRGEN_MODEL.NodeType_A2),
			typeof(GRGEN_MODEL.NodeType_A3),
			typeof(GRGEN_MODEL.NodeType_A4),
			typeof(GRGEN_MODEL.NodeType_A5),
			typeof(GRGEN_MODEL.NodeType_B21),
			typeof(GRGEN_MODEL.NodeType_B22),
			typeof(GRGEN_MODEL.NodeType_B23),
			typeof(GRGEN_MODEL.NodeType_B41),
			typeof(GRGEN_MODEL.NodeType_B42),
			typeof(GRGEN_MODEL.NodeType_B43),
			typeof(GRGEN_MODEL.NodeType_C221),
			typeof(GRGEN_MODEL.NodeType_C222_411),
			typeof(GRGEN_MODEL.NodeType_C412_421_431_51),
			typeof(GRGEN_MODEL.NodeType_C432_422),
			typeof(GRGEN_MODEL.NodeType_D11_2221),
			typeof(GRGEN_MODEL.NodeType_D2211_2222_31),
			typeof(GRGEN_MODEL.NodeType_D231_4121),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.NodeType_A1.AttributeType_a1,
			GRGEN_MODEL.NodeType_A2.AttributeType_a2,
			GRGEN_MODEL.NodeType_A3.AttributeType_a3,
			GRGEN_MODEL.NodeType_A4.AttributeType_a4,
			GRGEN_MODEL.NodeType_A5.AttributeType_a5,
			GRGEN_MODEL.NodeType_B21.AttributeType_b21,
			GRGEN_MODEL.NodeType_B22.AttributeType_b22,
			GRGEN_MODEL.NodeType_B23.AttributeType_b23,
			GRGEN_MODEL.NodeType_B41.AttributeType_b41,
			GRGEN_MODEL.NodeType_B42.AttributeType_b42,
			GRGEN_MODEL.NodeType_C221.AttributeType_c221,
			GRGEN_MODEL.NodeType_C222_411.AttributeType_c222_411,
			GRGEN_MODEL.NodeType_C432_422.AttributeType_c432_422,
			GRGEN_MODEL.NodeType_D11_2221.AttributeType_d11_2221,
			GRGEN_MODEL.NodeType_D2211_2222_31.AttributeType_d2211_2222_31,
			GRGEN_MODEL.NodeType_D231_4121.AttributeType_d231_4121,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge model
	//

	public sealed class complModelEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public complModelEdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public GRGEN_LIBGR.EdgeType RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		public GRGEN_LIBGR.EdgeType GetType(string name)
		{
			switch(name)
			{
				case "AEdge" : return GRGEN_MODEL.EdgeType_AEdge.typeVar;
				case "Edge" : return GRGEN_MODEL.EdgeType_Edge.typeVar;
				case "UEdge" : return GRGEN_MODEL.EdgeType_UEdge.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GraphElementType GRGEN_LIBGR.IGraphElementTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.EdgeType[] types = {
			GRGEN_MODEL.EdgeType_AEdge.typeVar,
			GRGEN_MODEL.EdgeType_Edge.typeVar,
			GRGEN_MODEL.EdgeType_UEdge.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GraphElementType[] GRGEN_LIBGR.IGraphElementTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Object model
	//

	public sealed class complModelObjectModel : GRGEN_LIBGR.IObjectModel
	{
		public complModelObjectModel()
		{
			GRGEN_MODEL.ObjectType_Object.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.subOrSameTypes = new GRGEN_LIBGR.ObjectType[] {
				GRGEN_MODEL.ObjectType_Object.typeVar,
			};
			GRGEN_MODEL.ObjectType_Object.typeVar.directSubGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.directSubTypes = new GRGEN_LIBGR.ObjectType[] {
			};
			GRGEN_MODEL.ObjectType_Object.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.superOrSameTypes = new GRGEN_LIBGR.ObjectType[] {
				GRGEN_MODEL.ObjectType_Object.typeVar,
			};
			GRGEN_MODEL.ObjectType_Object.typeVar.directSuperGrGenTypes = GRGEN_MODEL.ObjectType_Object.typeVar.directSuperTypes = new GRGEN_LIBGR.ObjectType[] {
			};
		}
		public bool IsTransientModel { get { return false; } }
		public GRGEN_LIBGR.ObjectType RootType { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.RootType { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.ObjectType_Object.typeVar; } }
		public GRGEN_LIBGR.ObjectType GetType(string name)
		{
			switch(name)
			{
				case "Object" : return GRGEN_MODEL.ObjectType_Object.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.ObjectType[] types = {
			GRGEN_MODEL.ObjectType_Object.typeVar,
		};
		public GRGEN_LIBGR.ObjectType[] Types { get { return types; } }
		GRGEN_LIBGR.BaseObjectType[] GRGEN_LIBGR.IBaseObjectTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.ObjectType_Object),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// TransientObject model
	//

	public sealed class complModelTransientObjectModel : GRGEN_LIBGR.ITransientObjectModel
	{
		public complModelTransientObjectModel()
		{
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.subOrSameTypes = new GRGEN_LIBGR.TransientObjectType[] {
				GRGEN_MODEL.TransientObjectType_TransientObject.typeVar,
			};
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSubGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSubTypes = new GRGEN_LIBGR.TransientObjectType[] {
			};
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.superOrSameTypes = new GRGEN_LIBGR.TransientObjectType[] {
				GRGEN_MODEL.TransientObjectType_TransientObject.typeVar,
			};
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSuperGrGenTypes = GRGEN_MODEL.TransientObjectType_TransientObject.typeVar.directSuperTypes = new GRGEN_LIBGR.TransientObjectType[] {
			};
		}
		public bool IsTransientModel { get { return true; } }
		public GRGEN_LIBGR.TransientObjectType RootType { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.RootType { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar; } }
		public GRGEN_LIBGR.TransientObjectType GetType(string name)
		{
			switch(name)
			{
				case "TransientObject" : return GRGEN_MODEL.TransientObjectType_TransientObject.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.BaseObjectType GRGEN_LIBGR.IBaseObjectTypeModel.GetType(string name)
		{
			return GetType(name);
		}
		GRGEN_LIBGR.InheritanceType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.TransientObjectType[] types = {
			GRGEN_MODEL.TransientObjectType_TransientObject.typeVar,
		};
		public GRGEN_LIBGR.TransientObjectType[] Types { get { return types; } }
		GRGEN_LIBGR.BaseObjectType[] GRGEN_LIBGR.IBaseObjectTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.TransientObjectType_TransientObject),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel (LGSPGraphModel) implementation
	//
	public sealed class complModelGraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public complModelGraphModel()
		{
			FullyInitializeExternalObjectTypes();
		}

		private complModelNodeModel nodeModel = new complModelNodeModel();
		private complModelEdgeModel edgeModel = new complModelEdgeModel();
		private complModelObjectModel objectModel = new complModelObjectModel();
		private complModelTransientObjectModel transientObjectModel = new complModelTransientObjectModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override GRGEN_LIBGR.IUniquenessHandler CreateUniquenessHandler(GRGEN_LIBGR.IGraph graph) {
			return null;
		}
		public override GRGEN_LIBGR.IIndexSet CreateIndexSet(GRGEN_LIBGR.IGraph graph) {
			return new complModelIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((complModelIndexSet)graph.Indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "complModel"; } }
		public override GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public override GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public override GRGEN_LIBGR.IObjectModel ObjectModel { get { return objectModel; } }
		public override GRGEN_LIBGR.ITransientObjectModel TransientObjectModel { get { return transientObjectModel; } }
		public override IEnumerable<string> Packages { get { return packages; } }
		public override IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public override IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public override IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions { get { return indexDescriptions; } }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) { return indexDescriptions[i]; }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)
 		{
			for(int i=0; i<indexDescriptions.Length; ++i)
				if(indexDescriptions[i].Name==indexName)
					return indexDescriptions[i];
			return null;
		}
		public override bool GraphElementsReferenceContainingGraph { get { return false; } }
		public override bool GraphElementUniquenessIsEnsured { get { return false; } }
		public override bool GraphElementUniquenessIsUserRequested { get { return false; } }
		public override bool ObjectUniquenessIsEnsured { get { return false; } }
		public override bool GraphElementsAreAccessibleByUniqueId { get { return false; } }
		public override bool AreFunctionsParallelized { get { return false; } }
		public override int BranchingFactorForEqualsAny { get { return 0; } }
		public override int ThreadPoolSizeForSequencesParallelExecution { get { return 0; } }

		public static GRGEN_LIBGR.ExternalObjectType externalObjectType_object = new ExternalObjectType_object();
		private GRGEN_LIBGR.ExternalObjectType[] externalObjectTypes = { externalObjectType_object };
		public override GRGEN_LIBGR.ExternalObjectType[] ExternalObjectTypes { get { return externalObjectTypes; } }

		private void FullyInitializeExternalObjectTypes()
		{
			externalObjectType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalObjectType[] { } );
		}

		public override global::System.Collections.IList ArrayOrderAscendingBy(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayOrderAscendingBy((List<GRGEN_MODEL.IA1>)array);
				default:
					return null;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.IA2>)array);
				default:
					return null;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayOrderAscendingBy((List<GRGEN_MODEL.IA3>)array);
				default:
					return null;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.IA4>)array);
				default:
					return null;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayOrderAscendingBy((List<GRGEN_MODEL.IA5>)array);
				default:
					return null;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB21>)array);
				case "b21":
					return ArrayHelper_B21_b21.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB21>)array);
				default:
					return null;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB22>)array);
				case "b22":
					return ArrayHelper_B22_b22.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB22>)array);
				default:
					return null;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB23>)array);
				case "b23":
					return ArrayHelper_B23_b23.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB23>)array);
				default:
					return null;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB41>)array);
				case "b41":
					return ArrayHelper_B41_b41.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB41>)array);
				default:
					return null;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB42>)array);
				case "b42":
					return ArrayHelper_B42_b42.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB42>)array);
				default:
					return null;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.IB43>)array);
				default:
					return null;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC221>)array);
				case "b22":
					return ArrayHelper_C221_b22.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC221>)array);
				case "c221":
					return ArrayHelper_C221_c221.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC221>)array);
				default:
					return null;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC222_411>)array);
				default:
					return null;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				default:
					return null;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC432_422>)array);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC432_422>)array);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayOrderAscendingBy((List<GRGEN_MODEL.IC432_422>)array);
				default:
					return null;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				default:
					return null;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayOrderAscendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override global::System.Collections.IList ArrayOrderDescendingBy(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayOrderDescendingBy((List<GRGEN_MODEL.IA1>)array);
				default:
					return null;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.IA2>)array);
				default:
					return null;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayOrderDescendingBy((List<GRGEN_MODEL.IA3>)array);
				default:
					return null;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.IA4>)array);
				default:
					return null;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayOrderDescendingBy((List<GRGEN_MODEL.IA5>)array);
				default:
					return null;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB21>)array);
				case "b21":
					return ArrayHelper_B21_b21.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB21>)array);
				default:
					return null;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB22>)array);
				case "b22":
					return ArrayHelper_B22_b22.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB22>)array);
				default:
					return null;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB23>)array);
				case "b23":
					return ArrayHelper_B23_b23.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB23>)array);
				default:
					return null;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB41>)array);
				case "b41":
					return ArrayHelper_B41_b41.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB41>)array);
				default:
					return null;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB42>)array);
				case "b42":
					return ArrayHelper_B42_b42.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB42>)array);
				default:
					return null;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.IB43>)array);
				default:
					return null;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC221>)array);
				case "b22":
					return ArrayHelper_C221_b22.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC221>)array);
				case "c221":
					return ArrayHelper_C221_c221.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC221>)array);
				default:
					return null;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC222_411>)array);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC222_411>)array);
				default:
					return null;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				default:
					return null;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC432_422>)array);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC432_422>)array);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayOrderDescendingBy((List<GRGEN_MODEL.IC432_422>)array);
				default:
					return null;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID11_2221>)array);
				default:
					return null;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayOrderDescendingBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override global::System.Collections.IList ArrayGroupBy(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayGroupBy((List<GRGEN_MODEL.IA1>)array);
				default:
					return null;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayGroupBy((List<GRGEN_MODEL.IA2>)array);
				default:
					return null;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayGroupBy((List<GRGEN_MODEL.IA3>)array);
				default:
					return null;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayGroupBy((List<GRGEN_MODEL.IA4>)array);
				default:
					return null;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayGroupBy((List<GRGEN_MODEL.IA5>)array);
				default:
					return null;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayGroupBy((List<GRGEN_MODEL.IB21>)array);
				case "b21":
					return ArrayHelper_B21_b21.ArrayGroupBy((List<GRGEN_MODEL.IB21>)array);
				default:
					return null;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayGroupBy((List<GRGEN_MODEL.IB22>)array);
				case "b22":
					return ArrayHelper_B22_b22.ArrayGroupBy((List<GRGEN_MODEL.IB22>)array);
				default:
					return null;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayGroupBy((List<GRGEN_MODEL.IB23>)array);
				case "b23":
					return ArrayHelper_B23_b23.ArrayGroupBy((List<GRGEN_MODEL.IB23>)array);
				default:
					return null;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayGroupBy((List<GRGEN_MODEL.IB41>)array);
				case "b41":
					return ArrayHelper_B41_b41.ArrayGroupBy((List<GRGEN_MODEL.IB41>)array);
				default:
					return null;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayGroupBy((List<GRGEN_MODEL.IB42>)array);
				case "b42":
					return ArrayHelper_B42_b42.ArrayGroupBy((List<GRGEN_MODEL.IB42>)array);
				default:
					return null;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayGroupBy((List<GRGEN_MODEL.IB43>)array);
				default:
					return null;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayGroupBy((List<GRGEN_MODEL.IC221>)array);
				case "b22":
					return ArrayHelper_C221_b22.ArrayGroupBy((List<GRGEN_MODEL.IC221>)array);
				case "c221":
					return ArrayHelper_C221_c221.ArrayGroupBy((List<GRGEN_MODEL.IC221>)array);
				default:
					return null;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayGroupBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayGroupBy((List<GRGEN_MODEL.IC222_411>)array);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayGroupBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayGroupBy((List<GRGEN_MODEL.IC222_411>)array);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayGroupBy((List<GRGEN_MODEL.IC222_411>)array);
				default:
					return null;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayGroupBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayGroupBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayGroupBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayGroupBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				default:
					return null;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayGroupBy((List<GRGEN_MODEL.IC432_422>)array);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayGroupBy((List<GRGEN_MODEL.IC432_422>)array);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayGroupBy((List<GRGEN_MODEL.IC432_422>)array);
				default:
					return null;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayGroupBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayGroupBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayGroupBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayGroupBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayGroupBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayGroupBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayGroupBy((List<GRGEN_MODEL.ID11_2221>)array);
				default:
					return null;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayGroupBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override global::System.Collections.IList ArrayKeepOneForEach(global::System.Collections.IList array, string member)
		{
			if(array.Count == 0)
				return array;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return null;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return null;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IA1>)array);
				default:
					return null;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IA2>)array);
				default:
					return null;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IA3>)array);
				default:
					return null;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IA4>)array);
				default:
					return null;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IA5>)array);
				default:
					return null;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB21>)array);
				case "b21":
					return ArrayHelper_B21_b21.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB21>)array);
				default:
					return null;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB22>)array);
				case "b22":
					return ArrayHelper_B22_b22.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB22>)array);
				default:
					return null;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB23>)array);
				case "b23":
					return ArrayHelper_B23_b23.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB23>)array);
				default:
					return null;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB41>)array);
				case "b41":
					return ArrayHelper_B41_b41.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB41>)array);
				default:
					return null;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB42>)array);
				case "b42":
					return ArrayHelper_B42_b42.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB42>)array);
				default:
					return null;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IB43>)array);
				default:
					return null;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC221>)array);
				case "b22":
					return ArrayHelper_C221_b22.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC221>)array);
				case "c221":
					return ArrayHelper_C221_c221.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC221>)array);
				default:
					return null;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC222_411>)array);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC222_411>)array);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC222_411>)array);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC222_411>)array);
				default:
					return null;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC412_421_431_51>)array);
				default:
					return null;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC432_422>)array);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC432_422>)array);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IC432_422>)array);
				default:
					return null;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID11_2221>)array);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID11_2221>)array);
				default:
					return null;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayKeepOneForEachBy((List<GRGEN_MODEL.ID2211_2222_31>)array);
				default:
					return null;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Edge":
				switch(member)
				{
				default:
					return null;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return null;
				}
			case "Object":
				switch(member)
				{
				default:
					return null;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return null;
				}
			default: return null;
			}
		}

		public override int ArrayIndexOfBy(global::System.Collections.IList array, string member, object value)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayIndexOfBy((List<GRGEN_MODEL.IA1>)array, (int)value);
				default:
					return -1;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IA2>)array, (int)value);
				default:
					return -1;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayIndexOfBy((List<GRGEN_MODEL.IA3>)array, (int)value);
				default:
					return -1;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IA4>)array, (int)value);
				default:
					return -1;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayIndexOfBy((List<GRGEN_MODEL.IA5>)array, (int)value);
				default:
					return -1;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value);
				case "b21":
					return ArrayHelper_B21_b21.ArrayIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value);
				default:
					return -1;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value);
				case "b22":
					return ArrayHelper_B22_b22.ArrayIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value);
				default:
					return -1;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value);
				case "b23":
					return ArrayHelper_B23_b23.ArrayIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value);
				default:
					return -1;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value);
				case "b41":
					return ArrayHelper_B41_b41.ArrayIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value);
				default:
					return -1;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value);
				case "b42":
					return ArrayHelper_B42_b42.ArrayIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value);
				default:
					return -1;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IB43>)array, (int)value);
				default:
					return -1;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				case "b22":
					return ArrayHelper_C221_b22.ArrayIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				case "c221":
					return ArrayHelper_C221_c221.ArrayIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				default:
					return -1;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				default:
					return -1;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				default:
					return -1;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				default:
					return -1;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				default:
					return -1;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayIndexOfBy(global::System.Collections.IList array, string member, object value, int startIndex)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayIndexOfBy((List<GRGEN_MODEL.IA1>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IA2>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayIndexOfBy((List<GRGEN_MODEL.IA3>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IA4>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayIndexOfBy((List<GRGEN_MODEL.IA5>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value, startIndex);
				case "b21":
					return ArrayHelper_B21_b21.ArrayIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_B22_b22.ArrayIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value, startIndex);
				case "b23":
					return ArrayHelper_B23_b23.ArrayIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_B41_b41.ArrayIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value, startIndex);
				case "b42":
					return ArrayHelper_B42_b42.ArrayIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IB43>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_C221_b22.ArrayIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value, startIndex);
				case "c221":
					return ArrayHelper_C221_c221.ArrayIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value, startIndex);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value, startIndex);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayLastIndexOfBy(global::System.Collections.IList array, string member, object value)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA1>)array, (int)value);
				default:
					return -1;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA2>)array, (int)value);
				default:
					return -1;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA3>)array, (int)value);
				default:
					return -1;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA4>)array, (int)value);
				default:
					return -1;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA5>)array, (int)value);
				default:
					return -1;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value);
				case "b21":
					return ArrayHelper_B21_b21.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value);
				default:
					return -1;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value);
				case "b22":
					return ArrayHelper_B22_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value);
				default:
					return -1;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value);
				case "b23":
					return ArrayHelper_B23_b23.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value);
				default:
					return -1;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value);
				case "b41":
					return ArrayHelper_B41_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value);
				default:
					return -1;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value);
				case "b42":
					return ArrayHelper_B42_b42.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value);
				default:
					return -1;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB43>)array, (int)value);
				default:
					return -1;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				case "b22":
					return ArrayHelper_C221_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				case "c221":
					return ArrayHelper_C221_c221.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				default:
					return -1;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				default:
					return -1;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				default:
					return -1;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				default:
					return -1;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				default:
					return -1;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayLastIndexOfBy(global::System.Collections.IList array, string member, object value, int startIndex)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA1>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA2>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA3>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA4>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayLastIndexOfBy((List<GRGEN_MODEL.IA5>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value, startIndex);
				case "b21":
					return ArrayHelper_B21_b21.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB21>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_B22_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB22>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value, startIndex);
				case "b23":
					return ArrayHelper_B23_b23.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB23>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_B41_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB41>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value, startIndex);
				case "b42":
					return ArrayHelper_B42_b42.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB42>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IB43>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_C221_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value, startIndex);
				case "c221":
					return ArrayHelper_C221_c221.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC221>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC222_411>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value, startIndex);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value, startIndex);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayLastIndexOfBy((List<GRGEN_MODEL.IC432_422>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayLastIndexOfBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}

		public override int ArrayIndexOfOrderedBy(global::System.Collections.IList array, string member, object value)
		{
			if(array.Count == 0)
				return -1;
			if(!(array[0] is GRGEN_LIBGR.IAttributeBearer))
				return -1;
			GRGEN_LIBGR.IAttributeBearer elem = (GRGEN_LIBGR.IAttributeBearer)array[0];
			switch(elem.Type.PackagePrefixedName)
			{
			case "Node":
				switch(member)
				{
				default:
					return -1;
				}
			case "A1":
				switch(member)
				{
				case "a1":
					return ArrayHelper_A1_a1.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IA1>)array, (int)value);
				default:
					return -1;
				}
			case "A2":
				switch(member)
				{
				case "a2":
					return ArrayHelper_A2_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IA2>)array, (int)value);
				default:
					return -1;
				}
			case "A3":
				switch(member)
				{
				case "a3":
					return ArrayHelper_A3_a3.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IA3>)array, (int)value);
				default:
					return -1;
				}
			case "A4":
				switch(member)
				{
				case "a4":
					return ArrayHelper_A4_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IA4>)array, (int)value);
				default:
					return -1;
				}
			case "A5":
				switch(member)
				{
				case "a5":
					return ArrayHelper_A5_a5.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IA5>)array, (int)value);
				default:
					return -1;
				}
			case "B21":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B21_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB21>)array, (int)value);
				case "b21":
					return ArrayHelper_B21_b21.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB21>)array, (int)value);
				default:
					return -1;
				}
			case "B22":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B22_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB22>)array, (int)value);
				case "b22":
					return ArrayHelper_B22_b22.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB22>)array, (int)value);
				default:
					return -1;
				}
			case "B23":
				switch(member)
				{
				case "a2":
					return ArrayHelper_B23_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB23>)array, (int)value);
				case "b23":
					return ArrayHelper_B23_b23.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB23>)array, (int)value);
				default:
					return -1;
				}
			case "B41":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B41_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB41>)array, (int)value);
				case "b41":
					return ArrayHelper_B41_b41.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB41>)array, (int)value);
				default:
					return -1;
				}
			case "B42":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B42_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB42>)array, (int)value);
				case "b42":
					return ArrayHelper_B42_b42.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB42>)array, (int)value);
				default:
					return -1;
				}
			case "B43":
				switch(member)
				{
				case "a4":
					return ArrayHelper_B43_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IB43>)array, (int)value);
				default:
					return -1;
				}
			case "C221":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C221_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				case "b22":
					return ArrayHelper_C221_b22.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				case "c221":
					return ArrayHelper_C221_c221.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC221>)array, (int)value);
				default:
					return -1;
				}
			case "C222_411":
				switch(member)
				{
				case "a2":
					return ArrayHelper_C222_411_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "b22":
					return ArrayHelper_C222_411_b22.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "a4":
					return ArrayHelper_C222_411_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "b41":
					return ArrayHelper_C222_411_b41.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				case "c222_411":
					return ArrayHelper_C222_411_c222_411.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC222_411>)array, (int)value);
				default:
					return -1;
				}
			case "C412_421_431_51":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C412_421_431_51_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "b41":
					return ArrayHelper_C412_421_431_51_b41.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "b42":
					return ArrayHelper_C412_421_431_51_b42.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				case "a5":
					return ArrayHelper_C412_421_431_51_a5.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC412_421_431_51>)array, (int)value);
				default:
					return -1;
				}
			case "C432_422":
				switch(member)
				{
				case "a4":
					return ArrayHelper_C432_422_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				case "b42":
					return ArrayHelper_C432_422_b42.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				case "c432_422":
					return ArrayHelper_C432_422_c432_422.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IC432_422>)array, (int)value);
				default:
					return -1;
				}
			case "D11_2221":
				switch(member)
				{
				case "a1":
					return ArrayHelper_D11_2221_a1.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "a2":
					return ArrayHelper_D11_2221_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "b22":
					return ArrayHelper_D11_2221_b22.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "a4":
					return ArrayHelper_D11_2221_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "b41":
					return ArrayHelper_D11_2221_b41.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "c222_411":
					return ArrayHelper_D11_2221_c222_411.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				case "d11_2221":
					return ArrayHelper_D11_2221_d11_2221.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID11_2221>)array, (int)value);
				default:
					return -1;
				}
			case "D2211_2222_31":
				switch(member)
				{
				case "a2":
					return ArrayHelper_D2211_2222_31_a2.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "b22":
					return ArrayHelper_D2211_2222_31_b22.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "c221":
					return ArrayHelper_D2211_2222_31_c221.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "a4":
					return ArrayHelper_D2211_2222_31_a4.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "b41":
					return ArrayHelper_D2211_2222_31_b41.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "c222_411":
					return ArrayHelper_D2211_2222_31_c222_411.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "a3":
					return ArrayHelper_D2211_2222_31_a3.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				case "d2211_2222_31":
					return ArrayHelper_D2211_2222_31_d2211_2222_31.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.ID2211_2222_31>)array, (int)value);
				default:
					return -1;
				}
			case "AEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Edge":
				switch(member)
				{
				default:
					return -1;
				}
			case "UEdge":
				switch(member)
				{
				default:
					return -1;
				}
			case "Object":
				switch(member)
				{
				default:
					return -1;
				}
			case "TransientObject":
				switch(member)
				{
				default:
					return -1;
				}
			default: return -1;
			}
		}


		public override void FailAssertion() { Debug.Assert(false); }
		public override string MD5Hash { get { return "363c3af47059e8ddde9b24be11c17a1a"; } }
	}

	//
	// IGraph (LGSPGraph) implementation
	//
	public class complModelGraph : GRGEN_LGSP.LGSPGraph
	{
		public complModelGraph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) : base(new complModelGraphModel(), globalVariables, GetGraphName())
		{
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@A1 CreateNodeA1()
		{
			return GRGEN_MODEL.@A1.CreateNode(this);
		}

		public GRGEN_MODEL.@A2 CreateNodeA2()
		{
			return GRGEN_MODEL.@A2.CreateNode(this);
		}

		public GRGEN_MODEL.@A3 CreateNodeA3()
		{
			return GRGEN_MODEL.@A3.CreateNode(this);
		}

		public GRGEN_MODEL.@A4 CreateNodeA4()
		{
			return GRGEN_MODEL.@A4.CreateNode(this);
		}

		public GRGEN_MODEL.@A5 CreateNodeA5()
		{
			return GRGEN_MODEL.@A5.CreateNode(this);
		}

		public GRGEN_MODEL.@B21 CreateNodeB21()
		{
			return GRGEN_MODEL.@B21.CreateNode(this);
		}

		public GRGEN_MODEL.@B22 CreateNodeB22()
		{
			return GRGEN_MODEL.@B22.CreateNode(this);
		}

		public GRGEN_MODEL.@B23 CreateNodeB23()
		{
			return GRGEN_MODEL.@B23.CreateNode(this);
		}

		public GRGEN_MODEL.@B41 CreateNodeB41()
		{
			return GRGEN_MODEL.@B41.CreateNode(this);
		}

		public GRGEN_MODEL.@B42 CreateNodeB42()
		{
			return GRGEN_MODEL.@B42.CreateNode(this);
		}

		public GRGEN_MODEL.@B43 CreateNodeB43()
		{
			return GRGEN_MODEL.@B43.CreateNode(this);
		}

		public GRGEN_MODEL.@C221 CreateNodeC221()
		{
			return GRGEN_MODEL.@C221.CreateNode(this);
		}

		public GRGEN_MODEL.@C222_411 CreateNodeC222_411()
		{
			return GRGEN_MODEL.@C222_411.CreateNode(this);
		}

		public GRGEN_MODEL.@C412_421_431_51 CreateNodeC412_421_431_51()
		{
			return GRGEN_MODEL.@C412_421_431_51.CreateNode(this);
		}

		public GRGEN_MODEL.@C432_422 CreateNodeC432_422()
		{
			return GRGEN_MODEL.@C432_422.CreateNode(this);
		}

		public GRGEN_MODEL.@D11_2221 CreateNodeD11_2221()
		{
			return GRGEN_MODEL.@D11_2221.CreateNode(this);
		}

		public GRGEN_MODEL.@D2211_2222_31 CreateNodeD2211_2222_31()
		{
			return GRGEN_MODEL.@D2211_2222_31.CreateNode(this);
		}

		public GRGEN_MODEL.@D231_4121 CreateNodeD231_4121()
		{
			return GRGEN_MODEL.@D231_4121.CreateNode(this);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

	}

	//
	// INamedGraph (LGSPNamedGraph) implementation
	//
	public class complModelNamedGraph : GRGEN_LGSP.LGSPNamedGraph
	{
		public complModelNamedGraph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) : base(new complModelGraphModel(), globalVariables, GetGraphName(), 0)
		{
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode(string nodeName)
		{
			return GRGEN_MODEL.@Node.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@A1 CreateNodeA1()
		{
			return GRGEN_MODEL.@A1.CreateNode(this);
		}

		public GRGEN_MODEL.@A1 CreateNodeA1(string nodeName)
		{
			return GRGEN_MODEL.@A1.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@A2 CreateNodeA2()
		{
			return GRGEN_MODEL.@A2.CreateNode(this);
		}

		public GRGEN_MODEL.@A2 CreateNodeA2(string nodeName)
		{
			return GRGEN_MODEL.@A2.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@A3 CreateNodeA3()
		{
			return GRGEN_MODEL.@A3.CreateNode(this);
		}

		public GRGEN_MODEL.@A3 CreateNodeA3(string nodeName)
		{
			return GRGEN_MODEL.@A3.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@A4 CreateNodeA4()
		{
			return GRGEN_MODEL.@A4.CreateNode(this);
		}

		public GRGEN_MODEL.@A4 CreateNodeA4(string nodeName)
		{
			return GRGEN_MODEL.@A4.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@A5 CreateNodeA5()
		{
			return GRGEN_MODEL.@A5.CreateNode(this);
		}

		public GRGEN_MODEL.@A5 CreateNodeA5(string nodeName)
		{
			return GRGEN_MODEL.@A5.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@B21 CreateNodeB21()
		{
			return GRGEN_MODEL.@B21.CreateNode(this);
		}

		public GRGEN_MODEL.@B21 CreateNodeB21(string nodeName)
		{
			return GRGEN_MODEL.@B21.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@B22 CreateNodeB22()
		{
			return GRGEN_MODEL.@B22.CreateNode(this);
		}

		public GRGEN_MODEL.@B22 CreateNodeB22(string nodeName)
		{
			return GRGEN_MODEL.@B22.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@B23 CreateNodeB23()
		{
			return GRGEN_MODEL.@B23.CreateNode(this);
		}

		public GRGEN_MODEL.@B23 CreateNodeB23(string nodeName)
		{
			return GRGEN_MODEL.@B23.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@B41 CreateNodeB41()
		{
			return GRGEN_MODEL.@B41.CreateNode(this);
		}

		public GRGEN_MODEL.@B41 CreateNodeB41(string nodeName)
		{
			return GRGEN_MODEL.@B41.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@B42 CreateNodeB42()
		{
			return GRGEN_MODEL.@B42.CreateNode(this);
		}

		public GRGEN_MODEL.@B42 CreateNodeB42(string nodeName)
		{
			return GRGEN_MODEL.@B42.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@B43 CreateNodeB43()
		{
			return GRGEN_MODEL.@B43.CreateNode(this);
		}

		public GRGEN_MODEL.@B43 CreateNodeB43(string nodeName)
		{
			return GRGEN_MODEL.@B43.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@C221 CreateNodeC221()
		{
			return GRGEN_MODEL.@C221.CreateNode(this);
		}

		public GRGEN_MODEL.@C221 CreateNodeC221(string nodeName)
		{
			return GRGEN_MODEL.@C221.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@C222_411 CreateNodeC222_411()
		{
			return GRGEN_MODEL.@C222_411.CreateNode(this);
		}

		public GRGEN_MODEL.@C222_411 CreateNodeC222_411(string nodeName)
		{
			return GRGEN_MODEL.@C222_411.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@C412_421_431_51 CreateNodeC412_421_431_51()
		{
			return GRGEN_MODEL.@C412_421_431_51.CreateNode(this);
		}

		public GRGEN_MODEL.@C412_421_431_51 CreateNodeC412_421_431_51(string nodeName)
		{
			return GRGEN_MODEL.@C412_421_431_51.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@C432_422 CreateNodeC432_422()
		{
			return GRGEN_MODEL.@C432_422.CreateNode(this);
		}

		public GRGEN_MODEL.@C432_422 CreateNodeC432_422(string nodeName)
		{
			return GRGEN_MODEL.@C432_422.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@D11_2221 CreateNodeD11_2221()
		{
			return GRGEN_MODEL.@D11_2221.CreateNode(this);
		}

		public GRGEN_MODEL.@D11_2221 CreateNodeD11_2221(string nodeName)
		{
			return GRGEN_MODEL.@D11_2221.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@D2211_2222_31 CreateNodeD2211_2222_31()
		{
			return GRGEN_MODEL.@D2211_2222_31.CreateNode(this);
		}

		public GRGEN_MODEL.@D2211_2222_31 CreateNodeD2211_2222_31(string nodeName)
		{
			return GRGEN_MODEL.@D2211_2222_31.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@D231_4121 CreateNodeD231_4121()
		{
			return GRGEN_MODEL.@D231_4121.CreateNode(this);
		}

		public GRGEN_MODEL.@D231_4121 CreateNodeD231_4121(string nodeName)
		{
			return GRGEN_MODEL.@D231_4121.CreateNode(this, nodeName);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target, edgeName);
		}

	}
}
