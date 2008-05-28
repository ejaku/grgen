// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Turing3\Turing3.grg" on Wed May 28 22:10:20 CEST 2008

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_Turing3
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

	public enum NodeTypes { @Node, @BandPosition, @State, @WriteValue };

	// *** Node Node ***


	public sealed class @Node : LGSPNode, INode
	{
		private static int poolLevel = 0;
		private static @Node[] pool = new @Node[10];
		public @Node() : base(NodeType_Node.typeVar)
		{
		}

		public static NodeType_Node TypeInstance { get { return NodeType_Node.typeVar; } }

		public override INode Clone() { return new @Node(this); }

		private @Node(@Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
		public static @Node CreateNode(LGSPGraph graph)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @Node CreateNode(LGSPGraph graph, String varName)
		{
			@Node node;
			if(poolLevel == 0)
				node = new @Node();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
			return new @Node();
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
			return new @Node();
		}

	}

	// *** Node BandPosition ***

	public interface IBandPosition : INode
	{
		int @value { get; set; }
	}

	public sealed class @BandPosition : LGSPNode, IBandPosition
	{
		private static int poolLevel = 0;
		private static @BandPosition[] pool = new @BandPosition[10];
		public @BandPosition() : base(NodeType_BandPosition.typeVar)
		{
		}

		public static NodeType_BandPosition TypeInstance { get { return NodeType_BandPosition.typeVar; } }

		public override INode Clone() { return new @BandPosition(this); }

		private @BandPosition(@BandPosition oldElem) : base(NodeType_BandPosition.typeVar)
		{
			_value = oldElem._value;
		}
		public static @BandPosition CreateNode(LGSPGraph graph)
		{
			@BandPosition node;
			if(poolLevel == 0)
				node = new @BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				node.@value = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static @BandPosition CreateNode(LGSPGraph graph, String varName)
		{
			@BandPosition node;
			if(poolLevel == 0)
				node = new @BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
				case "value": return this.@value;
			}
			throw new NullReferenceException(
				"The node type \"BandPosition\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": this.@value = (int) value; return;
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
		public static bool[] isA = new bool[] { true, true, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, };
		public static AttributeType AttributeType_value;
		public NodeType_BandPosition() : base((int) NodeTypes.@BandPosition)
		{
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "BandPosition"; } }
		public override INode CreateNode()
		{
			return new @BandPosition();
		}
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
			@BandPosition newNode = new @BandPosition();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@BandPosition:
					// copy attributes for: BandPosition
					{
						@IBandPosition old = (@IBandPosition) oldNode;
						newNode.@value = old.@value;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node State ***

	public interface IState : INode
	{
	}

	public sealed class @State : LGSPNode, IState
	{
		private static int poolLevel = 0;
		private static @State[] pool = new @State[10];
		public @State() : base(NodeType_State.typeVar)
		{
		}

		public static NodeType_State TypeInstance { get { return NodeType_State.typeVar; } }

		public override INode Clone() { return new @State(this); }

		private @State(@State oldElem) : base(NodeType_State.typeVar)
		{
		}
		public static @State CreateNode(LGSPGraph graph)
		{
			@State node;
			if(poolLevel == 0)
				node = new @State();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
			}
			graph.AddNode(node);
			return node;
		}

		public static @State CreateNode(LGSPGraph graph, String varName)
		{
			@State node;
			if(poolLevel == 0)
				node = new @State();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, };
		public NodeType_State() : base((int) NodeTypes.@State)
		{
		}
		public override String Name { get { return "State"; } }
		public override INode CreateNode()
		{
			return new @State();
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
			return new @State();
		}

	}

	// *** Node WriteValue ***

	public interface IWriteValue : INode
	{
		int @value { get; set; }
	}

	public sealed class @WriteValue : LGSPNode, IWriteValue
	{
		private static int poolLevel = 0;
		private static @WriteValue[] pool = new @WriteValue[10];
		public @WriteValue() : base(NodeType_WriteValue.typeVar)
		{
		}

		public static NodeType_WriteValue TypeInstance { get { return NodeType_WriteValue.typeVar; } }

		public override INode Clone() { return new @WriteValue(this); }

		private @WriteValue(@WriteValue oldElem) : base(NodeType_WriteValue.typeVar)
		{
			_value = oldElem._value;
		}
		public static @WriteValue CreateNode(LGSPGraph graph)
		{
			@WriteValue node;
			if(poolLevel == 0)
				node = new @WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				node.@value = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static @WriteValue CreateNode(LGSPGraph graph, String varName)
		{
			@WriteValue node;
			if(poolLevel == 0)
				node = new @WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
				case "value": return this.@value;
			}
			throw new NullReferenceException(
				"The node type \"WriteValue\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "value": this.@value = (int) value; return;
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
		public static bool[] isA = new bool[] { true, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, true, };
		public static AttributeType AttributeType_value;
		public NodeType_WriteValue() : base((int) NodeTypes.@WriteValue)
		{
			AttributeType_value = new AttributeType("value", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "WriteValue"; } }
		public override INode CreateNode()
		{
			return new @WriteValue();
		}
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
			@WriteValue newNode = new @WriteValue();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@WriteValue:
					// copy attributes for: WriteValue
					{
						@IWriteValue old = (@IWriteValue) oldNode;
						newNode.@value = old.@value;
					}
					break;
			}
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
				NodeType_BandPosition.typeVar,
				NodeType_State.typeVar,
				NodeType_WriteValue.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new NodeType[] {
				NodeType_BandPosition.typeVar,
				NodeType_State.typeVar,
				NodeType_WriteValue.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new NodeType[] {
			};
			NodeType_BandPosition.typeVar.subOrSameGrGenTypes = NodeType_BandPosition.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_BandPosition.typeVar,
			};
			NodeType_BandPosition.typeVar.directSubGrGenTypes = NodeType_BandPosition.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_BandPosition.typeVar.superOrSameGrGenTypes = NodeType_BandPosition.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_BandPosition.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_BandPosition.typeVar.directSuperGrGenTypes = NodeType_BandPosition.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_State.typeVar.subOrSameGrGenTypes = NodeType_State.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_State.typeVar,
			};
			NodeType_State.typeVar.directSubGrGenTypes = NodeType_State.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_State.typeVar.superOrSameGrGenTypes = NodeType_State.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_State.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_State.typeVar.directSuperGrGenTypes = NodeType_State.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_WriteValue.typeVar.subOrSameGrGenTypes = NodeType_WriteValue.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_WriteValue.typeVar,
			};
			NodeType_WriteValue.typeVar.directSubGrGenTypes = NodeType_WriteValue.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_WriteValue.typeVar.superOrSameGrGenTypes = NodeType_WriteValue.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_WriteValue.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_WriteValue.typeVar.directSuperGrGenTypes = NodeType_WriteValue.typeVar.directSuperTypes = new NodeType[] {
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
				case "BandPosition" : return NodeType_BandPosition.typeVar;
				case "State" : return NodeType_State.typeVar;
				case "WriteValue" : return NodeType_WriteValue.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_BandPosition.typeVar,
			NodeType_State.typeVar,
			NodeType_WriteValue.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_BandPosition),
			typeof(NodeType_State),
			typeof(NodeType_WriteValue),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			NodeType_BandPosition.AttributeType_value,
			NodeType_WriteValue.AttributeType_value,
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @right, @readZero, @readOne, @moveLeft, @moveRight };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : EdgeType
	{
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override String Name { get { return "AEdge"; } }
		public override Directedness Directedness { get { return Directedness.Arbitrary; } }
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


	public sealed class @Edge : LGSPEdge, IEdge
	{
		private static int poolLevel = 0;
		private static @Edge[] pool = new @Edge[10];
		public @Edge(LGSPNode source, LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
		}

		public static EdgeType_Edge TypeInstance { get { return EdgeType_Edge.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @Edge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @Edge(@Edge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public static @Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @Edge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@Edge edge;
			if(poolLevel == 0)
				edge = new @Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override String Name { get { return "Edge"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @Edge((LGSPNode) source, (LGSPNode) target);
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
			return new @Edge((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed class @UEdge : LGSPEdge, IEdge
	{
		private static int poolLevel = 0;
		private static @UEdge[] pool = new @UEdge[10];
		public @UEdge(LGSPNode source, LGSPNode target)
			: base(EdgeType_UEdge.typeVar, source, target)
		{
		}

		public static EdgeType_UEdge TypeInstance { get { return EdgeType_UEdge.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @UEdge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @UEdge(@UEdge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		public static @UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @UEdge CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@UEdge edge;
			if(poolLevel == 0)
				edge = new @UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override String Name { get { return "UEdge"; } }
		public override Directedness Directedness { get { return Directedness.Undirected; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @UEdge((LGSPNode) source, (LGSPNode) target);
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
			return new @UEdge((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge right ***

	public interface Iright : IEdge
	{
	}

	public sealed class @right : LGSPEdge, Iright
	{
		private static int poolLevel = 0;
		private static @right[] pool = new @right[10];
		public @right(LGSPNode source, LGSPNode target)
			: base(EdgeType_right.typeVar, source, target)
		{
		}

		public static EdgeType_right TypeInstance { get { return EdgeType_right.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @right(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @right(@right oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_right.typeVar, newSource, newTarget)
		{
		}
		public static @right CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@right edge;
			if(poolLevel == 0)
				edge = new @right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @right CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@right edge;
			if(poolLevel == 0)
				edge = new @right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, };
		public EdgeType_right() : base((int) EdgeTypes.@right)
		{
		}
		public override String Name { get { return "right"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @right((LGSPNode) source, (LGSPNode) target);
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
			return new @right((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge readZero ***

	public interface IreadZero : IEdge
	{
	}

	public sealed class @readZero : LGSPEdge, IreadZero
	{
		private static int poolLevel = 0;
		private static @readZero[] pool = new @readZero[10];
		public @readZero(LGSPNode source, LGSPNode target)
			: base(EdgeType_readZero.typeVar, source, target)
		{
		}

		public static EdgeType_readZero TypeInstance { get { return EdgeType_readZero.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @readZero(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @readZero(@readZero oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_readZero.typeVar, newSource, newTarget)
		{
		}
		public static @readZero CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@readZero edge;
			if(poolLevel == 0)
				edge = new @readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @readZero CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@readZero edge;
			if(poolLevel == 0)
				edge = new @readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, };
		public EdgeType_readZero() : base((int) EdgeTypes.@readZero)
		{
		}
		public override String Name { get { return "readZero"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @readZero((LGSPNode) source, (LGSPNode) target);
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
			return new @readZero((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge readOne ***

	public interface IreadOne : IEdge
	{
	}

	public sealed class @readOne : LGSPEdge, IreadOne
	{
		private static int poolLevel = 0;
		private static @readOne[] pool = new @readOne[10];
		public @readOne(LGSPNode source, LGSPNode target)
			: base(EdgeType_readOne.typeVar, source, target)
		{
		}

		public static EdgeType_readOne TypeInstance { get { return EdgeType_readOne.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @readOne(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @readOne(@readOne oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_readOne.typeVar, newSource, newTarget)
		{
		}
		public static @readOne CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@readOne edge;
			if(poolLevel == 0)
				edge = new @readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @readOne CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@readOne edge;
			if(poolLevel == 0)
				edge = new @readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, };
		public EdgeType_readOne() : base((int) EdgeTypes.@readOne)
		{
		}
		public override String Name { get { return "readOne"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @readOne((LGSPNode) source, (LGSPNode) target);
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
			return new @readOne((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge moveLeft ***

	public interface ImoveLeft : IEdge
	{
	}

	public sealed class @moveLeft : LGSPEdge, ImoveLeft
	{
		private static int poolLevel = 0;
		private static @moveLeft[] pool = new @moveLeft[10];
		public @moveLeft(LGSPNode source, LGSPNode target)
			: base(EdgeType_moveLeft.typeVar, source, target)
		{
		}

		public static EdgeType_moveLeft TypeInstance { get { return EdgeType_moveLeft.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @moveLeft(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @moveLeft(@moveLeft oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_moveLeft.typeVar, newSource, newTarget)
		{
		}
		public static @moveLeft CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@moveLeft edge;
			if(poolLevel == 0)
				edge = new @moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @moveLeft CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@moveLeft edge;
			if(poolLevel == 0)
				edge = new @moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, };
		public EdgeType_moveLeft() : base((int) EdgeTypes.@moveLeft)
		{
		}
		public override String Name { get { return "moveLeft"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @moveLeft((LGSPNode) source, (LGSPNode) target);
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
			return new @moveLeft((LGSPNode) source, (LGSPNode) target);
		}

	}

	// *** Edge moveRight ***

	public interface ImoveRight : IEdge
	{
	}

	public sealed class @moveRight : LGSPEdge, ImoveRight
	{
		private static int poolLevel = 0;
		private static @moveRight[] pool = new @moveRight[10];
		public @moveRight(LGSPNode source, LGSPNode target)
			: base(EdgeType_moveRight.typeVar, source, target)
		{
		}

		public static EdgeType_moveRight TypeInstance { get { return EdgeType_moveRight.typeVar; } }

		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new @moveRight(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private @moveRight(@moveRight oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_moveRight.typeVar, newSource, newTarget)
		{
		}
		public static @moveRight CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target)
		{
			@moveRight edge;
			if(poolLevel == 0)
				edge = new @moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
				edge.source = source;
				edge.target = target;
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static @moveRight CreateEdge(LGSPGraph graph, LGSPNode source, LGSPNode target, String varName)
		{
			@moveRight edge;
			if(poolLevel == 0)
				edge = new @moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.flags &= ~(uint) LGSPElemFlags.HAS_VARIABLES;
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
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, };
		public EdgeType_moveRight() : base((int) EdgeTypes.@moveRight)
		{
		}
		public override String Name { get { return "moveRight"; } }
		public override Directedness Directedness { get { return Directedness.Directed; } }
		public override IEdge CreateEdge(INode source, INode target)
		{
			return new @moveRight((LGSPNode) source, (LGSPNode) target);
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
			return new @moveRight((LGSPNode) source, (LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class Turing3EdgeModel : IEdgeModel
	{
		public Turing3EdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
				EdgeType_right.typeVar,
				EdgeType_readZero.typeVar,
				EdgeType_readOne.typeVar,
				EdgeType_moveLeft.typeVar,
				EdgeType_moveRight.typeVar,
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
				EdgeType_right.typeVar,
				EdgeType_readZero.typeVar,
				EdgeType_readOne.typeVar,
				EdgeType_moveLeft.typeVar,
				EdgeType_moveRight.typeVar,
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new EdgeType[] {
				EdgeType_right.typeVar,
				EdgeType_readZero.typeVar,
				EdgeType_readOne.typeVar,
				EdgeType_moveLeft.typeVar,
				EdgeType_moveRight.typeVar,
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
			EdgeType_right.typeVar.subOrSameGrGenTypes = EdgeType_right.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_right.typeVar,
			};
			EdgeType_right.typeVar.directSubGrGenTypes = EdgeType_right.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_right.typeVar.superOrSameGrGenTypes = EdgeType_right.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_right.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_right.typeVar.directSuperGrGenTypes = EdgeType_right.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_readZero.typeVar.subOrSameGrGenTypes = EdgeType_readZero.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_readZero.typeVar,
			};
			EdgeType_readZero.typeVar.directSubGrGenTypes = EdgeType_readZero.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_readZero.typeVar.superOrSameGrGenTypes = EdgeType_readZero.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_readZero.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_readZero.typeVar.directSuperGrGenTypes = EdgeType_readZero.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_readOne.typeVar.subOrSameGrGenTypes = EdgeType_readOne.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_readOne.typeVar,
			};
			EdgeType_readOne.typeVar.directSubGrGenTypes = EdgeType_readOne.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_readOne.typeVar.superOrSameGrGenTypes = EdgeType_readOne.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_readOne.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_readOne.typeVar.directSuperGrGenTypes = EdgeType_readOne.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveLeft.typeVar.subOrSameGrGenTypes = EdgeType_moveLeft.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_moveLeft.typeVar,
			};
			EdgeType_moveLeft.typeVar.directSubGrGenTypes = EdgeType_moveLeft.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_moveLeft.typeVar.superOrSameGrGenTypes = EdgeType_moveLeft.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_moveLeft.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveLeft.typeVar.directSuperGrGenTypes = EdgeType_moveLeft.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveRight.typeVar.subOrSameGrGenTypes = EdgeType_moveRight.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_moveRight.typeVar,
			};
			EdgeType_moveRight.typeVar.directSubGrGenTypes = EdgeType_moveRight.typeVar.directSubTypes = new EdgeType[] {
			};
			EdgeType_moveRight.typeVar.superOrSameGrGenTypes = EdgeType_moveRight.typeVar.superOrSameTypes = new EdgeType[] {
				EdgeType_moveRight.typeVar,
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
			};
			EdgeType_moveRight.typeVar.directSuperGrGenTypes = EdgeType_moveRight.typeVar.directSuperTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
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
				case "right" : return EdgeType_right.typeVar;
				case "readZero" : return EdgeType_readZero.typeVar;
				case "readOne" : return EdgeType_readOne.typeVar;
				case "moveLeft" : return EdgeType_moveLeft.typeVar;
				case "moveRight" : return EdgeType_moveRight.typeVar;
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
			EdgeType_right.typeVar,
			EdgeType_readZero.typeVar,
			EdgeType_readOne.typeVar,
			EdgeType_moveLeft.typeVar,
			EdgeType_moveRight.typeVar,
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_AEdge),
			typeof(EdgeType_Edge),
			typeof(EdgeType_UEdge),
			typeof(EdgeType_right),
			typeof(EdgeType_readZero),
			typeof(EdgeType_readOne),
			typeof(EdgeType_moveLeft),
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

		public String ModelName { get { return "Turing3"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class Turing3 : LGSPGraph, IGraphModel
	{
		public Turing3() : base(GetNextGraphName())
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

		public @BandPosition CreateNodeBandPosition()
		{
			return @BandPosition.CreateNode(this);
		}

		public @BandPosition CreateNodeBandPosition(String varName)
		{
			return @BandPosition.CreateNode(this, varName);
		}

		public @State CreateNodeState()
		{
			return @State.CreateNode(this);
		}

		public @State CreateNodeState(String varName)
		{
			return @State.CreateNode(this, varName);
		}

		public @WriteValue CreateNodeWriteValue()
		{
			return @WriteValue.CreateNode(this);
		}

		public @WriteValue CreateNodeWriteValue(String varName)
		{
			return @WriteValue.CreateNode(this, varName);
		}

		public @Edge CreateEdgeEdge(LGSPNode source, LGSPNode target)
		{
			return @Edge.CreateEdge(this, source, target);
		}

		public @Edge CreateEdgeEdge(LGSPNode source, LGSPNode target, String varName)
		{
			return @Edge.CreateEdge(this, source, target, varName);
		}

		public @UEdge CreateEdgeUEdge(LGSPNode source, LGSPNode target)
		{
			return @UEdge.CreateEdge(this, source, target);
		}

		public @UEdge CreateEdgeUEdge(LGSPNode source, LGSPNode target, String varName)
		{
			return @UEdge.CreateEdge(this, source, target, varName);
		}

		public @right CreateEdgeright(LGSPNode source, LGSPNode target)
		{
			return @right.CreateEdge(this, source, target);
		}

		public @right CreateEdgeright(LGSPNode source, LGSPNode target, String varName)
		{
			return @right.CreateEdge(this, source, target, varName);
		}

		public @readZero CreateEdgereadZero(LGSPNode source, LGSPNode target)
		{
			return @readZero.CreateEdge(this, source, target);
		}

		public @readZero CreateEdgereadZero(LGSPNode source, LGSPNode target, String varName)
		{
			return @readZero.CreateEdge(this, source, target, varName);
		}

		public @readOne CreateEdgereadOne(LGSPNode source, LGSPNode target)
		{
			return @readOne.CreateEdge(this, source, target);
		}

		public @readOne CreateEdgereadOne(LGSPNode source, LGSPNode target, String varName)
		{
			return @readOne.CreateEdge(this, source, target, varName);
		}

		public @moveLeft CreateEdgemoveLeft(LGSPNode source, LGSPNode target)
		{
			return @moveLeft.CreateEdge(this, source, target);
		}

		public @moveLeft CreateEdgemoveLeft(LGSPNode source, LGSPNode target, String varName)
		{
			return @moveLeft.CreateEdge(this, source, target, varName);
		}

		public @moveRight CreateEdgemoveRight(LGSPNode source, LGSPNode target)
		{
			return @moveRight.CreateEdge(this, source, target);
		}

		public @moveRight CreateEdgemoveRight(LGSPNode source, LGSPNode target, String varName)
		{
			return @moveRight.CreateEdge(this, source, target, varName);
		}

		private Turing3NodeModel nodeModel = new Turing3NodeModel();
		private Turing3EdgeModel edgeModel = new Turing3EdgeModel();
		private ValidateInfo[] validateInfos = {
			new ValidateInfo(EdgeType_right.typeVar, NodeType_BandPosition.typeVar, NodeType_BandPosition.typeVar, 0, 1, 0, 1),
		};

		public String ModelName { get { return "Turing3"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
	}
}
