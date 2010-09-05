// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Mutex\MutexPimped.grg" on Sun Sep 05 18:47:40 CEST 2010

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_Mutex;

namespace de.unika.ipd.grGen.Action_MutexPimped
{
	public class Rule_newRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_newRule instance = null;
		public static Rule_newRule Instance { get { if (instance==null) { instance = new Rule_newRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] newRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] newRule_node_p2_AllowedTypes = null;
		public static bool[] newRule_node_p1_IsAllowedType = null;
		public static bool[] newRule_node_p2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] newRule_edge_n_AllowedTypes = null;
		public static bool[] newRule_edge_n_IsAllowedType = null;
		public enum newRule_NodeNums { @p1, @p2, };
		public enum newRule_EdgeNums { @n, };
		public enum newRule_VariableNums { };
		public enum newRule_SubNums { };
		public enum newRule_AltNums { };
		public enum newRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_newRule;


		private Rule_newRule()
		{
			name = "newRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] newRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] newRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] newRule_minMatches = new int[0] ;
			int[] newRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode newRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "newRule_node_p1", "p1", newRule_node_p1_AllowedTypes, newRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode newRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "newRule_node_p2", "p2", newRule_node_p2_AllowedTypes, newRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge newRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@next, "GRGEN_MODEL.Inext", "newRule_edge_n", "n", newRule_edge_n_AllowedTypes, newRule_edge_n_IsAllowedType, 1.0F, -1, false);
			pat_newRule = new GRGEN_LGSP.PatternGraph(
				"newRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { newRule_node_p1, newRule_node_p2 }, 
				new GRGEN_LGSP.PatternEdge[] { newRule_edge_n }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				newRule_minMatches,
				newRule_maxMatches,
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
				newRule_isNodeHomomorphicGlobal,
				newRule_isEdgeHomomorphicGlobal
			);
			pat_newRule.edgeToSourceNode.Add(newRule_edge_n, newRule_node_p1);
			pat_newRule.edgeToTargetNode.Add(newRule_edge_n, newRule_node_p2);

			newRule_node_p1.PointOfDefinition = pat_newRule;
			newRule_node_p2.PointOfDefinition = pat_newRule;
			newRule_edge_n.PointOfDefinition = pat_newRule;

			patternGraph = pat_newRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_newRule curMatch = (Match_newRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_n = curMatch._edge_n;
			graph.SettingAddedNodeNames( newRule_addedNodeNames );
			GRGEN_MODEL.@Process node_p = GRGEN_MODEL.@Process.CreateNode(graph);
			graph.SettingAddedEdgeNames( newRule_addedEdgeNames );
			GRGEN_MODEL.@next edge_n1;
			if(edge_n.lgspType == GRGEN_MODEL.EdgeType_next.typeVar)
			{
				// re-using edge_n as edge_n1
				edge_n1 = (GRGEN_MODEL.@next) edge_n;
				graph.ReuseEdge(edge_n, null, node_p);
			}
			else
			{
				graph.Remove(edge_n);
				edge_n1 = GRGEN_MODEL.@next.CreateEdge(graph, node_p1, node_p);
			}
			GRGEN_MODEL.@next edge_n2 = GRGEN_MODEL.@next.CreateEdge(graph, node_p, node_p2);
			return;
		}
		private static string[] newRule_addedNodeNames = new string[] { "p" };
		private static string[] newRule_addedEdgeNames = new string[] { "n1", "n2" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_newRule curMatch = (Match_newRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_n = curMatch._edge_n;
			graph.SettingAddedNodeNames( newRule_addedNodeNames );
			GRGEN_MODEL.@Process node_p = GRGEN_MODEL.@Process.CreateNode(graph);
			graph.SettingAddedEdgeNames( newRule_addedEdgeNames );
			GRGEN_MODEL.@next edge_n1 = GRGEN_MODEL.@next.CreateEdge(graph, node_p1, node_p);
			GRGEN_MODEL.@next edge_n2 = GRGEN_MODEL.@next.CreateEdge(graph, node_p, node_p2);
			graph.Remove(edge_n);
			return;
		}

		static Rule_newRule() {
		}

		public interface IMatch_newRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			//Edges
			GRGEN_MODEL.Inext edge_n { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_newRule : GRGEN_LGSP.ListElement<Match_newRule>, IMatch_newRule
		{
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public enum newRule_NodeNums { @p1, @p2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)newRule_NodeNums.@p1: return _node_p1;
				case (int)newRule_NodeNums.@p2: return _node_p2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Inext edge_n { get { return (GRGEN_MODEL.Inext)_edge_n; } }
			public GRGEN_LGSP.LGSPEdge _edge_n;
			public enum newRule_EdgeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)newRule_EdgeNums.@n: return _edge_n;
				default: return null;
				}
			}
			
