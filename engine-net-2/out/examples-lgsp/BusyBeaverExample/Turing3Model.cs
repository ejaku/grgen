using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.models.Turing3
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

	public enum NodeTypes { @Node, @WriteValue, @BandPosition, @State };

	// *** Node Node ***

	public interface INode_Node : IAttributes
	{
	}

	public sealed class Node_Node : LGSPNode, INode_Node
	{
		private static int poolLevel = 0;
		private static Node_Node[] pool = new Node_Node[10];
		public Node_Node() : base(NodeType_Node.typeVar) { }
		private Node_Node(Node_Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
		public override INode Clone() { return new Node_Node(this); }
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
				node.hasVariables = false;
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
				node.hasVariables = false;
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
		public override INode CreateNode() { return new Node_Node(); }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			LGSPNode oldNode = (LGSPNode) oldINode;
			Node_Node newNode = new Node_Node();
			return newNode;
		}

	}

	// *** Node WriteValue ***

	public interface INode_WriteValue : INode_Node
	{
		int @value { get; set; }
	}

	public sealed class Node_WriteValue : LGSPNode, INode_WriteValue
	{
		private static int poolLevel = 0;
		private static Node_WriteValue[] pool = new Node_WriteValue[10];
		public Node_WriteValue() : base(NodeType_WriteValue.typeVar) { }
		private Node_WriteValue(Node_WriteValue oldElem) : base(NodeType_WriteValue.typeVar)
		{
			_value = oldElem._value;
		}
		public override INode Clone() { return new Node_WriteValue(this); }
		public static Node_WriteValue CreateNode(LGSPGraph graph)
		{
			Node_WriteValue node;
			if(poolLevel == 0)
				node = new Node_WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@value = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_WriteValue CreateNode(LGSPGraph graph, String varName)
		{
			Node_WriteValue node;
			if(poolLevel == 0)
				node = new Node_WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@value = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		private int _value;
		public int @value
		{
			get { return _value; }
			set { _value = value; }
		}

		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "value": return _value;
			}
			throw new NullReferenceException(
				"The node type \"WriteValue\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": _value = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"WriteValue\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@value = 0;
		}
	}

	public sealed class NodeType_WriteValue : NodeType
	{
		public static NodeType_WriteValue typeVar = new NodeType_WriteValue();
		public static bool[] isA = new bool[] { true, true, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, };
		public static AttributeType AttributeType_value;
		public NodeType_WriteValue() : base((int) NodeTypes.@WriteValue)
		{
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "WriteValue"; } }
		public override INode CreateNode() { return new Node_WriteValue(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_value;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "value" : return AttributeType_value;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			LGSPNode oldNode = (LGSPNode) oldINode;
			Node_WriteValue newNode = new Node_WriteValue();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@WriteValue:
					// copy attributes for: WriteValue
					{
						INode_WriteValue old = (INode_WriteValue) oldNode;
						newNode.value = old.value;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node BandPosition ***

	public interface INode_BandPosition : INode_Node
	{
		int @value { get; set; }
	}

	public sealed class Node_BandPosition : LGSPNode, INode_BandPosition
	{
		private static int poolLevel = 0;
		private static Node_BandPosition[] pool = new Node_BandPosition[10];
		public Node_BandPosition() : base(NodeType_BandPosition.typeVar) { }
		private Node_BandPosition(Node_BandPosition oldElem) : base(NodeType_BandPosition.typeVar)
		{
			_value = oldElem._value;
		}
		public override INode Clone() { return new Node_BandPosition(this); }
		public static Node_BandPosition CreateNode(LGSPGraph graph)
		{
			Node_BandPosition node;
			if(poolLevel == 0)
				node = new Node_BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@value = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_BandPosition CreateNode(LGSPGraph graph, String varName)
		{
			Node_BandPosition node;
			if(poolLevel == 0)
				node = new Node_BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@value = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		private int _value;
		public int @value
		{
			get { return _value; }
			set { _value = value; }
		}

		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "value": return _value;
			}
			throw new NullReferenceException(
				"The node type \"BandPosition\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": _value = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"BandPosition\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@value = 0;
		}
	}

	public sealed class NodeType_BandPosition : NodeType
	{
		public static NodeType_BandPosition typeVar = new NodeType_BandPosition();
		public static bool[] isA = new bool[] { true, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, };
		public static AttributeType AttributeType_value;
		public NodeType_BandPosition() : base((int) NodeTypes.@BandPosition)
		{
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "BandPosition"; } }
		public override INode CreateNode() { return new Node_BandPosition(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_value;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "value" : return AttributeType_value;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			LGSPNode oldNode = (LGSPNode) oldINode;
			Node_BandPosition newNode = new Node_BandPosition();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@BandPosition:
					// copy attributes for: BandPosition
					{
						INode_BandPosition old = (INode_BandPosition) oldNode;
						newNode.value = old.value;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node State ***

	public interface INode_State : INode_Node
	{
	}

	public sealed class Node_State : LGSPNode, INode_State
	{
		private static int poolLevel = 0;
		private static Node_State[] pool = new Node_State[10];
		public Node_State() : base(NodeType_State.typeVar) { }
		private Node_State(Node_State oldElem) : base(NodeType_State.typeVar)
		{
		}
		public override INode Clone() { return new Node_State(this); }
		public static Node_State CreateNode(LGSPGraph graph)
		{
			Node_State node;
			if(poolLevel == 0)
				node = new Node_State();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_State CreateNode(LGSPGraph graph, String varName)
		{
			Node_State node;
			if(poolLevel == 0)
				node = new Node_State();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
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
				"The node type \"State\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"State\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_State : NodeType
	{
		public static NodeType_State typeVar = new NodeType_State();
		public static bool[] isA = new bool[] { true, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, true, };
		public NodeType_State() : base((int) NodeTypes.@State)
		{
		}
		public override String Name { get { return "State"; } }
		public override INode CreateNode() { return new Node_State(); }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override INode CreateNodeWithCopyCommons(INode oldINode)
		{
			LGSPNode oldNode = (LGSPNode) oldINode;
			Node_State newNode = new Node_State();
			return newNode;
		}

	}

	//
	// Node model
	//

	public sealed class Turing3NodeModel : INodeModel
	{
		public Turing3NodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_WriteValue.typeVar,
				NodeType_BandPosition.typeVar,
				NodeType_State.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_WriteValue.typeVar.subOrSameGrGenTypes = NodeType_WriteValue.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_WriteValue.typeVar,
			};
			NodeType_WriteValue.typeVar.superOrSameGrGenTypes = NodeType_WriteValue.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_WriteValue.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_BandPosition.typeVar.subOrSameGrGenTypes = NodeType_BandPosition.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_BandPosition.typeVar,
			};
			NodeType_BandPosition.typeVar.superOrSameGrGenTypes = NodeType_BandPosition.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_BandPosition.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_State.typeVar.subOrSameGrGenTypes = NodeType_State.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_State.typeVar,
			};
			NodeType_State.typeVar.superOrSameGrGenTypes = NodeType_State.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_State.typeVar,
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
				case "WriteValue" : return NodeType_WriteValue.typeVar;
				case "BandPosition" : return NodeType_BandPosition.typeVar;
				case "State" : return NodeType_State.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_WriteValue.typeVar,
			NodeType_BandPosition.typeVar,
			NodeType_State.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_WriteValue),
			typeof(NodeType_BandPosition),
			typeof(NodeType_State),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			NodeType_WriteValue.AttributeType_value,
			NodeType_BandPosition.AttributeType_value,
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @readZero, @readOne, @moveRight, @right, @Edge, @moveLeft };

	// *** Edge readZero ***

	public interface IEdge_readZero : IEdge_Edge
	{
	}

	public sealed class Edge_readZero : LGSPEdge, IEdge_readZero
	{
		private static int poolLevel = 0;
		private static Edge_readZero[] pool = new Edge_readZero[10];
		public Edge_readZero(LGSPNode source, LGSPNode target)
			: base(EdgeType_readZero.typeVar, source, target) { }
		private Edge_readZero(Edge_readZero oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_readZero.typeVar, newSource, newTarget)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_readZero(this, (LGSPNode) newSource, (LGSPNode) newTarget); }
		public static Edge_readZero CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_readZero edge;
			if(poolLevel == 0)
				edge = new Edge_readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static Edge_readZero CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			Edge_readZero edge;
			if(poolLevel == 0)
				edge = new Edge_readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
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
				"The edge type \"readZero\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"readZero\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_readZero : EdgeType
	{
		public static EdgeType_readZero typeVar = new EdgeType_readZero();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { true, false, false, false, false, false, };
		public EdgeType_readZero() : base((int) EdgeTypes.@readZero)
		{
		}
		public override String Name { get { return "readZero"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_readZero((LGSPNode) source, (LGSPNode) target);
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
			LGSPEdge oldEdge = (LGSPEdge) oldIEdge;
			Edge_readZero newEdge = new Edge_readZero((LGSPNode) source, (LGSPNode) target);
			return newEdge;
		}

	}

	// *** Edge readOne ***

	public interface IEdge_readOne : IEdge_Edge
	{
	}

	public sealed class Edge_readOne : LGSPEdge, IEdge_readOne
	{
		private static int poolLevel = 0;
		private static Edge_readOne[] pool = new Edge_readOne[10];
		public Edge_readOne(LGSPNode source, LGSPNode target)
			: base(EdgeType_readOne.typeVar, source, target) { }
		private Edge_readOne(Edge_readOne oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_readOne.typeVar, newSource, newTarget)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_readOne(this, (LGSPNode) newSource, (LGSPNode) newTarget); }
		public static Edge_readOne CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_readOne edge;
			if(poolLevel == 0)
				edge = new Edge_readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static Edge_readOne CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			Edge_readOne edge;
			if(poolLevel == 0)
				edge = new Edge_readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
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
				"The edge type \"readOne\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"readOne\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_readOne : EdgeType
	{
		public static EdgeType_readOne typeVar = new EdgeType_readOne();
		public static bool[] isA = new bool[] { false, true, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, };
		public EdgeType_readOne() : base((int) EdgeTypes.@readOne)
		{
		}
		public override String Name { get { return "readOne"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_readOne((LGSPNode) source, (LGSPNode) target);
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
			LGSPEdge oldEdge = (LGSPEdge) oldIEdge;
			Edge_readOne newEdge = new Edge_readOne((LGSPNode) source, (LGSPNode) target);
			return newEdge;
		}

	}

	// *** Edge moveRight ***

	public interface IEdge_moveRight : IEdge_Edge
	{
	}

	public sealed class Edge_moveRight : LGSPEdge, IEdge_moveRight
	{
		private static int poolLevel = 0;
		private static Edge_moveRight[] pool = new Edge_moveRight[10];
		public Edge_moveRight(LGSPNode source, LGSPNode target)
			: base(EdgeType_moveRight.typeVar, source, target) { }
		private Edge_moveRight(Edge_moveRight oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_moveRight.typeVar, newSource, newTarget)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_moveRight(this, (LGSPNode) newSource, (LGSPNode) newTarget); }
		public static Edge_moveRight CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_moveRight edge;
			if(poolLevel == 0)
				edge = new Edge_moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static Edge_moveRight CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			Edge_moveRight edge;
			if(poolLevel == 0)
				edge = new Edge_moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
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
				"The edge type \"moveRight\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"moveRight\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_moveRight : EdgeType
	{
		public static EdgeType_moveRight typeVar = new EdgeType_moveRight();
		public static bool[] isA = new bool[] { false, false, true, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, };
		public EdgeType_moveRight() : base((int) EdgeTypes.@moveRight)
		{
		}
		public override String Name { get { return "moveRight"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_moveRight((LGSPNode) source, (LGSPNode) target);
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
			LGSPEdge oldEdge = (LGSPEdge) oldIEdge;
			Edge_moveRight newEdge = new Edge_moveRight((LGSPNode) source, (LGSPNode) target);
			return newEdge;
		}

	}

	// *** Edge right ***

	public interface IEdge_right : IEdge_Edge
	{
	}

	public sealed class Edge_right : LGSPEdge, IEdge_right
	{
		private static int poolLevel = 0;
		private static Edge_right[] pool = new Edge_right[10];
		public Edge_right(LGSPNode source, LGSPNode target)
			: base(EdgeType_right.typeVar, source, target) { }
		private Edge_right(Edge_right oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_right.typeVar, newSource, newTarget)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_right(this, (LGSPNode) newSource, (LGSPNode) newTarget); }
		public static Edge_right CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_right edge;
			if(poolLevel == 0)
				edge = new Edge_right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static Edge_right CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			Edge_right edge;
			if(poolLevel == 0)
				edge = new Edge_right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
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
				"The edge type \"right\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"right\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_right : EdgeType
	{
		public static EdgeType_right typeVar = new EdgeType_right();
		public static bool[] isA = new bool[] { false, false, false, true, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, };
		public EdgeType_right() : base((int) EdgeTypes.@right)
		{
		}
		public override String Name { get { return "right"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_right((LGSPNode) source, (LGSPNode) target);
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
			LGSPEdge oldEdge = (LGSPEdge) oldIEdge;
			Edge_right newEdge = new Edge_right((LGSPNode) source, (LGSPNode) target);
			return newEdge;
		}

	}

	// *** Edge Edge ***

	public interface IEdge_Edge : IAttributes
	{
	}

	public sealed class Edge_Edge : LGSPEdge, IEdge_Edge
	{
		private static int poolLevel = 0;
		private static Edge_Edge[] pool = new Edge_Edge[10];
		public Edge_Edge(LGSPNode source, LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target) { }
		private Edge_Edge(Edge_Edge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_Edge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }
		public static Edge_Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_Edge edge;
			if(poolLevel == 0)
				edge = new Edge_Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
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
				edge.hasVariables = false;
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
		public static bool[] isA = new bool[] { false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, };
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
			LGSPEdge oldEdge = (LGSPEdge) oldIEdge;
			Edge_Edge newEdge = new Edge_Edge((LGSPNode) source, (LGSPNode) target);
			return newEdge;
		}

	}

	// *** Edge moveLeft ***

	public interface IEdge_moveLeft : IEdge_Edge
	{
	}

	public sealed class Edge_moveLeft : LGSPEdge, IEdge_moveLeft
	{
		private static int poolLevel = 0;
		private static Edge_moveLeft[] pool = new Edge_moveLeft[10];
		public Edge_moveLeft(LGSPNode source, LGSPNode target)
			: base(EdgeType_moveLeft.typeVar, source, target) { }
		private Edge_moveLeft(Edge_moveLeft oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_moveLeft.typeVar, newSource, newTarget)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_moveLeft(this, (LGSPNode) newSource, (LGSPNode) newTarget); }
		public static Edge_moveLeft CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_moveLeft edge;
			if(poolLevel == 0)
				edge = new Edge_moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static Edge_moveLeft CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			Edge_moveLeft edge;
			if(poolLevel == 0)
				edge = new Edge_moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.hasVariables = false;
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
				"The edge type \"moveLeft\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"moveLeft\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class EdgeType_moveLeft : EdgeType
	{
		public static EdgeType_moveLeft typeVar = new EdgeType_moveLeft();
		public static bool[] isA = new bool[] { false, false, false, false, true, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, };
		public EdgeType_moveLeft() : base((int) EdgeTypes.@moveLeft)
		{
		}
		public override String Name { get { return "moveLeft"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_moveLeft((LGSPNode) source, (LGSPNode) target);
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
			LGSPEdge oldEdge = (LGSPEdge) oldIEdge;
			Edge_moveLeft newEdge = new Edge_moveLeft((LGSPNode) source, (LGSPNode) target);
			return newEdge;
		}

	}

	//
	// Edge model
	//

	public sealed class Turing3EdgeModel : IEdgeModel
	{
		public Turing3EdgeModel()
		{
			EdgeType_readZero.typeVar.subOrSameGrGenTypes = EdgeType_readZero.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_readZero.typeVar,
			};
			EdgeType_readZero.typeVar.superOrSameGrGenTypes = EdgeType_readZero.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_readZero.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_readOne.typeVar.subOrSameGrGenTypes = EdgeType_readOne.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_readOne.typeVar,
			};
			EdgeType_readOne.typeVar.superOrSameGrGenTypes = EdgeType_readOne.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_readOne.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveRight.typeVar.subOrSameGrGenTypes = EdgeType_moveRight.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_moveRight.typeVar,
			};
			EdgeType_moveRight.typeVar.superOrSameGrGenTypes = EdgeType_moveRight.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_moveRight.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_right.typeVar.subOrSameGrGenTypes = EdgeType_right.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_right.typeVar,
			};
			EdgeType_right.typeVar.superOrSameGrGenTypes = EdgeType_right.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_right.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_readZero.typeVar,
				EdgeType_readOne.typeVar,
				EdgeType_moveRight.typeVar,
				EdgeType_right.typeVar,
				EdgeType_moveLeft.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveLeft.typeVar.subOrSameGrGenTypes = EdgeType_moveLeft.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_moveLeft.typeVar,
			};
			EdgeType_moveLeft.typeVar.superOrSameGrGenTypes = EdgeType_moveLeft.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_moveLeft.typeVar,
				EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public EdgeType RootType { get { return EdgeType_Edge.typeVar; } }
		GrGenType ITypeModel.RootType { get { return EdgeType_Edge.typeVar; } }
		public EdgeType GetType(String name)
		{
			switch(name)
			{
				case "readZero" : return EdgeType_readZero.typeVar;
				case "readOne" : return EdgeType_readOne.typeVar;
				case "moveRight" : return EdgeType_moveRight.typeVar;
				case "right" : return EdgeType_right.typeVar;
				case "Edge" : return EdgeType_Edge.typeVar;
				case "moveLeft" : return EdgeType_moveLeft.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
			EdgeType_readZero.typeVar,
			EdgeType_readOne.typeVar,
			EdgeType_moveRight.typeVar,
			EdgeType_right.typeVar,
			EdgeType_Edge.typeVar,
			EdgeType_moveLeft.typeVar,
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_readZero),
			typeof(EdgeType_readOne),
			typeof(EdgeType_moveRight),
			typeof(EdgeType_right),
			typeof(EdgeType_Edge),
			typeof(EdgeType_moveLeft),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class Turing3GraphModel : IGraphModel
	{
		private Turing3NodeModel nodeModel = new Turing3NodeModel();
		private Turing3EdgeModel edgeModel = new Turing3EdgeModel();
		private ValidateInfo[] validateInfos = {
			new ValidateInfo(EdgeType_right.typeVar, NodeType_BandPosition.typeVar, NodeType_BandPosition.typeVar, 0, 1, 0, 1),
		};

		public String Name { get { return "Turing3"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "5a78f363d1b6a0cc5cea759830c3e6b1"; } }
	}
}
