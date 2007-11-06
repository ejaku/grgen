using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.MutexPimped;

namespace de.unika.ipd.grGen.actions.MutexPimped
{
	public class Rule_takeRule : LGSPRulePattern
	{
		private static Rule_takeRule instance = null;
		public static Rule_takeRule Instance { get { if (instance==null) instance = new Rule_takeRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] edge_t_AllowedTypes = null;
		public static ITypeFramework[] edge_req_AllowedTypes = null;
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
			inputs = new IType[] { };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p = match.nodes[ (int) NodeNums.@p - 1 ];
			LGSPEdge edge_t = match.edges[ (int) EdgeNums.@t - 1 ];
			LGSPEdge edge_req = match.edges[ (int) EdgeNums.@req - 1 ];
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
			graph.Remove(edge_t);
			graph.Remove(edge_req);
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

	public class Rule_unmountRule : LGSPRulePattern
	{
		private static Rule_unmountRule instance = null;
		public static Rule_unmountRule Instance { get { if (instance==null) instance = new Rule_unmountRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] edge_t_AllowedTypes = null;
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_unlockRule : LGSPRulePattern
	{
		private static Rule_unlockRule instance = null;
		public static Rule_unlockRule Instance { get { if (instance==null) instance = new Rule_unlockRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] edge_b_AllowedTypes = null;
		public static ITypeFramework[] edge_hb_AllowedTypes = null;
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_ignoreRule : LGSPRulePattern
	{
		private static Rule_ignoreRule instance = null;
		public static Rule_ignoreRule Instance { get { if (instance==null) instance = new Rule_ignoreRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] edge_b_AllowedTypes = null;
		public static bool[] edge_b_IsAllowedType = null;
		public static ITypeFramework[] neg_0_node_m_AllowedTypes = null;
		public static bool[] neg_0_node_m_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge_hb_AllowedTypes = null;
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
				new PatternNode[] { node_p, neg_0_node_m }, 
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_aux_attachResource : LGSPRulePattern
	{
		private static Rule_aux_attachResource instance = null;
		public static Rule_aux_attachResource Instance { get { if (instance==null) instance = new Rule_aux_attachResource(); return instance; } }

		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] neg_0_node_r_AllowedTypes = null;
		public static bool[] neg_0_node_r_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge__edge0_AllowedTypes = null;
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_requestStarRule : LGSPRulePattern
	{
		private static Rule_requestStarRule instance = null;
		public static Rule_requestStarRule Instance { get { if (instance==null) instance = new Rule_requestStarRule(); return instance; } }

		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static ITypeFramework[] node_r1_AllowedTypes = null;
		public static ITypeFramework[] node_r2_AllowedTypes = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static ITypeFramework[] edge_n_AllowedTypes = null;
		public static ITypeFramework[] edge_h2_AllowedTypes = null;
		public static ITypeFramework[] edge_h1_AllowedTypes = null;
		public static bool[] edge_n_IsAllowedType = null;
		public static bool[] edge_h2_IsAllowedType = null;
		public static bool[] edge_h1_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge_req_AllowedTypes = null;
		public static bool[] neg_0_edge_req_IsAllowedType = null;

		public enum NodeNums { @p1  = 1, @p2, @r1, @r2, };
		public enum EdgeNums { @n = 1, @h2, @h1, };

		private Rule_requestStarRule()
		{
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p2, node_p1, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h2 = new PatternEdge(node_r2, node_p2, (int) EdgeTypes.@held_by, "edge_h2", edge_h2_AllowedTypes, edge_h2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h1 = new PatternEdge(node_r1, node_p1, (int) EdgeTypes.@held_by, "edge_h1", edge_h1_AllowedTypes, edge_h1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p1, node_p2, node_r1, node_r2 }, 
				new PatternEdge[] { edge_n, edge_h2, edge_h1 }, 
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
					false, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new IType[] { };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_r2 = match.nodes[ (int) NodeNums.@r2 - 1 ];
			LGSPEdge edge_req = graph.AddEdge(EdgeType_request.typeVar, node_p1, node_r2);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_r2 = match.nodes[ (int) NodeNums.@r2 - 1 ];
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

	public class Rule_newRule : LGSPRulePattern
	{
		private static Rule_newRule instance = null;
		public static Rule_newRule Instance { get { if (instance==null) instance = new Rule_newRule(); return instance; } }

		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static ITypeFramework[] edge_n_AllowedTypes = null;
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
					false, }
			);

			negativePatternGraphs = new PatternGraph[] {};
			inputs = new IType[] { };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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

	public class Rule_passRule : LGSPRulePattern
	{
		private static Rule_passRule instance = null;
		public static Rule_passRule Instance { get { if (instance==null) instance = new Rule_passRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static ITypeFramework[] edge__edge0_AllowedTypes = null;
		public static ITypeFramework[] edge_n_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static bool[] edge_n_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge_req_AllowedTypes = null;
		public static bool[] neg_0_edge_req_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p1, @p2, };
		public enum EdgeNums { @_edge0 = 1, @n, };

		private Rule_passRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_r, node_p1, (int) EdgeTypes.@token, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p1, node_p2 }, 
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
			inputs = new IType[] { };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
			NodeCost = new float[] { 1.0F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { new float[] { 1.0F, 5.5F, }, };
			NegEdgeCost = new float[][] { new float[] { 5.5F, }, };
		}
	}
#endif

	public class Rule_releaseStarRule : LGSPRulePattern
	{
		private static Rule_releaseStarRule instance = null;
		public static Rule_releaseStarRule Instance { get { if (instance==null) instance = new Rule_releaseStarRule(); return instance; } }

		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static ITypeFramework[] node_r1_AllowedTypes = null;
		public static ITypeFramework[] node_r2_AllowedTypes = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static ITypeFramework[] edge_rq_AllowedTypes = null;
		public static ITypeFramework[] edge_h2_AllowedTypes = null;
		public static ITypeFramework[] edge_h1_AllowedTypes = null;
		public static bool[] edge_rq_IsAllowedType = null;
		public static bool[] edge_h2_IsAllowedType = null;
		public static bool[] edge_h1_IsAllowedType = null;

		public enum NodeNums { @p1  = 1, @p2, @r1, @r2, };
		public enum EdgeNums { @rq = 1, @h2, @h1, };

		private Rule_releaseStarRule()
		{
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rq = new PatternEdge(node_p1, node_r1, (int) EdgeTypes.@request, "edge_rq", edge_rq_AllowedTypes, edge_rq_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h2 = new PatternEdge(node_r2, node_p2, (int) EdgeTypes.@held_by, "edge_h2", edge_h2_AllowedTypes, edge_h2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_h1 = new PatternEdge(node_r1, node_p2, (int) EdgeTypes.@held_by, "edge_h1", edge_h1_AllowedTypes, edge_h1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p1, node_p2, node_r1, node_r2 }, 
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_killRule : LGSPRulePattern
	{
		private static Rule_killRule instance = null;
		public static Rule_killRule Instance { get { if (instance==null) instance = new Rule_killRule(); return instance; } }

		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] edge_n2_AllowedTypes = null;
		public static ITypeFramework[] edge_n1_AllowedTypes = null;
		public static bool[] edge_n2_IsAllowedType = null;
		public static bool[] edge_n1_IsAllowedType = null;

		public enum NodeNums { @p1  = 1, @p2, @p, };
		public enum EdgeNums { @n2 = 1, @n1, };

		private Rule_killRule()
		{
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p = new PatternNode((int) NodeTypes.@Process, "node_p", node_p_AllowedTypes, node_p_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n2 = new PatternEdge(node_p, node_p2, (int) EdgeTypes.@next, "edge_n2", edge_n2_AllowedTypes, edge_n2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n1 = new PatternEdge(node_p1, node_p, (int) EdgeTypes.@next, "edge_n1", edge_n1_AllowedTypes, edge_n1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p1, node_p2, node_p }, 
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
			inputs = new IType[] { };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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

	public class Rule_waitingRule : LGSPRulePattern
	{
		private static Rule_waitingRule instance = null;
		public static Rule_waitingRule Instance { get { if (instance==null) instance = new Rule_waitingRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static ITypeFramework[] node_r1_AllowedTypes = null;
		public static ITypeFramework[] node_r2_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_r1_IsAllowedType = null;
		public static bool[] node_r2_IsAllowedType = null;
		public static ITypeFramework[] edge_req_AllowedTypes = null;
		public static ITypeFramework[] edge_b_AllowedTypes = null;
		public static ITypeFramework[] edge_hb_AllowedTypes = null;
		public static bool[] edge_req_IsAllowedType = null;
		public static bool[] edge_b_IsAllowedType = null;
		public static bool[] edge_hb_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p1, @p2, @r1, @r2, };
		public enum EdgeNums { @req = 1, @b, @hb, };

		private Rule_waitingRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r1 = new PatternNode((int) NodeTypes.@Resource, "node_r1", node_r1_AllowedTypes, node_r1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_r2 = new PatternNode((int) NodeTypes.@Resource, "node_r2", node_r2_AllowedTypes, node_r2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p2, node_r1, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_b = new PatternEdge(node_r2, node_p1, (int) EdgeTypes.@blocked, "edge_b", edge_b_AllowedTypes, edge_b_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r1, node_p1, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p1, node_p2, node_r1, node_r2 }, 
				new PatternEdge[] { edge_req, edge_b, edge_hb }, 
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
			inputs = new IType[] { };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
			NodeCost = new float[] { 1.0F, 5.5F, 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_releaseRule : LGSPRulePattern
	{
		private static Rule_releaseRule instance = null;
		public static Rule_releaseRule Instance { get { if (instance==null) instance = new Rule_releaseRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] edge_hb_AllowedTypes = null;
		public static bool[] edge_hb_IsAllowedType = null;
		public static ITypeFramework[] neg_0_node_m_AllowedTypes = null;
		public static bool[] neg_0_node_m_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge_req_AllowedTypes = null;
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
					false, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, };
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_requestSimpleRule : LGSPRulePattern
	{
		private static Rule_requestSimpleRule instance = null;
		public static Rule_requestSimpleRule Instance { get { if (instance==null) instance = new Rule_requestSimpleRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] edge_t_AllowedTypes = null;
		public static bool[] edge_t_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge_req_AllowedTypes = null;
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_mountRule : LGSPRulePattern
	{
		private static Rule_mountRule instance = null;
		public static Rule_mountRule Instance { get { if (instance==null) instance = new Rule_mountRule(); return instance; } }

		public static ITypeFramework[] node_p_AllowedTypes = null;
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_blockedRule : LGSPRulePattern
	{
		private static Rule_blockedRule instance = null;
		public static Rule_blockedRule Instance { get { if (instance==null) instance = new Rule_blockedRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static ITypeFramework[] edge_req_AllowedTypes = null;
		public static ITypeFramework[] edge_hb_AllowedTypes = null;
		public static bool[] edge_req_IsAllowedType = null;
		public static bool[] edge_hb_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p1, @p2, };
		public enum EdgeNums { @req = 1, @hb, };

		private Rule_blockedRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_req = new PatternEdge(node_p1, node_r, (int) EdgeTypes.@request, "edge_req", edge_req_AllowedTypes, edge_req_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_hb = new PatternEdge(node_r, node_p2, (int) EdgeTypes.@held_by, "edge_hb", edge_hb_AllowedTypes, edge_hb_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p1, node_p2 }, 
				new PatternEdge[] { edge_req, edge_hb }, 
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
			inputs = new IType[] { };
			outputs = new IType[] { };
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
			NodeCost = new float[] { 1.0F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif

	public class Rule_requestRule : LGSPRulePattern
	{
		private static Rule_requestRule instance = null;
		public static Rule_requestRule Instance { get { if (instance==null) instance = new Rule_requestRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p_IsAllowedType = null;
		public static ITypeFramework[] neg_0_edge_hb_AllowedTypes = null;
		public static bool[] neg_0_edge_hb_IsAllowedType = null;
		public static ITypeFramework[] neg_1_node_m_AllowedTypes = null;
		public static bool[] neg_1_node_m_IsAllowedType = null;
		public static ITypeFramework[] neg_1_edge_req_AllowedTypes = null;
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
					false, }
			);
			}

			negativePatternGraphs = new PatternGraph[] {negPattern_0, negPattern_1, };
			inputs = new IType[] { };
			outputs = new IType[] { };
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

	public class Rule_giveRule : LGSPRulePattern
	{
		private static Rule_giveRule instance = null;
		public static Rule_giveRule Instance { get { if (instance==null) instance = new Rule_giveRule(); return instance; } }

		public static ITypeFramework[] node_r_AllowedTypes = null;
		public static ITypeFramework[] node_p1_AllowedTypes = null;
		public static ITypeFramework[] node_p2_AllowedTypes = null;
		public static bool[] node_r_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static ITypeFramework[] edge_rel_AllowedTypes = null;
		public static ITypeFramework[] edge_n_AllowedTypes = null;
		public static bool[] edge_rel_IsAllowedType = null;
		public static bool[] edge_n_IsAllowedType = null;

		public enum NodeNums { @r  = 1, @p1, @p2, };
		public enum EdgeNums { @rel = 1, @n, };

		private Rule_giveRule()
		{
			PatternNode node_r = new PatternNode((int) NodeTypes.@Resource, "node_r", node_r_AllowedTypes, node_r_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_rel = new PatternEdge(node_r, node_p1, (int) EdgeTypes.@release, "edge_rel", edge_rel_AllowedTypes, edge_rel_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge_n = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@next, "edge_n", edge_n_AllowedTypes, edge_n_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_r, node_p1, node_p2 }, 
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
			inputs = new IType[] { };
			outputs = new IType[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
			LGSPNode node_r = match.nodes[ (int) NodeNums.@r - 1 ];
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
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
            for(LGSPEdge edge_head_edge_req = graph.edgesByTypeHeads[0], edge_cur_edge_req = edge_head_edge_req.typeNext; edge_cur_edge_req != edge_head_edge_req; edge_cur_edge_req = edge_cur_edge_req.typeNext)
            {
                // ImplicitTarget(edge_req -> node_r:Resource)
                LGSPNode node_cur_node_r = edge_cur_edge_req.target;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_req_2;
                // ExtendOutgoing(node_r -> edge_t:token)
                LGSPEdge edge_head_edge_t = node_cur_node_r.outhead;
                if(edge_head_edge_t != null)
                {
                    LGSPEdge edge_cur_edge_t = edge_head_edge_t;
                    do
                    {
                        if(!EdgeType_token.isMyType[edge_cur_edge_t.type.typeID]) continue;
                        // ImplicitTarget(edge_t -> node_p:Process)
                        LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                        if(!NodeType_Process.isMyType[node_cur_node_p.type.typeID]) goto contunmap_edge_cur_edge_t_6;
                        if(edge_cur_edge_req.source != node_cur_node_p) goto contunmap_edge_cur_edge_t_6;
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.nodes[0] = node_cur_node_r;
                        match.nodes[1] = node_cur_node_p;
                        match.edges[0] = edge_cur_edge_t;
                        match.edges[1] = edge_cur_edge_req;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            graph.MoveHeadAfter(edge_cur_edge_req);
                            node_cur_node_r.MoveOutHeadAfter(edge_cur_edge_t);
                            return matches;
                        }
contunmap_edge_cur_edge_t_6:;
                        // Tail ExtendOutgoing(edge_cur_edge_t)
                    }
                    while((edge_cur_edge_t = edge_cur_edge_t.outNext) != edge_head_edge_t);
                }
contunmap_edge_cur_edge_req_2:;
                // Tail of Lookup(edge_cur_edge_req)
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
            for(LGSPEdge edge_head_edge_t = graph.edgesByTypeHeads[5], edge_cur_edge_t = edge_head_edge_t.typeNext; edge_cur_edge_t != edge_head_edge_t; edge_cur_edge_t = edge_cur_edge_t.typeNext)
            {
                // ImplicitSource(edge_t -> node_r:Resource)
                LGSPNode node_cur_node_r = edge_cur_edge_t.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_t_12;
                // ImplicitTarget(edge_t -> node_p:Process)
                LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.typeID]) goto contunmap_node_cur_node_r_14;
                LGSPMatch match = matchesList.GetNewMatch();
                match.nodes[0] = node_cur_node_r;
                match.nodes[1] = node_cur_node_p;
                match.edges[0] = edge_cur_edge_t;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_t);
                    return matches;
                }
contunmap_node_cur_node_r_14:;
contunmap_edge_cur_edge_t_12:;
                // Tail of Lookup(edge_cur_edge_t)
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
            for(LGSPEdge edge_head_edge_b = graph.edgesByTypeHeads[3], edge_cur_edge_b = edge_head_edge_b.typeNext; edge_cur_edge_b != edge_head_edge_b; edge_cur_edge_b = edge_cur_edge_b.typeNext)
            {
                // ImplicitSource(edge_b -> node_r:Resource)
                LGSPNode node_cur_node_r = edge_cur_edge_b.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_b_20;
                // ImplicitTarget(edge_b -> node_p:Process)
                LGSPNode node_cur_node_p = edge_cur_edge_b.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.typeID]) goto contunmap_node_cur_node_r_22;
                // ExtendOutgoing(node_r -> edge_hb:held_by)
                LGSPEdge edge_head_edge_hb = node_cur_node_r.outhead;
                if(edge_head_edge_hb != null)
                {
                    LGSPEdge edge_cur_edge_hb = edge_head_edge_hb;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_edge_hb.type.typeID]) continue;
                        if(edge_cur_edge_hb.target != node_cur_node_p) continue;
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.nodes[0] = node_cur_node_r;
                        match.nodes[1] = node_cur_node_p;
                        match.edges[0] = edge_cur_edge_b;
                        match.edges[1] = edge_cur_edge_hb;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            graph.MoveHeadAfter(edge_cur_edge_b);
                            node_cur_node_r.MoveOutHeadAfter(edge_cur_edge_hb);
                            return matches;
                        }
                        // Tail ExtendOutgoing(edge_cur_edge_hb)
                    }
                    while((edge_cur_edge_hb = edge_cur_edge_hb.outNext) != edge_head_edge_hb);
                }
contunmap_node_cur_node_r_22:;
contunmap_edge_cur_edge_b_20:;
                // Tail of Lookup(edge_cur_edge_b)
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
            for(LGSPEdge edge_head_edge_b = graph.edgesByTypeHeads[3], edge_cur_edge_b = edge_head_edge_b.typeNext; edge_cur_edge_b != edge_head_edge_b; edge_cur_edge_b = edge_cur_edge_b.typeNext)
            {
                // ImplicitSource(edge_b -> node_r:Resource)
                LGSPNode node_cur_node_r = edge_cur_edge_b.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_b_30;
                // ImplicitTarget(edge_b -> node_p:Process)
                LGSPNode node_cur_node_p = edge_cur_edge_b.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.typeID]) goto contunmap_node_cur_node_r_32;
                // NegativePattern
                // ExtendIncoming(node_p -> neg_0_edge_hb:held_by)
                LGSPEdge edge_head_neg_0_edge_hb = node_cur_node_p.inhead;
                if(edge_head_neg_0_edge_hb != null)
                {
                    LGSPEdge edge_cur_neg_0_edge_hb = edge_head_neg_0_edge_hb;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge_hb.type.typeID]) continue;
                        // ImplicitSource(neg_0_edge_hb -> neg_0_node_m:Resource)
                        LGSPNode node_cur_neg_0_node_m = edge_cur_neg_0_edge_hb.source;
                        if(!NodeType_Resource.isMyType[node_cur_neg_0_node_m.type.typeID]) goto contunmap_edge_cur_neg_0_edge_hb_40;
                        goto contunmap_node_cur_node_p_34;
contunmap_edge_cur_neg_0_edge_hb_40:;
                        // Tail ExtendIncoming(edge_cur_neg_0_edge_hb)
                    }
                    while((edge_cur_neg_0_edge_hb = edge_cur_neg_0_edge_hb.inNext) != edge_head_neg_0_edge_hb);
                }
                // End of NegativePattern
                LGSPMatch match = matchesList.GetNewMatch();
                match.nodes[0] = node_cur_node_r;
                match.nodes[1] = node_cur_node_p;
                match.edges[0] = edge_cur_edge_b;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_b);
                    return matches;
                }
contunmap_node_cur_node_p_34:;
contunmap_node_cur_node_r_32:;
contunmap_edge_cur_edge_b_30:;
                // Tail of Lookup(edge_cur_edge_b)
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
            for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[2], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
            {
                // NegativePattern
                // ExtendIncoming(node_p -> neg_0_edge__edge0:held_by)
                LGSPEdge edge_head_neg_0_edge__edge0 = node_cur_node_p.inhead;
                if(edge_head_neg_0_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_neg_0_edge__edge0 = edge_head_neg_0_edge__edge0;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge__edge0.type.typeID]) continue;
                        // ImplicitSource(neg_0_edge__edge0 -> neg_0_node_r:Resource)
                        LGSPNode node_cur_neg_0_node_r = edge_cur_neg_0_edge__edge0.source;
                        if(!NodeType_Resource.isMyType[node_cur_neg_0_node_r.type.typeID]) goto contunmap_edge_cur_neg_0_edge__edge0_52;
                        goto contunmap_node_cur_node_p_46;
contunmap_edge_cur_neg_0_edge__edge0_52:;
                        // Tail ExtendIncoming(edge_cur_neg_0_edge__edge0)
                    }
                    while((edge_cur_neg_0_edge__edge0 = edge_cur_neg_0_edge__edge0.inNext) != edge_head_neg_0_edge__edge0);
                }
                // End of NegativePattern
                LGSPMatch match = matchesList.GetNewMatch();
                match.nodes[0] = node_cur_node_p;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(node_cur_node_p);
                    return matches;
                }
contunmap_node_cur_node_p_46:;
                // Tail of Lookup(node_cur_node_p)
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
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[4], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // ImplicitTarget(edge_n -> node_p1:Process)
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_n_58;
                node_cur_node_p1.mappedTo = 1;
                // ImplicitSource(edge_n -> node_p2:Process)
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_node_cur_node_p1_60;
                if(node_cur_node_p2.mappedTo != 0) goto cont_node_cur_node_p2_63;
                // ExtendIncoming(node_p1 -> edge_h1:held_by)
                LGSPEdge edge_head_edge_h1 = node_cur_node_p1.inhead;
                if(edge_head_edge_h1 != null)
                {
                    LGSPEdge edge_cur_edge_h1 = edge_head_edge_h1;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_edge_h1.type.typeID]) continue;
                        edge_cur_edge_h1.mappedTo = 3;
                        // ImplicitSource(edge_h1 -> node_r1:Resource)
                        LGSPNode node_cur_node_r1 = edge_cur_edge_h1.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r1.type.typeID]) goto contunmap_edge_cur_edge_h1_64;
                        node_cur_node_r1.mappedTo = 3;
                        // ExtendIncoming(node_p2 -> edge_h2:held_by)
                        LGSPEdge edge_head_edge_h2 = node_cur_node_p2.inhead;
                        if(edge_head_edge_h2 != null)
                        {
                            LGSPEdge edge_cur_edge_h2 = edge_head_edge_h2;
                            do
                            {
                                if(!EdgeType_held_by.isMyType[edge_cur_edge_h2.type.typeID]) continue;
                                if(edge_cur_edge_h2.mappedTo != 0) goto cont_edge_cur_edge_h2_69;
                                // ImplicitSource(edge_h2 -> node_r2:Resource)
                                LGSPNode node_cur_node_r2 = edge_cur_edge_h2.source;
                                if(!NodeType_Resource.isMyType[node_cur_node_r2.type.typeID]) goto contunmap_edge_cur_edge_h2_68;
                                if(node_cur_node_r2.mappedTo != 0) goto cont_node_cur_node_r2_71;
                                // NegativePattern
                                // ExtendOutgoing(node_p1 -> neg_0_edge_req:request)
                                LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p1.outhead;
                                if(edge_head_neg_0_edge_req != null)
                                {
                                    LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                                    do
                                    {
                                        if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.typeID]) continue;
                                        if(edge_cur_neg_0_edge_req.target != node_cur_node_r2) continue;
                                        goto contunmap_node_cur_node_r2_70;
                                        // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                                    }
                                    while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                                }
                                // End of NegativePattern
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.nodes[0] = node_cur_node_p1;
                                match.nodes[1] = node_cur_node_p2;
                                match.nodes[2] = node_cur_node_r1;
                                match.nodes[3] = node_cur_node_r2;
                                match.edges[0] = edge_cur_edge_n;
                                match.edges[1] = edge_cur_edge_h2;
                                match.edges[2] = edge_cur_edge_h1;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_r1.mappedTo = 0;
                                    edge_cur_edge_h1.mappedTo = 0;
                                    node_cur_node_p1.mappedTo = 0;
                                    graph.MoveHeadAfter(edge_cur_edge_n);
                                    node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_h1);
                                    node_cur_node_p2.MoveInHeadAfter(edge_cur_edge_h2);
                                    return matches;
                                }
contunmap_node_cur_node_r2_70:;
cont_node_cur_node_r2_71:;
contunmap_edge_cur_edge_h2_68:;
cont_edge_cur_edge_h2_69:;
                                // Tail ExtendIncoming(edge_cur_edge_h2)
                            }
                            while((edge_cur_edge_h2 = edge_cur_edge_h2.inNext) != edge_head_edge_h2);
                        }
                        node_cur_node_r1.mappedTo = 0;
contunmap_edge_cur_edge_h1_64:;
                        edge_cur_edge_h1.mappedTo = 0;
                        // Tail ExtendIncoming(edge_cur_edge_h1)
                    }
                    while((edge_cur_edge_h1 = edge_cur_edge_h1.inNext) != edge_head_edge_h1);
                }
cont_node_cur_node_p2_63:;
contunmap_node_cur_node_p1_60:;
                node_cur_node_p1.mappedTo = 0;
contunmap_edge_cur_edge_n_58:;
                // Tail of Lookup(edge_cur_edge_n)
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
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[4], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // ImplicitSource(edge_n -> node_p1:Process)
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_n_82;
                node_cur_node_p1.mappedTo = 1;
                // ImplicitTarget(edge_n -> node_p2:Process)
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_node_cur_node_p1_84;
                if(node_cur_node_p2.mappedTo != 0) goto cont_node_cur_node_p2_87;
                LGSPMatch match = matchesList.GetNewMatch();
                match.nodes[0] = node_cur_node_p1;
                match.nodes[1] = node_cur_node_p2;
                match.edges[0] = edge_cur_edge_n;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    node_cur_node_p1.mappedTo = 0;
                    graph.MoveHeadAfter(edge_cur_edge_n);
                    return matches;
                }
cont_node_cur_node_p2_87:;
contunmap_node_cur_node_p1_84:;
                node_cur_node_p1.mappedTo = 0;
contunmap_edge_cur_edge_n_82:;
                // Tail of Lookup(edge_cur_edge_n)
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
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[4], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // ImplicitSource(edge_n -> node_p1:Process)
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_n_90;
                node_cur_node_p1.mappedTo = 2;
                // ImplicitTarget(edge_n -> node_p2:Process)
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_node_cur_node_p1_92;
                if(node_cur_node_p2.mappedTo != 0) goto cont_node_cur_node_p2_95;
                // ExtendIncoming(node_p1 -> edge__edge0:token)
                LGSPEdge edge_head_edge__edge0 = node_cur_node_p1.inhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(!EdgeType_token.isMyType[edge_cur_edge__edge0.type.typeID]) continue;
                        // ImplicitSource(edge__edge0 -> node_r:Resource)
                        LGSPNode node_cur_node_r = edge_cur_edge__edge0.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge__edge0_96;
                        // NegativePattern
                        // ExtendOutgoing(node_p1 -> neg_0_edge_req:request)
                        LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p1.outhead;
                        if(edge_head_neg_0_edge_req != null)
                        {
                            LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                            do
                            {
                                if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.typeID]) continue;
                                if(edge_cur_neg_0_edge_req.target != node_cur_node_r) continue;
                                goto contunmap_node_cur_node_r_98;
                                // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                            }
                            while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                        }
                        // End of NegativePattern
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.nodes[0] = node_cur_node_r;
                        match.nodes[1] = node_cur_node_p1;
                        match.nodes[2] = node_cur_node_p2;
                        match.edges[0] = edge_cur_edge__edge0;
                        match.edges[1] = edge_cur_edge_n;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_p1.mappedTo = 0;
                            graph.MoveHeadAfter(edge_cur_edge_n);
                            node_cur_node_p1.MoveInHeadAfter(edge_cur_edge__edge0);
                            return matches;
                        }
contunmap_node_cur_node_r_98:;
contunmap_edge_cur_edge__edge0_96:;
                        // Tail ExtendIncoming(edge_cur_edge__edge0)
                    }
                    while((edge_cur_edge__edge0 = edge_cur_edge__edge0.inNext) != edge_head_edge__edge0);
                }
cont_node_cur_node_p2_95:;
contunmap_node_cur_node_p1_92:;
                node_cur_node_p1.mappedTo = 0;
contunmap_edge_cur_edge_n_90:;
                // Tail of Lookup(edge_cur_edge_n)
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
            // Lookup(edge_h1:held_by)
            for(LGSPEdge edge_head_edge_h1 = graph.edgesByTypeHeads[1], edge_cur_edge_h1 = edge_head_edge_h1.typeNext; edge_cur_edge_h1 != edge_head_edge_h1; edge_cur_edge_h1 = edge_cur_edge_h1.typeNext)
            {
                edge_cur_edge_h1.mappedTo = 3;
                // ImplicitTarget(edge_h1 -> node_p2:Process)
                LGSPNode node_cur_node_p2 = edge_cur_edge_h1.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_edge_cur_edge_h1_110;
                node_cur_node_p2.mappedTo = 2;
                // ImplicitSource(edge_h1 -> node_r1:Resource)
                LGSPNode node_cur_node_r1 = edge_cur_edge_h1.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r1.type.typeID]) goto contunmap_node_cur_node_p2_112;
                node_cur_node_r1.mappedTo = 3;
                // ExtendIncoming(node_p2 -> edge_h2:held_by)
                LGSPEdge edge_head_edge_h2 = node_cur_node_p2.inhead;
                if(edge_head_edge_h2 != null)
                {
                    LGSPEdge edge_cur_edge_h2 = edge_head_edge_h2;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_edge_h2.type.typeID]) continue;
                        if(edge_cur_edge_h2.mappedTo != 0) goto cont_edge_cur_edge_h2_117;
                        // ImplicitSource(edge_h2 -> node_r2:Resource)
                        LGSPNode node_cur_node_r2 = edge_cur_edge_h2.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r2.type.typeID]) goto contunmap_edge_cur_edge_h2_116;
                        if(node_cur_node_r2.mappedTo != 0) goto cont_node_cur_node_r2_119;
                        // ExtendIncoming(node_r1 -> edge_rq:request)
                        LGSPEdge edge_head_edge_rq = node_cur_node_r1.inhead;
                        if(edge_head_edge_rq != null)
                        {
                            LGSPEdge edge_cur_edge_rq = edge_head_edge_rq;
                            do
                            {
                                if(!EdgeType_request.isMyType[edge_cur_edge_rq.type.typeID]) continue;
                                // ImplicitSource(edge_rq -> node_p1:Process)
                                LGSPNode node_cur_node_p1 = edge_cur_edge_rq.source;
                                if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_rq_120;
                                if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_123;
                                LGSPMatch match = matchesList.GetNewMatch();
                                match.nodes[0] = node_cur_node_p1;
                                match.nodes[1] = node_cur_node_p2;
                                match.nodes[2] = node_cur_node_r1;
                                match.nodes[3] = node_cur_node_r2;
                                match.edges[0] = edge_cur_edge_rq;
                                match.edges[1] = edge_cur_edge_h2;
                                match.edges[2] = edge_cur_edge_h1;
                                matchesList.CommitMatch();
                                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                {
                                    node_cur_node_r1.mappedTo = 0;
                                    node_cur_node_p2.mappedTo = 0;
                                    edge_cur_edge_h1.mappedTo = 0;
                                    graph.MoveHeadAfter(edge_cur_edge_h1);
                                    node_cur_node_p2.MoveInHeadAfter(edge_cur_edge_h2);
                                    node_cur_node_r1.MoveInHeadAfter(edge_cur_edge_rq);
                                    return matches;
                                }
cont_node_cur_node_p1_123:;
contunmap_edge_cur_edge_rq_120:;
                                // Tail ExtendIncoming(edge_cur_edge_rq)
                            }
                            while((edge_cur_edge_rq = edge_cur_edge_rq.inNext) != edge_head_edge_rq);
                        }
cont_node_cur_node_r2_119:;
contunmap_edge_cur_edge_h2_116:;
cont_edge_cur_edge_h2_117:;
                        // Tail ExtendIncoming(edge_cur_edge_h2)
                    }
                    while((edge_cur_edge_h2 = edge_cur_edge_h2.inNext) != edge_head_edge_h2);
                }
                node_cur_node_r1.mappedTo = 0;
