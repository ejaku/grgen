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

	public enum NodeTypes { @Resource, @Node, @Process };

	// *** Node Resource ***

	public interface INode_Resource : INode_Node
	{
	}

	public sealed class Node_Resource : INode_Resource
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_Resource : TypeFramework<NodeType_Resource, Node_Resource, INode_Resource>
	{
		public NodeType_Resource()
		{
			typeID   = (int) NodeTypes.@Resource;
			isA      = new bool[] { true, true, false, };
			isMyType      = new bool[] { true, false, false, };
		}
		public override String Name { get { return "Resource"; } }
		public override bool IsNodeType { get { return true; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

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
			isA      = new bool[] { false, true, false, };
			isMyType      = new bool[] { true, true, true, };
		}
		public override String Name { get { return "Node"; } }
		public override bool IsNodeType { get { return true; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Node Process ***

	public interface INode_Process : INode_Node
	{
	}

	public sealed class Node_Process : INode_Process
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class NodeType_Process : TypeFramework<NodeType_Process, Node_Process, INode_Process>
	{
		public NodeType_Process()
		{
			typeID   = (int) NodeTypes.@Process;
			isA      = new bool[] { false, true, true, };
			isMyType      = new bool[] { false, false, true, };
		}
		public override String Name { get { return "Process"; } }
		public override bool IsNodeType { get { return true; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	//
	// Node model
	//

	public sealed class MutexPimpedNodeModel : ITypeModel
	{
		public MutexPimpedNodeModel()
		{
			NodeType_Resource.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_Resource.typeVar,
			};
			NodeType_Resource.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_Resource.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_Node.typeVar,
				NodeType_Resource.typeVar,
				NodeType_Process.typeVar,
			};
			NodeType_Node.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_Node.typeVar,
			};
			NodeType_Process.typeVar.subOrSameTypes = new ITypeFramework[] {
				NodeType_Process.typeVar,
			};
			NodeType_Process.typeVar.superOrSameTypes = new ITypeFramework[] {
				NodeType_Process.typeVar,
				NodeType_Node.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public IType RootType { get { return NodeType_Node.typeVar; } }
		public IType GetType(String name)
		{
			switch(name)
			{
				case "Resource" : return NodeType_Resource.typeVar;
				case "Node" : return NodeType_Node.typeVar;
				case "Process" : return NodeType_Process.typeVar;
			}
			return null;
		}
		private ITypeFramework[] types = {
			NodeType_Resource.typeVar,
			NodeType_Node.typeVar,
			NodeType_Process.typeVar,
		};
		public IType[] Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Resource),
			typeof(NodeType_Node),
			typeof(NodeType_Process),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @request, @held_by, @release, @blocked, @next, @token, @Edge };

	// *** Edge request ***

	public interface IEdge_request : IEdge_Edge
	{
	}

	public sealed class Edge_request : IEdge_request
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_request : TypeFramework<EdgeType_request, Edge_request, IEdge_request>
	{
		public EdgeType_request()
		{
			typeID   = (int) EdgeTypes.@request;
			isA      = new bool[] { true, false, false, false, false, false, true, };
			isMyType      = new bool[] { true, false, false, false, false, false, false, };
		}
		public override String Name { get { return "request"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge held_by ***

	public interface IEdge_held_by : IEdge_Edge
	{
	}

	public sealed class Edge_held_by : IEdge_held_by
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_held_by : TypeFramework<EdgeType_held_by, Edge_held_by, IEdge_held_by>
	{
		public EdgeType_held_by()
		{
			typeID   = (int) EdgeTypes.@held_by;
			isA      = new bool[] { false, true, false, false, false, false, true, };
			isMyType      = new bool[] { false, true, false, false, false, false, false, };
		}
		public override String Name { get { return "held_by"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge release ***

	public interface IEdge_release : IEdge_Edge
	{
	}

	public sealed class Edge_release : IEdge_release
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_release : TypeFramework<EdgeType_release, Edge_release, IEdge_release>
	{
		public EdgeType_release()
		{
			typeID   = (int) EdgeTypes.@release;
			isA      = new bool[] { false, false, true, false, false, false, true, };
			isMyType      = new bool[] { false, false, true, false, false, false, false, };
		}
		public override String Name { get { return "release"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge blocked ***

	public interface IEdge_blocked : IEdge_Edge
	{
	}

	public sealed class Edge_blocked : IEdge_blocked
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_blocked : TypeFramework<EdgeType_blocked, Edge_blocked, IEdge_blocked>
	{
		public EdgeType_blocked()
		{
			typeID   = (int) EdgeTypes.@blocked;
			isA      = new bool[] { false, false, false, true, false, false, true, };
			isMyType      = new bool[] { false, false, false, true, false, false, false, };
		}
		public override String Name { get { return "blocked"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge next ***

	public interface IEdge_next : IEdge_Edge
	{
	}

	public sealed class Edge_next : IEdge_next
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_next : TypeFramework<EdgeType_next, Edge_next, IEdge_next>
	{
		public EdgeType_next()
		{
			typeID   = (int) EdgeTypes.@next;
			isA      = new bool[] { false, false, false, false, true, false, true, };
			isMyType      = new bool[] { false, false, false, false, true, false, false, };
		}
		public override String Name { get { return "next"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	// *** Edge token ***

	public interface IEdge_token : IEdge_Edge
	{
	}

	public sealed class Edge_token : IEdge_token
	{
		public Object Clone() { return MemberwiseClone(); }
	}

	public sealed class EdgeType_token : TypeFramework<EdgeType_token, Edge_token, IEdge_token>
	{
		public EdgeType_token()
		{
			typeID   = (int) EdgeTypes.@token;
			isA      = new bool[] { false, false, false, false, false, true, true, };
			isMyType      = new bool[] { false, false, false, false, false, true, false, };
		}
		public override String Name { get { return "token"; } }
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
			isA      = new bool[] { false, false, false, false, false, false, true, };
			isMyType      = new bool[] { true, true, true, true, true, true, true, };
		}
		public override String Name { get { return "Edge"; } }
		public override bool IsNodeType { get { return false; } }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
	}

	//
	// Edge model
	//

	public sealed class MutexPimpedEdgeModel : ITypeModel
	{
		public MutexPimpedEdgeModel()
		{
			EdgeType_request.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_request.typeVar,
			};
			EdgeType_request.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_request.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_held_by.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_held_by.typeVar,
			};
			EdgeType_held_by.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_held_by.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_release.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_release.typeVar,
			};
			EdgeType_release.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_release.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_blocked.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_blocked.typeVar,
			};
			EdgeType_blocked.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_blocked.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_next.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_next.typeVar,
			};
			EdgeType_next.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_next.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_token.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_token.typeVar,
			};
			EdgeType_token.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_token.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameTypes = new ITypeFramework[] {
				EdgeType_Edge.typeVar,
				EdgeType_request.typeVar,
				EdgeType_held_by.typeVar,
				EdgeType_release.typeVar,
				EdgeType_blocked.typeVar,
				EdgeType_next.typeVar,
				EdgeType_token.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameTypes = new ITypeFramework[] {
				EdgeType_Edge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public IType RootType { get { return EdgeType_Edge.typeVar; } }
		public IType GetType(String name)
		{
			switch(name)
			{
				case "request" : return EdgeType_request.typeVar;
				case "held_by" : return EdgeType_held_by.typeVar;
				case "release" : return EdgeType_release.typeVar;
				case "blocked" : return EdgeType_blocked.typeVar;
				case "next" : return EdgeType_next.typeVar;
				case "token" : return EdgeType_token.typeVar;
				case "Edge" : return EdgeType_Edge.typeVar;
			}
			return null;
		}
		private ITypeFramework[] types = {
			EdgeType_request.typeVar,
			EdgeType_held_by.typeVar,
			EdgeType_release.typeVar,
			EdgeType_blocked.typeVar,
			EdgeType_next.typeVar,
			EdgeType_token.typeVar,
			EdgeType_Edge.typeVar,
		};
		public IType[] Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_request),
			typeof(EdgeType_held_by),
			typeof(EdgeType_release),
			typeof(EdgeType_blocked),
			typeof(EdgeType_next),
			typeof(EdgeType_token),
			typeof(EdgeType_Edge),
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
			new ValidateInfo(EdgeType_request.typeVar, NodeType_Process.typeVar, NodeType_Resource.typeVar, 0, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_held_by.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_release.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_blocked.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 0, int.MaxValue, 0, int.MaxValue),
			new ValidateInfo(EdgeType_next.typeVar, NodeType_Process.typeVar, NodeType_Process.typeVar, 0, 1, 0, 1),
			new ValidateInfo(EdgeType_token.typeVar, NodeType_Resource.typeVar, NodeType_Process.typeVar, 1, int.MaxValue, 0, int.MaxValue),
		};

		public String Name { get { return "MutexPimped"; } }
		public ITypeModel NodeModel { get { return nodeModel; } }
		public ITypeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "0ecfadd9bae7c1bee09ddc57f323923f"; } }
	}
}
