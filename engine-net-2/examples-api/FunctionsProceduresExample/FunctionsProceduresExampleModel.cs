// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\FunctionsProceduresExample\FunctionsProceduresExample.grg" on Thu Jan 06 09:31:54 CET 2022

using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Diagnostics;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_FunctionsProceduresExample;

namespace de.unika.ipd.grGen.Model_FunctionsProceduresExample
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

	public enum NodeTypes { @Node=0, @N=1, @NN=2 };

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
		public static bool[] isA = new bool[] { true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@Node"; } }
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

	// *** Node N ***

	public interface IN : GRGEN_LIBGR.INode
	{
		int @i { get; set; }
		int foo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, int var_j);
		void bar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, int var_j);
	}

	public sealed partial class @N : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IN
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@N[] pool;

		// explicit initializations of N for target N
		// implicit initializations of N for target N
		static @N() {
		}

		public @N() : base(GRGEN_MODEL.NodeType_N.typeVar)
		{
			// implicit initialization, container creation of N
			// explicit initializations of N for target N
		}

		public static GRGEN_MODEL.NodeType_N TypeInstance { get { return GRGEN_MODEL.NodeType_N.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@N(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@N(this, graph, oldToNewObjectMap);
		}

		private @N(GRGEN_MODEL.@N oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_N.typeVar)
		{
			i_M0no_suXx_h4rD = oldElem.i_M0no_suXx_h4rD;
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
			if(!(that is @N))
				return false;
			@N that_ = (@N)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& i_M0no_suXx_h4rD == that_.i_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@N CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@N node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@N();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@N[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of N
				node.@i = 0;
				// explicit initializations of N for target N
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@N CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@N node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@N();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@N[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of N
				node.@i = 0;
				// explicit initializations of N for target N
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@N[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int i_M0no_suXx_h4rD;
		public int @i
		{
			get { return i_M0no_suXx_h4rD; }
			set { i_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "i": return this.@i;
			}
			throw new NullReferenceException(
				"The Node type \"N\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "i": this.@i = (int) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"N\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of N
			this.@i = 0;
			// explicit initializations of N for target N
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "foo":
					return @foo(actionEnv, graph, (int)arguments[0]);
				default: throw new NullReferenceException("N does not have the function method " + name + "!");
			}
		}

		public int foo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			return ((this.@i + var_j) + 1);
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "bar":
				{
					@bar(actionEnv, graph, (int)arguments[0]);
					return ReturnArray_bar_N;
				}
				default: throw new NullReferenceException("N does not have the procedure method " + name + "!");
			}
		}
		private static object[] ReturnArray_bar_N = new object[0]; // helper array for multi-value-returns, to allow for contravariant parameter assignment

		public void bar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("bar", var_j);
			int tempvar_0 = (int )var_j;
			graph.ChangingNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_i, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@i = tempvar_0;
			graph.ChangedNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_i);
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("bar");
			return;
		}
	}

	public sealed partial class NodeType_N : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_N typeVar = new GRGEN_MODEL.NodeType_N();
		public static bool[] isA = new bool[] { true, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_i;
		public NodeType_N() : base((int) NodeTypes.@N)
		{
			AttributeType_i = new GRGEN_LIBGR.AttributeType("i", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "N"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "N"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.IN"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@N"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@N();
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
				yield return AttributeType_i;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "i" : return AttributeType_i;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_foo_N.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "foo" : return FunctionMethodInfo_foo_N.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_bar_N.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "bar" : return ProcedureMethodInfo_bar_N.Instance;
			}
			return null;
}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@N newNode = new GRGEN_MODEL.@N();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@N:
				case (int) GRGEN_MODEL.NodeTypes.@NN:
					// copy attributes for: N
					{
						GRGEN_MODEL.IN old = (GRGEN_MODEL.IN) oldNode;
						newNode.@i = old.@i;
					}
					break;
			}
			return newNode;
		}

	}
	public class FunctionMethodInfo_foo_N : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_foo_N instance = null;
		public static FunctionMethodInfo_foo_N Instance { get { if(instance==null) { instance = new FunctionMethodInfo_foo_N(); } return instance; } }

		private FunctionMethodInfo_foo_N()
			: base(
				"foo",
				null, "foo",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				GRGEN_LIBGR.VarType.GetVarType(typeof(int))
			)
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_bar_N : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_bar_N instance = null;
		public static ProcedureMethodInfo_bar_N Instance { get { if(instance==null) { instance = new ProcedureMethodInfo_bar_N(); } return instance; } }

		private ProcedureMethodInfo_bar_N()
			: base(
				"bar",
				null, "bar",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				new GRGEN_LIBGR.GrGenType[] {  }
			)
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	public class Comparer_N_i : Comparer<GRGEN_MODEL.IN>
	{
		public static Comparer_N_i thisComparer = new Comparer_N_i();
		public override int Compare(GRGEN_MODEL.IN a, GRGEN_MODEL.IN b)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ReverseComparer_N_i : Comparer<GRGEN_MODEL.IN>
	{
		public static ReverseComparer_N_i thisComparer = new ReverseComparer_N_i();
		public override int Compare(GRGEN_MODEL.IN b, GRGEN_MODEL.IN a)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ArrayHelper_N_i
	{
		private static GRGEN_MODEL.IN instanceBearingAttributeForSearch = new GRGEN_MODEL.@N();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IN> list, int entry)
		{
			instanceBearingAttributeForSearch.@i = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_N_i.thisComparer);
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderAscendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(Comparer_N_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayOrderDescendingBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>(list);
			newList.Sort(ReverseComparer_N_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayGroupBy(List<GRGEN_MODEL.IN> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IN>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@i)) {
					seenValues[list[pos].@i].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IN> tempList = new List<GRGEN_MODEL.IN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@i, tempList);
				}
			}
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			foreach(List<GRGEN_MODEL.IN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IN> list)
		{
			List<GRGEN_MODEL.IN> newList = new List<GRGEN_MODEL.IN>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@i)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@i, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IN> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IN entry in list)
				resultList.Add(entry.@i);
			return resultList;
		}
	}


	// *** Node NN ***

	public interface INN : IN
	{
		string @s { get; set; }
		void bla(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string var_t, out string _out_param_0);
	}

	public sealed partial class @NN : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.INN
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@NN[] pool;

		// explicit initializations of N for target NN
		// implicit initializations of N for target NN
		// explicit initializations of NN for target NN
		// implicit initializations of NN for target NN
		static @NN() {
		}

		public @NN() : base(GRGEN_MODEL.NodeType_NN.typeVar)
		{
			// implicit initialization, container creation of NN
			// explicit initializations of N for target NN
			// explicit initializations of NN for target NN
		}

		public static GRGEN_MODEL.NodeType_NN TypeInstance { get { return GRGEN_MODEL.NodeType_NN.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() {
			return new GRGEN_MODEL.@NN(this, null, null);
		}

		public override GRGEN_LIBGR.INode Copy(GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@NN(this, graph, oldToNewObjectMap);
		}

		private @NN(GRGEN_MODEL.@NN oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.NodeType_NN.typeVar)
		{
			i_M0no_suXx_h4rD = oldElem.i_M0no_suXx_h4rD;
			s_M0no_suXx_h4rD = oldElem.s_M0no_suXx_h4rD;
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
			if(!(that is @NN))
				return false;
			@NN that_ = (@NN)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& i_M0no_suXx_h4rD == that_.i_M0no_suXx_h4rD
				&& s_M0no_suXx_h4rD == that_.s_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@NN CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@NN node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@NN();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@NN[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of NN
				node.@i = 0;
				node.@s = null;
				// explicit initializations of N for target NN
				// explicit initializations of NN for target NN
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@NN CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@NN node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@NN();
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@NN[GRGEN_LGSP.LGSPGraph.poolSize];
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of NN
				node.@i = 0;
				node.@s = null;
				// explicit initializations of N for target NN
				// explicit initializations of NN for target NN
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@NN[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int i_M0no_suXx_h4rD;
		public int @i
		{
			get { return i_M0no_suXx_h4rD; }
			set { i_M0no_suXx_h4rD = value; }
		}

		private string s_M0no_suXx_h4rD;
		public string @s
		{
			get { return s_M0no_suXx_h4rD; }
			set { s_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "i": return this.@i;
				case "s": return this.@s;
			}
			throw new NullReferenceException(
				"The Node type \"NN\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "i": this.@i = (int) value; return;
				case "s": this.@s = (string) value; return;
			}
			throw new NullReferenceException(
				"The Node type \"NN\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of NN
			this.@i = 0;
			this.@s = null;
			// explicit initializations of N for target NN
			// explicit initializations of NN for target NN
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "foo":
					return @foo(actionEnv, graph, (int)arguments[0]);
				default: throw new NullReferenceException("NN does not have the function method " + name + "!");
			}
		}

		public int foo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			return ((this.@i + var_j) + (this.@s).Length);
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "bar":
				{
					@bar(actionEnv, graph, (int)arguments[0]);
					return ReturnArray_bar_NN;
				}
				case "bla":
				{
					string _out_param_0;
					@bla(actionEnv, graph, (string)arguments[0], out _out_param_0);
					ReturnArray_bla_NN[0] = _out_param_0;
					return ReturnArray_bla_NN;
				}
				default: throw new NullReferenceException("NN does not have the procedure method " + name + "!");
			}
		}
		private static object[] ReturnArray_bar_NN = new object[0]; // helper array for multi-value-returns, to allow for contravariant parameter assignment
		private static object[] ReturnArray_bla_NN = new object[1]; // helper array for multi-value-returns, to allow for contravariant parameter assignment

		public void bar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("bar", var_j);
			int tempvar_0 = (int )var_j;
			graph.ChangingNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_i, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@i = tempvar_0;
			graph.ChangedNodeAttribute(this, GRGEN_MODEL.NodeType_N.AttributeType_i);
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("bar");
			return;
		}

		public void bla(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_t, out string _out_param_0)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("bla", var_t);
			string var_stemp = (string)(this.@s);
			string tempvar_0 = (string )var_t;
			graph.ChangingNodeAttribute(this, GRGEN_MODEL.NodeType_NN.AttributeType_s, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@s = tempvar_0;
			graph.ChangedNodeAttribute(this, GRGEN_MODEL.NodeType_NN.AttributeType_s);
			_out_param_0 = var_stemp;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("bla", _out_param_0);
			return;
		}
	}

	public sealed partial class NodeType_NN : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_NN typeVar = new GRGEN_MODEL.NodeType_NN();
		public static bool[] isA = new bool[] { true, true, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_s;
		public NodeType_NN() : base((int) NodeTypes.@NN)
		{
			AttributeType_s = new GRGEN_LIBGR.AttributeType("s", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
		}
		public override string Name { get { return "NN"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "NN"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.INN"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@NN"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@NN();
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
				yield return GRGEN_MODEL.NodeType_N.AttributeType_i;
				yield return AttributeType_s;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "i" : return GRGEN_MODEL.NodeType_N.AttributeType_i;
				case "s" : return AttributeType_s;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_foo_NN.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "foo" : return FunctionMethodInfo_foo_NN.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_bar_NN.Instance;
				yield return ProcedureMethodInfo_bla_NN.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "bar" : return ProcedureMethodInfo_bar_NN.Instance;
				case "bla" : return ProcedureMethodInfo_bla_NN.Instance;
			}
			return null;
}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@NN newNode = new GRGEN_MODEL.@NN();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@N:
					// copy attributes for: N
					{
						GRGEN_MODEL.IN old = (GRGEN_MODEL.IN) oldNode;
						newNode.@i = old.@i;
					}
					break;
				case (int) GRGEN_MODEL.NodeTypes.@NN:
					// copy attributes for: NN
					{
						GRGEN_MODEL.INN old = (GRGEN_MODEL.INN) oldNode;
						newNode.@i = old.@i;
						newNode.@s = old.@s;
					}
					break;
			}
			return newNode;
		}

	}
	public class FunctionMethodInfo_foo_NN : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_foo_NN instance = null;
		public static FunctionMethodInfo_foo_NN Instance { get { if(instance==null) { instance = new FunctionMethodInfo_foo_NN(); } return instance; } }

		private FunctionMethodInfo_foo_NN()
			: base(
				"foo",
				null, "foo",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				GRGEN_LIBGR.VarType.GetVarType(typeof(int))
			)
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_bar_NN : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_bar_NN instance = null;
		public static ProcedureMethodInfo_bar_NN Instance { get { if(instance==null) { instance = new ProcedureMethodInfo_bar_NN(); } return instance; } }

		private ProcedureMethodInfo_bar_NN()
			: base(
				"bar",
				null, "bar",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				new GRGEN_LIBGR.GrGenType[] {  }
			)
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}

	public class ProcedureMethodInfo_bla_NN : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_bla_NN instance = null;
		public static ProcedureMethodInfo_bla_NN Instance { get { if(instance==null) { instance = new ProcedureMethodInfo_bla_NN(); } return instance; } }

		private ProcedureMethodInfo_bla_NN()
			: base(
				"bla",
				null, "bla",
				false,
				new String[] { "t",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  }
			)
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	public class Comparer_NN_i : Comparer<GRGEN_MODEL.INN>
	{
		public static Comparer_NN_i thisComparer = new Comparer_NN_i();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ReverseComparer_NN_i : Comparer<GRGEN_MODEL.INN>
	{
		public static ReverseComparer_NN_i thisComparer = new ReverseComparer_NN_i();
		public override int Compare(GRGEN_MODEL.INN b, GRGEN_MODEL.INN a)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ArrayHelper_NN_i
	{
		private static GRGEN_MODEL.INN instanceBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.INN> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.INN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.INN> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.INN> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.INN> list, int entry)
		{
			instanceBearingAttributeForSearch.@i = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_NN_i.thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(Comparer_NN_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderDescendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(ReverseComparer_NN_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.INN> ArrayGroupBy(List<GRGEN_MODEL.INN> list)
		{
			Dictionary<int, List<GRGEN_MODEL.INN>> seenValues = new Dictionary<int, List<GRGEN_MODEL.INN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@i)) {
					seenValues[list[pos].@i].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.INN> tempList = new List<GRGEN_MODEL.INN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@i, tempList);
				}
			}
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>();
			foreach(List<GRGEN_MODEL.INN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.INN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.INN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@i)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@i, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.INN> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.INN entry in list)
				resultList.Add(entry.@i);
			return resultList;
		}
	}


	public class Comparer_NN_s : Comparer<GRGEN_MODEL.INN>
	{
		public static Comparer_NN_s thisComparer = new Comparer_NN_s();
		public override int Compare(GRGEN_MODEL.INN a, GRGEN_MODEL.INN b)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
	}

	public class ReverseComparer_NN_s : Comparer<GRGEN_MODEL.INN>
	{
		public static ReverseComparer_NN_s thisComparer = new ReverseComparer_NN_s();
		public override int Compare(GRGEN_MODEL.INN b, GRGEN_MODEL.INN a)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
	}

	public class ArrayHelper_NN_s
	{
		private static GRGEN_MODEL.INN instanceBearingAttributeForSearch = new GRGEN_MODEL.@NN();
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.INN> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.INN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.INN> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.INN> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.INN> list, string entry)
		{
			instanceBearingAttributeForSearch.@s = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_NN_s.thisComparer);
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderAscendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(Comparer_NN_s.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.INN> ArrayOrderDescendingBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>(list);
			newList.Sort(ReverseComparer_NN_s.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.INN> ArrayGroupBy(List<GRGEN_MODEL.INN> list)
		{
			Dictionary<string, List<GRGEN_MODEL.INN>> seenValues = new Dictionary<string, List<GRGEN_MODEL.INN>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@s)) {
					seenValues[list[pos].@s].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.INN> tempList = new List<GRGEN_MODEL.INN>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@s, tempList);
				}
			}
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>();
			foreach(List<GRGEN_MODEL.INN> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.INN> ArrayKeepOneForEachBy(List<GRGEN_MODEL.INN> list)
		{
			List<GRGEN_MODEL.INN> newList = new List<GRGEN_MODEL.INN>();
			Dictionary<string, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<string, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.INN element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@s)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@s, null);
				}
			}
			return newList;
		}
		public static List<string> Extract(List<GRGEN_MODEL.INN> list)
		{
			List<string> resultList = new List<string>(list.Count);
			foreach(GRGEN_MODEL.INN entry in list)
				resultList.Add(entry.@s);
			return resultList;
		}
	}


	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge=0, @Edge=1, @UEdge=2, @E=3, @EE=4 };

	// *** Edge AEdge ***


	public sealed partial class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, true, };
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
		public static bool[] isA = new bool[] { true, true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IDEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@Edge"; } }
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
		public static bool[] isA = new bool[] { true, false, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IUEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@UEdge"; } }
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

	// *** Edge E ***

	public interface IE : GRGEN_LIBGR.IDEdge
	{
		int @i { get; set; }
		int foo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, int var_j);
		void bar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, int var_j);
	}

	public sealed partial class @E : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IE
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@E[] pool;

		// explicit initializations of E for target E
		// implicit initializations of E for target E
		static @E() {
		}

		public @E(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_E.typeVar, source, target)
		{
			// implicit initialization, container creation of E
			// explicit initializations of E for target E
		}

		public static GRGEN_MODEL.EdgeType_E TypeInstance { get { return GRGEN_MODEL.EdgeType_E.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {
			return new GRGEN_MODEL.@E(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);
		}

		public override GRGEN_LIBGR.IEdge Copy(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@E(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);
		}

		private @E(GRGEN_MODEL.@E oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
			: base(GRGEN_MODEL.EdgeType_E.typeVar, newSource, newTarget)
		{
			i_M0no_suXx_h4rD = oldElem.i_M0no_suXx_h4rD;
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
			if(!(that is @E))
				return false;
			@E that_ = (@E)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& i_M0no_suXx_h4rD == that_.i_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@E CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@E edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@E(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@E[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of E
				edge.@i = 0;
				// explicit initializations of E for target E
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@E CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@E edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@E(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@E[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of E
				edge.@i = 0;
				// explicit initializations of E for target E
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@E[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int i_M0no_suXx_h4rD;
		public int @i
		{
			get { return i_M0no_suXx_h4rD; }
			set { i_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "i": return this.@i;
			}
			throw new NullReferenceException(
				"The Edge type \"E\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "i": this.@i = (int) value; return;
			}
			throw new NullReferenceException(
				"The Edge type \"E\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of E
			this.@i = 0;
			// explicit initializations of E for target E
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "foo":
					return @foo(actionEnv, graph, (int)arguments[0]);
				default: throw new NullReferenceException("E does not have the function method " + name + "!");
			}
		}

		public int foo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			return ((this.@i + var_j) + 1);
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "bar":
				{
					@bar(actionEnv, graph, (int)arguments[0]);
					return ReturnArray_bar_E;
				}
				default: throw new NullReferenceException("E does not have the procedure method " + name + "!");
			}
		}
		private static object[] ReturnArray_bar_E = new object[0]; // helper array for multi-value-returns, to allow for contravariant parameter assignment

		public void bar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("bar", var_j);
			int tempvar_0 = (int )var_j;
			graph.ChangingEdgeAttribute(this, GRGEN_MODEL.EdgeType_E.AttributeType_i, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@i = tempvar_0;
			graph.ChangedEdgeAttribute(this, GRGEN_MODEL.EdgeType_E.AttributeType_i);
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("bar");
			return;
		}
	}

	public sealed partial class EdgeType_E : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_E typeVar = new GRGEN_MODEL.EdgeType_E();
		public static bool[] isA = new bool[] { true, true, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_i;
		public EdgeType_E() : base((int) EdgeTypes.@E)
		{
			AttributeType_i = new GRGEN_LIBGR.AttributeType("i", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "E"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "E"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.IE"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@E"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@E((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}


		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
				yield return AttributeType_i;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "i" : return AttributeType_i;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_foo_E.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "foo" : return FunctionMethodInfo_foo_E.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_bar_E.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "bar" : return ProcedureMethodInfo_bar_E.Instance;
			}
			return null;
}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;
			GRGEN_MODEL.@E newEdge = new GRGEN_MODEL.@E((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
			switch(oldEdge.Type.TypeID)
			{
				case (int) GRGEN_MODEL.EdgeTypes.@E:
				case (int) GRGEN_MODEL.EdgeTypes.@EE:
					// copy attributes for: E
					{
						GRGEN_MODEL.IE old = (GRGEN_MODEL.IE) oldEdge;
						newEdge.@i = old.@i;
					}
					break;
			}
			return newEdge;
		}

	}
	public class FunctionMethodInfo_foo_E : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_foo_E instance = null;
		public static FunctionMethodInfo_foo_E Instance { get { if(instance==null) { instance = new FunctionMethodInfo_foo_E(); } return instance; } }

		private FunctionMethodInfo_foo_E()
			: base(
				"foo",
				null, "foo",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				GRGEN_LIBGR.VarType.GetVarType(typeof(int))
			)
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_bar_E : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_bar_E instance = null;
		public static ProcedureMethodInfo_bar_E Instance { get { if(instance==null) { instance = new ProcedureMethodInfo_bar_E(); } return instance; } }

		private ProcedureMethodInfo_bar_E()
			: base(
				"bar",
				null, "bar",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				new GRGEN_LIBGR.GrGenType[] {  }
			)
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	public class Comparer_E_i : Comparer<GRGEN_MODEL.IE>
	{
		public static Comparer_E_i thisComparer = new Comparer_E_i();
		public override int Compare(GRGEN_MODEL.IE a, GRGEN_MODEL.IE b)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ReverseComparer_E_i : Comparer<GRGEN_MODEL.IE>
	{
		public static ReverseComparer_E_i thisComparer = new ReverseComparer_E_i();
		public override int Compare(GRGEN_MODEL.IE b, GRGEN_MODEL.IE a)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ArrayHelper_E_i
	{
		private static GRGEN_MODEL.IE instanceBearingAttributeForSearch = new GRGEN_MODEL.@E(null, null);
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IE> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IE> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IE> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IE> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IE> list, int entry)
		{
			instanceBearingAttributeForSearch.@i = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_E_i.thisComparer);
		}
		public static List<GRGEN_MODEL.IE> ArrayOrderAscendingBy(List<GRGEN_MODEL.IE> list)
		{
			List<GRGEN_MODEL.IE> newList = new List<GRGEN_MODEL.IE>(list);
			newList.Sort(Comparer_E_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IE> ArrayOrderDescendingBy(List<GRGEN_MODEL.IE> list)
		{
			List<GRGEN_MODEL.IE> newList = new List<GRGEN_MODEL.IE>(list);
			newList.Sort(ReverseComparer_E_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IE> ArrayGroupBy(List<GRGEN_MODEL.IE> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IE>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IE>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@i)) {
					seenValues[list[pos].@i].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IE> tempList = new List<GRGEN_MODEL.IE>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@i, tempList);
				}
			}
			List<GRGEN_MODEL.IE> newList = new List<GRGEN_MODEL.IE>();
			foreach(List<GRGEN_MODEL.IE> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IE> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IE> list)
		{
			List<GRGEN_MODEL.IE> newList = new List<GRGEN_MODEL.IE>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IE element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@i)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@i, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IE> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IE entry in list)
				resultList.Add(entry.@i);
			return resultList;
		}
	}


	// *** Edge EE ***

	public interface IEE : IE
	{
		string @s { get; set; }
		void bla(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string var_t, out string _out_param_0);
	}

	public sealed partial class @EE : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IEE
	{
		[ThreadStatic] private static int poolLevel;
		[ThreadStatic] private static GRGEN_MODEL.@EE[] pool;

		// explicit initializations of E for target EE
		// implicit initializations of E for target EE
		// explicit initializations of EE for target EE
		// implicit initializations of EE for target EE
		static @EE() {
		}

		public @EE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_EE.typeVar, source, target)
		{
			// implicit initialization, container creation of EE
			// explicit initializations of E for target EE
			// explicit initializations of EE for target EE
		}

		public static GRGEN_MODEL.EdgeType_EE TypeInstance { get { return GRGEN_MODEL.EdgeType_EE.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget) {
			return new GRGEN_MODEL.@EE(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, null, null);
		}

		public override GRGEN_LIBGR.IEdge Copy(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) {
			return new GRGEN_MODEL.@EE(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget, graph, oldToNewObjectMap);
		}

		private @EE(GRGEN_MODEL.@EE oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap)
			: base(GRGEN_MODEL.EdgeType_EE.typeVar, newSource, newTarget)
		{
			i_M0no_suXx_h4rD = oldElem.i_M0no_suXx_h4rD;
			s_M0no_suXx_h4rD = oldElem.s_M0no_suXx_h4rD;
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
			if(!(that is @EE))
				return false;
			@EE that_ = (@EE)that;
			visitedObjects.Add(this, null);
			if(that != this)
				visitedObjects.Add(that, null);
			bool result = true
				&& i_M0no_suXx_h4rD == that_.i_M0no_suXx_h4rD
				&& s_M0no_suXx_h4rD == that_.s_M0no_suXx_h4rD
				;
			visitedObjects.Remove(this);
			visitedObjects.Remove(that);
			return result;
		}

		public static GRGEN_MODEL.@EE CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@EE edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@EE(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@EE[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of EE
				edge.@i = 0;
				edge.@s = null;
				// explicit initializations of E for target EE
				// explicit initializations of EE for target EE
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@EE CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@EE edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@EE(source, target);
			else
			{
				if(pool == null)
					pool = new GRGEN_MODEL.@EE[GRGEN_LGSP.LGSPGraph.poolSize];
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of EE
				edge.@i = 0;
				edge.@s = null;
				// explicit initializations of E for target EE
				// explicit initializations of EE for target EE
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(pool == null)
				pool = new GRGEN_MODEL.@EE[GRGEN_LGSP.LGSPGraph.poolSize];
			if(poolLevel < pool.Length)
				pool[poolLevel++] = this;
		}


		private int i_M0no_suXx_h4rD;
		public int @i
		{
			get { return i_M0no_suXx_h4rD; }
			set { i_M0no_suXx_h4rD = value; }
		}

		private string s_M0no_suXx_h4rD;
		public string @s
		{
			get { return s_M0no_suXx_h4rD; }
			set { s_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "i": return this.@i;
				case "s": return this.@s;
			}
			throw new NullReferenceException(
				"The Edge type \"EE\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "i": this.@i = (int) value; return;
				case "s": this.@s = (string) value; return;
			}
			throw new NullReferenceException(
				"The Edge type \"EE\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of EE
			this.@i = 0;
			this.@s = null;
			// explicit initializations of E for target EE
			// explicit initializations of EE for target EE
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "foo":
					return @foo(actionEnv, graph, (int)arguments[0]);
				default: throw new NullReferenceException("EE does not have the function method " + name + "!");
			}
		}

		public int foo(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			return ((this.@i + var_j) + (this.@s).Length);
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				case "bar":
				{
					@bar(actionEnv, graph, (int)arguments[0]);
					return ReturnArray_bar_EE;
				}
				case "bla":
				{
					string _out_param_0;
					@bla(actionEnv, graph, (string)arguments[0], out _out_param_0);
					ReturnArray_bla_EE[0] = _out_param_0;
					return ReturnArray_bla_EE;
				}
				default: throw new NullReferenceException("EE does not have the procedure method " + name + "!");
			}
		}
		private static object[] ReturnArray_bar_EE = new object[0]; // helper array for multi-value-returns, to allow for contravariant parameter assignment
		private static object[] ReturnArray_bla_EE = new object[1]; // helper array for multi-value-returns, to allow for contravariant parameter assignment

		public void bar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, int var_j)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("bar", var_j);
			int tempvar_0 = (int )var_j;
			graph.ChangingEdgeAttribute(this, GRGEN_MODEL.EdgeType_E.AttributeType_i, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@i = tempvar_0;
			graph.ChangedEdgeAttribute(this, GRGEN_MODEL.EdgeType_E.AttributeType_i);
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("bar");
			return;
		}

		public void bla(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv_, GRGEN_LIBGR.IGraph graph_, string var_t, out string _out_param_0)
		{
			GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv = (GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv_;
			GRGEN_LGSP.LGSPGraph graph = (GRGEN_LGSP.LGSPGraph)graph_;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("bla", var_t);
			string var_stemp = (string)(this.@s);
			string tempvar_0 = (string )var_t;
			graph.ChangingEdgeAttribute(this, GRGEN_MODEL.EdgeType_EE.AttributeType_s, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			this.@s = tempvar_0;
			graph.ChangedEdgeAttribute(this, GRGEN_MODEL.EdgeType_EE.AttributeType_s);
			_out_param_0 = var_stemp;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("bla", _out_param_0);
			return;
		}
	}

	public sealed partial class EdgeType_EE : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_EE typeVar = new GRGEN_MODEL.EdgeType_EE();
		public static bool[] isA = new bool[] { true, true, false, true, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_s;
		public EdgeType_EE() : base((int) EdgeTypes.@EE)
		{
			AttributeType_s = new GRGEN_LIBGR.AttributeType("s", this, GRGEN_LIBGR.AttributeKind.StringAttr, null, null, null, null, null, null, typeof(string));
		}
		public override string Name { get { return "EE"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "EE"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.IEE"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@EE"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@EE((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}


		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
				yield return GRGEN_MODEL.EdgeType_E.AttributeType_i;
				yield return AttributeType_s;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "i" : return GRGEN_MODEL.EdgeType_E.AttributeType_i;
				case "s" : return AttributeType_s;
			}
			return null;
		}
		public override int NumFunctionMethods { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods
		{
			get
			{
				yield return FunctionMethodInfo_foo_EE.Instance;
			}
		}
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name)
		{
			switch(name)
			{
				case "foo" : return FunctionMethodInfo_foo_EE.Instance;
			}
			return null;
		}
		public override int NumProcedureMethods { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods
		{
			get
			{
				yield return ProcedureMethodInfo_bar_EE.Instance;
				yield return ProcedureMethodInfo_bla_EE.Instance;
			}
		}
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name)
		{
			switch(name)
			{
				case "bar" : return ProcedureMethodInfo_bar_EE.Instance;
				case "bla" : return ProcedureMethodInfo_bla_EE.Instance;
			}
			return null;
}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			GRGEN_LGSP.LGSPEdge oldEdge = (GRGEN_LGSP.LGSPEdge) oldIEdge;
			GRGEN_MODEL.@EE newEdge = new GRGEN_MODEL.@EE((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
			switch(oldEdge.Type.TypeID)
			{
				case (int) GRGEN_MODEL.EdgeTypes.@E:
					// copy attributes for: E
					{
						GRGEN_MODEL.IE old = (GRGEN_MODEL.IE) oldEdge;
						newEdge.@i = old.@i;
					}
					break;
				case (int) GRGEN_MODEL.EdgeTypes.@EE:
					// copy attributes for: EE
					{
						GRGEN_MODEL.IEE old = (GRGEN_MODEL.IEE) oldEdge;
						newEdge.@i = old.@i;
						newEdge.@s = old.@s;
					}
					break;
			}
			return newEdge;
		}

	}
	public class FunctionMethodInfo_foo_EE : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionMethodInfo_foo_EE instance = null;
		public static FunctionMethodInfo_foo_EE Instance { get { if(instance==null) { instance = new FunctionMethodInfo_foo_EE(); } return instance; } }

		private FunctionMethodInfo_foo_EE()
			: base(
				"foo",
				null, "foo",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				GRGEN_LIBGR.VarType.GetVarType(typeof(int))
			)
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call function method without this object!");
		}
	}

	public class ProcedureMethodInfo_bar_EE : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_bar_EE instance = null;
		public static ProcedureMethodInfo_bar_EE Instance { get { if(instance==null) { instance = new ProcedureMethodInfo_bar_EE(); } return instance; } }

		private ProcedureMethodInfo_bar_EE()
			: base(
				"bar",
				null, "bar",
				false,
				new String[] { "j",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)),  },
				new GRGEN_LIBGR.GrGenType[] {  }
			)
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}

	public class ProcedureMethodInfo_bla_EE : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureMethodInfo_bla_EE instance = null;
		public static ProcedureMethodInfo_bla_EE Instance { get { if(instance==null) { instance = new ProcedureMethodInfo_bla_EE(); } return instance; } }

		private ProcedureMethodInfo_bla_EE()
			: base(
				"bla",
				null, "bla",
				false,
				new String[] { "t",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  }
			)
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			throw new Exception("Not implemented, can't call procedure method without this object!");
		}
	}


	public class Comparer_EE_i : Comparer<GRGEN_MODEL.IEE>
	{
		public static Comparer_EE_i thisComparer = new Comparer_EE_i();
		public override int Compare(GRGEN_MODEL.IEE a, GRGEN_MODEL.IEE b)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ReverseComparer_EE_i : Comparer<GRGEN_MODEL.IEE>
	{
		public static ReverseComparer_EE_i thisComparer = new ReverseComparer_EE_i();
		public override int Compare(GRGEN_MODEL.IEE b, GRGEN_MODEL.IEE a)
		{
			return a.@i.CompareTo(b.@i);
		}
	}

	public class ArrayHelper_EE_i
	{
		private static GRGEN_MODEL.IEE instanceBearingAttributeForSearch = new GRGEN_MODEL.@EE(null, null);
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IEE> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IEE> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IEE> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IEE> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@i.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IEE> list, int entry)
		{
			instanceBearingAttributeForSearch.@i = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_EE_i.thisComparer);
		}
		public static List<GRGEN_MODEL.IEE> ArrayOrderAscendingBy(List<GRGEN_MODEL.IEE> list)
		{
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>(list);
			newList.Sort(Comparer_EE_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IEE> ArrayOrderDescendingBy(List<GRGEN_MODEL.IEE> list)
		{
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>(list);
			newList.Sort(ReverseComparer_EE_i.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IEE> ArrayGroupBy(List<GRGEN_MODEL.IEE> list)
		{
			Dictionary<int, List<GRGEN_MODEL.IEE>> seenValues = new Dictionary<int, List<GRGEN_MODEL.IEE>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@i)) {
					seenValues[list[pos].@i].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IEE> tempList = new List<GRGEN_MODEL.IEE>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@i, tempList);
				}
			}
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>();
			foreach(List<GRGEN_MODEL.IEE> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IEE> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IEE> list)
		{
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IEE element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@i)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@i, null);
				}
			}
			return newList;
		}
		public static List<int> Extract(List<GRGEN_MODEL.IEE> list)
		{
			List<int> resultList = new List<int>(list.Count);
			foreach(GRGEN_MODEL.IEE entry in list)
				resultList.Add(entry.@i);
			return resultList;
		}
	}


	public class Comparer_EE_s : Comparer<GRGEN_MODEL.IEE>
	{
		public static Comparer_EE_s thisComparer = new Comparer_EE_s();
		public override int Compare(GRGEN_MODEL.IEE a, GRGEN_MODEL.IEE b)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
	}

	public class ReverseComparer_EE_s : Comparer<GRGEN_MODEL.IEE>
	{
		public static ReverseComparer_EE_s thisComparer = new ReverseComparer_EE_s();
		public override int Compare(GRGEN_MODEL.IEE b, GRGEN_MODEL.IEE a)
		{
			return StringComparer.InvariantCulture.Compare(a.@s, b.@s);
		}
	}

	public class ArrayHelper_EE_s
	{
		private static GRGEN_MODEL.IEE instanceBearingAttributeForSearch = new GRGEN_MODEL.@EE(null, null);
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IEE> list, string entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfBy(IList<GRGEN_MODEL.IEE> list, string entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IEE> list, string entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayLastIndexOfBy(IList<GRGEN_MODEL.IEE> list, string entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@s.Equals(entry))
					return i;
			return -1;
		}
		public static int ArrayIndexOfOrderedBy(List<GRGEN_MODEL.IEE> list, string entry)
		{
			instanceBearingAttributeForSearch.@s = entry;
			return list.BinarySearch(instanceBearingAttributeForSearch, Comparer_EE_s.thisComparer);
		}
		public static List<GRGEN_MODEL.IEE> ArrayOrderAscendingBy(List<GRGEN_MODEL.IEE> list)
		{
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>(list);
			newList.Sort(Comparer_EE_s.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IEE> ArrayOrderDescendingBy(List<GRGEN_MODEL.IEE> list)
		{
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>(list);
			newList.Sort(ReverseComparer_EE_s.thisComparer);
			return newList;
		}
		public static List<GRGEN_MODEL.IEE> ArrayGroupBy(List<GRGEN_MODEL.IEE> list)
		{
			Dictionary<string, List<GRGEN_MODEL.IEE>> seenValues = new Dictionary<string, List<GRGEN_MODEL.IEE>>();
			for(int pos = 0; pos < list.Count; ++pos)
			{
				if(seenValues.ContainsKey(list[pos].@s)) {
					seenValues[list[pos].@s].Add(list[pos]);
				} else {
					List<GRGEN_MODEL.IEE> tempList = new List<GRGEN_MODEL.IEE>();
					tempList.Add(list[pos]);
					seenValues.Add(list[pos].@s, tempList);
				}
			}
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>();
			foreach(List<GRGEN_MODEL.IEE> entry in seenValues.Values)
			{
				newList.AddRange(entry);
			}
			return newList;
		}
		public static List<GRGEN_MODEL.IEE> ArrayKeepOneForEachBy(List<GRGEN_MODEL.IEE> list)
		{
			List<GRGEN_MODEL.IEE> newList = new List<GRGEN_MODEL.IEE>();
			Dictionary<string, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<string, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_MODEL.IEE element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@s)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@s, null);
				}
			}
			return newList;
		}
		public static List<string> Extract(List<GRGEN_MODEL.IEE> list)
		{
			List<string> resultList = new List<string>(list.Count);
			foreach(GRGEN_MODEL.IEE entry in list)
				resultList.Add(entry.@s);
			return resultList;
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

		private @Object(GRGEN_MODEL.@Object oldElem, GRGEN_LIBGR.IGraph graph, IDictionary<object, object> oldToNewObjectMap) : base(GRGEN_MODEL.ObjectType_Object.typeVar, graph.GlobalVariables.FetchObjectUniqueId())
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
		public override string ObjectInterfaceName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.IObject"; } }
		public override string ObjectClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@Object"; } }
		public override GRGEN_LIBGR.IObject CreateObject(GRGEN_LIBGR.IGraph graph, long uniqueId)
		{
			if(uniqueId != -1) {
				GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(graph.GlobalVariables.FetchObjectUniqueId(uniqueId));
				((GRGEN_LIBGR.BaseGraph)graph).ObjectCreated(newObject);
				return newObject;
			} else {
				GRGEN_MODEL.@Object newObject = new GRGEN_MODEL.@Object(graph.GlobalVariables.FetchObjectUniqueId());
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
		public override string TransientObjectInterfaceName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.ITransientObject"; } }
		public override string TransientObjectClassName { get { return "de.unika.ipd.grGen.Model_FunctionsProceduresExample.@TransientObject"; } }
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

	public class FunctionsProceduresExampleIndexSet : GRGEN_LIBGR.IIndexSet
	{
		public FunctionsProceduresExampleIndexSet(GRGEN_LGSP.LGSPGraph graph)
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

	public sealed class FunctionsProceduresExampleNodeModel : GRGEN_LIBGR.INodeModel
	{
		public FunctionsProceduresExampleNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_N.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_NN.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_NN.typeVar,
			};
			GRGEN_MODEL.NodeType_NN.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_NN.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_NN.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_N.typeVar,
			};
			GRGEN_MODEL.NodeType_NN.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_NN.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
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
				case "N" : return GRGEN_MODEL.NodeType_N.typeVar;
				case "NN" : return GRGEN_MODEL.NodeType_NN.typeVar;
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
			GRGEN_MODEL.NodeType_N.typeVar,
			GRGEN_MODEL.NodeType_NN.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GraphElementType[] GRGEN_LIBGR.IGraphElementTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_N),
			typeof(GRGEN_MODEL.NodeType_NN),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.NodeType_N.AttributeType_i,
			GRGEN_MODEL.NodeType_NN.AttributeType_s,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge model
	//

	public sealed class FunctionsProceduresExampleEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public FunctionsProceduresExampleEdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_E.typeVar,
				GRGEN_MODEL.EdgeType_EE.typeVar,
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
				GRGEN_MODEL.EdgeType_E.typeVar,
				GRGEN_MODEL.EdgeType_EE.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_E.typeVar,
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
			GRGEN_MODEL.EdgeType_E.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_E.typeVar,
				GRGEN_MODEL.EdgeType_EE.typeVar,
			};
			GRGEN_MODEL.EdgeType_E.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_EE.typeVar,
			};
			GRGEN_MODEL.EdgeType_E.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_E.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_E.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_E.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_EE.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_EE.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_EE.typeVar,
			};
			GRGEN_MODEL.EdgeType_EE.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_EE.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_EE.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_EE.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_EE.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_E.typeVar,
			};
			GRGEN_MODEL.EdgeType_EE.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_EE.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_E.typeVar,
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
				case "E" : return GRGEN_MODEL.EdgeType_E.typeVar;
				case "EE" : return GRGEN_MODEL.EdgeType_EE.typeVar;
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
			GRGEN_MODEL.EdgeType_E.typeVar,
			GRGEN_MODEL.EdgeType_EE.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GraphElementType[] GRGEN_LIBGR.IGraphElementTypeModel.Types { get { return types; } }
		GRGEN_LIBGR.InheritanceType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private global::System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_E),
			typeof(GRGEN_MODEL.EdgeType_EE),
		};
		public global::System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.EdgeType_E.AttributeType_i,
			GRGEN_MODEL.EdgeType_EE.AttributeType_s,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Object model
	//

	public sealed class FunctionsProceduresExampleObjectModel : GRGEN_LIBGR.IObjectModel
	{
		public FunctionsProceduresExampleObjectModel()
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

	public sealed class FunctionsProceduresExampleTransientObjectModel : GRGEN_LIBGR.ITransientObjectModel
	{
		public FunctionsProceduresExampleTransientObjectModel()
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
	public sealed class FunctionsProceduresExampleGraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public FunctionsProceduresExampleGraphModel()
		{
			FullyInitializeExternalObjectTypes();
		}

		private FunctionsProceduresExampleNodeModel nodeModel = new FunctionsProceduresExampleNodeModel();
		private FunctionsProceduresExampleEdgeModel edgeModel = new FunctionsProceduresExampleEdgeModel();
		private FunctionsProceduresExampleObjectModel objectModel = new FunctionsProceduresExampleObjectModel();
		private FunctionsProceduresExampleTransientObjectModel transientObjectModel = new FunctionsProceduresExampleTransientObjectModel();
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
			return new FunctionsProceduresExampleIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((FunctionsProceduresExampleIndexSet)graph.Indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "FunctionsProceduresExample"; } }
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
		public override bool GraphElementUniquenessIsEnsured { get { return false; } }
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayOrderAscendingBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayOrderAscendingBy((List<GRGEN_MODEL.INN>)array);
				case "s":
					return ArrayHelper_NN_s.ArrayOrderAscendingBy((List<GRGEN_MODEL.INN>)array);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayOrderAscendingBy((List<GRGEN_MODEL.IE>)array);
				default:
					return null;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayOrderAscendingBy((List<GRGEN_MODEL.IEE>)array);
				case "s":
					return ArrayHelper_EE_s.ArrayOrderAscendingBy((List<GRGEN_MODEL.IEE>)array);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayOrderDescendingBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayOrderDescendingBy((List<GRGEN_MODEL.INN>)array);
				case "s":
					return ArrayHelper_NN_s.ArrayOrderDescendingBy((List<GRGEN_MODEL.INN>)array);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayOrderDescendingBy((List<GRGEN_MODEL.IE>)array);
				default:
					return null;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayOrderDescendingBy((List<GRGEN_MODEL.IEE>)array);
				case "s":
					return ArrayHelper_EE_s.ArrayOrderDescendingBy((List<GRGEN_MODEL.IEE>)array);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayGroupBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayGroupBy((List<GRGEN_MODEL.INN>)array);
				case "s":
					return ArrayHelper_NN_s.ArrayGroupBy((List<GRGEN_MODEL.INN>)array);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayGroupBy((List<GRGEN_MODEL.IE>)array);
				default:
					return null;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayGroupBy((List<GRGEN_MODEL.IEE>)array);
				case "s":
					return ArrayHelper_EE_s.ArrayGroupBy((List<GRGEN_MODEL.IEE>)array);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IN>)array);
				default:
					return null;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayKeepOneForEachBy((List<GRGEN_MODEL.INN>)array);
				case "s":
					return ArrayHelper_NN_s.ArrayKeepOneForEachBy((List<GRGEN_MODEL.INN>)array);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IE>)array);
				default:
					return null;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IEE>)array);
				case "s":
					return ArrayHelper_EE_s.ArrayKeepOneForEachBy((List<GRGEN_MODEL.IEE>)array);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value);
				default:
					return -1;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayIndexOfBy((List<GRGEN_MODEL.INN>)array, (int)value);
				case "s":
					return ArrayHelper_NN_s.ArrayIndexOfBy((List<GRGEN_MODEL.INN>)array, (string)value);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayIndexOfBy((List<GRGEN_MODEL.IE>)array, (int)value);
				default:
					return -1;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayIndexOfBy((List<GRGEN_MODEL.IEE>)array, (int)value);
				case "s":
					return ArrayHelper_EE_s.ArrayIndexOfBy((List<GRGEN_MODEL.IEE>)array, (string)value);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayIndexOfBy((List<GRGEN_MODEL.INN>)array, (int)value, startIndex);
				case "s":
					return ArrayHelper_NN_s.ArrayIndexOfBy((List<GRGEN_MODEL.INN>)array, (string)value, startIndex);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayIndexOfBy((List<GRGEN_MODEL.IE>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayIndexOfBy((List<GRGEN_MODEL.IEE>)array, (int)value, startIndex);
				case "s":
					return ArrayHelper_EE_s.ArrayIndexOfBy((List<GRGEN_MODEL.IEE>)array, (string)value, startIndex);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value);
				default:
					return -1;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.INN>)array, (int)value);
				case "s":
					return ArrayHelper_NN_s.ArrayLastIndexOfBy((List<GRGEN_MODEL.INN>)array, (string)value);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IE>)array, (int)value);
				default:
					return -1;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IEE>)array, (int)value);
				case "s":
					return ArrayHelper_EE_s.ArrayLastIndexOfBy((List<GRGEN_MODEL.IEE>)array, (string)value);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IN>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.INN>)array, (int)value, startIndex);
				case "s":
					return ArrayHelper_NN_s.ArrayLastIndexOfBy((List<GRGEN_MODEL.INN>)array, (string)value, startIndex);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IE>)array, (int)value, startIndex);
				default:
					return -1;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayLastIndexOfBy((List<GRGEN_MODEL.IEE>)array, (int)value, startIndex);
				case "s":
					return ArrayHelper_EE_s.ArrayLastIndexOfBy((List<GRGEN_MODEL.IEE>)array, (string)value, startIndex);
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
			case "N":
				switch(member)
				{
				case "i":
					return ArrayHelper_N_i.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IN>)array, (int)value);
				default:
					return -1;
				}
			case "NN":
				switch(member)
				{
				case "i":
					return ArrayHelper_NN_i.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.INN>)array, (int)value);
				case "s":
					return ArrayHelper_NN_s.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.INN>)array, (string)value);
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
			case "E":
				switch(member)
				{
				case "i":
					return ArrayHelper_E_i.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IE>)array, (int)value);
				default:
					return -1;
				}
			case "EE":
				switch(member)
				{
				case "i":
					return ArrayHelper_EE_i.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IEE>)array, (int)value);
				case "s":
					return ArrayHelper_EE_s.ArrayIndexOfOrderedBy((List<GRGEN_MODEL.IEE>)array, (string)value);
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
		public override string MD5Hash { get { return "21a439e33bc45ddb4edd71c010646a97"; } }
	}

	//
	// IGraph (LGSPGraph) implementation
	//
	public class FunctionsProceduresExampleGraph : GRGEN_LGSP.LGSPGraph
	{
		public FunctionsProceduresExampleGraph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) : base(new FunctionsProceduresExampleGraphModel(), globalVariables, GetGraphName())
		{
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@N CreateNodeN()
		{
			return GRGEN_MODEL.@N.CreateNode(this);
		}

		public GRGEN_MODEL.@NN CreateNodeNN()
		{
			return GRGEN_MODEL.@NN.CreateNode(this);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@E CreateEdgeE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@E.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@EE CreateEdgeEE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@EE.CreateEdge(this, source, target);
		}

	}

	//
	// INamedGraph (LGSPNamedGraph) implementation
	//
	public class FunctionsProceduresExampleNamedGraph : GRGEN_LGSP.LGSPNamedGraph
	{
		public FunctionsProceduresExampleNamedGraph(GRGEN_LGSP.LGSPGlobalVariables globalVariables) : base(new FunctionsProceduresExampleGraphModel(), globalVariables, GetGraphName(), 0)
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

		public GRGEN_MODEL.@N CreateNodeN()
		{
			return GRGEN_MODEL.@N.CreateNode(this);
		}

		public GRGEN_MODEL.@N CreateNodeN(string nodeName)
		{
			return GRGEN_MODEL.@N.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@NN CreateNodeNN()
		{
			return GRGEN_MODEL.@NN.CreateNode(this);
		}

		public GRGEN_MODEL.@NN CreateNodeNN(string nodeName)
		{
			return GRGEN_MODEL.@NN.CreateNode(this, nodeName);
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

		public @GRGEN_MODEL.@E CreateEdgeE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@E.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@E CreateEdgeE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@E.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@EE CreateEdgeEE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@EE.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@EE CreateEdgeEE(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@EE.CreateEdge(this, source, target, edgeName);
		}

	}
}
