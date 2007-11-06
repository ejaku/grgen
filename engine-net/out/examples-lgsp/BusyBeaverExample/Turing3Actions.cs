using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.Turing3;

namespace de.unika.ipd.grGen.actions.Turing3
{
	public class Rule_moveLeftRule : LGSPRulePattern
	{
		private static Rule_moveLeftRule instance = null;
		public static Rule_moveLeftRule Instance { get { if (instance==null) instance = new Rule_moveLeftRule(); return instance; } }

		public static ITypeFramework[] node_bp_AllowedTypes = null;
		public static ITypeFramework[] node_s_AllowedTypes = null;
		public static ITypeFramework[] node_lbp_AllowedTypes = null;
		public static ITypeFramework[] node_wv_AllowedTypes = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_lbp_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static ITypeFramework[] edge__edge1_AllowedTypes = null;
		public static ITypeFramework[] edge__edge0_AllowedTypes = null;
		public static bool[] edge__edge1_IsAllowedType = null;
		public static bool[] edge__edge0_IsAllowedType = null;

		public enum NodeNums { @bp  = 1, @s, @lbp, @wv, };
		public enum EdgeNums { @_edge1 = 1, @_edge0, };

		private Rule_moveLeftRule()
		{
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_lbp = new PatternNode((int) NodeTypes.@BandPosition, "node_lbp", node_lbp_AllowedTypes, node_lbp_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternEdge edge__edge1 = new PatternEdge(node_lbp, node_bp, (int) EdgeTypes.@right, "edge__edge1", edge__edge1_AllowedTypes, edge__edge1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node_s, (int) EdgeTypes.@moveLeft, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_bp, node_s, node_lbp, node_wv }, 
				new PatternEdge[] { edge__edge1, edge__edge0 }, 
				new Condition[] { },
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, false, false, },
				new bool[] {
					false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new IType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new IType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_lbp = match.nodes[ (int) NodeNums.@lbp - 1 ];
			return new IGraphElement[] { node_s, node_lbp, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_lbp = match.nodes[ (int) NodeNums.@lbp - 1 ];
			return new IGraphElement[] { node_s, node_lbp, };
		}
	}

#if INITIAL_WARMUP
	public class Schedule_moveLeftRule : LGSPStaticScheduleInfo
	{
		public Schedule_moveLeftRule()
		{
			ActionName = "moveLeftRule";
			this.RulePattern = Rule_moveLeftRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_moveRightRule : LGSPRulePattern
	{
		private static Rule_moveRightRule instance = null;
		public static Rule_moveRightRule Instance { get { if (instance==null) instance = new Rule_moveRightRule(); return instance; } }

		public static ITypeFramework[] node_bp_AllowedTypes = null;
		public static ITypeFramework[] node_s_AllowedTypes = null;
		public static ITypeFramework[] node_rbp_AllowedTypes = null;
		public static ITypeFramework[] node_wv_AllowedTypes = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_rbp_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static ITypeFramework[] edge__edge1_AllowedTypes = null;
		public static ITypeFramework[] edge__edge0_AllowedTypes = null;
		public static bool[] edge__edge1_IsAllowedType = null;
		public static bool[] edge__edge0_IsAllowedType = null;

		public enum NodeNums { @bp  = 1, @s, @rbp, @wv, };
		public enum EdgeNums { @_edge1 = 1, @_edge0, };

		private Rule_moveRightRule()
		{
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_rbp = new PatternNode((int) NodeTypes.@BandPosition, "node_rbp", node_rbp_AllowedTypes, node_rbp_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternEdge edge__edge1 = new PatternEdge(node_bp, node_rbp, (int) EdgeTypes.@right, "edge__edge1", edge__edge1_AllowedTypes, edge__edge1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node_s, (int) EdgeTypes.@moveRight, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_bp, node_s, node_rbp, node_wv }, 
				new PatternEdge[] { edge__edge1, edge__edge0 }, 
				new Condition[] { },
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, false, false, },
				new bool[] {
					false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new IType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new IType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_rbp = match.nodes[ (int) NodeNums.@rbp - 1 ];
			return new IGraphElement[] { node_s, node_rbp, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_rbp = match.nodes[ (int) NodeNums.@rbp - 1 ];
			return new IGraphElement[] { node_s, node_rbp, };
		}
	}

#if INITIAL_WARMUP
	public class Schedule_moveRightRule : LGSPStaticScheduleInfo
	{
		public Schedule_moveRightRule()
		{
			ActionName = "moveRightRule";
			this.RulePattern = Rule_moveRightRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_readZeroRule : LGSPRulePattern
	{
		private static Rule_readZeroRule instance = null;
		public static Rule_readZeroRule Instance { get { if (instance==null) instance = new Rule_readZeroRule(); return instance; } }

		public static ITypeFramework[] node_s_AllowedTypes = null;
		public static ITypeFramework[] node_bp_AllowedTypes = null;
		public static ITypeFramework[] node_wv_AllowedTypes = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static ITypeFramework[] edge_rv_AllowedTypes = null;
		public static bool[] edge_rv_IsAllowedType = null;

		public enum NodeNums { @s  = 1, @bp, @wv, };
		public enum EdgeNums { @rv = 1, };

		private Rule_readZeroRule()
		{
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rv = new PatternEdge(node_s, node_wv, (int) EdgeTypes.@readZero, "edge_rv", edge_rv_AllowedTypes, edge_rv_IsAllowedType, PatternElementType.Normal, -1);
			Condition cond_0 = new Condition(0, new String[] { "node_bp" }, new String[] {  });
			patternGraph = new PatternGraph(
				new PatternNode[] { node_s, node_bp, node_wv }, 
				new PatternEdge[] { edge_rv }, 
				new Condition[] { cond_0, },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new IType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new IType[] { NodeType_WriteValue.typeVar, };
		}

		public static bool Condition_0(LGSPNode node_bp)
		{
			return (((INode_BandPosition) node_bp.attributes).@value == 0);
		}

		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.nodes[ (int) NodeNums.@wv - 1 ];
			INode_BandPosition node_bp_attributes = (INode_BandPosition) node_bp.attributes;
			INode_WriteValue node_wv_attributes = (INode_WriteValue) node_wv.attributes;
			int var_i = node_wv_attributes.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, node_bp_attributes.@value, var_i);
			node_bp_attributes.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.nodes[ (int) NodeNums.@wv - 1 ];
			INode_BandPosition node_bp_attributes = (INode_BandPosition) node_bp.attributes;
			INode_WriteValue node_wv_attributes = (INode_WriteValue) node_wv.attributes;
			int var_i = node_wv_attributes.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, node_bp_attributes.@value, var_i);
			node_bp_attributes.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}
	}

#if INITIAL_WARMUP
	public class Schedule_readZeroRule : LGSPStaticScheduleInfo
	{
		public Schedule_readZeroRule()
		{
			ActionName = "readZeroRule";
			this.RulePattern = Rule_readZeroRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_readOneRule : LGSPRulePattern
	{
		private static Rule_readOneRule instance = null;
		public static Rule_readOneRule Instance { get { if (instance==null) instance = new Rule_readOneRule(); return instance; } }

		public static ITypeFramework[] node_bp_AllowedTypes = null;
		public static ITypeFramework[] node_s_AllowedTypes = null;
		public static ITypeFramework[] node_wv_AllowedTypes = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static ITypeFramework[] edge_rv_AllowedTypes = null;
		public static bool[] edge_rv_IsAllowedType = null;

		public enum NodeNums { @bp  = 1, @s, @wv, };
		public enum EdgeNums { @rv = 1, };

		private Rule_readOneRule()
		{
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rv = new PatternEdge(node_s, node_wv, (int) EdgeTypes.@readOne, "edge_rv", edge_rv_AllowedTypes, edge_rv_IsAllowedType, PatternElementType.Normal, -1);
			Condition cond_0 = new Condition(0, new String[] { "node_bp" }, new String[] {  });
			patternGraph = new PatternGraph(
				new PatternNode[] { node_bp, node_s, node_wv }, 
				new PatternEdge[] { edge_rv }, 
				new Condition[] { cond_0, },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new IType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new IType[] { NodeType_WriteValue.typeVar, };
		}

		public static bool Condition_0(LGSPNode node_bp)
		{
			return (((INode_BandPosition) node_bp.attributes).@value == 1);
		}

		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.nodes[ (int) NodeNums.@wv - 1 ];
			INode_BandPosition node_bp_attributes = (INode_BandPosition) node_bp.attributes;
			INode_WriteValue node_wv_attributes = (INode_WriteValue) node_wv.attributes;
			int var_i = node_wv_attributes.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, node_bp_attributes.@value, var_i);
			node_bp_attributes.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.nodes[ (int) NodeNums.@wv - 1 ];
			INode_BandPosition node_bp_attributes = (INode_BandPosition) node_bp.attributes;
			INode_WriteValue node_wv_attributes = (INode_WriteValue) node_wv.attributes;
			int var_i = node_wv_attributes.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, node_bp_attributes.@value, var_i);
			node_bp_attributes.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}
	}

#if INITIAL_WARMUP
	public class Schedule_readOneRule : LGSPStaticScheduleInfo
	{
		public Schedule_readOneRule()
		{
			ActionName = "readOneRule";
			this.RulePattern = Rule_readOneRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_ensureMoveRightValidRule : LGSPRulePattern
	{
		private static Rule_ensureMoveRightValidRule instance = null;
		public static Rule_ensureMoveRightValidRule Instance { get { if (instance==null) instance = new Rule_ensureMoveRightValidRule(); return instance; } }

		public static ITypeFramework[] node_bp_AllowedTypes = null;
		public static ITypeFramework[] node_wv_AllowedTypes = null;
		public static ITypeFramework[] node__node0_AllowedTypes = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static bool[] node__node0_IsAllowedType = null;
		public static ITypeFramework[] edge__edge0_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static ITypeFramework[] neg_0_node__node0_AllowedTypes = null;
		public static bool[] neg_0_node__node0_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] neg_0_edge__edge0_IsAllowedType = null;

		public enum NodeNums { @bp  = 1, @wv, @_node0, };
		public enum EdgeNums { @_edge0 = 1, };

		private Rule_ensureMoveRightValidRule()
		{
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node__node0 = new PatternNode((int) NodeTypes.@State, "node__node0", node__node0_AllowedTypes, node__node0_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node__node0, (int) EdgeTypes.@moveRight, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_bp, node_wv, node__node0 }, 
				new PatternEdge[] { edge__edge0 }, 
				new Condition[] { },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, }
			);

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node__node0 = new PatternNode((int) NodeTypes.@BandPosition, "neg_0_node__node0", neg_0_node__node0_AllowedTypes, neg_0_node__node0_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge__edge0 = new PatternEdge(node_bp, neg_0_node__node0, (int) EdgeTypes.@right, "neg_0_edge__edge0", neg_0_edge__edge0_AllowedTypes, neg_0_edge__edge0_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_bp, neg_0_node__node0 }, 
				new PatternEdge[] { neg_0_edge__edge0 }, 
				new Condition[] { },
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new IType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node__node1 = graph.AddNode(NodeType_BandPosition.typeVar);
			LGSPEdge edge__edge1 = graph.AddEdge(EdgeType_right.typeVar, node_bp, node__node1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node1" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node__node1 = graph.AddNode(NodeType_BandPosition.typeVar);
			LGSPEdge edge__edge1 = graph.AddEdge(EdgeType_right.typeVar, node_bp, node__node1);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_ensureMoveRightValidRule : LGSPStaticScheduleInfo
	{
		public Schedule_ensureMoveRightValidRule()
		{
			ActionName = "ensureMoveRightValidRule";
			this.RulePattern = Rule_ensureMoveRightValidRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_ensureMoveLeftValidRule : LGSPRulePattern
	{
		private static Rule_ensureMoveLeftValidRule instance = null;
		public static Rule_ensureMoveLeftValidRule Instance { get { if (instance==null) instance = new Rule_ensureMoveLeftValidRule(); return instance; } }

		public static ITypeFramework[] node_bp_AllowedTypes = null;
		public static ITypeFramework[] node_wv_AllowedTypes = null;
		public static ITypeFramework[] node__node0_AllowedTypes = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static bool[] node__node0_IsAllowedType = null;
		public static ITypeFramework[] edge__edge0_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static ITypeFramework[] neg_0_node__node0_AllowedTypes = null;
		public static bool[] neg_0_node__node0_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] neg_0_edge__edge0_IsAllowedType = null;

		public enum NodeNums { @bp  = 1, @wv, @_node0, };
		public enum EdgeNums { @_edge0 = 1, };

		private Rule_ensureMoveLeftValidRule()
		{
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node__node0 = new PatternNode((int) NodeTypes.@State, "node__node0", node__node0_AllowedTypes, node__node0_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node__node0, (int) EdgeTypes.@moveLeft, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_bp, node_wv, node__node0 }, 
				new PatternEdge[] { edge__edge0 }, 
				new Condition[] { },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, }
			);

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node__node0 = new PatternNode((int) NodeTypes.@BandPosition, "neg_0_node__node0", neg_0_node__node0_AllowedTypes, neg_0_node__node0_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge__edge0 = new PatternEdge(neg_0_node__node0, node_bp, (int) EdgeTypes.@right, "neg_0_edge__edge0", neg_0_edge__edge0_AllowedTypes, neg_0_edge__edge0_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_bp, neg_0_node__node0 }, 
				new PatternEdge[] { neg_0_edge__edge0 }, 
				new Condition[] { },
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new IType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node__node1 = graph.AddNode(NodeType_BandPosition.typeVar);
			LGSPEdge edge__edge1 = graph.AddEdge(EdgeType_right.typeVar, node__node1, node_bp);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node1" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node__node1 = graph.AddNode(NodeType_BandPosition.typeVar);
			LGSPEdge edge__edge1 = graph.AddEdge(EdgeType_right.typeVar, node__node1, node_bp);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_ensureMoveLeftValidRule : LGSPStaticScheduleInfo
	{
		public Schedule_ensureMoveLeftValidRule()
		{
			ActionName = "ensureMoveLeftValidRule";
			this.RulePattern = Rule_ensureMoveLeftValidRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif


    public class Action_moveLeftRule : LGSPAction
    {
        private static Action_moveLeftRule instance = new Action_moveLeftRule();

        public Action_moveLeftRule() { rulePattern = Rule_moveLeftRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 4, 2); matchesList = matches.matches;}

        public override string Name { get { return "moveLeftRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Preset(node_bp:BandPosition)
            ITypeFramework[] node_typelist_node_bp = null;
            int node_typeiter_node_bp = 0;
            LGSPNode node_listhead_node_bp = null;
            bool node_parWasSet_node_bp;
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp != null)
            {
                node_parWasSet_node_bp = true;
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.typeID]) goto returnLabel;
            }
            else
            {
                node_parWasSet_node_bp = false;
                node_typelist_node_bp = NodeType_BandPosition.typeVar.subOrSameTypes;
                node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                node_cur_node_bp = node_listhead_node_bp.typeNext;
            }
            while(true)
            {
                while(!node_parWasSet_node_bp && node_cur_node_bp == node_listhead_node_bp)
                {
                    node_typeiter_node_bp++;
                    if(node_typeiter_node_bp >= node_typelist_node_bp.Length) goto returnLabel;
                    node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                    node_cur_node_bp = node_listhead_node_bp.typeNext;
                }
                node_cur_node_bp.mappedTo = 1;
                // Preset(node_wv:WriteValue)
                ITypeFramework[] node_typelist_node_wv = null;
                int node_typeiter_node_wv = 0;
                LGSPNode node_listhead_node_wv = null;
                bool node_parWasSet_node_wv;
                LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
                if(node_cur_node_wv != null)
                {
                    node_parWasSet_node_wv = true;
                    if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.typeID]) goto contunmap_node_cur_node_bp_2;
                }
                else
                {
                    node_parWasSet_node_wv = false;
                    node_typelist_node_wv = NodeType_WriteValue.typeVar.subOrSameTypes;
                    node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                    node_cur_node_wv = node_listhead_node_wv.typeNext;
                }
                while(true)
                {
                    while(!node_parWasSet_node_wv && node_cur_node_wv == node_listhead_node_wv)
                    {
                        node_typeiter_node_wv++;
                        if(node_typeiter_node_wv >= node_typelist_node_wv.Length) goto contunmap_node_cur_node_bp_2;
                        node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                        node_cur_node_wv = node_listhead_node_wv.typeNext;
                    }
                    // ExtendIncoming(node_bp -> edge__edge1:right)
                    LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.inhead;
                    if(edge_head_edge__edge1 != null)
                    {
                        LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.typeID]) continue;
                            // ImplicitSource(edge__edge1 -> node_lbp:BandPosition)
                            LGSPNode node_cur_node_lbp = edge_cur_edge__edge1.source;
                            if(!NodeType_BandPosition.isMyType[node_cur_node_lbp.type.typeID]) goto contunmap_edge_cur_edge__edge1_6;
                            if(node_cur_node_lbp.mappedTo != 0) goto cont_node_cur_node_lbp_9;
                            // ExtendOutgoing(node_wv -> edge__edge0:moveLeft)
                            LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                            if(edge_head_edge__edge0 != null)
                            {
                                LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                                do
                                {
                                    if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.typeID]) continue;
                                    // ImplicitTarget(edge__edge0 -> node_s:State)
                                    LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                                    if(!NodeType_State.isMyType[node_cur_node_s.type.typeID]) goto contunmap_edge_cur_edge__edge0_10;
                                    LGSPMatch match = matchesList.GetNewMatch();
                                    match.nodes[0] = node_cur_node_bp;
                                    match.nodes[1] = node_cur_node_s;
                                    match.nodes[2] = node_cur_node_lbp;
                                    match.nodes[3] = node_cur_node_wv;
                                    match.edges[0] = edge_cur_edge__edge1;
                                    match.edges[1] = edge_cur_edge__edge0;
                                    matchesList.CommitMatch();
                                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                    {
                                        node_cur_node_bp.mappedTo = 0;
                                        node_cur_node_bp.MoveInHeadAfter(edge_cur_edge__edge1);
                                        node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                        return matches;
                                    }
contunmap_edge_cur_edge__edge0_10:;
                                    // Tail ExtendOutgoing(edge_cur_edge__edge0)
                                }
                                while((edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0);
                            }
cont_node_cur_node_lbp_9:;
contunmap_edge_cur_edge__edge1_6:;
                            // Tail ExtendIncoming(edge_cur_edge__edge1)
                        }
                        while((edge_cur_edge__edge1 = edge_cur_edge__edge1.inNext) != edge_head_edge__edge1);
                    }
                    // Tail Preset(node_cur_node_wv)
                    if(node_parWasSet_node_wv) break;
                    node_cur_node_wv = node_cur_node_wv.typeNext;
                }
contunmap_node_cur_node_bp_2:;
                node_cur_node_bp.mappedTo = 0;
                // Tail Preset(node_cur_node_bp)
                if(node_parWasSet_node_bp) break;
                node_cur_node_bp = node_cur_node_bp.typeNext;
            }
returnLabel:;
            return matches;
        }
    }
    public class Action_moveRightRule : LGSPAction
    {
        private static Action_moveRightRule instance = new Action_moveRightRule();

        public Action_moveRightRule() { rulePattern = Rule_moveRightRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 4, 2); matchesList = matches.matches;}

        public override string Name { get { return "moveRightRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Preset(node_bp:BandPosition)
            ITypeFramework[] node_typelist_node_bp = null;
            int node_typeiter_node_bp = 0;
            LGSPNode node_listhead_node_bp = null;
            bool node_parWasSet_node_bp;
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp != null)
            {
                node_parWasSet_node_bp = true;
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.typeID]) goto returnLabel;
            }
            else
            {
                node_parWasSet_node_bp = false;
                node_typelist_node_bp = NodeType_BandPosition.typeVar.subOrSameTypes;
                node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                node_cur_node_bp = node_listhead_node_bp.typeNext;
            }
            while(true)
            {
                while(!node_parWasSet_node_bp && node_cur_node_bp == node_listhead_node_bp)
                {
                    node_typeiter_node_bp++;
                    if(node_typeiter_node_bp >= node_typelist_node_bp.Length) goto returnLabel;
                    node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                    node_cur_node_bp = node_listhead_node_bp.typeNext;
                }
                node_cur_node_bp.mappedTo = 1;
                // Preset(node_wv:WriteValue)
                ITypeFramework[] node_typelist_node_wv = null;
                int node_typeiter_node_wv = 0;
                LGSPNode node_listhead_node_wv = null;
                bool node_parWasSet_node_wv;
                LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
                if(node_cur_node_wv != null)
                {
                    node_parWasSet_node_wv = true;
                    if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.typeID]) goto contunmap_node_cur_node_bp_16;
                }
                else
                {
                    node_parWasSet_node_wv = false;
                    node_typelist_node_wv = NodeType_WriteValue.typeVar.subOrSameTypes;
                    node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                    node_cur_node_wv = node_listhead_node_wv.typeNext;
                }
                while(true)
                {
                    while(!node_parWasSet_node_wv && node_cur_node_wv == node_listhead_node_wv)
                    {
                        node_typeiter_node_wv++;
                        if(node_typeiter_node_wv >= node_typelist_node_wv.Length) goto contunmap_node_cur_node_bp_16;
                        node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                        node_cur_node_wv = node_listhead_node_wv.typeNext;
                    }
                    // ExtendOutgoing(node_bp -> edge__edge1:right)
                    LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.outhead;
                    if(edge_head_edge__edge1 != null)
                    {
                        LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.typeID]) continue;
                            // ImplicitTarget(edge__edge1 -> node_rbp:BandPosition)
                            LGSPNode node_cur_node_rbp = edge_cur_edge__edge1.target;
                            if(!NodeType_BandPosition.isMyType[node_cur_node_rbp.type.typeID]) goto contunmap_edge_cur_edge__edge1_20;
                            if(node_cur_node_rbp.mappedTo != 0) goto cont_node_cur_node_rbp_23;
                            // ExtendOutgoing(node_wv -> edge__edge0:moveRight)
                            LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                            if(edge_head_edge__edge0 != null)
                            {
                                LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                                do
                                {
                                    if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.typeID]) continue;
                                    // ImplicitTarget(edge__edge0 -> node_s:State)
                                    LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                                    if(!NodeType_State.isMyType[node_cur_node_s.type.typeID]) goto contunmap_edge_cur_edge__edge0_24;
                                    LGSPMatch match = matchesList.GetNewMatch();
                                    match.nodes[0] = node_cur_node_bp;
                                    match.nodes[1] = node_cur_node_s;
                                    match.nodes[2] = node_cur_node_rbp;
                                    match.nodes[3] = node_cur_node_wv;
                                    match.edges[0] = edge_cur_edge__edge1;
                                    match.edges[1] = edge_cur_edge__edge0;
                                    matchesList.CommitMatch();
                                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                    {
                                        node_cur_node_bp.mappedTo = 0;
                                        node_cur_node_bp.MoveOutHeadAfter(edge_cur_edge__edge1);
                                        node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                        return matches;
                                    }
contunmap_edge_cur_edge__edge0_24:;
                                    // Tail ExtendOutgoing(edge_cur_edge__edge0)
                                }
                                while((edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0);
                            }
cont_node_cur_node_rbp_23:;
contunmap_edge_cur_edge__edge1_20:;
                            // Tail ExtendOutgoing(edge_cur_edge__edge1)
                        }
                        while((edge_cur_edge__edge1 = edge_cur_edge__edge1.outNext) != edge_head_edge__edge1);
                    }
                    // Tail Preset(node_cur_node_wv)
                    if(node_parWasSet_node_wv) break;
                    node_cur_node_wv = node_cur_node_wv.typeNext;
                }
