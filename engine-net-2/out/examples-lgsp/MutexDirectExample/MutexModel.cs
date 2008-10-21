// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Mutex\MutexPimped.grg" on Tue Oct 21 19:34:24 CEST 2008

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_Mutex
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

	public enum NodeTypes { @Node, @Process, @Resource };

	// *** Node Node ***


	public sealed class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		private static int poolLevel = 0;
		private static @Node[] pool = new @Node[10];
		public @Node() : base(NodeType_Node.typeVar)
		{
		}

		public static NodeType_Node TypeInstance { get { return NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Node(this); }

		private @Node(@Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
		public static @Node CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Node CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node, varName);
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
				"The node type \"Node\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Node\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Node typeVar = new NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override String Name { get { return "Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Node();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Node();
		}

	}

	// *** Node Process ***

	public interface IProcess : GRGEN_LIBGR.INode
	{
	}

	public sealed class @Process : GRGEN_LGSP.LGSPNode, IProcess
	{
		private static int poolLevel = 0;
		private static @Process[] pool = new @Process[10];
		public @Process() : base(NodeType_Process.typeVar)
		{
		}

		public static NodeType_Process TypeInstance { get { return NodeType_Process.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Process(this); }

		private @Process(@Process oldElem) : base(NodeType_Process.typeVar)
		{
		}
		public static @Process CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Process node;
			if(poolLevel == 0)
				node = new @Process();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Process CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Process node;
			if(poolLevel == 0)
				node = new @Process();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node, varName);
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
				"The node type \"Process\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Process\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_Process : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Process typeVar = new NodeType_Process();
		public static bool[] isA = new bool[] { true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, };
		public NodeType_Process() : base((int) NodeTypes.@Process)
		{
		}
		public override String Name { get { return "Process"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Process();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Process();
		}

	}

	// *** Node Resource ***

	public interface IResource : GRGEN_LIBGR.INode
	{
	}

	public sealed class @Resource : GRGEN_LGSP.LGSPNode, IResource
	{
		private static int poolLevel = 0;
		private static @Resource[] pool = new @Resource[10];
		public @Resource() : base(NodeType_Resource.typeVar)
		{
		}

		public static NodeType_Resource TypeInstance { get { return NodeType_Resource.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @Resource(this); }

		private @Resource(@Resource oldElem) : base(NodeType_Resource.typeVar)
		{
		}
		public static @Resource CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@Resource node;
			if(poolLevel == 0)
				node = new @Resource();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Resource CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@Resource node;
			if(poolLevel == 0)
				node = new @Resource();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node, varName);
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
				"The node type \"Resource\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"Resource\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_Resource : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Resource typeVar = new NodeType_Resource();
		public static bool[] isA = new bool[] { true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, true, };
		public NodeType_Resource() : base((int) NodeTypes.@Resource)
		{
		}
		public override String Name { get { return "Resource"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @Resource();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new @Resource();
		}

	}

	//
	// Node model
	//

	public sealed class MutexNodeModel : GRGEN_LIBGR.INodeModel
	{
		public MutexNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
				NodeType_Process.typeVar,
				NodeType_Resource.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Process.typeVar,
				NodeType_Resource.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Process.typeVar.subOrSameGrGenTypes = NodeType_Process.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Process.typeVar,
			};
			NodeType_Process.typeVar.directSubGrGenTypes = NodeType_Process.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Process.typeVar.superOrSameGrGenTypes = NodeType_Process.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Process.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Process.typeVar.directSuperGrGenTypes = NodeType_Process.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Resource.typeVar.subOrSameGrGenTypes = NodeType_Resource.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Resource.typeVar,
			};
			NodeType_Resource.typeVar.directSubGrGenTypes = NodeType_Resource.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Resource.typeVar.superOrSameGrGenTypes = NodeType_Resource.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Resource.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Resource.typeVar.directSuperGrGenTypes = NodeType_Resource.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public GRGEN_LIBGR.NodeType RootType { get { return NodeType_Node.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return NodeType_Node.typeVar; } }
		public GRGEN_LIBGR.NodeType GetType(String name)
		{
			switch(name)
			{
				case "Node" : return NodeType_Node.typeVar;
				case "Process" : return NodeType_Process.typeVar;
				case "Resource" : return NodeType_Resource.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_Process.typeVar,
			NodeType_Resource.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_Process),
			typeof(NodeType_Resource),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @next, @blocked, @held_by, @token, @release, @request };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override String Name { get { return "AEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Arbitrary; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
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
		private static @Edge[] pool = new @Edge[10];
		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
		}

		public static EdgeType_Edge TypeInstance { get { return EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @Edge(@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public static @Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"Edge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"Edge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static @UEdge[] pool = new @UEdge[10];
		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_UEdge.typeVar, source, target)
		{
		}

		public static EdgeType_UEdge TypeInstance { get { return EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @UEdge(@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		public static @UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"UEdge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"UEdge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_UEdge typeVar = new EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override String Name { get { return "UEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Undirected; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge next ***

	public interface Inext : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @next : GRGEN_LGSP.LGSPEdge, Inext
	{
		private static int poolLevel = 0;
		private static @next[] pool = new @next[10];
		public @next(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_next.typeVar, source, target)
		{
		}

		public static EdgeType_next TypeInstance { get { return EdgeType_next.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @next(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @next(@next oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_next.typeVar, newSource, newTarget)
		{
		}
		public static @next CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@next edge;
			if(poolLevel == 0)
				edge = new @next(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @next CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@next edge;
			if(poolLevel == 0)
				edge = new @next(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"next\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"next\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_next : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_next typeVar = new EdgeType_next();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, };
		public EdgeType_next() : base((int) EdgeTypes.@next)
		{
		}
		public override String Name { get { return "next"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @next((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @next((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge blocked ***

	public interface Iblocked : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @blocked : GRGEN_LGSP.LGSPEdge, Iblocked
	{
		private static int poolLevel = 0;
		private static @blocked[] pool = new @blocked[10];
		public @blocked(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_blocked.typeVar, source, target)
		{
		}

		public static EdgeType_blocked TypeInstance { get { return EdgeType_blocked.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @blocked(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @blocked(@blocked oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_blocked.typeVar, newSource, newTarget)
		{
		}
		public static @blocked CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@blocked edge;
			if(poolLevel == 0)
				edge = new @blocked(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @blocked CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@blocked edge;
			if(poolLevel == 0)
				edge = new @blocked(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"blocked\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"blocked\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_blocked : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_blocked typeVar = new EdgeType_blocked();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, };
		public EdgeType_blocked() : base((int) EdgeTypes.@blocked)
		{
		}
		public override String Name { get { return "blocked"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @blocked((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @blocked((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge held_by ***

	public interface Iheld_by : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @held_by : GRGEN_LGSP.LGSPEdge, Iheld_by
	{
		private static int poolLevel = 0;
		private static @held_by[] pool = new @held_by[10];
		public @held_by(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_held_by.typeVar, source, target)
		{
		}

		public static EdgeType_held_by TypeInstance { get { return EdgeType_held_by.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @held_by(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @held_by(@held_by oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_held_by.typeVar, newSource, newTarget)
		{
		}
		public static @held_by CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@held_by edge;
			if(poolLevel == 0)
				edge = new @held_by(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @held_by CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@held_by edge;
			if(poolLevel == 0)
				edge = new @held_by(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"held_by\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"held_by\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_held_by : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_held_by typeVar = new EdgeType_held_by();
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, };
		public EdgeType_held_by() : base((int) EdgeTypes.@held_by)
		{
		}
		public override String Name { get { return "held_by"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @held_by((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @held_by((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge token ***

	public interface Itoken : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @token : GRGEN_LGSP.LGSPEdge, Itoken
	{
		private static int poolLevel = 0;
		private static @token[] pool = new @token[10];
		public @token(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_token.typeVar, source, target)
		{
		}

		public static EdgeType_token TypeInstance { get { return EdgeType_token.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @token(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @token(@token oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_token.typeVar, newSource, newTarget)
		{
		}
		public static @token CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@token edge;
			if(poolLevel == 0)
				edge = new @token(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @token CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@token edge;
			if(poolLevel == 0)
				edge = new @token(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"token\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"token\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_token : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_token typeVar = new EdgeType_token();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, };
		public EdgeType_token() : base((int) EdgeTypes.@token)
		{
		}
		public override String Name { get { return "token"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @token((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @token((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge release ***

	public interface Irelease : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @release : GRGEN_LGSP.LGSPEdge, Irelease
	{
		private static int poolLevel = 0;
		private static @release[] pool = new @release[10];
		public @release(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_release.typeVar, source, target)
		{
		}

		public static EdgeType_release TypeInstance { get { return EdgeType_release.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @release(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @release(@release oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_release.typeVar, newSource, newTarget)
		{
		}
		public static @release CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@release edge;
			if(poolLevel == 0)
				edge = new @release(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @release CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@release edge;
			if(poolLevel == 0)
				edge = new @release(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"release\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"release\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_release : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_release typeVar = new EdgeType_release();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, };
		public EdgeType_release() : base((int) EdgeTypes.@release)
		{
		}
		public override String Name { get { return "release"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @release((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @release((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge request ***

	public interface Irequest : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @request : GRGEN_LGSP.LGSPEdge, Irequest
	{
		private static int poolLevel = 0;
		private static @request[] pool = new @request[10];
		public @request(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_request.typeVar, source, target)
		{
		}

		public static EdgeType_request TypeInstance { get { return EdgeType_request.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new @request(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @request(@request oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(EdgeType_request.typeVar, newSource, newTarget)
		{
		}
		public static @request CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			@request edge;
			if(poolLevel == 0)
				edge = new @request(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @request CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			@request edge;
			if(poolLevel == 0)
				edge = new @request(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge, varName);
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
				"The edge type \"request\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"request\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_request : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_request typeVar = new EdgeType_request();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, };
		public EdgeType_request() : base((int) EdgeTypes.@request)
		{
		}
		public override String Name { get { return "request"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new @request((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new @request((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class MutexEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public MutexEdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
				EdgeType_next.typeVar,
				EdgeType_blocked.typeVar,
				EdgeType_held_by.typeVar,
				EdgeType_token.typeVar,
				EdgeType_release.typeVar,
				EdgeType_request.typeVar,
			};
			EdgeType_AEdge.typeVar.directSubGrGenTypes = EdgeType_AEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.superOrSameGrGenTypes = EdgeType_AEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.directSuperGrGenTypes = EdgeType_AEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_next.typeVar,
				EdgeType_blocked.typeVar,
				EdgeType_held_by.typeVar,
				EdgeType_token.typeVar,
				EdgeType_release.typeVar,
				EdgeType_request.typeVar,
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_next.typeVar,
				EdgeType_blocked.typeVar,
				EdgeType_held_by.typeVar,
				EdgeType_token.typeVar,
				EdgeType_release.typeVar,
				EdgeType_request.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_Edge.typeVar.directSuperGrGenTypes = EdgeType_Edge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.subOrSameGrGenTypes = EdgeType_UEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_UEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSubGrGenTypes = EdgeType_UEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_UEdge.typeVar.superOrSameGrGenTypes = EdgeType_UEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_UEdge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSuperGrGenTypes = EdgeType_UEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_next.typeVar.subOrSameGrGenTypes = EdgeType_next.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_next.typeVar,
			};
			EdgeType_next.typeVar.directSubGrGenTypes = EdgeType_next.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_next.typeVar.superOrSameGrGenTypes = EdgeType_next.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_next.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_next.typeVar.directSuperGrGenTypes = EdgeType_next.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_blocked.typeVar.subOrSameGrGenTypes = EdgeType_blocked.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_blocked.typeVar,
			};
			EdgeType_blocked.typeVar.directSubGrGenTypes = EdgeType_blocked.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_blocked.typeVar.superOrSameGrGenTypes = EdgeType_blocked.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_blocked.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_blocked.typeVar.directSuperGrGenTypes = EdgeType_blocked.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_held_by.typeVar.subOrSameGrGenTypes = EdgeType_held_by.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_held_by.typeVar,
			};
			EdgeType_held_by.typeVar.directSubGrGenTypes = EdgeType_held_by.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_held_by.typeVar.superOrSameGrGenTypes = EdgeType_held_by.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_held_by.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_held_by.typeVar.directSuperGrGenTypes = EdgeType_held_by.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_token.typeVar.subOrSameGrGenTypes = EdgeType_token.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_token.typeVar,
			};
			EdgeType_token.typeVar.directSubGrGenTypes = EdgeType_token.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_token.typeVar.superOrSameGrGenTypes = EdgeType_token.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_token.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_token.typeVar.directSuperGrGenTypes = EdgeType_token.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_release.typeVar.subOrSameGrGenTypes = EdgeType_release.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_release.typeVar,
			};
			EdgeType_release.typeVar.directSubGrGenTypes = EdgeType_release.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_release.typeVar.superOrSameGrGenTypes = EdgeType_release.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_release.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_release.typeVar.directSuperGrGenTypes = EdgeType_release.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_request.typeVar.subOrSameGrGenTypes = EdgeType_request.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_request.typeVar,
			};
			EdgeType_request.typeVar.directSubGrGenTypes = EdgeType_request.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			EdgeType_request.typeVar.superOrSameGrGenTypes = EdgeType_request.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_request.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_request.typeVar.directSuperGrGenTypes = EdgeType_request.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public GRGEN_LIBGR.EdgeType RootType { get { return EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return EdgeType_AEdge.typeVar; } }
		public GRGEN_LIBGR.EdgeType GetType(String name)
		{
			switch(name)
			{
				case "AEdge" : return EdgeType_AEdge.typeVar;
				case "Edge" : return EdgeType_Edge.typeVar;
				case "UEdge" : return EdgeType_UEdge.typeVar;
				case "next" : return EdgeType_next.typeVar;
				case "blocked" : return EdgeType_blocked.typeVar;
				case "held_by" : return EdgeType_held_by.typeVar;
				case "token" : return EdgeType_token.typeVar;
				case "release" : return EdgeType_release.typeVar;
				case "request" : return EdgeType_request.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.EdgeType[] types = {
			EdgeType_AEdge.typeVar,
			EdgeType_Edge.typeVar,
			EdgeType_UEdge.typeVar,
			EdgeType_next.typeVar,
			EdgeType_blocked.typeVar,
			EdgeType_held_by.typeVar,
			EdgeType_token.typeVar,
			EdgeType_release.typeVar,
			EdgeType_request.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_AEdge),
			typeof(EdgeType_Edge),
			typeof(EdgeType_UEdge),
			typeof(EdgeType_next),
			typeof(EdgeType_blocked),
			typeof(EdgeType_held_by),
			typeof(EdgeType_token),
			typeof(EdgeType_release),
			typeof(EdgeType_request),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class MutexGraphModel : GRGEN_LIBGR.IGraphModel
	{
		private MutexNodeModel nodeModel = new MutexNodeModel();
		private MutexEdgeModel edgeModel = new MutexEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(EdgeType_next.typeVar, NodeType_Process.typeVar, NodeType_Process.typeVar, 0, 1, 0, 1),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_blocked.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 0, long.MaxValue, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_held_by.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_token.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_release.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_request.typeVar, NodeType_Process.typeVar, NodeType_Resource.typeVar, 0, long.MaxValue, 0, long.MaxValue),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "Mutex"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "b2c79abf46750619401de30166fff963"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class Mutex : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public Mutex() : base(GetNextGraphName())
		{
			InitializeGraph(this);
		}

		public @Node CreateNodeNode()
		{
			return @Node.CreateNode(this);
		}

		public @Node CreateNodeNode(String varName)
		{
			return @Node.CreateNode(this, varName);
		}

		public @Process CreateNodeProcess()
		{
			return @Process.CreateNode(this);
		}

		public @Process CreateNodeProcess(String varName)
		{
			return @Process.CreateNode(this, varName);
		}

		public @Resource CreateNodeResource()
		{
			return @Resource.CreateNode(this);
		}

		public @Resource CreateNodeResource(String varName)
		{
			return @Resource.CreateNode(this, varName);
		}

		public @Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @Edge.CreateEdge(this, source, target);
		}

		public @Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @Edge.CreateEdge(this, source, target, varName);
		}

		public @UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @UEdge.CreateEdge(this, source, target);
		}

		public @UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @UEdge.CreateEdge(this, source, target, varName);
		}

		public @next CreateEdgenext(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @next.CreateEdge(this, source, target);
		}

		public @next CreateEdgenext(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @next.CreateEdge(this, source, target, varName);
		}

		public @blocked CreateEdgeblocked(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @blocked.CreateEdge(this, source, target);
		}

		public @blocked CreateEdgeblocked(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @blocked.CreateEdge(this, source, target, varName);
		}

		public @held_by CreateEdgeheld_by(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @held_by.CreateEdge(this, source, target);
		}

		public @held_by CreateEdgeheld_by(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @held_by.CreateEdge(this, source, target, varName);
		}

		public @token CreateEdgetoken(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @token.CreateEdge(this, source, target);
		}

		public @token CreateEdgetoken(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @token.CreateEdge(this, source, target, varName);
		}

		public @release CreateEdgerelease(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @release.CreateEdge(this, source, target);
		}

		public @release CreateEdgerelease(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @release.CreateEdge(this, source, target, varName);
		}

		public @request CreateEdgerequest(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @request.CreateEdge(this, source, target);
		}

		public @request CreateEdgerequest(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, String varName)
		{
			return @request.CreateEdge(this, source, target, varName);
		}

		private MutexNodeModel nodeModel = new MutexNodeModel();
		private MutexEdgeModel edgeModel = new MutexEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(EdgeType_next.typeVar, NodeType_Process.typeVar, NodeType_Process.typeVar, 0, 1, 0, 1),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_blocked.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 0, long.MaxValue, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_held_by.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_token.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_release.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new GRGEN_LIBGR.ValidateInfo(EdgeType_request.typeVar, NodeType_Process.typeVar, NodeType_Resource.typeVar, 0, long.MaxValue, 0, long.MaxValue),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "Mutex"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "b2c79abf46750619401de30166fff963"; } }
	}
}
