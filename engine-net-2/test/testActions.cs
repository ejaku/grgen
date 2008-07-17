// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "test.grg" on Thu Jul 17 11:13:10 GMT+01:00 2008

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_complModel;

namespace de.unika.ipd.grGen.Action_test
{
	public class Rule_testRule : LGSPRulePattern
	{
		private static Rule_testRule instance = null;
		public static Rule_testRule Instance { get { if (instance==null) { instance = new Rule_testRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static NodeType[] testRule_node_a_AllowedTypes = null;
		public static NodeType[] testRule_node_f_AllowedTypes = null;
		public static NodeType[] testRule_node_m_AllowedTypes = null;
		public static bool[] testRule_node_a_IsAllowedType = null;
		public static bool[] testRule_node_f_IsAllowedType = null;
		public static bool[] testRule_node_m_IsAllowedType = null;
		public static EdgeType[] testRule_edge__edge0_AllowedTypes = null;
		public static EdgeType[] testRule_edge__edge1_AllowedTypes = null;
		public static bool[] testRule_edge__edge0_IsAllowedType = null;
		public static bool[] testRule_edge__edge1_IsAllowedType = null;
		public enum testRule_NodeNums { @a, @f, @m, };
		public enum testRule_EdgeNums { @_edge0, @_edge1, };
		public enum testRule_VariableNums { };
		public enum testRule_SubNums { };
		public enum testRule_AltNums { };
		PatternGraph pat_testRule;


#if INITIAL_WARMUP
		public Rule_testRule()
#else
		private Rule_testRule()
#endif
		{
			name = "testRule";

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] testRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] testRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternNode testRule_node_a = new PatternNode((int) NodeTypes.@D231_4121, "testRule_node_a", "a", testRule_node_a_AllowedTypes, testRule_node_a_IsAllowedType, 5.5F, -1);
			PatternNode testRule_node_f = new PatternNode((int) NodeTypes.@B21, "testRule_node_f", "f", testRule_node_f_AllowedTypes, testRule_node_f_IsAllowedType, 5.5F, -1);
			PatternNode testRule_node_m = new PatternNode((int) NodeTypes.@D2211_2222_31, "testRule_node_m", "m", testRule_node_m_AllowedTypes, testRule_node_m_IsAllowedType, 5.5F, -1);
			PatternEdge testRule_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "testRule_edge__edge0", "_edge0", testRule_edge__edge0_AllowedTypes, testRule_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge testRule_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "testRule_edge__edge1", "_edge1", testRule_edge__edge1_AllowedTypes, testRule_edge__edge1_IsAllowedType, 5.5F, -1);
			pat_testRule = new PatternGraph(
				"testRule",
				"",
				false,
				new PatternNode[] { testRule_node_a, testRule_node_f, testRule_node_m }, 
				new PatternEdge[] { testRule_edge__edge0, testRule_edge__edge1 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				testRule_isNodeHomomorphicGlobal,
				testRule_isEdgeHomomorphicGlobal
			);
			pat_testRule.edgeToSourceNode.Add(testRule_edge__edge0, testRule_node_a);
			pat_testRule.edgeToTargetNode.Add(testRule_edge__edge0, testRule_node_f);
			pat_testRule.edgeToSourceNode.Add(testRule_edge__edge1, testRule_node_f);
			pat_testRule.edgeToTargetNode.Add(testRule_edge__edge1, testRule_node_m);

			testRule_node_a.PointOfDefinition = pat_testRule;
			testRule_node_f.PointOfDefinition = pat_testRule;
			testRule_node_m.PointOfDefinition = pat_testRule;
			testRule_edge__edge0.PointOfDefinition = pat_testRule;
			testRule_edge__edge1.PointOfDefinition = pat_testRule;

			patternGraph = pat_testRule;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch curMatch)
		{
			LGSPNode node_a = curMatch.Nodes[(int)testRule_NodeNums.@a];
			LGSPNode node_f = curMatch.Nodes[(int)testRule_NodeNums.@f];
			LGSPNode node_m = curMatch.Nodes[(int)testRule_NodeNums.@m];
			graph.SettingAddedNodeNames( testRule_addedNodeNames );
			LGSPNode node_are = graph.Retype(node_a, NodeType_D2211_2222_31.typeVar);
			@ID2211_2222_31 inode_are = (@ID2211_2222_31) node_are;
			LGSPNode node_fre = graph.Retype(node_f, NodeType_D231_4121.typeVar);
			@ID231_4121 inode_fre = (@ID231_4121) node_fre;
			LGSPNode node_mre = graph.Retype(node_m, NodeType_D11_2221.typeVar);
			@ID11_2221 inode_mre = (@ID11_2221) node_mre;
			graph.SettingAddedEdgeNames( testRule_addedEdgeNames );
			int tempvar_i = 1234;
			graph.ChangingNodeAttribute(node_are, NodeType_D2211_2222_31.AttributeType_d2211_2222_31, inode_are.@d2211_2222_31, tempvar_i);
			inode_are.@d2211_2222_31 = tempvar_i;
			tempvar_i = 5678;
			graph.ChangingNodeAttribute(node_fre, NodeType_D231_4121.AttributeType_d231_4121, inode_fre.@d231_4121, tempvar_i);
			inode_fre.@d231_4121 = tempvar_i;
			tempvar_i = 9012;
			graph.ChangingNodeAttribute(node_mre, NodeType_D11_2221.AttributeType_d11_2221, inode_mre.@d11_2221, tempvar_i);
			inode_mre.@d11_2221 = tempvar_i;
			return EmptyReturnElements;
		}
		private static String[] testRule_addedNodeNames = new String[] {  };
		private static String[] testRule_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch curMatch)
		{
			LGSPNode node_a = curMatch.Nodes[(int)testRule_NodeNums.@a];
			LGSPNode node_f = curMatch.Nodes[(int)testRule_NodeNums.@f];
			LGSPNode node_m = curMatch.Nodes[(int)testRule_NodeNums.@m];
			graph.SettingAddedNodeNames( testRule_addedNodeNames );
			LGSPNode node_are = graph.Retype(node_a, NodeType_D2211_2222_31.typeVar);
			@ID2211_2222_31 inode_are = (@ID2211_2222_31) node_are;
			LGSPNode node_fre = graph.Retype(node_f, NodeType_D231_4121.typeVar);
			@ID231_4121 inode_fre = (@ID231_4121) node_fre;
			LGSPNode node_mre = graph.Retype(node_m, NodeType_D11_2221.typeVar);
			@ID11_2221 inode_mre = (@ID11_2221) node_mre;
			graph.SettingAddedEdgeNames( testRule_addedEdgeNames );
			int tempvar_i = 1234;
			graph.ChangingNodeAttribute(node_are, NodeType_D2211_2222_31.AttributeType_d2211_2222_31, inode_are.@d2211_2222_31, tempvar_i);
			inode_are.@d2211_2222_31 = tempvar_i;
			tempvar_i = 5678;
			graph.ChangingNodeAttribute(node_fre, NodeType_D231_4121.AttributeType_d231_4121, inode_fre.@d231_4121, tempvar_i);
			inode_fre.@d231_4121 = tempvar_i;
			tempvar_i = 9012;
			graph.ChangingNodeAttribute(node_mre, NodeType_D11_2221.AttributeType_d11_2221, inode_mre.@d11_2221, tempvar_i);
			inode_mre.@d11_2221 = tempvar_i;
			return EmptyReturnElements;
		}
	}


