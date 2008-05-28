// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Mutex\MutexPimped.grg" on Wed May 28 22:10:35 CEST 2008

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

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


	public sealed class @Node : LGSPNode, INode
	{
		private static int poolLevel = 0;
		private static @Node[] pool = new @Node[10];
		public @Node() : base(NodeType_Node.typeVar)
		{
		}

		public static NodeType_Node TypeInstance { get { return NodeType_Node.typeVar; } }

		public override INode Clone() { return new @Node(this); }

		private @Node(@Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
		public static @Node CreateNode(LGSPGraph graph)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Node CreateNode(LGSPGraph graph, String varName)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Node : NodeType
	{
		public static NodeType_Node typeVar = new NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override String Name { get { return "Node"; } }
		public override INode CreateNode()
		{
			return new @Node();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Node();
		}

	}

	// *** Node Process ***

	public interface IProcess : INode
	{
	}

	public sealed class @Process : LGSPNode, IProcess
	{
		private static int poolLevel = 0;
		private static @Process[] pool = new @Process[10];
		public @Process() : base(NodeType_Process.typeVar)
		{
		}

		public static NodeType_Process TypeInstance { get { return NodeType_Process.typeVar; } }

		public override INode Clone() { return new @Process(this); }

		private @Process(@Process oldElem) : base(NodeType_Process.typeVar)
		{
		}
		public static @Process CreateNode(LGSPGraph graph)
		{
			@Process node;
			if(poolLevel == 0)
				node = new @Process();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Process CreateNode(LGSPGraph graph, String varName)
		{
			@Process node;
			if(poolLevel == 0)
				node = new @Process();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Process : NodeType
	{
		public static NodeType_Process typeVar = new NodeType_Process();
		public static bool[] isA = new bool[] { true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, };
		public NodeType_Process() : base((int) NodeTypes.@Process)
		{
		}
		public override String Name { get { return "Process"; } }
		public override INode CreateNode()
		{
			return new @Process();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Process();
		}

	}

	// *** Node Resource ***

	public interface IResource : INode
	{
	}

	public sealed class @Resource : LGSPNode, IResource
	{
		private static int poolLevel = 0;
		private static @Resource[] pool = new @Resource[10];
		public @Resource() : base(NodeType_Resource.typeVar)
		{
		}

		public static NodeType_Resource TypeInstance { get { return NodeType_Resource.typeVar; } }

		public override INode Clone() { return new @Resource(this); }

		private @Resource(@Resource oldElem) : base(NodeType_Resource.typeVar)
		{
		}
		public static @Resource CreateNode(LGSPGraph graph)
		{
			@Resource node;
			if(poolLevel == 0)
				node = new @Resource();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Resource CreateNode(LGSPGraph graph, String varName)
		{
			@Resource node;
			if(poolLevel == 0)
				node = new @Resource();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class NodeType_Resource : NodeType
	{
		public static NodeType_Resource typeVar = new NodeType_Resource();
		public static bool[] isA = new bool[] { true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, true, };
		public NodeType_Resource() : base((int) NodeTypes.@Resource)
		{
		}
		public override String Name { get { return "Resource"; } }
		public override INode CreateNode()
		{
			return new @Resource();
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			return new @Resource();
		}

	}

	//
	// Node model
	//

	public sealed class MutexNodeModel : INodeModel
	{
		public MutexNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_Process.typeVar,
				NodeType_Resource.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new NodeType[] {
				NodeType_Process.typeVar,
				NodeType_Resource.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new NodeType[] {
			};
			NodeType_Process.typeVar.subOrSameGrGenTypes = NodeType_Process.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Process.typeVar,
			};
			NodeType_Process.typeVar.directSubGrGenTypes = NodeType_Process.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_Process.typeVar.superOrSameGrGenTypes = NodeType_Process.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Process.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Process.typeVar.directSuperGrGenTypes = NodeType_Process.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Resource.typeVar.subOrSameGrGenTypes = NodeType_Resource.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Resource.typeVar,
			};
			NodeType_Resource.typeVar.directSubGrGenTypes = NodeType_Resource.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_Resource.typeVar.superOrSameGrGenTypes = NodeType_Resource.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Resource.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Resource.typeVar.directSuperGrGenTypes = NodeType_Resource.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public NodeType RootType { get { return NodeType_Node.typeVar; } }
		GrGenType ITypeModel.RootType { get { return NodeType_Node.typeVar; } }
		public NodeType GetType(String name)
		{
			switch(name)
			{
				case "Node" : return NodeType_Node.typeVar;
				case "Process" : return NodeType_Process.typeVar;
				case "Resource" : return NodeType_Resource.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_Process.typeVar,
			NodeType_Resource.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_Process),
			typeof(NodeType_Resource),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @next, @blocked, @held_by, @token, @release, @request };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : EdgeType
	{
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override String Name { get { return "AEdge"; } }
		public override Directedness Directedness { get { return Directedness.Arbitrary; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			throw new Exception("Cannot retype to the abstract type AEdge!");
		}
	}

	// *** Edge Edge ***


	public sealed class @Edge : LGSPEdge, IEdge
	{
		private static int poolLevel = 0;
		private static @Edge[] pool = new @Edge[10];
		public @Edge(LGSPNode source, LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
		}

		public static EdgeType_Edge TypeInstance { get { return EdgeType_Edge.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @Edge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @Edge(@Edge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public static @Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_Edge : EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @Edge((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @Edge((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed class @UEdge : LGSPEdge, IEdge
	{
		private static int poolLevel = 0;
		private static @UEdge[] pool = new @UEdge[10];
		public @UEdge(LGSPNode source, LGSPNode target)
			: base(EdgeType_UEdge.typeVar, source, target)
		{
		}

		public static EdgeType_UEdge TypeInstance { get { return EdgeType_UEdge.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @UEdge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @UEdge(@UEdge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		public static @UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_UEdge : EdgeType
	{
		public static EdgeType_UEdge typeVar = new EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override String Name { get { return "UEdge"; } }
		public override Directedness Directedness { get { return Directedness.Undirected; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @UEdge((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @UEdge((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge next ***

	public interface Inext : IEdge
	{
	}

	public sealed class @next : LGSPEdge, Inext
	{
		private static int poolLevel = 0;
		private static @next[] pool = new @next[10];
		public @next(LGSPNode source, LGSPNode target)
			: base(EdgeType_next.typeVar, source, target)
		{
		}

		public static EdgeType_next TypeInstance { get { return EdgeType_next.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @next(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @next(@next oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_next.typeVar, newSource, newTarget)
		{
		}
		public static @next CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@next edge;
			if(poolLevel == 0)
				edge = new @next(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @next CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@next edge;
			if(poolLevel == 0)
				edge = new @next(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_next : EdgeType
	{
		public static EdgeType_next typeVar = new EdgeType_next();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, };
		public EdgeType_next() : base((int) EdgeTypes.@next)
		{
		}
		public override String Name { get { return "next"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @next((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @next((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge blocked ***

	public interface Iblocked : IEdge
	{
	}

	public sealed class @blocked : LGSPEdge, Iblocked
	{
		private static int poolLevel = 0;
		private static @blocked[] pool = new @blocked[10];
		public @blocked(LGSPNode source, LGSPNode target)
			: base(EdgeType_blocked.typeVar, source, target)
		{
		}

		public static EdgeType_blocked TypeInstance { get { return EdgeType_blocked.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @blocked(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @blocked(@blocked oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_blocked.typeVar, newSource, newTarget)
		{
		}
		public static @blocked CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@blocked edge;
			if(poolLevel == 0)
				edge = new @blocked(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @blocked CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@blocked edge;
			if(poolLevel == 0)
				edge = new @blocked(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_blocked : EdgeType
	{
		public static EdgeType_blocked typeVar = new EdgeType_blocked();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, };
		public EdgeType_blocked() : base((int) EdgeTypes.@blocked)
		{
		}
		public override String Name { get { return "blocked"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @blocked((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @blocked((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge held_by ***

	public interface Iheld_by : IEdge
	{
	}

	public sealed class @held_by : LGSPEdge, Iheld_by
	{
		private static int poolLevel = 0;
		private static @held_by[] pool = new @held_by[10];
		public @held_by(LGSPNode source, LGSPNode target)
			: base(EdgeType_held_by.typeVar, source, target)
		{
		}

		public static EdgeType_held_by TypeInstance { get { return EdgeType_held_by.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @held_by(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @held_by(@held_by oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_held_by.typeVar, newSource, newTarget)
		{
		}
		public static @held_by CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@held_by edge;
			if(poolLevel == 0)
				edge = new @held_by(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @held_by CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@held_by edge;
			if(poolLevel == 0)
				edge = new @held_by(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_held_by : EdgeType
	{
		public static EdgeType_held_by typeVar = new EdgeType_held_by();
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, };
		public EdgeType_held_by() : base((int) EdgeTypes.@held_by)
		{
		}
		public override String Name { get { return "held_by"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @held_by((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @held_by((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge token ***

	public interface Itoken : IEdge
	{
	}

	public sealed class @token : LGSPEdge, Itoken
	{
		private static int poolLevel = 0;
		private static @token[] pool = new @token[10];
		public @token(LGSPNode source, LGSPNode target)
			: base(EdgeType_token.typeVar, source, target)
		{
		}

		public static EdgeType_token TypeInstance { get { return EdgeType_token.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @token(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @token(@token oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_token.typeVar, newSource, newTarget)
		{
		}
		public static @token CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@token edge;
			if(poolLevel == 0)
				edge = new @token(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @token CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@token edge;
			if(poolLevel == 0)
				edge = new @token(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_token : EdgeType
	{
		public static EdgeType_token typeVar = new EdgeType_token();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, };
		public EdgeType_token() : base((int) EdgeTypes.@token)
		{
		}
		public override String Name { get { return "token"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @token((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @token((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge release ***

	public interface Irelease : IEdge
	{
	}

	public sealed class @release : LGSPEdge, Irelease
	{
		private static int poolLevel = 0;
		private static @release[] pool = new @release[10];
		public @release(LGSPNode source, LGSPNode target)
			: base(EdgeType_release.typeVar, source, target)
		{
		}

		public static EdgeType_release TypeInstance { get { return EdgeType_release.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @release(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @release(@release oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_release.typeVar, newSource, newTarget)
		{
		}
		public static @release CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@release edge;
			if(poolLevel == 0)
				edge = new @release(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @release CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@release edge;
			if(poolLevel == 0)
				edge = new @release(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_release : EdgeType
	{
		public static EdgeType_release typeVar = new EdgeType_release();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, };
		public EdgeType_release() : base((int) EdgeTypes.@release)
		{
		}
		public override String Name { get { return "release"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @release((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @release((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge request ***

	public interface Irequest : IEdge
	{
	}

	public sealed class @request : LGSPEdge, Irequest
	{
		private static int poolLevel = 0;
		private static @request[] pool = new @request[10];
		public @request(LGSPNode source, LGSPNode target)
			: base(EdgeType_request.typeVar, source, target)
		{
		}

		public static EdgeType_request TypeInstance { get { return EdgeType_request.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @request(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @request(@request oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_request.typeVar, newSource, newTarget)
		{
		}
		public static @request CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@request edge;
			if(poolLevel == 0)
				edge = new @request(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @request CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@request edge;
			if(poolLevel == 0)
				edge = new @request(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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

	public sealed class EdgeType_request : EdgeType
	{
		public static EdgeType_request typeVar = new EdgeType_request();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, };
		public EdgeType_request() : base((int) EdgeTypes.@request)
		{
		}
		public override String Name { get { return "request"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @request((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override IEdge CreateEdgeWithCopyCommons(INode source, INode target, IEdge oldIEdge)
		{
			return new @request((LGSPNode) source, (LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class MutexEdgeModel : IEdgeModel
	{
		public MutexEdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new EdgeType[] {
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
			EdgeType_AEdge.typeVar.directSubGrGenTypes = EdgeType_AEdge.typeVar.directSubTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.superOrSameGrGenTypes = EdgeType_AEdge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_AEdge.typeVar.directSuperGrGenTypes = EdgeType_AEdge.typeVar.directSuperTypes = new EdgeType[] {
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_next.typeVar,
				EdgeType_blocked.typeVar,
				EdgeType_held_by.typeVar,
				EdgeType_token.typeVar,
				EdgeType_release.typeVar,
				EdgeType_request.typeVar,
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new EdgeType[] {
				EdgeType_next.typeVar,
				EdgeType_blocked.typeVar,
				EdgeType_held_by.typeVar,
				EdgeType_token.typeVar,
				EdgeType_release.typeVar,
				EdgeType_request.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_Edge.typeVar.directSuperGrGenTypes = EdgeType_Edge.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.subOrSameGrGenTypes = EdgeType_UEdge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_UEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSubGrGenTypes = EdgeType_UEdge.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_UEdge.typeVar.superOrSameGrGenTypes = EdgeType_UEdge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_UEdge.typeVar,
				EdgeType_AEdge.typeVar,
			};
			EdgeType_UEdge.typeVar.directSuperGrGenTypes = EdgeType_UEdge.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
			};
			EdgeType_next.typeVar.subOrSameGrGenTypes = EdgeType_next.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_next.typeVar,
			};
			EdgeType_next.typeVar.directSubGrGenTypes = EdgeType_next.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_next.typeVar.superOrSameGrGenTypes = EdgeType_next.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_next.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_next.typeVar.directSuperGrGenTypes = EdgeType_next.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_blocked.typeVar.subOrSameGrGenTypes = EdgeType_blocked.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_blocked.typeVar,
			};
			EdgeType_blocked.typeVar.directSubGrGenTypes = EdgeType_blocked.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_blocked.typeVar.superOrSameGrGenTypes = EdgeType_blocked.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_blocked.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_blocked.typeVar.directSuperGrGenTypes = EdgeType_blocked.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_held_by.typeVar.subOrSameGrGenTypes = EdgeType_held_by.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_held_by.typeVar,
			};
			EdgeType_held_by.typeVar.directSubGrGenTypes = EdgeType_held_by.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_held_by.typeVar.superOrSameGrGenTypes = EdgeType_held_by.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_held_by.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_held_by.typeVar.directSuperGrGenTypes = EdgeType_held_by.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_token.typeVar.subOrSameGrGenTypes = EdgeType_token.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_token.typeVar,
			};
			EdgeType_token.typeVar.directSubGrGenTypes = EdgeType_token.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_token.typeVar.superOrSameGrGenTypes = EdgeType_token.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_token.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_token.typeVar.directSuperGrGenTypes = EdgeType_token.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_release.typeVar.subOrSameGrGenTypes = EdgeType_release.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_release.typeVar,
			};
			EdgeType_release.typeVar.directSubGrGenTypes = EdgeType_release.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_release.typeVar.superOrSameGrGenTypes = EdgeType_release.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_release.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_release.typeVar.directSuperGrGenTypes = EdgeType_release.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_request.typeVar.subOrSameGrGenTypes = EdgeType_request.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_request.typeVar,
			};
			EdgeType_request.typeVar.directSubGrGenTypes = EdgeType_request.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_request.typeVar.superOrSameGrGenTypes = EdgeType_request.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_request.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_request.typeVar.directSuperGrGenTypes = EdgeType_request.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public EdgeType RootType { get { return EdgeType_AEdge.typeVar; } }
		GrGenType ITypeModel.RootType { get { return EdgeType_AEdge.typeVar; } }
		public EdgeType GetType(String name)
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
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
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
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
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
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class MutexGraphModel : IGraphModel
	{
		private MutexNodeModel nodeModel = new MutexNodeModel();
		private MutexEdgeModel edgeModel = new MutexEdgeModel();
		private ValidateInfo[] validateInfos = {
			new ValidateInfo(EdgeType_next.typeVar, NodeType_Process.typeVar, NodeType_Process.typeVar, 0, 1, 0, 1),
			new ValidateInfo(EdgeType_blocked.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 0, long.MaxValue, 0, long.MaxValue),
			new ValidateInfo(EdgeType_held_by.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new ValidateInfo(EdgeType_token.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new ValidateInfo(EdgeType_release.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new ValidateInfo(EdgeType_request.typeVar, NodeType_Process.typeVar, NodeType_Resource.typeVar, 0, long.MaxValue, 0, long.MaxValue),
		};

		public String ModelName { get { return "Mutex"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "b2c79abf46750619401de30166fff963"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class Mutex : LGSPGraph, IGraphModel
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

		public @Edge CreateEdgeEdge(LGSPNode source, LGSPNode target)
		{
			return @Edge.CreateEdge(this, source, target);
		}

		public @Edge CreateEdgeEdge(LGSPNode source, LGSPNode target, String varName)
		{
			return @Edge.CreateEdge(this, source, target, varName);
		}

		public @UEdge CreateEdgeUEdge(LGSPNode source, LGSPNode target)
		{
			return @UEdge.CreateEdge(this, source, target);
		}

		public @UEdge CreateEdgeUEdge(LGSPNode source, LGSPNode target, String varName)
		{
			return @UEdge.CreateEdge(this, source, target, varName);
		}

		public @next CreateEdgenext(LGSPNode source, LGSPNode target)
		{
			return @next.CreateEdge(this, source, target);
		}

		public @next CreateEdgenext(LGSPNode source, LGSPNode target, String varName)
		{
			return @next.CreateEdge(this, source, target, varName);
		}

		public @blocked CreateEdgeblocked(LGSPNode source, LGSPNode target)
		{
			return @blocked.CreateEdge(this, source, target);
		}

		public @blocked CreateEdgeblocked(LGSPNode source, LGSPNode target, String varName)
		{
			return @blocked.CreateEdge(this, source, target, varName);
		}

		public @held_by CreateEdgeheld_by(LGSPNode source, LGSPNode target)
		{
			return @held_by.CreateEdge(this, source, target);
		}

		public @held_by CreateEdgeheld_by(LGSPNode source, LGSPNode target, String varName)
		{
			return @held_by.CreateEdge(this, source, target, varName);
		}

		public @token CreateEdgetoken(LGSPNode source, LGSPNode target)
		{
			return @token.CreateEdge(this, source, target);
		}

		public @token CreateEdgetoken(LGSPNode source, LGSPNode target, String varName)
		{
			return @token.CreateEdge(this, source, target, varName);
		}

		public @release CreateEdgerelease(LGSPNode source, LGSPNode target)
		{
			return @release.CreateEdge(this, source, target);
		}

		public @release CreateEdgerelease(LGSPNode source, LGSPNode target, String varName)
		{
			return @release.CreateEdge(this, source, target, varName);
		}

		public @request CreateEdgerequest(LGSPNode source, LGSPNode target)
		{
			return @request.CreateEdge(this, source, target);
		}

		public @request CreateEdgerequest(LGSPNode source, LGSPNode target, String varName)
		{
			return @request.CreateEdge(this, source, target, varName);
		}

		private MutexNodeModel nodeModel = new MutexNodeModel();
		private MutexEdgeModel edgeModel = new MutexEdgeModel();
		private ValidateInfo[] validateInfos = {
			new ValidateInfo(EdgeType_next.typeVar, NodeType_Process.typeVar, NodeType_Process.typeVar, 0, 1, 0, 1),
			new ValidateInfo(EdgeType_blocked.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 0, long.MaxValue, 0, long.MaxValue),
			new ValidateInfo(EdgeType_held_by.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new ValidateInfo(EdgeType_token.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new ValidateInfo(EdgeType_release.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, 1, 0, long.MaxValue),
			new ValidateInfo(EdgeType_request.typeVar, NodeType_Process.typeVar, NodeType_Resource.typeVar, 0, long.MaxValue, 0, long.MaxValue),
		};

		public String ModelName { get { return "Mutex"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "b2c79abf46750619401de30166fff963"; } }
	}
}
