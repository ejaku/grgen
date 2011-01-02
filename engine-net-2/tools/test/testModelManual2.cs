/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

		public static Node_Process CreateNode(LGSPGraph graph)
		{
			Node_Process node = new Node_Process();
			graph.AddNode(node);
			return node;
		}

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
				"The node type \"Process\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "val": _val = (int) value; return;
				case "name": _name = (String) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Process\" does not have the attribute \" + attrName + \"\"!");
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

        public LGSPNode Retype(LGSPGraph graph, LGSPNode oldNode)
        {
            Node_Process newNode = new Node_Process();
            graph.AddNodeWithoutEvents(newNode, (int) NodeTypes.@Process);
            switch(oldNode.Type.TypeID)
            {
                case (int) NodeTypes.@Process:
                {
                    Node_Process old = (Node_Process) oldNode;
                    newNode.val = old.val;
                    newNode.name = old.name;
                    break;
                }
                case (int) NodeTypes.@Node:
                {
                    break;
                }
            }

            // Reassign all outgoing edges
            LGSPEdge outHead = oldNode.outhead;
            if(outHead != null)
            {
                LGSPEdge outCur = outHead;
                do
                {
                    outCur.source = newNode;
                    outCur = outCur.outNext;
                }
                while(outCur != outHead);
            }
            newNode.outhead = outHead;

            // Reassign all incoming edges
            LGSPEdge inHead = oldNode.outhead;
            if(inHead != null)
            {
                LGSPEdge inCur = inHead;
                do
                {
                    inCur.target = newNode;
                    inCur = inCur.inNext;
                }
                while(inCur != inHead);
            }
            newNode.inhead = inHead;

            return newNode;
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

		public static Node_Node CreateNode(LGSPGraph graph)
		{
			Node_Node node = new Node_Node();
			graph.AddNode(node);
			return node;
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
			NodeType_Process.typeVar.superOrSameGrGenTypes = NodeType_Process.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Process.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_Process.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
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

	public enum EdgeTypes { @Edge, @bigspeedcon, @speedway, @connection, @speedcon, @slowway, @fluffway };

	// *** Edge Edge ***

	public interface IEdge_Edge : IAttributes
	{
	}

	public sealed class Edge_Edge : LGSPEdge, IEdge_Edge
	{
		public Edge_Edge(LGSPNode source, LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target) { }
		public Object Clone() { return MemberwiseClone(); }

		public static Edge_Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_Edge edge = new Edge_Edge(source, target);
			graph.AddEdge(edge);
			return edge;
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
	}

	public sealed class EdgeType_Edge : EdgeType
	{
		public static EdgeType_Edge typeVar = new EdgeType_Edge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, };
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
	}

	// *** Edge bigspeedcon ***

	public interface IEdge_bigspeedcon : IEdge_speedcon, IEdge_slowway
	{
	}

	public sealed class Edge_bigspeedcon : LGSPEdge, IEdge_bigspeedcon
	{
		public Edge_bigspeedcon(LGSPNode source, LGSPNode target)
			: base(EdgeType_bigspeedcon.typeVar, source, target) { }
		public Object Clone() { return MemberwiseClone(); }

		public static Edge_bigspeedcon CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_bigspeedcon edge = new Edge_bigspeedcon(source, target);
			graph.AddEdge(edge);
			return edge;
		}

		private String _classkind;
		public String @classkind
		{
			get { return _classkind; }
			set { _classkind = value; }
		}

		private int _speed;
		public int @speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

		private int _slowspeed;
		public int @slowspeed
		{
			get { return _slowspeed; }
			set { _slowspeed = value; }
		}

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
				case "classkind": return _classkind;
				case "speed": return _speed;
				case "slowspeed": return _slowspeed;
				case "bandwidth": return _bandwidth;
			}
			throw new NullReferenceException(
				"The edge type \"bigspeedcon\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "classkind": _classkind = (String) value; return;
				case "speed": _speed = (int) value; return;
				case "slowspeed": _slowspeed = (int) value; return;
				case "bandwidth": _bandwidth = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"bigspeedcon\" does not have the attribute \" + attrName + \"\"!");
		}
	}

	public sealed class EdgeType_bigspeedcon : EdgeType
	{
		public static EdgeType_bigspeedcon typeVar = new EdgeType_bigspeedcon();
		public static bool[] isA = new bool[] { true, true, true, true, true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, };
		public EdgeType_bigspeedcon() : base((int) EdgeTypes.@bigspeedcon)
		{
		}
		public override String Name { get { return "bigspeedcon"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_bigspeedcon((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 4; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return EdgeType_speedcon.AttributeType_classkind;
				yield return EdgeType_speedway.AttributeType_speed;
				yield return EdgeType_slowway.AttributeType_slowspeed;
				yield return EdgeType_connection.AttributeType_bandwidth;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "classkind" : return EdgeType_speedcon.AttributeType_classkind;
				case "speed" : return EdgeType_speedway.AttributeType_speed;
				case "slowspeed" : return EdgeType_slowway.AttributeType_slowspeed;
				case "bandwidth" : return EdgeType_connection.AttributeType_bandwidth;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge speedway ***

	public interface IEdge_speedway : IEdge_Edge
	{
		int @speed { get; set; }
	}

	public sealed class Edge_speedway : LGSPEdge, IEdge_speedway
	{
		public Edge_speedway(LGSPNode source, LGSPNode target)
			: base(EdgeType_speedway.typeVar, source, target) { }
		public Object Clone() { return MemberwiseClone(); }

		public static Edge_speedway CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_speedway edge = new Edge_speedway(source, target);
			graph.AddEdge(edge);
			return edge;
		}

		private int _speed;
		public int @speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "speed": return _speed;
			}
			throw new NullReferenceException(
				"The edge type \"speedway\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "speed": _speed = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"speedway\" does not have the attribute \" + attrName + \"\"!");
		}
	}

	public sealed class EdgeType_speedway : EdgeType
	{
		public static EdgeType_speedway typeVar = new EdgeType_speedway();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, true, false, true, false, false, };
		public static AttributeType AttributeType_speed;
		public EdgeType_speedway() : base((int) EdgeTypes.@speedway)
		{
			AttributeType_speed = new AttributeType("speed", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "speedway"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_speedway((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_speed;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "speed" : return AttributeType_speed;
			}
			return null;
		}
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

		public static Edge_connection CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_connection edge = new Edge_connection(source, target);
			graph.AddEdge(edge);
			return edge;
		}

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
				"The edge type \"connection\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "bandwidth": _bandwidth = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"connection\" does not have the attribute \" + attrName + \"\"!");
		}
	}

	public sealed class EdgeType_connection : EdgeType
	{
		public static EdgeType_connection typeVar = new EdgeType_connection();
		public static bool[] isA = new bool[] { true, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, false, true, };
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

	// *** Edge speedcon ***

	public interface IEdge_speedcon : IEdge_connection, IEdge_speedway
	{
		String @classkind { get; set; }
	}

	public sealed class Edge_speedcon : LGSPEdge, IEdge_speedcon
	{
		public Edge_speedcon(LGSPNode source, LGSPNode target)
			: base(EdgeType_speedcon.typeVar, source, target) { }
		public Object Clone() { return MemberwiseClone(); }

		public static Edge_speedcon CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_speedcon edge = new Edge_speedcon(source, target);
			graph.AddEdge(edge);
			return edge;
		}

		private String _classkind;
		public String @classkind
		{
			get { return _classkind; }
			set { _classkind = value; }
		}

		private int _speed;
		public int @speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

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
				case "classkind": return _classkind;
				case "speed": return _speed;
				case "bandwidth": return _bandwidth;
			}
			throw new NullReferenceException(
				"The edge type \"speedcon\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "classkind": _classkind = (String) value; return;
				case "speed": _speed = (int) value; return;
				case "bandwidth": _bandwidth = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"speedcon\" does not have the attribute \" + attrName + \"\"!");
		}
	}

	public sealed class EdgeType_speedcon : EdgeType
	{
		public static EdgeType_speedcon typeVar = new EdgeType_speedcon();
		public static bool[] isA = new bool[] { true, false, true, true, true, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, true, false, false, };
		public static AttributeType AttributeType_classkind;
		public EdgeType_speedcon() : base((int) EdgeTypes.@speedcon)
		{
			AttributeType_classkind = new AttributeType("classkind", this, AttributeKind.StringAttr, null);
		}
		public override String Name { get { return "speedcon"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_speedcon((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_classkind;
				yield return EdgeType_speedway.AttributeType_speed;
				yield return EdgeType_connection.AttributeType_bandwidth;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "classkind" : return AttributeType_classkind;
				case "speed" : return EdgeType_speedway.AttributeType_speed;
				case "bandwidth" : return EdgeType_connection.AttributeType_bandwidth;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge slowway ***

	public interface IEdge_slowway : IEdge_Edge
	{
		int @slowspeed { get; set; }
	}

	public sealed class Edge_slowway : LGSPEdge, IEdge_slowway
	{
		public Edge_slowway(LGSPNode source, LGSPNode target)
			: base(EdgeType_slowway.typeVar, source, target) { }
		public Object Clone() { return MemberwiseClone(); }

		public static Edge_slowway CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_slowway edge = new Edge_slowway(source, target);
			graph.AddEdge(edge);
			return edge;
		}

		private int _slowspeed;
		public int @slowspeed
		{
			get { return _slowspeed; }
			set { _slowspeed = value; }
		}

		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "slowspeed": return _slowspeed;
			}
			throw new NullReferenceException(
				"The edge type \"slowway\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "slowspeed": _slowspeed = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"slowway\" does not have the attribute \" + attrName + \"\"!");
		}
	}

	public sealed class EdgeType_slowway : EdgeType
	{
		public static EdgeType_slowway typeVar = new EdgeType_slowway();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, true, false, };
		public static AttributeType AttributeType_slowspeed;
		public EdgeType_slowway() : base((int) EdgeTypes.@slowway)
		{
			AttributeType_slowspeed = new AttributeType("slowspeed", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "slowway"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_slowway((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_slowspeed;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "slowspeed" : return AttributeType_slowspeed;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
	}

	// *** Edge fluffway ***

	public interface IEdge_fluffway : IEdge_connection
	{
		int @numtunnels { get; set; }
	}

	public sealed class Edge_fluffway : LGSPEdge, IEdge_fluffway
	{
		public Edge_fluffway(LGSPNode source, LGSPNode target)
			: base(EdgeType_fluffway.typeVar, source, target) { }
		public Object Clone() { return MemberwiseClone(); }

		public static Edge_fluffway CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			Edge_fluffway edge = new Edge_fluffway(source, target);
			graph.AddEdge(edge);
			return edge;
		}

		private int _numtunnels;
		public int @numtunnels
		{
			get { return _numtunnels; }
			set { _numtunnels = value; }
		}

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
				case "numtunnels": return _numtunnels;
				case "bandwidth": return _bandwidth;
			}
			throw new NullReferenceException(
				"The edge type \"fluffway\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "numtunnels": _numtunnels = (int) value; return;
				case "bandwidth": _bandwidth = (int) value; return;
			}
			throw new NullReferenceException(
				"The edge type \"fluffway\" does not have the attribute \" + attrName + \"\"!");
		}
	}

	public sealed class EdgeType_fluffway : EdgeType
	{
		public static EdgeType_fluffway typeVar = new EdgeType_fluffway();
		public static bool[] isA = new bool[] { true, false, false, true, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, };
		public static AttributeType AttributeType_numtunnels;
		public EdgeType_fluffway() : base((int) EdgeTypes.@fluffway)
		{
			AttributeType_numtunnels = new AttributeType("numtunnels", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "fluffway"; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new Edge_fluffway((LGSPNode) source, (LGSPNode) target);
		}
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_numtunnels;
				yield return EdgeType_connection.AttributeType_bandwidth;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "numtunnels" : return AttributeType_numtunnels;
				case "bandwidth" : return EdgeType_connection.AttributeType_bandwidth;
			}
			return null;
		}
		public override bool IsA(GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}

        public LGSPEdge Retype(LGSPGraph graph, LGSPEdge oldEdge)
        {
            Edge_fluffway newEdge = new Edge_fluffway(oldEdge.source, oldEdge.target);
            switch(oldEdge.Type.TypeID)
            {
                case (int) EdgeTypes.@speedcon:
                case (int) EdgeTypes.@bigspeedcon:
                case (int) EdgeTypes.@connection:
                {
                    IEdge_connection old = (IEdge_connection) oldEdge;
                    newEdge.bandwidth = old.bandwidth;
                    break;
                }
                case (int) EdgeTypes.@Edge:
                {
                    break;
                }
            }

            graph.ReplaceEdge(oldEdge, newEdge);

            return newEdge;
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
				EdgeType_bigspeedcon.typeVar,
				EdgeType_speedway.typeVar,
				EdgeType_connection.typeVar,
				EdgeType_speedcon.typeVar,
				EdgeType_slowway.typeVar,
				EdgeType_fluffway.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_bigspeedcon.typeVar.subOrSameGrGenTypes = EdgeType_bigspeedcon.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_bigspeedcon.typeVar,
			};
			EdgeType_bigspeedcon.typeVar.superOrSameGrGenTypes = EdgeType_bigspeedcon.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_bigspeedcon.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_speedway.typeVar,
				EdgeType_connection.typeVar,
				EdgeType_speedcon.typeVar,
				EdgeType_slowway.typeVar,
			};
			EdgeType_speedway.typeVar.subOrSameGrGenTypes = EdgeType_speedway.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_speedway.typeVar,
				EdgeType_bigspeedcon.typeVar,
				EdgeType_speedcon.typeVar,
			};
			EdgeType_speedway.typeVar.superOrSameGrGenTypes = EdgeType_speedway.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_speedway.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_connection.typeVar.subOrSameGrGenTypes = EdgeType_connection.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_connection.typeVar,
				EdgeType_bigspeedcon.typeVar,
				EdgeType_speedcon.typeVar,
				EdgeType_fluffway.typeVar,
			};
			EdgeType_connection.typeVar.superOrSameGrGenTypes = EdgeType_connection.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_connection.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_speedcon.typeVar.subOrSameGrGenTypes = EdgeType_speedcon.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_speedcon.typeVar,
				EdgeType_bigspeedcon.typeVar,
			};
			EdgeType_speedcon.typeVar.superOrSameGrGenTypes = EdgeType_speedcon.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_speedcon.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_speedway.typeVar,
				EdgeType_connection.typeVar,
			};
			EdgeType_slowway.typeVar.subOrSameGrGenTypes = EdgeType_slowway.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_slowway.typeVar,
				EdgeType_bigspeedcon.typeVar,
			};
			EdgeType_slowway.typeVar.superOrSameGrGenTypes = EdgeType_slowway.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_slowway.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_fluffway.typeVar.subOrSameGrGenTypes = EdgeType_fluffway.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_fluffway.typeVar,
			};
			EdgeType_fluffway.typeVar.superOrSameGrGenTypes = EdgeType_fluffway.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_fluffway.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_connection.typeVar,
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
				case "bigspeedcon" : return EdgeType_bigspeedcon.typeVar;
				case "speedway" : return EdgeType_speedway.typeVar;
				case "connection" : return EdgeType_connection.typeVar;
				case "speedcon" : return EdgeType_speedcon.typeVar;
				case "slowway" : return EdgeType_slowway.typeVar;
				case "fluffway" : return EdgeType_fluffway.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
			EdgeType_Edge.typeVar,
			EdgeType_bigspeedcon.typeVar,
			EdgeType_speedway.typeVar,
			EdgeType_connection.typeVar,
			EdgeType_speedcon.typeVar,
			EdgeType_slowway.typeVar,
			EdgeType_fluffway.typeVar,
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_Edge),
			typeof(EdgeType_bigspeedcon),
			typeof(EdgeType_speedway),
			typeof(EdgeType_connection),
			typeof(EdgeType_speedcon),
			typeof(EdgeType_slowway),
			typeof(EdgeType_fluffway),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			EdgeType_speedway.AttributeType_speed,
			EdgeType_connection.AttributeType_bandwidth,
			EdgeType_speedcon.AttributeType_classkind,
			EdgeType_slowway.AttributeType_slowspeed,
			EdgeType_fluffway.AttributeType_numtunnels,
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
		public String MD5Hash { get { return "16a340b17f5197df467efdb2439b4692"; } }
	}
}
