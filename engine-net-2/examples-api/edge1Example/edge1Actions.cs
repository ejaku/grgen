// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\edge1\edge1.grg" on Wed Sep 08 23:33:33 CEST 2010

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_edge1
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
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] init_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] init_minMatches = new int[0] ;
			int[] init_maxMatches = new int[0] ;
			pat_init = new GRGEN_LGSP.PatternGraph(
				"init",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				init_minMatches,
				init_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init_isNodeHomomorphicGlobal,
				init_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_init;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init curMatch = (Match_init)_curMatch;
			graph.SettingAddedNodeNames( init_addedNodeNames );
			GRGEN_MODEL.@Node node_x = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_y = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_z = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init_addedEdgeNames );
			GRGEN_MODEL.@UEdge edge__edge0 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_x, node_y);
			GRGEN_MODEL.@UEdge edge__edge1 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_y, node_z);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_y, node_y);
			return;
		}
		private static string[] init_addedNodeNames = new string[] { "x", "y", "z" };
		private static string[] init_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2" };

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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_init.instance.pat_init; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
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
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] init2_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init2_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] init2_minMatches = new int[0] ;
			int[] init2_maxMatches = new int[0] ;
			pat_init2 = new GRGEN_LGSP.PatternGraph(
				"init2",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				init2_minMatches,
				init2_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init2_isNodeHomomorphicGlobal,
				init2_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_init2;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init2 curMatch = (Match_init2)_curMatch;
			graph.SettingAddedNodeNames( init2_addedNodeNames );
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init2_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node0, node__node1);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node2, node__node1);
			return;
		}
		private static string[] init2_addedNodeNames = new string[] { "_node0", "_node1", "_node2" };
		private static string[] init2_addedEdgeNames = new string[] { "_edge0", "_edge1" };

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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_init2.instance.pat_init2; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_init3 : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init3 instance = null;
		public static Rule_init3 Instance { get { if (instance==null) { instance = new Rule_init3(); instance.initialize(); } return instance; } }

		public enum init3_NodeNums { };
		public enum init3_EdgeNums { };
		public enum init3_VariableNums { };
		public enum init3_SubNums { };
		public enum init3_AltNums { };
		public enum init3_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_init3;


		private Rule_init3()
		{
			name = "init3";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] init3_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init3_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] init3_minMatches = new int[0] ;
			int[] init3_maxMatches = new int[0] ;
			pat_init3 = new GRGEN_LGSP.PatternGraph(
				"init3",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				init3_minMatches,
				init3_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init3_isNodeHomomorphicGlobal,
				init3_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_init3;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init3 curMatch = (Match_init3)_curMatch;
			graph.SettingAddedNodeNames( init3_addedNodeNames );
			GRGEN_MODEL.@Node node_x = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_y = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_z = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init3_addedEdgeNames );
			GRGEN_MODEL.@UEdge edge__edge0 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_x, node_y);
			GRGEN_MODEL.@UEdge edge__edge1 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_y, node_z);
			GRGEN_MODEL.@UEdge edge__edge2 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_z, node_x);
			return;
		}
		private static string[] init3_addedNodeNames = new string[] { "x", "y", "z" };
		private static string[] init3_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2" };

		static Rule_init3() {
		}

		public interface IMatch_init3 : GRGEN_LIBGR.IMatch
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

		public class Match_init3 : GRGEN_LGSP.ListElement<Match_init3>, IMatch_init3
		{
			public enum init3_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init3_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init3_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init3_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init3_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init3_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum init3_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_init3.instance.pat_init3; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findUndirectedEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findUndirectedEdge instance = null;
		public static Rule_findUndirectedEdge Instance { get { if (instance==null) { instance = new Rule_findUndirectedEdge(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findUndirectedEdge_node_x_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findUndirectedEdge_node_y_AllowedTypes = null;
		public static bool[] findUndirectedEdge_node_x_IsAllowedType = null;
		public static bool[] findUndirectedEdge_node_y_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findUndirectedEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findUndirectedEdge_edge__edge0_IsAllowedType = null;
		public enum findUndirectedEdge_NodeNums { @x, @y, };
		public enum findUndirectedEdge_EdgeNums { @_edge0, };
		public enum findUndirectedEdge_VariableNums { };
		public enum findUndirectedEdge_SubNums { };
		public enum findUndirectedEdge_AltNums { };
		public enum findUndirectedEdge_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findUndirectedEdge;


		private Rule_findUndirectedEdge()
		{
			name = "findUndirectedEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findUndirectedEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findUndirectedEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] findUndirectedEdge_minMatches = new int[0] ;
			int[] findUndirectedEdge_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findUndirectedEdge_node_x = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findUndirectedEdge_node_x", "x", findUndirectedEdge_node_x_AllowedTypes, findUndirectedEdge_node_x_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findUndirectedEdge_node_y = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findUndirectedEdge_node_y", "y", findUndirectedEdge_node_y_AllowedTypes, findUndirectedEdge_node_y_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findUndirectedEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findUndirectedEdge_edge__edge0", "_edge0", findUndirectedEdge_edge__edge0_AllowedTypes, findUndirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1, false);
			pat_findUndirectedEdge = new GRGEN_LGSP.PatternGraph(
				"findUndirectedEdge",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findUndirectedEdge_node_x, findUndirectedEdge_node_y }, 
				new GRGEN_LGSP.PatternEdge[] { findUndirectedEdge_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findUndirectedEdge_minMatches,
				findUndirectedEdge_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				findUndirectedEdge_isNodeHomomorphicGlobal,
				findUndirectedEdge_isEdgeHomomorphicGlobal
			);
			pat_findUndirectedEdge.edgeToSourceNode.Add(findUndirectedEdge_edge__edge0, findUndirectedEdge_node_x);
			pat_findUndirectedEdge.edgeToTargetNode.Add(findUndirectedEdge_edge__edge0, findUndirectedEdge_node_y);

			findUndirectedEdge_node_x.PointOfDefinition = pat_findUndirectedEdge;
			findUndirectedEdge_node_y.PointOfDefinition = pat_findUndirectedEdge;
			findUndirectedEdge_edge__edge0.PointOfDefinition = pat_findUndirectedEdge;

			patternGraph = pat_findUndirectedEdge;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findUndirectedEdge curMatch = (Match_findUndirectedEdge)_curMatch;
			return;
		}

		static Rule_findUndirectedEdge() {
		}

		public interface IMatch_findUndirectedEdge : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			GRGEN_LIBGR.INode node_y { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findUndirectedEdge : GRGEN_LGSP.ListElement<Match_findUndirectedEdge>, IMatch_findUndirectedEdge
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LIBGR.INode node_y { get { return (GRGEN_LIBGR.INode)_node_y; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public GRGEN_LGSP.LGSPNode _node_y;
			public enum findUndirectedEdge_NodeNums { @x, @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findUndirectedEdge_NodeNums.@x: return _node_x;
				case (int)findUndirectedEdge_NodeNums.@y: return _node_y;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum findUndirectedEdge_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findUndirectedEdge_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum findUndirectedEdge_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findUndirectedEdge_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findUndirectedEdge_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findUndirectedEdge_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findUndirectedEdge_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findUndirectedEdge.instance.pat_findUndirectedEdge; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryEdge instance = null;
		public static Rule_findArbitraryEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryEdge(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findArbitraryEdge_node_x_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findArbitraryEdge_node_y_AllowedTypes = null;
		public static bool[] findArbitraryEdge_node_x_IsAllowedType = null;
		public static bool[] findArbitraryEdge_node_y_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findArbitraryEdge_edge__edge0_IsAllowedType = null;
		public enum findArbitraryEdge_NodeNums { @x, @y, };
		public enum findArbitraryEdge_EdgeNums { @_edge0, };
		public enum findArbitraryEdge_VariableNums { };
		public enum findArbitraryEdge_SubNums { };
		public enum findArbitraryEdge_AltNums { };
		public enum findArbitraryEdge_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findArbitraryEdge;


		private Rule_findArbitraryEdge()
		{
			name = "findArbitraryEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findArbitraryEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findArbitraryEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] findArbitraryEdge_minMatches = new int[0] ;
			int[] findArbitraryEdge_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findArbitraryEdge_node_x = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryEdge_node_x", "x", findArbitraryEdge_node_x_AllowedTypes, findArbitraryEdge_node_x_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findArbitraryEdge_node_y = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryEdge_node_y", "y", findArbitraryEdge_node_y_AllowedTypes, findArbitraryEdge_node_y_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findArbitraryEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@AEdge, "GRGEN_LIBGR.IEdge", "findArbitraryEdge_edge__edge0", "_edge0", findArbitraryEdge_edge__edge0_AllowedTypes, findArbitraryEdge_edge__edge0_IsAllowedType, 5.5F, -1, false);
			pat_findArbitraryEdge = new GRGEN_LGSP.PatternGraph(
				"findArbitraryEdge",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findArbitraryEdge_node_x, findArbitraryEdge_node_y }, 
				new GRGEN_LGSP.PatternEdge[] { findArbitraryEdge_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findArbitraryEdge_minMatches,
				findArbitraryEdge_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				findArbitraryEdge_isNodeHomomorphicGlobal,
				findArbitraryEdge_isEdgeHomomorphicGlobal
			);
			pat_findArbitraryEdge.edgeToSourceNode.Add(findArbitraryEdge_edge__edge0, findArbitraryEdge_node_x);
			pat_findArbitraryEdge.edgeToTargetNode.Add(findArbitraryEdge_edge__edge0, findArbitraryEdge_node_y);

			findArbitraryEdge_node_x.PointOfDefinition = pat_findArbitraryEdge;
			findArbitraryEdge_node_y.PointOfDefinition = pat_findArbitraryEdge;
			findArbitraryEdge_edge__edge0.PointOfDefinition = pat_findArbitraryEdge;

			patternGraph = pat_findArbitraryEdge;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryEdge curMatch = (Match_findArbitraryEdge)_curMatch;
			return;
		}

		static Rule_findArbitraryEdge() {
		}

		public interface IMatch_findArbitraryEdge : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			GRGEN_LIBGR.INode node_y { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findArbitraryEdge : GRGEN_LGSP.ListElement<Match_findArbitraryEdge>, IMatch_findArbitraryEdge
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LIBGR.INode node_y { get { return (GRGEN_LIBGR.INode)_node_y; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public GRGEN_LGSP.LGSPNode _node_y;
			public enum findArbitraryEdge_NodeNums { @x, @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryEdge_NodeNums.@x: return _node_x;
				case (int)findArbitraryEdge_NodeNums.@y: return _node_y;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum findArbitraryEdge_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryEdge_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum findArbitraryEdge_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryEdge_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryEdge_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryEdge_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryEdge_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findArbitraryEdge.instance.pat_findArbitraryEdge; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedEdge instance = null;
		public static Rule_findArbitraryDirectedEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedEdge(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedEdge_node_x_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedEdge_node_y_AllowedTypes = null;
		public static bool[] findArbitraryDirectedEdge_node_x_IsAllowedType = null;
		public static bool[] findArbitraryDirectedEdge_node_y_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryDirectedEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findArbitraryDirectedEdge_edge__edge0_IsAllowedType = null;
		public enum findArbitraryDirectedEdge_NodeNums { @x, @y, };
		public enum findArbitraryDirectedEdge_EdgeNums { @_edge0, };
		public enum findArbitraryDirectedEdge_VariableNums { };
		public enum findArbitraryDirectedEdge_SubNums { };
		public enum findArbitraryDirectedEdge_AltNums { };
		public enum findArbitraryDirectedEdge_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedEdge;


		private Rule_findArbitraryDirectedEdge()
		{
			name = "findArbitraryDirectedEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findArbitraryDirectedEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findArbitraryDirectedEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] findArbitraryDirectedEdge_minMatches = new int[0] ;
			int[] findArbitraryDirectedEdge_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findArbitraryDirectedEdge_node_x = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedEdge_node_x", "x", findArbitraryDirectedEdge_node_x_AllowedTypes, findArbitraryDirectedEdge_node_x_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findArbitraryDirectedEdge_node_y = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedEdge_node_y", "y", findArbitraryDirectedEdge_node_y_AllowedTypes, findArbitraryDirectedEdge_node_y_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedEdge_edge__edge0", "_edge0", findArbitraryDirectedEdge_edge__edge0_AllowedTypes, findArbitraryDirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1, false);
			pat_findArbitraryDirectedEdge = new GRGEN_LGSP.PatternGraph(
				"findArbitraryDirectedEdge",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findArbitraryDirectedEdge_node_x, findArbitraryDirectedEdge_node_y }, 
				new GRGEN_LGSP.PatternEdge[] { findArbitraryDirectedEdge_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findArbitraryDirectedEdge_minMatches,
				findArbitraryDirectedEdge_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				findArbitraryDirectedEdge_isNodeHomomorphicGlobal,
				findArbitraryDirectedEdge_isEdgeHomomorphicGlobal
			);
			pat_findArbitraryDirectedEdge.edgeToSourceNode.Add(findArbitraryDirectedEdge_edge__edge0, findArbitraryDirectedEdge_node_x);
			pat_findArbitraryDirectedEdge.edgeToTargetNode.Add(findArbitraryDirectedEdge_edge__edge0, findArbitraryDirectedEdge_node_y);

			findArbitraryDirectedEdge_node_x.PointOfDefinition = pat_findArbitraryDirectedEdge;
			findArbitraryDirectedEdge_node_y.PointOfDefinition = pat_findArbitraryDirectedEdge;
			findArbitraryDirectedEdge_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedEdge;

			patternGraph = pat_findArbitraryDirectedEdge;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedEdge curMatch = (Match_findArbitraryDirectedEdge)_curMatch;
			return;
		}

		static Rule_findArbitraryDirectedEdge() {
		}

		public interface IMatch_findArbitraryDirectedEdge : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			GRGEN_LIBGR.INode node_y { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findArbitraryDirectedEdge : GRGEN_LGSP.ListElement<Match_findArbitraryDirectedEdge>, IMatch_findArbitraryDirectedEdge
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LIBGR.INode node_y { get { return (GRGEN_LIBGR.INode)_node_y; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public GRGEN_LGSP.LGSPNode _node_y;
			public enum findArbitraryDirectedEdge_NodeNums { @x, @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedEdge_NodeNums.@x: return _node_x;
				case (int)findArbitraryDirectedEdge_NodeNums.@y: return _node_y;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum findArbitraryDirectedEdge_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedEdge_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedEdge_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedEdge_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedEdge_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedEdge_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedEdge_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findArbitraryDirectedEdge.instance.pat_findArbitraryDirectedEdge; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedReflexiveEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedReflexiveEdge instance = null;
		public static Rule_findArbitraryDirectedReflexiveEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedReflexiveEdge(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedReflexiveEdge_node_x_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdge_node_x_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryDirectedReflexiveEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdge_edge__edge0_IsAllowedType = null;
		public enum findArbitraryDirectedReflexiveEdge_NodeNums { @x, };
		public enum findArbitraryDirectedReflexiveEdge_EdgeNums { @_edge0, };
		public enum findArbitraryDirectedReflexiveEdge_VariableNums { };
		public enum findArbitraryDirectedReflexiveEdge_SubNums { };
		public enum findArbitraryDirectedReflexiveEdge_AltNums { };
		public enum findArbitraryDirectedReflexiveEdge_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedReflexiveEdge;


		private Rule_findArbitraryDirectedReflexiveEdge()
		{
			name = "findArbitraryDirectedReflexiveEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findArbitraryDirectedReflexiveEdge_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] findArbitraryDirectedReflexiveEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] findArbitraryDirectedReflexiveEdge_minMatches = new int[0] ;
			int[] findArbitraryDirectedReflexiveEdge_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findArbitraryDirectedReflexiveEdge_node_x = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedReflexiveEdge_node_x", "x", findArbitraryDirectedReflexiveEdge_node_x_AllowedTypes, findArbitraryDirectedReflexiveEdge_node_x_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedReflexiveEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedReflexiveEdge_edge__edge0", "_edge0", findArbitraryDirectedReflexiveEdge_edge__edge0_AllowedTypes, findArbitraryDirectedReflexiveEdge_edge__edge0_IsAllowedType, 5.5F, -1, false);
			pat_findArbitraryDirectedReflexiveEdge = new GRGEN_LGSP.PatternGraph(
				"findArbitraryDirectedReflexiveEdge",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findArbitraryDirectedReflexiveEdge_node_x }, 
				new GRGEN_LGSP.PatternEdge[] { findArbitraryDirectedReflexiveEdge_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findArbitraryDirectedReflexiveEdge_minMatches,
				findArbitraryDirectedReflexiveEdge_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				findArbitraryDirectedReflexiveEdge_isNodeHomomorphicGlobal,
				findArbitraryDirectedReflexiveEdge_isEdgeHomomorphicGlobal
			);
			pat_findArbitraryDirectedReflexiveEdge.edgeToSourceNode.Add(findArbitraryDirectedReflexiveEdge_edge__edge0, findArbitraryDirectedReflexiveEdge_node_x);
			pat_findArbitraryDirectedReflexiveEdge.edgeToTargetNode.Add(findArbitraryDirectedReflexiveEdge_edge__edge0, findArbitraryDirectedReflexiveEdge_node_x);

			findArbitraryDirectedReflexiveEdge_node_x.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdge;
			findArbitraryDirectedReflexiveEdge_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdge;

			patternGraph = pat_findArbitraryDirectedReflexiveEdge;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedReflexiveEdge curMatch = (Match_findArbitraryDirectedReflexiveEdge)_curMatch;
			return;
		}

		static Rule_findArbitraryDirectedReflexiveEdge() {
		}

		public interface IMatch_findArbitraryDirectedReflexiveEdge : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findArbitraryDirectedReflexiveEdge : GRGEN_LGSP.ListElement<Match_findArbitraryDirectedReflexiveEdge>, IMatch_findArbitraryDirectedReflexiveEdge
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public enum findArbitraryDirectedReflexiveEdge_NodeNums { @x, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedReflexiveEdge_NodeNums.@x: return _node_x;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum findArbitraryDirectedReflexiveEdge_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedReflexiveEdge_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdge_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdge_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdge_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdge_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdge_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findArbitraryDirectedReflexiveEdge.instance.pat_findArbitraryDirectedReflexiveEdge; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne instance = null;
		public static Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_IsAllowedType = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_IsAllowedType = null;
		public static bool[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_IsAllowedType = null;
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_NodeNums { @x, @y, };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_EdgeNums { @_edge0, @_edge1, };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_VariableNums { };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_SubNums { };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_AltNums { };
		public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;


		private Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne()
		{
			name = "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_minMatches = new int[0] ;
			int[] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x", "x", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y", "y", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0", "_edge0", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1", "_edge1", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_IsAllowedType, 5.5F, -1, false);
			pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne = new GRGEN_LGSP.PatternGraph(
				"findArbitraryDirectedReflexiveEdgeAfterUndirectedOne",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y }, 
				new GRGEN_LGSP.PatternEdge[] { findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_minMatches,
				findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_isNodeHomomorphicGlobal,
				findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_isEdgeHomomorphicGlobal
			);
			pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.edgeToSourceNode.Add(findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x);
			pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.edgeToTargetNode.Add(findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y);
			pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.edgeToSourceNode.Add(findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y);
			pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.edgeToTargetNode.Add(findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y);

			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
			findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.PointOfDefinition = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;

			patternGraph = pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne curMatch = (Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne)_curMatch;
			return;
		}

		static Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne() {
		}

		public interface IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			GRGEN_LIBGR.INode node_y { get; }
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

		public class Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : GRGEN_LGSP.ListElement<Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne>, IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LIBGR.INode node_y { get { return (GRGEN_LIBGR.INode)_node_y; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public GRGEN_LGSP.LGSPNode _node_y;
			public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_NodeNums { @x, @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_NodeNums.@x: return _node_x;
				case (int)findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_NodeNums.@y: return _node_y;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.instance.pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedTriple : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedTriple instance = null;
		public static Rule_findArbitraryDirectedTriple Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedTriple(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedTriple_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedTriple_node__node1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedTriple_node__node2_AllowedTypes = null;
		public static bool[] findArbitraryDirectedTriple_node__node0_IsAllowedType = null;
		public static bool[] findArbitraryDirectedTriple_node__node1_IsAllowedType = null;
		public static bool[] findArbitraryDirectedTriple_node__node2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryDirectedTriple_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryDirectedTriple_edge__edge1_AllowedTypes = null;
		public static bool[] findArbitraryDirectedTriple_edge__edge0_IsAllowedType = null;
		public static bool[] findArbitraryDirectedTriple_edge__edge1_IsAllowedType = null;
		public enum findArbitraryDirectedTriple_NodeNums { @_node0, @_node1, @_node2, };
		public enum findArbitraryDirectedTriple_EdgeNums { @_edge0, @_edge1, };
		public enum findArbitraryDirectedTriple_VariableNums { };
		public enum findArbitraryDirectedTriple_SubNums { };
		public enum findArbitraryDirectedTriple_AltNums { };
		public enum findArbitraryDirectedTriple_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedTriple;


		private Rule_findArbitraryDirectedTriple()
		{
			name = "findArbitraryDirectedTriple";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findArbitraryDirectedTriple_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findArbitraryDirectedTriple_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] findArbitraryDirectedTriple_minMatches = new int[0] ;
			int[] findArbitraryDirectedTriple_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findArbitraryDirectedTriple_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedTriple_node__node0", "_node0", findArbitraryDirectedTriple_node__node0_AllowedTypes, findArbitraryDirectedTriple_node__node0_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findArbitraryDirectedTriple_node__node1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedTriple_node__node1", "_node1", findArbitraryDirectedTriple_node__node1_AllowedTypes, findArbitraryDirectedTriple_node__node1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findArbitraryDirectedTriple_node__node2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedTriple_node__node2", "_node2", findArbitraryDirectedTriple_node__node2_AllowedTypes, findArbitraryDirectedTriple_node__node2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedTriple_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedTriple_edge__edge0", "_edge0", findArbitraryDirectedTriple_edge__edge0_AllowedTypes, findArbitraryDirectedTriple_edge__edge0_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedTriple_edge__edge1 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedTriple_edge__edge1", "_edge1", findArbitraryDirectedTriple_edge__edge1_AllowedTypes, findArbitraryDirectedTriple_edge__edge1_IsAllowedType, 5.5F, -1, false);
			pat_findArbitraryDirectedTriple = new GRGEN_LGSP.PatternGraph(
				"findArbitraryDirectedTriple",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findArbitraryDirectedTriple_node__node0, findArbitraryDirectedTriple_node__node1, findArbitraryDirectedTriple_node__node2 }, 
				new GRGEN_LGSP.PatternEdge[] { findArbitraryDirectedTriple_edge__edge0, findArbitraryDirectedTriple_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findArbitraryDirectedTriple_minMatches,
				findArbitraryDirectedTriple_maxMatches,
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
				findArbitraryDirectedTriple_isNodeHomomorphicGlobal,
				findArbitraryDirectedTriple_isEdgeHomomorphicGlobal
			);
			pat_findArbitraryDirectedTriple.edgeToSourceNode.Add(findArbitraryDirectedTriple_edge__edge0, findArbitraryDirectedTriple_node__node0);
			pat_findArbitraryDirectedTriple.edgeToTargetNode.Add(findArbitraryDirectedTriple_edge__edge0, findArbitraryDirectedTriple_node__node1);
			pat_findArbitraryDirectedTriple.edgeToSourceNode.Add(findArbitraryDirectedTriple_edge__edge1, findArbitraryDirectedTriple_node__node1);
			pat_findArbitraryDirectedTriple.edgeToTargetNode.Add(findArbitraryDirectedTriple_edge__edge1, findArbitraryDirectedTriple_node__node2);

			findArbitraryDirectedTriple_node__node0.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_node__node1.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_node__node2.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_edge__edge0.PointOfDefinition = pat_findArbitraryDirectedTriple;
			findArbitraryDirectedTriple_edge__edge1.PointOfDefinition = pat_findArbitraryDirectedTriple;

			patternGraph = pat_findArbitraryDirectedTriple;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedTriple curMatch = (Match_findArbitraryDirectedTriple)_curMatch;
			return;
		}

		static Rule_findArbitraryDirectedTriple() {
		}

		public interface IMatch_findArbitraryDirectedTriple : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node__node1 { get; }
			GRGEN_LIBGR.INode node__node2 { get; }
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

		public class Match_findArbitraryDirectedTriple : GRGEN_LGSP.ListElement<Match_findArbitraryDirectedTriple>, IMatch_findArbitraryDirectedTriple
		{
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node__node1 { get { return (GRGEN_LIBGR.INode)_node__node1; } }
			public GRGEN_LIBGR.INode node__node2 { get { return (GRGEN_LIBGR.INode)_node__node2; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public GRGEN_LGSP.LGSPNode _node__node2;
			public enum findArbitraryDirectedTriple_NodeNums { @_node0, @_node1, @_node2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedTriple_NodeNums.@_node0: return _node__node0;
				case (int)findArbitraryDirectedTriple_NodeNums.@_node1: return _node__node1;
				case (int)findArbitraryDirectedTriple_NodeNums.@_node2: return _node__node2;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findArbitraryDirectedTriple_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findArbitraryDirectedTriple_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findArbitraryDirectedTriple_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedTriple_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedTriple_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedTriple_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedTriple_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findArbitraryDirectedTriple_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findArbitraryDirectedTriple.instance.pat_findArbitraryDirectedTriple; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findDirectedEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findDirectedEdge instance = null;
		public static Rule_findDirectedEdge Instance { get { if (instance==null) { instance = new Rule_findDirectedEdge(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findDirectedEdge_node_x_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findDirectedEdge_node_y_AllowedTypes = null;
		public static bool[] findDirectedEdge_node_x_IsAllowedType = null;
		public static bool[] findDirectedEdge_node_y_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findDirectedEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findDirectedEdge_edge__edge0_IsAllowedType = null;
		public enum findDirectedEdge_NodeNums { @x, @y, };
		public enum findDirectedEdge_EdgeNums { @_edge0, };
		public enum findDirectedEdge_VariableNums { };
		public enum findDirectedEdge_SubNums { };
		public enum findDirectedEdge_AltNums { };
		public enum findDirectedEdge_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findDirectedEdge;


		private Rule_findDirectedEdge()
		{
			name = "findDirectedEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findDirectedEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findDirectedEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] findDirectedEdge_minMatches = new int[0] ;
			int[] findDirectedEdge_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findDirectedEdge_node_x = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findDirectedEdge_node_x", "x", findDirectedEdge_node_x_AllowedTypes, findDirectedEdge_node_x_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findDirectedEdge_node_y = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findDirectedEdge_node_y", "y", findDirectedEdge_node_y_AllowedTypes, findDirectedEdge_node_y_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findDirectedEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findDirectedEdge_edge__edge0", "_edge0", findDirectedEdge_edge__edge0_AllowedTypes, findDirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1, false);
			pat_findDirectedEdge = new GRGEN_LGSP.PatternGraph(
				"findDirectedEdge",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findDirectedEdge_node_x, findDirectedEdge_node_y }, 
				new GRGEN_LGSP.PatternEdge[] { findDirectedEdge_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findDirectedEdge_minMatches,
				findDirectedEdge_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				findDirectedEdge_isNodeHomomorphicGlobal,
				findDirectedEdge_isEdgeHomomorphicGlobal
			);
			pat_findDirectedEdge.edgeToSourceNode.Add(findDirectedEdge_edge__edge0, findDirectedEdge_node_x);
			pat_findDirectedEdge.edgeToTargetNode.Add(findDirectedEdge_edge__edge0, findDirectedEdge_node_y);

			findDirectedEdge_node_x.PointOfDefinition = pat_findDirectedEdge;
			findDirectedEdge_node_y.PointOfDefinition = pat_findDirectedEdge;
			findDirectedEdge_edge__edge0.PointOfDefinition = pat_findDirectedEdge;

			patternGraph = pat_findDirectedEdge;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findDirectedEdge curMatch = (Match_findDirectedEdge)_curMatch;
			return;
		}

		static Rule_findDirectedEdge() {
		}

		public interface IMatch_findDirectedEdge : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			GRGEN_LIBGR.INode node_y { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findDirectedEdge : GRGEN_LGSP.ListElement<Match_findDirectedEdge>, IMatch_findDirectedEdge
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LIBGR.INode node_y { get { return (GRGEN_LIBGR.INode)_node_y; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public GRGEN_LGSP.LGSPNode _node_y;
			public enum findDirectedEdge_NodeNums { @x, @y, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findDirectedEdge_NodeNums.@x: return _node_x;
				case (int)findDirectedEdge_NodeNums.@y: return _node_y;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum findDirectedEdge_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findDirectedEdge_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum findDirectedEdge_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findDirectedEdge_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findDirectedEdge_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findDirectedEdge_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findDirectedEdge_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findDirectedEdge.instance.pat_findDirectedEdge; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findTripleCircle : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findTripleCircle instance = null;
		public static Rule_findTripleCircle Instance { get { if (instance==null) { instance = new Rule_findTripleCircle(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] findTripleCircle_node_x_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findTripleCircle_node_y_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findTripleCircle_node_z_AllowedTypes = null;
		public static bool[] findTripleCircle_node_x_IsAllowedType = null;
		public static bool[] findTripleCircle_node_y_IsAllowedType = null;
		public static bool[] findTripleCircle_node_z_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findTripleCircle_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findTripleCircle_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findTripleCircle_edge__edge2_AllowedTypes = null;
		public static bool[] findTripleCircle_edge__edge0_IsAllowedType = null;
		public static bool[] findTripleCircle_edge__edge1_IsAllowedType = null;
		public static bool[] findTripleCircle_edge__edge2_IsAllowedType = null;
		public enum findTripleCircle_NodeNums { @x, @y, @z, };
		public enum findTripleCircle_EdgeNums { @_edge0, @_edge1, @_edge2, };
		public enum findTripleCircle_VariableNums { };
		public enum findTripleCircle_SubNums { };
		public enum findTripleCircle_AltNums { };
		public enum findTripleCircle_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_findTripleCircle;


		private Rule_findTripleCircle()
		{
			name = "findTripleCircle";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findTripleCircle_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findTripleCircle_isEdgeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			int[] findTripleCircle_minMatches = new int[0] ;
			int[] findTripleCircle_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode findTripleCircle_node_x = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findTripleCircle_node_x", "x", findTripleCircle_node_x_AllowedTypes, findTripleCircle_node_x_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findTripleCircle_node_y = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findTripleCircle_node_y", "y", findTripleCircle_node_y_AllowedTypes, findTripleCircle_node_y_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode findTripleCircle_node_z = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "findTripleCircle_node_z", "z", findTripleCircle_node_z_AllowedTypes, findTripleCircle_node_z_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findTripleCircle_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findTripleCircle_edge__edge0", "_edge0", findTripleCircle_edge__edge0_AllowedTypes, findTripleCircle_edge__edge0_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findTripleCircle_edge__edge1 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findTripleCircle_edge__edge1", "_edge1", findTripleCircle_edge__edge1_AllowedTypes, findTripleCircle_edge__edge1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge findTripleCircle_edge__edge2 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findTripleCircle_edge__edge2", "_edge2", findTripleCircle_edge__edge2_AllowedTypes, findTripleCircle_edge__edge2_IsAllowedType, 5.5F, -1, false);
			pat_findTripleCircle = new GRGEN_LGSP.PatternGraph(
				"findTripleCircle",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findTripleCircle_node_x, findTripleCircle_node_y, findTripleCircle_node_z }, 
				new GRGEN_LGSP.PatternEdge[] { findTripleCircle_edge__edge0, findTripleCircle_edge__edge1, findTripleCircle_edge__edge2 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				findTripleCircle_minMatches,
				findTripleCircle_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				findTripleCircle_isNodeHomomorphicGlobal,
				findTripleCircle_isEdgeHomomorphicGlobal
			);
			pat_findTripleCircle.edgeToSourceNode.Add(findTripleCircle_edge__edge0, findTripleCircle_node_x);
			pat_findTripleCircle.edgeToTargetNode.Add(findTripleCircle_edge__edge0, findTripleCircle_node_y);
			pat_findTripleCircle.edgeToSourceNode.Add(findTripleCircle_edge__edge1, findTripleCircle_node_y);
			pat_findTripleCircle.edgeToTargetNode.Add(findTripleCircle_edge__edge1, findTripleCircle_node_z);
			pat_findTripleCircle.edgeToSourceNode.Add(findTripleCircle_edge__edge2, findTripleCircle_node_z);
			pat_findTripleCircle.edgeToTargetNode.Add(findTripleCircle_edge__edge2, findTripleCircle_node_x);

			findTripleCircle_node_x.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_node_y.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_node_z.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_edge__edge0.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_edge__edge1.PointOfDefinition = pat_findTripleCircle;
			findTripleCircle_edge__edge2.PointOfDefinition = pat_findTripleCircle;

			patternGraph = pat_findTripleCircle;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findTripleCircle curMatch = (Match_findTripleCircle)_curMatch;
			return;
		}

		static Rule_findTripleCircle() {
		}

		public interface IMatch_findTripleCircle : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_x { get; }
			GRGEN_LIBGR.INode node_y { get; }
			GRGEN_LIBGR.INode node_z { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_findTripleCircle : GRGEN_LGSP.ListElement<Match_findTripleCircle>, IMatch_findTripleCircle
		{
			public GRGEN_LIBGR.INode node_x { get { return (GRGEN_LIBGR.INode)_node_x; } }
			public GRGEN_LIBGR.INode node_y { get { return (GRGEN_LIBGR.INode)_node_y; } }
			public GRGEN_LIBGR.INode node_z { get { return (GRGEN_LIBGR.INode)_node_z; } }
			public GRGEN_LGSP.LGSPNode _node_x;
			public GRGEN_LGSP.LGSPNode _node_y;
			public GRGEN_LGSP.LGSPNode _node_z;
			public enum findTripleCircle_NodeNums { @x, @y, @z, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findTripleCircle_NodeNums.@x: return _node_x;
				case (int)findTripleCircle_NodeNums.@y: return _node_y;
				case (int)findTripleCircle_NodeNums.@z: return _node_z;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public enum findTripleCircle_EdgeNums { @_edge0, @_edge1, @_edge2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 3;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findTripleCircle_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findTripleCircle_EdgeNums.@_edge1: return _edge__edge1;
				case (int)findTripleCircle_EdgeNums.@_edge2: return _edge__edge2;
				default: return null;
				}
			}
			
			public enum findTripleCircle_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findTripleCircle_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findTripleCircle_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findTripleCircle_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findTripleCircle_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findTripleCircle.instance.pat_findTripleCircle; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class edge1_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public edge1_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[11];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+11];
			rules[0] = Rule_init.Instance;
			rulesAndSubpatterns[0+0] = Rule_init.Instance;
			rules[1] = Rule_init2.Instance;
			rulesAndSubpatterns[0+1] = Rule_init2.Instance;
			rules[2] = Rule_init3.Instance;
			rulesAndSubpatterns[0+2] = Rule_init3.Instance;
			rules[3] = Rule_findUndirectedEdge.Instance;
			rulesAndSubpatterns[0+3] = Rule_findUndirectedEdge.Instance;
			rules[4] = Rule_findArbitraryEdge.Instance;
			rulesAndSubpatterns[0+4] = Rule_findArbitraryEdge.Instance;
			rules[5] = Rule_findArbitraryDirectedEdge.Instance;
			rulesAndSubpatterns[0+5] = Rule_findArbitraryDirectedEdge.Instance;
			rules[6] = Rule_findArbitraryDirectedReflexiveEdge.Instance;
			rulesAndSubpatterns[0+6] = Rule_findArbitraryDirectedReflexiveEdge.Instance;
			rules[7] = Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance;
			rulesAndSubpatterns[0+7] = Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance;
			rules[8] = Rule_findArbitraryDirectedTriple.Instance;
			rulesAndSubpatterns[0+8] = Rule_findArbitraryDirectedTriple.Instance;
			rules[9] = Rule_findDirectedEdge.Instance;
			rulesAndSubpatterns[0+9] = Rule_findDirectedEdge.Instance;
			rules[10] = Rule_findTripleCircle.Instance;
			rulesAndSubpatterns[0+10] = Rule_findTripleCircle.Instance;
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
    public interface IAction_init
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_init.IMatch_init match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches);
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

        public static Action_init Instance { get { return instance; } }
        private static Action_init instance = new Action_init();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_init.Match_init match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_init.IMatch_init match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches)
        {
            foreach(Rule_init.IMatch_init match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_init.IMatch_init match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init> matches;
            
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
            
            Modify(graph, (Rule_init.IMatch_init)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_init.IMatch_init>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_init2
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_init2.IMatch_init2 match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches);
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

        public static Action_init2 Instance { get { return instance; } }
        private static Action_init2 instance = new Action_init2();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_init2.Match_init2 match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_init2.IMatch_init2 match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches)
        {
            foreach(Rule_init2.IMatch_init2 match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_init2.IMatch_init2 match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2> matches;
            
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
            
            Modify(graph, (Rule_init2.IMatch_init2)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_init2.IMatch_init2>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_init3
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_init3.IMatch_init3 match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> matches);
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
    
    public class Action_init3 : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_init3
    {
        public Action_init3() {
            _rulePattern = Rule_init3.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_init3.Match_init3, Rule_init3.IMatch_init3>(this);
        }

        public Rule_init3 _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "init3"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_init3.Match_init3, Rule_init3.IMatch_init3> matches;

        public static Action_init3 Instance { get { return instance; } }
        private static Action_init3 instance = new Action_init3();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_init3.Match_init3 match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_init3.IMatch_init3 match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> matches)
        {
            foreach(Rule_init3.IMatch_init3 match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_init3.IMatch_init3 match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3> matches;
            
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
            
            Modify(graph, (Rule_init3.IMatch_init3)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_init3.IMatch_init3>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findUndirectedEdge
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findUndirectedEdge.IMatch_findUndirectedEdge match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches);
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
    
    public class Action_findUndirectedEdge : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findUndirectedEdge
    {
        public Action_findUndirectedEdge() {
            _rulePattern = Rule_findUndirectedEdge.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findUndirectedEdge.Match_findUndirectedEdge, Rule_findUndirectedEdge.IMatch_findUndirectedEdge>(this);
        }

        public Rule_findUndirectedEdge _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findUndirectedEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findUndirectedEdge.Match_findUndirectedEdge, Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches;

        public static Action_findUndirectedEdge Instance { get { return instance; } }
        private static Action_findUndirectedEdge instance = new Action_findUndirectedEdge();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findUndirectedEdge_edge__edge0 
            int type_id_candidate_findUndirectedEdge_edge__edge0 = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findUndirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findUndirectedEdge_edge__edge0], candidate_findUndirectedEdge_edge__edge0 = head_candidate_findUndirectedEdge_edge__edge0.lgspTypeNext; candidate_findUndirectedEdge_edge__edge0 != head_candidate_findUndirectedEdge_edge__edge0; candidate_findUndirectedEdge_edge__edge0 = candidate_findUndirectedEdge_edge__edge0.lgspTypeNext)
            {
                // both directions of findUndirectedEdge_edge__edge0
                for(int directionRunCounterOf_findUndirectedEdge_edge__edge0 = 0; directionRunCounterOf_findUndirectedEdge_edge__edge0 < 2; ++directionRunCounterOf_findUndirectedEdge_edge__edge0)
                {
                    // Implicit SourceOrTarget findUndirectedEdge_node_y from findUndirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findUndirectedEdge_node_y = directionRunCounterOf_findUndirectedEdge_edge__edge0==0 ? candidate_findUndirectedEdge_edge__edge0.lgspSource : candidate_findUndirectedEdge_edge__edge0.lgspTarget;
                    uint prev__candidate_findUndirectedEdge_node_y;
                    prev__candidate_findUndirectedEdge_node_y = candidate_findUndirectedEdge_node_y.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findUndirectedEdge_node_y.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findUndirectedEdge_node_x from findUndirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findUndirectedEdge_node_x = candidate_findUndirectedEdge_node_y==candidate_findUndirectedEdge_edge__edge0.lgspSource ? candidate_findUndirectedEdge_edge__edge0.lgspTarget : candidate_findUndirectedEdge_edge__edge0.lgspSource;
                    if((candidate_findUndirectedEdge_node_x.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findUndirectedEdge_node_y.lgspFlags = candidate_findUndirectedEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findUndirectedEdge_node_y;
                        continue;
                    }
                    Rule_findUndirectedEdge.Match_findUndirectedEdge match = matches.GetNextUnfilledPosition();
                    match._node_x = candidate_findUndirectedEdge_node_x;
                    match._node_y = candidate_findUndirectedEdge_node_y;
                    match._edge__edge0 = candidate_findUndirectedEdge_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(candidate_findUndirectedEdge_edge__edge0);
                        candidate_findUndirectedEdge_node_y.lgspFlags = candidate_findUndirectedEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findUndirectedEdge_node_y;
                        return matches;
                    }
                    candidate_findUndirectedEdge_node_y.lgspFlags = candidate_findUndirectedEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findUndirectedEdge_node_y;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findUndirectedEdge.IMatch_findUndirectedEdge match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches)
        {
            foreach(Rule_findUndirectedEdge.IMatch_findUndirectedEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findUndirectedEdge.IMatch_findUndirectedEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge> matches;
            
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
            
            Modify(graph, (Rule_findUndirectedEdge.IMatch_findUndirectedEdge)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findUndirectedEdge.IMatch_findUndirectedEdge>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findArbitraryEdge
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryEdge.IMatch_findArbitraryEdge match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches);
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
    
    public class Action_findArbitraryEdge : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findArbitraryEdge
    {
        public Action_findArbitraryEdge() {
            _rulePattern = Rule_findArbitraryEdge.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryEdge.Match_findArbitraryEdge, Rule_findArbitraryEdge.IMatch_findArbitraryEdge>(this);
        }

        public Rule_findArbitraryEdge _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findArbitraryEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryEdge.Match_findArbitraryEdge, Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches;

        public static Action_findArbitraryEdge Instance { get { return instance; } }
        private static Action_findArbitraryEdge instance = new Action_findArbitraryEdge();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryEdge_edge__edge0 
            foreach(GRGEN_LIBGR.EdgeType type_candidate_findArbitraryEdge_edge__edge0 in GRGEN_MODEL.EdgeType_AEdge.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_findArbitraryEdge_edge__edge0 = type_candidate_findArbitraryEdge_edge__edge0.TypeID;
                for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryEdge_edge__edge0], candidate_findArbitraryEdge_edge__edge0 = head_candidate_findArbitraryEdge_edge__edge0.lgspTypeNext; candidate_findArbitraryEdge_edge__edge0 != head_candidate_findArbitraryEdge_edge__edge0; candidate_findArbitraryEdge_edge__edge0 = candidate_findArbitraryEdge_edge__edge0.lgspTypeNext)
                {
                    // both directions of findArbitraryEdge_edge__edge0
                    for(int directionRunCounterOf_findArbitraryEdge_edge__edge0 = 0; directionRunCounterOf_findArbitraryEdge_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryEdge_edge__edge0)
                    {
                        // Implicit SourceOrTarget findArbitraryEdge_node_y from findArbitraryEdge_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_findArbitraryEdge_node_y = directionRunCounterOf_findArbitraryEdge_edge__edge0==0 ? candidate_findArbitraryEdge_edge__edge0.lgspSource : candidate_findArbitraryEdge_edge__edge0.lgspTarget;
                        uint prev__candidate_findArbitraryEdge_node_y;
                        prev__candidate_findArbitraryEdge_node_y = candidate_findArbitraryEdge_node_y.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_findArbitraryEdge_node_y.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        // Implicit TheOther findArbitraryEdge_node_x from findArbitraryEdge_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_findArbitraryEdge_node_x = candidate_findArbitraryEdge_node_y==candidate_findArbitraryEdge_edge__edge0.lgspSource ? candidate_findArbitraryEdge_edge__edge0.lgspTarget : candidate_findArbitraryEdge_edge__edge0.lgspSource;
                        if((candidate_findArbitraryEdge_node_x.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            candidate_findArbitraryEdge_node_y.lgspFlags = candidate_findArbitraryEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryEdge_node_y;
                            continue;
                        }
                        Rule_findArbitraryEdge.Match_findArbitraryEdge match = matches.GetNextUnfilledPosition();
                        match._node_x = candidate_findArbitraryEdge_node_x;
                        match._node_y = candidate_findArbitraryEdge_node_y;
                        match._edge__edge0 = candidate_findArbitraryEdge_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            graph.MoveHeadAfter(candidate_findArbitraryEdge_edge__edge0);
                            candidate_findArbitraryEdge_node_y.lgspFlags = candidate_findArbitraryEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryEdge_node_y;
                            return matches;
                        }
                        candidate_findArbitraryEdge_node_y.lgspFlags = candidate_findArbitraryEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryEdge_node_y;
                    }
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryEdge.IMatch_findArbitraryEdge match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches)
        {
            foreach(Rule_findArbitraryEdge.IMatch_findArbitraryEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findArbitraryEdge.IMatch_findArbitraryEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge> matches;
            
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
            
            Modify(graph, (Rule_findArbitraryEdge.IMatch_findArbitraryEdge)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryEdge.IMatch_findArbitraryEdge>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findArbitraryDirectedEdge
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches);
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
    
    public class Action_findArbitraryDirectedEdge : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findArbitraryDirectedEdge
    {
        public Action_findArbitraryDirectedEdge() {
            _rulePattern = Rule_findArbitraryDirectedEdge.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedEdge.Match_findArbitraryDirectedEdge, Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge>(this);
        }

        public Rule_findArbitraryDirectedEdge _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findArbitraryDirectedEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedEdge.Match_findArbitraryDirectedEdge, Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches;

        public static Action_findArbitraryDirectedEdge Instance { get { return instance; } }
        private static Action_findArbitraryDirectedEdge instance = new Action_findArbitraryDirectedEdge();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedEdge_edge__edge0 
            int type_id_candidate_findArbitraryDirectedEdge_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedEdge_edge__edge0], candidate_findArbitraryDirectedEdge_edge__edge0 = head_candidate_findArbitraryDirectedEdge_edge__edge0.lgspTypeNext; candidate_findArbitraryDirectedEdge_edge__edge0 != head_candidate_findArbitraryDirectedEdge_edge__edge0; candidate_findArbitraryDirectedEdge_edge__edge0 = candidate_findArbitraryDirectedEdge_edge__edge0.lgspTypeNext)
            {
                // both directions of findArbitraryDirectedEdge_edge__edge0
                for(int directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0 = 0; directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0)
                {
                    // Implicit SourceOrTarget findArbitraryDirectedEdge_node_y from findArbitraryDirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedEdge_node_y = directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0==0 ? candidate_findArbitraryDirectedEdge_edge__edge0.lgspSource : candidate_findArbitraryDirectedEdge_edge__edge0.lgspTarget;
                    uint prev__candidate_findArbitraryDirectedEdge_node_y;
                    prev__candidate_findArbitraryDirectedEdge_node_y = candidate_findArbitraryDirectedEdge_node_y.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedEdge_node_y.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findArbitraryDirectedEdge_node_x from findArbitraryDirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedEdge_node_x = candidate_findArbitraryDirectedEdge_node_y==candidate_findArbitraryDirectedEdge_edge__edge0.lgspSource ? candidate_findArbitraryDirectedEdge_edge__edge0.lgspTarget : candidate_findArbitraryDirectedEdge_edge__edge0.lgspSource;
                    if((candidate_findArbitraryDirectedEdge_node_x.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findArbitraryDirectedEdge_node_y.lgspFlags = candidate_findArbitraryDirectedEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedEdge_node_y;
                        continue;
                    }
                    Rule_findArbitraryDirectedEdge.Match_findArbitraryDirectedEdge match = matches.GetNextUnfilledPosition();
                    match._node_x = candidate_findArbitraryDirectedEdge_node_x;
                    match._node_y = candidate_findArbitraryDirectedEdge_node_y;
                    match._edge__edge0 = candidate_findArbitraryDirectedEdge_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(candidate_findArbitraryDirectedEdge_edge__edge0);
                        candidate_findArbitraryDirectedEdge_node_y.lgspFlags = candidate_findArbitraryDirectedEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedEdge_node_y;
                        return matches;
                    }
                    candidate_findArbitraryDirectedEdge_node_y.lgspFlags = candidate_findArbitraryDirectedEdge_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedEdge_node_y;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches)
        {
            foreach(Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge> matches;
            
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
            
            Modify(graph, (Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedEdge.IMatch_findArbitraryDirectedEdge>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findArbitraryDirectedReflexiveEdge
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches);
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
    
    public class Action_findArbitraryDirectedReflexiveEdge : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findArbitraryDirectedReflexiveEdge
    {
        public Action_findArbitraryDirectedReflexiveEdge() {
            _rulePattern = Rule_findArbitraryDirectedReflexiveEdge.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdge.Match_findArbitraryDirectedReflexiveEdge, Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge>(this);
        }

        public Rule_findArbitraryDirectedReflexiveEdge _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findArbitraryDirectedReflexiveEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdge.Match_findArbitraryDirectedReflexiveEdge, Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches;

        public static Action_findArbitraryDirectedReflexiveEdge Instance { get { return instance; } }
        private static Action_findArbitraryDirectedReflexiveEdge instance = new Action_findArbitraryDirectedReflexiveEdge();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedReflexiveEdge_edge__edge0 
            int type_id_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0], candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.lgspTypeNext; candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 != head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0; candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.lgspTypeNext)
            {
                // Implicit Source findArbitraryDirectedReflexiveEdge_node_x from findArbitraryDirectedReflexiveEdge_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedReflexiveEdge_node_x = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.lgspSource;
                if(candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.lgspSource != candidate_findArbitraryDirectedReflexiveEdge_node_x) {
                    continue;
                }
                if(candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.lgspTarget != candidate_findArbitraryDirectedReflexiveEdge_node_x) {
                    continue;
                }
                Rule_findArbitraryDirectedReflexiveEdge.Match_findArbitraryDirectedReflexiveEdge match = matches.GetNextUnfilledPosition();
                match._node_x = candidate_findArbitraryDirectedReflexiveEdge_node_x;
                match._edge__edge0 = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_findArbitraryDirectedReflexiveEdge_edge__edge0);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches)
        {
            foreach(Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge> matches;
            
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
            
            Modify(graph, (Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdge.IMatch_findArbitraryDirectedReflexiveEdge>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches);
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
    
    public class Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne
    {
        public Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne() {
            _rulePattern = Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne, Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne>(this);
        }

        public Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne, Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches;

        public static Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne Instance { get { return instance; } }
        private static Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne instance = new Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
            int type_id_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0], candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.lgspTypeNext; candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 != head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0; candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.lgspTypeNext)
            {
                // both directions of findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0
                for(int directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = 0; directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0)
                {
                    // Implicit SourceOrTarget findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0==0 ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.lgspSource : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.lgspTarget;
                    uint prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                    prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y==candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.lgspSource ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.lgspTarget : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.lgspSource;
                    if((candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                        continue;
                    }
                    // Extend Incoming findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y 
                    GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspInhead;
                    if(head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1;
                        do
                        {
                            if(candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.lgspType.TypeID!=1) {
                                continue;
                            }
                            if( (candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y==candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.lgspSource ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.lgspTarget : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.lgspSource) != candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y) {
                                continue;
                            }
                            Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne match = matches.GetNextUnfilledPosition();
                            match._node_x = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x;
                            match._node_y = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                            match._edge__edge0 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0;
                            match._edge__edge1 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1;
                            matches.PositionWasFilledFixIt();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.MoveInHeadAfter(candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1);
                                graph.MoveHeadAfter(candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0);
                                candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                                return matches;
                            }
                        }
                        while( (candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.lgspInNext) != head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 );
                    }
                    candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches)
        {
            foreach(Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches;
            
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
            
            Modify(graph, (Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.IMatch_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findArbitraryDirectedTriple
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches);
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
    
    public class Action_findArbitraryDirectedTriple : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findArbitraryDirectedTriple
    {
        public Action_findArbitraryDirectedTriple() {
            _rulePattern = Rule_findArbitraryDirectedTriple.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedTriple.Match_findArbitraryDirectedTriple, Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple>(this);
        }

        public Rule_findArbitraryDirectedTriple _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findArbitraryDirectedTriple"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedTriple.Match_findArbitraryDirectedTriple, Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches;

        public static Action_findArbitraryDirectedTriple Instance { get { return instance; } }
        private static Action_findArbitraryDirectedTriple instance = new Action_findArbitraryDirectedTriple();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedTriple_edge__edge1 
            int type_id_candidate_findArbitraryDirectedTriple_edge__edge1 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedTriple_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedTriple_edge__edge1], candidate_findArbitraryDirectedTriple_edge__edge1 = head_candidate_findArbitraryDirectedTriple_edge__edge1.lgspTypeNext; candidate_findArbitraryDirectedTriple_edge__edge1 != head_candidate_findArbitraryDirectedTriple_edge__edge1; candidate_findArbitraryDirectedTriple_edge__edge1 = candidate_findArbitraryDirectedTriple_edge__edge1.lgspTypeNext)
            {
                uint prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                prev__candidate_findArbitraryDirectedTriple_edge__edge1 = candidate_findArbitraryDirectedTriple_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findArbitraryDirectedTriple_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // both directions of findArbitraryDirectedTriple_edge__edge1
                for(int directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1 = 0; directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1 < 2; ++directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1)
                {
                    // Implicit SourceOrTarget findArbitraryDirectedTriple_node__node2 from findArbitraryDirectedTriple_edge__edge1 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedTriple_node__node2 = directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1==0 ? candidate_findArbitraryDirectedTriple_edge__edge1.lgspSource : candidate_findArbitraryDirectedTriple_edge__edge1.lgspTarget;
                    uint prev__candidate_findArbitraryDirectedTriple_node__node2;
                    prev__candidate_findArbitraryDirectedTriple_node__node2 = candidate_findArbitraryDirectedTriple_node__node2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedTriple_node__node2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findArbitraryDirectedTriple_node__node1 from findArbitraryDirectedTriple_edge__edge1 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedTriple_node__node1 = candidate_findArbitraryDirectedTriple_node__node2==candidate_findArbitraryDirectedTriple_edge__edge1.lgspSource ? candidate_findArbitraryDirectedTriple_edge__edge1.lgspTarget : candidate_findArbitraryDirectedTriple_edge__edge1.lgspSource;
                    if((candidate_findArbitraryDirectedTriple_node__node1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findArbitraryDirectedTriple_node__node2.lgspFlags = candidate_findArbitraryDirectedTriple_node__node2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node2;
                        continue;
                    }
                    uint prev__candidate_findArbitraryDirectedTriple_node__node1;
                    prev__candidate_findArbitraryDirectedTriple_node__node1 = candidate_findArbitraryDirectedTriple_node__node1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedTriple_node__node1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // both directions of findArbitraryDirectedTriple_edge__edge0
                    for(int directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0 = 0; directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0)
                    {
                        // Extend IncomingOrOutgoing findArbitraryDirectedTriple_edge__edge0 from findArbitraryDirectedTriple_node__node1 
                        GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedTriple_edge__edge0 = directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0==0 ? candidate_findArbitraryDirectedTriple_node__node1.lgspInhead : candidate_findArbitraryDirectedTriple_node__node1.lgspOuthead;
                        if(head_candidate_findArbitraryDirectedTriple_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_findArbitraryDirectedTriple_edge__edge0 = head_candidate_findArbitraryDirectedTriple_edge__edge0;
                            do
                            {
                                if(candidate_findArbitraryDirectedTriple_edge__edge0.lgspType.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_findArbitraryDirectedTriple_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // Implicit TheOther findArbitraryDirectedTriple_node__node0 from findArbitraryDirectedTriple_edge__edge0 
                                GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedTriple_node__node0 = candidate_findArbitraryDirectedTriple_node__node1==candidate_findArbitraryDirectedTriple_edge__edge0.lgspSource ? candidate_findArbitraryDirectedTriple_edge__edge0.lgspTarget : candidate_findArbitraryDirectedTriple_edge__edge0.lgspSource;
                                if((candidate_findArbitraryDirectedTriple_node__node0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                Rule_findArbitraryDirectedTriple.Match_findArbitraryDirectedTriple match = matches.GetNextUnfilledPosition();
                                match._node__node0 = candidate_findArbitraryDirectedTriple_node__node0;
                                match._node__node1 = candidate_findArbitraryDirectedTriple_node__node1;
                                match._node__node2 = candidate_findArbitraryDirectedTriple_node__node2;
                                match._edge__edge0 = candidate_findArbitraryDirectedTriple_edge__edge0;
                                match._edge__edge1 = candidate_findArbitraryDirectedTriple_edge__edge1;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    if(directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0==0) {
                                        candidate_findArbitraryDirectedTriple_node__node1.MoveInHeadAfter(candidate_findArbitraryDirectedTriple_edge__edge0);
                                    } else {
                                        candidate_findArbitraryDirectedTriple_node__node1.MoveOutHeadAfter(candidate_findArbitraryDirectedTriple_edge__edge0);
                                    }
                                    graph.MoveHeadAfter(candidate_findArbitraryDirectedTriple_edge__edge1);
                                    candidate_findArbitraryDirectedTriple_node__node1.lgspFlags = candidate_findArbitraryDirectedTriple_node__node1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node1;
                                    candidate_findArbitraryDirectedTriple_node__node2.lgspFlags = candidate_findArbitraryDirectedTriple_node__node2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node2;
                                    candidate_findArbitraryDirectedTriple_edge__edge1.lgspFlags = candidate_findArbitraryDirectedTriple_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                                    return matches;
                                }
                            }
                            while( (directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0==0 ? candidate_findArbitraryDirectedTriple_edge__edge0 = candidate_findArbitraryDirectedTriple_edge__edge0.lgspInNext : candidate_findArbitraryDirectedTriple_edge__edge0 = candidate_findArbitraryDirectedTriple_edge__edge0.lgspOutNext) != head_candidate_findArbitraryDirectedTriple_edge__edge0 );
                        }
                    }
                    candidate_findArbitraryDirectedTriple_node__node1.lgspFlags = candidate_findArbitraryDirectedTriple_node__node1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node1;
                    candidate_findArbitraryDirectedTriple_node__node2.lgspFlags = candidate_findArbitraryDirectedTriple_node__node2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node2;
                }
                candidate_findArbitraryDirectedTriple_edge__edge1.lgspFlags = candidate_findArbitraryDirectedTriple_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches)
        {
            foreach(Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple> matches;
            
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
            
            Modify(graph, (Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findArbitraryDirectedTriple.IMatch_findArbitraryDirectedTriple>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findDirectedEdge
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findDirectedEdge.IMatch_findDirectedEdge match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> matches);
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
    
    public class Action_findDirectedEdge : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findDirectedEdge
    {
        public Action_findDirectedEdge() {
            _rulePattern = Rule_findDirectedEdge.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findDirectedEdge.Match_findDirectedEdge, Rule_findDirectedEdge.IMatch_findDirectedEdge>(this);
        }

        public Rule_findDirectedEdge _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findDirectedEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findDirectedEdge.Match_findDirectedEdge, Rule_findDirectedEdge.IMatch_findDirectedEdge> matches;

        public static Action_findDirectedEdge Instance { get { return instance; } }
        private static Action_findDirectedEdge instance = new Action_findDirectedEdge();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findDirectedEdge_edge__edge0 
            int type_id_candidate_findDirectedEdge_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findDirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findDirectedEdge_edge__edge0], candidate_findDirectedEdge_edge__edge0 = head_candidate_findDirectedEdge_edge__edge0.lgspTypeNext; candidate_findDirectedEdge_edge__edge0 != head_candidate_findDirectedEdge_edge__edge0; candidate_findDirectedEdge_edge__edge0 = candidate_findDirectedEdge_edge__edge0.lgspTypeNext)
            {
                // Implicit Source findDirectedEdge_node_x from findDirectedEdge_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findDirectedEdge_node_x = candidate_findDirectedEdge_edge__edge0.lgspSource;
                uint prev__candidate_findDirectedEdge_node_x;
                prev__candidate_findDirectedEdge_node_x = candidate_findDirectedEdge_node_x.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findDirectedEdge_node_x.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target findDirectedEdge_node_y from findDirectedEdge_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findDirectedEdge_node_y = candidate_findDirectedEdge_edge__edge0.lgspTarget;
                if((candidate_findDirectedEdge_node_y.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_findDirectedEdge_node_x.lgspFlags = candidate_findDirectedEdge_node_x.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findDirectedEdge_node_x;
                    continue;
                }
                Rule_findDirectedEdge.Match_findDirectedEdge match = matches.GetNextUnfilledPosition();
                match._node_x = candidate_findDirectedEdge_node_x;
                match._node_y = candidate_findDirectedEdge_node_y;
                match._edge__edge0 = candidate_findDirectedEdge_edge__edge0;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_findDirectedEdge_edge__edge0);
                    candidate_findDirectedEdge_node_x.lgspFlags = candidate_findDirectedEdge_node_x.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findDirectedEdge_node_x;
                    return matches;
                }
                candidate_findDirectedEdge_node_x.lgspFlags = candidate_findDirectedEdge_node_x.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findDirectedEdge_node_x;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findDirectedEdge.IMatch_findDirectedEdge match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> matches)
        {
            foreach(Rule_findDirectedEdge.IMatch_findDirectedEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findDirectedEdge.IMatch_findDirectedEdge match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge> matches;
            
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
            
            Modify(graph, (Rule_findDirectedEdge.IMatch_findDirectedEdge)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findDirectedEdge.IMatch_findDirectedEdge>)matches);
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

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_findTripleCircle
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_findTripleCircle.IMatch_findTripleCircle match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matches);
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
    
    public class Action_findTripleCircle : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_findTripleCircle
    {
        public Action_findTripleCircle() {
            _rulePattern = Rule_findTripleCircle.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findTripleCircle.Match_findTripleCircle, Rule_findTripleCircle.IMatch_findTripleCircle>(this);
        }

        public Rule_findTripleCircle _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "findTripleCircle"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findTripleCircle.Match_findTripleCircle, Rule_findTripleCircle.IMatch_findTripleCircle> matches;

        public static Action_findTripleCircle Instance { get { return instance; } }
        private static Action_findTripleCircle instance = new Action_findTripleCircle();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findTripleCircle_edge__edge0 
            int type_id_candidate_findTripleCircle_edge__edge0 = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findTripleCircle_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findTripleCircle_edge__edge0], candidate_findTripleCircle_edge__edge0 = head_candidate_findTripleCircle_edge__edge0.lgspTypeNext; candidate_findTripleCircle_edge__edge0 != head_candidate_findTripleCircle_edge__edge0; candidate_findTripleCircle_edge__edge0 = candidate_findTripleCircle_edge__edge0.lgspTypeNext)
            {
                uint prev__candidate_findTripleCircle_edge__edge0;
                prev__candidate_findTripleCircle_edge__edge0 = candidate_findTripleCircle_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findTripleCircle_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // both directions of findTripleCircle_edge__edge0
                for(int directionRunCounterOf_findTripleCircle_edge__edge0 = 0; directionRunCounterOf_findTripleCircle_edge__edge0 < 2; ++directionRunCounterOf_findTripleCircle_edge__edge0)
                {
                    // Implicit SourceOrTarget findTripleCircle_node_y from findTripleCircle_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findTripleCircle_node_y = directionRunCounterOf_findTripleCircle_edge__edge0==0 ? candidate_findTripleCircle_edge__edge0.lgspSource : candidate_findTripleCircle_edge__edge0.lgspTarget;
                    uint prev__candidate_findTripleCircle_node_y;
                    prev__candidate_findTripleCircle_node_y = candidate_findTripleCircle_node_y.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findTripleCircle_node_y.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findTripleCircle_node_x from findTripleCircle_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findTripleCircle_node_x = candidate_findTripleCircle_node_y==candidate_findTripleCircle_edge__edge0.lgspSource ? candidate_findTripleCircle_edge__edge0.lgspTarget : candidate_findTripleCircle_edge__edge0.lgspSource;
                    if((candidate_findTripleCircle_node_x.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findTripleCircle_node_y.lgspFlags = candidate_findTripleCircle_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_y;
                        continue;
                    }
                    uint prev__candidate_findTripleCircle_node_x;
                    prev__candidate_findTripleCircle_node_x = candidate_findTripleCircle_node_x.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findTripleCircle_node_x.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // both directions of findTripleCircle_edge__edge1
                    for(int directionRunCounterOf_findTripleCircle_edge__edge1 = 0; directionRunCounterOf_findTripleCircle_edge__edge1 < 2; ++directionRunCounterOf_findTripleCircle_edge__edge1)
                    {
                        // Extend IncomingOrOutgoing findTripleCircle_edge__edge1 from findTripleCircle_node_y 
                        GRGEN_LGSP.LGSPEdge head_candidate_findTripleCircle_edge__edge1 = directionRunCounterOf_findTripleCircle_edge__edge1==0 ? candidate_findTripleCircle_node_y.lgspInhead : candidate_findTripleCircle_node_y.lgspOuthead;
                        if(head_candidate_findTripleCircle_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_findTripleCircle_edge__edge1 = head_candidate_findTripleCircle_edge__edge1;
                            do
                            {
                                if(candidate_findTripleCircle_edge__edge1.lgspType.TypeID!=2) {
                                    continue;
                                }
                                if((candidate_findTripleCircle_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                uint prev__candidate_findTripleCircle_edge__edge1;
                                prev__candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                candidate_findTripleCircle_edge__edge1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                // Implicit TheOther findTripleCircle_node_z from findTripleCircle_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_findTripleCircle_node_z = candidate_findTripleCircle_node_y==candidate_findTripleCircle_edge__edge1.lgspSource ? candidate_findTripleCircle_edge__edge1.lgspTarget : candidate_findTripleCircle_edge__edge1.lgspSource;
                                if((candidate_findTripleCircle_node_z.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    candidate_findTripleCircle_edge__edge1.lgspFlags = candidate_findTripleCircle_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge1;
                                    continue;
                                }
                                // both directions of findTripleCircle_edge__edge2
                                for(int directionRunCounterOf_findTripleCircle_edge__edge2 = 0; directionRunCounterOf_findTripleCircle_edge__edge2 < 2; ++directionRunCounterOf_findTripleCircle_edge__edge2)
                                {
                                    // Extend IncomingOrOutgoing findTripleCircle_edge__edge2 from findTripleCircle_node_z 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findTripleCircle_edge__edge2 = directionRunCounterOf_findTripleCircle_edge__edge2==0 ? candidate_findTripleCircle_node_z.lgspInhead : candidate_findTripleCircle_node_z.lgspOuthead;
                                    if(head_candidate_findTripleCircle_edge__edge2 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findTripleCircle_edge__edge2 = head_candidate_findTripleCircle_edge__edge2;
                                        do
                                        {
                                            if(candidate_findTripleCircle_edge__edge2.lgspType.TypeID!=2) {
                                                continue;
                                            }
                                            if( (candidate_findTripleCircle_node_z==candidate_findTripleCircle_edge__edge2.lgspSource ? candidate_findTripleCircle_edge__edge2.lgspTarget : candidate_findTripleCircle_edge__edge2.lgspSource) != candidate_findTripleCircle_node_x) {
                                                continue;
                                            }
                                            if((candidate_findTripleCircle_edge__edge2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                            {
                                                continue;
                                            }
                                            Rule_findTripleCircle.Match_findTripleCircle match = matches.GetNextUnfilledPosition();
                                            match._node_x = candidate_findTripleCircle_node_x;
                                            match._node_y = candidate_findTripleCircle_node_y;
                                            match._node_z = candidate_findTripleCircle_node_z;
                                            match._edge__edge0 = candidate_findTripleCircle_edge__edge0;
                                            match._edge__edge1 = candidate_findTripleCircle_edge__edge1;
                                            match._edge__edge2 = candidate_findTripleCircle_edge__edge2;
                                            matches.PositionWasFilledFixIt();
                                            // if enough matches were found, we leave
                                            if(maxMatches > 0 && matches.Count >= maxMatches)
                                            {
                                                if(directionRunCounterOf_findTripleCircle_edge__edge2==0) {
                                                    candidate_findTripleCircle_node_z.MoveInHeadAfter(candidate_findTripleCircle_edge__edge2);
                                                } else {
                                                    candidate_findTripleCircle_node_z.MoveOutHeadAfter(candidate_findTripleCircle_edge__edge2);
                                                }
                                                if(directionRunCounterOf_findTripleCircle_edge__edge1==0) {
                                                    candidate_findTripleCircle_node_y.MoveInHeadAfter(candidate_findTripleCircle_edge__edge1);
                                                } else {
                                                    candidate_findTripleCircle_node_y.MoveOutHeadAfter(candidate_findTripleCircle_edge__edge1);
                                                }
                                                graph.MoveHeadAfter(candidate_findTripleCircle_edge__edge0);
                                                candidate_findTripleCircle_edge__edge1.lgspFlags = candidate_findTripleCircle_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge1;
                                                candidate_findTripleCircle_node_x.lgspFlags = candidate_findTripleCircle_node_x.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_x;
                                                candidate_findTripleCircle_node_y.lgspFlags = candidate_findTripleCircle_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_y;
                                                candidate_findTripleCircle_edge__edge0.lgspFlags = candidate_findTripleCircle_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge0;
                                                return matches;
                                            }
                                        }
                                        while( (directionRunCounterOf_findTripleCircle_edge__edge2==0 ? candidate_findTripleCircle_edge__edge2 = candidate_findTripleCircle_edge__edge2.lgspInNext : candidate_findTripleCircle_edge__edge2 = candidate_findTripleCircle_edge__edge2.lgspOutNext) != head_candidate_findTripleCircle_edge__edge2 );
                                    }
                                }
                                candidate_findTripleCircle_edge__edge1.lgspFlags = candidate_findTripleCircle_edge__edge1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge1;
                            }
                            while( (directionRunCounterOf_findTripleCircle_edge__edge1==0 ? candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.lgspInNext : candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.lgspOutNext) != head_candidate_findTripleCircle_edge__edge1 );
                        }
                    }
                    candidate_findTripleCircle_node_x.lgspFlags = candidate_findTripleCircle_node_x.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_x;
                    candidate_findTripleCircle_node_y.lgspFlags = candidate_findTripleCircle_node_y.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_y;
                }
                candidate_findTripleCircle_edge__edge0.lgspFlags = candidate_findTripleCircle_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge0;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_findTripleCircle.IMatch_findTripleCircle match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matches)
        {
            foreach(Rule_findTripleCircle.IMatch_findTripleCircle match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_findTripleCircle.IMatch_findTripleCircle match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle> matches;
            
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
            
            Modify(graph, (Rule_findTripleCircle.IMatch_findTripleCircle)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_findTripleCircle.IMatch_findTripleCircle>)matches);
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
    public class edge1Actions : GRGEN_LGSP.LGSPActions
    {
        public edge1Actions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public edge1Actions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Rule_init.Instance);
            actions.Add("init", (GRGEN_LGSP.LGSPAction) Action_init.Instance);
            @init = Action_init.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_init2.Instance);
            actions.Add("init2", (GRGEN_LGSP.LGSPAction) Action_init2.Instance);
            @init2 = Action_init2.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_init3.Instance);
            actions.Add("init3", (GRGEN_LGSP.LGSPAction) Action_init3.Instance);
            @init3 = Action_init3.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findUndirectedEdge.Instance);
            actions.Add("findUndirectedEdge", (GRGEN_LGSP.LGSPAction) Action_findUndirectedEdge.Instance);
            @findUndirectedEdge = Action_findUndirectedEdge.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findArbitraryEdge.Instance);
            actions.Add("findArbitraryEdge", (GRGEN_LGSP.LGSPAction) Action_findArbitraryEdge.Instance);
            @findArbitraryEdge = Action_findArbitraryEdge.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findArbitraryDirectedEdge.Instance);
            actions.Add("findArbitraryDirectedEdge", (GRGEN_LGSP.LGSPAction) Action_findArbitraryDirectedEdge.Instance);
            @findArbitraryDirectedEdge = Action_findArbitraryDirectedEdge.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findArbitraryDirectedReflexiveEdge.Instance);
            actions.Add("findArbitraryDirectedReflexiveEdge", (GRGEN_LGSP.LGSPAction) Action_findArbitraryDirectedReflexiveEdge.Instance);
            @findArbitraryDirectedReflexiveEdge = Action_findArbitraryDirectedReflexiveEdge.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance);
            actions.Add("findArbitraryDirectedReflexiveEdgeAfterUndirectedOne", (GRGEN_LGSP.LGSPAction) Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance);
            @findArbitraryDirectedReflexiveEdgeAfterUndirectedOne = Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findArbitraryDirectedTriple.Instance);
            actions.Add("findArbitraryDirectedTriple", (GRGEN_LGSP.LGSPAction) Action_findArbitraryDirectedTriple.Instance);
            @findArbitraryDirectedTriple = Action_findArbitraryDirectedTriple.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findDirectedEdge.Instance);
            actions.Add("findDirectedEdge", (GRGEN_LGSP.LGSPAction) Action_findDirectedEdge.Instance);
            @findDirectedEdge = Action_findDirectedEdge.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_findTripleCircle.Instance);
            actions.Add("findTripleCircle", (GRGEN_LGSP.LGSPAction) Action_findTripleCircle.Instance);
            @findTripleCircle = Action_findTripleCircle.Instance;
            analyzer.ComputeInterPatternRelations();
        }
        
        public IAction_init @init;
        public IAction_init2 @init2;
        public IAction_init3 @init3;
        public IAction_findUndirectedEdge @findUndirectedEdge;
        public IAction_findArbitraryEdge @findArbitraryEdge;
        public IAction_findArbitraryDirectedEdge @findArbitraryDirectedEdge;
        public IAction_findArbitraryDirectedReflexiveEdge @findArbitraryDirectedReflexiveEdge;
        public IAction_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne @findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;
        public IAction_findArbitraryDirectedTriple @findArbitraryDirectedTriple;
        public IAction_findDirectedEdge @findDirectedEdge;
        public IAction_findTripleCircle @findTripleCircle;
        
        public override string Name { get { return "edge1Actions"; } }
        public override string ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}