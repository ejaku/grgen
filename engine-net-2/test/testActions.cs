using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.models.test;

namespace de.unika.ipd.grGen.actions.test
{
	public class Rule_testRule : LGSPRulePattern
	{
		private static Rule_testRule instance = null;
		public static Rule_testRule Instance { get { if (instance==null) instance = new Rule_testRule(); return instance; } }

		public static NodeType[] node_a_AllowedTypes = null;
		public static NodeType[] node_f_AllowedTypes = null;
		public static NodeType[] node_m_AllowedTypes = null;
		public static bool[] node_a_IsAllowedType = null;
		public static bool[] node_f_IsAllowedType = null;
		public static bool[] node_m_IsAllowedType = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static EdgeType[] edge__edge1_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;
		public static bool[] edge__edge1_IsAllowedType = null;

		public enum NodeNums { @a  = 1, @f, @m, };
		public enum EdgeNums { @_edge0 = 1, @_edge1, };
		public enum PatternNums { };

		private Rule_testRule()
		{
			PatternNode node_a = new PatternNode((int) NodeTypes.@D231_4121, "node_a", node_a_AllowedTypes, node_a_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_f = new PatternNode((int) NodeTypes.@B21, "node_f", node_f_AllowedTypes, node_f_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_m = new PatternNode((int) NodeTypes.@D2211_2222_31, "node_m", node_m_AllowedTypes, node_m_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_a, node_f, (int) EdgeTypes.@Edge, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge1 = new PatternEdge(node_f, node_m, (int) EdgeTypes.@Edge, "edge__edge1", edge__edge1_AllowedTypes, edge__edge1_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				"rule testRule.pattern",
				new PatternNode[] { node_a, node_f, node_m }, 
				new PatternEdge[] { edge__edge0, edge__edge1 }, 
				new PatternGraphEmbedding[] {  }, 
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
			LGSPNode node_a = match.Nodes[ (int) NodeNums.@a - 1 ];
			LGSPNode node_f = match.Nodes[ (int) NodeNums.@f - 1 ];
			LGSPNode node_m = match.Nodes[ (int) NodeNums.@m - 1 ];
			Node_D2211_2222_31 node_are = (Node_D2211_2222_31) graph.Retype(node_a, NodeType_D2211_2222_31.typeVar);
			Node_D231_4121 node_fre = (Node_D231_4121) graph.Retype(node_f, NodeType_D231_4121.typeVar);
			Node_D11_2221 node_mre = (Node_D11_2221) graph.Retype(node_m, NodeType_D11_2221.typeVar);
			int var_i = 1234;
			graph.ChangingNodeAttribute(node_are, NodeType_D2211_2222_31.AttributeType_d2211_2222_31, node_are.@d2211_2222_31, var_i);
			node_are.@d2211_2222_31 = var_i;
			var_i = 5678;
			graph.ChangingNodeAttribute(node_fre, NodeType_D231_4121.AttributeType_d231_4121, node_fre.@d231_4121, var_i);
			node_fre.@d231_4121 = var_i;
			var_i = 9012;
			graph.ChangingNodeAttribute(node_mre, NodeType_D11_2221.AttributeType_d11_2221, node_mre.@d11_2221, var_i);
			node_mre.@d11_2221 = var_i;
			return EmptyReturnElements;
		}

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_a = match.Nodes[ (int) NodeNums.@a - 1 ];
			LGSPNode node_f = match.Nodes[ (int) NodeNums.@f - 1 ];
			LGSPNode node_m = match.Nodes[ (int) NodeNums.@m - 1 ];
			Node_D2211_2222_31 node_are = (Node_D2211_2222_31) graph.Retype(node_a, NodeType_D2211_2222_31.typeVar);
			Node_D231_4121 node_fre = (Node_D231_4121) graph.Retype(node_f, NodeType_D231_4121.typeVar);
			Node_D11_2221 node_mre = (Node_D11_2221) graph.Retype(node_m, NodeType_D11_2221.typeVar);
			int var_i = 1234;
			graph.ChangingNodeAttribute(node_are, NodeType_D2211_2222_31.AttributeType_d2211_2222_31, node_are.@d2211_2222_31, var_i);
			node_are.@d2211_2222_31 = var_i;
			var_i = 5678;
			graph.ChangingNodeAttribute(node_fre, NodeType_D231_4121.AttributeType_d231_4121, node_fre.@d231_4121, var_i);
			node_fre.@d231_4121 = var_i;
			var_i = 9012;
			graph.ChangingNodeAttribute(node_mre, NodeType_D11_2221.AttributeType_d11_2221, node_mre.@d11_2221, var_i);
			node_mre.@d11_2221 = var_i;
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] {  };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] {  };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }
	}

#if INITIAL_WARMUP
	public class Schedule_testRule : LGSPStaticScheduleInfo
	{
		public Schedule_testRule()
		{
			ActionName = "testRule";
			this.RulePattern = Rule_testRule.Instance;
			isRule = true;
			NodeCost = new float[] { 5.5F, 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F, 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif


    public class Action_testRule : LGSPAction
    {
        private static Action_testRule instance = new Action_testRule();

        public Action_testRule() { rulePattern = Rule_testRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 3, 2); matchesList = matches.matches;}

        public override string Name { get { return "testRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup edge__edge1 
            int edge_type_id_edge__edge1 = 0;
            for(LGSPEdge edge_head_edge__edge1 = graph.edgesByTypeHeads[edge_type_id_edge__edge1], edge_cur_edge__edge1 = edge_head_edge__edge1.typeNext; edge_cur_edge__edge1 != edge_head_edge__edge1; edge_cur_edge__edge1 = edge_cur_edge__edge1.typeNext)
            {
                bool edge_cur_edge__edge1_prevIsMatched = edge_cur_edge__edge1.isMatched;
                edge_cur_edge__edge1.isMatched = true;
                // Implicit source node_f from edge__edge1 
                LGSPNode node_cur_node_f = edge_cur_edge__edge1.source;
                if(!NodeType_B21.isMyType[node_cur_node_f.type.TypeID]) {
                    edge_cur_edge__edge1.isMatched = edge_cur_edge__edge1_prevIsMatched;
                    continue;
                }
                // Implicit target node_m from edge__edge1 
                LGSPNode node_cur_node_m = edge_cur_edge__edge1.target;
                if(!NodeType_D2211_2222_31.isMyType[node_cur_node_m.type.TypeID]) {
                    edge_cur_edge__edge1.isMatched = edge_cur_edge__edge1_prevIsMatched;
                    continue;
                }
                // Extend incoming edge__edge0 from node_f 
                LGSPEdge edge_head_edge__edge0 = node_cur_node_f.inhead;
                if(edge_head_edge__edge0 != null)
                {
                    LGSPEdge edge_cur_edge__edge0 = edge_head_edge__edge0;
                    do
                    {
                        if(edge_cur_edge__edge0.isMatched
                            && edge_cur_edge__edge0==edge_cur_edge__edge1
                            )
                        {
                            continue;
                        }
                        // Implicit source node_a from edge__edge0 
                        LGSPNode node_cur_node_a = edge_cur_edge__edge0.source;
                        if(!NodeType_D231_4121.isMyType[node_cur_node_a.type.TypeID]) {
                            continue;
                        }
                        LGSPMatch match = matchesList.GetNewMatch();
                        match.Nodes[0] = node_cur_node_a;
                        match.Nodes[1] = node_cur_node_f;
                        match.Nodes[2] = node_cur_node_m;
                        match.Edges[0] = edge_cur_edge__edge0;
                        match.Edges[1] = edge_cur_edge__edge1;
                        matchesList.CommitMatch();
                        if(maxMatches > 0 && matchesList.Count >= maxMatches)
                        {
                            node_cur_node_f.MoveInHeadAfter(edge_cur_edge__edge0);
                            graph.MoveHeadAfter(edge_cur_edge__edge1);
                            edge_cur_edge__edge1.isMatched = edge_cur_edge__edge1_prevIsMatched;
                            return matches;
                        }
                    }
                    while( (edge_cur_edge__edge0 = edge_cur_edge__edge0.inNext) != edge_head_edge__edge0 );
                }
                edge_cur_edge__edge1.isMatched = edge_cur_edge__edge1_prevIsMatched;
            }
            return matches;
        }
    }

    public class testActions : LGSPActions
    {
        public testActions(LGSPGraph lgspgraph, IDumperFactory dumperfactory, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, dumperfactory, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public testActions(LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("testRule", (LGSPAction) Action_testRule.Instance);
        }

        public override String Name { get { return "testActions"; } }
        public override String ModelMD5Hash { get { return "72976c7fc07bd75a73674984ca518dfc"; } }
    }
}