// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\TNT\TNT.grg" on Sun Dec 28 14:48:04 GMT+01:00 2008

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

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
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, };
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

	// *** Node C ***

	public interface IC : GRGEN_LIBGR.INode
	{
	}

	public sealed class @C : GRGEN_LGSP.LGSPNode, IC
	{
		private static int poolLevel = 0;
		private static @C[] pool = new @C[10];
		
		// explicit initializations of C for target C
		static @C() {
		}
		
		public @C() : base(NodeType_C.typeVar)
		{
			// implicit initialization, map/set creation of C
			// explicit initializations of C for target C
		}

		public static NodeType_C TypeInstance { get { return NodeType_C.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @C(this); }

		private @C(@C oldElem) : base(NodeType_C.typeVar)
		{
		}
		public static @C CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@C node;
			if(poolLevel == 0)
				node = new @C();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of C
				// explicit initializations of C for target C
			}
			graph.AddNode(node);
			return node;
		}

		public static @C CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@C node;
			if(poolLevel == 0)
				node = new @C();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of C
				// explicit initializations of C for target C
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
			// implicit initialization, map/set creation of C
			// explicit initializations of C for target C
		}
	}

	public sealed class NodeType_C : GRGEN_LIBGR.NodeType
	{
		public static NodeType_C typeVar = new NodeType_C();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, };
		public NodeType_C() : base((int) NodeTypes.@C)
		{
		}
		public override String Name { get { return "C"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @C();
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
			return new @C();
		}

	}

	// *** Node H ***

	public interface IH : GRGEN_LIBGR.INode
	{
	}

	public sealed class @H : GRGEN_LGSP.LGSPNode, IH
	{
		private static int poolLevel = 0;
		private static @H[] pool = new @H[10];
		
		// explicit initializations of H for target H
		static @H() {
		}
		
		public @H() : base(NodeType_H.typeVar)
		{
			// implicit initialization, map/set creation of H
			// explicit initializations of H for target H
		}

		public static NodeType_H TypeInstance { get { return NodeType_H.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @H(this); }

		private @H(@H oldElem) : base(NodeType_H.typeVar)
		{
		}
		public static @H CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@H node;
			if(poolLevel == 0)
				node = new @H();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of H
				// explicit initializations of H for target H
			}
			graph.AddNode(node);
			return node;
		}

		public static @H CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@H node;
			if(poolLevel == 0)
				node = new @H();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of H
				// explicit initializations of H for target H
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
			// implicit initialization, map/set creation of H
			// explicit initializations of H for target H
		}
	}

	public sealed class NodeType_H : GRGEN_LIBGR.NodeType
	{
		public static NodeType_H typeVar = new NodeType_H();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, };
		public NodeType_H() : base((int) NodeTypes.@H)
		{
		}
		public override String Name { get { return "H"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @H();
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
			return new @H();
		}

	}

	// *** Node O ***

	public interface IO : GRGEN_LIBGR.INode
	{
	}

	public sealed class @O : GRGEN_LGSP.LGSPNode, IO
	{
		private static int poolLevel = 0;
		private static @O[] pool = new @O[10];
		
		// explicit initializations of O for target O
		static @O() {
		}
		
		public @O() : base(NodeType_O.typeVar)
		{
			// implicit initialization, map/set creation of O
			// explicit initializations of O for target O
		}

		public static NodeType_O TypeInstance { get { return NodeType_O.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @O(this); }

		private @O(@O oldElem) : base(NodeType_O.typeVar)
		{
		}
		public static @O CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@O node;
			if(poolLevel == 0)
				node = new @O();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of O
				// explicit initializations of O for target O
			}
			graph.AddNode(node);
			return node;
		}

		public static @O CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@O node;
			if(poolLevel == 0)
				node = new @O();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of O
				// explicit initializations of O for target O
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
			// implicit initialization, map/set creation of O
			// explicit initializations of O for target O
		}
	}

	public sealed class NodeType_O : GRGEN_LIBGR.NodeType
	{
		public static NodeType_O typeVar = new NodeType_O();
		public static bool[] isA = new bool[] { true, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, };
		public NodeType_O() : base((int) NodeTypes.@O)
		{
		}
		public override String Name { get { return "O"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @O();
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
			return new @O();
		}

	}

	// *** Node N ***

	public interface IN : GRGEN_LIBGR.INode
	{
	}

	public sealed class @N : GRGEN_LGSP.LGSPNode, IN
	{
		private static int poolLevel = 0;
		private static @N[] pool = new @N[10];
		
		// explicit initializations of N for target N
		static @N() {
		}
		
		public @N() : base(NodeType_N.typeVar)
		{
			// implicit initialization, map/set creation of N
			// explicit initializations of N for target N
		}

		public static NodeType_N TypeInstance { get { return NodeType_N.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @N(this); }

		private @N(@N oldElem) : base(NodeType_N.typeVar)
		{
		}
		public static @N CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@N node;
			if(poolLevel == 0)
				node = new @N();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of N
				// explicit initializations of N for target N
			}
			graph.AddNode(node);
			return node;
		}

		public static @N CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@N node;
			if(poolLevel == 0)
				node = new @N();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of N
				// explicit initializations of N for target N
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
			// implicit initialization, map/set creation of N
			// explicit initializations of N for target N
		}
	}

	public sealed class NodeType_N : GRGEN_LIBGR.NodeType
	{
		public static NodeType_N typeVar = new NodeType_N();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, };
		public NodeType_N() : base((int) NodeTypes.@N)
		{
		}
		public override String Name { get { return "N"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @N();
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
			return new @N();
		}

	}

	// *** Node P ***

	public interface IP : GRGEN_LIBGR.INode
	{
	}

	public sealed class @P : GRGEN_LGSP.LGSPNode, IP
	{
		private static int poolLevel = 0;
		private static @P[] pool = new @P[10];
		
		// explicit initializations of P for target P
		static @P() {
		}
		
		public @P() : base(NodeType_P.typeVar)
		{
			// implicit initialization, map/set creation of P
			// explicit initializations of P for target P
		}

		public static NodeType_P TypeInstance { get { return NodeType_P.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @P(this); }

		private @P(@P oldElem) : base(NodeType_P.typeVar)
		{
		}
		public static @P CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@P node;
			if(poolLevel == 0)
				node = new @P();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of P
				// explicit initializations of P for target P
			}
			graph.AddNode(node);
			return node;
		}

		public static @P CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@P node;
			if(poolLevel == 0)
				node = new @P();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of P
				// explicit initializations of P for target P
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
			// implicit initialization, map/set creation of P
			// explicit initializations of P for target P
		}
	}

	public sealed class NodeType_P : GRGEN_LIBGR.NodeType
	{
		public static NodeType_P typeVar = new NodeType_P();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, };
		public NodeType_P() : base((int) NodeTypes.@P)
		{
		}
		public override String Name { get { return "P"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @P();
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
			return new @P();
		}

	}

	// *** Node S ***

	public interface IS : GRGEN_LIBGR.INode
	{
	}

	public sealed class @S : GRGEN_LGSP.LGSPNode, IS
	{
		private static int poolLevel = 0;
		private static @S[] pool = new @S[10];
		
		// explicit initializations of S for target S
		static @S() {
		}
		
		public @S() : base(NodeType_S.typeVar)
		{
			// implicit initialization, map/set creation of S
			// explicit initializations of S for target S
		}

		public static NodeType_S TypeInstance { get { return NodeType_S.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new @S(this); }

		private @S(@S oldElem) : base(NodeType_S.typeVar)
		{
		}
		public static @S CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			@S node;
			if(poolLevel == 0)
				node = new @S();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of S
				// explicit initializations of S for target S
			}
			graph.AddNode(node);
			return node;
		}

		public static @S CreateNode(GRGEN_LGSP.LGSPGraph graph, String varName)
		{
			@S node;
			if(poolLevel == 0)
				node = new @S();
			else
			{
				node = pool[--poolLevel];
				node.inhead = null;
				node.outhead = null;
				node.flags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of S
				// explicit initializations of S for target S
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
			// implicit initialization, map/set creation of S
			// explicit initializations of S for target S
		}
	}

	public sealed class NodeType_S : GRGEN_LIBGR.NodeType
	{
		public static NodeType_S typeVar = new NodeType_S();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, };
		public NodeType_S() : base((int) NodeTypes.@S)
		{
		}
		public override String Name { get { return "S"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new @S();
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
			return new @S();
		}

	}

	//
	// Node model
	//

	public sealed class TNTNodeModel : GRGEN_LIBGR.INodeModel
	{
		public TNTNodeModel()
		{
			NodeType_Node.typeVar.subOrSameGrGenTypes = NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
				NodeType_C.typeVar,
				NodeType_H.typeVar,
				NodeType_O.typeVar,
				NodeType_N.typeVar,
				NodeType_P.typeVar,
				NodeType_S.typeVar,
			};
			NodeType_Node.typeVar.directSubGrGenTypes = NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_C.typeVar,
				NodeType_H.typeVar,
				NodeType_O.typeVar,
				NodeType_N.typeVar,
				NodeType_P.typeVar,
				NodeType_S.typeVar,
			};
			NodeType_Node.typeVar.superOrSameGrGenTypes = NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_Node.typeVar.directSuperGrGenTypes = NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_C.typeVar.subOrSameGrGenTypes = NodeType_C.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_C.typeVar,
			};
			NodeType_C.typeVar.directSubGrGenTypes = NodeType_C.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_C.typeVar.superOrSameGrGenTypes = NodeType_C.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_C.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_C.typeVar.directSuperGrGenTypes = NodeType_C.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_H.typeVar.subOrSameGrGenTypes = NodeType_H.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_H.typeVar,
			};
			NodeType_H.typeVar.directSubGrGenTypes = NodeType_H.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_H.typeVar.superOrSameGrGenTypes = NodeType_H.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_H.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_H.typeVar.directSuperGrGenTypes = NodeType_H.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_O.typeVar.subOrSameGrGenTypes = NodeType_O.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_O.typeVar,
			};
			NodeType_O.typeVar.directSubGrGenTypes = NodeType_O.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_O.typeVar.superOrSameGrGenTypes = NodeType_O.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_O.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_O.typeVar.directSuperGrGenTypes = NodeType_O.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_N.typeVar.subOrSameGrGenTypes = NodeType_N.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_N.typeVar,
			};
			NodeType_N.typeVar.directSubGrGenTypes = NodeType_N.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_N.typeVar.superOrSameGrGenTypes = NodeType_N.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_N.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_N.typeVar.directSuperGrGenTypes = NodeType_N.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_P.typeVar.subOrSameGrGenTypes = NodeType_P.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_P.typeVar,
			};
			NodeType_P.typeVar.directSubGrGenTypes = NodeType_P.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_P.typeVar.superOrSameGrGenTypes = NodeType_P.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_P.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_P.typeVar.directSuperGrGenTypes = NodeType_P.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
			};
			NodeType_S.typeVar.subOrSameGrGenTypes = NodeType_S.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_S.typeVar,
			};
			NodeType_S.typeVar.directSubGrGenTypes = NodeType_S.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			NodeType_S.typeVar.superOrSameGrGenTypes = NodeType_S.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_S.typeVar,
				NodeType_Node.typeVar,
			};
			NodeType_S.typeVar.directSuperGrGenTypes = NodeType_S.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				NodeType_Node.typeVar,
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
				case "C" : return NodeType_C.typeVar;
				case "H" : return NodeType_H.typeVar;
				case "O" : return NodeType_O.typeVar;
				case "N" : return NodeType_N.typeVar;
				case "P" : return NodeType_P.typeVar;
				case "S" : return NodeType_S.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(String name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			NodeType_Node.typeVar,
			NodeType_C.typeVar,
			NodeType_H.typeVar,
			NodeType_O.typeVar,
			NodeType_N.typeVar,
			NodeType_P.typeVar,
			NodeType_S.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
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

	public sealed class TNTEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public TNTEdgeModel()
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

	public sealed class TNTGraphModel : GRGEN_LIBGR.IGraphModel
	{
		private TNTNodeModel nodeModel = new TNTNodeModel();
		private TNTEdgeModel edgeModel = new TNTEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "TNT"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "6589f1c2ac770b020a3debe9eff7e74d"; } }
	}
	//
	// IGraph/IGraphModel implementation
	//

	public class TNT : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
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

		private TNTNodeModel nodeModel = new TNTNodeModel();
		private TNTEdgeModel edgeModel = new TNTEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public String ModelName { get { return "TNT"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public String MD5Hash { get { return "6589f1c2ac770b020a3debe9eff7e74d"; } }
	}
}