contunmap_node_cur_node_bp_16:;
                node_cur_node_bp.mappedTo = 0;
                // Tail Preset(node_cur_node_bp)
                if(node_parWasSet_node_bp) break;
                node_cur_node_bp = node_cur_node_bp.typeNext;
            }
returnLabel:;
            return matches;
        }
    }
    public class Action_readZeroRule : LGSPAction
    {
        private static Action_readZeroRule instance = new Action_readZeroRule();

        public Action_readZeroRule() { rulePattern = Rule_readZeroRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 1); matchesList = matches.matches;}

        public override string Name { get { return "readZeroRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Preset(node_s:State)
            ITypeFramework[] node_typelist_node_s = null;
            int node_typeiter_node_s = 0;
            LGSPNode node_listhead_node_s = null;
            bool node_parWasSet_node_s;
            LGSPNode node_cur_node_s = (LGSPNode) parameters[0];
            if(node_cur_node_s != null)
            {
                node_parWasSet_node_s = true;
                if(!NodeType_State.isMyType[node_cur_node_s.type.typeID]) goto returnLabel;
            }
            else
            {
                node_parWasSet_node_s = false;
                node_typelist_node_s = NodeType_State.typeVar.subOrSameTypes;
                node_listhead_node_s = graph.nodesByTypeHeads[node_typelist_node_s[node_typeiter_node_s].typeID];
                node_cur_node_s = node_listhead_node_s.typeNext;
            }
            while(true)
            {
                while(!node_parWasSet_node_s && node_cur_node_s == node_listhead_node_s)
                {
                    node_typeiter_node_s++;
                    if(node_typeiter_node_s >= node_typelist_node_s.Length) goto returnLabel;
                    node_listhead_node_s = graph.nodesByTypeHeads[node_typelist_node_s[node_typeiter_node_s].typeID];
                    node_cur_node_s = node_listhead_node_s.typeNext;
                }
                // Preset(node_bp:BandPosition)
                ITypeFramework[] node_typelist_node_bp = null;
                int node_typeiter_node_bp = 0;
                LGSPNode node_listhead_node_bp = null;
                bool node_parWasSet_node_bp;
                LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
                if(node_cur_node_bp != null)
                {
                    node_parWasSet_node_bp = true;
                    if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.typeID]) goto contunmap_node_cur_node_s_30;
                }
                else
                {
                    node_parWasSet_node_bp = false;
                    node_typelist_node_bp = NodeType_BandPosition.typeVar.subOrSameTypes;
                    node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                    node_cur_node_bp = node_listhead_node_bp.typeNext;
                }
                while(true)
                {
                    while(!node_parWasSet_node_bp && node_cur_node_bp == node_listhead_node_bp)
                    {
                        node_typeiter_node_bp++;
                        if(node_typeiter_node_bp >= node_typelist_node_bp.Length) goto contunmap_node_cur_node_s_30;
                        node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                        node_cur_node_bp = node_listhead_node_bp.typeNext;
                    }
                    // Condition[0]
                    if(!Rule_readZeroRule.Condition_0(node_cur_node_bp)) goto contunmap_node_cur_node_bp_32;
                    // ExtendOutgoing(node_s -> edge_rv:readZero)
                    LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
                    if(edge_head_edge_rv != null)
                    {
                        LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                        do
                        {
                            if(!EdgeType_readZero.isMyType[edge_cur_edge_rv.type.typeID]) continue;
                            // ImplicitTarget(edge_rv -> node_wv:WriteValue)
                            LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                            if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.typeID]) goto contunmap_edge_cur_edge_rv_34;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_s;
                            match.nodes[1] = node_cur_node_bp;
                            match.nodes[2] = node_cur_node_wv;
                            match.edges[0] = edge_cur_edge_rv;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                                return matches;
                            }
contunmap_edge_cur_edge_rv_34:;
                            // Tail ExtendOutgoing(edge_cur_edge_rv)
                        }
                        while((edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv);
                    }