			public enum newRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum newRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum newRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum newRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum newRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_newRule.instance.pat_newRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_killRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_killRule instance = null;
		public static Rule_killRule Instance { get { if (instance==null) { instance = new Rule_killRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] killRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] killRule_node_p_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] killRule_node_p2_AllowedTypes = null;
		public static bool[] killRule_node_p1_IsAllowedType = null;
		public static bool[] killRule_node_p_IsAllowedType = null;
		public static bool[] killRule_node_p2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] killRule_edge_n1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] killRule_edge_n2_AllowedTypes = null;
		public static bool[] killRule_edge_n1_IsAllowedType = null;
		public static bool[] killRule_edge_n2_IsAllowedType = null;
		public enum killRule_NodeNums { @p1, @p, @p2, };
		public enum killRule_EdgeNums { @n1, @n2, };
		public enum killRule_VariableNums { };
		public enum killRule_SubNums { };
		public enum killRule_AltNums { };
		public enum killRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_killRule;


		private Rule_killRule()
		{
			name = "killRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] killRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] killRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] killRule_minMatches = new int[0] ;
			int[] killRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode killRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "killRule_node_p1", "p1", killRule_node_p1_AllowedTypes, killRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode killRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "killRule_node_p", "p", killRule_node_p_AllowedTypes, killRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode killRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "killRule_node_p2", "p2", killRule_node_p2_AllowedTypes, killRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge killRule_edge_n1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@next, "GRGEN_MODEL.Inext", "killRule_edge_n1", "n1", killRule_edge_n1_AllowedTypes, killRule_edge_n1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge killRule_edge_n2 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@next, "GRGEN_MODEL.Inext", "killRule_edge_n2", "n2", killRule_edge_n2_AllowedTypes, killRule_edge_n2_IsAllowedType, 5.5F, -1, false);
			pat_killRule = new GRGEN_LGSP.PatternGraph(
				"killRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { killRule_node_p1, killRule_node_p, killRule_node_p2 }, 
				new GRGEN_LGSP.PatternEdge[] { killRule_edge_n1, killRule_edge_n2 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				killRule_minMatches,
				killRule_maxMatches,
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
				killRule_isNodeHomomorphicGlobal,
				killRule_isEdgeHomomorphicGlobal
			);
			pat_killRule.edgeToSourceNode.Add(killRule_edge_n1, killRule_node_p1);
			pat_killRule.edgeToTargetNode.Add(killRule_edge_n1, killRule_node_p);
			pat_killRule.edgeToSourceNode.Add(killRule_edge_n2, killRule_node_p);
			pat_killRule.edgeToTargetNode.Add(killRule_edge_n2, killRule_node_p2);

			killRule_node_p1.PointOfDefinition = pat_killRule;
			killRule_node_p.PointOfDefinition = pat_killRule;
			killRule_node_p2.PointOfDefinition = pat_killRule;
			killRule_edge_n1.PointOfDefinition = pat_killRule;
			killRule_edge_n2.PointOfDefinition = pat_killRule;

			patternGraph = pat_killRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_killRule curMatch = (Match_killRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_n1 = curMatch._edge_n1;
			GRGEN_LGSP.LGSPEdge edge_n2 = curMatch._edge_n2;
			graph.SettingAddedNodeNames( killRule_addedNodeNames );
			graph.SettingAddedEdgeNames( killRule_addedEdgeNames );
			GRGEN_MODEL.@next edge_n;
			if(edge_n1.lgspType == GRGEN_MODEL.EdgeType_next.typeVar)
			{
				// re-using edge_n1 as edge_n
				edge_n = (GRGEN_MODEL.@next) edge_n1;
				graph.ReuseEdge(edge_n1, null, node_p2);
			}
			else
			{
				graph.Remove(edge_n1);
				edge_n = GRGEN_MODEL.@next.CreateEdge(graph, node_p1, node_p2);
			}
			graph.Remove(edge_n2);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return;
		}
		private static string[] killRule_addedNodeNames = new string[] {  };
		private static string[] killRule_addedEdgeNames = new string[] { "n" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_killRule curMatch = (Match_killRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_n1 = curMatch._edge_n1;
			GRGEN_LGSP.LGSPEdge edge_n2 = curMatch._edge_n2;
			graph.SettingAddedNodeNames( killRule_addedNodeNames );
			graph.SettingAddedEdgeNames( killRule_addedEdgeNames );
			GRGEN_MODEL.@next edge_n = GRGEN_MODEL.@next.CreateEdge(graph, node_p1, node_p2);
			graph.Remove(edge_n1);
			graph.Remove(edge_n2);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return;
		}

		static Rule_killRule() {
		}

		public interface IMatch_killRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			//Edges
			GRGEN_MODEL.Inext edge_n1 { get; }
			GRGEN_MODEL.Inext edge_n2 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_killRule : GRGEN_LGSP.ListElement<Match_killRule>, IMatch_killRule
		{
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_p;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public enum killRule_NodeNums { @p1, @p, @p2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)killRule_NodeNums.@p1: return _node_p1;
				case (int)killRule_NodeNums.@p: return _node_p;
				case (int)killRule_NodeNums.@p2: return _node_p2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Inext edge_n1 { get { return (GRGEN_MODEL.Inext)_edge_n1; } }
			public GRGEN_MODEL.Inext edge_n2 { get { return (GRGEN_MODEL.Inext)_edge_n2; } }
			public GRGEN_LGSP.LGSPEdge _edge_n1;
			public GRGEN_LGSP.LGSPEdge _edge_n2;
			public enum killRule_EdgeNums { @n1, @n2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)killRule_EdgeNums.@n1: return _edge_n1;
				case (int)killRule_EdgeNums.@n2: return _edge_n2;
				default: return null;
				}
			}
			
			public enum killRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum killRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum killRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum killRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum killRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_killRule.instance.pat_killRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_mountRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_mountRule instance = null;
		public static Rule_mountRule Instance { get { if (instance==null) { instance = new Rule_mountRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] mountRule_node_p_AllowedTypes = null;
		public static bool[] mountRule_node_p_IsAllowedType = null;
		public enum mountRule_NodeNums { @p, };
		public enum mountRule_EdgeNums { };
		public enum mountRule_VariableNums { };
		public enum mountRule_SubNums { };
		public enum mountRule_AltNums { };
		public enum mountRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_mountRule;


		private Rule_mountRule()
		{
			name = "mountRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] mountRule_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] mountRule_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] mountRule_minMatches = new int[0] ;
			int[] mountRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode mountRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "mountRule_node_p", "p", mountRule_node_p_AllowedTypes, mountRule_node_p_IsAllowedType, 5.5F, -1, false);
			pat_mountRule = new GRGEN_LGSP.PatternGraph(
				"mountRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { mountRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				mountRule_minMatches,
				mountRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				mountRule_isNodeHomomorphicGlobal,
				mountRule_isEdgeHomomorphicGlobal
			);

			mountRule_node_p.PointOfDefinition = pat_mountRule;

			patternGraph = pat_mountRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_mountRule curMatch = (Match_mountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( mountRule_addedNodeNames );
			GRGEN_MODEL.@Resource node_r = GRGEN_MODEL.@Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( mountRule_addedEdgeNames );
			GRGEN_MODEL.@token edge_t = GRGEN_MODEL.@token.CreateEdge(graph, node_r, node_p);
			return;
		}
		private static string[] mountRule_addedNodeNames = new string[] { "r" };
		private static string[] mountRule_addedEdgeNames = new string[] { "t" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_mountRule curMatch = (Match_mountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( mountRule_addedNodeNames );
			GRGEN_MODEL.@Resource node_r = GRGEN_MODEL.@Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( mountRule_addedEdgeNames );
			GRGEN_MODEL.@token edge_t = GRGEN_MODEL.@token.CreateEdge(graph, node_r, node_p);
			return;
		}

		static Rule_mountRule() {
		}

		public interface IMatch_mountRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_mountRule : GRGEN_LGSP.ListElement<Match_mountRule>, IMatch_mountRule
		{
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum mountRule_NodeNums { @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)mountRule_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public enum mountRule_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum mountRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum mountRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum mountRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum mountRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum mountRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_mountRule.instance.pat_mountRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_unmountRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_unmountRule instance = null;
		public static Rule_unmountRule Instance { get { if (instance==null) { instance = new Rule_unmountRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] unmountRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] unmountRule_node_p_AllowedTypes = null;
		public static bool[] unmountRule_node_r_IsAllowedType = null;
		public static bool[] unmountRule_node_p_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] unmountRule_edge_t_AllowedTypes = null;
		public static bool[] unmountRule_edge_t_IsAllowedType = null;
		public enum unmountRule_NodeNums { @r, @p, };
		public enum unmountRule_EdgeNums { @t, };
		public enum unmountRule_VariableNums { };
		public enum unmountRule_SubNums { };
		public enum unmountRule_AltNums { };
		public enum unmountRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_unmountRule;


		private Rule_unmountRule()
		{
			name = "unmountRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] unmountRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] unmountRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] unmountRule_minMatches = new int[0] ;
			int[] unmountRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode unmountRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "unmountRule_node_r", "r", unmountRule_node_r_AllowedTypes, unmountRule_node_r_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternNode unmountRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "unmountRule_node_p", "p", unmountRule_node_p_AllowedTypes, unmountRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge unmountRule_edge_t = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@token, "GRGEN_MODEL.Itoken", "unmountRule_edge_t", "t", unmountRule_edge_t_AllowedTypes, unmountRule_edge_t_IsAllowedType, 5.5F, -1, false);
			pat_unmountRule = new GRGEN_LGSP.PatternGraph(
				"unmountRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { unmountRule_node_r, unmountRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { unmountRule_edge_t }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				unmountRule_minMatches,
				unmountRule_maxMatches,
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
				unmountRule_isNodeHomomorphicGlobal,
				unmountRule_isEdgeHomomorphicGlobal
			);
			pat_unmountRule.edgeToSourceNode.Add(unmountRule_edge_t, unmountRule_node_r);
			pat_unmountRule.edgeToTargetNode.Add(unmountRule_edge_t, unmountRule_node_p);

			unmountRule_node_r.PointOfDefinition = pat_unmountRule;
			unmountRule_node_p.PointOfDefinition = pat_unmountRule;
			unmountRule_edge_t.PointOfDefinition = pat_unmountRule;

			patternGraph = pat_unmountRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unmountRule curMatch = (Match_unmountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			graph.SettingAddedNodeNames( unmountRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unmountRule_addedEdgeNames );
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return;
		}
		private static string[] unmountRule_addedNodeNames = new string[] {  };
		private static string[] unmountRule_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unmountRule curMatch = (Match_unmountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			graph.SettingAddedNodeNames( unmountRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unmountRule_addedEdgeNames );
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return;
		}

		static Rule_unmountRule() {
		}

		public interface IMatch_unmountRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Itoken edge_t { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_unmountRule : GRGEN_LGSP.ListElement<Match_unmountRule>, IMatch_unmountRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum unmountRule_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)unmountRule_NodeNums.@r: return _node_r;
				case (int)unmountRule_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Itoken edge_t { get { return (GRGEN_MODEL.Itoken)_edge_t; } }
			public GRGEN_LGSP.LGSPEdge _edge_t;
			public enum unmountRule_EdgeNums { @t, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)unmountRule_EdgeNums.@t: return _edge_t;
				default: return null;
				}
			}
			
			public enum unmountRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unmountRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unmountRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unmountRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unmountRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_unmountRule.instance.pat_unmountRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_passRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_passRule instance = null;
		public static Rule_passRule Instance { get { if (instance==null) { instance = new Rule_passRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] passRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] passRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] passRule_node_p2_AllowedTypes = null;
		public static bool[] passRule_node_r_IsAllowedType = null;
		public static bool[] passRule_node_p1_IsAllowedType = null;
		public static bool[] passRule_node_p2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] passRule_edge__edge0_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] passRule_edge_n_AllowedTypes = null;
		public static bool[] passRule_edge__edge0_IsAllowedType = null;
		public static bool[] passRule_edge_n_IsAllowedType = null;
		public enum passRule_NodeNums { @r, @p1, @p2, };
		public enum passRule_EdgeNums { @_edge0, @n, };
		public enum passRule_VariableNums { };
		public enum passRule_SubNums { };
		public enum passRule_AltNums { };
		public enum passRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_passRule;

		public static GRGEN_LIBGR.EdgeType[] passRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] passRule_neg_0_edge_req_IsAllowedType = null;
		public enum passRule_neg_0_NodeNums { @p1, @r, };
		public enum passRule_neg_0_EdgeNums { @req, };
		public enum passRule_neg_0_VariableNums { };
		public enum passRule_neg_0_SubNums { };
		public enum passRule_neg_0_AltNums { };
		public enum passRule_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph passRule_neg_0;


		private Rule_passRule()
		{
			name = "passRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] passRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] passRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] passRule_minMatches = new int[0] ;
			int[] passRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode passRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "passRule_node_r", "r", passRule_node_r_AllowedTypes, passRule_node_r_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternNode passRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "passRule_node_p1", "p1", passRule_node_p1_AllowedTypes, passRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode passRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "passRule_node_p2", "p2", passRule_node_p2_AllowedTypes, passRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge passRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@token, "GRGEN_MODEL.Itoken", "passRule_edge__edge0", "_edge0", passRule_edge__edge0_AllowedTypes, passRule_edge__edge0_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge passRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@next, "GRGEN_MODEL.Inext", "passRule_edge_n", "n", passRule_edge_n_AllowedTypes, passRule_edge_n_IsAllowedType, 5.5F, -1, false);
			bool[,] passRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] passRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] passRule_neg_0_minMatches = new int[0] ;
			int[] passRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge passRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "passRule_neg_0_edge_req", "req", passRule_neg_0_edge_req_AllowedTypes, passRule_neg_0_edge_req_IsAllowedType, 5.5F, -1, false);
			passRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"passRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { passRule_node_p1, passRule_node_r }, 
				new GRGEN_LGSP.PatternEdge[] { passRule_neg_0_edge_req }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				passRule_neg_0_minMatches,
				passRule_neg_0_maxMatches,
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
				passRule_neg_0_isNodeHomomorphicGlobal,
				passRule_neg_0_isEdgeHomomorphicGlobal
			);
			passRule_neg_0.edgeToSourceNode.Add(passRule_neg_0_edge_req, passRule_node_p1);
			passRule_neg_0.edgeToTargetNode.Add(passRule_neg_0_edge_req, passRule_node_r);

			pat_passRule = new GRGEN_LGSP.PatternGraph(
				"passRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { passRule_node_r, passRule_node_p1, passRule_node_p2 }, 
				new GRGEN_LGSP.PatternEdge[] { passRule_edge__edge0, passRule_edge_n }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				passRule_minMatches,
				passRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { passRule_neg_0,  }, 
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
				passRule_isNodeHomomorphicGlobal,
				passRule_isEdgeHomomorphicGlobal
			);
			pat_passRule.edgeToSourceNode.Add(passRule_edge__edge0, passRule_node_r);
			pat_passRule.edgeToTargetNode.Add(passRule_edge__edge0, passRule_node_p1);
			pat_passRule.edgeToSourceNode.Add(passRule_edge_n, passRule_node_p1);
			pat_passRule.edgeToTargetNode.Add(passRule_edge_n, passRule_node_p2);
			passRule_neg_0.embeddingGraph = pat_passRule;

			passRule_node_r.PointOfDefinition = pat_passRule;
			passRule_node_p1.PointOfDefinition = pat_passRule;
			passRule_node_p2.PointOfDefinition = pat_passRule;
			passRule_edge__edge0.PointOfDefinition = pat_passRule;
			passRule_edge_n.PointOfDefinition = pat_passRule;
			passRule_neg_0_edge_req.PointOfDefinition = passRule_neg_0;

			patternGraph = pat_passRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_passRule curMatch = (Match_passRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( passRule_addedNodeNames );
			graph.SettingAddedEdgeNames( passRule_addedEdgeNames );
			GRGEN_MODEL.@token edge_t;
			if(edge__edge0.lgspType == GRGEN_MODEL.EdgeType_token.typeVar)
			{
				// re-using edge__edge0 as edge_t
				edge_t = (GRGEN_MODEL.@token) edge__edge0;
				graph.ReuseEdge(edge__edge0, null, node_p2);
			}
			else
			{
				graph.Remove(edge__edge0);
				edge_t = GRGEN_MODEL.@token.CreateEdge(graph, node_r, node_p2);
			}
			return;
		}
		private static string[] passRule_addedNodeNames = new string[] {  };
		private static string[] passRule_addedEdgeNames = new string[] { "t" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_passRule curMatch = (Match_passRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( passRule_addedNodeNames );
			graph.SettingAddedEdgeNames( passRule_addedEdgeNames );
			GRGEN_MODEL.@token edge_t = GRGEN_MODEL.@token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge__edge0);
			return;
		}

		static Rule_passRule() {
		}

		public interface IMatch_passRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			//Edges
			GRGEN_MODEL.Itoken edge__edge0 { get; }
			GRGEN_MODEL.Inext edge_n { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_passRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IResource node_r { get; }
			//Edges
			GRGEN_MODEL.Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_passRule : GRGEN_LGSP.ListElement<Match_passRule>, IMatch_passRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public enum passRule_NodeNums { @r, @p1, @p2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)passRule_NodeNums.@r: return _node_r;
				case (int)passRule_NodeNums.@p1: return _node_p1;
				case (int)passRule_NodeNums.@p2: return _node_p2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Itoken edge__edge0 { get { return (GRGEN_MODEL.Itoken)_edge__edge0; } }
			public GRGEN_MODEL.Inext edge_n { get { return (GRGEN_MODEL.Inext)_edge_n; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public GRGEN_LGSP.LGSPEdge _edge_n;
			public enum passRule_EdgeNums { @_edge0, @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)passRule_EdgeNums.@_edge0: return _edge__edge0;
				case (int)passRule_EdgeNums.@n: return _edge_n;
				default: return null;
				}
			}
			
			public enum passRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_passRule.instance.pat_passRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_passRule_neg_0 : GRGEN_LGSP.ListElement<Match_passRule_neg_0>, IMatch_passRule_neg_0
		{
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_r;
			public enum passRule_neg_0_NodeNums { @p1, @r, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)passRule_neg_0_NodeNums.@p1: return _node_p1;
				case (int)passRule_neg_0_NodeNums.@r: return _node_r;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public enum passRule_neg_0_EdgeNums { @req, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)passRule_neg_0_EdgeNums.@req: return _edge_req;
				default: return null;
				}
			}
			
			public enum passRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum passRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_passRule.instance.passRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_requestRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_requestRule instance = null;
		public static Rule_requestRule Instance { get { if (instance==null) { instance = new Rule_requestRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] requestRule_node_p_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] requestRule_node_r_AllowedTypes = null;
		public static bool[] requestRule_node_p_IsAllowedType = null;
		public static bool[] requestRule_node_r_IsAllowedType = null;
		public enum requestRule_NodeNums { @p, @r, };
		public enum requestRule_EdgeNums { };
		public enum requestRule_VariableNums { };
		public enum requestRule_SubNums { };
		public enum requestRule_AltNums { };
		public enum requestRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_requestRule;

		public static GRGEN_LIBGR.EdgeType[] requestRule_neg_0_edge_hb_AllowedTypes = null;
		public static bool[] requestRule_neg_0_edge_hb_IsAllowedType = null;
		public enum requestRule_neg_0_NodeNums { @r, @p, };
		public enum requestRule_neg_0_EdgeNums { @hb, };
		public enum requestRule_neg_0_VariableNums { };
		public enum requestRule_neg_0_SubNums { };
		public enum requestRule_neg_0_AltNums { };
		public enum requestRule_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph requestRule_neg_0;

		public static GRGEN_LIBGR.NodeType[] requestRule_neg_1_node_m_AllowedTypes = null;
		public static bool[] requestRule_neg_1_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] requestRule_neg_1_edge_req_AllowedTypes = null;
		public static bool[] requestRule_neg_1_edge_req_IsAllowedType = null;
		public enum requestRule_neg_1_NodeNums { @p, @m, };
		public enum requestRule_neg_1_EdgeNums { @req, };
		public enum requestRule_neg_1_VariableNums { };
		public enum requestRule_neg_1_SubNums { };
		public enum requestRule_neg_1_AltNums { };
		public enum requestRule_neg_1_IterNums { };

		public GRGEN_LGSP.PatternGraph requestRule_neg_1;


		private Rule_requestRule()
		{
			name = "requestRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] requestRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestRule_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] requestRule_minMatches = new int[0] ;
			int[] requestRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode requestRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "requestRule_node_p", "p", requestRule_node_p_AllowedTypes, requestRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode requestRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "requestRule_node_r", "r", requestRule_node_r_AllowedTypes, requestRule_node_r_IsAllowedType, 1.0F, -1, false);
			bool[,] requestRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] requestRule_neg_0_minMatches = new int[0] ;
			int[] requestRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge requestRule_neg_0_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "requestRule_neg_0_edge_hb", "hb", requestRule_neg_0_edge_hb_AllowedTypes, requestRule_neg_0_edge_hb_IsAllowedType, 5.5F, -1, false);
			requestRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"requestRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { requestRule_node_r, requestRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { requestRule_neg_0_edge_hb }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				requestRule_neg_0_minMatches,
				requestRule_neg_0_maxMatches,
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
				requestRule_neg_0_isNodeHomomorphicGlobal,
				requestRule_neg_0_isEdgeHomomorphicGlobal
			);
			requestRule_neg_0.edgeToSourceNode.Add(requestRule_neg_0_edge_hb, requestRule_node_r);
			requestRule_neg_0.edgeToTargetNode.Add(requestRule_neg_0_edge_hb, requestRule_node_p);

			bool[,] requestRule_neg_1_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestRule_neg_1_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] requestRule_neg_1_minMatches = new int[0] ;
			int[] requestRule_neg_1_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode requestRule_neg_1_node_m = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "requestRule_neg_1_node_m", "m", requestRule_neg_1_node_m_AllowedTypes, requestRule_neg_1_node_m_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge requestRule_neg_1_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "requestRule_neg_1_edge_req", "req", requestRule_neg_1_edge_req_AllowedTypes, requestRule_neg_1_edge_req_IsAllowedType, 5.5F, -1, false);
			requestRule_neg_1 = new GRGEN_LGSP.PatternGraph(
				"neg_1",
				"requestRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { requestRule_node_p, requestRule_neg_1_node_m }, 
				new GRGEN_LGSP.PatternEdge[] { requestRule_neg_1_edge_req }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				requestRule_neg_1_minMatches,
				requestRule_neg_1_maxMatches,
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
				requestRule_neg_1_isNodeHomomorphicGlobal,
				requestRule_neg_1_isEdgeHomomorphicGlobal
			);
			requestRule_neg_1.edgeToSourceNode.Add(requestRule_neg_1_edge_req, requestRule_node_p);
			requestRule_neg_1.edgeToTargetNode.Add(requestRule_neg_1_edge_req, requestRule_neg_1_node_m);

			pat_requestRule = new GRGEN_LGSP.PatternGraph(
				"requestRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { requestRule_node_p, requestRule_node_r }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				requestRule_minMatches,
				requestRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { requestRule_neg_0, requestRule_neg_1,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				requestRule_isNodeHomomorphicGlobal,
				requestRule_isEdgeHomomorphicGlobal
			);
			requestRule_neg_0.embeddingGraph = pat_requestRule;
			requestRule_neg_1.embeddingGraph = pat_requestRule;

			requestRule_node_p.PointOfDefinition = pat_requestRule;
			requestRule_node_r.PointOfDefinition = pat_requestRule;
			requestRule_neg_0_edge_hb.PointOfDefinition = requestRule_neg_0;
			requestRule_neg_1_node_m.PointOfDefinition = requestRule_neg_1;
			requestRule_neg_1_edge_req.PointOfDefinition = requestRule_neg_1;

			patternGraph = pat_requestRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestRule curMatch = (Match_requestRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestRule_addedEdgeNames );
			GRGEN_MODEL.@request edge_req = GRGEN_MODEL.@request.CreateEdge(graph, node_p, node_r);
			return;
		}
		private static string[] requestRule_addedNodeNames = new string[] {  };
		private static string[] requestRule_addedEdgeNames = new string[] { "req" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestRule curMatch = (Match_requestRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestRule_addedEdgeNames );
			GRGEN_MODEL.@request edge_req = GRGEN_MODEL.@request.CreateEdge(graph, node_p, node_r);
			return;
		}

		static Rule_requestRule() {
		}

		public interface IMatch_requestRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p { get; }
			GRGEN_MODEL.IResource node_r { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_requestRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_requestRule_neg_1 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p { get; }
			GRGEN_MODEL.IResource node_m { get; }
			//Edges
			GRGEN_MODEL.Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_requestRule : GRGEN_LGSP.ListElement<Match_requestRule>, IMatch_requestRule
		{
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_LGSP.LGSPNode _node_p;
			public GRGEN_LGSP.LGSPNode _node_r;
			public enum requestRule_NodeNums { @p, @r, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)requestRule_NodeNums.@p: return _node_p;
				case (int)requestRule_NodeNums.@r: return _node_r;
				default: return null;
				}
			}
			
			public enum requestRule_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_requestRule.instance.pat_requestRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestRule_neg_0 : GRGEN_LGSP.ListElement<Match_requestRule_neg_0>, IMatch_requestRule_neg_0
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum requestRule_neg_0_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)requestRule_neg_0_NodeNums.@r: return _node_r;
				case (int)requestRule_neg_0_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iheld_by edge_hb { get { return (GRGEN_MODEL.Iheld_by)_edge_hb; } }
			public GRGEN_LGSP.LGSPEdge _edge_hb;
			public enum requestRule_neg_0_EdgeNums { @hb, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)requestRule_neg_0_EdgeNums.@hb: return _edge_hb;
				default: return null;
				}
			}
			
			public enum requestRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_requestRule.instance.requestRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestRule_neg_1 : GRGEN_LGSP.ListElement<Match_requestRule_neg_1>, IMatch_requestRule_neg_1
		{
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_MODEL.IResource node_m { get { return (GRGEN_MODEL.IResource)_node_m; } }
			public GRGEN_LGSP.LGSPNode _node_p;
			public GRGEN_LGSP.LGSPNode _node_m;
			public enum requestRule_neg_1_NodeNums { @p, @m, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)requestRule_neg_1_NodeNums.@p: return _node_p;
				case (int)requestRule_neg_1_NodeNums.@m: return _node_m;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public enum requestRule_neg_1_EdgeNums { @req, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)requestRule_neg_1_EdgeNums.@req: return _edge_req;
				default: return null;
				}
			}
			
			public enum requestRule_neg_1_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_1_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_1_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_1_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestRule_neg_1_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_requestRule.instance.requestRule_neg_1; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_takeRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_takeRule instance = null;
		public static Rule_takeRule Instance { get { if (instance==null) { instance = new Rule_takeRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] takeRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] takeRule_node_p_AllowedTypes = null;
		public static bool[] takeRule_node_r_IsAllowedType = null;
		public static bool[] takeRule_node_p_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] takeRule_edge_t_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] takeRule_edge_req_AllowedTypes = null;
		public static bool[] takeRule_edge_t_IsAllowedType = null;
		public static bool[] takeRule_edge_req_IsAllowedType = null;
		public enum takeRule_NodeNums { @r, @p, };
		public enum takeRule_EdgeNums { @t, @req, };
		public enum takeRule_VariableNums { };
		public enum takeRule_SubNums { };
		public enum takeRule_AltNums { };
		public enum takeRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_takeRule;


		private Rule_takeRule()
		{
			name = "takeRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] takeRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] takeRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] takeRule_minMatches = new int[0] ;
			int[] takeRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode takeRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "takeRule_node_r", "r", takeRule_node_r_AllowedTypes, takeRule_node_r_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode takeRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "takeRule_node_p", "p", takeRule_node_p_AllowedTypes, takeRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge takeRule_edge_t = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@token, "GRGEN_MODEL.Itoken", "takeRule_edge_t", "t", takeRule_edge_t_AllowedTypes, takeRule_edge_t_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternEdge takeRule_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "takeRule_edge_req", "req", takeRule_edge_req_AllowedTypes, takeRule_edge_req_IsAllowedType, 5.5F, -1, false);
			pat_takeRule = new GRGEN_LGSP.PatternGraph(
				"takeRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { takeRule_node_r, takeRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { takeRule_edge_t, takeRule_edge_req }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				takeRule_minMatches,
				takeRule_maxMatches,
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
				takeRule_isNodeHomomorphicGlobal,
				takeRule_isEdgeHomomorphicGlobal
			);
			pat_takeRule.edgeToSourceNode.Add(takeRule_edge_t, takeRule_node_r);
			pat_takeRule.edgeToTargetNode.Add(takeRule_edge_t, takeRule_node_p);
			pat_takeRule.edgeToSourceNode.Add(takeRule_edge_req, takeRule_node_p);
			pat_takeRule.edgeToTargetNode.Add(takeRule_edge_req, takeRule_node_r);

			takeRule_node_r.PointOfDefinition = pat_takeRule;
			takeRule_node_p.PointOfDefinition = pat_takeRule;
			takeRule_edge_t.PointOfDefinition = pat_takeRule;
			takeRule_edge_req.PointOfDefinition = pat_takeRule;

			patternGraph = pat_takeRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_takeRule curMatch = (Match_takeRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			GRGEN_LGSP.LGSPEdge edge_req = curMatch._edge_req;
			graph.SettingAddedNodeNames( takeRule_addedNodeNames );
			graph.SettingAddedEdgeNames( takeRule_addedEdgeNames );
			GRGEN_MODEL.@held_by edge_hb = GRGEN_MODEL.@held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_t);
			graph.Remove(edge_req);
			return;
		}
		private static string[] takeRule_addedNodeNames = new string[] {  };
		private static string[] takeRule_addedEdgeNames = new string[] { "hb" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_takeRule curMatch = (Match_takeRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			GRGEN_LGSP.LGSPEdge edge_req = curMatch._edge_req;
			graph.SettingAddedNodeNames( takeRule_addedNodeNames );
			graph.SettingAddedEdgeNames( takeRule_addedEdgeNames );
			GRGEN_MODEL.@held_by edge_hb = GRGEN_MODEL.@held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_t);
			graph.Remove(edge_req);
			return;
		}

		static Rule_takeRule() {
		}

		public interface IMatch_takeRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Itoken edge_t { get; }
			GRGEN_MODEL.Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_takeRule : GRGEN_LGSP.ListElement<Match_takeRule>, IMatch_takeRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum takeRule_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)takeRule_NodeNums.@r: return _node_r;
				case (int)takeRule_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Itoken edge_t { get { return (GRGEN_MODEL.Itoken)_edge_t; } }
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_LGSP.LGSPEdge _edge_t;
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public enum takeRule_EdgeNums { @t, @req, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)takeRule_EdgeNums.@t: return _edge_t;
				case (int)takeRule_EdgeNums.@req: return _edge_req;
				default: return null;
				}
			}
			
			public enum takeRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum takeRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum takeRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum takeRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum takeRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_takeRule.instance.pat_takeRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_releaseRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_releaseRule instance = null;
		public static Rule_releaseRule Instance { get { if (instance==null) { instance = new Rule_releaseRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] releaseRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] releaseRule_node_p_AllowedTypes = null;
		public static bool[] releaseRule_node_r_IsAllowedType = null;
		public static bool[] releaseRule_node_p_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] releaseRule_edge_hb_AllowedTypes = null;
		public static bool[] releaseRule_edge_hb_IsAllowedType = null;
		public enum releaseRule_NodeNums { @r, @p, };
		public enum releaseRule_EdgeNums { @hb, };
		public enum releaseRule_VariableNums { };
		public enum releaseRule_SubNums { };
		public enum releaseRule_AltNums { };
		public enum releaseRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_releaseRule;

		public static GRGEN_LIBGR.NodeType[] releaseRule_neg_0_node_m_AllowedTypes = null;
		public static bool[] releaseRule_neg_0_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] releaseRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] releaseRule_neg_0_edge_req_IsAllowedType = null;
		public enum releaseRule_neg_0_NodeNums { @p, @m, };
		public enum releaseRule_neg_0_EdgeNums { @req, };
		public enum releaseRule_neg_0_VariableNums { };
		public enum releaseRule_neg_0_SubNums { };
		public enum releaseRule_neg_0_AltNums { };
		public enum releaseRule_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph releaseRule_neg_0;


		private Rule_releaseRule()
		{
			name = "releaseRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] releaseRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] releaseRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] releaseRule_minMatches = new int[0] ;
			int[] releaseRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode releaseRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "releaseRule_node_r", "r", releaseRule_node_r_AllowedTypes, releaseRule_node_r_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode releaseRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "releaseRule_node_p", "p", releaseRule_node_p_AllowedTypes, releaseRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge releaseRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "releaseRule_edge_hb", "hb", releaseRule_edge_hb_AllowedTypes, releaseRule_edge_hb_IsAllowedType, 1.0F, -1, false);
			bool[,] releaseRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] releaseRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] releaseRule_neg_0_minMatches = new int[0] ;
			int[] releaseRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode releaseRule_neg_0_node_m = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "releaseRule_neg_0_node_m", "m", releaseRule_neg_0_node_m_AllowedTypes, releaseRule_neg_0_node_m_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge releaseRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "releaseRule_neg_0_edge_req", "req", releaseRule_neg_0_edge_req_AllowedTypes, releaseRule_neg_0_edge_req_IsAllowedType, 5.5F, -1, false);
			releaseRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"releaseRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { releaseRule_node_p, releaseRule_neg_0_node_m }, 
				new GRGEN_LGSP.PatternEdge[] { releaseRule_neg_0_edge_req }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				releaseRule_neg_0_minMatches,
				releaseRule_neg_0_maxMatches,
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
				releaseRule_neg_0_isNodeHomomorphicGlobal,
				releaseRule_neg_0_isEdgeHomomorphicGlobal
			);
			releaseRule_neg_0.edgeToSourceNode.Add(releaseRule_neg_0_edge_req, releaseRule_node_p);
			releaseRule_neg_0.edgeToTargetNode.Add(releaseRule_neg_0_edge_req, releaseRule_neg_0_node_m);

			pat_releaseRule = new GRGEN_LGSP.PatternGraph(
				"releaseRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { releaseRule_node_r, releaseRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { releaseRule_edge_hb }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				releaseRule_minMatches,
				releaseRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { releaseRule_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				releaseRule_isNodeHomomorphicGlobal,
				releaseRule_isEdgeHomomorphicGlobal
			);
			pat_releaseRule.edgeToSourceNode.Add(releaseRule_edge_hb, releaseRule_node_r);
			pat_releaseRule.edgeToTargetNode.Add(releaseRule_edge_hb, releaseRule_node_p);
			releaseRule_neg_0.embeddingGraph = pat_releaseRule;

			releaseRule_node_r.PointOfDefinition = pat_releaseRule;
			releaseRule_node_p.PointOfDefinition = pat_releaseRule;
			releaseRule_edge_hb.PointOfDefinition = pat_releaseRule;
			releaseRule_neg_0_node_m.PointOfDefinition = releaseRule_neg_0;
			releaseRule_neg_0_edge_req.PointOfDefinition = releaseRule_neg_0;

			patternGraph = pat_releaseRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseRule curMatch = (Match_releaseRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( releaseRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseRule_addedEdgeNames );
			GRGEN_MODEL.@release edge_rel = GRGEN_MODEL.@release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return;
		}
		private static string[] releaseRule_addedNodeNames = new string[] {  };
		private static string[] releaseRule_addedEdgeNames = new string[] { "rel" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseRule curMatch = (Match_releaseRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( releaseRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseRule_addedEdgeNames );
			GRGEN_MODEL.@release edge_rel = GRGEN_MODEL.@release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return;
		}

		static Rule_releaseRule() {
		}

		public interface IMatch_releaseRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_releaseRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p { get; }
			GRGEN_MODEL.IResource node_m { get; }
			//Edges
			GRGEN_MODEL.Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_releaseRule : GRGEN_LGSP.ListElement<Match_releaseRule>, IMatch_releaseRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum releaseRule_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)releaseRule_NodeNums.@r: return _node_r;
				case (int)releaseRule_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iheld_by edge_hb { get { return (GRGEN_MODEL.Iheld_by)_edge_hb; } }
			public GRGEN_LGSP.LGSPEdge _edge_hb;
			public enum releaseRule_EdgeNums { @hb, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)releaseRule_EdgeNums.@hb: return _edge_hb;
				default: return null;
				}
			}
			
			public enum releaseRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_releaseRule.instance.pat_releaseRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_releaseRule_neg_0 : GRGEN_LGSP.ListElement<Match_releaseRule_neg_0>, IMatch_releaseRule_neg_0
		{
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_MODEL.IResource node_m { get { return (GRGEN_MODEL.IResource)_node_m; } }
			public GRGEN_LGSP.LGSPNode _node_p;
			public GRGEN_LGSP.LGSPNode _node_m;
			public enum releaseRule_neg_0_NodeNums { @p, @m, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)releaseRule_neg_0_NodeNums.@p: return _node_p;
				case (int)releaseRule_neg_0_NodeNums.@m: return _node_m;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public enum releaseRule_neg_0_EdgeNums { @req, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)releaseRule_neg_0_EdgeNums.@req: return _edge_req;
				default: return null;
				}
			}
			
			public enum releaseRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_releaseRule.instance.releaseRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_giveRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_giveRule instance = null;
		public static Rule_giveRule Instance { get { if (instance==null) { instance = new Rule_giveRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] giveRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] giveRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] giveRule_node_p2_AllowedTypes = null;
		public static bool[] giveRule_node_r_IsAllowedType = null;
		public static bool[] giveRule_node_p1_IsAllowedType = null;
		public static bool[] giveRule_node_p2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] giveRule_edge_rel_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] giveRule_edge_n_AllowedTypes = null;
		public static bool[] giveRule_edge_rel_IsAllowedType = null;
		public static bool[] giveRule_edge_n_IsAllowedType = null;
		public enum giveRule_NodeNums { @r, @p1, @p2, };
		public enum giveRule_EdgeNums { @rel, @n, };
		public enum giveRule_VariableNums { };
		public enum giveRule_SubNums { };
		public enum giveRule_AltNums { };
		public enum giveRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_giveRule;


		private Rule_giveRule()
		{
			name = "giveRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] giveRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] giveRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] giveRule_minMatches = new int[0] ;
			int[] giveRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode giveRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "giveRule_node_r", "r", giveRule_node_r_AllowedTypes, giveRule_node_r_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode giveRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "giveRule_node_p1", "p1", giveRule_node_p1_AllowedTypes, giveRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode giveRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "giveRule_node_p2", "p2", giveRule_node_p2_AllowedTypes, giveRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge giveRule_edge_rel = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@release, "GRGEN_MODEL.Irelease", "giveRule_edge_rel", "rel", giveRule_edge_rel_AllowedTypes, giveRule_edge_rel_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternEdge giveRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@next, "GRGEN_MODEL.Inext", "giveRule_edge_n", "n", giveRule_edge_n_AllowedTypes, giveRule_edge_n_IsAllowedType, 5.5F, -1, false);
			pat_giveRule = new GRGEN_LGSP.PatternGraph(
				"giveRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { giveRule_node_r, giveRule_node_p1, giveRule_node_p2 }, 
				new GRGEN_LGSP.PatternEdge[] { giveRule_edge_rel, giveRule_edge_n }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				giveRule_minMatches,
				giveRule_maxMatches,
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
				giveRule_isNodeHomomorphicGlobal,
				giveRule_isEdgeHomomorphicGlobal
			);
			pat_giveRule.edgeToSourceNode.Add(giveRule_edge_rel, giveRule_node_r);
			pat_giveRule.edgeToTargetNode.Add(giveRule_edge_rel, giveRule_node_p1);
			pat_giveRule.edgeToSourceNode.Add(giveRule_edge_n, giveRule_node_p1);
			pat_giveRule.edgeToTargetNode.Add(giveRule_edge_n, giveRule_node_p2);

			giveRule_node_r.PointOfDefinition = pat_giveRule;
			giveRule_node_p1.PointOfDefinition = pat_giveRule;
			giveRule_node_p2.PointOfDefinition = pat_giveRule;
			giveRule_edge_rel.PointOfDefinition = pat_giveRule;
			giveRule_edge_n.PointOfDefinition = pat_giveRule;

			patternGraph = pat_giveRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_giveRule curMatch = (Match_giveRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_rel = curMatch._edge_rel;
			graph.SettingAddedNodeNames( giveRule_addedNodeNames );
			graph.SettingAddedEdgeNames( giveRule_addedEdgeNames );
			GRGEN_MODEL.@token edge_t = GRGEN_MODEL.@token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return;
		}
		private static string[] giveRule_addedNodeNames = new string[] {  };
		private static string[] giveRule_addedEdgeNames = new string[] { "t" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_giveRule curMatch = (Match_giveRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_rel = curMatch._edge_rel;
			graph.SettingAddedNodeNames( giveRule_addedNodeNames );
			graph.SettingAddedEdgeNames( giveRule_addedEdgeNames );
			GRGEN_MODEL.@token edge_t = GRGEN_MODEL.@token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return;
		}

		static Rule_giveRule() {
		}

		public interface IMatch_giveRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			//Edges
			GRGEN_MODEL.Irelease edge_rel { get; }
			GRGEN_MODEL.Inext edge_n { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_giveRule : GRGEN_LGSP.ListElement<Match_giveRule>, IMatch_giveRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public enum giveRule_NodeNums { @r, @p1, @p2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)giveRule_NodeNums.@r: return _node_r;
				case (int)giveRule_NodeNums.@p1: return _node_p1;
				case (int)giveRule_NodeNums.@p2: return _node_p2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irelease edge_rel { get { return (GRGEN_MODEL.Irelease)_edge_rel; } }
			public GRGEN_MODEL.Inext edge_n { get { return (GRGEN_MODEL.Inext)_edge_n; } }
			public GRGEN_LGSP.LGSPEdge _edge_rel;
			public GRGEN_LGSP.LGSPEdge _edge_n;
			public enum giveRule_EdgeNums { @rel, @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)giveRule_EdgeNums.@rel: return _edge_rel;
				case (int)giveRule_EdgeNums.@n: return _edge_n;
				default: return null;
				}
			}
			
			public enum giveRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum giveRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum giveRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum giveRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum giveRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_giveRule.instance.pat_giveRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_blockedRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_blockedRule instance = null;
		public static Rule_blockedRule Instance { get { if (instance==null) { instance = new Rule_blockedRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] blockedRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] blockedRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] blockedRule_node_p2_AllowedTypes = null;
		public static bool[] blockedRule_node_p1_IsAllowedType = null;
		public static bool[] blockedRule_node_r_IsAllowedType = null;
		public static bool[] blockedRule_node_p2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] blockedRule_edge_req_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] blockedRule_edge_hb_AllowedTypes = null;
		public static bool[] blockedRule_edge_req_IsAllowedType = null;
		public static bool[] blockedRule_edge_hb_IsAllowedType = null;
		public enum blockedRule_NodeNums { @p1, @r, @p2, };
		public enum blockedRule_EdgeNums { @req, @hb, };
		public enum blockedRule_VariableNums { };
		public enum blockedRule_SubNums { };
		public enum blockedRule_AltNums { };
		public enum blockedRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_blockedRule;


		private Rule_blockedRule()
		{
			name = "blockedRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] blockedRule_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] blockedRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] blockedRule_minMatches = new int[0] ;
			int[] blockedRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode blockedRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "blockedRule_node_p1", "p1", blockedRule_node_p1_AllowedTypes, blockedRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode blockedRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "blockedRule_node_r", "r", blockedRule_node_r_AllowedTypes, blockedRule_node_r_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternNode blockedRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "blockedRule_node_p2", "p2", blockedRule_node_p2_AllowedTypes, blockedRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge blockedRule_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "blockedRule_edge_req", "req", blockedRule_edge_req_AllowedTypes, blockedRule_edge_req_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge blockedRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "blockedRule_edge_hb", "hb", blockedRule_edge_hb_AllowedTypes, blockedRule_edge_hb_IsAllowedType, 5.5F, -1, false);
			pat_blockedRule = new GRGEN_LGSP.PatternGraph(
				"blockedRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { blockedRule_node_p1, blockedRule_node_r, blockedRule_node_p2 }, 
				new GRGEN_LGSP.PatternEdge[] { blockedRule_edge_req, blockedRule_edge_hb }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				blockedRule_minMatches,
				blockedRule_maxMatches,
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
				blockedRule_isNodeHomomorphicGlobal,
				blockedRule_isEdgeHomomorphicGlobal
			);
			pat_blockedRule.edgeToSourceNode.Add(blockedRule_edge_req, blockedRule_node_p1);
			pat_blockedRule.edgeToTargetNode.Add(blockedRule_edge_req, blockedRule_node_r);
			pat_blockedRule.edgeToSourceNode.Add(blockedRule_edge_hb, blockedRule_node_r);
			pat_blockedRule.edgeToTargetNode.Add(blockedRule_edge_hb, blockedRule_node_p2);

			blockedRule_node_p1.PointOfDefinition = pat_blockedRule;
			blockedRule_node_r.PointOfDefinition = pat_blockedRule;
			blockedRule_node_p2.PointOfDefinition = pat_blockedRule;
			blockedRule_edge_req.PointOfDefinition = pat_blockedRule;
			blockedRule_edge_hb.PointOfDefinition = pat_blockedRule;

			patternGraph = pat_blockedRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_blockedRule curMatch = (Match_blockedRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			graph.SettingAddedNodeNames( blockedRule_addedNodeNames );
			graph.SettingAddedEdgeNames( blockedRule_addedEdgeNames );
			GRGEN_MODEL.@blocked edge_b = GRGEN_MODEL.@blocked.CreateEdge(graph, node_r, node_p1);
			return;
		}
		private static string[] blockedRule_addedNodeNames = new string[] {  };
		private static string[] blockedRule_addedEdgeNames = new string[] { "b" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_blockedRule curMatch = (Match_blockedRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			graph.SettingAddedNodeNames( blockedRule_addedNodeNames );
			graph.SettingAddedEdgeNames( blockedRule_addedEdgeNames );
			GRGEN_MODEL.@blocked edge_b = GRGEN_MODEL.@blocked.CreateEdge(graph, node_r, node_p1);
			return;
		}

		static Rule_blockedRule() {
		}

		public interface IMatch_blockedRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			//Edges
			GRGEN_MODEL.Irequest edge_req { get; }
			GRGEN_MODEL.Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_blockedRule : GRGEN_LGSP.ListElement<Match_blockedRule>, IMatch_blockedRule
		{
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public enum blockedRule_NodeNums { @p1, @r, @p2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)blockedRule_NodeNums.@p1: return _node_p1;
				case (int)blockedRule_NodeNums.@r: return _node_r;
				case (int)blockedRule_NodeNums.@p2: return _node_p2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_MODEL.Iheld_by edge_hb { get { return (GRGEN_MODEL.Iheld_by)_edge_hb; } }
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public GRGEN_LGSP.LGSPEdge _edge_hb;
			public enum blockedRule_EdgeNums { @req, @hb, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)blockedRule_EdgeNums.@req: return _edge_req;
				case (int)blockedRule_EdgeNums.@hb: return _edge_hb;
				default: return null;
				}
			}
			
			public enum blockedRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum blockedRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum blockedRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum blockedRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum blockedRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_blockedRule.instance.pat_blockedRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_waitingRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_waitingRule instance = null;
		public static Rule_waitingRule Instance { get { if (instance==null) { instance = new Rule_waitingRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] waitingRule_node_r2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] waitingRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] waitingRule_node_r1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] waitingRule_node_p2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] waitingRule_node_r_AllowedTypes = null;
		public static bool[] waitingRule_node_r2_IsAllowedType = null;
		public static bool[] waitingRule_node_p1_IsAllowedType = null;
		public static bool[] waitingRule_node_r1_IsAllowedType = null;
		public static bool[] waitingRule_node_p2_IsAllowedType = null;
		public static bool[] waitingRule_node_r_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] waitingRule_edge_b_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] waitingRule_edge_hb_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] waitingRule_edge_req_AllowedTypes = null;
		public static bool[] waitingRule_edge_b_IsAllowedType = null;
		public static bool[] waitingRule_edge_hb_IsAllowedType = null;
		public static bool[] waitingRule_edge_req_IsAllowedType = null;
		public enum waitingRule_NodeNums { @r2, @p1, @r1, @p2, @r, };
		public enum waitingRule_EdgeNums { @b, @hb, @req, };
		public enum waitingRule_VariableNums { };
		public enum waitingRule_SubNums { };
		public enum waitingRule_AltNums { };
		public enum waitingRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_waitingRule;


		private Rule_waitingRule()
		{
			name = "waitingRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] waitingRule_isNodeHomomorphicGlobal = new bool[5, 5] {
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
				{ false, false, false, false, false, },
			};
			bool[,] waitingRule_isEdgeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			int[] waitingRule_minMatches = new int[0] ;
			int[] waitingRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode waitingRule_node_r2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "waitingRule_node_r2", "r2", waitingRule_node_r2_AllowedTypes, waitingRule_node_r2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode waitingRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "waitingRule_node_p1", "p1", waitingRule_node_p1_AllowedTypes, waitingRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode waitingRule_node_r1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "waitingRule_node_r1", "r1", waitingRule_node_r1_AllowedTypes, waitingRule_node_r1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode waitingRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "waitingRule_node_p2", "p2", waitingRule_node_p2_AllowedTypes, waitingRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode waitingRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "waitingRule_node_r", "r", waitingRule_node_r_AllowedTypes, waitingRule_node_r_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternEdge waitingRule_edge_b = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@blocked, "GRGEN_MODEL.Iblocked", "waitingRule_edge_b", "b", waitingRule_edge_b_AllowedTypes, waitingRule_edge_b_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge waitingRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "waitingRule_edge_hb", "hb", waitingRule_edge_hb_AllowedTypes, waitingRule_edge_hb_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge waitingRule_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "waitingRule_edge_req", "req", waitingRule_edge_req_AllowedTypes, waitingRule_edge_req_IsAllowedType, 5.5F, -1, false);
			pat_waitingRule = new GRGEN_LGSP.PatternGraph(
				"waitingRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { waitingRule_node_r2, waitingRule_node_p1, waitingRule_node_r1, waitingRule_node_p2, waitingRule_node_r }, 
				new GRGEN_LGSP.PatternEdge[] { waitingRule_edge_b, waitingRule_edge_hb, waitingRule_edge_req }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				waitingRule_minMatches,
				waitingRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[5, 5] {
					{ true, false, false, false, false, },
					{ false, true, false, false, false, },
					{ false, false, true, false, false, },
					{ false, false, false, true, false, },
					{ false, false, false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				waitingRule_isNodeHomomorphicGlobal,
				waitingRule_isEdgeHomomorphicGlobal
			);
			pat_waitingRule.edgeToSourceNode.Add(waitingRule_edge_b, waitingRule_node_r2);
			pat_waitingRule.edgeToTargetNode.Add(waitingRule_edge_b, waitingRule_node_p1);
			pat_waitingRule.edgeToSourceNode.Add(waitingRule_edge_hb, waitingRule_node_r1);
			pat_waitingRule.edgeToTargetNode.Add(waitingRule_edge_hb, waitingRule_node_p1);
			pat_waitingRule.edgeToSourceNode.Add(waitingRule_edge_req, waitingRule_node_p2);
			pat_waitingRule.edgeToTargetNode.Add(waitingRule_edge_req, waitingRule_node_r1);

			waitingRule_node_r2.PointOfDefinition = pat_waitingRule;
			waitingRule_node_p1.PointOfDefinition = pat_waitingRule;
			waitingRule_node_r1.PointOfDefinition = pat_waitingRule;
			waitingRule_node_p2.PointOfDefinition = pat_waitingRule;
			waitingRule_node_r.PointOfDefinition = pat_waitingRule;
			waitingRule_edge_b.PointOfDefinition = pat_waitingRule;
			waitingRule_edge_hb.PointOfDefinition = pat_waitingRule;
			waitingRule_edge_req.PointOfDefinition = pat_waitingRule;

			patternGraph = pat_waitingRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_waitingRule curMatch = (Match_waitingRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( waitingRule_addedNodeNames );
			graph.SettingAddedEdgeNames( waitingRule_addedEdgeNames );
			GRGEN_MODEL.@blocked edge_bn;
			if(edge_b.lgspType == GRGEN_MODEL.EdgeType_blocked.typeVar)
			{
				// re-using edge_b as edge_bn
				edge_bn = (GRGEN_MODEL.@blocked) edge_b;
				graph.ReuseEdge(edge_b, null, node_p2);
			}
			else
			{
				graph.Remove(edge_b);
				edge_bn = GRGEN_MODEL.@blocked.CreateEdge(graph, node_r2, node_p2);
			}
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return;
		}
		private static string[] waitingRule_addedNodeNames = new string[] {  };
		private static string[] waitingRule_addedEdgeNames = new string[] { "bn" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_waitingRule curMatch = (Match_waitingRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( waitingRule_addedNodeNames );
			graph.SettingAddedEdgeNames( waitingRule_addedEdgeNames );
			GRGEN_MODEL.@blocked edge_bn = GRGEN_MODEL.@blocked.CreateEdge(graph, node_r2, node_p2);
			graph.Remove(edge_b);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return;
		}

		static Rule_waitingRule() {
		}

		public interface IMatch_waitingRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r2 { get; }
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IResource node_r1 { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			GRGEN_MODEL.IResource node_r { get; }
			//Edges
			GRGEN_MODEL.Iblocked edge_b { get; }
			GRGEN_MODEL.Iheld_by edge_hb { get; }
			GRGEN_MODEL.Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_waitingRule : GRGEN_LGSP.ListElement<Match_waitingRule>, IMatch_waitingRule
		{
			public GRGEN_MODEL.IResource node_r2 { get { return (GRGEN_MODEL.IResource)_node_r2; } }
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IResource node_r1 { get { return (GRGEN_MODEL.IResource)_node_r1; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_LGSP.LGSPNode _node_r2;
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_r1;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public GRGEN_LGSP.LGSPNode _node_r;
			public enum waitingRule_NodeNums { @r2, @p1, @r1, @p2, @r, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 5;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)waitingRule_NodeNums.@r2: return _node_r2;
				case (int)waitingRule_NodeNums.@p1: return _node_p1;
				case (int)waitingRule_NodeNums.@r1: return _node_r1;
				case (int)waitingRule_NodeNums.@p2: return _node_p2;
				case (int)waitingRule_NodeNums.@r: return _node_r;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iblocked edge_b { get { return (GRGEN_MODEL.Iblocked)_edge_b; } }
			public GRGEN_MODEL.Iheld_by edge_hb { get { return (GRGEN_MODEL.Iheld_by)_edge_hb; } }
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_LGSP.LGSPEdge _edge_b;
			public GRGEN_LGSP.LGSPEdge _edge_hb;
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public enum waitingRule_EdgeNums { @b, @hb, @req, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 3;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)waitingRule_EdgeNums.@b: return _edge_b;
				case (int)waitingRule_EdgeNums.@hb: return _edge_hb;
				case (int)waitingRule_EdgeNums.@req: return _edge_req;
				default: return null;
				}
			}
			
			public enum waitingRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum waitingRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum waitingRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum waitingRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum waitingRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_waitingRule.instance.pat_waitingRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_ignoreRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ignoreRule instance = null;
		public static Rule_ignoreRule Instance { get { if (instance==null) { instance = new Rule_ignoreRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ignoreRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ignoreRule_node_p_AllowedTypes = null;
		public static bool[] ignoreRule_node_r_IsAllowedType = null;
		public static bool[] ignoreRule_node_p_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ignoreRule_edge_b_AllowedTypes = null;
		public static bool[] ignoreRule_edge_b_IsAllowedType = null;
		public enum ignoreRule_NodeNums { @r, @p, };
		public enum ignoreRule_EdgeNums { @b, };
		public enum ignoreRule_VariableNums { };
		public enum ignoreRule_SubNums { };
		public enum ignoreRule_AltNums { };
		public enum ignoreRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_ignoreRule;

		public static GRGEN_LIBGR.NodeType[] ignoreRule_neg_0_node_m_AllowedTypes = null;
		public static bool[] ignoreRule_neg_0_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ignoreRule_neg_0_edge_hb_AllowedTypes = null;
		public static bool[] ignoreRule_neg_0_edge_hb_IsAllowedType = null;
		public enum ignoreRule_neg_0_NodeNums { @m, @p, };
		public enum ignoreRule_neg_0_EdgeNums { @hb, };
		public enum ignoreRule_neg_0_VariableNums { };
		public enum ignoreRule_neg_0_SubNums { };
		public enum ignoreRule_neg_0_AltNums { };
		public enum ignoreRule_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph ignoreRule_neg_0;


		private Rule_ignoreRule()
		{
			name = "ignoreRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] ignoreRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ignoreRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ignoreRule_minMatches = new int[0] ;
			int[] ignoreRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode ignoreRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "ignoreRule_node_r", "r", ignoreRule_node_r_AllowedTypes, ignoreRule_node_r_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternNode ignoreRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "ignoreRule_node_p", "p", ignoreRule_node_p_AllowedTypes, ignoreRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge ignoreRule_edge_b = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@blocked, "GRGEN_MODEL.Iblocked", "ignoreRule_edge_b", "b", ignoreRule_edge_b_AllowedTypes, ignoreRule_edge_b_IsAllowedType, 5.5F, -1, false);
			bool[,] ignoreRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ignoreRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] ignoreRule_neg_0_minMatches = new int[0] ;
			int[] ignoreRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode ignoreRule_neg_0_node_m = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "ignoreRule_neg_0_node_m", "m", ignoreRule_neg_0_node_m_AllowedTypes, ignoreRule_neg_0_node_m_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge ignoreRule_neg_0_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "ignoreRule_neg_0_edge_hb", "hb", ignoreRule_neg_0_edge_hb_AllowedTypes, ignoreRule_neg_0_edge_hb_IsAllowedType, 5.5F, -1, false);
			ignoreRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ignoreRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { ignoreRule_neg_0_node_m, ignoreRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { ignoreRule_neg_0_edge_hb }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ignoreRule_neg_0_minMatches,
				ignoreRule_neg_0_maxMatches,
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
				ignoreRule_neg_0_isNodeHomomorphicGlobal,
				ignoreRule_neg_0_isEdgeHomomorphicGlobal
			);
			ignoreRule_neg_0.edgeToSourceNode.Add(ignoreRule_neg_0_edge_hb, ignoreRule_neg_0_node_m);
			ignoreRule_neg_0.edgeToTargetNode.Add(ignoreRule_neg_0_edge_hb, ignoreRule_node_p);

			pat_ignoreRule = new GRGEN_LGSP.PatternGraph(
				"ignoreRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { ignoreRule_node_r, ignoreRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { ignoreRule_edge_b }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				ignoreRule_minMatches,
				ignoreRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { ignoreRule_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ignoreRule_isNodeHomomorphicGlobal,
				ignoreRule_isEdgeHomomorphicGlobal
			);
			pat_ignoreRule.edgeToSourceNode.Add(ignoreRule_edge_b, ignoreRule_node_r);
			pat_ignoreRule.edgeToTargetNode.Add(ignoreRule_edge_b, ignoreRule_node_p);
			ignoreRule_neg_0.embeddingGraph = pat_ignoreRule;

			ignoreRule_node_r.PointOfDefinition = pat_ignoreRule;
			ignoreRule_node_p.PointOfDefinition = pat_ignoreRule;
			ignoreRule_edge_b.PointOfDefinition = pat_ignoreRule;
			ignoreRule_neg_0_node_m.PointOfDefinition = ignoreRule_neg_0;
			ignoreRule_neg_0_edge_hb.PointOfDefinition = ignoreRule_neg_0;

			patternGraph = pat_ignoreRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ignoreRule curMatch = (Match_ignoreRule)_curMatch;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( ignoreRule_addedNodeNames );
			graph.SettingAddedEdgeNames( ignoreRule_addedEdgeNames );
			graph.Remove(edge_b);
			return;
		}
		private static string[] ignoreRule_addedNodeNames = new string[] {  };
		private static string[] ignoreRule_addedEdgeNames = new string[] {  };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ignoreRule curMatch = (Match_ignoreRule)_curMatch;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( ignoreRule_addedNodeNames );
			graph.SettingAddedEdgeNames( ignoreRule_addedEdgeNames );
			graph.Remove(edge_b);
			return;
		}

		static Rule_ignoreRule() {
		}

		public interface IMatch_ignoreRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Iblocked edge_b { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ignoreRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_m { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ignoreRule : GRGEN_LGSP.ListElement<Match_ignoreRule>, IMatch_ignoreRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum ignoreRule_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ignoreRule_NodeNums.@r: return _node_r;
				case (int)ignoreRule_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iblocked edge_b { get { return (GRGEN_MODEL.Iblocked)_edge_b; } }
			public GRGEN_LGSP.LGSPEdge _edge_b;
			public enum ignoreRule_EdgeNums { @b, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ignoreRule_EdgeNums.@b: return _edge_b;
				default: return null;
				}
			}
			
			public enum ignoreRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ignoreRule.instance.pat_ignoreRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ignoreRule_neg_0 : GRGEN_LGSP.ListElement<Match_ignoreRule_neg_0>, IMatch_ignoreRule_neg_0
		{
			public GRGEN_MODEL.IResource node_m { get { return (GRGEN_MODEL.IResource)_node_m; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_m;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum ignoreRule_neg_0_NodeNums { @m, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ignoreRule_neg_0_NodeNums.@m: return _node_m;
				case (int)ignoreRule_neg_0_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iheld_by edge_hb { get { return (GRGEN_MODEL.Iheld_by)_edge_hb; } }
			public GRGEN_LGSP.LGSPEdge _edge_hb;
			public enum ignoreRule_neg_0_EdgeNums { @hb, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ignoreRule_neg_0_EdgeNums.@hb: return _edge_hb;
				default: return null;
				}
			}
			
			public enum ignoreRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ignoreRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ignoreRule.instance.ignoreRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_unlockRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_unlockRule instance = null;
		public static Rule_unlockRule Instance { get { if (instance==null) { instance = new Rule_unlockRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] unlockRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] unlockRule_node_p_AllowedTypes = null;
		public static bool[] unlockRule_node_r_IsAllowedType = null;
		public static bool[] unlockRule_node_p_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] unlockRule_edge_b_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] unlockRule_edge_hb_AllowedTypes = null;
		public static bool[] unlockRule_edge_b_IsAllowedType = null;
		public static bool[] unlockRule_edge_hb_IsAllowedType = null;
		public enum unlockRule_NodeNums { @r, @p, };
		public enum unlockRule_EdgeNums { @b, @hb, };
		public enum unlockRule_VariableNums { };
		public enum unlockRule_SubNums { };
		public enum unlockRule_AltNums { };
		public enum unlockRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_unlockRule;


		private Rule_unlockRule()
		{
			name = "unlockRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] unlockRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] unlockRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			int[] unlockRule_minMatches = new int[0] ;
			int[] unlockRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode unlockRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "unlockRule_node_r", "r", unlockRule_node_r_AllowedTypes, unlockRule_node_r_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternNode unlockRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "unlockRule_node_p", "p", unlockRule_node_p_AllowedTypes, unlockRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge unlockRule_edge_b = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@blocked, "GRGEN_MODEL.Iblocked", "unlockRule_edge_b", "b", unlockRule_edge_b_AllowedTypes, unlockRule_edge_b_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge unlockRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "unlockRule_edge_hb", "hb", unlockRule_edge_hb_AllowedTypes, unlockRule_edge_hb_IsAllowedType, 5.5F, -1, false);
			pat_unlockRule = new GRGEN_LGSP.PatternGraph(
				"unlockRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { unlockRule_node_r, unlockRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { unlockRule_edge_b, unlockRule_edge_hb }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				unlockRule_minMatches,
				unlockRule_maxMatches,
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
				unlockRule_isNodeHomomorphicGlobal,
				unlockRule_isEdgeHomomorphicGlobal
			);
			pat_unlockRule.edgeToSourceNode.Add(unlockRule_edge_b, unlockRule_node_r);
			pat_unlockRule.edgeToTargetNode.Add(unlockRule_edge_b, unlockRule_node_p);
			pat_unlockRule.edgeToSourceNode.Add(unlockRule_edge_hb, unlockRule_node_r);
			pat_unlockRule.edgeToTargetNode.Add(unlockRule_edge_hb, unlockRule_node_p);

			unlockRule_node_r.PointOfDefinition = pat_unlockRule;
			unlockRule_node_p.PointOfDefinition = pat_unlockRule;
			unlockRule_edge_b.PointOfDefinition = pat_unlockRule;
			unlockRule_edge_hb.PointOfDefinition = pat_unlockRule;

			patternGraph = pat_unlockRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unlockRule curMatch = (Match_unlockRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( unlockRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unlockRule_addedEdgeNames );
			GRGEN_MODEL.@release edge_rel = GRGEN_MODEL.@release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return;
		}
		private static string[] unlockRule_addedNodeNames = new string[] {  };
		private static string[] unlockRule_addedEdgeNames = new string[] { "rel" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unlockRule curMatch = (Match_unlockRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( unlockRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unlockRule_addedEdgeNames );
			GRGEN_MODEL.@release edge_rel = GRGEN_MODEL.@release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return;
		}

		static Rule_unlockRule() {
		}

		public interface IMatch_unlockRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Iblocked edge_b { get; }
			GRGEN_MODEL.Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_unlockRule : GRGEN_LGSP.ListElement<Match_unlockRule>, IMatch_unlockRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum unlockRule_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)unlockRule_NodeNums.@r: return _node_r;
				case (int)unlockRule_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iblocked edge_b { get { return (GRGEN_MODEL.Iblocked)_edge_b; } }
			public GRGEN_MODEL.Iheld_by edge_hb { get { return (GRGEN_MODEL.Iheld_by)_edge_hb; } }
			public GRGEN_LGSP.LGSPEdge _edge_b;
			public GRGEN_LGSP.LGSPEdge _edge_hb;
			public enum unlockRule_EdgeNums { @b, @hb, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)unlockRule_EdgeNums.@b: return _edge_b;
				case (int)unlockRule_EdgeNums.@hb: return _edge_hb;
				default: return null;
				}
			}
			
			public enum unlockRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unlockRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unlockRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unlockRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum unlockRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_unlockRule.instance.pat_unlockRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_requestStarRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_requestStarRule instance = null;
		public static Rule_requestStarRule Instance { get { if (instance==null) { instance = new Rule_requestStarRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] requestStarRule_node_r1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] requestStarRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] requestStarRule_node_p2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] requestStarRule_node_r2_AllowedTypes = null;
		public static bool[] requestStarRule_node_r1_IsAllowedType = null;
		public static bool[] requestStarRule_node_p1_IsAllowedType = null;
		public static bool[] requestStarRule_node_p2_IsAllowedType = null;
		public static bool[] requestStarRule_node_r2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] requestStarRule_edge_h1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] requestStarRule_edge_n_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] requestStarRule_edge_h2_AllowedTypes = null;
		public static bool[] requestStarRule_edge_h1_IsAllowedType = null;
		public static bool[] requestStarRule_edge_n_IsAllowedType = null;
		public static bool[] requestStarRule_edge_h2_IsAllowedType = null;
		public enum requestStarRule_NodeNums { @r1, @p1, @p2, @r2, };
		public enum requestStarRule_EdgeNums { @h1, @n, @h2, };
		public enum requestStarRule_VariableNums { };
		public enum requestStarRule_SubNums { };
		public enum requestStarRule_AltNums { };
		public enum requestStarRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_requestStarRule;

		public static GRGEN_LIBGR.EdgeType[] requestStarRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] requestStarRule_neg_0_edge_req_IsAllowedType = null;
		public enum requestStarRule_neg_0_NodeNums { @p1, @r2, };
		public enum requestStarRule_neg_0_EdgeNums { @req, };
		public enum requestStarRule_neg_0_VariableNums { };
		public enum requestStarRule_neg_0_SubNums { };
		public enum requestStarRule_neg_0_AltNums { };
		public enum requestStarRule_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph requestStarRule_neg_0;


		private Rule_requestStarRule()
		{
			name = "requestStarRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] requestStarRule_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] requestStarRule_isEdgeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			int[] requestStarRule_minMatches = new int[0] ;
			int[] requestStarRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode requestStarRule_node_r1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "requestStarRule_node_r1", "r1", requestStarRule_node_r1_AllowedTypes, requestStarRule_node_r1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode requestStarRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "requestStarRule_node_p1", "p1", requestStarRule_node_p1_AllowedTypes, requestStarRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode requestStarRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "requestStarRule_node_p2", "p2", requestStarRule_node_p2_AllowedTypes, requestStarRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode requestStarRule_node_r2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "requestStarRule_node_r2", "r2", requestStarRule_node_r2_AllowedTypes, requestStarRule_node_r2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge requestStarRule_edge_h1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "requestStarRule_edge_h1", "h1", requestStarRule_edge_h1_AllowedTypes, requestStarRule_edge_h1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge requestStarRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@next, "GRGEN_MODEL.Inext", "requestStarRule_edge_n", "n", requestStarRule_edge_n_AllowedTypes, requestStarRule_edge_n_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge requestStarRule_edge_h2 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "requestStarRule_edge_h2", "h2", requestStarRule_edge_h2_AllowedTypes, requestStarRule_edge_h2_IsAllowedType, 5.5F, -1, false);
			bool[,] requestStarRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestStarRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] requestStarRule_neg_0_minMatches = new int[0] ;
			int[] requestStarRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge requestStarRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "requestStarRule_neg_0_edge_req", "req", requestStarRule_neg_0_edge_req_AllowedTypes, requestStarRule_neg_0_edge_req_IsAllowedType, 5.5F, -1, false);
			requestStarRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"requestStarRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { requestStarRule_node_p1, requestStarRule_node_r2 }, 
				new GRGEN_LGSP.PatternEdge[] { requestStarRule_neg_0_edge_req }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				requestStarRule_neg_0_minMatches,
				requestStarRule_neg_0_maxMatches,
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
				requestStarRule_neg_0_isNodeHomomorphicGlobal,
				requestStarRule_neg_0_isEdgeHomomorphicGlobal
			);
			requestStarRule_neg_0.edgeToSourceNode.Add(requestStarRule_neg_0_edge_req, requestStarRule_node_p1);
			requestStarRule_neg_0.edgeToTargetNode.Add(requestStarRule_neg_0_edge_req, requestStarRule_node_r2);

			pat_requestStarRule = new GRGEN_LGSP.PatternGraph(
				"requestStarRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { requestStarRule_node_r1, requestStarRule_node_p1, requestStarRule_node_p2, requestStarRule_node_r2 }, 
				new GRGEN_LGSP.PatternEdge[] { requestStarRule_edge_h1, requestStarRule_edge_n, requestStarRule_edge_h2 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				requestStarRule_minMatches,
				requestStarRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { requestStarRule_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				requestStarRule_isNodeHomomorphicGlobal,
				requestStarRule_isEdgeHomomorphicGlobal
			);
			pat_requestStarRule.edgeToSourceNode.Add(requestStarRule_edge_h1, requestStarRule_node_r1);
			pat_requestStarRule.edgeToTargetNode.Add(requestStarRule_edge_h1, requestStarRule_node_p1);
			pat_requestStarRule.edgeToSourceNode.Add(requestStarRule_edge_n, requestStarRule_node_p2);
			pat_requestStarRule.edgeToTargetNode.Add(requestStarRule_edge_n, requestStarRule_node_p1);
			pat_requestStarRule.edgeToSourceNode.Add(requestStarRule_edge_h2, requestStarRule_node_r2);
			pat_requestStarRule.edgeToTargetNode.Add(requestStarRule_edge_h2, requestStarRule_node_p2);
			requestStarRule_neg_0.embeddingGraph = pat_requestStarRule;

			requestStarRule_node_r1.PointOfDefinition = pat_requestStarRule;
			requestStarRule_node_p1.PointOfDefinition = pat_requestStarRule;
			requestStarRule_node_p2.PointOfDefinition = pat_requestStarRule;
			requestStarRule_node_r2.PointOfDefinition = pat_requestStarRule;
			requestStarRule_edge_h1.PointOfDefinition = pat_requestStarRule;
			requestStarRule_edge_n.PointOfDefinition = pat_requestStarRule;
			requestStarRule_edge_h2.PointOfDefinition = pat_requestStarRule;
			requestStarRule_neg_0_edge_req.PointOfDefinition = requestStarRule_neg_0;

			patternGraph = pat_requestStarRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestStarRule curMatch = (Match_requestStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			graph.SettingAddedNodeNames( requestStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestStarRule_addedEdgeNames );
			GRGEN_MODEL.@request edge_req = GRGEN_MODEL.@request.CreateEdge(graph, node_p1, node_r2);
			return;
		}
		private static string[] requestStarRule_addedNodeNames = new string[] {  };
		private static string[] requestStarRule_addedEdgeNames = new string[] { "req" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestStarRule curMatch = (Match_requestStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			graph.SettingAddedNodeNames( requestStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestStarRule_addedEdgeNames );
			GRGEN_MODEL.@request edge_req = GRGEN_MODEL.@request.CreateEdge(graph, node_p1, node_r2);
			return;
		}

		static Rule_requestStarRule() {
		}

		public interface IMatch_requestStarRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r1 { get; }
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			GRGEN_MODEL.IResource node_r2 { get; }
			//Edges
			GRGEN_MODEL.Iheld_by edge_h1 { get; }
			GRGEN_MODEL.Inext edge_n { get; }
			GRGEN_MODEL.Iheld_by edge_h2 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_requestStarRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IResource node_r2 { get; }
			//Edges
			GRGEN_MODEL.Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_requestStarRule : GRGEN_LGSP.ListElement<Match_requestStarRule>, IMatch_requestStarRule
		{
			public GRGEN_MODEL.IResource node_r1 { get { return (GRGEN_MODEL.IResource)_node_r1; } }
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_MODEL.IResource node_r2 { get { return (GRGEN_MODEL.IResource)_node_r2; } }
			public GRGEN_LGSP.LGSPNode _node_r1;
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public GRGEN_LGSP.LGSPNode _node_r2;
			public enum requestStarRule_NodeNums { @r1, @p1, @p2, @r2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)requestStarRule_NodeNums.@r1: return _node_r1;
				case (int)requestStarRule_NodeNums.@p1: return _node_p1;
				case (int)requestStarRule_NodeNums.@p2: return _node_p2;
				case (int)requestStarRule_NodeNums.@r2: return _node_r2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iheld_by edge_h1 { get { return (GRGEN_MODEL.Iheld_by)_edge_h1; } }
			public GRGEN_MODEL.Inext edge_n { get { return (GRGEN_MODEL.Inext)_edge_n; } }
			public GRGEN_MODEL.Iheld_by edge_h2 { get { return (GRGEN_MODEL.Iheld_by)_edge_h2; } }
			public GRGEN_LGSP.LGSPEdge _edge_h1;
			public GRGEN_LGSP.LGSPEdge _edge_n;
			public GRGEN_LGSP.LGSPEdge _edge_h2;
			public enum requestStarRule_EdgeNums { @h1, @n, @h2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 3;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)requestStarRule_EdgeNums.@h1: return _edge_h1;
				case (int)requestStarRule_EdgeNums.@n: return _edge_n;
				case (int)requestStarRule_EdgeNums.@h2: return _edge_h2;
				default: return null;
				}
			}
			
			public enum requestStarRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_requestStarRule.instance.pat_requestStarRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestStarRule_neg_0 : GRGEN_LGSP.ListElement<Match_requestStarRule_neg_0>, IMatch_requestStarRule_neg_0
		{
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IResource node_r2 { get { return (GRGEN_MODEL.IResource)_node_r2; } }
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_r2;
			public enum requestStarRule_neg_0_NodeNums { @p1, @r2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)requestStarRule_neg_0_NodeNums.@p1: return _node_p1;
				case (int)requestStarRule_neg_0_NodeNums.@r2: return _node_r2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public enum requestStarRule_neg_0_EdgeNums { @req, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)requestStarRule_neg_0_EdgeNums.@req: return _edge_req;
				default: return null;
				}
			}
			
			public enum requestStarRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestStarRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_requestStarRule.instance.requestStarRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_releaseStarRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_releaseStarRule instance = null;
		public static Rule_releaseStarRule Instance { get { if (instance==null) { instance = new Rule_releaseStarRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] releaseStarRule_node_p1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] releaseStarRule_node_r1_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] releaseStarRule_node_p2_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] releaseStarRule_node_r2_AllowedTypes = null;
		public static bool[] releaseStarRule_node_p1_IsAllowedType = null;
		public static bool[] releaseStarRule_node_r1_IsAllowedType = null;
		public static bool[] releaseStarRule_node_p2_IsAllowedType = null;
		public static bool[] releaseStarRule_node_r2_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] releaseStarRule_edge_rq_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] releaseStarRule_edge_h1_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] releaseStarRule_edge_h2_AllowedTypes = null;
		public static bool[] releaseStarRule_edge_rq_IsAllowedType = null;
		public static bool[] releaseStarRule_edge_h1_IsAllowedType = null;
		public static bool[] releaseStarRule_edge_h2_IsAllowedType = null;
		public enum releaseStarRule_NodeNums { @p1, @r1, @p2, @r2, };
		public enum releaseStarRule_EdgeNums { @rq, @h1, @h2, };
		public enum releaseStarRule_VariableNums { };
		public enum releaseStarRule_SubNums { };
		public enum releaseStarRule_AltNums { };
		public enum releaseStarRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_releaseStarRule;


		private Rule_releaseStarRule()
		{
			name = "releaseStarRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] releaseStarRule_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] releaseStarRule_isEdgeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			int[] releaseStarRule_minMatches = new int[0] ;
			int[] releaseStarRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode releaseStarRule_node_p1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "releaseStarRule_node_p1", "p1", releaseStarRule_node_p1_AllowedTypes, releaseStarRule_node_p1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode releaseStarRule_node_r1 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "releaseStarRule_node_r1", "r1", releaseStarRule_node_r1_AllowedTypes, releaseStarRule_node_r1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode releaseStarRule_node_p2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "releaseStarRule_node_p2", "p2", releaseStarRule_node_p2_AllowedTypes, releaseStarRule_node_p2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternNode releaseStarRule_node_r2 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "releaseStarRule_node_r2", "r2", releaseStarRule_node_r2_AllowedTypes, releaseStarRule_node_r2_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge releaseStarRule_edge_rq = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "releaseStarRule_edge_rq", "rq", releaseStarRule_edge_rq_AllowedTypes, releaseStarRule_edge_rq_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge releaseStarRule_edge_h1 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "releaseStarRule_edge_h1", "h1", releaseStarRule_edge_h1_AllowedTypes, releaseStarRule_edge_h1_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge releaseStarRule_edge_h2 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "releaseStarRule_edge_h2", "h2", releaseStarRule_edge_h2_AllowedTypes, releaseStarRule_edge_h2_IsAllowedType, 5.5F, -1, false);
			pat_releaseStarRule = new GRGEN_LGSP.PatternGraph(
				"releaseStarRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { releaseStarRule_node_p1, releaseStarRule_node_r1, releaseStarRule_node_p2, releaseStarRule_node_r2 }, 
				new GRGEN_LGSP.PatternEdge[] { releaseStarRule_edge_rq, releaseStarRule_edge_h1, releaseStarRule_edge_h2 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				releaseStarRule_minMatches,
				releaseStarRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				releaseStarRule_isNodeHomomorphicGlobal,
				releaseStarRule_isEdgeHomomorphicGlobal
			);
			pat_releaseStarRule.edgeToSourceNode.Add(releaseStarRule_edge_rq, releaseStarRule_node_p1);
			pat_releaseStarRule.edgeToTargetNode.Add(releaseStarRule_edge_rq, releaseStarRule_node_r1);
			pat_releaseStarRule.edgeToSourceNode.Add(releaseStarRule_edge_h1, releaseStarRule_node_r1);
			pat_releaseStarRule.edgeToTargetNode.Add(releaseStarRule_edge_h1, releaseStarRule_node_p2);
			pat_releaseStarRule.edgeToSourceNode.Add(releaseStarRule_edge_h2, releaseStarRule_node_r2);
			pat_releaseStarRule.edgeToTargetNode.Add(releaseStarRule_edge_h2, releaseStarRule_node_p2);

			releaseStarRule_node_p1.PointOfDefinition = pat_releaseStarRule;
			releaseStarRule_node_r1.PointOfDefinition = pat_releaseStarRule;
			releaseStarRule_node_p2.PointOfDefinition = pat_releaseStarRule;
			releaseStarRule_node_r2.PointOfDefinition = pat_releaseStarRule;
			releaseStarRule_edge_rq.PointOfDefinition = pat_releaseStarRule;
			releaseStarRule_edge_h1.PointOfDefinition = pat_releaseStarRule;
			releaseStarRule_edge_h2.PointOfDefinition = pat_releaseStarRule;

			patternGraph = pat_releaseStarRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseStarRule curMatch = (Match_releaseStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r1 = curMatch._node_r1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_h1 = curMatch._edge_h1;
			graph.SettingAddedNodeNames( releaseStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseStarRule_addedEdgeNames );
			GRGEN_MODEL.@release edge_rl = GRGEN_MODEL.@release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return;
		}
		private static string[] releaseStarRule_addedNodeNames = new string[] {  };
		private static string[] releaseStarRule_addedEdgeNames = new string[] { "rl" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseStarRule curMatch = (Match_releaseStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r1 = curMatch._node_r1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_h1 = curMatch._edge_h1;
			graph.SettingAddedNodeNames( releaseStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseStarRule_addedEdgeNames );
			GRGEN_MODEL.@release edge_rl = GRGEN_MODEL.@release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return;
		}

		static Rule_releaseStarRule() {
		}

		public interface IMatch_releaseStarRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p1 { get; }
			GRGEN_MODEL.IResource node_r1 { get; }
			GRGEN_MODEL.IProcess node_p2 { get; }
			GRGEN_MODEL.IResource node_r2 { get; }
			//Edges
			GRGEN_MODEL.Irequest edge_rq { get; }
			GRGEN_MODEL.Iheld_by edge_h1 { get; }
			GRGEN_MODEL.Iheld_by edge_h2 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_releaseStarRule : GRGEN_LGSP.ListElement<Match_releaseStarRule>, IMatch_releaseStarRule
		{
			public GRGEN_MODEL.IProcess node_p1 { get { return (GRGEN_MODEL.IProcess)_node_p1; } }
			public GRGEN_MODEL.IResource node_r1 { get { return (GRGEN_MODEL.IResource)_node_r1; } }
			public GRGEN_MODEL.IProcess node_p2 { get { return (GRGEN_MODEL.IProcess)_node_p2; } }
			public GRGEN_MODEL.IResource node_r2 { get { return (GRGEN_MODEL.IResource)_node_r2; } }
			public GRGEN_LGSP.LGSPNode _node_p1;
			public GRGEN_LGSP.LGSPNode _node_r1;
			public GRGEN_LGSP.LGSPNode _node_p2;
			public GRGEN_LGSP.LGSPNode _node_r2;
			public enum releaseStarRule_NodeNums { @p1, @r1, @p2, @r2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)releaseStarRule_NodeNums.@p1: return _node_p1;
				case (int)releaseStarRule_NodeNums.@r1: return _node_r1;
				case (int)releaseStarRule_NodeNums.@p2: return _node_p2;
				case (int)releaseStarRule_NodeNums.@r2: return _node_r2;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irequest edge_rq { get { return (GRGEN_MODEL.Irequest)_edge_rq; } }
			public GRGEN_MODEL.Iheld_by edge_h1 { get { return (GRGEN_MODEL.Iheld_by)_edge_h1; } }
			public GRGEN_MODEL.Iheld_by edge_h2 { get { return (GRGEN_MODEL.Iheld_by)_edge_h2; } }
			public GRGEN_LGSP.LGSPEdge _edge_rq;
			public GRGEN_LGSP.LGSPEdge _edge_h1;
			public GRGEN_LGSP.LGSPEdge _edge_h2;
			public enum releaseStarRule_EdgeNums { @rq, @h1, @h2, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 3;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)releaseStarRule_EdgeNums.@rq: return _edge_rq;
				case (int)releaseStarRule_EdgeNums.@h1: return _edge_h1;
				case (int)releaseStarRule_EdgeNums.@h2: return _edge_h2;
				default: return null;
				}
			}
			
			public enum releaseStarRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseStarRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseStarRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseStarRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum releaseStarRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_releaseStarRule.instance.pat_releaseStarRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_requestSimpleRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_requestSimpleRule instance = null;
		public static Rule_requestSimpleRule Instance { get { if (instance==null) { instance = new Rule_requestSimpleRule(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] requestSimpleRule_node_r_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] requestSimpleRule_node_p_AllowedTypes = null;
		public static bool[] requestSimpleRule_node_r_IsAllowedType = null;
		public static bool[] requestSimpleRule_node_p_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] requestSimpleRule_edge_t_AllowedTypes = null;
		public static bool[] requestSimpleRule_edge_t_IsAllowedType = null;
		public enum requestSimpleRule_NodeNums { @r, @p, };
		public enum requestSimpleRule_EdgeNums { @t, };
		public enum requestSimpleRule_VariableNums { };
		public enum requestSimpleRule_SubNums { };
		public enum requestSimpleRule_AltNums { };
		public enum requestSimpleRule_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_requestSimpleRule;

		public static GRGEN_LIBGR.EdgeType[] requestSimpleRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] requestSimpleRule_neg_0_edge_req_IsAllowedType = null;
		public enum requestSimpleRule_neg_0_NodeNums { @p, @r, };
		public enum requestSimpleRule_neg_0_EdgeNums { @req, };
		public enum requestSimpleRule_neg_0_VariableNums { };
		public enum requestSimpleRule_neg_0_SubNums { };
		public enum requestSimpleRule_neg_0_AltNums { };
		public enum requestSimpleRule_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph requestSimpleRule_neg_0;


		private Rule_requestSimpleRule()
		{
			name = "requestSimpleRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] requestSimpleRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestSimpleRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] requestSimpleRule_minMatches = new int[0] ;
			int[] requestSimpleRule_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode requestSimpleRule_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "requestSimpleRule_node_r", "r", requestSimpleRule_node_r_AllowedTypes, requestSimpleRule_node_r_IsAllowedType, 1.0F, -1, false);
			GRGEN_LGSP.PatternNode requestSimpleRule_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "requestSimpleRule_node_p", "p", requestSimpleRule_node_p_AllowedTypes, requestSimpleRule_node_p_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge requestSimpleRule_edge_t = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@token, "GRGEN_MODEL.Itoken", "requestSimpleRule_edge_t", "t", requestSimpleRule_edge_t_AllowedTypes, requestSimpleRule_edge_t_IsAllowedType, 5.5F, -1, false);
			bool[,] requestSimpleRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestSimpleRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] requestSimpleRule_neg_0_minMatches = new int[0] ;
			int[] requestSimpleRule_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternEdge requestSimpleRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@request, "GRGEN_MODEL.Irequest", "requestSimpleRule_neg_0_edge_req", "req", requestSimpleRule_neg_0_edge_req_AllowedTypes, requestSimpleRule_neg_0_edge_req_IsAllowedType, 5.5F, -1, false);
			requestSimpleRule_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"requestSimpleRule_",
				false,
				new GRGEN_LGSP.PatternNode[] { requestSimpleRule_node_p, requestSimpleRule_node_r }, 
				new GRGEN_LGSP.PatternEdge[] { requestSimpleRule_neg_0_edge_req }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				requestSimpleRule_neg_0_minMatches,
				requestSimpleRule_neg_0_maxMatches,
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
				requestSimpleRule_neg_0_isNodeHomomorphicGlobal,
				requestSimpleRule_neg_0_isEdgeHomomorphicGlobal
			);
			requestSimpleRule_neg_0.edgeToSourceNode.Add(requestSimpleRule_neg_0_edge_req, requestSimpleRule_node_p);
			requestSimpleRule_neg_0.edgeToTargetNode.Add(requestSimpleRule_neg_0_edge_req, requestSimpleRule_node_r);

			pat_requestSimpleRule = new GRGEN_LGSP.PatternGraph(
				"requestSimpleRule",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { requestSimpleRule_node_r, requestSimpleRule_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { requestSimpleRule_edge_t }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				requestSimpleRule_minMatches,
				requestSimpleRule_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { requestSimpleRule_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				requestSimpleRule_isNodeHomomorphicGlobal,
				requestSimpleRule_isEdgeHomomorphicGlobal
			);
			pat_requestSimpleRule.edgeToSourceNode.Add(requestSimpleRule_edge_t, requestSimpleRule_node_r);
			pat_requestSimpleRule.edgeToTargetNode.Add(requestSimpleRule_edge_t, requestSimpleRule_node_p);
			requestSimpleRule_neg_0.embeddingGraph = pat_requestSimpleRule;

			requestSimpleRule_node_r.PointOfDefinition = pat_requestSimpleRule;
			requestSimpleRule_node_p.PointOfDefinition = pat_requestSimpleRule;
			requestSimpleRule_edge_t.PointOfDefinition = pat_requestSimpleRule;
			requestSimpleRule_neg_0_edge_req.PointOfDefinition = requestSimpleRule_neg_0;

			patternGraph = pat_requestSimpleRule;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestSimpleRule curMatch = (Match_requestSimpleRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestSimpleRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestSimpleRule_addedEdgeNames );
			GRGEN_MODEL.@request edge_req = GRGEN_MODEL.@request.CreateEdge(graph, node_p, node_r);
			return;
		}
		private static string[] requestSimpleRule_addedNodeNames = new string[] {  };
		private static string[] requestSimpleRule_addedEdgeNames = new string[] { "req" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestSimpleRule curMatch = (Match_requestSimpleRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestSimpleRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestSimpleRule_addedEdgeNames );
			GRGEN_MODEL.@request edge_req = GRGEN_MODEL.@request.CreateEdge(graph, node_p, node_r);
			return;
		}

		static Rule_requestSimpleRule() {
		}

		public interface IMatch_requestSimpleRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Itoken edge_t { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_requestSimpleRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p { get; }
			GRGEN_MODEL.IResource node_r { get; }
			//Edges
			GRGEN_MODEL.Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_requestSimpleRule : GRGEN_LGSP.ListElement<Match_requestSimpleRule>, IMatch_requestSimpleRule
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum requestSimpleRule_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)requestSimpleRule_NodeNums.@r: return _node_r;
				case (int)requestSimpleRule_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Itoken edge_t { get { return (GRGEN_MODEL.Itoken)_edge_t; } }
			public GRGEN_LGSP.LGSPEdge _edge_t;
			public enum requestSimpleRule_EdgeNums { @t, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)requestSimpleRule_EdgeNums.@t: return _edge_t;
				default: return null;
				}
			}
			
			public enum requestSimpleRule_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_requestSimpleRule.instance.pat_requestSimpleRule; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestSimpleRule_neg_0 : GRGEN_LGSP.ListElement<Match_requestSimpleRule_neg_0>, IMatch_requestSimpleRule_neg_0
		{
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_LGSP.LGSPNode _node_p;
			public GRGEN_LGSP.LGSPNode _node_r;
			public enum requestSimpleRule_neg_0_NodeNums { @p, @r, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)requestSimpleRule_neg_0_NodeNums.@p: return _node_p;
				case (int)requestSimpleRule_neg_0_NodeNums.@r: return _node_r;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Irequest edge_req { get { return (GRGEN_MODEL.Irequest)_edge_req; } }
			public GRGEN_LGSP.LGSPEdge _edge_req;
			public enum requestSimpleRule_neg_0_EdgeNums { @req, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)requestSimpleRule_neg_0_EdgeNums.@req: return _edge_req;
				default: return null;
				}
			}
			
			public enum requestSimpleRule_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum requestSimpleRule_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_requestSimpleRule.instance.requestSimpleRule_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_aux_attachResource : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_aux_attachResource instance = null;
		public static Rule_aux_attachResource Instance { get { if (instance==null) { instance = new Rule_aux_attachResource(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] aux_attachResource_node_p_AllowedTypes = null;
		public static bool[] aux_attachResource_node_p_IsAllowedType = null;
		public enum aux_attachResource_NodeNums { @p, };
		public enum aux_attachResource_EdgeNums { };
		public enum aux_attachResource_VariableNums { };
		public enum aux_attachResource_SubNums { };
		public enum aux_attachResource_AltNums { };
		public enum aux_attachResource_IterNums { };




		public GRGEN_LGSP.PatternGraph pat_aux_attachResource;

		public static GRGEN_LIBGR.NodeType[] aux_attachResource_neg_0_node_r_AllowedTypes = null;
		public static bool[] aux_attachResource_neg_0_node_r_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] aux_attachResource_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] aux_attachResource_neg_0_edge__edge0_IsAllowedType = null;
		public enum aux_attachResource_neg_0_NodeNums { @r, @p, };
		public enum aux_attachResource_neg_0_EdgeNums { @_edge0, };
		public enum aux_attachResource_neg_0_VariableNums { };
		public enum aux_attachResource_neg_0_SubNums { };
		public enum aux_attachResource_neg_0_AltNums { };
		public enum aux_attachResource_neg_0_IterNums { };

		public GRGEN_LGSP.PatternGraph aux_attachResource_neg_0;


		private Rule_aux_attachResource()
		{
			name = "aux_attachResource";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		private void initialize()
		{
			bool[,] aux_attachResource_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] aux_attachResource_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			int[] aux_attachResource_minMatches = new int[0] ;
			int[] aux_attachResource_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode aux_attachResource_node_p = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Process, "GRGEN_MODEL.IProcess", "aux_attachResource_node_p", "p", aux_attachResource_node_p_AllowedTypes, aux_attachResource_node_p_IsAllowedType, 5.5F, -1, false);
			bool[,] aux_attachResource_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] aux_attachResource_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			int[] aux_attachResource_neg_0_minMatches = new int[0] ;
			int[] aux_attachResource_neg_0_maxMatches = new int[0] ;
			GRGEN_LGSP.PatternNode aux_attachResource_neg_0_node_r = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Resource, "GRGEN_MODEL.IResource", "aux_attachResource_neg_0_node_r", "r", aux_attachResource_neg_0_node_r_AllowedTypes, aux_attachResource_neg_0_node_r_IsAllowedType, 5.5F, -1, false);
			GRGEN_LGSP.PatternEdge aux_attachResource_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@held_by, "GRGEN_MODEL.Iheld_by", "aux_attachResource_neg_0_edge__edge0", "_edge0", aux_attachResource_neg_0_edge__edge0_AllowedTypes, aux_attachResource_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false);
			aux_attachResource_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"aux_attachResource_",
				false,
				new GRGEN_LGSP.PatternNode[] { aux_attachResource_neg_0_node_r, aux_attachResource_node_p }, 
				new GRGEN_LGSP.PatternEdge[] { aux_attachResource_neg_0_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				aux_attachResource_neg_0_minMatches,
				aux_attachResource_neg_0_maxMatches,
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
				aux_attachResource_neg_0_isNodeHomomorphicGlobal,
				aux_attachResource_neg_0_isEdgeHomomorphicGlobal
			);
			aux_attachResource_neg_0.edgeToSourceNode.Add(aux_attachResource_neg_0_edge__edge0, aux_attachResource_neg_0_node_r);
			aux_attachResource_neg_0.edgeToTargetNode.Add(aux_attachResource_neg_0_edge__edge0, aux_attachResource_node_p);

			pat_aux_attachResource = new GRGEN_LGSP.PatternGraph(
				"aux_attachResource",
				"",
				false,
				new GRGEN_LGSP.PatternNode[] { aux_attachResource_node_p }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				aux_attachResource_minMatches,
				aux_attachResource_maxMatches,
				new GRGEN_LGSP.PatternGraph[] { aux_attachResource_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				aux_attachResource_isNodeHomomorphicGlobal,
				aux_attachResource_isEdgeHomomorphicGlobal
			);
			aux_attachResource_neg_0.embeddingGraph = pat_aux_attachResource;

			aux_attachResource_node_p.PointOfDefinition = pat_aux_attachResource;
			aux_attachResource_neg_0_node_r.PointOfDefinition = aux_attachResource_neg_0;
			aux_attachResource_neg_0_edge__edge0.PointOfDefinition = aux_attachResource_neg_0;

			patternGraph = pat_aux_attachResource;
		}


		public void Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_aux_attachResource curMatch = (Match_aux_attachResource)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( aux_attachResource_addedNodeNames );
			GRGEN_MODEL.@Resource node_r = GRGEN_MODEL.@Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( aux_attachResource_addedEdgeNames );
			GRGEN_MODEL.@held_by edge__edge0 = GRGEN_MODEL.@held_by.CreateEdge(graph, node_r, node_p);
			return;
		}
		private static string[] aux_attachResource_addedNodeNames = new string[] { "r" };
		private static string[] aux_attachResource_addedEdgeNames = new string[] { "_edge0" };

		public void ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_aux_attachResource curMatch = (Match_aux_attachResource)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( aux_attachResource_addedNodeNames );
			GRGEN_MODEL.@Resource node_r = GRGEN_MODEL.@Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( aux_attachResource_addedEdgeNames );
			GRGEN_MODEL.@held_by edge__edge0 = GRGEN_MODEL.@held_by.CreateEdge(graph, node_r, node_p);
			return;
		}

		static Rule_aux_attachResource() {
		}

		public interface IMatch_aux_attachResource : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_aux_attachResource_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IResource node_r { get; }
			GRGEN_MODEL.IProcess node_p { get; }
			//Edges
			GRGEN_MODEL.Iheld_by edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_aux_attachResource : GRGEN_LGSP.ListElement<Match_aux_attachResource>, IMatch_aux_attachResource
		{
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum aux_attachResource_NodeNums { @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)aux_attachResource_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public enum aux_attachResource_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_aux_attachResource.instance.pat_aux_attachResource; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_aux_attachResource_neg_0 : GRGEN_LGSP.ListElement<Match_aux_attachResource_neg_0>, IMatch_aux_attachResource_neg_0
		{
			public GRGEN_MODEL.IResource node_r { get { return (GRGEN_MODEL.IResource)_node_r; } }
			public GRGEN_MODEL.IProcess node_p { get { return (GRGEN_MODEL.IProcess)_node_p; } }
			public GRGEN_LGSP.LGSPNode _node_r;
			public GRGEN_LGSP.LGSPNode _node_p;
			public enum aux_attachResource_neg_0_NodeNums { @r, @p, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)aux_attachResource_neg_0_NodeNums.@r: return _node_r;
				case (int)aux_attachResource_neg_0_NodeNums.@p: return _node_p;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.Iheld_by edge__edge0 { get { return (GRGEN_MODEL.Iheld_by)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum aux_attachResource_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)aux_attachResource_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum aux_attachResource_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum aux_attachResource_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_aux_attachResource.instance.aux_attachResource_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class MutexPimped_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public MutexPimped_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[17];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+17];
			rules[0] = Rule_newRule.Instance;
			rulesAndSubpatterns[0+0] = Rule_newRule.Instance;
			rules[1] = Rule_killRule.Instance;
			rulesAndSubpatterns[0+1] = Rule_killRule.Instance;
			rules[2] = Rule_mountRule.Instance;
			rulesAndSubpatterns[0+2] = Rule_mountRule.Instance;
			rules[3] = Rule_unmountRule.Instance;
			rulesAndSubpatterns[0+3] = Rule_unmountRule.Instance;
			rules[4] = Rule_passRule.Instance;
			rulesAndSubpatterns[0+4] = Rule_passRule.Instance;
			rules[5] = Rule_requestRule.Instance;
			rulesAndSubpatterns[0+5] = Rule_requestRule.Instance;
			rules[6] = Rule_takeRule.Instance;
			rulesAndSubpatterns[0+6] = Rule_takeRule.Instance;
			rules[7] = Rule_releaseRule.Instance;
			rulesAndSubpatterns[0+7] = Rule_releaseRule.Instance;
			rules[8] = Rule_giveRule.Instance;
			rulesAndSubpatterns[0+8] = Rule_giveRule.Instance;
			rules[9] = Rule_blockedRule.Instance;
			rulesAndSubpatterns[0+9] = Rule_blockedRule.Instance;
			rules[10] = Rule_waitingRule.Instance;
			rulesAndSubpatterns[0+10] = Rule_waitingRule.Instance;
			rules[11] = Rule_ignoreRule.Instance;
			rulesAndSubpatterns[0+11] = Rule_ignoreRule.Instance;
			rules[12] = Rule_unlockRule.Instance;
			rulesAndSubpatterns[0+12] = Rule_unlockRule.Instance;
			rules[13] = Rule_requestStarRule.Instance;
			rulesAndSubpatterns[0+13] = Rule_requestStarRule.Instance;
			rules[14] = Rule_releaseStarRule.Instance;
			rulesAndSubpatterns[0+14] = Rule_releaseStarRule.Instance;
			rules[15] = Rule_requestSimpleRule.Instance;
			rulesAndSubpatterns[0+15] = Rule_requestSimpleRule.Instance;
			rules[16] = Rule_aux_attachResource.Instance;
			rulesAndSubpatterns[0+16] = Rule_aux_attachResource.Instance;
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
    public interface IAction_newRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_newRule.IMatch_newRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> matches);
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
    
    public class Action_newRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_newRule
    {
        public Action_newRule() {
            _rulePattern = Rule_newRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_newRule.Match_newRule, Rule_newRule.IMatch_newRule>(this);
        }

        public Rule_newRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "newRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_newRule.Match_newRule, Rule_newRule.IMatch_newRule> matches;

        public static Action_newRule Instance { get { return instance; } }
        private static Action_newRule instance = new Action_newRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup newRule_edge_n 
            int type_id_candidate_newRule_edge_n = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_newRule_edge_n = graph.edgesByTypeHeads[type_id_candidate_newRule_edge_n], candidate_newRule_edge_n = head_candidate_newRule_edge_n.lgspTypeNext; candidate_newRule_edge_n != head_candidate_newRule_edge_n; candidate_newRule_edge_n = candidate_newRule_edge_n.lgspTypeNext)
            {
                // Implicit Source newRule_node_p1 from newRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_newRule_node_p1 = candidate_newRule_edge_n.lgspSource;
                if(candidate_newRule_node_p1.lgspType.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_newRule_node_p1;
                prev__candidate_newRule_node_p1 = candidate_newRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_newRule_node_p1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target newRule_node_p2 from newRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_newRule_node_p2 = candidate_newRule_edge_n.lgspTarget;
                if(candidate_newRule_node_p2.lgspType.TypeID!=1) {
                    candidate_newRule_node_p1.lgspFlags = candidate_newRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
                    continue;
                }
                if((candidate_newRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_newRule_node_p1.lgspFlags = candidate_newRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
                    continue;
                }
                Rule_newRule.Match_newRule match = matches.GetNextUnfilledPosition();
                match._node_p1 = candidate_newRule_node_p1;
                match._node_p2 = candidate_newRule_node_p2;
                match._edge_n = candidate_newRule_edge_n;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_newRule_edge_n);
                    candidate_newRule_node_p1.lgspFlags = candidate_newRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
                    return matches;
                }
                candidate_newRule_node_p1.lgspFlags = candidate_newRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_newRule.IMatch_newRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_newRule.IMatch_newRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_newRule.IMatch_newRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_newRule.IMatch_newRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_newRule.IMatch_newRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_newRule.IMatch_newRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_newRule.IMatch_newRule>)matches);
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
    public interface IAction_killRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_killRule.IMatch_killRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> matches);
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
    
    public class Action_killRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_killRule
    {
        public Action_killRule() {
            _rulePattern = Rule_killRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_killRule.Match_killRule, Rule_killRule.IMatch_killRule>(this);
        }

        public Rule_killRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "killRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_killRule.Match_killRule, Rule_killRule.IMatch_killRule> matches;

        public static Action_killRule Instance { get { return instance; } }
        private static Action_killRule instance = new Action_killRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup killRule_edge_n2 
            int type_id_candidate_killRule_edge_n2 = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_killRule_edge_n2 = graph.edgesByTypeHeads[type_id_candidate_killRule_edge_n2], candidate_killRule_edge_n2 = head_candidate_killRule_edge_n2.lgspTypeNext; candidate_killRule_edge_n2 != head_candidate_killRule_edge_n2; candidate_killRule_edge_n2 = candidate_killRule_edge_n2.lgspTypeNext)
            {
                uint prev__candidate_killRule_edge_n2;
                prev__candidate_killRule_edge_n2 = candidate_killRule_edge_n2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_killRule_edge_n2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source killRule_node_p from killRule_edge_n2 
                GRGEN_LGSP.LGSPNode candidate_killRule_node_p = candidate_killRule_edge_n2.lgspSource;
                if(candidate_killRule_node_p.lgspType.TypeID!=1) {
                    candidate_killRule_edge_n2.lgspFlags = candidate_killRule_edge_n2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                    continue;
                }
                uint prev__candidate_killRule_node_p;
                prev__candidate_killRule_node_p = candidate_killRule_node_p.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_killRule_node_p.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target killRule_node_p2 from killRule_edge_n2 
                GRGEN_LGSP.LGSPNode candidate_killRule_node_p2 = candidate_killRule_edge_n2.lgspTarget;
                if(candidate_killRule_node_p2.lgspType.TypeID!=1) {
                    candidate_killRule_node_p.lgspFlags = candidate_killRule_node_p.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                    candidate_killRule_edge_n2.lgspFlags = candidate_killRule_edge_n2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                    continue;
                }
                if((candidate_killRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_killRule_node_p.lgspFlags = candidate_killRule_node_p.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                    candidate_killRule_edge_n2.lgspFlags = candidate_killRule_edge_n2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                    continue;
                }
                uint prev__candidate_killRule_node_p2;
                prev__candidate_killRule_node_p2 = candidate_killRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_killRule_node_p2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming killRule_edge_n1 from killRule_node_p 
                GRGEN_LGSP.LGSPEdge head_candidate_killRule_edge_n1 = candidate_killRule_node_p.lgspInhead;
                if(head_candidate_killRule_edge_n1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_killRule_edge_n1 = head_candidate_killRule_edge_n1;
                    do
                    {
                        if(candidate_killRule_edge_n1.lgspType.TypeID!=3) {
                            continue;
                        }
                        if((candidate_killRule_edge_n1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Implicit Source killRule_node_p1 from killRule_edge_n1 
                        GRGEN_LGSP.LGSPNode candidate_killRule_node_p1 = candidate_killRule_edge_n1.lgspSource;
                        if(candidate_killRule_node_p1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_killRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        Rule_killRule.Match_killRule match = matches.GetNextUnfilledPosition();
                        match._node_p1 = candidate_killRule_node_p1;
                        match._node_p = candidate_killRule_node_p;
                        match._node_p2 = candidate_killRule_node_p2;
                        match._edge_n1 = candidate_killRule_edge_n1;
                        match._edge_n2 = candidate_killRule_edge_n2;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_killRule_node_p.MoveInHeadAfter(candidate_killRule_edge_n1);
                            graph.MoveHeadAfter(candidate_killRule_edge_n2);
                            candidate_killRule_node_p2.lgspFlags = candidate_killRule_node_p2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p2;
                            candidate_killRule_node_p.lgspFlags = candidate_killRule_node_p.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                            candidate_killRule_edge_n2.lgspFlags = candidate_killRule_edge_n2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                            return matches;
                        }
                    }
                    while( (candidate_killRule_edge_n1 = candidate_killRule_edge_n1.lgspInNext) != head_candidate_killRule_edge_n1 );
                }
                candidate_killRule_node_p2.lgspFlags = candidate_killRule_node_p2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p2;
                candidate_killRule_node_p.lgspFlags = candidate_killRule_node_p.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                candidate_killRule_edge_n2.lgspFlags = candidate_killRule_edge_n2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_killRule.IMatch_killRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_killRule.IMatch_killRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_killRule.IMatch_killRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_killRule.IMatch_killRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_killRule.IMatch_killRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_killRule.IMatch_killRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_killRule.IMatch_killRule>)matches);
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
    public interface IAction_mountRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_mountRule.IMatch_mountRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> matches);
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
    
    public class Action_mountRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_mountRule
    {
        public Action_mountRule() {
            _rulePattern = Rule_mountRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_mountRule.Match_mountRule, Rule_mountRule.IMatch_mountRule>(this);
        }

        public Rule_mountRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "mountRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_mountRule.Match_mountRule, Rule_mountRule.IMatch_mountRule> matches;

        public static Action_mountRule Instance { get { return instance; } }
        private static Action_mountRule instance = new Action_mountRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup mountRule_node_p 
            int type_id_candidate_mountRule_node_p = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_mountRule_node_p = graph.nodesByTypeHeads[type_id_candidate_mountRule_node_p], candidate_mountRule_node_p = head_candidate_mountRule_node_p.lgspTypeNext; candidate_mountRule_node_p != head_candidate_mountRule_node_p; candidate_mountRule_node_p = candidate_mountRule_node_p.lgspTypeNext)
            {
                Rule_mountRule.Match_mountRule match = matches.GetNextUnfilledPosition();
                match._node_p = candidate_mountRule_node_p;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_mountRule_node_p);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_mountRule.IMatch_mountRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_mountRule.IMatch_mountRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_mountRule.IMatch_mountRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_mountRule.IMatch_mountRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_mountRule.IMatch_mountRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_mountRule.IMatch_mountRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_mountRule.IMatch_mountRule>)matches);
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
    public interface IAction_unmountRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_unmountRule.IMatch_unmountRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> matches);
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
    
    public class Action_unmountRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_unmountRule
    {
        public Action_unmountRule() {
            _rulePattern = Rule_unmountRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_unmountRule.Match_unmountRule, Rule_unmountRule.IMatch_unmountRule>(this);
        }

        public Rule_unmountRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "unmountRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_unmountRule.Match_unmountRule, Rule_unmountRule.IMatch_unmountRule> matches;

        public static Action_unmountRule Instance { get { return instance; } }
        private static Action_unmountRule instance = new Action_unmountRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup unmountRule_edge_t 
            int type_id_candidate_unmountRule_edge_t = 6;
            for(GRGEN_LGSP.LGSPEdge head_candidate_unmountRule_edge_t = graph.edgesByTypeHeads[type_id_candidate_unmountRule_edge_t], candidate_unmountRule_edge_t = head_candidate_unmountRule_edge_t.lgspTypeNext; candidate_unmountRule_edge_t != head_candidate_unmountRule_edge_t; candidate_unmountRule_edge_t = candidate_unmountRule_edge_t.lgspTypeNext)
            {
                // Implicit Source unmountRule_node_r from unmountRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_unmountRule_node_r = candidate_unmountRule_edge_t.lgspSource;
                if(candidate_unmountRule_node_r.lgspType.TypeID!=2) {
                    continue;
                }
                // Implicit Target unmountRule_node_p from unmountRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_unmountRule_node_p = candidate_unmountRule_edge_t.lgspTarget;
                if(candidate_unmountRule_node_p.lgspType.TypeID!=1) {
                    continue;
                }
                Rule_unmountRule.Match_unmountRule match = matches.GetNextUnfilledPosition();
                match._node_r = candidate_unmountRule_node_r;
                match._node_p = candidate_unmountRule_node_p;
                match._edge_t = candidate_unmountRule_edge_t;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_unmountRule_edge_t);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_unmountRule.IMatch_unmountRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_unmountRule.IMatch_unmountRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_unmountRule.IMatch_unmountRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_unmountRule.IMatch_unmountRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_unmountRule.IMatch_unmountRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_unmountRule.IMatch_unmountRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_unmountRule.IMatch_unmountRule>)matches);
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
    public interface IAction_passRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_passRule.IMatch_passRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> matches);
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
    
    public class Action_passRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_passRule
    {
        public Action_passRule() {
            _rulePattern = Rule_passRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_passRule.Match_passRule, Rule_passRule.IMatch_passRule>(this);
        }

        public Rule_passRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "passRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_passRule.Match_passRule, Rule_passRule.IMatch_passRule> matches;

        public static Action_passRule Instance { get { return instance; } }
        private static Action_passRule instance = new Action_passRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup passRule_edge_n 
            int type_id_candidate_passRule_edge_n = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_passRule_edge_n = graph.edgesByTypeHeads[type_id_candidate_passRule_edge_n], candidate_passRule_edge_n = head_candidate_passRule_edge_n.lgspTypeNext; candidate_passRule_edge_n != head_candidate_passRule_edge_n; candidate_passRule_edge_n = candidate_passRule_edge_n.lgspTypeNext)
            {
                // Implicit Source passRule_node_p1 from passRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_passRule_node_p1 = candidate_passRule_edge_n.lgspSource;
                if(candidate_passRule_node_p1.lgspType.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_passRule_node_p1;
                prev__candidate_passRule_node_p1 = candidate_passRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_passRule_node_p1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target passRule_node_p2 from passRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_passRule_node_p2 = candidate_passRule_edge_n.lgspTarget;
                if(candidate_passRule_node_p2.lgspType.TypeID!=1) {
                    candidate_passRule_node_p1.lgspFlags = candidate_passRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
                    continue;
                }
                if((candidate_passRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_passRule_node_p1.lgspFlags = candidate_passRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
                    continue;
                }
                // Extend Incoming passRule_edge__edge0 from passRule_node_p1 
                GRGEN_LGSP.LGSPEdge head_candidate_passRule_edge__edge0 = candidate_passRule_node_p1.lgspInhead;
                if(head_candidate_passRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_passRule_edge__edge0 = head_candidate_passRule_edge__edge0;
                    do
                    {
                        if(candidate_passRule_edge__edge0.lgspType.TypeID!=6) {
                            continue;
                        }
                        // Implicit Source passRule_node_r from passRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_passRule_node_r = candidate_passRule_edge__edge0.lgspSource;
                        if(candidate_passRule_node_r.lgspType.TypeID!=2) {
                            continue;
                        }
                        // NegativePattern 
                        {
                            ++negLevel;
                            // Extend Outgoing passRule_neg_0_edge_req from passRule_node_p1 
                            GRGEN_LGSP.LGSPEdge head_candidate_passRule_neg_0_edge_req = candidate_passRule_node_p1.lgspOuthead;
                            if(head_candidate_passRule_neg_0_edge_req != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_passRule_neg_0_edge_req = head_candidate_passRule_neg_0_edge_req;
                                do
                                {
                                    if(candidate_passRule_neg_0_edge_req.lgspType.TypeID!=8) {
                                        continue;
                                    }
                                    if(candidate_passRule_neg_0_edge_req.lgspTarget != candidate_passRule_node_r) {
                                        continue;
                                    }
                                    // negative pattern found
                                    --negLevel;
                                    goto label0;
                                }
                                while( (candidate_passRule_neg_0_edge_req = candidate_passRule_neg_0_edge_req.lgspOutNext) != head_candidate_passRule_neg_0_edge_req );
                            }
                            --negLevel;
                        }
                        Rule_passRule.Match_passRule match = matches.GetNextUnfilledPosition();
                        match._node_r = candidate_passRule_node_r;
                        match._node_p1 = candidate_passRule_node_p1;
                        match._node_p2 = candidate_passRule_node_p2;
                        match._edge__edge0 = candidate_passRule_edge__edge0;
                        match._edge_n = candidate_passRule_edge_n;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_passRule_node_p1.MoveInHeadAfter(candidate_passRule_edge__edge0);
                            graph.MoveHeadAfter(candidate_passRule_edge_n);
                            candidate_passRule_node_p1.lgspFlags = candidate_passRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
                            return matches;
                        }
label0: ;
                    }
                    while( (candidate_passRule_edge__edge0 = candidate_passRule_edge__edge0.lgspInNext) != head_candidate_passRule_edge__edge0 );
                }
                candidate_passRule_node_p1.lgspFlags = candidate_passRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_passRule.IMatch_passRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_passRule.IMatch_passRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_passRule.IMatch_passRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_passRule.IMatch_passRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_passRule.IMatch_passRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_passRule.IMatch_passRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_passRule.IMatch_passRule>)matches);
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
    public interface IAction_requestRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_requestRule.IMatch_requestRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> matches);
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
    
    public class Action_requestRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_requestRule
    {
        public Action_requestRule() {
            _rulePattern = Rule_requestRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_requestRule.Match_requestRule, Rule_requestRule.IMatch_requestRule>(this);
        }

        public Rule_requestRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "requestRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_requestRule.Match_requestRule, Rule_requestRule.IMatch_requestRule> matches;

        public static Action_requestRule Instance { get { return instance; } }
        private static Action_requestRule instance = new Action_requestRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup requestRule_node_r 
            int type_id_candidate_requestRule_node_r = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_requestRule_node_r = graph.nodesByTypeHeads[type_id_candidate_requestRule_node_r], candidate_requestRule_node_r = head_candidate_requestRule_node_r.lgspTypeNext; candidate_requestRule_node_r != head_candidate_requestRule_node_r; candidate_requestRule_node_r = candidate_requestRule_node_r.lgspTypeNext)
            {
                // Lookup requestRule_node_p 
                int type_id_candidate_requestRule_node_p = 1;
                for(GRGEN_LGSP.LGSPNode head_candidate_requestRule_node_p = graph.nodesByTypeHeads[type_id_candidate_requestRule_node_p], candidate_requestRule_node_p = head_candidate_requestRule_node_p.lgspTypeNext; candidate_requestRule_node_p != head_candidate_requestRule_node_p; candidate_requestRule_node_p = candidate_requestRule_node_p.lgspTypeNext)
                {
                    // NegativePattern 
                    {
                        ++negLevel;
                        // Extend Outgoing requestRule_neg_0_edge_hb from requestRule_node_r 
                        GRGEN_LGSP.LGSPEdge head_candidate_requestRule_neg_0_edge_hb = candidate_requestRule_node_r.lgspOuthead;
                        if(head_candidate_requestRule_neg_0_edge_hb != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_requestRule_neg_0_edge_hb = head_candidate_requestRule_neg_0_edge_hb;
                            do
                            {
                                if(candidate_requestRule_neg_0_edge_hb.lgspType.TypeID!=5) {
                                    continue;
                                }
                                if(candidate_requestRule_neg_0_edge_hb.lgspTarget != candidate_requestRule_node_p) {
                                    continue;
                                }
                                // negative pattern found
                                --negLevel;
                                goto label1;
                            }
                            while( (candidate_requestRule_neg_0_edge_hb = candidate_requestRule_neg_0_edge_hb.lgspOutNext) != head_candidate_requestRule_neg_0_edge_hb );
                        }
                        --negLevel;
                    }
                    // NegativePattern 
                    {
                        ++negLevel;
                        // Extend Outgoing requestRule_neg_1_edge_req from requestRule_node_p 
                        GRGEN_LGSP.LGSPEdge head_candidate_requestRule_neg_1_edge_req = candidate_requestRule_node_p.lgspOuthead;
                        if(head_candidate_requestRule_neg_1_edge_req != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_requestRule_neg_1_edge_req = head_candidate_requestRule_neg_1_edge_req;
                            do
                            {
                                if(candidate_requestRule_neg_1_edge_req.lgspType.TypeID!=8) {
                                    continue;
                                }
                                // Implicit Target requestRule_neg_1_node_m from requestRule_neg_1_edge_req 
                                GRGEN_LGSP.LGSPNode candidate_requestRule_neg_1_node_m = candidate_requestRule_neg_1_edge_req.lgspTarget;
                                if(candidate_requestRule_neg_1_node_m.lgspType.TypeID!=2) {
                                    continue;
                                }
                                // negative pattern found
                                --negLevel;
                                goto label2;
                            }
                            while( (candidate_requestRule_neg_1_edge_req = candidate_requestRule_neg_1_edge_req.lgspOutNext) != head_candidate_requestRule_neg_1_edge_req );
                        }
                        --negLevel;
                    }
                    Rule_requestRule.Match_requestRule match = matches.GetNextUnfilledPosition();
                    match._node_p = candidate_requestRule_node_p;
                    match._node_r = candidate_requestRule_node_r;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(candidate_requestRule_node_p);
                        graph.MoveHeadAfter(candidate_requestRule_node_r);
                        return matches;
                    }
label1: ;
label2: ;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_requestRule.IMatch_requestRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_requestRule.IMatch_requestRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_requestRule.IMatch_requestRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_requestRule.IMatch_requestRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_requestRule.IMatch_requestRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_requestRule.IMatch_requestRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_requestRule.IMatch_requestRule>)matches);
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
    public interface IAction_takeRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_takeRule.IMatch_takeRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> matches);
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
    
    public class Action_takeRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_takeRule
    {
        public Action_takeRule() {
            _rulePattern = Rule_takeRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_takeRule.Match_takeRule, Rule_takeRule.IMatch_takeRule>(this);
        }

        public Rule_takeRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "takeRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_takeRule.Match_takeRule, Rule_takeRule.IMatch_takeRule> matches;

        public static Action_takeRule Instance { get { return instance; } }
        private static Action_takeRule instance = new Action_takeRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup takeRule_edge_req 
            int type_id_candidate_takeRule_edge_req = 8;
            for(GRGEN_LGSP.LGSPEdge head_candidate_takeRule_edge_req = graph.edgesByTypeHeads[type_id_candidate_takeRule_edge_req], candidate_takeRule_edge_req = head_candidate_takeRule_edge_req.lgspTypeNext; candidate_takeRule_edge_req != head_candidate_takeRule_edge_req; candidate_takeRule_edge_req = candidate_takeRule_edge_req.lgspTypeNext)
            {
                // Implicit Target takeRule_node_r from takeRule_edge_req 
                GRGEN_LGSP.LGSPNode candidate_takeRule_node_r = candidate_takeRule_edge_req.lgspTarget;
                if(candidate_takeRule_node_r.lgspType.TypeID!=2) {
                    continue;
                }
                // Extend Outgoing takeRule_edge_t from takeRule_node_r 
                GRGEN_LGSP.LGSPEdge head_candidate_takeRule_edge_t = candidate_takeRule_node_r.lgspOuthead;
                if(head_candidate_takeRule_edge_t != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_takeRule_edge_t = head_candidate_takeRule_edge_t;
                    do
                    {
                        if(candidate_takeRule_edge_t.lgspType.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target takeRule_node_p from takeRule_edge_t 
                        GRGEN_LGSP.LGSPNode candidate_takeRule_node_p = candidate_takeRule_edge_t.lgspTarget;
                        if(candidate_takeRule_node_p.lgspType.TypeID!=1) {
                            continue;
                        }
                        if(candidate_takeRule_edge_req.lgspSource != candidate_takeRule_node_p) {
                            continue;
                        }
                        Rule_takeRule.Match_takeRule match = matches.GetNextUnfilledPosition();
                        match._node_r = candidate_takeRule_node_r;
                        match._node_p = candidate_takeRule_node_p;
                        match._edge_t = candidate_takeRule_edge_t;
                        match._edge_req = candidate_takeRule_edge_req;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_takeRule_node_r.MoveOutHeadAfter(candidate_takeRule_edge_t);
                            graph.MoveHeadAfter(candidate_takeRule_edge_req);
                            return matches;
                        }
                    }
                    while( (candidate_takeRule_edge_t = candidate_takeRule_edge_t.lgspOutNext) != head_candidate_takeRule_edge_t );
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_takeRule.IMatch_takeRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_takeRule.IMatch_takeRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_takeRule.IMatch_takeRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_takeRule.IMatch_takeRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_takeRule.IMatch_takeRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_takeRule.IMatch_takeRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_takeRule.IMatch_takeRule>)matches);
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
    public interface IAction_releaseRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_releaseRule.IMatch_releaseRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> matches);
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
    
    public class Action_releaseRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_releaseRule
    {
        public Action_releaseRule() {
            _rulePattern = Rule_releaseRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_releaseRule.Match_releaseRule, Rule_releaseRule.IMatch_releaseRule>(this);
        }

        public Rule_releaseRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "releaseRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_releaseRule.Match_releaseRule, Rule_releaseRule.IMatch_releaseRule> matches;

        public static Action_releaseRule Instance { get { return instance; } }
        private static Action_releaseRule instance = new Action_releaseRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup releaseRule_edge_hb 
            int type_id_candidate_releaseRule_edge_hb = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_releaseRule_edge_hb = graph.edgesByTypeHeads[type_id_candidate_releaseRule_edge_hb], candidate_releaseRule_edge_hb = head_candidate_releaseRule_edge_hb.lgspTypeNext; candidate_releaseRule_edge_hb != head_candidate_releaseRule_edge_hb; candidate_releaseRule_edge_hb = candidate_releaseRule_edge_hb.lgspTypeNext)
            {
                // Implicit Source releaseRule_node_r from releaseRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_releaseRule_node_r = candidate_releaseRule_edge_hb.lgspSource;
                if(candidate_releaseRule_node_r.lgspType.TypeID!=2) {
                    continue;
                }
                // Implicit Target releaseRule_node_p from releaseRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_releaseRule_node_p = candidate_releaseRule_edge_hb.lgspTarget;
                if(candidate_releaseRule_node_p.lgspType.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Outgoing releaseRule_neg_0_edge_req from releaseRule_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_releaseRule_neg_0_edge_req = candidate_releaseRule_node_p.lgspOuthead;
                    if(head_candidate_releaseRule_neg_0_edge_req != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_releaseRule_neg_0_edge_req = head_candidate_releaseRule_neg_0_edge_req;
                        do
                        {
                            if(candidate_releaseRule_neg_0_edge_req.lgspType.TypeID!=8) {
                                continue;
                            }
                            // Implicit Target releaseRule_neg_0_node_m from releaseRule_neg_0_edge_req 
                            GRGEN_LGSP.LGSPNode candidate_releaseRule_neg_0_node_m = candidate_releaseRule_neg_0_edge_req.lgspTarget;
                            if(candidate_releaseRule_neg_0_node_m.lgspType.TypeID!=2) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_releaseRule_neg_0_edge_req = candidate_releaseRule_neg_0_edge_req.lgspOutNext) != head_candidate_releaseRule_neg_0_edge_req );
                    }
                    --negLevel;
                }
                Rule_releaseRule.Match_releaseRule match = matches.GetNextUnfilledPosition();
                match._node_r = candidate_releaseRule_node_r;
                match._node_p = candidate_releaseRule_node_p;
                match._edge_hb = candidate_releaseRule_edge_hb;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_releaseRule_edge_hb);
                    return matches;
                }
