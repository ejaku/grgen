// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\iterated\spanningTree.grg" on Sun Apr 26 23:49:47 GMT+01:00 2009

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_spanningTree
{
	public class Pattern_SpanningTree : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_SpanningTree instance = null;
		public static Pattern_SpanningTree Instance { get { if (instance==null) { instance = new Pattern_SpanningTree(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] SpanningTree_node_root_AllowedTypes = null;
		public static bool[] SpanningTree_node_root_IsAllowedType = null;
		public enum SpanningTree_NodeNums { @root, };
		public enum SpanningTree_EdgeNums { };
		public enum SpanningTree_VariableNums { };
		public enum SpanningTree_SubNums { };
		public enum SpanningTree_AltNums { };
		public enum SpanningTree_IterNums { @iter_0, };



		GRGEN_LGSP.PatternGraph pat_SpanningTree;

		public static GRGEN_LIBGR.NodeType[] SpanningTree_iter_0_node_next_AllowedTypes = null;
		public static bool[] SpanningTree_iter_0_node_next_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] SpanningTree_iter_0_edge_e_AllowedTypes = null;
		public static bool[] SpanningTree_iter_0_edge_e_IsAllowedType = null;
		public enum SpanningTree_iter_0_NodeNums { @root, @next, };
		public enum SpanningTree_iter_0_EdgeNums { @e, };
		public enum SpanningTree_iter_0_VariableNums { };
		public enum SpanningTree_iter_0_SubNums { @_subpattern0, };
		public enum SpanningTree_iter_0_AltNums { };
		public enum SpanningTree_iter_0_IterNums { };



		GRGEN_LGSP.PatternGraph SpanningTree_iter_0;


		private Pattern_SpanningTree()
		{
			name = "SpanningTree";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "SpanningTree_node_root", };
		}
		private void initialize()
		{
			bool[,] SpanningTree_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] SpanningTree_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode SpanningTree_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "SpanningTree_node_root", "root", SpanningTree_node_root_AllowedTypes, SpanningTree_node_root_IsAllowedType, 5.5F, 0);
			bool[,] SpanningTree_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] SpanningTree_iter_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode SpanningTree_iter_0_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "SpanningTree_iter_0_node_next", "next", SpanningTree_iter_0_node_next_AllowedTypes, SpanningTree_iter_0_node_next_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge SpanningTree_iter_0_edge_e = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "SpanningTree_iter_0_edge_e", "e", SpanningTree_iter_0_edge_e_AllowedTypes, SpanningTree_iter_0_edge_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding SpanningTree_iter_0__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_SpanningTree.Instance, new GRGEN_LGSP.PatternElement[] { SpanningTree_iter_0_node_next });
			SpanningTree_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"SpanningTree_",
				false,
				new GRGEN_LGSP.PatternNode[] { SpanningTree_node_root, SpanningTree_iter_0_node_next }, 
				new GRGEN_LGSP.PatternEdge[] { SpanningTree_iter_0_edge_e }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { SpanningTree_iter_0__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
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
				SpanningTree_iter_0_isNodeHomomorphicGlobal,
				SpanningTree_iter_0_isEdgeHomomorphicGlobal
			);
			SpanningTree_iter_0.edgeToSourceNode.Add(SpanningTree_iter_0_edge_e, SpanningTree_node_root);
			SpanningTree_iter_0.edgeToTargetNode.Add(SpanningTree_iter_0_edge_e, SpanningTree_iter_0_node_next);

			pat_SpanningTree = new GRGEN_LGSP.PatternGraph(
				"SpanningTree",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { SpanningTree_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { SpanningTree_iter_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				SpanningTree_isNodeHomomorphicGlobal,
				SpanningTree_isEdgeHomomorphicGlobal
			);
			SpanningTree_iter_0.embeddingGraph = pat_SpanningTree;

			SpanningTree_node_root.PointOfDefinition = null;
			SpanningTree_iter_0_node_next.PointOfDefinition = SpanningTree_iter_0;
			SpanningTree_iter_0_edge_e.PointOfDefinition = SpanningTree_iter_0;
			SpanningTree_iter_0__subpattern0.PointOfDefinition = SpanningTree_iter_0;

			patternGraph = pat_SpanningTree;
		}


		public void SpanningTree_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTree curMatch = (Match_SpanningTree)_curMatch;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> iterated_iter_0 = curMatch._iter_0;
			graph.SettingAddedNodeNames( SpanningTree_addedNodeNames );
			SpanningTree_iter_0_Modify(graph, iterated_iter_0);
			graph.SettingAddedEdgeNames( SpanningTree_addedEdgeNames );
		}
		private static string[] SpanningTree_addedNodeNames = new string[] {  };
		private static string[] SpanningTree_addedEdgeNames = new string[] {  };

		public void SpanningTree_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTree curMatch = (Match_SpanningTree)_curMatch;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> iterated_iter_0 = curMatch._iter_0;
			graph.SettingAddedNodeNames( SpanningTree_addedNodeNames );
			SpanningTree_iter_0_ModifyNoReuse(graph, iterated_iter_0);
			graph.SettingAddedEdgeNames( SpanningTree_addedEdgeNames );
		}

		public void SpanningTree_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_root)
		{
			graph.SettingAddedNodeNames( create_SpanningTree_addedNodeNames );
			graph.SettingAddedEdgeNames( create_SpanningTree_addedEdgeNames );
		}
		private static string[] create_SpanningTree_addedNodeNames = new string[] {  };
		private static string[] create_SpanningTree_addedEdgeNames = new string[] {  };

		public void SpanningTree_Delete(GRGEN_LGSP.LGSPGraph graph, Match_SpanningTree curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> iterated_iter_0 = curMatch._iter_0;
			SpanningTree_iter_0_Delete(graph, iterated_iter_0);
		}

		public void SpanningTree_iter_0_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> curMatches)
		{
			for(Match_SpanningTree_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTree_iter_0_Modify(graph, curMatch);
			}
		}

		public void SpanningTree_iter_0_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> curMatches)
		{
			for(Match_SpanningTree_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTree_iter_0_ModifyNoReuse(graph, curMatch);
			}
		}

		public void SpanningTree_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> curMatches)
		{
			for(Match_SpanningTree_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTree_iter_0_Delete(graph, curMatch);
			}
		}

		public void SpanningTree_iter_0_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTree_iter_0 curMatch = (Match_SpanningTree_iter_0)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			Pattern_SpanningTree.Match_SpanningTree subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.SettingAddedNodeNames( SpanningTree_iter_0_addedNodeNames );
			graph.SettingAddedEdgeNames( SpanningTree_iter_0_addedEdgeNames );
			graph.SetVisited(edge_e, 0, true);
			graph.SetVisited(node_next, 0, true);
		}
		private static string[] SpanningTree_iter_0_addedNodeNames = new string[] {  };
		private static string[] SpanningTree_iter_0_addedEdgeNames = new string[] {  };

		public void SpanningTree_iter_0_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTree_iter_0 curMatch = (Match_SpanningTree_iter_0)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			Pattern_SpanningTree.Match_SpanningTree subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.SettingAddedNodeNames( SpanningTree_iter_0_addedNodeNames );
			graph.SettingAddedEdgeNames( SpanningTree_iter_0_addedEdgeNames );
			graph.SetVisited(edge_e, 0, true);
			graph.SetVisited(node_next, 0, true);
		}

		public void SpanningTree_iter_0_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_SpanningTree_iter_0_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_next = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_SpanningTree_iter_0_addedEdgeNames );
			GRGEN_MODEL.@UEdge edge_e = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node_next);
			Pattern_SpanningTree.Instance.SpanningTree_Create(graph, node_next);
		}
		private static string[] create_SpanningTree_iter_0_addedNodeNames = new string[] { "root", "next" };
		private static string[] create_SpanningTree_iter_0_addedEdgeNames = new string[] { "e" };

		public void SpanningTree_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_SpanningTree_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			Pattern_SpanningTree.Match_SpanningTree subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.Remove(edge_e);
			graph.RemoveEdges(node_next);
			graph.Remove(node_next);
			Pattern_SpanningTree.Instance.SpanningTree_Delete(graph, subpattern__subpattern0);
		}

		static Pattern_SpanningTree() {
		}

		public interface IMatch_SpanningTree : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTree_iter_0> iter_0 { get; }
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_SpanningTree_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			GRGEN_LIBGR.INode node_next { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge_e { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTree.Match_SpanningTree @_subpattern0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SpanningTree : GRGEN_LGSP.ListElement<Match_SpanningTree>, IMatch_SpanningTree
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum SpanningTree_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTree_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			
			public enum SpanningTree_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTree_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTree_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTree_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTree_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> _iter_0;
			public enum SpanningTree_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)SpanningTree_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum SpanningTree_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTree.instance.pat_SpanningTree; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_SpanningTree_iter_0 : GRGEN_LGSP.ListElement<Match_SpanningTree_iter_0>, IMatch_SpanningTree_iter_0
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LIBGR.INode node_next { get { return (GRGEN_LIBGR.INode)_node_next; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public GRGEN_LGSP.LGSPNode _node_next;
			public enum SpanningTree_iter_0_NodeNums { @root, @next, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTree_iter_0_NodeNums.@root: return _node_root;
				case (int)SpanningTree_iter_0_NodeNums.@next: return _node_next;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge_e { get { return (GRGEN_LIBGR.IEdge)_edge_e; } }
			public GRGEN_LGSP.LGSPEdge _edge_e;
			public enum SpanningTree_iter_0_EdgeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SpanningTree_iter_0_EdgeNums.@e: return _edge_e;
				default: return null;
				}
			}
			
			public enum SpanningTree_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_SpanningTree.Match_SpanningTree @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_SpanningTree.Match_SpanningTree @__subpattern0;
			public enum SpanningTree_iter_0_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)SpanningTree_iter_0_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum SpanningTree_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTree_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTree_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTree.instance.SpanningTree_iter_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Pattern_SpanningTreeReverse : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_SpanningTreeReverse instance = null;
		public static Pattern_SpanningTreeReverse Instance { get { if (instance==null) { instance = new Pattern_SpanningTreeReverse(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] SpanningTreeReverse_node_root_AllowedTypes = null;
		public static bool[] SpanningTreeReverse_node_root_IsAllowedType = null;
		public enum SpanningTreeReverse_NodeNums { @root, };
		public enum SpanningTreeReverse_EdgeNums { };
		public enum SpanningTreeReverse_VariableNums { };
		public enum SpanningTreeReverse_SubNums { };
		public enum SpanningTreeReverse_AltNums { };
		public enum SpanningTreeReverse_IterNums { @iter_0, };



		GRGEN_LGSP.PatternGraph pat_SpanningTreeReverse;

		public static GRGEN_LIBGR.NodeType[] SpanningTreeReverse_iter_0_node_next_AllowedTypes = null;
		public static bool[] SpanningTreeReverse_iter_0_node_next_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] SpanningTreeReverse_iter_0_edge__edge0_AllowedTypes = null;
		public static bool[] SpanningTreeReverse_iter_0_edge__edge0_IsAllowedType = null;
		public enum SpanningTreeReverse_iter_0_NodeNums { @root, @next, };
		public enum SpanningTreeReverse_iter_0_EdgeNums { @_edge0, };
		public enum SpanningTreeReverse_iter_0_VariableNums { };
		public enum SpanningTreeReverse_iter_0_SubNums { @sptrr, };
		public enum SpanningTreeReverse_iter_0_AltNums { };
		public enum SpanningTreeReverse_iter_0_IterNums { };



		GRGEN_LGSP.PatternGraph SpanningTreeReverse_iter_0;


		private Pattern_SpanningTreeReverse()
		{
			name = "SpanningTreeReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "SpanningTreeReverse_node_root", };
		}
		private void initialize()
		{
			bool[,] SpanningTreeReverse_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] SpanningTreeReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode SpanningTreeReverse_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "SpanningTreeReverse_node_root", "root", SpanningTreeReverse_node_root_AllowedTypes, SpanningTreeReverse_node_root_IsAllowedType, 5.5F, 0);
			bool[,] SpanningTreeReverse_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] SpanningTreeReverse_iter_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode SpanningTreeReverse_iter_0_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "SpanningTreeReverse_iter_0_node_next", "next", SpanningTreeReverse_iter_0_node_next_AllowedTypes, SpanningTreeReverse_iter_0_node_next_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge SpanningTreeReverse_iter_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, "GRGEN_LIBGR.IEdge", "SpanningTreeReverse_iter_0_edge__edge0", "_edge0", SpanningTreeReverse_iter_0_edge__edge0_AllowedTypes, SpanningTreeReverse_iter_0_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternGraphEmbedding SpanningTreeReverse_iter_0_sptrr = new GRGEN_LGSP.PatternGraphEmbedding("sptrr", Pattern_SpanningTreeReverse.Instance, new GRGEN_LGSP.PatternElement[] { SpanningTreeReverse_iter_0_node_next });
			SpanningTreeReverse_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"SpanningTreeReverse_",
				false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeReverse_node_root, SpanningTreeReverse_iter_0_node_next }, 
				new GRGEN_LGSP.PatternEdge[] { SpanningTreeReverse_iter_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { SpanningTreeReverse_iter_0_sptrr }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
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
				SpanningTreeReverse_iter_0_isNodeHomomorphicGlobal,
				SpanningTreeReverse_iter_0_isEdgeHomomorphicGlobal
			);
			SpanningTreeReverse_iter_0.edgeToSourceNode.Add(SpanningTreeReverse_iter_0_edge__edge0, SpanningTreeReverse_node_root);
			SpanningTreeReverse_iter_0.edgeToTargetNode.Add(SpanningTreeReverse_iter_0_edge__edge0, SpanningTreeReverse_iter_0_node_next);

			pat_SpanningTreeReverse = new GRGEN_LGSP.PatternGraph(
				"SpanningTreeReverse",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeReverse_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { SpanningTreeReverse_iter_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				SpanningTreeReverse_isNodeHomomorphicGlobal,
				SpanningTreeReverse_isEdgeHomomorphicGlobal
			);
			SpanningTreeReverse_iter_0.embeddingGraph = pat_SpanningTreeReverse;

			SpanningTreeReverse_node_root.PointOfDefinition = null;
			SpanningTreeReverse_iter_0_node_next.PointOfDefinition = SpanningTreeReverse_iter_0;
			SpanningTreeReverse_iter_0_edge__edge0.PointOfDefinition = SpanningTreeReverse_iter_0;
			SpanningTreeReverse_iter_0_sptrr.PointOfDefinition = SpanningTreeReverse_iter_0;

			patternGraph = pat_SpanningTreeReverse;
		}


		public void SpanningTreeReverse_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTreeReverse curMatch = (Match_SpanningTreeReverse)_curMatch;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> iterated_iter_0 = curMatch._iter_0;
			graph.SettingAddedNodeNames( SpanningTreeReverse_addedNodeNames );
			SpanningTreeReverse_iter_0_Modify(graph, iterated_iter_0);
			graph.SettingAddedEdgeNames( SpanningTreeReverse_addedEdgeNames );
		}
		private static string[] SpanningTreeReverse_addedNodeNames = new string[] {  };
		private static string[] SpanningTreeReverse_addedEdgeNames = new string[] {  };

		public void SpanningTreeReverse_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTreeReverse curMatch = (Match_SpanningTreeReverse)_curMatch;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> iterated_iter_0 = curMatch._iter_0;
			graph.SettingAddedNodeNames( SpanningTreeReverse_addedNodeNames );
			SpanningTreeReverse_iter_0_ModifyNoReuse(graph, iterated_iter_0);
			graph.SettingAddedEdgeNames( SpanningTreeReverse_addedEdgeNames );
		}

		public void SpanningTreeReverse_Create(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPNode node_root)
		{
			graph.SettingAddedNodeNames( create_SpanningTreeReverse_addedNodeNames );
			graph.SettingAddedEdgeNames( create_SpanningTreeReverse_addedEdgeNames );
		}
		private static string[] create_SpanningTreeReverse_addedNodeNames = new string[] {  };
		private static string[] create_SpanningTreeReverse_addedEdgeNames = new string[] {  };

		public void SpanningTreeReverse_Delete(GRGEN_LGSP.LGSPGraph graph, Match_SpanningTreeReverse curMatch)
		{
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> iterated_iter_0 = curMatch._iter_0;
			SpanningTreeReverse_iter_0_Delete(graph, iterated_iter_0);
		}

		public void SpanningTreeReverse_iter_0_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> curMatches)
		{
			for(Match_SpanningTreeReverse_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTreeReverse_iter_0_Modify(graph, curMatch);
			}
		}

		public void SpanningTreeReverse_iter_0_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> curMatches)
		{
			for(Match_SpanningTreeReverse_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTreeReverse_iter_0_ModifyNoReuse(graph, curMatch);
			}
		}

		public void SpanningTreeReverse_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> curMatches)
		{
			for(Match_SpanningTreeReverse_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTreeReverse_iter_0_Delete(graph, curMatch);
			}
		}

		public void SpanningTreeReverse_iter_0_Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTreeReverse_iter_0 curMatch = (Match_SpanningTreeReverse_iter_0)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern_sptrr = curMatch.@_sptrr;
			graph.SettingAddedNodeNames( SpanningTreeReverse_iter_0_addedNodeNames );
			Pattern_SpanningTreeReverse.Instance.SpanningTreeReverse_Modify(graph, subpattern_sptrr);
			graph.SettingAddedEdgeNames( SpanningTreeReverse_iter_0_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge1;
			if(edge__edge0.type == GRGEN_MODEL.EdgeType_Edge.typeVar)
			{
				// re-using edge__edge0 as edge__edge1
				edge__edge1 = (GRGEN_MODEL.@Edge) edge__edge0;
				graph.ReuseEdge(edge__edge0, node_next, node_root);
			}
			else
			{
				graph.Remove(edge__edge0);
				edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_next, node_root);
			}
		}
		private static string[] SpanningTreeReverse_iter_0_addedNodeNames = new string[] {  };
		private static string[] SpanningTreeReverse_iter_0_addedEdgeNames = new string[] { "_edge1" };

		public void SpanningTreeReverse_iter_0_ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_SpanningTreeReverse_iter_0 curMatch = (Match_SpanningTreeReverse_iter_0)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern_sptrr = curMatch.@_sptrr;
			graph.SettingAddedNodeNames( SpanningTreeReverse_iter_0_addedNodeNames );
			Pattern_SpanningTreeReverse.Instance.SpanningTreeReverse_Modify(graph, subpattern_sptrr);
			graph.SettingAddedEdgeNames( SpanningTreeReverse_iter_0_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_next, node_root);
			graph.Remove(edge__edge0);
		}

		public void SpanningTreeReverse_iter_0_Create(GRGEN_LGSP.LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_SpanningTreeReverse_iter_0_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_next = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_SpanningTreeReverse_iter_0_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node_next);
			Pattern_SpanningTreeReverse.Instance.SpanningTreeReverse_Create(graph, node_next);
		}
		private static string[] create_SpanningTreeReverse_iter_0_addedNodeNames = new string[] { "root", "next" };
		private static string[] create_SpanningTreeReverse_iter_0_addedEdgeNames = new string[] { "_edge0" };

		public void SpanningTreeReverse_iter_0_Delete(GRGEN_LGSP.LGSPGraph graph, Match_SpanningTreeReverse_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern_sptrr = curMatch.@_sptrr;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_next);
			graph.Remove(node_next);
			Pattern_SpanningTreeReverse.Instance.SpanningTreeReverse_Delete(graph, subpattern_sptrr);
		}

		static Pattern_SpanningTreeReverse() {
		}

		public interface IMatch_SpanningTreeReverse : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTreeReverse_iter_0> iter_0 { get; }
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_SpanningTreeReverse_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			GRGEN_LIBGR.INode node_next { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @sptrr { get; }
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SpanningTreeReverse : GRGEN_LGSP.ListElement<Match_SpanningTreeReverse>, IMatch_SpanningTreeReverse
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum SpanningTreeReverse_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeReverse_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTreeReverse_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> _iter_0;
			public enum SpanningTreeReverse_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeReverse_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeReverse.instance.pat_SpanningTreeReverse; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_SpanningTreeReverse_iter_0 : GRGEN_LGSP.ListElement<Match_SpanningTreeReverse_iter_0>, IMatch_SpanningTreeReverse_iter_0
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LIBGR.INode node_next { get { return (GRGEN_LIBGR.INode)_node_next; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public GRGEN_LGSP.LGSPNode _node_next;
			public enum SpanningTreeReverse_iter_0_NodeNums { @root, @next, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeReverse_iter_0_NodeNums.@root: return _node_root;
				case (int)SpanningTreeReverse_iter_0_NodeNums.@next: return _node_next;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum SpanningTreeReverse_iter_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeReverse_iter_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_iter_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @sptrr { get { return @_sptrr; } }
			public @Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @_sptrr;
			public enum SpanningTreeReverse_iter_0_SubNums { @sptrr, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeReverse_iter_0_SubNums.@sptrr: return _sptrr;
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_iter_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_iter_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SpanningTreeReverse_iter_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeReverse.instance.SpanningTreeReverse_iter_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_initST : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_initST instance = null;
		public static Rule_initST Instance { get { if (instance==null) { instance = new Rule_initST(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[1];

		public enum initST_NodeNums { };
		public enum initST_EdgeNums { };
		public enum initST_VariableNums { };
		public enum initST_SubNums { };
		public enum initST_AltNums { };
		public enum initST_IterNums { };



		GRGEN_LGSP.PatternGraph pat_initST;


		private Rule_initST()
		{
			name = "initST";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] initST_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] initST_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_initST = new GRGEN_LGSP.PatternGraph(
				"initST",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				initST_isNodeHomomorphicGlobal,
				initST_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_initST;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_initST curMatch = (Match_initST)_curMatch;
			graph.SettingAddedNodeNames( initST_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node3 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node4 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( initST_addedEdgeNames );
			GRGEN_MODEL.@UEdge edge__edge0 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node__node0);
			GRGEN_MODEL.@UEdge edge__edge1 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node__node1);
			GRGEN_MODEL.@UEdge edge__edge2 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node1, node__node2);
			GRGEN_MODEL.@UEdge edge__edge3 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node_n);
			GRGEN_MODEL.@UEdge edge__edge4 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_n, node_root);
			GRGEN_MODEL.@UEdge edge__edge5 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_n, node__node3);
			GRGEN_MODEL.@UEdge edge__edge6 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node3, node__node4);
			GRGEN_MODEL.@UEdge edge__edge7 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node4, node_n);
			ReturnArray[0] = node_root;
			return ReturnArray;
		}
		private static string[] initST_addedNodeNames = new string[] { "root", "_node0", "_node1", "_node2", "n", "_node3", "_node4" };
		private static string[] initST_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_initST curMatch = (Match_initST)_curMatch;
			graph.SettingAddedNodeNames( initST_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node3 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node4 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( initST_addedEdgeNames );
			GRGEN_MODEL.@UEdge edge__edge0 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node__node0);
			GRGEN_MODEL.@UEdge edge__edge1 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node__node1);
			GRGEN_MODEL.@UEdge edge__edge2 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node1, node__node2);
			GRGEN_MODEL.@UEdge edge__edge3 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node_n);
			GRGEN_MODEL.@UEdge edge__edge4 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_n, node_root);
			GRGEN_MODEL.@UEdge edge__edge5 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_n, node__node3);
			GRGEN_MODEL.@UEdge edge__edge6 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node3, node__node4);
			GRGEN_MODEL.@UEdge edge__edge7 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node4, node_n);
			ReturnArray[0] = node_root;
			return ReturnArray;
		}

		static Rule_initST() {
		}

		public interface IMatch_initST : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_initST : GRGEN_LGSP.ListElement<Match_initST>, IMatch_initST
		{
			public enum initST_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initST_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initST_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initST_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initST_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initST_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initST_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_initST.instance.pat_initST; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_initDirected : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_initDirected instance = null;
		public static Rule_initDirected Instance { get { if (instance==null) { instance = new Rule_initDirected(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[1];

		public enum initDirected_NodeNums { };
		public enum initDirected_EdgeNums { };
		public enum initDirected_VariableNums { };
		public enum initDirected_SubNums { };
		public enum initDirected_AltNums { };
		public enum initDirected_IterNums { };



		GRGEN_LGSP.PatternGraph pat_initDirected;


		private Rule_initDirected()
		{
			name = "initDirected";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
		}
		private void initialize()
		{
			bool[,] initDirected_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] initDirected_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_initDirected = new GRGEN_LGSP.PatternGraph(
				"initDirected",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				initDirected_isNodeHomomorphicGlobal,
				initDirected_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_initDirected;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_initDirected curMatch = (Match_initDirected)_curMatch;
			graph.SettingAddedNodeNames( initDirected_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node3 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node4 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( initDirected_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node__node0);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node__node1);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node1, node__node2);
			GRGEN_MODEL.@Edge edge__edge3 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node_n);
			GRGEN_MODEL.@Edge edge__edge4 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n, node_root);
			GRGEN_MODEL.@Edge edge__edge5 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n, node__node3);
			GRGEN_MODEL.@Edge edge__edge6 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node3, node__node4);
			GRGEN_MODEL.@Edge edge__edge7 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n, node__node4);
			ReturnArray[0] = node_root;
			return ReturnArray;
		}
		private static string[] initDirected_addedNodeNames = new string[] { "root", "_node0", "_node1", "_node2", "n", "_node3", "_node4" };
		private static string[] initDirected_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_initDirected curMatch = (Match_initDirected)_curMatch;
			graph.SettingAddedNodeNames( initDirected_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node3 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node4 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( initDirected_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge0 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node__node0);
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node__node1);
			GRGEN_MODEL.@Edge edge__edge2 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node1, node__node2);
			GRGEN_MODEL.@Edge edge__edge3 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node_n);
			GRGEN_MODEL.@Edge edge__edge4 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n, node_root);
			GRGEN_MODEL.@Edge edge__edge5 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n, node__node3);
			GRGEN_MODEL.@Edge edge__edge6 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node3, node__node4);
			GRGEN_MODEL.@Edge edge__edge7 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n, node__node4);
			ReturnArray[0] = node_root;
			return ReturnArray;
		}

		static Rule_initDirected() {
		}

		public interface IMatch_initDirected : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_initDirected : GRGEN_LGSP.ListElement<Match_initDirected>, IMatch_initDirected
		{
			public enum initDirected_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initDirected_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initDirected_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initDirected_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initDirected_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initDirected_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum initDirected_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_initDirected.instance.pat_initDirected; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_spanningTree : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_spanningTree instance = null;
		public static Rule_spanningTree Instance { get { if (instance==null) { instance = new Rule_spanningTree(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] spanningTree_node_root_AllowedTypes = null;
		public static bool[] spanningTree_node_root_IsAllowedType = null;
		public enum spanningTree_NodeNums { @root, };
		public enum spanningTree_EdgeNums { };
		public enum spanningTree_VariableNums { };
		public enum spanningTree_SubNums { @_subpattern0, };
		public enum spanningTree_AltNums { };
		public enum spanningTree_IterNums { };



		GRGEN_LGSP.PatternGraph pat_spanningTree;


		private Rule_spanningTree()
		{
			name = "spanningTree";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "spanningTree_node_root", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] spanningTree_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] spanningTree_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode spanningTree_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "spanningTree_node_root", "root", spanningTree_node_root_AllowedTypes, spanningTree_node_root_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternGraphEmbedding spanningTree__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_SpanningTree.Instance, new GRGEN_LGSP.PatternElement[] { spanningTree_node_root });
			pat_spanningTree = new GRGEN_LGSP.PatternGraph(
				"spanningTree",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { spanningTree_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { spanningTree__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				spanningTree_isNodeHomomorphicGlobal,
				spanningTree_isEdgeHomomorphicGlobal
			);

			spanningTree_node_root.PointOfDefinition = null;
			spanningTree__subpattern0.PointOfDefinition = pat_spanningTree;

			patternGraph = pat_spanningTree;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_spanningTree curMatch = (Match_spanningTree)_curMatch;
			Pattern_SpanningTree.Match_SpanningTree subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.SettingAddedNodeNames( spanningTree_addedNodeNames );
			graph.SettingAddedEdgeNames( spanningTree_addedEdgeNames );
			return EmptyReturnElements;
		}
		private static string[] spanningTree_addedNodeNames = new string[] {  };
		private static string[] spanningTree_addedEdgeNames = new string[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_spanningTree curMatch = (Match_spanningTree)_curMatch;
			Pattern_SpanningTree.Match_SpanningTree subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.SettingAddedNodeNames( spanningTree_addedNodeNames );
			graph.SettingAddedEdgeNames( spanningTree_addedEdgeNames );
			return EmptyReturnElements;
		}

		static Rule_spanningTree() {
		}

		public interface IMatch_spanningTree : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTree.Match_SpanningTree @_subpattern0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_spanningTree : GRGEN_LGSP.ListElement<Match_spanningTree>, IMatch_spanningTree
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum spanningTree_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)spanningTree_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			
			public enum spanningTree_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum spanningTree_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_SpanningTree.Match_SpanningTree @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_SpanningTree.Match_SpanningTree @__subpattern0;
			public enum spanningTree_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)spanningTree_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum spanningTree_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum spanningTree_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum spanningTree_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_spanningTree.instance.pat_spanningTree; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_edgesVisited : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_edgesVisited instance = null;
		public static Rule_edgesVisited Instance { get { if (instance==null) { instance = new Rule_edgesVisited(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.EdgeType[] edgesVisited_edge_e_AllowedTypes = null;
		public static bool[] edgesVisited_edge_e_IsAllowedType = null;
		public enum edgesVisited_NodeNums { };
		public enum edgesVisited_EdgeNums { @e, };
		public enum edgesVisited_VariableNums { };
		public enum edgesVisited_SubNums { };
		public enum edgesVisited_AltNums { };
		public enum edgesVisited_IterNums { };


		GRGEN_LGSP.PatternGraph pat_edgesVisited;


		private Rule_edgesVisited()
		{
			name = "edgesVisited";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] edgesVisited_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] edgesVisited_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge edgesVisited_edge_e = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, "GRGEN_LIBGR.IEdge", "edgesVisited_edge_e", "e", edgesVisited_edge_e_AllowedTypes, edgesVisited_edge_e_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Visited("edgesVisited_edge_e", new GRGEN_EXPR.Constant("0")),
				new string[] {  }, new string[] { "edgesVisited_edge_e" }, new string[] {  });
			pat_edgesVisited = new GRGEN_LGSP.PatternGraph(
				"edgesVisited",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] { edgesVisited_edge_e }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { cond_0,  }, 
				new bool[0, 0] ,
				new bool[1, 1] {
					{ true, },
				},
				edgesVisited_isNodeHomomorphicGlobal,
				edgesVisited_isEdgeHomomorphicGlobal
			);

			edgesVisited_edge_e.PointOfDefinition = pat_edgesVisited;

			patternGraph = pat_edgesVisited;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_edgesVisited curMatch = (Match_edgesVisited)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_edgesVisited curMatch = (Match_edgesVisited)_curMatch;
			return EmptyReturnElements;
		}

		static Rule_edgesVisited() {
		}

		public interface IMatch_edgesVisited : GRGEN_LIBGR.IMatch
		{
			//Nodes
			//Edges
			GRGEN_LIBGR.IEdge edge_e { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_edgesVisited : GRGEN_LGSP.ListElement<Match_edgesVisited>, IMatch_edgesVisited
		{
			public enum edgesVisited_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge_e { get { return (GRGEN_LIBGR.IEdge)_edge_e; } }
			public GRGEN_LGSP.LGSPEdge _edge_e;
			public enum edgesVisited_EdgeNums { @e, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)edgesVisited_EdgeNums.@e: return _edge_e;
				default: return null;
				}
			}
			
			public enum edgesVisited_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum edgesVisited_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum edgesVisited_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum edgesVisited_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum edgesVisited_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_edgesVisited.instance.pat_edgesVisited; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_nodesVisited : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_nodesVisited instance = null;
		public static Rule_nodesVisited Instance { get { if (instance==null) { instance = new Rule_nodesVisited(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] nodesVisited_node_n_AllowedTypes = null;
		public static bool[] nodesVisited_node_n_IsAllowedType = null;
		public enum nodesVisited_NodeNums { @n, };
		public enum nodesVisited_EdgeNums { };
		public enum nodesVisited_VariableNums { };
		public enum nodesVisited_SubNums { };
		public enum nodesVisited_AltNums { };
		public enum nodesVisited_IterNums { };


		GRGEN_LGSP.PatternGraph pat_nodesVisited;


		private Rule_nodesVisited()
		{
			name = "nodesVisited";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] nodesVisited_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] nodesVisited_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode nodesVisited_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "nodesVisited_node_n", "n", nodesVisited_node_n_AllowedTypes, nodesVisited_node_n_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternCondition cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Visited("nodesVisited_node_n", new GRGEN_EXPR.Constant("0")),
				new string[] { "nodesVisited_node_n" }, new string[] {  }, new string[] {  });
			pat_nodesVisited = new GRGEN_LGSP.PatternGraph(
				"nodesVisited",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { nodesVisited_node_n }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { cond_0,  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				nodesVisited_isNodeHomomorphicGlobal,
				nodesVisited_isEdgeHomomorphicGlobal
			);

			nodesVisited_node_n.PointOfDefinition = pat_nodesVisited;

			patternGraph = pat_nodesVisited;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_nodesVisited curMatch = (Match_nodesVisited)_curMatch;
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_nodesVisited curMatch = (Match_nodesVisited)_curMatch;
			return EmptyReturnElements;
		}

		static Rule_nodesVisited() {
		}

		public interface IMatch_nodesVisited : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_n { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_nodesVisited : GRGEN_LGSP.ListElement<Match_nodesVisited>, IMatch_nodesVisited
		{
			public GRGEN_LIBGR.INode node_n { get { return (GRGEN_LIBGR.INode)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum nodesVisited_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)nodesVisited_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public enum nodesVisited_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum nodesVisited_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum nodesVisited_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum nodesVisited_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum nodesVisited_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum nodesVisited_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_nodesVisited.instance.pat_nodesVisited; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_spanningTreeReverse : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_spanningTreeReverse instance = null;
		public static Rule_spanningTreeReverse Instance { get { if (instance==null) { instance = new Rule_spanningTreeReverse(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] spanningTreeReverse_node_root_AllowedTypes = null;
		public static bool[] spanningTreeReverse_node_root_IsAllowedType = null;
		public enum spanningTreeReverse_NodeNums { @root, };
		public enum spanningTreeReverse_EdgeNums { };
		public enum spanningTreeReverse_VariableNums { };
		public enum spanningTreeReverse_SubNums { @_subpattern0, };
		public enum spanningTreeReverse_AltNums { };
		public enum spanningTreeReverse_IterNums { };



		GRGEN_LGSP.PatternGraph pat_spanningTreeReverse;


		private Rule_spanningTreeReverse()
		{
			name = "spanningTreeReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "spanningTreeReverse_node_root", };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] spanningTreeReverse_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] spanningTreeReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode spanningTreeReverse_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, "GRGEN_LIBGR.INode", "spanningTreeReverse_node_root", "root", spanningTreeReverse_node_root_AllowedTypes, spanningTreeReverse_node_root_IsAllowedType, 5.5F, 0);
			GRGEN_LGSP.PatternGraphEmbedding spanningTreeReverse__subpattern0 = new GRGEN_LGSP.PatternGraphEmbedding("_subpattern0", Pattern_SpanningTreeReverse.Instance, new GRGEN_LGSP.PatternElement[] { spanningTreeReverse_node_root });
			pat_spanningTreeReverse = new GRGEN_LGSP.PatternGraph(
				"spanningTreeReverse",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { spanningTreeReverse_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { spanningTreeReverse__subpattern0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				spanningTreeReverse_isNodeHomomorphicGlobal,
				spanningTreeReverse_isEdgeHomomorphicGlobal
			);

			spanningTreeReverse_node_root.PointOfDefinition = null;
			spanningTreeReverse__subpattern0.PointOfDefinition = pat_spanningTreeReverse;

			patternGraph = pat_spanningTreeReverse;
		}


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_spanningTreeReverse curMatch = (Match_spanningTreeReverse)_curMatch;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.SettingAddedNodeNames( spanningTreeReverse_addedNodeNames );
			graph.SettingAddedEdgeNames( spanningTreeReverse_addedEdgeNames );
			return EmptyReturnElements;
		}
		private static string[] spanningTreeReverse_addedNodeNames = new string[] {  };
		private static string[] spanningTreeReverse_addedEdgeNames = new string[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_spanningTreeReverse curMatch = (Match_spanningTreeReverse)_curMatch;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern__subpattern0 = curMatch.@__subpattern0;
			graph.SettingAddedNodeNames( spanningTreeReverse_addedNodeNames );
			graph.SettingAddedEdgeNames( spanningTreeReverse_addedEdgeNames );
			return EmptyReturnElements;
		}

		static Rule_spanningTreeReverse() {
		}

		public interface IMatch_spanningTreeReverse : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @_subpattern0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_spanningTreeReverse : GRGEN_LGSP.ListElement<Match_spanningTreeReverse>, IMatch_spanningTreeReverse
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum spanningTreeReverse_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)spanningTreeReverse_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			
			public enum spanningTreeReverse_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum spanningTreeReverse_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public @Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @_subpattern0 { get { return @__subpattern0; } }
			public @Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @__subpattern0;
			public enum spanningTreeReverse_SubNums { @_subpattern0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)spanningTreeReverse_SubNums.@_subpattern0: return __subpattern0;
				default: return null;
				}
			}
			
			public enum spanningTreeReverse_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum spanningTreeReverse_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum spanningTreeReverse_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_spanningTreeReverse.instance.pat_spanningTreeReverse; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class spanningTree_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public spanningTree_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[2];
			rules = new GRGEN_LGSP.LGSPRulePattern[6];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[2+6];
			subpatterns[0] = Pattern_SpanningTree.Instance;
			rulesAndSubpatterns[0] = Pattern_SpanningTree.Instance;
			subpatterns[1] = Pattern_SpanningTreeReverse.Instance;
			rulesAndSubpatterns[1] = Pattern_SpanningTreeReverse.Instance;
			rules[0] = Rule_initST.Instance;
			rulesAndSubpatterns[2+0] = Rule_initST.Instance;
			rules[1] = Rule_initDirected.Instance;
			rulesAndSubpatterns[2+1] = Rule_initDirected.Instance;
			rules[2] = Rule_spanningTree.Instance;
			rulesAndSubpatterns[2+2] = Rule_spanningTree.Instance;
			rules[3] = Rule_edgesVisited.Instance;
			rulesAndSubpatterns[2+3] = Rule_edgesVisited.Instance;
			rules[4] = Rule_nodesVisited.Instance;
			rulesAndSubpatterns[2+4] = Rule_nodesVisited.Instance;
			rules[5] = Rule_spanningTreeReverse.Instance;
			rulesAndSubpatterns[2+5] = Rule_spanningTreeReverse.Instance;
		}
		public override GRGEN_LGSP.LGSPRulePattern[] Rules { get { return rules; } }
		private GRGEN_LGSP.LGSPRulePattern[] rules;
		public override GRGEN_LGSP.LGSPMatchingPattern[] Subpatterns { get { return subpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] subpatterns;
		public override GRGEN_LGSP.LGSPMatchingPattern[] RulesAndSubpatterns { get { return rulesAndSubpatterns; } }
		private GRGEN_LGSP.LGSPMatchingPattern[] rulesAndSubpatterns;
	}


    public class PatternAction_SpanningTree : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_SpanningTree(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTree.Instance.patternGraph;
        }

        public static PatternAction_SpanningTree getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_SpanningTree newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_SpanningTree(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_SpanningTree oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_SpanningTree freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_SpanningTree next = null;

        public GRGEN_LGSP.LGSPNode SpanningTree_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_SpanningTree.Match_SpanningTree_iter_0 patternpath_match_SpanningTree_iter_0 = null;
            Pattern_SpanningTree.Match_SpanningTree patternpath_match_SpanningTree = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTree_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root = SpanningTree_node_root;
            // build match of SpanningTree for patternpath checks
            if(patternpath_match_SpanningTree==null) patternpath_match_SpanningTree = new Pattern_SpanningTree.Match_SpanningTree();
            patternpath_match_SpanningTree._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_SpanningTree._node_root = candidate_SpanningTree_node_root;
            // Push iterated matching task for SpanningTree_iter_0
            IteratedAction_SpanningTree_iter_0 taskFor_iter_0 = IteratedAction_SpanningTree_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.SpanningTree_node_root = candidate_SpanningTree_node_root;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_SpanningTree;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTree_iter_0
            openTasks.Pop();
            IteratedAction_SpanningTree_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_SpanningTree.Match_SpanningTree match = new Pattern_SpanningTree.Match_SpanningTree();
                    match._node_root = candidate_SpanningTree_node_root;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTree.Match_SpanningTree_iter_0, Pattern_SpanningTree.IMatch_SpanningTree_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTree.IMatch_SpanningTree_iter_0) {
                        ((Pattern_SpanningTree.IMatch_SpanningTree_iter_0)currentFoundPartialMatch.Peek()).SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add((Pattern_SpanningTree.Match_SpanningTree_iter_0)currentFoundPartialMatch.Pop());
                    }
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

    public class IteratedAction_SpanningTree_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTree_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTree.Instance.patternGraph;
        }

        public static IteratedAction_SpanningTree_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTree_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTree_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTree_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTree_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTree_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTree_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_SpanningTree.Match_SpanningTree_iter_0 patternpath_match_SpanningTree_iter_0 = null;
            Pattern_SpanningTree.Match_SpanningTree patternpath_match_SpanningTree = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTree_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root = SpanningTree_node_root;
            // both directions of SpanningTree_iter_0_edge_e
            for(int directionRunCounterOf_SpanningTree_iter_0_edge_e = 0; directionRunCounterOf_SpanningTree_iter_0_edge_e < 2; ++directionRunCounterOf_SpanningTree_iter_0_edge_e)
            {
                // Extend IncomingOrOutgoing SpanningTree_iter_0_edge_e from SpanningTree_node_root 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTree_iter_0_edge_e = directionRunCounterOf_SpanningTree_iter_0_edge_e==0 ? candidate_SpanningTree_node_root.inhead : candidate_SpanningTree_node_root.outhead;
                if(head_candidate_SpanningTree_iter_0_edge_e != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTree_iter_0_edge_e = head_candidate_SpanningTree_iter_0_edge_e;
                    do
                    {
                        if(candidate_SpanningTree_iter_0_edge_e.type.TypeID!=2) {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_SpanningTree_iter_0_edge_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_SpanningTree_iter_0_edge_e)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_SpanningTree_iter_0_edge_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_SpanningTree_iter_0_edge_e, null))
                        {
                            continue;
                        }
                        // Implicit TheOther SpanningTree_iter_0_node_next from SpanningTree_iter_0_edge_e 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTree_iter_0_node_next = candidate_SpanningTree_node_root==candidate_SpanningTree_iter_0_edge_e.source ? candidate_SpanningTree_iter_0_edge_e.target : candidate_SpanningTree_iter_0_edge_e.source;
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_SpanningTree_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_SpanningTree_iter_0_node_next)))
                        {
                            continue;
                        }
                        if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_SpanningTree_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_SpanningTree_iter_0_node_next)))
                        {
                            continue;
                        }
                        if(searchPatternpath && (candidate_SpanningTree_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_SpanningTree_iter_0_node_next, null))
                        {
                            continue;
                        }
                        // build match of SpanningTree_iter_0 for patternpath checks
                        if(patternpath_match_SpanningTree_iter_0==null) patternpath_match_SpanningTree_iter_0 = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                        patternpath_match_SpanningTree_iter_0._matchOfEnclosingPattern = null;
                        patternpath_match_SpanningTree_iter_0._node_root = candidate_SpanningTree_node_root;
                        patternpath_match_SpanningTree_iter_0._node_next = candidate_SpanningTree_iter_0_node_next;
                        patternpath_match_SpanningTree_iter_0._edge_e = candidate_SpanningTree_iter_0_edge_e;
                        uint prevSomeGlobal__candidate_SpanningTree_iter_0_node_next;
                        prevSomeGlobal__candidate_SpanningTree_iter_0_node_next = candidate_SpanningTree_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_SpanningTree_iter_0_node_next.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        uint prevSomeGlobal__candidate_SpanningTree_iter_0_edge_e;
                        prevSomeGlobal__candidate_SpanningTree_iter_0_edge_e = candidate_SpanningTree_iter_0_edge_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        candidate_SpanningTree_iter_0_edge_e.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                        // Push subpattern matching task for _subpattern0
                        PatternAction_SpanningTree taskFor__subpattern0 = PatternAction_SpanningTree.getNewTask(graph, openTasks);
                        taskFor__subpattern0.SpanningTree_node_root = candidate_SpanningTree_iter_0_node_next;
                        taskFor__subpattern0.searchPatternpath = false;
                        taskFor__subpattern0.matchOfNestingPattern = patternpath_match_SpanningTree_iter_0;
                        taskFor__subpattern0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_SpanningTree_iter_0_node_next;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_SpanningTree_iter_0_node_next = candidate_SpanningTree_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_node_next.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_SpanningTree_iter_0_node_next = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_SpanningTree_iter_0_node_next) ? 1U : 0U;
                            if(prevGlobal__candidate_SpanningTree_iter_0_node_next == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_SpanningTree_iter_0_node_next,candidate_SpanningTree_iter_0_node_next);
                        }
                        uint prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            prevGlobal__candidate_SpanningTree_iter_0_edge_e = candidate_SpanningTree_iter_0_edge_e.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_edge_e.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        } else {
                            prevGlobal__candidate_SpanningTree_iter_0_edge_e = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_SpanningTree_iter_0_edge_e) ? 1U : 0U;
                            if(prevGlobal__candidate_SpanningTree_iter_0_edge_e == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_SpanningTree_iter_0_edge_e,candidate_SpanningTree_iter_0_edge_e);
                        }
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_SpanningTree.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                                match._node_root = candidate_SpanningTree_node_root;
                                match._node_next = candidate_SpanningTree_iter_0_node_next;
                                match._edge_e = candidate_SpanningTree_iter_0_edge_e;
                                match.__subpattern0 = (@Pattern_SpanningTree.Match_SpanningTree)currentFoundPartialMatch.Pop();
                                match.__subpattern0._matchOfEnclosingPattern = match;
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
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_SpanningTree_iter_0_edge_e.flags = candidate_SpanningTree_iter_0_edge_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                                } else { 
                                    if(prevGlobal__candidate_SpanningTree_iter_0_edge_e == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_SpanningTree_iter_0_edge_e);
                                    }
                                }
                                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                    candidate_SpanningTree_iter_0_node_next.flags = candidate_SpanningTree_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next;
                                } else { 
                                    if(prevGlobal__candidate_SpanningTree_iter_0_node_next == 0) {
                                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_SpanningTree_iter_0_node_next);
                                    }
                                }
                                candidate_SpanningTree_iter_0_edge_e.flags = candidate_SpanningTree_iter_0_edge_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTree_iter_0_edge_e;
                                candidate_SpanningTree_iter_0_node_next.flags = candidate_SpanningTree_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTree_iter_0_node_next;
                                return;
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_SpanningTree_iter_0_edge_e.flags = candidate_SpanningTree_iter_0_edge_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                            } else { 
                                if(prevGlobal__candidate_SpanningTree_iter_0_edge_e == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_SpanningTree_iter_0_edge_e);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_SpanningTree_iter_0_node_next.flags = candidate_SpanningTree_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next;
                            } else { 
                                if(prevGlobal__candidate_SpanningTree_iter_0_node_next == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_SpanningTree_iter_0_node_next);
                                }
                            }
                            candidate_SpanningTree_iter_0_edge_e.flags = candidate_SpanningTree_iter_0_edge_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTree_iter_0_edge_e;
                            candidate_SpanningTree_iter_0_node_next.flags = candidate_SpanningTree_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTree_iter_0_node_next;
                            continue;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_SpanningTree_iter_0_node_next.flags = candidate_SpanningTree_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next;
                        } else { 
                            if(prevGlobal__candidate_SpanningTree_iter_0_node_next == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_SpanningTree_iter_0_node_next);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_SpanningTree_iter_0_edge_e.flags = candidate_SpanningTree_iter_0_edge_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                        } else { 
                            if(prevGlobal__candidate_SpanningTree_iter_0_edge_e == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_SpanningTree_iter_0_edge_e);
                            }
                        }
                        candidate_SpanningTree_iter_0_node_next.flags = candidate_SpanningTree_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTree_iter_0_node_next;
                        candidate_SpanningTree_iter_0_edge_e.flags = candidate_SpanningTree_iter_0_edge_e.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTree_iter_0_edge_e;
                    }
                    while( (directionRunCounterOf_SpanningTree_iter_0_edge_e==0 ? candidate_SpanningTree_iter_0_edge_e = candidate_SpanningTree_iter_0_edge_e.inNext : candidate_SpanningTree_iter_0_edge_e = candidate_SpanningTree_iter_0_edge_e.outNext) != head_candidate_SpanningTree_iter_0_edge_e );
                }
            }
            // Check whether the iterated pattern was not found
            if(!patternFound)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_SpanningTreeReverse : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_SpanningTreeReverse(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeReverse.Instance.patternGraph;
        }

        public static PatternAction_SpanningTreeReverse getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_SpanningTreeReverse newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_SpanningTreeReverse(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_SpanningTreeReverse oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_SpanningTreeReverse freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_SpanningTreeReverse next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeReverse_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 patternpath_match_SpanningTreeReverse_iter_0 = null;
            Pattern_SpanningTreeReverse.Match_SpanningTreeReverse patternpath_match_SpanningTreeReverse = null;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTreeReverse_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root = SpanningTreeReverse_node_root;
            // build match of SpanningTreeReverse for patternpath checks
            if(patternpath_match_SpanningTreeReverse==null) patternpath_match_SpanningTreeReverse = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse();
            patternpath_match_SpanningTreeReverse._matchOfEnclosingPattern = matchOfNestingPattern;
            patternpath_match_SpanningTreeReverse._node_root = candidate_SpanningTreeReverse_node_root;
            // Push iterated matching task for SpanningTreeReverse_iter_0
            IteratedAction_SpanningTreeReverse_iter_0 taskFor_iter_0 = IteratedAction_SpanningTreeReverse_iter_0.getNewTask(graph, openTasks);
            taskFor_iter_0.SpanningTreeReverse_node_root = candidate_SpanningTreeReverse_node_root;
            taskFor_iter_0.searchPatternpath = searchPatternpath;
            taskFor_iter_0.matchOfNestingPattern = patternpath_match_SpanningTreeReverse;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = lastMatchAtPreviousNestingLevel;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTreeReverse_iter_0
            openTasks.Pop();
            IteratedAction_SpanningTreeReverse_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_SpanningTreeReverse.Match_SpanningTreeReverse match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse();
                    match._node_root = candidate_SpanningTreeReverse_node_root;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0, Pattern_SpanningTreeReverse.IMatch_SpanningTreeReverse_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeReverse.IMatch_SpanningTreeReverse_iter_0) {
                        ((Pattern_SpanningTreeReverse.IMatch_SpanningTreeReverse_iter_0)currentFoundPartialMatch.Peek()).SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add((Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0)currentFoundPartialMatch.Pop());
                    }
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

    public class IteratedAction_SpanningTreeReverse_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeReverse_iter_0(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeReverse.Instance.patternGraph;
        }

        public static IteratedAction_SpanningTreeReverse_iter_0 getNewTask(GRGEN_LGSP.LGSPGraph graph_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeReverse_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeReverse_iter_0(graph_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeReverse_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeReverse_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeReverse_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeReverse_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 patternpath_match_SpanningTreeReverse_iter_0 = null;
            Pattern_SpanningTreeReverse.Match_SpanningTreeReverse patternpath_match_SpanningTreeReverse = null;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTreeReverse_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root = SpanningTreeReverse_node_root;
            // Extend Outgoing SpanningTreeReverse_iter_0_edge__edge0 from SpanningTreeReverse_node_root 
            GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeReverse_iter_0_edge__edge0 = candidate_SpanningTreeReverse_node_root.outhead;
            if(head_candidate_SpanningTreeReverse_iter_0_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_SpanningTreeReverse_iter_0_edge__edge0 = head_candidate_SpanningTreeReverse_iter_0_edge__edge0;
                do
                {
                    if(candidate_SpanningTreeReverse_iter_0_edge__edge0.type.TypeID!=1) {
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_SpanningTreeReverse_iter_0_edge__edge0)))
                    {
                        continue;
                    }
                    if(searchPatternpath && (candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_SpanningTreeReverse_iter_0_edge__edge0, null))
                    {
                        continue;
                    }
                    // Implicit Target SpanningTreeReverse_iter_0_node_next from SpanningTreeReverse_iter_0_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_iter_0_node_next = candidate_SpanningTreeReverse_iter_0_edge__edge0.target;
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_SpanningTreeReverse_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0 : graph.atNegLevelMatchedElements[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_SpanningTreeReverse_iter_0_node_next)))
                    {
                        continue;
                    }
                    if((negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL ? (candidate_SpanningTreeReverse_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel : graph.atNegLevelMatchedElementsGlobal[negLevel-(int)GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_SpanningTreeReverse_iter_0_node_next)))
                    {
                        continue;
                    }
                    if(searchPatternpath && (candidate_SpanningTreeReverse_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN && GRGEN_LGSP.PatternpathIsomorphyChecker.IsMatched(candidate_SpanningTreeReverse_iter_0_node_next, null))
                    {
                        continue;
                    }
                    // build match of SpanningTreeReverse_iter_0 for patternpath checks
                    if(patternpath_match_SpanningTreeReverse_iter_0==null) patternpath_match_SpanningTreeReverse_iter_0 = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                    patternpath_match_SpanningTreeReverse_iter_0._matchOfEnclosingPattern = null;
                    patternpath_match_SpanningTreeReverse_iter_0._node_root = candidate_SpanningTreeReverse_node_root;
                    patternpath_match_SpanningTreeReverse_iter_0._node_next = candidate_SpanningTreeReverse_iter_0_node_next;
                    patternpath_match_SpanningTreeReverse_iter_0._edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0;
                    uint prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                    prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_node_next = candidate_SpanningTreeReverse_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    candidate_SpanningTreeReverse_iter_0_node_next.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    uint prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                    prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    candidate_SpanningTreeReverse_iter_0_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                    // Push subpattern matching task for sptrr
                    PatternAction_SpanningTreeReverse taskFor_sptrr = PatternAction_SpanningTreeReverse.getNewTask(graph, openTasks);
                    taskFor_sptrr.SpanningTreeReverse_node_root = candidate_SpanningTreeReverse_iter_0_node_next;
                    taskFor_sptrr.searchPatternpath = false;
                    taskFor_sptrr.matchOfNestingPattern = patternpath_match_SpanningTreeReverse_iter_0;
                    taskFor_sptrr.lastMatchAtPreviousNestingLevel = null;
                    openTasks.Push(taskFor_sptrr);
                    uint prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next = candidate_SpanningTreeReverse_iter_0_node_next.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_node_next.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                    } else {
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_SpanningTreeReverse_iter_0_node_next) ? 1U : 0U;
                        if(prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_SpanningTreeReverse_iter_0_node_next,candidate_SpanningTreeReverse_iter_0_node_next);
                    }
                    uint prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_edge__edge0.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                    } else {
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.ContainsKey(candidate_SpanningTreeReverse_iter_0_edge__edge0) ? 1U : 0U;
                        if(prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Add(candidate_SpanningTreeReverse_iter_0_edge__edge0,candidate_SpanningTreeReverse_iter_0_edge__edge0);
                    }
                    // Match subpatterns 
                    openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                    // Pop subpattern matching task for sptrr
                    openTasks.Pop();
                    PatternAction_SpanningTreeReverse.releaseTask(taskFor_sptrr);
                    // Check whether subpatterns were found 
                    if(matchesList.Count>0) {
                        patternFound = true;                        // subpatterns/alternatives were found, extend the partial matches by our local match object
                        foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                        {
                            Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                            match._node_root = candidate_SpanningTreeReverse_node_root;
                            match._node_next = candidate_SpanningTreeReverse_iter_0_node_next;
                            match._edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0;
                            match._sptrr = (@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse)currentFoundPartialMatch.Pop();
                            match._sptrr._matchOfEnclosingPattern = match;
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
                        if(true) // as soon as there's a match, it's enough for iterated
                        {
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_SpanningTreeReverse_iter_0_edge__edge0.flags = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                            } else { 
                                if(prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_SpanningTreeReverse_iter_0_edge__edge0);
                                }
                            }
                            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                                candidate_SpanningTreeReverse_iter_0_node_next.flags = candidate_SpanningTreeReverse_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                            } else { 
                                if(prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next == 0) {
                                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_SpanningTreeReverse_iter_0_node_next);
                                }
                            }
                            candidate_SpanningTreeReverse_iter_0_edge__edge0.flags = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                            candidate_SpanningTreeReverse_iter_0_node_next.flags = candidate_SpanningTreeReverse_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                            return;
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_SpanningTreeReverse_iter_0_edge__edge0.flags = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                        } else { 
                            if(prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_SpanningTreeReverse_iter_0_edge__edge0);
                            }
                        }
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_SpanningTreeReverse_iter_0_node_next.flags = candidate_SpanningTreeReverse_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                        } else { 
                            if(prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_SpanningTreeReverse_iter_0_node_next);
                            }
                        }
                        candidate_SpanningTreeReverse_iter_0_edge__edge0.flags = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                        candidate_SpanningTreeReverse_iter_0_node_next.flags = candidate_SpanningTreeReverse_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                        continue;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_SpanningTreeReverse_iter_0_node_next.flags = candidate_SpanningTreeReverse_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                    } else { 
                        if(prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_SpanningTreeReverse_iter_0_node_next);
                        }
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_SpanningTreeReverse_iter_0_edge__edge0.flags = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                    } else { 
                        if(prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].snd.Remove(candidate_SpanningTreeReverse_iter_0_edge__edge0);
                        }
                    }
                    candidate_SpanningTreeReverse_iter_0_node_next.flags = candidate_SpanningTreeReverse_iter_0_node_next.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                    candidate_SpanningTreeReverse_iter_0_edge__edge0.flags = candidate_SpanningTreeReverse_iter_0_edge__edge0.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                }
                while( (candidate_SpanningTreeReverse_iter_0_edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0.outNext) != head_candidate_SpanningTreeReverse_iter_0_edge__edge0 );
            }
            // Check whether the iterated pattern was not found
            if(!patternFound)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class Action_initST : GRGEN_LGSP.LGSPAction
    {
        public Action_initST() {
            rulePattern = Rule_initST.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_initST.Match_initST, Rule_initST.IMatch_initST>(this);
        }

        public override string Name { get { return "initST"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_initST.Match_initST, Rule_initST.IMatch_initST> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_initST instance = new Action_initST();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_initST.Match_initST match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_initDirected : GRGEN_LGSP.LGSPAction
    {
        public Action_initDirected() {
            rulePattern = Rule_initDirected.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_initDirected.Match_initDirected, Rule_initDirected.IMatch_initDirected>(this);
        }

        public override string Name { get { return "initDirected"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_initDirected.Match_initDirected, Rule_initDirected.IMatch_initDirected> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_initDirected instance = new Action_initDirected();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_initDirected.Match_initDirected match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_spanningTree : GRGEN_LGSP.LGSPAction
    {
        public Action_spanningTree() {
            rulePattern = Rule_spanningTree.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_spanningTree.Match_spanningTree, Rule_spanningTree.IMatch_spanningTree>(this);
        }

        public override string Name { get { return "spanningTree"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_spanningTree.Match_spanningTree, Rule_spanningTree.IMatch_spanningTree> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_spanningTree instance = new Action_spanningTree();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_spanningTree.Match_spanningTree patternpath_match_spanningTree = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset spanningTree_node_root 
            GRGEN_LGSP.LGSPNode candidate_spanningTree_node_root = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_spanningTree_node_root == null) {
                MissingPreset_spanningTree_node_root(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // build match of spanningTree for patternpath checks
            if(patternpath_match_spanningTree==null) patternpath_match_spanningTree = new Rule_spanningTree.Match_spanningTree();
            patternpath_match_spanningTree._matchOfEnclosingPattern = null;
            patternpath_match_spanningTree._node_root = candidate_spanningTree_node_root;
            uint prevSomeGlobal__candidate_spanningTree_node_root;
            prevSomeGlobal__candidate_spanningTree_node_root = candidate_spanningTree_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            candidate_spanningTree_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            // Push subpattern matching task for _subpattern0
            PatternAction_SpanningTree taskFor__subpattern0 = PatternAction_SpanningTree.getNewTask(graph, openTasks);
            taskFor__subpattern0.SpanningTree_node_root = candidate_spanningTree_node_root;
            taskFor__subpattern0.searchPatternpath = false;
            taskFor__subpattern0.matchOfNestingPattern = patternpath_match_spanningTree;
            taskFor__subpattern0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_spanningTree_node_root;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prevGlobal__candidate_spanningTree_node_root = candidate_spanningTree_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                candidate_spanningTree_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            } else {
                prevGlobal__candidate_spanningTree_node_root = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_spanningTree_node_root) ? 1U : 0U;
                if(prevGlobal__candidate_spanningTree_node_root == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_spanningTree_node_root,candidate_spanningTree_node_root);
            }
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_SpanningTree.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_spanningTree.Match_spanningTree match = matches.GetNextUnfilledPosition();
                    match._node_root = candidate_spanningTree_node_root;
                    match.__subpattern0 = (@Pattern_SpanningTree.Match_SpanningTree)currentFoundPartialMatch.Pop();
                    match.__subpattern0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
                    } else { 
                        if(prevGlobal__candidate_spanningTree_node_root == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTree_node_root);
                        }
                    }
                    candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTree_node_root;
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
                } else { 
                    if(prevGlobal__candidate_spanningTree_node_root == 0) {
                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTree_node_root);
                    }
                }
                candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTree_node_root;
                return matches;
            }
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
            } else { 
                if(prevGlobal__candidate_spanningTree_node_root == 0) {
                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTree_node_root);
                }
            }
            candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTree_node_root;
            return matches;
        }
        public void MissingPreset_spanningTree_node_root(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_spanningTree.Match_spanningTree patternpath_match_spanningTree = null;
            // Lookup spanningTree_node_root 
            int type_id_candidate_spanningTree_node_root = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_spanningTree_node_root = graph.nodesByTypeHeads[type_id_candidate_spanningTree_node_root], candidate_spanningTree_node_root = head_candidate_spanningTree_node_root.typeNext; candidate_spanningTree_node_root != head_candidate_spanningTree_node_root; candidate_spanningTree_node_root = candidate_spanningTree_node_root.typeNext)
            {
                // build match of spanningTree for patternpath checks
                if(patternpath_match_spanningTree==null) patternpath_match_spanningTree = new Rule_spanningTree.Match_spanningTree();
                patternpath_match_spanningTree._matchOfEnclosingPattern = null;
                patternpath_match_spanningTree._node_root = candidate_spanningTree_node_root;
                uint prevSomeGlobal__candidate_spanningTree_node_root;
                prevSomeGlobal__candidate_spanningTree_node_root = candidate_spanningTree_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                candidate_spanningTree_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                // Push subpattern matching task for _subpattern0
                PatternAction_SpanningTree taskFor__subpattern0 = PatternAction_SpanningTree.getNewTask(graph, openTasks);
                taskFor__subpattern0.SpanningTree_node_root = candidate_spanningTree_node_root;
                taskFor__subpattern0.searchPatternpath = false;
                taskFor__subpattern0.matchOfNestingPattern = patternpath_match_spanningTree;
                taskFor__subpattern0.lastMatchAtPreviousNestingLevel = null;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_spanningTree_node_root;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prevGlobal__candidate_spanningTree_node_root = candidate_spanningTree_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                    candidate_spanningTree_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                } else {
                    prevGlobal__candidate_spanningTree_node_root = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_spanningTree_node_root) ? 1U : 0U;
                    if(prevGlobal__candidate_spanningTree_node_root == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_spanningTree_node_root,candidate_spanningTree_node_root);
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_SpanningTree.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_spanningTree.Match_spanningTree match = matches.GetNextUnfilledPosition();
                        match._node_root = candidate_spanningTree_node_root;
                        match.__subpattern0 = (@Pattern_SpanningTree.Match_SpanningTree)currentFoundPartialMatch.Pop();
                        match.__subpattern0._matchOfEnclosingPattern = match;
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
                        } else { 
                            if(prevGlobal__candidate_spanningTree_node_root == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTree_node_root);
                            }
                        }
                        candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTree_node_root;
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
                    } else { 
                        if(prevGlobal__candidate_spanningTree_node_root == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTree_node_root);
                        }
                    }
                    candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTree_node_root;
                    continue;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
                } else { 
                    if(prevGlobal__candidate_spanningTree_node_root == 0) {
                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTree_node_root);
                    }
                }
                candidate_spanningTree_node_root.flags = candidate_spanningTree_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTree_node_root;
            }
            return;
        }
    }

    public class Action_edgesVisited : GRGEN_LGSP.LGSPAction
    {
        public Action_edgesVisited() {
            rulePattern = Rule_edgesVisited.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_edgesVisited.Match_edgesVisited, Rule_edgesVisited.IMatch_edgesVisited>(this);
        }

        public override string Name { get { return "edgesVisited"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_edgesVisited.Match_edgesVisited, Rule_edgesVisited.IMatch_edgesVisited> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_edgesVisited instance = new Action_edgesVisited();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup edgesVisited_edge_e 
            int type_id_candidate_edgesVisited_edge_e = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_edgesVisited_edge_e = graph.edgesByTypeHeads[type_id_candidate_edgesVisited_edge_e], candidate_edgesVisited_edge_e = head_candidate_edgesVisited_edge_e.typeNext; candidate_edgesVisited_edge_e != head_candidate_edgesVisited_edge_e; candidate_edgesVisited_edge_e = candidate_edgesVisited_edge_e.typeNext)
            {
                // Condition 
                if(!(graph.IsVisited(candidate_edgesVisited_edge_e, 0))) {
                    continue;
                }
                Rule_edgesVisited.Match_edgesVisited match = matches.GetNextUnfilledPosition();
                match._edge_e = candidate_edgesVisited_edge_e;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_edgesVisited_edge_e);
                    return matches;
                }
            }
            return matches;
        }
    }

    public class Action_nodesVisited : GRGEN_LGSP.LGSPAction
    {
        public Action_nodesVisited() {
            rulePattern = Rule_nodesVisited.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_nodesVisited.Match_nodesVisited, Rule_nodesVisited.IMatch_nodesVisited>(this);
        }

        public override string Name { get { return "nodesVisited"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_nodesVisited.Match_nodesVisited, Rule_nodesVisited.IMatch_nodesVisited> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_nodesVisited instance = new Action_nodesVisited();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup nodesVisited_node_n 
            int type_id_candidate_nodesVisited_node_n = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_nodesVisited_node_n = graph.nodesByTypeHeads[type_id_candidate_nodesVisited_node_n], candidate_nodesVisited_node_n = head_candidate_nodesVisited_node_n.typeNext; candidate_nodesVisited_node_n != head_candidate_nodesVisited_node_n; candidate_nodesVisited_node_n = candidate_nodesVisited_node_n.typeNext)
            {
                // Condition 
                if(!(graph.IsVisited(candidate_nodesVisited_node_n, 0))) {
                    continue;
                }
                Rule_nodesVisited.Match_nodesVisited match = matches.GetNextUnfilledPosition();
                match._node_n = candidate_nodesVisited_node_n;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_nodesVisited_node_n);
                    return matches;
                }
            }
            return matches;
        }
    }

    public class Action_spanningTreeReverse : GRGEN_LGSP.LGSPAction
    {
        public Action_spanningTreeReverse() {
            rulePattern = Rule_spanningTreeReverse.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeReverse.Match_spanningTreeReverse, Rule_spanningTreeReverse.IMatch_spanningTreeReverse>(this);
        }

        public override string Name { get { return "spanningTreeReverse"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeReverse.Match_spanningTreeReverse, Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_spanningTreeReverse instance = new Action_spanningTreeReverse();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            Rule_spanningTreeReverse.Match_spanningTreeReverse patternpath_match_spanningTreeReverse = null;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset spanningTreeReverse_node_root 
            GRGEN_LGSP.LGSPNode candidate_spanningTreeReverse_node_root = (GRGEN_LGSP.LGSPNode) parameters[0];
            if(candidate_spanningTreeReverse_node_root == null) {
                MissingPreset_spanningTreeReverse_node_root(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // build match of spanningTreeReverse for patternpath checks
            if(patternpath_match_spanningTreeReverse==null) patternpath_match_spanningTreeReverse = new Rule_spanningTreeReverse.Match_spanningTreeReverse();
            patternpath_match_spanningTreeReverse._matchOfEnclosingPattern = null;
            patternpath_match_spanningTreeReverse._node_root = candidate_spanningTreeReverse_node_root;
            uint prevSomeGlobal__candidate_spanningTreeReverse_node_root;
            prevSomeGlobal__candidate_spanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            candidate_spanningTreeReverse_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
            // Push subpattern matching task for _subpattern0
            PatternAction_SpanningTreeReverse taskFor__subpattern0 = PatternAction_SpanningTreeReverse.getNewTask(graph, openTasks);
            taskFor__subpattern0.SpanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root;
            taskFor__subpattern0.searchPatternpath = false;
            taskFor__subpattern0.matchOfNestingPattern = patternpath_match_spanningTreeReverse;
            taskFor__subpattern0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_spanningTreeReverse_node_root;
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                prevGlobal__candidate_spanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                candidate_spanningTreeReverse_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            } else {
                prevGlobal__candidate_spanningTreeReverse_node_root = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_spanningTreeReverse_node_root) ? 1U : 0U;
                if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_spanningTreeReverse_node_root,candidate_spanningTreeReverse_node_root);
            }
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_SpanningTreeReverse.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_spanningTreeReverse.Match_spanningTreeReverse match = matches.GetNextUnfilledPosition();
                    match._node_root = candidate_spanningTreeReverse_node_root;
                    match.__subpattern0 = (@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse)currentFoundPartialMatch.Pop();
                    match.__subpattern0._matchOfEnclosingPattern = match;
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
                    } else { 
                        if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTreeReverse_node_root);
                        }
                    }
                    candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTreeReverse_node_root;
                    return matches;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
                } else { 
                    if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) {
                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTreeReverse_node_root);
                    }
                }
                candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTreeReverse_node_root;
                return matches;
            }
            if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
            } else { 
                if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) {
                    graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTreeReverse_node_root);
                }
            }
            candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTreeReverse_node_root;
            return matches;
        }
        public void MissingPreset_spanningTreeReverse_node_root(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks, List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, List<Stack<GRGEN_LIBGR.IMatch>> matchesList)
        {
            int negLevel = 0;
            Rule_spanningTreeReverse.Match_spanningTreeReverse patternpath_match_spanningTreeReverse = null;
            // Lookup spanningTreeReverse_node_root 
            int type_id_candidate_spanningTreeReverse_node_root = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_spanningTreeReverse_node_root = graph.nodesByTypeHeads[type_id_candidate_spanningTreeReverse_node_root], candidate_spanningTreeReverse_node_root = head_candidate_spanningTreeReverse_node_root.typeNext; candidate_spanningTreeReverse_node_root != head_candidate_spanningTreeReverse_node_root; candidate_spanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root.typeNext)
            {
                // build match of spanningTreeReverse for patternpath checks
                if(patternpath_match_spanningTreeReverse==null) patternpath_match_spanningTreeReverse = new Rule_spanningTreeReverse.Match_spanningTreeReverse();
                patternpath_match_spanningTreeReverse._matchOfEnclosingPattern = null;
                patternpath_match_spanningTreeReverse._node_root = candidate_spanningTreeReverse_node_root;
                uint prevSomeGlobal__candidate_spanningTreeReverse_node_root;
                prevSomeGlobal__candidate_spanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                candidate_spanningTreeReverse_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN;
                // Push subpattern matching task for _subpattern0
                PatternAction_SpanningTreeReverse taskFor__subpattern0 = PatternAction_SpanningTreeReverse.getNewTask(graph, openTasks);
                taskFor__subpattern0.SpanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root;
                taskFor__subpattern0.searchPatternpath = false;
                taskFor__subpattern0.matchOfNestingPattern = patternpath_match_spanningTreeReverse;
                taskFor__subpattern0.lastMatchAtPreviousNestingLevel = null;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_spanningTreeReverse_node_root;
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    prevGlobal__candidate_spanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                    candidate_spanningTreeReverse_node_root.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                } else {
                    prevGlobal__candidate_spanningTreeReverse_node_root = graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.ContainsKey(candidate_spanningTreeReverse_node_root) ? 1U : 0U;
                    if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Add(candidate_spanningTreeReverse_node_root,candidate_spanningTreeReverse_node_root);
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_SpanningTreeReverse.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Rule_spanningTreeReverse.Match_spanningTreeReverse match = matches.GetNextUnfilledPosition();
                        match._node_root = candidate_spanningTreeReverse_node_root;
                        match.__subpattern0 = (@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse)currentFoundPartialMatch.Pop();
                        match.__subpattern0._matchOfEnclosingPattern = match;
                        matches.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                            candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
                        } else { 
                            if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) {
                                graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTreeReverse_node_root);
                            }
                        }
                        candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTreeReverse_node_root;
                        return;
                    }
                    if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                        candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
                    } else { 
                        if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) {
                            graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTreeReverse_node_root);
                        }
                    }
                    candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTreeReverse_node_root;
                    continue;
                }
                if(negLevel <= (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL) {
                    candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
                } else { 
                    if(prevGlobal__candidate_spanningTreeReverse_node_root == 0) {
                        graph.atNegLevelMatchedElementsGlobal[negLevel - (int) GRGEN_LGSP.LGSPElemFlags.MAX_NEG_LEVEL - 1].fst.Remove(candidate_spanningTreeReverse_node_root);
                    }
                }
                candidate_spanningTreeReverse_node_root.flags = candidate_spanningTreeReverse_node_root.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_SOME_ENCLOSING_PATTERN) | prevSomeGlobal__candidate_spanningTreeReverse_node_root;
            }
            return;
        }
    }


    // class which instantiates and stores all the compiled actions of the module in a dictionary,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class spanningTreeActions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public spanningTreeActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public spanningTreeActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer analyzer = new de.unika.ipd.grGen.lgsp.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Pattern_SpanningTree.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Pattern_SpanningTreeReverse.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_initST.Instance);
            actions.Add("initST", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_initST.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_initDirected.Instance);
            actions.Add("initDirected", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_initDirected.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_spanningTree.Instance);
            actions.Add("spanningTree", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_spanningTree.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_edgesVisited.Instance);
            actions.Add("edgesVisited", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_edgesVisited.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_nodesVisited.Instance);
            actions.Add("nodesVisited", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_nodesVisited.Instance);
            analyzer.AnalyzeNestingOfAndRemember(Rule_spanningTreeReverse.Instance);
            actions.Add("spanningTreeReverse", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_spanningTreeReverse.Instance);
            analyzer.ComputeInterPatternRelations();
        }

        public override string Name { get { return "spanningTreeActions"; } }
        public override string ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}