contunmap_node_cur_node_bp_32:;
                    // Tail Preset(node_cur_node_bp)
                    if(node_parWasSet_node_bp) break;
                    node_cur_node_bp = node_cur_node_bp.typeNext;
                }
contunmap_node_cur_node_s_30:;
                // Tail Preset(node_cur_node_s)
                if(node_parWasSet_node_s) break;
                node_cur_node_s = node_cur_node_s.typeNext;
            }
returnLabel:;
            return matches;
        }
    }
    public class Action_readOneRule : LGSPAction
    {
        private static Action_readOneRule instance = new Action_readOneRule();

        public Action_readOneRule() { rulePattern = Rule_readOneRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 1); matchesList = matches.matches;}

        public override string Name { get { return "readOneRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Preset(node_bp:BandPosition)
            ITypeFramework[] node_typelist_node_bp = null;
            int node_typeiter_node_bp = 0;
            LGSPNode node_listhead_node_bp = null;
            bool node_parWasSet_node_bp;
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp != null)
            {
                node_parWasSet_node_bp = true;
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.typeID]) goto returnLabel;
            }
            else
            {
                node_parWasSet_node_bp = false;
                node_typelist_node_bp = NodeType_BandPosition.typeVar.subOrSameTypes;
                node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                node_cur_node_bp = node_listhead_node_bp.typeNext;
            }
            while(true)
            {
                while(!node_parWasSet_node_bp && node_cur_node_bp == node_listhead_node_bp)
                {
                    node_typeiter_node_bp++;
                    if(node_typeiter_node_bp >= node_typelist_node_bp.Length) goto returnLabel;
                    node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                    node_cur_node_bp = node_listhead_node_bp.typeNext;
                }
                // Condition[0]
                if(!Rule_readOneRule.Condition_0(node_cur_node_bp)) goto contunmap_node_cur_node_bp_40;
                // Preset(node_s:State)
                ITypeFramework[] node_typelist_node_s = null;
                int node_typeiter_node_s = 0;
                LGSPNode node_listhead_node_s = null;
                bool node_parWasSet_node_s;
                LGSPNode node_cur_node_s = (LGSPNode) parameters[0];
                if(node_cur_node_s != null)
                {
                    node_parWasSet_node_s = true;
                    if(!NodeType_State.isMyType[node_cur_node_s.type.typeID]) goto contunmap_node_cur_node_bp_40;
                }
                else
                {
                    node_parWasSet_node_s = false;
                    node_typelist_node_s = NodeType_State.typeVar.subOrSameTypes;
                    node_listhead_node_s = graph.nodesByTypeHeads[node_typelist_node_s[node_typeiter_node_s].typeID];
                    node_cur_node_s = node_listhead_node_s.typeNext;
                }
                while(true)
                {
                    while(!node_parWasSet_node_s && node_cur_node_s == node_listhead_node_s)
                    {
                        node_typeiter_node_s++;
                        if(node_typeiter_node_s >= node_typelist_node_s.Length) goto contunmap_node_cur_node_bp_40;
                        node_listhead_node_s = graph.nodesByTypeHeads[node_typelist_node_s[node_typeiter_node_s].typeID];
                        node_cur_node_s = node_listhead_node_s.typeNext;
                    }
                    // ExtendOutgoing(node_s -> edge_rv:readOne)
                    LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
                    if(edge_head_edge_rv != null)
                    {
                        LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                        do
                        {
                            if(!EdgeType_readOne.isMyType[edge_cur_edge_rv.type.typeID]) continue;
                            // ImplicitTarget(edge_rv -> node_wv:WriteValue)
                            LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                            if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.typeID]) goto contunmap_edge_cur_edge_rv_44;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_bp;
                            match.nodes[1] = node_cur_node_s;
                            match.nodes[2] = node_cur_node_wv;
                            match.edges[0] = edge_cur_edge_rv;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                                return matches;
                            }
contunmap_edge_cur_edge_rv_44:;
                            // Tail ExtendOutgoing(edge_cur_edge_rv)
                        }
                        while((edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv);
                    }
                    // Tail Preset(node_cur_node_s)
                    if(node_parWasSet_node_s) break;
                    node_cur_node_s = node_cur_node_s.typeNext;
                }
