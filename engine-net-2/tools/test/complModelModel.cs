// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "test.grg" on Sun Jan 12 22:27:34 CET 2020

using System;
using System.Collections.Generic;
using System.IO;
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
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@A1[] pool = new GRGEN_MODEL.@A1[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@A1(this); }

		private @A1(GRGEN_MODEL.@A1 oldElem) : base(GRGEN_MODEL.NodeType_A1.typeVar)
		{
			a1_M0no_suXx_h4rD = oldElem.a1_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @A1)) return false;
			@A1 that_ = (@A1)that;
			return true
				&& a1_M0no_suXx_h4rD == that_.a1_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@A1 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A1 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A1();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"A1\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a1": this.@a1 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A1\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IA1 nodeBearingAttributeForSearch = new GRGEN_MODEL.@A1();
		private static Comparer_A1_a1 thisComparer = new Comparer_A1_a1();
		public override int Compare(GRGEN_MODEL.IA1 a, GRGEN_MODEL.IA1 b)
		{
			return a.@a1.CompareTo(b.@a1);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA1> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IA1> list, int entry)
		{
			nodeBearingAttributeForSearch.@a1 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IA1> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA1> list)
		{
			List<GRGEN_MODEL.IA1> newList = new List<GRGEN_MODEL.IA1>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node A2 ***

	public interface IA2 : GRGEN_LIBGR.INode
	{
		int @a2 { get; set; }
	}

	public sealed partial class @A2 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA2
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@A2[] pool = new GRGEN_MODEL.@A2[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@A2(this); }

		private @A2(GRGEN_MODEL.@A2 oldElem) : base(GRGEN_MODEL.NodeType_A2.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @A2)) return false;
			@A2 that_ = (@A2)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@A2 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A2 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A2();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"A2\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A2\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IA2 nodeBearingAttributeForSearch = new GRGEN_MODEL.@A2();
		private static Comparer_A2_a2 thisComparer = new Comparer_A2_a2();
		public override int Compare(GRGEN_MODEL.IA2 a, GRGEN_MODEL.IA2 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA2> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IA2> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IA2> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA2> list)
		{
			List<GRGEN_MODEL.IA2> newList = new List<GRGEN_MODEL.IA2>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node A3 ***

	public interface IA3 : GRGEN_LIBGR.INode
	{
		int @a3 { get; set; }
	}

	public sealed partial class @A3 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA3
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@A3[] pool = new GRGEN_MODEL.@A3[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@A3(this); }

		private @A3(GRGEN_MODEL.@A3 oldElem) : base(GRGEN_MODEL.NodeType_A3.typeVar)
		{
			a3_M0no_suXx_h4rD = oldElem.a3_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @A3)) return false;
			@A3 that_ = (@A3)that;
			return true
				&& a3_M0no_suXx_h4rD == that_.a3_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@A3 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A3 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A3();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"A3\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a3": this.@a3 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A3\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IA3 nodeBearingAttributeForSearch = new GRGEN_MODEL.@A3();
		private static Comparer_A3_a3 thisComparer = new Comparer_A3_a3();
		public override int Compare(GRGEN_MODEL.IA3 a, GRGEN_MODEL.IA3 b)
		{
			return a.@a3.CompareTo(b.@a3);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA3> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IA3> list, int entry)
		{
			nodeBearingAttributeForSearch.@a3 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IA3> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA3> list)
		{
			List<GRGEN_MODEL.IA3> newList = new List<GRGEN_MODEL.IA3>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node A4 ***

	public interface IA4 : GRGEN_LIBGR.INode
	{
		int @a4 { get; set; }
	}

	public sealed partial class @A4 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA4
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@A4[] pool = new GRGEN_MODEL.@A4[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@A4(this); }

		private @A4(GRGEN_MODEL.@A4 oldElem) : base(GRGEN_MODEL.NodeType_A4.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @A4)) return false;
			@A4 that_ = (@A4)that;
			return true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@A4 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A4 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A4();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"A4\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A4\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IA4 nodeBearingAttributeForSearch = new GRGEN_MODEL.@A4();
		private static Comparer_A4_a4 thisComparer = new Comparer_A4_a4();
		public override int Compare(GRGEN_MODEL.IA4 a, GRGEN_MODEL.IA4 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA4> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IA4> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IA4> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA4> list)
		{
			List<GRGEN_MODEL.IA4> newList = new List<GRGEN_MODEL.IA4>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node A5 ***

	public interface IA5 : GRGEN_LIBGR.INode
	{
		int @a5 { get; set; }
	}

	public sealed partial class @A5 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IA5
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@A5[] pool = new GRGEN_MODEL.@A5[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@A5(this); }

		private @A5(GRGEN_MODEL.@A5 oldElem) : base(GRGEN_MODEL.NodeType_A5.typeVar)
		{
			a5_M0no_suXx_h4rD = oldElem.a5_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @A5)) return false;
			@A5 that_ = (@A5)that;
			return true
				&& a5_M0no_suXx_h4rD == that_.a5_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@A5 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@A5 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@A5();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"A5\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a5": this.@a5 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A5\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IA5 nodeBearingAttributeForSearch = new GRGEN_MODEL.@A5();
		private static Comparer_A5_a5 thisComparer = new Comparer_A5_a5();
		public override int Compare(GRGEN_MODEL.IA5 a, GRGEN_MODEL.IA5 b)
		{
			return a.@a5.CompareTo(b.@a5);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IA5> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IA5> list, int entry)
		{
			nodeBearingAttributeForSearch.@a5 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IA5> ArrayOrderAscendingBy(List<GRGEN_MODEL.IA5> list)
		{
			List<GRGEN_MODEL.IA5> newList = new List<GRGEN_MODEL.IA5>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node B21 ***

	public interface IB21 : IA2
	{
		int @b21 { get; set; }
	}

	public sealed partial class @B21 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB21
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@B21[] pool = new GRGEN_MODEL.@B21[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@B21(this); }

		private @B21(GRGEN_MODEL.@B21 oldElem) : base(GRGEN_MODEL.NodeType_B21.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b21_M0no_suXx_h4rD = oldElem.b21_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @B21)) return false;
			@B21 that_ = (@B21)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b21_M0no_suXx_h4rD == that_.b21_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@B21 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B21 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B21();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"B21\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b21": this.@b21 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B21\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IB21 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B21();
		private static Comparer_B21_a2 thisComparer = new Comparer_B21_a2();
		public override int Compare(GRGEN_MODEL.IB21 a, GRGEN_MODEL.IB21 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB21> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB21> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_B21_b21 : Comparer<GRGEN_MODEL.IB21>
	{
		private static GRGEN_MODEL.IB21 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B21();
		private static Comparer_B21_b21 thisComparer = new Comparer_B21_b21();
		public override int Compare(GRGEN_MODEL.IB21 a, GRGEN_MODEL.IB21 b)
		{
			return a.@b21.CompareTo(b.@b21);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB21> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b21.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB21> list, int entry)
		{
			nodeBearingAttributeForSearch.@b21 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB21> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB21> list)
		{
			List<GRGEN_MODEL.IB21> newList = new List<GRGEN_MODEL.IB21>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node B22 ***

	public interface IB22 : IA2
	{
		int @b22 { get; set; }
	}

	public sealed partial class @B22 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB22
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@B22[] pool = new GRGEN_MODEL.@B22[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@B22(this); }

		private @B22(GRGEN_MODEL.@B22 oldElem) : base(GRGEN_MODEL.NodeType_B22.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @B22)) return false;
			@B22 that_ = (@B22)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@B22 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B22 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B22();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"B22\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b22": this.@b22 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B22\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IB22 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B22();
		private static Comparer_B22_a2 thisComparer = new Comparer_B22_a2();
		public override int Compare(GRGEN_MODEL.IB22 a, GRGEN_MODEL.IB22 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB22> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB22> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_B22_b22 : Comparer<GRGEN_MODEL.IB22>
	{
		private static GRGEN_MODEL.IB22 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B22();
		private static Comparer_B22_b22 thisComparer = new Comparer_B22_b22();
		public override int Compare(GRGEN_MODEL.IB22 a, GRGEN_MODEL.IB22 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB22> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB22> list, int entry)
		{
			nodeBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB22> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB22> list)
		{
			List<GRGEN_MODEL.IB22> newList = new List<GRGEN_MODEL.IB22>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node B23 ***

	public interface IB23 : IA2
	{
		int @b23 { get; set; }
	}

	public sealed partial class @B23 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB23
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@B23[] pool = new GRGEN_MODEL.@B23[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@B23(this); }

		private @B23(GRGEN_MODEL.@B23 oldElem) : base(GRGEN_MODEL.NodeType_B23.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b23_M0no_suXx_h4rD = oldElem.b23_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @B23)) return false;
			@B23 that_ = (@B23)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b23_M0no_suXx_h4rD == that_.b23_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@B23 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B23 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B23();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"B23\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": this.@a2 = (int) value; return;
				case "b23": this.@b23 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B23\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IB23 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B23();
		private static Comparer_B23_a2 thisComparer = new Comparer_B23_a2();
		public override int Compare(GRGEN_MODEL.IB23 a, GRGEN_MODEL.IB23 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB23> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB23> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_B23_b23 : Comparer<GRGEN_MODEL.IB23>
	{
		private static GRGEN_MODEL.IB23 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B23();
		private static Comparer_B23_b23 thisComparer = new Comparer_B23_b23();
		public override int Compare(GRGEN_MODEL.IB23 a, GRGEN_MODEL.IB23 b)
		{
			return a.@b23.CompareTo(b.@b23);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB23> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b23.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB23> list, int entry)
		{
			nodeBearingAttributeForSearch.@b23 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB23> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB23> list)
		{
			List<GRGEN_MODEL.IB23> newList = new List<GRGEN_MODEL.IB23>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node B41 ***

	public interface IB41 : IA4
	{
		int @b41 { get; set; }
	}

	public sealed partial class @B41 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB41
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@B41[] pool = new GRGEN_MODEL.@B41[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@B41(this); }

		private @B41(GRGEN_MODEL.@B41 oldElem) : base(GRGEN_MODEL.NodeType_B41.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @B41)) return false;
			@B41 that_ = (@B41)that;
			return true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@B41 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B41 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B41();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"B41\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
				case "b41": this.@b41 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B41\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IB41 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B41();
		private static Comparer_B41_a4 thisComparer = new Comparer_B41_a4();
		public override int Compare(GRGEN_MODEL.IB41 a, GRGEN_MODEL.IB41 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB41> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB41> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_B41_b41 : Comparer<GRGEN_MODEL.IB41>
	{
		private static GRGEN_MODEL.IB41 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B41();
		private static Comparer_B41_b41 thisComparer = new Comparer_B41_b41();
		public override int Compare(GRGEN_MODEL.IB41 a, GRGEN_MODEL.IB41 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB41> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB41> list, int entry)
		{
			nodeBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB41> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB41> list)
		{
			List<GRGEN_MODEL.IB41> newList = new List<GRGEN_MODEL.IB41>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node B42 ***

	public interface IB42 : IA4
	{
		int @b42 { get; set; }
	}

	public sealed partial class @B42 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB42
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@B42[] pool = new GRGEN_MODEL.@B42[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@B42(this); }

		private @B42(GRGEN_MODEL.@B42 oldElem) : base(GRGEN_MODEL.NodeType_B42.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @B42)) return false;
			@B42 that_ = (@B42)that;
			return true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@B42 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B42 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B42();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"B42\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
				case "b42": this.@b42 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B42\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IB42 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B42();
		private static Comparer_B42_a4 thisComparer = new Comparer_B42_a4();
		public override int Compare(GRGEN_MODEL.IB42 a, GRGEN_MODEL.IB42 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB42> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB42> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_B42_b42 : Comparer<GRGEN_MODEL.IB42>
	{
		private static GRGEN_MODEL.IB42 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B42();
		private static Comparer_B42_b42 thisComparer = new Comparer_B42_b42();
		public override int Compare(GRGEN_MODEL.IB42 a, GRGEN_MODEL.IB42 b)
		{
			return a.@b42.CompareTo(b.@b42);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB42> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB42> list, int entry)
		{
			nodeBearingAttributeForSearch.@b42 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB42> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB42> list)
		{
			List<GRGEN_MODEL.IB42> newList = new List<GRGEN_MODEL.IB42>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node B43 ***

	public interface IB43 : IA4
	{
	}

	public sealed partial class @B43 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IB43
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@B43[] pool = new GRGEN_MODEL.@B43[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@B43(this); }

		private @B43(GRGEN_MODEL.@B43 oldElem) : base(GRGEN_MODEL.NodeType_B43.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @B43)) return false;
			@B43 that_ = (@B43)that;
			return true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@B43 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@B43 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@B43();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"B43\" does not have the attribute \"" + attrName + "\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": this.@a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B43\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IB43 nodeBearingAttributeForSearch = new GRGEN_MODEL.@B43();
		private static Comparer_B43_a4 thisComparer = new Comparer_B43_a4();
		public override int Compare(GRGEN_MODEL.IB43 a, GRGEN_MODEL.IB43 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IB43> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IB43> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IB43> ArrayOrderAscendingBy(List<GRGEN_MODEL.IB43> list)
		{
			List<GRGEN_MODEL.IB43> newList = new List<GRGEN_MODEL.IB43>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node C221 ***

	public interface IC221 : IB22
	{
		int @c221 { get; set; }
	}

	public sealed partial class @C221 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC221
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@C221[] pool = new GRGEN_MODEL.@C221[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@C221(this); }

		private @C221(GRGEN_MODEL.@C221 oldElem) : base(GRGEN_MODEL.NodeType_C221.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
			c221_M0no_suXx_h4rD = oldElem.c221_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @C221)) return false;
			@C221 that_ = (@C221)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& c221_M0no_suXx_h4rD == that_.c221_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@C221 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C221 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C221();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"C221\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"C221\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IC221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C221();
		private static Comparer_C221_a2 thisComparer = new Comparer_C221_a2();
		public override int Compare(GRGEN_MODEL.IC221 a, GRGEN_MODEL.IC221 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC221> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C221_b22 : Comparer<GRGEN_MODEL.IC221>
	{
		private static GRGEN_MODEL.IC221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C221();
		private static Comparer_C221_b22 thisComparer = new Comparer_C221_b22();
		public override int Compare(GRGEN_MODEL.IC221 a, GRGEN_MODEL.IC221 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC221> list, int entry)
		{
			nodeBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C221_c221 : Comparer<GRGEN_MODEL.IC221>
	{
		private static GRGEN_MODEL.IC221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C221();
		private static Comparer_C221_c221 thisComparer = new Comparer_C221_c221();
		public override int Compare(GRGEN_MODEL.IC221 a, GRGEN_MODEL.IC221 b)
		{
			return a.@c221.CompareTo(b.@c221);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC221> list, int entry)
		{
			nodeBearingAttributeForSearch.@c221 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC221> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC221> list)
		{
			List<GRGEN_MODEL.IC221> newList = new List<GRGEN_MODEL.IC221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node C222_411 ***

	public interface IC222_411 : IB22, IB41
	{
		int @c222_411 { get; set; }
	}

	public sealed partial class @C222_411 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC222_411
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@C222_411[] pool = new GRGEN_MODEL.@C222_411[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@C222_411(this); }

		private @C222_411(GRGEN_MODEL.@C222_411 oldElem) : base(GRGEN_MODEL.NodeType_C222_411.typeVar)
		{
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			c222_411_M0no_suXx_h4rD = oldElem.c222_411_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @C222_411)) return false;
			@C222_411 that_ = (@C222_411)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& c222_411_M0no_suXx_h4rD == that_.c222_411_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@C222_411 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C222_411 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C222_411();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"C222_411\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"C222_411\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IC222_411 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		private static Comparer_C222_411_a2 thisComparer = new Comparer_C222_411_a2();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C222_411_b22 : Comparer<GRGEN_MODEL.IC222_411>
	{
		private static GRGEN_MODEL.IC222_411 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		private static Comparer_C222_411_b22 thisComparer = new Comparer_C222_411_b22();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			nodeBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C222_411_a4 : Comparer<GRGEN_MODEL.IC222_411>
	{
		private static GRGEN_MODEL.IC222_411 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		private static Comparer_C222_411_a4 thisComparer = new Comparer_C222_411_a4();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C222_411_b41 : Comparer<GRGEN_MODEL.IC222_411>
	{
		private static GRGEN_MODEL.IC222_411 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		private static Comparer_C222_411_b41 thisComparer = new Comparer_C222_411_b41();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			nodeBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C222_411_c222_411 : Comparer<GRGEN_MODEL.IC222_411>
	{
		private static GRGEN_MODEL.IC222_411 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C222_411();
		private static Comparer_C222_411_c222_411 thisComparer = new Comparer_C222_411_c222_411();
		public override int Compare(GRGEN_MODEL.IC222_411 a, GRGEN_MODEL.IC222_411 b)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC222_411> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC222_411> list, int entry)
		{
			nodeBearingAttributeForSearch.@c222_411 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC222_411> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC222_411> list)
		{
			List<GRGEN_MODEL.IC222_411> newList = new List<GRGEN_MODEL.IC222_411>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node C412_421_431_51 ***

	public interface IC412_421_431_51 : IB41, IB42, IB43, IA5
	{
	}

	public sealed partial class @C412_421_431_51 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC412_421_431_51
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@C412_421_431_51[] pool = new GRGEN_MODEL.@C412_421_431_51[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@C412_421_431_51(this); }

		private @C412_421_431_51(GRGEN_MODEL.@C412_421_431_51 oldElem) : base(GRGEN_MODEL.NodeType_C412_421_431_51.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
			a5_M0no_suXx_h4rD = oldElem.a5_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @C412_421_431_51)) return false;
			@C412_421_431_51 that_ = (@C412_421_431_51)that;
			return true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
				&& a5_M0no_suXx_h4rD == that_.a5_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@C412_421_431_51 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C412_421_431_51 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C412_421_431_51();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"C412_421_431_51\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"C412_421_431_51\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IC412_421_431_51 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		private static Comparer_C412_421_431_51_a4 thisComparer = new Comparer_C412_421_431_51_a4();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C412_421_431_51_b41 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		private static GRGEN_MODEL.IC412_421_431_51 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		private static Comparer_C412_421_431_51_b41 thisComparer = new Comparer_C412_421_431_51_b41();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			nodeBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C412_421_431_51_b42 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		private static GRGEN_MODEL.IC412_421_431_51 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		private static Comparer_C412_421_431_51_b42 thisComparer = new Comparer_C412_421_431_51_b42();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@b42.CompareTo(b.@b42);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			nodeBearingAttributeForSearch.@b42 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C412_421_431_51_a5 : Comparer<GRGEN_MODEL.IC412_421_431_51>
	{
		private static GRGEN_MODEL.IC412_421_431_51 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C412_421_431_51();
		private static Comparer_C412_421_431_51_a5 thisComparer = new Comparer_C412_421_431_51_a5();
		public override int Compare(GRGEN_MODEL.IC412_421_431_51 a, GRGEN_MODEL.IC412_421_431_51 b)
		{
			return a.@a5.CompareTo(b.@a5);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC412_421_431_51> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a5.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC412_421_431_51> list, int entry)
		{
			nodeBearingAttributeForSearch.@a5 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC412_421_431_51> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC412_421_431_51> list)
		{
			List<GRGEN_MODEL.IC412_421_431_51> newList = new List<GRGEN_MODEL.IC412_421_431_51>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node C432_422 ***

	public interface IC432_422 : IB43, IB42
	{
		int @c432_422 { get; set; }
	}

	public sealed partial class @C432_422 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC432_422
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@C432_422[] pool = new GRGEN_MODEL.@C432_422[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@C432_422(this); }

		private @C432_422(GRGEN_MODEL.@C432_422 oldElem) : base(GRGEN_MODEL.NodeType_C432_422.typeVar)
		{
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b42_M0no_suXx_h4rD = oldElem.b42_M0no_suXx_h4rD;
			c432_422_M0no_suXx_h4rD = oldElem.c432_422_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @C432_422)) return false;
			@C432_422 that_ = (@C432_422)that;
			return true
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b42_M0no_suXx_h4rD == that_.b42_M0no_suXx_h4rD
				&& c432_422_M0no_suXx_h4rD == that_.c432_422_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@C432_422 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C432_422 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C432_422();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"C432_422\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"C432_422\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.IC432_422 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C432_422();
		private static Comparer_C432_422_a4 thisComparer = new Comparer_C432_422_a4();
		public override int Compare(GRGEN_MODEL.IC432_422 a, GRGEN_MODEL.IC432_422 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC432_422> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C432_422_b42 : Comparer<GRGEN_MODEL.IC432_422>
	{
		private static GRGEN_MODEL.IC432_422 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C432_422();
		private static Comparer_C432_422_b42 thisComparer = new Comparer_C432_422_b42();
		public override int Compare(GRGEN_MODEL.IC432_422 a, GRGEN_MODEL.IC432_422 b)
		{
			return a.@b42.CompareTo(b.@b42);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b42.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC432_422> list, int entry)
		{
			nodeBearingAttributeForSearch.@b42 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_C432_422_c432_422 : Comparer<GRGEN_MODEL.IC432_422>
	{
		private static GRGEN_MODEL.IC432_422 nodeBearingAttributeForSearch = new GRGEN_MODEL.@C432_422();
		private static Comparer_C432_422_c432_422 thisComparer = new Comparer_C432_422_c432_422();
		public override int Compare(GRGEN_MODEL.IC432_422 a, GRGEN_MODEL.IC432_422 b)
		{
			return a.@c432_422.CompareTo(b.@c432_422);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.IC432_422> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c432_422.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.IC432_422> list, int entry)
		{
			nodeBearingAttributeForSearch.@c432_422 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.IC432_422> ArrayOrderAscendingBy(List<GRGEN_MODEL.IC432_422> list)
		{
			List<GRGEN_MODEL.IC432_422> newList = new List<GRGEN_MODEL.IC432_422>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node D11_2221 ***

	public interface ID11_2221 : IA1, IC222_411
	{
		int @d11_2221 { get; set; }
	}

	public sealed partial class @D11_2221 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ID11_2221
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@D11_2221[] pool = new GRGEN_MODEL.@D11_2221[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@D11_2221(this); }

		private @D11_2221(GRGEN_MODEL.@D11_2221 oldElem) : base(GRGEN_MODEL.NodeType_D11_2221.typeVar)
		{
			a1_M0no_suXx_h4rD = oldElem.a1_M0no_suXx_h4rD;
			a2_M0no_suXx_h4rD = oldElem.a2_M0no_suXx_h4rD;
			b22_M0no_suXx_h4rD = oldElem.b22_M0no_suXx_h4rD;
			a4_M0no_suXx_h4rD = oldElem.a4_M0no_suXx_h4rD;
			b41_M0no_suXx_h4rD = oldElem.b41_M0no_suXx_h4rD;
			c222_411_M0no_suXx_h4rD = oldElem.c222_411_M0no_suXx_h4rD;
			d11_2221_M0no_suXx_h4rD = oldElem.d11_2221_M0no_suXx_h4rD;
		}

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @D11_2221)) return false;
			@D11_2221 that_ = (@D11_2221)that;
			return true
				&& a1_M0no_suXx_h4rD == that_.a1_M0no_suXx_h4rD
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& c222_411_M0no_suXx_h4rD == that_.c222_411_M0no_suXx_h4rD
				&& d11_2221_M0no_suXx_h4rD == that_.d11_2221_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@D11_2221 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@D11_2221 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@D11_2221();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"D11_2221\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"D11_2221\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.ID11_2221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		private static Comparer_D11_2221_a1 thisComparer = new Comparer_D11_2221_a1();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@a1.CompareTo(b.@a1);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a1.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			nodeBearingAttributeForSearch.@a1 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D11_2221_a2 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		private static GRGEN_MODEL.ID11_2221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		private static Comparer_D11_2221_a2 thisComparer = new Comparer_D11_2221_a2();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D11_2221_b22 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		private static GRGEN_MODEL.ID11_2221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		private static Comparer_D11_2221_b22 thisComparer = new Comparer_D11_2221_b22();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			nodeBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D11_2221_a4 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		private static GRGEN_MODEL.ID11_2221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		private static Comparer_D11_2221_a4 thisComparer = new Comparer_D11_2221_a4();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D11_2221_b41 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		private static GRGEN_MODEL.ID11_2221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		private static Comparer_D11_2221_b41 thisComparer = new Comparer_D11_2221_b41();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			nodeBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D11_2221_c222_411 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		private static GRGEN_MODEL.ID11_2221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		private static Comparer_D11_2221_c222_411 thisComparer = new Comparer_D11_2221_c222_411();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			nodeBearingAttributeForSearch.@c222_411 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D11_2221_d11_2221 : Comparer<GRGEN_MODEL.ID11_2221>
	{
		private static GRGEN_MODEL.ID11_2221 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D11_2221();
		private static Comparer_D11_2221_d11_2221 thisComparer = new Comparer_D11_2221_d11_2221();
		public override int Compare(GRGEN_MODEL.ID11_2221 a, GRGEN_MODEL.ID11_2221 b)
		{
			return a.@d11_2221.CompareTo(b.@d11_2221);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID11_2221> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@d11_2221.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID11_2221> list, int entry)
		{
			nodeBearingAttributeForSearch.@d11_2221 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID11_2221> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID11_2221> list)
		{
			List<GRGEN_MODEL.ID11_2221> newList = new List<GRGEN_MODEL.ID11_2221>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node D2211_2222_31 ***

	public interface ID2211_2222_31 : IC221, IC222_411, IA3
	{
		int @d2211_2222_31 { get; set; }
	}

	public sealed partial class @D2211_2222_31 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ID2211_2222_31
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@D2211_2222_31[] pool = new GRGEN_MODEL.@D2211_2222_31[10];
		
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

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@D2211_2222_31(this); }

		private @D2211_2222_31(GRGEN_MODEL.@D2211_2222_31 oldElem) : base(GRGEN_MODEL.NodeType_D2211_2222_31.typeVar)
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

		public override bool AreAttributesEqual(GRGEN_LIBGR.IGraphElement that) {
			if(!(that is @D2211_2222_31)) return false;
			@D2211_2222_31 that_ = (@D2211_2222_31)that;
			return true
				&& a2_M0no_suXx_h4rD == that_.a2_M0no_suXx_h4rD
				&& b22_M0no_suXx_h4rD == that_.b22_M0no_suXx_h4rD
				&& c221_M0no_suXx_h4rD == that_.c221_M0no_suXx_h4rD
				&& a4_M0no_suXx_h4rD == that_.a4_M0no_suXx_h4rD
				&& b41_M0no_suXx_h4rD == that_.b41_M0no_suXx_h4rD
				&& c222_411_M0no_suXx_h4rD == that_.c222_411_M0no_suXx_h4rD
				&& a3_M0no_suXx_h4rD == that_.a3_M0no_suXx_h4rD
				&& d2211_2222_31_M0no_suXx_h4rD == that_.d2211_2222_31_M0no_suXx_h4rD
			;
		}

		public static GRGEN_MODEL.@D2211_2222_31 CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@D2211_2222_31 node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@D2211_2222_31();
			else
			{
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
			if(poolLevel < 10)
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
				"The node type \"D2211_2222_31\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"D2211_2222_31\" does not have the attribute \"" + attrName + "\"!");
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
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_a2 thisComparer = new Comparer_D2211_2222_31_a2();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@a2.CompareTo(b.@a2);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a2.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@a2 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D2211_2222_31_b22 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_b22 thisComparer = new Comparer_D2211_2222_31_b22();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@b22.CompareTo(b.@b22);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b22.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@b22 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D2211_2222_31_c221 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_c221 thisComparer = new Comparer_D2211_2222_31_c221();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@c221.CompareTo(b.@c221);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c221.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@c221 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D2211_2222_31_a4 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_a4 thisComparer = new Comparer_D2211_2222_31_a4();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@a4.CompareTo(b.@a4);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a4.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@a4 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D2211_2222_31_b41 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_b41 thisComparer = new Comparer_D2211_2222_31_b41();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@b41.CompareTo(b.@b41);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@b41.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@b41 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D2211_2222_31_c222_411 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_c222_411 thisComparer = new Comparer_D2211_2222_31_c222_411();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@c222_411.CompareTo(b.@c222_411);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@c222_411.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@c222_411 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D2211_2222_31_a3 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_a3 thisComparer = new Comparer_D2211_2222_31_a3();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@a3.CompareTo(b.@a3);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@a3.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@a3 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	public class Comparer_D2211_2222_31_d2211_2222_31 : Comparer<GRGEN_MODEL.ID2211_2222_31>
	{
		private static GRGEN_MODEL.ID2211_2222_31 nodeBearingAttributeForSearch = new GRGEN_MODEL.@D2211_2222_31();
		private static Comparer_D2211_2222_31_d2211_2222_31 thisComparer = new Comparer_D2211_2222_31_d2211_2222_31();
		public override int Compare(GRGEN_MODEL.ID2211_2222_31 a, GRGEN_MODEL.ID2211_2222_31 b)
		{
			return a.@d2211_2222_31.CompareTo(b.@d2211_2222_31);
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = 0; i < list.Count; ++i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i < list.Count; ++i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			for(int i = list.Count - 1; i >= 0; --i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int LastIndexOfBy(IList<GRGEN_MODEL.ID2211_2222_31> list, int entry, int startIndex)
		{
			for(int i = startIndex; i >= 0; --i)
				if(list[i].@d2211_2222_31.Equals(entry))
					return i;
			return -1;
		}
		public static int IndexOfOrderedBy(List<GRGEN_MODEL.ID2211_2222_31> list, int entry)
		{
			nodeBearingAttributeForSearch.@d2211_2222_31 = entry;
			return list.BinarySearch(nodeBearingAttributeForSearch, thisComparer);
		}
		public static List<GRGEN_MODEL.ID2211_2222_31> ArrayOrderAscendingBy(List<GRGEN_MODEL.ID2211_2222_31> list)
		{
			List<GRGEN_MODEL.ID2211_2222_31> newList = new List<GRGEN_MODEL.ID2211_2222_31>(list);
			newList.Sort(thisComparer);
			return newList;
		}
	}


	// *** Node D231_4121 ***

	public interface ID231_4121 : IB23, IC412_421_431_51
	{
		int @d231_4121 { get; set; }
	}

	public abstract class @D231_4121 : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.ID231_4121
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@D231_4121[] pool = new GRGEN_MODEL.@D231_4121[10];
		
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
			if(poolLevel < 10)
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
				"The node type \"D231_4121\" does not have the attribute \"" + attrName + "\"!");
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
				"The node type \"D231_4121\" does not have the attribute \"" + attrName + "\"!");
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
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
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
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
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
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
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
		public System.Type[] TypeTypes { get { return typeTypes; } }
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
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
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
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.EdgeType[] types = {
			GRGEN_MODEL.EdgeType_AEdge.typeVar,
			GRGEN_MODEL.EdgeType_Edge.typeVar,
			GRGEN_MODEL.EdgeType_UEdge.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
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
			FullyInitializeExternalTypes();
		}

		private complModelNodeModel nodeModel = new complModelNodeModel();
		private complModelEdgeModel edgeModel = new complModelEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new complModelIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((complModelIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "complModel"; } }
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

		public override string MD5Hash { get { return "6a630d39ca3371b697e3fb227fb1f51a"; } }
	}

	//
	// IGraph (LGSPGraph) / IGraphModel implementation
	//
	public class complModelGraph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public complModelGraph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
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

		private complModelNodeModel nodeModel = new complModelNodeModel();
		private complModelEdgeModel edgeModel = new complModelEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new complModelIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((complModelIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "complModel"; } }
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

		public string MD5Hash { get { return "6a630d39ca3371b697e3fb227fb1f51a"; } }
	}

	//
	// INamedGraph (LGSPNamedGraph) / IGraphModel implementation
	//
	public class complModelNamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel
	{
		public complModelNamedGraph() : base(GetNextGraphName())
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

		private complModelNodeModel nodeModel = new complModelNodeModel();
		private complModelEdgeModel edgeModel = new complModelEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new complModelIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((complModelIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "complModel"; } }
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

		public string MD5Hash { get { return "6a630d39ca3371b697e3fb227fb1f51a"; } }
	}
}