contunmap_node_cur_node_p2_112:;
                node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_h1_110:;
                edge_cur_edge_h1.mappedTo = 0;
                // Tail of Lookup(edge_cur_edge_h1)
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
            for(LGSPEdge edge_head_edge_n2 = graph.edgesByTypeHeads[4], edge_cur_edge_n2 = edge_head_edge_n2.typeNext; edge_cur_edge_n2 != edge_head_edge_n2; edge_cur_edge_n2 = edge_cur_edge_n2.typeNext)
            {
                edge_cur_edge_n2.mappedTo = 1;
                // ImplicitTarget(edge_n2 -> node_p2:Process)
                LGSPNode node_cur_node_p2 = edge_cur_edge_n2.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_edge_cur_edge_n2_126;
                node_cur_node_p2.mappedTo = 2;
                // ImplicitSource(edge_n2 -> node_p:Process)
                LGSPNode node_cur_node_p = edge_cur_edge_n2.source;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.typeID]) goto contunmap_node_cur_node_p2_128;
                if(node_cur_node_p.mappedTo != 0) goto cont_node_cur_node_p_131;
                node_cur_node_p.mappedTo = 3;
                // ExtendIncoming(node_p -> edge_n1:next)
                LGSPEdge edge_head_edge_n1 = node_cur_node_p.inhead;
                if(edge_head_edge_n1 != null)
                {
                    LGSPEdge edge_cur_edge_n1 = edge_head_edge_n1;
                    do
                    {
                        if(!EdgeType_next.isMyType[edge_cur_edge_n1.type.typeID]) continue;
                        if(edge_cur_edge_n1.mappedTo != 0) goto cont_edge_cur_edge_n1_133;
                        // ImplicitSource(edge_n1 -> node_p1:Process)
                        LGSPNode node_cur_node_p1 = edge_cur_edge_n1.source;
                        if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_n1_132;
                        if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_135;
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.nodes[0] = node_cur_node_p1;
                        match.nodes[1] = node_cur_node_p2;
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
cont_node_cur_node_p1_135:;
contunmap_edge_cur_edge_n1_132:;
cont_edge_cur_edge_n1_133:;
                        // Tail ExtendIncoming(edge_cur_edge_n1)
                    }
                    while((edge_cur_edge_n1 = edge_cur_edge_n1.inNext) != edge_head_edge_n1);
                }
                node_cur_node_p.mappedTo = 0;
