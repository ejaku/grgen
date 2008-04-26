// This file has been generated automatically by GrGen.
// Do not modify this file! Any changes will be lost!
// Generated from "..\..\tests\Recursive\Recursive.grg" on Sat Apr 26 03:35:33 CEST 2008

using System;
using System.Collections.Generic;
using System.Text;
using de.unika.ipd.grGen.libGr;
using de.unika.ipd.grGen.lgsp;
using de.unika.ipd.grGen.Model_Std;

namespace de.unika.ipd.grGen.Action_Recursive
{
	public class Pattern_ChainFromTo : LGSPMatchingPattern
	{
		private static Pattern_ChainFromTo instance = null;
		public static Pattern_ChainFromTo Instance { get { if (instance==null) { instance = new Pattern_ChainFromTo(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFromTo_node_from_AllowedTypes = null;
		public static NodeType[] ChainFromTo_node_to_AllowedTypes = null;
		public static bool[] ChainFromTo_node_from_IsAllowedType = null;
		public static bool[] ChainFromTo_node_to_IsAllowedType = null;
		public enum ChainFromTo_NodeNums { @from, @to, };
		public enum ChainFromTo_EdgeNums { };
		public enum ChainFromTo_VariableNums { };
		public enum ChainFromTo_SubNums { };
		public enum ChainFromTo_AltNums { @alt_0, };
		PatternGraph pat_ChainFromTo;

		public enum ChainFromTo_alt_0_CaseNums { @base, @rec, };
		public static EdgeType[] ChainFromTo_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromTo_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_base_VariableNums { };
		public enum ChainFromTo_alt_0_base_SubNums { };
		public enum ChainFromTo_alt_0_base_AltNums { };
		PatternGraph ChainFromTo_alt_0_base;

		public static NodeType[] ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static EdgeType[] ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromTo_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromTo_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromTo_alt_0_rec_VariableNums { };
		public enum ChainFromTo_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFromTo_alt_0_rec_AltNums { };
		PatternGraph ChainFromTo_alt_0_rec;


#if INITIAL_WARMUP
		public Pattern_ChainFromTo()
#else
		private Pattern_ChainFromTo()
#endif
		{
			name = "ChainFromTo";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromTo_node_from", "ChainFromTo_node_to", };
		}
		public override void initialize()
		{
			bool[,] ChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode ChainFromTo_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFromTo_node_from", "from", ChainFromTo_node_from_AllowedTypes, ChainFromTo_node_from_IsAllowedType, 5.5F, 0);
			PatternNode ChainFromTo_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFromTo_node_to", "to", ChainFromTo_node_to_AllowedTypes, ChainFromTo_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ChainFromTo_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromTo_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternEdge ChainFromTo_alt_0_base_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromTo_alt_0_base_edge__edge0", "_edge0", ChainFromTo_alt_0_base_edge__edge0_AllowedTypes, ChainFromTo_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromTo_alt_0_base = new PatternGraph(
				"base",
				"ChainFromTo_alt_0_",
				false,
				new PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new PatternEdge[] { ChainFromTo_alt_0_base_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromTo_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromTo_alt_0_base_isEdgeHomomorphicGlobal
			);
			ChainFromTo_alt_0_base.edgeToSourceNode.Add(ChainFromTo_alt_0_base_edge__edge0, ChainFromTo_node_from);
			ChainFromTo_alt_0_base.edgeToTargetNode.Add(ChainFromTo_alt_0_base_edge__edge0, ChainFromTo_node_to);

			bool[,] ChainFromTo_alt_0_rec_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ChainFromTo_alt_0_rec_node_intermediate = new PatternNode((int) NodeTypes.@Node, "ChainFromTo_alt_0_rec_node_intermediate", "intermediate", ChainFromTo_alt_0_rec_node_intermediate_AllowedTypes, ChainFromTo_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromTo_alt_0_rec_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromTo_alt_0_rec_edge__edge0", "_edge0", ChainFromTo_alt_0_rec_edge__edge0_AllowedTypes, ChainFromTo_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFromTo_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromTo.Instance, new PatternElement[] { ChainFromTo_alt_0_rec_node_intermediate, ChainFromTo_node_to });
			ChainFromTo_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFromTo_alt_0_",
				false,
				new PatternNode[] { ChainFromTo_node_from, ChainFromTo_alt_0_rec_node_intermediate, ChainFromTo_node_to }, 
				new PatternEdge[] { ChainFromTo_alt_0_rec_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { ChainFromTo_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromTo_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromTo_alt_0_rec.edgeToSourceNode.Add(ChainFromTo_alt_0_rec_edge__edge0, ChainFromTo_node_from);
			ChainFromTo_alt_0_rec.edgeToTargetNode.Add(ChainFromTo_alt_0_rec_edge__edge0, ChainFromTo_alt_0_rec_node_intermediate);

			Alternative ChainFromTo_alt_0 = new Alternative( "alt_0", "ChainFromTo_", new PatternGraph[] { ChainFromTo_alt_0_base, ChainFromTo_alt_0_rec } );

			pat_ChainFromTo = new PatternGraph(
				"ChainFromTo",
				"",
				false,
				new PatternNode[] { ChainFromTo_node_from, ChainFromTo_node_to }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFromTo_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ChainFromTo_isNodeHomomorphicGlobal,
				ChainFromTo_isEdgeHomomorphicGlobal
			);
			ChainFromTo_alt_0_base.embeddingGraph = pat_ChainFromTo;
			ChainFromTo_alt_0_rec.embeddingGraph = pat_ChainFromTo;

			ChainFromTo_node_from.PointOfDefinition = null;
			ChainFromTo_node_to.PointOfDefinition = null;
			ChainFromTo_alt_0_base_edge__edge0.PointOfDefinition = ChainFromTo_alt_0_base;
			ChainFromTo_alt_0_rec_node_intermediate.PointOfDefinition = ChainFromTo_alt_0_rec;
			ChainFromTo_alt_0_rec_edge__edge0.PointOfDefinition = ChainFromTo_alt_0_rec;
			ChainFromTo_alt_0_rec__subpattern0.PointOfDefinition = ChainFromTo_alt_0_rec;

			patternGraph = pat_ChainFromTo;
		}



		public void ChainFromTo_Create(LGSPGraph graph, LGSPNode node_from, LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ChainFromTo_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromTo_addedEdgeNames );
		}
		private static String[] create_ChainFromTo_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromTo_addedEdgeNames = new String[] {  };

		public void ChainFromTo_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromTo_AltNums.@alt_0 + 0];
			ChainFromTo_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromTo_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ChainFromTo_alt_0_base) {
				ChainFromTo_alt_0_base_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == ChainFromTo_alt_0_rec) {
				ChainFromTo_alt_0_rec_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromTo_alt_0_base_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromTo_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromTo_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
		}
		private static String[] create_ChainFromTo_alt_0_base_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFromTo_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromTo_alt_0_base_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromTo_alt_0_base_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
		}

		public void ChainFromTo_alt_0_rec_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromTo_alt_0_rec_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_intermediate = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromTo_alt_0_rec_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_intermediate);
			Pattern_ChainFromTo.Instance.ChainFromTo_Create(graph, node_intermediate, node_to);
		}
		private static String[] create_ChainFromTo_alt_0_rec_addedNodeNames = new String[] { "from", "intermediate", "to" };
		private static String[] create_ChainFromTo_alt_0_rec_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromTo_alt_0_rec_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ChainFromTo_alt_0_rec_NodeNums.@intermediate];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromTo_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)ChainFromTo_alt_0_rec_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ChainFromTo.Instance.ChainFromTo_Delete(graph, subpattern__subpattern0);
		}
	}

	public class Pattern_ChainFrom : LGSPMatchingPattern
	{
		private static Pattern_ChainFrom instance = null;
		public static Pattern_ChainFrom Instance { get { if (instance==null) { instance = new Pattern_ChainFrom(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFrom_node_from_AllowedTypes = null;
		public static bool[] ChainFrom_node_from_IsAllowedType = null;
		public enum ChainFrom_NodeNums { @from, };
		public enum ChainFrom_EdgeNums { };
		public enum ChainFrom_VariableNums { };
		public enum ChainFrom_SubNums { };
		public enum ChainFrom_AltNums { @alt_0, };
		PatternGraph pat_ChainFrom;

		public enum ChainFrom_alt_0_CaseNums { @base, @rec, };
		public enum ChainFrom_alt_0_base_NodeNums { };
		public enum ChainFrom_alt_0_base_EdgeNums { };
		public enum ChainFrom_alt_0_base_VariableNums { };
		public enum ChainFrom_alt_0_base_SubNums { };
		public enum ChainFrom_alt_0_base_AltNums { };
		PatternGraph ChainFrom_alt_0_base;

		public static NodeType[] ChainFrom_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_node_to_IsAllowedType = null;
		public static EdgeType[] ChainFrom_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFrom_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFrom_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFrom_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFrom_alt_0_rec_VariableNums { };
		public enum ChainFrom_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFrom_alt_0_rec_AltNums { };
		PatternGraph ChainFrom_alt_0_rec;


#if INITIAL_WARMUP
		public Pattern_ChainFrom()
#else
		private Pattern_ChainFrom()
#endif
		{
			name = "ChainFrom";

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFrom_node_from", };
		}
		public override void initialize()
		{
			bool[,] ChainFrom_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFrom_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode ChainFrom_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFrom_node_from", "from", ChainFrom_node_from_AllowedTypes, ChainFrom_node_from_IsAllowedType, 5.5F, 0);
			bool[,] ChainFrom_alt_0_base_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] ChainFrom_alt_0_base_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			ChainFrom_alt_0_base = new PatternGraph(
				"base",
				"ChainFrom_alt_0_",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				ChainFrom_alt_0_base_isNodeHomomorphicGlobal,
				ChainFrom_alt_0_base_isEdgeHomomorphicGlobal
			);

			bool[,] ChainFrom_alt_0_rec_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFrom_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ChainFrom_alt_0_rec_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFrom_alt_0_rec_node_to", "to", ChainFrom_alt_0_rec_node_to_AllowedTypes, ChainFrom_alt_0_rec_node_to_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFrom_alt_0_rec_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFrom_alt_0_rec_edge__edge0", "_edge0", ChainFrom_alt_0_rec_edge__edge0_AllowedTypes, ChainFrom_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFrom_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFrom.Instance, new PatternElement[] { ChainFrom_alt_0_rec_node_to });
			ChainFrom_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFrom_alt_0_",
				false,
				new PatternNode[] { ChainFrom_node_from, ChainFrom_alt_0_rec_node_to }, 
				new PatternEdge[] { ChainFrom_alt_0_rec_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { ChainFrom_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFrom_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFrom_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFrom_alt_0_rec.edgeToSourceNode.Add(ChainFrom_alt_0_rec_edge__edge0, ChainFrom_node_from);
			ChainFrom_alt_0_rec.edgeToTargetNode.Add(ChainFrom_alt_0_rec_edge__edge0, ChainFrom_alt_0_rec_node_to);

			Alternative ChainFrom_alt_0 = new Alternative( "alt_0", "ChainFrom_", new PatternGraph[] { ChainFrom_alt_0_base, ChainFrom_alt_0_rec } );

			pat_ChainFrom = new PatternGraph(
				"ChainFrom",
				"",
				false,
				new PatternNode[] { ChainFrom_node_from }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFrom_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFrom_isNodeHomomorphicGlobal,
				ChainFrom_isEdgeHomomorphicGlobal
			);
			ChainFrom_alt_0_base.embeddingGraph = pat_ChainFrom;
			ChainFrom_alt_0_rec.embeddingGraph = pat_ChainFrom;

			ChainFrom_node_from.PointOfDefinition = null;
			ChainFrom_alt_0_rec_node_to.PointOfDefinition = ChainFrom_alt_0_rec;
			ChainFrom_alt_0_rec_edge__edge0.PointOfDefinition = ChainFrom_alt_0_rec;
			ChainFrom_alt_0_rec__subpattern0.PointOfDefinition = ChainFrom_alt_0_rec;

			patternGraph = pat_ChainFrom;
		}



		public void ChainFrom_Create(LGSPGraph graph, LGSPNode node_from)
		{
			graph.SettingAddedNodeNames( create_ChainFrom_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFrom_addedEdgeNames );
		}
		private static String[] create_ChainFrom_addedNodeNames = new String[] {  };
		private static String[] create_ChainFrom_addedEdgeNames = new String[] {  };

		public void ChainFrom_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFrom_AltNums.@alt_0 + 0];
			ChainFrom_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFrom_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ChainFrom_alt_0_base) {
				ChainFrom_alt_0_base_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == ChainFrom_alt_0_rec) {
				ChainFrom_alt_0_rec_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFrom_alt_0_base_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFrom_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFrom_alt_0_base_addedEdgeNames );
		}
		private static String[] create_ChainFrom_alt_0_base_addedNodeNames = new String[] {  };
		private static String[] create_ChainFrom_alt_0_base_addedEdgeNames = new String[] {  };

		public void ChainFrom_alt_0_base_Delete(LGSPGraph graph, LGSPMatch match)
		{
		}

		public void ChainFrom_alt_0_rec_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFrom_alt_0_rec_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFrom_alt_0_rec_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
			Pattern_ChainFrom.Instance.ChainFrom_Create(graph, node_to);
		}
		private static String[] create_ChainFrom_alt_0_rec_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFrom_alt_0_rec_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFrom_alt_0_rec_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_to = match.Nodes[(int)ChainFrom_alt_0_rec_NodeNums.@to];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFrom_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)ChainFrom_alt_0_rec_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFrom.Instance.ChainFrom_Delete(graph, subpattern__subpattern0);
		}
	}

	public class Pattern_ChainFromComplete : LGSPMatchingPattern
	{
		private static Pattern_ChainFromComplete instance = null;
		public static Pattern_ChainFromComplete Instance { get { if (instance==null) { instance = new Pattern_ChainFromComplete(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFromComplete_node_from_AllowedTypes = null;
		public static bool[] ChainFromComplete_node_from_IsAllowedType = null;
		public enum ChainFromComplete_NodeNums { @from, };
		public enum ChainFromComplete_EdgeNums { };
		public enum ChainFromComplete_VariableNums { };
		public enum ChainFromComplete_SubNums { };
		public enum ChainFromComplete_AltNums { @alt_0, };
		PatternGraph pat_ChainFromComplete;

		public enum ChainFromComplete_alt_0_CaseNums { @base, @rec, };
		public enum ChainFromComplete_alt_0_base_NodeNums { @from, };
		public enum ChainFromComplete_alt_0_base_EdgeNums { };
		public enum ChainFromComplete_alt_0_base_VariableNums { };
		public enum ChainFromComplete_alt_0_base_SubNums { };
		public enum ChainFromComplete_alt_0_base_AltNums { };
		PatternGraph ChainFromComplete_alt_0_base;

		public static NodeType[] ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType = null;
		public static EdgeType[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_base_neg_0_NodeNums { @from, @_node0, };
		public enum ChainFromComplete_alt_0_base_neg_0_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_base_neg_0_VariableNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_SubNums { };
		public enum ChainFromComplete_alt_0_base_neg_0_AltNums { };
		PatternGraph ChainFromComplete_alt_0_base_neg_0;

		public static NodeType[] ChainFromComplete_alt_0_rec_node_to_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_node_to_IsAllowedType = null;
		public static EdgeType[] ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromComplete_alt_0_rec_NodeNums { @from, @to, };
		public enum ChainFromComplete_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromComplete_alt_0_rec_VariableNums { };
		public enum ChainFromComplete_alt_0_rec_SubNums { @_subpattern0, };
		public enum ChainFromComplete_alt_0_rec_AltNums { };
		PatternGraph ChainFromComplete_alt_0_rec;


#if INITIAL_WARMUP
		public Pattern_ChainFromComplete()
#else
		private Pattern_ChainFromComplete()
#endif
		{
			name = "ChainFromComplete";

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromComplete_node_from", };
		}
		public override void initialize()
		{
			bool[,] ChainFromComplete_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromComplete_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode ChainFromComplete_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFromComplete_node_from", "from", ChainFromComplete_node_from_AllowedTypes, ChainFromComplete_node_from_IsAllowedType, 5.5F, 0);
			bool[,] ChainFromComplete_alt_0_base_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] ChainFromComplete_alt_0_base_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] ChainFromComplete_alt_0_base_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromComplete_alt_0_base_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ChainFromComplete_alt_0_base_neg_0_node__node0 = new PatternNode((int) NodeTypes.@Node, "ChainFromComplete_alt_0_base_neg_0_node__node0", "_node0", ChainFromComplete_alt_0_base_neg_0_node__node0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromComplete_alt_0_base_neg_0_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromComplete_alt_0_base_neg_0_edge__edge0", "_edge0", ChainFromComplete_alt_0_base_neg_0_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_base_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromComplete_alt_0_base_neg_0 = new PatternGraph(
				"neg_0",
				"ChainFromComplete_alt_0_base_",
				false,
				new PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_base_neg_0_node__node0 }, 
				new PatternEdge[] { ChainFromComplete_alt_0_base_neg_0_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromComplete_alt_0_base_neg_0_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_base_neg_0_isEdgeHomomorphicGlobal
			);
			ChainFromComplete_alt_0_base_neg_0.edgeToSourceNode.Add(ChainFromComplete_alt_0_base_neg_0_edge__edge0, ChainFromComplete_node_from);
			ChainFromComplete_alt_0_base_neg_0.edgeToTargetNode.Add(ChainFromComplete_alt_0_base_neg_0_edge__edge0, ChainFromComplete_alt_0_base_neg_0_node__node0);

			ChainFromComplete_alt_0_base = new PatternGraph(
				"base",
				"ChainFromComplete_alt_0_",
				false,
				new PatternNode[] { ChainFromComplete_node_from }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { ChainFromComplete_alt_0_base_neg_0,  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromComplete_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_base_isEdgeHomomorphicGlobal
			);
			ChainFromComplete_alt_0_base_neg_0.embeddingGraph = ChainFromComplete_alt_0_base;

			bool[,] ChainFromComplete_alt_0_rec_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromComplete_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ChainFromComplete_alt_0_rec_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFromComplete_alt_0_rec_node_to", "to", ChainFromComplete_alt_0_rec_node_to_AllowedTypes, ChainFromComplete_alt_0_rec_node_to_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromComplete_alt_0_rec_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromComplete_alt_0_rec_edge__edge0", "_edge0", ChainFromComplete_alt_0_rec_edge__edge0_AllowedTypes, ChainFromComplete_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFromComplete_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromComplete.Instance, new PatternElement[] { ChainFromComplete_alt_0_rec_node_to });
			ChainFromComplete_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFromComplete_alt_0_",
				false,
				new PatternNode[] { ChainFromComplete_node_from, ChainFromComplete_alt_0_rec_node_to }, 
				new PatternEdge[] { ChainFromComplete_alt_0_rec_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { ChainFromComplete_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromComplete_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromComplete_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromComplete_alt_0_rec.edgeToSourceNode.Add(ChainFromComplete_alt_0_rec_edge__edge0, ChainFromComplete_node_from);
			ChainFromComplete_alt_0_rec.edgeToTargetNode.Add(ChainFromComplete_alt_0_rec_edge__edge0, ChainFromComplete_alt_0_rec_node_to);

			Alternative ChainFromComplete_alt_0 = new Alternative( "alt_0", "ChainFromComplete_", new PatternGraph[] { ChainFromComplete_alt_0_base, ChainFromComplete_alt_0_rec } );

			pat_ChainFromComplete = new PatternGraph(
				"ChainFromComplete",
				"",
				false,
				new PatternNode[] { ChainFromComplete_node_from }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFromComplete_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				ChainFromComplete_isNodeHomomorphicGlobal,
				ChainFromComplete_isEdgeHomomorphicGlobal
			);
			ChainFromComplete_alt_0_base.embeddingGraph = pat_ChainFromComplete;
			ChainFromComplete_alt_0_rec.embeddingGraph = pat_ChainFromComplete;

			ChainFromComplete_node_from.PointOfDefinition = null;
			ChainFromComplete_alt_0_base_neg_0_node__node0.PointOfDefinition = ChainFromComplete_alt_0_base_neg_0;
			ChainFromComplete_alt_0_base_neg_0_edge__edge0.PointOfDefinition = ChainFromComplete_alt_0_base_neg_0;
			ChainFromComplete_alt_0_rec_node_to.PointOfDefinition = ChainFromComplete_alt_0_rec;
			ChainFromComplete_alt_0_rec_edge__edge0.PointOfDefinition = ChainFromComplete_alt_0_rec;
			ChainFromComplete_alt_0_rec__subpattern0.PointOfDefinition = ChainFromComplete_alt_0_rec;

			patternGraph = pat_ChainFromComplete;
		}



		public void ChainFromComplete_Create(LGSPGraph graph, LGSPNode node_from)
		{
			graph.SettingAddedNodeNames( create_ChainFromComplete_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromComplete_addedEdgeNames );
		}
		private static String[] create_ChainFromComplete_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromComplete_addedEdgeNames = new String[] {  };

		public void ChainFromComplete_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromComplete_AltNums.@alt_0 + 0];
			ChainFromComplete_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromComplete_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ChainFromComplete_alt_0_base) {
				ChainFromComplete_alt_0_base_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == ChainFromComplete_alt_0_rec) {
				ChainFromComplete_alt_0_rec_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromComplete_alt_0_base_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromComplete_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromComplete_alt_0_base_addedEdgeNames );
		}
		private static String[] create_ChainFromComplete_alt_0_base_addedNodeNames = new String[] { "from" };
		private static String[] create_ChainFromComplete_alt_0_base_addedEdgeNames = new String[] {  };

		public void ChainFromComplete_alt_0_base_Delete(LGSPGraph graph, LGSPMatch match)
		{
		}

		public void ChainFromComplete_alt_0_rec_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromComplete_alt_0_rec_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromComplete_alt_0_rec_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
			Pattern_ChainFromComplete.Instance.ChainFromComplete_Create(graph, node_to);
		}
		private static String[] create_ChainFromComplete_alt_0_rec_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFromComplete_alt_0_rec_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromComplete_alt_0_rec_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_to = match.Nodes[(int)ChainFromComplete_alt_0_rec_NodeNums.@to];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromComplete_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)ChainFromComplete_alt_0_rec_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_to);
			graph.Remove(node_to);
			Pattern_ChainFromComplete.Instance.ChainFromComplete_Delete(graph, subpattern__subpattern0);
		}
	}

	public class Pattern_Blowball : LGSPMatchingPattern
	{
		private static Pattern_Blowball instance = null;
		public static Pattern_Blowball Instance { get { if (instance==null) { instance = new Pattern_Blowball(); instance.initialize(); } return instance; } }

		public static NodeType[] Blowball_node_head_AllowedTypes = null;
		public static bool[] Blowball_node_head_IsAllowedType = null;
		public enum Blowball_NodeNums { @head, };
		public enum Blowball_EdgeNums { };
		public enum Blowball_VariableNums { };
		public enum Blowball_SubNums { };
		public enum Blowball_AltNums { @alt_0, };
		PatternGraph pat_Blowball;

		public enum Blowball_alt_0_CaseNums { @end, @further, };
		public enum Blowball_alt_0_end_NodeNums { @head, };
		public enum Blowball_alt_0_end_EdgeNums { };
		public enum Blowball_alt_0_end_VariableNums { };
		public enum Blowball_alt_0_end_SubNums { };
		public enum Blowball_alt_0_end_AltNums { };
		PatternGraph Blowball_alt_0_end;

		public static NodeType[] Blowball_alt_0_end_neg_0_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_node__node0_IsAllowedType = null;
		public static EdgeType[] Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_end_neg_0_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_end_neg_0_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_end_neg_0_VariableNums { };
		public enum Blowball_alt_0_end_neg_0_SubNums { };
		public enum Blowball_alt_0_end_neg_0_AltNums { };
		PatternGraph Blowball_alt_0_end_neg_0;

		public static NodeType[] Blowball_alt_0_further_node__node0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_node__node0_IsAllowedType = null;
		public static EdgeType[] Blowball_alt_0_further_edge__edge0_AllowedTypes = null;
		public static bool[] Blowball_alt_0_further_edge__edge0_IsAllowedType = null;
		public enum Blowball_alt_0_further_NodeNums { @head, @_node0, };
		public enum Blowball_alt_0_further_EdgeNums { @_edge0, };
		public enum Blowball_alt_0_further_VariableNums { };
		public enum Blowball_alt_0_further_SubNums { @_subpattern0, };
		public enum Blowball_alt_0_further_AltNums { };
		PatternGraph Blowball_alt_0_further;


#if INITIAL_WARMUP
		public Pattern_Blowball()
#else
		private Pattern_Blowball()
#endif
		{
			name = "Blowball";

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "Blowball_node_head", };
		}
		public override void initialize()
		{
			bool[,] Blowball_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Blowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode Blowball_node_head = new PatternNode((int) NodeTypes.@Node, "Blowball_node_head", "head", Blowball_node_head_AllowedTypes, Blowball_node_head_IsAllowedType, 5.5F, 0);
			bool[,] Blowball_alt_0_end_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] Blowball_alt_0_end_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] Blowball_alt_0_end_neg_0_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Blowball_alt_0_end_neg_0_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode Blowball_alt_0_end_neg_0_node__node0 = new PatternNode((int) NodeTypes.@Node, "Blowball_alt_0_end_neg_0_node__node0", "_node0", Blowball_alt_0_end_neg_0_node__node0_AllowedTypes, Blowball_alt_0_end_neg_0_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge Blowball_alt_0_end_neg_0_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Blowball_alt_0_end_neg_0_edge__edge0", "_edge0", Blowball_alt_0_end_neg_0_edge__edge0_AllowedTypes, Blowball_alt_0_end_neg_0_edge__edge0_IsAllowedType, 5.5F, -1);
			Blowball_alt_0_end_neg_0 = new PatternGraph(
				"neg_0",
				"Blowball_alt_0_end_",
				false,
				new PatternNode[] { Blowball_node_head, Blowball_alt_0_end_neg_0_node__node0 }, 
				new PatternEdge[] { Blowball_alt_0_end_neg_0_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				Blowball_alt_0_end_neg_0_isNodeHomomorphicGlobal,
				Blowball_alt_0_end_neg_0_isEdgeHomomorphicGlobal
			);
			Blowball_alt_0_end_neg_0.edgeToSourceNode.Add(Blowball_alt_0_end_neg_0_edge__edge0, Blowball_node_head);
			Blowball_alt_0_end_neg_0.edgeToTargetNode.Add(Blowball_alt_0_end_neg_0_edge__edge0, Blowball_alt_0_end_neg_0_node__node0);

			Blowball_alt_0_end = new PatternGraph(
				"end",
				"Blowball_alt_0_",
				false,
				new PatternNode[] { Blowball_node_head }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] { Blowball_alt_0_end_neg_0,  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Blowball_alt_0_end_isNodeHomomorphicGlobal,
				Blowball_alt_0_end_isEdgeHomomorphicGlobal
			);
			Blowball_alt_0_end_neg_0.embeddingGraph = Blowball_alt_0_end;

			bool[,] Blowball_alt_0_further_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] Blowball_alt_0_further_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode Blowball_alt_0_further_node__node0 = new PatternNode((int) NodeTypes.@Node, "Blowball_alt_0_further_node__node0", "_node0", Blowball_alt_0_further_node__node0_AllowedTypes, Blowball_alt_0_further_node__node0_IsAllowedType, 5.5F, -1);
			PatternEdge Blowball_alt_0_further_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "Blowball_alt_0_further_edge__edge0", "_edge0", Blowball_alt_0_further_edge__edge0_AllowedTypes, Blowball_alt_0_further_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding Blowball_alt_0_further__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_Blowball.Instance, new PatternElement[] { Blowball_node_head });
			Blowball_alt_0_further = new PatternGraph(
				"further",
				"Blowball_alt_0_",
				false,
				new PatternNode[] { Blowball_node_head, Blowball_alt_0_further_node__node0 }, 
				new PatternEdge[] { Blowball_alt_0_further_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { Blowball_alt_0_further__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				Blowball_alt_0_further_isNodeHomomorphicGlobal,
				Blowball_alt_0_further_isEdgeHomomorphicGlobal
			);
			Blowball_alt_0_further.edgeToSourceNode.Add(Blowball_alt_0_further_edge__edge0, Blowball_node_head);
			Blowball_alt_0_further.edgeToTargetNode.Add(Blowball_alt_0_further_edge__edge0, Blowball_alt_0_further_node__node0);

			Alternative Blowball_alt_0 = new Alternative( "alt_0", "Blowball_", new PatternGraph[] { Blowball_alt_0_end, Blowball_alt_0_further } );

			pat_Blowball = new PatternGraph(
				"Blowball",
				"",
				false,
				new PatternNode[] { Blowball_node_head }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { Blowball_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				Blowball_isNodeHomomorphicGlobal,
				Blowball_isEdgeHomomorphicGlobal
			);
			Blowball_alt_0_end.embeddingGraph = pat_Blowball;
			Blowball_alt_0_further.embeddingGraph = pat_Blowball;

			Blowball_node_head.PointOfDefinition = null;
			Blowball_alt_0_end_neg_0_node__node0.PointOfDefinition = Blowball_alt_0_end_neg_0;
			Blowball_alt_0_end_neg_0_edge__edge0.PointOfDefinition = Blowball_alt_0_end_neg_0;
			Blowball_alt_0_further_node__node0.PointOfDefinition = Blowball_alt_0_further;
			Blowball_alt_0_further_edge__edge0.PointOfDefinition = Blowball_alt_0_further;
			Blowball_alt_0_further__subpattern0.PointOfDefinition = Blowball_alt_0_further;

			patternGraph = pat_Blowball;
		}



		public void Blowball_Create(LGSPGraph graph, LGSPNode node_head)
		{
			graph.SettingAddedNodeNames( create_Blowball_addedNodeNames );
			graph.SettingAddedEdgeNames( create_Blowball_addedEdgeNames );
		}
		private static String[] create_Blowball_addedNodeNames = new String[] {  };
		private static String[] create_Blowball_addedEdgeNames = new String[] {  };

		public void Blowball_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)Blowball_AltNums.@alt_0 + 0];
			Blowball_alt_0_Delete(graph, alternative_alt_0);
		}

		public void Blowball_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == Blowball_alt_0_end) {
				Blowball_alt_0_end_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == Blowball_alt_0_further) {
				Blowball_alt_0_further_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void Blowball_alt_0_end_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Blowball_alt_0_end_addedNodeNames );
			@Node node_head = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Blowball_alt_0_end_addedEdgeNames );
		}
		private static String[] create_Blowball_alt_0_end_addedNodeNames = new String[] { "head" };
		private static String[] create_Blowball_alt_0_end_addedEdgeNames = new String[] {  };

		public void Blowball_alt_0_end_Delete(LGSPGraph graph, LGSPMatch match)
		{
		}

		public void Blowball_alt_0_further_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_Blowball_alt_0_further_addedNodeNames );
			@Node node_head = @Node.CreateNode(graph);
			@Node node__node0 = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_Blowball_alt_0_further_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_head, node__node0);
			Pattern_Blowball.Instance.Blowball_Create(graph, node_head);
		}
		private static String[] create_Blowball_alt_0_further_addedNodeNames = new String[] { "head", "_node0" };
		private static String[] create_Blowball_alt_0_further_addedEdgeNames = new String[] { "_edge0" };

		public void Blowball_alt_0_further_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node__node0 = match.Nodes[(int)Blowball_alt_0_further_NodeNums.@_node0];
			LGSPEdge edge__edge0 = match.Edges[(int)Blowball_alt_0_further_EdgeNums.@_edge0];
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)Blowball_alt_0_further_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node__node0);
			graph.Remove(node__node0);
			Pattern_Blowball.Instance.Blowball_Delete(graph, subpattern__subpattern0);
		}
	}

	public class Pattern_ReverseChainFromTo : LGSPMatchingPattern
	{
		private static Pattern_ReverseChainFromTo instance = null;
		public static Pattern_ReverseChainFromTo Instance { get { if (instance==null) { instance = new Pattern_ReverseChainFromTo(); instance.initialize(); } return instance; } }

		public static NodeType[] ReverseChainFromTo_node_from_AllowedTypes = null;
		public static NodeType[] ReverseChainFromTo_node_to_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_node_from_IsAllowedType = null;
		public static bool[] ReverseChainFromTo_node_to_IsAllowedType = null;
		public enum ReverseChainFromTo_NodeNums { @from, @to, };
		public enum ReverseChainFromTo_EdgeNums { };
		public enum ReverseChainFromTo_VariableNums { };
		public enum ReverseChainFromTo_SubNums { };
		public enum ReverseChainFromTo_AltNums { @alt_0, };
		PatternGraph pat_ReverseChainFromTo;

		public enum ReverseChainFromTo_alt_0_CaseNums { @base, @rec, };
		public static EdgeType[] ReverseChainFromTo_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ReverseChainFromTo_alt_0_base_NodeNums { @to, @from, };
		public enum ReverseChainFromTo_alt_0_base_EdgeNums { @_edge0, };
		public enum ReverseChainFromTo_alt_0_base_VariableNums { };
		public enum ReverseChainFromTo_alt_0_base_SubNums { };
		public enum ReverseChainFromTo_alt_0_base_AltNums { };
		PatternGraph ReverseChainFromTo_alt_0_base;

		public static NodeType[] ReverseChainFromTo_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static EdgeType[] ReverseChainFromTo_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ReverseChainFromTo_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ReverseChainFromTo_alt_0_rec_NodeNums { @intermediate, @from, @to, };
		public enum ReverseChainFromTo_alt_0_rec_EdgeNums { @_edge0, };
		public enum ReverseChainFromTo_alt_0_rec_VariableNums { };
		public enum ReverseChainFromTo_alt_0_rec_SubNums { @_subpattern0, };
		public enum ReverseChainFromTo_alt_0_rec_AltNums { };
		PatternGraph ReverseChainFromTo_alt_0_rec;


#if INITIAL_WARMUP
		public Pattern_ReverseChainFromTo()
#else
		private Pattern_ReverseChainFromTo()
#endif
		{
			name = "ReverseChainFromTo";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ReverseChainFromTo_node_from", "ReverseChainFromTo_node_to", };
		}
		public override void initialize()
		{
			bool[,] ReverseChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReverseChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode ReverseChainFromTo_node_from = new PatternNode((int) NodeTypes.@Node, "ReverseChainFromTo_node_from", "from", ReverseChainFromTo_node_from_AllowedTypes, ReverseChainFromTo_node_from_IsAllowedType, 5.5F, 0);
			PatternNode ReverseChainFromTo_node_to = new PatternNode((int) NodeTypes.@Node, "ReverseChainFromTo_node_to", "to", ReverseChainFromTo_node_to_AllowedTypes, ReverseChainFromTo_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ReverseChainFromTo_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ReverseChainFromTo_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternEdge ReverseChainFromTo_alt_0_base_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ReverseChainFromTo_alt_0_base_edge__edge0", "_edge0", ReverseChainFromTo_alt_0_base_edge__edge0_AllowedTypes, ReverseChainFromTo_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ReverseChainFromTo_alt_0_base = new PatternGraph(
				"base",
				"ReverseChainFromTo_alt_0_",
				false,
				new PatternNode[] { ReverseChainFromTo_node_to, ReverseChainFromTo_node_from }, 
				new PatternEdge[] { ReverseChainFromTo_alt_0_base_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ReverseChainFromTo_alt_0_base_isNodeHomomorphicGlobal,
				ReverseChainFromTo_alt_0_base_isEdgeHomomorphicGlobal
			);
			ReverseChainFromTo_alt_0_base.edgeToSourceNode.Add(ReverseChainFromTo_alt_0_base_edge__edge0, ReverseChainFromTo_node_to);
			ReverseChainFromTo_alt_0_base.edgeToTargetNode.Add(ReverseChainFromTo_alt_0_base_edge__edge0, ReverseChainFromTo_node_from);

			bool[,] ReverseChainFromTo_alt_0_rec_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ReverseChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ReverseChainFromTo_alt_0_rec_node_intermediate = new PatternNode((int) NodeTypes.@Node, "ReverseChainFromTo_alt_0_rec_node_intermediate", "intermediate", ReverseChainFromTo_alt_0_rec_node_intermediate_AllowedTypes, ReverseChainFromTo_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			PatternEdge ReverseChainFromTo_alt_0_rec_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ReverseChainFromTo_alt_0_rec_edge__edge0", "_edge0", ReverseChainFromTo_alt_0_rec_edge__edge0_AllowedTypes, ReverseChainFromTo_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ReverseChainFromTo_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromTo.Instance, new PatternElement[] { ReverseChainFromTo_alt_0_rec_node_intermediate, ReverseChainFromTo_node_to });
			ReverseChainFromTo_alt_0_rec = new PatternGraph(
				"rec",
				"ReverseChainFromTo_alt_0_",
				false,
				new PatternNode[] { ReverseChainFromTo_alt_0_rec_node_intermediate, ReverseChainFromTo_node_from, ReverseChainFromTo_node_to }, 
				new PatternEdge[] { ReverseChainFromTo_alt_0_rec_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { ReverseChainFromTo_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ReverseChainFromTo_alt_0_rec_isNodeHomomorphicGlobal,
				ReverseChainFromTo_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ReverseChainFromTo_alt_0_rec.edgeToSourceNode.Add(ReverseChainFromTo_alt_0_rec_edge__edge0, ReverseChainFromTo_alt_0_rec_node_intermediate);
			ReverseChainFromTo_alt_0_rec.edgeToTargetNode.Add(ReverseChainFromTo_alt_0_rec_edge__edge0, ReverseChainFromTo_node_from);

			Alternative ReverseChainFromTo_alt_0 = new Alternative( "alt_0", "ReverseChainFromTo_", new PatternGraph[] { ReverseChainFromTo_alt_0_base, ReverseChainFromTo_alt_0_rec } );

			pat_ReverseChainFromTo = new PatternGraph(
				"ReverseChainFromTo",
				"",
				false,
				new PatternNode[] { ReverseChainFromTo_node_from, ReverseChainFromTo_node_to }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ReverseChainFromTo_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ReverseChainFromTo_isNodeHomomorphicGlobal,
				ReverseChainFromTo_isEdgeHomomorphicGlobal
			);
			ReverseChainFromTo_alt_0_base.embeddingGraph = pat_ReverseChainFromTo;
			ReverseChainFromTo_alt_0_rec.embeddingGraph = pat_ReverseChainFromTo;

			ReverseChainFromTo_node_from.PointOfDefinition = null;
			ReverseChainFromTo_node_to.PointOfDefinition = null;
			ReverseChainFromTo_alt_0_base_edge__edge0.PointOfDefinition = ReverseChainFromTo_alt_0_base;
			ReverseChainFromTo_alt_0_rec_node_intermediate.PointOfDefinition = ReverseChainFromTo_alt_0_rec;
			ReverseChainFromTo_alt_0_rec_edge__edge0.PointOfDefinition = ReverseChainFromTo_alt_0_rec;
			ReverseChainFromTo_alt_0_rec__subpattern0.PointOfDefinition = ReverseChainFromTo_alt_0_rec;

			patternGraph = pat_ReverseChainFromTo;
		}



		public void ReverseChainFromTo_Create(LGSPGraph graph, LGSPNode node_from, LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromTo_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ReverseChainFromTo_addedEdgeNames );
		}
		private static String[] create_ReverseChainFromTo_addedNodeNames = new String[] {  };
		private static String[] create_ReverseChainFromTo_addedEdgeNames = new String[] {  };

		public void ReverseChainFromTo_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ReverseChainFromTo_AltNums.@alt_0 + 0];
			ReverseChainFromTo_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ReverseChainFromTo_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ReverseChainFromTo_alt_0_base) {
				ReverseChainFromTo_alt_0_base_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == ReverseChainFromTo_alt_0_rec) {
				ReverseChainFromTo_alt_0_rec_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ReverseChainFromTo_alt_0_base_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromTo_alt_0_base_addedNodeNames );
			@Node node_to = @Node.CreateNode(graph);
			@Node node_from = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ReverseChainFromTo_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_to, node_from);
		}
		private static String[] create_ReverseChainFromTo_alt_0_base_addedNodeNames = new String[] { "to", "from" };
		private static String[] create_ReverseChainFromTo_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ReverseChainFromTo_alt_0_base_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge__edge0 = match.Edges[(int)ReverseChainFromTo_alt_0_base_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
		}

		public void ReverseChainFromTo_alt_0_rec_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromTo_alt_0_rec_addedNodeNames );
			@Node node_intermediate = @Node.CreateNode(graph);
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ReverseChainFromTo_alt_0_rec_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			Pattern_ReverseChainFromTo.Instance.ReverseChainFromTo_Create(graph, node_intermediate, node_to);
		}
		private static String[] create_ReverseChainFromTo_alt_0_rec_addedNodeNames = new String[] { "intermediate", "from", "to" };
		private static String[] create_ReverseChainFromTo_alt_0_rec_addedEdgeNames = new String[] { "_edge0" };

		public void ReverseChainFromTo_alt_0_rec_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ReverseChainFromTo_alt_0_rec_NodeNums.@intermediate];
			LGSPEdge edge__edge0 = match.Edges[(int)ReverseChainFromTo_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)ReverseChainFromTo_alt_0_rec_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ReverseChainFromTo.Instance.ReverseChainFromTo_Delete(graph, subpattern__subpattern0);
		}
	}

	public class Pattern_ChainFromToReverse : LGSPMatchingPattern
	{
		private static Pattern_ChainFromToReverse instance = null;
		public static Pattern_ChainFromToReverse Instance { get { if (instance==null) { instance = new Pattern_ChainFromToReverse(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFromToReverse_node_from_AllowedTypes = null;
		public static NodeType[] ChainFromToReverse_node_to_AllowedTypes = null;
		public static bool[] ChainFromToReverse_node_from_IsAllowedType = null;
		public static bool[] ChainFromToReverse_node_to_IsAllowedType = null;
		public enum ChainFromToReverse_NodeNums { @from, @to, };
		public enum ChainFromToReverse_EdgeNums { };
		public enum ChainFromToReverse_VariableNums { };
		public enum ChainFromToReverse_SubNums { };
		public enum ChainFromToReverse_AltNums { @alt_0, };
		PatternGraph pat_ChainFromToReverse;

		public enum ChainFromToReverse_alt_0_CaseNums { @base, @rec, };
		public static EdgeType[] ChainFromToReverse_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverse_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromToReverse_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromToReverse_alt_0_base_VariableNums { };
		public enum ChainFromToReverse_alt_0_base_SubNums { };
		public enum ChainFromToReverse_alt_0_base_AltNums { };
		PatternGraph ChainFromToReverse_alt_0_base;

		public static NodeType[] ChainFromToReverse_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static EdgeType[] ChainFromToReverse_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverse_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverse_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromToReverse_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromToReverse_alt_0_rec_VariableNums { };
		public enum ChainFromToReverse_alt_0_rec_SubNums { @cftr, };
		public enum ChainFromToReverse_alt_0_rec_AltNums { };
		PatternGraph ChainFromToReverse_alt_0_rec;


#if INITIAL_WARMUP
		public Pattern_ChainFromToReverse()
#else
		private Pattern_ChainFromToReverse()
#endif
		{
			name = "ChainFromToReverse";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromToReverse_node_from", "ChainFromToReverse_node_to", };
		}
		public override void initialize()
		{
			bool[,] ChainFromToReverse_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode ChainFromToReverse_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFromToReverse_node_from", "from", ChainFromToReverse_node_from_AllowedTypes, ChainFromToReverse_node_from_IsAllowedType, 5.5F, 0);
			PatternNode ChainFromToReverse_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFromToReverse_node_to", "to", ChainFromToReverse_node_to_AllowedTypes, ChainFromToReverse_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ChainFromToReverse_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverse_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternEdge ChainFromToReverse_alt_0_base_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromToReverse_alt_0_base_edge__edge0", "_edge0", ChainFromToReverse_alt_0_base_edge__edge0_AllowedTypes, ChainFromToReverse_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromToReverse_alt_0_base = new PatternGraph(
				"base",
				"ChainFromToReverse_alt_0_",
				false,
				new PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_node_to }, 
				new PatternEdge[] { ChainFromToReverse_alt_0_base_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromToReverse_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromToReverse_alt_0_base_isEdgeHomomorphicGlobal
			);
			ChainFromToReverse_alt_0_base.edgeToSourceNode.Add(ChainFromToReverse_alt_0_base_edge__edge0, ChainFromToReverse_node_from);
			ChainFromToReverse_alt_0_base.edgeToTargetNode.Add(ChainFromToReverse_alt_0_base_edge__edge0, ChainFromToReverse_node_to);

			bool[,] ChainFromToReverse_alt_0_rec_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ChainFromToReverse_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ChainFromToReverse_alt_0_rec_node_intermediate = new PatternNode((int) NodeTypes.@Node, "ChainFromToReverse_alt_0_rec_node_intermediate", "intermediate", ChainFromToReverse_alt_0_rec_node_intermediate_AllowedTypes, ChainFromToReverse_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromToReverse_alt_0_rec_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromToReverse_alt_0_rec_edge__edge0", "_edge0", ChainFromToReverse_alt_0_rec_edge__edge0_AllowedTypes, ChainFromToReverse_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFromToReverse_alt_0_rec_cftr = new PatternGraphEmbedding("cftr", Pattern_ChainFromToReverse.Instance, new PatternElement[] { ChainFromToReverse_alt_0_rec_node_intermediate, ChainFromToReverse_node_to });
			ChainFromToReverse_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFromToReverse_alt_0_",
				false,
				new PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_alt_0_rec_node_intermediate, ChainFromToReverse_node_to }, 
				new PatternEdge[] { ChainFromToReverse_alt_0_rec_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { ChainFromToReverse_alt_0_rec_cftr }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromToReverse_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromToReverse_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromToReverse_alt_0_rec.edgeToSourceNode.Add(ChainFromToReverse_alt_0_rec_edge__edge0, ChainFromToReverse_node_from);
			ChainFromToReverse_alt_0_rec.edgeToTargetNode.Add(ChainFromToReverse_alt_0_rec_edge__edge0, ChainFromToReverse_alt_0_rec_node_intermediate);

			Alternative ChainFromToReverse_alt_0 = new Alternative( "alt_0", "ChainFromToReverse_", new PatternGraph[] { ChainFromToReverse_alt_0_base, ChainFromToReverse_alt_0_rec } );

			pat_ChainFromToReverse = new PatternGraph(
				"ChainFromToReverse",
				"",
				false,
				new PatternNode[] { ChainFromToReverse_node_from, ChainFromToReverse_node_to }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFromToReverse_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ChainFromToReverse_isNodeHomomorphicGlobal,
				ChainFromToReverse_isEdgeHomomorphicGlobal
			);
			ChainFromToReverse_alt_0_base.embeddingGraph = pat_ChainFromToReverse;
			ChainFromToReverse_alt_0_rec.embeddingGraph = pat_ChainFromToReverse;

			ChainFromToReverse_node_from.PointOfDefinition = null;
			ChainFromToReverse_node_to.PointOfDefinition = null;
			ChainFromToReverse_alt_0_base_edge__edge0.PointOfDefinition = ChainFromToReverse_alt_0_base;
			ChainFromToReverse_alt_0_rec_node_intermediate.PointOfDefinition = ChainFromToReverse_alt_0_rec;
			ChainFromToReverse_alt_0_rec_edge__edge0.PointOfDefinition = ChainFromToReverse_alt_0_rec;
			ChainFromToReverse_alt_0_rec_cftr.PointOfDefinition = ChainFromToReverse_alt_0_rec;

			patternGraph = pat_ChainFromToReverse;
		}



		public void ChainFromToReverse_Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromToReverse_AltNums.@alt_0 + 0];
			graph.SettingAddedNodeNames( ChainFromToReverse_addedNodeNames );
			ChainFromToReverse_alt_0_Modify(graph, alternative_alt_0);
			graph.SettingAddedEdgeNames( ChainFromToReverse_addedEdgeNames );
		}
		private static String[] ChainFromToReverse_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverse_addedEdgeNames = new String[] {  };

		public void ChainFromToReverse_ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromToReverse_AltNums.@alt_0 + 0];
			graph.SettingAddedNodeNames( ChainFromToReverse_addedNodeNames );
			ChainFromToReverse_alt_0_ModifyNoReuse(graph, alternative_alt_0);
			graph.SettingAddedEdgeNames( ChainFromToReverse_addedEdgeNames );
		}

		public void ChainFromToReverse_Create(LGSPGraph graph, LGSPNode node_from, LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverse_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromToReverse_addedEdgeNames );
		}
		private static String[] create_ChainFromToReverse_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromToReverse_addedEdgeNames = new String[] {  };

		public void ChainFromToReverse_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromToReverse_AltNums.@alt_0 + 0];
			ChainFromToReverse_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromToReverse_alt_0_Modify(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_Modify(graph, match);
				return;
			}
			else if(match.patternGraph == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_Modify(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_ModifyNoReuse(graph, match);
				return;
			}
			else if(match.patternGraph == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_ModifyNoReuse(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ChainFromToReverse_alt_0_base) {
				ChainFromToReverse_alt_0_base_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == ChainFromToReverse_alt_0_rec) {
				ChainFromToReverse_alt_0_rec_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverse_alt_0_base_Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_to = match.Nodes[(int)ChainFromToReverse_alt_0_base_NodeNums.@to];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverse_alt_0_base_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverse_alt_0_base_EdgeNums.@_edge0];
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_base_addedEdgeNames );
			@Edge edge__edge1;
			if(edge__edge0.type == EdgeType_Edge.typeVar)
			{
				// re-using edge__edge0 as edge__edge1
				edge__edge1 = (@Edge) edge__edge0;
				graph.ReuseEdge(edge__edge0, node_to, node_from);
			}
			else
			{
				graph.Remove(edge__edge0);
				edge__edge1 = @Edge.CreateEdge(graph, node_to, node_from);
			}
		}
		private static String[] ChainFromToReverse_alt_0_base_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverse_alt_0_base_addedEdgeNames = new String[] { "_edge1" };

		public void ChainFromToReverse_alt_0_base_ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_to = match.Nodes[(int)ChainFromToReverse_alt_0_base_NodeNums.@to];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverse_alt_0_base_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverse_alt_0_base_EdgeNums.@_edge0];
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_base_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_to, node_from);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverse_alt_0_base_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverse_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromToReverse_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
		}
		private static String[] create_ChainFromToReverse_alt_0_base_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFromToReverse_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromToReverse_alt_0_base_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverse_alt_0_base_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverse_alt_0_rec_Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ChainFromToReverse_alt_0_rec_NodeNums.@intermediate];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverse_alt_0_rec_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverse_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern_cftr = match.EmbeddedGraphs[(int)ChainFromToReverse_alt_0_rec_SubNums.@cftr];
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(graph, subpattern_cftr);
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_rec_addedEdgeNames );
			@Edge edge__edge1;
			if(edge__edge0.type == EdgeType_Edge.typeVar)
			{
				// re-using edge__edge0 as edge__edge1
				edge__edge1 = (@Edge) edge__edge0;
				graph.ReuseEdge(edge__edge0, node_intermediate, node_from);
			}
			else
			{
				graph.Remove(edge__edge0);
				edge__edge1 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			}
		}
		private static String[] ChainFromToReverse_alt_0_rec_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverse_alt_0_rec_addedEdgeNames = new String[] { "_edge1" };

		public void ChainFromToReverse_alt_0_rec_ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ChainFromToReverse_alt_0_rec_NodeNums.@intermediate];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverse_alt_0_rec_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverse_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern_cftr = match.EmbeddedGraphs[(int)ChainFromToReverse_alt_0_rec_SubNums.@cftr];
			graph.SettingAddedNodeNames( ChainFromToReverse_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(graph, subpattern_cftr);
			graph.SettingAddedEdgeNames( ChainFromToReverse_alt_0_rec_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverse_alt_0_rec_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverse_alt_0_rec_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_intermediate = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromToReverse_alt_0_rec_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_intermediate);
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Create(graph, node_intermediate, node_to);
		}
		private static String[] create_ChainFromToReverse_alt_0_rec_addedNodeNames = new String[] { "from", "intermediate", "to" };
		private static String[] create_ChainFromToReverse_alt_0_rec_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromToReverse_alt_0_rec_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ChainFromToReverse_alt_0_rec_NodeNums.@intermediate];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverse_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern_cftr = match.EmbeddedGraphs[(int)ChainFromToReverse_alt_0_rec_SubNums.@cftr];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Delete(graph, subpattern_cftr);
		}
	}

	public class Pattern_ChainFromToReverseToCommon : LGSPMatchingPattern
	{
		private static Pattern_ChainFromToReverseToCommon instance = null;
		public static Pattern_ChainFromToReverseToCommon Instance { get { if (instance==null) { instance = new Pattern_ChainFromToReverseToCommon(); instance.initialize(); } return instance; } }

		public static NodeType[] ChainFromToReverseToCommon_node_from_AllowedTypes = null;
		public static NodeType[] ChainFromToReverseToCommon_node_to_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_node_from_IsAllowedType = null;
		public static bool[] ChainFromToReverseToCommon_node_to_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_NodeNums { @from, @to, };
		public enum ChainFromToReverseToCommon_EdgeNums { };
		public enum ChainFromToReverseToCommon_VariableNums { };
		public enum ChainFromToReverseToCommon_SubNums { };
		public enum ChainFromToReverseToCommon_AltNums { @alt_0, };
		PatternGraph pat_ChainFromToReverseToCommon;

		public enum ChainFromToReverseToCommon_alt_0_CaseNums { @base, @rec, };
		public static EdgeType[] ChainFromToReverseToCommon_alt_0_base_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_base_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_alt_0_base_NodeNums { @from, @to, };
		public enum ChainFromToReverseToCommon_alt_0_base_EdgeNums { @_edge0, };
		public enum ChainFromToReverseToCommon_alt_0_base_VariableNums { };
		public enum ChainFromToReverseToCommon_alt_0_base_SubNums { };
		public enum ChainFromToReverseToCommon_alt_0_base_AltNums { };
		PatternGraph ChainFromToReverseToCommon_alt_0_base;

		public static NodeType[] ChainFromToReverseToCommon_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static EdgeType[] ChainFromToReverseToCommon_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static bool[] ChainFromToReverseToCommon_alt_0_rec_edge__edge0_IsAllowedType = null;
		public enum ChainFromToReverseToCommon_alt_0_rec_NodeNums { @from, @intermediate, @to, };
		public enum ChainFromToReverseToCommon_alt_0_rec_EdgeNums { @_edge0, };
		public enum ChainFromToReverseToCommon_alt_0_rec_VariableNums { };
		public enum ChainFromToReverseToCommon_alt_0_rec_SubNums { @cftrtc, };
		public enum ChainFromToReverseToCommon_alt_0_rec_AltNums { };
		PatternGraph ChainFromToReverseToCommon_alt_0_rec;


#if INITIAL_WARMUP
		public Pattern_ChainFromToReverseToCommon()
#else
		private Pattern_ChainFromToReverseToCommon()
#endif
		{
			name = "ChainFromToReverseToCommon";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ChainFromToReverseToCommon_node_from", "ChainFromToReverseToCommon_node_to", };
		}
		public override void initialize()
		{
			bool[,] ChainFromToReverseToCommon_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverseToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode ChainFromToReverseToCommon_node_from = new PatternNode((int) NodeTypes.@Node, "ChainFromToReverseToCommon_node_from", "from", ChainFromToReverseToCommon_node_from_AllowedTypes, ChainFromToReverseToCommon_node_from_IsAllowedType, 5.5F, 0);
			PatternNode ChainFromToReverseToCommon_node_to = new PatternNode((int) NodeTypes.@Node, "ChainFromToReverseToCommon_node_to", "to", ChainFromToReverseToCommon_node_to_AllowedTypes, ChainFromToReverseToCommon_node_to_IsAllowedType, 5.5F, 1);
			bool[,] ChainFromToReverseToCommon_alt_0_base_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] ChainFromToReverseToCommon_alt_0_base_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternEdge ChainFromToReverseToCommon_alt_0_base_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromToReverseToCommon_alt_0_base_edge__edge0", "_edge0", ChainFromToReverseToCommon_alt_0_base_edge__edge0_AllowedTypes, ChainFromToReverseToCommon_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			ChainFromToReverseToCommon_alt_0_base = new PatternGraph(
				"base",
				"ChainFromToReverseToCommon_alt_0_",
				false,
				new PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_node_to }, 
				new PatternEdge[] { ChainFromToReverseToCommon_alt_0_base_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromToReverseToCommon_alt_0_base_isNodeHomomorphicGlobal,
				ChainFromToReverseToCommon_alt_0_base_isEdgeHomomorphicGlobal
			);
			ChainFromToReverseToCommon_alt_0_base.edgeToSourceNode.Add(ChainFromToReverseToCommon_alt_0_base_edge__edge0, ChainFromToReverseToCommon_node_from);
			ChainFromToReverseToCommon_alt_0_base.edgeToTargetNode.Add(ChainFromToReverseToCommon_alt_0_base_edge__edge0, ChainFromToReverseToCommon_node_to);

			bool[,] ChainFromToReverseToCommon_alt_0_rec_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ChainFromToReverseToCommon_alt_0_rec_isEdgeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			PatternNode ChainFromToReverseToCommon_alt_0_rec_node_intermediate = new PatternNode((int) NodeTypes.@Node, "ChainFromToReverseToCommon_alt_0_rec_node_intermediate", "intermediate", ChainFromToReverseToCommon_alt_0_rec_node_intermediate_AllowedTypes, ChainFromToReverseToCommon_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			PatternEdge ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ChainFromToReverseToCommon_alt_0_rec_edge__edge0", "_edge0", ChainFromToReverseToCommon_alt_0_rec_edge__edge0_AllowedTypes, ChainFromToReverseToCommon_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ChainFromToReverseToCommon_alt_0_rec_cftrtc = new PatternGraphEmbedding("cftrtc", Pattern_ChainFromToReverseToCommon.Instance, new PatternElement[] { ChainFromToReverseToCommon_alt_0_rec_node_intermediate, ChainFromToReverseToCommon_node_to });
			ChainFromToReverseToCommon_alt_0_rec = new PatternGraph(
				"rec",
				"ChainFromToReverseToCommon_alt_0_",
				false,
				new PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_alt_0_rec_node_intermediate, ChainFromToReverseToCommon_node_to }, 
				new PatternEdge[] { ChainFromToReverseToCommon_alt_0_rec_edge__edge0 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { ChainFromToReverseToCommon_alt_0_rec_cftrtc }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, true, },
					{ false, true, true, },
					{ true, true, true, },
				},
				new bool[1, 1] {
					{ true, },
				},
				ChainFromToReverseToCommon_alt_0_rec_isNodeHomomorphicGlobal,
				ChainFromToReverseToCommon_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ChainFromToReverseToCommon_alt_0_rec.edgeToSourceNode.Add(ChainFromToReverseToCommon_alt_0_rec_edge__edge0, ChainFromToReverseToCommon_node_from);
			ChainFromToReverseToCommon_alt_0_rec.edgeToTargetNode.Add(ChainFromToReverseToCommon_alt_0_rec_edge__edge0, ChainFromToReverseToCommon_alt_0_rec_node_intermediate);

			Alternative ChainFromToReverseToCommon_alt_0 = new Alternative( "alt_0", "ChainFromToReverseToCommon_", new PatternGraph[] { ChainFromToReverseToCommon_alt_0_base, ChainFromToReverseToCommon_alt_0_rec } );

			pat_ChainFromToReverseToCommon = new PatternGraph(
				"ChainFromToReverseToCommon",
				"",
				false,
				new PatternNode[] { ChainFromToReverseToCommon_node_from, ChainFromToReverseToCommon_node_to }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ChainFromToReverseToCommon_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				ChainFromToReverseToCommon_isNodeHomomorphicGlobal,
				ChainFromToReverseToCommon_isEdgeHomomorphicGlobal
			);
			ChainFromToReverseToCommon_alt_0_base.embeddingGraph = pat_ChainFromToReverseToCommon;
			ChainFromToReverseToCommon_alt_0_rec.embeddingGraph = pat_ChainFromToReverseToCommon;

			ChainFromToReverseToCommon_node_from.PointOfDefinition = null;
			ChainFromToReverseToCommon_node_to.PointOfDefinition = null;
			ChainFromToReverseToCommon_alt_0_base_edge__edge0.PointOfDefinition = ChainFromToReverseToCommon_alt_0_base;
			ChainFromToReverseToCommon_alt_0_rec_node_intermediate.PointOfDefinition = ChainFromToReverseToCommon_alt_0_rec;
			ChainFromToReverseToCommon_alt_0_rec_edge__edge0.PointOfDefinition = ChainFromToReverseToCommon_alt_0_rec;
			ChainFromToReverseToCommon_alt_0_rec_cftrtc.PointOfDefinition = ChainFromToReverseToCommon_alt_0_rec;

			patternGraph = pat_ChainFromToReverseToCommon;
		}



		public void ChainFromToReverseToCommon_Modify(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromToReverseToCommon_AltNums.@alt_0 + 0];
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_addedNodeNames );
			ChainFromToReverseToCommon_alt_0_Modify(graph, alternative_alt_0, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_addedEdgeNames );
		}
		private static String[] ChainFromToReverseToCommon_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverseToCommon_addedEdgeNames = new String[] {  };

		public void ChainFromToReverseToCommon_ModifyNoReuse(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromToReverseToCommon_AltNums.@alt_0 + 0];
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_addedNodeNames );
			ChainFromToReverseToCommon_alt_0_ModifyNoReuse(graph, alternative_alt_0, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_addedEdgeNames );
		}

		public void ChainFromToReverseToCommon_Create(LGSPGraph graph, LGSPNode node_from, LGSPNode node_to)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverseToCommon_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ChainFromToReverseToCommon_addedEdgeNames );
		}
		private static String[] create_ChainFromToReverseToCommon_addedNodeNames = new String[] {  };
		private static String[] create_ChainFromToReverseToCommon_addedEdgeNames = new String[] {  };

		public void ChainFromToReverseToCommon_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ChainFromToReverseToCommon_AltNums.@alt_0 + 0];
			ChainFromToReverseToCommon_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ChainFromToReverseToCommon_alt_0_Modify(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			if(match.patternGraph == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_Modify(graph, match, node_common);
				return;
			}
			else if(match.patternGraph == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_Modify(graph, match, node_common);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_ModifyNoReuse(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			if(match.patternGraph == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_ModifyNoReuse(graph, match, node_common);
				return;
			}
			else if(match.patternGraph == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_ModifyNoReuse(graph, match, node_common);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ChainFromToReverseToCommon_alt_0_base) {
				ChainFromToReverseToCommon_alt_0_base_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == ChainFromToReverseToCommon_alt_0_rec) {
				ChainFromToReverseToCommon_alt_0_rec_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ChainFromToReverseToCommon_alt_0_base_Modify(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			LGSPNode node_to = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_base_NodeNums.@to];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_base_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverseToCommon_alt_0_base_EdgeNums.@_edge0];
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_base_addedEdgeNames );
			@Edge edge__edge1;
			if(edge__edge0.type == EdgeType_Edge.typeVar)
			{
				// re-using edge__edge0 as edge__edge1
				edge__edge1 = (@Edge) edge__edge0;
				graph.ReuseEdge(edge__edge0, node_to, node_from);
			}
			else
			{
				graph.Remove(edge__edge0);
				edge__edge1 = @Edge.CreateEdge(graph, node_to, node_from);
			}
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_from, node_common);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_to, node_common);
		}
		private static String[] ChainFromToReverseToCommon_alt_0_base_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverseToCommon_alt_0_base_addedEdgeNames = new String[] { "_edge1", "_edge2", "_edge3" };

		public void ChainFromToReverseToCommon_alt_0_base_ModifyNoReuse(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			LGSPNode node_to = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_base_NodeNums.@to];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_base_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverseToCommon_alt_0_base_EdgeNums.@_edge0];
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_base_addedNodeNames );
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_base_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_to, node_from);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_from, node_common);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_to, node_common);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverseToCommon_alt_0_base_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverseToCommon_alt_0_base_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromToReverseToCommon_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_to);
		}
		private static String[] create_ChainFromToReverseToCommon_alt_0_base_addedNodeNames = new String[] { "from", "to" };
		private static String[] create_ChainFromToReverseToCommon_alt_0_base_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromToReverseToCommon_alt_0_base_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverseToCommon_alt_0_base_EdgeNums.@_edge0];
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverseToCommon_alt_0_rec_Modify(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@intermediate];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverseToCommon_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern_cftrtc = match.EmbeddedGraphs[(int)ChainFromToReverseToCommon_alt_0_rec_SubNums.@cftrtc];
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(graph, subpattern_cftrtc, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames );
			@Edge edge__edge1;
			if(edge__edge0.type == EdgeType_Edge.typeVar)
			{
				// re-using edge__edge0 as edge__edge1
				edge__edge1 = (@Edge) edge__edge0;
				graph.ReuseEdge(edge__edge0, node_intermediate, node_from);
			}
			else
			{
				graph.Remove(edge__edge0);
				edge__edge1 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			}
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_from, node_common);
		}
		private static String[] ChainFromToReverseToCommon_alt_0_rec_addedNodeNames = new String[] {  };
		private static String[] ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames = new String[] { "_edge1", "_edge2" };

		public void ChainFromToReverseToCommon_alt_0_rec_ModifyNoReuse(LGSPGraph graph, LGSPMatch match, LGSPNode node_common)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@intermediate];
			LGSPNode node_from = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@from];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverseToCommon_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern_cftrtc = match.EmbeddedGraphs[(int)ChainFromToReverseToCommon_alt_0_rec_SubNums.@cftrtc];
			graph.SettingAddedNodeNames( ChainFromToReverseToCommon_alt_0_rec_addedNodeNames );
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(graph, subpattern_cftrtc, node_common);
			graph.SettingAddedEdgeNames( ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames );
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_from, node_common);
			graph.Remove(edge__edge0);
		}

		public void ChainFromToReverseToCommon_alt_0_rec_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ChainFromToReverseToCommon_alt_0_rec_addedNodeNames );
			@Node node_from = @Node.CreateNode(graph);
			@Node node_intermediate = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_from, node_intermediate);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Create(graph, node_intermediate, node_to);
		}
		private static String[] create_ChainFromToReverseToCommon_alt_0_rec_addedNodeNames = new String[] { "from", "intermediate", "to" };
		private static String[] create_ChainFromToReverseToCommon_alt_0_rec_addedEdgeNames = new String[] { "_edge0" };

		public void ChainFromToReverseToCommon_alt_0_rec_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ChainFromToReverseToCommon_alt_0_rec_NodeNums.@intermediate];
			LGSPEdge edge__edge0 = match.Edges[(int)ChainFromToReverseToCommon_alt_0_rec_EdgeNums.@_edge0];
			LGSPMatch subpattern_cftrtc = match.EmbeddedGraphs[(int)ChainFromToReverseToCommon_alt_0_rec_SubNums.@cftrtc];
			graph.Remove(edge__edge0);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Delete(graph, subpattern_cftrtc);
		}
	}

	public class Pattern_ReverseChainFromToToCommon : LGSPMatchingPattern
	{
		private static Pattern_ReverseChainFromToToCommon instance = null;
		public static Pattern_ReverseChainFromToToCommon Instance { get { if (instance==null) { instance = new Pattern_ReverseChainFromToToCommon(); instance.initialize(); } return instance; } }

		public static NodeType[] ReverseChainFromToToCommon_node_from_AllowedTypes = null;
		public static NodeType[] ReverseChainFromToToCommon_node_to_AllowedTypes = null;
		public static NodeType[] ReverseChainFromToToCommon_node_common_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_node_from_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_node_to_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_node_common_IsAllowedType = null;
		public enum ReverseChainFromToToCommon_NodeNums { @from, @to, @common, };
		public enum ReverseChainFromToToCommon_EdgeNums { };
		public enum ReverseChainFromToToCommon_VariableNums { };
		public enum ReverseChainFromToToCommon_SubNums { };
		public enum ReverseChainFromToToCommon_AltNums { @alt_0, };
		PatternGraph pat_ReverseChainFromToToCommon;

		public enum ReverseChainFromToToCommon_alt_0_CaseNums { @base, @rec, };
		public static EdgeType[] ReverseChainFromToToCommon_alt_0_base_edge__edge0_AllowedTypes = null;
		public static EdgeType[] ReverseChainFromToToCommon_alt_0_base_edge__edge1_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_base_edge__edge0_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_base_edge__edge1_IsAllowedType = null;
		public enum ReverseChainFromToToCommon_alt_0_base_NodeNums { @to, @from, @common, };
		public enum ReverseChainFromToToCommon_alt_0_base_EdgeNums { @_edge0, @_edge1, };
		public enum ReverseChainFromToToCommon_alt_0_base_VariableNums { };
		public enum ReverseChainFromToToCommon_alt_0_base_SubNums { };
		public enum ReverseChainFromToToCommon_alt_0_base_AltNums { };
		PatternGraph ReverseChainFromToToCommon_alt_0_base;

		public static NodeType[] ReverseChainFromToToCommon_alt_0_rec_node_intermediate_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_node_intermediate_IsAllowedType = null;
		public static EdgeType[] ReverseChainFromToToCommon_alt_0_rec_edge__edge0_AllowedTypes = null;
		public static EdgeType[] ReverseChainFromToToCommon_alt_0_rec_edge__edge1_AllowedTypes = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_edge__edge0_IsAllowedType = null;
		public static bool[] ReverseChainFromToToCommon_alt_0_rec_edge__edge1_IsAllowedType = null;
		public enum ReverseChainFromToToCommon_alt_0_rec_NodeNums { @intermediate, @from, @common, @to, };
		public enum ReverseChainFromToToCommon_alt_0_rec_EdgeNums { @_edge0, @_edge1, };
		public enum ReverseChainFromToToCommon_alt_0_rec_VariableNums { };
		public enum ReverseChainFromToToCommon_alt_0_rec_SubNums { @_subpattern0, };
		public enum ReverseChainFromToToCommon_alt_0_rec_AltNums { };
		PatternGraph ReverseChainFromToToCommon_alt_0_rec;


#if INITIAL_WARMUP
		public Pattern_ReverseChainFromToToCommon()
#else
		private Pattern_ReverseChainFromToToCommon()
#endif
		{
			name = "ReverseChainFromToToCommon";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "ReverseChainFromToToCommon_node_from", "ReverseChainFromToToCommon_node_to", "ReverseChainFromToToCommon_node_common", };
		}
		public override void initialize()
		{
			bool[,] ReverseChainFromToToCommon_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ReverseChainFromToToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode ReverseChainFromToToCommon_node_from = new PatternNode((int) NodeTypes.@Node, "ReverseChainFromToToCommon_node_from", "from", ReverseChainFromToToCommon_node_from_AllowedTypes, ReverseChainFromToToCommon_node_from_IsAllowedType, 5.5F, 0);
			PatternNode ReverseChainFromToToCommon_node_to = new PatternNode((int) NodeTypes.@Node, "ReverseChainFromToToCommon_node_to", "to", ReverseChainFromToToCommon_node_to_AllowedTypes, ReverseChainFromToToCommon_node_to_IsAllowedType, 5.5F, 1);
			PatternNode ReverseChainFromToToCommon_node_common = new PatternNode((int) NodeTypes.@Node, "ReverseChainFromToToCommon_node_common", "common", ReverseChainFromToToCommon_node_common_AllowedTypes, ReverseChainFromToToCommon_node_common_IsAllowedType, 5.5F, 2);
			bool[,] ReverseChainFromToToCommon_alt_0_base_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] ReverseChainFromToToCommon_alt_0_base_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternEdge ReverseChainFromToToCommon_alt_0_base_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ReverseChainFromToToCommon_alt_0_base_edge__edge0", "_edge0", ReverseChainFromToToCommon_alt_0_base_edge__edge0_AllowedTypes, ReverseChainFromToToCommon_alt_0_base_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge ReverseChainFromToToCommon_alt_0_base_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ReverseChainFromToToCommon_alt_0_base_edge__edge1", "_edge1", ReverseChainFromToToCommon_alt_0_base_edge__edge1_AllowedTypes, ReverseChainFromToToCommon_alt_0_base_edge__edge1_IsAllowedType, 5.5F, -1);
			ReverseChainFromToToCommon_alt_0_base = new PatternGraph(
				"base",
				"ReverseChainFromToToCommon_alt_0_",
				false,
				new PatternNode[] { ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_common }, 
				new PatternEdge[] { ReverseChainFromToToCommon_alt_0_base_edge__edge0, ReverseChainFromToToCommon_alt_0_base_edge__edge1 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				ReverseChainFromToToCommon_alt_0_base_isNodeHomomorphicGlobal,
				ReverseChainFromToToCommon_alt_0_base_isEdgeHomomorphicGlobal
			);
			ReverseChainFromToToCommon_alt_0_base.edgeToSourceNode.Add(ReverseChainFromToToCommon_alt_0_base_edge__edge0, ReverseChainFromToToCommon_node_to);
			ReverseChainFromToToCommon_alt_0_base.edgeToTargetNode.Add(ReverseChainFromToToCommon_alt_0_base_edge__edge0, ReverseChainFromToToCommon_node_from);
			ReverseChainFromToToCommon_alt_0_base.edgeToSourceNode.Add(ReverseChainFromToToCommon_alt_0_base_edge__edge1, ReverseChainFromToToCommon_node_from);
			ReverseChainFromToToCommon_alt_0_base.edgeToTargetNode.Add(ReverseChainFromToToCommon_alt_0_base_edge__edge1, ReverseChainFromToToCommon_node_common);

			bool[,] ReverseChainFromToToCommon_alt_0_rec_isNodeHomomorphicGlobal = new bool[4, 4] {
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
				{ false, false, false, false, },
			};
			bool[,] ReverseChainFromToToCommon_alt_0_rec_isEdgeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			PatternNode ReverseChainFromToToCommon_alt_0_rec_node_intermediate = new PatternNode((int) NodeTypes.@Node, "ReverseChainFromToToCommon_alt_0_rec_node_intermediate", "intermediate", ReverseChainFromToToCommon_alt_0_rec_node_intermediate_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_node_intermediate_IsAllowedType, 5.5F, -1);
			PatternEdge ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ReverseChainFromToToCommon_alt_0_rec_edge__edge0", "_edge0", ReverseChainFromToToCommon_alt_0_rec_edge__edge0_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_edge__edge0_IsAllowedType, 5.5F, -1);
			PatternEdge ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = new PatternEdge(true, (int) EdgeTypes.@Edge, "ReverseChainFromToToCommon_alt_0_rec_edge__edge1", "_edge1", ReverseChainFromToToCommon_alt_0_rec_edge__edge1_AllowedTypes, ReverseChainFromToToCommon_alt_0_rec_edge__edge1_IsAllowedType, 5.5F, -1);
			PatternGraphEmbedding ReverseChainFromToToCommon_alt_0_rec__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromToToCommon.Instance, new PatternElement[] { ReverseChainFromToToCommon_alt_0_rec_node_intermediate, ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_common });
			ReverseChainFromToToCommon_alt_0_rec = new PatternGraph(
				"rec",
				"ReverseChainFromToToCommon_alt_0_",
				false,
				new PatternNode[] { ReverseChainFromToToCommon_alt_0_rec_node_intermediate, ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_common, ReverseChainFromToToCommon_node_to }, 
				new PatternEdge[] { ReverseChainFromToToCommon_alt_0_rec_edge__edge0, ReverseChainFromToToCommon_alt_0_rec_edge__edge1 }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { ReverseChainFromToToCommon_alt_0_rec__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[4, 4] {
					{ true, false, false, true, },
					{ false, true, false, true, },
					{ false, false, true, true, },
					{ true, true, true, true, },
				},
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				ReverseChainFromToToCommon_alt_0_rec_isNodeHomomorphicGlobal,
				ReverseChainFromToToCommon_alt_0_rec_isEdgeHomomorphicGlobal
			);
			ReverseChainFromToToCommon_alt_0_rec.edgeToSourceNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge0, ReverseChainFromToToCommon_alt_0_rec_node_intermediate);
			ReverseChainFromToToCommon_alt_0_rec.edgeToTargetNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge0, ReverseChainFromToToCommon_node_from);
			ReverseChainFromToToCommon_alt_0_rec.edgeToSourceNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge1, ReverseChainFromToToCommon_node_from);
			ReverseChainFromToToCommon_alt_0_rec.edgeToTargetNode.Add(ReverseChainFromToToCommon_alt_0_rec_edge__edge1, ReverseChainFromToToCommon_node_common);

			Alternative ReverseChainFromToToCommon_alt_0 = new Alternative( "alt_0", "ReverseChainFromToToCommon_", new PatternGraph[] { ReverseChainFromToToCommon_alt_0_base, ReverseChainFromToToCommon_alt_0_rec } );

			pat_ReverseChainFromToToCommon = new PatternGraph(
				"ReverseChainFromToToCommon",
				"",
				false,
				new PatternNode[] { ReverseChainFromToToCommon_node_from, ReverseChainFromToToCommon_node_to, ReverseChainFromToToCommon_node_common }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] { ReverseChainFromToToCommon_alt_0,  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[0, 0] ,
				ReverseChainFromToToCommon_isNodeHomomorphicGlobal,
				ReverseChainFromToToCommon_isEdgeHomomorphicGlobal
			);
			ReverseChainFromToToCommon_alt_0_base.embeddingGraph = pat_ReverseChainFromToToCommon;
			ReverseChainFromToToCommon_alt_0_rec.embeddingGraph = pat_ReverseChainFromToToCommon;

			ReverseChainFromToToCommon_node_from.PointOfDefinition = null;
			ReverseChainFromToToCommon_node_to.PointOfDefinition = null;
			ReverseChainFromToToCommon_node_common.PointOfDefinition = null;
			ReverseChainFromToToCommon_alt_0_base_edge__edge0.PointOfDefinition = ReverseChainFromToToCommon_alt_0_base;
			ReverseChainFromToToCommon_alt_0_base_edge__edge1.PointOfDefinition = ReverseChainFromToToCommon_alt_0_base;
			ReverseChainFromToToCommon_alt_0_rec_node_intermediate.PointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;
			ReverseChainFromToToCommon_alt_0_rec_edge__edge0.PointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;
			ReverseChainFromToToCommon_alt_0_rec_edge__edge1.PointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;
			ReverseChainFromToToCommon_alt_0_rec__subpattern0.PointOfDefinition = ReverseChainFromToToCommon_alt_0_rec;

			patternGraph = pat_ReverseChainFromToToCommon;
		}



		public void ReverseChainFromToToCommon_Create(LGSPGraph graph, LGSPNode node_from, LGSPNode node_to, LGSPNode node_common)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromToToCommon_addedNodeNames );
			graph.SettingAddedEdgeNames( create_ReverseChainFromToToCommon_addedEdgeNames );
		}
		private static String[] create_ReverseChainFromToToCommon_addedNodeNames = new String[] {  };
		private static String[] create_ReverseChainFromToToCommon_addedEdgeNames = new String[] {  };

		public void ReverseChainFromToToCommon_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch alternative_alt_0 = match.EmbeddedGraphs[(int)ReverseChainFromToToCommon_AltNums.@alt_0 + 0];
			ReverseChainFromToToCommon_alt_0_Delete(graph, alternative_alt_0);
		}

		public void ReverseChainFromToToCommon_alt_0_Delete(LGSPGraph graph, LGSPMatch match)
		{
			if(match.patternGraph == ReverseChainFromToToCommon_alt_0_base) {
				ReverseChainFromToToCommon_alt_0_base_Delete(graph, match);
				return;
			}
			else if(match.patternGraph == ReverseChainFromToToCommon_alt_0_rec) {
				ReverseChainFromToToCommon_alt_0_rec_Delete(graph, match);
				return;
			}
			throw new ApplicationException(); //debug assert
		}

		public void ReverseChainFromToToCommon_alt_0_base_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromToToCommon_alt_0_base_addedNodeNames );
			@Node node_to = @Node.CreateNode(graph);
			@Node node_from = @Node.CreateNode(graph);
			@Node node_common = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ReverseChainFromToToCommon_alt_0_base_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_to, node_from);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_from, node_common);
		}
		private static String[] create_ReverseChainFromToToCommon_alt_0_base_addedNodeNames = new String[] { "to", "from", "common" };
		private static String[] create_ReverseChainFromToToCommon_alt_0_base_addedEdgeNames = new String[] { "_edge0", "_edge1" };

		public void ReverseChainFromToToCommon_alt_0_base_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPEdge edge__edge0 = match.Edges[(int)ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge0];
			LGSPEdge edge__edge1 = match.Edges[(int)ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge1];
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
		}

		public void ReverseChainFromToToCommon_alt_0_rec_Create(LGSPGraph graph)
		{
			graph.SettingAddedNodeNames( create_ReverseChainFromToToCommon_alt_0_rec_addedNodeNames );
			@Node node_intermediate = @Node.CreateNode(graph);
			@Node node_from = @Node.CreateNode(graph);
			@Node node_common = @Node.CreateNode(graph);
			@Node node_to = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( create_ReverseChainFromToToCommon_alt_0_rec_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_intermediate, node_from);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_from, node_common);
			Pattern_ReverseChainFromToToCommon.Instance.ReverseChainFromToToCommon_Create(graph, node_intermediate, node_to, node_common);
		}
		private static String[] create_ReverseChainFromToToCommon_alt_0_rec_addedNodeNames = new String[] { "intermediate", "from", "common", "to" };
		private static String[] create_ReverseChainFromToToCommon_alt_0_rec_addedEdgeNames = new String[] { "_edge0", "_edge1" };

		public void ReverseChainFromToToCommon_alt_0_rec_Delete(LGSPGraph graph, LGSPMatch match)
		{
			LGSPNode node_intermediate = match.Nodes[(int)ReverseChainFromToToCommon_alt_0_rec_NodeNums.@intermediate];
			LGSPEdge edge__edge0 = match.Edges[(int)ReverseChainFromToToCommon_alt_0_rec_EdgeNums.@_edge0];
			LGSPEdge edge__edge1 = match.Edges[(int)ReverseChainFromToToCommon_alt_0_rec_EdgeNums.@_edge1];
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)ReverseChainFromToToCommon_alt_0_rec_SubNums.@_subpattern0];
			graph.Remove(edge__edge0);
			graph.Remove(edge__edge1);
			graph.RemoveEdges(node_intermediate);
			graph.Remove(node_intermediate);
			Pattern_ReverseChainFromToToCommon.Instance.ReverseChainFromToToCommon_Delete(graph, subpattern__subpattern0);
		}
	}

	public class Rule_createChain : LGSPRulePattern
	{
		private static Rule_createChain instance = null;
		public static Rule_createChain Instance { get { if (instance==null) { instance = new Rule_createChain(); instance.initialize(); } return instance; } }

		public enum createChain_NodeNums { };
		public enum createChain_EdgeNums { };
		public enum createChain_VariableNums { };
		public enum createChain_SubNums { };
		public enum createChain_AltNums { };
		PatternGraph pat_createChain;


#if INITIAL_WARMUP
		public Rule_createChain()
#else
		private Rule_createChain()
#endif
		{
			name = "createChain";

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
		}
		public override void initialize()
		{
			bool[,] createChain_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createChain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createChain = new PatternGraph(
				"createChain",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createChain_isNodeHomomorphicGlobal,
				createChain_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createChain;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			graph.SettingAddedNodeNames( createChain_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node__node0);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node__node1, node_end);
			return new object[] { node_beg, node_end, };
		}
		private static String[] createChain_addedNodeNames = new String[] { "beg", "_node0", "_node1", "end" };
		private static String[] createChain_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2" };

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			graph.SettingAddedNodeNames( createChain_addedNodeNames );
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_beg, node__node0);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node0, node__node1);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node__node1, node_end);
			return new object[] { node_beg, node_end, };
		}
	}

	public class Rule_chainFromTo : LGSPRulePattern
	{
		private static Rule_chainFromTo instance = null;
		public static Rule_chainFromTo Instance { get { if (instance==null) { instance = new Rule_chainFromTo(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFromTo_node_beg_AllowedTypes = null;
		public static NodeType[] chainFromTo_node_end_AllowedTypes = null;
		public static bool[] chainFromTo_node_beg_IsAllowedType = null;
		public static bool[] chainFromTo_node_end_IsAllowedType = null;
		public enum chainFromTo_NodeNums { @beg, @end, };
		public enum chainFromTo_EdgeNums { };
		public enum chainFromTo_VariableNums { };
		public enum chainFromTo_SubNums { @_subpattern0, };
		public enum chainFromTo_AltNums { };
		PatternGraph pat_chainFromTo;


#if INITIAL_WARMUP
		public Rule_chainFromTo()
#else
		private Rule_chainFromTo()
#endif
		{
			name = "chainFromTo";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromTo_node_beg", "chainFromTo_node_end", };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] chainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode chainFromTo_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFromTo_node_beg", "beg", chainFromTo_node_beg_AllowedTypes, chainFromTo_node_beg_IsAllowedType, 5.5F, 0);
			PatternNode chainFromTo_node_end = new PatternNode((int) NodeTypes.@Node, "chainFromTo_node_end", "end", chainFromTo_node_end_AllowedTypes, chainFromTo_node_end_IsAllowedType, 5.5F, 1);
			PatternGraphEmbedding chainFromTo__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromTo.Instance, new PatternElement[] { chainFromTo_node_beg, chainFromTo_node_end });
			pat_chainFromTo = new PatternGraph(
				"chainFromTo",
				"",
				false,
				new PatternNode[] { chainFromTo_node_beg, chainFromTo_node_end }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { chainFromTo__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				chainFromTo_isNodeHomomorphicGlobal,
				chainFromTo_isEdgeHomomorphicGlobal
			);

			chainFromTo_node_beg.PointOfDefinition = null;
			chainFromTo_node_end.PointOfDefinition = null;
			chainFromTo__subpattern0.PointOfDefinition = pat_chainFromTo;

			patternGraph = pat_chainFromTo;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)chainFromTo_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)chainFromTo_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}
	}

	public class Rule_chainFrom : LGSPRulePattern
	{
		private static Rule_chainFrom instance = null;
		public static Rule_chainFrom Instance { get { if (instance==null) { instance = new Rule_chainFrom(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFrom_node_beg_AllowedTypes = null;
		public static bool[] chainFrom_node_beg_IsAllowedType = null;
		public enum chainFrom_NodeNums { @beg, };
		public enum chainFrom_EdgeNums { };
		public enum chainFrom_VariableNums { };
		public enum chainFrom_SubNums { @_subpattern0, };
		public enum chainFrom_AltNums { };
		PatternGraph pat_chainFrom;


#if INITIAL_WARMUP
		public Rule_chainFrom()
#else
		private Rule_chainFrom()
#endif
		{
			name = "chainFrom";

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFrom_node_beg", };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] chainFrom_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFrom_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode chainFrom_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFrom_node_beg", "beg", chainFrom_node_beg_AllowedTypes, chainFrom_node_beg_IsAllowedType, 5.5F, 0);
			PatternGraphEmbedding chainFrom__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFrom.Instance, new PatternElement[] { chainFrom_node_beg });
			pat_chainFrom = new PatternGraph(
				"chainFrom",
				"",
				false,
				new PatternNode[] { chainFrom_node_beg }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { chainFrom__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				chainFrom_isNodeHomomorphicGlobal,
				chainFrom_isEdgeHomomorphicGlobal
			);

			chainFrom_node_beg.PointOfDefinition = null;
			chainFrom__subpattern0.PointOfDefinition = pat_chainFrom;

			patternGraph = pat_chainFrom;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)chainFrom_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)chainFrom_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}
	}

	public class Rule_chainFromComplete : LGSPRulePattern
	{
		private static Rule_chainFromComplete instance = null;
		public static Rule_chainFromComplete Instance { get { if (instance==null) { instance = new Rule_chainFromComplete(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFromComplete_node_beg_AllowedTypes = null;
		public static bool[] chainFromComplete_node_beg_IsAllowedType = null;
		public enum chainFromComplete_NodeNums { @beg, };
		public enum chainFromComplete_EdgeNums { };
		public enum chainFromComplete_VariableNums { };
		public enum chainFromComplete_SubNums { @_subpattern0, };
		public enum chainFromComplete_AltNums { };
		PatternGraph pat_chainFromComplete;


#if INITIAL_WARMUP
		public Rule_chainFromComplete()
#else
		private Rule_chainFromComplete()
#endif
		{
			name = "chainFromComplete";

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromComplete_node_beg", };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] chainFromComplete_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] chainFromComplete_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode chainFromComplete_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFromComplete_node_beg", "beg", chainFromComplete_node_beg_AllowedTypes, chainFromComplete_node_beg_IsAllowedType, 5.5F, 0);
			PatternGraphEmbedding chainFromComplete__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ChainFromComplete.Instance, new PatternElement[] { chainFromComplete_node_beg });
			pat_chainFromComplete = new PatternGraph(
				"chainFromComplete",
				"",
				false,
				new PatternNode[] { chainFromComplete_node_beg }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { chainFromComplete__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				chainFromComplete_isNodeHomomorphicGlobal,
				chainFromComplete_isEdgeHomomorphicGlobal
			);

			chainFromComplete_node_beg.PointOfDefinition = null;
			chainFromComplete__subpattern0.PointOfDefinition = pat_chainFromComplete;

			patternGraph = pat_chainFromComplete;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)chainFromComplete_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)chainFromComplete_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}
	}

	public class Rule_createBlowball : LGSPRulePattern
	{
		private static Rule_createBlowball instance = null;
		public static Rule_createBlowball Instance { get { if (instance==null) { instance = new Rule_createBlowball(); instance.initialize(); } return instance; } }

		public enum createBlowball_NodeNums { };
		public enum createBlowball_EdgeNums { };
		public enum createBlowball_VariableNums { };
		public enum createBlowball_SubNums { };
		public enum createBlowball_AltNums { };
		PatternGraph pat_createBlowball;


#if INITIAL_WARMUP
		public Rule_createBlowball()
#else
		private Rule_createBlowball()
#endif
		{
			name = "createBlowball";

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { NodeType_Node.typeVar, };
		}
		public override void initialize()
		{
			bool[,] createBlowball_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createBlowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createBlowball = new PatternGraph(
				"createBlowball",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createBlowball_isNodeHomomorphicGlobal,
				createBlowball_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createBlowball;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			graph.SettingAddedNodeNames( createBlowball_addedNodeNames );
			@Node node_head = @Node.CreateNode(graph);
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node__node2 = @Node.CreateNode(graph);
			@Node node__node3 = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createBlowball_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_head, node__node0);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_head, node__node1);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_head, node__node2);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_head, node__node3);
			return new object[] { node_head, };
		}
		private static String[] createBlowball_addedNodeNames = new String[] { "head", "_node0", "_node1", "_node2", "_node3" };
		private static String[] createBlowball_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2", "_edge3" };

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			graph.SettingAddedNodeNames( createBlowball_addedNodeNames );
			@Node node_head = @Node.CreateNode(graph);
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node__node2 = @Node.CreateNode(graph);
			@Node node__node3 = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createBlowball_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node_head, node__node0);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node_head, node__node1);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_head, node__node2);
			@Edge edge__edge3 = @Edge.CreateEdge(graph, node_head, node__node3);
			return new object[] { node_head, };
		}
	}

	public class Rule_blowball : LGSPRulePattern
	{
		private static Rule_blowball instance = null;
		public static Rule_blowball Instance { get { if (instance==null) { instance = new Rule_blowball(); instance.initialize(); } return instance; } }

		public static NodeType[] blowball_node_head_AllowedTypes = null;
		public static bool[] blowball_node_head_IsAllowedType = null;
		public enum blowball_NodeNums { @head, };
		public enum blowball_EdgeNums { };
		public enum blowball_VariableNums { };
		public enum blowball_SubNums { @_subpattern0, };
		public enum blowball_AltNums { };
		PatternGraph pat_blowball;


#if INITIAL_WARMUP
		public Rule_blowball()
#else
		private Rule_blowball()
#endif
		{
			name = "blowball";

			inputs = new GrGenType[] { NodeType_Node.typeVar, };
			inputNames = new string[] { "blowball_node_head", };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] blowball_isNodeHomomorphicGlobal = new bool[1, 1] {
				{ false, },
			};
			bool[,] blowball_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode blowball_node_head = new PatternNode((int) NodeTypes.@Node, "blowball_node_head", "head", blowball_node_head_AllowedTypes, blowball_node_head_IsAllowedType, 5.5F, 0);
			PatternGraphEmbedding blowball__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_Blowball.Instance, new PatternElement[] { blowball_node_head });
			pat_blowball = new PatternGraph(
				"blowball",
				"",
				false,
				new PatternNode[] { blowball_node_head }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { blowball__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[1, 1] {
					{ true, },
				},
				new bool[0, 0] ,
				blowball_isNodeHomomorphicGlobal,
				blowball_isEdgeHomomorphicGlobal
			);

			blowball_node_head.PointOfDefinition = null;
			blowball__subpattern0.PointOfDefinition = pat_blowball;

			patternGraph = pat_blowball;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)blowball_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)blowball_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}
	}

	public class Rule_reverseChainFromTo : LGSPRulePattern
	{
		private static Rule_reverseChainFromTo instance = null;
		public static Rule_reverseChainFromTo Instance { get { if (instance==null) { instance = new Rule_reverseChainFromTo(); instance.initialize(); } return instance; } }

		public static NodeType[] reverseChainFromTo_node_beg_AllowedTypes = null;
		public static NodeType[] reverseChainFromTo_node_end_AllowedTypes = null;
		public static bool[] reverseChainFromTo_node_beg_IsAllowedType = null;
		public static bool[] reverseChainFromTo_node_end_IsAllowedType = null;
		public enum reverseChainFromTo_NodeNums { @beg, @end, };
		public enum reverseChainFromTo_EdgeNums { };
		public enum reverseChainFromTo_VariableNums { };
		public enum reverseChainFromTo_SubNums { @_subpattern0, };
		public enum reverseChainFromTo_AltNums { };
		PatternGraph pat_reverseChainFromTo;


#if INITIAL_WARMUP
		public Rule_reverseChainFromTo()
#else
		private Rule_reverseChainFromTo()
#endif
		{
			name = "reverseChainFromTo";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "reverseChainFromTo_node_beg", "reverseChainFromTo_node_end", };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] reverseChainFromTo_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] reverseChainFromTo_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode reverseChainFromTo_node_beg = new PatternNode((int) NodeTypes.@Node, "reverseChainFromTo_node_beg", "beg", reverseChainFromTo_node_beg_AllowedTypes, reverseChainFromTo_node_beg_IsAllowedType, 5.5F, 0);
			PatternNode reverseChainFromTo_node_end = new PatternNode((int) NodeTypes.@Node, "reverseChainFromTo_node_end", "end", reverseChainFromTo_node_end_AllowedTypes, reverseChainFromTo_node_end_IsAllowedType, 5.5F, 1);
			PatternGraphEmbedding reverseChainFromTo__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromTo.Instance, new PatternElement[] { reverseChainFromTo_node_beg, reverseChainFromTo_node_end });
			pat_reverseChainFromTo = new PatternGraph(
				"reverseChainFromTo",
				"",
				false,
				new PatternNode[] { reverseChainFromTo_node_beg, reverseChainFromTo_node_end }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { reverseChainFromTo__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				reverseChainFromTo_isNodeHomomorphicGlobal,
				reverseChainFromTo_isEdgeHomomorphicGlobal
			);

			reverseChainFromTo_node_beg.PointOfDefinition = null;
			reverseChainFromTo_node_end.PointOfDefinition = null;
			reverseChainFromTo__subpattern0.PointOfDefinition = pat_reverseChainFromTo;

			patternGraph = pat_reverseChainFromTo;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)reverseChainFromTo_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)reverseChainFromTo_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}
	}

	public class Rule_createReverseChain : LGSPRulePattern
	{
		private static Rule_createReverseChain instance = null;
		public static Rule_createReverseChain Instance { get { if (instance==null) { instance = new Rule_createReverseChain(); instance.initialize(); } return instance; } }

		public enum createReverseChain_NodeNums { };
		public enum createReverseChain_EdgeNums { };
		public enum createReverseChain_VariableNums { };
		public enum createReverseChain_SubNums { };
		public enum createReverseChain_AltNums { };
		PatternGraph pat_createReverseChain;


#if INITIAL_WARMUP
		public Rule_createReverseChain()
#else
		private Rule_createReverseChain()
#endif
		{
			name = "createReverseChain";

			inputs = new GrGenType[] { };
			inputNames = new string[] { };
			outputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
		}
		public override void initialize()
		{
			bool[,] createReverseChain_isNodeHomomorphicGlobal = new bool[0, 0] ;
			bool[,] createReverseChain_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			pat_createReverseChain = new PatternGraph(
				"createReverseChain",
				"",
				false,
				new PatternNode[] {  }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] {  }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[0, 0] ,
				new bool[0, 0] ,
				createReverseChain_isNodeHomomorphicGlobal,
				createReverseChain_isEdgeHomomorphicGlobal
			);


			patternGraph = pat_createReverseChain;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			graph.SettingAddedNodeNames( createReverseChain_addedNodeNames );
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createReverseChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node_beg);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node1, node__node0);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_end, node__node1);
			return new object[] { node_beg, node_end, };
		}
		private static String[] createReverseChain_addedNodeNames = new String[] { "_node0", "beg", "_node1", "end" };
		private static String[] createReverseChain_addedEdgeNames = new String[] { "_edge0", "_edge1", "_edge2" };

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			graph.SettingAddedNodeNames( createReverseChain_addedNodeNames );
			@Node node__node0 = @Node.CreateNode(graph);
			@Node node_beg = @Node.CreateNode(graph);
			@Node node__node1 = @Node.CreateNode(graph);
			@Node node_end = @Node.CreateNode(graph);
			graph.SettingAddedEdgeNames( createReverseChain_addedEdgeNames );
			@Edge edge__edge0 = @Edge.CreateEdge(graph, node__node0, node_beg);
			@Edge edge__edge1 = @Edge.CreateEdge(graph, node__node1, node__node0);
			@Edge edge__edge2 = @Edge.CreateEdge(graph, node_end, node__node1);
			return new object[] { node_beg, node_end, };
		}
	}

	public class Rule_chainFromToReverse : LGSPRulePattern
	{
		private static Rule_chainFromToReverse instance = null;
		public static Rule_chainFromToReverse Instance { get { if (instance==null) { instance = new Rule_chainFromToReverse(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFromToReverse_node_beg_AllowedTypes = null;
		public static NodeType[] chainFromToReverse_node_end_AllowedTypes = null;
		public static bool[] chainFromToReverse_node_beg_IsAllowedType = null;
		public static bool[] chainFromToReverse_node_end_IsAllowedType = null;
		public enum chainFromToReverse_NodeNums { @beg, @end, };
		public enum chainFromToReverse_EdgeNums { };
		public enum chainFromToReverse_VariableNums { };
		public enum chainFromToReverse_SubNums { @cftr, };
		public enum chainFromToReverse_AltNums { };
		PatternGraph pat_chainFromToReverse;


#if INITIAL_WARMUP
		public Rule_chainFromToReverse()
#else
		private Rule_chainFromToReverse()
#endif
		{
			name = "chainFromToReverse";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromToReverse_node_beg", "chainFromToReverse_node_end", };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] chainFromToReverse_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromToReverse_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode chainFromToReverse_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFromToReverse_node_beg", "beg", chainFromToReverse_node_beg_AllowedTypes, chainFromToReverse_node_beg_IsAllowedType, 5.5F, 0);
			PatternNode chainFromToReverse_node_end = new PatternNode((int) NodeTypes.@Node, "chainFromToReverse_node_end", "end", chainFromToReverse_node_end_AllowedTypes, chainFromToReverse_node_end_IsAllowedType, 5.5F, 1);
			PatternGraphEmbedding chainFromToReverse_cftr = new PatternGraphEmbedding("cftr", Pattern_ChainFromToReverse.Instance, new PatternElement[] { chainFromToReverse_node_beg, chainFromToReverse_node_end });
			pat_chainFromToReverse = new PatternGraph(
				"chainFromToReverse",
				"",
				false,
				new PatternNode[] { chainFromToReverse_node_beg, chainFromToReverse_node_end }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { chainFromToReverse_cftr }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				chainFromToReverse_isNodeHomomorphicGlobal,
				chainFromToReverse_isEdgeHomomorphicGlobal
			);

			chainFromToReverse_node_beg.PointOfDefinition = null;
			chainFromToReverse_node_end.PointOfDefinition = null;
			chainFromToReverse_cftr.PointOfDefinition = pat_chainFromToReverse;

			patternGraph = pat_chainFromToReverse;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern_cftr = match.EmbeddedGraphs[(int)chainFromToReverse_SubNums.@cftr];
			graph.SettingAddedNodeNames( chainFromToReverse_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(graph, subpattern_cftr);
			graph.SettingAddedEdgeNames( chainFromToReverse_addedEdgeNames );
			return EmptyReturnElements;
		}
		private static String[] chainFromToReverse_addedNodeNames = new String[] {  };
		private static String[] chainFromToReverse_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern_cftr = match.EmbeddedGraphs[(int)chainFromToReverse_SubNums.@cftr];
			graph.SettingAddedNodeNames( chainFromToReverse_addedNodeNames );
			Pattern_ChainFromToReverse.Instance.ChainFromToReverse_Modify(graph, subpattern_cftr);
			graph.SettingAddedEdgeNames( chainFromToReverse_addedEdgeNames );
			return EmptyReturnElements;
		}
	}

	public class Rule_chainFromToReverseToCommon : LGSPRulePattern
	{
		private static Rule_chainFromToReverseToCommon instance = null;
		public static Rule_chainFromToReverseToCommon Instance { get { if (instance==null) { instance = new Rule_chainFromToReverseToCommon(); instance.initialize(); } return instance; } }

		public static NodeType[] chainFromToReverseToCommon_node_beg_AllowedTypes = null;
		public static NodeType[] chainFromToReverseToCommon_node_end_AllowedTypes = null;
		public static bool[] chainFromToReverseToCommon_node_beg_IsAllowedType = null;
		public static bool[] chainFromToReverseToCommon_node_end_IsAllowedType = null;
		public enum chainFromToReverseToCommon_NodeNums { @beg, @end, };
		public enum chainFromToReverseToCommon_EdgeNums { };
		public enum chainFromToReverseToCommon_VariableNums { };
		public enum chainFromToReverseToCommon_SubNums { @cftrtc, };
		public enum chainFromToReverseToCommon_AltNums { };
		PatternGraph pat_chainFromToReverseToCommon;


#if INITIAL_WARMUP
		public Rule_chainFromToReverseToCommon()
#else
		private Rule_chainFromToReverseToCommon()
#endif
		{
			name = "chainFromToReverseToCommon";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "chainFromToReverseToCommon_node_beg", "chainFromToReverseToCommon_node_end", };
			outputs = new GrGenType[] { NodeType_Node.typeVar, };
		}
		public override void initialize()
		{
			bool[,] chainFromToReverseToCommon_isNodeHomomorphicGlobal = new bool[2, 2] {
				{ false, false, },
				{ false, false, },
			};
			bool[,] chainFromToReverseToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode chainFromToReverseToCommon_node_beg = new PatternNode((int) NodeTypes.@Node, "chainFromToReverseToCommon_node_beg", "beg", chainFromToReverseToCommon_node_beg_AllowedTypes, chainFromToReverseToCommon_node_beg_IsAllowedType, 5.5F, 0);
			PatternNode chainFromToReverseToCommon_node_end = new PatternNode((int) NodeTypes.@Node, "chainFromToReverseToCommon_node_end", "end", chainFromToReverseToCommon_node_end_AllowedTypes, chainFromToReverseToCommon_node_end_IsAllowedType, 5.5F, 1);
			PatternGraphEmbedding chainFromToReverseToCommon_cftrtc = new PatternGraphEmbedding("cftrtc", Pattern_ChainFromToReverseToCommon.Instance, new PatternElement[] { chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end });
			pat_chainFromToReverseToCommon = new PatternGraph(
				"chainFromToReverseToCommon",
				"",
				false,
				new PatternNode[] { chainFromToReverseToCommon_node_beg, chainFromToReverseToCommon_node_end }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { chainFromToReverseToCommon_cftrtc }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[2, 2] {
					{ true, false, },
					{ false, true, },
				},
				new bool[0, 0] ,
				chainFromToReverseToCommon_isNodeHomomorphicGlobal,
				chainFromToReverseToCommon_isEdgeHomomorphicGlobal
			);

			chainFromToReverseToCommon_node_beg.PointOfDefinition = null;
			chainFromToReverseToCommon_node_end.PointOfDefinition = null;
			chainFromToReverseToCommon_cftrtc.PointOfDefinition = pat_chainFromToReverseToCommon;

			patternGraph = pat_chainFromToReverseToCommon;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern_cftrtc = match.EmbeddedGraphs[(int)chainFromToReverseToCommon_SubNums.@cftrtc];
			graph.SettingAddedNodeNames( chainFromToReverseToCommon_addedNodeNames );
			@Node node_common = @Node.CreateNode(graph);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(graph, subpattern_cftrtc, node_common);
			graph.SettingAddedEdgeNames( chainFromToReverseToCommon_addedEdgeNames );
			return new object[] { node_common, };
		}
		private static String[] chainFromToReverseToCommon_addedNodeNames = new String[] { "common" };
		private static String[] chainFromToReverseToCommon_addedEdgeNames = new String[] {  };

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern_cftrtc = match.EmbeddedGraphs[(int)chainFromToReverseToCommon_SubNums.@cftrtc];
			graph.SettingAddedNodeNames( chainFromToReverseToCommon_addedNodeNames );
			@Node node_common = @Node.CreateNode(graph);
			Pattern_ChainFromToReverseToCommon.Instance.ChainFromToReverseToCommon_Modify(graph, subpattern_cftrtc, node_common);
			graph.SettingAddedEdgeNames( chainFromToReverseToCommon_addedEdgeNames );
			return new object[] { node_common, };
		}
	}

	public class Rule_reverseChainFromToToCommon : LGSPRulePattern
	{
		private static Rule_reverseChainFromToToCommon instance = null;
		public static Rule_reverseChainFromToToCommon Instance { get { if (instance==null) { instance = new Rule_reverseChainFromToToCommon(); instance.initialize(); } return instance; } }

		public static NodeType[] reverseChainFromToToCommon_node_beg_AllowedTypes = null;
		public static NodeType[] reverseChainFromToToCommon_node_end_AllowedTypes = null;
		public static NodeType[] reverseChainFromToToCommon_node_common_AllowedTypes = null;
		public static bool[] reverseChainFromToToCommon_node_beg_IsAllowedType = null;
		public static bool[] reverseChainFromToToCommon_node_end_IsAllowedType = null;
		public static bool[] reverseChainFromToToCommon_node_common_IsAllowedType = null;
		public enum reverseChainFromToToCommon_NodeNums { @beg, @end, @common, };
		public enum reverseChainFromToToCommon_EdgeNums { };
		public enum reverseChainFromToToCommon_VariableNums { };
		public enum reverseChainFromToToCommon_SubNums { @_subpattern0, };
		public enum reverseChainFromToToCommon_AltNums { };
		PatternGraph pat_reverseChainFromToToCommon;


#if INITIAL_WARMUP
		public Rule_reverseChainFromToToCommon()
#else
		private Rule_reverseChainFromToToCommon()
#endif
		{
			name = "reverseChainFromToToCommon";

			inputs = new GrGenType[] { NodeType_Node.typeVar, NodeType_Node.typeVar, NodeType_Node.typeVar, };
			inputNames = new string[] { "reverseChainFromToToCommon_node_beg", "reverseChainFromToToCommon_node_end", "reverseChainFromToToCommon_node_common", };
			outputs = new GrGenType[] { };
		}
		public override void initialize()
		{
			bool[,] reverseChainFromToToCommon_isNodeHomomorphicGlobal = new bool[3, 3] {
				{ false, false, false, },
				{ false, false, false, },
				{ false, false, false, },
			};
			bool[,] reverseChainFromToToCommon_isEdgeHomomorphicGlobal = new bool[0, 0] ;
			PatternNode reverseChainFromToToCommon_node_beg = new PatternNode((int) NodeTypes.@Node, "reverseChainFromToToCommon_node_beg", "beg", reverseChainFromToToCommon_node_beg_AllowedTypes, reverseChainFromToToCommon_node_beg_IsAllowedType, 5.5F, 0);
			PatternNode reverseChainFromToToCommon_node_end = new PatternNode((int) NodeTypes.@Node, "reverseChainFromToToCommon_node_end", "end", reverseChainFromToToCommon_node_end_AllowedTypes, reverseChainFromToToCommon_node_end_IsAllowedType, 5.5F, 1);
			PatternNode reverseChainFromToToCommon_node_common = new PatternNode((int) NodeTypes.@Node, "reverseChainFromToToCommon_node_common", "common", reverseChainFromToToCommon_node_common_AllowedTypes, reverseChainFromToToCommon_node_common_IsAllowedType, 5.5F, 2);
			PatternGraphEmbedding reverseChainFromToToCommon__subpattern0 = new PatternGraphEmbedding("_subpattern0", Pattern_ReverseChainFromToToCommon.Instance, new PatternElement[] { reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common });
			pat_reverseChainFromToToCommon = new PatternGraph(
				"reverseChainFromToToCommon",
				"",
				false,
				new PatternNode[] { reverseChainFromToToCommon_node_beg, reverseChainFromToToCommon_node_end, reverseChainFromToToCommon_node_common }, 
				new PatternEdge[] {  }, 
				new PatternVariable[] {  }, 
				new PatternGraphEmbedding[] { reverseChainFromToToCommon__subpattern0 }, 
				new Alternative[] {  }, 
				new PatternGraph[] {  }, 
				new PatternCondition[] {  }, 
				new bool[3, 3] {
					{ true, false, false, },
					{ false, true, false, },
					{ false, false, true, },
				},
				new bool[0, 0] ,
				reverseChainFromToToCommon_isNodeHomomorphicGlobal,
				reverseChainFromToToCommon_isEdgeHomomorphicGlobal
			);

			reverseChainFromToToCommon_node_beg.PointOfDefinition = null;
			reverseChainFromToToCommon_node_end.PointOfDefinition = null;
			reverseChainFromToToCommon_node_common.PointOfDefinition = null;
			reverseChainFromToToCommon__subpattern0.PointOfDefinition = pat_reverseChainFromToToCommon;

			patternGraph = pat_reverseChainFromToToCommon;
		}



		public override object[] Modify(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)reverseChainFromToToCommon_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}

		public override object[] ModifyNoReuse(LGSPGraph graph, LGSPMatch match)
		{
			LGSPMatch subpattern__subpattern0 = match.EmbeddedGraphs[(int)reverseChainFromToToCommon_SubNums.@_subpattern0];
			return EmptyReturnElements;
		}
	}


    public class PatternAction_ChainFromTo : LGSPSubpatternAction
    {
        private PatternAction_ChainFromTo(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromTo.Instance.patternGraph;
        }

        public static PatternAction_ChainFromTo getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromTo newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromTo(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromTo oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromTo freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromTo next = null;

        public LGSPNode ChainFromTo_node_from;
        public LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromTo_node_from 
            LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
            // SubPreset ChainFromTo_node_to 
            LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
            // Push alternative matching task for ChainFromTo_alt_0
            AlternativeAction_ChainFromTo_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromTo_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFromTo.ChainFromTo_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromTo_node_from = candidate_ChainFromTo_node_from;
            taskFor_alt_0.ChainFromTo_node_to = candidate_ChainFromTo_node_to;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromTo_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_NodeNums.@from] = candidate_ChainFromTo_node_from;
                    match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_NodeNums.@to] = candidate_ChainFromTo_node_to;
                    match.EmbeddedGraphs[((int)Pattern_ChainFromTo.ChainFromTo_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_ChainFromTo_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromTo_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromTo_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromTo_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromTo_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromTo_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromTo_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromTo_alt_0 next = null;

        public LGSPNode ChainFromTo_node_from;
        public LGSPNode ChainFromTo_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromTo_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_CaseNums.@base];
                // SubPreset ChainFromTo_node_from 
                LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
                // SubPreset ChainFromTo_node_to 
                LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
                // Extend Outgoing ChainFromTo_alt_0_base_edge__edge0 from ChainFromTo_node_from 
                LGSPEdge head_candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_node_from.outhead;
                if(head_candidate_ChainFromTo_alt_0_base_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromTo_alt_0_base_edge__edge0 = head_candidate_ChainFromTo_alt_0_base_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromTo_alt_0_base_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(candidate_ChainFromTo_alt_0_base_edge__edge0.target != candidate_ChainFromTo_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@from] = candidate_ChainFromTo_node_from;
                            match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@to] = candidate_ChainFromTo_node_to;
                            match.Edges[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromTo_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                        prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_base_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@from] = candidate_ChainFromTo_node_from;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_NodeNums.@to] = candidate_ChainFromTo_node_to;
                                match.Edges[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromTo_alt_0_base_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromTo_alt_0_base_edge__edge0.flags = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_base_edge__edge0.flags = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromTo_alt_0_base_edge__edge0.flags = candidate_ChainFromTo_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromTo_alt_0_base_edge__edge0 = candidate_ChainFromTo_alt_0_base_edge__edge0.outNext) != head_candidate_ChainFromTo_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case ChainFromTo_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_CaseNums.@rec];
                // SubPreset ChainFromTo_node_from 
                LGSPNode candidate_ChainFromTo_node_from = ChainFromTo_node_from;
                // SubPreset ChainFromTo_node_to 
                LGSPNode candidate_ChainFromTo_node_to = ChainFromTo_node_to;
                // Extend Outgoing ChainFromTo_alt_0_rec_edge__edge0 from ChainFromTo_node_from 
                LGSPEdge head_candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_node_from.outhead;
                if(head_candidate_ChainFromTo_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromTo_alt_0_rec_edge__edge0 = head_candidate_ChainFromTo_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromTo_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromTo_alt_0_rec_node_intermediate from ChainFromTo_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFromTo_alt_0_rec_node_intermediate = candidate_ChainFromTo_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromTo_alt_0_rec_node_intermediate))
                            && candidate_ChainFromTo_alt_0_rec_node_intermediate==candidate_ChainFromTo_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFromTo taskFor__subpattern0 = PatternAction_ChainFromTo.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ChainFromTo_node_from = candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        taskFor__subpattern0.ChainFromTo_node_to = candidate_ChainFromTo_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[1], new object[0], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_NodeNums.@from] = candidate_ChainFromTo_node_from;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_NodeNums.@intermediate] = candidate_ChainFromTo_alt_0_rec_node_intermediate;
                                match.Nodes[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_NodeNums.@to] = candidate_ChainFromTo_node_to;
                                match.Edges[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFromTo_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFromTo.ChainFromTo_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                                candidate_ChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                            candidate_ChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ChainFromTo_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_node_intermediate;
                        candidate_ChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ChainFromTo_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromTo_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromTo_alt_0_rec_edge__edge0 = candidate_ChainFromTo_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromTo_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFrom : LGSPSubpatternAction
    {
        private PatternAction_ChainFrom(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFrom.Instance.patternGraph;
        }

        public static PatternAction_ChainFrom getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFrom newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFrom(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_ChainFrom oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFrom freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFrom next = null;

        public LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFrom_node_from 
            LGSPNode candidate_ChainFrom_node_from = ChainFrom_node_from;
            // Push alternative matching task for ChainFrom_alt_0
            AlternativeAction_ChainFrom_alt_0 taskFor_alt_0 = AlternativeAction_ChainFrom_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFrom.ChainFrom_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFrom_node_from = candidate_ChainFrom_node_from;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_ChainFrom_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFrom.ChainFrom_NodeNums.@from] = candidate_ChainFrom_node_from;
                    match.EmbeddedGraphs[((int)Pattern_ChainFrom.ChainFrom_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_ChainFrom_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_ChainFrom_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFrom_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFrom_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFrom_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFrom_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFrom_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFrom_alt_0 next = null;

        public LGSPNode ChainFrom_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFrom_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_CaseNums.@base];
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    LGSPMatch match = new LGSPMatch(new LGSPNode[0], new LGSPEdge[0], new object[0], new LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    continue;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[0], new LGSPEdge[0], new object[0], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
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
                    continue;
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case ChainFrom_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_CaseNums.@rec];
                // SubPreset ChainFrom_node_from 
                LGSPNode candidate_ChainFrom_node_from = ChainFrom_node_from;
                // Extend Outgoing ChainFrom_alt_0_rec_edge__edge0 from ChainFrom_node_from 
                LGSPEdge head_candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_node_from.outhead;
                if(head_candidate_ChainFrom_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFrom_alt_0_rec_edge__edge0 = head_candidate_ChainFrom_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFrom_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFrom_alt_0_rec_node_to from ChainFrom_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFrom_alt_0_rec_node_to = candidate_ChainFrom_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFrom_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFrom_alt_0_rec_node_to))
                            && candidate_ChainFrom_alt_0_rec_node_to==candidate_ChainFrom_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFrom_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFrom taskFor__subpattern0 = PatternAction_ChainFrom.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ChainFrom_node_from = candidate_ChainFrom_alt_0_rec_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFrom_alt_0_rec_node_to = candidate_ChainFrom_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFrom_alt_0_rec_node_to.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ChainFrom.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_NodeNums.@from] = candidate_ChainFrom_node_from;
                                match.Nodes[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_NodeNums.@to] = candidate_ChainFrom_alt_0_rec_node_to;
                                match.Edges[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFrom_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFrom.ChainFrom_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFrom_alt_0_rec_edge__edge0.flags = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                                candidate_ChainFrom_alt_0_rec_node_to.flags = candidate_ChainFrom_alt_0_rec_node_to.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFrom_alt_0_rec_edge__edge0.flags = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                            candidate_ChainFrom_alt_0_rec_node_to.flags = candidate_ChainFrom_alt_0_rec_node_to.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFrom_alt_0_rec_node_to.flags = candidate_ChainFrom_alt_0_rec_node_to.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_node_to;
                        candidate_ChainFrom_alt_0_rec_edge__edge0.flags = candidate_ChainFrom_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFrom_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFrom_alt_0_rec_edge__edge0 = candidate_ChainFrom_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFrom_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromComplete : LGSPSubpatternAction
    {
        private PatternAction_ChainFromComplete(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromComplete.Instance.patternGraph;
        }

        public static PatternAction_ChainFromComplete getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromComplete newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromComplete(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromComplete oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromComplete freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromComplete next = null;

        public LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromComplete_node_from 
            LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
            // Push alternative matching task for ChainFromComplete_alt_0
            AlternativeAction_ChainFromComplete_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromComplete_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFromComplete.ChainFromComplete_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromComplete_node_from = candidate_ChainFromComplete_node_from;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromComplete_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                    match.EmbeddedGraphs[((int)Pattern_ChainFromComplete.ChainFromComplete_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_ChainFromComplete_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromComplete_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromComplete_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromComplete_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromComplete_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromComplete_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromComplete_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromComplete_alt_0 next = null;

        public LGSPNode ChainFromComplete_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromComplete_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_CaseNums.@base];
                // SubPreset ChainFromComplete_node_from 
                LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > MAX_NEG_LEVEL && negLevel-MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst = new Dictionary<LGSPNode, LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd = new Dictionary<LGSPEdge, LGSPEdge>();
                    }
                    uint prev_neg_0__candidate_ChainFromComplete_node_from;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        prev_neg_0__candidate_ChainFromComplete_node_from = candidate_ChainFromComplete_node_from.flags & LGSPNode.IS_MATCHED<<negLevel;
                        candidate_ChainFromComplete_node_from.flags |= LGSPNode.IS_MATCHED<<negLevel;
                    } else {
                        prev_neg_0__candidate_ChainFromComplete_node_from = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_node_from) ? 1U : 0U;
                        if(prev_neg_0__candidate_ChainFromComplete_node_from==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_ChainFromComplete_node_from,candidate_ChainFromComplete_node_from);
                    }
                    // Extend Outgoing ChainFromComplete_alt_0_base_neg_0_edge__edge0 from ChainFromComplete_node_from 
                    LGSPEdge head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_node_from.outhead;
                    if(head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_Edge.isMyType[candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target ChainFromComplete_alt_0_base_neg_0_node__node0 from ChainFromComplete_alt_0_base_neg_0_edge__edge0 
                            LGSPNode candidate_ChainFromComplete_alt_0_base_neg_0_node__node0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.target;
                            if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_alt_0_base_neg_0_node__node0))
                                && candidate_ChainFromComplete_alt_0_base_neg_0_node__node0==candidate_ChainFromComplete_node_from
                                )
                            {
                                continue;
                            }
                            if((candidate_ChainFromComplete_alt_0_base_neg_0_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ChainFromComplete_node_from.flags = candidate_ChainFromComplete_node_from.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev_neg_0__candidate_ChainFromComplete_node_from;
                            } else { 
                                if(prev_neg_0__candidate_ChainFromComplete_node_from==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ChainFromComplete_node_from);
                                }
                            }
                            if(negLevel > MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                            }
                            --negLevel;
                            goto label0;
                        }
                        while( (candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 = candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0.outNext) != head_candidate_ChainFromComplete_alt_0_base_neg_0_edge__edge0 );
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_ChainFromComplete_node_from.flags = candidate_ChainFromComplete_node_from.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev_neg_0__candidate_ChainFromComplete_node_from;
                    } else { 
                        if(prev_neg_0__candidate_ChainFromComplete_node_from==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_ChainFromComplete_node_from);
                        }
                    }
                    if(negLevel > MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                    }
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new object[0], new LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_base_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label1;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new object[0], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_base_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
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
                    goto label2;
                }
