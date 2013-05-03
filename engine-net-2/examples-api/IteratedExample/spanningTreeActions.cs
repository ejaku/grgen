// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\iterated\spanningTree.grg" on Thu May 02 20:05:51 CEST 2013

using System;
using System.Collections.Generic;
using System.Collections;
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

		public static GRGEN_LIBGR.NodeType[] SpanningTree_node_root_AllowedTypes = null;
		public static bool[] SpanningTree_node_root_IsAllowedType = null;
		public enum SpanningTree_NodeNums { @root, };
		public enum SpanningTree_EdgeNums { };
		public enum SpanningTree_VariableNums { };
		public enum SpanningTree_SubNums { };
		public enum SpanningTree_AltNums { };
		public enum SpanningTree_IterNums { @iter_0, };





		public GRGEN_LGSP.PatternGraph pat_SpanningTree;

		public static GRGEN_LIBGR.NodeType[] SpanningTree_iter_0_node_next_AllowedTypes = null;
		public static bool[] SpanningTree_iter_0_node_next_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] SpanningTree_iter_0_edge_e_AllowedTypes = null;
		public static bool[] SpanningTree_iter_0_edge_e_IsAllowedType = null;
		public enum SpanningTree_iter_0_NodeNums { @root, @next, };
		public enum SpanningTree_iter_0_EdgeNums { @e, };
		public enum SpanningTree_iter_0_VariableNums { };
		public enum SpanningTree_iter_0_SubNums { @sptr, };
		public enum SpanningTree_iter_0_AltNums { };
		public enum SpanningTree_iter_0_IterNums { };






		public GRGEN_LGSP.PatternGraph SpanningTree_iter_0;


		private Pattern_SpanningTree()
		{
			name = "SpanningTree";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "SpanningTree_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] SpanningTree_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] SpanningTree_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] SpanningTree_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] SpanningTree_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode SpanningTree_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTree_node_root", "root", SpanningTree_node_root_AllowedTypes, SpanningTree_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			bool[,] SpanningTree_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] SpanningTree_iter_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] SpanningTree_iter_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] SpanningTree_iter_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode SpanningTree_iter_0_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTree_iter_0_node_next", "next", SpanningTree_iter_0_node_next_AllowedTypes, SpanningTree_iter_0_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SpanningTree_iter_0_edge_e = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, GRGEN_MODEL.EdgeType_UEdge.typeVar, "GRGEN_LIBGR.IEdge", "SpanningTree_iter_0_edge_e", "e", SpanningTree_iter_0_edge_e_AllowedTypes, SpanningTree_iter_0_edge_e_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding SpanningTree_iter_0_sptr = new GRGEN_LGSP.PatternGraphEmbedding("sptr", Pattern_SpanningTree.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("SpanningTree_iter_0_node_next"),
				}, 
				new string[] { }, new string[] { "SpanningTree_iter_0_node_next" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			SpanningTree_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"SpanningTree_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTree_node_root, SpanningTree_iter_0_node_next }, 
				new GRGEN_LGSP.PatternEdge[] { SpanningTree_iter_0_edge_e }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { SpanningTree_iter_0_sptr }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				SpanningTree_iter_0_isNodeHomomorphicGlobal,
				SpanningTree_iter_0_isEdgeHomomorphicGlobal,
				SpanningTree_iter_0_isNodeTotallyHomomorphic,
				SpanningTree_iter_0_isEdgeTotallyHomomorphic
			);
			SpanningTree_iter_0.edgeToSourceNode.Add(SpanningTree_iter_0_edge_e, SpanningTree_node_root);
			SpanningTree_iter_0.edgeToTargetNode.Add(SpanningTree_iter_0_edge_e, SpanningTree_iter_0_node_next);

			GRGEN_LGSP.Iterated SpanningTree_iter_0_it = new GRGEN_LGSP.Iterated( SpanningTree_iter_0, 0, 0);
			pat_SpanningTree = new GRGEN_LGSP.PatternGraph(
				"SpanningTree",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTree_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] { SpanningTree_iter_0_it,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				SpanningTree_isNodeHomomorphicGlobal,
				SpanningTree_isEdgeHomomorphicGlobal,
				SpanningTree_isNodeTotallyHomomorphic,
				SpanningTree_isEdgeTotallyHomomorphic
			);
			SpanningTree_iter_0.embeddingGraph = pat_SpanningTree;

			SpanningTree_node_root.pointOfDefinition = null;
			SpanningTree_iter_0_node_next.pointOfDefinition = SpanningTree_iter_0;
			SpanningTree_iter_0_edge_e.pointOfDefinition = SpanningTree_iter_0;
			SpanningTree_iter_0_sptr.PointOfDefinition = SpanningTree_iter_0;

			patternGraph = pat_SpanningTree;
		}


		public void SpanningTree_Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_SpanningTree curMatch = (Match_SpanningTree)_curMatch;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> iterated_iter_0 = curMatch._iter_0;
			graph.SettingAddedNodeNames( SpanningTree_addedNodeNames );
			SpanningTree_iter_0_Modify(actionEnv, iterated_iter_0);
			graph.SettingAddedEdgeNames( SpanningTree_addedEdgeNames );
		}
		private static string[] SpanningTree_addedNodeNames = new string[] {  };
		private static string[] SpanningTree_addedEdgeNames = new string[] {  };

		public void SpanningTree_Create(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPNode node_root)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			graph.SettingAddedNodeNames( create_SpanningTree_addedNodeNames );
			graph.SettingAddedEdgeNames( create_SpanningTree_addedEdgeNames );
		}
		private static string[] create_SpanningTree_addedNodeNames = new string[] {  };
		private static string[] create_SpanningTree_addedEdgeNames = new string[] {  };

		public void SpanningTree_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTree curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> iterated_iter_0 = curMatch._iter_0;
			SpanningTree_iter_0_Delete(actionEnv, iterated_iter_0);
		}

		public void SpanningTree_iter_0_Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> curMatches)
		{
			for(Match_SpanningTree_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTree_iter_0_Modify(actionEnv, curMatch);
			}
		}

		public void SpanningTree_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTree_iter_0, IMatch_SpanningTree_iter_0> curMatches)
		{
			for(Match_SpanningTree_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTree_iter_0_Delete(actionEnv, curMatch);
			}
		}

		public void SpanningTree_iter_0_Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_SpanningTree_iter_0 curMatch = (Match_SpanningTree_iter_0)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			Pattern_SpanningTree.Match_SpanningTree subpattern_sptr = curMatch.@_sptr;
			graph.SettingAddedNodeNames( SpanningTree_iter_0_addedNodeNames );
			{ // sptr
			Pattern_SpanningTree.Instance.SpanningTree_Modify(actionEnv, subpattern_sptr);
			}
			graph.SettingAddedEdgeNames( SpanningTree_iter_0_addedEdgeNames );
			{ // eval_0
			graph.SetVisited(edge_e, 0, true);
			graph.SetVisited(node_next, 0, true);
			}
		}
		private static string[] SpanningTree_iter_0_addedNodeNames = new string[] {  };
		private static string[] SpanningTree_iter_0_addedEdgeNames = new string[] {  };

		public void SpanningTree_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTree_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge_e = curMatch._edge_e;
			Pattern_SpanningTree.Match_SpanningTree subpattern_sptr = curMatch.@_sptr;
			graph.Remove(edge_e);
			graph.RemoveEdges(node_root);
			graph.Remove(node_root);
			graph.RemoveEdges(node_next);
			graph.Remove(node_next);
			Pattern_SpanningTree.Instance.SpanningTree_Delete(actionEnv, subpattern_sptr);
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
			// further match object stuff
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
			@Pattern_SpanningTree.Match_SpanningTree @sptr { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
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
			public object getVariable(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				case "iter_0": return _iter_0;
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTree.instance.pat_SpanningTree; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTree(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTree(Match_SpanningTree that)
			{
				_node_root = that._node_root;
				_iter_0 = that._iter_0;
			}
			public Match_SpanningTree()
			{
			}
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
				case "next": return _node_next;
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "e": return _edge_e;
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
			public object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public @Pattern_SpanningTree.Match_SpanningTree @sptr { get { return @_sptr; } }
			public @Pattern_SpanningTree.Match_SpanningTree @_sptr;
			public enum SpanningTree_iter_0_SubNums { @sptr, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)SpanningTree_iter_0_SubNums.@sptr: return _sptr;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "sptr": return _sptr;
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTree.instance.SpanningTree_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTree_iter_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTree_iter_0(Match_SpanningTree_iter_0 that)
			{
				_node_root = that._node_root;
				_node_next = that._node_next;
				_edge_e = that._edge_e;
				@_sptr = that.@_sptr;
			}
			public Match_SpanningTree_iter_0()
			{
			}
		}

	}

	public class Pattern_SpanningTreeReverse : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_SpanningTreeReverse instance = null;
		public static Pattern_SpanningTreeReverse Instance { get { if (instance==null) { instance = new Pattern_SpanningTreeReverse(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] SpanningTreeReverse_node_root_AllowedTypes = null;
		public static bool[] SpanningTreeReverse_node_root_IsAllowedType = null;
		public enum SpanningTreeReverse_NodeNums { @root, };
		public enum SpanningTreeReverse_EdgeNums { };
		public enum SpanningTreeReverse_VariableNums { };
		public enum SpanningTreeReverse_SubNums { };
		public enum SpanningTreeReverse_AltNums { };
		public enum SpanningTreeReverse_IterNums { @iter_0, };





		public GRGEN_LGSP.PatternGraph pat_SpanningTreeReverse;

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





		public GRGEN_LGSP.PatternGraph SpanningTreeReverse_iter_0;


		private Pattern_SpanningTreeReverse()
		{
			name = "SpanningTreeReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "SpanningTreeReverse_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] SpanningTreeReverse_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] SpanningTreeReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] SpanningTreeReverse_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] SpanningTreeReverse_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode SpanningTreeReverse_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTreeReverse_node_root", "root", SpanningTreeReverse_node_root_AllowedTypes, SpanningTreeReverse_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			bool[,] SpanningTreeReverse_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] SpanningTreeReverse_iter_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] SpanningTreeReverse_iter_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] SpanningTreeReverse_iter_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode SpanningTreeReverse_iter_0_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTreeReverse_iter_0_node_next", "next", SpanningTreeReverse_iter_0_node_next_AllowedTypes, SpanningTreeReverse_iter_0_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SpanningTreeReverse_iter_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IEdge", "SpanningTreeReverse_iter_0_edge__edge0", "_edge0", SpanningTreeReverse_iter_0_edge__edge0_AllowedTypes, SpanningTreeReverse_iter_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding SpanningTreeReverse_iter_0_sptrr = new GRGEN_LGSP.PatternGraphEmbedding("sptrr", Pattern_SpanningTreeReverse.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("SpanningTreeReverse_iter_0_node_next"),
				}, 
				new string[] { }, new string[] { "SpanningTreeReverse_iter_0_node_next" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			SpanningTreeReverse_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"SpanningTreeReverse_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeReverse_node_root, SpanningTreeReverse_iter_0_node_next }, 
				new GRGEN_LGSP.PatternEdge[] { SpanningTreeReverse_iter_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { SpanningTreeReverse_iter_0_sptrr }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				SpanningTreeReverse_iter_0_isNodeHomomorphicGlobal,
				SpanningTreeReverse_iter_0_isEdgeHomomorphicGlobal,
				SpanningTreeReverse_iter_0_isNodeTotallyHomomorphic,
				SpanningTreeReverse_iter_0_isEdgeTotallyHomomorphic
			);
			SpanningTreeReverse_iter_0.edgeToSourceNode.Add(SpanningTreeReverse_iter_0_edge__edge0, SpanningTreeReverse_node_root);
			SpanningTreeReverse_iter_0.edgeToTargetNode.Add(SpanningTreeReverse_iter_0_edge__edge0, SpanningTreeReverse_iter_0_node_next);

			GRGEN_LGSP.Iterated SpanningTreeReverse_iter_0_it = new GRGEN_LGSP.Iterated( SpanningTreeReverse_iter_0, 0, 0);
			pat_SpanningTreeReverse = new GRGEN_LGSP.PatternGraph(
				"SpanningTreeReverse",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeReverse_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] { SpanningTreeReverse_iter_0_it,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				SpanningTreeReverse_isNodeHomomorphicGlobal,
				SpanningTreeReverse_isEdgeHomomorphicGlobal,
				SpanningTreeReverse_isNodeTotallyHomomorphic,
				SpanningTreeReverse_isEdgeTotallyHomomorphic
			);
			SpanningTreeReverse_iter_0.embeddingGraph = pat_SpanningTreeReverse;

			SpanningTreeReverse_node_root.pointOfDefinition = null;
			SpanningTreeReverse_iter_0_node_next.pointOfDefinition = SpanningTreeReverse_iter_0;
			SpanningTreeReverse_iter_0_edge__edge0.pointOfDefinition = SpanningTreeReverse_iter_0;
			SpanningTreeReverse_iter_0_sptrr.PointOfDefinition = SpanningTreeReverse_iter_0;

			patternGraph = pat_SpanningTreeReverse;
		}


		public void SpanningTreeReverse_Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_SpanningTreeReverse curMatch = (Match_SpanningTreeReverse)_curMatch;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> iterated_iter_0 = curMatch._iter_0;
			graph.SettingAddedNodeNames( SpanningTreeReverse_addedNodeNames );
			SpanningTreeReverse_iter_0_Modify(actionEnv, iterated_iter_0);
			graph.SettingAddedEdgeNames( SpanningTreeReverse_addedEdgeNames );
		}
		private static string[] SpanningTreeReverse_addedNodeNames = new string[] {  };
		private static string[] SpanningTreeReverse_addedEdgeNames = new string[] {  };

		public void SpanningTreeReverse_Create(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPNode node_root)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			graph.SettingAddedNodeNames( create_SpanningTreeReverse_addedNodeNames );
			graph.SettingAddedEdgeNames( create_SpanningTreeReverse_addedEdgeNames );
		}
		private static string[] create_SpanningTreeReverse_addedNodeNames = new string[] {  };
		private static string[] create_SpanningTreeReverse_addedEdgeNames = new string[] {  };

		public void SpanningTreeReverse_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTreeReverse curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> iterated_iter_0 = curMatch._iter_0;
			SpanningTreeReverse_iter_0_Delete(actionEnv, iterated_iter_0);
		}

		public void SpanningTreeReverse_iter_0_Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> curMatches)
		{
			for(Match_SpanningTreeReverse_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTreeReverse_iter_0_Modify(actionEnv, curMatch);
			}
		}

		public void SpanningTreeReverse_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeReverse_iter_0, IMatch_SpanningTreeReverse_iter_0> curMatches)
		{
			for(Match_SpanningTreeReverse_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTreeReverse_iter_0_Delete(actionEnv, curMatch);
			}
		}

		public void SpanningTreeReverse_iter_0_Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_SpanningTreeReverse_iter_0 curMatch = (Match_SpanningTreeReverse_iter_0)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern_sptrr = curMatch.@_sptrr;
			graph.SettingAddedNodeNames( SpanningTreeReverse_iter_0_addedNodeNames );
			{ // sptrr
			Pattern_SpanningTreeReverse.Instance.SpanningTreeReverse_Modify(actionEnv, subpattern_sptrr);
			}
			graph.SettingAddedEdgeNames( SpanningTreeReverse_iter_0_addedEdgeNames );
			GRGEN_MODEL.@Edge edge__edge1 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_next, node_root);
			graph.Remove(edge__edge0);
		}
		private static string[] SpanningTreeReverse_iter_0_addedNodeNames = new string[] {  };
		private static string[] SpanningTreeReverse_iter_0_addedEdgeNames = new string[] { "_edge1" };

		public void SpanningTreeReverse_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTreeReverse_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern_sptrr = curMatch.@_sptrr;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_root);
			graph.Remove(node_root);
			graph.RemoveEdges(node_next);
			graph.Remove(node_next);
			Pattern_SpanningTreeReverse.Instance.SpanningTreeReverse_Delete(actionEnv, subpattern_sptrr);
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
			// further match object stuff
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
			// further match object stuff
			bool IsNullMatch { get; }
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
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
			public object getVariable(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				case "iter_0": return _iter_0;
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeReverse.instance.pat_SpanningTreeReverse; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTreeReverse(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTreeReverse(Match_SpanningTreeReverse that)
			{
				_node_root = that._node_root;
				_iter_0 = that._iter_0;
			}
			public Match_SpanningTreeReverse()
			{
			}
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
				case "next": return _node_next;
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
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
			public object getVariable(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "sptrr": return _sptrr;
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeReverse.instance.SpanningTreeReverse_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTreeReverse_iter_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTreeReverse_iter_0(Match_SpanningTreeReverse_iter_0 that)
			{
				_node_root = that._node_root;
				_node_next = that._node_next;
				_edge__edge0 = that._edge__edge0;
				@_sptrr = that.@_sptrr;
			}
			public Match_SpanningTreeReverse_iter_0()
			{
			}
		}

	}

	public class Pattern_SpanningTreeOutgoing : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_SpanningTreeOutgoing instance = null;
		public static Pattern_SpanningTreeOutgoing Instance { get { if (instance==null) { instance = new Pattern_SpanningTreeOutgoing(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] SpanningTreeOutgoing_node_root_AllowedTypes = null;
		public static bool[] SpanningTreeOutgoing_node_root_IsAllowedType = null;
		public enum SpanningTreeOutgoing_NodeNums { @root, };
		public enum SpanningTreeOutgoing_EdgeNums { };
		public enum SpanningTreeOutgoing_VariableNums { };
		public enum SpanningTreeOutgoing_SubNums { };
		public enum SpanningTreeOutgoing_AltNums { };
		public enum SpanningTreeOutgoing_IterNums { @iter_0, };



		public GRGEN_LGSP.PatternGraph pat_SpanningTreeOutgoing;

		public static GRGEN_LIBGR.NodeType[] SpanningTreeOutgoing_iter_0_node_next_AllowedTypes = null;
		public static bool[] SpanningTreeOutgoing_iter_0_node_next_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] SpanningTreeOutgoing_iter_0_edge__edge0_AllowedTypes = null;
		public static bool[] SpanningTreeOutgoing_iter_0_edge__edge0_IsAllowedType = null;
		public enum SpanningTreeOutgoing_iter_0_NodeNums { @root, @next, };
		public enum SpanningTreeOutgoing_iter_0_EdgeNums { @_edge0, };
		public enum SpanningTreeOutgoing_iter_0_VariableNums { };
		public enum SpanningTreeOutgoing_iter_0_SubNums { @_sub0, };
		public enum SpanningTreeOutgoing_iter_0_AltNums { };
		public enum SpanningTreeOutgoing_iter_0_IterNums { };



		public GRGEN_LGSP.PatternGraph SpanningTreeOutgoing_iter_0;


		private Pattern_SpanningTreeOutgoing()
		{
			name = "SpanningTreeOutgoing";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "SpanningTreeOutgoing_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] SpanningTreeOutgoing_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] SpanningTreeOutgoing_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] SpanningTreeOutgoing_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] SpanningTreeOutgoing_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode SpanningTreeOutgoing_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTreeOutgoing_node_root", "root", SpanningTreeOutgoing_node_root_AllowedTypes, SpanningTreeOutgoing_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			bool[,] SpanningTreeOutgoing_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] SpanningTreeOutgoing_iter_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] SpanningTreeOutgoing_iter_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] SpanningTreeOutgoing_iter_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode SpanningTreeOutgoing_iter_0_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTreeOutgoing_iter_0_node_next", "next", SpanningTreeOutgoing_iter_0_node_next_AllowedTypes, SpanningTreeOutgoing_iter_0_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SpanningTreeOutgoing_iter_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IEdge", "SpanningTreeOutgoing_iter_0_edge__edge0", "_edge0", SpanningTreeOutgoing_iter_0_edge__edge0_AllowedTypes, SpanningTreeOutgoing_iter_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding SpanningTreeOutgoing_iter_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_SpanningTreeOutgoing.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("SpanningTreeOutgoing_iter_0_node_next"),
				}, 
				new string[] { }, new string[] { "SpanningTreeOutgoing_iter_0_node_next" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			SpanningTreeOutgoing_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"SpanningTreeOutgoing_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeOutgoing_node_root, SpanningTreeOutgoing_iter_0_node_next }, 
				new GRGEN_LGSP.PatternEdge[] { SpanningTreeOutgoing_iter_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { SpanningTreeOutgoing_iter_0__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				SpanningTreeOutgoing_iter_0_isNodeHomomorphicGlobal,
				SpanningTreeOutgoing_iter_0_isEdgeHomomorphicGlobal,
				SpanningTreeOutgoing_iter_0_isNodeTotallyHomomorphic,
				SpanningTreeOutgoing_iter_0_isEdgeTotallyHomomorphic
			);
			SpanningTreeOutgoing_iter_0.edgeToSourceNode.Add(SpanningTreeOutgoing_iter_0_edge__edge0, SpanningTreeOutgoing_node_root);
			SpanningTreeOutgoing_iter_0.edgeToTargetNode.Add(SpanningTreeOutgoing_iter_0_edge__edge0, SpanningTreeOutgoing_iter_0_node_next);

			GRGEN_LGSP.Iterated SpanningTreeOutgoing_iter_0_it = new GRGEN_LGSP.Iterated( SpanningTreeOutgoing_iter_0, 0, 0);
			pat_SpanningTreeOutgoing = new GRGEN_LGSP.PatternGraph(
				"SpanningTreeOutgoing",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeOutgoing_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] { SpanningTreeOutgoing_iter_0_it,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				SpanningTreeOutgoing_isNodeHomomorphicGlobal,
				SpanningTreeOutgoing_isEdgeHomomorphicGlobal,
				SpanningTreeOutgoing_isNodeTotallyHomomorphic,
				SpanningTreeOutgoing_isEdgeTotallyHomomorphic
			);
			SpanningTreeOutgoing_iter_0.embeddingGraph = pat_SpanningTreeOutgoing;

			SpanningTreeOutgoing_node_root.pointOfDefinition = null;
			SpanningTreeOutgoing_iter_0_node_next.pointOfDefinition = SpanningTreeOutgoing_iter_0;
			SpanningTreeOutgoing_iter_0_edge__edge0.pointOfDefinition = SpanningTreeOutgoing_iter_0;
			SpanningTreeOutgoing_iter_0__sub0.PointOfDefinition = SpanningTreeOutgoing_iter_0;

			patternGraph = pat_SpanningTreeOutgoing;
		}


		public void SpanningTreeOutgoing_Create(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPNode node_root)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			graph.SettingAddedNodeNames( create_SpanningTreeOutgoing_addedNodeNames );
			graph.SettingAddedEdgeNames( create_SpanningTreeOutgoing_addedEdgeNames );
		}
		private static string[] create_SpanningTreeOutgoing_addedNodeNames = new string[] {  };
		private static string[] create_SpanningTreeOutgoing_addedEdgeNames = new string[] {  };

		public void SpanningTreeOutgoing_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTreeOutgoing curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeOutgoing_iter_0, IMatch_SpanningTreeOutgoing_iter_0> iterated_iter_0 = curMatch._iter_0;
			SpanningTreeOutgoing_iter_0_Delete(actionEnv, iterated_iter_0);
		}

		public void SpanningTreeOutgoing_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeOutgoing_iter_0, IMatch_SpanningTreeOutgoing_iter_0> curMatches)
		{
			for(Match_SpanningTreeOutgoing_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTreeOutgoing_iter_0_Delete(actionEnv, curMatch);
			}
		}

		public void SpanningTreeOutgoing_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTreeOutgoing_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_root);
			graph.Remove(node_root);
			graph.RemoveEdges(node_next);
			graph.Remove(node_next);
			Pattern_SpanningTreeOutgoing.Instance.SpanningTreeOutgoing_Delete(actionEnv, subpattern__sub0);
		}

		static Pattern_SpanningTreeOutgoing() {
		}

		public interface IMatch_SpanningTreeOutgoing : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTreeOutgoing_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_SpanningTreeOutgoing_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			GRGEN_LIBGR.INode node_next { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SpanningTreeOutgoing : GRGEN_LGSP.ListElement<Match_SpanningTreeOutgoing>, IMatch_SpanningTreeOutgoing
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum SpanningTreeOutgoing_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeOutgoing_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
				default: return null;
				}
			}
			
			public enum SpanningTreeOutgoing_EdgeNums { END_OF_ENUM };
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
			
			public enum SpanningTreeOutgoing_VariableNums { END_OF_ENUM };
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
			
			public enum SpanningTreeOutgoing_SubNums { END_OF_ENUM };
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
			
			public enum SpanningTreeOutgoing_AltNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTreeOutgoing_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeOutgoing_iter_0, IMatch_SpanningTreeOutgoing_iter_0> _iter_0;
			public enum SpanningTreeOutgoing_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeOutgoing_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				case "iter_0": return _iter_0;
				default: return null;
				}
			}
			
			public enum SpanningTreeOutgoing_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeOutgoing.instance.pat_SpanningTreeOutgoing; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTreeOutgoing(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTreeOutgoing(Match_SpanningTreeOutgoing that)
			{
				_node_root = that._node_root;
				_iter_0 = that._iter_0;
			}
			public Match_SpanningTreeOutgoing()
			{
			}
		}

		public class Match_SpanningTreeOutgoing_iter_0 : GRGEN_LGSP.ListElement<Match_SpanningTreeOutgoing_iter_0>, IMatch_SpanningTreeOutgoing_iter_0
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LIBGR.INode node_next { get { return (GRGEN_LIBGR.INode)_node_next; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public GRGEN_LGSP.LGSPNode _node_next;
			public enum SpanningTreeOutgoing_iter_0_NodeNums { @root, @next, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeOutgoing_iter_0_NodeNums.@root: return _node_root;
				case (int)SpanningTreeOutgoing_iter_0_NodeNums.@next: return _node_next;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
				case "next": return _node_next;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum SpanningTreeOutgoing_iter_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeOutgoing_iter_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}
			
			public enum SpanningTreeOutgoing_iter_0_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing @_sub0 { get { return @__sub0; } }
			public @Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing @__sub0;
			public enum SpanningTreeOutgoing_iter_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeOutgoing_iter_0_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "_sub0": return __sub0;
				default: return null;
				}
			}
			
			public enum SpanningTreeOutgoing_iter_0_AltNums { END_OF_ENUM };
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
			
			public enum SpanningTreeOutgoing_iter_0_IterNums { END_OF_ENUM };
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
			
			public enum SpanningTreeOutgoing_iter_0_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeOutgoing.instance.SpanningTreeOutgoing_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTreeOutgoing_iter_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTreeOutgoing_iter_0(Match_SpanningTreeOutgoing_iter_0 that)
			{
				_node_root = that._node_root;
				_node_next = that._node_next;
				_edge__edge0 = that._edge__edge0;
				@__sub0 = that.@__sub0;
			}
			public Match_SpanningTreeOutgoing_iter_0()
			{
			}
		}

	}

	public class Pattern_SpanningTreeIncoming : GRGEN_LGSP.LGSPMatchingPattern
	{
		private static Pattern_SpanningTreeIncoming instance = null;
		public static Pattern_SpanningTreeIncoming Instance { get { if (instance==null) { instance = new Pattern_SpanningTreeIncoming(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] SpanningTreeIncoming_node_root_AllowedTypes = null;
		public static bool[] SpanningTreeIncoming_node_root_IsAllowedType = null;
		public enum SpanningTreeIncoming_NodeNums { @root, };
		public enum SpanningTreeIncoming_EdgeNums { };
		public enum SpanningTreeIncoming_VariableNums { };
		public enum SpanningTreeIncoming_SubNums { };
		public enum SpanningTreeIncoming_AltNums { };
		public enum SpanningTreeIncoming_IterNums { @iter_0, };



		public GRGEN_LGSP.PatternGraph pat_SpanningTreeIncoming;

		public static GRGEN_LIBGR.NodeType[] SpanningTreeIncoming_iter_0_node_next_AllowedTypes = null;
		public static bool[] SpanningTreeIncoming_iter_0_node_next_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] SpanningTreeIncoming_iter_0_edge__edge0_AllowedTypes = null;
		public static bool[] SpanningTreeIncoming_iter_0_edge__edge0_IsAllowedType = null;
		public enum SpanningTreeIncoming_iter_0_NodeNums { @next, @root, };
		public enum SpanningTreeIncoming_iter_0_EdgeNums { @_edge0, };
		public enum SpanningTreeIncoming_iter_0_VariableNums { };
		public enum SpanningTreeIncoming_iter_0_SubNums { @_sub0, };
		public enum SpanningTreeIncoming_iter_0_AltNums { };
		public enum SpanningTreeIncoming_iter_0_IterNums { };



		public GRGEN_LGSP.PatternGraph SpanningTreeIncoming_iter_0;


		private Pattern_SpanningTreeIncoming()
		{
			name = "SpanningTreeIncoming";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "SpanningTreeIncoming_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };

		}
		private void initialize()
		{
			bool[,] SpanningTreeIncoming_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] SpanningTreeIncoming_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] SpanningTreeIncoming_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] SpanningTreeIncoming_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode SpanningTreeIncoming_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTreeIncoming_node_root", "root", SpanningTreeIncoming_node_root_AllowedTypes, SpanningTreeIncoming_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			bool[,] SpanningTreeIncoming_iter_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] SpanningTreeIncoming_iter_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] SpanningTreeIncoming_iter_0_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] SpanningTreeIncoming_iter_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode SpanningTreeIncoming_iter_0_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "SpanningTreeIncoming_iter_0_node_next", "next", SpanningTreeIncoming_iter_0_node_next_AllowedTypes, SpanningTreeIncoming_iter_0_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SpanningTreeIncoming_iter_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@Edge, GRGEN_MODEL.EdgeType_Edge.typeVar, "GRGEN_LIBGR.IEdge", "SpanningTreeIncoming_iter_0_edge__edge0", "_edge0", SpanningTreeIncoming_iter_0_edge__edge0_AllowedTypes, SpanningTreeIncoming_iter_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding SpanningTreeIncoming_iter_0__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_SpanningTreeIncoming.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("SpanningTreeIncoming_iter_0_node_next"),
				}, 
				new string[] { }, new string[] { "SpanningTreeIncoming_iter_0_node_next" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			SpanningTreeIncoming_iter_0 = new GRGEN_LGSP.PatternGraph(
				"iter_0",
				"SpanningTreeIncoming_",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeIncoming_iter_0_node_next, SpanningTreeIncoming_node_root }, 
				new GRGEN_LGSP.PatternEdge[] { SpanningTreeIncoming_iter_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { SpanningTreeIncoming_iter_0__sub0 }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				SpanningTreeIncoming_iter_0_isNodeHomomorphicGlobal,
				SpanningTreeIncoming_iter_0_isEdgeHomomorphicGlobal,
				SpanningTreeIncoming_iter_0_isNodeTotallyHomomorphic,
				SpanningTreeIncoming_iter_0_isEdgeTotallyHomomorphic
			);
			SpanningTreeIncoming_iter_0.edgeToSourceNode.Add(SpanningTreeIncoming_iter_0_edge__edge0, SpanningTreeIncoming_iter_0_node_next);
			SpanningTreeIncoming_iter_0.edgeToTargetNode.Add(SpanningTreeIncoming_iter_0_edge__edge0, SpanningTreeIncoming_node_root);

			GRGEN_LGSP.Iterated SpanningTreeIncoming_iter_0_it = new GRGEN_LGSP.Iterated( SpanningTreeIncoming_iter_0, 0, 0);
			pat_SpanningTreeIncoming = new GRGEN_LGSP.PatternGraph(
				"SpanningTreeIncoming",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SpanningTreeIncoming_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] { SpanningTreeIncoming_iter_0_it,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				SpanningTreeIncoming_isNodeHomomorphicGlobal,
				SpanningTreeIncoming_isEdgeHomomorphicGlobal,
				SpanningTreeIncoming_isNodeTotallyHomomorphic,
				SpanningTreeIncoming_isEdgeTotallyHomomorphic
			);
			SpanningTreeIncoming_iter_0.embeddingGraph = pat_SpanningTreeIncoming;

			SpanningTreeIncoming_node_root.pointOfDefinition = null;
			SpanningTreeIncoming_iter_0_node_next.pointOfDefinition = SpanningTreeIncoming_iter_0;
			SpanningTreeIncoming_iter_0_edge__edge0.pointOfDefinition = SpanningTreeIncoming_iter_0;
			SpanningTreeIncoming_iter_0__sub0.PointOfDefinition = SpanningTreeIncoming_iter_0;

			patternGraph = pat_SpanningTreeIncoming;
		}


		public void SpanningTreeIncoming_Create(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPNode node_root)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			graph.SettingAddedNodeNames( create_SpanningTreeIncoming_addedNodeNames );
			graph.SettingAddedEdgeNames( create_SpanningTreeIncoming_addedEdgeNames );
		}
		private static string[] create_SpanningTreeIncoming_addedNodeNames = new string[] {  };
		private static string[] create_SpanningTreeIncoming_addedEdgeNames = new string[] {  };

		public void SpanningTreeIncoming_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTreeIncoming curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeIncoming_iter_0, IMatch_SpanningTreeIncoming_iter_0> iterated_iter_0 = curMatch._iter_0;
			SpanningTreeIncoming_iter_0_Delete(actionEnv, iterated_iter_0);
		}

		public void SpanningTreeIncoming_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeIncoming_iter_0, IMatch_SpanningTreeIncoming_iter_0> curMatches)
		{
			for(Match_SpanningTreeIncoming_iter_0 curMatch=curMatches.Root; curMatch!=null; curMatch=curMatch.next) {
				SpanningTreeIncoming_iter_0_Delete(actionEnv, curMatch);
			}
		}

		public void SpanningTreeIncoming_iter_0_Delete(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, Match_SpanningTreeIncoming_iter_0 curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming subpattern__sub0 = curMatch.@__sub0;
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_next);
			graph.Remove(node_next);
			graph.RemoveEdges(node_root);
			graph.Remove(node_root);
			Pattern_SpanningTreeIncoming.Instance.SpanningTreeIncoming_Delete(actionEnv, subpattern__sub0);
		}

		static Pattern_SpanningTreeIncoming() {
		}

		public interface IMatch_SpanningTreeIncoming : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTreeIncoming_iter_0> iter_0 { get; }
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_SpanningTreeIncoming_iter_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_next { get; }
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			GRGEN_LIBGR.IEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			bool IsNullMatch { get; }
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SpanningTreeIncoming : GRGEN_LGSP.ListElement<Match_SpanningTreeIncoming>, IMatch_SpanningTreeIncoming
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum SpanningTreeIncoming_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeIncoming_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
				default: return null;
				}
			}
			
			public enum SpanningTreeIncoming_EdgeNums { END_OF_ENUM };
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
			
			public enum SpanningTreeIncoming_VariableNums { END_OF_ENUM };
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
			
			public enum SpanningTreeIncoming_SubNums { END_OF_ENUM };
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
			
			public enum SpanningTreeIncoming_AltNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IMatchesExact<IMatch_SpanningTreeIncoming_iter_0> iter_0 { get { return _iter_0; } }
			public GRGEN_LGSP.LGSPMatchesList<Match_SpanningTreeIncoming_iter_0, IMatch_SpanningTreeIncoming_iter_0> _iter_0;
			public enum SpanningTreeIncoming_IterNums { @iter_0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 1;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeIncoming_IterNums.@iter_0: return _iter_0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				case "iter_0": return _iter_0;
				default: return null;
				}
			}
			
			public enum SpanningTreeIncoming_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeIncoming.instance.pat_SpanningTreeIncoming; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTreeIncoming(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTreeIncoming(Match_SpanningTreeIncoming that)
			{
				_node_root = that._node_root;
				_iter_0 = that._iter_0;
			}
			public Match_SpanningTreeIncoming()
			{
			}
		}

		public class Match_SpanningTreeIncoming_iter_0 : GRGEN_LGSP.ListElement<Match_SpanningTreeIncoming_iter_0>, IMatch_SpanningTreeIncoming_iter_0
		{
			public GRGEN_LIBGR.INode node_next { get { return (GRGEN_LIBGR.INode)_node_next; } }
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum SpanningTreeIncoming_iter_0_NodeNums { @next, @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeIncoming_iter_0_NodeNums.@next: return _node_next;
				case (int)SpanningTreeIncoming_iter_0_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "next": return _node_next;
				case "root": return _node_root;
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IEdge edge__edge0 { get { return (GRGEN_LIBGR.IEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum SpanningTreeIncoming_iter_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeIncoming_iter_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}
			
			public enum SpanningTreeIncoming_iter_0_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming @_sub0 { get { return @__sub0; } }
			public @Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming @__sub0;
			public enum SpanningTreeIncoming_iter_0_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)SpanningTreeIncoming_iter_0_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "_sub0": return __sub0;
				default: return null;
				}
			}
			
			public enum SpanningTreeIncoming_iter_0_AltNums { END_OF_ENUM };
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
			
			public enum SpanningTreeIncoming_iter_0_IterNums { END_OF_ENUM };
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
			
			public enum SpanningTreeIncoming_iter_0_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Pattern_SpanningTreeIncoming.instance.SpanningTreeIncoming_iter_0; } }
			public bool IsNullMatch { get { return _isNullMatch; } }
			public bool _isNullMatch;
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SpanningTreeIncoming_iter_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_SpanningTreeIncoming_iter_0(Match_SpanningTreeIncoming_iter_0 that)
			{
				_node_next = that._node_next;
				_node_root = that._node_root;
				_edge__edge0 = that._edge__edge0;
				@__sub0 = that.@__sub0;
			}
			public Match_SpanningTreeIncoming_iter_0()
			{
			}
		}

	}

	public class Rule_initTree : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_initTree instance = null;
		public static Rule_initTree Instance { get { if (instance==null) { instance = new Rule_initTree(); instance.initialize(); } return instance; } }

		public enum initTree_NodeNums { };
		public enum initTree_EdgeNums { };
		public enum initTree_VariableNums { };
		public enum initTree_SubNums { };
		public enum initTree_AltNums { };
		public enum initTree_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_initTree;


		private Rule_initTree()
		{
			name = "initTree";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] initTree_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] initTree_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] initTree_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] initTree_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_initTree = new GRGEN_LGSP.PatternGraph(
				"initTree",
				"",
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
				initTree_isNodeHomomorphicGlobal,
				initTree_isEdgeHomomorphicGlobal,
				initTree_isNodeTotallyHomomorphic,
				initTree_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_initTree;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_initTree curMatch = (Match_initTree)_curMatch;
			graph.SettingAddedNodeNames( initTree_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_left = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_right = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_rightleft = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node3 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node4 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node5 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node6 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( initTree_addedEdgeNames );
			GRGEN_MODEL.@UEdge edge__edge0 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node_left);
			GRGEN_MODEL.@UEdge edge__edge1 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_left, node__node0);
			GRGEN_MODEL.@UEdge edge__edge2 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_left, node__node1);
			GRGEN_MODEL.@UEdge edge__edge3 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_left, node__node2);
			GRGEN_MODEL.@UEdge edge__edge4 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node_right);
			GRGEN_MODEL.@UEdge edge__edge5 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_right, node_rightleft);
			GRGEN_MODEL.@UEdge edge__edge6 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_rightleft, node__node3);
			GRGEN_MODEL.@UEdge edge__edge7 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_rightleft, node__node4);
			GRGEN_MODEL.@UEdge edge__edge8 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_rightleft, node__node5);
			GRGEN_MODEL.@UEdge edge__edge9 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_right, node__node6);
			output_0 = (GRGEN_LIBGR.INode)(node_root);
			return;
		}
		private static string[] initTree_addedNodeNames = new string[] { "root", "left", "_node0", "_node1", "_node2", "right", "rightleft", "_node3", "_node4", "_node5", "_node6" };
		private static string[] initTree_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7", "_edge8", "_edge9" };

		static Rule_initTree() {
		}

		public interface IMatch_initTree : GRGEN_LIBGR.IMatch
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

		public class Match_initTree : GRGEN_LGSP.ListElement<Match_initTree>, IMatch_initTree
		{
			public enum initTree_NodeNums { END_OF_ENUM };
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
			
			public enum initTree_EdgeNums { END_OF_ENUM };
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
			
			public enum initTree_VariableNums { END_OF_ENUM };
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
			
			public enum initTree_SubNums { END_OF_ENUM };
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
			
			public enum initTree_AltNums { END_OF_ENUM };
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
			
			public enum initTree_IterNums { END_OF_ENUM };
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
			
			public enum initTree_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_initTree.instance.pat_initTree; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_initTree(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_initTree(Match_initTree that)
			{
			}
			public Match_initTree()
			{
			}
		}

	}

	public class Rule_initUndirected : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_initUndirected instance = null;
		public static Rule_initUndirected Instance { get { if (instance==null) { instance = new Rule_initUndirected(); instance.initialize(); } return instance; } }

		public enum initUndirected_NodeNums { };
		public enum initUndirected_EdgeNums { };
		public enum initUndirected_VariableNums { };
		public enum initUndirected_SubNums { };
		public enum initUndirected_AltNums { };
		public enum initUndirected_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_initUndirected;


		private Rule_initUndirected()
		{
			name = "initUndirected";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] initUndirected_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] initUndirected_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] initUndirected_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] initUndirected_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_initUndirected = new GRGEN_LGSP.PatternGraph(
				"initUndirected",
				"",
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
				initUndirected_isNodeHomomorphicGlobal,
				initUndirected_isEdgeHomomorphicGlobal,
				initUndirected_isNodeTotallyHomomorphic,
				initUndirected_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_initUndirected;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_initUndirected curMatch = (Match_initUndirected)_curMatch;
			graph.SettingAddedNodeNames( initUndirected_addedNodeNames );
			GRGEN_MODEL.@Node node_root = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node0 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node1 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node2 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node_n = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node3 = GRGEN_MODEL.@Node.CreateNode(graph);
			GRGEN_MODEL.@Node node__node4 = GRGEN_MODEL.@Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( initUndirected_addedEdgeNames );
			GRGEN_MODEL.@UEdge edge__edge0 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node__node0);
			GRGEN_MODEL.@UEdge edge__edge1 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node__node1);
			GRGEN_MODEL.@UEdge edge__edge2 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node1, node__node2);
			GRGEN_MODEL.@UEdge edge__edge3 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_root, node_n);
			GRGEN_MODEL.@UEdge edge__edge4 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_n, node_root);
			GRGEN_MODEL.@UEdge edge__edge5 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node_n, node__node3);
			GRGEN_MODEL.@UEdge edge__edge6 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node3, node__node4);
			GRGEN_MODEL.@UEdge edge__edge7 = GRGEN_MODEL.@UEdge.CreateEdge(graph, node__node4, node_n);
			output_0 = (GRGEN_LIBGR.INode)(node_root);
			return;
		}
		private static string[] initUndirected_addedNodeNames = new string[] { "root", "_node0", "_node1", "_node2", "n", "_node3", "_node4" };
		private static string[] initUndirected_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7" };

		static Rule_initUndirected() {
		}

		public interface IMatch_initUndirected : GRGEN_LIBGR.IMatch
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

		public class Match_initUndirected : GRGEN_LGSP.ListElement<Match_initUndirected>, IMatch_initUndirected
		{
			public enum initUndirected_NodeNums { END_OF_ENUM };
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
			
			public enum initUndirected_EdgeNums { END_OF_ENUM };
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
			
			public enum initUndirected_VariableNums { END_OF_ENUM };
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
			
			public enum initUndirected_SubNums { END_OF_ENUM };
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
			
			public enum initUndirected_AltNums { END_OF_ENUM };
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
			
			public enum initUndirected_IterNums { END_OF_ENUM };
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
			
			public enum initUndirected_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_initUndirected.instance.pat_initUndirected; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_initUndirected(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_initUndirected(Match_initUndirected that)
			{
			}
			public Match_initUndirected()
			{
			}
		}

	}

	public class Rule_initDirected : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_initDirected instance = null;
		public static Rule_initDirected Instance { get { if (instance==null) { instance = new Rule_initDirected(); instance.initialize(); } return instance; } }

		public enum initDirected_NodeNums { };
		public enum initDirected_EdgeNums { };
		public enum initDirected_VariableNums { };
		public enum initDirected_SubNums { };
		public enum initDirected_AltNums { };
		public enum initDirected_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_initDirected;


		private Rule_initDirected()
		{
			name = "initDirected";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] initDirected_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] initDirected_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] initDirected_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] initDirected_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_initDirected = new GRGEN_LGSP.PatternGraph(
				"initDirected",
				"",
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
				initDirected_isNodeHomomorphicGlobal,
				initDirected_isEdgeHomomorphicGlobal,
				initDirected_isNodeTotallyHomomorphic,
				initDirected_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_initDirected;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_LIBGR.INode output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
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
			GRGEN_MODEL.@Edge edge__edge4 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_root, node_n);
			GRGEN_MODEL.@Edge edge__edge5 = GRGEN_MODEL.@Edge.CreateEdge(graph, node_n, node__node3);
			GRGEN_MODEL.@Edge edge__edge6 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node3, node__node4);
			GRGEN_MODEL.@Edge edge__edge7 = GRGEN_MODEL.@Edge.CreateEdge(graph, node__node4, node_n);
			output_0 = (GRGEN_LIBGR.INode)(node_root);
			return;
		}
		private static string[] initDirected_addedNodeNames = new string[] { "root", "_node0", "_node1", "_node2", "n", "_node3", "_node4" };
		private static string[] initDirected_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7" };

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
			// further match object stuff
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
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
			public object getVariable(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_initDirected.instance.pat_initDirected; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_initDirected(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_initDirected(Match_initDirected that)
			{
			}
			public Match_initDirected()
			{
			}
		}

	}

	public class Rule_spanningTree : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_spanningTree instance = null;
		public static Rule_spanningTree Instance { get { if (instance==null) { instance = new Rule_spanningTree(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] spanningTree_node_root_AllowedTypes = null;
		public static bool[] spanningTree_node_root_IsAllowedType = null;
		public enum spanningTree_NodeNums { @root, };
		public enum spanningTree_EdgeNums { };
		public enum spanningTree_VariableNums { };
		public enum spanningTree_SubNums { @sptr, };
		public enum spanningTree_AltNums { };
		public enum spanningTree_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_spanningTree;


		private Rule_spanningTree()
		{
			name = "spanningTree";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "spanningTree_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] spanningTree_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] spanningTree_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] spanningTree_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] spanningTree_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode spanningTree_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "spanningTree_node_root", "root", spanningTree_node_root_AllowedTypes, spanningTree_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding spanningTree_sptr = new GRGEN_LGSP.PatternGraphEmbedding("sptr", Pattern_SpanningTree.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("spanningTree_node_root"),
				}, 
				new string[] { }, new string[] { "spanningTree_node_root" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_spanningTree = new GRGEN_LGSP.PatternGraph(
				"spanningTree",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { spanningTree_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { spanningTree_sptr }, 
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
				spanningTree_isNodeHomomorphicGlobal,
				spanningTree_isEdgeHomomorphicGlobal,
				spanningTree_isNodeTotallyHomomorphic,
				spanningTree_isEdgeTotallyHomomorphic
			);

			spanningTree_node_root.pointOfDefinition = null;
			spanningTree_sptr.PointOfDefinition = pat_spanningTree;

			patternGraph = pat_spanningTree;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_spanningTree curMatch = (Match_spanningTree)_curMatch;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			Pattern_SpanningTree.Match_SpanningTree subpattern_sptr = curMatch.@_sptr;
			graph.SettingAddedNodeNames( spanningTree_addedNodeNames );
			{ // sptr
			Pattern_SpanningTree.Instance.SpanningTree_Modify(actionEnv, subpattern_sptr);
			}
			graph.SettingAddedEdgeNames( spanningTree_addedEdgeNames );
			{ // eval_0
			graph.SetVisited(node_root, 0, true);
			}
			return;
		}
		private static string[] spanningTree_addedNodeNames = new string[] {  };
		private static string[] spanningTree_addedEdgeNames = new string[] {  };

		static Rule_spanningTree() {
		}

		public interface IMatch_spanningTree : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTree.Match_SpanningTree @sptr { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
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
			public object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public @Pattern_SpanningTree.Match_SpanningTree @sptr { get { return @_sptr; } }
			public @Pattern_SpanningTree.Match_SpanningTree @_sptr;
			public enum spanningTree_SubNums { @sptr, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)spanningTree_SubNums.@sptr: return _sptr;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "sptr": return _sptr;
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_spanningTree.instance.pat_spanningTree; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_spanningTree(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_spanningTree(Match_spanningTree that)
			{
				_node_root = that._node_root;
				@_sptr = that.@_sptr;
			}
			public Match_spanningTree()
			{
			}
		}

	}

	public class Rule_edgesVisited : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_edgesVisited instance = null;
		public static Rule_edgesVisited Instance { get { if (instance==null) { instance = new Rule_edgesVisited(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.EdgeType[] edgesVisited_edge_e_AllowedTypes = null;
		public static bool[] edgesVisited_edge_e_IsAllowedType = null;
		public enum edgesVisited_NodeNums { };
		public enum edgesVisited_EdgeNums { @e, };
		public enum edgesVisited_VariableNums { };
		public enum edgesVisited_SubNums { };
		public enum edgesVisited_AltNums { };
		public enum edgesVisited_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_edgesVisited;


		private Rule_edgesVisited()
		{
			name = "edgesVisited";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] edgesVisited_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] edgesVisited_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] edgesVisited_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] edgesVisited_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge edgesVisited_edge_e = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@UEdge, GRGEN_MODEL.EdgeType_UEdge.typeVar, "GRGEN_LIBGR.IEdge", "edgesVisited_edge_e", "e", edgesVisited_edge_e_AllowedTypes, edgesVisited_edge_e_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition edgesVisited_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Visited(new GRGEN_EXPR.GraphEntityExpression("edgesVisited_edge_e"), new GRGEN_EXPR.Constant("0")),
				new string[] {  }, new string[] { "edgesVisited_edge_e" }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_edgesVisited = new GRGEN_LGSP.PatternGraph(
				"edgesVisited",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] {  }, 
				new GRGEN_LGSP.PatternEdge[] { edgesVisited_edge_e }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { edgesVisited_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[0, 0] ,
				new bool[1, 1] {
					{ true, },
				},
				edgesVisited_isNodeHomomorphicGlobal,
				edgesVisited_isEdgeHomomorphicGlobal,
				edgesVisited_isNodeTotallyHomomorphic,
				edgesVisited_isEdgeTotallyHomomorphic
			);

			edgesVisited_edge_e.pointOfDefinition = pat_edgesVisited;

			patternGraph = pat_edgesVisited;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_edgesVisited curMatch = (Match_edgesVisited)_curMatch;
			return;
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
			// further match object stuff
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "e": return _edge_e;
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
			public object getVariable(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_edgesVisited.instance.pat_edgesVisited; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_edgesVisited(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_edgesVisited(Match_edgesVisited that)
			{
				_edge_e = that._edge_e;
			}
			public Match_edgesVisited()
			{
			}
		}

	}

	public class Rule_nodesVisited : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_nodesVisited instance = null;
		public static Rule_nodesVisited Instance { get { if (instance==null) { instance = new Rule_nodesVisited(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] nodesVisited_node_n_AllowedTypes = null;
		public static bool[] nodesVisited_node_n_IsAllowedType = null;
		public enum nodesVisited_NodeNums { @n, };
		public enum nodesVisited_EdgeNums { };
		public enum nodesVisited_VariableNums { };
		public enum nodesVisited_SubNums { };
		public enum nodesVisited_AltNums { };
		public enum nodesVisited_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_nodesVisited;


		private Rule_nodesVisited()
		{
			name = "nodesVisited";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] nodesVisited_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] nodesVisited_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] nodesVisited_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] nodesVisited_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode nodesVisited_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "nodesVisited_node_n", "n", nodesVisited_node_n_AllowedTypes, nodesVisited_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition nodesVisited_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Visited(new GRGEN_EXPR.GraphEntityExpression("nodesVisited_node_n"), new GRGEN_EXPR.Constant("0")),
				new string[] { "nodesVisited_node_n" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_nodesVisited = new GRGEN_LGSP.PatternGraph(
				"nodesVisited",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { nodesVisited_node_n }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { nodesVisited_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				nodesVisited_isNodeHomomorphicGlobal,
				nodesVisited_isEdgeHomomorphicGlobal,
				nodesVisited_isNodeTotallyHomomorphic,
				nodesVisited_isEdgeTotallyHomomorphic
			);

			nodesVisited_node_n.pointOfDefinition = pat_nodesVisited;

			patternGraph = pat_nodesVisited;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_nodesVisited curMatch = (Match_nodesVisited)_curMatch;
			return;
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
			// further match object stuff
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
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
			public object getVariable(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_nodesVisited.instance.pat_nodesVisited; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_nodesVisited(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_nodesVisited(Match_nodesVisited that)
			{
				_node_n = that._node_n;
			}
			public Match_nodesVisited()
			{
			}
		}

	}

	public class Rule_spanningTreeReverse : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_spanningTreeReverse instance = null;
		public static Rule_spanningTreeReverse Instance { get { if (instance==null) { instance = new Rule_spanningTreeReverse(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] spanningTreeReverse_node_root_AllowedTypes = null;
		public static bool[] spanningTreeReverse_node_root_IsAllowedType = null;
		public enum spanningTreeReverse_NodeNums { @root, };
		public enum spanningTreeReverse_EdgeNums { };
		public enum spanningTreeReverse_VariableNums { };
		public enum spanningTreeReverse_SubNums { @sptrr, };
		public enum spanningTreeReverse_AltNums { };
		public enum spanningTreeReverse_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_spanningTreeReverse;


		private Rule_spanningTreeReverse()
		{
			name = "spanningTreeReverse";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "spanningTreeReverse_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] spanningTreeReverse_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] spanningTreeReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] spanningTreeReverse_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] spanningTreeReverse_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode spanningTreeReverse_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "spanningTreeReverse_node_root", "root", spanningTreeReverse_node_root_AllowedTypes, spanningTreeReverse_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding spanningTreeReverse_sptrr = new GRGEN_LGSP.PatternGraphEmbedding("sptrr", Pattern_SpanningTreeReverse.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("spanningTreeReverse_node_root"),
				}, 
				new string[] { }, new string[] { "spanningTreeReverse_node_root" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_spanningTreeReverse = new GRGEN_LGSP.PatternGraph(
				"spanningTreeReverse",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { spanningTreeReverse_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { spanningTreeReverse_sptrr }, 
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
				spanningTreeReverse_isNodeHomomorphicGlobal,
				spanningTreeReverse_isEdgeHomomorphicGlobal,
				spanningTreeReverse_isNodeTotallyHomomorphic,
				spanningTreeReverse_isEdgeTotallyHomomorphic
			);

			spanningTreeReverse_node_root.pointOfDefinition = null;
			spanningTreeReverse_sptrr.PointOfDefinition = pat_spanningTreeReverse;

			patternGraph = pat_spanningTreeReverse;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_spanningTreeReverse curMatch = (Match_spanningTreeReverse)_curMatch;
			Pattern_SpanningTreeReverse.Match_SpanningTreeReverse subpattern_sptrr = curMatch.@_sptrr;
			graph.SettingAddedNodeNames( spanningTreeReverse_addedNodeNames );
			{ // sptrr
			Pattern_SpanningTreeReverse.Instance.SpanningTreeReverse_Modify(actionEnv, subpattern_sptrr);
			}
			graph.SettingAddedEdgeNames( spanningTreeReverse_addedEdgeNames );
			return;
		}
		private static string[] spanningTreeReverse_addedNodeNames = new string[] {  };
		private static string[] spanningTreeReverse_addedEdgeNames = new string[] {  };

		static Rule_spanningTreeReverse() {
		}

		public interface IMatch_spanningTreeReverse : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @sptrr { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
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
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
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
			public GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
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
			public object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public @Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @sptrr { get { return @_sptrr; } }
			public @Pattern_SpanningTreeReverse.Match_SpanningTreeReverse @_sptrr;
			public enum spanningTreeReverse_SubNums { @sptrr, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)spanningTreeReverse_SubNums.@sptrr: return _sptrr;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "sptrr": return _sptrr;
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
			public GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
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
			public GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_spanningTreeReverse.instance.pat_spanningTreeReverse; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_spanningTreeReverse(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_spanningTreeReverse(Match_spanningTreeReverse that)
			{
				_node_root = that._node_root;
				@_sptrr = that.@_sptrr;
			}
			public Match_spanningTreeReverse()
			{
			}
		}

	}

	public class Rule_spanningTreeOutgoing : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_spanningTreeOutgoing instance = null;
		public static Rule_spanningTreeOutgoing Instance { get { if (instance==null) { instance = new Rule_spanningTreeOutgoing(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] spanningTreeOutgoing_node_root_AllowedTypes = null;
		public static bool[] spanningTreeOutgoing_node_root_IsAllowedType = null;
		public enum spanningTreeOutgoing_NodeNums { @root, };
		public enum spanningTreeOutgoing_EdgeNums { };
		public enum spanningTreeOutgoing_VariableNums { };
		public enum spanningTreeOutgoing_SubNums { @_sub0, };
		public enum spanningTreeOutgoing_AltNums { };
		public enum spanningTreeOutgoing_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_spanningTreeOutgoing;


		private Rule_spanningTreeOutgoing()
		{
			name = "spanningTreeOutgoing";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "spanningTreeOutgoing_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] spanningTreeOutgoing_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] spanningTreeOutgoing_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] spanningTreeOutgoing_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] spanningTreeOutgoing_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode spanningTreeOutgoing_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "spanningTreeOutgoing_node_root", "root", spanningTreeOutgoing_node_root_AllowedTypes, spanningTreeOutgoing_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding spanningTreeOutgoing__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_SpanningTreeOutgoing.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("spanningTreeOutgoing_node_root"),
				}, 
				new string[] { }, new string[] { "spanningTreeOutgoing_node_root" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_spanningTreeOutgoing = new GRGEN_LGSP.PatternGraph(
				"spanningTreeOutgoing",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { spanningTreeOutgoing_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { spanningTreeOutgoing__sub0 }, 
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
				spanningTreeOutgoing_isNodeHomomorphicGlobal,
				spanningTreeOutgoing_isEdgeHomomorphicGlobal,
				spanningTreeOutgoing_isNodeTotallyHomomorphic,
				spanningTreeOutgoing_isEdgeTotallyHomomorphic
			);

			spanningTreeOutgoing_node_root.pointOfDefinition = null;
			spanningTreeOutgoing__sub0.PointOfDefinition = pat_spanningTreeOutgoing;

			patternGraph = pat_spanningTreeOutgoing;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_spanningTreeOutgoing curMatch = (Match_spanningTreeOutgoing)_curMatch;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_spanningTreeOutgoing() {
		}

		public interface IMatch_spanningTreeOutgoing : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_spanningTreeOutgoing : GRGEN_LGSP.ListElement<Match_spanningTreeOutgoing>, IMatch_spanningTreeOutgoing
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum spanningTreeOutgoing_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)spanningTreeOutgoing_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
				default: return null;
				}
			}
			
			public enum spanningTreeOutgoing_EdgeNums { END_OF_ENUM };
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
			
			public enum spanningTreeOutgoing_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing @_sub0 { get { return @__sub0; } }
			public @Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing @__sub0;
			public enum spanningTreeOutgoing_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)spanningTreeOutgoing_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "_sub0": return __sub0;
				default: return null;
				}
			}
			
			public enum spanningTreeOutgoing_AltNums { END_OF_ENUM };
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
			
			public enum spanningTreeOutgoing_IterNums { END_OF_ENUM };
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
			
			public enum spanningTreeOutgoing_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_spanningTreeOutgoing.instance.pat_spanningTreeOutgoing; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_spanningTreeOutgoing(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_spanningTreeOutgoing(Match_spanningTreeOutgoing that)
			{
				_node_root = that._node_root;
				@__sub0 = that.@__sub0;
			}
			public Match_spanningTreeOutgoing()
			{
			}
		}

	}

	public class Rule_spanningTreeIncoming : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_spanningTreeIncoming instance = null;
		public static Rule_spanningTreeIncoming Instance { get { if (instance==null) { instance = new Rule_spanningTreeIncoming(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] spanningTreeIncoming_node_root_AllowedTypes = null;
		public static bool[] spanningTreeIncoming_node_root_IsAllowedType = null;
		public enum spanningTreeIncoming_NodeNums { @root, };
		public enum spanningTreeIncoming_EdgeNums { };
		public enum spanningTreeIncoming_VariableNums { };
		public enum spanningTreeIncoming_SubNums { @_sub0, };
		public enum spanningTreeIncoming_AltNums { };
		public enum spanningTreeIncoming_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_spanningTreeIncoming;


		private Rule_spanningTreeIncoming()
		{
			name = "spanningTreeIncoming";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Node.typeVar, };
			inputNames = new string[] { "spanningTreeIncoming_node_root", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
			filters = new String[] { };

		}
		private void initialize()
		{
			bool[,] spanningTreeIncoming_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] spanningTreeIncoming_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] spanningTreeIncoming_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] spanningTreeIncoming_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode spanningTreeIncoming_node_root = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Node, GRGEN_MODEL.NodeType_Node.typeVar, "GRGEN_LIBGR.INode", "spanningTreeIncoming_node_root", "root", spanningTreeIncoming_node_root_AllowedTypes, spanningTreeIncoming_node_root_IsAllowedType, 5.5F, 0, false, null, null, null, false,null);
			GRGEN_LGSP.PatternGraphEmbedding spanningTreeIncoming__sub0 = new GRGEN_LGSP.PatternGraphEmbedding("_sub0", Pattern_SpanningTreeIncoming.Instance, 
				new GRGEN_EXPR.Expression[] {
					new GRGEN_EXPR.GraphEntityExpression("spanningTreeIncoming_node_root"),
				}, 
				new string[] { }, new string[] { "spanningTreeIncoming_node_root" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_spanningTreeIncoming = new GRGEN_LGSP.PatternGraph(
				"spanningTreeIncoming",
				"",
				false, false,
				new GRGEN_LGSP.PatternNode[] { spanningTreeIncoming_node_root }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] { spanningTreeIncoming__sub0 }, 
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
				spanningTreeIncoming_isNodeHomomorphicGlobal,
				spanningTreeIncoming_isEdgeHomomorphicGlobal,
				spanningTreeIncoming_isNodeTotallyHomomorphic,
				spanningTreeIncoming_isEdgeTotallyHomomorphic
			);

			spanningTreeIncoming_node_root.pointOfDefinition = null;
			spanningTreeIncoming__sub0.PointOfDefinition = pat_spanningTreeIncoming;

			patternGraph = pat_spanningTreeIncoming;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_spanningTreeIncoming curMatch = (Match_spanningTreeIncoming)_curMatch;
			GRGEN_LGSP.LGSPNode node_root = curMatch._node_root;
			Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming subpattern__sub0 = curMatch.@__sub0;
			return;
		}

		static Rule_spanningTreeIncoming() {
		}

		public interface IMatch_spanningTreeIncoming : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_LIBGR.INode node_root { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			@Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming @_sub0 { get; }
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_spanningTreeIncoming : GRGEN_LGSP.ListElement<Match_spanningTreeIncoming>, IMatch_spanningTreeIncoming
		{
			public GRGEN_LIBGR.INode node_root { get { return (GRGEN_LIBGR.INode)_node_root; } }
			public GRGEN_LGSP.LGSPNode _node_root;
			public enum spanningTreeIncoming_NodeNums { @root, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)spanningTreeIncoming_NodeNums.@root: return _node_root;
				default: return null;
				}
			}
			public GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "root": return _node_root;
				default: return null;
				}
			}
			
			public enum spanningTreeIncoming_EdgeNums { END_OF_ENUM };
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
			
			public enum spanningTreeIncoming_VariableNums { END_OF_ENUM };
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
			
			public @Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming @_sub0 { get { return @__sub0; } }
			public @Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming @__sub0;
			public enum spanningTreeIncoming_SubNums { @_sub0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 1;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				case (int)spanningTreeIncoming_SubNums.@_sub0: return __sub0;
				default: return null;
				}
			}
			public GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				case "_sub0": return __sub0;
				default: return null;
				}
			}
			
			public enum spanningTreeIncoming_AltNums { END_OF_ENUM };
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
			
			public enum spanningTreeIncoming_IterNums { END_OF_ENUM };
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
			
			public enum spanningTreeIncoming_IdptNums { END_OF_ENUM };
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
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_spanningTreeIncoming.instance.pat_spanningTreeIncoming; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_spanningTreeIncoming(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
			public bool _flag;
			public void Mark(bool flag) { _flag = flag; }
			public bool IsMarked() { return _flag; }

			public Match_spanningTreeIncoming(Match_spanningTreeIncoming that)
			{
				_node_root = that._node_root;
				@__sub0 = that.@__sub0;
			}
			public Match_spanningTreeIncoming()
			{
			}
		}

	}

	public class Functions
	{
	}

	public class Procedures
	{
	}

	public class spanningTree_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public spanningTree_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[4];
			rules = new GRGEN_LGSP.LGSPRulePattern[9];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[4+9];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			functions = new GRGEN_LIBGR.FunctionInfo[0];
			procedures = new GRGEN_LIBGR.ProcedureInfo[0];
			subpatterns[0] = Pattern_SpanningTree.Instance;
			rulesAndSubpatterns[0] = Pattern_SpanningTree.Instance;
			subpatterns[1] = Pattern_SpanningTreeReverse.Instance;
			rulesAndSubpatterns[1] = Pattern_SpanningTreeReverse.Instance;
			subpatterns[2] = Pattern_SpanningTreeOutgoing.Instance;
			rulesAndSubpatterns[2] = Pattern_SpanningTreeOutgoing.Instance;
			subpatterns[3] = Pattern_SpanningTreeIncoming.Instance;
			rulesAndSubpatterns[3] = Pattern_SpanningTreeIncoming.Instance;
			rules[0] = Rule_initTree.Instance;
			rulesAndSubpatterns[4+0] = Rule_initTree.Instance;
			rules[1] = Rule_initUndirected.Instance;
			rulesAndSubpatterns[4+1] = Rule_initUndirected.Instance;
			rules[2] = Rule_initDirected.Instance;
			rulesAndSubpatterns[4+2] = Rule_initDirected.Instance;
			rules[3] = Rule_spanningTree.Instance;
			rulesAndSubpatterns[4+3] = Rule_spanningTree.Instance;
			rules[4] = Rule_edgesVisited.Instance;
			rulesAndSubpatterns[4+4] = Rule_edgesVisited.Instance;
			rules[5] = Rule_nodesVisited.Instance;
			rulesAndSubpatterns[4+5] = Rule_nodesVisited.Instance;
			rules[6] = Rule_spanningTreeReverse.Instance;
			rulesAndSubpatterns[4+6] = Rule_spanningTreeReverse.Instance;
			rules[7] = Rule_spanningTreeOutgoing.Instance;
			rulesAndSubpatterns[4+7] = Rule_spanningTreeOutgoing.Instance;
			rules[8] = Rule_spanningTreeIncoming.Instance;
			rulesAndSubpatterns[4+8] = Rule_spanningTreeIncoming.Instance;
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
	}


    public class PatternAction_SpanningTree : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_SpanningTree(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTree.Instance.patternGraph;
        }

        public static PatternAction_SpanningTree getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_SpanningTree newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_SpanningTree(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_SpanningTree oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
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
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTree_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root = SpanningTree_node_root;
            // Push iterated matching task for SpanningTree_iter_0
            IteratedAction_SpanningTree_iter_0 taskFor_iter_0 = IteratedAction_SpanningTree_iter_0.getNewTask(actionEnv, openTasks);
            taskFor_iter_0.SpanningTree_node_root = candidate_SpanningTree_node_root;
            taskFor_iter_0.searchPatternpath = false;
            taskFor_iter_0.matchOfNestingPattern = null;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = null;
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
                        Pattern_SpanningTree.Match_SpanningTree_iter_0 cfpm = (Pattern_SpanningTree.Match_SpanningTree_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
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
        private IteratedAction_SpanningTree_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTree.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTree_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTree_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTree_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTree_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
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
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTree_node_root 
                GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root = SpanningTree_node_root;
                // both directions of SpanningTree_iter_0_edge_e
                for(int directionRunCounterOf_SpanningTree_iter_0_edge_e = 0; directionRunCounterOf_SpanningTree_iter_0_edge_e < 2; ++directionRunCounterOf_SpanningTree_iter_0_edge_e)
                {
                    // Extend IncomingOrOutgoing SpanningTree_iter_0_edge_e from SpanningTree_node_root 
                    GRGEN_LGSP.LGSPEdge head_candidate_SpanningTree_iter_0_edge_e = directionRunCounterOf_SpanningTree_iter_0_edge_e==0 ? candidate_SpanningTree_node_root.lgspInhead : candidate_SpanningTree_node_root.lgspOuthead;
                    if(head_candidate_SpanningTree_iter_0_edge_e != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_SpanningTree_iter_0_edge_e = head_candidate_SpanningTree_iter_0_edge_e;
                        do
                        {
                            if(candidate_SpanningTree_iter_0_edge_e.lgspType.TypeID!=2) {
                                continue;
                            }
                            if((candidate_SpanningTree_iter_0_edge_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // Implicit TheOther SpanningTree_iter_0_node_next from SpanningTree_iter_0_edge_e 
                            GRGEN_LGSP.LGSPNode candidate_SpanningTree_iter_0_node_next = candidate_SpanningTree_node_root==candidate_SpanningTree_iter_0_edge_e.lgspSource ? candidate_SpanningTree_iter_0_edge_e.lgspTarget : candidate_SpanningTree_iter_0_edge_e.lgspSource;
                            if((candidate_SpanningTree_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            if((candidate_SpanningTree_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // Element SpanningTree_node_root_inlined_sptr_0 assigned from other element SpanningTree_iter_0_node_next 
                            GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root_inlined_sptr_0 = candidate_SpanningTree_iter_0_node_next;
                            // accept iterated instance match
                            ++numMatchesIter;
                            // Push iterated matching task for SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0
                            IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 taskFor_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 = IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0.getNewTask(actionEnv, openTasks);
                            taskFor_iter_0_inlined_sptr_0_in_SpanningTree_iter_0.SpanningTree_node_root_inlined_sptr_0 = candidate_SpanningTree_node_root_inlined_sptr_0;
                            taskFor_iter_0_inlined_sptr_0_in_SpanningTree_iter_0.searchPatternpath = false;
                            taskFor_iter_0_inlined_sptr_0_in_SpanningTree_iter_0.matchOfNestingPattern = null;
                            taskFor_iter_0_inlined_sptr_0_in_SpanningTree_iter_0.lastMatchAtPreviousNestingLevel = null;
                            openTasks.Push(taskFor_iter_0_inlined_sptr_0_in_SpanningTree_iter_0);
                            uint prevGlobal__candidate_SpanningTree_iter_0_node_next;
                            prevGlobal__candidate_SpanningTree_iter_0_node_next = candidate_SpanningTree_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            uint prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                            prevGlobal__candidate_SpanningTree_iter_0_edge_e = candidate_SpanningTree_iter_0_edge_e.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_edge_e.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            // Match subpatterns 
                            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                            // Pop iterated matching task for SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0
                            openTasks.Pop();
                            IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0.releaseTask(taskFor_iter_0_inlined_sptr_0_in_SpanningTree_iter_0);
                            // Check whether subpatterns were found 
                            if(matchesList.Count>0) {
                                patternFound = true;
                                // subpatterns/alternatives were found, extend the partial matches by our local match object
                                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                {
                                    Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                                    Pattern_SpanningTree.Match_SpanningTree match_sptr = new Pattern_SpanningTree.Match_SpanningTree();
                                    match_sptr.SetMatchOfEnclosingPattern(match);
                                    match._node_root = candidate_SpanningTree_node_root;
                                    match._node_next = candidate_SpanningTree_iter_0_node_next;
                                    match_sptr._node_root = candidate_SpanningTree_node_root_inlined_sptr_0;
                                    match._edge_e = candidate_SpanningTree_iter_0_edge_e;
                                    match._sptr = match_sptr;
                                    match_sptr._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTree.Match_SpanningTree_iter_0, Pattern_SpanningTree.IMatch_SpanningTree_iter_0>(null);
                                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTree.IMatch_SpanningTree_iter_0) {
                                        Pattern_SpanningTree.Match_SpanningTree_iter_0 cfpm = (Pattern_SpanningTree.Match_SpanningTree_iter_0)currentFoundPartialMatch.Pop();
                                        if(cfpm.IsNullMatch) break;
                                        cfpm.SetMatchOfEnclosingPattern(match_sptr);
                                        match_sptr._iter_0.Add(cfpm);
                                    }
                                    currentFoundPartialMatch.Push(match);
                                }
                                // if enough matches were found, we leave
                                if(true) // as soon as there's a match, it's enough for iterated
                                {
                                    candidate_SpanningTree_iter_0_edge_e.lgspFlags = candidate_SpanningTree_iter_0_edge_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                                    candidate_SpanningTree_iter_0_node_next.lgspFlags = candidate_SpanningTree_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next;
                                    --numMatchesIter;
                                    goto maxMatchesIterReached;
                                }
                                candidate_SpanningTree_iter_0_edge_e.lgspFlags = candidate_SpanningTree_iter_0_edge_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                                candidate_SpanningTree_iter_0_node_next.lgspFlags = candidate_SpanningTree_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next;
                                --numMatchesIter;
                                continue;
                            }
                            candidate_SpanningTree_iter_0_node_next.lgspFlags = candidate_SpanningTree_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next;
                            candidate_SpanningTree_iter_0_edge_e.lgspFlags = candidate_SpanningTree_iter_0_edge_e.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e;
                            --numMatchesIter;
                        }
                        while( (directionRunCounterOf_SpanningTree_iter_0_edge_e==0 ? candidate_SpanningTree_iter_0_edge_e = candidate_SpanningTree_iter_0_edge_e.lgspInNext : candidate_SpanningTree_iter_0_edge_e = candidate_SpanningTree_iter_0_edge_e.lgspOutNext) != head_candidate_SpanningTree_iter_0_edge_e );
                    }
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTree.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTree_iter_0_inlined_sptr_0_in_SpanningTree_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTree_node_root_inlined_sptr_0;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTree_node_root_inlined_sptr_0 
                GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root_inlined_sptr_0 = SpanningTree_node_root_inlined_sptr_0;
                // both directions of SpanningTree_iter_0_edge_e_inlined_sptr_0
                for(int directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_0 = 0; directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_0 < 2; ++directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_0)
                {
                    // Extend IncomingOrOutgoing SpanningTree_iter_0_edge_e_inlined_sptr_0 from SpanningTree_node_root_inlined_sptr_0 
                    GRGEN_LGSP.LGSPEdge head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0 = directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_0==0 ? candidate_SpanningTree_node_root_inlined_sptr_0.lgspInhead : candidate_SpanningTree_node_root_inlined_sptr_0.lgspOuthead;
                    if(head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0 = head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0;
                        do
                        {
                            if(candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspType.TypeID!=2) {
                                continue;
                            }
                            if((candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // Implicit TheOther SpanningTree_iter_0_node_next_inlined_sptr_0 from SpanningTree_iter_0_edge_e_inlined_sptr_0 
                            GRGEN_LGSP.LGSPNode candidate_SpanningTree_iter_0_node_next_inlined_sptr_0 = candidate_SpanningTree_node_root_inlined_sptr_0==candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspSource ? candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspTarget : candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspSource;
                            if((candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            if((candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // accept iterated instance match
                            ++numMatchesIter;
                            // Push subpattern matching task for sptr_inlined_sptr_0
                            PatternAction_SpanningTree taskFor_sptr_inlined_sptr_0 = PatternAction_SpanningTree.getNewTask(actionEnv, openTasks);
                            taskFor_sptr_inlined_sptr_0.SpanningTree_node_root = candidate_SpanningTree_iter_0_node_next_inlined_sptr_0;
                            taskFor_sptr_inlined_sptr_0.searchPatternpath = false;
                            taskFor_sptr_inlined_sptr_0.matchOfNestingPattern = null;
                            taskFor_sptr_inlined_sptr_0.lastMatchAtPreviousNestingLevel = null;
                            openTasks.Push(taskFor_sptr_inlined_sptr_0);
                            uint prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_0;
                            prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_0 = candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            uint prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0;
                            prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0 = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            // Match subpatterns 
                            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                            // Pop subpattern matching task for sptr_inlined_sptr_0
                            openTasks.Pop();
                            PatternAction_SpanningTree.releaseTask(taskFor_sptr_inlined_sptr_0);
                            // Check whether subpatterns were found 
                            if(matchesList.Count>0) {
                                patternFound = true;
                                // subpatterns/alternatives were found, extend the partial matches by our local match object
                                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                {
                                    Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                                    match._node_root = candidate_SpanningTree_node_root_inlined_sptr_0;
                                    match._node_next = candidate_SpanningTree_iter_0_node_next_inlined_sptr_0;
                                    match._edge_e = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0;
                                    match._sptr = (@Pattern_SpanningTree.Match_SpanningTree)currentFoundPartialMatch.Pop();
                                    match._sptr._matchOfEnclosingPattern = match;
                                    currentFoundPartialMatch.Push(match);
                                }
                                // if enough matches were found, we leave
                                if(true) // as soon as there's a match, it's enough for iterated
                                {
                                    candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0;
                                    candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags = candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_0;
                                    --numMatchesIter;
                                    goto maxMatchesIterReached;
                                }
                                candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0;
                                candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags = candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_0;
                                --numMatchesIter;
                                continue;
                            }
                            candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags = candidate_SpanningTree_iter_0_node_next_inlined_sptr_0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_0;
                            candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0;
                            --numMatchesIter;
                        }
                        while( (directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_0==0 ? candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0 = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspInNext : candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0 = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0.lgspOutNext) != head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_0 );
                    }
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_SpanningTreeReverse : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_SpanningTreeReverse(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeReverse.Instance.patternGraph;
        }

        public static PatternAction_SpanningTreeReverse getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_SpanningTreeReverse newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_SpanningTreeReverse(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_SpanningTreeReverse oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
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
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTreeReverse_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root = SpanningTreeReverse_node_root;
            // Push iterated matching task for SpanningTreeReverse_iter_0
            IteratedAction_SpanningTreeReverse_iter_0 taskFor_iter_0 = IteratedAction_SpanningTreeReverse_iter_0.getNewTask(actionEnv, openTasks);
            taskFor_iter_0.SpanningTreeReverse_node_root = candidate_SpanningTreeReverse_node_root;
            taskFor_iter_0.searchPatternpath = false;
            taskFor_iter_0.matchOfNestingPattern = null;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = null;
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
                        Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 cfpm = (Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
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
        private IteratedAction_SpanningTreeReverse_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeReverse.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeReverse_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeReverse_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeReverse_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeReverse_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
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
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeReverse_node_root 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root = SpanningTreeReverse_node_root;
                // Extend Outgoing SpanningTreeReverse_iter_0_edge__edge0 from SpanningTreeReverse_node_root 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeReverse_iter_0_edge__edge0 = candidate_SpanningTreeReverse_node_root.lgspOuthead;
                if(head_candidate_SpanningTreeReverse_iter_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeReverse_iter_0_edge__edge0 = head_candidate_SpanningTreeReverse_iter_0_edge__edge0;
                    do
                    {
                        if(candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target SpanningTreeReverse_iter_0_node_next from SpanningTreeReverse_iter_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_iter_0_node_next = candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspTarget;
                        if((candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Element SpanningTreeReverse_node_root_inlined_sptrr_1 assigned from other element SpanningTreeReverse_iter_0_node_next 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root_inlined_sptrr_1 = candidate_SpanningTreeReverse_iter_0_node_next;
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push iterated matching task for SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0
                        IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 taskFor_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 = IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0.getNewTask(actionEnv, openTasks);
                        taskFor_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0.SpanningTreeReverse_node_root_inlined_sptrr_1 = candidate_SpanningTreeReverse_node_root_inlined_sptrr_1;
                        taskFor_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0.searchPatternpath = false;
                        taskFor_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0.matchOfNestingPattern = null;
                        taskFor_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0);
                        uint prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next = candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop iterated matching task for SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0
                        openTasks.Pop();
                        IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0.releaseTask(taskFor_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                                Pattern_SpanningTreeReverse.Match_SpanningTreeReverse match_sptrr = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse();
                                match_sptrr.SetMatchOfEnclosingPattern(match);
                                match._node_root = candidate_SpanningTreeReverse_node_root;
                                match._node_next = candidate_SpanningTreeReverse_iter_0_node_next;
                                match_sptrr._node_root = candidate_SpanningTreeReverse_node_root_inlined_sptrr_1;
                                match._edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0;
                                match._sptrr = match_sptrr;
                                match_sptrr._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0, Pattern_SpanningTreeReverse.IMatch_SpanningTreeReverse_iter_0>(null);
                                while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeReverse.IMatch_SpanningTreeReverse_iter_0) {
                                    Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 cfpm = (Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0)currentFoundPartialMatch.Pop();
                                    if(cfpm.IsNullMatch) break;
                                    cfpm.SetMatchOfEnclosingPattern(match_sptrr);
                                    match_sptrr._iter_0.Add(cfpm);
                                }
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                                candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                            candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next;
                        candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeReverse_iter_0_edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0.lgspOutNext) != head_candidate_SpanningTreeReverse_iter_0_edge__edge0 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeReverse.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_1_in_SpanningTreeReverse_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeReverse_node_root_inlined_sptrr_1;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeReverse_node_root_inlined_sptrr_1 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root_inlined_sptrr_1 = SpanningTreeReverse_node_root_inlined_sptrr_1;
                // Extend Outgoing SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 from SpanningTreeReverse_node_root_inlined_sptrr_1 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 = candidate_SpanningTreeReverse_node_root_inlined_sptrr_1.lgspOuthead;
                if(head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 = head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1;
                    do
                    {
                        if(candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1 from SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspTarget;
                        if((candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push subpattern matching task for sptrr_inlined_sptrr_1
                        PatternAction_SpanningTreeReverse taskFor_sptrr_inlined_sptrr_1 = PatternAction_SpanningTreeReverse.getNewTask(actionEnv, openTasks);
                        taskFor_sptrr_inlined_sptrr_1.SpanningTreeReverse_node_root = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1;
                        taskFor_sptrr_inlined_sptrr_1.searchPatternpath = false;
                        taskFor_sptrr_inlined_sptrr_1.matchOfNestingPattern = null;
                        taskFor_sptrr_inlined_sptrr_1.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_sptrr_inlined_sptrr_1);
                        uint prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1;
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1 = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1;
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for sptrr_inlined_sptrr_1
                        openTasks.Pop();
                        PatternAction_SpanningTreeReverse.releaseTask(taskFor_sptrr_inlined_sptrr_1);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                                match._node_root = candidate_SpanningTreeReverse_node_root_inlined_sptrr_1;
                                match._node_next = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1;
                                match._edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1;
                                match._sptrr = (@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse)currentFoundPartialMatch.Pop();
                                match._sptrr._matchOfEnclosingPattern = match;
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1;
                                candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1;
                            candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_1;
                        candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1.lgspOutNext) != head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_1 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_SpanningTreeOutgoing : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_SpanningTreeOutgoing(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeOutgoing.Instance.patternGraph;
        }

        public static PatternAction_SpanningTreeOutgoing getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_SpanningTreeOutgoing newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_SpanningTreeOutgoing(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_SpanningTreeOutgoing oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_SpanningTreeOutgoing freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_SpanningTreeOutgoing next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeOutgoing_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTreeOutgoing_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_node_root = SpanningTreeOutgoing_node_root;
            // Push iterated matching task for SpanningTreeOutgoing_iter_0
            IteratedAction_SpanningTreeOutgoing_iter_0 taskFor_iter_0 = IteratedAction_SpanningTreeOutgoing_iter_0.getNewTask(actionEnv, openTasks);
            taskFor_iter_0.SpanningTreeOutgoing_node_root = candidate_SpanningTreeOutgoing_node_root;
            taskFor_iter_0.searchPatternpath = false;
            taskFor_iter_0.matchOfNestingPattern = null;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTreeOutgoing_iter_0
            openTasks.Pop();
            IteratedAction_SpanningTreeOutgoing_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing();
                    match._node_root = candidate_SpanningTreeOutgoing_node_root;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0, Pattern_SpanningTreeOutgoing.IMatch_SpanningTreeOutgoing_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeOutgoing.IMatch_SpanningTreeOutgoing_iter_0) {
                        Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 cfpm = (Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
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

    public class IteratedAction_SpanningTreeOutgoing_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeOutgoing_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeOutgoing.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeOutgoing_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeOutgoing_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeOutgoing_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeOutgoing_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeOutgoing_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeOutgoing_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeOutgoing_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeOutgoing_node_root 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_node_root = SpanningTreeOutgoing_node_root;
                // Extend Outgoing SpanningTreeOutgoing_iter_0_edge__edge0 from SpanningTreeOutgoing_node_root 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0 = candidate_SpanningTreeOutgoing_node_root.lgspOuthead;
                if(head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeOutgoing_iter_0_edge__edge0 = head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0;
                    do
                    {
                        if(candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target SpanningTreeOutgoing_iter_0_node_next from SpanningTreeOutgoing_iter_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_iter_0_node_next = candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspTarget;
                        if((candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Element SpanningTreeOutgoing_node_root_inlined__sub0_2 assigned from other element SpanningTreeOutgoing_iter_0_node_next 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_node_root_inlined__sub0_2 = candidate_SpanningTreeOutgoing_iter_0_node_next;
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push iterated matching task for SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0
                        IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 taskFor_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 = IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0.getNewTask(actionEnv, openTasks);
                        taskFor_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0.SpanningTreeOutgoing_node_root_inlined__sub0_2 = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_2;
                        taskFor_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0.searchPatternpath = false;
                        taskFor_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0.matchOfNestingPattern = null;
                        taskFor_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0);
                        uint prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next;
                        prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next = candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0;
                        prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop iterated matching task for SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0
                        openTasks.Pop();
                        IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0.releaseTask(taskFor_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                                Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing match__sub0 = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing();
                                match__sub0.SetMatchOfEnclosingPattern(match);
                                match._node_root = candidate_SpanningTreeOutgoing_node_root;
                                match._node_next = candidate_SpanningTreeOutgoing_iter_0_node_next;
                                match__sub0._node_root = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_2;
                                match._edge__edge0 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0;
                                match.__sub0 = match__sub0;
                                match__sub0._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0, Pattern_SpanningTreeOutgoing.IMatch_SpanningTreeOutgoing_iter_0>(null);
                                while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeOutgoing.IMatch_SpanningTreeOutgoing_iter_0) {
                                    Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 cfpm = (Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0)currentFoundPartialMatch.Pop();
                                    if(cfpm.IsNullMatch) break;
                                    cfpm.SetMatchOfEnclosingPattern(match__sub0);
                                    match__sub0._iter_0.Add(cfpm);
                                }
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0;
                                candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0;
                            candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next;
                        candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeOutgoing_iter_0_edge__edge0 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0.lgspOutNext) != head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeOutgoing.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_2_in_SpanningTreeOutgoing_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeOutgoing_node_root_inlined__sub0_2;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeOutgoing_node_root_inlined__sub0_2 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_node_root_inlined__sub0_2 = SpanningTreeOutgoing_node_root_inlined__sub0_2;
                // Extend Outgoing SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 from SpanningTreeOutgoing_node_root_inlined__sub0_2 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_2.lgspOuthead;
                if(head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 = head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2;
                    do
                    {
                        if(candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2 from SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspTarget;
                        if((candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push subpattern matching task for _sub0_inlined__sub0_2
                        PatternAction_SpanningTreeOutgoing taskFor__sub0_inlined__sub0_2 = PatternAction_SpanningTreeOutgoing.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_2.SpanningTreeOutgoing_node_root = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2;
                        taskFor__sub0_inlined__sub0_2.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_2.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_2.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_2);
                        uint prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2;
                        prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2 = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2;
                        prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0_inlined__sub0_2
                        openTasks.Pop();
                        PatternAction_SpanningTreeOutgoing.releaseTask(taskFor__sub0_inlined__sub0_2);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                                match._node_root = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_2;
                                match._node_next = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2;
                                match._edge__edge0 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2;
                                match.__sub0 = (@Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2;
                                candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2;
                            candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_2;
                        candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2.lgspOutNext) != head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_2 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class PatternAction_SpanningTreeIncoming : GRGEN_LGSP.LGSPSubpatternAction
    {
        private PatternAction_SpanningTreeIncoming(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeIncoming.Instance.patternGraph;
        }

        public static PatternAction_SpanningTreeIncoming getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            PatternAction_SpanningTreeIncoming newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_SpanningTreeIncoming(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(PatternAction_SpanningTreeIncoming oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_SpanningTreeIncoming freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_SpanningTreeIncoming next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeIncoming_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            openTasks.Pop();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset SpanningTreeIncoming_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_node_root = SpanningTreeIncoming_node_root;
            // Push iterated matching task for SpanningTreeIncoming_iter_0
            IteratedAction_SpanningTreeIncoming_iter_0 taskFor_iter_0 = IteratedAction_SpanningTreeIncoming_iter_0.getNewTask(actionEnv, openTasks);
            taskFor_iter_0.SpanningTreeIncoming_node_root = candidate_SpanningTreeIncoming_node_root;
            taskFor_iter_0.searchPatternpath = false;
            taskFor_iter_0.matchOfNestingPattern = null;
            taskFor_iter_0.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_iter_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTreeIncoming_iter_0
            openTasks.Pop();
            IteratedAction_SpanningTreeIncoming_iter_0.releaseTask(taskFor_iter_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming();
                    match._node_root = candidate_SpanningTreeIncoming_node_root;
                    match._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0, Pattern_SpanningTreeIncoming.IMatch_SpanningTreeIncoming_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeIncoming.IMatch_SpanningTreeIncoming_iter_0) {
                        Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 cfpm = (Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match);
                        match._iter_0.Add(cfpm);
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

    public class IteratedAction_SpanningTreeIncoming_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeIncoming_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeIncoming.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeIncoming_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeIncoming_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeIncoming_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeIncoming_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeIncoming_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeIncoming_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeIncoming_node_root;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeIncoming_node_root 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_node_root = SpanningTreeIncoming_node_root;
                // Extend Incoming SpanningTreeIncoming_iter_0_edge__edge0 from SpanningTreeIncoming_node_root 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeIncoming_iter_0_edge__edge0 = candidate_SpanningTreeIncoming_node_root.lgspInhead;
                if(head_candidate_SpanningTreeIncoming_iter_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeIncoming_iter_0_edge__edge0 = head_candidate_SpanningTreeIncoming_iter_0_edge__edge0;
                    do
                    {
                        if(candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Source SpanningTreeIncoming_iter_0_node_next from SpanningTreeIncoming_iter_0_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_iter_0_node_next = candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspSource;
                        if((candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Element SpanningTreeIncoming_node_root_inlined__sub0_3 assigned from other element SpanningTreeIncoming_iter_0_node_next 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_node_root_inlined__sub0_3 = candidate_SpanningTreeIncoming_iter_0_node_next;
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push iterated matching task for SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0
                        IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 taskFor_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 = IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0.getNewTask(actionEnv, openTasks);
                        taskFor_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0.SpanningTreeIncoming_node_root_inlined__sub0_3 = candidate_SpanningTreeIncoming_node_root_inlined__sub0_3;
                        taskFor_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0.searchPatternpath = false;
                        taskFor_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0.matchOfNestingPattern = null;
                        taskFor_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0);
                        uint prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next;
                        prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next = candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0;
                        prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0 = candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop iterated matching task for SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0
                        openTasks.Pop();
                        IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0.releaseTask(taskFor_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                                Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming match__sub0 = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming();
                                match__sub0.SetMatchOfEnclosingPattern(match);
                                match._node_next = candidate_SpanningTreeIncoming_iter_0_node_next;
                                match._node_root = candidate_SpanningTreeIncoming_node_root;
                                match__sub0._node_root = candidate_SpanningTreeIncoming_node_root_inlined__sub0_3;
                                match._edge__edge0 = candidate_SpanningTreeIncoming_iter_0_edge__edge0;
                                match.__sub0 = match__sub0;
                                match__sub0._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0, Pattern_SpanningTreeIncoming.IMatch_SpanningTreeIncoming_iter_0>(null);
                                while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeIncoming.IMatch_SpanningTreeIncoming_iter_0) {
                                    Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 cfpm = (Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0)currentFoundPartialMatch.Pop();
                                    if(cfpm.IsNullMatch) break;
                                    cfpm.SetMatchOfEnclosingPattern(match__sub0);
                                    match__sub0._iter_0.Add(cfpm);
                                }
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0;
                                candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0;
                            candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next;
                        candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeIncoming_iter_0_edge__edge0 = candidate_SpanningTreeIncoming_iter_0_edge__edge0.lgspInNext) != head_candidate_SpanningTreeIncoming_iter_0_edge__edge0 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    public class IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Pattern_SpanningTreeIncoming.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_3_in_SpanningTreeIncoming_iter_0 next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeIncoming_node_root_inlined__sub0_3;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeIncoming_node_root_inlined__sub0_3 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_node_root_inlined__sub0_3 = SpanningTreeIncoming_node_root_inlined__sub0_3;
                // Extend Incoming SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 from SpanningTreeIncoming_node_root_inlined__sub0_3 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 = candidate_SpanningTreeIncoming_node_root_inlined__sub0_3.lgspInhead;
                if(head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 = head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3;
                    do
                    {
                        if(candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Source SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3 from SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspSource;
                        if((candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push subpattern matching task for _sub0_inlined__sub0_3
                        PatternAction_SpanningTreeIncoming taskFor__sub0_inlined__sub0_3 = PatternAction_SpanningTreeIncoming.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_3.SpanningTreeIncoming_node_root = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3;
                        taskFor__sub0_inlined__sub0_3.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_3.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_3.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_3);
                        uint prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3;
                        prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3 = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3;
                        prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0_inlined__sub0_3
                        openTasks.Pop();
                        PatternAction_SpanningTreeIncoming.releaseTask(taskFor__sub0_inlined__sub0_3);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                                match._node_next = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3;
                                match._node_root = candidate_SpanningTreeIncoming_node_root_inlined__sub0_3;
                                match._edge__edge0 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3;
                                match.__sub0 = (@Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3;
                                candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3;
                            candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_3;
                        candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3.lgspInNext) != head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_3 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_initTree
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_initTree.IMatch_initTree match, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> matches, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_initTree : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_initTree
    {
        public Action_initTree() {
            _rulePattern = Rule_initTree.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_initTree.Match_initTree, Rule_initTree.IMatch_initTree>(this);
        }

        public Rule_initTree _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "initTree"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_initTree.Match_initTree, Rule_initTree.IMatch_initTree> matches;

        public static Action_initTree Instance { get { return instance; } }
        private static Action_initTree instance = new Action_initTree();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Rule_initTree.Match_initTree match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_initTree.IMatch_initTree match, out GRGEN_LIBGR.INode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> matches, out GRGEN_LIBGR.INode output_0)
        {
            output_0 = null;
            foreach(Rule_initTree.IMatch_initTree match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_initTree.IMatch_initTree match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> matches;
            GRGEN_LIBGR.INode output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree> matches;
            GRGEN_LIBGR.INode output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
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
            GRGEN_LIBGR.INode output_0; 
            Modify(actionEnv, (Rule_initTree.IMatch_initTree)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_LIBGR.INode output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_initTree.IMatch_initTree>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_initUndirected
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_initUndirected.IMatch_initUndirected match, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matches, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_initUndirected : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_initUndirected
    {
        public Action_initUndirected() {
            _rulePattern = Rule_initUndirected.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_initUndirected.Match_initUndirected, Rule_initUndirected.IMatch_initUndirected>(this);
        }

        public Rule_initUndirected _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "initUndirected"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_initUndirected.Match_initUndirected, Rule_initUndirected.IMatch_initUndirected> matches;

        public static Action_initUndirected Instance { get { return instance; } }
        private static Action_initUndirected instance = new Action_initUndirected();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Rule_initUndirected.Match_initUndirected match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_initUndirected.IMatch_initUndirected match, out GRGEN_LIBGR.INode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matches, out GRGEN_LIBGR.INode output_0)
        {
            output_0 = null;
            foreach(Rule_initUndirected.IMatch_initUndirected match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_initUndirected.IMatch_initUndirected match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matches;
            GRGEN_LIBGR.INode output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected> matches;
            GRGEN_LIBGR.INode output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
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
            GRGEN_LIBGR.INode output_0; 
            Modify(actionEnv, (Rule_initUndirected.IMatch_initUndirected)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_LIBGR.INode output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_initUndirected.IMatch_initUndirected>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_initDirected
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_initDirected.IMatch_initDirected match, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> matches, out GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_initDirected : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_initDirected
    {
        public Action_initDirected() {
            _rulePattern = Rule_initDirected.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_initDirected.Match_initDirected, Rule_initDirected.IMatch_initDirected>(this);
        }

        public Rule_initDirected _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "initDirected"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_initDirected.Match_initDirected, Rule_initDirected.IMatch_initDirected> matches;

        public static Action_initDirected Instance { get { return instance; } }
        private static Action_initDirected instance = new Action_initDirected();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_initDirected.IMatch_initDirected match, out GRGEN_LIBGR.INode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> matches, out GRGEN_LIBGR.INode output_0)
        {
            output_0 = null;
            foreach(Rule_initDirected.IMatch_initDirected match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_LIBGR.INode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_initDirected.IMatch_initDirected match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> matches;
            GRGEN_LIBGR.INode output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_LIBGR.INode output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected> matches;
            GRGEN_LIBGR.INode output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
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
            GRGEN_LIBGR.INode output_0; 
            Modify(actionEnv, (Rule_initDirected.IMatch_initDirected)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_LIBGR.INode output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_initDirected.IMatch_initDirected>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_LIBGR.INode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_spanningTree
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTree_node_root);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTree.IMatch_spanningTree match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTree_node_root);
    }
    
    public class Action_spanningTree : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_spanningTree
    {
        public Action_spanningTree() {
            _rulePattern = Rule_spanningTree.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_spanningTree.Match_spanningTree, Rule_spanningTree.IMatch_spanningTree>(this);
        }

        public Rule_spanningTree _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "spanningTree"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_spanningTree.Match_spanningTree, Rule_spanningTree.IMatch_spanningTree> matches;

        public static Action_spanningTree Instance { get { return instance; } }
        private static Action_spanningTree instance = new Action_spanningTree();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTree_node_root)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset spanningTree_node_root 
            GRGEN_LGSP.LGSPNode candidate_spanningTree_node_root = (GRGEN_LGSP.LGSPNode)spanningTree_node_root;
            // Element SpanningTree_node_root_inlined_sptr_4 assigned from other element spanningTree_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root_inlined_sptr_4 = candidate_spanningTree_node_root;
            // Push iterated matching task for SpanningTree_iter_0_inlined_sptr_4_in_spanningTree
            IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree taskFor_iter_0_inlined_sptr_4_in_spanningTree = IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree.getNewTask(actionEnv, openTasks);
            taskFor_iter_0_inlined_sptr_4_in_spanningTree.SpanningTree_node_root_inlined_sptr_4 = candidate_SpanningTree_node_root_inlined_sptr_4;
            taskFor_iter_0_inlined_sptr_4_in_spanningTree.searchPatternpath = false;
            taskFor_iter_0_inlined_sptr_4_in_spanningTree.matchOfNestingPattern = null;
            taskFor_iter_0_inlined_sptr_4_in_spanningTree.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_iter_0_inlined_sptr_4_in_spanningTree);
            uint prevGlobal__candidate_spanningTree_node_root;
            prevGlobal__candidate_spanningTree_node_root = candidate_spanningTree_node_root.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_spanningTree_node_root.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTree_iter_0_inlined_sptr_4_in_spanningTree
            openTasks.Pop();
            IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree.releaseTask(taskFor_iter_0_inlined_sptr_4_in_spanningTree);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_spanningTree.Match_spanningTree match = matches.GetNextUnfilledPosition();
                    Pattern_SpanningTree.Match_SpanningTree match_sptr = new Pattern_SpanningTree.Match_SpanningTree();
                    match_sptr.SetMatchOfEnclosingPattern(match);
                    match._node_root = candidate_spanningTree_node_root;
                    match_sptr._node_root = candidate_SpanningTree_node_root_inlined_sptr_4;
                    match._sptr = match_sptr;
                    match_sptr._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTree.Match_SpanningTree_iter_0, Pattern_SpanningTree.IMatch_SpanningTree_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTree.IMatch_SpanningTree_iter_0) {
                        Pattern_SpanningTree.Match_SpanningTree_iter_0 cfpm = (Pattern_SpanningTree.Match_SpanningTree_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match_sptr);
                        match_sptr._iter_0.Add(cfpm);
                    }
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_spanningTree_node_root.lgspFlags = candidate_spanningTree_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
                    return matches;
                }
                candidate_spanningTree_node_root.lgspFlags = candidate_spanningTree_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
                return matches;
            }
            candidate_spanningTree_node_root.lgspFlags = candidate_spanningTree_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTree_node_root;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTree_node_root);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTree_node_root)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTree_node_root);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTree.IMatch_spanningTree match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matches)
        {
            foreach(Rule_spanningTree.IMatch_spanningTree match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTree_node_root);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTree_node_root);
            if(matches.Count <= 0) return false;
            foreach(Rule_spanningTree.IMatch_spanningTree match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTree_node_root);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTree_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTree_node_root);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTree_node_root);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTree_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTree_node_root);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_spanningTree.IMatch_spanningTree)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_spanningTree.IMatch_spanningTree>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    public class IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Rule_spanningTree.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTree_iter_0_inlined_sptr_4_in_spanningTree next = null;

        public GRGEN_LGSP.LGSPNode SpanningTree_node_root_inlined_sptr_4;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTree_node_root_inlined_sptr_4 
                GRGEN_LGSP.LGSPNode candidate_SpanningTree_node_root_inlined_sptr_4 = SpanningTree_node_root_inlined_sptr_4;
                // both directions of SpanningTree_iter_0_edge_e_inlined_sptr_4
                for(int directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_4 = 0; directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_4 < 2; ++directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_4)
                {
                    // Extend IncomingOrOutgoing SpanningTree_iter_0_edge_e_inlined_sptr_4 from SpanningTree_node_root_inlined_sptr_4 
                    GRGEN_LGSP.LGSPEdge head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4 = directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_4==0 ? candidate_SpanningTree_node_root_inlined_sptr_4.lgspInhead : candidate_SpanningTree_node_root_inlined_sptr_4.lgspOuthead;
                    if(head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4 = head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4;
                        do
                        {
                            if(candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspType.TypeID!=2) {
                                continue;
                            }
                            if((candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // Implicit TheOther SpanningTree_iter_0_node_next_inlined_sptr_4 from SpanningTree_iter_0_edge_e_inlined_sptr_4 
                            GRGEN_LGSP.LGSPNode candidate_SpanningTree_iter_0_node_next_inlined_sptr_4 = candidate_SpanningTree_node_root_inlined_sptr_4==candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspSource ? candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspTarget : candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspSource;
                            if((candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            if((candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                            {
                                continue;
                            }
                            // accept iterated instance match
                            ++numMatchesIter;
                            // Push subpattern matching task for sptr_inlined_sptr_4
                            PatternAction_SpanningTree taskFor_sptr_inlined_sptr_4 = PatternAction_SpanningTree.getNewTask(actionEnv, openTasks);
                            taskFor_sptr_inlined_sptr_4.SpanningTree_node_root = candidate_SpanningTree_iter_0_node_next_inlined_sptr_4;
                            taskFor_sptr_inlined_sptr_4.searchPatternpath = false;
                            taskFor_sptr_inlined_sptr_4.matchOfNestingPattern = null;
                            taskFor_sptr_inlined_sptr_4.lastMatchAtPreviousNestingLevel = null;
                            openTasks.Push(taskFor_sptr_inlined_sptr_4);
                            uint prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_4;
                            prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_4 = candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            uint prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4;
                            prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4 = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                            // Match subpatterns 
                            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                            // Pop subpattern matching task for sptr_inlined_sptr_4
                            openTasks.Pop();
                            PatternAction_SpanningTree.releaseTask(taskFor_sptr_inlined_sptr_4);
                            // Check whether subpatterns were found 
                            if(matchesList.Count>0) {
                                patternFound = true;
                                // subpatterns/alternatives were found, extend the partial matches by our local match object
                                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                                {
                                    Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                                    match._node_root = candidate_SpanningTree_node_root_inlined_sptr_4;
                                    match._node_next = candidate_SpanningTree_iter_0_node_next_inlined_sptr_4;
                                    match._edge_e = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4;
                                    match._sptr = (@Pattern_SpanningTree.Match_SpanningTree)currentFoundPartialMatch.Pop();
                                    match._sptr._matchOfEnclosingPattern = match;
                                    currentFoundPartialMatch.Push(match);
                                }
                                // if enough matches were found, we leave
                                if(true) // as soon as there's a match, it's enough for iterated
                                {
                                    candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4;
                                    candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags = candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_4;
                                    --numMatchesIter;
                                    goto maxMatchesIterReached;
                                }
                                candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4;
                                candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags = candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_4;
                                --numMatchesIter;
                                continue;
                            }
                            candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags = candidate_SpanningTree_iter_0_node_next_inlined_sptr_4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_node_next_inlined_sptr_4;
                            candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4;
                            --numMatchesIter;
                        }
                        while( (directionRunCounterOf_SpanningTree_iter_0_edge_e_inlined_sptr_4==0 ? candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4 = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspInNext : candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4 = candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4.lgspOutNext) != head_candidate_SpanningTree_iter_0_edge_e_inlined_sptr_4 );
                    }
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTree.Match_SpanningTree_iter_0 match = new Pattern_SpanningTree.Match_SpanningTree_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_edgesVisited
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_edgesVisited.IMatch_edgesVisited match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_edgesVisited : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_edgesVisited
    {
        public Action_edgesVisited() {
            _rulePattern = Rule_edgesVisited.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_edgesVisited.Match_edgesVisited, Rule_edgesVisited.IMatch_edgesVisited>(this);
        }

        public Rule_edgesVisited _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "edgesVisited"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_edgesVisited.Match_edgesVisited, Rule_edgesVisited.IMatch_edgesVisited> matches;

        public static Action_edgesVisited Instance { get { return instance; } }
        private static Action_edgesVisited instance = new Action_edgesVisited();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Lookup edgesVisited_edge_e 
            int type_id_candidate_edgesVisited_edge_e = 2;
            for(GRGEN_LGSP.LGSPEdge head_candidate_edgesVisited_edge_e = graph.edgesByTypeHeads[type_id_candidate_edgesVisited_edge_e], candidate_edgesVisited_edge_e = head_candidate_edgesVisited_edge_e.lgspTypeNext; candidate_edgesVisited_edge_e != head_candidate_edgesVisited_edge_e; candidate_edgesVisited_edge_e = candidate_edgesVisited_edge_e.lgspTypeNext)
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_edgesVisited.IMatch_edgesVisited match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> matches)
        {
            foreach(Rule_edgesVisited.IMatch_edgesVisited match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_edgesVisited.IMatch_edgesVisited match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited> matches;
            
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
            
            Modify(actionEnv, (Rule_edgesVisited.IMatch_edgesVisited)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_edgesVisited.IMatch_edgesVisited>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            
            if(ApplyAll(maxMatches, actionEnv)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv)) {
                return ReturnArray;
            }
            else return null;
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_nodesVisited
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_nodesVisited.IMatch_nodesVisited match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_nodesVisited : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_nodesVisited
    {
        public Action_nodesVisited() {
            _rulePattern = Rule_nodesVisited.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_nodesVisited.Match_nodesVisited, Rule_nodesVisited.IMatch_nodesVisited>(this);
        }

        public Rule_nodesVisited _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "nodesVisited"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_nodesVisited.Match_nodesVisited, Rule_nodesVisited.IMatch_nodesVisited> matches;

        public static Action_nodesVisited Instance { get { return instance; } }
        private static Action_nodesVisited instance = new Action_nodesVisited();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Lookup nodesVisited_node_n 
            int type_id_candidate_nodesVisited_node_n = 0;
            for(GRGEN_LGSP.LGSPNode head_candidate_nodesVisited_node_n = graph.nodesByTypeHeads[type_id_candidate_nodesVisited_node_n], candidate_nodesVisited_node_n = head_candidate_nodesVisited_node_n.lgspTypeNext; candidate_nodesVisited_node_n != head_candidate_nodesVisited_node_n; candidate_nodesVisited_node_n = candidate_nodesVisited_node_n.lgspTypeNext)
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
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_nodesVisited.IMatch_nodesVisited match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> matches)
        {
            foreach(Rule_nodesVisited.IMatch_nodesVisited match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_nodesVisited.IMatch_nodesVisited match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited> matches;
            
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
            
            Modify(actionEnv, (Rule_nodesVisited.IMatch_nodesVisited)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_nodesVisited.IMatch_nodesVisited>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            
            if(ApplyAll(maxMatches, actionEnv)) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv)) {
                return ReturnArray;
            }
            else return null;
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
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_spanningTreeReverse
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeReverse_node_root);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTreeReverse.IMatch_spanningTreeReverse match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTreeReverse_node_root);
    }
    
    public class Action_spanningTreeReverse : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_spanningTreeReverse
    {
        public Action_spanningTreeReverse() {
            _rulePattern = Rule_spanningTreeReverse.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeReverse.Match_spanningTreeReverse, Rule_spanningTreeReverse.IMatch_spanningTreeReverse>(this);
        }

        public Rule_spanningTreeReverse _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "spanningTreeReverse"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeReverse.Match_spanningTreeReverse, Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches;

        public static Action_spanningTreeReverse Instance { get { return instance; } }
        private static Action_spanningTreeReverse instance = new Action_spanningTreeReverse();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeReverse_node_root)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset spanningTreeReverse_node_root 
            GRGEN_LGSP.LGSPNode candidate_spanningTreeReverse_node_root = (GRGEN_LGSP.LGSPNode)spanningTreeReverse_node_root;
            // Element SpanningTreeReverse_node_root_inlined_sptrr_5 assigned from other element spanningTreeReverse_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root_inlined_sptrr_5 = candidate_spanningTreeReverse_node_root;
            // Push iterated matching task for SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse
            IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse taskFor_iter_0_inlined_sptrr_5_in_spanningTreeReverse = IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse.getNewTask(actionEnv, openTasks);
            taskFor_iter_0_inlined_sptrr_5_in_spanningTreeReverse.SpanningTreeReverse_node_root_inlined_sptrr_5 = candidate_SpanningTreeReverse_node_root_inlined_sptrr_5;
            taskFor_iter_0_inlined_sptrr_5_in_spanningTreeReverse.searchPatternpath = false;
            taskFor_iter_0_inlined_sptrr_5_in_spanningTreeReverse.matchOfNestingPattern = null;
            taskFor_iter_0_inlined_sptrr_5_in_spanningTreeReverse.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_iter_0_inlined_sptrr_5_in_spanningTreeReverse);
            uint prevGlobal__candidate_spanningTreeReverse_node_root;
            prevGlobal__candidate_spanningTreeReverse_node_root = candidate_spanningTreeReverse_node_root.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_spanningTreeReverse_node_root.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse
            openTasks.Pop();
            IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse.releaseTask(taskFor_iter_0_inlined_sptrr_5_in_spanningTreeReverse);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_spanningTreeReverse.Match_spanningTreeReverse match = matches.GetNextUnfilledPosition();
                    Pattern_SpanningTreeReverse.Match_SpanningTreeReverse match_sptrr = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse();
                    match_sptrr.SetMatchOfEnclosingPattern(match);
                    match._node_root = candidate_spanningTreeReverse_node_root;
                    match_sptrr._node_root = candidate_SpanningTreeReverse_node_root_inlined_sptrr_5;
                    match._sptrr = match_sptrr;
                    match_sptrr._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0, Pattern_SpanningTreeReverse.IMatch_SpanningTreeReverse_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeReverse.IMatch_SpanningTreeReverse_iter_0) {
                        Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 cfpm = (Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match_sptrr);
                        match_sptrr._iter_0.Add(cfpm);
                    }
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_spanningTreeReverse_node_root.lgspFlags = candidate_spanningTreeReverse_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
                    return matches;
                }
                candidate_spanningTreeReverse_node_root.lgspFlags = candidate_spanningTreeReverse_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
                return matches;
            }
            candidate_spanningTreeReverse_node_root.lgspFlags = candidate_spanningTreeReverse_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeReverse_node_root;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeReverse_node_root);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeReverse_node_root)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTreeReverse_node_root);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTreeReverse.IMatch_spanningTreeReverse match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches)
        {
            foreach(Rule_spanningTreeReverse.IMatch_spanningTreeReverse match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeReverse_node_root);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTreeReverse_node_root);
            if(matches.Count <= 0) return false;
            foreach(Rule_spanningTreeReverse.IMatch_spanningTreeReverse match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeReverse_node_root);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeReverse_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeReverse_node_root);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeReverse_node_root);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTreeReverse_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeReverse_node_root);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_spanningTreeReverse.IMatch_spanningTreeReverse)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeReverse.IMatch_spanningTreeReverse>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    public class IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Rule_spanningTreeReverse.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeReverse_iter_0_inlined_sptrr_5_in_spanningTreeReverse next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeReverse_node_root_inlined_sptrr_5;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeReverse_node_root_inlined_sptrr_5 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_node_root_inlined_sptrr_5 = SpanningTreeReverse_node_root_inlined_sptrr_5;
                // Extend Outgoing SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 from SpanningTreeReverse_node_root_inlined_sptrr_5 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 = candidate_SpanningTreeReverse_node_root_inlined_sptrr_5.lgspOuthead;
                if(head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 = head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5;
                    do
                    {
                        if(candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5 from SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspTarget;
                        if((candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push subpattern matching task for sptrr_inlined_sptrr_5
                        PatternAction_SpanningTreeReverse taskFor_sptrr_inlined_sptrr_5 = PatternAction_SpanningTreeReverse.getNewTask(actionEnv, openTasks);
                        taskFor_sptrr_inlined_sptrr_5.SpanningTreeReverse_node_root = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5;
                        taskFor_sptrr_inlined_sptrr_5.searchPatternpath = false;
                        taskFor_sptrr_inlined_sptrr_5.matchOfNestingPattern = null;
                        taskFor_sptrr_inlined_sptrr_5.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor_sptrr_inlined_sptrr_5);
                        uint prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5;
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5 = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5;
                        prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for sptrr_inlined_sptrr_5
                        openTasks.Pop();
                        PatternAction_SpanningTreeReverse.releaseTask(taskFor_sptrr_inlined_sptrr_5);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                                match._node_root = candidate_SpanningTreeReverse_node_root_inlined_sptrr_5;
                                match._node_next = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5;
                                match._edge__edge0 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5;
                                match._sptrr = (@Pattern_SpanningTreeReverse.Match_SpanningTreeReverse)currentFoundPartialMatch.Pop();
                                match._sptrr._matchOfEnclosingPattern = match;
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5;
                                candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5;
                            candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags = candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_node_next_inlined_sptrr_5;
                        candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 = candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5.lgspOutNext) != head_candidate_SpanningTreeReverse_iter_0_edge__edge0_inlined_sptrr_5 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0 match = new Pattern_SpanningTreeReverse.Match_SpanningTreeReverse_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_spanningTreeOutgoing
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root);
    }
    
    public class Action_spanningTreeOutgoing : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_spanningTreeOutgoing
    {
        public Action_spanningTreeOutgoing() {
            _rulePattern = Rule_spanningTreeOutgoing.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeOutgoing.Match_spanningTreeOutgoing, Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing>(this);
        }

        public Rule_spanningTreeOutgoing _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "spanningTreeOutgoing"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeOutgoing.Match_spanningTreeOutgoing, Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches;

        public static Action_spanningTreeOutgoing Instance { get { return instance; } }
        private static Action_spanningTreeOutgoing instance = new Action_spanningTreeOutgoing();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset spanningTreeOutgoing_node_root 
            GRGEN_LGSP.LGSPNode candidate_spanningTreeOutgoing_node_root = (GRGEN_LGSP.LGSPNode)spanningTreeOutgoing_node_root;
            // Element SpanningTreeOutgoing_node_root_inlined__sub0_6 assigned from other element spanningTreeOutgoing_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_node_root_inlined__sub0_6 = candidate_spanningTreeOutgoing_node_root;
            // Push iterated matching task for SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing
            IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing taskFor_iter_0_inlined__sub0_6_in_spanningTreeOutgoing = IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing.getNewTask(actionEnv, openTasks);
            taskFor_iter_0_inlined__sub0_6_in_spanningTreeOutgoing.SpanningTreeOutgoing_node_root_inlined__sub0_6 = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_6;
            taskFor_iter_0_inlined__sub0_6_in_spanningTreeOutgoing.searchPatternpath = false;
            taskFor_iter_0_inlined__sub0_6_in_spanningTreeOutgoing.matchOfNestingPattern = null;
            taskFor_iter_0_inlined__sub0_6_in_spanningTreeOutgoing.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_iter_0_inlined__sub0_6_in_spanningTreeOutgoing);
            uint prevGlobal__candidate_spanningTreeOutgoing_node_root;
            prevGlobal__candidate_spanningTreeOutgoing_node_root = candidate_spanningTreeOutgoing_node_root.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_spanningTreeOutgoing_node_root.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing
            openTasks.Pop();
            IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing.releaseTask(taskFor_iter_0_inlined__sub0_6_in_spanningTreeOutgoing);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_spanningTreeOutgoing.Match_spanningTreeOutgoing match = matches.GetNextUnfilledPosition();
                    Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing match__sub0 = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing();
                    match__sub0.SetMatchOfEnclosingPattern(match);
                    match._node_root = candidate_spanningTreeOutgoing_node_root;
                    match__sub0._node_root = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_6;
                    match.__sub0 = match__sub0;
                    match__sub0._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0, Pattern_SpanningTreeOutgoing.IMatch_SpanningTreeOutgoing_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeOutgoing.IMatch_SpanningTreeOutgoing_iter_0) {
                        Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 cfpm = (Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match__sub0);
                        match__sub0._iter_0.Add(cfpm);
                    }
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_spanningTreeOutgoing_node_root.lgspFlags = candidate_spanningTreeOutgoing_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeOutgoing_node_root;
                    return matches;
                }
                candidate_spanningTreeOutgoing_node_root.lgspFlags = candidate_spanningTreeOutgoing_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeOutgoing_node_root;
                return matches;
            }
            candidate_spanningTreeOutgoing_node_root.lgspFlags = candidate_spanningTreeOutgoing_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeOutgoing_node_root;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTreeOutgoing_node_root);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches)
        {
            foreach(Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeOutgoing_node_root);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTreeOutgoing_node_root);
            if(matches.Count <= 0) return false;
            foreach(Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeOutgoing_node_root);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeOutgoing_node_root);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeOutgoing_node_root);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTreeOutgoing_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeOutgoing_node_root);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeOutgoing.IMatch_spanningTreeOutgoing>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    public class IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Rule_spanningTreeOutgoing.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeOutgoing_iter_0_inlined__sub0_6_in_spanningTreeOutgoing next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeOutgoing_node_root_inlined__sub0_6;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeOutgoing_node_root_inlined__sub0_6 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_node_root_inlined__sub0_6 = SpanningTreeOutgoing_node_root_inlined__sub0_6;
                // Extend Outgoing SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 from SpanningTreeOutgoing_node_root_inlined__sub0_6 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_6.lgspOuthead;
                if(head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 = head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6;
                    do
                    {
                        if(candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Target SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6 from SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspTarget;
                        if((candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push subpattern matching task for _sub0_inlined__sub0_6
                        PatternAction_SpanningTreeOutgoing taskFor__sub0_inlined__sub0_6 = PatternAction_SpanningTreeOutgoing.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_6.SpanningTreeOutgoing_node_root = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6;
                        taskFor__sub0_inlined__sub0_6.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_6.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_6.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_6);
                        uint prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6;
                        prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6 = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6;
                        prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0_inlined__sub0_6
                        openTasks.Pop();
                        PatternAction_SpanningTreeOutgoing.releaseTask(taskFor__sub0_inlined__sub0_6);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                                match._node_root = candidate_SpanningTreeOutgoing_node_root_inlined__sub0_6;
                                match._node_next = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6;
                                match._edge__edge0 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6;
                                match.__sub0 = (@Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6;
                                candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6;
                            candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_node_next_inlined__sub0_6;
                        candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 = candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6.lgspOutNext) != head_candidate_SpanningTreeOutgoing_iter_0_edge__edge0_inlined__sub0_6 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0 match = new Pattern_SpanningTreeOutgoing.Match_SpanningTreeOutgoing_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }

    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_spanningTreeIncoming
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeIncoming_node_root);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTreeIncoming_node_root);
    }
    
    public class Action_spanningTreeIncoming : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_spanningTreeIncoming
    {
        public Action_spanningTreeIncoming() {
            _rulePattern = Rule_spanningTreeIncoming.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeIncoming.Match_spanningTreeIncoming, Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming>(this);
        }

        public Rule_spanningTreeIncoming _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "spanningTreeIncoming"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_spanningTreeIncoming.Match_spanningTreeIncoming, Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches;

        public static Action_spanningTreeIncoming Instance { get { return instance; } }
        private static Action_spanningTreeIncoming instance = new Action_spanningTreeIncoming();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeIncoming_node_root)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks = new Stack<GRGEN_LGSP.LGSPSubpatternAction>();
            List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches = new List<Stack<GRGEN_LIBGR.IMatch>>();
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            // Preset spanningTreeIncoming_node_root 
            GRGEN_LGSP.LGSPNode candidate_spanningTreeIncoming_node_root = (GRGEN_LGSP.LGSPNode)spanningTreeIncoming_node_root;
            // Element SpanningTreeIncoming_node_root_inlined__sub0_7 assigned from other element spanningTreeIncoming_node_root 
            GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_node_root_inlined__sub0_7 = candidate_spanningTreeIncoming_node_root;
            // Push iterated matching task for SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming
            IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming taskFor_iter_0_inlined__sub0_7_in_spanningTreeIncoming = IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming.getNewTask(actionEnv, openTasks);
            taskFor_iter_0_inlined__sub0_7_in_spanningTreeIncoming.SpanningTreeIncoming_node_root_inlined__sub0_7 = candidate_SpanningTreeIncoming_node_root_inlined__sub0_7;
            taskFor_iter_0_inlined__sub0_7_in_spanningTreeIncoming.searchPatternpath = false;
            taskFor_iter_0_inlined__sub0_7_in_spanningTreeIncoming.matchOfNestingPattern = null;
            taskFor_iter_0_inlined__sub0_7_in_spanningTreeIncoming.lastMatchAtPreviousNestingLevel = null;
            openTasks.Push(taskFor_iter_0_inlined__sub0_7_in_spanningTreeIncoming);
            uint prevGlobal__candidate_spanningTreeIncoming_node_root;
            prevGlobal__candidate_spanningTreeIncoming_node_root = candidate_spanningTreeIncoming_node_root.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            candidate_spanningTreeIncoming_node_root.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop iterated matching task for SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming
            openTasks.Pop();
            IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming.releaseTask(taskFor_iter_0_inlined__sub0_7_in_spanningTreeIncoming);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                {
                    Rule_spanningTreeIncoming.Match_spanningTreeIncoming match = matches.GetNextUnfilledPosition();
                    Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming match__sub0 = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming();
                    match__sub0.SetMatchOfEnclosingPattern(match);
                    match._node_root = candidate_spanningTreeIncoming_node_root;
                    match__sub0._node_root = candidate_SpanningTreeIncoming_node_root_inlined__sub0_7;
                    match.__sub0 = match__sub0;
                    match__sub0._iter_0 = new GRGEN_LGSP.LGSPMatchesList<Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0, Pattern_SpanningTreeIncoming.IMatch_SpanningTreeIncoming_iter_0>(null);
                    while(currentFoundPartialMatch.Count>0 && currentFoundPartialMatch.Peek() is Pattern_SpanningTreeIncoming.IMatch_SpanningTreeIncoming_iter_0) {
                        Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 cfpm = (Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0)currentFoundPartialMatch.Pop();
                        if(cfpm.IsNullMatch) break;
                        cfpm.SetMatchOfEnclosingPattern(match__sub0);
                        match__sub0._iter_0.Add(cfpm);
                    }
                    matches.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    candidate_spanningTreeIncoming_node_root.lgspFlags = candidate_spanningTreeIncoming_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeIncoming_node_root;
                    return matches;
                }
                candidate_spanningTreeIncoming_node_root.lgspFlags = candidate_spanningTreeIncoming_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeIncoming_node_root;
                return matches;
            }
            candidate_spanningTreeIncoming_node_root.lgspFlags = candidate_spanningTreeIncoming_node_root.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_spanningTreeIncoming_node_root;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeIncoming_node_root);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_LIBGR.INode spanningTreeIncoming_node_root)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTreeIncoming_node_root);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches)
        {
            foreach(Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeIncoming_node_root);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, spanningTreeIncoming_node_root);
            if(matches.Count <= 0) return false;
            foreach(Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeIncoming_node_root);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.INode spanningTreeIncoming_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeIncoming_node_root);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeIncoming_node_root);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_LIBGR.INode spanningTreeIncoming_node_root)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, spanningTreeIncoming_node_root);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_LIBGR.INode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_spanningTreeIncoming.IMatch_spanningTreeIncoming>)matches);
            return ReturnArray;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_LIBGR.INode) parameters[0])) {
                return ReturnArray;
            }
            else return null;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_LIBGR.INode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_LIBGR.INode) parameters[0]);
        }
        void GRGEN_LIBGR.IAction.Filter(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches, string filterName)
        {
            switch(filterName) {
                default: throw new Exception("Unknown filter name");
            }
        }
    }

    public class IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming : GRGEN_LGSP.LGSPSubpatternAction
    {
        private IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            actionEnv = actionEnv_; openTasks = openTasks_;
            patternGraph = Rule_spanningTreeIncoming.Instance.patternGraph;
            minMatchesIter = 0;
            maxMatchesIter = 0;
            numMatchesIter = 0;
        }

        int minMatchesIter;
        int maxMatchesIter;
        int numMatchesIter;

        public static IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming getNewTask(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv_, Stack<GRGEN_LGSP.LGSPSubpatternAction> openTasks_) {
            IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.actionEnv = actionEnv_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming(actionEnv_, openTasks_);
            }
            return newTask;
        }

        public static void releaseTask(IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.actionEnv = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private IteratedAction_SpanningTreeIncoming_iter_0_inlined__sub0_7_in_spanningTreeIncoming next = null;

        public GRGEN_LGSP.LGSPNode SpanningTreeIncoming_node_root_inlined__sub0_7;
        
        public override void myMatch(List<Stack<GRGEN_LIBGR.IMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            bool patternFound = false;
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            List<Stack<GRGEN_LIBGR.IMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // if the maximum number of matches of the iterated is reached, we complete iterated matching by building the null match object
            if(maxMatchesIter>0 && numMatchesIter>=maxMatchesIter) goto maxMatchesIterReached;
            // dummy iteration for iterated return prevention
            do
            {
                // SubPreset SpanningTreeIncoming_node_root_inlined__sub0_7 
                GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_node_root_inlined__sub0_7 = SpanningTreeIncoming_node_root_inlined__sub0_7;
                // Extend Incoming SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 from SpanningTreeIncoming_node_root_inlined__sub0_7 
                GRGEN_LGSP.LGSPEdge head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 = candidate_SpanningTreeIncoming_node_root_inlined__sub0_7.lgspInhead;
                if(head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 = head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7;
                    do
                    {
                        if(candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // Implicit Source SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7 from SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 
                        GRGEN_LGSP.LGSPNode candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspSource;
                        if((candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        if((candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)==(uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel)
                        {
                            continue;
                        }
                        // accept iterated instance match
                        ++numMatchesIter;
                        // Push subpattern matching task for _sub0_inlined__sub0_7
                        PatternAction_SpanningTreeIncoming taskFor__sub0_inlined__sub0_7 = PatternAction_SpanningTreeIncoming.getNewTask(actionEnv, openTasks);
                        taskFor__sub0_inlined__sub0_7.SpanningTreeIncoming_node_root = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7;
                        taskFor__sub0_inlined__sub0_7.searchPatternpath = false;
                        taskFor__sub0_inlined__sub0_7.matchOfNestingPattern = null;
                        taskFor__sub0_inlined__sub0_7.lastMatchAtPreviousNestingLevel = null;
                        openTasks.Push(taskFor__sub0_inlined__sub0_7);
                        uint prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7;
                        prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7 = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        uint prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7;
                        prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _sub0_inlined__sub0_7
                        openTasks.Pop();
                        PatternAction_SpanningTreeIncoming.releaseTask(taskFor__sub0_inlined__sub0_7);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            patternFound = true;
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                            {
                                Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                                match._node_next = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7;
                                match._node_root = candidate_SpanningTreeIncoming_node_root_inlined__sub0_7;
                                match._edge__edge0 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7;
                                match.__sub0 = (@Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming)currentFoundPartialMatch.Pop();
                                match.__sub0._matchOfEnclosingPattern = match;
                                currentFoundPartialMatch.Push(match);
                            }
                            // if enough matches were found, we leave
                            if(true) // as soon as there's a match, it's enough for iterated
                            {
                                candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7;
                                candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7;
                                --numMatchesIter;
                                goto maxMatchesIterReached;
                            }
                            candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7;
                            candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7;
                            --numMatchesIter;
                            continue;
                        }
                        candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags = candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_node_next_inlined__sub0_7;
                        candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED_BY_ENCLOSING_PATTERN << negLevel) | prevGlobal__candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7;
                        --numMatchesIter;
                    }
                    while( (candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 = candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7.lgspInNext) != head_candidate_SpanningTreeIncoming_iter_0_edge__edge0_inlined__sub0_7 );
                }
            } while(false);
            // Check whether the iterated pattern null match was found
maxMatchesIterReached:
            if(!patternFound && numMatchesIter>=minMatchesIter)
            {
                openTasks.Pop();
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch = new Stack<GRGEN_LIBGR.IMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                    match._isNullMatch = true; // null match of iterated pattern
                    currentFoundPartialMatch.Push(match);
                    openTasks.Push(this);
                    return;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<GRGEN_LIBGR.IMatch> currentFoundPartialMatch in matchesList)
                    {
                        Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0 match = new Pattern_SpanningTreeIncoming.Match_SpanningTreeIncoming_iter_0();
                        match._isNullMatch = true; // null match of iterated pattern
                        currentFoundPartialMatch.Push(match);
                    }
                }
                openTasks.Push(this);
                return;
            }
            return;
        }
    }


    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class spanningTreeActions : GRGEN_LGSP.LGSPActions
    {
        public spanningTreeActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public spanningTreeActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTree.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Pattern_SpanningTree.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Pattern_SpanningTree.Instance);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTreeReverse.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Pattern_SpanningTreeReverse.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Pattern_SpanningTreeReverse.Instance);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTreeOutgoing.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Pattern_SpanningTreeOutgoing.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Pattern_SpanningTreeOutgoing.Instance);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTreeIncoming.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Pattern_SpanningTreeIncoming.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Pattern_SpanningTreeIncoming.Instance);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_initTree.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_initTree.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_initTree.Instance);
            actions.Add("initTree", (GRGEN_LGSP.LGSPAction) Action_initTree.Instance);
            @initTree = Action_initTree.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_initUndirected.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_initUndirected.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_initUndirected.Instance);
            actions.Add("initUndirected", (GRGEN_LGSP.LGSPAction) Action_initUndirected.Instance);
            @initUndirected = Action_initUndirected.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_initDirected.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_initDirected.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_initDirected.Instance);
            actions.Add("initDirected", (GRGEN_LGSP.LGSPAction) Action_initDirected.Instance);
            @initDirected = Action_initDirected.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTree.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_spanningTree.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_spanningTree.Instance);
            actions.Add("spanningTree", (GRGEN_LGSP.LGSPAction) Action_spanningTree.Instance);
            @spanningTree = Action_spanningTree.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_edgesVisited.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_edgesVisited.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_edgesVisited.Instance);
            actions.Add("edgesVisited", (GRGEN_LGSP.LGSPAction) Action_edgesVisited.Instance);
            @edgesVisited = Action_edgesVisited.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_nodesVisited.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_nodesVisited.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_nodesVisited.Instance);
            actions.Add("nodesVisited", (GRGEN_LGSP.LGSPAction) Action_nodesVisited.Instance);
            @nodesVisited = Action_nodesVisited.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTreeReverse.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_spanningTreeReverse.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_spanningTreeReverse.Instance);
            actions.Add("spanningTreeReverse", (GRGEN_LGSP.LGSPAction) Action_spanningTreeReverse.Instance);
            @spanningTreeReverse = Action_spanningTreeReverse.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTreeOutgoing.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_spanningTreeOutgoing.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_spanningTreeOutgoing.Instance);
            actions.Add("spanningTreeOutgoing", (GRGEN_LGSP.LGSPAction) Action_spanningTreeOutgoing.Instance);
            @spanningTreeOutgoing = Action_spanningTreeOutgoing.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTreeIncoming.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(Rule_spanningTreeIncoming.Instance.patternGraph);
            analyzer.RememberMatchingPattern(Rule_spanningTreeIncoming.Instance);
            actions.Add("spanningTreeIncoming", (GRGEN_LGSP.LGSPAction) Action_spanningTreeIncoming.Instance);
            @spanningTreeIncoming = Action_spanningTreeIncoming.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_SpanningTree.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_SpanningTreeReverse.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_SpanningTreeOutgoing.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Pattern_SpanningTreeIncoming.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_initTree.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_initUndirected.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_initDirected.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_spanningTree.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_edgesVisited.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_nodesVisited.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_spanningTreeReverse.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_spanningTreeOutgoing.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_spanningTreeIncoming.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Pattern_SpanningTree.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Pattern_SpanningTreeReverse.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Pattern_SpanningTreeOutgoing.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Pattern_SpanningTreeIncoming.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_initTree.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_initUndirected.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_initDirected.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_spanningTree.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_edgesVisited.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_nodesVisited.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_spanningTreeReverse.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_spanningTreeOutgoing.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(Rule_spanningTreeIncoming.Instance.patternGraph);
            Pattern_SpanningTree.Instance.patternGraph.maxNegLevel = 0;
            Pattern_SpanningTreeReverse.Instance.patternGraph.maxNegLevel = 0;
            Pattern_SpanningTreeOutgoing.Instance.patternGraph.maxNegLevel = 0;
            Pattern_SpanningTreeIncoming.Instance.patternGraph.maxNegLevel = 0;
            Rule_initTree.Instance.patternGraph.maxNegLevel = 0;
            Rule_initUndirected.Instance.patternGraph.maxNegLevel = 0;
            Rule_initDirected.Instance.patternGraph.maxNegLevel = 0;
            Rule_spanningTree.Instance.patternGraph.maxNegLevel = 0;
            Rule_edgesVisited.Instance.patternGraph.maxNegLevel = 0;
            Rule_nodesVisited.Instance.patternGraph.maxNegLevel = 0;
            Rule_spanningTreeReverse.Instance.patternGraph.maxNegLevel = 0;
            Rule_spanningTreeOutgoing.Instance.patternGraph.maxNegLevel = 0;
            Rule_spanningTreeIncoming.Instance.patternGraph.maxNegLevel = 0;
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTree.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTreeReverse.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTreeOutgoing.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Pattern_SpanningTreeIncoming.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_initTree.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_initUndirected.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_initDirected.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTree.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_edgesVisited.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_nodesVisited.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTreeReverse.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTreeOutgoing.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(Rule_spanningTreeIncoming.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
        }
        
        public IAction_initTree @initTree;
        public IAction_initUndirected @initUndirected;
        public IAction_initDirected @initDirected;
        public IAction_spanningTree @spanningTree;
        public IAction_edgesVisited @edgesVisited;
        public IAction_nodesVisited @nodesVisited;
        public IAction_spanningTreeReverse @spanningTreeReverse;
        public IAction_spanningTreeOutgoing @spanningTreeOutgoing;
        public IAction_spanningTreeIncoming @spanningTreeIncoming;
        
        
        public override string Name { get { return "spanningTreeActions"; } }
        public override string ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}