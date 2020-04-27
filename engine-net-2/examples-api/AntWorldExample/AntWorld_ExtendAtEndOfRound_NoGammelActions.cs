// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\antWorld\AntWorld_ExtendAtEndOfRound_NoGammel.grg" on Mon Apr 27 20:32:28 CEST 2020

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_AntWorld_NoGammel;
using GRGEN_ACTIONS = de.unika.ipd.grGen.Action_AntWorld_ExtendAtEndOfRound_NoGammel;

namespace de.unika.ipd.grGen.Action_AntWorld_ExtendAtEndOfRound_NoGammel
{
	public class Rule_InitWorld : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_InitWorld instance = null;
		public static Rule_InitWorld Instance { get { if (instance==null) { instance = new Rule_InitWorld(); instance.initialize(); } return instance; } }

		public enum InitWorld_NodeNums { };
		public enum InitWorld_EdgeNums { };
		public enum InitWorld_VariableNums { };
		public enum InitWorld_SubNums { };
		public enum InitWorld_AltNums { };
		public enum InitWorld_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_InitWorld;


		private Rule_InitWorld()
			: base("InitWorld",
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
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
			bool[,] InitWorld_isNodeHomomorphicGlobal = new bool[0, 0];
			bool[,] InitWorld_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] InitWorld_isNodeTotallyHomomorphic = new bool[0];
			bool[] InitWorld_isEdgeTotallyHomomorphic = new bool[0];
			pat_InitWorld = new GRGEN_LGSP.PatternGraph(
				"InitWorld",
				"",
				null, "InitWorld",
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
				InitWorld_isNodeHomomorphicGlobal,
				InitWorld_isEdgeHomomorphicGlobal,
				InitWorld_isNodeTotallyHomomorphic,
				InitWorld_isEdgeTotallyHomomorphic
			);


