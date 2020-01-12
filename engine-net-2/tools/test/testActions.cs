// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "test.grg" on Sun Jan 12 22:27:34 CET 2020

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_complModel;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_test;

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
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

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
			bool[] testRule_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] testRule_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode testRule_node_a = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@D231_4121, GRGEN_MODEL.NodeType_D231_4121.typeVar, "GRGEN_MODEL.ID231_4121", "testRule_node_a", "a", testRule_node_a_AllowedTypes, testRule_node_a_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode testRule_node_f = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@B21, GRGEN_MODEL.NodeType_B21.typeVar, "GRGEN_MODEL.IB21", "testRule_node_f", "f", testRule_node_f_AllowedTypes, testRule_node_f_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode testRule_node_m = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@D2211_2222_31, GRGEN_MODEL.NodeType_D2211_2222_31.typeVar, "GRGEN_MODEL.ID2211_2222_31", "testRule_node_m", "m", testRule_node_m_AllowedTypes, testRule_node_m_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge testRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "testRule_edge__edge0", "_edge0", testRule_edge__edge0_AllowedTypes, testRule_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge testRule_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IDEdge", "testRule_edge__edge1", "_edge1", testRule_edge__edge1_AllowedTypes, testRule_edge__edge1_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_testRule = new GRGEN_LGSP.PatternGraph(
				"testRule",
				"",
				null, "testRule",
				false, false,
				new GRGEN_LGSP.PatternNode[] { testRule_node_a, testRule_node_f, testRule_node_m }, 
				new GRGEN_LGSP.PatternEdge[] { testRule_edge__edge0, testRule_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
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
				testRule_isEdgeHomomorphicGlobal,
				testRule_isNodeTotallyHomomorphic,
				testRule_isEdgeTotallyHomomorphic
			);
			pat_testRule.edgeToSourceNode.Add(testRule_edge__edge0, testRule_node_a);
			pat_testRule.edgeToTargetNode.Add(testRule_edge__edge0, testRule_node_f);
			pat_testRule.edgeToSourceNode.Add(testRule_edge__edge1, testRule_node_f);
			pat_testRule.edgeToTargetNode.Add(testRule_edge__edge1, testRule_node_m);

			testRule_node_a.pointOfDefinition = pat_testRule;
			testRule_node_f.pointOfDefinition = pat_testRule;
			testRule_node_m.pointOfDefinition = pat_testRule;
			testRule_edge__edge0.pointOfDefinition = pat_testRule;
			testRule_edge__edge1.pointOfDefinition = pat_testRule;

			patternGraph = pat_testRule;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
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
			{ // eval_0
			int tempvar_0 = (int )1234;
			graph.ChangingNodeAttribute(node_are, GRGEN_MODEL.NodeType_D2211_2222_31.AttributeType_d2211_2222_31, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			inode_are.@d2211_2222_31 = tempvar_0;
			graph.ChangedNodeAttribute(node_are, GRGEN_MODEL.NodeType_D2211_2222_31.AttributeType_d2211_2222_31);
			int tempvar_1 = (int )5678;
			graph.ChangingNodeAttribute(node_fre, GRGEN_MODEL.NodeType_D231_4121.AttributeType_d231_4121, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_1, null);
			inode_fre.@d231_4121 = tempvar_1;
			graph.ChangedNodeAttribute(node_fre, GRGEN_MODEL.NodeType_D231_4121.AttributeType_d231_4121);
			int tempvar_2 = (int )9012;
			graph.ChangingNodeAttribute(node_mre, GRGEN_MODEL.NodeType_D11_2221.AttributeType_d11_2221, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_2, null);
			inode_mre.@d11_2221 = tempvar_2;
			graph.ChangedNodeAttribute(node_mre, GRGEN_MODEL.NodeType_D11_2221.AttributeType_d11_2221);
			}
			return;
		}
		private static string[] testRule_addedNodeNames = new string[] {  };
		private static string[] testRule_addedEdgeNames = new string[] {  };

		static Rule_testRule() {
		}

		public interface IMatch_testRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.ID231_4121 node_a { get; set; }
			GRGEN_MODEL.IB21 node_f { get; set; }
			GRGEN_MODEL.ID2211_2222_31 node_m { get; set; }
			//Edges
			GRGEN_LIBGR.IDEdge edge__edge0 { get; set; }
			GRGEN_LIBGR.IDEdge edge__edge1 { get; set; }
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
			public GRGEN_MODEL.ID231_4121 node_a { get { return (GRGEN_MODEL.ID231_4121)_node_a; } set { _node_a = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IB21 node_f { get { return (GRGEN_MODEL.IB21)_node_f; } set { _node_f = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.ID2211_2222_31 node_m { get { return (GRGEN_MODEL.ID2211_2222_31)_node_m; } set { _node_m = (GRGEN_LGSP.LGSPNode)value; } }
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "a": return _node_a;
				case "f": return _node_f;
				case "m": return _node_m;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IDEdge edge__edge0 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LIBGR.IDEdge edge__edge1 { get { return (GRGEN_LIBGR.IDEdge)_edge__edge1; } set { _edge__edge1 = (GRGEN_LGSP.LGSPEdge)value; } }
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				case "_edge1": return _edge__edge1;
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
			public object getVariable(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_testRule.instance.pat_testRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_testRule(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_testRule nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_testRule cur = this;
				while(cur != null) {
					Match_testRule next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_testRule that)
			{
				_node_a = that._node_a;
				_node_f = that._node_f;
				_node_m = that._node_m;
				_edge__edge0 = that._edge__edge0;
				_edge__edge1 = that._edge__edge1;
			}

			public Match_testRule(Match_testRule that)
			{
				CopyMatchContent(that);
			}
			public Match_testRule()
			{
			}

			public bool IsEqual(Match_testRule that)
			{
				if(that==null) return false;
				if(_node_a != that._node_a) return false;
				if(_node_f != that._node_f) return false;
				if(_node_m != that._node_m) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				if(_edge__edge1 != that._edge__edge1) return false;
				return true;
			}
		}

	}

	public class Functions
	{

		static Functions() {
		}

	}

	public class Procedures
	{

		static Procedures() {
		}

	}

	public partial class MatchFilters
	{

		static MatchFilters() {
		}

	}


	//-----------------------------------------------------------

	public class test_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public test_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[1];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+1];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			functions = new GRGEN_LIBGR.FunctionInfo[0+0];
			procedures = new GRGEN_LIBGR.ProcedureInfo[0+0];
			packages = new string[0];
			rules[0] = Rule_testRule.Instance;
			rulesAndSubpatterns[0+0] = Rule_testRule.Instance;
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
		public override GRGEN_LIBGR.DefinedSequenceInfo[] DefinedSequences { get { return definedSequences; } }
		private GRGEN_LIBGR.DefinedSequenceInfo[] definedSequences;
		public override GRGEN_LIBGR.FunctionInfo[] Functions { get { return functions; } }
		private GRGEN_LIBGR.FunctionInfo[] functions;
		public override GRGEN_LIBGR.ProcedureInfo[] Procedures { get { return procedures; } }
		private GRGEN_LIBGR.ProcedureInfo[] procedures;
		public override string[] Packages { get { return packages; } }
		private string[] packages;
	}


    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_testRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testRule.IMatch_testRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
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

        public static Action_testRule Instance { get { return instance; } set { instance = value; } }
        private static Action_testRule instance = new Action_testRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup testRule_edge__edge1 
            int type_id_candidate_testRule_edge__edge1 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_testRule_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_testRule_edge__edge1], candidate_testRule_edge__edge1 = head_candidate_testRule_edge__edge1.lgspTypeNext; candidate_testRule_edge__edge1 != head_candidate_testRule_edge__edge1; candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.lgspTypeNext)
            {
                uint prev__candidate_testRule_edge__edge1;
                prev__candidate_testRule_edge__edge1 = candidate_testRule_edge__edge1.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                candidate_testRule_edge__edge1.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                // Implicit Source testRule_node_f from testRule_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_testRule_node_f = candidate_testRule_edge__edge1.lgspSource;
                if(candidate_testRule_node_f.lgspType.TypeID!=6) {
                    candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
                    continue;
                }
                // Implicit Target testRule_node_m from testRule_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_testRule_node_m = candidate_testRule_edge__edge1.lgspTarget;
                if(candidate_testRule_node_m.lgspType.TypeID!=17) {
                    candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
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
                        if((candidate_testRule_edge__edge0.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
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
                            candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
                            return matches;
                        }
                    }
                    while( (candidate_testRule_edge__edge0 = candidate_testRule_edge__edge0.lgspInNext) != head_candidate_testRule_edge__edge0 );
                }
                candidate_testRule_edge__edge1.lgspFlags = candidate_testRule_edge__edge1.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testRule_edge__edge1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testRule.IMatch_testRule match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches)
        {
            foreach(Rule_testRule.IMatch_testRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_testRule.IMatch_testRule match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_testRule.IMatch_testRule)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_testRule.IMatch_testRule>)matches);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[0]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            
            if(Apply(actionEnv)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv)) {
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[0]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[0]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[0]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            return ApplyStar(actionEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            return ApplyPlus(actionEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            return ApplyMinMax(actionEnv, min, max);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max);
        }
        public void Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, GRGEN_LIBGR.FilterCall filter)
        {
            if(filter.IsAutoSupplied) {
                switch(filter.Name) {
                    case "keepFirst": matches.FilterKeepFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLast": matches.FilterKeepLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepFirstFraction": matches.FilterKeepFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "keepLastFraction": matches.FilterKeepLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirst": matches.FilterRemoveFirst((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLast": matches.FilterRemoveLast((int)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeFirstFraction": matches.FilterRemoveFirstFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    case "removeLastFraction": matches.FilterRemoveLastFraction((double)(filter.ArgumentExpressions[0]!=null ? filter.ArgumentExpressions[0].Evaluate((GRGEN_LIBGR.IGraphProcessingEnvironment)actionEnv) : filter.Arguments[0])); break;
                    default: throw new Exception("Unknown auto supplied filter name!");
                }
                return;
            }
            switch(filter.FullName) {
                default: throw new Exception("Unknown filter name!");
            }
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
            packages = new string[0];
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(Rule_testRule.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_testRule.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_testRule.Instance);
            actions.Add("testRule", (GRGEN_LGSP.LGSPAction) Action_testRule.Instance);
            @testRule = Action_testRule.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_testRule.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_testRule.Instance.patternGraph);
            Rule_testRule.Instance.patternGraph.maxIsoSpace = 0;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_testRule.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
        }
        
        public IAction_testRule @testRule;
        
        
        public override string[] Packages { get { return packages; } }
        private string[] packages;
        
        public override string Name { get { return "testActions"; } }
        public override string StatisticsPath { get { return null; } }
        public override bool LazyNIC { get { return false; } }
        public override bool InlineIndependents { get { return true; } }
        public override bool Profile { get { return false; } }

        public override string ModelMD5Hash { get { return "6a630d39ca3371b697e3fb227fb1f51a"; } }
    }
}