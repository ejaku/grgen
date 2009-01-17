// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\Mutex\MutexPimped.grg" on Sat Jan 17 01:17:19 CET 2009

using System;
using System.Collections.Generic;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using de.unika.ipd.grGen.Model_Mutex;

namespace de.unika.ipd.grGen.Action_MutexPimped
{
	public class Rule_newRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_newRule instance = null;
		public static Rule_newRule Instance { get { if (instance==null) { instance = new Rule_newRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_newRule;


#if INITIAL_WARMUP
		public Rule_newRule()
#else
		private Rule_newRule()
#endif
		{
			name = "newRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] newRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] newRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode newRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "newRule_node_p1", "p1", newRule_node_p1_AllowedTypes, newRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode newRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "newRule_node_p2", "p2", newRule_node_p2_AllowedTypes, newRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge newRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@next, "Inext", "newRule_edge_n", "n", newRule_edge_n_AllowedTypes, newRule_edge_n_IsAllowedType, 1.0F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_newRule curMatch = (Match_newRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_n = curMatch._edge_n;
			graph.SettingAddedNodeNames( newRule_addedNodeNames );
			@Process node_p = @Process.CreateNode(graph);
			graph.SettingAddedEdgeNames( newRule_addedEdgeNames );
			@next edge_n1;
			if(edge_n.type == EdgeType_next.typeVar)
			{
				// re-using edge_n as edge_n1
				edge_n1 = (@next) edge_n;
				graph.ReuseEdge(edge_n, null, node_p);
			}
			else
			{
				graph.Remove(edge_n);
				edge_n1 = @next.CreateEdge(graph, node_p1, node_p);
			}
			@next edge_n2 = @next.CreateEdge(graph, node_p, node_p2);
			return EmptyReturnElements;
		}
		private static String[] newRule_addedNodeNames = new String[] { "p" };
		private static String[] newRule_addedEdgeNames = new String[] { "n1", "n2" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_newRule curMatch = (Match_newRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_n = curMatch._edge_n;
			graph.SettingAddedNodeNames( newRule_addedNodeNames );
			@Process node_p = @Process.CreateNode(graph);
			graph.SettingAddedEdgeNames( newRule_addedEdgeNames );
			@next edge_n1 = @next.CreateEdge(graph, node_p1, node_p);
			@next edge_n2 = @next.CreateEdge(graph, node_p, node_p2);
			graph.Remove(edge_n);
			return EmptyReturnElements;
		}

		static Rule_newRule() {
		}

		public interface IMatch_newRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p1 { get; }
			IProcess node_p2 { get; }
			//Edges
			Inext edge_n { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_newRule : GRGEN_LGSP.ListElement<Match_newRule>, IMatch_newRule
		{
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
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
			
			public Inext edge_n { get { return (Inext)_edge_n; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_killRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_killRule instance = null;
		public static Rule_killRule Instance { get { if (instance==null) { instance = new Rule_killRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_killRule;


#if INITIAL_WARMUP
		public Rule_killRule()
#else
		private Rule_killRule()
#endif
		{
			name = "killRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode killRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "killRule_node_p1", "p1", killRule_node_p1_AllowedTypes, killRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode killRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "killRule_node_p", "p", killRule_node_p_AllowedTypes, killRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode killRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "killRule_node_p2", "p2", killRule_node_p2_AllowedTypes, killRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge killRule_edge_n1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@next, "Inext", "killRule_edge_n1", "n1", killRule_edge_n1_AllowedTypes, killRule_edge_n1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge killRule_edge_n2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@next, "Inext", "killRule_edge_n2", "n2", killRule_edge_n2_AllowedTypes, killRule_edge_n2_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_killRule curMatch = (Match_killRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_n1 = curMatch._edge_n1;
			GRGEN_LGSP.LGSPEdge edge_n2 = curMatch._edge_n2;
			graph.SettingAddedNodeNames( killRule_addedNodeNames );
			graph.SettingAddedEdgeNames( killRule_addedEdgeNames );
			@next edge_n;
			if(edge_n1.type == EdgeType_next.typeVar)
			{
				// re-using edge_n1 as edge_n
				edge_n = (@next) edge_n1;
				graph.ReuseEdge(edge_n1, null, node_p2);
			}
			else
			{
				graph.Remove(edge_n1);
				edge_n = @next.CreateEdge(graph, node_p1, node_p2);
			}
			graph.Remove(edge_n2);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return EmptyReturnElements;
		}
		private static String[] killRule_addedNodeNames = new String[] {  };
		private static String[] killRule_addedEdgeNames = new String[] { "n" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_killRule curMatch = (Match_killRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_n1 = curMatch._edge_n1;
			GRGEN_LGSP.LGSPEdge edge_n2 = curMatch._edge_n2;
			graph.SettingAddedNodeNames( killRule_addedNodeNames );
			graph.SettingAddedEdgeNames( killRule_addedEdgeNames );
			@next edge_n = @next.CreateEdge(graph, node_p1, node_p2);
			graph.Remove(edge_n1);
			graph.Remove(edge_n2);
			graph.RemoveEdges(node_p);
			graph.Remove(node_p);
			return EmptyReturnElements;
		}

		static Rule_killRule() {
		}

		public interface IMatch_killRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p1 { get; }
			IProcess node_p { get; }
			IProcess node_p2 { get; }
			//Edges
			Inext edge_n1 { get; }
			Inext edge_n2 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_killRule : GRGEN_LGSP.ListElement<Match_killRule>, IMatch_killRule
		{
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
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
			
			public Inext edge_n1 { get { return (Inext)_edge_n1; } }
			public Inext edge_n2 { get { return (Inext)_edge_n2; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_mountRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_mountRule instance = null;
		public static Rule_mountRule Instance { get { if (instance==null) { instance = new Rule_mountRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] mountRule_node_p_AllowedTypes = null;
		public static bool[] mountRule_node_p_IsAllowedType = null;
		public enum mountRule_NodeNums { @p, };
		public enum mountRule_EdgeNums { };
		public enum mountRule_VariableNums { };
		public enum mountRule_SubNums { };
		public enum mountRule_AltNums { };



		GRGEN_LGSP.PatternGraph pat_mountRule;


#if INITIAL_WARMUP
		public Rule_mountRule()
#else
		private Rule_mountRule()
#endif
		{
			name = "mountRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] mountRule_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] mountRule_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode mountRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "mountRule_node_p", "p", mountRule_node_p_AllowedTypes, mountRule_node_p_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_mountRule curMatch = (Match_mountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( mountRule_addedNodeNames );
			@Resource node_r = @Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( mountRule_addedEdgeNames );
			@token edge_t = @token.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] mountRule_addedNodeNames = new String[] { "r" };
		private static String[] mountRule_addedEdgeNames = new String[] { "t" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_mountRule curMatch = (Match_mountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( mountRule_addedNodeNames );
			@Resource node_r = @Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( mountRule_addedEdgeNames );
			@token edge_t = @token.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}

		static Rule_mountRule() {
		}

		public interface IMatch_mountRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_mountRule : GRGEN_LGSP.ListElement<Match_mountRule>, IMatch_mountRule
		{
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_unmountRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_unmountRule instance = null;
		public static Rule_unmountRule Instance { get { if (instance==null) { instance = new Rule_unmountRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_unmountRule;


#if INITIAL_WARMUP
		public Rule_unmountRule()
#else
		private Rule_unmountRule()
#endif
		{
			name = "unmountRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] unmountRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] unmountRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode unmountRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "unmountRule_node_r", "r", unmountRule_node_r_AllowedTypes, unmountRule_node_r_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternNode unmountRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "unmountRule_node_p", "p", unmountRule_node_p_AllowedTypes, unmountRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge unmountRule_edge_t = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@token, "Itoken", "unmountRule_edge_t", "t", unmountRule_edge_t_AllowedTypes, unmountRule_edge_t_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unmountRule curMatch = (Match_unmountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			graph.SettingAddedNodeNames( unmountRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unmountRule_addedEdgeNames );
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
		private static String[] unmountRule_addedNodeNames = new String[] {  };
		private static String[] unmountRule_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unmountRule curMatch = (Match_unmountRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			graph.SettingAddedNodeNames( unmountRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unmountRule_addedEdgeNames );
			graph.Remove(edge_t);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}

		static Rule_unmountRule() {
		}

		public interface IMatch_unmountRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Itoken edge_t { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_unmountRule : GRGEN_LGSP.ListElement<Match_unmountRule>, IMatch_unmountRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Itoken edge_t { get { return (Itoken)_edge_t; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_passRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_passRule instance = null;
		public static Rule_passRule Instance { get { if (instance==null) { instance = new Rule_passRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_passRule;

		public static GRGEN_LIBGR.EdgeType[] passRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] passRule_neg_0_edge_req_IsAllowedType = null;
		public enum passRule_neg_0_NodeNums { @p1, @r, };
		public enum passRule_neg_0_EdgeNums { @req, };
		public enum passRule_neg_0_VariableNums { };
		public enum passRule_neg_0_SubNums { };
		public enum passRule_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph passRule_neg_0;


#if INITIAL_WARMUP
		public Rule_passRule()
#else
		private Rule_passRule()
#endif
		{
			name = "passRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode passRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "passRule_node_r", "r", passRule_node_r_AllowedTypes, passRule_node_r_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternNode passRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "passRule_node_p1", "p1", passRule_node_p1_AllowedTypes, passRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode passRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "passRule_node_p2", "p2", passRule_node_p2_AllowedTypes, passRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge passRule_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@token, "Itoken", "passRule_edge__edge0", "_edge0", passRule_edge__edge0_AllowedTypes, passRule_edge__edge0_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge passRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@next, "Inext", "passRule_edge_n", "n", passRule_edge_n_AllowedTypes, passRule_edge_n_IsAllowedType, 5.5F, -1);
			bool[,] passRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] passRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge passRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "passRule_neg_0_edge_req", "req", passRule_neg_0_edge_req_AllowedTypes, passRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_passRule curMatch = (Match_passRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( passRule_addedNodeNames );
			graph.SettingAddedEdgeNames( passRule_addedEdgeNames );
			@token edge_t;
			if(edge__edge0.type == EdgeType_token.typeVar)
			{
				// re-using edge__edge0 as edge_t
				edge_t = (@token) edge__edge0;
				graph.ReuseEdge(edge__edge0, null, node_p2);
			}
			else
			{
				graph.Remove(edge__edge0);
				edge_t = @token.CreateEdge(graph, node_r, node_p2);
			}
			return EmptyReturnElements;
		}
		private static String[] passRule_addedNodeNames = new String[] {  };
		private static String[] passRule_addedEdgeNames = new String[] { "t" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_passRule curMatch = (Match_passRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge__edge0 = curMatch._edge__edge0;
			graph.SettingAddedNodeNames( passRule_addedNodeNames );
			graph.SettingAddedEdgeNames( passRule_addedEdgeNames );
			@token edge_t = @token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge__edge0);
			return EmptyReturnElements;
		}

		static Rule_passRule() {
		}

		public interface IMatch_passRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p1 { get; }
			IProcess node_p2 { get; }
			//Edges
			Itoken edge__edge0 { get; }
			Inext edge_n { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_passRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p1 { get; }
			IResource node_r { get; }
			//Edges
			Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_passRule : GRGEN_LGSP.ListElement<Match_passRule>, IMatch_passRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
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
			
			public Itoken edge__edge0 { get { return (Itoken)_edge__edge0; } }
			public Inext edge_n { get { return (Inext)_edge_n; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_passRule_neg_0 : GRGEN_LGSP.ListElement<Match_passRule_neg_0>, IMatch_passRule_neg_0
		{
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IResource node_r { get { return (IResource)_node_r; } }
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
			
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_requestRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_requestRule instance = null;
		public static Rule_requestRule Instance { get { if (instance==null) { instance = new Rule_requestRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] requestRule_node_p_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] requestRule_node_r_AllowedTypes = null;
		public static bool[] requestRule_node_p_IsAllowedType = null;
		public static bool[] requestRule_node_r_IsAllowedType = null;
		public enum requestRule_NodeNums { @p, @r, };
		public enum requestRule_EdgeNums { };
		public enum requestRule_VariableNums { };
		public enum requestRule_SubNums { };
		public enum requestRule_AltNums { };



		GRGEN_LGSP.PatternGraph pat_requestRule;

		public static GRGEN_LIBGR.EdgeType[] requestRule_neg_0_edge_hb_AllowedTypes = null;
		public static bool[] requestRule_neg_0_edge_hb_IsAllowedType = null;
		public enum requestRule_neg_0_NodeNums { @r, @p, };
		public enum requestRule_neg_0_EdgeNums { @hb, };
		public enum requestRule_neg_0_VariableNums { };
		public enum requestRule_neg_0_SubNums { };
		public enum requestRule_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph requestRule_neg_0;

		public static GRGEN_LIBGR.NodeType[] requestRule_neg_1_node_m_AllowedTypes = null;
		public static bool[] requestRule_neg_1_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] requestRule_neg_1_edge_req_AllowedTypes = null;
		public static bool[] requestRule_neg_1_edge_req_IsAllowedType = null;
		public enum requestRule_neg_1_NodeNums { @p, @m, };
		public enum requestRule_neg_1_EdgeNums { @req, };
		public enum requestRule_neg_1_VariableNums { };
		public enum requestRule_neg_1_SubNums { };
		public enum requestRule_neg_1_AltNums { };

		GRGEN_LGSP.PatternGraph requestRule_neg_1;


#if INITIAL_WARMUP
		public Rule_requestRule()
#else
		private Rule_requestRule()
#endif
		{
			name = "requestRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] requestRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestRule_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode requestRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "requestRule_node_p", "p", requestRule_node_p_AllowedTypes, requestRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode requestRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "requestRule_node_r", "r", requestRule_node_r_AllowedTypes, requestRule_node_r_IsAllowedType, 1.0F, -1);
			bool[,] requestRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge requestRule_neg_0_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "requestRule_neg_0_edge_hb", "hb", requestRule_neg_0_edge_hb_AllowedTypes, requestRule_neg_0_edge_hb_IsAllowedType, 5.5F, -1);
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
			GRGEN_LGSP.PatternNode requestRule_neg_1_node_m = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "requestRule_neg_1_node_m", "m", requestRule_neg_1_node_m_AllowedTypes, requestRule_neg_1_node_m_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge requestRule_neg_1_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "requestRule_neg_1_edge_req", "req", requestRule_neg_1_edge_req_AllowedTypes, requestRule_neg_1_edge_req_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestRule curMatch = (Match_requestRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestRule_addedEdgeNames );
			@request edge_req = @request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] requestRule_addedNodeNames = new String[] {  };
		private static String[] requestRule_addedEdgeNames = new String[] { "req" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestRule curMatch = (Match_requestRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestRule_addedEdgeNames );
			@request edge_req = @request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}

		static Rule_requestRule() {
		}

		public interface IMatch_requestRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p { get; }
			IResource node_r { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_requestRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_requestRule_neg_1 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p { get; }
			IResource node_m { get; }
			//Edges
			Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_requestRule : GRGEN_LGSP.ListElement<Match_requestRule>, IMatch_requestRule
		{
			public IProcess node_p { get { return (IProcess)_node_p; } }
			public IResource node_r { get { return (IResource)_node_r; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestRule_neg_0 : GRGEN_LGSP.ListElement<Match_requestRule_neg_0>, IMatch_requestRule_neg_0
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Iheld_by edge_hb { get { return (Iheld_by)_edge_hb; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestRule_neg_1 : GRGEN_LGSP.ListElement<Match_requestRule_neg_1>, IMatch_requestRule_neg_1
		{
			public IProcess node_p { get { return (IProcess)_node_p; } }
			public IResource node_m { get { return (IResource)_node_m; } }
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
			
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_takeRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_takeRule instance = null;
		public static Rule_takeRule Instance { get { if (instance==null) { instance = new Rule_takeRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_takeRule;


#if INITIAL_WARMUP
		public Rule_takeRule()
#else
		private Rule_takeRule()
#endif
		{
			name = "takeRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] takeRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] takeRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode takeRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "takeRule_node_r", "r", takeRule_node_r_AllowedTypes, takeRule_node_r_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode takeRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "takeRule_node_p", "p", takeRule_node_p_AllowedTypes, takeRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge takeRule_edge_t = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@token, "Itoken", "takeRule_edge_t", "t", takeRule_edge_t_AllowedTypes, takeRule_edge_t_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternEdge takeRule_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "takeRule_edge_req", "req", takeRule_edge_req_AllowedTypes, takeRule_edge_req_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_takeRule curMatch = (Match_takeRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			GRGEN_LGSP.LGSPEdge edge_req = curMatch._edge_req;
			graph.SettingAddedNodeNames( takeRule_addedNodeNames );
			graph.SettingAddedEdgeNames( takeRule_addedEdgeNames );
			@held_by edge_hb = @held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_t);
			graph.Remove(edge_req);
			return EmptyReturnElements;
		}
		private static String[] takeRule_addedNodeNames = new String[] {  };
		private static String[] takeRule_addedEdgeNames = new String[] { "hb" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_takeRule curMatch = (Match_takeRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_t = curMatch._edge_t;
			GRGEN_LGSP.LGSPEdge edge_req = curMatch._edge_req;
			graph.SettingAddedNodeNames( takeRule_addedNodeNames );
			graph.SettingAddedEdgeNames( takeRule_addedEdgeNames );
			@held_by edge_hb = @held_by.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_t);
			graph.Remove(edge_req);
			return EmptyReturnElements;
		}

		static Rule_takeRule() {
		}

		public interface IMatch_takeRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Itoken edge_t { get; }
			Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_takeRule : GRGEN_LGSP.ListElement<Match_takeRule>, IMatch_takeRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Itoken edge_t { get { return (Itoken)_edge_t; } }
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_releaseRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_releaseRule instance = null;
		public static Rule_releaseRule Instance { get { if (instance==null) { instance = new Rule_releaseRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_releaseRule;

		public static GRGEN_LIBGR.NodeType[] releaseRule_neg_0_node_m_AllowedTypes = null;
		public static bool[] releaseRule_neg_0_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] releaseRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] releaseRule_neg_0_edge_req_IsAllowedType = null;
		public enum releaseRule_neg_0_NodeNums { @p, @m, };
		public enum releaseRule_neg_0_EdgeNums { @req, };
		public enum releaseRule_neg_0_VariableNums { };
		public enum releaseRule_neg_0_SubNums { };
		public enum releaseRule_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph releaseRule_neg_0;


#if INITIAL_WARMUP
		public Rule_releaseRule()
#else
		private Rule_releaseRule()
#endif
		{
			name = "releaseRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] releaseRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] releaseRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode releaseRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "releaseRule_node_r", "r", releaseRule_node_r_AllowedTypes, releaseRule_node_r_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode releaseRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "releaseRule_node_p", "p", releaseRule_node_p_AllowedTypes, releaseRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge releaseRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "releaseRule_edge_hb", "hb", releaseRule_edge_hb_AllowedTypes, releaseRule_edge_hb_IsAllowedType, 1.0F, -1);
			bool[,] releaseRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] releaseRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode releaseRule_neg_0_node_m = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "releaseRule_neg_0_node_m", "m", releaseRule_neg_0_node_m_AllowedTypes, releaseRule_neg_0_node_m_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge releaseRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "releaseRule_neg_0_edge_req", "req", releaseRule_neg_0_edge_req_AllowedTypes, releaseRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseRule curMatch = (Match_releaseRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( releaseRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseRule_addedEdgeNames );
			@release edge_rel = @release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
		private static String[] releaseRule_addedNodeNames = new String[] {  };
		private static String[] releaseRule_addedEdgeNames = new String[] { "rel" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseRule curMatch = (Match_releaseRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( releaseRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseRule_addedEdgeNames );
			@release edge_rel = @release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}

		static Rule_releaseRule() {
		}

		public interface IMatch_releaseRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_releaseRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p { get; }
			IResource node_m { get; }
			//Edges
			Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_releaseRule : GRGEN_LGSP.ListElement<Match_releaseRule>, IMatch_releaseRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Iheld_by edge_hb { get { return (Iheld_by)_edge_hb; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_releaseRule_neg_0 : GRGEN_LGSP.ListElement<Match_releaseRule_neg_0>, IMatch_releaseRule_neg_0
		{
			public IProcess node_p { get { return (IProcess)_node_p; } }
			public IResource node_m { get { return (IResource)_node_m; } }
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
			
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_giveRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_giveRule instance = null;
		public static Rule_giveRule Instance { get { if (instance==null) { instance = new Rule_giveRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_giveRule;


#if INITIAL_WARMUP
		public Rule_giveRule()
#else
		private Rule_giveRule()
#endif
		{
			name = "giveRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode giveRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "giveRule_node_r", "r", giveRule_node_r_AllowedTypes, giveRule_node_r_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode giveRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "giveRule_node_p1", "p1", giveRule_node_p1_AllowedTypes, giveRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode giveRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "giveRule_node_p2", "p2", giveRule_node_p2_AllowedTypes, giveRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge giveRule_edge_rel = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@release, "Irelease", "giveRule_edge_rel", "rel", giveRule_edge_rel_AllowedTypes, giveRule_edge_rel_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternEdge giveRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@next, "Inext", "giveRule_edge_n", "n", giveRule_edge_n_AllowedTypes, giveRule_edge_n_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_giveRule curMatch = (Match_giveRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_rel = curMatch._edge_rel;
			graph.SettingAddedNodeNames( giveRule_addedNodeNames );
			graph.SettingAddedEdgeNames( giveRule_addedEdgeNames );
			@token edge_t = @token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return EmptyReturnElements;
		}
		private static String[] giveRule_addedNodeNames = new String[] {  };
		private static String[] giveRule_addedEdgeNames = new String[] { "t" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_giveRule curMatch = (Match_giveRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_rel = curMatch._edge_rel;
			graph.SettingAddedNodeNames( giveRule_addedNodeNames );
			graph.SettingAddedEdgeNames( giveRule_addedEdgeNames );
			@token edge_t = @token.CreateEdge(graph, node_r, node_p2);
			graph.Remove(edge_rel);
			return EmptyReturnElements;
		}

		static Rule_giveRule() {
		}

		public interface IMatch_giveRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p1 { get; }
			IProcess node_p2 { get; }
			//Edges
			Irelease edge_rel { get; }
			Inext edge_n { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_giveRule : GRGEN_LGSP.ListElement<Match_giveRule>, IMatch_giveRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
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
			
			public Irelease edge_rel { get { return (Irelease)_edge_rel; } }
			public Inext edge_n { get { return (Inext)_edge_n; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_blockedRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_blockedRule instance = null;
		public static Rule_blockedRule Instance { get { if (instance==null) { instance = new Rule_blockedRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_blockedRule;


#if INITIAL_WARMUP
		public Rule_blockedRule()
#else
		private Rule_blockedRule()
#endif
		{
			name = "blockedRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode blockedRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "blockedRule_node_p1", "p1", blockedRule_node_p1_AllowedTypes, blockedRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode blockedRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "blockedRule_node_r", "r", blockedRule_node_r_AllowedTypes, blockedRule_node_r_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternNode blockedRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "blockedRule_node_p2", "p2", blockedRule_node_p2_AllowedTypes, blockedRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge blockedRule_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "blockedRule_edge_req", "req", blockedRule_edge_req_AllowedTypes, blockedRule_edge_req_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge blockedRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "blockedRule_edge_hb", "hb", blockedRule_edge_hb_AllowedTypes, blockedRule_edge_hb_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_blockedRule curMatch = (Match_blockedRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			graph.SettingAddedNodeNames( blockedRule_addedNodeNames );
			graph.SettingAddedEdgeNames( blockedRule_addedEdgeNames );
			@blocked edge_b = @blocked.CreateEdge(graph, node_r, node_p1);
			return EmptyReturnElements;
		}
		private static String[] blockedRule_addedNodeNames = new String[] {  };
		private static String[] blockedRule_addedEdgeNames = new String[] { "b" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_blockedRule curMatch = (Match_blockedRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			graph.SettingAddedNodeNames( blockedRule_addedNodeNames );
			graph.SettingAddedEdgeNames( blockedRule_addedEdgeNames );
			@blocked edge_b = @blocked.CreateEdge(graph, node_r, node_p1);
			return EmptyReturnElements;
		}

		static Rule_blockedRule() {
		}

		public interface IMatch_blockedRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p1 { get; }
			IResource node_r { get; }
			IProcess node_p2 { get; }
			//Edges
			Irequest edge_req { get; }
			Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_blockedRule : GRGEN_LGSP.ListElement<Match_blockedRule>, IMatch_blockedRule
		{
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
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
			
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
			public Iheld_by edge_hb { get { return (Iheld_by)_edge_hb; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_waitingRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_waitingRule instance = null;
		public static Rule_waitingRule Instance { get { if (instance==null) { instance = new Rule_waitingRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_waitingRule;


#if INITIAL_WARMUP
		public Rule_waitingRule()
#else
		private Rule_waitingRule()
#endif
		{
			name = "waitingRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode waitingRule_node_r2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "waitingRule_node_r2", "r2", waitingRule_node_r2_AllowedTypes, waitingRule_node_r2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode waitingRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "waitingRule_node_p1", "p1", waitingRule_node_p1_AllowedTypes, waitingRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode waitingRule_node_r1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "waitingRule_node_r1", "r1", waitingRule_node_r1_AllowedTypes, waitingRule_node_r1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode waitingRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "waitingRule_node_p2", "p2", waitingRule_node_p2_AllowedTypes, waitingRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode waitingRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "waitingRule_node_r", "r", waitingRule_node_r_AllowedTypes, waitingRule_node_r_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternEdge waitingRule_edge_b = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@blocked, "Iblocked", "waitingRule_edge_b", "b", waitingRule_edge_b_AllowedTypes, waitingRule_edge_b_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge waitingRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "waitingRule_edge_hb", "hb", waitingRule_edge_hb_AllowedTypes, waitingRule_edge_hb_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge waitingRule_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "waitingRule_edge_req", "req", waitingRule_edge_req_AllowedTypes, waitingRule_edge_req_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_waitingRule curMatch = (Match_waitingRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( waitingRule_addedNodeNames );
			graph.SettingAddedEdgeNames( waitingRule_addedEdgeNames );
			@blocked edge_bn;
			if(edge_b.type == EdgeType_blocked.typeVar)
			{
				// re-using edge_b as edge_bn
				edge_bn = (@blocked) edge_b;
				graph.ReuseEdge(edge_b, null, node_p2);
			}
			else
			{
				graph.Remove(edge_b);
				edge_bn = @blocked.CreateEdge(graph, node_r2, node_p2);
			}
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}
		private static String[] waitingRule_addedNodeNames = new String[] {  };
		private static String[] waitingRule_addedEdgeNames = new String[] { "bn" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_waitingRule curMatch = (Match_waitingRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( waitingRule_addedNodeNames );
			graph.SettingAddedEdgeNames( waitingRule_addedEdgeNames );
			@blocked edge_bn = @blocked.CreateEdge(graph, node_r2, node_p2);
			graph.Remove(edge_b);
			graph.RemoveEdges(node_r);
			graph.Remove(node_r);
			return EmptyReturnElements;
		}

		static Rule_waitingRule() {
		}

		public interface IMatch_waitingRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r2 { get; }
			IProcess node_p1 { get; }
			IResource node_r1 { get; }
			IProcess node_p2 { get; }
			IResource node_r { get; }
			//Edges
			Iblocked edge_b { get; }
			Iheld_by edge_hb { get; }
			Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_waitingRule : GRGEN_LGSP.ListElement<Match_waitingRule>, IMatch_waitingRule
		{
			public IResource node_r2 { get { return (IResource)_node_r2; } }
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IResource node_r1 { get { return (IResource)_node_r1; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
			public IResource node_r { get { return (IResource)_node_r; } }
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
			
			public Iblocked edge_b { get { return (Iblocked)_edge_b; } }
			public Iheld_by edge_hb { get { return (Iheld_by)_edge_hb; } }
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_ignoreRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ignoreRule instance = null;
		public static Rule_ignoreRule Instance { get { if (instance==null) { instance = new Rule_ignoreRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_ignoreRule;

		public static GRGEN_LIBGR.NodeType[] ignoreRule_neg_0_node_m_AllowedTypes = null;
		public static bool[] ignoreRule_neg_0_node_m_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] ignoreRule_neg_0_edge_hb_AllowedTypes = null;
		public static bool[] ignoreRule_neg_0_edge_hb_IsAllowedType = null;
		public enum ignoreRule_neg_0_NodeNums { @m, @p, };
		public enum ignoreRule_neg_0_EdgeNums { @hb, };
		public enum ignoreRule_neg_0_VariableNums { };
		public enum ignoreRule_neg_0_SubNums { };
		public enum ignoreRule_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph ignoreRule_neg_0;


#if INITIAL_WARMUP
		public Rule_ignoreRule()
#else
		private Rule_ignoreRule()
#endif
		{
			name = "ignoreRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] ignoreRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ignoreRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode ignoreRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "ignoreRule_node_r", "r", ignoreRule_node_r_AllowedTypes, ignoreRule_node_r_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternNode ignoreRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "ignoreRule_node_p", "p", ignoreRule_node_p_AllowedTypes, ignoreRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ignoreRule_edge_b = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@blocked, "Iblocked", "ignoreRule_edge_b", "b", ignoreRule_edge_b_AllowedTypes, ignoreRule_edge_b_IsAllowedType, 5.5F, -1);
			bool[,] ignoreRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ignoreRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode ignoreRule_neg_0_node_m = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "ignoreRule_neg_0_node_m", "m", ignoreRule_neg_0_node_m_AllowedTypes, ignoreRule_neg_0_node_m_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge ignoreRule_neg_0_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "ignoreRule_neg_0_edge_hb", "hb", ignoreRule_neg_0_edge_hb_AllowedTypes, ignoreRule_neg_0_edge_hb_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ignoreRule curMatch = (Match_ignoreRule)_curMatch;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( ignoreRule_addedNodeNames );
			graph.SettingAddedEdgeNames( ignoreRule_addedEdgeNames );
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}
		private static String[] ignoreRule_addedNodeNames = new String[] {  };
		private static String[] ignoreRule_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_ignoreRule curMatch = (Match_ignoreRule)_curMatch;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			graph.SettingAddedNodeNames( ignoreRule_addedNodeNames );
			graph.SettingAddedEdgeNames( ignoreRule_addedEdgeNames );
			graph.Remove(edge_b);
			return EmptyReturnElements;
		}

		static Rule_ignoreRule() {
		}

		public interface IMatch_ignoreRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Iblocked edge_b { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_ignoreRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_m { get; }
			IProcess node_p { get; }
			//Edges
			Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_ignoreRule : GRGEN_LGSP.ListElement<Match_ignoreRule>, IMatch_ignoreRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Iblocked edge_b { get { return (Iblocked)_edge_b; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_ignoreRule_neg_0 : GRGEN_LGSP.ListElement<Match_ignoreRule_neg_0>, IMatch_ignoreRule_neg_0
		{
			public IResource node_m { get { return (IResource)_node_m; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Iheld_by edge_hb { get { return (Iheld_by)_edge_hb; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_unlockRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_unlockRule instance = null;
		public static Rule_unlockRule Instance { get { if (instance==null) { instance = new Rule_unlockRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_unlockRule;


#if INITIAL_WARMUP
		public Rule_unlockRule()
#else
		private Rule_unlockRule()
#endif
		{
			name = "unlockRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] unlockRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] unlockRule_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			GRGEN_LGSP.PatternNode unlockRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "unlockRule_node_r", "r", unlockRule_node_r_AllowedTypes, unlockRule_node_r_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternNode unlockRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "unlockRule_node_p", "p", unlockRule_node_p_AllowedTypes, unlockRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge unlockRule_edge_b = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@blocked, "Iblocked", "unlockRule_edge_b", "b", unlockRule_edge_b_AllowedTypes, unlockRule_edge_b_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge unlockRule_edge_hb = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "unlockRule_edge_hb", "hb", unlockRule_edge_hb_AllowedTypes, unlockRule_edge_hb_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unlockRule curMatch = (Match_unlockRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( unlockRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unlockRule_addedEdgeNames );
			@release edge_rel = @release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}
		private static String[] unlockRule_addedNodeNames = new String[] {  };
		private static String[] unlockRule_addedEdgeNames = new String[] { "rel" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_unlockRule curMatch = (Match_unlockRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPEdge edge_b = curMatch._edge_b;
			GRGEN_LGSP.LGSPEdge edge_hb = curMatch._edge_hb;
			graph.SettingAddedNodeNames( unlockRule_addedNodeNames );
			graph.SettingAddedEdgeNames( unlockRule_addedEdgeNames );
			@release edge_rel = @release.CreateEdge(graph, node_r, node_p);
			graph.Remove(edge_b);
			graph.Remove(edge_hb);
			return EmptyReturnElements;
		}

		static Rule_unlockRule() {
		}

		public interface IMatch_unlockRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Iblocked edge_b { get; }
			Iheld_by edge_hb { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_unlockRule : GRGEN_LGSP.ListElement<Match_unlockRule>, IMatch_unlockRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Iblocked edge_b { get { return (Iblocked)_edge_b; } }
			public Iheld_by edge_hb { get { return (Iheld_by)_edge_hb; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_requestStarRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_requestStarRule instance = null;
		public static Rule_requestStarRule Instance { get { if (instance==null) { instance = new Rule_requestStarRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_requestStarRule;

		public static GRGEN_LIBGR.EdgeType[] requestStarRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] requestStarRule_neg_0_edge_req_IsAllowedType = null;
		public enum requestStarRule_neg_0_NodeNums { @p1, @r2, };
		public enum requestStarRule_neg_0_EdgeNums { @req, };
		public enum requestStarRule_neg_0_VariableNums { };
		public enum requestStarRule_neg_0_SubNums { };
		public enum requestStarRule_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph requestStarRule_neg_0;


#if INITIAL_WARMUP
		public Rule_requestStarRule()
#else
		private Rule_requestStarRule()
#endif
		{
			name = "requestStarRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode requestStarRule_node_r1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "requestStarRule_node_r1", "r1", requestStarRule_node_r1_AllowedTypes, requestStarRule_node_r1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode requestStarRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "requestStarRule_node_p1", "p1", requestStarRule_node_p1_AllowedTypes, requestStarRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode requestStarRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "requestStarRule_node_p2", "p2", requestStarRule_node_p2_AllowedTypes, requestStarRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode requestStarRule_node_r2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "requestStarRule_node_r2", "r2", requestStarRule_node_r2_AllowedTypes, requestStarRule_node_r2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge requestStarRule_edge_h1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "requestStarRule_edge_h1", "h1", requestStarRule_edge_h1_AllowedTypes, requestStarRule_edge_h1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge requestStarRule_edge_n = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@next, "Inext", "requestStarRule_edge_n", "n", requestStarRule_edge_n_AllowedTypes, requestStarRule_edge_n_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge requestStarRule_edge_h2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "requestStarRule_edge_h2", "h2", requestStarRule_edge_h2_AllowedTypes, requestStarRule_edge_h2_IsAllowedType, 5.5F, -1);
			bool[,] requestStarRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestStarRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge requestStarRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "requestStarRule_neg_0_edge_req", "req", requestStarRule_neg_0_edge_req_AllowedTypes, requestStarRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestStarRule curMatch = (Match_requestStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			graph.SettingAddedNodeNames( requestStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestStarRule_addedEdgeNames );
			@request edge_req = @request.CreateEdge(graph, node_p1, node_r2);
			return EmptyReturnElements;
		}
		private static String[] requestStarRule_addedNodeNames = new String[] {  };
		private static String[] requestStarRule_addedEdgeNames = new String[] { "req" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestStarRule curMatch = (Match_requestStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p1 = curMatch._node_p1;
			GRGEN_LGSP.LGSPNode node_r2 = curMatch._node_r2;
			graph.SettingAddedNodeNames( requestStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestStarRule_addedEdgeNames );
			@request edge_req = @request.CreateEdge(graph, node_p1, node_r2);
			return EmptyReturnElements;
		}

		static Rule_requestStarRule() {
		}

		public interface IMatch_requestStarRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r1 { get; }
			IProcess node_p1 { get; }
			IProcess node_p2 { get; }
			IResource node_r2 { get; }
			//Edges
			Iheld_by edge_h1 { get; }
			Inext edge_n { get; }
			Iheld_by edge_h2 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_requestStarRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p1 { get; }
			IResource node_r2 { get; }
			//Edges
			Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_requestStarRule : GRGEN_LGSP.ListElement<Match_requestStarRule>, IMatch_requestStarRule
		{
			public IResource node_r1 { get { return (IResource)_node_r1; } }
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
			public IResource node_r2 { get { return (IResource)_node_r2; } }
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
			
			public Iheld_by edge_h1 { get { return (Iheld_by)_edge_h1; } }
			public Inext edge_n { get { return (Inext)_edge_n; } }
			public Iheld_by edge_h2 { get { return (Iheld_by)_edge_h2; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestStarRule_neg_0 : GRGEN_LGSP.ListElement<Match_requestStarRule_neg_0>, IMatch_requestStarRule_neg_0
		{
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IResource node_r2 { get { return (IResource)_node_r2; } }
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
			
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_releaseStarRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_releaseStarRule instance = null;
		public static Rule_releaseStarRule Instance { get { if (instance==null) { instance = new Rule_releaseStarRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_releaseStarRule;


#if INITIAL_WARMUP
		public Rule_releaseStarRule()
#else
		private Rule_releaseStarRule()
#endif
		{
			name = "releaseStarRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
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
			GRGEN_LGSP.PatternNode releaseStarRule_node_p1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "releaseStarRule_node_p1", "p1", releaseStarRule_node_p1_AllowedTypes, releaseStarRule_node_p1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode releaseStarRule_node_r1 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "releaseStarRule_node_r1", "r1", releaseStarRule_node_r1_AllowedTypes, releaseStarRule_node_r1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode releaseStarRule_node_p2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "releaseStarRule_node_p2", "p2", releaseStarRule_node_p2_AllowedTypes, releaseStarRule_node_p2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternNode releaseStarRule_node_r2 = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "releaseStarRule_node_r2", "r2", releaseStarRule_node_r2_AllowedTypes, releaseStarRule_node_r2_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge releaseStarRule_edge_rq = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "releaseStarRule_edge_rq", "rq", releaseStarRule_edge_rq_AllowedTypes, releaseStarRule_edge_rq_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge releaseStarRule_edge_h1 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "releaseStarRule_edge_h1", "h1", releaseStarRule_edge_h1_AllowedTypes, releaseStarRule_edge_h1_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge releaseStarRule_edge_h2 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "releaseStarRule_edge_h2", "h2", releaseStarRule_edge_h2_AllowedTypes, releaseStarRule_edge_h2_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseStarRule curMatch = (Match_releaseStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r1 = curMatch._node_r1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_h1 = curMatch._edge_h1;
			graph.SettingAddedNodeNames( releaseStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseStarRule_addedEdgeNames );
			@release edge_rl = @release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return EmptyReturnElements;
		}
		private static String[] releaseStarRule_addedNodeNames = new String[] {  };
		private static String[] releaseStarRule_addedEdgeNames = new String[] { "rl" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_releaseStarRule curMatch = (Match_releaseStarRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_r1 = curMatch._node_r1;
			GRGEN_LGSP.LGSPNode node_p2 = curMatch._node_p2;
			GRGEN_LGSP.LGSPEdge edge_h1 = curMatch._edge_h1;
			graph.SettingAddedNodeNames( releaseStarRule_addedNodeNames );
			graph.SettingAddedEdgeNames( releaseStarRule_addedEdgeNames );
			@release edge_rl = @release.CreateEdge(graph, node_r1, node_p2);
			graph.Remove(edge_h1);
			return EmptyReturnElements;
		}

		static Rule_releaseStarRule() {
		}

		public interface IMatch_releaseStarRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p1 { get; }
			IResource node_r1 { get; }
			IProcess node_p2 { get; }
			IResource node_r2 { get; }
			//Edges
			Irequest edge_rq { get; }
			Iheld_by edge_h1 { get; }
			Iheld_by edge_h2 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_releaseStarRule : GRGEN_LGSP.ListElement<Match_releaseStarRule>, IMatch_releaseStarRule
		{
			public IProcess node_p1 { get { return (IProcess)_node_p1; } }
			public IResource node_r1 { get { return (IResource)_node_r1; } }
			public IProcess node_p2 { get { return (IProcess)_node_p2; } }
			public IResource node_r2 { get { return (IResource)_node_r2; } }
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
			
			public Irequest edge_rq { get { return (Irequest)_edge_rq; } }
			public Iheld_by edge_h1 { get { return (Iheld_by)_edge_h1; } }
			public Iheld_by edge_h2 { get { return (Iheld_by)_edge_h2; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_requestSimpleRule : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_requestSimpleRule instance = null;
		public static Rule_requestSimpleRule Instance { get { if (instance==null) { instance = new Rule_requestSimpleRule(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

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



		GRGEN_LGSP.PatternGraph pat_requestSimpleRule;

		public static GRGEN_LIBGR.EdgeType[] requestSimpleRule_neg_0_edge_req_AllowedTypes = null;
		public static bool[] requestSimpleRule_neg_0_edge_req_IsAllowedType = null;
		public enum requestSimpleRule_neg_0_NodeNums { @p, @r, };
		public enum requestSimpleRule_neg_0_EdgeNums { @req, };
		public enum requestSimpleRule_neg_0_VariableNums { };
		public enum requestSimpleRule_neg_0_SubNums { };
		public enum requestSimpleRule_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph requestSimpleRule_neg_0;


#if INITIAL_WARMUP
		public Rule_requestSimpleRule()
#else
		private Rule_requestSimpleRule()
#endif
		{
			name = "requestSimpleRule";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] requestSimpleRule_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestSimpleRule_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode requestSimpleRule_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "requestSimpleRule_node_r", "r", requestSimpleRule_node_r_AllowedTypes, requestSimpleRule_node_r_IsAllowedType, 1.0F, -1);
			GRGEN_LGSP.PatternNode requestSimpleRule_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "requestSimpleRule_node_p", "p", requestSimpleRule_node_p_AllowedTypes, requestSimpleRule_node_p_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge requestSimpleRule_edge_t = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@token, "Itoken", "requestSimpleRule_edge_t", "t", requestSimpleRule_edge_t_AllowedTypes, requestSimpleRule_edge_t_IsAllowedType, 5.5F, -1);
			bool[,] requestSimpleRule_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] requestSimpleRule_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternEdge requestSimpleRule_neg_0_edge_req = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@request, "Irequest", "requestSimpleRule_neg_0_edge_req", "req", requestSimpleRule_neg_0_edge_req_AllowedTypes, requestSimpleRule_neg_0_edge_req_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestSimpleRule curMatch = (Match_requestSimpleRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestSimpleRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestSimpleRule_addedEdgeNames );
			@request edge_req = @request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}
		private static String[] requestSimpleRule_addedNodeNames = new String[] {  };
		private static String[] requestSimpleRule_addedEdgeNames = new String[] { "req" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_requestSimpleRule curMatch = (Match_requestSimpleRule)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			GRGEN_LGSP.LGSPNode node_r = curMatch._node_r;
			graph.SettingAddedNodeNames( requestSimpleRule_addedNodeNames );
			graph.SettingAddedEdgeNames( requestSimpleRule_addedEdgeNames );
			@request edge_req = @request.CreateEdge(graph, node_p, node_r);
			return EmptyReturnElements;
		}

		static Rule_requestSimpleRule() {
		}

		public interface IMatch_requestSimpleRule : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Itoken edge_t { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_requestSimpleRule_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p { get; }
			IResource node_r { get; }
			//Edges
			Irequest edge_req { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_requestSimpleRule : GRGEN_LGSP.ListElement<Match_requestSimpleRule>, IMatch_requestSimpleRule
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Itoken edge_t { get { return (Itoken)_edge_t; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_requestSimpleRule_neg_0 : GRGEN_LGSP.ListElement<Match_requestSimpleRule_neg_0>, IMatch_requestSimpleRule_neg_0
		{
			public IProcess node_p { get { return (IProcess)_node_p; } }
			public IResource node_r { get { return (IResource)_node_r; } }
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
			
			public Irequest edge_req { get { return (Irequest)_edge_req; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}

	public class Rule_aux_attachResource : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_aux_attachResource instance = null;
		public static Rule_aux_attachResource Instance { get { if (instance==null) { instance = new Rule_aux_attachResource(); instance.initialize(); } return instance; } }

		private static object[] ReturnArray = new object[0];

		public static GRGEN_LIBGR.NodeType[] aux_attachResource_node_p_AllowedTypes = null;
		public static bool[] aux_attachResource_node_p_IsAllowedType = null;
		public enum aux_attachResource_NodeNums { @p, };
		public enum aux_attachResource_EdgeNums { };
		public enum aux_attachResource_VariableNums { };
		public enum aux_attachResource_SubNums { };
		public enum aux_attachResource_AltNums { };



		GRGEN_LGSP.PatternGraph pat_aux_attachResource;

		public static GRGEN_LIBGR.NodeType[] aux_attachResource_neg_0_node_r_AllowedTypes = null;
		public static bool[] aux_attachResource_neg_0_node_r_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] aux_attachResource_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] aux_attachResource_neg_0_edge__edge0_IsAllowedType = null;
		public enum aux_attachResource_neg_0_NodeNums { @r, @p, };
		public enum aux_attachResource_neg_0_EdgeNums { @_edge0, };
		public enum aux_attachResource_neg_0_VariableNums { };
		public enum aux_attachResource_neg_0_SubNums { };
		public enum aux_attachResource_neg_0_AltNums { };

		GRGEN_LGSP.PatternGraph aux_attachResource_neg_0;


#if INITIAL_WARMUP
		public Rule_aux_attachResource()
#else
		private Rule_aux_attachResource()
#endif
		{
			name = "aux_attachResource";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] aux_attachResource_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] aux_attachResource_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			GRGEN_LGSP.PatternNode aux_attachResource_node_p = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Process, "IProcess", "aux_attachResource_node_p", "p", aux_attachResource_node_p_AllowedTypes, aux_attachResource_node_p_IsAllowedType, 5.5F, -1);
			bool[,] aux_attachResource_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] aux_attachResource_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			GRGEN_LGSP.PatternNode aux_attachResource_neg_0_node_r = new GRGEN_LGSP.PatternNode((int) NodeTypes.@Resource, "IResource", "aux_attachResource_neg_0_node_r", "r", aux_attachResource_neg_0_node_r_AllowedTypes, aux_attachResource_neg_0_node_r_IsAllowedType, 5.5F, -1);
			GRGEN_LGSP.PatternEdge aux_attachResource_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) EdgeTypes.@held_by, "Iheld_by", "aux_attachResource_neg_0_edge__edge0", "_edge0", aux_attachResource_neg_0_edge__edge0_AllowedTypes, aux_attachResource_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
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


		public override object[] Modify(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_aux_attachResource curMatch = (Match_aux_attachResource)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( aux_attachResource_addedNodeNames );
			@Resource node_r = @Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( aux_attachResource_addedEdgeNames );
			@held_by edge__edge0 = @held_by.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}
		private static String[] aux_attachResource_addedNodeNames = new String[] { "r" };
		private static String[] aux_attachResource_addedEdgeNames = new String[] { "_edge0" };

		public override object[] ModifyNoReuse(GRGEN_LGSP.LGSPGraph graph, GRGEN_LIBGR.IMatch _curMatch)
		{
			Match_aux_attachResource curMatch = (Match_aux_attachResource)_curMatch;
			GRGEN_LGSP.LGSPNode node_p = curMatch._node_p;
			graph.SettingAddedNodeNames( aux_attachResource_addedNodeNames );
			@Resource node_r = @Resource.CreateNode(graph);
			graph.SettingAddedEdgeNames( aux_attachResource_addedEdgeNames );
			@held_by edge__edge0 = @held_by.CreateEdge(graph, node_r, node_p);
			return EmptyReturnElements;
		}

		static Rule_aux_attachResource() {
		}

		public interface IMatch_aux_attachResource : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IProcess node_p { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public interface IMatch_aux_attachResource_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			IResource node_r { get; }
			IProcess node_p { get; }
			//Edges
			Iheld_by edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Independents
		}

		public class Match_aux_attachResource : GRGEN_LGSP.ListElement<Match_aux_attachResource>, IMatch_aux_attachResource
		{
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

		public class Match_aux_attachResource_neg_0 : GRGEN_LGSP.ListElement<Match_aux_attachResource_neg_0>, IMatch_aux_attachResource_neg_0
		{
			public IResource node_r { get { return (IResource)_node_r; } }
			public IProcess node_p { get { return (IProcess)_node_p; } }
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
			
			public Iheld_by edge__edge0 { get { return (Iheld_by)_edge__edge0; } }
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
			public override string ToString() { return "Match of " + Pattern.Name; }
		}

	}


    public class Action_newRule : GRGEN_LGSP.LGSPAction
    {
        public Action_newRule() {
            rulePattern = Rule_newRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_newRule.Match_newRule>(this);
        }

        public override string Name { get { return "newRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_newRule.Match_newRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_newRule instance = new Action_newRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup newRule_edge_n 
            int type_id_candidate_newRule_edge_n = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_newRule_edge_n = graph.edgesByTypeHeads[type_id_candidate_newRule_edge_n], candidate_newRule_edge_n = head_candidate_newRule_edge_n.typeNext; candidate_newRule_edge_n != head_candidate_newRule_edge_n; candidate_newRule_edge_n = candidate_newRule_edge_n.typeNext)
            {
                // Implicit Source newRule_node_p1 from newRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_newRule_node_p1 = candidate_newRule_edge_n.source;
                if(candidate_newRule_node_p1.type.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_newRule_node_p1;
                prev__candidate_newRule_node_p1 = candidate_newRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_newRule_node_p1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target newRule_node_p2 from newRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_newRule_node_p2 = candidate_newRule_edge_n.target;
                if(candidate_newRule_node_p2.type.TypeID!=1) {
                    candidate_newRule_node_p1.flags = candidate_newRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
                    continue;
                }
                if((candidate_newRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_newRule_node_p1.flags = candidate_newRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
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
                    candidate_newRule_node_p1.flags = candidate_newRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
                    return matches;
                }
                candidate_newRule_node_p1.flags = candidate_newRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_newRule_node_p1;
            }
            return matches;
        }
    }

    public class Action_killRule : GRGEN_LGSP.LGSPAction
    {
        public Action_killRule() {
            rulePattern = Rule_killRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_killRule.Match_killRule>(this);
        }

        public override string Name { get { return "killRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_killRule.Match_killRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_killRule instance = new Action_killRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup killRule_edge_n2 
            int type_id_candidate_killRule_edge_n2 = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_killRule_edge_n2 = graph.edgesByTypeHeads[type_id_candidate_killRule_edge_n2], candidate_killRule_edge_n2 = head_candidate_killRule_edge_n2.typeNext; candidate_killRule_edge_n2 != head_candidate_killRule_edge_n2; candidate_killRule_edge_n2 = candidate_killRule_edge_n2.typeNext)
            {
                uint prev__candidate_killRule_edge_n2;
                prev__candidate_killRule_edge_n2 = candidate_killRule_edge_n2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_killRule_edge_n2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source killRule_node_p from killRule_edge_n2 
                GRGEN_LGSP.LGSPNode candidate_killRule_node_p = candidate_killRule_edge_n2.source;
                if(candidate_killRule_node_p.type.TypeID!=1) {
                    candidate_killRule_edge_n2.flags = candidate_killRule_edge_n2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                    continue;
                }
                uint prev__candidate_killRule_node_p;
                prev__candidate_killRule_node_p = candidate_killRule_node_p.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_killRule_node_p.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target killRule_node_p2 from killRule_edge_n2 
                GRGEN_LGSP.LGSPNode candidate_killRule_node_p2 = candidate_killRule_edge_n2.target;
                if(candidate_killRule_node_p2.type.TypeID!=1) {
                    candidate_killRule_node_p.flags = candidate_killRule_node_p.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                    candidate_killRule_edge_n2.flags = candidate_killRule_edge_n2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                    continue;
                }
                if((candidate_killRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_killRule_node_p.flags = candidate_killRule_node_p.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                    candidate_killRule_edge_n2.flags = candidate_killRule_edge_n2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                    continue;
                }
                uint prev__candidate_killRule_node_p2;
                prev__candidate_killRule_node_p2 = candidate_killRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_killRule_node_p2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming killRule_edge_n1 from killRule_node_p 
                GRGEN_LGSP.LGSPEdge head_candidate_killRule_edge_n1 = candidate_killRule_node_p.inhead;
                if(head_candidate_killRule_edge_n1 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_killRule_edge_n1 = head_candidate_killRule_edge_n1;
                    do
                    {
                        if(candidate_killRule_edge_n1.type.TypeID!=3) {
                            continue;
                        }
                        if((candidate_killRule_edge_n1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Implicit Source killRule_node_p1 from killRule_edge_n1 
                        GRGEN_LGSP.LGSPNode candidate_killRule_node_p1 = candidate_killRule_edge_n1.source;
                        if(candidate_killRule_node_p1.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_killRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                            candidate_killRule_node_p2.flags = candidate_killRule_node_p2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p2;
                            candidate_killRule_node_p.flags = candidate_killRule_node_p.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                            candidate_killRule_edge_n2.flags = candidate_killRule_edge_n2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
                            return matches;
                        }
                    }
                    while( (candidate_killRule_edge_n1 = candidate_killRule_edge_n1.inNext) != head_candidate_killRule_edge_n1 );
                }
                candidate_killRule_node_p2.flags = candidate_killRule_node_p2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p2;
                candidate_killRule_node_p.flags = candidate_killRule_node_p.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_node_p;
                candidate_killRule_edge_n2.flags = candidate_killRule_edge_n2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_killRule_edge_n2;
            }
            return matches;
        }
    }

    public class Action_mountRule : GRGEN_LGSP.LGSPAction
    {
        public Action_mountRule() {
            rulePattern = Rule_mountRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_mountRule.Match_mountRule>(this);
        }

        public override string Name { get { return "mountRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_mountRule.Match_mountRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_mountRule instance = new Action_mountRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup mountRule_node_p 
            int type_id_candidate_mountRule_node_p = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_mountRule_node_p = graph.nodesByTypeHeads[type_id_candidate_mountRule_node_p], candidate_mountRule_node_p = head_candidate_mountRule_node_p.typeNext; candidate_mountRule_node_p != head_candidate_mountRule_node_p; candidate_mountRule_node_p = candidate_mountRule_node_p.typeNext)
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
    }

    public class Action_unmountRule : GRGEN_LGSP.LGSPAction
    {
        public Action_unmountRule() {
            rulePattern = Rule_unmountRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_unmountRule.Match_unmountRule>(this);
        }

        public override string Name { get { return "unmountRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_unmountRule.Match_unmountRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_unmountRule instance = new Action_unmountRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup unmountRule_edge_t 
            int type_id_candidate_unmountRule_edge_t = 6;
            for(GRGEN_LGSP.LGSPEdge head_candidate_unmountRule_edge_t = graph.edgesByTypeHeads[type_id_candidate_unmountRule_edge_t], candidate_unmountRule_edge_t = head_candidate_unmountRule_edge_t.typeNext; candidate_unmountRule_edge_t != head_candidate_unmountRule_edge_t; candidate_unmountRule_edge_t = candidate_unmountRule_edge_t.typeNext)
            {
                // Implicit Source unmountRule_node_r from unmountRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_unmountRule_node_r = candidate_unmountRule_edge_t.source;
                if(candidate_unmountRule_node_r.type.TypeID!=2) {
                    continue;
                }
                // Implicit Target unmountRule_node_p from unmountRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_unmountRule_node_p = candidate_unmountRule_edge_t.target;
                if(candidate_unmountRule_node_p.type.TypeID!=1) {
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
    }

    public class Action_passRule : GRGEN_LGSP.LGSPAction
    {
        public Action_passRule() {
            rulePattern = Rule_passRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_passRule.Match_passRule>(this);
        }

        public override string Name { get { return "passRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_passRule.Match_passRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_passRule instance = new Action_passRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup passRule_edge_n 
            int type_id_candidate_passRule_edge_n = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_passRule_edge_n = graph.edgesByTypeHeads[type_id_candidate_passRule_edge_n], candidate_passRule_edge_n = head_candidate_passRule_edge_n.typeNext; candidate_passRule_edge_n != head_candidate_passRule_edge_n; candidate_passRule_edge_n = candidate_passRule_edge_n.typeNext)
            {
                // Implicit Source passRule_node_p1 from passRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_passRule_node_p1 = candidate_passRule_edge_n.source;
                if(candidate_passRule_node_p1.type.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_passRule_node_p1;
                prev__candidate_passRule_node_p1 = candidate_passRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_passRule_node_p1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target passRule_node_p2 from passRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_passRule_node_p2 = candidate_passRule_edge_n.target;
                if(candidate_passRule_node_p2.type.TypeID!=1) {
                    candidate_passRule_node_p1.flags = candidate_passRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
                    continue;
                }
                if((candidate_passRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_passRule_node_p1.flags = candidate_passRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
                    continue;
                }
                // Extend Incoming passRule_edge__edge0 from passRule_node_p1 
                GRGEN_LGSP.LGSPEdge head_candidate_passRule_edge__edge0 = candidate_passRule_node_p1.inhead;
                if(head_candidate_passRule_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_passRule_edge__edge0 = head_candidate_passRule_edge__edge0;
                    do
                    {
                        if(candidate_passRule_edge__edge0.type.TypeID!=6) {
                            continue;
                        }
                        // Implicit Source passRule_node_r from passRule_edge__edge0 
                        GRGEN_LGSP.LGSPNode candidate_passRule_node_r = candidate_passRule_edge__edge0.source;
                        if(candidate_passRule_node_r.type.TypeID!=2) {
                            continue;
                        }
                        // NegativePattern 
                        {
                            ++negLevel;
                            // Extend Outgoing passRule_neg_0_edge_req from passRule_node_p1 
                            GRGEN_LGSP.LGSPEdge head_candidate_passRule_neg_0_edge_req = candidate_passRule_node_p1.outhead;
                            if(head_candidate_passRule_neg_0_edge_req != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_passRule_neg_0_edge_req = head_candidate_passRule_neg_0_edge_req;
                                do
                                {
                                    if(candidate_passRule_neg_0_edge_req.type.TypeID!=8) {
                                        continue;
                                    }
                                    if(candidate_passRule_neg_0_edge_req.target != candidate_passRule_node_r) {
                                        continue;
                                    }
                                    // negative pattern found
                                    --negLevel;
                                    goto label0;
                                }
                                while( (candidate_passRule_neg_0_edge_req = candidate_passRule_neg_0_edge_req.outNext) != head_candidate_passRule_neg_0_edge_req );
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
                            candidate_passRule_node_p1.flags = candidate_passRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
                            return matches;
                        }
label0: ;
                    }
                    while( (candidate_passRule_edge__edge0 = candidate_passRule_edge__edge0.inNext) != head_candidate_passRule_edge__edge0 );
                }
                candidate_passRule_node_p1.flags = candidate_passRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_passRule_node_p1;
            }
            return matches;
        }
    }

    public class Action_requestRule : GRGEN_LGSP.LGSPAction
    {
        public Action_requestRule() {
            rulePattern = Rule_requestRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_requestRule.Match_requestRule>(this);
        }

        public override string Name { get { return "requestRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_requestRule.Match_requestRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_requestRule instance = new Action_requestRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup requestRule_node_r 
            int type_id_candidate_requestRule_node_r = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_requestRule_node_r = graph.nodesByTypeHeads[type_id_candidate_requestRule_node_r], candidate_requestRule_node_r = head_candidate_requestRule_node_r.typeNext; candidate_requestRule_node_r != head_candidate_requestRule_node_r; candidate_requestRule_node_r = candidate_requestRule_node_r.typeNext)
            {
                // Lookup requestRule_node_p 
                int type_id_candidate_requestRule_node_p = 1;
                for(GRGEN_LGSP.LGSPNode head_candidate_requestRule_node_p = graph.nodesByTypeHeads[type_id_candidate_requestRule_node_p], candidate_requestRule_node_p = head_candidate_requestRule_node_p.typeNext; candidate_requestRule_node_p != head_candidate_requestRule_node_p; candidate_requestRule_node_p = candidate_requestRule_node_p.typeNext)
                {
                    // NegativePattern 
                    {
                        ++negLevel;
                        // Extend Outgoing requestRule_neg_0_edge_hb from requestRule_node_r 
                        GRGEN_LGSP.LGSPEdge head_candidate_requestRule_neg_0_edge_hb = candidate_requestRule_node_r.outhead;
                        if(head_candidate_requestRule_neg_0_edge_hb != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_requestRule_neg_0_edge_hb = head_candidate_requestRule_neg_0_edge_hb;
                            do
                            {
                                if(candidate_requestRule_neg_0_edge_hb.type.TypeID!=5) {
                                    continue;
                                }
                                if(candidate_requestRule_neg_0_edge_hb.target != candidate_requestRule_node_p) {
                                    continue;
                                }
                                // negative pattern found
                                --negLevel;
                                goto label1;
                            }
                            while( (candidate_requestRule_neg_0_edge_hb = candidate_requestRule_neg_0_edge_hb.outNext) != head_candidate_requestRule_neg_0_edge_hb );
                        }
                        --negLevel;
                    }
                    // NegativePattern 
                    {
                        ++negLevel;
                        // Extend Outgoing requestRule_neg_1_edge_req from requestRule_node_p 
                        GRGEN_LGSP.LGSPEdge head_candidate_requestRule_neg_1_edge_req = candidate_requestRule_node_p.outhead;
                        if(head_candidate_requestRule_neg_1_edge_req != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_requestRule_neg_1_edge_req = head_candidate_requestRule_neg_1_edge_req;
                            do
                            {
                                if(candidate_requestRule_neg_1_edge_req.type.TypeID!=8) {
                                    continue;
                                }
                                // Implicit Target requestRule_neg_1_node_m from requestRule_neg_1_edge_req 
                                GRGEN_LGSP.LGSPNode candidate_requestRule_neg_1_node_m = candidate_requestRule_neg_1_edge_req.target;
                                if(candidate_requestRule_neg_1_node_m.type.TypeID!=2) {
                                    continue;
                                }
                                // negative pattern found
                                --negLevel;
                                goto label2;
                            }
                            while( (candidate_requestRule_neg_1_edge_req = candidate_requestRule_neg_1_edge_req.outNext) != head_candidate_requestRule_neg_1_edge_req );
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
    }

    public class Action_takeRule : GRGEN_LGSP.LGSPAction
    {
        public Action_takeRule() {
            rulePattern = Rule_takeRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_takeRule.Match_takeRule>(this);
        }

        public override string Name { get { return "takeRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_takeRule.Match_takeRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_takeRule instance = new Action_takeRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup takeRule_edge_req 
            int type_id_candidate_takeRule_edge_req = 8;
            for(GRGEN_LGSP.LGSPEdge head_candidate_takeRule_edge_req = graph.edgesByTypeHeads[type_id_candidate_takeRule_edge_req], candidate_takeRule_edge_req = head_candidate_takeRule_edge_req.typeNext; candidate_takeRule_edge_req != head_candidate_takeRule_edge_req; candidate_takeRule_edge_req = candidate_takeRule_edge_req.typeNext)
            {
                // Implicit Target takeRule_node_r from takeRule_edge_req 
                GRGEN_LGSP.LGSPNode candidate_takeRule_node_r = candidate_takeRule_edge_req.target;
                if(candidate_takeRule_node_r.type.TypeID!=2) {
                    continue;
                }
                // Extend Outgoing takeRule_edge_t from takeRule_node_r 
                GRGEN_LGSP.LGSPEdge head_candidate_takeRule_edge_t = candidate_takeRule_node_r.outhead;
                if(head_candidate_takeRule_edge_t != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_takeRule_edge_t = head_candidate_takeRule_edge_t;
                    do
                    {
                        if(candidate_takeRule_edge_t.type.TypeID!=6) {
                            continue;
                        }
                        // Implicit Target takeRule_node_p from takeRule_edge_t 
                        GRGEN_LGSP.LGSPNode candidate_takeRule_node_p = candidate_takeRule_edge_t.target;
                        if(candidate_takeRule_node_p.type.TypeID!=1) {
                            continue;
                        }
                        if(candidate_takeRule_edge_req.source != candidate_takeRule_node_p) {
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
                    while( (candidate_takeRule_edge_t = candidate_takeRule_edge_t.outNext) != head_candidate_takeRule_edge_t );
                }
            }
            return matches;
        }
    }

    public class Action_releaseRule : GRGEN_LGSP.LGSPAction
    {
        public Action_releaseRule() {
            rulePattern = Rule_releaseRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_releaseRule.Match_releaseRule>(this);
        }

        public override string Name { get { return "releaseRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_releaseRule.Match_releaseRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_releaseRule instance = new Action_releaseRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup releaseRule_edge_hb 
            int type_id_candidate_releaseRule_edge_hb = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_releaseRule_edge_hb = graph.edgesByTypeHeads[type_id_candidate_releaseRule_edge_hb], candidate_releaseRule_edge_hb = head_candidate_releaseRule_edge_hb.typeNext; candidate_releaseRule_edge_hb != head_candidate_releaseRule_edge_hb; candidate_releaseRule_edge_hb = candidate_releaseRule_edge_hb.typeNext)
            {
                // Implicit Source releaseRule_node_r from releaseRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_releaseRule_node_r = candidate_releaseRule_edge_hb.source;
                if(candidate_releaseRule_node_r.type.TypeID!=2) {
                    continue;
                }
                // Implicit Target releaseRule_node_p from releaseRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_releaseRule_node_p = candidate_releaseRule_edge_hb.target;
                if(candidate_releaseRule_node_p.type.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Outgoing releaseRule_neg_0_edge_req from releaseRule_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_releaseRule_neg_0_edge_req = candidate_releaseRule_node_p.outhead;
                    if(head_candidate_releaseRule_neg_0_edge_req != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_releaseRule_neg_0_edge_req = head_candidate_releaseRule_neg_0_edge_req;
                        do
                        {
                            if(candidate_releaseRule_neg_0_edge_req.type.TypeID!=8) {
                                continue;
                            }
                            // Implicit Target releaseRule_neg_0_node_m from releaseRule_neg_0_edge_req 
                            GRGEN_LGSP.LGSPNode candidate_releaseRule_neg_0_node_m = candidate_releaseRule_neg_0_edge_req.target;
                            if(candidate_releaseRule_neg_0_node_m.type.TypeID!=2) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_releaseRule_neg_0_edge_req = candidate_releaseRule_neg_0_edge_req.outNext) != head_candidate_releaseRule_neg_0_edge_req );
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
    }

    public class Action_giveRule : GRGEN_LGSP.LGSPAction
    {
        public Action_giveRule() {
            rulePattern = Rule_giveRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_giveRule.Match_giveRule>(this);
        }

        public override string Name { get { return "giveRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_giveRule.Match_giveRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_giveRule instance = new Action_giveRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup giveRule_edge_n 
            int type_id_candidate_giveRule_edge_n = 3;
            for(GRGEN_LGSP.LGSPEdge head_candidate_giveRule_edge_n = graph.edgesByTypeHeads[type_id_candidate_giveRule_edge_n], candidate_giveRule_edge_n = head_candidate_giveRule_edge_n.typeNext; candidate_giveRule_edge_n != head_candidate_giveRule_edge_n; candidate_giveRule_edge_n = candidate_giveRule_edge_n.typeNext)
            {
                // Implicit Source giveRule_node_p1 from giveRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_giveRule_node_p1 = candidate_giveRule_edge_n.source;
                if(candidate_giveRule_node_p1.type.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_giveRule_node_p1;
                prev__candidate_giveRule_node_p1 = candidate_giveRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_giveRule_node_p1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target giveRule_node_p2 from giveRule_edge_n 
                GRGEN_LGSP.LGSPNode candidate_giveRule_node_p2 = candidate_giveRule_edge_n.target;
                if(candidate_giveRule_node_p2.type.TypeID!=1) {
                    candidate_giveRule_node_p1.flags = candidate_giveRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
                    continue;
                }
                if((candidate_giveRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                {
                    candidate_giveRule_node_p1.flags = candidate_giveRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
                    continue;
                }
                // Extend Incoming giveRule_edge_rel from giveRule_node_p1 
                GRGEN_LGSP.LGSPEdge head_candidate_giveRule_edge_rel = candidate_giveRule_node_p1.inhead;
                if(head_candidate_giveRule_edge_rel != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_giveRule_edge_rel = head_candidate_giveRule_edge_rel;
                    do
                    {
                        if(candidate_giveRule_edge_rel.type.TypeID!=7) {
                            continue;
                        }
                        // Implicit Source giveRule_node_r from giveRule_edge_rel 
                        GRGEN_LGSP.LGSPNode candidate_giveRule_node_r = candidate_giveRule_edge_rel.source;
                        if(candidate_giveRule_node_r.type.TypeID!=2) {
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
                            candidate_giveRule_node_p1.flags = candidate_giveRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
                            return matches;
                        }
                    }
                    while( (candidate_giveRule_edge_rel = candidate_giveRule_edge_rel.inNext) != head_candidate_giveRule_edge_rel );
                }
                candidate_giveRule_node_p1.flags = candidate_giveRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_giveRule_node_p1;
            }
            return matches;
        }
    }

    public class Action_blockedRule : GRGEN_LGSP.LGSPAction
    {
        public Action_blockedRule() {
            rulePattern = Rule_blockedRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_blockedRule.Match_blockedRule>(this);
        }

        public override string Name { get { return "blockedRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_blockedRule.Match_blockedRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_blockedRule instance = new Action_blockedRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup blockedRule_edge_hb 
            int type_id_candidate_blockedRule_edge_hb = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_blockedRule_edge_hb = graph.edgesByTypeHeads[type_id_candidate_blockedRule_edge_hb], candidate_blockedRule_edge_hb = head_candidate_blockedRule_edge_hb.typeNext; candidate_blockedRule_edge_hb != head_candidate_blockedRule_edge_hb; candidate_blockedRule_edge_hb = candidate_blockedRule_edge_hb.typeNext)
            {
                // Implicit Source blockedRule_node_r from blockedRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_blockedRule_node_r = candidate_blockedRule_edge_hb.source;
                if(candidate_blockedRule_node_r.type.TypeID!=2) {
                    continue;
                }
                // Implicit Target blockedRule_node_p2 from blockedRule_edge_hb 
                GRGEN_LGSP.LGSPNode candidate_blockedRule_node_p2 = candidate_blockedRule_edge_hb.target;
                if(candidate_blockedRule_node_p2.type.TypeID!=1) {
                    continue;
                }
                uint prev__candidate_blockedRule_node_p2;
                prev__candidate_blockedRule_node_p2 = candidate_blockedRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_blockedRule_node_p2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming blockedRule_edge_req from blockedRule_node_r 
                GRGEN_LGSP.LGSPEdge head_candidate_blockedRule_edge_req = candidate_blockedRule_node_r.inhead;
                if(head_candidate_blockedRule_edge_req != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_blockedRule_edge_req = head_candidate_blockedRule_edge_req;
                    do
                    {
                        if(candidate_blockedRule_edge_req.type.TypeID!=8) {
                            continue;
                        }
                        // Implicit Source blockedRule_node_p1 from blockedRule_edge_req 
                        GRGEN_LGSP.LGSPNode candidate_blockedRule_node_p1 = candidate_blockedRule_edge_req.source;
                        if(candidate_blockedRule_node_p1.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_blockedRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                            candidate_blockedRule_node_p2.flags = candidate_blockedRule_node_p2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_blockedRule_node_p2;
                            return matches;
                        }
                    }
                    while( (candidate_blockedRule_edge_req = candidate_blockedRule_edge_req.inNext) != head_candidate_blockedRule_edge_req );
                }
                candidate_blockedRule_node_p2.flags = candidate_blockedRule_node_p2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_blockedRule_node_p2;
            }
            return matches;
        }
    }

    public class Action_waitingRule : GRGEN_LGSP.LGSPAction
    {
        public Action_waitingRule() {
            rulePattern = Rule_waitingRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_waitingRule.Match_waitingRule>(this);
        }

        public override string Name { get { return "waitingRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_waitingRule.Match_waitingRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_waitingRule instance = new Action_waitingRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup waitingRule_node_r 
            int type_id_candidate_waitingRule_node_r = 2;
            for(GRGEN_LGSP.LGSPNode head_candidate_waitingRule_node_r = graph.nodesByTypeHeads[type_id_candidate_waitingRule_node_r], candidate_waitingRule_node_r = head_candidate_waitingRule_node_r.typeNext; candidate_waitingRule_node_r != head_candidate_waitingRule_node_r; candidate_waitingRule_node_r = candidate_waitingRule_node_r.typeNext)
            {
                uint prev__candidate_waitingRule_node_r;
                prev__candidate_waitingRule_node_r = candidate_waitingRule_node_r.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_waitingRule_node_r.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Lookup waitingRule_edge_b 
                int type_id_candidate_waitingRule_edge_b = 4;
                for(GRGEN_LGSP.LGSPEdge head_candidate_waitingRule_edge_b = graph.edgesByTypeHeads[type_id_candidate_waitingRule_edge_b], candidate_waitingRule_edge_b = head_candidate_waitingRule_edge_b.typeNext; candidate_waitingRule_edge_b != head_candidate_waitingRule_edge_b; candidate_waitingRule_edge_b = candidate_waitingRule_edge_b.typeNext)
                {
                    // Implicit Source waitingRule_node_r2 from waitingRule_edge_b 
                    GRGEN_LGSP.LGSPNode candidate_waitingRule_node_r2 = candidate_waitingRule_edge_b.source;
                    if(candidate_waitingRule_node_r2.type.TypeID!=2) {
                        continue;
                    }
                    if((candidate_waitingRule_node_r2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_waitingRule_node_r2;
                    prev__candidate_waitingRule_node_r2 = candidate_waitingRule_node_r2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_waitingRule_node_r2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Implicit Target waitingRule_node_p1 from waitingRule_edge_b 
                    GRGEN_LGSP.LGSPNode candidate_waitingRule_node_p1 = candidate_waitingRule_edge_b.target;
                    if(candidate_waitingRule_node_p1.type.TypeID!=1) {
                        candidate_waitingRule_node_r2.flags = candidate_waitingRule_node_r2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r2;
                        continue;
                    }
                    uint prev__candidate_waitingRule_node_p1;
                    prev__candidate_waitingRule_node_p1 = candidate_waitingRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_waitingRule_node_p1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Extend Incoming waitingRule_edge_hb from waitingRule_node_p1 
                    GRGEN_LGSP.LGSPEdge head_candidate_waitingRule_edge_hb = candidate_waitingRule_node_p1.inhead;
                    if(head_candidate_waitingRule_edge_hb != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_waitingRule_edge_hb = head_candidate_waitingRule_edge_hb;
                        do
                        {
                            if(candidate_waitingRule_edge_hb.type.TypeID!=5) {
                                continue;
                            }
                            // Implicit Source waitingRule_node_r1 from waitingRule_edge_hb 
                            GRGEN_LGSP.LGSPNode candidate_waitingRule_node_r1 = candidate_waitingRule_edge_hb.source;
                            if(candidate_waitingRule_node_r1.type.TypeID!=2) {
                                continue;
                            }
                            if((candidate_waitingRule_node_r1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                            {
                                continue;
                            }
                            // Extend Incoming waitingRule_edge_req from waitingRule_node_r1 
                            GRGEN_LGSP.LGSPEdge head_candidate_waitingRule_edge_req = candidate_waitingRule_node_r1.inhead;
                            if(head_candidate_waitingRule_edge_req != null)
                            {
                                GRGEN_LGSP.LGSPEdge candidate_waitingRule_edge_req = head_candidate_waitingRule_edge_req;
                                do
                                {
                                    if(candidate_waitingRule_edge_req.type.TypeID!=8) {
                                        continue;
                                    }
                                    // Implicit Source waitingRule_node_p2 from waitingRule_edge_req 
                                    GRGEN_LGSP.LGSPNode candidate_waitingRule_node_p2 = candidate_waitingRule_edge_req.source;
                                    if(candidate_waitingRule_node_p2.type.TypeID!=1) {
                                        continue;
                                    }
                                    if((candidate_waitingRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                        candidate_waitingRule_node_p1.flags = candidate_waitingRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_p1;
                                        candidate_waitingRule_node_r2.flags = candidate_waitingRule_node_r2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r2;
                                        candidate_waitingRule_node_r.flags = candidate_waitingRule_node_r.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r;
                                        return matches;
                                    }
                                }
                                while( (candidate_waitingRule_edge_req = candidate_waitingRule_edge_req.inNext) != head_candidate_waitingRule_edge_req );
                            }
                        }
                        while( (candidate_waitingRule_edge_hb = candidate_waitingRule_edge_hb.inNext) != head_candidate_waitingRule_edge_hb );
                    }
                    candidate_waitingRule_node_p1.flags = candidate_waitingRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_p1;
                    candidate_waitingRule_node_r2.flags = candidate_waitingRule_node_r2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r2;
                }
                candidate_waitingRule_node_r.flags = candidate_waitingRule_node_r.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_waitingRule_node_r;
            }
            return matches;
        }
    }

    public class Action_ignoreRule : GRGEN_LGSP.LGSPAction
    {
        public Action_ignoreRule() {
            rulePattern = Rule_ignoreRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ignoreRule.Match_ignoreRule>(this);
        }

        public override string Name { get { return "ignoreRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ignoreRule.Match_ignoreRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_ignoreRule instance = new Action_ignoreRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup ignoreRule_edge_b 
            int type_id_candidate_ignoreRule_edge_b = 4;
            for(GRGEN_LGSP.LGSPEdge head_candidate_ignoreRule_edge_b = graph.edgesByTypeHeads[type_id_candidate_ignoreRule_edge_b], candidate_ignoreRule_edge_b = head_candidate_ignoreRule_edge_b.typeNext; candidate_ignoreRule_edge_b != head_candidate_ignoreRule_edge_b; candidate_ignoreRule_edge_b = candidate_ignoreRule_edge_b.typeNext)
            {
                // Implicit Source ignoreRule_node_r from ignoreRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_ignoreRule_node_r = candidate_ignoreRule_edge_b.source;
                if(candidate_ignoreRule_node_r.type.TypeID!=2) {
                    continue;
                }
                // Implicit Target ignoreRule_node_p from ignoreRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_ignoreRule_node_p = candidate_ignoreRule_edge_b.target;
                if(candidate_ignoreRule_node_p.type.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Incoming ignoreRule_neg_0_edge_hb from ignoreRule_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_ignoreRule_neg_0_edge_hb = candidate_ignoreRule_node_p.inhead;
                    if(head_candidate_ignoreRule_neg_0_edge_hb != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ignoreRule_neg_0_edge_hb = head_candidate_ignoreRule_neg_0_edge_hb;
                        do
                        {
                            if(candidate_ignoreRule_neg_0_edge_hb.type.TypeID!=5) {
                                continue;
                            }
                            // Implicit Source ignoreRule_neg_0_node_m from ignoreRule_neg_0_edge_hb 
                            GRGEN_LGSP.LGSPNode candidate_ignoreRule_neg_0_node_m = candidate_ignoreRule_neg_0_edge_hb.source;
                            if(candidate_ignoreRule_neg_0_node_m.type.TypeID!=2) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label4;
                        }
                        while( (candidate_ignoreRule_neg_0_edge_hb = candidate_ignoreRule_neg_0_edge_hb.inNext) != head_candidate_ignoreRule_neg_0_edge_hb );
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
    }

    public class Action_unlockRule : GRGEN_LGSP.LGSPAction
    {
        public Action_unlockRule() {
            rulePattern = Rule_unlockRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_unlockRule.Match_unlockRule>(this);
        }

        public override string Name { get { return "unlockRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_unlockRule.Match_unlockRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_unlockRule instance = new Action_unlockRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup unlockRule_edge_b 
            int type_id_candidate_unlockRule_edge_b = 4;
            for(GRGEN_LGSP.LGSPEdge head_candidate_unlockRule_edge_b = graph.edgesByTypeHeads[type_id_candidate_unlockRule_edge_b], candidate_unlockRule_edge_b = head_candidate_unlockRule_edge_b.typeNext; candidate_unlockRule_edge_b != head_candidate_unlockRule_edge_b; candidate_unlockRule_edge_b = candidate_unlockRule_edge_b.typeNext)
            {
                // Implicit Source unlockRule_node_r from unlockRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_unlockRule_node_r = candidate_unlockRule_edge_b.source;
                if(candidate_unlockRule_node_r.type.TypeID!=2) {
                    continue;
                }
                // Implicit Target unlockRule_node_p from unlockRule_edge_b 
                GRGEN_LGSP.LGSPNode candidate_unlockRule_node_p = candidate_unlockRule_edge_b.target;
                if(candidate_unlockRule_node_p.type.TypeID!=1) {
                    continue;
                }
                // Extend Outgoing unlockRule_edge_hb from unlockRule_node_r 
                GRGEN_LGSP.LGSPEdge head_candidate_unlockRule_edge_hb = candidate_unlockRule_node_r.outhead;
                if(head_candidate_unlockRule_edge_hb != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_unlockRule_edge_hb = head_candidate_unlockRule_edge_hb;
                    do
                    {
                        if(candidate_unlockRule_edge_hb.type.TypeID!=5) {
                            continue;
                        }
                        if(candidate_unlockRule_edge_hb.target != candidate_unlockRule_node_p) {
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
                    while( (candidate_unlockRule_edge_hb = candidate_unlockRule_edge_hb.outNext) != head_candidate_unlockRule_edge_hb );
                }
            }
            return matches;
        }
    }

    public class Action_requestStarRule : GRGEN_LGSP.LGSPAction
    {
        public Action_requestStarRule() {
            rulePattern = Rule_requestStarRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_requestStarRule.Match_requestStarRule>(this);
        }

        public override string Name { get { return "requestStarRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_requestStarRule.Match_requestStarRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_requestStarRule instance = new Action_requestStarRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup requestStarRule_edge_h1 
            int type_id_candidate_requestStarRule_edge_h1 = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_edge_h1 = graph.edgesByTypeHeads[type_id_candidate_requestStarRule_edge_h1], candidate_requestStarRule_edge_h1 = head_candidate_requestStarRule_edge_h1.typeNext; candidate_requestStarRule_edge_h1 != head_candidate_requestStarRule_edge_h1; candidate_requestStarRule_edge_h1 = candidate_requestStarRule_edge_h1.typeNext)
            {
                uint prev__candidate_requestStarRule_edge_h1;
                prev__candidate_requestStarRule_edge_h1 = candidate_requestStarRule_edge_h1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_requestStarRule_edge_h1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source requestStarRule_node_r1 from requestStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_r1 = candidate_requestStarRule_edge_h1.source;
                if(candidate_requestStarRule_node_r1.type.TypeID!=2) {
                    candidate_requestStarRule_edge_h1.flags = candidate_requestStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_requestStarRule_node_r1;
                prev__candidate_requestStarRule_node_r1 = candidate_requestStarRule_node_r1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_requestStarRule_node_r1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target requestStarRule_node_p1 from requestStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_p1 = candidate_requestStarRule_edge_h1.target;
                if(candidate_requestStarRule_node_p1.type.TypeID!=1) {
                    candidate_requestStarRule_node_r1.flags = candidate_requestStarRule_node_r1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_r1;
                    candidate_requestStarRule_edge_h1.flags = candidate_requestStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_requestStarRule_node_p1;
                prev__candidate_requestStarRule_node_p1 = candidate_requestStarRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_requestStarRule_node_p1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming requestStarRule_edge_n from requestStarRule_node_p1 
                GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_edge_n = candidate_requestStarRule_node_p1.inhead;
                if(head_candidate_requestStarRule_edge_n != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_requestStarRule_edge_n = head_candidate_requestStarRule_edge_n;
                    do
                    {
                        if(candidate_requestStarRule_edge_n.type.TypeID!=3) {
                            continue;
                        }
                        // Implicit Source requestStarRule_node_p2 from requestStarRule_edge_n 
                        GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_p2 = candidate_requestStarRule_edge_n.source;
                        if(candidate_requestStarRule_node_p2.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_requestStarRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Extend Incoming requestStarRule_edge_h2 from requestStarRule_node_p2 
                        GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_edge_h2 = candidate_requestStarRule_node_p2.inhead;
                        if(head_candidate_requestStarRule_edge_h2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_requestStarRule_edge_h2 = head_candidate_requestStarRule_edge_h2;
                            do
                            {
                                if(candidate_requestStarRule_edge_h2.type.TypeID!=5) {
                                    continue;
                                }
                                if((candidate_requestStarRule_edge_h2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // Implicit Source requestStarRule_node_r2 from requestStarRule_edge_h2 
                                GRGEN_LGSP.LGSPNode candidate_requestStarRule_node_r2 = candidate_requestStarRule_edge_h2.source;
                                if(candidate_requestStarRule_node_r2.type.TypeID!=2) {
                                    continue;
                                }
                                if((candidate_requestStarRule_node_r2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // NegativePattern 
                                {
                                    ++negLevel;
                                    // Extend Outgoing requestStarRule_neg_0_edge_req from requestStarRule_node_p1 
                                    GRGEN_LGSP.LGSPEdge head_candidate_requestStarRule_neg_0_edge_req = candidate_requestStarRule_node_p1.outhead;
                                    if(head_candidate_requestStarRule_neg_0_edge_req != null)
                                    {
                                        GRGEN_LGSP.LGSPEdge candidate_requestStarRule_neg_0_edge_req = head_candidate_requestStarRule_neg_0_edge_req;
                                        do
                                        {
                                            if(candidate_requestStarRule_neg_0_edge_req.type.TypeID!=8) {
                                                continue;
                                            }
                                            if(candidate_requestStarRule_neg_0_edge_req.target != candidate_requestStarRule_node_r2) {
                                                continue;
                                            }
                                            // negative pattern found
                                            --negLevel;
                                            goto label5;
                                        }
                                        while( (candidate_requestStarRule_neg_0_edge_req = candidate_requestStarRule_neg_0_edge_req.outNext) != head_candidate_requestStarRule_neg_0_edge_req );
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
                                    candidate_requestStarRule_node_p1.flags = candidate_requestStarRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_p1;
                                    candidate_requestStarRule_node_r1.flags = candidate_requestStarRule_node_r1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_r1;
                                    candidate_requestStarRule_edge_h1.flags = candidate_requestStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
                                    return matches;
                                }
label5: ;
                            }
                            while( (candidate_requestStarRule_edge_h2 = candidate_requestStarRule_edge_h2.inNext) != head_candidate_requestStarRule_edge_h2 );
                        }
                    }
                    while( (candidate_requestStarRule_edge_n = candidate_requestStarRule_edge_n.inNext) != head_candidate_requestStarRule_edge_n );
                }
                candidate_requestStarRule_node_p1.flags = candidate_requestStarRule_node_p1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_p1;
                candidate_requestStarRule_node_r1.flags = candidate_requestStarRule_node_r1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_node_r1;
                candidate_requestStarRule_edge_h1.flags = candidate_requestStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_requestStarRule_edge_h1;
            }
            return matches;
        }
    }

    public class Action_releaseStarRule : GRGEN_LGSP.LGSPAction
    {
        public Action_releaseStarRule() {
            rulePattern = Rule_releaseStarRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_releaseStarRule.Match_releaseStarRule>(this);
        }

        public override string Name { get { return "releaseStarRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_releaseStarRule.Match_releaseStarRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_releaseStarRule instance = new Action_releaseStarRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup releaseStarRule_edge_h1 
            int type_id_candidate_releaseStarRule_edge_h1 = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_releaseStarRule_edge_h1 = graph.edgesByTypeHeads[type_id_candidate_releaseStarRule_edge_h1], candidate_releaseStarRule_edge_h1 = head_candidate_releaseStarRule_edge_h1.typeNext; candidate_releaseStarRule_edge_h1 != head_candidate_releaseStarRule_edge_h1; candidate_releaseStarRule_edge_h1 = candidate_releaseStarRule_edge_h1.typeNext)
            {
                uint prev__candidate_releaseStarRule_edge_h1;
                prev__candidate_releaseStarRule_edge_h1 = candidate_releaseStarRule_edge_h1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_releaseStarRule_edge_h1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Source releaseStarRule_node_r1 from releaseStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_r1 = candidate_releaseStarRule_edge_h1.source;
                if(candidate_releaseStarRule_node_r1.type.TypeID!=2) {
                    candidate_releaseStarRule_edge_h1.flags = candidate_releaseStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_releaseStarRule_node_r1;
                prev__candidate_releaseStarRule_node_r1 = candidate_releaseStarRule_node_r1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_releaseStarRule_node_r1.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Implicit Target releaseStarRule_node_p2 from releaseStarRule_edge_h1 
                GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_p2 = candidate_releaseStarRule_edge_h1.target;
                if(candidate_releaseStarRule_node_p2.type.TypeID!=1) {
                    candidate_releaseStarRule_node_r1.flags = candidate_releaseStarRule_node_r1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_r1;
                    candidate_releaseStarRule_edge_h1.flags = candidate_releaseStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
                    continue;
                }
                uint prev__candidate_releaseStarRule_node_p2;
                prev__candidate_releaseStarRule_node_p2 = candidate_releaseStarRule_node_p2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                candidate_releaseStarRule_node_p2.flags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                // Extend Incoming releaseStarRule_edge_rq from releaseStarRule_node_r1 
                GRGEN_LGSP.LGSPEdge head_candidate_releaseStarRule_edge_rq = candidate_releaseStarRule_node_r1.inhead;
                if(head_candidate_releaseStarRule_edge_rq != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_releaseStarRule_edge_rq = head_candidate_releaseStarRule_edge_rq;
                    do
                    {
                        if(candidate_releaseStarRule_edge_rq.type.TypeID!=8) {
                            continue;
                        }
                        // Implicit Source releaseStarRule_node_p1 from releaseStarRule_edge_rq 
                        GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_p1 = candidate_releaseStarRule_edge_rq.source;
                        if(candidate_releaseStarRule_node_p1.type.TypeID!=1) {
                            continue;
                        }
                        if((candidate_releaseStarRule_node_p1.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                        {
                            continue;
                        }
                        // Extend Incoming releaseStarRule_edge_h2 from releaseStarRule_node_p2 
                        GRGEN_LGSP.LGSPEdge head_candidate_releaseStarRule_edge_h2 = candidate_releaseStarRule_node_p2.inhead;
                        if(head_candidate_releaseStarRule_edge_h2 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_releaseStarRule_edge_h2 = head_candidate_releaseStarRule_edge_h2;
                            do
                            {
                                if(candidate_releaseStarRule_edge_h2.type.TypeID!=5) {
                                    continue;
                                }
                                if((candidate_releaseStarRule_edge_h2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                                {
                                    continue;
                                }
                                // Implicit Source releaseStarRule_node_r2 from releaseStarRule_edge_h2 
                                GRGEN_LGSP.LGSPNode candidate_releaseStarRule_node_r2 = candidate_releaseStarRule_edge_h2.source;
                                if(candidate_releaseStarRule_node_r2.type.TypeID!=2) {
                                    continue;
                                }
                                if((candidate_releaseStarRule_node_r2.flags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                    candidate_releaseStarRule_node_p2.flags = candidate_releaseStarRule_node_p2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_p2;
                                    candidate_releaseStarRule_node_r1.flags = candidate_releaseStarRule_node_r1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_r1;
                                    candidate_releaseStarRule_edge_h1.flags = candidate_releaseStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
                                    return matches;
                                }
                            }
                            while( (candidate_releaseStarRule_edge_h2 = candidate_releaseStarRule_edge_h2.inNext) != head_candidate_releaseStarRule_edge_h2 );
                        }
                    }
                    while( (candidate_releaseStarRule_edge_rq = candidate_releaseStarRule_edge_rq.inNext) != head_candidate_releaseStarRule_edge_rq );
                }
                candidate_releaseStarRule_node_p2.flags = candidate_releaseStarRule_node_p2.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_p2;
                candidate_releaseStarRule_node_r1.flags = candidate_releaseStarRule_node_r1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_node_r1;
                candidate_releaseStarRule_edge_h1.flags = candidate_releaseStarRule_edge_h1.flags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_releaseStarRule_edge_h1;
            }
            return matches;
        }
    }

    public class Action_requestSimpleRule : GRGEN_LGSP.LGSPAction
    {
        public Action_requestSimpleRule() {
            rulePattern = Rule_requestSimpleRule.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_requestSimpleRule.Match_requestSimpleRule>(this);
        }

        public override string Name { get { return "requestSimpleRule"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_requestSimpleRule.Match_requestSimpleRule> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_requestSimpleRule instance = new Action_requestSimpleRule();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup requestSimpleRule_edge_t 
            int type_id_candidate_requestSimpleRule_edge_t = 6;
            for(GRGEN_LGSP.LGSPEdge head_candidate_requestSimpleRule_edge_t = graph.edgesByTypeHeads[type_id_candidate_requestSimpleRule_edge_t], candidate_requestSimpleRule_edge_t = head_candidate_requestSimpleRule_edge_t.typeNext; candidate_requestSimpleRule_edge_t != head_candidate_requestSimpleRule_edge_t; candidate_requestSimpleRule_edge_t = candidate_requestSimpleRule_edge_t.typeNext)
            {
                // Implicit Source requestSimpleRule_node_r from requestSimpleRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_requestSimpleRule_node_r = candidate_requestSimpleRule_edge_t.source;
                if(candidate_requestSimpleRule_node_r.type.TypeID!=2) {
                    continue;
                }
                // Implicit Target requestSimpleRule_node_p from requestSimpleRule_edge_t 
                GRGEN_LGSP.LGSPNode candidate_requestSimpleRule_node_p = candidate_requestSimpleRule_edge_t.target;
                if(candidate_requestSimpleRule_node_p.type.TypeID!=1) {
                    continue;
                }
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Outgoing requestSimpleRule_neg_0_edge_req from requestSimpleRule_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_requestSimpleRule_neg_0_edge_req = candidate_requestSimpleRule_node_p.outhead;
                    if(head_candidate_requestSimpleRule_neg_0_edge_req != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_requestSimpleRule_neg_0_edge_req = head_candidate_requestSimpleRule_neg_0_edge_req;
                        do
                        {
                            if(candidate_requestSimpleRule_neg_0_edge_req.type.TypeID!=8) {
                                continue;
                            }
                            if(candidate_requestSimpleRule_neg_0_edge_req.target != candidate_requestSimpleRule_node_r) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label6;
                        }
                        while( (candidate_requestSimpleRule_neg_0_edge_req = candidate_requestSimpleRule_neg_0_edge_req.outNext) != head_candidate_requestSimpleRule_neg_0_edge_req );
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
    }

    public class Action_aux_attachResource : GRGEN_LGSP.LGSPAction
    {
        public Action_aux_attachResource() {
            rulePattern = Rule_aux_attachResource.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_aux_attachResource.Match_aux_attachResource>(this);
        }

        public override string Name { get { return "aux_attachResource"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_aux_attachResource.Match_aux_attachResource> matches;

        public static GRGEN_LGSP.LGSPAction Instance { get { return instance; } }
        private static Action_aux_attachResource instance = new Action_aux_attachResource();
        
        public GRGEN_LIBGR.IMatches myMatch(GRGEN_LGSP.LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.Clear();
            int negLevel = 0;
            // Lookup aux_attachResource_node_p 
            int type_id_candidate_aux_attachResource_node_p = 1;
            for(GRGEN_LGSP.LGSPNode head_candidate_aux_attachResource_node_p = graph.nodesByTypeHeads[type_id_candidate_aux_attachResource_node_p], candidate_aux_attachResource_node_p = head_candidate_aux_attachResource_node_p.typeNext; candidate_aux_attachResource_node_p != head_candidate_aux_attachResource_node_p; candidate_aux_attachResource_node_p = candidate_aux_attachResource_node_p.typeNext)
            {
                // NegativePattern 
                {
                    ++negLevel;
                    // Extend Incoming aux_attachResource_neg_0_edge__edge0 from aux_attachResource_node_p 
                    GRGEN_LGSP.LGSPEdge head_candidate_aux_attachResource_neg_0_edge__edge0 = candidate_aux_attachResource_node_p.inhead;
                    if(head_candidate_aux_attachResource_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_aux_attachResource_neg_0_edge__edge0 = head_candidate_aux_attachResource_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_aux_attachResource_neg_0_edge__edge0.type.TypeID!=5) {
                                continue;
                            }
                            // Implicit Source aux_attachResource_neg_0_node_r from aux_attachResource_neg_0_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_aux_attachResource_neg_0_node_r = candidate_aux_attachResource_neg_0_edge__edge0.source;
                            if(candidate_aux_attachResource_neg_0_node_r.type.TypeID!=2) {
                                continue;
                            }
                            // negative pattern found
                            --negLevel;
                            goto label7;
                        }
                        while( (candidate_aux_attachResource_neg_0_edge__edge0 = candidate_aux_attachResource_neg_0_edge__edge0.inNext) != head_candidate_aux_attachResource_neg_0_edge__edge0 );
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
    }


    public class MutexPimpedActions : de.unika.ipd.grGen.lgsp.LGSPActions
    {
        public MutexPimpedActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public MutexPimpedActions(de.unika.ipd.grGen.lgsp.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("newRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_newRule.Instance);
            actions.Add("killRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_killRule.Instance);
            actions.Add("mountRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_mountRule.Instance);
            actions.Add("unmountRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_unmountRule.Instance);
            actions.Add("passRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_passRule.Instance);
            actions.Add("requestRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_requestRule.Instance);
            actions.Add("takeRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_takeRule.Instance);
            actions.Add("releaseRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_releaseRule.Instance);
            actions.Add("giveRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_giveRule.Instance);
            actions.Add("blockedRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_blockedRule.Instance);
            actions.Add("waitingRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_waitingRule.Instance);
            actions.Add("ignoreRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_ignoreRule.Instance);
            actions.Add("unlockRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_unlockRule.Instance);
            actions.Add("requestStarRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_requestStarRule.Instance);
            actions.Add("releaseStarRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_releaseStarRule.Instance);
            actions.Add("requestSimpleRule", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_requestSimpleRule.Instance);
            actions.Add("aux_attachResource", (de.unika.ipd.grGen.lgsp.LGSPAction) Action_aux_attachResource.Instance);
        }

        public override String Name { get { return "MutexPimpedActions"; } }
        public override String ModelMD5Hash { get { return "b2c79abf46750619401de30166fff963"; } }
    }
}