    public class Action_testRule : LGSPAction
    {
        public Action_testRule() {
            rulePattern = Rule_testRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 3, 2, 0, 0 + 0);
        }

        public override string Name { get { return "testRule"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_testRule instance = new Action_testRule();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            int negLevel = 0;
            // Lookup testRule_edge__edge1 
            int type_id_candidate_testRule_edge__edge1 = 1;
            for(LGSPEdge head_candidate_testRule_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_testRule_edge__edge1], candidate_testRule_edge__edge1 = head_candidate_testRule_edge__edge1.typeNext; candidate_testRule_edge__edge1 != head_candidate_testRule_edge__edge1; candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.typeNext)
            {
                uint prev__candidate_testRule_edge__edge1;
                prev__candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.flags & (uint) LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_testRule_edge__edge1.flags |= (uint) LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source testRule_node_f from testRule_edge__edge1 
                LGSPNode candidate_testRule_node_f = candidate_testRule_edge__edge1.source;
                if(candidate_testRule_node_f.type.TypeID!=6) {
                    candidate_testRule_edge__edge1.flags = candidate_testRule_edge__edge1.flags & ~((uint) LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
                    continue;
                }
                // Implicit Target testRule_node_m from testRule_edge__edge1 
                LGSPNode candidate_testRule_node_m = candidate_testRule_edge__edge1.target;
                if(candidate_testRule_node_m.type.TypeID!=17) {
                    candidate_testRule_edge__edge1.flags = candidate_testRule_edge__edge1.flags & ~((uint) LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
                    continue;
                }
                // Extend Incoming testRule_edge__edge0 from testRule_node_f 
                LGSPEdge head_candidate_testRule_edge__edge0 = candidate_testRule_node_f.inhead;
                if(head_candidate_testRule_edge__edge0 != null)
                {
                    LGSPEdge candidate_testRule_edge__edge0 = head_candidate_testRule_edge__edge0;
                    do
                    {
                        if(candidate_testRule_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_testRule_edge__edge0.flags & (uint) LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Implicit Source testRule_node_a from testRule_edge__edge0 
                        LGSPNode candidate_testRule_node_a = candidate_testRule_edge__edge0.source;
                        if(candidate_testRule_node_a.type.TypeID!=18) {
                            continue;
                        }
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_testRule.testRule_NodeNums.@a] = candidate_testRule_node_a;
                        match.Nodes[(int)Rule_testRule.testRule_NodeNums.@f] = candidate_testRule_node_f;
                        match.Nodes[(int)Rule_testRule.testRule_NodeNums.@m] = candidate_testRule_node_m;
                        match.Edges[(int)Rule_testRule.testRule_EdgeNums.@_edge0] = candidate_testRule_edge__edge0;
                        match.Edges[(int)Rule_testRule.testRule_EdgeNums.@_edge1] = candidate_testRule_edge__edge1;
                        matches.matchesList.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                        {
                            candidate_testRule_node_f.MoveInHeadAfter(candidate_testRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_testRule_edge__edge1);
                            candidate_testRule_edge__edge1.flags = candidate_testRule_edge__edge1.flags & ~((uint) LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
                            return matches;
                        }
                    }
                    while( (candidate_testRule_edge__edge0 = candidate_testRule_edge__edge0.inNext) != head_candidate_testRule_edge__edge0 );
                }
                candidate_testRule_edge__edge1.flags = candidate_testRule_edge__edge1.flags & ~((uint) LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
            }
            return matches;
        }
    }


    public class testActions : LGSPActions
    {
        public testActions(LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
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
        public override String ModelMD5Hash { get { return "6a630d39ca3371b697e3fb227fb1f51a"; } }
    }
}