// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "VisitedExample.grg" on Sun Dec 28 14:48:37 GMT+01:00 2008

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

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


	public sealed class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		private static int poolLevel = 0;
		private static @Node[] pool = new @Node[10];
		
		static @Node() {
		}
		
		public @Node() : base(NodeType_Node.typeVar)
		{
			// implicit initialization, map/set creation of Node
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
				// implicit initialization, map/set creation of Node
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
				// implicit initialization, map/set creation of Node
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
			// implicit initialization, map/set creation of Node
		}
	}

	public sealed class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static NodeType_Node typeVar = new NodeType_Node();
		public static bool[] isA = new bool[] { true, };
		public static bool[] isMyType = new bool[] { true, };
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

	//
	// Node model
	//

	public sealed class StdNodeModel : GRGEN_LIBGR.INodeModel
	{
		public StdNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
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
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			NodeType_Node.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, };
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
		
		static @Edge() {
		}
		
		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
			// implicit initialization, map/set creation of Edge
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
				// implicit initialization, map/set creation of Edge
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
				// implicit initialization, map/set creation of Edge
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
			// implicit initialization, map/set creation of Edge
		}
	}

	public sealed class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, };
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
		
		static @UEdge() {
		}
		
		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(EdgeType_UEdge.typeVar, source, target)
		{
			// implicit initialization, map/set creation of UEdge
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
				// implicit initialization, map/set creation of UEdge
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
				// implicit initialization, map/set creation of UEdge
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
			// implicit initialization, map/set creation of UEdge
		}
	}

	public sealed class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static EdgeType_UEdge typeVar = new EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, true, };
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

	//
	// Edge model
	//

	public sealed class StdEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public StdEdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
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
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
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
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_AEdge),
			typeof(EdgeType_Edge),
			typeof(EdgeType_UEdge),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class StdGraphModel : GRGEN_LIBGR.IGraphModel
	{
		private StdNodeModel nodeModel = new StdNodeModel();
		private StdEdgeModel edgeModel = new StdEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "Std"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class Std : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public Std() : base(GetNextGraphName())
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

		private StdNodeModel nodeModel = new StdNodeModel();
		private StdEdgeModel edgeModel = new StdEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "Std"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
	}
}
