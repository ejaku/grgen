// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\independent\Independent.grg" on Sun Jan 25 17:24:40 CET 2009

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using de.unika.ipd.grGen.Model_Independent;

namespace de.unika.ipd.grGen.Action_Independent
{
	public class Pattern_iteratedPath : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_iteratedPath instance = null;
		public static Pattern_iteratedPath Instance { get { if (instance==null) { instance = new Pattern_iteratedPath(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] iteratedPath_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] iteratedPath_node_end_AllowedTypes = null;
		public static bool[] iteratedPath_node_beg_IsAllowedType = null;
		public static bool[] iteratedPath_node_end_IsAllowedType = null;
		public enum iteratedPath_NodeNums { @beg, @end, };
		public enum iteratedPath_EdgeNums { };
		public enum iteratedPath_VariableNums { };
		public enum iteratedPath_SubNums { };
		public enum iteratedPath_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_iteratedPath;

		public enum iteratedPath_alt_0_CaseNums { @base, @recursive, };
		public static GRGEN_LIBGR.EdgeType[] iteratedPath_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPath_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum iteratedPath_alt_0_base_NodeNums { @beg, @end, };
		public enum iteratedPath_alt_0_base_EdgeNums { @_edge0, };
		public enum iteratedPath_alt_0_base_VariableNums { };
		public enum iteratedPath_alt_0_base_SubNums { };
		public enum iteratedPath_alt_0_base_AltNums { };


		GRGEN_LGSP.PatternGraph iteratedPath_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] iteratedPath_alt_0_recursive_node_intermediate_AllowedTypes = null;
		public static bool[] iteratedPath_alt_0_recursive_node_intermediate_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] iteratedPath_alt_0_recursive_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPath_alt_0_recursive_edge__edge0_IsAllowedType = null;
		public enum iteratedPath_alt_0_recursive_NodeNums { @beg, @intermediate, @end, };
		public enum iteratedPath_alt_0_recursive_EdgeNums { @_edge0, };
		public enum iteratedPath_alt_0_recursive_VariableNums { };
		public enum iteratedPath_alt_0_recursive_SubNums { @_subpattern0, };
		public enum iteratedPath_alt_0_recursive_AltNums { };


		GRGEN_LGSP.PatternGraph iteratedPath_alt_0_recursive;


