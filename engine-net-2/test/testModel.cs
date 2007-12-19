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

	public enum NodeTypes { @Node, @A1, @A2, @A3, @A4, @A5, @B21, @B22, @B23, @B41, @B42, @B43, @C221, @C222_411, @C412_421_431_51, @C432_422, @D11_2221, @D2211_2222_31, @D231_4121 };

	// *** Node Node ***

	public interface INode_Node : IAttributes
	{
	}

	public sealed class Node_Node : LGSPNode, INode_Node
	{
		private static int poolLevel = 0;
		private static Node_Node[] pool = new Node_Node[10];
		public Node_Node() : base(NodeType_Node.typeVar)
		{
		}
		public override INode Clone() { return new Node_Node(this); }

		private Node_Node(Node_Node oldElem) : base(NodeType_Node.typeVar)
		{
		}
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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, };
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

	// *** Node A1 ***

	public interface INode_A1 : INode_Node
	{
		int @a1 { get; set; }
	}

	public sealed class Node_A1 : LGSPNode, INode_A1
	{
		private static int poolLevel = 0;
		private static Node_A1[] pool = new Node_A1[10];
		public Node_A1() : base(NodeType_A1.typeVar)
		{
		}
		public override INode Clone() { return new Node_A1(this); }

		private Node_A1(Node_A1 oldElem) : base(NodeType_A1.typeVar)
		{
			_a1 = oldElem._a1;
		}
		public static Node_A1 CreateNode(LGSPGraph graph)
		{
			Node_A1 node;
			if(poolLevel == 0)
				node = new Node_A1();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a1 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_A1 CreateNode(LGSPGraph graph, String varName)
		{
			Node_A1 node;
			if(poolLevel == 0)
				node = new Node_A1();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a1 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _a1;
		public int @a1
		{
			get { return _a1; }
			set { _a1 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a1": return @a1;
			}
			throw new NullReferenceException(
				"The node type \"A1\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a1": @a1 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A1\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@a1 = 0;
		}
	}

	public sealed class NodeType_A1 : NodeType
	{
		public static NodeType_A1 typeVar = new NodeType_A1();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public static AttributeType AttributeType_a1;
		public NodeType_A1() : base((int) NodeTypes.@A1)
		{
			AttributeType_a1 = new AttributeType("a1", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "A1"; } }
		public override INode CreateNode() { return new Node_A1(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a1;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "a1" : return AttributeType_a1;
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
			Node_A1 newNode = new Node_A1();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A1:
				case (int) NodeTypes.@D11_2221:
					// copy attributes for: A1
					{
						INode_A1 old = (INode_A1) oldNode;
						newNode.@a1 = old.@a1;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node A2 ***

	public interface INode_A2 : INode_Node
	{
		int @a2 { get; set; }
	}

	public sealed class Node_A2 : LGSPNode, INode_A2
	{
		private static int poolLevel = 0;
		private static Node_A2[] pool = new Node_A2[10];
		public Node_A2() : base(NodeType_A2.typeVar)
		{
		}
		public override INode Clone() { return new Node_A2(this); }

		private Node_A2(Node_A2 oldElem) : base(NodeType_A2.typeVar)
		{
			_a2 = oldElem._a2;
		}
		public static Node_A2 CreateNode(LGSPGraph graph)
		{
			Node_A2 node;
			if(poolLevel == 0)
				node = new Node_A2();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a2 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_A2 CreateNode(LGSPGraph graph, String varName)
		{
			Node_A2 node;
			if(poolLevel == 0)
				node = new Node_A2();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a2 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a2": return @a2;
			}
			throw new NullReferenceException(
				"The node type \"A2\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a2": @a2 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A2\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@a2 = 0;
		}
	}

	public sealed class NodeType_A2 : NodeType
	{
		public static NodeType_A2 typeVar = new NodeType_A2();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, true, true, true, false, false, false, true, true, false, false, true, true, true, };
		public static AttributeType AttributeType_a2;
		public NodeType_A2() : base((int) NodeTypes.@A2)
		{
			AttributeType_a2 = new AttributeType("a2", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "A2"; } }
		public override INode CreateNode() { return new Node_A2(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a2;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "a2" : return AttributeType_a2;
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
			Node_A2 newNode = new Node_A2();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@B23:
				case (int) NodeTypes.@C221:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node A3 ***

	public interface INode_A3 : INode_Node
	{
		int @a3 { get; set; }
	}

	public sealed class Node_A3 : LGSPNode, INode_A3
	{
		private static int poolLevel = 0;
		private static Node_A3[] pool = new Node_A3[10];
		public Node_A3() : base(NodeType_A3.typeVar)
		{
		}
		public override INode Clone() { return new Node_A3(this); }

		private Node_A3(Node_A3 oldElem) : base(NodeType_A3.typeVar)
		{
			_a3 = oldElem._a3;
		}
		public static Node_A3 CreateNode(LGSPGraph graph)
		{
			Node_A3 node;
			if(poolLevel == 0)
				node = new Node_A3();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a3 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_A3 CreateNode(LGSPGraph graph, String varName)
		{
			Node_A3 node;
			if(poolLevel == 0)
				node = new Node_A3();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a3 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _a3;
		public int @a3
		{
			get { return _a3; }
			set { _a3 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a3": return @a3;
			}
			throw new NullReferenceException(
				"The node type \"A3\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a3": @a3 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A3\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@a3 = 0;
		}
	}

	public sealed class NodeType_A3 : NodeType
	{
		public static NodeType_A3 typeVar = new NodeType_A3();
		public static bool[] isA = new bool[] { true, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public static AttributeType AttributeType_a3;
		public NodeType_A3() : base((int) NodeTypes.@A3)
		{
			AttributeType_a3 = new AttributeType("a3", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "A3"; } }
		public override INode CreateNode() { return new Node_A3(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a3;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "a3" : return AttributeType_a3;
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
			Node_A3 newNode = new Node_A3();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A3:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: A3
					{
						INode_A3 old = (INode_A3) oldNode;
						newNode.@a3 = old.@a3;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node A4 ***

	public interface INode_A4 : INode_Node
	{
		int @a4 { get; set; }
	}

	public sealed class Node_A4 : LGSPNode, INode_A4
	{
		private static int poolLevel = 0;
		private static Node_A4[] pool = new Node_A4[10];
		public Node_A4() : base(NodeType_A4.typeVar)
		{
		}
		public override INode Clone() { return new Node_A4(this); }

		private Node_A4(Node_A4 oldElem) : base(NodeType_A4.typeVar)
		{
			_a4 = oldElem._a4;
		}
		public static Node_A4 CreateNode(LGSPGraph graph)
		{
			Node_A4 node;
			if(poolLevel == 0)
				node = new Node_A4();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a4 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_A4 CreateNode(LGSPGraph graph, String varName)
		{
			Node_A4 node;
			if(poolLevel == 0)
				node = new Node_A4();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a4 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return @a4;
			}
			throw new NullReferenceException(
				"The node type \"A4\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": @a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A4\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@a4 = 0;
		}
	}

	public sealed class NodeType_A4 : NodeType
	{
		public static NodeType_A4 typeVar = new NodeType_A4();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, false, true, true, true, false, true, true, true, true, true, true, };
		public static AttributeType AttributeType_a4;
		public NodeType_A4() : base((int) NodeTypes.@A4)
		{
			AttributeType_a4 = new AttributeType("a4", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "A4"; } }
		public override INode CreateNode() { return new Node_A4(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a4;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "a4" : return AttributeType_a4;
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
			Node_A4 newNode = new Node_A4();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@B42:
				case (int) NodeTypes.@B43:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@C412_421_431_51:
				case (int) NodeTypes.@C432_422:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node A5 ***

	public interface INode_A5 : INode_Node
	{
		int @a5 { get; set; }
	}

	public sealed class Node_A5 : LGSPNode, INode_A5
	{
		private static int poolLevel = 0;
		private static Node_A5[] pool = new Node_A5[10];
		public Node_A5() : base(NodeType_A5.typeVar)
		{
		}
		public override INode Clone() { return new Node_A5(this); }

		private Node_A5(Node_A5 oldElem) : base(NodeType_A5.typeVar)
		{
			_a5 = oldElem._a5;
		}
		public static Node_A5 CreateNode(LGSPGraph graph)
		{
			Node_A5 node;
			if(poolLevel == 0)
				node = new Node_A5();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a5 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_A5 CreateNode(LGSPGraph graph, String varName)
		{
			Node_A5 node;
			if(poolLevel == 0)
				node = new Node_A5();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a5 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _a5;
		public int @a5
		{
			get { return _a5; }
			set { _a5 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a5": return @a5;
			}
			throw new NullReferenceException(
				"The node type \"A5\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a5": @a5 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"A5\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@a5 = 0;
		}
	}

	public sealed class NodeType_A5 : NodeType
	{
		public static NodeType_A5 typeVar = new NodeType_A5();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, false, false, false, false, false, false, true, false, false, false, true, };
		public static AttributeType AttributeType_a5;
		public NodeType_A5() : base((int) NodeTypes.@A5)
		{
			AttributeType_a5 = new AttributeType("a5", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "A5"; } }
		public override INode CreateNode() { return new Node_A5(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_a5;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "a5" : return AttributeType_a5;
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
			Node_A5 newNode = new Node_A5();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A5:
				case (int) NodeTypes.@C412_421_431_51:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A5
					{
						INode_A5 old = (INode_A5) oldNode;
						newNode.@a5 = old.@a5;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node B21 ***

	public interface INode_B21 : INode_A2
	{
		int @b21 { get; set; }
	}

	public sealed class Node_B21 : LGSPNode, INode_B21
	{
		private static int poolLevel = 0;
		private static Node_B21[] pool = new Node_B21[10];
		public Node_B21() : base(NodeType_B21.typeVar)
		{
		}
		public override INode Clone() { return new Node_B21(this); }

		private Node_B21(Node_B21 oldElem) : base(NodeType_B21.typeVar)
		{
			_b21 = oldElem._b21;
			_a2 = oldElem._a2;
		}
		public static Node_B21 CreateNode(LGSPGraph graph)
		{
			Node_B21 node;
			if(poolLevel == 0)
				node = new Node_B21();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b21 = 0;
				node.@a2 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_B21 CreateNode(LGSPGraph graph, String varName)
		{
			Node_B21 node;
			if(poolLevel == 0)
				node = new Node_B21();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b21 = 0;
				node.@a2 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _b21;
		public int @b21
		{
			get { return _b21; }
			set { _b21 = value; }
		}

		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "b21": return @b21;
				case "a2": return @a2;
			}
			throw new NullReferenceException(
				"The node type \"B21\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "b21": @b21 = (int) value; return;
				case "a2": @a2 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B21\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@b21 = 0;
			this.@a2 = 0;
		}
	}

	public sealed class NodeType_B21 : NodeType
	{
		public static NodeType_B21 typeVar = new NodeType_B21();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, false, };
		public static AttributeType AttributeType_b21;
		public NodeType_B21() : base((int) NodeTypes.@B21)
		{
			AttributeType_b21 = new AttributeType("b21", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "B21"; } }
		public override INode CreateNode() { return new Node_B21(); }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_b21;
				yield return NodeType_A2.AttributeType_a2;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "b21" : return AttributeType_b21;
				case "a2" : return NodeType_A2.AttributeType_a2;
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
			Node_B21 newNode = new Node_B21();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@B23:
				case (int) NodeTypes.@C221:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B21:
					// copy attributes for: B21
					{
						INode_B21 old = (INode_B21) oldNode;
						newNode.@b21 = old.@b21;
						newNode.@a2 = old.@a2;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node B22 ***

	public interface INode_B22 : INode_A2
	{
		int @b22 { get; set; }
	}

	public sealed class Node_B22 : LGSPNode, INode_B22
	{
		private static int poolLevel = 0;
		private static Node_B22[] pool = new Node_B22[10];
		public Node_B22() : base(NodeType_B22.typeVar)
		{
		}
		public override INode Clone() { return new Node_B22(this); }

		private Node_B22(Node_B22 oldElem) : base(NodeType_B22.typeVar)
		{
			_b22 = oldElem._b22;
			_a2 = oldElem._a2;
		}
		public static Node_B22 CreateNode(LGSPGraph graph)
		{
			Node_B22 node;
			if(poolLevel == 0)
				node = new Node_B22();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b22 = 0;
				node.@a2 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_B22 CreateNode(LGSPGraph graph, String varName)
		{
			Node_B22 node;
			if(poolLevel == 0)
				node = new Node_B22();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b22 = 0;
				node.@a2 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _b22;
		public int @b22
		{
			get { return _b22; }
			set { _b22 = value; }
		}

		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "b22": return @b22;
				case "a2": return @a2;
			}
			throw new NullReferenceException(
				"The node type \"B22\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "b22": @b22 = (int) value; return;
				case "a2": @a2 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B22\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@b22 = 0;
			this.@a2 = 0;
		}
	}

	public sealed class NodeType_B22 : NodeType
	{
		public static NodeType_B22 typeVar = new NodeType_B22();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, false, false, false, false, true, true, false, false, true, true, false, };
		public static AttributeType AttributeType_b22;
		public NodeType_B22() : base((int) NodeTypes.@B22)
		{
			AttributeType_b22 = new AttributeType("b22", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "B22"; } }
		public override INode CreateNode() { return new Node_B22(); }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_b22;
				yield return NodeType_A2.AttributeType_a2;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "b22" : return AttributeType_b22;
				case "a2" : return NodeType_A2.AttributeType_a2;
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
			Node_B22 newNode = new Node_B22();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B23:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@C221:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: B22
					{
						INode_B22 old = (INode_B22) oldNode;
						newNode.@b22 = old.@b22;
						newNode.@a2 = old.@a2;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node B23 ***

	public interface INode_B23 : INode_A2
	{
		int @b23 { get; set; }
	}

	public sealed class Node_B23 : LGSPNode, INode_B23
	{
		private static int poolLevel = 0;
		private static Node_B23[] pool = new Node_B23[10];
		public Node_B23() : base(NodeType_B23.typeVar)
		{
		}
		public override INode Clone() { return new Node_B23(this); }

		private Node_B23(Node_B23 oldElem) : base(NodeType_B23.typeVar)
		{
			_b23 = oldElem._b23;
			_a2 = oldElem._a2;
		}
		public static Node_B23 CreateNode(LGSPGraph graph)
		{
			Node_B23 node;
			if(poolLevel == 0)
				node = new Node_B23();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b23 = 0;
				node.@a2 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_B23 CreateNode(LGSPGraph graph, String varName)
		{
			Node_B23 node;
			if(poolLevel == 0)
				node = new Node_B23();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b23 = 0;
				node.@a2 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _b23;
		public int @b23
		{
			get { return _b23; }
			set { _b23 = value; }
		}

		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "b23": return @b23;
				case "a2": return @a2;
			}
			throw new NullReferenceException(
				"The node type \"B23\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "b23": @b23 = (int) value; return;
				case "a2": @a2 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B23\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@b23 = 0;
			this.@a2 = 0;
		}
	}

	public sealed class NodeType_B23 : NodeType
	{
		public static NodeType_B23 typeVar = new NodeType_B23();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, true, false, false, false, false, false, false, false, false, false, true, };
		public static AttributeType AttributeType_b23;
		public NodeType_B23() : base((int) NodeTypes.@B23)
		{
			AttributeType_b23 = new AttributeType("b23", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "B23"; } }
		public override INode CreateNode() { return new Node_B23(); }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_b23;
				yield return NodeType_A2.AttributeType_a2;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "b23" : return AttributeType_b23;
				case "a2" : return NodeType_A2.AttributeType_a2;
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
			Node_B23 newNode = new Node_B23();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@C221:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B23:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: B23
					{
						INode_B23 old = (INode_B23) oldNode;
						newNode.@b23 = old.@b23;
						newNode.@a2 = old.@a2;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node B41 ***

	public interface INode_B41 : INode_A4
	{
		int @b41 { get; set; }
	}

	public sealed class Node_B41 : LGSPNode, INode_B41
	{
		private static int poolLevel = 0;
		private static Node_B41[] pool = new Node_B41[10];
		public Node_B41() : base(NodeType_B41.typeVar)
		{
		}
		public override INode Clone() { return new Node_B41(this); }

		private Node_B41(Node_B41 oldElem) : base(NodeType_B41.typeVar)
		{
			_b41 = oldElem._b41;
			_a4 = oldElem._a4;
		}
		public static Node_B41 CreateNode(LGSPGraph graph)
		{
			Node_B41 node;
			if(poolLevel == 0)
				node = new Node_B41();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b41 = 0;
				node.@a4 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_B41 CreateNode(LGSPGraph graph, String varName)
		{
			Node_B41 node;
			if(poolLevel == 0)
				node = new Node_B41();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b41 = 0;
				node.@a4 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _b41;
		public int @b41
		{
			get { return _b41; }
			set { _b41 = value; }
		}

		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "b41": return @b41;
				case "a4": return @a4;
			}
			throw new NullReferenceException(
				"The node type \"B41\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "b41": @b41 = (int) value; return;
				case "a4": @a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B41\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@b41 = 0;
			this.@a4 = 0;
		}
	}

	public sealed class NodeType_B41 : NodeType
	{
		public static NodeType_B41 typeVar = new NodeType_B41();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, true, false, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, true, false, false, false, true, true, false, true, true, true, };
		public static AttributeType AttributeType_b41;
		public NodeType_B41() : base((int) NodeTypes.@B41)
		{
			AttributeType_b41 = new AttributeType("b41", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "B41"; } }
		public override INode CreateNode() { return new Node_B41(); }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_b41;
				yield return NodeType_A4.AttributeType_a4;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "b41" : return AttributeType_b41;
				case "a4" : return NodeType_A4.AttributeType_a4;
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
			Node_B41 newNode = new Node_B41();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B42:
				case (int) NodeTypes.@B43:
				case (int) NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@C412_421_431_51:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node B42 ***

	public interface INode_B42 : INode_A4
	{
		int @b42 { get; set; }
	}

	public sealed class Node_B42 : LGSPNode, INode_B42
	{
		private static int poolLevel = 0;
		private static Node_B42[] pool = new Node_B42[10];
		public Node_B42() : base(NodeType_B42.typeVar)
		{
		}
		public override INode Clone() { return new Node_B42(this); }

		private Node_B42(Node_B42 oldElem) : base(NodeType_B42.typeVar)
		{
			_b42 = oldElem._b42;
			_a4 = oldElem._a4;
		}
		public static Node_B42 CreateNode(LGSPGraph graph)
		{
			Node_B42 node;
			if(poolLevel == 0)
				node = new Node_B42();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b42 = 0;
				node.@a4 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_B42 CreateNode(LGSPGraph graph, String varName)
		{
			Node_B42 node;
			if(poolLevel == 0)
				node = new Node_B42();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@b42 = 0;
				node.@a4 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _b42;
		public int @b42
		{
			get { return _b42; }
			set { _b42 = value; }
		}

		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "b42": return @b42;
				case "a4": return @a4;
			}
			throw new NullReferenceException(
				"The node type \"B42\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "b42": @b42 = (int) value; return;
				case "a4": @a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B42\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@b42 = 0;
			this.@a4 = 0;
		}
	}

	public sealed class NodeType_B42 : NodeType
	{
		public static NodeType_B42 typeVar = new NodeType_B42();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, true, false, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, true, false, false, false, true, true, false, false, true, };
		public static AttributeType AttributeType_b42;
		public NodeType_B42() : base((int) NodeTypes.@B42)
		{
			AttributeType_b42 = new AttributeType("b42", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "B42"; } }
		public override INode CreateNode() { return new Node_B42(); }
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_b42;
				yield return NodeType_A4.AttributeType_a4;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "b42" : return AttributeType_b42;
				case "a4" : return NodeType_A4.AttributeType_a4;
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
			Node_B42 newNode = new Node_B42();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@B43:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B42:
				case (int) NodeTypes.@C412_421_431_51:
				case (int) NodeTypes.@C432_422:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: B42
					{
						INode_B42 old = (INode_B42) oldNode;
						newNode.@b42 = old.@b42;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node B43 ***

	public interface INode_B43 : INode_A4
	{
	}

	public sealed class Node_B43 : LGSPNode, INode_B43
	{
		private static int poolLevel = 0;
		private static Node_B43[] pool = new Node_B43[10];
		public Node_B43() : base(NodeType_B43.typeVar)
		{
		}
		public override INode Clone() { return new Node_B43(this); }

		private Node_B43(Node_B43 oldElem) : base(NodeType_B43.typeVar)
		{
			_a4 = oldElem._a4;
		}
		public static Node_B43 CreateNode(LGSPGraph graph)
		{
			Node_B43 node;
			if(poolLevel == 0)
				node = new Node_B43();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a4 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_B43 CreateNode(LGSPGraph graph, String varName)
		{
			Node_B43 node;
			if(poolLevel == 0)
				node = new Node_B43();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a4 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return @a4;
			}
			throw new NullReferenceException(
				"The node type \"B43\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": @a4 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"B43\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@a4 = 0;
		}
	}

	public sealed class NodeType_B43 : NodeType
	{
		public static NodeType_B43 typeVar = new NodeType_B43();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, false, true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, true, false, false, true, };
		public NodeType_B43() : base((int) NodeTypes.@B43)
		{
		}
		public override String Name { get { return "B43"; } }
		public override INode CreateNode() { return new Node_B43(); }
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return NodeType_A4.AttributeType_a4;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "a4" : return NodeType_A4.AttributeType_a4;
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
			Node_B43 newNode = new Node_B43();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@B42:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B43:
				case (int) NodeTypes.@C412_421_431_51:
				case (int) NodeTypes.@C432_422:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: B43
					{
						INode_B43 old = (INode_B43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node C221 ***

	public interface INode_C221 : INode_B22
	{
		int @c221 { get; set; }
	}

	public sealed class Node_C221 : LGSPNode, INode_C221
	{
		private static int poolLevel = 0;
		private static Node_C221[] pool = new Node_C221[10];
		public Node_C221() : base(NodeType_C221.typeVar)
		{
		}
		public override INode Clone() { return new Node_C221(this); }

		private Node_C221(Node_C221 oldElem) : base(NodeType_C221.typeVar)
		{
			_c221 = oldElem._c221;
			_a2 = oldElem._a2;
			_b22 = oldElem._b22;
		}
		public static Node_C221 CreateNode(LGSPGraph graph)
		{
			Node_C221 node;
			if(poolLevel == 0)
				node = new Node_C221();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@c221 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_C221 CreateNode(LGSPGraph graph, String varName)
		{
			Node_C221 node;
			if(poolLevel == 0)
				node = new Node_C221();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@c221 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _c221;
		public int @c221
		{
			get { return _c221; }
			set { _c221 = value; }
		}

		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}

		private int _b22;
		public int @b22
		{
			get { return _b22; }
			set { _b22 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "c221": return @c221;
				case "a2": return @a2;
				case "b22": return @b22;
			}
			throw new NullReferenceException(
				"The node type \"C221\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "c221": @c221 = (int) value; return;
				case "a2": @a2 = (int) value; return;
				case "b22": @b22 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"C221\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@c221 = 0;
			this.@a2 = 0;
			this.@b22 = 0;
		}
	}

	public sealed class NodeType_C221 : NodeType
	{
		public static NodeType_C221 typeVar = new NodeType_C221();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, true, false, false, false, false, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, false, true, false, };
		public static AttributeType AttributeType_c221;
		public NodeType_C221() : base((int) NodeTypes.@C221)
		{
			AttributeType_c221 = new AttributeType("c221", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "C221"; } }
		public override INode CreateNode() { return new Node_C221(); }
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_c221;
				yield return NodeType_A2.AttributeType_a2;
				yield return NodeType_B22.AttributeType_b22;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "c221" : return AttributeType_c221;
				case "a2" : return NodeType_A2.AttributeType_a2;
				case "b22" : return NodeType_B22.AttributeType_b22;
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
			Node_C221 newNode = new Node_C221();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B23:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
					// copy attributes for: B22
					{
						INode_B22 old = (INode_B22) oldNode;
						newNode.@b22 = old.@b22;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@C221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: C221
					{
						INode_C221 old = (INode_C221) oldNode;
						newNode.@c221 = old.@c221;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node C222_411 ***

	public interface INode_C222_411 : INode_B22, INode_B41
	{
		int @c222_411 { get; set; }
	}

	public sealed class Node_C222_411 : LGSPNode, INode_C222_411
	{
		private static int poolLevel = 0;
		private static Node_C222_411[] pool = new Node_C222_411[10];
		public Node_C222_411() : base(NodeType_C222_411.typeVar)
		{
		}
		public override INode Clone() { return new Node_C222_411(this); }

		private Node_C222_411(Node_C222_411 oldElem) : base(NodeType_C222_411.typeVar)
		{
			_c222_411 = oldElem._c222_411;
			_a2 = oldElem._a2;
			_b22 = oldElem._b22;
			_a4 = oldElem._a4;
			_b41 = oldElem._b41;
		}
		public static Node_C222_411 CreateNode(LGSPGraph graph)
		{
			Node_C222_411 node;
			if(poolLevel == 0)
				node = new Node_C222_411();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@c222_411 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_C222_411 CreateNode(LGSPGraph graph, String varName)
		{
			Node_C222_411 node;
			if(poolLevel == 0)
				node = new Node_C222_411();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@c222_411 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _c222_411;
		public int @c222_411
		{
			get { return _c222_411; }
			set { _c222_411 = value; }
		}

		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}

		private int _b22;
		public int @b22
		{
			get { return _b22; }
			set { _b22 = value; }
		}

		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}

		private int _b41;
		public int @b41
		{
			get { return _b41; }
			set { _b41 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "c222_411": return @c222_411;
				case "a2": return @a2;
				case "b22": return @b22;
				case "a4": return @a4;
				case "b41": return @b41;
			}
			throw new NullReferenceException(
				"The node type \"C222_411\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "c222_411": @c222_411 = (int) value; return;
				case "a2": @a2 = (int) value; return;
				case "b22": @b22 = (int) value; return;
				case "a4": @a4 = (int) value; return;
				case "b41": @b41 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"C222_411\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@c222_411 = 0;
			this.@a2 = 0;
			this.@b22 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
		}
	}

	public sealed class NodeType_C222_411 : NodeType
	{
		public static NodeType_C222_411 typeVar = new NodeType_C222_411();
		public static bool[] isA = new bool[] { true, false, true, false, true, false, false, true, false, true, false, false, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, true, true, false, };
		public static AttributeType AttributeType_c222_411;
		public NodeType_C222_411() : base((int) NodeTypes.@C222_411)
		{
			AttributeType_c222_411 = new AttributeType("c222_411", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "C222_411"; } }
		public override INode CreateNode() { return new Node_C222_411(); }
		public override int NumAttributes { get { return 5; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_c222_411;
				yield return NodeType_A2.AttributeType_a2;
				yield return NodeType_B22.AttributeType_b22;
				yield return NodeType_A4.AttributeType_a4;
				yield return NodeType_B41.AttributeType_b41;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "c222_411" : return AttributeType_c222_411;
				case "a2" : return NodeType_A2.AttributeType_a2;
				case "b22" : return NodeType_B22.AttributeType_b22;
				case "a4" : return NodeType_A4.AttributeType_a4;
				case "b41" : return NodeType_B41.AttributeType_b41;
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
			Node_C222_411 newNode = new Node_C222_411();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B23:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B42:
				case (int) NodeTypes.@B43:
				case (int) NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@C221:
					// copy attributes for: B22
					{
						INode_B22 old = (INode_B22) oldNode;
						newNode.@b22 = old.@b22;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@C412_421_431_51:
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: C222_411
					{
						INode_C222_411 old = (INode_C222_411) oldNode;
						newNode.@c222_411 = old.@c222_411;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node C412_421_431_51 ***

	public interface INode_C412_421_431_51 : INode_B41, INode_B42, INode_B43, INode_A5
	{
	}

	public sealed class Node_C412_421_431_51 : LGSPNode, INode_C412_421_431_51
	{
		private static int poolLevel = 0;
		private static Node_C412_421_431_51[] pool = new Node_C412_421_431_51[10];
		public Node_C412_421_431_51() : base(NodeType_C412_421_431_51.typeVar)
		{
		}
		public override INode Clone() { return new Node_C412_421_431_51(this); }

		private Node_C412_421_431_51(Node_C412_421_431_51 oldElem) : base(NodeType_C412_421_431_51.typeVar)
		{
			_a4 = oldElem._a4;
			_b41 = oldElem._b41;
			_b42 = oldElem._b42;
			_a5 = oldElem._a5;
		}
		public static Node_C412_421_431_51 CreateNode(LGSPGraph graph)
		{
			Node_C412_421_431_51 node;
			if(poolLevel == 0)
				node = new Node_C412_421_431_51();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_C412_421_431_51 CreateNode(LGSPGraph graph, String varName)
		{
			Node_C412_421_431_51 node;
			if(poolLevel == 0)
				node = new Node_C412_421_431_51();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}

		private int _b41;
		public int @b41
		{
			get { return _b41; }
			set { _b41 = value; }
		}

		private int _b42;
		public int @b42
		{
			get { return _b42; }
			set { _b42 = value; }
		}

		private int _a5;
		public int @a5
		{
			get { return _a5; }
			set { _a5 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "a4": return @a4;
				case "b41": return @b41;
				case "b42": return @b42;
				case "a5": return @a5;
			}
			throw new NullReferenceException(
				"The node type \"C412_421_431_51\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "a4": @a4 = (int) value; return;
				case "b41": @b41 = (int) value; return;
				case "b42": @b42 = (int) value; return;
				case "a5": @a5 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"C412_421_431_51\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@a4 = 0;
			this.@b41 = 0;
			this.@b42 = 0;
			this.@a5 = 0;
		}
	}

	public sealed class NodeType_C412_421_431_51 : NodeType
	{
		public static NodeType_C412_421_431_51 typeVar = new NodeType_C412_421_431_51();
		public static bool[] isA = new bool[] { true, false, false, false, true, true, false, false, false, true, true, true, false, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, true, };
		public NodeType_C412_421_431_51() : base((int) NodeTypes.@C412_421_431_51)
		{
		}
		public override String Name { get { return "C412_421_431_51"; } }
		public override INode CreateNode() { return new Node_C412_421_431_51(); }
		public override int NumAttributes { get { return 4; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return NodeType_A4.AttributeType_a4;
				yield return NodeType_B41.AttributeType_b41;
				yield return NodeType_B42.AttributeType_b42;
				yield return NodeType_A5.AttributeType_a5;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "a4" : return NodeType_A4.AttributeType_a4;
				case "b41" : return NodeType_B41.AttributeType_b41;
				case "b42" : return NodeType_B42.AttributeType_b42;
				case "a5" : return NodeType_A5.AttributeType_a5;
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
			Node_C412_421_431_51 newNode = new Node_C412_421_431_51();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A4:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@A5:
					// copy attributes for: A5
					{
						INode_A5 old = (INode_A5) oldNode;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B42:
					// copy attributes for: B42
					{
						INode_B42 old = (INode_B42) oldNode;
						newNode.@b42 = old.@b42;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B43:
					// copy attributes for: B43
					{
						INode_B43 old = (INode_B43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@C412_421_431_51:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: C412_421_431_51
					{
						INode_C412_421_431_51 old = (INode_C412_421_431_51) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@b42 = old.@b42;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) NodeTypes.@C432_422:
					// copy attributes for: B42
					{
						INode_B42 old = (INode_B42) oldNode;
						newNode.@b42 = old.@b42;
						newNode.@a4 = old.@a4;
					}
					// copy attributes for: B43
						// already copied: a4
					break;
			}
			return newNode;
		}

	}

	// *** Node C432_422 ***

	public interface INode_C432_422 : INode_B43, INode_B42
	{
		int @c432_422 { get; set; }
	}

	public sealed class Node_C432_422 : LGSPNode, INode_C432_422
	{
		private static int poolLevel = 0;
		private static Node_C432_422[] pool = new Node_C432_422[10];
		public Node_C432_422() : base(NodeType_C432_422.typeVar)
		{
		}
		public override INode Clone() { return new Node_C432_422(this); }

		private Node_C432_422(Node_C432_422 oldElem) : base(NodeType_C432_422.typeVar)
		{
			_c432_422 = oldElem._c432_422;
			_a4 = oldElem._a4;
			_b42 = oldElem._b42;
		}
		public static Node_C432_422 CreateNode(LGSPGraph graph)
		{
			Node_C432_422 node;
			if(poolLevel == 0)
				node = new Node_C432_422();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@c432_422 = 0;
				node.@a4 = 0;
				node.@b42 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_C432_422 CreateNode(LGSPGraph graph, String varName)
		{
			Node_C432_422 node;
			if(poolLevel == 0)
				node = new Node_C432_422();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@c432_422 = 0;
				node.@a4 = 0;
				node.@b42 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _c432_422;
		public int @c432_422
		{
			get { return _c432_422; }
			set { _c432_422 = value; }
		}

		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}

		private int _b42;
		public int @b42
		{
			get { return _b42; }
			set { _b42 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "c432_422": return @c432_422;
				case "a4": return @a4;
				case "b42": return @b42;
			}
			throw new NullReferenceException(
				"The node type \"C432_422\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "c432_422": @c432_422 = (int) value; return;
				case "a4": @a4 = (int) value; return;
				case "b42": @b42 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"C432_422\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@c432_422 = 0;
			this.@a4 = 0;
			this.@b42 = 0;
		}
	}

	public sealed class NodeType_C432_422 : NodeType
	{
		public static NodeType_C432_422 typeVar = new NodeType_C432_422();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, false, false, false, true, true, false, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, false, };
		public static AttributeType AttributeType_c432_422;
		public NodeType_C432_422() : base((int) NodeTypes.@C432_422)
		{
			AttributeType_c432_422 = new AttributeType("c432_422", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "C432_422"; } }
		public override INode CreateNode() { return new Node_C432_422(); }
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_c432_422;
				yield return NodeType_A4.AttributeType_a4;
				yield return NodeType_B42.AttributeType_b42;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "c432_422" : return AttributeType_c432_422;
				case "a4" : return NodeType_A4.AttributeType_a4;
				case "b42" : return NodeType_B42.AttributeType_b42;
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
			Node_C432_422 newNode = new Node_C432_422();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B42:
					// copy attributes for: B42
					{
						INode_B42 old = (INode_B42) oldNode;
						newNode.@b42 = old.@b42;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B43:
					// copy attributes for: B43
					{
						INode_B43 old = (INode_B43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@C412_421_431_51:
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: B42
					{
						INode_B42 old = (INode_B42) oldNode;
						newNode.@b42 = old.@b42;
						newNode.@a4 = old.@a4;
					}
					// copy attributes for: B43
						// already copied: a4
					break;
				case (int) NodeTypes.@C432_422:
					// copy attributes for: C432_422
					{
						INode_C432_422 old = (INode_C432_422) oldNode;
						newNode.@c432_422 = old.@c432_422;
						newNode.@a4 = old.@a4;
						newNode.@b42 = old.@b42;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node D11_2221 ***

	public interface INode_D11_2221 : INode_A1, INode_C222_411
	{
		int @d11_2221 { get; set; }
	}

	public sealed class Node_D11_2221 : LGSPNode, INode_D11_2221
	{
		private static int poolLevel = 0;
		private static Node_D11_2221[] pool = new Node_D11_2221[10];
		public Node_D11_2221() : base(NodeType_D11_2221.typeVar)
		{
		}
		public override INode Clone() { return new Node_D11_2221(this); }

		private Node_D11_2221(Node_D11_2221 oldElem) : base(NodeType_D11_2221.typeVar)
		{
			_d11_2221 = oldElem._d11_2221;
			_a1 = oldElem._a1;
			_a2 = oldElem._a2;
			_b22 = oldElem._b22;
			_a4 = oldElem._a4;
			_b41 = oldElem._b41;
			_c222_411 = oldElem._c222_411;
		}
		public static Node_D11_2221 CreateNode(LGSPGraph graph)
		{
			Node_D11_2221 node;
			if(poolLevel == 0)
				node = new Node_D11_2221();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@d11_2221 = 0;
				node.@a1 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_D11_2221 CreateNode(LGSPGraph graph, String varName)
		{
			Node_D11_2221 node;
			if(poolLevel == 0)
				node = new Node_D11_2221();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@d11_2221 = 0;
				node.@a1 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _d11_2221;
		public int @d11_2221
		{
			get { return _d11_2221; }
			set { _d11_2221 = value; }
		}

		private int _a1;
		public int @a1
		{
			get { return _a1; }
			set { _a1 = value; }
		}

		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}

		private int _b22;
		public int @b22
		{
			get { return _b22; }
			set { _b22 = value; }
		}

		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}

		private int _b41;
		public int @b41
		{
			get { return _b41; }
			set { _b41 = value; }
		}

		private int _c222_411;
		public int @c222_411
		{
			get { return _c222_411; }
			set { _c222_411 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "d11_2221": return @d11_2221;
				case "a1": return @a1;
				case "a2": return @a2;
				case "b22": return @b22;
				case "a4": return @a4;
				case "b41": return @b41;
				case "c222_411": return @c222_411;
			}
			throw new NullReferenceException(
				"The node type \"D11_2221\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "d11_2221": @d11_2221 = (int) value; return;
				case "a1": @a1 = (int) value; return;
				case "a2": @a2 = (int) value; return;
				case "b22": @b22 = (int) value; return;
				case "a4": @a4 = (int) value; return;
				case "b41": @b41 = (int) value; return;
				case "c222_411": @c222_411 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"D11_2221\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@d11_2221 = 0;
			this.@a1 = 0;
			this.@a2 = 0;
			this.@b22 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
			this.@c222_411 = 0;
		}
	}

	public sealed class NodeType_D11_2221 : NodeType
	{
		public static NodeType_D11_2221 typeVar = new NodeType_D11_2221();
		public static bool[] isA = new bool[] { true, true, true, false, true, false, false, true, false, true, false, false, false, true, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, false, };
		public static AttributeType AttributeType_d11_2221;
		public NodeType_D11_2221() : base((int) NodeTypes.@D11_2221)
		{
			AttributeType_d11_2221 = new AttributeType("d11_2221", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "D11_2221"; } }
		public override INode CreateNode() { return new Node_D11_2221(); }
		public override int NumAttributes { get { return 7; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_d11_2221;
				yield return NodeType_A1.AttributeType_a1;
				yield return NodeType_A2.AttributeType_a2;
				yield return NodeType_B22.AttributeType_b22;
				yield return NodeType_A4.AttributeType_a4;
				yield return NodeType_B41.AttributeType_b41;
				yield return NodeType_C222_411.AttributeType_c222_411;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "d11_2221" : return AttributeType_d11_2221;
				case "a1" : return NodeType_A1.AttributeType_a1;
				case "a2" : return NodeType_A2.AttributeType_a2;
				case "b22" : return NodeType_B22.AttributeType_b22;
				case "a4" : return NodeType_A4.AttributeType_a4;
				case "b41" : return NodeType_B41.AttributeType_b41;
				case "c222_411" : return NodeType_C222_411.AttributeType_c222_411;
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
			Node_D11_2221 newNode = new Node_D11_2221();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A1:
					// copy attributes for: A1
					{
						INode_A1 old = (INode_A1) oldNode;
						newNode.@a1 = old.@a1;
					}
					break;
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B23:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B42:
				case (int) NodeTypes.@B43:
				case (int) NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@C221:
					// copy attributes for: B22
					{
						INode_B22 old = (INode_B22) oldNode;
						newNode.@b22 = old.@b22;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@C412_421_431_51:
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: C222_411
					{
						INode_C222_411 old = (INode_C222_411) oldNode;
						newNode.@c222_411 = old.@c222_411;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) NodeTypes.@D11_2221:
					// copy attributes for: D11_2221
					{
						INode_D11_2221 old = (INode_D11_2221) oldNode;
						newNode.@d11_2221 = old.@d11_2221;
						newNode.@a1 = old.@a1;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@c222_411 = old.@c222_411;
					}
					break;
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node D2211_2222_31 ***

	public interface INode_D2211_2222_31 : INode_C221, INode_C222_411, INode_A3
	{
		int @d2211_2222_31 { get; set; }
	}

	public sealed class Node_D2211_2222_31 : LGSPNode, INode_D2211_2222_31
	{
		private static int poolLevel = 0;
		private static Node_D2211_2222_31[] pool = new Node_D2211_2222_31[10];
		public Node_D2211_2222_31() : base(NodeType_D2211_2222_31.typeVar)
		{
		}
		public override INode Clone() { return new Node_D2211_2222_31(this); }

		private Node_D2211_2222_31(Node_D2211_2222_31 oldElem) : base(NodeType_D2211_2222_31.typeVar)
		{
			_d2211_2222_31 = oldElem._d2211_2222_31;
			_a2 = oldElem._a2;
			_b22 = oldElem._b22;
			_c221 = oldElem._c221;
			_a4 = oldElem._a4;
			_b41 = oldElem._b41;
			_c222_411 = oldElem._c222_411;
			_a3 = oldElem._a3;
		}
		public static Node_D2211_2222_31 CreateNode(LGSPGraph graph)
		{
			Node_D2211_2222_31 node;
			if(poolLevel == 0)
				node = new Node_D2211_2222_31();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@d2211_2222_31 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@c221 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				node.@a3 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_D2211_2222_31 CreateNode(LGSPGraph graph, String varName)
		{
			Node_D2211_2222_31 node;
			if(poolLevel == 0)
				node = new Node_D2211_2222_31();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@d2211_2222_31 = 0;
				node.@a2 = 0;
				node.@b22 = 0;
				node.@c221 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@c222_411 = 0;
				node.@a3 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int _d2211_2222_31;
		public int @d2211_2222_31
		{
			get { return _d2211_2222_31; }
			set { _d2211_2222_31 = value; }
		}

		private int _a2;
		public int @a2
		{
			get { return _a2; }
			set { _a2 = value; }
		}

		private int _b22;
		public int @b22
		{
			get { return _b22; }
			set { _b22 = value; }
		}

		private int _c221;
		public int @c221
		{
			get { return _c221; }
			set { _c221 = value; }
		}

		private int _a4;
		public int @a4
		{
			get { return _a4; }
			set { _a4 = value; }
		}

		private int _b41;
		public int @b41
		{
			get { return _b41; }
			set { _b41 = value; }
		}

		private int _c222_411;
		public int @c222_411
		{
			get { return _c222_411; }
			set { _c222_411 = value; }
		}

		private int _a3;
		public int @a3
		{
			get { return _a3; }
			set { _a3 = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "d2211_2222_31": return @d2211_2222_31;
				case "a2": return @a2;
				case "b22": return @b22;
				case "c221": return @c221;
				case "a4": return @a4;
				case "b41": return @b41;
				case "c222_411": return @c222_411;
				case "a3": return @a3;
			}
			throw new NullReferenceException(
				"The node type \"D2211_2222_31\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "d2211_2222_31": @d2211_2222_31 = (int) value; return;
				case "a2": @a2 = (int) value; return;
				case "b22": @b22 = (int) value; return;
				case "c221": @c221 = (int) value; return;
				case "a4": @a4 = (int) value; return;
				case "b41": @b41 = (int) value; return;
				case "c222_411": @c222_411 = (int) value; return;
				case "a3": @a3 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"D2211_2222_31\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@d2211_2222_31 = 0;
			this.@a2 = 0;
			this.@b22 = 0;
			this.@c221 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
			this.@c222_411 = 0;
			this.@a3 = 0;
		}
	}

	public sealed class NodeType_D2211_2222_31 : NodeType
	{
		public static NodeType_D2211_2222_31 typeVar = new NodeType_D2211_2222_31();
		public static bool[] isA = new bool[] { true, false, true, true, true, false, false, true, false, true, false, false, true, true, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, false, };
		public static AttributeType AttributeType_d2211_2222_31;
		public NodeType_D2211_2222_31() : base((int) NodeTypes.@D2211_2222_31)
		{
			AttributeType_d2211_2222_31 = new AttributeType("d2211_2222_31", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "D2211_2222_31"; } }
		public override INode CreateNode() { return new Node_D2211_2222_31(); }
		public override int NumAttributes { get { return 8; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_d2211_2222_31;
				yield return NodeType_A2.AttributeType_a2;
				yield return NodeType_B22.AttributeType_b22;
				yield return NodeType_C221.AttributeType_c221;
				yield return NodeType_A4.AttributeType_a4;
				yield return NodeType_B41.AttributeType_b41;
				yield return NodeType_C222_411.AttributeType_c222_411;
				yield return NodeType_A3.AttributeType_a3;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "d2211_2222_31" : return AttributeType_d2211_2222_31;
				case "a2" : return NodeType_A2.AttributeType_a2;
				case "b22" : return NodeType_B22.AttributeType_b22;
				case "c221" : return NodeType_C221.AttributeType_c221;
				case "a4" : return NodeType_A4.AttributeType_a4;
				case "b41" : return NodeType_B41.AttributeType_b41;
				case "c222_411" : return NodeType_C222_411.AttributeType_c222_411;
				case "a3" : return NodeType_A3.AttributeType_a3;
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
			Node_D2211_2222_31 newNode = new Node_D2211_2222_31();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B23:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@A3:
					// copy attributes for: A3
					{
						INode_A3 old = (INode_A3) oldNode;
						newNode.@a3 = old.@a3;
					}
					break;
				case (int) NodeTypes.@A4:
				case (int) NodeTypes.@B42:
				case (int) NodeTypes.@B43:
				case (int) NodeTypes.@C432_422:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B22:
					// copy attributes for: B22
					{
						INode_B22 old = (INode_B22) oldNode;
						newNode.@b22 = old.@b22;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B41:
				case (int) NodeTypes.@C412_421_431_51:
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@C221:
					// copy attributes for: C221
					{
						INode_C221 old = (INode_C221) oldNode;
						newNode.@c221 = old.@c221;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
					}
					break;
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
					// copy attributes for: C222_411
					{
						INode_C222_411 old = (INode_C222_411) oldNode;
						newNode.@c222_411 = old.@c222_411;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
					}
					break;
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: D2211_2222_31
					{
						INode_D2211_2222_31 old = (INode_D2211_2222_31) oldNode;
						newNode.@d2211_2222_31 = old.@d2211_2222_31;
						newNode.@a2 = old.@a2;
						newNode.@b22 = old.@b22;
						newNode.@c221 = old.@c221;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@c222_411 = old.@c222_411;
						newNode.@a3 = old.@a3;
					}
					break;
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node D231_4121 ***

	public interface INode_D231_4121 : INode_B23, INode_C412_421_431_51
	{
		int @d231_4121 { get; set; }
	}

	public abstract class Node_D231_4121 : LGSPNode, INode_D231_4121
	{
		private static int poolLevel = 0;
		private static Node_D231_4121[] pool = new Node_D231_4121[10];
		public Node_D231_4121() : base(NodeType_D231_4121.typeVar)
		{
		}
		public static Node_D231_4121 CreateNode(LGSPGraph graph)
		{
			Node_D231_4121 node;
			if(poolLevel == 0)
				node = new global::test.D231_4121_Impl();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@d231_4121 = 0;
				node.@a2 = 0;
				node.@b23 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
			}
			graph.AddNode(node);
			return node;
		}

		public static Node_D231_4121 CreateNode(LGSPGraph graph, String varName)
		{
			Node_D231_4121 node;
			if(poolLevel == 0)
				node = new global::test.D231_4121_Impl();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.hasVariables = false;
				node.@d231_4121 = 0;
				node.@a2 = 0;
				node.@b23 = 0;
				node.@a4 = 0;
				node.@b41 = 0;
				node.@b42 = 0;
				node.@a5 = 0;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}

		public abstract int @d231_4121 { get; set; }
		public abstract int @a2 { get; set; }
		public abstract int @b23 { get; set; }
		public abstract int @a4 { get; set; }
		public abstract int @b41 { get; set; }
		public abstract int @b42 { get; set; }
		public abstract int @a5 { get; set; }
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "d231_4121": return @d231_4121;
				case "a2": return @a2;
				case "b23": return @b23;
				case "a4": return @a4;
				case "b41": return @b41;
				case "b42": return @b42;
				case "a5": return @a5;
			}
			throw new NullReferenceException(
				"The node type \"D231_4121\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "d231_4121": @d231_4121 = (int) value; return;
				case "a2": @a2 = (int) value; return;
				case "b23": @b23 = (int) value; return;
				case "a4": @a4 = (int) value; return;
				case "b41": @b41 = (int) value; return;
				case "b42": @b42 = (int) value; return;
				case "a5": @a5 = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"D231_4121\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			this.@d231_4121 = 0;
			this.@a2 = 0;
			this.@b23 = 0;
			this.@a4 = 0;
			this.@b41 = 0;
			this.@b42 = 0;
			this.@a5 = 0;
		}
	}

	public sealed class NodeType_D231_4121 : NodeType
	{
		public static NodeType_D231_4121 typeVar = new NodeType_D231_4121();
		public static bool[] isA = new bool[] { true, false, true, false, true, true, false, false, true, true, true, true, false, false, true, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true, };
		public static AttributeType AttributeType_d231_4121;
		public NodeType_D231_4121() : base((int) NodeTypes.@D231_4121)
		{
			AttributeType_d231_4121 = new AttributeType("d231_4121", this, AttributeKind.IntegerAttr, null);
		}
		public override String Name { get { return "D231_4121"; } }
		public override INode CreateNode() { return new global::test.D231_4121_Impl(); }
		public override int NumAttributes { get { return 7; } }
		public override IEnumerable<AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_d231_4121;
				yield return NodeType_A2.AttributeType_a2;
				yield return NodeType_B23.AttributeType_b23;
				yield return NodeType_A4.AttributeType_a4;
				yield return NodeType_B41.AttributeType_b41;
				yield return NodeType_B42.AttributeType_b42;
				yield return NodeType_A5.AttributeType_a5;
			}
		}
		public override AttributeType GetAttributeType(String name)
		{
			switch(name)
			{
				case "d231_4121" : return AttributeType_d231_4121;
				case "a2" : return NodeType_A2.AttributeType_a2;
				case "b23" : return NodeType_B23.AttributeType_b23;
				case "a4" : return NodeType_A4.AttributeType_a4;
				case "b41" : return NodeType_B41.AttributeType_b41;
				case "b42" : return NodeType_B42.AttributeType_b42;
				case "a5" : return NodeType_A5.AttributeType_a5;
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
			Node_D231_4121 newNode = new global::test.D231_4121_Impl();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@A2:
				case (int) NodeTypes.@B21:
				case (int) NodeTypes.@B22:
				case (int) NodeTypes.@C221:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@A4:
					// copy attributes for: A4
					{
						INode_A4 old = (INode_A4) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@A5:
					// copy attributes for: A5
					{
						INode_A5 old = (INode_A5) oldNode;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) NodeTypes.@B23:
					// copy attributes for: B23
					{
						INode_B23 old = (INode_B23) oldNode;
						newNode.@b23 = old.@b23;
						newNode.@a2 = old.@a2;
					}
					break;
				case (int) NodeTypes.@B41:
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B42:
					// copy attributes for: B42
					{
						INode_B42 old = (INode_B42) oldNode;
						newNode.@b42 = old.@b42;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@B43:
					// copy attributes for: B43
					{
						INode_B43 old = (INode_B43) oldNode;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@C222_411:
				case (int) NodeTypes.@D11_2221:
				case (int) NodeTypes.@D2211_2222_31:
					// copy attributes for: A2
					{
						INode_A2 old = (INode_A2) oldNode;
						newNode.@a2 = old.@a2;
					}
					// copy attributes for: B41
					{
						INode_B41 old = (INode_B41) oldNode;
						newNode.@b41 = old.@b41;
						newNode.@a4 = old.@a4;
					}
					break;
				case (int) NodeTypes.@C412_421_431_51:
					// copy attributes for: C412_421_431_51
					{
						INode_C412_421_431_51 old = (INode_C412_421_431_51) oldNode;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@b42 = old.@b42;
						newNode.@a5 = old.@a5;
					}
					break;
				case (int) NodeTypes.@C432_422:
					// copy attributes for: B42
					{
						INode_B42 old = (INode_B42) oldNode;
						newNode.@b42 = old.@b42;
						newNode.@a4 = old.@a4;
					}
					// copy attributes for: B43
						// already copied: a4
					break;
				case (int) NodeTypes.@D231_4121:
					// copy attributes for: D231_4121
					{
						INode_D231_4121 old = (INode_D231_4121) oldNode;
						newNode.@d231_4121 = old.@d231_4121;
						newNode.@a2 = old.@a2;
						newNode.@b23 = old.@b23;
						newNode.@a4 = old.@a4;
						newNode.@b41 = old.@b41;
						newNode.@b42 = old.@b42;
						newNode.@a5 = old.@a5;
					}
					break;
			}
			return newNode;
		}

	}

	//
	// Node model
	//

	public sealed class testNodeModel : INodeModel
	{
		public testNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_A1.typeVar,
				NodeType_A2.typeVar,
				NodeType_A3.typeVar,
				NodeType_A4.typeVar,
				NodeType_A5.typeVar,
				NodeType_B21.typeVar,
				NodeType_B22.typeVar,
				NodeType_B23.typeVar,
				NodeType_B41.typeVar,
				NodeType_B42.typeVar,
				NodeType_B43.typeVar,
				NodeType_C221.typeVar,
				NodeType_C222_411.typeVar,
				NodeType_C412_421_431_51.typeVar,
				NodeType_C432_422.typeVar,
				NodeType_D11_2221.typeVar,
				NodeType_D2211_2222_31.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_A1.typeVar.subOrSameGrGenTypes = NodeType_A1.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_A1.typeVar,
				NodeType_D11_2221.typeVar,
			};
			NodeType_A1.typeVar.superOrSameGrGenTypes = NodeType_A1.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_A1.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_A2.typeVar.subOrSameGrGenTypes = NodeType_A2.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_A2.typeVar,
				NodeType_B21.typeVar,
				NodeType_B22.typeVar,
				NodeType_B23.typeVar,
				NodeType_C221.typeVar,
				NodeType_C222_411.typeVar,
				NodeType_D11_2221.typeVar,
				NodeType_D2211_2222_31.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_A2.typeVar.superOrSameGrGenTypes = NodeType_A2.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_A2.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_A3.typeVar.subOrSameGrGenTypes = NodeType_A3.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_A3.typeVar,
				NodeType_D2211_2222_31.typeVar,
			};
			NodeType_A3.typeVar.superOrSameGrGenTypes = NodeType_A3.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_A3.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_A4.typeVar.subOrSameGrGenTypes = NodeType_A4.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_A4.typeVar,
				NodeType_B41.typeVar,
				NodeType_B42.typeVar,
				NodeType_B43.typeVar,
				NodeType_C222_411.typeVar,
				NodeType_C412_421_431_51.typeVar,
				NodeType_C432_422.typeVar,
				NodeType_D11_2221.typeVar,
				NodeType_D2211_2222_31.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_A4.typeVar.superOrSameGrGenTypes = NodeType_A4.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_A4.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_A5.typeVar.subOrSameGrGenTypes = NodeType_A5.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_A5.typeVar,
				NodeType_C412_421_431_51.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_A5.typeVar.superOrSameGrGenTypes = NodeType_A5.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_A5.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_B21.typeVar.subOrSameGrGenTypes = NodeType_B21.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_B21.typeVar,
			};
			NodeType_B21.typeVar.superOrSameGrGenTypes = NodeType_B21.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_B21.typeVar,
				NodeType_Node.typeVar,
				NodeType_A2.typeVar,
			};
			NodeType_B22.typeVar.subOrSameGrGenTypes = NodeType_B22.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_B22.typeVar,
				NodeType_C221.typeVar,
				NodeType_C222_411.typeVar,
				NodeType_D11_2221.typeVar,
				NodeType_D2211_2222_31.typeVar,
			};
			NodeType_B22.typeVar.superOrSameGrGenTypes = NodeType_B22.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_B22.typeVar,
				NodeType_Node.typeVar,
				NodeType_A2.typeVar,
			};
			NodeType_B23.typeVar.subOrSameGrGenTypes = NodeType_B23.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_B23.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_B23.typeVar.superOrSameGrGenTypes = NodeType_B23.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_B23.typeVar,
				NodeType_Node.typeVar,
				NodeType_A2.typeVar,
			};
			NodeType_B41.typeVar.subOrSameGrGenTypes = NodeType_B41.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_B41.typeVar,
				NodeType_C222_411.typeVar,
				NodeType_C412_421_431_51.typeVar,
				NodeType_D11_2221.typeVar,
				NodeType_D2211_2222_31.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_B41.typeVar.superOrSameGrGenTypes = NodeType_B41.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_B41.typeVar,
				NodeType_Node.typeVar,
				NodeType_A4.typeVar,
			};
			NodeType_B42.typeVar.subOrSameGrGenTypes = NodeType_B42.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_B42.typeVar,
				NodeType_C412_421_431_51.typeVar,
				NodeType_C432_422.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_B42.typeVar.superOrSameGrGenTypes = NodeType_B42.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_B42.typeVar,
				NodeType_Node.typeVar,
				NodeType_A4.typeVar,
			};
			NodeType_B43.typeVar.subOrSameGrGenTypes = NodeType_B43.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_B43.typeVar,
				NodeType_C412_421_431_51.typeVar,
				NodeType_C432_422.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_B43.typeVar.superOrSameGrGenTypes = NodeType_B43.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_B43.typeVar,
				NodeType_Node.typeVar,
				NodeType_A4.typeVar,
			};
			NodeType_C221.typeVar.subOrSameGrGenTypes = NodeType_C221.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_C221.typeVar,
				NodeType_D2211_2222_31.typeVar,
			};
			NodeType_C221.typeVar.superOrSameGrGenTypes = NodeType_C221.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_C221.typeVar,
				NodeType_Node.typeVar,
				NodeType_A2.typeVar,
				NodeType_B22.typeVar,
			};
			NodeType_C222_411.typeVar.subOrSameGrGenTypes = NodeType_C222_411.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_C222_411.typeVar,
				NodeType_D11_2221.typeVar,
				NodeType_D2211_2222_31.typeVar,
			};
			NodeType_C222_411.typeVar.superOrSameGrGenTypes = NodeType_C222_411.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_C222_411.typeVar,
				NodeType_Node.typeVar,
				NodeType_A2.typeVar,
				NodeType_A4.typeVar,
				NodeType_B22.typeVar,
				NodeType_B41.typeVar,
			};
			NodeType_C412_421_431_51.typeVar.subOrSameGrGenTypes = NodeType_C412_421_431_51.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_C412_421_431_51.typeVar,
				NodeType_D231_4121.typeVar,
			};
			NodeType_C412_421_431_51.typeVar.superOrSameGrGenTypes = NodeType_C412_421_431_51.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_C412_421_431_51.typeVar,
				NodeType_Node.typeVar,
				NodeType_A4.typeVar,
				NodeType_A5.typeVar,
				NodeType_B41.typeVar,
				NodeType_B42.typeVar,
				NodeType_B43.typeVar,
			};
			NodeType_C432_422.typeVar.subOrSameGrGenTypes = NodeType_C432_422.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_C432_422.typeVar,
			};
			NodeType_C432_422.typeVar.superOrSameGrGenTypes = NodeType_C432_422.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_C432_422.typeVar,
				NodeType_Node.typeVar,
				NodeType_A4.typeVar,
				NodeType_B42.typeVar,
				NodeType_B43.typeVar,
			};
			NodeType_D11_2221.typeVar.subOrSameGrGenTypes = NodeType_D11_2221.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_D11_2221.typeVar,
			};
			NodeType_D11_2221.typeVar.superOrSameGrGenTypes = NodeType_D11_2221.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_D11_2221.typeVar,
				NodeType_Node.typeVar,
				NodeType_A1.typeVar,
				NodeType_A2.typeVar,
				NodeType_A4.typeVar,
				NodeType_B22.typeVar,
				NodeType_B41.typeVar,
				NodeType_C222_411.typeVar,
			};
			NodeType_D2211_2222_31.typeVar.subOrSameGrGenTypes = NodeType_D2211_2222_31.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_D2211_2222_31.typeVar,
			};
			NodeType_D2211_2222_31.typeVar.superOrSameGrGenTypes = NodeType_D2211_2222_31.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_D2211_2222_31.typeVar,
				NodeType_Node.typeVar,
				NodeType_A2.typeVar,
				NodeType_A3.typeVar,
				NodeType_A4.typeVar,
				NodeType_B22.typeVar,
				NodeType_B41.typeVar,
				NodeType_C221.typeVar,
				NodeType_C222_411.typeVar,
			};
			NodeType_D231_4121.typeVar.subOrSameGrGenTypes = NodeType_D231_4121.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_D231_4121.typeVar,
			};
			NodeType_D231_4121.typeVar.superOrSameGrGenTypes = NodeType_D231_4121.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_D231_4121.typeVar,
				NodeType_Node.typeVar,
				NodeType_A2.typeVar,
				NodeType_A4.typeVar,
				NodeType_A5.typeVar,
				NodeType_B23.typeVar,
				NodeType_B41.typeVar,
				NodeType_B42.typeVar,
				NodeType_B43.typeVar,
				NodeType_C412_421_431_51.typeVar,
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
				case "A1" : return NodeType_A1.typeVar;
				case "A2" : return NodeType_A2.typeVar;
				case "A3" : return NodeType_A3.typeVar;
				case "A4" : return NodeType_A4.typeVar;
				case "A5" : return NodeType_A5.typeVar;
				case "B21" : return NodeType_B21.typeVar;
				case "B22" : return NodeType_B22.typeVar;
				case "B23" : return NodeType_B23.typeVar;
				case "B41" : return NodeType_B41.typeVar;
				case "B42" : return NodeType_B42.typeVar;
				case "B43" : return NodeType_B43.typeVar;
				case "C221" : return NodeType_C221.typeVar;
				case "C222_411" : return NodeType_C222_411.typeVar;
				case "C412_421_431_51" : return NodeType_C412_421_431_51.typeVar;
				case "C432_422" : return NodeType_C432_422.typeVar;
				case "D11_2221" : return NodeType_D11_2221.typeVar;
				case "D2211_2222_31" : return NodeType_D2211_2222_31.typeVar;
				case "D231_4121" : return NodeType_D231_4121.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_A1.typeVar,
			NodeType_A2.typeVar,
			NodeType_A3.typeVar,
			NodeType_A4.typeVar,
			NodeType_A5.typeVar,
			NodeType_B21.typeVar,
			NodeType_B22.typeVar,
			NodeType_B23.typeVar,
			NodeType_B41.typeVar,
			NodeType_B42.typeVar,
			NodeType_B43.typeVar,
			NodeType_C221.typeVar,
			NodeType_C222_411.typeVar,
			NodeType_C412_421_431_51.typeVar,
			NodeType_C432_422.typeVar,
			NodeType_D11_2221.typeVar,
			NodeType_D2211_2222_31.typeVar,
			NodeType_D231_4121.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_A1),
			typeof(NodeType_A2),
			typeof(NodeType_A3),
			typeof(NodeType_A4),
			typeof(NodeType_A5),
			typeof(NodeType_B21),
			typeof(NodeType_B22),
			typeof(NodeType_B23),
			typeof(NodeType_B41),
			typeof(NodeType_B42),
			typeof(NodeType_B43),
			typeof(NodeType_C221),
			typeof(NodeType_C222_411),
			typeof(NodeType_C412_421_431_51),
			typeof(NodeType_C432_422),
			typeof(NodeType_D11_2221),
			typeof(NodeType_D2211_2222_31),
			typeof(NodeType_D231_4121),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
			NodeType_A1.AttributeType_a1,
			NodeType_A2.AttributeType_a2,
			NodeType_A3.AttributeType_a3,
			NodeType_A4.AttributeType_a4,
			NodeType_A5.AttributeType_a5,
			NodeType_B21.AttributeType_b21,
			NodeType_B22.AttributeType_b22,
			NodeType_B23.AttributeType_b23,
			NodeType_B41.AttributeType_b41,
			NodeType_B42.AttributeType_b42,
			NodeType_C221.AttributeType_c221,
			NodeType_C222_411.AttributeType_c222_411,
			NodeType_C432_422.AttributeType_c432_422,
			NodeType_D11_2221.AttributeType_d11_2221,
			NodeType_D2211_2222_31.AttributeType_d2211_2222_31,
			NodeType_D231_4121.AttributeType_d231_4121,
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @Edge };

	// *** Edge Edge ***

	public interface IEdge_Edge : IAttributes
	{
	}

	public sealed class Edge_Edge : LGSPEdge, IEdge_Edge
	{
		private static int poolLevel = 0;
		private static Edge_Edge[] pool = new Edge_Edge[10];
		public Edge_Edge(LGSPNode source, LGSPNode target)
			: base(EdgeType_Edge.typeVar, source, target)
		{
		}
		public override IEdge Clone(INode newSource, INode newTarget)
		{ return new Edge_Edge(this, (LGSPNode) newSource, (LGSPNode) newTarget); }

		private Edge_Edge(Edge_Edge oldElem, LGSPNode newSource, LGSPNode newTarget)
			: base(EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
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
		public static bool[] isA = new bool[] { true, };
		public static bool[] isMyType = new bool[] { true, };
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

	//
	// Edge model
	//

	public sealed class testEdgeModel : IEdgeModel
	{
		public testEdgeModel()
		{
			EdgeType_Edge.typeVar.subOrSameGrGenTypes = EdgeType_Edge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_Edge.typeVar,
			};
			EdgeType_Edge.typeVar.superOrSameGrGenTypes = EdgeType_Edge.typeVar.superOrSameTypes = new EdgeType[] {
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
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private EdgeType[] types = {
			EdgeType_Edge.typeVar,
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
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
		public String MD5Hash { get { return "72976c7fc07bd75a73674984ca518dfc"; } }
	}
}
