// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Turing3\Turing3.grg" on Sat Jun 25 14:38:18 CEST 2011

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_Turing3
{
	using GRGEN_MODEL = de.unika.ipd.grGen.Model_Turing3;
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


	public sealed class @Node : GRGEN_LGSP.LGSPNode, GRGEN_LIBGR.INode
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Node[] pool = new GRGEN_MODEL.@Node[10];
		
		static @Node() {
		}
		
		public @Node() : base(GRGEN_MODEL.NodeType_Node.typeVar)
		{
			// implicit initialization, map/set/array creation of Node
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
				// implicit initialization, map/set/array creation of Node
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
				// implicit initialization, map/set/array creation of Node
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
			// implicit initialization, map/set/array creation of Node
		}
	}

	public sealed class NodeType_Node : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Node typeVar = new GRGEN_MODEL.NodeType_Node();
		public static bool[] isA = new bool[] { true, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@Node"; } }
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

	// *** Node BandPosition ***

	public interface IBandPosition : GRGEN_LIBGR.INode
	{
		int @value { get; set; }
	}

	public sealed class @BandPosition : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IBandPosition
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@BandPosition[] pool = new GRGEN_MODEL.@BandPosition[10];
		
		// explicit initializations of BandPosition for target BandPosition
		// implicit initializations of BandPosition for target BandPosition
		static @BandPosition() {
		}
		
		public @BandPosition() : base(GRGEN_MODEL.NodeType_BandPosition.typeVar)
		{
			// implicit initialization, map/set/array creation of BandPosition
			// explicit initializations of BandPosition for target BandPosition
		}

		public static GRGEN_MODEL.NodeType_BandPosition TypeInstance { get { return GRGEN_MODEL.NodeType_BandPosition.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@BandPosition(this); }

		private @BandPosition(GRGEN_MODEL.@BandPosition oldElem) : base(GRGEN_MODEL.NodeType_BandPosition.typeVar)
		{
			value_M0no_suXx_h4rD = oldElem.value_M0no_suXx_h4rD;
		}
		public static GRGEN_MODEL.@BandPosition CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@BandPosition node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of BandPosition
				node.@value = 0;
				// explicit initializations of BandPosition for target BandPosition
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@BandPosition CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@BandPosition node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@BandPosition();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of BandPosition
				node.@value = 0;
				// explicit initializations of BandPosition for target BandPosition
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int value_M0no_suXx_h4rD;
		public int @value
		{
			get { return value_M0no_suXx_h4rD; }
			set { value_M0no_suXx_h4rD = value; }
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
			// implicit initialization, map/set/array creation of BandPosition
			this.@value = 0;
			// explicit initializations of BandPosition for target BandPosition
		}
	}

	public sealed class NodeType_BandPosition : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_BandPosition typeVar = new GRGEN_MODEL.NodeType_BandPosition();
		public static bool[] isA = new bool[] { true, true, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_value;
		public NodeType_BandPosition() : base((int) NodeTypes.@BandPosition)
		{
			AttributeType_value = new GRGEN_LIBGR.AttributeType("value", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null);
		}
		public override string Name { get { return "BandPosition"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IBandPosition"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@BandPosition"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@BandPosition();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_value;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "value" : return AttributeType_value;
			}
			return null;
		}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@BandPosition newNode = new GRGEN_MODEL.@BandPosition();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@BandPosition:
					// copy attributes for: BandPosition
					{
						GRGEN_MODEL.IBandPosition old = (GRGEN_MODEL.IBandPosition) oldNode;
						newNode.@value = old.@value;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node State ***

	public interface IState : GRGEN_LIBGR.INode
	{
	}

	public sealed class @State : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IState
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@State[] pool = new GRGEN_MODEL.@State[10];
		
		// explicit initializations of State for target State
		// implicit initializations of State for target State
		static @State() {
		}
		
		public @State() : base(GRGEN_MODEL.NodeType_State.typeVar)
		{
			// implicit initialization, map/set/array creation of State
			// explicit initializations of State for target State
		}

		public static GRGEN_MODEL.NodeType_State TypeInstance { get { return GRGEN_MODEL.NodeType_State.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@State(this); }

		private @State(GRGEN_MODEL.@State oldElem) : base(GRGEN_MODEL.NodeType_State.typeVar)
		{
		}
		public static GRGEN_MODEL.@State CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@State node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@State();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of State
				// explicit initializations of State for target State
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@State CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@State node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@State();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of State
				// explicit initializations of State for target State
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
			// implicit initialization, map/set/array creation of State
			// explicit initializations of State for target State
		}
	}

	public sealed class NodeType_State : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_State typeVar = new GRGEN_MODEL.NodeType_State();
		public static bool[] isA = new bool[] { true, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, };
		public NodeType_State() : base((int) NodeTypes.@State)
		{
		}
		public override string Name { get { return "State"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IState"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@State"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@State();
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
			return new GRGEN_MODEL.@State();
		}

	}

	// *** Node WriteValue ***

	public interface IWriteValue : GRGEN_LIBGR.INode
	{
		int @value { get; set; }
	}

	public sealed class @WriteValue : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IWriteValue
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@WriteValue[] pool = new GRGEN_MODEL.@WriteValue[10];
		
		// explicit initializations of WriteValue for target WriteValue
		// implicit initializations of WriteValue for target WriteValue
		static @WriteValue() {
		}
		
		public @WriteValue() : base(GRGEN_MODEL.NodeType_WriteValue.typeVar)
		{
			// implicit initialization, map/set/array creation of WriteValue
			// explicit initializations of WriteValue for target WriteValue
		}

		public static GRGEN_MODEL.NodeType_WriteValue TypeInstance { get { return GRGEN_MODEL.NodeType_WriteValue.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@WriteValue(this); }

		private @WriteValue(GRGEN_MODEL.@WriteValue oldElem) : base(GRGEN_MODEL.NodeType_WriteValue.typeVar)
		{
			value_M0no_suXx_h4rD = oldElem.value_M0no_suXx_h4rD;
		}
		public static GRGEN_MODEL.@WriteValue CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@WriteValue node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of WriteValue
				node.@value = 0;
				// explicit initializations of WriteValue for target WriteValue
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@WriteValue CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@WriteValue node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@WriteValue();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of WriteValue
				node.@value = 0;
				// explicit initializations of WriteValue for target WriteValue
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int value_M0no_suXx_h4rD;
		public int @value
		{
			get { return value_M0no_suXx_h4rD; }
			set { value_M0no_suXx_h4rD = value; }
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
			// implicit initialization, map/set/array creation of WriteValue
			this.@value = 0;
			// explicit initializations of WriteValue for target WriteValue
		}
	}

	public sealed class NodeType_WriteValue : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_WriteValue typeVar = new GRGEN_MODEL.NodeType_WriteValue();
		public static bool[] isA = new bool[] { true, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, true, };
		public static GRGEN_LIBGR.AttributeType AttributeType_value;
		public NodeType_WriteValue() : base((int) NodeTypes.@WriteValue)
		{
			AttributeType_value = new GRGEN_LIBGR.AttributeType("value", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null);
		}
		public override string Name { get { return "WriteValue"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IWriteValue"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@WriteValue"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@WriteValue();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 1; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_value;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "value" : return AttributeType_value;
			}
			return null;
		}
		public override bool IsA(GRGEN_LIBGR.GrGenType other)
		{
			return (this == other) || isA[other.TypeID];
		}
		public override GRGEN_LIBGR.INode CreateNodeWithCopyCommons(GRGEN_LIBGR.INode oldINode)
		{
			GRGEN_LGSP.LGSPNode oldNode = (GRGEN_LGSP.LGSPNode) oldINode;
			GRGEN_MODEL.@WriteValue newNode = new GRGEN_MODEL.@WriteValue();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@WriteValue:
					// copy attributes for: WriteValue
					{
						GRGEN_MODEL.IWriteValue old = (GRGEN_MODEL.IWriteValue) oldNode;
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

	public sealed class Turing3NodeModel : GRGEN_LIBGR.INodeModel
	{
		public Turing3NodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
				GRGEN_MODEL.NodeType_State.typeVar,
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
				GRGEN_MODEL.NodeType_State.typeVar,
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_BandPosition.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_BandPosition.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_BandPosition.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_State.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_State.typeVar,
			};
			GRGEN_MODEL.NodeType_State.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_State.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_State.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_State.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_State.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_WriteValue.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_WriteValue.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_WriteValue.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
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
				case "BandPosition" : return GRGEN_MODEL.NodeType_BandPosition.typeVar;
				case "State" : return GRGEN_MODEL.NodeType_State.typeVar;
				case "WriteValue" : return GRGEN_MODEL.NodeType_WriteValue.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_BandPosition.typeVar,
			GRGEN_MODEL.NodeType_State.typeVar,
			GRGEN_MODEL.NodeType_WriteValue.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_BandPosition),
			typeof(GRGEN_MODEL.NodeType_State),
			typeof(GRGEN_MODEL.NodeType_WriteValue),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.NodeType_BandPosition.AttributeType_value,
			GRGEN_MODEL.NodeType_WriteValue.AttributeType_value,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @right, @readZero, @readOne, @moveLeft, @moveRight };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, true, };
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
			// implicit initialization, map/set/array creation of Edge
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
				// implicit initialization, map/set/array creation of Edge
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
				// implicit initialization, map/set/array creation of Edge
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
			// implicit initialization, map/set/array creation of Edge
		}
	}

	public sealed class EdgeType_Edge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_Edge typeVar = new GRGEN_MODEL.EdgeType_Edge();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@Edge"; } }
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
			// implicit initialization, map/set/array creation of UEdge
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
				// implicit initialization, map/set/array creation of UEdge
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
				// implicit initialization, map/set/array creation of UEdge
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
			// implicit initialization, map/set/array creation of UEdge
		}
	}

	public sealed class EdgeType_UEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_UEdge typeVar = new GRGEN_MODEL.EdgeType_UEdge();
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@UEdge"; } }
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

	// *** Edge right ***

	public interface Iright : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @right : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.Iright
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@right[] pool = new GRGEN_MODEL.@right[10];
		
		// explicit initializations of right for target right
		// implicit initializations of right for target right
		static @right() {
		}
		
		public @right(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_right.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of right
			// explicit initializations of right for target right
		}

		public static GRGEN_MODEL.EdgeType_right TypeInstance { get { return GRGEN_MODEL.EdgeType_right.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@right(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @right(GRGEN_MODEL.@right oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_right.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@right CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@right edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of right
				// explicit initializations of right for target right
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@right CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@right edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@right(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of right
				// explicit initializations of right for target right
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
			// implicit initialization, map/set/array creation of right
			// explicit initializations of right for target right
		}
	}

	public sealed class EdgeType_right : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_right typeVar = new GRGEN_MODEL.EdgeType_right();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, false, false, false, };
		public EdgeType_right() : base((int) EdgeTypes.@right)
		{
		}
		public override string Name { get { return "right"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.Iright"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@right"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@right((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@right((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge readZero ***

	public interface IreadZero : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @readZero : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IreadZero
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@readZero[] pool = new GRGEN_MODEL.@readZero[10];
		
		// explicit initializations of readZero for target readZero
		// implicit initializations of readZero for target readZero
		static @readZero() {
		}
		
		public @readZero(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_readZero.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of readZero
			// explicit initializations of readZero for target readZero
		}

		public static GRGEN_MODEL.EdgeType_readZero TypeInstance { get { return GRGEN_MODEL.EdgeType_readZero.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@readZero(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @readZero(GRGEN_MODEL.@readZero oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_readZero.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@readZero CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@readZero edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of readZero
				// explicit initializations of readZero for target readZero
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@readZero CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@readZero edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readZero(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of readZero
				// explicit initializations of readZero for target readZero
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
			// implicit initialization, map/set/array creation of readZero
			// explicit initializations of readZero for target readZero
		}
	}

	public sealed class EdgeType_readZero : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_readZero typeVar = new GRGEN_MODEL.EdgeType_readZero();
		public static bool[] isA = new bool[] { true, true, false, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, false, };
		public EdgeType_readZero() : base((int) EdgeTypes.@readZero)
		{
		}
		public override string Name { get { return "readZero"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IreadZero"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@readZero"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@readZero((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@readZero((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge readOne ***

	public interface IreadOne : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @readOne : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IreadOne
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@readOne[] pool = new GRGEN_MODEL.@readOne[10];
		
		// explicit initializations of readOne for target readOne
		// implicit initializations of readOne for target readOne
		static @readOne() {
		}
		
		public @readOne(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_readOne.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of readOne
			// explicit initializations of readOne for target readOne
		}

		public static GRGEN_MODEL.EdgeType_readOne TypeInstance { get { return GRGEN_MODEL.EdgeType_readOne.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@readOne(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @readOne(GRGEN_MODEL.@readOne oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_readOne.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@readOne CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@readOne edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of readOne
				// explicit initializations of readOne for target readOne
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@readOne CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@readOne edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@readOne(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of readOne
				// explicit initializations of readOne for target readOne
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
			// implicit initialization, map/set/array creation of readOne
			// explicit initializations of readOne for target readOne
		}
	}

	public sealed class EdgeType_readOne : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_readOne typeVar = new GRGEN_MODEL.EdgeType_readOne();
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, false, };
		public EdgeType_readOne() : base((int) EdgeTypes.@readOne)
		{
		}
		public override string Name { get { return "readOne"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.IreadOne"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@readOne"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@readOne((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@readOne((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge moveLeft ***

	public interface ImoveLeft : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @moveLeft : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.ImoveLeft
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@moveLeft[] pool = new GRGEN_MODEL.@moveLeft[10];
		
		// explicit initializations of moveLeft for target moveLeft
		// implicit initializations of moveLeft for target moveLeft
		static @moveLeft() {
		}
		
		public @moveLeft(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_moveLeft.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of moveLeft
			// explicit initializations of moveLeft for target moveLeft
		}

		public static GRGEN_MODEL.EdgeType_moveLeft TypeInstance { get { return GRGEN_MODEL.EdgeType_moveLeft.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@moveLeft(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @moveLeft(GRGEN_MODEL.@moveLeft oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_moveLeft.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@moveLeft CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@moveLeft edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of moveLeft
				// explicit initializations of moveLeft for target moveLeft
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@moveLeft CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@moveLeft edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveLeft(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of moveLeft
				// explicit initializations of moveLeft for target moveLeft
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
			// implicit initialization, map/set/array creation of moveLeft
			// explicit initializations of moveLeft for target moveLeft
		}
	}

	public sealed class EdgeType_moveLeft : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_moveLeft typeVar = new GRGEN_MODEL.EdgeType_moveLeft();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, false, };
		public EdgeType_moveLeft() : base((int) EdgeTypes.@moveLeft)
		{
		}
		public override string Name { get { return "moveLeft"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.ImoveLeft"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@moveLeft"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@moveLeft((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@moveLeft((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge moveRight ***

	public interface ImoveRight : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @moveRight : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.ImoveRight
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@moveRight[] pool = new GRGEN_MODEL.@moveRight[10];
		
		// explicit initializations of moveRight for target moveRight
		// implicit initializations of moveRight for target moveRight
		static @moveRight() {
		}
		
		public @moveRight(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_moveRight.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of moveRight
			// explicit initializations of moveRight for target moveRight
		}

		public static GRGEN_MODEL.EdgeType_moveRight TypeInstance { get { return GRGEN_MODEL.EdgeType_moveRight.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@moveRight(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @moveRight(GRGEN_MODEL.@moveRight oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_moveRight.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@moveRight CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@moveRight edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of moveRight
				// explicit initializations of moveRight for target moveRight
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@moveRight CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@moveRight edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@moveRight(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of moveRight
				// explicit initializations of moveRight for target moveRight
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
			// implicit initialization, map/set/array creation of moveRight
			// explicit initializations of moveRight for target moveRight
		}
	}

	public sealed class EdgeType_moveRight : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_moveRight typeVar = new GRGEN_MODEL.EdgeType_moveRight();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, false, true, };
		public EdgeType_moveRight() : base((int) EdgeTypes.@moveRight)
		{
		}
		public override string Name { get { return "moveRight"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_Turing3.ImoveRight"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_Turing3.@moveRight"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@moveRight((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@moveRight((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class Turing3EdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public Turing3EdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
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
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
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
			GRGEN_MODEL.EdgeType_right.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_right.typeVar,
			};
			GRGEN_MODEL.EdgeType_right.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_right.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_right.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_right.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_right.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readZero.typeVar,
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readZero.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readZero.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_readZero.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readOne.typeVar,
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_readOne.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_readOne.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_readOne.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveLeft.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_moveLeft.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_moveRight.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_moveRight.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_moveRight.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
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
				case "right" : return GRGEN_MODEL.EdgeType_right.typeVar;
				case "readZero" : return GRGEN_MODEL.EdgeType_readZero.typeVar;
				case "readOne" : return GRGEN_MODEL.EdgeType_readOne.typeVar;
				case "moveLeft" : return GRGEN_MODEL.EdgeType_moveLeft.typeVar;
				case "moveRight" : return GRGEN_MODEL.EdgeType_moveRight.typeVar;
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
			GRGEN_MODEL.EdgeType_right.typeVar,
			GRGEN_MODEL.EdgeType_readZero.typeVar,
			GRGEN_MODEL.EdgeType_readOne.typeVar,
			GRGEN_MODEL.EdgeType_moveLeft.typeVar,
			GRGEN_MODEL.EdgeType_moveRight.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_right),
			typeof(GRGEN_MODEL.EdgeType_readZero),
			typeof(GRGEN_MODEL.EdgeType_readOne),
			typeof(GRGEN_MODEL.EdgeType_moveLeft),
			typeof(GRGEN_MODEL.EdgeType_moveRight),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//
	public sealed class Turing3GraphModel : GRGEN_LIBGR.IGraphModel
	{
		private Turing3NodeModel nodeModel = new Turing3NodeModel();
		private Turing3EdgeModel edgeModel = new Turing3EdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_right.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, 0, 1, 0, 1, false),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "Turing3"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
	}

	//
	// IGraph/IGraphModel implementation
	//
	public class Turing3Graph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public Turing3Graph() : base(GetNextGraphName())
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

		public GRGEN_MODEL.@BandPosition CreateNodeBandPosition()
		{
			return GRGEN_MODEL.@BandPosition.CreateNode(this);
		}

		public GRGEN_MODEL.@BandPosition CreateNodeBandPosition(string varName)
		{
			return GRGEN_MODEL.@BandPosition.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@State CreateNodeState()
		{
			return GRGEN_MODEL.@State.CreateNode(this);
		}

		public GRGEN_MODEL.@State CreateNodeState(string varName)
		{
			return GRGEN_MODEL.@State.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@WriteValue CreateNodeWriteValue()
		{
			return GRGEN_MODEL.@WriteValue.CreateNode(this);
		}

		public GRGEN_MODEL.@WriteValue CreateNodeWriteValue(string varName)
		{
			return GRGEN_MODEL.@WriteValue.CreateNode(this, varName);
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

		public @GRGEN_MODEL.@right CreateEdgeright(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@right.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@right CreateEdgeright(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@right.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@readZero CreateEdgereadZero(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@readZero.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@readZero CreateEdgereadZero(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@readZero.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@readOne CreateEdgereadOne(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@readOne.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@readOne CreateEdgereadOne(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@readOne.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@moveLeft CreateEdgemoveLeft(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@moveLeft.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@moveLeft CreateEdgemoveLeft(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@moveLeft.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@moveRight CreateEdgemoveRight(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@moveRight.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@moveRight CreateEdgemoveRight(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@moveRight.CreateEdge(this, source, target, varName);
		}

		private Turing3NodeModel nodeModel = new Turing3NodeModel();
		private Turing3EdgeModel edgeModel = new Turing3EdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_right.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, GRGEN_MODEL.NodeType_BandPosition.typeVar, 0, 1, 0, 1, false),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "Turing3"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "3f4f1e3e3ccd5475eeca1ab5c25802bc"; } }
	}
}