contunmap_node_cur_node_bp_40:;
                // Tail Preset(node_cur_node_bp)
                if(node_parWasSet_node_bp) break;
                node_cur_node_bp = node_cur_node_bp.typeNext;
            }
returnLabel:;
            return matches;
        }
    }
    public class Action_ensureMoveRightValidRule : LGSPAction
    {
        private static Action_ensureMoveRightValidRule instance = new Action_ensureMoveRightValidRule();

        public Action_ensureMoveRightValidRule() { rulePattern = Rule_ensureMoveRightValidRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 1); matchesList = matches.matches;}

        public override string Name { get { return "ensureMoveRightValidRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Preset(node_bp:BandPosition)
            ITypeFramework[] node_typelist_node_bp = null;
            int node_typeiter_node_bp = 0;
            LGSPNode node_listhead_node_bp = null;
            bool node_parWasSet_node_bp;
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp != null)
            {
                node_parWasSet_node_bp = true;
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.typeID]) goto returnLabel;
            }
            else
            {
                node_parWasSet_node_bp = false;
                node_typelist_node_bp = NodeType_BandPosition.typeVar.subOrSameTypes;
                node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                node_cur_node_bp = node_listhead_node_bp.typeNext;
            }
            while(true)
            {
                while(!node_parWasSet_node_bp && node_cur_node_bp == node_listhead_node_bp)
                {
                    node_typeiter_node_bp++;
                    if(node_typeiter_node_bp >= node_typelist_node_bp.Length) goto returnLabel;
                    node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                    node_cur_node_bp = node_listhead_node_bp.typeNext;
                }
                // NegativePattern
                node_cur_node_bp.negMappedTo = 1;
                // ExtendOutgoing(node_bp -> neg_0_edge__edge0:right)
                LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.outhead;
                if(edge_head_neg_0_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                    do
                    {
                        if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.typeID]) continue;
                        // ImplicitTarget(neg_0_edge__edge0 -> neg_0_node__node0:BandPosition)
                        LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.target;
                        if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.typeID]) goto contunmap_edge_cur_neg_0_edge__edge0_56;
                        if(node_cur_neg_0_node__node0.negMappedTo != 0) goto cont_node_cur_neg_0_node__node0_59;
                        node_cur_node_bp.negMappedTo = 0;
                        goto contunmap_node_cur_node_bp_50;
