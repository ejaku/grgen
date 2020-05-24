// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\examples\FunctionsProceduresExample\FunctionsProceduresExample.grg" on Sun May 24 19:21:40 CEST 2020

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_FunctionsProceduresExample;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_FunctionsProceduresExample;

namespace de.unika.ipd.grGen.Action_FunctionsProceduresExample
{
	public class Rule_init : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_init instance = null;
		public static Rule_init Instance { get { if(instance==null) { instance = new Rule_init(); instance.initialize(); } return instance; } }

		public enum init_NodeNums { };
		public enum init_EdgeNums { };
		public enum init_VariableNums { };
		public enum init_SubNums { };
		public enum init_AltNums { };
		public enum init_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_init;


		private Rule_init()
			: base("init",
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { }
			)
		{
		}
		private void initialize()
		{
			bool[,] init_isNodeHomomorphicGlobal = new bool[0, 0];
			bool[,] init_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] init_isNodeTotallyHomomorphic = new bool[0];
			bool[] init_isEdgeTotallyHomomorphic = new bool[0];
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
			GRGEN_MODEL.@NN node_nn = GRGEN_MODEL.@NN.CreateNode(graph);
			graph.SettingAddedEdgeNames( init_addedEdgeNames );
			GRGEN_MODEL.@EE edge__edge0 = GRGEN_MODEL.@EE.CreateEdge(graph, node_nn, node_nn);
			return;
		}
		private static string[] init_addedNodeNames = new string[] { "nn" };
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

		public class Match_init : GRGEN_LGSP.MatchListElement<Match_init>, IMatch_init
		{
			public enum init_NodeNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 0;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum init_EdgeNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 0;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum init_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0;} }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum init_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0;} }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum init_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0;} }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum init_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0;} }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum init_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0;} }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_init.instance.pat_init; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_init(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_init nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_init cur = this;
				while(cur != null) {
					Match_init next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

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


		public class Extractor
		{
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_r : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_r instance = null;
		public static Rule_r Instance { get { if(instance==null) { instance = new Rule_r(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] r_node_nn_AllowedTypes = null;
		public static bool[] r_node_nn_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] r_edge_ee_AllowedTypes = null;
		public static bool[] r_edge_ee_IsAllowedType = null;
		public enum r_NodeNums { @nn, };
		public enum r_EdgeNums { @ee, };
		public enum r_VariableNums { };
		public enum r_SubNums { };
		public enum r_AltNums { };
		public enum r_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_r;


		private Rule_r()
			: base("r",
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new GRGEN_LGSP.LGSPFilter[] {
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirst", null, "keepFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLast", null, "keepLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepFirstFraction", null, "keepFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("keepLastFraction", null, "keepLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirst", null, "removeFirst", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLast", null, "removeLast", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(int)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeFirstFraction", null, "removeFirstFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
					new GRGEN_LGSP.LGSPFilterAutoSupplied("removeLastFraction", null, "removeLastFraction", null, new GRGEN_LIBGR.GrGenType[] {GRGEN_LIBGR.VarType.GetVarType(typeof(double)), }, new String[] {"param"}),
				},
				new GRGEN_LIBGR.MatchClassInfo[] { }
			)
		{
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
			GRGEN_LGSP.PatternNode r_node_nn = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@NN, GRGEN_MODEL.NodeType_NN.typeVar, "GRGEN_MODEL.INN", "r_node_nn", "nn", r_node_nn_AllowedTypes, r_node_nn_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge r_edge_ee = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@EE, GRGEN_MODEL.EdgeType_EE.typeVar, "GRGEN_MODEL.IEE", "r_edge_ee", "ee", r_edge_ee_AllowedTypes, r_edge_ee_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_r = new GRGEN_LGSP.PatternGraph(
				"r",
				"",
				null, "r",
				false, false,
				new GRGEN_LGSP.PatternNode[] { r_node_nn }, 
				new GRGEN_LGSP.PatternEdge[] { r_edge_ee }, 
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
				r_isNodeHomomorphicGlobal,
				r_isEdgeHomomorphicGlobal,
				r_isNodeTotallyHomomorphic,
				r_isEdgeTotallyHomomorphic
			);
			pat_r.edgeToSourceNode.Add(r_edge_ee, r_node_nn);
			pat_r.edgeToTargetNode.Add(r_edge_ee, r_node_nn);

			r_node_nn.pointOfDefinition = pat_r;
			r_edge_ee.pointOfDefinition = pat_r;

			patternGraph = pat_r;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_r curMatch = (Match_r)_curMatch;
			GRGEN_LGSP.LGSPNode node_nn = curMatch._node_nn;
			GRGEN_LGSP.LGSPEdge edge_ee = curMatch._edge_ee;
			graph.SettingAddedNodeNames( r_addedNodeNames );
			graph.SettingAddedEdgeNames( r_addedEdgeNames );
			{ // eval_0
				string var_tmp = (string)("bla");
				string outvar_0;
				((GRGEN_MODEL.INN) node_nn).@bla(actionEnv, graph, var_tmp, out outvar_0);
				var_tmp = (string) (outvar_0);
				string outvar_1;
				((GRGEN_MODEL.IEE) edge_ee).@bla(actionEnv, graph, var_tmp, out outvar_1);
				var_tmp = (string) (outvar_1);
			}
			return;
		}
		private static string[] r_addedNodeNames = new string[] {  };
		private static string[] r_addedEdgeNames = new string[] {  };

		static Rule_r() {
		}

		public interface IMatch_r : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.INN node_nn { get; set; }
			//Edges
			GRGEN_MODEL.IEE edge_ee { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_r : GRGEN_LGSP.MatchListElement<Match_r>, IMatch_r
		{
			public GRGEN_MODEL.INN node_nn { get { return (GRGEN_MODEL.INN)_node_nn; } set { _node_nn = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_nn;
			public enum r_NodeNums { @nn, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)r_NodeNums.@nn: return _node_nn;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "nn": return _node_nn;
				default: return null;
				}
			}

			public GRGEN_MODEL.IEE edge_ee { get { return (GRGEN_MODEL.IEE)_edge_ee; } set { _edge_ee = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_ee;
			public enum r_EdgeNums { @ee, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)r_EdgeNums.@ee: return _edge_ee;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "ee": return _edge_ee;
				default: return null;
				}
			}

			public enum r_VariableNums { END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 0;} }
			public override object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum r_SubNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public override int NumberOfEmbeddedGraphs { get { return 0;} }
			public override GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getEmbeddedGraph(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum r_AltNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public override int NumberOfAlternatives { get { return 0;} }
			public override GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getAlternative(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum r_IterNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public override int NumberOfIterateds { get { return 0;} }
			public override GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatches getIterated(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public enum r_IdptNums { END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public override int NumberOfIndependents { get { return 0;} }
			public override GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IMatch getIndependent(string name)
			{
				switch(name) {
				default: return null;
				}
			}

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_r.instance.pat_r; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_r(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_r nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_r cur = this;
				while(cur != null) {
					Match_r next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_r that)
			{
				_node_nn = that._node_nn;
				_edge_ee = that._edge_ee;
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
				if(_node_nn != that._node_nn) return false;
				if(_edge_ee != that._edge_ee) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.INN> Extract_nn(List<IMatch_r> matchList)
			{
				List<GRGEN_MODEL.INN> resultList = new List<GRGEN_MODEL.INN>(matchList.Count);
				foreach(IMatch_r match in matchList)
					resultList.Add(match.node_nn);
				return resultList;
			}
			public static List<GRGEN_MODEL.IEE> Extract_ee(List<IMatch_r> matchList)
			{
				List<GRGEN_MODEL.IEE> resultList = new List<GRGEN_MODEL.IEE>(matchList.Count);
				foreach(IMatch_r match in matchList)
					resultList.Add(match.edge_ee);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
		public static List<GRGEN_ACTIONS.Rule_r.IMatch_r> Array_r_keepOneForEachBy_nn(List<GRGEN_ACTIONS.Rule_r.IMatch_r> list)
		{
			List<GRGEN_ACTIONS.Rule_r.IMatch_r> newList = new List<GRGEN_ACTIONS.Rule_r.IMatch_r>();
			Dictionary<GRGEN_MODEL.INN, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.INN, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_r.IMatch_r element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@node_nn)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@node_nn, null);
				}
			}
			return newList;
		}
		public static List<GRGEN_ACTIONS.Rule_r.IMatch_r> Array_r_keepOneForEachBy_ee(List<GRGEN_ACTIONS.Rule_r.IMatch_r> list)
		{
			List<GRGEN_ACTIONS.Rule_r.IMatch_r> newList = new List<GRGEN_ACTIONS.Rule_r.IMatch_r>();
			Dictionary<GRGEN_MODEL.IEE, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<GRGEN_MODEL.IEE, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_r.IMatch_r element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@edge_ee)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@edge_ee, null);
				}
			}
			return newList;
		}
	}

	public class SequenceInfo_s : GRGEN_LIBGR.DefinedSequenceInfo
	{
		private static SequenceInfo_s instance = null;
		public static SequenceInfo_s Instance { get { if(instance==null) { instance = new SequenceInfo_s(); } return instance; } }

		private SequenceInfo_s()
			: base(
				new String[] { "nn", "ee",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_NN.typeVar, GRGEN_MODEL.EdgeType_EE.typeVar,  },
				new String[] { "no", "eo",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, GRGEN_MODEL.EdgeType_E.typeVar,  },
				"s",
				null, "s",
				"r() ;> {(no,eo)=dur(nn,ee)} ;> {dur(nn,ee)}",
				35
			)
		{
			annotations.annotations.Add("foo", "bar");
		}
	}

	public class Functions
	{
		public static int hur(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPGraph graph, GRGEN_MODEL.INN node_nn, GRGEN_MODEL.IEE edge_ee)
		{
			return (((GRGEN_MODEL.INN) node_nn).@foo(actionEnv, graph, 42) + ((GRGEN_MODEL.IEE) edge_ee).@foo(actionEnv, graph, 42));
		}


		static Functions() {
		}

	}

	public class FunctionInfo_hur : GRGEN_LIBGR.FunctionInfo
	{
		private static FunctionInfo_hur instance = null;
		public static FunctionInfo_hur Instance { get { if(instance==null) { instance = new FunctionInfo_hur(); } return instance; } }

		private FunctionInfo_hur()
			: base(
				"hur",
				null, "hur",
				false,
				new String[] { "nn", "ee",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_NN.typeVar, GRGEN_MODEL.EdgeType_EE.typeVar,  },
				GRGEN_LIBGR.VarType.GetVarType(typeof(int))
			)
		{
			annotations.annotations.Add("foo", "bar");
		}
		public override object Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			return GRGEN_ACTIONS.Functions.hur((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.INN)arguments[0], (GRGEN_MODEL.IEE)arguments[1]);
		}
	}

	public class Procedures
	{
		public static void dur(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LGSP.LGSPGraph graph, GRGEN_MODEL.INN node_nn, GRGEN_MODEL.IEE edge_ee, out GRGEN_MODEL.IN _out_param_0, out GRGEN_MODEL.IE _out_param_1)
		{
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugEntering("dur", node_nn, edge_ee);
			((GRGEN_MODEL.INN) node_nn).@bar(actionEnv, graph, 42);
			((GRGEN_MODEL.IEE) edge_ee).@bar(actionEnv, graph, 42);
			_out_param_0 = node_nn;
			_out_param_1 = edge_ee;
			((GRGEN_LGSP.LGSPSubactionAndOutputAdditionEnvironment)actionEnv).DebugExiting("dur", _out_param_0, _out_param_1);
			return;
		}


		static Procedures() {
		}

	}

	public class ProcedureInfo_dur : GRGEN_LIBGR.ProcedureInfo
	{
		private static ProcedureInfo_dur instance = null;
		public static ProcedureInfo_dur Instance { get { if(instance==null) { instance = new ProcedureInfo_dur(); } return instance; } }

		private ProcedureInfo_dur()
			: base(
				"dur",
				null, "dur",
				false,
				new String[] { "nn", "ee",  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_NN.typeVar, GRGEN_MODEL.EdgeType_EE.typeVar,  },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_N.typeVar, GRGEN_MODEL.EdgeType_E.typeVar, }
			)
		{
			annotations.annotations.Add("foo", "bar");
		}
		public override object[] Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IGraph graph, object[] arguments)
		{
			GRGEN_MODEL.IN _out_param_0;
			GRGEN_MODEL.IE _out_param_1;
			GRGEN_ACTIONS.Procedures.dur((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, (GRGEN_LGSP.LGSPGraph)graph, (GRGEN_MODEL.INN)arguments[0], (GRGEN_MODEL.IEE)arguments[1], out _out_param_0, out _out_param_1);
			ReturnArray[0] = _out_param_0;
			ReturnArray[1] = _out_param_1;
			return ReturnArray;
		}
	}

	public partial class MatchFilters
	{

		static MatchFilters() {
		}

	}

	public partial class MatchClassFilters
	{

		static MatchClassFilters() {
		}

	}



	//-----------------------------------------------------------

	public class FunctionsProceduresExample_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public FunctionsProceduresExample_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[2];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+2];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[1];
			functions = new GRGEN_LIBGR.FunctionInfo[1+0];
			procedures = new GRGEN_LIBGR.ProcedureInfo[1+0];
			matchClasses = new GRGEN_LIBGR.MatchClassInfo[0];
			packages = new string[0];
			rules[0] = Rule_init.Instance;
			rulesAndSubpatterns[0+0] = Rule_init.Instance;
			rules[1] = Rule_r.Instance;
			rulesAndSubpatterns[0+1] = Rule_r.Instance;
			definedSequences[0] = SequenceInfo_s.Instance;
			functions[0] = FunctionInfo_hur.Instance;
			procedures[0] = ProcedureInfo_dur.Instance;
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
		public override GRGEN_LIBGR.MatchClassInfo[] MatchClasses { get { return matchClasses; } }
		private GRGEN_LIBGR.MatchClassInfo[] matchClasses;
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
        public Action_init()
            : base(Rule_init.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_init.Instance;
            DynamicMatch = myMatch;
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
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
        public static List<GRGEN_ACTIONS.Rule_init.IMatch_init> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_init.IMatch_init>)
                return ((List<GRGEN_ACTIONS.Rule_init.IMatch_init>)parameter);
            else
                return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_init.IMatch_init>((IList<GRGEN_LIBGR.IMatch>)parameter);
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
        public Action_r()
            : base(Rule_r.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_r.Instance;
            DynamicMatch = myMatch;
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
            // Lookup r_edge_ee 
            int type_id_candidate_r_edge_ee = 4;
            for(GRGEN_LGSP.LGSPEdge head_candidate_r_edge_ee = graph.edgesByTypeHeads[type_id_candidate_r_edge_ee], candidate_r_edge_ee = head_candidate_r_edge_ee.lgspTypeNext; candidate_r_edge_ee != head_candidate_r_edge_ee; candidate_r_edge_ee = candidate_r_edge_ee.lgspTypeNext)
            {
                // Implicit Source r_node_nn from r_edge_ee 
                GRGEN_LGSP.LGSPNode candidate_r_node_nn = candidate_r_edge_ee.lgspSource;
                if(candidate_r_node_nn.lgspType.TypeID!=2) {
                    continue;
                }
                if(candidate_r_edge_ee.lgspSource != candidate_r_node_nn) {
                    continue;
                }
                if(candidate_r_edge_ee.lgspTarget != candidate_r_node_nn) {
                    continue;
                }
                Rule_r.Match_r match = matches.GetNextUnfilledPosition();
                match._node_nn = candidate_r_node_nn;
                match._edge_ee = candidate_r_edge_ee;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_r_edge_ee);
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
            switch(filter.PackagePrefixedName) {
                case "keepFirst": matches.Filter_keepFirst((System.Int32)(filter.Arguments[0])); break;
                case "keepLast": matches.Filter_keepLast((System.Int32)(filter.Arguments[0])); break;
                case "keepFirstFraction": matches.Filter_keepFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "keepLastFraction": matches.Filter_keepLastFraction((System.Double)(filter.Arguments[0])); break;
                case "removeFirst": matches.Filter_removeFirst((System.Int32)(filter.Arguments[0])); break;
                case "removeLast": matches.Filter_removeLast((System.Int32)(filter.Arguments[0])); break;
                case "removeFirstFraction": matches.Filter_removeFirstFraction((System.Double)(filter.Arguments[0])); break;
                case "removeLastFraction": matches.Filter_removeLastFraction((System.Double)(filter.Arguments[0])); break;
                default: throw new Exception("Unknown filter name " + filter.PackagePrefixedName + "!");
            }
        }
        public static List<GRGEN_ACTIONS.Rule_r.IMatch_r> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_r.IMatch_r>)
                return ((List<GRGEN_ACTIONS.Rule_r.IMatch_r>)parameter);
            else
                return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_r.IMatch_r>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    

    public class Sequence_s : GRGEN_LIBGR.SequenceDefinitionCompiled
    {
        private static Sequence_s instance = null;
        public static Sequence_s Instance { get { if(instance==null) instance = new Sequence_s(); return instance; } }
        private Sequence_s() : base("s", SequenceInfo_s.Instance) { }

        private object[] ReturnValues = new object[2];

        public static bool ApplyXGRS_s(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.INN var_nn, GRGEN_MODEL.IEE var_ee, ref GRGEN_MODEL.IN var_no, ref GRGEN_MODEL.IE var_eo)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            procEnv.DebugEntering("s", var_nn, var_ee);
            bool res_10;
            bool res_5;
            bool res_0;
            GRGEN_ACTIONS.Action_r rule_r = GRGEN_ACTIONS.Action_r.Instance;
            bool res_4;
            object res_3;
            bool res_9;
            object res_8;
            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_r.IMatch_r> matches_0 = rule_r.Match(procEnv, 1);
            procEnv.PerformanceInfo.MatchesFound += matches_0.Count;
            if(matches_0.Count == 0) {
                res_0 = (bool)(false);
            } else {
                res_0 = (bool)(true);
                procEnv.Matched(matches_0, null, false);
                procEnv.Finishing(matches_0, false);
                GRGEN_ACTIONS.Rule_r.IMatch_r match_0 = matches_0.FirstExact;
                rule_r.Modify(procEnv, match_0);
                procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_0, false);
            }
            GRGEN_MODEL.IN tmpvar_0no; GRGEN_MODEL.IE tmpvar_1eo; 
            GRGEN_ACTIONS.Procedures.dur(procEnv, graph, (GRGEN_MODEL.INN)var_nn, (GRGEN_MODEL.IEE)var_ee, out tmpvar_0no, out tmpvar_1eo);
            var_no = (GRGEN_MODEL.IN)(tmpvar_0no);
var_eo = (GRGEN_MODEL.IE)(tmpvar_1eo);

            res_3 = null;
            res_4 = (bool)(true);
            res_5 = (bool)(res_4);
            GRGEN_MODEL.IN tmpvar_2; GRGEN_MODEL.IE tmpvar_3; 
            GRGEN_ACTIONS.Procedures.dur(procEnv, graph, (GRGEN_MODEL.INN)var_nn, (GRGEN_MODEL.IEE)var_ee, out tmpvar_2, out tmpvar_3);
            res_8 = null;
            res_9 = (bool)(true);
            res_10 = (bool)(res_9);
            procEnv.DebugExiting("s", var_no, var_eo);
            return res_10;
        }

        public static bool Apply_s(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, GRGEN_MODEL.INN var_nn, GRGEN_MODEL.IEE var_ee, ref GRGEN_MODEL.IN var_no, ref GRGEN_MODEL.IE var_eo)
        {
            GRGEN_MODEL.IN vari_no = null;
            GRGEN_MODEL.IE vari_eo = null;
            bool result = ApplyXGRS_s((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_nn, var_ee, ref var_no, ref var_eo);
            if(result) {
                var_no = vari_no;
                var_eo = vari_eo;
            }
            return result;
        }

        public override bool Apply(GRGEN_LIBGR.IGraphProcessingEnvironment procEnv, object[] arguments, out object[] returnValues)        {
            GRGEN_LGSP.LGSPGraph graph = ((GRGEN_LGSP.LGSPActionExecutionEnvironment)procEnv).graph;
            GRGEN_MODEL.INN var_nn = (GRGEN_MODEL.INN)arguments[0];
            GRGEN_MODEL.IEE var_ee = (GRGEN_MODEL.IEE)arguments[1];
            GRGEN_MODEL.IN var_no = null;
            GRGEN_MODEL.IE var_eo = null;
            bool result = ApplyXGRS_s((GRGEN_LGSP.LGSPGraphProcessingEnvironment)procEnv, var_nn, var_ee, ref var_no, ref var_eo);
            returnValues = ReturnValues;
            if(result) {
                returnValues[0] = var_no;
                returnValues[1] = var_eo;
            }
            return result;
        }
    }

    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class FunctionsProceduresExampleActions : GRGEN_LGSP.LGSPActions
    {
        public FunctionsProceduresExampleActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public FunctionsProceduresExampleActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            packages = new string[0];
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_init.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_init.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_init.Instance);
            actions.Add("init", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_init.Instance);
            @init = GRGEN_ACTIONS.Action_init.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_r.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_r.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_r.Instance);
            actions.Add("r", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_r.Instance);
            @r = GRGEN_ACTIONS.Action_r.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_init.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_r.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_init.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_r.Instance.patternGraph);
            GRGEN_ACTIONS.Rule_init.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_r.Instance.patternGraph.maxIsoSpace = 0;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_init.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_r.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
            RegisterGraphRewriteSequenceDefinition(GRGEN_ACTIONS.Sequence_s.Instance);
            @s = GRGEN_ACTIONS.Sequence_s.Instance;
            namesToFunctionDefinitions.Add("hur", GRGEN_ACTIONS.FunctionInfo_hur.Instance);
            namesToProcedureDefinitions.Add("dur", GRGEN_ACTIONS.ProcedureInfo_dur.Instance);
        }
        
        public GRGEN_ACTIONS.IAction_init @init;
        public GRGEN_ACTIONS.IAction_r @r;
        
        public GRGEN_ACTIONS.Sequence_s @s;
        
        public override string[] Packages { get { return packages; } }
        private string[] packages;
        
        public override string Name { get { return "FunctionsProceduresExampleActions"; } }
        public override string StatisticsPath { get { return null; } }
        public override bool LazyNIC { get { return false; } }
        public override bool InlineIndependents { get { return true; } }
        public override bool Profile { get { return false; } }

        public override void FailAssertion() { Debug.Assert(false); }
        public override string ModelMD5Hash { get { return "9435e7299d00cb79680575270aa3cce6"; } }
    }
}