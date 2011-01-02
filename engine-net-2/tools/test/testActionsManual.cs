/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

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

		public static NodeType[] node_p2_AllowedTypes = null;
		public static NodeType[] node_p1_AllowedTypes = null;
		public static bool[] node_p2_IsAllowedType = null;
		public static bool[] node_p1_IsAllowedType = null;
		public static EdgeType[] edge__edge0_AllowedTypes = null;
		public static bool[] edge__edge0_IsAllowedType = null;

		public enum NodeNums { @p2  = 1, @p1, };
		public enum EdgeNums { @_edge0 = 1, };

		private Rule_testRule()
		{
			PatternNode node_p2 = new PatternNode((int) NodeTypes.@Process, "node_p2", node_p2_AllowedTypes, node_p2_IsAllowedType, PatternElementType.Normal, -1);
			PatternNode node_p1 = new PatternNode((int) NodeTypes.@Process, "node_p1", node_p1_AllowedTypes, node_p1_IsAllowedType, PatternElementType.Normal, -1);
			PatternEdge edge__edge0 = new PatternEdge(node_p1, node_p2, (int) EdgeTypes.@connection, "edge__edge0", edge__edge0_AllowedTypes, edge__edge0_IsAllowedType, PatternElementType.Normal, -1);
			patternGraph = new PatternGraph(
				new PatternNode[] { node_p2, node_p1 }, 
				new PatternEdge[] { edge__edge0 }, 
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
			LGSPEdge edge__edge0 = match.edges[ (int) EdgeNums.@_edge0 - 1 ];
			LGSPNode node__node0 = graph.AddNode(NodeType_Process.typeVar);
			// re-using edge__edge0 as edge__edge1
			LGSPEdge edge__edge1 = edge__edge0;
			graph.ReuseEdge(edge__edge0, null, node__node0, null);
			LGSPEdge edge__edge2 = graph.AddEdge(EdgeType_connection.typeVar, node__node0, node_p2);
			return EmptyReturnElements;
		}
		private static String[] addedNodeNames = new String[] { "_node0" };
		public override String[] AddedNodeNames { get { return addedNodeNames; } }
		private static String[] addedEdgeNames = new String[] { "_edge1", "_edge2" };
		public override String[] AddedEdgeNames { get { return addedEdgeNames; } }

		public override IGraphElement[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_p2 = match.nodes[ (int) NodeNums.@p2 - 1 ];
			LGSPNode node_p1 = match.nodes[ (int) NodeNums.@p1 - 1 ];
			LGSPEdge edge__edge0 = match.edges[ (int) EdgeNums.@_edge0 - 1 ];
			LGSPNode node__node0 = graph.AddNode(NodeType_Process.typeVar);
			LGSPEdge edge__edge1 = graph.AddEdge(EdgeType_connection.typeVar, node_p1, node__node0);
			LGSPEdge edge__edge2 = graph.AddEdge(EdgeType_connection.typeVar, node__node0, node_p2);
			graph.Remove(edge__edge0);
			return EmptyReturnElements;
		}
	}

#if INITIAL_WARMUP
	public class Schedule_testRule : LGSPStaticScheduleInfo
	{
		public Schedule_testRule()
		{
			ActionName = "testRule";
			this.RulePattern = Rule_testRule.Instance;
			NodeCost = new float[] { 5.5F, 5.5F,  };
			EdgeCost = new float[] { 5.5F,  };
			NegNodeCost = new float[][] { };
			NegEdgeCost = new float[][] { };
		}
	}
#endif


    public class Action_testRule : LGSPAction
    {
        private static Action_testRule instance = new Action_testRule();

        public Action_testRule() { rulePattern = Rule_testRule.Instance; DynamicMatch = myMatch; matches = new LGSPMatches(this, 2, 1); matchesList = matches.matches;}

        public override string Name { get { return "testRule"; } }
        public static LGSPAction Instance { get { return instance; } }
        private LGSPMatches matches;
        private LGSPMatchesList matchesList;
        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, IGraphElement[] parameters)
        {
            matches.matches.Clear();
            // Lookup(edge__edge0:connection)
            foreach(EdgeType edge_type_edge__edge0 in EdgeType_connection.typeVar.SubOrSameTypes)
            {
                for(LGSPEdge edge_head_edge__edge0 = graph.edgesByTypeHeads[edge_type_edge__edge0.TypeID], edge_cur_edge__edge0 = edge_head_edge__edge0.typeNext; edge_cur_edge__edge0 != edge_head_edge__edge0; edge_cur_edge__edge0 = edge_cur_edge__edge0.typeNext)
                {
                    // ImplicitTarget(edge__edge0 -> node_p2:Process)
                    LGSPNode node_cur_node_p2 = edge_cur_edge__edge0.target;
                    if(!NodeType_Process.isMyType[node_cur_node_p2.type.TypeID]) goto contunmap_edge_cur_edge__edge0_2;
                    node_cur_node_p2.mappedTo = 1;
                    // ImplicitSource(edge__edge0 -> node_p1:Process)
                    LGSPNode node_cur_node_p1 = edge_cur_edge__edge0.source;
                    if(!NodeType_Process.isMyType[node_cur_node_p1.type.TypeID]) goto contunmap_node_cur_node_p2_4;
                    if(node_cur_node_p1.mappedTo != 0) goto cont_node_cur_node_p1_7;
                    LGSPMatch match = matchesList.GetNewMatch();
                    match.nodes[0] = node_cur_node_p2;
                    match.nodes[1] = node_cur_node_p1;
                    match.edges[0] = edge_cur_edge__edge0;
                    matchesList.CommitMatch();
                    if(maxMatches > 0 && matchesList.Count >= maxMatches)
                    {
                        node_cur_node_p2.mappedTo = 0;
                        graph.MoveHeadAfter(edge_cur_edge__edge0);
                        return matches;
                    }
cont_node_cur_node_p1_7:;
contunmap_node_cur_node_p2_4:;
                    node_cur_node_p2.mappedTo = 0;
contunmap_edge_cur_edge__edge0_2:;
                    // Tail of Lookup(edge_cur_edge__edge0)
                }
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
        public override String ModelMD5Hash { get { return "ed674c417526b8f9c14286768b74e3f3"; } }
    }
}