label3: ;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_releaseRule.IMatch_releaseRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_releaseRule.IMatch_releaseRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_releaseRule.IMatch_releaseRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_releaseRule.IMatch_releaseRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_releaseRule.IMatch_releaseRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_releaseRule.IMatch_releaseRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_releaseRule.IMatch_releaseRule>)matches);
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
    public interface IAction_giveRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_giveRule.IMatch_giveRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> matches);
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
    
    public class Action_giveRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_giveRule
    {
        public Action_giveRule() {
            _rulePattern = Rule_giveRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_giveRule.Match_giveRule, Rule_giveRule.IMatch_giveRule>(this);
        }

        public Rule_giveRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "giveRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_giveRule.Match_giveRule, Rule_giveRule.IMatch_giveRule> matches;

        public static Action_giveRule Instance { get { return instance; } }
        private static Action_giveRule instance = new Action_giveRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup giveRule_edge_n 
            int type_id_candidate_giveRule_edge_n = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_giveRule_edge_n = graph.edgesByTypeHeads[type_id_candidate_giveRule_edge_n], candidate_giveRule_edge_n = head_candidate_giveRule_edge_n.lgspTypeNext; candidate_giveRule_edge_n != head_candidate_giveRule_edge_n; candidate_giveRule_edge_n = candidate_giveRule_edge_n.lgspTypeNext)
            {
                // Implicit Source giveRule_node_p1 from giveRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_giveRule_node_p1 = candidate_giveRule_edge_n.lgspSource;
                if(candidate_giveRule_node_p1.lgspType.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_giveRule_node_p1;
                prev__candidate_giveRule_node_p1 = candidate_giveRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_giveRule_node_p1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target giveRule_node_p2 from giveRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_giveRule_node_p2 = candidate_giveRule_edge_n.lgspTarget;
                if(candidate_giveRule_node_p2.lgspType.TypeID!=1) {
                    candidate_giveRule_node_p1.lgspFlags = candidate_giveRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
                    continue;
                }
                if((candidate_giveRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_giveRule_node_p1.lgspFlags = candidate_giveRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
                    continue;
                }
                // Extend Incoming giveRule_edge_rel from giveRule_node_p1 
                GRGEN_LGSP.LGSPEdge head_candidate_giveRule_edge_rel = candidate_giveRule_node_p1.lgspInhead;
                if(head_candidate_giveRule_edge_rel != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_giveRule_edge_rel = head_candidate_giveRule_edge_rel;
                    do
                    {
                        if(candidate_giveRule_edge_rel.lgspType.TypeID!=7) {
                            continue;
                        }
                        // Implicit Source giveRule_node_r from giveRule_edge_rel 
                        GRGEN_LGSP.LGSPNode candidate_giveRule_node_r = candidate_giveRule_edge_rel.lgspSource;
                        if(candidate_giveRule_node_r.lgspType.TypeID!=2) {
                            continue;
                        }
                        Rule_giveRule.Match_giveRule match = matches.GetNextUnfilledPosition();
                        match._node_r = candidate_giveRule_node_r;
                        match._node_p1 = candidate_giveRule_node_p1;
                        match._node_p2 = candidate_giveRule_node_p2;
                        match._edge_rel = candidate_giveRule_edge_rel;
                        match._edge_n = candidate_giveRule_edge_n;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_giveRule_node_p1.MoveInHeadAfter(candidate_giveRule_edge_rel);
                            graph.MoveHeadAfter(candidate_giveRule_edge_n);
                            candidate_giveRule_node_p1.lgspFlags = candidate_giveRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
                            return matches;
                        }
                    }
                    while( (candidate_giveRule_edge_rel = candidate_giveRule_edge_rel.lgspInNext) != head_candidate_giveRule_edge_rel );
                }
                candidate_giveRule_node_p1.lgspFlags = candidate_giveRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_giveRule.IMatch_giveRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_giveRule.IMatch_giveRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_giveRule.IMatch_giveRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_giveRule.IMatch_giveRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_giveRule.IMatch_giveRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_giveRule.IMatch_giveRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_giveRule.IMatch_giveRule>)matches);
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
    public interface IAction_blockedRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_blockedRule.IMatch_blockedRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> matches);
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
    
    public class Action_blockedRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_blockedRule
    {
        public Action_blockedRule() {
            _rulePattern = Rule_blockedRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_blockedRule.Match_blockedRule, Rule_blockedRule.IMatch_blockedRule>(this);
        }

        public Rule_blockedRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "blockedRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_blockedRule.Match_blockedRule, Rule_blockedRule.IMatch_blockedRule> matches;

        public static Action_blockedRule Instance { get { return instance; } }
        private static Action_blockedRule instance = new Action_blockedRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup blockedRule_edge_hb 
            int type_id_candidate_blockedRule_edge_hb = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_blockedRule_edge_hb = graph.edgesByTypeHeads[type_id_candidate_blockedRule_edge_hb], candidate_blockedRule_edge_hb = head_candidate_blockedRule_edge_hb.lgspTypeNext; candidate_blockedRule_edge_hb != head_candidate_blockedRule_edge_hb; candidate_blockedRule_edge_hb = candidate_blockedRule_edge_hb.lgspTypeNext)
            {
                // Implicit Source blockedRule_node_r from blockedRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_blockedRule_node_r = candidate_blockedRule_edge_hb.lgspSource;
                if(candidate_blockedRule_node_r.lgspType.TypeID!=2) {
                    continue;
                }
                // Implicit Target blockedRule_node_p2 from blockedRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_blockedRule_node_p2 = candidate_blockedRule_edge_hb.lgspTarget;
                if(candidate_blockedRule_node_p2.lgspType.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_blockedRule_node_p2;
                prev__candidate_blockedRule_node_p2 = candidate_blockedRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_blockedRule_node_p2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming blockedRule_edge_req from blockedRule_node_r 
                GRGEN_LGSP.LGSPEdge head_candidate_blockedRule_edge_req = candidate_blockedRule_node_r.lgspInhead;
                if(head_candidate_blockedRule_edge_req != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_blockedRule_edge_req = head_candidate_blockedRule_edge_req;
                    do
                    {
                        if(candidate_blockedRule_edge_req.lgspType.TypeID!=8) {
                            continue;
                        }
                        // Implicit Source blockedRule_node_p1 from blockedRule_edge_req 
                        GRGEN_LGSP.LGSPNode candidate_blockedRule_node_p1 = candidate_blockedRule_edge_req.lgspSource;
                        if(candidate_blockedRule_node_p1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_blockedRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        Rule_blockedRule.Match_blockedRule match = matches.GetNextUnfilledPosition();
                        match._node_p1 = candidate_blockedRule_node_p1;
                        match._node_r = candidate_blockedRule_node_r;
                        match._node_p2 = candidate_blockedRule_node_p2;
                        match._edge_req = candidate_blockedRule_edge_req;
                        match._edge_hb = candidate_blockedRule_edge_hb;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_blockedRule_node_r.MoveInHeadAfter(candidate_blockedRule_edge_req);
                            graph.MoveHeadAfter(candidate_blockedRule_edge_hb);
                            candidate_blockedRule_node_p2.lgspFlags = candidate_blockedRule_node_p2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_blockedRule_node_p2;
                            return matches;
                        }
                    }
                    while( (candidate_blockedRule_edge_req = candidate_blockedRule_edge_req.lgspInNext) != head_candidate_blockedRule_edge_req );
                }
                candidate_blockedRule_node_p2.lgspFlags = candidate_blockedRule_node_p2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_blockedRule_node_p2;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_blockedRule.IMatch_blockedRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_blockedRule.IMatch_blockedRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_blockedRule.IMatch_blockedRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_blockedRule.IMatch_blockedRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_blockedRule.IMatch_blockedRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_blockedRule.IMatch_blockedRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_blockedRule.IMatch_blockedRule>)matches);
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
    public interface IAction_waitingRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_waitingRule.IMatch_waitingRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> matches);
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
    
    public class Action_waitingRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_waitingRule
    {
        public Action_waitingRule() {
            _rulePattern = Rule_waitingRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_waitingRule.Match_waitingRule, Rule_waitingRule.IMatch_waitingRule>(this);
        }

        public Rule_waitingRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "waitingRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_waitingRule.Match_waitingRule, Rule_waitingRule.IMatch_waitingRule> matches;

        public static Action_waitingRule Instance { get { return instance; } }
        private static Action_waitingRule instance = new Action_waitingRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup waitingRule_node_r 
            int type_id_candidate_waitingRule_node_r = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_waitingRule_node_r = graph.nodesByTypeHeads[type_id_candidate_waitingRule_node_r], candidate_waitingRule_node_r = head_candidate_waitingRule_node_r.lgspTypeNext; candidate_waitingRule_node_r != head_candidate_waitingRule_node_r; candidate_waitingRule_node_r = candidate_waitingRule_node_r.lgspTypeNext)
            {
                uint prev__candidate_waitingRule_node_r;
                prev__candidate_waitingRule_node_r = candidate_waitingRule_node_r.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_waitingRule_node_r.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Lookup waitingRule_edge_b 
                int type_id_candidate_waitingRule_edge_b = 4;
                for(GRGEN_LGSP.LGSPEdge head_candidate_waitingRule_edge_b = graph.edgesByTypeHeads[type_id_candidate_waitingRule_edge_b], candidate_waitingRule_edge_b = head_candidate_waitingRule_edge_b.lgspTypeNext; candidate_waitingRule_edge_b != head_candidate_waitingRule_edge_b; candidate_waitingRule_edge_b = candidate_waitingRule_edge_b.lgspTypeNext)
                {
                    // Implicit Source waitingRule_node_r2 from waitingRule_edge_b 
                    GRGEN_LGSP.LGSPNode candidate_waitingRule_node_r2 = candidate_waitingRule_edge_b.lgspSource;
                    if(candidate_waitingRule_node_r2.lgspType.TypeID!=2) {
                        continue;
                    }
                    if((candidate_waitingRule_node_r2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_waitingRule_node_r2;
                    prev__candidate_waitingRule_node_r2 = candidate_waitingRule_node_r2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_waitingRule_node_r2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit Target waitingRule_node_p1 from waitingRule_edge_b 
                    GRGEN_LGSP.LGSPNode candidate_waitingRule_node_p1 = candidate_waitingRule_edge_b.lgspTarget;
                    if(candidate_waitingRule_node_p1.lgspType.TypeID!=1) {
                        candidate_waitingRule_node_r2.lgspFlags = candidate_waitingRule_node_r2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r2;
                        continue;
                    }
                    uint prev__candidate_waitingRule_node_p1;
                    prev__candidate_waitingRule_node_p1 = candidate_waitingRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_waitingRule_node_p1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Incoming waitingRule_edge_hb from waitingRule_node_p1 
                    GRGEN_LGSP.LGSPEdge head_candidate_waitingRule_edge_hb = candidate_waitingRule_node_p1.lgspInhead;
                    if(head_candidate_waitingRule_edge_hb != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_waitingRule_edge_hb = head_candidate_waitingRule_edge_hb;
                        do
                        {
                            if(candidate_waitingRule_edge_hb.lgspType.TypeID!=5) {
                                continue;
                            }
                            // Implicit Source waitingRule_node_r1 from waitingRule_edge_hb 
                            GRGEN_LGSP.LGSPNode candidate_waitingRule_node_r1 = candidate_waitingRule_edge_hb.lgspSource;
                            if(candidate_waitingRule_node_r1.lgspType.TypeID!=2) {
                                continue;
                            }
                            if((candidate_waitingRule_node_r1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // Extend Incoming waitingRule_edge_req from waitingRule_node_r1 
                            GRGEN_LGSP.LGSPEdge head_candidate_waitingRule_edge_req = candidate_waitingRule_node_r1.lgspInhead;
                            if(head_candidate_waitingRule_edge_req != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_waitingRule_edge_req = head_candidate_waitingRule_edge_req;
                                do
                                {
                                    if(candidate_waitingRule_edge_req.lgspType.TypeID!=8) {
                                        continue;
                                    }
                                    // Implicit Source waitingRule_node_p2 from waitingRule_edge_req 
                                    GRGEN_LGSP.LGSPNode candidate_waitingRule_node_p2 = candidate_waitingRule_edge_req.lgspSource;
                                    if(candidate_waitingRule_node_p2.lgspType.TypeID!=1) {
                                        continue;
                                    }
                                    if((candidate_waitingRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                    {
                                        continue;
                                    }
                                    Rule_waitingRule.Match_waitingRule match = matches.GetNextUnfilledPosition();
                                    match._node_r2 = candidate_waitingRule_node_r2;
                                    match._node_p1 = candidate_waitingRule_node_p1;
                                    match._node_r1 = candidate_waitingRule_node_r1;
                                    match._node_p2 = candidate_waitingRule_node_p2;
                                    match._node_r = candidate_waitingRule_node_r;
                                    match._edge_b = candidate_waitingRule_edge_b;
                                    match._edge_hb = candidate_waitingRule_edge_hb;
                                    match._edge_req = candidate_waitingRule_edge_req;
                                    matches.PositionWasFilledFixIt();
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && matches.Count >= maxMatches)
                                    {
                                        candidate_waitingRule_node_r1.MoveInHeadAfter(candidate_waitingRule_edge_req);
                                        candidate_waitingRule_node_p1.MoveInHeadAfter(candidate_waitingRule_edge_hb);
                                        graph.MoveHeadAfter(candidate_waitingRule_edge_b);
                                        graph.MoveHeadAfter(candidate_waitingRule_node_r);
                                        candidate_waitingRule_node_p1.lgspFlags = candidate_waitingRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_p1;
                                        candidate_waitingRule_node_r2.lgspFlags = candidate_waitingRule_node_r2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r2;
                                        candidate_waitingRule_node_r.lgspFlags = candidate_waitingRule_node_r.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r;
                                        return matches;
                                    }
                                }
                                while( (candidate_waitingRule_edge_req = candidate_waitingRule_edge_req.lgspInNext) != head_candidate_waitingRule_edge_req );
                            }
                        }
                        while( (candidate_waitingRule_edge_hb = candidate_waitingRule_edge_hb.lgspInNext) != head_candidate_waitingRule_edge_hb );
                    }
                    candidate_waitingRule_node_p1.lgspFlags = candidate_waitingRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_p1;
                    candidate_waitingRule_node_r2.lgspFlags = candidate_waitingRule_node_r2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r2;
                }
                candidate_waitingRule_node_r.lgspFlags = candidate_waitingRule_node_r.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_waitingRule.IMatch_waitingRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_waitingRule.IMatch_waitingRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_waitingRule.IMatch_waitingRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_waitingRule.IMatch_waitingRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_waitingRule.IMatch_waitingRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_waitingRule.IMatch_waitingRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_waitingRule.IMatch_waitingRule>)matches);
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
    public interface IAction_ignoreRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_ignoreRule.IMatch_ignoreRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> matches);
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
    
    public class Action_ignoreRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ignoreRule
    {
        public Action_ignoreRule() {
            _rulePattern = Rule_ignoreRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ignoreRule.Match_ignoreRule, Rule_ignoreRule.IMatch_ignoreRule>(this);
        }

        public Rule_ignoreRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ignoreRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ignoreRule.Match_ignoreRule, Rule_ignoreRule.IMatch_ignoreRule> matches;

        public static Action_ignoreRule Instance { get { return instance; } }
        private static Action_ignoreRule instance = new Action_ignoreRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup ignoreRule_edge_b 
            int type_id_candidate_ignoreRule_edge_b = 4;
            for(GRGEN_LGSP.LGSPEdge head_candidate_ignoreRule_edge_b = graph.edgesByTypeHeads[type_id_candidate_ignoreRule_edge_b], candidate_ignoreRule_edge_b = head_candidate_ignoreRule_edge_b.lgspTypeNext; candidate_ignoreRule_edge_b != head_candidate_ignoreRule_edge_b; candidate_ignoreRule_edge_b = candidate_ignoreRule_edge_b.lgspTypeNext)
            {
                // Implicit Source ignoreRule_node_r from ignoreRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_ignoreRule_node_r = candidate_ignoreRule_edge_b.lgspSource;
                if(candidate_ignoreRule_node_r.lgspType.TypeID!=2) {
                    continue;
                }
                // Implicit Target ignoreRule_node_p from ignoreRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_ignoreRule_node_p = candidate_ignoreRule_edge_b.lgspTarget;
                if(candidate_ignoreRule_node_p.lgspType.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Incoming ignoreRule_neg_0_edge_hb from ignoreRule_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_ignoreRule_neg_0_edge_hb = candidate_ignoreRule_node_p.lgspInhead;
                    if(head_candidate_ignoreRule_neg_0_edge_hb != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ignoreRule_neg_0_edge_hb = head_candidate_ignoreRule_neg_0_edge_hb;
                        do
                        {
                            if(candidate_ignoreRule_neg_0_edge_hb.lgspType.TypeID!=5) {
                                continue;
                            }
                            // Implicit Source ignoreRule_neg_0_node_m from ignoreRule_neg_0_edge_hb 
                            GRGEN_LGSP.LGSPNode candidate_ignoreRule_neg_0_node_m = candidate_ignoreRule_neg_0_edge_hb.lgspSource;
                            if(candidate_ignoreRule_neg_0_node_m.lgspType.TypeID!=2) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label4;
                        }
                        while( (candidate_ignoreRule_neg_0_edge_hb = candidate_ignoreRule_neg_0_edge_hb.lgspInNext) != head_candidate_ignoreRule_neg_0_edge_hb );
                    }
                    --negLevel;
                }
                Rule_ignoreRule.Match_ignoreRule match = matches.GetNextUnfilledPosition();
                match._node_r = candidate_ignoreRule_node_r;
                match._node_p = candidate_ignoreRule_node_p;
                match._edge_b = candidate_ignoreRule_edge_b;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_ignoreRule_edge_b);
                    return matches;
                }
label4: ;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_ignoreRule.IMatch_ignoreRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_ignoreRule.IMatch_ignoreRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_ignoreRule.IMatch_ignoreRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_ignoreRule.IMatch_ignoreRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_ignoreRule.IMatch_ignoreRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_ignoreRule.IMatch_ignoreRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_ignoreRule.IMatch_ignoreRule>)matches);
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
    public interface IAction_unlockRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_unlockRule.IMatch_unlockRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> matches);
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
    
    public class Action_unlockRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_unlockRule
    {
        public Action_unlockRule() {
            _rulePattern = Rule_unlockRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_unlockRule.Match_unlockRule, Rule_unlockRule.IMatch_unlockRule>(this);
        }

        public Rule_unlockRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "unlockRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_unlockRule.Match_unlockRule, Rule_unlockRule.IMatch_unlockRule> matches;

        public static Action_unlockRule Instance { get { return instance; } }
        private static Action_unlockRule instance = new Action_unlockRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup unlockRule_edge_b 
            int type_id_candidate_unlockRule_edge_b = 4;
            for(GRGEN_LGSP.LGSPEdge head_candidate_unlockRule_edge_b = graph.edgesByTypeHeads[type_id_candidate_unlockRule_edge_b], candidate_unlockRule_edge_b = head_candidate_unlockRule_edge_b.lgspTypeNext; candidate_unlockRule_edge_b != head_candidate_unlockRule_edge_b; candidate_unlockRule_edge_b = candidate_unlockRule_edge_b.lgspTypeNext)
            {
                // Implicit Source unlockRule_node_r from unlockRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_unlockRule_node_r = candidate_unlockRule_edge_b.lgspSource;
                if(candidate_unlockRule_node_r.lgspType.TypeID!=2) {
                    continue;
                }
                // Implicit Target unlockRule_node_p from unlockRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_unlockRule_node_p = candidate_unlockRule_edge_b.lgspTarget;
                if(candidate_unlockRule_node_p.lgspType.TypeID!=1) {
                    continue;
                }
                // Extend Outgoing unlockRule_edge_hb from unlockRule_node_r 
                GRGEN_LGSP.LGSPEdge head_candidate_unlockRule_edge_hb = candidate_unlockRule_node_r.lgspOuthead;
                if(head_candidate_unlockRule_edge_hb != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_unlockRule_edge_hb = head_candidate_unlockRule_edge_hb;
                    do
                    {
                        if(candidate_unlockRule_edge_hb.lgspType.TypeID!=5) {
                            continue;
                        }
                        if(candidate_unlockRule_edge_hb.lgspTarget != candidate_unlockRule_node_p) {
                            continue;
                        }
                        Rule_unlockRule.Match_unlockRule match = matches.GetNextUnfilledPosition();
                        match._node_r = candidate_unlockRule_node_r;
                        match._node_p = candidate_unlockRule_node_p;
                        match._edge_b = candidate_unlockRule_edge_b;
                        match._edge_hb = candidate_unlockRule_edge_hb;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            candidate_unlockRule_node_r.MoveOutHeadAfter(candidate_unlockRule_edge_hb);
                            graph.MoveHeadAfter(candidate_unlockRule_edge_b);
                            return matches;
                        }
                    }
                    while( (candidate_unlockRule_edge_hb = candidate_unlockRule_edge_hb.lgspOutNext) != head_candidate_unlockRule_edge_hb );
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_unlockRule.IMatch_unlockRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_unlockRule.IMatch_unlockRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_unlockRule.IMatch_unlockRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_unlockRule.IMatch_unlockRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_unlockRule.IMatch_unlockRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_unlockRule.IMatch_unlockRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_unlockRule.IMatch_unlockRule>)matches);
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
    public interface IAction_requestStarRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_requestStarRule.IMatch_requestStarRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> matches);
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
    
    public class Action_requestStarRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_requestStarRule
    {
        public Action_requestStarRule() {
            _rulePattern = Rule_requestStarRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_requestStarRule.Match_requestStarRule, Rule_requestStarRule.IMatch_requestStarRule>(this);
        }

        public Rule_requestStarRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "requestStarRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_requestStarRule.Match_requestStarRule, Rule_requestStarRule.IMatch_requestStarRule> matches;

        public static Action_requestStarRule Instance { get { return instance; } }
        private static Action_requestStarRule instance = new Action_requestStarRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup requestStarRule_edge_h1 
            int type_id_candidate_requestStarRule_edge_h1 = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_edge_h1 = graph.edgesByTypeHeads[type_id_candidate_requestStarRule_edge_h1], candidate_requestStarRule_edge_h1 = head_candidate_requestStarRule_edge_h1.lgspTypeNext; candidate_requestStarRule_edge_h1 != head_candidate_requestStarRule_edge_h1; candidate_requestStarRule_edge_h1 = candidate_requestStarRule_edge_h1.lgspTypeNext)
            {
                uint prev__candidate_requestStarRule_edge_h1;
                prev__candidate_requestStarRule_edge_h1 = candidate_requestStarRule_edge_h1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_requestStarRule_edge_h1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source requestStarRule_node_r1 from requestStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_r1 = candidate_requestStarRule_edge_h1.lgspSource;
                if(candidate_requestStarRule_node_r1.lgspType.TypeID!=2) {
                    candidate_requestStarRule_edge_h1.lgspFlags = candidate_requestStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_requestStarRule_node_r1;
                prev__candidate_requestStarRule_node_r1 = candidate_requestStarRule_node_r1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_requestStarRule_node_r1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target requestStarRule_node_p1 from requestStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_p1 = candidate_requestStarRule_edge_h1.lgspTarget;
                if(candidate_requestStarRule_node_p1.lgspType.TypeID!=1) {
                    candidate_requestStarRule_node_r1.lgspFlags = candidate_requestStarRule_node_r1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_r1;
                    candidate_requestStarRule_edge_h1.lgspFlags = candidate_requestStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_requestStarRule_node_p1;
                prev__candidate_requestStarRule_node_p1 = candidate_requestStarRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_requestStarRule_node_p1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming requestStarRule_edge_n from requestStarRule_node_p1 
                GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_edge_n = candidate_requestStarRule_node_p1.lgspInhead;
                if(head_candidate_requestStarRule_edge_n != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_requestStarRule_edge_n = head_candidate_requestStarRule_edge_n;
                    do
                    {
                        if(candidate_requestStarRule_edge_n.lgspType.TypeID!=3) {
                            continue;
                        }
                        // Implicit Source requestStarRule_node_p2 from requestStarRule_edge_n 
                        GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_p2 = candidate_requestStarRule_edge_n.lgspSource;
                        if(candidate_requestStarRule_node_p2.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_requestStarRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Extend Incoming requestStarRule_edge_h2 from requestStarRule_node_p2 
                        GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_edge_h2 = candidate_requestStarRule_node_p2.lgspInhead;
                        if(head_candidate_requestStarRule_edge_h2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_requestStarRule_edge_h2 = head_candidate_requestStarRule_edge_h2;
                            do
                            {
                                if(candidate_requestStarRule_edge_h2.lgspType.TypeID!=5) {
                                    continue;
                                }
                                if((candidate_requestStarRule_edge_h2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // Implicit Source requestStarRule_node_r2 from requestStarRule_edge_h2 
                                GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_r2 = candidate_requestStarRule_edge_h2.lgspSource;
                                if(candidate_requestStarRule_node_r2.lgspType.TypeID!=2) {
                                    continue;
                                }
                                if((candidate_requestStarRule_node_r2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // NegativePattern 
                                {
                                    ++negLevel;
                                    // Extend Outgoing requestStarRule_neg_0_edge_req from requestStarRule_node_p1 
                                    GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_neg_0_edge_req = candidate_requestStarRule_node_p1.lgspOuthead;
                                    if(head_candidate_requestStarRule_neg_0_edge_req != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_requestStarRule_neg_0_edge_req = head_candidate_requestStarRule_neg_0_edge_req;
                                        do
                                        {
                                            if(candidate_requestStarRule_neg_0_edge_req.lgspType.TypeID!=8) {
                                                continue;
                                            }
                                            if(candidate_requestStarRule_neg_0_edge_req.lgspTarget != candidate_requestStarRule_node_r2) {
                                                continue;
                                            }
                                            // negative pattern found
                                            --negLevel;
                                            goto label5;
                                        }
                                        while( (candidate_requestStarRule_neg_0_edge_req = candidate_requestStarRule_neg_0_edge_req.lgspOutNext) != head_candidate_requestStarRule_neg_0_edge_req );
                                    }
                                    --negLevel;
                                }
                                Rule_requestStarRule.Match_requestStarRule match = matches.GetNextUnfilledPosition();
                                match._node_r1 = candidate_requestStarRule_node_r1;
                                match._node_p1 = candidate_requestStarRule_node_p1;
                                match._node_p2 = candidate_requestStarRule_node_p2;
                                match._node_r2 = candidate_requestStarRule_node_r2;
                                match._edge_h1 = candidate_requestStarRule_edge_h1;
                                match._edge_n = candidate_requestStarRule_edge_n;
                                match._edge_h2 = candidate_requestStarRule_edge_h2;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    candidate_requestStarRule_node_p2.MoveInHeadAfter(candidate_requestStarRule_edge_h2);
                                    candidate_requestStarRule_node_p1.MoveInHeadAfter(candidate_requestStarRule_edge_n);
                                    graph.MoveHeadAfter(candidate_requestStarRule_edge_h1);
                                    candidate_requestStarRule_node_p1.lgspFlags = candidate_requestStarRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_p1;
                                    candidate_requestStarRule_node_r1.lgspFlags = candidate_requestStarRule_node_r1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_r1;
                                    candidate_requestStarRule_edge_h1.lgspFlags = candidate_requestStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
                                    return matches;
                                }
label5: ;
                            }
                            while( (candidate_requestStarRule_edge_h2 = candidate_requestStarRule_edge_h2.lgspInNext) != head_candidate_requestStarRule_edge_h2 );
                        }
                    }
                    while( (candidate_requestStarRule_edge_n = candidate_requestStarRule_edge_n.lgspInNext) != head_candidate_requestStarRule_edge_n );
                }
                candidate_requestStarRule_node_p1.lgspFlags = candidate_requestStarRule_node_p1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_p1;
                candidate_requestStarRule_node_r1.lgspFlags = candidate_requestStarRule_node_r1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_r1;
                candidate_requestStarRule_edge_h1.lgspFlags = candidate_requestStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_requestStarRule.IMatch_requestStarRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_requestStarRule.IMatch_requestStarRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_requestStarRule.IMatch_requestStarRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_requestStarRule.IMatch_requestStarRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_requestStarRule.IMatch_requestStarRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_requestStarRule.IMatch_requestStarRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_requestStarRule.IMatch_requestStarRule>)matches);
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
    public interface IAction_releaseStarRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_releaseStarRule.IMatch_releaseStarRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> matches);
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
    
    public class Action_releaseStarRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_releaseStarRule
    {
        public Action_releaseStarRule() {
            _rulePattern = Rule_releaseStarRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_releaseStarRule.Match_releaseStarRule, Rule_releaseStarRule.IMatch_releaseStarRule>(this);
        }

        public Rule_releaseStarRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "releaseStarRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_releaseStarRule.Match_releaseStarRule, Rule_releaseStarRule.IMatch_releaseStarRule> matches;

        public static Action_releaseStarRule Instance { get { return instance; } }
        private static Action_releaseStarRule instance = new Action_releaseStarRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup releaseStarRule_edge_h1 
            int type_id_candidate_releaseStarRule_edge_h1 = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_releaseStarRule_edge_h1 = graph.edgesByTypeHeads[type_id_candidate_releaseStarRule_edge_h1], candidate_releaseStarRule_edge_h1 = head_candidate_releaseStarRule_edge_h1.lgspTypeNext; candidate_releaseStarRule_edge_h1 != head_candidate_releaseStarRule_edge_h1; candidate_releaseStarRule_edge_h1 = candidate_releaseStarRule_edge_h1.lgspTypeNext)
            {
                uint prev__candidate_releaseStarRule_edge_h1;
                prev__candidate_releaseStarRule_edge_h1 = candidate_releaseStarRule_edge_h1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_releaseStarRule_edge_h1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source releaseStarRule_node_r1 from releaseStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_r1 = candidate_releaseStarRule_edge_h1.lgspSource;
                if(candidate_releaseStarRule_node_r1.lgspType.TypeID!=2) {
                    candidate_releaseStarRule_edge_h1.lgspFlags = candidate_releaseStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_releaseStarRule_node_r1;
                prev__candidate_releaseStarRule_node_r1 = candidate_releaseStarRule_node_r1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_releaseStarRule_node_r1.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target releaseStarRule_node_p2 from releaseStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_p2 = candidate_releaseStarRule_edge_h1.lgspTarget;
                if(candidate_releaseStarRule_node_p2.lgspType.TypeID!=1) {
                    candidate_releaseStarRule_node_r1.lgspFlags = candidate_releaseStarRule_node_r1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_r1;
                    candidate_releaseStarRule_edge_h1.lgspFlags = candidate_releaseStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_releaseStarRule_node_p2;
                prev__candidate_releaseStarRule_node_p2 = candidate_releaseStarRule_node_p2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_releaseStarRule_node_p2.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming releaseStarRule_edge_rq from releaseStarRule_node_r1 
                GRGEN_LGSP.LGSPEdge head_candidate_releaseStarRule_edge_rq = candidate_releaseStarRule_node_r1.lgspInhead;
                if(head_candidate_releaseStarRule_edge_rq != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_releaseStarRule_edge_rq = head_candidate_releaseStarRule_edge_rq;
                    do
                    {
                        if(candidate_releaseStarRule_edge_rq.lgspType.TypeID!=8) {
                            continue;
                        }
                        // Implicit Source releaseStarRule_node_p1 from releaseStarRule_edge_rq 
                        GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_p1 = candidate_releaseStarRule_edge_rq.lgspSource;
                        if(candidate_releaseStarRule_node_p1.lgspType.TypeID!=1) {
                            continue;
                        }
                        if((candidate_releaseStarRule_node_p1.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Extend Incoming releaseStarRule_edge_h2 from releaseStarRule_node_p2 
                        GRGEN_LGSP.LGSPEdge head_candidate_releaseStarRule_edge_h2 = candidate_releaseStarRule_node_p2.lgspInhead;
                        if(head_candidate_releaseStarRule_edge_h2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_releaseStarRule_edge_h2 = head_candidate_releaseStarRule_edge_h2;
                            do
                            {
                                if(candidate_releaseStarRule_edge_h2.lgspType.TypeID!=5) {
                                    continue;
                                }
                                if((candidate_releaseStarRule_edge_h2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // Implicit Source releaseStarRule_node_r2 from releaseStarRule_edge_h2 
                                GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_r2 = candidate_releaseStarRule_edge_h2.lgspSource;
                                if(candidate_releaseStarRule_node_r2.lgspType.TypeID!=2) {
                                    continue;
                                }
                                if((candidate_releaseStarRule_node_r2.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                Rule_releaseStarRule.Match_releaseStarRule match = matches.GetNextUnfilledPosition();
                                match._node_p1 = candidate_releaseStarRule_node_p1;
                                match._node_r1 = candidate_releaseStarRule_node_r1;
                                match._node_p2 = candidate_releaseStarRule_node_p2;
                                match._node_r2 = candidate_releaseStarRule_node_r2;
                                match._edge_rq = candidate_releaseStarRule_edge_rq;
                                match._edge_h1 = candidate_releaseStarRule_edge_h1;
                                match._edge_h2 = candidate_releaseStarRule_edge_h2;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    candidate_releaseStarRule_node_p2.MoveInHeadAfter(candidate_releaseStarRule_edge_h2);
                                    candidate_releaseStarRule_node_r1.MoveInHeadAfter(candidate_releaseStarRule_edge_rq);
                                    graph.MoveHeadAfter(candidate_releaseStarRule_edge_h1);
                                    candidate_releaseStarRule_node_p2.lgspFlags = candidate_releaseStarRule_node_p2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_p2;
                                    candidate_releaseStarRule_node_r1.lgspFlags = candidate_releaseStarRule_node_r1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_r1;
                                    candidate_releaseStarRule_edge_h1.lgspFlags = candidate_releaseStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
                                    return matches;
                                }
                            }
                            while( (candidate_releaseStarRule_edge_h2 = candidate_releaseStarRule_edge_h2.lgspInNext) != head_candidate_releaseStarRule_edge_h2 );
                        }
                    }
                    while( (candidate_releaseStarRule_edge_rq = candidate_releaseStarRule_edge_rq.lgspInNext) != head_candidate_releaseStarRule_edge_rq );
                }
                candidate_releaseStarRule_node_p2.lgspFlags = candidate_releaseStarRule_node_p2.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_p2;
                candidate_releaseStarRule_node_r1.lgspFlags = candidate_releaseStarRule_node_r1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_r1;
                candidate_releaseStarRule_edge_h1.lgspFlags = candidate_releaseStarRule_edge_h1.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_releaseStarRule.IMatch_releaseStarRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_releaseStarRule.IMatch_releaseStarRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_releaseStarRule.IMatch_releaseStarRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_releaseStarRule.IMatch_releaseStarRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_releaseStarRule.IMatch_releaseStarRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_releaseStarRule.IMatch_releaseStarRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_releaseStarRule.IMatch_releaseStarRule>)matches);
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
    public interface IAction_requestSimpleRule
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_requestSimpleRule.IMatch_requestSimpleRule match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> matches);
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
    
    public class Action_requestSimpleRule : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_requestSimpleRule
    {
        public Action_requestSimpleRule() {
            _rulePattern = Rule_requestSimpleRule.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_requestSimpleRule.Match_requestSimpleRule, Rule_requestSimpleRule.IMatch_requestSimpleRule>(this);
        }

        public Rule_requestSimpleRule _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "requestSimpleRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_requestSimpleRule.Match_requestSimpleRule, Rule_requestSimpleRule.IMatch_requestSimpleRule> matches;

        public static Action_requestSimpleRule Instance { get { return instance; } }
        private static Action_requestSimpleRule instance = new Action_requestSimpleRule();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup requestSimpleRule_edge_t 
            int type_id_candidate_requestSimpleRule_edge_t = 6;
            for(GRGEN_LGSP.LGSPEdge head_candidate_requestSimpleRule_edge_t = graph.edgesByTypeHeads[type_id_candidate_requestSimpleRule_edge_t], candidate_requestSimpleRule_edge_t = head_candidate_requestSimpleRule_edge_t.lgspTypeNext; candidate_requestSimpleRule_edge_t != head_candidate_requestSimpleRule_edge_t; candidate_requestSimpleRule_edge_t = candidate_requestSimpleRule_edge_t.lgspTypeNext)
            {
                // Implicit Source requestSimpleRule_node_r from requestSimpleRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_requestSimpleRule_node_r = candidate_requestSimpleRule_edge_t.lgspSource;
                if(candidate_requestSimpleRule_node_r.lgspType.TypeID!=2) {
                    continue;
                }
                // Implicit Target requestSimpleRule_node_p from requestSimpleRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_requestSimpleRule_node_p = candidate_requestSimpleRule_edge_t.lgspTarget;
                if(candidate_requestSimpleRule_node_p.lgspType.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Outgoing requestSimpleRule_neg_0_edge_req from requestSimpleRule_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_requestSimpleRule_neg_0_edge_req = candidate_requestSimpleRule_node_p.lgspOuthead;
                    if(head_candidate_requestSimpleRule_neg_0_edge_req != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_requestSimpleRule_neg_0_edge_req = head_candidate_requestSimpleRule_neg_0_edge_req;
                        do
                        {
                            if(candidate_requestSimpleRule_neg_0_edge_req.lgspType.TypeID!=8) {
                                continue;
                            }
                            if(candidate_requestSimpleRule_neg_0_edge_req.lgspTarget != candidate_requestSimpleRule_node_r) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label6;
                        }
                        while( (candidate_requestSimpleRule_neg_0_edge_req = candidate_requestSimpleRule_neg_0_edge_req.lgspOutNext) != head_candidate_requestSimpleRule_neg_0_edge_req );
                    }
                    --negLevel;
                }
                Rule_requestSimpleRule.Match_requestSimpleRule match = matches.GetNextUnfilledPosition();
                match._node_r = candidate_requestSimpleRule_node_r;
                match._node_p = candidate_requestSimpleRule_node_p;
                match._edge_t = candidate_requestSimpleRule_edge_t;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_requestSimpleRule_edge_t);
                    return matches;
                }
label6: ;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_requestSimpleRule.IMatch_requestSimpleRule match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_requestSimpleRule.IMatch_requestSimpleRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_requestSimpleRule.IMatch_requestSimpleRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_requestSimpleRule.IMatch_requestSimpleRule match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_requestSimpleRule.IMatch_requestSimpleRule match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_requestSimpleRule.IMatch_requestSimpleRule)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_requestSimpleRule.IMatch_requestSimpleRule>)matches);
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
    public interface IAction_aux_attachResource
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> Match(GRGEN_LIBGR.IGraph graph, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IGraph graph, Rule_aux_attachResource.IMatch_aux_attachResource match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> matches);
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
    
    public class Action_aux_attachResource : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_aux_attachResource
    {
        public Action_aux_attachResource() {
            _rulePattern = Rule_aux_attachResource.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_aux_attachResource.Match_aux_attachResource, Rule_aux_attachResource.IMatch_aux_attachResource>(this);
        }

        public Rule_aux_attachResource _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "aux_attachResource"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_aux_attachResource.Match_aux_attachResource, Rule_aux_attachResource.IMatch_aux_attachResource> matches;

        public static Action_aux_attachResource Instance { get { return instance; } }
        private static Action_aux_attachResource instance = new Action_aux_attachResource();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup aux_attachResource_node_p 
            int type_id_candidate_aux_attachResource_node_p = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_aux_attachResource_node_p = graph.nodesByTypeHeads[type_id_candidate_aux_attachResource_node_p], candidate_aux_attachResource_node_p = head_candidate_aux_attachResource_node_p.lgspTypeNext; candidate_aux_attachResource_node_p != head_candidate_aux_attachResource_node_p; candidate_aux_attachResource_node_p = candidate_aux_attachResource_node_p.lgspTypeNext)
            {
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Incoming aux_attachResource_neg_0_edge__edge0 from aux_attachResource_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_aux_attachResource_neg_0_edge__edge0 = candidate_aux_attachResource_node_p.lgspInhead;
                    if(head_candidate_aux_attachResource_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_aux_attachResource_neg_0_edge__edge0 = head_candidate_aux_attachResource_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_aux_attachResource_neg_0_edge__edge0.lgspType.TypeID!=5) {
                                continue;
                            }
                            // Implicit Source aux_attachResource_neg_0_node_r from aux_attachResource_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_aux_attachResource_neg_0_node_r = candidate_aux_attachResource_neg_0_edge__edge0.lgspSource;
                            if(candidate_aux_attachResource_neg_0_node_r.lgspType.TypeID!=2) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label7;
                        }
                        while( (candidate_aux_attachResource_neg_0_edge__edge0 = candidate_aux_attachResource_neg_0_edge__edge0.lgspInNext) != head_candidate_aux_attachResource_neg_0_edge__edge0 );
                    }
                    --negLevel;
                }
                Rule_aux_attachResource.Match_aux_attachResource match = matches.GetNextUnfilledPosition();
                match._node_p = candidate_aux_attachResource_node_p;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_aux_attachResource_node_p);
                    return matches;
                }
label7: ;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> MatchInvoker(GRGEN_LGSP.LGSPGraph graph, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> Match(GRGEN_LIBGR.IGraph graph, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IGraph graph, Rule_aux_attachResource.IMatch_aux_attachResource match)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> matches)
        {
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_aux_attachResource.IMatch_aux_attachResource match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_aux_attachResource.IMatch_aux_attachResource match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, maxMatches);
            if(matches.Count <= 0) return false;
            if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) {
                foreach(Rule_aux_attachResource.IMatch_aux_attachResource match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, match);
            } else {
                foreach(Rule_aux_attachResource.IMatch_aux_attachResource match in matches) _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, match);
            }
            return true;
        }
        public bool ApplyStar(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return true;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IGraph graph)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            if(matches.Count <= 0) return false;
            
            do
            {
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IGraph graph, int min, int max)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPGraph)graph, 1);
                if(matches.Count <= 0) return i >= min;
                if(!graph.TransactionManager.TransactionActive && graph.ReuseOptimization) _rulePattern.Modify((GRGEN_LGSP.LGSPGraph)graph, matches.First);
                else _rulePattern.ModifyNoReuse((GRGEN_LGSP.LGSPGraph)graph, matches.First);
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
            
            Modify(graph, (Rule_aux_attachResource.IMatch_aux_attachResource)match);
            return ReturnArray;
        }
        public object[] ModifyAll(GRGEN_LIBGR.IGraph graph, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(graph, (GRGEN_LIBGR.IMatchesExact<Rule_aux_attachResource.IMatch_aux_attachResource>)matches);
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
    public class MutexPimpedActions : GRGEN_LGSP.LGSPActions
    {
        public MutexPimpedActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public MutexPimpedActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Rule_newRule.Instance);
            actions.Add("newRule", (GRGEN_LGSP.LGSPAction) Action_newRule.Instance);
            @newRule = Action_newRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_killRule.Instance);
            actions.Add("killRule", (GRGEN_LGSP.LGSPAction) Action_killRule.Instance);
            @killRule = Action_killRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_mountRule.Instance);
            actions.Add("mountRule", (GRGEN_LGSP.LGSPAction) Action_mountRule.Instance);
            @mountRule = Action_mountRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_unmountRule.Instance);
            actions.Add("unmountRule", (GRGEN_LGSP.LGSPAction) Action_unmountRule.Instance);
            @unmountRule = Action_unmountRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_passRule.Instance);
            actions.Add("passRule", (GRGEN_LGSP.LGSPAction) Action_passRule.Instance);
            @passRule = Action_passRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_requestRule.Instance);
            actions.Add("requestRule", (GRGEN_LGSP.LGSPAction) Action_requestRule.Instance);
            @requestRule = Action_requestRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_takeRule.Instance);
            actions.Add("takeRule", (GRGEN_LGSP.LGSPAction) Action_takeRule.Instance);
            @takeRule = Action_takeRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_releaseRule.Instance);
            actions.Add("releaseRule", (GRGEN_LGSP.LGSPAction) Action_releaseRule.Instance);
            @releaseRule = Action_releaseRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_giveRule.Instance);
            actions.Add("giveRule", (GRGEN_LGSP.LGSPAction) Action_giveRule.Instance);
            @giveRule = Action_giveRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_blockedRule.Instance);
            actions.Add("blockedRule", (GRGEN_LGSP.LGSPAction) Action_blockedRule.Instance);
            @blockedRule = Action_blockedRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_waitingRule.Instance);
            actions.Add("waitingRule", (GRGEN_LGSP.LGSPAction) Action_waitingRule.Instance);
            @waitingRule = Action_waitingRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_ignoreRule.Instance);
            actions.Add("ignoreRule", (GRGEN_LGSP.LGSPAction) Action_ignoreRule.Instance);
            @ignoreRule = Action_ignoreRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_unlockRule.Instance);
            actions.Add("unlockRule", (GRGEN_LGSP.LGSPAction) Action_unlockRule.Instance);
            @unlockRule = Action_unlockRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_requestStarRule.Instance);
            actions.Add("requestStarRule", (GRGEN_LGSP.LGSPAction) Action_requestStarRule.Instance);
            @requestStarRule = Action_requestStarRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_releaseStarRule.Instance);
            actions.Add("releaseStarRule", (GRGEN_LGSP.LGSPAction) Action_releaseStarRule.Instance);
            @releaseStarRule = Action_releaseStarRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_requestSimpleRule.Instance);
            actions.Add("requestSimpleRule", (GRGEN_LGSP.LGSPAction) Action_requestSimpleRule.Instance);
            @requestSimpleRule = Action_requestSimpleRule.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_aux_attachResource.Instance);
            actions.Add("aux_attachResource", (GRGEN_LGSP.LGSPAction) Action_aux_attachResource.Instance);
            @aux_attachResource = Action_aux_attachResource.Instance;
            analyzer.ComputeInterPatternRelations();
        }
        
        public IAction_newRule @newRule;
        public IAction_killRule @killRule;
        public IAction_mountRule @mountRule;
        public IAction_unmountRule @unmountRule;
        public IAction_passRule @passRule;
        public IAction_requestRule @requestRule;
        public IAction_takeRule @takeRule;
        public IAction_releaseRule @releaseRule;
        public IAction_giveRule @giveRule;
        public IAction_blockedRule @blockedRule;
        public IAction_waitingRule @waitingRule;
        public IAction_ignoreRule @ignoreRule;
        public IAction_unlockRule @unlockRule;
        public IAction_requestStarRule @requestStarRule;
        public IAction_releaseStarRule @releaseStarRule;
        public IAction_requestSimpleRule @requestSimpleRule;
        public IAction_aux_attachResource @aux_attachResource;
        
        public override string Name { get { return "MutexPimpedActions"; } }
        public override string ModelMD5Hash { get { return "e0f6366a053bc32a0af0fbc6d2bd0c9a"; } }
    }
}