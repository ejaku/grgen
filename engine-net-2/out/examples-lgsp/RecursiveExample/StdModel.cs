// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\Recursive\Recursive.grg" on Wed Apr 23 23:46:29 CEST 2008

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

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

	public enum NodeTypes { @Node };

	// *** Node Node ***

	public interface INode_Node : INode
	{
	}

	public sealed class Node_Node : LGSPNode, INode_Node
	{
		private static int poolLevel = 0;
		private static Node_Node[] pool = new Node_Node[10];
		public Node_Node() : base(NodeType_Node.typeVar)
		{
		}
		public override INode Clone() { return new Node_Node(this); }

		private Node_Node(Node_Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
		public static Node_Node CreateNode(LGSPGraph graph)
		{
			Node_Node node;
			if(poolLevel == 0)
				node = new Node_Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~LGSPNode.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_Node CreateNode(LGSPGraph graph, String varName)
		{
			Node_Node node;
			if(poolLevel == 0)
				node = new Node_Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~LGSPNode.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, };
		public static bool[] isMyType = new bool[] { true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override String Name { get { return "Node"; } }
		public override INode CreateNode()
		{
			return new Node_Node();
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
			return new Node_Node();
		}

	}

	//
	// Node model
	//

	public sealed class StdNodeModel : INodeModel
	{
		public StdNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new NodeType[] {
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
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Node.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge };

	// *** Edge AEdge ***

	public interface IEdge_AEdge : IEdge
	{
	}

	public sealed class EdgeType_AEdge : EdgeType
	{
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override String Name { get { return "AEdge"; } }
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

	public interface IEdge_Edge : IEdge_AEdge
	{
	}

	public sealed class Edge_Edge : LGSPEdge, IEdge_Edge
	{
		private static int poolLevel = 0;
		private static Edge_Edge[] pool = new Edge_Edge[10];
		public Edge_Edge(LGSPNode source, LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_Edge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private Edge_Edge(Edge_Edge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public static Edge_Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_Edge edge;
			if(poolLevel == 0)
				edge = new Edge_Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~LGSPEdge.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static Edge_Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			Edge_Edge edge;
			if(poolLevel == 0)
				edge = new Edge_Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~LGSPEdge.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_Edge((LGSPNode) source, (LGSPNode) target);
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
			return new Edge_Edge((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge UEdge ***

	public interface IEdge_UEdge : IEdge_AEdge
	{
	}

	public sealed class Edge_UEdge : LGSPEdge, IEdge_UEdge
	{
		private static int poolLevel = 0;
		private static Edge_UEdge[] pool = new Edge_UEdge[10];
		public Edge_UEdge(LGSPNode source, LGSPNode target)
			: base(EdgeType_UEdge.typeVar, source, target)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_UEdge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private Edge_UEdge(Edge_UEdge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		public static Edge_UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_UEdge edge;
			if(poolLevel == 0)
				edge = new Edge_UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~LGSPEdge.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static Edge_UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			Edge_UEdge edge;
			if(poolLevel == 0)
				edge = new Edge_UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~LGSPEdge.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, true, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override String Name { get { return "UEdge"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_UEdge((LGSPNode) source, (LGSPNode) target);
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
			return new Edge_UEdge((LGSPNode) source, (LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class StdEdgeModel : IEdgeModel
	{
		public StdEdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
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
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new EdgeType[] {
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
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_AEdge),
			typeof(EdgeType_Edge),
			typeof(EdgeType_UEdge),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class StdGraphModel : IGraphModel
	{
		private StdNodeModel nodeModel = new StdNodeModel();
		private StdEdgeModel edgeModel = new StdEdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String ModelName { get { return "Std"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class Std : LGSPGraph, IGraphModel
	{
		public Std() : base(GetNextGraphName())
		{
			InitializeGraph(this);
		}

		public Node_Node CreateNode_Node()
		{
			return Node_Node.CreateNode(this);
		}

		public Node_Node CreateNode_Node(String varName)
		{
			return Node_Node.CreateNode(this, varName);
		}

		public Edge_Edge CreateEdge_Edge(LGSPNode source, LGSPNode target)
		{
			return Edge_Edge.CreateEdge(this, source, target);
		}

		public Edge_Edge CreateEdge_Edge(LGSPNode source, LGSPNode target, String varName)
		{
			return Edge_Edge.CreateEdge(this, source, target, varName);
		}

		public Edge_UEdge CreateEdge_UEdge(LGSPNode source, LGSPNode target)
		{
			return Edge_UEdge.CreateEdge(this, source, target);
		}

		public Edge_UEdge CreateEdge_UEdge(LGSPNode source, LGSPNode target, String varName)
		{
			return Edge_UEdge.CreateEdge(this, source, target, varName);
		}

		private StdNodeModel nodeModel = new StdNodeModel();
		private StdEdgeModel edgeModel = new StdEdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String ModelName { get { return "Std"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
	}
}