cont_node_cur_node_p_131:;
contunmap_node_cur_node_p2_128:;
                node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge_n2_126:;
                edge_cur_edge_n2.mappedTo = 0;
                // Tail of Lookup(edge_cur_edge_n2)
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
            for(LGSPNode node_head_node_r = graph.nodesByTypeHeads[0], node_cur_node_r = node_head_node_r.typeNext; node_cur_node_r != node_head_node_r; node_cur_node_r = node_cur_node_r.typeNext)
            {
                node_cur_node_r.mappedTo = 1;
                // Lookup(edge_b:blocked)
                for(LGSPEdge edge_head_edge_b = graph.edgesByTypeHeads[3], edge_cur_edge_b = edge_head_edge_b.typeNext; edge_cur_edge_b != edge_head_edge_b; edge_cur_edge_b = edge_cur_edge_b.typeNext)
                {
                    // ImplicitTarget(edge_b -> node_p1:Process)
                    LGSPNode node_cur_node_p1 = edge_cur_edge_b.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_b_140;
                    node_cur_node_p1.mappedTo = 2;
                    // ImplicitSource(edge_b -> node_r2:Resource)
                    LGSPNode node_cur_node_r2 = edge_cur_edge_b.source;
                    if(!NodeType_Resource.isMyType[node_cur_node_r2.type.typeID]) goto contunmap_node_cur_node_p1_142;
                    if(node_cur_node_r2.mappedTo != 0) goto cont_node_cur_node_r2_145;
                    node_cur_node_r2.mappedTo = 5;
                    // ExtendIncoming(node_p1 -> edge_hb:held_by)
                    LGSPEdge edge_head_edge_hb = node_cur_node_p1.inhead;
                    if(edge_head_edge_hb != null)
                    {
                        LGSPEdge edge_cur_edge_hb = edge_head_edge_hb;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_edge_hb.type.typeID]) continue;
                            // ImplicitSource(edge_hb -> node_r1:Resource)
                            LGSPNode node_cur_node_r1 = edge_cur_edge_hb.source;
                            if(!NodeType_Resource.isMyType[node_cur_node_r1.type.typeID]) goto contunmap_edge_cur_edge_hb_146;
                            if(node_cur_node_r1.mappedTo != 0) goto cont_node_cur_node_r1_149;
                            // ExtendIncoming(node_r1 -> edge_req:request)
                            LGSPEdge edge_head_edge_req = node_cur_node_r1.inhead;
                            if(edge_head_edge_req != null)
                            {
                                LGSPEdge edge_cur_edge_req = edge_head_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_edge_req.type.typeID]) continue;
                                    // ImplicitSource(edge_req -> node_p2:Process)
                                    LGSPNode node_cur_node_p2 = edge_cur_edge_req.source;
                                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_edge_cur_edge_req_150;
                                    if(node_cur_node_p2.mappedTo != 0) goto cont_node_cur_node_p2_153;
                                    LGSPMatch match = matchesList.GetNewMatch();
                                    match.nodes[0] = node_cur_node_r;
                                    match.nodes[1] = node_cur_node_p1;
                                    match.nodes[2] = node_cur_node_p2;
                                    match.nodes[3] = node_cur_node_r1;
                                    match.nodes[4] = node_cur_node_r2;
                                    match.edges[0] = edge_cur_edge_req;
                                    match.edges[1] = edge_cur_edge_b;
                                    match.edges[2] = edge_cur_edge_hb;
                                    matchesList.CommitMatch();
                                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                                    {
                                        node_cur_node_r2.mappedTo = 0;
                                        node_cur_node_p1.mappedTo = 0;
                                        node_cur_node_r.mappedTo = 0;
                                        graph.MoveHeadAfter(node_cur_node_r);
                                        graph.MoveHeadAfter(edge_cur_edge_b);
                                        node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_hb);
                                        node_cur_node_r1.MoveInHeadAfter(edge_cur_edge_req);
                                        return matches;
                                    }