cont_node_cur_neg_0_node__node0_59:;
contunmap_edge_cur_neg_0_edge__edge0_56:;
                        // Tail ExtendOutgoing(edge_cur_neg_0_edge__edge0)
                    }
                    while((edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.outNext) != edge_head_neg_0_edge__edge0);
                }
                node_cur_node_bp.negMappedTo = 0;
                // End of NegativePattern
                // Preset(node_wv:WriteValue)
                ITypeFramework[] node_typelist_node_wv = null;
                int node_typeiter_node_wv = 0;
                LGSPNode node_listhead_node_wv = null;
                bool node_parWasSet_node_wv;
                LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
                if(node_cur_node_wv != null)
                {
                    node_parWasSet_node_wv = true;
                    if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.typeID]) goto contunmap_node_cur_node_bp_50;
                }
                else
                {
                    node_parWasSet_node_wv = false;
                    node_typelist_node_wv = NodeType_WriteValue.typeVar.subOrSameTypes;
                    node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                    node_cur_node_wv = node_listhead_node_wv.typeNext;
                }
                while(true)
                {
                    while(!node_parWasSet_node_wv && node_cur_node_wv == node_listhead_node_wv)
                    {
                        node_typeiter_node_wv++;
                        if(node_typeiter_node_wv >= node_typelist_node_wv.Length) goto contunmap_node_cur_node_bp_50;
                        node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                        node_cur_node_wv = node_listhead_node_wv.typeNext;
                    }
                    // ExtendOutgoing(node_wv -> edge__edge0:moveRight)
                    LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                    if(edge_head_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                        do
                        {
                            if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.typeID]) continue;
                            // ImplicitTarget(edge__edge0 -> node__node0:State)
                            LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                            if(!NodeType_State.isMyType[node_cur_node__node0.type.typeID]) goto contunmap_edge_cur_edge__edge0_62;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_bp;
                            match.nodes[1] = node_cur_node_wv;
                            match.nodes[2] = node_cur_node__node0;
                            match.edges[0] = edge_cur_edge__edge0;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                return matches;
                            }
