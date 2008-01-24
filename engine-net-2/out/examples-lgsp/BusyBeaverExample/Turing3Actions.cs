using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.Turing3;

namespace de.unika.ipd.grGen.actions.Turing3
{
	public class Rule_ensureMoveLeftValidRule : LGSPRulePattern
	{
		private static Rule_ensureMoveLeftValidRule instance = null;
		public static Rule_ensureMoveLeftValidRule Instance { get { if (instance==null) instance = new Rule_ensureMoveLeftValidRule(); return instance; } }

		public static NodeType[] node_wv_AllowedTypes = null;
		public static NodeType[] node__node0_AllowedTypes = null;
		public static NodeType[] node_bp_AllowedTypes = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static bool[] node__node0_IsAllowedType = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static NodeType[] neg_0_node__node0_AllowedTypes = null;
		public static bool[] neg_0_node__node0_IsAllowedType = null;
		public static EdgeType[] neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] neg_0_edge__edge0_IsAllowedType = null;

		public enum NodeNums { @wv  = 1, @_node0, @bp, };
		public enum EdgeNums { @_edge0 = 1, };
		public enum PatternNums { };

		private Rule_ensureMoveLeftValidRule()
		{
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node__node0 = new PatternNode((int) NodeTypes.@State, "node__node0", node__node0_AllowedTypes, node__node0_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node__node0, (int) EdgeTypes.@moveLeft, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				"rule ensureMoveLeftValidRule.pattern",
				new PatternNode[] { node_wv, node__node0, node_bp }, 
				new PatternEdge[] { edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, }
			);

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node__node0 = new PatternNode((int) NodeTypes.@BandPosition, "neg_0_node__node0", neg_0_node__node0_AllowedTypes, neg_0_node__node0_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge__edge0 = new PatternEdge(neg_0_node__node0, node_bp, (int) EdgeTypes.@right, "neg_0_edge__edge0", neg_0_edge__edge0_AllowedTypes, neg_0_edge__edge0_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				"rule ensureMoveLeftValidRule.pattern.negative.pattern",
				new PatternNode[] { neg_0_node__node0, node_bp }, 
				new PatternEdge[] { neg_0_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			Node_BandPosition node__node1 = Node_BandPosition.CreateNode(graph);
			Edge_right edge__edge1 = Edge_right.CreateEdge(graph, node__node1, node_bp);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			Node_BandPosition node__node1 = Node_BandPosition.CreateNode(graph);
			Edge_right edge__edge1 = Edge_right.CreateEdge(graph, node__node1, node_bp);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node1" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_ensureMoveLeftValidRule : LGSPStaticScheduleInfo
	{
		public Schedule_ensureMoveLeftValidRule()
		{
			ActionName = "ensureMoveLeftValidRule";
			this.RulePattern = Rule_ensureMoveLeftValidRule.Instance;
			isRule = true;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_ensureMoveRightValidRule : LGSPRulePattern
	{
		private static Rule_ensureMoveRightValidRule instance = null;
		public static Rule_ensureMoveRightValidRule Instance { get { if (instance==null) instance = new Rule_ensureMoveRightValidRule(); return instance; } }

		public static NodeType[] node_wv_AllowedTypes = null;
		public static NodeType[] node__node0_AllowedTypes = null;
		public static NodeType[] node_bp_AllowedTypes = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static bool[] node__node0_IsAllowedType = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static NodeType[] neg_0_node__node0_AllowedTypes = null;
		public static bool[] neg_0_node__node0_IsAllowedType = null;
		public static EdgeType[] neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] neg_0_edge__edge0_IsAllowedType = null;

		public enum NodeNums { @wv  = 1, @_node0, @bp, };
		public enum EdgeNums { @_edge0 = 1, };
		public enum PatternNums { };

		private Rule_ensureMoveRightValidRule()
		{
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node__node0 = new PatternNode((int) NodeTypes.@State, "node__node0", node__node0_AllowedTypes, node__node0_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node__node0, (int) EdgeTypes.@moveRight, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				"rule ensureMoveRightValidRule.pattern",
				new PatternNode[] { node_wv, node__node0, node_bp }, 
				new PatternEdge[] { edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, }
			);

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node__node0 = new PatternNode((int) NodeTypes.@BandPosition, "neg_0_node__node0", neg_0_node__node0_AllowedTypes, neg_0_node__node0_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge__edge0 = new PatternEdge(node_bp, neg_0_node__node0, (int) EdgeTypes.@right, "neg_0_edge__edge0", neg_0_edge__edge0_AllowedTypes, neg_0_edge__edge0_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				"rule ensureMoveRightValidRule.pattern.negative.pattern",
				new PatternNode[] { node_bp, neg_0_node__node0 }, 
				new PatternEdge[] { neg_0_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			Node_BandPosition node__node1 = Node_BandPosition.CreateNode(graph);
			Edge_right edge__edge1 = Edge_right.CreateEdge(graph, node_bp, node__node1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			Node_BandPosition node__node1 = Node_BandPosition.CreateNode(graph);
			Edge_right edge__edge1 = Edge_right.CreateEdge(graph, node_bp, node__node1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node1" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_ensureMoveRightValidRule : LGSPStaticScheduleInfo
	{
		public Schedule_ensureMoveRightValidRule()
		{
			ActionName = "ensureMoveRightValidRule";
			this.RulePattern = Rule_ensureMoveRightValidRule.Instance;
			isRule = true;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_moveLeftRule : LGSPRulePattern
	{
		private static Rule_moveLeftRule instance = null;
		public static Rule_moveLeftRule Instance { get { if (instance==null) instance = new Rule_moveLeftRule(); return instance; } }

		public static NodeType[] node_wv_AllowedTypes = null;
		public static NodeType[] node_s_AllowedTypes = null;
		public static NodeType[] node_lbp_AllowedTypes = null;
		public static NodeType[] node_bp_AllowedTypes = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_lbp_IsAllowedType = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static EdgeType[] edge__edge1_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static bool[] edge__edge1_IsAllowedType = null;

		public enum NodeNums { @wv  = 1, @s, @lbp, @bp, };
		public enum EdgeNums { @_edge0 = 1, @_edge1, };
		public enum PatternNums { };

		private Rule_moveLeftRule()
		{
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_lbp = new PatternNode((int) NodeTypes.@BandPosition, "node_lbp", node_lbp_AllowedTypes, node_lbp_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node_s, (int) EdgeTypes.@moveLeft, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge1 = new PatternEdge(node_lbp, node_bp, (int) EdgeTypes.@right, "edge__edge1", edge__edge1_AllowedTypes, edge__edge1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				"rule moveLeftRule.pattern",
				new PatternNode[] { node_wv, node_s, node_lbp, node_bp }, 
				new PatternEdge[] { edge__edge0, edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, false, },
				new bool[] {
					true, true, true, true, },
				new bool[] {
					true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new GrGenType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.Nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_lbp = match.Nodes[ (int) NodeNums.@lbp - 1 ];
			return new IGraphElement[] { node_s, node_lbp, };
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.Nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_lbp = match.Nodes[ (int) NodeNums.@lbp - 1 ];
			return new IGraphElement[] { node_s, node_lbp, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_moveLeftRule : LGSPStaticScheduleInfo
	{
		public Schedule_moveLeftRule()
		{
			ActionName = "moveLeftRule";
			this.RulePattern = Rule_moveLeftRule.Instance;
			isRule = true;
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

		public static NodeType[] node_wv_AllowedTypes = null;
		public static NodeType[] node_s_AllowedTypes = null;
		public static NodeType[] node_bp_AllowedTypes = null;
		public static NodeType[] node_rbp_AllowedTypes = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_rbp_IsAllowedType = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static EdgeType[] edge__edge1_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static bool[] edge__edge1_IsAllowedType = null;

		public enum NodeNums { @wv  = 1, @s, @bp, @rbp, };
		public enum EdgeNums { @_edge0 = 1, @_edge1, };
		public enum PatternNums { };

		private Rule_moveRightRule()
		{
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_rbp = new PatternNode((int) NodeTypes.@BandPosition, "node_rbp", node_rbp_AllowedTypes, node_rbp_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_wv, node_s, (int) EdgeTypes.@moveRight, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge1 = new PatternEdge(node_bp, node_rbp, (int) EdgeTypes.@right, "edge__edge1", edge__edge1_AllowedTypes, edge__edge1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				"rule moveRightRule.pattern",
				new PatternNode[] { node_wv, node_s, node_bp, node_rbp }, 
				new PatternEdge[] { edge__edge0, edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, false, },
				new bool[] {
					true, true, true, true, },
				new bool[] {
					true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { NodeType_WriteValue.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new GrGenType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.Nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_rbp = match.Nodes[ (int) NodeNums.@rbp - 1 ];
			return new IGraphElement[] { node_s, node_rbp, };
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_s = match.Nodes[ (int) NodeNums.@s - 1 ];
			LGSPNode node_rbp = match.Nodes[ (int) NodeNums.@rbp - 1 ];
			return new IGraphElement[] { node_s, node_rbp, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_moveRightRule : LGSPStaticScheduleInfo
	{
		public Schedule_moveRightRule()
		{
			ActionName = "moveRightRule";
			this.RulePattern = Rule_moveRightRule.Instance;
			isRule = true;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_readOneRule : LGSPRulePattern
	{
		private static Rule_readOneRule instance = null;
		public static Rule_readOneRule Instance { get { if (instance==null) instance = new Rule_readOneRule(); return instance; } }

		public static NodeType[] node_s_AllowedTypes = null;
		public static NodeType[] node_wv_AllowedTypes = null;
		public static NodeType[] node_bp_AllowedTypes = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static EdgeType[] edge_rv_AllowedTypes = null;
		public static bool[] edge_rv_IsAllowedType = null;

		public enum NodeNums { @s  = 1, @wv, @bp, };
		public enum EdgeNums { @rv = 1, };
		public enum PatternNums { };

		private Rule_readOneRule()
		{
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternEdge edge_rv = new PatternEdge(node_s, node_wv, (int) EdgeTypes.@readOne, "edge_rv", edge_rv_AllowedTypes, edge_rv_IsAllowedType, PatternElementType.Normal, -1);
			Condition cond_0 = new Condition(0, new String[] { "node_bp" }, new String[] {  });
			patternGraph = new PatternGraph(
				"rule readOneRule.pattern",
				new PatternNode[] { node_s, node_wv, node_bp }, 
				new PatternEdge[] { edge_rv }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new GrGenType[] { NodeType_WriteValue.typeVar, };
		}

		public static bool Condition_0(LGSPNode node_bp)
		{
			return (((INode_BandPosition) node_bp).@value == 1);
		}

		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.Nodes[ (int) NodeNums.@wv - 1 ];
			INode_WriteValue inode_wv = (INode_WriteValue) node_wv;
			INode_BandPosition inode_bp = (INode_BandPosition) node_bp;
			int var_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, inode_bp.@value, var_i);
			inode_bp.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.Nodes[ (int) NodeNums.@wv - 1 ];
			INode_WriteValue inode_wv = (INode_WriteValue) node_wv;
			INode_BandPosition inode_bp = (INode_BandPosition) node_bp;
			int var_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, inode_bp.@value, var_i);
			inode_bp.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_readOneRule : LGSPStaticScheduleInfo
	{
		public Schedule_readOneRule()
		{
			ActionName = "readOneRule";
			this.RulePattern = Rule_readOneRule.Instance;
			isRule = true;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_readZeroRule : LGSPRulePattern
	{
		private static Rule_readZeroRule instance = null;
		public static Rule_readZeroRule Instance { get { if (instance==null) instance = new Rule_readZeroRule(); return instance; } }

		public static NodeType[] node_bp_AllowedTypes = null;
		public static NodeType[] node_s_AllowedTypes = null;
		public static NodeType[] node_wv_AllowedTypes = null;
		public static bool[] node_bp_IsAllowedType = null;
		public static bool[] node_s_IsAllowedType = null;
		public static bool[] node_wv_IsAllowedType = null;
		public static EdgeType[] edge_rv_AllowedTypes = null;
		public static bool[] edge_rv_IsAllowedType = null;

		public enum NodeNums { @bp  = 1, @s, @wv, };
		public enum EdgeNums { @rv = 1, };
		public enum PatternNums { };

		private Rule_readZeroRule()
		{
			PatternNode node_bp = new PatternNode((int) NodeTypes.@BandPosition, "node_bp", node_bp_AllowedTypes, node_bp_IsAllowedType, PatternElementType.Preset, 1);
			PatternNode node_s = new PatternNode((int) NodeTypes.@State, "node_s", node_s_AllowedTypes, node_s_IsAllowedType, PatternElementType.Preset, 0);
			PatternNode node_wv = new PatternNode((int) NodeTypes.@WriteValue, "node_wv", node_wv_AllowedTypes, node_wv_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rv = new PatternEdge(node_s, node_wv, (int) EdgeTypes.@readZero, "edge_rv", edge_rv_AllowedTypes, edge_rv_IsAllowedType, PatternElementType.Normal, -1);
			Condition cond_0 = new Condition(0, new String[] { "node_bp" }, new String[] {  });
			patternGraph = new PatternGraph(
				"rule readZeroRule.pattern",
				new PatternNode[] { node_bp, node_s, node_wv }, 
				new PatternEdge[] { edge_rv }, 
				new PatternGraphEmbedding[] {  }, 
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
					false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { NodeType_State.typeVar, NodeType_BandPosition.typeVar, };
			outputs = new GrGenType[] { NodeType_WriteValue.typeVar, };
		}

		public static bool Condition_0(LGSPNode node_bp)
		{
			return (((INode_BandPosition) node_bp).@value == 0);
		}

		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.Nodes[ (int) NodeNums.@wv - 1 ];
			INode_WriteValue inode_wv = (INode_WriteValue) node_wv;
			INode_BandPosition inode_bp = (INode_BandPosition) node_bp;
			int var_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, inode_bp.@value, var_i);
			inode_bp.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_bp = match.Nodes[ (int) NodeNums.@bp - 1 ];
			LGSPNode node_wv = match.Nodes[ (int) NodeNums.@wv - 1 ];
			INode_WriteValue inode_wv = (INode_WriteValue) node_wv;
			INode_BandPosition inode_bp = (INode_BandPosition) node_bp;
			int var_i = inode_wv.@value;
			graph.ChangingNodeAttribute(node_bp, NodeType_BandPosition.AttributeType_value, inode_bp.@value, var_i);
			inode_bp.@value = var_i;
			return new IGraphElement[] { node_wv, };
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_readZeroRule : LGSPStaticScheduleInfo
	{
		public Schedule_readZeroRule()
		{
			ActionName = "readZeroRule";
			this.RulePattern = Rule_readZeroRule.Instance;
			isRule = true;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif


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
            // Preset node_wv 
            LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
            if(node_cur_node_wv == null) {
                MissingPreset_node_wv(graph, maxMatches, parameters);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                return matches;
            }
            // Preset node_bp 
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp == null) {
                MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                return matches;
            }
            // NegativePattern 
            {
                bool node_cur_node_bp_prevIsMatchedNeg = node_cur_node_bp.isMatchedNeg;
                node_cur_node_bp.isMatchedNeg = true;
                // Extend incoming neg_0_edge__edge0 from node_bp 
                LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.inhead;
                if(edge_head_neg_0_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                    do
                    {
                        if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit source neg_0_node__node0 from neg_0_edge__edge0 
                        LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.source;
                        if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_neg_0_node__node0.isMatchedNeg
                            && node_cur_neg_0_node__node0==node_cur_node_bp
                            )
                        {
                            continue;
                        }
                        node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                        return matches;
                    }
                    while( (edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.inNext) != edge_head_neg_0_edge__edge0 );
                }
                node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
            }
            // Extend outgoing edge__edge0 from node_wv 
            LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
            if(edge_head_edge__edge0 != null)
            {
                LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                do
                {
                    if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                        continue;
                    }
                    // Implicit target node__node0 from edge__edge0 
                    LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                    if(!NodeType_State.isMyType[node_cur_node__node0.type.TypeID]) {
                        continue;
                    }
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.Nodes[0] = node_cur_node_wv;
                    match.Nodes[1] = node_cur_node__node0;
                    match.Nodes[2] = node_cur_node_bp;
                    match.Edges[0] = edge_cur_edge__edge0;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                        return matches;
                    }
                }
                while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_node_wv(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            // Lookup node_wv 
            int node_type_id_node_wv = 3;
            for(LGSPNode node_head_node_wv = graph.nodesByTypeHeads[node_type_id_node_wv], node_cur_node_wv = node_head_node_wv.typeNext; node_cur_node_wv != node_head_node_wv; node_cur_node_wv = node_cur_node_wv.typeNext)
            {
                // Preset node_bp 
                LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
                if(node_cur_node_bp == null) {
                    MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                    continue;
                }
                // NegativePattern 
                {
                    bool node_cur_node_bp_prevIsMatchedNeg = node_cur_node_bp.isMatchedNeg;
                    node_cur_node_bp.isMatchedNeg = true;
                    // Extend incoming neg_0_edge__edge0 from node_bp 
                    LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.inhead;
                    if(edge_head_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            // Implicit source neg_0_node__node0 from neg_0_edge__edge0 
                            LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.source;
                            if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_neg_0_node__node0.isMatchedNeg
                                && node_cur_neg_0_node__node0==node_cur_node_bp
                                )
                            {
                                continue;
                            }
                            node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                            goto label0;
                        }
                        while( (edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.inNext) != edge_head_neg_0_edge__edge0 );
                    }
                    node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                }
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node__node0 from edge__edge0 
                        LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node__node0.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_wv;
                        match.Nodes[1] = node_cur_node__node0;
                        match.Nodes[2] = node_cur_node_bp;
                        match.Edges[0] = edge_cur_edge__edge0;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                            graph.MoveHeadAfter(node_cur_node_wv);
                            return;
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
label0: ;
            }
            return;
        }
        public void MissingPreset_node_bp(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, LGSPNode node_cur_node_wv)
        {
            // Lookup node_bp 
            int node_type_id_node_bp = 1;
            for(LGSPNode node_head_node_bp = graph.nodesByTypeHeads[node_type_id_node_bp], node_cur_node_bp = node_head_node_bp.typeNext; node_cur_node_bp != node_head_node_bp; node_cur_node_bp = node_cur_node_bp.typeNext)
            {
                // NegativePattern 
                {
                    bool node_cur_node_bp_prevIsMatchedNeg = node_cur_node_bp.isMatchedNeg;
                    node_cur_node_bp.isMatchedNeg = true;
                    // Extend incoming neg_0_edge__edge0 from node_bp 
                    LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.inhead;
                    if(edge_head_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            // Implicit source neg_0_node__node0 from neg_0_edge__edge0 
                            LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.source;
                            if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_neg_0_node__node0.isMatchedNeg
                                && node_cur_neg_0_node__node0==node_cur_node_bp
                                )
                            {
                                continue;
                            }
                            node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                            goto label1;
                        }
                        while( (edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.inNext) != edge_head_neg_0_edge__edge0 );
                    }
                    node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                }
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node__node0 from edge__edge0 
                        LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node__node0.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_wv;
                        match.Nodes[1] = node_cur_node__node0;
                        match.Nodes[2] = node_cur_node_bp;
                        match.Edges[0] = edge_cur_edge__edge0;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                            graph.MoveHeadAfter(node_cur_node_bp);
                            return;
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
label1: ;
            }
            return;
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
            // Preset node_wv 
            LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
            if(node_cur_node_wv == null) {
                MissingPreset_node_wv(graph, maxMatches, parameters);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                return matches;
            }
            // Preset node_bp 
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp == null) {
                MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                return matches;
            }
            // NegativePattern 
            {
                bool node_cur_node_bp_prevIsMatchedNeg = node_cur_node_bp.isMatchedNeg;
                node_cur_node_bp.isMatchedNeg = true;
                // Extend outgoing neg_0_edge__edge0 from node_bp 
                LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.outhead;
                if(edge_head_neg_0_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                    do
                    {
                        if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target neg_0_node__node0 from neg_0_edge__edge0 
                        LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.target;
                        if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_neg_0_node__node0.isMatchedNeg
                            && node_cur_neg_0_node__node0==node_cur_node_bp
                            )
                        {
                            continue;
                        }
                        node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                        return matches;
                    }
                    while( (edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.outNext) != edge_head_neg_0_edge__edge0 );
                }
                node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
            }
            // Extend outgoing edge__edge0 from node_wv 
            LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
            if(edge_head_edge__edge0 != null)
            {
                LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                do
                {
                    if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                        continue;
                    }
                    // Implicit target node__node0 from edge__edge0 
                    LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                    if(!NodeType_State.isMyType[node_cur_node__node0.type.TypeID]) {
                        continue;
                    }
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.Nodes[0] = node_cur_node_wv;
                    match.Nodes[1] = node_cur_node__node0;
                    match.Nodes[2] = node_cur_node_bp;
                    match.Edges[0] = edge_cur_edge__edge0;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                        return matches;
                    }
                }
                while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_node_wv(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            // Lookup node_wv 
            int node_type_id_node_wv = 3;
            for(LGSPNode node_head_node_wv = graph.nodesByTypeHeads[node_type_id_node_wv], node_cur_node_wv = node_head_node_wv.typeNext; node_cur_node_wv != node_head_node_wv; node_cur_node_wv = node_cur_node_wv.typeNext)
            {
                // Preset node_bp 
                LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
                if(node_cur_node_bp == null) {
                    MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                    continue;
                }
                // NegativePattern 
                {
                    bool node_cur_node_bp_prevIsMatchedNeg = node_cur_node_bp.isMatchedNeg;
                    node_cur_node_bp.isMatchedNeg = true;
                    // Extend outgoing neg_0_edge__edge0 from node_bp 
                    LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.outhead;
                    if(edge_head_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            // Implicit target neg_0_node__node0 from neg_0_edge__edge0 
                            LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.target;
                            if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_neg_0_node__node0.isMatchedNeg
                                && node_cur_neg_0_node__node0==node_cur_node_bp
                                )
                            {
                                continue;
                            }
                            node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                            goto label2;
                        }
                        while( (edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.outNext) != edge_head_neg_0_edge__edge0 );
                    }
                    node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                }
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node__node0 from edge__edge0 
                        LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node__node0.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_wv;
                        match.Nodes[1] = node_cur_node__node0;
                        match.Nodes[2] = node_cur_node_bp;
                        match.Edges[0] = edge_cur_edge__edge0;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                            graph.MoveHeadAfter(node_cur_node_wv);
                            return;
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
label2: ;
            }
            return;
        }
        public void MissingPreset_node_bp(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, LGSPNode node_cur_node_wv)
        {
            // Lookup node_bp 
            int node_type_id_node_bp = 1;
            for(LGSPNode node_head_node_bp = graph.nodesByTypeHeads[node_type_id_node_bp], node_cur_node_bp = node_head_node_bp.typeNext; node_cur_node_bp != node_head_node_bp; node_cur_node_bp = node_cur_node_bp.typeNext)
            {
                // NegativePattern 
                {
                    bool node_cur_node_bp_prevIsMatchedNeg = node_cur_node_bp.isMatchedNeg;
                    node_cur_node_bp.isMatchedNeg = true;
                    // Extend outgoing neg_0_edge__edge0 from node_bp 
                    LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_bp.outhead;
                    if(edge_head_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            // Implicit target neg_0_node__node0 from neg_0_edge__edge0 
                            LGSPNode node_cur_neg_0_node__node0 = edge_cur_neg_0_edge__edge0.target;
                            if(!NodeType_BandPosition.isMyType[node_cur_neg_0_node__node0.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_neg_0_node__node0.isMatchedNeg
                                && node_cur_neg_0_node__node0==node_cur_node_bp
                                )
                            {
                                continue;
                            }
                            node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                            goto label3;
                        }
                        while( (edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.outNext) != edge_head_neg_0_edge__edge0 );
                    }
                    node_cur_node_bp.isMatchedNeg = node_cur_node_bp_prevIsMatchedNeg;
                }
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node__node0 from edge__edge0 
                        LGSPNode node_cur_node__node0 = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node__node0.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_wv;
                        match.Nodes[1] = node_cur_node__node0;
                        match.Nodes[2] = node_cur_node_bp;
                        match.Edges[0] = edge_cur_edge__edge0;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                            graph.MoveHeadAfter(node_cur_node_bp);
                            return;
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
label3: ;
            }
            return;
        }
    }
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
            // Preset node_wv 
            LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
            if(node_cur_node_wv == null) {
                MissingPreset_node_wv(graph, maxMatches, parameters);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                return matches;
            }
            // Preset node_bp 
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp == null) {
                MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                return matches;
            }
            // Extend outgoing edge__edge0 from node_wv 
            LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
            if(edge_head_edge__edge0 != null)
            {
                LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                do
                {
                    if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                        continue;
                    }
                    // Implicit target node_s from edge__edge0 
                    LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                    if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                        continue;
                    }
                    // Extend incoming edge__edge1 from node_bp 
                    LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.inhead;
                    if(edge_head_edge__edge1 != null)
                    {
                        LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.TypeID]) {
                                continue;
                            }
                            // Implicit source node_lbp from edge__edge1 
                            LGSPNode node_cur_node_lbp = edge_cur_edge__edge1.source;
                            if(!NodeType_BandPosition.isMyType[node_cur_node_lbp.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_node_lbp.isMatched
                                && node_cur_node_lbp==node_cur_node_bp
                                )
                            {
                                continue;
                            }
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.Nodes[0] = node_cur_node_wv;
                            match.Nodes[1] = node_cur_node_s;
                            match.Nodes[2] = node_cur_node_lbp;
                            match.Nodes[3] = node_cur_node_bp;
                            match.Edges[0] = edge_cur_edge__edge0;
                            match.Edges[1] = edge_cur_edge__edge1;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_bp.MoveInHeadAfter(edge_cur_edge__edge1);
                                node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                return matches;
                            }
                        }
                        while( (edge_cur_edge__edge1 = edge_cur_edge__edge1.inNext) != edge_head_edge__edge1 );
                    }
                }
                while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_node_wv(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            // Lookup node_wv 
            int node_type_id_node_wv = 3;
            for(LGSPNode node_head_node_wv = graph.nodesByTypeHeads[node_type_id_node_wv], node_cur_node_wv = node_head_node_wv.typeNext; node_cur_node_wv != node_head_node_wv; node_cur_node_wv = node_cur_node_wv.typeNext)
            {
                // Preset node_bp 
                LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
                if(node_cur_node_bp == null) {
                    MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                    continue;
                }
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_s from edge__edge0 
                        LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                            continue;
                        }
                        // Extend incoming edge__edge1 from node_bp 
                        LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.inhead;
                        if(edge_head_edge__edge1 != null)
                        {
                            LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                            do
                            {
                                if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                // Implicit source node_lbp from edge__edge1 
                                LGSPNode node_cur_node_lbp = edge_cur_edge__edge1.source;
                                if(!NodeType_BandPosition.isMyType[node_cur_node_lbp.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_node_lbp.isMatched
                                    && node_cur_node_lbp==node_cur_node_bp
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.Nodes[0] = node_cur_node_wv;
                                match.Nodes[1] = node_cur_node_s;
                                match.Nodes[2] = node_cur_node_lbp;
                                match.Nodes[3] = node_cur_node_bp;
                                match.Edges[0] = edge_cur_edge__edge0;
                                match.Edges[1] = edge_cur_edge__edge1;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_bp.MoveInHeadAfter(edge_cur_edge__edge1);
                                    node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                    graph.MoveHeadAfter(node_cur_node_wv);
                                    return;
                                }
                            }
                            while( (edge_cur_edge__edge1 = edge_cur_edge__edge1.inNext) != edge_head_edge__edge1 );
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
            }
            return;
        }
        public void MissingPreset_node_bp(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, LGSPNode node_cur_node_wv)
        {
            // Lookup node_bp 
            int node_type_id_node_bp = 1;
            for(LGSPNode node_head_node_bp = graph.nodesByTypeHeads[node_type_id_node_bp], node_cur_node_bp = node_head_node_bp.typeNext; node_cur_node_bp != node_head_node_bp; node_cur_node_bp = node_cur_node_bp.typeNext)
            {
                bool node_cur_node_bp_prevIsMatched = node_cur_node_bp.isMatched;
                node_cur_node_bp.isMatched = true;
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveLeft.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_s from edge__edge0 
                        LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                            continue;
                        }
                        // Extend incoming edge__edge1 from node_bp 
                        LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.inhead;
                        if(edge_head_edge__edge1 != null)
                        {
                            LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                            do
                            {
                                if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                // Implicit source node_lbp from edge__edge1 
                                LGSPNode node_cur_node_lbp = edge_cur_edge__edge1.source;
                                if(!NodeType_BandPosition.isMyType[node_cur_node_lbp.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_node_lbp.isMatched
                                    && node_cur_node_lbp==node_cur_node_bp
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.Nodes[0] = node_cur_node_wv;
                                match.Nodes[1] = node_cur_node_s;
                                match.Nodes[2] = node_cur_node_lbp;
                                match.Nodes[3] = node_cur_node_bp;
                                match.Edges[0] = edge_cur_edge__edge0;
                                match.Edges[1] = edge_cur_edge__edge1;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_bp.MoveInHeadAfter(edge_cur_edge__edge1);
                                    node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                    graph.MoveHeadAfter(node_cur_node_bp);
                                    node_cur_node_bp.isMatched = node_cur_node_bp_prevIsMatched;
                                    return;
                                }
                            }
                            while( (edge_cur_edge__edge1 = edge_cur_edge__edge1.inNext) != edge_head_edge__edge1 );
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
                node_cur_node_bp.isMatched = node_cur_node_bp_prevIsMatched;
            }
            return;
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
            // Preset node_wv 
            LGSPNode node_cur_node_wv = (LGSPNode) parameters[0];
            if(node_cur_node_wv == null) {
                MissingPreset_node_wv(graph, maxMatches, parameters);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                return matches;
            }
            // Preset node_bp 
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp == null) {
                MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                return matches;
            }
            // Extend outgoing edge__edge0 from node_wv 
            LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
            if(edge_head_edge__edge0 != null)
            {
                LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                do
                {
                    if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                        continue;
                    }
                    // Implicit target node_s from edge__edge0 
                    LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                    if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                        continue;
                    }
                    // Extend outgoing edge__edge1 from node_bp 
                    LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.outhead;
                    if(edge_head_edge__edge1 != null)
                    {
                        LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                        do
                        {
                            if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.TypeID]) {
                                continue;
                            }
                            // Implicit target node_rbp from edge__edge1 
                            LGSPNode node_cur_node_rbp = edge_cur_edge__edge1.target;
                            if(!NodeType_BandPosition.isMyType[node_cur_node_rbp.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_node_rbp.isMatched
                                && node_cur_node_rbp==node_cur_node_bp
                                )
                            {
                                continue;
                            }
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.Nodes[0] = node_cur_node_wv;
                            match.Nodes[1] = node_cur_node_s;
                            match.Nodes[2] = node_cur_node_bp;
                            match.Nodes[3] = node_cur_node_rbp;
                            match.Edges[0] = edge_cur_edge__edge0;
                            match.Edges[1] = edge_cur_edge__edge1;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_bp.MoveOutHeadAfter(edge_cur_edge__edge1);
                                node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                return matches;
                            }
                        }
                        while( (edge_cur_edge__edge1 = edge_cur_edge__edge1.outNext) != edge_head_edge__edge1 );
                    }
                }
                while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
            }
            return matches;
        }
        public void MissingPreset_node_wv(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            // Lookup node_wv 
            int node_type_id_node_wv = 3;
            for(LGSPNode node_head_node_wv = graph.nodesByTypeHeads[node_type_id_node_wv], node_cur_node_wv = node_head_node_wv.typeNext; node_cur_node_wv != node_head_node_wv; node_cur_node_wv = node_cur_node_wv.typeNext)
            {
                // Preset node_bp 
                LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
                if(node_cur_node_bp == null) {
                    MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_wv);
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                    continue;
                }
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_s from edge__edge0 
                        LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                            continue;
                        }
                        // Extend outgoing edge__edge1 from node_bp 
                        LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.outhead;
                        if(edge_head_edge__edge1 != null)
                        {
                            LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                            do
                            {
                                if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                // Implicit target node_rbp from edge__edge1 
                                LGSPNode node_cur_node_rbp = edge_cur_edge__edge1.target;
                                if(!NodeType_BandPosition.isMyType[node_cur_node_rbp.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_node_rbp.isMatched
                                    && node_cur_node_rbp==node_cur_node_bp
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.Nodes[0] = node_cur_node_wv;
                                match.Nodes[1] = node_cur_node_s;
                                match.Nodes[2] = node_cur_node_bp;
                                match.Nodes[3] = node_cur_node_rbp;
                                match.Edges[0] = edge_cur_edge__edge0;
                                match.Edges[1] = edge_cur_edge__edge1;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_bp.MoveOutHeadAfter(edge_cur_edge__edge1);
                                    node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                    graph.MoveHeadAfter(node_cur_node_wv);
                                    return;
                                }
                            }
                            while( (edge_cur_edge__edge1 = edge_cur_edge__edge1.outNext) != edge_head_edge__edge1 );
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
            }
            return;
        }
        public void MissingPreset_node_bp(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, LGSPNode node_cur_node_wv)
        {
            // Lookup node_bp 
            int node_type_id_node_bp = 1;
            for(LGSPNode node_head_node_bp = graph.nodesByTypeHeads[node_type_id_node_bp], node_cur_node_bp = node_head_node_bp.typeNext; node_cur_node_bp != node_head_node_bp; node_cur_node_bp = node_cur_node_bp.typeNext)
            {
                bool node_cur_node_bp_prevIsMatched = node_cur_node_bp.isMatched;
                node_cur_node_bp.isMatched = true;
                // Extend outgoing edge__edge0 from node_wv 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_wv.outhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_moveRight.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_s from edge__edge0 
                        LGSPNode node_cur_node_s = edge_cur_edge__edge0.target;
                        if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                            continue;
                        }
                        // Extend outgoing edge__edge1 from node_bp 
                        LGSPEdge edge_head_edge__edge1 = node_cur_node_bp.outhead;
                        if(edge_head_edge__edge1 != null)
                        {
                            LGSPEdge edge_cur_edge__edge1 = edge_head_edge__edge1;
                            do
                            {
                                if(!EdgeType_right.isMyType[edge_cur_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                // Implicit target node_rbp from edge__edge1 
                                LGSPNode node_cur_node_rbp = edge_cur_edge__edge1.target;
                                if(!NodeType_BandPosition.isMyType[node_cur_node_rbp.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_node_rbp.isMatched
                                    && node_cur_node_rbp==node_cur_node_bp
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.Nodes[0] = node_cur_node_wv;
                                match.Nodes[1] = node_cur_node_s;
                                match.Nodes[2] = node_cur_node_bp;
                                match.Nodes[3] = node_cur_node_rbp;
                                match.Edges[0] = edge_cur_edge__edge0;
                                match.Edges[1] = edge_cur_edge__edge1;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_bp.MoveOutHeadAfter(edge_cur_edge__edge1);
                                    node_cur_node_wv.MoveOutHeadAfter(edge_cur_edge__edge0);
                                    graph.MoveHeadAfter(node_cur_node_bp);
                                    node_cur_node_bp.isMatched = node_cur_node_bp_prevIsMatched;
                                    return;
                                }
                            }
                            while( (edge_cur_edge__edge1 = edge_cur_edge__edge1.outNext) != edge_head_edge__edge1 );
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.outNext) != edge_head_edge__edge0 );
                }
                node_cur_node_bp.isMatched = node_cur_node_bp_prevIsMatched;
            }
            return;
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
            // Preset node_s 
            LGSPNode node_cur_node_s = (LGSPNode) parameters[0];
            if(node_cur_node_s == null) {
                MissingPreset_node_s(graph, maxMatches, parameters);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                return matches;
            }
            // Preset node_bp 
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp == null) {
                MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_s);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                return matches;
            }
            // Condition 
            if(!Rule_readOneRule.Condition_0(node_cur_node_bp)) {
                return matches;
            }
            // Extend outgoing edge_rv from node_s 
            LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
            if(edge_head_edge_rv != null)
            {
                LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                do
                {
                    if(!EdgeType_readOne.isMyType[edge_cur_edge_rv.type.TypeID]) {
                        continue;
                    }
                    // Implicit target node_wv from edge_rv 
                    LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                    if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                        continue;
                    }
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.Nodes[0] = node_cur_node_s;
                    match.Nodes[1] = node_cur_node_wv;
                    match.Nodes[2] = node_cur_node_bp;
                    match.Edges[0] = edge_cur_edge_rv;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                        return matches;
                    }
                }
                while( (edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv );
            }
            return matches;
        }
        public void MissingPreset_node_s(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            // Lookup node_s 
            int node_type_id_node_s = 2;
            for(LGSPNode node_head_node_s = graph.nodesByTypeHeads[node_type_id_node_s], node_cur_node_s = node_head_node_s.typeNext; node_cur_node_s != node_head_node_s; node_cur_node_s = node_cur_node_s.typeNext)
            {
                // Preset node_bp 
                LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
                if(node_cur_node_bp == null) {
                    MissingPreset_node_bp(graph, maxMatches, parameters, node_cur_node_s);
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                    continue;
                }
                // Condition 
                if(!Rule_readOneRule.Condition_0(node_cur_node_bp)) {
                    continue;
                }
                // Extend outgoing edge_rv from node_s 
                LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
                if(edge_head_edge_rv != null)
                {
                    LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                    do
                    {
                        if(!EdgeType_readOne.isMyType[edge_cur_edge_rv.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_wv from edge_rv 
                        LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                        if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_s;
                        match.Nodes[1] = node_cur_node_wv;
                        match.Nodes[2] = node_cur_node_bp;
                        match.Edges[0] = edge_cur_edge_rv;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                            graph.MoveHeadAfter(node_cur_node_s);
                            return;
                        }
                    }
                    while( (edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv );
                }
            }
            return;
        }
        public void MissingPreset_node_bp(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, LGSPNode node_cur_node_s)
        {
            // Lookup node_bp 
            int node_type_id_node_bp = 1;
            for(LGSPNode node_head_node_bp = graph.nodesByTypeHeads[node_type_id_node_bp], node_cur_node_bp = node_head_node_bp.typeNext; node_cur_node_bp != node_head_node_bp; node_cur_node_bp = node_cur_node_bp.typeNext)
            {
                // Condition 
                if(!Rule_readOneRule.Condition_0(node_cur_node_bp)) {
                    continue;
                }
                // Extend outgoing edge_rv from node_s 
                LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
                if(edge_head_edge_rv != null)
                {
                    LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                    do
                    {
                        if(!EdgeType_readOne.isMyType[edge_cur_edge_rv.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_wv from edge_rv 
                        LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                        if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_s;
                        match.Nodes[1] = node_cur_node_wv;
                        match.Nodes[2] = node_cur_node_bp;
                        match.Edges[0] = edge_cur_edge_rv;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                            graph.MoveHeadAfter(node_cur_node_bp);
                            return;
                        }
                    }
                    while( (edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv );
                }
            }
            return;
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
            // Preset node_bp 
            LGSPNode node_cur_node_bp = (LGSPNode) parameters[1];
            if(node_cur_node_bp == null) {
                MissingPreset_node_bp(graph, maxMatches, parameters);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_BandPosition.isMyType[node_cur_node_bp.type.TypeID]) {
                return matches;
            }
            // Condition 
            if(!Rule_readZeroRule.Condition_0(node_cur_node_bp)) {
                return matches;
            }
            // Preset node_s 
            LGSPNode node_cur_node_s = (LGSPNode) parameters[0];
            if(node_cur_node_s == null) {
                MissingPreset_node_s(graph, maxMatches, parameters, node_cur_node_bp);
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                return matches;
            }
            // Extend outgoing edge_rv from node_s 
            LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
            if(edge_head_edge_rv != null)
            {
                LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                do
                {
                    if(!EdgeType_readZero.isMyType[edge_cur_edge_rv.type.TypeID]) {
                        continue;
                    }
                    // Implicit target node_wv from edge_rv 
                    LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                    if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                        continue;
                    }
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.Nodes[0] = node_cur_node_bp;
                    match.Nodes[1] = node_cur_node_s;
                    match.Nodes[2] = node_cur_node_wv;
                    match.Edges[0] = edge_cur_edge_rv;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                        return matches;
                    }
                }
                while( (edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv );
            }
            return matches;
        }
        public void MissingPreset_node_bp(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            // Lookup node_bp 
            int node_type_id_node_bp = 1;
            for(LGSPNode node_head_node_bp = graph.nodesByTypeHeads[node_type_id_node_bp], node_cur_node_bp = node_head_node_bp.typeNext; node_cur_node_bp != node_head_node_bp; node_cur_node_bp = node_cur_node_bp.typeNext)
            {
                // Condition 
                if(!Rule_readZeroRule.Condition_0(node_cur_node_bp)) {
                    continue;
                }
                // Preset node_s 
                LGSPNode node_cur_node_s = (LGSPNode) parameters[0];
                if(node_cur_node_s == null) {
                    MissingPreset_node_s(graph, maxMatches, parameters, node_cur_node_bp);
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        return;
                    }
                    continue;
                }
                if(!NodeType_State.isMyType[node_cur_node_s.type.TypeID]) {
                    continue;
                }
                // Extend outgoing edge_rv from node_s 
                LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
                if(edge_head_edge_rv != null)
                {
                    LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                    do
                    {
                        if(!EdgeType_readZero.isMyType[edge_cur_edge_rv.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_wv from edge_rv 
                        LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                        if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_bp;
                        match.Nodes[1] = node_cur_node_s;
                        match.Nodes[2] = node_cur_node_wv;
                        match.Edges[0] = edge_cur_edge_rv;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                            graph.MoveHeadAfter(node_cur_node_bp);
                            return;
                        }
                    }
                    while( (edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv );
                }
            }
            return;
        }
        public void MissingPreset_node_s(LGSPGraph graph, int maxMatches, IGraphElement[] parameters, LGSPNode node_cur_node_bp)
        {
            // Lookup node_s 
            int node_type_id_node_s = 2;
            for(LGSPNode node_head_node_s = graph.nodesByTypeHeads[node_type_id_node_s], node_cur_node_s = node_head_node_s.typeNext; node_cur_node_s != node_head_node_s; node_cur_node_s = node_cur_node_s.typeNext)
            {
                // Extend outgoing edge_rv from node_s 
                LGSPEdge edge_head_edge_rv = node_cur_node_s.outhead;
                if(edge_head_edge_rv != null)
                {
                    LGSPEdge edge_cur_edge_rv = edge_head_edge_rv;
                    do
                    {
                        if(!EdgeType_readZero.isMyType[edge_cur_edge_rv.type.TypeID]) {
                            continue;
                        }
                        // Implicit target node_wv from edge_rv 
                        LGSPNode node_cur_node_wv = edge_cur_edge_rv.target;
                        if(!NodeType_WriteValue.isMyType[node_cur_node_wv.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_bp;
                        match.Nodes[1] = node_cur_node_s;
                        match.Nodes[2] = node_cur_node_wv;
                        match.Edges[0] = edge_cur_edge_rv;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_s.MoveOutHeadAfter(edge_cur_edge_rv);
                            graph.MoveHeadAfter(node_cur_node_s);
                            return;
                        }
                    }
                    while( (edge_cur_edge_rv = edge_cur_edge_rv.outNext) != edge_head_edge_rv );
                }
            }
            return;
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
            actions.Add("ensureMoveLeftValidRule", (LGSPAction) Action_ensureMoveLeftValidRule.Instance);
            actions.Add("ensureMoveRightValidRule", (LGSPAction) Action_ensureMoveRightValidRule.Instance);
            actions.Add("moveLeftRule", (LGSPAction) Action_moveLeftRule.Instance);
            actions.Add("moveRightRule", (LGSPAction) Action_moveRightRule.Instance);
            actions.Add("readOneRule", (LGSPAction) Action_readOneRule.Instance);
            actions.Add("readZeroRule", (LGSPAction) Action_readZeroRule.Instance);
        }

        public override String Name { get { return "Turing3Actions"; } }
        public override String ModelMD5Hash { get { return "5a78f363d1b6a0cc5cea759830c3e6b1"; } }
    }
}