cont_node_cur_node_p2_153:;
contunmap_edge_cur_edge_req_150:;
                                    // Tail ExtendIncoming(edge_cur_edge_req)
                                }
                                while((edge_cur_edge_req = edge_cur_edge_req.inNext) != edge_head_edge_req);
                            }
cont_node_cur_node_r1_149:;
contunmap_edge_cur_edge_hb_146:;
                            // Tail ExtendIncoming(edge_cur_edge_hb)
                        }
                        while((edge_cur_edge_hb = edge_cur_edge_hb.inNext) != edge_head_edge_hb);
                    }
                    node_cur_node_r2.mappedTo = 0;
cont_node_cur_node_r2_145:;
contunmap_node_cur_node_p1_142:;
                    node_cur_node_p1.mappedTo = 0;
contunmap_edge_cur_edge_b_140:;
                    // Tail of Lookup(edge_cur_edge_b)
                }
                node_cur_node_r.mappedTo = 0;
                // Tail of Lookup(node_cur_node_r)
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
            for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[1], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
            {
                // ImplicitSource(edge_hb -> node_r:Resource)
                LGSPNode node_cur_node_r = edge_cur_edge_hb.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_hb_156;
                // ImplicitTarget(edge_hb -> node_p:Process)
                LGSPNode node_cur_node_p = edge_cur_edge_hb.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.typeID]) goto contunmap_node_cur_node_r_158;
                // NegativePattern
                // ExtendOutgoing(node_p -> neg_0_edge_req:request)
                LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p.outhead;
                if(edge_head_neg_0_edge_req != null)
                {
                    LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                    do
                    {
                        if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.typeID]) continue;
                        // ImplicitTarget(neg_0_edge_req -> neg_0_node_m:Resource)
                        LGSPNode node_cur_neg_0_node_m = edge_cur_neg_0_edge_req.target;
                        if(!NodeType_Resource.isMyType[node_cur_neg_0_node_m.type.typeID]) goto contunmap_edge_cur_neg_0_edge_req_166;
                        goto contunmap_node_cur_node_p_160;
