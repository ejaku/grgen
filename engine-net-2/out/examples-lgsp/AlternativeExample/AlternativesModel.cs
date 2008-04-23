// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\alternatives\Alternatives.grg" on Wed Apr 23 23:46:16 CEST 2008

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_Alternatives
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

	public enum NodeTypes { @Node, @A, @B, @C };

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
		public static bool[] isA = new bool[] { true, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, };
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

	// *** Node A ***

	public interface INode_A : INode_Node
	{
	}

	public sealed class Node_A : LGSPNode, INode_A
	{
		private static int poolLevel = 0;
		private static Node_A[] pool = new Node_A[10];
		public Node_A() : base(NodeType_A.typeVar)
		{
		}
		public override INode Clone() { return new Node_A(this); }

		private Node_A(Node_A oldElem) : base(NodeType_A.typeVar)
		{
		}
		public static Node_A CreateNode(LGSPGraph graph)
		{
			Node_A node;
			if(poolLevel == 0)
				node = new Node_A();
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

		public static Node_A CreateNode(LGSPGraph graph, String varName)
		{
			Node_A node;
			if(poolLevel == 0)
				node = new Node_A();
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
				"The node type \"A\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"A\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_A : NodeType
	{
		public static NodeType_A typeVar = new NodeType_A();
		public static bool[] isA = new bool[] { true, true, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, };
		public NodeType_A() : base((int) NodeTypes.@A)
		{
		}
		public override String Name { get { return "A"; } }
		public override INode CreateNode()
		{
			return new Node_A();
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
			return new Node_A();
		}

	}

	// *** Node B ***

	public interface INode_B : INode_Node
	{
	}

	public sealed class Node_B : LGSPNode, INode_B
	{
		private static int poolLevel = 0;
		private static Node_B[] pool = new Node_B[10];
		public Node_B() : base(NodeType_B.typeVar)
		{
		}
		public override INode Clone() { return new Node_B(this); }

		private Node_B(Node_B oldElem) : base(NodeType_B.typeVar)
		{
		}
		public static Node_B CreateNode(LGSPGraph graph)
		{
			Node_B node;
			if(poolLevel == 0)
				node = new Node_B();
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

		public static Node_B CreateNode(LGSPGraph graph, String varName)
		{
			Node_B node;
			if(poolLevel == 0)
				node = new Node_B();
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
				"The node type \"B\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"B\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_B : NodeType
	{
		public static NodeType_B typeVar = new NodeType_B();
		public static bool[] isA = new bool[] { true, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, };
		public NodeType_B() : base((int) NodeTypes.@B)
		{
		}
		public override String Name { get { return "B"; } }
		public override INode CreateNode()
		{
			return new Node_B();
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
			return new Node_B();
		}

	}

	// *** Node C ***

	public interface INode_C : INode_Node
	{
	}

	public sealed class Node_C : LGSPNode, INode_C
	{
		private static int poolLevel = 0;
		private static Node_C[] pool = new Node_C[10];
		public Node_C() : base(NodeType_C.typeVar)
		{
		}
		public override INode Clone() { return new Node_C(this); }

		private Node_C(Node_C oldElem) : base(NodeType_C.typeVar)
		{
		}
		public static Node_C CreateNode(LGSPGraph graph)
		{
			Node_C node;
			if(poolLevel == 0)
				node = new Node_C();
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

		public static Node_C CreateNode(LGSPGraph graph, String varName)
		{
			Node_C node;
			if(poolLevel == 0)
				node = new Node_C();
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
				"The node type \"C\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"C\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_C : NodeType
	{
		public static NodeType_C typeVar = new NodeType_C();
		public static bool[] isA = new bool[] { true, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, true, };
		public NodeType_C() : base((int) NodeTypes.@C)
		{
		}
		public override String Name { get { return "C"; } }
		public override INode CreateNode()
		{
			return new Node_C();
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
			return new Node_C();
		}

	}

	//
	// Node model
	//

	public sealed class AlternativesNodeModel : INodeModel
	{
		public AlternativesNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_A.typeVar,
				NodeType_B.typeVar,
				NodeType_C.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new NodeType[] {
				NodeType_A.typeVar,
				NodeType_B.typeVar,
				NodeType_C.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new NodeType[] {
			};
			NodeType_A.typeVar.subOrSameGrGenTypes = NodeType_A.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_A.typeVar,
			};
			NodeType_A.typeVar.directSubGrGenTypes = NodeType_A.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_A.typeVar.superOrSameGrGenTypes = NodeType_A.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_A.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_A.typeVar.directSuperGrGenTypes = NodeType_A.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_B.typeVar.subOrSameGrGenTypes = NodeType_B.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_B.typeVar,
			};
			NodeType_B.typeVar.directSubGrGenTypes = NodeType_B.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_B.typeVar.superOrSameGrGenTypes = NodeType_B.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_B.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_B.typeVar.directSuperGrGenTypes = NodeType_B.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_C.typeVar.subOrSameGrGenTypes = NodeType_C.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_C.typeVar,
			};
			NodeType_C.typeVar.directSubGrGenTypes = NodeType_C.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_C.typeVar.superOrSameGrGenTypes = NodeType_C.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_C.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_C.typeVar.directSuperGrGenTypes = NodeType_C.typeVar.directSuperTypes = new NodeType[] {
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
				case "A" : return NodeType_A.typeVar;
				case "B" : return NodeType_B.typeVar;
				case "C" : return NodeType_C.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_A.typeVar,
			NodeType_B.typeVar,
			NodeType_C.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_A),
			typeof(NodeType_B),
			typeof(NodeType_C),
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

	public sealed class AlternativesEdgeModel : IEdgeModel
	{
		public AlternativesEdgeModel()
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

	public sealed class AlternativesGraphModel : IGraphModel
	{
		private AlternativesNodeModel nodeModel = new AlternativesNodeModel();
		private AlternativesEdgeModel edgeModel = new AlternativesEdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String ModelName { get { return "Alternatives"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "9318fc8b892e7676373a2a9f05e2f491"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class Alternatives : LGSPGraph, IGraphModel
	{
		public Alternatives() : base(GetNextGraphName())
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

		public Node_A CreateNode_A()
		{
			return Node_A.CreateNode(this);
		}

		public Node_A CreateNode_A(String varName)
		{
			return Node_A.CreateNode(this, varName);
		}

		public Node_B CreateNode_B()
		{
			return Node_B.CreateNode(this);
		}

		public Node_B CreateNode_B(String varName)
		{
			return Node_B.CreateNode(this, varName);
		}

		public Node_C CreateNode_C()
		{
			return Node_C.CreateNode(this);
		}

		public Node_C CreateNode_C(String varName)
		{
			return Node_C.CreateNode(this, varName);
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

		private AlternativesNodeModel nodeModel = new AlternativesNodeModel();
		private AlternativesEdgeModel edgeModel = new AlternativesEdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String ModelName { get { return "Alternatives"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "9318fc8b892e7676373a2a9f05e2f491"; } }
	}
}
