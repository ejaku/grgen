// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\TNT\TNT.grg" on Thu Jul 17 11:12:35 GMT+01:00 2008

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_TNT
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

	public enum NodeTypes { @Node, @C, @H, @O, @N, @P, @S };

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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, };
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

	// *** Node C ***

	public interface IC : INode
	{
	}

	public sealed class @C : LGSPNode, IC
	{
		private static int poolLevel = 0;
		private static @C[] pool = new @C[10];
		public @C() : base(NodeType_C.typeVar)
		{
		}

		public static NodeType_C TypeInstance { get { return NodeType_C.typeVar; } }

		public override INode Clone() { return new @C(this); }

		private @C(@C oldElem) : base(NodeType_C.typeVar)
		{
		}
		public static @C CreateNode(LGSPGraph graph)
		{
			@C node;
			if(poolLevel == 0)
				node = new @C();
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

		public static @C CreateNode(LGSPGraph graph, String varName)
		{
			@C node;
			if(poolLevel == 0)
				node = new @C();
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
				"The node type \"C\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"C\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_C : NodeType
	{
		public static NodeType_C typeVar = new NodeType_C();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, };
		public NodeType_C() : base((int) NodeTypes.@C)
		{
		}
		public override String Name { get { return "C"; } }
		public override INode CreateNode()
		{
			return new @C();
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
			return new @C();
		}

	}

	// *** Node H ***

	public interface IH : INode
	{
	}

	public sealed class @H : LGSPNode, IH
	{
		private static int poolLevel = 0;
		private static @H[] pool = new @H[10];
		public @H() : base(NodeType_H.typeVar)
		{
		}

		public static NodeType_H TypeInstance { get { return NodeType_H.typeVar; } }

		public override INode Clone() { return new @H(this); }

		private @H(@H oldElem) : base(NodeType_H.typeVar)
		{
		}
		public static @H CreateNode(LGSPGraph graph)
		{
			@H node;
			if(poolLevel == 0)
				node = new @H();
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

		public static @H CreateNode(LGSPGraph graph, String varName)
		{
			@H node;
			if(poolLevel == 0)
				node = new @H();
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
				"The node type \"H\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"H\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_H : NodeType
	{
		public static NodeType_H typeVar = new NodeType_H();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, };
		public NodeType_H() : base((int) NodeTypes.@H)
		{
		}
		public override String Name { get { return "H"; } }
		public override INode CreateNode()
		{
			return new @H();
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
			return new @H();
		}

	}

	// *** Node O ***

	public interface IO : INode
	{
	}

	public sealed class @O : LGSPNode, IO
	{
		private static int poolLevel = 0;
		private static @O[] pool = new @O[10];
		public @O() : base(NodeType_O.typeVar)
		{
		}

		public static NodeType_O TypeInstance { get { return NodeType_O.typeVar; } }

		public override INode Clone() { return new @O(this); }

		private @O(@O oldElem) : base(NodeType_O.typeVar)
		{
		}
		public static @O CreateNode(LGSPGraph graph)
		{
			@O node;
			if(poolLevel == 0)
				node = new @O();
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

		public static @O CreateNode(LGSPGraph graph, String varName)
		{
			@O node;
			if(poolLevel == 0)
				node = new @O();
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
				"The node type \"O\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"O\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_O : NodeType
	{
		public static NodeType_O typeVar = new NodeType_O();
		public static bool[] isA = new bool[] { true, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, };
		public NodeType_O() : base((int) NodeTypes.@O)
		{
		}
		public override String Name { get { return "O"; } }
		public override INode CreateNode()
		{
			return new @O();
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
			return new @O();
		}

	}

	// *** Node N ***

	public interface IN : INode
	{
	}

	public sealed class @N : LGSPNode, IN
	{
		private static int poolLevel = 0;
		private static @N[] pool = new @N[10];
		public @N() : base(NodeType_N.typeVar)
		{
		}

		public static NodeType_N TypeInstance { get { return NodeType_N.typeVar; } }

		public override INode Clone() { return new @N(this); }

		private @N(@N oldElem) : base(NodeType_N.typeVar)
		{
		}
		public static @N CreateNode(LGSPGraph graph)
		{
			@N node;
			if(poolLevel == 0)
				node = new @N();
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

		public static @N CreateNode(LGSPGraph graph, String varName)
		{
			@N node;
			if(poolLevel == 0)
				node = new @N();
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
				"The node type \"N\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"N\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_N : NodeType
	{
		public static NodeType_N typeVar = new NodeType_N();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, };
		public NodeType_N() : base((int) NodeTypes.@N)
		{
		}
		public override String Name { get { return "N"; } }
		public override INode CreateNode()
		{
			return new @N();
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
			return new @N();
		}

	}

	// *** Node P ***

	public interface IP : INode
	{
	}

	public sealed class @P : LGSPNode, IP
	{
		private static int poolLevel = 0;
		private static @P[] pool = new @P[10];
		public @P() : base(NodeType_P.typeVar)
		{
		}

		public static NodeType_P TypeInstance { get { return NodeType_P.typeVar; } }

		public override INode Clone() { return new @P(this); }

		private @P(@P oldElem) : base(NodeType_P.typeVar)
		{
		}
		public static @P CreateNode(LGSPGraph graph)
		{
			@P node;
			if(poolLevel == 0)
				node = new @P();
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

		public static @P CreateNode(LGSPGraph graph, String varName)
		{
			@P node;
			if(poolLevel == 0)
				node = new @P();
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
				"The node type \"P\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"P\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_P : NodeType
	{
		public static NodeType_P typeVar = new NodeType_P();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, };
		public NodeType_P() : base((int) NodeTypes.@P)
		{
		}
		public override String Name { get { return "P"; } }
		public override INode CreateNode()
		{
			return new @P();
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
			return new @P();
		}

	}

	// *** Node S ***

	public interface IS : INode
	{
	}

	public sealed class @S : LGSPNode, IS
	{
		private static int poolLevel = 0;
		private static @S[] pool = new @S[10];
		public @S() : base(NodeType_S.typeVar)
		{
		}

		public static NodeType_S TypeInstance { get { return NodeType_S.typeVar; } }

		public override INode Clone() { return new @S(this); }

		private @S(@S oldElem) : base(NodeType_S.typeVar)
		{
		}
		public static @S CreateNode(LGSPGraph graph)
		{
			@S node;
			if(poolLevel == 0)
				node = new @S();
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

		public static @S CreateNode(LGSPGraph graph, String varName)
		{
			@S node;
			if(poolLevel == 0)
				node = new @S();
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
				"The node type \"S\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The node type \"S\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
		}
	}

	public sealed class NodeType_S : NodeType
	{
		public static NodeType_S typeVar = new NodeType_S();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, };
		public NodeType_S() : base((int) NodeTypes.@S)
		{
		}
		public override String Name { get { return "S"; } }
		public override INode CreateNode()
		{
			return new @S();
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
			return new @S();
		}

	}

	//
	// Node model
	//

	public sealed class TNTNodeModel : INodeModel
	{
		public TNTNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
				NodeType_C.typeVar,
				NodeType_H.typeVar,
				NodeType_O.typeVar,
				NodeType_N.typeVar,
				NodeType_P.typeVar,
				NodeType_S.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new NodeType[] {
				NodeType_C.typeVar,
				NodeType_H.typeVar,
				NodeType_O.typeVar,
				NodeType_N.typeVar,
				NodeType_P.typeVar,
				NodeType_S.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new NodeType[] {
			};
			NodeType_C.typeVar.subOrSameGrGenTypes = NodeType_C.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_C.typeVar,
			};
			NodeType_C.typeVar.directSubGrGenTypes = NodeType_C.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_C.typeVar.superOrSameGrGenTypes = NodeType_C.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_C.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_C.typeVar.directSuperGrGenTypes = NodeType_C.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_H.typeVar.subOrSameGrGenTypes = NodeType_H.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_H.typeVar,
			};
			NodeType_H.typeVar.directSubGrGenTypes = NodeType_H.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_H.typeVar.superOrSameGrGenTypes = NodeType_H.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_H.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_H.typeVar.directSuperGrGenTypes = NodeType_H.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_O.typeVar.subOrSameGrGenTypes = NodeType_O.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_O.typeVar,
			};
			NodeType_O.typeVar.directSubGrGenTypes = NodeType_O.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_O.typeVar.superOrSameGrGenTypes = NodeType_O.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_O.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_O.typeVar.directSuperGrGenTypes = NodeType_O.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_N.typeVar.subOrSameGrGenTypes = NodeType_N.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_N.typeVar,
			};
			NodeType_N.typeVar.directSubGrGenTypes = NodeType_N.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_N.typeVar.superOrSameGrGenTypes = NodeType_N.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_N.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_N.typeVar.directSuperGrGenTypes = NodeType_N.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_P.typeVar.subOrSameGrGenTypes = NodeType_P.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_P.typeVar,
			};
			NodeType_P.typeVar.directSubGrGenTypes = NodeType_P.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_P.typeVar.superOrSameGrGenTypes = NodeType_P.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_P.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_P.typeVar.directSuperGrGenTypes = NodeType_P.typeVar.directSuperTypes = new NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_S.typeVar.subOrSameGrGenTypes = NodeType_S.typeVar.subOrSameTypes = new NodeType[] {
				NodeType_S.typeVar,
			};
			NodeType_S.typeVar.directSubGrGenTypes = NodeType_S.typeVar.directSubTypes = new NodeType[] {
			};
			NodeType_S.typeVar.superOrSameGrGenTypes = NodeType_S.typeVar.superOrSameTypes = new NodeType[] {
				NodeType_S.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_S.typeVar.directSuperGrGenTypes = NodeType_S.typeVar.directSuperTypes = new NodeType[] {
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
				case "C" : return NodeType_C.typeVar;
				case "H" : return NodeType_H.typeVar;
				case "O" : return NodeType_O.typeVar;
				case "N" : return NodeType_N.typeVar;
				case "P" : return NodeType_P.typeVar;
				case "S" : return NodeType_S.typeVar;
			}
			return null;
		}
		GrGenType ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_C.typeVar,
			NodeType_H.typeVar,
			NodeType_O.typeVar,
			NodeType_N.typeVar,
			NodeType_P.typeVar,
			NodeType_S.typeVar,
		};
		public NodeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(NodeType_Node),
			typeof(NodeType_C),
			typeof(NodeType_H),
			typeof(NodeType_O),
			typeof(NodeType_N),
			typeof(NodeType_P),
			typeof(NodeType_S),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : EdgeType
	{
		public static EdgeType_AEdge typeVar = new EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, };
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
		public static bool[] isA = new bool[] { true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, };
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
		public static bool[] isA = new bool[] { true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, true, };
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

	//
	// Edge model
	//

	public sealed class TNTEdgeModel : IEdgeModel
	{
		public TNTEdgeModel()
		{
			EdgeType_AEdge.typeVar.subOrSameGrGenTypes = EdgeType_AEdge.typeVar.subOrSameTypes = new EdgeType[] {
				EdgeType_AEdge.typeVar,
				EdgeType_Edge.typeVar,
				EdgeType_UEdge.typeVar,
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
			};
			EdgeType_Edge.typeVar.directSubGrGenTypes = EdgeType_Edge.typeVar.directSubTypes = new EdgeType[] {
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
		};
		public EdgeType[] Types { get { return types; } }
		GrGenType[] ITypeModel.Types { get { return types; } }
		private Type[] typeTypes = {
			typeof(EdgeType_AEdge),
			typeof(EdgeType_Edge),
			typeof(EdgeType_UEdge),
		};
		public Type[] TypeTypes { get { return typeTypes; } }
		private AttributeType[] attributeTypes = {
		};
		public IEnumerable<AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//

	public sealed class TNTGraphModel : IGraphModel
	{
		private TNTNodeModel nodeModel = new TNTNodeModel();
		private TNTEdgeModel edgeModel = new TNTEdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String ModelName { get { return "TNT"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "6589f1c2ac770b020a3debe9eff7e74d"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class TNT : LGSPGraph, IGraphModel
	{
		public TNT() : base(GetNextGraphName())
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

		public @C CreateNodeC()
		{
			return @C.CreateNode(this);
		}

		public @C CreateNodeC(String varName)
		{
			return @C.CreateNode(this, varName);
		}

		public @H CreateNodeH()
		{
			return @H.CreateNode(this);
		}

		public @H CreateNodeH(String varName)
		{
			return @H.CreateNode(this, varName);
		}

		public @O CreateNodeO()
		{
			return @O.CreateNode(this);
		}

		public @O CreateNodeO(String varName)
		{
			return @O.CreateNode(this, varName);
		}

		public @N CreateNodeN()
		{
			return @N.CreateNode(this);
		}

		public @N CreateNodeN(String varName)
		{
			return @N.CreateNode(this, varName);
		}

		public @P CreateNodeP()
		{
			return @P.CreateNode(this);
		}

		public @P CreateNodeP(String varName)
		{
			return @P.CreateNode(this, varName);
		}

		public @S CreateNodeS()
		{
			return @S.CreateNode(this);
		}

		public @S CreateNodeS(String varName)
		{
			return @S.CreateNode(this, varName);
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

		private TNTNodeModel nodeModel = new TNTNodeModel();
		private TNTEdgeModel edgeModel = new TNTEdgeModel();
		private ValidateInfo[] validateInfos = {
		};

		public String ModelName { get { return "TNT"; } }
		public INodeModel NodeModel { get { return nodeModel; } }
		public IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public String MD5Hash { get { return "6589f1c2ac770b020a3debe9eff7e74d"; } }
	}
}
