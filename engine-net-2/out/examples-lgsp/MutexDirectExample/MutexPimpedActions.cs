using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.MutexPimped;

namespace de.unika.ipd.grGen.actions.MutexPimped
{
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
				new bool[] {},
				new bool[] {
					true, },
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_held_by edge__edge0 = Edge_held_by.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_held_by edge__edge0 = Edge_held_by.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "r" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_blockedRule : LGSPRulePattern
	{
		private static Rule_blockedRule instance = null;
		public static Rule_blockedRule Instance { get { if (instance==null) instance = new Rule_blockedRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge_hb_AllowedTypes = null;
		public static EdgeType[] edge_req_AllowedTypes = null;
		public static bool[] edge_hb_IsAllowedType = null;
		public static bool[] edge_req_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p2, @p1, };
		public enum EdgeNums { @hb = 1, @req, };

		private Rule_blockedRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r, node_p2, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p1, node_r, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p2, node_p1 }, 
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
					false, false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			Edge_blocked edge_b = Edge_blocked.CreateEdge(graph, node_r, node_p1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			Edge_blocked edge_b = Edge_blocked.CreateEdge(graph, node_r, node_p1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "b" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_blockedRule : LGSPStaticScheduleInfo
	{
		public Schedule_blockedRule()
		{
			ActionName = "blockedRule";
			this.RulePattern = Rule_blockedRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_giveRule : LGSPRulePattern
	{
		private static Rule_giveRule instance = null;
		public static Rule_giveRule Instance { get { if (instance==null) instance = new Rule_giveRule(); return instance; } }

		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r_AllowedTypes = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r_IsAllowedType = null;
		public static EdgeType[] edge_n_AllowedTypes = null;
		public static EdgeType[] edge_rel_AllowedTypes = null;
		public static bool[] edge_n_IsAllowedType = null;
		public static bool[] edge_rel_IsAllowedType = null;

		public enum NodeNums { @p1  = 1, @p2, @r, };
		public enum EdgeNums { @n = 1, @rel, };

		private Rule_giveRule()
		{
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rel = new PatternEdge(node_r, node_p1, (int) EdgeTypes.@release, "edge_rel", edge_rel_AllowedTypes, edge_rel_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p1, node_p2, node_r }, 
				new PatternEdge[] { edge_n, edge_rel }, 
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
					false, false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPEdge edge_rel = match.Edges[ (int) EdgeNums.@rel - 1 ];
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPEdge edge_rel = match.Edges[ (int) EdgeNums.@rel - 1 ];
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_giveRule : LGSPStaticScheduleInfo
	{
		public Schedule_giveRule()
		{
			ActionName = "giveRule";
			this.RulePattern = Rule_giveRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 1.0F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge_b = match.Edges[ (int) EdgeNums.@b - 1 ];
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge_b = match.Edges[ (int) EdgeNums.@b - 1 ];
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_killRule : LGSPRulePattern
	{
		private static Rule_killRule instance = null;
		public static Rule_killRule Instance { get { if (instance==null) instance = new Rule_killRule(); return instance; } }

		public static NodeType[] node_p_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_p_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge_n2_AllowedTypes = null;
		public static EdgeType[] edge_n1_AllowedTypes = null;
		public static bool[] edge_n2_IsAllowedType = null;
		public static bool[] edge_n1_IsAllowedType = null;

		public enum NodeNums { @p  = 1, @p2, @p1, };
		public enum EdgeNums { @n2 = 1, @n1, };

		private Rule_killRule()
		{
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n2 = new PatternEdge(node_p, node_p2, (int) EdgeTypes.@next, "edge_n2", edge_n2_AllowedTypes, edge_n2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n1 = new PatternEdge(node_p1, node_p, (int) EdgeTypes.@next, "edge_n1", edge_n1_AllowedTypes, edge_n1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p, node_p2, node_p1 }, 
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
					false, false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_n2 = match.Edges[ (int) EdgeNums.@n2 - 1 ];
			LGSPEdge edge_n1 = match.Edges[ (int) EdgeNums.@n1 - 1 ];
			Edge_next edge_n;
			if(edge_n2.type == EdgeType_next.typeVar)
			{
				// re-using edge_n2 as edge_n
				edge_n = (Edge_next) edge_n2;
				graph.ReuseEdge(edge_n2, node_p1, null);
			}
			else
				edge_n = Edge_next.CreateEdge(graph, node_p1, node_p2);
			graph.Remove(edge_n1);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_n2 = match.Edges[ (int) EdgeNums.@n2 - 1 ];
			LGSPEdge edge_n1 = match.Edges[ (int) EdgeNums.@n1 - 1 ];
			Edge_next edge_n = Edge_next.CreateEdge(graph, node_p1, node_p2);
			graph.Remove(edge_n2);
			graph.Remove(edge_n1);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "n" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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
				new bool[] {},
				new bool[] {
					true, },
				new bool[] {}
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "r" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_newRule : LGSPRulePattern
	{
		private static Rule_newRule instance = null;
		public static Rule_newRule Instance { get { if (instance==null) instance = new Rule_newRule(); return instance; } }

		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static EdgeType[] edge_n_AllowedTypes = null;
		public static bool[] edge_n_IsAllowedType = null;

		public enum NodeNums { @p1  = 1, @p2, };
		public enum EdgeNums { @n = 1, };

		private Rule_newRule()
		{
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p1, node_p2 }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_n = match.Edges[ (int) EdgeNums.@n - 1 ];
			Node_Process node_p = Node_Process.CreateNode(graph);
			Edge_next edge_n2;
			if(edge_n.type == EdgeType_next.typeVar)
			{
				// re-using edge_n as edge_n2
				edge_n2 = (Edge_next) edge_n;
				graph.ReuseEdge(edge_n, node_p, null);
			}
			else
				edge_n2 = Edge_next.CreateEdge(graph, node_p, node_p2);
			Edge_next edge_n1 = Edge_next.CreateEdge(graph, node_p1, node_p);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge_n = match.Edges[ (int) EdgeNums.@n - 1 ];
			Node_Process node_p = Node_Process.CreateNode(graph);
			Edge_next edge_n2 = Edge_next.CreateEdge(graph, node_p, node_p2);
			Edge_next edge_n1 = Edge_next.CreateEdge(graph, node_p1, node_p);
			graph.Remove(edge_n);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "p" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "n2", "n1" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_passRule : LGSPRulePattern
	{
		private static Rule_passRule instance = null;
		public static Rule_passRule Instance { get { if (instance==null) instance = new Rule_passRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static EdgeType[] edge_n_AllowedTypes = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static bool[] edge_n_IsAllowedType = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_req_AllowedTypes = null;
		public static bool[] neg_0_edge_req_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p1, @p2, };
		public enum EdgeNums { @n = 1, @_edge0, };

		private Rule_passRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_r, node_p1, (int) EdgeTypes.@token, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p1, node_p2 }, 
				new PatternEdge[] { edge_n, edge__edge0 }, 
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
					false, false, },
				new bool[] {
					true, true, true, },
				new bool[] {
					true, true, }
			);

			PatternGraph negPattern_0;
			{
			PatternEdge neg_0_edge_req = new PatternEdge(node_p1, node_r, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_p1, node_r }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPEdge edge__edge0 = match.Edges[ (int) EdgeNums.@_edge0 - 1 ];
			Edge_token edge_t;
			if(edge__edge0.type == EdgeType_token.typeVar)
			{
				// re-using edge__edge0 as edge_t
				edge_t = (Edge_token) edge__edge0;
				graph.ReuseEdge(edge__edge0, null, node_p2);
			}
			else
				edge_t = Edge_token.CreateEdge(graph, node_r, node_p2);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPEdge edge__edge0 = match.Edges[ (int) EdgeNums.@_edge0 - 1 ];
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge__edge0);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_passRule : LGSPStaticScheduleInfo
	{
		public Schedule_passRule()
		{
			ActionName = "passRule";
			this.RulePattern = Rule_passRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 5.5F, 1.0F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);

			PatternGraph negPattern_0;
			{
			PatternNode neg_0_node_m = new PatternNode((int) NodeTypes.@Resource, "neg_0_node_m", neg_0_node_m_AllowedTypes, neg_0_node_m_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_0_edge_req = new PatternEdge(node_p, neg_0_node_m, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_p, neg_0_node_m }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_hb = match.Edges[ (int) EdgeNums.@hb - 1 ];
			Edge_release edge_rel = Edge_release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_hb = match.Edges[ (int) EdgeNums.@hb - 1 ];
			Edge_release edge_rel = Edge_release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rel" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_releaseStarRule : LGSPRulePattern
	{
		private static Rule_releaseStarRule instance = null;
		public static Rule_releaseStarRule Instance { get { if (instance==null) instance = new Rule_releaseStarRule(); return instance; } }

		public static NodeType[] node_r2_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r1_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge_h2_AllowedTypes = null;
		public static EdgeType[] edge_h1_AllowedTypes = null;
		public static EdgeType[] edge_rq_AllowedTypes = null;
		public static bool[] edge_h2_IsAllowedType = null;
		public static bool[] edge_h1_IsAllowedType = null;
		public static bool[] edge_rq_IsAllowedType = null;

		public enum NodeNums { @r2  = 1, @p2, @r1, @p1, };
		public enum EdgeNums { @h2 = 1, @h1, @rq, };

		private Rule_releaseStarRule()
		{
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h2 = new PatternEdge(node_r2, node_p2, (int) EdgeTypes.@held_by, "edge_h2", edge_h2_AllowedTypes, edge_h2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h1 = new PatternEdge(node_r1, node_p2, (int) EdgeTypes.@held_by, "edge_h1", edge_h1_AllowedTypes, edge_h1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rq = new PatternEdge(node_p1, node_r1, (int) EdgeTypes.@request, "edge_rq", edge_rq_AllowedTypes, edge_rq_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r2, node_p2, node_r1, node_p1 }, 
				new PatternEdge[] { edge_h2, edge_h1, edge_rq }, 
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
					false, false, false, },
				new bool[] {
					true, true, true, true, },
				new bool[] {
					true, true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r1 = match.Nodes[ (int) NodeNums.@r1 - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPEdge edge_h1 = match.Edges[ (int) EdgeNums.@h1 - 1 ];
			Edge_release edge_rl = Edge_release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r1 = match.Nodes[ (int) NodeNums.@r1 - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPEdge edge_h1 = match.Edges[ (int) EdgeNums.@h1 - 1 ];
			Edge_release edge_rl = Edge_release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rl" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_requestRule : LGSPRulePattern
	{
		private static Rule_requestRule instance = null;
		public static Rule_requestRule Instance { get { if (instance==null) instance = new Rule_requestRule(); return instance; } }

		public static NodeType[] node_p_AllowedTypes = null;
		public static NodeType[] node_r_AllowedTypes = null;
		public static bool[] node_p_IsAllowedType = null;
		public static bool[] node_r_IsAllowedType = null;
		public static EdgeType[] neg_0_edge_hb_AllowedTypes = null;
		public static bool[] neg_0_edge_hb_IsAllowedType = null;
		public static NodeType[] neg_1_node_m_AllowedTypes = null;
		public static bool[] neg_1_node_m_IsAllowedType = null;
		public static EdgeType[] neg_1_edge_req_AllowedTypes = null;
		public static bool[] neg_1_edge_req_IsAllowedType = null;

		public enum NodeNums { @p  = 1, @r, };
		public enum EdgeNums { };

		private Rule_requestRule()
		{
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p, node_r }, 
				new PatternEdge[] {  }, 
				new Condition[] { },
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				new bool[] {
					false, false, },
				new bool[] {},
				new bool[] {
					true, true, },
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			PatternGraph negPattern_1;
			{
			PatternNode neg_1_node_m = new PatternNode((int) NodeTypes.@Resource, "neg_1_node_m", neg_1_node_m_AllowedTypes, neg_1_node_m_IsAllowedType, PatternElementType.NegElement, -1);
			PatternEdge neg_1_edge_req = new PatternEdge(node_p, neg_1_node_m, (int) EdgeTypes.@request, "neg_1_edge_req", neg_1_edge_req_AllowedTypes, neg_1_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_1 = new PatternGraph(
				new PatternNode[] { node_p, neg_1_node_m }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, negPattern_1, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_requestRule : LGSPStaticScheduleInfo
	{
		public Schedule_requestRule()
		{
			ActionName = "requestRule";
			this.RulePattern = Rule_requestRule.Instance;
			NodeCost = new float[] { 5.5F, 1.0F,  };
			EdgeCost = new float[] {  };
			NegNodeCost = new float[][] { new float[] { 1.0F, 5.5F, }, new float[] { 5.5F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, new float[] { 5.5F, }, };
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);

			PatternGraph negPattern_0;
			{
			PatternEdge neg_0_edge_req = new PatternEdge(node_p, node_r, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_p, node_r }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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
			NegNodeCost = new float[][] { new float[] { 5.5F, 1.0F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_requestStarRule : LGSPRulePattern
	{
		private static Rule_requestStarRule instance = null;
		public static Rule_requestStarRule Instance { get { if (instance==null) instance = new Rule_requestStarRule(); return instance; } }

		public static NodeType[] node_r2_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_r1_AllowedTypes = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
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

		public enum NodeNums { @r2  = 1, @p2, @p1, @r1, };
		public enum EdgeNums { @h2 = 1, @n, @h1, };

		private Rule_requestStarRule()
		{
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h2 = new PatternEdge(node_r2, node_p2, (int) EdgeTypes.@held_by, "edge_h2", edge_h2_AllowedTypes, edge_h2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p2, node_p1, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h1 = new PatternEdge(node_r1, node_p1, (int) EdgeTypes.@held_by, "edge_h1", edge_h1_AllowedTypes, edge_h1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r2, node_p2, node_p1, node_r1 }, 
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
					false, false, false, },
				new bool[] {
					true, true, true, true, },
				new bool[] {
					true, true, true, }
			);

			PatternGraph negPattern_0;
			{
			PatternEdge neg_0_edge_req = new PatternEdge(node_p1, node_r2, (int) EdgeTypes.@request, "neg_0_edge_req", neg_0_edge_req_AllowedTypes, neg_0_edge_req_IsAllowedType, PatternElementType.NegElement, -1);
			negPattern_0 = new PatternGraph(
				new PatternNode[] { node_p1, node_r2 }, 
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_r2 = match.Nodes[ (int) NodeNums.@r2 - 1 ];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p1, node_r2);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.Nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_r2 = match.Nodes[ (int) NodeNums.@r2 - 1 ];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p1, node_r2);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_takeRule : LGSPRulePattern
	{
		private static Rule_takeRule instance = null;
		public static Rule_takeRule Instance { get { if (instance==null) instance = new Rule_takeRule(); return instance; } }

		public static NodeType[] node_p_AllowedTypes = null;
		public static NodeType[] node_r_AllowedTypes = null;
		public static bool[] node_p_IsAllowedType = null;
		public static bool[] node_r_IsAllowedType = null;
		public static EdgeType[] edge_req_AllowedTypes = null;
		public static EdgeType[] edge_t_AllowedTypes = null;
		public static bool[] edge_req_IsAllowedType = null;
		public static bool[] edge_t_IsAllowedType = null;

		public enum NodeNums { @p  = 1, @r, };
		public enum EdgeNums { @req = 1, @t, };

		private Rule_takeRule()
		{
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p, node_r, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_t = new PatternEdge(node_r, node_p, (int) EdgeTypes.@token, "edge_t", edge_t_AllowedTypes, edge_t_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p, node_r }, 
				new PatternEdge[] { edge_req, edge_t }, 
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
					false, false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_t = match.Edges[ (int) EdgeNums.@t - 1 ];
			LGSPEdge edge_req = match.Edges[ (int) EdgeNums.@req - 1 ];
			Edge_held_by edge_hb = Edge_held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_t);
			graph.Remove(edge_req);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_t = match.Edges[ (int) EdgeNums.@t - 1 ];
			LGSPEdge edge_req = match.Edges[ (int) EdgeNums.@req - 1 ];
			Edge_held_by edge_hb = Edge_held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_t);
			graph.Remove(edge_req);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "hb" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_takeRule : LGSPStaticScheduleInfo
	{
		public Schedule_takeRule()
		{
			ActionName = "takeRule";
			this.RulePattern = Rule_takeRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 1.0F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
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
		public static EdgeType[] edge_hb_AllowedTypes = null;
		public static EdgeType[] edge_b_AllowedTypes = null;
		public static bool[] edge_hb_IsAllowedType = null;
		public static bool[] edge_b_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p, };
		public enum EdgeNums { @hb = 1, @b, };

		private Rule_unlockRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r, node_p, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_b = new PatternEdge(node_r, node_p, (int) EdgeTypes.@blocked, "edge_b", edge_b_AllowedTypes, edge_b_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p }, 
				new PatternEdge[] { edge_hb, edge_b }, 
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
					false, false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_b = match.Edges[ (int) EdgeNums.@b - 1 ];
			LGSPEdge edge_hb = match.Edges[ (int) EdgeNums.@hb - 1 ];
			Edge_release edge_rel = Edge_release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.Nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_b = match.Edges[ (int) EdgeNums.@b - 1 ];
			LGSPEdge edge_hb = match.Edges[ (int) EdgeNums.@hb - 1 ];
			Edge_release edge_rel = Edge_release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rel" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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
					false, },
				new bool[] {
					true, true, },
				new bool[] {
					true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_t = match.Edges[ (int) EdgeNums.@t - 1 ];
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_t = match.Edges[ (int) EdgeNums.@t - 1 ];
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
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

	public class Rule_waitingRule : LGSPRulePattern
	{
		private static Rule_waitingRule instance = null;
		public static Rule_waitingRule Instance { get { if (instance==null) instance = new Rule_waitingRule(); return instance; } }

		public static NodeType[] node_r_AllowedTypes = null;
		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_r1_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static NodeType[] node_r2_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static EdgeType[] edge_req_AllowedTypes = null;
		public static EdgeType[] edge_hb_AllowedTypes = null;
		public static EdgeType[] edge_b_AllowedTypes = null;
		public static bool[] edge_req_IsAllowedType = null;
		public static bool[] edge_hb_IsAllowedType = null;
		public static bool[] edge_b_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p2, @r1, @p1, @r2, };
		public enum EdgeNums { @req = 1, @hb, @b, };

		private Rule_waitingRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p2, node_r1, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r1, node_p1, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_b = new PatternEdge(node_r2, node_p1, (int) EdgeTypes.@blocked, "edge_b", edge_b_AllowedTypes, edge_b_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p2, node_r1, node_p1, node_r2 }, 
				new PatternEdge[] { edge_req, edge_hb, edge_b }, 
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
					false, false, false, },
				new bool[] {
					true, true, true, true, true, },
				new bool[] {
					true, true, true, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new GrGenType[] { };
			outputs = new GrGenType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r2 = match.Nodes[ (int) NodeNums.@r2 - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_b = match.Edges[ (int) EdgeNums.@b - 1 ];
			Edge_blocked edge_bn;
			if(edge_b.type == EdgeType_blocked.typeVar)
			{
				// re-using edge_b as edge_bn
				edge_bn = (Edge_blocked) edge_b;
				graph.ReuseEdge(edge_b, null, node_p2);
			}
			else
				edge_bn = Edge_blocked.CreateEdge(graph, node_r2, node_p2);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r2 = match.Nodes[ (int) NodeNums.@r2 - 1 ];
			LGSPNode node_p2 = match.Nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_r = match.Nodes[ (int) NodeNums.@r - 1 ];
			LGSPEdge edge_b = match.Edges[ (int) EdgeNums.@b - 1 ];
			Edge_blocked edge_bn = Edge_blocked.CreateEdge(graph, node_r2, node_p2);
			graph.Remove(edge_b);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "bn" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_waitingRule : LGSPStaticScheduleInfo
	{
		public Schedule_waitingRule()
		{
			ActionName = "waitingRule";
			this.RulePattern = Rule_waitingRule.Instance;
			NodeCost = new float[] { 1.0F, 5.5F, 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif


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
            // Lookup node_p 
            int node_type_id_node_p = 0;
            for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[node_type_id_node_p], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
            {
                // NegativePattern 
                {
                    // Extend incoming neg_0_edge__edge0 from node_p 
                    LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_p.inhead;
                    if(edge_head_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            // Implicit source neg_0_node_r from neg_0_edge__edge0 
                            LGSPNode node_cur_neg_0_node_r = edge_cur_neg_0_edge__edge0.source;
                            if(!NodeType_Resource.isMyType[node_cur_neg_0_node_r.type.TypeID]) {
                                continue;
                            }
                            goto label0;
                        }
                        while( (edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.inNext) != edge_head_neg_0_edge__edge0 );
                    }
                }
                LGSPMatch match = matchesList.GetNewMatch();
                match.Nodes[0] = node_cur_node_p;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(node_cur_node_p);
                    return matches;
                }
label0: ;
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
            // Lookup edge_hb 
            int edge_type_id_edge_hb = 1;
            for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[edge_type_id_edge_hb], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
            {
                // Implicit source node_r from edge_hb 
                LGSPNode node_cur_node_r = edge_cur_edge_hb.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                    continue;
                }
                // Implicit target node_p2 from edge_hb 
                LGSPNode node_cur_node_p2 = edge_cur_edge_hb.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                    continue;
                }
                bool node_cur_node_p2_prevIsMatched = node_cur_node_p2.isMatched;
                node_cur_node_p2.isMatched = true;
                // Extend incoming edge_req from node_r 
                LGSPEdge edge_head_edge_req = node_cur_node_r.inhead;
                if(edge_head_edge_req != null)
                {
                    LGSPEdge edge_cur_edge_req = edge_head_edge_req;
                    do
                    {
                        if(!EdgeType_request.isMyType[edge_cur_edge_req.type.TypeID]) {
                            continue;
                        }
                        // Implicit source node_p1 from edge_req 
                        LGSPNode node_cur_node_p1 = edge_cur_edge_req.source;
                        if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_node_p1.isMatched
                            && node_cur_node_p1==node_cur_node_p2
                            )
                        {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_r;
                        match.Nodes[1] = node_cur_node_p2;
                        match.Nodes[2] = node_cur_node_p1;
                        match.Edges[0] = edge_cur_edge_hb;
                        match.Edges[1] = edge_cur_edge_req;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_r.MoveInHeadAfter(edge_cur_edge_req);
                            graph.MoveHeadAfter(edge_cur_edge_hb);
                            node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                            return matches;
                        }
                    }
                    while( (edge_cur_edge_req = edge_cur_edge_req.inNext) != edge_head_edge_req );
                }
                node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
            }
            return matches;
        }
    }
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
            // Lookup edge_n 
            int edge_type_id_edge_n = 2;
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_id_edge_n], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // Implicit source node_p1 from edge_n 
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                    continue;
                }
                bool node_cur_node_p1_prevIsMatched = node_cur_node_p1.isMatched;
                node_cur_node_p1.isMatched = true;
                // Implicit target node_p2 from edge_n 
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    continue;
                }
                if(node_cur_node_p2.isMatched
                    && node_cur_node_p2==node_cur_node_p1
                    )
                {
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    continue;
                }
                // Extend incoming edge_rel from node_p1 
                LGSPEdge edge_head_edge_rel = node_cur_node_p1.inhead;
                if(edge_head_edge_rel != null)
                {
                    LGSPEdge edge_cur_edge_rel = edge_head_edge_rel;
                    do
                    {
                        if(!EdgeType_release.isMyType[edge_cur_edge_rel.type.TypeID]) {
                            continue;
                        }
                        // Implicit source node_r from edge_rel 
                        LGSPNode node_cur_node_r = edge_cur_edge_rel.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_p1;
                        match.Nodes[1] = node_cur_node_p2;
                        match.Nodes[2] = node_cur_node_r;
                        match.Edges[0] = edge_cur_edge_n;
                        match.Edges[1] = edge_cur_edge_rel;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_rel);
                            graph.MoveHeadAfter(edge_cur_edge_n);
                            node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                            return matches;
                        }
                    }
                    while( (edge_cur_edge_rel = edge_cur_edge_rel.inNext) != edge_head_edge_rel );
                }
                node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
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
            // Lookup edge_b 
            int edge_type_id_edge_b = 0;
            for(LGSPEdge edge_head_edge_b = graph.edgesByTypeHeads[edge_type_id_edge_b], edge_cur_edge_b = edge_head_edge_b.typeNext; edge_cur_edge_b != edge_head_edge_b; edge_cur_edge_b = edge_cur_edge_b.typeNext)
            {
                // Implicit source node_r from edge_b 
                LGSPNode node_cur_node_r = edge_cur_edge_b.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                    continue;
                }
                // Implicit target node_p from edge_b 
                LGSPNode node_cur_node_p = edge_cur_edge_b.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) {
                    continue;
                }
                // NegativePattern 
                {
                    // Extend incoming neg_0_edge_hb from node_p 
                    LGSPEdge edge_head_neg_0_edge_hb = node_cur_node_p.inhead;
                    if(edge_head_neg_0_edge_hb != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge_hb = edge_head_neg_0_edge_hb;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge_hb.type.TypeID]) {
                                continue;
                            }
                            // Implicit source neg_0_node_m from neg_0_edge_hb 
                            LGSPNode node_cur_neg_0_node_m = edge_cur_neg_0_edge_hb.source;
                            if(!NodeType_Resource.isMyType[node_cur_neg_0_node_m.type.TypeID]) {
                                continue;
                            }
                            goto label1;
                        }
                        while( (edge_cur_neg_0_edge_hb = edge_cur_neg_0_edge_hb.inNext) != edge_head_neg_0_edge_hb );
                    }
                }
                LGSPMatch match = matchesList.GetNewMatch();
                match.Nodes[0] = node_cur_node_r;
                match.Nodes[1] = node_cur_node_p;
                match.Edges[0] = edge_cur_edge_b;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_b);
                    return matches;
                }
label1: ;
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
            // Lookup edge_n2 
            int edge_type_id_edge_n2 = 2;
            for(LGSPEdge edge_head_edge_n2 = graph.edgesByTypeHeads[edge_type_id_edge_n2], edge_cur_edge_n2 = edge_head_edge_n2.typeNext; edge_cur_edge_n2 != edge_head_edge_n2; edge_cur_edge_n2 = edge_cur_edge_n2.typeNext)
            {
                bool edge_cur_edge_n2_prevIsMatched = edge_cur_edge_n2.isMatched;
                edge_cur_edge_n2.isMatched = true;
                // Implicit source node_p from edge_n2 
                LGSPNode node_cur_node_p = edge_cur_edge_n2.source;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) {
                    edge_cur_edge_n2.isMatched = edge_cur_edge_n2_prevIsMatched;
                    continue;
                }
                bool node_cur_node_p_prevIsMatched = node_cur_node_p.isMatched;
                node_cur_node_p.isMatched = true;
                // Implicit target node_p2 from edge_n2 
                LGSPNode node_cur_node_p2 = edge_cur_edge_n2.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                    node_cur_node_p.isMatched = node_cur_node_p_prevIsMatched;
                    edge_cur_edge_n2.isMatched = edge_cur_edge_n2_prevIsMatched;
                    continue;
                }
                if(node_cur_node_p2.isMatched
                    && node_cur_node_p2==node_cur_node_p
                    )
                {
                    node_cur_node_p.isMatched = node_cur_node_p_prevIsMatched;
                    edge_cur_edge_n2.isMatched = edge_cur_edge_n2_prevIsMatched;
                    continue;
                }
                bool node_cur_node_p2_prevIsMatched = node_cur_node_p2.isMatched;
                node_cur_node_p2.isMatched = true;
                // Extend incoming edge_n1 from node_p 
                LGSPEdge edge_head_edge_n1 = node_cur_node_p.inhead;
                if(edge_head_edge_n1 != null)
                {
                    LGSPEdge edge_cur_edge_n1 = edge_head_edge_n1;
                    do
                    {
                        if(!EdgeType_next.isMyType[edge_cur_edge_n1.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_edge_n1.isMatched
                            && edge_cur_edge_n1==edge_cur_edge_n2
                            )
                        {
                            continue;
                        }
                        // Implicit source node_p1 from edge_n1 
                        LGSPNode node_cur_node_p1 = edge_cur_edge_n1.source;
                        if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_node_p1.isMatched
                            && (node_cur_node_p1==node_cur_node_p
                                || node_cur_node_p1==node_cur_node_p2
                                )
                            )
                        {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_p;
                        match.Nodes[1] = node_cur_node_p2;
                        match.Nodes[2] = node_cur_node_p1;
                        match.Edges[0] = edge_cur_edge_n2;
                        match.Edges[1] = edge_cur_edge_n1;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_p.MoveInHeadAfter(edge_cur_edge_n1);
                            graph.MoveHeadAfter(edge_cur_edge_n2);
                            node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                            node_cur_node_p.isMatched = node_cur_node_p_prevIsMatched;
                            edge_cur_edge_n2.isMatched = edge_cur_edge_n2_prevIsMatched;
                            return matches;
                        }
                    }
                    while( (edge_cur_edge_n1 = edge_cur_edge_n1.inNext) != edge_head_edge_n1 );
                }
                node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                node_cur_node_p.isMatched = node_cur_node_p_prevIsMatched;
                edge_cur_edge_n2.isMatched = edge_cur_edge_n2_prevIsMatched;
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
            // Lookup node_p 
            int node_type_id_node_p = 0;
            for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[node_type_id_node_p], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
            {
                LGSPMatch match = matchesList.GetNewMatch();
                match.Nodes[0] = node_cur_node_p;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(node_cur_node_p);
                    return matches;
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
            // Lookup edge_n 
            int edge_type_id_edge_n = 2;
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_id_edge_n], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // Implicit source node_p1 from edge_n 
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                    continue;
                }
                bool node_cur_node_p1_prevIsMatched = node_cur_node_p1.isMatched;
                node_cur_node_p1.isMatched = true;
                // Implicit target node_p2 from edge_n 
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    continue;
                }
                if(node_cur_node_p2.isMatched
                    && node_cur_node_p2==node_cur_node_p1
                    )
                {
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    continue;
                }
                LGSPMatch match = matchesList.GetNewMatch();
                match.Nodes[0] = node_cur_node_p1;
                match.Nodes[1] = node_cur_node_p2;
                match.Edges[0] = edge_cur_edge_n;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_n);
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    return matches;
                }
                node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
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
            // Lookup edge_n 
            int edge_type_id_edge_n = 2;
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_id_edge_n], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // Implicit source node_p1 from edge_n 
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                    continue;
                }
                bool node_cur_node_p1_prevIsMatched = node_cur_node_p1.isMatched;
                node_cur_node_p1.isMatched = true;
                // Implicit target node_p2 from edge_n 
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    continue;
                }
                if(node_cur_node_p2.isMatched
                    && node_cur_node_p2==node_cur_node_p1
                    )
                {
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    continue;
                }
                // Extend incoming edge__edge0 from node_p1 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_p1.inhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_token.isMyType[edge_cur_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        // Implicit source node_r from edge__edge0 
                        LGSPNode node_cur_node_r = edge_cur_edge__edge0.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                            continue;
                        }
                        // NegativePattern 
                        {
                            // Extend outgoing neg_0_edge_req from node_p1 
                            LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p1.outhead;
                            if(edge_head_neg_0_edge_req != null)
                            {
                                LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) {
                                        continue;
                                    }
                                    if(edge_cur_neg_0_edge_req.target != node_cur_node_r) {
                                        continue;
                                    }
                                    goto label2;
                                }
                                while( (edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req );
                            }
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_r;
                        match.Nodes[1] = node_cur_node_p1;
                        match.Nodes[2] = node_cur_node_p2;
                        match.Edges[0] = edge_cur_edge_n;
                        match.Edges[1] = edge_cur_edge__edge0;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_p1.MoveInHeadAfter(edge_cur_edge__edge0);
                            graph.MoveHeadAfter(edge_cur_edge_n);
                            node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                            return matches;
                        }
label2: ;
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.inNext) != edge_head_edge__edge0 );
                }
                node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
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
            // Lookup edge_hb 
            int edge_type_id_edge_hb = 1;
            for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[edge_type_id_edge_hb], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
            {
                // Implicit source node_r from edge_hb 
                LGSPNode node_cur_node_r = edge_cur_edge_hb.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                    continue;
                }
                // Implicit target node_p from edge_hb 
                LGSPNode node_cur_node_p = edge_cur_edge_hb.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) {
                    continue;
                }
                // NegativePattern 
                {
                    // Extend outgoing neg_0_edge_req from node_p 
                    LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p.outhead;
                    if(edge_head_neg_0_edge_req != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) {
                                continue;
                            }
                            // Implicit target neg_0_node_m from neg_0_edge_req 
                            LGSPNode node_cur_neg_0_node_m = edge_cur_neg_0_edge_req.target;
                            if(!NodeType_Resource.isMyType[node_cur_neg_0_node_m.type.TypeID]) {
                                continue;
                            }
                            goto label3;
                        }
                        while( (edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req );
                    }
                }
                LGSPMatch match = matchesList.GetNewMatch();
                match.Nodes[0] = node_cur_node_r;
                match.Nodes[1] = node_cur_node_p;
                match.Edges[0] = edge_cur_edge_hb;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_hb);
                    return matches;
                }
label3: ;
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
            // Lookup edge_h2 
            int edge_type_id_edge_h2 = 1;
            for(LGSPEdge edge_head_edge_h2 = graph.edgesByTypeHeads[edge_type_id_edge_h2], edge_cur_edge_h2 = edge_head_edge_h2.typeNext; edge_cur_edge_h2 != edge_head_edge_h2; edge_cur_edge_h2 = edge_cur_edge_h2.typeNext)
            {
                bool edge_cur_edge_h2_prevIsMatched = edge_cur_edge_h2.isMatched;
                edge_cur_edge_h2.isMatched = true;
                // Implicit source node_r2 from edge_h2 
                LGSPNode node_cur_node_r2 = edge_cur_edge_h2.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r2.type.TypeID]) {
                    edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
                    continue;
                }
                bool node_cur_node_r2_prevIsMatched = node_cur_node_r2.isMatched;
                node_cur_node_r2.isMatched = true;
                // Implicit target node_p2 from edge_h2 
                LGSPNode node_cur_node_p2 = edge_cur_edge_h2.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                    node_cur_node_r2.isMatched = node_cur_node_r2_prevIsMatched;
                    edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
                    continue;
                }
                bool node_cur_node_p2_prevIsMatched = node_cur_node_p2.isMatched;
                node_cur_node_p2.isMatched = true;
                // Extend incoming edge_h1 from node_p2 
                LGSPEdge edge_head_edge_h1 = node_cur_node_p2.inhead;
                if(edge_head_edge_h1 != null)
                {
                    LGSPEdge edge_cur_edge_h1 = edge_head_edge_h1;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_edge_h1.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_edge_h1.isMatched
                            && edge_cur_edge_h1==edge_cur_edge_h2
                            )
                        {
                            continue;
                        }
                        // Implicit source node_r1 from edge_h1 
                        LGSPNode node_cur_node_r1 = edge_cur_edge_h1.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r1.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_node_r1.isMatched
                            && node_cur_node_r1==node_cur_node_r2
                            )
                        {
                            continue;
                        }
                        // Extend incoming edge_rq from node_r1 
                        LGSPEdge edge_head_edge_rq = node_cur_node_r1.inhead;
                        if(edge_head_edge_rq != null)
                        {
                            LGSPEdge edge_cur_edge_rq = edge_head_edge_rq;
                            do
                            {
                                if(!EdgeType_request.isMyType[edge_cur_edge_rq.type.TypeID]) {
                                    continue;
                                }
                                // Implicit source node_p1 from edge_rq 
                                LGSPNode node_cur_node_p1 = edge_cur_edge_rq.source;
                                if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_node_p1.isMatched
                                    && node_cur_node_p1==node_cur_node_p2
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.Nodes[0] = node_cur_node_r2;
                                match.Nodes[1] = node_cur_node_p2;
                                match.Nodes[2] = node_cur_node_r1;
                                match.Nodes[3] = node_cur_node_p1;
                                match.Edges[0] = edge_cur_edge_h2;
                                match.Edges[1] = edge_cur_edge_h1;
                                match.Edges[2] = edge_cur_edge_rq;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_r1.MoveInHeadAfter(edge_cur_edge_rq);
                                    node_cur_node_p2.MoveInHeadAfter(edge_cur_edge_h1);
                                    graph.MoveHeadAfter(edge_cur_edge_h2);
                                    node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                                    node_cur_node_r2.isMatched = node_cur_node_r2_prevIsMatched;
                                    edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
                                    return matches;
                                }
                            }
                            while( (edge_cur_edge_rq = edge_cur_edge_rq.inNext) != edge_head_edge_rq );
                        }
                    }
                    while( (edge_cur_edge_h1 = edge_cur_edge_h1.inNext) != edge_head_edge_h1 );
                }
                node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                node_cur_node_r2.isMatched = node_cur_node_r2_prevIsMatched;
                edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
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
            // Lookup node_r 
            int node_type_id_node_r = 1;
            for(LGSPNode node_head_node_r = graph.nodesByTypeHeads[node_type_id_node_r], node_cur_node_r = node_head_node_r.typeNext; node_cur_node_r != node_head_node_r; node_cur_node_r = node_cur_node_r.typeNext)
            {
                // Lookup node_p 
                int node_type_id_node_p = 0;
                for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[node_type_id_node_p], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
                {
                    // NegativePattern 
                    {
                        // Extend outgoing neg_0_edge_hb from node_r 
                        LGSPEdge edge_head_neg_0_edge_hb = node_cur_node_r.outhead;
                        if(edge_head_neg_0_edge_hb != null)
                        {
                            LGSPEdge edge_cur_neg_0_edge_hb = edge_head_neg_0_edge_hb;
                            do
                            {
                                if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge_hb.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_neg_0_edge_hb.target != node_cur_node_p) {
                                    continue;
                                }
                                goto label4;
                            }
                            while( (edge_cur_neg_0_edge_hb = edge_cur_neg_0_edge_hb.outNext) != edge_head_neg_0_edge_hb );
                        }
                    }
                    // NegativePattern 
                    {
                        // Extend outgoing neg_1_edge_req from node_p 
                        LGSPEdge edge_head_neg_1_edge_req = node_cur_node_p.outhead;
                        if(edge_head_neg_1_edge_req != null)
                        {
                            LGSPEdge edge_cur_neg_1_edge_req = edge_head_neg_1_edge_req;
                            do
                            {
                                if(!EdgeType_request.isMyType[edge_cur_neg_1_edge_req.type.TypeID]) {
                                    continue;
                                }
                                // Implicit target neg_1_node_m from neg_1_edge_req 
                                LGSPNode node_cur_neg_1_node_m = edge_cur_neg_1_edge_req.target;
                                if(!NodeType_Resource.isMyType[node_cur_neg_1_node_m.type.TypeID]) {
                                    continue;
                                }
                                goto label5;
                            }
                            while( (edge_cur_neg_1_edge_req = edge_cur_neg_1_edge_req.outNext) != edge_head_neg_1_edge_req );
                        }
                    }
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.Nodes[0] = node_cur_node_p;
                    match.Nodes[1] = node_cur_node_r;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(node_cur_node_p);
                        graph.MoveHeadAfter(node_cur_node_r);
                        return matches;
                    }