contunmap_edge_cur_edge__edge0_62:;
                            // Tail ExtendOutgoing(edge_cur_edge__edge0)
                        }
                        while((edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0);
                    }
                    // Tail Preset(node_cur_node_wv)
                    if(node_parWasSet_node_wv) break;
                    node_cur_node_wv = node_cur_node_wv.typeNext;
                }
contunmap_node_cur_node_bp_50:;
                // Tail Preset(node_cur_node_bp)
                if(node_parWasSet_node_bp) break;
                node_cur_node_bp = node_cur_node_bp.typeNext;
            }
returnLabel:;
            return matches;
        }
    }
    public class Action_ensureMoveLeftValidRule : LGSPAction
    {
        private static Action_ensureMoveLeftValidRule instance = new Action_ensureMoveLeftValidRule();

        public Action_ensureMoveLeftValidRule() { rulePattern = Rule_ensureMoveLeftValidRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 1); matchesList = matches.matches;}

        public override string Name { get { return "ensureMoveLeftValidRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Preset(node_bp:BandPosition)
            ITypeFramework[] node_typelist_node_bp = null;
            int node_typeiter_node_bp = 0;
            LGSPNode node_listhead_node_bp = null;
            bool node_parWasSet_node_bp;
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp != null)
            {
                node_parWasSet_node_bp = true;
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.typeID]) goto returnLabel;
            }
            else
            {
                node_parWasSet_node_bp = false;
                node_typelist_node_bp = NodeType_BandPosition.typeVar.subOrSameTypes;
                node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                node_cur_node_bp = node_listhead_node_bp.typeNext;
            }
            while(true)
            {
                while(!node_parWasSet_node_bp && node_cur_node_bp == node_listhead_node_bp)
                {
                    node_typeiter_node_bp++;
                    if(node_typeiter_node_bp >= node_typelist_node_bp.Length) goto returnLabel;
                    node_listhead_node_bp = graph.nodesByTypeHeads[node_typelist_node_bp[node_typeiter_node_bp].typeID];
                    node_cur_node_bp = node_listhead_node_bp.typeNext;
                }
                // NegativePattern
                node_cur_node_bp.negMappedTo = 1;
                // ExtendIncoming(node_bp -> neg_0_edge__edge0:right)
                LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.inhead;
                if(edge_head_neg_0_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                    do
                    {
                        if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.typeID]) continue;
                        // ImplicitSource(neg_0_edge__edge0 -> neg_0_node__node0:BandPosition)
                        LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.source;
                        if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.typeID]) goto contunmap_edge_cur_neg_0_edge__edge0_74;
                        if(node_cur_neg_0_node__node0.negMappedTo != 0) goto cont_node_cur_neg_0_node__node0_77;
                        node_cur_node_bp.negMappedTo = 0;
                        goto contunmap_node_cur_node_bp_68;
