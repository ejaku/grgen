// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\Recursive\Recursive.grg" on Mon Jul 28 08:46:16 CEST 2014

using System;
using System.Collections.Generic;
using System.IO;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Model_Std
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

	public enum NodeTypes { @Node=0 };

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
		public static bool[] isA = new bool[] { true, };
		public override bool IsA(int typeID) { return isA[typeID]; }
		public static bool[] isMyType = new bool[] { true, };
		public override bool IsMyType(int typeID) { return isMyType[typeID]; }
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string Package { get { return null; } }
		public override string PackagePrefixedName { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Std.@Node"; } }
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

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge=0, @Edge=1, @UEdge=2 };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
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
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Std.@Edge"; } }
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
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Std.@UEdge"; } }
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

	public class StdIndexSet : GRGEN_LIBGR.IIndexSet
	{
		public StdIndexSet(GRGEN_LGSP.LGSPGraph graph)
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

	public sealed class StdNodeModel : GRGEN_LIBGR.INodeModel
	{
		public StdNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
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
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge model
	//

	public sealed class StdEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public StdEdgeModel()
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
	public sealed class StdGraphModel : GRGEN_LGSP.LGSPGraphModel
	{
		public StdGraphModel()
		{
			FullyInitializeExternalTypes();
		}

		private StdNodeModel nodeModel = new StdNodeModel();
		private StdEdgeModel edgeModel = new StdEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public override void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new StdIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public override void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((StdIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public override string ModelName { get { return "Std"; } }
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

		public override string MD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
	}

	//
	// IGraph (LGSPGraph) / IGraphModel implementation
	//
	public class StdGraph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public StdGraph() : base(GetNextGraphName())
		{
			FullyInitializeExternalTypes();
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		private StdNodeModel nodeModel = new StdNodeModel();
		private StdEdgeModel edgeModel = new StdEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new StdIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((StdIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "Std"; } }
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

		public string MD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
	}

	//
	// INamedGraph (LGSPNamedGraph) / IGraphModel implementation
	//
	public class StdNamedGraph : GRGEN_LGSP.LGSPNamedGraph, GRGEN_LIBGR.IGraphModel
	{
		public StdNamedGraph() : base(GetNextGraphName())
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

		private StdNodeModel nodeModel = new StdNodeModel();
		private StdEdgeModel edgeModel = new StdEdgeModel();
		private string[] packages = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private static GRGEN_LIBGR.IndexDescription[] indexDescriptions = {
		};
		public void CreateAndBindIndexSet(GRGEN_LIBGR.IGraph graph) {
			((GRGEN_LGSP.LGSPGraph)graph).indices = new StdIndexSet((GRGEN_LGSP.LGSPGraph)graph);
		}
		public void FillIndexSetAsClone(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IGraph originalGraph, IDictionary<GRGEN_LIBGR.IGraphElement, GRGEN_LIBGR.IGraphElement> oldToNewMap) {
			((StdIndexSet)((GRGEN_LGSP.LGSPGraph)graph).indices).FillAsClone((GRGEN_LGSP.LGSPGraph)originalGraph, oldToNewMap);
		}

		public string ModelName { get { return "Std"; } }
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

		public string MD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
	}
}