			patternGraph = pat_InitWorld;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IAnt output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_InitWorld curMatch = (Match_InitWorld)_curMatch;
			graph.SettingAddedNodeNames( InitWorld_addedNodeNames );
			GRGEN_MODEL.@GridCornerNode node_b2 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@AntHill node_hill = GRGEN_MODEL.@AntHill.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_b3 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_c3 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_c2 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_b1 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_a1 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_a2 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_a3 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_a4 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_b4 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_c4 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_d4 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_d3 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_d2 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_d1 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_c1 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@Ant node_queen = GRGEN_MODEL.@Ant.CreateNode(graph);
			GRGEN_MODEL.@Ant node_atta = GRGEN_MODEL.@Ant.CreateNode(graph);
			GRGEN_MODEL.@Ant node_flick = GRGEN_MODEL.@Ant.CreateNode(graph);
			GRGEN_MODEL.@Ant node_dot = GRGEN_MODEL.@Ant.CreateNode(graph);
			GRGEN_MODEL.@Ant node_fred = GRGEN_MODEL.@Ant.CreateNode(graph);
			GRGEN_MODEL.@Ant node_slim = GRGEN_MODEL.@Ant.CreateNode(graph);
			GRGEN_MODEL.@Ant node_chewap = GRGEN_MODEL.@Ant.CreateNode(graph);
			GRGEN_MODEL.@Ant node_cici = GRGEN_MODEL.@Ant.CreateNode(graph);
			graph.SettingAddedEdgeNames( InitWorld_addedEdgeNames );
			GRGEN_MODEL.@PathToHill edge__edge0 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_b2, node_hill);
			GRGEN_MODEL.@PathToHill edge__edge1 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_b3, node_hill);
			GRGEN_MODEL.@PathToHill edge__edge2 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_c3, node_hill);
			GRGEN_MODEL.@PathToHill edge__edge3 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_c2, node_hill);
			GRGEN_MODEL.@GridEdge edge__edge4 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_b2, node_b3);
			GRGEN_MODEL.@GridEdge edge__edge5 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_b3, node_c3);
			GRGEN_MODEL.@GridEdge edge__edge6 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_c3, node_c2);
			GRGEN_MODEL.@GridEdge edge__edge7 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_c2, node_b2);
			GRGEN_MODEL.@PathToHill edge__edge8 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_b1, node_b2);
			GRGEN_MODEL.@PathToHill edge__edge9 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_a1, node_b2);
			GRGEN_MODEL.@PathToHill edge__edge10 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_a2, node_b2);
			GRGEN_MODEL.@PathToHill edge__edge11 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_a3, node_b3);
			GRGEN_MODEL.@PathToHill edge__edge12 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_a4, node_b3);
			GRGEN_MODEL.@PathToHill edge__edge13 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_b4, node_b3);
			GRGEN_MODEL.@PathToHill edge__edge14 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_c4, node_c3);
			GRGEN_MODEL.@PathToHill edge__edge15 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_d4, node_c3);
			GRGEN_MODEL.@PathToHill edge__edge16 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_d3, node_c3);
			GRGEN_MODEL.@PathToHill edge__edge17 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_d2, node_c2);
			GRGEN_MODEL.@PathToHill edge__edge18 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_d1, node_c2);
			GRGEN_MODEL.@PathToHill edge__edge19 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_c1, node_c2);
			GRGEN_MODEL.@GridEdge edge__edge20 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_a1, node_a2);
			GRGEN_MODEL.@GridEdge edge__edge21 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_a2, node_a3);
			GRGEN_MODEL.@GridEdge edge__edge22 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_a3, node_a4);
			GRGEN_MODEL.@GridEdge edge__edge23 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_a4, node_b4);
			GRGEN_MODEL.@GridEdge edge__edge24 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_b4, node_c4);
			GRGEN_MODEL.@GridEdge edge__edge25 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_c4, node_d4);
			GRGEN_MODEL.@GridEdge edge__edge26 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_d4, node_d3);
			GRGEN_MODEL.@GridEdge edge__edge27 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_d3, node_d2);
			GRGEN_MODEL.@GridEdge edge__edge28 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_d2, node_d1);
			GRGEN_MODEL.@GridEdge edge__edge29 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_d1, node_c1);
			GRGEN_MODEL.@GridEdge edge__edge30 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_c1, node_b1);
			GRGEN_MODEL.@GridEdge edge__edge31 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_b1, node_a1);
			GRGEN_MODEL.@AntPosition edge__edge32 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_queen, node_hill);
			GRGEN_MODEL.@AntPosition edge__edge33 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_atta, node_hill);
			GRGEN_MODEL.@AntPosition edge__edge34 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_flick, node_hill);
			GRGEN_MODEL.@AntPosition edge__edge35 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_dot, node_hill);
			GRGEN_MODEL.@AntPosition edge__edge36 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_fred, node_hill);
			GRGEN_MODEL.@AntPosition edge__edge37 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_slim, node_hill);
			GRGEN_MODEL.@AntPosition edge__edge38 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_chewap, node_hill);
			GRGEN_MODEL.@AntPosition edge__edge39 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_cici, node_hill);
			GRGEN_MODEL.@NextAnt edge__edge40 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_queen, node_atta);
			GRGEN_MODEL.@NextAnt edge__edge41 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_atta, node_flick);
			GRGEN_MODEL.@NextAnt edge__edge42 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_flick, node_dot);
			GRGEN_MODEL.@NextAnt edge__edge43 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_dot, node_fred);
			GRGEN_MODEL.@NextAnt edge__edge44 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_fred, node_slim);
			GRGEN_MODEL.@NextAnt edge__edge45 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_slim, node_chewap);
			GRGEN_MODEL.@NextAnt edge__edge46 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_chewap, node_cici);
			output_0 = (GRGEN_MODEL.IAnt)(node_queen);
			return;
		}
		private static string[] InitWorld_addedNodeNames = new string[] { "b2", "hill", "b3", "c3", "c2", "b1", "a1", "a2", "a3", "a4", "b4", "c4", "d4", "d3", "d2", "d1", "c1", "queen", "atta", "flick", "dot", "fred", "slim", "chewap", "cici" };
		private static string[] InitWorld_addedEdgeNames = new string[] { "_edge0", "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6", "_edge7", "_edge8", "_edge9", "_edge10", "_edge11", "_edge12", "_edge13", "_edge14", "_edge15", "_edge16", "_edge17", "_edge18", "_edge19", "_edge20", "_edge21", "_edge22", "_edge23", "_edge24", "_edge25", "_edge26", "_edge27", "_edge28", "_edge29", "_edge30", "_edge31", "_edge32", "_edge33", "_edge34", "_edge35", "_edge36", "_edge37", "_edge38", "_edge39", "_edge40", "_edge41", "_edge42", "_edge43", "_edge44", "_edge45", "_edge46" };

		static Rule_InitWorld() {
		}

		public interface IMatch_InitWorld : GRGEN_LIBGR.IMatch
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

		public class Match_InitWorld : GRGEN_LGSP.MatchListElement<Match_InitWorld>, IMatch_InitWorld
		{
			public enum InitWorld_NodeNums { END_OF_ENUM };
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

			public enum InitWorld_EdgeNums { END_OF_ENUM };
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

			public enum InitWorld_VariableNums { END_OF_ENUM };
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

			public enum InitWorld_SubNums { END_OF_ENUM };
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

			public enum InitWorld_AltNums { END_OF_ENUM };
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

			public enum InitWorld_IterNums { END_OF_ENUM };
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

			public enum InitWorld_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_InitWorld.instance.pat_InitWorld; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_InitWorld(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_InitWorld nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_InitWorld cur = this;
				while(cur != null) {
					Match_InitWorld next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_InitWorld that)
			{
			}

			public Match_InitWorld(Match_InitWorld that)
			{
				CopyMatchContent(that);
			}
			public Match_InitWorld()
			{
			}

			public bool IsEqual(Match_InitWorld that)
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

	public class Rule_TakeFood : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_TakeFood instance = null;
		public static Rule_TakeFood Instance { get { if (instance==null) { instance = new Rule_TakeFood(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] TakeFood_node_curAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] TakeFood_node_n_AllowedTypes = { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridCornerNode.typeVar, };
		public static bool[] TakeFood_node_curAnt_IsAllowedType = null;
		public static bool[] TakeFood_node_n_IsAllowedType = { false, true, true, false, false, };
		public static GRGEN_LIBGR.EdgeType[] TakeFood_edge__edge0_AllowedTypes = null;
		public static bool[] TakeFood_edge__edge0_IsAllowedType = null;
		public enum TakeFood_NodeNums { @curAnt, @n, };
		public enum TakeFood_EdgeNums { @_edge0, };
		public enum TakeFood_VariableNums { };
		public enum TakeFood_SubNums { };
		public enum TakeFood_AltNums { };
		public enum TakeFood_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_TakeFood;


		private Rule_TakeFood()
			: base("TakeFood",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "TakeFood_node_curAnt", },
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
			bool[,] TakeFood_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] TakeFood_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] TakeFood_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] TakeFood_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode TakeFood_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "TakeFood_node_curAnt", "curAnt", TakeFood_node_curAnt_AllowedTypes, TakeFood_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode TakeFood_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "TakeFood_node_n", "n", TakeFood_node_n_AllowedTypes, TakeFood_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge TakeFood_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, GRGEN_MODEL.EdgeType_AntPosition.typeVar, "GRGEN_MODEL.IAntPosition", "TakeFood_edge__edge0", "_edge0", TakeFood_edge__edge0_AllowedTypes, TakeFood_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition TakeFood_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.LOG_AND(new GRGEN_EXPR.LOG_NOT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAnt", "TakeFood_node_curAnt", "hasFood")), new GRGEN_EXPR.GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IGridNode", "TakeFood_node_n", "food"), new GRGEN_EXPR.Constant("0"))),
				new string[] { "TakeFood_node_curAnt", "TakeFood_node_n" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { TakeFood_node_curAnt, TakeFood_node_n }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_TakeFood = new GRGEN_LGSP.PatternGraph(
				"TakeFood",
				"",
				null, "TakeFood",
				false, false,
				new GRGEN_LGSP.PatternNode[] { TakeFood_node_curAnt, TakeFood_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { TakeFood_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { TakeFood_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				TakeFood_isNodeHomomorphicGlobal,
				TakeFood_isEdgeHomomorphicGlobal,
				TakeFood_isNodeTotallyHomomorphic,
				TakeFood_isEdgeTotallyHomomorphic
			);
			pat_TakeFood.edgeToSourceNode.Add(TakeFood_edge__edge0, TakeFood_node_curAnt);
			pat_TakeFood.edgeToTargetNode.Add(TakeFood_edge__edge0, TakeFood_node_n);

			TakeFood_node_curAnt.pointOfDefinition = null;
			TakeFood_node_n.pointOfDefinition = pat_TakeFood;
			TakeFood_edge__edge0.pointOfDefinition = pat_TakeFood;

			patternGraph = pat_TakeFood;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_TakeFood curMatch = (Match_TakeFood)_curMatch;
			GRGEN_LGSP.LGSPNode node_curAnt = curMatch._node_curAnt;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			GRGEN_MODEL.IAnt inode_curAnt = curMatch.node_curAnt;
			GRGEN_MODEL.IGridNode inode_n = curMatch.node_n;
			graph.SettingAddedNodeNames( TakeFood_addedNodeNames );
			graph.SettingAddedEdgeNames( TakeFood_addedEdgeNames );
			{ // eval_0
				bool tempvar_0 = (bool )true;
				graph.ChangingNodeAttribute(node_curAnt, GRGEN_MODEL.NodeType_Ant.AttributeType_hasFood, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_curAnt.@hasFood = tempvar_0;
				graph.ChangedNodeAttribute(node_curAnt, GRGEN_MODEL.NodeType_Ant.AttributeType_hasFood);
				int tempvar_1 = (int )(inode_n.@food - 1);
				graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_1, null);
				inode_n.@food = tempvar_1;
				graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_food);
			}
			return;
		}
		private static string[] TakeFood_addedNodeNames = new string[] {  };
		private static string[] TakeFood_addedEdgeNames = new string[] {  };

		static Rule_TakeFood() {
		}

		public interface IMatch_TakeFood : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; set; }
			GRGEN_MODEL.IGridNode node_n { get; set; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_TakeFood : GRGEN_LGSP.MatchListElement<Match_TakeFood>, IMatch_TakeFood
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } set { _node_curAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum TakeFood_NodeNums { @curAnt, @n, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)TakeFood_NodeNums.@curAnt: return _node_curAnt;
				case (int)TakeFood_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "curAnt": return _node_curAnt;
				case "n": return _node_n;
				default: return null;
				}
			}

			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum TakeFood_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)TakeFood_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum TakeFood_VariableNums { END_OF_ENUM };
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

			public enum TakeFood_SubNums { END_OF_ENUM };
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

			public enum TakeFood_AltNums { END_OF_ENUM };
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

			public enum TakeFood_IterNums { END_OF_ENUM };
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

			public enum TakeFood_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_TakeFood.instance.pat_TakeFood; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_TakeFood(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_TakeFood nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_TakeFood cur = this;
				while(cur != null) {
					Match_TakeFood next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_TakeFood that)
			{
				_node_curAnt = that._node_curAnt;
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_TakeFood(Match_TakeFood that)
			{
				CopyMatchContent(that);
			}
			public Match_TakeFood()
			{
			}

			public bool IsEqual(Match_TakeFood that)
			{
				if(that==null) return false;
				if(_node_curAnt != that._node_curAnt) return false;
				if(_node_n != that._node_n) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_curAnt(List<IMatch_TakeFood> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_TakeFood match in matchList)
					resultList.Add(match.node_curAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_n(List<IMatch_TakeFood> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_TakeFood match in matchList)
					resultList.Add(match.node_n);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntPosition> Extract__edge0(List<IMatch_TakeFood> matchList)
			{
				List<GRGEN_MODEL.IAntPosition> resultList = new List<GRGEN_MODEL.IAntPosition>(matchList.Count);
				foreach(IMatch_TakeFood match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_GoHome : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GoHome instance = null;
		public static Rule_GoHome Instance { get { if (instance==null) { instance = new Rule_GoHome(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GoHome_node_curAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GoHome_node_old_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GoHome_node_new_AllowedTypes = null;
		public static bool[] GoHome_node_curAnt_IsAllowedType = null;
		public static bool[] GoHome_node_old_IsAllowedType = null;
		public static bool[] GoHome_node_new_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] GoHome_edge_oldPos_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] GoHome_edge__edge0_AllowedTypes = null;
		public static bool[] GoHome_edge_oldPos_IsAllowedType = null;
		public static bool[] GoHome_edge__edge0_IsAllowedType = null;
		public enum GoHome_NodeNums { @curAnt, @old, @new, };
		public enum GoHome_EdgeNums { @oldPos, @_edge0, };
		public enum GoHome_VariableNums { };
		public enum GoHome_SubNums { };
		public enum GoHome_AltNums { };
		public enum GoHome_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_GoHome;


		private Rule_GoHome()
			: base("GoHome",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "GoHome_node_curAnt", },
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
			bool[,] GoHome_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] GoHome_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] GoHome_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] GoHome_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode GoHome_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "GoHome_node_curAnt", "curAnt", GoHome_node_curAnt_AllowedTypes, GoHome_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GoHome_node_old = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GoHome_node_old", "old", GoHome_node_old_AllowedTypes, GoHome_node_old_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GoHome_node_new = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GoHome_node_new", "new", GoHome_node_new_AllowedTypes, GoHome_node_new_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GoHome_edge_oldPos = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, GRGEN_MODEL.EdgeType_AntPosition.typeVar, "GRGEN_MODEL.IAntPosition", "GoHome_edge_oldPos", "oldPos", GoHome_edge_oldPos_AllowedTypes, GoHome_edge_oldPos_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GoHome_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, GRGEN_MODEL.EdgeType_PathToHill.typeVar, "GRGEN_MODEL.IPathToHill", "GoHome_edge__edge0", "_edge0", GoHome_edge__edge0_AllowedTypes, GoHome_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition GoHome_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAnt", "GoHome_node_curAnt", "hasFood"),
				new string[] { "GoHome_node_curAnt" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { GoHome_node_curAnt }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_GoHome = new GRGEN_LGSP.PatternGraph(
				"GoHome",
				"",
				null, "GoHome",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GoHome_node_curAnt, GoHome_node_old, GoHome_node_new }, 
				new GRGEN_LGSP.PatternEdge[] { GoHome_edge_oldPos, GoHome_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { GoHome_cond_0,  }, 
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
				GoHome_isNodeHomomorphicGlobal,
				GoHome_isEdgeHomomorphicGlobal,
				GoHome_isNodeTotallyHomomorphic,
				GoHome_isEdgeTotallyHomomorphic
			);
			pat_GoHome.edgeToSourceNode.Add(GoHome_edge_oldPos, GoHome_node_curAnt);
			pat_GoHome.edgeToTargetNode.Add(GoHome_edge_oldPos, GoHome_node_old);
			pat_GoHome.edgeToSourceNode.Add(GoHome_edge__edge0, GoHome_node_old);
			pat_GoHome.edgeToTargetNode.Add(GoHome_edge__edge0, GoHome_node_new);

			GoHome_node_curAnt.pointOfDefinition = null;
			GoHome_node_old.pointOfDefinition = pat_GoHome;
			GoHome_node_new.pointOfDefinition = pat_GoHome;
			GoHome_edge_oldPos.pointOfDefinition = pat_GoHome;
			GoHome_edge__edge0.pointOfDefinition = pat_GoHome;

			patternGraph = pat_GoHome;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GoHome curMatch = (Match_GoHome)_curMatch;
			GRGEN_LGSP.LGSPNode node_old = curMatch._node_old;
			GRGEN_LGSP.LGSPNode node_curAnt = curMatch._node_curAnt;
			GRGEN_LGSP.LGSPNode node_new = curMatch._node_new;
			GRGEN_MODEL.IGridNode inode_old = curMatch.node_old;
			GRGEN_LGSP.LGSPEdge edge_oldPos = curMatch._edge_oldPos;
			graph.SettingAddedNodeNames( GoHome_addedNodeNames );
			graph.SettingAddedEdgeNames( GoHome_addedEdgeNames );
			GRGEN_MODEL.@AntPosition edge__edge1 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_curAnt, node_new);
			{ // eval_0
				int tempvar_0 = (int )(inode_old.@pheromones + 1024);
				graph.ChangingNodeAttribute(node_old, GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_old.@pheromones = tempvar_0;
				graph.ChangedNodeAttribute(node_old, GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones);
			}
			graph.Remove(edge_oldPos);
			return;
		}
		private static string[] GoHome_addedNodeNames = new string[] {  };
		private static string[] GoHome_addedEdgeNames = new string[] { "_edge1" };

		static Rule_GoHome() {
		}

		public interface IMatch_GoHome : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; set; }
			GRGEN_MODEL.IGridNode node_old { get; set; }
			GRGEN_MODEL.IGridNode node_new { get; set; }
			//Edges
			GRGEN_MODEL.IAntPosition edge_oldPos { get; set; }
			GRGEN_MODEL.IPathToHill edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GoHome : GRGEN_LGSP.MatchListElement<Match_GoHome>, IMatch_GoHome
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } set { _node_curAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_old { get { return (GRGEN_MODEL.IGridNode)_node_old; } set { _node_old = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_new { get { return (GRGEN_MODEL.IGridNode)_node_new; } set { _node_new = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_old;
			public GRGEN_LGSP.LGSPNode _node_new;
			public enum GoHome_NodeNums { @curAnt, @old, @new, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GoHome_NodeNums.@curAnt: return _node_curAnt;
				case (int)GoHome_NodeNums.@old: return _node_old;
				case (int)GoHome_NodeNums.@new: return _node_new;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "curAnt": return _node_curAnt;
				case "old": return _node_old;
				case "new": return _node_new;
				default: return null;
				}
			}

			public GRGEN_MODEL.IAntPosition edge_oldPos { get { return (GRGEN_MODEL.IAntPosition)_edge_oldPos; } set { _edge_oldPos = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_oldPos;
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GoHome_EdgeNums { @oldPos, @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 2;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GoHome_EdgeNums.@oldPos: return _edge_oldPos;
				case (int)GoHome_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "oldPos": return _edge_oldPos;
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GoHome_VariableNums { END_OF_ENUM };
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

			public enum GoHome_SubNums { END_OF_ENUM };
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

			public enum GoHome_AltNums { END_OF_ENUM };
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

			public enum GoHome_IterNums { END_OF_ENUM };
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

			public enum GoHome_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GoHome.instance.pat_GoHome; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GoHome(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GoHome nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GoHome cur = this;
				while(cur != null) {
					Match_GoHome next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GoHome that)
			{
				_node_curAnt = that._node_curAnt;
				_node_old = that._node_old;
				_node_new = that._node_new;
				_edge_oldPos = that._edge_oldPos;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GoHome(Match_GoHome that)
			{
				CopyMatchContent(that);
			}
			public Match_GoHome()
			{
			}

			public bool IsEqual(Match_GoHome that)
			{
				if(that==null) return false;
				if(_node_curAnt != that._node_curAnt) return false;
				if(_node_old != that._node_old) return false;
				if(_node_new != that._node_new) return false;
				if(_edge_oldPos != that._edge_oldPos) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_curAnt(List<IMatch_GoHome> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_GoHome match in matchList)
					resultList.Add(match.node_curAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_old(List<IMatch_GoHome> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GoHome match in matchList)
					resultList.Add(match.node_old);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_new(List<IMatch_GoHome> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GoHome match in matchList)
					resultList.Add(match.node_new);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntPosition> Extract_oldPos(List<IMatch_GoHome> matchList)
			{
				List<GRGEN_MODEL.IAntPosition> resultList = new List<GRGEN_MODEL.IAntPosition>(matchList.Count);
				foreach(IMatch_GoHome match in matchList)
					resultList.Add(match.edge_oldPos);
				return resultList;
			}
			public static List<GRGEN_MODEL.IPathToHill> Extract__edge0(List<IMatch_GoHome> matchList)
			{
				List<GRGEN_MODEL.IPathToHill> resultList = new List<GRGEN_MODEL.IPathToHill>(matchList.Count);
				foreach(IMatch_GoHome match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_DropFood : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_DropFood instance = null;
		public static Rule_DropFood Instance { get { if (instance==null) { instance = new Rule_DropFood(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] DropFood_node_curAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] DropFood_node_hill_AllowedTypes = null;
		public static bool[] DropFood_node_curAnt_IsAllowedType = null;
		public static bool[] DropFood_node_hill_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] DropFood_edge__edge0_AllowedTypes = null;
		public static bool[] DropFood_edge__edge0_IsAllowedType = null;
		public enum DropFood_NodeNums { @curAnt, @hill, };
		public enum DropFood_EdgeNums { @_edge0, };
		public enum DropFood_VariableNums { };
		public enum DropFood_SubNums { };
		public enum DropFood_AltNums { };
		public enum DropFood_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_DropFood;


		private Rule_DropFood()
			: base("DropFood",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "DropFood_node_curAnt", },
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
			bool[,] DropFood_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] DropFood_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] DropFood_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] DropFood_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode DropFood_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "DropFood_node_curAnt", "curAnt", DropFood_node_curAnt_AllowedTypes, DropFood_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode DropFood_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, GRGEN_MODEL.NodeType_AntHill.typeVar, "GRGEN_MODEL.IAntHill", "DropFood_node_hill", "hill", DropFood_node_hill_AllowedTypes, DropFood_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge DropFood_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, GRGEN_MODEL.EdgeType_AntPosition.typeVar, "GRGEN_MODEL.IAntPosition", "DropFood_edge__edge0", "_edge0", DropFood_edge__edge0_AllowedTypes, DropFood_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition DropFood_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAnt", "DropFood_node_curAnt", "hasFood"),
				new string[] { "DropFood_node_curAnt" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { DropFood_node_curAnt }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_DropFood = new GRGEN_LGSP.PatternGraph(
				"DropFood",
				"",
				null, "DropFood",
				false, false,
				new GRGEN_LGSP.PatternNode[] { DropFood_node_curAnt, DropFood_node_hill }, 
				new GRGEN_LGSP.PatternEdge[] { DropFood_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { DropFood_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				DropFood_isNodeHomomorphicGlobal,
				DropFood_isEdgeHomomorphicGlobal,
				DropFood_isNodeTotallyHomomorphic,
				DropFood_isEdgeTotallyHomomorphic
			);
			pat_DropFood.edgeToSourceNode.Add(DropFood_edge__edge0, DropFood_node_curAnt);
			pat_DropFood.edgeToTargetNode.Add(DropFood_edge__edge0, DropFood_node_hill);

			DropFood_node_curAnt.pointOfDefinition = null;
			DropFood_node_hill.pointOfDefinition = pat_DropFood;
			DropFood_edge__edge0.pointOfDefinition = pat_DropFood;

			patternGraph = pat_DropFood;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_DropFood curMatch = (Match_DropFood)_curMatch;
			GRGEN_LGSP.LGSPNode node_curAnt = curMatch._node_curAnt;
			GRGEN_LGSP.LGSPNode node_hill = curMatch._node_hill;
			GRGEN_MODEL.IAnt inode_curAnt = curMatch.node_curAnt;
			GRGEN_MODEL.IAntHill inode_hill = curMatch.node_hill;
			graph.SettingAddedNodeNames( DropFood_addedNodeNames );
			graph.SettingAddedEdgeNames( DropFood_addedEdgeNames );
			{ // eval_0
				bool tempvar_0 = (bool )false;
				graph.ChangingNodeAttribute(node_curAnt, GRGEN_MODEL.NodeType_Ant.AttributeType_hasFood, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_curAnt.@hasFood = tempvar_0;
				graph.ChangedNodeAttribute(node_curAnt, GRGEN_MODEL.NodeType_Ant.AttributeType_hasFood);
				int tempvar_1 = (int )(inode_hill.@food + 1);
				graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_1, null);
				inode_hill.@food = tempvar_1;
				graph.ChangedNodeAttribute(node_hill, GRGEN_MODEL.NodeType_GridNode.AttributeType_food);
			}
			return;
		}
		private static string[] DropFood_addedNodeNames = new string[] {  };
		private static string[] DropFood_addedEdgeNames = new string[] {  };

		static Rule_DropFood() {
		}

		public interface IMatch_DropFood : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; set; }
			GRGEN_MODEL.IAntHill node_hill { get; set; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_DropFood : GRGEN_LGSP.MatchListElement<Match_DropFood>, IMatch_DropFood
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } set { _node_curAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } set { _node_hill = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum DropFood_NodeNums { @curAnt, @hill, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)DropFood_NodeNums.@curAnt: return _node_curAnt;
				case (int)DropFood_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "curAnt": return _node_curAnt;
				case "hill": return _node_hill;
				default: return null;
				}
			}

			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum DropFood_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)DropFood_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum DropFood_VariableNums { END_OF_ENUM };
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

			public enum DropFood_SubNums { END_OF_ENUM };
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

			public enum DropFood_AltNums { END_OF_ENUM };
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

			public enum DropFood_IterNums { END_OF_ENUM };
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

			public enum DropFood_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_DropFood.instance.pat_DropFood; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_DropFood(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_DropFood nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_DropFood cur = this;
				while(cur != null) {
					Match_DropFood next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_DropFood that)
			{
				_node_curAnt = that._node_curAnt;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_DropFood(Match_DropFood that)
			{
				CopyMatchContent(that);
			}
			public Match_DropFood()
			{
			}

			public bool IsEqual(Match_DropFood that)
			{
				if(that==null) return false;
				if(_node_curAnt != that._node_curAnt) return false;
				if(_node_hill != that._node_hill) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_curAnt(List<IMatch_DropFood> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_DropFood match in matchList)
					resultList.Add(match.node_curAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntHill> Extract_hill(List<IMatch_DropFood> matchList)
			{
				List<GRGEN_MODEL.IAntHill> resultList = new List<GRGEN_MODEL.IAntHill>(matchList.Count);
				foreach(IMatch_DropFood match in matchList)
					resultList.Add(match.node_hill);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntPosition> Extract__edge0(List<IMatch_DropFood> matchList)
			{
				List<GRGEN_MODEL.IAntPosition> resultList = new List<GRGEN_MODEL.IAntPosition>(matchList.Count);
				foreach(IMatch_DropFood match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_SearchAlongPheromones : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_SearchAlongPheromones instance = null;
		public static Rule_SearchAlongPheromones Instance { get { if (instance==null) { instance = new Rule_SearchAlongPheromones(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] SearchAlongPheromones_node_curAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] SearchAlongPheromones_node_old_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] SearchAlongPheromones_node_new_AllowedTypes = null;
		public static bool[] SearchAlongPheromones_node_curAnt_IsAllowedType = null;
		public static bool[] SearchAlongPheromones_node_old_IsAllowedType = null;
		public static bool[] SearchAlongPheromones_node_new_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] SearchAlongPheromones_edge_oldPos_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] SearchAlongPheromones_edge__edge0_AllowedTypes = null;
		public static bool[] SearchAlongPheromones_edge_oldPos_IsAllowedType = null;
		public static bool[] SearchAlongPheromones_edge__edge0_IsAllowedType = null;
		public enum SearchAlongPheromones_NodeNums { @curAnt, @old, @new, };
		public enum SearchAlongPheromones_EdgeNums { @oldPos, @_edge0, };
		public enum SearchAlongPheromones_VariableNums { };
		public enum SearchAlongPheromones_SubNums { };
		public enum SearchAlongPheromones_AltNums { };
		public enum SearchAlongPheromones_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_SearchAlongPheromones;


		private Rule_SearchAlongPheromones()
			: base("SearchAlongPheromones",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "SearchAlongPheromones_node_curAnt", },
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
			bool[,] SearchAlongPheromones_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] SearchAlongPheromones_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] SearchAlongPheromones_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] SearchAlongPheromones_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode SearchAlongPheromones_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "SearchAlongPheromones_node_curAnt", "curAnt", SearchAlongPheromones_node_curAnt_AllowedTypes, SearchAlongPheromones_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode SearchAlongPheromones_node_old = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "SearchAlongPheromones_node_old", "old", SearchAlongPheromones_node_old_AllowedTypes, SearchAlongPheromones_node_old_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode SearchAlongPheromones_node_new = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "SearchAlongPheromones_node_new", "new", SearchAlongPheromones_node_new_AllowedTypes, SearchAlongPheromones_node_new_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SearchAlongPheromones_edge_oldPos = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, GRGEN_MODEL.EdgeType_AntPosition.typeVar, "GRGEN_MODEL.IAntPosition", "SearchAlongPheromones_edge_oldPos", "oldPos", SearchAlongPheromones_edge_oldPos_AllowedTypes, SearchAlongPheromones_edge_oldPos_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SearchAlongPheromones_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, GRGEN_MODEL.EdgeType_PathToHill.typeVar, "GRGEN_MODEL.IPathToHill", "SearchAlongPheromones_edge__edge0", "_edge0", SearchAlongPheromones_edge__edge0_AllowedTypes, SearchAlongPheromones_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition SearchAlongPheromones_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IGridNode", "SearchAlongPheromones_node_new", "pheromones"), new GRGEN_EXPR.Constant("9")),
				new string[] { "SearchAlongPheromones_node_new" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { SearchAlongPheromones_node_new }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_SearchAlongPheromones = new GRGEN_LGSP.PatternGraph(
				"SearchAlongPheromones",
				"",
				null, "SearchAlongPheromones",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SearchAlongPheromones_node_curAnt, SearchAlongPheromones_node_old, SearchAlongPheromones_node_new }, 
				new GRGEN_LGSP.PatternEdge[] { SearchAlongPheromones_edge_oldPos, SearchAlongPheromones_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { SearchAlongPheromones_cond_0,  }, 
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
				SearchAlongPheromones_isNodeHomomorphicGlobal,
				SearchAlongPheromones_isEdgeHomomorphicGlobal,
				SearchAlongPheromones_isNodeTotallyHomomorphic,
				SearchAlongPheromones_isEdgeTotallyHomomorphic
			);
			pat_SearchAlongPheromones.edgeToSourceNode.Add(SearchAlongPheromones_edge_oldPos, SearchAlongPheromones_node_curAnt);
			pat_SearchAlongPheromones.edgeToTargetNode.Add(SearchAlongPheromones_edge_oldPos, SearchAlongPheromones_node_old);
			pat_SearchAlongPheromones.edgeToSourceNode.Add(SearchAlongPheromones_edge__edge0, SearchAlongPheromones_node_new);
			pat_SearchAlongPheromones.edgeToTargetNode.Add(SearchAlongPheromones_edge__edge0, SearchAlongPheromones_node_old);

			SearchAlongPheromones_node_curAnt.pointOfDefinition = null;
			SearchAlongPheromones_node_old.pointOfDefinition = pat_SearchAlongPheromones;
			SearchAlongPheromones_node_new.pointOfDefinition = pat_SearchAlongPheromones;
			SearchAlongPheromones_edge_oldPos.pointOfDefinition = pat_SearchAlongPheromones;
			SearchAlongPheromones_edge__edge0.pointOfDefinition = pat_SearchAlongPheromones;

			patternGraph = pat_SearchAlongPheromones;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_SearchAlongPheromones curMatch = (Match_SearchAlongPheromones)_curMatch;
			GRGEN_LGSP.LGSPNode node_curAnt = curMatch._node_curAnt;
			GRGEN_LGSP.LGSPNode node_new = curMatch._node_new;
			GRGEN_LGSP.LGSPEdge edge_oldPos = curMatch._edge_oldPos;
			graph.SettingAddedNodeNames( SearchAlongPheromones_addedNodeNames );
			graph.SettingAddedEdgeNames( SearchAlongPheromones_addedEdgeNames );
			GRGEN_MODEL.@AntPosition edge__edge1 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_curAnt, node_new);
			graph.Remove(edge_oldPos);
			return;
		}
		private static string[] SearchAlongPheromones_addedNodeNames = new string[] {  };
		private static string[] SearchAlongPheromones_addedEdgeNames = new string[] { "_edge1" };

		static Rule_SearchAlongPheromones() {
		}

		public interface IMatch_SearchAlongPheromones : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; set; }
			GRGEN_MODEL.IGridNode node_old { get; set; }
			GRGEN_MODEL.IGridNode node_new { get; set; }
			//Edges
			GRGEN_MODEL.IAntPosition edge_oldPos { get; set; }
			GRGEN_MODEL.IPathToHill edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SearchAlongPheromones : GRGEN_LGSP.MatchListElement<Match_SearchAlongPheromones>, IMatch_SearchAlongPheromones
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } set { _node_curAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_old { get { return (GRGEN_MODEL.IGridNode)_node_old; } set { _node_old = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_new { get { return (GRGEN_MODEL.IGridNode)_node_new; } set { _node_new = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_old;
			public GRGEN_LGSP.LGSPNode _node_new;
			public enum SearchAlongPheromones_NodeNums { @curAnt, @old, @new, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SearchAlongPheromones_NodeNums.@curAnt: return _node_curAnt;
				case (int)SearchAlongPheromones_NodeNums.@old: return _node_old;
				case (int)SearchAlongPheromones_NodeNums.@new: return _node_new;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "curAnt": return _node_curAnt;
				case "old": return _node_old;
				case "new": return _node_new;
				default: return null;
				}
			}

			public GRGEN_MODEL.IAntPosition edge_oldPos { get { return (GRGEN_MODEL.IAntPosition)_edge_oldPos; } set { _edge_oldPos = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_oldPos;
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum SearchAlongPheromones_EdgeNums { @oldPos, @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 2;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SearchAlongPheromones_EdgeNums.@oldPos: return _edge_oldPos;
				case (int)SearchAlongPheromones_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "oldPos": return _edge_oldPos;
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum SearchAlongPheromones_VariableNums { END_OF_ENUM };
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

			public enum SearchAlongPheromones_SubNums { END_OF_ENUM };
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

			public enum SearchAlongPheromones_AltNums { END_OF_ENUM };
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

			public enum SearchAlongPheromones_IterNums { END_OF_ENUM };
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

			public enum SearchAlongPheromones_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_SearchAlongPheromones.instance.pat_SearchAlongPheromones; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_SearchAlongPheromones(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_SearchAlongPheromones nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_SearchAlongPheromones cur = this;
				while(cur != null) {
					Match_SearchAlongPheromones next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_SearchAlongPheromones that)
			{
				_node_curAnt = that._node_curAnt;
				_node_old = that._node_old;
				_node_new = that._node_new;
				_edge_oldPos = that._edge_oldPos;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_SearchAlongPheromones(Match_SearchAlongPheromones that)
			{
				CopyMatchContent(that);
			}
			public Match_SearchAlongPheromones()
			{
			}

			public bool IsEqual(Match_SearchAlongPheromones that)
			{
				if(that==null) return false;
				if(_node_curAnt != that._node_curAnt) return false;
				if(_node_old != that._node_old) return false;
				if(_node_new != that._node_new) return false;
				if(_edge_oldPos != that._edge_oldPos) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_curAnt(List<IMatch_SearchAlongPheromones> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_SearchAlongPheromones match in matchList)
					resultList.Add(match.node_curAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_old(List<IMatch_SearchAlongPheromones> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_SearchAlongPheromones match in matchList)
					resultList.Add(match.node_old);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_new(List<IMatch_SearchAlongPheromones> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_SearchAlongPheromones match in matchList)
					resultList.Add(match.node_new);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntPosition> Extract_oldPos(List<IMatch_SearchAlongPheromones> matchList)
			{
				List<GRGEN_MODEL.IAntPosition> resultList = new List<GRGEN_MODEL.IAntPosition>(matchList.Count);
				foreach(IMatch_SearchAlongPheromones match in matchList)
					resultList.Add(match.edge_oldPos);
				return resultList;
			}
			public static List<GRGEN_MODEL.IPathToHill> Extract__edge0(List<IMatch_SearchAlongPheromones> matchList)
			{
				List<GRGEN_MODEL.IPathToHill> resultList = new List<GRGEN_MODEL.IPathToHill>(matchList.Count);
				foreach(IMatch_SearchAlongPheromones match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_SearchAimless : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_SearchAimless instance = null;
		public static Rule_SearchAimless Instance { get { if (instance==null) { instance = new Rule_SearchAimless(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] SearchAimless_node_curAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] SearchAimless_node_old_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] SearchAimless_node_new_AllowedTypes = { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridCornerNode.typeVar, };
		public static bool[] SearchAimless_node_curAnt_IsAllowedType = null;
		public static bool[] SearchAimless_node_old_IsAllowedType = null;
		public static bool[] SearchAimless_node_new_IsAllowedType = { false, true, true, false, false, };
		public static GRGEN_LIBGR.EdgeType[] SearchAimless_edge_oldPos_AllowedTypes = null;
		public static GRGEN_LIBGR.EdgeType[] SearchAimless_edge__edge0_AllowedTypes = null;
		public static bool[] SearchAimless_edge_oldPos_IsAllowedType = null;
		public static bool[] SearchAimless_edge__edge0_IsAllowedType = null;
		public enum SearchAimless_NodeNums { @curAnt, @old, @new, };
		public enum SearchAimless_EdgeNums { @oldPos, @_edge0, };
		public enum SearchAimless_VariableNums { };
		public enum SearchAimless_SubNums { };
		public enum SearchAimless_AltNums { };
		public enum SearchAimless_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_SearchAimless;


		private Rule_SearchAimless()
			: base("SearchAimless",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "SearchAimless_node_curAnt", },
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
			bool[,] SearchAimless_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] SearchAimless_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[] SearchAimless_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] SearchAimless_isEdgeTotallyHomomorphic = new bool[2] { false, false,  };
			GRGEN_LGSP.PatternNode SearchAimless_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "SearchAimless_node_curAnt", "curAnt", SearchAimless_node_curAnt_AllowedTypes, SearchAimless_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode SearchAimless_node_old = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "SearchAimless_node_old", "old", SearchAimless_node_old_AllowedTypes, SearchAimless_node_old_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode SearchAimless_node_new = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "SearchAimless_node_new", "new", SearchAimless_node_new_AllowedTypes, SearchAimless_node_new_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SearchAimless_edge_oldPos = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, GRGEN_MODEL.EdgeType_AntPosition.typeVar, "GRGEN_MODEL.IAntPosition", "SearchAimless_edge_oldPos", "oldPos", SearchAimless_edge_oldPos_AllowedTypes, SearchAimless_edge_oldPos_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge SearchAimless_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, GRGEN_MODEL.EdgeType_GridEdge.typeVar, "GRGEN_MODEL.IGridEdge", "SearchAimless_edge__edge0", "_edge0", SearchAimless_edge__edge0_AllowedTypes, SearchAimless_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_SearchAimless = new GRGEN_LGSP.PatternGraph(
				"SearchAimless",
				"",
				null, "SearchAimless",
				false, false,
				new GRGEN_LGSP.PatternNode[] { SearchAimless_node_curAnt, SearchAimless_node_old, SearchAimless_node_new }, 
				new GRGEN_LGSP.PatternEdge[] { SearchAimless_edge_oldPos, SearchAimless_edge__edge0 }, 
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
				SearchAimless_isNodeHomomorphicGlobal,
				SearchAimless_isEdgeHomomorphicGlobal,
				SearchAimless_isNodeTotallyHomomorphic,
				SearchAimless_isEdgeTotallyHomomorphic
			);
			pat_SearchAimless.edgeToSourceNode.Add(SearchAimless_edge_oldPos, SearchAimless_node_curAnt);
			pat_SearchAimless.edgeToTargetNode.Add(SearchAimless_edge_oldPos, SearchAimless_node_old);
			pat_SearchAimless.edgeToSourceNode.Add(SearchAimless_edge__edge0, SearchAimless_node_old);
			pat_SearchAimless.edgeToTargetNode.Add(SearchAimless_edge__edge0, SearchAimless_node_new);

			SearchAimless_node_curAnt.pointOfDefinition = null;
			SearchAimless_node_old.pointOfDefinition = pat_SearchAimless;
			SearchAimless_node_new.pointOfDefinition = pat_SearchAimless;
			SearchAimless_edge_oldPos.pointOfDefinition = pat_SearchAimless;
			SearchAimless_edge__edge0.pointOfDefinition = pat_SearchAimless;

			patternGraph = pat_SearchAimless;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_SearchAimless curMatch = (Match_SearchAimless)_curMatch;
			GRGEN_LGSP.LGSPNode node_curAnt = curMatch._node_curAnt;
			GRGEN_LGSP.LGSPNode node_new = curMatch._node_new;
			GRGEN_LGSP.LGSPEdge edge_oldPos = curMatch._edge_oldPos;
			graph.SettingAddedNodeNames( SearchAimless_addedNodeNames );
			graph.SettingAddedEdgeNames( SearchAimless_addedEdgeNames );
			GRGEN_MODEL.@AntPosition edge__edge1 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_curAnt, node_new);
			graph.Remove(edge_oldPos);
			return;
		}
		private static string[] SearchAimless_addedNodeNames = new string[] {  };
		private static string[] SearchAimless_addedEdgeNames = new string[] { "_edge1" };

		static Rule_SearchAimless() {
		}

		public interface IMatch_SearchAimless : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; set; }
			GRGEN_MODEL.IGridNode node_old { get; set; }
			GRGEN_MODEL.IGridNode node_new { get; set; }
			//Edges
			GRGEN_MODEL.IAntPosition edge_oldPos { get; set; }
			GRGEN_MODEL.IGridEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SearchAimless : GRGEN_LGSP.MatchListElement<Match_SearchAimless>, IMatch_SearchAimless
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } set { _node_curAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_old { get { return (GRGEN_MODEL.IGridNode)_node_old; } set { _node_old = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_new { get { return (GRGEN_MODEL.IGridNode)_node_new; } set { _node_new = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_old;
			public GRGEN_LGSP.LGSPNode _node_new;
			public enum SearchAimless_NodeNums { @curAnt, @old, @new, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SearchAimless_NodeNums.@curAnt: return _node_curAnt;
				case (int)SearchAimless_NodeNums.@old: return _node_old;
				case (int)SearchAimless_NodeNums.@new: return _node_new;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "curAnt": return _node_curAnt;
				case "old": return _node_old;
				case "new": return _node_new;
				default: return null;
				}
			}

			public GRGEN_MODEL.IAntPosition edge_oldPos { get { return (GRGEN_MODEL.IAntPosition)_edge_oldPos; } set { _edge_oldPos = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge_oldPos;
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum SearchAimless_EdgeNums { @oldPos, @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 2;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SearchAimless_EdgeNums.@oldPos: return _edge_oldPos;
				case (int)SearchAimless_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "oldPos": return _edge_oldPos;
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum SearchAimless_VariableNums { END_OF_ENUM };
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

			public enum SearchAimless_SubNums { END_OF_ENUM };
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

			public enum SearchAimless_AltNums { END_OF_ENUM };
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

			public enum SearchAimless_IterNums { END_OF_ENUM };
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

			public enum SearchAimless_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_SearchAimless.instance.pat_SearchAimless; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_SearchAimless(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_SearchAimless nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_SearchAimless cur = this;
				while(cur != null) {
					Match_SearchAimless next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_SearchAimless that)
			{
				_node_curAnt = that._node_curAnt;
				_node_old = that._node_old;
				_node_new = that._node_new;
				_edge_oldPos = that._edge_oldPos;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_SearchAimless(Match_SearchAimless that)
			{
				CopyMatchContent(that);
			}
			public Match_SearchAimless()
			{
			}

			public bool IsEqual(Match_SearchAimless that)
			{
				if(that==null) return false;
				if(_node_curAnt != that._node_curAnt) return false;
				if(_node_old != that._node_old) return false;
				if(_node_new != that._node_new) return false;
				if(_edge_oldPos != that._edge_oldPos) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_curAnt(List<IMatch_SearchAimless> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_SearchAimless match in matchList)
					resultList.Add(match.node_curAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_old(List<IMatch_SearchAimless> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_SearchAimless match in matchList)
					resultList.Add(match.node_old);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_new(List<IMatch_SearchAimless> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_SearchAimless match in matchList)
					resultList.Add(match.node_new);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntPosition> Extract_oldPos(List<IMatch_SearchAimless> matchList)
			{
				List<GRGEN_MODEL.IAntPosition> resultList = new List<GRGEN_MODEL.IAntPosition>(matchList.Count);
				foreach(IMatch_SearchAimless match in matchList)
					resultList.Add(match.edge_oldPos);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridEdge> Extract__edge0(List<IMatch_SearchAimless> matchList)
			{
				List<GRGEN_MODEL.IGridEdge> resultList = new List<GRGEN_MODEL.IGridEdge>(matchList.Count);
				foreach(IMatch_SearchAimless match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_ReachedEndOfWorld : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ReachedEndOfWorld instance = null;
		public static Rule_ReachedEndOfWorld Instance { get { if (instance==null) { instance = new Rule_ReachedEndOfWorld(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ReachedEndOfWorld_node_curAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ReachedEndOfWorld_node_n_AllowedTypes = { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridCornerNode.typeVar, };
		public static bool[] ReachedEndOfWorld_node_curAnt_IsAllowedType = null;
		public static bool[] ReachedEndOfWorld_node_n_IsAllowedType = { false, true, true, false, false, };
		public static GRGEN_LIBGR.EdgeType[] ReachedEndOfWorld_edge__edge0_AllowedTypes = null;
		public static bool[] ReachedEndOfWorld_edge__edge0_IsAllowedType = null;
		public enum ReachedEndOfWorld_NodeNums { @curAnt, @n, };
		public enum ReachedEndOfWorld_EdgeNums { @_edge0, };
		public enum ReachedEndOfWorld_VariableNums { };
		public enum ReachedEndOfWorld_SubNums { };
		public enum ReachedEndOfWorld_AltNums { };
		public enum ReachedEndOfWorld_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ReachedEndOfWorld;

		public static GRGEN_LIBGR.EdgeType[] ReachedEndOfWorld_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ReachedEndOfWorld_neg_0_edge__edge0_IsAllowedType = null;
		public enum ReachedEndOfWorld_neg_0_NodeNums { @n, };
		public enum ReachedEndOfWorld_neg_0_EdgeNums { @_edge0, };
		public enum ReachedEndOfWorld_neg_0_VariableNums { };
		public enum ReachedEndOfWorld_neg_0_SubNums { };
		public enum ReachedEndOfWorld_neg_0_AltNums { };
		public enum ReachedEndOfWorld_neg_0_IterNums { };


		public GRGEN_LGSP.PatternGraph ReachedEndOfWorld_neg_0;


		private Rule_ReachedEndOfWorld()
			: base("ReachedEndOfWorld",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "ReachedEndOfWorld_node_curAnt", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, },
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
			bool[,] ReachedEndOfWorld_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReachedEndOfWorld_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ReachedEndOfWorld_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ReachedEndOfWorld_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ReachedEndOfWorld_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "ReachedEndOfWorld_node_curAnt", "curAnt", ReachedEndOfWorld_node_curAnt_AllowedTypes, ReachedEndOfWorld_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode ReachedEndOfWorld_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "ReachedEndOfWorld_node_n", "n", ReachedEndOfWorld_node_n_AllowedTypes, ReachedEndOfWorld_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge ReachedEndOfWorld_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, GRGEN_MODEL.EdgeType_AntPosition.typeVar, "GRGEN_MODEL.IAntPosition", "ReachedEndOfWorld_edge__edge0", "_edge0", ReachedEndOfWorld_edge__edge0_AllowedTypes, ReachedEndOfWorld_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] ReachedEndOfWorld_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ReachedEndOfWorld_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ReachedEndOfWorld_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ReachedEndOfWorld_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ReachedEndOfWorld_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, GRGEN_MODEL.EdgeType_PathToHill.typeVar, "GRGEN_MODEL.IPathToHill", "ReachedEndOfWorld_neg_0_edge__edge0", "_edge0", ReachedEndOfWorld_neg_0_edge__edge0_AllowedTypes, ReachedEndOfWorld_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			ReachedEndOfWorld_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ReachedEndOfWorld_",
				null, "neg_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReachedEndOfWorld_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { ReachedEndOfWorld_neg_0_edge__edge0 }, 
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
				ReachedEndOfWorld_neg_0_isNodeHomomorphicGlobal,
				ReachedEndOfWorld_neg_0_isEdgeHomomorphicGlobal,
				ReachedEndOfWorld_neg_0_isNodeTotallyHomomorphic,
				ReachedEndOfWorld_neg_0_isEdgeTotallyHomomorphic
			);
			ReachedEndOfWorld_neg_0.edgeToTargetNode.Add(ReachedEndOfWorld_neg_0_edge__edge0, ReachedEndOfWorld_node_n);

			pat_ReachedEndOfWorld = new GRGEN_LGSP.PatternGraph(
				"ReachedEndOfWorld",
				"",
				null, "ReachedEndOfWorld",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReachedEndOfWorld_node_curAnt, ReachedEndOfWorld_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { ReachedEndOfWorld_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ReachedEndOfWorld_neg_0,  }, 
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
				ReachedEndOfWorld_isNodeHomomorphicGlobal,
				ReachedEndOfWorld_isEdgeHomomorphicGlobal,
				ReachedEndOfWorld_isNodeTotallyHomomorphic,
				ReachedEndOfWorld_isEdgeTotallyHomomorphic
			);
			pat_ReachedEndOfWorld.edgeToSourceNode.Add(ReachedEndOfWorld_edge__edge0, ReachedEndOfWorld_node_curAnt);
			pat_ReachedEndOfWorld.edgeToTargetNode.Add(ReachedEndOfWorld_edge__edge0, ReachedEndOfWorld_node_n);
			ReachedEndOfWorld_neg_0.embeddingGraph = pat_ReachedEndOfWorld;

			ReachedEndOfWorld_node_curAnt.pointOfDefinition = null;
			ReachedEndOfWorld_node_n.pointOfDefinition = pat_ReachedEndOfWorld;
			ReachedEndOfWorld_edge__edge0.pointOfDefinition = pat_ReachedEndOfWorld;
			ReachedEndOfWorld_neg_0_edge__edge0.pointOfDefinition = ReachedEndOfWorld_neg_0;

			patternGraph = pat_ReachedEndOfWorld;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IGridNode output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_ReachedEndOfWorld curMatch = (Match_ReachedEndOfWorld)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			output_0 = (GRGEN_MODEL.IGridNode)(node_n);
			return;
		}

		static Rule_ReachedEndOfWorld() {
		}

		public interface IMatch_ReachedEndOfWorld : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; set; }
			GRGEN_MODEL.IGridNode node_n { get; set; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReachedEndOfWorld_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_n { get; set; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ReachedEndOfWorld : GRGEN_LGSP.MatchListElement<Match_ReachedEndOfWorld>, IMatch_ReachedEndOfWorld
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } set { _node_curAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorld_NodeNums { @curAnt, @n, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_NodeNums.@curAnt: return _node_curAnt;
				case (int)ReachedEndOfWorld_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "curAnt": return _node_curAnt;
				case "n": return _node_n;
				default: return null;
				}
			}

			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorld_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum ReachedEndOfWorld_VariableNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_SubNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_AltNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_IterNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorld.instance.pat_ReachedEndOfWorld; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorld(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ReachedEndOfWorld nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ReachedEndOfWorld cur = this;
				while(cur != null) {
					Match_ReachedEndOfWorld next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_ReachedEndOfWorld that)
			{
				_node_curAnt = that._node_curAnt;
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ReachedEndOfWorld(Match_ReachedEndOfWorld that)
			{
				CopyMatchContent(that);
			}
			public Match_ReachedEndOfWorld()
			{
			}

			public bool IsEqual(Match_ReachedEndOfWorld that)
			{
				if(that==null) return false;
				if(_node_curAnt != that._node_curAnt) return false;
				if(_node_n != that._node_n) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_ReachedEndOfWorld_neg_0 : GRGEN_LGSP.MatchListElement<Match_ReachedEndOfWorld_neg_0>, IMatch_ReachedEndOfWorld_neg_0
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorld_neg_0_NodeNums { @n, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_neg_0_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				default: return null;
				}
			}

			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorld_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum ReachedEndOfWorld_neg_0_VariableNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_neg_0_SubNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_neg_0_AltNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_neg_0_IterNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorld_neg_0_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorld.instance.ReachedEndOfWorld_neg_0; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorld_neg_0(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ReachedEndOfWorld_neg_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ReachedEndOfWorld_neg_0 cur = this;
				while(cur != null) {
					Match_ReachedEndOfWorld_neg_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_ReachedEndOfWorld_neg_0 that)
			{
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ReachedEndOfWorld_neg_0(Match_ReachedEndOfWorld_neg_0 that)
			{
				CopyMatchContent(that);
			}
			public Match_ReachedEndOfWorld_neg_0()
			{
			}

			public bool IsEqual(Match_ReachedEndOfWorld_neg_0 that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_curAnt(List<IMatch_ReachedEndOfWorld> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_ReachedEndOfWorld match in matchList)
					resultList.Add(match.node_curAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_n(List<IMatch_ReachedEndOfWorld> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_ReachedEndOfWorld match in matchList)
					resultList.Add(match.node_n);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntPosition> Extract__edge0(List<IMatch_ReachedEndOfWorld> matchList)
			{
				List<GRGEN_MODEL.IAntPosition> resultList = new List<GRGEN_MODEL.IAntPosition>(matchList.Count);
				foreach(IMatch_ReachedEndOfWorld match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_ReachedEndOfWorldAnywhere : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_ReachedEndOfWorldAnywhere instance = null;
		public static Rule_ReachedEndOfWorldAnywhere Instance { get { if (instance==null) { instance = new Rule_ReachedEndOfWorldAnywhere(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] ReachedEndOfWorldAnywhere_node__node0_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] ReachedEndOfWorldAnywhere_node_n_AllowedTypes = { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridCornerNode.typeVar, };
		public static bool[] ReachedEndOfWorldAnywhere_node__node0_IsAllowedType = null;
		public static bool[] ReachedEndOfWorldAnywhere_node_n_IsAllowedType = { false, true, true, false, false, };
		public static GRGEN_LIBGR.EdgeType[] ReachedEndOfWorldAnywhere_edge__edge0_AllowedTypes = null;
		public static bool[] ReachedEndOfWorldAnywhere_edge__edge0_IsAllowedType = null;
		public enum ReachedEndOfWorldAnywhere_NodeNums { @_node0, @n, };
		public enum ReachedEndOfWorldAnywhere_EdgeNums { @_edge0, };
		public enum ReachedEndOfWorldAnywhere_VariableNums { };
		public enum ReachedEndOfWorldAnywhere_SubNums { };
		public enum ReachedEndOfWorldAnywhere_AltNums { };
		public enum ReachedEndOfWorldAnywhere_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_ReachedEndOfWorldAnywhere;

		public static GRGEN_LIBGR.EdgeType[] ReachedEndOfWorldAnywhere_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ReachedEndOfWorldAnywhere_neg_0_edge__edge0_IsAllowedType = null;
		public enum ReachedEndOfWorldAnywhere_neg_0_NodeNums { @n, };
		public enum ReachedEndOfWorldAnywhere_neg_0_EdgeNums { @_edge0, };
		public enum ReachedEndOfWorldAnywhere_neg_0_VariableNums { };
		public enum ReachedEndOfWorldAnywhere_neg_0_SubNums { };
		public enum ReachedEndOfWorldAnywhere_neg_0_AltNums { };
		public enum ReachedEndOfWorldAnywhere_neg_0_IterNums { };


		public GRGEN_LGSP.PatternGraph ReachedEndOfWorldAnywhere_neg_0;


		private Rule_ReachedEndOfWorldAnywhere()
			: base("ReachedEndOfWorldAnywhere",
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, },
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
			bool[,] ReachedEndOfWorldAnywhere_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReachedEndOfWorldAnywhere_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ReachedEndOfWorldAnywhere_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] ReachedEndOfWorldAnywhere_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode ReachedEndOfWorldAnywhere_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "ReachedEndOfWorldAnywhere_node__node0", "_node0", ReachedEndOfWorldAnywhere_node__node0_AllowedTypes, ReachedEndOfWorldAnywhere_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode ReachedEndOfWorldAnywhere_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "ReachedEndOfWorldAnywhere_node_n", "n", ReachedEndOfWorldAnywhere_node_n_AllowedTypes, ReachedEndOfWorldAnywhere_node_n_IsAllowedType, 1.0F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge ReachedEndOfWorldAnywhere_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, GRGEN_MODEL.EdgeType_AntPosition.typeVar, "GRGEN_MODEL.IAntPosition", "ReachedEndOfWorldAnywhere_edge__edge0", "_edge0", ReachedEndOfWorldAnywhere_edge__edge0_AllowedTypes, ReachedEndOfWorldAnywhere_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] ReachedEndOfWorldAnywhere_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ReachedEndOfWorldAnywhere_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ReachedEndOfWorldAnywhere_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ReachedEndOfWorldAnywhere_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ReachedEndOfWorldAnywhere_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, GRGEN_MODEL.EdgeType_PathToHill.typeVar, "GRGEN_MODEL.IPathToHill", "ReachedEndOfWorldAnywhere_neg_0_edge__edge0", "_edge0", ReachedEndOfWorldAnywhere_neg_0_edge__edge0_AllowedTypes, ReachedEndOfWorldAnywhere_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			ReachedEndOfWorldAnywhere_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ReachedEndOfWorldAnywhere_",
				null, "neg_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReachedEndOfWorldAnywhere_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { ReachedEndOfWorldAnywhere_neg_0_edge__edge0 }, 
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
				ReachedEndOfWorldAnywhere_neg_0_isNodeHomomorphicGlobal,
				ReachedEndOfWorldAnywhere_neg_0_isEdgeHomomorphicGlobal,
				ReachedEndOfWorldAnywhere_neg_0_isNodeTotallyHomomorphic,
				ReachedEndOfWorldAnywhere_neg_0_isEdgeTotallyHomomorphic
			);
			ReachedEndOfWorldAnywhere_neg_0.edgeToTargetNode.Add(ReachedEndOfWorldAnywhere_neg_0_edge__edge0, ReachedEndOfWorldAnywhere_node_n);

			pat_ReachedEndOfWorldAnywhere = new GRGEN_LGSP.PatternGraph(
				"ReachedEndOfWorldAnywhere",
				"",
				null, "ReachedEndOfWorldAnywhere",
				false, false,
				new GRGEN_LGSP.PatternNode[] { ReachedEndOfWorldAnywhere_node__node0, ReachedEndOfWorldAnywhere_node_n }, 
				new GRGEN_LGSP.PatternEdge[] { ReachedEndOfWorldAnywhere_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { ReachedEndOfWorldAnywhere_neg_0,  }, 
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
				ReachedEndOfWorldAnywhere_isNodeHomomorphicGlobal,
				ReachedEndOfWorldAnywhere_isEdgeHomomorphicGlobal,
				ReachedEndOfWorldAnywhere_isNodeTotallyHomomorphic,
				ReachedEndOfWorldAnywhere_isEdgeTotallyHomomorphic
			);
			pat_ReachedEndOfWorldAnywhere.edgeToSourceNode.Add(ReachedEndOfWorldAnywhere_edge__edge0, ReachedEndOfWorldAnywhere_node__node0);
			pat_ReachedEndOfWorldAnywhere.edgeToTargetNode.Add(ReachedEndOfWorldAnywhere_edge__edge0, ReachedEndOfWorldAnywhere_node_n);
			ReachedEndOfWorldAnywhere_neg_0.embeddingGraph = pat_ReachedEndOfWorldAnywhere;

			ReachedEndOfWorldAnywhere_node__node0.pointOfDefinition = pat_ReachedEndOfWorldAnywhere;
			ReachedEndOfWorldAnywhere_node_n.pointOfDefinition = pat_ReachedEndOfWorldAnywhere;
			ReachedEndOfWorldAnywhere_node_n.annotations.annotations.Add("prio", "10000");
			ReachedEndOfWorldAnywhere_edge__edge0.pointOfDefinition = pat_ReachedEndOfWorldAnywhere;
			ReachedEndOfWorldAnywhere_neg_0_edge__edge0.pointOfDefinition = ReachedEndOfWorldAnywhere_neg_0;

			patternGraph = pat_ReachedEndOfWorldAnywhere;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IGridNode output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_ReachedEndOfWorldAnywhere curMatch = (Match_ReachedEndOfWorldAnywhere)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			output_0 = (GRGEN_MODEL.IGridNode)(node_n);
			return;
		}

		static Rule_ReachedEndOfWorldAnywhere() {
		}

		public interface IMatch_ReachedEndOfWorldAnywhere : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node__node0 { get; set; }
			GRGEN_MODEL.IGridNode node_n { get; set; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_ReachedEndOfWorldAnywhere_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_n { get; set; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ReachedEndOfWorldAnywhere : GRGEN_LGSP.MatchListElement<Match_ReachedEndOfWorldAnywhere>, IMatch_ReachedEndOfWorldAnywhere
		{
			public GRGEN_MODEL.IAnt node__node0 { get { return (GRGEN_MODEL.IAnt)_node__node0; } set { _node__node0 = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorldAnywhere_NodeNums { @_node0, @n, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_NodeNums.@_node0: return _node__node0;
				case (int)ReachedEndOfWorldAnywhere_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "_node0": return _node__node0;
				case "n": return _node_n;
				default: return null;
				}
			}

			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorldAnywhere_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum ReachedEndOfWorldAnywhere_VariableNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_SubNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_AltNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_IterNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorldAnywhere.instance.pat_ReachedEndOfWorldAnywhere; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorldAnywhere(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ReachedEndOfWorldAnywhere nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ReachedEndOfWorldAnywhere cur = this;
				while(cur != null) {
					Match_ReachedEndOfWorldAnywhere next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_ReachedEndOfWorldAnywhere that)
			{
				_node__node0 = that._node__node0;
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ReachedEndOfWorldAnywhere(Match_ReachedEndOfWorldAnywhere that)
			{
				CopyMatchContent(that);
			}
			public Match_ReachedEndOfWorldAnywhere()
			{
			}

			public bool IsEqual(Match_ReachedEndOfWorldAnywhere that)
			{
				if(that==null) return false;
				if(_node__node0 != that._node__node0) return false;
				if(_node_n != that._node_n) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_ReachedEndOfWorldAnywhere_neg_0 : GRGEN_LGSP.MatchListElement<Match_ReachedEndOfWorldAnywhere_neg_0>, IMatch_ReachedEndOfWorldAnywhere_neg_0
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorldAnywhere_neg_0_NodeNums { @n, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_neg_0_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				default: return null;
				}
			}

			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorldAnywhere_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum ReachedEndOfWorldAnywhere_neg_0_VariableNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_neg_0_SubNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_neg_0_AltNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_neg_0_IterNums { END_OF_ENUM };
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

			public enum ReachedEndOfWorldAnywhere_neg_0_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorldAnywhere.instance.ReachedEndOfWorldAnywhere_neg_0; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorldAnywhere_neg_0(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_ReachedEndOfWorldAnywhere_neg_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_ReachedEndOfWorldAnywhere_neg_0 cur = this;
				while(cur != null) {
					Match_ReachedEndOfWorldAnywhere_neg_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_ReachedEndOfWorldAnywhere_neg_0 that)
			{
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_ReachedEndOfWorldAnywhere_neg_0(Match_ReachedEndOfWorldAnywhere_neg_0 that)
			{
				CopyMatchContent(that);
			}
			public Match_ReachedEndOfWorldAnywhere_neg_0()
			{
			}

			public bool IsEqual(Match_ReachedEndOfWorldAnywhere_neg_0 that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract__node0(List<IMatch_ReachedEndOfWorldAnywhere> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_ReachedEndOfWorldAnywhere match in matchList)
					resultList.Add(match.node__node0);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_n(List<IMatch_ReachedEndOfWorldAnywhere> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_ReachedEndOfWorldAnywhere match in matchList)
					resultList.Add(match.node_n);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntPosition> Extract__edge0(List<IMatch_ReachedEndOfWorldAnywhere> matchList)
			{
				List<GRGEN_MODEL.IAntPosition> resultList = new List<GRGEN_MODEL.IAntPosition>(matchList.Count);
				foreach(IMatch_ReachedEndOfWorldAnywhere match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_GrowFoodIfEqual : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GrowFoodIfEqual instance = null;
		public static Rule_GrowFoodIfEqual Instance { get { if (instance==null) { instance = new Rule_GrowFoodIfEqual(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GrowFoodIfEqual_node_n_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowFoodIfEqual_node_hill_AllowedTypes = null;
		public static bool[] GrowFoodIfEqual_node_n_IsAllowedType = null;
		public static bool[] GrowFoodIfEqual_node_hill_IsAllowedType = null;
		public enum GrowFoodIfEqual_NodeNums { @n, @hill, };
		public enum GrowFoodIfEqual_EdgeNums { };
		public enum GrowFoodIfEqual_VariableNums { @val, };
		public enum GrowFoodIfEqual_SubNums { };
		public enum GrowFoodIfEqual_AltNums { };
		public enum GrowFoodIfEqual_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_GrowFoodIfEqual;


		private Rule_GrowFoodIfEqual()
			: base("GrowFoodIfEqual",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_LIBGR.VarType.GetVarType(typeof(int)), },
				new string[] { "GrowFoodIfEqual_node_n", "GrowFoodIfEqual_var_val", },
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
			bool[,] GrowFoodIfEqual_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] GrowFoodIfEqual_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] GrowFoodIfEqual_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] GrowFoodIfEqual_isEdgeTotallyHomomorphic = new bool[0];
			GRGEN_LGSP.PatternVariable GrowFoodIfEqual_var_val = new GRGEN_LGSP.PatternVariable(GRGEN_LIBGR.VarType.GetVarType(typeof(int)), "GrowFoodIfEqual_var_val", "val", 1, false, null);
			GRGEN_LGSP.PatternNode GrowFoodIfEqual_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowFoodIfEqual_node_n", "n", GrowFoodIfEqual_node_n_AllowedTypes, GrowFoodIfEqual_node_n_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowFoodIfEqual_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, GRGEN_MODEL.NodeType_AntHill.typeVar, "GRGEN_MODEL.IAntHill", "GrowFoodIfEqual_node_hill", "hill", GrowFoodIfEqual_node_hill_AllowedTypes, GrowFoodIfEqual_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition GrowFoodIfEqual_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAntHill", "GrowFoodIfEqual_node_hill", "foodCountdown"), new GRGEN_EXPR.VariableExpression("GrowFoodIfEqual_var_val")),
				new string[] { "GrowFoodIfEqual_node_hill" }, new string[] {  }, new string[] { "GrowFoodIfEqual_var_val" },
				new GRGEN_LGSP.PatternNode[] { GrowFoodIfEqual_node_hill }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] { GrowFoodIfEqual_var_val });
			pat_GrowFoodIfEqual = new GRGEN_LGSP.PatternGraph(
				"GrowFoodIfEqual",
				"",
				null, "GrowFoodIfEqual",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowFoodIfEqual_node_n, GrowFoodIfEqual_node_hill }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] { GrowFoodIfEqual_var_val }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { GrowFoodIfEqual_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				GrowFoodIfEqual_isNodeHomomorphicGlobal,
				GrowFoodIfEqual_isEdgeHomomorphicGlobal,
				GrowFoodIfEqual_isNodeTotallyHomomorphic,
				GrowFoodIfEqual_isEdgeTotallyHomomorphic
			);

			GrowFoodIfEqual_var_val.pointOfDefinition = null;
			GrowFoodIfEqual_node_n.pointOfDefinition = null;
			GrowFoodIfEqual_node_hill.pointOfDefinition = pat_GrowFoodIfEqual;

			patternGraph = pat_GrowFoodIfEqual;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GrowFoodIfEqual curMatch = (Match_GrowFoodIfEqual)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			GRGEN_LGSP.LGSPNode node_hill = curMatch._node_hill;
			GRGEN_MODEL.IGridNode inode_n = curMatch.node_n;
			GRGEN_MODEL.IAntHill inode_hill = curMatch.node_hill;
			graph.SettingAddedNodeNames( GrowFoodIfEqual_addedNodeNames );
			graph.SettingAddedEdgeNames( GrowFoodIfEqual_addedEdgeNames );
			{ // eval_0
				int tempvar_0 = (int )(inode_n.@food + 100);
				graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_n.@food = tempvar_0;
				graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_food);
				int tempvar_1 = (int )(inode_hill.@foodCountdown + 10);
				graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_1, null);
				inode_hill.@foodCountdown = tempvar_1;
				graph.ChangedNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown);
			}
			return;
		}
		private static string[] GrowFoodIfEqual_addedNodeNames = new string[] {  };
		private static string[] GrowFoodIfEqual_addedEdgeNames = new string[] {  };

		static Rule_GrowFoodIfEqual() {
		}

		public interface IMatch_GrowFoodIfEqual : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_n { get; set; }
			GRGEN_MODEL.IAntHill node_hill { get; set; }
			//Edges
			//Variables
			int @var_val { get; set; }
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowFoodIfEqual : GRGEN_LGSP.MatchListElement<Match_GrowFoodIfEqual>, IMatch_GrowFoodIfEqual
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } set { _node_hill = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowFoodIfEqual_NodeNums { @n, @hill, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowFoodIfEqual_NodeNums.@n: return _node_n;
				case (int)GrowFoodIfEqual_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				case "hill": return _node_hill;
				default: return null;
				}
			}

			public enum GrowFoodIfEqual_EdgeNums { END_OF_ENUM };
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

			public int var_val { get { return _var_val; } set { _var_val = value; } }
			public int _var_val;
			public enum GrowFoodIfEqual_VariableNums { @val, END_OF_ENUM };
			public override IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public override IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public override int NumberOfVariables { get { return 1;} }
			public override object getVariableAt(int index)
			{
				switch(index) {
				case (int)GrowFoodIfEqual_VariableNums.@val: return _var_val;
				default: return null;
				}
			}
			public override object getVariable(string name)
			{
				switch(name) {
				case "val": return _var_val;
				default: return null;
				}
			}

			public enum GrowFoodIfEqual_SubNums { END_OF_ENUM };
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

			public enum GrowFoodIfEqual_AltNums { END_OF_ENUM };
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

			public enum GrowFoodIfEqual_IterNums { END_OF_ENUM };
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

			public enum GrowFoodIfEqual_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowFoodIfEqual.instance.pat_GrowFoodIfEqual; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowFoodIfEqual(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowFoodIfEqual nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowFoodIfEqual cur = this;
				while(cur != null) {
					Match_GrowFoodIfEqual next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowFoodIfEqual that)
			{
				_node_n = that._node_n;
				_node_hill = that._node_hill;
				_var_val = that._var_val;
			}

			public Match_GrowFoodIfEqual(Match_GrowFoodIfEqual that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowFoodIfEqual()
			{
			}

			public bool IsEqual(Match_GrowFoodIfEqual that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				if(_node_hill != that._node_hill) return false;
				if(_var_val != that._var_val) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IGridNode> Extract_n(List<IMatch_GrowFoodIfEqual> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowFoodIfEqual match in matchList)
					resultList.Add(match.node_n);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntHill> Extract_hill(List<IMatch_GrowFoodIfEqual> matchList)
			{
				List<GRGEN_MODEL.IAntHill> resultList = new List<GRGEN_MODEL.IAntHill>(matchList.Count);
				foreach(IMatch_GrowFoodIfEqual match in matchList)
					resultList.Add(match.node_hill);
				return resultList;
			}
			public static List<int> Extract_val(List<IMatch_GrowFoodIfEqual> matchList)
			{
				List<int> resultList = new List<int>(matchList.Count);
				foreach(IMatch_GrowFoodIfEqual match in matchList)
					resultList.Add(match.var_val);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
		public static List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> Array_GrowFoodIfEqual_orderAscendingBy_val(List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> list)
		{
			List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> newList = new List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>(list);
			newList.Sort(new Comparer_GrowFoodIfEqual_orderAscendingBy_val());
			return newList;
		}
		class Comparer_GrowFoodIfEqual_orderAscendingBy_val : Comparer<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>
		{
			public override int Compare(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual left, GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual right)
			{
				return left.var_val.CompareTo(right.var_val);
			}
		}
		public static List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> Array_GrowFoodIfEqual_orderDescendingBy_val(List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> list)
		{
			List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> newList = new List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>(list);
			newList.Sort(new Comparer_GrowFoodIfEqual_orderDescendingBy_val());
			return newList;
		}
		class Comparer_GrowFoodIfEqual_orderDescendingBy_val : Comparer<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>
		{
			public override int Compare(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual left, GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual right)
			{
				return -left.var_val.CompareTo(right.var_val);
			}
		}
		public static List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> Array_GrowFoodIfEqual_keepOneForEachBy_val(List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> list)
		{
			List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> newList = new List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>();
			Dictionary<int, GRGEN_LIBGR.SetValueType> alreadySeenMembers = new Dictionary<int, GRGEN_LIBGR.SetValueType>();
			foreach(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual element in list)
			{
				if(!alreadySeenMembers.ContainsKey(element.@var_val)) {
					newList.Add(element);
					alreadySeenMembers.Add(element.@var_val, null);
				}
			}
			return newList;
		}
	}

	public class Rule_GrowWorldFirstAtCorner : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GrowWorldFirstAtCorner instance = null;
		public static Rule_GrowWorldFirstAtCorner Instance { get { if (instance==null) { instance = new Rule_GrowWorldFirstAtCorner(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GrowWorldFirstAtCorner_node_cur_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldFirstAtCorner_node_next_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldFirstAtCorner_node_hill_AllowedTypes = null;
		public static bool[] GrowWorldFirstAtCorner_node_cur_IsAllowedType = null;
		public static bool[] GrowWorldFirstAtCorner_node_next_IsAllowedType = null;
		public static bool[] GrowWorldFirstAtCorner_node_hill_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] GrowWorldFirstAtCorner_edge__edge0_AllowedTypes = { GRGEN_MODEL.EdgeType_GridEdge.typeVar, };
		public static bool[] GrowWorldFirstAtCorner_edge__edge0_IsAllowedType = { false, false, false, true, false, false, false, };
		public enum GrowWorldFirstAtCorner_NodeNums { @cur, @next, @hill, };
		public enum GrowWorldFirstAtCorner_EdgeNums { @_edge0, };
		public enum GrowWorldFirstAtCorner_VariableNums { };
		public enum GrowWorldFirstAtCorner_SubNums { };
		public enum GrowWorldFirstAtCorner_AltNums { };
		public enum GrowWorldFirstAtCorner_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_GrowWorldFirstAtCorner;


		private Rule_GrowWorldFirstAtCorner()
			: base("GrowWorldFirstAtCorner",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, },
				new string[] { "GrowWorldFirstAtCorner_node_cur", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, },
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
			bool[,] GrowWorldFirstAtCorner_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] GrowWorldFirstAtCorner_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldFirstAtCorner_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] GrowWorldFirstAtCorner_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode GrowWorldFirstAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridCornerNode, GRGEN_MODEL.NodeType_GridCornerNode.typeVar, "GRGEN_MODEL.IGridCornerNode", "GrowWorldFirstAtCorner_node_cur", "cur", GrowWorldFirstAtCorner_node_cur_AllowedTypes, GrowWorldFirstAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldFirstAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldFirstAtCorner_node_next", "next", GrowWorldFirstAtCorner_node_next_AllowedTypes, GrowWorldFirstAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldFirstAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, GRGEN_MODEL.NodeType_AntHill.typeVar, "GRGEN_MODEL.IAntHill", "GrowWorldFirstAtCorner_node_hill", "hill", GrowWorldFirstAtCorner_node_hill_AllowedTypes, GrowWorldFirstAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GrowWorldFirstAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, GRGEN_MODEL.EdgeType_GridEdge.typeVar, "GRGEN_MODEL.IGridEdge", "GrowWorldFirstAtCorner_edge__edge0", "_edge0", GrowWorldFirstAtCorner_edge__edge0_AllowedTypes, GrowWorldFirstAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_GrowWorldFirstAtCorner = new GRGEN_LGSP.PatternGraph(
				"GrowWorldFirstAtCorner",
				"",
				null, "GrowWorldFirstAtCorner",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowWorldFirstAtCorner_node_cur, GrowWorldFirstAtCorner_node_next, GrowWorldFirstAtCorner_node_hill }, 
				new GRGEN_LGSP.PatternEdge[] { GrowWorldFirstAtCorner_edge__edge0 }, 
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
				new bool[1, 1] {
					{ true, },
				},
				GrowWorldFirstAtCorner_isNodeHomomorphicGlobal,
				GrowWorldFirstAtCorner_isEdgeHomomorphicGlobal,
				GrowWorldFirstAtCorner_isNodeTotallyHomomorphic,
				GrowWorldFirstAtCorner_isEdgeTotallyHomomorphic
			);
			pat_GrowWorldFirstAtCorner.edgeToSourceNode.Add(GrowWorldFirstAtCorner_edge__edge0, GrowWorldFirstAtCorner_node_cur);
			pat_GrowWorldFirstAtCorner.edgeToTargetNode.Add(GrowWorldFirstAtCorner_edge__edge0, GrowWorldFirstAtCorner_node_next);

			GrowWorldFirstAtCorner_node_cur.pointOfDefinition = null;
			GrowWorldFirstAtCorner_node_next.pointOfDefinition = pat_GrowWorldFirstAtCorner;
			GrowWorldFirstAtCorner_node_hill.pointOfDefinition = pat_GrowWorldFirstAtCorner;
			GrowWorldFirstAtCorner_edge__edge0.pointOfDefinition = pat_GrowWorldFirstAtCorner;

			patternGraph = pat_GrowWorldFirstAtCorner;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GrowWorldFirstAtCorner curMatch = (Match_GrowWorldFirstAtCorner)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_hill = curMatch._node_hill;
			GRGEN_LGSP.LGSPNode node_cur = curMatch._node_cur;
			GRGEN_MODEL.IAntHill inode_hill = curMatch.node_hill;
			graph.SettingAddedNodeNames( GrowWorldFirstAtCorner_addedNodeNames );
			GRGEN_MODEL.@GridNode node_outer1 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_outer2 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_outer3 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			graph.SettingAddedEdgeNames( GrowWorldFirstAtCorner_addedEdgeNames );
			GRGEN_MODEL.@PathToHill edge__edge1 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer1, node_cur);
			GRGEN_MODEL.@PathToHill edge__edge2 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer2, node_cur);
			GRGEN_MODEL.@PathToHill edge__edge3 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer3, node_cur);
			GRGEN_MODEL.@GridEdge edge__edge4 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_outer1, node_outer2);
			GRGEN_MODEL.@GridEdge edge__edge5 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_outer2, node_outer3);
			{ // eval_0
				int tempvar_0 = (int )(inode_hill.@foodCountdown - 3);
				graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_hill.@foodCountdown = tempvar_0;
				graph.ChangedNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown);
			}
			GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;
			ApplyXGRS_GrowWorldFirstAtCorner_0(procEnv, (GRGEN_MODEL.IGridNode)node_outer1, (GRGEN_MODEL.IGridCornerNode)node_outer2, (GRGEN_MODEL.IGridNode)node_outer3);
			output_0 = (GRGEN_MODEL.IGridNode)(node_next);
			output_1 = (GRGEN_MODEL.IGridNode)(node_outer3);
			return;
		}
		private static string[] GrowWorldFirstAtCorner_addedNodeNames = new string[] { "outer1", "outer2", "outer3" };
		private static string[] GrowWorldFirstAtCorner_addedEdgeNames = new string[] { "_edge1", "_edge2", "_edge3", "_edge4", "_edge5" };

        public static bool ApplyXGRS_GrowWorldFirstAtCorner_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IGridNode var_outer1, GRGEN_MODEL.IGridCornerNode var_outer2, GRGEN_MODEL.IGridNode var_outer3)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            procEnv.DebugEntering("GrowWorldFirstAtCorner.exec_0", "GrowFoodIfEqual(outer1,-2) || GrowFoodIfEqual(outer2,-1) || GrowFoodIfEqual(outer3,0)");
            bool res_10;
            bool res_6;
            bool res_2;
            GRGEN_ACTIONS.Action_GrowFoodIfEqual rule_GrowFoodIfEqual = GRGEN_ACTIONS.Action_GrowFoodIfEqual.Instance;
            bool res_5;
            bool res_9;
            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_2 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer1, (int)-2);
            procEnv.PerformanceInfo.MatchesFound += matches_2.Count;
            if(matches_2.Count == 0) {
                res_2 = (bool)(false);
            } else {
                res_2 = (bool)(true);
                procEnv.Matched(matches_2, null, false);
                procEnv.Finishing(matches_2, false);
                GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_2 = matches_2.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_2);
                procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_2, false);
            }
            if(res_2)
                res_6 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_5 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer2, (int)-1);
                procEnv.PerformanceInfo.MatchesFound += matches_5.Count;
                if(matches_5.Count == 0) {
                    res_5 = (bool)(false);
                } else {
                    res_5 = (bool)(true);
                    procEnv.Matched(matches_5, null, false);
                    procEnv.Finishing(matches_5, false);
                    GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_5 = matches_5.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_5);
                    procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_5, false);
                }
                res_6 = (bool)(res_5);
            }
            if(res_6)
                res_10 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_9 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer3, (int)0);
                procEnv.PerformanceInfo.MatchesFound += matches_9.Count;
                if(matches_9.Count == 0) {
                    res_9 = (bool)(false);
                } else {
                    res_9 = (bool)(true);
                    procEnv.Matched(matches_9, null, false);
                    procEnv.Finishing(matches_9, false);
                    GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_9 = matches_9.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_9);
                    procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_9, false);
                }
                res_10 = (bool)(res_9);
            }
            procEnv.DebugExiting("GrowWorldFirstAtCorner.exec_0");
            return res_10;
        }

		static Rule_GrowWorldFirstAtCorner() {
		}

		public interface IMatch_GrowWorldFirstAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridCornerNode node_cur { get; set; }
			GRGEN_MODEL.IGridNode node_next { get; set; }
			GRGEN_MODEL.IAntHill node_hill { get; set; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldFirstAtCorner : GRGEN_LGSP.MatchListElement<Match_GrowWorldFirstAtCorner>, IMatch_GrowWorldFirstAtCorner
		{
			public GRGEN_MODEL.IGridCornerNode node_cur { get { return (GRGEN_MODEL.IGridCornerNode)_node_cur; } set { _node_cur = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } set { _node_next = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } set { _node_hill = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldFirstAtCorner_NodeNums { @cur, @next, @hill, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldFirstAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldFirstAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "cur": return _node_cur;
				case "next": return _node_next;
				case "hill": return _node_hill;
				default: return null;
				}
			}

			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldFirstAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GrowWorldFirstAtCorner_VariableNums { END_OF_ENUM };
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

			public enum GrowWorldFirstAtCorner_SubNums { END_OF_ENUM };
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

			public enum GrowWorldFirstAtCorner_AltNums { END_OF_ENUM };
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

			public enum GrowWorldFirstAtCorner_IterNums { END_OF_ENUM };
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

			public enum GrowWorldFirstAtCorner_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldFirstAtCorner.instance.pat_GrowWorldFirstAtCorner; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldFirstAtCorner(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowWorldFirstAtCorner nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowWorldFirstAtCorner cur = this;
				while(cur != null) {
					Match_GrowWorldFirstAtCorner next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowWorldFirstAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GrowWorldFirstAtCorner(Match_GrowWorldFirstAtCorner that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowWorldFirstAtCorner()
			{
			}

			public bool IsEqual(Match_GrowWorldFirstAtCorner that)
			{
				if(that==null) return false;
				if(_node_cur != that._node_cur) return false;
				if(_node_next != that._node_next) return false;
				if(_node_hill != that._node_hill) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IGridCornerNode> Extract_cur(List<IMatch_GrowWorldFirstAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridCornerNode> resultList = new List<GRGEN_MODEL.IGridCornerNode>(matchList.Count);
				foreach(IMatch_GrowWorldFirstAtCorner match in matchList)
					resultList.Add(match.node_cur);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_next(List<IMatch_GrowWorldFirstAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldFirstAtCorner match in matchList)
					resultList.Add(match.node_next);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntHill> Extract_hill(List<IMatch_GrowWorldFirstAtCorner> matchList)
			{
				List<GRGEN_MODEL.IAntHill> resultList = new List<GRGEN_MODEL.IAntHill>(matchList.Count);
				foreach(IMatch_GrowWorldFirstAtCorner match in matchList)
					resultList.Add(match.node_hill);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridEdge> Extract__edge0(List<IMatch_GrowWorldFirstAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridEdge> resultList = new List<GRGEN_MODEL.IGridEdge>(matchList.Count);
				foreach(IMatch_GrowWorldFirstAtCorner match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_GrowWorldFirstNotAtCorner : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GrowWorldFirstNotAtCorner instance = null;
		public static Rule_GrowWorldFirstNotAtCorner Instance { get { if (instance==null) { instance = new Rule_GrowWorldFirstNotAtCorner(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GrowWorldFirstNotAtCorner_node_cur_AllowedTypes = { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_AntHill.typeVar, };
		public static GRGEN_LIBGR.NodeType[] GrowWorldFirstNotAtCorner_node_next_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldFirstNotAtCorner_node_hill_AllowedTypes = null;
		public static bool[] GrowWorldFirstNotAtCorner_node_cur_IsAllowedType = { false, true, false, true, false, };
		public static bool[] GrowWorldFirstNotAtCorner_node_next_IsAllowedType = null;
		public static bool[] GrowWorldFirstNotAtCorner_node_hill_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] GrowWorldFirstNotAtCorner_edge__edge0_AllowedTypes = { GRGEN_MODEL.EdgeType_GridEdge.typeVar, };
		public static bool[] GrowWorldFirstNotAtCorner_edge__edge0_IsAllowedType = { false, false, false, true, false, false, false, };
		public enum GrowWorldFirstNotAtCorner_NodeNums { @cur, @next, @hill, };
		public enum GrowWorldFirstNotAtCorner_EdgeNums { @_edge0, };
		public enum GrowWorldFirstNotAtCorner_VariableNums { };
		public enum GrowWorldFirstNotAtCorner_SubNums { };
		public enum GrowWorldFirstNotAtCorner_AltNums { };
		public enum GrowWorldFirstNotAtCorner_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_GrowWorldFirstNotAtCorner;


		private Rule_GrowWorldFirstNotAtCorner()
			: base("GrowWorldFirstNotAtCorner",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, },
				new string[] { "GrowWorldFirstNotAtCorner_node_cur", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, },
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
			bool[,] GrowWorldFirstNotAtCorner_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] GrowWorldFirstNotAtCorner_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldFirstNotAtCorner_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] GrowWorldFirstNotAtCorner_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode GrowWorldFirstNotAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldFirstNotAtCorner_node_cur", "cur", GrowWorldFirstNotAtCorner_node_cur_AllowedTypes, GrowWorldFirstNotAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldFirstNotAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldFirstNotAtCorner_node_next", "next", GrowWorldFirstNotAtCorner_node_next_AllowedTypes, GrowWorldFirstNotAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldFirstNotAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, GRGEN_MODEL.NodeType_AntHill.typeVar, "GRGEN_MODEL.IAntHill", "GrowWorldFirstNotAtCorner_node_hill", "hill", GrowWorldFirstNotAtCorner_node_hill_AllowedTypes, GrowWorldFirstNotAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GrowWorldFirstNotAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, GRGEN_MODEL.EdgeType_GridEdge.typeVar, "GRGEN_MODEL.IGridEdge", "GrowWorldFirstNotAtCorner_edge__edge0", "_edge0", GrowWorldFirstNotAtCorner_edge__edge0_AllowedTypes, GrowWorldFirstNotAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_GrowWorldFirstNotAtCorner = new GRGEN_LGSP.PatternGraph(
				"GrowWorldFirstNotAtCorner",
				"",
				null, "GrowWorldFirstNotAtCorner",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowWorldFirstNotAtCorner_node_cur, GrowWorldFirstNotAtCorner_node_next, GrowWorldFirstNotAtCorner_node_hill }, 
				new GRGEN_LGSP.PatternEdge[] { GrowWorldFirstNotAtCorner_edge__edge0 }, 
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
				new bool[1, 1] {
					{ true, },
				},
				GrowWorldFirstNotAtCorner_isNodeHomomorphicGlobal,
				GrowWorldFirstNotAtCorner_isEdgeHomomorphicGlobal,
				GrowWorldFirstNotAtCorner_isNodeTotallyHomomorphic,
				GrowWorldFirstNotAtCorner_isEdgeTotallyHomomorphic
			);
			pat_GrowWorldFirstNotAtCorner.edgeToSourceNode.Add(GrowWorldFirstNotAtCorner_edge__edge0, GrowWorldFirstNotAtCorner_node_cur);
			pat_GrowWorldFirstNotAtCorner.edgeToTargetNode.Add(GrowWorldFirstNotAtCorner_edge__edge0, GrowWorldFirstNotAtCorner_node_next);

			GrowWorldFirstNotAtCorner_node_cur.pointOfDefinition = null;
			GrowWorldFirstNotAtCorner_node_next.pointOfDefinition = pat_GrowWorldFirstNotAtCorner;
			GrowWorldFirstNotAtCorner_node_hill.pointOfDefinition = pat_GrowWorldFirstNotAtCorner;
			GrowWorldFirstNotAtCorner_edge__edge0.pointOfDefinition = pat_GrowWorldFirstNotAtCorner;

			patternGraph = pat_GrowWorldFirstNotAtCorner;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GrowWorldFirstNotAtCorner curMatch = (Match_GrowWorldFirstNotAtCorner)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_hill = curMatch._node_hill;
			GRGEN_LGSP.LGSPNode node_cur = curMatch._node_cur;
			GRGEN_MODEL.IAntHill inode_hill = curMatch.node_hill;
			graph.SettingAddedNodeNames( GrowWorldFirstNotAtCorner_addedNodeNames );
			GRGEN_MODEL.@GridNode node_outer = GRGEN_MODEL.@GridNode.CreateNode(graph);
			graph.SettingAddedEdgeNames( GrowWorldFirstNotAtCorner_addedEdgeNames );
			GRGEN_MODEL.@PathToHill edge__edge1 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer, node_cur);
			{ // eval_0
				int tempvar_0 = (int )(inode_hill.@foodCountdown - 1);
				graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_hill.@foodCountdown = tempvar_0;
				graph.ChangedNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown);
			}
			GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;
			ApplyXGRS_GrowWorldFirstNotAtCorner_0(procEnv, (GRGEN_MODEL.IGridNode)node_outer);
			output_0 = (GRGEN_MODEL.IGridNode)(node_next);
			output_1 = (GRGEN_MODEL.IGridNode)(node_outer);
			return;
		}
		private static string[] GrowWorldFirstNotAtCorner_addedNodeNames = new string[] { "outer" };
		private static string[] GrowWorldFirstNotAtCorner_addedEdgeNames = new string[] { "_edge1" };

        public static bool ApplyXGRS_GrowWorldFirstNotAtCorner_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IGridNode var_outer)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            procEnv.DebugEntering("GrowWorldFirstNotAtCorner.exec_0", "GrowFoodIfEqual(outer,0)");
            bool res_13;
            GRGEN_ACTIONS.Action_GrowFoodIfEqual rule_GrowFoodIfEqual = GRGEN_ACTIONS.Action_GrowFoodIfEqual.Instance;
            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_13 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer, (int)0);
            procEnv.PerformanceInfo.MatchesFound += matches_13.Count;
            if(matches_13.Count == 0) {
                res_13 = (bool)(false);
            } else {
                res_13 = (bool)(true);
                procEnv.Matched(matches_13, null, false);
                procEnv.Finishing(matches_13, false);
                GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_13 = matches_13.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_13);
                procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_13, false);
            }
            procEnv.DebugExiting("GrowWorldFirstNotAtCorner.exec_0");
            return res_13;
        }

		static Rule_GrowWorldFirstNotAtCorner() {
		}

		public interface IMatch_GrowWorldFirstNotAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_cur { get; set; }
			GRGEN_MODEL.IGridNode node_next { get; set; }
			GRGEN_MODEL.IAntHill node_hill { get; set; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldFirstNotAtCorner : GRGEN_LGSP.MatchListElement<Match_GrowWorldFirstNotAtCorner>, IMatch_GrowWorldFirstNotAtCorner
		{
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } set { _node_cur = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } set { _node_next = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } set { _node_hill = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldFirstNotAtCorner_NodeNums { @cur, @next, @hill, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstNotAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldFirstNotAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldFirstNotAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "cur": return _node_cur;
				case "next": return _node_next;
				case "hill": return _node_hill;
				default: return null;
				}
			}

			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldFirstNotAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstNotAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GrowWorldFirstNotAtCorner_VariableNums { END_OF_ENUM };
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

			public enum GrowWorldFirstNotAtCorner_SubNums { END_OF_ENUM };
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

			public enum GrowWorldFirstNotAtCorner_AltNums { END_OF_ENUM };
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

			public enum GrowWorldFirstNotAtCorner_IterNums { END_OF_ENUM };
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

			public enum GrowWorldFirstNotAtCorner_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldFirstNotAtCorner.instance.pat_GrowWorldFirstNotAtCorner; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldFirstNotAtCorner(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowWorldFirstNotAtCorner nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowWorldFirstNotAtCorner cur = this;
				while(cur != null) {
					Match_GrowWorldFirstNotAtCorner next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowWorldFirstNotAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GrowWorldFirstNotAtCorner(Match_GrowWorldFirstNotAtCorner that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowWorldFirstNotAtCorner()
			{
			}

			public bool IsEqual(Match_GrowWorldFirstNotAtCorner that)
			{
				if(that==null) return false;
				if(_node_cur != that._node_cur) return false;
				if(_node_next != that._node_next) return false;
				if(_node_hill != that._node_hill) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IGridNode> Extract_cur(List<IMatch_GrowWorldFirstNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldFirstNotAtCorner match in matchList)
					resultList.Add(match.node_cur);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_next(List<IMatch_GrowWorldFirstNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldFirstNotAtCorner match in matchList)
					resultList.Add(match.node_next);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntHill> Extract_hill(List<IMatch_GrowWorldFirstNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IAntHill> resultList = new List<GRGEN_MODEL.IAntHill>(matchList.Count);
				foreach(IMatch_GrowWorldFirstNotAtCorner match in matchList)
					resultList.Add(match.node_hill);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridEdge> Extract__edge0(List<IMatch_GrowWorldFirstNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridEdge> resultList = new List<GRGEN_MODEL.IGridEdge>(matchList.Count);
				foreach(IMatch_GrowWorldFirstNotAtCorner match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_GrowWorldNextAtCorner : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GrowWorldNextAtCorner instance = null;
		public static Rule_GrowWorldNextAtCorner Instance { get { if (instance==null) { instance = new Rule_GrowWorldNextAtCorner(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GrowWorldNextAtCorner_node_cur_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldNextAtCorner_node_next_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldNextAtCorner_node_curOuter_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldNextAtCorner_node_hill_AllowedTypes = null;
		public static bool[] GrowWorldNextAtCorner_node_cur_IsAllowedType = null;
		public static bool[] GrowWorldNextAtCorner_node_next_IsAllowedType = null;
		public static bool[] GrowWorldNextAtCorner_node_curOuter_IsAllowedType = null;
		public static bool[] GrowWorldNextAtCorner_node_hill_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] GrowWorldNextAtCorner_edge__edge0_AllowedTypes = { GRGEN_MODEL.EdgeType_GridEdge.typeVar, };
		public static bool[] GrowWorldNextAtCorner_edge__edge0_IsAllowedType = { false, false, false, true, false, false, false, };
		public enum GrowWorldNextAtCorner_NodeNums { @cur, @next, @curOuter, @hill, };
		public enum GrowWorldNextAtCorner_EdgeNums { @_edge0, };
		public enum GrowWorldNextAtCorner_VariableNums { };
		public enum GrowWorldNextAtCorner_SubNums { };
		public enum GrowWorldNextAtCorner_AltNums { };
		public enum GrowWorldNextAtCorner_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_GrowWorldNextAtCorner;

		public static GRGEN_LIBGR.EdgeType[] GrowWorldNextAtCorner_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] GrowWorldNextAtCorner_neg_0_edge__edge0_IsAllowedType = null;
		public enum GrowWorldNextAtCorner_neg_0_NodeNums { @cur, };
		public enum GrowWorldNextAtCorner_neg_0_EdgeNums { @_edge0, };
		public enum GrowWorldNextAtCorner_neg_0_VariableNums { };
		public enum GrowWorldNextAtCorner_neg_0_SubNums { };
		public enum GrowWorldNextAtCorner_neg_0_AltNums { };
		public enum GrowWorldNextAtCorner_neg_0_IterNums { };


		public GRGEN_LGSP.PatternGraph GrowWorldNextAtCorner_neg_0;


		private Rule_GrowWorldNextAtCorner()
			: base("GrowWorldNextAtCorner",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, },
				new string[] { "GrowWorldNextAtCorner_node_cur", "GrowWorldNextAtCorner_node_curOuter", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, },
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
			bool[,] GrowWorldNextAtCorner_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] GrowWorldNextAtCorner_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldNextAtCorner_isNodeTotallyHomomorphic = new bool[4] { false, false, false, false,  };
			bool[] GrowWorldNextAtCorner_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridCornerNode, GRGEN_MODEL.NodeType_GridCornerNode.typeVar, "GRGEN_MODEL.IGridCornerNode", "GrowWorldNextAtCorner_node_cur", "cur", GrowWorldNextAtCorner_node_cur_AllowedTypes, GrowWorldNextAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldNextAtCorner_node_next", "next", GrowWorldNextAtCorner_node_next_AllowedTypes, GrowWorldNextAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_curOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldNextAtCorner_node_curOuter", "curOuter", GrowWorldNextAtCorner_node_curOuter_AllowedTypes, GrowWorldNextAtCorner_node_curOuter_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, GRGEN_MODEL.NodeType_AntHill.typeVar, "GRGEN_MODEL.IAntHill", "GrowWorldNextAtCorner_node_hill", "hill", GrowWorldNextAtCorner_node_hill_AllowedTypes, GrowWorldNextAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GrowWorldNextAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, GRGEN_MODEL.EdgeType_GridEdge.typeVar, "GRGEN_MODEL.IGridEdge", "GrowWorldNextAtCorner_edge__edge0", "_edge0", GrowWorldNextAtCorner_edge__edge0_AllowedTypes, GrowWorldNextAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] GrowWorldNextAtCorner_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] GrowWorldNextAtCorner_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldNextAtCorner_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] GrowWorldNextAtCorner_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge GrowWorldNextAtCorner_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, GRGEN_MODEL.EdgeType_PathToHill.typeVar, "GRGEN_MODEL.IPathToHill", "GrowWorldNextAtCorner_neg_0_edge__edge0", "_edge0", GrowWorldNextAtCorner_neg_0_edge__edge0_AllowedTypes, GrowWorldNextAtCorner_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GrowWorldNextAtCorner_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"GrowWorldNextAtCorner_",
				null, "neg_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowWorldNextAtCorner_node_cur }, 
				new GRGEN_LGSP.PatternEdge[] { GrowWorldNextAtCorner_neg_0_edge__edge0 }, 
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
				GrowWorldNextAtCorner_neg_0_isNodeHomomorphicGlobal,
				GrowWorldNextAtCorner_neg_0_isEdgeHomomorphicGlobal,
				GrowWorldNextAtCorner_neg_0_isNodeTotallyHomomorphic,
				GrowWorldNextAtCorner_neg_0_isEdgeTotallyHomomorphic
			);
			GrowWorldNextAtCorner_neg_0.edgeToTargetNode.Add(GrowWorldNextAtCorner_neg_0_edge__edge0, GrowWorldNextAtCorner_node_cur);

			pat_GrowWorldNextAtCorner = new GRGEN_LGSP.PatternGraph(
				"GrowWorldNextAtCorner",
				"",
				null, "GrowWorldNextAtCorner",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_next, GrowWorldNextAtCorner_node_curOuter, GrowWorldNextAtCorner_node_hill }, 
				new GRGEN_LGSP.PatternEdge[] { GrowWorldNextAtCorner_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { GrowWorldNextAtCorner_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				GrowWorldNextAtCorner_isNodeHomomorphicGlobal,
				GrowWorldNextAtCorner_isEdgeHomomorphicGlobal,
				GrowWorldNextAtCorner_isNodeTotallyHomomorphic,
				GrowWorldNextAtCorner_isEdgeTotallyHomomorphic
			);
			pat_GrowWorldNextAtCorner.edgeToSourceNode.Add(GrowWorldNextAtCorner_edge__edge0, GrowWorldNextAtCorner_node_cur);
			pat_GrowWorldNextAtCorner.edgeToTargetNode.Add(GrowWorldNextAtCorner_edge__edge0, GrowWorldNextAtCorner_node_next);
			GrowWorldNextAtCorner_neg_0.embeddingGraph = pat_GrowWorldNextAtCorner;

			GrowWorldNextAtCorner_node_cur.pointOfDefinition = null;
			GrowWorldNextAtCorner_node_next.pointOfDefinition = pat_GrowWorldNextAtCorner;
			GrowWorldNextAtCorner_node_curOuter.pointOfDefinition = null;
			GrowWorldNextAtCorner_node_hill.pointOfDefinition = pat_GrowWorldNextAtCorner;
			GrowWorldNextAtCorner_edge__edge0.pointOfDefinition = pat_GrowWorldNextAtCorner;
			GrowWorldNextAtCorner_neg_0_edge__edge0.pointOfDefinition = GrowWorldNextAtCorner_neg_0;

			patternGraph = pat_GrowWorldNextAtCorner;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GrowWorldNextAtCorner curMatch = (Match_GrowWorldNextAtCorner)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_hill = curMatch._node_hill;
			GRGEN_LGSP.LGSPNode node_cur = curMatch._node_cur;
			GRGEN_LGSP.LGSPNode node_curOuter = curMatch._node_curOuter;
			GRGEN_MODEL.IAntHill inode_hill = curMatch.node_hill;
			graph.SettingAddedNodeNames( GrowWorldNextAtCorner_addedNodeNames );
			GRGEN_MODEL.@GridNode node_outer1 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			GRGEN_MODEL.@GridCornerNode node_outer2 = GRGEN_MODEL.@GridCornerNode.CreateNode(graph);
			GRGEN_MODEL.@GridNode node_outer3 = GRGEN_MODEL.@GridNode.CreateNode(graph);
			graph.SettingAddedEdgeNames( GrowWorldNextAtCorner_addedEdgeNames );
			GRGEN_MODEL.@PathToHill edge__edge1 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer1, node_cur);
			GRGEN_MODEL.@PathToHill edge__edge2 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer2, node_cur);
			GRGEN_MODEL.@PathToHill edge__edge3 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer3, node_cur);
			GRGEN_MODEL.@GridEdge edge__edge4 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_curOuter, node_outer1);
			GRGEN_MODEL.@GridEdge edge__edge5 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_outer1, node_outer2);
			GRGEN_MODEL.@GridEdge edge__edge6 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_outer2, node_outer3);
			{ // eval_0
				int tempvar_0 = (int )(inode_hill.@foodCountdown - 3);
				graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_hill.@foodCountdown = tempvar_0;
				graph.ChangedNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown);
			}
			GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;
			ApplyXGRS_GrowWorldNextAtCorner_0(procEnv, (GRGEN_MODEL.IGridNode)node_outer1, (GRGEN_MODEL.IGridCornerNode)node_outer2, (GRGEN_MODEL.IGridNode)node_outer3);
			output_0 = (GRGEN_MODEL.IGridNode)(node_next);
			output_1 = (GRGEN_MODEL.IGridNode)(node_outer3);
			return;
		}
		private static string[] GrowWorldNextAtCorner_addedNodeNames = new string[] { "outer1", "outer2", "outer3" };
		private static string[] GrowWorldNextAtCorner_addedEdgeNames = new string[] { "_edge1", "_edge2", "_edge3", "_edge4", "_edge5", "_edge6" };

        public static bool ApplyXGRS_GrowWorldNextAtCorner_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IGridNode var_outer1, GRGEN_MODEL.IGridCornerNode var_outer2, GRGEN_MODEL.IGridNode var_outer3)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            procEnv.DebugEntering("GrowWorldNextAtCorner.exec_0", "GrowFoodIfEqual(outer1,-2) || GrowFoodIfEqual(outer2,-1) || GrowFoodIfEqual(outer3,0)");
            bool res_24;
            bool res_20;
            bool res_16;
            GRGEN_ACTIONS.Action_GrowFoodIfEqual rule_GrowFoodIfEqual = GRGEN_ACTIONS.Action_GrowFoodIfEqual.Instance;
            bool res_19;
            bool res_23;
            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_16 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer1, (int)-2);
            procEnv.PerformanceInfo.MatchesFound += matches_16.Count;
            if(matches_16.Count == 0) {
                res_16 = (bool)(false);
            } else {
                res_16 = (bool)(true);
                procEnv.Matched(matches_16, null, false);
                procEnv.Finishing(matches_16, false);
                GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_16 = matches_16.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_16);
                procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_16, false);
            }
            if(res_16)
                res_20 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_19 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer2, (int)-1);
                procEnv.PerformanceInfo.MatchesFound += matches_19.Count;
                if(matches_19.Count == 0) {
                    res_19 = (bool)(false);
                } else {
                    res_19 = (bool)(true);
                    procEnv.Matched(matches_19, null, false);
                    procEnv.Finishing(matches_19, false);
                    GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_19 = matches_19.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_19);
                    procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_19, false);
                }
                res_20 = (bool)(res_19);
            }
            if(res_20)
                res_24 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_23 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer3, (int)0);
                procEnv.PerformanceInfo.MatchesFound += matches_23.Count;
                if(matches_23.Count == 0) {
                    res_23 = (bool)(false);
                } else {
                    res_23 = (bool)(true);
                    procEnv.Matched(matches_23, null, false);
                    procEnv.Finishing(matches_23, false);
                    GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_23 = matches_23.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_23);
                    procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_23, false);
                }
                res_24 = (bool)(res_23);
            }
            procEnv.DebugExiting("GrowWorldNextAtCorner.exec_0");
            return res_24;
        }

		static Rule_GrowWorldNextAtCorner() {
		}

		public interface IMatch_GrowWorldNextAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridCornerNode node_cur { get; set; }
			GRGEN_MODEL.IGridNode node_next { get; set; }
			GRGEN_MODEL.IGridNode node_curOuter { get; set; }
			GRGEN_MODEL.IAntHill node_hill { get; set; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_GrowWorldNextAtCorner_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridCornerNode node_cur { get; set; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldNextAtCorner : GRGEN_LGSP.MatchListElement<Match_GrowWorldNextAtCorner>, IMatch_GrowWorldNextAtCorner
		{
			public GRGEN_MODEL.IGridCornerNode node_cur { get { return (GRGEN_MODEL.IGridCornerNode)_node_cur; } set { _node_cur = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } set { _node_next = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_curOuter { get { return (GRGEN_MODEL.IGridNode)_node_curOuter; } set { _node_curOuter = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } set { _node_hill = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_curOuter;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldNextAtCorner_NodeNums { @cur, @next, @curOuter, @hill, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 4;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldNextAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldNextAtCorner_NodeNums.@curOuter: return _node_curOuter;
				case (int)GrowWorldNextAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "cur": return _node_cur;
				case "next": return _node_next;
				case "curOuter": return _node_curOuter;
				case "hill": return _node_hill;
				default: return null;
				}
			}

			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GrowWorldNextAtCorner_VariableNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_SubNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_AltNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_IterNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextAtCorner.instance.pat_GrowWorldNextAtCorner; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextAtCorner(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowWorldNextAtCorner nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowWorldNextAtCorner cur = this;
				while(cur != null) {
					Match_GrowWorldNextAtCorner next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowWorldNextAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_curOuter = that._node_curOuter;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GrowWorldNextAtCorner(Match_GrowWorldNextAtCorner that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowWorldNextAtCorner()
			{
			}

			public bool IsEqual(Match_GrowWorldNextAtCorner that)
			{
				if(that==null) return false;
				if(_node_cur != that._node_cur) return false;
				if(_node_next != that._node_next) return false;
				if(_node_curOuter != that._node_curOuter) return false;
				if(_node_hill != that._node_hill) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_GrowWorldNextAtCorner_neg_0 : GRGEN_LGSP.MatchListElement<Match_GrowWorldNextAtCorner_neg_0>, IMatch_GrowWorldNextAtCorner_neg_0
		{
			public GRGEN_MODEL.IGridCornerNode node_cur { get { return (GRGEN_MODEL.IGridCornerNode)_node_cur; } set { _node_cur = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public enum GrowWorldNextAtCorner_neg_0_NodeNums { @cur, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_neg_0_NodeNums.@cur: return _node_cur;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "cur": return _node_cur;
				default: return null;
				}
			}

			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextAtCorner_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GrowWorldNextAtCorner_neg_0_VariableNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_neg_0_SubNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_neg_0_AltNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_neg_0_IterNums { END_OF_ENUM };
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

			public enum GrowWorldNextAtCorner_neg_0_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextAtCorner.instance.GrowWorldNextAtCorner_neg_0; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextAtCorner_neg_0(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowWorldNextAtCorner_neg_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowWorldNextAtCorner_neg_0 cur = this;
				while(cur != null) {
					Match_GrowWorldNextAtCorner_neg_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowWorldNextAtCorner_neg_0 that)
			{
				_node_cur = that._node_cur;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GrowWorldNextAtCorner_neg_0(Match_GrowWorldNextAtCorner_neg_0 that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowWorldNextAtCorner_neg_0()
			{
			}

			public bool IsEqual(Match_GrowWorldNextAtCorner_neg_0 that)
			{
				if(that==null) return false;
				if(_node_cur != that._node_cur) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IGridCornerNode> Extract_cur(List<IMatch_GrowWorldNextAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridCornerNode> resultList = new List<GRGEN_MODEL.IGridCornerNode>(matchList.Count);
				foreach(IMatch_GrowWorldNextAtCorner match in matchList)
					resultList.Add(match.node_cur);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_next(List<IMatch_GrowWorldNextAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldNextAtCorner match in matchList)
					resultList.Add(match.node_next);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_curOuter(List<IMatch_GrowWorldNextAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldNextAtCorner match in matchList)
					resultList.Add(match.node_curOuter);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntHill> Extract_hill(List<IMatch_GrowWorldNextAtCorner> matchList)
			{
				List<GRGEN_MODEL.IAntHill> resultList = new List<GRGEN_MODEL.IAntHill>(matchList.Count);
				foreach(IMatch_GrowWorldNextAtCorner match in matchList)
					resultList.Add(match.node_hill);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridEdge> Extract__edge0(List<IMatch_GrowWorldNextAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridEdge> resultList = new List<GRGEN_MODEL.IGridEdge>(matchList.Count);
				foreach(IMatch_GrowWorldNextAtCorner match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_GrowWorldNextNotAtCorner : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GrowWorldNextNotAtCorner instance = null;
		public static Rule_GrowWorldNextNotAtCorner Instance { get { if (instance==null) { instance = new Rule_GrowWorldNextNotAtCorner(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GrowWorldNextNotAtCorner_node_cur_AllowedTypes = { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_AntHill.typeVar, };
		public static GRGEN_LIBGR.NodeType[] GrowWorldNextNotAtCorner_node_next_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldNextNotAtCorner_node_curOuter_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldNextNotAtCorner_node_hill_AllowedTypes = null;
		public static bool[] GrowWorldNextNotAtCorner_node_cur_IsAllowedType = { false, true, false, true, false, };
		public static bool[] GrowWorldNextNotAtCorner_node_next_IsAllowedType = null;
		public static bool[] GrowWorldNextNotAtCorner_node_curOuter_IsAllowedType = null;
		public static bool[] GrowWorldNextNotAtCorner_node_hill_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] GrowWorldNextNotAtCorner_edge__edge0_AllowedTypes = { GRGEN_MODEL.EdgeType_GridEdge.typeVar, };
		public static bool[] GrowWorldNextNotAtCorner_edge__edge0_IsAllowedType = { false, false, false, true, false, false, false, };
		public enum GrowWorldNextNotAtCorner_NodeNums { @cur, @next, @curOuter, @hill, };
		public enum GrowWorldNextNotAtCorner_EdgeNums { @_edge0, };
		public enum GrowWorldNextNotAtCorner_VariableNums { };
		public enum GrowWorldNextNotAtCorner_SubNums { };
		public enum GrowWorldNextNotAtCorner_AltNums { };
		public enum GrowWorldNextNotAtCorner_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_GrowWorldNextNotAtCorner;

		public static GRGEN_LIBGR.EdgeType[] GrowWorldNextNotAtCorner_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] GrowWorldNextNotAtCorner_neg_0_edge__edge0_IsAllowedType = null;
		public enum GrowWorldNextNotAtCorner_neg_0_NodeNums { @cur, };
		public enum GrowWorldNextNotAtCorner_neg_0_EdgeNums { @_edge0, };
		public enum GrowWorldNextNotAtCorner_neg_0_VariableNums { };
		public enum GrowWorldNextNotAtCorner_neg_0_SubNums { };
		public enum GrowWorldNextNotAtCorner_neg_0_AltNums { };
		public enum GrowWorldNextNotAtCorner_neg_0_IterNums { };


		public GRGEN_LGSP.PatternGraph GrowWorldNextNotAtCorner_neg_0;


		private Rule_GrowWorldNextNotAtCorner()
			: base("GrowWorldNextNotAtCorner",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, },
				new string[] { "GrowWorldNextNotAtCorner_node_cur", "GrowWorldNextNotAtCorner_node_curOuter", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, },
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
			bool[,] GrowWorldNextNotAtCorner_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] GrowWorldNextNotAtCorner_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldNextNotAtCorner_isNodeTotallyHomomorphic = new bool[4] { false, false, false, false,  };
			bool[] GrowWorldNextNotAtCorner_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldNextNotAtCorner_node_cur", "cur", GrowWorldNextNotAtCorner_node_cur_AllowedTypes, GrowWorldNextNotAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldNextNotAtCorner_node_next", "next", GrowWorldNextNotAtCorner_node_next_AllowedTypes, GrowWorldNextNotAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_curOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldNextNotAtCorner_node_curOuter", "curOuter", GrowWorldNextNotAtCorner_node_curOuter_AllowedTypes, GrowWorldNextNotAtCorner_node_curOuter_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, GRGEN_MODEL.NodeType_AntHill.typeVar, "GRGEN_MODEL.IAntHill", "GrowWorldNextNotAtCorner_node_hill", "hill", GrowWorldNextNotAtCorner_node_hill_AllowedTypes, GrowWorldNextNotAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GrowWorldNextNotAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, GRGEN_MODEL.EdgeType_GridEdge.typeVar, "GRGEN_MODEL.IGridEdge", "GrowWorldNextNotAtCorner_edge__edge0", "_edge0", GrowWorldNextNotAtCorner_edge__edge0_AllowedTypes, GrowWorldNextNotAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			bool[,] GrowWorldNextNotAtCorner_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] GrowWorldNextNotAtCorner_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldNextNotAtCorner_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] GrowWorldNextNotAtCorner_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge GrowWorldNextNotAtCorner_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, GRGEN_MODEL.EdgeType_PathToHill.typeVar, "GRGEN_MODEL.IPathToHill", "GrowWorldNextNotAtCorner_neg_0_edge__edge0", "_edge0", GrowWorldNextNotAtCorner_neg_0_edge__edge0_AllowedTypes, GrowWorldNextNotAtCorner_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GrowWorldNextNotAtCorner_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"GrowWorldNextNotAtCorner_",
				null, "neg_0",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowWorldNextNotAtCorner_node_cur }, 
				new GRGEN_LGSP.PatternEdge[] { GrowWorldNextNotAtCorner_neg_0_edge__edge0 }, 
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
				GrowWorldNextNotAtCorner_neg_0_isNodeHomomorphicGlobal,
				GrowWorldNextNotAtCorner_neg_0_isEdgeHomomorphicGlobal,
				GrowWorldNextNotAtCorner_neg_0_isNodeTotallyHomomorphic,
				GrowWorldNextNotAtCorner_neg_0_isEdgeTotallyHomomorphic
			);
			GrowWorldNextNotAtCorner_neg_0.edgeToTargetNode.Add(GrowWorldNextNotAtCorner_neg_0_edge__edge0, GrowWorldNextNotAtCorner_node_cur);

			pat_GrowWorldNextNotAtCorner = new GRGEN_LGSP.PatternGraph(
				"GrowWorldNextNotAtCorner",
				"",
				null, "GrowWorldNextNotAtCorner",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_next, GrowWorldNextNotAtCorner_node_curOuter, GrowWorldNextNotAtCorner_node_hill }, 
				new GRGEN_LGSP.PatternEdge[] { GrowWorldNextNotAtCorner_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] { GrowWorldNextNotAtCorner_neg_0,  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] {  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[4, 4] {
					{ true, false, false, false, },
					{ false, true, false, false, },
					{ false, false, true, false, },
					{ false, false, false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				GrowWorldNextNotAtCorner_isNodeHomomorphicGlobal,
				GrowWorldNextNotAtCorner_isEdgeHomomorphicGlobal,
				GrowWorldNextNotAtCorner_isNodeTotallyHomomorphic,
				GrowWorldNextNotAtCorner_isEdgeTotallyHomomorphic
			);
			pat_GrowWorldNextNotAtCorner.edgeToSourceNode.Add(GrowWorldNextNotAtCorner_edge__edge0, GrowWorldNextNotAtCorner_node_cur);
			pat_GrowWorldNextNotAtCorner.edgeToTargetNode.Add(GrowWorldNextNotAtCorner_edge__edge0, GrowWorldNextNotAtCorner_node_next);
			GrowWorldNextNotAtCorner_neg_0.embeddingGraph = pat_GrowWorldNextNotAtCorner;

			GrowWorldNextNotAtCorner_node_cur.pointOfDefinition = null;
			GrowWorldNextNotAtCorner_node_next.pointOfDefinition = pat_GrowWorldNextNotAtCorner;
			GrowWorldNextNotAtCorner_node_curOuter.pointOfDefinition = null;
			GrowWorldNextNotAtCorner_node_hill.pointOfDefinition = pat_GrowWorldNextNotAtCorner;
			GrowWorldNextNotAtCorner_edge__edge0.pointOfDefinition = pat_GrowWorldNextNotAtCorner;
			GrowWorldNextNotAtCorner_neg_0_edge__edge0.pointOfDefinition = GrowWorldNextNotAtCorner_neg_0;

			patternGraph = pat_GrowWorldNextNotAtCorner;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GrowWorldNextNotAtCorner curMatch = (Match_GrowWorldNextNotAtCorner)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			GRGEN_LGSP.LGSPNode node_hill = curMatch._node_hill;
			GRGEN_LGSP.LGSPNode node_cur = curMatch._node_cur;
			GRGEN_LGSP.LGSPNode node_curOuter = curMatch._node_curOuter;
			GRGEN_MODEL.IAntHill inode_hill = curMatch.node_hill;
			graph.SettingAddedNodeNames( GrowWorldNextNotAtCorner_addedNodeNames );
			GRGEN_MODEL.@GridNode node_outer = GRGEN_MODEL.@GridNode.CreateNode(graph);
			graph.SettingAddedEdgeNames( GrowWorldNextNotAtCorner_addedEdgeNames );
			GRGEN_MODEL.@PathToHill edge__edge1 = GRGEN_MODEL.@PathToHill.CreateEdge(graph, node_outer, node_cur);
			GRGEN_MODEL.@GridEdge edge__edge2 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_curOuter, node_outer);
			{ // eval_0
				int tempvar_0 = (int )(inode_hill.@foodCountdown - 1);
				graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_hill.@foodCountdown = tempvar_0;
				graph.ChangedNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown);
			}
			GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;
			ApplyXGRS_GrowWorldNextNotAtCorner_0(procEnv, (GRGEN_MODEL.IGridNode)node_outer);
			output_0 = (GRGEN_MODEL.IGridNode)(node_next);
			output_1 = (GRGEN_MODEL.IGridNode)(node_outer);
			return;
		}
		private static string[] GrowWorldNextNotAtCorner_addedNodeNames = new string[] { "outer" };
		private static string[] GrowWorldNextNotAtCorner_addedEdgeNames = new string[] { "_edge1", "_edge2" };

        public static bool ApplyXGRS_GrowWorldNextNotAtCorner_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IGridNode var_outer)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            procEnv.DebugEntering("GrowWorldNextNotAtCorner.exec_0", "GrowFoodIfEqual(outer,0)");
            bool res_27;
            GRGEN_ACTIONS.Action_GrowFoodIfEqual rule_GrowFoodIfEqual = GRGEN_ACTIONS.Action_GrowFoodIfEqual.Instance;
            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_27 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer, (int)0);
            procEnv.PerformanceInfo.MatchesFound += matches_27.Count;
            if(matches_27.Count == 0) {
                res_27 = (bool)(false);
            } else {
                res_27 = (bool)(true);
                procEnv.Matched(matches_27, null, false);
                procEnv.Finishing(matches_27, false);
                GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_27 = matches_27.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_27);
                procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_27, false);
            }
            procEnv.DebugExiting("GrowWorldNextNotAtCorner.exec_0");
            return res_27;
        }

		static Rule_GrowWorldNextNotAtCorner() {
		}

		public interface IMatch_GrowWorldNextNotAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_cur { get; set; }
			GRGEN_MODEL.IGridNode node_next { get; set; }
			GRGEN_MODEL.IGridNode node_curOuter { get; set; }
			GRGEN_MODEL.IAntHill node_hill { get; set; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public interface IMatch_GrowWorldNextNotAtCorner_neg_0 : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_cur { get; set; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldNextNotAtCorner : GRGEN_LGSP.MatchListElement<Match_GrowWorldNextNotAtCorner>, IMatch_GrowWorldNextNotAtCorner
		{
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } set { _node_cur = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } set { _node_next = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_curOuter { get { return (GRGEN_MODEL.IGridNode)_node_curOuter; } set { _node_curOuter = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } set { _node_hill = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_curOuter;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldNextNotAtCorner_NodeNums { @cur, @next, @curOuter, @hill, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 4;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldNextNotAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldNextNotAtCorner_NodeNums.@curOuter: return _node_curOuter;
				case (int)GrowWorldNextNotAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "cur": return _node_cur;
				case "next": return _node_next;
				case "curOuter": return _node_curOuter;
				case "hill": return _node_hill;
				default: return null;
				}
			}

			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextNotAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GrowWorldNextNotAtCorner_VariableNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_SubNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_AltNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_IterNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextNotAtCorner.instance.pat_GrowWorldNextNotAtCorner; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextNotAtCorner(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowWorldNextNotAtCorner nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowWorldNextNotAtCorner cur = this;
				while(cur != null) {
					Match_GrowWorldNextNotAtCorner next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowWorldNextNotAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_curOuter = that._node_curOuter;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GrowWorldNextNotAtCorner(Match_GrowWorldNextNotAtCorner that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowWorldNextNotAtCorner()
			{
			}

			public bool IsEqual(Match_GrowWorldNextNotAtCorner that)
			{
				if(that==null) return false;
				if(_node_cur != that._node_cur) return false;
				if(_node_next != that._node_next) return false;
				if(_node_curOuter != that._node_curOuter) return false;
				if(_node_hill != that._node_hill) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}

		public class Match_GrowWorldNextNotAtCorner_neg_0 : GRGEN_LGSP.MatchListElement<Match_GrowWorldNextNotAtCorner_neg_0>, IMatch_GrowWorldNextNotAtCorner_neg_0
		{
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } set { _node_cur = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public enum GrowWorldNextNotAtCorner_neg_0_NodeNums { @cur, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_neg_0_NodeNums.@cur: return _node_cur;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "cur": return _node_cur;
				default: return null;
				}
			}

			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextNotAtCorner_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GrowWorldNextNotAtCorner_neg_0_VariableNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_neg_0_SubNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_neg_0_AltNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_neg_0_IterNums { END_OF_ENUM };
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

			public enum GrowWorldNextNotAtCorner_neg_0_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextNotAtCorner.instance.GrowWorldNextNotAtCorner_neg_0; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextNotAtCorner_neg_0(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowWorldNextNotAtCorner_neg_0 nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowWorldNextNotAtCorner_neg_0 cur = this;
				while(cur != null) {
					Match_GrowWorldNextNotAtCorner_neg_0 next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowWorldNextNotAtCorner_neg_0 that)
			{
				_node_cur = that._node_cur;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GrowWorldNextNotAtCorner_neg_0(Match_GrowWorldNextNotAtCorner_neg_0 that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowWorldNextNotAtCorner_neg_0()
			{
			}

			public bool IsEqual(Match_GrowWorldNextNotAtCorner_neg_0 that)
			{
				if(that==null) return false;
				if(_node_cur != that._node_cur) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IGridNode> Extract_cur(List<IMatch_GrowWorldNextNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldNextNotAtCorner match in matchList)
					resultList.Add(match.node_cur);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_next(List<IMatch_GrowWorldNextNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldNextNotAtCorner match in matchList)
					resultList.Add(match.node_next);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_curOuter(List<IMatch_GrowWorldNextNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldNextNotAtCorner match in matchList)
					resultList.Add(match.node_curOuter);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntHill> Extract_hill(List<IMatch_GrowWorldNextNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IAntHill> resultList = new List<GRGEN_MODEL.IAntHill>(matchList.Count);
				foreach(IMatch_GrowWorldNextNotAtCorner match in matchList)
					resultList.Add(match.node_hill);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridEdge> Extract__edge0(List<IMatch_GrowWorldNextNotAtCorner> matchList)
			{
				List<GRGEN_MODEL.IGridEdge> resultList = new List<GRGEN_MODEL.IGridEdge>(matchList.Count);
				foreach(IMatch_GrowWorldNextNotAtCorner match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_GrowWorldEnd : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GrowWorldEnd instance = null;
		public static Rule_GrowWorldEnd Instance { get { if (instance==null) { instance = new Rule_GrowWorldEnd(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GrowWorldEnd_node_nextOuter_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldEnd_node_cur_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GrowWorldEnd_node_curOuter_AllowedTypes = null;
		public static bool[] GrowWorldEnd_node_nextOuter_IsAllowedType = null;
		public static bool[] GrowWorldEnd_node_cur_IsAllowedType = null;
		public static bool[] GrowWorldEnd_node_curOuter_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] GrowWorldEnd_edge__edge0_AllowedTypes = null;
		public static bool[] GrowWorldEnd_edge__edge0_IsAllowedType = null;
		public enum GrowWorldEnd_NodeNums { @nextOuter, @cur, @curOuter, };
		public enum GrowWorldEnd_EdgeNums { @_edge0, };
		public enum GrowWorldEnd_VariableNums { };
		public enum GrowWorldEnd_SubNums { };
		public enum GrowWorldEnd_AltNums { };
		public enum GrowWorldEnd_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_GrowWorldEnd;


		private Rule_GrowWorldEnd()
			: base("GrowWorldEnd",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, },
				new string[] { "GrowWorldEnd_node_cur", "GrowWorldEnd_node_curOuter", },
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
			bool[,] GrowWorldEnd_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] GrowWorldEnd_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldEnd_isNodeTotallyHomomorphic = new bool[3] { false, false, false,  };
			bool[] GrowWorldEnd_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode GrowWorldEnd_node_nextOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldEnd_node_nextOuter", "nextOuter", GrowWorldEnd_node_nextOuter_AllowedTypes, GrowWorldEnd_node_nextOuter_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldEnd_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldEnd_node_cur", "cur", GrowWorldEnd_node_cur_AllowedTypes, GrowWorldEnd_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GrowWorldEnd_node_curOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "GrowWorldEnd_node_curOuter", "curOuter", GrowWorldEnd_node_curOuter_AllowedTypes, GrowWorldEnd_node_curOuter_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GrowWorldEnd_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, GRGEN_MODEL.EdgeType_PathToHill.typeVar, "GRGEN_MODEL.IPathToHill", "GrowWorldEnd_edge__edge0", "_edge0", GrowWorldEnd_edge__edge0_AllowedTypes, GrowWorldEnd_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_GrowWorldEnd = new GRGEN_LGSP.PatternGraph(
				"GrowWorldEnd",
				"",
				null, "GrowWorldEnd",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GrowWorldEnd_node_nextOuter, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter }, 
				new GRGEN_LGSP.PatternEdge[] { GrowWorldEnd_edge__edge0 }, 
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
				new bool[1, 1] {
					{ true, },
				},
				GrowWorldEnd_isNodeHomomorphicGlobal,
				GrowWorldEnd_isEdgeHomomorphicGlobal,
				GrowWorldEnd_isNodeTotallyHomomorphic,
				GrowWorldEnd_isEdgeTotallyHomomorphic
			);
			pat_GrowWorldEnd.edgeToSourceNode.Add(GrowWorldEnd_edge__edge0, GrowWorldEnd_node_nextOuter);
			pat_GrowWorldEnd.edgeToTargetNode.Add(GrowWorldEnd_edge__edge0, GrowWorldEnd_node_cur);

			GrowWorldEnd_node_nextOuter.pointOfDefinition = pat_GrowWorldEnd;
			GrowWorldEnd_node_cur.pointOfDefinition = null;
			GrowWorldEnd_node_curOuter.pointOfDefinition = null;
			GrowWorldEnd_edge__edge0.pointOfDefinition = pat_GrowWorldEnd;

			patternGraph = pat_GrowWorldEnd;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GrowWorldEnd curMatch = (Match_GrowWorldEnd)_curMatch;
			GRGEN_LGSP.LGSPNode node_curOuter = curMatch._node_curOuter;
			GRGEN_LGSP.LGSPNode node_nextOuter = curMatch._node_nextOuter;
			graph.SettingAddedNodeNames( GrowWorldEnd_addedNodeNames );
			graph.SettingAddedEdgeNames( GrowWorldEnd_addedEdgeNames );
			GRGEN_MODEL.@GridEdge edge__edge1 = GRGEN_MODEL.@GridEdge.CreateEdge(graph, node_curOuter, node_nextOuter);
			return;
		}
		private static string[] GrowWorldEnd_addedNodeNames = new string[] {  };
		private static string[] GrowWorldEnd_addedEdgeNames = new string[] { "_edge1" };

		static Rule_GrowWorldEnd() {
		}

		public interface IMatch_GrowWorldEnd : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_nextOuter { get; set; }
			GRGEN_MODEL.IGridNode node_cur { get; set; }
			GRGEN_MODEL.IGridNode node_curOuter { get; set; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldEnd : GRGEN_LGSP.MatchListElement<Match_GrowWorldEnd>, IMatch_GrowWorldEnd
		{
			public GRGEN_MODEL.IGridNode node_nextOuter { get { return (GRGEN_MODEL.IGridNode)_node_nextOuter; } set { _node_nextOuter = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } set { _node_cur = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IGridNode node_curOuter { get { return (GRGEN_MODEL.IGridNode)_node_curOuter; } set { _node_curOuter = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_nextOuter;
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_curOuter;
			public enum GrowWorldEnd_NodeNums { @nextOuter, @cur, @curOuter, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 3;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldEnd_NodeNums.@nextOuter: return _node_nextOuter;
				case (int)GrowWorldEnd_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldEnd_NodeNums.@curOuter: return _node_curOuter;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "nextOuter": return _node_nextOuter;
				case "cur": return _node_cur;
				case "curOuter": return _node_curOuter;
				default: return null;
				}
			}

			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldEnd_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldEnd_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GrowWorldEnd_VariableNums { END_OF_ENUM };
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

			public enum GrowWorldEnd_SubNums { END_OF_ENUM };
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

			public enum GrowWorldEnd_AltNums { END_OF_ENUM };
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

			public enum GrowWorldEnd_IterNums { END_OF_ENUM };
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

			public enum GrowWorldEnd_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldEnd.instance.pat_GrowWorldEnd; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldEnd(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GrowWorldEnd nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GrowWorldEnd cur = this;
				while(cur != null) {
					Match_GrowWorldEnd next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GrowWorldEnd that)
			{
				_node_nextOuter = that._node_nextOuter;
				_node_cur = that._node_cur;
				_node_curOuter = that._node_curOuter;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GrowWorldEnd(Match_GrowWorldEnd that)
			{
				CopyMatchContent(that);
			}
			public Match_GrowWorldEnd()
			{
			}

			public bool IsEqual(Match_GrowWorldEnd that)
			{
				if(that==null) return false;
				if(_node_nextOuter != that._node_nextOuter) return false;
				if(_node_cur != that._node_cur) return false;
				if(_node_curOuter != that._node_curOuter) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IGridNode> Extract_nextOuter(List<IMatch_GrowWorldEnd> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldEnd match in matchList)
					resultList.Add(match.node_nextOuter);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_cur(List<IMatch_GrowWorldEnd> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldEnd match in matchList)
					resultList.Add(match.node_cur);
				return resultList;
			}
			public static List<GRGEN_MODEL.IGridNode> Extract_curOuter(List<IMatch_GrowWorldEnd> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_GrowWorldEnd match in matchList)
					resultList.Add(match.node_curOuter);
				return resultList;
			}
			public static List<GRGEN_MODEL.IPathToHill> Extract__edge0(List<IMatch_GrowWorldEnd> matchList)
			{
				List<GRGEN_MODEL.IPathToHill> resultList = new List<GRGEN_MODEL.IPathToHill>(matchList.Count);
				foreach(IMatch_GrowWorldEnd match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_GetNextAnt : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_GetNextAnt instance = null;
		public static Rule_GetNextAnt Instance { get { if (instance==null) { instance = new Rule_GetNextAnt(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] GetNextAnt_node_curAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] GetNextAnt_node_next_AllowedTypes = null;
		public static bool[] GetNextAnt_node_curAnt_IsAllowedType = null;
		public static bool[] GetNextAnt_node_next_IsAllowedType = null;
		public static GRGEN_LIBGR.EdgeType[] GetNextAnt_edge__edge0_AllowedTypes = null;
		public static bool[] GetNextAnt_edge__edge0_IsAllowedType = null;
		public enum GetNextAnt_NodeNums { @curAnt, @next, };
		public enum GetNextAnt_EdgeNums { @_edge0, };
		public enum GetNextAnt_VariableNums { };
		public enum GetNextAnt_SubNums { };
		public enum GetNextAnt_AltNums { };
		public enum GetNextAnt_IterNums { };



		public GRGEN_LGSP.PatternGraph pat_GetNextAnt;


		private Rule_GetNextAnt()
			: base("GetNextAnt",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "GetNextAnt_node_curAnt", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
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
			bool[,] GetNextAnt_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] GetNextAnt_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GetNextAnt_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] GetNextAnt_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternNode GetNextAnt_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "GetNextAnt_node_curAnt", "curAnt", GetNextAnt_node_curAnt_AllowedTypes, GetNextAnt_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode GetNextAnt_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "GetNextAnt_node_next", "next", GetNextAnt_node_next_AllowedTypes, GetNextAnt_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternEdge GetNextAnt_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@NextAnt, GRGEN_MODEL.EdgeType_NextAnt.typeVar, "GRGEN_MODEL.INextAnt", "GetNextAnt_edge__edge0", "_edge0", GetNextAnt_edge__edge0_AllowedTypes, GetNextAnt_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_GetNextAnt = new GRGEN_LGSP.PatternGraph(
				"GetNextAnt",
				"",
				null, "GetNextAnt",
				false, false,
				new GRGEN_LGSP.PatternNode[] { GetNextAnt_node_curAnt, GetNextAnt_node_next }, 
				new GRGEN_LGSP.PatternEdge[] { GetNextAnt_edge__edge0 }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
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
				GetNextAnt_isNodeHomomorphicGlobal,
				GetNextAnt_isEdgeHomomorphicGlobal,
				GetNextAnt_isNodeTotallyHomomorphic,
				GetNextAnt_isEdgeTotallyHomomorphic
			);
			pat_GetNextAnt.edgeToSourceNode.Add(GetNextAnt_edge__edge0, GetNextAnt_node_curAnt);
			pat_GetNextAnt.edgeToTargetNode.Add(GetNextAnt_edge__edge0, GetNextAnt_node_next);

			GetNextAnt_node_curAnt.pointOfDefinition = null;
			GetNextAnt_node_next.pointOfDefinition = pat_GetNextAnt;
			GetNextAnt_edge__edge0.pointOfDefinition = pat_GetNextAnt;

			patternGraph = pat_GetNextAnt;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IAnt output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_GetNextAnt curMatch = (Match_GetNextAnt)_curMatch;
			GRGEN_LGSP.LGSPNode node_next = curMatch._node_next;
			output_0 = (GRGEN_MODEL.IAnt)(node_next);
			return;
		}

		static Rule_GetNextAnt() {
		}

		public interface IMatch_GetNextAnt : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; set; }
			GRGEN_MODEL.IAnt node_next { get; set; }
			//Edges
			GRGEN_MODEL.INextAnt edge__edge0 { get; set; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GetNextAnt : GRGEN_LGSP.MatchListElement<Match_GetNextAnt>, IMatch_GetNextAnt
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } set { _node_curAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAnt node_next { get { return (GRGEN_MODEL.IAnt)_node_next; } set { _node_next = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_next;
			public enum GetNextAnt_NodeNums { @curAnt, @next, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GetNextAnt_NodeNums.@curAnt: return _node_curAnt;
				case (int)GetNextAnt_NodeNums.@next: return _node_next;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "curAnt": return _node_curAnt;
				case "next": return _node_next;
				default: return null;
				}
			}

			public GRGEN_MODEL.INextAnt edge__edge0 { get { return (GRGEN_MODEL.INextAnt)_edge__edge0; } set { _edge__edge0 = (GRGEN_LGSP.LGSPEdge)value; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GetNextAnt_EdgeNums { @_edge0, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public override int NumberOfEdges { get { return 1;} }
			public override GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GetNextAnt_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.IEdge getEdge(string name)
			{
				switch(name) {
				case "_edge0": return _edge__edge0;
				default: return null;
				}
			}

			public enum GetNextAnt_VariableNums { END_OF_ENUM };
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

			public enum GetNextAnt_SubNums { END_OF_ENUM };
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

			public enum GetNextAnt_AltNums { END_OF_ENUM };
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

			public enum GetNextAnt_IterNums { END_OF_ENUM };
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

			public enum GetNextAnt_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GetNextAnt.instance.pat_GetNextAnt; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_GetNextAnt(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_GetNextAnt nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_GetNextAnt cur = this;
				while(cur != null) {
					Match_GetNextAnt next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_GetNextAnt that)
			{
				_node_curAnt = that._node_curAnt;
				_node_next = that._node_next;
				_edge__edge0 = that._edge__edge0;
			}

			public Match_GetNextAnt(Match_GetNextAnt that)
			{
				CopyMatchContent(that);
			}
			public Match_GetNextAnt()
			{
			}

			public bool IsEqual(Match_GetNextAnt that)
			{
				if(that==null) return false;
				if(_node_curAnt != that._node_curAnt) return false;
				if(_node_next != that._node_next) return false;
				if(_edge__edge0 != that._edge__edge0) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_curAnt(List<IMatch_GetNextAnt> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_GetNextAnt match in matchList)
					resultList.Add(match.node_curAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAnt> Extract_next(List<IMatch_GetNextAnt> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_GetNextAnt match in matchList)
					resultList.Add(match.node_next);
				return resultList;
			}
			public static List<GRGEN_MODEL.INextAnt> Extract__edge0(List<IMatch_GetNextAnt> matchList)
			{
				List<GRGEN_MODEL.INextAnt> resultList = new List<GRGEN_MODEL.INextAnt>(matchList.Count);
				foreach(IMatch_GetNextAnt match in matchList)
					resultList.Add(match.edge__edge0);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_Food2Ant : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_Food2Ant instance = null;
		public static Rule_Food2Ant Instance { get { if (instance==null) { instance = new Rule_Food2Ant(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] Food2Ant_node_lastAnt_AllowedTypes = null;
		public static GRGEN_LIBGR.NodeType[] Food2Ant_node_hill_AllowedTypes = null;
		public static bool[] Food2Ant_node_lastAnt_IsAllowedType = null;
		public static bool[] Food2Ant_node_hill_IsAllowedType = null;
		public enum Food2Ant_NodeNums { @lastAnt, @hill, };
		public enum Food2Ant_EdgeNums { };
		public enum Food2Ant_VariableNums { };
		public enum Food2Ant_SubNums { };
		public enum Food2Ant_AltNums { };
		public enum Food2Ant_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_Food2Ant;


		private Rule_Food2Ant()
			: base("Food2Ant",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "Food2Ant_node_lastAnt", },
				new GRGEN_LIBGR.GrGenType[] { },
				new string[] { },
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
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
			bool[,] Food2Ant_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Food2Ant_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] Food2Ant_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] Food2Ant_isEdgeTotallyHomomorphic = new bool[0];
			GRGEN_LGSP.PatternNode Food2Ant_node_lastAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "Food2Ant_node_lastAnt", "lastAnt", Food2Ant_node_lastAnt_AllowedTypes, Food2Ant_node_lastAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternNode Food2Ant_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, GRGEN_MODEL.NodeType_AntHill.typeVar, "GRGEN_MODEL.IAntHill", "Food2Ant_node_hill", "hill", Food2Ant_node_hill_AllowedTypes, Food2Ant_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			GRGEN_LGSP.PatternCondition Food2Ant_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAntHill", "Food2Ant_node_hill", "food"), new GRGEN_EXPR.Constant("0")),
				new string[] { "Food2Ant_node_hill" }, new string[] {  }, new string[] {  },
				new GRGEN_LGSP.PatternNode[] { Food2Ant_node_hill }, new GRGEN_LGSP.PatternEdge[] {  }, new GRGEN_LGSP.PatternVariable[] {  });
			pat_Food2Ant = new GRGEN_LGSP.PatternGraph(
				"Food2Ant",
				"",
				null, "Food2Ant",
				false, false,
				new GRGEN_LGSP.PatternNode[] { Food2Ant_node_lastAnt, Food2Ant_node_hill }, 
				new GRGEN_LGSP.PatternEdge[] {  }, 
				new GRGEN_LGSP.PatternVariable[] {  }, 
				new GRGEN_LGSP.PatternGraphEmbedding[] {  }, 
				new GRGEN_LGSP.Alternative[] {  }, 
				new GRGEN_LGSP.Iterated[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternGraph[] {  }, 
				new GRGEN_LGSP.PatternCondition[] { Food2Ant_cond_0,  }, 
				new GRGEN_LGSP.PatternYielding[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				Food2Ant_isNodeHomomorphicGlobal,
				Food2Ant_isEdgeHomomorphicGlobal,
				Food2Ant_isNodeTotallyHomomorphic,
				Food2Ant_isEdgeTotallyHomomorphic
			);

			Food2Ant_node_lastAnt.pointOfDefinition = null;
			Food2Ant_node_hill.pointOfDefinition = pat_Food2Ant;

			patternGraph = pat_Food2Ant;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch, out GRGEN_MODEL.IAnt output_0)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_Food2Ant curMatch = (Match_Food2Ant)_curMatch;
			GRGEN_LGSP.LGSPNode node_hill = curMatch._node_hill;
			GRGEN_LGSP.LGSPNode node_lastAnt = curMatch._node_lastAnt;
			GRGEN_MODEL.IAntHill inode_hill = curMatch.node_hill;
			graph.SettingAddedNodeNames( Food2Ant_addedNodeNames );
			GRGEN_MODEL.@Ant node_newAnt = GRGEN_MODEL.@Ant.CreateNode(graph);
			graph.SettingAddedEdgeNames( Food2Ant_addedEdgeNames );
			GRGEN_MODEL.@NextAnt edge__edge0 = GRGEN_MODEL.@NextAnt.CreateEdge(graph, node_lastAnt, node_newAnt);
			GRGEN_MODEL.@AntPosition edge__edge1 = GRGEN_MODEL.@AntPosition.CreateEdge(graph, node_newAnt, node_hill);
			{ // eval_0
				int tempvar_0 = (int )(inode_hill.@food - 1);
				graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_hill.@food = tempvar_0;
				graph.ChangedNodeAttribute(node_hill, GRGEN_MODEL.NodeType_GridNode.AttributeType_food);
			}
			output_0 = (GRGEN_MODEL.IAnt)(node_newAnt);
			return;
		}
		private static string[] Food2Ant_addedNodeNames = new string[] { "newAnt" };
		private static string[] Food2Ant_addedEdgeNames = new string[] { "_edge0", "_edge1" };

		static Rule_Food2Ant() {
		}

		public interface IMatch_Food2Ant : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_lastAnt { get; set; }
			GRGEN_MODEL.IAntHill node_hill { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Food2Ant : GRGEN_LGSP.MatchListElement<Match_Food2Ant>, IMatch_Food2Ant
		{
			public GRGEN_MODEL.IAnt node_lastAnt { get { return (GRGEN_MODEL.IAnt)_node_lastAnt; } set { _node_lastAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } set { _node_hill = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_lastAnt;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum Food2Ant_NodeNums { @lastAnt, @hill, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 2;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Food2Ant_NodeNums.@lastAnt: return _node_lastAnt;
				case (int)Food2Ant_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "lastAnt": return _node_lastAnt;
				case "hill": return _node_hill;
				default: return null;
				}
			}

			public enum Food2Ant_EdgeNums { END_OF_ENUM };
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

			public enum Food2Ant_VariableNums { END_OF_ENUM };
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

			public enum Food2Ant_SubNums { END_OF_ENUM };
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

			public enum Food2Ant_AltNums { END_OF_ENUM };
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

			public enum Food2Ant_IterNums { END_OF_ENUM };
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

			public enum Food2Ant_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_Food2Ant.instance.pat_Food2Ant; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_Food2Ant(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_Food2Ant nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_Food2Ant cur = this;
				while(cur != null) {
					Match_Food2Ant next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_Food2Ant that)
			{
				_node_lastAnt = that._node_lastAnt;
				_node_hill = that._node_hill;
			}

			public Match_Food2Ant(Match_Food2Ant that)
			{
				CopyMatchContent(that);
			}
			public Match_Food2Ant()
			{
			}

			public bool IsEqual(Match_Food2Ant that)
			{
				if(that==null) return false;
				if(_node_lastAnt != that._node_lastAnt) return false;
				if(_node_hill != that._node_hill) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_lastAnt(List<IMatch_Food2Ant> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_Food2Ant match in matchList)
					resultList.Add(match.node_lastAnt);
				return resultList;
			}
			public static List<GRGEN_MODEL.IAntHill> Extract_hill(List<IMatch_Food2Ant> matchList)
			{
				List<GRGEN_MODEL.IAntHill> resultList = new List<GRGEN_MODEL.IAntHill>(matchList.Count);
				foreach(IMatch_Food2Ant match in matchList)
					resultList.Add(match.node_hill);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_EvaporateWorld : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_EvaporateWorld instance = null;
		public static Rule_EvaporateWorld Instance { get { if (instance==null) { instance = new Rule_EvaporateWorld(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] EvaporateWorld_node_n_AllowedTypes = { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridCornerNode.typeVar, };
		public static bool[] EvaporateWorld_node_n_IsAllowedType = { false, true, true, false, false, };
		public enum EvaporateWorld_NodeNums { @n, };
		public enum EvaporateWorld_EdgeNums { };
		public enum EvaporateWorld_VariableNums { };
		public enum EvaporateWorld_SubNums { };
		public enum EvaporateWorld_AltNums { };
		public enum EvaporateWorld_IterNums { };






		public GRGEN_LGSP.PatternGraph pat_EvaporateWorld;


		private Rule_EvaporateWorld()
			: base("EvaporateWorld",
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
			bool[,] EvaporateWorld_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] EvaporateWorld_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] EvaporateWorld_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] EvaporateWorld_isEdgeTotallyHomomorphic = new bool[0];
			GRGEN_LGSP.PatternNode EvaporateWorld_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, GRGEN_MODEL.NodeType_GridNode.typeVar, "GRGEN_MODEL.IGridNode", "EvaporateWorld_node_n", "n", EvaporateWorld_node_n_AllowedTypes, EvaporateWorld_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, null, false,null);
			pat_EvaporateWorld = new GRGEN_LGSP.PatternGraph(
				"EvaporateWorld",
				"",
				null, "EvaporateWorld",
				false, false,
				new GRGEN_LGSP.PatternNode[] { EvaporateWorld_node_n }, 
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
				EvaporateWorld_isNodeHomomorphicGlobal,
				EvaporateWorld_isEdgeHomomorphicGlobal,
				EvaporateWorld_isNodeTotallyHomomorphic,
				EvaporateWorld_isEdgeTotallyHomomorphic
			);

			EvaporateWorld_node_n.pointOfDefinition = pat_EvaporateWorld;

			patternGraph = pat_EvaporateWorld;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_EvaporateWorld curMatch = (Match_EvaporateWorld)_curMatch;
			GRGEN_LGSP.LGSPNode node_n = curMatch._node_n;
			GRGEN_MODEL.IGridNode inode_n = curMatch.node_n;
			graph.SettingAddedNodeNames( EvaporateWorld_addedNodeNames );
			graph.SettingAddedEdgeNames( EvaporateWorld_addedEdgeNames );
			{ // eval_0
				int tempvar_0 = (int )((int) (((double) inode_n.@pheromones) * 0.95));
				graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_0, null);
				inode_n.@pheromones = tempvar_0;
				graph.ChangedNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones);
			}
			return;
		}
		private static string[] EvaporateWorld_addedNodeNames = new string[] {  };
		private static string[] EvaporateWorld_addedEdgeNames = new string[] {  };

		static Rule_EvaporateWorld() {
		}

		public interface IMatch_EvaporateWorld : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_n { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_EvaporateWorld : GRGEN_LGSP.MatchListElement<Match_EvaporateWorld>, IMatch_EvaporateWorld
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } set { _node_n = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum EvaporateWorld_NodeNums { @n, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)EvaporateWorld_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "n": return _node_n;
				default: return null;
				}
			}

			public enum EvaporateWorld_EdgeNums { END_OF_ENUM };
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

			public enum EvaporateWorld_VariableNums { END_OF_ENUM };
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

			public enum EvaporateWorld_SubNums { END_OF_ENUM };
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

			public enum EvaporateWorld_AltNums { END_OF_ENUM };
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

			public enum EvaporateWorld_IterNums { END_OF_ENUM };
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

			public enum EvaporateWorld_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_EvaporateWorld.instance.pat_EvaporateWorld; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_EvaporateWorld(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_EvaporateWorld nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_EvaporateWorld cur = this;
				while(cur != null) {
					Match_EvaporateWorld next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_EvaporateWorld that)
			{
				_node_n = that._node_n;
			}

			public Match_EvaporateWorld(Match_EvaporateWorld that)
			{
				CopyMatchContent(that);
			}
			public Match_EvaporateWorld()
			{
			}

			public bool IsEqual(Match_EvaporateWorld that)
			{
				if(that==null) return false;
				if(_node_n != that._node_n) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IGridNode> Extract_n(List<IMatch_EvaporateWorld> matchList)
			{
				List<GRGEN_MODEL.IGridNode> resultList = new List<GRGEN_MODEL.IGridNode>(matchList.Count);
				foreach(IMatch_EvaporateWorld match in matchList)
					resultList.Add(match.node_n);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
	}

	public class Rule_doAntWorld : GRGEN_LGSP.LGSPRulePattern
	{
		private static Rule_doAntWorld instance = null;
		public static Rule_doAntWorld Instance { get { if (instance==null) { instance = new Rule_doAntWorld(); instance.initialize(); } return instance; } }

		public static GRGEN_LIBGR.NodeType[] doAntWorld_node_firstAnt_AllowedTypes = null;
		public static bool[] doAntWorld_node_firstAnt_IsAllowedType = null;
		public enum doAntWorld_NodeNums { @firstAnt, };
		public enum doAntWorld_EdgeNums { };
		public enum doAntWorld_VariableNums { };
		public enum doAntWorld_SubNums { };
		public enum doAntWorld_AltNums { };
		public enum doAntWorld_IterNums { };





		public GRGEN_LGSP.PatternGraph pat_doAntWorld;


		private Rule_doAntWorld()
			: base("doAntWorld",
				new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, },
				new string[] { "doAntWorld_node_firstAnt", },
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
			bool[,] doAntWorld_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] doAntWorld_isEdgeHomomorphicGlobal = new bool[0, 0];
			bool[] doAntWorld_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] doAntWorld_isEdgeTotallyHomomorphic = new bool[0];
			GRGEN_LGSP.PatternNode doAntWorld_node_firstAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, GRGEN_MODEL.NodeType_Ant.typeVar, "GRGEN_MODEL.IAnt", "doAntWorld_node_firstAnt", "firstAnt", doAntWorld_node_firstAnt_AllowedTypes, doAntWorld_node_firstAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, null, false,null);
			pat_doAntWorld = new GRGEN_LGSP.PatternGraph(
				"doAntWorld",
				"",
				null, "doAntWorld",
				false, false,
				new GRGEN_LGSP.PatternNode[] { doAntWorld_node_firstAnt }, 
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
				doAntWorld_isNodeHomomorphicGlobal,
				doAntWorld_isEdgeHomomorphicGlobal,
				doAntWorld_isNodeTotallyHomomorphic,
				doAntWorld_isEdgeTotallyHomomorphic
			);

			doAntWorld_node_firstAnt.pointOfDefinition = null;

			patternGraph = pat_doAntWorld;
		}


		public void Modify(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch _curMatch)
		{
			GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
			Match_doAntWorld curMatch = (Match_doAntWorld)_curMatch;
			GRGEN_LGSP.LGSPNode node_firstAnt = curMatch._node_firstAnt;
			graph.SettingAddedNodeNames( doAntWorld_addedNodeNames );
			graph.SettingAddedEdgeNames( doAntWorld_addedEdgeNames );
			GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv = (GRGEN_LGSP.LGSPGraphProcessingEnvironment)actionEnv;
			ApplyXGRS_doAntWorld_0(procEnv, (GRGEN_MODEL.IAnt)node_firstAnt);
			return;
		}
		private static string[] doAntWorld_addedNodeNames = new string[] {  };
		private static string[] doAntWorld_addedEdgeNames = new string[] {  };

        public static bool ApplyXGRS_doAntWorld_0(GRGEN_LGSP.LGSPGraphProcessingEnvironment procEnv, GRGEN_MODEL.IAnt var_firstAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = procEnv.graph;
            GRGEN_LGSP.LGSPActions actions = procEnv.curActions;
            procEnv.DebugEntering("doAntWorld.exec_0", "(curAnt:Ant=firstAnt && ((TakeFood(curAnt) | GoHome(curAnt) || DropFood(curAnt) | ($[SearchAlongPheromones(curAnt)] || $[SearchAimless(curAnt)])) && (curAnt)=GetNextAnt(curAnt))* | ((cur:GridNode)=ReachedEndOfWorldAnywhere && ((cur,curOuter:GridNode)=GrowWorldFirstNotAtCorner(cur) || (cur,curOuter)=GrowWorldFirstAtCorner(cur)) && ((cur,curOuter)=GrowWorldNextNotAtCorner(cur,curOuter) || (cur,curOuter)=GrowWorldNextAtCorner(cur,curOuter))* && GrowWorldEnd(cur,curOuter)) | (curAnt)=Food2Ant(curAnt)* | [EvaporateWorld])[50]");
            bool res_75;
            bool res_74;
            bool res_28;
            GRGEN_MODEL.IAnt var_curAnt = null;
            bool res_73;
            bool res_71;
            bool res_67;
            bool res_46;
            bool res_45;
            bool res_42;
            bool res_33;
            bool res_30;
            GRGEN_ACTIONS.Action_TakeFood rule_TakeFood = GRGEN_ACTIONS.Action_TakeFood.Instance;
            bool res_32;
            GRGEN_ACTIONS.Action_GoHome rule_GoHome = GRGEN_ACTIONS.Action_GoHome.Instance;
            bool res_41;
            bool res_35;
            GRGEN_ACTIONS.Action_DropFood rule_DropFood = GRGEN_ACTIONS.Action_DropFood.Instance;
            bool res_40;
            bool res_37;
            GRGEN_ACTIONS.Action_SearchAlongPheromones rule_SearchAlongPheromones = GRGEN_ACTIONS.Action_SearchAlongPheromones.Instance;
            bool res_39;
            GRGEN_ACTIONS.Action_SearchAimless rule_SearchAimless = GRGEN_ACTIONS.Action_SearchAimless.Instance;
            bool res_44;
            GRGEN_ACTIONS.Action_GetNextAnt rule_GetNextAnt = GRGEN_ACTIONS.Action_GetNextAnt.Instance;
            bool res_66;
            bool res_62;
            bool res_53;
            bool res_47;
            GRGEN_ACTIONS.Action_ReachedEndOfWorldAnywhere rule_ReachedEndOfWorldAnywhere = GRGEN_ACTIONS.Action_ReachedEndOfWorldAnywhere.Instance;
            GRGEN_MODEL.IGridNode var_cur = null;
            bool res_52;
            bool res_49;
            GRGEN_ACTIONS.Action_GrowWorldFirstNotAtCorner rule_GrowWorldFirstNotAtCorner = GRGEN_ACTIONS.Action_GrowWorldFirstNotAtCorner.Instance;
            GRGEN_MODEL.IGridNode var_curOuter = null;
            bool res_51;
            GRGEN_ACTIONS.Action_GrowWorldFirstAtCorner rule_GrowWorldFirstAtCorner = GRGEN_ACTIONS.Action_GrowWorldFirstAtCorner.Instance;
            bool res_61;
            bool res_60;
            bool res_56;
            GRGEN_ACTIONS.Action_GrowWorldNextNotAtCorner rule_GrowWorldNextNotAtCorner = GRGEN_ACTIONS.Action_GrowWorldNextNotAtCorner.Instance;
            bool res_59;
            GRGEN_ACTIONS.Action_GrowWorldNextAtCorner rule_GrowWorldNextAtCorner = GRGEN_ACTIONS.Action_GrowWorldNextAtCorner.Instance;
            bool res_65;
            GRGEN_ACTIONS.Action_GrowWorldEnd rule_GrowWorldEnd = GRGEN_ACTIONS.Action_GrowWorldEnd.Instance;
            bool res_70;
            bool res_69;
            GRGEN_ACTIONS.Action_Food2Ant rule_Food2Ant = GRGEN_ACTIONS.Action_Food2Ant.Instance;
            bool res_72;
            GRGEN_ACTIONS.Action_EvaporateWorld rule_EvaporateWorld = GRGEN_ACTIONS.Action_EvaporateWorld.Instance;
            long i_75 = 0;
            for(; i_75 < 50; ++i_75)
            {
                var_curAnt = (GRGEN_MODEL.IAnt)(var_firstAnt);
                res_28 = (bool)(true);
                if(!res_28)
                    res_74 = (bool)(false);
                else
                {
                    long i_46 = 0;
                    while(true)
                    {
                        GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_TakeFood.IMatch_TakeFood> matches_30 = rule_TakeFood.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                        procEnv.PerformanceInfo.MatchesFound += matches_30.Count;
                        if(matches_30.Count == 0) {
                            res_30 = (bool)(false);
                        } else {
                            res_30 = (bool)(true);
                            procEnv.Matched(matches_30, null, false);
                            procEnv.Finishing(matches_30, false);
                            GRGEN_ACTIONS.Rule_TakeFood.IMatch_TakeFood match_30 = matches_30.FirstExact;
                            rule_TakeFood.Modify(procEnv, match_30);
                            procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_30, false);
                        }
                        GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GoHome.IMatch_GoHome> matches_32 = rule_GoHome.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                        procEnv.PerformanceInfo.MatchesFound += matches_32.Count;
                        if(matches_32.Count == 0) {
                            res_32 = (bool)(false);
                        } else {
                            res_32 = (bool)(true);
                            procEnv.Matched(matches_32, null, false);
                            procEnv.Finishing(matches_32, false);
                            GRGEN_ACTIONS.Rule_GoHome.IMatch_GoHome match_32 = matches_32.FirstExact;
                            rule_GoHome.Modify(procEnv, match_32);
                            procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_32, false);
                        }
                        res_33 = (bool)(res_30 | res_32);
                        if(res_33)
                            res_42 = (bool)(true);
                        else
                        {
                            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_DropFood.IMatch_DropFood> matches_35 = rule_DropFood.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                            procEnv.PerformanceInfo.MatchesFound += matches_35.Count;
                            if(matches_35.Count == 0) {
                                res_35 = (bool)(false);
                            } else {
                                res_35 = (bool)(true);
                                procEnv.Matched(matches_35, null, false);
                                procEnv.Finishing(matches_35, false);
                                GRGEN_ACTIONS.Rule_DropFood.IMatch_DropFood match_35 = matches_35.FirstExact;
                                rule_DropFood.Modify(procEnv, match_35);
                                procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_35, false);
                            }
                            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches_37 = rule_SearchAlongPheromones.Match(procEnv, procEnv.MaxMatches, (GRGEN_MODEL.IAnt)var_curAnt);
                            procEnv.PerformanceInfo.MatchesFound += matches_37.Count;
                            if(matches_37.Count == 0) {
                                res_37 = (bool)(false);
                            } else {
                                res_37 = (bool)(true);
                                procEnv.Matched(matches_37, null, false);
                                procEnv.Finishing(matches_37, false);
                                int numchooserandomvar_37 = (int)1;
                                if(matches_37.Count < numchooserandomvar_37) numchooserandomvar_37 = matches_37.Count;
                                for(int i = 0; i < numchooserandomvar_37; ++i)
                                {
                                    if(i != 0) procEnv.RewritingNextMatch();
                                    GRGEN_ACTIONS.Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match_37 = matches_37.RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(matches_37.Count));
                                    rule_SearchAlongPheromones.Modify(procEnv, match_37);
                                    procEnv.PerformanceInfo.RewritesPerformed++;
                                }
                                procEnv.Finished(matches_37, false);
                            }
                            if(res_37)
                                res_40 = (bool)(true);
                            else
                            {
                                GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_SearchAimless.IMatch_SearchAimless> matches_39 = rule_SearchAimless.Match(procEnv, procEnv.MaxMatches, (GRGEN_MODEL.IAnt)var_curAnt);
                                procEnv.PerformanceInfo.MatchesFound += matches_39.Count;
                                if(matches_39.Count == 0) {
                                    res_39 = (bool)(false);
                                } else {
                                    res_39 = (bool)(true);
                                    procEnv.Matched(matches_39, null, false);
                                    procEnv.Finishing(matches_39, false);
                                    int numchooserandomvar_39 = (int)1;
                                    if(matches_39.Count < numchooserandomvar_39) numchooserandomvar_39 = matches_39.Count;
                                    for(int i = 0; i < numchooserandomvar_39; ++i)
                                    {
                                        if(i != 0) procEnv.RewritingNextMatch();
                                        GRGEN_ACTIONS.Rule_SearchAimless.IMatch_SearchAimless match_39 = matches_39.RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(matches_39.Count));
                                        rule_SearchAimless.Modify(procEnv, match_39);
                                        procEnv.PerformanceInfo.RewritesPerformed++;
                                    }
                                    procEnv.Finished(matches_39, false);
                                }
                                res_40 = (bool)(res_39);
                            }
                            res_41 = (bool)(res_35 | res_40);
                            res_42 = (bool)(res_41);
                        }
                        if(!res_42)
                            res_45 = (bool)(false);
                        else
                        {
                            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GetNextAnt.IMatch_GetNextAnt> matches_44 = rule_GetNextAnt.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                            procEnv.PerformanceInfo.MatchesFound += matches_44.Count;
                            if(matches_44.Count == 0) {
                                res_44 = (bool)(false);
                            } else {
                                res_44 = (bool)(true);
                                procEnv.Matched(matches_44, null, false);
                                procEnv.Finishing(matches_44, false);
                                GRGEN_ACTIONS.Rule_GetNextAnt.IMatch_GetNextAnt match_44 = matches_44.FirstExact;
                                GRGEN_MODEL.IAnt tmpvar_0curAnt; 
                                rule_GetNextAnt.Modify(procEnv, match_44, out tmpvar_0curAnt);
                                var_curAnt = (GRGEN_MODEL.IAnt)(tmpvar_0curAnt);

                                procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_44, false);
                            }
                            res_45 = (bool)(res_44);
                        }
                        if(!res_45)
                        	break;
                        ++i_46;
                    }
                    res_46 = (bool)(i_46 >= 0);
                    GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches_47 = rule_ReachedEndOfWorldAnywhere.Match(procEnv, 1);
                    procEnv.PerformanceInfo.MatchesFound += matches_47.Count;
                    if(matches_47.Count == 0) {
                        res_47 = (bool)(false);
                    } else {
                        res_47 = (bool)(true);
                        procEnv.Matched(matches_47, null, false);
                        procEnv.Finishing(matches_47, false);
                        GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match_47 = matches_47.FirstExact;
                        GRGEN_MODEL.IGridNode tmpvar_1cur; 
                        rule_ReachedEndOfWorldAnywhere.Modify(procEnv, match_47, out tmpvar_1cur);
                        var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_1cur);

                        procEnv.PerformanceInfo.RewritesPerformed++;
                        procEnv.Finished(matches_47, false);
                    }
                    if(!res_47)
                        res_53 = (bool)(false);
                    else
                    {
                        GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches_49 = rule_GrowWorldFirstNotAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur);
                        procEnv.PerformanceInfo.MatchesFound += matches_49.Count;
                        if(matches_49.Count == 0) {
                            res_49 = (bool)(false);
                        } else {
                            res_49 = (bool)(true);
                            procEnv.Matched(matches_49, null, false);
                            procEnv.Finishing(matches_49, false);
                            GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match_49 = matches_49.FirstExact;
                            GRGEN_MODEL.IGridNode tmpvar_2cur; GRGEN_MODEL.IGridNode tmpvar_3curOuter; 
                            rule_GrowWorldFirstNotAtCorner.Modify(procEnv, match_49, out tmpvar_2cur, out tmpvar_3curOuter);
                            var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_2cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_3curOuter);

                            procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_49, false);
                        }
                        if(res_49)
                            res_52 = (bool)(true);
                        else
                        {
                            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches_51 = rule_GrowWorldFirstAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur);
                            procEnv.PerformanceInfo.MatchesFound += matches_51.Count;
                            if(matches_51.Count == 0) {
                                res_51 = (bool)(false);
                            } else {
                                res_51 = (bool)(true);
                                procEnv.Matched(matches_51, null, false);
                                procEnv.Finishing(matches_51, false);
                                GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match_51 = matches_51.FirstExact;
                                GRGEN_MODEL.IGridNode tmpvar_4cur; GRGEN_MODEL.IGridNode tmpvar_5curOuter; 
                                rule_GrowWorldFirstAtCorner.Modify(procEnv, match_51, out tmpvar_4cur, out tmpvar_5curOuter);
                                var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_4cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_5curOuter);

                                procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_51, false);
                            }
                            res_52 = (bool)(res_51);
                        }
                        res_53 = (bool)(res_52);
                    }
                    if(!res_53)
                        res_62 = (bool)(false);
                    else
                    {
                        long i_61 = 0;
                        while(true)
                        {
                            GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches_56 = rule_GrowWorldNextNotAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur, (GRGEN_MODEL.IGridNode)var_curOuter);
                            procEnv.PerformanceInfo.MatchesFound += matches_56.Count;
                            if(matches_56.Count == 0) {
                                res_56 = (bool)(false);
                            } else {
                                res_56 = (bool)(true);
                                procEnv.Matched(matches_56, null, false);
                                procEnv.Finishing(matches_56, false);
                                GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match_56 = matches_56.FirstExact;
                                GRGEN_MODEL.IGridNode tmpvar_6cur; GRGEN_MODEL.IGridNode tmpvar_7curOuter; 
                                rule_GrowWorldNextNotAtCorner.Modify(procEnv, match_56, out tmpvar_6cur, out tmpvar_7curOuter);
                                var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_6cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_7curOuter);

                                procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_56, false);
                            }
                            if(res_56)
                                res_60 = (bool)(true);
                            else
                            {
                                GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches_59 = rule_GrowWorldNextAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur, (GRGEN_MODEL.IGridNode)var_curOuter);
                                procEnv.PerformanceInfo.MatchesFound += matches_59.Count;
                                if(matches_59.Count == 0) {
                                    res_59 = (bool)(false);
                                } else {
                                    res_59 = (bool)(true);
                                    procEnv.Matched(matches_59, null, false);
                                    procEnv.Finishing(matches_59, false);
                                    GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match_59 = matches_59.FirstExact;
                                    GRGEN_MODEL.IGridNode tmpvar_8cur; GRGEN_MODEL.IGridNode tmpvar_9curOuter; 
                                    rule_GrowWorldNextAtCorner.Modify(procEnv, match_59, out tmpvar_8cur, out tmpvar_9curOuter);
                                    var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_8cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_9curOuter);

                                    procEnv.PerformanceInfo.RewritesPerformed++;
                                    procEnv.Finished(matches_59, false);
                                }
                                res_60 = (bool)(res_59);
                            }
                            if(!res_60)
                            	break;
                            ++i_61;
                        }
                        res_61 = (bool)(i_61 >= 0);
                        res_62 = (bool)(res_61);
                    }
                    if(!res_62)
                        res_66 = (bool)(false);
                    else
                    {
                        GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches_65 = rule_GrowWorldEnd.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur, (GRGEN_MODEL.IGridNode)var_curOuter);
                        procEnv.PerformanceInfo.MatchesFound += matches_65.Count;
                        if(matches_65.Count == 0) {
                            res_65 = (bool)(false);
                        } else {
                            res_65 = (bool)(true);
                            procEnv.Matched(matches_65, null, false);
                            procEnv.Finishing(matches_65, false);
                            GRGEN_ACTIONS.Rule_GrowWorldEnd.IMatch_GrowWorldEnd match_65 = matches_65.FirstExact;
                            rule_GrowWorldEnd.Modify(procEnv, match_65);
                            procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_65, false);
                        }
                        res_66 = (bool)(res_65);
                    }
                    res_67 = (bool)(res_46 | res_66);
                    long i_70 = 0;
                    while(true)
                    {
                        GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_Food2Ant.IMatch_Food2Ant> matches_69 = rule_Food2Ant.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                        procEnv.PerformanceInfo.MatchesFound += matches_69.Count;
                        if(matches_69.Count == 0) {
                            res_69 = (bool)(false);
                        } else {
                            res_69 = (bool)(true);
                            procEnv.Matched(matches_69, null, false);
                            procEnv.Finishing(matches_69, false);
                            GRGEN_ACTIONS.Rule_Food2Ant.IMatch_Food2Ant match_69 = matches_69.FirstExact;
                            GRGEN_MODEL.IAnt tmpvar_10curAnt; 
                            rule_Food2Ant.Modify(procEnv, match_69, out tmpvar_10curAnt);
                            var_curAnt = (GRGEN_MODEL.IAnt)(tmpvar_10curAnt);

                            procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_69, false);
                        }
                        if(!res_69)
                        	break;
                        ++i_70;
                    }
                    res_70 = (bool)(i_70 >= 0);
                    res_71 = (bool)(res_67 | res_70);
                    GRGEN_LIBGR.IMatchesExact<GRGEN_ACTIONS.Rule_EvaporateWorld.IMatch_EvaporateWorld> matches_72 = rule_EvaporateWorld.Match(procEnv, procEnv.MaxMatches);
                    procEnv.PerformanceInfo.MatchesFound += matches_72.Count;
                    if(matches_72.Count == 0) {
                        res_72 = (bool)(false);
                    } else {
                        res_72 = (bool)(true);
                        procEnv.Matched(matches_72, null, false);
                        procEnv.Finishing(matches_72, false);
                        IEnumerator<GRGEN_ACTIONS.Rule_EvaporateWorld.IMatch_EvaporateWorld> enum_72 = matches_72.GetEnumeratorExact();
                        while(enum_72.MoveNext())
                        {
                            GRGEN_ACTIONS.Rule_EvaporateWorld.IMatch_EvaporateWorld match_72 = enum_72.Current;
                            if(match_72!=matches_72.FirstExact) procEnv.RewritingNextMatch();
                            rule_EvaporateWorld.Modify(procEnv, match_72);
                            procEnv.PerformanceInfo.RewritesPerformed++;
                        }
                        procEnv.Finished(matches_72, false);
                    }
                    res_73 = (bool)(res_71 | res_72);
                    res_74 = (bool)(res_73);
                }
                if(!res_74)
                	break;
            }
            res_75 = (bool)(i_75 >= 50);
            procEnv.DebugExiting("doAntWorld.exec_0");
            return res_75;
        }

		static Rule_doAntWorld() {
		}

		public interface IMatch_doAntWorld : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_firstAnt { get; set; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_doAntWorld : GRGEN_LGSP.MatchListElement<Match_doAntWorld>, IMatch_doAntWorld
		{
			public GRGEN_MODEL.IAnt node_firstAnt { get { return (GRGEN_MODEL.IAnt)_node_firstAnt; } set { _node_firstAnt = (GRGEN_LGSP.LGSPNode)value; } }
			public GRGEN_LGSP.LGSPNode _node_firstAnt;
			public enum doAntWorld_NodeNums { @firstAnt, END_OF_ENUM };
			public override IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public override IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public override int NumberOfNodes { get { return 1;} }
			public override GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)doAntWorld_NodeNums.@firstAnt: return _node_firstAnt;
				default: return null;
				}
			}
			public override GRGEN_LIBGR.INode getNode(string name)
			{
				switch(name) {
				case "firstAnt": return _node_firstAnt;
				default: return null;
				}
			}

			public enum doAntWorld_EdgeNums { END_OF_ENUM };
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

			public enum doAntWorld_VariableNums { END_OF_ENUM };
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

			public enum doAntWorld_SubNums { END_OF_ENUM };
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

			public enum doAntWorld_AltNums { END_OF_ENUM };
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

			public enum doAntWorld_IterNums { END_OF_ENUM };
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

			public enum doAntWorld_IdptNums { END_OF_ENUM };
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

			public override GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_doAntWorld.instance.pat_doAntWorld; } }
			public override GRGEN_LIBGR.IMatch Clone() { return new Match_doAntWorld(this); }
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public Match_doAntWorld nextWithSameHash;
			public void CleanNextWithSameHash() {
				Match_doAntWorld cur = this;
				while(cur != null) {
					Match_doAntWorld next = cur.nextWithSameHash;
					cur.nextWithSameHash = null;
					cur = next;
				}
			}

			public void CopyMatchContent(Match_doAntWorld that)
			{
				_node_firstAnt = that._node_firstAnt;
			}

			public Match_doAntWorld(Match_doAntWorld that)
			{
				CopyMatchContent(that);
			}
			public Match_doAntWorld()
			{
			}

			public bool IsEqual(Match_doAntWorld that)
			{
				if(that==null) return false;
				if(_node_firstAnt != that._node_firstAnt) return false;
				return true;
			}
		}


		public class Extractor
		{
			public static List<GRGEN_MODEL.IAnt> Extract_firstAnt(List<IMatch_doAntWorld> matchList)
			{
				List<GRGEN_MODEL.IAnt> resultList = new List<GRGEN_MODEL.IAnt>(matchList.Count);
				foreach(IMatch_doAntWorld match in matchList)
					resultList.Add(match.node_firstAnt);
				return resultList;
			}
		}

	}

	public partial class MatchFilters
	{
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

	public partial class MatchClassFilters
	{

		static MatchClassFilters() {
		}

	}



	//-----------------------------------------------------------

	public class AntWorld_ExtendAtEndOfRound_NoGammel_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public AntWorld_ExtendAtEndOfRound_NoGammel_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[18];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+18];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
			functions = new GRGEN_LIBGR.FunctionInfo[0+0];
			procedures = new GRGEN_LIBGR.ProcedureInfo[0+0];
			matchClasses = new GRGEN_LIBGR.MatchClassInfo[0];
			packages = new string[0];
			rules[0] = Rule_InitWorld.Instance;
			rulesAndSubpatterns[0+0] = Rule_InitWorld.Instance;
			rules[1] = Rule_TakeFood.Instance;
			rulesAndSubpatterns[0+1] = Rule_TakeFood.Instance;
			rules[2] = Rule_GoHome.Instance;
			rulesAndSubpatterns[0+2] = Rule_GoHome.Instance;
			rules[3] = Rule_DropFood.Instance;
			rulesAndSubpatterns[0+3] = Rule_DropFood.Instance;
			rules[4] = Rule_SearchAlongPheromones.Instance;
			rulesAndSubpatterns[0+4] = Rule_SearchAlongPheromones.Instance;
			rules[5] = Rule_SearchAimless.Instance;
			rulesAndSubpatterns[0+5] = Rule_SearchAimless.Instance;
			rules[6] = Rule_ReachedEndOfWorld.Instance;
			rulesAndSubpatterns[0+6] = Rule_ReachedEndOfWorld.Instance;
			rules[7] = Rule_ReachedEndOfWorldAnywhere.Instance;
			rulesAndSubpatterns[0+7] = Rule_ReachedEndOfWorldAnywhere.Instance;
			rules[8] = Rule_GrowFoodIfEqual.Instance;
			rulesAndSubpatterns[0+8] = Rule_GrowFoodIfEqual.Instance;
			rules[9] = Rule_GrowWorldFirstAtCorner.Instance;
			rulesAndSubpatterns[0+9] = Rule_GrowWorldFirstAtCorner.Instance;
			rules[10] = Rule_GrowWorldFirstNotAtCorner.Instance;
			rulesAndSubpatterns[0+10] = Rule_GrowWorldFirstNotAtCorner.Instance;
			rules[11] = Rule_GrowWorldNextAtCorner.Instance;
			rulesAndSubpatterns[0+11] = Rule_GrowWorldNextAtCorner.Instance;
			rules[12] = Rule_GrowWorldNextNotAtCorner.Instance;
			rulesAndSubpatterns[0+12] = Rule_GrowWorldNextNotAtCorner.Instance;
			rules[13] = Rule_GrowWorldEnd.Instance;
			rulesAndSubpatterns[0+13] = Rule_GrowWorldEnd.Instance;
			rules[14] = Rule_GetNextAnt.Instance;
			rulesAndSubpatterns[0+14] = Rule_GetNextAnt.Instance;
			rules[15] = Rule_Food2Ant.Instance;
			rulesAndSubpatterns[0+15] = Rule_Food2Ant.Instance;
			rules[16] = Rule_EvaporateWorld.Instance;
			rulesAndSubpatterns[0+16] = Rule_EvaporateWorld.Instance;
			rules[17] = Rule_doAntWorld.Instance;
			rulesAndSubpatterns[0+17] = Rule_doAntWorld.Instance;
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
    public interface IAction_InitWorld
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_InitWorld.IMatch_InitWorld match, out GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches, List<GRGEN_MODEL.IAnt> output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_MODEL.IAnt> output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_InitWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_InitWorld
    {
        public Action_InitWorld()
            : base(Rule_InitWorld.Instance.patternGraph, new object[1])
        {
            _rulePattern = Rule_InitWorld.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_InitWorld.Match_InitWorld, Rule_InitWorld.IMatch_InitWorld>(this);
        }

        public Rule_InitWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "InitWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_InitWorld.Match_InitWorld, Rule_InitWorld.IMatch_InitWorld> matches;

        public static Action_InitWorld Instance { get { return instance; } set { instance = value; } }
        private static Action_InitWorld instance = new Action_InitWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            Rule_InitWorld.Match_InitWorld match = matches.GetNextUnfilledPosition();
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IAnt> output_list_0 = new List<GRGEN_MODEL.IAnt>();
        public GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_InitWorld.IMatch_InitWorld match, out GRGEN_MODEL.IAnt output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches, List<GRGEN_MODEL.IAnt> output_0)
        {
            foreach(Rule_InitWorld.IMatch_InitWorld match in matches)
            {
                GRGEN_MODEL.IAnt output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_MODEL.IAnt> output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_InitWorld.IMatch_InitWorld match in matches)
            {
                GRGEN_MODEL.IAnt output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches;
            GRGEN_MODEL.IAnt output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IAnt output_0; 
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
            GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches;
            GRGEN_MODEL.IAnt output_0; 
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
            GRGEN_MODEL.IAnt output_0; 
            Modify(actionEnv, (Rule_InitWorld.IMatch_InitWorld)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld>)matches, output_list_0);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
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
        public static List<GRGEN_ACTIONS.Rule_InitWorld.IMatch_InitWorld> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_InitWorld.IMatch_InitWorld>)
            	return ((List<GRGEN_ACTIONS.Rule_InitWorld.IMatch_InitWorld>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_InitWorld.IMatch_InitWorld>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_TakeFood
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_TakeFood.IMatch_TakeFood match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
    }
    
    public class Action_TakeFood : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_TakeFood
    {
        public Action_TakeFood()
            : base(Rule_TakeFood.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_TakeFood.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_TakeFood.Match_TakeFood, Rule_TakeFood.IMatch_TakeFood>(this);
        }

        public Rule_TakeFood _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "TakeFood"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_TakeFood.Match_TakeFood, Rule_TakeFood.IMatch_TakeFood> matches;

        public static Action_TakeFood Instance { get { return instance; } set { instance = value; } }
        private static Action_TakeFood instance = new Action_TakeFood();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset TakeFood_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_TakeFood_node_curAnt = (GRGEN_LGSP.LGSPNode)TakeFood_node_curAnt;
            if(candidate_TakeFood_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            // Extend Outgoing TakeFood_edge__edge0 from TakeFood_node_curAnt 
            GRGEN_LGSP.LGSPEdge head_candidate_TakeFood_edge__edge0 = candidate_TakeFood_node_curAnt.lgspOuthead;
            if(head_candidate_TakeFood_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_TakeFood_edge__edge0 = head_candidate_TakeFood_edge__edge0;
                do
                {
                    if(candidate_TakeFood_edge__edge0.lgspType.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target TakeFood_node_n from TakeFood_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_TakeFood_node_n = candidate_TakeFood_edge__edge0.lgspTarget;
                    if(candidate_TakeFood_node_n.lgspType.TypeID!=1 && candidate_TakeFood_node_n.lgspType.TypeID!=2) {
                        continue;
                    }
                    // Condition 
                    if(!((!((GRGEN_MODEL.IAnt)candidate_TakeFood_node_curAnt).@hasFood && (((GRGEN_MODEL.IGridNode)candidate_TakeFood_node_n).@food > 0)))) {
                        continue;
                    }
                    Rule_TakeFood.Match_TakeFood match = matches.GetNextUnfilledPosition();
                    match._node_curAnt = candidate_TakeFood_node_curAnt;
                    match._node_n = candidate_TakeFood_node_n;
                    match._edge__edge0 = candidate_TakeFood_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_TakeFood_node_curAnt.MoveOutHeadAfter(candidate_TakeFood_edge__edge0);
                        return matches;
                    }
                }
                while( (candidate_TakeFood_edge__edge0 = candidate_TakeFood_edge__edge0.lgspOutNext) != head_candidate_TakeFood_edge__edge0 );
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, TakeFood_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_TakeFood.IMatch_TakeFood match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches)
        {
            foreach(Rule_TakeFood.IMatch_TakeFood match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, TakeFood_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, TakeFood_node_curAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_TakeFood.IMatch_TakeFood match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, TakeFood_node_curAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, TakeFood_node_curAnt);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, TakeFood_node_curAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, TakeFood_node_curAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_TakeFood.IMatch_TakeFood)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_TakeFood.IMatch_TakeFood> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_TakeFood.IMatch_TakeFood>)
            	return ((List<GRGEN_ACTIONS.Rule_TakeFood.IMatch_TakeFood>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_TakeFood.IMatch_TakeFood>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GoHome
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GoHome.IMatch_GoHome match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt GoHome_node_curAnt);
    }
    
    public class Action_GoHome : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GoHome
    {
        public Action_GoHome()
            : base(Rule_GoHome.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_GoHome.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GoHome.Match_GoHome, Rule_GoHome.IMatch_GoHome>(this);
        }

        public Rule_GoHome _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GoHome"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GoHome.Match_GoHome, Rule_GoHome.IMatch_GoHome> matches;

        public static Action_GoHome Instance { get { return instance; } set { instance = value; } }
        private static Action_GoHome instance = new Action_GoHome();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset GoHome_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_GoHome_node_curAnt = (GRGEN_LGSP.LGSPNode)GoHome_node_curAnt;
            if(candidate_GoHome_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            // Condition 
            if(!(((GRGEN_MODEL.IAnt)candidate_GoHome_node_curAnt).@hasFood)) {
                return matches;
            }
            // Extend Outgoing GoHome_edge_oldPos from GoHome_node_curAnt 
            GRGEN_LGSP.LGSPEdge head_candidate_GoHome_edge_oldPos = candidate_GoHome_node_curAnt.lgspOuthead;
            if(head_candidate_GoHome_edge_oldPos != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_GoHome_edge_oldPos = head_candidate_GoHome_edge_oldPos;
                do
                {
                    if(candidate_GoHome_edge_oldPos.lgspType.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target GoHome_node_old from GoHome_edge_oldPos 
                    GRGEN_LGSP.LGSPNode candidate_GoHome_node_old = candidate_GoHome_edge_oldPos.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GoHome_node_old.lgspType.TypeID]) {
                        continue;
                    }
                    uint prev__candidate_GoHome_node_old;
                    prev__candidate_GoHome_node_old = candidate_GoHome_node_old.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_GoHome_node_old.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // Extend Outgoing GoHome_edge__edge0 from GoHome_node_old 
                    GRGEN_LGSP.LGSPEdge head_candidate_GoHome_edge__edge0 = candidate_GoHome_node_old.lgspOuthead;
                    if(head_candidate_GoHome_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_GoHome_edge__edge0 = head_candidate_GoHome_edge__edge0;
                        do
                        {
                            if(candidate_GoHome_edge__edge0.lgspType.TypeID!=4) {
                                continue;
                            }
                            // Implicit Target GoHome_node_new from GoHome_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_GoHome_node_new = candidate_GoHome_edge__edge0.lgspTarget;
                            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GoHome_node_new.lgspType.TypeID]) {
                                continue;
                            }
                            if((candidate_GoHome_node_new.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                            {
                                continue;
                            }
                            Rule_GoHome.Match_GoHome match = matches.GetNextUnfilledPosition();
                            match._node_curAnt = candidate_GoHome_node_curAnt;
                            match._node_old = candidate_GoHome_node_old;
                            match._node_new = candidate_GoHome_node_new;
                            match._edge_oldPos = candidate_GoHome_edge_oldPos;
                            match._edge__edge0 = candidate_GoHome_edge__edge0;
                            matches.PositionWasFilledFixIt();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_GoHome_node_old.MoveOutHeadAfter(candidate_GoHome_edge__edge0);
                                candidate_GoHome_node_curAnt.MoveOutHeadAfter(candidate_GoHome_edge_oldPos);
                                candidate_GoHome_node_old.lgspFlags = candidate_GoHome_node_old.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GoHome_node_old;
                                return matches;
                            }
                        }
                        while( (candidate_GoHome_edge__edge0 = candidate_GoHome_edge__edge0.lgspOutNext) != head_candidate_GoHome_edge__edge0 );
                    }
                    candidate_GoHome_node_old.lgspFlags = candidate_GoHome_node_old.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GoHome_node_old;
                }
                while( (candidate_GoHome_edge_oldPos = candidate_GoHome_edge_oldPos.lgspOutNext) != head_candidate_GoHome_edge_oldPos );
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GoHome_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GoHome.IMatch_GoHome match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches)
        {
            foreach(Rule_GoHome.IMatch_GoHome match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GoHome_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GoHome_node_curAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GoHome.IMatch_GoHome match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GoHome_node_curAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GoHome_node_curAnt);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GoHome_node_curAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GoHome_node_curAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_GoHome.IMatch_GoHome)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_GoHome.IMatch_GoHome> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GoHome.IMatch_GoHome>)
            	return ((List<GRGEN_ACTIONS.Rule_GoHome.IMatch_GoHome>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GoHome.IMatch_GoHome>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_DropFood
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_DropFood.IMatch_DropFood match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt DropFood_node_curAnt);
    }
    
    public class Action_DropFood : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_DropFood
    {
        public Action_DropFood()
            : base(Rule_DropFood.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_DropFood.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_DropFood.Match_DropFood, Rule_DropFood.IMatch_DropFood>(this);
        }

        public Rule_DropFood _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "DropFood"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_DropFood.Match_DropFood, Rule_DropFood.IMatch_DropFood> matches;

        public static Action_DropFood Instance { get { return instance; } set { instance = value; } }
        private static Action_DropFood instance = new Action_DropFood();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset DropFood_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_DropFood_node_curAnt = (GRGEN_LGSP.LGSPNode)DropFood_node_curAnt;
            if(candidate_DropFood_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            // Condition 
            if(!(((GRGEN_MODEL.IAnt)candidate_DropFood_node_curAnt).@hasFood)) {
                return matches;
            }
            // Extend Outgoing DropFood_edge__edge0 from DropFood_node_curAnt 
            GRGEN_LGSP.LGSPEdge head_candidate_DropFood_edge__edge0 = candidate_DropFood_node_curAnt.lgspOuthead;
            if(head_candidate_DropFood_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_DropFood_edge__edge0 = head_candidate_DropFood_edge__edge0;
                do
                {
                    if(candidate_DropFood_edge__edge0.lgspType.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target DropFood_node_hill from DropFood_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_DropFood_node_hill = candidate_DropFood_edge__edge0.lgspTarget;
                    if(candidate_DropFood_node_hill.lgspType.TypeID!=3) {
                        continue;
                    }
                    Rule_DropFood.Match_DropFood match = matches.GetNextUnfilledPosition();
                    match._node_curAnt = candidate_DropFood_node_curAnt;
                    match._node_hill = candidate_DropFood_node_hill;
                    match._edge__edge0 = candidate_DropFood_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_DropFood_node_curAnt.MoveOutHeadAfter(candidate_DropFood_edge__edge0);
                        return matches;
                    }
                }
                while( (candidate_DropFood_edge__edge0 = candidate_DropFood_edge__edge0.lgspOutNext) != head_candidate_DropFood_edge__edge0 );
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, DropFood_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_DropFood.IMatch_DropFood match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches)
        {
            foreach(Rule_DropFood.IMatch_DropFood match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, DropFood_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, DropFood_node_curAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_DropFood.IMatch_DropFood match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, DropFood_node_curAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, DropFood_node_curAnt);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, DropFood_node_curAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, DropFood_node_curAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_DropFood.IMatch_DropFood)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_DropFood.IMatch_DropFood> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_DropFood.IMatch_DropFood>)
            	return ((List<GRGEN_ACTIONS.Rule_DropFood.IMatch_DropFood>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_DropFood.IMatch_DropFood>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_SearchAlongPheromones
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
    }
    
    public class Action_SearchAlongPheromones : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_SearchAlongPheromones
    {
        public Action_SearchAlongPheromones()
            : base(Rule_SearchAlongPheromones.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_SearchAlongPheromones.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_SearchAlongPheromones.Match_SearchAlongPheromones, Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones>(this);
        }

        public Rule_SearchAlongPheromones _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "SearchAlongPheromones"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_SearchAlongPheromones.Match_SearchAlongPheromones, Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches;

        public static Action_SearchAlongPheromones Instance { get { return instance; } set { instance = value; } }
        private static Action_SearchAlongPheromones instance = new Action_SearchAlongPheromones();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset SearchAlongPheromones_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_SearchAlongPheromones_node_curAnt = (GRGEN_LGSP.LGSPNode)SearchAlongPheromones_node_curAnt;
            if(candidate_SearchAlongPheromones_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            // Extend Outgoing SearchAlongPheromones_edge_oldPos from SearchAlongPheromones_node_curAnt 
            GRGEN_LGSP.LGSPEdge head_candidate_SearchAlongPheromones_edge_oldPos = candidate_SearchAlongPheromones_node_curAnt.lgspOuthead;
            if(head_candidate_SearchAlongPheromones_edge_oldPos != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_SearchAlongPheromones_edge_oldPos = head_candidate_SearchAlongPheromones_edge_oldPos;
                do
                {
                    if(candidate_SearchAlongPheromones_edge_oldPos.lgspType.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target SearchAlongPheromones_node_old from SearchAlongPheromones_edge_oldPos 
                    GRGEN_LGSP.LGSPNode candidate_SearchAlongPheromones_node_old = candidate_SearchAlongPheromones_edge_oldPos.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_SearchAlongPheromones_node_old.lgspType.TypeID]) {
                        continue;
                    }
                    uint prev__candidate_SearchAlongPheromones_node_old;
                    prev__candidate_SearchAlongPheromones_node_old = candidate_SearchAlongPheromones_node_old.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_SearchAlongPheromones_node_old.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // Extend Incoming SearchAlongPheromones_edge__edge0 from SearchAlongPheromones_node_old 
                    GRGEN_LGSP.LGSPEdge head_candidate_SearchAlongPheromones_edge__edge0 = candidate_SearchAlongPheromones_node_old.lgspInhead;
                    if(head_candidate_SearchAlongPheromones_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_SearchAlongPheromones_edge__edge0 = head_candidate_SearchAlongPheromones_edge__edge0;
                        do
                        {
                            if(candidate_SearchAlongPheromones_edge__edge0.lgspType.TypeID!=4) {
                                continue;
                            }
                            // Implicit Source SearchAlongPheromones_node_new from SearchAlongPheromones_edge__edge0 
                            GRGEN_LGSP.LGSPNode candidate_SearchAlongPheromones_node_new = candidate_SearchAlongPheromones_edge__edge0.lgspSource;
                            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_SearchAlongPheromones_node_new.lgspType.TypeID]) {
                                continue;
                            }
                            if((candidate_SearchAlongPheromones_node_new.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                            {
                                continue;
                            }
                            // Condition 
                            if(!((((GRGEN_MODEL.IGridNode)candidate_SearchAlongPheromones_node_new).@pheromones > 9))) {
                                continue;
                            }
                            Rule_SearchAlongPheromones.Match_SearchAlongPheromones match = matches.GetNextUnfilledPosition();
                            match._node_curAnt = candidate_SearchAlongPheromones_node_curAnt;
                            match._node_old = candidate_SearchAlongPheromones_node_old;
                            match._node_new = candidate_SearchAlongPheromones_node_new;
                            match._edge_oldPos = candidate_SearchAlongPheromones_edge_oldPos;
                            match._edge__edge0 = candidate_SearchAlongPheromones_edge__edge0;
                            matches.PositionWasFilledFixIt();
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && matches.Count >= maxMatches)
                            {
                                candidate_SearchAlongPheromones_node_old.MoveInHeadAfter(candidate_SearchAlongPheromones_edge__edge0);
                                candidate_SearchAlongPheromones_node_curAnt.MoveOutHeadAfter(candidate_SearchAlongPheromones_edge_oldPos);
                                candidate_SearchAlongPheromones_node_old.lgspFlags = candidate_SearchAlongPheromones_node_old.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_SearchAlongPheromones_node_old;
                                return matches;
                            }
                        }
                        while( (candidate_SearchAlongPheromones_edge__edge0 = candidate_SearchAlongPheromones_edge__edge0.lgspInNext) != head_candidate_SearchAlongPheromones_edge__edge0 );
                    }
                    candidate_SearchAlongPheromones_node_old.lgspFlags = candidate_SearchAlongPheromones_node_old.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_SearchAlongPheromones_node_old;
                }
                while( (candidate_SearchAlongPheromones_edge_oldPos = candidate_SearchAlongPheromones_edge_oldPos.lgspOutNext) != head_candidate_SearchAlongPheromones_edge_oldPos );
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, SearchAlongPheromones_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches)
        {
            foreach(Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAlongPheromones_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, SearchAlongPheromones_node_curAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAlongPheromones_node_curAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAlongPheromones_node_curAnt);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAlongPheromones_node_curAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAlongPheromones_node_curAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones>)
            	return ((List<GRGEN_ACTIONS.Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_SearchAimless
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_SearchAimless.IMatch_SearchAimless match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
    }
    
    public class Action_SearchAimless : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_SearchAimless
    {
        public Action_SearchAimless()
            : base(Rule_SearchAimless.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_SearchAimless.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_SearchAimless.Match_SearchAimless, Rule_SearchAimless.IMatch_SearchAimless>(this);
        }

        public Rule_SearchAimless _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "SearchAimless"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_SearchAimless.Match_SearchAimless, Rule_SearchAimless.IMatch_SearchAimless> matches;

        public static Action_SearchAimless Instance { get { return instance; } set { instance = value; } }
        private static Action_SearchAimless instance = new Action_SearchAimless();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset SearchAimless_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_SearchAimless_node_curAnt = (GRGEN_LGSP.LGSPNode)SearchAimless_node_curAnt;
            if(candidate_SearchAimless_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            // Extend Outgoing SearchAimless_edge_oldPos from SearchAimless_node_curAnt 
            GRGEN_LGSP.LGSPEdge head_candidate_SearchAimless_edge_oldPos = candidate_SearchAimless_node_curAnt.lgspOuthead;
            if(head_candidate_SearchAimless_edge_oldPos != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_SearchAimless_edge_oldPos = head_candidate_SearchAimless_edge_oldPos;
                do
                {
                    if(candidate_SearchAimless_edge_oldPos.lgspType.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target SearchAimless_node_old from SearchAimless_edge_oldPos 
                    GRGEN_LGSP.LGSPNode candidate_SearchAimless_node_old = candidate_SearchAimless_edge_oldPos.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_SearchAimless_node_old.lgspType.TypeID]) {
                        continue;
                    }
                    uint prev__candidate_SearchAimless_node_old;
                    prev__candidate_SearchAimless_node_old = candidate_SearchAimless_node_old.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_SearchAimless_node_old.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // both directions of SearchAimless_edge__edge0
                    for(int directionRunCounterOf_SearchAimless_edge__edge0 = 0; directionRunCounterOf_SearchAimless_edge__edge0 < 2; ++directionRunCounterOf_SearchAimless_edge__edge0)
                    {
                        // Extend IncomingOrOutgoing SearchAimless_edge__edge0 from SearchAimless_node_old 
                        GRGEN_LGSP.LGSPEdge head_candidate_SearchAimless_edge__edge0 = directionRunCounterOf_SearchAimless_edge__edge0==0 ? candidate_SearchAimless_node_old.lgspInhead : candidate_SearchAimless_node_old.lgspOuthead;
                        if(head_candidate_SearchAimless_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_SearchAimless_edge__edge0 = head_candidate_SearchAimless_edge__edge0;
                            do
                            {
                                if(candidate_SearchAimless_edge__edge0.lgspType.TypeID!=3 && candidate_SearchAimless_edge__edge0.lgspType.TypeID!=4) {
                                    continue;
                                }
                                // Implicit TheOther SearchAimless_node_new from SearchAimless_edge__edge0 
                                GRGEN_LGSP.LGSPNode candidate_SearchAimless_node_new = candidate_SearchAimless_node_old==candidate_SearchAimless_edge__edge0.lgspSource ? candidate_SearchAimless_edge__edge0.lgspTarget : candidate_SearchAimless_edge__edge0.lgspSource;
                                if(candidate_SearchAimless_node_new.lgspType.TypeID!=1 && candidate_SearchAimless_node_new.lgspType.TypeID!=2) {
                                    continue;
                                }
                                if((candidate_SearchAimless_node_new.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                                {
                                    continue;
                                }
                                Rule_SearchAimless.Match_SearchAimless match = matches.GetNextUnfilledPosition();
                                match._node_curAnt = candidate_SearchAimless_node_curAnt;
                                match._node_old = candidate_SearchAimless_node_old;
                                match._node_new = candidate_SearchAimless_node_new;
                                match._edge_oldPos = candidate_SearchAimless_edge_oldPos;
                                match._edge__edge0 = candidate_SearchAimless_edge__edge0;
                                matches.PositionWasFilledFixIt();
                                // if enough matches were found, we leave
                                if(maxMatches > 0 && matches.Count >= maxMatches)
                                {
                                    if(directionRunCounterOf_SearchAimless_edge__edge0==0) {
                                        candidate_SearchAimless_node_old.MoveInHeadAfter(candidate_SearchAimless_edge__edge0);
                                    } else {
                                        candidate_SearchAimless_node_old.MoveOutHeadAfter(candidate_SearchAimless_edge__edge0);
                                    }
                                    candidate_SearchAimless_node_curAnt.MoveOutHeadAfter(candidate_SearchAimless_edge_oldPos);
                                    candidate_SearchAimless_node_old.lgspFlags = candidate_SearchAimless_node_old.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_SearchAimless_node_old;
                                    return matches;
                                }
                            }
                            while( (directionRunCounterOf_SearchAimless_edge__edge0==0 ? candidate_SearchAimless_edge__edge0 = candidate_SearchAimless_edge__edge0.lgspInNext : candidate_SearchAimless_edge__edge0 = candidate_SearchAimless_edge__edge0.lgspOutNext) != head_candidate_SearchAimless_edge__edge0 );
                        }
                    }
                    candidate_SearchAimless_node_old.lgspFlags = candidate_SearchAimless_node_old.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_SearchAimless_node_old;
                }
                while( (candidate_SearchAimless_edge_oldPos = candidate_SearchAimless_edge_oldPos.lgspOutNext) != head_candidate_SearchAimless_edge_oldPos );
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, SearchAimless_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_SearchAimless.IMatch_SearchAimless match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches)
        {
            foreach(Rule_SearchAimless.IMatch_SearchAimless match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAimless_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, SearchAimless_node_curAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_SearchAimless.IMatch_SearchAimless match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAimless_node_curAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAimless_node_curAnt);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAimless_node_curAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAimless_node_curAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_SearchAimless.IMatch_SearchAimless)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_SearchAimless.IMatch_SearchAimless> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_SearchAimless.IMatch_SearchAimless>)
            	return ((List<GRGEN_ACTIONS.Rule_SearchAimless.IMatch_SearchAimless>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_SearchAimless.IMatch_SearchAimless>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_ReachedEndOfWorld
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld match, out GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches, List<GRGEN_MODEL.IGridNode> output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, ref GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, List<GRGEN_MODEL.IGridNode> output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
    }
    
    public class Action_ReachedEndOfWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ReachedEndOfWorld
    {
        public Action_ReachedEndOfWorld()
            : base(Rule_ReachedEndOfWorld.Instance.patternGraph, new object[1])
        {
            _rulePattern = Rule_ReachedEndOfWorld.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorld.Match_ReachedEndOfWorld, Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld>(this);
        }

        public Rule_ReachedEndOfWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ReachedEndOfWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorld.Match_ReachedEndOfWorld, Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches;

        public static Action_ReachedEndOfWorld Instance { get { return instance; } set { instance = value; } }
        private static Action_ReachedEndOfWorld instance = new Action_ReachedEndOfWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset ReachedEndOfWorld_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_ReachedEndOfWorld_node_curAnt = (GRGEN_LGSP.LGSPNode)ReachedEndOfWorld_node_curAnt;
            if(candidate_ReachedEndOfWorld_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            // Extend Outgoing ReachedEndOfWorld_edge__edge0 from ReachedEndOfWorld_node_curAnt 
            GRGEN_LGSP.LGSPEdge head_candidate_ReachedEndOfWorld_edge__edge0 = candidate_ReachedEndOfWorld_node_curAnt.lgspOuthead;
            if(head_candidate_ReachedEndOfWorld_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_ReachedEndOfWorld_edge__edge0 = head_candidate_ReachedEndOfWorld_edge__edge0;
                do
                {
                    if(candidate_ReachedEndOfWorld_edge__edge0.lgspType.TypeID!=5) {
                        continue;
                    }
                    // Implicit Target ReachedEndOfWorld_node_n from ReachedEndOfWorld_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_ReachedEndOfWorld_node_n = candidate_ReachedEndOfWorld_edge__edge0.lgspTarget;
                    if(candidate_ReachedEndOfWorld_node_n.lgspType.TypeID!=1 && candidate_ReachedEndOfWorld_node_n.lgspType.TypeID!=2) {
                        continue;
                    }
                    // NegativePattern 
                    {
                        ++isoSpace;
                        // Extend Incoming ReachedEndOfWorld_neg_0_edge__edge0 from ReachedEndOfWorld_node_n 
                        GRGEN_LGSP.LGSPEdge head_candidate_ReachedEndOfWorld_neg_0_edge__edge0 = candidate_ReachedEndOfWorld_node_n.lgspInhead;
                        if(head_candidate_ReachedEndOfWorld_neg_0_edge__edge0 != null)
                        {
                            GRGEN_LGSP.LGSPEdge candidate_ReachedEndOfWorld_neg_0_edge__edge0 = head_candidate_ReachedEndOfWorld_neg_0_edge__edge0;
                            do
                            {
                                if(candidate_ReachedEndOfWorld_neg_0_edge__edge0.lgspType.TypeID!=4) {
                                    continue;
                                }
                                // negative pattern found
                                --isoSpace;
                                goto label0;
                            }
                            while( (candidate_ReachedEndOfWorld_neg_0_edge__edge0 = candidate_ReachedEndOfWorld_neg_0_edge__edge0.lgspInNext) != head_candidate_ReachedEndOfWorld_neg_0_edge__edge0 );
                        }
                        --isoSpace;
                    }
                    Rule_ReachedEndOfWorld.Match_ReachedEndOfWorld match = matches.GetNextUnfilledPosition();
                    match._node_curAnt = candidate_ReachedEndOfWorld_node_curAnt;
                    match._node_n = candidate_ReachedEndOfWorld_node_n;
                    match._edge__edge0 = candidate_ReachedEndOfWorld_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_ReachedEndOfWorld_node_curAnt.MoveOutHeadAfter(candidate_ReachedEndOfWorld_edge__edge0);
                        return matches;
                    }
label0: ;
                }
                while( (candidate_ReachedEndOfWorld_edge__edge0 = candidate_ReachedEndOfWorld_edge__edge0.lgspOutNext) != head_candidate_ReachedEndOfWorld_edge__edge0 );
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IGridNode> output_list_0 = new List<GRGEN_MODEL.IGridNode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ReachedEndOfWorld_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld match, out GRGEN_MODEL.IGridNode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches, List<GRGEN_MODEL.IGridNode> output_0)
        {
            foreach(Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, ref GRGEN_MODEL.IGridNode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ReachedEndOfWorld_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, List<GRGEN_MODEL.IGridNode> output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ReachedEndOfWorld_node_curAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches;
            GRGEN_MODEL.IGridNode output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ReachedEndOfWorld_node_curAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ReachedEndOfWorld_node_curAnt);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IGridNode output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ReachedEndOfWorld_node_curAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches;
            GRGEN_MODEL.IGridNode output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ReachedEndOfWorld_node_curAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IGridNode output_0; 
            Modify(actionEnv, (Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld>)matches, output_list_0);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0], ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0], output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld>)
            	return ((List<GRGEN_ACTIONS.Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_ReachedEndOfWorldAnywhere
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match, out GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches, List<GRGEN_MODEL.IGridNode> output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_MODEL.IGridNode> output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_ReachedEndOfWorldAnywhere : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ReachedEndOfWorldAnywhere
    {
        public Action_ReachedEndOfWorldAnywhere()
            : base(Rule_ReachedEndOfWorldAnywhere.Instance.patternGraph, new object[1])
        {
            _rulePattern = Rule_ReachedEndOfWorldAnywhere.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorldAnywhere.Match_ReachedEndOfWorldAnywhere, Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere>(this);
        }

        public Rule_ReachedEndOfWorldAnywhere _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ReachedEndOfWorldAnywhere"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorldAnywhere.Match_ReachedEndOfWorldAnywhere, Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches;

        public static Action_ReachedEndOfWorldAnywhere Instance { get { return instance; } set { instance = value; } }
        private static Action_ReachedEndOfWorldAnywhere instance = new Action_ReachedEndOfWorldAnywhere();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup ReachedEndOfWorldAnywhere_edge__edge0 
            int type_id_candidate_ReachedEndOfWorldAnywhere_edge__edge0 = 5;
            for(GRGEN_LGSP.LGSPEdge head_candidate_ReachedEndOfWorldAnywhere_edge__edge0 = graph.edgesByTypeHeads[type_id_candidate_ReachedEndOfWorldAnywhere_edge__edge0], candidate_ReachedEndOfWorldAnywhere_edge__edge0 = head_candidate_ReachedEndOfWorldAnywhere_edge__edge0.lgspTypeNext; candidate_ReachedEndOfWorldAnywhere_edge__edge0 != head_candidate_ReachedEndOfWorldAnywhere_edge__edge0; candidate_ReachedEndOfWorldAnywhere_edge__edge0 = candidate_ReachedEndOfWorldAnywhere_edge__edge0.lgspTypeNext)
            {
                // Implicit Source ReachedEndOfWorldAnywhere_node__node0 from ReachedEndOfWorldAnywhere_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_ReachedEndOfWorldAnywhere_node__node0 = candidate_ReachedEndOfWorldAnywhere_edge__edge0.lgspSource;
                if(candidate_ReachedEndOfWorldAnywhere_node__node0.lgspType.TypeID!=4) {
                    continue;
                }
                // Implicit Target ReachedEndOfWorldAnywhere_node_n from ReachedEndOfWorldAnywhere_edge__edge0 
                GRGEN_LGSP.LGSPNode candidate_ReachedEndOfWorldAnywhere_node_n = candidate_ReachedEndOfWorldAnywhere_edge__edge0.lgspTarget;
                if(candidate_ReachedEndOfWorldAnywhere_node_n.lgspType.TypeID!=1 && candidate_ReachedEndOfWorldAnywhere_node_n.lgspType.TypeID!=2) {
                    continue;
                }
                // NegativePattern 
                {
                    ++isoSpace;
                    // Extend Incoming ReachedEndOfWorldAnywhere_neg_0_edge__edge0 from ReachedEndOfWorldAnywhere_node_n 
                    GRGEN_LGSP.LGSPEdge head_candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0 = candidate_ReachedEndOfWorldAnywhere_node_n.lgspInhead;
                    if(head_candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0 != null)
                    {
                        GRGEN_LGSP.LGSPEdge candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0 = head_candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0;
                        do
                        {
                            if(candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0.lgspType.TypeID!=4) {
                                continue;
                            }
                            // negative pattern found
                            --isoSpace;
                            goto label1;
                        }
                        while( (candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0 = candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0.lgspInNext) != head_candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0 );
                    }
                    --isoSpace;
                }
                Rule_ReachedEndOfWorldAnywhere.Match_ReachedEndOfWorldAnywhere match = matches.GetNextUnfilledPosition();
                match._node__node0 = candidate_ReachedEndOfWorldAnywhere_node__node0;
                match._node_n = candidate_ReachedEndOfWorldAnywhere_node_n;
                match._edge__edge0 = candidate_ReachedEndOfWorldAnywhere_edge__edge0;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_ReachedEndOfWorldAnywhere_edge__edge0);
                    return matches;
                }
label1: ;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IGridNode> output_list_0 = new List<GRGEN_MODEL.IGridNode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match, out GRGEN_MODEL.IGridNode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches, List<GRGEN_MODEL.IGridNode> output_0)
        {
            foreach(Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IGridNode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, List<GRGEN_MODEL.IGridNode> output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches;
            GRGEN_MODEL.IGridNode output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IGridNode output_0; 
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
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches;
            GRGEN_MODEL.IGridNode output_0; 
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
            GRGEN_MODEL.IGridNode output_0; 
            Modify(actionEnv, (Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere>)matches, output_list_0);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_MODEL.IGridNode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; 
            if(Apply(actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
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
        public static List<GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere>)
            	return ((List<GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GrowFoodIfEqual
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
    }
    
    public class Action_GrowFoodIfEqual : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowFoodIfEqual
    {
        public Action_GrowFoodIfEqual()
            : base(Rule_GrowFoodIfEqual.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_GrowFoodIfEqual.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowFoodIfEqual.Match_GrowFoodIfEqual, Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>(this);
        }

        public Rule_GrowFoodIfEqual _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowFoodIfEqual"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowFoodIfEqual.Match_GrowFoodIfEqual, Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches;

        public static Action_GrowFoodIfEqual Instance { get { return instance; } set { instance = value; } }
        private static Action_GrowFoodIfEqual instance = new Action_GrowFoodIfEqual();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            System.Int32 var_GrowFoodIfEqual_var_val = (System.Int32)GrowFoodIfEqual_var_val;
            // Preset GrowFoodIfEqual_node_n 
            GRGEN_LGSP.LGSPNode candidate_GrowFoodIfEqual_node_n = (GRGEN_LGSP.LGSPNode)GrowFoodIfEqual_node_n;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowFoodIfEqual_node_n.lgspType.TypeID]) {
                return matches;
            }
            uint prev__candidate_GrowFoodIfEqual_node_n;
            prev__candidate_GrowFoodIfEqual_node_n = candidate_GrowFoodIfEqual_node_n.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowFoodIfEqual_node_n.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Lookup GrowFoodIfEqual_node_hill 
            int type_id_candidate_GrowFoodIfEqual_node_hill = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_GrowFoodIfEqual_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowFoodIfEqual_node_hill], candidate_GrowFoodIfEqual_node_hill = head_candidate_GrowFoodIfEqual_node_hill.lgspTypeNext; candidate_GrowFoodIfEqual_node_hill != head_candidate_GrowFoodIfEqual_node_hill; candidate_GrowFoodIfEqual_node_hill = candidate_GrowFoodIfEqual_node_hill.lgspTypeNext)
            {
                if((candidate_GrowFoodIfEqual_node_hill.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                {
                    continue;
                }
                // Condition 
                if(!((((GRGEN_MODEL.IAntHill)candidate_GrowFoodIfEqual_node_hill).@foodCountdown == var_GrowFoodIfEqual_var_val))) {
                    continue;
                }
                Rule_GrowFoodIfEqual.Match_GrowFoodIfEqual match = matches.GetNextUnfilledPosition();
                match._node_n = candidate_GrowFoodIfEqual_node_n;
                match._node_hill = candidate_GrowFoodIfEqual_node_hill;
                match._var_val = var_GrowFoodIfEqual_var_val;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_GrowFoodIfEqual_node_hill);
                    candidate_GrowFoodIfEqual_node_n.lgspFlags = candidate_GrowFoodIfEqual_node_n.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowFoodIfEqual_node_n;
                    return matches;
                }
            }
            candidate_GrowFoodIfEqual_node_n.lgspFlags = candidate_GrowFoodIfEqual_node_n.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowFoodIfEqual_node_n;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches)
        {
            foreach(Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IGridNode) parameters[0], (System.Int32) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (System.Int32) parameters[1])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (System.Int32) parameters[1]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (System.Int32) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (System.Int32) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IGridNode) parameters[0], (System.Int32) parameters[1]);
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
        public static List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>)
            	return ((List<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GrowWorldFirstAtCorner
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
    }
    
    public class Action_GrowWorldFirstAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldFirstAtCorner
    {
        public Action_GrowWorldFirstAtCorner()
            : base(Rule_GrowWorldFirstAtCorner.Instance.patternGraph, new object[2])
        {
            _rulePattern = Rule_GrowWorldFirstAtCorner.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstAtCorner.Match_GrowWorldFirstAtCorner, Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner>(this);
        }

        public Rule_GrowWorldFirstAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldFirstAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstAtCorner.Match_GrowWorldFirstAtCorner, Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches;

        public static Action_GrowWorldFirstAtCorner Instance { get { return instance; } set { instance = value; } }
        private static Action_GrowWorldFirstAtCorner instance = new Action_GrowWorldFirstAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset GrowWorldFirstAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldFirstAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldFirstAtCorner_node_cur;
            if(candidate_GrowWorldFirstAtCorner_node_cur.lgspType.TypeID!=2) {
                return matches;
            }
            uint prev__candidate_GrowWorldFirstAtCorner_node_cur;
            prev__candidate_GrowWorldFirstAtCorner_node_cur = candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Extend Outgoing GrowWorldFirstAtCorner_edge__edge0 from GrowWorldFirstAtCorner_node_cur 
            GRGEN_LGSP.LGSPEdge head_candidate_GrowWorldFirstAtCorner_edge__edge0 = candidate_GrowWorldFirstAtCorner_node_cur.lgspOuthead;
            if(head_candidate_GrowWorldFirstAtCorner_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_GrowWorldFirstAtCorner_edge__edge0 = head_candidate_GrowWorldFirstAtCorner_edge__edge0;
                do
                {
                    if(candidate_GrowWorldFirstAtCorner_edge__edge0.lgspType.TypeID!=3) {
                        continue;
                    }
                    // Implicit Target GrowWorldFirstAtCorner_node_next from GrowWorldFirstAtCorner_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_GrowWorldFirstAtCorner_node_next = candidate_GrowWorldFirstAtCorner_edge__edge0.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldFirstAtCorner_node_next.lgspType.TypeID]) {
                        continue;
                    }
                    if((candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldFirstAtCorner_node_next;
                    prev__candidate_GrowWorldFirstAtCorner_node_next = candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_GrowWorldFirstAtCorner_node_next.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // Lookup GrowWorldFirstAtCorner_node_hill 
                    int type_id_candidate_GrowWorldFirstAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldFirstAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldFirstAtCorner_node_hill], candidate_GrowWorldFirstAtCorner_node_hill = head_candidate_GrowWorldFirstAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldFirstAtCorner_node_hill != head_candidate_GrowWorldFirstAtCorner_node_hill; candidate_GrowWorldFirstAtCorner_node_hill = candidate_GrowWorldFirstAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldFirstAtCorner_node_hill.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        Rule_GrowWorldFirstAtCorner.Match_GrowWorldFirstAtCorner match = matches.GetNextUnfilledPosition();
                        match._node_cur = candidate_GrowWorldFirstAtCorner_node_cur;
                        match._node_next = candidate_GrowWorldFirstAtCorner_node_next;
                        match._node_hill = candidate_GrowWorldFirstAtCorner_node_hill;
                        match._edge__edge0 = candidate_GrowWorldFirstAtCorner_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            graph.MoveHeadAfter(candidate_GrowWorldFirstAtCorner_node_hill);
                            candidate_GrowWorldFirstAtCorner_node_cur.MoveOutHeadAfter(candidate_GrowWorldFirstAtCorner_edge__edge0);
                            candidate_GrowWorldFirstAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstAtCorner_node_next;
                            candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldFirstAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstAtCorner_node_next;
                }
                while( (candidate_GrowWorldFirstAtCorner_edge__edge0 = candidate_GrowWorldFirstAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldFirstAtCorner_edge__edge0 );
            }
            candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IGridNode> output_list_0 = new List<GRGEN_MODEL.IGridNode>();
        List<GRGEN_MODEL.IGridNode> output_list_1 = new List<GRGEN_MODEL.IGridNode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstAtCorner_node_cur);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            foreach(Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstAtCorner_node_cur);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstAtCorner_node_cur);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstAtCorner_node_cur);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstAtCorner_node_cur);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IGridNode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            Modify(actionEnv, (Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner>)matches, output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IGridNode) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner>)
            	return ((List<GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GrowWorldFirstNotAtCorner
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
    }
    
    public class Action_GrowWorldFirstNotAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldFirstNotAtCorner
    {
        public Action_GrowWorldFirstNotAtCorner()
            : base(Rule_GrowWorldFirstNotAtCorner.Instance.patternGraph, new object[2])
        {
            _rulePattern = Rule_GrowWorldFirstNotAtCorner.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstNotAtCorner.Match_GrowWorldFirstNotAtCorner, Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner>(this);
        }

        public Rule_GrowWorldFirstNotAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldFirstNotAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstNotAtCorner.Match_GrowWorldFirstNotAtCorner, Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches;

        public static Action_GrowWorldFirstNotAtCorner Instance { get { return instance; } set { instance = value; } }
        private static Action_GrowWorldFirstNotAtCorner instance = new Action_GrowWorldFirstNotAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset GrowWorldFirstNotAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldFirstNotAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldFirstNotAtCorner_node_cur;
            if(candidate_GrowWorldFirstNotAtCorner_node_cur.lgspType.TypeID!=1 && candidate_GrowWorldFirstNotAtCorner_node_cur.lgspType.TypeID!=3) {
                return matches;
            }
            uint prev__candidate_GrowWorldFirstNotAtCorner_node_cur;
            prev__candidate_GrowWorldFirstNotAtCorner_node_cur = candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Extend Outgoing GrowWorldFirstNotAtCorner_edge__edge0 from GrowWorldFirstNotAtCorner_node_cur 
            GRGEN_LGSP.LGSPEdge head_candidate_GrowWorldFirstNotAtCorner_edge__edge0 = candidate_GrowWorldFirstNotAtCorner_node_cur.lgspOuthead;
            if(head_candidate_GrowWorldFirstNotAtCorner_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_GrowWorldFirstNotAtCorner_edge__edge0 = head_candidate_GrowWorldFirstNotAtCorner_edge__edge0;
                do
                {
                    if(candidate_GrowWorldFirstNotAtCorner_edge__edge0.lgspType.TypeID!=3) {
                        continue;
                    }
                    // Implicit Target GrowWorldFirstNotAtCorner_node_next from GrowWorldFirstNotAtCorner_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_GrowWorldFirstNotAtCorner_node_next = candidate_GrowWorldFirstNotAtCorner_edge__edge0.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldFirstNotAtCorner_node_next.lgspType.TypeID]) {
                        continue;
                    }
                    if((candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldFirstNotAtCorner_node_next;
                    prev__candidate_GrowWorldFirstNotAtCorner_node_next = candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // Lookup GrowWorldFirstNotAtCorner_node_hill 
                    int type_id_candidate_GrowWorldFirstNotAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldFirstNotAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldFirstNotAtCorner_node_hill], candidate_GrowWorldFirstNotAtCorner_node_hill = head_candidate_GrowWorldFirstNotAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldFirstNotAtCorner_node_hill != head_candidate_GrowWorldFirstNotAtCorner_node_hill; candidate_GrowWorldFirstNotAtCorner_node_hill = candidate_GrowWorldFirstNotAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldFirstNotAtCorner_node_hill.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        Rule_GrowWorldFirstNotAtCorner.Match_GrowWorldFirstNotAtCorner match = matches.GetNextUnfilledPosition();
                        match._node_cur = candidate_GrowWorldFirstNotAtCorner_node_cur;
                        match._node_next = candidate_GrowWorldFirstNotAtCorner_node_next;
                        match._node_hill = candidate_GrowWorldFirstNotAtCorner_node_hill;
                        match._edge__edge0 = candidate_GrowWorldFirstNotAtCorner_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            graph.MoveHeadAfter(candidate_GrowWorldFirstNotAtCorner_node_hill);
                            candidate_GrowWorldFirstNotAtCorner_node_cur.MoveOutHeadAfter(candidate_GrowWorldFirstNotAtCorner_edge__edge0);
                            candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstNotAtCorner_node_next;
                            candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstNotAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstNotAtCorner_node_next;
                }
                while( (candidate_GrowWorldFirstNotAtCorner_edge__edge0 = candidate_GrowWorldFirstNotAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldFirstNotAtCorner_edge__edge0 );
            }
            candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldFirstNotAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IGridNode> output_list_0 = new List<GRGEN_MODEL.IGridNode>();
        List<GRGEN_MODEL.IGridNode> output_list_1 = new List<GRGEN_MODEL.IGridNode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstNotAtCorner_node_cur);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            foreach(Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstNotAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstNotAtCorner_node_cur);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstNotAtCorner_node_cur);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstNotAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstNotAtCorner_node_cur);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstNotAtCorner_node_cur);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IGridNode) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            Modify(actionEnv, (Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner>)matches, output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IGridNode) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner>)
            	return ((List<GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GrowWorldNextAtCorner
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
    }
    
    public class Action_GrowWorldNextAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldNextAtCorner
    {
        public Action_GrowWorldNextAtCorner()
            : base(Rule_GrowWorldNextAtCorner.Instance.patternGraph, new object[2])
        {
            _rulePattern = Rule_GrowWorldNextAtCorner.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextAtCorner.Match_GrowWorldNextAtCorner, Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner>(this);
        }

        public Rule_GrowWorldNextAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldNextAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextAtCorner.Match_GrowWorldNextAtCorner, Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches;

        public static Action_GrowWorldNextAtCorner Instance { get { return instance; } set { instance = value; } }
        private static Action_GrowWorldNextAtCorner instance = new Action_GrowWorldNextAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset GrowWorldNextAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldNextAtCorner_node_cur;
            if(candidate_GrowWorldNextAtCorner_node_cur.lgspType.TypeID!=2) {
                return matches;
            }
            uint prev__candidate_GrowWorldNextAtCorner_node_cur;
            prev__candidate_GrowWorldNextAtCorner_node_cur = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldNextAtCorner_node_cur.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // NegativePattern 
            {
                ++isoSpace;
                // Extend Incoming GrowWorldNextAtCorner_neg_0_edge__edge0 from GrowWorldNextAtCorner_node_cur 
                GRGEN_LGSP.LGSPEdge head_candidate_GrowWorldNextAtCorner_neg_0_edge__edge0 = candidate_GrowWorldNextAtCorner_node_cur.lgspInhead;
                if(head_candidate_GrowWorldNextAtCorner_neg_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_GrowWorldNextAtCorner_neg_0_edge__edge0 = head_candidate_GrowWorldNextAtCorner_neg_0_edge__edge0;
                    do
                    {
                        if(candidate_GrowWorldNextAtCorner_neg_0_edge__edge0.lgspType.TypeID!=4) {
                            continue;
                        }
                        // negative pattern found
                        --isoSpace;
                        candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                        return matches;
                    }
                    while( (candidate_GrowWorldNextAtCorner_neg_0_edge__edge0 = candidate_GrowWorldNextAtCorner_neg_0_edge__edge0.lgspInNext) != head_candidate_GrowWorldNextAtCorner_neg_0_edge__edge0 );
                }
                --isoSpace;
            }
            // Preset GrowWorldNextAtCorner_node_curOuter 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextAtCorner_node_curOuter = (GRGEN_LGSP.LGSPNode)GrowWorldNextAtCorner_node_curOuter;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldNextAtCorner_node_curOuter.lgspType.TypeID]) {
                candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                return matches;
            }
            if((candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
            {
                candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                return matches;
            }
            uint prev__candidate_GrowWorldNextAtCorner_node_curOuter;
            prev__candidate_GrowWorldNextAtCorner_node_curOuter = candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Extend Outgoing GrowWorldNextAtCorner_edge__edge0 from GrowWorldNextAtCorner_node_cur 
            GRGEN_LGSP.LGSPEdge head_candidate_GrowWorldNextAtCorner_edge__edge0 = candidate_GrowWorldNextAtCorner_node_cur.lgspOuthead;
            if(head_candidate_GrowWorldNextAtCorner_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_GrowWorldNextAtCorner_edge__edge0 = head_candidate_GrowWorldNextAtCorner_edge__edge0;
                do
                {
                    if(candidate_GrowWorldNextAtCorner_edge__edge0.lgspType.TypeID!=3) {
                        continue;
                    }
                    // Implicit Target GrowWorldNextAtCorner_node_next from GrowWorldNextAtCorner_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_GrowWorldNextAtCorner_node_next = candidate_GrowWorldNextAtCorner_edge__edge0.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldNextAtCorner_node_next.lgspType.TypeID]) {
                        continue;
                    }
                    if((candidate_GrowWorldNextAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldNextAtCorner_node_next;
                    prev__candidate_GrowWorldNextAtCorner_node_next = candidate_GrowWorldNextAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_GrowWorldNextAtCorner_node_next.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // Lookup GrowWorldNextAtCorner_node_hill 
                    int type_id_candidate_GrowWorldNextAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldNextAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldNextAtCorner_node_hill], candidate_GrowWorldNextAtCorner_node_hill = head_candidate_GrowWorldNextAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldNextAtCorner_node_hill != head_candidate_GrowWorldNextAtCorner_node_hill; candidate_GrowWorldNextAtCorner_node_hill = candidate_GrowWorldNextAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldNextAtCorner_node_hill.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        Rule_GrowWorldNextAtCorner.Match_GrowWorldNextAtCorner match = matches.GetNextUnfilledPosition();
                        match._node_cur = candidate_GrowWorldNextAtCorner_node_cur;
                        match._node_next = candidate_GrowWorldNextAtCorner_node_next;
                        match._node_curOuter = candidate_GrowWorldNextAtCorner_node_curOuter;
                        match._node_hill = candidate_GrowWorldNextAtCorner_node_hill;
                        match._edge__edge0 = candidate_GrowWorldNextAtCorner_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            graph.MoveHeadAfter(candidate_GrowWorldNextAtCorner_node_hill);
                            candidate_GrowWorldNextAtCorner_node_cur.MoveOutHeadAfter(candidate_GrowWorldNextAtCorner_edge__edge0);
                            candidate_GrowWorldNextAtCorner_node_next.lgspFlags = candidate_GrowWorldNextAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_next;
                            candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_curOuter;
                            candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldNextAtCorner_node_next.lgspFlags = candidate_GrowWorldNextAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_next;
                }
                while( (candidate_GrowWorldNextAtCorner_edge__edge0 = candidate_GrowWorldNextAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldNextAtCorner_edge__edge0 );
            }
            candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_curOuter;
            candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IGridNode> output_list_0 = new List<GRGEN_MODEL.IGridNode>();
        List<GRGEN_MODEL.IGridNode> output_list_1 = new List<GRGEN_MODEL.IGridNode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            foreach(Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            Modify(actionEnv, (Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner>)matches, output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1], output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
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
        public static List<GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner>)
            	return ((List<GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GrowWorldNextNotAtCorner
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
    }
    
    public class Action_GrowWorldNextNotAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldNextNotAtCorner
    {
        public Action_GrowWorldNextNotAtCorner()
            : base(Rule_GrowWorldNextNotAtCorner.Instance.patternGraph, new object[2])
        {
            _rulePattern = Rule_GrowWorldNextNotAtCorner.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextNotAtCorner.Match_GrowWorldNextNotAtCorner, Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner>(this);
        }

        public Rule_GrowWorldNextNotAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldNextNotAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextNotAtCorner.Match_GrowWorldNextNotAtCorner, Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches;

        public static Action_GrowWorldNextNotAtCorner Instance { get { return instance; } set { instance = value; } }
        private static Action_GrowWorldNextNotAtCorner instance = new Action_GrowWorldNextNotAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset GrowWorldNextNotAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextNotAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldNextNotAtCorner_node_cur;
            if(candidate_GrowWorldNextNotAtCorner_node_cur.lgspType.TypeID!=1 && candidate_GrowWorldNextNotAtCorner_node_cur.lgspType.TypeID!=3) {
                return matches;
            }
            uint prev__candidate_GrowWorldNextNotAtCorner_node_cur;
            prev__candidate_GrowWorldNextNotAtCorner_node_cur = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // NegativePattern 
            {
                ++isoSpace;
                // Extend Incoming GrowWorldNextNotAtCorner_neg_0_edge__edge0 from GrowWorldNextNotAtCorner_node_cur 
                GRGEN_LGSP.LGSPEdge head_candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0 = candidate_GrowWorldNextNotAtCorner_node_cur.lgspInhead;
                if(head_candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0 != null)
                {
                    GRGEN_LGSP.LGSPEdge candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0 = head_candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0;
                    do
                    {
                        if(candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0.lgspType.TypeID!=4) {
                            continue;
                        }
                        // negative pattern found
                        --isoSpace;
                        candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                        return matches;
                    }
                    while( (candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0 = candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0.lgspInNext) != head_candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0 );
                }
                --isoSpace;
            }
            // Preset GrowWorldNextNotAtCorner_node_curOuter 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextNotAtCorner_node_curOuter = (GRGEN_LGSP.LGSPNode)GrowWorldNextNotAtCorner_node_curOuter;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspType.TypeID]) {
                candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                return matches;
            }
            if((candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
            {
                candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                return matches;
            }
            uint prev__candidate_GrowWorldNextNotAtCorner_node_curOuter;
            prev__candidate_GrowWorldNextNotAtCorner_node_curOuter = candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Extend Outgoing GrowWorldNextNotAtCorner_edge__edge0 from GrowWorldNextNotAtCorner_node_cur 
            GRGEN_LGSP.LGSPEdge head_candidate_GrowWorldNextNotAtCorner_edge__edge0 = candidate_GrowWorldNextNotAtCorner_node_cur.lgspOuthead;
            if(head_candidate_GrowWorldNextNotAtCorner_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_GrowWorldNextNotAtCorner_edge__edge0 = head_candidate_GrowWorldNextNotAtCorner_edge__edge0;
                do
                {
                    if(candidate_GrowWorldNextNotAtCorner_edge__edge0.lgspType.TypeID!=3) {
                        continue;
                    }
                    // Implicit Target GrowWorldNextNotAtCorner_node_next from GrowWorldNextNotAtCorner_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_GrowWorldNextNotAtCorner_node_next = candidate_GrowWorldNextNotAtCorner_edge__edge0.lgspTarget;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldNextNotAtCorner_node_next.lgspType.TypeID]) {
                        continue;
                    }
                    if((candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldNextNotAtCorner_node_next;
                    prev__candidate_GrowWorldNextNotAtCorner_node_next = candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
                    // Lookup GrowWorldNextNotAtCorner_node_hill 
                    int type_id_candidate_GrowWorldNextNotAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldNextNotAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldNextNotAtCorner_node_hill], candidate_GrowWorldNextNotAtCorner_node_hill = head_candidate_GrowWorldNextNotAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldNextNotAtCorner_node_hill != head_candidate_GrowWorldNextNotAtCorner_node_hill; candidate_GrowWorldNextNotAtCorner_node_hill = candidate_GrowWorldNextNotAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldNextNotAtCorner_node_hill.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                        {
                            continue;
                        }
                        Rule_GrowWorldNextNotAtCorner.Match_GrowWorldNextNotAtCorner match = matches.GetNextUnfilledPosition();
                        match._node_cur = candidate_GrowWorldNextNotAtCorner_node_cur;
                        match._node_next = candidate_GrowWorldNextNotAtCorner_node_next;
                        match._node_curOuter = candidate_GrowWorldNextNotAtCorner_node_curOuter;
                        match._node_hill = candidate_GrowWorldNextNotAtCorner_node_hill;
                        match._edge__edge0 = candidate_GrowWorldNextNotAtCorner_edge__edge0;
                        matches.PositionWasFilledFixIt();
                        // if enough matches were found, we leave
                        if(maxMatches > 0 && matches.Count >= maxMatches)
                        {
                            graph.MoveHeadAfter(candidate_GrowWorldNextNotAtCorner_node_hill);
                            candidate_GrowWorldNextNotAtCorner_node_cur.MoveOutHeadAfter(candidate_GrowWorldNextNotAtCorner_edge__edge0);
                            candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_next;
                            candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_curOuter;
                            candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_next;
                }
                while( (candidate_GrowWorldNextNotAtCorner_edge__edge0 = candidate_GrowWorldNextNotAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldNextNotAtCorner_edge__edge0 );
            }
            candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_curOuter;
            candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IGridNode> output_list_0 = new List<GRGEN_MODEL.IGridNode>();
        List<GRGEN_MODEL.IGridNode> output_list_1 = new List<GRGEN_MODEL.IGridNode>();
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            foreach(Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, List<GRGEN_MODEL.IGridNode> output_0, List<GRGEN_MODEL.IGridNode> output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match in matches)
            {
                GRGEN_MODEL.IGridNode output_local_0; GRGEN_MODEL.IGridNode output_local_1; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0, out output_local_1);
                output_0.Add(output_local_0);
                output_1.Add(output_local_1);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches;
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            Modify(actionEnv, (Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner)match, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner>)matches, output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            output_list_1.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1], output_list_0, output_list_1);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[2]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
                ReturnArrayListForAll[i][1] = output_list_1[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
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
        public static List<GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner>)
            	return ((List<GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GrowWorldEnd
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldEnd.IMatch_GrowWorldEnd match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
    }
    
    public class Action_GrowWorldEnd : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldEnd
    {
        public Action_GrowWorldEnd()
            : base(Rule_GrowWorldEnd.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_GrowWorldEnd.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldEnd.Match_GrowWorldEnd, Rule_GrowWorldEnd.IMatch_GrowWorldEnd>(this);
        }

        public Rule_GrowWorldEnd _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldEnd"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldEnd.Match_GrowWorldEnd, Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches;

        public static Action_GrowWorldEnd Instance { get { return instance; } set { instance = value; } }
        private static Action_GrowWorldEnd instance = new Action_GrowWorldEnd();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset GrowWorldEnd_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldEnd_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldEnd_node_cur;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldEnd_node_cur.lgspType.TypeID]) {
                return matches;
            }
            uint prev__candidate_GrowWorldEnd_node_cur;
            prev__candidate_GrowWorldEnd_node_cur = candidate_GrowWorldEnd_node_cur.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldEnd_node_cur.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Preset GrowWorldEnd_node_curOuter 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldEnd_node_curOuter = (GRGEN_LGSP.LGSPNode)GrowWorldEnd_node_curOuter;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldEnd_node_curOuter.lgspType.TypeID]) {
                candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldEnd_node_cur;
                return matches;
            }
            if((candidate_GrowWorldEnd_node_curOuter.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
            {
                candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldEnd_node_cur;
                return matches;
            }
            uint prev__candidate_GrowWorldEnd_node_curOuter;
            prev__candidate_GrowWorldEnd_node_curOuter = candidate_GrowWorldEnd_node_curOuter.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GrowWorldEnd_node_curOuter.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Extend Incoming GrowWorldEnd_edge__edge0 from GrowWorldEnd_node_cur 
            GRGEN_LGSP.LGSPEdge head_candidate_GrowWorldEnd_edge__edge0 = candidate_GrowWorldEnd_node_cur.lgspInhead;
            if(head_candidate_GrowWorldEnd_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_GrowWorldEnd_edge__edge0 = head_candidate_GrowWorldEnd_edge__edge0;
                do
                {
                    if(candidate_GrowWorldEnd_edge__edge0.lgspType.TypeID!=4) {
                        continue;
                    }
                    // Implicit Source GrowWorldEnd_node_nextOuter from GrowWorldEnd_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_GrowWorldEnd_node_nextOuter = candidate_GrowWorldEnd_edge__edge0.lgspSource;
                    if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldEnd_node_nextOuter.lgspType.TypeID]) {
                        continue;
                    }
                    if((candidate_GrowWorldEnd_node_nextOuter.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                    {
                        continue;
                    }
                    Rule_GrowWorldEnd.Match_GrowWorldEnd match = matches.GetNextUnfilledPosition();
                    match._node_nextOuter = candidate_GrowWorldEnd_node_nextOuter;
                    match._node_cur = candidate_GrowWorldEnd_node_cur;
                    match._node_curOuter = candidate_GrowWorldEnd_node_curOuter;
                    match._edge__edge0 = candidate_GrowWorldEnd_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_GrowWorldEnd_node_cur.MoveInHeadAfter(candidate_GrowWorldEnd_edge__edge0);
                        candidate_GrowWorldEnd_node_curOuter.lgspFlags = candidate_GrowWorldEnd_node_curOuter.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldEnd_node_curOuter;
                        candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldEnd_node_cur;
                        return matches;
                    }
                }
                while( (candidate_GrowWorldEnd_edge__edge0 = candidate_GrowWorldEnd_edge__edge0.lgspInNext) != head_candidate_GrowWorldEnd_edge__edge0 );
            }
            candidate_GrowWorldEnd_node_curOuter.lgspFlags = candidate_GrowWorldEnd_node_curOuter.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldEnd_node_curOuter;
            candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GrowWorldEnd_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldEnd.IMatch_GrowWorldEnd match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches)
        {
            foreach(Rule_GrowWorldEnd.IMatch_GrowWorldEnd match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GrowWorldEnd.IMatch_GrowWorldEnd match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_GrowWorldEnd.IMatch_GrowWorldEnd)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1]);
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
        public static List<GRGEN_ACTIONS.Rule_GrowWorldEnd.IMatch_GrowWorldEnd> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GrowWorldEnd.IMatch_GrowWorldEnd>)
            	return ((List<GRGEN_ACTIONS.Rule_GrowWorldEnd.IMatch_GrowWorldEnd>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GrowWorldEnd.IMatch_GrowWorldEnd>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_GetNextAnt
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GetNextAnt.IMatch_GetNextAnt match, out GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches, List<GRGEN_MODEL.IAnt> output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, List<GRGEN_MODEL.IAnt> output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
    }
    
    public class Action_GetNextAnt : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GetNextAnt
    {
        public Action_GetNextAnt()
            : base(Rule_GetNextAnt.Instance.patternGraph, new object[1])
        {
            _rulePattern = Rule_GetNextAnt.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GetNextAnt.Match_GetNextAnt, Rule_GetNextAnt.IMatch_GetNextAnt>(this);
        }

        public Rule_GetNextAnt _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GetNextAnt"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GetNextAnt.Match_GetNextAnt, Rule_GetNextAnt.IMatch_GetNextAnt> matches;

        public static Action_GetNextAnt Instance { get { return instance; } set { instance = value; } }
        private static Action_GetNextAnt instance = new Action_GetNextAnt();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset GetNextAnt_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_GetNextAnt_node_curAnt = (GRGEN_LGSP.LGSPNode)GetNextAnt_node_curAnt;
            if(candidate_GetNextAnt_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            uint prev__candidate_GetNextAnt_node_curAnt;
            prev__candidate_GetNextAnt_node_curAnt = candidate_GetNextAnt_node_curAnt.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            candidate_GetNextAnt_node_curAnt.lgspFlags |= (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace;
            // Extend Outgoing GetNextAnt_edge__edge0 from GetNextAnt_node_curAnt 
            GRGEN_LGSP.LGSPEdge head_candidate_GetNextAnt_edge__edge0 = candidate_GetNextAnt_node_curAnt.lgspOuthead;
            if(head_candidate_GetNextAnt_edge__edge0 != null)
            {
                GRGEN_LGSP.LGSPEdge candidate_GetNextAnt_edge__edge0 = head_candidate_GetNextAnt_edge__edge0;
                do
                {
                    if(candidate_GetNextAnt_edge__edge0.lgspType.TypeID!=6) {
                        continue;
                    }
                    // Implicit Target GetNextAnt_node_next from GetNextAnt_edge__edge0 
                    GRGEN_LGSP.LGSPNode candidate_GetNextAnt_node_next = candidate_GetNextAnt_edge__edge0.lgspTarget;
                    if(candidate_GetNextAnt_node_next.lgspType.TypeID!=4) {
                        continue;
                    }
                    if((candidate_GetNextAnt_node_next.lgspFlags & (uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) != 0)
                    {
                        continue;
                    }
                    Rule_GetNextAnt.Match_GetNextAnt match = matches.GetNextUnfilledPosition();
                    match._node_curAnt = candidate_GetNextAnt_node_curAnt;
                    match._node_next = candidate_GetNextAnt_node_next;
                    match._edge__edge0 = candidate_GetNextAnt_edge__edge0;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        candidate_GetNextAnt_node_curAnt.MoveOutHeadAfter(candidate_GetNextAnt_edge__edge0);
                        candidate_GetNextAnt_node_curAnt.lgspFlags = candidate_GetNextAnt_node_curAnt.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GetNextAnt_node_curAnt;
                        return matches;
                    }
                }
                while( (candidate_GetNextAnt_edge__edge0 = candidate_GetNextAnt_edge__edge0.lgspOutNext) != head_candidate_GetNextAnt_edge__edge0 );
            }
            candidate_GetNextAnt_node_curAnt.lgspFlags = candidate_GetNextAnt_node_curAnt.lgspFlags & ~((uint)GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << isoSpace) | prev__candidate_GetNextAnt_node_curAnt;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IAnt> output_list_0 = new List<GRGEN_MODEL.IAnt>();
        public GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GetNextAnt_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GetNextAnt.IMatch_GetNextAnt match, out GRGEN_MODEL.IAnt output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches, List<GRGEN_MODEL.IAnt> output_0)
        {
            foreach(Rule_GetNextAnt.IMatch_GetNextAnt match in matches)
            {
                GRGEN_MODEL.IAnt output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GetNextAnt_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, List<GRGEN_MODEL.IAnt> output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GetNextAnt_node_curAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_GetNextAnt.IMatch_GetNextAnt match in matches)
            {
                GRGEN_MODEL.IAnt output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches;
            GRGEN_MODEL.IAnt output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GetNextAnt_node_curAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GetNextAnt_node_curAnt);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IAnt output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GetNextAnt_node_curAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches;
            GRGEN_MODEL.IAnt output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GetNextAnt_node_curAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IAnt output_0; 
            Modify(actionEnv, (Rule_GetNextAnt.IMatch_GetNextAnt)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt>)matches, output_list_0);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0], ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0], output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_GetNextAnt.IMatch_GetNextAnt> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_GetNextAnt.IMatch_GetNextAnt>)
            	return ((List<GRGEN_ACTIONS.Rule_GetNextAnt.IMatch_GetNextAnt>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_GetNextAnt.IMatch_GetNextAnt>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_Food2Ant
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_Food2Ant.IMatch_Food2Ant match, out GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches, List<GRGEN_MODEL.IAnt> output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, List<GRGEN_MODEL.IAnt> output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
    }
    
    public class Action_Food2Ant : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_Food2Ant
    {
        public Action_Food2Ant()
            : base(Rule_Food2Ant.Instance.patternGraph, new object[1])
        {
            _rulePattern = Rule_Food2Ant.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_Food2Ant.Match_Food2Ant, Rule_Food2Ant.IMatch_Food2Ant>(this);
        }

        public Rule_Food2Ant _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "Food2Ant"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_Food2Ant.Match_Food2Ant, Rule_Food2Ant.IMatch_Food2Ant> matches;

        public static Action_Food2Ant Instance { get { return instance; } set { instance = value; } }
        private static Action_Food2Ant instance = new Action_Food2Ant();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset Food2Ant_node_lastAnt 
            GRGEN_LGSP.LGSPNode candidate_Food2Ant_node_lastAnt = (GRGEN_LGSP.LGSPNode)Food2Ant_node_lastAnt;
            if(candidate_Food2Ant_node_lastAnt.lgspType.TypeID!=4) {
                return matches;
            }
            // Lookup Food2Ant_node_hill 
            int type_id_candidate_Food2Ant_node_hill = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_Food2Ant_node_hill = graph.nodesByTypeHeads[type_id_candidate_Food2Ant_node_hill], candidate_Food2Ant_node_hill = head_candidate_Food2Ant_node_hill.lgspTypeNext; candidate_Food2Ant_node_hill != head_candidate_Food2Ant_node_hill; candidate_Food2Ant_node_hill = candidate_Food2Ant_node_hill.lgspTypeNext)
            {
                // Condition 
                if(!((((GRGEN_MODEL.IAntHill)candidate_Food2Ant_node_hill).@food > 0))) {
                    continue;
                }
                Rule_Food2Ant.Match_Food2Ant match = matches.GetNextUnfilledPosition();
                match._node_lastAnt = candidate_Food2Ant_node_lastAnt;
                match._node_hill = candidate_Food2Ant_node_hill;
                matches.PositionWasFilledFixIt();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.Count >= maxMatches)
                {
                    graph.MoveHeadAfter(candidate_Food2Ant_node_hill);
                    return matches;
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        List<GRGEN_MODEL.IAnt> output_list_0 = new List<GRGEN_MODEL.IAnt>();
        public GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, Food2Ant_node_lastAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_Food2Ant.IMatch_Food2Ant match, out GRGEN_MODEL.IAnt output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches, List<GRGEN_MODEL.IAnt> output_0)
        {
            foreach(Rule_Food2Ant.IMatch_Food2Ant match in matches)
            {
                GRGEN_MODEL.IAnt output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, Food2Ant_node_lastAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, List<GRGEN_MODEL.IAnt> output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, Food2Ant_node_lastAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_Food2Ant.IMatch_Food2Ant match in matches)
            {
                GRGEN_MODEL.IAnt output_local_0; 
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_local_0);
                output_0.Add(output_local_0);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches;
            GRGEN_MODEL.IAnt output_0; 
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, Food2Ant_node_lastAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, Food2Ant_node_lastAnt);
            if(matches.Count <= 0) return false;
            GRGEN_MODEL.IAnt output_0; 
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, Food2Ant_node_lastAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches;
            GRGEN_MODEL.IAnt output_0; 
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, Food2Ant_node_lastAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            GRGEN_MODEL.IAnt output_0; 
            Modify(actionEnv, (Rule_Food2Ant.IMatch_Food2Ant)match, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            output_list_0.Clear();
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant>)matches, output_list_0);
            while(AvailableReturnArrays.Count < matches.Count) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matches.Count; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0], ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        public List<object[]> Reserve(int numReturns)
        {
            while(AvailableReturnArrays.Count < numReturns) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<numReturns; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
            }
            return ReturnArrayListForAll;
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            output_list_0.Clear();
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0], output_list_0);
            while(AvailableReturnArrays.Count < matchesCount) AvailableReturnArrays.Add(new object[1]);
            ReturnArrayListForAll.Clear();
            for(int i=0; i<matchesCount; ++i)
            {
                ReturnArrayListForAll.Add(AvailableReturnArrays[i]);
                ReturnArrayListForAll[i][0] = output_list_0[i];
            }
            return ReturnArrayListForAll;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_Food2Ant.IMatch_Food2Ant> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_Food2Ant.IMatch_Food2Ant>)
            	return ((List<GRGEN_ACTIONS.Rule_Food2Ant.IMatch_Food2Ant>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_Food2Ant.IMatch_Food2Ant>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_EvaporateWorld
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_EvaporateWorld.IMatch_EvaporateWorld match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches);
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
    
    public class Action_EvaporateWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_EvaporateWorld
    {
        public Action_EvaporateWorld()
            : base(Rule_EvaporateWorld.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_EvaporateWorld.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_EvaporateWorld.Match_EvaporateWorld, Rule_EvaporateWorld.IMatch_EvaporateWorld>(this);
        }

        public Rule_EvaporateWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "EvaporateWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_EvaporateWorld.Match_EvaporateWorld, Rule_EvaporateWorld.IMatch_EvaporateWorld> matches;

        public static Action_EvaporateWorld Instance { get { return instance; } set { instance = value; } }
        private static Action_EvaporateWorld instance = new Action_EvaporateWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Lookup EvaporateWorld_node_n 
            foreach(GRGEN_LIBGR.NodeType type_candidate_EvaporateWorld_node_n in Rule_EvaporateWorld.EvaporateWorld_node_n_AllowedTypes)
            {
                int type_id_candidate_EvaporateWorld_node_n = type_candidate_EvaporateWorld_node_n.TypeID;
                for(GRGEN_LGSP.LGSPNode head_candidate_EvaporateWorld_node_n = graph.nodesByTypeHeads[type_id_candidate_EvaporateWorld_node_n], candidate_EvaporateWorld_node_n = head_candidate_EvaporateWorld_node_n.lgspTypeNext; candidate_EvaporateWorld_node_n != head_candidate_EvaporateWorld_node_n; candidate_EvaporateWorld_node_n = candidate_EvaporateWorld_node_n.lgspTypeNext)
                {
                    Rule_EvaporateWorld.Match_EvaporateWorld match = matches.GetNextUnfilledPosition();
                    match._node_n = candidate_EvaporateWorld_node_n;
                    matches.PositionWasFilledFixIt();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.Count >= maxMatches)
                    {
                        graph.MoveHeadAfter(candidate_EvaporateWorld_node_n);
                        return matches;
                    }
                }
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_EvaporateWorld.IMatch_EvaporateWorld match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches)
        {
            foreach(Rule_EvaporateWorld.IMatch_EvaporateWorld match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return 0;
            foreach(Rule_EvaporateWorld.IMatch_EvaporateWorld match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
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
            GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches;
            
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
            
            Modify(actionEnv, (Rule_EvaporateWorld.IMatch_EvaporateWorld)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld>)matches);
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
        public static List<GRGEN_ACTIONS.Rule_EvaporateWorld.IMatch_EvaporateWorld> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_EvaporateWorld.IMatch_EvaporateWorld>)
            	return ((List<GRGEN_ACTIONS.Rule_EvaporateWorld.IMatch_EvaporateWorld>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_EvaporateWorld.IMatch_EvaporateWorld>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    
    /// <summary>
    /// An object representing an executable rule - same as IAction, but with exact types and distinct parameters.
    /// </summary>
    public interface IAction_doAntWorld
    {
        /// <summary> same as IAction.Match, but with exact types and distinct parameters. </summary>
        GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.Modify, but with exact types and distinct parameters. </summary>
        void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_doAntWorld.IMatch_doAntWorld match);
        /// <summary> same as IAction.ModifyAll, but with exact types and distinct parameters. </summary>
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns the number of matches found/applied. </summary>
        int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
    }
    
    public class Action_doAntWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_doAntWorld
    {
        public Action_doAntWorld()
            : base(Rule_doAntWorld.Instance.patternGraph, new object[0])
        {
            _rulePattern = Rule_doAntWorld.Instance;
            DynamicMatch = myMatch;
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_doAntWorld.Match_doAntWorld, Rule_doAntWorld.IMatch_doAntWorld>(this);
        }

        public Rule_doAntWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "doAntWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_doAntWorld.Match_doAntWorld, Rule_doAntWorld.IMatch_doAntWorld> matches;

        public static Action_doAntWorld Instance { get { return instance; } set { instance = value; } }
        private static Action_doAntWorld instance = new Action_doAntWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int isoSpace = 0;
            // Preset doAntWorld_node_firstAnt 
            GRGEN_LGSP.LGSPNode candidate_doAntWorld_node_firstAnt = (GRGEN_LGSP.LGSPNode)doAntWorld_node_firstAnt;
            if(candidate_doAntWorld_node_firstAnt.lgspType.TypeID!=4) {
                return matches;
            }
            Rule_doAntWorld.Match_doAntWorld match = matches.GetNextUnfilledPosition();
            match._node_firstAnt = candidate_doAntWorld_node_firstAnt;
            matches.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, doAntWorld_node_firstAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_doAntWorld.IMatch_doAntWorld match)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches)
        {
            foreach(Rule_doAntWorld.IMatch_doAntWorld match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, doAntWorld_node_firstAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public int ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, doAntWorld_node_firstAnt);
            if(matches.Count <= 0) return 0;
            foreach(Rule_doAntWorld.IMatch_doAntWorld match in matches)
            {
                
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            }
            return matches.Count;
        }
        public bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches;
            
            while(true)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, doAntWorld_node_firstAnt);
                if(matches.Count <= 0) return true;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
        }
        public bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, doAntWorld_node_firstAnt);
            if(matches.Count <= 0) return false;
            
            do
            {
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, doAntWorld_node_firstAnt);
            }
            while(matches.Count > 0) ;
            return true;
        }
        public bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches;
            
            for(int i = 0; i < max; i++)
            {
                matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, doAntWorld_node_firstAnt);
                if(matches.Count <= 0) return i >= min;
                _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            }
            return true;
        }
        // implementation of inexact action interface by delegation to exact action interface
        public GRGEN_LIBGR.IMatches Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, object[] parameters)
        {
            return Match(actionEnv, maxMatches, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        public object[] Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatch match)
        {
            
            Modify(actionEnv, (Rule_doAntWorld.IMatch_doAntWorld)match);
            return ReturnArray;
        }
        public List<object[]> ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld>)matches);
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
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(Apply(actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
            throw new Exception();
        }
        List<object[]> GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            int matchesCount = ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
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
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyStar(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            return ApplyPlus(actionEnv, (GRGEN_MODEL.IAnt) parameters[0]);
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max)
        {
            throw new Exception(); return false;
        }
        bool GRGEN_LIBGR.IAction.ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, params object[] parameters)
        {
            return ApplyMinMax(actionEnv, min, max, (GRGEN_MODEL.IAnt) parameters[0]);
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
        public static List<GRGEN_ACTIONS.Rule_doAntWorld.IMatch_doAntWorld> ConvertAsNeeded(object parameter)
        {
            if(parameter is List<GRGEN_ACTIONS.Rule_doAntWorld.IMatch_doAntWorld>)
            	return ((List<GRGEN_ACTIONS.Rule_doAntWorld.IMatch_doAntWorld>)parameter);
            else
            	return GRGEN_LIBGR.MatchListHelper.ToList<GRGEN_ACTIONS.Rule_doAntWorld.IMatch_doAntWorld>((IList<GRGEN_LIBGR.IMatch>)parameter);
        }
    }
    

    // class which instantiates and stores all the compiled actions of the module,
    // dynamic regeneration and compilation causes the old action to be overwritten by the new one
    // matching/rule patterns are analyzed at creation time here, once, so that later regeneration runs have all the information available
    public class AntWorld_ExtendAtEndOfRound_NoGammelActions : GRGEN_LGSP.LGSPActions
    {
        public AntWorld_ExtendAtEndOfRound_NoGammelActions(GRGEN_LGSP.LGSPGraph lgspgraph, string modelAsmName, string actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public AntWorld_ExtendAtEndOfRound_NoGammelActions(GRGEN_LGSP.LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            packages = new string[0];
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_InitWorld.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_InitWorld.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_InitWorld.Instance);
            actions.Add("InitWorld", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_InitWorld.Instance);
            @InitWorld = GRGEN_ACTIONS.Action_InitWorld.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_TakeFood.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_TakeFood.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_TakeFood.Instance);
            actions.Add("TakeFood", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_TakeFood.Instance);
            @TakeFood = GRGEN_ACTIONS.Action_TakeFood.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GoHome.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GoHome.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GoHome.Instance);
            actions.Add("GoHome", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GoHome.Instance);
            @GoHome = GRGEN_ACTIONS.Action_GoHome.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_DropFood.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_DropFood.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_DropFood.Instance);
            actions.Add("DropFood", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_DropFood.Instance);
            @DropFood = GRGEN_ACTIONS.Action_DropFood.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_SearchAlongPheromones.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_SearchAlongPheromones.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_SearchAlongPheromones.Instance);
            actions.Add("SearchAlongPheromones", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_SearchAlongPheromones.Instance);
            @SearchAlongPheromones = GRGEN_ACTIONS.Action_SearchAlongPheromones.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_SearchAimless.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_SearchAimless.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_SearchAimless.Instance);
            actions.Add("SearchAimless", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_SearchAimless.Instance);
            @SearchAimless = GRGEN_ACTIONS.Action_SearchAimless.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ReachedEndOfWorld.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_ReachedEndOfWorld.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_ReachedEndOfWorld.Instance);
            actions.Add("ReachedEndOfWorld", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_ReachedEndOfWorld.Instance);
            @ReachedEndOfWorld = GRGEN_ACTIONS.Action_ReachedEndOfWorld.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.Instance);
            actions.Add("ReachedEndOfWorldAnywhere", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_ReachedEndOfWorldAnywhere.Instance);
            @ReachedEndOfWorldAnywhere = GRGEN_ACTIONS.Action_ReachedEndOfWorldAnywhere.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.Instance);
            actions.Add("GrowFoodIfEqual", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GrowFoodIfEqual.Instance);
            @GrowFoodIfEqual = GRGEN_ACTIONS.Action_GrowFoodIfEqual.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.Instance);
            actions.Add("GrowWorldFirstAtCorner", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GrowWorldFirstAtCorner.Instance);
            @GrowWorldFirstAtCorner = GRGEN_ACTIONS.Action_GrowWorldFirstAtCorner.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.Instance);
            actions.Add("GrowWorldFirstNotAtCorner", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GrowWorldFirstNotAtCorner.Instance);
            @GrowWorldFirstNotAtCorner = GRGEN_ACTIONS.Action_GrowWorldFirstNotAtCorner.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.Instance);
            actions.Add("GrowWorldNextAtCorner", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GrowWorldNextAtCorner.Instance);
            @GrowWorldNextAtCorner = GRGEN_ACTIONS.Action_GrowWorldNextAtCorner.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.Instance);
            actions.Add("GrowWorldNextNotAtCorner", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GrowWorldNextNotAtCorner.Instance);
            @GrowWorldNextNotAtCorner = GRGEN_ACTIONS.Action_GrowWorldNextNotAtCorner.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldEnd.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GrowWorldEnd.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GrowWorldEnd.Instance);
            actions.Add("GrowWorldEnd", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GrowWorldEnd.Instance);
            @GrowWorldEnd = GRGEN_ACTIONS.Action_GrowWorldEnd.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GetNextAnt.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_GetNextAnt.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_GetNextAnt.Instance);
            actions.Add("GetNextAnt", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_GetNextAnt.Instance);
            @GetNextAnt = GRGEN_ACTIONS.Action_GetNextAnt.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_Food2Ant.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_Food2Ant.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_Food2Ant.Instance);
            actions.Add("Food2Ant", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_Food2Ant.Instance);
            @Food2Ant = GRGEN_ACTIONS.Action_Food2Ant.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_EvaporateWorld.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_EvaporateWorld.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_EvaporateWorld.Instance);
            actions.Add("EvaporateWorld", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_EvaporateWorld.Instance);
            @EvaporateWorld = GRGEN_ACTIONS.Action_EvaporateWorld.Instance;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_doAntWorld.Instance.patternGraph, false);
            GRGEN_LGSP.PatternGraphAnalyzer.PrepareInline(GRGEN_ACTIONS.Rule_doAntWorld.Instance.patternGraph);
            analyzer.RememberMatchingPattern(GRGEN_ACTIONS.Rule_doAntWorld.Instance);
            actions.Add("doAntWorld", (GRGEN_LGSP.LGSPAction) GRGEN_ACTIONS.Action_doAntWorld.Instance);
            @doAntWorld = GRGEN_ACTIONS.Action_doAntWorld.Instance;
            analyzer.ComputeInterPatternRelations(false);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_InitWorld.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_TakeFood.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GoHome.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_DropFood.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_SearchAlongPheromones.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_SearchAimless.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_ReachedEndOfWorld.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GrowWorldEnd.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_GetNextAnt.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_Food2Ant.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_EvaporateWorld.Instance.patternGraph);
            analyzer.AnalyzeWithInterPatternRelationsKnown(GRGEN_ACTIONS.Rule_doAntWorld.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_InitWorld.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_TakeFood.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GoHome.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_DropFood.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_SearchAlongPheromones.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_SearchAimless.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_ReachedEndOfWorld.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GrowWorldEnd.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_GetNextAnt.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_Food2Ant.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_EvaporateWorld.Instance.patternGraph);
            analyzer.InlineSubpatternUsages(GRGEN_ACTIONS.Rule_doAntWorld.Instance.patternGraph);
            GRGEN_ACTIONS.Rule_InitWorld.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_TakeFood.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GoHome.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_DropFood.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_SearchAlongPheromones.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_SearchAimless.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_ReachedEndOfWorld.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GrowFoodIfEqual.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GrowWorldEnd.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_GetNextAnt.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_Food2Ant.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_EvaporateWorld.Instance.patternGraph.maxIsoSpace = 0;
            GRGEN_ACTIONS.Rule_doAntWorld.Instance.patternGraph.maxIsoSpace = 0;
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_InitWorld.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_TakeFood.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GoHome.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_DropFood.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_SearchAlongPheromones.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_SearchAimless.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ReachedEndOfWorld.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_ReachedEndOfWorldAnywhere.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowFoodIfEqual.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldFirstAtCorner.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldFirstNotAtCorner.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldNextAtCorner.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldNextNotAtCorner.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GrowWorldEnd.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_GetNextAnt.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_Food2Ant.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_EvaporateWorld.Instance.patternGraph, true);
            analyzer.AnalyzeNestingOfPatternGraph(GRGEN_ACTIONS.Rule_doAntWorld.Instance.patternGraph, true);
            analyzer.ComputeInterPatternRelations(true);
        }
        
        public GRGEN_ACTIONS.IAction_InitWorld @InitWorld;
        public GRGEN_ACTIONS.IAction_TakeFood @TakeFood;
        public GRGEN_ACTIONS.IAction_GoHome @GoHome;
        public GRGEN_ACTIONS.IAction_DropFood @DropFood;
        public GRGEN_ACTIONS.IAction_SearchAlongPheromones @SearchAlongPheromones;
        public GRGEN_ACTIONS.IAction_SearchAimless @SearchAimless;
        public GRGEN_ACTIONS.IAction_ReachedEndOfWorld @ReachedEndOfWorld;
        public GRGEN_ACTIONS.IAction_ReachedEndOfWorldAnywhere @ReachedEndOfWorldAnywhere;
        public GRGEN_ACTIONS.IAction_GrowFoodIfEqual @GrowFoodIfEqual;
        public GRGEN_ACTIONS.IAction_GrowWorldFirstAtCorner @GrowWorldFirstAtCorner;
        public GRGEN_ACTIONS.IAction_GrowWorldFirstNotAtCorner @GrowWorldFirstNotAtCorner;
        public GRGEN_ACTIONS.IAction_GrowWorldNextAtCorner @GrowWorldNextAtCorner;
        public GRGEN_ACTIONS.IAction_GrowWorldNextNotAtCorner @GrowWorldNextNotAtCorner;
        public GRGEN_ACTIONS.IAction_GrowWorldEnd @GrowWorldEnd;
        public GRGEN_ACTIONS.IAction_GetNextAnt @GetNextAnt;
        public GRGEN_ACTIONS.IAction_Food2Ant @Food2Ant;
        public GRGEN_ACTIONS.IAction_EvaporateWorld @EvaporateWorld;
        public GRGEN_ACTIONS.IAction_doAntWorld @doAntWorld;
        
        
        public override string[] Packages { get { return packages; } }
        private string[] packages;
        
        public override string Name { get { return "AntWorld_ExtendAtEndOfRound_NoGammelActions"; } }
        public override string StatisticsPath { get { return null; } }
        public override bool LazyNIC { get { return false; } }
        public override bool InlineIndependents { get { return true; } }
        public override bool Profile { get { return false; } }

        public override void FailAssertion() { Debug.Assert(false); }
        public override string ModelMD5Hash { get { return "5efeccfb37eb4c2835fae110fe22d2e7"; } }
    }
}