		private Pattern_iteratedPath()
		{
			name = "iteratedPath";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "iteratedPath_node_beg", "iteratedPath_node_end", };
		}
		private void initialize()
		{
			bool[,] iteratedPath_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPath_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode iteratedPath_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "iteratedPath_node_beg", "beg", iteratedPath_node_beg_AllowedTypes, iteratedPath_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode iteratedPath_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "iteratedPath_node_end", "end", iteratedPath_node_end_AllowedTypes, iteratedPath_node_end_IsAllowedType, 5.5F, 1);
			bool[,] iteratedPath_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPath_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge iteratedPath_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "iteratedPath_alt_0_base_edge__edge0", "_edge0", iteratedPath_alt_0_base_edge__edge0_AllowedTypes, iteratedPath_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			iteratedPath_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"iteratedPath_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { iteratedPath_node_beg, iteratedPath_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPath_alt_0_base_edge__edge0 }, 
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
				iteratedPath_alt_0_base_isNodeHomomorphicGlobal,
				iteratedPath_alt_0_base_isEdgeHomomorphicGlobal
			);
			iteratedPath_alt_0_base.edgeToSourceNode.Add(iteratedPath_alt_0_base_edge__edge0, iteratedPath_node_beg);
			iteratedPath_alt_0_base.edgeToTargetNode.Add(iteratedPath_alt_0_base_edge__edge0, iteratedPath_node_end);

			bool[,] iteratedPath_alt_0_recursive_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] iteratedPath_alt_0_recursive_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode iteratedPath_alt_0_recursive_node_intermediate = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "iteratedPath_alt_0_recursive_node_intermediate", "intermediate", iteratedPath_alt_0_recursive_node_intermediate_AllowedTypes, iteratedPath_alt_0_recursive_node_intermediate_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge iteratedPath_alt_0_recursive_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "iteratedPath_alt_0_recursive_edge__edge0", "_edge0", iteratedPath_alt_0_recursive_edge__edge0_AllowedTypes, iteratedPath_alt_0_recursive_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding iteratedPath_alt_0_recursive__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_iteratedPath.Instance, new GRGEN_LGSP.PatternElement[] { iteratedPath_alt_0_recursive_node_intermediate, iteratedPath_node_end });
			iteratedPath_alt_0_recursive = new GRGEN_LGSP.PatternGraph(
				"recursive",
				"iteratedPath_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { iteratedPath_node_beg, iteratedPath_alt_0_recursive_node_intermediate, iteratedPath_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPath_alt_0_recursive_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { iteratedPath_alt_0_recursive__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				iteratedPath_alt_0_recursive_isNodeHomomorphicGlobal,
				iteratedPath_alt_0_recursive_isEdgeHomomorphicGlobal
			);
			iteratedPath_alt_0_recursive.edgeToSourceNode.Add(iteratedPath_alt_0_recursive_edge__edge0, iteratedPath_node_beg);
			iteratedPath_alt_0_recursive.edgeToTargetNode.Add(iteratedPath_alt_0_recursive_edge__edge0, iteratedPath_alt_0_recursive_node_intermediate);

			GRGEN_LGSP.Alternative iteratedPath_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "iteratedPath_", new GRGEN_LGSP.PatternGraph[] { iteratedPath_alt_0_base, iteratedPath_alt_0_recursive } );

			pat_iteratedPath = new GRGEN_LGSP.PatternGraph(
				"iteratedPath",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { iteratedPath_node_beg, iteratedPath_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { iteratedPath_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				iteratedPath_isNodeHomomorphicGlobal,
				iteratedPath_isEdgeHomomorphicGlobal
			);
			iteratedPath_alt_0_base.embeddingGraph = pat_iteratedPath;
			iteratedPath_alt_0_recursive.embeddingGraph = pat_iteratedPath;

			iteratedPath_node_beg.PointOfDefinition = null;
			iteratedPath_node_end.PointOfDefinition = null;
			iteratedPath_alt_0_base_edge__edge0.PointOfDefinition = iteratedPath_alt_0_base;
			iteratedPath_alt_0_recursive_node_intermediate.PointOfDefinition = iteratedPath_alt_0_recursive;
			iteratedPath_alt_0_recursive_edge__edge0.PointOfDefinition = iteratedPath_alt_0_recursive;
			iteratedPath_alt_0_recursive__subpattern0.PointOfDefinition = iteratedPath_alt_0_recursive;

			patternGraph = pat_iteratedPath;
		}


		public void iteratedPath_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_beg, GRGEN_LGSP.LGSPNode node_end)
		{
			graph.SettingAddedNodeNames( create_iteratedPath_addedNodeNames );
			graph.SettingAddedEdgeNames( create_iteratedPath_addedEdgeNames );
		}
		private static String[] create_iteratedPath_addedNodeNames = new String[] {  };
		private static String[] create_iteratedPath_addedEdgeNames = new String[] {  };

		public void iteratedPath_Delete(GRGEN_LGSP.LGSPGraph graph, Match_iteratedPath curMatch)
		{
			IMatch_iteratedPath_alt_0 alternative_alt_0 = curMatch._alt_0;
			iteratedPath_alt_0_Delete(graph, alternative_alt_0);
		}

		public void iteratedPath_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_iteratedPath_alt_0 curMatch)
		{
			if(curMatch.Pattern == iteratedPath_alt_0_base) {
				iteratedPath_alt_0_base_Delete(graph, (Match_iteratedPath_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == iteratedPath_alt_0_recursive) {
				iteratedPath_alt_0_recursive_Delete(graph, (Match_iteratedPath_alt_0_recursive)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void iteratedPath_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_iteratedPath_alt_0_base_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_iteratedPath_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node_end);
		}
		private static String[] create_iteratedPath_alt_0_base_addedNodeNames = new String[] { "beg", "end" };
		private static String[] create_iteratedPath_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void iteratedPath_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_iteratedPath_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
		}

		public void iteratedPath_alt_0_recursive_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_iteratedPath_alt_0_recursive_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@Node node_intermediate = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_iteratedPath_alt_0_recursive_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node_intermediate);
			Pattern_iteratedPath.Instance.iteratedPath_Create(graph, node_intermediate, node_end);
		}
		private static String[] create_iteratedPath_alt_0_recursive_addedNodeNames = new String[] { "beg", "intermediate", "end" };
		private static String[] create_iteratedPath_alt_0_recursive_addedEdgeNames = new String[] { "_edge0" };

		public void iteratedPath_alt_0_recursive_Delete(GRGEN_LGSP.LGSPGraph graph, Match_iteratedPath_alt_0_recursive curMatch)
		{
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_iteratedPath.Match_iteratedPath subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_iteratedPath.Instance.iteratedPath_Delete(graph, subpattern__subpattern0);
		}

		static Pattern_iteratedPath() {
		}

		public interface IMatch_iteratedPath : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_iteratedPath_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_iteratedPath_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_iteratedPath_alt_0_base : IMatch_iteratedPath_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_iteratedPath_alt_0_recursive : IMatch_iteratedPath_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_intermediate { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPath.Match_iteratedPath @_subpattern0 { get; }
			//Alternatives
			//Independents
		}

		public class Match_iteratedPath : GRGEN_LGSP.ListElement<Match_iteratedPath>, IMatch_iteratedPath
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPath_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_NodeNums.@beg: return _node_beg;
				case (int)iteratedPath_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum iteratedPath_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPath_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPath_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_iteratedPath_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_iteratedPath_alt_0 _alt_0;
			public enum iteratedPath_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum iteratedPath_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPath.instance.pat_iteratedPath; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_iteratedPath_alt_0_base : GRGEN_LGSP.ListElement<Match_iteratedPath_alt_0_base>, IMatch_iteratedPath_alt_0_base
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPath_alt_0_base_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_base_NodeNums.@beg: return _node_beg;
				case (int)iteratedPath_alt_0_base_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPath_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum iteratedPath_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPath_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPath_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPath_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPath.instance.iteratedPath_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_iteratedPath_alt_0_recursive : GRGEN_LGSP.ListElement<Match_iteratedPath_alt_0_recursive>, IMatch_iteratedPath_alt_0_recursive
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPath_alt_0_recursive_NodeNums { @beg, @intermediate, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_recursive_NodeNums.@beg: return _node_beg;
				case (int)iteratedPath_alt_0_recursive_NodeNums.@intermediate: return _node_intermediate;
				case (int)iteratedPath_alt_0_recursive_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPath_alt_0_recursive_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_recursive_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum iteratedPath_alt_0_recursive_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_iteratedPath.Match_iteratedPath @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_iteratedPath.Match_iteratedPath @__subpattern0;
			public enum iteratedPath_alt_0_recursive_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)iteratedPath_alt_0_recursive_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum iteratedPath_alt_0_recursive_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPath_alt_0_recursive_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPath.instance.iteratedPath_alt_0_recursive; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_iteratedPathToIntNode : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_iteratedPathToIntNode instance = null;
		public static Pattern_iteratedPathToIntNode Instance { get { if (instance==null) { instance = new Pattern_iteratedPathToIntNode(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] iteratedPathToIntNode_node_beg_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_node_beg_IsAllowedType = null;
		public enum iteratedPathToIntNode_NodeNums { @beg, };
		public enum iteratedPathToIntNode_EdgeNums { };
		public enum iteratedPathToIntNode_VariableNums { };
		public enum iteratedPathToIntNode_SubNums { };
		public enum iteratedPathToIntNode_AltNums { @alt_0, };


		GRGEN_LGSP.PatternGraph pat_iteratedPathToIntNode;

		public enum iteratedPathToIntNode_alt_0_CaseNums { @base, @recursive, };
		public static GRGEN_LIBGR.NodeType[] iteratedPathToIntNode_alt_0_base_node_end_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_alt_0_base_node_end_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] iteratedPathToIntNode_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum iteratedPathToIntNode_alt_0_base_NodeNums { @beg, @end, };
		public enum iteratedPathToIntNode_alt_0_base_EdgeNums { @_edge0, };
		public enum iteratedPathToIntNode_alt_0_base_VariableNums { };
		public enum iteratedPathToIntNode_alt_0_base_SubNums { };
		public enum iteratedPathToIntNode_alt_0_base_AltNums { };


		GRGEN_LGSP.PatternGraph iteratedPathToIntNode_alt_0_base;

		public static GRGEN_LIBGR.NodeType[] iteratedPathToIntNode_alt_0_recursive_node_intermediate_AllowedTypes = { NodeType_Node.typeVar, };
		public static bool[] iteratedPathToIntNode_alt_0_recursive_node_intermediate_IsAllowedType = { true, false, };
		public static GRGEN_LIBGR.EdgeType[] iteratedPathToIntNode_alt_0_recursive_edge__edge0_AllowedTypes = null;
		public static bool[] iteratedPathToIntNode_alt_0_recursive_edge__edge0_IsAllowedType = null;
		public enum iteratedPathToIntNode_alt_0_recursive_NodeNums { @beg, @intermediate, };
		public enum iteratedPathToIntNode_alt_0_recursive_EdgeNums { @_edge0, };
		public enum iteratedPathToIntNode_alt_0_recursive_VariableNums { };
		public enum iteratedPathToIntNode_alt_0_recursive_SubNums { @_subpattern0, };
		public enum iteratedPathToIntNode_alt_0_recursive_AltNums { };


		GRGEN_LGSP.PatternGraph iteratedPathToIntNode_alt_0_recursive;


		private Pattern_iteratedPathToIntNode()
		{
			name = "iteratedPathToIntNode";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "iteratedPathToIntNode_node_beg", };
		}
		private void initialize()
		{
			bool[,] iteratedPathToIntNode_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] iteratedPathToIntNode_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode iteratedPathToIntNode_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "iteratedPathToIntNode_node_beg", "beg", iteratedPathToIntNode_node_beg_AllowedTypes, iteratedPathToIntNode_node_beg_IsAllowedType, 5.5F, 0);
			bool[,] iteratedPathToIntNode_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPathToIntNode_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode iteratedPathToIntNode_alt_0_base_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@intNode, "IintNode", "iteratedPathToIntNode_alt_0_base_node_end", "end", iteratedPathToIntNode_alt_0_base_node_end_AllowedTypes, iteratedPathToIntNode_alt_0_base_node_end_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge iteratedPathToIntNode_alt_0_base_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "iteratedPathToIntNode_alt_0_base_edge__edge0", "_edge0", iteratedPathToIntNode_alt_0_base_edge__edge0_AllowedTypes, iteratedPathToIntNode_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			iteratedPathToIntNode_alt_0_base = new GRGEN_LGSP.PatternGraph(
				"base",
				"iteratedPathToIntNode_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { iteratedPathToIntNode_node_beg, iteratedPathToIntNode_alt_0_base_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPathToIntNode_alt_0_base_edge__edge0 }, 
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
				iteratedPathToIntNode_alt_0_base_isNodeHomomorphicGlobal,
				iteratedPathToIntNode_alt_0_base_isEdgeHomomorphicGlobal
			);
			iteratedPathToIntNode_alt_0_base.edgeToSourceNode.Add(iteratedPathToIntNode_alt_0_base_edge__edge0, iteratedPathToIntNode_node_beg);
			iteratedPathToIntNode_alt_0_base.edgeToTargetNode.Add(iteratedPathToIntNode_alt_0_base_edge__edge0, iteratedPathToIntNode_alt_0_base_node_end);

			bool[,] iteratedPathToIntNode_alt_0_recursive_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] iteratedPathToIntNode_alt_0_recursive_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode iteratedPathToIntNode_alt_0_recursive_node_intermediate = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "iteratedPathToIntNode_alt_0_recursive_node_intermediate", "intermediate", iteratedPathToIntNode_alt_0_recursive_node_intermediate_AllowedTypes, iteratedPathToIntNode_alt_0_recursive_node_intermediate_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge iteratedPathToIntNode_alt_0_recursive_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "iteratedPathToIntNode_alt_0_recursive_edge__edge0", "_edge0", iteratedPathToIntNode_alt_0_recursive_edge__edge0_AllowedTypes, iteratedPathToIntNode_alt_0_recursive_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding iteratedPathToIntNode_alt_0_recursive__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_iteratedPathToIntNode.Instance, new GRGEN_LGSP.PatternElement[] { iteratedPathToIntNode_alt_0_recursive_node_intermediate });
			iteratedPathToIntNode_alt_0_recursive = new GRGEN_LGSP.PatternGraph(
				"recursive",
				"iteratedPathToIntNode_alt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { iteratedPathToIntNode_node_beg, iteratedPathToIntNode_alt_0_recursive_node_intermediate }, 
				new GRGEN_LGSP.PatternEdge[] { iteratedPathToIntNode_alt_0_recursive_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { iteratedPathToIntNode_alt_0_recursive__subpattern0 }, 
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
				iteratedPathToIntNode_alt_0_recursive_isNodeHomomorphicGlobal,
				iteratedPathToIntNode_alt_0_recursive_isEdgeHomomorphicGlobal
			);
			iteratedPathToIntNode_alt_0_recursive.edgeToSourceNode.Add(iteratedPathToIntNode_alt_0_recursive_edge__edge0, iteratedPathToIntNode_node_beg);
			iteratedPathToIntNode_alt_0_recursive.edgeToTargetNode.Add(iteratedPathToIntNode_alt_0_recursive_edge__edge0, iteratedPathToIntNode_alt_0_recursive_node_intermediate);

			GRGEN_LGSP.Alternative iteratedPathToIntNode_alt_0 = new GRGEN_LGSP.Alternative( "alt_0", "iteratedPathToIntNode_", new GRGEN_LGSP.PatternGraph[] { iteratedPathToIntNode_alt_0_base, iteratedPathToIntNode_alt_0_recursive } );

			pat_iteratedPathToIntNode = new GRGEN_LGSP.PatternGraph(
				"iteratedPathToIntNode",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { iteratedPathToIntNode_node_beg }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] { iteratedPathToIntNode_alt_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				iteratedPathToIntNode_isNodeHomomorphicGlobal,
				iteratedPathToIntNode_isEdgeHomomorphicGlobal
			);
			iteratedPathToIntNode_alt_0_base.embeddingGraph = pat_iteratedPathToIntNode;
			iteratedPathToIntNode_alt_0_recursive.embeddingGraph = pat_iteratedPathToIntNode;

			iteratedPathToIntNode_node_beg.PointOfDefinition = null;
			iteratedPathToIntNode_alt_0_base_node_end.PointOfDefinition = iteratedPathToIntNode_alt_0_base;
			iteratedPathToIntNode_alt_0_base_edge__edge0.PointOfDefinition = iteratedPathToIntNode_alt_0_base;
			iteratedPathToIntNode_alt_0_recursive_node_intermediate.PointOfDefinition = iteratedPathToIntNode_alt_0_recursive;
			iteratedPathToIntNode_alt_0_recursive_edge__edge0.PointOfDefinition = iteratedPathToIntNode_alt_0_recursive;
			iteratedPathToIntNode_alt_0_recursive__subpattern0.PointOfDefinition = iteratedPathToIntNode_alt_0_recursive;

			patternGraph = pat_iteratedPathToIntNode;
		}


		public void iteratedPathToIntNode_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_beg)
		{
			graph.SettingAddedNodeNames( create_iteratedPathToIntNode_addedNodeNames );
			graph.SettingAddedEdgeNames( create_iteratedPathToIntNode_addedEdgeNames );
		}
		private static String[] create_iteratedPathToIntNode_addedNodeNames = new String[] {  };
		private static String[] create_iteratedPathToIntNode_addedEdgeNames = new String[] {  };

		public void iteratedPathToIntNode_Delete(GRGEN_LGSP.LGSPGraph graph, Match_iteratedPathToIntNode curMatch)
		{
			IMatch_iteratedPathToIntNode_alt_0 alternative_alt_0 = curMatch._alt_0;
			iteratedPathToIntNode_alt_0_Delete(graph, alternative_alt_0);
		}

		public void iteratedPathToIntNode_alt_0_Delete(GRGEN_LGSP.LGSPGraph graph, IMatch_iteratedPathToIntNode_alt_0 curMatch)
		{
			if(curMatch.Pattern == iteratedPathToIntNode_alt_0_base) {
				iteratedPathToIntNode_alt_0_base_Delete(graph, (Match_iteratedPathToIntNode_alt_0_base)curMatch);
				return;
			}
			else if(curMatch.Pattern == iteratedPathToIntNode_alt_0_recursive) {
				iteratedPathToIntNode_alt_0_recursive_Delete(graph, (Match_iteratedPathToIntNode_alt_0_recursive)curMatch);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void iteratedPathToIntNode_alt_0_base_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_iteratedPathToIntNode_alt_0_base_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@intNode node_end = @intNode.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_iteratedPathToIntNode_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node_end);
		}
		private static String[] create_iteratedPathToIntNode_alt_0_base_addedNodeNames = new String[] { "beg", "end" };
		private static String[] create_iteratedPathToIntNode_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void iteratedPathToIntNode_alt_0_base_Delete(GRGEN_LGSP.LGSPGraph graph, Match_iteratedPathToIntNode_alt_0_base curMatch)
		{
			GRGEN_LGSP.LGSPNode node_end = curMatch._node_end;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_end);
			graph.Remove(node_end);
		}

		public void iteratedPathToIntNode_alt_0_recursive_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_iteratedPathToIntNode_alt_0_recursive_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@Node node_intermediate = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_iteratedPathToIntNode_alt_0_recursive_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node_intermediate);
			Pattern_iteratedPathToIntNode.Instance.iteratedPathToIntNode_Create(graph, node_intermediate);
		}
		private static String[] create_iteratedPathToIntNode_alt_0_recursive_addedNodeNames = new String[] { "beg", "intermediate" };
		private static String[] create_iteratedPathToIntNode_alt_0_recursive_addedEdgeNames = new String[] { "_edge0" };

		public void iteratedPathToIntNode_alt_0_recursive_Delete(GRGEN_LGSP.LGSPGraph graph, Match_iteratedPathToIntNode_alt_0_recursive curMatch)
		{
			GRGEN_LGSP.LGSPNode node_intermediate = curMatch._node_intermediate;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_iteratedPathToIntNode.Instance.iteratedPathToIntNode_Delete(graph, subpattern__subpattern0);
		}

		static Pattern_iteratedPathToIntNode() {
		}

		public interface IMatch_iteratedPathToIntNode : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			IMatch_iteratedPathToIntNode_alt_0 alt_0 { get; }
			//Independents
		}

		public interface IMatch_iteratedPathToIntNode_alt_0 : GRGEN_LIBGR.IMatch
		{
		}

		public interface IMatch_iteratedPathToIntNode_alt_0_base : IMatch_iteratedPathToIntNode_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			IintNode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_iteratedPathToIntNode_alt_0_recursive : IMatch_iteratedPathToIntNode_alt_0
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_intermediate { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_subpattern0 { get; }
			//Alternatives
			//Independents
		}

		public class Match_iteratedPathToIntNode : GRGEN_LGSP.ListElement<Match_iteratedPathToIntNode>, IMatch_iteratedPathToIntNode
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public enum iteratedPathToIntNode_NodeNums { @beg, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_NodeNums.@beg: return _node_beg;
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_iteratedPathToIntNode_alt_0 alt_0 { get { return _alt_0; } }
			public IMatch_iteratedPathToIntNode_alt_0 _alt_0;
			public enum iteratedPathToIntNode_AltNums { @alt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 1;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_AltNums.@alt_0: return _alt_0;
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPathToIntNode.instance.pat_iteratedPathToIntNode; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_iteratedPathToIntNode_alt_0_base : GRGEN_LGSP.ListElement<Match_iteratedPathToIntNode_alt_0_base>, IMatch_iteratedPathToIntNode_alt_0_base
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public IintNode node_end { get { return (IintNode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum iteratedPathToIntNode_alt_0_base_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_base_NodeNums.@beg: return _node_beg;
				case (int)iteratedPathToIntNode_alt_0_base_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPathToIntNode_alt_0_base_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_base_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_alt_0_base_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_alt_0_base_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_alt_0_base_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_alt_0_base_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPathToIntNode.instance.iteratedPathToIntNode_alt_0_base; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_iteratedPathToIntNode_alt_0_recursive : GRGEN_LGSP.ListElement<Match_iteratedPathToIntNode_alt_0_recursive>, IMatch_iteratedPathToIntNode_alt_0_recursive
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_intermediate { get { return (GRGEN_LIBGR.INode)_node_intermediate; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_intermediate;
			public enum iteratedPathToIntNode_alt_0_recursive_NodeNums { @beg, @intermediate, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_recursive_NodeNums.@beg: return _node_beg;
				case (int)iteratedPathToIntNode_alt_0_recursive_NodeNums.@intermediate: return _node_intermediate;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum iteratedPathToIntNode_alt_0_recursive_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_recursive_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_alt_0_recursive_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @__subpattern0;
			public enum iteratedPathToIntNode_alt_0_recursive_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)iteratedPathToIntNode_alt_0_recursive_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_alt_0_recursive_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum iteratedPathToIntNode_alt_0_recursive_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_iteratedPathToIntNode.instance.iteratedPathToIntNode_alt_0_recursive; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_create : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_create instance = null;
		public static Rule_create Instance { get { if (instance==null) { instance = new Rule_create(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[2];

		public enum create_NodeNums { };
		public enum create_EdgeNums { };
		public enum create_VariableNums { };
		public enum create_SubNums { };
		public enum create_AltNums { };



		GRGEN_LGSP.PatternGraph pat_create;


		private Rule_create()
		{
			name = "create";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] create_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] create_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_create = new GRGEN_LGSP.PatternGraph(
				"create",
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
				create_isNodeHomomorphicGlobal,
				create_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_create;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_create curMatch = (Match_create)_curMatch;
			graph.SettingAddedNodeNames( create_addedNodeNames );
			@Node node_n1 = @Node.CreateNode(graph);
			@Node node_n2 = @Node.CreateNode(graph);
			@Node node_n3 = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_n1, node_n2);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_n2, node_n3);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_n2, node_n1);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_n3, node_n2);
			ReturnArray[0] = node_n1;
			ReturnArray[1] = node_n3;
			return ReturnArray;
		}
		private static String[] create_addedNodeNames = new String[] { "n1", "n2", "n3" };
		private static String[] create_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_create curMatch = (Match_create)_curMatch;
			graph.SettingAddedNodeNames( create_addedNodeNames );
			@Node node_n1 = @Node.CreateNode(graph);
			@Node node_n2 = @Node.CreateNode(graph);
			@Node node_n3 = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_n1, node_n2);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_n2, node_n3);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_n2, node_n1);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_n3, node_n2);
			ReturnArray[0] = node_n1;
			ReturnArray[1] = node_n3;
			return ReturnArray;
		}

		static Rule_create() {
		}

		public interface IMatch_create : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_create : GRGEN_LGSP.ListElement<Match_create>, IMatch_create
		{
			public enum create_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum create_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum create_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum create_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum create_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum create_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_create.instance.pat_create; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_find : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_find instance = null;
		public static Rule_find Instance { get { if (instance==null) { instance = new Rule_find(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] find_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] find_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] find_node_end_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] find_node__node1_AllowedTypes = null;
		public static bool[] find_node_beg_IsAllowedType = null;
		public static bool[] find_node__node0_IsAllowedType = null;
		public static bool[] find_node_end_IsAllowedType = null;
		public static bool[] find_node__node1_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge2_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] find_edge__edge3_AllowedTypes = null;
		public static bool[] find_edge__edge0_IsAllowedType = null;
		public static bool[] find_edge__edge1_IsAllowedType = null;
		public static bool[] find_edge__edge2_IsAllowedType = null;
		public static bool[] find_edge__edge3_IsAllowedType = null;
		public enum find_NodeNums { @beg, @_node0, @end, @_node1, };
		public enum find_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, };
		public enum find_VariableNums { };
		public enum find_SubNums { };
		public enum find_AltNums { };


		GRGEN_LGSP.PatternGraph pat_find;


		private Rule_find()
		{
			name = "find";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] find_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] find_isEdgeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			GRGEN_LGSP.PatternNode find_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "find_node_beg", "beg", find_node_beg_AllowedTypes, find_node_beg_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode find_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "find_node__node0", "_node0", find_node__node0_AllowedTypes, find_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode find_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "find_node_end", "end", find_node_end_AllowedTypes, find_node_end_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode find_node__node1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "find_node__node1", "_node1", find_node__node1_AllowedTypes, find_node__node1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge find_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "find_edge__edge0", "_edge0", find_edge__edge0_AllowedTypes, find_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge find_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "find_edge__edge1", "_edge1", find_edge__edge1_AllowedTypes, find_edge__edge1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge find_edge__edge2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "find_edge__edge2", "_edge2", find_edge__edge2_AllowedTypes, find_edge__edge2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge find_edge__edge3 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "find_edge__edge3", "_edge3", find_edge__edge3_AllowedTypes, find_edge__edge3_IsAllowedType, 5.5F, -1);
			pat_find = new GRGEN_LGSP.PatternGraph(
				"find",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { find_node_beg, find_node__node0, find_node_end, find_node__node1 }, 
				new GRGEN_LGSP.PatternEdge[] { find_edge__edge0, find_edge__edge1, find_edge__edge2, find_edge__edge3 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				find_isNodeHomomorphicGlobal,
				find_isEdgeHomomorphicGlobal
			);
			pat_find.edgeToSourceNode.Add(find_edge__edge0, find_node_beg);
			pat_find.edgeToTargetNode.Add(find_edge__edge0, find_node__node0);
			pat_find.edgeToSourceNode.Add(find_edge__edge1, find_node__node0);
			pat_find.edgeToTargetNode.Add(find_edge__edge1, find_node_end);
			pat_find.edgeToSourceNode.Add(find_edge__edge2, find_node__node1);
			pat_find.edgeToTargetNode.Add(find_edge__edge2, find_node_beg);
			pat_find.edgeToSourceNode.Add(find_edge__edge3, find_node_end);
			pat_find.edgeToTargetNode.Add(find_edge__edge3, find_node__node1);

			find_node_beg.PointOfDefinition = pat_find;
			find_node__node0.PointOfDefinition = pat_find;
			find_node_end.PointOfDefinition = pat_find;
			find_node__node1.PointOfDefinition = pat_find;
			find_edge__edge0.PointOfDefinition = pat_find;
			find_edge__edge1.PointOfDefinition = pat_find;
			find_edge__edge2.PointOfDefinition = pat_find;
			find_edge__edge3.PointOfDefinition = pat_find;

			patternGraph = pat_find;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_find curMatch = (Match_find)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_find curMatch = (Match_find)_curMatch;
			return EmptyReturnElements;
		}

		static Rule_find() {
		}

		public interface IMatch_find : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_end { get; }
			GRGEN_LIBGR.INode node__node1 { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			GRGEN_LIBGR.IEdge edge__edge2 { get; }
			GRGEN_LIBGR.IEdge edge__edge3 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_find : GRGEN_LGSP.ListElement<Match_find>, IMatch_find
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LIBGR.INode node__node1 { get { return (GRGEN_LIBGR.INode)_node__node1; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public GRGEN_LGSP.LGSPNode _node__node1;
			public enum find_NodeNums { @beg, @_node0, @end, @_node1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)find_NodeNums.@beg: return _node_beg;
				case (int)find_NodeNums.@_node0: return _node__node0;
				case (int)find_NodeNums.@end: return _node_end;
				case (int)find_NodeNums.@_node1: return _node__node1;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LIBGR.IEdge edge__edge2 { get { return (GRGEN_LIBGR.IEdge)_edge__edge2; } }
			public GRGEN_LIBGR.IEdge edge__edge3 { get { return (GRGEN_LIBGR.IEdge)_edge__edge3; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public GRGEN_LGSP.LGSPEdge _edge__edge2;
			public GRGEN_LGSP.LGSPEdge _edge__edge3;
			public enum find_EdgeNums { @_edge0, @_edge1, @_edge2, @_edge3, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 4;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)find_EdgeNums.@_edge0: return _edge__edge0;
				case (int)find_EdgeNums.@_edge1: return _edge__edge1;
				case (int)find_EdgeNums.@_edge2: return _edge__edge2;
				case (int)find_EdgeNums.@_edge3: return _edge__edge3;
				default: return null;
				}
			}
			
			public enum find_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum find_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum find_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum find_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_find.instance.pat_find; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findIndependent : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findIndependent instance = null;
		public static Rule_findIndependent Instance { get { if (instance==null) { instance = new Rule_findIndependent(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] findIndependent_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findIndependent_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findIndependent_node_end_AllowedTypes = null;
		public static bool[] findIndependent_node_beg_IsAllowedType = null;
		public static bool[] findIndependent_node__node0_IsAllowedType = null;
		public static bool[] findIndependent_node_end_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_edge__edge1_AllowedTypes = null;
		public static bool[] findIndependent_edge__edge0_IsAllowedType = null;
		public static bool[] findIndependent_edge__edge1_IsAllowedType = null;
		public enum findIndependent_NodeNums { @beg, @_node0, @end, };
		public enum findIndependent_EdgeNums { @_edge0, @_edge1, };
		public enum findIndependent_VariableNums { };
		public enum findIndependent_SubNums { };
		public enum findIndependent_AltNums { };


		GRGEN_LGSP.PatternGraph pat_findIndependent;

		public static GRGEN_LIBGR.NodeType[] findIndependent_idpt_0_node__node0_AllowedTypes = null;
		public static bool[] findIndependent_idpt_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_idpt_0_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findIndependent_idpt_0_edge__edge1_AllowedTypes = null;
		public static bool[] findIndependent_idpt_0_edge__edge0_IsAllowedType = null;
		public static bool[] findIndependent_idpt_0_edge__edge1_IsAllowedType = null;
		public enum findIndependent_idpt_0_NodeNums { @_node0, @beg, @end, };
		public enum findIndependent_idpt_0_EdgeNums { @_edge0, @_edge1, };
		public enum findIndependent_idpt_0_VariableNums { };
		public enum findIndependent_idpt_0_SubNums { };
		public enum findIndependent_idpt_0_AltNums { };

		GRGEN_LGSP.PatternGraph findIndependent_idpt_0;


		private Rule_findIndependent()
		{
			name = "findIndependent";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findIndependent_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findIndependent_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findIndependent_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findIndependent_node_beg", "beg", findIndependent_node_beg_AllowedTypes, findIndependent_node_beg_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findIndependent_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findIndependent_node__node0", "_node0", findIndependent_node__node0_AllowedTypes, findIndependent_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findIndependent_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findIndependent_node_end", "end", findIndependent_node_end_AllowedTypes, findIndependent_node_end_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findIndependent_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findIndependent_edge__edge0", "_edge0", findIndependent_edge__edge0_AllowedTypes, findIndependent_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findIndependent_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findIndependent_edge__edge1", "_edge1", findIndependent_edge__edge1_AllowedTypes, findIndependent_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] findIndependent_idpt_0_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findIndependent_idpt_0_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findIndependent_idpt_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findIndependent_idpt_0_node__node0", "_node0", findIndependent_idpt_0_node__node0_AllowedTypes, findIndependent_idpt_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findIndependent_idpt_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findIndependent_idpt_0_edge__edge0", "_edge0", findIndependent_idpt_0_edge__edge0_AllowedTypes, findIndependent_idpt_0_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findIndependent_idpt_0_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findIndependent_idpt_0_edge__edge1", "_edge1", findIndependent_idpt_0_edge__edge1_AllowedTypes, findIndependent_idpt_0_edge__edge1_IsAllowedType, 5.5F, -1);
			findIndependent_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findIndependent_",
				false,
				new GRGEN_LGSP.PatternNode[] { findIndependent_idpt_0_node__node0, findIndependent_node_beg, findIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findIndependent_idpt_0_edge__edge0, findIndependent_idpt_0_edge__edge1 }, 
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
				findIndependent_idpt_0_isNodeHomomorphicGlobal,
				findIndependent_idpt_0_isEdgeHomomorphicGlobal
			);
			findIndependent_idpt_0.edgeToSourceNode.Add(findIndependent_idpt_0_edge__edge0, findIndependent_idpt_0_node__node0);
			findIndependent_idpt_0.edgeToTargetNode.Add(findIndependent_idpt_0_edge__edge0, findIndependent_node_beg);
			findIndependent_idpt_0.edgeToSourceNode.Add(findIndependent_idpt_0_edge__edge1, findIndependent_node_end);
			findIndependent_idpt_0.edgeToTargetNode.Add(findIndependent_idpt_0_edge__edge1, findIndependent_idpt_0_node__node0);

			pat_findIndependent = new GRGEN_LGSP.PatternGraph(
				"findIndependent",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findIndependent_node_beg, findIndependent_node__node0, findIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findIndependent_edge__edge0, findIndependent_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findIndependent_idpt_0,  }, 
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
				findIndependent_isNodeHomomorphicGlobal,
				findIndependent_isEdgeHomomorphicGlobal
			);
			pat_findIndependent.edgeToSourceNode.Add(findIndependent_edge__edge0, findIndependent_node_beg);
			pat_findIndependent.edgeToTargetNode.Add(findIndependent_edge__edge0, findIndependent_node__node0);
			pat_findIndependent.edgeToSourceNode.Add(findIndependent_edge__edge1, findIndependent_node__node0);
			pat_findIndependent.edgeToTargetNode.Add(findIndependent_edge__edge1, findIndependent_node_end);
			findIndependent_idpt_0.embeddingGraph = pat_findIndependent;

			findIndependent_node_beg.PointOfDefinition = pat_findIndependent;
			findIndependent_node__node0.PointOfDefinition = pat_findIndependent;
			findIndependent_node_end.PointOfDefinition = pat_findIndependent;
			findIndependent_edge__edge0.PointOfDefinition = pat_findIndependent;
			findIndependent_edge__edge1.PointOfDefinition = pat_findIndependent;
			findIndependent_idpt_0_node__node0.PointOfDefinition = findIndependent_idpt_0;
			findIndependent_idpt_0_edge__edge0.PointOfDefinition = findIndependent_idpt_0;
			findIndependent_idpt_0_edge__edge1.PointOfDefinition = findIndependent_idpt_0;

			patternGraph = pat_findIndependent;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findIndependent curMatch = (Match_findIndependent)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findIndependent curMatch = (Match_findIndependent)_curMatch;
			return EmptyReturnElements;
		}

		static Rule_findIndependent() {
		}

		public interface IMatch_findIndependent : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
			IMatch_findIndependent_idpt_0 idpt_0 { get; }
		}

		public interface IMatch_findIndependent_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_findIndependent : GRGEN_LGSP.ListElement<Match_findIndependent>, IMatch_findIndependent
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findIndependent_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_NodeNums.@beg: return _node_beg;
				case (int)findIndependent_NodeNums.@_node0: return _node__node0;
				case (int)findIndependent_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findIndependent_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findIndependent_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findIndependent_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findIndependent_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findIndependent_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_findIndependent_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findIndependent_idpt_0 _idpt_0;
			public enum findIndependent_IdptNums { @idpt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findIndependent_IdptNums.@idpt_0: return _idpt_0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findIndependent.instance.pat_findIndependent; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_findIndependent_idpt_0 : GRGEN_LGSP.ListElement<Match_findIndependent_idpt_0>, IMatch_findIndependent_idpt_0
		{
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findIndependent_idpt_0_NodeNums { @_node0, @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_idpt_0_NodeNums.@_node0: return _node__node0;
				case (int)findIndependent_idpt_0_NodeNums.@beg: return _node_beg;
				case (int)findIndependent_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findIndependent_idpt_0_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findIndependent_idpt_0_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findIndependent_idpt_0_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findIndependent_idpt_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findIndependent_idpt_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findIndependent_idpt_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findIndependent_idpt_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findIndependent.instance.findIndependent_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findMultiNested : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findMultiNested instance = null;
		public static Rule_findMultiNested Instance { get { if (instance==null) { instance = new Rule_findMultiNested(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] findMultiNested_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findMultiNested_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findMultiNested_node_end_AllowedTypes = null;
		public static bool[] findMultiNested_node_beg_IsAllowedType = null;
		public static bool[] findMultiNested_node__node0_IsAllowedType = null;
		public static bool[] findMultiNested_node_end_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_NodeNums { @beg, @_node0, @end, };
		public enum findMultiNested_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_VariableNums { };
		public enum findMultiNested_SubNums { };
		public enum findMultiNested_AltNums { };


		GRGEN_LGSP.PatternGraph pat_findMultiNested;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_0_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_0_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_0_NodeNums { @_node0, @beg, @end, };
		public enum findMultiNested_idpt_0_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_0_VariableNums { };
		public enum findMultiNested_idpt_0_SubNums { };
		public enum findMultiNested_idpt_0_AltNums { };

		GRGEN_LGSP.PatternGraph findMultiNested_idpt_0;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_0_idpt_0_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_idpt_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_idpt_0_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_0_idpt_0_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_0_idpt_0_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_0_idpt_0_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_0_idpt_0_NodeNums { @beg, @_node0, @end, };
		public enum findMultiNested_idpt_0_idpt_0_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_0_idpt_0_VariableNums { };
		public enum findMultiNested_idpt_0_idpt_0_SubNums { };
		public enum findMultiNested_idpt_0_idpt_0_AltNums { };

		GRGEN_LGSP.PatternGraph findMultiNested_idpt_0_idpt_0;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_1_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_1_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_1_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_1_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_1_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_1_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_1_NodeNums { @beg, @_node0, @end, };
		public enum findMultiNested_idpt_1_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_1_VariableNums { };
		public enum findMultiNested_idpt_1_SubNums { };
		public enum findMultiNested_idpt_1_AltNums { };

		GRGEN_LGSP.PatternGraph findMultiNested_idpt_1;

		public static GRGEN_LIBGR.NodeType[] findMultiNested_idpt_1_idpt_0_node__node0_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_1_idpt_0_node__node0_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_1_idpt_0_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] findMultiNested_idpt_1_idpt_0_edge__edge1_AllowedTypes = null;
		public static bool[] findMultiNested_idpt_1_idpt_0_edge__edge0_IsAllowedType = null;
		public static bool[] findMultiNested_idpt_1_idpt_0_edge__edge1_IsAllowedType = null;
		public enum findMultiNested_idpt_1_idpt_0_NodeNums { @_node0, @beg, @end, };
		public enum findMultiNested_idpt_1_idpt_0_EdgeNums { @_edge0, @_edge1, };
		public enum findMultiNested_idpt_1_idpt_0_VariableNums { };
		public enum findMultiNested_idpt_1_idpt_0_SubNums { };
		public enum findMultiNested_idpt_1_idpt_0_AltNums { };

		GRGEN_LGSP.PatternGraph findMultiNested_idpt_1_idpt_0;


		private Rule_findMultiNested()
		{
			name = "findMultiNested";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findMultiNested_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findMultiNested_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findMultiNested_node_beg", "beg", findMultiNested_node_beg_AllowedTypes, findMultiNested_node_beg_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findMultiNested_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findMultiNested_node__node0", "_node0", findMultiNested_node__node0_AllowedTypes, findMultiNested_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode findMultiNested_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findMultiNested_node_end", "end", findMultiNested_node_end_AllowedTypes, findMultiNested_node_end_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_edge__edge0", "_edge0", findMultiNested_edge__edge0_AllowedTypes, findMultiNested_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_edge__edge1", "_edge1", findMultiNested_edge__edge1_AllowedTypes, findMultiNested_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] findMultiNested_idpt_0_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_0_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findMultiNested_idpt_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findMultiNested_idpt_0_node__node0", "_node0", findMultiNested_idpt_0_node__node0_AllowedTypes, findMultiNested_idpt_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_0_edge__edge0", "_edge0", findMultiNested_idpt_0_edge__edge0_AllowedTypes, findMultiNested_idpt_0_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_0_edge__edge1", "_edge1", findMultiNested_idpt_0_edge__edge1_AllowedTypes, findMultiNested_idpt_0_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] findMultiNested_idpt_0_idpt_0_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_0_idpt_0_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findMultiNested_idpt_0_idpt_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findMultiNested_idpt_0_idpt_0_node__node0", "_node0", findMultiNested_idpt_0_idpt_0_node__node0_AllowedTypes, findMultiNested_idpt_0_idpt_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_idpt_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_0_idpt_0_edge__edge0", "_edge0", findMultiNested_idpt_0_idpt_0_edge__edge0_AllowedTypes, findMultiNested_idpt_0_idpt_0_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_0_idpt_0_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_0_idpt_0_edge__edge1", "_edge1", findMultiNested_idpt_0_idpt_0_edge__edge1_AllowedTypes, findMultiNested_idpt_0_idpt_0_edge__edge1_IsAllowedType, 5.5F, -1);
			findMultiNested_idpt_0_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findMultiNested_idpt_0_",
				false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_node_beg, findMultiNested_idpt_0_idpt_0_node__node0, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_0_idpt_0_edge__edge0, findMultiNested_idpt_0_idpt_0_edge__edge1 }, 
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
				findMultiNested_idpt_0_idpt_0_isNodeHomomorphicGlobal,
				findMultiNested_idpt_0_idpt_0_isEdgeHomomorphicGlobal
			);
			findMultiNested_idpt_0_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_0_idpt_0_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_0_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_0_idpt_0_edge__edge0, findMultiNested_idpt_0_idpt_0_node__node0);
			findMultiNested_idpt_0_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_0_idpt_0_edge__edge1, findMultiNested_idpt_0_idpt_0_node__node0);
			findMultiNested_idpt_0_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_0_idpt_0_edge__edge1, findMultiNested_node_end);

			findMultiNested_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findMultiNested_",
				false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_idpt_0_node__node0, findMultiNested_node_beg, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_0_edge__edge0, findMultiNested_idpt_0_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findMultiNested_idpt_0_idpt_0,  }, 
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
				findMultiNested_idpt_0_isNodeHomomorphicGlobal,
				findMultiNested_idpt_0_isEdgeHomomorphicGlobal
			);
			findMultiNested_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_0_edge__edge0, findMultiNested_idpt_0_node__node0);
			findMultiNested_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_0_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_0_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_0_edge__edge1, findMultiNested_idpt_0_node__node0);
			findMultiNested_idpt_0_idpt_0.embeddingGraph = findMultiNested_idpt_0;

			bool[,] findMultiNested_idpt_1_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_1_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findMultiNested_idpt_1_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findMultiNested_idpt_1_node__node0", "_node0", findMultiNested_idpt_1_node__node0_AllowedTypes, findMultiNested_idpt_1_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_1_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_1_edge__edge0", "_edge0", findMultiNested_idpt_1_edge__edge0_AllowedTypes, findMultiNested_idpt_1_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_1_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_1_edge__edge1", "_edge1", findMultiNested_idpt_1_edge__edge1_AllowedTypes, findMultiNested_idpt_1_edge__edge1_IsAllowedType, 5.5F, -1);
			bool[,] findMultiNested_idpt_1_idpt_0_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] findMultiNested_idpt_1_idpt_0_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode findMultiNested_idpt_1_idpt_0_node__node0 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findMultiNested_idpt_1_idpt_0_node__node0", "_node0", findMultiNested_idpt_1_idpt_0_node__node0_AllowedTypes, findMultiNested_idpt_1_idpt_0_node__node0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_1_idpt_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_1_idpt_0_edge__edge0", "_edge0", findMultiNested_idpt_1_idpt_0_edge__edge0_AllowedTypes, findMultiNested_idpt_1_idpt_0_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge findMultiNested_idpt_1_idpt_0_edge__edge1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "findMultiNested_idpt_1_idpt_0_edge__edge1", "_edge1", findMultiNested_idpt_1_idpt_0_edge__edge1_AllowedTypes, findMultiNested_idpt_1_idpt_0_edge__edge1_IsAllowedType, 5.5F, -1);
			findMultiNested_idpt_1_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findMultiNested_idpt_1_",
				false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_idpt_1_idpt_0_node__node0, findMultiNested_node_beg, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_1_idpt_0_edge__edge0, findMultiNested_idpt_1_idpt_0_edge__edge1 }, 
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
				findMultiNested_idpt_1_idpt_0_isNodeHomomorphicGlobal,
				findMultiNested_idpt_1_idpt_0_isEdgeHomomorphicGlobal
			);
			findMultiNested_idpt_1_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_1_idpt_0_edge__edge0, findMultiNested_idpt_1_idpt_0_node__node0);
			findMultiNested_idpt_1_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_1_idpt_0_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_1_idpt_0.edgeToSourceNode.Add(findMultiNested_idpt_1_idpt_0_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_1_idpt_0.edgeToTargetNode.Add(findMultiNested_idpt_1_idpt_0_edge__edge1, findMultiNested_idpt_1_idpt_0_node__node0);

			findMultiNested_idpt_1 = new GRGEN_LGSP.PatternGraph(
				"idpt_1",
				"findMultiNested_",
				false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_node_beg, findMultiNested_idpt_1_node__node0, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_idpt_1_edge__edge0, findMultiNested_idpt_1_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findMultiNested_idpt_1_idpt_0,  }, 
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
				findMultiNested_idpt_1_isNodeHomomorphicGlobal,
				findMultiNested_idpt_1_isEdgeHomomorphicGlobal
			);
			findMultiNested_idpt_1.edgeToSourceNode.Add(findMultiNested_idpt_1_edge__edge0, findMultiNested_node_beg);
			findMultiNested_idpt_1.edgeToTargetNode.Add(findMultiNested_idpt_1_edge__edge0, findMultiNested_idpt_1_node__node0);
			findMultiNested_idpt_1.edgeToSourceNode.Add(findMultiNested_idpt_1_edge__edge1, findMultiNested_idpt_1_node__node0);
			findMultiNested_idpt_1.edgeToTargetNode.Add(findMultiNested_idpt_1_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_1_idpt_0.embeddingGraph = findMultiNested_idpt_1;

			pat_findMultiNested = new GRGEN_LGSP.PatternGraph(
				"findMultiNested",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findMultiNested_node_beg, findMultiNested_node__node0, findMultiNested_node_end }, 
				new GRGEN_LGSP.PatternEdge[] { findMultiNested_edge__edge0, findMultiNested_edge__edge1 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findMultiNested_idpt_0, findMultiNested_idpt_1,  }, 
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
				findMultiNested_isNodeHomomorphicGlobal,
				findMultiNested_isEdgeHomomorphicGlobal
			);
			pat_findMultiNested.edgeToSourceNode.Add(findMultiNested_edge__edge0, findMultiNested_node_beg);
			pat_findMultiNested.edgeToTargetNode.Add(findMultiNested_edge__edge0, findMultiNested_node__node0);
			pat_findMultiNested.edgeToSourceNode.Add(findMultiNested_edge__edge1, findMultiNested_node__node0);
			pat_findMultiNested.edgeToTargetNode.Add(findMultiNested_edge__edge1, findMultiNested_node_end);
			findMultiNested_idpt_0.embeddingGraph = pat_findMultiNested;
			findMultiNested_idpt_1.embeddingGraph = pat_findMultiNested;

			findMultiNested_node_beg.PointOfDefinition = pat_findMultiNested;
			findMultiNested_node__node0.PointOfDefinition = pat_findMultiNested;
			findMultiNested_node_end.PointOfDefinition = pat_findMultiNested;
			findMultiNested_edge__edge0.PointOfDefinition = pat_findMultiNested;
			findMultiNested_edge__edge1.PointOfDefinition = pat_findMultiNested;
			findMultiNested_idpt_0_node__node0.PointOfDefinition = findMultiNested_idpt_0;
			findMultiNested_idpt_0_edge__edge0.PointOfDefinition = findMultiNested_idpt_0;
			findMultiNested_idpt_0_edge__edge1.PointOfDefinition = findMultiNested_idpt_0;
			findMultiNested_idpt_0_idpt_0_node__node0.PointOfDefinition = findMultiNested_idpt_0_idpt_0;
			findMultiNested_idpt_0_idpt_0_edge__edge0.PointOfDefinition = findMultiNested_idpt_0_idpt_0;
			findMultiNested_idpt_0_idpt_0_edge__edge1.PointOfDefinition = findMultiNested_idpt_0_idpt_0;
			findMultiNested_idpt_1_node__node0.PointOfDefinition = findMultiNested_idpt_1;
			findMultiNested_idpt_1_edge__edge0.PointOfDefinition = findMultiNested_idpt_1;
			findMultiNested_idpt_1_edge__edge1.PointOfDefinition = findMultiNested_idpt_1;
			findMultiNested_idpt_1_idpt_0_node__node0.PointOfDefinition = findMultiNested_idpt_1_idpt_0;
			findMultiNested_idpt_1_idpt_0_edge__edge0.PointOfDefinition = findMultiNested_idpt_1_idpt_0;
			findMultiNested_idpt_1_idpt_0_edge__edge1.PointOfDefinition = findMultiNested_idpt_1_idpt_0;

			patternGraph = pat_findMultiNested;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findMultiNested curMatch = (Match_findMultiNested)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findMultiNested curMatch = (Match_findMultiNested)_curMatch;
			return EmptyReturnElements;
		}

		static Rule_findMultiNested() {
		}

		public interface IMatch_findMultiNested : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
			IMatch_findMultiNested_idpt_0 idpt_0 { get; }
			IMatch_findMultiNested_idpt_1 idpt_1 { get; }
		}

		public interface IMatch_findMultiNested_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
			IMatch_findMultiNested_idpt_0_idpt_0 idpt_0 { get; }
		}

		public interface IMatch_findMultiNested_idpt_0_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_findMultiNested_idpt_1 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
			IMatch_findMultiNested_idpt_1_idpt_0 idpt_0 { get; }
		}

		public interface IMatch_findMultiNested_idpt_1_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node__node0 { get; }
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			GRGEN_LIBGR.IEdge edge__edge1 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_findMultiNested : GRGEN_LGSP.ListElement<Match_findMultiNested>, IMatch_findMultiNested
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_findMultiNested_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findMultiNested_idpt_1 idpt_1 { get { return _idpt_1; } }
			public IMatch_findMultiNested_idpt_0 _idpt_0;
			public IMatch_findMultiNested_idpt_1 _idpt_1;
			public enum findMultiNested_IdptNums { @idpt_0, @idpt_1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 2;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_IdptNums.@idpt_0: return _idpt_0;
				case (int)findMultiNested_IdptNums.@idpt_1: return _idpt_1;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.pat_findMultiNested; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_findMultiNested_idpt_0 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_0>, IMatch_findMultiNested_idpt_0
		{
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_0_NodeNums { @_node0, @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_0_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_0_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_0_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_findMultiNested_idpt_0_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findMultiNested_idpt_0_idpt_0 _idpt_0;
			public enum findMultiNested_idpt_0_IdptNums { @idpt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_IdptNums.@idpt_0: return _idpt_0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_findMultiNested_idpt_0_idpt_0 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_0_idpt_0>, IMatch_findMultiNested_idpt_0_idpt_0
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_0_idpt_0_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_idpt_0_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_0_idpt_0_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_0_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_0_idpt_0_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_0_idpt_0_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_0_idpt_0_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_idpt_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_idpt_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_idpt_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_0_idpt_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_0_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_findMultiNested_idpt_1 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_1>, IMatch_findMultiNested_idpt_1
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_1_NodeNums { @beg, @_node0, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_1_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_1_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_1_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_1_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_1_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_1_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_1_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_1_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_1_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_findMultiNested_idpt_1_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findMultiNested_idpt_1_idpt_0 _idpt_0;
			public enum findMultiNested_idpt_1_IdptNums { @idpt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_1_IdptNums.@idpt_0: return _idpt_0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_1; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_findMultiNested_idpt_1_idpt_0 : GRGEN_LGSP.ListElement<Match_findMultiNested_idpt_1_idpt_0>, IMatch_findMultiNested_idpt_1_idpt_0
		{
			public GRGEN_LIBGR.INode node__node0 { get { return (GRGEN_LIBGR.INode)_node__node0; } }
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findMultiNested_idpt_1_idpt_0_NodeNums { @_node0, @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_1_idpt_0_NodeNums.@_node0: return _node__node0;
				case (int)findMultiNested_idpt_1_idpt_0_NodeNums.@beg: return _node_beg;
				case (int)findMultiNested_idpt_1_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LIBGR.IEdge edge__edge1 { get { return (GRGEN_LIBGR.IEdge)_edge__edge1; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge__edge1;
			public enum findMultiNested_idpt_1_idpt_0_EdgeNums { @_edge0, @_edge1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)findMultiNested_idpt_1_idpt_0_EdgeNums.@_edge0: return _edge__edge0;
				case (int)findMultiNested_idpt_1_idpt_0_EdgeNums.@_edge1: return _edge__edge1;
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_1_idpt_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_1_idpt_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_1_idpt_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findMultiNested_idpt_1_idpt_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findMultiNested.instance.findMultiNested_idpt_1_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_createIterated : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_createIterated instance = null;
		public static Rule_createIterated Instance { get { if (instance==null) { instance = new Rule_createIterated(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[2];

		public enum createIterated_NodeNums { };
		public enum createIterated_EdgeNums { };
		public enum createIterated_VariableNums { };
		public enum createIterated_SubNums { };
		public enum createIterated_AltNums { };



		GRGEN_LGSP.PatternGraph pat_createIterated;


		private Rule_createIterated()
		{
			name = "createIterated";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { NodeType_intNode.typeVar, NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] createIterated_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createIterated_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createIterated = new GRGEN_LGSP.PatternGraph(
				"createIterated",
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
				createIterated_isNodeHomomorphicGlobal,
				createIterated_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createIterated;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createIterated curMatch = (Match_createIterated)_curMatch;
			graph.SettingAddedNodeNames( createIterated_addedNodeNames );
			@intNode node_n1 = @intNode.CreateNode(graph);
			@Node node_n2 = @Node.CreateNode(graph);
			@Node node_n3 = @Node.CreateNode(graph);
			@Node node_n4 = @Node.CreateNode(graph);
			@Node node_n5 = @Node.CreateNode(graph);
			@Node node_n3b = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createIterated_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_n1, node_n2);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_n2, node_n3);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_n3, node_n4);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_n4, node_n5);
			@Edge edge__edge4 = @Edge.CreateEdge(graph, node_n2, node_n1);
			@Edge edge__edge5 = @Edge.CreateEdge(graph, node_n3b, node_n2);
			@Edge edge__edge6 = @Edge.CreateEdge(graph, node_n4, node_n3b);
			@Edge edge__edge7 = @Edge.CreateEdge(graph, node_n5, node_n4);
			ReturnArray[0] = node_n1;
			ReturnArray[1] = node_n5;
			return ReturnArray;
		}
		private static String[] createIterated_addedNodeNames = new String[] { "n1", "n2", "n3", "n4", "n5", "n3b" };
		private static String[] createIterated_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_createIterated curMatch = (Match_createIterated)_curMatch;
			graph.SettingAddedNodeNames( createIterated_addedNodeNames );
			@intNode node_n1 = @intNode.CreateNode(graph);
			@Node node_n2 = @Node.CreateNode(graph);
			@Node node_n3 = @Node.CreateNode(graph);
			@Node node_n4 = @Node.CreateNode(graph);
			@Node node_n5 = @Node.CreateNode(graph);
			@Node node_n3b = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createIterated_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_n1, node_n2);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_n2, node_n3);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_n3, node_n4);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_n4, node_n5);
			@Edge edge__edge4 = @Edge.CreateEdge(graph, node_n2, node_n1);
			@Edge edge__edge5 = @Edge.CreateEdge(graph, node_n3b, node_n2);
			@Edge edge__edge6 = @Edge.CreateEdge(graph, node_n4, node_n3b);
			@Edge edge__edge7 = @Edge.CreateEdge(graph, node_n5, node_n4);
			ReturnArray[0] = node_n1;
			ReturnArray[1] = node_n5;
			return ReturnArray;
		}

		static Rule_createIterated() {
		}

		public interface IMatch_createIterated : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_createIterated : GRGEN_LGSP.ListElement<Match_createIterated>, IMatch_createIterated
		{
			public enum createIterated_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createIterated_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createIterated_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createIterated_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createIterated_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum createIterated_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_createIterated.instance.pat_createIterated; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findChainPlusChainToInt : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findChainPlusChainToInt instance = null;
		public static Rule_findChainPlusChainToInt Instance { get { if (instance==null) { instance = new Rule_findChainPlusChainToInt(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToInt_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToInt_node_end_AllowedTypes = null;
		public static bool[] findChainPlusChainToInt_node_beg_IsAllowedType = null;
		public static bool[] findChainPlusChainToInt_node_end_IsAllowedType = null;
		public enum findChainPlusChainToInt_NodeNums { @beg, @end, };
		public enum findChainPlusChainToInt_EdgeNums { };
		public enum findChainPlusChainToInt_VariableNums { };
		public enum findChainPlusChainToInt_SubNums { @_subpattern0, @_subpattern1, };
		public enum findChainPlusChainToInt_AltNums { };


		GRGEN_LGSP.PatternGraph pat_findChainPlusChainToInt;


		private Rule_findChainPlusChainToInt()
		{
			name = "findChainPlusChainToInt";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "findChainPlusChainToInt_node_beg", "findChainPlusChainToInt_node_end", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findChainPlusChainToInt_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findChainPlusChainToInt_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode findChainPlusChainToInt_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findChainPlusChainToInt_node_beg", "beg", findChainPlusChainToInt_node_beg_AllowedTypes, findChainPlusChainToInt_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode findChainPlusChainToInt_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findChainPlusChainToInt_node_end", "end", findChainPlusChainToInt_node_end_AllowedTypes, findChainPlusChainToInt_node_end_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToInt__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_iteratedPath.Instance, new GRGEN_LGSP.PatternElement[] { findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end });
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToInt__subpattern1 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern1", Pattern_iteratedPathToIntNode.Instance, new GRGEN_LGSP.PatternElement[] { findChainPlusChainToInt_node_end });
			pat_findChainPlusChainToInt = new GRGEN_LGSP.PatternGraph(
				"findChainPlusChainToInt",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findChainPlusChainToInt_node_beg, findChainPlusChainToInt_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { findChainPlusChainToInt__subpattern0, findChainPlusChainToInt__subpattern1 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				findChainPlusChainToInt_isNodeHomomorphicGlobal,
				findChainPlusChainToInt_isEdgeHomomorphicGlobal
			);

			findChainPlusChainToInt_node_beg.PointOfDefinition = null;
			findChainPlusChainToInt_node_end.PointOfDefinition = null;
			findChainPlusChainToInt__subpattern0.PointOfDefinition = pat_findChainPlusChainToInt;
			findChainPlusChainToInt__subpattern1.PointOfDefinition = pat_findChainPlusChainToInt;

			patternGraph = pat_findChainPlusChainToInt;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findChainPlusChainToInt curMatch = (Match_findChainPlusChainToInt)_curMatch;
			Pattern_iteratedPath.Match_iteratedPath subpattern__subpattern0 = curMatch.@__subpattern0;
			Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode subpattern__subpattern1 = curMatch.@__subpattern1;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findChainPlusChainToInt curMatch = (Match_findChainPlusChainToInt)_curMatch;
			Pattern_iteratedPath.Match_iteratedPath subpattern__subpattern0 = curMatch.@__subpattern0;
			Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode subpattern__subpattern1 = curMatch.@__subpattern1;
			return EmptyReturnElements;
		}

		static Rule_findChainPlusChainToInt() {
		}

		public interface IMatch_findChainPlusChainToInt : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPath.Match_iteratedPath @_subpattern0 { get; }
			@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_subpattern1 { get; }
			//Alternatives
			//Independents
		}

		public class Match_findChainPlusChainToInt : GRGEN_LGSP.ListElement<Match_findChainPlusChainToInt>, IMatch_findChainPlusChainToInt
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findChainPlusChainToInt_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToInt_NodeNums.@beg: return _node_beg;
				case (int)findChainPlusChainToInt_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToInt_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findChainPlusChainToInt_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_iteratedPath.Match_iteratedPath @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_subpattern1 { get { return @__subpattern1; } }
			public @Pattern_iteratedPath.Match_iteratedPath @__subpattern0;
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @__subpattern1;
			public enum findChainPlusChainToInt_SubNums { @_subpattern0, @_subpattern1, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 2;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToInt_SubNums.@_subpattern0: return __subpattern0;
				case (int)findChainPlusChainToInt_SubNums.@_subpattern1: return __subpattern1;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToInt_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findChainPlusChainToInt_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findChainPlusChainToInt.instance.pat_findChainPlusChainToInt; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_findChainPlusChainToIntIndependent : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_findChainPlusChainToIntIndependent instance = null;
		public static Rule_findChainPlusChainToIntIndependent Instance { get { if (instance==null) { instance = new Rule_findChainPlusChainToIntIndependent(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToIntIndependent_node_beg_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] findChainPlusChainToIntIndependent_node_end_AllowedTypes = null;
		public static bool[] findChainPlusChainToIntIndependent_node_beg_IsAllowedType = null;
		public static bool[] findChainPlusChainToIntIndependent_node_end_IsAllowedType = null;
		public enum findChainPlusChainToIntIndependent_NodeNums { @beg, @end, };
		public enum findChainPlusChainToIntIndependent_EdgeNums { };
		public enum findChainPlusChainToIntIndependent_VariableNums { };
		public enum findChainPlusChainToIntIndependent_SubNums { @_subpattern0, };
		public enum findChainPlusChainToIntIndependent_AltNums { };


		GRGEN_LGSP.PatternGraph pat_findChainPlusChainToIntIndependent;

		public enum findChainPlusChainToIntIndependent_idpt_0_NodeNums { @end, };
		public enum findChainPlusChainToIntIndependent_idpt_0_EdgeNums { };
		public enum findChainPlusChainToIntIndependent_idpt_0_VariableNums { };
		public enum findChainPlusChainToIntIndependent_idpt_0_SubNums { @_subpattern0, };
		public enum findChainPlusChainToIntIndependent_idpt_0_AltNums { };

		GRGEN_LGSP.PatternGraph findChainPlusChainToIntIndependent_idpt_0;


		private Rule_findChainPlusChainToIntIndependent()
		{
			name = "findChainPlusChainToIntIndependent";

			inputs = new GRGEN_LIBGR.GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "findChainPlusChainToIntIndependent_node_beg", "findChainPlusChainToIntIndependent_node_end", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] findChainPlusChainToIntIndependent_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] findChainPlusChainToIntIndependent_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode findChainPlusChainToIntIndependent_node_beg = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findChainPlusChainToIntIndependent_node_beg", "beg", findChainPlusChainToIntIndependent_node_beg_AllowedTypes, findChainPlusChainToIntIndependent_node_beg_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternNode findChainPlusChainToIntIndependent_node_end = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Node, "GRGEN_LIBGR.INode", "findChainPlusChainToIntIndependent_node_end", "end", findChainPlusChainToIntIndependent_node_end_AllowedTypes, findChainPlusChainToIntIndependent_node_end_IsAllowedType, 5.5F, 1);
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToIntIndependent__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_iteratedPath.Instance, new GRGEN_LGSP.PatternElement[] { findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end });
			bool[,] findChainPlusChainToIntIndependent_idpt_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] findChainPlusChainToIntIndependent_idpt_0_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternGraphEmbedding findChainPlusChainToIntIndependent_idpt_0__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_iteratedPathToIntNode.Instance, new GRGEN_LGSP.PatternElement[] { findChainPlusChainToIntIndependent_node_end });
			findChainPlusChainToIntIndependent_idpt_0 = new GRGEN_LGSP.PatternGraph(
				"idpt_0",
				"findChainPlusChainToIntIndependent_",
				false,
				new GRGEN_LGSP.PatternNode[] { findChainPlusChainToIntIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { findChainPlusChainToIntIndependent_idpt_0__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				findChainPlusChainToIntIndependent_idpt_0_isNodeHomomorphicGlobal,
				findChainPlusChainToIntIndependent_idpt_0_isEdgeHomomorphicGlobal
			);

			pat_findChainPlusChainToIntIndependent = new GRGEN_LGSP.PatternGraph(
				"findChainPlusChainToIntIndependent",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { findChainPlusChainToIntIndependent_node_beg, findChainPlusChainToIntIndependent_node_end }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { findChainPlusChainToIntIndependent__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { findChainPlusChainToIntIndependent_idpt_0,  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				findChainPlusChainToIntIndependent_isNodeHomomorphicGlobal,
				findChainPlusChainToIntIndependent_isEdgeHomomorphicGlobal
			);
			findChainPlusChainToIntIndependent_idpt_0.embeddingGraph = pat_findChainPlusChainToIntIndependent;

			findChainPlusChainToIntIndependent_node_beg.PointOfDefinition = null;
			findChainPlusChainToIntIndependent_node_end.PointOfDefinition = null;
			findChainPlusChainToIntIndependent__subpattern0.PointOfDefinition = pat_findChainPlusChainToIntIndependent;
			findChainPlusChainToIntIndependent_idpt_0__subpattern0.PointOfDefinition = findChainPlusChainToIntIndependent_idpt_0;

			patternGraph = pat_findChainPlusChainToIntIndependent;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findChainPlusChainToIntIndependent curMatch = (Match_findChainPlusChainToIntIndependent)_curMatch;
			Pattern_iteratedPath.Match_iteratedPath subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_findChainPlusChainToIntIndependent curMatch = (Match_findChainPlusChainToIntIndependent)_curMatch;
			Pattern_iteratedPath.Match_iteratedPath subpattern__subpattern0 = curMatch.@__subpattern0;
			return EmptyReturnElements;
		}

		static Rule_findChainPlusChainToIntIndependent() {
		}

		public interface IMatch_findChainPlusChainToIntIndependent : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_beg { get; }
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPath.Match_iteratedPath @_subpattern0 { get; }
			//Alternatives
			//Independents
			IMatch_findChainPlusChainToIntIndependent_idpt_0 idpt_0 { get; }
		}

		public interface IMatch_findChainPlusChainToIntIndependent_idpt_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_end { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_subpattern0 { get; }
			//Alternatives
			//Independents
		}

		public class Match_findChainPlusChainToIntIndependent : GRGEN_LGSP.ListElement<Match_findChainPlusChainToIntIndependent>, IMatch_findChainPlusChainToIntIndependent
		{
			public GRGEN_LIBGR.INode node_beg { get { return (GRGEN_LIBGR.INode)_node_beg; } }
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_beg;
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findChainPlusChainToIntIndependent_NodeNums { @beg, @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_NodeNums.@beg: return _node_beg;
				case (int)findChainPlusChainToIntIndependent_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_iteratedPath.Match_iteratedPath @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_iteratedPath.Match_iteratedPath @__subpattern0;
			public enum findChainPlusChainToIntIndependent_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public IMatch_findChainPlusChainToIntIndependent_idpt_0 idpt_0 { get { return _idpt_0; } }
			public IMatch_findChainPlusChainToIntIndependent_idpt_0 _idpt_0;
			public enum findChainPlusChainToIntIndependent_IdptNums { @idpt_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 1;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_IdptNums.@idpt_0: return _idpt_0;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findChainPlusChainToIntIndependent.instance.pat_findChainPlusChainToIntIndependent; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_findChainPlusChainToIntIndependent_idpt_0 : GRGEN_LGSP.ListElement<Match_findChainPlusChainToIntIndependent_idpt_0>, IMatch_findChainPlusChainToIntIndependent_idpt_0
		{
			public GRGEN_LIBGR.INode node_end { get { return (GRGEN_LIBGR.INode)_node_end; } }
			public GRGEN_LGSP.LGSPNode _node_end;
			public enum findChainPlusChainToIntIndependent_idpt_0_NodeNums { @end, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_idpt_0_NodeNums.@end: return _node_end;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_idpt_0_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_idpt_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode @__subpattern0;
			public enum findChainPlusChainToIntIndependent_idpt_0_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)findChainPlusChainToIntIndependent_idpt_0_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_idpt_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum findChainPlusChainToIntIndependent_idpt_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_findChainPlusChainToIntIndependent.instance.findChainPlusChainToIntIndependent_idpt_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Independent_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public Independent_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[2];
			rules = new GRGEN_LGSP.LGSPRulePattern[7];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[2+7];
			subpatterns[0] = Pattern_iteratedPath.Instance;
			rulesAndSubpatterns[0] = Pattern_iteratedPath.Instance;
			subpatterns[1] = Pattern_iteratedPathToIntNode.Instance;
			rulesAndSubpatterns[1] = Pattern_iteratedPathToIntNode.Instance;
			rules[0] = Rule_create.Instance;
			rulesAndSubpatterns[2+0] = Rule_create.Instance;
			rules[1] = Rule_find.Instance;
			rulesAndSubpatterns[2+1] = Rule_find.Instance;
			rules[2] = Rule_findIndependent.Instance;
			rulesAndSubpatterns[2+2] = Rule_findIndependent.Instance;
			rules[3] = Rule_findMultiNested.Instance;
			rulesAndSubpatterns[2+3] = Rule_findMultiNested.Instance;
			rules[4] = Rule_createIterated.Instance;
			rulesAndSubpatterns[2+4] = Rule_createIterated.Instance;
			rules[5] = Rule_findChainPlusChainToInt.Instance;
			rulesAndSubpatterns[2+5] = Rule_findChainPlusChainToInt.Instance;
			rules[6] = Rule_findChainPlusChainToIntIndependent.Instance;
			rulesAndSubpatterns[2+6] = Rule_findChainPlusChainToIntIndependent.Instance;
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
	}


    public class PatternAction_iteratedPath : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_iteratedPath(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_iteratedPath.Instance.patternGraph;
        }

        public static PatternAction_iteratedPath getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_iteratedPath newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_iteratedPath(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_iteratedPath oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_iteratedPath freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_iteratedPath next = null;

        public GRGEN_LGSP.LGSPNode iteratedPath_node_beg;
        public GRGEN_LGSP.LGSPNode iteratedPath_node_end;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_iteratedPath.Match_iteratedPath patternpath_match_iteratedPath = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset iteratedPath_node_beg 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg = iteratedPath_node_beg;
            // SubPreset iteratedPath_node_end 
            GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end = iteratedPath_node_end;
            // Push alternative matching task for iteratedPath_alt_0
            AlternativeAction_iteratedPath_alt_0 taskFor_alt_0 = AlternativeAction_iteratedPath_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_iteratedPath.iteratedPath_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.iteratedPath_node_beg = candidate_iteratedPath_node_beg;
            taskFor_alt_0.iteratedPath_node_end = candidate_iteratedPath_node_end;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_iteratedPath_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_iteratedPath.Match_iteratedPath match = new Pattern_iteratedPath.Match_iteratedPath();
                    match._node_beg = candidate_iteratedPath_node_beg;
                    match._node_end = candidate_iteratedPath_node_end;
                    match._alt_0 = (Pattern_iteratedPath.IMatch_iteratedPath_alt_0)currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                } else {
                    foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
                // if enough matches were found, we leave
                if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                {
                    openTasks.Push(this);
                    return;
                }
                openTasks.Push(this);
                return;
            }
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_iteratedPath_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_iteratedPath_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_iteratedPath_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_iteratedPath_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_iteratedPath_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_iteratedPath_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_iteratedPath_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_iteratedPath_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode iteratedPath_node_beg;
        public GRGEN_LGSP.LGSPNode iteratedPath_node_end;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive patternpath_match_iteratedPath_alt_0_recursive = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPath_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@base];
                // SubPreset iteratedPath_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg = iteratedPath_node_beg;
                // SubPreset iteratedPath_node_end 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end = iteratedPath_node_end;
                // Extend Outgoing iteratedPath_alt_0_base_edge__edge0 from iteratedPath_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_base_edge__edge0 = candidate_iteratedPath_node_beg.outhead;
                if(head_candidate_iteratedPath_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_base_edge__edge0 = head_candidate_iteratedPath_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_base_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_iteratedPath_alt_0_base_edge__edge0.target != candidate_iteratedPath_node_end) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                            match._node_beg = candidate_iteratedPath_node_beg;
                            match._node_end = candidate_iteratedPath_node_end;
                            match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                        prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_iteratedPath_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_base match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_base();
                                match._node_beg = candidate_iteratedPath_node_beg;
                                match._node_end = candidate_iteratedPath_node_end;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_iteratedPath_alt_0_base_edge__edge0.flags = candidate_iteratedPath_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_base_edge__edge0.flags = candidate_iteratedPath_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_base_edge__edge0.flags = candidate_iteratedPath_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_base_edge__edge0;
                    }
                    while( (candidate_iteratedPath_alt_0_base_edge__edge0 = candidate_iteratedPath_alt_0_base_edge__edge0.outNext) != head_candidate_iteratedPath_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                } else {
                    foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case iteratedPath_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPath.iteratedPath_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPath_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_beg = iteratedPath_node_beg;
                // SubPreset iteratedPath_node_end 
                GRGEN_LGSP.LGSPNode candidate_iteratedPath_node_end = iteratedPath_node_end;
                // Extend Outgoing iteratedPath_alt_0_recursive_edge__edge0 from iteratedPath_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPath_alt_0_recursive_edge__edge0 = candidate_iteratedPath_node_beg.outhead;
                if(head_candidate_iteratedPath_alt_0_recursive_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPath_alt_0_recursive_edge__edge0 = head_candidate_iteratedPath_alt_0_recursive_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPath_alt_0_recursive_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPath_alt_0_recursive_node_intermediate from iteratedPath_alt_0_recursive_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPath_alt_0_recursive_node_intermediate = candidate_iteratedPath_alt_0_recursive_edge__edge0.target;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_iteratedPath_alt_0_recursive_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_iteratedPath_alt_0_recursive_node_intermediate))
                            && candidate_iteratedPath_alt_0_recursive_node_intermediate==candidate_iteratedPath_node_beg
                            )
                        {
                            continue;
                        }
                        if((candidate_iteratedPath_alt_0_recursive_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_iteratedPath taskFor__subpattern0 = PatternAction_iteratedPath.getNewTask(graph, openTasks);
                        taskFor__subpattern0.iteratedPath_node_beg = candidate_iteratedPath_alt_0_recursive_node_intermediate;
                        taskFor__subpattern0.iteratedPath_node_end = candidate_iteratedPath_node_end;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate = candidate_iteratedPath_alt_0_recursive_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_iteratedPath_alt_0_recursive_node_intermediate.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                        prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_iteratedPath.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive match = new Pattern_iteratedPath.Match_iteratedPath_alt_0_recursive();
                                match._node_beg = candidate_iteratedPath_node_beg;
                                match._node_intermediate = candidate_iteratedPath_alt_0_recursive_node_intermediate;
                                match._node_end = candidate_iteratedPath_node_end;
                                match._edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0;
                                match.__subpattern0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_iteratedPath_alt_0_recursive_edge__edge0.flags = candidate_iteratedPath_alt_0_recursive_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                                candidate_iteratedPath_alt_0_recursive_node_intermediate.flags = candidate_iteratedPath_alt_0_recursive_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPath_alt_0_recursive_edge__edge0.flags = candidate_iteratedPath_alt_0_recursive_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                            candidate_iteratedPath_alt_0_recursive_node_intermediate.flags = candidate_iteratedPath_alt_0_recursive_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                            continue;
                        }
                        candidate_iteratedPath_alt_0_recursive_node_intermediate.flags = candidate_iteratedPath_alt_0_recursive_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_recursive_node_intermediate;
                        candidate_iteratedPath_alt_0_recursive_edge__edge0.flags = candidate_iteratedPath_alt_0_recursive_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPath_alt_0_recursive_edge__edge0;
                    }
                    while( (candidate_iteratedPath_alt_0_recursive_edge__edge0 = candidate_iteratedPath_alt_0_recursive_edge__edge0.outNext) != head_candidate_iteratedPath_alt_0_recursive_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_iteratedPathToIntNode : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_iteratedPathToIntNode(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_iteratedPathToIntNode.Instance.patternGraph;
        }

        public static PatternAction_iteratedPathToIntNode getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_iteratedPathToIntNode newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_iteratedPathToIntNode(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_iteratedPathToIntNode oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_iteratedPathToIntNode freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_iteratedPathToIntNode next = null;

        public GRGEN_LGSP.LGSPNode iteratedPathToIntNode_node_beg;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode patternpath_match_iteratedPathToIntNode = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset iteratedPathToIntNode_node_beg 
            GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg = iteratedPathToIntNode_node_beg;
            // Push alternative matching task for iteratedPathToIntNode_alt_0
            AlternativeAction_iteratedPathToIntNode_alt_0 taskFor_alt_0 = AlternativeAction_iteratedPathToIntNode_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.iteratedPathToIntNode_node_beg = candidate_iteratedPathToIntNode_node_beg;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_iteratedPathToIntNode_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode();
                    match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                    match._alt_0 = (Pattern_iteratedPathToIntNode.IMatch_iteratedPathToIntNode_alt_0)currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                } else {
                    foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
                // if enough matches were found, we leave
                if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                {
                    openTasks.Push(this);
                    return;
                }
                openTasks.Push(this);
                return;
            }
            openTasks.Push(this);
            return;
        }
    }

    public class AlternativeAction_iteratedPathToIntNode_alt_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private AlternativeAction_iteratedPathToIntNode_alt_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_iteratedPathToIntNode_alt_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_, GRGEN_LGSP.PatternGraph[] patternGraphs_) {
            AlternativeAction_iteratedPathToIntNode_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_iteratedPathToIntNode_alt_0(graph_, openTasks_, patternGraphs_);
            }
            return newTask;
        }

        public static void releaseTask(AlternativeAction_iteratedPathToIntNode_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_iteratedPathToIntNode_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_iteratedPathToIntNode_alt_0 next = null;

        public GRGEN_LGSP.LGSPNode iteratedPathToIntNode_node_beg;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive patternpath_match_iteratedPathToIntNode_alt_0_recursive = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case iteratedPathToIntNode_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@base];
                // SubPreset iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg = iteratedPathToIntNode_node_beg;
                // Extend Outgoing iteratedPathToIntNode_alt_0_base_edge__edge0 from iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = candidate_iteratedPathToIntNode_node_beg.outhead;
                if(head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_base_node_end from iteratedPathToIntNode_alt_0_base_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_base_node_end = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.target;
                        if(candidate_iteratedPathToIntNode_alt_0_base_node_end.type.TypeID!=1) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_iteratedPathToIntNode_alt_0_base_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_iteratedPathToIntNode_alt_0_base_node_end)))
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_base_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                            match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                            match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end;
                            match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_iteratedPathToIntNode_alt_0_base_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_base();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                                match._node_end = candidate_iteratedPathToIntNode_alt_0_base_node_end;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                                candidate_iteratedPathToIntNode_alt_0_base_node_end.flags = candidate_iteratedPathToIntNode_alt_0_base_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                            candidate_iteratedPathToIntNode_alt_0_base_node_end.flags = candidate_iteratedPathToIntNode_alt_0_base_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_base_node_end.flags = candidate_iteratedPathToIntNode_alt_0_base_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_node_end;
                        candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_base_edge__edge0;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_base_edge__edge0.outNext) != head_candidate_iteratedPathToIntNode_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                } else {
                    foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case iteratedPathToIntNode_alt_0_recursive 
            do {
                patternGraph = patternGraphs[(int)Pattern_iteratedPathToIntNode.iteratedPathToIntNode_alt_0_CaseNums.@recursive];
                // SubPreset iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_node_beg = iteratedPathToIntNode_node_beg;
                // Extend Outgoing iteratedPathToIntNode_alt_0_recursive_edge__edge0 from iteratedPathToIntNode_node_beg 
                GRGEN_LGSP.LGSPEdge head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = candidate_iteratedPathToIntNode_node_beg.outhead;
                if(head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                    do
                    {
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target iteratedPathToIntNode_alt_0_recursive_node_intermediate from iteratedPathToIntNode_alt_0_recursive_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.target;
                        if(candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.type.TypeID!=0) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate)))
                        {
                            continue;
                        }
                        if((candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_iteratedPathToIntNode taskFor__subpattern0 = PatternAction_iteratedPathToIntNode.getNewTask(graph, openTasks);
                        taskFor__subpattern0.iteratedPathToIntNode_node_beg = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                        prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_iteratedPathToIntNode.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive match = new Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode_alt_0_recursive();
                                match._node_beg = candidate_iteratedPathToIntNode_node_beg;
                                match._node_intermediate = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                                match._edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                                match.__subpattern0 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<GRGEN_LIBGR.IMatch>>();
                            } else {
                                foreach(Stack<GRGEN_LIBGR.IMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                                candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                            candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                            continue;
                        }
                        candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags = candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_node_intermediate;
                        candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0;
                    }
                    while( (candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 = candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0.outNext) != head_candidate_iteratedPathToIntNode_alt_0_recursive_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_create : GRGEN_LGSP.LGSPAction
    {
        public Action_create() {
            rulePattern = Rule_create.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_create.Match_create>(this);
        }

        public override string Name { get { return "create"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_create.Match_create> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_create instance = new Action_create();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_create.Match_create match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_find : GRGEN_LGSP.LGSPAction
    {
        public Action_find() {
            rulePattern = Rule_find.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_find.Match_find>(this);
        }

        public override string Name { get { return "find"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_find.Match_find> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_find instance = new Action_find();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup find_edge__edge0 
            int type_id_candidate_find_edge__edge0 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_find_edge__edge0], candidate_find_edge__edge0 = head_candidate_find_edge__edge0.typeNext; candidate_find_edge__edge0 != head_candidate_find_edge__edge0; candidate_find_edge__edge0 = candidate_find_edge__edge0.typeNext)
            {
                uint prev__candidate_find_edge__edge0;
                prev__candidate_find_edge__edge0 = candidate_find_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_find_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source find_node_beg from find_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_find_node_beg = candidate_find_edge__edge0.source;
                uint prev__candidate_find_node_beg;
                prev__candidate_find_node_beg = candidate_find_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_find_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target find_node__node0 from find_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_find_node__node0 = candidate_find_edge__edge0.target;
                if((candidate_find_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_find_node_beg.flags = candidate_find_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_node_beg;
                    candidate_find_edge__edge0.flags = candidate_find_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge0;
                    continue;
                }
                uint prev__candidate_find_node__node0;
                prev__candidate_find_node__node0 = candidate_find_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_find_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Outgoing find_edge__edge1 from find_node__node0 
                GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge1 = candidate_find_node__node0.outhead;
                if(head_candidate_find_edge__edge1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_find_edge__edge1 = head_candidate_find_edge__edge1;
                    do
                    {
                        if(candidate_find_edge__edge1.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_find_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        uint prev__candidate_find_edge__edge1;
                        prev__candidate_find_edge__edge1 = candidate_find_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_find_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        // Implicit Target find_node_end from find_edge__edge1 
                        GRGEN_LGSP.LGSPNode candidate_find_node_end = candidate_find_edge__edge1.target;
                        if((candidate_find_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            candidate_find_edge__edge1.flags = candidate_find_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge1;
                            continue;
                        }
                        uint prev__candidate_find_node_end;
                        prev__candidate_find_node_end = candidate_find_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_find_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        // Extend Outgoing find_edge__edge3 from find_node_end 
                        GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge3 = candidate_find_node_end.outhead;
                        if(head_candidate_find_edge__edge3 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_find_edge__edge3 = head_candidate_find_edge__edge3;
                            do
                            {
                                if(candidate_find_edge__edge3.type.TypeID!=1) {
                                    continue;
                                }
                                if((candidate_find_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                uint prev__candidate_find_edge__edge3;
                                prev__candidate_find_edge__edge3 = candidate_find_edge__edge3.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                candidate_find_edge__edge3.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                // Implicit Target find_node__node1 from find_edge__edge3 
                                GRGEN_LGSP.LGSPNode candidate_find_node__node1 = candidate_find_edge__edge3.target;
                                if((candidate_find_node__node1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    candidate_find_edge__edge3.flags = candidate_find_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge3;
                                    continue;
                                }
                                // Extend Outgoing find_edge__edge2 from find_node__node1 
                                GRGEN_LGSP.LGSPEdge head_candidate_find_edge__edge2 = candidate_find_node__node1.outhead;
                                if(head_candidate_find_edge__edge2 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_find_edge__edge2 = head_candidate_find_edge__edge2;
                                    do
                                    {
                                        if(candidate_find_edge__edge2.type.TypeID!=1) {
                                            continue;
                                        }
                                        if(candidate_find_edge__edge2.target != candidate_find_node_beg) {
                                            continue;
                                        }
                                        if((candidate_find_edge__edge2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                        {
                                            continue;
                                        }
                                        Rule_find.Match_find match = matches.GetNextUnfilledPosition();
                                        match._node_beg = candidate_find_node_beg;
                                        match._node__node0 = candidate_find_node__node0;
                                        match._node_end = candidate_find_node_end;
                                        match._node__node1 = candidate_find_node__node1;
                                        match._edge__edge0 = candidate_find_edge__edge0;
                                        match._edge__edge1 = candidate_find_edge__edge1;
                                        match._edge__edge2 = candidate_find_edge__edge2;
                                        match._edge__edge3 = candidate_find_edge__edge3;
                                        matches.PositionWasFilledFixIt();
                                        // if enough matches were found, we leave
                                        if(maxMatches > 0 && matches.Count >= maxMatches)
                                        {
                                            candidate_find_node__node1.MoveOutHeadAfter(candidate_find_edge__edge2);
                                            candidate_find_node_end.MoveOutHeadAfter(candidate_find_edge__edge3);
                                            candidate_find_node__node0.MoveOutHeadAfter(candidate_find_edge__edge1);
                                            graph.MoveHeadAfter(candidate_find_edge__edge0);
                                            candidate_find_edge__edge3.flags = candidate_find_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge3;
                                            candidate_find_node_end.flags = candidate_find_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_node_end;
                                            candidate_find_edge__edge1.flags = candidate_find_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge1;
                                            candidate_find_node__node0.flags = candidate_find_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_node__node0;
                                            candidate_find_node_beg.flags = candidate_find_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_node_beg;
                                            candidate_find_edge__edge0.flags = candidate_find_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge0;
                                            return matches;
                                        }
                                    }
                                    while( (candidate_find_edge__edge2 = candidate_find_edge__edge2.outNext) != head_candidate_find_edge__edge2 );
                                }
                                candidate_find_edge__edge3.flags = candidate_find_edge__edge3.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge3;
                            }
                            while( (candidate_find_edge__edge3 = candidate_find_edge__edge3.outNext) != head_candidate_find_edge__edge3 );
                        }
                        candidate_find_node_end.flags = candidate_find_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_node_end;
                        candidate_find_edge__edge1.flags = candidate_find_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge1;
                    }
                    while( (candidate_find_edge__edge1 = candidate_find_edge__edge1.outNext) != head_candidate_find_edge__edge1 );
                }
                candidate_find_node__node0.flags = candidate_find_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_node__node0;
                candidate_find_node_beg.flags = candidate_find_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_node_beg;
                candidate_find_edge__edge0.flags = candidate_find_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_find_edge__edge0;
            }
            return matches;
        }
    }

    public class Action_findIndependent : GRGEN_LGSP.LGSPAction
    {
        public Action_findIndependent() {
            rulePattern = Rule_findIndependent.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findIndependent.Match_findIndependent>(this);
        }

        public override string Name { get { return "findIndependent"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findIndependent.Match_findIndependent> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findIndependent instance = new Action_findIndependent();
        private Rule_findIndependent.Match_findIndependent_idpt_0 matched_independent_findIndependent_idpt_0 = new Rule_findIndependent.Match_findIndependent_idpt_0();        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findIndependent_edge__edge1 
            int type_id_candidate_findIndependent_edge__edge1 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_findIndependent_edge__edge1], candidate_findIndependent_edge__edge1 = head_candidate_findIndependent_edge__edge1.typeNext; candidate_findIndependent_edge__edge1 != head_candidate_findIndependent_edge__edge1; candidate_findIndependent_edge__edge1 = candidate_findIndependent_edge__edge1.typeNext)
            {
                uint prev__candidate_findIndependent_edge__edge1;
                prev__candidate_findIndependent_edge__edge1 = candidate_findIndependent_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findIndependent_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source findIndependent_node__node0 from findIndependent_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_findIndependent_node__node0 = candidate_findIndependent_edge__edge1.source;
                uint prev__candidate_findIndependent_node__node0;
                prev__candidate_findIndependent_node__node0 = candidate_findIndependent_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findIndependent_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target findIndependent_node_end from findIndependent_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_findIndependent_node_end = candidate_findIndependent_edge__edge1.target;
                if((candidate_findIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_findIndependent_node__node0.flags = candidate_findIndependent_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_node__node0;
                    candidate_findIndependent_edge__edge1.flags = candidate_findIndependent_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_edge__edge1;
                    continue;
                }
                uint prev__candidate_findIndependent_node_end;
                prev__candidate_findIndependent_node_end = candidate_findIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming findIndependent_edge__edge0 from findIndependent_node__node0 
                GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_edge__edge0 = candidate_findIndependent_node__node0.inhead;
                if(head_candidate_findIndependent_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_findIndependent_edge__edge0 = head_candidate_findIndependent_edge__edge0;
                    do
                    {
                        if(candidate_findIndependent_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_findIndependent_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Implicit Source findIndependent_node_beg from findIndependent_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_findIndependent_node_beg = candidate_findIndependent_edge__edge0.source;
                        if((candidate_findIndependent_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // IndependentPattern 
                        {
                            ++negLevel;
                            uint prev_findIndependent__candidate_findIndependent_node_beg;
                            prev_findIndependent__candidate_findIndependent_node_beg = candidate_findIndependent_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_findIndependent_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            if((candidate_findIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                candidate_findIndependent_node_beg.flags = candidate_findIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_node_beg;
                                --negLevel;
                                goto label0;
                            }
                            uint prev_findIndependent__candidate_findIndependent_node_end;
                            prev_findIndependent__candidate_findIndependent_node_end = candidate_findIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_findIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            // Extend Outgoing findIndependent_idpt_0_edge__edge1 from findIndependent_node_end 
                            GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_idpt_0_edge__edge1 = candidate_findIndependent_node_end.outhead;
                            if(head_candidate_findIndependent_idpt_0_edge__edge1 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_findIndependent_idpt_0_edge__edge1 = head_candidate_findIndependent_idpt_0_edge__edge1;
                                do
                                {
                                    if(candidate_findIndependent_idpt_0_edge__edge1.type.TypeID!=1) {
                                        continue;
                                    }
                                    uint prev_findIndependent__candidate_findIndependent_idpt_0_edge__edge1;
                                    prev_findIndependent__candidate_findIndependent_idpt_0_edge__edge1 = candidate_findIndependent_idpt_0_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_findIndependent_idpt_0_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    // Implicit Target findIndependent_idpt_0_node__node0 from findIndependent_idpt_0_edge__edge1 
                                    GRGEN_LGSP.LGSPNode candidate_findIndependent_idpt_0_node__node0 = candidate_findIndependent_idpt_0_edge__edge1.target;
                                    if((candidate_findIndependent_idpt_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                    {
                                        candidate_findIndependent_idpt_0_edge__edge1.flags = candidate_findIndependent_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_idpt_0_edge__edge1;
                                        continue;
                                    }
                                    // Extend Outgoing findIndependent_idpt_0_edge__edge0 from findIndependent_idpt_0_node__node0 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findIndependent_idpt_0_edge__edge0 = candidate_findIndependent_idpt_0_node__node0.outhead;
                                    if(head_candidate_findIndependent_idpt_0_edge__edge0 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findIndependent_idpt_0_edge__edge0 = head_candidate_findIndependent_idpt_0_edge__edge0;
                                        do
                                        {
                                            if(candidate_findIndependent_idpt_0_edge__edge0.type.TypeID!=1) {
                                                continue;
                                            }
                                            if(candidate_findIndependent_idpt_0_edge__edge0.target != candidate_findIndependent_node_beg) {
                                                continue;
                                            }
                                            if((candidate_findIndependent_idpt_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                            {
                                                continue;
                                            }
                                            // independent pattern found
                                            matched_independent_findIndependent_idpt_0._node__node0 = candidate_findIndependent_idpt_0_node__node0;
                                            matched_independent_findIndependent_idpt_0._node_beg = candidate_findIndependent_node_beg;
                                            matched_independent_findIndependent_idpt_0._node_end = candidate_findIndependent_node_end;
                                            matched_independent_findIndependent_idpt_0._edge__edge0 = candidate_findIndependent_idpt_0_edge__edge0;
                                            matched_independent_findIndependent_idpt_0._edge__edge1 = candidate_findIndependent_idpt_0_edge__edge1;
                                            candidate_findIndependent_idpt_0_edge__edge1.flags = candidate_findIndependent_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_idpt_0_edge__edge1;
                                            candidate_findIndependent_node_end.flags = candidate_findIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_node_end;
                                            candidate_findIndependent_node_beg.flags = candidate_findIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_node_beg;
                                            --negLevel;
                                            goto label1;
                                        }
                                        while( (candidate_findIndependent_idpt_0_edge__edge0 = candidate_findIndependent_idpt_0_edge__edge0.outNext) != head_candidate_findIndependent_idpt_0_edge__edge0 );
                                    }
                                    candidate_findIndependent_idpt_0_edge__edge1.flags = candidate_findIndependent_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_idpt_0_edge__edge1;
                                }
                                while( (candidate_findIndependent_idpt_0_edge__edge1 = candidate_findIndependent_idpt_0_edge__edge1.outNext) != head_candidate_findIndependent_idpt_0_edge__edge1 );
                            }
                            candidate_findIndependent_node_end.flags = candidate_findIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_node_end;
                            candidate_findIndependent_node_beg.flags = candidate_findIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findIndependent__candidate_findIndependent_node_beg;
                            --negLevel;
                        }
label0: ;
                        goto label2;
label1: ;
                        Rule_findIndependent.Match_findIndependent match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_findIndependent_node_beg;
                        match._node__node0 = candidate_findIndependent_node__node0;
                        match._node_end = candidate_findIndependent_node_end;
                        match._edge__edge0 = candidate_findIndependent_edge__edge0;
                        match._edge__edge1 = candidate_findIndependent_edge__edge1;
                        match._idpt_0 = matched_independent_findIndependent_idpt_0;
                        matched_independent_findIndependent_idpt_0 = new Rule_findIndependent.Match_findIndependent_idpt_0();
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findIndependent_node__node0.MoveInHeadAfter(candidate_findIndependent_edge__edge0);
                            graph.MoveHeadAfter(candidate_findIndependent_edge__edge1);
                            candidate_findIndependent_node_end.flags = candidate_findIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_node_end;
                            candidate_findIndependent_node__node0.flags = candidate_findIndependent_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_node__node0;
                            candidate_findIndependent_edge__edge1.flags = candidate_findIndependent_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_edge__edge1;
                            return matches;
                        }
label2: ;
                    }
                    while( (candidate_findIndependent_edge__edge0 = candidate_findIndependent_edge__edge0.inNext) != head_candidate_findIndependent_edge__edge0 );
                }
                candidate_findIndependent_node_end.flags = candidate_findIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_node_end;
                candidate_findIndependent_node__node0.flags = candidate_findIndependent_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_node__node0;
                candidate_findIndependent_edge__edge1.flags = candidate_findIndependent_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findIndependent_edge__edge1;
            }
            return matches;
        }
    }

    public class Action_findMultiNested : GRGEN_LGSP.LGSPAction
    {
        public Action_findMultiNested() {
            rulePattern = Rule_findMultiNested.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findMultiNested.Match_findMultiNested>(this);
        }

        public override string Name { get { return "findMultiNested"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findMultiNested.Match_findMultiNested> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findMultiNested instance = new Action_findMultiNested();
        private Rule_findMultiNested.Match_findMultiNested_idpt_0 matched_independent_findMultiNested_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_0();        private Rule_findMultiNested.Match_findMultiNested_idpt_1 matched_independent_findMultiNested_idpt_1 = new Rule_findMultiNested.Match_findMultiNested_idpt_1();        private Rule_findMultiNested.Match_findMultiNested_idpt_0_idpt_0 matched_independent_findMultiNested_idpt_0_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_0_idpt_0();        private Rule_findMultiNested.Match_findMultiNested_idpt_1_idpt_0 matched_independent_findMultiNested_idpt_1_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_1_idpt_0();        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup findMultiNested_edge__edge1 
            int type_id_candidate_findMultiNested_edge__edge1 = 1;
            for(GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_edge__edge1 = graph.edgesByTypeHeads[type_id_candidate_findMultiNested_edge__edge1], candidate_findMultiNested_edge__edge1 = head_candidate_findMultiNested_edge__edge1.typeNext; candidate_findMultiNested_edge__edge1 != head_candidate_findMultiNested_edge__edge1; candidate_findMultiNested_edge__edge1 = candidate_findMultiNested_edge__edge1.typeNext)
            {
                uint prev__candidate_findMultiNested_edge__edge1;
                prev__candidate_findMultiNested_edge__edge1 = candidate_findMultiNested_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findMultiNested_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source findMultiNested_node__node0 from findMultiNested_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_findMultiNested_node__node0 = candidate_findMultiNested_edge__edge1.source;
                uint prev__candidate_findMultiNested_node__node0;
                prev__candidate_findMultiNested_node__node0 = candidate_findMultiNested_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findMultiNested_node__node0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target findMultiNested_node_end from findMultiNested_edge__edge1 
                GRGEN_LGSP.LGSPNode candidate_findMultiNested_node_end = candidate_findMultiNested_edge__edge1.target;
                if((candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_findMultiNested_node__node0.flags = candidate_findMultiNested_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_node__node0;
                    candidate_findMultiNested_edge__edge1.flags = candidate_findMultiNested_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_edge__edge1;
                    continue;
                }
                uint prev__candidate_findMultiNested_node_end;
                prev__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findMultiNested_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming findMultiNested_edge__edge0 from findMultiNested_node__node0 
                GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_edge__edge0 = candidate_findMultiNested_node__node0.inhead;
                if(head_candidate_findMultiNested_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_findMultiNested_edge__edge0 = head_candidate_findMultiNested_edge__edge0;
                    do
                    {
                        if(candidate_findMultiNested_edge__edge0.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_findMultiNested_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Implicit Source findMultiNested_node_beg from findMultiNested_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_findMultiNested_node_beg = candidate_findMultiNested_edge__edge0.source;
                        if((candidate_findMultiNested_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // IndependentPattern 
                        {
                            ++negLevel;
                            uint prev_findMultiNested__candidate_findMultiNested_node_beg;
                            prev_findMultiNested__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_findMultiNested_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            if((candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                                --negLevel;
                                goto label3;
                            }
                            uint prev_findMultiNested__candidate_findMultiNested_node_end;
                            prev_findMultiNested__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_findMultiNested_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            // IndependentPattern 
                            {
                                ++negLevel;
                                uint prev_idpt_0findMultiNested__candidate_findMultiNested_node_beg;
                                prev_idpt_0findMultiNested__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                candidate_findMultiNested_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                if((candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_node_beg;
                                    --negLevel;
                                    goto label4;
                                }
                                uint prev_idpt_0findMultiNested__candidate_findMultiNested_node_end;
                                prev_idpt_0findMultiNested__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                candidate_findMultiNested_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                // Extend Outgoing findMultiNested_idpt_0_idpt_0_edge__edge0 from findMultiNested_node_beg 
                                GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge0 = candidate_findMultiNested_node_beg.outhead;
                                if(head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge0 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_idpt_0_edge__edge0 = head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge0;
                                    do
                                    {
                                        if(candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.type.TypeID!=1) {
                                            continue;
                                        }
                                        uint prev_idpt_0findMultiNested__candidate_findMultiNested_idpt_0_idpt_0_edge__edge0;
                                        prev_idpt_0findMultiNested__candidate_findMultiNested_idpt_0_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        // Implicit Target findMultiNested_idpt_0_idpt_0_node__node0 from findMultiNested_idpt_0_idpt_0_edge__edge0 
                                        GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_0_idpt_0_node__node0 = candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.target;
                                        if((candidate_findMultiNested_idpt_0_idpt_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                        {
                                            candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags = candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_idpt_0_idpt_0_edge__edge0;
                                            continue;
                                        }
                                        // Extend Outgoing findMultiNested_idpt_0_idpt_0_edge__edge1 from findMultiNested_idpt_0_idpt_0_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_0_idpt_0_node__node0.outhead;
                                        if(head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge1 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_idpt_0_edge__edge1 = head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge1;
                                            do
                                            {
                                                if(candidate_findMultiNested_idpt_0_idpt_0_edge__edge1.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if(candidate_findMultiNested_idpt_0_idpt_0_edge__edge1.target != candidate_findMultiNested_node_end) {
                                                    continue;
                                                }
                                                if((candidate_findMultiNested_idpt_0_idpt_0_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                                {
                                                    continue;
                                                }
                                                // independent pattern found
                                                matched_independent_findMultiNested_idpt_0_idpt_0._node_beg = candidate_findMultiNested_node_beg;
                                                matched_independent_findMultiNested_idpt_0_idpt_0._node__node0 = candidate_findMultiNested_idpt_0_idpt_0_node__node0;
                                                matched_independent_findMultiNested_idpt_0_idpt_0._node_end = candidate_findMultiNested_node_end;
                                                matched_independent_findMultiNested_idpt_0_idpt_0._edge__edge0 = candidate_findMultiNested_idpt_0_idpt_0_edge__edge0;
                                                matched_independent_findMultiNested_idpt_0_idpt_0._edge__edge1 = candidate_findMultiNested_idpt_0_idpt_0_edge__edge1;
                                                candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags = candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_idpt_0_idpt_0_edge__edge0;
                                                candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_node_end;
                                                candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_node_beg;
                                                --negLevel;
                                                goto label5;
                                            }
                                            while( (candidate_findMultiNested_idpt_0_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_0_idpt_0_edge__edge1.outNext) != head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge1 );
                                        }
                                        candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags = candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_idpt_0_idpt_0_edge__edge0;
                                    }
                                    while( (candidate_findMultiNested_idpt_0_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_0_idpt_0_edge__edge0.outNext) != head_candidate_findMultiNested_idpt_0_idpt_0_edge__edge0 );
                                }
                                candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_node_end;
                                candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_0findMultiNested__candidate_findMultiNested_node_beg;
                                --negLevel;
                            }
label4: ;
                            candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                            --negLevel;
                            goto label6;
label5: ;
                            // Extend Outgoing findMultiNested_idpt_0_edge__edge1 from findMultiNested_node_end 
                            GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_edge__edge1 = candidate_findMultiNested_node_end.outhead;
                            if(head_candidate_findMultiNested_idpt_0_edge__edge1 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_edge__edge1 = head_candidate_findMultiNested_idpt_0_edge__edge1;
                                do
                                {
                                    if(candidate_findMultiNested_idpt_0_edge__edge1.type.TypeID!=1) {
                                        continue;
                                    }
                                    uint prev_findMultiNested__candidate_findMultiNested_idpt_0_edge__edge1;
                                    prev_findMultiNested__candidate_findMultiNested_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_0_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_findMultiNested_idpt_0_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    // Implicit Target findMultiNested_idpt_0_node__node0 from findMultiNested_idpt_0_edge__edge1 
                                    GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_0_node__node0 = candidate_findMultiNested_idpt_0_edge__edge1.target;
                                    if((candidate_findMultiNested_idpt_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                    {
                                        candidate_findMultiNested_idpt_0_edge__edge1.flags = candidate_findMultiNested_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_idpt_0_edge__edge1;
                                        continue;
                                    }
                                    // Extend Outgoing findMultiNested_idpt_0_edge__edge0 from findMultiNested_idpt_0_node__node0 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_0_node__node0.outhead;
                                    if(head_candidate_findMultiNested_idpt_0_edge__edge0 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_0_edge__edge0 = head_candidate_findMultiNested_idpt_0_edge__edge0;
                                        do
                                        {
                                            if(candidate_findMultiNested_idpt_0_edge__edge0.type.TypeID!=1) {
                                                continue;
                                            }
                                            if(candidate_findMultiNested_idpt_0_edge__edge0.target != candidate_findMultiNested_node_beg) {
                                                continue;
                                            }
                                            if((candidate_findMultiNested_idpt_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                            {
                                                continue;
                                            }
                                            // independent pattern found
                                            matched_independent_findMultiNested_idpt_0._node__node0 = candidate_findMultiNested_idpt_0_node__node0;
                                            matched_independent_findMultiNested_idpt_0._node_beg = candidate_findMultiNested_node_beg;
                                            matched_independent_findMultiNested_idpt_0._node_end = candidate_findMultiNested_node_end;
                                            matched_independent_findMultiNested_idpt_0._edge__edge0 = candidate_findMultiNested_idpt_0_edge__edge0;
                                            matched_independent_findMultiNested_idpt_0._edge__edge1 = candidate_findMultiNested_idpt_0_edge__edge1;
                                            matched_independent_findMultiNested_idpt_0._idpt_0 = matched_independent_findMultiNested_idpt_0_idpt_0;
                                            matched_independent_findMultiNested_idpt_0_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_0_idpt_0();
                                            candidate_findMultiNested_idpt_0_edge__edge1.flags = candidate_findMultiNested_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_idpt_0_edge__edge1;
                                            candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_end;
                                            candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                                            --negLevel;
                                            goto label7;
                                        }
                                        while( (candidate_findMultiNested_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_0_edge__edge0.outNext) != head_candidate_findMultiNested_idpt_0_edge__edge0 );
                                    }
                                    candidate_findMultiNested_idpt_0_edge__edge1.flags = candidate_findMultiNested_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_idpt_0_edge__edge1;
                                }
                                while( (candidate_findMultiNested_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_0_edge__edge1.outNext) != head_candidate_findMultiNested_idpt_0_edge__edge1 );
                            }
                            candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                            --negLevel;
                        }
label3: ;
                        goto label8;
label7: ;
                        // IndependentPattern 
                        {
                            ++negLevel;
                            uint prev_findMultiNested__candidate_findMultiNested_node_beg;
                            prev_findMultiNested__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_findMultiNested_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            if((candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                                --negLevel;
                                goto label9;
                            }
                            uint prev_findMultiNested__candidate_findMultiNested_node_end;
                            prev_findMultiNested__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            candidate_findMultiNested_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                            // IndependentPattern 
                            {
                                ++negLevel;
                                uint prev_idpt_1findMultiNested__candidate_findMultiNested_node_beg;
                                prev_idpt_1findMultiNested__candidate_findMultiNested_node_beg = candidate_findMultiNested_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                candidate_findMultiNested_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                if((candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_node_beg;
                                    --negLevel;
                                    goto label10;
                                }
                                uint prev_idpt_1findMultiNested__candidate_findMultiNested_node_end;
                                prev_idpt_1findMultiNested__candidate_findMultiNested_node_end = candidate_findMultiNested_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                candidate_findMultiNested_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                // Extend Outgoing findMultiNested_idpt_1_idpt_0_edge__edge1 from findMultiNested_node_end 
                                GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge1 = candidate_findMultiNested_node_end.outhead;
                                if(head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge1 != null)
                                {
                                    GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_1_idpt_0_edge__edge1 = head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge1;
                                    do
                                    {
                                        if(candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.type.TypeID!=1) {
                                            continue;
                                        }
                                        uint prev_idpt_1findMultiNested__candidate_findMultiNested_idpt_1_idpt_0_edge__edge1;
                                        prev_idpt_1findMultiNested__candidate_findMultiNested_idpt_1_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                        // Implicit Target findMultiNested_idpt_1_idpt_0_node__node0 from findMultiNested_idpt_1_idpt_0_edge__edge1 
                                        GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_1_idpt_0_node__node0 = candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.target;
                                        if((candidate_findMultiNested_idpt_1_idpt_0_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                        {
                                            candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags = candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_idpt_1_idpt_0_edge__edge1;
                                            continue;
                                        }
                                        // Extend Outgoing findMultiNested_idpt_1_idpt_0_edge__edge0 from findMultiNested_idpt_1_idpt_0_node__node0 
                                        GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_1_idpt_0_node__node0.outhead;
                                        if(head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge0 != null)
                                        {
                                            GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_1_idpt_0_edge__edge0 = head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge0;
                                            do
                                            {
                                                if(candidate_findMultiNested_idpt_1_idpt_0_edge__edge0.type.TypeID!=1) {
                                                    continue;
                                                }
                                                if(candidate_findMultiNested_idpt_1_idpt_0_edge__edge0.target != candidate_findMultiNested_node_beg) {
                                                    continue;
                                                }
                                                if((candidate_findMultiNested_idpt_1_idpt_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                                {
                                                    continue;
                                                }
                                                // independent pattern found
                                                matched_independent_findMultiNested_idpt_1_idpt_0._node__node0 = candidate_findMultiNested_idpt_1_idpt_0_node__node0;
                                                matched_independent_findMultiNested_idpt_1_idpt_0._node_beg = candidate_findMultiNested_node_beg;
                                                matched_independent_findMultiNested_idpt_1_idpt_0._node_end = candidate_findMultiNested_node_end;
                                                matched_independent_findMultiNested_idpt_1_idpt_0._edge__edge0 = candidate_findMultiNested_idpt_1_idpt_0_edge__edge0;
                                                matched_independent_findMultiNested_idpt_1_idpt_0._edge__edge1 = candidate_findMultiNested_idpt_1_idpt_0_edge__edge1;
                                                candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags = candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_idpt_1_idpt_0_edge__edge1;
                                                candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_node_end;
                                                candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_node_beg;
                                                --negLevel;
                                                goto label11;
                                            }
                                            while( (candidate_findMultiNested_idpt_1_idpt_0_edge__edge0 = candidate_findMultiNested_idpt_1_idpt_0_edge__edge0.outNext) != head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge0 );
                                        }
                                        candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags = candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_idpt_1_idpt_0_edge__edge1;
                                    }
                                    while( (candidate_findMultiNested_idpt_1_idpt_0_edge__edge1 = candidate_findMultiNested_idpt_1_idpt_0_edge__edge1.outNext) != head_candidate_findMultiNested_idpt_1_idpt_0_edge__edge1 );
                                }
                                candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_node_end;
                                candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_idpt_1findMultiNested__candidate_findMultiNested_node_beg;
                                --negLevel;
                            }
label10: ;
                            candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                            --negLevel;
                            goto label12;
label11: ;
                            // Extend Outgoing findMultiNested_idpt_1_edge__edge0 from findMultiNested_node_beg 
                            GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_1_edge__edge0 = candidate_findMultiNested_node_beg.outhead;
                            if(head_candidate_findMultiNested_idpt_1_edge__edge0 != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_1_edge__edge0 = head_candidate_findMultiNested_idpt_1_edge__edge0;
                                do
                                {
                                    if(candidate_findMultiNested_idpt_1_edge__edge0.type.TypeID!=1) {
                                        continue;
                                    }
                                    uint prev_findMultiNested__candidate_findMultiNested_idpt_1_edge__edge0;
                                    prev_findMultiNested__candidate_findMultiNested_idpt_1_edge__edge0 = candidate_findMultiNested_idpt_1_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    candidate_findMultiNested_idpt_1_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                                    // Implicit Target findMultiNested_idpt_1_node__node0 from findMultiNested_idpt_1_edge__edge0 
                                    GRGEN_LGSP.LGSPNode candidate_findMultiNested_idpt_1_node__node0 = candidate_findMultiNested_idpt_1_edge__edge0.target;
                                    if((candidate_findMultiNested_idpt_1_node__node0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                    {
                                        candidate_findMultiNested_idpt_1_edge__edge0.flags = candidate_findMultiNested_idpt_1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_idpt_1_edge__edge0;
                                        continue;
                                    }
                                    // Extend Outgoing findMultiNested_idpt_1_edge__edge1 from findMultiNested_idpt_1_node__node0 
                                    GRGEN_LGSP.LGSPEdge head_candidate_findMultiNested_idpt_1_edge__edge1 = candidate_findMultiNested_idpt_1_node__node0.outhead;
                                    if(head_candidate_findMultiNested_idpt_1_edge__edge1 != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_findMultiNested_idpt_1_edge__edge1 = head_candidate_findMultiNested_idpt_1_edge__edge1;
                                        do
                                        {
                                            if(candidate_findMultiNested_idpt_1_edge__edge1.type.TypeID!=1) {
                                                continue;
                                            }
                                            if(candidate_findMultiNested_idpt_1_edge__edge1.target != candidate_findMultiNested_node_end) {
                                                continue;
                                            }
                                            if((candidate_findMultiNested_idpt_1_edge__edge1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                            {
                                                continue;
                                            }
                                            // independent pattern found
                                            matched_independent_findMultiNested_idpt_1._node_beg = candidate_findMultiNested_node_beg;
                                            matched_independent_findMultiNested_idpt_1._node__node0 = candidate_findMultiNested_idpt_1_node__node0;
                                            matched_independent_findMultiNested_idpt_1._node_end = candidate_findMultiNested_node_end;
                                            matched_independent_findMultiNested_idpt_1._edge__edge0 = candidate_findMultiNested_idpt_1_edge__edge0;
                                            matched_independent_findMultiNested_idpt_1._edge__edge1 = candidate_findMultiNested_idpt_1_edge__edge1;
                                            matched_independent_findMultiNested_idpt_1._idpt_0 = matched_independent_findMultiNested_idpt_1_idpt_0;
                                            matched_independent_findMultiNested_idpt_1_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_1_idpt_0();
                                            candidate_findMultiNested_idpt_1_edge__edge0.flags = candidate_findMultiNested_idpt_1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_idpt_1_edge__edge0;
                                            candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_end;
                                            candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                                            --negLevel;
                                            goto label13;
                                        }
                                        while( (candidate_findMultiNested_idpt_1_edge__edge1 = candidate_findMultiNested_idpt_1_edge__edge1.outNext) != head_candidate_findMultiNested_idpt_1_edge__edge1 );
                                    }
                                    candidate_findMultiNested_idpt_1_edge__edge0.flags = candidate_findMultiNested_idpt_1_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_idpt_1_edge__edge0;
                                }
                                while( (candidate_findMultiNested_idpt_1_edge__edge0 = candidate_findMultiNested_idpt_1_edge__edge0.outNext) != head_candidate_findMultiNested_idpt_1_edge__edge0 );
                            }
                            candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node_beg.flags = candidate_findMultiNested_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev_findMultiNested__candidate_findMultiNested_node_beg;
                            --negLevel;
                        }
label9: ;
                        goto label14;
label13: ;
                        Rule_findMultiNested.Match_findMultiNested match = matches.GetNextUnfilledPosition();
                        match._node_beg = candidate_findMultiNested_node_beg;
                        match._node__node0 = candidate_findMultiNested_node__node0;
                        match._node_end = candidate_findMultiNested_node_end;
                        match._edge__edge0 = candidate_findMultiNested_edge__edge0;
                        match._edge__edge1 = candidate_findMultiNested_edge__edge1;
                        match._idpt_0 = matched_independent_findMultiNested_idpt_0;
                        matched_independent_findMultiNested_idpt_0 = new Rule_findMultiNested.Match_findMultiNested_idpt_0();
                        match._idpt_1 = matched_independent_findMultiNested_idpt_1;
                        matched_independent_findMultiNested_idpt_1 = new Rule_findMultiNested.Match_findMultiNested_idpt_1();
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findMultiNested_node__node0.MoveInHeadAfter(candidate_findMultiNested_edge__edge0);
                            graph.MoveHeadAfter(candidate_findMultiNested_edge__edge1);
                            candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_node_end;
                            candidate_findMultiNested_node__node0.flags = candidate_findMultiNested_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_node__node0;
                            candidate_findMultiNested_edge__edge1.flags = candidate_findMultiNested_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_edge__edge1;
                            return matches;
                        }
label6: ;
label8: ;
label12: ;
label14: ;
                    }
                    while( (candidate_findMultiNested_edge__edge0 = candidate_findMultiNested_edge__edge0.inNext) != head_candidate_findMultiNested_edge__edge0 );
                }
                candidate_findMultiNested_node_end.flags = candidate_findMultiNested_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_node_end;
                candidate_findMultiNested_node__node0.flags = candidate_findMultiNested_node__node0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_node__node0;
                candidate_findMultiNested_edge__edge1.flags = candidate_findMultiNested_edge__edge1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findMultiNested_edge__edge1;
            }
            return matches;
        }
    }

    public class Action_createIterated : GRGEN_LGSP.LGSPAction
    {
        public Action_createIterated() {
            rulePattern = Rule_createIterated.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_createIterated.Match_createIterated>(this);
        }

        public override string Name { get { return "createIterated"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_createIterated.Match_createIterated> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_createIterated instance = new Action_createIterated();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_createIterated.Match_createIterated match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_findChainPlusChainToInt : GRGEN_LGSP.LGSPAction
    {
        public Action_findChainPlusChainToInt() {
            rulePattern = Rule_findChainPlusChainToInt.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt>(this);
        }

        public override string Name { get { return "findChainPlusChainToInt"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findChainPlusChainToInt instance = new Action_findChainPlusChainToInt();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt patternpath_match_findChainPlusChainToInt = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset findChainPlusChainToInt_node_beg 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToInt_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_findChainPlusChainToInt_node_beg == null) {
                MissingPreset_findChainPlusChainToInt_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_findChainPlusChainToInt_node_beg;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findChainPlusChainToInt_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_findChainPlusChainToInt_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_findChainPlusChainToInt_node_beg) ? 1U : 0U;
                if(prev__candidate_findChainPlusChainToInt_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_findChainPlusChainToInt_node_beg,candidate_findChainPlusChainToInt_node_beg);
            }
            // Preset findChainPlusChainToInt_node_end 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToInt_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_findChainPlusChainToInt_node_end == null) {
                MissingPreset_findChainPlusChainToInt_node_end(graph, maxMatches, parameters, null, null, null, candidate_findChainPlusChainToInt_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                    } else { 
                        if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                } else { 
                    if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_findChainPlusChainToInt_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_findChainPlusChainToInt_node_end)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                } else { 
                    if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                    }
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern1
            PatternAction_iteratedPathToIntNode taskFor__subpattern1 = PatternAction_iteratedPathToIntNode.getNewTask(graph, openTasks);
            taskFor__subpattern1.iteratedPathToIntNode_node_beg = candidate_findChainPlusChainToInt_node_end;
            openTasks.Push(taskFor__subpattern1);
            // Push subpattern matching task for _subpattern0
            PatternAction_iteratedPath taskFor__subpattern0 = PatternAction_iteratedPath.getNewTask(graph, openTasks);
            taskFor__subpattern0.iteratedPath_node_beg = candidate_findChainPlusChainToInt_node_beg;
            taskFor__subpattern0.iteratedPath_node_end = candidate_findChainPlusChainToInt_node_end;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_findChainPlusChainToInt_node_beg;
            prevGlobal__candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_findChainPlusChainToInt_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_findChainPlusChainToInt_node_end;
            prevGlobal__candidate_findChainPlusChainToInt_node_end = candidate_findChainPlusChainToInt_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_findChainPlusChainToInt_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_iteratedPath.releaseTask(taskFor__subpattern0);
            // Pop subpattern matching task for _subpattern1
            openTasks.Pop();
            PatternAction_iteratedPathToIntNode.releaseTask(taskFor__subpattern1);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_findChainPlusChainToInt_node_beg;
                    match._node_end = candidate_findChainPlusChainToInt_node_end;
                    match.__subpattern0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
                    match.__subpattern1 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                    candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                    } else { 
                        if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                } else { 
                    if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                    }
                }
                return matches;
            }
            candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
            candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
            } else { 
                if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_findChainPlusChainToInt_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt patternpath_match_findChainPlusChainToInt = null;
            // Lookup findChainPlusChainToInt_node_beg 
            foreach(GRGEN_LIBGR.NodeType type_candidate_findChainPlusChainToInt_node_beg in NodeType_Node.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_findChainPlusChainToInt_node_beg = type_candidate_findChainPlusChainToInt_node_beg.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_findChainPlusChainToInt_node_beg = graph.nodesByTypeHeads[type_id_candidate_findChainPlusChainToInt_node_beg], candidate_findChainPlusChainToInt_node_beg = head_candidate_findChainPlusChainToInt_node_beg.typeNext; candidate_findChainPlusChainToInt_node_beg != head_candidate_findChainPlusChainToInt_node_beg; candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.typeNext)
                {
                    uint prev__candidate_findChainPlusChainToInt_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev__candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_findChainPlusChainToInt_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev__candidate_findChainPlusChainToInt_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_findChainPlusChainToInt_node_beg) ? 1U : 0U;
                        if(prev__candidate_findChainPlusChainToInt_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_findChainPlusChainToInt_node_beg,candidate_findChainPlusChainToInt_node_beg);
                    }
                    // Preset findChainPlusChainToInt_node_end 
                    GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToInt_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
                    if(candidate_findChainPlusChainToInt_node_end == null) {
                        MissingPreset_findChainPlusChainToInt_node_end(graph, maxMatches, parameters, null, null, null, candidate_findChainPlusChainToInt_node_beg);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                            } else { 
                                if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                                }
                            }
                            return;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                        } else { 
                            if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                            }
                        }
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_findChainPlusChainToInt_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_findChainPlusChainToInt_node_end)))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                        } else { 
                            if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                            }
                        }
                        continue;
                    }
                    // Push subpattern matching task for _subpattern1
                    PatternAction_iteratedPathToIntNode taskFor__subpattern1 = PatternAction_iteratedPathToIntNode.getNewTask(graph, openTasks);
                    taskFor__subpattern1.iteratedPathToIntNode_node_beg = candidate_findChainPlusChainToInt_node_end;
                    openTasks.Push(taskFor__subpattern1);
                    // Push subpattern matching task for _subpattern0
                    PatternAction_iteratedPath taskFor__subpattern0 = PatternAction_iteratedPath.getNewTask(graph, openTasks);
                    taskFor__subpattern0.iteratedPath_node_beg = candidate_findChainPlusChainToInt_node_beg;
                    taskFor__subpattern0.iteratedPath_node_end = candidate_findChainPlusChainToInt_node_end;
                    openTasks.Push(taskFor__subpattern0);
                    uint prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                    prevGlobal__candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToInt_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    uint prevGlobal__candidate_findChainPlusChainToInt_node_end;
                    prevGlobal__candidate_findChainPlusChainToInt_node_end = candidate_findChainPlusChainToInt_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToInt_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for _subpattern0
                    openTasks.Pop();
                    PatternAction_iteratedPath.releaseTask(taskFor__subpattern0);
                    // Pop subpattern matching task for _subpattern1
                    openTasks.Pop();
                    PatternAction_iteratedPathToIntNode.releaseTask(taskFor__subpattern1);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt match = matches.GetNextUnfilledPosition();
                            match._node_beg = candidate_findChainPlusChainToInt_node_beg;
                            match._node_end = candidate_findChainPlusChainToInt_node_end;
                            match.__subpattern0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
                            match.__subpattern1 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
                            matches.PositionWasFilledFixIt();
                        }
                        matchesList.Clear();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                            candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                            } else { 
                                if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                                }
                            }
                            return;
                        }
                        candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                        candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                        } else { 
                            if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                            }
                        }
                        continue;
                    }
                    candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                    candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToInt_node_beg;
                    } else { 
                        if(prev__candidate_findChainPlusChainToInt_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToInt_node_beg);
                        }
                    }
                }
            }
            return;
        }
        public void MissingPreset_findChainPlusChainToInt_node_end(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToInt_node_beg)
        {
            int negLevel = 0;
            Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt patternpath_match_findChainPlusChainToInt = null;
            // Lookup findChainPlusChainToInt_node_end 
            foreach(GRGEN_LIBGR.NodeType type_candidate_findChainPlusChainToInt_node_end in NodeType_Node.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_findChainPlusChainToInt_node_end = type_candidate_findChainPlusChainToInt_node_end.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_findChainPlusChainToInt_node_end = graph.nodesByTypeHeads[type_id_candidate_findChainPlusChainToInt_node_end], candidate_findChainPlusChainToInt_node_end = head_candidate_findChainPlusChainToInt_node_end.typeNext; candidate_findChainPlusChainToInt_node_end != head_candidate_findChainPlusChainToInt_node_end; candidate_findChainPlusChainToInt_node_end = candidate_findChainPlusChainToInt_node_end.typeNext)
                {
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_findChainPlusChainToInt_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_findChainPlusChainToInt_node_end)))
                    {
                        continue;
                    }
                    // Push subpattern matching task for _subpattern1
                    PatternAction_iteratedPathToIntNode taskFor__subpattern1 = PatternAction_iteratedPathToIntNode.getNewTask(graph, openTasks);
                    taskFor__subpattern1.iteratedPathToIntNode_node_beg = candidate_findChainPlusChainToInt_node_end;
                    openTasks.Push(taskFor__subpattern1);
                    // Push subpattern matching task for _subpattern0
                    PatternAction_iteratedPath taskFor__subpattern0 = PatternAction_iteratedPath.getNewTask(graph, openTasks);
                    taskFor__subpattern0.iteratedPath_node_beg = candidate_findChainPlusChainToInt_node_beg;
                    taskFor__subpattern0.iteratedPath_node_end = candidate_findChainPlusChainToInt_node_end;
                    openTasks.Push(taskFor__subpattern0);
                    uint prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                    prevGlobal__candidate_findChainPlusChainToInt_node_beg = candidate_findChainPlusChainToInt_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToInt_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    uint prevGlobal__candidate_findChainPlusChainToInt_node_end;
                    prevGlobal__candidate_findChainPlusChainToInt_node_end = candidate_findChainPlusChainToInt_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToInt_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for _subpattern0
                    openTasks.Pop();
                    PatternAction_iteratedPath.releaseTask(taskFor__subpattern0);
                    // Pop subpattern matching task for _subpattern1
                    openTasks.Pop();
                    PatternAction_iteratedPathToIntNode.releaseTask(taskFor__subpattern1);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_findChainPlusChainToInt.Match_findChainPlusChainToInt match = matches.GetNextUnfilledPosition();
                            match._node_beg = candidate_findChainPlusChainToInt_node_beg;
                            match._node_end = candidate_findChainPlusChainToInt_node_end;
                            match.__subpattern0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
                            match.__subpattern1 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
                            matches.PositionWasFilledFixIt();
                        }
                        matchesList.Clear();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                            candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                            return;
                        }
                        candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                        candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                        continue;
                    }
                    candidate_findChainPlusChainToInt_node_beg.flags = candidate_findChainPlusChainToInt_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_beg;
                    candidate_findChainPlusChainToInt_node_end.flags = candidate_findChainPlusChainToInt_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToInt_node_end;
                }
            }
            return;
        }
    }

    public class Action_findChainPlusChainToIntIndependent : GRGEN_LGSP.LGSPAction
    {
        public Action_findChainPlusChainToIntIndependent() {
            rulePattern = Rule_findChainPlusChainToIntIndependent.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent>(this);
        }

        public override string Name { get { return "findChainPlusChainToIntIndependent"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_findChainPlusChainToIntIndependent instance = new Action_findChainPlusChainToIntIndependent();
        private Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0 matched_independent_findChainPlusChainToIntIndependent_idpt_0 = new Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0();        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0 patternpath_match_findChainPlusChainToIntIndependent_idpt_0 = null;
            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent patternpath_match_findChainPlusChainToIntIndependent = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset findChainPlusChainToIntIndependent_node_beg 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToIntIndependent_node_beg = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_findChainPlusChainToIntIndependent_node_beg == null) {
                MissingPreset_findChainPlusChainToIntIndependent_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_findChainPlusChainToIntIndependent_node_beg;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prev__candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_findChainPlusChainToIntIndependent_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            } else {
                prev__candidate_findChainPlusChainToIntIndependent_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_findChainPlusChainToIntIndependent_node_beg) ? 1U : 0U;
                if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_findChainPlusChainToIntIndependent_node_beg,candidate_findChainPlusChainToIntIndependent_node_beg);
            }
            // Preset findChainPlusChainToIntIndependent_node_end 
            GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToIntIndependent_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
            if(candidate_findChainPlusChainToIntIndependent_node_end == null) {
                MissingPreset_findChainPlusChainToIntIndependent_node_end(graph, maxMatches, parameters, null, null, null, candidate_findChainPlusChainToIntIndependent_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                    } else { 
                        if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                } else { 
                    if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_findChainPlusChainToIntIndependent_node_end)))
            {
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                } else { 
                    if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                    }
                }
                return matches;
            }
            // IndependentPattern 
            {
                ++negLevel;
                if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                    graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                }
                Stack<GRGEN_LGSP.LGSPSubpatternAction> findChainPlusChainToIntIndependent_openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
                List<Stack<GRGEN_LIBGR.IMatch>> findChainPlusChainToIntIndependent_foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
                List<Stack<GRGEN_LIBGR.IMatch>> findChainPlusChainToIntIndependent_matchesList = findChainPlusChainToIntIndependent_foundPartialMatches;
                // Push subpattern matching task for _subpattern0
                PatternAction_iteratedPathToIntNode taskFor_findChainPlusChainToIntIndependent__subpattern0 = PatternAction_iteratedPathToIntNode.getNewTask(graph, findChainPlusChainToIntIndependent_openTasks);
                taskFor_findChainPlusChainToIntIndependent__subpattern0.iteratedPathToIntNode_node_beg = candidate_findChainPlusChainToIntIndependent_node_end;
                findChainPlusChainToIntIndependent_openTasks.Push(taskFor_findChainPlusChainToIntIndependent__subpattern0);
                uint prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_findChainPlusChainToIntIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns of findChainPlusChainToIntIndependent_
                findChainPlusChainToIntIndependent_openTasks.Peek().myMatch(findChainPlusChainToIntIndependent_matchesList, 1, negLevel);
                // Pop subpattern matching task for _subpattern0
                findChainPlusChainToIntIndependent_openTasks.Pop();
                PatternAction_iteratedPathToIntNode.releaseTask(taskFor_findChainPlusChainToIntIndependent__subpattern0);
                // Check whether subpatterns were found 
                if(findChainPlusChainToIntIndependent_matchesList.Count>0) {
                    // independent pattern with contained subpatterns found
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = findChainPlusChainToIntIndependent_matchesList[0];
                    findChainPlusChainToIntIndependent_matchesList.Clear();
                    matched_independent_findChainPlusChainToIntIndependent_idpt_0._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                    matched_independent_findChainPlusChainToIntIndependent_idpt_0.__subpattern0 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
                    candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                    if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                    }
                    --negLevel;
                    goto label15;
                }
                candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                }
                --negLevel;
            }
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
            } else { 
                if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                }
            }
            return matches;
label15: ;
            // Push subpattern matching task for _subpattern0
            PatternAction_iteratedPath taskFor__subpattern0 = PatternAction_iteratedPath.getNewTask(graph, openTasks);
            taskFor__subpattern0.iteratedPath_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg;
            taskFor__subpattern0.iteratedPath_node_end = candidate_findChainPlusChainToIntIndependent_node_end;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
            prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_findChainPlusChainToIntIndependent_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
            prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_findChainPlusChainToIntIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_iteratedPath.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent match = matches.GetNextUnfilledPosition();
                    match._node_beg = candidate_findChainPlusChainToIntIndependent_node_beg;
                    match._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                    match.__subpattern0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
                    match._idpt_0 = matched_independent_findChainPlusChainToIntIndependent_idpt_0;
                    matched_independent_findChainPlusChainToIntIndependent_idpt_0 = new Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0();
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                    } else { 
                        if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                } else { 
                    if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                        graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                    }
                }
                return matches;
            }
            candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
            candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
            } else { 
                if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_findChainPlusChainToIntIndependent_node_beg(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0 patternpath_match_findChainPlusChainToIntIndependent_idpt_0 = null;
            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent patternpath_match_findChainPlusChainToIntIndependent = null;
            // Lookup findChainPlusChainToIntIndependent_node_beg 
            foreach(GRGEN_LIBGR.NodeType type_candidate_findChainPlusChainToIntIndependent_node_beg in NodeType_Node.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_findChainPlusChainToIntIndependent_node_beg = type_candidate_findChainPlusChainToIntIndependent_node_beg.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_findChainPlusChainToIntIndependent_node_beg = graph.nodesByTypeHeads[type_id_candidate_findChainPlusChainToIntIndependent_node_beg], candidate_findChainPlusChainToIntIndependent_node_beg = head_candidate_findChainPlusChainToIntIndependent_node_beg.typeNext; candidate_findChainPlusChainToIntIndependent_node_beg != head_candidate_findChainPlusChainToIntIndependent_node_beg; candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.typeNext)
                {
                    uint prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prev__candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                        candidate_findChainPlusChainToIntIndependent_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    } else {
                        prev__candidate_findChainPlusChainToIntIndependent_node_beg = graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_findChainPlusChainToIntIndependent_node_beg) ? 1U : 0U;
                        if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_findChainPlusChainToIntIndependent_node_beg,candidate_findChainPlusChainToIntIndependent_node_beg);
                    }
                    // Preset findChainPlusChainToIntIndependent_node_end 
                    GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToIntIndependent_node_end = (GRGEN_LGSP.LGSPNode) parameters[1];
                    if(candidate_findChainPlusChainToIntIndependent_node_end == null) {
                        MissingPreset_findChainPlusChainToIntIndependent_node_end(graph, maxMatches, parameters, null, null, null, candidate_findChainPlusChainToIntIndependent_node_beg);
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                            } else { 
                                if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                                }
                            }
                            return;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                        } else { 
                            if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                            }
                        }
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_findChainPlusChainToIntIndependent_node_end)))
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                        } else { 
                            if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                            }
                        }
                        continue;
                    }
                    // IndependentPattern 
                    {
                        ++negLevel;
                        if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                            graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                        }
                        Stack<GRGEN_LGSP.LGSPSubpatternAction> findChainPlusChainToIntIndependent_openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
                        List<Stack<GRGEN_LIBGR.IMatch>> findChainPlusChainToIntIndependent_foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
                        List<Stack<GRGEN_LIBGR.IMatch>> findChainPlusChainToIntIndependent_matchesList = findChainPlusChainToIntIndependent_foundPartialMatches;
                        // Push subpattern matching task for _subpattern0
                        PatternAction_iteratedPathToIntNode taskFor_findChainPlusChainToIntIndependent__subpattern0 = PatternAction_iteratedPathToIntNode.getNewTask(graph, findChainPlusChainToIntIndependent_openTasks);
                        taskFor_findChainPlusChainToIntIndependent__subpattern0.iteratedPathToIntNode_node_beg = candidate_findChainPlusChainToIntIndependent_node_end;
                        findChainPlusChainToIntIndependent_openTasks.Push(taskFor_findChainPlusChainToIntIndependent__subpattern0);
                        uint prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                        prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_findChainPlusChainToIntIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns of findChainPlusChainToIntIndependent_
                        findChainPlusChainToIntIndependent_openTasks.Peek().myMatch(findChainPlusChainToIntIndependent_matchesList, 1, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        findChainPlusChainToIntIndependent_openTasks.Pop();
                        PatternAction_iteratedPathToIntNode.releaseTask(taskFor_findChainPlusChainToIntIndependent__subpattern0);
                        // Check whether subpatterns were found 
                        if(findChainPlusChainToIntIndependent_matchesList.Count>0) {
                            // independent pattern with contained subpatterns found
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = findChainPlusChainToIntIndependent_matchesList[0];
                            findChainPlusChainToIntIndependent_matchesList.Clear();
                            matched_independent_findChainPlusChainToIntIndependent_idpt_0._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                            matched_independent_findChainPlusChainToIntIndependent_idpt_0.__subpattern0 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
                            candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label16;
                        }
                        candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                        if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                        }
                        --negLevel;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                    } else { 
                        if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                        }
                    }
                    goto label17;
