// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "test.grg" on Wed Sep 08 23:33:14 CEST 2010

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_complModel;

namespace de.unika.ipd.grGen.Action_test
{
	public class Rule_testRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_testRule instance = null;
		public static Rule_testRule Instance { get { if (instance==null) { instance = new Rule_testRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] testRule_node_a_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] testRule_node_f_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] testRule_node_m_AllowedTypes = null;
		public static bool[] testRule_node_a_IsAllowedType = null;
		public static bool[] testRule_node_f_IsAllowedType = null;
		public static bool[] testRule_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] testRule_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] testRule_edge__edge1_AllowedTypes = null;
		public static bool[] testRule_edge__edge0_IsAllowedType = null;
		public static bool[] testRule_edge__edge1_IsAllowedType = null;
		public enum testRule_NodeNums { @a, @f, @m, };
		public enum testRule_EdgeNums { @_edge0, @_edge1, };
		public enum testRule_VariableNums { };
		public enum testRule_SubNums { };
		public enum testRule_AltNums { };
		public enum testRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_testRule;


		private Rule_testRule()
		{
			name = "testRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
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
			int[] testRule_minMatches = new int[0] ;
			int[] testRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode testRule_node_a = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@D231_4121, "GRGEN_MODEL.ID231_4121", "testRule_node_a", "a", testRule_node_a_AllowedTypes, testRule_node_a_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode testRule_node_f = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@B21, "GRGEN_MODEL.IB21", "testRule_node_f", "f", testRule_node_f_AllowedTypes, testRule_node_f_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode testRule_node_m = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@D2211_2222_31, "GRGEN_MODEL.ID2211_2222_31", "testRule_node_m", "m", testRule_node_m_AllowedTypes, testRule_node_m_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge testRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "testRule_edge__edge0", "_edge0", testRule_edge__edge0_AllowedTypes, testRule_edge__edge0_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge testRule_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "testRule_edge__edge1", "_edge1", testRule_edge__edge1_AllowedTypes, testRule_edge__edge1_IsAllowedType, 5.5F, -1, false);
			pat_testRule = new GRGEN_LGSP.PatternGraph(
				"testRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { testRule_node_a, testRule_node_f, testRule_node_m }, 
				new GRGEN_LGSP.PatternEdge[] { testRule_edge__edge0, testRule_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				testRule_minMatches,
				testRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
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


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_testRule curMatch = (Match_testRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_a = curMatch._node_a;
			GRGEN_LGSP.LGSPNode node_f = curMatch._node_f;
			GRGEN_LGSP.LGSPNode node_m = curMatch._node_m;
			graph.SettingAddedNodeNames( testRule_addedNodeNames );
			GRGEN_LGSP.LGSPNode node_are = graph.Retype(node_a, GRGEN_MODEL.NodeType_D2211_2222_31.typeVar);
			GRGEN_MODEL.ID2211_2222_31 inode_are = (GRGEN_MODEL.ID2211_2222_31) node_are;
			GRGEN_LGSP.LGSPNode node_fre = graph.Retype(node_f, GRGEN_MODEL.NodeType_D231_4121.typeVar);
			GRGEN_MODEL.ID231_4121 inode_fre = (GRGEN_MODEL.ID231_4121) node_fre;
			GRGEN_LGSP.LGSPNode node_mre = graph.Retype(node_m, GRGEN_MODEL.NodeType_D11_2221.typeVar);
			GRGEN_MODEL.ID11_2221 inode_mre = (GRGEN_MODEL.ID11_2221) node_mre;
			graph.SettingAddedEdgeNames( testRule_addedEdgeNames );
			int tempvar_i = 1234;
			graph.ChangingNodeAttribute(node_are, GRGEN_MODEL.NodeType_D2211_2222_31.AttributeType_d2211_2222_31, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_i, null);
			inode_are.@d2211_2222_31 = tempvar_i;
			tempvar_i = 5678;
			graph.ChangingNodeAttribute(node_fre, GRGEN_MODEL.NodeType_D231_4121.AttributeType_d231_4121, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_i, null);
			inode_fre.@d231_4121 = tempvar_i;
			tempvar_i = 9012;
			graph.ChangingNodeAttribute(node_mre, GRGEN_MODEL.NodeType_D11_2221.AttributeType_d11_2221, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_i, null);
			inode_mre.@d11_2221 = tempvar_i;
			return;
		}
		private static string[] testRule_addedNodeNames = new string[] {  };
		private static string[] testRule_addedEdgeNames = new string[] {  };

		static Rule_testRule() {
		}

		public interface IMatch_testRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.ID231_4121 node_a { get; }
			GRGEN_MODEL.IB21 node_f { get; }
			GRGEN_MODEL.ID2211_2222_31 node_m { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_testRule : GRGEN_LGSP.ListElement<Match_testRule>, IMatch_testRule
		{
			public GRGEN_MODEL.ID231_4121 node_a { get { return (GRGEN_MODEL.ID231_4121)_node_a; } }
			public GRGEN_MODEL.IB21 node_f { get { return (GRGEN_MODEL.IB21)_node_f; } }
			public GRGEN_MODEL.ID2211_2222_31 node_m { get { return (GRGEN_MODEL.ID2211_2222_31)_node_m; } }
			public GRGEN_LGSP.LGSPNode _node_a;
			public GRGEN_LGSP.LGSPNode _node_f;
			public GRGEN_LGSP.LGSPNode _node_m;
			public enum testRule_NodeNums { @a, @f, @m, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)testRule_NodeNums.@a: return _node_a;
				case (int)testRule_NodeNums.@f: return _node_f;
				case (int)testRule_NodeNums.@m: return _node_m;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum testRule_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)testRule_EdgeNums.@_edge0: return _edge__edge0;
				case (int)testRule_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum testRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum testRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum testRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum testRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum testRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_testRule.instance.pat_testRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class test_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public test_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[1];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+1];
			rules[0] = Rule_testRule.Instance;
			rulesAndSubpatterns[0+0] = Rule_testRule.Instance;
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
	}


    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_testRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_testRule.IMatch_testRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IGraph graph);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max);
    }
    
    public class Action_testRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_testRule
    {
        public Action_testRule() {
            _rulePattern = Rule_testRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_testRule.Match_testRule, Rule_testRule.IMatch_testRule>(this);
        }

        public Rule_testRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "testRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_testRule.Match_testRule, Rule_testRule.IMatch_testRule> matches;

        public static Action_testRule Instance { get { return instance; } }
        private static Action_testRule instance = new Action_testRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup testRule_edge__edge1 
            int type_id_candidate_testRule_edge__edge1 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_testRule_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_testRule_edge__edge1], candidate_testRule_edge__edge1 = head_candidate_testRule_edge__edge1.lgspTypeNext; candidate_testRule_edge__edge1 != head_candidate_testRule_edge__edge1; candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.lgspTypeNext)
            {
                uint prev__candidate_testRule_edge__edge1;
                prev__candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_testRule_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source testRule_node_f from testRule_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_testRule_node_f = candidate_testRule_edge__edge1.lgspSource;
                if(candidate_testRule_node_f.lgspType.TypeID!=6) {
                    candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
                    continue;
                }
                // Implicit Target testRule_node_m from testRule_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_testRule_node_m = candidate_testRule_edge__edge1.lgspTarget;
                if(candidate_testRule_node_m.lgspType.TypeID!=17) {
                    candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
                    continue;
                }
                // Extend Incoming testRule_edge__edge0 from testRule_node_f 
                GRGEN_LGSP.LGSPEdge head_candidate_testRule_edge__edge0 = candidate_testRule_node_f.lgspInhead;
                if(head_candidate_testRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_testRule_edge__edge0 = head_candidate_testRule_edge__edge0;
                    do
                    {
                        if(candidate_testRule_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_testRule_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Implicit Source testRule_node_a from testRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_testRule_node_a = candidate_testRule_edge__edge0.lgspSource;
                        if(candidate_testRule_node_a.lgspType.TypeID!=18) {
                            continue;
                        }
                        Rule_testRule.Match_testRule match = matches.GetNextUnfilledPosition();
                        match._node_a = candidate_testRule_node_a;
                        match._node_f = candidate_testRule_node_f;
                        match._node_m = candidate_testRule_node_m;
                        match._edge__edge0 = candidate_testRule_edge__edge0;
                        match._edge__edge1 = candidate_testRule_edge__edge1;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_testRule_node_f.MoveInHeadAfter(candidate_testRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_testRule_edge__edge1);
                            candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
                            return matches;
                        }
                    }
                    while( (candidate_testRule_edge__edge0 = candidate_testRule_edge__edge0.lgspInNext) != head_candidate_testRule_edge__edge0 );
                }
                candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_testRule_edge__edge1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_testRule.IMatch_testRule match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches)
        {
            foreach(Rule_testRule.IMatch_testRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_testRule.IMatch_testRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IGraph graph, int maxMatches, object[] parameters)
        {
            return Match(graph, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(graph, (Rule_testRule.IMatch_testRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(Apply(graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, graph)) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyStar(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IGraph graph, params object[] parameters)
        {
            return ApplyPlus(graph);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            return ApplyMinMax(graph, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(graph, min, max);
        }
    }


    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class testActions : GRGEN_LGSP.LGSPActions
    {
        public testActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public testActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Rule_testRule.Instance);
            actions.Add("testRule", (GRGEN_LGSP.LGSPAction) Action_testRule.Instance);
            @testRule = Action_testRule.Instance;
            analyzer.ComputeInterPatternRelations();
        }
        
        public IAction_testRule @testRule;
        
        public override string Name { get { return "testActions"; } }
        public override string ModelMD5Hash { get { return "6a630d39ca3371b697e3fb227fb1f51a"; } }
    }
}