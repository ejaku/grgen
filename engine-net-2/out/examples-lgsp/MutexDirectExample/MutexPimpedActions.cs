using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.MutexPimped;

namespace de.unika.ipd.grGen.actions.MutexPimped
{
	public class Rule_giveRule : LGSPRulePattern
	{
		private static Rule_giveRule instance = null;
		public static Rule_giveRule Instance { get { if (instance==null) instance = new Rule_giveRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge_rel_AllowedTypes = null;
		public static EdgeType[] edge_n_AllowedTypes = null;
		public static bool[] edge_rel_IsAllowedType = null;
		public static bool[] edge_n_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @r, @p1, };
		public enum EdgeNums { @rel = 1, @n, };

		private Rule_giveRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rel = new PatternEdge(node_r, node_p1, (int) EdgeTypes.@release, "edge_rel", edge_rel_AllowedTypes, edge_rel_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_r, node_p1 }, 
				new PatternEdge[] { edge_rel, edge_n }, 
				new Condition[] { },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_rel = match.edges[ (int) EdgeNums.@rel - 1 ];
			// re-using edge_rel as edge_t
			LGSPEdge edge_t = edge_rel;
			graph.ReuseEdge(edge_rel, null, node_p2, EdgeType_token.typeVar);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_rel = match.edges[ (int) EdgeNums.@rel - 1 ];
			LGSPEdge edge_t = graph.AddEdge(EdgeType_token.typeVar, node_r, node_p2);
			graph.Remove(edge_rel);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_giveRule : LGSPStaticScheduleInfo
	{
		public Schedule_giveRule()
		{
			ActionName = "giveRule";
			this.RulePattern = Rule_giveRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 1.0F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_requestRule : LGSPRulePattern
	{
		private static Rule_requestRule instance = null;
		public static Rule_requestRule Instance { get { if (instance==null) instance = new Rule_requestRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_hb_AllowedTypes = null;
		public static bool[] neg_0_edge_hb_IsAllowedType = null;
		public static NodeType[] neg_1_node_m_AllowedTypes = null;
		public static bool[] neg_1_node_m_IsAllowedType = null;
		public static EdgeType[] neg_1_edge_req_AllowedTypes = null;
		public static bool[] neg_1_edge_req_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { };

		private Rule_requestRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] {  }, 
				new Condition[] { },
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, false, },
				new bool[] {}
			);

			PatternGraph negPattern_0;
			{
			PatternEdge neg_0_edge_hb = new PatternEdge(node_r, node_p, (int) EdgeTypes.@held_by, "neg_0_edge_hb", neg_0_edge_hb_AllowedTypes, neg_0_edge_hb_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { neg_0_edge_hb }, 
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

			PatternGraph negPattern_1;
			{
			PatternNode neg_1_node_m = new PatternNode((int) NodeTypes.@Resource, "neg_1_node_m", neg_1_node_m_AllowedTypes, neg_1_node_m_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_1_edge_req = new PatternEdge(node_p, neg_1_node_m, (int) EdgeTypes.@request, "neg_1_edge_req", neg_1_edge_req_AllowedTypes, neg_1_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_1 = new PatternGraph(
				new PatternNode[] { neg_1_node_m, node_p }, 
				new PatternEdge[] { neg_1_edge_req }, 
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

			negativePatternGraphs = new PatternGraph[] {negPattern_0, negPattern_1, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_req = graph.AddEdge(EdgeType_request.typeVar, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_req = graph.AddEdge(EdgeType_request.typeVar, node_p, node_r);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_requestRule : LGSPStaticScheduleInfo
	{
		public Schedule_requestRule()
		{
			ActionName = "requestRule";
			this.RulePattern = Rule_requestRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F,  };
			EdgeCost = new float[] {  };
			NegNodeCost = new float[][] { new float[] { 1.0F, 5.5F, }, new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_unlockRule : LGSPRulePattern
	{
		private static Rule_unlockRule instance = null;
		public static Rule_unlockRule Instance { get { if (instance==null) instance = new Rule_unlockRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] edge_b_AllowedTypes = null;
		public static EdgeType[] edge_hb_AllowedTypes = null;
		public static bool[] edge_b_IsAllowedType = null;
		public static bool[] edge_hb_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { @b = 1, @hb, };

		private Rule_unlockRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_b = new PatternEdge(node_r, node_p, (int) EdgeTypes.@blocked, "edge_b", edge_b_AllowedTypes, edge_b_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r, node_p, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { edge_b, edge_hb }, 
				new Condition[] { },
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_b = match.edges[ (int) EdgeNums.@b - 1 ];
			LGSPEdge edge_hb = match.edges[ (int) EdgeNums.@hb - 1 ];
			// re-using edge_b as edge_rel
			LGSPEdge edge_rel = edge_b;
			graph.ReuseEdge(edge_b, null, null, EdgeType_release.typeVar);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rel" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_b = match.edges[ (int) EdgeNums.@b - 1 ];
			LGSPEdge edge_hb = match.edges[ (int) EdgeNums.@hb - 1 ];
			LGSPEdge edge_rel = graph.AddEdge(EdgeType_release.typeVar, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_unlockRule : LGSPStaticScheduleInfo
	{
		public Schedule_unlockRule()
		{
			ActionName = "unlockRule";
			this.RulePattern = Rule_unlockRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_takeRule : LGSPRulePattern
	{
		private static Rule_takeRule instance = null;
		public static Rule_takeRule Instance { get { if (instance==null) instance = new Rule_takeRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] edge_t_AllowedTypes = null;
		public static EdgeType[] edge_req_AllowedTypes = null;
		public static bool[] edge_t_IsAllowedType = null;
		public static bool[] edge_req_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { @t = 1, @req, };

		private Rule_takeRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_t = new PatternEdge(node_r, node_p, (int) EdgeTypes.@token, "edge_t", edge_t_AllowedTypes, edge_t_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p, node_r, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { edge_t, edge_req }, 
				new Condition[] { },
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, },
				new bool[] {
					false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_req = match.edges[ (int) EdgeNums.@req - 1 ];
			LGSPEdge edge_t = match.edges[ (int) EdgeNums.@t - 1 ];
			// re-using edge_t as edge_hb
			LGSPEdge edge_hb = edge_t;
			graph.ReuseEdge(edge_t, null, null, EdgeType_held_by.typeVar);
			graph.Remove(edge_req);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "hb" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_t = match.edges[ (int) EdgeNums.@t - 1 ];
			LGSPEdge edge_req = match.edges[ (int) EdgeNums.@req - 1 ];
			LGSPEdge edge_hb = graph.AddEdge(EdgeType_held_by.typeVar, node_r, node_p);
			graph.Remove(edge_req);
			graph.Remove(edge_t);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_takeRule : LGSPStaticScheduleInfo
	{
		public Schedule_takeRule()
		{
			ActionName = "takeRule";
			this.RulePattern = Rule_takeRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F,  };
			EdgeCost = new float[] { 1.0F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_releaseRule : LGSPRulePattern
	{
		private static Rule_releaseRule instance = null;
		public static Rule_releaseRule Instance { get { if (instance==null) instance = new Rule_releaseRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] edge_hb_AllowedTypes = null;
		public static bool[] edge_hb_IsAllowedType = null;
		public static NodeType[] neg_0_node_m_AllowedTypes = null;
		public static bool[] neg_0_node_m_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_req_AllowedTypes = null;
		public static bool[] neg_0_edge_req_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { @hb = 1, };

		private Rule_releaseRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r, node_p, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { edge_hb }, 
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

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node_m = new PatternNode((int) NodeTypes.@Resource, "neg_0_node_m", neg_0_node_m_AllowedTypes, neg_0_node_m_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge_req = new PatternEdge(node_p, neg_0_node_m, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { neg_0_node_m, node_p }, 
				new PatternEdge[] { neg_0_edge_req }, 
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
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_hb = match.edges[ (int) EdgeNums.@hb - 1 ];
			// re-using edge_hb as edge_rel
			LGSPEdge edge_rel = edge_hb;
			graph.ReuseEdge(edge_hb, null, null, EdgeType_release.typeVar);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rel" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_hb = match.edges[ (int) EdgeNums.@hb - 1 ];
			LGSPEdge edge_rel = graph.AddEdge(EdgeType_release.typeVar, node_r, node_p);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_releaseRule : LGSPStaticScheduleInfo
	{
		public Schedule_releaseRule()
		{
			ActionName = "releaseRule";
			this.RulePattern = Rule_releaseRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F,  };
			EdgeCost = new float[] { 1.0F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_waitingRule : LGSPRulePattern
	{
		private static Rule_waitingRule instance = null;
		public static Rule_waitingRule Instance { get { if (instance==null) instance = new Rule_waitingRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r2_AllowedTypes = null;
		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_r1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static EdgeType[] edge_b_AllowedTypes = null;
		public static EdgeType[] edge_hb_AllowedTypes = null;
		public static EdgeType[] edge_req_AllowedTypes = null;
		public static bool[] edge_b_IsAllowedType = null;
		public static bool[] edge_hb_IsAllowedType = null;
		public static bool[] edge_req_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @r2, @r, @p1, @r1, };
		public enum EdgeNums { @b = 1, @hb, @req, };

		private Rule_waitingRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_b = new PatternEdge(node_r2, node_p1, (int) EdgeTypes.@blocked, "edge_b", edge_b_AllowedTypes, edge_b_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r1, node_p1, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p2, node_r1, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_r2, node_r, node_p1, node_r1 }, 
				new PatternEdge[] { edge_b, edge_hb, edge_req }, 
				new Condition[] { },
				new bool[5, 5] {
					{ true, false, false, false, false, },
					{ false, true, false, false, false, },
					{ false, false, true, false, false, },
					{ false, false, false, true, false, },
					{ false, false, false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[] {
					false, false, false, false, false, },
				new bool[] {
					false, false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_r2 = match.nodes[ (int) NodeNums.@r2 - 1 ];
			LGSPEdge edge_b = match.edges[ (int) EdgeNums.@b - 1 ];
			// re-using edge_b as edge_bn
			LGSPEdge edge_bn = edge_b;
			graph.ReuseEdge(edge_b, null, node_p2, null);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "bn" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_r2 = match.nodes[ (int) NodeNums.@r2 - 1 ];
			LGSPEdge edge_b = match.edges[ (int) EdgeNums.@b - 1 ];
			LGSPEdge edge_bn = graph.AddEdge(EdgeType_blocked.typeVar, node_r2, node_p2);
			graph.Remove(edge_b);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_waitingRule : LGSPStaticScheduleInfo
	{
		public Schedule_waitingRule()
		{
			ActionName = "waitingRule";
			this.RulePattern = Rule_waitingRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 1.0F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_unmountRule : LGSPRulePattern
	{
		private static Rule_unmountRule instance = null;
		public static Rule_unmountRule Instance { get { if (instance==null) instance = new Rule_unmountRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] edge_t_AllowedTypes = null;
		public static bool[] edge_t_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { @t = 1, };

		private Rule_unmountRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_t = new PatternEdge(node_r, node_p, (int) EdgeTypes.@token, "edge_t", edge_t_AllowedTypes, edge_t_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { edge_t }, 
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

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_t = match.edges[ (int) EdgeNums.@t - 1 ];
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_t = match.edges[ (int) EdgeNums.@t - 1 ];
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_unmountRule : LGSPStaticScheduleInfo
	{
		public Schedule_unmountRule()
		{
			ActionName = "unmountRule";
			this.RulePattern = Rule_unmountRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_newRule : LGSPRulePattern
	{
		private static Rule_newRule instance = null;
		public static Rule_newRule Instance { get { if (instance==null) instance = new Rule_newRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge_n_AllowedTypes = null;
		public static bool[] edge_n_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @p1, };
		public enum EdgeNums { @n = 1, };

		private Rule_newRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_p1 }, 
				new PatternEdge[] { edge_n }, 
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

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_n = match.edges[ (int) EdgeNums.@n - 1 ];
			LGSPNode node_p = graph.AddNode(NodeType_Process.typeVar);
			// re-using edge_n as edge_n2
			LGSPEdge edge_n2 = edge_n;
			graph.ReuseEdge(edge_n, node_p, null, null);
			LGSPEdge edge_n1 = graph.AddEdge(EdgeType_next.typeVar, node_p1, node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "p" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "n2", "n1" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_n = match.edges[ (int) EdgeNums.@n - 1 ];
			LGSPNode node_p = graph.AddNode(NodeType_Process.typeVar);
			LGSPEdge edge_n2 = graph.AddEdge(EdgeType_next.typeVar, node_p, node_p2);
			LGSPEdge edge_n1 = graph.AddEdge(EdgeType_next.typeVar, node_p1, node_p);
			graph.Remove(edge_n);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_newRule : LGSPStaticScheduleInfo
	{
		public Schedule_newRule()
		{
			ActionName = "newRule";
			this.RulePattern = Rule_newRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F,  };
			EdgeCost = new float[] { 1.0F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_requestStarRule : LGSPRulePattern
	{
		private static Rule_requestStarRule instance = null;
		public static Rule_requestStarRule Instance { get { if (instance==null) instance = new Rule_requestStarRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_r1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static EdgeType[] edge_h2_AllowedTypes = null;
		public static EdgeType[] edge_n_AllowedTypes = null;
		public static EdgeType[] edge_h1_AllowedTypes = null;
		public static bool[] edge_h2_IsAllowedType = null;
		public static bool[] edge_n_IsAllowedType = null;
		public static bool[] edge_h1_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_req_AllowedTypes = null;
		public static bool[] neg_0_edge_req_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @r2, @p1, @r1, };
		public enum EdgeNums { @h2 = 1, @n, @h1, };

		private Rule_requestStarRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h2 = new PatternEdge(node_r2, node_p2, (int) EdgeTypes.@held_by, "edge_h2", edge_h2_AllowedTypes, edge_h2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p2, node_p1, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h1 = new PatternEdge(node_r1, node_p1, (int) EdgeTypes.@held_by, "edge_h1", edge_h1_AllowedTypes, edge_h1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_r2, node_p1, node_r1 }, 
				new PatternEdge[] { edge_h2, edge_n, edge_h1 }, 
				new Condition[] { },
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[] {
					false, false, false, false, },
				new bool[] {
					false, false, false, }
			);

			PatternGraph negPattern_0;
			{
			PatternEdge neg_0_edge_req = new PatternEdge(node_p1, node_r2, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_r2, node_p1 }, 
				new PatternEdge[] { neg_0_edge_req }, 
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
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r2 = match.nodes[ (int) NodeNums.@r2 - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_req = graph.AddEdge(EdgeType_request.typeVar, node_p1, node_r2);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r2 = match.nodes[ (int) NodeNums.@r2 - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_req = graph.AddEdge(EdgeType_request.typeVar, node_p1, node_r2);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_requestStarRule : LGSPStaticScheduleInfo
	{
		public Schedule_requestStarRule()
		{
			ActionName = "requestStarRule";
			this.RulePattern = Rule_requestStarRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_passRule : LGSPRulePattern
	{
		private static Rule_passRule instance = null;
		public static Rule_passRule Instance { get { if (instance==null) instance = new Rule_passRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static EdgeType[] edge_n_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static bool[] edge_n_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_req_AllowedTypes = null;
		public static bool[] neg_0_edge_req_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @r, @p1, };
		public enum EdgeNums { @_edge0 = 1, @n, };

		private Rule_passRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_r, node_p1, (int) EdgeTypes.@token, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_r, node_p1 }, 
				new PatternEdge[] { edge__edge0, edge_n }, 
				new Condition[] { },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, false, }
			);

			PatternGraph negPattern_0;
			{
			PatternEdge neg_0_edge_req = new PatternEdge(node_p1, node_r, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_r, node_p1 }, 
				new PatternEdge[] { neg_0_edge_req }, 
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
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge__edge0 = match.edges[ (int) EdgeNums.@_edge0 - 1 ];
			// re-using edge__edge0 as edge_t
			LGSPEdge edge_t = edge__edge0;
			graph.ReuseEdge(edge__edge0, null, node_p2, null);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge__edge0 = match.edges[ (int) EdgeNums.@_edge0 - 1 ];
			LGSPEdge edge_t = graph.AddEdge(EdgeType_token.typeVar, node_r, node_p2);
			graph.Remove(edge__edge0);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_passRule : LGSPStaticScheduleInfo
	{
		public Schedule_passRule()
		{
			ActionName = "passRule";
			this.RulePattern = Rule_passRule.Instance;
			NodeCost = new float[] { 5.5F, 1.0F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 1.0F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_blockedRule : LGSPRulePattern
	{
		private static Rule_blockedRule instance = null;
		public static Rule_blockedRule Instance { get { if (instance==null) instance = new Rule_blockedRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge_hb_AllowedTypes = null;
		public static EdgeType[] edge_req_AllowedTypes = null;
		public static bool[] edge_hb_IsAllowedType = null;
		public static bool[] edge_req_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @r, @p1, };
		public enum EdgeNums { @hb = 1, @req, };

		private Rule_blockedRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r, node_p2, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p1, node_r, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_r, node_p1 }, 
				new PatternEdge[] { edge_hb, edge_req }, 
				new Condition[] { },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_b = graph.AddEdge(EdgeType_blocked.typeVar, node_r, node_p1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "b" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_b = graph.AddEdge(EdgeType_blocked.typeVar, node_r, node_p1);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_blockedRule : LGSPStaticScheduleInfo
	{
		public Schedule_blockedRule()
		{
			ActionName = "blockedRule";
			this.RulePattern = Rule_blockedRule.Instance;
			NodeCost = new float[] { 5.5F, 1.0F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_requestSimpleRule : LGSPRulePattern
	{
		private static Rule_requestSimpleRule instance = null;
		public static Rule_requestSimpleRule Instance { get { if (instance==null) instance = new Rule_requestSimpleRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] edge_t_AllowedTypes = null;
		public static bool[] edge_t_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_req_AllowedTypes = null;
		public static bool[] neg_0_edge_req_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { @t = 1, };

		private Rule_requestSimpleRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_t = new PatternEdge(node_r, node_p, (int) EdgeTypes.@token, "edge_t", edge_t_AllowedTypes, edge_t_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { edge_t }, 
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

			PatternGraph negPattern_0;
			{
			PatternEdge neg_0_edge_req = new PatternEdge(node_p, node_r, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { neg_0_edge_req }, 
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
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_req = graph.AddEdge(EdgeType_request.typeVar, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_req = graph.AddEdge(EdgeType_request.typeVar, node_p, node_r);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_requestSimpleRule : LGSPStaticScheduleInfo
	{
		public Schedule_requestSimpleRule()
		{
			ActionName = "requestSimpleRule";
			this.RulePattern = Rule_requestSimpleRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 1.0F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_killRule : LGSPRulePattern
	{
		private static Rule_killRule instance = null;
		public static Rule_killRule Instance { get { if (instance==null) instance = new Rule_killRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] edge_n2_AllowedTypes = null;
		public static EdgeType[] edge_n1_AllowedTypes = null;
		public static bool[] edge_n2_IsAllowedType = null;
		public static bool[] edge_n1_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @p1, @p, };
		public enum EdgeNums { @n2 = 1, @n1, };

		private Rule_killRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n2 = new PatternEdge(node_p, node_p2, (int) EdgeTypes.@next, "edge_n2", edge_n2_AllowedTypes, edge_n2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n1 = new PatternEdge(node_p1, node_p, (int) EdgeTypes.@next, "edge_n1", edge_n1_AllowedTypes, edge_n1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_p1, node_p }, 
				new PatternEdge[] { edge_n2, edge_n1 }, 
				new Condition[] { },
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[] {
					false, false, false, },
				new bool[] {
					false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_n2 = match.edges[ (int) EdgeNums.@n2 - 1 ];
			LGSPEdge edge_n1 = match.edges[ (int) EdgeNums.@n1 - 1 ];
			// re-using edge_n2 as edge_n
			LGSPEdge edge_n = edge_n2;
			graph.ReuseEdge(edge_n2, node_p1, null, null);
			graph.Remove(edge_n1);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "n" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_n2 = match.edges[ (int) EdgeNums.@n2 - 1 ];
			LGSPEdge edge_n1 = match.edges[ (int) EdgeNums.@n1 - 1 ];
			LGSPEdge edge_n = graph.AddEdge(EdgeType_next.typeVar, node_p1, node_p2);
			graph.Remove(edge_n2);
			graph.Remove(edge_n1);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_killRule : LGSPStaticScheduleInfo
	{
		public Schedule_killRule()
		{
			ActionName = "killRule";
			this.RulePattern = Rule_killRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_releaseStarRule : LGSPRulePattern
	{
		private static Rule_releaseStarRule instance = null;
		public static Rule_releaseStarRule Instance { get { if (instance==null) instance = new Rule_releaseStarRule(); return instance; } }

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_r1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static EdgeType[] edge_rq_AllowedTypes = null;
		public static EdgeType[] edge_h2_AllowedTypes = null;
		public static EdgeType[] edge_h1_AllowedTypes = null;
		public static bool[] edge_rq_IsAllowedType = null;
		public static bool[] edge_h2_IsAllowedType = null;
		public static bool[] edge_h1_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @r2, @p1, @r1, };
		public enum EdgeNums { @rq = 1, @h2, @h1, };

		private Rule_releaseStarRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rq = new PatternEdge(node_p1, node_r1, (int) EdgeTypes.@request, "edge_rq", edge_rq_AllowedTypes, edge_rq_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h2 = new PatternEdge(node_r2, node_p2, (int) EdgeTypes.@held_by, "edge_h2", edge_h2_AllowedTypes, edge_h2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h1 = new PatternEdge(node_r1, node_p2, (int) EdgeTypes.@held_by, "edge_h1", edge_h1_AllowedTypes, edge_h1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_r2, node_p1, node_r1 }, 
				new PatternEdge[] { edge_rq, edge_h2, edge_h1 }, 
				new Condition[] { },
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[] {
					false, false, false, false, },
				new bool[] {
					false, false, false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r1 = match.nodes[ (int) NodeNums.@r1 - 1 ];
			LGSPEdge edge_h1 = match.edges[ (int) EdgeNums.@h1 - 1 ];
			// re-using edge_h1 as edge_rl
			LGSPEdge edge_rl = edge_h1;
			graph.ReuseEdge(edge_h1, null, null, EdgeType_release.typeVar);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rl" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r1 = match.nodes[ (int) NodeNums.@r1 - 1 ];
			LGSPEdge edge_h1 = match.edges[ (int) EdgeNums.@h1 - 1 ];
			LGSPEdge edge_rl = graph.AddEdge(EdgeType_release.typeVar, node_r1, node_p2);
			graph.Remove(edge_h1);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_releaseStarRule : LGSPStaticScheduleInfo
	{
		public Schedule_releaseStarRule()
		{
			ActionName = "releaseStarRule";
			this.RulePattern = Rule_releaseStarRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_mountRule : LGSPRulePattern
	{
		private static Rule_mountRule instance = null;
		public static Rule_mountRule Instance { get { if (instance==null) instance = new Rule_mountRule(); return instance; } }

		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_p_IsAllowedType = null;

		public enum NodeNums { @p  = 1, };
		public enum EdgeNums { };

		private Rule_mountRule()
		{
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p }, 
				new PatternEdge[] {  }, 
				new Condition[] { },
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {}
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = graph.AddNode(NodeType_Resource.typeVar);
			LGSPEdge edge_t = graph.AddEdge(EdgeType_token.typeVar, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "r" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = graph.AddNode(NodeType_Resource.typeVar);
			LGSPEdge edge_t = graph.AddEdge(EdgeType_token.typeVar, node_r, node_p);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_mountRule : LGSPStaticScheduleInfo
	{
		public Schedule_mountRule()
		{
			ActionName = "mountRule";
			this.RulePattern = Rule_mountRule.Instance;
			NodeCost = new float[] { 5.5F,  };
			EdgeCost = new float[] {  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_aux_attachResource : LGSPRulePattern
	{
		private static Rule_aux_attachResource instance = null;
		public static Rule_aux_attachResource Instance { get { if (instance==null) instance = new Rule_aux_attachResource(); return instance; } }

		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_p_IsAllowedType = null;
		public static NodeType[] neg_0_node_r_AllowedTypes = null;
		public static bool[] neg_0_node_r_IsAllowedType = null;
		public static EdgeType[] neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] neg_0_edge__edge0_IsAllowedType = null;

		public enum NodeNums { @p  = 1, };
		public enum EdgeNums { };

		private Rule_aux_attachResource()
		{
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p }, 
				new PatternEdge[] {  }, 
				new Condition[] { },
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, },
				new bool[] {}
			);

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node_r = new PatternNode((int) NodeTypes.@Resource, "neg_0_node_r", neg_0_node_r_AllowedTypes, neg_0_node_r_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge__edge0 = new PatternEdge(neg_0_node_r, node_p, (int) EdgeTypes.@held_by, "neg_0_edge__edge0", neg_0_edge__edge0_AllowedTypes, neg_0_edge__edge0_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { neg_0_node_r, node_p }, 
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
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = graph.AddNode(NodeType_Resource.typeVar);
			LGSPEdge edge__edge0 = graph.AddEdge(EdgeType_held_by.typeVar, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "r" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = graph.AddNode(NodeType_Resource.typeVar);
			LGSPEdge edge__edge0 = graph.AddEdge(EdgeType_held_by.typeVar, node_r, node_p);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_aux_attachResource : LGSPStaticScheduleInfo
	{
		public Schedule_aux_attachResource()
		{
			ActionName = "aux_attachResource";
			this.RulePattern = Rule_aux_attachResource.Instance;
			NodeCost = new float[] { 5.5F,  };
			EdgeCost = new float[] {  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_ignoreRule : LGSPRulePattern
	{
		private static Rule_ignoreRule instance = null;
		public static Rule_ignoreRule Instance { get { if (instance==null) instance = new Rule_ignoreRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static EdgeType[] edge_b_AllowedTypes = null;
		public static bool[] edge_b_IsAllowedType = null;
		public static NodeType[] neg_0_node_m_AllowedTypes = null;
		public static bool[] neg_0_node_m_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_hb_AllowedTypes = null;
		public static bool[] neg_0_edge_hb_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { @b = 1, };

		private Rule_ignoreRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_b = new PatternEdge(node_r, node_p, (int) EdgeTypes.@blocked, "edge_b", edge_b_AllowedTypes, edge_b_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { edge_b }, 
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

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node_m = new PatternNode((int) NodeTypes.@Resource, "neg_0_node_m", neg_0_node_m_AllowedTypes, neg_0_node_m_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge_hb = new PatternEdge(neg_0_node_m, node_p, (int) EdgeTypes.@held_by, "neg_0_edge_hb", neg_0_edge_hb_AllowedTypes, neg_0_edge_hb_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { neg_0_node_m, node_p }, 
				new PatternEdge[] { neg_0_edge_hb }, 
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
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge_b = match.edges[ (int) EdgeNums.@b - 1 ];
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge_b = match.edges[ (int) EdgeNums.@b - 1 ];
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_ignoreRule : LGSPStaticScheduleInfo
	{
		public Schedule_ignoreRule()
		{
			ActionName = "ignoreRule";
			this.RulePattern = Rule_ignoreRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif


    public class Action_giveRule : LGSPAction
    {
        private static Action_giveRule instance = new Action_giveRule();

        public Action_giveRule() { rulePattern = Rule_giveRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2); matchesList = matches.matches;}

        public override string Name { get { return "giveRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_n:next)
            foreach(EdgeType edge_type_edge_n in EdgeType_next.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_edge_n.TypeID], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
                {
                    edge_cur_edge_n.mappedTo = 2;
                    // ImplicitTarget(edge_n -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_n_2;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitSource(edge_n -> node_p1:Process)
                    LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_node_cur_node_p2_4;
                    if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_7;
                    node_cur_node_p1.mappedTo = 3;
                    // ExtendIncoming(node_p1 -> edge_rel:release)
                    LGSPEdge edge_head_edge_rel = node_cur_node_p1.inhead;
                    if(edge_head_edge_rel != null)
                    {
                        LGSPEdge edge_cur_edge_rel = edge_head_edge_rel;
                        do
                        {
                            if(!EdgeType_release.isMyType[edge_cur_edge_rel.type.TypeID]) continue;
                            if(edge_cur_edge_rel.mappedTo != 0) goto cont_edge_cur_edge_rel_9;
                            // ImplicitSource(edge_rel -> node_r:Resource)
                            LGSPNode node_cur_node_r = edge_cur_edge_rel.source;
                            if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge_rel_8;
                            if(node_cur_node_r.mappedTo != 0) goto cont_node_cur_node_r_11;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_p2;
                            match.nodes[1] = node_cur_node_r;
                            match.nodes[2] = node_cur_node_p1;
                            match.edges[0] = edge_cur_edge_rel;
                            match.edges[1] = edge_cur_edge_n;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_p1.mappedTo = 0;
                                node_cur_node_p2.mappedTo = 0;
                                edge_cur_edge_n.mappedTo = 0;
                                graph.MoveHeadAfter(edge_cur_edge_n);
                                node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_rel);
                                return matches;
                            }
cont_node_cur_node_r_11:;
contunmap_edge_cur_edge_rel_8:;
cont_edge_cur_edge_rel_9:;
                            // Tail ExtendIncoming(edge_cur_edge_rel)
                        }
                        while((edge_cur_edge_rel = edge_cur_edge_rel.inNext) != edge_head_edge_rel);
                    }
                    node_cur_node_p1.mappedTo = 0;
cont_node_cur_node_p1_7:;
contunmap_node_cur_node_p2_4:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_n_2:;
                    edge_cur_edge_n.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_n)
                }
            }
            return matches;
        }
    }
    public class Action_requestRule : LGSPAction
    {
        private static Action_requestRule instance = new Action_requestRule();

        public Action_requestRule() { rulePattern = Rule_requestRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 0); matchesList = matches.matches;}

        public override string Name { get { return "requestRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(node_r:Resource)
            foreach(NodeType node_type_node_r in NodeType_Resource.typeVar.SubOrSameTypes)
            {
                for(LGSPNode node_head_node_r = graph.nodesByTypeHeads[node_type_node_r.TypeID], node_cur_node_r = node_head_node_r.typeNext; node_cur_node_r != node_head_node_r; node_cur_node_r = node_cur_node_r.typeNext)
                {
                    node_cur_node_r.mappedTo = 1;
                    // Lookup(node_p:Process)
                    foreach(NodeType node_type_node_p in NodeType_Process.typeVar.SubOrSameTypes)
                    {
                        for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[node_type_node_p.TypeID], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
                        {
                            if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_17;
                            // NegativePattern
                            node_cur_node_r.negMappedTo = 1;
                            if(node_cur_node_p.negMappedTo != 0) goto cont_node_cur_node_p_23;
                            // ExtendOutgoing(node_r -> neg_0_edge_hb:held_by)
                            LGSPEdge edge_head_neg_0_edge_hb = node_cur_node_r.outhead;
                            if(edge_head_neg_0_edge_hb != null)
                            {
                                LGSPEdge edge_cur_neg_0_edge_hb = edge_head_neg_0_edge_hb;
                                do
                                {
                                    if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge_hb.type.TypeID]) continue;
                                    if(edge_cur_neg_0_edge_hb.target != node_cur_node_p) continue;
                                    node_cur_node_r.negMappedTo = 0;
                                    goto contunmap_node_cur_node_p_16;
                                    // Tail ExtendOutgoing(edge_cur_neg_0_edge_hb)
                                }
                                while((edge_cur_neg_0_edge_hb = edge_cur_neg_0_edge_hb.outNext) != edge_head_neg_0_edge_hb);
                            }
cont_node_cur_node_p_23:;
                            node_cur_node_r.negMappedTo = 0;
                            // End of NegativePattern
                            // NegativePattern
                            node_cur_node_p.negMappedTo = 2;
                            // ExtendOutgoing(node_p -> neg_1_edge_req:request)
                            LGSPEdge edge_head_neg_1_edge_req = node_cur_node_p.outhead;
                            if(edge_head_neg_1_edge_req != null)
                            {
                                LGSPEdge edge_cur_neg_1_edge_req = edge_head_neg_1_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_neg_1_edge_req.type.TypeID]) continue;
                                    // ImplicitTarget(neg_1_edge_req -> neg_1_node_m:Resource)
                                    LGSPNode node_cur_neg_1_node_m = edge_cur_neg_1_edge_req.target;
                                    if(!NodeType_Resource.isMyType[node_cur_neg_1_node_m.type.TypeID]) goto contunmap_edge_cur_neg_1_edge_req_30;
                                    if(node_cur_neg_1_node_m.negMappedTo != 0) goto cont_node_cur_neg_1_node_m_33;
                                    node_cur_node_p.negMappedTo = 0;
                                    goto contunmap_node_cur_node_p_16;
cont_node_cur_neg_1_node_m_33:;
contunmap_edge_cur_neg_1_edge_req_30:;
                                    // Tail ExtendOutgoing(edge_cur_neg_1_edge_req)
                                }
                                while((edge_cur_neg_1_edge_req = edge_cur_neg_1_edge_req.outNext) != edge_head_neg_1_edge_req);
                            }
                            node_cur_node_p.negMappedTo = 0;
                            // End of NegativePattern
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_r;
                            match.nodes[1] = node_cur_node_p;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_r.mappedTo = 0;
                                graph.MoveHeadAfter(node_cur_node_r);
                                graph.MoveHeadAfter(node_cur_node_p);
                                return matches;
                            }
contunmap_node_cur_node_p_16:;
cont_node_cur_node_p_17:;
                            // Tail of Lookup(node_cur_node_p)
                        }
                    }
                    node_cur_node_r.mappedTo = 0;
                    // Tail of Lookup(node_cur_node_r)
                }
            }
            return matches;
        }
    }
    public class Action_unlockRule : LGSPAction
    {
        private static Action_unlockRule instance = new Action_unlockRule();

        public Action_unlockRule() { rulePattern = Rule_unlockRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2); matchesList = matches.matches;}

        public override string Name { get { return "unlockRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_b:blocked)
            foreach(EdgeType edge_type_edge_b in EdgeType_blocked.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_b = graph.edgesByTypeHeads[edge_type_edge_b.TypeID], edge_cur_edge_b = edge_head_edge_b.typeNext; edge_cur_edge_b != edge_head_edge_b; edge_cur_edge_b = edge_cur_edge_b.typeNext)
                {
                    edge_cur_edge_b.mappedTo = 1;
                    // ImplicitSource(edge_b -> node_r:Resource)
                    LGSPNode node_cur_node_r = edge_cur_edge_b.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge_b_36;
                    node_cur_node_r.mappedTo = 1;
                    // ImplicitTarget(edge_b -> node_p:Process)
                    LGSPNode node_cur_node_p = edge_cur_edge_b.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) goto contunmap_node_cur_node_r_38;
                    if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_41;
                    // ExtendOutgoing(node_r -> edge_hb:held_by)
                    LGSPEdge edge_head_edge_hb = node_cur_node_r.outhead;
                    if(edge_head_edge_hb != null)
                    {
                        LGSPEdge edge_cur_edge_hb = edge_head_edge_hb;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_edge_hb.type.TypeID]) continue;
                            if(edge_cur_edge_hb.target != node_cur_node_p) continue;
                            if(edge_cur_edge_hb.mappedTo != 0) goto cont_edge_cur_edge_hb_43;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_r;
                            match.nodes[1] = node_cur_node_p;
                            match.edges[0] = edge_cur_edge_b;
                            match.edges[1] = edge_cur_edge_hb;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_r.mappedTo = 0;
                                edge_cur_edge_b.mappedTo = 0;
                                graph.MoveHeadAfter(edge_cur_edge_b);
                                node_cur_node_r.MoveOutHeadAfter(edge_cur_edge_hb);
                                return matches;
                            }
cont_edge_cur_edge_hb_43:;
                            // Tail ExtendOutgoing(edge_cur_edge_hb)
                        }
                        while((edge_cur_edge_hb = edge_cur_edge_hb.outNext) != edge_head_edge_hb);
                    }
cont_node_cur_node_p_41:;
contunmap_node_cur_node_r_38:;
                    node_cur_node_r.mappedTo = 0;
contunmap_edge_cur_edge_b_36:;
                    edge_cur_edge_b.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_b)
                }
            }
            return matches;
        }
    }
    public class Action_takeRule : LGSPAction
    {
        private static Action_takeRule instance = new Action_takeRule();

        public Action_takeRule() { rulePattern = Rule_takeRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2); matchesList = matches.matches;}

        public override string Name { get { return "takeRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_req:request)
            foreach(EdgeType edge_type_edge_req in EdgeType_request.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_req = graph.edgesByTypeHeads[edge_type_edge_req.TypeID], edge_cur_edge_req = edge_head_edge_req.typeNext; edge_cur_edge_req != edge_head_edge_req; edge_cur_edge_req = edge_cur_edge_req.typeNext)
                {
                    edge_cur_edge_req.mappedTo = 2;
                    // ImplicitTarget(edge_req -> node_r:Resource)
                    LGSPNode node_cur_node_r = edge_cur_edge_req.target;
                    if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge_req_46;
                    node_cur_node_r.mappedTo = 1;
                    // ExtendOutgoing(node_r -> edge_t:token)
                    LGSPEdge edge_head_edge_t = node_cur_node_r.outhead;
                    if(edge_head_edge_t != null)
                    {
                        LGSPEdge edge_cur_edge_t = edge_head_edge_t;
                        do
                        {
                            if(!EdgeType_token.isMyType[edge_cur_edge_t.type.TypeID]) continue;
                            if(edge_cur_edge_t.mappedTo != 0) goto cont_edge_cur_edge_t_51;
                            // ImplicitTarget(edge_t -> node_p:Process)
                            LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                            if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) goto contunmap_edge_cur_edge_t_50;
                            if(edge_cur_edge_req.source != node_cur_node_p) goto contunmap_edge_cur_edge_t_50;
                            if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_53;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_r;
                            match.nodes[1] = node_cur_node_p;
                            match.edges[0] = edge_cur_edge_t;
                            match.edges[1] = edge_cur_edge_req;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_r.mappedTo = 0;
                                edge_cur_edge_req.mappedTo = 0;
                                graph.MoveHeadAfter(edge_cur_edge_req);
                                node_cur_node_r.MoveOutHeadAfter(edge_cur_edge_t);
                                return matches;
                            }
cont_node_cur_node_p_53:;
contunmap_edge_cur_edge_t_50:;
cont_edge_cur_edge_t_51:;
                            // Tail ExtendOutgoing(edge_cur_edge_t)
                        }
                        while((edge_cur_edge_t = edge_cur_edge_t.outNext) != edge_head_edge_t);
                    }
                    node_cur_node_r.mappedTo = 0;
contunmap_edge_cur_edge_req_46:;
                    edge_cur_edge_req.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_req)
                }
            }
            return matches;
        }
    }
    public class Action_releaseRule : LGSPAction
    {
        private static Action_releaseRule instance = new Action_releaseRule();

        public Action_releaseRule() { rulePattern = Rule_releaseRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1); matchesList = matches.matches;}

        public override string Name { get { return "releaseRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_hb:held_by)
            foreach(EdgeType edge_type_edge_hb in EdgeType_held_by.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[edge_type_edge_hb.TypeID], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
                {
                    // ImplicitSource(edge_hb -> node_r:Resource)
                    LGSPNode node_cur_node_r = edge_cur_edge_hb.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge_hb_56;
                    node_cur_node_r.mappedTo = 1;
                    // ImplicitTarget(edge_hb -> node_p:Process)
                    LGSPNode node_cur_node_p = edge_cur_edge_hb.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) goto contunmap_node_cur_node_r_58;
                    if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_61;
                    // NegativePattern
                    node_cur_node_p.negMappedTo = 2;
                    // ExtendOutgoing(node_p -> neg_0_edge_req:request)
                    LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p.outhead;
                    if(edge_head_neg_0_edge_req != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) continue;
                            // ImplicitTarget(neg_0_edge_req -> neg_0_node_m:Resource)
                            LGSPNode node_cur_neg_0_node_m = edge_cur_neg_0_edge_req.target;
                            if(!NodeType_Resource.isMyType[node_cur_neg_0_node_m.type.TypeID]) goto contunmap_edge_cur_neg_0_edge_req_66;
                            if(node_cur_neg_0_node_m.negMappedTo != 0) goto cont_node_cur_neg_0_node_m_69;
                            node_cur_node_p.negMappedTo = 0;
                            goto contunmap_node_cur_node_p_60;
cont_node_cur_neg_0_node_m_69:;
contunmap_edge_cur_neg_0_edge_req_66:;
                            // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                        }
                        while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                    }
                    node_cur_node_p.negMappedTo = 0;
                    // End of NegativePattern
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_r;
                    match.nodes[1] = node_cur_node_p;
                    match.edges[0] = edge_cur_edge_hb;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_r.mappedTo = 0;
                        graph.MoveHeadAfter(edge_cur_edge_hb);
                        return matches;
                    }
contunmap_node_cur_node_p_60:;
cont_node_cur_node_p_61:;
contunmap_node_cur_node_r_58:;
                    node_cur_node_r.mappedTo = 0;
contunmap_edge_cur_edge_hb_56:;
                    // Tail of Lookup(edge_cur_edge_hb)
                }
            }
            return matches;
        }
    }
    public class Action_waitingRule : LGSPAction
    {
        private static Action_waitingRule instance = new Action_waitingRule();

        public Action_waitingRule() { rulePattern = Rule_waitingRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 5, 3); matchesList = matches.matches;}

        public override string Name { get { return "waitingRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(node_r:Resource)
            foreach(NodeType node_type_node_r in NodeType_Resource.typeVar.SubOrSameTypes)
            {
                for(LGSPNode node_head_node_r = graph.nodesByTypeHeads[node_type_node_r.TypeID], node_cur_node_r = node_head_node_r.typeNext; node_cur_node_r != node_head_node_r; node_cur_node_r = node_cur_node_r.typeNext)
                {
                    node_cur_node_r.mappedTo = 3;
                    // Lookup(edge_hb:held_by)
                    foreach(EdgeType edge_type_edge_hb in EdgeType_held_by.typeVar.SubOrSameTypes)
                    {
                        for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[edge_type_edge_hb.TypeID], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
                        {
                            edge_cur_edge_hb.mappedTo = 2;
                            // ImplicitTarget(edge_hb -> node_p1:Process)
                            LGSPNode node_cur_node_p1 = edge_cur_edge_hb.target;
                            if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_edge_cur_edge_hb_74;
                            if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_77;
                            node_cur_node_p1.mappedTo = 4;
                            // ImplicitSource(edge_hb -> node_r1:Resource)
                            LGSPNode node_cur_node_r1 = edge_cur_edge_hb.source;
                            if(!NodeType_Resource.isMyType[node_cur_node_r1.type.TypeID]) goto contunmap_node_cur_node_p1_76;
                            if(node_cur_node_r1.mappedTo != 0) goto cont_node_cur_node_r1_79;
                            node_cur_node_r1.mappedTo = 5;
                            // ExtendIncoming(node_p1 -> edge_b:blocked)
                            LGSPEdge edge_head_edge_b = node_cur_node_p1.inhead;
                            if(edge_head_edge_b != null)
                            {
                                LGSPEdge edge_cur_edge_b = edge_head_edge_b;
                                do
                                {
                                    if(!EdgeType_blocked.isMyType[edge_cur_edge_b.type.TypeID]) continue;
                                    if(edge_cur_edge_b.mappedTo != 0) goto cont_edge_cur_edge_b_81;
                                    edge_cur_edge_b.mappedTo = 1;
                                    // ImplicitSource(edge_b -> node_r2:Resource)
                                    LGSPNode node_cur_node_r2 = edge_cur_edge_b.source;
                                    if(!NodeType_Resource.isMyType[node_cur_node_r2.type.TypeID]) goto contunmap_edge_cur_edge_b_80;
                                    if(node_cur_node_r2.mappedTo != 0) goto cont_node_cur_node_r2_83;
                                    node_cur_node_r2.mappedTo = 2;
                                    // ExtendIncoming(node_r1 -> edge_req:request)
                                    LGSPEdge edge_head_edge_req = node_cur_node_r1.inhead;
                                    if(edge_head_edge_req != null)
                                    {
                                        LGSPEdge edge_cur_edge_req = edge_head_edge_req;
                                        do
                                        {
                                            if(!EdgeType_request.isMyType[edge_cur_edge_req.type.TypeID]) continue;
                                            if(edge_cur_edge_req.mappedTo != 0) goto cont_edge_cur_edge_req_85;
                                            // ImplicitSource(edge_req -> node_p2:Process)
                                            LGSPNode node_cur_node_p2 = edge_cur_edge_req.source;
                                            if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_req_84;
                                            if(node_cur_node_p2.mappedTo != 0) goto cont_node_cur_node_p2_87;
                                            LGSPMatch match = matchesList.GetNewMatch();
                                            match.nodes[0] = node_cur_node_p2;
                                            match.nodes[1] = node_cur_node_r2;
                                            match.nodes[2] = node_cur_node_r;
                                            match.nodes[3] = node_cur_node_p1;
                                            match.nodes[4] = node_cur_node_r1;
                                            match.edges[0] = edge_cur_edge_b;
                                            match.edges[1] = edge_cur_edge_hb;
                                            match.edges[2] = edge_cur_edge_req;
                                            matchesList.CommitMatch();
                                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                            {
                                                node_cur_node_r2.mappedTo = 0;
                                                edge_cur_edge_b.mappedTo = 0;
                                                node_cur_node_r1.mappedTo = 0;
                                                node_cur_node_p1.mappedTo = 0;
                                                edge_cur_edge_hb.mappedTo = 0;
                                                node_cur_node_r.mappedTo = 0;
                                                graph.MoveHeadAfter(node_cur_node_r);
                                                graph.MoveHeadAfter(edge_cur_edge_hb);
                                                node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_b);
                                                node_cur_node_r1.MoveInHeadAfter(edge_cur_edge_req);
                                                return matches;
                                            }
cont_node_cur_node_p2_87:;
contunmap_edge_cur_edge_req_84:;
cont_edge_cur_edge_req_85:;
                                            // Tail ExtendIncoming(edge_cur_edge_req)
                                        }
                                        while((edge_cur_edge_req = edge_cur_edge_req.inNext) != edge_head_edge_req);
                                    }
                                    node_cur_node_r2.mappedTo = 0;
cont_node_cur_node_r2_83:;
contunmap_edge_cur_edge_b_80:;
                                    edge_cur_edge_b.mappedTo = 0;
cont_edge_cur_edge_b_81:;
                                    // Tail ExtendIncoming(edge_cur_edge_b)
                                }
                                while((edge_cur_edge_b = edge_cur_edge_b.inNext) != edge_head_edge_b);
                            }
                            node_cur_node_r1.mappedTo = 0;
cont_node_cur_node_r1_79:;
contunmap_node_cur_node_p1_76:;
                            node_cur_node_p1.mappedTo = 0;
cont_node_cur_node_p1_77:;
contunmap_edge_cur_edge_hb_74:;
                            edge_cur_edge_hb.mappedTo = 0;
                            // Tail of Lookup(edge_cur_edge_hb)
                        }
                    }
                    node_cur_node_r.mappedTo = 0;
                    // Tail of Lookup(node_cur_node_r)
                }
            }
            return matches;
        }
    }
    public class Action_unmountRule : LGSPAction
    {
        private static Action_unmountRule instance = new Action_unmountRule();

        public Action_unmountRule() { rulePattern = Rule_unmountRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1); matchesList = matches.matches;}

        public override string Name { get { return "unmountRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_t:token)
            foreach(EdgeType edge_type_edge_t in EdgeType_token.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_t = graph.edgesByTypeHeads[edge_type_edge_t.TypeID], edge_cur_edge_t = edge_head_edge_t.typeNext; edge_cur_edge_t != edge_head_edge_t; edge_cur_edge_t = edge_cur_edge_t.typeNext)
                {
                    // ImplicitSource(edge_t -> node_r:Resource)
                    LGSPNode node_cur_node_r = edge_cur_edge_t.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge_t_90;
                    node_cur_node_r.mappedTo = 1;
                    // ImplicitTarget(edge_t -> node_p:Process)
                    LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) goto contunmap_node_cur_node_r_92;
                    if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_95;
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_r;
                    match.nodes[1] = node_cur_node_p;
                    match.edges[0] = edge_cur_edge_t;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_r.mappedTo = 0;
                        graph.MoveHeadAfter(edge_cur_edge_t);
                        return matches;
                    }
cont_node_cur_node_p_95:;
contunmap_node_cur_node_r_92:;
                    node_cur_node_r.mappedTo = 0;
contunmap_edge_cur_edge_t_90:;
                    // Tail of Lookup(edge_cur_edge_t)
                }
            }
            return matches;
        }
    }
    public class Action_newRule : LGSPAction
    {
        private static Action_newRule instance = new Action_newRule();

        public Action_newRule() { rulePattern = Rule_newRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1); matchesList = matches.matches;}

        public override string Name { get { return "newRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_n:next)
            foreach(EdgeType edge_type_edge_n in EdgeType_next.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_edge_n.TypeID], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
                {
                    // ImplicitTarget(edge_n -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_n_98;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitSource(edge_n -> node_p1:Process)
                    LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_node_cur_node_p2_100;
                    if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_103;
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_p2;
                    match.nodes[1] = node_cur_node_p1;
                    match.edges[0] = edge_cur_edge_n;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_p2.mappedTo = 0;
                        graph.MoveHeadAfter(edge_cur_edge_n);
                        return matches;
                    }
cont_node_cur_node_p1_103:;
contunmap_node_cur_node_p2_100:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_n_98:;
                    // Tail of Lookup(edge_cur_edge_n)
                }
            }
            return matches;
        }
    }
    public class Action_requestStarRule : LGSPAction
    {
        private static Action_requestStarRule instance = new Action_requestStarRule();

        public Action_requestStarRule() { rulePattern = Rule_requestStarRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 4, 3); matchesList = matches.matches;}

        public override string Name { get { return "requestStarRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_n:next)
            foreach(EdgeType edge_type_edge_n in EdgeType_next.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_edge_n.TypeID], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
                {
                    edge_cur_edge_n.mappedTo = 2;
                    // ImplicitSource(edge_n -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge_n.source;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_n_106;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitTarget(edge_n -> node_p1:Process)
                    LGSPNode node_cur_node_p1 = edge_cur_edge_n.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_node_cur_node_p2_108;
                    if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_111;
                    node_cur_node_p1.mappedTo = 3;
                    // ExtendIncoming(node_p2 -> edge_h2:held_by)
                    LGSPEdge edge_head_edge_h2 = node_cur_node_p2.inhead;
                    if(edge_head_edge_h2 != null)
                    {
                        LGSPEdge edge_cur_edge_h2 = edge_head_edge_h2;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_edge_h2.type.TypeID]) continue;
                            if(edge_cur_edge_h2.mappedTo != 0) goto cont_edge_cur_edge_h2_113;
                            edge_cur_edge_h2.mappedTo = 1;
                            // ImplicitSource(edge_h2 -> node_r2:Resource)
                            LGSPNode node_cur_node_r2 = edge_cur_edge_h2.source;
                            if(!NodeType_Resource.isMyType[node_cur_node_r2.type.TypeID]) goto contunmap_edge_cur_edge_h2_112;
                            if(node_cur_node_r2.mappedTo != 0) goto cont_node_cur_node_r2_115;
                            node_cur_node_r2.mappedTo = 2;
                            // NegativePattern
                            node_cur_node_r2.negMappedTo = 1;
                            if(node_cur_node_p1.negMappedTo != 0) goto cont_node_cur_node_p1_121;
                            // ExtendOutgoing(node_p1 -> neg_0_edge_req:request)
                            LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p1.outhead;
                            if(edge_head_neg_0_edge_req != null)
                            {
                                LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) continue;
                                    if(edge_cur_neg_0_edge_req.target != node_cur_node_r2) continue;
                                    node_cur_node_r2.negMappedTo = 0;
                                    goto contunmap_node_cur_node_r2_114;
                                    // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                                }
                                while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                            }
cont_node_cur_node_p1_121:;
                            node_cur_node_r2.negMappedTo = 0;
                            // End of NegativePattern
                            // ExtendIncoming(node_p1 -> edge_h1:held_by)
                            LGSPEdge edge_head_edge_h1 = node_cur_node_p1.inhead;
                            if(edge_head_edge_h1 != null)
                            {
                                LGSPEdge edge_cur_edge_h1 = edge_head_edge_h1;
                                do
                                {
                                    if(!EdgeType_held_by.isMyType[edge_cur_edge_h1.type.TypeID]) continue;
                                    if(edge_cur_edge_h1.mappedTo != 0) goto cont_edge_cur_edge_h1_125;
                                    // ImplicitSource(edge_h1 -> node_r1:Resource)
                                    LGSPNode node_cur_node_r1 = edge_cur_edge_h1.source;
                                    if(!NodeType_Resource.isMyType[node_cur_node_r1.type.TypeID]) goto contunmap_edge_cur_edge_h1_124;
                                    if(node_cur_node_r1.mappedTo != 0) goto cont_node_cur_node_r1_127;
                                    LGSPMatch match = matchesList.GetNewMatch();
                                    match.nodes[0] = node_cur_node_p2;
                                    match.nodes[1] = node_cur_node_r2;
                                    match.nodes[2] = node_cur_node_p1;
                                    match.nodes[3] = node_cur_node_r1;
                                    match.edges[0] = edge_cur_edge_h2;
                                    match.edges[1] = edge_cur_edge_n;
                                    match.edges[2] = edge_cur_edge_h1;
                                    matchesList.CommitMatch();
                                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                    {
                                        node_cur_node_r2.mappedTo = 0;
                                        edge_cur_edge_h2.mappedTo = 0;
                                        node_cur_node_p1.mappedTo = 0;
                                        node_cur_node_p2.mappedTo = 0;
                                        edge_cur_edge_n.mappedTo = 0;
                                        graph.MoveHeadAfter(edge_cur_edge_n);
                                        node_cur_node_p2.MoveInHeadAfter(edge_cur_edge_h2);
                                        node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_h1);
                                        return matches;
                                    }
cont_node_cur_node_r1_127:;
contunmap_edge_cur_edge_h1_124:;
cont_edge_cur_edge_h1_125:;
                                    // Tail ExtendIncoming(edge_cur_edge_h1)
                                }
                                while((edge_cur_edge_h1 = edge_cur_edge_h1.inNext) != edge_head_edge_h1);
                            }
contunmap_node_cur_node_r2_114:;
                            node_cur_node_r2.mappedTo = 0;
cont_node_cur_node_r2_115:;
contunmap_edge_cur_edge_h2_112:;
                            edge_cur_edge_h2.mappedTo = 0;
cont_edge_cur_edge_h2_113:;
                            // Tail ExtendIncoming(edge_cur_edge_h2)
                        }
                        while((edge_cur_edge_h2 = edge_cur_edge_h2.inNext) != edge_head_edge_h2);
                    }
                    node_cur_node_p1.mappedTo = 0;
cont_node_cur_node_p1_111:;
contunmap_node_cur_node_p2_108:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_n_106:;
                    edge_cur_edge_n.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_n)
                }
            }
            return matches;
        }
    }
    public class Action_passRule : LGSPAction
    {
        private static Action_passRule instance = new Action_passRule();

        public Action_passRule() { rulePattern = Rule_passRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2); matchesList = matches.matches;}

        public override string Name { get { return "passRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_n:next)
            foreach(EdgeType edge_type_edge_n in EdgeType_next.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_edge_n.TypeID], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
                {
                    edge_cur_edge_n.mappedTo = 2;
                    // ImplicitTarget(edge_n -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_n_130;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitSource(edge_n -> node_p1:Process)
                    LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_node_cur_node_p2_132;
                    if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_135;
                    node_cur_node_p1.mappedTo = 3;
                    // ExtendIncoming(node_p1 -> edge__edge0:token)
                    LGSPEdge edge_head_edge__edge0 = node_cur_node_p1.inhead;
                    if(edge_head_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                        do
                        {
                            if(!EdgeType_token.isMyType[edge_cur_edge__edge0.type.TypeID]) continue;
                            if(edge_cur_edge__edge0.mappedTo != 0) goto cont_edge_cur_edge__edge0_137;
                            // ImplicitSource(edge__edge0 -> node_r:Resource)
                            LGSPNode node_cur_node_r = edge_cur_edge__edge0.source;
                            if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge__edge0_136;
                            if(node_cur_node_r.mappedTo != 0) goto cont_node_cur_node_r_139;
                            // NegativePattern
                            node_cur_node_r.negMappedTo = 1;
                            if(node_cur_node_p1.negMappedTo != 0) goto cont_node_cur_node_p1_145;
                            // ExtendOutgoing(node_p1 -> neg_0_edge_req:request)
                            LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p1.outhead;
                            if(edge_head_neg_0_edge_req != null)
                            {
                                LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) continue;
                                    if(edge_cur_neg_0_edge_req.target != node_cur_node_r) continue;
                                    node_cur_node_r.negMappedTo = 0;
                                    goto contunmap_node_cur_node_r_138;
                                    // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                                }
                                while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                            }
cont_node_cur_node_p1_145:;
                            node_cur_node_r.negMappedTo = 0;
                            // End of NegativePattern
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_p2;
                            match.nodes[1] = node_cur_node_r;
                            match.nodes[2] = node_cur_node_p1;
                            match.edges[0] = edge_cur_edge__edge0;
                            match.edges[1] = edge_cur_edge_n;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_p1.mappedTo = 0;
                                node_cur_node_p2.mappedTo = 0;
                                edge_cur_edge_n.mappedTo = 0;
                                graph.MoveHeadAfter(edge_cur_edge_n);
                                node_cur_node_p1.MoveInHeadAfter(edge_cur_edge__edge0);
                                return matches;
                            }
contunmap_node_cur_node_r_138:;
cont_node_cur_node_r_139:;
contunmap_edge_cur_edge__edge0_136:;
cont_edge_cur_edge__edge0_137:;
                            // Tail ExtendIncoming(edge_cur_edge__edge0)
                        }
                        while((edge_cur_edge__edge0 = edge_cur_edge__edge0.inNext) != edge_head_edge__edge0);
                    }
                    node_cur_node_p1.mappedTo = 0;
cont_node_cur_node_p1_135:;
contunmap_node_cur_node_p2_132:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_n_130:;
                    edge_cur_edge_n.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_n)
                }
            }
            return matches;
        }
    }
    public class Action_blockedRule : LGSPAction
    {
        private static Action_blockedRule instance = new Action_blockedRule();

        public Action_blockedRule() { rulePattern = Rule_blockedRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2); matchesList = matches.matches;}

        public override string Name { get { return "blockedRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_hb:held_by)
            foreach(EdgeType edge_type_edge_hb in EdgeType_held_by.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[edge_type_edge_hb.TypeID], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
                {
                    edge_cur_edge_hb.mappedTo = 1;
                    // ImplicitTarget(edge_hb -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge_hb.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_hb_150;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitSource(edge_hb -> node_r:Resource)
                    LGSPNode node_cur_node_r = edge_cur_edge_hb.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_node_cur_node_p2_152;
                    if(node_cur_node_r.mappedTo != 0) goto cont_node_cur_node_r_155;
                    node_cur_node_r.mappedTo = 2;
                    // ExtendIncoming(node_r -> edge_req:request)
                    LGSPEdge edge_head_edge_req = node_cur_node_r.inhead;
                    if(edge_head_edge_req != null)
                    {
                        LGSPEdge edge_cur_edge_req = edge_head_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_edge_req.type.TypeID]) continue;
                            if(edge_cur_edge_req.mappedTo != 0) goto cont_edge_cur_edge_req_157;
                            // ImplicitSource(edge_req -> node_p1:Process)
                            LGSPNode node_cur_node_p1 = edge_cur_edge_req.source;
                            if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_edge_cur_edge_req_156;
                            if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_159;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_p2;
                            match.nodes[1] = node_cur_node_r;
                            match.nodes[2] = node_cur_node_p1;
                            match.edges[0] = edge_cur_edge_hb;
                            match.edges[1] = edge_cur_edge_req;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_r.mappedTo = 0;
                                node_cur_node_p2.mappedTo = 0;
                                edge_cur_edge_hb.mappedTo = 0;
                                graph.MoveHeadAfter(edge_cur_edge_hb);
                                node_cur_node_r.MoveInHeadAfter(edge_cur_edge_req);
                                return matches;
                            }
cont_node_cur_node_p1_159:;
contunmap_edge_cur_edge_req_156:;
cont_edge_cur_edge_req_157:;
                            // Tail ExtendIncoming(edge_cur_edge_req)
                        }
                        while((edge_cur_edge_req = edge_cur_edge_req.inNext) != edge_head_edge_req);
                    }
                    node_cur_node_r.mappedTo = 0;
cont_node_cur_node_r_155:;
contunmap_node_cur_node_p2_152:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_hb_150:;
                    edge_cur_edge_hb.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_hb)
                }
            }
            return matches;
        }
    }
    public class Action_requestSimpleRule : LGSPAction
    {
        private static Action_requestSimpleRule instance = new Action_requestSimpleRule();

        public Action_requestSimpleRule() { rulePattern = Rule_requestSimpleRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1); matchesList = matches.matches;}

        public override string Name { get { return "requestSimpleRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_t:token)
            foreach(EdgeType edge_type_edge_t in EdgeType_token.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_t = graph.edgesByTypeHeads[edge_type_edge_t.TypeID], edge_cur_edge_t = edge_head_edge_t.typeNext; edge_cur_edge_t != edge_head_edge_t; edge_cur_edge_t = edge_cur_edge_t.typeNext)
                {
                    // ImplicitSource(edge_t -> node_r:Resource)
                    LGSPNode node_cur_node_r = edge_cur_edge_t.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge_t_162;
                    node_cur_node_r.mappedTo = 1;
                    // ImplicitTarget(edge_t -> node_p:Process)
                    LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) goto contunmap_node_cur_node_r_164;
                    if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_167;
                    // NegativePattern
                    node_cur_node_r.negMappedTo = 1;
                    if(node_cur_node_p.negMappedTo != 0) goto cont_node_cur_node_p_173;
                    // ExtendOutgoing(node_p -> neg_0_edge_req:request)
                    LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p.outhead;
                    if(edge_head_neg_0_edge_req != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) continue;
                            if(edge_cur_neg_0_edge_req.target != node_cur_node_r) continue;
                            node_cur_node_r.negMappedTo = 0;
                            goto contunmap_node_cur_node_p_166;
                            // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                        }
                        while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                    }
cont_node_cur_node_p_173:;
                    node_cur_node_r.negMappedTo = 0;
                    // End of NegativePattern
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_r;
                    match.nodes[1] = node_cur_node_p;
                    match.edges[0] = edge_cur_edge_t;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_r.mappedTo = 0;
                        graph.MoveHeadAfter(edge_cur_edge_t);
                        return matches;
                    }
contunmap_node_cur_node_p_166:;
cont_node_cur_node_p_167:;
contunmap_node_cur_node_r_164:;
                    node_cur_node_r.mappedTo = 0;
contunmap_edge_cur_edge_t_162:;
                    // Tail of Lookup(edge_cur_edge_t)
                }
            }
            return matches;
        }
    }
    public class Action_killRule : LGSPAction
    {
        private static Action_killRule instance = new Action_killRule();

        public Action_killRule() { rulePattern = Rule_killRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2); matchesList = matches.matches;}

        public override string Name { get { return "killRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_n2:next)
            foreach(EdgeType edge_type_edge_n2 in EdgeType_next.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_n2 = graph.edgesByTypeHeads[edge_type_edge_n2.TypeID], edge_cur_edge_n2 = edge_head_edge_n2.typeNext; edge_cur_edge_n2 != edge_head_edge_n2; edge_cur_edge_n2 = edge_cur_edge_n2.typeNext)
                {
                    edge_cur_edge_n2.mappedTo = 1;
                    // ImplicitTarget(edge_n2 -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge_n2.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_n2_178;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitSource(edge_n2 -> node_p:Process)
                    LGSPNode node_cur_node_p = edge_cur_edge_n2.source;
                    if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) goto contunmap_node_cur_node_p2_180;
                    if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_183;
                    node_cur_node_p.mappedTo = 3;
                    // ExtendIncoming(node_p -> edge_n1:next)
                    LGSPEdge edge_head_edge_n1 = node_cur_node_p.inhead;
                    if(edge_head_edge_n1 != null)
                    {
                        LGSPEdge edge_cur_edge_n1 = edge_head_edge_n1;
                        do
                        {
                            if(!EdgeType_next.isMyType[edge_cur_edge_n1.type.TypeID]) continue;
                            if(edge_cur_edge_n1.mappedTo != 0) goto cont_edge_cur_edge_n1_185;
                            // ImplicitSource(edge_n1 -> node_p1:Process)
                            LGSPNode node_cur_node_p1 = edge_cur_edge_n1.source;
                            if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_edge_cur_edge_n1_184;
                            if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_187;
                            LGSPMatch match = matchesList.GetNewMatch();
                            match.nodes[0] = node_cur_node_p2;
                            match.nodes[1] = node_cur_node_p1;
                            match.nodes[2] = node_cur_node_p;
                            match.edges[0] = edge_cur_edge_n2;
                            match.edges[1] = edge_cur_edge_n1;
                            matchesList.CommitMatch();
                            if(maxMatches > 0 && matchesList.Count >= maxMatches)
                            {
                                node_cur_node_p.mappedTo = 0;
                                node_cur_node_p2.mappedTo = 0;
                                edge_cur_edge_n2.mappedTo = 0;
                                graph.MoveHeadAfter(edge_cur_edge_n2);
                                node_cur_node_p.MoveInHeadAfter(edge_cur_edge_n1);
                                return matches;
                            }
cont_node_cur_node_p1_187:;
contunmap_edge_cur_edge_n1_184:;
cont_edge_cur_edge_n1_185:;
                            // Tail ExtendIncoming(edge_cur_edge_n1)
                        }
                        while((edge_cur_edge_n1 = edge_cur_edge_n1.inNext) != edge_head_edge_n1);
                    }
                    node_cur_node_p.mappedTo = 0;
cont_node_cur_node_p_183:;
contunmap_node_cur_node_p2_180:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_n2_178:;
                    edge_cur_edge_n2.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_n2)
                }
            }
            return matches;
        }
    }
    public class Action_releaseStarRule : LGSPAction
    {
        private static Action_releaseStarRule instance = new Action_releaseStarRule();

        public Action_releaseStarRule() { rulePattern = Rule_releaseStarRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 4, 3); matchesList = matches.matches;}

        public override string Name { get { return "releaseStarRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_h2:held_by)
            foreach(EdgeType edge_type_edge_h2 in EdgeType_held_by.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_h2 = graph.edgesByTypeHeads[edge_type_edge_h2.TypeID], edge_cur_edge_h2 = edge_head_edge_h2.typeNext; edge_cur_edge_h2 != edge_head_edge_h2; edge_cur_edge_h2 = edge_cur_edge_h2.typeNext)
                {
                    edge_cur_edge_h2.mappedTo = 2;
                    // ImplicitTarget(edge_h2 -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge_h2.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge_h2_190;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitSource(edge_h2 -> node_r2:Resource)
                    LGSPNode node_cur_node_r2 = edge_cur_edge_h2.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r2.type.TypeID]) goto contunmap_node_cur_node_p2_192;
                    if(node_cur_node_r2.mappedTo != 0) goto cont_node_cur_node_r2_195;
                    node_cur_node_r2.mappedTo = 2;
                    // ExtendIncoming(node_p2 -> edge_h1:held_by)
                    LGSPEdge edge_head_edge_h1 = node_cur_node_p2.inhead;
                    if(edge_head_edge_h1 != null)
                    {
                        LGSPEdge edge_cur_edge_h1 = edge_head_edge_h1;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_edge_h1.type.TypeID]) continue;
                            if(edge_cur_edge_h1.mappedTo != 0) goto cont_edge_cur_edge_h1_197;
                            edge_cur_edge_h1.mappedTo = 3;
                            // ImplicitSource(edge_h1 -> node_r1:Resource)
                            LGSPNode node_cur_node_r1 = edge_cur_edge_h1.source;
                            if(!NodeType_Resource.isMyType[node_cur_node_r1.type.TypeID]) goto contunmap_edge_cur_edge_h1_196;
                            if(node_cur_node_r1.mappedTo != 0) goto cont_node_cur_node_r1_199;
                            node_cur_node_r1.mappedTo = 4;
                            // ExtendIncoming(node_r1 -> edge_rq:request)
                            LGSPEdge edge_head_edge_rq = node_cur_node_r1.inhead;
                            if(edge_head_edge_rq != null)
                            {
                                LGSPEdge edge_cur_edge_rq = edge_head_edge_rq;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_edge_rq.type.TypeID]) continue;
                                    if(edge_cur_edge_rq.mappedTo != 0) goto cont_edge_cur_edge_rq_201;
                                    // ImplicitSource(edge_rq -> node_p1:Process)
                                    LGSPNode node_cur_node_p1 = edge_cur_edge_rq.source;
                                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_edge_cur_edge_rq_200;
                                    if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_203;
                                    LGSPMatch match = matchesList.GetNewMatch();
                                    match.nodes[0] = node_cur_node_p2;
                                    match.nodes[1] = node_cur_node_r2;
                                    match.nodes[2] = node_cur_node_p1;
                                    match.nodes[3] = node_cur_node_r1;
                                    match.edges[0] = edge_cur_edge_rq;
                                    match.edges[1] = edge_cur_edge_h2;
                                    match.edges[2] = edge_cur_edge_h1;
                                    matchesList.CommitMatch();
                                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                    {
                                        node_cur_node_r1.mappedTo = 0;
                                        edge_cur_edge_h1.mappedTo = 0;
                                        node_cur_node_r2.mappedTo = 0;
                                        node_cur_node_p2.mappedTo = 0;
                                        edge_cur_edge_h2.mappedTo = 0;
                                        graph.MoveHeadAfter(edge_cur_edge_h2);
                                        node_cur_node_p2.MoveInHeadAfter(edge_cur_edge_h1);
                                        node_cur_node_r1.MoveInHeadAfter(edge_cur_edge_rq);
                                        return matches;
                                    }
cont_node_cur_node_p1_203:;
contunmap_edge_cur_edge_rq_200:;
cont_edge_cur_edge_rq_201:;
                                    // Tail ExtendIncoming(edge_cur_edge_rq)
                                }
                                while((edge_cur_edge_rq = edge_cur_edge_rq.inNext) != edge_head_edge_rq);
                            }
                            node_cur_node_r1.mappedTo = 0;
cont_node_cur_node_r1_199:;
contunmap_edge_cur_edge_h1_196:;
                            edge_cur_edge_h1.mappedTo = 0;
cont_edge_cur_edge_h1_197:;
                            // Tail ExtendIncoming(edge_cur_edge_h1)
                        }
                        while((edge_cur_edge_h1 = edge_cur_edge_h1.inNext) != edge_head_edge_h1);
                    }
                    node_cur_node_r2.mappedTo = 0;
cont_node_cur_node_r2_195:;
contunmap_node_cur_node_p2_192:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_h2_190:;
                    edge_cur_edge_h2.mappedTo = 0;
                    // Tail of Lookup(edge_cur_edge_h2)
                }
            }
            return matches;
        }
    }
    public class Action_mountRule : LGSPAction
    {
        private static Action_mountRule instance = new Action_mountRule();

        public Action_mountRule() { rulePattern = Rule_mountRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0); matchesList = matches.matches;}

        public override string Name { get { return "mountRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(node_p:Process)
            foreach(NodeType node_type_node_p in NodeType_Process.typeVar.SubOrSameTypes)
            {
                for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[node_type_node_p.TypeID], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
                {
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_p;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(node_cur_node_p);
                        return matches;
                    }
                    // Tail of Lookup(node_cur_node_p)
                }
            }
            return matches;
        }
    }
    public class Action_aux_attachResource : LGSPAction
    {
        private static Action_aux_attachResource instance = new Action_aux_attachResource();

        public Action_aux_attachResource() { rulePattern = Rule_aux_attachResource.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0); matchesList = matches.matches;}

        public override string Name { get { return "aux_attachResource"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(node_p:Process)
            foreach(NodeType node_type_node_p in NodeType_Process.typeVar.SubOrSameTypes)
            {
                for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[node_type_node_p.TypeID], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
                {
                    // NegativePattern
                    node_cur_node_p.negMappedTo = 2;
                    // ExtendIncoming(node_p -> neg_0_edge__edge0:held_by)
                    LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_p.inhead;
                    if(edge_head_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) continue;
                            // ImplicitSource(neg_0_edge__edge0 -> neg_0_node_r:Resource)
                            LGSPNode node_cur_neg_0_node_r = edge_cur_neg_0_edge__edge0.source;
                            if(!NodeType_Resource.isMyType[node_cur_neg_0_node_r.type.TypeID]) goto contunmap_edge_cur_neg_0_edge__edge0_216;
                            if(node_cur_neg_0_node_r.negMappedTo != 0) goto cont_node_cur_neg_0_node_r_219;
                            node_cur_node_p.negMappedTo = 0;
                            goto contunmap_node_cur_node_p_210;
cont_node_cur_neg_0_node_r_219:;
contunmap_edge_cur_neg_0_edge__edge0_216:;
                            // Tail ExtendIncoming(edge_cur_neg_0_edge__edge0)
                        }
                        while((edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.inNext) != edge_head_neg_0_edge__edge0);
                    }
                    node_cur_node_p.negMappedTo = 0;
                    // End of NegativePattern
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_p;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(node_cur_node_p);
                        return matches;
                    }
contunmap_node_cur_node_p_210:;
                    // Tail of Lookup(node_cur_node_p)
                }
            }
            return matches;
        }
    }
    public class Action_ignoreRule : LGSPAction
    {
        private static Action_ignoreRule instance = new Action_ignoreRule();

        public Action_ignoreRule() { rulePattern = Rule_ignoreRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1); matchesList = matches.matches;}

        public override string Name { get { return "ignoreRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge_b:blocked)
            foreach(EdgeType edge_type_edge_b in EdgeType_blocked.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge_b = graph.edgesByTypeHeads[edge_type_edge_b.TypeID], edge_cur_edge_b = edge_head_edge_b.typeNext; edge_cur_edge_b != edge_head_edge_b; edge_cur_edge_b = edge_cur_edge_b.typeNext)
                {
                    // ImplicitSource(edge_b -> node_r:Resource)
                    LGSPNode node_cur_node_r = edge_cur_edge_b.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) goto contunmap_edge_cur_edge_b_222;
                    node_cur_node_r.mappedTo = 1;
                    // ImplicitTarget(edge_b -> node_p:Process)
                    LGSPNode node_cur_node_p = edge_cur_edge_b.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) goto contunmap_node_cur_node_r_224;
                    if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_227;
                    // NegativePattern
                    node_cur_node_p.negMappedTo = 2;
                    // ExtendIncoming(node_p -> neg_0_edge_hb:held_by)
                    LGSPEdge edge_head_neg_0_edge_hb = node_cur_node_p.inhead;
                    if(edge_head_neg_0_edge_hb != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge_hb = edge_head_neg_0_edge_hb;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge_hb.type.TypeID]) continue;
                            // ImplicitSource(neg_0_edge_hb -> neg_0_node_m:Resource)
                            LGSPNode node_cur_neg_0_node_m = edge_cur_neg_0_edge_hb.source;
                            if(!NodeType_Resource.isMyType[node_cur_neg_0_node_m.type.TypeID]) goto contunmap_edge_cur_neg_0_edge_hb_232;
                            if(node_cur_neg_0_node_m.negMappedTo != 0) goto cont_node_cur_neg_0_node_m_235;
                            node_cur_node_p.negMappedTo = 0;
                            goto contunmap_node_cur_node_p_226;
cont_node_cur_neg_0_node_m_235:;
contunmap_edge_cur_neg_0_edge_hb_232:;
                            // Tail ExtendIncoming(edge_cur_neg_0_edge_hb)
                        }
                        while((edge_cur_neg_0_edge_hb = edge_cur_neg_0_edge_hb.inNext) != edge_head_neg_0_edge_hb);
                    }
                    node_cur_node_p.negMappedTo = 0;
                    // End of NegativePattern
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_r;
                    match.nodes[1] = node_cur_node_p;
                    match.edges[0] = edge_cur_edge_b;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_r.mappedTo = 0;
                        graph.MoveHeadAfter(edge_cur_edge_b);
                        return matches;
                    }
contunmap_node_cur_node_p_226:;
cont_node_cur_node_p_227:;
contunmap_node_cur_node_r_224:;
                    node_cur_node_r.mappedTo = 0;
contunmap_edge_cur_edge_b_222:;
                    // Tail of Lookup(edge_cur_edge_b)
                }
            }
            return matches;
        }
    }

    public class MutexPimpedActions : LGSPActions
    {
        public MutexPimpedActions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, dumperfactory, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public MutexPimpedActions(LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("giveRule", (LGSPAction) Action_giveRule.Instance);
            actions.Add("requestRule", (LGSPAction) Action_requestRule.Instance);
            actions.Add("unlockRule", (LGSPAction) Action_unlockRule.Instance);
            actions.Add("takeRule", (LGSPAction) Action_takeRule.Instance);
            actions.Add("releaseRule", (LGSPAction) Action_releaseRule.Instance);
            actions.Add("waitingRule", (LGSPAction) Action_waitingRule.Instance);
            actions.Add("unmountRule", (LGSPAction) Action_unmountRule.Instance);
            actions.Add("newRule", (LGSPAction) Action_newRule.Instance);
            actions.Add("requestStarRule", (LGSPAction) Action_requestStarRule.Instance);
            actions.Add("passRule", (LGSPAction) Action_passRule.Instance);
            actions.Add("blockedRule", (LGSPAction) Action_blockedRule.Instance);
            actions.Add("requestSimpleRule", (LGSPAction) Action_requestSimpleRule.Instance);
            actions.Add("killRule", (LGSPAction) Action_killRule.Instance);
            actions.Add("releaseStarRule", (LGSPAction) Action_releaseStarRule.Instance);
            actions.Add("mountRule", (LGSPAction) Action_mountRule.Instance);
            actions.Add("aux_attachResource", (LGSPAction) Action_aux_attachResource.Instance);
            actions.Add("ignoreRule", (LGSPAction) Action_ignoreRule.Instance);
        }

        public override String Name { get { return "MutexPimpedActions"; } }
        public override String ModelMD5Hash { get { return "0ecfadd9bae7c1bee09ddc57f323923f"; } }
    }
}