contunmap_edge_cur_neg_0_edge_req_166:;
                        // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                    }
                    while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                }
                // End of NegativePattern
                LGSPMatch match = matchesList.GetNewMatch();
                match.nodes[0] = node_cur_node_r;
                match.nodes[1] = node_cur_node_p;
                match.edges[0] = edge_cur_edge_hb;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_hb);
                    return matches;
                }
contunmap_node_cur_node_p_160:;
contunmap_node_cur_node_r_158:;
contunmap_edge_cur_edge_hb_156:;
                // Tail of Lookup(edge_cur_edge_hb)
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
            for(LGSPEdge edge_head_edge_t = graph.edgesByTypeHeads[5], edge_cur_edge_t = edge_head_edge_t.typeNext; edge_cur_edge_t != edge_head_edge_t; edge_cur_edge_t = edge_cur_edge_t.typeNext)
            {
                // ImplicitSource(edge_t -> node_r:Resource)
                LGSPNode node_cur_node_r = edge_cur_edge_t.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_t_172;
                // ImplicitTarget(edge_t -> node_p:Process)
                LGSPNode node_cur_node_p = edge_cur_edge_t.target;
                if(!NodeType_Process.isMyType[node_cur_node_p.type.typeID]) goto contunmap_node_cur_node_r_174;
                // NegativePattern
                // ExtendOutgoing(node_p -> neg_0_edge_req:request)
                LGSPEdge edge_head_neg_0_edge_req = node_cur_node_p.outhead;
                if(edge_head_neg_0_edge_req != null)
                {
                    LGSPEdge edge_cur_neg_0_edge_req = edge_head_neg_0_edge_req;
                    do
                    {
                        if(!EdgeType_request.isMyType[edge_cur_neg_0_edge_req.type.typeID]) continue;
                        if(edge_cur_neg_0_edge_req.target != node_cur_node_r) continue;
                        goto contunmap_node_cur_node_p_176;
                        // Tail ExtendOutgoing(edge_cur_neg_0_edge_req)
                    }
                    while((edge_cur_neg_0_edge_req = edge_cur_neg_0_edge_req.outNext) != edge_head_neg_0_edge_req);
                }
                // End of NegativePattern
                LGSPMatch match = matchesList.GetNewMatch();
                match.nodes[0] = node_cur_node_r;
                match.nodes[1] = node_cur_node_p;
                match.edges[0] = edge_cur_edge_t;
                matchesList.CommitMatch();
                if(maxMatches > 0 && matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_edge_t);
                    return matches;
                }
