// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\TNT\TNT.grg" on Sun Feb 06 21:14:44 CET 2011

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_TNT
{
	using GRGEN_MODEL = de.unika.ipd.grGen.Model_TNT;
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
		private static GRGEN_MODEL.@Node[] pool = new GRGEN_MODEL.@Node[10];
		
		static @Node() {
		}
		
		public @Node() : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
			// implicit initialization, map/set creation of Node
		}

		public static GRGEN_MODEL.NodeType_Node TypeInstance { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Node(this); }

		private @Node(GRGEN_MODEL.@Node oldElem) : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
		}
		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of Node
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Node CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Node node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Node();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static GRGEN_MODEL.NodeType_Node typeVar = new GRGEN_MODEL.NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@Node"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Node();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@Node();
		}

	}

	// *** Node C ***

	public interface IC : GRGEN_LIBGR.INode
	{
	}

	public sealed class @C : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IC
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@C[] pool = new GRGEN_MODEL.@C[10];
		
		// explicit initializations of C for target C
		// implicit initializations of C for target C
		static @C() {
		}
		
		public @C() : base(GRGEN_MODEL.NodeType_C.typeVar)
		{
			// implicit initialization, map/set creation of C
			// explicit initializations of C for target C
		}

		public static GRGEN_MODEL.NodeType_C TypeInstance { get { return GRGEN_MODEL.NodeType_C.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@C(this); }

		private @C(GRGEN_MODEL.@C oldElem) : base(GRGEN_MODEL.NodeType_C.typeVar)
		{
		}
		public static GRGEN_MODEL.@C CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@C node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of C
				// explicit initializations of C for target C
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@C CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@C node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@C();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static GRGEN_MODEL.NodeType_C typeVar = new GRGEN_MODEL.NodeType_C();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, false, false, false, };
		public NodeType_C() : base((int) NodeTypes.@C)
		{
		}
		public override string Name { get { return "C"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_TNT.IC"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@C"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@C();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@C();
		}

	}

	// *** Node H ***

	public interface IH : GRGEN_LIBGR.INode
	{
	}

	public sealed class @H : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IH
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@H[] pool = new GRGEN_MODEL.@H[10];
		
		// explicit initializations of H for target H
		// implicit initializations of H for target H
		static @H() {
		}
		
		public @H() : base(GRGEN_MODEL.NodeType_H.typeVar)
		{
			// implicit initialization, map/set creation of H
			// explicit initializations of H for target H
		}

		public static GRGEN_MODEL.NodeType_H TypeInstance { get { return GRGEN_MODEL.NodeType_H.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@H(this); }

		private @H(GRGEN_MODEL.@H oldElem) : base(GRGEN_MODEL.NodeType_H.typeVar)
		{
		}
		public static GRGEN_MODEL.@H CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@H node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@H();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of H
				// explicit initializations of H for target H
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@H CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@H node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@H();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static GRGEN_MODEL.NodeType_H typeVar = new GRGEN_MODEL.NodeType_H();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, };
		public NodeType_H() : base((int) NodeTypes.@H)
		{
		}
		public override string Name { get { return "H"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_TNT.IH"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@H"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@H();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@H();
		}

	}

	// *** Node O ***

	public interface IO : GRGEN_LIBGR.INode
	{
	}

	public sealed class @O : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IO
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@O[] pool = new GRGEN_MODEL.@O[10];
		
		// explicit initializations of O for target O
		// implicit initializations of O for target O
		static @O() {
		}
		
		public @O() : base(GRGEN_MODEL.NodeType_O.typeVar)
		{
			// implicit initialization, map/set creation of O
			// explicit initializations of O for target O
		}

		public static GRGEN_MODEL.NodeType_O TypeInstance { get { return GRGEN_MODEL.NodeType_O.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@O(this); }

		private @O(GRGEN_MODEL.@O oldElem) : base(GRGEN_MODEL.NodeType_O.typeVar)
		{
		}
		public static GRGEN_MODEL.@O CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@O node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@O();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of O
				// explicit initializations of O for target O
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@O CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@O node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@O();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static GRGEN_MODEL.NodeType_O typeVar = new GRGEN_MODEL.NodeType_O();
		public static bool[] isA = new bool[] { true, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, };
		public NodeType_O() : base((int) NodeTypes.@O)
		{
		}
		public override string Name { get { return "O"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_TNT.IO"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@O"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@O();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@O();
		}

	}

	// *** Node N ***

	public interface IN : GRGEN_LIBGR.INode
	{
	}

	public sealed class @N : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IN
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@N[] pool = new GRGEN_MODEL.@N[10];
		
		// explicit initializations of N for target N
		// implicit initializations of N for target N
		static @N() {
		}
		
		public @N() : base(GRGEN_MODEL.NodeType_N.typeVar)
		{
			// implicit initialization, map/set creation of N
			// explicit initializations of N for target N
		}

		public static GRGEN_MODEL.NodeType_N TypeInstance { get { return GRGEN_MODEL.NodeType_N.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@N(this); }

		private @N(GRGEN_MODEL.@N oldElem) : base(GRGEN_MODEL.NodeType_N.typeVar)
		{
		}
		public static GRGEN_MODEL.@N CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@N node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@N();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of N
				// explicit initializations of N for target N
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@N CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@N node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@N();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static GRGEN_MODEL.NodeType_N typeVar = new GRGEN_MODEL.NodeType_N();
		public static bool[] isA = new bool[] { true, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, };
		public NodeType_N() : base((int) NodeTypes.@N)
		{
		}
		public override string Name { get { return "N"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_TNT.IN"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@N"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@N();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@N();
		}

	}

	// *** Node P ***

	public interface IP : GRGEN_LIBGR.INode
	{
	}

	public sealed class @P : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IP
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@P[] pool = new GRGEN_MODEL.@P[10];
		
		// explicit initializations of P for target P
		// implicit initializations of P for target P
		static @P() {
		}
		
		public @P() : base(GRGEN_MODEL.NodeType_P.typeVar)
		{
			// implicit initialization, map/set creation of P
			// explicit initializations of P for target P
		}

		public static GRGEN_MODEL.NodeType_P TypeInstance { get { return GRGEN_MODEL.NodeType_P.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@P(this); }

		private @P(GRGEN_MODEL.@P oldElem) : base(GRGEN_MODEL.NodeType_P.typeVar)
		{
		}
		public static GRGEN_MODEL.@P CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@P node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@P();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of P
				// explicit initializations of P for target P
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@P CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@P node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@P();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static GRGEN_MODEL.NodeType_P typeVar = new GRGEN_MODEL.NodeType_P();
		public static bool[] isA = new bool[] { true, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, };
		public NodeType_P() : base((int) NodeTypes.@P)
		{
		}
		public override string Name { get { return "P"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_TNT.IP"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@P"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@P();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@P();
		}

	}

	// *** Node S ***

	public interface IS : GRGEN_LIBGR.INode
	{
	}

	public sealed class @S : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IS
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@S[] pool = new GRGEN_MODEL.@S[10];
		
		// explicit initializations of S for target S
		// implicit initializations of S for target S
		static @S() {
		}
		
		public @S() : base(GRGEN_MODEL.NodeType_S.typeVar)
		{
			// implicit initialization, map/set creation of S
			// explicit initializations of S for target S
		}

		public static GRGEN_MODEL.NodeType_S TypeInstance { get { return GRGEN_MODEL.NodeType_S.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@S(this); }

		private @S(GRGEN_MODEL.@S oldElem) : base(GRGEN_MODEL.NodeType_S.typeVar)
		{
		}
		public static GRGEN_MODEL.@S CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@S node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@S();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set creation of S
				// explicit initializations of S for target S
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@S CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@S node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@S();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
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
		public static GRGEN_MODEL.NodeType_S typeVar = new GRGEN_MODEL.NodeType_S();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, };
		public NodeType_S() : base((int) NodeTypes.@S)
		{
		}
		public override string Name { get { return "S"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_TNT.IS"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@S"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@S();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			return new GRGEN_MODEL.@S();
		}

	}

	//
	// Node model
	//

	public sealed class TNTNodeModel : GRGEN_LIBGR.INodeModel
	{
		public TNTNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_C.typeVar,
				GRGEN_MODEL.NodeType_H.typeVar,
				GRGEN_MODEL.NodeType_O.typeVar,
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_P.typeVar,
				GRGEN_MODEL.NodeType_S.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C.typeVar,
				GRGEN_MODEL.NodeType_H.typeVar,
				GRGEN_MODEL.NodeType_O.typeVar,
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_P.typeVar,
				GRGEN_MODEL.NodeType_S.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_C.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_C.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C.typeVar,
			};
			GRGEN_MODEL.NodeType_C.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_C.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_C.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_C.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_C.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_C.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_C.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_H.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_H.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_H.typeVar,
			};
			GRGEN_MODEL.NodeType_H.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_H.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_H.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_H.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_H.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_H.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_H.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_O.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_O.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_O.typeVar,
			};
			GRGEN_MODEL.NodeType_O.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_O.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_O.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_O.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_O.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_O.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_O.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_N.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_N.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_N.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_N.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_P.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_P.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_P.typeVar,
			};
			GRGEN_MODEL.NodeType_P.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_P.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_P.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_P.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_P.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_P.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_P.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_S.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_S.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_S.typeVar,
			};
			GRGEN_MODEL.NodeType_S.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_S.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_S.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_S.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_S.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_S.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_S.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
		}
		public bool IsNodeModel { get { return true; } }
		public GRGEN_LIBGR.NodeType RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.NodeType_Node.typeVar; } }
		public GRGEN_LIBGR.NodeType GetType(string name)
		{
			switch(name)
			{
				case "Node" : return GRGEN_MODEL.NodeType_Node.typeVar;
				case "C" : return GRGEN_MODEL.NodeType_C.typeVar;
				case "H" : return GRGEN_MODEL.NodeType_H.typeVar;
				case "O" : return GRGEN_MODEL.NodeType_O.typeVar;
				case "N" : return GRGEN_MODEL.NodeType_N.typeVar;
				case "P" : return GRGEN_MODEL.NodeType_P.typeVar;
				case "S" : return GRGEN_MODEL.NodeType_S.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_C.typeVar,
			GRGEN_MODEL.NodeType_H.typeVar,
			GRGEN_MODEL.NodeType_O.typeVar,
			GRGEN_MODEL.NodeType_N.typeVar,
			GRGEN_MODEL.NodeType_P.typeVar,
			GRGEN_MODEL.NodeType_S.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_C),
			typeof(GRGEN_MODEL.NodeType_H),
			typeof(GRGEN_MODEL.NodeType_O),
			typeof(GRGEN_MODEL.NodeType_N),
			typeof(GRGEN_MODEL.NodeType_P),
			typeof(GRGEN_MODEL.NodeType_S),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
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
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, };
		public EdgeType_AEdge() : base((int) EdgeTypes.@AEdge)
		{
		}
		public override string Name { get { return "AEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return null; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Arbitrary; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			throw new Exception("The abstract edge type AEdge cannot be instantiated!");
		}
		public override bool IsAbstract { get { return true; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
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
		private static GRGEN_MODEL.@Edge[] pool = new GRGEN_MODEL.@Edge[10];
		
		static @Edge() {
		}
		
		public @Edge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, source, target)
		{
			// implicit initialization, map/set creation of Edge
		}

		public static GRGEN_MODEL.EdgeType_Edge TypeInstance { get { return GRGEN_MODEL.EdgeType_Edge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@Edge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @Edge(GRGEN_MODEL.@Edge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_Edge.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set creation of Edge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@Edge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@Edge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@Edge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
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
		public static GRGEN_MODEL.EdgeType_Edge typeVar = new GRGEN_MODEL.EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, };
		public static bool[] isMyType = new bool[] { false, true, false, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@Edge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@Edge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge UEdge ***


	public sealed class @UEdge : GRGEN_LGSP.LGSPEdge, GRGEN_LIBGR.IEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@UEdge[] pool = new GRGEN_MODEL.@UEdge[10];
		
		static @UEdge() {
		}
		
		public @UEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, source, target)
		{
			// implicit initialization, map/set creation of UEdge
		}

		public static GRGEN_MODEL.EdgeType_UEdge TypeInstance { get { return GRGEN_MODEL.EdgeType_UEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@UEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @UEdge(GRGEN_MODEL.@UEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_UEdge.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set creation of UEdge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@UEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@UEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@UEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
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
		public static GRGEN_MODEL.EdgeType_UEdge typeVar = new GRGEN_MODEL.EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, };
		public static bool[] isMyType = new bool[] { false, false, true, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_TNT.@UEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Undirected; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 0; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { yield break; } }
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name) { return null; }
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.IEdge CreateEdgeWithCopyCommons(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target, GRGEN_LIBGR.IEdge oldIEdge)
		{
			return new GRGEN_MODEL.@UEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class TNTEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public TNTEdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AEdge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_UEdge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_UEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
			};
		}
		public bool IsNodeModel { get { return false; } }
		public GRGEN_LIBGR.EdgeType RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.RootType { get { return GRGEN_MODEL.EdgeType_AEdge.typeVar; } }
		public GRGEN_LIBGR.EdgeType GetType(string name)
		{
			switch(name)
			{
				case "AEdge" : return GRGEN_MODEL.EdgeType_AEdge.typeVar;
				case "Edge" : return GRGEN_MODEL.EdgeType_Edge.typeVar;
				case "UEdge" : return GRGEN_MODEL.EdgeType_UEdge.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.EdgeType[] types = {
			GRGEN_MODEL.EdgeType_AEdge.typeVar,
			GRGEN_MODEL.EdgeType_Edge.typeVar,
			GRGEN_MODEL.EdgeType_UEdge.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
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

		public string ModelName { get { return "TNT"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "6589f1c2ac770b020a3debe9eff7e74d"; } }
	}

	//
	// IGraph/IGraphModel implementation
	//
	public class TNTGraph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public TNTGraph() : base(GetNextGraphName())
		{
			InitializeGraph(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode()
		{
			return GRGEN_MODEL.@Node.CreateNode(this);
		}

		public GRGEN_MODEL.@Node CreateNodeNode(string varName)
		{
			return GRGEN_MODEL.@Node.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@C CreateNodeC()
		{
			return GRGEN_MODEL.@C.CreateNode(this);
		}

		public GRGEN_MODEL.@C CreateNodeC(string varName)
		{
			return GRGEN_MODEL.@C.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@H CreateNodeH()
		{
			return GRGEN_MODEL.@H.CreateNode(this);
		}

		public GRGEN_MODEL.@H CreateNodeH(string varName)
		{
			return GRGEN_MODEL.@H.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@O CreateNodeO()
		{
			return GRGEN_MODEL.@O.CreateNode(this);
		}

		public GRGEN_MODEL.@O CreateNodeO(string varName)
		{
			return GRGEN_MODEL.@O.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@N CreateNodeN()
		{
			return GRGEN_MODEL.@N.CreateNode(this);
		}

		public GRGEN_MODEL.@N CreateNodeN(string varName)
		{
			return GRGEN_MODEL.@N.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@P CreateNodeP()
		{
			return GRGEN_MODEL.@P.CreateNode(this);
		}

		public GRGEN_MODEL.@P CreateNodeP(string varName)
		{
			return GRGEN_MODEL.@P.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@S CreateNodeS()
		{
			return GRGEN_MODEL.@S.CreateNode(this);
		}

		public GRGEN_MODEL.@S CreateNodeS(string varName)
		{
			return GRGEN_MODEL.@S.CreateNode(this, varName);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@Edge CreateEdgeEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@Edge.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@UEdge CreateEdgeUEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@UEdge.CreateEdge(this, source, target, varName);
		}

		private TNTNodeModel nodeModel = new TNTNodeModel();
		private TNTEdgeModel edgeModel = new TNTEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "TNT"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "6589f1c2ac770b020a3debe9eff7e74d"; } }
	}
}
