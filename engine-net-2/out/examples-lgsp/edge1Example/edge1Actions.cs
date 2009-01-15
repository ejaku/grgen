// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\edge1\edge1.grg" on Thu Jan 15 21:54:05 CET 2009

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_edge1
{
	public class Rule_init : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init instance = null;
		public static Rule_init Instance { get { if (instance==null) { instance = new Rule_init(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum init_NodeNums { };
		public enum init_EdgeNums { };
		public enum init_VariableNums { };
		public enum init_SubNums { };
		public enum init_AltNums { };



		GRGEN_LGSP.PatternGraph pat_init;


#if INITIAL_WARMUP
		public Rule_init()
#else
		private Rule_init()
#endif
		{
			name = "init";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] init_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init_isEdgeHomomorphicGlobal = new bool[0, 0] ;
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
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init_isNodeHomomorphicGlobal,
				init_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_init;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init curMatch = (Match_init)_curMatch;
			graph.SettingAddedNodeNames( init_addedNodeNames );
			@Node node_x = @Node.CreateNode(graph);
			@Node node_y = @Node.CreateNode(graph);
			@Node node_z = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init_addedEdgeNames );
			@UEdge edge__edge0 = @UEdge.CreateEdge(graph, node_x, node_y);
			@UEdge edge__edge1 = @UEdge.CreateEdge(graph, node_y, node_z);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_y, node_y);
			return EmptyReturnElements;
		}
		private static String[] init_addedNodeNames = new String[] { "x", "y", "z" };
		private static String[] init_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init curMatch = (Match_init)_curMatch;
			graph.SettingAddedNodeNames( init_addedNodeNames );
			@Node node_x = @Node.CreateNode(graph);
			@Node node_y = @Node.CreateNode(graph);
			@Node node_z = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init_addedEdgeNames );
			@UEdge edge__edge0 = @UEdge.CreateEdge(graph, node_x, node_y);
			@UEdge edge__edge1 = @UEdge.CreateEdge(graph, node_y, node_z);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_y, node_y);
			return EmptyReturnElements;
		}

		static Rule_init() {
		}

		public interface IMatch_init : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_init2 : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init2 instance = null;
		public static Rule_init2 Instance { get { if (instance==null) { instance = new Rule_init2(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum init2_NodeNums { };
		public enum init2_EdgeNums { };
		public enum init2_VariableNums { };
		public enum init2_SubNums { };
		public enum init2_AltNums { };



		GRGEN_LGSP.PatternGraph pat_init2;


#if INITIAL_WARMUP
		public Rule_init2()
#else
		private Rule_init2()
#endif
		{
			name = "init2";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] init2_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init2_isEdgeHomomorphicGlobal = new bool[0, 0] ;
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
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init2_isNodeHomomorphicGlobal,
				init2_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_init2;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init2 curMatch = (Match_init2)_curMatch;
			graph.SettingAddedNodeNames( init2_addedNodeNames );
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node__node2 = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init2_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node2, node__node1);
			return EmptyReturnElements;
		}
		private static String[] init2_addedNodeNames = new String[] { "_node0", "_node1", "_node2" };
		private static String[] init2_addedEdgeNames = new String[] { "_edge0", "_edge1" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init2 curMatch = (Match_init2)_curMatch;
			graph.SettingAddedNodeNames( init2_addedNodeNames );
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node__node2 = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init2_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node2, node__node1);
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_init3 : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init3 instance = null;
		public static Rule_init3 Instance { get { if (instance==null) { instance = new Rule_init3(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public enum init3_NodeNums { };
		public enum init3_EdgeNums { };
		public enum init3_VariableNums { };
		public enum init3_SubNums { };
		public enum init3_AltNums { };



		GRGEN_LGSP.PatternGraph pat_init3;


#if INITIAL_WARMUP
		public Rule_init3()
#else
		private Rule_init3()
#endif
		{
			name = "init3";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] init3_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] init3_isEdgeHomomorphicGlobal = new bool[0, 0] ;
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
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				init3_isNodeHomomorphicGlobal,
				init3_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_init3;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init3 curMatch = (Match_init3)_curMatch;
			graph.SettingAddedNodeNames( init3_addedNodeNames );
			@Node node_x = @Node.CreateNode(graph);
			@Node node_y = @Node.CreateNode(graph);
			@Node node_z = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init3_addedEdgeNames );
			@UEdge edge__edge0 = @UEdge.CreateEdge(graph, node_x, node_y);
			@UEdge edge__edge1 = @UEdge.CreateEdge(graph, node_y, node_z);
			@UEdge edge__edge2 = @UEdge.CreateEdge(graph, node_z, node_x);
			return EmptyReturnElements;
		}
		private static String[] init3_addedNodeNames = new String[] { "x", "y", "z" };
		private static String[] init3_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_init3 curMatch = (Match_init3)_curMatch;
			graph.SettingAddedNodeNames( init3_addedNodeNames );
			@Node node_x = @Node.CreateNode(graph);
			@Node node_y = @Node.CreateNode(graph);
			@Node node_z = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( init3_addedEdgeNames );
			@UEdge edge__edge0 = @UEdge.CreateEdge(graph, node_x, node_y);
			@UEdge edge__edge1 = @UEdge.CreateEdge(graph, node_y, node_z);
			@UEdge edge__edge2 = @UEdge.CreateEdge(graph, node_z, node_x);
			return EmptyReturnElements;
		}

		static Rule_init3() {
		}

		public interface IMatch_init3 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findUndirectedEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findUndirectedEdge instance = null;
		public static Rule_findUndirectedEdge Instance { get { if (instance==null) { instance = new Rule_findUndirectedEdge(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_findUndirectedEdge;


#if INITIAL_WARMUP
		public Rule_findUndirectedEdge()
#else
		private Rule_findUndirectedEdge()
#endif
		{
			name = "findUndirectedEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] findUndirectedEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findUndirectedEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode findUndirectedEdge_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findUndirectedEdge_node_x", "x", findUndirectedEdge_node_x_AllowedTypes, findUndirectedEdge_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findUndirectedEdge_node_y = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findUndirectedEdge_node_y", "y", findUndirectedEdge_node_y_AllowedTypes, findUndirectedEdge_node_y_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findUndirectedEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findUndirectedEdge_edge__edge0", "_edge0", findUndirectedEdge_edge__edge0_AllowedTypes, findUndirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findUndirectedEdge curMatch = (Match_findUndirectedEdge)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findUndirectedEdge curMatch = (Match_findUndirectedEdge)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryEdge instance = null;
		public static Rule_findArbitraryEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryEdge(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_findArbitraryEdge;


#if INITIAL_WARMUP
		public Rule_findArbitraryEdge()
#else
		private Rule_findArbitraryEdge()
#endif
		{
			name = "findArbitraryEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] findArbitraryEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findArbitraryEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode findArbitraryEdge_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryEdge_node_x", "x", findArbitraryEdge_node_x_AllowedTypes, findArbitraryEdge_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findArbitraryEdge_node_y = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryEdge_node_y", "y", findArbitraryEdge_node_y_AllowedTypes, findArbitraryEdge_node_y_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findArbitraryEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@AEdge, "GRGEN_LIBGR.IEdge", "findArbitraryEdge_edge__edge0", "_edge0", findArbitraryEdge_edge__edge0_AllowedTypes, findArbitraryEdge_edge__edge0_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryEdge curMatch = (Match_findArbitraryEdge)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryEdge curMatch = (Match_findArbitraryEdge)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedEdge instance = null;
		public static Rule_findArbitraryDirectedEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedEdge(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedEdge;


#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedEdge()
#else
		private Rule_findArbitraryDirectedEdge()
#endif
		{
			name = "findArbitraryDirectedEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] findArbitraryDirectedEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findArbitraryDirectedEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode findArbitraryDirectedEdge_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedEdge_node_x", "x", findArbitraryDirectedEdge_node_x_AllowedTypes, findArbitraryDirectedEdge_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findArbitraryDirectedEdge_node_y = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedEdge_node_y", "y", findArbitraryDirectedEdge_node_y_AllowedTypes, findArbitraryDirectedEdge_node_y_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedEdge_edge__edge0", "_edge0", findArbitraryDirectedEdge_edge__edge0_AllowedTypes, findArbitraryDirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedEdge curMatch = (Match_findArbitraryDirectedEdge)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedEdge curMatch = (Match_findArbitraryDirectedEdge)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedReflexiveEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedReflexiveEdge instance = null;
		public static Rule_findArbitraryDirectedReflexiveEdge Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedReflexiveEdge(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] findArbitraryDirectedReflexiveEdge_node_x_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdge_node_x_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findArbitraryDirectedReflexiveEdge_edge__edge0_AllowedTypes = null;
		public static bool[] findArbitraryDirectedReflexiveEdge_edge__edge0_IsAllowedType = null;
		public enum findArbitraryDirectedReflexiveEdge_NodeNums { @x, };
		public enum findArbitraryDirectedReflexiveEdge_EdgeNums { @_edge0, };
		public enum findArbitraryDirectedReflexiveEdge_VariableNums { };
		public enum findArbitraryDirectedReflexiveEdge_SubNums { };
		public enum findArbitraryDirectedReflexiveEdge_AltNums { };


		GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedReflexiveEdge;


#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedReflexiveEdge()
#else
		private Rule_findArbitraryDirectedReflexiveEdge()
#endif
		{
			name = "findArbitraryDirectedReflexiveEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] findArbitraryDirectedReflexiveEdge_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] findArbitraryDirectedReflexiveEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode findArbitraryDirectedReflexiveEdge_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedReflexiveEdge_node_x", "x", findArbitraryDirectedReflexiveEdge_node_x_AllowedTypes, findArbitraryDirectedReflexiveEdge_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedReflexiveEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedReflexiveEdge_edge__edge0", "_edge0", findArbitraryDirectedReflexiveEdge_edge__edge0_AllowedTypes, findArbitraryDirectedReflexiveEdge_edge__edge0_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedReflexiveEdge curMatch = (Match_findArbitraryDirectedReflexiveEdge)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedReflexiveEdge curMatch = (Match_findArbitraryDirectedReflexiveEdge)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne instance = null;
		public static Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne;


#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne()
#else
		private Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne()
#endif
		{
			name = "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x", "x", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y", "y", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0", "_edge0", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1", "_edge1", findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_AllowedTypes, findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne curMatch = (Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne curMatch = (Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findArbitraryDirectedTriple : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findArbitraryDirectedTriple instance = null;
		public static Rule_findArbitraryDirectedTriple Instance { get { if (instance==null) { instance = new Rule_findArbitraryDirectedTriple(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_findArbitraryDirectedTriple;


#if INITIAL_WARMUP
		public Rule_findArbitraryDirectedTriple()
#else
		private Rule_findArbitraryDirectedTriple()
#endif
		{
			name = "findArbitraryDirectedTriple";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode findArbitraryDirectedTriple_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedTriple_node__node0", "_node0", findArbitraryDirectedTriple_node__node0_AllowedTypes, findArbitraryDirectedTriple_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findArbitraryDirectedTriple_node__node1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedTriple_node__node1", "_node1", findArbitraryDirectedTriple_node__node1_AllowedTypes, findArbitraryDirectedTriple_node__node1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findArbitraryDirectedTriple_node__node2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findArbitraryDirectedTriple_node__node2", "_node2", findArbitraryDirectedTriple_node__node2_AllowedTypes, findArbitraryDirectedTriple_node__node2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedTriple_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedTriple_edge__edge0", "_edge0", findArbitraryDirectedTriple_edge__edge0_AllowedTypes, findArbitraryDirectedTriple_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findArbitraryDirectedTriple_edge__edge1 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findArbitraryDirectedTriple_edge__edge1", "_edge1", findArbitraryDirectedTriple_edge__edge1_AllowedTypes, findArbitraryDirectedTriple_edge__edge1_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedTriple curMatch = (Match_findArbitraryDirectedTriple)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findArbitraryDirectedTriple curMatch = (Match_findArbitraryDirectedTriple)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findDirectedEdge : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findDirectedEdge instance = null;
		public static Rule_findDirectedEdge Instance { get { if (instance==null) { instance = new Rule_findDirectedEdge(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_findDirectedEdge;


#if INITIAL_WARMUP
		public Rule_findDirectedEdge()
#else
		private Rule_findDirectedEdge()
#endif
		{
			name = "findDirectedEdge";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] findDirectedEdge_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findDirectedEdge_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode findDirectedEdge_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findDirectedEdge_node_x", "x", findDirectedEdge_node_x_AllowedTypes, findDirectedEdge_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findDirectedEdge_node_y = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findDirectedEdge_node_y", "y", findDirectedEdge_node_y_AllowedTypes, findDirectedEdge_node_y_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findDirectedEdge_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findDirectedEdge_edge__edge0", "_edge0", findDirectedEdge_edge__edge0_AllowedTypes, findDirectedEdge_edge__edge0_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findDirectedEdge curMatch = (Match_findDirectedEdge)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findDirectedEdge curMatch = (Match_findDirectedEdge)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findTripleCircle : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findTripleCircle instance = null;
		public static Rule_findTripleCircle Instance { get { if (instance==null) { instance = new Rule_findTripleCircle(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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


		GRGEN_LGSP.PatternGraph pat_findTripleCircle;


#if INITIAL_WARMUP
		public Rule_findTripleCircle()
#else
		private Rule_findTripleCircle()
#endif
		{
			name = "findTripleCircle";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode findTripleCircle_node_x = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findTripleCircle_node_x", "x", findTripleCircle_node_x_AllowedTypes, findTripleCircle_node_x_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findTripleCircle_node_y = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findTripleCircle_node_y", "y", findTripleCircle_node_y_AllowedTypes, findTripleCircle_node_y_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findTripleCircle_node_z = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findTripleCircle_node_z", "z", findTripleCircle_node_z_AllowedTypes, findTripleCircle_node_z_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findTripleCircle_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findTripleCircle_edge__edge0", "_edge0", findTripleCircle_edge__edge0_AllowedTypes, findTripleCircle_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findTripleCircle_edge__edge1 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findTripleCircle_edge__edge1", "_edge1", findTripleCircle_edge__edge1_AllowedTypes, findTripleCircle_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findTripleCircle_edge__edge2 = new GRGEN_LGSP.PatternEdge(false, (int) EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "findTripleCircle_edge__edge2", "_edge2", findTripleCircle_edge__edge2_AllowedTypes, findTripleCircle_edge__edge2_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findTripleCircle curMatch = (Match_findTripleCircle)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findTripleCircle curMatch = (Match_findTripleCircle)_curMatch;
			return EmptyReturnElements;
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
			//Independents
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}


    public class Action_init : GRGEN_LGSP.LGSPAction
    {
        public Action_init() {
            rulePattern = Rule_init.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_init.Match_init>(this);
        }

        public override string Name { get { return "init"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_init.Match_init> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_init instance = new Action_init();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
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
    }

    public class Action_init2 : GRGEN_LGSP.LGSPAction
    {
        public Action_init2() {
            rulePattern = Rule_init2.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_init2.Match_init2>(this);
        }

        public override string Name { get { return "init2"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_init2.Match_init2> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_init2 instance = new Action_init2();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
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
    }

    public class Action_init3 : GRGEN_LGSP.LGSPAction
    {
        public Action_init3() {
            rulePattern = Rule_init3.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_init3.Match_init3>(this);
        }

        public override string Name { get { return "init3"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_init3.Match_init3> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_init3 instance = new Action_init3();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
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
    }

    public class Action_findUndirectedEdge : GRGEN_LGSP.LGSPAction
    {
        public Action_findUndirectedEdge() {
            rulePattern = Rule_findUndirectedEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findUndirectedEdge.Match_findUndirectedEdge>(this);
        }

        public override string Name { get { return "findUndirectedEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findUndirectedEdge.Match_findUndirectedEdge> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findUndirectedEdge instance = new Action_findUndirectedEdge();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findUndirectedEdge_edge__edge0 
            int type_id_candidate_findUndirectedEdge_edge__edge0 = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findUndirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findUndirectedEdge_edge__edge0], candidate_findUndirectedEdge_edge__edge0 = head_candidate_findUndirectedEdge_edge__edge0.typeNext; candidate_findUndirectedEdge_edge__edge0 != head_candidate_findUndirectedEdge_edge__edge0; candidate_findUndirectedEdge_edge__edge0 = candidate_findUndirectedEdge_edge__edge0.typeNext)
            {
                // both directions of findUndirectedEdge_edge__edge0
                for(int directionRunCounterOf_findUndirectedEdge_edge__edge0 = 0; directionRunCounterOf_findUndirectedEdge_edge__edge0 < 2; ++directionRunCounterOf_findUndirectedEdge_edge__edge0)
                {
                    // Implicit SourceOrTarget findUndirectedEdge_node_y from findUndirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findUndirectedEdge_node_y = directionRunCounterOf_findUndirectedEdge_edge__edge0==0 ? candidate_findUndirectedEdge_edge__edge0.source : candidate_findUndirectedEdge_edge__edge0.target;
                    uint prev__candidate_findUndirectedEdge_node_y;
                    prev__candidate_findUndirectedEdge_node_y = candidate_findUndirectedEdge_node_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findUndirectedEdge_node_y.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findUndirectedEdge_node_x from findUndirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findUndirectedEdge_node_x = candidate_findUndirectedEdge_node_y==candidate_findUndirectedEdge_edge__edge0.source ? candidate_findUndirectedEdge_edge__edge0.target : candidate_findUndirectedEdge_edge__edge0.source;
                    if((candidate_findUndirectedEdge_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findUndirectedEdge_node_y.flags = candidate_findUndirectedEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findUndirectedEdge_node_y;
                        goto label0;
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
                        candidate_findUndirectedEdge_node_y.flags = candidate_findUndirectedEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findUndirectedEdge_node_y;
                        return matches;
                    }
                    candidate_findUndirectedEdge_node_y.flags = candidate_findUndirectedEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findUndirectedEdge_node_y;
                }
label0: ;
            }
            return matches;
        }
    }

    public class Action_findArbitraryEdge : GRGEN_LGSP.LGSPAction
    {
        public Action_findArbitraryEdge() {
            rulePattern = Rule_findArbitraryEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryEdge.Match_findArbitraryEdge>(this);
        }

        public override string Name { get { return "findArbitraryEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryEdge.Match_findArbitraryEdge> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryEdge instance = new Action_findArbitraryEdge();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryEdge_edge__edge0 
            foreach(GRGEN_LIBGR.EdgeType type_candidate_findArbitraryEdge_edge__edge0 in EdgeType_AEdge.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_findArbitraryEdge_edge__edge0 = type_candidate_findArbitraryEdge_edge__edge0.TypeID;
                for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryEdge_edge__edge0], candidate_findArbitraryEdge_edge__edge0 = head_candidate_findArbitraryEdge_edge__edge0.typeNext; candidate_findArbitraryEdge_edge__edge0 != head_candidate_findArbitraryEdge_edge__edge0; candidate_findArbitraryEdge_edge__edge0 = candidate_findArbitraryEdge_edge__edge0.typeNext)
                {
                    // both directions of findArbitraryEdge_edge__edge0
                    for(int directionRunCounterOf_findArbitraryEdge_edge__edge0 = 0; directionRunCounterOf_findArbitraryEdge_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryEdge_edge__edge0)
                    {
                        // Implicit SourceOrTarget findArbitraryEdge_node_y from findArbitraryEdge_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_findArbitraryEdge_node_y = directionRunCounterOf_findArbitraryEdge_edge__edge0==0 ? candidate_findArbitraryEdge_edge__edge0.source : candidate_findArbitraryEdge_edge__edge0.target;
                        uint prev__candidate_findArbitraryEdge_node_y;
                        prev__candidate_findArbitraryEdge_node_y = candidate_findArbitraryEdge_node_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_findArbitraryEdge_node_y.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        // Implicit TheOther findArbitraryEdge_node_x from findArbitraryEdge_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_findArbitraryEdge_node_x = candidate_findArbitraryEdge_node_y==candidate_findArbitraryEdge_edge__edge0.source ? candidate_findArbitraryEdge_edge__edge0.target : candidate_findArbitraryEdge_edge__edge0.source;
                        if((candidate_findArbitraryEdge_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            candidate_findArbitraryEdge_node_y.flags = candidate_findArbitraryEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryEdge_node_y;
                            goto label1;
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
                            candidate_findArbitraryEdge_node_y.flags = candidate_findArbitraryEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryEdge_node_y;
                            return matches;
                        }
                        candidate_findArbitraryEdge_node_y.flags = candidate_findArbitraryEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryEdge_node_y;
                    }
label1: ;
                }
            }
            return matches;
        }
    }

    public class Action_findArbitraryDirectedEdge : GRGEN_LGSP.LGSPAction
    {
        public Action_findArbitraryDirectedEdge() {
            rulePattern = Rule_findArbitraryDirectedEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedEdge.Match_findArbitraryDirectedEdge>(this);
        }

        public override string Name { get { return "findArbitraryDirectedEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedEdge.Match_findArbitraryDirectedEdge> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedEdge instance = new Action_findArbitraryDirectedEdge();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedEdge_edge__edge0 
            int type_id_candidate_findArbitraryDirectedEdge_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedEdge_edge__edge0], candidate_findArbitraryDirectedEdge_edge__edge0 = head_candidate_findArbitraryDirectedEdge_edge__edge0.typeNext; candidate_findArbitraryDirectedEdge_edge__edge0 != head_candidate_findArbitraryDirectedEdge_edge__edge0; candidate_findArbitraryDirectedEdge_edge__edge0 = candidate_findArbitraryDirectedEdge_edge__edge0.typeNext)
            {
                // both directions of findArbitraryDirectedEdge_edge__edge0
                for(int directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0 = 0; directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0)
                {
                    // Implicit SourceOrTarget findArbitraryDirectedEdge_node_y from findArbitraryDirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedEdge_node_y = directionRunCounterOf_findArbitraryDirectedEdge_edge__edge0==0 ? candidate_findArbitraryDirectedEdge_edge__edge0.source : candidate_findArbitraryDirectedEdge_edge__edge0.target;
                    uint prev__candidate_findArbitraryDirectedEdge_node_y;
                    prev__candidate_findArbitraryDirectedEdge_node_y = candidate_findArbitraryDirectedEdge_node_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedEdge_node_y.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findArbitraryDirectedEdge_node_x from findArbitraryDirectedEdge_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedEdge_node_x = candidate_findArbitraryDirectedEdge_node_y==candidate_findArbitraryDirectedEdge_edge__edge0.source ? candidate_findArbitraryDirectedEdge_edge__edge0.target : candidate_findArbitraryDirectedEdge_edge__edge0.source;
                    if((candidate_findArbitraryDirectedEdge_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findArbitraryDirectedEdge_node_y.flags = candidate_findArbitraryDirectedEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedEdge_node_y;
                        goto label2;
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
                        candidate_findArbitraryDirectedEdge_node_y.flags = candidate_findArbitraryDirectedEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedEdge_node_y;
                        return matches;
                    }
                    candidate_findArbitraryDirectedEdge_node_y.flags = candidate_findArbitraryDirectedEdge_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedEdge_node_y;
                }
label2: ;
            }
            return matches;
        }
    }

    public class Action_findArbitraryDirectedReflexiveEdge : GRGEN_LGSP.LGSPAction
    {
        public Action_findArbitraryDirectedReflexiveEdge() {
            rulePattern = Rule_findArbitraryDirectedReflexiveEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdge.Match_findArbitraryDirectedReflexiveEdge>(this);
        }

        public override string Name { get { return "findArbitraryDirectedReflexiveEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdge.Match_findArbitraryDirectedReflexiveEdge> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedReflexiveEdge instance = new Action_findArbitraryDirectedReflexiveEdge();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedReflexiveEdge_edge__edge0 
            int type_id_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0], candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.typeNext; candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 != head_candidate_findArbitraryDirectedReflexiveEdge_edge__edge0; candidate_findArbitraryDirectedReflexiveEdge_edge__edge0 = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.typeNext)
            {
                // Implicit Source findArbitraryDirectedReflexiveEdge_node_x from findArbitraryDirectedReflexiveEdge_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedReflexiveEdge_node_x = candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.source;
                if(candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.source != candidate_findArbitraryDirectedReflexiveEdge_node_x) {
                    continue;
                }
                if(candidate_findArbitraryDirectedReflexiveEdge_edge__edge0.target != candidate_findArbitraryDirectedReflexiveEdge_node_x) {
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
    }

    public class Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne : GRGEN_LGSP.LGSPAction
    {
        public Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne() {
            rulePattern = Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne>(this);
        }

        public override string Name { get { return "findArbitraryDirectedReflexiveEdgeAfterUndirectedOne"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Match_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne instance = new Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
            int type_id_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0], candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.typeNext; candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 != head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0; candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.typeNext)
            {
                // both directions of findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0
                for(int directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 = 0; directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0)
                {
                    // Implicit SourceOrTarget findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = directionRunCounterOf_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0==0 ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.source : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.target;
                    uint prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                    prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y==candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.source ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.target : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge0.source;
                    if((candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                        goto label3;
                    }
                    // Extend Incoming findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 from findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y 
                    GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.inhead;
                    if(head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1;
                        do
                        {
                            if(candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.type.TypeID!=1) {
                                continue;
                            }
                            if( (candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y==candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.source ? candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.target : candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.source) != candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y) {
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
                                candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                                return matches;
                            }
                        }
                        while( (candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1.inNext) != head_candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_edge__edge1 );
                    }
                    candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags = candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne_node_y;
                }
label3: ;
            }
            return matches;
        }
    }

    public class Action_findArbitraryDirectedTriple : GRGEN_LGSP.LGSPAction
    {
        public Action_findArbitraryDirectedTriple() {
            rulePattern = Rule_findArbitraryDirectedTriple.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedTriple.Match_findArbitraryDirectedTriple>(this);
        }

        public override string Name { get { return "findArbitraryDirectedTriple"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findArbitraryDirectedTriple.Match_findArbitraryDirectedTriple> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findArbitraryDirectedTriple instance = new Action_findArbitraryDirectedTriple();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findArbitraryDirectedTriple_edge__edge1 
            int type_id_candidate_findArbitraryDirectedTriple_edge__edge1 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedTriple_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_findArbitraryDirectedTriple_edge__edge1], candidate_findArbitraryDirectedTriple_edge__edge1 = head_candidate_findArbitraryDirectedTriple_edge__edge1.typeNext; candidate_findArbitraryDirectedTriple_edge__edge1 != head_candidate_findArbitraryDirectedTriple_edge__edge1; candidate_findArbitraryDirectedTriple_edge__edge1 = candidate_findArbitraryDirectedTriple_edge__edge1.typeNext)
            {
                uint prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                prev__candidate_findArbitraryDirectedTriple_edge__edge1 = candidate_findArbitraryDirectedTriple_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findArbitraryDirectedTriple_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // both directions of findArbitraryDirectedTriple_edge__edge1
                for(int directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1 = 0; directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1 < 2; ++directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1)
                {
                    // Implicit SourceOrTarget findArbitraryDirectedTriple_node__node2 from findArbitraryDirectedTriple_edge__edge1 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedTriple_node__node2 = directionRunCounterOf_findArbitraryDirectedTriple_edge__edge1==0 ? candidate_findArbitraryDirectedTriple_edge__edge1.source : candidate_findArbitraryDirectedTriple_edge__edge1.target;
                    uint prev__candidate_findArbitraryDirectedTriple_node__node2;
                    prev__candidate_findArbitraryDirectedTriple_node__node2 = candidate_findArbitraryDirectedTriple_node__node2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedTriple_node__node2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findArbitraryDirectedTriple_node__node1 from findArbitraryDirectedTriple_edge__edge1 
                    GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedTriple_node__node1 = candidate_findArbitraryDirectedTriple_node__node2==candidate_findArbitraryDirectedTriple_edge__edge1.source ? candidate_findArbitraryDirectedTriple_edge__edge1.target : candidate_findArbitraryDirectedTriple_edge__edge1.source;
                    if((candidate_findArbitraryDirectedTriple_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findArbitraryDirectedTriple_node__node2.flags = candidate_findArbitraryDirectedTriple_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node2;
                        candidate_findArbitraryDirectedTriple_edge__edge1.flags = candidate_findArbitraryDirectedTriple_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                        goto label4;
                    }
                    uint prev__candidate_findArbitraryDirectedTriple_node__node1;
                    prev__candidate_findArbitraryDirectedTriple_node__node1 = candidate_findArbitraryDirectedTriple_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findArbitraryDirectedTriple_node__node1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // both directions of findArbitraryDirectedTriple_edge__edge0
                    for(int directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0 = 0; directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0 < 2; ++directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0)
                    {
                        // Extend IncomingOrOutgoing findArbitraryDirectedTriple_edge__edge0 from findArbitraryDirectedTriple_node__node1 
                        GRGEN_LGSP.LGSPEdge head_candidate_findArbitraryDirectedTriple_edge__edge0 = directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0==0 ? candidate_findArbitraryDirectedTriple_node__node1.inhead : candidate_findArbitraryDirectedTriple_node__node1.outhead;
                        if(head_candidate_findArbitraryDirectedTriple_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_findArbitraryDirectedTriple_edge__edge0 = head_candidate_findArbitraryDirectedTriple_edge__edge0;
                            do
                            {
                                if(candidate_findArbitraryDirectedTriple_edge__edge0.type.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_findArbitraryDirectedTriple_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // Implicit TheOther findArbitraryDirectedTriple_node__node0 from findArbitraryDirectedTriple_edge__edge0 
                                GRGEN_LGSP.LGSPNode candidate_findArbitraryDirectedTriple_node__node0 = candidate_findArbitraryDirectedTriple_node__node1==candidate_findArbitraryDirectedTriple_edge__edge0.source ? candidate_findArbitraryDirectedTriple_edge__edge0.target : candidate_findArbitraryDirectedTriple_edge__edge0.source;
                                if((candidate_findArbitraryDirectedTriple_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                    candidate_findArbitraryDirectedTriple_node__node1.flags = candidate_findArbitraryDirectedTriple_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node1;
                                    candidate_findArbitraryDirectedTriple_node__node2.flags = candidate_findArbitraryDirectedTriple_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node2;
                                    candidate_findArbitraryDirectedTriple_edge__edge1.flags = candidate_findArbitraryDirectedTriple_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
                                    return matches;
                                }
                            }
                            while( (directionRunCounterOf_findArbitraryDirectedTriple_edge__edge0==0 ? candidate_findArbitraryDirectedTriple_edge__edge0 = candidate_findArbitraryDirectedTriple_edge__edge0.inNext : candidate_findArbitraryDirectedTriple_edge__edge0 = candidate_findArbitraryDirectedTriple_edge__edge0.outNext) != head_candidate_findArbitraryDirectedTriple_edge__edge0 );
                        }
                    }
                    candidate_findArbitraryDirectedTriple_node__node1.flags = candidate_findArbitraryDirectedTriple_node__node1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node1;
                    candidate_findArbitraryDirectedTriple_node__node2.flags = candidate_findArbitraryDirectedTriple_node__node2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_node__node2;
                }
                candidate_findArbitraryDirectedTriple_edge__edge1.flags = candidate_findArbitraryDirectedTriple_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findArbitraryDirectedTriple_edge__edge1;
label4: ;
            }
            return matches;
        }
    }

    public class Action_findDirectedEdge : GRGEN_LGSP.LGSPAction
    {
        public Action_findDirectedEdge() {
            rulePattern = Rule_findDirectedEdge.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findDirectedEdge.Match_findDirectedEdge>(this);
        }

        public override string Name { get { return "findDirectedEdge"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findDirectedEdge.Match_findDirectedEdge> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findDirectedEdge instance = new Action_findDirectedEdge();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findDirectedEdge_edge__edge0 
            int type_id_candidate_findDirectedEdge_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findDirectedEdge_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findDirectedEdge_edge__edge0], candidate_findDirectedEdge_edge__edge0 = head_candidate_findDirectedEdge_edge__edge0.typeNext; candidate_findDirectedEdge_edge__edge0 != head_candidate_findDirectedEdge_edge__edge0; candidate_findDirectedEdge_edge__edge0 = candidate_findDirectedEdge_edge__edge0.typeNext)
            {
                // Implicit Source findDirectedEdge_node_x from findDirectedEdge_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findDirectedEdge_node_x = candidate_findDirectedEdge_edge__edge0.source;
                uint prev__candidate_findDirectedEdge_node_x;
                prev__candidate_findDirectedEdge_node_x = candidate_findDirectedEdge_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findDirectedEdge_node_x.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target findDirectedEdge_node_y from findDirectedEdge_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_findDirectedEdge_node_y = candidate_findDirectedEdge_edge__edge0.target;
                if((candidate_findDirectedEdge_node_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_findDirectedEdge_node_x.flags = candidate_findDirectedEdge_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findDirectedEdge_node_x;
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
                    candidate_findDirectedEdge_node_x.flags = candidate_findDirectedEdge_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findDirectedEdge_node_x;
                    return matches;
                }
                candidate_findDirectedEdge_node_x.flags = candidate_findDirectedEdge_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findDirectedEdge_node_x;
            }
            return matches;
        }
    }

    public class Action_findTripleCircle : GRGEN_LGSP.LGSPAction
    {
        public Action_findTripleCircle() {
            rulePattern = Rule_findTripleCircle.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findTripleCircle.Match_findTripleCircle>(this);
        }

        public override string Name { get { return "findTripleCircle"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findTripleCircle.Match_findTripleCircle> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findTripleCircle instance = new Action_findTripleCircle();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findTripleCircle_edge__edge0 
            int type_id_candidate_findTripleCircle_edge__edge0 = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findTripleCircle_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_findTripleCircle_edge__edge0], candidate_findTripleCircle_edge__edge0 = head_candidate_findTripleCircle_edge__edge0.typeNext; candidate_findTripleCircle_edge__edge0 != head_candidate_findTripleCircle_edge__edge0; candidate_findTripleCircle_edge__edge0 = candidate_findTripleCircle_edge__edge0.typeNext)
            {
                uint prev__candidate_findTripleCircle_edge__edge0;
                prev__candidate_findTripleCircle_edge__edge0 = candidate_findTripleCircle_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findTripleCircle_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // both directions of findTripleCircle_edge__edge0
                for(int directionRunCounterOf_findTripleCircle_edge__edge0 = 0; directionRunCounterOf_findTripleCircle_edge__edge0 < 2; ++directionRunCounterOf_findTripleCircle_edge__edge0)
                {
                    // Implicit SourceOrTarget findTripleCircle_node_y from findTripleCircle_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findTripleCircle_node_y = directionRunCounterOf_findTripleCircle_edge__edge0==0 ? candidate_findTripleCircle_edge__edge0.source : candidate_findTripleCircle_edge__edge0.target;
                    uint prev__candidate_findTripleCircle_node_y;
                    prev__candidate_findTripleCircle_node_y = candidate_findTripleCircle_node_y.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findTripleCircle_node_y.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit TheOther findTripleCircle_node_x from findTripleCircle_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_findTripleCircle_node_x = candidate_findTripleCircle_node_y==candidate_findTripleCircle_edge__edge0.source ? candidate_findTripleCircle_edge__edge0.target : candidate_findTripleCircle_edge__edge0.source;
                    if((candidate_findTripleCircle_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        candidate_findTripleCircle_node_y.flags = candidate_findTripleCircle_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_y;
                        candidate_findTripleCircle_edge__edge0.flags = candidate_findTripleCircle_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge0;
                        goto label5;
                    }
                    uint prev__candidate_findTripleCircle_node_x;
                    prev__candidate_findTripleCircle_node_x = candidate_findTripleCircle_node_x.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_findTripleCircle_node_x.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // both directions of findTripleCircle_edge__edge1
                    for(int directionRunCounterOf_findTripleCircle_edge__edge1 = 0; directionRunCounterOf_findTripleCircle_edge__edge1 < 2; ++directionRunCounterOf_findTripleCircle_edge__edge1)
                    {
                        // Extend IncomingOrOutgoing findTripleCircle_edge__edge1 from findTripleCircle_node_y 
                        GRGEN_LGSP.LGSPEdge head_candidate_findTripleCircle_edge__edge1 = directionRunCounterOf_findTripleCircle_edge__edge1==0 ? candidate_findTripleCircle_node_y.inhead : candidate_findTripleCircle_node_y.outhead;
                        if(head_candidate_findTripleCircle_edge__edge1 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_findTripleCircle_edge__edge1 = head_candidate_findTripleCircle_edge__edge1;
                            do
                            {
                                if(candidate_findTripleCircle_edge__edge1.type.TypeID!=2) {
                                    continue;
                                }
                                if((candidate_findTripleCircle_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                uint prev__candidate_findTripleCircle_edge__edge1;
                                prev__candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                candidate_findTripleCircle_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                // Implicit TheOther findTripleCircle_node_z from findTripleCircle_edge__edge1 
                                GRGEN_LGSP.LGSPNode candidate_findTripleCircle_node_z = candidate_findTripleCircle_node_y==candidate_findTripleCircle_edge__edge1.source ? candidate_findTripleCircle_edge__edge1.target : candidate_findTripleCircle_edge__edge1.source;
                                if((candidate_findTripleCircle_node_z.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    candidate_findTripleCircle_edge__edge1.flags = candidate_findTripleCircle_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge1;
                                    continue;
                                }
                                // both directions of findTripleCircle_edge__edge2
                                for(int directionRunCounterOf_findTripleCircle_edge__edge2 = 0; directionRunCounterOf_findTripleCircle_edge__edge2 < 2; ++directionRunCounterOf_findTripleCircle_edge__edge2)
                                {
                                    // Extend IncomingOrOutgoing findTripleCircle_edge__edge2 from findTripleCircle_node_z 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findTripleCircle_edge__edge2 = directionRunCounterOf_findTripleCircle_edge__edge2==0 ? candidate_findTripleCircle_node_z.inhead : candidate_findTripleCircle_node_z.outhead;
                                    if(head_candidate_findTripleCircle_edge__edge2 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findTripleCircle_edge__edge2 = head_candidate_findTripleCircle_edge__edge2;
                                        do
                                        {
                                            if(candidate_findTripleCircle_edge__edge2.type.TypeID!=2) {
                                                continue;
                                            }
                                            if( (candidate_findTripleCircle_node_z==candidate_findTripleCircle_edge__edge2.source ? candidate_findTripleCircle_edge__edge2.target : candidate_findTripleCircle_edge__edge2.source) != candidate_findTripleCircle_node_x) {
                                                continue;
                                            }
                                            if((candidate_findTripleCircle_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                                candidate_findTripleCircle_edge__edge1.flags = candidate_findTripleCircle_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge1;
                                                candidate_findTripleCircle_node_x.flags = candidate_findTripleCircle_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_x;
                                                candidate_findTripleCircle_node_y.flags = candidate_findTripleCircle_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_y;
                                                candidate_findTripleCircle_edge__edge0.flags = candidate_findTripleCircle_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge0;
                                                return matches;
                                            }
                                        }
                                        while( (directionRunCounterOf_findTripleCircle_edge__edge2==0 ? candidate_findTripleCircle_edge__edge2 = candidate_findTripleCircle_edge__edge2.inNext : candidate_findTripleCircle_edge__edge2 = candidate_findTripleCircle_edge__edge2.outNext) != head_candidate_findTripleCircle_edge__edge2 );
                                    }
                                }
                                candidate_findTripleCircle_edge__edge1.flags = candidate_findTripleCircle_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge1;
                            }
                            while( (directionRunCounterOf_findTripleCircle_edge__edge1==0 ? candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.inNext : candidate_findTripleCircle_edge__edge1 = candidate_findTripleCircle_edge__edge1.outNext) != head_candidate_findTripleCircle_edge__edge1 );
                        }
                    }
                    candidate_findTripleCircle_node_x.flags = candidate_findTripleCircle_node_x.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_x;
                    candidate_findTripleCircle_node_y.flags = candidate_findTripleCircle_node_y.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_node_y;
                }
                candidate_findTripleCircle_edge__edge0.flags = candidate_findTripleCircle_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findTripleCircle_edge__edge0;
label5: ;
            }
            return matches;
        }
    }


    public class edge1Actions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public edge1Actions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public edge1Actions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("init", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_init.Instance);
            actions.Add("init2", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_init2.Instance);
            actions.Add("init3", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_init3.Instance);
            actions.Add("findUndirectedEdge", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findUndirectedEdge.Instance);
            actions.Add("findArbitraryEdge", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findArbitraryEdge.Instance);
            actions.Add("findArbitraryDirectedEdge", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findArbitraryDirectedEdge.Instance);
            actions.Add("findArbitraryDirectedReflexiveEdge", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findArbitraryDirectedReflexiveEdge.Instance);
            actions.Add("findArbitraryDirectedReflexiveEdgeAfterUndirectedOne", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findArbitraryDirectedReflexiveEdgeAfterUndirectedOne.Instance);
            actions.Add("findArbitraryDirectedTriple", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findArbitraryDirectedTriple.Instance);
            actions.Add("findDirectedEdge", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findDirectedEdge.Instance);
            actions.Add("findTripleCircle", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findTripleCircle.Instance);
        }

        public override String Name { get { return "edge1Actions"; } }
        public override String ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}