contunmap_node_cur_node_p_176:;
contunmap_node_cur_node_r_174:;
contunmap_edge_cur_edge_t_172:;
                // Tail of Lookup(edge_cur_edge_t)
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
            for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[2], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
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
            for(LGSPEdge edge_head_edge_hb = graph.edgesByTypeHeads[1], edge_cur_edge_hb = edge_head_edge_hb.typeNext; edge_cur_edge_hb != edge_head_edge_hb; edge_cur_edge_hb = edge_cur_edge_hb.typeNext)
            {
                // ImplicitSource(edge_hb -> node_r:Resource)
                LGSPNode node_cur_node_r = edge_cur_edge_hb.source;
                if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_hb_192;
                // ImplicitTarget(edge_hb -> node_p2:Process)
                LGSPNode node_cur_node_p2 = edge_cur_edge_hb.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_node_cur_node_r_194;
                node_cur_node_p2.mappedTo = 3;
                // ExtendIncoming(node_r -> edge_req:request)
                LGSPEdge edge_head_edge_req = node_cur_node_r.inhead;
                if(edge_head_edge_req != null)
                {
                    LGSPEdge edge_cur_edge_req = edge_head_edge_req;
                    do
                    {
                        if(!EdgeType_request.isMyType[edge_cur_edge_req.type.typeID]) continue;
                        // ImplicitSource(edge_req -> node_p1:Process)
                        LGSPNode node_cur_node_p1 = edge_cur_edge_req.source;
                        if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_req_198;
                        if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_201;
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.nodes[0] = node_cur_node_r;
                        match.nodes[1] = node_cur_node_p1;
                        match.nodes[2] = node_cur_node_p2;
                        match.edges[0] = edge_cur_edge_req;
                        match.edges[1] = edge_cur_edge_hb;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_p2.mappedTo = 0;
                            graph.MoveHeadAfter(edge_cur_edge_hb);
                            node_cur_node_r.MoveInHeadAfter(edge_cur_edge_req);
                            return matches;
                        }
cont_node_cur_node_p1_201:;
contunmap_edge_cur_edge_req_198:;
                        // Tail ExtendIncoming(edge_cur_edge_req)
                    }
                    while((edge_cur_edge_req = edge_cur_edge_req.inNext) != edge_head_edge_req);
                }
                node_cur_node_p2.mappedTo = 0;