cont_node_cur_neg_0_node__node0_77:;
contunmap_edge_cur_neg_0_edge__edge0_74:;
                        // Tail ExtendIncoming(edge_cur_neg_0_edge__edge0)
                    }
                    while((edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.inNext) != edge_head_neg_0_edge__edge0);
                }
                node_cur_node_bp.negMappedTo = 0;
                // End of NegativePattern
                // Preset(node_wv:WriteValue)
                ITypeFramework[] node_typelist_node_wv = null;
                int node_typeiter_node_wv = 0;
                LGSPNode node_listhead_node_wv = null;
                bool node_parWasSet_node_wv;
                LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
                if(node_cur_node_wv != null)
                {
                    node_parWasSet_node_wv = true;
                    if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.typeID]) goto contunmap_node_cur_node_bp_68;
                }
                else
                {
                    node_parWasSet_node_wv = false;
                    node_typelist_node_wv = NodeType_WriteValue.typeVar.subOrSameTypes;
                    node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                    node_cur_node_wv = node_listhead_node_wv.typeNext;
                }
                while(true)
                {
                    while(!node_parWasSet_node_wv && node_cur_node_wv == node_listhead_node_wv)
                    {
                        node_typeiter_node_wv++;
                        if(node_typeiter_node_wv >= node_typelist_node_wv.Length) goto contunmap_node_cur_node_bp_68;
                        node_listhead_node_wv = graph.nodesByTypeHeads[node_typelist_node_wv[node_typeiter_node_wv].typeID];
                        node_cur_node_wv = node_listhead_node_wv.typeNext;
                    }
                    // ExtendOutgoing(node_wv -> edge__edge0:moveLeft)
                    LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                    if(edge_head_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                        do
                        {
                            if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.typeID]) continue;
                            // ImplicitTarget(edge__edge0 -> node__node0:State)
                            LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                            if(!NodeType_State.isMyType[node_cur_node__node0.type.typeID]) goto contunmap_edge_cur_edge__edge0_80;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_bp;
                            match.nodes[1] = node_cur_node_wv;
                            match.nodes[2] = node_cur_node__node0;
                            match.edges[0] = edge_cur_edge__edge0;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                return matches;
                            }
