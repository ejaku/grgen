// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\ExternalAttributeEvaluationExample\ExternalAttributeEvaluation.grg" on Sun Jul 29 09:00:50 CEST 2018

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_ExternalAttributeEvaluation;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_ExternalAttributeEvaluation;

namespace de.unika.ipd.grGen.Action_ExternalAttributeEvaluation
{
	public class Rule_init : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init instance = null;
		public static Rule_init Instance { get { if (instance==null) { instance = new Rule_init(); instance.initialize(); } return instance; } }

		public enum init_NodeNums { };
		public enum init_EdgeNums { };
		public enum init_VariableNums { };
		public enum init_SubNums { };
		public enum init_AltNums { };
		public enum init_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_init;


		private Rule_init()
		{
			name = "init";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] init_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] init_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] init_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_init = new GRGEN_LGSP.PatternGraph(
				"init",
				"",
				null, "init",
				false, false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init_isNodeHomomorphicGlobal,
				init_isEdgeHomomorphicGlobal,
				init_isNodeTotallyHomomorphic,
				init_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_init;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_init curMatch = (Match_init)_curMatch;
			graph.SettingAddedNodeNames( init_addedNodeNames );
			GRGEN_MODEL.@N node_n = GRGEN_MODEL.@N.CreateNode(graph);
			graph.SettingAddedEdgeNames( init_addedEdgeNames );
			GRGEN_MODEL.@E edge__edge0 = GRGEN_MODEL.@E.CreateEdge(graph, node_n, node_n);
			return;
		}
		private static string[] init_addedNodeNames = new string[] { "n" };
		private static string[] init_addedEdgeNames = new string[] { "_edge0" };

		static Rule_init() {
		}

		public interface IMatch_init : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_init : GRGEN_LGSP.ListElement<Match_init>, IMatch_init
		{
			public enum init_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public enum init_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public enum init_VariableNums { END_OF_ENUM };
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
			
			public enum init_SubNums { END_OF_ENUM };
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
			
			public enum init_AltNums { END_OF_ENUM };
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
			
			public enum init_IterNums { END_OF_ENUM };
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
			
			public enum init_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_init.instance.pat_init; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_init(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_init nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_init cur = this;
				while(cur != null) {
					Match_init next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_init that)
			{
			}

			public Match_init(Match_init that)
			{
				CopyMatchContent(that);
			}
			public Match_init()
			{
			}

			public bool IsEqual(Match_init that)
			{
				if(that==null) return false;
				return true;
			}
		}

	}

	public class Rule_init2 : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init2 instance = null;
		public static Rule_init2 Instance { get { if (instance==null) { instance = new Rule_init2(); instance.initialize(); } return instance; } }

		public enum init2_NodeNums { };
		public enum init2_EdgeNums { };
		public enum init2_VariableNums { };
		public enum init2_SubNums { };
		public enum init2_AltNums { };
		public enum init2_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_init2;


		private Rule_init2()
		{
			name = "init2";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] init2_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init2_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] init2_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] init2_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_init2 = new GRGEN_LGSP.PatternGraph(
				"init2",
				"",
				null, "init2",
				false, false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init2_isNodeHomomorphicGlobal,
				init2_isEdgeHomomorphicGlobal,
				init2_isNodeTotallyHomomorphic,
				init2_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_init2;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_init2 curMatch = (Match_init2)_curMatch;
			graph.SettingAddedNodeNames( init2_addedNodeNames );
			GRGEN_MODEL.@N node_n = GRGEN_MODEL.@N.CreateNode(graph);
			graph.SettingAddedEdgeNames( init2_addedEdgeNames );
			GRGEN_MODEL.@E edge__edge0 = GRGEN_MODEL.@E.CreateEdge(graph, node_n, node_n);
			{ // eval_0
			GRGEN_MODEL.Own tempvar_0 = (GRGEN_MODEL.Own )GRGEN_EXPR.ExternalFunctions.own(actionEnv, graph);
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_ow, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			node_n.@ow = tempvar_0;
			graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_ow);
			}
			GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;
			ApplyXGRS_init2_0(procEnv, (GRGEN_MODEL.IN)node_n);
			return;
		}
		private static string[] init2_addedNodeNames = new string[] { "n" };
		private static string[] init2_addedEdgeNames = new string[] { "_edge0" };

        public static bool ApplyXGRS_init2_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IN var_n)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            procEnv.DebugEntering("init2.exec_0", "{::nn=n;::v=n.ow}");
            bool res_11;
            object res_10;
            object res_3;
            object res_0;
                        object res_9;
            object res_4;
                        procEnv.SetVariableValue("nn", var_n);
            res_0 = procEnv.GetVariableValue("nn");
            res_3 = res_0;
            procEnv.SetVariableValue("v", (object)(((GRGEN_LIBGR.IGraphElement)var_n).GetAttribute("ow")));
            res_4 = procEnv.GetVariableValue("v");
            res_9 = res_4;
            res_10 = res_9;
            res_11 = (bool)(true);
            procEnv.DebugExiting("init2.exec_0");
            return res_11;
        }

		static Rule_init2() {
		}

		public interface IMatch_init2 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_init2 : GRGEN_LGSP.ListElement<Match_init2>, IMatch_init2
		{
			public enum init2_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public enum init2_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public enum init2_VariableNums { END_OF_ENUM };
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
			
			public enum init2_SubNums { END_OF_ENUM };
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
			
			public enum init2_AltNums { END_OF_ENUM };
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
			
			public enum init2_IterNums { END_OF_ENUM };
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
			
			public enum init2_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_init2.instance.pat_init2; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_init2(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_init2 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_init2 cur = this;
				while(cur != null) {
					Match_init2 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_init2 that)
			{
			}

			public Match_init2(Match_init2 that)
			{
				CopyMatchContent(that);
			}
			public Match_init2()
			{
			}

			public bool IsEqual(Match_init2 that)
			{
				if(that==null) return false;
				return true;
			}
		}

	}

	public class Rule_r : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_r instance = null;
		public static Rule_r Instance { get { if (instance==null) { instance = new Rule_r(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] r_node_n_AllowedTypes = null;
		public static bool[] r_node_n_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] r_edge_e_AllowedTypes = null;
		public static bool[] r_edge_e_IsAllowedType = null;
		public enum r_NodeNums { @n, };
		public enum r_EdgeNums { @e, };
		public enum r_VariableNums { };
		public enum r_SubNums { };
		public enum r_AltNums { };
		public enum r_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_r;


		private Rule_r()
		{
			name = "r";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] r_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] r_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] r_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] r_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode r_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, GRGEN_MODEL.NodeType_N.typeVar, "GRGEN_MODEL.IN", "r_node_n", "n", r_node_n_AllowedTypes, r_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge r_edge_e = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@E, GRGEN_MODEL.EdgeType_E.typeVar, "GRGEN_MODEL.IE", "r_edge_e", "e", r_edge_e_AllowedTypes, r_edge_e_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition r_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.LOG_AND(new GRGEN_EXPR.ExternalFunctionInvocation("foo", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.Constant("42"), new GRGEN_EXPR.Constant("3.141"), new GRGEN_EXPR.ConstantEnumExpression("Enu", "hurz"), new GRGEN_EXPR.Constant("\"S21-heiteitei\""), }, new String[] {null, null, null, null, }), new GRGEN_EXPR.ExternalFunctionInvocation("foo", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "i"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "d"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "enu"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "s"), }, new String[] {null, null, null, null, })),
				new string[] { "r_node_n" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition r_cond_1 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.ExternalFunctionInvocation("isnull", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.ExternalFunctionInvocation("bar", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.Constant("null"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "o"), }, new String[] {null, null, }), }, new String[] {null, }),
				new string[] { "r_node_n" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition r_cond_2 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.ExternalFunctionInvocation("bla", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.GraphEntityExpression("r_node_n"), new GRGEN_EXPR.GraphEntityExpression("r_edge_e"), }, new String[] {"GRGEN_MODEL.IN", "GRGEN_MODEL.IE", }),
				new string[] { "r_node_n" }, new string[] { "r_edge_e" }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition r_cond_3 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.LOG_AND(new GRGEN_EXPR.ExternalFunctionInvocation("hur", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.ExternalFunctionInvocation("har", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "ow"), new GRGEN_EXPR.ExternalFunctionInvocation("har", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "op"), }, new String[] {"GRGEN_MODEL.Own", "GRGEN_MODEL.OwnPown", }), }, new String[] {"GRGEN_MODEL.Own", "GRGEN_MODEL.OwnPown", }), }, new String[] {"GRGEN_MODEL.OwnPown", }), new GRGEN_EXPR.ExternalFunctionInvocation("hurdur", new GRGEN_EXPR.Expression[] {new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "r_node_n", "oh"), }, new String[] {"GRGEN_MODEL.OwnPownHome", })),
				new string[] { "r_node_n" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_r = new GRGEN_LGSP.PatternGraph(
				"r",
				"",
				null, "r",
				false, false,
				new GRGEN_LGSP.PatternNode[] { r_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { r_edge_e }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { r_cond_0, r_cond_1, r_cond_2, r_cond_3,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				r_isNodeHomomorphicGlobal,
				r_isEdgeHomomorphicGlobal,
				r_isNodeTotallyHomomorphic,
				r_isEdgeTotallyHomomorphic
			);
			pat_r.edgeToSourceNode.Add(r_edge_e, r_node_n);
			pat_r.edgeToTargetNode.Add(r_edge_e, r_node_n);

			r_node_n.pointOfDefinition = pat_r;
			r_edge_e.pointOfDefinition = pat_r;

			patternGraph = pat_r;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_r curMatch = (Match_r)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			GRGEN_MODEL.IN inode_n = curMatch.node_n;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			graph.SettingAddedNodeNames( r_addedNodeNames );
			GRGEN_MODEL.@N node_m = GRGEN_MODEL.@N.CreateNode(graph);
			graph.SettingAddedEdgeNames( r_addedEdgeNames );
			{ // eval_0
			bool tempvar_0 = (bool )(GRGEN_EXPR.ExternalFunctions.foo(actionEnv, graph, 42, 3.141, GRGEN_MODEL.ENUM_Enu.@hurz, "S21-heiteitei") && GRGEN_EXPR.ExternalFunctions.foo(actionEnv, graph, inode_n.@i, inode_n.@d, inode_n.@enu, inode_n.@s));
			graph.ChangingNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_b, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
			node_m.@b = tempvar_0;
			graph.ChangedNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_b);
			Object tempvar_1 = (Object )GRGEN_EXPR.ExternalFunctions.bar(actionEnv, graph, null, inode_n.@o);
			graph.ChangingNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_o, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_1, null);
			node_m.@o = tempvar_1;
			graph.ChangedNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_o);
			bool tempvar_2 = (bool )GRGEN_EXPR.ExternalFunctions.bla(actionEnv, graph, (GRGEN_MODEL.IN)node_m, (GRGEN_MODEL.IE)edge_e);
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_b, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_2, null);
			inode_n.@b = tempvar_2;
			graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_b);
			GRGEN_MODEL.OwnPown tempvar_3 = (GRGEN_MODEL.OwnPown )GRGEN_EXPR.ExternalFunctions.har(actionEnv, graph, (GRGEN_MODEL.Own)inode_n.@ow, (GRGEN_MODEL.OwnPown)inode_n.@op);
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_op, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_3, null);
			inode_n.@op = tempvar_3;
			graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_op);
			}
			return;
		}
		private static string[] r_addedNodeNames = new string[] { "m" };
		private static string[] r_addedEdgeNames = new string[] {  };

		static Rule_r() {
		}

		public interface IMatch_r : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IN node_n { get; set; }
			//Edges
			GRGEN_MODEL.IE edge_e { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_r : GRGEN_LGSP.ListElement<Match_r>, IMatch_r
		{
			public GRGEN_MODEL.IN node_n { get { return (GRGEN_MODEL.IN)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum r_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)r_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IE edge_e { get { return (GRGEN_MODEL.IE)_edge_e; } set { _edge_e = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_e;
			public enum r_EdgeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)r_EdgeNums.@e: return _edge_e;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "e": return _edge_e;
				default: return null;
				}
			}
			
			public enum r_VariableNums { END_OF_ENUM };
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
			
			public enum r_SubNums { END_OF_ENUM };
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
			
			public enum r_AltNums { END_OF_ENUM };
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
			
			public enum r_IterNums { END_OF_ENUM };
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
			
			public enum r_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_r.instance.pat_r; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_r(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_r nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_r cur = this;
				while(cur != null) {
					Match_r next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_r that)
			{
				_node_n = that._node_n;
				_edge_e = that._edge_e;
			}

			public Match_r(Match_r that)
			{
				CopyMatchContent(that);
			}
			public Match_r()
			{
			}

			public bool IsEqual(Match_r that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				if(_edge_e != that._edge_e) return false;
				return true;
			}
		}

	}

	public class Rule_rp : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_rp instance = null;
		public static Rule_rp Instance { get { if (instance==null) { instance = new Rule_rp(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] rp_node_n_AllowedTypes = null;
		public static bool[] rp_node_n_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] rp_edge_e_AllowedTypes = null;
		public static bool[] rp_edge_e_IsAllowedType = null;
		public enum rp_NodeNums { @n, };
		public enum rp_EdgeNums { @e, };
		public enum rp_VariableNums { };
		public enum rp_SubNums { };
		public enum rp_AltNums { };
		public enum rp_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_rp;


		private Rule_rp()
		{
			name = "rp";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] rp_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] rp_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] rp_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] rp_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode rp_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, GRGEN_MODEL.NodeType_N.typeVar, "GRGEN_MODEL.IN", "rp_node_n", "n", rp_node_n_AllowedTypes, rp_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge rp_edge_e = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@E, GRGEN_MODEL.EdgeType_E.typeVar, "GRGEN_MODEL.IE", "rp_edge_e", "e", rp_edge_e_AllowedTypes, rp_edge_e_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_rp = new GRGEN_LGSP.PatternGraph(
				"rp",
				"",
				null, "rp",
				false, false,
				new GRGEN_LGSP.PatternNode[] { rp_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { rp_edge_e }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				rp_isNodeHomomorphicGlobal,
				rp_isEdgeHomomorphicGlobal,
				rp_isNodeTotallyHomomorphic,
				rp_isEdgeTotallyHomomorphic
			);
			pat_rp.edgeToSourceNode.Add(rp_edge_e, rp_node_n);
			pat_rp.edgeToTargetNode.Add(rp_edge_e, rp_node_n);

			rp_node_n.pointOfDefinition = pat_rp;
			rp_edge_e.pointOfDefinition = pat_rp;

			patternGraph = pat_rp;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_rp curMatch = (Match_rp)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			GRGEN_MODEL.IN inode_n = curMatch.node_n;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			graph.SettingAddedNodeNames( rp_addedNodeNames );
			GRGEN_MODEL.@N node_m = GRGEN_MODEL.@N.CreateNode(graph);
			graph.SettingAddedEdgeNames( rp_addedEdgeNames );
			{ // eval_0
			GRGEN_MODEL.IN node_nn = null;
			GRGEN_EXPR.ExternalProcedures.fooProc(actionEnv, graph, 42, 3.141, GRGEN_MODEL.ENUM_Enu.@hurz, "S21-heiteitei");
			GRGEN_EXPR.ExternalProcedures.fooProc(actionEnv, graph, inode_n.@i, inode_n.@d, inode_n.@enu, inode_n.@s);
			object outvar_0;
			GRGEN_EXPR.ExternalProcedures.barProc(actionEnv, graph, null, inode_n.@o, out outvar_0);
			Object tempvar_1 = (Object )outvar_0;
			graph.ChangingNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_o, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_1, null);
			node_m.@o = tempvar_1;
			graph.ChangedNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_o);
			bool outvar_2;
			bool outvar_3;
			GRGEN_EXPR.ExternalProcedures.blaProc(actionEnv, graph, (GRGEN_MODEL.IN)node_m, (GRGEN_MODEL.IE)edge_e, out outvar_2, out outvar_3);
			bool tempvar_4 = (bool )outvar_2;
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_b, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_4, null);
			inode_n.@b = tempvar_4;
			graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_b);
			bool tempvar_5 = (bool )outvar_3;
			graph.ChangingNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_b, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_5, null);
			node_m.@b = tempvar_5;
			graph.ChangedNodeAttribute(node_m, GRGEN_MODEL.NodeType_N.AttributeType_b);
			GRGEN_MODEL.OwnPown outvar_6;
			GRGEN_MODEL.Own outvar_7;
			GRGEN_MODEL.IN outvar_8;
			GRGEN_EXPR.ExternalProcedures.harProc(actionEnv, graph, (GRGEN_MODEL.Own)inode_n.@ow, (GRGEN_MODEL.OwnPown)inode_n.@op, out outvar_6, out outvar_7, out outvar_8);
			GRGEN_MODEL.OwnPown tempvar_9 = (GRGEN_MODEL.OwnPown )outvar_6;
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_op, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_9, null);
			inode_n.@op = tempvar_9;
			graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_op);
			GRGEN_MODEL.Own tempvar_10 = (GRGEN_MODEL.Own )outvar_7;
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_ow, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_10, null);
			inode_n.@ow = tempvar_10;
			graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_N.AttributeType_ow);
			node_nn = outvar_8;
			}
			return;
		}
		private static string[] rp_addedNodeNames = new string[] { "m" };
		private static string[] rp_addedEdgeNames = new string[] {  };

		static Rule_rp() {
		}

		public interface IMatch_rp : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IN node_n { get; set; }
			//Edges
			GRGEN_MODEL.IE edge_e { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_rp : GRGEN_LGSP.ListElement<Match_rp>, IMatch_rp
		{
			public GRGEN_MODEL.IN node_n { get { return (GRGEN_MODEL.IN)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum rp_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)rp_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IE edge_e { get { return (GRGEN_MODEL.IE)_edge_e; } set { _edge_e = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_e;
			public enum rp_EdgeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)rp_EdgeNums.@e: return _edge_e;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "e": return _edge_e;
				default: return null;
				}
			}
			
			public enum rp_VariableNums { END_OF_ENUM };
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
			
			public enum rp_SubNums { END_OF_ENUM };
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
			
			public enum rp_AltNums { END_OF_ENUM };
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
			
			public enum rp_IterNums { END_OF_ENUM };
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
			
			public enum rp_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_rp.instance.pat_rp; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_rp(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_rp nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_rp cur = this;
				while(cur != null) {
					Match_rp next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_rp that)
			{
				_node_n = that._node_n;
				_edge_e = that._edge_e;
			}

			public Match_rp(Match_rp that)
			{
				CopyMatchContent(that);
			}
			public Match_rp()
			{
			}

			public bool IsEqual(Match_rp that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				if(_edge_e != that._edge_e) return false;
				return true;
			}
		}

	}

	public class Rule_testCopy : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_testCopy instance = null;
		public static Rule_testCopy Instance { get { if (instance==null) { instance = new Rule_testCopy(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] testCopy_node_n_AllowedTypes = null;
		public static bool[] testCopy_node_n_IsAllowedType = null;
		public enum testCopy_NodeNums { @n, };
		public enum testCopy_EdgeNums { };
		public enum testCopy_VariableNums { };
		public enum testCopy_SubNums { };
		public enum testCopy_AltNums { };
		public enum testCopy_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_testCopy;


		private Rule_testCopy()
		{
			name = "testCopy";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] testCopy_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] testCopy_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] testCopy_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] testCopy_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode testCopy_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, GRGEN_MODEL.NodeType_N.typeVar, "GRGEN_MODEL.IN", "testCopy_node_n", "n", testCopy_node_n_AllowedTypes, testCopy_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_testCopy = new GRGEN_LGSP.PatternGraph(
				"testCopy",
				"",
				null, "testCopy",
				false, false,
				new GRGEN_LGSP.PatternNode[] { testCopy_node_n }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				testCopy_isNodeHomomorphicGlobal,
				testCopy_isEdgeHomomorphicGlobal,
				testCopy_isNodeTotallyHomomorphic,
				testCopy_isEdgeTotallyHomomorphic
			);

			testCopy_node_n.pointOfDefinition = pat_testCopy;

			patternGraph = pat_testCopy;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_testCopy curMatch = (Match_testCopy)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			graph.SettingAddedNodeNames( testCopy_addedNodeNames );
			GRGEN_LGSP.LGSPNode node_nn = (GRGEN_LGSP.LGSPNode) node_n.Clone();
			graph.AddNode(node_nn);
			graph.SettingAddedEdgeNames( testCopy_addedEdgeNames );
			return;
		}
		private static string[] testCopy_addedNodeNames = new string[] { "nn" };
		private static string[] testCopy_addedEdgeNames = new string[] {  };

		static Rule_testCopy() {
		}

		public interface IMatch_testCopy : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IN node_n { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_testCopy : GRGEN_LGSP.ListElement<Match_testCopy>, IMatch_testCopy
		{
			public GRGEN_MODEL.IN node_n { get { return (GRGEN_MODEL.IN)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum testCopy_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)testCopy_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				default: return null;
				}
			}
			
			public enum testCopy_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public enum testCopy_VariableNums { END_OF_ENUM };
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
			
			public enum testCopy_SubNums { END_OF_ENUM };
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
			
			public enum testCopy_AltNums { END_OF_ENUM };
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
			
			public enum testCopy_IterNums { END_OF_ENUM };
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
			
			public enum testCopy_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_testCopy.instance.pat_testCopy; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_testCopy(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_testCopy nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_testCopy cur = this;
				while(cur != null) {
					Match_testCopy next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_testCopy that)
			{
				_node_n = that._node_n;
			}

			public Match_testCopy(Match_testCopy that)
			{
				CopyMatchContent(that);
			}
			public Match_testCopy()
			{
			}

			public bool IsEqual(Match_testCopy that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				return true;
			}
		}

	}

	public class Rule_testComparison : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_testComparison instance = null;
		public static Rule_testComparison Instance { get { if (instance==null) { instance = new Rule_testComparison(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] testComparison_node_n_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] testComparison_node_m_AllowedTypes = null;
		public static bool[] testComparison_node_n_IsAllowedType = null;
		public static bool[] testComparison_node_m_IsAllowedType = null;
		public enum testComparison_NodeNums { @n, @m, };
		public enum testComparison_EdgeNums { };
		public enum testComparison_VariableNums { };
		public enum testComparison_SubNums { };
		public enum testComparison_AltNums { };
		public enum testComparison_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_testComparison;


		private Rule_testComparison()
		{
			name = "testComparison";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new GRGEN_LGSP.LGSPFilter[] { };

		}
		private void initialize()
		{
			bool[,] testComparison_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] testComparison_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] testComparison_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] testComparison_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode testComparison_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, GRGEN_MODEL.NodeType_N.typeVar, "GRGEN_MODEL.IN", "testComparison_node_n", "n", testComparison_node_n_AllowedTypes, testComparison_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode testComparison_node_m = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@N, GRGEN_MODEL.NodeType_N.typeVar, "GRGEN_MODEL.IN", "testComparison_node_m", "m", testComparison_node_m_AllowedTypes, testComparison_node_m_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition testComparison_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "ow")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_1 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_NE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "ow")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_2 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_LT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "ow")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_3 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_LE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "ow")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_4 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "ow")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_5 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_GE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "ow")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_6 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "op"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_7 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_NE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "op"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_8 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_LT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "op"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_9 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_LE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "op"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_10 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "op"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_11 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_GE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "op"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_12 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_13 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_NE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_14 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_LT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_15 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_LE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_16 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			GRGEN_LGSP.PatternCondition testComparison_cond_17 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EXTERNAL_GE(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_n", "ow"), new GRGEN_EXPR.Qualification("GRGEN_MODEL.IN", "testComparison_node_m", "op")),
				new string[] { "testComparison_node_n", "testComparison_node_m" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_testComparison = new GRGEN_LGSP.PatternGraph(
				"testComparison",
				"",
				null, "testComparison",
				false, false,
				new GRGEN_LGSP.PatternNode[] { testComparison_node_n, testComparison_node_m }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { testComparison_cond_0, testComparison_cond_1, testComparison_cond_2, testComparison_cond_3, testComparison_cond_4, testComparison_cond_5, testComparison_cond_6, testComparison_cond_7, testComparison_cond_8, testComparison_cond_9, testComparison_cond_10, testComparison_cond_11, testComparison_cond_12, testComparison_cond_13, testComparison_cond_14, testComparison_cond_15, testComparison_cond_16, testComparison_cond_17,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				testComparison_isNodeHomomorphicGlobal,
				testComparison_isEdgeHomomorphicGlobal,
				testComparison_isNodeTotallyHomomorphic,
				testComparison_isEdgeTotallyHomomorphic
			);

			testComparison_node_n.pointOfDefinition = pat_testComparison;
			testComparison_node_m.pointOfDefinition = pat_testComparison;

			patternGraph = pat_testComparison;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_testComparison curMatch = (Match_testComparison)_curMatch;
			GRGEN_MODEL.IN inode_n = curMatch.node_n;
			GRGEN_MODEL.IN inode_m = curMatch.node_m;
			graph.SettingAddedNodeNames( testComparison_addedNodeNames );
			graph.SettingAddedEdgeNames( testComparison_addedEdgeNames );
			{ // eval_0
			bool var_b = (bool)((((((GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@ow) ^ !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@ow)) ^ GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@ow)) ^ (GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@ow)|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@ow))) ^ (!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@ow)&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@ow))) ^ !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@ow)));
			var_b = (bool) ((((((GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@op,inode_m.@op) ^ !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@op,inode_m.@op)) ^ GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@op,inode_m.@op)) ^ (GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@op,inode_m.@op)|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@op,inode_m.@op))) ^ (!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@op,inode_m.@op)&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@op,inode_m.@op))) ^ !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@op,inode_m.@op)));
			var_b = (bool) ((((((GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@op) ^ !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@op)) ^ GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@op)) ^ (GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@op)|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@op))) ^ (!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@op)&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(inode_n.@ow,inode_m.@op))) ^ !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(inode_n.@ow,inode_m.@op)));
			}
			return;
		}
		private static string[] testComparison_addedNodeNames = new string[] {  };
		private static string[] testComparison_addedEdgeNames = new string[] {  };

		static Rule_testComparison() {
		}

		public interface IMatch_testComparison : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IN node_n { get; set; }
			GRGEN_MODEL.IN node_m { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_testComparison : GRGEN_LGSP.ListElement<Match_testComparison>, IMatch_testComparison
		{
			public GRGEN_MODEL.IN node_n { get { return (GRGEN_MODEL.IN)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IN node_m { get { return (GRGEN_MODEL.IN)_node_m; } set { _node_m = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public GRGEN_LGSP.LGSPNode _node_m;
			public enum testComparison_NodeNums { @n, @m, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)testComparison_NodeNums.@n: return _node_n;
				case (int)testComparison_NodeNums.@m: return _node_m;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				case "m": return _node_m;
				default: return null;
				}
			}
			
			public enum testComparison_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public enum testComparison_VariableNums { END_OF_ENUM };
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
			
			public enum testComparison_SubNums { END_OF_ENUM };
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
			
			public enum testComparison_AltNums { END_OF_ENUM };
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
			
			public enum testComparison_IterNums { END_OF_ENUM };
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
			
			public enum testComparison_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_testComparison.instance.pat_testComparison; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_testComparison(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }
			public Match_testComparison nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_testComparison cur = this;
				while(cur != null) {
					Match_testComparison next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}
			public int _iterationNumber;
			public int IterationNumber { get { return _iterationNumber; } set { _iterationNumber = value; } }

			public void CopyMatchContent(Match_testComparison that)
			{
				_node_n = that._node_n;
				_node_m = that._node_m;
			}

			public Match_testComparison(Match_testComparison that)
			{
				CopyMatchContent(that);
			}
			public Match_testComparison()
			{
			}

			public bool IsEqual(Match_testComparison that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				if(_node_m != that._node_m) return false;
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

	public class FunctionInfo_foo : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_foo instance = null;
		public static FunctionInfo_foo Instance { get { if (instance==null) { instance = new FunctionInfo_foo(); } return instance; } }

		private FunctionInfo_foo()
					: base(
						"foo",
						null, "foo",
						true,
						new String[] { "in_0", "in_1", "in_2", "in_3",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)), GRGEN_LIBGR.VarType.GetVarType(typeof(double)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.ENUM_Enu)), GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(bool))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.foo((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (int)paramBindings.Arguments[0], (double)paramBindings.Arguments[1], (GRGEN_MODEL.ENUM_Enu)paramBindings.Arguments[2], (string)paramBindings.Arguments[3]);
		}
	}

	public class FunctionInfo_bar : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_bar instance = null;
		public static FunctionInfo_bar Instance { get { if (instance==null) { instance = new FunctionInfo_bar(); } return instance; } }

		private FunctionInfo_bar()
					: base(
						"bar",
						null, "bar",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)), GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(object))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.bar((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (object)paramBindings.Arguments[0], (object)paramBindings.Arguments[1]);
		}
	}

	public class FunctionInfo_isnull : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_isnull instance = null;
		public static FunctionInfo_isnull Instance { get { if (instance==null) { instance = new FunctionInfo_isnull(); } return instance; } }

		private FunctionInfo_isnull()
					: base(
						"isnull",
						null, "isnull",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(bool))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.isnull((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (object)paramBindings.Arguments[0]);
		}
	}

	public class FunctionInfo_bla : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_bla instance = null;
		public static FunctionInfo_bla Instance { get { if (instance==null) { instance = new FunctionInfo_bla(); } return instance; } }

		private FunctionInfo_bla()
					: base(
						"bla",
						null, "bla",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, GRGEN_MODEL.EdgeType_E.typeVar,  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(bool))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.bla((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.IN)paramBindings.Arguments[0], (GRGEN_MODEL.IE)paramBindings.Arguments[1]);
		}
	}

	public class FunctionInfo_blo : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_blo instance = null;
		public static FunctionInfo_blo Instance { get { if (instance==null) { instance = new FunctionInfo_blo(); } return instance; } }

		private FunctionInfo_blo()
					: base(
						"blo",
						null, "blo",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.EdgeType_Edge.typeVar,  },
						GRGEN_MODEL.NodeType_N.typeVar
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.blo((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_LIBGR.INode)paramBindings.Arguments[0], (GRGEN_LIBGR.IDEdge)paramBindings.Arguments[1]);
		}
	}

	public class FunctionInfo_har : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_har instance = null;
		public static FunctionInfo_har Instance { get { if (instance==null) { instance = new FunctionInfo_har(); } return instance; } }

		private FunctionInfo_har()
					: base(
						"har",
						null, "har",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.Own)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.har((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.Own)paramBindings.Arguments[0], (GRGEN_MODEL.OwnPown)paramBindings.Arguments[1]);
		}
	}

	public class FunctionInfo_hur : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_hur instance = null;
		public static FunctionInfo_hur Instance { get { if (instance==null) { instance = new FunctionInfo_hur(); } return instance; } }

		private FunctionInfo_hur()
					: base(
						"hur",
						null, "hur",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(bool))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.hur((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.OwnPown)paramBindings.Arguments[0]);
		}
	}

	public class FunctionInfo_hurdur : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_hurdur instance = null;
		public static FunctionInfo_hurdur Instance { get { if (instance==null) { instance = new FunctionInfo_hurdur(); } return instance; } }

		private FunctionInfo_hurdur()
					: base(
						"hurdur",
						null, "hurdur",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome)),  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(bool))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.hurdur((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.OwnPownHome)paramBindings.Arguments[0]);
		}
	}

	public class FunctionInfo_own : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_own instance = null;
		public static FunctionInfo_own Instance { get { if (instance==null) { instance = new FunctionInfo_own(); } return instance; } }

		private FunctionInfo_own()
					: base(
						"own",
						null, "own",
						true,
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.Own))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.own((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph);
		}
	}

	public class FunctionInfo_ownPown : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_ownPown instance = null;
		public static FunctionInfo_ownPown Instance { get { if (instance==null) { instance = new FunctionInfo_ownPown(); } return instance; } }

		private FunctionInfo_ownPown()
					: base(
						"ownPown",
						null, "ownPown",
						true,
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.ownPown((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph);
		}
	}

	public class FunctionInfo_ownPownHome : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_ownPownHome instance = null;
		public static FunctionInfo_ownPownHome Instance { get { if (instance==null) { instance = new FunctionInfo_ownPownHome(); } return instance; } }

		private FunctionInfo_ownPownHome()
					: base(
						"ownPownHome",
						null, "ownPownHome",
						true,
						new String[] {  },
						new GRGEN_LIBGR.GrGenType[] {  },
						GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome))
					  )
		{
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.FunctionInvocationParameterBindings paramBindings)
		{
			return GRGEN_EXPR.ExternalFunctions.ownPownHome((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph);
		}
	}

	public class ProcedureInfo_fooProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_fooProc instance = null;
		public static ProcedureInfo_fooProc Instance { get { if (instance==null) { instance = new ProcedureInfo_fooProc(); } return instance; } }

		private ProcedureInfo_fooProc()
					: base(
						"fooProc",
						null, "fooProc",
						true,
						new String[] { "in_0", "in_1", "in_2", "in_3",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)), GRGEN_LIBGR.VarType.GetVarType(typeof(double)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.ENUM_Enu)), GRGEN_LIBGR.VarType.GetVarType(typeof(string)),  },
						new GRGEN_LIBGR.GrGenType[] {  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			GRGEN_EXPR.ExternalProcedures.fooProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (int)paramBindings.Arguments[0], (double)paramBindings.Arguments[1], (GRGEN_MODEL.ENUM_Enu)paramBindings.Arguments[2], (string)paramBindings.Arguments[3]);
			return ReturnArray;
		}
	}

	public class ProcedureInfo_barProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_barProc instance = null;
		public static ProcedureInfo_barProc Instance { get { if (instance==null) { instance = new ProcedureInfo_barProc(); } return instance; } }

		private ProcedureInfo_barProc()
					: base(
						"barProc",
						null, "barProc",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)), GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			object _out_param_0;
			GRGEN_EXPR.ExternalProcedures.barProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (object)paramBindings.Arguments[0], (object)paramBindings.Arguments[1], out _out_param_0);
			ReturnArray[0] = _out_param_0;
			return ReturnArray;
		}
	}

	public class ProcedureInfo_isnullProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_isnullProc instance = null;
		public static ProcedureInfo_isnullProc Instance { get { if (instance==null) { instance = new ProcedureInfo_isnullProc(); } return instance; } }

		private ProcedureInfo_isnullProc()
					: base(
						"isnullProc",
						null, "isnullProc",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(object)),  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(bool)),  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			bool _out_param_0;
			GRGEN_EXPR.ExternalProcedures.isnullProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (object)paramBindings.Arguments[0], out _out_param_0);
			ReturnArray[0] = _out_param_0;
			return ReturnArray;
		}
	}

	public class ProcedureInfo_blaProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_blaProc instance = null;
		public static ProcedureInfo_blaProc Instance { get { if (instance==null) { instance = new ProcedureInfo_blaProc(); } return instance; } }

		private ProcedureInfo_blaProc()
					: base(
						"blaProc",
						null, "blaProc",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, GRGEN_MODEL.EdgeType_E.typeVar,  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(bool)), GRGEN_LIBGR.VarType.GetVarType(typeof(bool)),  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			bool _out_param_0;
			bool _out_param_1;
			GRGEN_EXPR.ExternalProcedures.blaProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.IN)paramBindings.Arguments[0], (GRGEN_MODEL.IE)paramBindings.Arguments[1], out _out_param_0, out _out_param_1);
			ReturnArray[0] = _out_param_0;
			ReturnArray[1] = _out_param_1;
			return ReturnArray;
		}
	}

	public class ProcedureInfo_bloProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_bloProc instance = null;
		public static ProcedureInfo_bloProc Instance { get { if (instance==null) { instance = new ProcedureInfo_bloProc(); } return instance; } }

		private ProcedureInfo_bloProc()
					: base(
						"bloProc",
						null, "bloProc",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, GRGEN_MODEL.EdgeType_Edge.typeVar,  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar,  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			GRGEN_MODEL.IN _out_param_0;
			GRGEN_EXPR.ExternalProcedures.bloProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_LIBGR.INode)paramBindings.Arguments[0], (GRGEN_LIBGR.IDEdge)paramBindings.Arguments[1], out _out_param_0);
			ReturnArray[0] = _out_param_0;
			return ReturnArray;
		}
	}

	public class ProcedureInfo_harProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_harProc instance = null;
		public static ProcedureInfo_harProc Instance { get { if (instance==null) { instance = new ProcedureInfo_harProc(); } return instance; } }

		private ProcedureInfo_harProc()
					: base(
						"harProc",
						null, "harProc",
						true,
						new String[] { "in_0", "in_1",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.Own)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)), GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.Own)), GRGEN_MODEL.NodeType_N.typeVar,  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			GRGEN_MODEL.OwnPown _out_param_0;
			GRGEN_MODEL.Own _out_param_1;
			GRGEN_MODEL.IN _out_param_2;
			GRGEN_EXPR.ExternalProcedures.harProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.Own)paramBindings.Arguments[0], (GRGEN_MODEL.OwnPown)paramBindings.Arguments[1], out _out_param_0, out _out_param_1, out _out_param_2);
			ReturnArray[0] = _out_param_0;
			ReturnArray[1] = _out_param_1;
			ReturnArray[2] = _out_param_2;
			return ReturnArray;
		}
	}

	public class ProcedureInfo_hurProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_hurProc instance = null;
		public static ProcedureInfo_hurProc Instance { get { if (instance==null) { instance = new ProcedureInfo_hurProc(); } return instance; } }

		private ProcedureInfo_hurProc()
					: base(
						"hurProc",
						null, "hurProc",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPown)),  },
						new GRGEN_LIBGR.GrGenType[] {  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			GRGEN_EXPR.ExternalProcedures.hurProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.OwnPown)paramBindings.Arguments[0]);
			return ReturnArray;
		}
	}

	public class ProcedureInfo_hurdurProc : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_hurdurProc instance = null;
		public static ProcedureInfo_hurdurProc Instance { get { if (instance==null) { instance = new ProcedureInfo_hurdurProc(); } return instance; } }

		private ProcedureInfo_hurdurProc()
					: base(
						"hurdurProc",
						null, "hurdurProc",
						true,
						new String[] { "in_0",  },
						new GRGEN_LIBGR.GrGenType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(GRGEN_MODEL.OwnPownHome)),  },
						new GRGEN_LIBGR.GrGenType[] {  }
					  )
		{
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.ProcedureInvocationParameterBindings paramBindings)
		{
			GRGEN_EXPR.ExternalProcedures.hurdurProc((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.OwnPownHome)paramBindings.Arguments[0]);
			return ReturnArray;
		}
	}


	//-----------------------------------------------------------

	public class ExternalAttributeEvaluation_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public ExternalAttributeEvaluation_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[6];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+6];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			functions = new GRGEN_LIBGR.FunctionInfo[0+11];
			procedures = new GRGEN_LIBGR.ProcedureInfo[0+8];
			packages = new string[0];
			rules[0] = Rule_init.Instance;
			rulesAndSubpatterns[0+0] = Rule_init.Instance;
			rules[1] = Rule_init2.Instance;
			rulesAndSubpatterns[0+1] = Rule_init2.Instance;
			rules[2] = Rule_r.Instance;
			rulesAndSubpatterns[0+2] = Rule_r.Instance;
			rules[3] = Rule_rp.Instance;
			rulesAndSubpatterns[0+3] = Rule_rp.Instance;
			rules[4] = Rule_testCopy.Instance;
			rulesAndSubpatterns[0+4] = Rule_testCopy.Instance;
			rules[5] = Rule_testComparison.Instance;
			rulesAndSubpatterns[0+5] = Rule_testComparison.Instance;
			functions[0] = FunctionInfo_foo.Instance;
			functions[1] = FunctionInfo_bar.Instance;
			functions[2] = FunctionInfo_isnull.Instance;
			functions[3] = FunctionInfo_bla.Instance;
			functions[4] = FunctionInfo_blo.Instance;
			functions[5] = FunctionInfo_har.Instance;
			functions[6] = FunctionInfo_hur.Instance;
			functions[7] = FunctionInfo_hurdur.Instance;
			functions[8] = FunctionInfo_own.Instance;
			functions[9] = FunctionInfo_ownPown.Instance;
			functions[10] = FunctionInfo_ownPownHome.Instance;
			procedures[0] = ProcedureInfo_fooProc.Instance;
			procedures[1] = ProcedureInfo_barProc.Instance;
			procedures[2] = ProcedureInfo_isnullProc.Instance;
			procedures[3] = ProcedureInfo_blaProc.Instance;
			procedures[4] = ProcedureInfo_bloProc.Instance;
			procedures[5] = ProcedureInfo_harProc.Instance;
			procedures[6] = ProcedureInfo_hurProc.Instance;
			procedures[7] = ProcedureInfo_hurdurProc.Instance;
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
    public interface IAction_init
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_init.IMatch_init match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches);
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
    
    public class Action_init : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_init
    {
        public Action_init() {
            _rulePattern = Rule_init.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_init.Match_init, Rule_init.IMatch_init>(this);
        }

        public Rule_init _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "init"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_init.Match_init, Rule_init.IMatch_init> matches;

        public static Action_init Instance { get { return instance; } set { instance = value; } }
        private static Action_init instance = new Action_init();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            Rule_init.Match_init match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_init.IMatch_init match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches)
        {
            foreach(Rule_init.IMatch_init match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_init.IMatch_init match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches;
            
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
            
            Modify(actionEnv, (Rule_init.IMatch_init)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init>)matches);
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
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_init2
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_init2.IMatch_init2 match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches);
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
    
    public class Action_init2 : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_init2
    {
        public Action_init2() {
            _rulePattern = Rule_init2.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_init2.Match_init2, Rule_init2.IMatch_init2>(this);
        }

        public Rule_init2 _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "init2"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_init2.Match_init2, Rule_init2.IMatch_init2> matches;

        public static Action_init2 Instance { get { return instance; } set { instance = value; } }
        private static Action_init2 instance = new Action_init2();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            Rule_init2.Match_init2 match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_init2.IMatch_init2 match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches)
        {
            foreach(Rule_init2.IMatch_init2 match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_init2.IMatch_init2 match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches;
            
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
            
            Modify(actionEnv, (Rule_init2.IMatch_init2)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2>)matches);
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
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_r
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_r.IMatch_r match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches);
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
    
    public class Action_r : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_r
    {
        public Action_r() {
            _rulePattern = Rule_r.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_r.Match_r, Rule_r.IMatch_r>(this);
        }

        public Rule_r _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "r"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_r.Match_r, Rule_r.IMatch_r> matches;

        public static Action_r Instance { get { return instance; } set { instance = value; } }
        private static Action_r instance = new Action_r();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup r_edge_e 
            int type_id_candidate_r_edge_e = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_r_edge_e = graph.edgesByTypeHeads[type_id_candidate_r_edge_e], candidate_r_edge_e = head_candidate_r_edge_e.lgspTypeNext; candidate_r_edge_e != head_candidate_r_edge_e; candidate_r_edge_e = candidate_r_edge_e.lgspTypeNext)
            {
                // Implicit Source r_node_n from r_edge_e 
                GRGEN_LGSP.LGSPNode candidate_r_node_n = candidate_r_edge_e.lgspSource;
                if(candidate_r_node_n.lgspType.TypeID!=1 && candidate_r_node_n.lgspType.TypeID!=2) {
                    continue;
                }
                if(candidate_r_edge_e.lgspSource != candidate_r_node_n) {
                    continue;
                }
                if(candidate_r_edge_e.lgspTarget != candidate_r_node_n) {
                    continue;
                }
                // Condition 
                if(!((GRGEN_EXPR.ExternalFunctions.hur(actionEnv, graph, (GRGEN_MODEL.OwnPown)GRGEN_EXPR.ExternalFunctions.har(actionEnv, graph, (GRGEN_MODEL.Own)((GRGEN_MODEL.IN)candidate_r_node_n).@ow, (GRGEN_MODEL.OwnPown)GRGEN_EXPR.ExternalFunctions.har(actionEnv, graph, (GRGEN_MODEL.Own)((GRGEN_MODEL.IN)candidate_r_node_n).@ow, (GRGEN_MODEL.OwnPown)((GRGEN_MODEL.IN)candidate_r_node_n).@op))) && GRGEN_EXPR.ExternalFunctions.hurdur(actionEnv, graph, (GRGEN_MODEL.OwnPownHome)((GRGEN_MODEL.IN)candidate_r_node_n).@oh)))) {
                    continue;
                }
                // Condition 
                if(!(GRGEN_EXPR.ExternalFunctions.bla(actionEnv, graph, (GRGEN_MODEL.IN)candidate_r_node_n, (GRGEN_MODEL.IE)candidate_r_edge_e))) {
                    continue;
                }
                // Condition 
                if(!(GRGEN_EXPR.ExternalFunctions.isnull(actionEnv, graph, GRGEN_EXPR.ExternalFunctions.bar(actionEnv, graph, null, ((GRGEN_MODEL.IN)candidate_r_node_n).@o)))) {
                    continue;
                }
                // Condition 
                if(!((GRGEN_EXPR.ExternalFunctions.foo(actionEnv, graph, 42, 3.141, GRGEN_MODEL.ENUM_Enu.@hurz, "S21-heiteitei") && GRGEN_EXPR.ExternalFunctions.foo(actionEnv, graph, ((GRGEN_MODEL.IN)candidate_r_node_n).@i, ((GRGEN_MODEL.IN)candidate_r_node_n).@d, ((GRGEN_MODEL.IN)candidate_r_node_n).@enu, ((GRGEN_MODEL.IN)candidate_r_node_n).@s)))) {
                    continue;
                }
                Rule_r.Match_r match = matches.GetNextUnfilledPosition();
                match._node_n = candidate_r_node_n;
                match._edge_e = candidate_r_edge_e;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_r_edge_e);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_r.IMatch_r match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches)
        {
            foreach(Rule_r.IMatch_r match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_r.IMatch_r match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r> matches;
            
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
            
            Modify(actionEnv, (Rule_r.IMatch_r)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_r.IMatch_r>)matches);
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
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_rp
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_rp.IMatch_rp match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> matches);
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
    
    public class Action_rp : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_rp
    {
        public Action_rp() {
            _rulePattern = Rule_rp.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_rp.Match_rp, Rule_rp.IMatch_rp>(this);
        }

        public Rule_rp _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "rp"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_rp.Match_rp, Rule_rp.IMatch_rp> matches;

        public static Action_rp Instance { get { return instance; } set { instance = value; } }
        private static Action_rp instance = new Action_rp();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup rp_edge_e 
            int type_id_candidate_rp_edge_e = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_rp_edge_e = graph.edgesByTypeHeads[type_id_candidate_rp_edge_e], candidate_rp_edge_e = head_candidate_rp_edge_e.lgspTypeNext; candidate_rp_edge_e != head_candidate_rp_edge_e; candidate_rp_edge_e = candidate_rp_edge_e.lgspTypeNext)
            {
                // Implicit Source rp_node_n from rp_edge_e 
                GRGEN_LGSP.LGSPNode candidate_rp_node_n = candidate_rp_edge_e.lgspSource;
                if(candidate_rp_node_n.lgspType.TypeID!=1 && candidate_rp_node_n.lgspType.TypeID!=2) {
                    continue;
                }
                if(candidate_rp_edge_e.lgspSource != candidate_rp_node_n) {
                    continue;
                }
                if(candidate_rp_edge_e.lgspTarget != candidate_rp_node_n) {
                    continue;
                }
                Rule_rp.Match_rp match = matches.GetNextUnfilledPosition();
                match._node_n = candidate_rp_node_n;
                match._edge_e = candidate_rp_edge_e;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_rp_edge_e);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_rp.IMatch_rp match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> matches)
        {
            foreach(Rule_rp.IMatch_rp match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_rp.IMatch_rp match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp> matches;
            
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
            
            Modify(actionEnv, (Rule_rp.IMatch_rp)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_rp.IMatch_rp>)matches);
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
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_testCopy
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testCopy.IMatch_testCopy match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> matches);
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
    
    public class Action_testCopy : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_testCopy
    {
        public Action_testCopy() {
            _rulePattern = Rule_testCopy.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_testCopy.Match_testCopy, Rule_testCopy.IMatch_testCopy>(this);
        }

        public Rule_testCopy _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "testCopy"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_testCopy.Match_testCopy, Rule_testCopy.IMatch_testCopy> matches;

        public static Action_testCopy Instance { get { return instance; } set { instance = value; } }
        private static Action_testCopy instance = new Action_testCopy();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup testCopy_node_n 
            foreach(GRGEN_LIBGR.NodeType type_candidate_testCopy_node_n in GRGEN_MODEL.NodeType_N.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_testCopy_node_n = type_candidate_testCopy_node_n.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_testCopy_node_n = graph.nodesByTypeHeads[type_id_candidate_testCopy_node_n], candidate_testCopy_node_n = head_candidate_testCopy_node_n.lgspTypeNext; candidate_testCopy_node_n != head_candidate_testCopy_node_n; candidate_testCopy_node_n = candidate_testCopy_node_n.lgspTypeNext)
                {
                    Rule_testCopy.Match_testCopy match = matches.GetNextUnfilledPosition();
                    match._node_n = candidate_testCopy_node_n;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(candidate_testCopy_node_n);
                        return matches;
                    }
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testCopy.IMatch_testCopy match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> matches)
        {
            foreach(Rule_testCopy.IMatch_testCopy match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_testCopy.IMatch_testCopy match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy> matches;
            
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
            
            Modify(actionEnv, (Rule_testCopy.IMatch_testCopy)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_testCopy.IMatch_testCopy>)matches);
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
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_testComparison
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testComparison.IMatch_testComparison match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> matches);
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
    
    public class Action_testComparison : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_testComparison
    {
        public Action_testComparison() {
            _rulePattern = Rule_testComparison.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_testComparison.Match_testComparison, Rule_testComparison.IMatch_testComparison>(this);
        }

        public Rule_testComparison _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "testComparison"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_testComparison.Match_testComparison, Rule_testComparison.IMatch_testComparison> matches;

        public static Action_testComparison Instance { get { return instance; } set { instance = value; } }
        private static Action_testComparison instance = new Action_testComparison();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup testComparison_node_m 
            foreach(GRGEN_LIBGR.NodeType type_candidate_testComparison_node_m in GRGEN_MODEL.NodeType_N.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_testComparison_node_m = type_candidate_testComparison_node_m.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_testComparison_node_m = graph.nodesByTypeHeads[type_id_candidate_testComparison_node_m], candidate_testComparison_node_m = head_candidate_testComparison_node_m.lgspTypeNext; candidate_testComparison_node_m != head_candidate_testComparison_node_m; candidate_testComparison_node_m = candidate_testComparison_node_m.lgspTypeNext)
                {
                    uint prev__candidate_testComparison_node_m;
                    prev__candidate_testComparison_node_m = candidate_testComparison_node_m.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_testComparison_node_m.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // Lookup testComparison_node_n 
                    foreach(GRGEN_LIBGR.NodeType type_candidate_testComparison_node_n in GRGEN_MODEL.NodeType_N.typeVar.SubOrSameTypes)
                    {
                        int type_id_candidate_testComparison_node_n = type_candidate_testComparison_node_n.TypeID;
                        for(GRGEN_LGSP.LGSPNode head_candidate_testComparison_node_n = graph.nodesByTypeHeads[type_id_candidate_testComparison_node_n], candidate_testComparison_node_n = head_candidate_testComparison_node_n.lgspTypeNext; candidate_testComparison_node_n != head_candidate_testComparison_node_n; candidate_testComparison_node_n = candidate_testComparison_node_n.lgspTypeNext)
                        {
                            if((candidate_testComparison_node_n.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                            {
                                continue;
                            }
                            // Condition 
                            if(!(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!((!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)))) {
                                continue;
                            }
                            // Condition 
                            if(!((GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)))) {
                                continue;
                            }
                            // Condition 
                            if(!(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!((!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)))) {
                                continue;
                            }
                            // Condition 
                            if(!((GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op)))) {
                                continue;
                            }
                            // Condition 
                            if(!(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@op, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@op))) {
                                continue;
                            }
                            // Condition 
                            if(!(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow))) {
                                continue;
                            }
                            // Condition 
                            if(!((!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow)&& !GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow)))) {
                                continue;
                            }
                            // Condition 
                            if(!((GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow)|| GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow)))) {
                                continue;
                            }
                            // Condition 
                            if(!(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsLower(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow))) {
                                continue;
                            }
                            // Condition 
                            if(!(!GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow))) {
                                continue;
                            }
                            // Condition 
                            if(!(GRGEN_MODEL.AttributeTypeObjectCopierComparer.IsEqual(((GRGEN_MODEL.IN)candidate_testComparison_node_n).@ow, ((GRGEN_MODEL.IN)candidate_testComparison_node_m).@ow))) {
                                continue;
                            }
                            Rule_testComparison.Match_testComparison match = matches.GetNextUnfilledPosition();
                            match._node_n = candidate_testComparison_node_n;
                            match._node_m = candidate_testComparison_node_m;
                            matches.PositionWasFilledFixIt();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                graph.MoveHeadAfter(candidate_testComparison_node_n);
                                graph.MoveHeadAfter(candidate_testComparison_node_m);
                                candidate_testComparison_node_m.lgspFlags = candidate_testComparison_node_m.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testComparison_node_m;
                                return matches;
                            }
                        }
                    }
                    candidate_testComparison_node_m.lgspFlags = candidate_testComparison_node_m.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_testComparison_node_m;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_testComparison.IMatch_testComparison match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> matches)
        {
            foreach(Rule_testComparison.IMatch_testComparison match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_testComparison.IMatch_testComparison match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison> matches;
            
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
            
            Modify(actionEnv, (Rule_testComparison.IMatch_testComparison)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_testComparison.IMatch_testComparison>)matches);
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
    public class ExternalAttributeEvaluationActions : GRGEN_LGSP.LGSPActions
    {
        public ExternalAttributeEvaluationActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public ExternalAttributeEvaluationActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            packages = new string[0];
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(Rule_init.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_init.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_init.Instance);
            actions.Add("init", (GRGEN_LGSP.LGSPAction) Action_init.Instance);
            @init = Action_init.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_init2.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_init2.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_init2.Instance);
            actions.Add("init2", (GRGEN_LGSP.LGSPAction) Action_init2.Instance);
            @init2 = Action_init2.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_r.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_r.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_r.Instance);
            actions.Add("r", (GRGEN_LGSP.LGSPAction) Action_r.Instance);
            @r = Action_r.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_rp.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_rp.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_rp.Instance);
            actions.Add("rp", (GRGEN_LGSP.LGSPAction) Action_rp.Instance);
            @rp = Action_rp.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_testCopy.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_testCopy.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_testCopy.Instance);
            actions.Add("testCopy", (GRGEN_LGSP.LGSPAction) Action_testCopy.Instance);
            @testCopy = Action_testCopy.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_testComparison.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_testComparison.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_testComparison.Instance);
            actions.Add("testComparison", (GRGEN_LGSP.LGSPAction) Action_testComparison.Instance);
            @testComparison = Action_testComparison.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_init.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_init2.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_r.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_rp.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_testCopy.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_testComparison.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_init.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_init2.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_r.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_rp.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_testCopy.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_testComparison.Instance.patternGraph);
            Rule_init.Instance.patternGraph.maxIsoSpace = 0;
            Rule_init2.Instance.patternGraph.maxIsoSpace = 0;
            Rule_r.Instance.patternGraph.maxIsoSpace = 0;
            Rule_rp.Instance.patternGraph.maxIsoSpace = 0;
            Rule_testCopy.Instance.patternGraph.maxIsoSpace = 0;
            Rule_testComparison.Instance.patternGraph.maxIsoSpace = 0;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_init.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_init2.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_r.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_rp.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_testCopy.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_testComparison.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
            namesToFunctionDefinitions.Add("foo", FunctionInfo_foo.Instance);
            namesToFunctionDefinitions.Add("bar", FunctionInfo_bar.Instance);
            namesToFunctionDefinitions.Add("isnull", FunctionInfo_isnull.Instance);
            namesToFunctionDefinitions.Add("bla", FunctionInfo_bla.Instance);
            namesToFunctionDefinitions.Add("blo", FunctionInfo_blo.Instance);
            namesToFunctionDefinitions.Add("har", FunctionInfo_har.Instance);
            namesToFunctionDefinitions.Add("hur", FunctionInfo_hur.Instance);
            namesToFunctionDefinitions.Add("hurdur", FunctionInfo_hurdur.Instance);
            namesToFunctionDefinitions.Add("own", FunctionInfo_own.Instance);
            namesToFunctionDefinitions.Add("ownPown", FunctionInfo_ownPown.Instance);
            namesToFunctionDefinitions.Add("ownPownHome", FunctionInfo_ownPownHome.Instance);
            namesToProcedureDefinitions.Add("fooProc", ProcedureInfo_fooProc.Instance);
            namesToProcedureDefinitions.Add("barProc", ProcedureInfo_barProc.Instance);
            namesToProcedureDefinitions.Add("isnullProc", ProcedureInfo_isnullProc.Instance);
            namesToProcedureDefinitions.Add("blaProc", ProcedureInfo_blaProc.Instance);
            namesToProcedureDefinitions.Add("bloProc", ProcedureInfo_bloProc.Instance);
            namesToProcedureDefinitions.Add("harProc", ProcedureInfo_harProc.Instance);
            namesToProcedureDefinitions.Add("hurProc", ProcedureInfo_hurProc.Instance);
            namesToProcedureDefinitions.Add("hurdurProc", ProcedureInfo_hurdurProc.Instance);
        }
        
        public IAction_init @init;
        public IAction_init2 @init2;
        public IAction_r @r;
        public IAction_rp @rp;
        public IAction_testCopy @testCopy;
        public IAction_testComparison @testComparison;
        
        
        public override string[] Packages { get { return packages; } }
        private string[] packages;
        
        public override string Name { get { return "ExternalAttributeEvaluationActions"; } }
        public override string StatisticsPath { get { return null; } }
        public override bool LazyNIC { get { return false; } }
        public override bool InlineIndependents { get { return true; } }
        public override bool Profile { get { return false; } }

        public override string ModelMD5Hash { get { return "c31b4a83d7adddb9205f28026a0414cd"; } }
    }
}