label4: ;
label5: ;
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
            // Lookup edge_t 
            int edge_type_id_edge_t = 5;
            for(LGSPEdge edge_head_edge_t = graph.edgesByTypeHeads[edge_type_id_edge_t], edge_cur_edge_t = edge_head_edge_t.typeNext; edge_cur_edge_t != edge_head_edge_t; edge_cur_edge_t = edge_cur_edge_t.typeNext)
            {
                // Implicit source node_r from edge_t 
                LGSPNode node_cur_node_r = edge_cur_edge_t.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                    continue;
                }
                // Implicit target node_p from edge_t 
                LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) {
                    continue;
                }
                // NegativePattern 
                {
                    // Extend outgoing neg_0_edge_req from node_p 
                    LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p.outhead;
                    if(edge_head_neg_0_edge_req != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) {
                                continue;
                            }
                            if(edge_cur_neg_0_edge_req.target != node_cur_node_r) {
                                continue;
                            }
                            goto label6;
                        }
                        while( (edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req );
                    }
                }
                LGSPMatch match = matchesList.GetNewMatch();
                match.Nodes[0] = node_cur_node_r;
                match.Nodes[1] = node_cur_node_p;
                match.Edges[0] = edge_cur_edge_t;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_t);
                    return matches;
                }