contunmap_node_cur_node_r_194:;
contunmap_edge_cur_edge_hb_192:;
                // Tail of Lookup(edge_cur_edge_hb)
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
            for(LGSPNode node_head_node_r = graph.nodesByTypeHeads[0], node_cur_node_r = node_head_node_r.typeNext; node_cur_node_r != node_head_node_r; node_cur_node_r = node_cur_node_r.typeNext)
            {
                // Lookup(node_p:Process)
                for(LGSPNode node_head_node_p = graph.nodesByTypeHeads[2], node_cur_node_p = node_head_node_p.typeNext; node_cur_node_p != node_head_node_p; node_cur_node_p = node_cur_node_p.typeNext)
                {
                    // NegativePattern
                    // ExtendOutgoing(node_r -> neg_0_edge_hb:held_by)
                    LGSPEdge edge_head_neg_0_edge_hb = node_cur_node_r.outhead;
                    if(edge_head_neg_0_edge_hb != null)
                    {
                        LGSPEdge edge_cur_neg_0_edge_hb = edge_head_neg_0_edge_hb;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_neg_0_edge_hb.type.typeID]) continue;
                            if(edge_cur_neg_0_edge_hb.target != node_cur_node_p) continue;
                            goto contunmap_node_cur_node_p_206;
                            // Tail ExtendOutgoing(edge_cur_neg_0_edge_hb)
                        }
                        while((edge_cur_neg_0_edge_hb = edge_cur_neg_0_edge_hb.outNext) != edge_head_neg_0_edge_hb);
                    }
                    // End of NegativePattern
                    // NegativePattern
                    // ExtendOutgoing(node_p -> neg_1_edge_req:request)
                    LGSPEdge edge_head_neg_1_edge_req = node_cur_node_p.outhead;
                    if(edge_head_neg_1_edge_req != null)
                    {
                        LGSPEdge edge_cur_neg_1_edge_req = edge_head_neg_1_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_neg_1_edge_req.type.typeID]) continue;
                            // ImplicitTarget(neg_1_edge_req -> neg_1_node_m:Resource)
                            LGSPNode node_cur_neg_1_node_m = edge_cur_neg_1_edge_req.target;
                            if(!NodeType_Resource.isMyType[node_cur_neg_1_node_m.type.typeID]) goto contunmap_edge_cur_neg_1_edge_req_220;
                            goto contunmap_node_cur_node_p_206;