contunmap_edge_cur_edge__edge0_80:;
                            // Tail ExtendOutgoing(edge_cur_edge__edge0)
                        }
                        while((edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0);
                    }
                    // Tail Preset(node_cur_node_wv)
                    if(node_parWasSet_node_wv) break;
                    node_cur_node_wv = node_cur_node_wv.typeNext;
                }
contunmap_node_cur_node_bp_68:;
                // Tail Preset(node_cur_node_bp)
                if(node_parWasSet_node_bp) break;
                node_cur_node_bp = node_cur_node_bp.typeNext;
            }
returnLabel:;
            return matches;
        }
    }

    public class Turing3Actions : LGSPActions
    {
        public Turing3Actions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, dumperfactory, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public Turing3Actions(LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("moveLeftRule", (LGSPAction) Action_moveLeftRule.Instance);
            actions.Add("moveRightRule", (LGSPAction) Action_moveRightRule.Instance);
            actions.Add("readZeroRule", (LGSPAction) Action_readZeroRule.Instance);
            actions.Add("readOneRule", (LGSPAction) Action_readOneRule.Instance);
            actions.Add("ensureMoveRightValidRule", (LGSPAction) Action_ensureMoveRightValidRule.Instance);
            actions.Add("ensureMoveLeftValidRule", (LGSPAction) Action_ensureMoveLeftValidRule.Instance);
        }

        public override String Name { get { return "Turing3Actions"; } }
        public override String ModelMD5Hash { get { return "5a78f363d1b6a0cc5cea759830c3e6b1"; } }
    }
}