label0: ;
label1: ;
label2: ;
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case ChainFromComplete_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_CaseNums.@rec];
                // SubPreset ChainFromComplete_node_from 
                LGSPNode candidate_ChainFromComplete_node_from = ChainFromComplete_node_from;
                // Extend Outgoing ChainFromComplete_alt_0_rec_edge__edge0 from ChainFromComplete_node_from 
                LGSPEdge head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_node_from.outhead;
                if(head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromComplete_alt_0_rec_edge__edge0 = head_candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromComplete_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromComplete_alt_0_rec_node_to from ChainFromComplete_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFromComplete_alt_0_rec_node_to = candidate_ChainFromComplete_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromComplete_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromComplete_alt_0_rec_node_to))
                            && candidate_ChainFromComplete_alt_0_rec_node_to==candidate_ChainFromComplete_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromComplete_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ChainFromComplete taskFor__subpattern0 = PatternAction_ChainFromComplete.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ChainFromComplete_node_from = candidate_ChainFromComplete_alt_0_rec_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                        prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to = candidate_ChainFromComplete_alt_0_rec_node_to.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromComplete_alt_0_rec_node_to.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ChainFromComplete.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_NodeNums.@from] = candidate_ChainFromComplete_node_from;
                                match.Nodes[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_NodeNums.@to] = candidate_ChainFromComplete_alt_0_rec_node_to;
                                match.Edges[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFromComplete.ChainFromComplete_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                                candidate_ChainFromComplete_alt_0_rec_node_to.flags = candidate_ChainFromComplete_alt_0_rec_node_to.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                            candidate_ChainFromComplete_alt_0_rec_node_to.flags = candidate_ChainFromComplete_alt_0_rec_node_to.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                            continue;
                        }
                        candidate_ChainFromComplete_alt_0_rec_node_to.flags = candidate_ChainFromComplete_alt_0_rec_node_to.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_node_to;
                        candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags = candidate_ChainFromComplete_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromComplete_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromComplete_alt_0_rec_edge__edge0 = candidate_ChainFromComplete_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromComplete_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_Blowball : LGSPSubpatternAction
    {
        private PatternAction_Blowball(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_Blowball.Instance.patternGraph;
        }

        public static PatternAction_Blowball getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_Blowball newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_Blowball(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_Blowball oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_Blowball freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_Blowball next = null;

        public LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset Blowball_node_head 
            LGSPNode candidate_Blowball_node_head = Blowball_node_head;
            // Push alternative matching task for Blowball_alt_0
            AlternativeAction_Blowball_alt_0 taskFor_alt_0 = AlternativeAction_Blowball_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_Blowball.Blowball_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.Blowball_node_head = candidate_Blowball_node_head;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_Blowball_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_Blowball.Blowball_NodeNums.@head] = candidate_Blowball_node_head;
                    match.EmbeddedGraphs[((int)Pattern_Blowball.Blowball_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_Blowball_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_Blowball_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_Blowball_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_Blowball_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_Blowball_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_Blowball_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_Blowball_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_Blowball_alt_0 next = null;

        public LGSPNode Blowball_node_head;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case Blowball_alt_0_end 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@end];
                // SubPreset Blowball_node_head 
                LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // NegativePattern 
                {
                    ++negLevel;
                    if(negLevel > MAX_NEG_LEVEL && negLevel-MAX_NEG_LEVEL > graph.atNegLevelMatchedElements.Count) {
                        graph.atNegLevelMatchedElements.Add(new Pair<Dictionary<LGSPNode, LGSPNode>, Dictionary<LGSPEdge, LGSPEdge>>());
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst = new Dictionary<LGSPNode, LGSPNode>();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd = new Dictionary<LGSPEdge, LGSPEdge>();
                    }
                    uint prev_neg_0__candidate_Blowball_node_head;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        prev_neg_0__candidate_Blowball_node_head = candidate_Blowball_node_head.flags & LGSPNode.IS_MATCHED<<negLevel;
                        candidate_Blowball_node_head.flags |= LGSPNode.IS_MATCHED<<negLevel;
                    } else {
                        prev_neg_0__candidate_Blowball_node_head = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_node_head) ? 1U : 0U;
                        if(prev_neg_0__candidate_Blowball_node_head==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_Blowball_node_head,candidate_Blowball_node_head);
                    }
                    // Extend Outgoing Blowball_alt_0_end_neg_0_edge__edge0 from Blowball_node_head 
                    LGSPEdge head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_node_head.outhead;
                    if(head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 != null)
                    {
                        LGSPEdge candidate_Blowball_alt_0_end_neg_0_edge__edge0 = head_candidate_Blowball_alt_0_end_neg_0_edge__edge0;
                        do
                        {
                            if(!EdgeType_Edge.isMyType[candidate_Blowball_alt_0_end_neg_0_edge__edge0.type.TypeID]) {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // Implicit Target Blowball_alt_0_end_neg_0_node__node0 from Blowball_alt_0_end_neg_0_edge__edge0 
                            LGSPNode candidate_Blowball_alt_0_end_neg_0_node__node0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.target;
                            if((negLevel<=MAX_NEG_LEVEL ? (candidate_Blowball_alt_0_end_neg_0_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_alt_0_end_neg_0_node__node0))
                                && candidate_Blowball_alt_0_end_neg_0_node__node0==candidate_Blowball_node_head
                                )
                            {
                                continue;
                            }
                            if((candidate_Blowball_alt_0_end_neg_0_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                            {
                                continue;
                            }
                            // negative pattern found
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_Blowball_node_head.flags = candidate_Blowball_node_head.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev_neg_0__candidate_Blowball_node_head;
                            } else { 
                                if(prev_neg_0__candidate_Blowball_node_head==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Blowball_node_head);
                                }
                            }
                            if(negLevel > MAX_NEG_LEVEL) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                            }
                            --negLevel;
                            goto label3;
                        }
                        while( (candidate_Blowball_alt_0_end_neg_0_edge__edge0 = candidate_Blowball_alt_0_end_neg_0_edge__edge0.outNext) != head_candidate_Blowball_alt_0_end_neg_0_edge__edge0 );
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_Blowball_node_head.flags = candidate_Blowball_node_head.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev_neg_0__candidate_Blowball_node_head;
                    } else { 
                        if(prev_neg_0__candidate_Blowball_node_head==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_Blowball_node_head);
                        }
                    }
                    if(negLevel > MAX_NEG_LEVEL) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Clear();
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Clear();
                    }
                    --negLevel;
                }
                // Check whether there are subpattern matching tasks left to execute
                if(openTasks.Count==0)
                {
                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                    foundPartialMatches.Add(currentFoundPartialMatch);
                    LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new object[0], new LGSPMatch[0]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_end_NodeNums.@head] = candidate_Blowball_node_head;
                    currentFoundPartialMatch.Push(match);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                    {
                        openTasks.Push(this);
                        return;
                    }
                    goto label4;
                }
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = new LGSPMatch(new LGSPNode[1], new LGSPEdge[0], new object[0], new LGSPMatch[0+0]);
                        match.patternGraph = patternGraph;
                        match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_end_NodeNums.@head] = candidate_Blowball_node_head;
                        currentFoundPartialMatch.Push(match);
                    }
                    if(matchesList==foundPartialMatches) {
                        matchesList = new List<Stack<LGSPMatch>>();
                    } else {
                        foreach(Stack<LGSPMatch> match in matchesList) {
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
                    goto label5;
                }
label3: ;
label4: ;
label5: ;
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case Blowball_alt_0_further 
            do {
                patternGraph = patternGraphs[(int)Pattern_Blowball.Blowball_alt_0_CaseNums.@further];
                // SubPreset Blowball_node_head 
                LGSPNode candidate_Blowball_node_head = Blowball_node_head;
                // Extend Outgoing Blowball_alt_0_further_edge__edge0 from Blowball_node_head 
                LGSPEdge head_candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_node_head.outhead;
                if(head_candidate_Blowball_alt_0_further_edge__edge0 != null)
                {
                    LGSPEdge candidate_Blowball_alt_0_further_edge__edge0 = head_candidate_Blowball_alt_0_further_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_Blowball_alt_0_further_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target Blowball_alt_0_further_node__node0 from Blowball_alt_0_further_edge__edge0 
                        LGSPNode candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_Blowball_alt_0_further_node__node0.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_Blowball_alt_0_further_node__node0))
                            && candidate_Blowball_alt_0_further_node__node0==candidate_Blowball_node_head
                            )
                        {
                            continue;
                        }
                        if((candidate_Blowball_alt_0_further_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_Blowball taskFor__subpattern0 = PatternAction_Blowball.getNewTask(graph, openTasks);
                        taskFor__subpattern0.Blowball_node_head = candidate_Blowball_node_head;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                        prevGlobal__candidate_Blowball_alt_0_further_node__node0 = candidate_Blowball_alt_0_further_node__node0.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_node__node0.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                        prevGlobal__candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_Blowball_alt_0_further_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_Blowball.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_further_NodeNums.@head] = candidate_Blowball_node_head;
                                match.Nodes[(int)Pattern_Blowball.Blowball_alt_0_further_NodeNums.@_node0] = candidate_Blowball_alt_0_further_node__node0;
                                match.Edges[(int)Pattern_Blowball.Blowball_alt_0_further_EdgeNums.@_edge0] = candidate_Blowball_alt_0_further_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_Blowball.Blowball_alt_0_further_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_Blowball_alt_0_further_edge__edge0.flags = candidate_Blowball_alt_0_further_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                                candidate_Blowball_alt_0_further_node__node0.flags = candidate_Blowball_alt_0_further_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_Blowball_alt_0_further_edge__edge0.flags = candidate_Blowball_alt_0_further_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                            candidate_Blowball_alt_0_further_node__node0.flags = candidate_Blowball_alt_0_further_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                            continue;
                        }
                        candidate_Blowball_alt_0_further_node__node0.flags = candidate_Blowball_alt_0_further_node__node0.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_node__node0;
                        candidate_Blowball_alt_0_further_edge__edge0.flags = candidate_Blowball_alt_0_further_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_Blowball_alt_0_further_edge__edge0;
                    }
                    while( (candidate_Blowball_alt_0_further_edge__edge0 = candidate_Blowball_alt_0_further_edge__edge0.outNext) != head_candidate_Blowball_alt_0_further_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ReverseChainFromTo : LGSPSubpatternAction
    {
        private PatternAction_ReverseChainFromTo(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ReverseChainFromTo.Instance.patternGraph;
        }

        public static PatternAction_ReverseChainFromTo getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_ReverseChainFromTo newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ReverseChainFromTo(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_ReverseChainFromTo oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ReverseChainFromTo freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ReverseChainFromTo next = null;

        public LGSPNode ReverseChainFromTo_node_from;
        public LGSPNode ReverseChainFromTo_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ReverseChainFromTo_node_from 
            LGSPNode candidate_ReverseChainFromTo_node_from = ReverseChainFromTo_node_from;
            // SubPreset ReverseChainFromTo_node_to 
            LGSPNode candidate_ReverseChainFromTo_node_to = ReverseChainFromTo_node_to;
            // Push alternative matching task for ReverseChainFromTo_alt_0
            AlternativeAction_ReverseChainFromTo_alt_0 taskFor_alt_0 = AlternativeAction_ReverseChainFromTo_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ReverseChainFromTo_node_to = candidate_ReverseChainFromTo_node_to;
            taskFor_alt_0.ReverseChainFromTo_node_from = candidate_ReverseChainFromTo_node_from;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_ReverseChainFromTo_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_NodeNums.@from] = candidate_ReverseChainFromTo_node_from;
                    match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_NodeNums.@to] = candidate_ReverseChainFromTo_node_to;
                    match.EmbeddedGraphs[((int)Pattern_ReverseChainFromTo.ReverseChainFromTo_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_ReverseChainFromTo_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_ReverseChainFromTo_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ReverseChainFromTo_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_ReverseChainFromTo_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ReverseChainFromTo_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_ReverseChainFromTo_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ReverseChainFromTo_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ReverseChainFromTo_alt_0 next = null;

        public LGSPNode ReverseChainFromTo_node_to;
        public LGSPNode ReverseChainFromTo_node_from;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ReverseChainFromTo_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_CaseNums.@base];
                // SubPreset ReverseChainFromTo_node_to 
                LGSPNode candidate_ReverseChainFromTo_node_to = ReverseChainFromTo_node_to;
                // SubPreset ReverseChainFromTo_node_from 
                LGSPNode candidate_ReverseChainFromTo_node_from = ReverseChainFromTo_node_from;
                // Extend Outgoing ReverseChainFromTo_alt_0_base_edge__edge0 from ReverseChainFromTo_node_to 
                LGSPEdge head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_node_to.outhead;
                if(head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 != null)
                {
                    LGSPEdge candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ReverseChainFromTo_alt_0_base_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(candidate_ReverseChainFromTo_alt_0_base_edge__edge0.target != candidate_ReverseChainFromTo_node_from) {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_base_NodeNums.@to] = candidate_ReverseChainFromTo_node_to;
                            match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_base_NodeNums.@from] = candidate_ReverseChainFromTo_node_from;
                            match.Edges[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_base_EdgeNums.@_edge0] = candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_base_NodeNums.@to] = candidate_ReverseChainFromTo_node_to;
                                match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_base_NodeNums.@from] = candidate_ReverseChainFromTo_node_from;
                                match.Edges[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_base_EdgeNums.@_edge0] = candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromTo_alt_0_base_edge__edge0 = candidate_ReverseChainFromTo_alt_0_base_edge__edge0.outNext) != head_candidate_ReverseChainFromTo_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case ReverseChainFromTo_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_CaseNums.@rec];
                // SubPreset ReverseChainFromTo_node_from 
                LGSPNode candidate_ReverseChainFromTo_node_from = ReverseChainFromTo_node_from;
                // SubPreset ReverseChainFromTo_node_to 
                LGSPNode candidate_ReverseChainFromTo_node_to = ReverseChainFromTo_node_to;
                // Extend Incoming ReverseChainFromTo_alt_0_rec_edge__edge0 from ReverseChainFromTo_node_from 
                LGSPEdge head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_node_from.inhead;
                if(head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Source ReverseChainFromTo_alt_0_rec_node_intermediate from ReverseChainFromTo_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ReverseChainFromTo_alt_0_rec_node_intermediate = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.source;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ReverseChainFromTo_alt_0_rec_node_intermediate))
                            && candidate_ReverseChainFromTo_alt_0_rec_node_intermediate==candidate_ReverseChainFromTo_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for _subpattern0
                        PatternAction_ReverseChainFromTo taskFor__subpattern0 = PatternAction_ReverseChainFromTo.getNewTask(graph, openTasks);
                        taskFor__subpattern0.ReverseChainFromTo_node_from = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        taskFor__subpattern0.ReverseChainFromTo_node_to = candidate_ReverseChainFromTo_node_to;
                        openTasks.Push(taskFor__subpattern0);
                        uint prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for _subpattern0
                        openTasks.Pop();
                        PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[1], new object[0], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_rec_NodeNums.@intermediate] = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                                match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_rec_NodeNums.@from] = candidate_ReverseChainFromTo_node_from;
                                match.Nodes[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_rec_NodeNums.@to] = candidate_ReverseChainFromTo_node_to;
                                match.Edges[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_rec_EdgeNums.@_edge0] = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ReverseChainFromTo.ReverseChainFromTo_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                                candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                            candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromTo_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_node_intermediate;
                        candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromTo_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 = candidate_ReverseChainFromTo_alt_0_rec_edge__edge0.inNext) != head_candidate_ReverseChainFromTo_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromToReverse : LGSPSubpatternAction
    {
        private PatternAction_ChainFromToReverse(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromToReverse.Instance.patternGraph;
        }

        public static PatternAction_ChainFromToReverse getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromToReverse newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromToReverse(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromToReverse oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromToReverse freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromToReverse next = null;

        public LGSPNode ChainFromToReverse_node_from;
        public LGSPNode ChainFromToReverse_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromToReverse_node_from 
            LGSPNode candidate_ChainFromToReverse_node_from = ChainFromToReverse_node_from;
            // SubPreset ChainFromToReverse_node_to 
            LGSPNode candidate_ChainFromToReverse_node_to = ChainFromToReverse_node_to;
            // Push alternative matching task for ChainFromToReverse_alt_0
            AlternativeAction_ChainFromToReverse_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromToReverse_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFromToReverse.ChainFromToReverse_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromToReverse_node_from = candidate_ChainFromToReverse_node_from;
            taskFor_alt_0.ChainFromToReverse_node_to = candidate_ChainFromToReverse_node_to;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromToReverse_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_NodeNums.@from] = candidate_ChainFromToReverse_node_from;
                    match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_NodeNums.@to] = candidate_ChainFromToReverse_node_to;
                    match.EmbeddedGraphs[((int)Pattern_ChainFromToReverse.ChainFromToReverse_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_ChainFromToReverse_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromToReverse_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromToReverse_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromToReverse_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromToReverse_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromToReverse_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromToReverse_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromToReverse_alt_0 next = null;

        public LGSPNode ChainFromToReverse_node_from;
        public LGSPNode ChainFromToReverse_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromToReverse_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_CaseNums.@base];
                // SubPreset ChainFromToReverse_node_from 
                LGSPNode candidate_ChainFromToReverse_node_from = ChainFromToReverse_node_from;
                // SubPreset ChainFromToReverse_node_to 
                LGSPNode candidate_ChainFromToReverse_node_to = ChainFromToReverse_node_to;
                // Extend Outgoing ChainFromToReverse_alt_0_base_edge__edge0 from ChainFromToReverse_node_from 
                LGSPEdge head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_node_from.outhead;
                if(head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromToReverse_alt_0_base_edge__edge0 = head_candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromToReverse_alt_0_base_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(candidate_ChainFromToReverse_alt_0_base_edge__edge0.target != candidate_ChainFromToReverse_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_base_NodeNums.@from] = candidate_ChainFromToReverse_node_from;
                            match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_base_NodeNums.@to] = candidate_ChainFromToReverse_node_to;
                            match.Edges[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_base_NodeNums.@from] = candidate_ChainFromToReverse_node_from;
                                match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_base_NodeNums.@to] = candidate_ChainFromToReverse_node_to;
                                match.Edges[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverse_alt_0_base_edge__edge0 = candidate_ChainFromToReverse_alt_0_base_edge__edge0.outNext) != head_candidate_ChainFromToReverse_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case ChainFromToReverse_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_CaseNums.@rec];
                // SubPreset ChainFromToReverse_node_from 
                LGSPNode candidate_ChainFromToReverse_node_from = ChainFromToReverse_node_from;
                // SubPreset ChainFromToReverse_node_to 
                LGSPNode candidate_ChainFromToReverse_node_to = ChainFromToReverse_node_to;
                // Extend Outgoing ChainFromToReverse_alt_0_rec_edge__edge0 from ChainFromToReverse_node_from 
                LGSPEdge head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_node_from.outhead;
                if(head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromToReverse_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromToReverse_alt_0_rec_node_intermediate from ChainFromToReverse_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFromToReverse_alt_0_rec_node_intermediate = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromToReverse_alt_0_rec_node_intermediate))
                            && candidate_ChainFromToReverse_alt_0_rec_node_intermediate==candidate_ChainFromToReverse_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for cftr
                        PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(graph, openTasks);
                        taskFor_cftr.ChainFromToReverse_node_from = candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        taskFor_cftr.ChainFromToReverse_node_to = candidate_ChainFromToReverse_node_to;
                        openTasks.Push(taskFor_cftr);
                        uint prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for cftr
                        openTasks.Pop();
                        PatternAction_ChainFromToReverse.releaseTask(taskFor_cftr);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[1], new object[0], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_rec_NodeNums.@from] = candidate_ChainFromToReverse_node_from;
                                match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_rec_NodeNums.@intermediate] = candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                                match.Nodes[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_rec_NodeNums.@to] = candidate_ChainFromToReverse_node_to;
                                match.Edges[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFromToReverse.ChainFromToReverse_alt_0_rec_SubNums.@cftr] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                                candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                            candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverse_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_node_intermediate;
                        candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverse_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverse_alt_0_rec_edge__edge0 = candidate_ChainFromToReverse_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromToReverse_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ChainFromToReverseToCommon : LGSPSubpatternAction
    {
        private PatternAction_ChainFromToReverseToCommon(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ChainFromToReverseToCommon.Instance.patternGraph;
        }

        public static PatternAction_ChainFromToReverseToCommon getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_ChainFromToReverseToCommon newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ChainFromToReverseToCommon(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_ChainFromToReverseToCommon oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ChainFromToReverseToCommon freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ChainFromToReverseToCommon next = null;

        public LGSPNode ChainFromToReverseToCommon_node_from;
        public LGSPNode ChainFromToReverseToCommon_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ChainFromToReverseToCommon_node_from 
            LGSPNode candidate_ChainFromToReverseToCommon_node_from = ChainFromToReverseToCommon_node_from;
            // SubPreset ChainFromToReverseToCommon_node_to 
            LGSPNode candidate_ChainFromToReverseToCommon_node_to = ChainFromToReverseToCommon_node_to;
            // Push alternative matching task for ChainFromToReverseToCommon_alt_0
            AlternativeAction_ChainFromToReverseToCommon_alt_0 taskFor_alt_0 = AlternativeAction_ChainFromToReverseToCommon_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ChainFromToReverseToCommon_node_from = candidate_ChainFromToReverseToCommon_node_from;
            taskFor_alt_0.ChainFromToReverseToCommon_node_to = candidate_ChainFromToReverseToCommon_node_to;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_ChainFromToReverseToCommon_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_NodeNums.@from] = candidate_ChainFromToReverseToCommon_node_from;
                    match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_NodeNums.@to] = candidate_ChainFromToReverseToCommon_node_to;
                    match.EmbeddedGraphs[((int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_ChainFromToReverseToCommon_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_ChainFromToReverseToCommon_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ChainFromToReverseToCommon_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_ChainFromToReverseToCommon_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ChainFromToReverseToCommon_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_ChainFromToReverseToCommon_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ChainFromToReverseToCommon_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ChainFromToReverseToCommon_alt_0 next = null;

        public LGSPNode ChainFromToReverseToCommon_node_from;
        public LGSPNode ChainFromToReverseToCommon_node_to;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ChainFromToReverseToCommon_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_CaseNums.@base];
                // SubPreset ChainFromToReverseToCommon_node_from 
                LGSPNode candidate_ChainFromToReverseToCommon_node_from = ChainFromToReverseToCommon_node_from;
                // SubPreset ChainFromToReverseToCommon_node_to 
                LGSPNode candidate_ChainFromToReverseToCommon_node_to = ChainFromToReverseToCommon_node_to;
                // Extend Outgoing ChainFromToReverseToCommon_alt_0_base_edge__edge0 from ChainFromToReverseToCommon_node_from 
                LGSPEdge head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_node_from.outhead;
                if(head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.target != candidate_ChainFromToReverseToCommon_node_to) {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Check whether there are subpattern matching tasks left to execute
                        if(openTasks.Count==0)
                        {
                            Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                            foundPartialMatches.Add(currentFoundPartialMatch);
                            LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0]);
                            match.patternGraph = patternGraph;
                            match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_base_NodeNums.@from] = candidate_ChainFromToReverseToCommon_node_from;
                            match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_base_NodeNums.@to] = candidate_ChainFromToReverseToCommon_node_to;
                            match.Edges[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                            currentFoundPartialMatch.Push(match);
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                openTasks.Push(this);
                                return;
                            }
                            continue;
                        }
                        uint prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[2], new LGSPEdge[1], new object[0], new LGSPMatch[0+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_base_NodeNums.@from] = candidate_ChainFromToReverseToCommon_node_from;
                                match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_base_NodeNums.@to] = candidate_ChainFromToReverseToCommon_node_to;
                                match.Edges[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_base_EdgeNums.@_edge0] = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                            continue;
                        }
                        candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0.outNext) != head_candidate_ChainFromToReverseToCommon_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case ChainFromToReverseToCommon_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_CaseNums.@rec];
                // SubPreset ChainFromToReverseToCommon_node_from 
                LGSPNode candidate_ChainFromToReverseToCommon_node_from = ChainFromToReverseToCommon_node_from;
                // SubPreset ChainFromToReverseToCommon_node_to 
                LGSPNode candidate_ChainFromToReverseToCommon_node_to = ChainFromToReverseToCommon_node_to;
                // Extend Outgoing ChainFromToReverseToCommon_alt_0_rec_edge__edge0 from ChainFromToReverseToCommon_node_from 
                LGSPEdge head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_node_from.outhead;
                if(head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Implicit Target ChainFromToReverseToCommon_alt_0_rec_node_intermediate from ChainFromToReverseToCommon_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.target;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate))
                            && candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate==candidate_ChainFromToReverseToCommon_node_from
                            )
                        {
                            continue;
                        }
                        if((candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        // Push subpattern matching task for cftrtc
                        PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(graph, openTasks);
                        taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_ChainFromToReverseToCommon_node_to;
                        openTasks.Push(taskFor_cftrtc);
                        uint prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        uint prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                        prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                        // Match subpatterns 
                        openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                        // Pop subpattern matching task for cftrtc
                        openTasks.Pop();
                        PatternAction_ChainFromToReverseToCommon.releaseTask(taskFor_cftrtc);
                        // Check whether subpatterns were found 
                        if(matchesList.Count>0) {
                            // subpatterns/alternatives were found, extend the partial matches by our local match object
                            foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                            {
                                LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[1], new object[0], new LGSPMatch[1+0]);
                                match.patternGraph = patternGraph;
                                match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_rec_NodeNums.@from] = candidate_ChainFromToReverseToCommon_node_from;
                                match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_rec_NodeNums.@intermediate] = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                                match.Nodes[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_rec_NodeNums.@to] = candidate_ChainFromToReverseToCommon_node_to;
                                match.Edges[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_rec_EdgeNums.@_edge0] = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                                match.EmbeddedGraphs[(int)Pattern_ChainFromToReverseToCommon.ChainFromToReverseToCommon_alt_0_rec_SubNums.@cftrtc] = currentFoundPartialMatch.Pop();
                                currentFoundPartialMatch.Push(match);
                            }
                            if(matchesList==foundPartialMatches) {
                                matchesList = new List<Stack<LGSPMatch>>();
                            } else {
                                foreach(Stack<LGSPMatch> match in matchesList) {
                                    foundPartialMatches.Add(match);
                                }
                                matchesList.Clear();
                            }
                            // if enough matches were found, we leave
                            if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                            {
                                candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                                candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                                openTasks.Push(this);
                                return;
                            }
                            candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                            candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                            continue;
                        }
                        candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_node_intermediate;
                        candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0;
                    }
                    while( (candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 = candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0.outNext) != head_candidate_ChainFromToReverseToCommon_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class PatternAction_ReverseChainFromToToCommon : LGSPSubpatternAction
    {
        private PatternAction_ReverseChainFromToToCommon(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            graph = graph_; openTasks = openTasks_;
            patternGraph = Pattern_ReverseChainFromToToCommon.Instance.patternGraph;
        }

        public static PatternAction_ReverseChainFromToToCommon getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_) {
            PatternAction_ReverseChainFromToToCommon newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new PatternAction_ReverseChainFromToToCommon(graph_, openTasks_);
            }
        return newTask;
        }

        public static void releaseTask(PatternAction_ReverseChainFromToToCommon oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static PatternAction_ReverseChainFromToToCommon freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private PatternAction_ReverseChainFromToToCommon next = null;

        public LGSPNode ReverseChainFromToToCommon_node_from;
        public LGSPNode ReverseChainFromToToCommon_node_to;
        public LGSPNode ReverseChainFromToToCommon_node_common;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // SubPreset ReverseChainFromToToCommon_node_from 
            LGSPNode candidate_ReverseChainFromToToCommon_node_from = ReverseChainFromToToCommon_node_from;
            // SubPreset ReverseChainFromToToCommon_node_to 
            LGSPNode candidate_ReverseChainFromToToCommon_node_to = ReverseChainFromToToCommon_node_to;
            // SubPreset ReverseChainFromToToCommon_node_common 
            LGSPNode candidate_ReverseChainFromToToCommon_node_common = ReverseChainFromToToCommon_node_common;
            // Push alternative matching task for ReverseChainFromToToCommon_alt_0
            AlternativeAction_ReverseChainFromToToCommon_alt_0 taskFor_alt_0 = AlternativeAction_ReverseChainFromToToCommon_alt_0.getNewTask(graph, openTasks, patternGraph.alternatives[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_AltNums.@alt_0].alternativeCases);
            taskFor_alt_0.ReverseChainFromToToCommon_node_to = candidate_ReverseChainFromToToCommon_node_to;
            taskFor_alt_0.ReverseChainFromToToCommon_node_from = candidate_ReverseChainFromToToCommon_node_from;
            taskFor_alt_0.ReverseChainFromToToCommon_node_common = candidate_ReverseChainFromToToCommon_node_common;
            openTasks.Push(taskFor_alt_0);
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for alt_0
            openTasks.Pop();
            AlternativeAction_ReverseChainFromToToCommon_alt_0.releaseTask(taskFor_alt_0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[0], new object[0], new LGSPMatch[0+1]);
                    match.patternGraph = patternGraph;
                    match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_NodeNums.@from] = candidate_ReverseChainFromToToCommon_node_from;
                    match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_NodeNums.@to] = candidate_ReverseChainFromToToCommon_node_to;
                    match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_NodeNums.@common] = candidate_ReverseChainFromToToCommon_node_common;
                    match.EmbeddedGraphs[((int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_AltNums.@alt_0)+0] = currentFoundPartialMatch.Pop();
                    currentFoundPartialMatch.Push(match);
                }
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
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

    public class AlternativeAction_ReverseChainFromToToCommon_alt_0 : LGSPSubpatternAction
    {
        private AlternativeAction_ReverseChainFromToToCommon_alt_0(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            graph = graph_; openTasks = openTasks_;
            patternGraphs = patternGraphs_;
        }

        public static AlternativeAction_ReverseChainFromToToCommon_alt_0 getNewTask(LGSPGraph graph_, Stack<LGSPSubpatternAction> openTasks_, PatternGraph[] patternGraphs_) {
            AlternativeAction_ReverseChainFromToToCommon_alt_0 newTask;
            if(numFreeTasks>0) {
                newTask = freeListHead;
                newTask.graph = graph_; newTask.openTasks = openTasks_;
                newTask.patternGraphs = patternGraphs_;
                freeListHead = newTask.next;
                newTask.next = null;
                --numFreeTasks;
            } else {
                newTask = new AlternativeAction_ReverseChainFromToToCommon_alt_0(graph_, openTasks_, patternGraphs_);
            }
        return newTask;
        }

        public static void releaseTask(AlternativeAction_ReverseChainFromToToCommon_alt_0 oldTask) {
            if(numFreeTasks<MAX_NUM_FREE_TASKS) {
                oldTask.next = freeListHead;
                oldTask.graph = null; oldTask.openTasks = null;
                freeListHead = oldTask;
                ++numFreeTasks;
            }
        }

        private static AlternativeAction_ReverseChainFromToToCommon_alt_0 freeListHead = null;
        private static int numFreeTasks = 0;
        private const int MAX_NUM_FREE_TASKS = 100;

        private AlternativeAction_ReverseChainFromToToCommon_alt_0 next = null;

        public LGSPNode ReverseChainFromToToCommon_node_to;
        public LGSPNode ReverseChainFromToToCommon_node_from;
        public LGSPNode ReverseChainFromToToCommon_node_common;
        
        public override void myMatch(List<Stack<LGSPMatch>> foundPartialMatches, int maxMatches, int negLevel)
        {
            const int MAX_NEG_LEVEL = 5;
            openTasks.Pop();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            if(matchesList.Count!=0) throw new ApplicationException(); //debug assert
            // Alternative case ReverseChainFromToToCommon_alt_0_base 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_CaseNums.@base];
                // SubPreset ReverseChainFromToToCommon_node_to 
                LGSPNode candidate_ReverseChainFromToToCommon_node_to = ReverseChainFromToToCommon_node_to;
                // SubPreset ReverseChainFromToToCommon_node_from 
                LGSPNode candidate_ReverseChainFromToToCommon_node_from = ReverseChainFromToToCommon_node_from;
                // SubPreset ReverseChainFromToToCommon_node_common 
                LGSPNode candidate_ReverseChainFromToToCommon_node_common = ReverseChainFromToToCommon_node_common;
                // Extend Outgoing ReverseChainFromToToCommon_alt_0_base_edge__edge0 from ReverseChainFromToToCommon_node_to 
                LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_node_to.outhead;
                if(head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 != null)
                {
                    LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.target != candidate_ReverseChainFromToToCommon_node_from) {
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0,candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                        }
                        // Extend Outgoing ReverseChainFromToToCommon_alt_0_base_edge__edge1 from ReverseChainFromToToCommon_node_from 
                        LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_node_from.outhead;
                        if(head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 != null)
                        {
                            LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                if(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.target != candidate_ReverseChainFromToToCommon_node_common) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1))
                                    && candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1==candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Check whether there are subpattern matching tasks left to execute
                                if(openTasks.Count==0)
                                {
                                    Stack<LGSPMatch> currentFoundPartialMatch = new Stack<LGSPMatch>();
                                    foundPartialMatches.Add(currentFoundPartialMatch);
                                    LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[2], new object[0], new LGSPMatch[0]);
                                    match.patternGraph = patternGraph;
                                    match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_NodeNums.@to] = candidate_ReverseChainFromToToCommon_node_to;
                                    match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_NodeNums.@from] = candidate_ReverseChainFromToToCommon_node_from;
                                    match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_NodeNums.@common] = candidate_ReverseChainFromToToCommon_node_common;
                                    match.Edges[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge0] = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                    match.Edges[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge1] = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                    currentFoundPartialMatch.Push(match);
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        } else { 
                                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    continue;
                                }
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        LGSPMatch match = new LGSPMatch(new LGSPNode[3], new LGSPEdge[2], new object[0], new LGSPMatch[0+0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_NodeNums.@to] = candidate_ReverseChainFromToToCommon_node_to;
                                        match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_NodeNums.@from] = candidate_ReverseChainFromToToCommon_node_from;
                                        match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_NodeNums.@common] = candidate_ReverseChainFromToToCommon_node_common;
                                        match.Edges[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge0] = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        match.Edges[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_base_EdgeNums.@_edge1] = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<LGSPMatch>>();
                                    } else {
                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                        candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                        } else { 
                                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                                    candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                    continue;
                                }
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                                candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1;
                            }
                            while( (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1.outNext) != head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge1 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0;
                        } else { 
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0.outNext) != head_candidate_ReverseChainFromToToCommon_alt_0_base_edge__edge0 );
                }
            } while(false);
            if(matchesList.Count>0) {
                if(matchesList==foundPartialMatches) {
                    matchesList = new List<Stack<LGSPMatch>>();
                } else {
                    foreach(Stack<LGSPMatch> match in matchesList) {
                        foundPartialMatches.Add(match);
                    }
                    matchesList.Clear();
                }
            }
            // Alternative case ReverseChainFromToToCommon_alt_0_rec 
            do {
                patternGraph = patternGraphs[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_CaseNums.@rec];
                // SubPreset ReverseChainFromToToCommon_node_from 
                LGSPNode candidate_ReverseChainFromToToCommon_node_from = ReverseChainFromToToCommon_node_from;
                // SubPreset ReverseChainFromToToCommon_node_common 
                LGSPNode candidate_ReverseChainFromToToCommon_node_common = ReverseChainFromToToCommon_node_common;
                // SubPreset ReverseChainFromToToCommon_node_to 
                LGSPNode candidate_ReverseChainFromToToCommon_node_to = ReverseChainFromToToCommon_node_to;
                // Extend Incoming ReverseChainFromToToCommon_alt_0_rec_edge__edge0 from ReverseChainFromToToCommon_node_from 
                LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_node_from.inhead;
                if(head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 != null)
                {
                    LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                    do
                    {
                        if(!EdgeType_Edge.isMyType[candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.type.TypeID]) {
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            continue;
                        }
                        uint prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED<<negLevel;
                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED<<negLevel;
                        } else {
                            prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0) ? 1U : 0U;
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Add(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0,candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                        }
                        // Implicit Source ReverseChainFromToToCommon_alt_0_rec_node_intermediate from ReverseChainFromToToCommon_alt_0_rec_edge__edge0 
                        LGSPNode candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.source;
                        if((negLevel<=MAX_NEG_LEVEL ? (candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate))
                            && (candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate==candidate_ReverseChainFromToToCommon_node_from
                                || candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate==candidate_ReverseChainFromToToCommon_node_common
                                )
                            )
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                            } else { 
                                if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                                }
                            }
                            continue;
                        }
                        if((candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN)
                        {
                            if(negLevel <= MAX_NEG_LEVEL) {
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                            } else { 
                                if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0==0) {
                                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                                }
                            }
                            continue;
                        }
                        // Extend Outgoing ReverseChainFromToToCommon_alt_0_rec_edge__edge1 from ReverseChainFromToToCommon_node_from 
                        LGSPEdge head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_node_from.outhead;
                        if(head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 != null)
                        {
                            LGSPEdge candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                            do
                            {
                                if(!EdgeType_Edge.isMyType[candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.type.TypeID]) {
                                    continue;
                                }
                                if(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.target != candidate_ReverseChainFromToToCommon_node_common) {
                                    continue;
                                }
                                if((negLevel<=MAX_NEG_LEVEL ? (candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & LGSPEdge.IS_MATCHED<<negLevel) == LGSPEdge.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.ContainsKey(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1))
                                    && candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1==candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0
                                    )
                                {
                                    continue;
                                }
                                if((candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)==LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN)
                                {
                                    continue;
                                }
                                // Push subpattern matching task for _subpattern0
                                PatternAction_ReverseChainFromToToCommon taskFor__subpattern0 = PatternAction_ReverseChainFromToToCommon.getNewTask(graph, openTasks);
                                taskFor__subpattern0.ReverseChainFromToToCommon_node_from = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                taskFor__subpattern0.ReverseChainFromToToCommon_node_to = candidate_ReverseChainFromToToCommon_node_to;
                                taskFor__subpattern0.ReverseChainFromToToCommon_node_common = candidate_ReverseChainFromToToCommon_node_common;
                                openTasks.Push(taskFor__subpattern0);
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                uint prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags |= LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN;
                                // Match subpatterns 
                                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                                // Pop subpattern matching task for _subpattern0
                                openTasks.Pop();
                                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
                                // Check whether subpatterns were found 
                                if(matchesList.Count>0) {
                                    // subpatterns/alternatives were found, extend the partial matches by our local match object
                                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                                    {
                                        LGSPMatch match = new LGSPMatch(new LGSPNode[4], new LGSPEdge[2], new object[0], new LGSPMatch[1+0]);
                                        match.patternGraph = patternGraph;
                                        match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_rec_NodeNums.@intermediate] = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                        match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_rec_NodeNums.@from] = candidate_ReverseChainFromToToCommon_node_from;
                                        match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_rec_NodeNums.@common] = candidate_ReverseChainFromToToCommon_node_common;
                                        match.Nodes[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_rec_NodeNums.@to] = candidate_ReverseChainFromToToCommon_node_to;
                                        match.Edges[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_rec_EdgeNums.@_edge0] = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        match.Edges[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_rec_EdgeNums.@_edge1] = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                        match.EmbeddedGraphs[(int)Pattern_ReverseChainFromToToCommon.ReverseChainFromToToCommon_alt_0_rec_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                                        currentFoundPartialMatch.Push(match);
                                    }
                                    if(matchesList==foundPartialMatches) {
                                        matchesList = new List<Stack<LGSPMatch>>();
                                    } else {
                                        foreach(Stack<LGSPMatch> match in matchesList) {
                                            foundPartialMatches.Add(match);
                                        }
                                        matchesList.Clear();
                                    }
                                    // if enough matches were found, we leave
                                    if(maxMatches > 0 && foundPartialMatches.Count >= maxMatches)
                                    {
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                        if(negLevel <= MAX_NEG_LEVEL) {
                                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                        } else { 
                                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0==0) {
                                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                                            }
                                        }
                                        openTasks.Push(this);
                                        return;
                                    }
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                    candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                    continue;
                                }
                                candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_node_intermediate;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                                candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.flags & ~(LGSPEdge.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1;
                            }
                            while( (candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1.outNext) != head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge1 );
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.flags & ~(LGSPEdge.IS_MATCHED<<negLevel) | prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0;
                        } else { 
                            if(prev__candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].snd.Remove(candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0);
                            }
                        }
                    }
                    while( (candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 = candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0.inNext) != head_candidate_ReverseChainFromToToCommon_alt_0_rec_edge__edge0 );
                }
            } while(false);
            openTasks.Push(this);
            return;
        }
    }

    public class Action_createChain : LGSPAction
    {
        public Action_createChain() {
            rulePattern = Rule_createChain.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 0, 0, 0, 0 + 0);
        }

        public override string Name { get { return "createChain"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createChain instance = new Action_createChain();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_chainFromTo : LGSPAction
    {
        public Action_chainFromTo() {
            rulePattern = Rule_chainFromTo.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 2, 0, 0, 1 + 0);
        }

        public override string Name { get { return "chainFromTo"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFromTo instance = new Action_chainFromTo();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFromTo_node_beg 
            LGSPNode candidate_chainFromTo_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFromTo_node_beg == null) {
                MissingPreset_chainFromTo_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_chainFromTo_node_beg;
            if(negLevel <= MAX_NEG_LEVEL) {
                prev__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
            } else {
                prev__candidate_chainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_beg) ? 1U : 0U;
                if(prev__candidate_chainFromTo_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromTo_node_beg,candidate_chainFromTo_node_beg);
            }
            // Preset chainFromTo_node_end 
            LGSPNode candidate_chainFromTo_node_end = (LGSPNode) parameters[1];
            if(candidate_chainFromTo_node_end == null) {
                MissingPreset_chainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromTo_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end))
                && candidate_chainFromTo_node_end==candidate_chainFromTo_node_beg
                )
            {
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ChainFromTo taskFor__subpattern0 = PatternAction_ChainFromTo.getNewTask(graph, openTasks);
            taskFor__subpattern0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
            taskFor__subpattern0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_chainFromTo_node_beg;
            prevGlobal__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_chainFromTo_node_end;
            prevGlobal__candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@beg] = candidate_chainFromTo_node_beg;
                    match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@end] = candidate_chainFromTo_node_end;
                    match.EmbeddedGraphs[(int)Rule_chainFromTo.chainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
                return matches;
            }
            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
            candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
            if(negLevel <= MAX_NEG_LEVEL) {
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
            } else { 
                if(prev__candidate_chainFromTo_node_beg==0) {
                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_chainFromTo_node_beg(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromTo_node_beg 
            int type_id_candidate_chainFromTo_node_beg = 0;
            for(LGSPNode head_candidate_chainFromTo_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromTo_node_beg], candidate_chainFromTo_node_beg = head_candidate_chainFromTo_node_beg.typeNext; candidate_chainFromTo_node_beg != head_candidate_chainFromTo_node_beg; candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.typeNext)
            {
                uint prev__candidate_chainFromTo_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_chainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_beg) ? 1U : 0U;
                    if(prev__candidate_chainFromTo_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromTo_node_beg,candidate_chainFromTo_node_beg);
                }
                // Preset chainFromTo_node_end 
                LGSPNode candidate_chainFromTo_node_end = (LGSPNode) parameters[1];
                if(candidate_chainFromTo_node_end == null) {
                    MissingPreset_chainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromTo_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_chainFromTo_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end))
                    && candidate_chainFromTo_node_end==candidate_chainFromTo_node_beg
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromTo taskFor__subpattern0 = PatternAction_ChainFromTo.getNewTask(graph, openTasks);
                taskFor__subpattern0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
                taskFor__subpattern0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_chainFromTo_node_beg;
                prevGlobal__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromTo_node_end;
                prevGlobal__candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@beg] = candidate_chainFromTo_node_beg;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@end] = candidate_chainFromTo_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromTo.chainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_chainFromTo_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_chainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromTo_node_beg;
                } else { 
                    if(prev__candidate_chainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromTo_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_chainFromTo_node_end(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList, LGSPNode candidate_chainFromTo_node_beg)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromTo_node_end 
            int type_id_candidate_chainFromTo_node_end = 0;
            for(LGSPNode head_candidate_chainFromTo_node_end = graph.nodesByTypeHeads[type_id_candidate_chainFromTo_node_end], candidate_chainFromTo_node_end = head_candidate_chainFromTo_node_end.typeNext; candidate_chainFromTo_node_end != head_candidate_chainFromTo_node_end; candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.typeNext)
            {
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromTo_node_end))
                    && candidate_chainFromTo_node_end==candidate_chainFromTo_node_beg
                    )
                {
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromTo taskFor__subpattern0 = PatternAction_ChainFromTo.getNewTask(graph, openTasks);
                taskFor__subpattern0.ChainFromTo_node_from = candidate_chainFromTo_node_beg;
                taskFor__subpattern0.ChainFromTo_node_to = candidate_chainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_chainFromTo_node_beg;
                prevGlobal__candidate_chainFromTo_node_beg = candidate_chainFromTo_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromTo_node_end;
                prevGlobal__candidate_chainFromTo_node_end = candidate_chainFromTo_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@beg] = candidate_chainFromTo_node_beg;
                        match.Nodes[(int)Rule_chainFromTo.chainFromTo_NodeNums.@end] = candidate_chainFromTo_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromTo.chainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                        candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                        return;
                    }
                    candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
                    candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                    continue;
                }
                candidate_chainFromTo_node_beg.flags = candidate_chainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_beg;
                candidate_chainFromTo_node_end.flags = candidate_chainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromTo_node_end;
            }
            return;
        }
    }

    public class Action_chainFrom : LGSPAction
    {
        public Action_chainFrom() {
            rulePattern = Rule_chainFrom.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 1, 0, 0, 1 + 0);
        }

        public override string Name { get { return "chainFrom"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFrom instance = new Action_chainFrom();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFrom_node_beg 
            LGSPNode candidate_chainFrom_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFrom_node_beg == null) {
                MissingPreset_chainFrom_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ChainFrom taskFor__subpattern0 = PatternAction_ChainFrom.getNewTask(graph, openTasks);
            taskFor__subpattern0.ChainFrom_node_from = candidate_chainFrom_node_beg;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_chainFrom_node_beg;
            prevGlobal__candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFrom_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ChainFrom.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFrom.chainFrom_NodeNums.@beg] = candidate_chainFrom_node_beg;
                    match.EmbeddedGraphs[(int)Rule_chainFrom.chainFrom_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                    return matches;
                }
                candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                return matches;
            }
            candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
            return matches;
        }
        public void MissingPreset_chainFrom_node_beg(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFrom_node_beg 
            int type_id_candidate_chainFrom_node_beg = 0;
            for(LGSPNode head_candidate_chainFrom_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFrom_node_beg], candidate_chainFrom_node_beg = head_candidate_chainFrom_node_beg.typeNext; candidate_chainFrom_node_beg != head_candidate_chainFrom_node_beg; candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFrom taskFor__subpattern0 = PatternAction_ChainFrom.getNewTask(graph, openTasks);
                taskFor__subpattern0.ChainFrom_node_from = candidate_chainFrom_node_beg;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_chainFrom_node_beg;
                prevGlobal__candidate_chainFrom_node_beg = candidate_chainFrom_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFrom_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFrom.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFrom.chainFrom_NodeNums.@beg] = candidate_chainFrom_node_beg;
                        match.EmbeddedGraphs[(int)Rule_chainFrom.chainFrom_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                        return;
                    }
                    candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
                    continue;
                }
                candidate_chainFrom_node_beg.flags = candidate_chainFrom_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFrom_node_beg;
            }
            return;
        }
    }

    public class Action_chainFromComplete : LGSPAction
    {
        public Action_chainFromComplete() {
            rulePattern = Rule_chainFromComplete.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 1, 0, 0, 1 + 0);
        }

        public override string Name { get { return "chainFromComplete"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFromComplete instance = new Action_chainFromComplete();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFromComplete_node_beg 
            LGSPNode candidate_chainFromComplete_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFromComplete_node_beg == null) {
                MissingPreset_chainFromComplete_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ChainFromComplete taskFor__subpattern0 = PatternAction_ChainFromComplete.getNewTask(graph, openTasks);
            taskFor__subpattern0.ChainFromComplete_node_from = candidate_chainFromComplete_node_beg;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_chainFromComplete_node_beg;
            prevGlobal__candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromComplete_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ChainFromComplete.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFromComplete.chainFromComplete_NodeNums.@beg] = candidate_chainFromComplete_node_beg;
                    match.EmbeddedGraphs[(int)Rule_chainFromComplete.chainFromComplete_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                    return matches;
                }
                candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                return matches;
            }
            candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
            return matches;
        }
        public void MissingPreset_chainFromComplete_node_beg(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromComplete_node_beg 
            int type_id_candidate_chainFromComplete_node_beg = 0;
            for(LGSPNode head_candidate_chainFromComplete_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromComplete_node_beg], candidate_chainFromComplete_node_beg = head_candidate_chainFromComplete_node_beg.typeNext; candidate_chainFromComplete_node_beg != head_candidate_chainFromComplete_node_beg; candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_ChainFromComplete taskFor__subpattern0 = PatternAction_ChainFromComplete.getNewTask(graph, openTasks);
                taskFor__subpattern0.ChainFromComplete_node_from = candidate_chainFromComplete_node_beg;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_chainFromComplete_node_beg;
                prevGlobal__candidate_chainFromComplete_node_beg = candidate_chainFromComplete_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromComplete_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ChainFromComplete.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromComplete.chainFromComplete_NodeNums.@beg] = candidate_chainFromComplete_node_beg;
                        match.EmbeddedGraphs[(int)Rule_chainFromComplete.chainFromComplete_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                        return;
                    }
                    candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
                    continue;
                }
                candidate_chainFromComplete_node_beg.flags = candidate_chainFromComplete_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromComplete_node_beg;
            }
            return;
        }
    }

    public class Action_createBlowball : LGSPAction
    {
        public Action_createBlowball() {
            rulePattern = Rule_createBlowball.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 0, 0, 0, 0 + 0);
        }

        public override string Name { get { return "createBlowball"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createBlowball instance = new Action_createBlowball();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_blowball : LGSPAction
    {
        public Action_blowball() {
            rulePattern = Rule_blowball.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 1, 0, 0, 1 + 0);
        }

        public override string Name { get { return "blowball"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_blowball instance = new Action_blowball();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset blowball_node_head 
            LGSPNode candidate_blowball_node_head = (LGSPNode) parameters[0];
            if(candidate_blowball_node_head == null) {
                MissingPreset_blowball_node_head(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_Blowball taskFor__subpattern0 = PatternAction_Blowball.getNewTask(graph, openTasks);
            taskFor__subpattern0.Blowball_node_head = candidate_blowball_node_head;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_blowball_node_head;
            prevGlobal__candidate_blowball_node_head = candidate_blowball_node_head.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_blowball_node_head.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_Blowball.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_blowball.blowball_NodeNums.@head] = candidate_blowball_node_head;
                    match.EmbeddedGraphs[(int)Rule_blowball.blowball_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                    return matches;
                }
                candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                return matches;
            }
            candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
            return matches;
        }
        public void MissingPreset_blowball_node_head(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup blowball_node_head 
            int type_id_candidate_blowball_node_head = 0;
            for(LGSPNode head_candidate_blowball_node_head = graph.nodesByTypeHeads[type_id_candidate_blowball_node_head], candidate_blowball_node_head = head_candidate_blowball_node_head.typeNext; candidate_blowball_node_head != head_candidate_blowball_node_head; candidate_blowball_node_head = candidate_blowball_node_head.typeNext)
            {
                // Push subpattern matching task for _subpattern0
                PatternAction_Blowball taskFor__subpattern0 = PatternAction_Blowball.getNewTask(graph, openTasks);
                taskFor__subpattern0.Blowball_node_head = candidate_blowball_node_head;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_blowball_node_head;
                prevGlobal__candidate_blowball_node_head = candidate_blowball_node_head.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_blowball_node_head.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_Blowball.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_blowball.blowball_NodeNums.@head] = candidate_blowball_node_head;
                        match.EmbeddedGraphs[(int)Rule_blowball.blowball_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                        return;
                    }
                    candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
                    continue;
                }
                candidate_blowball_node_head.flags = candidate_blowball_node_head.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_blowball_node_head;
            }
            return;
        }
    }

    public class Action_reverseChainFromTo : LGSPAction
    {
        public Action_reverseChainFromTo() {
            rulePattern = Rule_reverseChainFromTo.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 2, 0, 0, 1 + 0);
        }

        public override string Name { get { return "reverseChainFromTo"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_reverseChainFromTo instance = new Action_reverseChainFromTo();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset reverseChainFromTo_node_beg 
            LGSPNode candidate_reverseChainFromTo_node_beg = (LGSPNode) parameters[0];
            if(candidate_reverseChainFromTo_node_beg == null) {
                MissingPreset_reverseChainFromTo_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_reverseChainFromTo_node_beg;
            if(negLevel <= MAX_NEG_LEVEL) {
                prev__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_reverseChainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
            } else {
                prev__candidate_reverseChainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_beg) ? 1U : 0U;
                if(prev__candidate_reverseChainFromTo_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_reverseChainFromTo_node_beg,candidate_reverseChainFromTo_node_beg);
            }
            // Preset reverseChainFromTo_node_end 
            LGSPNode candidate_reverseChainFromTo_node_end = (LGSPNode) parameters[1];
            if(candidate_reverseChainFromTo_node_end == null) {
                MissingPreset_reverseChainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromTo_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_end))
                && candidate_reverseChainFromTo_node_end==candidate_reverseChainFromTo_node_beg
                )
            {
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                    }
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ReverseChainFromTo taskFor__subpattern0 = PatternAction_ReverseChainFromTo.getNewTask(graph, openTasks);
            taskFor__subpattern0.ReverseChainFromTo_node_from = candidate_reverseChainFromTo_node_beg;
            taskFor__subpattern0.ReverseChainFromTo_node_to = candidate_reverseChainFromTo_node_end;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_reverseChainFromTo_node_beg;
            prevGlobal__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_reverseChainFromTo_node_end;
            prevGlobal__candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_reverseChainFromTo.reverseChainFromTo_NodeNums.@beg] = candidate_reverseChainFromTo_node_beg;
                    match.Nodes[(int)Rule_reverseChainFromTo.reverseChainFromTo_NodeNums.@end] = candidate_reverseChainFromTo_node_end;
                    match.EmbeddedGraphs[(int)Rule_reverseChainFromTo.reverseChainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                    }
                }
                return matches;
            }
            candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
            candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
            if(negLevel <= MAX_NEG_LEVEL) {
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
            } else { 
                if(prev__candidate_reverseChainFromTo_node_beg==0) {
                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_reverseChainFromTo_node_beg(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup reverseChainFromTo_node_beg 
            int type_id_candidate_reverseChainFromTo_node_beg = 0;
            for(LGSPNode head_candidate_reverseChainFromTo_node_beg = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromTo_node_beg], candidate_reverseChainFromTo_node_beg = head_candidate_reverseChainFromTo_node_beg.typeNext; candidate_reverseChainFromTo_node_beg != head_candidate_reverseChainFromTo_node_beg; candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.typeNext)
            {
                uint prev__candidate_reverseChainFromTo_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_reverseChainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_reverseChainFromTo_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_beg) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromTo_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_reverseChainFromTo_node_beg,candidate_reverseChainFromTo_node_beg);
                }
                // Preset reverseChainFromTo_node_end 
                LGSPNode candidate_reverseChainFromTo_node_end = (LGSPNode) parameters[1];
                if(candidate_reverseChainFromTo_node_end == null) {
                    MissingPreset_reverseChainFromTo_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromTo_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromTo_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_end))
                    && candidate_reverseChainFromTo_node_end==candidate_reverseChainFromTo_node_beg
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ReverseChainFromTo taskFor__subpattern0 = PatternAction_ReverseChainFromTo.getNewTask(graph, openTasks);
                taskFor__subpattern0.ReverseChainFromTo_node_from = candidate_reverseChainFromTo_node_beg;
                taskFor__subpattern0.ReverseChainFromTo_node_to = candidate_reverseChainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_reverseChainFromTo_node_beg;
                prevGlobal__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromTo_node_end;
                prevGlobal__candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_reverseChainFromTo.reverseChainFromTo_NodeNums.@beg] = candidate_reverseChainFromTo_node_beg;
                        match.Nodes[(int)Rule_reverseChainFromTo.reverseChainFromTo_NodeNums.@end] = candidate_reverseChainFromTo_node_end;
                        match.EmbeddedGraphs[(int)Rule_reverseChainFromTo.reverseChainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromTo_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromTo_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                        }
                    }
                    continue;
                }
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromTo_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromTo_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromTo_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_reverseChainFromTo_node_end(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList, LGSPNode candidate_reverseChainFromTo_node_beg)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup reverseChainFromTo_node_end 
            int type_id_candidate_reverseChainFromTo_node_end = 0;
            for(LGSPNode head_candidate_reverseChainFromTo_node_end = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromTo_node_end], candidate_reverseChainFromTo_node_end = head_candidate_reverseChainFromTo_node_end.typeNext; candidate_reverseChainFromTo_node_end != head_candidate_reverseChainFromTo_node_end; candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.typeNext)
            {
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromTo_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromTo_node_end))
                    && candidate_reverseChainFromTo_node_end==candidate_reverseChainFromTo_node_beg
                    )
                {
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ReverseChainFromTo taskFor__subpattern0 = PatternAction_ReverseChainFromTo.getNewTask(graph, openTasks);
                taskFor__subpattern0.ReverseChainFromTo_node_from = candidate_reverseChainFromTo_node_beg;
                taskFor__subpattern0.ReverseChainFromTo_node_to = candidate_reverseChainFromTo_node_end;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_reverseChainFromTo_node_beg;
                prevGlobal__candidate_reverseChainFromTo_node_beg = candidate_reverseChainFromTo_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromTo_node_end;
                prevGlobal__candidate_reverseChainFromTo_node_end = candidate_reverseChainFromTo_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromTo_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromTo.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_reverseChainFromTo.reverseChainFromTo_NodeNums.@beg] = candidate_reverseChainFromTo_node_beg;
                        match.Nodes[(int)Rule_reverseChainFromTo.reverseChainFromTo_NodeNums.@end] = candidate_reverseChainFromTo_node_end;
                        match.EmbeddedGraphs[(int)Rule_reverseChainFromTo.reverseChainFromTo_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                        candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                        return;
                    }
                    candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
                    candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                    continue;
                }
                candidate_reverseChainFromTo_node_beg.flags = candidate_reverseChainFromTo_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_beg;
                candidate_reverseChainFromTo_node_end.flags = candidate_reverseChainFromTo_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromTo_node_end;
            }
            return;
        }
    }

    public class Action_createReverseChain : LGSPAction
    {
        public Action_createReverseChain() {
            rulePattern = Rule_createReverseChain.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 0, 0, 0, 0 + 0);
        }

        public override string Name { get { return "createReverseChain"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_createReverseChain instance = new Action_createReverseChain();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
            match.patternGraph = rulePattern.patternGraph;
            matches.matchesList.PositionWasFilledFixIt();
            // if enough matches were found, we leave
            if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
            {
                return matches;
            }
            return matches;
        }
    }

    public class Action_chainFromToReverse : LGSPAction
    {
        public Action_chainFromToReverse() {
            rulePattern = Rule_chainFromToReverse.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 2, 0, 0, 1 + 0);
        }

        public override string Name { get { return "chainFromToReverse"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFromToReverse instance = new Action_chainFromToReverse();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFromToReverse_node_beg 
            LGSPNode candidate_chainFromToReverse_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFromToReverse_node_beg == null) {
                MissingPreset_chainFromToReverse_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_chainFromToReverse_node_beg;
            if(negLevel <= MAX_NEG_LEVEL) {
                prev__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_chainFromToReverse_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
            } else {
                prev__candidate_chainFromToReverse_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_beg) ? 1U : 0U;
                if(prev__candidate_chainFromToReverse_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromToReverse_node_beg,candidate_chainFromToReverse_node_beg);
            }
            // Preset chainFromToReverse_node_end 
            LGSPNode candidate_chainFromToReverse_node_end = (LGSPNode) parameters[1];
            if(candidate_chainFromToReverse_node_end == null) {
                MissingPreset_chainFromToReverse_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverse_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromToReverse_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_end))
                && candidate_chainFromToReverse_node_end==candidate_chainFromToReverse_node_beg
                )
            {
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                    }
                }
                return matches;
            }
            // Push subpattern matching task for cftr
            PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(graph, openTasks);
            taskFor_cftr.ChainFromToReverse_node_from = candidate_chainFromToReverse_node_beg;
            taskFor_cftr.ChainFromToReverse_node_to = candidate_chainFromToReverse_node_end;
            openTasks.Push(taskFor_cftr);
            uint prevGlobal__candidate_chainFromToReverse_node_beg;
            prevGlobal__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverse_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_chainFromToReverse_node_end;
            prevGlobal__candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverse_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for cftr
            openTasks.Pop();
            PatternAction_ChainFromToReverse.releaseTask(taskFor_cftr);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFromToReverse.chainFromToReverse_NodeNums.@beg] = candidate_chainFromToReverse_node_beg;
                    match.Nodes[(int)Rule_chainFromToReverse.chainFromToReverse_NodeNums.@end] = candidate_chainFromToReverse_node_end;
                    match.EmbeddedGraphs[(int)Rule_chainFromToReverse.chainFromToReverse_SubNums.@cftr] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                    }
                }
                return matches;
            }
            candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
            candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
            if(negLevel <= MAX_NEG_LEVEL) {
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
            } else { 
                if(prev__candidate_chainFromToReverse_node_beg==0) {
                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_chainFromToReverse_node_beg(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromToReverse_node_beg 
            int type_id_candidate_chainFromToReverse_node_beg = 0;
            for(LGSPNode head_candidate_chainFromToReverse_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverse_node_beg], candidate_chainFromToReverse_node_beg = head_candidate_chainFromToReverse_node_beg.typeNext; candidate_chainFromToReverse_node_beg != head_candidate_chainFromToReverse_node_beg; candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.typeNext)
            {
                uint prev__candidate_chainFromToReverse_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_chainFromToReverse_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_chainFromToReverse_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_beg) ? 1U : 0U;
                    if(prev__candidate_chainFromToReverse_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromToReverse_node_beg,candidate_chainFromToReverse_node_beg);
                }
                // Preset chainFromToReverse_node_end 
                LGSPNode candidate_chainFromToReverse_node_end = (LGSPNode) parameters[1];
                if(candidate_chainFromToReverse_node_end == null) {
                    MissingPreset_chainFromToReverse_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverse_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverse_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromToReverse_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_end))
                    && candidate_chainFromToReverse_node_end==candidate_chainFromToReverse_node_beg
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    continue;
                }
                // Push subpattern matching task for cftr
                PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(graph, openTasks);
                taskFor_cftr.ChainFromToReverse_node_from = candidate_chainFromToReverse_node_beg;
                taskFor_cftr.ChainFromToReverse_node_to = candidate_chainFromToReverse_node_end;
                openTasks.Push(taskFor_cftr);
                uint prevGlobal__candidate_chainFromToReverse_node_beg;
                prevGlobal__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverse_node_end;
                prevGlobal__candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for cftr
                openTasks.Pop();
                PatternAction_ChainFromToReverse.releaseTask(taskFor_cftr);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromToReverse.chainFromToReverse_NodeNums.@beg] = candidate_chainFromToReverse_node_beg;
                        match.Nodes[(int)Rule_chainFromToReverse.chainFromToReverse_NodeNums.@end] = candidate_chainFromToReverse_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromToReverse.chainFromToReverse_SubNums.@cftr] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverse_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverse_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                        }
                    }
                    continue;
                }
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverse_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverse_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverse_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_chainFromToReverse_node_end(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList, LGSPNode candidate_chainFromToReverse_node_beg)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromToReverse_node_end 
            int type_id_candidate_chainFromToReverse_node_end = 0;
            for(LGSPNode head_candidate_chainFromToReverse_node_end = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverse_node_end], candidate_chainFromToReverse_node_end = head_candidate_chainFromToReverse_node_end.typeNext; candidate_chainFromToReverse_node_end != head_candidate_chainFromToReverse_node_end; candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.typeNext)
            {
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromToReverse_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverse_node_end))
                    && candidate_chainFromToReverse_node_end==candidate_chainFromToReverse_node_beg
                    )
                {
                    continue;
                }
                // Push subpattern matching task for cftr
                PatternAction_ChainFromToReverse taskFor_cftr = PatternAction_ChainFromToReverse.getNewTask(graph, openTasks);
                taskFor_cftr.ChainFromToReverse_node_from = candidate_chainFromToReverse_node_beg;
                taskFor_cftr.ChainFromToReverse_node_to = candidate_chainFromToReverse_node_end;
                openTasks.Push(taskFor_cftr);
                uint prevGlobal__candidate_chainFromToReverse_node_beg;
                prevGlobal__candidate_chainFromToReverse_node_beg = candidate_chainFromToReverse_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverse_node_end;
                prevGlobal__candidate_chainFromToReverse_node_end = candidate_chainFromToReverse_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverse_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for cftr
                openTasks.Pop();
                PatternAction_ChainFromToReverse.releaseTask(taskFor_cftr);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromToReverse.chainFromToReverse_NodeNums.@beg] = candidate_chainFromToReverse_node_beg;
                        match.Nodes[(int)Rule_chainFromToReverse.chainFromToReverse_NodeNums.@end] = candidate_chainFromToReverse_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromToReverse.chainFromToReverse_SubNums.@cftr] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                        candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                        return;
                    }
                    candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
                    candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                    continue;
                }
                candidate_chainFromToReverse_node_beg.flags = candidate_chainFromToReverse_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_beg;
                candidate_chainFromToReverse_node_end.flags = candidate_chainFromToReverse_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverse_node_end;
            }
            return;
        }
    }

    public class Action_chainFromToReverseToCommon : LGSPAction
    {
        public Action_chainFromToReverseToCommon() {
            rulePattern = Rule_chainFromToReverseToCommon.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 2, 0, 0, 1 + 0);
        }

        public override string Name { get { return "chainFromToReverseToCommon"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_chainFromToReverseToCommon instance = new Action_chainFromToReverseToCommon();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset chainFromToReverseToCommon_node_beg 
            LGSPNode candidate_chainFromToReverseToCommon_node_beg = (LGSPNode) parameters[0];
            if(candidate_chainFromToReverseToCommon_node_beg == null) {
                MissingPreset_chainFromToReverseToCommon_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_chainFromToReverseToCommon_node_beg;
            if(negLevel <= MAX_NEG_LEVEL) {
                prev__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_chainFromToReverseToCommon_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
            } else {
                prev__candidate_chainFromToReverseToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_beg) ? 1U : 0U;
                if(prev__candidate_chainFromToReverseToCommon_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromToReverseToCommon_node_beg,candidate_chainFromToReverseToCommon_node_beg);
            }
            // Preset chainFromToReverseToCommon_node_end 
            LGSPNode candidate_chainFromToReverseToCommon_node_end = (LGSPNode) parameters[1];
            if(candidate_chainFromToReverseToCommon_node_end == null) {
                MissingPreset_chainFromToReverseToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverseToCommon_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromToReverseToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_end))
                && candidate_chainFromToReverseToCommon_node_end==candidate_chainFromToReverseToCommon_node_beg
                )
            {
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                    }
                }
                return matches;
            }
            // Push subpattern matching task for cftrtc
            PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(graph, openTasks);
            taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_chainFromToReverseToCommon_node_beg;
            taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_chainFromToReverseToCommon_node_end;
            openTasks.Push(taskFor_cftrtc);
            uint prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
            prevGlobal__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverseToCommon_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            prevGlobal__candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_chainFromToReverseToCommon_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for cftrtc
            openTasks.Pop();
            PatternAction_ChainFromToReverseToCommon.releaseTask(taskFor_cftrtc);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_NodeNums.@beg] = candidate_chainFromToReverseToCommon_node_beg;
                    match.Nodes[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_NodeNums.@end] = candidate_chainFromToReverseToCommon_node_end;
                    match.EmbeddedGraphs[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_SubNums.@cftrtc] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                    }
                }
                return matches;
            }
            candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
            candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            if(negLevel <= MAX_NEG_LEVEL) {
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
            } else { 
                if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_chainFromToReverseToCommon_node_beg(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromToReverseToCommon_node_beg 
            int type_id_candidate_chainFromToReverseToCommon_node_beg = 0;
            for(LGSPNode head_candidate_chainFromToReverseToCommon_node_beg = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverseToCommon_node_beg], candidate_chainFromToReverseToCommon_node_beg = head_candidate_chainFromToReverseToCommon_node_beg.typeNext; candidate_chainFromToReverseToCommon_node_beg != head_candidate_chainFromToReverseToCommon_node_beg; candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.typeNext)
            {
                uint prev__candidate_chainFromToReverseToCommon_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_chainFromToReverseToCommon_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_chainFromToReverseToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_beg) ? 1U : 0U;
                    if(prev__candidate_chainFromToReverseToCommon_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_chainFromToReverseToCommon_node_beg,candidate_chainFromToReverseToCommon_node_beg);
                }
                // Preset chainFromToReverseToCommon_node_end 
                LGSPNode candidate_chainFromToReverseToCommon_node_end = (LGSPNode) parameters[1];
                if(candidate_chainFromToReverseToCommon_node_end == null) {
                    MissingPreset_chainFromToReverseToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_chainFromToReverseToCommon_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromToReverseToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_end))
                    && candidate_chainFromToReverseToCommon_node_end==candidate_chainFromToReverseToCommon_node_beg
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    continue;
                }
                // Push subpattern matching task for cftrtc
                PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(graph, openTasks);
                taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_chainFromToReverseToCommon_node_beg;
                taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_chainFromToReverseToCommon_node_end;
                openTasks.Push(taskFor_cftrtc);
                uint prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                prevGlobal__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                prevGlobal__candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for cftrtc
                openTasks.Pop();
                PatternAction_ChainFromToReverseToCommon.releaseTask(taskFor_cftrtc);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_NodeNums.@beg] = candidate_chainFromToReverseToCommon_node_beg;
                        match.Nodes[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_NodeNums.@end] = candidate_chainFromToReverseToCommon_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_SubNums.@cftrtc] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                        } else { 
                            if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                    } else { 
                        if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                        }
                    }
                    continue;
                }
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_chainFromToReverseToCommon_node_beg;
                } else { 
                    if(prev__candidate_chainFromToReverseToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_chainFromToReverseToCommon_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_chainFromToReverseToCommon_node_end(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList, LGSPNode candidate_chainFromToReverseToCommon_node_beg)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup chainFromToReverseToCommon_node_end 
            int type_id_candidate_chainFromToReverseToCommon_node_end = 0;
            for(LGSPNode head_candidate_chainFromToReverseToCommon_node_end = graph.nodesByTypeHeads[type_id_candidate_chainFromToReverseToCommon_node_end], candidate_chainFromToReverseToCommon_node_end = head_candidate_chainFromToReverseToCommon_node_end.typeNext; candidate_chainFromToReverseToCommon_node_end != head_candidate_chainFromToReverseToCommon_node_end; candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.typeNext)
            {
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_chainFromToReverseToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_chainFromToReverseToCommon_node_end))
                    && candidate_chainFromToReverseToCommon_node_end==candidate_chainFromToReverseToCommon_node_beg
                    )
                {
                    continue;
                }
                // Push subpattern matching task for cftrtc
                PatternAction_ChainFromToReverseToCommon taskFor_cftrtc = PatternAction_ChainFromToReverseToCommon.getNewTask(graph, openTasks);
                taskFor_cftrtc.ChainFromToReverseToCommon_node_from = candidate_chainFromToReverseToCommon_node_beg;
                taskFor_cftrtc.ChainFromToReverseToCommon_node_to = candidate_chainFromToReverseToCommon_node_end;
                openTasks.Push(taskFor_cftrtc);
                uint prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                prevGlobal__candidate_chainFromToReverseToCommon_node_beg = candidate_chainFromToReverseToCommon_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                prevGlobal__candidate_chainFromToReverseToCommon_node_end = candidate_chainFromToReverseToCommon_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_chainFromToReverseToCommon_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for cftrtc
                openTasks.Pop();
                PatternAction_ChainFromToReverseToCommon.releaseTask(taskFor_cftrtc);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_NodeNums.@beg] = candidate_chainFromToReverseToCommon_node_beg;
                        match.Nodes[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_NodeNums.@end] = candidate_chainFromToReverseToCommon_node_end;
                        match.EmbeddedGraphs[(int)Rule_chainFromToReverseToCommon.chainFromToReverseToCommon_SubNums.@cftrtc] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                        candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                        return;
                    }
                    candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
                    candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                    continue;
                }
                candidate_chainFromToReverseToCommon_node_beg.flags = candidate_chainFromToReverseToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_beg;
                candidate_chainFromToReverseToCommon_node_end.flags = candidate_chainFromToReverseToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_chainFromToReverseToCommon_node_end;
            }
            return;
        }
    }

    public class Action_reverseChainFromToToCommon : LGSPAction
    {
        public Action_reverseChainFromToToCommon() {
            rulePattern = Rule_reverseChainFromToToCommon.Instance;
            patternGraph = rulePattern.patternGraph;
            DynamicMatch = myMatch;
            matches = new LGSPMatches(this, 3, 0, 0, 1 + 0);
        }

        public override string Name { get { return "reverseChainFromToToCommon"; } }
        private LGSPMatches matches;

        public static LGSPAction Instance { get { return instance; } }
        private static Action_reverseChainFromToToCommon instance = new Action_reverseChainFromToToCommon();

        public LGSPMatches myMatch(LGSPGraph graph, int maxMatches, object[] parameters)
        {
            matches.matchesList.Clear();
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            Stack<LGSPSubpatternAction> openTasks = new Stack<LGSPSubpatternAction>();
            List<Stack<LGSPMatch>> foundPartialMatches = new List<Stack<LGSPMatch>>();
            List<Stack<LGSPMatch>> matchesList = foundPartialMatches;
            // Preset reverseChainFromToToCommon_node_beg 
            LGSPNode candidate_reverseChainFromToToCommon_node_beg = (LGSPNode) parameters[0];
            if(candidate_reverseChainFromToToCommon_node_beg == null) {
                MissingPreset_reverseChainFromToToCommon_node_beg(graph, maxMatches, parameters, null, null, null);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    return matches;
                }
                return matches;
            }
            uint prev__candidate_reverseChainFromToToCommon_node_beg;
            if(negLevel <= MAX_NEG_LEVEL) {
                prev__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_reverseChainFromToToCommon_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
            } else {
                prev__candidate_reverseChainFromToToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_beg) ? 1U : 0U;
                if(prev__candidate_reverseChainFromToToCommon_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_reverseChainFromToToCommon_node_beg,candidate_reverseChainFromToToCommon_node_beg);
            }
            // Preset reverseChainFromToToCommon_node_end 
            LGSPNode candidate_reverseChainFromToToCommon_node_end = (LGSPNode) parameters[1];
            if(candidate_reverseChainFromToToCommon_node_end == null) {
                MissingPreset_reverseChainFromToToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end))
                && candidate_reverseChainFromToToCommon_node_end==candidate_reverseChainFromToToCommon_node_beg
                )
            {
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            uint prev__candidate_reverseChainFromToToCommon_node_end;
            if(negLevel <= MAX_NEG_LEVEL) {
                prev__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel;
                candidate_reverseChainFromToToCommon_node_end.flags |= LGSPNode.IS_MATCHED<<negLevel;
            } else {
                prev__candidate_reverseChainFromToToCommon_node_end = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end) ? 1U : 0U;
                if(prev__candidate_reverseChainFromToToCommon_node_end==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_reverseChainFromToToCommon_node_end,candidate_reverseChainFromToToCommon_node_end);
            }
            // Preset reverseChainFromToToCommon_node_common 
            LGSPNode candidate_reverseChainFromToToCommon_node_common = (LGSPNode) parameters[2];
            if(candidate_reverseChainFromToToCommon_node_common == null) {
                MissingPreset_reverseChainFromToToCommon_node_common(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg, candidate_reverseChainFromToToCommon_node_end);
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common))
                && (candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_beg
                    || candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_end
                    )
                )
            {
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            // Push subpattern matching task for _subpattern0
            PatternAction_ReverseChainFromToToCommon taskFor__subpattern0 = PatternAction_ReverseChainFromToToCommon.getNewTask(graph, openTasks);
            taskFor__subpattern0.ReverseChainFromToToCommon_node_from = candidate_reverseChainFromToToCommon_node_beg;
            taskFor__subpattern0.ReverseChainFromToToCommon_node_to = candidate_reverseChainFromToToCommon_node_end;
            taskFor__subpattern0.ReverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common;
            openTasks.Push(taskFor__subpattern0);
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
            prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromToToCommon_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
            prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromToToCommon_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            candidate_reverseChainFromToToCommon_node_common.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
            // Match subpatterns 
            openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
            // Pop subpattern matching task for _subpattern0
            openTasks.Pop();
            PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
            // Check whether subpatterns were found 
            if(matchesList.Count>0) {
                // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                {
                    LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                    match.patternGraph = rulePattern.patternGraph;
                    match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@beg] = candidate_reverseChainFromToToCommon_node_beg;
                    match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@end] = candidate_reverseChainFromToToCommon_node_end;
                    match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@common] = candidate_reverseChainFromToToCommon_node_common;
                    match.EmbeddedGraphs[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                    matches.matchesList.PositionWasFilledFixIt();
                }
                matchesList.Clear();
                // if enough matches were found, we leave
                if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                {
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    return matches;
                }
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
                return matches;
            }
            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
            candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            if(negLevel <= MAX_NEG_LEVEL) {
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
            } else { 
                if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                }
            }
            if(negLevel <= MAX_NEG_LEVEL) {
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
            } else { 
                if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                    graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                }
            }
            return matches;
        }
        public void MissingPreset_reverseChainFromToToCommon_node_beg(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup reverseChainFromToToCommon_node_beg 
            int type_id_candidate_reverseChainFromToToCommon_node_beg = 0;
            for(LGSPNode head_candidate_reverseChainFromToToCommon_node_beg = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromToToCommon_node_beg], candidate_reverseChainFromToToCommon_node_beg = head_candidate_reverseChainFromToToCommon_node_beg.typeNext; candidate_reverseChainFromToToCommon_node_beg != head_candidate_reverseChainFromToToCommon_node_beg; candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.typeNext)
            {
                uint prev__candidate_reverseChainFromToToCommon_node_beg;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_reverseChainFromToToCommon_node_beg.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_reverseChainFromToToCommon_node_beg = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_beg) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromToToCommon_node_beg==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_reverseChainFromToToCommon_node_beg,candidate_reverseChainFromToToCommon_node_beg);
                }
                // Preset reverseChainFromToToCommon_node_end 
                LGSPNode candidate_reverseChainFromToToCommon_node_end = (LGSPNode) parameters[1];
                if(candidate_reverseChainFromToToCommon_node_end == null) {
                    MissingPreset_reverseChainFromToToCommon_node_end(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end))
                    && candidate_reverseChainFromToToCommon_node_end==candidate_reverseChainFromToToCommon_node_beg
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                uint prev__candidate_reverseChainFromToToCommon_node_end;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_reverseChainFromToToCommon_node_end.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_reverseChainFromToToCommon_node_end = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromToToCommon_node_end==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_reverseChainFromToToCommon_node_end,candidate_reverseChainFromToToCommon_node_end);
                }
                // Preset reverseChainFromToToCommon_node_common 
                LGSPNode candidate_reverseChainFromToToCommon_node_common = (LGSPNode) parameters[2];
                if(candidate_reverseChainFromToToCommon_node_common == null) {
                    MissingPreset_reverseChainFromToToCommon_node_common(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg, candidate_reverseChainFromToToCommon_node_end);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common))
                    && (candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_beg
                        || candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_end
                        )
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ReverseChainFromToToCommon taskFor__subpattern0 = PatternAction_ReverseChainFromToToCommon.getNewTask(graph, openTasks);
                taskFor__subpattern0.ReverseChainFromToToCommon_node_from = candidate_reverseChainFromToToCommon_node_beg;
                taskFor__subpattern0.ReverseChainFromToToCommon_node_to = candidate_reverseChainFromToToCommon_node_end;
                taskFor__subpattern0.ReverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_common.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@beg] = candidate_reverseChainFromToToCommon_node_beg;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@end] = candidate_reverseChainFromToToCommon_node_end;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@common] = candidate_reverseChainFromToToCommon_node_common;
                        match.EmbeddedGraphs[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                            }
                        }
                        return;
                    }
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                        }
                    }
                    continue;
                }
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_beg;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_beg==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_beg);
                    }
                }
            }
            return;
        }
        public void MissingPreset_reverseChainFromToToCommon_node_end(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList, LGSPNode candidate_reverseChainFromToToCommon_node_beg)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup reverseChainFromToToCommon_node_end 
            int type_id_candidate_reverseChainFromToToCommon_node_end = 0;
            for(LGSPNode head_candidate_reverseChainFromToToCommon_node_end = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromToToCommon_node_end], candidate_reverseChainFromToToCommon_node_end = head_candidate_reverseChainFromToToCommon_node_end.typeNext; candidate_reverseChainFromToToCommon_node_end != head_candidate_reverseChainFromToToCommon_node_end; candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.typeNext)
            {
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end))
                    && candidate_reverseChainFromToToCommon_node_end==candidate_reverseChainFromToToCommon_node_beg
                    )
                {
                    continue;
                }
                uint prev__candidate_reverseChainFromToToCommon_node_end;
                if(negLevel <= MAX_NEG_LEVEL) {
                    prev__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED<<negLevel;
                    candidate_reverseChainFromToToCommon_node_end.flags |= LGSPNode.IS_MATCHED<<negLevel;
                } else {
                    prev__candidate_reverseChainFromToToCommon_node_end = graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_end) ? 1U : 0U;
                    if(prev__candidate_reverseChainFromToToCommon_node_end==0) graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Add(candidate_reverseChainFromToToCommon_node_end,candidate_reverseChainFromToToCommon_node_end);
                }
                // Preset reverseChainFromToToCommon_node_common 
                LGSPNode candidate_reverseChainFromToToCommon_node_common = (LGSPNode) parameters[2];
                if(candidate_reverseChainFromToToCommon_node_common == null) {
                    MissingPreset_reverseChainFromToToCommon_node_common(graph, maxMatches, parameters, null, null, null, candidate_reverseChainFromToToCommon_node_beg, candidate_reverseChainFromToToCommon_node_end);
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        return;
                    }
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    continue;
                }
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common))
                    && (candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_beg
                        || candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_end
                        )
                    )
                {
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ReverseChainFromToToCommon taskFor__subpattern0 = PatternAction_ReverseChainFromToToCommon.getNewTask(graph, openTasks);
                taskFor__subpattern0.ReverseChainFromToToCommon_node_from = candidate_reverseChainFromToToCommon_node_beg;
                taskFor__subpattern0.ReverseChainFromToToCommon_node_to = candidate_reverseChainFromToToCommon_node_end;
                taskFor__subpattern0.ReverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_common.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@beg] = candidate_reverseChainFromToToCommon_node_beg;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@end] = candidate_reverseChainFromToToCommon_node_end;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@common] = candidate_reverseChainFromToToCommon_node_common;
                        match.EmbeddedGraphs[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                        if(negLevel <= MAX_NEG_LEVEL) {
                            candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                        } else { 
                            if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                                graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                            }
                        }
                        return;
                    }
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    if(negLevel <= MAX_NEG_LEVEL) {
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                    } else { 
                        if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                            graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                        }
                    }
                    continue;
                }
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                if(negLevel <= MAX_NEG_LEVEL) {
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED<<negLevel) | prev__candidate_reverseChainFromToToCommon_node_end;
                } else { 
                    if(prev__candidate_reverseChainFromToToCommon_node_end==0) {
                        graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.Remove(candidate_reverseChainFromToToCommon_node_end);
                    }
                }
            }
            return;
        }
        public void MissingPreset_reverseChainFromToToCommon_node_common(LGSPGraph graph, int maxMatches, object[] parameters, Stack<LGSPSubpatternAction> openTasks, List<Stack<LGSPMatch>> foundPartialMatches, List<Stack<LGSPMatch>> matchesList, LGSPNode candidate_reverseChainFromToToCommon_node_beg, LGSPNode candidate_reverseChainFromToToCommon_node_end)
        {
            const int MAX_NEG_LEVEL = 5;
            int negLevel = 0;
            // Lookup reverseChainFromToToCommon_node_common 
            int type_id_candidate_reverseChainFromToToCommon_node_common = 0;
            for(LGSPNode head_candidate_reverseChainFromToToCommon_node_common = graph.nodesByTypeHeads[type_id_candidate_reverseChainFromToToCommon_node_common], candidate_reverseChainFromToToCommon_node_common = head_candidate_reverseChainFromToToCommon_node_common.typeNext; candidate_reverseChainFromToToCommon_node_common != head_candidate_reverseChainFromToToCommon_node_common; candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.typeNext)
            {
                if((negLevel<=MAX_NEG_LEVEL ? (candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED<<negLevel) == LGSPNode.IS_MATCHED<<negLevel : graph.atNegLevelMatchedElements[negLevel-MAX_NEG_LEVEL-1].fst.ContainsKey(candidate_reverseChainFromToToCommon_node_common))
                    && (candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_beg
                        || candidate_reverseChainFromToToCommon_node_common==candidate_reverseChainFromToToCommon_node_end
                        )
                    )
                {
                    continue;
                }
                // Push subpattern matching task for _subpattern0
                PatternAction_ReverseChainFromToToCommon taskFor__subpattern0 = PatternAction_ReverseChainFromToToCommon.getNewTask(graph, openTasks);
                taskFor__subpattern0.ReverseChainFromToToCommon_node_from = candidate_reverseChainFromToToCommon_node_beg;
                taskFor__subpattern0.ReverseChainFromToToCommon_node_to = candidate_reverseChainFromToToCommon_node_end;
                taskFor__subpattern0.ReverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common;
                openTasks.Push(taskFor__subpattern0);
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                prevGlobal__candidate_reverseChainFromToToCommon_node_beg = candidate_reverseChainFromToToCommon_node_beg.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_beg.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                prevGlobal__candidate_reverseChainFromToToCommon_node_end = candidate_reverseChainFromToToCommon_node_end.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_end.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                uint prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                prevGlobal__candidate_reverseChainFromToToCommon_node_common = candidate_reverseChainFromToToCommon_node_common.flags & LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                candidate_reverseChainFromToToCommon_node_common.flags |= LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN;
                // Match subpatterns 
                openTasks.Peek().myMatch(matchesList, maxMatches - foundPartialMatches.Count, negLevel);
                // Pop subpattern matching task for _subpattern0
                openTasks.Pop();
                PatternAction_ReverseChainFromToToCommon.releaseTask(taskFor__subpattern0);
                // Check whether subpatterns were found 
                if(matchesList.Count>0) {
                    // subpatterns/alternatives were found, extend the partial matches by our local match object, becoming a complete match object and save it
                    foreach(Stack<LGSPMatch> currentFoundPartialMatch in matchesList)
                    {
                        LGSPMatch match = matches.matchesList.GetNextUnfilledPosition();
                        match.patternGraph = rulePattern.patternGraph;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@beg] = candidate_reverseChainFromToToCommon_node_beg;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@end] = candidate_reverseChainFromToToCommon_node_end;
                        match.Nodes[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_NodeNums.@common] = candidate_reverseChainFromToToCommon_node_common;
                        match.EmbeddedGraphs[(int)Rule_reverseChainFromToToCommon.reverseChainFromToToCommon_SubNums.@_subpattern0] = currentFoundPartialMatch.Pop();
                        matches.matchesList.PositionWasFilledFixIt();
                    }
                    matchesList.Clear();
                    // if enough matches were found, we leave
                    if(maxMatches > 0 && matches.matchesList.Count >= maxMatches)
                    {
                        candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                        candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                        candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                        return;
                    }
                    candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
                    candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                    candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                    continue;
                }
                candidate_reverseChainFromToToCommon_node_beg.flags = candidate_reverseChainFromToToCommon_node_beg.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_beg;
                candidate_reverseChainFromToToCommon_node_end.flags = candidate_reverseChainFromToToCommon_node_end.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_end;
                candidate_reverseChainFromToToCommon_node_common.flags = candidate_reverseChainFromToToCommon_node_common.flags & ~(LGSPNode.IS_MATCHED_BY_ENCLOSING_PATTERN) | prevGlobal__candidate_reverseChainFromToToCommon_node_common;
            }
            return;
        }
    }


    public class RecursiveActions : LGSPActions
    {
        public RecursiveActions(LGSPGraph lgspgraph, String modelAsmName, String actionsAsmName)
            : base(lgspgraph, modelAsmName, actionsAsmName)
        {
            InitActions();
        }

        public RecursiveActions(LGSPGraph lgspgraph)
            : base(lgspgraph)
        {
            InitActions();
        }

        private void InitActions()
        {
            actions.Add("createChain", (LGSPAction) Action_createChain.Instance);
            actions.Add("chainFromTo", (LGSPAction) Action_chainFromTo.Instance);
            actions.Add("chainFrom", (LGSPAction) Action_chainFrom.Instance);
            actions.Add("chainFromComplete", (LGSPAction) Action_chainFromComplete.Instance);
            actions.Add("createBlowball", (LGSPAction) Action_createBlowball.Instance);
            actions.Add("blowball", (LGSPAction) Action_blowball.Instance);
            actions.Add("reverseChainFromTo", (LGSPAction) Action_reverseChainFromTo.Instance);
            actions.Add("createReverseChain", (LGSPAction) Action_createReverseChain.Instance);
            actions.Add("chainFromToReverse", (LGSPAction) Action_chainFromToReverse.Instance);
            actions.Add("chainFromToReverseToCommon", (LGSPAction) Action_chainFromToReverseToCommon.Instance);
            actions.Add("reverseChainFromToToCommon", (LGSPAction) Action_reverseChainFromToToCommon.Instance);
        }

        public override String Name { get { return "RecursiveActions"; } }
        public override String ModelMD5Hash { get { return "cee2fe3026e313db20fe574ef2ea4643"; } }
    }
}