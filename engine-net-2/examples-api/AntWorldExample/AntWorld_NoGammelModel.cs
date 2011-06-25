// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\antWorld\AntWorld_ExtendAtEndOfRound_NoGammel.grg" on Sat Jun 25 14:38:13 CEST 2011

using System;
using System.Collections.Generic;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;

namespace de.unika.ipd.grGen.Model_AntWorld_NoGammel
{
	using GRGEN_MODEL = de.unika.ipd.grGen.Model_AntWorld_NoGammel;
	//
	// Enums
	//

	public class Enums
	{
	}

	//
	// Node types
	//

	public enum NodeTypes { @Node, @GridNode, @GridCornerNode, @AntHill, @Ant };

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
		public static bool[] isA = new bool[] { true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, };
		public NodeType_Node() : base((int) NodeTypes.@Node)
		{
		}
		public override string Name { get { return "Node"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.libGr.INode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@Node"; } }
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

	// *** Node GridNode ***

	public interface IGridNode : GRGEN_LIBGR.INode
	{
		int @food { get; set; }
		int @pheromones { get; set; }
	}

	public sealed class @GridNode : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IGridNode
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@GridNode[] pool = new GRGEN_MODEL.@GridNode[10];
		
		// explicit initializations of GridNode for target GridNode
		// implicit initializations of GridNode for target GridNode
		static @GridNode() {
		}
		
		public @GridNode() : base(GRGEN_MODEL.NodeType_GridNode.typeVar)
		{
			// implicit initialization, map/set/array creation of GridNode
			// explicit initializations of GridNode for target GridNode
		}

		public static GRGEN_MODEL.NodeType_GridNode TypeInstance { get { return GRGEN_MODEL.NodeType_GridNode.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@GridNode(this); }

		private @GridNode(GRGEN_MODEL.@GridNode oldElem) : base(GRGEN_MODEL.NodeType_GridNode.typeVar)
		{
			food_M0no_suXx_h4rD = oldElem.food_M0no_suXx_h4rD;
			pheromones_M0no_suXx_h4rD = oldElem.pheromones_M0no_suXx_h4rD;
		}
		public static GRGEN_MODEL.@GridNode CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@GridNode node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@GridNode();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of GridNode
				node.@food = 0;
				node.@pheromones = 0;
				// explicit initializations of GridNode for target GridNode
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@GridNode CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@GridNode node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@GridNode();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of GridNode
				node.@food = 0;
				node.@pheromones = 0;
				// explicit initializations of GridNode for target GridNode
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int food_M0no_suXx_h4rD;
		public int @food
		{
			get { return food_M0no_suXx_h4rD; }
			set { food_M0no_suXx_h4rD = value; }
		}

		private int pheromones_M0no_suXx_h4rD;
		public int @pheromones
		{
			get { return pheromones_M0no_suXx_h4rD; }
			set { pheromones_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "food": return this.@food;
				case "pheromones": return this.@pheromones;
			}
			throw new NullReferenceException(
				"The node type \"GridNode\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "food": this.@food = (int) value; return;
				case "pheromones": this.@pheromones = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"GridNode\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of GridNode
			this.@food = 0;
			this.@pheromones = 0;
			// explicit initializations of GridNode for target GridNode
		}
	}

	public sealed class NodeType_GridNode : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_GridNode typeVar = new GRGEN_MODEL.NodeType_GridNode();
		public static bool[] isA = new bool[] { true, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, true, true, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_food;
		public static GRGEN_LIBGR.AttributeType AttributeType_pheromones;
		public NodeType_GridNode() : base((int) NodeTypes.@GridNode)
		{
			AttributeType_food = new GRGEN_LIBGR.AttributeType("food", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null);
			AttributeType_pheromones = new GRGEN_LIBGR.AttributeType("pheromones", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null);
		}
		public override string Name { get { return "GridNode"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.IGridNode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@GridNode"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@GridNode();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return AttributeType_food;
				yield return AttributeType_pheromones;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "food" : return AttributeType_food;
				case "pheromones" : return AttributeType_pheromones;
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
			GRGEN_MODEL.@GridNode newNode = new GRGEN_MODEL.@GridNode();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@GridNode:
				case (int) NodeTypes.@GridCornerNode:
				case (int) NodeTypes.@AntHill:
					// copy attributes for: GridNode
					{
						GRGEN_MODEL.IGridNode old = (GRGEN_MODEL.IGridNode) oldNode;
						newNode.@food = old.@food;
						newNode.@pheromones = old.@pheromones;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node GridCornerNode ***

	public interface IGridCornerNode : IGridNode
	{
	}

	public sealed class @GridCornerNode : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IGridCornerNode
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@GridCornerNode[] pool = new GRGEN_MODEL.@GridCornerNode[10];
		
		// explicit initializations of GridNode for target GridCornerNode
		// implicit initializations of GridNode for target GridCornerNode
		// explicit initializations of GridCornerNode for target GridCornerNode
		// implicit initializations of GridCornerNode for target GridCornerNode
		static @GridCornerNode() {
		}
		
		public @GridCornerNode() : base(GRGEN_MODEL.NodeType_GridCornerNode.typeVar)
		{
			// implicit initialization, map/set/array creation of GridCornerNode
			// explicit initializations of GridNode for target GridCornerNode
			// explicit initializations of GridCornerNode for target GridCornerNode
		}

		public static GRGEN_MODEL.NodeType_GridCornerNode TypeInstance { get { return GRGEN_MODEL.NodeType_GridCornerNode.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@GridCornerNode(this); }

		private @GridCornerNode(GRGEN_MODEL.@GridCornerNode oldElem) : base(GRGEN_MODEL.NodeType_GridCornerNode.typeVar)
		{
			food_M0no_suXx_h4rD = oldElem.food_M0no_suXx_h4rD;
			pheromones_M0no_suXx_h4rD = oldElem.pheromones_M0no_suXx_h4rD;
		}
		public static GRGEN_MODEL.@GridCornerNode CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@GridCornerNode node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@GridCornerNode();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of GridCornerNode
				node.@food = 0;
				node.@pheromones = 0;
				// explicit initializations of GridNode for target GridCornerNode
				// explicit initializations of GridCornerNode for target GridCornerNode
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@GridCornerNode CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@GridCornerNode node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@GridCornerNode();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of GridCornerNode
				node.@food = 0;
				node.@pheromones = 0;
				// explicit initializations of GridNode for target GridCornerNode
				// explicit initializations of GridCornerNode for target GridCornerNode
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int food_M0no_suXx_h4rD;
		public int @food
		{
			get { return food_M0no_suXx_h4rD; }
			set { food_M0no_suXx_h4rD = value; }
		}

		private int pheromones_M0no_suXx_h4rD;
		public int @pheromones
		{
			get { return pheromones_M0no_suXx_h4rD; }
			set { pheromones_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "food": return this.@food;
				case "pheromones": return this.@pheromones;
			}
			throw new NullReferenceException(
				"The node type \"GridCornerNode\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "food": this.@food = (int) value; return;
				case "pheromones": this.@pheromones = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"GridCornerNode\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of GridCornerNode
			this.@food = 0;
			this.@pheromones = 0;
			// explicit initializations of GridNode for target GridCornerNode
			// explicit initializations of GridCornerNode for target GridCornerNode
		}
	}

	public sealed class NodeType_GridCornerNode : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_GridCornerNode typeVar = new GRGEN_MODEL.NodeType_GridCornerNode();
		public static bool[] isA = new bool[] { true, true, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, };
		public NodeType_GridCornerNode() : base((int) NodeTypes.@GridCornerNode)
		{
		}
		public override string Name { get { return "GridCornerNode"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.IGridCornerNode"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@GridCornerNode"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@GridCornerNode();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 2; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_GridNode.AttributeType_food;
				yield return GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "food" : return GRGEN_MODEL.NodeType_GridNode.AttributeType_food;
				case "pheromones" : return GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones;
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
			GRGEN_MODEL.@GridCornerNode newNode = new GRGEN_MODEL.@GridCornerNode();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@GridNode:
				case (int) NodeTypes.@AntHill:
					// copy attributes for: GridNode
					{
						GRGEN_MODEL.IGridNode old = (GRGEN_MODEL.IGridNode) oldNode;
						newNode.@food = old.@food;
						newNode.@pheromones = old.@pheromones;
					}
					break;
				case (int) NodeTypes.@GridCornerNode:
					// copy attributes for: GridCornerNode
					{
						GRGEN_MODEL.IGridCornerNode old = (GRGEN_MODEL.IGridCornerNode) oldNode;
						newNode.@food = old.@food;
						newNode.@pheromones = old.@pheromones;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node AntHill ***

	public interface IAntHill : IGridNode
	{
		int @foodCountdown { get; set; }
	}

	public sealed class @AntHill : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IAntHill
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@AntHill[] pool = new GRGEN_MODEL.@AntHill[10];
		
		// explicit initializations of GridNode for target AntHill
		// implicit initializations of GridNode for target AntHill
		// explicit initializations of AntHill for target AntHill
		// implicit initializations of AntHill for target AntHill
		static @AntHill() {
		}
		
		public @AntHill() : base(GRGEN_MODEL.NodeType_AntHill.typeVar)
		{
			// implicit initialization, map/set/array creation of AntHill
			// explicit initializations of GridNode for target AntHill
			// explicit initializations of AntHill for target AntHill
			this.@foodCountdown = 10;
		}

		public static GRGEN_MODEL.NodeType_AntHill TypeInstance { get { return GRGEN_MODEL.NodeType_AntHill.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@AntHill(this); }

		private @AntHill(GRGEN_MODEL.@AntHill oldElem) : base(GRGEN_MODEL.NodeType_AntHill.typeVar)
		{
			food_M0no_suXx_h4rD = oldElem.food_M0no_suXx_h4rD;
			pheromones_M0no_suXx_h4rD = oldElem.pheromones_M0no_suXx_h4rD;
			foodCountdown_M0no_suXx_h4rD = oldElem.foodCountdown_M0no_suXx_h4rD;
		}
		public static GRGEN_MODEL.@AntHill CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@AntHill node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@AntHill();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of AntHill
				node.@food = 0;
				node.@pheromones = 0;
				node.@foodCountdown = 0;
				// explicit initializations of GridNode for target AntHill
				// explicit initializations of AntHill for target AntHill
				node.@foodCountdown = 10;
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@AntHill CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@AntHill node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@AntHill();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of AntHill
				node.@food = 0;
				node.@pheromones = 0;
				node.@foodCountdown = 0;
				// explicit initializations of GridNode for target AntHill
				// explicit initializations of AntHill for target AntHill
				node.@foodCountdown = 10;
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private int food_M0no_suXx_h4rD;
		public int @food
		{
			get { return food_M0no_suXx_h4rD; }
			set { food_M0no_suXx_h4rD = value; }
		}

		private int pheromones_M0no_suXx_h4rD;
		public int @pheromones
		{
			get { return pheromones_M0no_suXx_h4rD; }
			set { pheromones_M0no_suXx_h4rD = value; }
		}

		private int foodCountdown_M0no_suXx_h4rD;
		public int @foodCountdown
		{
			get { return foodCountdown_M0no_suXx_h4rD; }
			set { foodCountdown_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "food": return this.@food;
				case "pheromones": return this.@pheromones;
				case "foodCountdown": return this.@foodCountdown;
			}
			throw new NullReferenceException(
				"The node type \"AntHill\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "food": this.@food = (int) value; return;
				case "pheromones": this.@pheromones = (int) value; return;
				case "foodCountdown": this.@foodCountdown = (int) value; return;
			}
			throw new NullReferenceException(
				"The node type \"AntHill\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of AntHill
			this.@food = 0;
			this.@pheromones = 0;
			this.@foodCountdown = 0;
			// explicit initializations of GridNode for target AntHill
			// explicit initializations of AntHill for target AntHill
			this.@foodCountdown = 10;
		}
	}

	public sealed class NodeType_AntHill : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_AntHill typeVar = new GRGEN_MODEL.NodeType_AntHill();
		public static bool[] isA = new bool[] { true, true, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, false, };
		public static GRGEN_LIBGR.AttributeType AttributeType_foodCountdown;
		public NodeType_AntHill() : base((int) NodeTypes.@AntHill)
		{
			AttributeType_foodCountdown = new GRGEN_LIBGR.AttributeType("foodCountdown", this, GRGEN_LIBGR.AttributeKind.IntegerAttr, null, null, null, null);
		}
		public override string Name { get { return "AntHill"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.IAntHill"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@AntHill"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@AntHill();
		}
		public override bool IsAbstract { get { return false; } }
		public override bool IsConst { get { return false; } }
		public override IEnumerable<KeyValuePair<string, string>> Annotations { get { return annotations; } }
		public IDictionary<string, string> annotations = new Dictionary<string, string>();
		public override int NumAttributes { get { return 3; } }
		public override IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes
		{
			get
			{
				yield return GRGEN_MODEL.NodeType_GridNode.AttributeType_food;
				yield return GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones;
				yield return AttributeType_foodCountdown;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "food" : return GRGEN_MODEL.NodeType_GridNode.AttributeType_food;
				case "pheromones" : return GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones;
				case "foodCountdown" : return AttributeType_foodCountdown;
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
			GRGEN_MODEL.@AntHill newNode = new GRGEN_MODEL.@AntHill();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@GridNode:
				case (int) NodeTypes.@GridCornerNode:
					// copy attributes for: GridNode
					{
						GRGEN_MODEL.IGridNode old = (GRGEN_MODEL.IGridNode) oldNode;
						newNode.@food = old.@food;
						newNode.@pheromones = old.@pheromones;
					}
					break;
				case (int) NodeTypes.@AntHill:
					// copy attributes for: AntHill
					{
						GRGEN_MODEL.IAntHill old = (GRGEN_MODEL.IAntHill) oldNode;
						newNode.@food = old.@food;
						newNode.@pheromones = old.@pheromones;
						newNode.@foodCountdown = old.@foodCountdown;
					}
					break;
			}
			return newNode;
		}

	}

	// *** Node Ant ***

	public interface IAnt : GRGEN_LIBGR.INode
	{
		bool @hasFood { get; set; }
	}

	public sealed class @Ant : GRGEN_LGSP.LGSPNode, GRGEN_MODEL.IAnt
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@Ant[] pool = new GRGEN_MODEL.@Ant[10];
		
		// explicit initializations of Ant for target Ant
		// implicit initializations of Ant for target Ant
		static @Ant() {
		}
		
		public @Ant() : base(GRGEN_MODEL.NodeType_Ant.typeVar)
		{
			// implicit initialization, map/set/array creation of Ant
			// explicit initializations of Ant for target Ant
		}

		public static GRGEN_MODEL.NodeType_Ant TypeInstance { get { return GRGEN_MODEL.NodeType_Ant.typeVar; } }

		public override GRGEN_LIBGR.INode Clone() { return new GRGEN_MODEL.@Ant(this); }

		private @Ant(GRGEN_MODEL.@Ant oldElem) : base(GRGEN_MODEL.NodeType_Ant.typeVar)
		{
			hasFood_M0no_suXx_h4rD = oldElem.hasFood_M0no_suXx_h4rD;
		}
		public static GRGEN_MODEL.@Ant CreateNode(GRGEN_LGSP.LGSPGraph graph)
		{
			GRGEN_MODEL.@Ant node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Ant();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of Ant
				node.@hasFood = false;
				// explicit initializations of Ant for target Ant
			}
			graph.AddNode(node);
			return node;
		}

		public static GRGEN_MODEL.@Ant CreateNode(GRGEN_LGSP.LGSPGraph graph, string varName)
		{
			GRGEN_MODEL.@Ant node;
			if(poolLevel == 0)
				node = new GRGEN_MODEL.@Ant();
			else
			{
				node = pool[--poolLevel];
				node.lgspInhead = null;
				node.lgspOuthead = null;
				node.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				// implicit initialization, map/set/array creation of Ant
				node.@hasFood = false;
				// explicit initializations of Ant for target Ant
			}
			graph.AddNode(node, varName);
			return node;
		}

		public override void Recycle()
		{
			if(poolLevel < 10)
				pool[poolLevel++] = this;
		}


		private bool hasFood_M0no_suXx_h4rD;
		public bool @hasFood
		{
			get { return hasFood_M0no_suXx_h4rD; }
			set { hasFood_M0no_suXx_h4rD = value; }
		}
		public override object GetAttribute(string attrName)
		{
			switch(attrName)
			{
				case "hasFood": return this.@hasFood;
			}
			throw new NullReferenceException(
				"The node type \"Ant\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			switch(attrName)
			{
				case "hasFood": this.@hasFood = (bool) value; return;
			}
			throw new NullReferenceException(
				"The node type \"Ant\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of Ant
			this.@hasFood = false;
			// explicit initializations of Ant for target Ant
		}
	}

	public sealed class NodeType_Ant : GRGEN_LIBGR.NodeType
	{
		public static GRGEN_MODEL.NodeType_Ant typeVar = new GRGEN_MODEL.NodeType_Ant();
		public static bool[] isA = new bool[] { true, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, };
		public static GRGEN_LIBGR.AttributeType AttributeType_hasFood;
		public NodeType_Ant() : base((int) NodeTypes.@Ant)
		{
			AttributeType_hasFood = new GRGEN_LIBGR.AttributeType("hasFood", this, GRGEN_LIBGR.AttributeKind.BooleanAttr, null, null, null, null);
		}
		public override string Name { get { return "Ant"; } }
		public override string NodeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.IAnt"; } }
		public override string NodeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@Ant"; } }
		public override GRGEN_LIBGR.INode CreateNode()
		{
			return new GRGEN_MODEL.@Ant();
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
				yield return AttributeType_hasFood;
			}
		}
		public override GRGEN_LIBGR.AttributeType GetAttributeType(string name)
		{
			switch(name)
			{
				case "hasFood" : return AttributeType_hasFood;
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
			GRGEN_MODEL.@Ant newNode = new GRGEN_MODEL.@Ant();
			switch(oldNode.Type.TypeID)
			{
				case (int) NodeTypes.@Ant:
					// copy attributes for: Ant
					{
						GRGEN_MODEL.IAnt old = (GRGEN_MODEL.IAnt) oldNode;
						newNode.@hasFood = old.@hasFood;
					}
					break;
			}
			return newNode;
		}

	}

	//
	// Node model
	//

	public sealed class AntWorld_NoGammelNodeModel : GRGEN_LIBGR.INodeModel
	{
		public AntWorld_NoGammelNodeModel()
		{
			GRGEN_MODEL.NodeType_Node.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_GridNode.typeVar,
				GRGEN_MODEL.NodeType_GridCornerNode.typeVar,
				GRGEN_MODEL.NodeType_AntHill.typeVar,
				GRGEN_MODEL.NodeType_Ant.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridNode.typeVar,
				GRGEN_MODEL.NodeType_Ant.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Node.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Node.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_GridNode.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_GridNode.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridNode.typeVar,
				GRGEN_MODEL.NodeType_GridCornerNode.typeVar,
				GRGEN_MODEL.NodeType_AntHill.typeVar,
			};
			GRGEN_MODEL.NodeType_GridNode.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_GridNode.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridCornerNode.typeVar,
				GRGEN_MODEL.NodeType_AntHill.typeVar,
			};
			GRGEN_MODEL.NodeType_GridNode.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_GridNode.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridNode.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_GridNode.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_GridNode.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_GridCornerNode.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_GridCornerNode.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridCornerNode.typeVar,
			};
			GRGEN_MODEL.NodeType_GridCornerNode.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_GridCornerNode.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_GridCornerNode.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_GridCornerNode.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridCornerNode.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_GridNode.typeVar,
			};
			GRGEN_MODEL.NodeType_GridCornerNode.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_GridCornerNode.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridNode.typeVar,
			};
			GRGEN_MODEL.NodeType_AntHill.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_AntHill.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_AntHill.typeVar,
			};
			GRGEN_MODEL.NodeType_AntHill.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_AntHill.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_AntHill.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_AntHill.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_AntHill.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
				GRGEN_MODEL.NodeType_GridNode.typeVar,
			};
			GRGEN_MODEL.NodeType_AntHill.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_AntHill.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_GridNode.typeVar,
			};
			GRGEN_MODEL.NodeType_Ant.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.NodeType_Ant.typeVar.subOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Ant.typeVar,
			};
			GRGEN_MODEL.NodeType_Ant.typeVar.directSubGrGenTypes = GRGEN_MODEL.NodeType_Ant.typeVar.directSubTypes = new GRGEN_LIBGR.NodeType[] {
			};
			GRGEN_MODEL.NodeType_Ant.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.NodeType_Ant.typeVar.superOrSameTypes = new GRGEN_LIBGR.NodeType[] {
				GRGEN_MODEL.NodeType_Ant.typeVar,
				GRGEN_MODEL.NodeType_Node.typeVar,
			};
			GRGEN_MODEL.NodeType_Ant.typeVar.directSuperGrGenTypes = GRGEN_MODEL.NodeType_Ant.typeVar.directSuperTypes = new GRGEN_LIBGR.NodeType[] {
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
				case "GridNode" : return GRGEN_MODEL.NodeType_GridNode.typeVar;
				case "GridCornerNode" : return GRGEN_MODEL.NodeType_GridCornerNode.typeVar;
				case "AntHill" : return GRGEN_MODEL.NodeType_AntHill.typeVar;
				case "Ant" : return GRGEN_MODEL.NodeType_Ant.typeVar;
			}
			return null;
		}
		GRGEN_LIBGR.GrGenType GRGEN_LIBGR.ITypeModel.GetType(string name)
		{
			return GetType(name);
		}
		private GRGEN_LIBGR.NodeType[] types = {
			GRGEN_MODEL.NodeType_Node.typeVar,
			GRGEN_MODEL.NodeType_GridNode.typeVar,
			GRGEN_MODEL.NodeType_GridCornerNode.typeVar,
			GRGEN_MODEL.NodeType_AntHill.typeVar,
			GRGEN_MODEL.NodeType_Ant.typeVar,
		};
		public GRGEN_LIBGR.NodeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.NodeType_Node),
			typeof(GRGEN_MODEL.NodeType_GridNode),
			typeof(GRGEN_MODEL.NodeType_GridCornerNode),
			typeof(GRGEN_MODEL.NodeType_AntHill),
			typeof(GRGEN_MODEL.NodeType_Ant),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
			GRGEN_MODEL.NodeType_GridNode.AttributeType_food,
			GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones,
			GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown,
			GRGEN_MODEL.NodeType_Ant.AttributeType_hasFood,
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// Edge types
	//

	public enum EdgeTypes { @AEdge, @Edge, @UEdge, @GridEdge, @PathToHill, @AntPosition, @NextAnt };

	// *** Edge AEdge ***


	public sealed class EdgeType_AEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AEdge typeVar = new GRGEN_MODEL.EdgeType_AEdge();
		public static bool[] isA = new bool[] { true, false, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { true, true, true, true, true, true, true, };
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
		public static bool[] isA = new bool[] { true, true, false, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, true, false, true, true, true, true, };
		public EdgeType_Edge() : base((int) EdgeTypes.@Edge)
		{
		}
		public override string Name { get { return "Edge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@Edge"; } }
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
		public static bool[] isA = new bool[] { true, false, true, false, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, true, false, false, false, false, };
		public EdgeType_UEdge() : base((int) EdgeTypes.@UEdge)
		{
		}
		public override string Name { get { return "UEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.libGr.IEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@UEdge"; } }
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

	// *** Edge GridEdge ***

	public interface IGridEdge : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @GridEdge : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IGridEdge
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@GridEdge[] pool = new GRGEN_MODEL.@GridEdge[10];
		
		// explicit initializations of GridEdge for target GridEdge
		// implicit initializations of GridEdge for target GridEdge
		static @GridEdge() {
		}
		
		public @GridEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_GridEdge.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of GridEdge
			// explicit initializations of GridEdge for target GridEdge
		}

		public static GRGEN_MODEL.EdgeType_GridEdge TypeInstance { get { return GRGEN_MODEL.EdgeType_GridEdge.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@GridEdge(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @GridEdge(GRGEN_MODEL.@GridEdge oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_GridEdge.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@GridEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@GridEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@GridEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of GridEdge
				// explicit initializations of GridEdge for target GridEdge
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@GridEdge CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@GridEdge edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@GridEdge(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of GridEdge
				// explicit initializations of GridEdge for target GridEdge
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
				"The edge type \"GridEdge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"GridEdge\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of GridEdge
			// explicit initializations of GridEdge for target GridEdge
		}
	}

	public sealed class EdgeType_GridEdge : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_GridEdge typeVar = new GRGEN_MODEL.EdgeType_GridEdge();
		public static bool[] isA = new bool[] { true, true, false, true, false, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, true, true, false, false, };
		public EdgeType_GridEdge() : base((int) EdgeTypes.@GridEdge)
		{
		}
		public override string Name { get { return "GridEdge"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.IGridEdge"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@GridEdge"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@GridEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@GridEdge((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge PathToHill ***

	public interface IPathToHill : IGridEdge
	{
	}

	public sealed class @PathToHill : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IPathToHill
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@PathToHill[] pool = new GRGEN_MODEL.@PathToHill[10];
		
		// explicit initializations of GridEdge for target PathToHill
		// implicit initializations of GridEdge for target PathToHill
		// explicit initializations of PathToHill for target PathToHill
		// implicit initializations of PathToHill for target PathToHill
		static @PathToHill() {
		}
		
		public @PathToHill(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_PathToHill.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of PathToHill
			// explicit initializations of GridEdge for target PathToHill
			// explicit initializations of PathToHill for target PathToHill
		}

		public static GRGEN_MODEL.EdgeType_PathToHill TypeInstance { get { return GRGEN_MODEL.EdgeType_PathToHill.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@PathToHill(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @PathToHill(GRGEN_MODEL.@PathToHill oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_PathToHill.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@PathToHill CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@PathToHill edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@PathToHill(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of PathToHill
				// explicit initializations of GridEdge for target PathToHill
				// explicit initializations of PathToHill for target PathToHill
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@PathToHill CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@PathToHill edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@PathToHill(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of PathToHill
				// explicit initializations of GridEdge for target PathToHill
				// explicit initializations of PathToHill for target PathToHill
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
				"The edge type \"PathToHill\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"PathToHill\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of PathToHill
			// explicit initializations of GridEdge for target PathToHill
			// explicit initializations of PathToHill for target PathToHill
		}
	}

	public sealed class EdgeType_PathToHill : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_PathToHill typeVar = new GRGEN_MODEL.EdgeType_PathToHill();
		public static bool[] isA = new bool[] { true, true, false, true, true, false, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, true, false, false, };
		public EdgeType_PathToHill() : base((int) EdgeTypes.@PathToHill)
		{
		}
		public override string Name { get { return "PathToHill"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.IPathToHill"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@PathToHill"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@PathToHill((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@PathToHill((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge AntPosition ***

	public interface IAntPosition : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @AntPosition : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.IAntPosition
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@AntPosition[] pool = new GRGEN_MODEL.@AntPosition[10];
		
		// explicit initializations of AntPosition for target AntPosition
		// implicit initializations of AntPosition for target AntPosition
		static @AntPosition() {
		}
		
		public @AntPosition(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_AntPosition.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of AntPosition
			// explicit initializations of AntPosition for target AntPosition
		}

		public static GRGEN_MODEL.EdgeType_AntPosition TypeInstance { get { return GRGEN_MODEL.EdgeType_AntPosition.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@AntPosition(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @AntPosition(GRGEN_MODEL.@AntPosition oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_AntPosition.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@AntPosition CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@AntPosition edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@AntPosition(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of AntPosition
				// explicit initializations of AntPosition for target AntPosition
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@AntPosition CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@AntPosition edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@AntPosition(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of AntPosition
				// explicit initializations of AntPosition for target AntPosition
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
				"The edge type \"AntPosition\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"AntPosition\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of AntPosition
			// explicit initializations of AntPosition for target AntPosition
		}
	}

	public sealed class EdgeType_AntPosition : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_AntPosition typeVar = new GRGEN_MODEL.EdgeType_AntPosition();
		public static bool[] isA = new bool[] { true, true, false, false, false, true, false, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, true, false, };
		public EdgeType_AntPosition() : base((int) EdgeTypes.@AntPosition)
		{
		}
		public override string Name { get { return "AntPosition"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.IAntPosition"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@AntPosition"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@AntPosition((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@AntPosition((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	// *** Edge NextAnt ***

	public interface INextAnt : GRGEN_LIBGR.IEdge
	{
	}

	public sealed class @NextAnt : GRGEN_LGSP.LGSPEdge, GRGEN_MODEL.INextAnt
	{
		private static int poolLevel = 0;
		private static GRGEN_MODEL.@NextAnt[] pool = new GRGEN_MODEL.@NextAnt[10];
		
		// explicit initializations of NextAnt for target NextAnt
		// implicit initializations of NextAnt for target NextAnt
		static @NextAnt() {
		}
		
		public @NextAnt(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
			: base(GRGEN_MODEL.EdgeType_NextAnt.typeVar, source, target)
		{
			// implicit initialization, map/set/array creation of NextAnt
			// explicit initializations of NextAnt for target NextAnt
		}

		public static GRGEN_MODEL.EdgeType_NextAnt TypeInstance { get { return GRGEN_MODEL.EdgeType_NextAnt.typeVar; } }

		public override GRGEN_LIBGR.IEdge Clone(GRGEN_LIBGR.INode newSource, GRGEN_LIBGR.INode newTarget)
		{ return new GRGEN_MODEL.@NextAnt(this, (GRGEN_LGSP.LGSPNode) newSource, (GRGEN_LGSP.LGSPNode) newTarget); }

		private @NextAnt(GRGEN_MODEL.@NextAnt oldElem, GRGEN_LGSP.LGSPNode newSource, GRGEN_LGSP.LGSPNode newTarget)
			: base(GRGEN_MODEL.EdgeType_NextAnt.typeVar, newSource, newTarget)
		{
		}
		public static GRGEN_MODEL.@NextAnt CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			GRGEN_MODEL.@NextAnt edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@NextAnt(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of NextAnt
				// explicit initializations of NextAnt for target NextAnt
			}
			graph.AddEdge(edge);
			return edge;
		}

		public static GRGEN_MODEL.@NextAnt CreateEdge(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			GRGEN_MODEL.@NextAnt edge;
			if(poolLevel == 0)
				edge = new GRGEN_MODEL.@NextAnt(source, target);
			else
			{
				edge = pool[--poolLevel];
				edge.lgspFlags &= ~(uint) GRGEN_LGSP.LGSPElemFlags.HAS_VARIABLES;
				edge.lgspSource = source;
				edge.lgspTarget = target;
				// implicit initialization, map/set/array creation of NextAnt
				// explicit initializations of NextAnt for target NextAnt
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
				"The edge type \"NextAnt\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void SetAttribute(string attrName, object value)
		{
			throw new NullReferenceException(
				"The edge type \"NextAnt\" does not have the attribute \" + attrName + \"\"!");
		}
		public override void ResetAllAttributes()
		{
			// implicit initialization, map/set/array creation of NextAnt
			// explicit initializations of NextAnt for target NextAnt
		}
	}

	public sealed class EdgeType_NextAnt : GRGEN_LIBGR.EdgeType
	{
		public static GRGEN_MODEL.EdgeType_NextAnt typeVar = new GRGEN_MODEL.EdgeType_NextAnt();
		public static bool[] isA = new bool[] { true, true, false, false, false, false, true, };
		public static bool[] isMyType = new bool[] { false, false, false, false, false, false, true, };
		public EdgeType_NextAnt() : base((int) EdgeTypes.@NextAnt)
		{
		}
		public override string Name { get { return "NextAnt"; } }
		public override string EdgeInterfaceName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.INextAnt"; } }
		public override string EdgeClassName { get { return "de.unika.ipd.grGen.Model_AntWorld_NoGammel.@NextAnt"; } }
		public override GRGEN_LIBGR.Directedness Directedness { get { return GRGEN_LIBGR.Directedness.Directed; } }
		public override GRGEN_LIBGR.IEdge CreateEdge(GRGEN_LIBGR.INode source, GRGEN_LIBGR.INode target)
		{
			return new GRGEN_MODEL.@NextAnt((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
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
			return new GRGEN_MODEL.@NextAnt((GRGEN_LGSP.LGSPNode) source, (GRGEN_LGSP.LGSPNode) target);
		}

	}

	//
	// Edge model
	//

	public sealed class AntWorld_NoGammelEdgeModel : GRGEN_LIBGR.IEdgeModel
	{
		public AntWorld_NoGammelEdgeModel()
		{
			GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_UEdge.typeVar,
				GRGEN_MODEL.EdgeType_GridEdge.typeVar,
				GRGEN_MODEL.EdgeType_PathToHill.typeVar,
				GRGEN_MODEL.EdgeType_AntPosition.typeVar,
				GRGEN_MODEL.EdgeType_NextAnt.typeVar,
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
				GRGEN_MODEL.EdgeType_GridEdge.typeVar,
				GRGEN_MODEL.EdgeType_PathToHill.typeVar,
				GRGEN_MODEL.EdgeType_AntPosition.typeVar,
				GRGEN_MODEL.EdgeType_NextAnt.typeVar,
			};
			GRGEN_MODEL.EdgeType_Edge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_Edge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_GridEdge.typeVar,
				GRGEN_MODEL.EdgeType_AntPosition.typeVar,
				GRGEN_MODEL.EdgeType_NextAnt.typeVar,
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
			GRGEN_MODEL.EdgeType_GridEdge.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_GridEdge.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_GridEdge.typeVar,
				GRGEN_MODEL.EdgeType_PathToHill.typeVar,
			};
			GRGEN_MODEL.EdgeType_GridEdge.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_GridEdge.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_PathToHill.typeVar,
			};
			GRGEN_MODEL.EdgeType_GridEdge.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_GridEdge.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_GridEdge.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_GridEdge.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_GridEdge.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_PathToHill.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_PathToHill.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_PathToHill.typeVar,
			};
			GRGEN_MODEL.EdgeType_PathToHill.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_PathToHill.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_PathToHill.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_PathToHill.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_PathToHill.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
				GRGEN_MODEL.EdgeType_GridEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_PathToHill.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_PathToHill.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_GridEdge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AntPosition.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AntPosition.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AntPosition.typeVar,
			};
			GRGEN_MODEL.EdgeType_AntPosition.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_AntPosition.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_AntPosition.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_AntPosition.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_AntPosition.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_AntPosition.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_AntPosition.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_NextAnt.typeVar.subOrSameGrGenTypes = GRGEN_MODEL.EdgeType_NextAnt.typeVar.subOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_NextAnt.typeVar,
			};
			GRGEN_MODEL.EdgeType_NextAnt.typeVar.directSubGrGenTypes = GRGEN_MODEL.EdgeType_NextAnt.typeVar.directSubTypes = new GRGEN_LIBGR.EdgeType[] {
			};
			GRGEN_MODEL.EdgeType_NextAnt.typeVar.superOrSameGrGenTypes = GRGEN_MODEL.EdgeType_NextAnt.typeVar.superOrSameTypes = new GRGEN_LIBGR.EdgeType[] {
				GRGEN_MODEL.EdgeType_NextAnt.typeVar,
				GRGEN_MODEL.EdgeType_AEdge.typeVar,
				GRGEN_MODEL.EdgeType_Edge.typeVar,
			};
			GRGEN_MODEL.EdgeType_NextAnt.typeVar.directSuperGrGenTypes = GRGEN_MODEL.EdgeType_NextAnt.typeVar.directSuperTypes = new GRGEN_LIBGR.EdgeType[] {
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
				case "GridEdge" : return GRGEN_MODEL.EdgeType_GridEdge.typeVar;
				case "PathToHill" : return GRGEN_MODEL.EdgeType_PathToHill.typeVar;
				case "AntPosition" : return GRGEN_MODEL.EdgeType_AntPosition.typeVar;
				case "NextAnt" : return GRGEN_MODEL.EdgeType_NextAnt.typeVar;
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
			GRGEN_MODEL.EdgeType_GridEdge.typeVar,
			GRGEN_MODEL.EdgeType_PathToHill.typeVar,
			GRGEN_MODEL.EdgeType_AntPosition.typeVar,
			GRGEN_MODEL.EdgeType_NextAnt.typeVar,
		};
		public GRGEN_LIBGR.EdgeType[] Types { get { return types; } }
		GRGEN_LIBGR.GrGenType[] GRGEN_LIBGR.ITypeModel.Types { get { return types; } }
		private System.Type[] typeTypes = {
			typeof(GRGEN_MODEL.EdgeType_AEdge),
			typeof(GRGEN_MODEL.EdgeType_Edge),
			typeof(GRGEN_MODEL.EdgeType_UEdge),
			typeof(GRGEN_MODEL.EdgeType_GridEdge),
			typeof(GRGEN_MODEL.EdgeType_PathToHill),
			typeof(GRGEN_MODEL.EdgeType_AntPosition),
			typeof(GRGEN_MODEL.EdgeType_NextAnt),
		};
		public System.Type[] TypeTypes { get { return typeTypes; } }
		private GRGEN_LIBGR.AttributeType[] attributeTypes = {
		};
		public IEnumerable<GRGEN_LIBGR.AttributeType> AttributeTypes { get { return attributeTypes; } }
	}

	//
	// IGraphModel implementation
	//
	public sealed class AntWorld_NoGammelGraphModel : GRGEN_LIBGR.IGraphModel
	{
		private AntWorld_NoGammelNodeModel nodeModel = new AntWorld_NoGammelNodeModel();
		private AntWorld_NoGammelEdgeModel edgeModel = new AntWorld_NoGammelEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_GridEdge.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, 1, 1, 1, 1, false),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "AntWorld_NoGammel"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "5efeccfb37eb4c2835fae110fe22d2e7"; } }
	}

	//
	// IGraph/IGraphModel implementation
	//
	public class AntWorld_NoGammelGraph : GRGEN_LGSP.LGSPGraph, GRGEN_LIBGR.IGraphModel
	{
		public AntWorld_NoGammelGraph() : base(GetNextGraphName())
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

		public GRGEN_MODEL.@GridNode CreateNodeGridNode()
		{
			return GRGEN_MODEL.@GridNode.CreateNode(this);
		}

		public GRGEN_MODEL.@GridNode CreateNodeGridNode(string varName)
		{
			return GRGEN_MODEL.@GridNode.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@GridCornerNode CreateNodeGridCornerNode()
		{
			return GRGEN_MODEL.@GridCornerNode.CreateNode(this);
		}

		public GRGEN_MODEL.@GridCornerNode CreateNodeGridCornerNode(string varName)
		{
			return GRGEN_MODEL.@GridCornerNode.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@AntHill CreateNodeAntHill()
		{
			return GRGEN_MODEL.@AntHill.CreateNode(this);
		}

		public GRGEN_MODEL.@AntHill CreateNodeAntHill(string varName)
		{
			return GRGEN_MODEL.@AntHill.CreateNode(this, varName);
		}

		public GRGEN_MODEL.@Ant CreateNodeAnt()
		{
			return GRGEN_MODEL.@Ant.CreateNode(this);
		}

		public GRGEN_MODEL.@Ant CreateNodeAnt(string varName)
		{
			return GRGEN_MODEL.@Ant.CreateNode(this, varName);
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

		public @GRGEN_MODEL.@GridEdge CreateEdgeGridEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@GridEdge.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@GridEdge CreateEdgeGridEdge(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@GridEdge.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@PathToHill CreateEdgePathToHill(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@PathToHill.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@PathToHill CreateEdgePathToHill(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@PathToHill.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@AntPosition CreateEdgeAntPosition(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@AntPosition.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@AntPosition CreateEdgeAntPosition(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@AntPosition.CreateEdge(this, source, target, varName);
		}

		public @GRGEN_MODEL.@NextAnt CreateEdgeNextAnt(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target)
		{
			return @GRGEN_MODEL.@NextAnt.CreateEdge(this, source, target);
		}

		public @GRGEN_MODEL.@NextAnt CreateEdgeNextAnt(GRGEN_LGSP.LGSPNode source, GRGEN_LGSP.LGSPNode target, string varName)
		{
			return @GRGEN_MODEL.@NextAnt.CreateEdge(this, source, target, varName);
		}

		private AntWorld_NoGammelNodeModel nodeModel = new AntWorld_NoGammelNodeModel();
		private AntWorld_NoGammelEdgeModel edgeModel = new AntWorld_NoGammelEdgeModel();
		private GRGEN_LIBGR.ValidateInfo[] validateInfos = {
			new GRGEN_LIBGR.ValidateInfo(GRGEN_MODEL.EdgeType_GridEdge.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, 1, 1, 1, 1, false),
		};
		private GRGEN_LIBGR.EnumAttributeType[] enumAttributeTypes = {
		};

		public string ModelName { get { return "AntWorld_NoGammel"; } }
		public GRGEN_LIBGR.INodeModel NodeModel { get { return nodeModel; } }
		public GRGEN_LIBGR.IEdgeModel EdgeModel { get { return edgeModel; } }
		public IEnumerable<GRGEN_LIBGR.ValidateInfo> ValidateInfo { get { return validateInfos; } }
		public IEnumerable<GRGEN_LIBGR.EnumAttributeType> EnumAttributeTypes { get { return enumAttributeTypes; } }
		public string MD5Hash { get { return "5efeccfb37eb4c2835fae110fe22d2e7"; } }
	}
}