label16: ;
                    // Push subpattern matching task for _subpattern0
                    PatternAction_iteratedPath taskFor__subpattern0 = PatternAction_iteratedPath.getNewTask(graph, openTasks);
                    taskFor__subpattern0.iteratedPath_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg;
                    taskFor__subpattern0.iteratedPath_node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                    openTasks.Push(taskFor__subpattern0);
                    uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                    prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                    prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToIntIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for _subpattern0
                    openTasks.Pop();
                    PatternAction_iteratedPath.releaseTask(taskFor__subpattern0);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent match = matches.GetNextUnfilledPosition();
                            match._node_beg = candidate_findChainPlusChainToIntIndependent_node_beg;
                            match._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                            match.__subpattern0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
                            match._idpt_0 = matched_independent_findChainPlusChainToIntIndependent_idpt_0;
                            matched_independent_findChainPlusChainToIntIndependent_idpt_0 = new Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0();
                            matches.PositionWasFilledFixIt();
                        }
                        matchesList.Clear();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                            candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                            } else { 
                                if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                                    graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                                }
                            }
                            return;
                        }
                        candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                        candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                        } else { 
                            if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                            }
                        }
                        goto label18;
                    }
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                    candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_findChainPlusChainToIntIndependent_node_beg;
                    } else { 
                        if(prev__candidate_findChainPlusChainToIntIndependent_node_beg == 0) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_findChainPlusChainToIntIndependent_node_beg);
                        }
                    }
