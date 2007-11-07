using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.models.MutexPimped
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

	public enum NodeTypes { @Resource, @Process, @Node };

	// *** Node Resource ***

	public interface INode_Resource : INode_Node
	{
	}

	public sealed class Node_Resource : INode_Resource
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_Resource : NodeType
	{
		public static NodeType_Resource typeVar = new NodeType_Resource();
		public static bool[] isA = new bool[] { true, false, true, };
		public static bool[] isMyType = new bool[] { true, false, false, };
		public NodeType_Resource() : base((int) NodeTypes.@Resource)
		{
		}
		public override String Name { get { return "Resource"; } }
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

	// *** Node Process ***

	public interface INode_Process : INode_Node
	{
	}

	public sealed class Node_Process : INode_Process
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_Process : NodeType
	{
		public static NodeType_Process typeVar = new NodeType_Process();
		public static bool[] isA = new bool[] { false, true, true, };
		public static bool[] isMyType = new bool[] { false, true, false, };
		public NodeType_Process() : base((int) NodeTypes.@Process)
		{
		}
		public override String Name { get { return "Process"; } }
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
		public static bool[] isA = new bool[] { false, false, true, };
		public static bool[] isMyType = new bool[] { true, true, true, };
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

	//
	// Node model
	//

	public sealed class MutexPimpedNodeModel : INodeModel
	{
		public MutexPimpedNodeModel()
		{
			NodeType_Resource.typeVar.subOrSameGrGenTypes = NodeType_Resource.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Resource.typeVar,
			};
			NodeType_Resource.typeVar.subOrSameGrGenTypes = NodeType_Resource.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Resource.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Process.typeVar.subOrSameGrGenTypes = NodeType_Process.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Process.typeVar,
			};
			NodeType_Process.typeVar.subOrSameGrGenTypes = NodeType_Process.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Process.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_Resource.typeVar,
				NodeType_Process.typeVar,
			};
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
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
				case "Resource" : return NodeType_Resource.typeVar;
				case "Process" : return NodeType_Process.typeVar;
				case "Node" : return NodeType_Node.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Resource.typeVar,
			NodeType_Process.typeVar,
			NodeType_Node.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Resource),
			typeof(NodeType_Process),
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

	public enum EdgeTypes { @release, @Edge, @blocked, @token, @held_by, @request, @next };

	// *** Edge release ***

	public interface IEdge_release : IEdge_Edge
	{
	}

	public sealed class Edge_release : IEdge_release
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_release : EdgeType
	{
		public static EdgeType_release typeVar = new EdgeType_release();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, false, false, false, false, false, false, };
		public EdgeType_release() : base((int) EdgeTypes.@release)
		{
		}
		public override String Name { get { return "release"; } }
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
		public static bool[] isA = new bool[] { false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, };
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

	// *** Edge blocked ***

	public interface IEdge_blocked : IEdge_Edge
	{
	}

	public sealed class Edge_blocked : IEdge_blocked
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_blocked : EdgeType
	{
		public static EdgeType_blocked typeVar = new EdgeType_blocked();
		public static bool[] isA = new bool[] { false, true, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, };
		public EdgeType_blocked() : base((int) EdgeTypes.@blocked)
		{
		}
		public override String Name { get { return "blocked"; } }
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

	// *** Edge token ***

	public interface IEdge_token : IEdge_Edge
	{
	}

	public sealed class Edge_token : IEdge_token
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_token : EdgeType
	{
		public static EdgeType_token typeVar = new EdgeType_token();
		public static bool[] isA = new bool[] { false, true, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, };
		public EdgeType_token() : base((int) EdgeTypes.@token)
		{
		}
		public override String Name { get { return "token"; } }
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

	// *** Edge held_by ***

	public interface IEdge_held_by : IEdge_Edge
	{
	}

	public sealed class Edge_held_by : IEdge_held_by
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_held_by : EdgeType
	{
		public static EdgeType_held_by typeVar = new EdgeType_held_by();
		public static bool[] isA = new bool[] { false, true, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, };
		public EdgeType_held_by() : base((int) EdgeTypes.@held_by)
		{
		}
		public override String Name { get { return "held_by"; } }
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

	// *** Edge request ***

	public interface IEdge_request : IEdge_Edge
	{
	}

	public sealed class Edge_request : IEdge_request
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_request : EdgeType
	{
		public static EdgeType_request typeVar = new EdgeType_request();
		public static bool[] isA = new bool[] { false, true, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, };
		public EdgeType_request() : base((int) EdgeTypes.@request)
		{
		}
		public override String Name { get { return "request"; } }
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

	// *** Edge next ***

	public interface IEdge_next : IEdge_Edge
	{
	}

	public sealed class Edge_next : IEdge_next
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_next : EdgeType
	{
		public static EdgeType_next typeVar = new EdgeType_next();
		public static bool[] isA = new bool[] { false, true, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, };
		public EdgeType_next() : base((int) EdgeTypes.@next)
		{
		}
		public override String Name { get { return "next"; } }
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

	public sealed class MutexPimpedEdgeModel : IEdgeModel
	{
		public MutexPimpedEdgeModel()
		{
			EdgeType_release.typeVar.subOrSameGrGenTypes = EdgeType_release.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_release.typeVar,
			};
			EdgeType_release.typeVar.subOrSameGrGenTypes = EdgeType_release.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_release.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_release.typeVar,
				EdgeType_blocked.typeVar,
				EdgeType_token.typeVar,
				EdgeType_held_by.typeVar,
				EdgeType_request.typeVar,
				EdgeType_next.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_blocked.typeVar.subOrSameGrGenTypes = EdgeType_blocked.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_blocked.typeVar,
			};
			EdgeType_blocked.typeVar.subOrSameGrGenTypes = EdgeType_blocked.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_blocked.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_token.typeVar.subOrSameGrGenTypes = EdgeType_token.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_token.typeVar,
			};
			EdgeType_token.typeVar.subOrSameGrGenTypes = EdgeType_token.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_token.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_held_by.typeVar.subOrSameGrGenTypes = EdgeType_held_by.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_held_by.typeVar,
			};
			EdgeType_held_by.typeVar.subOrSameGrGenTypes = EdgeType_held_by.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_held_by.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_request.typeVar.subOrSameGrGenTypes = EdgeType_request.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_request.typeVar,
			};
			EdgeType_request.typeVar.subOrSameGrGenTypes = EdgeType_request.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_request.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_next.typeVar.subOrSameGrGenTypes = EdgeType_next.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_next.typeVar,
			};
			EdgeType_next.typeVar.subOrSameGrGenTypes = EdgeType_next.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_next.typeVar,
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
				case "release" : return EdgeType_release.typeVar;
				case "Edge" : return EdgeType_Edge.typeVar;
				case "blocked" : return EdgeType_blocked.typeVar;
				case "token" : return EdgeType_token.typeVar;
				case "held_by" : return EdgeType_held_by.typeVar;
				case "request" : return EdgeType_request.typeVar;
				case "next" : return EdgeType_next.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
			EdgeType_release.typeVar,
			EdgeType_Edge.typeVar,
			EdgeType_blocked.typeVar,
			EdgeType_token.typeVar,
			EdgeType_held_by.typeVar,
			EdgeType_request.typeVar,
			EdgeType_next.typeVar,
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_release),
			typeof(EdgeType_Edge),
			typeof(EdgeType_blocked),
			typeof(EdgeType_token),
			typeof(EdgeType_held_by),
			typeof(EdgeType_request),
			typeof(EdgeType_next),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class MutexPimpedGraphModel : IGraphModel
	{
		private MutexPimpedNodeModel nodeModel = new MutexPimpedNodeModel();
		private MutexPimpedEdgeModel edgeModel = new MutexPimpedEdgeModel();
		private ValidateInfo[] validateInfos = {
			new ValidateInfo(EdgeType_release.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_blocked.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 0, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_token.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_held_by.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_request.typeVar, NodeType_Process.typeVar, NodeType_Resource.typeVar, 0, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_next.typeVar, NodeType_Process.typeVar, NodeType_Process.typeVar, 0, 1, 0, 1),
		};

		public String Name { get { return "MutexPimped"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "0ecfadd9bae7c1bee09ddc57f323923f"; } }
	}
}
