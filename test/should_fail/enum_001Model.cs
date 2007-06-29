using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.models.enum_001
{
	//
	// Enums
	//

	public enum ENUM_te { a = 0, b = 1, c = 42, };

	public class Enums
	{
		public static EnumAttributeType te = new EnumAttributeType("ENUM_te", new EnumMember[] {
			new EnumMember(0, "a"),
			new EnumMember(1, "b"),
			new EnumMember(42, "c"),
		});
	}

	//
	// Node types
	//

	public enum NodeTypes { Node, A };

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
			typeID   = (int) NodeTypes.Node;
			isA      = new bool[] { true, false, };
			isMyType      = new bool[] { true, true, };
		}
		public override String Name { get { return "Node"; } }
		public override bool IsNodeType { get { return true; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Node A ***

	public interface INode_A : INode_Node
	{
		ENUM_te e { get; set; }
	}

	public sealed class Node_A : INode_A
	{
		public Object Clone() { return MemberwiseClone(); }
		private ENUM_te _e;
		public ENUM_te e
		{
			get { return _e; }
			set { _e = value; }
		}

	}

	public sealed class NodeType_A : TypeFramework<NodeType_A, Node_A, INode_A>
	{
		public static AttributeType AttributeType_e;
		public NodeType_A()
		{
			typeID   = (int) NodeTypes.A;
			isA      = new bool[] { true, true, };
			isMyType      = new bool[] { false, true, };
			AttributeType_e = new AttributeType("e", this, AttributeKind.EnumAttr, Enums.te);
		}
		public override String Name { get { return "A"; } }
		public override bool IsNodeType { get { return true; } }
		public override IAttributes CreateAttributes() { return new Node_A(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_e;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "e" : return AttributeType_e;
			}
			return null;
		}
	}

	//
	// Node model
	//

	public sealed class enum_001NodeModel : ITypeModel
	{
		public enum_001NodeModel()
		{
			NodeType_Node.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_Node.typeVar,
				NodeType_A.typeVar,
			};
			NodeType_Node.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_Node.typeVar,
			};
			NodeType_A.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_A.typeVar,
			};
			NodeType_A.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_A.typeVar,
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
				case "A" : return NodeType_A.typeVar;
			}
			return null;
		}
		private ITypeFramework[] types = {
			NodeType_Node.typeVar,
			NodeType_A.typeVar,
		};
		public IType[] Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_A),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			NodeType_A.AttributeType_e,
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { Edge, B };

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
			typeID   = (int) EdgeTypes.Edge;
			isA      = new bool[] { true, false, };
			isMyType      = new bool[] { true, true, };
		}
		public override String Name { get { return "Edge"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge B ***

	public interface IEdge_B : IEdge_Edge
	{
		ENUM_te e { get; set; }
		int i { get; set; }
	}

	public sealed class Edge_B : IEdge_B
	{
		public Object Clone() { return MemberwiseClone(); }
		private int _i;
		public int i
		{
			get { return _i; }
			set { _i = value; }
		}

		private ENUM_te _e;
		public ENUM_te e
		{
			get { return _e; }
			set { _e = value; }
		}

	}

	public sealed class EdgeType_B : TypeFramework<EdgeType_B, Edge_B, IEdge_B>
	{
		public static AttributeType AttributeType_e;
		public static AttributeType AttributeType_i;
		public EdgeType_B()
		{
			typeID   = (int) EdgeTypes.B;
			isA      = new bool[] { true, true, };
			isMyType      = new bool[] { false, true, };
			AttributeType_e = new AttributeType("e", this, AttributeKind.EnumAttr, Enums.te);
			AttributeType_i = new AttributeType("i", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "B"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return new Edge_B(); }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_i;
				yield return AttributeType_e;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "i" : return AttributeType_i;
				case "e" : return AttributeType_e;
			}
			return null;
		}
	}

	//
	// Edge model
	//

	public sealed class enum_001EdgeModel : ITypeModel
	{
		public enum_001EdgeModel()
		{
			EdgeType_Edge.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_Edge.typeVar,
				EdgeType_B.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_B.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_B.typeVar,
			};
			EdgeType_B.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_B.typeVar,
				EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public IType RootType { get { return EdgeType_Edge.typeVar; } }
		public IType GetType(String name)
		{
			switch(name)
			{
				case "Edge" : return EdgeType_Edge.typeVar;
				case "B" : return EdgeType_B.typeVar;
			}
			return null;
		}
		private ITypeFramework[] types = {
			EdgeType_Edge.typeVar,
			EdgeType_B.typeVar,
		};
		public IType[] Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_Edge),
			typeof(EdgeType_B),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			EdgeType_B.AttributeType_e,
			EdgeType_B.AttributeType_i,
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class enum_001GraphModel : IGraphModel
	{
		private enum_001NodeModel nodeModel = new enum_001NodeModel();
		private enum_001EdgeModel edgeModel = new enum_001EdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String Name { get { return "enum_001"; } }
		public ITypeModel NodeModel { get { return nodeModel; } }
		public ITypeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "f07c629a9cdb59ab74dbe081b738eaa2"; } }
	}
}
