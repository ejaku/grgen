using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

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

	public enum NodeTypes { @WriteValue, @State, @Node, @BandPosition };

	// *** Node WriteValue ***

	public interface INode_WriteValue : INode_Node
	{
		int @value { get; set; }
	}

	public sealed class Node_WriteValue : INode_WriteValue
	{
		public Object Clone() { return MemberwiseClone(); }
		private int _value;
		public int @value
		{
			get { return _value; }
			set { _value = value; }
		}

	}

	public sealed class NodeType_WriteValue : NodeType
	{
		public static NodeType_WriteValue typeVar = new NodeType_WriteValue();
		public static bool[] isA = new bool[] { true, false, true, false, };
		public static bool[] isMyType = new bool[] { true, false, false, false, };
		public static AttributeType AttributeType_value;
		public NodeType_WriteValue() : base((int) NodeTypes.@WriteValue)
		{
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "WriteValue"; } }
		public override INode CreateNode() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return new Node_WriteValue(); }
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
	}

	// *** Node State ***

	public interface INode_State : INode_Node
	{
	}

	public sealed class Node_State : INode_State
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_State : NodeType
	{
		public static NodeType_State typeVar = new NodeType_State();
		public static bool[] isA = new bool[] { false, true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, };
		public NodeType_State() : base((int) NodeTypes.@State)
		{
		}
		public override String Name { get { return "State"; } }
		public override INode CreateNode() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Node Node ***

	public interface INode_Node : IAttributes
	{
	}

	public sealed class Node_Node : INode_Node
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_Node : NodeType
	{
		public static NodeType_Node typeVar = new NodeType_Node();
		public static bool[] isA = new bool[] { false, false, true, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override String Name { get { return "Node"; } }
		public override INode CreateNode() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Node BandPosition ***

	public interface INode_BandPosition : INode_Node
	{
		int @value { get; set; }
	}

	public sealed class Node_BandPosition : INode_BandPosition
	{
		public Object Clone() { return MemberwiseClone(); }
		private int _value;
		public int @value
		{
			get { return _value; }
			set { _value = value; }
		}

	}

	public sealed class NodeType_BandPosition : NodeType
	{
		public static NodeType_BandPosition typeVar = new NodeType_BandPosition();
		public static bool[] isA = new bool[] { false, false, true, true, };
		public static bool[] isMyType = new bool[] { false, false, false, true, };
		public static AttributeType AttributeType_value;
		public NodeType_BandPosition() : base((int) NodeTypes.@BandPosition)
		{
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "BandPosition"; } }
		public override INode CreateNode() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return new Node_BandPosition(); }
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
	}

	//
	// Node model
	//

	public sealed class Turing3NodeModel : INodeModel
	{
		public Turing3NodeModel()
		{
			NodeType_WriteValue.typeVar.subOrSameGrGenTypes = NodeType_WriteValue.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_WriteValue.typeVar,
			};
			NodeType_WriteValue.typeVar.subOrSameGrGenTypes = NodeType_WriteValue.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_WriteValue.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_State.typeVar.subOrSameGrGenTypes = NodeType_State.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_State.typeVar,
			};
			NodeType_State.typeVar.subOrSameGrGenTypes = NodeType_State.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_State.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_WriteValue.typeVar,
				NodeType_State.typeVar,
				NodeType_BandPosition.typeVar,
			};
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_BandPosition.typeVar.subOrSameGrGenTypes = NodeType_BandPosition.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_BandPosition.typeVar,
			};
			NodeType_BandPosition.typeVar.subOrSameGrGenTypes = NodeType_BandPosition.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_BandPosition.typeVar,
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
				case "WriteValue" : return NodeType_WriteValue.typeVar;
				case "State" : return NodeType_State.typeVar;
				case "Node" : return NodeType_Node.typeVar;
				case "BandPosition" : return NodeType_BandPosition.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_WriteValue.typeVar,
			NodeType_State.typeVar,
			NodeType_Node.typeVar,
			NodeType_BandPosition.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_WriteValue),
			typeof(NodeType_State),
			typeof(NodeType_Node),
			typeof(NodeType_BandPosition),
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

	public enum EdgeTypes { @readOne, @Edge, @moveLeft, @right, @readZero, @moveRight };

	// *** Edge readOne ***

	public interface IEdge_readOne : IEdge_Edge
	{
	}

	public sealed class Edge_readOne : IEdge_readOne
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_readOne : EdgeType
	{
		public static EdgeType_readOne typeVar = new EdgeType_readOne();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, false, false, false, false, false, };
		public EdgeType_readOne() : base((int) EdgeTypes.@readOne)
		{
		}
		public override String Name { get { return "readOne"; } }
		public override IEdge CreateEdge() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge Edge ***

	public interface IEdge_Edge : IAttributes
	{
	}

	public sealed class Edge_Edge : IEdge_Edge
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_Edge : EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override IEdge CreateEdge() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge moveLeft ***

	public interface IEdge_moveLeft : IEdge_Edge
	{
	}

	public sealed class Edge_moveLeft : IEdge_moveLeft
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_moveLeft : EdgeType
	{
		public static EdgeType_moveLeft typeVar = new EdgeType_moveLeft();
		public static bool[] isA = new bool[] { false, true, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, };
		public EdgeType_moveLeft() : base((int) EdgeTypes.@moveLeft)
		{
		}
		public override String Name { get { return "moveLeft"; } }
		public override IEdge CreateEdge() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge right ***

	public interface IEdge_right : IEdge_Edge
	{
	}

	public sealed class Edge_right : IEdge_right
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_right : EdgeType
	{
		public static EdgeType_right typeVar = new EdgeType_right();
		public static bool[] isA = new bool[] { false, true, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, };
		public EdgeType_right() : base((int) EdgeTypes.@right)
		{
		}
		public override String Name { get { return "right"; } }
		public override IEdge CreateEdge() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge readZero ***

	public interface IEdge_readZero : IEdge_Edge
	{
	}

	public sealed class Edge_readZero : IEdge_readZero
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_readZero : EdgeType
	{
		public static EdgeType_readZero typeVar = new EdgeType_readZero();
		public static bool[] isA = new bool[] { false, true, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, };
		public EdgeType_readZero() : base((int) EdgeTypes.@readZero)
		{
		}
		public override String Name { get { return "readZero"; } }
		public override IEdge CreateEdge() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge moveRight ***

	public interface IEdge_moveRight : IEdge_Edge
	{
	}

	public sealed class Edge_moveRight : IEdge_moveRight
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_moveRight : EdgeType
	{
		public static EdgeType_moveRight typeVar = new EdgeType_moveRight();
		public static bool[] isA = new bool[] { false, true, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, };
		public EdgeType_moveRight() : base((int) EdgeTypes.@moveRight)
		{
		}
		public override String Name { get { return "moveRight"; } }
		public override IEdge CreateEdge() { throw new Exception("The method or operation is not implemented."); }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	//
	// Edge model
	//

	public sealed class Turing3EdgeModel : IEdgeModel
	{
		public Turing3EdgeModel()
		{
			EdgeType_readOne.typeVar.subOrSameGrGenTypes = EdgeType_readOne.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_readOne.typeVar,
			};
			EdgeType_readOne.typeVar.subOrSameGrGenTypes = EdgeType_readOne.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_readOne.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_readOne.typeVar,
				EdgeType_moveLeft.typeVar,
				EdgeType_right.typeVar,
				EdgeType_readZero.typeVar,
				EdgeType_moveRight.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveLeft.typeVar.subOrSameGrGenTypes = EdgeType_moveLeft.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_moveLeft.typeVar,
			};
			EdgeType_moveLeft.typeVar.subOrSameGrGenTypes = EdgeType_moveLeft.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_moveLeft.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_right.typeVar.subOrSameGrGenTypes = EdgeType_right.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_right.typeVar,
			};
			EdgeType_right.typeVar.subOrSameGrGenTypes = EdgeType_right.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_right.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_readZero.typeVar.subOrSameGrGenTypes = EdgeType_readZero.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_readZero.typeVar,
			};
			EdgeType_readZero.typeVar.subOrSameGrGenTypes = EdgeType_readZero.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_readZero.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveRight.typeVar.subOrSameGrGenTypes = EdgeType_moveRight.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_moveRight.typeVar,
			};
			EdgeType_moveRight.typeVar.subOrSameGrGenTypes = EdgeType_moveRight.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_moveRight.typeVar,
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
				case "readOne" : return EdgeType_readOne.typeVar;
				case "Edge" : return EdgeType_Edge.typeVar;
				case "moveLeft" : return EdgeType_moveLeft.typeVar;
				case "right" : return EdgeType_right.typeVar;
				case "readZero" : return EdgeType_readZero.typeVar;
				case "moveRight" : return EdgeType_moveRight.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
			EdgeType_readOne.typeVar,
			EdgeType_Edge.typeVar,
			EdgeType_moveLeft.typeVar,
			EdgeType_right.typeVar,
			EdgeType_readZero.typeVar,
			EdgeType_moveRight.typeVar,
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_readOne),
			typeof(EdgeType_Edge),
			typeof(EdgeType_moveLeft),
			typeof(EdgeType_right),
			typeof(EdgeType_readZero),
			typeof(EdgeType_moveRight),
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
