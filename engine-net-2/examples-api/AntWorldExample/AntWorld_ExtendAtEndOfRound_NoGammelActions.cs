// This file has been generated automatically by GrGen (www.grgen.net)
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\antWorld\AntWorld_ExtendAtEndOfRound_NoGammel.grg" on Sun Feb 05 16:26:01 CET 2012

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using GRGEN_LIBGR = de.unika.ipd.grGen.libGr;
using GRGEN_LGSP = de.unika.ipd.grGen.lgsp;
using GRGEN_EXPR = de.unika.ipd.grGen.expression;
using GRGEN_MODEL = de.unika.ipd.grGen.Model_AntWorld_NoGammel;

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
		{
			name = "InitWorld";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };

		}
		private void initialize()
		{
			bool[,] InitWorld_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] InitWorld_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] InitWorld_isNodeTotallyHomomorphic = new bool[0] ;
			bool[] InitWorld_isEdgeTotallyHomomorphic = new bool[0] ;
			pat_InitWorld = new GRGEN_LGSP.PatternGraph(
				"InitWorld",
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

		public class Match_InitWorld : GRGEN_LGSP.ListElement<Match_InitWorld>, IMatch_InitWorld
		{
			public enum InitWorld_NodeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 0;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InitWorld_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InitWorld_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InitWorld_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InitWorld_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InitWorld_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum InitWorld_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_InitWorld.instance.pat_InitWorld; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_InitWorld(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_InitWorld(Match_InitWorld that)
			{
			}
			public Match_InitWorld()
			{
			}
		}

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
		{
			name = "TakeFood";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "TakeFood_node_curAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

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
			GRGEN_LGSP.PatternNode TakeFood_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "TakeFood_node_curAnt", "curAnt", TakeFood_node_curAnt_AllowedTypes, TakeFood_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode TakeFood_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "TakeFood_node_n", "n", TakeFood_node_n_AllowedTypes, TakeFood_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge TakeFood_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, "GRGEN_MODEL.IAntPosition", "TakeFood_edge__edge0", "_edge0", TakeFood_edge__edge0_AllowedTypes, TakeFood_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternCondition TakeFood_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.LOG_AND(new GRGEN_EXPR.LOG_NOT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAnt", "TakeFood_node_curAnt", "hasFood")), new GRGEN_EXPR.GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IGridNode", "TakeFood_node_n", "food"), new GRGEN_EXPR.Constant("0"))),
				new string[] { "TakeFood_node_curAnt", "TakeFood_node_n" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_TakeFood = new GRGEN_LGSP.PatternGraph(
				"TakeFood",
				"",
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
			bool tempvar_bool = true;
			graph.ChangingNodeAttribute(node_curAnt, GRGEN_MODEL.NodeType_Ant.AttributeType_hasFood, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_bool, null);
			inode_curAnt.@hasFood = tempvar_bool;
			int tempvar_int = (inode_n.@food - 1);
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_n.@food = tempvar_int;
			return;
		}
		private static string[] TakeFood_addedNodeNames = new string[] {  };
		private static string[] TakeFood_addedEdgeNames = new string[] {  };

		static Rule_TakeFood() {
		}

		public interface IMatch_TakeFood : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; }
			GRGEN_MODEL.IGridNode node_n { get; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_TakeFood : GRGEN_LGSP.ListElement<Match_TakeFood>, IMatch_TakeFood
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } }
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum TakeFood_NodeNums { @curAnt, @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)TakeFood_NodeNums.@curAnt: return _node_curAnt;
				case (int)TakeFood_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum TakeFood_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)TakeFood_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum TakeFood_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum TakeFood_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum TakeFood_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum TakeFood_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum TakeFood_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_TakeFood.instance.pat_TakeFood; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_TakeFood(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_TakeFood(Match_TakeFood that)
			{
				_node_curAnt = that._node_curAnt;
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_TakeFood()
			{
			}
		}

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
		{
			name = "GoHome";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "GoHome_node_curAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

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
			GRGEN_LGSP.PatternNode GoHome_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "GoHome_node_curAnt", "curAnt", GoHome_node_curAnt_AllowedTypes, GoHome_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GoHome_node_old = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GoHome_node_old", "old", GoHome_node_old_AllowedTypes, GoHome_node_old_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GoHome_node_new = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GoHome_node_new", "new", GoHome_node_new_AllowedTypes, GoHome_node_new_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GoHome_edge_oldPos = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, "GRGEN_MODEL.IAntPosition", "GoHome_edge_oldPos", "oldPos", GoHome_edge_oldPos_AllowedTypes, GoHome_edge_oldPos_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GoHome_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, "GRGEN_MODEL.IPathToHill", "GoHome_edge__edge0", "_edge0", GoHome_edge__edge0_AllowedTypes, GoHome_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternCondition GoHome_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAnt", "GoHome_node_curAnt", "hasFood"),
				new string[] { "GoHome_node_curAnt" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_GoHome = new GRGEN_LGSP.PatternGraph(
				"GoHome",
				"",
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
			int tempvar_int = (inode_old.@pheromones + 1024);
			graph.ChangingNodeAttribute(node_old, GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_old.@pheromones = tempvar_int;
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
			GRGEN_MODEL.IAnt node_curAnt { get; }
			GRGEN_MODEL.IGridNode node_old { get; }
			GRGEN_MODEL.IGridNode node_new { get; }
			//Edges
			GRGEN_MODEL.IAntPosition edge_oldPos { get; }
			GRGEN_MODEL.IPathToHill edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GoHome : GRGEN_LGSP.ListElement<Match_GoHome>, IMatch_GoHome
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } }
			public GRGEN_MODEL.IGridNode node_old { get { return (GRGEN_MODEL.IGridNode)_node_old; } }
			public GRGEN_MODEL.IGridNode node_new { get { return (GRGEN_MODEL.IGridNode)_node_new; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_old;
			public GRGEN_LGSP.LGSPNode _node_new;
			public enum GoHome_NodeNums { @curAnt, @old, @new, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GoHome_NodeNums.@curAnt: return _node_curAnt;
				case (int)GoHome_NodeNums.@old: return _node_old;
				case (int)GoHome_NodeNums.@new: return _node_new;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IAntPosition edge_oldPos { get { return (GRGEN_MODEL.IAntPosition)_edge_oldPos; } }
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge_oldPos;
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GoHome_EdgeNums { @oldPos, @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GoHome_EdgeNums.@oldPos: return _edge_oldPos;
				case (int)GoHome_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GoHome_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GoHome_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GoHome_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GoHome_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GoHome_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GoHome.instance.pat_GoHome; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GoHome(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GoHome(Match_GoHome that)
			{
				_node_curAnt = that._node_curAnt;
				_node_old = that._node_old;
				_node_new = that._node_new;
				_edge_oldPos = that._edge_oldPos;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GoHome()
			{
			}
		}

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
		{
			name = "DropFood";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "DropFood_node_curAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

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
			GRGEN_LGSP.PatternNode DropFood_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "DropFood_node_curAnt", "curAnt", DropFood_node_curAnt_AllowedTypes, DropFood_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode DropFood_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, "GRGEN_MODEL.IAntHill", "DropFood_node_hill", "hill", DropFood_node_hill_AllowedTypes, DropFood_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge DropFood_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, "GRGEN_MODEL.IAntPosition", "DropFood_edge__edge0", "_edge0", DropFood_edge__edge0_AllowedTypes, DropFood_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternCondition DropFood_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAnt", "DropFood_node_curAnt", "hasFood"),
				new string[] { "DropFood_node_curAnt" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_DropFood = new GRGEN_LGSP.PatternGraph(
				"DropFood",
				"",
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
			bool tempvar_bool = false;
			graph.ChangingNodeAttribute(node_curAnt, GRGEN_MODEL.NodeType_Ant.AttributeType_hasFood, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_bool, null);
			inode_curAnt.@hasFood = tempvar_bool;
			int tempvar_int = (inode_hill.@food + 1);
			graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_hill.@food = tempvar_int;
			return;
		}
		private static string[] DropFood_addedNodeNames = new string[] {  };
		private static string[] DropFood_addedEdgeNames = new string[] {  };

		static Rule_DropFood() {
		}

		public interface IMatch_DropFood : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_curAnt { get; }
			GRGEN_MODEL.IAntHill node_hill { get; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_DropFood : GRGEN_LGSP.ListElement<Match_DropFood>, IMatch_DropFood
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum DropFood_NodeNums { @curAnt, @hill, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)DropFood_NodeNums.@curAnt: return _node_curAnt;
				case (int)DropFood_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum DropFood_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)DropFood_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum DropFood_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum DropFood_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum DropFood_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum DropFood_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum DropFood_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_DropFood.instance.pat_DropFood; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_DropFood(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_DropFood(Match_DropFood that)
			{
				_node_curAnt = that._node_curAnt;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_DropFood()
			{
			}
		}

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
		{
			name = "SearchAlongPheromones";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "SearchAlongPheromones_node_curAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

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
			GRGEN_LGSP.PatternNode SearchAlongPheromones_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "SearchAlongPheromones_node_curAnt", "curAnt", SearchAlongPheromones_node_curAnt_AllowedTypes, SearchAlongPheromones_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode SearchAlongPheromones_node_old = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "SearchAlongPheromones_node_old", "old", SearchAlongPheromones_node_old_AllowedTypes, SearchAlongPheromones_node_old_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode SearchAlongPheromones_node_new = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "SearchAlongPheromones_node_new", "new", SearchAlongPheromones_node_new_AllowedTypes, SearchAlongPheromones_node_new_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge SearchAlongPheromones_edge_oldPos = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, "GRGEN_MODEL.IAntPosition", "SearchAlongPheromones_edge_oldPos", "oldPos", SearchAlongPheromones_edge_oldPos_AllowedTypes, SearchAlongPheromones_edge_oldPos_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge SearchAlongPheromones_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, "GRGEN_MODEL.IPathToHill", "SearchAlongPheromones_edge__edge0", "_edge0", SearchAlongPheromones_edge__edge0_AllowedTypes, SearchAlongPheromones_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternCondition SearchAlongPheromones_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IGridNode", "SearchAlongPheromones_node_new", "pheromones"), new GRGEN_EXPR.Constant("9")),
				new string[] { "SearchAlongPheromones_node_new" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_SearchAlongPheromones = new GRGEN_LGSP.PatternGraph(
				"SearchAlongPheromones",
				"",
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
			GRGEN_MODEL.IAnt node_curAnt { get; }
			GRGEN_MODEL.IGridNode node_old { get; }
			GRGEN_MODEL.IGridNode node_new { get; }
			//Edges
			GRGEN_MODEL.IAntPosition edge_oldPos { get; }
			GRGEN_MODEL.IPathToHill edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SearchAlongPheromones : GRGEN_LGSP.ListElement<Match_SearchAlongPheromones>, IMatch_SearchAlongPheromones
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } }
			public GRGEN_MODEL.IGridNode node_old { get { return (GRGEN_MODEL.IGridNode)_node_old; } }
			public GRGEN_MODEL.IGridNode node_new { get { return (GRGEN_MODEL.IGridNode)_node_new; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_old;
			public GRGEN_LGSP.LGSPNode _node_new;
			public enum SearchAlongPheromones_NodeNums { @curAnt, @old, @new, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SearchAlongPheromones_NodeNums.@curAnt: return _node_curAnt;
				case (int)SearchAlongPheromones_NodeNums.@old: return _node_old;
				case (int)SearchAlongPheromones_NodeNums.@new: return _node_new;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IAntPosition edge_oldPos { get { return (GRGEN_MODEL.IAntPosition)_edge_oldPos; } }
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge_oldPos;
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum SearchAlongPheromones_EdgeNums { @oldPos, @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SearchAlongPheromones_EdgeNums.@oldPos: return _edge_oldPos;
				case (int)SearchAlongPheromones_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum SearchAlongPheromones_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAlongPheromones_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAlongPheromones_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAlongPheromones_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAlongPheromones_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_SearchAlongPheromones.instance.pat_SearchAlongPheromones; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SearchAlongPheromones(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_SearchAlongPheromones(Match_SearchAlongPheromones that)
			{
				_node_curAnt = that._node_curAnt;
				_node_old = that._node_old;
				_node_new = that._node_new;
				_edge_oldPos = that._edge_oldPos;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_SearchAlongPheromones()
			{
			}
		}

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
		{
			name = "SearchAimless";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "SearchAimless_node_curAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

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
			GRGEN_LGSP.PatternNode SearchAimless_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "SearchAimless_node_curAnt", "curAnt", SearchAimless_node_curAnt_AllowedTypes, SearchAimless_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode SearchAimless_node_old = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "SearchAimless_node_old", "old", SearchAimless_node_old_AllowedTypes, SearchAimless_node_old_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode SearchAimless_node_new = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "SearchAimless_node_new", "new", SearchAimless_node_new_AllowedTypes, SearchAimless_node_new_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge SearchAimless_edge_oldPos = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, "GRGEN_MODEL.IAntPosition", "SearchAimless_edge_oldPos", "oldPos", SearchAimless_edge_oldPos_AllowedTypes, SearchAimless_edge_oldPos_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge SearchAimless_edge__edge0 = new GRGEN_LGSP.PatternEdge(false, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, "GRGEN_MODEL.IGridEdge", "SearchAimless_edge__edge0", "_edge0", SearchAimless_edge__edge0_AllowedTypes, SearchAimless_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_SearchAimless = new GRGEN_LGSP.PatternGraph(
				"SearchAimless",
				"",
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
			GRGEN_MODEL.IAnt node_curAnt { get; }
			GRGEN_MODEL.IGridNode node_old { get; }
			GRGEN_MODEL.IGridNode node_new { get; }
			//Edges
			GRGEN_MODEL.IAntPosition edge_oldPos { get; }
			GRGEN_MODEL.IGridEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_SearchAimless : GRGEN_LGSP.ListElement<Match_SearchAimless>, IMatch_SearchAimless
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } }
			public GRGEN_MODEL.IGridNode node_old { get { return (GRGEN_MODEL.IGridNode)_node_old; } }
			public GRGEN_MODEL.IGridNode node_new { get { return (GRGEN_MODEL.IGridNode)_node_new; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_old;
			public GRGEN_LGSP.LGSPNode _node_new;
			public enum SearchAimless_NodeNums { @curAnt, @old, @new, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)SearchAimless_NodeNums.@curAnt: return _node_curAnt;
				case (int)SearchAimless_NodeNums.@old: return _node_old;
				case (int)SearchAimless_NodeNums.@new: return _node_new;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IAntPosition edge_oldPos { get { return (GRGEN_MODEL.IAntPosition)_edge_oldPos; } }
			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge_oldPos;
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum SearchAimless_EdgeNums { @oldPos, @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 2;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)SearchAimless_EdgeNums.@oldPos: return _edge_oldPos;
				case (int)SearchAimless_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum SearchAimless_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAimless_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAimless_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAimless_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum SearchAimless_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_SearchAimless.instance.pat_SearchAimless; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_SearchAimless(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_SearchAimless(Match_SearchAimless that)
			{
				_node_curAnt = that._node_curAnt;
				_node_old = that._node_old;
				_node_new = that._node_new;
				_edge_oldPos = that._edge_oldPos;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_SearchAimless()
			{
			}
		}

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
		{
			name = "ReachedEndOfWorld";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "ReachedEndOfWorld_node_curAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, };

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
			GRGEN_LGSP.PatternNode ReachedEndOfWorld_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "ReachedEndOfWorld_node_curAnt", "curAnt", ReachedEndOfWorld_node_curAnt_AllowedTypes, ReachedEndOfWorld_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ReachedEndOfWorld_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "ReachedEndOfWorld_node_n", "n", ReachedEndOfWorld_node_n_AllowedTypes, ReachedEndOfWorld_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ReachedEndOfWorld_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, "GRGEN_MODEL.IAntPosition", "ReachedEndOfWorld_edge__edge0", "_edge0", ReachedEndOfWorld_edge__edge0_AllowedTypes, ReachedEndOfWorld_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			bool[,] ReachedEndOfWorld_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ReachedEndOfWorld_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ReachedEndOfWorld_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ReachedEndOfWorld_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ReachedEndOfWorld_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, "GRGEN_MODEL.IPathToHill", "ReachedEndOfWorld_neg_0_edge__edge0", "_edge0", ReachedEndOfWorld_neg_0_edge__edge0_AllowedTypes, ReachedEndOfWorld_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ReachedEndOfWorld_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ReachedEndOfWorld_",
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
			GRGEN_MODEL.IAnt node_curAnt { get; }
			GRGEN_MODEL.IGridNode node_n { get; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; }
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
			GRGEN_MODEL.IGridNode node_n { get; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ReachedEndOfWorld : GRGEN_LGSP.ListElement<Match_ReachedEndOfWorld>, IMatch_ReachedEndOfWorld
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } }
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorld_NodeNums { @curAnt, @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_NodeNums.@curAnt: return _node_curAnt;
				case (int)ReachedEndOfWorld_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorld_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorld.instance.pat_ReachedEndOfWorld; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorld(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_ReachedEndOfWorld(Match_ReachedEndOfWorld that)
			{
				_node_curAnt = that._node_curAnt;
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_ReachedEndOfWorld()
			{
			}
		}

		public class Match_ReachedEndOfWorld_neg_0 : GRGEN_LGSP.ListElement<Match_ReachedEndOfWorld_neg_0>, IMatch_ReachedEndOfWorld_neg_0
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorld_neg_0_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_neg_0_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorld_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorld_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorld_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorld.instance.ReachedEndOfWorld_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorld_neg_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_ReachedEndOfWorld_neg_0(Match_ReachedEndOfWorld_neg_0 that)
			{
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_ReachedEndOfWorld_neg_0()
			{
			}
		}

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
		{
			name = "ReachedEndOfWorldAnywhere";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, };

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
			GRGEN_LGSP.PatternNode ReachedEndOfWorldAnywhere_node__node0 = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "ReachedEndOfWorldAnywhere_node__node0", "_node0", ReachedEndOfWorldAnywhere_node__node0_AllowedTypes, ReachedEndOfWorldAnywhere_node__node0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode ReachedEndOfWorldAnywhere_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "ReachedEndOfWorldAnywhere_node_n", "n", ReachedEndOfWorldAnywhere_node_n_AllowedTypes, ReachedEndOfWorldAnywhere_node_n_IsAllowedType, 1.0F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge ReachedEndOfWorldAnywhere_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@AntPosition, "GRGEN_MODEL.IAntPosition", "ReachedEndOfWorldAnywhere_edge__edge0", "_edge0", ReachedEndOfWorldAnywhere_edge__edge0_AllowedTypes, ReachedEndOfWorldAnywhere_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			bool[,] ReachedEndOfWorldAnywhere_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ReachedEndOfWorldAnywhere_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] ReachedEndOfWorldAnywhere_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] ReachedEndOfWorldAnywhere_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge ReachedEndOfWorldAnywhere_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, "GRGEN_MODEL.IPathToHill", "ReachedEndOfWorldAnywhere_neg_0_edge__edge0", "_edge0", ReachedEndOfWorldAnywhere_neg_0_edge__edge0_AllowedTypes, ReachedEndOfWorldAnywhere_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			ReachedEndOfWorldAnywhere_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"ReachedEndOfWorldAnywhere_",
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
			ReachedEndOfWorldAnywhere_node_n.annotations.Add("prio", "10000");
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
			GRGEN_MODEL.IAnt node__node0 { get; }
			GRGEN_MODEL.IGridNode node_n { get; }
			//Edges
			GRGEN_MODEL.IAntPosition edge__edge0 { get; }
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
			GRGEN_MODEL.IGridNode node_n { get; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_ReachedEndOfWorldAnywhere : GRGEN_LGSP.ListElement<Match_ReachedEndOfWorldAnywhere>, IMatch_ReachedEndOfWorldAnywhere
		{
			public GRGEN_MODEL.IAnt node__node0 { get { return (GRGEN_MODEL.IAnt)_node__node0; } }
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node__node0;
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorldAnywhere_NodeNums { @_node0, @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_NodeNums.@_node0: return _node__node0;
				case (int)ReachedEndOfWorldAnywhere_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IAntPosition edge__edge0 { get { return (GRGEN_MODEL.IAntPosition)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorldAnywhere_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorldAnywhere.instance.pat_ReachedEndOfWorldAnywhere; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorldAnywhere(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_ReachedEndOfWorldAnywhere(Match_ReachedEndOfWorldAnywhere that)
			{
				_node__node0 = that._node__node0;
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_ReachedEndOfWorldAnywhere()
			{
			}
		}

		public class Match_ReachedEndOfWorldAnywhere_neg_0 : GRGEN_LGSP.ListElement<Match_ReachedEndOfWorldAnywhere_neg_0>, IMatch_ReachedEndOfWorldAnywhere_neg_0
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum ReachedEndOfWorldAnywhere_neg_0_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_neg_0_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum ReachedEndOfWorldAnywhere_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)ReachedEndOfWorldAnywhere_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum ReachedEndOfWorldAnywhere_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_ReachedEndOfWorldAnywhere.instance.ReachedEndOfWorldAnywhere_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_ReachedEndOfWorldAnywhere_neg_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_ReachedEndOfWorldAnywhere_neg_0(Match_ReachedEndOfWorldAnywhere_neg_0 that)
			{
				_node_n = that._node_n;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_ReachedEndOfWorldAnywhere_neg_0()
			{
			}
		}

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
		{
			name = "GrowFoodIfEqual";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_LIBGR.VarType.GetVarType(typeof(int)), };
			inputNames = new string[] { "GrowFoodIfEqual_node_n", "GrowFoodIfEqual_var_val", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] GrowFoodIfEqual_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] GrowFoodIfEqual_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] GrowFoodIfEqual_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] GrowFoodIfEqual_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternVariable GrowFoodIfEqual_var_val = new GRGEN_LGSP.PatternVariable(GRGEN_LIBGR.VarType.GetVarType(typeof(int)), "GrowFoodIfEqual_var_val", "val", 1, false, null);
			GRGEN_LGSP.PatternNode GrowFoodIfEqual_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowFoodIfEqual_node_n", "n", GrowFoodIfEqual_node_n_AllowedTypes, GrowFoodIfEqual_node_n_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowFoodIfEqual_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, "GRGEN_MODEL.IAntHill", "GrowFoodIfEqual_node_hill", "hill", GrowFoodIfEqual_node_hill_AllowedTypes, GrowFoodIfEqual_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternCondition GrowFoodIfEqual_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.EQ(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAntHill", "GrowFoodIfEqual_node_hill", "foodCountdown"), new GRGEN_EXPR.VariableExpression("GrowFoodIfEqual_var_val")),
				new string[] { "GrowFoodIfEqual_node_hill" }, new string[] {  }, new string[] { "GrowFoodIfEqual_var_val" }, new GRGEN_LIBGR.VarType[] { GRGEN_LIBGR.VarType.GetVarType(typeof(int)) });
			pat_GrowFoodIfEqual = new GRGEN_LGSP.PatternGraph(
				"GrowFoodIfEqual",
				"",
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
			int tempvar_int = (inode_n.@food + 100);
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_n.@food = tempvar_int;
			tempvar_int = (inode_hill.@foodCountdown + 10);
			graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_hill.@foodCountdown = tempvar_int;
			return;
		}
		private static string[] GrowFoodIfEqual_addedNodeNames = new string[] {  };
		private static string[] GrowFoodIfEqual_addedEdgeNames = new string[] {  };

		static Rule_GrowFoodIfEqual() {
		}

		public interface IMatch_GrowFoodIfEqual : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_n { get; }
			GRGEN_MODEL.IAntHill node_hill { get; }
			//Edges
			//Variables
			int @var_val { get; }
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowFoodIfEqual : GRGEN_LGSP.ListElement<Match_GrowFoodIfEqual>, IMatch_GrowFoodIfEqual
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowFoodIfEqual_NodeNums { @n, @hill, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowFoodIfEqual_NodeNums.@n: return _node_n;
				case (int)GrowFoodIfEqual_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			
			public enum GrowFoodIfEqual_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public int var_val { get { return _var_val; } }
			public int _var_val;
			public enum GrowFoodIfEqual_VariableNums { @val, END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 1;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				case (int)GrowFoodIfEqual_VariableNums.@val: return _var_val;
				default: return null;
				}
			}
			
			public enum GrowFoodIfEqual_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowFoodIfEqual_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowFoodIfEqual_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowFoodIfEqual_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowFoodIfEqual.instance.pat_GrowFoodIfEqual; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowFoodIfEqual(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowFoodIfEqual(Match_GrowFoodIfEqual that)
			{
				_node_n = that._node_n;
				_node_hill = that._node_hill;
				_var_val = that._var_val;
			}
			public Match_GrowFoodIfEqual()
			{
			}
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
		{
			name = "GrowWorldFirstAtCorner";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, };
			inputNames = new string[] { "GrowWorldFirstAtCorner_node_cur", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, };

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
			GRGEN_LGSP.PatternNode GrowWorldFirstAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridCornerNode, "GRGEN_MODEL.IGridCornerNode", "GrowWorldFirstAtCorner_node_cur", "cur", GrowWorldFirstAtCorner_node_cur_AllowedTypes, GrowWorldFirstAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldFirstAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldFirstAtCorner_node_next", "next", GrowWorldFirstAtCorner_node_next_AllowedTypes, GrowWorldFirstAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldFirstAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, "GRGEN_MODEL.IAntHill", "GrowWorldFirstAtCorner_node_hill", "hill", GrowWorldFirstAtCorner_node_hill_AllowedTypes, GrowWorldFirstAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GrowWorldFirstAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, "GRGEN_MODEL.IGridEdge", "GrowWorldFirstAtCorner_edge__edge0", "_edge0", GrowWorldFirstAtCorner_edge__edge0_AllowedTypes, GrowWorldFirstAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_GrowWorldFirstAtCorner = new GRGEN_LGSP.PatternGraph(
				"GrowWorldFirstAtCorner",
				"",
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
			int tempvar_int = (inode_hill.@foodCountdown - 3);
			graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_hill.@foodCountdown = tempvar_int;
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
            bool res_16;
            bool res_10;
            bool res_4;
            Action_GrowFoodIfEqual rule_GrowFoodIfEqual = Action_GrowFoodIfEqual.Instance;
            bool res_9;
            bool res_15;
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_4 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer1, (int)-2);
            procEnv.Matched(matches_4, false);
            if(matches_4.Count==0) {
                res_4 = (bool)(false);
            } else {
                res_4 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_4.Count;
                procEnv.Finishing(matches_4, false);
                Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_4 = matches_4.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_4);
                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_4, false);
            }
            if(res_4)
                res_10 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_9 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer2, (int)-1);
                procEnv.Matched(matches_9, false);
                if(matches_9.Count==0) {
                    res_9 = (bool)(false);
                } else {
                    res_9 = (bool)(true);
                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_9.Count;
                    procEnv.Finishing(matches_9, false);
                    Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_9 = matches_9.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_9);
                    if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_9, false);
                }
                res_10 = (bool)(res_9);
            }
            if(res_10)
                res_16 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_15 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer3, (int)0);
                procEnv.Matched(matches_15, false);
                if(matches_15.Count==0) {
                    res_15 = (bool)(false);
                } else {
                    res_15 = (bool)(true);
                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_15.Count;
                    procEnv.Finishing(matches_15, false);
                    Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_15 = matches_15.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_15);
                    if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_15, false);
                }
                res_16 = (bool)(res_15);
            }
            return res_16;
        }

		static Rule_GrowWorldFirstAtCorner() {
		}

		public interface IMatch_GrowWorldFirstAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridCornerNode node_cur { get; }
			GRGEN_MODEL.IGridNode node_next { get; }
			GRGEN_MODEL.IAntHill node_hill { get; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldFirstAtCorner : GRGEN_LGSP.ListElement<Match_GrowWorldFirstAtCorner>, IMatch_GrowWorldFirstAtCorner
		{
			public GRGEN_MODEL.IGridCornerNode node_cur { get { return (GRGEN_MODEL.IGridCornerNode)_node_cur; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldFirstAtCorner_NodeNums { @cur, @next, @hill, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldFirstAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldFirstAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldFirstAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GrowWorldFirstAtCorner_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstAtCorner_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstAtCorner_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstAtCorner_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstAtCorner_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldFirstAtCorner.instance.pat_GrowWorldFirstAtCorner; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldFirstAtCorner(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowWorldFirstAtCorner(Match_GrowWorldFirstAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GrowWorldFirstAtCorner()
			{
			}
		}

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
		{
			name = "GrowWorldFirstNotAtCorner";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, };
			inputNames = new string[] { "GrowWorldFirstNotAtCorner_node_cur", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, };

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
			GRGEN_LGSP.PatternNode GrowWorldFirstNotAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldFirstNotAtCorner_node_cur", "cur", GrowWorldFirstNotAtCorner_node_cur_AllowedTypes, GrowWorldFirstNotAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldFirstNotAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldFirstNotAtCorner_node_next", "next", GrowWorldFirstNotAtCorner_node_next_AllowedTypes, GrowWorldFirstNotAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldFirstNotAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, "GRGEN_MODEL.IAntHill", "GrowWorldFirstNotAtCorner_node_hill", "hill", GrowWorldFirstNotAtCorner_node_hill_AllowedTypes, GrowWorldFirstNotAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GrowWorldFirstNotAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, "GRGEN_MODEL.IGridEdge", "GrowWorldFirstNotAtCorner_edge__edge0", "_edge0", GrowWorldFirstNotAtCorner_edge__edge0_AllowedTypes, GrowWorldFirstNotAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_GrowWorldFirstNotAtCorner = new GRGEN_LGSP.PatternGraph(
				"GrowWorldFirstNotAtCorner",
				"",
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
			int tempvar_int = (inode_hill.@foodCountdown - 1);
			graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_hill.@foodCountdown = tempvar_int;
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
            bool res_21;
            Action_GrowFoodIfEqual rule_GrowFoodIfEqual = Action_GrowFoodIfEqual.Instance;
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_21 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer, (int)0);
            procEnv.Matched(matches_21, false);
            if(matches_21.Count==0) {
                res_21 = (bool)(false);
            } else {
                res_21 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_21.Count;
                procEnv.Finishing(matches_21, false);
                Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_21 = matches_21.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_21);
                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_21, false);
            }
            return res_21;
        }

		static Rule_GrowWorldFirstNotAtCorner() {
		}

		public interface IMatch_GrowWorldFirstNotAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_cur { get; }
			GRGEN_MODEL.IGridNode node_next { get; }
			GRGEN_MODEL.IAntHill node_hill { get; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldFirstNotAtCorner : GRGEN_LGSP.ListElement<Match_GrowWorldFirstNotAtCorner>, IMatch_GrowWorldFirstNotAtCorner
		{
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldFirstNotAtCorner_NodeNums { @cur, @next, @hill, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstNotAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldFirstNotAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldFirstNotAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldFirstNotAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldFirstNotAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GrowWorldFirstNotAtCorner_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstNotAtCorner_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstNotAtCorner_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstNotAtCorner_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldFirstNotAtCorner_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldFirstNotAtCorner.instance.pat_GrowWorldFirstNotAtCorner; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldFirstNotAtCorner(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowWorldFirstNotAtCorner(Match_GrowWorldFirstNotAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GrowWorldFirstNotAtCorner()
			{
			}
		}

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
		{
			name = "GrowWorldNextAtCorner";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, };
			inputNames = new string[] { "GrowWorldNextAtCorner_node_cur", "GrowWorldNextAtCorner_node_curOuter", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, };

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
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridCornerNode, "GRGEN_MODEL.IGridCornerNode", "GrowWorldNextAtCorner_node_cur", "cur", GrowWorldNextAtCorner_node_cur_AllowedTypes, GrowWorldNextAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldNextAtCorner_node_next", "next", GrowWorldNextAtCorner_node_next_AllowedTypes, GrowWorldNextAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_curOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldNextAtCorner_node_curOuter", "curOuter", GrowWorldNextAtCorner_node_curOuter_AllowedTypes, GrowWorldNextAtCorner_node_curOuter_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldNextAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, "GRGEN_MODEL.IAntHill", "GrowWorldNextAtCorner_node_hill", "hill", GrowWorldNextAtCorner_node_hill_AllowedTypes, GrowWorldNextAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GrowWorldNextAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, "GRGEN_MODEL.IGridEdge", "GrowWorldNextAtCorner_edge__edge0", "_edge0", GrowWorldNextAtCorner_edge__edge0_AllowedTypes, GrowWorldNextAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			bool[,] GrowWorldNextAtCorner_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] GrowWorldNextAtCorner_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldNextAtCorner_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] GrowWorldNextAtCorner_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge GrowWorldNextAtCorner_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, "GRGEN_MODEL.IPathToHill", "GrowWorldNextAtCorner_neg_0_edge__edge0", "_edge0", GrowWorldNextAtCorner_neg_0_edge__edge0_AllowedTypes, GrowWorldNextAtCorner_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GrowWorldNextAtCorner_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"GrowWorldNextAtCorner_",
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
			int tempvar_int = (inode_hill.@foodCountdown - 3);
			graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_hill.@foodCountdown = tempvar_int;
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
            bool res_38;
            bool res_32;
            bool res_26;
            Action_GrowFoodIfEqual rule_GrowFoodIfEqual = Action_GrowFoodIfEqual.Instance;
            bool res_31;
            bool res_37;
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_26 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer1, (int)-2);
            procEnv.Matched(matches_26, false);
            if(matches_26.Count==0) {
                res_26 = (bool)(false);
            } else {
                res_26 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_26.Count;
                procEnv.Finishing(matches_26, false);
                Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_26 = matches_26.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_26);
                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_26, false);
            }
            if(res_26)
                res_32 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_31 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer2, (int)-1);
                procEnv.Matched(matches_31, false);
                if(matches_31.Count==0) {
                    res_31 = (bool)(false);
                } else {
                    res_31 = (bool)(true);
                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_31.Count;
                    procEnv.Finishing(matches_31, false);
                    Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_31 = matches_31.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_31);
                    if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_31, false);
                }
                res_32 = (bool)(res_31);
            }
            if(res_32)
                res_38 = (bool)(true);
            else
            {
                GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_37 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer3, (int)0);
                procEnv.Matched(matches_37, false);
                if(matches_37.Count==0) {
                    res_37 = (bool)(false);
                } else {
                    res_37 = (bool)(true);
                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_37.Count;
                    procEnv.Finishing(matches_37, false);
                    Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_37 = matches_37.FirstExact;
                    rule_GrowFoodIfEqual.Modify(procEnv, match_37);
                    if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                    procEnv.Finished(matches_37, false);
                }
                res_38 = (bool)(res_37);
            }
            return res_38;
        }

		static Rule_GrowWorldNextAtCorner() {
		}

		public interface IMatch_GrowWorldNextAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridCornerNode node_cur { get; }
			GRGEN_MODEL.IGridNode node_next { get; }
			GRGEN_MODEL.IGridNode node_curOuter { get; }
			GRGEN_MODEL.IAntHill node_hill { get; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; }
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
			GRGEN_MODEL.IGridCornerNode node_cur { get; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldNextAtCorner : GRGEN_LGSP.ListElement<Match_GrowWorldNextAtCorner>, IMatch_GrowWorldNextAtCorner
		{
			public GRGEN_MODEL.IGridCornerNode node_cur { get { return (GRGEN_MODEL.IGridCornerNode)_node_cur; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } }
			public GRGEN_MODEL.IGridNode node_curOuter { get { return (GRGEN_MODEL.IGridNode)_node_curOuter; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_curOuter;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldNextAtCorner_NodeNums { @cur, @next, @curOuter, @hill, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldNextAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldNextAtCorner_NodeNums.@curOuter: return _node_curOuter;
				case (int)GrowWorldNextAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextAtCorner.instance.pat_GrowWorldNextAtCorner; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextAtCorner(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowWorldNextAtCorner(Match_GrowWorldNextAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_curOuter = that._node_curOuter;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GrowWorldNextAtCorner()
			{
			}
		}

		public class Match_GrowWorldNextAtCorner_neg_0 : GRGEN_LGSP.ListElement<Match_GrowWorldNextAtCorner_neg_0>, IMatch_GrowWorldNextAtCorner_neg_0
		{
			public GRGEN_MODEL.IGridCornerNode node_cur { get { return (GRGEN_MODEL.IGridCornerNode)_node_cur; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public enum GrowWorldNextAtCorner_neg_0_NodeNums { @cur, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_neg_0_NodeNums.@cur: return _node_cur;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextAtCorner_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextAtCorner_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextAtCorner_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextAtCorner.instance.GrowWorldNextAtCorner_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextAtCorner_neg_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowWorldNextAtCorner_neg_0(Match_GrowWorldNextAtCorner_neg_0 that)
			{
				_node_cur = that._node_cur;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GrowWorldNextAtCorner_neg_0()
			{
			}
		}

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
		{
			name = "GrowWorldNextNotAtCorner";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, };
			inputNames = new string[] { "GrowWorldNextNotAtCorner_node_cur", "GrowWorldNextNotAtCorner_node_curOuter", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, };

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
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldNextNotAtCorner_node_cur", "cur", GrowWorldNextNotAtCorner_node_cur_AllowedTypes, GrowWorldNextNotAtCorner_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldNextNotAtCorner_node_next", "next", GrowWorldNextNotAtCorner_node_next_AllowedTypes, GrowWorldNextNotAtCorner_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_curOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldNextNotAtCorner_node_curOuter", "curOuter", GrowWorldNextNotAtCorner_node_curOuter_AllowedTypes, GrowWorldNextNotAtCorner_node_curOuter_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldNextNotAtCorner_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, "GRGEN_MODEL.IAntHill", "GrowWorldNextNotAtCorner_node_hill", "hill", GrowWorldNextNotAtCorner_node_hill_AllowedTypes, GrowWorldNextNotAtCorner_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GrowWorldNextNotAtCorner_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@GridEdge, "GRGEN_MODEL.IGridEdge", "GrowWorldNextNotAtCorner_edge__edge0", "_edge0", GrowWorldNextNotAtCorner_edge__edge0_AllowedTypes, GrowWorldNextNotAtCorner_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			bool[,] GrowWorldNextNotAtCorner_neg_0_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] GrowWorldNextNotAtCorner_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[] GrowWorldNextNotAtCorner_neg_0_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] GrowWorldNextNotAtCorner_neg_0_isEdgeTotallyHomomorphic = new bool[1] { false,  };
			GRGEN_LGSP.PatternEdge GrowWorldNextNotAtCorner_neg_0_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, "GRGEN_MODEL.IPathToHill", "GrowWorldNextNotAtCorner_neg_0_edge__edge0", "_edge0", GrowWorldNextNotAtCorner_neg_0_edge__edge0_AllowedTypes, GrowWorldNextNotAtCorner_neg_0_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GrowWorldNextNotAtCorner_neg_0 = new GRGEN_LGSP.PatternGraph(
				"neg_0",
				"GrowWorldNextNotAtCorner_",
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
			int tempvar_int = (inode_hill.@foodCountdown - 1);
			graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_AntHill.AttributeType_foodCountdown, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_hill.@foodCountdown = tempvar_int;
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
            bool res_43;
            Action_GrowFoodIfEqual rule_GrowFoodIfEqual = Action_GrowFoodIfEqual.Instance;
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches_43 = rule_GrowFoodIfEqual.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_outer, (int)0);
            procEnv.Matched(matches_43, false);
            if(matches_43.Count==0) {
                res_43 = (bool)(false);
            } else {
                res_43 = (bool)(true);
                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_43.Count;
                procEnv.Finishing(matches_43, false);
                Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match_43 = matches_43.FirstExact;
                rule_GrowFoodIfEqual.Modify(procEnv, match_43);
                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                procEnv.Finished(matches_43, false);
            }
            return res_43;
        }

		static Rule_GrowWorldNextNotAtCorner() {
		}

		public interface IMatch_GrowWorldNextNotAtCorner : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_cur { get; }
			GRGEN_MODEL.IGridNode node_next { get; }
			GRGEN_MODEL.IGridNode node_curOuter { get; }
			GRGEN_MODEL.IAntHill node_hill { get; }
			//Edges
			GRGEN_MODEL.IGridEdge edge__edge0 { get; }
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
			GRGEN_MODEL.IGridNode node_cur { get; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldNextNotAtCorner : GRGEN_LGSP.ListElement<Match_GrowWorldNextNotAtCorner>, IMatch_GrowWorldNextNotAtCorner
		{
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } }
			public GRGEN_MODEL.IGridNode node_next { get { return (GRGEN_MODEL.IGridNode)_node_next; } }
			public GRGEN_MODEL.IGridNode node_curOuter { get { return (GRGEN_MODEL.IGridNode)_node_curOuter; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_next;
			public GRGEN_LGSP.LGSPNode _node_curOuter;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum GrowWorldNextNotAtCorner_NodeNums { @cur, @next, @curOuter, @hill, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 4;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldNextNotAtCorner_NodeNums.@next: return _node_next;
				case (int)GrowWorldNextNotAtCorner_NodeNums.@curOuter: return _node_curOuter;
				case (int)GrowWorldNextNotAtCorner_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IGridEdge edge__edge0 { get { return (GRGEN_MODEL.IGridEdge)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextNotAtCorner_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextNotAtCorner.instance.pat_GrowWorldNextNotAtCorner; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextNotAtCorner(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowWorldNextNotAtCorner(Match_GrowWorldNextNotAtCorner that)
			{
				_node_cur = that._node_cur;
				_node_next = that._node_next;
				_node_curOuter = that._node_curOuter;
				_node_hill = that._node_hill;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GrowWorldNextNotAtCorner()
			{
			}
		}

		public class Match_GrowWorldNextNotAtCorner_neg_0 : GRGEN_LGSP.ListElement<Match_GrowWorldNextNotAtCorner_neg_0>, IMatch_GrowWorldNextNotAtCorner_neg_0
		{
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } }
			public GRGEN_LGSP.LGSPNode _node_cur;
			public enum GrowWorldNextNotAtCorner_neg_0_NodeNums { @cur, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_neg_0_NodeNums.@cur: return _node_cur;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldNextNotAtCorner_neg_0_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldNextNotAtCorner_neg_0_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_neg_0_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_neg_0_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_neg_0_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_neg_0_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldNextNotAtCorner_neg_0_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldNextNotAtCorner.instance.GrowWorldNextNotAtCorner_neg_0; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldNextNotAtCorner_neg_0(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowWorldNextNotAtCorner_neg_0(Match_GrowWorldNextNotAtCorner_neg_0 that)
			{
				_node_cur = that._node_cur;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GrowWorldNextNotAtCorner_neg_0()
			{
			}
		}

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
		{
			name = "GrowWorldEnd";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_GridNode.typeVar, GRGEN_MODEL.NodeType_GridNode.typeVar, };
			inputNames = new string[] { "GrowWorldEnd_node_cur", "GrowWorldEnd_node_curOuter", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

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
			GRGEN_LGSP.PatternNode GrowWorldEnd_node_nextOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldEnd_node_nextOuter", "nextOuter", GrowWorldEnd_node_nextOuter_AllowedTypes, GrowWorldEnd_node_nextOuter_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldEnd_node_cur = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldEnd_node_cur", "cur", GrowWorldEnd_node_cur_AllowedTypes, GrowWorldEnd_node_cur_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GrowWorldEnd_node_curOuter = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "GrowWorldEnd_node_curOuter", "curOuter", GrowWorldEnd_node_curOuter_AllowedTypes, GrowWorldEnd_node_curOuter_IsAllowedType, 5.5F, 1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GrowWorldEnd_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@PathToHill, "GRGEN_MODEL.IPathToHill", "GrowWorldEnd_edge__edge0", "_edge0", GrowWorldEnd_edge__edge0_AllowedTypes, GrowWorldEnd_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_GrowWorldEnd = new GRGEN_LGSP.PatternGraph(
				"GrowWorldEnd",
				"",
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
			GRGEN_MODEL.IGridNode node_nextOuter { get; }
			GRGEN_MODEL.IGridNode node_cur { get; }
			GRGEN_MODEL.IGridNode node_curOuter { get; }
			//Edges
			GRGEN_MODEL.IPathToHill edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GrowWorldEnd : GRGEN_LGSP.ListElement<Match_GrowWorldEnd>, IMatch_GrowWorldEnd
		{
			public GRGEN_MODEL.IGridNode node_nextOuter { get { return (GRGEN_MODEL.IGridNode)_node_nextOuter; } }
			public GRGEN_MODEL.IGridNode node_cur { get { return (GRGEN_MODEL.IGridNode)_node_cur; } }
			public GRGEN_MODEL.IGridNode node_curOuter { get { return (GRGEN_MODEL.IGridNode)_node_curOuter; } }
			public GRGEN_LGSP.LGSPNode _node_nextOuter;
			public GRGEN_LGSP.LGSPNode _node_cur;
			public GRGEN_LGSP.LGSPNode _node_curOuter;
			public enum GrowWorldEnd_NodeNums { @nextOuter, @cur, @curOuter, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 3;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldEnd_NodeNums.@nextOuter: return _node_nextOuter;
				case (int)GrowWorldEnd_NodeNums.@cur: return _node_cur;
				case (int)GrowWorldEnd_NodeNums.@curOuter: return _node_curOuter;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.IPathToHill edge__edge0 { get { return (GRGEN_MODEL.IPathToHill)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GrowWorldEnd_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GrowWorldEnd_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GrowWorldEnd_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldEnd_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldEnd_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldEnd_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GrowWorldEnd_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GrowWorldEnd.instance.pat_GrowWorldEnd; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GrowWorldEnd(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GrowWorldEnd(Match_GrowWorldEnd that)
			{
				_node_nextOuter = that._node_nextOuter;
				_node_cur = that._node_cur;
				_node_curOuter = that._node_curOuter;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GrowWorldEnd()
			{
			}
		}

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
		{
			name = "GetNextAnt";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "GetNextAnt_node_curAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };

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
			GRGEN_LGSP.PatternNode GetNextAnt_node_curAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "GetNextAnt_node_curAnt", "curAnt", GetNextAnt_node_curAnt_AllowedTypes, GetNextAnt_node_curAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode GetNextAnt_node_next = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "GetNextAnt_node_next", "next", GetNextAnt_node_next_AllowedTypes, GetNextAnt_node_next_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternEdge GetNextAnt_edge__edge0 = new GRGEN_LGSP.PatternEdge(true, (int) GRGEN_MODEL.EdgeTypes.@NextAnt, "GRGEN_MODEL.INextAnt", "GetNextAnt_edge__edge0", "_edge0", GetNextAnt_edge__edge0_AllowedTypes, GetNextAnt_edge__edge0_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_GetNextAnt = new GRGEN_LGSP.PatternGraph(
				"GetNextAnt",
				"",
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
			GRGEN_MODEL.IAnt node_curAnt { get; }
			GRGEN_MODEL.IAnt node_next { get; }
			//Edges
			GRGEN_MODEL.INextAnt edge__edge0 { get; }
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_GetNextAnt : GRGEN_LGSP.ListElement<Match_GetNextAnt>, IMatch_GetNextAnt
		{
			public GRGEN_MODEL.IAnt node_curAnt { get { return (GRGEN_MODEL.IAnt)_node_curAnt; } }
			public GRGEN_MODEL.IAnt node_next { get { return (GRGEN_MODEL.IAnt)_node_next; } }
			public GRGEN_LGSP.LGSPNode _node_curAnt;
			public GRGEN_LGSP.LGSPNode _node_next;
			public enum GetNextAnt_NodeNums { @curAnt, @next, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)GetNextAnt_NodeNums.@curAnt: return _node_curAnt;
				case (int)GetNextAnt_NodeNums.@next: return _node_next;
				default: return null;
				}
			}
			
			public GRGEN_MODEL.INextAnt edge__edge0 { get { return (GRGEN_MODEL.INextAnt)_edge__edge0; } }
			public GRGEN_LGSP.LGSPEdge _edge__edge0;
			public enum GetNextAnt_EdgeNums { @_edge0, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 1;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				case (int)GetNextAnt_EdgeNums.@_edge0: return _edge__edge0;
				default: return null;
				}
			}
			
			public enum GetNextAnt_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GetNextAnt_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GetNextAnt_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GetNextAnt_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum GetNextAnt_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_GetNextAnt.instance.pat_GetNextAnt; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_GetNextAnt(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_GetNextAnt(Match_GetNextAnt that)
			{
				_node_curAnt = that._node_curAnt;
				_node_next = that._node_next;
				_edge__edge0 = that._edge__edge0;
			}
			public Match_GetNextAnt()
			{
			}
		}

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
		{
			name = "Food2Ant";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "Food2Ant_node_lastAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };

		}
		private void initialize()
		{
			bool[,] Food2Ant_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Food2Ant_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] Food2Ant_isNodeTotallyHomomorphic = new bool[2] { false, false,  };
			bool[] Food2Ant_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode Food2Ant_node_lastAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "Food2Ant_node_lastAnt", "lastAnt", Food2Ant_node_lastAnt_AllowedTypes, Food2Ant_node_lastAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternNode Food2Ant_node_hill = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@AntHill, "GRGEN_MODEL.IAntHill", "Food2Ant_node_hill", "hill", Food2Ant_node_hill_AllowedTypes, Food2Ant_node_hill_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			GRGEN_LGSP.PatternCondition Food2Ant_cond_0 = new GRGEN_LGSP.PatternCondition(
				new GRGEN_EXPR.GT(new GRGEN_EXPR.Qualification("GRGEN_MODEL.IAntHill", "Food2Ant_node_hill", "food"), new GRGEN_EXPR.Constant("0")),
				new string[] { "Food2Ant_node_hill" }, new string[] {  }, new string[] {  }, new GRGEN_LIBGR.VarType[] {  });
			pat_Food2Ant = new GRGEN_LGSP.PatternGraph(
				"Food2Ant",
				"",
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
			int tempvar_int = (inode_hill.@food - 1);
			graph.ChangingNodeAttribute(node_hill, GRGEN_MODEL.NodeType_GridNode.AttributeType_food, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_hill.@food = tempvar_int;
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
			GRGEN_MODEL.IAnt node_lastAnt { get; }
			GRGEN_MODEL.IAntHill node_hill { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_Food2Ant : GRGEN_LGSP.ListElement<Match_Food2Ant>, IMatch_Food2Ant
		{
			public GRGEN_MODEL.IAnt node_lastAnt { get { return (GRGEN_MODEL.IAnt)_node_lastAnt; } }
			public GRGEN_MODEL.IAntHill node_hill { get { return (GRGEN_MODEL.IAntHill)_node_hill; } }
			public GRGEN_LGSP.LGSPNode _node_lastAnt;
			public GRGEN_LGSP.LGSPNode _node_hill;
			public enum Food2Ant_NodeNums { @lastAnt, @hill, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 2;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)Food2Ant_NodeNums.@lastAnt: return _node_lastAnt;
				case (int)Food2Ant_NodeNums.@hill: return _node_hill;
				default: return null;
				}
			}
			
			public enum Food2Ant_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Food2Ant_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Food2Ant_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Food2Ant_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Food2Ant_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum Food2Ant_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_Food2Ant.instance.pat_Food2Ant; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_Food2Ant(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_Food2Ant(Match_Food2Ant that)
			{
				_node_lastAnt = that._node_lastAnt;
				_node_hill = that._node_hill;
			}
			public Match_Food2Ant()
			{
			}
		}

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
		{
			name = "EvaporateWorld";

			inputs = new GRGEN_LIBGR.GrGenType[] { };
			inputNames = new string[] { };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] EvaporateWorld_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] EvaporateWorld_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] EvaporateWorld_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] EvaporateWorld_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode EvaporateWorld_node_n = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@GridNode, "GRGEN_MODEL.IGridNode", "EvaporateWorld_node_n", "n", EvaporateWorld_node_n_AllowedTypes, EvaporateWorld_node_n_IsAllowedType, 5.5F, -1, false, null, null, null, null, null, false);
			pat_EvaporateWorld = new GRGEN_LGSP.PatternGraph(
				"EvaporateWorld",
				"",
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
			int tempvar_int = ((int) (((double) inode_n.@pheromones) * 0.95));
			graph.ChangingNodeAttribute(node_n, GRGEN_MODEL.NodeType_GridNode.AttributeType_pheromones, GRGEN_LIBGR.AttributeChangeType.Assign, tempvar_int, null);
			inode_n.@pheromones = tempvar_int;
			return;
		}
		private static string[] EvaporateWorld_addedNodeNames = new string[] {  };
		private static string[] EvaporateWorld_addedEdgeNames = new string[] {  };

		static Rule_EvaporateWorld() {
		}

		public interface IMatch_EvaporateWorld : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IGridNode node_n { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_EvaporateWorld : GRGEN_LGSP.ListElement<Match_EvaporateWorld>, IMatch_EvaporateWorld
		{
			public GRGEN_MODEL.IGridNode node_n { get { return (GRGEN_MODEL.IGridNode)_node_n; } }
			public GRGEN_LGSP.LGSPNode _node_n;
			public enum EvaporateWorld_NodeNums { @n, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)EvaporateWorld_NodeNums.@n: return _node_n;
				default: return null;
				}
			}
			
			public enum EvaporateWorld_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum EvaporateWorld_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum EvaporateWorld_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum EvaporateWorld_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum EvaporateWorld_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum EvaporateWorld_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_EvaporateWorld.instance.pat_EvaporateWorld; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_EvaporateWorld(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_EvaporateWorld(Match_EvaporateWorld that)
			{
				_node_n = that._node_n;
			}
			public Match_EvaporateWorld()
			{
			}
		}

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
		{
			name = "doAntWorld";

			inputs = new GRGEN_LIBGR.GrGenType[] { GRGEN_MODEL.NodeType_Ant.typeVar, };
			inputNames = new string[] { "doAntWorld_node_firstAnt", };
			defs = new GRGEN_LIBGR.GrGenType[] { };
			defNames = new string[] { };
			outputs = new GRGEN_LIBGR.GrGenType[] { };

		}
		private void initialize()
		{
			bool[,] doAntWorld_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] doAntWorld_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[] doAntWorld_isNodeTotallyHomomorphic = new bool[1] { false,  };
			bool[] doAntWorld_isEdgeTotallyHomomorphic = new bool[0] ;
			GRGEN_LGSP.PatternNode doAntWorld_node_firstAnt = new GRGEN_LGSP.PatternNode((int) GRGEN_MODEL.NodeTypes.@Ant, "GRGEN_MODEL.IAnt", "doAntWorld_node_firstAnt", "firstAnt", doAntWorld_node_firstAnt_AllowedTypes, doAntWorld_node_firstAnt_IsAllowedType, 5.5F, 0, false, null, null, null, null, null, false);
			pat_doAntWorld = new GRGEN_LGSP.PatternGraph(
				"doAntWorld",
				"",
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
            bool res_106;
            bool res_105;
            bool res_44;
            GRGEN_MODEL.IAnt var_curAnt = null;
            bool res_104;
            bool res_102;
            bool res_97;
            bool res_68;
            bool res_67;
            bool res_63;
            bool res_51;
            bool res_47;
            Action_TakeFood rule_TakeFood = Action_TakeFood.Instance;
            bool res_50;
            Action_GoHome rule_GoHome = Action_GoHome.Instance;
            bool res_62;
            bool res_54;
            Action_DropFood rule_DropFood = Action_DropFood.Instance;
            bool res_61;
            bool res_57;
            Action_SearchAlongPheromones rule_SearchAlongPheromones = Action_SearchAlongPheromones.Instance;
            bool res_60;
            Action_SearchAimless rule_SearchAimless = Action_SearchAimless.Instance;
            bool res_66;
            Action_GetNextAnt rule_GetNextAnt = Action_GetNextAnt.Instance;
            bool res_96;
            bool res_90;
            bool res_77;
            bool res_69;
            Action_ReachedEndOfWorldAnywhere rule_ReachedEndOfWorldAnywhere = Action_ReachedEndOfWorldAnywhere.Instance;
            GRGEN_MODEL.IGridNode var_cur = null;
            bool res_76;
            bool res_72;
            Action_GrowWorldFirstNotAtCorner rule_GrowWorldFirstNotAtCorner = Action_GrowWorldFirstNotAtCorner.Instance;
            GRGEN_MODEL.IGridNode var_curOuter = null;
            bool res_75;
            Action_GrowWorldFirstAtCorner rule_GrowWorldFirstAtCorner = Action_GrowWorldFirstAtCorner.Instance;
            bool res_89;
            bool res_88;
            bool res_82;
            Action_GrowWorldNextNotAtCorner rule_GrowWorldNextNotAtCorner = Action_GrowWorldNextNotAtCorner.Instance;
            bool res_87;
            Action_GrowWorldNextAtCorner rule_GrowWorldNextAtCorner = Action_GrowWorldNextAtCorner.Instance;
            bool res_95;
            Action_GrowWorldEnd rule_GrowWorldEnd = Action_GrowWorldEnd.Instance;
            bool res_101;
            bool res_100;
            Action_Food2Ant rule_Food2Ant = Action_Food2Ant.Instance;
            bool res_103;
            Action_EvaporateWorld rule_EvaporateWorld = Action_EvaporateWorld.Instance;
            long i_106 = 0;
            for(; i_106 < 50; i_106++)
            {
                var_curAnt = (GRGEN_MODEL.IAnt)(var_firstAnt);
                res_44 = (bool)(true);
                if(!res_44)
                    res_105 = (bool)(false);
                else
                {
                    long i_68 = 0;
                    while(true)
                    {
                        GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches_47 = rule_TakeFood.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                        procEnv.Matched(matches_47, false);
                        if(matches_47.Count==0) {
                            res_47 = (bool)(false);
                        } else {
                            res_47 = (bool)(true);
                            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_47.Count;
                            procEnv.Finishing(matches_47, false);
                            Rule_TakeFood.IMatch_TakeFood match_47 = matches_47.FirstExact;
                            rule_TakeFood.Modify(procEnv, match_47);
                            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_47, false);
                        }
                        GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches_50 = rule_GoHome.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                        procEnv.Matched(matches_50, false);
                        if(matches_50.Count==0) {
                            res_50 = (bool)(false);
                        } else {
                            res_50 = (bool)(true);
                            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_50.Count;
                            procEnv.Finishing(matches_50, false);
                            Rule_GoHome.IMatch_GoHome match_50 = matches_50.FirstExact;
                            rule_GoHome.Modify(procEnv, match_50);
                            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_50, false);
                        }
                        res_51 = (bool)(res_47 | res_50);
                        if(res_51)
                            res_63 = (bool)(true);
                        else
                        {
                            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches_54 = rule_DropFood.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                            procEnv.Matched(matches_54, false);
                            if(matches_54.Count==0) {
                                res_54 = (bool)(false);
                            } else {
                                res_54 = (bool)(true);
                                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_54.Count;
                                procEnv.Finishing(matches_54, false);
                                Rule_DropFood.IMatch_DropFood match_54 = matches_54.FirstExact;
                                rule_DropFood.Modify(procEnv, match_54);
                                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_54, false);
                            }
                            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches_57 = rule_SearchAlongPheromones.Match(procEnv, procEnv.MaxMatches, (GRGEN_MODEL.IAnt)var_curAnt);
                            procEnv.Matched(matches_57, false);
                            if(matches_57.Count==0) {
                                res_57 = (bool)(false);
                            } else {
                                res_57 = (bool)(true);
                                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_57.Count;
                                procEnv.Finishing(matches_57, false);
                                int numchooserandomvar_57 = (int)1;
                                if(matches_57.Count < numchooserandomvar_57) numchooserandomvar_57 = matches_57.Count;
                                for(int i = 0; i < numchooserandomvar_57; ++i)
                                {
                                    if(i != 0) procEnv.RewritingNextMatch();
                                    Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match_57 = matches_57.RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(matches_57.Count));
                                    rule_SearchAlongPheromones.Modify(procEnv, match_57);
                                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;
                                }
                                procEnv.Finished(matches_57, false);
                            }
                            if(res_57)
                                res_61 = (bool)(true);
                            else
                            {
                                GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches_60 = rule_SearchAimless.Match(procEnv, procEnv.MaxMatches, (GRGEN_MODEL.IAnt)var_curAnt);
                                procEnv.Matched(matches_60, false);
                                if(matches_60.Count==0) {
                                    res_60 = (bool)(false);
                                } else {
                                    res_60 = (bool)(true);
                                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_60.Count;
                                    procEnv.Finishing(matches_60, false);
                                    int numchooserandomvar_60 = (int)1;
                                    if(matches_60.Count < numchooserandomvar_60) numchooserandomvar_60 = matches_60.Count;
                                    for(int i = 0; i < numchooserandomvar_60; ++i)
                                    {
                                        if(i != 0) procEnv.RewritingNextMatch();
                                        Rule_SearchAimless.IMatch_SearchAimless match_60 = matches_60.RemoveMatchExact(GRGEN_LIBGR.Sequence.randomGenerator.Next(matches_60.Count));
                                        rule_SearchAimless.Modify(procEnv, match_60);
                                        if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;
                                    }
                                    procEnv.Finished(matches_60, false);
                                }
                                res_61 = (bool)(res_60);
                            }
                            res_62 = (bool)(res_54 | res_61);
                            res_63 = (bool)(res_62);
                        }
                        if(!res_63)
                            res_67 = (bool)(false);
                        else
                        {
                            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches_66 = rule_GetNextAnt.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                            procEnv.Matched(matches_66, false);
                            if(matches_66.Count==0) {
                                res_66 = (bool)(false);
                            } else {
                                res_66 = (bool)(true);
                                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_66.Count;
                                procEnv.Finishing(matches_66, false);
                                Rule_GetNextAnt.IMatch_GetNextAnt match_66 = matches_66.FirstExact;
                                GRGEN_MODEL.IAnt tmpvar_curAnt; 
                                rule_GetNextAnt.Modify(procEnv, match_66, out tmpvar_curAnt);
                                var_curAnt = (GRGEN_MODEL.IAnt)(tmpvar_curAnt);

                                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_66, false);
                            }
                            res_67 = (bool)(res_66);
                        }
                        if(!res_67) break;
                        i_68++;
                    }
                    res_68 = (bool)(i_68 >= 0);
                    GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches_69 = rule_ReachedEndOfWorldAnywhere.Match(procEnv, 1);
                    procEnv.Matched(matches_69, false);
                    if(matches_69.Count==0) {
                        res_69 = (bool)(false);
                    } else {
                        res_69 = (bool)(true);
                        if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_69.Count;
                        procEnv.Finishing(matches_69, false);
                        Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match_69 = matches_69.FirstExact;
                        GRGEN_MODEL.IGridNode tmpvar_cur; 
                        rule_ReachedEndOfWorldAnywhere.Modify(procEnv, match_69, out tmpvar_cur);
                        var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_cur);

                        if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                        procEnv.Finished(matches_69, false);
                    }
                    if(!res_69)
                        res_77 = (bool)(false);
                    else
                    {
                        GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches_72 = rule_GrowWorldFirstNotAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur);
                        procEnv.Matched(matches_72, false);
                        if(matches_72.Count==0) {
                            res_72 = (bool)(false);
                        } else {
                            res_72 = (bool)(true);
                            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_72.Count;
                            procEnv.Finishing(matches_72, false);
                            Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match_72 = matches_72.FirstExact;
                            GRGEN_MODEL.IGridNode tmpvar_cur; GRGEN_MODEL.IGridNode tmpvar_curOuter; 
                            rule_GrowWorldFirstNotAtCorner.Modify(procEnv, match_72, out tmpvar_cur, out tmpvar_curOuter);
                            var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_curOuter);

                            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_72, false);
                        }
                        if(res_72)
                            res_76 = (bool)(true);
                        else
                        {
                            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches_75 = rule_GrowWorldFirstAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur);
                            procEnv.Matched(matches_75, false);
                            if(matches_75.Count==0) {
                                res_75 = (bool)(false);
                            } else {
                                res_75 = (bool)(true);
                                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_75.Count;
                                procEnv.Finishing(matches_75, false);
                                Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match_75 = matches_75.FirstExact;
                                GRGEN_MODEL.IGridNode tmpvar_cur; GRGEN_MODEL.IGridNode tmpvar_curOuter; 
                                rule_GrowWorldFirstAtCorner.Modify(procEnv, match_75, out tmpvar_cur, out tmpvar_curOuter);
                                var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_curOuter);

                                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_75, false);
                            }
                            res_76 = (bool)(res_75);
                        }
                        res_77 = (bool)(res_76);
                    }
                    if(!res_77)
                        res_90 = (bool)(false);
                    else
                    {
                        long i_89 = 0;
                        while(true)
                        {
                            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches_82 = rule_GrowWorldNextNotAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur, (GRGEN_MODEL.IGridNode)var_curOuter);
                            procEnv.Matched(matches_82, false);
                            if(matches_82.Count==0) {
                                res_82 = (bool)(false);
                            } else {
                                res_82 = (bool)(true);
                                if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_82.Count;
                                procEnv.Finishing(matches_82, false);
                                Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match_82 = matches_82.FirstExact;
                                GRGEN_MODEL.IGridNode tmpvar_cur; GRGEN_MODEL.IGridNode tmpvar_curOuter; 
                                rule_GrowWorldNextNotAtCorner.Modify(procEnv, match_82, out tmpvar_cur, out tmpvar_curOuter);
                                var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_curOuter);

                                if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                                procEnv.Finished(matches_82, false);
                            }
                            if(res_82)
                                res_88 = (bool)(true);
                            else
                            {
                                GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches_87 = rule_GrowWorldNextAtCorner.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur, (GRGEN_MODEL.IGridNode)var_curOuter);
                                procEnv.Matched(matches_87, false);
                                if(matches_87.Count==0) {
                                    res_87 = (bool)(false);
                                } else {
                                    res_87 = (bool)(true);
                                    if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_87.Count;
                                    procEnv.Finishing(matches_87, false);
                                    Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match_87 = matches_87.FirstExact;
                                    GRGEN_MODEL.IGridNode tmpvar_cur; GRGEN_MODEL.IGridNode tmpvar_curOuter; 
                                    rule_GrowWorldNextAtCorner.Modify(procEnv, match_87, out tmpvar_cur, out tmpvar_curOuter);
                                    var_cur = (GRGEN_MODEL.IGridNode)(tmpvar_cur);
var_curOuter = (GRGEN_MODEL.IGridNode)(tmpvar_curOuter);

                                    if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                                    procEnv.Finished(matches_87, false);
                                }
                                res_88 = (bool)(res_87);
                            }
                            if(!res_88) break;
                            i_89++;
                        }
                        res_89 = (bool)(i_89 >= 0);
                        res_90 = (bool)(res_89);
                    }
                    if(!res_90)
                        res_96 = (bool)(false);
                    else
                    {
                        GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches_95 = rule_GrowWorldEnd.Match(procEnv, 1, (GRGEN_MODEL.IGridNode)var_cur, (GRGEN_MODEL.IGridNode)var_curOuter);
                        procEnv.Matched(matches_95, false);
                        if(matches_95.Count==0) {
                            res_95 = (bool)(false);
                        } else {
                            res_95 = (bool)(true);
                            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_95.Count;
                            procEnv.Finishing(matches_95, false);
                            Rule_GrowWorldEnd.IMatch_GrowWorldEnd match_95 = matches_95.FirstExact;
                            rule_GrowWorldEnd.Modify(procEnv, match_95);
                            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_95, false);
                        }
                        res_96 = (bool)(res_95);
                    }
                    res_97 = (bool)(res_68 | res_96);
                    long i_101 = 0;
                    while(true)
                    {
                        GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches_100 = rule_Food2Ant.Match(procEnv, 1, (GRGEN_MODEL.IAnt)var_curAnt);
                        procEnv.Matched(matches_100, false);
                        if(matches_100.Count==0) {
                            res_100 = (bool)(false);
                        } else {
                            res_100 = (bool)(true);
                            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_100.Count;
                            procEnv.Finishing(matches_100, false);
                            Rule_Food2Ant.IMatch_Food2Ant match_100 = matches_100.FirstExact;
                            GRGEN_MODEL.IAnt tmpvar_curAnt; 
                            rule_Food2Ant.Modify(procEnv, match_100, out tmpvar_curAnt);
                            var_curAnt = (GRGEN_MODEL.IAnt)(tmpvar_curAnt);

                            if(procEnv.PerformanceInfo != null) procEnv.PerformanceInfo.RewritesPerformed++;
                            procEnv.Finished(matches_100, false);
                        }
                        if(!res_100) break;
                        i_101++;
                    }
                    res_101 = (bool)(i_101 >= 0);
                    res_102 = (bool)(res_97 | res_101);
                    GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches_103 = rule_EvaporateWorld.Match(procEnv, procEnv.MaxMatches);
                    procEnv.Matched(matches_103, false);
                    if(matches_103.Count==0) {
                        res_103 = (bool)(false);
                    } else {
                        res_103 = (bool)(true);
                        if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.MatchesFound += matches_103.Count;
                        procEnv.Finishing(matches_103, false);
                        IEnumerator<Rule_EvaporateWorld.IMatch_EvaporateWorld> enum_103 = matches_103.GetEnumeratorExact();
                        while(enum_103.MoveNext())
                        {
                            Rule_EvaporateWorld.IMatch_EvaporateWorld match_103 = enum_103.Current;
                            if(match_103!=matches_103.FirstExact) procEnv.RewritingNextMatch();
                            rule_EvaporateWorld.Modify(procEnv, match_103);
                            if(procEnv.PerformanceInfo!=null) procEnv.PerformanceInfo.RewritesPerformed++;
                        }
                        procEnv.Finished(matches_103, false);
                    }
                    res_104 = (bool)(res_102 | res_103);
                    res_105 = (bool)(res_104);
                }
                if(!res_105) break;
            }
            res_106 = (bool)(i_106 >= 50);
            return res_106;
        }

		static Rule_doAntWorld() {
		}

		public interface IMatch_doAntWorld : GRGEN_LIBGR.IMatch
		{
			//Nodes
			GRGEN_MODEL.IAnt node_firstAnt { get; }
			//Edges
			//Variables
			//EmbeddedGraphs
			//Alternatives
			//Iterateds
			//Independents
			// further match object stuff
			void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern);
		}

		public class Match_doAntWorld : GRGEN_LGSP.ListElement<Match_doAntWorld>, IMatch_doAntWorld
		{
			public GRGEN_MODEL.IAnt node_firstAnt { get { return (GRGEN_MODEL.IAnt)_node_firstAnt; } }
			public GRGEN_LGSP.LGSPNode _node_firstAnt;
			public enum doAntWorld_NodeNums { @firstAnt, END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.INode> Nodes { get { return new GRGEN_LGSP.Nodes_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.INode> NodesEnumerator { get { return new GRGEN_LGSP.Nodes_Enumerator(this); } }
			public int NumberOfNodes { get { return 1;} }
			public GRGEN_LIBGR.INode getNodeAt(int index)
			{
				switch(index) {
				case (int)doAntWorld_NodeNums.@firstAnt: return _node_firstAnt;
				default: return null;
				}
			}
			
			public enum doAntWorld_EdgeNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IEdge> Edges { get { return new GRGEN_LGSP.Edges_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IEdge> EdgesEnumerator { get { return new GRGEN_LGSP.Edges_Enumerator(this); } }
			public int NumberOfEdges { get { return 0;} }
			public GRGEN_LIBGR.IEdge getEdgeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum doAntWorld_VariableNums { END_OF_ENUM };
			public IEnumerable<object> Variables { get { return new GRGEN_LGSP.Variables_Enumerable(this); } }
			public IEnumerator<object> VariablesEnumerator { get { return new GRGEN_LGSP.Variables_Enumerator(this); } }
			public int NumberOfVariables { get { return 0;} }
			public object getVariableAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum doAntWorld_SubNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> EmbeddedGraphs { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> EmbeddedGraphsEnumerator { get { return new GRGEN_LGSP.EmbeddedGraphs_Enumerator(this); } }
			public int NumberOfEmbeddedGraphs { get { return 0;} }
			public GRGEN_LIBGR.IMatch getEmbeddedGraphAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum doAntWorld_AltNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Alternatives { get { return new GRGEN_LGSP.Alternatives_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> AlternativesEnumerator { get { return new GRGEN_LGSP.Alternatives_Enumerator(this); } }
			public int NumberOfAlternatives { get { return 0;} }
			public GRGEN_LIBGR.IMatch getAlternativeAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum doAntWorld_IterNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatches> Iterateds { get { return new GRGEN_LGSP.Iterateds_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatches> IteratedsEnumerator { get { return new GRGEN_LGSP.Iterateds_Enumerator(this); } }
			public int NumberOfIterateds { get { return 0;} }
			public GRGEN_LIBGR.IMatches getIteratedAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public enum doAntWorld_IdptNums { END_OF_ENUM };
			public IEnumerable<GRGEN_LIBGR.IMatch> Independents { get { return new GRGEN_LGSP.Independents_Enumerable(this); } }
			public IEnumerator<GRGEN_LIBGR.IMatch> IndependentsEnumerator { get { return new GRGEN_LGSP.Independents_Enumerator(this); } }
			public int NumberOfIndependents { get { return 0;} }
			public GRGEN_LIBGR.IMatch getIndependentAt(int index)
			{
				switch(index) {
				default: return null;
				}
			}
			
			public GRGEN_LIBGR.IPatternGraph Pattern { get { return Rule_doAntWorld.instance.pat_doAntWorld; } }
			public GRGEN_LIBGR.IMatch MatchOfEnclosingPattern { get { return _matchOfEnclosingPattern; } }
			public GRGEN_LIBGR.IMatch Clone() { return new Match_doAntWorld(this); }
			public GRGEN_LIBGR.IMatch _matchOfEnclosingPattern;
			public void SetMatchOfEnclosingPattern(GRGEN_LIBGR.IMatch matchOfEnclosingPattern) { _matchOfEnclosingPattern = matchOfEnclosingPattern; }
			public override string ToString() { return "Match of " + Pattern.Name; }

			public Match_doAntWorld(Match_doAntWorld that)
			{
				_node_firstAnt = that._node_firstAnt;
			}
			public Match_doAntWorld()
			{
			}
		}

	}

	public class AntWorld_ExtendAtEndOfRound_NoGammel_RuleAndMatchingPatterns : GRGEN_LGSP.LGSPRuleAndMatchingPatterns
	{
		public AntWorld_ExtendAtEndOfRound_NoGammel_RuleAndMatchingPatterns()
		{
			subpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0];
			rules = new GRGEN_LGSP.LGSPRulePattern[18];
			rulesAndSubpatterns = new GRGEN_LGSP.LGSPMatchingPattern[0+18];
			definedSequences = new GRGEN_LIBGR.DefinedSequenceInfo[0];
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches, out GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_InitWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_InitWorld
    {
        public Action_InitWorld() {
            _rulePattern = Rule_InitWorld.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_InitWorld.Match_InitWorld, Rule_InitWorld.IMatch_InitWorld>(this);
        }

        public Rule_InitWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "InitWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_InitWorld.Match_InitWorld, Rule_InitWorld.IMatch_InitWorld> matches;

        public static Action_InitWorld Instance { get { return instance; } }
        private static Action_InitWorld instance = new Action_InitWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
        public GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_InitWorld.IMatch_InitWorld match, out GRGEN_MODEL.IAnt output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches, out GRGEN_MODEL.IAnt output_0)
        {
            output_0 = null;
            foreach(Rule_InitWorld.IMatch_InitWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_InitWorld.IMatch_InitWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IAnt output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_InitWorld.IMatch_InitWorld>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt TakeFood_node_curAnt);
    }
    
    public class Action_TakeFood : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_TakeFood
    {
        public Action_TakeFood() {
            _rulePattern = Rule_TakeFood.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_TakeFood.Match_TakeFood, Rule_TakeFood.IMatch_TakeFood>(this);
        }

        public Rule_TakeFood _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "TakeFood"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_TakeFood.Match_TakeFood, Rule_TakeFood.IMatch_TakeFood> matches;

        public static Action_TakeFood Instance { get { return instance; } }
        private static Action_TakeFood instance = new Action_TakeFood();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
            foreach(Rule_TakeFood.IMatch_TakeFood match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, TakeFood_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt TakeFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, TakeFood_node_curAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_TakeFood.IMatch_TakeFood match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_TakeFood.IMatch_TakeFood>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt GoHome_node_curAnt);
    }
    
    public class Action_GoHome : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GoHome
    {
        public Action_GoHome() {
            _rulePattern = Rule_GoHome.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GoHome.Match_GoHome, Rule_GoHome.IMatch_GoHome>(this);
        }

        public Rule_GoHome _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GoHome"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GoHome.Match_GoHome, Rule_GoHome.IMatch_GoHome> matches;

        public static Action_GoHome Instance { get { return instance; } }
        private static Action_GoHome instance = new Action_GoHome();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
                    prev__candidate_GoHome_node_old = candidate_GoHome_node_old.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_GoHome_node_old.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                            if((candidate_GoHome_node_new.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                candidate_GoHome_node_old.lgspFlags = candidate_GoHome_node_old.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GoHome_node_old;
                                return matches;
                            }
                        }
                        while( (candidate_GoHome_edge__edge0 = candidate_GoHome_edge__edge0.lgspOutNext) != head_candidate_GoHome_edge__edge0 );
                    }
                    candidate_GoHome_node_old.lgspFlags = candidate_GoHome_node_old.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GoHome_node_old;
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
            foreach(Rule_GoHome.IMatch_GoHome match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GoHome_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GoHome_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GoHome_node_curAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_GoHome.IMatch_GoHome match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GoHome.IMatch_GoHome>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt DropFood_node_curAnt);
    }
    
    public class Action_DropFood : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_DropFood
    {
        public Action_DropFood() {
            _rulePattern = Rule_DropFood.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_DropFood.Match_DropFood, Rule_DropFood.IMatch_DropFood>(this);
        }

        public Rule_DropFood _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "DropFood"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_DropFood.Match_DropFood, Rule_DropFood.IMatch_DropFood> matches;

        public static Action_DropFood Instance { get { return instance; } }
        private static Action_DropFood instance = new Action_DropFood();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
            foreach(Rule_DropFood.IMatch_DropFood match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, DropFood_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt DropFood_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, DropFood_node_curAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_DropFood.IMatch_DropFood match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_DropFood.IMatch_DropFood>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt);
    }
    
    public class Action_SearchAlongPheromones : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_SearchAlongPheromones
    {
        public Action_SearchAlongPheromones() {
            _rulePattern = Rule_SearchAlongPheromones.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_SearchAlongPheromones.Match_SearchAlongPheromones, Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones>(this);
        }

        public Rule_SearchAlongPheromones _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "SearchAlongPheromones"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_SearchAlongPheromones.Match_SearchAlongPheromones, Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches;

        public static Action_SearchAlongPheromones Instance { get { return instance; } }
        private static Action_SearchAlongPheromones instance = new Action_SearchAlongPheromones();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
                    prev__candidate_SearchAlongPheromones_node_old = candidate_SearchAlongPheromones_node_old.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_SearchAlongPheromones_node_old.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                            if((candidate_SearchAlongPheromones_node_new.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                candidate_SearchAlongPheromones_node_old.lgspFlags = candidate_SearchAlongPheromones_node_old.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_SearchAlongPheromones_node_old;
                                return matches;
                            }
                        }
                        while( (candidate_SearchAlongPheromones_edge__edge0 = candidate_SearchAlongPheromones_edge__edge0.lgspInNext) != head_candidate_SearchAlongPheromones_edge__edge0 );
                    }
                    candidate_SearchAlongPheromones_node_old.lgspFlags = candidate_SearchAlongPheromones_node_old.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_SearchAlongPheromones_node_old;
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
            foreach(Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAlongPheromones_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAlongPheromones_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, SearchAlongPheromones_node_curAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_SearchAlongPheromones.IMatch_SearchAlongPheromones>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt SearchAimless_node_curAnt);
    }
    
    public class Action_SearchAimless : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_SearchAimless
    {
        public Action_SearchAimless() {
            _rulePattern = Rule_SearchAimless.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_SearchAimless.Match_SearchAimless, Rule_SearchAimless.IMatch_SearchAimless>(this);
        }

        public Rule_SearchAimless _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "SearchAimless"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_SearchAimless.Match_SearchAimless, Rule_SearchAimless.IMatch_SearchAimless> matches;

        public static Action_SearchAimless Instance { get { return instance; } }
        private static Action_SearchAimless instance = new Action_SearchAimless();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
                    prev__candidate_SearchAimless_node_old = candidate_SearchAimless_node_old.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_SearchAimless_node_old.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                                if((candidate_SearchAimless_node_new.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                                    candidate_SearchAimless_node_old.lgspFlags = candidate_SearchAimless_node_old.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_SearchAimless_node_old;
                                    return matches;
                                }
                            }
                            while( (directionRunCounterOf_SearchAimless_edge__edge0==0 ? candidate_SearchAimless_edge__edge0 = candidate_SearchAimless_edge__edge0.lgspInNext : candidate_SearchAimless_edge__edge0 = candidate_SearchAimless_edge__edge0.lgspOutNext) != head_candidate_SearchAimless_edge__edge0 );
                        }
                    }
                    candidate_SearchAimless_node_old.lgspFlags = candidate_SearchAimless_node_old.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_SearchAimless_node_old;
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
            foreach(Rule_SearchAimless.IMatch_SearchAimless match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, SearchAimless_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt SearchAimless_node_curAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, SearchAimless_node_curAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_SearchAimless.IMatch_SearchAimless match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_SearchAimless.IMatch_SearchAimless>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches, out GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, ref GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, ref GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt);
    }
    
    public class Action_ReachedEndOfWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ReachedEndOfWorld
    {
        public Action_ReachedEndOfWorld() {
            _rulePattern = Rule_ReachedEndOfWorld.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorld.Match_ReachedEndOfWorld, Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld>(this);
        }

        public Rule_ReachedEndOfWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ReachedEndOfWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorld.Match_ReachedEndOfWorld, Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches;

        public static Action_ReachedEndOfWorld Instance { get { return instance; } }
        private static Action_ReachedEndOfWorld instance = new Action_ReachedEndOfWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
                        ++negLevel;
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
                                --negLevel;
                                goto label0;
                            }
                            while( (candidate_ReachedEndOfWorld_neg_0_edge__edge0 = candidate_ReachedEndOfWorld_neg_0_edge__edge0.lgspInNext) != head_candidate_ReachedEndOfWorld_neg_0_edge__edge0 );
                        }
                        --negLevel;
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
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ReachedEndOfWorld_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld match, out GRGEN_MODEL.IGridNode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches, out GRGEN_MODEL.IGridNode output_0)
        {
            output_0 = null;
            foreach(Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, ref GRGEN_MODEL.IGridNode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, ReachedEndOfWorld_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt ReachedEndOfWorld_node_curAnt, ref GRGEN_MODEL.IGridNode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, ReachedEndOfWorld_node_curAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IGridNode output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorld.IMatch_ReachedEndOfWorld>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0], ref output_0)) {
                ReturnArray[0] = output_0;
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches, out GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IGridNode output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_ReachedEndOfWorldAnywhere : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_ReachedEndOfWorldAnywhere
    {
        public Action_ReachedEndOfWorldAnywhere() {
            _rulePattern = Rule_ReachedEndOfWorldAnywhere.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorldAnywhere.Match_ReachedEndOfWorldAnywhere, Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere>(this);
        }

        public Rule_ReachedEndOfWorldAnywhere _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "ReachedEndOfWorldAnywhere"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_ReachedEndOfWorldAnywhere.Match_ReachedEndOfWorldAnywhere, Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches;

        public static Action_ReachedEndOfWorldAnywhere Instance { get { return instance; } }
        private static Action_ReachedEndOfWorldAnywhere instance = new Action_ReachedEndOfWorldAnywhere();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
                    ++negLevel;
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
                            --negLevel;
                            goto label1;
                        }
                        while( (candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0 = candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0.lgspInNext) != head_candidate_ReachedEndOfWorldAnywhere_neg_0_edge__edge0 );
                    }
                    --negLevel;
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
        public GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match, out GRGEN_MODEL.IGridNode output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches, out GRGEN_MODEL.IGridNode output_0)
        {
            output_0 = null;
            foreach(Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IGridNode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, ref GRGEN_MODEL.IGridNode output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IGridNode output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_ReachedEndOfWorldAnywhere.IMatch_ReachedEndOfWorldAnywhere>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_MODEL.IGridNode output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, ref output_0)) {
                ReturnArray[0] = output_0;
                return ReturnArray;
            }
            else return null;
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; 
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val);
    }
    
    public class Action_GrowFoodIfEqual : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowFoodIfEqual
    {
        public Action_GrowFoodIfEqual() {
            _rulePattern = Rule_GrowFoodIfEqual.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowFoodIfEqual.Match_GrowFoodIfEqual, Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>(this);
        }

        public Rule_GrowFoodIfEqual _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowFoodIfEqual"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowFoodIfEqual.Match_GrowFoodIfEqual, Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches;

        public static Action_GrowFoodIfEqual Instance { get { return instance; } }
        private static Action_GrowFoodIfEqual instance = new Action_GrowFoodIfEqual();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            System.Int32 var_GrowFoodIfEqual_var_val = (System.Int32)GrowFoodIfEqual_var_val;
            // Preset GrowFoodIfEqual_node_n 
            GRGEN_LGSP.LGSPNode candidate_GrowFoodIfEqual_node_n = (GRGEN_LGSP.LGSPNode)GrowFoodIfEqual_node_n;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowFoodIfEqual_node_n.lgspType.TypeID]) {
                return matches;
            }
            uint prev__candidate_GrowFoodIfEqual_node_n;
            prev__candidate_GrowFoodIfEqual_node_n = candidate_GrowFoodIfEqual_node_n.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowFoodIfEqual_node_n.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Lookup GrowFoodIfEqual_node_hill 
            int type_id_candidate_GrowFoodIfEqual_node_hill = 3;
            for(GRGEN_LGSP.LGSPNode head_candidate_GrowFoodIfEqual_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowFoodIfEqual_node_hill], candidate_GrowFoodIfEqual_node_hill = head_candidate_GrowFoodIfEqual_node_hill.lgspTypeNext; candidate_GrowFoodIfEqual_node_hill != head_candidate_GrowFoodIfEqual_node_hill; candidate_GrowFoodIfEqual_node_hill = candidate_GrowFoodIfEqual_node_hill.lgspTypeNext)
            {
                if((candidate_GrowFoodIfEqual_node_hill.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                    candidate_GrowFoodIfEqual_node_n.lgspFlags = candidate_GrowFoodIfEqual_node_n.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowFoodIfEqual_node_n;
                    return matches;
                }
            }
            candidate_GrowFoodIfEqual_node_n.lgspFlags = candidate_GrowFoodIfEqual_node_n.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowFoodIfEqual_node_n;
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
            foreach(Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowFoodIfEqual_node_n, System.Int32 GrowFoodIfEqual_var_val)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowFoodIfEqual_node_n, GrowFoodIfEqual_var_val);
            if(matches.Count <= 0) return false;
            foreach(Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowFoodIfEqual.IMatch_GrowFoodIfEqual>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (System.Int32) parameters[1])) {
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
    }
    
    public class Action_GrowWorldFirstAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldFirstAtCorner
    {
        public Action_GrowWorldFirstAtCorner() {
            _rulePattern = Rule_GrowWorldFirstAtCorner.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstAtCorner.Match_GrowWorldFirstAtCorner, Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner>(this);
        }

        public Rule_GrowWorldFirstAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldFirstAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstAtCorner.Match_GrowWorldFirstAtCorner, Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches;

        public static Action_GrowWorldFirstAtCorner Instance { get { return instance; } }
        private static Action_GrowWorldFirstAtCorner instance = new Action_GrowWorldFirstAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Preset GrowWorldFirstAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldFirstAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldFirstAtCorner_node_cur;
            if(candidate_GrowWorldFirstAtCorner_node_cur.lgspType.TypeID!=2) {
                return matches;
            }
            uint prev__candidate_GrowWorldFirstAtCorner_node_cur;
            prev__candidate_GrowWorldFirstAtCorner_node_cur = candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                    if((candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldFirstAtCorner_node_next;
                    prev__candidate_GrowWorldFirstAtCorner_node_next = candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_GrowWorldFirstAtCorner_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Lookup GrowWorldFirstAtCorner_node_hill 
                    int type_id_candidate_GrowWorldFirstAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldFirstAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldFirstAtCorner_node_hill], candidate_GrowWorldFirstAtCorner_node_hill = head_candidate_GrowWorldFirstAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldFirstAtCorner_node_hill != head_candidate_GrowWorldFirstAtCorner_node_hill; candidate_GrowWorldFirstAtCorner_node_hill = candidate_GrowWorldFirstAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldFirstAtCorner_node_hill.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                            candidate_GrowWorldFirstAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstAtCorner_node_next;
                            candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldFirstAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstAtCorner_node_next;
                }
                while( (candidate_GrowWorldFirstAtCorner_edge__edge0 = candidate_GrowWorldFirstAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldFirstAtCorner_edge__edge0 );
            }
            candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstAtCorner_node_cur);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            output_0 = null;
            output_1 = null;
            foreach(Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            foreach(Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstAtCorner.IMatch_GrowWorldFirstAtCorner>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
    }
    
    public class Action_GrowWorldFirstNotAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldFirstNotAtCorner
    {
        public Action_GrowWorldFirstNotAtCorner() {
            _rulePattern = Rule_GrowWorldFirstNotAtCorner.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstNotAtCorner.Match_GrowWorldFirstNotAtCorner, Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner>(this);
        }

        public Rule_GrowWorldFirstNotAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldFirstNotAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldFirstNotAtCorner.Match_GrowWorldFirstNotAtCorner, Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches;

        public static Action_GrowWorldFirstNotAtCorner Instance { get { return instance; } }
        private static Action_GrowWorldFirstNotAtCorner instance = new Action_GrowWorldFirstNotAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Preset GrowWorldFirstNotAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldFirstNotAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldFirstNotAtCorner_node_cur;
            if(candidate_GrowWorldFirstNotAtCorner_node_cur.lgspType.TypeID!=1 && candidate_GrowWorldFirstNotAtCorner_node_cur.lgspType.TypeID!=3) {
                return matches;
            }
            uint prev__candidate_GrowWorldFirstNotAtCorner_node_cur;
            prev__candidate_GrowWorldFirstNotAtCorner_node_cur = candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                    if((candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldFirstNotAtCorner_node_next;
                    prev__candidate_GrowWorldFirstNotAtCorner_node_next = candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Lookup GrowWorldFirstNotAtCorner_node_hill 
                    int type_id_candidate_GrowWorldFirstNotAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldFirstNotAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldFirstNotAtCorner_node_hill], candidate_GrowWorldFirstNotAtCorner_node_hill = head_candidate_GrowWorldFirstNotAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldFirstNotAtCorner_node_hill != head_candidate_GrowWorldFirstNotAtCorner_node_hill; candidate_GrowWorldFirstNotAtCorner_node_hill = candidate_GrowWorldFirstNotAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldFirstNotAtCorner_node_hill.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                            candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstNotAtCorner_node_next;
                            candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstNotAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstNotAtCorner_node_next;
                }
                while( (candidate_GrowWorldFirstNotAtCorner_edge__edge0 = candidate_GrowWorldFirstNotAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldFirstNotAtCorner_edge__edge0 );
            }
            candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldFirstNotAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldFirstNotAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstNotAtCorner_node_cur);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            output_0 = null;
            output_1 = null;
            foreach(Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldFirstNotAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldFirstNotAtCorner_node_cur, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldFirstNotAtCorner_node_cur);
            if(matches.Count <= 0) return false;
            foreach(Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldFirstNotAtCorner.IMatch_GrowWorldFirstNotAtCorner>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
    }
    
    public class Action_GrowWorldNextAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldNextAtCorner
    {
        public Action_GrowWorldNextAtCorner() {
            _rulePattern = Rule_GrowWorldNextAtCorner.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextAtCorner.Match_GrowWorldNextAtCorner, Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner>(this);
        }

        public Rule_GrowWorldNextAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldNextAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextAtCorner.Match_GrowWorldNextAtCorner, Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches;

        public static Action_GrowWorldNextAtCorner Instance { get { return instance; } }
        private static Action_GrowWorldNextAtCorner instance = new Action_GrowWorldNextAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Preset GrowWorldNextAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldNextAtCorner_node_cur;
            if(candidate_GrowWorldNextAtCorner_node_cur.lgspType.TypeID!=2) {
                return matches;
            }
            uint prev__candidate_GrowWorldNextAtCorner_node_cur;
            prev__candidate_GrowWorldNextAtCorner_node_cur = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldNextAtCorner_node_cur.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // NegativePattern 
            {
                ++negLevel;
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
                        --negLevel;
                        candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                        return matches;
                    }
                    while( (candidate_GrowWorldNextAtCorner_neg_0_edge__edge0 = candidate_GrowWorldNextAtCorner_neg_0_edge__edge0.lgspInNext) != head_candidate_GrowWorldNextAtCorner_neg_0_edge__edge0 );
                }
                --negLevel;
            }
            // Preset GrowWorldNextAtCorner_node_curOuter 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextAtCorner_node_curOuter = (GRGEN_LGSP.LGSPNode)GrowWorldNextAtCorner_node_curOuter;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldNextAtCorner_node_curOuter.lgspType.TypeID]) {
                candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                return matches;
            }
            if((candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                return matches;
            }
            uint prev__candidate_GrowWorldNextAtCorner_node_curOuter;
            prev__candidate_GrowWorldNextAtCorner_node_curOuter = candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                    if((candidate_GrowWorldNextAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldNextAtCorner_node_next;
                    prev__candidate_GrowWorldNextAtCorner_node_next = candidate_GrowWorldNextAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_GrowWorldNextAtCorner_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Lookup GrowWorldNextAtCorner_node_hill 
                    int type_id_candidate_GrowWorldNextAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldNextAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldNextAtCorner_node_hill], candidate_GrowWorldNextAtCorner_node_hill = head_candidate_GrowWorldNextAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldNextAtCorner_node_hill != head_candidate_GrowWorldNextAtCorner_node_hill; candidate_GrowWorldNextAtCorner_node_hill = candidate_GrowWorldNextAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldNextAtCorner_node_hill.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                            candidate_GrowWorldNextAtCorner_node_next.lgspFlags = candidate_GrowWorldNextAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_next;
                            candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_curOuter;
                            candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldNextAtCorner_node_next.lgspFlags = candidate_GrowWorldNextAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_next;
                }
                while( (candidate_GrowWorldNextAtCorner_edge__edge0 = candidate_GrowWorldNextAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldNextAtCorner_edge__edge0 );
            }
            candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextAtCorner_node_curOuter.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_curOuter;
            candidate_GrowWorldNextAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            output_0 = null;
            output_1 = null;
            foreach(Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextAtCorner_node_cur, GrowWorldNextAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            foreach(Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextAtCorner.IMatch_GrowWorldNextAtCorner>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
    }
    
    public class Action_GrowWorldNextNotAtCorner : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldNextNotAtCorner
    {
        public Action_GrowWorldNextNotAtCorner() {
            _rulePattern = Rule_GrowWorldNextNotAtCorner.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[2];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextNotAtCorner.Match_GrowWorldNextNotAtCorner, Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner>(this);
        }

        public Rule_GrowWorldNextNotAtCorner _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldNextNotAtCorner"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldNextNotAtCorner.Match_GrowWorldNextNotAtCorner, Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches;

        public static Action_GrowWorldNextNotAtCorner Instance { get { return instance; } }
        private static Action_GrowWorldNextNotAtCorner instance = new Action_GrowWorldNextNotAtCorner();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Preset GrowWorldNextNotAtCorner_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextNotAtCorner_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldNextNotAtCorner_node_cur;
            if(candidate_GrowWorldNextNotAtCorner_node_cur.lgspType.TypeID!=1 && candidate_GrowWorldNextNotAtCorner_node_cur.lgspType.TypeID!=3) {
                return matches;
            }
            uint prev__candidate_GrowWorldNextNotAtCorner_node_cur;
            prev__candidate_GrowWorldNextNotAtCorner_node_cur = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // NegativePattern 
            {
                ++negLevel;
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
                        --negLevel;
                        candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                        return matches;
                    }
                    while( (candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0 = candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0.lgspInNext) != head_candidate_GrowWorldNextNotAtCorner_neg_0_edge__edge0 );
                }
                --negLevel;
            }
            // Preset GrowWorldNextNotAtCorner_node_curOuter 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldNextNotAtCorner_node_curOuter = (GRGEN_LGSP.LGSPNode)GrowWorldNextNotAtCorner_node_curOuter;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspType.TypeID]) {
                candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                return matches;
            }
            if((candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                return matches;
            }
            uint prev__candidate_GrowWorldNextNotAtCorner_node_curOuter;
            prev__candidate_GrowWorldNextNotAtCorner_node_curOuter = candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                    if((candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
                    {
                        continue;
                    }
                    uint prev__candidate_GrowWorldNextNotAtCorner_node_next;
                    prev__candidate_GrowWorldNextNotAtCorner_node_next = candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
                    // Lookup GrowWorldNextNotAtCorner_node_hill 
                    int type_id_candidate_GrowWorldNextNotAtCorner_node_hill = 3;
                    for(GRGEN_LGSP.LGSPNode head_candidate_GrowWorldNextNotAtCorner_node_hill = graph.nodesByTypeHeads[type_id_candidate_GrowWorldNextNotAtCorner_node_hill], candidate_GrowWorldNextNotAtCorner_node_hill = head_candidate_GrowWorldNextNotAtCorner_node_hill.lgspTypeNext; candidate_GrowWorldNextNotAtCorner_node_hill != head_candidate_GrowWorldNextNotAtCorner_node_hill; candidate_GrowWorldNextNotAtCorner_node_hill = candidate_GrowWorldNextNotAtCorner_node_hill.lgspTypeNext)
                    {
                        if((candidate_GrowWorldNextNotAtCorner_node_hill.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                            candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_next;
                            candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_curOuter;
                            candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
                            return matches;
                        }
                    }
                    candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_next.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_next;
                }
                while( (candidate_GrowWorldNextNotAtCorner_edge__edge0 = candidate_GrowWorldNextNotAtCorner_edge__edge0.lgspOutNext) != head_candidate_GrowWorldNextNotAtCorner_edge__edge0 );
            }
            candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_curOuter.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_curOuter;
            candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags = candidate_GrowWorldNextNotAtCorner_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldNextNotAtCorner_node_cur;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches, out GRGEN_MODEL.IGridNode output_0, out GRGEN_MODEL.IGridNode output_1)
        {
            output_0 = null;
            output_1 = null;
            foreach(Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0, out output_1);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_cur, GRGEN_MODEL.IGridNode GrowWorldNextNotAtCorner_node_curOuter, ref GRGEN_MODEL.IGridNode output_0, ref GRGEN_MODEL.IGridNode output_1)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldNextNotAtCorner_node_cur, GrowWorldNextNotAtCorner_node_curOuter);
            if(matches.Count <= 0) return false;
            foreach(Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0, out output_1);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IGridNode output_0; GRGEN_MODEL.IGridNode output_1; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldNextNotAtCorner.IMatch_GrowWorldNextNotAtCorner>)matches, out output_0, out output_1);
            ReturnArray[0] = output_0;
            ReturnArray[1] = output_1;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IGridNode output_0 = null; GRGEN_MODEL.IGridNode output_1 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1], ref output_0, ref output_1)) {
                ReturnArray[0] = output_0;
                ReturnArray[1] = output_1;
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter);
    }
    
    public class Action_GrowWorldEnd : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GrowWorldEnd
    {
        public Action_GrowWorldEnd() {
            _rulePattern = Rule_GrowWorldEnd.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldEnd.Match_GrowWorldEnd, Rule_GrowWorldEnd.IMatch_GrowWorldEnd>(this);
        }

        public Rule_GrowWorldEnd _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GrowWorldEnd"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GrowWorldEnd.Match_GrowWorldEnd, Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches;

        public static Action_GrowWorldEnd Instance { get { return instance; } }
        private static Action_GrowWorldEnd instance = new Action_GrowWorldEnd();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Preset GrowWorldEnd_node_cur 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldEnd_node_cur = (GRGEN_LGSP.LGSPNode)GrowWorldEnd_node_cur;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldEnd_node_cur.lgspType.TypeID]) {
                return matches;
            }
            uint prev__candidate_GrowWorldEnd_node_cur;
            prev__candidate_GrowWorldEnd_node_cur = candidate_GrowWorldEnd_node_cur.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldEnd_node_cur.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            // Preset GrowWorldEnd_node_curOuter 
            GRGEN_LGSP.LGSPNode candidate_GrowWorldEnd_node_curOuter = (GRGEN_LGSP.LGSPNode)GrowWorldEnd_node_curOuter;
            if(!GRGEN_MODEL.NodeType_GridNode.isMyType[candidate_GrowWorldEnd_node_curOuter.lgspType.TypeID]) {
                candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldEnd_node_cur;
                return matches;
            }
            if((candidate_GrowWorldEnd_node_curOuter.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
            {
                candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldEnd_node_cur;
                return matches;
            }
            uint prev__candidate_GrowWorldEnd_node_curOuter;
            prev__candidate_GrowWorldEnd_node_curOuter = candidate_GrowWorldEnd_node_curOuter.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GrowWorldEnd_node_curOuter.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                    if((candidate_GrowWorldEnd_node_nextOuter.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                        candidate_GrowWorldEnd_node_curOuter.lgspFlags = candidate_GrowWorldEnd_node_curOuter.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldEnd_node_curOuter;
                        candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldEnd_node_cur;
                        return matches;
                    }
                }
                while( (candidate_GrowWorldEnd_edge__edge0 = candidate_GrowWorldEnd_edge__edge0.lgspInNext) != head_candidate_GrowWorldEnd_edge__edge0 );
            }
            candidate_GrowWorldEnd_node_curOuter.lgspFlags = candidate_GrowWorldEnd_node_curOuter.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldEnd_node_curOuter;
            candidate_GrowWorldEnd_node_cur.lgspFlags = candidate_GrowWorldEnd_node_cur.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GrowWorldEnd_node_cur;
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
            foreach(Rule_GrowWorldEnd.IMatch_GrowWorldEnd match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IGridNode GrowWorldEnd_node_cur, GRGEN_MODEL.IGridNode GrowWorldEnd_node_curOuter)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GrowWorldEnd_node_cur, GrowWorldEnd_node_curOuter);
            if(matches.Count <= 0) return false;
            foreach(Rule_GrowWorldEnd.IMatch_GrowWorldEnd match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GrowWorldEnd.IMatch_GrowWorldEnd>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IGridNode) parameters[0], (GRGEN_MODEL.IGridNode) parameters[1])) {
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches, out GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
    }
    
    public class Action_GetNextAnt : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_GetNextAnt
    {
        public Action_GetNextAnt() {
            _rulePattern = Rule_GetNextAnt.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_GetNextAnt.Match_GetNextAnt, Rule_GetNextAnt.IMatch_GetNextAnt>(this);
        }

        public Rule_GetNextAnt _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "GetNextAnt"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_GetNextAnt.Match_GetNextAnt, Rule_GetNextAnt.IMatch_GetNextAnt> matches;

        public static Action_GetNextAnt Instance { get { return instance; } }
        private static Action_GetNextAnt instance = new Action_GetNextAnt();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
            // Preset GetNextAnt_node_curAnt 
            GRGEN_LGSP.LGSPNode candidate_GetNextAnt_node_curAnt = (GRGEN_LGSP.LGSPNode)GetNextAnt_node_curAnt;
            if(candidate_GetNextAnt_node_curAnt.lgspType.TypeID!=4) {
                return matches;
            }
            uint prev__candidate_GetNextAnt_node_curAnt;
            prev__candidate_GetNextAnt_node_curAnt = candidate_GetNextAnt_node_curAnt.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
            candidate_GetNextAnt_node_curAnt.lgspFlags |= (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel;
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
                    if((candidate_GetNextAnt_node_next.lgspFlags & (uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) != 0)
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
                        candidate_GetNextAnt_node_curAnt.lgspFlags = candidate_GetNextAnt_node_curAnt.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GetNextAnt_node_curAnt;
                        return matches;
                    }
                }
                while( (candidate_GetNextAnt_edge__edge0 = candidate_GetNextAnt_edge__edge0.lgspOutNext) != head_candidate_GetNextAnt_edge__edge0 );
            }
            candidate_GetNextAnt_node_curAnt.lgspFlags = candidate_GetNextAnt_node_curAnt.lgspFlags & ~((uint) GRGEN_LGSP.LGSPElemFlags.IS_MATCHED << negLevel) | prev__candidate_GetNextAnt_node_curAnt;
            return matches;
        }
        /// <summary> Type of the matcher method (with parameters processing environment containing host graph, maximum number of matches to search for (zero=unlimited), and rule parameters; returning found matches). </summary>
        public delegate GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> MatchInvoker(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt);
        /// <summary> A delegate pointing to the current matcher program for this rule. </summary>
        public MatchInvoker DynamicMatch;
        /// <summary> The RulePattern object from which this LGSPAction object has been created. </summary>
        public GRGEN_LIBGR.IRulePattern RulePattern { get { return _rulePattern; } }
        public GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GetNextAnt_node_curAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_GetNextAnt.IMatch_GetNextAnt match, out GRGEN_MODEL.IAnt output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches, out GRGEN_MODEL.IAnt output_0)
        {
            output_0 = null;
            foreach(Rule_GetNextAnt.IMatch_GetNextAnt match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, GetNextAnt_node_curAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt GetNextAnt_node_curAnt, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, GetNextAnt_node_curAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_GetNextAnt.IMatch_GetNextAnt match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IAnt output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_GetNextAnt.IMatch_GetNextAnt>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0], ref output_0)) {
                ReturnArray[0] = output_0;
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
        void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches, out GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.Apply, but with exact types and distinct parameters; returns true if applied </summary>
        bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, ref GRGEN_MODEL.IAnt output_0);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt);
    }
    
    public class Action_Food2Ant : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_Food2Ant
    {
        public Action_Food2Ant() {
            _rulePattern = Rule_Food2Ant.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[1];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_Food2Ant.Match_Food2Ant, Rule_Food2Ant.IMatch_Food2Ant>(this);
        }

        public Rule_Food2Ant _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "Food2Ant"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_Food2Ant.Match_Food2Ant, Rule_Food2Ant.IMatch_Food2Ant> matches;

        public static Action_Food2Ant Instance { get { return instance; } }
        private static Action_Food2Ant instance = new Action_Food2Ant();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
        public GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> Match(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt)
        {
            return DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, Food2Ant_node_lastAnt);
        }
        public void Modify(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, Rule_Food2Ant.IMatch_Food2Ant match, out GRGEN_MODEL.IAnt output_0)
        {
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public void ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches, out GRGEN_MODEL.IAnt output_0)
        {
            output_0 = null;
            foreach(Rule_Food2Ant.IMatch_Food2Ant match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, Food2Ant_node_lastAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First, out output_0);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt Food2Ant_node_lastAnt, ref GRGEN_MODEL.IAnt output_0)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, Food2Ant_node_lastAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_Food2Ant.IMatch_Food2Ant match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match, out output_0);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            GRGEN_MODEL.IAnt output_0; 
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_Food2Ant.IMatch_Food2Ant>)matches, out output_0);
            ReturnArray[0] = output_0;
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            GRGEN_MODEL.IAnt output_0 = null; 
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0], ref output_0)) {
                ReturnArray[0] = output_0;
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max);
    }
    
    public class Action_EvaporateWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_EvaporateWorld
    {
        public Action_EvaporateWorld() {
            _rulePattern = Rule_EvaporateWorld.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_EvaporateWorld.Match_EvaporateWorld, Rule_EvaporateWorld.IMatch_EvaporateWorld>(this);
        }

        public Rule_EvaporateWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "EvaporateWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_EvaporateWorld.Match_EvaporateWorld, Rule_EvaporateWorld.IMatch_EvaporateWorld> matches;

        public static Action_EvaporateWorld Instance { get { return instance; } }
        private static Action_EvaporateWorld instance = new Action_EvaporateWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
            foreach(Rule_EvaporateWorld.IMatch_EvaporateWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches);
            if(matches.Count <= 0) return false;
            foreach(Rule_EvaporateWorld.IMatch_EvaporateWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_EvaporateWorld.IMatch_EvaporateWorld>)matches);
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
        /// <summary> same as IAction.ApplyAll, but with exact types and distinct parameters; returns true if applied at least once. </summary>
        bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.ApplyStar, but with exact types and distinct parameters. </summary>
        bool ApplyStar(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.ApplyPlus, but with exact types and distinct parameters. </summary>
        bool ApplyPlus(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
        /// <summary> same as IAction.ApplyMinMax, but with exact types and distinct parameters. </summary>
        bool ApplyMinMax(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, int min, int max, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt);
    }
    
    public class Action_doAntWorld : GRGEN_LGSP.LGSPAction, GRGEN_LIBGR.IAction, IAction_doAntWorld
    {
        public Action_doAntWorld() {
            _rulePattern = Rule_doAntWorld.Instance;
            patternGraph = _rulePattern.patternGraph;
            DynamicMatch = myMatch;
            ReturnArray = new object[0];
            matches = new GRGEN_LGSP.LGSPMatchesList<Rule_doAntWorld.Match_doAntWorld, Rule_doAntWorld.IMatch_doAntWorld>(this);
        }

        public Rule_doAntWorld _rulePattern;
        public override GRGEN_LGSP.LGSPRulePattern rulePattern { get { return _rulePattern; } }
        public override string Name { get { return "doAntWorld"; } }
        private GRGEN_LGSP.LGSPMatchesList<Rule_doAntWorld.Match_doAntWorld, Rule_doAntWorld.IMatch_doAntWorld> matches;

        public static Action_doAntWorld Instance { get { return instance; } }
        private static Action_doAntWorld instance = new Action_doAntWorld();
        
        public GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> myMatch(GRGEN_LGSP.LGSPActionExecutionEnvironment actionEnv, int maxMatches, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LGSP.LGSPGraph graph = actionEnv.graph;
            matches.Clear();
            int negLevel = 0;
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
            foreach(Rule_doAntWorld.IMatch_doAntWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
        }
        public bool Apply(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, 1, doAntWorld_node_firstAnt);
            if(matches.Count <= 0) return false;
            _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, matches.First);
            return true;
        }
        public bool ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_MODEL.IAnt doAntWorld_node_firstAnt)
        {
            GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld> matches = DynamicMatch((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, maxMatches, doAntWorld_node_firstAnt);
            if(matches.Count <= 0) return false;
            foreach(Rule_doAntWorld.IMatch_doAntWorld match in matches) _rulePattern.Modify((GRGEN_LGSP.LGSPActionExecutionEnvironment)actionEnv, match);
            return true;
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
        public object[] ModifyAll(GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, GRGEN_LIBGR.IMatches matches)
        {
            
            ModifyAll(actionEnv, (GRGEN_LIBGR.IMatchesExact<Rule_doAntWorld.IMatch_doAntWorld>)matches);
            return ReturnArray;
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
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv)
        {
            throw new Exception();
        }
        object[] GRGEN_LIBGR.IAction.ApplyAll(int maxMatches, GRGEN_LIBGR.IActionExecutionEnvironment actionEnv, params object[] parameters)
        {
            
            if(ApplyAll(maxMatches, actionEnv, (GRGEN_MODEL.IAnt) parameters[0])) {
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
            GRGEN_LGSP.PatternGraphAnalyzer analyzer = new GRGEN_LGSP.PatternGraphAnalyzer();
            analyzer.AnalyzeNestingOfAndRemember(Rule_InitWorld.Instance);
            actions.Add("InitWorld", (GRGEN_LGSP.LGSPAction) Action_InitWorld.Instance);
            @InitWorld = Action_InitWorld.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_TakeFood.Instance);
            actions.Add("TakeFood", (GRGEN_LGSP.LGSPAction) Action_TakeFood.Instance);
            @TakeFood = Action_TakeFood.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GoHome.Instance);
            actions.Add("GoHome", (GRGEN_LGSP.LGSPAction) Action_GoHome.Instance);
            @GoHome = Action_GoHome.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_DropFood.Instance);
            actions.Add("DropFood", (GRGEN_LGSP.LGSPAction) Action_DropFood.Instance);
            @DropFood = Action_DropFood.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_SearchAlongPheromones.Instance);
            actions.Add("SearchAlongPheromones", (GRGEN_LGSP.LGSPAction) Action_SearchAlongPheromones.Instance);
            @SearchAlongPheromones = Action_SearchAlongPheromones.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_SearchAimless.Instance);
            actions.Add("SearchAimless", (GRGEN_LGSP.LGSPAction) Action_SearchAimless.Instance);
            @SearchAimless = Action_SearchAimless.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_ReachedEndOfWorld.Instance);
            actions.Add("ReachedEndOfWorld", (GRGEN_LGSP.LGSPAction) Action_ReachedEndOfWorld.Instance);
            @ReachedEndOfWorld = Action_ReachedEndOfWorld.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_ReachedEndOfWorldAnywhere.Instance);
            actions.Add("ReachedEndOfWorldAnywhere", (GRGEN_LGSP.LGSPAction) Action_ReachedEndOfWorldAnywhere.Instance);
            @ReachedEndOfWorldAnywhere = Action_ReachedEndOfWorldAnywhere.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GrowFoodIfEqual.Instance);
            actions.Add("GrowFoodIfEqual", (GRGEN_LGSP.LGSPAction) Action_GrowFoodIfEqual.Instance);
            @GrowFoodIfEqual = Action_GrowFoodIfEqual.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GrowWorldFirstAtCorner.Instance);
            actions.Add("GrowWorldFirstAtCorner", (GRGEN_LGSP.LGSPAction) Action_GrowWorldFirstAtCorner.Instance);
            @GrowWorldFirstAtCorner = Action_GrowWorldFirstAtCorner.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GrowWorldFirstNotAtCorner.Instance);
            actions.Add("GrowWorldFirstNotAtCorner", (GRGEN_LGSP.LGSPAction) Action_GrowWorldFirstNotAtCorner.Instance);
            @GrowWorldFirstNotAtCorner = Action_GrowWorldFirstNotAtCorner.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GrowWorldNextAtCorner.Instance);
            actions.Add("GrowWorldNextAtCorner", (GRGEN_LGSP.LGSPAction) Action_GrowWorldNextAtCorner.Instance);
            @GrowWorldNextAtCorner = Action_GrowWorldNextAtCorner.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GrowWorldNextNotAtCorner.Instance);
            actions.Add("GrowWorldNextNotAtCorner", (GRGEN_LGSP.LGSPAction) Action_GrowWorldNextNotAtCorner.Instance);
            @GrowWorldNextNotAtCorner = Action_GrowWorldNextNotAtCorner.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GrowWorldEnd.Instance);
            actions.Add("GrowWorldEnd", (GRGEN_LGSP.LGSPAction) Action_GrowWorldEnd.Instance);
            @GrowWorldEnd = Action_GrowWorldEnd.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_GetNextAnt.Instance);
            actions.Add("GetNextAnt", (GRGEN_LGSP.LGSPAction) Action_GetNextAnt.Instance);
            @GetNextAnt = Action_GetNextAnt.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_Food2Ant.Instance);
            actions.Add("Food2Ant", (GRGEN_LGSP.LGSPAction) Action_Food2Ant.Instance);
            @Food2Ant = Action_Food2Ant.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_EvaporateWorld.Instance);
            actions.Add("EvaporateWorld", (GRGEN_LGSP.LGSPAction) Action_EvaporateWorld.Instance);
            @EvaporateWorld = Action_EvaporateWorld.Instance;
            analyzer.AnalyzeNestingOfAndRemember(Rule_doAntWorld.Instance);
            actions.Add("doAntWorld", (GRGEN_LGSP.LGSPAction) Action_doAntWorld.Instance);
            @doAntWorld = Action_doAntWorld.Instance;
            analyzer.ComputeInterPatternRelations();
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_InitWorld.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_TakeFood.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GoHome.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_DropFood.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_SearchAlongPheromones.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_SearchAimless.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_ReachedEndOfWorld.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_ReachedEndOfWorldAnywhere.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GrowFoodIfEqual.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GrowWorldFirstAtCorner.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GrowWorldFirstNotAtCorner.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GrowWorldNextAtCorner.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GrowWorldNextNotAtCorner.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GrowWorldEnd.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_GetNextAnt.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_Food2Ant.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_EvaporateWorld.Instance);
            analyzer.AnalyzeWithInterPatternRelationsKnown(Rule_doAntWorld.Instance);
        }
        
        public IAction_InitWorld @InitWorld;
        public IAction_TakeFood @TakeFood;
        public IAction_GoHome @GoHome;
        public IAction_DropFood @DropFood;
        public IAction_SearchAlongPheromones @SearchAlongPheromones;
        public IAction_SearchAimless @SearchAimless;
        public IAction_ReachedEndOfWorld @ReachedEndOfWorld;
        public IAction_ReachedEndOfWorldAnywhere @ReachedEndOfWorldAnywhere;
        public IAction_GrowFoodIfEqual @GrowFoodIfEqual;
        public IAction_GrowWorldFirstAtCorner @GrowWorldFirstAtCorner;
        public IAction_GrowWorldFirstNotAtCorner @GrowWorldFirstNotAtCorner;
        public IAction_GrowWorldNextAtCorner @GrowWorldNextAtCorner;
        public IAction_GrowWorldNextNotAtCorner @GrowWorldNextNotAtCorner;
        public IAction_GrowWorldEnd @GrowWorldEnd;
        public IAction_GetNextAnt @GetNextAnt;
        public IAction_Food2Ant @Food2Ant;
        public IAction_EvaporateWorld @EvaporateWorld;
        public IAction_doAntWorld @doAntWorld;
        
        
        public override string Name { get { return "AntWorld_ExtendAtEndOfRound_NoGammelActions"; } }
        public override string ModelMD5Hash { get { return "5efeccfb37eb4c2835fae110fe22d2e7"; } }
    }
}