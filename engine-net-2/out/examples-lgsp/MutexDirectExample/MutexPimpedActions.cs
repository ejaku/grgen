using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_MutexPimped;

namespace de.unika.ipd.grGen.Action_MutexPimped
{
	public class Rule_aux_attachResource : LGSPRulePattern
	{
		private static Rule_aux_attachResource instance = null;
		public static Rule_aux_attachResource Instance { get { if (instance==null) instance = new Rule_aux_attachResource(); return instance; } }

		public static NodeType[] aux_attachResource_node_p_AllowedTypes = null;
		public static bool[] aux_attachResource_node_p_IsAllowedType = null;
		public enum aux_attachResource_NodeNums { @p, };
		public enum aux_attachResource_EdgeNums { };
		public enum aux_attachResource_SubNums { };
		public enum aux_attachResource_AltNums { };
		public static NodeType[] aux_attachResource_neg_0_node_r_AllowedTypes = null;
		public static bool[] aux_attachResource_neg_0_node_r_IsAllowedType = null;
		public static EdgeType[] aux_attachResource_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] aux_attachResource_neg_0_edge__edge0_IsAllowedType = null;
		public enum aux_attachResource_neg_0_NodeNums { @r, @p, };
		public enum aux_attachResource_neg_0_EdgeNums { @_edge0, };
		public enum aux_attachResource_neg_0_SubNums { };
		public enum aux_attachResource_neg_0_AltNums { };

#if INITIAL_WARMUP
		public Rule_aux_attachResource()
#else
		private Rule_aux_attachResource()
#endif
		{
			name = "aux_attachResource";
			isSubpattern = false;

			PatternGraph aux_attachResource;
			PatternNode aux_attachResource_node_p = new PatternNode((int) NodeTypes.@Process, "aux_attachResource_node_p", "p", aux_attachResource_node_p_AllowedTypes, aux_attachResource_node_p_IsAllowedType, 5.5F, -1);
			PatternGraph aux_attachResource_neg_0;
			PatternNode aux_attachResource_neg_0_node_r = new PatternNode((int) NodeTypes.@Resource, "aux_attachResource_neg_0_node_r", "r", aux_attachResource_neg_0_node_r_AllowedTypes, aux_attachResource_neg_0_node_r_IsAllowedType, 5.5F, -1);
			PatternEdge aux_attachResource_neg_0_edge__edge0 = new PatternEdge(aux_attachResource_neg_0_node_r, aux_attachResource_node_p, (int) EdgeTypes.@held_by, "aux_attachResource_neg_0_edge__edge0", "_edge0", aux_attachResource_neg_0_edge__edge0_AllowedTypes, aux_attachResource_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			aux_attachResource_neg_0 = new PatternGraph(
				"neg_0",
				"aux_attachResource_",
				new PatternNode[] { aux_attachResource_neg_0_node_r, aux_attachResource_node_p }, 
				new PatternEdge[] { aux_attachResource_neg_0_edge__edge0 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			aux_attachResource = new PatternGraph(
				"aux_attachResource",
				"",
				new PatternNode[] { aux_attachResource_node_p }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { aux_attachResource_neg_0,  }, 
				new Condition[] {  }, 
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
			aux_attachResource_node_p.PointOfDefinition = aux_attachResource;
			aux_attachResource_neg_0_node_r.PointOfDefinition = aux_attachResource_neg_0;
			aux_attachResource_neg_0_edge__edge0.PointOfDefinition = aux_attachResource_neg_0;

			patternGraph = aux_attachResource;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) aux_attachResource_NodeNums.@p];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_held_by edge__edge0 = Edge_held_by.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) aux_attachResource_NodeNums.@p];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_held_by edge__edge0 = Edge_held_by.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "r" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge0" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_blockedRule : LGSPRulePattern
	{
		private static Rule_blockedRule instance = null;
		public static Rule_blockedRule Instance { get { if (instance==null) instance = new Rule_blockedRule(); return instance; } }

		public static NodeType[] blockedRule_node_r_AllowedTypes = null;
		public static NodeType[] blockedRule_node_p1_AllowedTypes = null;
		public static NodeType[] blockedRule_node_p2_AllowedTypes = null;
		public static bool[] blockedRule_node_r_IsAllowedType = null;
		public static bool[] blockedRule_node_p1_IsAllowedType = null;
		public static bool[] blockedRule_node_p2_IsAllowedType = null;
		public static EdgeType[] blockedRule_edge_req_AllowedTypes = null;
		public static EdgeType[] blockedRule_edge_hb_AllowedTypes = null;
		public static bool[] blockedRule_edge_req_IsAllowedType = null;
		public static bool[] blockedRule_edge_hb_IsAllowedType = null;
		public enum blockedRule_NodeNums { @r, @p1, @p2, };
		public enum blockedRule_EdgeNums { @req, @hb, };
		public enum blockedRule_SubNums { };
		public enum blockedRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_blockedRule()
#else
		private Rule_blockedRule()
#endif
		{
			name = "blockedRule";
			isSubpattern = false;

			PatternGraph blockedRule;
			PatternNode blockedRule_node_r = new PatternNode((int) NodeTypes.@Resource, "blockedRule_node_r", "r", blockedRule_node_r_AllowedTypes, blockedRule_node_r_IsAllowedType, 1.0F, -1);
			PatternNode blockedRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "blockedRule_node_p1", "p1", blockedRule_node_p1_AllowedTypes, blockedRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode blockedRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "blockedRule_node_p2", "p2", blockedRule_node_p2_AllowedTypes, blockedRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternEdge blockedRule_edge_req = new PatternEdge(blockedRule_node_p1, blockedRule_node_r, (int) EdgeTypes.@request, "blockedRule_edge_req", "req", blockedRule_edge_req_AllowedTypes, blockedRule_edge_req_IsAllowedType, 5.5F, -1);
			PatternEdge blockedRule_edge_hb = new PatternEdge(blockedRule_node_r, blockedRule_node_p2, (int) EdgeTypes.@held_by, "blockedRule_edge_hb", "hb", blockedRule_edge_hb_AllowedTypes, blockedRule_edge_hb_IsAllowedType, 5.5F, -1);
			blockedRule = new PatternGraph(
				"blockedRule",
				"",
				new PatternNode[] { blockedRule_node_r, blockedRule_node_p1, blockedRule_node_p2 }, 
				new PatternEdge[] { blockedRule_edge_req, blockedRule_edge_hb }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			blockedRule_node_r.PointOfDefinition = blockedRule;
			blockedRule_node_p1.PointOfDefinition = blockedRule;
			blockedRule_node_p2.PointOfDefinition = blockedRule;
			blockedRule_edge_req.PointOfDefinition = blockedRule;
			blockedRule_edge_hb.PointOfDefinition = blockedRule;

			patternGraph = blockedRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) blockedRule_NodeNums.@r];
			LGSPNode node_p1 = match.Nodes[ (int) blockedRule_NodeNums.@p1];
			Edge_blocked edge_b = Edge_blocked.CreateEdge(graph, node_r, node_p1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) blockedRule_NodeNums.@r];
			LGSPNode node_p1 = match.Nodes[ (int) blockedRule_NodeNums.@p1];
			Edge_blocked edge_b = Edge_blocked.CreateEdge(graph, node_r, node_p1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "b" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_giveRule : LGSPRulePattern
	{
		private static Rule_giveRule instance = null;
		public static Rule_giveRule Instance { get { if (instance==null) instance = new Rule_giveRule(); return instance; } }

		public static NodeType[] giveRule_node_r_AllowedTypes = null;
		public static NodeType[] giveRule_node_p1_AllowedTypes = null;
		public static NodeType[] giveRule_node_p2_AllowedTypes = null;
		public static bool[] giveRule_node_r_IsAllowedType = null;
		public static bool[] giveRule_node_p1_IsAllowedType = null;
		public static bool[] giveRule_node_p2_IsAllowedType = null;
		public static EdgeType[] giveRule_edge_rel_AllowedTypes = null;
		public static EdgeType[] giveRule_edge_n_AllowedTypes = null;
		public static bool[] giveRule_edge_rel_IsAllowedType = null;
		public static bool[] giveRule_edge_n_IsAllowedType = null;
		public enum giveRule_NodeNums { @r, @p1, @p2, };
		public enum giveRule_EdgeNums { @rel, @n, };
		public enum giveRule_SubNums { };
		public enum giveRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_giveRule()
#else
		private Rule_giveRule()
#endif
		{
			name = "giveRule";
			isSubpattern = false;

			PatternGraph giveRule;
			PatternNode giveRule_node_r = new PatternNode((int) NodeTypes.@Resource, "giveRule_node_r", "r", giveRule_node_r_AllowedTypes, giveRule_node_r_IsAllowedType, 5.5F, -1);
			PatternNode giveRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "giveRule_node_p1", "p1", giveRule_node_p1_AllowedTypes, giveRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode giveRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "giveRule_node_p2", "p2", giveRule_node_p2_AllowedTypes, giveRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternEdge giveRule_edge_rel = new PatternEdge(giveRule_node_r, giveRule_node_p1, (int) EdgeTypes.@release, "giveRule_edge_rel", "rel", giveRule_edge_rel_AllowedTypes, giveRule_edge_rel_IsAllowedType, 1.0F, -1);
			PatternEdge giveRule_edge_n = new PatternEdge(giveRule_node_p1, giveRule_node_p2, (int) EdgeTypes.@next, "giveRule_edge_n", "n", giveRule_edge_n_AllowedTypes, giveRule_edge_n_IsAllowedType, 5.5F, -1);
			giveRule = new PatternGraph(
				"giveRule",
				"",
				new PatternNode[] { giveRule_node_r, giveRule_node_p1, giveRule_node_p2 }, 
				new PatternEdge[] { giveRule_edge_rel, giveRule_edge_n }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			giveRule_node_r.PointOfDefinition = giveRule;
			giveRule_node_p1.PointOfDefinition = giveRule;
			giveRule_node_p2.PointOfDefinition = giveRule;
			giveRule_edge_rel.PointOfDefinition = giveRule;
			giveRule_edge_n.PointOfDefinition = giveRule;

			patternGraph = giveRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) giveRule_NodeNums.@r];
			LGSPNode node_p2 = match.Nodes[ (int) giveRule_NodeNums.@p2];
			LGSPEdge edge_rel = match.Edges[ (int) giveRule_EdgeNums.@rel];
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) giveRule_NodeNums.@r];
			LGSPNode node_p2 = match.Nodes[ (int) giveRule_NodeNums.@p2];
			LGSPEdge edge_rel = match.Edges[ (int) giveRule_EdgeNums.@rel];
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_ignoreRule : LGSPRulePattern
	{
		private static Rule_ignoreRule instance = null;
		public static Rule_ignoreRule Instance { get { if (instance==null) instance = new Rule_ignoreRule(); return instance; } }

		public static NodeType[] ignoreRule_node_r_AllowedTypes = null;
		public static NodeType[] ignoreRule_node_p_AllowedTypes = null;
		public static bool[] ignoreRule_node_r_IsAllowedType = null;
		public static bool[] ignoreRule_node_p_IsAllowedType = null;
		public static EdgeType[] ignoreRule_edge_b_AllowedTypes = null;
		public static bool[] ignoreRule_edge_b_IsAllowedType = null;
		public enum ignoreRule_NodeNums { @r, @p, };
		public enum ignoreRule_EdgeNums { @b, };
		public enum ignoreRule_SubNums { };
		public enum ignoreRule_AltNums { };
		public static NodeType[] ignoreRule_neg_0_node_m_AllowedTypes = null;
		public static bool[] ignoreRule_neg_0_node_m_IsAllowedType = null;
		public static EdgeType[] ignoreRule_neg_0_edge_hb_AllowedTypes = null;
		public static bool[] ignoreRule_neg_0_edge_hb_IsAllowedType = null;
		public enum ignoreRule_neg_0_NodeNums { @m, @p, };
		public enum ignoreRule_neg_0_EdgeNums { @hb, };
		public enum ignoreRule_neg_0_SubNums { };
		public enum ignoreRule_neg_0_AltNums { };

#if INITIAL_WARMUP
		public Rule_ignoreRule()
#else
		private Rule_ignoreRule()
#endif
		{
			name = "ignoreRule";
			isSubpattern = false;

			PatternGraph ignoreRule;
			PatternNode ignoreRule_node_r = new PatternNode((int) NodeTypes.@Resource, "ignoreRule_node_r", "r", ignoreRule_node_r_AllowedTypes, ignoreRule_node_r_IsAllowedType, 1.0F, -1);
			PatternNode ignoreRule_node_p = new PatternNode((int) NodeTypes.@Process, "ignoreRule_node_p", "p", ignoreRule_node_p_AllowedTypes, ignoreRule_node_p_IsAllowedType, 5.5F, -1);
			PatternEdge ignoreRule_edge_b = new PatternEdge(ignoreRule_node_r, ignoreRule_node_p, (int) EdgeTypes.@blocked, "ignoreRule_edge_b", "b", ignoreRule_edge_b_AllowedTypes, ignoreRule_edge_b_IsAllowedType, 5.5F, -1);
			PatternGraph ignoreRule_neg_0;
			PatternNode ignoreRule_neg_0_node_m = new PatternNode((int) NodeTypes.@Resource, "ignoreRule_neg_0_node_m", "m", ignoreRule_neg_0_node_m_AllowedTypes, ignoreRule_neg_0_node_m_IsAllowedType, 5.5F, -1);
			PatternEdge ignoreRule_neg_0_edge_hb = new PatternEdge(ignoreRule_neg_0_node_m, ignoreRule_node_p, (int) EdgeTypes.@held_by, "ignoreRule_neg_0_edge_hb", "hb", ignoreRule_neg_0_edge_hb_AllowedTypes, ignoreRule_neg_0_edge_hb_IsAllowedType, 5.5F, -1);
			ignoreRule_neg_0 = new PatternGraph(
				"neg_0",
				"ignoreRule_",
				new PatternNode[] { ignoreRule_neg_0_node_m, ignoreRule_node_p }, 
				new PatternEdge[] { ignoreRule_neg_0_edge_hb }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			ignoreRule = new PatternGraph(
				"ignoreRule",
				"",
				new PatternNode[] { ignoreRule_node_r, ignoreRule_node_p }, 
				new PatternEdge[] { ignoreRule_edge_b }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { ignoreRule_neg_0,  }, 
				new Condition[] {  }, 
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
			ignoreRule_node_r.PointOfDefinition = ignoreRule;
			ignoreRule_node_p.PointOfDefinition = ignoreRule;
			ignoreRule_edge_b.PointOfDefinition = ignoreRule;
			ignoreRule_neg_0_node_m.PointOfDefinition = ignoreRule_neg_0;
			ignoreRule_neg_0_edge_hb.PointOfDefinition = ignoreRule_neg_0;

			patternGraph = ignoreRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge_b = match.Edges[ (int) ignoreRule_EdgeNums.@b];
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge_b = match.Edges[ (int) ignoreRule_EdgeNums.@b];
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_killRule : LGSPRulePattern
	{
		private static Rule_killRule instance = null;
		public static Rule_killRule Instance { get { if (instance==null) instance = new Rule_killRule(); return instance; } }

		public static NodeType[] killRule_node_p1_AllowedTypes = null;
		public static NodeType[] killRule_node_p_AllowedTypes = null;
		public static NodeType[] killRule_node_p2_AllowedTypes = null;
		public static bool[] killRule_node_p1_IsAllowedType = null;
		public static bool[] killRule_node_p_IsAllowedType = null;
		public static bool[] killRule_node_p2_IsAllowedType = null;
		public static EdgeType[] killRule_edge_n1_AllowedTypes = null;
		public static EdgeType[] killRule_edge_n2_AllowedTypes = null;
		public static bool[] killRule_edge_n1_IsAllowedType = null;
		public static bool[] killRule_edge_n2_IsAllowedType = null;
		public enum killRule_NodeNums { @p1, @p, @p2, };
		public enum killRule_EdgeNums { @n1, @n2, };
		public enum killRule_SubNums { };
		public enum killRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_killRule()
#else
		private Rule_killRule()
#endif
		{
			name = "killRule";
			isSubpattern = false;

			PatternGraph killRule;
			PatternNode killRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "killRule_node_p1", "p1", killRule_node_p1_AllowedTypes, killRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode killRule_node_p = new PatternNode((int) NodeTypes.@Process, "killRule_node_p", "p", killRule_node_p_AllowedTypes, killRule_node_p_IsAllowedType, 5.5F, -1);
			PatternNode killRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "killRule_node_p2", "p2", killRule_node_p2_AllowedTypes, killRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternEdge killRule_edge_n1 = new PatternEdge(killRule_node_p1, killRule_node_p, (int) EdgeTypes.@next, "killRule_edge_n1", "n1", killRule_edge_n1_AllowedTypes, killRule_edge_n1_IsAllowedType, 5.5F, -1);
			PatternEdge killRule_edge_n2 = new PatternEdge(killRule_node_p, killRule_node_p2, (int) EdgeTypes.@next, "killRule_edge_n2", "n2", killRule_edge_n2_AllowedTypes, killRule_edge_n2_IsAllowedType, 5.5F, -1);
			killRule = new PatternGraph(
				"killRule",
				"",
				new PatternNode[] { killRule_node_p1, killRule_node_p, killRule_node_p2 }, 
				new PatternEdge[] { killRule_edge_n1, killRule_edge_n2 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			killRule_node_p1.PointOfDefinition = killRule;
			killRule_node_p.PointOfDefinition = killRule;
			killRule_node_p2.PointOfDefinition = killRule;
			killRule_edge_n1.PointOfDefinition = killRule;
			killRule_edge_n2.PointOfDefinition = killRule;

			patternGraph = killRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.Nodes[ (int) killRule_NodeNums.@p1];
			LGSPNode node_p2 = match.Nodes[ (int) killRule_NodeNums.@p2];
			LGSPNode node_p = match.Nodes[ (int) killRule_NodeNums.@p];
			LGSPEdge edge_n2 = match.Edges[ (int) killRule_EdgeNums.@n2];
			LGSPEdge edge_n1 = match.Edges[ (int) killRule_EdgeNums.@n1];
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
			LGSPNode node_p1 = match.Nodes[ (int) killRule_NodeNums.@p1];
			LGSPNode node_p2 = match.Nodes[ (int) killRule_NodeNums.@p2];
			LGSPNode node_p = match.Nodes[ (int) killRule_NodeNums.@p];
			LGSPEdge edge_n2 = match.Edges[ (int) killRule_EdgeNums.@n2];
			LGSPEdge edge_n1 = match.Edges[ (int) killRule_EdgeNums.@n1];
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

	public class Rule_mountRule : LGSPRulePattern
	{
		private static Rule_mountRule instance = null;
		public static Rule_mountRule Instance { get { if (instance==null) instance = new Rule_mountRule(); return instance; } }

		public static NodeType[] mountRule_node_p_AllowedTypes = null;
		public static bool[] mountRule_node_p_IsAllowedType = null;
		public enum mountRule_NodeNums { @p, };
		public enum mountRule_EdgeNums { };
		public enum mountRule_SubNums { };
		public enum mountRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_mountRule()
#else
		private Rule_mountRule()
#endif
		{
			name = "mountRule";
			isSubpattern = false;

			PatternGraph mountRule;
			PatternNode mountRule_node_p = new PatternNode((int) NodeTypes.@Process, "mountRule_node_p", "p", mountRule_node_p_AllowedTypes, mountRule_node_p_IsAllowedType, 5.5F, -1);
			mountRule = new PatternGraph(
				"mountRule",
				"",
				new PatternNode[] { mountRule_node_p }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			mountRule_node_p.PointOfDefinition = mountRule;

			patternGraph = mountRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) mountRule_NodeNums.@p];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) mountRule_NodeNums.@p];
			Node_Resource node_r = Node_Resource.CreateNode(graph);
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "r" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_newRule : LGSPRulePattern
	{
		private static Rule_newRule instance = null;
		public static Rule_newRule Instance { get { if (instance==null) instance = new Rule_newRule(); return instance; } }

		public static NodeType[] newRule_node_p1_AllowedTypes = null;
		public static NodeType[] newRule_node_p2_AllowedTypes = null;
		public static bool[] newRule_node_p1_IsAllowedType = null;
		public static bool[] newRule_node_p2_IsAllowedType = null;
		public static EdgeType[] newRule_edge_n_AllowedTypes = null;
		public static bool[] newRule_edge_n_IsAllowedType = null;
		public enum newRule_NodeNums { @p1, @p2, };
		public enum newRule_EdgeNums { @n, };
		public enum newRule_SubNums { };
		public enum newRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_newRule()
#else
		private Rule_newRule()
#endif
		{
			name = "newRule";
			isSubpattern = false;

			PatternGraph newRule;
			PatternNode newRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "newRule_node_p1", "p1", newRule_node_p1_AllowedTypes, newRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode newRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "newRule_node_p2", "p2", newRule_node_p2_AllowedTypes, newRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternEdge newRule_edge_n = new PatternEdge(newRule_node_p1, newRule_node_p2, (int) EdgeTypes.@next, "newRule_edge_n", "n", newRule_edge_n_AllowedTypes, newRule_edge_n_IsAllowedType, 1.0F, -1);
			newRule = new PatternGraph(
				"newRule",
				"",
				new PatternNode[] { newRule_node_p1, newRule_node_p2 }, 
				new PatternEdge[] { newRule_edge_n }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			newRule_node_p1.PointOfDefinition = newRule;
			newRule_node_p2.PointOfDefinition = newRule;
			newRule_edge_n.PointOfDefinition = newRule;

			patternGraph = newRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.Nodes[ (int) newRule_NodeNums.@p2];
			LGSPNode node_p1 = match.Nodes[ (int) newRule_NodeNums.@p1];
			LGSPEdge edge_n = match.Edges[ (int) newRule_EdgeNums.@n];
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
			LGSPNode node_p2 = match.Nodes[ (int) newRule_NodeNums.@p2];
			LGSPNode node_p1 = match.Nodes[ (int) newRule_NodeNums.@p1];
			LGSPEdge edge_n = match.Edges[ (int) newRule_EdgeNums.@n];
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

	public class Rule_passRule : LGSPRulePattern
	{
		private static Rule_passRule instance = null;
		public static Rule_passRule Instance { get { if (instance==null) instance = new Rule_passRule(); return instance; } }

		public static NodeType[] passRule_node_r_AllowedTypes = null;
		public static NodeType[] passRule_node_p1_AllowedTypes = null;
		public static NodeType[] passRule_node_p2_AllowedTypes = null;
		public static bool[] passRule_node_r_IsAllowedType = null;
		public static bool[] passRule_node_p1_IsAllowedType = null;
		public static bool[] passRule_node_p2_IsAllowedType = null;
		public static EdgeType[] passRule_edge__edge0_AllowedTypes = null;
		public static EdgeType[] passRule_edge_n_AllowedTypes = null;
		public static bool[] passRule_edge__edge0_IsAllowedType = null;
		public static bool[] passRule_edge_n_IsAllowedType = null;
		public enum passRule_NodeNums { @r, @p1, @p2, };
		public enum passRule_EdgeNums { @_edge0, @n, };
		public enum passRule_SubNums { };
		public enum passRule_AltNums { };
		public static EdgeType[] passRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] passRule_neg_0_edge_req_IsAllowedType = null;
		public enum passRule_neg_0_NodeNums { @p1, @r, };
		public enum passRule_neg_0_EdgeNums { @req, };
		public enum passRule_neg_0_SubNums { };
		public enum passRule_neg_0_AltNums { };

#if INITIAL_WARMUP
		public Rule_passRule()
#else
		private Rule_passRule()
#endif
		{
			name = "passRule";
			isSubpattern = false;

			PatternGraph passRule;
			PatternNode passRule_node_r = new PatternNode((int) NodeTypes.@Resource, "passRule_node_r", "r", passRule_node_r_AllowedTypes, passRule_node_r_IsAllowedType, 1.0F, -1);
			PatternNode passRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "passRule_node_p1", "p1", passRule_node_p1_AllowedTypes, passRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode passRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "passRule_node_p2", "p2", passRule_node_p2_AllowedTypes, passRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternEdge passRule_edge__edge0 = new PatternEdge(passRule_node_r, passRule_node_p1, (int) EdgeTypes.@token, "passRule_edge__edge0", "_edge0", passRule_edge__edge0_AllowedTypes, passRule_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge passRule_edge_n = new PatternEdge(passRule_node_p1, passRule_node_p2, (int) EdgeTypes.@next, "passRule_edge_n", "n", passRule_edge_n_AllowedTypes, passRule_edge_n_IsAllowedType, 5.5F, -1);
			PatternGraph passRule_neg_0;
			PatternEdge passRule_neg_0_edge_req = new PatternEdge(passRule_node_p1, passRule_node_r, (int) EdgeTypes.@request, "passRule_neg_0_edge_req", "req", passRule_neg_0_edge_req_AllowedTypes, passRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
			passRule_neg_0 = new PatternGraph(
				"neg_0",
				"passRule_",
				new PatternNode[] { passRule_node_p1, passRule_node_r }, 
				new PatternEdge[] { passRule_neg_0_edge_req }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			passRule = new PatternGraph(
				"passRule",
				"",
				new PatternNode[] { passRule_node_r, passRule_node_p1, passRule_node_p2 }, 
				new PatternEdge[] { passRule_edge__edge0, passRule_edge_n }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { passRule_neg_0,  }, 
				new Condition[] {  }, 
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
			passRule_node_r.PointOfDefinition = passRule;
			passRule_node_p1.PointOfDefinition = passRule;
			passRule_node_p2.PointOfDefinition = passRule;
			passRule_edge__edge0.PointOfDefinition = passRule;
			passRule_edge_n.PointOfDefinition = passRule;
			passRule_neg_0_edge_req.PointOfDefinition = passRule_neg_0;

			patternGraph = passRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) passRule_NodeNums.@r];
			LGSPNode node_p2 = match.Nodes[ (int) passRule_NodeNums.@p2];
			LGSPEdge edge__edge0 = match.Edges[ (int) passRule_EdgeNums.@_edge0];
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
			LGSPNode node_r = match.Nodes[ (int) passRule_NodeNums.@r];
			LGSPNode node_p2 = match.Nodes[ (int) passRule_NodeNums.@p2];
			LGSPEdge edge__edge0 = match.Edges[ (int) passRule_EdgeNums.@_edge0];
			Edge_token edge_t = Edge_token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge__edge0);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "t" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_releaseRule : LGSPRulePattern
	{
		private static Rule_releaseRule instance = null;
		public static Rule_releaseRule Instance { get { if (instance==null) instance = new Rule_releaseRule(); return instance; } }

		public static NodeType[] releaseRule_node_r_AllowedTypes = null;
		public static NodeType[] releaseRule_node_p_AllowedTypes = null;
		public static bool[] releaseRule_node_r_IsAllowedType = null;
		public static bool[] releaseRule_node_p_IsAllowedType = null;
		public static EdgeType[] releaseRule_edge_hb_AllowedTypes = null;
		public static bool[] releaseRule_edge_hb_IsAllowedType = null;
		public enum releaseRule_NodeNums { @r, @p, };
		public enum releaseRule_EdgeNums { @hb, };
		public enum releaseRule_SubNums { };
		public enum releaseRule_AltNums { };
		public static NodeType[] releaseRule_neg_0_node_m_AllowedTypes = null;
		public static bool[] releaseRule_neg_0_node_m_IsAllowedType = null;
		public static EdgeType[] releaseRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] releaseRule_neg_0_edge_req_IsAllowedType = null;
		public enum releaseRule_neg_0_NodeNums { @p, @m, };
		public enum releaseRule_neg_0_EdgeNums { @req, };
		public enum releaseRule_neg_0_SubNums { };
		public enum releaseRule_neg_0_AltNums { };

#if INITIAL_WARMUP
		public Rule_releaseRule()
#else
		private Rule_releaseRule()
#endif
		{
			name = "releaseRule";
			isSubpattern = false;

			PatternGraph releaseRule;
			PatternNode releaseRule_node_r = new PatternNode((int) NodeTypes.@Resource, "releaseRule_node_r", "r", releaseRule_node_r_AllowedTypes, releaseRule_node_r_IsAllowedType, 5.5F, -1);
			PatternNode releaseRule_node_p = new PatternNode((int) NodeTypes.@Process, "releaseRule_node_p", "p", releaseRule_node_p_AllowedTypes, releaseRule_node_p_IsAllowedType, 5.5F, -1);
			PatternEdge releaseRule_edge_hb = new PatternEdge(releaseRule_node_r, releaseRule_node_p, (int) EdgeTypes.@held_by, "releaseRule_edge_hb", "hb", releaseRule_edge_hb_AllowedTypes, releaseRule_edge_hb_IsAllowedType, 1.0F, -1);
			PatternGraph releaseRule_neg_0;
			PatternNode releaseRule_neg_0_node_m = new PatternNode((int) NodeTypes.@Resource, "releaseRule_neg_0_node_m", "m", releaseRule_neg_0_node_m_AllowedTypes, releaseRule_neg_0_node_m_IsAllowedType, 5.5F, -1);
			PatternEdge releaseRule_neg_0_edge_req = new PatternEdge(releaseRule_node_p, releaseRule_neg_0_node_m, (int) EdgeTypes.@request, "releaseRule_neg_0_edge_req", "req", releaseRule_neg_0_edge_req_AllowedTypes, releaseRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
			releaseRule_neg_0 = new PatternGraph(
				"neg_0",
				"releaseRule_",
				new PatternNode[] { releaseRule_node_p, releaseRule_neg_0_node_m }, 
				new PatternEdge[] { releaseRule_neg_0_edge_req }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			releaseRule = new PatternGraph(
				"releaseRule",
				"",
				new PatternNode[] { releaseRule_node_r, releaseRule_node_p }, 
				new PatternEdge[] { releaseRule_edge_hb }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { releaseRule_neg_0,  }, 
				new Condition[] {  }, 
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
			releaseRule_node_r.PointOfDefinition = releaseRule;
			releaseRule_node_p.PointOfDefinition = releaseRule;
			releaseRule_edge_hb.PointOfDefinition = releaseRule;
			releaseRule_neg_0_node_m.PointOfDefinition = releaseRule_neg_0;
			releaseRule_neg_0_edge_req.PointOfDefinition = releaseRule_neg_0;

			patternGraph = releaseRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) releaseRule_NodeNums.@r];
			LGSPNode node_p = match.Nodes[ (int) releaseRule_NodeNums.@p];
			LGSPEdge edge_hb = match.Edges[ (int) releaseRule_EdgeNums.@hb];
			Edge_release edge_rel = Edge_release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) releaseRule_NodeNums.@r];
			LGSPNode node_p = match.Nodes[ (int) releaseRule_NodeNums.@p];
			LGSPEdge edge_hb = match.Edges[ (int) releaseRule_EdgeNums.@hb];
			Edge_release edge_rel = Edge_release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rel" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_releaseStarRule : LGSPRulePattern
	{
		private static Rule_releaseStarRule instance = null;
		public static Rule_releaseStarRule Instance { get { if (instance==null) instance = new Rule_releaseStarRule(); return instance; } }

		public static NodeType[] releaseStarRule_node_p1_AllowedTypes = null;
		public static NodeType[] releaseStarRule_node_r1_AllowedTypes = null;
		public static NodeType[] releaseStarRule_node_p2_AllowedTypes = null;
		public static NodeType[] releaseStarRule_node_r2_AllowedTypes = null;
		public static bool[] releaseStarRule_node_p1_IsAllowedType = null;
		public static bool[] releaseStarRule_node_r1_IsAllowedType = null;
		public static bool[] releaseStarRule_node_p2_IsAllowedType = null;
		public static bool[] releaseStarRule_node_r2_IsAllowedType = null;
		public static EdgeType[] releaseStarRule_edge_rq_AllowedTypes = null;
		public static EdgeType[] releaseStarRule_edge_h1_AllowedTypes = null;
		public static EdgeType[] releaseStarRule_edge_h2_AllowedTypes = null;
		public static bool[] releaseStarRule_edge_rq_IsAllowedType = null;
		public static bool[] releaseStarRule_edge_h1_IsAllowedType = null;
		public static bool[] releaseStarRule_edge_h2_IsAllowedType = null;
		public enum releaseStarRule_NodeNums { @p1, @r1, @p2, @r2, };
		public enum releaseStarRule_EdgeNums { @rq, @h1, @h2, };
		public enum releaseStarRule_SubNums { };
		public enum releaseStarRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_releaseStarRule()
#else
		private Rule_releaseStarRule()
#endif
		{
			name = "releaseStarRule";
			isSubpattern = false;

			PatternGraph releaseStarRule;
			PatternNode releaseStarRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "releaseStarRule_node_p1", "p1", releaseStarRule_node_p1_AllowedTypes, releaseStarRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode releaseStarRule_node_r1 = new PatternNode((int) NodeTypes.@Resource, "releaseStarRule_node_r1", "r1", releaseStarRule_node_r1_AllowedTypes, releaseStarRule_node_r1_IsAllowedType, 5.5F, -1);
			PatternNode releaseStarRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "releaseStarRule_node_p2", "p2", releaseStarRule_node_p2_AllowedTypes, releaseStarRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternNode releaseStarRule_node_r2 = new PatternNode((int) NodeTypes.@Resource, "releaseStarRule_node_r2", "r2", releaseStarRule_node_r2_AllowedTypes, releaseStarRule_node_r2_IsAllowedType, 5.5F, -1);
			PatternEdge releaseStarRule_edge_rq = new PatternEdge(releaseStarRule_node_p1, releaseStarRule_node_r1, (int) EdgeTypes.@request, "releaseStarRule_edge_rq", "rq", releaseStarRule_edge_rq_AllowedTypes, releaseStarRule_edge_rq_IsAllowedType, 5.5F, -1);
			PatternEdge releaseStarRule_edge_h1 = new PatternEdge(releaseStarRule_node_r1, releaseStarRule_node_p2, (int) EdgeTypes.@held_by, "releaseStarRule_edge_h1", "h1", releaseStarRule_edge_h1_AllowedTypes, releaseStarRule_edge_h1_IsAllowedType, 5.5F, -1);
			PatternEdge releaseStarRule_edge_h2 = new PatternEdge(releaseStarRule_node_r2, releaseStarRule_node_p2, (int) EdgeTypes.@held_by, "releaseStarRule_edge_h2", "h2", releaseStarRule_edge_h2_AllowedTypes, releaseStarRule_edge_h2_IsAllowedType, 5.5F, -1);
			releaseStarRule = new PatternGraph(
				"releaseStarRule",
				"",
				new PatternNode[] { releaseStarRule_node_p1, releaseStarRule_node_r1, releaseStarRule_node_p2, releaseStarRule_node_r2 }, 
				new PatternEdge[] { releaseStarRule_edge_rq, releaseStarRule_edge_h1, releaseStarRule_edge_h2 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			releaseStarRule_node_p1.PointOfDefinition = releaseStarRule;
			releaseStarRule_node_r1.PointOfDefinition = releaseStarRule;
			releaseStarRule_node_p2.PointOfDefinition = releaseStarRule;
			releaseStarRule_node_r2.PointOfDefinition = releaseStarRule;
			releaseStarRule_edge_rq.PointOfDefinition = releaseStarRule;
			releaseStarRule_edge_h1.PointOfDefinition = releaseStarRule;
			releaseStarRule_edge_h2.PointOfDefinition = releaseStarRule;

			patternGraph = releaseStarRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r1 = match.Nodes[ (int) releaseStarRule_NodeNums.@r1];
			LGSPNode node_p2 = match.Nodes[ (int) releaseStarRule_NodeNums.@p2];
			LGSPEdge edge_h1 = match.Edges[ (int) releaseStarRule_EdgeNums.@h1];
			Edge_release edge_rl = Edge_release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r1 = match.Nodes[ (int) releaseStarRule_NodeNums.@r1];
			LGSPNode node_p2 = match.Nodes[ (int) releaseStarRule_NodeNums.@p2];
			LGSPEdge edge_h1 = match.Edges[ (int) releaseStarRule_EdgeNums.@h1];
			Edge_release edge_rl = Edge_release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "rl" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_requestRule : LGSPRulePattern
	{
		private static Rule_requestRule instance = null;
		public static Rule_requestRule Instance { get { if (instance==null) instance = new Rule_requestRule(); return instance; } }

		public static NodeType[] requestRule_node_p_AllowedTypes = null;
		public static NodeType[] requestRule_node_r_AllowedTypes = null;
		public static bool[] requestRule_node_p_IsAllowedType = null;
		public static bool[] requestRule_node_r_IsAllowedType = null;
		public enum requestRule_NodeNums { @p, @r, };
		public enum requestRule_EdgeNums { };
		public enum requestRule_SubNums { };
		public enum requestRule_AltNums { };
		public static EdgeType[] requestRule_neg_0_edge_hb_AllowedTypes = null;
		public static bool[] requestRule_neg_0_edge_hb_IsAllowedType = null;
		public enum requestRule_neg_0_NodeNums { @r, @p, };
		public enum requestRule_neg_0_EdgeNums { @hb, };
		public enum requestRule_neg_0_SubNums { };
		public enum requestRule_neg_0_AltNums { };
		public static NodeType[] requestRule_neg_1_node_m_AllowedTypes = null;
		public static bool[] requestRule_neg_1_node_m_IsAllowedType = null;
		public static EdgeType[] requestRule_neg_1_edge_req_AllowedTypes = null;
		public static bool[] requestRule_neg_1_edge_req_IsAllowedType = null;
		public enum requestRule_neg_1_NodeNums { @p, @m, };
		public enum requestRule_neg_1_EdgeNums { @req, };
		public enum requestRule_neg_1_SubNums { };
		public enum requestRule_neg_1_AltNums { };

#if INITIAL_WARMUP
		public Rule_requestRule()
#else
		private Rule_requestRule()
#endif
		{
			name = "requestRule";
			isSubpattern = false;

			PatternGraph requestRule;
			PatternNode requestRule_node_p = new PatternNode((int) NodeTypes.@Process, "requestRule_node_p", "p", requestRule_node_p_AllowedTypes, requestRule_node_p_IsAllowedType, 5.5F, -1);
			PatternNode requestRule_node_r = new PatternNode((int) NodeTypes.@Resource, "requestRule_node_r", "r", requestRule_node_r_AllowedTypes, requestRule_node_r_IsAllowedType, 1.0F, -1);
			PatternGraph requestRule_neg_0;
			PatternEdge requestRule_neg_0_edge_hb = new PatternEdge(requestRule_node_r, requestRule_node_p, (int) EdgeTypes.@held_by, "requestRule_neg_0_edge_hb", "hb", requestRule_neg_0_edge_hb_AllowedTypes, requestRule_neg_0_edge_hb_IsAllowedType, 5.5F, -1);
			requestRule_neg_0 = new PatternGraph(
				"neg_0",
				"requestRule_",
				new PatternNode[] { requestRule_node_r, requestRule_node_p }, 
				new PatternEdge[] { requestRule_neg_0_edge_hb }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			PatternGraph requestRule_neg_1;
			PatternNode requestRule_neg_1_node_m = new PatternNode((int) NodeTypes.@Resource, "requestRule_neg_1_node_m", "m", requestRule_neg_1_node_m_AllowedTypes, requestRule_neg_1_node_m_IsAllowedType, 5.5F, -1);
			PatternEdge requestRule_neg_1_edge_req = new PatternEdge(requestRule_node_p, requestRule_neg_1_node_m, (int) EdgeTypes.@request, "requestRule_neg_1_edge_req", "req", requestRule_neg_1_edge_req_AllowedTypes, requestRule_neg_1_edge_req_IsAllowedType, 5.5F, -1);
			requestRule_neg_1 = new PatternGraph(
				"neg_1",
				"requestRule_",
				new PatternNode[] { requestRule_node_p, requestRule_neg_1_node_m }, 
				new PatternEdge[] { requestRule_neg_1_edge_req }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			requestRule = new PatternGraph(
				"requestRule",
				"",
				new PatternNode[] { requestRule_node_p, requestRule_node_r }, 
				new PatternEdge[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { requestRule_neg_0, requestRule_neg_1,  }, 
				new Condition[] {  }, 
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
			requestRule_node_p.PointOfDefinition = requestRule;
			requestRule_node_r.PointOfDefinition = requestRule;
			requestRule_neg_0_edge_hb.PointOfDefinition = requestRule_neg_0;
			requestRule_neg_1_node_m.PointOfDefinition = requestRule_neg_1;
			requestRule_neg_1_edge_req.PointOfDefinition = requestRule_neg_1;

			patternGraph = requestRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) requestRule_NodeNums.@p];
			LGSPNode node_r = match.Nodes[ (int) requestRule_NodeNums.@r];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) requestRule_NodeNums.@p];
			LGSPNode node_r = match.Nodes[ (int) requestRule_NodeNums.@r];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_requestSimpleRule : LGSPRulePattern
	{
		private static Rule_requestSimpleRule instance = null;
		public static Rule_requestSimpleRule Instance { get { if (instance==null) instance = new Rule_requestSimpleRule(); return instance; } }

		public static NodeType[] requestSimpleRule_node_r_AllowedTypes = null;
		public static NodeType[] requestSimpleRule_node_p_AllowedTypes = null;
		public static bool[] requestSimpleRule_node_r_IsAllowedType = null;
		public static bool[] requestSimpleRule_node_p_IsAllowedType = null;
		public static EdgeType[] requestSimpleRule_edge_t_AllowedTypes = null;
		public static bool[] requestSimpleRule_edge_t_IsAllowedType = null;
		public enum requestSimpleRule_NodeNums { @r, @p, };
		public enum requestSimpleRule_EdgeNums { @t, };
		public enum requestSimpleRule_SubNums { };
		public enum requestSimpleRule_AltNums { };
		public static EdgeType[] requestSimpleRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] requestSimpleRule_neg_0_edge_req_IsAllowedType = null;
		public enum requestSimpleRule_neg_0_NodeNums { @p, @r, };
		public enum requestSimpleRule_neg_0_EdgeNums { @req, };
		public enum requestSimpleRule_neg_0_SubNums { };
		public enum requestSimpleRule_neg_0_AltNums { };

#if INITIAL_WARMUP
		public Rule_requestSimpleRule()
#else
		private Rule_requestSimpleRule()
#endif
		{
			name = "requestSimpleRule";
			isSubpattern = false;

			PatternGraph requestSimpleRule;
			PatternNode requestSimpleRule_node_r = new PatternNode((int) NodeTypes.@Resource, "requestSimpleRule_node_r", "r", requestSimpleRule_node_r_AllowedTypes, requestSimpleRule_node_r_IsAllowedType, 1.0F, -1);
			PatternNode requestSimpleRule_node_p = new PatternNode((int) NodeTypes.@Process, "requestSimpleRule_node_p", "p", requestSimpleRule_node_p_AllowedTypes, requestSimpleRule_node_p_IsAllowedType, 5.5F, -1);
			PatternEdge requestSimpleRule_edge_t = new PatternEdge(requestSimpleRule_node_r, requestSimpleRule_node_p, (int) EdgeTypes.@token, "requestSimpleRule_edge_t", "t", requestSimpleRule_edge_t_AllowedTypes, requestSimpleRule_edge_t_IsAllowedType, 5.5F, -1);
			PatternGraph requestSimpleRule_neg_0;
			PatternEdge requestSimpleRule_neg_0_edge_req = new PatternEdge(requestSimpleRule_node_p, requestSimpleRule_node_r, (int) EdgeTypes.@request, "requestSimpleRule_neg_0_edge_req", "req", requestSimpleRule_neg_0_edge_req_AllowedTypes, requestSimpleRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
			requestSimpleRule_neg_0 = new PatternGraph(
				"neg_0",
				"requestSimpleRule_",
				new PatternNode[] { requestSimpleRule_node_p, requestSimpleRule_node_r }, 
				new PatternEdge[] { requestSimpleRule_neg_0_edge_req }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			requestSimpleRule = new PatternGraph(
				"requestSimpleRule",
				"",
				new PatternNode[] { requestSimpleRule_node_r, requestSimpleRule_node_p }, 
				new PatternEdge[] { requestSimpleRule_edge_t }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { requestSimpleRule_neg_0,  }, 
				new Condition[] {  }, 
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
			requestSimpleRule_node_r.PointOfDefinition = requestSimpleRule;
			requestSimpleRule_node_p.PointOfDefinition = requestSimpleRule;
			requestSimpleRule_edge_t.PointOfDefinition = requestSimpleRule;
			requestSimpleRule_neg_0_edge_req.PointOfDefinition = requestSimpleRule_neg_0;

			patternGraph = requestSimpleRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) requestSimpleRule_NodeNums.@p];
			LGSPNode node_r = match.Nodes[ (int) requestSimpleRule_NodeNums.@r];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p = match.Nodes[ (int) requestSimpleRule_NodeNums.@p];
			LGSPNode node_r = match.Nodes[ (int) requestSimpleRule_NodeNums.@r];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_requestStarRule : LGSPRulePattern
	{
		private static Rule_requestStarRule instance = null;
		public static Rule_requestStarRule Instance { get { if (instance==null) instance = new Rule_requestStarRule(); return instance; } }

		public static NodeType[] requestStarRule_node_r1_AllowedTypes = null;
		public static NodeType[] requestStarRule_node_p1_AllowedTypes = null;
		public static NodeType[] requestStarRule_node_p2_AllowedTypes = null;
		public static NodeType[] requestStarRule_node_r2_AllowedTypes = null;
		public static bool[] requestStarRule_node_r1_IsAllowedType = null;
		public static bool[] requestStarRule_node_p1_IsAllowedType = null;
		public static bool[] requestStarRule_node_p2_IsAllowedType = null;
		public static bool[] requestStarRule_node_r2_IsAllowedType = null;
		public static EdgeType[] requestStarRule_edge_h1_AllowedTypes = null;
		public static EdgeType[] requestStarRule_edge_n_AllowedTypes = null;
		public static EdgeType[] requestStarRule_edge_h2_AllowedTypes = null;
		public static bool[] requestStarRule_edge_h1_IsAllowedType = null;
		public static bool[] requestStarRule_edge_n_IsAllowedType = null;
		public static bool[] requestStarRule_edge_h2_IsAllowedType = null;
		public enum requestStarRule_NodeNums { @r1, @p1, @p2, @r2, };
		public enum requestStarRule_EdgeNums { @h1, @n, @h2, };
		public enum requestStarRule_SubNums { };
		public enum requestStarRule_AltNums { };
		public static EdgeType[] requestStarRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] requestStarRule_neg_0_edge_req_IsAllowedType = null;
		public enum requestStarRule_neg_0_NodeNums { @p1, @r2, };
		public enum requestStarRule_neg_0_EdgeNums { @req, };
		public enum requestStarRule_neg_0_SubNums { };
		public enum requestStarRule_neg_0_AltNums { };

#if INITIAL_WARMUP
		public Rule_requestStarRule()
#else
		private Rule_requestStarRule()
#endif
		{
			name = "requestStarRule";
			isSubpattern = false;

			PatternGraph requestStarRule;
			PatternNode requestStarRule_node_r1 = new PatternNode((int) NodeTypes.@Resource, "requestStarRule_node_r1", "r1", requestStarRule_node_r1_AllowedTypes, requestStarRule_node_r1_IsAllowedType, 5.5F, -1);
			PatternNode requestStarRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "requestStarRule_node_p1", "p1", requestStarRule_node_p1_AllowedTypes, requestStarRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode requestStarRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "requestStarRule_node_p2", "p2", requestStarRule_node_p2_AllowedTypes, requestStarRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternNode requestStarRule_node_r2 = new PatternNode((int) NodeTypes.@Resource, "requestStarRule_node_r2", "r2", requestStarRule_node_r2_AllowedTypes, requestStarRule_node_r2_IsAllowedType, 5.5F, -1);
			PatternEdge requestStarRule_edge_h1 = new PatternEdge(requestStarRule_node_r1, requestStarRule_node_p1, (int) EdgeTypes.@held_by, "requestStarRule_edge_h1", "h1", requestStarRule_edge_h1_AllowedTypes, requestStarRule_edge_h1_IsAllowedType, 5.5F, -1);
			PatternEdge requestStarRule_edge_n = new PatternEdge(requestStarRule_node_p2, requestStarRule_node_p1, (int) EdgeTypes.@next, "requestStarRule_edge_n", "n", requestStarRule_edge_n_AllowedTypes, requestStarRule_edge_n_IsAllowedType, 5.5F, -1);
			PatternEdge requestStarRule_edge_h2 = new PatternEdge(requestStarRule_node_r2, requestStarRule_node_p2, (int) EdgeTypes.@held_by, "requestStarRule_edge_h2", "h2", requestStarRule_edge_h2_AllowedTypes, requestStarRule_edge_h2_IsAllowedType, 5.5F, -1);
			PatternGraph requestStarRule_neg_0;
			PatternEdge requestStarRule_neg_0_edge_req = new PatternEdge(requestStarRule_node_p1, requestStarRule_node_r2, (int) EdgeTypes.@request, "requestStarRule_neg_0_edge_req", "req", requestStarRule_neg_0_edge_req_AllowedTypes, requestStarRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
			requestStarRule_neg_0 = new PatternGraph(
				"neg_0",
				"requestStarRule_",
				new PatternNode[] { requestStarRule_node_p1, requestStarRule_node_r2 }, 
				new PatternEdge[] { requestStarRule_neg_0_edge_req }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			requestStarRule = new PatternGraph(
				"requestStarRule",
				"",
				new PatternNode[] { requestStarRule_node_r1, requestStarRule_node_p1, requestStarRule_node_p2, requestStarRule_node_r2 }, 
				new PatternEdge[] { requestStarRule_edge_h1, requestStarRule_edge_n, requestStarRule_edge_h2 }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { requestStarRule_neg_0,  }, 
				new Condition[] {  }, 
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
			requestStarRule_node_r1.PointOfDefinition = requestStarRule;
			requestStarRule_node_p1.PointOfDefinition = requestStarRule;
			requestStarRule_node_p2.PointOfDefinition = requestStarRule;
			requestStarRule_node_r2.PointOfDefinition = requestStarRule;
			requestStarRule_edge_h1.PointOfDefinition = requestStarRule;
			requestStarRule_edge_n.PointOfDefinition = requestStarRule;
			requestStarRule_edge_h2.PointOfDefinition = requestStarRule;
			requestStarRule_neg_0_edge_req.PointOfDefinition = requestStarRule_neg_0;

			patternGraph = requestStarRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.Nodes[ (int) requestStarRule_NodeNums.@p1];
			LGSPNode node_r2 = match.Nodes[ (int) requestStarRule_NodeNums.@r2];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p1, node_r2);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p1 = match.Nodes[ (int) requestStarRule_NodeNums.@p1];
			LGSPNode node_r2 = match.Nodes[ (int) requestStarRule_NodeNums.@r2];
			Edge_request edge_req = Edge_request.CreateEdge(graph, node_p1, node_r2);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "req" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_takeRule : LGSPRulePattern
	{
		private static Rule_takeRule instance = null;
		public static Rule_takeRule Instance { get { if (instance==null) instance = new Rule_takeRule(); return instance; } }

		public static NodeType[] takeRule_node_r_AllowedTypes = null;
		public static NodeType[] takeRule_node_p_AllowedTypes = null;
		public static bool[] takeRule_node_r_IsAllowedType = null;
		public static bool[] takeRule_node_p_IsAllowedType = null;
		public static EdgeType[] takeRule_edge_t_AllowedTypes = null;
		public static EdgeType[] takeRule_edge_req_AllowedTypes = null;
		public static bool[] takeRule_edge_t_IsAllowedType = null;
		public static bool[] takeRule_edge_req_IsAllowedType = null;
		public enum takeRule_NodeNums { @r, @p, };
		public enum takeRule_EdgeNums { @t, @req, };
		public enum takeRule_SubNums { };
		public enum takeRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_takeRule()
#else
		private Rule_takeRule()
#endif
		{
			name = "takeRule";
			isSubpattern = false;

			PatternGraph takeRule;
			PatternNode takeRule_node_r = new PatternNode((int) NodeTypes.@Resource, "takeRule_node_r", "r", takeRule_node_r_AllowedTypes, takeRule_node_r_IsAllowedType, 5.5F, -1);
			PatternNode takeRule_node_p = new PatternNode((int) NodeTypes.@Process, "takeRule_node_p", "p", takeRule_node_p_AllowedTypes, takeRule_node_p_IsAllowedType, 5.5F, -1);
			PatternEdge takeRule_edge_t = new PatternEdge(takeRule_node_r, takeRule_node_p, (int) EdgeTypes.@token, "takeRule_edge_t", "t", takeRule_edge_t_AllowedTypes, takeRule_edge_t_IsAllowedType, 1.0F, -1);
			PatternEdge takeRule_edge_req = new PatternEdge(takeRule_node_p, takeRule_node_r, (int) EdgeTypes.@request, "takeRule_edge_req", "req", takeRule_edge_req_AllowedTypes, takeRule_edge_req_IsAllowedType, 5.5F, -1);
			takeRule = new PatternGraph(
				"takeRule",
				"",
				new PatternNode[] { takeRule_node_r, takeRule_node_p }, 
				new PatternEdge[] { takeRule_edge_t, takeRule_edge_req }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			takeRule_node_r.PointOfDefinition = takeRule;
			takeRule_node_p.PointOfDefinition = takeRule;
			takeRule_edge_t.PointOfDefinition = takeRule;
			takeRule_edge_req.PointOfDefinition = takeRule;

			patternGraph = takeRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) takeRule_NodeNums.@r];
			LGSPNode node_p = match.Nodes[ (int) takeRule_NodeNums.@p];
			LGSPEdge edge_req = match.Edges[ (int) takeRule_EdgeNums.@req];
			LGSPEdge edge_t = match.Edges[ (int) takeRule_EdgeNums.@t];
			Edge_held_by edge_hb = Edge_held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_req);
			graph.Remove(edge_t);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) takeRule_NodeNums.@r];
			LGSPNode node_p = match.Nodes[ (int) takeRule_NodeNums.@p];
			LGSPEdge edge_req = match.Edges[ (int) takeRule_EdgeNums.@req];
			LGSPEdge edge_t = match.Edges[ (int) takeRule_EdgeNums.@t];
			Edge_held_by edge_hb = Edge_held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_req);
			graph.Remove(edge_t);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "hb" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

	public class Rule_unlockRule : LGSPRulePattern
	{
		private static Rule_unlockRule instance = null;
		public static Rule_unlockRule Instance { get { if (instance==null) instance = new Rule_unlockRule(); return instance; } }

		public static NodeType[] unlockRule_node_r_AllowedTypes = null;
		public static NodeType[] unlockRule_node_p_AllowedTypes = null;
		public static bool[] unlockRule_node_r_IsAllowedType = null;
		public static bool[] unlockRule_node_p_IsAllowedType = null;
		public static EdgeType[] unlockRule_edge_b_AllowedTypes = null;
		public static EdgeType[] unlockRule_edge_hb_AllowedTypes = null;
		public static bool[] unlockRule_edge_b_IsAllowedType = null;
		public static bool[] unlockRule_edge_hb_IsAllowedType = null;
		public enum unlockRule_NodeNums { @r, @p, };
		public enum unlockRule_EdgeNums { @b, @hb, };
		public enum unlockRule_SubNums { };
		public enum unlockRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_unlockRule()
#else
		private Rule_unlockRule()
#endif
		{
			name = "unlockRule";
			isSubpattern = false;

			PatternGraph unlockRule;
			PatternNode unlockRule_node_r = new PatternNode((int) NodeTypes.@Resource, "unlockRule_node_r", "r", unlockRule_node_r_AllowedTypes, unlockRule_node_r_IsAllowedType, 1.0F, -1);
			PatternNode unlockRule_node_p = new PatternNode((int) NodeTypes.@Process, "unlockRule_node_p", "p", unlockRule_node_p_AllowedTypes, unlockRule_node_p_IsAllowedType, 5.5F, -1);
			PatternEdge unlockRule_edge_b = new PatternEdge(unlockRule_node_r, unlockRule_node_p, (int) EdgeTypes.@blocked, "unlockRule_edge_b", "b", unlockRule_edge_b_AllowedTypes, unlockRule_edge_b_IsAllowedType, 5.5F, -1);
			PatternEdge unlockRule_edge_hb = new PatternEdge(unlockRule_node_r, unlockRule_node_p, (int) EdgeTypes.@held_by, "unlockRule_edge_hb", "hb", unlockRule_edge_hb_AllowedTypes, unlockRule_edge_hb_IsAllowedType, 5.5F, -1);
			unlockRule = new PatternGraph(
				"unlockRule",
				"",
				new PatternNode[] { unlockRule_node_r, unlockRule_node_p }, 
				new PatternEdge[] { unlockRule_edge_b, unlockRule_edge_hb }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			unlockRule_node_r.PointOfDefinition = unlockRule;
			unlockRule_node_p.PointOfDefinition = unlockRule;
			unlockRule_edge_b.PointOfDefinition = unlockRule;
			unlockRule_edge_hb.PointOfDefinition = unlockRule;

			patternGraph = unlockRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) unlockRule_NodeNums.@r];
			LGSPNode node_p = match.Nodes[ (int) unlockRule_NodeNums.@p];
			LGSPEdge edge_b = match.Edges[ (int) unlockRule_EdgeNums.@b];
			LGSPEdge edge_hb = match.Edges[ (int) unlockRule_EdgeNums.@hb];
			Edge_release edge_rel = Edge_release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) unlockRule_NodeNums.@r];
			LGSPNode node_p = match.Nodes[ (int) unlockRule_NodeNums.@p];
			LGSPEdge edge_b = match.Edges[ (int) unlockRule_EdgeNums.@b];
			LGSPEdge edge_hb = match.Edges[ (int) unlockRule_EdgeNums.@hb];
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

	public class Rule_unmountRule : LGSPRulePattern
	{
		private static Rule_unmountRule instance = null;
		public static Rule_unmountRule Instance { get { if (instance==null) instance = new Rule_unmountRule(); return instance; } }

		public static NodeType[] unmountRule_node_r_AllowedTypes = null;
		public static NodeType[] unmountRule_node_p_AllowedTypes = null;
		public static bool[] unmountRule_node_r_IsAllowedType = null;
		public static bool[] unmountRule_node_p_IsAllowedType = null;
		public static EdgeType[] unmountRule_edge_t_AllowedTypes = null;
		public static bool[] unmountRule_edge_t_IsAllowedType = null;
		public enum unmountRule_NodeNums { @r, @p, };
		public enum unmountRule_EdgeNums { @t, };
		public enum unmountRule_SubNums { };
		public enum unmountRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_unmountRule()
#else
		private Rule_unmountRule()
#endif
		{
			name = "unmountRule";
			isSubpattern = false;

			PatternGraph unmountRule;
			PatternNode unmountRule_node_r = new PatternNode((int) NodeTypes.@Resource, "unmountRule_node_r", "r", unmountRule_node_r_AllowedTypes, unmountRule_node_r_IsAllowedType, 1.0F, -1);
			PatternNode unmountRule_node_p = new PatternNode((int) NodeTypes.@Process, "unmountRule_node_p", "p", unmountRule_node_p_AllowedTypes, unmountRule_node_p_IsAllowedType, 5.5F, -1);
			PatternEdge unmountRule_edge_t = new PatternEdge(unmountRule_node_r, unmountRule_node_p, (int) EdgeTypes.@token, "unmountRule_edge_t", "t", unmountRule_edge_t_AllowedTypes, unmountRule_edge_t_IsAllowedType, 5.5F, -1);
			unmountRule = new PatternGraph(
				"unmountRule",
				"",
				new PatternNode[] { unmountRule_node_r, unmountRule_node_p }, 
				new PatternEdge[] { unmountRule_edge_t }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			unmountRule_node_r.PointOfDefinition = unmountRule;
			unmountRule_node_p.PointOfDefinition = unmountRule;
			unmountRule_edge_t.PointOfDefinition = unmountRule;

			patternGraph = unmountRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) unmountRule_NodeNums.@r];
			LGSPEdge edge_t = match.Edges[ (int) unmountRule_EdgeNums.@t];
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r = match.Nodes[ (int) unmountRule_NodeNums.@r];
			LGSPEdge edge_t = match.Edges[ (int) unmountRule_EdgeNums.@t];
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

	public class Rule_waitingRule : LGSPRulePattern
	{
		private static Rule_waitingRule instance = null;
		public static Rule_waitingRule Instance { get { if (instance==null) instance = new Rule_waitingRule(); return instance; } }

		public static NodeType[] waitingRule_node_r_AllowedTypes = null;
		public static NodeType[] waitingRule_node_r2_AllowedTypes = null;
		public static NodeType[] waitingRule_node_p1_AllowedTypes = null;
		public static NodeType[] waitingRule_node_r1_AllowedTypes = null;
		public static NodeType[] waitingRule_node_p2_AllowedTypes = null;
		public static bool[] waitingRule_node_r_IsAllowedType = null;
		public static bool[] waitingRule_node_r2_IsAllowedType = null;
		public static bool[] waitingRule_node_p1_IsAllowedType = null;
		public static bool[] waitingRule_node_r1_IsAllowedType = null;
		public static bool[] waitingRule_node_p2_IsAllowedType = null;
		public static EdgeType[] waitingRule_edge_b_AllowedTypes = null;
		public static EdgeType[] waitingRule_edge_hb_AllowedTypes = null;
		public static EdgeType[] waitingRule_edge_req_AllowedTypes = null;
		public static bool[] waitingRule_edge_b_IsAllowedType = null;
		public static bool[] waitingRule_edge_hb_IsAllowedType = null;
		public static bool[] waitingRule_edge_req_IsAllowedType = null;
		public enum waitingRule_NodeNums { @r, @r2, @p1, @r1, @p2, };
		public enum waitingRule_EdgeNums { @b, @hb, @req, };
		public enum waitingRule_SubNums { };
		public enum waitingRule_AltNums { };

#if INITIAL_WARMUP
		public Rule_waitingRule()
#else
		private Rule_waitingRule()
#endif
		{
			name = "waitingRule";
			isSubpattern = false;

			PatternGraph waitingRule;
			PatternNode waitingRule_node_r = new PatternNode((int) NodeTypes.@Resource, "waitingRule_node_r", "r", waitingRule_node_r_AllowedTypes, waitingRule_node_r_IsAllowedType, 1.0F, -1);
			PatternNode waitingRule_node_r2 = new PatternNode((int) NodeTypes.@Resource, "waitingRule_node_r2", "r2", waitingRule_node_r2_AllowedTypes, waitingRule_node_r2_IsAllowedType, 5.5F, -1);
			PatternNode waitingRule_node_p1 = new PatternNode((int) NodeTypes.@Process, "waitingRule_node_p1", "p1", waitingRule_node_p1_AllowedTypes, waitingRule_node_p1_IsAllowedType, 5.5F, -1);
			PatternNode waitingRule_node_r1 = new PatternNode((int) NodeTypes.@Resource, "waitingRule_node_r1", "r1", waitingRule_node_r1_AllowedTypes, waitingRule_node_r1_IsAllowedType, 5.5F, -1);
			PatternNode waitingRule_node_p2 = new PatternNode((int) NodeTypes.@Process, "waitingRule_node_p2", "p2", waitingRule_node_p2_AllowedTypes, waitingRule_node_p2_IsAllowedType, 5.5F, -1);
			PatternEdge waitingRule_edge_b = new PatternEdge(waitingRule_node_r2, waitingRule_node_p1, (int) EdgeTypes.@blocked, "waitingRule_edge_b", "b", waitingRule_edge_b_AllowedTypes, waitingRule_edge_b_IsAllowedType, 5.5F, -1);
			PatternEdge waitingRule_edge_hb = new PatternEdge(waitingRule_node_r1, waitingRule_node_p1, (int) EdgeTypes.@held_by, "waitingRule_edge_hb", "hb", waitingRule_edge_hb_AllowedTypes, waitingRule_edge_hb_IsAllowedType, 5.5F, -1);
			PatternEdge waitingRule_edge_req = new PatternEdge(waitingRule_node_p2, waitingRule_node_r1, (int) EdgeTypes.@request, "waitingRule_edge_req", "req", waitingRule_edge_req_AllowedTypes, waitingRule_edge_req_IsAllowedType, 5.5F, -1);
			waitingRule = new PatternGraph(
				"waitingRule",
				"",
				new PatternNode[] { waitingRule_node_r, waitingRule_node_r2, waitingRule_node_p1, waitingRule_node_r1, waitingRule_node_p2 }, 
				new PatternEdge[] { waitingRule_edge_b, waitingRule_edge_hb, waitingRule_edge_req }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new Condition[] {  }, 
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
			waitingRule_node_r.PointOfDefinition = waitingRule;
			waitingRule_node_r2.PointOfDefinition = waitingRule;
			waitingRule_node_p1.PointOfDefinition = waitingRule;
			waitingRule_node_r1.PointOfDefinition = waitingRule;
			waitingRule_node_p2.PointOfDefinition = waitingRule;
			waitingRule_edge_b.PointOfDefinition = waitingRule;
			waitingRule_edge_hb.PointOfDefinition = waitingRule;
			waitingRule_edge_req.PointOfDefinition = waitingRule;

			patternGraph = waitingRule;

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
			outputNames = new string[] { };
		}


		public override IGraphElement[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_r2 = match.Nodes[ (int) waitingRule_NodeNums.@r2];
			LGSPNode node_p2 = match.Nodes[ (int) waitingRule_NodeNums.@p2];
			LGSPNode node_r = match.Nodes[ (int) waitingRule_NodeNums.@r];
			LGSPEdge edge_b = match.Edges[ (int) waitingRule_EdgeNums.@b];
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
			LGSPNode node_r2 = match.Nodes[ (int) waitingRule_NodeNums.@r2];
			LGSPNode node_p2 = match.Nodes[ (int) waitingRule_NodeNums.@p2];
			LGSPNode node_r = match.Nodes[ (int) waitingRule_NodeNums.@r];
			LGSPEdge edge_b = match.Edges[ (int) waitingRule_EdgeNums.@b];
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


    public class Action_aux_attachResource : LGSPAction
    {
        public Action_aux_attachResource() {
            rulePattern = Rule_aux_attachResource.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 0);
        }

        public override string Name { get { return "aux_attachResource"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_aux_attachResource instance = new Action_aux_attachResource();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int node_type_id_aux_attachResource_node_p = 0;
            for(LGSPNode node_head_aux_attachResource_node_p = graph.nodesByTypeHeads[node_type_id_aux_attachResource_node_p], node_cur_aux_attachResource_node_p = node_head_aux_attachResource_node_p.typeNext; node_cur_aux_attachResource_node_p != node_head_aux_attachResource_node_p; node_cur_aux_attachResource_node_p = node_cur_aux_attachResource_node_p.typeNext)
            {
                {
                    LGSPEdge edge_head_aux_attachResource_neg_0_edge__edge0 = node_cur_aux_attachResource_node_p.inhead;
                    if(edge_head_aux_attachResource_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge edge_cur_aux_attachResource_neg_0_edge__edge0 = edge_head_aux_attachResource_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_aux_attachResource_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            LGSPNode node_cur_aux_attachResource_neg_0_node_r = edge_cur_aux_attachResource_neg_0_edge__edge0.source;
                            if(!NodeType_Resource.isMyType[node_cur_aux_attachResource_neg_0_node_r.type.TypeID]) {
                                continue;
                            }
                            goto label0;
                        }
                        while( (edge_cur_aux_attachResource_neg_0_edge__edge0 = edge_cur_aux_attachResource_neg_0_edge__edge0.inNext) != edge_head_aux_attachResource_neg_0_edge__edge0 );
                    }
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_aux_attachResource.aux_attachResource_NodeNums.@p] = node_cur_aux_attachResource_node_p;
                matches.matchesList.PositionWasFilledFixIt();
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(node_cur_aux_attachResource_node_p);
                    return matches;
                }
label0: ;
            }
            return matches;
        }
    }

    public class Action_blockedRule : LGSPAction
    {
        public Action_blockedRule() {
            rulePattern = Rule_blockedRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2, 0);
        }

        public override string Name { get { return "blockedRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_blockedRule instance = new Action_blockedRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_blockedRule_edge_hb = 2;
            for(LGSPEdge edge_head_blockedRule_edge_hb = graph.edgesByTypeHeads[edge_type_id_blockedRule_edge_hb], edge_cur_blockedRule_edge_hb = edge_head_blockedRule_edge_hb.typeNext; edge_cur_blockedRule_edge_hb != edge_head_blockedRule_edge_hb; edge_cur_blockedRule_edge_hb = edge_cur_blockedRule_edge_hb.typeNext)
            {
                LGSPNode node_cur_blockedRule_node_r = edge_cur_blockedRule_edge_hb.source;
                if(!NodeType_Resource.isMyType[node_cur_blockedRule_node_r.type.TypeID]) {
                    continue;
                }
                LGSPNode node_cur_blockedRule_node_p2 = edge_cur_blockedRule_edge_hb.target;
                if(!NodeType_Process.isMyType[node_cur_blockedRule_node_p2.type.TypeID]) {
                    continue;
                }
                bool node_cur_blockedRule_node_p2_prevIsMatched = node_cur_blockedRule_node_p2.isMatched;
                node_cur_blockedRule_node_p2.isMatched = true;
                LGSPEdge edge_head_blockedRule_edge_req = node_cur_blockedRule_node_r.inhead;
                if(edge_head_blockedRule_edge_req != null)
                {
                    LGSPEdge edge_cur_blockedRule_edge_req = edge_head_blockedRule_edge_req;
                    do
                    {
                        if(!EdgeType_request.isMyType[edge_cur_blockedRule_edge_req.type.TypeID]) {
                            continue;
                        }
                        LGSPNode node_cur_blockedRule_node_p1 = edge_cur_blockedRule_edge_req.source;
                        if(!NodeType_Process.isMyType[node_cur_blockedRule_node_p1.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_blockedRule_node_p1.isMatched
                            && node_cur_blockedRule_node_p1==node_cur_blockedRule_node_p2
                            )
                        {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_blockedRule.blockedRule_NodeNums.@r] = node_cur_blockedRule_node_r;
                        match.Nodes[(int)Rule_blockedRule.blockedRule_NodeNums.@p1] = node_cur_blockedRule_node_p1;
                        match.Nodes[(int)Rule_blockedRule.blockedRule_NodeNums.@p2] = node_cur_blockedRule_node_p2;
                        match.Edges[(int)Rule_blockedRule.blockedRule_EdgeNums.@req] = edge_cur_blockedRule_edge_req;
                        match.Edges[(int)Rule_blockedRule.blockedRule_EdgeNums.@hb] = edge_cur_blockedRule_edge_hb;
                        matches.matchesList.PositionWasFilledFixIt();
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            node_cur_blockedRule_node_r.MoveInHeadAfter(edge_cur_blockedRule_edge_req);
                            graph.MoveHeadAfter(edge_cur_blockedRule_edge_hb);
                            node_cur_blockedRule_node_p2.isMatched = node_cur_blockedRule_node_p2_prevIsMatched;
                            return matches;
                        }
                    }
                    while( (edge_cur_blockedRule_edge_req = edge_cur_blockedRule_edge_req.inNext) != edge_head_blockedRule_edge_req );
                }
                node_cur_blockedRule_node_p2.isMatched = node_cur_blockedRule_node_p2_prevIsMatched;
            }
            return matches;
        }
    }

    public class Action_giveRule : LGSPAction
    {
        public Action_giveRule() {
            rulePattern = Rule_giveRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2, 0);
        }

        public override string Name { get { return "giveRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_giveRule instance = new Action_giveRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_giveRule_edge_n = 0;
            for(LGSPEdge edge_head_giveRule_edge_n = graph.edgesByTypeHeads[edge_type_id_giveRule_edge_n], edge_cur_giveRule_edge_n = edge_head_giveRule_edge_n.typeNext; edge_cur_giveRule_edge_n != edge_head_giveRule_edge_n; edge_cur_giveRule_edge_n = edge_cur_giveRule_edge_n.typeNext)
            {
                LGSPNode node_cur_giveRule_node_p1 = edge_cur_giveRule_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_giveRule_node_p1.type.TypeID]) {
                    continue;
                }
                bool node_cur_giveRule_node_p1_prevIsMatched = node_cur_giveRule_node_p1.isMatched;
                node_cur_giveRule_node_p1.isMatched = true;
                LGSPNode node_cur_giveRule_node_p2 = edge_cur_giveRule_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_giveRule_node_p2.type.TypeID]) {
                    node_cur_giveRule_node_p1.isMatched = node_cur_giveRule_node_p1_prevIsMatched;
                    continue;
                }
                if(node_cur_giveRule_node_p2.isMatched
                    && node_cur_giveRule_node_p2==node_cur_giveRule_node_p1
                    )
                {
                    node_cur_giveRule_node_p1.isMatched = node_cur_giveRule_node_p1_prevIsMatched;
                    continue;
                }
                LGSPEdge edge_head_giveRule_edge_rel = node_cur_giveRule_node_p1.inhead;
                if(edge_head_giveRule_edge_rel != null)
                {
                    LGSPEdge edge_cur_giveRule_edge_rel = edge_head_giveRule_edge_rel;
                    do
                    {
                        if(!EdgeType_release.isMyType[edge_cur_giveRule_edge_rel.type.TypeID]) {
                            continue;
                        }
                        LGSPNode node_cur_giveRule_node_r = edge_cur_giveRule_edge_rel.source;
                        if(!NodeType_Resource.isMyType[node_cur_giveRule_node_r.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_giveRule.giveRule_NodeNums.@r] = node_cur_giveRule_node_r;
                        match.Nodes[(int)Rule_giveRule.giveRule_NodeNums.@p1] = node_cur_giveRule_node_p1;
                        match.Nodes[(int)Rule_giveRule.giveRule_NodeNums.@p2] = node_cur_giveRule_node_p2;
                        match.Edges[(int)Rule_giveRule.giveRule_EdgeNums.@rel] = edge_cur_giveRule_edge_rel;
                        match.Edges[(int)Rule_giveRule.giveRule_EdgeNums.@n] = edge_cur_giveRule_edge_n;
                        matches.matchesList.PositionWasFilledFixIt();
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            node_cur_giveRule_node_p1.MoveInHeadAfter(edge_cur_giveRule_edge_rel);
                            graph.MoveHeadAfter(edge_cur_giveRule_edge_n);
                            node_cur_giveRule_node_p1.isMatched = node_cur_giveRule_node_p1_prevIsMatched;
                            return matches;
                        }
                    }
                    while( (edge_cur_giveRule_edge_rel = edge_cur_giveRule_edge_rel.inNext) != edge_head_giveRule_edge_rel );
                }
                node_cur_giveRule_node_p1.isMatched = node_cur_giveRule_node_p1_prevIsMatched;
            }
            return matches;
        }
    }

    public class Action_ignoreRule : LGSPAction
    {
        public Action_ignoreRule() {
            rulePattern = Rule_ignoreRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0);
        }

        public override string Name { get { return "ignoreRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_ignoreRule instance = new Action_ignoreRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_ignoreRule_edge_b = 1;
            for(LGSPEdge edge_head_ignoreRule_edge_b = graph.edgesByTypeHeads[edge_type_id_ignoreRule_edge_b], edge_cur_ignoreRule_edge_b = edge_head_ignoreRule_edge_b.typeNext; edge_cur_ignoreRule_edge_b != edge_head_ignoreRule_edge_b; edge_cur_ignoreRule_edge_b = edge_cur_ignoreRule_edge_b.typeNext)
            {
                LGSPNode node_cur_ignoreRule_node_r = edge_cur_ignoreRule_edge_b.source;
                if(!NodeType_Resource.isMyType[node_cur_ignoreRule_node_r.type.TypeID]) {
                    continue;
                }
                LGSPNode node_cur_ignoreRule_node_p = edge_cur_ignoreRule_edge_b.target;
                if(!NodeType_Process.isMyType[node_cur_ignoreRule_node_p.type.TypeID]) {
                    continue;
                }
                {
                    LGSPEdge edge_head_ignoreRule_neg_0_edge_hb = node_cur_ignoreRule_node_p.inhead;
                    if(edge_head_ignoreRule_neg_0_edge_hb != null)
                    {
                        LGSPEdge edge_cur_ignoreRule_neg_0_edge_hb = edge_head_ignoreRule_neg_0_edge_hb;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_ignoreRule_neg_0_edge_hb.type.TypeID]) {
                                continue;
                            }
                            LGSPNode node_cur_ignoreRule_neg_0_node_m = edge_cur_ignoreRule_neg_0_edge_hb.source;
                            if(!NodeType_Resource.isMyType[node_cur_ignoreRule_neg_0_node_m.type.TypeID]) {
                                continue;
                            }
                            goto label1;
                        }
                        while( (edge_cur_ignoreRule_neg_0_edge_hb = edge_cur_ignoreRule_neg_0_edge_hb.inNext) != edge_head_ignoreRule_neg_0_edge_hb );
                    }
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_ignoreRule.ignoreRule_NodeNums.@r] = node_cur_ignoreRule_node_r;
                match.Nodes[(int)Rule_ignoreRule.ignoreRule_NodeNums.@p] = node_cur_ignoreRule_node_p;
                match.Edges[(int)Rule_ignoreRule.ignoreRule_EdgeNums.@b] = edge_cur_ignoreRule_edge_b;
                matches.matchesList.PositionWasFilledFixIt();
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_ignoreRule_edge_b);
                    return matches;
                }
label1: ;
            }
            return matches;
        }
    }

    public class Action_killRule : LGSPAction
    {
        public Action_killRule() {
            rulePattern = Rule_killRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2, 0);
        }

        public override string Name { get { return "killRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_killRule instance = new Action_killRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_killRule_edge_n2 = 0;
            for(LGSPEdge edge_head_killRule_edge_n2 = graph.edgesByTypeHeads[edge_type_id_killRule_edge_n2], edge_cur_killRule_edge_n2 = edge_head_killRule_edge_n2.typeNext; edge_cur_killRule_edge_n2 != edge_head_killRule_edge_n2; edge_cur_killRule_edge_n2 = edge_cur_killRule_edge_n2.typeNext)
            {
                bool edge_cur_killRule_edge_n2_prevIsMatched = edge_cur_killRule_edge_n2.isMatched;
                edge_cur_killRule_edge_n2.isMatched = true;
                LGSPNode node_cur_killRule_node_p = edge_cur_killRule_edge_n2.source;
                if(!NodeType_Process.isMyType[node_cur_killRule_node_p.type.TypeID]) {
                    edge_cur_killRule_edge_n2.isMatched = edge_cur_killRule_edge_n2_prevIsMatched;
                    continue;
                }
                bool node_cur_killRule_node_p_prevIsMatched = node_cur_killRule_node_p.isMatched;
                node_cur_killRule_node_p.isMatched = true;
                LGSPNode node_cur_killRule_node_p2 = edge_cur_killRule_edge_n2.target;
                if(!NodeType_Process.isMyType[node_cur_killRule_node_p2.type.TypeID]) {
                    node_cur_killRule_node_p.isMatched = node_cur_killRule_node_p_prevIsMatched;
                    edge_cur_killRule_edge_n2.isMatched = edge_cur_killRule_edge_n2_prevIsMatched;
                    continue;
                }
                if(node_cur_killRule_node_p2.isMatched
                    && node_cur_killRule_node_p2==node_cur_killRule_node_p
                    )
                {
                    node_cur_killRule_node_p.isMatched = node_cur_killRule_node_p_prevIsMatched;
                    edge_cur_killRule_edge_n2.isMatched = edge_cur_killRule_edge_n2_prevIsMatched;
                    continue;
                }
                bool node_cur_killRule_node_p2_prevIsMatched = node_cur_killRule_node_p2.isMatched;
                node_cur_killRule_node_p2.isMatched = true;
                LGSPEdge edge_head_killRule_edge_n1 = node_cur_killRule_node_p.inhead;
                if(edge_head_killRule_edge_n1 != null)
                {
                    LGSPEdge edge_cur_killRule_edge_n1 = edge_head_killRule_edge_n1;
                    do
                    {
                        if(!EdgeType_next.isMyType[edge_cur_killRule_edge_n1.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_killRule_edge_n1.isMatched
                            && edge_cur_killRule_edge_n1==edge_cur_killRule_edge_n2
                            )
                        {
                            continue;
                        }
                        LGSPNode node_cur_killRule_node_p1 = edge_cur_killRule_edge_n1.source;
                        if(!NodeType_Process.isMyType[node_cur_killRule_node_p1.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_killRule_node_p1.isMatched
                            && (node_cur_killRule_node_p1==node_cur_killRule_node_p
                                || node_cur_killRule_node_p1==node_cur_killRule_node_p2
                                )
                            )
                        {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_killRule.killRule_NodeNums.@p1] = node_cur_killRule_node_p1;
                        match.Nodes[(int)Rule_killRule.killRule_NodeNums.@p] = node_cur_killRule_node_p;
                        match.Nodes[(int)Rule_killRule.killRule_NodeNums.@p2] = node_cur_killRule_node_p2;
                        match.Edges[(int)Rule_killRule.killRule_EdgeNums.@n1] = edge_cur_killRule_edge_n1;
                        match.Edges[(int)Rule_killRule.killRule_EdgeNums.@n2] = edge_cur_killRule_edge_n2;
                        matches.matchesList.PositionWasFilledFixIt();
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            node_cur_killRule_node_p.MoveInHeadAfter(edge_cur_killRule_edge_n1);
                            graph.MoveHeadAfter(edge_cur_killRule_edge_n2);
                            node_cur_killRule_node_p2.isMatched = node_cur_killRule_node_p2_prevIsMatched;
                            node_cur_killRule_node_p.isMatched = node_cur_killRule_node_p_prevIsMatched;
                            edge_cur_killRule_edge_n2.isMatched = edge_cur_killRule_edge_n2_prevIsMatched;
                            return matches;
                        }
                    }
                    while( (edge_cur_killRule_edge_n1 = edge_cur_killRule_edge_n1.inNext) != edge_head_killRule_edge_n1 );
                }
                node_cur_killRule_node_p2.isMatched = node_cur_killRule_node_p2_prevIsMatched;
                node_cur_killRule_node_p.isMatched = node_cur_killRule_node_p_prevIsMatched;
                edge_cur_killRule_edge_n2.isMatched = edge_cur_killRule_edge_n2_prevIsMatched;
            }
            return matches;
        }
    }

    public class Action_mountRule : LGSPAction
    {
        public Action_mountRule() {
            rulePattern = Rule_mountRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 1, 0, 0);
        }

        public override string Name { get { return "mountRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_mountRule instance = new Action_mountRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int node_type_id_mountRule_node_p = 0;
            for(LGSPNode node_head_mountRule_node_p = graph.nodesByTypeHeads[node_type_id_mountRule_node_p], node_cur_mountRule_node_p = node_head_mountRule_node_p.typeNext; node_cur_mountRule_node_p != node_head_mountRule_node_p; node_cur_mountRule_node_p = node_cur_mountRule_node_p.typeNext)
            {
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_mountRule.mountRule_NodeNums.@p] = node_cur_mountRule_node_p;
                matches.matchesList.PositionWasFilledFixIt();
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(node_cur_mountRule_node_p);
                    return matches;
                }
            }
            return matches;
        }
    }

    public class Action_newRule : LGSPAction
    {
        public Action_newRule() {
            rulePattern = Rule_newRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0);
        }

        public override string Name { get { return "newRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_newRule instance = new Action_newRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_newRule_edge_n = 0;
            for(LGSPEdge edge_head_newRule_edge_n = graph.edgesByTypeHeads[edge_type_id_newRule_edge_n], edge_cur_newRule_edge_n = edge_head_newRule_edge_n.typeNext; edge_cur_newRule_edge_n != edge_head_newRule_edge_n; edge_cur_newRule_edge_n = edge_cur_newRule_edge_n.typeNext)
            {
                LGSPNode node_cur_newRule_node_p1 = edge_cur_newRule_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_newRule_node_p1.type.TypeID]) {
                    continue;
                }
                bool node_cur_newRule_node_p1_prevIsMatched = node_cur_newRule_node_p1.isMatched;
                node_cur_newRule_node_p1.isMatched = true;
                LGSPNode node_cur_newRule_node_p2 = edge_cur_newRule_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_newRule_node_p2.type.TypeID]) {
                    node_cur_newRule_node_p1.isMatched = node_cur_newRule_node_p1_prevIsMatched;
                    continue;
                }
                if(node_cur_newRule_node_p2.isMatched
                    && node_cur_newRule_node_p2==node_cur_newRule_node_p1
                    )
                {
                    node_cur_newRule_node_p1.isMatched = node_cur_newRule_node_p1_prevIsMatched;
                    continue;
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_newRule.newRule_NodeNums.@p1] = node_cur_newRule_node_p1;
                match.Nodes[(int)Rule_newRule.newRule_NodeNums.@p2] = node_cur_newRule_node_p2;
                match.Edges[(int)Rule_newRule.newRule_EdgeNums.@n] = edge_cur_newRule_edge_n;
                matches.matchesList.PositionWasFilledFixIt();
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_newRule_edge_n);
                    node_cur_newRule_node_p1.isMatched = node_cur_newRule_node_p1_prevIsMatched;
                    return matches;
                }
                node_cur_newRule_node_p1.isMatched = node_cur_newRule_node_p1_prevIsMatched;
            }
            return matches;
        }
    }

    public class Action_passRule : LGSPAction
    {
        public Action_passRule() {
            rulePattern = Rule_passRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2, 0);
        }

        public override string Name { get { return "passRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_passRule instance = new Action_passRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_passRule_edge_n = 0;
            for(LGSPEdge edge_head_passRule_edge_n = graph.edgesByTypeHeads[edge_type_id_passRule_edge_n], edge_cur_passRule_edge_n = edge_head_passRule_edge_n.typeNext; edge_cur_passRule_edge_n != edge_head_passRule_edge_n; edge_cur_passRule_edge_n = edge_cur_passRule_edge_n.typeNext)
            {
                LGSPNode node_cur_passRule_node_p1 = edge_cur_passRule_edge_n.source;
                if(!NodeType_Process.isMyType[node_cur_passRule_node_p1.type.TypeID]) {
                    continue;
                }
                bool node_cur_passRule_node_p1_prevIsMatched = node_cur_passRule_node_p1.isMatched;
                node_cur_passRule_node_p1.isMatched = true;
                LGSPNode node_cur_passRule_node_p2 = edge_cur_passRule_edge_n.target;
                if(!NodeType_Process.isMyType[node_cur_passRule_node_p2.type.TypeID]) {
                    node_cur_passRule_node_p1.isMatched = node_cur_passRule_node_p1_prevIsMatched;
                    continue;
                }
                if(node_cur_passRule_node_p2.isMatched
                    && node_cur_passRule_node_p2==node_cur_passRule_node_p1
                    )
                {
                    node_cur_passRule_node_p1.isMatched = node_cur_passRule_node_p1_prevIsMatched;
                    continue;
                }
                LGSPEdge edge_head_passRule_edge__edge0 = node_cur_passRule_node_p1.inhead;
                if(edge_head_passRule_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_passRule_edge__edge0 = edge_head_passRule_edge__edge0;
                    do
                    {
                        if(!EdgeType_token.isMyType[edge_cur_passRule_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        LGSPNode node_cur_passRule_node_r = edge_cur_passRule_edge__edge0.source;
                        if(!NodeType_Resource.isMyType[node_cur_passRule_node_r.type.TypeID]) {
                            continue;
                        }
                        {
                            LGSPEdge edge_head_passRule_neg_0_edge_req = node_cur_passRule_node_p1.outhead;
                            if(edge_head_passRule_neg_0_edge_req != null)
                            {
                                LGSPEdge edge_cur_passRule_neg_0_edge_req = edge_head_passRule_neg_0_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_passRule_neg_0_edge_req.type.TypeID]) {
                                        continue;
                                    }
                                    if(edge_cur_passRule_neg_0_edge_req.target != node_cur_passRule_node_r) {
                                        continue;
                                    }
                                    goto label2;
                                }
                                while( (edge_cur_passRule_neg_0_edge_req = edge_cur_passRule_neg_0_edge_req.outNext) != edge_head_passRule_neg_0_edge_req );
                            }
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_passRule.passRule_NodeNums.@r] = node_cur_passRule_node_r;
                        match.Nodes[(int)Rule_passRule.passRule_NodeNums.@p1] = node_cur_passRule_node_p1;
                        match.Nodes[(int)Rule_passRule.passRule_NodeNums.@p2] = node_cur_passRule_node_p2;
                        match.Edges[(int)Rule_passRule.passRule_EdgeNums.@_edge0] = edge_cur_passRule_edge__edge0;
                        match.Edges[(int)Rule_passRule.passRule_EdgeNums.@n] = edge_cur_passRule_edge_n;
                        matches.matchesList.PositionWasFilledFixIt();
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            node_cur_passRule_node_p1.MoveInHeadAfter(edge_cur_passRule_edge__edge0);
                            graph.MoveHeadAfter(edge_cur_passRule_edge_n);
                            node_cur_passRule_node_p1.isMatched = node_cur_passRule_node_p1_prevIsMatched;
                            return matches;
                        }
label2: ;
                    }
                    while( (edge_cur_passRule_edge__edge0 = edge_cur_passRule_edge__edge0.inNext) != edge_head_passRule_edge__edge0 );
                }
                node_cur_passRule_node_p1.isMatched = node_cur_passRule_node_p1_prevIsMatched;
            }
            return matches;
        }
    }

    public class Action_releaseRule : LGSPAction
    {
        public Action_releaseRule() {
            rulePattern = Rule_releaseRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0);
        }

        public override string Name { get { return "releaseRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_releaseRule instance = new Action_releaseRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_releaseRule_edge_hb = 2;
            for(LGSPEdge edge_head_releaseRule_edge_hb = graph.edgesByTypeHeads[edge_type_id_releaseRule_edge_hb], edge_cur_releaseRule_edge_hb = edge_head_releaseRule_edge_hb.typeNext; edge_cur_releaseRule_edge_hb != edge_head_releaseRule_edge_hb; edge_cur_releaseRule_edge_hb = edge_cur_releaseRule_edge_hb.typeNext)
            {
                LGSPNode node_cur_releaseRule_node_r = edge_cur_releaseRule_edge_hb.source;
                if(!NodeType_Resource.isMyType[node_cur_releaseRule_node_r.type.TypeID]) {
                    continue;
                }
                LGSPNode node_cur_releaseRule_node_p = edge_cur_releaseRule_edge_hb.target;
                if(!NodeType_Process.isMyType[node_cur_releaseRule_node_p.type.TypeID]) {
                    continue;
                }
                {
                    LGSPEdge edge_head_releaseRule_neg_0_edge_req = node_cur_releaseRule_node_p.outhead;
                    if(edge_head_releaseRule_neg_0_edge_req != null)
                    {
                        LGSPEdge edge_cur_releaseRule_neg_0_edge_req = edge_head_releaseRule_neg_0_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_releaseRule_neg_0_edge_req.type.TypeID]) {
                                continue;
                            }
                            LGSPNode node_cur_releaseRule_neg_0_node_m = edge_cur_releaseRule_neg_0_edge_req.target;
                            if(!NodeType_Resource.isMyType[node_cur_releaseRule_neg_0_node_m.type.TypeID]) {
                                continue;
                            }
                            goto label3;
                        }
                        while( (edge_cur_releaseRule_neg_0_edge_req = edge_cur_releaseRule_neg_0_edge_req.outNext) != edge_head_releaseRule_neg_0_edge_req );
                    }
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_releaseRule.releaseRule_NodeNums.@r] = node_cur_releaseRule_node_r;
                match.Nodes[(int)Rule_releaseRule.releaseRule_NodeNums.@p] = node_cur_releaseRule_node_p;
                match.Edges[(int)Rule_releaseRule.releaseRule_EdgeNums.@hb] = edge_cur_releaseRule_edge_hb;
                matches.matchesList.PositionWasFilledFixIt();
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_releaseRule_edge_hb);
                    return matches;
                }
label3: ;
            }
            return matches;
        }
    }

    public class Action_releaseStarRule : LGSPAction
    {
        public Action_releaseStarRule() {
            rulePattern = Rule_releaseStarRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 4, 3, 0);
        }

        public override string Name { get { return "releaseStarRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_releaseStarRule instance = new Action_releaseStarRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_releaseStarRule_edge_h1 = 2;
            for(LGSPEdge edge_head_releaseStarRule_edge_h1 = graph.edgesByTypeHeads[edge_type_id_releaseStarRule_edge_h1], edge_cur_releaseStarRule_edge_h1 = edge_head_releaseStarRule_edge_h1.typeNext; edge_cur_releaseStarRule_edge_h1 != edge_head_releaseStarRule_edge_h1; edge_cur_releaseStarRule_edge_h1 = edge_cur_releaseStarRule_edge_h1.typeNext)
            {
                bool edge_cur_releaseStarRule_edge_h1_prevIsMatched = edge_cur_releaseStarRule_edge_h1.isMatched;
                edge_cur_releaseStarRule_edge_h1.isMatched = true;
                LGSPNode node_cur_releaseStarRule_node_r1 = edge_cur_releaseStarRule_edge_h1.source;
                if(!NodeType_Resource.isMyType[node_cur_releaseStarRule_node_r1.type.TypeID]) {
                    edge_cur_releaseStarRule_edge_h1.isMatched = edge_cur_releaseStarRule_edge_h1_prevIsMatched;
                    continue;
                }
                bool node_cur_releaseStarRule_node_r1_prevIsMatched = node_cur_releaseStarRule_node_r1.isMatched;
                node_cur_releaseStarRule_node_r1.isMatched = true;
                LGSPNode node_cur_releaseStarRule_node_p2 = edge_cur_releaseStarRule_edge_h1.target;
                if(!NodeType_Process.isMyType[node_cur_releaseStarRule_node_p2.type.TypeID]) {
                    node_cur_releaseStarRule_node_r1.isMatched = node_cur_releaseStarRule_node_r1_prevIsMatched;
                    edge_cur_releaseStarRule_edge_h1.isMatched = edge_cur_releaseStarRule_edge_h1_prevIsMatched;
                    continue;
                }
                bool node_cur_releaseStarRule_node_p2_prevIsMatched = node_cur_releaseStarRule_node_p2.isMatched;
                node_cur_releaseStarRule_node_p2.isMatched = true;
                LGSPEdge edge_head_releaseStarRule_edge_rq = node_cur_releaseStarRule_node_r1.inhead;
                if(edge_head_releaseStarRule_edge_rq != null)
                {
                    LGSPEdge edge_cur_releaseStarRule_edge_rq = edge_head_releaseStarRule_edge_rq;
                    do
                    {
                        if(!EdgeType_request.isMyType[edge_cur_releaseStarRule_edge_rq.type.TypeID]) {
                            continue;
                        }
                        LGSPNode node_cur_releaseStarRule_node_p1 = edge_cur_releaseStarRule_edge_rq.source;
                        if(!NodeType_Process.isMyType[node_cur_releaseStarRule_node_p1.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_releaseStarRule_node_p1.isMatched
                            && node_cur_releaseStarRule_node_p1==node_cur_releaseStarRule_node_p2
                            )
                        {
                            continue;
                        }
                        LGSPEdge edge_head_releaseStarRule_edge_h2 = node_cur_releaseStarRule_node_p2.inhead;
                        if(edge_head_releaseStarRule_edge_h2 != null)
                        {
                            LGSPEdge edge_cur_releaseStarRule_edge_h2 = edge_head_releaseStarRule_edge_h2;
                            do
                            {
                                if(!EdgeType_held_by.isMyType[edge_cur_releaseStarRule_edge_h2.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_releaseStarRule_edge_h2.isMatched
                                    && edge_cur_releaseStarRule_edge_h2==edge_cur_releaseStarRule_edge_h1
                                    )
                                {
                                    continue;
                                }
                                LGSPNode node_cur_releaseStarRule_node_r2 = edge_cur_releaseStarRule_edge_h2.source;
                                if(!NodeType_Resource.isMyType[node_cur_releaseStarRule_node_r2.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_releaseStarRule_node_r2.isMatched
                                    && node_cur_releaseStarRule_node_r2==node_cur_releaseStarRule_node_r1
                                    )
                                {
                                    continue;
                                }
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_releaseStarRule.releaseStarRule_NodeNums.@p1] = node_cur_releaseStarRule_node_p1;
                                match.Nodes[(int)Rule_releaseStarRule.releaseStarRule_NodeNums.@r1] = node_cur_releaseStarRule_node_r1;
                                match.Nodes[(int)Rule_releaseStarRule.releaseStarRule_NodeNums.@p2] = node_cur_releaseStarRule_node_p2;
                                match.Nodes[(int)Rule_releaseStarRule.releaseStarRule_NodeNums.@r2] = node_cur_releaseStarRule_node_r2;
                                match.Edges[(int)Rule_releaseStarRule.releaseStarRule_EdgeNums.@rq] = edge_cur_releaseStarRule_edge_rq;
                                match.Edges[(int)Rule_releaseStarRule.releaseStarRule_EdgeNums.@h1] = edge_cur_releaseStarRule_edge_h1;
                                match.Edges[(int)Rule_releaseStarRule.releaseStarRule_EdgeNums.@h2] = edge_cur_releaseStarRule_edge_h2;
                                matches.matchesList.PositionWasFilledFixIt();
                                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                                {
                                    node_cur_releaseStarRule_node_p2.MoveInHeadAfter(edge_cur_releaseStarRule_edge_h2);
                                    node_cur_releaseStarRule_node_r1.MoveInHeadAfter(edge_cur_releaseStarRule_edge_rq);
                                    graph.MoveHeadAfter(edge_cur_releaseStarRule_edge_h1);
                                    node_cur_releaseStarRule_node_p2.isMatched = node_cur_releaseStarRule_node_p2_prevIsMatched;
                                    node_cur_releaseStarRule_node_r1.isMatched = node_cur_releaseStarRule_node_r1_prevIsMatched;
                                    edge_cur_releaseStarRule_edge_h1.isMatched = edge_cur_releaseStarRule_edge_h1_prevIsMatched;
                                    return matches;
                                }
                            }
                            while( (edge_cur_releaseStarRule_edge_h2 = edge_cur_releaseStarRule_edge_h2.inNext) != edge_head_releaseStarRule_edge_h2 );
                        }
                    }
                    while( (edge_cur_releaseStarRule_edge_rq = edge_cur_releaseStarRule_edge_rq.inNext) != edge_head_releaseStarRule_edge_rq );
                }
                node_cur_releaseStarRule_node_p2.isMatched = node_cur_releaseStarRule_node_p2_prevIsMatched;
                node_cur_releaseStarRule_node_r1.isMatched = node_cur_releaseStarRule_node_r1_prevIsMatched;
                edge_cur_releaseStarRule_edge_h1.isMatched = edge_cur_releaseStarRule_edge_h1_prevIsMatched;
            }
            return matches;
        }
    }

    public class Action_requestRule : LGSPAction
    {
        public Action_requestRule() {
            rulePattern = Rule_requestRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 0, 0);
        }

        public override string Name { get { return "requestRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_requestRule instance = new Action_requestRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int node_type_id_requestRule_node_r = 1;
            for(LGSPNode node_head_requestRule_node_r = graph.nodesByTypeHeads[node_type_id_requestRule_node_r], node_cur_requestRule_node_r = node_head_requestRule_node_r.typeNext; node_cur_requestRule_node_r != node_head_requestRule_node_r; node_cur_requestRule_node_r = node_cur_requestRule_node_r.typeNext)
            {
                int node_type_id_requestRule_node_p = 0;
                for(LGSPNode node_head_requestRule_node_p = graph.nodesByTypeHeads[node_type_id_requestRule_node_p], node_cur_requestRule_node_p = node_head_requestRule_node_p.typeNext; node_cur_requestRule_node_p != node_head_requestRule_node_p; node_cur_requestRule_node_p = node_cur_requestRule_node_p.typeNext)
                {
                    {
                        LGSPEdge edge_head_requestRule_neg_0_edge_hb = node_cur_requestRule_node_r.outhead;
                        if(edge_head_requestRule_neg_0_edge_hb != null)
                        {
                            LGSPEdge edge_cur_requestRule_neg_0_edge_hb = edge_head_requestRule_neg_0_edge_hb;
                            do
                            {
                                if(!EdgeType_held_by.isMyType[edge_cur_requestRule_neg_0_edge_hb.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_requestRule_neg_0_edge_hb.target != node_cur_requestRule_node_p) {
                                    continue;
                                }
                                goto label4;
                            }
                            while( (edge_cur_requestRule_neg_0_edge_hb = edge_cur_requestRule_neg_0_edge_hb.outNext) != edge_head_requestRule_neg_0_edge_hb );
                        }
                    }
                    {
                        LGSPEdge edge_head_requestRule_neg_1_edge_req = node_cur_requestRule_node_p.outhead;
                        if(edge_head_requestRule_neg_1_edge_req != null)
                        {
                            LGSPEdge edge_cur_requestRule_neg_1_edge_req = edge_head_requestRule_neg_1_edge_req;
                            do
                            {
                                if(!EdgeType_request.isMyType[edge_cur_requestRule_neg_1_edge_req.type.TypeID]) {
                                    continue;
                                }
                                LGSPNode node_cur_requestRule_neg_1_node_m = edge_cur_requestRule_neg_1_edge_req.target;
                                if(!NodeType_Resource.isMyType[node_cur_requestRule_neg_1_node_m.type.TypeID]) {
                                    continue;
                                }
                                goto label5;
                            }
                            while( (edge_cur_requestRule_neg_1_edge_req = edge_cur_requestRule_neg_1_edge_req.outNext) != edge_head_requestRule_neg_1_edge_req );
                        }
                    }
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_requestRule.requestRule_NodeNums.@p] = node_cur_requestRule_node_p;
                    match.Nodes[(int)Rule_requestRule.requestRule_NodeNums.@r] = node_cur_requestRule_node_r;
                    matches.matchesList.PositionWasFilledFixIt();
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(node_cur_requestRule_node_p);
                        graph.MoveHeadAfter(node_cur_requestRule_node_r);
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
        public Action_requestSimpleRule() {
            rulePattern = Rule_requestSimpleRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0);
        }

        public override string Name { get { return "requestSimpleRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_requestSimpleRule instance = new Action_requestSimpleRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_requestSimpleRule_edge_t = 3;
            for(LGSPEdge edge_head_requestSimpleRule_edge_t = graph.edgesByTypeHeads[edge_type_id_requestSimpleRule_edge_t], edge_cur_requestSimpleRule_edge_t = edge_head_requestSimpleRule_edge_t.typeNext; edge_cur_requestSimpleRule_edge_t != edge_head_requestSimpleRule_edge_t; edge_cur_requestSimpleRule_edge_t = edge_cur_requestSimpleRule_edge_t.typeNext)
            {
                LGSPNode node_cur_requestSimpleRule_node_r = edge_cur_requestSimpleRule_edge_t.source;
                if(!NodeType_Resource.isMyType[node_cur_requestSimpleRule_node_r.type.TypeID]) {
                    continue;
                }
                LGSPNode node_cur_requestSimpleRule_node_p = edge_cur_requestSimpleRule_edge_t.target;
                if(!NodeType_Process.isMyType[node_cur_requestSimpleRule_node_p.type.TypeID]) {
                    continue;
                }
                {
                    LGSPEdge edge_head_requestSimpleRule_neg_0_edge_req = node_cur_requestSimpleRule_node_p.outhead;
                    if(edge_head_requestSimpleRule_neg_0_edge_req != null)
                    {
                        LGSPEdge edge_cur_requestSimpleRule_neg_0_edge_req = edge_head_requestSimpleRule_neg_0_edge_req;
                        do
                        {
                            if(!EdgeType_request.isMyType[edge_cur_requestSimpleRule_neg_0_edge_req.type.TypeID]) {
                                continue;
                            }
                            if(edge_cur_requestSimpleRule_neg_0_edge_req.target != node_cur_requestSimpleRule_node_r) {
                                continue;
                            }
                            goto label6;
                        }
                        while( (edge_cur_requestSimpleRule_neg_0_edge_req = edge_cur_requestSimpleRule_neg_0_edge_req.outNext) != edge_head_requestSimpleRule_neg_0_edge_req );
                    }
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_requestSimpleRule.requestSimpleRule_NodeNums.@r] = node_cur_requestSimpleRule_node_r;
                match.Nodes[(int)Rule_requestSimpleRule.requestSimpleRule_NodeNums.@p] = node_cur_requestSimpleRule_node_p;
                match.Edges[(int)Rule_requestSimpleRule.requestSimpleRule_EdgeNums.@t] = edge_cur_requestSimpleRule_edge_t;
                matches.matchesList.PositionWasFilledFixIt();
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_requestSimpleRule_edge_t);
                    return matches;
                }
label6: ;
            }
            return matches;
        }
    }

    public class Action_requestStarRule : LGSPAction
    {
        public Action_requestStarRule() {
            rulePattern = Rule_requestStarRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 4, 3, 0);
        }

        public override string Name { get { return "requestStarRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_requestStarRule instance = new Action_requestStarRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_requestStarRule_edge_h1 = 2;
            for(LGSPEdge edge_head_requestStarRule_edge_h1 = graph.edgesByTypeHeads[edge_type_id_requestStarRule_edge_h1], edge_cur_requestStarRule_edge_h1 = edge_head_requestStarRule_edge_h1.typeNext; edge_cur_requestStarRule_edge_h1 != edge_head_requestStarRule_edge_h1; edge_cur_requestStarRule_edge_h1 = edge_cur_requestStarRule_edge_h1.typeNext)
            {
                bool edge_cur_requestStarRule_edge_h1_prevIsMatched = edge_cur_requestStarRule_edge_h1.isMatched;
                edge_cur_requestStarRule_edge_h1.isMatched = true;
                LGSPNode node_cur_requestStarRule_node_r1 = edge_cur_requestStarRule_edge_h1.source;
                if(!NodeType_Resource.isMyType[node_cur_requestStarRule_node_r1.type.TypeID]) {
                    edge_cur_requestStarRule_edge_h1.isMatched = edge_cur_requestStarRule_edge_h1_prevIsMatched;
                    continue;
                }
                bool node_cur_requestStarRule_node_r1_prevIsMatched = node_cur_requestStarRule_node_r1.isMatched;
                node_cur_requestStarRule_node_r1.isMatched = true;
                LGSPNode node_cur_requestStarRule_node_p1 = edge_cur_requestStarRule_edge_h1.target;
                if(!NodeType_Process.isMyType[node_cur_requestStarRule_node_p1.type.TypeID]) {
                    node_cur_requestStarRule_node_r1.isMatched = node_cur_requestStarRule_node_r1_prevIsMatched;
                    edge_cur_requestStarRule_edge_h1.isMatched = edge_cur_requestStarRule_edge_h1_prevIsMatched;
                    continue;
                }
                bool node_cur_requestStarRule_node_p1_prevIsMatched = node_cur_requestStarRule_node_p1.isMatched;
                node_cur_requestStarRule_node_p1.isMatched = true;
                LGSPEdge edge_head_requestStarRule_edge_n = node_cur_requestStarRule_node_p1.inhead;
                if(edge_head_requestStarRule_edge_n != null)
                {
                    LGSPEdge edge_cur_requestStarRule_edge_n = edge_head_requestStarRule_edge_n;
                    do
                    {
                        if(!EdgeType_next.isMyType[edge_cur_requestStarRule_edge_n.type.TypeID]) {
                            continue;
                        }
                        LGSPNode node_cur_requestStarRule_node_p2 = edge_cur_requestStarRule_edge_n.source;
                        if(!NodeType_Process.isMyType[node_cur_requestStarRule_node_p2.type.TypeID]) {
                            continue;
                        }
                        if(node_cur_requestStarRule_node_p2.isMatched
                            && node_cur_requestStarRule_node_p2==node_cur_requestStarRule_node_p1
                            )
                        {
                            continue;
                        }
                        LGSPEdge edge_head_requestStarRule_edge_h2 = node_cur_requestStarRule_node_p2.inhead;
                        if(edge_head_requestStarRule_edge_h2 != null)
                        {
                            LGSPEdge edge_cur_requestStarRule_edge_h2 = edge_head_requestStarRule_edge_h2;
                            do
                            {
                                if(!EdgeType_held_by.isMyType[edge_cur_requestStarRule_edge_h2.type.TypeID]) {
                                    continue;
                                }
                                if(edge_cur_requestStarRule_edge_h2.isMatched
                                    && edge_cur_requestStarRule_edge_h2==edge_cur_requestStarRule_edge_h1
                                    )
                                {
                                    continue;
                                }
                                LGSPNode node_cur_requestStarRule_node_r2 = edge_cur_requestStarRule_edge_h2.source;
                                if(!NodeType_Resource.isMyType[node_cur_requestStarRule_node_r2.type.TypeID]) {
                                    continue;
                                }
                                if(node_cur_requestStarRule_node_r2.isMatched
                                    && node_cur_requestStarRule_node_r2==node_cur_requestStarRule_node_r1
                                    )
                                {
                                    continue;
                                }
                                {
                                    LGSPEdge edge_head_requestStarRule_neg_0_edge_req = node_cur_requestStarRule_node_p1.outhead;
                                    if(edge_head_requestStarRule_neg_0_edge_req != null)
                                    {
                                        LGSPEdge edge_cur_requestStarRule_neg_0_edge_req = edge_head_requestStarRule_neg_0_edge_req;
                                        do
                                        {
                                            if(!EdgeType_request.isMyType[edge_cur_requestStarRule_neg_0_edge_req.type.TypeID]) {
                                                continue;
                                            }
                                            if(edge_cur_requestStarRule_neg_0_edge_req.target != node_cur_requestStarRule_node_r2) {
                                                continue;
                                            }
                                            goto label7;
                                        }
                                        while( (edge_cur_requestStarRule_neg_0_edge_req = edge_cur_requestStarRule_neg_0_edge_req.outNext) != edge_head_requestStarRule_neg_0_edge_req );
                                    }
                                }
                                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                match.patternGraph = rulePattern.patternGraph;
                                match.Nodes[(int)Rule_requestStarRule.requestStarRule_NodeNums.@r1] = node_cur_requestStarRule_node_r1;
                                match.Nodes[(int)Rule_requestStarRule.requestStarRule_NodeNums.@p1] = node_cur_requestStarRule_node_p1;
                                match.Nodes[(int)Rule_requestStarRule.requestStarRule_NodeNums.@p2] = node_cur_requestStarRule_node_p2;
                                match.Nodes[(int)Rule_requestStarRule.requestStarRule_NodeNums.@r2] = node_cur_requestStarRule_node_r2;
                                match.Edges[(int)Rule_requestStarRule.requestStarRule_EdgeNums.@h1] = edge_cur_requestStarRule_edge_h1;
                                match.Edges[(int)Rule_requestStarRule.requestStarRule_EdgeNums.@n] = edge_cur_requestStarRule_edge_n;
                                match.Edges[(int)Rule_requestStarRule.requestStarRule_EdgeNums.@h2] = edge_cur_requestStarRule_edge_h2;
                                matches.matchesList.PositionWasFilledFixIt();
                                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                                {
                                    node_cur_requestStarRule_node_p2.MoveInHeadAfter(edge_cur_requestStarRule_edge_h2);
                                    node_cur_requestStarRule_node_p1.MoveInHeadAfter(edge_cur_requestStarRule_edge_n);
                                    graph.MoveHeadAfter(edge_cur_requestStarRule_edge_h1);
                                    node_cur_requestStarRule_node_p1.isMatched = node_cur_requestStarRule_node_p1_prevIsMatched;
                                    node_cur_requestStarRule_node_r1.isMatched = node_cur_requestStarRule_node_r1_prevIsMatched;
                                    edge_cur_requestStarRule_edge_h1.isMatched = edge_cur_requestStarRule_edge_h1_prevIsMatched;
                                    return matches;
                                }
label7: ;
                            }
                            while( (edge_cur_requestStarRule_edge_h2 = edge_cur_requestStarRule_edge_h2.inNext) != edge_head_requestStarRule_edge_h2 );
                        }
                    }
                    while( (edge_cur_requestStarRule_edge_n = edge_cur_requestStarRule_edge_n.inNext) != edge_head_requestStarRule_edge_n );
                }
                node_cur_requestStarRule_node_p1.isMatched = node_cur_requestStarRule_node_p1_prevIsMatched;
                node_cur_requestStarRule_node_r1.isMatched = node_cur_requestStarRule_node_r1_prevIsMatched;
                edge_cur_requestStarRule_edge_h1.isMatched = edge_cur_requestStarRule_edge_h1_prevIsMatched;
            }
            return matches;
        }
    }

    public class Action_takeRule : LGSPAction
    {
        public Action_takeRule() {
            rulePattern = Rule_takeRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0);
        }

        public override string Name { get { return "takeRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_takeRule instance = new Action_takeRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_takeRule_edge_req = 5;
            for(LGSPEdge edge_head_takeRule_edge_req = graph.edgesByTypeHeads[edge_type_id_takeRule_edge_req], edge_cur_takeRule_edge_req = edge_head_takeRule_edge_req.typeNext; edge_cur_takeRule_edge_req != edge_head_takeRule_edge_req; edge_cur_takeRule_edge_req = edge_cur_takeRule_edge_req.typeNext)
            {
                LGSPNode node_cur_takeRule_node_r = edge_cur_takeRule_edge_req.target;
                if(!NodeType_Resource.isMyType[node_cur_takeRule_node_r.type.TypeID]) {
                    continue;
                }
                LGSPEdge edge_head_takeRule_edge_t = node_cur_takeRule_node_r.outhead;
                if(edge_head_takeRule_edge_t != null)
                {
                    LGSPEdge edge_cur_takeRule_edge_t = edge_head_takeRule_edge_t;
                    do
                    {
                        if(!EdgeType_token.isMyType[edge_cur_takeRule_edge_t.type.TypeID]) {
                            continue;
                        }
                        LGSPNode node_cur_takeRule_node_p = edge_cur_takeRule_edge_t.target;
                        if(!NodeType_Process.isMyType[node_cur_takeRule_node_p.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_takeRule_edge_req.source != node_cur_takeRule_node_p) {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_takeRule.takeRule_NodeNums.@r] = node_cur_takeRule_node_r;
                        match.Nodes[(int)Rule_takeRule.takeRule_NodeNums.@p] = node_cur_takeRule_node_p;
                        match.Edges[(int)Rule_takeRule.takeRule_EdgeNums.@t] = edge_cur_takeRule_edge_t;
                        match.Edges[(int)Rule_takeRule.takeRule_EdgeNums.@req] = edge_cur_takeRule_edge_req;
                        matches.matchesList.PositionWasFilledFixIt();
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            node_cur_takeRule_node_r.MoveOutHeadAfter(edge_cur_takeRule_edge_t);
                            graph.MoveHeadAfter(edge_cur_takeRule_edge_req);
                            return matches;
                        }
                    }
                    while( (edge_cur_takeRule_edge_t = edge_cur_takeRule_edge_t.outNext) != edge_head_takeRule_edge_t );
                }
            }
            return matches;
        }
    }

    public class Action_unlockRule : LGSPAction
    {
        public Action_unlockRule() {
            rulePattern = Rule_unlockRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 2, 0);
        }

        public override string Name { get { return "unlockRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_unlockRule instance = new Action_unlockRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_unlockRule_edge_b = 1;
            for(LGSPEdge edge_head_unlockRule_edge_b = graph.edgesByTypeHeads[edge_type_id_unlockRule_edge_b], edge_cur_unlockRule_edge_b = edge_head_unlockRule_edge_b.typeNext; edge_cur_unlockRule_edge_b != edge_head_unlockRule_edge_b; edge_cur_unlockRule_edge_b = edge_cur_unlockRule_edge_b.typeNext)
            {
                LGSPNode node_cur_unlockRule_node_r = edge_cur_unlockRule_edge_b.source;
                if(!NodeType_Resource.isMyType[node_cur_unlockRule_node_r.type.TypeID]) {
                    continue;
                }
                LGSPNode node_cur_unlockRule_node_p = edge_cur_unlockRule_edge_b.target;
                if(!NodeType_Process.isMyType[node_cur_unlockRule_node_p.type.TypeID]) {
                    continue;
                }
                LGSPEdge edge_head_unlockRule_edge_hb = node_cur_unlockRule_node_r.outhead;
                if(edge_head_unlockRule_edge_hb != null)
                {
                    LGSPEdge edge_cur_unlockRule_edge_hb = edge_head_unlockRule_edge_hb;
                    do
                    {
                        if(!EdgeType_held_by.isMyType[edge_cur_unlockRule_edge_hb.type.TypeID]) {
                            continue;
                        }
                        if(edge_cur_unlockRule_edge_hb.target != node_cur_unlockRule_node_p) {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_unlockRule.unlockRule_NodeNums.@r] = node_cur_unlockRule_node_r;
                        match.Nodes[(int)Rule_unlockRule.unlockRule_NodeNums.@p] = node_cur_unlockRule_node_p;
                        match.Edges[(int)Rule_unlockRule.unlockRule_EdgeNums.@b] = edge_cur_unlockRule_edge_b;
                        match.Edges[(int)Rule_unlockRule.unlockRule_EdgeNums.@hb] = edge_cur_unlockRule_edge_hb;
                        matches.matchesList.PositionWasFilledFixIt();
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            node_cur_unlockRule_node_r.MoveOutHeadAfter(edge_cur_unlockRule_edge_hb);
                            graph.MoveHeadAfter(edge_cur_unlockRule_edge_b);
                            return matches;
                        }
                    }
                    while( (edge_cur_unlockRule_edge_hb = edge_cur_unlockRule_edge_hb.outNext) != edge_head_unlockRule_edge_hb );
                }
            }
            return matches;
        }
    }

    public class Action_unmountRule : LGSPAction
    {
        public Action_unmountRule() {
            rulePattern = Rule_unmountRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1, 0);
        }

        public override string Name { get { return "unmountRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_unmountRule instance = new Action_unmountRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int edge_type_id_unmountRule_edge_t = 3;
            for(LGSPEdge edge_head_unmountRule_edge_t = graph.edgesByTypeHeads[edge_type_id_unmountRule_edge_t], edge_cur_unmountRule_edge_t = edge_head_unmountRule_edge_t.typeNext; edge_cur_unmountRule_edge_t != edge_head_unmountRule_edge_t; edge_cur_unmountRule_edge_t = edge_cur_unmountRule_edge_t.typeNext)
            {
                LGSPNode node_cur_unmountRule_node_r = edge_cur_unmountRule_edge_t.source;
                if(!NodeType_Resource.isMyType[node_cur_unmountRule_node_r.type.TypeID]) {
                    continue;
                }
                LGSPNode node_cur_unmountRule_node_p = edge_cur_unmountRule_edge_t.target;
                if(!NodeType_Process.isMyType[node_cur_unmountRule_node_p.type.TypeID]) {
                    continue;
                }
                LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                match.patternGraph = rulePattern.patternGraph;
                match.Nodes[(int)Rule_unmountRule.unmountRule_NodeNums.@r] = node_cur_unmountRule_node_r;
                match.Nodes[(int)Rule_unmountRule.unmountRule_NodeNums.@p] = node_cur_unmountRule_node_p;
                match.Edges[(int)Rule_unmountRule.unmountRule_EdgeNums.@t] = edge_cur_unmountRule_edge_t;
                matches.matchesList.PositionWasFilledFixIt();
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(edge_cur_unmountRule_edge_t);
                    return matches;
                }
            }
            return matches;
        }
    }

    public class Action_waitingRule : LGSPAction
    {
        public Action_waitingRule() {
            rulePattern = Rule_waitingRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch; matches = new LGSPMatches(this, 5, 3, 0);
        }

        public override string Name { get { return "waitingRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_waitingRule instance = new Action_waitingRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matchesList.Clear();
            int node_type_id_waitingRule_node_r = 1;
            for(LGSPNode node_head_waitingRule_node_r = graph.nodesByTypeHeads[node_type_id_waitingRule_node_r], node_cur_waitingRule_node_r = node_head_waitingRule_node_r.typeNext; node_cur_waitingRule_node_r != node_head_waitingRule_node_r; node_cur_waitingRule_node_r = node_cur_waitingRule_node_r.typeNext)
            {
                bool node_cur_waitingRule_node_r_prevIsMatched = node_cur_waitingRule_node_r.isMatched;
                node_cur_waitingRule_node_r.isMatched = true;
                int edge_type_id_waitingRule_edge_b = 1;
                for(LGSPEdge edge_head_waitingRule_edge_b = graph.edgesByTypeHeads[edge_type_id_waitingRule_edge_b], edge_cur_waitingRule_edge_b = edge_head_waitingRule_edge_b.typeNext; edge_cur_waitingRule_edge_b != edge_head_waitingRule_edge_b; edge_cur_waitingRule_edge_b = edge_cur_waitingRule_edge_b.typeNext)
                {
                    LGSPNode node_cur_waitingRule_node_r2 = edge_cur_waitingRule_edge_b.source;
                    if(!NodeType_Resource.isMyType[node_cur_waitingRule_node_r2.type.TypeID]) {
                        continue;
                    }
                    if(node_cur_waitingRule_node_r2.isMatched
                        && node_cur_waitingRule_node_r2==node_cur_waitingRule_node_r
                        )
                    {
                        continue;
                    }
                    bool node_cur_waitingRule_node_r2_prevIsMatched = node_cur_waitingRule_node_r2.isMatched;
                    node_cur_waitingRule_node_r2.isMatched = true;
                    LGSPNode node_cur_waitingRule_node_p1 = edge_cur_waitingRule_edge_b.target;
                    if(!NodeType_Process.isMyType[node_cur_waitingRule_node_p1.type.TypeID]) {
                        node_cur_waitingRule_node_r2.isMatched = node_cur_waitingRule_node_r2_prevIsMatched;
                        continue;
                    }
                    bool node_cur_waitingRule_node_p1_prevIsMatched = node_cur_waitingRule_node_p1.isMatched;
                    node_cur_waitingRule_node_p1.isMatched = true;
                    LGSPEdge edge_head_waitingRule_edge_hb = node_cur_waitingRule_node_p1.inhead;
                    if(edge_head_waitingRule_edge_hb != null)
                    {
                        LGSPEdge edge_cur_waitingRule_edge_hb = edge_head_waitingRule_edge_hb;
                        do
                        {
                            if(!EdgeType_held_by.isMyType[edge_cur_waitingRule_edge_hb.type.TypeID]) {
                                continue;
                            }
                            LGSPNode node_cur_waitingRule_node_r1 = edge_cur_waitingRule_edge_hb.source;
                            if(!NodeType_Resource.isMyType[node_cur_waitingRule_node_r1.type.TypeID]) {
                                continue;
                            }
                            if(node_cur_waitingRule_node_r1.isMatched
                                && (node_cur_waitingRule_node_r1==node_cur_waitingRule_node_r
                                    || node_cur_waitingRule_node_r1==node_cur_waitingRule_node_r2
                                    )
                                )
                            {
                                continue;
                            }
                            LGSPEdge edge_head_waitingRule_edge_req = node_cur_waitingRule_node_r1.inhead;
                            if(edge_head_waitingRule_edge_req != null)
                            {
                                LGSPEdge edge_cur_waitingRule_edge_req = edge_head_waitingRule_edge_req;
                                do
                                {
                                    if(!EdgeType_request.isMyType[edge_cur_waitingRule_edge_req.type.TypeID]) {
                                        continue;
                                    }
                                    LGSPNode node_cur_waitingRule_node_p2 = edge_cur_waitingRule_edge_req.source;
                                    if(!NodeType_Process.isMyType[node_cur_waitingRule_node_p2.type.TypeID]) {
                                        continue;
                                    }
                                    if(node_cur_waitingRule_node_p2.isMatched
                                        && node_cur_waitingRule_node_p2==node_cur_waitingRule_node_p1
                                        )
                                    {
                                        continue;
                                    }
                                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                                    match.patternGraph = rulePattern.patternGraph;
                                    match.Nodes[(int)Rule_waitingRule.waitingRule_NodeNums.@r] = node_cur_waitingRule_node_r;
                                    match.Nodes[(int)Rule_waitingRule.waitingRule_NodeNums.@r2] = node_cur_waitingRule_node_r2;
                                    match.Nodes[(int)Rule_waitingRule.waitingRule_NodeNums.@p1] = node_cur_waitingRule_node_p1;
                                    match.Nodes[(int)Rule_waitingRule.waitingRule_NodeNums.@r1] = node_cur_waitingRule_node_r1;
                                    match.Nodes[(int)Rule_waitingRule.waitingRule_NodeNums.@p2] = node_cur_waitingRule_node_p2;
                                    match.Edges[(int)Rule_waitingRule.waitingRule_EdgeNums.@b] = edge_cur_waitingRule_edge_b;
                                    match.Edges[(int)Rule_waitingRule.waitingRule_EdgeNums.@hb] = edge_cur_waitingRule_edge_hb;
                                    match.Edges[(int)Rule_waitingRule.waitingRule_EdgeNums.@req] = edge_cur_waitingRule_edge_req;
                                    matches.matchesList.PositionWasFilledFixIt();
                                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                                    {
                                        node_cur_waitingRule_node_r1.MoveInHeadAfter(edge_cur_waitingRule_edge_req);
                                        node_cur_waitingRule_node_p1.MoveInHeadAfter(edge_cur_waitingRule_edge_hb);
                                        graph.MoveHeadAfter(edge_cur_waitingRule_edge_b);
                                        graph.MoveHeadAfter(node_cur_waitingRule_node_r);
                                        node_cur_waitingRule_node_p1.isMatched = node_cur_waitingRule_node_p1_prevIsMatched;
                                        node_cur_waitingRule_node_r2.isMatched = node_cur_waitingRule_node_r2_prevIsMatched;
                                        node_cur_waitingRule_node_r.isMatched = node_cur_waitingRule_node_r_prevIsMatched;
                                        return matches;
                                    }
                                }
                                while( (edge_cur_waitingRule_edge_req = edge_cur_waitingRule_edge_req.inNext) != edge_head_waitingRule_edge_req );
                            }
                        }
                        while( (edge_cur_waitingRule_edge_hb = edge_cur_waitingRule_edge_hb.inNext) != edge_head_waitingRule_edge_hb );
                    }
                    node_cur_waitingRule_node_p1.isMatched = node_cur_waitingRule_node_p1_prevIsMatched;
                    node_cur_waitingRule_node_r2.isMatched = node_cur_waitingRule_node_r2_prevIsMatched;
                }
                node_cur_waitingRule_node_r.isMatched = node_cur_waitingRule_node_r_prevIsMatched;
            }
            return matches;
        }
    }


    public class Model_MutexPimped_Actions : LGSPActions
    {
        public Model_MutexPimped_Actions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, dumperfactory, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public Model_MutexPimped_Actions(LGSPGraph lgspgraph)
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
        public override String ModelMD5Hash { get { return "ac160ef7c8b339e3f8207121808da3a1"; } }
    }
}