label6: ;
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
            // Lookup edge_n 
            int edge_type_id_edge_n = 2;
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[edge_type_id_edge_n], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // Implicit source node_p2 from edge_n 
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                    continue;
                }
                bool node_cur_node_p2_prevIsMatched = node_cur_node_p2.isMatched;
                node_cur_node_p2.isMatched = true;
                // Implicit target node_p1 from edge_n 
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                    node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                    continue;
                }
                if(node_cur_node_p1.isMatched
                    && node_cur_node_p1==node_cur_node_p2
                    )
                {
                    node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                    continue;
                }
                // Extend incoming edge_h2 from node_p2 
                LGSPEdge edge_head_edge_h2 = node_cur_node_p2.inhead;
                if(edge_head_edge_h2 != null)
                {
                    LGSPEdge edge_cur_edge_h2 = edge_head_edge_h2;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_edge_h2.type.TypeID]) {
                            continue;
                        }
                        bool edge_cur_edge_h2_prevIsMatched = edge_cur_edge_h2.isMatched;
                        edge_cur_edge_h2.isMatched = true;
                        // Implicit source node_r2 from edge_h2 
                        LGSPNode node_cur_node_r2 = edge_cur_edge_h2.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r2.type.TypeID]) {
                            edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
                            continue;
                        }
                        bool node_cur_node_r2_prevIsMatched = node_cur_node_r2.isMatched;
                        node_cur_node_r2.isMatched = true;
                        // NegativePattern 
                        {
                            // Extend outgoing neg_0_edge_req from node_p1 
                            LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p1.outhead;
                            if(edge_head_neg_0_edge_req != null)
                            {
                                LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.TypeID]) {
                                        continue;
                                    }
                                    if(edge_cur_neg_0_edge_req.target != node_cur_node_r2) {
                                        continue;
                                    }
                                    node_cur_node_r2.isMatched = node_cur_node_r2_prevIsMatched;
                                    edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
                                    goto label7;
                                }
                                while( (edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req );
                            }
                        }
                        // Extend incoming edge_h1 from node_p1 
                        LGSPEdge edge_head_edge_h1 = node_cur_node_p1.inhead;
                        if(edge_head_edge_h1 != null)
                        {
                            LGSPEdge edge_cur_edge_h1 = edge_head_edge_h1;
                            do
                            {
                                if(!EdgeType_held_by.isMyType[edge_cur_edge_h1.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_edge_h1.isMatched
                                    && edge_cur_edge_h1==edge_cur_edge_h2
                                    )
                                {
                                    continue;
                                }
                                // Implicit source node_r1 from edge_h1 
                                LGSPNode node_cur_node_r1 = edge_cur_edge_h1.source;
                                if(!NodeType_Resource.isMyType[node_cur_node_r1.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_node_r1.isMatched
                                    && node_cur_node_r1==node_cur_node_r2
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.Nodes[0] = node_cur_node_r2;
                                match.Nodes[1] = node_cur_node_p2;
                                match.Nodes[2] = node_cur_node_p1;
                                match.Nodes[3] = node_cur_node_r1;
                                match.Edges[0] = edge_cur_edge_h2;
                                match.Edges[1] = edge_cur_edge_n;
                                match.Edges[2] = edge_cur_edge_h1;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_h1);
                                    node_cur_node_p2.MoveInHeadAfter(edge_cur_edge_h2);
                                    graph.MoveHeadAfter(edge_cur_edge_n);
                                    node_cur_node_r2.isMatched = node_cur_node_r2_prevIsMatched;
                                    edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
                                    node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
                                    return matches;
                                }
                            }
                            while( (edge_cur_edge_h1 = edge_cur_edge_h1.inNext) != edge_head_edge_h1 );
                        }
                        node_cur_node_r2.isMatched = node_cur_node_r2_prevIsMatched;
                        edge_cur_edge_h2.isMatched = edge_cur_edge_h2_prevIsMatched;
label7: ;
                    }
                    while( (edge_cur_edge_h2 = edge_cur_edge_h2.inNext) != edge_head_edge_h2 );
                }
                node_cur_node_p2.isMatched = node_cur_node_p2_prevIsMatched;
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
            // Lookup edge_req 
            int edge_type_id_edge_req = 4;
            for(LGSPEdge edge_head_edge_req = graph.edgesByTypeHeads[edge_type_id_edge_req], edge_cur_edge_req = edge_head_edge_req.typeNext; edge_cur_edge_req != edge_head_edge_req; edge_cur_edge_req = edge_cur_edge_req.typeNext)
            {
                // Implicit source node_p from edge_req 
                LGSPNode node_cur_node_p = edge_cur_edge_req.source;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) {
                    continue;
                }
                // Implicit target node_r from edge_req 
                LGSPNode node_cur_node_r = edge_cur_edge_req.target;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                    continue;
                }
                // Extend outgoing edge_t from node_r 
                LGSPEdge edge_head_edge_t = node_cur_node_r.outhead;
                if(edge_head_edge_t != null)
                {
                    LGSPEdge edge_cur_edge_t = edge_head_edge_t;
                    do
                    {
                        if(!EdgeType_token.isMyType[edge_cur_edge_t.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_edge_t.target != node_cur_node_p) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_p;
                        match.Nodes[1] = node_cur_node_r;
                        match.Edges[0] = edge_cur_edge_req;
                        match.Edges[1] = edge_cur_edge_t;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_r.MoveOutHeadAfter(edge_cur_edge_t);
                            graph.MoveHeadAfter(edge_cur_edge_req);
                            return matches;
                        }
                    }
                    while( (edge_cur_edge_t = edge_cur_edge_t.outNext) != edge_head_edge_t );
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
            // Lookup edge_hb 
            int edge_type_id_edge_hb = 1;
            for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[edge_type_id_edge_hb], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
            {
                // Implicit source node_r from edge_hb 
                LGSPNode node_cur_node_r = edge_cur_edge_hb.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                    continue;
                }
                // Implicit target node_p from edge_hb 
                LGSPNode node_cur_node_p = edge_cur_edge_hb.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) {
                    continue;
                }
                // Extend outgoing edge_b from node_r 
                LGSPEdge edge_head_edge_b = node_cur_node_r.outhead;
                if(edge_head_edge_b != null)
                {
                    LGSPEdge edge_cur_edge_b = edge_head_edge_b;
                    do
                    {
                        if(!EdgeType_blocked.isMyType[edge_cur_edge_b.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_edge_b.target != node_cur_node_p) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_r;
                        match.Nodes[1] = node_cur_node_p;
                        match.Edges[0] = edge_cur_edge_hb;
                        match.Edges[1] = edge_cur_edge_b;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_r.MoveOutHeadAfter(edge_cur_edge_b);
                            graph.MoveHeadAfter(edge_cur_edge_hb);
                            return matches;
                        }
                    }
                    while( (edge_cur_edge_b = edge_cur_edge_b.outNext) != edge_head_edge_b );
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
            // Lookup edge_t 
            int edge_type_id_edge_t = 5;
            for(LGSPEdge edge_head_edge_t = graph.edgesByTypeHeads[edge_type_id_edge_t], edge_cur_edge_t = edge_head_edge_t.typeNext; edge_cur_edge_t != edge_head_edge_t; edge_cur_edge_t = edge_cur_edge_t.typeNext)
            {
                // Implicit source node_r from edge_t 
                LGSPNode node_cur_node_r = edge_cur_edge_t.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.TypeID]) {
                    continue;
                }
                // Implicit target node_p from edge_t 
                LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.TypeID]) {
                    continue;
                }
                LGSPMatch match = matchesList.GetNewMatch();
                match.Nodes[0] = node_cur_node_r;
                match.Nodes[1] = node_cur_node_p;
                match.Edges[0] = edge_cur_edge_t;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_t);
                    return matches;
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
            // Lookup node_r 
            int node_type_id_node_r = 1;
            for(LGSPNode node_head_node_r = graph.nodesByTypeHeads[node_type_id_node_r], node_cur_node_r = node_head_node_r.typeNext; node_cur_node_r != node_head_node_r; node_cur_node_r = node_cur_node_r.typeNext)
            {
                bool node_cur_node_r_prevIsMatched = node_cur_node_r.isMatched;
                node_cur_node_r.isMatched = true;
                // Lookup edge_hb 
                int edge_type_id_edge_hb = 1;
                for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[edge_type_id_edge_hb], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
                {
                    // Implicit source node_r1 from edge_hb 
                    LGSPNode node_cur_node_r1 = edge_cur_edge_hb.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r1.type.TypeID]) {
                        continue;
                    }
                    if(node_cur_node_r1.isMatched
                        && node_cur_node_r1==node_cur_node_r
                        )
                    {
                        continue;
                    }
                    bool node_cur_node_r1_prevIsMatched = node_cur_node_r1.isMatched;
                    node_cur_node_r1.isMatched = true;
                    // Implicit target node_p1 from edge_hb 
                    LGSPNode node_cur_node_p1 = edge_cur_edge_hb.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) {
                        node_cur_node_r1.isMatched = node_cur_node_r1_prevIsMatched;
                        continue;
                    }
                    bool node_cur_node_p1_prevIsMatched = node_cur_node_p1.isMatched;
                    node_cur_node_p1.isMatched = true;
                    // Extend incoming edge_req from node_r1 
                    LGSPEdge edge_head_edge_req = node_cur_node_r1.inhead;
                    if(edge_head_edge_req != null)
                    {
                        LGSPEdge edge_cur_edge_req = edge_head_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_edge_req.type.TypeID]) {
                                continue;
                            }
                            // Implicit source node_p2 from edge_req 
                            LGSPNode node_cur_node_p2 = edge_cur_edge_req.source;
                            if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_node_p2.isMatched
                                && node_cur_node_p2==node_cur_node_p1
                                )
                            {
                                continue;
                            }
                            // Extend incoming edge_b from node_p1 
                            LGSPEdge edge_head_edge_b = node_cur_node_p1.inhead;
                            if(edge_head_edge_b != null)
                            {
                                LGSPEdge edge_cur_edge_b = edge_head_edge_b;
                                do
                                {
                                    if(!EdgeType_blocked.isMyType[edge_cur_edge_b.type.TypeID]) {
                                        continue;
                                    }
                                    // Implicit source node_r2 from edge_b 
                                    LGSPNode node_cur_node_r2 = edge_cur_edge_b.source;
                                    if(!NodeType_Resource.isMyType[node_cur_node_r2.type.TypeID]) {
                                        continue;
                                    }
                                    if(node_cur_node_r2.isMatched
                                        && (node_cur_node_r2==node_cur_node_r
                                            || node_cur_node_r2==node_cur_node_r1
                                            )
                                        )
                                    {
                                        continue;
                                    }
                                    LGSPMatch match = matchesList.GetNewMatch();
                                    match.Nodes[0] = node_cur_node_r;
                                    match.Nodes[1] = node_cur_node_p2;
                                    match.Nodes[2] = node_cur_node_r1;
                                    match.Nodes[3] = node_cur_node_p1;
                                    match.Nodes[4] = node_cur_node_r2;
                                    match.Edges[0] = edge_cur_edge_req;
                                    match.Edges[1] = edge_cur_edge_hb;
                                    match.Edges[2] = edge_cur_edge_b;
                                    matchesList.CommitMatch();
                                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                    {
                                        node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_b);
                                        node_cur_node_r1.MoveInHeadAfter(edge_cur_edge_req);
                                        graph.MoveHeadAfter(edge_cur_edge_hb);
                                        graph.MoveHeadAfter(node_cur_node_r);
                                        node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                                        node_cur_node_r1.isMatched = node_cur_node_r1_prevIsMatched;
                                        node_cur_node_r.isMatched = node_cur_node_r_prevIsMatched;
                                        return matches;
                                    }
                                }
                                while( (edge_cur_edge_b = edge_cur_edge_b.inNext) != edge_head_edge_b );
                            }
                        }
                        while( (edge_cur_edge_req = edge_cur_edge_req.inNext) != edge_head_edge_req );
                    }
                    node_cur_node_p1.isMatched = node_cur_node_p1_prevIsMatched;
                    node_cur_node_r1.isMatched = node_cur_node_r1_prevIsMatched;
                }
                node_cur_node_r.isMatched = node_cur_node_r_prevIsMatched;
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
            actions.Add("aux_attachResource", (LGSPAction) Action_aux_attachResource.Instance);
            actions.Add("blockedRule", (LGSPAction) Action_blockedRule.Instance);
            actions.Add("giveRule", (LGSPAction) Action_giveRule.Instance);
            actions.Add("ignoreRule", (LGSPAction) Action_ignoreRule.Instance);
            actions.Add("killRule", (LGSPAction) Action_killRule.Instance);
            actions.Add("mountRule", (LGSPAction) Action_mountRule.Instance);
            actions.Add("newRule", (LGSPAction) Action_newRule.Instance);
            actions.Add("passRule", (LGSPAction) Action_passRule.Instance);
            actions.Add("releaseRule", (LGSPAction) Action_releaseRule.Instance);
            actions.Add("releaseStarRule", (LGSPAction) Action_releaseStarRule.Instance);
            actions.Add("requestRule", (LGSPAction) Action_requestRule.Instance);
            actions.Add("requestSimpleRule", (LGSPAction) Action_requestSimpleRule.Instance);
            actions.Add("requestStarRule", (LGSPAction) Action_requestStarRule.Instance);
            actions.Add("takeRule", (LGSPAction) Action_takeRule.Instance);
            actions.Add("unlockRule", (LGSPAction) Action_unlockRule.Instance);
            actions.Add("unmountRule", (LGSPAction) Action_unmountRule.Instance);
            actions.Add("waitingRule", (LGSPAction) Action_waitingRule.Instance);
        }

        public override String Name { get { return "MutexPimpedActions"; } }
        public override String ModelMD5Hash { get { return "0ecfadd9bae7c1bee09ddc57f323923f"; } }
    }
}