label17: ;
label18: ;
                }
            }
            return;
        }
        public void MissingPreset_findChainPlusChainToIntIndependent_node_end(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList, GRGEN_LGSP.LGSPNode candidate_findChainPlusChainToIntIndependent_node_beg)
        {
            int negLevel = 0;
            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0 patternpath_match_findChainPlusChainToIntIndependent_idpt_0 = null;
            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent patternpath_match_findChainPlusChainToIntIndependent = null;
            // Lookup findChainPlusChainToIntIndependent_node_end 
            foreach(GRGEN_LIBGR.NodeType type_candidate_findChainPlusChainToIntIndependent_node_end in NodeType_Node.typeVar.SubOrSameTypes)
            {
                int type_id_candidate_findChainPlusChainToIntIndependent_node_end = type_candidate_findChainPlusChainToIntIndependent_node_end.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_findChainPlusChainToIntIndependent_node_end = graph.nodesByTypeHeads[type_id_candidate_findChainPlusChainToIntIndependent_node_end], candidate_findChainPlusChainToIntIndependent_node_end = head_candidate_findChainPlusChainToIntIndependent_node_end.typeNext; candidate_findChainPlusChainToIntIndependent_node_end != head_candidate_findChainPlusChainToIntIndependent_node_end; candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.typeNext)
                {
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_findChainPlusChainToIntIndependent_node_end)))
                    {
                        continue;
                    }
                    // IndependentPattern 
                    {
                        ++negLevel;
                        if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL && negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                            graph.atNegLevelMatchedElements.Add(new GRGEN_LGSP.Pair<Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>, Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>>());
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst = new Dictionary<GRGEN_LGSP.LGSPNode, GRGEN_LGSP.LGSPNode>();
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd = new Dictionary<GRGEN_LGSP.LGSPEdge, GRGEN_LGSP.LGSPEdge>();
                        }
                        Stack<GRGEN_LGSP.LGSPSubpatternAction> findChainPlusChainToIntIndependent_openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
                        List<Stack<GRGEN_LIBGR.IMatch>> findChainPlusChainToIntIndependent_foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
                        List<Stack<GRGEN_LIBGR.IMatch>> findChainPlusChainToIntIndependent_matchesList = findChainPlusChainToIntIndependent_foundPartialMatches;
                        // Push subpattern matching task for _subpattern0
                        PatternAction_iteratedPathToIntNode taskFor_findChainPlusChainToIntIndependent__subpattern0 = PatternAction_iteratedPathToIntNode.getNewTask(graph, findChainPlusChainToIntIndependent_openTasks);
                        taskFor_findChainPlusChainToIntIndependent__subpattern0.iteratedPathToIntNode_node_beg = candidate_findChainPlusChainToIntIndependent_node_end;
                        findChainPlusChainToIntIndependent_openTasks.Push(taskFor_findChainPlusChainToIntIndependent__subpattern0);
                        uint prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                        prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_findChainPlusChainToIntIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns of findChainPlusChainToIntIndependent_
                        findChainPlusChainToIntIndependent_openTasks.Peek().myMatch(findChainPlusChainToIntIndependent_matchesList, 1, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        findChainPlusChainToIntIndependent_openTasks.Pop();
                        PatternAction_iteratedPathToIntNode.releaseTask(taskFor_findChainPlusChainToIntIndependent__subpattern0);
                        // Check whether subpatterns were found 
                        if(findChainPlusChainToIntIndependent_matchesList.Count>0) {
                            // independent pattern with contained subpatterns found
                            Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = findChainPlusChainToIntIndependent_matchesList[0];
                            findChainPlusChainToIntIndependent_matchesList.Clear();
                            matched_independent_findChainPlusChainToIntIndependent_idpt_0._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                            matched_independent_findChainPlusChainToIntIndependent_idpt_0.__subpattern0 = (@Pattern_iteratedPathToIntNode.Match_iteratedPathToIntNode)currentFoundPartialMatch.Pop();
                            candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                            if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                            }
                            --negLevel;
                            goto label19;
                        }
                        candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal_findChainPlusChainToIntIndependent__candidate_findChainPlusChainToIntIndependent_node_end;
                        if(negLevel > (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Clear();
                            graph.atNegLevelMatchedElements[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Clear();
                        }
                        --negLevel;
                    }
                    goto label20;
label19: ;
                    // Push subpattern matching task for _subpattern0
                    PatternAction_iteratedPath taskFor__subpattern0 = PatternAction_iteratedPath.getNewTask(graph, openTasks);
                    taskFor__subpattern0.iteratedPath_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg;
                    taskFor__subpattern0.iteratedPath_node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                    openTasks.Push(taskFor__subpattern0);
                    uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                    prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg = candidate_findChainPlusChainToIntIndependent_node_beg.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    uint prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                    prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end = candidate_findChainPlusChainToIntIndependent_node_end.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    candidate_findChainPlusChainToIntIndependent_node_end.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN;
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for _subpattern0
                    openTasks.Pop();
                    PatternAction_iteratedPath.releaseTask(taskFor__subpattern0);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent match = matches.GetNextUnfilledPosition();
                            match._node_beg = candidate_findChainPlusChainToIntIndependent_node_beg;
                            match._node_end = candidate_findChainPlusChainToIntIndependent_node_end;
                            match.__subpattern0 = (@Pattern_iteratedPath.Match_iteratedPath)currentFoundPartialMatch.Pop();
                            match._idpt_0 = matched_independent_findChainPlusChainToIntIndependent_idpt_0;
                            matched_independent_findChainPlusChainToIntIndependent_idpt_0 = new Rule_findChainPlusChainToIntIndependent.Match_findChainPlusChainToIntIndependent_idpt_0();
                            matches.PositionWasFilledFixIt();
                        }
                        matchesList.Clear();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                            candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                            return;
                        }
                        candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
                        candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                        goto label21;
                    }
                    candidate_findChainPlusChainToIntIndependent_node_beg.flags = candidate_findChainPlusChainToIntIndependent_node_beg.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_beg;
                    candidate_findChainPlusChainToIntIndependent_node_end.flags = candidate_findChainPlusChainToIntIndependent_node_end.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_findChainPlusChainToIntIndependent_node_end;
label20: ;
label21: ;
                }
            }
            return;
        }
    }


    // class which instantiates and stores all the compiled actions of the module in a dictionary,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class IndependentActions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public IndependentActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public IndependentActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer analyzer = new de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Pattern_iteratedPath.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_iteratedPathToIntNode.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_create.Instance);
            actions.Add("create", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_create.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_find.Instance);
            actions.Add("find", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_find.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_findIndependent.Instance);
            actions.Add("findIndependent", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findIndependent.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_findMultiNested.Instance);
            actions.Add("findMultiNested", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findMultiNested.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_createIterated.Instance);
            actions.Add("createIterated", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_createIterated.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_findChainPlusChainToInt.Instance);
            actions.Add("findChainPlusChainToInt", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findChainPlusChainToInt.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_findChainPlusChainToIntIndependent.Instance);
            actions.Add("findChainPlusChainToIntIndependent", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_findChainPlusChainToIntIndependent.Instance);
            analyzer.ComputeInterPatternRelations();
        }

        public override String Name { get { return "IndependentActions"; } }
        public override String ModelMD5Hash { get { return "a5b70deb49575f4d0997a3b831be3dfa"; } }
    }
}