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

	public enum NodeTypes { @Node, @WriteValue, @BandPosition, @State };

	// *** Node Node ***

	public interface INode_Node : IAttributes
	{
	}

	public sealed class Node_Node : INode_Node
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_Node : TypeFramework<NodeType_Node, Node_Node, INode_Node>
	{
		public NodeType_Node()
		{
			typeID   = (int) NodeTypes.@Node;
			isA      = new bool[] { true, false, false, false, };
			isMyType      = new bool[] { true, true, true, true, };
		}
		public override String Name { get { return "Node"; } }
		public override bool IsNodeType { get { return true; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

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

	public sealed class NodeType_WriteValue : TypeFramework<NodeType_WriteValue, Node_WriteValue, INode_WriteValue>
	{
		public static AttributeType AttributeType_value;
		public NodeType_WriteValue()
		{
			typeID   = (int) NodeTypes.@WriteValue;
			isA      = new bool[] { true, true, false, false, };
			isMyType      = new bool[] { false, true, false, false, };
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "WriteValue"; } }
		public override bool IsNodeType { get { return true; } }
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

	public sealed class NodeType_BandPosition : TypeFramework<NodeType_BandPosition, Node_BandPosition, INode_BandPosition>
	{
		public static AttributeType AttributeType_value;
		public NodeType_BandPosition()
		{
			typeID   = (int) NodeTypes.@BandPosition;
			isA      = new bool[] { true, false, true, false, };
			isMyType      = new bool[] { false, false, true, false, };
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "BandPosition"; } }
		public override bool IsNodeType { get { return true; } }
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
	}

	// *** Node State ***

	public interface INode_State : INode_Node
	{
	}

	public sealed class Node_State : INode_State
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_State : TypeFramework<NodeType_State, Node_State, INode_State>
	{
		public NodeType_State()
		{
			typeID   = (int) NodeTypes.@State;
			isA      = new bool[] { true, false, false, true, };
			isMyType      = new bool[] { false, false, false, true, };
		}
		public override String Name { get { return "State"; } }
		public override bool IsNodeType { get { return true; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	//
	// Node model
	//

	public sealed class Turing3NodeModel : ITypeModel
	{
		public Turing3NodeModel()
		{
			NodeType_Node.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_Node.typeVar,
				NodeType_WriteValue.typeVar,
				NodeType_BandPosition.typeVar,
				NodeType_State.typeVar,
			};
			NodeType_Node.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_Node.typeVar,
			};
			NodeType_WriteValue.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_WriteValue.typeVar,
			};
			NodeType_WriteValue.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_WriteValue.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_BandPosition.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_BandPosition.typeVar,
			};
			NodeType_BandPosition.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_BandPosition.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_State.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_State.typeVar,
			};
			NodeType_State.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_State.typeVar,
				NodeType_Node.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public IType RootType { get { return NodeType_Node.typeVar; } }
		public IType GetType(String name)
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
		private ITypeFramework[] types = {
			NodeType_Node.typeVar,
			NodeType_WriteValue.typeVar,
			NodeType_BandPosition.typeVar,
			NodeType_State.typeVar,
		};
		public IType[] Types { get { return types; } }
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

	public sealed class Edge_readZero : IEdge_readZero
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_readZero : TypeFramework<EdgeType_readZero, Edge_readZero, IEdge_readZero>
	{
		public EdgeType_readZero()
		{
			typeID   = (int) EdgeTypes.@readZero;
			isA      = new bool[] { true, false, false, false, true, false, };
			isMyType      = new bool[] { true, false, false, false, false, false, };
		}
		public override String Name { get { return "readZero"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge readOne ***

	public interface IEdge_readOne : IEdge_Edge
	{
	}

	public sealed class Edge_readOne : IEdge_readOne
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_readOne : TypeFramework<EdgeType_readOne, Edge_readOne, IEdge_readOne>
	{
		public EdgeType_readOne()
		{
			typeID   = (int) EdgeTypes.@readOne;
			isA      = new bool[] { false, true, false, false, true, false, };
			isMyType      = new bool[] { false, true, false, false, false, false, };
		}
		public override String Name { get { return "readOne"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge moveRight ***

	public interface IEdge_moveRight : IEdge_Edge
	{
	}

	public sealed class Edge_moveRight : IEdge_moveRight
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_moveRight : TypeFramework<EdgeType_moveRight, Edge_moveRight, IEdge_moveRight>
	{
		public EdgeType_moveRight()
		{
			typeID   = (int) EdgeTypes.@moveRight;
			isA      = new bool[] { false, false, true, false, true, false, };
			isMyType      = new bool[] { false, false, true, false, false, false, };
		}
		public override String Name { get { return "moveRight"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge right ***

	public interface IEdge_right : IEdge_Edge
	{
	}

	public sealed class Edge_right : IEdge_right
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_right : TypeFramework<EdgeType_right, Edge_right, IEdge_right>
	{
		public EdgeType_right()
		{
			typeID   = (int) EdgeTypes.@right;
			isA      = new bool[] { false, false, false, true, true, false, };
			isMyType      = new bool[] { false, false, false, true, false, false, };
		}
		public override String Name { get { return "right"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge Edge ***

	public interface IEdge_Edge : IAttributes
	{
	}

	public sealed class Edge_Edge : IEdge_Edge
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_Edge : TypeFramework<EdgeType_Edge, Edge_Edge, IEdge_Edge>
	{
		public EdgeType_Edge()
		{
			typeID   = (int) EdgeTypes.@Edge;
			isA      = new bool[] { false, false, false, false, true, false, };
			isMyType      = new bool[] { true, true, true, true, true, true, };
		}
		public override String Name { get { return "Edge"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge moveLeft ***

	public interface IEdge_moveLeft : IEdge_Edge
	{
	}

	public sealed class Edge_moveLeft : IEdge_moveLeft
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_moveLeft : TypeFramework<EdgeType_moveLeft, Edge_moveLeft, IEdge_moveLeft>
	{
		public EdgeType_moveLeft()
		{
			typeID   = (int) EdgeTypes.@moveLeft;
			isA      = new bool[] { false, false, false, false, true, true, };
			isMyType      = new bool[] { false, false, false, false, false, true, };
		}
		public override String Name { get { return "moveLeft"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	//
	// Edge model
	//

	public sealed class Turing3EdgeModel : ITypeModel
	{
		public Turing3EdgeModel()
		{
			EdgeType_readZero.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_readZero.typeVar,
			};
			EdgeType_readZero.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_readZero.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_readOne.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_readOne.typeVar,
			};
			EdgeType_readOne.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_readOne.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveRight.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_moveRight.typeVar,
			};
			EdgeType_moveRight.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_moveRight.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_right.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_right.typeVar,
			};
			EdgeType_right.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_right.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_Edge.typeVar,
				EdgeType_readZero.typeVar,
				EdgeType_readOne.typeVar,
				EdgeType_moveRight.typeVar,
				EdgeType_right.typeVar,
				EdgeType_moveLeft.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveLeft.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_moveLeft.typeVar,
			};
			EdgeType_moveLeft.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_moveLeft.typeVar,
				EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public IType RootType { get { return EdgeType_Edge.typeVar; } }
		public IType GetType(String name)
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
		private ITypeFramework[] types = {
			EdgeType_readZero.typeVar,
			EdgeType_readOne.typeVar,
			EdgeType_moveRight.typeVar,
			EdgeType_right.typeVar,
			EdgeType_Edge.typeVar,
			EdgeType_moveLeft.typeVar,
		};
		public IType[] Types { get { return types; } }
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
		public ITypeModel NodeModel { get { return nodeModel; } }
		public ITypeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "5a78f363d1b6a0cc5cea759830c3e6b1"; } }
	}
}
