// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Turing3\Turing3.grg" on Mon Jul 28 08:45:28 CEST 2014

using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Turing3;

namespace de.unika.ipd.grGen.Model_Turing3
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

	public enum NodeTypes { @Node=0, @BandPosition=1, @State=2, @WriteValue=3 };

	// *** Node Node ***


	public sealed class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Node[] pool = new GRGEN_MODEL.@Node[10];
		
		static @Node() {
		}
		
		public @Node() : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
			// implicit initialization, container creation of Node
		}

		public static GRGEN_MODEL.NodeType_Node TypeInstance { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Node(this); }

		private @Node(GRGEN_MODEL.@Node oldElem) : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Node)) return false;
			@Node that_ = (@Node)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
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
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The node type \"Node\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Node\" does not have the attribute \"" + attrName + "\"!");
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

	public sealed class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Node typeVar = new GRGEN_MODEL.NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Node();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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

	// *** Node BandPosition ***

	public interface IBandPosition : GRGEN_LIBGR.INode
	{
		int @value { get; set; }
	}

	public sealed class @BandPosition : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IBandPosition
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@BandPosition[] pool = new GRGEN_MODEL.@BandPosition[10];
		
		// explicit initializations of BandPosition for target BandPosition
		// implicit initializations of BandPosition for target BandPosition
		static @BandPosition() {
		}
		
		public @BandPosition() : base(GRGEN_MODEL.NodeType_BandPosition.typeVar)
		{
			// implicit initialization, container creation of BandPosition
			// explicit initializations of BandPosition for target BandPosition
		}

		public static GRGEN_MODEL.NodeType_BandPosition TypeInstance { get { return GRGEN_MODEL.NodeType_BandPosition.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@BandPosition(this); }

		private @BandPosition(GRGEN_MODEL.@BandPosition oldElem) : base(GRGEN_MODEL.NodeType_BandPosition.typeVar)
		{
			value_M0no_suXx_h4rD = oldElem.value_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @BandPosition)) return false;
			@BandPosition that_ = (@BandPosition)that;
			return true
				&& value_M0no_suXx_h4rD == that_.value_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@BandPosition CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@BandPosition node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of BandPosition
				node.@value = 0;
				// explicit initializations of BandPosition for target BandPosition
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@BandPosition CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@BandPosition node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of BandPosition
				node.@value = 0;
				// explicit initializations of BandPosition for target BandPosition
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int value_M0no_suXx_h4rD;
		public int @value
		{
			get { return value_M0no_suXx_h4rD; }
			set { value_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "value": return this.@value;
			}
			throw new NullReferenceException(
				"The node type \"BandPosition\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": this.@value = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"BandPosition\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of BandPosition
			this.@value = 0;
			// explicit initializations of BandPosition for target BandPosition
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("BandPosition does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("BandPosition does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class NodeType_BandPosition : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_BandPosition typeVar = new GRGEN_MODEL.NodeType_BandPosition();
		public static bool[] isA = new bool[] { true, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_value;
		public NodeType_BandPosition() : base((int) NodeTypes.@BandPosition)
		{
			AttributeType_value = new GRGEN_LIBGR.AttributeType("value", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "BandPosition"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "BandPosition"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IBandPosition"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@BandPosition"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@BandPosition();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_value;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "value" : return AttributeType_value;
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
			GRGEN_MODEL.@BandPosition newNode = new GRGEN_MODEL.@BandPosition();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@BandPosition:
					// copy attributes for: BandPosition
					{
						GRGEN_MODEL.IBandPosition old = (GRGEN_MODEL.IBandPosition) oldNode;
						newNode.@value = old.@value;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_BandPosition_value : Comparer<GRGEN_MODEL.IBandPosition>
	{
		private static GRGEN_MODEL.IBandPosition nodeBearingAttributeForSearch = new GRGEN_MODEL.@BandPosition();
		private static Comparer_BandPosition_value thisComparer = new Comparer_BandPosition_value();
		public override int Compare(GRGEN_MODEL.IBandPosition a, GRGEN_MODEL.IBandPosition b)
		{
			return a.@value.CompareTo(b.@value);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IBandPosition> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IBandPosition> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IBandPosition> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IBandPosition> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IBandPosition> list, int entry)
		{
			nodeBearingAttributeForSearch.@value = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IBandPosition> ArrayOrderAscendingBy(List<GRGEN_MODEL.IBandPosition> list)
		{
			List<GRGEN_MODEL.IBandPosition> newList = new List<GRGEN_MODEL.IBandPosition>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node State ***

	public interface IState : GRGEN_LIBGR.INode
	{
	}

	public sealed class @State : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IState
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@State[] pool = new GRGEN_MODEL.@State[10];
		
		// explicit initializations of State for target State
		// implicit initializations of State for target State
		static @State() {
		}
		
		public @State() : base(GRGEN_MODEL.NodeType_State.typeVar)
		{
			// implicit initialization, container creation of State
			// explicit initializations of State for target State
		}

		public static GRGEN_MODEL.NodeType_State TypeInstance { get { return GRGEN_MODEL.NodeType_State.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@State(this); }

		private @State(GRGEN_MODEL.@State oldElem) : base(GRGEN_MODEL.NodeType_State.typeVar)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @State)) return false;
			@State that_ = (@State)that;
			return true
			;
		}

		public static GRGEN_MODEL.@State CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@State node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@State();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of State
				// explicit initializations of State for target State
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@State CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@State node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@State();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of State
				// explicit initializations of State for target State
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The node type \"State\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"State\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of State
			// explicit initializations of State for target State
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("State does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("State does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class NodeType_State : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_State typeVar = new GRGEN_MODEL.NodeType_State();
		public static bool[] isA = new bool[] { true, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_State() : base((int) NodeTypes.@State)
		{
		}
		public override string Name { get { return "State"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "State"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IState"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@State"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@State();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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
			return new GRGEN_MODEL.@State();
		}

	}

	// *** Node WriteValue ***

	public interface IWriteValue : GRGEN_LIBGR.INode
	{
		int @value { get; set; }
	}

	public sealed class @WriteValue : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IWriteValue
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@WriteValue[] pool = new GRGEN_MODEL.@WriteValue[10];
		
		// explicit initializations of WriteValue for target WriteValue
		// implicit initializations of WriteValue for target WriteValue
		static @WriteValue() {
		}
		
		public @WriteValue() : base(GRGEN_MODEL.NodeType_WriteValue.typeVar)
		{
			// implicit initialization, container creation of WriteValue
			// explicit initializations of WriteValue for target WriteValue
		}

		public static GRGEN_MODEL.NodeType_WriteValue TypeInstance { get { return GRGEN_MODEL.NodeType_WriteValue.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@WriteValue(this); }

		private @WriteValue(GRGEN_MODEL.@WriteValue oldElem) : base(GRGEN_MODEL.NodeType_WriteValue.typeVar)
		{
			value_M0no_suXx_h4rD = oldElem.value_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @WriteValue)) return false;
			@WriteValue that_ = (@WriteValue)that;
			return true
				&& value_M0no_suXx_h4rD == that_.value_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@WriteValue CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@WriteValue node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of WriteValue
				node.@value = 0;
				// explicit initializations of WriteValue for target WriteValue
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@WriteValue CreateNode(GRGEN_LGSP.LGSPNamedGraph graph, string nodeName)
		{
			GRGEN_MODEL.@WriteValue node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, container creation of WriteValue
				node.@value = 0;
				// explicit initializations of WriteValue for target WriteValue
			}
			graph.AddNode(node, nodeName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int value_M0no_suXx_h4rD;
		public int @value
		{
			get { return value_M0no_suXx_h4rD; }
			set { value_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "value": return this.@value;
			}
			throw new NullReferenceException(
				"The node type \"WriteValue\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": this.@value = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"WriteValue\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of WriteValue
			this.@value = 0;
			// explicit initializations of WriteValue for target WriteValue
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("WriteValue does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("WriteValue does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class NodeType_WriteValue : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_WriteValue typeVar = new GRGEN_MODEL.NodeType_WriteValue();
		public static bool[] isA = new bool[] { true, false, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public static GRGEN_LIBGR.AttributeType AttributeType_value;
		public NodeType_WriteValue() : base((int) NodeTypes.@WriteValue)
		{
			AttributeType_value = new GRGEN_LIBGR.AttributeType("value", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null, null, null, typeof(int));
		}
		public override string Name { get { return "WriteValue"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "WriteValue"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IWriteValue"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@WriteValue"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@WriteValue();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_value;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "value" : return AttributeType_value;
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
			GRGEN_MODEL.@WriteValue newNode = new GRGEN_MODEL.@WriteValue();
			switch(oldNode.Type.TypeID)
			{
				case (int) GRGEN_MODEL.NodeTypes.@WriteValue:
					// copy attributes for: WriteValue
					{
						GRGEN_MODEL.IWriteValue old = (GRGEN_MODEL.IWriteValue) oldNode;
						newNode.@value = old.@value;
					}
					break;
			}
			return newNode;
		}

	}

	public class Comparer_WriteValue_value : Comparer<GRGEN_MODEL.IWriteValue>
	{
		private static GRGEN_MODEL.IWriteValue nodeBearingAttributeForSearch = new GRGEN_MODEL.@WriteValue();
		private static Comparer_WriteValue_value thisComparer = new Comparer_WriteValue_value();
		public override int Compare(GRGEN_MODEL.IWriteValue a, GRGEN_MODEL.IWriteValue b)
		{
			return a.@value.CompareTo(b.@value);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IWriteValue> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IWriteValue> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IWriteValue> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IWriteValue> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@value.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IWriteValue> list, int entry)
		{
			nodeBearingAttributeForSearch.@value = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IWriteValue> ArrayOrderAscendingBy(List<GRGEN_MODEL.IWriteValue> list)
		{
			List<GRGEN_MODEL.IWriteValue> newList = new List<GRGEN_MODEL.IWriteValue>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge=0, @Edge=1, @UEdge=2, @right=3, @readZero=4, @readOne=5, @moveLeft=6, @moveRight=7 };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, };
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
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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


	public sealed class @Edge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Edge[] pool = new GRGEN_MODEL.@Edge[10];
		
		static @Edge() {
		}
		
		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, source, target)
		{
			// implicit initialization, container creation of Edge
		}

		public static GRGEN_MODEL.EdgeType_Edge TypeInstance { get { return GRGEN_MODEL.EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @Edge(GRGEN_MODEL.@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @Edge)) return false;
			@Edge that_ = (@Edge)that;
			return true
			;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
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
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
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

	public sealed class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_Edge typeVar = new GRGEN_MODEL.EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@Edge"; } }
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
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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


	public sealed class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@UEdge[] pool = new GRGEN_MODEL.@UEdge[10];
		
		static @UEdge() {
		}
		
		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, source, target)
		{
			// implicit initialization, container creation of UEdge
		}

		public static GRGEN_MODEL.EdgeType_UEdge TypeInstance { get { return GRGEN_MODEL.EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @UEdge(GRGEN_MODEL.@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @UEdge)) return false;
			@UEdge that_ = (@UEdge)that;
			return true
			;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
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
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"UEdge\" does not have the attribute \"" + attrName + "\"!");
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

	public sealed class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_UEdge typeVar = new GRGEN_MODEL.EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@UEdge"; } }
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
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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

	// *** Edge right ***

	public interface Iright : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @right : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iright
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@right[] pool = new GRGEN_MODEL.@right[10];
		
		// explicit initializations of right for target right
		// implicit initializations of right for target right
		static @right() {
		}
		
		public @right(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_right.typeVar, source, target)
		{
			// implicit initialization, container creation of right
			// explicit initializations of right for target right
		}

		public static GRGEN_MODEL.EdgeType_right TypeInstance { get { return GRGEN_MODEL.EdgeType_right.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@right(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @right(GRGEN_MODEL.@right oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_right.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @right)) return false;
			@right that_ = (@right)that;
			return true
			;
		}

		public static GRGEN_MODEL.@right CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@right edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of right
				// explicit initializations of right for target right
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@right CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@right edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of right
				// explicit initializations of right for target right
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"right\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"right\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of right
			// explicit initializations of right for target right
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("right does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("right does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class EdgeType_right : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_right typeVar = new GRGEN_MODEL.EdgeType_right();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_right() : base((int) EdgeTypes.@right)
		{
		}
		public override string Name { get { return "right"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "right"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.Iright"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@right"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@right((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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
			return new GRGEN_MODEL.@right((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge readZero ***

	public interface IreadZero : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @readZero : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IreadZero
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@readZero[] pool = new GRGEN_MODEL.@readZero[10];
		
		// explicit initializations of readZero for target readZero
		// implicit initializations of readZero for target readZero
		static @readZero() {
		}
		
		public @readZero(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_readZero.typeVar, source, target)
		{
			// implicit initialization, container creation of readZero
			// explicit initializations of readZero for target readZero
		}

		public static GRGEN_MODEL.EdgeType_readZero TypeInstance { get { return GRGEN_MODEL.EdgeType_readZero.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@readZero(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @readZero(GRGEN_MODEL.@readZero oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_readZero.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @readZero)) return false;
			@readZero that_ = (@readZero)that;
			return true
			;
		}

		public static GRGEN_MODEL.@readZero CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@readZero edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of readZero
				// explicit initializations of readZero for target readZero
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@readZero CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@readZero edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of readZero
				// explicit initializations of readZero for target readZero
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"readZero\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"readZero\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of readZero
			// explicit initializations of readZero for target readZero
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("readZero does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("readZero does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class EdgeType_readZero : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_readZero typeVar = new GRGEN_MODEL.EdgeType_readZero();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_readZero() : base((int) EdgeTypes.@readZero)
		{
		}
		public override string Name { get { return "readZero"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "readZero"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IreadZero"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@readZero"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@readZero((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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
			return new GRGEN_MODEL.@readZero((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge readOne ***

	public interface IreadOne : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @readOne : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IreadOne
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@readOne[] pool = new GRGEN_MODEL.@readOne[10];
		
		// explicit initializations of readOne for target readOne
		// implicit initializations of readOne for target readOne
		static @readOne() {
		}
		
		public @readOne(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_readOne.typeVar, source, target)
		{
			// implicit initialization, container creation of readOne
			// explicit initializations of readOne for target readOne
		}

		public static GRGEN_MODEL.EdgeType_readOne TypeInstance { get { return GRGEN_MODEL.EdgeType_readOne.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@readOne(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @readOne(GRGEN_MODEL.@readOne oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_readOne.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @readOne)) return false;
			@readOne that_ = (@readOne)that;
			return true
			;
		}

		public static GRGEN_MODEL.@readOne CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@readOne edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of readOne
				// explicit initializations of readOne for target readOne
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@readOne CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@readOne edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of readOne
				// explicit initializations of readOne for target readOne
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"readOne\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"readOne\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of readOne
			// explicit initializations of readOne for target readOne
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("readOne does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("readOne does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class EdgeType_readOne : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_readOne typeVar = new GRGEN_MODEL.EdgeType_readOne();
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_readOne() : base((int) EdgeTypes.@readOne)
		{
		}
		public override string Name { get { return "readOne"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "readOne"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IreadOne"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@readOne"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@readOne((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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
			return new GRGEN_MODEL.@readOne((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge moveLeft ***

	public interface ImoveLeft : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @moveLeft : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.ImoveLeft
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@moveLeft[] pool = new GRGEN_MODEL.@moveLeft[10];
		
		// explicit initializations of moveLeft for target moveLeft
		// implicit initializations of moveLeft for target moveLeft
		static @moveLeft() {
		}
		
		public @moveLeft(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_moveLeft.typeVar, source, target)
		{
			// implicit initialization, container creation of moveLeft
			// explicit initializations of moveLeft for target moveLeft
		}

		public static GRGEN_MODEL.EdgeType_moveLeft TypeInstance { get { return GRGEN_MODEL.EdgeType_moveLeft.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@moveLeft(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @moveLeft(GRGEN_MODEL.@moveLeft oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_moveLeft.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @moveLeft)) return false;
			@moveLeft that_ = (@moveLeft)that;
			return true
			;
		}

		public static GRGEN_MODEL.@moveLeft CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@moveLeft edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of moveLeft
				// explicit initializations of moveLeft for target moveLeft
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@moveLeft CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@moveLeft edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of moveLeft
				// explicit initializations of moveLeft for target moveLeft
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"moveLeft\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"moveLeft\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of moveLeft
			// explicit initializations of moveLeft for target moveLeft
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("moveLeft does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("moveLeft does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class EdgeType_moveLeft : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_moveLeft typeVar = new GRGEN_MODEL.EdgeType_moveLeft();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, false, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_moveLeft() : base((int) EdgeTypes.@moveLeft)
		{
		}
		public override string Name { get { return "moveLeft"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "moveLeft"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.ImoveLeft"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@moveLeft"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@moveLeft((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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
			return new GRGEN_MODEL.@moveLeft((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge moveRight ***

	public interface ImoveRight : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @moveRight : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.ImoveRight
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@moveRight[] pool = new GRGEN_MODEL.@moveRight[10];
		
		// explicit initializations of moveRight for target moveRight
		// implicit initializations of moveRight for target moveRight
		static @moveRight() {
		}
		
		public @moveRight(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_moveRight.typeVar, source, target)
		{
			// implicit initialization, container creation of moveRight
			// explicit initializations of moveRight for target moveRight
		}

		public static GRGEN_MODEL.EdgeType_moveRight TypeInstance { get { return GRGEN_MODEL.EdgeType_moveRight.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@moveRight(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @moveRight(GRGEN_MODEL.@moveRight oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_moveRight.typeVar, newSource, newTarget)
		{
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @moveRight)) return false;
			@moveRight that_ = (@moveRight)that;
			return true
			;
		}

		public static GRGEN_MODEL.@moveRight CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@moveRight edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of moveRight
				// explicit initializations of moveRight for target moveRight
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@moveRight CreateEdge(GRGEN_LGSP.LGSPNamedGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			GRGEN_MODEL.@moveRight edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, container creation of moveRight
				// explicit initializations of moveRight for target moveRight
			}
			graph.AddEdge(edge, edgeName);
			return edge;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public override object GetAttribute(string attrName)
		{
			throw new NullReferenceException(
				"The edge type \"moveRight\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"moveRight\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, container creation of moveRight
			// explicit initializations of moveRight for target moveRight
		}

		public override object ApplyFunctionMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("moveRight does not have the function method " + name + "!");
			}
		}
		public override object[] ApplyProcedureMethod(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, string name, object[] arguments)
		{
			switch(name)
			{
				default: throw new NullReferenceException("moveRight does not have the procedure method " + name + "!");
			}
		}
	}

	public sealed class EdgeType_moveRight : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_moveRight typeVar = new GRGEN_MODEL.EdgeType_moveRight();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public EdgeType_moveRight() : base((int) EdgeTypes.@moveRight)
		{
		}
		public override string Name { get { return "moveRight"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "moveRight"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.ImoveRight"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@moveRight"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@moveRight((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

		public override void SetSourceAndTarget(GRGEN_LIBGR.IEdge edge, GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			((GRGEN_LGSP.LGSPEdge)edge).SetSourceAndTarget((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
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
			return new GRGEN_MODEL.@moveRight((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	public sealed class ExternalType_object : GRGEN_LIBGR.ExternalType
	{
		public ExternalType_object()
			: base("object", typeof(object))
		{
		}
		public override int NumFunctionMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IFunctionDefinition> FunctionMethods { get { yield break; } }
		public override GRGEN_LIBGR.IFunctionDefinition GetFunctionMethod(string name) { return null; }
		public override int NumProcedureMethods { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.IProcedureDefinition> ProcedureMethods { get { yield break; } }
		public override GRGEN_LIBGR.IProcedureDefinition GetProcedureMethod(string name) { return null; }
	}

	//
	// Indices
	//

	public class Turing3IndexSet : GRGEN_LIBGR.IIndexSet
	{
		public Turing3IndexSet(GRGEN_LGSP.LGSPGraph graph)
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

	public sealed class Turing3NodeModel : GRGEN_LIBGR.INodeModel
	{
		public Turing3NodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
				GRGEN_MODEL.NodeType_State.typeVar,
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
				GRGEN_MODEL.NodeType_State.typeVar,
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_State.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_State.typeVar,
			};
			GRGEN_MODEL.NodeType_State.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_State.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_State.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_State.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public GRGEN_LIBGR.NodeType RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		public GRGEN_LIBGR.NodeType GetType(string name)
		{
			switch(name)
			{
				case "Node" : return GRGEN_MODEL.NodeType_Node.typeVar;
				case "BandPosition" : return GRGEN_MODEL.NodeType_BandPosition.typeVar;
				case "State" : return GRGEN_MODEL.NodeType_State.typeVar;
				case "WriteValue" : return GRGEN_MODEL.NodeType_WriteValue.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_BandPosition.typeVar,
			GRGEN_MODEL.NodeType_State.typeVar,
			GRGEN_MODEL.NodeType_WriteValue.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_BandPosition),
			typeof(GRGEN_MODEL.NodeType_State),
			typeof(GRGEN_MODEL.NodeType_WriteValue),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.NodeType_BandPosition.AttributeType_value,
			GRGEN_MODEL.NodeType_WriteValue.AttributeType_value,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge model
	//

	public sealed class Turing3EdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public Turing3EdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
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
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
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
			GRGEN_MODEL.EdgeType_right.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_right.typeVar,
			};
			GRGEN_MODEL.EdgeType_right.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_right.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_right.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readZero.typeVar,
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readOne.typeVar,
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public GRGEN_LIBGR.EdgeType RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		public GRGEN_LIBGR.EdgeType GetType(string name)
		{
			switch(name)
			{
				case "AEdge" : return GRGEN_MODEL.EdgeType_AEdge.typeVar;
				case "Edge" : return GRGEN_MODEL.EdgeType_Edge.typeVar;
				case "UEdge" : return GRGEN_MODEL.EdgeType_UEdge.typeVar;
				case "right" : return GRGEN_MODEL.EdgeType_right.typeVar;
				case "readZero" : return GRGEN_MODEL.EdgeType_readZero.typeVar;
				case "readOne" : return GRGEN_MODEL.EdgeType_readOne.typeVar;
				case "moveLeft" : return GRGEN_MODEL.EdgeType_moveLeft.typeVar;
				case "moveRight" : return GRGEN_MODEL.EdgeType_moveRight.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.EdgeType[] types = {
			GRGEN_MODEL.EdgeType_AEdge.typeVar,
			GRGEN_MODEL.EdgeType_Edge.typeVar,
			GRGEN_MODEL.EdgeType_UEdge.typeVar,
			GRGEN_MODEL.EdgeType_right.typeVar,
			GRGEN_MODEL.EdgeType_readZero.typeVar,
			GRGEN_MODEL.EdgeType_readOne.typeVar,
			GRGEN_MODEL.EdgeType_moveLeft.typeVar,
			GRGEN_MODEL.EdgeType_moveRight.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_right),
			typeof(GRGEN_MODEL.EdgeType_readZero),
			typeof(GRGEN_MODEL.EdgeType_readOne),
			typeof(GRGEN_MODEL.EdgeType_moveLeft),
			typeof(GRGEN_MODEL.EdgeType_moveRight),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel (LGSPGraphModel) implementation
	//
	public sealed class Turing3GraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public Turing3GraphModel()
		{
			FullyInitializeExternalTypes();
		}

		private Turing3NodeModel nodeModel = new Turing3NodeModel();
		private Turing3EdgeModel edgeModel = new Turing3EdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_right.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, 0, 1, 0, 1, false),
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new Turing3IndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((Turing3IndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "Turing3"; } }
		public override GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public override GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
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
		public override int BranchingFactorForEqualsAny { get { return 0; } }

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object };
		public override GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
		}

		public override string MD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
	}

	//
	// IGraph (LGSPGraph) / IGraphModel implementation
	//
	public class Turing3Graph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public Turing3Graph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@BandPosition CreateNodeBandPosition()
		{
			return GRGEN_MODEL.@BandPosition.CreateNode(this);
		}

		public GRGEN_MODEL.@State CreateNodeState()
		{
			return GRGEN_MODEL.@State.CreateNode(this);
		}

		public GRGEN_MODEL.@WriteValue CreateNodeWriteValue()
		{
			return GRGEN_MODEL.@WriteValue.CreateNode(this);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@right CreateEdgeright(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@right.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@readZero CreateEdgereadZero(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@readZero.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@readOne CreateEdgereadOne(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@readOne.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@moveLeft CreateEdgemoveLeft(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@moveLeft.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@moveRight CreateEdgemoveRight(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@moveRight.CreateEdge(this, source, target);
		}

		private Turing3NodeModel nodeModel = new Turing3NodeModel();
		private Turing3EdgeModel edgeModel = new Turing3EdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_right.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, 0, 1, 0, 1, false),
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new Turing3IndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((Turing3IndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "Turing3"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<string> Packages { get { return packages; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions { get { return indexDescriptions; } }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) { return indexDescriptions[i]; }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)
 		{
			for(int i=0; i<indexDescriptions.Length; ++i)
				if(indexDescriptions[i].Name==indexName)
					return indexDescriptions[i];
			return null;
		}
		public bool GraphElementUniquenessIsEnsured { get { return false; } }
		public bool GraphElementsAreAccessibleByUniqueId { get { return false; } }
		public int BranchingFactorForEqualsAny { get { return 0; } }

		public object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l'
			return null;
		}
		public string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			Console.WriteLine("Warning: Exporting attribute of object type to null");
			return "null";
		}
		public string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return attribute!=null ? attribute.ToString() : "null";
		}
		public void External(string line, GRGEN_LIBGR.IGraph graph)
		{
			Console.Write("Ignoring: ");
			Console.WriteLine(line);
		}
		public GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return null;
		}

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object };
		public GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
		}

		public bool IsEqualClassDefined { get { return false; } }
		public bool IsLowerClassDefined { get { return false; } }
		public bool IsEqual(object this_, object that)
		{
			return this_ == that;
		}
		public bool IsLower(object this_, object that)
		{
			return this_ == that;
		}

		public string MD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
	}

	//
	// INamedGraph (LGSPNamedGraph) / IGraphModel implementation
	//
	public class Turing3NamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel
	{
		public Turing3NamedGraph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode(string nodeName)
		{
			return GRGEN_MODEL.@Node.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@BandPosition CreateNodeBandPosition()
		{
			return GRGEN_MODEL.@BandPosition.CreateNode(this);
		}

		public GRGEN_MODEL.@BandPosition CreateNodeBandPosition(string nodeName)
		{
			return GRGEN_MODEL.@BandPosition.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@State CreateNodeState()
		{
			return GRGEN_MODEL.@State.CreateNode(this);
		}

		public GRGEN_MODEL.@State CreateNodeState(string nodeName)
		{
			return GRGEN_MODEL.@State.CreateNode(this, nodeName);
		}

		public GRGEN_MODEL.@WriteValue CreateNodeWriteValue()
		{
			return GRGEN_MODEL.@WriteValue.CreateNode(this);
		}

		public GRGEN_MODEL.@WriteValue CreateNodeWriteValue(string nodeName)
		{
			return GRGEN_MODEL.@WriteValue.CreateNode(this, nodeName);
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

		public @GRGEN_MODEL.@right CreateEdgeright(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@right.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@right CreateEdgeright(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@right.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@readZero CreateEdgereadZero(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@readZero.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@readZero CreateEdgereadZero(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@readZero.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@readOne CreateEdgereadOne(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@readOne.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@readOne CreateEdgereadOne(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@readOne.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@moveLeft CreateEdgemoveLeft(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@moveLeft.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@moveLeft CreateEdgemoveLeft(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@moveLeft.CreateEdge(this, source, target, edgeName);
		}

		public @GRGEN_MODEL.@moveRight CreateEdgemoveRight(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@moveRight.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@moveRight CreateEdgemoveRight(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string edgeName)
		{
			return @GRGEN_MODEL.@moveRight.CreateEdge(this, source, target, edgeName);
		}

		private Turing3NodeModel nodeModel = new Turing3NodeModel();
		private Turing3EdgeModel edgeModel = new Turing3EdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_right.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, 0, 1, 0, 1, false),
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new Turing3IndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((Turing3IndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "Turing3"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<string> Packages { get { return packages; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.IndexDescription> IndexDescriptions { get { return indexDescriptions; } }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(int i) { return indexDescriptions[i]; }
		public static GRGEN_LIBGR.IndexDescription GetIndexDescription(string indexName)
 		{
			for(int i=0; i<indexDescriptions.Length; ++i)
				if(indexDescriptions[i].Name==indexName)
					return indexDescriptions[i];
			return null;
		}
		public bool GraphElementUniquenessIsEnsured { get { return false; } }
		public bool GraphElementsAreAccessibleByUniqueId { get { return false; } }
		public int BranchingFactorForEqualsAny { get { return 0; } }

		public object Parse(TextReader reader, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			reader.Read(); reader.Read(); reader.Read(); reader.Read(); // eat 'n' 'u' 'l' 'l'
			return null;
		}
		public string Serialize(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			Console.WriteLine("Warning: Exporting attribute of object type to null");
			return "null";
		}
		public string Emit(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return attribute!=null ? attribute.ToString() : "null";
		}
		public void External(string line, GRGEN_LIBGR.IGraph graph)
		{
			Console.Write("Ignoring: ");
			Console.WriteLine(line);
		}
		public GRGEN_LIBGR.INamedGraph AsGraph(object attribute, GRGEN_LIBGR.AttributeType attrType, GRGEN_LIBGR.IGraph graph)
		{
			return null;
		}

		public static GRGEN_LIBGR.ExternalType externalType_object = new ExternalType_object();
		private GRGEN_LIBGR.ExternalType[] externalTypes = { externalType_object };
		public GRGEN_LIBGR.ExternalType[] ExternalTypes { get { return externalTypes; } }

		private void FullyInitializeExternalTypes()
		{
			externalType_object.InitDirectSupertypes( new GRGEN_LIBGR.ExternalType[] { } );
		}

		public bool IsEqualClassDefined { get { return false; } }
		public bool IsLowerClassDefined { get { return false; } }
		public bool IsEqual(object this_, object that)
		{
			return this_ == that;
		}
		public bool IsLower(object this_, object that)
		{
			return this_ == that;
		}

		public string MD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
	}
}
