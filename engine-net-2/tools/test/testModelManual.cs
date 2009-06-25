/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.models.test
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

	public enum NodeTypes { @Process, @Node };

	// *** Node Process ***

	public interface INode_Process : INode_Node
	{
		String @name { get; set; }
		int @val { get; set; }
	}

	public sealed class Node_Process : LGSPNode, INode_Process
	{
        public Node_Process() : base(NodeType_Process.typeVar) { }

		public Object Clone() { return MemberwiseClone(); }
		private int _val;
		public int @val
		{
			get { return _val; }
			set { _val = value; }
		}

		private String _name;
		public String @name
		{
			get { return _name; }
			set { _name = value; }
		}

        public override object GetAttribute(string attrName)
        {
            switch(attrName)
            {
                case "val": return _val;
                case "name": return _name;
            }
            throw new NullReferenceException(
                "The node type \"Process\" does not have the attribute \"" + attrName + "\"!");
        }

        public override void SetAttribute(string attrName, object value)
        {
            switch(attrName)
            {
                case "val": _val = (int) value; return;
                case "name": _name = (String) value; return;
            }
            throw new NullReferenceException(
                "The node type \"Process\" does not have the attribute \"" + attrName + "\"!");
        }

        public static Node_Process CreateNode(LGSPGraph graph)
        {
            Node_Process node = new Node_Process();
            graph.AddNode(node);
            return node;
        }
    }

	public sealed class NodeType_Process : NodeType
	{
		public static NodeType_Process typeVar = new NodeType_Process();
		public static bool[] isA = new bool[] { true, true, };
		public static bool[] isMyType = new bool[] { true, false, };
		public static AttributeType AttributeType_name;
		public static AttributeType AttributeType_val;
		public NodeType_Process() : base((int) NodeTypes.@Process)
		{
			AttributeType_name = new AttributeType("name", this, AttributeKind.StringAttr, null);
			AttributeType_val = new AttributeType("val", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "Process"; } }
        public override INode CreateNode() { return new Node_Process(); }

		public override IAttributes CreateAttributes() { return new Node_Process(); }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_val;
				yield return AttributeType_name;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "val" : return AttributeType_val;
				case "name" : return AttributeType_name;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Node Node ***

	public interface INode_Node : IAttributes
	{
	}

	public sealed class Node_Node : LGSPNode, INode_Node
	{
        public Node_Node() : base(NodeType_Node.typeVar) { }

		public Object Clone() { return MemberwiseClone(); }

        public override object GetAttribute(string attrName)
        {
            throw new NullReferenceException(
                "The node type \"Node\" does not have the attribute \"" + attrName + "\"!");
        }

        public override void SetAttribute(string attrName, object value)
        {
            throw new NullReferenceException(
                "The node type \"Node\" does not have the attribute \"" + attrName + "\"!");
        }
    }

	public sealed class NodeType_Node : NodeType
	{
		public static NodeType_Node typeVar = new NodeType_Node();
		public static bool[] isA = new bool[] { false, true, };
		public static bool[] isMyType = new bool[] { true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override String Name { get { return "Node"; } }
        public override INode CreateNode() { return new Node_Node(); }
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

	public sealed class testNodeModel : INodeModel
	{
		public testNodeModel()
		{
			NodeType_Process.typeVar.subOrSameGrGenTypes = NodeType_Process.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Process.typeVar,
			};
			NodeType_Process.typeVar.subOrSameGrGenTypes = NodeType_Process.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Process.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
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
			NodeType_Process.typeVar,
			NodeType_Node.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Process),
			typeof(NodeType_Node),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			NodeType_Process.AttributeType_name,
			NodeType_Process.AttributeType_val,
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @Edge, @connection };

	// *** Edge Edge ***

	public interface IEdge_Edge : IAttributes
	{
	}

	public sealed class Edge_Edge : LGSPEdge, IEdge_Edge
	{
        public Edge_Edge(LGSPNode source, LGSPNode target)
            : base(EdgeType_Edge.typeVar, source, target) { }

		public Object Clone() { return MemberwiseClone(); }

        public override object GetAttribute(string attrName)
        {
            throw new NullReferenceException(
                "The edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
        }

        public override void SetAttribute(string attrName, object value)
        {
            throw new NullReferenceException(
                "The edge type \"Edge\" does not have the attribute \"" + attrName + "\"!");
        }
    }

	public sealed class EdgeType_Edge : EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { true, false, };
		public static bool[] isMyType = new bool[] { true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override IEdge CreateEdge(INode source, INode target)
        {
            return new Edge_Edge((LGSPNode) source, (LGSPNode) target);
        }
		public override IAttributes CreateAttributes() { return null; }
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<AttributeType> AttributeTypes { get { yield break; } }
		public override AttributeType GetAttributeType(String name) { return null; }
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge connection ***

	public interface IEdge_connection : IEdge_Edge
	{
		int @bandwidth { get; set; }
	}

	public sealed class Edge_connection : LGSPEdge, IEdge_connection
	{
        public Edge_connection(LGSPNode source, LGSPNode target)
            : base(EdgeType_connection.typeVar, source, target) { }

        public Object Clone() { return MemberwiseClone(); }
		private int _bandwidth;
		public int @bandwidth
		{
			get { return _bandwidth; }
			set { _bandwidth = value; }
		}


        public override object GetAttribute(string attrName)
        {
            switch(attrName)
            {
                case "bandwidth": return _bandwidth;
            }
            throw new NullReferenceException(
                "The edge type \"connection\" does not have the attribute \"" + attrName + "\"!");
        }

        public override void SetAttribute(string attrName, object value)
        {
            switch(attrName)
            {
                case "bandwidth": _bandwidth = (int) value; return;
            }
            throw new NullReferenceException(
                "The edge type \"connection\" does not have the attribute \"" + attrName + "\"!");
        }

        public static Edge_connection CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
        {
            Edge_connection edge = new Edge_connection(source, target);
            graph.AddEdge(edge);
            return edge;
        }
    }

	public sealed class EdgeType_connection : EdgeType
	{
		public static EdgeType_connection typeVar = new EdgeType_connection();
		public static bool[] isA = new bool[] { true, true, };
		public static bool[] isMyType = new bool[] { false, true, };
		public static AttributeType AttributeType_bandwidth;
		public EdgeType_connection() : base((int) EdgeTypes.@connection)
		{
			AttributeType_bandwidth = new AttributeType("bandwidth", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "connection"; } }
        public override IEdge CreateEdge(INode source, INode target)
        {
            return new Edge_connection((LGSPNode) source, (LGSPNode) target);
        }
        public override IAttributes CreateAttributes()
        {
            throw new Exception("CreateAttributes is no longer supported");
        }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_bandwidth;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "bandwidth" : return AttributeType_bandwidth;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	//
	// Edge model
	//

	public sealed class testEdgeModel : IEdgeModel
	{
		public testEdgeModel()
		{
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
				EdgeType_connection.typeVar,
			};
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_connection.typeVar.subOrSameGrGenTypes = EdgeType_connection.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_connection.typeVar,
			};
			EdgeType_connection.typeVar.subOrSameGrGenTypes = EdgeType_connection.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_connection.typeVar,
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
				case "Edge" : return EdgeType_Edge.typeVar;
				case "connection" : return EdgeType_connection.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
			EdgeType_Edge.typeVar,
			EdgeType_connection.typeVar,
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_Edge),
			typeof(EdgeType_connection),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			EdgeType_connection.AttributeType_bandwidth,
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class testGraphModel : IGraphModel
	{
		private testNodeModel nodeModel = new testNodeModel();
		private testEdgeModel edgeModel = new testEdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String Name { get { return "test"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "ed674c417526b8f9c14286768b74e3f3"; } }
	}
}