contunmap_edge_cur_neg_1_edge_req_220:;
                            // Tail ExtendOutgoing(edge_cur_neg_1_edge_req)
                        }
                        while((edge_cur_neg_1_edge_req = edge_cur_neg_1_edge_req.outNext) != edge_head_neg_1_edge_req);
                    }
                    // End of NegativePattern
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_r;
                    match.nodes[1] = node_cur_node_p;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(node_cur_node_r);
                        graph.MoveHeadAfter(node_cur_node_p);
                        return matches;
                    }
contunmap_node_cur_node_p_206:;
                    // Tail of Lookup(node_cur_node_p)
                }
                // Tail of Lookup(node_cur_node_r)
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
            // Lookup(edge_n:next)
            for(LGSPEdge edge_head_edge_n = graph.edgesByTypeHeads[4], edge_cur_edge_n = edge_head_edge_n.typeNext; edge_cur_edge_n != edge_head_edge_n; edge_cur_edge_n = edge_cur_edge_n.typeNext)
            {
                // ImplicitSource(edge_n -> node_p1:Process)
                LGSPNode node_cur_node_p1 = edge_cur_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_node_p1.type.typeID]) goto contunmap_edge_cur_edge_n_226;
                node_cur_node_p1.mappedTo = 2;
                // ImplicitTarget(edge_n -> node_p2:Process)
                LGSPNode node_cur_node_p2 = edge_cur_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_node_p2.type.typeID]) goto contunmap_node_cur_node_p1_228;
                if(node_cur_node_p2.mappedTo != 0) goto cont_node_cur_node_p2_231;
                // ExtendIncoming(node_p1 -> edge_rel:release)
                LGSPEdge edge_head_edge_rel = node_cur_node_p1.inhead;
                if(edge_head_edge_rel != null)
                {
                    LGSPEdge edge_cur_edge_rel = edge_head_edge_rel;
                    do
                    {
                        if(!EdgeType_release.isMyType[edge_cur_edge_rel.type.typeID]) continue;
                        // ImplicitSource(edge_rel -> node_r:Resource)
                        LGSPNode node_cur_node_r = edge_cur_edge_rel.source;
                        if(!NodeType_Resource.isMyType[node_cur_node_r.type.typeID]) goto contunmap_edge_cur_edge_rel_232;
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.nodes[0] = node_cur_node_r;
                        match.nodes[1] = node_cur_node_p1;
                        match.nodes[2] = node_cur_node_p2;
                        match.edges[0] = edge_cur_edge_rel;
                        match.edges[1] = edge_cur_edge_n;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_p1.mappedTo = 0;
                            graph.MoveHeadAfter(edge_cur_edge_n);
                            node_cur_node_p1.MoveInHeadAfter(edge_cur_edge_rel);
                            return matches;
                        }
contunmap_edge_cur_edge_rel_232:;
                        // Tail ExtendIncoming(edge_cur_edge_rel)
                    }
                    while((edge_cur_edge_rel = edge_cur_edge_rel.inNext) != edge_head_edge_rel);
                }
cont_node_cur_node_p2_231:;
contunmap_node_cur_node_p1_228:;
                node_cur_node_p1.mappedTo = 0;
contunmap_edge_cur_edge_n_226:;
                // Tail of Lookup(edge_cur_edge_n)
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
            actions.Add("takeRule", (LGSPAction) Action_takeRule.Instance);
            actions.Add("unmountRule", (LGSPAction) Action_unmountRule.Instance);
            actions.Add("unlockRule", (LGSPAction) Action_unlockRule.Instance);
            actions.Add("ignoreRule", (LGSPAction) Action_ignoreRule.Instance);
            actions.Add("aux_attachResource", (LGSPAction) Action_aux_attachResource.Instance);
            actions.Add("requestStarRule", (LGSPAction) Action_requestStarRule.Instance);
            actions.Add("newRule", (LGSPAction) Action_newRule.Instance);
            actions.Add("passRule", (LGSPAction) Action_passRule.Instance);
            actions.Add("releaseStarRule", (LGSPAction) Action_releaseStarRule.Instance);
            actions.Add("killRule", (LGSPAction) Action_killRule.Instance);
            actions.Add("waitingRule", (LGSPAction) Action_waitingRule.Instance);
            actions.Add("releaseRule", (LGSPAction) Action_releaseRule.Instance);
            actions.Add("requestSimpleRule", (LGSPAction) Action_requestSimpleRule.Instance);
            actions.Add("mountRule", (LGSPAction) Action_mountRule.Instance);
            actions.Add("blockedRule", (LGSPAction) Action_blockedRule.Instance);
            actions.Add("requestRule", (LGSPAction) Action_requestRule.Instance);
            actions.Add("giveRule", (LGSPAction) Action_giveRule.Instance);
        }

        public override String Name { get { return "MutexPimpedActions"; } }
        public override String ModelMD5Hash { get { return "0ecfadd9bae7c1bee09ddc57f323923f"; } }
    }
}